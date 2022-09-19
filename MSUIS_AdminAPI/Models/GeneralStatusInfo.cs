using System;
using System.Collections.Generic;


namespace MSUISApi.Models
{

    #region CommonDataModel

    public class StatStudentList
    {
        public Int64 PRN { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string InstancePartTermName { get; set; }
        public string InstituteName { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public decimal DisabilityPercentage { get; set; }

    }
    #endregion


    #region GeneralStatusInfo

    public class GeneralStatusInfo
    {
        // From Angular Side
        public string AcademicYear { get; set; }
        public int ModifiedInstituteId { get; set; }
        public int ModifiedSubjectId { get; set; }

        // GetGeneralStatusInfo
        public int TotalStudents { get; set; }
        public int TotalTeachers { get; set; }
        public int TotalInstitutes { get; set; }
        public int TotalSubjects { get; set; }
        public int TotalPrograms { get; set; }
        public int TotalStudentAchivements { get; set; }
        public int TotalTeacherAchivements { get; set; }
        public List<FacultyList> Facultylist { get; set; }
        public List<CollegeList> Collegelist { get; set; }
        public List<SubjectList> Subjectlist { get; set; }
        //public List<FacultyList> Facultylist { get; set; }
    }

    public class FacultyList
    {
        public int FacultyId { get; set; }
        public string FacultyName { get; set; }
    }

    public class CollegeList
    {
        public int CollegeId { get; set; }
        public string CollegeName { get; set; }
    }

    public class SubjectList
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
    }

    #endregion


    #region ApplicationStatusInfo

    public class ApplicationStatusInfo
    {

        // From Angular Side
        public string AcademicYear { get; set; }
        public int ModifiedInstituteId { get; set; }
        public int ModifiedSubjectId { get; set; }

        // GetApplicationStatusInfo
        public int TotalApplications { get; set; }
        public int ApprovedApplications { get; set; }
        public int TotalPendingApplications { get; set; }
        public int TotalCancelledorRejectedApplications { get; set; }


        // GetApplicationsCancelledOrRejected
        public int CancelledApplicationsByStudent { get; set; }
        public int RejectedApplicationsByFaculty { get; set; }
        public int RejectedApplicationsByAcademics { get; set; }


        // GetApplicationFeesInfo
        public int ApplicationFeesPaid { get; set; }
        public int ApplicationFeesNotPaid { get; set; }


        // GetApplicationInProgressInfo
        public int PendingVerificationByFaculty { get; set; }
        public int PendingStatusByFaculty { get; set; }
        public int PendingVerificationByAcademics { get; set; }
        public int PendingStatusByAcademics { get; set; }

    }

    public class ApplicationStudentInfo
    {

        // From Angular Side
        public string AcademicYear { get; set; }
        public int ModifiedInstituteId { get; set; }
        public int ModifiedSubjectId { get; set; }
        public string ApplicationFlag { get; set; }
        public List<StatStudentList> Studentlist { get; set; }

    }

    #endregion


    #region AdmissionStatusInfo

    public class AdmissionStatusInfo
    {

        // From Angular Side
        public string AcademicYear { get; set; }
        public int ModifiedInstituteId { get; set; }
        public int ModifiedSubjectId { get; set; }


        // GetAdmissionStatusInfo
        public int TotalAdmittedStudents { get; set; }
        public int TotalEligible { get; set; }
        public int TotalProvisionallyEligible { get; set; }
        public int TotalPRNGenerated { get; set; }
        public int TotalPRNNotGenerated { get; set; }
        public int TotalPaperSelected { get; set; }
        public int TotalPaperNotSelected { get; set; }


        // GetAdmissionFeesInfo
        public int StudentFeesPaid { get; set; }
        public int StudentPaidFullyFees { get; set; }
        public int StudentPaidPartialAdmissionFees { get; set; }
        public int StudentNotPaidAdmissionFees { get; set; }


