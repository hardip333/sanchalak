using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{


    #region GetPaperListbyPartTermId
    public class GetPaperListbyPartTermId
    {
        public Int64 FailPaperId { get; set; }
        public String PaperName { get; set; }
        public Int32 ProgrammePartTermId { get; set; }
        public Int32 SpecialisationId { get; set; }

    }
    #endregion

    #region TLMAMATbyPaperId
    public class TLMAMATbyPaperId
    {
        public Int64 FailPaperId { get; set; }
        public Int32 FPMstPaperTeachingLearningMapId { get; set; }
        public Boolean TLMAMATChecked { get; set; }
        public String TLMAMATName { get; set; }
        public Int32 ProgrammePartTermId { get; set; }

    }
    #endregion

    #region SpecialisationbyProgrammeInstanceId
    public class SpecialisationGetbyProgrammeInstanceId
    {
        public Int32 SpecialisationId { get; set; }
        public String BranchName { get; set; }
        public Int64 ProgrammeInstanceId { get; set; }



    }
    #endregion

    #region GetPaperListbyPartTermIdandTLMAMATId
    public class GetPaperListbyPartTermIdandTLMAMATId
    {
        public Int64 DonotExemptPaperId { get; set; }
        public String PaperandAT { get; set; }
        public Int32 ProgrammePartTermId { get; set; }
        public Int32 DExPMstPaperTeachingLearningMapId { get; set; }
        public Int64 FailPaperId { get; set; }
        public Boolean DoNotExemptPaperChecked { get; set; }
        public Int32 SpecialisationId { get; set; }




    }
    #endregion

    #region ResExemptionDependency
    public class ResExemptionDependency
    {
        public Int32 Id { get; set; }
        public Int32 ProgrammePartTermId { get; set; }
        public Int32 FailPaperId { get; set; }
        public Int32 FPMstPaperTeachingLearningMapId { get; set; }
        public Int32 DonotExemptPaperId { get; set; }
        public Int32 DExPMstPaperTeachingLearningMapId { get; set; }
        public Boolean IsLaunched { get; set; }
        public Boolean TLMAMATChecked { get; set; }
        public Boolean DoNotExemptPaperChecked { get; set; }
        public Int32 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int32 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public Boolean IsActive { get; set; }

        public List<PaperListforExemptionDependency> PaperList1 { get; set; }


    }
    #endregion


    #region PaperListforExemptionDependency
    public class PaperListforExemptionDependency
    {
        public Int32 Id { get; set; }
        public Int32 ProgrammePartTermId { get; set; }
        public Int32 AcademicYearId { get; set; }
        public Int32 FailPaperId { get; set; }
        public Int32 FPMstPaperTeachingLearningMapId { get; set; }
        public Int32 DonotExemptPaperId { get; set; }
        public Int32 DExPMstPaperTeachingLearningMapId { get; set; }
        public Boolean TLMAMATChecked { get; set; }
        public Boolean DoNotExemptPaperChecked { get; set; }


    }
    #endregion





}