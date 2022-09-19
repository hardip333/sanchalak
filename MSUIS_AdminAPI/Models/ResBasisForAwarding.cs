using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class ResBasisForAwarding
    {
        public Int32 Id { get; set; }
        public Int64 ProgramInstanceId { get; set; }
        public Int64 IncProgramInstancePartId { get; set; }
        public Int64 IncProgrammeInstancePartTermId { get; set; }
        public decimal? ProgramInstancePartPercent { get; set; }
        public decimal? ProgramInstancePartTermPercent { get; set; }
        public string Applicable { get; set; }
        public Int32 MstEvaluationId { get; set; }
        public Int32 ClassGradeTemplateId { get; set; }
        public string RoundingOption { get; set; }
        public decimal? AggregatePercentage { get; set; }
        public decimal? MinPassingPercentage { get; set; }
        public Boolean IsLaunched { get; set; }
        public Int32 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int32 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public string FacultyName { get; set; }
        public string InstanceName { get; set; }
        public string PartName { get; set; }
        public string PartShortName { get; set; }
        public Int32 FacultyId { get; set; }
        public string InstancePartTermName { get; set; }
        public string EvaluationName { get; set; }
        public Int32 GradeScaleId { get; set; }
        public Int32 MstProgrammeBranchMapId { get; set; }
        public string BranchName { get; set; }
        public List<PartTermListResBasis> InstPartTermListResBasis { get; set; }
    }
    public class PartTermListResBasis
    {
        public Int32 Id { get; set; }
        public Int64 IncProgramInstancePartId { get; set; }
        public Int64 IncProgrammeInstancePartTermId { get; set; }
        public decimal? ProgramInstancePartPercent { get; set; }
        public decimal? ProgramInstancePartTermPercent { get; set; }
        public decimal? AggregatePercentage { get; set; }
        public string PartName { get; set; }


    }
}
