using Newtonsoft.Json;

namespace ifesenko.com.Infrastructure.Services.Implementation.HealthService.Model
{
    public struct CaloriesBurnedSummary
    {
        [JsonProperty("period")]
        public string Period { get; set; }

        [JsonProperty("totalCalories")]
        public int TotalCalories { get; set; }
    }
}