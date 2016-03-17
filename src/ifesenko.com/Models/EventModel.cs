using System;
using System.Collections.Generic;

namespace ifesenko.com.Models
{
    public sealed class EventModel
    {
        public EventModel()
        {
            Collateral = new Dictionary<string, string>(0);
        }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public Dictionary<string,string> Collateral { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
    }
}