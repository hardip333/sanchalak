using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class ExamFormMasterListGenerated
	{
			public string FullNume { get; set; }
			public Int64 PRN { get; set; }
			public string SeatNumber { get; set; }

			public Int64 ProgInstancePartTermId { get; set; }
			

			public string FullName { get; set; }
			public string InwardDate { get; set; }

			public string InwardByTimestampview { get; set; }
			public string ExamVenueName { get; set; }
			public string ProgrammeName { get; set; }
			public string AppearanceType { get; set; }
			public string ExamMasterName { get; set; }

			public Int64 IndexId { get; set; }		
			
			public Int32 ExamMasterId { get; set; }

			public Int32 ProgrammeId { get; set; }

			public Int32 FacultyId { get; set; }
			public string FacultyName { get; set; }
			public Int32 SpecialisationId { get; set; }
			public string BranchName { get; set; }
		
			public Int32 ProgrammePartTermId { get; set; }
			public string PartTermName { get; set; }
	}

	}