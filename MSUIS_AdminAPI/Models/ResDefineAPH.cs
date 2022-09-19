using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class ResDefineAPH
    {
        public Int32 Id { get; set; }
        public string APHCode { get; set; }
        public Int32 MstEvaluationId { get; set; }
        public string APHName { get; set; }
        public string APHAbbrev { get; set; }
        public string APHDescription { get; set; }
        public string EvaluationName { get; set; }
        public Int32 APHMinMarks { get; set; }
        public Int32 APHMaxMarks { get; set; }
        public bool IsLaunched { get; set; }
        public Int32 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int32 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }

    }
}