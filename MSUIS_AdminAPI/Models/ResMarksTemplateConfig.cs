using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class ResMarksTemplateConfig
    {
        public Int32 Id { get; set; }
        public Int32 MarkTemplateId { get; set; }
        public Int32 ClassId { get; set; }
        public Decimal RangeFrom { get; set; }
        public Decimal RangeTo { get; set; }
        public Int32 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int32 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public string MarksLevelName { get; set; }
        public List<List> List1 { get; set; }
    }

    public class List
    {
        public Int32 Id { get; set; }
        public Int32 MarkTemplateId { get; set; }
        public Int32 ClassId { get; set; }
        public Decimal RangeFrom { get; set; }
        public Decimal RangeTo { get; set; }
        
    }

}