        // GetAdmissionStudentInfo
        public int MaleStudents { get; set; }
        public int FemaleStudents { get; set; }
        public int OtherStudents { get; set; }


        public int NRIStudents { get; set; }
        public int LocalStudents { get; set; }
        public int PhysicallyChallengedStudents { get; set; }

        // Cast wise students
        public int GENERALStudents { get; set; }
        public int EWSStudents { get; set; }
        public int SEBCStudents { get; set; }
        public int STStudents { get; set; }
        public int SCStudents { get; set; }

        // Social Category wise students
        public int ExServiceStudents { get; set; }
        public int ActServiceStudents { get; set; }
        public int WFFStudents { get; set; }
        public int WPTStudents { get; set; }
        public int WSTStudents { get; set; }
        public int DDWStudents { get; set; }
        public int MPAFStudents { get; set; }
        public int EAFStudents { get; set; }
        public int FAFStudents { get; set; }
        public int RTAStudents { get; set; }
        public int KMStudents { get; set; }
        public int TFWStudents { get; set; }
        public int DPStudents { get; set; }
        public int MSUBSQStudents { get; set; }
        public int NAStudents { get; set; }
        public int SQStudents { get; set; }

        // Blood Group Wise Students

        public int APosBloodGroupStudents { get; set; }
        public int ANegBloodGroupStudents { get; set; }
        public int BPosBloodGroupStudents { get; set; }
        public int BNegBloodGroupStudents { get; set; }
        public int OPosBloodGroupStudents { get; set; }
        public int ONegBloodGroupStudents { get; set; }
        public int ABPosBloodGroupStudents { get; set; }
        public int ABNegBloodGroupStudents { get; set; }

    }

    public class AdmissionStudentInfo
    {

        // From Angular Side
        public string AcademicYear { get; set; }
        public int ModifiedInstituteId { get; set; }
        public int ModifiedSubjectId { get; set; }
        public string AdmissionFlag { get; set; }
        public List<StatStudentList> Studentlist { get; set; }

    }

    #endregion


    #region PreExaminationStatusInfo

    public class PreExaminationStatusExamEventDetails
    {
        public int ExamEventId;
        public string ExamEventName;
    }

    public class PreExaminationStatusInfo
    {
        // From Angular Side
        public string AcademicYear { get; set; }
        public int ModifiedInstituteId { get; set; }
        public int ModifiedSubjectId { get; set; }
        public int ModifiedExamEventId { get; set; }


        // GetGeneralStatusInfo
        public int TotalActiveStudents { get; set; }
        public int PaperSelected { get; set; }
        public int NotSelectedPaper { get; set; }
        public int ExamFormsGenereated { get; set; }
        public int ExamFormsNotGenereated { get; set; }
        public int TotalProgrammePartTermExams { get; set; }
        public int TimeTableConfigured { get; set; }
        public int TimeTableNotConfigured { get; set; }
        public int TotalInstitute { get; set; }
        public int TotalExamScheduled { get; set; }
        public int TotalExamNotScheduled { get; set; }
        public int TotalRepeaters { get; set; }
        public int CurrentExamEventId { get; set; }
        public string CurrentExamEventName { get; set; }
        public List<PreExaminationStatusExamEventDetails> ExamEventDetails { get; set; }


    }

    public class PreExaminationTotalProgramExams
    {
        /*Data Elements*/
        public string InstancePartTermName { get; set; }
        public string InstituteName { get; set; }
        public string PartName { get; set; }
        public string StartDateOfExam { get; set; }
        public string EndDateOfExam { get; set; }
        public string InstituteEmail { get; set; }
    }

    public class PreExaminationTotalTimeTable
    {
        public string BranchName { get; set; }
        public string PartTermName { get; set; }
        public string DisplayName { get; set; }
        public string InstituteName { get; set; }
        public string InstituteEmail { get; set; }
    }

    public class PreExaminationTotalInstituteExams
    {
        public string InstituteName { get; set; }
        public string InstituteType { get; set; }
        public string DisplayName { get; set; }
        public string InstituteEmail { get; set; }
    }

