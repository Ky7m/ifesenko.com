using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PersonalHomePage.Extensions;
using PersonalHomePage.Services.HealthService.Model;
using PersonalHomePage.Services.HealthService.Model.Requests;
using PersonalHomePage.Services.HealthService.Model.Responses;

// await GetActivity("Sleep");
// await GetActivity("FreePlay");
// await GetActivity("GuidedWorkout");

namespace PersonalHomePage.Services.HealthService
{
    public sealed class HealthService
    {
        private const string RedirectUri = "https://login.live.com/oauth20_desktop.srf";
        private const string TokenUrl = "https://login.live.com/oauth20_token.srf";

        private readonly HttpClient _httpClient;

        private readonly string _apiUri;
        private readonly string _clientId;
        private readonly string _clientSecret;

        private LiveIdCredentials _credentials;

        private readonly SettingsService _settingsService;

        public HealthService()
        {
            _settingsService = new SettingsService();
            var settings = _settingsService.RetrieveAllSettingsValuesForService("HealthService");

            _apiUri = settings["ApiUri"];
            _clientId = settings["ClientId"];
            _clientSecret = settings["ClientSecret"];

            var accessToken = settings["AccessToken"];
            var refreshToken = settings["RefreshToken"];

            _credentials = new LiveIdCredentials { AccessToken = accessToken, RefreshToken = refreshToken };

            var messageHandler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
            };

            _httpClient = new HttpClient(messageHandler);
            SetCredentials(_credentials);
        }

        public Task<SummariesResponse> GetTodaysSummaryAsync()
        {
            var now = DateTime.UtcNow;
            return GetDailySummaryAsync(now.StartOfDay(), now.EndOfDay());
        }

        public async Task<Profile> GetProfileAsync()
        {
            await ValidateCredentials();

            var response = await GetResponse<Profile>("Profile", new Dictionary<string, string>());

            return response;
        }

        public async Task<ActivitiesResponse> GetActivitiesAsync(ActivitiesRequest request = null)
        {
            await ValidateCredentials();

            var postData = request != null ? request.ToDictionary() : new Dictionary<string, string>();

            var response = await GetResponse<ActivitiesResponse>("Activities", postData);

            return response;
        }

        #region Private

        private Task<SummariesResponse> GetDailySummaryAsync(DateTime startTime, DateTime endTime, int? maxItemsToReturn = null)
        {
            return GetSummaryInfo(startTime, endTime, "Daily", maxItemsToReturn);
        }

        private async Task<SummariesResponse> GetSummaryInfo(DateTime startTime, DateTime endTime, string period, int? maxItemsToReturn)
        {
            await ValidateCredentials();

            var postData = new Dictionary<string, string>();

            var startTimeString = startTime.ToString("O");
            var endtimeString = endTime.ToString("O");

            postData.Add("startTime", startTimeString);
            postData.Add("endTime", endtimeString);

            if (maxItemsToReturn.HasValue)
            {
                postData.Add("maxPageSize", maxItemsToReturn.Value.ToString());
            }

            var path = $"Summaries/{period}";

            return await GetResponse<SummariesResponse>(path, postData);
        }

        private async Task<TReturnType> GetResponse<TReturnType>(string path, Dictionary<string, string> postData, string baseUri = null)
        {
            var uri = new UriBuilder(baseUri ?? _apiUri);
            uri.Path += path;

            var queryParams = string.Join("&", postData.Select(x => $"{x.Key}={x.Value}"));
            uri.Query = queryParams;

            var response = await _httpClient.GetAsync(uri.Uri);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await ExchangeCodeAsync(_credentials.RefreshToken, true);

                // Re-issue the same request (will use new auth token now)
                return await GetResponse<TReturnType>(path, postData, baseUri);
            }

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject<TReturnType>(responseString);
            return item;
        }

        private Task<bool> ValidateCredentials()
        {
            if (string.IsNullOrEmpty(_credentials?.AccessToken))
            {
                throw new AuthenticationException("No valid credentials have been set");
            }

            return Task.FromResult(true);
        }

        private void SetCredentials(LiveIdCredentials credentials)
        {
            _credentials = credentials;
            _httpClient.DefaultRequestHeaders.Remove(HttpRequestHeader.Authorization.ToString());
            _httpClient.DefaultRequestHeaders.Add(HttpRequestHeader.Authorization.ToString(), $"bearer {_credentials.AccessToken}");
        }

        private async Task<LiveIdCredentials> ExchangeCodeAsync(string code, bool isTokenRefresh = false)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentNullException(nameof(code), "code cannot be null or empty");
            }

            var postData = new Dictionary<string, string>
            {
                {"redirect_uri", Uri.EscapeUriString(RedirectUri)},
                {"client_id", Uri.EscapeUriString(_clientId)},
                {"client_secret", Uri.EscapeUriString(_clientSecret)}
            };

            if (isTokenRefresh)
            {
                postData.Add("refresh_token", Uri.EscapeUriString(code));
                postData.Add("grant_type", "refresh_token");
            }
            else
            {
                postData.Add("code", Uri.EscapeUriString(code));
                postData.Add("grant_type", "authorization_code");
            }

            var response = await GetResponse<LiveIdCredentials>(string.Empty, postData, TokenUrl);
            SetCredentials(response);
            if (isTokenRefresh)
            {
                _settingsService.ReplaceSettingValueForService("HealthService", "AccessToken", response.AccessToken);
                _settingsService.ReplaceSettingValueForService("HealthService", "RefreshToken", response.RefreshToken);
            }

            return response;
        }

        #endregion Private
    }
}
