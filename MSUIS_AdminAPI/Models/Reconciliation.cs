using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
   
    #region Reconciliation
    public class FeeReconciliation
    {       
        public string NameAsPerMarksheet { get; set; }
        public string InstancePartTermName { get; set; }
        public Int64 ProgInstancePartTermId { get; set; }
        public Int32 ExamMasterId { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string OrderId { get; set; }
        public string BankReferanceNumber { get; set; }
        public Int64 TransactionId { get; set; }
        public Int64 PRN { get; set; }
        public string TransactionDate { get; set; }
        public string TransactionStatus { get; set; }
        public string ApplicationStatus { get; set; }
        public string AppearanceType { get; set; }
        public string FeesPaidInr { get; set; }
        public string ExamFeeAmount { get; set; }
        public string AmountPaid { get; set; }
        public string TotalAmountPaid { get; set; }
        public string FeeSubHeadName { get; set; }

        public List<TempAdmFee> FeeDetails { get; set; }
      
    }
    #endregion

    #region TempAdmFee
    public class TempAdmFee
    {
       
        public string InstancePartTermName { get; set; }        
        public string OrderId { get; set; }        
        public string TransactionStatus { get; set; }       
        public string AmountPaid { get; set; }
        public string FeeSubHeadName { get; set; }

    }
    #endregion

}