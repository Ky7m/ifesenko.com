using Newtonsoft.Json;

namespace ifesenko.com.Services.Implementation.HealthService.Model
{
    public struct HeartRateSummary
    {
        [JsonProperty("period")]
        public string Period { get; set; }

        [JsonProperty("averageHeartRate")]
        public string AverageHeartRate { get; set; }

        [JsonProperty("peakHeartRate")]
        public string PeakHeartRate { get; set; }

        [JsonProperty("lowestHeartRate")]
        public string LowestHeartRate { get; set; }
    }
}