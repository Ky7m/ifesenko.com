using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace PersonalHomePage.Services
{
    public sealed class HealthService
    {
        const string RedirectUri = "https://login.live.com/oauth20_desktop.srf";

        private string _apiUri;
        private string _clientId;
        private string _clientSecret;
        private string _scope;

        private LiveIdCredentials _credentials;

        public HealthService()
        {
            _apiUri = ConfigurationManager.AppSettings["healthService:ApiUri"];
            _clientId = ConfigurationManager.AppSettings["healthService:ClientId"];
            _clientSecret = ConfigurationManager.AppSettings["healthService:ClientSecret"];
            _scope = ConfigurationManager.AppSettings["healthService:Scope"];

            var accessToken = ConfigurationManager.AppSettings["healthService:AccessToken"];
            var refreshToken = ConfigurationManager.AppSettings["healthService:RefreshToken"];
            _credentials = new LiveIdCredentials(accessToken, refreshToken);

            // await this.MakeRequestAsync("Profile");
            // await this.MakeRequestAsync("Devices");
            // await GetActivity("Sleep");
            // await GetActivity("FreePlay");
            // await GetActivity("GuidedWorkout");
        }

        public Uri GetAuthorizationRequestUri()
        {
            UriBuilder uri = new UriBuilder("https://login.live.com/oauth20_authorize.srf");
            var query = new StringBuilder();

            query.AppendFormat("redirect_uri={0}", Uri.EscapeUriString(RedirectUri));

            query.AppendFormat("&client_id={0}", Uri.EscapeUriString(_clientId));
            query.AppendFormat("&client_secret={0}", Uri.EscapeUriString(_clientSecret));

            query.AppendFormat("&scope={0}", Uri.EscapeUriString(_scope));
            query.Append("&response_type=code");

            uri.Query = query.ToString();
            return uri.Uri;
        }

        public Uri CreateOAuthTokenRequestUri(string code, bool isRefresh)
        {
            UriBuilder uri = new UriBuilder("https://login.live.com/oauth20_token.srf");
            var query = new StringBuilder();

            query.AppendFormat("redirect_uri={0}", Uri.EscapeUriString(RedirectUri));
            query.AppendFormat("&client_id={0}", Uri.EscapeUriString(_clientId));
            query.AppendFormat("&client_secret={0}", Uri.EscapeUriString(_clientSecret));

            if (isRefresh)
            {
                query.AppendFormat("&refresh_token={0}", Uri.EscapeUriString(code));
                query.Append("&grant_type=refresh_token");
            }
            else
            {
                query.AppendFormat("&code={0}", Uri.EscapeUriString(code));
                query.Append("&grant_type=authorization_code");
            }

            uri.Query = query.ToString();
            return uri.Uri;
        }

        private async Task<string> GetRefreshToken(string code, bool isRefresh)
        {
            var request = WebRequest.Create(CreateOAuthTokenRequestUri(code,isRefresh));

            try
            {
                using (var response = await request.GetResponseAsync())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        using (var streamReader = new StreamReader(stream))
                        {
                            var responseString = streamReader.ReadToEnd();
                            var jsonResponse = JObject.Parse(responseString);
                            _credentials.AccessToken = (string)jsonResponse["access_token"];
                            _credentials.ExpiresIn = (long)jsonResponse["expires_in"];
                            _credentials.RefreshToken = (string)jsonResponse["refresh_token"];
                            string error = (string)jsonResponse["error"];

                            return error;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        private async Task<string> MakeRequestAsync(string path, string query = "")
        {
            var http = new HttpClient();
            http.DefaultRequestHeaders.Add(HttpRequestHeader.Authorization.ToString(), string.Format("bearer {0}", _credentials.AccessToken));

            var ub = new UriBuilder(_apiUri);
            ub.Path += path;
            ub.Query = query;

            string resStr = string.Empty;

            var resp = await http.GetAsync(ub.Uri);

            if (resp.StatusCode == HttpStatusCode.Unauthorized)
            {
                await GetRefreshToken(_credentials.RefreshToken, true);

                // Re-issue the same request (will use new auth token now)
                return await MakeRequestAsync(path, query);
            }

            if (resp.IsSuccessStatusCode)
            {
                resStr = await resp.Content.ReadAsStringAsync();
            }
            return resStr;
        }


        public async Task<string> GetDailySummary()
        {
            string startTime = DateTime.UtcNow.AddDays(-1).ToString("O");
            string endtime = DateTime.UtcNow.ToString("O");

            return await this.MakeRequestAsync(
                "Summaries/Daily",
                string.Format("startTime={0}&endTime={1}", startTime, endtime));
        }

        public async Task<string> GetHourlySummary()
        {
            string startTime = DateTime.UtcNow.AddDays(-1).ToString("O");
            string endTime = DateTime.UtcNow.ToString("O");

            return await this.MakeRequestAsync(
                "Summaries/Hourly",
                string.Format("startTime={0}&endTime={1}", startTime, endTime));
        }

        public async Task<string> GetActivitiesSummary()
        {
            string startTime = DateTime.UtcNow.AddDays(-29).ToString("O");
            string endtime = DateTime.UtcNow.ToString("O");

            return await this.MakeRequestAsync(
                "Activities",
                string.Format("startTime={0}&endTime={1}", startTime, endtime));
        }
        public async Task<string> GetActivitySummary(string activity)
        {
            string startTime = DateTime.UtcNow.AddDays(-1).ToString("O");
            string endtime = DateTime.UtcNow.ToString("O");

            return await this.MakeRequestAsync(
                "Activities",
                string.Format("startTime={0}&endTime={1}&activityTypes={2}", startTime, endtime, activity));
        }
    }
}