    public class PreExaminationStudentInfo
    {
        // From Angular Side
        public string AcademicYear { get; set; }
        public int ModifiedInstituteId { get; set; }
        public int ModifiedSubjectId { get; set; }
        public int ModifiedExamEventId { get; set; }
        public string PreExaminationFlag { get; set; }

        // lists
        public List<StatStudentList> Studentlist { get; set; }
        public List<PreExaminationTotalProgramExams> TotalProgrammeExams { get; set; }
        public List<PreExaminationTotalTimeTable> TotalTimeTable { get; set; }
        public List<PreExaminationTotalInstituteExams> TotalInstituteExams { get; set; }

    }

    #endregion

    #region Student All Details

    //public class ApplicantsAdmissionPersonalDetails
    //{
    //    public Int64 UserName { get; set; }
    //    public String FirstName { get; set; }
    //    public String LastName { get; set; }
    //    public String MiddleName { get; set; }
    //    public string Gender { get; set; }
    //    public string Religion { get; set; }
    //    public string MotherTongue { get; set; }
    //    public String NameAsPerMarksheet { get; set; }
    //    public String EmailId { get; set; }
    //    public String MobileNo { get; set; }
    //    public String SpouseName { get; set; }
    //    public String NameOfFather { get; set; }
    //    public String NameOfMother { get; set; }
    //    public String OccupationOfFather { get; set; }
    //    public String OccupationOfMother { get; set; }
    //    public String FatherMotherContactNo { get; set; }
    //    public String OccupationOfGuardian { get; set; }
    //    public Boolean IsGuardianEbc { get; set; }
    //    public String GuardianAnnualIncome { get; set; }
    //    public String GuardianContactNo { get; set; }
    //    public String GuardianName { get; set; }

    //    public Boolean IsApplicantSignatureVerified { get; set; }
    //    public Boolean IsApplicantPhotoVerified { get; set; }
    //    public Boolean IsPhotoIdDocVerified { get; set; }
    //    public Boolean IsConfirm { get; set; }
    //    public Boolean IsEmp { get; set; }
    //    public Boolean IsLocalToVadodara { get; set; }
    //    public Boolean IsNRI { get; set; }
    //    public Boolean IsSmsPermissionGiven { get; set; }
    //    public String CurrentEmployerName { get; set; }
    //    public String IsMajorThelesamiaStatus { get; set; }

    //    public String OptionalMobileNo { get; set; }
    //    public String OtherReligion { get; set; }
    //    public String FamilyAnnualIncome { get; set; }
    //    public String MaritalStatus { get; set; }
    //    public String BloodGroup { get; set; }
    //    public String PassportDate { get; set; }
    //    public String PassportNumber { get; set; }
    //    public String CommunicationLanguage { get; set; }
    //    public String NameOnAadhar { get; set; }
    //    public String WeightInKgs { get; set; }
    //    public String HeightInCms { get; set; }
    //    public String AadharNumber { get; set; }
    //    public String AadharDoc { get; set; }
    //    public Boolean IsAadharDocVerified { get; set; }
    //    public String DOB { get; set; }
    //    public String DOBDoc { get; set; }
    //    public Boolean IsDOBDocVerified { get; set; }
    //    public Boolean IsCurrentAsPermanent { get; set; }
    //    public String CurrentAddress { get; set; }
    //    public String CurrentCountry { get; set; }
    //    public String CurrentState { get; set; }
    //    public String CurrentDistrict { get; set; }
    //    public String CurrentCityVillage { get; set; }
    //    public String CurrentPincode { get; set; }
    //    public String PermanentAddress { get; set; }
    //    public String PermanentCityVillage { get; set; }
    //    public String PermanentCountry { get; set; }
    //    public String PermanentDistrict { get; set; }
    //    public String PermanentPincode { get; set; }
    //    public String PermanentState { get; set; }
    //    public String ApplicantPhoto { get; set; }
    //    public String ApplicantSignature { get; set; }

