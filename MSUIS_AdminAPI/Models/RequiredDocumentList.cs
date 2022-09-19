using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class RequiredDocumentList
    {
        public Int64 Id { get; set; }
        public Int64 IndexId { get; set; }
        public Int64 AcademicYearId { get; set; }
        public Int64 InstituteId { get; set; }
        public Int64 ProgrammeId { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public string AcademicYearCode { get; set; }
        public string ProgrammeName { get; set; }
        public string InstancePartTermName { get; set; }
     
        public string NameOfTheDocument { get; set; }
        public string DocumentType { get; set; }
       
    }
        
}