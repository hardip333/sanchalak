using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class ReallocationVenueList
    {
        public Int32 ExamMasterId { get; set; }
        public Int32 ProgrammePartTermId { get; set; }
        public Int32 ExamVenueId { get; set; }
        public Int32 PendingCapacity { get; set; }
        public string DisplayName { get; set; }

    }

    public class ReAllocationStudent

    {
        public Int32 ExamMasterId { get; set; }
        public string EventName { get; set; }
        public Int64 PRN { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public Int32 ExamVenueId { get; set; }
        public string VenueName { get; set; }
        public Int64 ProgInstancePartTermId { get; set; }
        public string InstancePartTermName { get; set; }
        public Int32 ProgrammeId { get; set; }
        public string ProgrammeName { get; set; }
        public Int32 BranchId { get; set; }
        public Int32 SpecialisationId { get; set; }
        public string BranchName { get; set; }
        public Int32 ProgrammePartTermId { get; set; }
        public string PartTermName { get; set; }
        public Int64 IndexId { get; set; }
        public Int32 ExamReVenueId { get; set; }
        public string ReAllocationPRNData { get; set; }
        public Int64 LastVenueReAllocatedBy { get; set; }
        public Int32 SelectedCheckCount { get; set; }
    }
}