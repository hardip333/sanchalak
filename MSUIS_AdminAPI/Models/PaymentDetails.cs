using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    
        #region AdmApplication
        public class AdmApplication
        {
            public Int64 Id { get; set; }
            public Int64 ApplicationRegistrationId { get; set; }

            //public Int64 ProgrammeInstancePartTermId { get; set; }

            public string FirstName { get; set; }

            public string MiddleName { get; set; }

            public string LastName { get; set; }

            public Int32 ProgrammeId { get; set; }

            public string ProgrammeName { get; set; }

            public string ApplicationFeesPaid { get; set; }

            public string TransactionId { get; set; }
            public string TransactionDate { get; set; }

            public string TransactionStatus { get; set; }

            public string GatewayRequest { get; set; }
            public string GatewayResponse { get; set; }
            public string Error { get; set; }
            public string IpAddress { get; set; }
            public string Browser { get; set; }
            public string OS { get; set; }
            public string DeviceName { get; set; }
            public Boolean IsDeleted { get; set; }
        }
    #endregion

    public class HDFCStatusAPI
    {
        public string reference_no { get; set; }
        public string order_id { get; set; }
        public string Token { get; set; }

    }
}
