using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class HallticketInstructionConfiguration
    {
        public Int64 Id { get; set; }
        public Int32 AcademicYearId { get; set; }
        public string AcademicYearCode { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string DisplayName { get; set; }
        public string MandatoryInstructionForOnlineExam { get; set; }
        public string OptionalInstructionForOnlineExam { get; set; }
        public string MandatoryInstructionForOfflineExam { get; set; }
        public string OptionalInstructionForOfflineExam { get; set; }
        public string DyrExamSign { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
        public Boolean IsClosed { get; set; }
        public Boolean IsOnline { get; set; }
        public Boolean EditStatus { get; set; }
    }
}