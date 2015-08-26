using System;

namespace PersonalHomePage.Models
{
    public class HomeModel
    {
        public HomeModel()
        {
            CupOfCoffee = 1;
            LineOfCode = 50 + new Random().Next(1,30);
        }
        public Int64 CupOfCoffee { get; set; }
        public Int64 LineOfCode { get; set; }

        public DateTime? LastUpdateTimeUtc { get; set; }
        public int? StepsTaken { get; set; }
        public int? CaloriesBurned { get; set; }
        public double? TotalDistanceOnFoot { get; set; }
        public string AverageHeartRate { get; set; }
    }
}