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
    public class JrSupervisor
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
    public class JrSupervisorBlockReport
    {
        public Int32 ExamBlockId { get; set; }    
        public String ExamBlockName { get; set; }
        public String VanueName { get; set; }
        public Int32 StudentCount { get; set; }
        public String FromSeatNumber { get; set; }
        public String ToSeatNo { get; set; }
        public List<JrSupervisorReport> StudentList { get; set; }
    }
    public class JrSupervisorReport
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
        public Int64 ExamVenueExamCenterId { get; set; }
    }

    public class ExamBlocksGet
    {

        public Int64 ExamEventId { get; set; }
        public Int64 PaperId { get; set; }
        public Int64 ExamVenueId { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public Int64 ExamBlockId { get; set; }
        public string ExamBlockName { get; set; }
        public Int64 BlockWiseStudentCount { get; set; }
        
    }

    //public class PdfReportforJrSP
    //{
    //    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
    //    public static decimal loop;
    //    internal interface ILineDash
    //    {
    //        void ApplyLineDash(PdfContentByte canvas);
    //    }
    //    internal abstract class AbstractLineDash : ILineDash
    //    {
    //        private float lineWidth;
    //        public AbstractLineDash(float lineWidth) => this.lineWidth = lineWidth;
    //        public virtual void ApplyLineDash(PdfContentByte canvas) => canvas.SetLineWidth(lineWidth);
    //    }

    //    internal class SolidLine : AbstractLineDash
    //    {
    //        public SolidLine(float lineWidth) : base(lineWidth) { }
    //    }
    //    internal class DottedLine : AbstractLineDash
    //    {
    //        public DottedLine(float lineWidth) : base(lineWidth) { }
    //        public override void ApplyLineDash(PdfContentByte canvas)
    //        {
    //            base.ApplyLineDash(canvas);
    //            canvas.SetLineCap(PdfContentByte.LINE_CAP_PROJECTING_SQUARE);
    //            canvas.SetLineDash(0, 2, 1);
    //        }
    //    }
    //    internal class DashedLine : AbstractLineDash
    //    {
    //        public DashedLine(float lineWidth) : base(lineWidth) { }
    //        public override void ApplyLineDash(PdfContentByte canvas)
    //        {
    //            base.ApplyLineDash(canvas);
    //            canvas.SetLineDash(3, 3);
    //        }
    //    }
    //    internal class CustomBorder : IPdfPCellEvent
    //    {
    //        protected ILineDash left;
    //        protected ILineDash right;
    //        protected ILineDash top;
    //        protected ILineDash bottom;
    //        public CustomBorder(ILineDash left, ILineDash right, ILineDash top, ILineDash bottom)
    //        {
    //            this.left = left;
    //            this.right = right;
    //            this.top = top; this.bottom = bottom;
    //        }
    //        public void CellLayout(PdfPCell cell, Rectangle position, PdfContentByte[] canvases)
    //        {
    //            var canvas = canvases[PdfPTable.LINECANVAS];
    //            if (top != null)
    //            {
    //                canvas.SaveState();
    //                top.ApplyLineDash(canvas);
    //                canvas.MoveTo(position.Right, position.Top);
    //                canvas.LineTo(position.Left, position.Top);
    //                canvas.Stroke();
    //                canvas.RestoreState();
    //            }
    //            if (bottom != null)
    //            {
    //                canvas.SaveState();
    //                bottom.ApplyLineDash(canvas);
    //                canvas.MoveTo(position.Right, position.Bottom);
    //                canvas.LineTo(position.Left, position.Bottom);
    //                canvas.Stroke();
    //                canvas.RestoreState();
    //            }
    //            if (right != null)
    //            {
    //                canvas.SaveState();
    //                right.ApplyLineDash(canvas);
    //                canvas.MoveTo(position.Right, position.Top);
    //                canvas.LineTo(position.Right, position.Bottom);
    //                canvas.Stroke();
    //                canvas.RestoreState();
    //            }
    //            if (left != null)
    //            {
    //                canvas.SaveState();
    //                left.ApplyLineDash(canvas);
    //                canvas.MoveTo(position.Left, position.Top);
    //                canvas.LineTo(position.Left, position.Bottom);
    //                canvas.Stroke();
    //                canvas.RestoreState();
    //            }
    //        }


    //    }

    //    PdfPCell getTableCell(string content, int rowspan, int colspan, Font f, BaseColor color, int alignDirection, AbstractLineDash topBorder, AbstractLineDash rightBorder, AbstractLineDash bottomBorder, AbstractLineDash leftBorder)
    //    {
    //        PdfPCell cell = new PdfPCell(new Phrase(content, f))
    //        {
    //            Border = 0, // Do not draw normal borders
    //                        // Add your own cell renderer and draw left, right, top, bottom
    //            CellEvent = new CustomBorder(leftBorder, rightBorder, topBorder, bottomBorder),
    //        };

    //        if (alignDirection == 1)
    //            cell.HorizontalAlignment = Element.ALIGN_LEFT;
    //        else if (alignDirection == 2)
    //            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
    //        else if (alignDirection == 0)
    //            cell.HorizontalAlignment = Element.ALIGN_CENTER;
    //        cell.Colspan = colspan;
    //        cell.Rowspan = rowspan;
    //        if (color != null)
    //            cell.BackgroundColor = color;

    //        return cell;
    //    }

    //    iTextSharp.text.pdf.PdfPCell getTableCell(iTextSharp.text.Image content, int rowspan, int colspan, Font f, BaseColor color, int alignDirection, AbstractLineDash topBorder, AbstractLineDash rightBorder, AbstractLineDash bottomBorder, AbstractLineDash leftBorder)
    //    {
    //        iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(content)
    //        {
    //            Border = 0, // Do not draw normal borders
    //                        // Add your own cell renderer and draw left, right, top, bottom
    //            CellEvent = new CustomBorder(leftBorder, rightBorder, topBorder, bottomBorder),
    //        };

    //        if (alignDirection == 1)
    //            cell.HorizontalAlignment = Element.ALIGN_LEFT;
    //        else if (alignDirection == 2)
    //            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
    //        else if (alignDirection == 0)
    //            cell.HorizontalAlignment = Element.ALIGN_CENTER;
    //        cell.Colspan = colspan;
    //        cell.Rowspan = rowspan;
    //        cell.BackgroundColor = color;

    //        return cell;
    //    }

    //    PdfPCell getTableCell(Paragraph paragraphs, int rowspan, int colspan, Font f, BaseColor color, int alignDirection, AbstractLineDash topBorder, AbstractLineDash rightBorder, AbstractLineDash bottomBorder, AbstractLineDash leftBorder)
    //    {
    //        PdfPCell cell = new PdfPCell(new Phrase(paragraphs))
    //        {
    //            Border = 0, // Do not draw normal borders
    //                        // Add your own cell renderer and draw left, right, top, bottom
    //            CellEvent = new CustomBorder(leftBorder, rightBorder, topBorder, bottomBorder),
    //        };

    //        if (alignDirection == 1)
    //            cell.HorizontalAlignment = Element.ALIGN_LEFT;
    //        else if (alignDirection == 2)
    //            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
    //        else if (alignDirection == 0)
    //            cell.HorizontalAlignment = Element.ALIGN_CENTER;
    //        cell.Colspan = colspan;
    //        cell.Rowspan = rowspan;
    //        if (color != null)
    //            cell.BackgroundColor = color;

    //        return cell;
    //    }


    //    public string GeneratePDFforJrSP(JrSupervisor JRS)
    //    {
    //        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
    //        SqlDataAdapter Da = new SqlDataAdapter();
    //        DataTable Dt = new DataTable();

    //        Int64 ProgrammeInstancePartTermId = JRS.ProgrammeInstancePartTermId;
    //        Int64 ExamEventId = JRS.ExamEventId;
    //        Int64 PaperId = JRS.PaperId;



    //        SqlCommand cmd = new SqlCommand("JrSuperVisorReportList2", Con);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
    //        cmd.Parameters.AddWithValue("@ExamEventId", ExamEventId);
    //        cmd.Parameters.AddWithValue("@PaperId", PaperId);
    //        Da.SelectCommand = cmd;
    //        Da.Fill(Dt);
    //        Int32 count = 0;

    //        BALCommon common = new BALCommon();

    //        string OldBaseUrlForPhoto = "https://admission.msubaroda.ac.in/MSUISApi/Upload/Photo/";
    //        //string NewBaseUrlForPhoto = "https://localhost:44374/Upload/Photo/";
    //        string NewBaseUrlForPhoto = "https://msuis.msubaroda.ac.in/Vidhyarthi_API/Upload/Photo/";

    //        //string sPath = "";
    //        //sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/Photo/");

    //        string OldBaseUrlForSign = "https://admission.msubaroda.ac.in/MSUISApi/Upload/Signature/";
    //        //string NewBaseUrlForSign = "https://localhost:44374/Upload/Signature/";
    //        string NewBaseUrlForSign = "https://msuis.msubaroda.ac.in/Vidhyarthi_API/Upload/Signature/";

    //        //string sPathSign = "";
    //        //sPathSign = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/Signature/");


    //        List<JrSupervisor> ObjSPList2 = new List<JrSupervisor>();

    //        if (Dt.Rows.Count > 0)
    //        {
    //            foreach (DataRow dr in Dt.Rows)
    //            {
    //                JrSupervisor ObjspList2 = new JrSupervisor();
    //                ObjspList2.FullName = Convert.ToString(dr["LastName"]) + " " + Convert.ToString(dr["FirstName"]) + " " + Convert.ToString(dr["MiddleName"]);
    //                ObjspList2.PRN = Convert.ToInt64(dr["PRN"]);
    //                ObjspList2.SeatNumber = (dr["SeatNumber"].ToString());
    //                ObjspList2.StudentSignature = (dr["StudentSignature"].ToString());
    //                ObjspList2.StudentPhoto = (dr["StudentPhoto"].ToString());
    //                var ExamBlockId = dr["ExamBlockId"];
    //                if (ExamBlockId is DBNull)
    //                {
    //                    ObjspList2.ExamBlockId = null;
    //                }
    //                else
    //                {
    //                    ObjspList2.ExamBlockId = Convert.ToInt32(dr["ExamBlockId"]);
    //                }

    //                //==========Start Code for Fetching Photo from Vidhyarthi or Applicant Side============
    //                string FullPathOld = OldBaseUrlForPhoto + ObjspList2.StudentPhoto;
    //                string FullPathNew = NewBaseUrlForPhoto + ObjspList2.StudentPhoto;

    //                bool FileResponseOldPath = common.ServerFileExists(FullPathOld);
    //                bool FileResponseNewPath = common.ServerFileExists(FullPathNew);

    //                if (FileResponseNewPath == true)
    //                {
    //                    ObjspList2.StudentPhoto = FullPathNew;
    //                }
    //                else if (FileResponseOldPath == true)
    //                {
    //                    ObjspList2.StudentPhoto = FullPathOld;
    //                }
    //                else
    //                {
    //                    ObjspList2.StudentPhoto = NewBaseUrlForPhoto + "DefaultPhoto.png";
    //                }
    //                //============End Code for Fetching Photo from Vidhyarthi or Applicant Side============

    //                //============Start Code for Fetching Signature from Vidhyarthi or Applicant Side============
    //                string FullPathOldSign = OldBaseUrlForSign + ObjspList2.StudentSignature;
    //                string FullPathNewSign = NewBaseUrlForSign + ObjspList2.StudentSignature;

    //                bool FileResponseOldPathSign = common.ServerFileExists(FullPathOldSign);
    //                bool FileResponseNewPathSign = common.ServerFileExists(FullPathNewSign);

    //                if (FileResponseNewPathSign == true)
    //                {
    //                    ObjspList2.StudentSignature = FullPathNewSign;
    //                }
    //                else if (FileResponseOldPathSign == true)
    //                {
    //                    ObjspList2.StudentSignature = FullPathOldSign;
    //                }
    //                else
    //                {
    //                    ObjspList2.StudentSignature = NewBaseUrlForSign + "DefaultPhoto.png";
    //                }
    //                //===========End Code for Fetching Signature from Vidhyarthi or Applicant Side============


    //                count = count + 1;
    //                ObjspList2.IndexId = count;
    //                ObjSPList2.Add(ObjspList2);
    //            }

    //        }
    //        List<JrSupervisor> FinalListforJR = ObjSPList2;

    //        Document doc = new Document(iTextSharp.text.PageSize.A4, 35, 35, 10, 25);
    //        string filename = "sample";

    //        string path = "C:/Mohini_Soni_Data/Latest_Server_Project/Sanchalak_28_Feb_2022/MSUIS_AdminAPI/Upload/JrSuperVisor_Report/" + filename + ".pdf";



    //        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));



    //        doc.Open();
    //        //using footer class



    //        writer.PageEvent = new Footer1();



    //        PdfContentByte cd = writer.DirectContent;



    //        iTextSharp.text.Font front = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, 8);
    //        BaseColor bgColor = BaseColor.WHITE;
    //        BaseColor blackBgColor = BaseColor.BLACK;



    //        BaseFont baseFont = BaseFont.CreateFont("C:/Windows/Fonts/arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
    //        BaseFont baseBoldFont = BaseFont.CreateFont("C:/Windows/Fonts/Calibri.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
    //        Font cellFont = new Font(baseFont, 9, Font.NORMAL);
    //        Font cellFontSmall = new Font(baseFont, 7, Font.NORMAL);
    //        Font cellBoldFontSmall = new Font(baseBoldFont, 6, Font.NORMAL);
    //        //Font whitecellFont = new Font(baseFont, 6, Font.NORMAL, BaseColor.WHITE);
    //        Font cellAddressFont = new Font(baseFont, 9, Font.NORMAL);
    //        Font cellBOLDFont = new Font(baseBoldFont, 9, Font.NORMAL);
    //        Font cellBOLDFont11Size = new Font(baseBoldFont, 11, Font.NORMAL);
    //        //Font whiteCellBOLDFont = new Font(baseBoldFont, 6, Font.NORMAL, BaseColor.WHITE);
    //        var solid = new SolidLine(1);// normal
    //        var dotted = new DottedLine(0.1f);// dotted
    //        var dashed = new DashedLine(1);// dashed line



    //        //decimal Count = obj.Count;



    //        PdfPTable tableLayout = new PdfPTable(5);
    //        tableLayout.WidthPercentage = 100;
    //        tableLayout.SetWidths(new float[] { 0.8f, 1f, 1f, 1f, 1f });
    //        // tableLayout.DefaultCell.Border = Rectangle.BOX;



    //        //string imageURL = "C:/inetpub/wwwroot/MSUIS_AdminAPI/Upload/BlankMarksheet/MSU_Logo.png";
    //        string imageURL = "C:/Mohini_Soni_Data/Latest_Server_Project/Sanchalak_28_Feb_2022/MSUIS_AdminAPI/Upload/BlankMarksheet/MSU_Logo.png";
    //        Font boldFont = new Font(baseBoldFont, 8);
    //        iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
    //        jpg.ScaleAbsolute(50f, 50f);
    //        PdfPCell cell = getTableCell(jpg, 3, 1, cellFont, bgColor, 1, solid, null, solid, solid);
    //        cell.Padding = 5;
    //        tableLayout.AddCell(cell);



    //        boldFont = new Font(baseFont, 11, Font.BOLD);
    //        cell = getTableCell("The Maharaja Sayajirao University of Baroda, Vadodara", 1, 4, boldFont, bgColor, 0, solid, solid, null, null);
    //        cell.PaddingTop = 10;
    //        cell.HorizontalAlignment = Element.ALIGN_CENTER;
    //        cell.VerticalAlignment = Element.ALIGN_CENTER;
    //        tableLayout.AddCell(cell);

    //        doc.Add(tableLayout);

    //        tableLayout = new PdfPTable(5);
    //        tableLayout.WidthPercentage = 100;
    //        tableLayout.SetWidths(new float[] { 0.5f, 1.5f, 1.5f, 6f, 1.5f });


    //        cell = getTableCell("Sr No", 1, 1, cellBOLDFont, bgColor, 0, solid, solid, solid, solid);
    //        cell.PaddingBottom = 7;
    //        // cell.HorizontalAlignment = Element.ALIGN_CENTER;
    //        tableLayout.AddCell(cell);

    //        cell = getTableCell("Seat Number", 1, 1, cellBOLDFont, bgColor, 0, solid, solid, solid, solid);
    //        cell.PaddingBottom = 7;
    //        // cell.HorizontalAlignment = Element.ALIGN_CENTER;
    //        tableLayout.AddCell(cell);

    //        cell = getTableCell("PRN", 1, 1, cellBOLDFont, bgColor, 0, solid, solid, solid, solid);
    //        cell.PaddingBottom = 7;
    //        // cell.HorizontalAlignment = Element.ALIGN_CENTER;
    //        tableLayout.AddCell(cell);

    //        cell = getTableCell("Student Name", 1, 1, cellBOLDFont, bgColor, 0, solid, solid, solid, solid);
    //        cell.PaddingBottom = 7;
    //        //cell.HorizontalAlignment = Element.ALIGN_LEFT;
    //        tableLayout.AddCell(cell);


    //        cell = getTableCell("Total Marks", 1, 1, cellBOLDFont, bgColor, 0, solid, solid, solid, solid);
    //        cell.PaddingBottom = 7;
    //        // cell.HorizontalAlignment = Element.ALIGN_CENTER;
    //        tableLayout.AddCell(cell);
    //        doc.Add(tableLayout);

    //        doc.Close();
    //        writer.Close();



    //        return null;
    //    }
    //}

}