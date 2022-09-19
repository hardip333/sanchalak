using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using MSUISApi.BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
//using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;


namespace MSUISApi.Models
{
    public class JrSupervisorNew
    {
        public Int64 Id { get; set; }
        public Int64 ExamCenterId { get; set; }
        public string DisplayName { get; set; }
        public string Code { get; set; }
        public string InstancePartTermName { get; set; }
        public string PaperCode { get; set; }
        public string PaperName { get; set; }
        public string ExamDate { get; set; }
        public string SlotName { get; set; }
        public Int64 StudentCount { get; set; }
        public Int64 ExamVenueId { get; set; }
        public Int64 ExamEventId { get; set; }
        public Int64 IndexId { get; set; }
        public string Block_Allocation_Status { get; set; }
        public Int64 PaperId { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }

        public string FullName { get; set; }
        public Int64 PRN { get; set; }
        public string SeatNumber { get; set; }
        public string StudentSignature { get; set; }
        public string StudentPhoto { get; set; }
        public Int32? ExamBlockId { get; set; }
        public string Block_Name { get; set; }
    }
    public class JrSupervisorBlockReportNew
    {
        public Int32 ExamBlockId { get; set; }    
        public String ExamBlockName { get; set; }
        public String VanueName { get; set; }
        public Int32 StudentCount { get; set; }
        public String FromSeatNumber { get; set; }
        public String ToSeatNo { get; set; }
        public List<JrSupervisorReport> StudentList { get; set; }
    }
    public class JrSupervisorReportNew
    {
       
        public Int64 ExamEventId { get; set; }
        public Int64 IndexId { get; set; }
        public Int64 PaperId { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public string FullName { get; set; }
        public Int64 PRN { get; set; }
        public string SeatNumber { get; set; }
        public string StudentSignature { get; set; }
        public string StudentPhoto { get; set; }
        public string InstructionMediumName { get; set; }
        public Int32? ExamBlockId { get; set; }
        public Int64 ExamVenueId { get; set; }
    }

    public class ExamBlocksGetNew
    {

        public Int64 ExamEventId { get; set; }
        public Int64 PaperId { get; set; }
        public Int64 ExamVenueId { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public Int64 ExamBlockId { get; set; }
        public string ExamBlockName { get; set; }
        public Int64 BlockWiseStudentCount { get; set; }
        public Int64 ExamVenueExamCenterId { get; set; }
        
    }

    public class ExamVenueExamCenterNew
    {
        public Int32 ExamVenueId { get; set; }
        public Int32 ExamVenueExamCenterId { get; set; }
        public string CenterName { get; set; }
    }

}