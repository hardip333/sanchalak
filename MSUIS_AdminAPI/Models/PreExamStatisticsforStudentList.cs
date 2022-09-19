using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class PreExamStatisticsforStudentList
    {
        public Int64 PRN { get; set; }

        public string FullName { get; set; }

        public Int64 ProgrammeInstancePartTermId { get; set; }

        public Int32 FacultyId { get; set; }
        public string FacultyName { get; set; }

        public string InstancePartTermName { get; set; }

        public string ProgrammeName { get; set; }


        public Int32 ProgrammePartTermId { get; set; }
        public string PartTermName { get; set; }
        public string BranchName { get; set; }
        public string InstituteName { get; set; }

        public Int32 FacultyExamMapId { get; set; }

        public Int32 ExamMasterId { get; set; }

        public Int32 ExamEventId { get; set; }

        public Int32 IndexId { get; set; }

        public string flag { get; set; }

        public string SeatNumber { get; set; }

        public Int64 ApplicationId { get; set; }

        public Int32 InstituteId { get; set; }

        public string ExamVenueName { get; set; }

        public Int32 SpecialisationId { get; set; }

        public string InwardMode { get; set; }

        public string ExamFees { get; set; }

        


    }
}