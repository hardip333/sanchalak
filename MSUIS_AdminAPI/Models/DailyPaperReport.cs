using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class DailyPaperReport
    {

        public string CenterCode { get; set; }
        public Int32 FacultyId { get; set; }

        public Int32 ExamEventId { get; set; }

        public string FacultyName { get; set; }

        public string ExamEventName { get; set; }

        public string ExamCenter { get; set; }
        public string ExamCode { get; set; }


        public string ExamVenueName { get; set; }
        //public Int32 ProgrammePartTermId { get; set; }
        public string Course { get; set; }
        public string ExamDate { get; set; }
        public string ExamDate2 { get; set; }

        public string PaperStartTime { get; set; }

        public string PaperStartTimeView { get; set; }

        public string PaperCode { get; set; }

        public string PaperName { get; set; }
        public string StudentCount { get; set; }

        public Int32 IndexId { get; set; }


    }
}