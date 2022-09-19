using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class Mandate
    {
        public Int64 Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string InstancePartTermName { get; set; }
        public string FacultyName { get; set; }
        public string MobileNo { get; set; }
        public string AcademicYear { get; set; }
        public Int64 ApplicationId { get; set; }
    }
}