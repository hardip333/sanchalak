using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class StudentPaymentPendingReport
    {
        public Int64 Id { get; set; }
        public Int64 FacultyId { get; set; }
        public Int64 AcademicYearId { get; set; }
        public Int64 ProgrammeInstanceId { get; set; }
        public Int64 BranchId { get; set; }
        public Int64 ProgrammeInstancePartId { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }

        public string FacultyName { get; set; }
        public string AcademicYearCode { get; set; }
        public string ProgrammeInstanceName { get; set; }
        public string BranchName { get; set; }
        public string ProgrammeInstancePartName { get; set; }
        public string InstancePartTermName { get; set; }
        public Int64 StudentPRN { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string NameAsPerMarksheet { get; set; }
        public string TotalAmount { get; set; }          
        public string AmountPaid { get; set; }
        public string IsInstalmentSelected { get; set; }
        public Int64 InstalmentNo { get; set; }    
        public Int64 TotalInstalmentGiven { get; set; }    
        public Int64 TotalInstalmentSelected { get; set; }    
        public Int64 RemainingInstallment { get; set; }    
       
        public Int32 IndexId { get; set; }
    }
}