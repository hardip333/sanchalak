using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class AdmittedStudent
    {
        public Int64 StudentAdmissionId { get; set; }

        public Int64 PRN { get; set; }

        public string NameAsPerMarksheet { get; set; }

        public Int64 ProgrammeInstancePartTermId { get; set; }

        public string InstancePartTermName { get; set; }

        public string AcademicYearCode { get; set; }

        public Int32 AdmissionFeeCategoryId { get; set; }

        public string FeeCategoryName { get; set; }

        public string FacultyName { get; set; }

        public string BranchName { get; set; }

        public string InstituteName { get; set; }

        public string ProgrammeName { get; set; }

        public string EligibilityByAcademics { get; set; }

        public string AdminRemarkByAcademics { get; set; }

        public string EligibilityByFaculty { get; set; }

        public string AdminRemarkByFaculty { get; set; }

        public string AdmissionStatus { get; set; }

        public Boolean IsDeleted { get; set; }

        public Int64 IndexId { get; set; }

        public string Gender { get; set; }

        public string IsPhysicallyChanllenged { get; set; }

        public string ApplicationReservationName { get; set; }

        public string SocialCategoryName { get; set; }

        public string CommitteeName { get; set; }


    }
}