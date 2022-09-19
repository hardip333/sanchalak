using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class ExamScheduleReport
    {
        public int IndexId { get; set; }
        public Int64 Id { get; set; }
        public Int64 StudentCount { get; set; }
        public Int64 ExamMasterId { get; set; }

        public string PaperName { get; set; }
        public string PaperCode { get; set; }
        public string ExamDate { get; set; }
        public string ProgrammeModeName { get; set; }
        public string ProgrammeName { get; set; }
        public string ProgrammeCode { get; set; }
        public string PartName { get; set; }
        public string BranchName { get; set; }
        public string InstancePartTermName { get; set; }
        public string PartTermName { get; set; }
        public string FacultyName { get; set; }
        public string Duration { get; set; }
        public string TeachingLearningMethodName { get; set; }
        public string AssessmentMethodName { get; set; }
        public string AssessmentType { get; set; }
    }
}