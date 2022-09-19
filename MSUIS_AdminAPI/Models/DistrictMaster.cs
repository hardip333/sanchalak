using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class DistrictMaster
    {
        public Int64 Id { get; set; }
        public String DistrictName { get; set; }
        public Int64 StateId { get; set; }

    }
}