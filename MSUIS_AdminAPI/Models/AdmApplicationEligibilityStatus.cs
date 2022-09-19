using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class AdmApplicationEligibilityStatus
    {
        public Int64 Id { get; set; }

        public Int64 ProgrammeInstancePartTermId { get; set; }

        public Int64 ApplicantRegistrationId { get; set; }

        public Int32 FacultyId { get; set; }

        public Int32 ProgrammeId { get; set; }

        public Int32 AcademicYearId { get; set; }
        public Int32 SpecialisationId { get; set; }

        public string AcademicYearCode { get; set; }

        public string InstancePartTermName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }


        public string FacultyName { get; set; }

        public string ProgrammeName { get; set; }

        public string BranchName { get; set; }

        public string EligibilityStatus { get; set; }

        public Boolean IsDeleted { get; set; }

        public string AdminRemarkByFaculty { get; set; }

        public String MobileNo { get; set; }

        public string FacultyEmail { get; set; }

        public string EmailId { get; set; }

    }
}