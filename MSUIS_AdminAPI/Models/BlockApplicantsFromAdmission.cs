using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class BlockApplicantsFromAdmission
    {
        public Int64 Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string FullName { get; set; }
        public Int64 ApplicantUserName { get; set; }
        public string FeeCategoryName { get; set; }
        public DateTime IsVerificationSmsOn { get; set; }
        public DateTime IsVerificationEmailOn { get; set; }

        public string IsVerificationSmsOns { get; set; }
        public string IsVerificationEmailOns { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public Int64 IndexId { get; set; }

        public Boolean IsBlocked { get; set; }

        public Int64 BlockedBy { get; set; }

        public DateTime BlockedOn { get; set; }

        public string BlockedRemark { get; set; }

        public string AdmissionBlocked { get; set; }

        public Int32 AdmittedInstituteId { get; set; }

        public Int32 InstituteId { get; set; }

        public Int32 AcademicYearId { get; set; }


    }
}