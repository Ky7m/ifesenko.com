using System.Collections.Generic;

namespace PersonalHomePage.Services
{
    public interface ISettingsService
    {
        Dictionary<string, string> RetrieveAllSettingsValuesForService(string serviceName);

        void ReplaceSettingValueForService(string serviceName, string settingName, string settingValue);

    }
}
