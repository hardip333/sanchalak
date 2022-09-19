using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class IncStudentReAdmissionRequest
    {
        public Int64 PRN { get; set; }
        public Int64 ApplicationId { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public Int64 ProgrammeInstanceId { get; set; }
        public Int64 IncStudentAcademicInformationId { get; set; }
        public Int64 IncStudentAdmissionId { get; set; }
        public Int32 IndexId { get; set; }
        public String FacultyName { get; set; }     
        public String BranchName { get; set; }
        //public String ProgrammeName { get; set; }
        //public String PartName { get; set; }
        public String ProgrammeInstanceName { get; set; }
        public String InstancePartTermName { get; set; }
        public String NameAsPerMarksheet { get; set; }
        public String EmailId { get; set; }
        public String MobileNo { get; set; }
        public String StudentHandWrittenDocument { get; set; }
        public String FacultyRemark { get; set; }
        public Boolean IncStudentReAdmissionRequestStatus { get; set; }
    }
}