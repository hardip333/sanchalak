using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class AdmittedApplicantList
    {
        public Int64 ApplicationId { get; set; }
        public Int64 EligibleApplicationid { get; set; }
        public String StudentName { get; set; }
        public String MobileNo { get; set; }
        public String EmailId { get; set; }
        public Int32 AcademicYearId { get; set; }
        public String AdmittedFaculty { get; set; }
        public String EligibleCourseFaculty { get; set; }
        public String AdmittedCourse { get; set; }
        public String EligibleCourse { get; set; }
        public Int64 AdmittedParttermId { get; set; }
        public Int64 EligiblePartTermId { get; set; }
        public int IndexId { get; set; }

        //public Int64 PRN { get; set; }
        //public Int32 FacultyId { get; set; }
        //public Int32 InstituteId { get; set; }
        //public Int64 ProgrammeInstancePartTermId { get; set; }
        //public String AdmittedInstitute { get; set; }
        //public Boolean ? IsPRNGenerated { get; set; }

    }
}

