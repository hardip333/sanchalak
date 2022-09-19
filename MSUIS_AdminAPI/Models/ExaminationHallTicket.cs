using iTextSharp.text;
using iTextSharp.text.pdf;
using MSUISApi.BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using Ionic.Zip;
using iTextSharp.text.pdf.draw;

namespace MSUISApi.Models
{
    public class ExaminationHallTicket
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        internal interface ILineDash
        {
            void ApplyLineDash(PdfContentByte canvas);
        }
        internal abstract class AbstractLineDash : ILineDash
        {
            private float lineWidth;
            public AbstractLineDash(float lineWidth) => this.lineWidth = lineWidth;
            public virtual void ApplyLineDash(PdfContentByte canvas) => canvas.SetLineWidth(lineWidth);
        }

        internal class SolidLine : AbstractLineDash
        {
            public SolidLine(float lineWidth) : base(lineWidth) { }
        }
        internal class DottedLine : AbstractLineDash
        {
            public DottedLine(float lineWidth) : base(lineWidth) { }
            public override void ApplyLineDash(PdfContentByte canvas)
            {
                base.ApplyLineDash(canvas);
                canvas.SetLineCap(PdfContentByte.LINE_CAP_PROJECTING_SQUARE);
                canvas.SetLineDash(0, 2, 1);
            }
        }
        internal class DashedLine : AbstractLineDash
        {
            public DashedLine(float lineWidth) : base(lineWidth) { }
            public override void ApplyLineDash(PdfContentByte canvas)
            {
                base.ApplyLineDash(canvas);
                canvas.SetLineDash(3, 3);
            }
        }
        internal class CustomBorder : IPdfPCellEvent
        {
            protected ILineDash left;
            protected ILineDash right;
            protected ILineDash top;
            protected ILineDash bottom;
            public CustomBorder(ILineDash left, ILineDash right, ILineDash top, ILineDash bottom)
            {
                this.left = left;
                this.right = right;
                this.top = top; this.bottom = bottom;
            }
            public void CellLayout(PdfPCell cell, Rectangle position, PdfContentByte[] canvases)
            {
                var canvas = canvases[PdfPTable.LINECANVAS];
                if (top != null)
                {
                    canvas.SaveState();
                    top.ApplyLineDash(canvas);
                    canvas.MoveTo(position.Right, position.Top);
                    canvas.LineTo(position.Left, position.Top);
                    canvas.Stroke();
                    canvas.RestoreState();
                }
                if (bottom != null)
                {
                    canvas.SaveState();
                    bottom.ApplyLineDash(canvas);
                    canvas.MoveTo(position.Right, position.Bottom);
                    canvas.LineTo(position.Left, position.Bottom);
                    canvas.Stroke();
                    canvas.RestoreState();
                }
                if (right != null)
                {
                    canvas.SaveState();
                    right.ApplyLineDash(canvas);
                    canvas.MoveTo(position.Right, position.Top);
                    canvas.LineTo(position.Right, position.Bottom);
                    canvas.Stroke();
                    canvas.RestoreState();
                }
                if (left != null)
                {
                    canvas.SaveState();
                    left.ApplyLineDash(canvas);
                    canvas.MoveTo(position.Left, position.Top);
                    canvas.LineTo(position.Left, position.Bottom);
                    canvas.Stroke();
                    canvas.RestoreState();
                }
            }


        }

        private PdfPCell getTableCell(string content, int rowspan, int colspan, Font f, BaseColor color, int alignDirection, AbstractLineDash topBorder, AbstractLineDash rightBorder, AbstractLineDash bottomBorder, AbstractLineDash leftBorder)
        {
            PdfPCell cell = new PdfPCell(new Phrase(content, f))
            {
                Border = 0, // Do not draw normal borders
                            // Add your own cell renderer and draw left, right, top, bottom
                CellEvent = new CustomBorder(leftBorder, rightBorder, topBorder, bottomBorder),
            };

            if (alignDirection == 1)
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
            else if (alignDirection == 2)
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            else if (alignDirection == 0)
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = colspan;
            cell.Rowspan = rowspan;
            if (color != null)
                cell.BackgroundColor = color;

            return cell;
        }

        private iTextSharp.text.pdf.PdfPCell getTableCell(iTextSharp.text.Image content, int rowspan, int colspan, Font f, BaseColor color, int alignDirection, AbstractLineDash topBorder, AbstractLineDash rightBorder, AbstractLineDash bottomBorder, AbstractLineDash leftBorder)
        {
            iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(content)
            {
                Border = 0, // Do not draw normal borders
                            // Add your own cell renderer and draw left, right, top, bottom
                CellEvent = new CustomBorder(leftBorder, rightBorder, topBorder, bottomBorder),
            };

            if (alignDirection == 1)
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
            else if (alignDirection == 2)
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            else if (alignDirection == 0)
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = colspan;
            cell.Rowspan = rowspan;
            cell.BackgroundColor = color;

            return cell;
        }

        private PdfPCell getTableCell(Paragraph paragraphs, int rowspan, int colspan, Font f, BaseColor color, int alignDirection, AbstractLineDash topBorder, AbstractLineDash rightBorder, AbstractLineDash bottomBorder, AbstractLineDash leftBorder)
        {
            PdfPCell cell = new PdfPCell(new Phrase(paragraphs))
            {
                Border = 0, // Do not draw normal borders
                            // Add your own cell renderer and draw left, right, top, bottom
                CellEvent = new CustomBorder(leftBorder, rightBorder, topBorder, bottomBorder),
            };

            if (alignDirection == 1)
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
            else if (alignDirection == 2)
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            else if (alignDirection == 0)
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = colspan;
            cell.Rowspan = rowspan;
            if (color != null)
                cell.BackgroundColor = color;

            return cell;
        }

        //public string GenerateHallTicket(ApplicationFormForExaminationDetail dataString)
        //{
        //    try
        //    {
        //        SqlCommand cmd2 = new SqlCommand("ApplicationFormForExaminationDetails", con);
        //        SqlDataAdapter Da = new SqlDataAdapter();
        //        cmd2.CommandType = CommandType.StoredProcedure;
        //        cmd2.Parameters.AddWithValue("@PRN", dataString.PRN);
        //        DataTable Dt = new DataTable();
        //        Da.SelectCommand = cmd2;

        //        Da.Fill(Dt);
        //        if (Dt.Rows.Count > 0)
        //        {
        //            dataString.InstituteName = Dt.Rows[0][0].ToString();
        //            dataString.ExamEventMaster = Dt.Rows[0][1].ToString();
        //            dataString.FormNo = Dt.Rows[0][2].ToString();
        //            dataString.Nationality = Dt.Rows[0][3].ToString();
        //            dataString.StudentName = Dt.Rows[0][4].ToString();
        //            dataString.MotherName = Dt.Rows[0][5].ToString();
        //            dataString.Gender = Dt.Rows[0][6].ToString();
        //            dataString.Address = Dt.Rows[0][7].ToString();
        //            dataString.City = Dt.Rows[0][8].ToString();
        //            dataString.Taluka = Dt.Rows[0][8].ToString();
        //            dataString.District = Dt.Rows[0][9].ToString();
        //            dataString.State = Dt.Rows[0][10].ToString();
        //            dataString.Pincode = Dt.Rows[0][11].ToString();
        //            dataString.MobileNo = Dt.Rows[0][12].ToString();
        //            dataString.EmailId = Dt.Rows[0][13].ToString();
        //            dataString.DOB = Dt.Rows[0][14].ToString();
        //            if (!String.IsNullOrEmpty(Dt.Rows[0][15].ToString()))
        //            {
        //                if (Convert.ToBoolean(Dt.Rows[0][15].ToString()))
        //                    dataString.PhysicallyChallenged = "YES";
        //                else
        //                    dataString.PhysicallyChallenged = "NO";
        //            }
        //            dataString.SeatNumber = Dt.Rows[0][16].ToString();
        //            dataString.AppearanceType = Dt.Rows[0][17].ToString();
        //            if (!String.IsNullOrEmpty(Dt.Rows[0][18].ToString()))
        //                dataString.ExamMasterId = Convert.ToInt32(Dt.Rows[0][18].ToString());
        //            if (!String.IsNullOrEmpty(Dt.Rows[0][19].ToString()))
        //                dataString.ProgInstancePartTermId = Convert.ToInt32(Dt.Rows[0][19].ToString());
        //            dataString.ExamType = Dt.Rows[0][23].ToString();

        //            SqlCommand cmd4 = new SqlCommand("GetProgrammeDetailsByProgIstancePartTermId", con);
        //            SqlDataAdapter Da4 = new SqlDataAdapter();
        //            cmd4.CommandType = CommandType.StoredProcedure;
        //            cmd4.Parameters.AddWithValue("@ProgrammeInstancePartTermId", dataString.ProgInstancePartTermId);
        //            DataTable Dt4 = new DataTable();
        //            Da4.SelectCommand = cmd4;
        //            Da4.Fill(Dt4);

        //            string programmeDetails = string.Empty;

        //            if (Dt4.Rows.Count > 0)
        //            {
        //                string ProgrammeCode = Dt4.Rows[0][0].ToString();
        //                string ProgrammeMode = Dt4.Rows[0][1].ToString();
        //                string Branch = Dt4.Rows[0][2].ToString();
        //                string PartShortName = Dt4.Rows[0][3].ToString();
        //                string PartTermShortName = Dt4.Rows[0][4].ToString();

        //                programmeDetails = ProgrammeCode + "-" + ProgrammeMode + "-" + Branch + "-" + PartShortName + "-" + PartTermShortName;
        //            }

        //            SqlCommand cmd3 = new SqlCommand("ApplicationFormForExaminationCourseDetails", con);
        //            cmd3.CommandType = CommandType.StoredProcedure;
        //            cmd3.Parameters.AddWithValue("@PRN", dataString.PRN);
        //            cmd3.Parameters.AddWithValue("@ExamMasterId", dataString.ExamMasterId);
        //            cmd3.Parameters.AddWithValue("@ProgInstancePartTermId", dataString.ProgInstancePartTermId);
        //            DataTable Dt1 = new DataTable();
        //            Da.SelectCommand = cmd3;

        //            Da.Fill(Dt1);

        //            string path = "C:/temp/" + dataString.PRN + "MSUIS_PDF_HallTicket.pdf";
        //            Document doc = new Document(iTextSharp.text.PageSize.A4, 0, 0, 0, 0);
        //            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
        //            doc.Open();
        //            PdfContentByte cd = writer.DirectContent;

        //            iTextSharp.text.Font front = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, 8);
        //            BaseColor bgColor = BaseColor.WHITE;
        //            BaseColor blackBgColor = BaseColor.BLACK;

        //            BaseFont baseFont = BaseFont.CreateFont("C:/Windows/Fonts/arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        //            BaseFont baseBoldFont = BaseFont.CreateFont("C:/Windows/Fonts/arialbd.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        //            Font cellFont = new Font(baseFont, 7, Font.NORMAL);
        //            Font cellFontSmall = new Font(baseFont, 6, Font.NORMAL);
        //            Font cellFont8Size = new Font(baseFont, 8, Font.NORMAL);
        //            Font cellBoldFontSmall = new Font(baseBoldFont, 6, Font.NORMAL);
        //            //Font whitecellFont = new Font(baseFont, 6, Font.NORMAL, BaseColor.WHITE);
        //            Font cellAddressFont = new Font(baseFont, 7, Font.NORMAL);
        //            Font cellBOLDFont = new Font(baseBoldFont, 7, Font.NORMAL);
        //            Font cellBOLDFont11Size = new Font(baseBoldFont, 11, Font.NORMAL);
        //            //Font whiteCellBOLDFont = new Font(baseBoldFont, 6, Font.NORMAL, BaseColor.WHITE);
        //            var solid = new SolidLine(1);// normal
        //            var dotted = new DottedLine(0.1f);// dotted
        //            var dashed = new DashedLine(1);// dashed line


        //            PdfPTable layout = new PdfPTable(new float[] { 1f });
        //            //tableLayout.SetWidths(new float[] { 3f, 3f, 3f });
        //            layout.WidthPercentage = 100;
        //            layout.DefaultCell.Padding = 5;
        //            layout.DefaultCell.Border = Rectangle.NO_BORDER;

        //            PdfPTable tableLayout = new PdfPTable(1);
        //            tableLayout.WidthPercentage = 100;
        //            tableLayout.DefaultCell.Padding = 0;
        //            tableLayout.DefaultCell.Border = Rectangle.BOX;

        //            ///1 Row
        //            PdfPTable subtableLayout = new PdfPTable(12);
        //            subtableLayout.WidthPercentage = 100;
        //            subtableLayout.DefaultCell.Padding = 5;
        //            subtableLayout.DefaultCell.Border = Rectangle.BOX;


        //            string imageURL = "C:/temp/Msu_baroda_logo.jpeg";
        //            iTextSharp.text.Image MSUlogo = iTextSharp.text.Image.GetInstance(imageURL);
        //            MSUlogo.ScaleAbsolute(40f, 40f);

        //            PdfPCell cell = new PdfPCell();
        //            cell = getTableCell(MSUlogo, 7, 1, cellFont, bgColor, 1, null, null, null, null);
        //            cell.HorizontalAlignment = Element.ALIGN_CENTER;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            //cell = getTableCell(" https://msub.digitaluniversity.ac/ ", 1, 9, cellFontSmall, bgColor, 0, null, null, null, null);
        //            //cell.Padding = 2;
        //            //cell.HorizontalAlignment = Element.ALIGN_CENTER;
        //            //cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            //subtableLayout.AddCell(cell);

