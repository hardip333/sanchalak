using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSUISApi.Models
{
    public class ExamFormDiscrepancyStatistics
    {
        public Int64 Id { get; set; }
        
		public Int64 PartTermId { get; set; }
		public Int64 ProgrammeId { get; set; }
		public Int64 BranchId { get; set; }
		public Int64 ExamEventId { get; set; }
		public Int64 StudentPRN { get; set; }
        public string StudentName { get; set; }
        public string PartTermName { get; set; }
       
        public string ProgrammeName { get; set; }
        public string BranchName { get; set; }
        public string ExamEvent { get; set; }
        public string ResultStatus { get; set; }
        public string ExamFormNotInwarded { get; set; }
        public string CancelExamForm1 { get; set; }
        public Boolean IsCancelled { get; set; }
        public Int64 UserId { get; set; }
        public Int32 IndexId { get; set; }



    }


    public class ExamFormApplicantDetails
    {
        public Boolean IsCancelledBy { get; set; }
       
    }
}
