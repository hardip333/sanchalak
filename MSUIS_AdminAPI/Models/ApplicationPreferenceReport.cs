using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
   
    public class ApplicationInstitutePreferenceReport
    {

        public Int32 Id { get; set; }
        public string ApplicationNo { get; set; }
        public int InstituteId { get; set; }

        public int IncProgInstId { get; set; }
        public String InstituteName { get; set; }
        public string InstancePartTermName { get; set; }
        public String GroupName { get; set; }
        public String ProgrammeName { get; set; }
        public String BranchName { get; set; }
        public Int32 PreferenceNo { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsDeleted { get; set; }

        public string PreferenceGivenOn { get; set; }

    }

    public class IncProgInstancePartTerm
    {
        public int Id { get; set; }
        public int InstituteId { get; set; }
        public int FacultyId { get; set; }


        public String IncProgramInstancePartTermName { get; set; }
    }
}