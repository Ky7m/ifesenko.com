using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ifesenko.com.Infrastructure.Extensions;
using ifesenko.com.Infrastructure.Services.Implementation.HealthService.Model;
using ifesenko.com.Infrastructure.Services.Implementation.HealthService.Model.Requests;
using ifesenko.com.Infrastructure.Services.Implementation.HealthService.Model.Responses;
using ifesenko.com.Infrastructure.Services.Interfaces;
using Newtonsoft.Json;

namespace ifesenko.com.Infrastructure.Services.Implementation.HealthService
{
    public sealed class HealthService : IHealthService
    {
        private const string RedirectUri = "https://login.live.com/oauth20_desktop.srf";
        private const string TokenUrl = "https://login.live.com/oauth20_token.srf";

        private volatile HttpClient _httpClient;

        private string _apiUri;
        private string _clientId;
        private string _clientSecret;

        private LiveIdCredentials _credentials;

        private readonly ISettingsService _settingsService;

        public HealthService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
            var messageHandler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
            };

            _httpClient = new HttpClient(messageHandler);
            ReadSettings().Wait();
        }

        private async Task ReadSettings()
        {
            var settings = await _settingsService.RetrieveAllSettingsValuesForServiceAsync(nameof(HealthService));

            _apiUri = settings["ApiUri"].Value;
            _clientId = settings["ClientId"].Value;
            _clientSecret = settings["ClientSecret"].Value;

            var accessToken = settings["AccessToken"];
            var expires = accessToken.Timestamp.UtcDateTime.AddSeconds(Convert.ToDouble(settings["ExpiresIn"].Value));
            _credentials = new LiveIdCredentials
            {
                AccessToken = accessToken.Value,
                Expires = expires,
                RefreshToken = settings["RefreshToken"].Value
            };

            SetAuthorizationHttpRequestHeader();
        }

        public async Task<Summary> GetTodaysSummaryAsync()
        {
            var now = DateTime.UtcNow;
            var dailySummaries = await GetDailySummaryAsync(now.StartOfDay(), now.EndOfDay());
            return dailySummaries?.Summaries?.FirstOrDefault();
        }

        public async Task<SleepActivity> GetTodaysSleepActivityAsync()
        {
            var now = DateTime.UtcNow;
            var request = new ActivitiesRequest
            {
                ActivityTypes = new[] { "Sleep" },
                StartTime = now.AddDays(-1.0).StartOfDay(),
                EndTime = now.EndOfDay(),
                MaxItemsReturned = 1
            };
            var activitiesResponse = await GetActivitiesAsync(request);
            return activitiesResponse?.SleepActivities?.FirstOrDefault();
        }

        #region Private

        private async Task<ActivitiesResponse> GetActivitiesAsync(ActivitiesRequest request)
        {
            return await GetResponseAsync<ActivitiesResponse>(BuildRequestUri("Activities", request.ToDictionary()));
        }

        private async Task<SummariesResponse> GetDailySummaryAsync(DateTime startTime, DateTime endTime, int? maxItemsToReturn = null)
        {
            return await GetSummaryInfoAsync(startTime, endTime, "Daily", maxItemsToReturn);
        }

        private async Task<SummariesResponse> GetSummaryInfoAsync(DateTime startTime, DateTime endTime, string period, int? maxItemsToReturn)
        {
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

            return await GetResponseAsync<SummariesResponse>(BuildRequestUri(path, postData));
        }

        private async Task<TReturnType> GetResponseAsync<TReturnType>(Uri requestUri)
        {
            if (DateTime.UtcNow.CompareTo(_credentials.Expires) >= 0)
            {
                await ExchangeCodeAsync(_credentials.RefreshToken);
            }

            var response = await _httpClient.GetAsync(requestUri);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await ExchangeCodeAsync(_credentials.RefreshToken);
                // Re-issue the same request (will use new auth token now)
                return await GetResponseAsync<TReturnType>(requestUri);
            }

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject<TReturnType>(responseString);
            return item;
        }

        private void SetAuthorizationHttpRequestHeader()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _credentials.AccessToken);
        }

        private Uri BuildRequestUri(string path, Dictionary<string, string> postData, string baseUri = null)
        {
            var uri = new UriBuilder(baseUri ?? _apiUri);
            uri.Path += path;

            var queryParams = string.Join("&", postData.Select(x => $"{x.Key}={x.Value}"));
            uri.Query = queryParams;

            return uri.Uri;
        }

        private async Task ExchangeCodeAsync(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentNullException(nameof(code));
            }

            var postData = new Dictionary<string, string>
            {
                {"redirect_uri", Uri.EscapeUriString(RedirectUri)},
                {"client_id", Uri.EscapeUriString(_clientId)},
                {"client_secret", Uri.EscapeUriString(_clientSecret)},
                {"refresh_token", Uri.EscapeUriString(code)},
                {"grant_type", "refresh_token"}
            };

            var response = await _httpClient.GetAsync(BuildRequestUri(string.Empty, postData, TokenUrl));
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            _credentials = JsonConvert.DeserializeObject<LiveIdCredentials>(responseString);
            _credentials.Expires = DateTime.UtcNow.AddSeconds(_credentials.ExpiresIn);

            SetAuthorizationHttpRequestHeader();

            await _settingsService.ReplaceSettingValueForServiceAsync("HealthService", "AccessToken", _credentials.AccessToken);
            await _settingsService.ReplaceSettingValueForServiceAsync("HealthService", "RefreshToken", _credentials.RefreshToken);
            await _settingsService.ReplaceSettingValueForServiceAsync("HealthService", "ExpiresIn", _credentials.ExpiresIn.ToString());
        }

        #endregion Private
    }
}
