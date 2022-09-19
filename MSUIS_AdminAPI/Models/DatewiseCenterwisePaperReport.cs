using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class DatewiseCenterwisePaperReport
    {
        public Int32 FacultyId { get; set; }

        public Int32 ExamEventId { get; set; }

        public string FacultyName { get; set; }

        public string ExamEventName { get; set; }


        public string PaperCode { get; set; }

        public string PaperName { get; set; }

        public string TLMAMAT { get; set; }

        public string PartName { get; set; }
        public string MOL { get; set; }

        public string ProgrammeName { get; set; }
        public string Pattern { get; set; }

        public string BranchName { get; set; }

        public string PartTermName { get; set; }

        public string ExamVenueName { get; set; }

        public string ExamCenterName { get; set; }

        public string Date { get; set; }
        public string DateView { get; set; }


        public string Duration { get; set; }


        public Int64 StudentCount { get; set; }

        public Int32 IndexId { get; set; }


    }
}