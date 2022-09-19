using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class CopyCourseProgrammeDefination
    {

        public Int32 FacultyId { get; set; }

       
        public string FacultyName { get; set; }

        public Int32 ProgrammeId { get; set; }

        public string ProgrammeName { get; set; }

        public Int32 SpecialisationId { get; set; }

        public string BranchName { get; set; }

        public Int32 ProgrammePartTermId { get; set; }

        public string PartTermName { get; set; }

        public Int32 SourceAcademicYearId { get; set; }

        public string SourceAcademicYearCode { get; set; }

        public string DestinationAcademicYearCode { get; set; }

        public Int32 DestinationAcademicYearId { get; set; }

        public Int32 IndexId { get; set; }

        public Int64 DestinationProgrammeInstancePartTermId { get; set; }

        public string DestinationInstancePartTermName { get; set; }


    }

    public class CopyCourseDefination
    {

        public Int32 FacultyId { get; set; }


        public string FacultyName { get; set; }

        public Int32 ProgrammeId { get; set; }

        public string ProgrammeName { get; set; }

        public Int32 SpecialisationId { get; set; }

        public string BranchName { get; set; }

        public Int32 ProgrammePartTermId { get; set; }

        public string PartTermName { get; set; }

        public Int32 SourceAcademicYearId { get; set; }

        public string SourceAcademicYearCode { get; set; }

        public string DestinationAcademicYearCode { get; set; }

        public Int32 DestinationAcademicYearId { get; set; }

        public Int32 IncProgrammePartTermSource { get; set; }

        public Int32 IncProgrammePartTermDestination { get; set; }

        public Int32 IndexId { get; set; }

        public Int64 DestinationProgrammeInstancePartTermId { get; set; }

        public string DestinationInstancePartTermName { get; set; }


    }


}