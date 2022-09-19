using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class RefundRequestByFaculty
    {
        public Int64 Id { get; set; }

        public string ApplicationId { get; set; }

        public Int64 ProgrammeInstancePartTermId { get; set; }

        public Boolean IsApprovedByAcademic { get; set; }

        public string InstancePartTermName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public Int64 IndexId { get; set; }

        public string FacultyName { get; set; }

        public string ProgrammeName { get; set; }

        public string BranchName { get; set; }

    }
}