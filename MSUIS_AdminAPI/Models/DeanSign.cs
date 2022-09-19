using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class DeanSign
    {
        public Int64 UserId { get; set; }
        public String NameOfAuthority { get; set; }
        public String DSign { get; set; }       
    }
}