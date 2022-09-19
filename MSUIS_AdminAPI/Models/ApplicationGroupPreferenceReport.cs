using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class ApplicationGroupPreferenceReport
    {
        
            public Int32 Id { get; set; }
           public Int32 IndexId { get; set; }       
           public Int32 AcademicYearId { get; set; }
           public Int32 ProgrammeId { get; set; }
           public Int32 InstituteId { get; set; }
           public Int64 ProgrammeInstancePartTermId { get; set; }
         

        public String NameAsPerMarksheet { get; set; }

        public String ApplicationNo { get; set; }
        public String UserName { get; set; }
         public String EmailId { get; set; }
         public String MobileNo { get; set; }
         public String SocialCategoryName { get; set; }
        public String EligibleDegreeName { get; set; }
        public decimal Percentage { get; set; }
        public string FacultyName { get; set; }
        public string AcademicYearCode { get; set; }
        public String ProgrammeName { get; set; }
        public String BranchName { get; set; }
        public string InstancePartTermName { get; set; }

        

        

        public String Choice1 { get; set; }
            public String Choice2 { get; set; }
            public String Choice3 { get; set; }
            public String Choice4 { get; set; }
            public String Choice5 { get; set; }
            public String Choice6 { get; set; }
            public String Choice7 { get; set; }
            public String Choice8 { get; set; }
            public String Choice9 { get; set; }
            public String Choice10 { get; set; }
            public String Choice11 { get; set; }
            public String Choice12 { get; set; }
            public String Choice13 { get; set; }
            public String Choice14 { get; set; }
            public String Choice15 { get; set; }
            public String Choice16 { get; set; }
            public String Choice17 { get; set; }
            public String Choice18 { get; set; }
            public String Choice19 { get; set; }
            public String Choice20 { get; set; }
            public String Choice21 { get; set; }
            public String Choice22 { get; set; }
            public String Choice23 { get; set; }
            public String Choice24 { get; set; }
            public String Choice25 { get; set; }
            public String Choice26 { get; set; }
            public String Choice27 { get; set; }
            public String Choice28 { get; set; }
            public String Choice29 { get; set; }
            public String Choice30 { get; set; }
            public String Choice31 { get; set; }

        //bhushan's Code Starts
        public Int64 FacultyId { get; set; }
        public Int64 PreferenceNo { get; set; }
        public Int32 PreferenceId { get; set; } //Bhushan Code

        public Int32 SpecialisationId { get; set; } //Bhushan Code

        public Int32 ProgrammePartId { get; set; } //Bhushan Code
        public Int32 ProgrammePartTermId { get; set; } //Bhushan Code

        //bhushan's Code Ends





    }
}