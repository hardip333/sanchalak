using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class TransferCertificateReport
    {

        public string PRN { get; set; }

        public string NameAsPerMarksheet { get; set; }

       

        public Int32 FacultyId { get; set; }

        public Int32 ProgrammeId { get; set; }

        public string FacultyName { get; set; }

        public string ProgrammeName { get; set; }

        public string TCCode { get; set; }

        public Int32 CertiId { get; set; }

        public string CertificateName { get; set; }

        public Int64 IndexId { get; set; }




    }




}