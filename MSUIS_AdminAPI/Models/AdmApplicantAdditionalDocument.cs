using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class AdmApplicantAdditionalDocument
    {     
            public Int32 Id { get; set; }
            public Int64 AdmApplicationId { get; set; }      
            public Int64 ProgrammeInstancePartTermId { get; set; }
            public string DocName { get; set; }
            public string DocFileName { get; set; }
            public string IsVerified { get; set; }
            public int CreatedBy { get; set; }
      
    }
    public class AdmApplicantAdditionalDocDelete
    {
        public Dictionary<String, String> ChkAdditionalId { get; set; }
    }
}