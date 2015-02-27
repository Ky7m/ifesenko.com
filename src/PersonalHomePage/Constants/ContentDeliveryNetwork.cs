namespace PersonalHomePage.Constants
{
    public static class ContentDeliveryNetwork
    {
        public static class Google
        {
            private const string Domain = "//ajax.googleapis.com/";
            public const string JQueryUrl = Domain + "ajax/libs/jquery/2.1.3/jquery.min.js";
        }

        public static class MaxCdn
        {
            private const string Domain = "//maxcdn.bootstrapcdn.com/";
            public const string BootstrapJsUrl = Domain + "bootstrap/3.3.2/js/bootstrap.min.js";
            public const string BootstrapCssUrl = Domain + "bootstrap/3.3.2/css/bootstrap.min.css";
        }
    }
}