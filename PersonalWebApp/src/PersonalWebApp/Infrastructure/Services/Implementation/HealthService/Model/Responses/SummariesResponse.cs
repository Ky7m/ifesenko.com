using System.Collections.Generic;
using Newtonsoft.Json;

namespace PersonalWebApp.Infrastructure.Services.Implementation.HealthService.Model.Responses
{
    public sealed class SummariesResponse
    {
        [JsonProperty("summaries")]
        public IEnumerable<Summary> Summaries { get; set; }

        [JsonProperty("nextPage")]
        public string NextPage { get; set; }

        [JsonProperty("itemCount")]
        public int ItemCount { get; set; }
    }
}
