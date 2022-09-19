using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class RolePermissionMap
    {
        public Int64 Id { get; set; }
        public Int64 RoleId { get; set; }
        public String RoleName { get; set; }
        public String Icon { get; set; }
        public String PageUrl { get; set; }
        public String text { get; set; }
        public Int64 DisplayOrder { get; set; }
        public Int64 RefId { get; set; }
        public bool? SelectedRoleFlag { get; set; }
        public bool? IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public state state { get; set; }
        public List<RolePermissionMap> children { get; set; }
    }
}