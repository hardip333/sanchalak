using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class PostConfigurationAdmission
    {
        public Int64 Id { get; set; }
        public Int64 ApplicantRegistrationId { get; set; }
        public Int64 AcademicYearId { get; set; } //Add by Mohini on 18-May-2022
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string FullName { get; set; }
        public string ApplicationReservationCode { get; set; }
        public string AdminRemarkByFaculty { get; set; }
        public string AdminRemarkByAcademics { get; set; }
        public string FeeTypeCode { get; set; }
        public Int64 ApprovedByFaculty { get; set; }
        public Int64 ApprovedByAcademics { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public string FacultyEligibilityStatus { get; set; }
        public string EligibilityStatus { get; set; }
        public string VerificationStatus { get; set; }
        public string VerificationRemarks { get; set; }
        public string EligibilityByAcademics { get; set; }
        public string InstancePartTermName { get; set; }
        public string FeeCategoryName { get; set; }
        public Int64 IndexId { get; set; }

        public bool IsVerificationSms { get; set; }
        public bool IsVerificationEmail { get; set; }
         public bool SelectedCheck { get; set; }
        public bool SelectedCheckEmail { get; set; }

        //For Approval List Extra
        public Int64 FacultyId { get; set; }
        public Int64 InstituteId { get; set; }
        public bool IsAdmissionFeePaid { get; set; }

        public string ProgrammeName { get; set; }
        public string BranchName { get; set; }
        public string AcademicYearCode { get; set; }
        //public string EmailKeyName { get; set; }
        //public string StuEmailList { get; set; }
        //public Int64 IsVerificationSmsBy { get; set; }
        //public Int64 IsVerificationEmailBy { get; set; }
        //public Int64 TotalCount { get; set; }
        //public Int64 SMSSuccessCount { get; set; }
        //public Int64 SMSFailureCount { get; set; }
        //public string AdmissionFeesStopDate { get; set; }
        //public string FinalAdmFeeDate { get; set; }
        //public Int64 AdmFeePaidCount { get; set; }
        //public Int64 EligibleCountByFaculty { get; set; }
        //public Int64 EligibleCountByAcademic { get; set; }
        public string IsVerificationSmsOn { get; set; }
        public string IsVerificationEmailOn { get; set; }
        public string EduColorCode { get; set; }
        public string EduBoardName { get; set; }
        public Boolean IsPRNGenerated { get; set; }
        public string InstituteName { get; set; }
        public Int64 UserName { get; set; }
        public Boolean IsLastQualifyMS { get; set; }
    }
    public class SentSMSEmail
    {
    
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public Int64 TotalCount { get; set; }
        public Int64 SMSSuccessCount { get; set; }
        public Int64 SMSFailureCount { get; set; }
        public Int64 EmailSuccessCount { get; set; }
        public Int64 EmailFailureCount { get; set; }
      
    }

    public class ConfigureDates
    {
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public string AdmissionFeesStopDate { get; set; }
    }

	public class PostSMSSent
    {
        public Int64 Id { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public bool IsVerificationSms { get; set; }
        public Int64 IsVerificationSmsBy { get; set; }
        //public string AdmissionFeesStopDate { get; set; }
        public string FinalAdmFeeDate { get; set; }
   
    }
    public class PostEmailSent
    {
        public Int64 Id { get; set; }
        public bool IsVerificationEmail { get; set; }
        public Int64 IsVerificationSmsBy { get; set; }
        //public string AdmissionFeesStopDate { get; set; }
        public string ProgrammeName { get; set; }
        public string BranchName { get; set; }
        public string AcademicYearCode { get; set; }
        public string EmailKeyName { get; set; }
        public string StuEmailList { get; set; }
        public Int64 IsVerificationEmailBy { get; set; }
        public string FinalAdmFeeDate { get; set; }

    }
    public class FacultyVerificationCount
    {
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public Int64 TotalCountforFaculty { get; set; }
        public Int64 ProApprovedCountforFaculty { get; set; }
        public Int64 NotApprovedCountforFaculty { get; set; }
        public Int64 PendingCountforFaculty { get; set; }
        public Int64 VerifiedCountforFaculty { get; set; }

    }
    
    // Jay Code Start
    public class VerificationReport
    {
        public Int64 Id { get; set; }
        public Int64 IndexId { get; set; }
        public Int64 ApplicantRegistrationId { get; set; }
        public String LastName { get; set; }
        public String FirstName { get; set; }
        public String MiddleName { get; set; }
        public String FullName { get; set; }
        public String NameAsPerMarksheet { get; set; }
        public String DocName { get; set; }
        public String AdminRemarkByFaculty { get; set; }
        public String EligibilityStatus { get; set; }
        public String InstancePartTermName { get; set; }
        public String ChoiceValue { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public Int32 FacultyId { get; set; }
        public String ApplicationStatus { get; set; }
        public String InstituteName { get; set; }
        public String FeeCategoryName { get; set; }
        public String GroupName { get; set; }
        public Int32 AdmApplicantsSubmittedDocumentsVerified { get; set; }
        public Int32 AdmApplicantsSubmittedDocumentsNotVerified { get; set; }
        public Int32 AdmApplicantsSubmittedDocumentsTotal { get; set; }
        public Int32 AdmApplicationEducationDetailsVerified { get; set; }
        public Int32 AdmApplicationEducationDetailsNotVerified { get; set; }
        public Int32 AdmApplicationEducationDetailsTotal { get; set; }
        public Int32 AdmStudentAddOnInformationVerified { get; set; }
        public Int32 AdmStudentAddOnInformationNotVerified { get; set; }
        public Int32 AdmStudentAddOnInformationTotal { get; set; }
        

      
    }

    // Jay Code End


    //Start - Mohini's Code

    public class AcademicVerificationCount
    {
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public Int64 TotalCount { get; set; }
        public Int64 EligibleCountByFaculty { get; set; }
        public Int64 EligibleCountByAcademic { get; set; }
        public Int64 AdmFeePaidCount { get; set; }

    }

    //End - Mohini's Code









}