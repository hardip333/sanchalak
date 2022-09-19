using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class AdmStudentAddOnInformation
    {
        public Int32 Id { get; set; }
        public Int64 ApplicationId { get; set; }
        public Int32 ProgrammeAddOnCriteriaId { get; set; }
        public string AddOnValue { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int32 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public string TitleType { get; set; }
        public string TitleName { get; set; }
        public string IsVerified { get; set; }
    }
}