using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class AdmEligibilityCriteriaComponent
    {
        public Int32 Id { get; set; }
        public Int32 EligibleDegreeId { get; set; }
        public Int32 EligibilitySpecializationId { get; set; }
        public Nullable<int> MinimumPercentage { get; set; }
        public string GradeCGPA { get; set; }
        public string AdditionalInfo { get; set; }
        public Int32 ProgramEligibilityId { get; set; }
        public Int32 ProgramInstancePartTermId { get; set; }
        public Int32 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int32 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public string EligibleDegreeName { get; set; }
        public string EligibilitySpecializationName { get; set; }
        public string EligibilityCriteria { get; set; }
        public Int32 EligibilityGroupId { get; set; }
        public string EligibilityGroup { get; set; }
        

    }
}