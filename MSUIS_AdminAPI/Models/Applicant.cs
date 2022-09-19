using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{

 public class DecodePassword
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        [JsonIgnore]
        public string RetrivedPassword { get; set; }
        [JsonIgnore]
        public string SaltKey { get; set; }
        [JsonIgnore]
        public string HashPassword { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class Applicant
    {
        public Int64 Id { get; set; }
        public Int64 UserName { get; set; }
        public String Password { get; set; }
        public String LastName { get; set; }
        public String FirstName { get; set; }
        public String MiddleName { get; set; }
        public String Gender { get; set; }
        public String ReligionName { get; set; }
        public Int64 ReligionId { get; set; }
        public Int64? MaritalStatusId { get; set; }
        public String MaritalStatus { get; set; }
        public Int64 MotherTongueId { get; set; }
        public String MotherTongueName { get; set; }
        public Int64 CommunicationLanguageId { get; set; }
        public String CommunicationLanguageName { get; set; }
        public DateTime DOB { get; set; }
        public Int64? BloodGroupId { get; set; }
        public String BloodGroupName { get; set; }
        public Decimal? HeightInCms { get; set; }
        public Decimal? WeightInKgs { get; set; }
        public String IsMajorThelesamiaStatus { get; set; }
        public bool? IsNRI { get; set; }
        public Int64 CountryIdOfCitizenship { get; set; }
        public String CitizenCountry { get; set; }
        public String PassportNumber { get; set; }
        public String AadharNumber { get; set; }
        public String NameOnAadhar { get; set; }
        public String PermanentAddress { get; set; }
        public Int64 PermanentCountryId { get; set; }
        public String PermanentCountry { get; set; }
        public Int64 PermanentStateId { get; set; }
        public String PermanentState { get; set; }
        public Int64 PermanentDistrictId { get; set; }
        public String PermanentDistrict { get; set; }
        public String PermanentCityVillage { get; set; }
        public Int64? PermanentPincode { get; set; }
        public bool IsCurrentAsPermanent { get; set; }
        public String CurrentAddress { get; set; }
        public Int64? CurrentCountryId { get; set; }
        public String CurrentCountry { get; set; }
        public Int64? CurrentStateId { get; set; }
        public String CurrentState { get; set; }
        public Int64? CurrentDistrictId { get; set; }
        public String CurrentDistrict { get; set; }
        public String CurrentCityVillage { get; set; }
        public Int64? CurrentPincode { get; set; }
        public String NameOfFather { get; set; }
        public String NameOfMother { get; set; }
        public Int64? SocialCategoryId { get; set; }
        public String SocialCategoryName { get; set; }
        public String SocialCategoryCode { get; set; } //Added by Mohini
        public Int64? GSocialCategoryId { get; set; }
        public String GSocialCategory { get; set; }
        public String GuardianName { get; set; }
        public String GuardianContactNo { get; set; }
        public Int64? FamilyAnnualIncome { get; set; }
        public Int64? OccupationIdOfFather { get; set; }
        public String FatherOccupation { get; set; }
        public Int64? OccupationIdOfMother { get; set; }
        public String MotherOccupation { get; set; }
        public Int64? OccupationIdOfGuardian { get; set; }
        public String GuardianOccupation { get; set; }
        public Int64? GuardianAnnualIncome { get; set; }
        public String EmailId { get; set; }
        public String MobileNo { get; set; }
        public String OptionalMobileNo { get; set; }
        public bool? IsSmsPermissionGiven { get; set; }
        public bool? IsLocalToVadodara { get; set; }
        public Int64? ActivityId { get; set; }
        public String Activity { get; set; }
        public String ActivityName { get; set; }
        public Int64? ParticipationLevelsId { get; set; }
        public String ParticipationLevel { get; set; }
        public Int64? SecuredRankId { get; set; }
        public String SecuredRank { get; set; }
        public Int64 ApplicationCategoryId { get; set; }
        public String ReservationCategory { get; set; }
        public String ReservationCategoryCode { get; set; } //Added by Mohini
        public Int64 AppliedCategoryId { get; set; }
        public String AppliedCategory { get; set; }
        public String AppliedCategoryDocument { get; set; }
        public String IsEWS { get; set; }
        public String IsPhysicallyChallenged { get; set; }
        public Int64? DisabilityPercentage { get; set; }
        public String EWSDoc { get; set; }
        public String DisabilityType { get; set; }
        public String PCDoc { get; set; }
        public String DOBDoc { get; set; }
        public String SpouseName { get; set; }
        public String NameAsPerMarksheet { get; set; }
        public String AadharDoc { get; set; }
        public String ApplicantPhoto { get; set; }
        public String ApplicantSignature { get; set; }
        public bool? IsEmp { get; set; }
        public String CurrentEmployerName { get; set; }
        public bool? IsGuardianEbc { get; set; }
        public String FatherMotherContactNo { get; set; }
        public String PhotoIdDoc { get; set; }
        public String OtherReligion { get; set; }
        public bool? IsSocialDocSubmitted { get; set; }
        public bool? IsReservationDocSubmitted { get; set; }
        public bool? IsEWSDocSubmitted { get; set; }
        public bool? IsPCDocSubmitted { get; set; }
        public String SocialCategoryDoc { get; set; }
        public String ReservationCategoryDoc { get; set; }
        public String MainCategoryRemarks { get; set; }
        public String AppliedCategoryRemarks { get; set; }
        public bool IsConfirm { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public String IsDOBDocVerified { get; set; }
        public String IsPhotoIdDocVerified { get; set; }
        public String IsAadharDocVerified { get; set; }
        public String IsSocialCategoryDocVerified { get; set; }
        public String IsReservationCategoryDocVerified { get; set; }
        public String IsEWSDocVerified { get; set; }
        public String IsPCDocVerified { get; set; }
        public String IsApplicantPhotoVerified { get; set; }
        public String IsApplicantSignatureVerified { get; set; }
        public String NCLCerti { get; set; }
        public String NCLCertiNo { get; set; }  
        public String NCLCerti_IsuueDateView { get; set; }
        public String NCLCertiValidityDateView { get; set; }
        public String IsNCLCertiVerified { get; set; }
        public String IsEWS_IADocVerified { get; set; }
        public String EWS_IADoc { get; set; }
        public String EWSCertiNo { get; set; }
        public String EWSCerti_IsuueDateView { get; set; }
        public String EWSCertiValidityDateView { get; set; }

    }
	
	
	 public class VerifiedApplicantForStudentProcess
    {
        public Int64 SerializeNumber { get; set; }
        public Int64 ApplicationId { get; set; }
        public Int64 UserName { get; set; }
        public String Password { get; set; }
        public String LastName { get; set; }
        public String FirstName { get; set; }
        public String MiddleName { get; set; }
        public bool IsApprovedByFaculty { get; set; }
        public bool IsApprovedByAcademic { get; set; }
        public bool IsCancelledByStudent { get; set; }
        public bool IsCancelledByFaculty { get; set; }
        public bool IsCancelledByAcademics { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public bool IsAdmitted { get; set; }
        public string InstancePartTermName { get; set; }
        public string EligibilityByAcademics { get; set; }
        public string EligibilityStatus { get; set; }
        public string AdminRemarkByAcademics { get; set; }
        
    }

    public class TransferredStudentGetByProgrammeInstancePartTermId
    {
        public Int64 SerializeNumber { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public Int64 PRN { get; set; }
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string EligibilityByAcademics { get; set; }
        public string AdminRemarkByAcademics { get; set; }
        public bool IsEmailSend { get; set; }
        public bool IsSMSSend { get; set; }

    }

    public class TransferredStudentForSMSSend
    {
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public string PRN { get; set; }
        public string FullName { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public string SaltKey { get; set; }
        public string HashPassword { get; set; }
        public bool IsSMSSend { get; set; }

    }
    public class TransferredStudentForEmailSend
    {
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public string InstancePartTermName { get; set; }
        public string PRN { get; set; }
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string SaltKey { get; set; }
        public string HashPassword { get; set; }
        public bool IsEmailSend { get; set; }

    }

	
    public class ApplicantVerification
    {
        public String ApplicantRegistrationId { get; set; }
        public String token { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public String UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public String Password { get; set; }
        public Int32? ModifiedBy { get; set; }
    }
	public class ForgotUsername
    {
        public String EmailId { get; set; }
    }

    public class ForgotPassword
    {
        public String Username { get; set; }
        public String EmailId { get; set; }
    }

    public class ChangePassword
    {
        public String Username { get; set; }
        public String Password { get; set; }
    }
}
