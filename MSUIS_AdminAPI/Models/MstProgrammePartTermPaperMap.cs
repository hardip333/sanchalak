using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class MstProgrammePartTermPaperMap
    {
        public Int32 PartTermId { get; set; }
        public Int32 PartId { get; set; }
        public Int64 MstProgrammePartTermId { get; set; }

            public Int64 Id { get; set; }
            public Int64 ProgrammeInstancePartTermId { get; set; }
          
            public string PartTermName { get; set; }
            public int SubjectId { get; set; }
            public string SubjectName { get; set; }
            public string EvaluationName { get; set; }
            public int EvaluationId { get; set; }
            public Int64 PaperId { get; set; }
            public string PaperName { get; set; }
            public Int64 GroupId { get; set; }
            public string GroupName { get; set; }
            public string PaperCode { get; set; }
            public int FacultyId { get; set; }
            public string FacultyName { get; set; }
            public int ProgrammeId { get; set; }
            public string ProgrammeName { get; set; }
            public int SpecialisationId { get; set; }
            public string BranchName { get; set; }
            public Int64 ProgrammeInstancePartId { get; set; }
            public string PartName { get; set; }
            public int ProgrammePartTermId { get; set; }
            public Int64 ProgrammeInstanceId { get; set; }
            public int AcademicYearId { get; set; }
            public string AcademicYearCode { get; set; }
            public Boolean IsActive { get; set; }
            public string IsActiveSts { get; set; }
            public string IsCreditSts { get; set; }
            public string IsSeparatePassingHeadSts { get; set; }
            public Decimal Credits { get; set; }
            public int MaxMarks { get; set; }
            public int MinMarks { get; set; }
            public int NoOfLecturesPerWeek { get; set; }
            public Int64 CreatedBy { get; set; }
            public DateTime CreatedOn { get; set; }
            public Int64 ModifiedBy { get; set; }
            public DateTime ModifiedOn { get; set; }
            public Boolean IsDeleted { get; set; }
        
       
    }
}