using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class StudentMarksheetModel
{
    public Int64 PRN { get; set; }
    public string SeatNumber { get; set; }
    public StudentConsolidatedMarksheetModel ObjStudConsolidatedResult { get; set; }
    public StudentConsolidatedMarksheetModel ObjStudConsolidatedResultPrevSem { get; set; }
    public List<StudentResultDataMarksheetModel> ObjLstPaperResultModel { get; set; }
    public List<StudentMarkGradeEntryMarksheetModel> ObjLstResMarkGradeEntry { get; set; }
}
public class MarksheetModel
{
    public List<StudentMarksheetModel> GetObjLstStudentMarksheet { get; set; }
    public List<PaperResultStatusModel> ObjLstPaperResultStatusModel { get; set; }
}

public class StudentConsolidatedMarksheetModel
{
    public int ObjLstResMarkGradeEntryCount { get; set; }
    public int Id { get; set; }
    public Int64 StudentPRN { get; set; }
    public string SeatNumber { get; set; }
    public string StudentName { get; set; }
    public string MotherName { get; set; }

    public Int64 ExamEventId { get; set; }
    public string ExamEventName { get; set; }
    public int FacultyId { get; set; }
    public string FacultyName { get; set; }
    public Int64 InstituteId { get; set; }
    public string InstituteName { get; set; }
    public int ProgrammeId { get; set; }
    public string ProgrammeName { get; set; }
    public Int64 SpecialisationId { get; set; }
    public string SpecialisationName { get; set; }
    public int ProgrammePartId { get; set; }
    public string ProgrammePartName { get; set; }
    public Int64 ProgrammePartTermId { get; set; }
    public string ProgrammePartTermName { get; set; }
    public string ProgrammePartTermShortName { get; set; }
    public int MarksObtained { get; set; }
    public string MarkstoWords { get; set; }
    public int MarksObtainedOutof { get; set; }
    public decimal Percentage { get; set; }
    public string Grade { get; set; }
    public decimal TotalCredit { get; set; }
    public decimal EarnedCredit { get; set; }
    public decimal EGP { get; set; }
    public decimal SGPA { get; set; }
    public decimal CGPA { get; set; }
    public string ResultStatus { get; set; }

    public string ClassRemark { get; set; }
    public string Ordinance { get; set; }
    public string AdditionalRemark { get; set; }

    //public DateTime? ResultDeclaredDate { get; set; }
    public string StudentAppType { get; set; }
    public string EligibilityStatus { get; set; }
    public Int64 StatementNo { get; set; }
    public int PaperCount { get; set; }
    public string HeldBackStatus { get; set; }
    public string HeldBackRemark { get; set; }
    public string PartStatus { get; set; }
    public bool IsPublished { get; set; }

    // For Part - By CB & Raj on 02 Aug 2022
    public bool IsPart { get; set; }
    public string PartGrade { get; set; }
    public int PartMarksObtained { get; set; }
    public int PartMarksObtainedOutof { get; set; }
    public decimal PartPercentage { get; set; }
}
public class StudentResultDataMarksheetModel
{
    public int Id { get; set; }
    public Int64 ExamEventId { get; set; }
    public Int64 SpecialisationId { get; set; }
    public Int64 ProgrammePartTermId { get; set; }
    public int AdecId { get; set; }
    public Int64 PaperId { get; set; }
    public string PaperCode { get; set; }
    public string PaperName { get; set; }
    public int EvaluationId { get; set; }
    public string EvaluationName { get; set; }
    public int? ClassGradeTemplateId { get; set; }
    public string TemplateName { get; set; }
    public Int64 StudentPRN { get; set; }
    public string SeatNumber { get; set; }

    //public int? APHId { get; set; }
    //public string APHCode { get; set; }
    //public string APHName { get; set; }
    //public int? APHMinMarks { get; set; }
    //public int? APHMaxMarks { get; set; }
    //public string APHStatus { get; set; }

    public int? IAMarks { get; set; }
    public string IAGrade { get; set; }
    public int? UAMarks { get; set; }
    public string UAGrade { get; set; }

