using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
   public class PaperListGetByExamFormMaster
    {
		public Int64 ProgInstancePartTermId { get; set; }

		public Int64 PRN { get; set; }

		public string PaperName { get; set; }

		public string PaperCode { get; set; }

		public string ExamDate { get; set; }

		public string SlotName { get; set; }

		public string ExamBlockName { get; set; }

		public Int32 ExamMasterId { get; set; }

		public Int64 IndexId { get; set; }

		public string ExamFeesPaidStatus { get; set; }

		public string ExamMasterName { get; set; }

		public string InstancePartTermName { get; set; }


	}
}