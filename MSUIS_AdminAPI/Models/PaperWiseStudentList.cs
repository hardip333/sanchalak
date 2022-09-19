using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class PaperWiseStudentList
    {
        public Int64 PaperId { get; set; }

        public Int64 StudentPRN { get; set; }

        public Int64 StudentCount { get; set; }

        public string SeatNumber { get; set; }

        public string PaperCode { get; set; }

        public string PaperName { get; set; }

        public DateTime ? ExamDate { get; set; }

        public string ExamDateView { get; set; }

        public Int32 ExamEventId { get; set; }

        public Int32 IndexId { get; set; }
    }
}