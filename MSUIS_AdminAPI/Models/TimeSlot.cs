using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class TimeSlot
    {
        public Int64 Code { get; set; }
        public TimeSpan StartTime { get; set; } 
        public TimeSpan EndTime { get; set; }
        public DateTime DtStartTime { get; set; }
        public DateTime DtEndTime { get; set; }
        public string StartTimeView { get; set; }
        public string EndTimeView { get; set; }
        public string TimeSlotName { get; set; }
    }
}