        //            imageURL = "C:/temp/Photo.jpeg";
        //            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
        //            jpg.ScaleAbsolute(65f, 70f);
        //            cell = getTableCell(jpg, 7, 12, cellFont, bgColor, 1, null, null, null, solid);
        //            cell.HorizontalAlignment = Element.ALIGN_CENTER;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            cell = getTableCell(" The Maharaja Sayajirao University of Baroda, Vadodara ", 1, 9, cellBOLDFont11Size, bgColor, 0, null, null, null, null);
        //            cell.Padding = 3;
        //            cell.HorizontalAlignment = Element.ALIGN_CENTER;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            cell = getTableCell(" Vadodara-390002, Gujarat (India) ", 1, 9, cellFont, bgColor, 0, null, null, null, null);
        //            cell.Padding = 2;
        //            cell.HorizontalAlignment = Element.ALIGN_CENTER;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            cell = getTableCell(" Examination Hall Ticket ", 1, 9, cellBOLDFont, bgColor, 0, null, null, null, null);
        //            cell.Padding = 2;
        //            cell.HorizontalAlignment = Element.ALIGN_CENTER;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            cell = getTableCell(" B.Sc.(Hons.)(with Credits)-Regular-FoS [BSC] CBCS 2019-BSC-I-SSBSC-I for November-2020 Examination ", 1, 9, cellFont8Size, bgColor, 0, null, null, null, null);
        //            cell.Padding = 2;
        //            cell.HorizontalAlignment = Element.ALIGN_CENTER;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            cell = getTableCell(" Faculty/College/Institution: "+ dataString.InstituteName, 1, 9, cellBOLDFont, bgColor, 0, null, null, null, null);
        //            cell.PaddingBottom = 3;
        //            cell.HorizontalAlignment = Element.ALIGN_CENTER;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            tableLayout.AddCell(subtableLayout);


        //            ///2 Row
        //            subtableLayout = new PdfPTable(24);
        //            subtableLayout.WidthPercentage = 100;
        //            subtableLayout.DefaultCell.Padding = 10;
        //            subtableLayout.DefaultCell.Border = Rectangle.BOX;

        //            cell = getTableCell(" PRN: ", 1, 2, cellFont, bgColor, 0, null, null, null, null);
        //            cell.Padding = 3;
        //            cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            cell = getTableCell(dataString.PRN.ToString(), 1, 4, cellFont, bgColor, 0, null, solid, null, null);
        //            cell.Padding = 3;
        //            cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            cell = getTableCell(" Seat Number: ", 1, 3, cellFont, bgColor, 0, null, null, null, null);
        //            cell.Padding = 3;
        //            cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            cell = getTableCell(" ", 1, 2, cellFont, bgColor, 0, null, solid, null, null);
        //            cell.Padding = 3;
        //            cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            cell = getTableCell(" Exam Center: ", 1, 3, cellFont, bgColor, 0, null, null, null, null);
        //            cell.Padding = 3;
        //            cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            cell = getTableCell(" Vadodara(1) ", 1, 4, cellBOLDFont, bgColor, 0, null, solid, null, null);
        //            cell.Padding = 3;
        //            cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            cell = getTableCell(" ", 1, 2, cellFont, bgColor, 0, null, solid, null, null);
        //            cell.Padding = 3;
        //            cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            imageURL = "C:/temp/signature.jpeg";
        //            jpg = iTextSharp.text.Image.GetInstance(imageURL);
        //            jpg.ScaleAbsolute(75f, 30f);
        //            cell = getTableCell(jpg, 3, 4, cellBOLDFont, bgColor, 1, null, null, null, null);
        //            cell.HorizontalAlignment = Element.ALIGN_CENTER;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            cell = getTableCell(" Student Name: ", 2, 3, cellFont, bgColor, 0, solid, solid, null, null);
        //            cell.Padding = 3;
        //            cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            cell = getTableCell(dataString.StudentName, 2, 9, cellFont, bgColor, 0, solid, solid, null, null);
        //            cell.Padding = 3;
        //            cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            cell = getTableCell(" Eligibility:", 2, 2, cellFont, bgColor, 0, solid, null, null, null);
        //            cell.Padding = 3;
        //            cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            cell = getTableCell(" Eligible ", 2, 2, cellBOLDFont, bgColor, 0, solid, solid, null, null);
        //            cell.Padding = 3;
        //            cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            cell = getTableCell(" Medium: ", 1, 2, cellFont, bgColor, 0, solid, null, null, null);
        //            cell.Padding = 3;
        //            cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            cell = getTableCell(" English ", 1, 2, cellBOLDFont, bgColor, 0, solid, solid, null, null);
        //            cell.Padding = 3;
        //            cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            cell = getTableCell(" Gender: ", 1, 2, cellFont, bgColor, 0, null, null, null, null);
        //            cell.Padding = 3;
        //            cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            cell = getTableCell(" Male ", 1, 2, cellBOLDFont, bgColor, 0, null, solid, null, null);
        //            cell.Padding = 3;
        //            cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            tableLayout.AddCell(subtableLayout);

        //            ///3 Row
        //            subtableLayout = new PdfPTable(15);
        //            subtableLayout.WidthPercentage = 100;
        //            subtableLayout.DefaultCell.Padding = 10;
        //            subtableLayout.DefaultCell.Border = Rectangle.BOX;

        //            //cell = getTableCell(" Vernacular Name:", 1, 2, cellFont, bgColor, 0, null, null, null, null);
        //            //cell.Padding = 3;
        //            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //            //cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            //subtableLayout.AddCell(cell);

        //            cell = getTableCell(" Exam Type: "+ dataString.ExamType, 1, 2, cellFont, bgColor, 0, null, null, null, null);
        //            cell.Padding = 3;
        //            cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);


        //            cell = getTableCell(" ", 1, 4, cellBOLDFont, bgColor, 0, null, solid, null, null);
        //            cell.Padding = 3;
        //            cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            cell = getTableCell(" Phy. Challenged:", 1, 2, cellFont, bgColor, 0, null, null, null, null);
        //            cell.Padding = 3;
        //            cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            cell = getTableCell(dataString.PhysicallyChallenged , 1, 2, cellBOLDFont, bgColor, 0, null, solid, null, null);
        //            cell.Padding = 3;
        //            cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            cell = getTableCell(" Appearance Type:", 1, 2, cellFont, bgColor, 0, null, null, null, null);
        //            cell.Padding = 3;
        //            cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            cell = getTableCell(dataString.AppearanceType, 1, 3, cellBOLDFont, bgColor, 0, null, null, null, null);
        //            cell.Padding = 3;
        //            cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            tableLayout.AddCell(subtableLayout);

        //            ///4 Row
        //            subtableLayout = new PdfPTable(1);
        //            subtableLayout.WidthPercentage = 100;
        //            subtableLayout.DefaultCell.Padding = 10;
        //            subtableLayout.DefaultCell.Border = Rectangle.BOX;

        //            cell = getTableCell(" ", 1, 1, cellFont, bgColor, 0, null, null, null, null);
        //            cell.Padding = 3;
        //            cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);



        //            tableLayout.AddCell(subtableLayout);

        //            ///5 Row
        //            subtableLayout = new PdfPTable(26);
        //            subtableLayout.WidthPercentage = 100;
        //            subtableLayout.DefaultCell.Padding = 10;
        //            subtableLayout.DefaultCell.Border = Rectangle.BOX;

        //            cell = getTableCell(" SN ", 1, 1, cellBOLDFont, bgColor, 0, null, solid, null, null);
        //            cell.Padding = 2;
        //            cell.HorizontalAlignment = Element.ALIGN_CENTER;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            cell = getTableCell(" Paper Code ", 1, 3, cellBOLDFont, bgColor, 0, null, solid, null, null);
        //            cell.Padding = 2;
        //            cell.HorizontalAlignment = Element.ALIGN_CENTER;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            cell = getTableCell(" Course Name", 1, 4, cellBOLDFont, bgColor, 0, null, solid, null, null);
        //            cell.Padding = 2;
        //            cell.HorizontalAlignment = Element.ALIGN_CENTER;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            cell = getTableCell("(UA - University Assessment,IA - Internal Assessment)", 1, 11, cellBOLDFont, bgColor, 0, null, solid, null, null);
        //            cell.Padding = 2;
        //            cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            cell = getTableCell(" Date ", 1, 2, cellBOLDFont, bgColor, 0, null, solid, null, null);
        //            cell.Padding = 2;
        //            cell.HorizontalAlignment = Element.ALIGN_CENTER;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            cell = getTableCell(" Time ", 1, 2, cellBOLDFont, bgColor, 0, null, solid, null, null);
        //            cell.Padding = 2;
        //            cell.HorizontalAlignment = Element.ALIGN_CENTER;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            cell = getTableCell("Jr. Supervisor¶s Sign", 1, 5, cellBoldFontSmall, bgColor, 0, null, null, null, null);
        //            cell.PaddingTop = 1;
        //            cell.PaddingBottom = 1;
        //            cell.HorizontalAlignment = Element.ALIGN_CENTER;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            if(Dt1.Rows.Count>0)
        //            {
        //                dataString.CourseList = new List<CourseDetails>();
        //                for(int i = 0; i < Dt1.Rows.Count; i++)
        //                {
        //                    CourseDetails element = new CourseDetails();

        //                    element.Id = Convert.ToInt32(Dt1.Rows[i][0].ToString());
        //                    element.PaperName = Dt1.Rows[i][1].ToString();
        //                    element.PaperCode = Dt1.Rows[i][2].ToString();
        //                    element.AssessmentMethod = Dt1.Rows[i][3].ToString();
        //                    element.AssetmentType = Dt1.Rows[i][4].ToString();

        //                    cell = getTableCell((i+1).ToString(), 1, 1, cellFont, bgColor, 0, solid, solid, null, null);
        //                    cell.Padding = 2;
        //                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
        //                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //                    subtableLayout.AddCell(cell);

        //                    cell = getTableCell(element.PaperCode, 1, 3, cellFont, bgColor, 0, solid, solid, null, null);
        //                    cell.Padding = 2;
        //                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
        //                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //                    subtableLayout.AddCell(cell);

        //                    cell = getTableCell(element.PaperName, 1, 11, cellFont, bgColor, 0, solid, solid, null, null);
        //                    cell.Padding = 2;
        //                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //                    subtableLayout.AddCell(cell);

        //                    cell = getTableCell(element.AssessmentMethod, 1, 3, cellFont, bgColor, 0, solid, solid, null, null);
        //                    cell.Padding = 2;
        //                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //                    subtableLayout.AddCell(cell);

        //                    cell = getTableCell(element.AssetmentType, 1, 1, cellFont, bgColor, 0, solid, solid, null, null);
        //                    cell.Padding = 2;
        //                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
        //                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //                    subtableLayout.AddCell(cell);

        //                    cell = getTableCell("", 1, 2, cellFont, bgColor, 0, solid, solid, null, null);
        //                    cell.Padding = 1;
        //                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
        //                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //                    subtableLayout.AddCell(cell);

        //                    cell = getTableCell(" ", 1, 2, cellFont, bgColor, 0, solid, solid, null, null);
        //                    cell.Padding = 2;
        //                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
        //                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //                    subtableLayout.AddCell(cell);

        //                    cell = getTableCell("  ", 1, 5, cellFont, bgColor, 0, solid, null, null, null);
        //                    cell.Padding = 2;
        //                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
        //                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //                    subtableLayout.AddCell(cell);
        //                }
        //            }
        //            tableLayout.AddCell(subtableLayout);


        //            ///6 Row
        //            subtableLayout = new PdfPTable(26);
        //            subtableLayout.WidthPercentage = 100;
        //            subtableLayout.DefaultCell.Padding = 10;
        //            subtableLayout.DefaultCell.Border = Rectangle.BOX;


        //            string text = @" Admit Card is valid if signed by Deputy Registrar (Exams). Student must preserve & produce this at each session of the Online examinations.Adoption of recent changes in thetime - table is responsibility of the  Student.Avoid any kind of Malpractice such as talking toothers, use of any other devices, moving out of camera etc.during the Online Examination.Youwill be warned through the On - Screen Arrangements, if you are observed indulging in any typeof Malpractice. If you are involved in any type of malpractice, you will be disqualified anddisciplinary action(s) will be initiated as per the University rules.";


        //            Chunk ChunkNote = new Chunk(" Note: ", cellBOLDFont);
        //            Chunk ChunkText = new Chunk(text, cellFont);
        //            Phrase p1 = new Phrase(ChunkNote);
        //            Phrase p2 = new Phrase(ChunkText);
        //            Paragraph p = new Paragraph();
        //            p.Add(p1);
        //            p.Add(p2);

        //            cell = getTableCell(p, 3, 15, cellBOLDFont, bgColor, 0, null, solid, null, null);
        //            cell.Padding = 3;
        //            cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            cell = getTableCell("  ", 3, 3, cellFont, bgColor, 0, null, solid, null, null);
        //            cell.Padding = 3;
        //            cell.HorizontalAlignment = Element.ALIGN_CENTER;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            imageURL = "C:/temp/signature.jpeg";
        //            jpg = iTextSharp.text.Image.GetInstance(imageURL);
        //            jpg.ScaleAbsolute(75f, 25f);
        //            cell = getTableCell(jpg, 2, 13, cellBOLDFont, bgColor, 1, null, null, null, null);
        //            cell.HorizontalAlignment = Element.ALIGN_CENTER;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            cell.PaddingTop = 3;
        //            subtableLayout.AddCell(cell);

        //            cell = getTableCell(" Deputy Registrar (Exams) ", 1, 13, cellBOLDFont, bgColor, 0, null, null, null, null);
        //            cell.Padding = 3;
        //            cell.HorizontalAlignment = Element.ALIGN_CENTER;
        //            cell.VerticalAlignment = Element.ALIGN_BOTTOM;
        //            subtableLayout.AddCell(cell);


