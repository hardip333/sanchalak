using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class UserRoleMap
    {
        public Int64 Id { get; set; }
		public Int64 UserId { get; set; }
		public String UserName { get; set; }
		public String DisplayName { get; set; }
		public Int64 RoleId { get; set; }
		public String RoleName { get; set; }
		public Int64 DesignationId { get; set; }
		public String DesignationName { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public String StartDateView { get; set; }
		public String EndDateView { get; set; }
		public String Tennure { get; set; }
		public Int64 ? FacultyId { get; set; }
		public String FacultyName { get; set; }
		public Int64? DepartmentId { get; set; }
		public String DepartmentName { get; set; }
		public Int64? InstituteId { get; set; }
		public String InstituteName { get; set; }
		public Int64? UniversitySectionId { get; set; }
		public String UniversitySectionName { get; set; }
		public String UserLevelType { get; set; }
		public Int64 IndexId { get; set; }
		public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
		public String SubjectName { get; set; }

	}
}