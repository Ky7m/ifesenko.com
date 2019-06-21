using System;
using System.Runtime.InteropServices;
// ReSharper disable All

namespace PersonalWebApp.Extensions
{
    // Workaround for base dir when hosting in-process https://github.com/aspnet/Docs/pull/9873
    public static class CurrentDirectoryHelpers
    {
        private const string AspNetCoreModuleDll = "aspnetcorev2_inprocess.dll";

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport(AspNetCoreModuleDll)]
        private static extern int http_get_application_properties(ref IISConfigurationData iiConfigData);

        [StructLayout(LayoutKind.Sequential)]
        private struct IISConfigurationData
        {
            public IntPtr pNativeApplication;

            [MarshalAs(UnmanagedType.BStr)]
            public string pwzFullApplicationPath;

            [MarshalAs(UnmanagedType.BStr)]
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
                var sitePhysicalPath = Environment.GetEnvironmentVariable("ASPNETCORE_IIS_PHYSICAL_PATH");
                if (string.IsNullOrEmpty(sitePhysicalPath))
                {
                    // Skip if not running ANCM InProcess
                    if (GetModuleHandle(AspNetCoreModuleDll) == IntPtr.Zero)
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

                Environment.CurrentDirectory = sitePhysicalPath;
            }
            catch
            {
                // ignore
            }
        }
    }
}