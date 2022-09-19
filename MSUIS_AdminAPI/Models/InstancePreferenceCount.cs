using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class InstancePreferenceCount
    {

        public Int32 Id { get; set; }

        public Int64 InstancePartTermId { get; set; }

        public Int16 MinNoOfPreference { get; set; }

        public Int32 FacultyId { get; set; }

        public string FacultyName { get; set; }

        public string InstancePartTermName { get; set; }

        public Boolean IsActive { get; set; }
        public string IsActiveSts { get; set; }
        public Boolean IsDeleted { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

    }
}