using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class CountryMaster
    {
        public Int64 Id { get; set; }
        public String Alpha2Code { get; set; }
        public String Alpha3Code { get; set; }
        public String CountryName { get; set; }

    }
}