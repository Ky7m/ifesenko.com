using System.Collections.Generic;
using Newtonsoft.Json;

namespace ifesenko.com.Infrastructure.Services.Implementation.HealthService.Model.Responses
{
    internal struct DevicesResponse
    {
        [JsonProperty("deviceProfiles")]
        public IEnumerable<Device> Devices { get; set; }
    }
}
