using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class ApplicationStatistics
    {
        public int FacultyId { get; set; }
        public int ProgrammeInstancePartTermId { get; set; }
        public string InstancePartTermName { get; set; }
        public int TotalApplication { get; set; }
        public int ApplicationFeeNotPaid { get; set; }

        public int PreVerificationVerifried { get; set; }

        public int PreVerificationPending { get; set; }

        public int PreVerificationNotApproved { get; set; }

        public int PreVerificationPendingVerified { get; set; }

        public int PendingApprovedByAcademic { get; set; }

        

        public int ApprovedByFaculty { get; set; }
        public int NotApprovedByFaculty { get; set; }
        public int ApprovedByAcademic { get; set; }
        public int NotApprovedByAcademic { get; set; }
        public int AdmissionFeePaid { get; set; }
        public int AdmissionFeeNotPaid { get; set; }
        public int AdmittedStudent { get; set; }
        public int PrnGenerated { get; set; }
        
        public int PRNNotGenerated { get; set; }
        public int PaperSelected { get; set; }
        public int? Intake { get; set; }
        public int CancelledByAcademics { get; set; }
        public int CancelledByFaculty { get; set; }
        public int CancelledByStudent { get; set; }
        public int AcademicYearId { get; set; }
        public int InstituteId { get; set; }
        public string ProgrammeName { get; set; }
        public string BranchName { get; set; }
        public int EligibilityGroupCount { get; set; }
        public int EligibilityGroupComponentCount { get; set; }
        public int ProgrammeAddOnCount { get; set; }
        public int RequiredDocumentCount { get; set; }
        public int ApplicationConfigurationCount { get; set; }
        public int ApplicationFeeConfigured { get; set; }
        public int ApplicationFeePublish { get; set; }
        public Int32 Id { get; set; }
        public string InstituteName { get; set; }
        public string AcademicYearCode { get; set; }
        public Int64 IndexId { get; set; }
        public string EligibilityGroup { get; set; }
        public string EligibilityGroupComponent { get; set; }
        public string ProgrammeAddOn { get; set; }
        public string RequiredDocument { get; set; }
        public string ApplicationConfiguration { get; set; }
        public string ApplicationFeeConfig { get; set; }
        public string AppFeePublish { get; set; }
        public int AdmissionFeeConfigured { get; set; }
        public int AdmissionFeePublish { get; set; }
        public string AdmFeeConfig { get; set; }
        public string AdmFeePublish { get; set; }

        public string FacultyName { get; set; }

        public Int32 RefundRequestByStudent { get; set; }

        public Int32 RefundRequestByFaculty { get; set; }

        public Int32 RefundRequestByAcademics { get; set; }

        public Int32 RefundRequestByAudit { get; set; }

        public Int32 RefundRequestByAccount { get; set; }

       


    }
}