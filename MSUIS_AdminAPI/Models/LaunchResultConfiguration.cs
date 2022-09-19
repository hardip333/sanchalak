using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    #region LaunchResultConfiguration
    public class LaunchResultConfiguration
    {
        public Int64 Id { get; set; }
        public Int64 FacultyId { get; set; }
        public Int64 AcademicYearId { get; set; }
        public String InstancePartTermName { get; set; }
        public Int32 ProgramInstanceId { get; set; }
        public Int32 ProgrammePartTermId { get; set; }

        public Boolean ResCourseEvalSystem { get; set; }
        public Boolean ResCalculation { get; set; }
        public Boolean ResExemptionMarkConfig { get; set; }
        public Boolean ResExemptionMarkConfigSts { get; set; }
        public Boolean ResCalculationSts { get; set; }
        public Boolean ResCourseEvalSystemSts { get; set; }

        public Int64 IndexId { get; set; }


    }
    #endregion

}
