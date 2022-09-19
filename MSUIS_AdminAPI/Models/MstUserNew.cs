using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class MstUserNew
    {
        public Int64 Id { get; set; }
        public Int64 UserTypeId { get; set; }
        public string UserTypeName { get; set; }
        public Int64? EmployeeId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string DisplayName { get; set; }
        public bool ? IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public Int64 IndexId { get; set; }
    }
}