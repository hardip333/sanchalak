using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class ResMarksTemplate
    {
        public Int32 Id { get; set; }
        public Int32 NoofIntervals { get; set; }
        public string MarksTemplateName { get; set; }
        public string MarksTemplateDescription { get; set; }
        public string TemplateUsedwith { get; set; }
        public Int32 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int32 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public Int32 IndexId { get; set; }
    }
}