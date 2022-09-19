using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class ProgrammeWisePaperTLMDetails
    {
        public Int32 FacultyId { get; set; }
        public Int32 InstituteId { get; set; }
        public Int32 AcademicYearId { get; set; }
        public Int32 IndexId { get; set; }
        public string InstituteName { get; set; }
        public string AcademicYearCode { get; set; }
        public string FacultyName { get; set; }
        public string ProgrammeName { get; set; }
        public string PartName { get; set; }
        public string PartTermName { get; set; }
        public string BranchName { get; set; }
        public string InstancePartTermName { get; set; }
        public string PaperCode { get; set; }
        public string PaperName { get; set; }
        public string PaperCredit { get; set; }
        public string MaxMarks { get; set; }
        public string MinMarks { get; set; }
        public string TLM { get; set; }
        public string AM { get; set; }
        public string AssessmentMethodMarks { get; set; }
        public string FollowCreditSystem { get; set; }
        public string AssessmentType { get; set; }
        public string AssessmentTypeMaxMarks { get; set; }
        public string AssessmentTypeMinMarks { get; set; }
      
                      
    }
}