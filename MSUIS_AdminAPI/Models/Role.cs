using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class MstRole
    {
        public Int64 Id { get; set; }
        public String RoleName { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsDelete { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        /*Required to store the information related to Get the username and id*/
        public String UserId { get; set; }
        public String UserName { get; set; }
        public Int64 UserTypeId { get; set; }
        public String Icon { get; set; }
        public String PageUrl { get; set; }
        public String DisplayName { get; set; }
        public String UserMenuGroupId { get; set; }
        public Int64 DisplayOrder { get; set; }
        public Int64 cn { get; set; }
        public String ParentName { get; set; }
        public String DesignationName { get; set; }
        public Int64? FacultyId { get; set; }
        public String FacultyName { get; set; }
        public Int64? InstituteId { get; set; }
        public String InstituteName { get; set; }
        public Int64? DepartmentId { get; set; }
        public String DepartmentName { get; set; }
        public String TypeWithId { get; set; }
        public String NameforDisplay { get; set; }
        public String facultyOrDepId { get; set; }
        public Int64 MainId { get; set; }
        public Int64 RefId { get; set; }
        public bool IsParent { get; set; }

    }
}