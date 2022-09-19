using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class PreVerificationVerifried
    {
        public Int64 ApplicationId { get; set; }

        public string NameAsPerMarksheet { get; set; }

        public Int64 ProgrammeInstancePartTermId { get; set; }

        public string InstancePartTermName { get; set; }

        public string FacultyName { get; set; }

        public string ProgrammeName { get; set; }

        public string BranchName { get; set; }

        public string InstituteName { get; set; }

        public string VerificationApplicant { get; set; }

        public Int32 IndexId { get; set; }

        public string Gender { get; set; }

        public string IsPhysicallyChanllenged { get; set; }

        public string ApplicationReservationName { get; set; }

        public string SocialCategoryName { get; set; }

        public string CommitteeName { get; set; }
    }
}