    public int? InitialMarks { get; set; }
    public string InitialGrade { get; set; }
    public bool C1Applied { get; set; }
    public decimal C1AppliedMarks { get; set; }
    public bool C2Applied { get; set; }
    public decimal C2AppliedMarks { get; set; }
    public decimal? ChairPersonGrace { get; set; }
    public int? EarningMarks { get; set; }
    public int? AdhocMarks { get; set; }
    public int? AdhocPreMarks { get; set; }
    public string AdhocPreOrdinance { get; set; }
    public int? FinalMarks { get; set; }
    public string FinalGrade { get; set; }

    public string PaperStatus { get; set; }
    public decimal GradePoints { get; set; }
    public decimal Credit { get; set; }
    public decimal EGP { get; set; }
    public string Remark { get; set; }
    public string RemarkEvent { get; set; }

    public bool IsMarkGradeCorrected { get; set; }
    public bool IsUFM { get; set; }
    public bool IsAbsent { get; set; }
    public bool IsMissingMarks { get; set; }
    public string Annulment { get; set; }

    public string ProcessedRemarks { get; set; }
    public int? MinMarks { get; set; }
    public int? MaxMarks { get; set; }
    public int? IAMinMarks { get; set; }
    public int? IAMaxMarks { get; set; }
    public int? UAMinMarks { get; set; }
    public int? UAMaxMarks { get; set; }
    public string AssessmentMethodCode { get; set; }
}
public class StudentMarkGradeEntryMarksheetModel
{
    public int Id { get; set; }
    public Int64 ProgrammePartTermId { get; set; }
    public Int64 PaperId { get; set; }
    public Int64 PaperTLMAMATMapId { get; set; }
    public Int64 ExamEventId { get; set; }
    public Int64 SpecialisationId { get; set; }
    public int? InstituteId { get; set; }
    public int AdecId { get; set; }
    //public int? BundleNumber { get; set; }
    public string BundleNumber { get; set; }
    public int? SectionNumber { get; set; }
    public Int64? StudentPRN { get; set; }
    public string SeatNumber { get; set; }
    public string PrimaryBarcode { get; set; }
    public string SecondaryBarcode { get; set; }
    public string MarksObtained { get; set; }
    public string GradeObtained { get; set; }
    public bool IsAbsent { get; set; }
    public bool IsUFM { get; set; }
    public bool IsMissingMarks { get; set; }
    public string Remark { get; set; }
    public string AssessmentType { get; set; }
    public string AssessmentMethodCode { get; set; }
    public int? MinMarks { get; set; }
    public int? MaxMarks { get; set; }
    public string ResultRemark { get; set; }
}

public class PaperResultStatusModel
{
    public Int64 PaperId { get; set; }
    public string PaperCode { get; set; }
    public string PaperName { get; set; }
    public int EvaluationId { get; set; }
    public string EvaluationName { get; set; }
    public int? ClassGradeTemplateId { get; set; }
    public string TemplateName { get; set; }
    public bool IsDataExported { get; set; }
    public string ExportRemark { get; set; }
    public bool IsBasicResultDone { get; set; }
    public int StudCountPaper { get; set; }
    public int StudCountResult { get; set; }
    public int MaxMarks { get; set; }
    public int MinMarks { get; set; }
    public decimal Credits { get; set; }

    // Added By Raj on 06 Apr 2022.
    public bool C1Applied { get; set; }
    public decimal C1AppliedMarks { get; set; }
    public bool C2Applied { get; set; }
    public decimal C2AppliedMarks { get; set; }
    public decimal? ChairPersonGrace { get; set; }
    public bool? IsEarningDone { get; set; }
    public int? AdhocPaperCount { get; set; }
    public int? AdhocGracePass { get; set; }
    public int? AdhocGraceClass { get; set; }
    public int? AdhocGraceATKT { get; set; }

    public bool IsOnlyUA { get; set; }

    // Added By Raj on 14 Apr 2022.
    public List<PaperTLMAMATModel> ObjLstPaperTLMAMATModel { get; set; }
}

public class PaperTLMAMATModel
{
    public Int64 PaperId { get; set; }
    public Int64 PaperTLMAMATMapId { get; set; }
    public string AssessmentType { get; set; }
    public int TeachingLearningMethodId { get; set; }
    public int AssessmentMethodId { get; set; }
    public string TeachingLearningMethodName { get; set; }
    public string AssessmentMethodName { get; set; }
    public string AssessmentMethodCode { get; set; }
    public int AssessmentTypeMaxMarks { get; set; }
    public int AssessmentTypeMinMarks { get; set; }
    public bool IsDataExported { get; set; }
}
