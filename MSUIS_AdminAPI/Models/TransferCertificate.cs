using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{

    public class GetStdudentTransferDetails
    {
        public int ProgId { get; set; }

        public string ProgrammeName { get; set; }

        public Int64 PRN { get; set; }

        public int SpecId { get; set; }

    }

    public class GetBranchList
    {
        public int SpecId { get; set; }

        public string BranchName { get; set; }
    }
    public class TransferCertificate
    {
        public string InstituteName { get; set; }

        public string InstituteAddress { get; set; }

        public string CertificateName { get; set; }

        public string StudentPhoto { get; set; }

        public string PRN { get; set; }

        //public string tcn { get; set; }

        public string TCCode { get; set; }

        public string StudentSignature { get; set; }
        
        public string Sign { get; set; }

        public string AuthorityType { get; set; }

        public string NameOfAuthority { get; set; }

        public string Gender { get; set; }

        public string NameAsPerMarksheet { get; set; }

        //public string ResultStatus { get; set; }

        public string PartName{get; set;}

        //public string PartName1 { get; set; }//multiple values

        public string BranchName { get; set; }

        public string ProgrammeModeName { get; set; }

        public string PartTermShortName { get; set; }

        public string LastPartTermShortName { get; set; }

        //public string InstancePartTermName { get; set; }
        public string AcademicYearCode { get; set; }
        public string DisplayName { get; set; } //multiple

        public string DOB { get; set; }

        public string PartStatus { get; set; }

        public bool IsTransfered { get; set; }

        public string Part1 { get; set; }

        public string Part2 { get; set; }

        public string Part3 { get; set; }

        public string Part4 { get; set; }

        public string Part5 { get; set; }

        public string Part6 { get; set; }

        public string Part7 { get; set; }

        public string Part8 { get; set; }

        public string Part9 { get; set; }

        public string Part10 { get; set; }

        public string Part11 { get; set; }

        public string Part12 { get; set; }

        public string Part13 { get; set; }

        public string Part14 { get; set; }

        public string Part15 { get; set; }
    }

    public class TransferCertificateVerification
    {
        public string NameAsPerMarksheet { get; set; }

        public string ProgrammeName { get; set; }

        public string CertificateName { get; set; }

        public string IssueDate { get; set; }

        public string Sign { get; set; }

        public string AuthorityType { get; set; }

        public string IssueReason { get; set; }

        public string StudentPhoto { get; set; }
    }
}