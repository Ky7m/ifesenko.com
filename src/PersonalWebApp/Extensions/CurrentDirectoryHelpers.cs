namespace PersonalWebApp.Extensions
{
    // Workaround for base dir when hosting in-process https://github.com/aspnet/Docs/pull/9873
    public static class CurrentDirectoryHelpers
    {
        private const string AspNetCoreModuleDll = "aspnetcorev2_inprocess.dll";

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern System.IntPtr GetModuleHandle(string lpModuleName);

        [System.Runtime.InteropServices.DllImport(AspNetCoreModuleDll)]
        private static extern int http_get_application_properties(ref IISConfigurationData iiConfigData);

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        private struct IISConfigurationData
        {
            public System.IntPtr pNativeApplication;

            [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.BStr)]
            public string pwzFullApplicationPath;

            [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.BStr)]
            public string pwzVirtualApplicationPath;

            public bool fWindowsAuthEnabled;
            public bool fBasicAuthEnabled;
            public bool fAnonymousAuthEnable;
        }

        public static void SetCurrentDirectory()
        {
            try
            {
                // Check if physical path was provided by ANCM
                var sitePhysicalPath = System.Environment.GetEnvironmentVariable("ASPNETCORE_IIS_PHYSICAL_PATH");
                if (string.IsNullOrEmpty(sitePhysicalPath))
                {
                    // Skip if not running ANCM InProcess
                    if (GetModuleHandle(AspNetCoreModuleDll) == System.IntPtr.Zero)
                    {
                        return;
                    }

                    IISConfigurationData configurationData = default;
                    if (http_get_application_properties(ref configurationData) != 0)
                    {
                        return;
                    }

                    sitePhysicalPath = configurationData.pwzFullApplicationPath;
                }

                System.Environment.CurrentDirectory = sitePhysicalPath;
            }
            catch
            {
                // ignore
            }
        }
    }
}