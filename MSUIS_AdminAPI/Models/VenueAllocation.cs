using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class VenueAllocation
    {
        public Int64 ExamMasterId { get; set; }
        public Int64 ProgrammeId { get; set; }
        public Int64 SpecialisationId { get; set; }
        public Int64 BranchId { get; set; }
        public Int64 ProgrammePartTermId { get; set; }
        public Int64 SeatNoGeneratedCount { get; set; }
        public Int64 VenueAllocatedCount { get; set; }
        public Int64 VenueAllocatedPendingCount { get; set; }
    }
    public class VenueList
    {
        public Int64 SpecialisationId { get; set; }
        public Int32 ExamMasterId { get; set; }
        public Int32 ProgrammePartTermId { get; set; }
        public Int32 CourseScheduleMapId { get; set; }
        public Int32 Capacity { get; set; }
        public Int32 PendingCapacity { get; set; }
        public Int32 Sequence { get; set; }
        public string ExamVenueId { get; set; }
        public string DisplayName { get; set; }
        public Int32 IndexId { get; set; }
        public bool VenueSelected { get; set; }
        public Int64 UserId { get; set; }

    }
}