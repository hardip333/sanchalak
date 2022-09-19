using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class PaperSelected
    {

        public Int64 Id { get; set; }

        public Int64 PRN { get; set; }

        public Int64 ProgrammeInstancePartTermId { get; set; }

        public Int64 StudentAcademicInformationId { get; set; }

        public string InstancePartTermName { get; set; }

        public string ProgrammeName { get; set; }

        public string FacultyName { get; set; }
        public string BranchName { get; set; }

        public string InstituteName { get; set; }
        public string AcademicYearCode { get; set; }

        public string PaperName { get; set; }
        public string Exemption { get; set; }

        public string PaperStatus { get; set; }
        public string PartTermStatus { get; set; }

         public Int32 InstituteId { get; set; }

        public string PaperSelection { get; set; }
        public Int32 IndexId { get; set; }

        public string Gender { get; set; }

        public string IsPhysicallyChanllenged { get; set; }

        public string ApplicationReservationName { get; set; }

        public string SocialCategoryName { get; set; }

        public string CommitteeName { get; set; }


    }
}