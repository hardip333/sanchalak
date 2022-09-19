using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class InwardStatistics
    {
        public Int64 AcademicYearId { get; set; }
        public Int64 ExamMasterId { get; set; }
        public Int64 SpecialisationId { get; set; }
        public Int64 ProgrammePartTermId { get; set; }
        public string DisplayName { get; set; }
        public Int64 FacultyId { get; set; }
        public Int64 ProgrammeId { get; set; }
        public Int64 ProgrammePartId { get; set; }

        public Int64 MstPaperId { get; set; }
        public Int64 StudentCount { get; set; }
        public string PaperCode { get; set; }
		public string PaperName { get; set; }
		public string ExamDate { get; set; }
		public Int64? ExamSlotId { get; set; }
		public string SlotName { get; set; }
        public Int64 ProgInstPTId { get; set; }
        public Int64 IndexId { get; set; }
    }
    public class InwardStatisticsStu
    {
        public Int64 ExamMasterId { get; set; }
        public Int64 ProgInstPTId { get; set; }
        public Int64 SpecialisationId { get; set; }
        public Int64 ProgrammePartTermId { get; set; }
        public Int64 MstPaperId { get; set; }
        public Int64 PRN { get; set; }
        public string StudentName { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string InwardStatus { get; set; }
        public string InwardByTimestamp { get; set; }
        public Int64 AppearanceTypeId { get; set; }
        public string AppearanceType { get; set; }
        public Int64 IndexId { get; set; }
    }

    public class InwardStatProg
    {
        public Int64 ExamMasterId { get; set; }
        public Int64 SpecialisationId { get; set; }
        public Int64 ProgrammePartTermId { get; set; }
        public Int64 ProgInstPTId { get; set; }
        public string FacultyName { get; set; }
        public string ProgrammeName { get; set; }
        public string BranchName { get; set; }
        public string PartName { get; set; }
        public string PartShortName { get; set; }
        public string PartTermName { get; set; }
        public string PartTermShortName { get; set; }
        public string EventName { get; set; }
     
    }
}