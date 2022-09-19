using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class UserTypeMaster
    {
        public Int64 Id { get; set; }
        public string UserTypeName { get; set; }
        public string UserTypeDesc { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Int64 ModifiedBy { get; set; }

    }
}