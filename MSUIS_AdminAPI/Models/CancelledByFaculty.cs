using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class CancelledByFaculty
    {
        public string FacultyName { get; set; }

        public string InstancePartTermName { get; set; }

        public string ApplicationId { get; set; }

        public string FullName { get; set; }

        public string EmailId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string MobileNo { get; set; }

        public string InstituteId { get; set; }
        public string InstituteName { get; set; }

        public string GroupAllocated { get; set; }

        public string FeeCategoryName { get; set; }

        public string FeesPaidStatus { get; set; }

        public string InstallmentSelected { get; set; }

       

        public decimal TotalAmount { get; set; }

        public decimal AmountPaid { get; set; }

        public Int64 ProgrammeInstancePartTermId { get; set; }

        public string EligibilityStatus { get; set; }

        public string IsVerificationEmail { get; set; }

        public string IsVerificationSms { get; set; }

        public DateTime IsVerificationEmailOn { get; set; }

        public DateTime IsVerificationSmsOn { get; set; }

        public string PreferenceGroupId { get; set; }

        public string GroupName { get; set; }

        public string IsAdmissionFeePaid { get; set; }

        public string IsInstalmentSelected { get; set; }

        public Int32 IndexId { get; set; }
        public string IsVerificationEmailOns { get; set; }

        public string IsVerificationSmsOns { get; set; }

        public Boolean IsCancelledByFaculty { get; set; }

        public string CanclledByFaculty { get; set; }

        public string FacultyCancelledOns { get; set; }

        public string FacultyCancelledRemark { get; set; }

        public string Gender { get; set; }

        public string IsPhysicallyChanllenged { get; set; }

        public string ApplicationReservationName { get; set; }

        public string SocialCategoryName { get; set; }

        public string CommitteeName { get; set; }
    }
}