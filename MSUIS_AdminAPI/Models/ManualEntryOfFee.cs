using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class ManualEntryOfFee
    {
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public Int64 FeeCategoryPartTermMapId { get; set; }
        public Int32 FeeCategoryId { get; set; }
        public Int64 ApplicationFormNo { get; set; }
        public string NameAsPerMarksheet { get; set; }
        public string FeeCategoryName { get; set; }
        public string CommitteeName { get; set; }
        public double TotalAmountPaid { get; set; }
        public double TotalAmount { get; set; }
        public int TransactionId { get; set; }
        public Boolean TransactionStatus { get; set; }
        public Boolean PaymentStatus { get; set; }
        public int UniversityAccountNumber { get; set; }
        public int ChequeNo { get; set; }
        public int IndexId { get; set; }

        public string FacultyName { get; set; }

        public string ProgrammeName { get; set; }

        public string BranchName { get; set; }

        public Int32 AcademicYearId { get; set; }

        public Int64 PRN { get; set; }
    }
}