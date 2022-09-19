using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class OESPaperRegPendingStatistics
    {
        public Int32 ExamEventId { get; set; }

        public string PaperCode { get; set; }

        public string PaperName { get; set; }

        public string SubjectName { get; set; }

        public DateTime Examdate { get; set; }

        public string ExamdateView { get; set; }

        public string DisplayName { get; set; }

        public Int32 IndexId { get; set; }

    }


    public class OESStudentRegPendingStatistics
    {
        public Int32 ExamEventId { get; set; }

        public string DisplayName { get; set; }

        public Int64 UserName { get; set; }

        public string StudentName { get; set; }

        public string EmailID { get; set; }

        public string MobileNo { get; set; }

        public Int32 IndexId { get; set; }

    }

    public class OESStudentPaperMapRegPending
    {
        public Int32 ExamEventId { get; set; }

        //public string DisplayName { get; set; }

        public Int64 UserName { get; set; }

        public string PaperCode { get; set; }

        public string SeatNumber { get; set; }


        public Int32 IndexId { get; set; }

    }
}