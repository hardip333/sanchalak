using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class MstPreferenceCodeMap
    {
        public Int64 Id { get; set; }
        
        public int PreferenceId { get; set; }
        
        public Int64 DestinationIncProgInstPartTermId { get; set; }
        public string GroupName { get; set; }
        
        public int CodeId { get; set; }
        
        public string GroupCode { get; set; }

        public Int64 ProgInstPartTermId { get; set; }
        
        public string InstancePartTermName { get; set; }
        
        public Boolean IsActive { get; set; }
        public Boolean IsDeleted { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public string IsActiveSts { get; set; }

        public string StudentGroupPreference { get; set; }

        public Int64 IndexId { get; set; }

        public Int32 FacultyId { get; set; }
        public Int32 AcademicYearId { get; set; }
    }
}