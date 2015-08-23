using System;

namespace PersonalHomePage.Models
{
    public class HomeModel
    {
        public HomeModel()
        {
            var startPoint = new DateTime(2010, 1, 1).ToUniversalTime();
            var currentPoint = DateTime.UtcNow;
            var totalDays = Convert.ToInt64((currentPoint - startPoint).TotalDays);
            CupOfCoffee = totalDays;
            LineOfCode = totalDays*50 + new Random().Next(1,10);
        }
        public Int64 CupOfCoffee { get; set; }
        public Int64 LineOfCode { get; set; }

        public DateTime? LastUpdateTimeUtc { get; set; }
        public int? StepsTaken { get; set; }
        public int? CaloriesBurned { get; set; }
    }
}