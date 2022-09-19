using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class BlockAdmission
    {
        public Int64 Id { get; set; }

        public Int64 ProgrammeInstancePartTermId { get; set; }

        public Int64 ApplicationId { get; set; }

        public string BlockedRemark1 { get; set; }

        public Boolean IsBlocked { get; set; }

        public Int64 BlockedBy { get; set; }

        public DateTime BlockedOn { get; set; }

        public Boolean IsActive { get; set; }
        public String IsActiveSts { get; set; }

        public DateTime CreatedOn { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public Boolean IsDeleted { get; set; }

        public string BlockedRemark { get; set; }
    }
}