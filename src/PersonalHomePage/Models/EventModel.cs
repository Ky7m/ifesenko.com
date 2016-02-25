using System;
using System.Collections.Generic;

namespace PersonalHomePage.Models
{
    public sealed class EventModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Dictionary<string,string> Collateral { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
    }
}