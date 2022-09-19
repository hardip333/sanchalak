using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class PostApplicantVerification
    {
        public Int64 Id { get; set; }
        public Int64 ApplicantRegId { get; set; } 
        public Int64 ModifiedBy { get; set; }
        public Int64 AcademicYearId { get; set; }
        public string MainCategoryRemarks { get; set; }
        public string AppliedCategoryRemarks { get; set; }
        public string AdminRemarkByFaculty { get; set; }
        public string EligibilityStatus { get; set; }
        public string EduVerification { get; set; }
        public bool EduVerificationEdit { get; set; }
        public string IsDOBDocVerified { get; set; }
        public bool IsDOBDocVerifiedEdit { get; set; }
        public string IsPhotoIdDocVerified { get; set; }
        public bool IsPhotoIdDocVerifiedEdit { get; set; }
        public string IsAadharDocVerified { get; set; }
        public bool IsAadharDocVerifiedEdit { get; set; }
        public string IsSocialCategoryDocVerified { get; set; }
        public bool IsSocialCategoryDocVerifiedEdit { get; set; }
        public string IsReservationCategoryDocVerified { get; set; }
        public bool IsReservationCategoryDocVerifiedEdit { get; set; }
        public bool IsNCLCertiVerifiedEdit { get; set; }
        public bool IsEWS_IADocVerifiedEdit { get; set; }
        public string IsEWSDocVerified { get; set; }
        public bool IsEWSDocVerifiedEdit { get; set; }
        public string IsPCDocVerified { get; set; }
        public bool IsPCDocVerifiedEdit { get; set; }
        public string IsApplicantPhotoVerified { get; set; }
        public bool IsApplicantPhotoVerifiedEdit { get; set; }
        public string IsApplicantSignatureVerified { get; set; }
        public bool IsApplicantSignatureVerifiedEdit { get; set; }





        public string CategoryVerification { get; set; }
        public bool CategoryVerificationEdit { get; set; }
        public string DisabilityVerification { get; set; }
        public bool DisabilityVerificationEdit { get; set; }
        public string AppliedCategoryVerification { get; set; }
        public bool AppliedCategoryVerificationEdit { get; set; }
        public Int32 FeeCategoryId { get; set; }
        public Int32 ProgrammeInstancePartTermId { get; set; }
        public string VerificationStatus { get; set; }
        public string VerificationRemarks { get; set; }
        public string FeeCategoryName { get; set; }
        public string GroupName { get; set; }
        public Int64 FeeCategoryPartTermMapId { get; set; }
        public string ApplicationIDStatus { get; set; }
        public Int64 AdmissionApplicationId { get; set; }
        public Int64 AdmApplicationId { get; set; }
        public Int64 InstPartTermId { get; set; }
        public Int64 ApplicationId { get; set; }
        public bool IsVerified { get; set; }
        public bool IsVerifiedByAcademic { get; set; }
        public Int64 ApprovedByAcademic { get; set; }


        //public Int64 AdmissionApplicationId{ get; set; }
        public string RemarkByFaculty { get; set; }
        public Int64 RequestBy { get; set; }
        public Int64 VerifiedByFaculty { get; set; }

        //For Institute List Get
        public Int64 InstituteId { get; set; }
        public string InstituteName { get; set; }
        public Int64 AdmittedInstituteId { get; set; }

        public Int32? PreferenceGroupId{ get; set; }
        public Int64? DestinationIncProgInstPartTermId { get; set; }
        public Int64? DestProgID { get; set; }
        
        //For Academic Section Get
        public string EligibilityByAcademics { get; set; }
        public string AdminRemarkByAcademics { get; set; }
        public Int64 ApprovedByFaculty { get; set; }
        public Int64 ApprovedByAcademics { get; set; }
        public bool IsDualAdmission { get; set; }
        public Boolean ? IsPRNGenerated { get; set; }




        //For Send SMS & Email
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string InstPartTerm { get; set; }
        public string ProgrammeName { get; set; }
        public string BranchName { get; set; }
        public string AcademicYearCode { get; set; }
        public string EmailKeyName { get; set; }
        public string PendingDocList { get; set; }
        public Int64 IsVerificationEmailBy { get; set; }
        public Int64 IsVerificationSmsBy { get; set; }
        public string FinalAdmFeeDate { get; set; }
    }
    public class FeeCategoryWithDest
    {
        public Int64 AppId { get; set; }
        public Int64 DestPartTermId { get; set; }
        public Int32 FeeCategoryId { get; set; }
        public Int32 ProgrammeInstancePartTermId { get; set; }
        public string FeeCategoryName { get; set; }
        public string EligibilityStatus { get; set; }
        public string AdminRemarkByFaculty { get; set; }
        public Int64 FeeCategoryPartTermMapId { get; set; }
        public string ApplicationIDStatus { get; set; }
        public string Gender { get; set; }
    }

    public class PersonalDetails
    {
        public Int64 AdmApplicantRegiUserName { get; set; }
        public Int64 ApplicantRegId { get; set; }
        public string Gender { get; set; }
        public Int32? SocialCategoryId { get; set; }
        public Int32 ApplicationCategoryId { get; set; }
        public DateTime? NCLCerti_IsuueDate { get; set; }
        public DateTime? NCLCertiValidityDate { get; set; }

        public string NCLCertiNo { get; set; }
        public string SocialCategoryDoc { get; set; }
        public string ReservationCategoryDoc { get; set; }
        public string NCLCerti { get; set; }
        public string EWSDoc { get; set; }
        public string IsEWS { get; set; }
        public string EWS_IADoc { get; set; }
        public string EWSCertiNo { get; set; }
        public DateTime? EWSCerti_IsuueDate { get; set; }
        public DateTime? EWSCertiValidityDate { get; set; }
    }
}