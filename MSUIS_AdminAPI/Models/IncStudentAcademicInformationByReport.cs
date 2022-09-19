using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class IncStudentAcademicInformationByReport
    {
        public Int64 Id { get; set; }

        public Int64 PRN { get; set; }

        public Int64 ProgrammeInstancePartTermId { get; set; }

        public Int32 ProgrammeId { get; set; }

        public Int32 AcademicYearId { get; set; }

        public Int32 FacultyId { get; set; }

        public Int32 InstituteId { get; set; }

        public string InstancePartTermName { get; set; }

        public string ProgrammeName { get; set; }
        public string AcademicYearDisplayName { get; set; }
        public string InstituteName { get; set; }

        public string FacultyName { get; set; }

        public Boolean IsDeleted { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime ApprovedOnAcademics { get; set; }

        public string ApprovedOnAcademicsView { get; set; }


    }
}