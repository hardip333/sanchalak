using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class ApplicationPaymentReport
    {
        public Int64 Id { get; set; }
        public Int64 ApplicantRegistrationId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string EmailId { get; set; }

        public string MobileNo { get; set; }

        public Int32 ProgrammeId { get; set; }

        public string ProgrammeName { get; set; }

        public string TransactionStatus { get; set; }

        public Boolean IsDeleted { get; set; }

        public Int64 IndexId { get; set; }

        public DateTime TransactionDate { get; set; }

        public string TransactionDates { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        //For get Institute Id - Mohini's Code
        public Int64 FacultyId { get; set; }
        public Int64 InstituteId { get; set; }
    }
}