    //    public String IsPhysicallyChallenged { get; set; }
    //    public String DisabilityType { get; set; }
    //    public String DisabilityPercentage { get; set; }
    //    public String PCDoc { get; set; }
    //    public Boolean IsPCDocVerified { get; set; }
    //    public Boolean IsPCDocSubmitted { get; set; }

    //    public String EWSDoc { get; set; }
    //    public Boolean IsEWSDocSubmitted { get; set; }
    //    public Boolean IsEWSDocVerified { get; set; }

    //    public String ReservationCategoryDoc { get; set; }
    //    public Boolean IsReservationCategoryDocVerified { get; set; }
    //    public Boolean IsReservationDocSubmitted { get; set; }

    //    public String SocialCategoryDoc { get; set; }
    //    public Boolean IsSocialCategoryDocVerified { get; set; }
    //    public Boolean IsSocialDocSubmitted { get; set; }


    //}
    //public class ApplicantsAdmissionInformationDetails
    //{
    //    public Int64 UserName { get; set; }
    //    public int IndexId { get; set; }
    //    public String InstancePartTermName { get; set; }
    //    public String ProgrammeName { get; set; }
    //    public String FacultyName { get; set; }
    //    public String BranchName { get; set; }
    //    public String ApplicationStatus { get; set; }
    //    public Int64 ApplicationId { get; set; }

    //    public Boolean IsApprovedByFaculty { get; set; }
    //    public string ApprovedByFaculty { get; set; }
    //    public Boolean IsApprovedByAcademic { get; set; }
    //    public string ApprovedByAcademic { get; set; }
    //    public Boolean IsAdmissionFeePaid { get; set; }
    //    public string AdmissionFeePaid { get; set; }
    //    public Boolean IsPRNGenerated { get; set; }
    //    public string PRNGenerated { get; set; }
    //    public Boolean IsPaperSelected { get; set; }
    //    public string PaperSelected { get; set; }
    //    public Boolean IsCancelledByStudent { get; set; }
    //    public string CancelledByStudent { get; set; }
    //    public Boolean IsCancelledByFaculty { get; set; }
    //    public string CancelledByFaculty { get; set; }
    //    public Boolean IsCentrallyAdmission { get; set; }
    //    public string CentrallyAdmission { get; set; }
    //    public Boolean IsCancelledByAcademics { get; set; }
    //    public Boolean IsDualAdmission { get; set; }
    //    public string CancelledByAcademics { get; set; }


    //    public string TotalAdmissionFeePaid { get; set; }
    //    public string TotalAmount { get; set; }
    //    public string AdminRemarkByFaculty { get; set; }
    //    public string AdminRemarkByAcademics { get; set; }
    //    public string StudentCancelledRemark { get; set; }
    //    public string AcademicsCancelledRemark { get; set; }
    //    public string FacultyCancelledRemark { get; set; }
    //    public string CancelType { get; set; }
    //    public string CancelledApplicationStatus { get; set; }
    //    public string AuthorityLetter { get; set; }
    //    public string PassbookDoc { get; set; }
    //    public string RTGSForm { get; set; }
    //    public string ApplicationDoc { get; set; }
    //    public Boolean IsBOBAccount { get; set; }

    //    public string BranchChangeRequest { get; set; }
    //    public string FeeCategoryChangeRequest { get; set; }
    //    public string InstituteChangeRequest { get; set; }
    //    public string CancelChangeRequest { get; set; }

    //    public Int64 ApplicantRegistrationId { get; set; }


