using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class ResGPATemplate
    {
        public Int32 Id { get; set; }
        public Int32 GradeScaleId { get; set; }
        public Int32 MstEvaluationId { get; set; }
        public Int32 NoofIntervals { get; set; }
        public string GPATemplateName { get; set; }
        public string GPATemplateDescription { get; set; }
        public Int32 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int32 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive {get; set; }
        public string EvaluationName { get; set; }
        public string GradeScaleName { get; set; }
        public Int32 IndexId { get; set; }
    }
}