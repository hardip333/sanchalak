using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    #region ResCourseEvalSystem
    public class ResCourseEvalSystem
    {
        public Int32 Id { get; set; }
        public Int32 MstPaperTeachingLearningMapId { get; set; }
        public Int32 AcademicYearId { get; set; }
        public Int64 MstPaperId { get; set; }
        public string PaperName { get; set; }
        public string AssessmentType { get; set; }
        public string TeachingLearningMethodName { get; set; }
        public string AssessmentMethodName { get; set; }
        public Int32 PaperMinMarks { get; set; }
        public Int32 PaperMaxMarks { get; set; }
        public Int32 ProgrammePartTermId { get; set; }
        public Int64 PaperTLMAMATMapId { get; set; }
        public Int32 ClassGradeTemplateId { get; set; }
        public Boolean IsLaunched { get; set; }
        public Int32 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int32 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsCheckSelect { get; set; }
        public Int32 MstEvaluationId { get; set; }
        public List<PaperList> PaperList1 { get; set; }
        public Int32 GradeScaleId { get; set; }
        public Double Weightage { get; set; }
        public Int32 PaperTotalMaxMarks { get; set; }
        public Int32 PaperTotalMinMarks { get; set; }

        public string EvaluationName { get; set; }
        public Boolean IsCheckSelectSts { get; set; }

        public Boolean IsCheckPaperSelectSts { get; set; }
        public string TemplateName { get; set; }
        



    }
    #endregion

    public class PaperList
    {
        public Int32 Id { get; set; }
        public Int32 ProgrammePartTermId { get; set; }
        public Int64 MstPaperTeachingLearningMapId { get; set; }
        public Int64 IncProgrammeInstancePartTermId { get; set; }
        public Int64 MstPaperId { get; set; }
        public Int32 MstEvaluationId { get; set; }
        public Int32 ClassGradeTemplateId { get; set; }
        public Int32 GradeScaleId { get; set; }
        public Double Weightage { get; set; }
        public String AssessmentType { get; set; }
        public string PaperName { get; set; }
        public Int32 PaperTotalMaxMarks { get; set; }
        public Int32 PaperTotalMinMarks { get; set; }
        
        public string TeachingLearningMethodName { get; set; }
        public string AssessmentMethodName { get; set; }
        public Int32 PaperMinMarks { get; set; }
        public Int32 PaperMaxMarks { get; set; }
        public string EvaluationName { get; set; }
        public Boolean IsCheckSelect { get; set; }
        public Boolean IsCheckSelectSts { get; set; }

        public Boolean IsCheckPaperSelectSts { get; set; }
        public string TemplateName { get; set; }
        public Boolean IsLaunched { get; set; }

    }

    #region GetPartTermShortName
    public class GetPartTermShortName
    {
        public Int32 ProgrammePartId { get; set; }
        public String PartTermShortName { get; set; }
        public String PartTermName { get; set; }
        
        public Int32 ProgrammePartTermId { get; set; }

    }
    #endregion



}