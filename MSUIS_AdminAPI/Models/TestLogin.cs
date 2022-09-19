using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class AndroidAppLogin
    {
        public Int64 ApplicantRegistrationId { get; set; }

        public String token { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public Boolean IsAndroid { get; set; }

        public Int32? ModifiedBy { get; set; }
    }
}