using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    
    public class ResCalculation
    {
        public Int32 Id { get; set; }    
        public Int64 ProgramInstanceId { get; set; }
        public Int64 ProgramInstancePartId { get; set; }
        public Int64 IncProgrammeInstancePartTermId { get; set; }
        public Boolean IsResCalcApplicable { get; set; }
        public Boolean AttachResultClass { get; set; }
        public Int32 MstEvaluationId { get; set; }
        public Int32 ClassGradeTemplateId { get; set; }
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
        public string TemplateName { get; set; }
        public Int32 GradeScaleId { get; set; }
        public List<PartTermList> InstPartTermList { get; set; }
    }
    public class PartTermList
    {
        public Int32 Id { get; set; }
        public Int64 ProgramInstancePartId { get; set; }
        public Int64 IncProgrammeInstancePartTermId { get; set; }
        public Boolean IsResCalcApplicable { get; set; }
        public Boolean AttachResultClass { get; set; }
        public Int32 MstEvaluationId { get; set; }
        public Int32 ClassGradeTemplateId { get; set; }

    }
}