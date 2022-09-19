using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class MstStudentTransferRequest
    {
        public Int64 Id { get; set; }
        public Int32 IndexId { get; set; }
        public Int64 StudentPRN { get; set; }
        public Int32 FacultyId { get; set; }
        public Int64 ProgInstPartTermId { get; set; }
        public Int32 AcademicYearId { get; set; }
        public Int32 SourceInstId { get; set; }
        public Int32 DestinationInstId { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public string IFSCCODE { get; set; }
        public Int64 RequestedBy { get; set; }
        public DateTime RequestedOn { get; set; }
        public string SourceInstituteStatus { get; set; }
        public string SourceInstituteRemark { get; set; }
        public Int64 SourceRequestProcessedBy { get; set; }
        public DateTime SourceRequestProcessedOn { get; set; }
        public string DestinationInstituteStatus { get; set; }
        public string DestinationInstituteRemark { get; set; }
        public Int64 DestinationRequestProcessedBy { get; set; }
        public DateTime DestinationRequestProcessedOn { get; set; }
        public Boolean IsFeeCategoryChanged { get; set; }
        public Int64 ChangedFeeCategoryPartTermMapId { get; set; }
        public Boolean IsFeesPaid { get; set; }
        public Int32 DestinationInstituteUserId { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public Boolean IsDeleted { get; set; }
        public Boolean IsActive { get; set; }
        public Int32 InstituteId { get; set; }
        public string InstancePartTermName { get; set; }
        public string SourceInstituteName { get; set; }
        public string DestinationInstituteName { get; set; }
        public string EligibleDegreeName { get; set; }
        public string BranchName { get; set; }
        public string ExaminationBodyName { get; set; }
        public string InstituteAttended { get; set; }
        public string CityName { get; set; }
        public string ExamPassMonth { get; set; }
        public string ExamPassYear { get; set; }
        public string ExamCertificateNumber { get; set; }
        public string MarkObtained { get; set; }
        public string MarkOutof { get; set; }
        public string Grade { get; set; }
        public string CGPA { get; set; }
        public string PercentageEquivalenceCGPA { get; set; }
        public string Percentage { get; set; }
        public string ClassName { get; set; }
        public string IsFirstTrial { get; set; }
        public string IsLastQualifyingExam { get; set; }
        public string LanguageName { get; set; }
        public string ResultStatus { get; set; }
        public string OtherCity { get; set; }
        public string AttachDocument { get; set; }
        public string FeeCategoryName { get; set; }
        public Int64 FeeId { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public String LastName { get; set; }
        public String FirstName { get; set; }
        public String MiddleName { get; set; }
        public string UserName { get; set; }
        public string SourceRequestProcessedOnDate { get; set; }
        public string DestinationRequestProcessedOnDate { get; set; }
        public string OldFee { get; set; }
        public string NewFee { get; set; }
        public decimal OldFeeTotalAmount { get; set; }
        public decimal NewFeeTotalAmount { get; set; }

    }
}