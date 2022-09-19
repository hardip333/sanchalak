using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class ProfileForm
    {
        //Course & Faculty Module

        public Int64 PRN { get; set; }
        public Int32 FeePayCourseId { get; set; }
        public string AdmApplicationCreatedOn { get; set; }
        public Int64 AdmissionApplicationId { get; set; }
        public string FacultyName { get; set; }
        public string FacultyAddress { get; set; }
        public string AcademicYearCode { get; set; }
        public string Amount { get; set; }
        public string FeeTypeName { get; set; }
        public string PartTermName { get; set; }
        public string PartName { get; set; }
        public string ProgrammeName { get; set; }

        public string BranchName { get; set; }
        public string TransectionId { get; set; }
        public string TransectionDate { get; set; }
        public string FeeCategoryName { get; set; }
        public string AllotmentNo { get; set; }


        //AdmApplicantRegistration Module
        public Int64 Id { get; set; }
        public Int64 UserName { get; set; }
        public String Password { get; set; }
        public String LastName { get; set; }
        public String FirstName { get; set; }
        public String MiddleName { get; set; }
        public String Gender { get; set; }
        public String ReligionName { get; set; }
        public Int64 ReligionId { get; set; }

        //public Int64? MaritalStatusId { get; set; }
        public String MaritalStatus { get; set; }
        public Int64 MotherTongueId { get; set; }
        public String MotherTongueName { get; set; }
        public Int64 CommunicationLanguageId { get; set; }
        public String CommunicationLanguageName { get; set; }
        public String DOB { get; set; }
        public Int64? BloodGroupId { get; set; }
        public String BloodGroupName { get; set; }
        public Decimal? HeightInCms { get; set; }
        public Decimal? WeightInKgs { get; set; }
        public String IsMajorThelesamiaStatus { get; set; }
        public string IsNRI { get; set; }
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
        public Int64 PermanentPincode { get; set; }
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
        public Int64? GSocialCategoryId { get; set; }
        public String GSocialCategory { get; set; }
        public String ReservationCategory { get; set; }

        public String GuardianName { get; set; }
        public String GuardianContactNo { get; set; }
        public Int64? FamilyAnnualIncome { get; set; }
        public Int64? OccupationIdOfFather { get; set; }
        public String OccupationOfFather { get; set; }
        public Int64? OccupationIdOfMother { get; set; }
        public String OccupationOfMother { get; set; }
        public Int64? OccupationIdOfGuardian { get; set; }
        public String OccupationOfGuardian { get; set; }
        public bool IsSelfEmp { get; set; }
        public String IsSelfEmpDisplay { get; set; }
        public bool IsEbc { get; set; }

        public String IsEbcDisplay { get; set; }

        //public string Occupation { get; set; }
        public String GuardianOccupation { get; set; }
        public Int64? GuardianAnnualIncome { get; set; }
        public String EmailId { get; set; }
        public String MobileNo { get; set; }
        public String OptionalMobileNo { get; set; }
        public bool IsSmsPermissionGiven { get; set; }
        public String IsSmsPermissionGivenDisplay { get; set; }

        public bool? IsLocalToVadodara { get; set; }
        public Int64? ActivityId { get; set; }
        public String Activity { get; set; }
        public String ActivityName { get; set; }
        public Int64? ParticipationLevelsId { get; set; }
        public String ParticipationLevel { get; set; }
        public Int64? SecuredRankId { get; set; }
        public String SecuredRank { get; set; }
        public String ApplicantPhoto { get; set; }
        public String ApplicantSignature { get; set; }

        public Int32? ApplicationCategoryId { get; set; }
        public String IsEWS { get; set; }
        public String EWSDoc { get; set; }
        public String IsPhysicallyChallenged { get; set; }
        public String PCDoc { get; set; }
        public Int64? DisabilityPercentage { get; set; }
        public String DisabilityType { get; set; }
        //public string ApplicationReservationName { get; set; }




        //Education Module
        public Int32 EduId { get; set; }
        public Int32 AdmApplicantRegistrationId { get; set; }
        public Int64 AdmApplicantRegiUserName { get; set; }
        public Int32 EligibleDegreeId { get; set; }
        public Int32 SpecializationId { get; set; }
        public Int32 ExaminationBodyId { get; set; }
        public string InstituteAttended { get; set; }
        public Int32 InstituteCityId { get; set; }
        public string ExamPassMonth { get; set; }
        public string ExamPassYear { get; set; }
        public string ExamSeatNumber { get; set; }
        public string ExamCertificateNumber { get; set; }
        public Int32 MarkObtained { get; set; }
        public Int32 MarkOutof { get; set; }
        public string Grade { get; set; }
        public decimal CGPA { get; set; }
        public decimal PercentageEquivalenceCGPA { get; set; }

        public decimal Percentage { get; set; }
        public Int32 ClassId { get; set; }
        public bool IsFirstTrial { get; set; }
        public bool IsLastQualifyingExam { get; set; }
        public Int32 TeachingLanguageId { get; set; }
        public string ResultStatus { get; set; }
        public string AttachDocument { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int32 CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Int32 ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string EligibleDegreeName { get; set; }
        public string SpecializationName { get; set; }
        public string ExaminationBodyName { get; set; }
        public string CityName { get; set; }
        public string LanguageName { get; set; }
        public string ClassName { get; set; }
        public string OtherCity { get; set; }
        public bool? IsDeclared { get; set; }
        public string NameAsPerMarksheet { get; set; }

    }
}