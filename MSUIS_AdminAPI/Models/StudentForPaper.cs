using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class StudentForPaper
    {
        public string PRN { get; set; }

        public string NameAsPerMarksheet { get; set; }

        public string SeatNumber { get; set; }

        public string PaperName { get; set; }

        public bool IsUFMAbsent { get; set; }

        public string FlagName { get; set; }

        public string SlotName { get; set; }

        public string ExamDate { get; set; }

        public int PaperId { get; set; }

        public int ProgInstPartTermId { get; set; }

        public int BranchId { get; set; }

        public string BranchName { get; set; }

        public string DisplayName { get; set; }

    }

    public class ReportData
    {
        public string PRNList { get; set; }

        public int StudentPaperId { get; set; }
        
        public int DropDownValue { get; set; }

        public int ExamMasterId { get; set; }

    }

    public class UFMAbsentStatusReport
    {
        public string NameAsPerMarksheet { get; set; }

        public string PRN { get; set; }

        public string SeatNumber { get; set; }

        public Boolean IsUFM { get; set; }

        public Boolean IsAbsent { get; set; }
        public string Status { get; set; }

        public string PaperName { get; set;}

        public int PaperId { get; set; }

        public string SlotName { get; set; }

        public string ExamDate { get; set; }

        public int BranchId { get; set; }

        public string BranchName { get; set; }

        public string DisplayName { get; set; }
    }

    //Steffi Code Starts
    public class GetVenueListForUFMStatusReport
    {
        public int ExamVenueId { get; set; }
        public int ProgrammePartTermId { get; set; }
        public int ExamMasterId { get; set; }
        public int FacultyExamMapId { get; set; }
       public string ExamVenueName { get; set; }
        

    }

    //Steffi Code Ends
}