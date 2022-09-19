using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class AddOnInformation
    {
       

            public Int32 Id { get; set; }
            public string ApplicationNo { get; set; }
            public Int64 InstituteId { get; set; }
            public Int64 AcademicYearId { get; set; }
            public int IncProgInstId { get; set; }
            public string InstancePartTermName { get; set; }
            public String ProgrammeName { get; set; }
            public String BranchName { get; set; }
            public List<AddOn> DocList { get; set; }
    }
    public class AddOn
        {
            public int Id { get; set; }
            public string TitleName { get; set; }
          

     }

    public class AdmStudentAddOnInfo
    {
        public Int32 Id { get; set; }
        public int IndexId { get; set; }
       public String ApplicationFormNumber { get; set; }
       public Int64 ApplicationFormNo { get; set; }
        public int IncProgInstId { get; set; }
        public Int64 PRN { get; set; }
        public String EmailId { get; set; }
        public String MobileNo { get; set; }
        public String NameAsPerMarkSheet { get; set; }
        public String InstancePartTermName { get; set; }
        public String AddOnValue { get; set; }
        public String TitleName { get; set; }
    }

    public class AdmStudentAddOnTitle
    {
      
        public Int64 ApplicationFormNo { get; set; }
        public int IndexId { get; set; }

        public String AddOnValue { get; set; }
        public String TitleName { get; set; }

        
    }

    public class AdmStudentAddOnNotAnswered
    {
        public int IndexId { get; set; }
        public Int64 ApplicationFormNo { get; set; }
        public String NotAnsweredQuestion { get; set; }
    }

    public class QuestionList
    {
        public List<AdmStudentAddOnTitle> ansquestList { get; set; }
        public List<AdmStudentAddOnNotAnswered> NoansquestList { get; set; }

        public Boolean AnsweredListflag { get; set; }
        public Boolean NotAnsweredListflag { get; set; }
    }
}