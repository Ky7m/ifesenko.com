using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PersonalHomePage.Services.HealthService.Model;
using PersonalHomePage.Services.HealthService.Model.Requests;
using PersonalHomePage.Services.HealthService.Model.Responses;

// await this.MakeRequestAsync("Profile");
// await this.MakeRequestAsync("Devices");
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

        public Task<SummariesResponse> GetDailySummaryAsync(DateTime startTime, DateTime endTime, int? maxItemsToReturn = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetSummaryInfo(startTime, endTime, "Daily", maxItemsToReturn, cancellationToken);
        }


        public Task<SummariesResponse> GetTodaysSummaryAsync(int? maxItemsToReturn = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetDailySummaryAsync(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow, maxItemsToReturn, cancellationToken);
        }


        public Task<SummariesResponse> GetHourlySummaryAsync(DateTime startTime, DateTime endTime, int? maxItemsToReturn = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetSummaryInfo(startTime, endTime, "Hourly", maxItemsToReturn, cancellationToken);
        }

        public Task<SummariesResponse> GetTodaysHourlySummaryAsync(int? maxItemsToReturn = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetHourlySummaryAsync(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow, maxItemsToReturn, cancellationToken);
        }


        public async Task<Profile> GetProfileAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await ValidateCredentials();

            var response = await GetResponse<Profile>("Profile", new Dictionary<string, string>(), cancellationToken);

            return response;
        }
       

        public async Task<ActivitiesResponse> GetActivitiesAsync(ActivitiesRequest request = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            await ValidateCredentials();

            var postData = request != null ? request.ToDictionary() : new Dictionary<string, string>();

            var response = await GetResponse<ActivitiesResponse>("Activities", postData, cancellationToken);

            return response;
        }

        private async Task<SummariesResponse> GetSummaryInfo(DateTime startTime, DateTime endTime, string period, int? maxItemsToReturn, CancellationToken cancellationToken)
        {
            await ValidateCredentials();

            var startTimeString = startTime.ToString("O");
            var endtimeString = endTime.ToString("O");

            var postData = new Dictionary<string, string>
            {
                { "startTime", startTimeString },
                { "endTime", endtimeString }
            };

            if (maxItemsToReturn.HasValue)
            {
                postData.Add("maxPageSize", maxItemsToReturn.Value.ToString());
            }

            var path = $"Summaries/{period}";

            return await GetResponse<SummariesResponse>(path, postData, cancellationToken);
        }

        private async Task<TReturnType> GetResponse<TReturnType>(string path, Dictionary<string, string> postData, CancellationToken cancellationToken = default(CancellationToken), string altBaseUrl = null)
        {
            var uri = new UriBuilder(altBaseUrl ?? _apiUri);
            uri.Path += path;

            var queryParams = string.Join("&", postData.Select(x => $"{x.Key}={x.Value}"));
            uri.Query = queryParams;

            var response = await _httpClient.GetAsync(uri.Uri, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await ExchangeCodeAsync(_credentials.RefreshToken, true, cancellationToken);

                // Re-issue the same request (will use new auth token now)
                return await GetResponse<TReturnType>(path, postData, cancellationToken, altBaseUrl);
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

        private async Task<LiveIdCredentials> ExchangeCodeAsync(string code, bool isTokenRefresh = false, CancellationToken cancellationToken = default(CancellationToken))
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

            var response = await GetResponse<LiveIdCredentials>("", postData, cancellationToken, TokenUrl);
            SetCredentials(response);
            if (isTokenRefresh)
            {
                _settingsService.ReplaceSettingValueForService("HealthService", "AccessToken", response.AccessToken);
                _settingsService.ReplaceSettingValueForService("HealthService", "RefreshToken", response.RefreshToken);
            }

            return response;
        }
    }
}
