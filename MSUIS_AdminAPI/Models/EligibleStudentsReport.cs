using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class EligibleStudentsReport
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

        public string EligibilityByAcademics { get; set; }

        public string PRNGeneratedOn { get; set; }

        public string FacultyRemarks { get; set; }

        public string AcademicsRemarks { get; set; }

        public string ApprovedOnAcademics { get; set; }

        public Int32 IndexId { get; set; }
    }
}