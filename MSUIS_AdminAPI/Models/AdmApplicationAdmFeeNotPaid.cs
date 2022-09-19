using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class AdmApplicationAdmFeeNotPaid
    {
        public int InstituteId { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string InstancePartTermName { get; set; }
        public string BranchName { get; set; }
        public string InstituteName { get; set; }
        public string FeeCategoryName { get; set; }
        public Int64 IndexId { get; set; }

    }
}