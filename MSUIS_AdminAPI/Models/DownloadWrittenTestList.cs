using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class DownloadWrittenTestList
    {
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public Int64 ApplicationId { get; set; }
        public Int64 ApplicantUserName { get; set; }
        public Int64 EntranceTestSeatNo { get; set; }
        public string FullName { get; set; }
        public string ApplicantPhoto { get; set; }
        public string ApplicantSignature { get; set; }
        public Int32 IndexId { get; set; }
    }

    public class WrittenTestSchedule
    {
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public string Venue { get; set; }
        public string ExamDate { get; set; }
        public string StartTimeView { get; set; }
        public string EndTimeView { get; set; }
    
    }
}