using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class GenericConfiguration
    {
        public int Id{ get; set; }
        public string KeyName { get; set; }
        public string ShortCode { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}