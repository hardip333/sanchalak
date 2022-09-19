using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class RefundCaseCancelByAccount
    {
        public Int64 Id { get; set; }
        public string FacultyName { get; set; }
        public Int64 ApplicationId { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public Int32 FacultyId { get; set; }
        public Int64 IndexId { get; set; }
        public string InstancePartTermName { get; set; }
        public string FullName { get; set; }
        public Boolean RequestStatusFlag { get; set; }
        public Boolean ? IsApprovedByAccount { get; set; }
        public decimal? RefundAmountByAcademic { get; set; }
        public decimal? RefundAmountByAudit { get; set; }
        public Boolean? IsBOBAccount { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string BankName { get; set; }
        public string IFSCCode { get; set; }
        public string PassbookDoc { get; set; }
        public string RTGSForm { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public string DC { get; set; }
        public string TransDate { get; set; }
        public string DebitAccountNumber { get; set; }
        public DateTime? FromDate { get; set; }
        public string FromDateFinal { get; set; }
        public DateTime? ToDate { get; set; }
        public string ToDateFinal { get; set; }
        public string BgColor { get; set; }

        public string ProcessedOnAccount { get; set; }
        public string AccountRemark { get; set; }
        public string RefundedTansactionId { get; set; }
        public string RequestStatus { get; set; }
    }

    public class RefundCancelModelFinal
    {
        public Int64 ApplicationId { get; set; }
       
        public Int32 Id { get; set; }
        public String ProgrammeName { get; set; }
        public String ApplicationStatus { get; set; }
        public String InstancePartTermName { get; set; }
        public Boolean IsApprovedByAccount { get; set; }
        public Int64 ProcessedByAccount { get; set; }
        public String RefundedTansactionId { get; set; }
        public String AccountRemark { get; set; }

    }
}