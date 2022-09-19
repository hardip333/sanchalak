using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    #region MstFeeType
    public class MstFeeType
    {
        public Int32 Id { get; set; }
        public String FeeTypeName { get; set; }
        public String FeeTypeCode { get; set; }
        public String IsActiveSts { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
    }
    #endregion

    #region MstFeeCategory
    public class MstFeeCategory
    {
        public Int32 Id { get; set; }
        public String FeeCategoryName { get; set; }
        public Int32 FeeCategoryTypeId { get; set; }
        public String FeeCategoryTypeName { get; set; }
        public String FeeCategoryCode { get; set; }
        public String CategoryPeriodName { get; set; }
        public Int32 CategoryPeriodId { get; set; }
        public String IsActiveSts { get; set; }
        public Boolean IsActive { get; set; }
        //public Boolean Checked { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public Int64 FeeCategoryPartTermId { get; set; }
        public Int32 NoOfInstalment { get; set; }
        public Boolean IsInstalmentGiven { get; set; }
        public Boolean IsVerified { get; set; }
    }
    #endregion

    #region MstFeeHead
    public class MstFeeHead
    {
        public String FeeHeadName { get; set; }
        public Int32 Id { get; set; }
        [Required]
        public List<MstFeeSubHead> FeeSubHeadList { get; set; }
        public String FeeHeadCode { get; set; }
        public String IsActiveSts { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public Boolean HasSubHead { get; set; }
        public Boolean IsEditable { get; set; }
        public Int32 FeeSubHeadSrno { get; set; }
        public Dictionary<String, String> FeeHeadList { get; set; }
        public Int32 FeeSubHeadId { get; set; }
        public String FeeSubHeadName { get; set; }
        public String FeeSubHeadCode { get; set; }


    }
    #endregion

    #region MstFeeSubHead
    public class MstFeeSubHead
    {
        public String FeeSubHeadCode { get; set; }
        public Int32 FeeHeadId { get; set; }
        public Int32 FeeSubHeadSrno { get; set; }
        public String FeeHeadList { get; set; }
        public String FeeHeadName { get; set; }
        public String FeeHeadCode { get; set; }
        public String IsActiveSts { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public Int32 Id { get; set; }
        public Boolean AllowsNegativeValue { get; set; }
        public String FeeSubHeadName { get; set; }
        public Boolean IsEditable { get; set; }
        public Boolean AddCheck { get; set; }
        //public Boolean IsInstalmentAllowed { get; set; }
        [Required]
        public List<Amount> Amount { get; set; }
    }
    #endregion

    #region FeeTypeFeeCategoryMap
    public class FeeTypeFeeCategoryMap
    {
        public Int64 Id { get; set; }
        public Int32 AcademicYearId { get; set; }
        public String AcademicYearCode { get; set; }
        public Int32 FeeTypeId { get; set; }
        public Dictionary<string, string> FeeCategoryList { get; set; }
        public String FeeTypeName { get; set; }
        public String FeeTypeCode { get; set; }
        public Int32 FeeCategoryId { get; set; }
        public String FeeCategoryName { get; set; }
        public String FeeCategoryCode { get; set; }
        public String FeeCategoryNameOriginal { get; set; }
        public String IsActiveSts { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public List<FeeTypeFeeCategoryMap> FCM { get; set; }
        public Boolean Checked { get; set; }
    }
    #endregion

    #region FeeTypeFeeHeadMap
    public class FeeTypeFeeHeadMap
    {
        public Int32 FeeSubHeadSrno { get; set; }
        public Int64 Id { get; set; }
        public Int32 AcademicYearId { get; set; }
        public String AcademicYearCode { get; set; }
        public Int32 FeeTypeId { get; set; }
        public String FeeTypeName { get; set; }
        public String FeeTypeCode { get; set; }
        public Dictionary<string, string> FeeHeadList { get; set; }
        public Int32 FeeHeadId { get; set; }
        public String FeeHeadName { get; set; }
        public String FeeHeadCode { get; set; }
        public Dictionary<string, string> FeeSubHeadList { get; set; }
        public List<MstFeeSubHead> FeeSHList { get; set; }
        public Int32 FeeSubHeadId { get; set; }
        public String FeeSubHeadName { get; set; }
        public String FeeSubHeadCode { get; set; }
        public Boolean IsEditable { get; set; }
        public Boolean AllowsNegativeValue { get; set; }
        public String IsActiveSts { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public Boolean HeadChecked { get; set; }
        public Boolean SubHeadChecked { get; set; }
    }
    #endregion

    #region FeeConfiguration
    public class FeeConfiguration
    {
        public String AcademicYearCode { get; set; }
        public String FacultyName { get; set; }
        public String FacultyCode { get; set; }
        public String FacultyAddress { get; set; }
        public Int64 ProgrammeInstanceId { get; set; }
        public Int64 FeeCategoryPartTermMapId { get; set; }
        public String ProgrammeName { get; set; }
        public String ProgrammeCode { get; set; }
        public String PartName { get; set; }
        public String ProgrammePartTermName { get; set; }
        public String FeeTypeName { get; set; }
        public String FeeTypeCode { get; set; }
        public Int32 FeeHeadId { get; set; }
        public String FeeHeadName { get; set; }
        public String FeeHeadCode { get; set; }
        public Int32 FeeSubHeadId { get; set; }
        public String FeeSubHeadName { get; set; }
        public String FeeSubHeadCode { get; set; }
        public Boolean IsEditable { get; set; }
        public Int32 FeeCategoryId { get; set; }
        public String FeeCategoryName { get; set; }
        public String FeeCategoryNameOriginal { get; set; }
        public String FeeCategoryCode { get; set; }
        public Double Amount { get; set; }
        public Int64 Id { get; set; }
        public Int64 FeeTypeFeeCategoryMapId { get; set; }
        public String IsEditableSts { get; set; }
        public Boolean IsVerified { get; set; }
        public String IsVerifiedSts { get; set; }
        public Int64 VerifiedBy { get; set; }
        public DateTime VerifiedOn { get; set; }
        public String IsActiveSts { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public Int32 AcademicYearId { get; set; }
        public Int32 FacultyId { get; set; }
        public Int32 ProgrammeLevelId { get; set; }
        public Int32 ProgrammeId { get; set; }
        public Int32 ProgrammeBranchMapId { get; set; }
        public Int64 ProgrammeInstancePartId { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public Int32 FeeTypeId { get; set; }
        public Dictionary<string, string> FeeHeadList { get; set; }
        public Dictionary<string, string> FeeCateList { get; set; }
        public Dictionary<string, string> PIPTList { get; set; }
        [Required]
        public List<MstFeeSubHead> AmountList { get; set; }
        public List<MstFeeSubHead> AmountEditList { get; set; }
        //public List<MstFeeHead> AmountList { get; set; }
        public Int32 Count { get; set; }
        public Int32 countTotal { get; set; }
        public Int32 countDelete { get; set; }
        public List<TotalValue> TotalAmountList { get; set; }
        public List<TotalValue> TotalAmountEditList { get; set; }
        public List<TotalValue> TotalAmountDeleteList { get; set; }
        public String PartTermName { get; set; }
       
    }
    #endregion

    #region FeeCategoryPartTermMap
    public class FeeCategoryPartTermMap
    {
        public Int64 Id { get; set; }
        public Int32 FeeTypeId { get; set; }
        public String FeeTypeName { get; set; }
        public String FeeTypeCode { get; set; }
        public Int32 FeeCategoryId { get; set; }
        public String FeeCategoryName { get; set; }
        public String FeeCategoryNameOriginal { get; set; }
        public String FeeCategoryCode { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public String PartTermName { get; set; }
        public String PartTermShortName { get; set; }
        public Int32 SpecialisationId { get; set; }
        public String BranchName { get; set; }
        public Int64 ProgrammeInstancePartId { get; set; }
        public String PartName { get; set; }
        public String PartShortName { get; set; }
        public Int64 ProgrammeInstanceId { get; set; }
        public Int32 ProgrammeId { get; set; }
        public String ProgrammeName { get; set; }
        public Int32 FacultyId { get; set; }
        public String FacultyName { get; set; }
        public String ProgrammeLevelName { get; set; }
        public Int32 AcademicYearId { get; set; }
        public String AcademicYearCode { get; set; }
        public Double TotalAmount { get; set; }
        public Boolean IsVerified { get; set; }
        public Boolean IsVerifiedCheck { get; set; }
        public String IsVerifiedSts { get; set; }
        public Int64 VerifiedBy { get; set; }
        public DateTime VerifiedOn { get; set; }
        public Boolean IsPublished { get; set; }
        public Boolean IsPublishedCheck { get; set; }
        public String IsPublishedSts { get; set; }
        public Int64 PublishedBy { get; set; }
        public DateTime PublishedOn { get; set; }
        public String IsActiveSts { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public String InstanceName { get; set; }
        public String InstancePartName { get; set; }
        public String InstancePartTermName { get; set; }
        public Int64 IndexId { get; set; }
    }
    #endregion

    #region TotalValue
    public class TotalValue
    {
        public Double TotalFee { get; set; }
        public Int32 Id { get; set; }
        public Int64 FeeCategoryPartTermId { get; set; }
        public Boolean IsInstalmentGiven { get; set; }
        public Int32 NoOfInstalment { get; set; }
        public Boolean FeecateDeleteCheck { get; set; }
    }
    #endregion



    #region MstApplicationFee
    public class MstApplicationFee
    {
        public Int64 Id { get; set; }
        public Int32 FeeCategoryId { get; set; }
        public Int32 FeeTypeId { get; set; }
        public Int32 AcademicYearId { get; set; }
        public String FeeCategoryName { get; set; }
        public String FeeTypeName { get; set; }
        public String AcademicYearCode { get; set; }
        public Double Amount { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
    }
    #endregion

    #region Amount
    public class Amount
    {
        public Int32 Id { get; set; }
        public Int64 FeeConfigId { get; set; }
        public String FeeCategoryName { get; set; }
        public String FeeCategoryCode { get; set; }
        public Double FeeAmount { get; set; }
        public Boolean IsPublished { get; set; }
        public Boolean IsInstalmentAllowed { get; set; }
        public Boolean InstalmentGivenCheck { get; set; }
        public Boolean IsInstalmentGiven { get; set; }
        public Boolean FeeSHDeleteCheck { get; set; }

    }
    #endregion   

    #region MstFeeCategoryType
    public class MstFeeCategoryType
    {
        public Int32 Id { get; set; }
        public String FeeCategoryTypeName { get; set; }
        public String IsActiveSts { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
    }
    #endregion

    #region AdmApplicationFeesPaid
    public class AdmApplicationFeesPaid
    {
        public Int64 Id { get; set; }
        public Int64 AdmissionApplicationId { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public Int64 FeeConfigurationId { get; set; }
        public Int32 AcademicYearId { get; set; }
        public Int32 FeeTypeId { get; set; }
        public Double FeesPaidInr { get; set; }
        public String NameAsPerMarksheet { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public Boolean IsApplicationFee { get; set; }
        public String FacultyName { get; set; }
        public String ProgrammeName { get; set; }
        public String BranchName { get; set; }
        public String PartName { get; set; }
        public String InstancePartTermName { get; set; }
        public String AcademicYearCode { get; set; }
		
		 //Steffi Code-Applicant Fee Recepit Starts
        public Int64 ApplicationRegistrationId { get; set; }
       
        public decimal Amount { get; set; }

        public string TransactionId { get; set; }

        public string TransactionDate { get; set; }

        public Int64 ApplicationNo { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }
		
		//Steffi Code-Applicant Fee Recepit Ends
    }
    #endregion
    #region AdmStudentAdmissionFeesPaid
    public class AdmStudentAdmissionFeesPaid
    {
        public Int64 Id { get; set; }
        public Int64 PRN { get; set; }
        public Int32 ProgrammeInstancePartTermId { get; set; }
        public Int64 ApplicationId { get; set; }
        public Int64 FeeCategoryPartTermMapId { get; set; }
        public Int64 FeeTypeFeeCategoryMapId { get; set; }
        public Int64 StudentAcademicInfoId { get; set; }
        public Boolean IsInstalmentSelected { get; set; }
        public Int32 TotalInstalmentGiven { get; set; }
        public Int32 TotalInstalmentSelected { get; set; }
        public Int32 RemainingInstallment { get; set; }
        public Int32 InstalmentNo { get; set; }
        public Double TotalAmount { get; set; }
        public Double AmountPaid { get; set; }
        public String TransactionStatus { get; set; }
        public String NameAsPerMarksheet { get; set; }
        public DateTime PaymentDate { get; set; }
        public Boolean IsFullyPaid { get; set; }
        public DateTime RefundDate { get; set; }
        public Int64 RefundBy { get; set; }
        public String FeeStatus { get; set; }
        public Boolean IsApplicationFee { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public String InstancePartTermName { get; set; }
        public String FacultyName { get; set; }
        public String ProgrammeName { get; set; }
        public String BranchName { get; set; }
        public String MobileNo { get; set; }
        public String EmailId { get; set; }
        public String IsInstalmentSelectedSts { get; set; }
        public String OrderId { get; set; }
        public String FeeCategoryName { get; set; }
    }
    #endregion
    #region FeeCategoryChange
    public class FeeCategoryChange
    {

        public Int64 ApplicationId { get; set; }
        public String NameAsPerMarksheet { get; set; }
        public String FacultyName { get; set; }
        public String ProgrammeName { get; set; }
        public String BranchName { get; set; }
        public String InstancePartTermName { get; set; }
        public String ApplicationStatus { get; set; }
        public String EligibilityStatus { get; set; }
        public String FeeCategoryName { get; set; }
        public Double TotalAmount { get; set; }
        public Boolean IsVerificationSms { get; set; }
        public Boolean IsVerificationEmail { get; set; }
        public Int64 FeeCategoryPartTermMapId { get; set; }

        //Mohini's Code Start
        public String EligibilityByAcademics { get; set; }
        public String GroupName { get; set; }
        public String AdmittedInstituteName { get; set; }
        public DateTime IsVerificationEmailOn { get; set; }
        public String IsVerificationEmailOnView { get; set; }

        public DateTime IsVerificationSmsOn { get; set; }
        public String IsVerificationSMSOnView { get; set; }
        public Boolean IsAdmissionFeePaid { get; set; }
        public Int32 FeeCategoryId { get; set; }
        public Boolean IsPRNGenerated { get; set; }
        //Mohini's Code End



    }
    #endregion

    #region AdmissionFeeCategoryChange

    public class AdmissionFeeCategoryChange
    {

        public Int64 ApplicationId { get; set; }
        public Int64 Id { get; set; }
        public Int64 OldFeeCategoryPartTermMapId { get; set; }
        public Int64 NewFeeCategoryPartTermMapId { get; set; }
        public Int64 FeeCategoryPartTermMapId { get; set; }
        public Int64 FeeCategoryId { get; set; }

        public Double OldFeeAmount { get; set; }
        public Double NewFeeAmount { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public string FeeCategoryChangeRemark { get; set; }
        public Boolean IsVerificationSms { get; set; }
        public Boolean IsVerificationEmail { get; set; }
        public DateTime ? IsVerificationSmsOn { get; set; }
        public DateTime ? IsVerificationEmailOn { get; set; }
        public Boolean IsAdmissionFeePaid { get; set; }
        public Boolean IsPRNGenerated { get; set; }
        public Int64 OldFeeCategoryId { get; set; }
        public Int64 NewFeeCategoryId { get; set; }
        public Boolean IsRequestCompleted { get; set; }
        public Boolean IsBOBAccount { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public Int32 ? BankId { get; set; }
        public string IFSCCode { get; set; }
        public string ShortIFSCCode { get; set; }
        public string PassbookDoc { get; set; }
        public string RTGSForm { get; set; }
        public string RequestStatus { get; set; }
    }
    #endregion
    
    #region BankNameList

    public class BankNameList
    {
        public int Id { get; set; }
        public string BankName { get; set; }
        public string BankType { get; set; }
        public string IfscInitial { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
    }

    #endregion

	
	//Bhushan Code
    #region AdmissionFeesReportByAcademic
    public class AdmissionFeesReportByAcademic
    {
        public Int64 Id { get; set; }

        public Int64 ProgrammeInstancePartTermId { get; set; }
        public Int32 FeeCategoryId { get; set; }

        public Int32 FacultyId { get; set; }
        public Int32 AcademicYearId { get; set; }
        public Boolean IsPublished { get; set; }

        public string PublishedStatus { get; set; }
        public string AdmissionFeeStatus { get; set; }
        public string InstancePartTermName { get; set; }
        public string AcademicYearCode { get; set; }
        public string FacultyName { get; set; }

        public string FeeCategoryName { get; set; }

        public Boolean IsDeleted { get; set; }

        public Int64 IndexId { get; set; }

        public decimal TotalAmount { get; set; }

        public string FeeTypeName { get; set; }

        public string BranchName { get; set; }

        public string ProgrammeName { get; set; }

        

    }

    #endregion

    #region AdmissionFeesReportByFaculty
    public class AdmissionFeesReportByFaculty
    {
        public Int64 Id { get; set; }

        public Int64 ProgrammeInstancePartTermId { get; set; }
        public Int32 FeeCategoryId { get; set; }

        public Int32 FacultyId { get; set; }
        public Int32 AcademicYearId { get; set; }
        public Boolean IsPublished { get; set; }

        public string PublishedStatus { get; set; }
        public string AdmissionFeeStatus { get; set; }
        public string InstancePartTermName { get; set; }
        public string AcademicYearCode { get; set; }
        public string FacultyName { get; set; }

        public string FeeCategoryName { get; set; }

        public Boolean IsDeleted { get; set; }

        public Int64 IndexId { get; set; }

        public decimal TotalAmount { get; set; }

        public string FeeTypeName { get; set; }

        public string BranchName { get; set; }

        public string ProgrammeName { get; set; }

        public Int32 InstituteId { get; set; }

        public string InstituteName { get; set; }

        

    }

    #endregion



}