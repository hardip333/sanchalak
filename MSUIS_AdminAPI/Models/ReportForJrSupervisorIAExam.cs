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
    public class JrSupervisorIA
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

    public class ProgIncPartTermGet
    {
        public Int64 BranchId { get; set; }
        public Int64 ProgrammePartTermId { get; set; }
        public Int64 InstituteId { get; set; }
        public Int64 ProgInstPartTermId { get; set; }
        public Int64 PaperId { get; set; }
        public string InstancePartTermName { get; set; }
        public Int64 ExamEventId { get; set; }
        
        public string PaperCode { get; set; }
        public string PaperName { get; set; }
        public string ExamDate { get; set; }
        public Int64 IndexId { get; set; }
    }

    public class JrSupervisorReportIA
    {
        public Int64 IndexId { get; set; }
        public string FullName { get; set; }
        public Int64 PRN { get; set; }
        public string SeatNumber { get; set; }
        public string FromSeatNo { get; set; }
        public string ToSeatNo { get; set; }
        public string TotalStudentCount { get; set; }
        public string StudentSignature { get; set; }
        public string StudentPhoto { get; set; }
        public string InstructionMediumName { get; set; }
        public string ExamEventName { get; set; }
        public string ProgrammePartTermName { get; set; }
    }

}