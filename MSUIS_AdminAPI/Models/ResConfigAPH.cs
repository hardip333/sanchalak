using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class ResConfigAPH
    {
        public Int64 Id { get; set; }
        public Int64 IncProgrammeInstancePartTermId { get; set; }
        public Int32 FacultyId { get; set; }
        public Int32 AcademicYearId { get; set; }
        public Int32 APHId { get; set; }
        public Int64 MstPaperId { get; set; }
        public Int64 MstPaperTeachingLearningMapId { get; set; }
        public Int32 PaperMaxMarks { get; set; }       
        public bool IsLaunched { get; set; }
        public Int32 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int32 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public Int64 ProgrammeInstanceId { get; set; }
        public string InstanceName { get; set; }
        public Int64 IncProgrammeInstancePartId { get; set; }
        public string PartName { get; set; }
        public string InstancePartTermName { get; set; }
        public string PaperName { get; set; }
        public string AssessmentType { get; set; }
        public Int32 AssessmentMethodId { get; set; }
        public string TeachingLearningMethodName { get; set; }
        public string AssessmentMethodName { get; set; }
        public Int32 APHMinMarks { get; set; }
        public Int32 APHMaxMarks { get; set; }
        public Int32 MstEvaluationId { get; set; }
        public Int32 ClassGradeTemplateId { get; set; }
        public string PartShortName { get; set; }
        public bool PaperFlag { get; set; }
        public Int32 GradeScaleId { get; set; }
        public string PaperCode { get; set; }
        public List<APHConfigList> APHConfgList { get; set; }
    }
    public class APHConfigList 
    {
        public Int64 Id { get; set; }
        public Int64 IncProgrammeInstancePartTermId { get; set; }
        public Int32 APHId { get; set; }
        public Int64 MstPaperId { get; set; }
        public Int64 MstPaperTeachingLearningMapId { get; set; }
        public Int32 PaperMaxMarks { get; set; }
        public bool CheckPaper { get; set; }
    }
}