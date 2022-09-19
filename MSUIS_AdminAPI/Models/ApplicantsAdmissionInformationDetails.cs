using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class ApplicantsAdmissionPersonalDetails
    {
        public Int64 UserName { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String MiddleName { get; set; }
        public string Gender { get; set; }
        public string Religion { get; set; }
        public string MotherTongue { get; set; }
        public String NameAsPerMarksheet { get; set; }
        public String EmailId { get; set; }
        public String MobileNo { get; set; }
        public String SpouseName { get; set; }
        public String NameOfFather { get; set; }
        public String NameOfMother { get; set; }
        public String OccupationOfFather { get; set; }
        public String OccupationOfMother { get; set; }
        public String FatherMotherContactNo { get; set; }
        public String OccupationOfGuardian { get; set; }
        public Boolean IsGuardianEbc { get; set; }
        public String GuardianAnnualIncome { get; set; }
        public String GuardianContactNo { get; set; }
        public String GuardianName { get; set; }

        public Boolean IsApplicantSignatureVerified { get; set; }
        public Boolean IsApplicantPhotoVerified { get; set; }
        public Boolean IsPhotoIdDocVerified { get; set; }
        public Boolean IsConfirm { get; set; }
        public Boolean IsEmp { get; set; }
        public Boolean IsLocalToVadodara { get; set; }
        public Boolean IsNRI { get; set; }
        public Boolean IsSmsPermissionGiven { get; set; }
        public String CurrentEmployerName { get; set; }
        public String IsMajorThelesamiaStatus { get; set; }

        public String OptionalMobileNo { get; set; }
        public String OtherReligion { get; set; }
        public String FamilyAnnualIncome { get; set; }
        public String MaritalStatus { get; set; }
        public String BloodGroup { get; set; }
        public String PassportDate { get; set; }
        public String PassportNumber { get; set; }
        public String CommunicationLanguage { get; set; }
        public String NameOnAadhar { get; set; }
        public String WeightInKgs { get; set; }
        public String HeightInCms { get; set; }
        public String AadharNumber { get; set; }
        public String AadharDoc { get; set; }
        public Boolean IsAadharDocVerified { get; set; }
        public String DOB { get; set; }
        public String DOBDoc { get; set; }
        public Boolean IsDOBDocVerified { get; set; }
        public Boolean IsCurrentAsPermanent { get; set; }
        public String CurrentAddress { get; set; }
        public String CurrentCountry { get; set; }
        public String CurrentState { get; set; }
        public String CurrentDistrict { get; set; }
        public String CurrentCityVillage { get; set; }
        public String CurrentPincode { get; set; }
        public String PermanentAddress { get; set; }
        public String PermanentCityVillage { get; set; }
        public String PermanentCountry { get; set; }
        public String PermanentDistrict { get; set; }
        public String PermanentPincode { get; set; }
        public String PermanentState { get; set; }
        public String ApplicantPhoto { get; set; }
        public String ApplicantSignature { get; set; }
       
        public String IsPhysicallyChallenged { get; set; }
        public String DisabilityType { get; set; }
        public String DisabilityPercentage { get; set; }
        public String PCDoc { get; set; }
        public Boolean IsPCDocVerified { get; set; }
        public Boolean IsPCDocSubmitted { get; set; }
     
        public String EWSDoc { get; set; }
        public Boolean IsEWSDocSubmitted { get; set; }
        public Boolean IsEWSDocVerified { get; set; }
       
        public String ReservationCategoryDoc { get; set; }      
        public Boolean IsReservationCategoryDocVerified { get; set; }
        public Boolean IsReservationDocSubmitted { get; set; }
      
        public String SocialCategoryDoc { get; set; }
        public Boolean IsSocialCategoryDocVerified { get; set; }
        public Boolean IsSocialDocSubmitted { get; set; }


    }
    public class ApplicantsAdmissionInformationDetails
    {
        public Int64 UserName { get; set; }
        public int IndexId { get; set; }
        public String InstancePartTermName { get; set; }
        public String ProgrammeName { get; set; }
        public String FacultyName { get; set; }
        public String BranchName { get; set; }
        public String ApplicationStatus { get; set; }
        public Int64 ApplicationId { get; set; }
      
           
        public Boolean IsApprovedByFaculty { get; set; }
        public string ApprovedByFaculty { get; set; }
        public Boolean IsApprovedByAcademic { get; set; }
        public string ApprovedByAcademic { get; set; }
        public Boolean IsAdmissionFeePaid { get; set; }
        public string AdmissionFeePaid { get; set; }
        public Boolean IsPRNGenerated { get; set; }
        public string PRNGenerated { get; set; }
        public Boolean IsPaperSelected { get; set; }
        public string PaperSelected { get; set; }
        public Boolean IsCancelledByStudent { get; set; }
        public string CancelledByStudent { get; set; }
        public Boolean IsCancelledByFaculty { get; set; }
        public string CancelledByFaculty { get; set; }
        public Boolean IsCentrallyAdmission { get; set; }
        public string CentrallyAdmission { get; set; }
        public Boolean IsCancelledByAcademics { get; set; }
        public Boolean IsDualAdmission { get; set; }
        public string CancelledByAcademics { get; set; }
      
    
        public string TotalAdmissionFeePaid { get; set; }
        public string TotalAmount { get; set; }
        public string AdminRemarkByFaculty { get; set; }
        public string AdminRemarkByAcademics { get; set; }
        public string StudentCancelledRemark { get; set; }
        public string AcademicsCancelledRemark { get; set; }
        public string FacultyCancelledRemark { get; set; }
        public string CancelType { get; set; }
        public string CancelledApplicationStatus { get; set; }
        public string AuthorityLetter { get; set; }
        public string PassbookDoc { get; set; }
        public string RTGSForm { get; set; }
        public string ApplicationDoc { get; set; }
        public Boolean IsBOBAccount { get; set; }

        public string BranchChangeRequest { get; set; }
        public string FeeCategoryChangeRequest { get; set; }
        public string InstituteChangeRequest { get; set; }
        public string CancelChangeRequest { get; set; }
        
        public Int64 ApplicantRegistrationId { get; set; }


        public string DateOfApplication { get; set; }    
        public string EligibilityByAcademics { get; set; }    
        public string DestinationIncProgInstPartTerm { get; set; }
        public string AdmittedOn { get; set; }
        public string BlockedOn { get; set; }
        public string FacultyCancelledOn { get; set; }
        public string AcademicsCancelledOn { get; set; }
        public string VerifiedOnFaculty { get; set; }
        public string ApprovedOnFaculty { get; set; }
        public string ApprovedOnAcademics { get; set; }       
        public string AdmissionBlockRemark { get; set; }
        public string FeeCategoryName { get; set; }
        public string EligibilityStatus { get; set; }
        public string BlockedRemark { get; set; }
        public string AllotmentNo { get; set; }
        public string MeritNo { get; set; }
        public string AdmissionCommittee { get; set; }

        public Boolean IsAdmitted { get; set; }
        public Boolean IsAdmissionBlock { get; set; }
        public Boolean IsSmsSent { get; set; }
        public Boolean IsEmailSent { get; set; }
        public Boolean IsAdmissionSecuredElesewhere { get; set; }
        public Boolean IsAppliedElseWhere { get; set; }
        public Boolean IsVerifiedByFaculty { get; set; }
        public Boolean IsVerificationSms { get; set; }
        public Boolean IsVerificationEmail { get; set; }
        public string IsVerificationSmsOn { get; set; }
        public string IsVerificationEmailOn { get; set; }
        public Boolean IsBlocked { get; set; }

        public string ApprovedProgrammeInstancePartTerm { get; set; }
        public string BankName { get; set; }
        public string OtherUniversityName { get; set; }
        public string OtherUniversityProgrammeName { get; set; }
        public string Reason { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string IFSCCode { get; set; }
        public string RequestedOn { get; set; }
        public string CancelApprovedOnAcademic { get; set; }
        public Decimal AutoDeductAmount { get; set; }

        public Boolean IsApprovedByAudit { get; set; }
        public Decimal RefundAmountByAudit { get; set; }
        public string AuditRemark { get; set; }
        public string ProcessedOnAudit { get; set; }
        public string CancelApprovedOnFaculty { get; set; }

        public Boolean IsApprovedByAccount { get; set; }
        public Boolean IsRefundedbyAccount { get; set; }
        public string RefundModeByAcademic { get; set; }
        public string RefundedTansactionId { get; set; }
        public string ProcessedOnAccount { get; set; }
        public string AccountRemark { get; set; }
        public Decimal RefundAmountByAcademic { get; set; }
       
        public Boolean IsRequestCompleted { get; set; }
        public string RequestStatus { get; set; }


    }

    public class ApplicantEducationDetails
    {

        public int IndexEduId { get; set; }
        public String EligibleDegreeName { get; set; }
        public String LanguageName { get; set; }
        public String ClassName { get; set; }
        public String CityName { get; set; }
        public String ExaminationBodyName { get; set; }
        public String SpecializationName { get; set; }
        public String ExamCertificateNumber { get; set; }
        public String AttachDocument { get; set; }
       
        public String ExamPassCity { get; set; }
        public String ExamPassMonth { get; set; }
        public String ExamPassYear { get; set; }
        public String ExamSeatNumber { get; set; }
        public String Grade { get; set; }
        public String InstituteAttended { get; set; }
        public String OtherCity { get; set; }   
        public String ResultStatus { get; set; }
        public String SchoolNo { get; set; }

        public Int64 MarkObtained { get; set; }
        public Int64 MarkOutOf { get; set; }

        public Boolean IsDeclared { get; set; }
        public Boolean IsFirstTrial { get; set; }
        public Boolean IsLastQualifyingExam { get; set; }
        public Boolean IsVerified { get; set; }
      
        public decimal CGPA { get; set; }
        public decimal Percentage { get; set; }
        public decimal PercentageEquivalenceCGPA { get; set; }
            
        public Boolean IsPRNGenerated { get; set; }
        public string PRNGenerated { get; set; }
        public Boolean IsPaperSelected { get; set; }
        public string PaperSelected { get; set; }
      

    }

    public class ApplicantRequestStatusDetails
    {

        public Int64 Id { get; set; }
        public Int64 ApplicationId { get; set; }
        public int IndexRequestId { get; set; }
        public string RequestType { get; set; }    
        public Boolean IsRequestCompleted { get; set; }        
        public string RequestStatus { get; set; }
       
        //public string FeeCategoryChangeRemark { get; set; }
        //public string BranchChangeRemark { get; set; }
        //public string Remarks { get; set; }
        public string OldFeeCategoryName { get; set; }
        public string NewFeeCategoryName { get; set; }
        public string OldInstancePartTermName { get; set; }
        public string NewInstancePartTermName { get; set; }
        public string OldInstituteName { get; set; }
        public string NewInstituteName { get; set; }
        public string OldGroupName { get; set; }
        public string NewGroupName { get; set; }
        public string BranchName { get; set; }
        public string FacultyRemark { get; set; }
        public string FacultyName { get; set; }
        public string ProgrammeName { get; set; }
        public string NameAsPerMarksheet { get; set; }
         


    }

    public class ApplicantPreExaminationDetails
    {
      
        public Int64 ApplicationId { get; set; }
        public Int64 ProgInstPartTermId { get; set; } // Added by Mohini on 08-Apr-2022
        public Int64 PRN { get; set; } // Added by Mohini on 08-Apr-2022
        public Int64 ExamEventId { get; set; } // Added by Mohini on 08-Apr-2022
        public int IndexPreExaminationId { get; set; }
        public string InstancePartTermName { get; set; }
        public string FormNo { get; set; }
        public string SeatNumber { get; set; }
        public string PartTermStatus { get; set; }      
        public string PartStatus { get; set; }      
        public string FacultyName { get; set; }
        public string DisplayName { get; set; }
        public Boolean IsExamFeesPaid { get; set; }
        public Int64 EvaluationId { get; set; }
        public Int64 SpecialisationId { get; set; }
        public Int64 ProgrammePartTermId { get; set; }
    



    }

    public class ApplicantList
    {
        public List<ApplicantsAdmissionPersonalDetails> PersonalList { get; set; }
        public List<ApplicantsAdmissionInformationDetails> AdmissionList { get; set; }
        public Boolean PersonalListflag { get; set; }
        public Boolean AdmissionListflag { get; set; }
        public Boolean EducationListflag { get; set; }
        public Boolean RequestStatusListflag { get; set; }
        public Boolean PreExaminationListflag { get; set; }
        public List<ApplicantEducationDetails> EducationList { get; set; }
        public List<ApplicantRequestStatusDetails> RequestStatusList { get; set; }
        public List<ApplicantPreExaminationDetails> PreExaminationList { get; set; }
        public List<Programme> ProgrammeList { get; set; }
        public Boolean ProgrammeListflag { get; set; }
    }

    public class Programme
    {

        public String InstancePartTermName { get; set; }
        public Int64 Id { get; set; }
        public String InstanceName { get; set; }
        public Int64 ProgrammeInstanceId { get; set; }
        public List<PartTermInst> PTList { get; set; }

    }
    public class PartTermInst
    {

        public String InstancePartTermName { get; set; }
        public Int64 Id { get; set; }
        public String InstanceName { get; set; }
        public Int64 ProgrammeInstanceId { get; set; }
        public String CreatedOn { get; set; }
        public String EligibilityByFaculty { get; set; }
        public String EligibilityByAcademics { get; set; }
        public List<Paper> PaperList { get; set; }

    }
     public class Paper
    {

        public String PaperName { get; set; }
        public Int64 Id { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public Int64 PaperId { get; set; }


    }

    public class StudentDashboard
    {
        public Int32 ProgrammeId { get; set; }
        public String OrderId { get; set; }
        public String InstancePartTermName { get; set; }
        public Int16 InstalmentNo { get; set; }
        public String FeeStatus { get; set; }
        public Int64 ApplicationId { get; set; }
        public String ProgrammeName { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public Int64 AppId { get; set; }
        public Boolean IsHallTicket { get; set; }
        public Boolean IsResult { get; set; }

        //Start region ExamTimeTable
        public Int64 PaperId { get; set; }
        public String PaperName { get; set; }
        public Int32 TeachingLearningMethodId { get; set; }
        public Int32 AssetmentMethodId { get; set; }
        public String TLMAMAT { get; set; }
        public String PaperDate { get; set; }
        public Int64 ExamSlotId { get; set; }
        public String TimeSlot { get; set; }
        public Boolean IsConfirmedByFaculty { get; set; }
        public Boolean IsPublishedByExamSection { get; set; }
        public Int64 ExamMasterId { get; set; }
        public Int64 ExamEventId { get; set; }

        public Boolean IsClosed { get; set; }
        public String DisplayName { get; set; }

        //End region ExamTimeTable

        // Start region ResMarksGradeEntry
        public String SeatNumber { get; set; }
        public String MarksOrGradeObtained { get; set; }
        public String IsAbsent { get; set; }

        public Int16 EvaluationId { get; set; }
        public Int64 SpecialisationId { get; set; }
        public Int64 ProgrammePartTermId { get; set; }
        //Steffi Code Starts
        public Int64 PRN { get; set; }
        //Steffi Code Ends
        // End region ResMarksGradeEntry
    }

    public class ExamFormPrintData
    {
        public Int64 ProgInstPartTermId { get; set; }
        public Int64 PRN { get; set; }
        public Int64 ExamEventId { get; set; }
        public Int64 UserName { get; set; }
        public string FullName { get; set; }
        public string NameOfMother { get; set; }
        public string Gender { get; set; }
        public string PermanentAddress { get; set; }
        public string PermanentCityVillage { get; set; }
        public string DistrictName { get; set; }
        public string StateName { get; set; }
        public Int64 PermanentPincode { get; set; }
        public string MobileNo { get; set; }
        public string OptionalMobileNo { get; set; }
        public string EmailId { get; set; }
        public string DOB { get; set; }
        public string AppearanceType { get; set; }
        public string ApplicationReservationName { get; set; }
        public string EligibilityByAcademics { get; set; }
        public string InstructionMediumName { get; set; }
        public string StudentPhoto { get; set; }
        public string StudentSignature { get; set; }
        public Int64 FormNo { get; set; }
        public string InstancePartTermName { get; set; }
        public string FacultyName { get; set; }
        public string DisplayName { get; set; }
        public Boolean? IsPhysicallyChallenged { get; set; }
    }

    public class ExamFormPrintDataPaperDetails
    {
        public Int64 ProgInstPartTermId { get; set; }
        public Int64 PRN { get; set; }
        public Int64 ExamEventId { get; set; }
        public string PaperCode { get; set; }
        public string PaperName { get; set; }
        public string TLMAMAT { get; set; }
        public Int64 IndexId { get; set; }

    }
}