using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class AdmRequiredDocumentsProgram
    {
        public Int32 Id { get; set; }
        public Int32 ProgramId { get; set; }
        public Int32 DocumentId { get; set; }
        public bool DocumentType { get; set; }
        public bool IsCompulsoryDocument { get; set; }
        public Int32 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int32 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public string ProgrammeName { get; set; }
        public string NameOfTheDocument { get; set; }
        public bool documentexits { get; set; }
        public Int32 AdmissionApplicationId { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
    }
}