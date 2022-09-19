using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class RefundCaseByAudit
    {
        public Int64 Id { get; set; }
        public Int64 ApplicationId { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public Int32 FacultyId { get; set; }
        public Int64 IndexId { get; set; }
        public string InstancePartTermName { get; set; }
        public string FullName { get; set; }
        //public Boolean RequestStatusFlag { get; set; }
        //public Boolean ? IsApprovedByAudit { get; set; }
        public string RequestType { get; set; }
        //public string ProcessedOnAudit { get; set; }
        public string PassbookDoc { get; set; }
        public string RTGSForm { get; set; }
        public string IsApprovedByAcademic { get; set; }
        public string ProcessedOnAcademic { get; set; }

        public string IsApprovedByAudit { get; set; }
        public string ProcessedOnAudit { get; set; }

        public string IsApprovedByAccount { get; set; }
        public string ProcessedOnAccount { get; set; }
        public decimal? AmountPaid { get; set; }
        public decimal? RefundAmountByAcademic { get; set; }
        public decimal? RefundAmountByAudit { get; set; }
        public string RequestStatus { get; set; }
    }

    public class RefundCaseAuditApplicantDetail
    {
        public Int64 ApplicationId { get; set; }
        public String NameAsPerMarksheet { get; set; }
        public String FacultyName { get; set; }
        public Int32 Id { get; set; }
        public Int32 FacultyId { get; set; }
        public String ProgrammeName { get; set; }
        public String BranchName { get; set; }
        public String OldInstancePartTermName { get; set; }
        public String NewInstancePartTermName { get; set; }
        public String InstancePartTermName { get; set; }
        public String EligibilityStatus { get; set; }
        public String OldFeeCategoryName { get; set; }
        public String NewFeeCategoryName { get; set; }
        public String FeeCategoryName { get; set; }
        public decimal ? TotalAmount { get; set; }
        public decimal ? AmountPaid { get; set; }
        public decimal ? RefundAmountByAcademic { get; set; }
        public Boolean IsVerificationSms { get; set; }
        public Boolean IsVerificationEmail { get; set; }
        public Int64 FeeCategoryPartTermMapId { get; set; }
        public String EligibilityByAcademics { get; set; }
        public String OldGroupName { get; set; }
        public String NewGroupName { get; set; }
        public String OldInstituteName { get; set; }
        public String NewInstituteName { get; set; }
        public String GroupName { get; set; }
        public String AdmittedInstituteName { get; set; }
        public String IsVerificationEmailOnView { get; set; }
        public String IsVerificationSMSOnView { get; set; }
        public Boolean IsAdmissionFeePaid { get; set; }
        public Int32 FeeCategoryId { get; set; }
        public Boolean IsPRNGenerated { get; set; }
        public String AdmittedOnView { get; set; }
        public String PaymentDateView { get; set; }
        public String RequestedOn { get; set; }
        public String ProcessedOnAcademic { get; set; }
        public string RequestType { get; set; }
        public string AcademicRemark { get; set; }
        public String FeePaidDate { get; set; }
    }

    public class RefundedAmountAudit
    {
        public Int64 ApplicationId { get; set; }
        public Double RefundAmountByAudit { get; set; }
        public Double RefundAmountByAcademic { get; set; }
        public Int64 ProcessedByAudit { get; set; }
        public string AuditRemark { get; set; }
        public string RequestType { get; set; }
    }

}