namespace PersonalHomePage.Services
{
    public sealed class LiveIdCredentials
    {
        public LiveIdCredentials(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
        public string AccessToken { get; set; }

        public long ExpiresIn { get; set; }

        public string RefreshToken { get; set; }
    }
}