using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class WrittenTestHallTicketConfiguration
    {
        public Int64 Id { get; set; }
        public Int32 IndexId { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public Int64 ApplicationId { get; set; }
         
        public string AppId { get; set; }
        public Int32 FacultyId { get; set; }
        public Int32 AcademicYearId { get; set; }
        public Int32 ProgrammeId { get; set; }
        public Int64 EntranceTestSeatNo { get; set; }
        public string VenueName { get; set; }
        public string Address { get; set; }
        public string BlockName { get; set; }
        public DateTime? ExamDate { get; set; }
        public String ExamDateView { get; set; }
        public DateTime DtStartTime { get; set; }
        public TimeSpan StartTime { get; set; }
        public DateTime DtEndTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string StartTimeView { get; set; }
        public string EndTimeView { get; set; }
        public Int64 ApplicantUserName { get; set; }
        public string InstancePartTermName { get; set; }
        public string NameAsPerMarksheet { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string RadioALLFlag { get; set; }
        public string RadioVerifiedFlag { get; set; }

        public string EmailKeyName { get; set; }

        public Int64 IsEntranceTestSmsBy { get; set; }

        public Int64 IsEntranceTestEmailBy { get; set; }
        public bool IsEntranceTestSms { get; set; }
        public bool IsEntranceTestEmail { get; set; }





    }

    public class EntranceTestSMSSent
    {
        public Int64 Id { get; set; }
        public Int64 ApplicationId { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string Venue { get; set; }
        public bool IsEntranceTestSms { get; set; }
        public Int64 IsEntranceTestSmsBy { get; set; }
       
    }

    public class EntranceTestEmailSent
    {
        public Int64 Id { get; set; }
        public Int64 ApplicationId { get; set; }
        public bool IsEntranceTestEmail { get; set; }
        public string EmailKeyName { get; set; }
        public string StuEmailList { get; set; }
        public Int64 IsEntranceTestEmailBy { get; set; }
       

    }

    public class EntranceTestSentSMSEmail
    {

        public Int64 ProgrammeInstancePartTermId { get; set; }
        public Int64 TotalCount { get; set; }
        public Int64 SMSSuccessCount { get; set; }
        public Int64 SMSFailureCount { get; set; }
        public Int64 EmailSuccessCount { get; set; }
        public Int64 EmailFailureCount { get; set; }
        public string RadioALLFlag { get; set; }
        public string RadioVerifiedFlag { get; set; }

    }

    public class ImportExcelofVenueDetails
    {
        public Int64 ApplicationId { get; set; }      
        public String VenueName { get; set; }
        public String Address { get; set; }
        public String BlockName { get; set; }
        public Int64 EntranceTestSeatNo { get; set; }


    }
}