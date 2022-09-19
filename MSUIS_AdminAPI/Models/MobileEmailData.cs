using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class MobileEmailData
    {
        public Int64 ApplicationId { get; set; }

        public Int64 ProgrammeInstancePartTermId { get; set; }

        public string NameAsPerMarksheet { get; set; }

        public string EmailId { get; set; }

        public string MobileNo { get; set; }
        public string InstancePartTermName { get; set; }

        public string FacultyName { get; set; }

        public string ProgrammeName { get; set; }

        public string BranchName { get; set; }

        public string GroupName { get; set; }

        public string InstituteName { get; set; }
        public string SEmailId { get; set; }

        public string SMobileNo { get; set; }



    }

    public class MobileEmailUpdateRequest
    {
        public Int64 Id { get; set; }

        public Int64 ApplicationId { get; set; }

        public string OldMobileNo { get; set; }

        public string NewMobileNo { get; set; }

        public string OldEmailId { get; set; }

        public string NewEmailId { get; set; }

        public Boolean IsActive { get; set; }
        public Int64 CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public Boolean IsDeleted { get; set; }

        public string SEmailId { get; set; }

        public string SMobileNo { get; set; }

        public string EmailId { get; set; }

        public string MobileNo { get; set; }
    }
}