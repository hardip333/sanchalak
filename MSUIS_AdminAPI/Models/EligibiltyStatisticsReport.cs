using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class EligibiltyStatisticsReport
    {
        public Int32 FacultyId { get; set; }

        public Int32 AcademicYearId { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public string InstancePartTermName { get; set; }

        public string FacultyName { get; set; }

        public string AcademicYearCode { get; set; }

        public int ProvisionallyEligible { get; set; }

        public int Eligible { get; set; }

        public int? Intake { get; set; }

        public Int32 IndexId { get; set; }

        public Int32 InstituteId { get; set; }

        public string InstituteName { get; set; }


    }
}