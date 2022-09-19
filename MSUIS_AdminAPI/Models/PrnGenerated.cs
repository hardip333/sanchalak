using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class PrnGenerated
    {
        public string AdmissionId { get; set; }
        public string PRN { get; set; }
        public string NameAsPerMarksheet { get; set; }
        public string InstancePartName { get; set; }
        public Int64 ProgrammeInstancePartId { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public string InstancePartTermName { get; set; }
        public string FacultyName { get; set; }
        public string ProgrammeName { get; set; }
        public string BranchName { get; set; }
        public string InstituteName { get; set; }
        public string FeeCategoryName { get; set; }

        public Int64 IndexId { get; set; }

        public string PRNGenerated { get; set; }

        public string Gender { get; set; }

        public string IsPhysicallyChanllenged { get; set; }

        public string ApplicationReservationName { get; set; }

        public string SocialCategoryName { get; set; }

        public string CommitteeName { get; set; }

        public string Nationality { get; set; }


        public string CurrentAddress { get; set; }

        public string PermanentAddress { get; set; }
        public string FatherMotherContactNo { get; set; }
        public string LocalToVadodara { get; set; }

        public string MobileNo { get; set; }

        public string EmailId { get; set; }

        public string AdminRemarkByAcademics { get; set; }

        public String EligibilityByAcademics { get; set; }

        //ApplicationList Model

        public Int32 InstituteId { get; set; }
        public Int32 InstancePartTermId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }

        public String MiddleName { get; set; }
        public String SpouseName { get; set; }
        public String NameOfMother { get; set; }
        public String NameOfFather { get; set; }

        public String DOB { get; set; }
        public String AdmApplicantRegistrationId { get; set; }
        public String MaritalStatus { get; set; }
        public String MotherTongueName { get; set; }
        public String BloodGroupName { get; set; }
        public String ReligionName { get; set; }
        public String IsNRI { get; set; }
        public String HeightInCms { get; set; }
        public String WeightInKgs { get; set; }
        public String CitizenCountry { get; set; }

        public String PermanentCountry { get; set; }
        public String PermanentState { get; set; }
        public String PermanentDistrict { get; set; }
        public String PermanentCityVillage { get; set; }
        public String PermanentPincode { get; set; }
        public String IsCurrentAsPermanent { get; set; }
        public String CurrentCountry { get; set; }
        public String CurrentState { get; set; }
        public String CurrentDistrict { get; set; }
        public String CurrentCityVillage { get; set; }
        public String CurrentPincode { get; set; }
        public String OptionalMobileNo { get; set; }
        public String GuardianContactNo { get; set; }
        public String IsEWS { get; set; }
        public String IsPhysicallyChallenged { get; set; }
        public String IsEmp { get; set; }
        public String FatherOccupation { get; set; }
        public String MotherOccupation { get; set; }
        public String GuardianOccupation { get; set; }
        public String FamilyAnnualIncome { get; set; }
        public String GuardianAnnualIncome { get; set; }
        public String IsGuardianEbc { get; set; }
        public String UserName { get; set; }
        public String OtherReligion { get; set; }
        public String DOBDoc { get; set; }
        public String PhotoIdDoc { get; set; }
        public String IsMajorThelesamiaStatus { get; set; }
        public String PassportNumber { get; set; }
        public String PassportDate { get; set; }
        public String AadharNumber { get; set; }
        public String AadharDoc { get; set; }
        public String NameOnAadhar { get; set; }

        public String GSocialCategory { get; set; }
        public String IsSocialDocSubmitted { get; set; }
        public String IsEWSDocSubmitted { get; set; }
        public String IsPCDocSubmitted { get; set; }
        public String IsReservationDocSubmitted { get; set; }
        public String SocialCategoryDoc { get; set; }
        public String ReservationCategoryDoc { get; set; }
        public String ReservationCategory { get; set; }
        public String GuardianName { get; set; }
        public String CurrentEmployerName { get; set; }
        public String IsSmsPermissionGiven { get; set; }
        public String IsLocalToVadodara { get; set; }
        public String EWSDoc { get; set; }
        public String PCDoc { get; set; }
        public String DisabilityPercentage { get; set; }
        public String DisabilityType { get; set; }
        public String ApplicantPhoto { get; set; }
        public String ApplicantSignature { get; set; }

        public String AdmApplicationIdStr { get; set; }
        public Int64 AdmApplicationId { get; set; }
        public String VerifiedStatus { get; set; }
        public String IsAdmitted { get; set; }
        public String AdmittedOn { get; set; }
        public String AdmittedInstituteName { get; set; }
        public String AdminRemarkByFaculty { get; set; }

        public String EligibleDegreeName { get; set; }
        public string SpecializationName { get; set; }
        public String ExaminationBodyName { get; set; }
        public String InstituteAttended { get; set; }
        public String SchoolNo { get; set; }
        public String CityName { get; set; }
        public String OtherCity { get; set; }
        public String ExamPassCity { get; set; }
        public String ExamPassMonth { get; set; }
        public String ExamPassYear { get; set; }
        public string ExamSeatNumber { get; set; }
        public String ExamCertificateNumber { get; set; }
        public String MarkObtained { get; set; }
        public String MarkOutof { get; set; }
        public String Grade { get; set; }
        public String CGPA { get; set; }
        public String PercentageEquivalenceCGPA { get; set; }
        public String Percentage { get; set; }
        public String ClassName { get; set; }
        public String IsFirstTrial { get; set; }
        public String IsLastQualifyingExam { get; set; }
        public String LanguageName { get; set; }
        public String ResultStatus { get; set; }
        public String AttachDocument { get; set; }
        public String IsDeclared { get; set; }

        public String Name { get; set; }
        public String IsVerifiedByFaculty { get; set; }

        public Int32 FacultyId { get; set; }

        public Int32 AcademicYearId { get; set; }

        public String IsCancelledByStudent { get; set; }

        public Int32 ProgInstPartTermId { get; set; }


    }
}