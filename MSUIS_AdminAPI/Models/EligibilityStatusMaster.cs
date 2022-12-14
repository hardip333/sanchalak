using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class EligibilityStatusMaster
    {
        public Int32 Id { get; set; }

        public String EligibilityStatus { get; set; }

        public Boolean IsActive { get; set; }
        public string IsActiveSts { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}