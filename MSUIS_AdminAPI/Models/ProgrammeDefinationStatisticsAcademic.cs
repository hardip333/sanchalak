using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class ProgrammeDefinationStatisticsAcademic
    {
        public Int64 ProgrammeInstancePartTermId { get; set; }

        public string InstancePartTermName { get; set; }

        public string VerficationStatus { get; set; }

        public string LaunchStatus { get; set; }

        public string CompleteStatus { get; set; }

        public Boolean CompleteStatusFlag1 { get; set; }

        public Boolean CompleteStatusFlag2 { get; set; }

        public string FacultyName { get; set; }
        public string AcademicYearCode { get; set; }

        public Int32 InstituteId { get; set; }

        public Int32 IndexId { get; set; }

        public Int32 FacultyId { get; set; }
        public Int32 AcademicYearId { get; set; }
        public string VerficationStatusAssessment { get; set; }

        public string LaunchStatusAssessment { get; set; }

        public string CompleteStatusAssessment { get; set; }

        public Boolean CompleteStatusAssessmentFlag1 { get; set; }

        public Boolean CompleteStatusAssessmentFlag2 { get; set; }

    }
}