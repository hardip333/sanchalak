using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class AdmApplicationCancelForm
    {
        public Int64 Id { get; set; }
        public String ApplicationId { get; set; }
        public Int64 ApplicantUserName { get; set; }
        public Int64 InstituteId { get; set; }
        public String InstancePartTermName { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String FullName { get; set; }
        public String EmailId { get; set; }
        public String MobileNo { get; set; }
        public String ApplicantName { get; set; }
        public Boolean StatusUpdated { get; set; }
        public String Reason { get; set; }
        public String HandwrittenDocument { get; set; }
        public String PassbookDoc { get; set; }
        public String AuthorityLetter { get; set; }
        public String RTGSForm { get; set; }
        public String FacultyRemark { get; set; }
        public String ApplicationStatus { get; set; }
        public String AllotedInstitute { get; set; }
        public String IsAdmissionFeePaid { get; set; }
        public String RemarkStatus { get; set; }
        public int IndexId { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsDeleted { get; set; }
        public ABC obj { get; set; }
    }

    public class ABC
    {
        public Int64 Id { get; set; }

        public String name { get; set; }



    }
}


    
