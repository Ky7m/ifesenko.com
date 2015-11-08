using Newtonsoft.Json;

namespace PersonalHomePage.Services.Implementation.HealthService.Model
{
    public struct Location
    {
        [JsonProperty("speedOverGround")]
        public int SpeedOverGround { get; set; }

        [JsonProperty("latitude")]
        public int Latitude { get; set; }

        [JsonProperty("longitude")]
        public int Longitude { get; set; }

        [JsonProperty("elevationFromMeanSeaLevel")]
        public int ElevationFromMeanSeaLevel { get; set; }

        [JsonProperty("estimatedHorizontalError")]
        public int EstimatedHorizontalError { get; set; }

        [JsonProperty("estimatedVerticalError")]
        public int EstimatedVerticalError { get; set; }
    }
}