using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class StudentListByVenue
    {
        public Int64 PRN { get; set; }

        public string SeatNumber { get; set; }

        public string FullName { get; set; }

        public string AppearanceType { get; set; }

        public string Gender { get; set; }

        public string ApplicationReservationName { get; set; }

        public string ExamEventName { get; set; }

        public string PartTermShortName { get; set; }

        public string ExamVenueName { get; set; }

        public string ExamVenueAddress { get; set; }

        public string StudentCount { get; set; }

        public string ScheduleCode { get; set; }

        public string PaperList { get; set; }

        public Int32 ExamMasterId { get; set; }

        
        public Int32 ProgrammeId { get; set; }

        public Int32 BranchId { get; set; }

        public Int32 ProgrammePartTermId { get; set; }
        public Int32 ExamVenueId { get; set; }

        public Int32 FacultyExamMapId { get; set; }

        public Int32 IndexId { get; set; }

        public string  ProgrammeName { get; set; }

        public string BranchName { get; set; }

        public List<PaperData> Paperlist { get; set; }
    }
    public class PaperData
    {
        public string  PaperName { get; set; }
        public string PaperCode { get; set; }
        
    }

}