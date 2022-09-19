using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class AdmProgrammeAddOnCriteria
    {
        public Int32 Id { get; set; }
        public Int32 ProgrammeInstancePartTermId { get; set; }
        public string TitleName { get; set; }
        public string TitleType { get; set; }
        public Int32 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int32 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public Int32 AdmApplicationId { get; set; }
        public string AddOnValue { get; set; }

        public Int64 PostAdmApplicationId { get; set; } //Added by Mohini for Verification
    }
}