using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class CertificateIssue
    {
        public string NameAsPerMarksheet { get; set; }

        public Int64 CertiId { get; set; }

        public string PRN { get; set; }

        public string IssueDate { get; set; }

        public string IssueReason { get; set; }

        public Boolean FeesPaid { get; set; }

        public string ValidUpto { get; set; }

        public string AcademicYearCode { get; set; }

        public string PreviousAcademicYearCode { get; set; }

        public string StudentPhoto { get; set; }

        public string ProgrammeId { get; set; }

    }

    public class RequestCerti
    {
        public Int64 CertiId { get; set; }

        public string PRN { get; set; }

        public string RequestDate { get; set; }

        public string ProgrammeId { get; set; }

        public Boolean IsDualAdmission { get; set; }

        public Int32 FacultyId { get; set; }

        public string FacultyName { get; set; }

        public string ProgrammeName { get; set; }

        public string CertificateName { get; set; }

        public decimal Fees { get; set; }

        public Int64 FeeConfigId { get; set; }
    }

    public class StudentBonafied
    {
        
        public string NameAsPerMarksheet { get; set; }

        public string PRN { get; set; }

        public string InstituteName { get; set; }

        public string InstituteAddress { get; set; }

        public string InstituteContactNo { get; set; }

        public string AcademicYearCode { get; set; }

        public string Sign { get; set; }

        public string PartName { get; set; }

        public string IssueDate { get; set; }

        public string AuthorityType { get; set; }

        public string CertificateName { get; set; }

        public string Preface1 { get; set; }

        public string Preface2 { get; set; }

        public string Postface1 { get; set; }

        public string Postface2 { get; set; }

        
    }
}