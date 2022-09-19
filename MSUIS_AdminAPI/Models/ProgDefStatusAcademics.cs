using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class ProgDefStatusAcademics
    {
        public Int64 Id { get; set; }

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

        public string CompletedStatus { get; set; }

        public string IsCompleted { get; set; }

        
    }
}