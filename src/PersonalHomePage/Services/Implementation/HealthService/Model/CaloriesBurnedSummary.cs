﻿using Newtonsoft.Json;

namespace PersonalHomePage.Services.Implementation.HealthService.Model
{
    public class CaloriesBurnedSummary
    {
        [JsonProperty("period")]
        public string Period { get; set; }

        [JsonProperty("totalCalories")]
        public int TotalCalories { get; set; }
    }
}