using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class AddonDetailsReport
    {
        public Int64 Id { get; set; }

        public Int64 ApplicationId { get; set; }

        public Int64 ProgrammeAddOnCriteriaId { get; set; }

        public Int64 ProgrammeInstancePartTermId { get; set; }

        public Int64 ApplicantRegistrationId { get; set; }

        public string TitleName { get; set; }

        public string AddOnValue { get; set; }

        public string FacultyName { get; set; }

        public string ProgrammeName { get; set; }

        public string InstancePartTermName { get; set; }
        public string BranchName { get; set; }

        public Boolean IsDeleted { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public Int64 ApprovedByFaculty { get; set; }

        public DateTime ApprovedOnFaculty { get; set; }

        public string ApprovedOnFacultyIs { get; set; }
    }
}