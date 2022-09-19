using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    #region ResExemptionConfig
    public class ResExemptionConfig
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
       
        public Boolean IsLaunched { get; set; }
        public Int32 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int32 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public Boolean IsActive { get; set; }
       
        public List<PaperListforExemtion> PaperList1 { get; set; }
        
        public Int32 PaperTotalMaxMarks { get; set; }
        public Int32 PaperTotalMinMarks { get; set; }
        //public Double MinMarksInPercent { get; set; }

        public Int32? ExemptionMarks { get; set; }
        public Double? ExemptionMarksPercent { get; set; }




    }
    #endregion

    #region PaperListforExemtion
    public class PaperListforExemtion
    {
        public Int32 Id { get; set; }
        public Int32 ProgrammePartTermId { get; set; }
        public Int64 MstPaperTeachingLearningMapId { get; set; }
        public Int64 IncProgrammeInstancePartTermId { get; set; }
        public Int64 MstPaperId { get; set; }
        
        public String AssessmentType { get; set; }
        public string PaperName { get; set; }
        public Int32 PaperTotalMaxMarks { get; set; }
        public Int32 PaperTotalMinMarks { get; set; }

        public string TeachingLearningMethodName { get; set; }
        public string AssessmentMethodName { get; set; }
        public Int32 PaperMinMarks { get; set; }
        public Int32 PaperMaxMarks { get; set; }
        //public Double MinMarksInPercent { get; set; }

        public Int32 ? ExemptionMarks { get; set; }
        public Double ? ExemptionMarksPercent { get; set; }
        public Boolean IsLaunched { get; set; }




    }
    #endregion

    #region GetPartTermShortName
    public class GetPartTermShortNameforExemtion
    {
        public Int64 ProgrammeInstancePartId { get; set; }
        public String PartTermShortName { get; set; }
        public Int64 Id { get; set; }
        public Int32 ProgrammePartTermId { get; set; }

    }
    #endregion



}