using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class FacultyWiseCategoryReport
    {
        public Int64 FacultyId { get; set; }
        public Int64 AcademicYearId { get; set; }
        public string FacultyName {get;set;}
        public string ProgrammeName { get; set; }
        public string ProgrammeCode { get; set; }
        public string ProgrammeModeName { get; set; }
        public string  BranchName { get; set; }
        public string InstancePartTermName { get; set; }
        public string PartName { get; set; }
        public string PartTermName { get; set; }
        public Int32 FemaleGeneral { get; set; }
        public string FemalePH { get; set; }
        public Int32 FemaleSC { get; set; }
        public Int32 FemaleST { get; set; }
        public Int32 FemaleSEBC { get; set; }
        public Int32 TotalFemale{ get; set; }
        public Int32 MaleGeneral { get; set; }
        public string MalePH { get; set; }
        public Int32 MaleSC { get; set; }
        public Int32 MaleST { get; set; }
        public Int32 MaleSEBC { get; set; }
        public Int32 TotalMale{ get; set; }
        public Int32 TotalStudent { get; set; }
        public Int32 IndexId { get; set; }
    }
}