    //    public string DateOfApplication { get; set; }
    //    public string EligibilityByAcademics { get; set; }
    //    public string DestinationIncProgInstPartTerm { get; set; }
    //    public string AdmittedOn { get; set; }
    //    public string BlockedOn { get; set; }
    //    public string FacultyCancelledOn { get; set; }
    //    public string AcademicsCancelledOn { get; set; }
    //    public string VerifiedOnFaculty { get; set; }
    //    public string ApprovedOnFaculty { get; set; }
    //    public string ApprovedOnAcademics { get; set; }
    //    public string AdmissionBlockRemark { get; set; }
    //    public string FeeCategoryName { get; set; }
    //    public string EligibilityStatus { get; set; }
    //    public string BlockedRemark { get; set; }
    //    public string AllotmentNo { get; set; }
    //    public string MeritNo { get; set; }
    //    public string AdmissionCommittee { get; set; }

    //    public Boolean IsAdmitted { get; set; }
    //    public Boolean IsAdmissionBlock { get; set; }
    //    public Boolean IsSmsSent { get; set; }
    //    public Boolean IsEmailSent { get; set; }
    //    public Boolean IsAdmissionSecuredElesewhere { get; set; }
    //    public Boolean IsAppliedElseWhere { get; set; }
    //    public Boolean IsVerifiedByFaculty { get; set; }
    //    public Boolean IsVerificationSms { get; set; }
    //    public Boolean IsVerificationEmail { get; set; }
    //    public string IsVerificationSmsOn { get; set; }
    //    public string IsVerificationEmailOn { get; set; }
    //    public Boolean IsBlocked { get; set; }

    //    public string ApprovedProgrammeInstancePartTerm { get; set; }
    //    public string BankName { get; set; }
    //    public string OtherUniversityName { get; set; }
    //    public string OtherUniversityProgrammeName { get; set; }
    //    public string Reason { get; set; }
    //    public string AccountName { get; set; }
    //    public string AccountNumber { get; set; }
    //    public string IFSCCode { get; set; }
    //    public string RequestedOn { get; set; }
    //    public string CancelApprovedOnAcademic { get; set; }
    //    public Decimal AutoDeductAmount { get; set; }

    //    public Boolean IsApprovedByAudit { get; set; }
    //    public Decimal RefundAmountByAudit { get; set; }
    //    public string AuditRemark { get; set; }
    //    public string ProcessedOnAudit { get; set; }
    //    public string CancelApprovedOnFaculty { get; set; }

    //    public Boolean IsApprovedByAccount { get; set; }
    //    public Boolean IsRefundedbyAccount { get; set; }
    //    public string RefundModeByAcademic { get; set; }
    //    public string RefundedTansactionId { get; set; }
    //    public string ProcessedOnAccount { get; set; }
    //    public string AccountRemark { get; set; }
    //    public Decimal RefundAmountByAcademic { get; set; }

    //    public Boolean IsRequestCompleted { get; set; }
    //    public string RequestStatus { get; set; }


    //}
    //public class ApplicantEducationDetails
    //{

    //    public int IndexEduId { get; set; }
    //    public String EligibleDegreeName { get; set; }
    //    public String LanguageName { get; set; }
    //    public String ClassName { get; set; }
    //    public String CityName { get; set; }
    //    public String ExaminationBodyName { get; set; }
    //    public String SpecializationName { get; set; }
    //    public String ExamCertificateNumber { get; set; }
    //    public String AttachDocument { get; set; }

    //    public String ExamPassCity { get; set; }
    //    public String ExamPassMonth { get; set; }
    //    public String ExamPassYear { get; set; }
    //    public String ExamSeatNumber { get; set; }
    //    public String Grade { get; set; }
    //    public String InstituteAttended { get; set; }
    //    public String OtherCity { get; set; }
    //    public String ResultStatus { get; set; }
    //    public String SchoolNo { get; set; }

    //    public Int64 MarkObtained { get; set; }
    //    public Int64 MarkOutOf { get; set; }

    //    public Boolean IsDeclared { get; set; }
    //    public Boolean IsFirstTrial { get; set; }
    //    public Boolean IsLastQualifyingExam { get; set; }
    //    public Boolean IsVerified { get; set; }

