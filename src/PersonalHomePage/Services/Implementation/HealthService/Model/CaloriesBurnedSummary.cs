using System;
using Newtonsoft.Json;

namespace PersonalHomePage.Services.Implementation.HealthService.Model
{
    [Serializable]
    public class CaloriesBurnedSummary
    {
        [JsonProperty("period")]
        public string Period { get; set; }

        [JsonProperty("totalCalories")]
        public int TotalCalories { get; set; }
    }
}