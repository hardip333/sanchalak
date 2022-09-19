using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class StudentBlockChange
    {
        public Int32 IndexId { get; set; }
        public Int64 ExamVenueId { get; set; }
        public Int64 ExamMasterId { get; set; }
        public Int64 ProgrammePartTermId { get; set; }
        public Int64 ProgInstancePartTermId { get; set; }
        public Int64 BranchId { get; set; }
        public Int64 ProgrammeId { get; set; }
        public Int64 ExamBlockId { get; set; }
        public Int64 ExamBlkId { get; set; }
        public Int64 MstPaperId { get; set; }
        public Int32 AvailableCapacity { get; set; }
        public Int64 PRN { get; set; }
        public Int64 IncStudentExamPaperMapId { get; set; }

        public Int32 ExamVenueExamCenterId { get; set; }
        public Int32 ExamVenExamCentId { get; set; }
        public string CenterName { get; set; }
        public string ExamCenterName { get; set; }
        public String ExamBlockName { get; set; }
        public String ExamBlkName { get; set; }
        public String ExamVenueName { get; set; }
        public String PaperCode { get; set; }
        public String PaperName { get; set; }
        public String ExamDate { get; set; }
        public String SlotName { get; set; }
        public String SeatNumber { get; set; }
        public String InstancePartTermName { get; set; }
        public String BranchName { get; set; }
        public String ProgrammeName { get; set; }
        public String VenueName { get; set; }
        public String ExamDateView { get; set; }
        public String ExamEventName { get; set; }
        public string ExamStudentBlockChange { get; set; }
        public Int64 ExamBlockIdNew { get; set; }
        public Int64 ExamBlockIdOld { get; set; }

        public List<StudentExamPaper> StudentList { get; set; }

    }
    public class StudentExamPaper
    {
        
        public Int64 ExamVenueId { get; set; }
        public Int64 ExamVenueExamCenterId { get; set; }
        public Int64 ExamMasterId { get; set; }
        public Int64 ProgrammePartTermId { get; set; }
        public Int64 BranchId { get; set; }
        public Int64 MstPaperId { get; set; }
        public Int64 ExamBlockIdNew { get; set; }
        public Int64 ExamBlockIdOld { get; set; }
        public Int64 PRN { get; set; }
        public Int64 ProgInstancePartTermId { get; set; }
        public Int64 IncStudentExamPaperMapId { get; set; }

    }
}