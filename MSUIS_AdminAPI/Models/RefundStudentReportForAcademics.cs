using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class RefundStudentsReportForAcademics
    {
        public Int64 ApplicationId { get; set; }

        public Int64 ProgrammeInstancePartTermId { get; set; }
        
        public string AcademicYearCode { get; set; }

        public string FacultyName { get; set; }

        public string BranchName { get; set; }

        public string ProgrammeName { get; set; }

        public string InstancePartTermName { get; set; }

        public string TotalAmount { get; set; }

        public string AmountPaid { get; set; }

        public string RefundAmountByAcademic { get; set; }

        public string RefundAmountByAudit { get; set; }

        public Int64 RefundBy { get; set; }

        public string RefundedOn { get; set; }

        public Int32 AdmittedInstituteId { get; set; }

        public string InstituteName { get; set; }

        public Int32 InstituteId { get; set; }

        public Int64 IndexId { get; set; }
        


    }



}