using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class PaperSelectionReport
    {
        public string PRN { get; set; }

        public string NameAsPerMarksheet { get; set; }

        public string ProgrammeInstancePartTermId { get; set; }

        public string ProgrammePartTermId { get; set; }

       
        public string InstituteId { get; set; }
        public string AcademicYearId { get; set; }
        public string PartTermName { get; set; }

        public string PaperId { get; set; }


        public string PaperName { get; set; }

        public string PaperCode { get; set; }

        public string BranchName { get; set; }

        public string FacultyName { get; set; }

        public string ProgrammeName { get; set; }

        public string InstituteName { get; set; }

        public string AcademicYearCode { get; set; }

        public Int32 IndexId { get; set; }

        public Boolean StudentWise1 { get; set; }

        public Boolean PaperWise1 { get; set; }

        public string StudentWise { get; set; }

        public string PaperWise { get; set; }


    }

    public class ProgrammePartTermGetByInstIdAcaId
    {
        public Int32 Id { get; set; }

        public string PartTermName { get; set; }

        public Int32 ProgrammePartTermId { get; set; }

        public Int32 AcademicYearId { get; set; }

        public Int32 InstituteId { get; set; }

        public string AcademicYearCode { get; set; }

        public string InstituteName { get; set; }

        //public Boolean StudentWise1 { get; set; }

        public Boolean PaperWise1 { get; set; }

       // public string StudentWise { get; set; }

        public string PaperWise { get; set; }

        public Int64 PRN { get; set; }

        public Int64 PaperId { get; set; }



    }

    public class ProgPartTermGetByInstIdAcaId
    {
        public Int32 Id { get; set; }

        public string PartTermName { get; set; }

        public Int32 ProgrammePartTermId { get; set; }

        public Int32 AcademicYearId { get; set; }

        public Int32 InstituteId { get; set; }

        public string AcademicYearCode { get; set; }

        public string InstituteName { get; set; }

        public Boolean StudentWise1 { get; set; }

        //public Boolean PaperWise1 { get; set; }

        public string StudentWise { get; set; }

        //public string PaperWise { get; set; }

        public Int64 PRN { get; set; }

        public Int64 PaperId { get; set; }






    }

    public class PaperSelectionReportExport
    {
        public string PRN { get; set; }

        public string NameAsPerMarksheet { get; set; }

        public string ProgrammeInstancePartTermId { get; set; }

        public string ProgrammePartTermId { get; set; }


        public string InstituteId { get; set; }
        public string AcademicYearId { get; set; }
        public string PartTermName { get; set; }

        public string PaperId { get; set; }


        public string PaperName { get; set; }

        public string PaperCode { get; set; }

        public string BranchName { get; set; }

        public string FacultyName { get; set; }

        public string ProgrammeName { get; set; }

        public string InstituteName { get; set; }

        public string AcademicYearCode { get; set; }

        public Int32 IndexId { get; set; }

        public Boolean StudentWise1 { get; set; }

        public Boolean PaperWise1 { get; set; }

        public string StudentWise { get; set; }

        public string PaperWise { get; set; }


    }
}