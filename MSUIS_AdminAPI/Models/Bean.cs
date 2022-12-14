using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;

namespace MSUISApi.Models
{
    #region Devanshubhai's Code

    public class updateProvisionalManualParameterValues
    {
        public int? MeritListInstanceId { get; set; }
        public JObject[] data { get; set; }

        public string[] HeadersName { get; set; }
       
    }

    public class MeritListSpecialCategoryConfiguration
    {
        public int? Id { get; set; }
        public int? MeritListInstanceId { get; set; }
        public string MeritListInstance { get; set; }
        public int? GenReservationCategoryId { get; set; }
        public string GenReservationCategory { get; set; }
        public float? PhysicallyHandicapReservedPercentage { get; set; }
        public float? EWSReservedPercentage { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }
    public class ProvisionalMeritListManualParameterValues
    {
        public int? Id { get; set; }
        public int? MeritListInstanceId { get; set; }
        public string MeritListInstance { get; set; }
        public int? ProvisionalMeritListId { get; set; }
        public int? MeritListManualParameterId { get; set; }
        public string DisplayName { get; set; }
        public float Value { get; set; }
        public bool? IsVerified { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        //public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }

    public class ProvisionalMeritListAddOnInformation
    {
        public int? Id { get; set; }
        public int? MeritListInstanceId { get; set; }
        public string MeritListInstance { get; set; }
        public int? ProvisionalMeritListId { get; set; }
        public Int64 ApplicationId { get; set; }
        public int? ProgrammeAddOnCriteriaId { get; set; }
        public string ProgrammeAddOnCriteria { get; set; }
        public float AddOnValue { get; set; }
        public bool? IsVerified { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        //public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }
    public class ProvisionalMeritListEducationDetails
    {
        public int? Id { get; set; }
        public int? MeritListInstanceId { get; set; }
        public string MeritListInstance { get; set; }
        public int? ProvisionalMeritListId { get; set; }
        public Int64 AdmApplicantRegistrationId { get; set; }
        public float Percentage { get; set; }
        public int? NoOfTrial { get; set; }
        public bool? IsVerified { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        //public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }

    public class ProvisionalMeritList
    {
        public int? Id { get; set; }

        public int? MeritListId { get; set; }
        public string MeritList { get; set; }
        public string UserName { get; set; }
        public string ApplocationFormNo { get; set; }
        public string Gender { get; set; }
        public string NameAsPerMarksheet { get; set; }
        public int? ReservationCategoryId { get; set; }
        public string ReservationCategory { get; set; }
        public bool? IsEWS { get; set; }
        public int? SocialCategoryId { get; set; }
        public string SocialCategory { get; set; }
        public bool? IsPhysicallyChallanged { get; set; }
        public bool? LocalToVadodara { get; set; }
        public string LastQualifyingDegreeSchool { get; set; }
        public int? LastQualifyingDegreeSchoolCityId { get; set; }
        public string LastQualifyingDegreeSchoolCity { get; set; }
        public string HSCSchool { get; set; }
        public int? HSCSchoolCityId { get; set; }
        public string HSCSchoolCity { get; set; }
        public string Mobile { get; set; }
        public string EmailId { get; set; }
        //Current Address
        public string Address { get; set; }
        public int? StateId { get; set; }
        public string State { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public float? WeightageValue { get; set; }
        public bool? IsSeatAllocated { get; set; }
        public int? AllocatedLocationId { get; set; }
        public string AllocatedLocation { get; set; }
        public int? AllocatedPreferenceId { get; set; }
        public string AllocatedPreference { get; set; }
        public int? AllocatedGenReservationCategoryId { get; set; }
        public string AllocatedGenReservationCategory { get; set; }
        public List<ProvisionalMeritListManualParameterValues> ProvisionalMeritListManualParameterValuesLst { get; set; }
        public List<ProvisionalMeritListEducationDetails> ProvisionalMeritListEducationDetailsLst { get; set; }
        public List<ProvisionalMeritListAddOnInformation> ProvisionalMeritListAddOnInformationLst { get; set; }
    }

    public class SeatMatrixAllowedCategoryMapping
    {
        public int? Id { get; set; }
        public int? SeatMatrixId { get; set; }
        public string SeatMatrix { get; set; }
        public int? CategoryId { get; set; }
        public string Category { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }
    public class MeritListAddOnQuestionParameterConfiguration
    {
        public int? Id { get; set; }
        public int? MeritListInstanceId { get; set; }
        public int? GroupCodeId { get; set; }
        public string MeritListInstance { get; set; }
        public int? AddOnQuestionId { get; set; }
        public string AddOnQuestion { get; set; }
        public float? Weightage { get; set; }
        public bool? DenominatorType { get; set; }
        public float? DenominatorValue { get; set; }
        // AddOn QuestionId
        public int? DenominatorFieldId { get; set; }
        public string DenominatorField { get; set; }
        public int? Sequence { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

    }

    public class MeritListEducationParameterConfiguration
    {
        public int? Id { get; set; }
        public int? MeritListInstanceId { get; set; }
        public string MeritListInstance { get; set; }
        public string FieldName { get; set; }
        public int? level { get; set; }
        public string LevelName { get; set; }
        public float? Weightage { get; set; }
        public bool? DenominatorType { get; set; }
        public float  DenominatorValue { get; set; }
        public string DenominatorField { get; set; }
        public int? Sequence { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

    }

    public class MeritListManualParameterConfiguration
    {
        public int? Id { get; set; }
        public int? MeritListInstanceId { get; set; }
        public string MeritListInstance { get; set; }
        public string DisplayName { get; set; }
        public float? Weightage { get; set; }
        public float? Value { get; set; }
        public float? Denominator { get; set; }
        public int? Sequence { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

    }

    public class SeatMatrixSpecialCategoryMapping
    {
        public int? Id { get; set; }

        public int? SeatMatrixId { get; set; }
        public string SpecialCategoryFieldName { get; set; }
        public int? SocialCategoryId { get; set; }
        public string SocialCategory { get; set; }
        public float? ReservationPercentage { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }

    public class SeatMatrix
    {
        public int? Id { get; set; }
        public int? MeritListInstanceId { get; set; }
        public string MeritListInstance { get; set; }
        public int? CategoryId { get; set; }
        public string Category { get; set; }
        public int? Location { get; set; }
        public int? AvailableSeats { get; set; }
        public int? TotalSeats { get; set; }
        public int? PreferenceId { get; set; }
        public string Preference { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        //public float? EWSReservationPercentage { get; set; }
        //public float? PhysicallyHandicapReservationPercentage { get; set; }
        public string StrGender { get; set; }

        public List<SeatMatrixPrefernceList> SeatMatrixPrefernceList { get; set; }
        public List<SeatMatrixSpecialCategoryMapping> SeatMatrixCatMapList { get; set; }
        public List<SeatMatrixAllowedCategoryMapping> SeatMatrixAllowedCatList { get; set; }


    }

    public class SeatMatrixPrefernceList
    {
        public bool? IsChecked { get; set; }
        public int? TotalSeats { get; set; }
        public int? Id { get; set; }
        public string Preference { get; set; }
    }

    public class MeritListConfiguration
    {
        public int? Id { get; set; }
        public int? MeritListId { get; set; }
        public string MeritListName { get; set; }
        public string FieldName { get; set; }
        public string DisplayName { get; set; }
        public int Type { get; set; }
        public string FieldTableRef { get; set; }
        public float? Weightage { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public bool? IsDeleted { get; set; }
    }

    public class MeritListInstance
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? ProgrammeInstancePartTermId { get; set; }
        public string ProgrammeInstancePartTerm { get; set; }
        public int? AcademicYearId { get; set; }
        public string AcademicYearCode { get; set; }
        public int? Round { get; set; }
        public int? RefMeritListId { get; set; }
        public string RefMeritList { get; set; }
        public bool? ConfigurationLocked { get; set; }
        public string ConfigurationLockedByTimeStamp { get; set; }
        public int? ConfigurationLockedById { get; set; }
        public bool? SeatMatrixLocked { get; set; }
        public string SeatMatrixByTimeStamp { get; set; }
        public int? SeatMatrixById { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public int? TotalSeats { get; set; }
        public bool? ConsiderPreference { get; set; }
        public int? FacultyId { get; set; }
        public string FacultyName { get; set; }

    }
	public class HallTicket
    {
        public bool Vidhyarthi { get; set; }
        public int ExamMasterId { get; set; }
        public int ProgrammeInstancePartTermId { get; set; }
        public int ProgrammePartTermId { get; set; }
        public int SpecialisationId { get; set; }
        public string ExamEventName { get; set; }
        public int FacultyId { get; set; }
        public string FacultyName { get; set; }
        public Int64 PRN { get; set; }
        public string SeatNumber { get; set; }
        public int AppearanceTypeId { get; set; }
        public string NameAsPerMarksheet { get; set; }
        public string IsPhysicallyChallenged { get; set; }
        public string AppearanceType { get; set; }
        public string Gender { get; set; }
        public string ExamCenter { get; set; }
        public string InwardMode { get; set; }
        public string StudentPhoto { get; set; }
        public string StudentSignature { get; set; }
        public string MandatoryInstructionForOnlineExam { get; set; }
        public string OptionalInstructionForOnlineExam { get; set; }
        public string MandatoryInstructionForOfflineExam { get; set; }
        public string OptionalInstructionForOfflineExam { get; set; }
        public string DyrExamSign { get; set; }
        public string VenueName { get; set; }
        
    }
	
    public class ExamFormBarcodePdfGenerationRequest
    {
        public int? Id { get; set; }
        public int? ExamMasterId { get; set; }
        public string ExamEvent { get; set; }
        public int? FacultyExamMapId { get; set; }
        public string FacultyExamMap { get; set; }
        public int? ProgrammeId { get; set; }

        public string Programme { get; set; }
        public int? ProgrammePartTermId { get; set; }
        public string ProgrammePartTerm { get; set; }
        public string SpecialisationIds { get; set; }
        public string SpecialisationNames { get; set; }
        public string PaperIds { get; set; }
        public string PaperNames { get; set; }
        public string ZipFile { get; set; }
        public Int64 CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public bool? IsGenerated { get; set; }
        public string GeneratedTimeStamp { get; set; }
    }
    public class ExamFormBarcodeList
    {
        public string UId { get; set; }
        public string ExamDate { get; set; }
        public string SeatNo { get; set; }
        public string ExamEventMaster { get; set; }
        public int? ExamMasterId { get; set; }
        public string strPaperId { get; set; } // For Filter
        public string strBlockId { get; set; } // For Filter
        public int? PaperId { get; set; }
        public string PaperName { get; set; }
        public string PaperCode { get; set; }
        public string ProgrammePart { get; set; }
        public string RoomNo { get; set; }
        public int? ExamBlockId { get; set; }
        public string ExamVenue { get; set; }
        public int? ExamVenueId { get; set; }
        public int? FacultyExamMapId { get; set; }
    }
    public class AssignBlocks
    {
        public List<MstSpecialisation> SpecialisationsList { get; set; }
        public List<MstPaper> PaperList { get; set; }
        public List<AvailableExamBlock> ExamBlockList { get; set; }
        public int? ExamMasterId { get; set; }
        public int? FacultyExamMapId { get; set; }
        public int? ExamVenueId { get; set; }
        public int? ProgrammeId { get; set; }
        public int? ProgrammePartTermId { get; set; }
        public string SpecialisationId { get; set; }
        public string PaperId { get; set; }
        public string ExamBlockId { get; set; }
        public Int32 ExamVenueExamCenterId { get; set; }
    }

    public class AvailableExamBlock
    {
        public bool? IsSelected { get; set; }
        public int? Id { get; set; }
        public int? ExamVenueId { get; set; }
        public string ExamVenue { get; set; }
        public string Name { get; set; }
        public string RoomNo { get; set; }
        public int? Capacity { get; set; }
    }

    public class BlockAllocation
    {
        public int? Id { get; set; }
        public string Date { get; set; }
        public int? ExamSlotId { get; set; }
        public string ExamSlot { get; set; }
        public int? ExamBlockId { get; set; }
        public string ExamBlock { get; set; }
        public int TotalCapacity { get; set; }
        public int AvailableCapacity { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public string ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public int? ExamEventId { get; set; }
        public int? FacultyExamMapId { get; set; }
        public int? ProgrammeId { get; set; }

        public string SpecialisationId { get; set; }
        public int? SemesterId { get; set; }
        public int? ProgrammePartTermId { get; set; }
    }

    public class ExamBlocks
    {
        public int? Id { get; set; }
        public int? ExamCenterId { get; set; }
        public string ExamCenter { get; set; }
        public int? ExamVenueId { get; set; }
        public string ExamVenue { get; set; }
        public string Name { get; set; }
        public string RoomNo { get; set; }
        public int? SequenceNo { get; set; }
        public int? TotalBench { get; set; }
        public bool? IsDualCapacityAvailable { get; set; }
        public bool? IncludeInAutoAllocation { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public string ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public bool? IsDeleted { get; set; }

        public Int32 ExamBlockId { get; set; }

        public Int32 ExamBlockId1 { get; set; }

        public string ExamBlockName { get; set; }

        public string ExamBlockName1 { get; set; }

    }
    public class CourseAttachDetachVenue
    {
        public List<CourseAttachVenue> AttachedCourseVenueMapList { get; set; }
        public List<CourseAttachVenue> pendingCourseAttachVenueList { get; set; }

    }
    public class CourseAttachVenue
    {
        public int? Id { get; set; }
        public int? ExamMasterId { get; set; }
        public string ExamMaster { get; set; }
        public int? ExamCenterId { get; set; }
        public int? FacultyExamMapId { get; set; }
        public string FacultyExamMap { get; set; }
        public int? ProgrammeId { get; set; }
        public int? ProgrammePartTermId { get; set; }
        public int? CourseScheduleMapId { get; set; }
        public string CourseScheduleMap { get; set; }
        public int? ExamVenueId { get; set; }
        public string ExamVenue { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public string ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? ToBeAttached { get; set; }
        public int? Capacity { get; set; }
        public int? Sequence { get; set; }
    }

    public class ExamFormMaster
    {
        public int? Id { get; set; }
        public int? ExamMasterId { get; set; }
        public string ExamMaster { get; set; }
        public int? FacultyId { get; set; }
        public String FacultyName { get; set; }
        public int? FacultyExamMapId { get; set; }
        public string ScheduleCode { get; set; }
        public int? ProgrammeId { get; set; }
        public string ProgrammeName { get; set; }

        public int? ProgrammePartTermId { get; set; }
        public string ProgrammePartTermName { get; set; }
        public int? IncStudentAcademicInfoId { get; set; }
        public int? IncPartTermExamFormConfigurationId { get; set; }
        public Int64? ProgInstancePartTermId { get; set; }
        public string ProgInstancePartTerm { get; set; }
        public string PRN { get; set; }
        public int? AppearanceTypeId { get; set; }
        public bool? IsExemptedForExamFee { get; set; }
        public bool? IsExamFeesPaid { get; set; }
        public int? VenueId { get; set; }
        public string SeatNumber { get; set; }
        public bool? IsHallTicketgenerated { get; set; }
        public string ResultStatus { get; set; }
        public bool? IsResultDeclared { get; set; }
        public int? FreshersAvailable { get; set; }
        public int? RepeaterAvailable { get; set; }
        public int? NeverAppeareadAvailable { get; set; }
        public int? FreshersGenerated { get; set; }
        public int? RepeaterGenerated { get; set; }
        public int? NeverAppeareadGenerated { get; set; }
        public int? FreshersInward { get; set; }
        public int? RepeaterInward { get; set; }
        public int? NeverAppeareadInward { get; set; }
        public string FormNo { get; set; }
        public string InwardMode { get; set; }
        public int? InwardBy { get; set; }
        public int? CourseSchduleMapId { get; set; }
        public int? CreatedById { get; set; }
        public string CreationTimeStamp { get; set; }
        public int? FeesPaidById { get; set; }
        public string FeesPaidByTimeStamp { get; set; }
        public int? SeatNumberGeneratedById { get; set; }
        public string SeatNumberGeneratedTimestamp { get; set; }
        public int? BranchId { get; set; }
        public string BranchName { get; set; }
        public string NameAsPerMarksheet { get; set; }
        public string ExceptionMsg { get; set; }
        public string SectionName { get; set; }
        public Int32 SectionMarks { get; set; }
    }

    public class BlankMarkSheets
    {
        public int? ExamMasterId { get; set; }
        public int? InstituteId { get; set; }
        public int? ProgrammeId { get; set; }
        public int? ProgrammePartTermId { get; set; }
        public int? TeachingLearningMethodId { get; set; }
        public int? AssessmentMethodId { get; set; }
        public int? BranchId { get; set; }
        public int? FacultyExamMapId { get; set; }
        public int? PaperId { get; set; }
        public List<MstPaper> PaperList { get; set; }
        public string SectionName { get; set; }
        public Int32 SectionMarks { get; set; }
    }

    public class ExamFormInward
    {
        public int? ExamMasterId { get; set; }
        public int? FacultyExamMapId { get; set; }
        public int? ProgrammeId { get; set; }
        public int? BranchId { get; set; }
        public int? ProgrammePartTermId { get; set; }
        public int? ProgInstancePartTermId { get; set; }
        public string PRNNo { get; set; }

    }

    public class UploadExcel
    {
        public string fileName { get; set; }
        public int ExamMasterId { get; set; }
        public int ProgInstancePartTermId { get; set; }

        public string PRNNo { get; set; }
    }

    public class StudentListForExamForm
    {
        public List<StudentProfile> students { get; set; }
        public int ExamMasterId { get; set; }
        public int ProgInstancePartTermId { get; set; }
    }

    public class ApplicationFormForExaminationDetail
    {
        public Int64? PRN { get; set; }
        public string InstituteName { get; set; }
        public string ExamEventMaster { get; set; }
        public string FormNo { get; set; }
        public string Nationality { get; set; }
        public string StudentName { get; set; }
        public string MotherName { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Taluka { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string DOB { get; set; }
        public bool? IsPhysicallyChallenged { get; set; }
        public string PhysicallyChallenged { get; set; }
        public string SeatNumber { get; set; }
        public string AppearanceType { get; set; }
        public int? ExamMasterId { get; set; }
        public int? ProgInstancePartTermId { get; set; }
        public string ApplicationCategoryCode { get; set; }
        public string ApplicationCategoryName { get; set; }
        public int ApplicationCategoryId { get; set; }
        public string ExamType { get; set; }
        public List<CourseDetails> CourseList { get; set; }

    }

    public class CourseDetails
    {
        public int? Id { get; set; }
        public string PaperName { get; set; }
        public string PaperCode { get; set; }
        public string AssetmentType { get; set; }
        public string AssessmentMethod { get; set; }
        public string PaperDate { get; set; }
        public string PaperTime { get; set; }
        public string ExceptionMsg { get; set; }
    }

    public class ExamFormSeatGeneration
    {
        public Boolean IncludeFresher { get; set; }
        public Boolean IncludeRepeater { get; set; }
        public Boolean IncludeNeverAppeared { get; set; }

        public int? ExamMasterId { get; set; }
        public int? ProgrammePartTermId { get; set; }
        public int? SpecialisationId { get; set; }

        public SeatNo[] SeatNoGeneration { get; set; }
    }

    public class SeatNo
    {
        public string DisplayName { get; set; }
        public string FieldName { get; set; }
        public Boolean IsSelected { get; set; }
        public int SequenceNo { get; set; }
        public int OrderingType { get; set; }
    }

    public class ExamFormDetails
    {
        public int FresherGeneratedCount { get; set; }
        public int RepeaterGeneratedCount { get; set; }
        public int NeverAppearedGeneratedCount { get; set; }

        public int FresherInwardCount { get; set; }
        public int RepeaterInwardCount { get; set; }
        public int NeverAppearedInwardCount { get; set; }

        public int FresherSeatNoGeneratedCount { get; set; }
        public int RepeaterSeatNoGeneratedCount { get; set; }
        public int NeverAppearedSeatNoGeneratedCount { get; set; }
        public int ExamMasterId { get; set; }
        public int ProgrammePartTermId { get; set; }
        public int BranchId { get; set; }
    }

    public class ExamInwardDetails
    {
        public Int64 Id { get; set; }
        public int ExamMasterId { get; set; }
        public Int64 ProgInstancePartTermId { get; set; }
        public int ProgrammePartTermId { get; set; }
        // public string PRN { get; set; }
        public string ExamFormNo { get; set; }
        public string FacultyExamMap { get; set; }
        public string ProgrammeName { get; set; }
        public string ProgrammePartName { get; set; }
        public string ProgrammePartTermName { get; set; }
        public string ProgrammeInstancePartTermName { get; set; }
        public Boolean IsSelected { get; set; }
        public String NameAsPerMarkSheet { get; set; }
        public String OrderId { get; set; }
        public String TransactionId { get; set; }
        public Int32 SpecialisationId { get; set; }
        public String FacultyName { get; set; }
        public String InstancePartTermName { get; set; }
        public String DisplayName { get; set; }
        public String ExamFeeStartDateView { get; set; }
        public String ExamFeeEndDateForStudentView { get; set; }
        public String ExamFeeEndDateForFacultyView { get; set; }
        public Boolean IsFeeOpenForStudent { get; set; }
        public Boolean IsFeeOpenForFaculty { get; set; }
        public Int64 PRN { get; set; }
        public Int64 FormNo { get; set; }
        public Decimal ExamFeeAmount { get; set; }
        public Decimal LateFeesFacultyPower { get; set; }
        public Decimal LateFeesVCPower { get; set; }
        public Decimal ExamFeesToBePaid { get; set; }

    }
    public class FacultyExamMap
    {
        public Int64 Id { get; set; }
        public string ScheduleCode { get; set; }
        public int ExamMasterId { get; set; }
        public string DisplayName { get; set; }
        public int AcademicYearID { get; set; }
        public int FacultyId { get; set; }
        public string FacultyName { get; set; }
        public int InstituteId { get; set; }
        public string InstituteName { get; set; }
        public string LastDateOfFeesPaymentForStudent { get; set; }
        public string LastDateOfFeesPaymentForCollege { get; set; }
        public string StartDateOfExam { get; set; }
        public string EndDateOfExam { get; set; }
        public string ProbableDateOfResult { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public Boolean IsConfirmed { get; set; }
        public int AttachCourseCount { get; set; }
        public int AttchedVenueCount { get; set; }
        public string VenueStatus { get; set; }
        public List<CourseScheduleMap> attachedCourses { get; set; }
    }
    public class CopyExamFeeConfiguration
    {
        public int? ProgrammePartTermIdFrom { get; set; }
        public int? ProgrammeId { get; set; }
        public int? FacultyExamMapId { get; set; }
        public int? ExamMasterId { get; set; }
        public int? UserId { get; set; }
        public List<MstProgrammePartTerm> programmePartTermList { get; set; }
    }
    //public class FacultyExamMap
    //{
    //    public Int64 Id { get; set; }
    //    public string ScheduleCode { get; set; }
    //    public int ExamMasterId { get; set; }
    //    public string DisplayName { get; set; }
    //    public int AcademicYearID { get; set; }
    //    public int FacultyId { get; set; }
    //    public string FacultyName { get; set; }
    //    public int InstituteId { get; set; }
    //    public string LastDateOfFeesPaymentForStudent { get; set; }
    //    public string LastDateOfFeesPaymentForCollege { get; set; }
    //    public string StartDateOfExam { get; set; }
    //    public string EndDateOfExam { get; set; }
    //    public string ProbableDateOfResult { get; set; }
    //    public Boolean IsActive { get; set; }
    //    public Int64 CreatedBy { get; set; }
    //    public DateTime? CreatedOn { get; set; }
    //    public Int64 ModifiedBy { get; set; }
    //    public DateTime? ModifiedOn { get; set; }
    //    public Boolean IsDeleted { get; set; }
    //    public Boolean IsConfirmed { get; set; }
    //    public int AttachCourseCount { get; set; }
    //    public int AttchedVenueCount { get; set; }
    //    public string VenueStatus { get; set; }
    //    public List<CourseScheduleMap> attachedCourses { get; set; }
    //}

    public class ExamCenter
    {
        public Int32 Id { get; set; }
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public Int64 Capacity { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public Boolean IsDeleted { get; set; }
    }

    public class ExamVenue
    {
        public Int32 Id { get; set; }
        public Int32 ProgrammePartTermId { get; set; }
        public Int32 ExamCenterId { get; set; }
        public string ExamCenter { get; set; }
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public string Address { get; set; }
        public Int32 InstituteId { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public Boolean IsDeleted { get; set; }

        public Int32 ExamVenueId { get; set; }

        public string ExamVenueName { get; set; }


    }
    public class ExamFeeHead
    {
        public Int32 Id { get; set; }
        public string ExamFeeHeadName { get; set; }
        public string ExamFeeHeadCode { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsDeleted { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public int? DayRange { get; set; }

    }
    public class ExamEventMaster
    {
        public Int64 Id { get; set; }
        public Int32 AcademicYearId { get; set; }
        public string AcademicYearCode { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string DisplayName { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public Boolean IsClosed { get; set; }
        public Boolean IsOnline { get; set; }

        public string LateFeesFacultyPower { get; set; }

        public string LateFeesVCPower { get; set; }    
        public Int32 ExamMasterId { get; set; }
    }

    public class CourseScheduleMap
    {
        public Int32 Id { get; set; }
        public Int32 ProgrammeInstanceId { get; set; }
        public string ProgrammeInstance { get; set; }
        public Int32 FacultyExamMapId { get; set; }
        public Int32 ProgrammeId { get; set; }
        public string ProgrammeName { get; set; }
        public Int32 BranchId { get; set; }
        public Int32 FacultyId { get; set; }
        public string BranchName { get; set; }
        public Int32 ProgrammePartId { get; set; }
        public string PartName { get; set; }
        public Int32 ProgrammePartTermId { get; set; }
        public string ProgrammePartTermName { get; set; }
        public Int32 AcademicYearId { get; set; }
        public string AcademicYearCode { get; set; }
        public Int32 ProgrammeInstancePartTermId { get; set; }
        public string ProgrammeInstancePartTerm { get; set; }
        public Boolean FresherEligible { get; set; }
        public Boolean FresherProvisionalEligible { get; set; }
        public Boolean RepeaterEligible { get; set; }
        public Boolean RepeaterProvisionalEligible { get; set; }
        public Boolean NeverAppearedEligible { get; set; }
        public Boolean NeverAppearedProvisionalEligible { get; set; }
        public int MaxTLMAMATCount { get; set; }
        public Boolean IsScheduledWithOutExamForm { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public Boolean IsDeleted { get; set; }
        public Boolean ToBeAttached { get; set; }
        public Boolean IsConfirmed { get; set; }
        public Boolean IsPublished { get; set; }
        public Int32 ExamMasterId { get; set; }
        public int AttchedVenueCount { get; set; }
        public string VenueStatus { get; set; }
        public Boolean DeamedInward { get; set; }
        public DateTime? ExamFeeStartDate { get; set; }
        public DateTime? ExamFeeEndDateForStudent { get; set; }
        public DateTime? ExamFeeEndDateforFaculty { get; set; }

        public string ExamFeeStartDateView { get; set; }
        public string ExamFeeEndDateForStudentView { get; set; }
        public string ExamFeeEndDateforFacultyView { get; set; }

    }
    public class ExamFeesConfiguration
    {
        public Int32 Id { get; set; }
        public int? AppearanceTypeId { get; set; }
        public string AppearanceType { get; set; }
        public int? ExamMasterId { get; set; }
        public string ExamMaster { get; set; }
        public float? FeesAmount { get; set; }
        public int? ProgrammePartTermId { get; set; }
        public int? FacultyExamMapId { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public Boolean IsDeleted { get; set; }
    }
    public class ExamFeeConfigurationHeadMap
    {
        public Int32 Id { get; set; }
        public int? ExamFeeConfigurationId { get; set; }
        public int? FacultyExamMapId { get; set; }
        public Int32 ExamFeesHeadId { get; set; }
        public string ExamFeesHead { get; set; }
        public float? FeesAmount { get; set; }
        public Boolean IsEditable { get; set; }
        public Boolean IsDayWiseApplicable { get; set; }
        public float? IncrementedBy { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public Boolean IsDeleted { get; set; }
        public int DayRange { get; set; }
        public int FacultyExamMapIdFrom { get; set; }
        public ExamFeesConfiguration ExamFeeConfiguration { get; set; }
    }

    public class ExamFeeCongfigAdd
    {
        public ExamFeesConfiguration feesConfiguration { get; set; }
        public ExamFeeConfigurationHeadMap[] ExamFeeConfigurationHeadMaps { get; set; }
    }

    public class ExamFeeCongfigGet
    {
        public ExamFeesConfiguration feesConfiguration { get; set; }
        public List<ExamFeeConfigurationHeadMap> ExamFeeConfigurationHeadMaps { get; set; }
    }
    public class ExamSlotMaster
    {
        public Int64 Id { get; set; }

        public DateTime DtStartTime { get; set; }
        public TimeSpan StartTime { get; set; }
        public DateTime DtEndTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public string StartTimeView { get; set; }

        public string EndTimeView { get; set; }

        public string ShiftName { get; set; }

        public string SlotName { get; set; }

        public Boolean IsActive { get; set; }

        public Int64 CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public Int64 ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public Boolean IsDeleted { get; set; }

        public Int64 ExamSlotId { get; set; }

        public DateTime ExamDate { get; set; }
    }

    public class AppearanceTypeMaster
    {
        public Int64 Id { get; set; }
        public string AppearanceType { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public string IsActiveSts { get; set; }
        public Int64 AppearanceTypeId { get; set; }
    }

    public class TimeTableMaster
    {
        public Int32 Id { get; set; }
        public Int32 ExamMasterId { get; set; }

        public string ExamMaster { get; set; }
        public Int32 BranchId { get; set; }
        public string Branch { get; set; }
        public Int32 ProgrammePartId { get; set; }
        public string ProgrammePart { get; set; }
        public Int32 ProgrammePartTermId { get; set; }
        public string PartTermName { get; set; }
        public Int32 ProgramInstancePartTermId { get; set; }
        public Int32 PaperId { get; set; }
        public string PaperName { get; set; }
        public Int32 TeachingLearningMethodId { get; set; }
        public string TeachingLearningMethod { get; set; }
        public Int32 FacultyId { get; set; }
        public string FacultyName { get; set; }
        public Int32 AssetmentMethodId { get; set; }
        public string AssetsmentMethodName { get; set; }
        //public Int32 AssetmentTypeId { get; set; }
        public string AssetmentType { get; set; }
        public string ExamDate { get; set; }
        public Int32 ExamSlotId { get; set; }
        public string ExamSlotName { get; set; }
        public Boolean IsConfirmedByFaculty { get; set; }
        public Boolean IsConfirm { get; set; }
        public Boolean IsPublishedByExamSection { get; set; }
        public string ReasonForUnPublish { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public Boolean IsDeleted { get; set; }
        public int? AcademicYearId { get; set; }
        public string AcademicYearCode { get; set; }
        public int? FacultyExamMapId { get; set; }
        public string FacultyExamMap { get; set; }
        public int? ProgrammeId { get; set; }
        public string Programme { get; set; }
        public int? Sequence { get; set; }

        public bool buttonVisibility { get; set; }

    }
    public class TimeTableConfig
    {
        public Int64 Id { get; set; }
        public Int32 ExamMasterId { get; set; }

        public string ExamMaster { get; set; }
        public Int32 BranchId { get; set; }
        public string Branch { get; set; }
        public Int32 ProgrammePartTermId { get; set; }
        public string PartTermName { get; set; }
        public Int32 ExamSlotId { get; set; }
        public string ExamSlotName { get; set; }
        public Boolean IsConfirm { get; set; }
        public Boolean IsPublish { get; set; }
        public Boolean IsDisplay { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public Boolean IsDeleted { get; set; }
        public int? FacultyExamMapId { get; set; }
        public string FacultyExamMap { get; set; }
        public int? ProgrammeId { get; set; }
        public string Programme { get; set; }
        public List<TimeTableMaster> TTM { get; set; }
        
    }

    //public class ProvisionalMeritList
    //{
    //    public int? Id { get; set; }

    //    public int? MeritListId { get; set; }
    //    public string MeritList { get; set; }
    //    public string UserName { get; set; }
    //    public string ApplocationFormNo { get; set; }
    //    public string Gender { get; set; }
    //    public string NameAsPerMarksheet { get; set; }
    //    public int? ReservationCategoryId { get; set; }
    //    public string ReservationCategory { get; set; }
    //    public bool? IsEWS { get; set; }
    //    public int? SocialCategoryId { get; set; }
    //    public string SocialCategory { get; set; }
    //    public bool? IsPhysicallyChallanged { get; set; }
    //    public bool? LocalToVadodara { get; set; }
    //    public string LastQualifyingDegreeSchool { get; set; }
    //    public int? LastQualifyingDegreeSchoolCityId { get; set; }
    //    public string LastQualifyingDegreeSchoolCity { get; set; }
    //    public string HSCSchool { get; set; }
    //    public int? HSCSchoolCityId { get; set; }
    //    public string HSCSchoolCity { get; set; }
    //    public string Mobile { get; set; }
    //    public string EmailId { get; set; }
    //    //Current Address
    //    public string Address { get; set; }
    //    public int? StateId { get; set; }
    //    public string State { get; set; }
    //    public int? CreatedBy { get; set; }
    //    public DateTime CreatedOn { get; set; }
    //    public int? ModifiedBy { get; set; }
    //    public DateTime ModifiedOn { get; set; }
    //    public bool? IsActive { get; set; }
    //    public bool? IsDeleted { get; set; }
    //}

    //public class SeatMatrixAllowedCategoryMapping
    //{
    //    public int? Id { get; set; }
    //    public int? SeatMatrixId { get; set; }
    //    public string SeatMatrix { get; set; }
    //    public int? CategoryId { get; set; }
    //    public string Category { get; set; }
    //    public int? CreatedBy { get; set; }
    //    public DateTime CreatedOn { get; set; }
    //    public int? ModifiedBy { get; set; }
    //    public DateTime ModifiedOn { get; set; }
    //    public bool? IsActive { get; set; }
    //    public bool? IsDeleted { get; set; }
    //}
    //public class MeritListAddOnQuestionParameterConfiguration
    //{
    //    public int? Id { get; set; }
    //    public int? MeritListInstanceId { get; set; }
    //    public string MeritListInstance { get; set; }
    //    public int? AddOnQuestionId { get; set; }
    //    public string AddOnQuestion { get; set; }
    //    public float? Weightage { get; set; }
    //    public bool? DenominatorType { get; set; }
    //    public float? DenominatorValue { get; set; }
    //    public string DenominatorField { get; set; }
    //    public int? Sequence { get; set; }
    //    public int? CreatedBy { get; set; }
    //    public DateTime CreatedOn { get; set; }
    //    public int? ModifiedBy { get; set; }
    //    public DateTime ModifiedOn { get; set; }
    //    public bool? IsActive { get; set; }
    //    public bool? IsDeleted { get; set; }

    //}

    //public class MeritListEducationParameterConfiguration
    //{
    //    public int? Id { get; set; }
    //    public int? MeritListInstanceId { get; set; }
    //    public string MeritListInstance { get; set; }
    //    public string FieldName { get; set; }
    //    public int? level { get; set; }
    //    public string LevelName { get; set; }
    //    public float? Weightage { get; set; }
    //    public bool? DenominatorType { get; set; }
    //    public float DenominatorValue { get; set; }
    //    public string DenominatorField { get; set; }
    //    public int? Sequence { get; set; }
    //    public int? CreatedBy { get; set; }
    //    public DateTime CreatedOn { get; set; }
    //    public int? ModifiedBy { get; set; }
    //    public DateTime ModifiedOn { get; set; }
    //    public bool? IsActive { get; set; }
    //    public bool? IsDeleted { get; set; }

    //}

    //public class MeritListManualParameterConfiguration
    //{
    //    public int? Id { get; set; }
    //    public int? MeritListInstanceId { get; set; }
    //    public string MeritListInstance { get; set; }
    //    public string DisplayName { get; set; }
    //    public float? Weightage { get; set; }
    //    public float? Value { get; set; }
    //    public float? Denominator { get; set; }
    //    public int? Sequence { get; set; }
    //    public int? CreatedBy { get; set; }
    //    public DateTime CreatedOn { get; set; }
    //    public int? ModifiedBy { get; set; }
    //    public DateTime ModifiedOn { get; set; }
    //    public bool? IsActive { get; set; }
    //    public bool? IsDeleted { get; set; }

    //}

    //public class SeatMatrixSpecialCategoryMapping
    //{
    //    public int? Id { get; set; }

    //    public int? SeatMatrixId { get; set; }
    //    public string SpecialCategoryFieldName { get; set; }
    //    public int? SocialCategoryId { get; set; }
    //    public string SocialCategory { get; set; }
    //    public float? ReservationPercentage { get; set; }
    //    public int? CreatedBy { get; set; }
    //    public DateTime CreatedOn { get; set; }
    //    public int? ModifiedBy { get; set; }
    //    public DateTime ModifiedOn { get; set; }
    //    public bool? IsActive { get; set; }
    //    public bool? IsDeleted { get; set; }
    //}

    //public class SeatMatrix
    //{
    //    public int? Id { get; set; }
    //    public int? MeritListInstanceId { get; set; }
    //    public string MeritListInstance { get; set; }
    //    public int? CategoryId { get; set; }
    //    public string Category { get; set; }
    //    public int? Location { get; set; }
    //    public int? AvailableSeats { get; set; }
    //    public int? TotalSeats { get; set; }
    //    public int? PreferenceId { get; set; }
    //    public string Preference { get; set; }
    //    public int? CreatedBy { get; set; }
    //    public DateTime CreatedOn { get; set; }
    //    public int? ModifiedBy { get; set; }
    //    public DateTime ModifiedOn { get; set; }
    //    public bool? IsActive { get; set; }
    //    public bool? IsDeleted { get; set; }

    //    public float? EWSReservationPercentage { get; set; }
    //    public float? PhysicallyHandicapReservationPercentage { get; set; }

    //    public List<SeatMatrixSpecialCategoryMapping> SeatMatrixCatMapList { get; set; }
    //    public List<SeatMatrixAllowedCategoryMapping> SeatMatrixAllowedCatList { get; set; }


    //}

    //public class MeritListConfiguration
    //{
    //    public int? Id { get; set; }
    //    public int? MeritListId { get; set; }
    //    public string MeritListName { get; set; }
    //    public string FieldName { get; set; }
    //    public string DisplayName { get; set; }
    //    public int Type { get; set; }
    //    public string FieldTableRef { get; set; }
    //    public float? Weightage { get; set; }
    //    public bool? IsActive { get; set; }
    //    public DateTime CreatedOn { get; set; }
    //    public int? CreatedBy { get; set; }
    //    public DateTime ModifiedOn { get; set; }
    //    public int? ModifiedBy { get; set; }
    //    public bool? IsDeleted { get; set; }
    //}

    //public class MeritListInstance
    //{
    //    public int? Id { get; set; }
    //    public string Name { get; set; }
    //    public int? ProgrammeInstancePartTermId { get; set; }
    //    public string ProgrammeInstancePartTerm { get; set; }
    //    public int? AcademicYearId { get; set; }
    //    public string AcademicYearCode { get; set; }
    //    public int? Round { get; set; }
    //    public int? RefMeritListId { get; set; }
    //    public string RefMeritList { get; set; }
    //    public bool? ConfigurationLocked { get; set; }
    //    public string ConfigurationLockedByTimeStamp { get; set; }
    //    public int? ConfigurationLockedById { get; set; }
    //    public bool? SeatMatrixLocked { get; set; }
    //    public string SeatMatrixByTimeStamp { get; set; }
    //    public int? SeatMatrixById { get; set; }
    //    public bool? IsActive { get; set; }
    //    public DateTime CreatedOn { get; set; }
    //    public int? CreatedBy { get; set; }
    //    public DateTime ModifiedOn { get; set; }
    //    public int? ModifiedBy { get; set; }
    //    public bool? IsDeleted { get; set; }
    //    public int? TotalSeats { get; set; }
    //    public bool? ConsiderPreference { get; set; }
    //    public int? FacultyId { get; set; }
    //    public string FacultyName { get; set; }

    //}


    #endregion


    #region MstInstitute
    public class MstInstitute
    {
        public int Id { get; set; }
        public Int32 BranchId { get; set; }
        public Int32 ProgrammePartTermId { get; set; }
        public int FacultyExamId { get; set; }
        public int ExamMasterId { get; set; }
        public String InstituteName { get; set; }
        public int FacultyId { get; set; }
        public String FacultyName { get; set; }
        public string InstituteCode { get; set; }
        public string InstituteType { get; set; }
        public string InstituteAddress { get; set; }
        public string CityName { get; set; }
        public int? Pincode { get; set; }
        public string InstituteContactNo { get; set; }
        public string InstituteFaxNo { get; set; }
        public string InstituteEmail { get; set; }
        public string InstituteUrl { get; set; }
        public string IsActiveSts { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public Int64 IndexId { get; set; }

        public Int64 ProgrammeInstancePartTermId { get; set; }

        public Int32 InstituteId { get; set; }

        public Int32 AcademicYearId { get; set; }
    }
    #endregion

    #region AdminLogin
    public class AdminLogin
    {
        /* public string id { get; set; }
         public string username { get; set; }
         public string password { get; set; }*/

        public string id { get; set; }
        public string userTypeName { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string MobileNo { get; set; }
        public string OTP { get; set; }
        public Boolean IsOTP { get; set; }
    }
    #endregion

    #region GenOccupation
    public class GenOccupation
    {
        public int Id { get; set; }
        public string Occupation { get; set; }
        public string CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
    #endregion

    #region Occupation add with file
    public class Temp
    {
        public int Temp_Id { get; set; }
        public bool IsSelfEmp { get; set; }
        public int Id { get; set; }
        public int Income { get; set; }
        public bool IsEbc { get; set; }
        public string FilePath { get; set; }
    }
    #endregion

    #region AdmApplicantRegistration
    public class AdmApplicantRegistration
    {
        public int Id { get; set; }
        public int OccupationIdOfGuardian { get; set; }
        public bool IsSelfEmp { get; set; }
        public bool IsEbc { get; set; }
        public int GuardianAnnualIncome { get; set; }
        public string Occupation { get; set; }
    }
    #endregion

    #region MstExaminationPattern
    public class MstExaminationPattern
    {
        public int Id { get; set; }
        public string ExaminationPatternName { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public Boolean IsDeleted { get; set; }
        public Int64 IndexId { get; set; }
    }
    #endregion

    #region MstFaculty
    public class MstFaculty
    {
        public Int32 Id { get; set; }
        public String FacultyName { get; set; }
        public String FacultyCode { get; set; }
        public String FacultyAddress { get; set; }
        public string InstituteName { get; set; }
        public Int32 InstituteId { get; set; }
        public string InstituteCode { get; set; }
        public String CityName { get; set; }
        public Int32? Pincode { get; set; }
        public string FacultyContactNo { get; set; }
        public string FacultyFaxNo { get; set; }
        public String FacultyEmail { get; set; }
        public String FacultyUrl { get; set; }
        public Boolean IsActive { get; set; }
        public string IsActiveSts { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public Int64 IndexId { get; set; }

        public Int32 FacultyId { get; set; }

        public Int32 AcademicYearId { get; set; }
    }
    #endregion

    #region BoardOfStudy
    public class BoardOfStudy
    {
        public int Id { get; set; }
        public String BoardName { get; set; }
        public int FacultyId { get; set; }
        public string FacultyName { get; set; }
        public Boolean IsActive { get; set; }
        public string IsActiveSts { get; set; }
        public Boolean IsDeleted { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Int64 IndexId { get; set; }
    }
    #endregion

    #region MstPaper
    public class MstPaper
    {
        public Int64 Id { get; set; }
        public Int64 SubjectId { get; set; }
        public string FacultyName { get; set; }
        public int FacultyId { get; set; }
        public string SubjectName { get; set; }
        public string PaperName { get; set; }
        public string PaperCode { get; set; }
        public Boolean IsCredit { get; set; }
        public Boolean IsChecked { get; set; }
        public int MaxMarks { get; set; }
        public int MinMarks { get; set; }
        //public int? Credits { get; set; }
        public decimal Credits { get; set; }
        public Boolean IsSeparatePassingHead { get; set; }
        public Int32 EvaluationId { get; set; }
        public string EvaluationName { get; set; }
        public Boolean IsActive { get; set; }
        public String IsActiveSts { get; set; }
        public String IsCreditSts { get; set; }
        public String IsSeparatePassingHeadSts { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public Boolean IsDeleted { get; set; }
        public Boolean PaperChecked { get; set; }

        public Boolean PreRequisiteAdded { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public List<PaperTLMMapDetails> PaperTLMMapData { get; set; }
        public Int64 IndexId { get; set; }
        public string AssessmentMethodName { get; set; }
        public string TeachingLearningMethodName { get; set; }
        public string SectionName { get; set; }
        public Int32 SectionMarks { get; set; }

    }
    #endregion

    #region MstProgrammePartTermGroupMap
    public class MstProgrammePartTermGroupMap
    {
        public Int64 Id { get; set; }
        public string GroupName { get; set; }
        public Int64 ProgrammePartTermId { get; set; }
        public Int32 GroupTypeNameId { get; set; }
        public string GroupTypeName { get; set; }
        public string PartTermName { get; set; }
        public string PartTermShortName { get; set; }
        public int CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public Int32 PartId { get; set; }
        public string FacultyName { get; set; }
        public string ProgrammeName { get; set; }
        public string PartName { get; set; }
        public Int64 IndexId { get; set; }


    }
    #endregion

    #region MstEvaluation
    public class MstEvaluation
    {
        public Int32 Id { get; set; }
        public string EvaluationName { get; set; }
        public Boolean IsActive { get; set; }
        public string IsActiveSts { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public Int64 IndexId { get; set; }
    }
    #endregion

    #region MstSubject
    public class MstSubject
    {
        public Int64 Id { get; set; }
        public string SubjectName { get; set; }
        public string FacultyName { get; set; }
        public int FacultyId { get; set; }
        public Boolean IsActive { get; set; }
        public string IsActiveSts { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public Int64 IndexId { get; set; }
        public Int32 InstituteSubjectMapId { get; set; }

        public Int32 SubjectId { get; set; }

        public Boolean SubChecked { get; set; }

        public Int32 InstituteId { get; set; }

        public string InstituteName { get; set; }
        public Int32 ProgrammeId { get; set; }

        public string ProgrammeName { get; set; }
        public Int32 SpecialisationId { get; set; }

        public string BranchName { get; set; }
    }

    #endregion

    #region MstDepartment
    public class MstDepartment
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public int FacultyId { get; set; }
        public string FacultyName { get; set; }
        public string IsActiveSts { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public Boolean IsDeleted { get; set; }
        public Int64 IndexId { get; set; }
    }
    #endregion

    #region FacultyInstituteMap

    public class FacultyInstituteMap
    {
        public int Id { get; set; }
        public int FacultyId { get; set; }
        public string FacultyName { get; set; }
        public int InstituteId { get; set; }
        public string InstituteName { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsDeleted { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Int64 IndexId { get; set; }
    }
    #endregion

    #region BoardOfStudySubjectMap

    public class BoardOfStudySubjectMap
    {
        public int Id { get; set; }
        public int BoardofStudyId { get; set; }
        public string BoardName { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsDeleted { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Int64 IndexId { get; set; }
    }
    #endregion

    #region MstProgramme
    public class MstProgramme
    {
        public int Id { get; set; }
        public string ProgrammeName { get; set; }
        public string ProgrammeCode { get; set; }
        public int FacultyId { get; set; }
        public int InstituteId { get; set; }
        public int SubjectId { get; set; }
        public string FacultyName { get; set; }
        public string InstituteName { get; set; }
        public string IsActiveSts { get; set; }
        public string ProgrammeDescription { get; set; }
        public int ProgrammeLevelId { get; set; }
        public string ProgrammeLevelName { get; set; }
        public string ProgrammeModeName { get; set; }
        public string ProgrammeTypeName { get; set; }
        public int ProgrammeModeId { get; set; }
        public int ProgrammeTypeId { get; set; }
        public int InstructionMediumId { get; set; }
        public string InstructionMediumName { get; set; }
        public string EvaluationName { get; set; }
        public int EvaluationId { get; set; }
        public Boolean IsCBCS { get; set; }
        public Boolean IsSepartePassingHead { get; set; }
        public int MaxMarks { get; set; }
        public int MinMarks { get; set; }
        public Decimal MaxCredits { get; set; }
        public Decimal MinCredits { get; set; }
        public int ProgrammeDuration { get; set; }
        public int ProgrammeValidity { get; set; }
        public int TotalParts { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsDeleted { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public String IsSeparatePassingHeadSts { get; set; }

        public Int32 InstituteProgrammeMapId { get; set; }

        public Int32 ProgrammeId { get; set; }

        public Boolean ProgChecked { get; set; }
        public Int64 IndexId { get; set; }
        public Int32 FacultyExamMapId { get; set; }


      
        public List<MstProgrammePart> TotalPartsList { get; set; }
       


    }
    #endregion

    #region MstProgrammeLevel
    public class MstProgrammeLevel
    {
        public Int64 Id { get; set; }
        public string ProgrammeLevelName { get; set; }
        public string IsActiveSts { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public Int64 IndexId { get; set; }
    }
    #endregion

    #region MstProgrammeMode
    public class MstProgrammeMode
    {
        public Int64 Id { get; set; }
        public string ProgrammeModeName { get; set; }
        public string IsActiveSts { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public Int64 IndexId { get; set; }
    }
    #endregion

    #region MstProgrammeType
    public class MstProgrammeType
    {
        public Int64 Id { get; set; }
        public string ProgrammeTypeName { get; set; }
        public string IsActiveSts { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public Int64 IndexId { get; set; }
    }
    #endregion

    #region MstInstructionMedium
    public class MstInstructionMedium
    {
        public Int64 Id { get; set; }
        public string InstructionMediumName { get; set; }
        public string IsActiveSts { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public Int64 IndexId { get; set; }
    }
    #endregion

    #region ProgramInstance
    public class ProgramInstance
    {
        public Int64 Id { get; set; }
        public int FacultyId { get; set; }
        public string FacultyName { get; set; }
        public string InstituteName { get; set; }
        public Int32 InstituteId { get; set; }
        public int ProgrammeId { get; set; }
        public string ProgrammeName { get; set; }
        public string ProgrammeCode { get; set; } //Added by Mohini
        public int AdmissionYearId { get; set; }
        public int AcademicYearId { get; set; }
        public string AcademicYear { get; set; }
        public string InstanceName { get; set; }
        public Int32 ProgrammeInstanceId { get; set; }
        public int ProgrammeBranchMapId { get; set; }
        public int SpecialisationId { get; set; }
        public string BranchName { get; set; }
        public int Intake { get; set; }
        public Boolean IsActive { get; set; }
        public string IsActiveSts { get; set; }
        public Boolean IsDeleted { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Int64 IndexId { get; set; }
        public string AdmissionRight { get; set; }
    }
    #endregion

    #region AcademicYear
    public class AcademicYear
    {
        public int Id { get; set; }
        public string AcademicYearCode { get; set; }
        public string AdmissionYearCode { get; set; } //Added by Mohini
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public Boolean IsOpen { get; set; }
        public string FromDateView { get; set; }
        public string ToDateView { get; set; }
        public Boolean IsActive { get; set; }
        public string IsActiveSts { get; set; }
        public Boolean IsDeleted { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Int64 IndexId { get; set; }

        public Int32 DestinationAcademicYearId { get; set; }
        public string DestinationAcademicYearCode { get; set; }

        public Int32 SourceAcademicYearId { get; set; }

        public string SourceAcademicYearCode { get; set; }

        public Int32 AcademicYearId { get; set; }
    }
    #endregion

    #region MstProgrammePart
    public class MstProgrammePart
    {
        public int Id { get; set; }
        public int ProgrammeId { get; set; }
        public int FacultyId { get; set; }
        public string FacultyName { get; set; }
        public string ProgrammeName { get; set; }
        public string ExaminationPatternName { get; set; }
        public Int64 ExamPatternId { get; set; }
        public Int64 TotalNoOfDuration{ get; set; }
        public string PartName { get; set; }
        public string PartShortName { get; set; }
        public Int64 NoOfTerms { get; set; }
        public Int64 SequenceNo { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public Int64 IndexId { get; set; }

        public Int32 ProgrammePartId { get; set; }

    }
    #endregion

    #region MstProgrammePartData
    public class MstProgrammePartData
    {

        public string PartName { get; set; }
        public string PartShortName { get; set; }
        public Int64 ExamPatternId { get; set; }
        public Int64 TotalNoOfDuration { get; set; }
        public Int64 SequenceNo { get; set; }
        public Int64 NoOfTerms { get; set; }
      
       
        
        

    }
    #endregion



    #region MstSpecialisation
    public class MstSpecialisation
    {
        public int Id { get; set; }
        public int ProgrammeBranchMapId { get; set; }
        public string BranchName { get; set; }
        public string BranchInstitute { get; set; }

        public Int32 ProgrammeId { get; set; }
        public Int32 ProgrammePartId { get; set; }
        public string ProgrammeCode { get; set; }
        public string ProgrammeName { get; set; }

        public string PartShortName { get; set; }
        public int FacultyId { get; set; }
        public int InstituteId { get; set; }
        public string FacultyName { get; set; }
        public string IsActiveSts { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsDeleted { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean SpecialisationSts { get; set; }
        public Int64 IndexId { get; set; }
        public Int64 ProgrammePartTermId { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }

        public Int32 SpecialisationId { get; set; }

        public Boolean IsForAdmission { get; set; }
        public Int64 ProgrammeInstanceId { get; set; }
    }
    #endregion

    #region ProgrammeBranchMap
    public class ProgrammeBranchMap
    {
        public int Id { get; set; }
        public int FacultyId { get; set; }
        public string FacultyName { get; set; }
        public string BranchName { get; set; }
        public int ProgrammeId { get; set; }
        public int SpecialisationId { get; set; }
        public int SubSpecialisationId { get; set; }
        public string ProgrammeName { get; set; }
        public string SubjectName { get; set; }
        public string SubSpecialisationName { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsDeleted { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Int64 IndexId { get; set; }
    }
    #endregion

    #region MstTeachingLearningAssessmentMap
    public class MstTeachingLearningAssessmentMap
    {
        public int Id { get; set; }
        public int TeachingLearningMethodId { get; set; }
        public string TeachingLearningMethodName { get; set; }
        public int AssessmentMethodId { get; set; }
        public string AssessmentMethodName { get; set; }
        public string IsActiveSts { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public Int64 IndexId { get; set; }
    }
    #endregion

    #region SubSpecialisation
    public class SubSpecialisation
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int SpecialisationId { get; set; }
        public string SubSpecialisationName { get; set; }
        public string SubjectName { get; set; }
        public string FacultyName { get; set; }
        public int FacultyId { get; set; }
        public int BoardOfStudyId { get; set; }
        public string BoardName { get; set; }
        public string BranchName { get; set; }
        public string IsActiveSts { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public Int64 IndexId { get; set; }
    }
    #endregion

    #region ProgrammeInstancePart
    public class ProgrammeInstancePart
    {
        public Int64 Id { get; set; }
        public string ProgrammeName { get; set; }
        public int ProgrammeId { get; set; }
        public int SpecialisationId { get; set; }
        public int InstituteId { get; set; }
        public string BranchName { get; set; }
        public int AcademicYearId { get; set; }
        public int FacultyId { get; set; }
        public string AcademicYearCode { get; set; }
        public string FacultyName { get; set; }
        public Int64 ProgrammeInstanceId { get; set; }
        public Int64 ProgrammeInstancePartId { get; set; }
        public Int64 ProgrammePartId { get; set; }
        public string ProgrammePartName { get; set; }
        public string PartShortName { get; set; }
        public int SequenceNo { get; set; }
        public int ExamPatternId { get; set; }
        public string ExamPatternName { get; set; }
        public string InstancePartName { get; set; }
        public int MaxMarks { get; set; }
        public int MinMarks { get; set; }
        public Decimal MaxCredits { get; set; }
        public Decimal MinCredits { get; set; }
        public Boolean IsSeparatePassingHead { get; set; }
        public string IsSepratePassHeadSts { get; set; }
        public Boolean IsActive { get; set; }
        public string IsActiveSts { get; set; }
        public Boolean IsDeleted { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        //public Int64 ProgrammeInsatnceId { get; set; }
        public string InstanceName { get; set; }
        public Nullable<int> MaxMarks1 { get; set; }
        public Nullable<int> MinMarks1 { get; set; }
        public Int64 IndexId { get; set; }

    }
    #endregion

    #region MstProgrammePartTerm
    public class MstProgrammePartTerm
    {
        public int Id { get; set; }
        public int PartId { get; set; }
        public string PartName { get; set; }
        public string PartShortName { get; set; }
        public int FacultyId { get; set; }
        public string FacultyName { get; set; }
        public int ProgrammeId { get; set; }
        public string ProgrammeName { get; set; }
        public string ProgrammeCode { get; set; }
        public string PartTermName { get; set; }
        public string PartTermShortName { get; set; }
        public int SequenceNo { get; set; }
        public int ProgrammePartId { get; set; }
        
        public Boolean IsActive { get; set; }
        public string IsActiveSts { get; set; }
        public Boolean IsDeleted { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Int64 IndexId { get; set; }
        public Boolean IsSelected { get; set; }

        public Int32 SpecialisationId { get; set; }

        public Int32 ProgrammePartTermId { get; set; }
    }
    #endregion

    #region ProgrammeInstancePartTerm
    public class ProgrammeInstancePartTerm
    {
        public Int64 Id { get; set; }
        public int FacultyId { get; set; }
        public int ProgrammeId { get; set; }
        public int AcademicYearId { get; set; }
        public int AdmissionYearId { get; set; }  //By Mohini on 27Aug2022
        public int ProgrammeBranchMapId { get; set; }
        public Int64 ProgrammeInstanceId { get; set; }
        public Int64 ProgrammeInstancePartId { get; set; }
        public int ProgrammePartId { get; set; }
        public int ProgrammePartTermId { get; set; }
        public int SpecialisationId { get; set; }
        public string BranchName { get; set; }
        public string ProgrammeName { get; set; }
        public string ProgrammeCode { get; set; }
        public string AcademicYearCode { get; set; }
        public string FacultyName { get; set; }
        public string FacultyCode { get; set; }
        public string PartName { get; set; }
        public string PartShortName { get; set; }
        public string ProgrammePartTermName { get; set; }
        public string PartTermShortName { get; set; }
        public int Duration { get; set; }
        public int SequenceNo { get; set; }
        public int ExamPatternId { get; set; }
        public string ExaminationPatternName { get; set; }
        public int MaxMarks { get; set; }
        public int MinMarks { get; set; }
        public int MaxPapers { get; set; }
        public int MinPapers { get; set; }
        public int? Intake { get; set; }
        public Boolean IsSeparatePassingHead { get; set; }
        public Boolean IsForApplication { get; set; }
        public Boolean IsForAdmission { get; set; }
        public Boolean IsCentrallyAdmission { get; set; }
        public string IsSepratePassHeadSts { get; set; }
        public Boolean IsActive { get; set; }
        public string IsActiveSts { get; set; }
        public Boolean IsDeleted { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean Islaunch { get; set; }
        public Int64 LaunchedBy { get; set; }
        public DateTime LaunchedOn { get; set; }
        public Boolean IsAssessmentLaunched { get; set; }
        public Int64 AssessmentLaunchedBy { get; set; }
        public DateTime AssessmentLaunchedOn { get; set; }
        public Boolean IsVerify { get; set; }
        public Int64 VerifiedBy { get; set; }
        public DateTime VerifiedOn { get; set; }
        public string StructureReportRemark { get; set; }
        public Boolean IsApprovedStructureReport { get; set; }
        public string IsApprovedStructureReportSts { get; set; }
        public DateTime ApprovedStructureReportOn { get; set; }
        public string ViewApprovedStructureReportOn { get; set; }          
        public string AssessmentReportRemark { get; set; }
        public Boolean IsApprovedAssessmentReport { get; set; }
        public string IsApprovedAssessmentReportSts { get; set; }
        public DateTime ApprovedAssessmentReportOn { get; set; }
        public string ViewApprovedAssessmentReportOn { get; set; }
        public string PartTermName { get; set; }
        public string InstanceName { get; set; }
        public string InstancePartTermName { get; set; }
        public List<Prerequisite> PaperPrereqList { get; set; }
        public int InstituteId { get; set; }
        public int? MaxMarks1 { get; set; }
        public int? MinMarks1 { get; set; }
        public Int64 IncProgInstancePartTermId { get; set; }
        public Int32 InstitutePartTermMapId { get; set; }
        public Boolean ProgChecked { get; set; }
        public Int64 IndexId { get; set; }

        public Int32 PreferenceId { get; set; }

        public Int32 CodeId { get; set; }

    }
    #endregion

    #region Instruction Medium
    public class InstructionMedium
    {
        public int Id { get; set; }
        public string InstructionMediumName { get; set; }
        public string IsActiveSts { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public Int64 IndexId { get; set; }
    }

    #endregion

    #region Assessment Method

    public class MstAssessmentMethod
    {
        public int Id { get; set; }
        public string AssessmentMethodName { get; set; }
        public string AssessmentMethodCode { get; set; }
        public Int32 PartTermId { get; set; }
        public Int32 SpecialisationId { get; set; }
        public string IsActiveSts { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public Boolean AMCheckedStatus { get; set; }
        public Int64 IndexId { get; set; }
    }

    #endregion

    #region Teaching Learning Method

    public class MstTeachingLearningMethod
    {
        public int Id { get; set; }
        public string TeachingLearningMethodName { get; set; }

        public string TeachingLearningMethodCode { get; set; }
        public Int32 PartTermId { get; set; }

        public Int32 SpecialisationId { get; set; }
        public string IsActiveSts { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public Boolean TLMCheckedStatus { get; set; }
        public Int64 IndexId { get; set; }
    }

    #endregion

    #region Assessment Type List
    public class AssessmentTypeList
    {
        public Boolean CheckBox { get; set; }
        public Boolean Flag { get; set; }
        public Int32 Max { get; set; }
        public Int32 Min { get; set; }
        public string ATName { get; set; }

    }
    #endregion

    #region Paper Teaching Learning Map
    public class MstPaperTeachingLearningMap
    {
        public Int64 Id { get; set; }
        public Int64 PaperId { get; set; }
        public string PaperName { get; set; }
        public int TeachingLearningMethodId { get; set; }
        public List<MstTeachingLearningMethod> TeachingLearningMethodList { get; set; }
        public string TeachingLearningMethodName { get; set; }
        public int AssessmentMethodId { get; set; }
        public List<MstAssessmentMethod> AssessmentMethodList { get; set; }
        public string AssessmentMethodName { get; set; }
        public string AssessmentType { get; set; }
        public List<AssessmentTypeList> AssessType { get; set; }
        public int AssessmentTypeMaxMarks { get; set; }
        public int AssessmentTypeMinMarks { get; set; }
        public int AssessmentMethodMarks { get; set; }
        public int UniversityAssessmentMinMarks { get; set; }
        public int UniversityAssessmentMaxMarks { get; set; }
        public int InternalAssessmentMinMarks { get; set; }
        public int InternalAssessmentMaxMarks { get; set; }
        public Decimal NoOfCredits { get; set; }
        public int NoOfLecturesPerWeek { get; set; }
        public int NoOfHoursPerWeek { get; set; }
        public Boolean IsExemption { get; set; }
        public Boolean IsCarryForwarded { get; set; }
        public string IsActiveSts { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public Boolean TLMCheckedStatus { get; set; }
        public Boolean AMCheckedStatus { get; set; }
        public Int64 IndexId { get; set; }
    }
    #endregion

    #region InstituteProgrammeMap

    public class InstituteProgrammeMap
    {
        public int Id { get; set; }
        public int InstituteId { get; set; }
        public int ProgrammeId { get; set; }
    }

    #endregion
	
	#region Institute Group Change
    public class InstituteGroupChange
    {
        public Int64 ApplicationId { get; set; }
        public Int64 ApplicantUserName { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public Int64 DestinationIncProgInstPartTermId { get; set; }
        public Int32 AdmittedInstituteId { get; set; }        
        public Int32 FeeCategoryId { get; set; }        
        public Int32 PreferenceGroupId { get; set; }        
        public string ApplicantName { get; set; }
        public string ChangeIn { get; set; }
        public Boolean IsFeeCategorySame { get; set; }        
        public Int64 FeeCategoryPartTermMapId { get; set; }
        public string FeeCategoryName { get; set; }
        public string InstituteName { get; set; }
        public string BranchName { get; set; }
        public string ProgrammeName { get; set; }        
        public string GroupName { get; set; }
        public string IsApprovedByFacultySts { get; set; }
        public string IsApprovedByAcademicSts { get; set; }
        public string IsAdmissionFeePaidSts { get; set; }
        public Boolean IsApprovedByFaculty { get; set; }
        public Boolean IsApprovedByAcademic { get; set; }
        public Boolean IsAdmissionFeePaid { get; set; }
        public Boolean IsPRNGenerated { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public Int64 IndexId { get; set; }
    }

    #endregion

    #region New Institute
    public class NewInstitute
    {
        public Int64 ApplicationId { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public Int32 AdmittedInstituteId { get; set; }
        public string InstituteName { get; set; }
    }
    #endregion

    #region New Group
    public class NewGroup
    {
        public Int64 ApplicationId { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public Int32 PreferenceGroupId { get; set; }
        public string GroupName { get; set; }
    }
    #endregion

    #region New Fee Category
    public class NewFeeCategory
    {
        public Int64 FeeCategoryPartTermMapId { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public Int64 DestinationIncProgInstPartTermId { get; set; }
        public Int32 FeeCategoryId { get; set; }
        public string FeeCategoryName { get; set; }
    }
    #endregion

    #region New Institute Group Change
    public class NewInstituteGroupChange
    {
        public Int64 ApplicationId { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }           
        public Int32 OldInstituteId { get; set; }
        public Int32 NewInstituteId { get; set; }        
        public Int32 OldFeeCategoryId { get; set; }
        public Int32? NewFeeCategoryId { get; set; }        
        public Int32? OldPreferenceGroupId { get; set; }
        public Int32? NewPreferenceGroupId { get; set; }
        public Int64 OldFeeCategoryPartTermMapId { get; set; }
        public Int64? NewFeeCategoryPartTermMapId { get; set; }       
        public Boolean IsFeeCategorySame { get; set; }       
        public Boolean IsAdmissionFeePaid { get; set; }
        public string Remarks { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string BankId { get; set; }
        public string IFSCCODE { get; set; }
        public string IFSCShortCode { get; set; }
        public string PassbookPhoto { get; set; }
        public string RTGSForm { get; set; }
        public Boolean IsPRNGenerated { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
    }

    #endregion

    #region Institute Group Change Report
    public class InstituteGroupChangeReport
    {
        public Int64 Application_Form_Number { get; set; }
        public string Instance_Part_Term_Name { get; set; }
        public string Faculty_Name { get; set; }
        public string Name_As_Per_Marksheet { get; set; }
        public string ChangeType { get; set; }
        public string OldInstitute { get; set; }
        public string NewInstitute { get; set; }
        public string OldFeeCategory { get; set; }
        public string NewFeeCategory { get; set; }
        public string OldAllotedGroup { get; set; }
        public string NewAllotedGroup { get; set; }        
        public string Is_AdmissionFee_Category_Same { get; set; }
        public string Admission_Fee_Payment_Status { get; set; }
        public string Is_PRN_Generated_Status { get; set; }
        public string RequestStatus { get; set; }
        public Boolean IsPRNGenerated { get; set; }
        public Int32  InstituteId { get; set; }
        public Int64 IndexId { get; set; }
    }

    #endregion

    #region Bank Details

    public class BankDetails
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

    #region Verify Structure Report 
    public class VerifyStructureReport
    {
        public Int64 Id { get; set; }
        public string StructureReportRemark { get; set; }
        public Boolean IsApprovedStructureReport { get; set; }
        public Int64 ApprovedStructureReportBy { get; set; }
        public DateTime ApprovedStructureReportOn { get; set; }
    }
    #endregion

    #region Verify Assessment Report 
    public class VerifyAssessmentReport
    {
        public Int64 Id { get; set; }
        public string AssessmentReportRemark { get; set; }
        public Boolean IsApprovedAssessmentReport { get; set; }
        public Int64 ApprovedAssessmentReportBy { get; set; }
        public DateTime ApprovedAssessmentReportOn { get; set; }
    }
    #endregion

    //#region InstitutePartTermMap
    //public class InstitutePartTermMap
    //{
    //    public int Id { get; set; }
    //    public int InstituteId { get; set; }
    //    public string InstituteName { get; set; }
    //    public Int64 IncProgInstancePartTermId { get; set; }
    //    public string InstancePartTermName { get; set; }
    //    public Int64 PaperId { get; set; }
    //    public string PaperName { get; set; }
    //    public bool IsActive { get; set; }
    //    public DateTime CreatedOn { get; set; }
    //    public Int64 CreatedBy { get; set; }
    //    public DateTime ModifiedOn { get; set; }
    //    public Int64 ModifiedBy { get; set; }
    //    public bool IsDeleted { get; set; }
    //}
    //#endregion   

    #region Paper TLM MAP Details List
    public class PaperTLMMapDetails
    {
        public Int64 IndexId { get; set; }
        public string PaperName { get; set; }
        public string PaperCode { get; set; }
        public string TeachingLearningMethodName { get; set; }
        public string AssessmentMethodName { get; set; }
        public string AssessmentType { get; set; }
        public int AssessmentTypeMaxMarks { get; set; }
        public int AssessmentTypeMinMarks { get; set; }
        public int AssessmentMethodMarks { get; set; }
        public Decimal NoOfCredits { get; set; }
        public int NoOfHoursPerWeek { get; set; }

    }
    #endregion

    #region Programme Instance Part Term - Group Definition
    public class IncProgInstPartTermGroup
    {
        public Int64 Id { get; set; }
        public Int32 ProgrammeId { get; set; }
        public Int32 AcademicYearId { get; set; }
        public Int64 PaperId { get; set; }
        public string PaperName { get; set; }
        public string AcademicYearCode { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public Int64 ProgrammeInstancePartId { get; set; }
        public Int64 MstProgInstPartTermGroupMapId { get; set; }
        public Int32 MstProgInstPartTermId { get; set; }
        public Int32 InstituteId { get; set; }
        public Int32 SpecialisationId { get; set; }
        public Int32 FacultyId { get; set; }
        public Int64 ParentGroupId { get; set; }
        public string InstancePartTermName { get; set; }
        public Int64 GroupId { get; set; }
        public Int64 MPTGroupId { get; set; }
        public string GroupName { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean GroupCheckedSts { get; set; }
        public Boolean PaperCheckedSts { get; set; }
        public Boolean SeparatePassingHead { get; set; }
        public Boolean ContainsGroup { get; set; }
        public Boolean IsSubGroup { get; set; }
        public string IsSubGroupSts { get; set; }
        public string IsActiveSts { get; set; }
        public string IsCreditSts { get; set; }
        public string IsSeparatePassingHeadSts { get; set; }
        public string HasSubGroupSts { get; set; }
        public int MinPapers { get; set; }
        public int MaxPapers { get; set; }
        public int MinSubGroups { get; set; }
        public int MaxSubGroups { get; set; }
        public Decimal MinCredits { get; set; }
        public Decimal MaxCredits { get; set; }
        public int MaxMarks { get; set; }
        public int MinMarks { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public List<IncProgInstPartTermGroup> GroupList { get; set; }
        public List<IncProgInstPartTermGroup> PaperList { get; set; }
        public String ItemType { get; set; }
        public String FacultyName { get; set; }
        public String ProgrammeName { get; set; }
        public String ProgrammeCode { get; set; }
        public String BranchName { get; set; }
        public Int32 SubChildCount { get; set; }
        public IEnumerable<PaperReport> PaperListdata { get; set; }
        public Boolean IsPaper { get; set; }
        public Int32 Count { get; set; }
        public Int64 PRN { get; set; }
    }
    #endregion    
    #region Programme Instance PartTerm - Paper Report
    public class PaperReport
    {
        public Int64 Id { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public String InstancePartTermName { get; set; }
        public String ItemType { get; set; }
        public Int64 PaperId { get; set; }
        public String PaperName { get; set; }
        public String PaperCode { get; set; }
        public Int32 MaxMarks { get; set; }
        public Int32 MinMarks { get; set; }
        public Int32 EvaluationId { get; set; }
        public String EvaluationName { get; set; }
        public Decimal Credits { get; set; }
        public Int64 GroupId { get; set; }
        public String GroupName { get; set; }
        public Boolean IsActive { get; set; }
        public String IsActiveSts { get; set; }
        public Boolean IsDeleted { get; set; }
        public Int64 CreatedBy { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        //public List<AssessmentTypeListdata> ATList { get; set; }
        public Boolean IsPaper { get; set; }

    }
    #endregion    
    #region ProgrammeInstance Part Term - Paper Attach
    public class IncProgInstPartTermPaperMap
    {
        public Int64 Id { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public string PartTermName { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string EvaluationName { get; set; }
        public int EvaluationId { get; set; }
        public Int64 PaperId { get; set; }
        public string PaperName { get; set; }
        public Int64 GroupId { get; set; }
        public string GroupName { get; set; }
        public string PaperCode { get; set; }
        public int FacultyId { get; set; }
        public string FacultyName { get; set; }
        public int ProgrammeId { get; set; }
        public string ProgrammeName { get; set; }
        public int SpecialisationId { get; set; }
        public string BranchName { get; set; }
        public Int64 ProgrammeInstancePartId { get; set; }
        public string PartName { get; set; }
        public int ProgrammePartTermId { get; set; }
        public Int64 ProgrammeInstanceId { get; set; }
        public int AcademicYearId { get; set; }
        public string AcademicYearCode { get; set; }
        public Boolean IsActive { get; set; }
        public string IsActiveSts { get; set; }
        public string IsCreditSts { get; set; }
        public string IsSeparatePassingHeadSts { get; set; }
        public Decimal Credits { get; set; }
        public int MaxMarks { get; set; }
        public int MinMarks { get; set; }
        public int NoOfLecturesPerWeek { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public String InstancePartTermName { get; set; }

        public Int64 IncProgInstancePartTermId { get; set; }

        public string SourcePaperNameCode { get; set; }

        public Int64 SourcePaperId { get; set; }

        public string DestinationPaperNameCode { get; set; }

        public Int64 DestinationPaperId { get; set; }

    }
    #endregion

    //Jay Model Start
    #region PaperATDetail
    public class PaperATDetail
    {

        public String AssessmentType { get; set; }
        public Int32 AssessmentTypeMaxMarks { get; set; }
        public Int32 AssessmentTypeMinMarks { get; set; }
        public Int64 PaperId { get; set; }
        public Int32 AssessmentMethodId { get; set; }
        public Int32 TeachingLearningMethodId { get; set; }
    }
    #endregion
    #region PaperDetail
    public class PaperDetail
    {
        public Int32 AssessmentMethodId { get; set; }
        public Int32 TeachingLearningMethodId { get; set; }
        public Int64 PaperId { get; set; }
        public Int32 AssessmentMethodMarks { get; set; }
        public decimal NoOfCredits { get; set; }
        public Int32 NoOfHoursPerWeek { get; set; }
        public String PaperName { get; set; }
        public String PaperCode { get; set; }
        public String AssessmentMethodName { get; set; }
        public String TeachingLearningMethodName { get; set; }
        public List<PaperATDetail> ATdata { get; set; }
    }
    #endregion
    #region PaperPT
    public class PaperPT
    {
        public Int64 Id { get; set; }
        public Int64 InstitutePartTermPaperMapId { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public String ItemType { get; set; }
        public Int64 PaperId { get; set; }
        public String PaperName { get; set; }
        public String PaperCode { get; set; }
        public Int32 MaxMarks { get; set; }
        public Int32 MinMarks { get; set; }
        public Int32 EvaluationId { get; set; }
        public String EvaluationName { get; set; }
        public Decimal Credits { get; set; }
        public Int64 GroupId { get; set; }
        public String GroupName { get; set; }
        public List<PaperDetail> ATDetail { get; set; }
        public Boolean IsPaper { get; set; }
        public Boolean PaperCheckSts { get; set; }
        public Boolean PaperCheck { get; set; }

    }
    #endregion
    #region GroupPT
    public class GroupPT
    {
        public Int64 Id { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public Int64 ParentGroupId { get; set; }
        public Int32 Count { get; set; }
        public string InstancePartTermName { get; set; }
        public string GroupName { get; set; }
        public Boolean ContainsGroup { get; set; }
        public int MinPapers { get; set; }
        public int MaxPapers { get; set; }
        public int MinSubGroups { get; set; }
        public int MaxSubGroups { get; set; }
        public String ItemType { get; set; }
        public String FacultyName { get; set; }
        public String ProgrammeName { get; set; }
        public String BranchName { get; set; }
        public Int32 SubChildCount { get; set; }
        public Boolean IsPaper { get; set; }
        public List<PaperPT> PaperList { get; set; }
        public List<GroupPT> Children { get; set; }
        public Boolean PreferanceGroupCheck { get; set; }
        public Int64 IncPreferanceGroupMapId { get; set; }
    }
    #endregion
    #region PartTerm
    public class PartTerm
    {
        public Int64 Id { get; set; }
        public Int64 PRN { get; set; }
        public string text { get; set; }
        public string InstancePartTermName { get; set; }
        public String FacultyName { get; set; }
        public String ProgrammeName { get; set; }
        public String BranchName { get; set; }
        public String AcademicYearCode { get; set; }
        public List<GroupPT> GroupList { get; set; }
        public List<GroupSelect> children { get; set; }
        public Boolean IsSelected { get; set; }
        public Boolean IsPaperSelected { get; set; }
        public Boolean CompleteStatusFlag1 { get; set; }
        public Boolean CompleteStatusFlag2 { get; set; }
        public Boolean IsCompleted { get; set; }
        public Boolean PaperCompleteStatus { get; set; }
        public Boolean GroupCompleteStatus { get; set; }
        public Boolean IsCompletedSts { get; set; }
        public Boolean AssessmentCompleteStatus { get; set; }
        public Boolean IsCompletedAssessment { get; set; }
        public Boolean TLMcheck { get; set; }
        public state state { get; set; }
    }
    #endregion

    #region GroupPTDX
    public class GroupPTDX
    {
        public Int64 Id { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public Int64 ParentGroupId { get; set; }
        public string InstancePartTermName { get; set; }
        public string GroupName { get; set; }
        public Boolean ContainsGroup { get; set; }
        public int MinPapers { get; set; }
        public int MaxPapers { get; set; }
        public int MinSubGroups { get; set; }
        public int MaxSubGroups { get; set; }
        public String ItemType { get; set; }
        public String FacultyName { get; set; }
        public String ProgrammeName { get; set; }
        public String BranchName { get; set; }
        public Int32 SubChildCount { get; set; }
        public Boolean IsPaper { get; set; }
        public Boolean expanded { get; set; }
        public List<GroupPTDX> items { get; set; }


        public Int64 PaperPTMapId { get; set; }
        public Int64 PaperId { get; set; }
        public String PaperName { get; set; }
        public String PaperCode { get; set; }
        public Int32 MaxMarks { get; set; }
        public Int32 MinMarks { get; set; }
        public Int32 EvaluationId { get; set; }
        public String EvaluationName { get; set; }
        public Decimal Credits { get; set; }
        public Int64 GroupId { get; set; }
        public List<PaperDetail> ATDetail { get; set; }
    }
    #endregion
    #region PartTermDX
    public class PartTermDX
    {
        public Int64 Id { get; set; }
        public string InstancePartTermName { get; set; }
        public String FacultyName { get; set; }
        public String ProgrammeName { get; set; }
        public String BranchName { get; set; }
        public String AcademicYearCode { get; set; }
        public Boolean IsPartTerm { get; set; }
        public List<GroupPTDX> items { get; set; }
        public Boolean expanded { get; set; }
        public String ItemType { get; set; }
        public Boolean IsCompleted { get; set; }
        public Boolean PaperCompleteStatus { get; set; }
        public Boolean GroupCompleteStatus { get; set; }
        public Boolean AssessmentCompleteStatus { get; set; }
        public Boolean IsCompletedAssessment { get; set; }
        public Boolean TLMcheck { get; set; }

    }
    #endregion

    #region PapercheckList
    public class PapercheckList
    {
        public Int64 Id { get; set; }
        public String PaperName { get; set; }
        public Int32 Found { get; set; }
        public Int32 TotalP { get; set; }
        public Int32 Minimum { get; set; }
        public Int32 Maximum { get; set; }


    }
    #endregion

    #region GroupSelect
    public class GroupSelect
    {
        public Int64 Id { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public Int64 ParentGroupId { get; set; }
        public string InstancePartTermName { get; set; }
        public string text { get; set; }
        public Boolean ContainsGroup { get; set; }
        public int MinPapers { get; set; }
        public int MaxPapers { get; set; }
        public int MinSubGroups { get; set; }
        public int MaxSubGroups { get; set; }
        public String ItemType { get; set; }
        public String FacultyName { get; set; }
        public String ProgrammeName { get; set; }
        public String BranchName { get; set; }
        public String icon { get; set; }
        public Int32 SubChildCount { get; set; }
        public Boolean IsPaper { get; set; }
        public Boolean expanded { get; set; }
        public List<GroupSelect> children { get; set; }
        public Boolean PreferanceGroupCheck { get; set; }
        public Int64 IncPreferanceGroupMapId { get; set; }
        public Int64 PaperPTMapId { get; set; }
        public Int64 PaperId { get; set; }
        public String PaperName { get; set; }
        public String PaperCode { get; set; }
        public Int32 MaxMarks { get; set; }
        public Int32 Count { get; set; }
        public Int32 MinMarks { get; set; }
        public Int32 EvaluationId { get; set; }
        public String EvaluationName { get; set; }
        public Decimal Credits { get; set; }
        public Int64 GroupId { get; set; }
        public List<PaperDetail> ATDetail { get; set; }
        public state state { get; set; }
        public Boolean PaperCheck { get; set; }
    }
    #endregion
    #region GPreportdata
    public class GPreportdata
    {
        //public List<GroupSelect> Parent { get; set; }
        public List<GroupSelect> Parent1 { get; set; }
        public PartTerm Parent { get; set; }
        public List<GroupPT> MinMaxData { get; set; }
        public List<GroupSelect> FinalArray { get; set; }
        public Int64 PRN { get; set; }
        public Int64 StudentAcademicId { get; set; }
        public String StudentName { get; set; }
        public Int32 PreferenceId { get; set; }
        public Int32 InstituteId { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public String ProgrammeInstancePartTermName { get; set; }

    }
    #endregion
    #region state
    public class state
    {
        public Boolean opened { get; set; }
        public Boolean disabled { get; set; }
        public Boolean selected { get; set; }

    }
    #endregion

    #region StudentList
    public class StudentList
    {

        public Int64 Id { get; set; }
        public Int64 PaperId { get; set; }
        public Int64 PRN { get; set; }
        public String InstancePartTermName { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public Int32 AcademicYearId { get; set; }
        public Int32 Index { get; set; }
        public Int32 InstituteId { get; set; }
        public Int64 StudentAdmissionId { get; set; }
        public Int64 StudentAcademicId { get; set; }
        public String NameAsPerMarksheet { get; set; }
        public String EmailId { get; set; }
        public String MobileNo{ get; set; }
        public String PaperName { get; set; }
        public String PaperCode { get; set; }
        public String SeatNumber { get; set; }
        public Int64 IndexId { get; set; }
        public Boolean IsPaperSelected { get; set; }
        public List<PartTerm> PTList { get; set; }
        public Int64 PartTerm1 { get; set; }
        public String PartTermName1 { get; set; }
        public Boolean PaperSelected1 { get; set; }
        public Int64 PartTerm2 { get; set; }
        public String PartTermName2 { get; set; }
        public Boolean PaperSelected2 { get; set; }
        public Int64 PartTerm3 { get; set; }
        public String PartTermName3 { get; set; }
        public Boolean PaperSelected3 { get; set; }
    }
    #endregion
    
    #region StudentRequestList
    public class StudentRequestList
    {
        public Int64 IndexId { get; set; }        
        public Int64 PRN { get; set; }        
        public Int32 ProgrammeId { get; set; }
        public String StudentName { get; set; }
        public String ExistingRecord { get; set; }
        public String ChangeRecord { get; set; }
        public String AttachDocument { get; set; }
        public String ReasonOfRequest { get; set; }
        public String Request { get; set; }
        public String Status { get; set; }
        public Int32 RequestId { get; set; }
        public DateTime RequestedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public String RequestedOnView { get; set; }
        public String ModifiedOnView { get; set; }
        public String RemarksByFaculty { get; set; }
        public String RemarksByAcademic { get; set; }
    }
    #endregion
    
    #region ExamEventDateWiseReport
    public class ExamEventDateWiseReport 
    {
        public Int32 ExamMasterId { get; set; }
        public string ExamMaster { get; set; }
        public string PaperCode { get; set; }
        public string PaperName { get; set; }
        public string ExamDate { get; set; }
        public string EndDate { get; set; }
        public string SeatNumber { get; set; }
        public Int64 PRN { get; set; }
        public Int64 ProgInstancePartTermId { get; set; }
        public string S1 { get; set; }
        public string S2 { get; set; }
        public string S3 { get; set; }
        public string E1 { get; set; }
        public string E2 { get; set; }
        public string E3 { get; set; }
        public string StudentPRN { get; set; }
        public string ExamPaperDate { get; set; }
        public Int64 StudentCode { get; set; }
        public string TimeSlotCode { get; set; }
        public Int64 IndexId { get; set; }
    }
    #endregion

    #region Event Closure

    public class EventClosure
    {
        public Int64 IndexId { get; set; }
        public Int32 CourseScheduleMapId { get; set; }
        public string PartTermName { get; set; }
        public string InstancePartTermName { get; set; }
        public string ProgrammeName { get; set; }
        public string BranchName { get; set; }
        public Int64 SeatNumberCount { get; set; }
        public Int64 ResultDeclaredCount { get; set; }
        public Int32 ExamMasterId { get; set; }
        public Int32 FacultyExamId { get; set; }
        public bool IsClosed { get; set; }

    }

    #endregion

    #region IncInstitutePartTermPaperMap
    public class IncInstitutePartTermPaperMap
    {

        public Int64 Id { get; set; }
        public Int64 PartTermPaperMapId { get; set; }
        public Int64 ProgInstPartTermId { get; set; }
        public Int32 InstituteId { get; set; }
        public String InstituteName { get; set; }
        public String InstancePartTermName { get; set; }
        public String PaperName { get; set; }
        public String PaperCode { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public Boolean IsActive { get; set; }
        public List<PaperPT> PaperList { get; set; }
    }
    #endregion
    #region IncPreferanceGroupMap
    public class IncPreferanceGroupMap
    {
        public Int64 Id { get; set; }
        public Int64 GroupId { get; set; }
        public Int64 ParentGroupId { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public Int32 PreferenceId { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public String GroupName { get; set; }
        public String GroupNamedata { get; set; }
        public String InstancePartTermName { get; set; }
        public int MinPapers { get; set; }
        public int MaxPapers { get; set; }
        public int MinSubGroups { get; set; }
        public int MaxSubGroups { get; set; }
        public Boolean ContainsGroup { get; set; }
        public String FacultyName { get; set; }
        public String ProgrammeName { get; set; }
        public String BranchName { get; set; }
    }
    #endregion


    #region Prerequisite
    public class Prerequisite
    {
        public Int64 Id { get; set; }
        public Int64 SourcePartTermId { get; set; }
        public String SourcePartTermName { get; set; }
        public Int64 DestinationPartTermId { get; set; }
        public String DestinationPartTermName { get; set; }
        public Int64 SourceIncPaperId { get; set; }
        public Int64 DestinationIncPaperId { get; set; }
        public Int64 SourcePaperId { get; set; }
        public String SourcePaperName { get; set; }
        public String SourcePaperCode { get; set; }
        public Int64 DestinationPaperId { get; set; }
        public String DestinationPaperName { get; set; }
        public String DestinationPaperCode { get; set; }
        public Boolean DestinationPaperSelected { get; set; }
        public Boolean PaperSelectedDisabled { get; set; }
        public Int32 PrerequisiteLableId { get; set; }
        public Int32 PrerequisiteTypeId { get; set; }
        public String PrerequisiteLableName { get; set; }
        public String PrerequisiteTypeName { get; set; }
        public String PreRequisiteTypeNameCurrent { get; set; }
        public Int32 Minimum { get; set; }
        public Int32 Maximum { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
    }
    #endregion


    #region PrerequisitePaperMap
    public class PrerequisitePaperMap
    {
        public Int64 Id { get; set; }
        public Int64 InstitutePartTermPaperMapId { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public Int32 ProgrammePartTermId { get; set; }
        public Int64 SourcePartTermId { get; set; }
        public String SourcePartTermName { get; set; }
        public Int64 DestinationPartTermId { get; set; }
        public String DestinationPartTermName { get; set; }
        public Int64 SourcePaperId { get; set; }
        public String SourcePaperName { get; set; }
        public String SourcePaperCode { get; set; }
        public Int64 DestinationPaperId { get; set; }
        public String DestinationPaperName { get; set; }
        public String DestinationPaperCode { get; set; }
        public Int32 PrerequisiteLableId { get; set; }
        public Int32 PrerequisiteTypeId { get; set; }
        public Int32 Minimum { get; set; }
        public Int32 Maximum { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
    }
    #endregion
    //#region PapercheckList
    //public class PapercheckList
    //{
    //    public Int64 Id { get; set; }
    //    public String PaperName { get; set; }
    //    public Int32 Found { get; set; }
    //    public Int32 TotalP { get; set; }
    //    public Int32 Minimum { get; set; }
    //    public Int32 Maximum { get; set; }


    //}
    //#endregion

    //For Branch Change   -Jay Dated 07102021
    #region BranchChange
    public class BranchChange
    {
    
        public Int64 DestinationIncProgInstPartTermId { get; set; }
        public Int32 SpecialisationId { get; set; }
        public Int64 ApplicationId { get; set; }
        public String NameAsPerMarksheet { get; set; }
        public String FacultyName { get; set; }
        public String ProgrammeName { get; set; }
        public String BranchName { get; set; }
        public String InstancePartTermName { get; set; }
        public String ApplicationStatus { get; set; }
        public String EligibilityStatus { get; set; }
        public String FeeCategoryName { get; set; }
        public String InstituteName { get; set; }
        public String GroupName { get; set; }
        public Double TotalAmount { get; set; }
        public Boolean IsVerificationSms { get; set; }
        public Boolean IsVerificationEmail { get; set; }
        public Boolean IsPRNGenerated { get; set; }
        public Boolean IsAdmissionFeePaid { get; set; }
        public Boolean UserConfirmAfterFee { get; set; }
        public Boolean SameFeeCategory { get; set; }
        public Double AmountPaid { get; set; }
        public String BranchChangeRemark { get; set; }
        public Int64 FeeCategoryPartTermMapId { get; set; }
        public Int32 FeeCategoryId { get; set; }
        public Int32 BankId { get; set; }
        public DateTime IsVerificationEmailOn { get; set; }
        public DateTime IsVerificationSmsOn { get; set; }
        public String IsVerificationEmailOnView { get; set; }
        public String IsVerificationSmsOnView { get; set; }
        public Boolean IsBOBAccount { get; set; }
        public String AccountName { get; set; }
        public String AccountNumber { get; set; }
        public String IFSCCode { get; set; }
        public String PassbookDoc { get; set; }
        public String RTGSForm { get; set; }
        public String ShortIFSCCode { get; set; }

    }
    #endregion


    #region InstitutePartTermPaperMap
    public class InstitutePartTermPaperMap
    {

        public Int64 Id { get; set; }
        public Int32 InstituteId { get; set; }
        public Int64 PaperId { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public String InstancePartTermName { get; set; }
        public String PaperName { get; set; }
        public String InstituteName { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public Boolean IsActive { get; set; }

    }
    #endregion



    #region StudentPartTermPaper
    public class StudentPartTermPaper
    {


        public Int32 Index { get; set; }
        public Int64 PRN { get; set; }

        public String SeatNumber { get; set; }
        public String PaperCode { get; set; }
        public String PaperName { get; set; }
        public String EmailId { get; set; }
        public String MobileNo { get; set; }
        public String FullName { get; set; }
        public String InstancePartTermName { get; set; }
        public String FacultyName { get; set; }


    }
    #endregion



    public class PublishHallTicket
    {
        public Int32 Id { get; set; }
        public String PartTermName { get; set; }
        public String PartName { get; set; }
        public String ProgrammeName { get; set; }
        public Int32 AttachedVenueCount { get; set; }
        public Boolean IsConfirmed { get; set; }
        public Boolean IsPublished { get; set; }
        public Int32 ExamFormGenratedCount { get; set; }
        public Int32 ExamformInwardCount { get; set; }
        public Int32 ExamformSeatNoGenCount { get; set; }
        public Int32 ExamformVenueAttachedCount { get; set; }
        public Int32 ProgrammePartTermId { get; set; }
        public String MandatoryInstructionForOfflineExam { get; set; }
        public String MandatoryInstructionForOnlineExam { get; set; }
        public String OptionalInstructionForOfflineExam { get; set; }
        public String OptionalInstructionForOnlineExam { get; set; }
        public String DyrExamSign { get; set; }
        public String DyrExamSignDisplay { get; set; }
        public Int32 ExamMasterId { get; set; }
        public Int32 FacultyExamMapId { get; set; }
        public Boolean IsExamModeOnline { get; set; }
        public Boolean HallTicketPublished { get; set; }
    }

    //Jay Model End



    #region Student Transfer Request

    public class StudentTransferRequest
    {
        public Int64 Id { get; set; }
        public Int64 StudentPRN { get; set; }
        public Int32 FacultyId { get; set; }
        public string FacultyName { get; set; }
        public Int64 ProgInstPartTermId { get; set; }
        public string InstancePartTermName { get; set; }
        public Int32 AcademicYearId { get; set; }
        public string AcademicYearCode { get; set; }
        public Int32 SourceInstId { get; set; }
        public string SourceInst { get; set; }
        public Int32 DestinationInstId { get; set; }
        public string DestInst { get; set; }
        //public MstInstitute DestinationInstId { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public string IFSCCODE { get; set; }
        public string PassbookPhoto { get; set; }
        public Int64 RequestedBy { get; set; }
        public DateTime RequestedOn { get; set; }
        public string RequestedOnView { get; set; }
        public string SourceInstituteStatus { get; set; }
        public string SourceInstituteStatus1 { get; set; }
        public string SourceInstituteRemark { get; set; }
        public Int64 SourceRequestProcessedBy { get; set; }
        public DateTime SourceRequestProcessedOn { get; set; }
        public string SourceRequestProcessedOnView { get; set; }
        public string DestinationInstituteStatus { get; set; }
        public string DestinationInstituteStatus1 { get; set; }
        public string DestinationInstituteRemark { get; set; }
        public Int64 DestinationRequestProcessedBy { get; set; }
        public DateTime DestinationRequestProcessedOn { get; set; }
        public string DestinationRequestProcessedOnView { get; set; }
        public Boolean IsFeeCategoryChanged { get; set; }
        public string IsFeeCategoryChangedSts { get; set; }
        public string FeeCategoryName { get; set; }
        public Int32 ChangedFeeCategoryId { get; set; }
        public Boolean IsFeesPaid { get; set; }
        public Int32 DestinationInstituteUserId { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedOnView { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public Boolean IsDeleted { get; set; }
        public Boolean IsActive { get; set; }

    }

    #endregion

    #region Pre-Exam Data Report

    public class PreExamDataReport
    {
        public Int64 IndexId { get; set; }
        public string FacultyName { get; set; }
        public string InstancePartTermName { get; set; }
        public string ProgrammeName { get; set; }
        public string ProgrammeCode { get; set; }
        public string ProgrammePartName { get; set; }
        public string ProgrammePartTermName { get; set; }
        public string ProgrammeModeName { get; set; }
        public string BranchName { get; set; }
        public Int64 PRN { get; set; }
        public string NameAsPerMarksheet { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NameOfFather { get; set; }
        public string NameOfMother { get; set; }
        public string DOB { get; set; }
        public string InstructionMediumName { get; set; }
        public string ApplicationReservationName { get; set; }
        public string OptionalMobileNo { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string CurrentAddress { get; set; }
        public string CurrentCityVillage { get; set; }
        public string CurrentPincode { get; set; }
        public string StateName { get; set; }
        public string DistrictName { get; set; }
        public string PhysicalDisability { get; set; }
        public string InstituteCode { get; set; }
        public string InstituteName { get; set; }
        public Int64 FormNo { get; set; }
        public string SeatNumber { get; set; }
        public string BarCode { get; set; }
        public string EligibilityByAcademics { get; set; }
        public string InwardByTimestamp { get; set; }
        public string InwardDate { get; set; }
        public string InwardTime { get; set; }
        public string PaperCode { get; set; }
        public string PaperName { get; set; }
        public string AssessmentType { get; set; }
        public string TeachingLearningMethodName { get; set; }
        public string AssessmentMethodName { get; set; }
        public string AppearanceType { get; set; }
        public string ExamDate { get; set; }
        public string Duration { get; set; }
        public string ExamEvent { get; set; }
        public string VenueName { get; set; }
        public string CenterName { get; set; }
        public string VenueCode { get; set; }
        public Int32 ExamMasterId { get; set; }
        public Int32 BranchId { get; set; }
        public Int32 ProgrammePartTermId { get; set; }
    }

    #endregion

    #region General Register Report

    public class GeneralRegisterReport
    {
        public Int64 IndexId { get; set; }
        public Int32 FacultyId { get; set; }
        public Int64 PRN { get; set; }
        public string StudentName { get; set; }
        public string Gender { get; set; }
        public string CorrespondenceAddress { get; set; }
        public string MobileNo { get; set; }
        public string ApplicationReservationName { get; set; }
        public string Caste { get; set; }        
        public string PlaceOfBirth { get; set; }        
        public string DOB { get; set; }       
        public string PreviousSchoolCollegeName { get; set; }       
        public string DateOfAdmission { get; set; }       
        public string CourseName { get; set; }       
        
    }

    #endregion


    /* Harsh Mistry - START */
    #region DepartmentStudentList
    public class DepartmentStudentList
    {

        public Int64 Id { get; set; }
        public Int64 InstancePartTermId { get; set; }
        public Int64 FacultyId { get; set; }
        public Int64 InstituteId { get; set; }
        public Int64 DepartmentId { get; set; }
        public Int64 ProgrammeId { get; set; }
        public Int32 AcademicYearId { get; set; }
        public Int64 SerializeNumber { get; set; }
        public Int64 StudentAdmissionId { get; set; }
        public Int64 PRN { get; set; }
        public String StudentName { get; set; }
        public String MobileNo { get; set; }
        public String EmailId { get; set; }
        public String ReportType { get; set; }
    }
    #endregion

    #region DepartmentStudentPaperList
    public class DepartmentStudentPaperList
    {

        public Int64 Id { get; set; }
        public Int64 InstancePartTermId { get; set; }
        public Int64 FacultyId { get; set; }
        public Int64 InstituteId { get; set; }
        public Int64 DepartmentId { get; set; }
        public Int64 ProgrammeId { get; set; }
        public Int32 AcademicYearId { get; set; }
        public Int64 SerializeNumber { get; set; }
        public Int64 StudentAdmissionId { get; set; }
        public Int64 PRN { get; set; }
        public String StudentName { get; set; }
        public String MobileNo { get; set; }
        public String EmailId { get; set; }
        public String ReportType { get; set; }
    }
    #endregion
    /* Harsh Mistry - END */

    //Gayatri's Code
    public class Sectionwiseblankmarksheet
    {
        public int? ExamMasterId { get; set; }
        public int? ProgrammeId { get; set; }
        public int? ProgrammePartTermId { get; set; }
        public int? TeachingLearningMethodId { get; set; }
        public int? AssessmentMethodId { get; set; }
        public int? BranchId { get; set; }
        public int? FacultyExamMapId { get; set; }
        public int? PaperId { get; set; }
        public List<MstPaper> PaperList { get; set; }
        public string SectionName { get; set; }
        public Int32 SectionMarks { get; set; }
    }

    //Start - Mohini's Code 23-Apr-2022============================

    public class ExamVenueGetforAssignBlocks
    {
        public Int32 ExamVenueId { get; set; }
        public Int32 ExamVenueExamCenterId { get; set; }
        public string CenterName { get; set; }

    }
    //End - Mohini's Code 23-Apr-2022============================


    //Bhushan's Code

    public class ReplacePaper
    {
        public Int64 IncProgInstancePartTermId { get; set; }

        public Int64 SourcePaperId { get; set; }

        public Int64 DestinationPaperId { get; set; }

        public Int32 AcademicYear { get; set; }

        public Int32 FacultyId { get; set; }

        public Int32 ProgrammeId { get; set; }

        public Int32 SpecialisationId { get; set; }

        public Int32 ProgrammeInstancePartId { get; set; }

        public Int64 IndexId { get; set; }



    }
    public class CopyAdmissionFees
    {
        public string SourceInstancePartTermName { get; set; }
        public string DestinationInstancePartTermName { get; set; }

        public string BranchName { get; set; }

        public string ProgPartTermName { get; set; }

        public Int32 AcademicYearId { get; set; }

        public Int32 FacultyId { get; set; }

        public Int32 ProgrammeId { get; set; }



    }

    public class SeatNumberWiseBlockChange
    {
        public Int64 PRN { get; set; }
        public string SeatNumber { get; set; }

        public string ExamVenueName { get; set; }

        public string ExamBlockName { get; set; }

        public string PaperCodeName { get; set; }

        public Int64 IndexId { get; set; }

        public Int32 ExamVenueId { get; set; }
        public Int32 ExamBlockId { get; set; }

        public Int32 ExamBlockId1 { get; set; }

        public Int64 UserId { get; set; }

        public Boolean SeatNumberCheck { get; set; }

        public string ExamBlockName1 { get; set; }

        public Int64 MstPaperId { get; set; }

    }

}