        //            tableLayout.AddCell(subtableLayout);

        //            ///7 Row
        //            subtableLayout = new PdfPTable(1);
        //            subtableLayout.WidthPercentage = 100;
        //            subtableLayout.DefaultCell.Padding = 10;
        //            subtableLayout.DefaultCell.Border = Rectangle.BOX;


        //            //string strInstructions1 = "Student fulfilling the Attendance criteria shall be allowed to appear for Exams.Students are required to get ready with the Desktop / Laptop(Windows 7 or an advanced version of Operating System) with microphone and webcamOR i-Pad / Tablet / Mobile - Phone(with latest android version and an Updated version of Mozilla FireFox / Google Chrome Browser) with microphone and front facing camera facility.Ensure uninterrupted power supply(Including Power-bank for Mobile device), Internet connectivity and the required bandwidth(At least 1 MBPS) to complete the Online Examination.You will not be able to start the Online Examination, if the Desktop / Laptop / i - Pad / Tablet / Mobile - Phone does not have the camera.If the student withdraws camera / microphone permission, his / her response / answer shall not be registered.";
        //            //string strInstructions2 = "If you are using Mobile-Phone for Online Examination, please ensure that Mobile-Phone settings are done for Call Diverting so as to avoid interruption during Online Examination. ";
        //            //string strInstructions3 = "Shut down all Instant Messaging services(Skype, AIM, MSN Messenger) and email programme as they can conflict with the Online Examination.";
        //            //string strInstructions4 = "Make sure that there is sufficient light in the place where you settle for Online Examination for proper face recognition.";
        //            //string strInstructions5 = "Make sure that there is no other person/device in your nearby vicinity. Separate arrangement is considered for Blind / Differently-able Students.";
        //            //string strInstructions6 = "The Online examinations will be monitored through Artificial Intelligence Based Remote Proctoring System (AIBRPS) and scrutinized by a Vigilance Committee. During the entire session of Online Examination, your photos will be auto clicked randomly.Your screen activity will be monitored and captured during the Online Examination.";
        //            //string strInstructions7 = "Your Log IN for Online Examination will be operational at the time of Examination.";
        //            //string strInstructions8 = "Candidate shall have to preserve this Hall-ticket up to the Declaration of the Result. ";

        //            cell = getTableCell(" Instructions: ", 1, 1, cellBOLDFont, bgColor, 0, null, solid, null, null);
        //            cell.Padding = 1;
        //            cell.PaddingLeft = 3;
        //            cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            cell = getTableCell(" ",8, 1, cellBOLDFont, bgColor, 0, null, solid, null, null);
        //           // cell.Width=50f;
        //            cell.PaddingLeft = 3;
        //            cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            subtableLayout.AddCell(cell);

        //            //cell = getTableCell(strInstructions2, 1, 1, cellBOLDFont, bgColor, 0, null, solid, null, null);
        //            //cell.Padding = 0;
        //            //cell.PaddingLeft = 3;
        //            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //            //cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            //subtableLayout.AddCell(cell);

        //            //cell = getTableCell(strInstructions3, 1, 1, cellBOLDFont, bgColor, 0, null, solid, null, null);
        //            //cell.Padding = 0;
        //            //cell.PaddingLeft = 3;
        //            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //            //cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            //subtableLayout.AddCell(cell);

        //            //cell = getTableCell(strInstructions4, 1, 1, cellBOLDFont, bgColor, 0, null, solid, null, null);
        //            //cell.Padding = 0;
        //            //cell.PaddingLeft = 3;
        //            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //            //cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            //subtableLayout.AddCell(cell);

        //            //cell = getTableCell(strInstructions5, 1, 1, cellBOLDFont, bgColor, 0, null, solid, null, null);
        //            //cell.Padding = 0;
        //            //cell.PaddingLeft = 3;
        //            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //            //cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            //subtableLayout.AddCell(cell);

        //            //cell = getTableCell(strInstructions6, 1, 1, cellBOLDFont, bgColor, 0, null, solid, null, null);
        //            //cell.Padding = 0;
        //            //cell.PaddingLeft = 3;
        //            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //            //cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            //subtableLayout.AddCell(cell);

        //            //cell = getTableCell(strInstructions7, 1, 1, cellBOLDFont, bgColor, 0, null, solid, null, null);
        //            //cell.Padding = 0;
        //            //cell.PaddingLeft = 3;
        //            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //            //cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            //subtableLayout.AddCell(cell);

        //            //cell = getTableCell(strInstructions8, 1, 1, cellBOLDFont, bgColor, 0, null, solid, null, null);
        //            //cell.Padding = 0;
        //            //cell.PaddingBottom = 3;
        //            //cell.PaddingLeft = 3;
        //            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //            //cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        //            //subtableLayout.AddCell(cell);


        //            tableLayout.AddCell(subtableLayout);

        //            //tableLayout.AddCell(subtableLayout);
        //            layout.AddCell(tableLayout);


        //            doc.Add(layout);

        //            doc.Close();
        //            return path;
        //        }
        //        return null;
        //    }
        //    catch (Exception e)
        //    {
        //        return e.Message.ToString();
        //    }
        //}

        BALCommon common = new BALCommon();
        string OldBaseUrlForPhoto = "https://admission.msubaroda.ac.in/MSUISApi/Upload/Photo/";
        //string NewBaseUrlForPhoto = "https://localhost:44374/Upload/Photo/";
        string NewBaseUrlForPhoto = "https://msuis.msubaroda.ac.in/Vidhyarthi_API/Upload/Photo/";

        string OldBaseUrlForSign = "https://admission.msubaroda.ac.in/MSUISApi/Upload/Signature/";
        //string NewBaseUrlForDoc = "https://localhost:44374/Upload/Documents/";
        string NewBaseUrlForSign = "https://msuis.msubaroda.ac.in/Vidhyarthi_API/Upload/Signature/";

        //string SingleHallTicketPath = "F:/Sanchalak/MSUIS_AdminAPI/Upload/SingleHallTicket/";
        string SingleHallTicketPath = "F:/inetpub/wwwroot/MSUIS_AdminAPI/Upload/SingleHallTicket/"; // Live Path
        string DownloadSingleHallTicketPath = "https://admission.msubaroda.ac.in/MSUIS_AdminAPI/Upload/SingleHallTicket/";
        //string SingleHallTicketPath1 = "https://localhost:44374/Upload/SingleHallTicket/";

        //string DyrExamSignPath = "http://172.25.15.22/MSUIS_AdminAPI/Upload/HallTicketSignature/";
        string DyrExamSignPath = "https://admission.msubaroda.ac.in/MSUIS_AdminAPI/Upload/HallTicketSignature/"; // Live Path

        //string SingleHallTicketPath = "E:/VS Projects/Sanchalak_12Mar22_Barcode/MSUIS_AdminAPI/Upload/SingleHallTicket/";
        public string GenerateSingleHallTicket(HallTicket dataString)
        {
            int TmpErrorCnt = 0;            
            string TmpErrorExceptionMsg = "";

            try
            {
                BALExamFormMaster func = new BALExamFormMaster();
                HallTicket obj = new HallTicket();
                obj = func.getSingleHallTicketDetails(dataString);

                SqlCommand cmd4 = new SqlCommand("GetProgrammeDetailsByProgIstancePartTermId", con);
                SqlDataAdapter Da4 = new SqlDataAdapter();
                cmd4.CommandType = CommandType.StoredProcedure;
                cmd4.Parameters.AddWithValue("@ProgrammeInstancePartTermId", dataString.ProgrammeInstancePartTermId);
                DataTable Dt4 = new DataTable();
                Da4.SelectCommand = cmd4;
                Da4.Fill(Dt4);

                string programmeDetails = string.Empty;

                if (Dt4.Rows.Count > 0)
                {
                    string ProgrammeCode = Dt4.Rows[0][0].ToString();
                    string ProgrammeMode = Dt4.Rows[0][1].ToString();
                    string Branch = Dt4.Rows[0][2].ToString();
                    string PartShortName = Dt4.Rows[0][3].ToString();
                    string PartTermShortName = Dt4.Rows[0][4].ToString();

                    programmeDetails = ProgrammeCode + "-" + ProgrammeMode + "-" + Branch + "-" + PartShortName + "-" + PartTermShortName;
                }

                string details = programmeDetails + " for " + obj.ExamEventName + " Examination";

                List<CourseDetails> paperList = new List<CourseDetails>();
                paperList = func.getPaperList(dataString.ExamMasterId, dataString.ProgrammeInstancePartTermId, dataString.PRN);

                for (int i = 0; i < paperList.Count; i++)
                {
                    if (paperList[i].ExceptionMsg != null)
                    {
                        TmpErrorCnt = i + 1;
                        TmpErrorExceptionMsg = TmpErrorExceptionMsg + " " + paperList[i].ExceptionMsg;
                    }

                }
                if (TmpErrorCnt > 0)
                {
                    return TmpErrorExceptionMsg;
                }
                else
                {
                    Document doc = new Document();
                    doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 5, 5);

                    string filename = SingleHallTicketPath + dataString.PRN + "MSUIS_PDF_HallTicket.pdf";
                    PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(filename, FileMode.Create));
                    doc.Open();
                    PdfContentByte cb = writer.DirectContent;

                    iTextSharp.text.Font font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, 8);
                    BaseColor bgColor = BaseColor.WHITE;

                    PdfPTable tableLayout = new PdfPTable(5);
                    tableLayout.WidthPercentage = 100;
                    tableLayout.SetWidths(new float[] { 0.7f, 1.3f, 1f, 1f, 1f });

