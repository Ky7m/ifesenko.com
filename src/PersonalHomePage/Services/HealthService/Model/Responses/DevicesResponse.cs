using System.Collections.Generic;
using Newtonsoft.Json;

namespace PersonalHomePage.Services.HealthService.Model.Responses
{
    internal class DevicesResponse
    {
        [JsonProperty("deviceProfiles")]
        public IEnumerable<Device> Devices { get; set; }
    }
}
