using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class AdmPaymentTransaction
    {
        public Int64 Id { get; set; }
        public String InstancePartTermName { get; set; }
        public String Status { get; set; }
        public Int32 InstituteId { get; set; }
        public String InstituteName { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public Int64 OrderId { get; set; }
        public String TransactionDate { get; set; }
        public String TransactionStatus { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String MiddleName { get; set; }
        public String EmailId { get; set; }
        public String MobileNo { get; set; } 
        public Int32 IndexId { get; set; }
        public String Name { get; set; }
        public String GatewayRequest { get; set; }
        public String GatewayResponse { get; set; }
        public Int32 InstalmentNo { get; set; }
        public Int32 RemainingInstallment { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public String ToDateFinal { get; set; }

    }
}