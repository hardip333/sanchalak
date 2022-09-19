using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class ProvisionallyEligibleStudentsReport
    {
        public string ApplicationId { get; set; }
        

        public Int64 PRN { get; set; }

        public Int64 ProgrammeInstancePartTermId { get; set; }

        public Int32 InstituteId { get; set; }
        public string InstancePartTermName { get; set; }

        public string FullName { get; set; }
        
        public string InstituteName { get; set; }

        public string FacultyName { get; set; }

        public string AcademicYearCode { get; set; }

        public string PRNGeneratedOn { get; set; }

        public string FacultyRemarks { get; set; }

        public string AcademicsRemarks { get; set; }

        public string EligibilityByAcademics { get; set; }

        public Boolean EligibilityByAcademics1 { get; set; }

        public Boolean IsApprovedByAcademic { get; set; }

        public Int32 IndexId { get; set; }

        public Int64 ApplicantUserName { get; set; }

        public Boolean ApplicationCheck { get; set; }

        public string ProvisionEligibleStudent { get; set; }
        public Int64 UserId { get; set; }
    }
}