    //    public decimal CGPA { get; set; }
    //    public decimal Percentage { get; set; }
    //    public decimal PercentageEquivalenceCGPA { get; set; }











    //    public Boolean IsPRNGenerated { get; set; }
    //    public string PRNGenerated { get; set; }
    //    public Boolean IsPaperSelected { get; set; }
    //    public string PaperSelected { get; set; }


    //}
    //public class ApplicantRequestStatusDetails
    //{

    //    public Int64 Id { get; set; }
    //    public Int64 ApplicationId { get; set; }
    //    public int IndexRequestId { get; set; }
    //    public string RequestType { get; set; }
    //    public Boolean IsRequestCompleted { get; set; }
    //    public string RequestStatus { get; set; }

    //    //public string FeeCategoryChangeRemark { get; set; }
    //    //public string BranchChangeRemark { get; set; }
    //    //public string Remarks { get; set; }
    //    public string OldFeeCategoryName { get; set; }
    //    public string NewFeeCategoryName { get; set; }
    //    public string OldInstancePartTermName { get; set; }
    //    public string NewInstancePartTermName { get; set; }
    //    public string OldInstituteName { get; set; }
    //    public string NewInstituteName { get; set; }
    //    public string OldGroupName { get; set; }
    //    public string NewGroupName { get; set; }
    //    public string BranchName { get; set; }
    //    public string FacultyRemark { get; set; }
    //    public string FacultyName { get; set; }
    //    public string ProgrammeName { get; set; }
    //    public string NameAsPerMarksheet { get; set; }



    //}
    //public class ApplicantPreExaminationDetails
    //{

    //    public Int64 ApplicationId { get; set; }
    //    public int IndexPreExaminationId { get; set; }
    //    public string InstancePartTermName { get; set; }
    //    public string FormNo { get; set; }
    //    public string SeatNumber { get; set; }
    //    public string ResultStatus { get; set; }
    //    public string FacultyName { get; set; }
    //    public string DisplayName { get; set; }
    //    public Boolean IsExamFeesPaid { get; set; }




    //}
    //public class ApplicantList
    //{
    //    public List<ApplicantsAdmissionPersonalDetails> PersonalList { get; set; }
    //    public List<ApplicantsAdmissionInformationDetails> AdmissionList { get; set; }
    //    public Boolean PersonalListflag { get; set; }
    //    public Boolean AdmissionListflag { get; set; }
    //    public Boolean EducationListflag { get; set; }
    //    public Boolean RequestStatusListflag { get; set; }
    //    public Boolean PreExaminationListflag { get; set; }
    //    public List<ApplicantEducationDetails> EducationList { get; set; }
    //    public List<ApplicantRequestStatusDetails> RequestStatusList { get; set; }
    //    public List<ApplicantPreExaminationDetails> PreExaminationList { get; set; }
    //    public List<Programme> ProgrammeList { get; set; }
    //    public Boolean ProgrammeListflag { get; set; }
    //}
    //public class Programme
    //{

    //    public String InstancePartTermName { get; set; }
    //    public Int64 Id { get; set; }
    //    public String InstanceName { get; set; }
    //    public Int64 ProgrammeInstanceId { get; set; }
    //    public List<PartTermInst> PTList { get; set; }

    //}
    //public class PartTermInst
    //{

    //    public String InstancePartTermName { get; set; }
    //    public Int64 Id { get; set; }
    //    public String InstanceName { get; set; }
    //    public Int64 ProgrammeInstanceId { get; set; }
    //    public String CreatedOn { get; set; }
    //    public String EligibilityByFaculty { get; set; }
    //    public String EligibilityByAcademics { get; set; }
    //    public List<Paper> PaperList { get; set; }

    //}
    //public class Paper
    //{

    //    public String PaperName { get; set; }
    //    public Int64 Id { get; set; }
    //    public Int64 ProgrammeInstancePartTermId { get; set; }
    //    public Int64 PaperId { get; set; }


    //}

    #endregion

}