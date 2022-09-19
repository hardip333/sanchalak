using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class RequiredDocumentPendingList
    {
        public int Id { get; set; }
      
        public Int64 AcademicYearId { get; set; }
        public Int64 InstituteId { get; set; }
        public Int64 FacultyId { get; set; }
        public Int64 ProgrammeId { get; set; }
        public String InstancePartTermName { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public int IndexId { get; set; }
      
        public Int64 ApplicationFormNo { get; set; }
        public String ApplicationFormNum { get; set; }
        public String NameAsPerMarksheet { get; set; }
        public Int64 ApplicantUserName { get; set; }
        public String UserName { get; set; }
        public String MobileNo { get; set; }
        public String EmailId { get; set; }
        public String ProgrammeName { get; set; }
        public String BranchName { get; set; }
        public String NameOfTheDocument { get; set; }
       

    }
    
}