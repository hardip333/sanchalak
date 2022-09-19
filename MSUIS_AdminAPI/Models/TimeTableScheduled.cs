using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    #region ExamEventList
    public class ExamEventList
    {
        public Int32 Id { get; set; }
        public Int32 AcademicYearId { get; set; }
        public string DisplayName { get; set; }
    }
    #endregion

    #region AcademicYear
    public class AcademicYearList
    {
        public int Id { get; set; }
        public string AcademicYearCode { get; set; }
    
    }
    #endregion

    #region TimeTableScheduled
    public class TimeTableScheduled
    {
        public Int32 Id { get; set; } 
        public Int32 AcademicYearId { get; set; }
        public Int32 ExamEventId { get; set; }
        public string PaperName { get; set; }
        public string PaperCode { get; set; }
        public string ExamDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string SlotName { get; set; }
        public string Start_Time_HR { get; set; }
        public string Start_Time_MIN { get; set; }
        public string Start_AM_PM { get; set; }
        public string End_Time_HR { get; set; }
        public string End_Time_MIN { get; set; }
        public string End_AM_PM { get; set; }
        public int IndexId { get; set; }
    }
    #endregion
}