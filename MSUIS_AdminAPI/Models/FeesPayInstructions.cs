using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class FeesPayInstructions
    {
        public Int32 Id { get; set; }
        public string Amount { get; set; }
        public string FeeTypeName { get; set; }
        public string PartTermName { get; set; }
        public string PartName { get; set; }
        public string ProgrammeName { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public Int64 AdmissionApplicationId { get; set; }


    }
}