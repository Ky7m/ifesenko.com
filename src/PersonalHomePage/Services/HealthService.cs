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

        public Uri CreateOAuthTokenRequestUri(string code, string refreshToken = "")
        {
            UriBuilder uri = new UriBuilder("https://login.live.com/oauth20_token.srf");
            var query = new StringBuilder();

            query.AppendFormat("redirect_uri={0}", Uri.EscapeUriString(RedirectUri));
            query.AppendFormat("&client_id={0}", Uri.EscapeUriString(_clientId));
            query.AppendFormat("&client_secret={0}", Uri.EscapeUriString(_clientSecret));

            string grant = "authorization_code";
            if (!string.IsNullOrEmpty(refreshToken))
            {
                grant = "refresh_token";
                query.AppendFormat("&refresh_token={0}", Uri.EscapeUriString(refreshToken));
            }
            else
            {
                query.AppendFormat("&code={0}", Uri.EscapeUriString(code));
            }

            query.Append(string.Format("&grant_type={0}", grant));
            uri.Query = query.ToString();
            return uri.Uri;
        }

        private async Task<string> GetToken(string code, bool isRefresh)
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

            var request = WebRequest.Create(uri.Uri);

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

        

        public async Task<string> MakeRequestAsync(string path, string query = "")
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
                await GetToken(_credentials.RefreshToken, true);

                // Re-issue the same request (will use new auth token now)
                return await MakeRequestAsync(path, query);
            }

            if (resp.IsSuccessStatusCode)
            {
                resStr = await resp.Content.ReadAsStringAsync();
            }
            return resStr;
        }

      /*

       

        private async void profile_Click(object sender, RoutedEventArgs e)
        {
            var res = await MakeRequestAsync("me/profile");
            // Format the JSON string
            var obj = JsonConvert.DeserializeObject(res);
            res = JsonConvert.SerializeObject(obj, Formatting.Indented);
            TextDisplay.Text = res;
        }

        private async void devices_Click(object sender, RoutedEventArgs e)
        {
            var res = await MakeRequestAsync("me/devices");
            // Format the JSON string
            var obj = JsonConvert.DeserializeObject(res);
            res = JsonConvert.SerializeObject(obj, Formatting.Indented);
            TextDisplay.Text = res;
        }

        private async void summaries_Click(object sender, RoutedEventArgs e)
        {
            var res = await MakeRequestAsync("me/summaries/Daily",
                string.Format("startTime={0}", DateTime.Now.AddYears(-1).ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'")));

            // Format the JSON string
            var obj = JsonConvert.DeserializeObject(res);
            res = JsonConvert.SerializeObject(obj, Formatting.Indented);
            TextDisplay.Text = res;
        }

        private async Task<string> GetActivity(string activity)
        {
            var res = await MakeRequestAsync("me/Activities/",
                string.Format("startTime={0}&endTime={1}&activityTypes={2}",
                DateTime.Now.AddYears(-1).ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"),
                DateTime.Now.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"),
                activity));

            await Task.Run(() =>
            {
                // Format the JSON string
                var obj = JsonConvert.DeserializeObject(res);
                res = JsonConvert.SerializeObject(obj, Formatting.Indented);
            });

            return res;
        }

        private async void SleepActivityClick(object sender, RoutedEventArgs e)
        {
            TextDisplay.Text = await GetActivity("Sleep");
        }

        private async void FreePlayActivityClick(object sender, RoutedEventArgs e)
        {
            TextDisplay.Text = await GetActivity("FreePlay");
        }

        private async void GuidedWorkoutActivityClick(object sender, RoutedEventArgs e)
        {
            TextDisplay.Text = await GetActivity("GuidedWorkout");
        }

        private async void BikeActivityClick(object sender, RoutedEventArgs e)
        {
            TextDisplay.Text = await GetActivity("Bike");
        }

        private async void GolfActivityClick(object sender, RoutedEventArgs e)
        {
            TextDisplay.Text = await GetActivity("Golf");
        }

        private async void RunActivityClick(object sender, RoutedEventArgs e)
        {
            TextDisplay.Text = await GetActivity("Run");
        }

        private void ClientSecretChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ClientIdChanged(object sender, TextChangedEventArgs e)
        {

        }*/
    }
}
