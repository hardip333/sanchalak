using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class ResGPATemplateConfig
    {
        public Int32 Id { get; set; }
        public Int32 GradeTemplateId { get; set; }
        public string GradeAbbrev { get; set; }
        public decimal? MarkPercentRangeFrom { get; set; }
        public decimal? MarkPercentRangeTo { get; set; }
        public string Status { get; set; }
        public decimal? GPAFrom { get; set; }
        public decimal? GPATo { get; set; }
        public float GradePoint { get; set; }
        public Int32 GradeLevelId { get; set; }
        public Int32 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int32 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public string GradeLevelName { get; set; }
        public List<GPATemplateList> GTempConfigList { get; set; }
    }
    public class GPATemplateList
    {
        public Int32 Id { get; set; }
        public Int32 GradeTemplateId { get; set; }
        public string GradeAbbrev { get; set; }
        public decimal? MarkPercentRangeFrom { get; set; }
        public decimal? MarkPercentRangeTo { get; set; }
        public string Status { get; set; }
        public decimal? GPAFrom { get; set; }
        public decimal? GPATo { get; set; }
        public float GradePoint { get; set; }
        public Int32 GradeLevelId { get; set; }

    }
}