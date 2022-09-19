using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class ExamVenueExamCenter
    {
        public Int32 Id { get; set; }
        public Int32 ExamVenueId  { get; set; }
        public string CenterName { get; set; }
        public string VenueName { get; set; }
        public bool ? IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public Int64 ExamCenterId { get; set; }
        public string DisplayName { get; set; }
        public string Code { get; set; }
        public Int64 IndexId { get; set; }
    }
    public class ExamVenueExamCenter1
    {
        public Int32 Id { get; set; }

        public string CenterName { get; set; }

        public Int32 ExamVenueExamCenterId { get; set; }

        public Int32 ExamVenueId { get; set; }

    }
}