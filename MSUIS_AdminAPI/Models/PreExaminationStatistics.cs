using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class PreExaminationStatistics
    {
        public Int32 FacultyExamMapId { get; set; }

        public Int32 ExamEventId { get; set; }


        public string DisplayName { get; set; }

        public string BranchName { get; set; }
        public Int32 SpecialisationId { get; set; }

        public string ScheduleCode { get; set; }
        public Int32 FacultyId { get; set; }

        public string FacultyName { get; set; }
        public Int32 ProgrammePartTermId { get; set; }
        public string PartTermName { get; set; }
        public string ProgrammeName { get; set; }
        public string InstituteName { get; set; }
        public string AvailableStudent { get; set; }

        public string AdmittedStudents { get; set; }

        public string PaperSelected { get; set; }

        public string ExamFormGenerated { get; set; }
        public string ExamFormPending { get; set; }

        public string ExamFormInward { get; set; }
        public string SeatNoGenerated { get; set; }

        public string SeatNumberPending { get; set; }

        public string VenueStatus { get; set; }
        public Int32 IndexId { get; set; }
        public string VenueAllocation { get; set; }

        public string ExamMasterName { get; set; }

        public Int32 InstituteId { get; set; }

        public string TimeTablePublished { get; set; }

        public string HallTicketPublished { get; set; }

        public string InwardModeStudent { get; set; }

        public string NotInwardModeStudent { get; set; }

        public string ExamFeesPaid { get; set; }

        public string NOExamFeesPaid { get; set; }


    }


}