                    BaseFont baseFont = BaseFont.CreateFont("C:/Windows/Fonts/arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    BaseFont baseBoldFont = BaseFont.CreateFont("C:/Windows/Fonts/arialbd.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    Font cellBOLDFont11Size = new Font(baseBoldFont, 11, Font.NORMAL);
                    Font cellBOLDFont8Size = new Font(baseBoldFont, 8, Font.NORMAL);
                    Font cellFont = new Font(baseFont, 8);
                    Font cellFontSmall = new Font(baseFont, 7, Font.NORMAL);
                    var solid = new SolidLine(1);// normal
                    var dotted = new DottedLine(0.1f);// dotted
                    var dashed = new DashedLine(1);// dashed line
                                                   // Table 1
                    Font boldFont = new Font(baseFont, 8, Font.NORMAL);

                    string imageURL = SingleHallTicketPath + "MSU_Logo.png";
                    iTextSharp.text.Image MSUlogo = iTextSharp.text.Image.GetInstance(imageURL);
                    MSUlogo.ScaleAbsolute(40f, 40f);

                    PdfPCell cell = new PdfPCell();
                    cell = getTableCell(MSUlogo, 5, 1, cellFont, bgColor, 1, solid, solid, solid, solid);

                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.PaddingLeft = 5;
                    tableLayout.AddCell(cell);

                    cell = getTableCell("The Maharaja Sayajirao University of Baroda, Vadodara", 1, 3, cellBOLDFont11Size, bgColor, 0, solid, solid, solid, solid);
                    tableLayout.AddCell(cell);

                    string StudentPhoto = obj.StudentPhoto;
                    //string PhotoURL = OldBaseUrlForPhoto;

                    string NewFullPath = NewBaseUrlForPhoto + StudentPhoto;
                    bool NewFileResponse = common.ServerFileExists(NewFullPath);

                    string OldFullPath = OldBaseUrlForPhoto + StudentPhoto;
                    bool OldFileResponse = common.ServerFileExists(OldFullPath);

                    if (NewFileResponse == true)
                    {
                        StudentPhoto = NewFullPath;
                    }
                    else if (OldFileResponse == true)
                    {
                        StudentPhoto = OldFullPath;
                    }
                    else
                    {
                        StudentPhoto = NewBaseUrlForPhoto + "DefaultPhoto.png";
                    }
                    iTextSharp.text.Image Photo = iTextSharp.text.Image.GetInstance(StudentPhoto);
                    Photo.ScaleAbsolute(40f, 40f);

                    cell = getTableCell(Photo, 5, 1, cellFont, bgColor, 0, solid, solid, solid, solid);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tableLayout.AddCell(cell);

                    cell = getTableCell("Vadodara-390 002, Gujarat (India)", 1, 3, cellFont, bgColor, 0, null, null, null, null);
                    tableLayout.AddCell(cell);
                    cell = getTableCell("Examination Hall Ticket", 1, 3, cellBOLDFont8Size, bgColor, 0, null, null, null, null);
                    tableLayout.AddCell(cell);

                    cell = getTableCell(details, 1, 3, cellFont, bgColor, 0, null, null, null, null);
                    tableLayout.AddCell(cell);


                    cell = getTableCell("Faculty/College/Institution: " + obj.FacultyName, 1, 3, cellBOLDFont8Size, bgColor, 0, null, null, solid, null);
                    cell.PaddingBottom = 5;
                    tableLayout.AddCell(cell);

                    doc.Add(tableLayout);

                    //tableLayout = new PdfPTable(5);
                    tableLayout = new PdfPTable(3);
                    tableLayout.WidthPercentage = 100;
                    //tableLayout.SetWidths(new float[] { 1f, 1f, 1f, 1f, 1f });
                    tableLayout.SetWidths(new float[] { 1f, 1f, 1f });


                    Chunk ChunkPRN = new Chunk(" PRN :", cellFont);
                    Chunk Number = new Chunk(obj.PRN.ToString(), cellBOLDFont8Size);
                    Phrase p1 = new Phrase(ChunkPRN);
                    Phrase p2 = new Phrase(Number);
                    Paragraph para = new Paragraph();
                    para.Add(p1);
                    para.Add(p2);
                    cell = getTableCell(para, 1, 1, boldFont, bgColor, 1, solid, solid, solid, solid);
                    tableLayout.AddCell(cell);
                    Chunk ChunkSeatNo = new Chunk(" Seat Number :", cellFont);
                    Chunk SeatNumber = new Chunk(obj.SeatNumber.ToString(), cellBOLDFont8Size);
                    Phrase p3 = new Phrase(ChunkSeatNo);
                    Phrase p4 = new Phrase(SeatNumber);
                    Paragraph para2 = new Paragraph();
                    para2.Add(p3);
                    para2.Add(p4);
                    cell = getTableCell(para2, 1, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                    tableLayout.AddCell(cell);
                    Chunk ChunkExamCenter = new Chunk(" Exam Center:", cellFont);
                    Chunk CenterName = new Chunk(obj.ExamCenter.ToString(), cellBOLDFont8Size);
                    Phrase p5 = new Phrase(ChunkExamCenter);
                    Phrase p6 = new Phrase(CenterName);
                    Paragraph para3 = new Paragraph();
                    para3.Add(p5);
                    para3.Add(p6);
                    cell = getTableCell(para3, 1, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                    tableLayout.AddCell(cell);
                    /*cell = getTableCell("", 1, 1, cellFont, bgColor, 0, solid, solid, solid, solid);
                    tableLayout.AddCell(cell);
                    cell = getTableCell("", 1, 1, cellFont, bgColor, 0, solid, solid, solid, solid);
                    cell.PaddingBottom = 5;
                    tableLayout.AddCell(cell);*/

                    doc.Add(tableLayout);

                    tableLayout = new PdfPTable(4);
                    tableLayout.WidthPercentage = 100;
                    tableLayout.SetWidths(new float[] { 2.3f, 0.7f, 1f, 1f });

                    Chunk ChunkStudentNm = new Chunk(" Student Name:", cellFont);
                    Chunk StudentName = new Chunk(obj.NameAsPerMarksheet, cellBOLDFont8Size);
                    Phrase p7 = new Phrase(ChunkStudentNm);
                    Phrase p8 = new Phrase(StudentName);
                    Paragraph para4 = new Paragraph();
                    para4.Add(p7);
                    para4.Add(p8);
                    cell = getTableCell(para4, 2, 1, boldFont, bgColor, 1, solid, solid, solid, solid);
                    tableLayout.AddCell(cell);
                    Chunk ChunkEligibility = new Chunk(" Eligibility :", cellFont);
                    Chunk EligibilityName = new Chunk(" Eligible", cellBOLDFont8Size);
                    Phrase p9 = new Phrase(ChunkEligibility);
                    Phrase p10 = new Phrase(EligibilityName);
                    Paragraph para5 = new Paragraph();
                    para5.Add(p9);
                    para5.Add(p10);
                    cell = getTableCell(para5, 2, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                    tableLayout.AddCell(cell);
                    Chunk ChunkMedium = new Chunk(" Medium :", cellFont);
                    Chunk MediumName = new Chunk(" English", cellBOLDFont8Size);
                    Phrase p11 = new Phrase(ChunkMedium);
                    Phrase p12 = new Phrase(MediumName);
                    Paragraph para6 = new Paragraph();
                    para6.Add(p11);
                    para6.Add(p12);
                    cell = getTableCell(para6, 1, 1, cellFont, bgColor, 0, solid, solid, solid, solid);
                    tableLayout.AddCell(cell);

                    string StudentSignature = obj.StudentSignature;
                    //string SignURL = "C:/temp/signature.jpeg";

                    string NewFullPathSign = NewBaseUrlForSign + StudentSignature;
                    bool NewFileResponseSign = common.ServerFileExists(NewFullPathSign);

                    string OldFullPathSign = OldBaseUrlForSign + StudentSignature;
                    bool OldFileResponseSign = common.ServerFileExists(OldFullPathSign);

                    if (NewFileResponseSign == true)
                    {
                        StudentSignature = NewFullPathSign;
                    }
                    else if (OldFileResponseSign == true)
                    {
                        StudentSignature = OldFullPathSign;
                    }
                    else
                    {
                        StudentSignature = NewBaseUrlForSign + "DefaultSign.png";
                    }
                    iTextSharp.text.Image Sign = iTextSharp.text.Image.GetInstance(StudentSignature);
                    Sign.ScaleAbsolute(80f, 20f);

                    cell = getTableCell(Sign, 2, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                    cell.Padding = 5;
                    tableLayout.AddCell(cell);

                    Chunk ChunkGender = new Chunk(" Gender:", cellFont);
                    Chunk GenderName = new Chunk(obj.Gender, cellBOLDFont8Size);
                    Phrase p13 = new Phrase(ChunkGender);
                    Phrase p14 = new Phrase(GenderName);
                    Paragraph para7 = new Paragraph();
                    para7.Add(p13);
                    para7.Add(p14);
                    cell = getTableCell(para7, 1, 1, cellFont, bgColor, 0, solid, solid, solid, solid);
                    tableLayout.AddCell(cell);

                    doc.Add(tableLayout);

                    tableLayout = new PdfPTable(3);
                    tableLayout.WidthPercentage = 100;
                    tableLayout.SetWidths(new float[] { 1f, 1f, 1f });

                    Chunk ChunkExamType = new Chunk(" Exam Type:", cellFont);
                    Chunk ExamTypeName = new Chunk(obj.InwardMode, cellBOLDFont8Size);
                    Phrase p15 = new Phrase(ChunkExamType);
                    Phrase p16 = new Phrase(ExamTypeName);
                    Paragraph para8 = new Paragraph();
                    para8.Add(p15);
                    para8.Add(p16);
                    cell = getTableCell(para8, 1, 1, boldFont, bgColor, 1, solid, solid, solid, solid);
                    tableLayout.AddCell(cell);
                    Chunk ChunkPhyChallenged = new Chunk("Phy. Challenged:", cellFont);
                    Chunk PhyChallengedName = new Chunk(obj.IsPhysicallyChallenged.ToString(), cellBOLDFont8Size);
                    Phrase p17 = new Phrase(ChunkPhyChallenged);
                    Phrase p18 = new Phrase(PhyChallengedName);
                    Paragraph para9 = new Paragraph();
                    para9.Add(p17);
                    para9.Add(p18);
                    cell = getTableCell(para9, 1, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                    tableLayout.AddCell(cell);
                    Chunk ChunkAppearanceType = new Chunk("Appearance Type:", cellFont);
                    Chunk AppearanceType = new Chunk(obj.AppearanceType, cellBOLDFont8Size);
                    Phrase p19 = new Phrase(ChunkAppearanceType);
                    Phrase p20 = new Phrase(AppearanceType);
                    Paragraph para10 = new Paragraph();
                    para10.Add(p19);
                    para10.Add(p20);
                    cell = getTableCell(para10, 1, 1, cellFont, bgColor, 0, solid, solid, solid, solid);
                    tableLayout.AddCell(cell);

                    doc.Add(tableLayout);

                    tableLayout = new PdfPTable(1);
                    tableLayout.WidthPercentage = 100;
                    tableLayout.SetWidths(new float[] { 1f });

                    Chunk ChunkExamVenue = new Chunk(" Exam Venue:", cellFont);
                    Chunk ExamVenue = new Chunk(obj.VenueName, cellBOLDFont8Size);
                    Phrase pVenue1 = new Phrase(ChunkExamVenue);
                    Phrase pVenue2 = new Phrase(ExamVenue);
                    Paragraph paraVenue = new Paragraph();
                    paraVenue.Add(pVenue1);
                    paraVenue.Add(pVenue2);
                    cell = getTableCell(paraVenue, 1, 1, boldFont, bgColor, 1, solid, solid, solid, solid);
                    tableLayout.AddCell(cell);

                    /*cell = getTableCell(" ", 1, 1, boldFont, bgColor, 1, solid, solid, solid, solid);
                    tableLayout.AddCell(cell);*/
                    doc.Add(tableLayout);


                    tableLayout = new PdfPTable(8);
                    tableLayout.WidthPercentage = 100;
                    tableLayout.SetWidths(new float[] { 0.3f, 1f, 2.7f, 0.7f, 0.3f, 1f, 1f, 1f });

                    cell = getTableCell("SN", 1, 1, cellBOLDFont8Size, bgColor, 1, solid, solid, solid, solid);
                    tableLayout.AddCell(cell);
                    cell = getTableCell("Paper Code", 1, 1, cellBOLDFont8Size, bgColor, 1, solid, solid, solid, solid);
                    tableLayout.AddCell(cell);
                    string text = @" ( UA - University Assessment,IA - Internal Assessment )";


                    Chunk ChunkNote = new Chunk(" Course Name", cellBOLDFont8Size);
                    Chunk ChunkText = new Chunk(text, cellFont);
                    Phrase p31 = new Phrase(ChunkNote);
                    Phrase p41 = new Phrase(ChunkText);
                    Paragraph para1 = new Paragraph();
                    para1.Add(p31);
                    para1.Add(p41);
                    cell = getTableCell(para1, 1, 3, boldFont, bgColor, 1, solid, solid, solid, solid);
                    tableLayout.AddCell(cell);

                    cell = getTableCell("Date", 1, 1, cellBOLDFont8Size, bgColor, 1, solid, solid, solid, solid);
                    tableLayout.AddCell(cell);

                    cell = getTableCell("Time", 1, 1, cellBOLDFont8Size, bgColor, 1, solid, solid, solid, solid);
                    tableLayout.AddCell(cell);

                    cell = getTableCell("Jr. Supervisor's Sign.", 1, 1, cellBOLDFont8Size, bgColor, 1, solid, solid, solid, solid);
                    tableLayout.AddCell(cell);


                    doc.Add(tableLayout);

                    if (paperList.Count > 0)
                    {
                        for (int i = 0; i < paperList.Count; i++)
                        {
                            tableLayout = new PdfPTable(8);
                            tableLayout.WidthPercentage = 100;
                            tableLayout.SetWidths(new float[] { 0.3f, 1f, 2.7f, 0.7f, 0.3f, 1f, 1f, 1f });

                            cell = getTableCell((i + 1).ToString(), 1, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                            tableLayout.AddCell(cell);
                            cell = getTableCell(paperList[i].PaperCode, 1, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                            tableLayout.AddCell(cell);

                            cell = getTableCell(paperList[i].PaperName, 1, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                            tableLayout.AddCell(cell);
                            cell = getTableCell(paperList[i].AssessmentMethod, 1, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                            tableLayout.AddCell(cell);
                            cell = getTableCell(paperList[i].AssetmentType, 1, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                            tableLayout.AddCell(cell);

                            cell = getTableCell(paperList[i].PaperDate, 1, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                            tableLayout.AddCell(cell);

                            cell = getTableCell(paperList[i].PaperTime, 1, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                            tableLayout.AddCell(cell);

                            cell = getTableCell("", 1, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                            tableLayout.AddCell(cell);

                            doc.Add(tableLayout);
                        }
                    }


                    tableLayout = new PdfPTable(2);
                    tableLayout.WidthPercentage = 100;
                    tableLayout.SetWidths(new float[] { 5f, 3f });

                    cell.Rowspan = 10;
                    Chunk ChunkNote1 = new Chunk(" Note:", cellBOLDFont8Size);
                    Paragraph para11 = new Paragraph();
                    if (obj.InwardMode == "OFFLINE")
                    {
                        Chunk ChunkNoteText = new Chunk(obj.MandatoryInstructionForOfflineExam, cellFont);
                        Phrase p32 = new Phrase(ChunkNote1);
                        Phrase p42 = new Phrase(ChunkNoteText);
                        para11.Add(p32);
                        para11.Add(p42);
                    }
                    else
                    {
                        Chunk ChunkNoteText = new Chunk(obj.MandatoryInstructionForOnlineExam, cellFont);
                        Phrase p32 = new Phrase(ChunkNote1);
                        Phrase p42 = new Phrase(ChunkNoteText);
                        para11.Add(p32);
                        para11.Add(p42);
                    }

                    string DyrExamSign = DyrExamSignPath + obj.DyrExamSign;

                    bool DyrExamSignResponse = common.ServerFileExists(DyrExamSign);

                    if (DyrExamSignResponse == true)
                    {
                        obj.DyrExamSign = DyrExamSign;
                    }

                    cell = getTableCell(para11, 7, 1, cellBOLDFont8Size, bgColor, 1, solid, null, solid, solid);
                    tableLayout.AddCell(cell);
                    /*cell = getTableCell(" ", 7, 1, boldFont, bgColor, 1, solid, solid, solid, solid);
                    tableLayout.AddCell(cell);
                    cell = getTableCell("", 6, 1, boldFont, bgColor, 1, solid, solid, solid, solid);
                    tableLayout.AddCell(cell);*/

                    //cell = getTableCell("Deputy Registrar (Exams)", 1, 1, boldFont, bgColor, 1, null, solid, solid, solid);
                    //iTextSharp.text.Image DyrExamSignature = iTextSharp.text.Image.GetInstance(obj.DyrExamSign);
                    //DyrExamSignature.ScaleAbsolute(80f, 20f);
                    ////cell.PaddingBottom = 5;
                    //tableLayout.AddCell(cell);
                    //cell = getTableCell(DyrExamSignature, 1, 2, cellFont, bgColor, 1, null, solid, solid, null);
                    //cell = getTableCell(Sign, 2, 1, cellFont, bgColor, 1, null, solid, solid, null);
                    //cell.Padding = 5;
                    //tableLayout.AddCell(cell);


                    //cell = getTableCell(Sign11, 2, 1, cellFont, bgColor, 1, null, solid, solid, null);
                    //cell.Padding = 5;
                    //tableLayout.AddCell(cell);


                    iTextSharp.text.Image Sign11 = iTextSharp.text.Image.GetInstance(obj.DyrExamSign);
                    Sign11.ScaleAbsolute(120f, 50f);

                    cell = getTableCell(Sign11, 2, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                    cell.PaddingTop = 10;
                    cell.PaddingBottom = 10;
                    cell.PaddingLeft = 50;
                    tableLayout.AddCell(cell);

                    //cell = getTableCell(para11, 7, 1, cellBOLDFont8Size, bgColor, 1, null, solid, solid, solid);
                    //tableLayout.AddCell(cell);

                    //cell = getTableCell("", 7, 1, cellBOLDFont8Size, bgColor, 1, null, solid, solid, solid);
                    //tableLayout.AddCell(cell);

                    //cell = getTableCell(obj.DyrExamSign, 6, 1, boldFont, bgColor, 1, null, solid, solid, solid);
                    //tableLayout.AddCell(cell);
                    //cell = getTableCell("Deputy Registrar (Exams)", 1, 1, boldFont, bgColor, 1, null, solid, solid, solid);
                    //cell.PaddingBottom = 5;
                    //tableLayout.AddCell(cell);


                    doc.Add(tableLayout);

                    tableLayout = new PdfPTable(2);
                    tableLayout.WidthPercentage = 100;
                    tableLayout.SetWidths(new float[] { 5f, 3f });

                    cell = getTableCell("", 3, 1, boldFont, bgColor, 1, solid, solid, solid, solid);
                    tableLayout.AddCell(cell);
                    /*cell = getTableCell("", 3, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                    tableLayout.AddCell(cell);*/
                    cell = getTableCell("Deputy Registrar (Exams)", 3, 1, cellFont, bgColor, 0, solid, solid, solid, solid);
                    //tableLayout.DefaultCell.Border = Rectangle.NO_BORDER;
                    tableLayout.AddCell(cell);

                    doc.Add(tableLayout);

                    tableLayout = new PdfPTable(1);
                    tableLayout.WidthPercentage = 100;
                    tableLayout.SetWidths(new float[] { 1f });

                    Chunk ChunkIns = new Chunk(" Instructions:", cellBOLDFont8Size);
                    Paragraph para12 = new Paragraph();

                    if (obj.InwardMode == "OFFLINE")
                    {
                        Chunk ChunkInsText = new Chunk(obj.OptionalInstructionForOfflineExam, cellFont);
                        Phrase p32 = new Phrase(ChunkIns);
                        Phrase p42 = new Phrase(ChunkInsText);
                        para12.Add(p32);
                        para12.Add(p42);
                    }
                    else
                    {
                        Chunk ChunkInsText = new Chunk(obj.OptionalInstructionForOnlineExam, cellFont);
                        Phrase p32 = new Phrase(ChunkIns);
                        Phrase p42 = new Phrase(ChunkInsText);
                        para12.Add(p32);
                        para12.Add(p42);
                    }

                    cell = getTableCell(para12, 1, 1, cellBOLDFont8Size, bgColor, 1, solid, solid, solid, solid);
                    tableLayout.AddCell(cell);
                    //cell = getTableCell("-", 10, 1, boldFont, bgColor, 1, null, solid, solid, solid);
                    //tableLayout.AddCell(cell);

                    doc.Add(tableLayout);

                    Chunk glue = new Chunk(new VerticalPositionMark());
                    string datetime = DateTime.Now.ToString("dddd, dd MMMM yyyy");
                    Paragraph footer = new Paragraph(" Hall Ticket Generated on " + datetime, cellFontSmall);
                    footer.Add(new Chunk(glue));
                    //footer.Add("Page " + writer.CurrentPageNumber + " of " + BlankMarksheet.loop);
                    doc.Add(footer);
                    PdfPTable footerTbl = new PdfPTable(1);

                    footerTbl.TotalWidth = 100;

                    cell = new PdfPCell(footer);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;

                    doc.Close();
                    string FileNamePath = DownloadSingleHallTicketPath + dataString.PRN + "MSUIS_PDF_HallTicket.pdf";

                    return FileNamePath;
                }
            }
            catch (Exception e)
            {
                //return e.Message.ToString();
                string ErrorMsg = "Error :";
                return ErrorMsg + " " + e.Message;
            }
        }

        public string GenerateBulkHallTicket(HallTicket dataString)
        {
            int TmpErrorCnt = 0;
            string TmpErrorExceptionMsg = "";
            try
            {
                BALExamFormMaster func = new BALExamFormMaster();
                List<HallTicket> obj = new List<HallTicket>();
                obj = func.getBulkHallTicketList(dataString);
                string FolderName = string.Empty;

                if (obj.Count > 0)
                {
                    for (int i = 0; i < obj.Count; i++)
                    {

                        SqlCommand cmd4 = new SqlCommand("GetProgrammeDetailsByProgIstancePartTermId", con);
                        SqlDataAdapter Da4 = new SqlDataAdapter();
                        cmd4.CommandType = CommandType.StoredProcedure;
                        cmd4.Parameters.AddWithValue("@ProgrammeInstancePartTermId", obj[i].ProgrammeInstancePartTermId);
                        DataTable Dt4 = new DataTable();
                        Da4.SelectCommand = cmd4;
                        Da4.Fill(Dt4);

                        string programmeDetails = string.Empty;


                        if (Dt4.Rows.Count > 0)
                        {
                            string ProgrammeCode = Dt4.Rows[0][0].ToString();
                            string ProgrammeMode = Dt4.Rows[0][1].ToString();
                            string Branch = Dt4.Rows[0][2].ToString();
                            string PartShortName = Dt4.Rows[0][3].ToString();
                            string PartTermShortName = Dt4.Rows[0][4].ToString();

                            programmeDetails = ProgrammeCode + "-" + ProgrammeMode + "-" + Branch + "-" + PartShortName + "-" + PartTermShortName;
                            FolderName = ProgrammeCode + "-" + Branch + "1";
                        }

                        //string directory = @"C:\temp\" + FolderName;
                        string directory = @"F:\inetpub\wwwroot\MSUIS_AdminAPI\Upload\BulkHallTicket\" + FolderName;
                        //string directory = @"C:\inetpub\wwwroot\MSUIS_AdminAPI\Upload\BulkHallTicket\" + FolderName;

                        if (!Directory.Exists(directory))
                        {
                            // Directory exits!
                            Directory.CreateDirectory(directory);
                        }

                        string details = programmeDetails + " for " + obj[i].ExamEventName + " Examination";

                        List<CourseDetails> paperList = new List<CourseDetails>();
                        dataString.PRN = obj[i].PRN;
                        dataString.ProgrammeInstancePartTermId = Convert.ToInt32(obj[i].ProgrammeInstancePartTermId);
                        paperList = func.getPaperList(dataString.ExamMasterId, dataString.ProgrammeInstancePartTermId, dataString.PRN);

                        for (int j = 0; j < paperList.Count; j++)
                        {
                            if (paperList[j].ExceptionMsg != null)
                            {
                                TmpErrorCnt = j + 1;
                                TmpErrorExceptionMsg = TmpErrorExceptionMsg + " " + paperList[j].ExceptionMsg;
                            }

                        }
                        if (TmpErrorCnt > 0)
                        {
                            return TmpErrorExceptionMsg;
                        }
                        else
                        {


                        Document doc = new Document();
                        doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 5, 5);

                        //string filename = "C:/Temp/" + FolderName + "/" + obj[i].PRN + "MSUIS_PDF_HallTicket.pdf";
                        //string filename = "C:/inetpub/wwwroot/MSUIS_AdminAPI/Upload/BulkHallTicket/" + FolderName + "/" + obj[i].PRN + "MSUIS_PDF_HallTicket.pdf";
                        string filename = "F:/inetpub/wwwroot/MSUIS_AdminAPI/Upload/BulkHallTicket/" + FolderName + "/" + obj[i].PRN + "MSUIS_PDF_HallTicket.pdf";
                        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(filename, FileMode.Create));
                        doc.Open();
                        PdfContentByte cb = writer.DirectContent;

                        iTextSharp.text.Font font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, 8);
                        BaseColor bgColor = BaseColor.WHITE;

                        PdfPTable tableLayout = new PdfPTable(5);
                        tableLayout.WidthPercentage = 100;
                        tableLayout.SetWidths(new float[] { 0.7f, 1.3f, 1f, 1f, 1f });

                        BaseFont baseFont = BaseFont.CreateFont("C:/Windows/Fonts/arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                        BaseFont baseBoldFont = BaseFont.CreateFont("C:/Windows/Fonts/arialbd.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                        
                        Font cellBOLDFont11Size = new Font(baseBoldFont, 11, Font.NORMAL);
                        Font cellBOLDFont8Size = new Font(baseBoldFont, 8, Font.NORMAL);
                        Font cellFont = new Font(baseFont, 8);
                        Font cellFontSmall = new Font(baseFont, 7, Font.NORMAL);
                        var solid = new SolidLine(1);// normal
                        var dotted = new DottedLine(0.1f);// dotted
                        var dashed = new DashedLine(1);// dashed line

                        Font boldFont = new Font(baseFont, 8, Font.NORMAL);

                        //string imageURL = "C:/temp/MSU_Logo.png";
                        //iTextSharp.text.Image MSUlogo = iTextSharp.text.Image.GetInstance(imageURL);
						string imageURL = SingleHallTicketPath+ "MSU_Logo.png";
						iTextSharp.text.Image MSUlogo = iTextSharp.text.Image.GetInstance(imageURL);
                        MSUlogo.ScaleAbsolute(40f, 40f);

                        PdfPCell cell = new PdfPCell();
                        cell = getTableCell(MSUlogo, 5, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                         cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell.PaddingLeft = 5;
                        tableLayout.AddCell(cell);

                        cell = getTableCell("The Maharaja Sayajirao University of Baroda, Vadodara", 1, 3, cellBOLDFont11Size, bgColor, 0, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);

                        string StudentPhoto = obj[i].StudentPhoto;
                        //string PhotoURL = OldBaseUrlForPhoto;

                        string NewFullPath = NewBaseUrlForPhoto + StudentPhoto;
                        bool NewFileResponse = common.ServerFileExists(NewFullPath);

                        string OldFullPath = OldBaseUrlForPhoto + StudentPhoto;
                        bool OldFileResponse = common.ServerFileExists(OldFullPath);

                        if (NewFileResponse == true)
                        {
                            StudentPhoto = NewFullPath;
                        }
                        else if (OldFileResponse == true)
                        {
                            StudentPhoto = OldFullPath;
                        }
                        else
                        {
                            StudentPhoto = NewBaseUrlForPhoto + "DefaultPhoto.png";
                        }

                        //string StudentPhoto = obj[i].StudentPhoto;
                        //string PhotoURL = "C:/temp/MSU_Logo.png";
                        iTextSharp.text.Image Photo = iTextSharp.text.Image.GetInstance(StudentPhoto);
                        Photo.ScaleAbsolute(40f, 40f);

                        cell = getTableCell(Photo, 5, 1, cellFont, bgColor, 0, solid, solid, solid, solid);
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        tableLayout.AddCell(cell);

                        cell = getTableCell("Vadodara-390 002, Gujarat (India)", 1, 3, cellFont, bgColor, 0, null, null, null, null);
                        tableLayout.AddCell(cell);
                        cell = getTableCell("Examination Hall Ticket", 1, 3, cellBOLDFont8Size, bgColor, 0, null, null, null, null);
                        tableLayout.AddCell(cell);

                        cell = getTableCell(details, 1, 3, cellFont, bgColor, 0, null, null, null, null);
                        tableLayout.AddCell(cell);


                        cell = getTableCell("Faculty/College/Institution: " + obj[i].FacultyName, 1, 3, cellBOLDFont8Size, bgColor, 0, null, null, solid, null);
                        cell.PaddingBottom = 5;
                        tableLayout.AddCell(cell);

                        doc.Add(tableLayout);

                        tableLayout = new PdfPTable(3);
                        //tableLayout = new PdfPTable(5);
                        tableLayout.WidthPercentage = 100;
                        //tableLayout.SetWidths(new float[] { 1f, 1f, 1f, 1f, 1f });
                        tableLayout.SetWidths(new float[] { 1f, 1f, 1f });


                        Chunk ChunkPRN = new Chunk(" PRN :", cellFont);
                        Chunk Number = new Chunk(obj[i].PRN.ToString(), cellBOLDFont8Size);
                        Phrase p1 = new Phrase(ChunkPRN);
                        Phrase p2 = new Phrase(Number);
                        Paragraph para = new Paragraph();
                        para.Add(p1);
                        para.Add(p2);
                        cell = getTableCell(para, 1, 1, boldFont, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);
                        Chunk ChunkSeatNo = new Chunk(" Seat Number :", cellFont);
                        Chunk SeatNumber = new Chunk(obj[i].SeatNumber.ToString(), cellBOLDFont8Size);
                        Phrase p3 = new Phrase(ChunkSeatNo);
                        Phrase p4 = new Phrase(SeatNumber);
                        Paragraph para2 = new Paragraph();
                        para2.Add(p3);
                        para2.Add(p4);
                        cell = getTableCell(para2, 1, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);
                        Chunk ChunkExamCenter = new Chunk(" Exam Center:", cellFont);
                        Chunk CenterName = new Chunk(obj[i].ExamCenter.ToString(), cellBOLDFont8Size);
                        Phrase p5 = new Phrase(ChunkExamCenter);
                        Phrase p6 = new Phrase(CenterName);
                        Paragraph para3 = new Paragraph();
                        para3.Add(p5);
                        para3.Add(p6);
                        cell = getTableCell(para3, 1, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);
                        /*cell = getTableCell("", 1, 1, cellFont, bgColor, 0, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);

                        cell = getTableCell("", 1, 1, cellFont, bgColor, 0, solid, solid, solid, solid);
                        //cell.PaddingBottom = 5;
                        tableLayout.AddCell(cell);*/

                        doc.Add(tableLayout);

                        tableLayout = new PdfPTable(4);
                        tableLayout.WidthPercentage = 100;
                        tableLayout.SetWidths(new float[] { 2.3f, 0.7f, 1f, 1f });

                        Chunk ChunkStudentNm = new Chunk(" Student Name:", cellFont);
                        Chunk StudentName = new Chunk(obj[i].NameAsPerMarksheet, cellBOLDFont8Size);
                        Phrase p7 = new Phrase(ChunkStudentNm);
                        Phrase p8 = new Phrase(StudentName);
                        Paragraph para4 = new Paragraph();
                        para4.Add(p7);
                        para4.Add(p8);
                        cell = getTableCell(para4, 2, 1, boldFont, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);
                        Chunk ChunkEligibility = new Chunk(" Eligibility :", cellFont);
                        Chunk EligibilityName = new Chunk(" Eligible", cellBOLDFont8Size);
                        Phrase p9 = new Phrase(ChunkEligibility);
                        Phrase p10 = new Phrase(EligibilityName);
                        Paragraph para5 = new Paragraph();
                        para5.Add(p9);
                        para5.Add(p10);
                        cell = getTableCell(para5, 2, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);
                        Chunk ChunkMedium = new Chunk(" Medium :", cellFont);
                        Chunk MediumName = new Chunk(" English", cellBOLDFont8Size);
                        Phrase p11 = new Phrase(ChunkMedium);
                        Phrase p12 = new Phrase(MediumName);
                        Paragraph para6 = new Paragraph();
                        para6.Add(p11);
                        para6.Add(p12);
                        cell = getTableCell(para6, 1, 1, cellFont, bgColor, 0, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);

                        string StudentSignature = obj[i].StudentSignature;
                        //string SignURL = "C:/temp/signature.jpeg";

                        string NewFullPathSign = NewBaseUrlForSign + StudentSignature;
                        bool NewFileResponseSign = common.ServerFileExists(NewFullPathSign);

                        string OldFullPathSign = OldBaseUrlForSign + StudentSignature;
                        bool OldFileResponseSign = common.ServerFileExists(OldFullPathSign);

                        if (NewFileResponseSign == true)
                        {
                            StudentSignature = NewFullPathSign;
                        }
                        else if (OldFileResponseSign == true)
                        {
                            StudentSignature = OldFullPathSign;
                        }
                        else
                        {
                            StudentSignature = NewBaseUrlForSign + "DefaultSign.png";
                        }

                        //string StudentSignature = obj[i].StudentSignature;
                        //string SignURL = "C:/temp/MSU_Logo.png";
                        iTextSharp.text.Image Sign = iTextSharp.text.Image.GetInstance(StudentSignature);
                        Sign.ScaleAbsolute(80f, 20f);

                        cell = getTableCell(Sign, 2, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                        cell.Padding = 5;
                        tableLayout.AddCell(cell);
                        Chunk ChunkGender = new Chunk(" Gender:", cellFont);
                        Chunk GenderName = new Chunk(obj[i].Gender, cellBOLDFont8Size);
                        Phrase p13 = new Phrase(ChunkGender);
                        Phrase p14 = new Phrase(GenderName);
                        Paragraph para7 = new Paragraph();
                        para7.Add(p13);
                        para7.Add(p14);
                        cell = getTableCell(para7, 1, 1, cellFont, bgColor, 0, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);

                        doc.Add(tableLayout);

                        tableLayout = new PdfPTable(3);
                        tableLayout.WidthPercentage = 100;
                        tableLayout.SetWidths(new float[] { 1f, 1f, 1f });

                        Chunk ChunkExamType = new Chunk(" Exam Type:", cellFont);
                        Chunk ExamTypeName = new Chunk(obj[i].InwardMode, cellBOLDFont8Size);
                        Phrase p15 = new Phrase(ChunkExamType);
                        Phrase p16 = new Phrase(ExamTypeName);
                        Paragraph para8 = new Paragraph();
                        para8.Add(p15);
                        para8.Add(p16);
                        cell = getTableCell(para8, 1, 1, boldFont, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);
                        Chunk ChunkPhyChallenged = new Chunk("Phy. Challenged:", cellFont);
                        Chunk PhyChallengedName = new Chunk(obj[i].IsPhysicallyChallenged.ToString(), cellBOLDFont8Size);
                        Phrase p17 = new Phrase(ChunkPhyChallenged);
                        Phrase p18 = new Phrase(PhyChallengedName);
                        Paragraph para9 = new Paragraph();
                        para9.Add(p17);
                        para9.Add(p18);
                        cell = getTableCell(para9, 1, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);
                        Chunk ChunkAppearanceType = new Chunk("Appearance Type:", cellFont);
                        Chunk AppearanceType = new Chunk(obj[i].AppearanceType, cellBOLDFont8Size);
                        Phrase p19 = new Phrase(ChunkAppearanceType);
                        Phrase p20 = new Phrase(AppearanceType);
                        Paragraph para10 = new Paragraph();
                        para10.Add(p19);
                        para10.Add(p20);
                        cell = getTableCell(para10, 1, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);

                        doc.Add(tableLayout);

                        tableLayout = new PdfPTable(1);
                        tableLayout.WidthPercentage = 100;
                        tableLayout.SetWidths(new float[] { 1f });

                        Chunk ChunkExamVenue = new Chunk(" Exam Venue:", cellFont);
                        Chunk ExamVenue = new Chunk(obj[i].VenueName, cellBOLDFont8Size);
                        Phrase pVenue1 = new Phrase(ChunkExamVenue);
                        Phrase pVenue2 = new Phrase(ExamVenue);
                        Paragraph paraVenue = new Paragraph();
                        paraVenue.Add(pVenue1);
                        paraVenue.Add(pVenue2);
                        cell = getTableCell(paraVenue, 1, 1, boldFont, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);

                        /*cell = getTableCell(" ", 1, 1, boldFont, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);*/
                        doc.Add(tableLayout);


                        tableLayout = new PdfPTable(8);
                        tableLayout.WidthPercentage = 100;
                        tableLayout.SetWidths(new float[] { 0.3f, 1f, 2.7f, 0.7f, 0.3f, 1f, 1f, 1f });

                        cell = getTableCell("SN", 1, 1, cellBOLDFont8Size, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);
                        cell = getTableCell("Paper Code", 1, 1, cellBOLDFont8Size, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);
                        string text = @" ( UA - University Assessment,IA - Internal Assessment )";


                        Chunk ChunkNote = new Chunk(" Course Name", cellBOLDFont8Size);
                        Chunk ChunkText = new Chunk(text, cellFont);
                        Phrase p31 = new Phrase(ChunkNote);
                        Phrase p41 = new Phrase(ChunkText);
                        Paragraph para1 = new Paragraph();
                        para1.Add(p31);
                        para1.Add(p41);
                        cell = getTableCell(para1, 1, 3, boldFont, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);

                        cell = getTableCell("Date", 1, 1, cellBOLDFont8Size, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);

                        cell = getTableCell("Time", 1, 1, cellBOLDFont8Size, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);

                        cell = getTableCell("Jr. Supervisor's Sign.", 1, 1, cellBOLDFont8Size, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);


                        doc.Add(tableLayout);
                        if (paperList != null)
                        {
                            if (paperList.Count > 0)
                            {
                                for (int j = 0; j < paperList.Count; j++)
                                {
                                    tableLayout = new PdfPTable(8);
                                    tableLayout.WidthPercentage = 100;
                                    tableLayout.SetWidths(new float[] { 0.3f, 1f, 2.7f, 0.7f, 0.3f, 1f, 1f, 1f });

                                    cell = getTableCell((j + 1).ToString(), 1, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                                    tableLayout.AddCell(cell);
                                    cell = getTableCell(paperList[j].PaperCode, 1, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                                    tableLayout.AddCell(cell);

                                    cell = getTableCell(paperList[j].PaperName, 1, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                                    tableLayout.AddCell(cell);
                                    cell = getTableCell(paperList[j].AssessmentMethod, 1, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                                    tableLayout.AddCell(cell);
                                    cell = getTableCell(paperList[j].AssetmentType, 1, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                                    tableLayout.AddCell(cell);

                                    cell = getTableCell(paperList[j].PaperDate, 1, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                                    tableLayout.AddCell(cell);

                                    cell = getTableCell(paperList[j].PaperTime, 1, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                                    tableLayout.AddCell(cell);

                                    cell = getTableCell("", 1, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                                    tableLayout.AddCell(cell);

                                    doc.Add(tableLayout);
                                }
                            }
                        }

                        //tableLayout = new PdfPTable(3);
                        tableLayout = new PdfPTable(2);
                        tableLayout.WidthPercentage = 100;
                        tableLayout.SetWidths(new float[] { 5f, 3f });
                        //tableLayout.SetWidths(new float[] { 4f, 1f, 3f });

                        cell.Rowspan = 10;
                        Chunk ChunkNote1 = new Chunk(" Note:", cellBOLDFont8Size);
                        Paragraph para11 = new Paragraph();
                        if (obj[i].InwardMode == "OFFLINE")
                        {
                            Chunk ChunkNoteText = new Chunk(obj[i].MandatoryInstructionForOfflineExam, cellFont);
                            Phrase p32 = new Phrase(ChunkNote1);
                            Phrase p42 = new Phrase(ChunkNoteText);
                            para11.Add(p32);
                            para11.Add(p42);
                        }
                        else
                        {
                            Chunk ChunkNoteText = new Chunk(obj[i].MandatoryInstructionForOnlineExam, cellFont);
                            Phrase p32 = new Phrase(ChunkNote1);
                            Phrase p42 = new Phrase(ChunkNoteText);
                            para11.Add(p32);
                            para11.Add(p42);
                        }

                        string DyrExamSign = DyrExamSignPath + obj[i].DyrExamSign;

                        bool DyrExamSignResponse = common.ServerFileExists(DyrExamSign);

                        if (DyrExamSignResponse == true)
                        {
                            obj[i].DyrExamSign = DyrExamSign;
                        }



                        cell = getTableCell(para11, 7, 1, cellBOLDFont8Size, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);
                        /*cell = getTableCell(" ", 7, 1, boldFont, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);
                        cell = getTableCell("", 6, 1, boldFont, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);*/
                        /*cell = getTableCell("Deputy Registrar (Exams)", 1, 1, boldFont, bgColor, 1, null, solid, solid, solid);
                        cell.PaddingBottom = 5;
                        tableLayout.AddCell(cell);*/
                        //cell = getTableCell(para11, 7, 1, cellBOLDFont8Size, bgColor, 1, null, solid, solid, solid);
                        //tableLayout.AddCell(cell);

                        //cell = getTableCell("", 7, 1, cellBOLDFont8Size, bgColor, 1, null, solid, solid, solid);
                        //tableLayout.AddCell(cell);

                        //cell = getTableCell(obj.DyrExamSign, 6, 1, boldFont, bgColor, 1, null, solid, solid, solid);
                        //tableLayout.AddCell(cell);
                        //cell = getTableCell("Deputy Registrar (Exams)", 1, 1, boldFont, bgColor, 1, null, solid, solid, solid);
                        //cell.PaddingBottom = 5;
                        //tableLayout.AddCell(cell);

                        iTextSharp.text.Image Sign11 = iTextSharp.text.Image.GetInstance(obj[i].DyrExamSign);
                        Sign11.ScaleAbsolute(120f, 50f);

                        cell = getTableCell(Sign11, 2, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                        cell.PaddingTop = 10;
                        cell.PaddingBottom = 10;
                        cell.PaddingLeft = 50;
                        tableLayout.AddCell(cell);
                        doc.Add(tableLayout);

                        tableLayout = new PdfPTable(2);
                        tableLayout.WidthPercentage = 100;
                        tableLayout.SetWidths(new float[] { 5f, 3f });

                        cell = getTableCell("", 3, 1, boldFont, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);
                        /*cell = getTableCell("", 3, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);*/
                        cell = getTableCell("Deputy Registrar (Exams)", 3, 1, cellFont, bgColor, 0, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);
                        doc.Add(tableLayout);

                        tableLayout = new PdfPTable(1);
                        tableLayout.WidthPercentage = 100;
                        tableLayout.SetWidths(new float[] { 1f });

                        Chunk ChunkIns = new Chunk(" Instructions:", cellBOLDFont8Size);
                        Paragraph para12 = new Paragraph();

                        if (obj[i].InwardMode == "OFFLINE")
                        {
                            Chunk ChunkInsText = new Chunk(obj[i].OptionalInstructionForOfflineExam, cellFont);
                            Phrase p32 = new Phrase(ChunkIns);
                            Phrase p42 = new Phrase(ChunkInsText);
                            para12.Add(p32);
                            para12.Add(p42);
                        }
                        else
                        {
                            Chunk ChunkInsText = new Chunk(obj[i].OptionalInstructionForOnlineExam, cellFont);
                            Phrase p32 = new Phrase(ChunkIns);
                            Phrase p42 = new Phrase(ChunkInsText);
                            para12.Add(p32);
                            para12.Add(p42);
                        }

                        cell = getTableCell(para12, 1, 1, cellBOLDFont8Size, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);
                        //cell = getTableCell("-", 10, 1, boldFont, bgColor, 1, null, solid, solid, solid);
                        //tableLayout.AddCell(cell);

                        doc.Add(tableLayout);
                        Chunk glue = new Chunk(new VerticalPositionMark());
                        string datetime = DateTime.Now.ToString("dddd, dd MMMM yyyy");
                        Paragraph footer = new Paragraph(" Hall Ticket Generated on " + datetime, cellFontSmall);
                        footer.Add(new Chunk(glue));
                        //footer.Add("Page " + writer.CurrentPageNumber + " of " + BlankMarksheet.loop);
                        doc.Add(footer);
                        PdfPTable footerTbl = new PdfPTable(1);

                        footerTbl.TotalWidth = 100;

                        cell = new PdfPCell(footer);
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = Rectangle.NO_BORDER;
                        /*tableLayout = new PdfPTable(1);
                        tableLayout.WidthPercentage = 100;
                        tableLayout.SetWidths(new float[] { 1f });


                        cell = getTableCell("Instructions:", 1, 1, cellBOLDFont8Size, bgColor, 1, null, solid, solid, solid);
                        tableLayout.AddCell(cell);
                        if (obj[i].InwardMode == "OFFLINE")
                            cell = getTableCell(obj[i].OptionalInstructionForOfflineExam, 10, 1, boldFont, bgColor, 1, null, solid, solid, solid);
                        else
                            cell = getTableCell(obj[i].OptionalInstructionForOnlineExam, 10, 1, boldFont, bgColor, 1, null, solid, solid, solid);


                        doc.Add(tableLayout);*/

                        doc.Close();

                        }
                    }
                }

                //string path = @"C:\temp\" + FolderName;//Location for inside Test Folder  
                //string path = @"C:\inetpub\wwwroot\MSUIS_AdminAPI\Upload\BulkHallTicket\" + FolderName;//Location for inside Test Folder  
                string path = @"F:\inetpub\wwwroot\MSUIS_AdminAPI\Upload\BulkHallTicket\" + FolderName;//Location for inside Test Folder  
                string[] Filenames = Directory.GetFiles(path);
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddFiles(Filenames, "HallTickets");//Zip file inside filename  
                    //zip.Save(@"C:\temp\" + FolderName + ".zip");//location and name for creating zip file  
                    //zip.Save(@"C:\inetpub\wwwroot\MSUIS_AdminAPI\Upload\BulkHallTicket\" + FolderName + ".zip");//location and name for creating zip file  
                    zip.Save(@"F:\inetpub\wwwroot\MSUIS_AdminAPI\Upload\BulkHallTicket\" + FolderName + ".zip");//location and name for creating zip file  

                }
                /*if (Directory.Exists(path))
                {
                    //Delete all files from the Directory
                    foreach (string file in Directory.GetFiles(path))
                    {
                        File.Delete(file);
                    }
                    //Delete a Directory
                    Directory.Delete(path);
                }*/
                return "https://admission.msubaroda.ac.in/MSUIS_AdminAPI/Upload/BulkHallTicket/" + FolderName + ".zip";
                //return "http://172.25.15.22/MSUIS_AdminAPI/Upload/BulkHallTicket/" + FolderName + ".zip";
            }
            catch (Exception e)
            {
                string ErrorMsg = "Error :";
                return ErrorMsg + " " + e.Message;
            }
        }

        public string GenerateBulkHallTicketInOnePDF(HallTicket dataString)
        {
            int TmpErrorCnt = 0;
            string TmpErrorExceptionMsg = "";
            try
            {
                BALExamFormMaster func = new BALExamFormMaster();
                List<HallTicket> obj = new List<HallTicket>();
                obj = func.getBulkHallTicketList(dataString);
                string FolderName = string.Empty;

                Document doc = new Document();
                doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 5, 5);

                //string filename = "C:/Temp/MSUIS_PDF_HallTicketB1.pdf";
                string filename = "F:/inetpub/wwwroot/MSUIS_AdminAPI/Upload/BulkHallTicketInSinglePdf/" + dataString.ProgrammePartTermId + "_MSUIS_PDF_HallTicket.pdf";
                //string filename = "C:/inetpub/wwwroot/MSUIS_AdminAPI/Upload/BulkHallTicketInSinglePdf/MSUIS_PDF_HallTicketB1.pdf";
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(filename, FileMode.Create));
                doc.Open();
                PdfContentByte cb = writer.DirectContent;


                if (obj.Count > 0)
                {
                    for (int i = 0; i < obj.Count; i++)
                    {
                        SqlCommand cmd4 = new SqlCommand("GetProgrammeDetailsByProgIstancePartTermId", con);
                        SqlDataAdapter Da4 = new SqlDataAdapter();
                        cmd4.CommandType = CommandType.StoredProcedure;
                        cmd4.Parameters.AddWithValue("@ProgrammeInstancePartTermId", obj[i].ProgrammeInstancePartTermId);
                        DataTable Dt4 = new DataTable();
                        Da4.SelectCommand = cmd4;
                        Da4.Fill(Dt4);

                        string programmeDetails = string.Empty;


                        if (Dt4.Rows.Count > 0)
                        {
                            string ProgrammeCode = Dt4.Rows[0][0].ToString();
                            string ProgrammeMode = Dt4.Rows[0][1].ToString();
                            string Branch = Dt4.Rows[0][2].ToString();
                            string PartShortName = Dt4.Rows[0][3].ToString();
                            string PartTermShortName = Dt4.Rows[0][4].ToString();

                            programmeDetails = ProgrammeCode + "-" + ProgrammeMode + "-" + Branch + "-" + PartShortName + "-" + PartTermShortName;
                            FolderName = ProgrammeCode + "-" + Branch;
                        }

                        //string directory = @"C:\temp\" + FolderName;
                        string directory = @"F:\inetpub\wwwroot\MSUIS_AdminAPI\Upload\BulkHallTicketInSinglePdf\" + FolderName;
                        //string directory = @"C:\inetpub\wwwroot\MSUIS_AdminAPI\Upload\BulkHallTicketInSinglePdf\" + FolderName;

                        if (!Directory.Exists(directory))
                        {
                            // Directory exits!
                            Directory.CreateDirectory(directory);
                        }

                        string details = programmeDetails + " for " + obj[i].ExamEventName + " Examination";


                        /*List<CourseDetails> paperList = new List<CourseDetails>();
                        paperList = func.getPaperList(obj[i].ExamMasterId, obj[i].ProgrammeInstancePartTermId, obj[i].PRN);*/
                        List<CourseDetails> paperList = new List<CourseDetails>();
                        dataString.PRN = obj[i].PRN;
                        dataString.ProgrammeInstancePartTermId = Convert.ToInt32(obj[i].ProgrammeInstancePartTermId);
                        paperList = func.getPaperList(dataString.ExamMasterId, dataString.ProgrammeInstancePartTermId, dataString.PRN);

                        for (int j = 0; j < paperList.Count; j++)
                        {
                            if (paperList[j].ExceptionMsg != null)
                            {
                                TmpErrorCnt = j + 1;
                                TmpErrorExceptionMsg = TmpErrorExceptionMsg + " " + paperList[j].ExceptionMsg;
                            }
                         }
                        if (TmpErrorCnt > 0)
                        {
                            return TmpErrorExceptionMsg;
                        }
                        else
                        {


                        iTextSharp.text.Font font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, 8);
                        BaseColor bgColor = BaseColor.WHITE;

                        PdfPTable tableLayout = new PdfPTable(5);
                        tableLayout.WidthPercentage = 100;
                        tableLayout.SetWidths(new float[] { 0.7f, 1.3f, 1f, 1f, 1f });

                        /*BaseFont baseFont = BaseFont.CreateFont("C:/Users/ceila/AppData/Local/Microsoft/Windows/Fonts/Ubuntu-Regular.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                        BaseFont baseBoldFont = BaseFont.CreateFont("C:/Users/ceila/AppData/Local/Microsoft/Windows/Fonts/Ubuntu-Bold.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);*/
                        BaseFont baseFont = BaseFont.CreateFont("C:/Windows/Fonts/arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                        BaseFont baseBoldFont = BaseFont.CreateFont("C:/Windows/Fonts/arialbd.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                        Font cellBOLDFont11Size = new Font(baseBoldFont, 11, Font.NORMAL);
                        Font cellBOLDFont8Size = new Font(baseBoldFont, 8, Font.NORMAL);
                        Font cellFont = new Font(baseFont, 8);
                        Font cellFontSmall = new Font(baseFont, 7, Font.NORMAL);
                        var solid = new SolidLine(1);// normal
                        var dotted = new DottedLine(0.1f);// dotted
                        var dashed = new DashedLine(1);// dashed line
                                                       // Table 1
                        Font boldFont = new Font(baseFont, 8, Font.NORMAL);

                        /*string imageURL = "C:/temp/Msu_baroda_logo.jpeg";
                        iTextSharp.text.Image MSUlogo = iTextSharp.text.Image.GetInstance(imageURL);*/
                        string imageURL = SingleHallTicketPath + "MSU_Logo.png";
                        iTextSharp.text.Image MSUlogo = iTextSharp.text.Image.GetInstance(imageURL);
                        MSUlogo.ScaleAbsolute(40f, 40f);

                        PdfPCell cell = new PdfPCell();
                        cell = getTableCell(MSUlogo, 5, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell.PaddingLeft = 5;
                        tableLayout.AddCell(cell);

                        cell = getTableCell("The Maharaja Sayajirao University of Baroda, Vadodara", 1, 3, cellBOLDFont11Size, bgColor, 0, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);

                        string StudentPhoto = obj[i].StudentPhoto;
                        //string PhotoURL = OldBaseUrlForPhoto;

                        string NewFullPath = NewBaseUrlForPhoto + StudentPhoto;
                        bool NewFileResponse = common.ServerFileExists(NewFullPath);

                        string OldFullPath = OldBaseUrlForPhoto + StudentPhoto;
                        bool OldFileResponse = common.ServerFileExists(OldFullPath);

                        if (NewFileResponse == true)
                        {
                            StudentPhoto = NewFullPath;
                        }
                        else if (OldFileResponse == true)
                        {
                            StudentPhoto = OldFullPath;
                        }
                        else
                        {
                            StudentPhoto = NewBaseUrlForPhoto + "DefaultPhoto.png";
                        }

                        //string PhotoURL = "C:/temp/Photo.jpeg";
                        iTextSharp.text.Image Photo = iTextSharp.text.Image.GetInstance(StudentPhoto);
                        Photo.ScaleAbsolute(40f, 40f);

                        cell = getTableCell(Photo, 5, 1, cellFont, bgColor, 0, solid, solid, solid, solid);
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        tableLayout.AddCell(cell);

                        cell = getTableCell("Vadodara-390 002, Gujarat (India)", 1, 3, cellFont, bgColor, 0, null, null, null, null);
                        tableLayout.AddCell(cell);
                        cell = getTableCell("Examination Hall Ticket", 1, 3, cellBOLDFont8Size, bgColor, 0, null, null, null, null);
                        tableLayout.AddCell(cell);

                        cell = getTableCell(details, 1, 3, cellFont, bgColor, 0, null, null, null, null);
                        tableLayout.AddCell(cell);


                        cell = getTableCell("Faculty/College/Institution: " + obj[i].FacultyName, 1, 3, cellBOLDFont8Size, bgColor, 0, null, null, solid, null);
                        cell.PaddingBottom = 5;
                        tableLayout.AddCell(cell);

                        doc.Add(tableLayout);

                        tableLayout = new PdfPTable(3);
                        tableLayout.WidthPercentage = 100;
                        tableLayout.SetWidths(new float[] { 1f, 1f, 1f });


                        Chunk ChunkPRN = new Chunk(" PRN :", cellFont);
                        Chunk Number = new Chunk(obj[i].PRN.ToString(), cellBOLDFont8Size);
                        Phrase p1 = new Phrase(ChunkPRN);
                        Phrase p2 = new Phrase(Number);
                        Paragraph para = new Paragraph();
                        para.Add(p1);
                        para.Add(p2);
                        cell = getTableCell(para, 1, 1, boldFont, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);
                        Chunk ChunkSeatNo = new Chunk(" Seat Number :", cellFont);
                        Chunk SeatNumber = new Chunk(obj[i].SeatNumber.ToString(), cellBOLDFont8Size);
                        Phrase p3 = new Phrase(ChunkSeatNo);
                        Phrase p4 = new Phrase(SeatNumber);
                        Paragraph para2 = new Paragraph();
                        para2.Add(p3);
                        para2.Add(p4);
                        cell = getTableCell(para2, 1, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);
                        Chunk ChunkExamCenter = new Chunk(" Exam Center:", cellFont);
                        Chunk CenterName = new Chunk(obj[i].ExamCenter.ToString(), cellBOLDFont8Size);
                        Phrase p5 = new Phrase(ChunkExamCenter);
                        Phrase p6 = new Phrase(CenterName);
                        Paragraph para3 = new Paragraph();
                        para3.Add(p5);
                        para3.Add(p6);
                        cell = getTableCell(para3, 1, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);
                        /*cell = getTableCell("", 1, 1, cellFont, bgColor, 0, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);
                        cell = getTableCell("", 1, 1, cellFont, bgColor, 0, solid, solid, solid, solid);
                        cell.PaddingBottom = 5;
                        tableLayout.AddCell(cell);*/

                        doc.Add(tableLayout);

                        tableLayout = new PdfPTable(4);
                        tableLayout.WidthPercentage = 100;
                        tableLayout.SetWidths(new float[] { 2.3f, 0.7f, 1f, 1f });

                        Chunk ChunkStudentNm = new Chunk(" Student Name:", cellFont);
                        Chunk StudentName = new Chunk(obj[i].NameAsPerMarksheet, cellBOLDFont8Size);
                        Phrase p7 = new Phrase(ChunkStudentNm);
                        Phrase p8 = new Phrase(StudentName);
                        Paragraph para4 = new Paragraph();
                        para4.Add(p7);
                        para4.Add(p8);
                        cell = getTableCell(para4, 2, 1, boldFont, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);
                        Chunk ChunkEligibility = new Chunk(" Eligibility :", cellFont);
                        Chunk EligibilityName = new Chunk(" Eligible", cellBOLDFont8Size);
                        Phrase p9 = new Phrase(ChunkEligibility);
                        Phrase p10 = new Phrase(EligibilityName);
                        Paragraph para5 = new Paragraph();
                        para5.Add(p9);
                        para5.Add(p10);
                        cell = getTableCell(para5, 2, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);
                        Chunk ChunkMedium = new Chunk(" Medium :", cellFont);
                        Chunk MediumName = new Chunk(" English", cellBOLDFont8Size);
                        Phrase p11 = new Phrase(ChunkMedium);
                        Phrase p12 = new Phrase(MediumName);
                        Paragraph para6 = new Paragraph();
                        para6.Add(p11);
                        para6.Add(p12);
                        cell = getTableCell(para6, 1, 1, cellFont, bgColor, 0, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);

                        string StudentSignature = obj[i].StudentSignature;
                        //string SignURL = "C:/temp/signature.jpeg";

                        string NewFullPathSign = NewBaseUrlForSign + StudentSignature;
                        bool NewFileResponseSign = common.ServerFileExists(NewFullPathSign);

                        string OldFullPathSign = OldBaseUrlForSign + StudentSignature;
                        bool OldFileResponseSign = common.ServerFileExists(OldFullPathSign);

                        if (NewFileResponseSign == true)
                        {
                            StudentSignature = NewFullPathSign;
                        }
                        else if (OldFileResponseSign == true)
                        {
                            StudentSignature = OldFullPathSign;
                        }
                        else
                        {
                            StudentSignature = NewBaseUrlForSign + "DefaultSign.png";
                        }

                        //string SignURL = "C:/temp/signature.jpeg";
                        iTextSharp.text.Image Sign = iTextSharp.text.Image.GetInstance(StudentSignature);
                        Sign.ScaleAbsolute(80f, 20f);

                        cell = getTableCell(Sign, 2, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                        cell.Padding = 5;
                        tableLayout.AddCell(cell);
                        Chunk ChunkGender = new Chunk(" Gender:", cellFont);
                        Chunk GenderName = new Chunk(obj[i].Gender, cellBOLDFont8Size);
                        Phrase p13 = new Phrase(ChunkGender);
                        Phrase p14 = new Phrase(GenderName);
                        Paragraph para7 = new Paragraph();
                        para7.Add(p13);
                        para7.Add(p14);
                        cell = getTableCell(para7, 1, 1, cellFont, bgColor, 0, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);

                        doc.Add(tableLayout);

                        tableLayout = new PdfPTable(3);
                        tableLayout.WidthPercentage = 100;
                        tableLayout.SetWidths(new float[] { 1f, 1f, 1f });

                        Chunk ChunkExamType = new Chunk(" Exam Type:", cellFont);
                        Chunk ExamTypeName = new Chunk(obj[i].InwardMode, cellBOLDFont8Size);
                        Phrase p15 = new Phrase(ChunkExamType);
                        Phrase p16 = new Phrase(ExamTypeName);
                        Paragraph para8 = new Paragraph();
                        para8.Add(p15);
                        para8.Add(p16);
                        cell = getTableCell(para8, 1, 1, boldFont, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);
                        Chunk ChunkPhyChallenged = new Chunk("Phy. Challenged:", cellFont);
                        Chunk PhyChallengedName = new Chunk(obj[i].IsPhysicallyChallenged.ToString(), cellBOLDFont8Size);
                        Phrase p17 = new Phrase(ChunkPhyChallenged);
                        Phrase p18 = new Phrase(PhyChallengedName);
                        Paragraph para9 = new Paragraph();
                        para9.Add(p17);
                        para9.Add(p18);
                        cell = getTableCell(para9, 1, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);
                        Chunk ChunkAppearanceType = new Chunk("Appearance Type:", cellFont);
                        Chunk AppearanceType = new Chunk(obj[i].AppearanceType, cellBOLDFont8Size);
                        Phrase p19 = new Phrase(ChunkAppearanceType);
                        Phrase p20 = new Phrase(AppearanceType);
                        Paragraph para10 = new Paragraph();
                        para10.Add(p19);
                        para10.Add(p20);
                        cell = getTableCell(para10, 1, 1, cellFont, bgColor, 0, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);

                        doc.Add(tableLayout);

                        tableLayout = new PdfPTable(1);
                        tableLayout.WidthPercentage = 100;
                        tableLayout.SetWidths(new float[] { 1f });

                        Chunk ChunkExamVenue = new Chunk(" Exam Venue:", cellFont);
                        Chunk ExamVenue = new Chunk(obj[i].VenueName, cellBOLDFont8Size);
                        Phrase pVenue1 = new Phrase(ChunkExamVenue);
                        Phrase pVenue2 = new Phrase(ExamVenue);
                        Paragraph paraVenue = new Paragraph();
                        paraVenue.Add(pVenue1);
                        paraVenue.Add(pVenue2);
                        cell = getTableCell(paraVenue, 1, 1, boldFont, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);

                        cell = getTableCell(" ", 1, 1, boldFont, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);
                        doc.Add(tableLayout);


                        tableLayout = new PdfPTable(8);
                        tableLayout.WidthPercentage = 100;
                        tableLayout.SetWidths(new float[] { 0.3f, 1f, 2.7f, 0.7f, 0.3f, 1f, 1f, 1f });

                        cell = getTableCell("SN", 1, 1, cellBOLDFont8Size, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);
                        cell = getTableCell("Paper Code", 1, 1, cellBOLDFont8Size, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);
                        string text = @" ( UA - University Assessment,IA - Internal Assessment )";


                        Chunk ChunkNote = new Chunk(" Course Name", cellBOLDFont8Size);
                        Chunk ChunkText = new Chunk(text, cellFont);
                        Phrase p31 = new Phrase(ChunkNote);
                        Phrase p41 = new Phrase(ChunkText);
                        Paragraph para1 = new Paragraph();
                        para1.Add(p31);
                        para1.Add(p41);
                        cell = getTableCell(para1, 1, 3, boldFont, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);

                        cell = getTableCell("Date", 1, 1, cellBOLDFont8Size, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);

                        cell = getTableCell("Time", 1, 1, cellBOLDFont8Size, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);

                        cell = getTableCell("Jr. Supervisor's Sign.", 1, 1, cellBOLDFont8Size, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);


                        doc.Add(tableLayout);
                        if (paperList != null)
                        {
                            if (paperList.Count > 0)
                            {
                                for (int j = 0; j < paperList.Count; j++)
                                {
                                    tableLayout = new PdfPTable(8);
                                    tableLayout.WidthPercentage = 100;
                                    tableLayout.SetWidths(new float[] { 0.3f, 1f, 2.7f, 0.7f, 0.3f, 1f, 1f, 1f });

                                    cell = getTableCell((j + 1).ToString(), 1, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                                    tableLayout.AddCell(cell);
                                    cell = getTableCell(paperList[j].PaperCode, 1, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                                    tableLayout.AddCell(cell);

                                    cell = getTableCell(paperList[j].PaperName, 1, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                                    tableLayout.AddCell(cell);
                                    cell = getTableCell(paperList[j].AssessmentMethod, 1, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                                    tableLayout.AddCell(cell);
                                    cell = getTableCell(paperList[j].AssetmentType, 1, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                                    tableLayout.AddCell(cell);

                                    cell = getTableCell(paperList[j].PaperDate, 1, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                                    tableLayout.AddCell(cell);

                                    cell = getTableCell(paperList[j].PaperTime, 1, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                                    tableLayout.AddCell(cell);

                                    cell = getTableCell("", 1, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                                    tableLayout.AddCell(cell);

                                    doc.Add(tableLayout);
                                }
                            }
                        }
                        
                        tableLayout = new PdfPTable(2);
                        tableLayout.WidthPercentage = 100;
                        tableLayout.SetWidths(new float[] { 5f , 3f });
                        
                        cell.Rowspan = 10;
                        Chunk ChunkNote1 = new Chunk(" Note:", cellBOLDFont8Size);

                        Paragraph para11 = new Paragraph();
                        if (obj[i].InwardMode == "OFFLINE")
                        {
                            Chunk ChunkNoteText = new Chunk(obj[i].MandatoryInstructionForOfflineExam, cellFont);
                            Phrase p32 = new Phrase(ChunkNote1);
                            Phrase p42 = new Phrase(ChunkNoteText);
                            para11.Add(p32);
                            para11.Add(p42);
                        }
                        else
                        {
                            Chunk ChunkNoteText = new Chunk(obj[i].MandatoryInstructionForOnlineExam, cellFont);
                            Phrase p32 = new Phrase(ChunkNote1);
                            Phrase p42 = new Phrase(ChunkNoteText);
                            para11.Add(p32);
                            para11.Add(p42);
                        }

                        string DyrExamSign = DyrExamSignPath + obj[i].DyrExamSign;

                        bool DyrExamSignResponse = common.ServerFileExists(DyrExamSign);

                        if (DyrExamSignResponse == true)
                        {
                            obj[i].DyrExamSign = DyrExamSign;
                        }

                        cell.Rowspan = 10;
                        cell = getTableCell(para11, 7, 1, cellBOLDFont8Size, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);
                        /*cell = getTableCell(" ", 7, 1, boldFont, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);
                        cell = getTableCell("", 6, 1, boldFont, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);*/
                        /*cell = getTableCell("Deputy Registrar (Exams)", 1, 1, boldFont, bgColor, 1, null, solid, solid, solid);
                        cell.PaddingBottom = 5;
                        tableLayout.AddCell(cell);*/

                        iTextSharp.text.Image Sign11 = iTextSharp.text.Image.GetInstance(obj[i].DyrExamSign);
                        Sign11.ScaleAbsolute(120f, 50f);

                        cell = getTableCell(Sign11, 2, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                        cell.PaddingTop = 10;
                        cell.PaddingBottom = 10;
                        cell.PaddingLeft = 50;
                        tableLayout.AddCell(cell);
                        doc.Add(tableLayout);

                        tableLayout = new PdfPTable(2);
                        tableLayout.WidthPercentage = 100;
                        tableLayout.SetWidths(new float[] { 5f, 3f });

                        cell = getTableCell("", 3, 1, boldFont, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);
                        /*cell = getTableCell("", 3, 1, cellFont, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);*/
                        cell = getTableCell("Deputy Registrar (Exams)", 3, 1, cellFont, bgColor, 0, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);                      

                        doc.Add(tableLayout);

                        tableLayout = new PdfPTable(1);
                        tableLayout.WidthPercentage = 100;
                        tableLayout.SetWidths(new float[] { 1f });

                        Chunk ChunkIns = new Chunk(" Instructions:", cellBOLDFont8Size);
                        Paragraph para12 = new Paragraph();

                        if (obj[i].InwardMode == "OFFLINE")
                        {
                            Chunk ChunkInsText = new Chunk(obj[i].OptionalInstructionForOfflineExam, cellFont);
                            Phrase p32 = new Phrase(ChunkIns);
                            Phrase p42 = new Phrase(ChunkInsText);
                            para12.Add(p32);
                            para12.Add(p42);
                        }
                        else
                        {
                            Chunk ChunkInsText = new Chunk(obj[i].OptionalInstructionForOnlineExam, cellFont);
                            Phrase p32 = new Phrase(ChunkIns);
                            Phrase p42 = new Phrase(ChunkInsText);
                            para12.Add(p32);
                            para12.Add(p42);
                        }

                        cell = getTableCell(para12, 1, 1, cellBOLDFont8Size, bgColor, 1, solid, solid, solid, solid);
                        tableLayout.AddCell(cell);
                        /*cell = getTableCell("Instructions:", 1, 1, cellBOLDFont8Size, bgColor, 1, null, solid, solid, solid);
                        tableLayout.AddCell(cell);
                        cell = getTableCell("-", 10, 1, boldFont, bgColor, 1, null, solid, solid, solid);
*/

                        doc.Add(tableLayout);

                        Chunk glue = new Chunk(new VerticalPositionMark());
                        string datetime = DateTime.Now.ToString("dddd, dd MMMM yyyy");
                        Paragraph footer = new Paragraph(" Hall Ticket Generated on " + datetime, cellFontSmall);
                        footer.Add(new Chunk(glue));
                        //footer.Add("Page " + writer.CurrentPageNumber + " of " + BlankMarksheet.loop);
                        doc.Add(footer);
                        PdfPTable footerTbl = new PdfPTable(1);

                        footerTbl.TotalWidth = 100;

                        cell = new PdfPCell(footer);
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = Rectangle.NO_BORDER;
                        doc.NewPage();

						}
                    }
                }

                doc.Close();

                //string path = @"C:\temp\" + FolderName;//Location for inside Test Folder  
                //string[] Filenames = Directory.GetFiles(path);
                //using (ZipFile zip = new ZipFile())
                //{
                //    zip.AddFiles(Filenames, "HallTickets");//Zip file inside filename  
                //    zip.Save(@"C:\temp\" + FolderName + ".zip");//location and name for creating zip file  

                //}
                string filenamePath = "https://admission.msubaroda.ac.in/MSUIS_AdminAPI/Upload/BulkHallTicketInSinglePdf/" + dataString.ProgrammePartTermId + "_MSUIS_PDF_HallTicket.pdf";

                //return filename;
                return filenamePath;

                
            }
            catch (Exception e)
            {
                string ErrorMsg = "Error :";
                return ErrorMsg + " " + e.Message;
            }
        }

    }
}