using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class ExamFeeConfiguration
    {
        public Int64 Id { get; set; }
        public Int64 ExamEventId { get; set; }
        public Int64 CourseScheduleId { get; set; }
        public Int64 IndexId { get; set; }
        public string DisplayName { get; set; }     
        public string ScheduleCode { get; set; }
        public string FacultyName { get; set; }
        public Int64 FacultyId { get; set; }
        public string InstituteName { get; set; }
        public string PartTermName { get; set; }
        public Decimal ExamFeeAmount { get; set; }      
        public string ExamFeeStartDate { get; set; }
        public string ExamFeeEndDateForStudent { get; set; }
        public string ExamFeeEndDateForFaculty { get; set; }
        public string ExamFeeConfig { get; set; }
        public Boolean ExamFeePublish { get; set; }
        public Int64 UserId { get; set; }

    }
}