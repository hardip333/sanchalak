using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class ApplicantAcademicVerificationPendingReport
    {
        public int IndexId { get; set; }
        public Int64 Id { get; set; }
        public Int64 FacultyId { get; set; }
        public Int64 AcademicYearId { get; set; }
        public string NameAsPerMarksheet { get; set; }
        public string InstancePartTermName { get; set; }
        public string EligibilityByAcademics { get; set; }
        public string AdminRemarkByAcademics { get; set; }
        public string ApprovedOnAcademics { get; set; }

    }
}