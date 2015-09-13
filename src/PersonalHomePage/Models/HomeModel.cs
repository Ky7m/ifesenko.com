using System;

namespace PersonalHomePage.Models
{
    public class HomeModel
    {
        public DateTime? LastUpdateTimeUtc { get; set; }
        public int? StepsTaken { get; set; }
        public int? CaloriesBurned { get; set; }
        public double? TotalDistanceOnFoot { get; set; }
        public string AverageHeartRate { get; set; }
        public string SleepDuration { get; set; }
        public int SleepEfficiencyPercentage { get; set; }
    }
}