using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class AdmApplicantsSubmittedDocuments
    {
        public Int32 Id { get; set; }
        public Int32 AdmissionApplicationId { get; set; }
        public Int32 DocumentId { get; set; }
        public string FileNameofDocumnetByUser { get; set; }
        public string FileNameofDocumnetBySystem { get; set; }
        public string IsCompulsoryDocument { get; set; }
        public string IsVerified { get; set; }
        public Int32 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int32 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public string NameOfTheDocument { get; set; }
        public Int64 ProgInstPartTermId { get; set; }
        public string IsVerifiedByAcademic { get; set; }
    }
}