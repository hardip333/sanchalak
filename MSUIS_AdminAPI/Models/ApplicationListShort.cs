using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class ApplicationListShort
    {
        public Int32 InstancePartTermId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String EmailId { get; set; }
        public String MobileNo { get; set; }
        public String IsEWS { get; set; }
        public String IsPhysicallyChallenged { get; set; }
        public String SocialCategoryName { get; set; }
        public String ReservationCategory { get; set; }
        public String IsLocalToVadodara { get; set; }
        public String DisabilityPercentage { get; set; }
        public String DisabilityType { get; set; }
        public string InstancePartTermName { get; set; }
        public String AdmApplicationIdStr { get; set; }
        public String VerifiedStatus { get; set; }
        public String IsAdmitted { get; set; }
        public String AdmittedOn { get; set; }
        public String AdmittedInstituteName { get; set; }
        public String AdminRemarkByFaculty { get; set; }
        public String AdminRemarkByAcademics { get; set; }
        public Int64 IndexId { get; set; }
        public String Name { get; set; }

        public String IsCancelledByStudent { get; set; }
    }
}