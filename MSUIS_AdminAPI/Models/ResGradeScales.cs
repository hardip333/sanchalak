using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class ResGradeScales
    {
        public Int32 Id { get; set; }
        public string GradeScaleName { get; set; }
        public Int32 MaxGradePoints { get; set; }
        public Int32 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int32 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}