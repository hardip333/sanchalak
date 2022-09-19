using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class AdmApplicationConfiguration
    {
        public Int32 Id { get; set; }
        public Int32 ProgramInstancePartTermId { get; set; }
        public Int32 ProgrammeId { get; set; }
        public DateTime ? ApplicationStartDate { get; set; } 
        public DateTime ? ApplicationStopDate { get; set; }
        public DateTime ? ApplicationFeeStartDate { get; set; }
        public DateTime ? ApplicationFeeEndDate { get; set; }
        public DateTime ? AdmissionFeesStartDate { get; set; }
        public DateTime ? AdmissionFeesStopDate { get; set; }
        public DateTime? ApplicationApprovalStartDate { get; set; }
        public DateTime? ApplicationApprovalStopDate { get; set; }
        public DateTime ? UpdatedDocLastDate { get; set; }
        public DateTime ? AdmInstallementEndDate { get; set; }
        public string ApplicationStartDateView { get; set; }
        public string ApplicationStopDateView { get; set; }
        public string ApplicationFeeStartDateView { get; set; }
        public string ApplicationFeeEndDateView { get; set; }
        public string AdmissionFeesStartDateView { get; set; }
        public string AdmissionFeesStopDateView { get; set; }
        public string ApplicationApprovalStartDateView { get; set; }
        public string ApplicationApprovalStopDateView { get; set; }
        public string UpdatedDocLastDateView { get; set; }
        public string AdmInstallementEndDateView { get; set; }
        public string Remarks { get; set; }
        public Int32 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int32 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public string ProgrammeName { get; set; }
        public string FacultyName { get; set; }
        public string PartTermName { get; set; }
        public string visibility { get; set; }
        public Int64 SpecialisationId { get; set; }
        public Int64 AcademicYearId { get; set; }
        public Int64 ProgrammePartId { get; set; }
        public Int64 ProgrammePartTermId { get; set; }
        public Int64 FacultyId { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public string ProspectusURL { get; set; }

        public string WrittenTestSyllabusURL { get; set; }
        public string WrittenTestScheduleURL { get; set; }
        public string ContactPersonEmail { get; set; }
        public string ContactPersonName { get; set; }
        public String ContactPersonNo { get; set; }
        public String FeeCategoryName { get; set; }
        public Double TotalAmount { get; set; }
        public Boolean FeeSts { get; set; }
        public Boolean SequenceCount { get; set; }
        public Boolean IsPaperSelByStudent { get; set; }
        public Boolean IsPaperSelBeforeFees { get; set; }
    }
}