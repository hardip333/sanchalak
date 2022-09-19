using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class InstituteQuery
    {
        public Int64 Id { get; set; }

        public Int64 UserRegistrationId { get; set; }

        public string UserName { get; set; }

        public Int32 InstituteId { get; set; }

        public string InstituteName { get; set; }

        public Int32 FacultyId { get; set; }

        public string FacultyName { get; set; }

        public string Query { get; set; }

        public string QueryDescription { get; set; }

        public string UploadImage { get; set; }

        public Int64 CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public Int64 ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public Boolean IsActive { get; set; }

        public Boolean IsDeleted { get; set; }

        public string FacultyEmail { get; set; }

        

    }

    
}