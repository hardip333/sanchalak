using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class AdmSpecializationMaster
    {
        public Int64 Id { get; set; }

        public string SpecializationCode { get; set; }

        public string SpecializationName { get; set; }

        public Int64 EligibleDegreeId { get; set; }

        public string EligibleDegreeName { get; set; }

        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }



    }

}