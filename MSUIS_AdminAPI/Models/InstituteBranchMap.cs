using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class InstituteBranchMap
    {
        public Int64 Id { get; set; }
        public Int32 InstituteId { get; set; }
        public Int32 ProgrammeId { get; set; }
        public Int32 ProgrammeBranchMapId { get; set; }
        public Int32 SpecialisationId { get; set; }
        public List<MstSpecialisation> SpeList { get; set; }
        public String IsActiveSts { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public String InstituteName { get; set; }
        public String ProgrammeName { get; set; }
        public String BranchName { get; set; }

    }

    
}