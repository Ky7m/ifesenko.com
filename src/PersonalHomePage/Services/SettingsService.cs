using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalHomePage.Services
{
    public sealed class SettingsService
    {
        private CloudStorageService.CloudStorageService _storageService;

        public SettingsService()
        {
            _storageService = new CloudStorageService.CloudStorageService();
        }


    }
}
