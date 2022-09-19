using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSUISApi.Models
{
    public class SeatingArrangementReport
    {
        public Int32 ProgrammePartTermId { get; set; }

        public string PartBranchName { get; set; }

        public string FromSeatNumber { get; set; }

        public string ToSeatNo { get; set; }

        public string Details { get; set; }

        public string ExamDate { get; set; }
        public string ExamDate2 { get; set; }

        public string RoomNo { get; set; }

        public Int32 ExamSlotId { get; set; }

        public Int32 ExamVenueId { get; set; }

        public Int32 InstituteId { get; set; }

        public Int32 IndexId { get; set; }

        public Int32 ExamMasterId { get; set; }

        public string InstituteName { get; set; }

        public string SlotName { get; set; }

        public string TotalSeatNumber { get; set; }

        public string ExamCenterName { get; set; }

        public string PaperStartTime { get; set; }
        public string PaperEndTime { get; set; }

        public Int32 ExamVenueExamCenterId { get; set; }

        public string BlockNo { get; set; }

        public Boolean SeatingArrangeMent1 { get; set; }

        public string SeatingArrangeMent { get; set; }

    }

    public class BlockWisePaperReport
    {

        public string BlockNo { get; set; }
        public string ExamDate { get; set; }
        public Int32 ExamSlotId { get; set; }

        public Int32 ExamVenueId { get; set; }

        public Int32 InstituteId { get; set; }

        public Int32 IndexId { get; set; }

        public Int32 ExamMasterId { get; set; }

        public string InstituteName { get; set; }

        public string SlotName { get; set; }

        public Int64 ProgInstancePartTermId { get; set; }

        public string PartBranchName { get; set; }

        public string PaperName { get; set; }

        public string PaperCode { get; set; }

        public string RoomNo { get; set; }

        public string ExamCenterName { get; set; }

        public string PaperStartTime { get; set; }
        public string PaperEndTime { get; set; }

        public Int32 ExamVenueExamCenterId { get; set; }

        public string ExamVenueExamCenterName { get; set; }

        public Boolean BlockAllocationPaper1 { get; set; }

        public string BlockAllocationPaper { get; set; }

        public string StudentCount { get; set; }

        public string ExamVenueName { get; set; }

        public string ExamBlockName { get; set; }



    }
}
