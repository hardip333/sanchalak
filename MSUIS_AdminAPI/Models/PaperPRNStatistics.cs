using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class PaperPRNStatistics
    {
        public int FacultyId { get; set; }
        public int ProgrammeInstancePartTermId { get; set; }
        public string InstancePartTermName { get; set; }
        
        public int AdmittedStudent { get; set; }
        public int PrnGenerated { get; set; }
        public int PaperSelected { get; set; }

        public int PaperPendingSelection { get; set; }
        public int? Intake { get; set; }
        
        public int AcademicYearId { get; set; }
        public int InstituteId { get; set; }
        public string ProgrammeName { get; set; }
        public string BranchName { get; set; }
        public Int32 Id { get; set; }
        public string InstituteName { get; set; }
        public string AcademicYearCode { get; set; }
        public Int64 IndexId { get; set; }
        
    }
}