using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using MSUISApi.BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class Sectionwiseblankmarksheet1
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        public static decimal loop;
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

        PdfPCell getTableCell(string content, int rowspan, int colspan, Font f, BaseColor color, int alignDirection, AbstractLineDash topBorder, AbstractLineDash rightBorder, AbstractLineDash bottomBorder, AbstractLineDash leftBorder)
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

        iTextSharp.text.pdf.PdfPCell getTableCell(iTextSharp.text.Image content, int rowspan, int colspan, Font f, BaseColor color, int alignDirection, AbstractLineDash topBorder, AbstractLineDash rightBorder, AbstractLineDash bottomBorder, AbstractLineDash leftBorder)
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

        PdfPCell getTableCell(Paragraph paragraphs, int rowspan, int colspan, Font f, BaseColor color, int alignDirection, AbstractLineDash topBorder, AbstractLineDash rightBorder, AbstractLineDash bottomBorder, AbstractLineDash leftBorder)
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
        public string GenerateBlankMarksheet(Sectionwiseblankmarksheet dataString)
        {
            int TmpErrorCnt = 0;
            int TmpErrorIndex = -1;
            string TmpErrorExceptionMsg = "Error :";

            try
            {
                BALPaper func1 = new BALPaper();
                MstPaper objPaper = func1.MstPaperByIdandSection(dataString.PaperId, "UA", dataString.TeachingLearningMethodId, dataString.AssessmentMethodId, dataString.SectionName);
                //if (dataString.TeachingLearningMethodId == 5 && dataString.AssessmentMethodId == 2)
                //{
                    BALExamFormMaster func = new BALExamFormMaster();
                    List<ExamFormMaster> obj = new List<ExamFormMaster>();
                    obj = func.getBlankMarksheetDetailsForUASectionwise(dataString);
                    for (int i = 0; i < obj.Count; i++)
                    {
                        if (obj[i].ExceptionMsg != null)
                        {
                            TmpErrorIndex = i;
                            TmpErrorCnt = i + 1;
                            TmpErrorExceptionMsg = TmpErrorExceptionMsg + " " + obj[i].ExceptionMsg;
                        }

                    }
                    if (TmpErrorCnt > 0)
                    {
                        //return obj[TmpErrorCnt-1].ExceptionMsg;
                        //return obj[TmpErrorIndex].ExceptionMsg;
                        return TmpErrorExceptionMsg;
                    }
                    else
                    {
                        Document doc = new Document(iTextSharp.text.PageSize.A4, 35, 35, 10, 25);
                    //string filename = "BlankMarksheetFor" + objPaper.PaperCode + dataString.SectionName;
                    string filename = "BlankMarksheetFor"  + "_" + objPaper.PaperCode + "_" + objPaper.PaperName + "_" + dataString.SectionName;
                    //string path = "D:/Sanchalak/MSUIS_AdminAPI/Upload/BlankMarksheet/" + filename + ".pdf";
                    //string path = "C:/inetpub/wwwroot/MSUIS_AdminAPI/Upload/BlankMarksheet/UA/" + filename + "_UA.pdf";
                    string path = "F:/inetpub/wwwroot/MSUIS_AdminAPI/Upload/BlankMarksheet/UA/" + filename + "_UA.pdf";
                    //string path = "";
                        //if (objPaper.SectionName == "NoSection")
                        //{
                        //    path = "F:/inetpub/wwwroot/MSUIS_AdminAPI/Upload/BlankMarksheet/UA/" + filename + "_UA.pdf";
                        //}
                        //else
                        //{
                        //    path = "F:/inetpub/wwwroot/MSUIS_AdminAPI/Upload/BlankMarksheet/UA/" + filename + "_UA_" + objPaper.SectionName + ".pdf";
                        //}
                        // if (File.Exists("C:/inetpub/wwwroot/MSUIS_AdminAPI/Upload/BlankMarksheet/UA/" + filename + "_UA.pdf"))
                        if (File.Exists("F:/inetpub/wwwroot/MSUIS_AdminAPI/Upload/BlankMarksheet/UA/" + filename + "_UA.pdf"))
                        {
                            Console.WriteLine(path);
                            File.Delete(path);
                        }
                        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));

                        doc.Open();
                        //using footer class

                        //writer.PageEvent = new Footer1();

                        PdfContentByte cd = writer.DirectContent;

                        iTextSharp.text.Font front = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, 8);
                        BaseColor bgColor = BaseColor.WHITE;
                        BaseColor blackBgColor = BaseColor.BLACK;

                        BaseFont baseFont = BaseFont.CreateFont("C:/Windows/Fonts/arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                        BaseFont baseBoldFont = BaseFont.CreateFont("C:/Windows/Fonts/arialbd.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                        Font cellFont = new Font(baseFont, 9, Font.NORMAL);
                        Font cellFontSmall = new Font(baseFont, 7, Font.NORMAL);
                        Font cellBoldFontSmall = new Font(baseBoldFont, 6, Font.NORMAL);
                        //Font whitecellFont = new Font(baseFont, 6, Font.NORMAL, BaseColor.WHITE);
                        Font cellAddressFont = new Font(baseFont, 9, Font.NORMAL);
                        Font cellBOLDFont = new Font(baseBoldFont, 9, Font.NORMAL);
                        Font cellBOLDFont11Size = new Font(baseBoldFont, 11, Font.NORMAL);
                        //Font whiteCellBOLDFont = new Font(baseBoldFont, 6, Font.NORMAL, BaseColor.WHITE);
                        var solid = new SolidLine(1);// normal
                        var dotted = new DottedLine(0.1f);// dotted
                        var dashed = new DashedLine(1);// dashed line

                        decimal Count = obj.Count;

                        SqlCommand cmd4 = new SqlCommand("GetProgrammeDetailsByProgIstancePartTermId", con);
                        SqlDataAdapter Da4 = new SqlDataAdapter();
                        cmd4.CommandType = CommandType.StoredProcedure;
                        cmd4.Parameters.AddWithValue("@ProgrammeInstancePartTermId", obj[0].ProgInstancePartTermId);
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

                        string details = programmeDetails + " for " + obj[0].ExamMaster;

                        //decimal temp = Count / 8;
                        loop = Math.Ceiling(Count / 25);
                        //int loop = Count/8 ;
                        int k = 0;
                        for (int i = 1; i <= loop; i++)
                        {
                            PdfPTable tableLayout = new PdfPTable(5);
                            tableLayout.WidthPercentage = 100;
                            tableLayout.SetWidths(new float[] { 0.8f, 1f, 1f, 1f, 1f });
                            // tableLayout.DefaultCell.Border = Rectangle.BOX;

                            //string imageURL = "C:/inetpub/wwwroot/MSUIS_AdminAPI/Upload/BlankMarksheet/MSU_Logo.png";
                            string imageURL = "F:/inetpub/wwwroot/MSUIS_AdminAPI/Upload/BlankMarksheet/MSU_Logo.png";
                            Font boldFont = new Font(baseBoldFont, 8);
                            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                            jpg.ScaleAbsolute(50f, 50f);
                            PdfPCell cell = getTableCell(jpg, 3, 1, cellFont, bgColor, 1, solid, null, solid, solid);
                            cell.Padding = 5;
                            tableLayout.AddCell(cell);

                            boldFont = new Font(baseFont, 11, Font.BOLD);
                            cell = getTableCell("The Maharaja Sayajirao University of Baroda, Vadodara", 1, 4, boldFont, bgColor, 0, solid, solid, null, null);
                            cell.PaddingTop = 10;
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.VerticalAlignment = Element.ALIGN_CENTER;
                            tableLayout.AddCell(cell);

                            boldFont = new Font(baseFont, 7, Font.BOLD);
                            cell = getTableCell("Fatehgunj, Vadodara-390 002, Gujarat (India)", 1, 4, boldFont, bgColor, 0, null, solid, null, null);
                            cell.VerticalAlignment = Element.ALIGN_CENTER;
                            tableLayout.AddCell(cell);

                            doc.Add(new Paragraph("\n"));
                            doc.Add(tableLayout);
                            //doc.Add(new Paragraph("\n"));

                            tableLayout = new PdfPTable(1);
                            tableLayout.WidthPercentage = 100;
                            tableLayout.SetWidths(new float[] { 1f });

                            boldFont = new Font(baseFont, 9, Font.BOLD);
                            cell = getTableCell("Blank Mark List For", 1, 1, boldFont, bgColor, 0, solid, solid, null, solid);
                            cell.PaddingBottom = 7;
                            // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableLayout.AddCell(cell);

                            boldFont = new Font(baseFont, 9, Font.BOLD);
                            /*SqlCommand cmd4 = new SqlCommand("GetProgrammeDetailsByProgIstancePartTermId", con);
                            SqlDataAdapter Da4 = new SqlDataAdapter();
                            cmd4.CommandType = CommandType.StoredProcedure;
                            cmd4.Parameters.AddWithValue("@ProgrammeInstancePartTermId", obj[0].ProgInstancePartTermId);
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

                            string details = programmeDetails + " for " + obj[0].ExamMaster;*/

                            cell = getTableCell(details, 1, 1, boldFont, bgColor, 0, null, solid, null, solid);
                            cell.PaddingBottom = 7;
                            //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableLayout.AddCell(cell);

                            boldFont = new Font(baseFont, 9, Font.BOLD);
                            cell = getTableCell("College : " + obj[0].FacultyName + ", Sayajigunj Vadodara", 1, 1, boldFont, bgColor, 0, null, solid, solid, solid);
                            cell.PaddingBottom = 7;
                            //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableLayout.AddCell(cell);

                            doc.Add(tableLayout);

                            tableLayout = new PdfPTable(3);
                            tableLayout.WidthPercentage = 100;
                            tableLayout.SetWidths(new float[] { 6.5f, 3f, 1.5f });

                            cell = getTableCell("Paper Name: " + objPaper.PaperName + "(" + objPaper.PaperCode + ")", 1, 1, cellFont, bgColor, 1, solid, solid, null, solid);
                            cell.PaddingBottom = 7;
                            // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableLayout.AddCell(cell);

                            cell = getTableCell(objPaper.AssessmentMethodName + " UA - " + objPaper.SectionName + "(Max Mark:" + objPaper.SectionMarks + " Min Mark:" + objPaper.MinMarks + ")", 1, 1, cellFont, bgColor, 1, solid, solid, null, solid);
                            cell.PaddingBottom = 7;
                            // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableLayout.AddCell(cell);

                            cell = getTableCell("Count of Students:" + Count, 1, 1, cellFont, bgColor, 1, solid, solid, null, solid);
                            cell.PaddingBottom = 7;
                            // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableLayout.AddCell(cell);

                            doc.Add(tableLayout);

                            tableLayout = new PdfPTable(16);
                            tableLayout.WidthPercentage = 100;
                            tableLayout.SetWidths(new float[] { 0.5f, 1.5f, 1.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 1.5f });


                            cell = getTableCell("Sr", 1, 1, cellBOLDFont, bgColor, 0, solid, solid, null, solid);
                            cell.PaddingBottom = 7;
                            // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableLayout.AddCell(cell);

                            cell = getTableCell("Seat Number", 1, 1, cellBOLDFont, bgColor, 0, solid, solid, null, solid);
                            cell.PaddingBottom = 7;
                            // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableLayout.AddCell(cell);

                            cell = getTableCell("PRN", 1, 1, cellBOLDFont, bgColor, 0, solid, solid, null, solid);
                            cell.PaddingBottom = 7;
                            // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableLayout.AddCell(cell);

                            cell = getTableCell("Question-wise OR Section-wise Marks / Grade", 1, 12, cellBOLDFont, bgColor, 0, solid, solid, null, solid);
                            cell.PaddingBottom = 7;
                            // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableLayout.AddCell(cell);


                            cell = getTableCell("Total Marks", 1, 1, cellBOLDFont, bgColor, 0, solid, solid, null, solid);
                            cell.PaddingBottom = 7;
                            // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableLayout.AddCell(cell);


                            cell = getTableCell("No", 1, 1, cellBOLDFont, bgColor, 0, null, solid, solid, solid);
                            cell.PaddingBottom = 7;
                            // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableLayout.AddCell(cell);

                            cell = getTableCell("", 1, 1, cellBOLDFont, bgColor, 0, null, solid, solid, solid);
                            cell.PaddingBottom = 7;
                            // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableLayout.AddCell(cell);

                            cell = getTableCell("", 1, 1, cellBOLDFont, bgColor, 0, null, solid, solid, solid);
                            cell.PaddingBottom = 7;
                            // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableLayout.AddCell(cell);


                            cell = getTableCell("Q1", 1, 1, cellBOLDFont, bgColor, 0, solid, solid, solid, solid);
                            cell.PaddingBottom = 7;
                            // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableLayout.AddCell(cell);

                            cell = getTableCell("Q2", 1, 1, cellBOLDFont, bgColor, 0, solid, solid, solid, solid);
                            cell.PaddingBottom = 7;
                            // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableLayout.AddCell(cell);

                            cell = getTableCell("Q3", 1, 1, cellBOLDFont, bgColor, 0, solid, solid, solid, solid);
                            cell.PaddingBottom = 7;
                            // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableLayout.AddCell(cell);

                            cell = getTableCell("Q4", 1, 1, cellBOLDFont, bgColor, 0, solid, solid, solid, solid);
                            cell.PaddingBottom = 7;
                            // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableLayout.AddCell(cell);


                            cell = getTableCell("Q5", 1, 1, cellBOLDFont, bgColor, 0, solid, solid, solid, solid);
                            cell.PaddingBottom = 7;
                            // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableLayout.AddCell(cell);

                            cell = getTableCell("Q6", 1, 1, cellBOLDFont, bgColor, 0, solid, solid, solid, solid);
                            cell.PaddingBottom = 7;
                            // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableLayout.AddCell(cell);

                            cell = getTableCell("Q7", 1, 1, cellBOLDFont, bgColor, 0, solid, solid, solid, solid);
                            cell.PaddingBottom = 7;
                            // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableLayout.AddCell(cell);

                            cell = getTableCell("Q8", 1, 1, cellBOLDFont, bgColor, 0, solid, solid, solid, solid);
                            cell.PaddingBottom = 7;
                            // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableLayout.AddCell(cell);

                            cell = getTableCell("Q9", 1, 1, cellBOLDFont, bgColor, 0, solid, solid, solid, solid);
                            cell.PaddingBottom = 7;
                            // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableLayout.AddCell(cell);

                            cell = getTableCell("Q10", 1, 1, cellBOLDFont, bgColor, 0, solid, solid, solid, solid);
                            cell.PaddingBottom = 7;
                            // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableLayout.AddCell(cell);

                            cell = getTableCell("Q11", 1, 1, cellBOLDFont, bgColor, 0, solid, solid, solid, solid);
                            cell.PaddingBottom = 7;
                            // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableLayout.AddCell(cell);

                            cell = getTableCell("Q12", 1, 1, cellBOLDFont, bgColor, 0, solid, solid, solid, solid);
                            cell.PaddingBottom = 7;
                            // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableLayout.AddCell(cell);

                            cell = getTableCell("", 1, 1, cellBOLDFont, bgColor, 0, null, solid, solid, solid);
                            cell.PaddingBottom = 7;
                            // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableLayout.AddCell(cell);

                            for (int j = 1; j <= 25; j++)
                            {
                                if (k >= Count)
                                {
                                    cell = getTableCell("", 1, 1, cellFont, bgColor, 0, null, solid, solid, solid);
                                    cell.PaddingBottom = 7;
                                    // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tableLayout.AddCell(cell);

                                    cell = getTableCell("", 1, 1, cellFont, bgColor, 0, null, solid, solid, solid);
                                    // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tableLayout.AddCell(cell);

                                    cell = getTableCell("", 1, 1, cellFont, bgColor, 0, null, solid, solid, solid);
                                    // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tableLayout.AddCell(cell);
                                }
                                else
                                {

                                    int p = k + 1;
                                    cell = getTableCell(p.ToString(), 1, 1, cellFont, bgColor, 0, null, solid, solid, solid);
                                    cell.PaddingBottom = 7;
                                    tableLayout.AddCell(cell);

                                    cell = getTableCell(obj[k].SeatNumber, 1, 1, cellFont, bgColor, 0, null, solid, solid, solid);
                                    tableLayout.AddCell(cell);

                                    cell = getTableCell(obj[k].PRN.ToString(), 1, 1, cellFont, bgColor, 0, null, solid, solid, solid);
                                    tableLayout.AddCell(cell);

                                }
                                cell = getTableCell("", 1, 1, cellFont, bgColor, 0, solid, solid, solid, solid);
                                // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                tableLayout.AddCell(cell);

                                cell = getTableCell("", 1, 1, cellFont, bgColor, 0, solid, solid, solid, solid);
                                // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                tableLayout.AddCell(cell);

                                cell = getTableCell("", 1, 1, cellFont, bgColor, 0, solid, solid, solid, solid);
                                // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                tableLayout.AddCell(cell);

                                cell = getTableCell("", 1, 1, cellFont, bgColor, 0, solid, solid, solid, solid);
                                // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                tableLayout.AddCell(cell);


                                cell = getTableCell("", 1, 1, cellFont, bgColor, 0, solid, solid, solid, solid);
                                // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                tableLayout.AddCell(cell);

                                cell = getTableCell("", 1, 1, cellFont, bgColor, 0, solid, solid, solid, solid);
                                // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                tableLayout.AddCell(cell);

                                cell = getTableCell("", 1, 1, cellFont, bgColor, 0, solid, solid, solid, solid);
                                // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                tableLayout.AddCell(cell);

                                cell = getTableCell("", 1, 1, cellFont, bgColor, 0, solid, solid, solid, solid);
                                // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                tableLayout.AddCell(cell);

                                cell = getTableCell("", 1, 1, cellFont, bgColor, 0, solid, solid, solid, solid);
                                // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                tableLayout.AddCell(cell);

                                cell = getTableCell("", 1, 1, cellFont, bgColor, 0, solid, solid, solid, solid);
                                // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                tableLayout.AddCell(cell);

                                cell = getTableCell("", 1, 1, cellFont, bgColor, 0, solid, solid, solid, solid);
                                // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                tableLayout.AddCell(cell);

                                cell = getTableCell("", 1, 1, cellFont, bgColor, 0, solid, solid, solid, solid);
                                // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                tableLayout.AddCell(cell);

                                cell = getTableCell("", 1, 1, cellFont, bgColor, 0, null, solid, solid, solid);
                                // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                tableLayout.AddCell(cell);

                                k++;
                            }

                            doc.Add(tableLayout);
                            doc.Add(new Paragraph("\n"));

                            tableLayout = new PdfPTable(2);
                            tableLayout.WidthPercentage = 100;
                            tableLayout.SetWidths(new float[] { 1f, 0.5f });

                            cell = getTableCell("Seal", 2, 1, cellFont, bgColor, 1, solid, null, solid, solid);
                            cell.PaddingBottom = 5;
                            cell.PaddingTop = 15;
                            //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableLayout.AddCell(cell);

                            boldFont = new Font(baseFont, 7, Font.BOLD);
                            cell = getTableCell("Signature of Examiner/Chairperson", 1, 1, boldFont, bgColor, 1, solid, solid, null, null);
                            cell.PaddingBottom = 5;
                            cell.PaddingTop = 15;
                            //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableLayout.AddCell(cell);

                            boldFont = new Font(baseFont, 7, Font.BOLD);
                            cell = getTableCell("Date", 1, 1, boldFont, bgColor, 1, null, solid, solid, null);
                            cell.PaddingBottom = 5;
                            cell.PaddingTop = 15;
                            //cell.HorizontalAlignment = Element.;
                            tableLayout.AddCell(cell);

                            doc.Add(tableLayout);

                            tableLayout = new PdfPTable(1);
                            tableLayout.WidthPercentage = 100;
                            tableLayout.SetWidths(new float[] { 1f });

                            boldFont = new Font(baseFont, 7, Font.BOLD);
                            cell = getTableCell("Instruction", 1, 1, boldFont, bgColor, 1, solid, solid, null, solid);
                            tableLayout.AddCell(cell);

                            boldFont = new Font(baseFont, 7, Font.BOLD);
                            cell = getTableCell("1. While entering the marks, please ensure a clear, legible hand-writing, without any scratches or over-writing.", 1, 1, boldFont, bgColor, 1, null, solid, null, solid);
                            tableLayout.AddCell(cell);

                            boldFont = new Font(baseFont, 7, Font.BOLD);
                            cell = getTableCell("2. In case of scratches, over-writing or corrections, please re-write the marks separately with your signature.", 1, 1, boldFont, bgColor, 1, null, solid, null, solid);
                            tableLayout.AddCell(cell);

                            boldFont = new Font(baseFont, 7, Font.BOLD);
                            cell = getTableCell("3. Use English number while entering the marks.", 1, 1, boldFont, bgColor, 1, null, solid, null, solid);
                            tableLayout.AddCell(cell);


                            boldFont = new Font(baseFont, 7, Font.BOLD);
                            cell = getTableCell("4. Usage of whitner is strictly prohibited.", 1, 1, boldFont, bgColor, 1, null, solid, solid, solid);
                            cell.PaddingBottom = 5;
                            tableLayout.AddCell(cell);

                            doc.Add(tableLayout);
                        }

                        doc.Close();
                        writer.Close();

                        //string newpath = "http://172.25.15.22/MSUIS_AdminAPI/Upload/BlankMarksheet/UA/" + filename + "_UA.pdf";
                        //if (objPaper.SectionName == "")
                        //{
                        //    filename = filename + "_UA.pdf";
                        //}
                        //else
                        //{
                        //    filename = filename + "_UA_" + objPaper.SectionName + ".pdf";
                        //}
                        string newpath = "https://admission.msubaroda.ac.in/MSUIS_AdminAPI/Upload/BlankMarksheet/UA/" + filename + "_UA_" + objPaper.SectionName + ".pdf";

                        PdfReader pdfReader = new PdfReader(path);
                        int numberOfPages = pdfReader.NumberOfPages;
                        pdfReader.Close();
                        return newpath;
                    }
                //}
                //else
                //{
                //    BALExamFormMaster func = new BALExamFormMaster();
                //    List<ExamFormMaster> obj = new List<ExamFormMaster>();
                //    obj = func.getBlankMarksheetDetailsForIA(dataString, "UA");
                //    for (int i = 0; i < obj.Count; i++)
                //    {
                //        if (obj[i].ExceptionMsg != null)
                //        {
                //            TmpErrorCnt = i + 1;
                //            TmpErrorExceptionMsg = TmpErrorExceptionMsg + " " + obj[i].ExceptionMsg;
                //        }

                //    }
                //    if (TmpErrorCnt > 0)
                //    {
                //        //return obj[TmpErrorCnt - 1].ExceptionMsg;
                //        return TmpErrorExceptionMsg;
                //    }
                //    else
                //    {
                //        Document doc = new Document(iTextSharp.text.PageSize.A4, 35, 35, 10, 30);
                //        string filename = "BlankMarksheetFor" + objPaper.PaperCode;
                //        //string path = "C:/inetpub/wwwroot/MSUIS_AdminAPI/Upload/BlankMarksheet/UA/" + filename + "_UA.pdf";
                //        string path = "F:/inetpub/wwwroot/MSUIS_AdminAPI/Upload/BlankMarksheet/UA/" + filename + "_UA.pdf";
                //        //  if (File.Exists("C:/inetpub/wwwroot/MSUIS_AdminAPI/Upload/BlankMarksheet/UA/" + filename + "_UA.pdf"))
                //        if (File.Exists("F:/inetpub/wwwroot/MSUIS_AdminAPI/Upload/BlankMarksheet/UA/" + filename + "_UA.pdf"))
                //        {
                //            Console.WriteLine(path);
                //            File.Delete(path);
                //        }

                //        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));

                //        doc.Open();
                //        //using footer class
                //        writer.PageEvent = new Footer1();

                //        PdfContentByte cd = writer.DirectContent;

                //        iTextSharp.text.Font front = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, 8);
                //        BaseColor bgColor = BaseColor.WHITE;
                //        BaseColor blackBgColor = BaseColor.BLACK;

                //        BaseFont baseFont = BaseFont.CreateFont("C:/Windows/Fonts/arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                //        BaseFont baseBoldFont = BaseFont.CreateFont("C:/Windows/Fonts/arialbd.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
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

                //        decimal Count = obj.Count;

                //        //decimal temp = Count / 8;
                //        loop = Math.Ceiling(Count / 25);
                //        //int loop = Count/8 ;
                //        int k = 0;
                //        for (int i = 1; i <= loop; i++)
                //        {
                //            PdfPTable tableLayout = new PdfPTable(5);
                //            tableLayout.WidthPercentage = 100;
                //            tableLayout.SetWidths(new float[] { 0.8f, 1f, 1f, 1f, 1f });
                //            // tableLayout.DefaultCell.Border = Rectangle.BOX;

                //            //string imageURL = "C:/inetpub/wwwroot/MSUIS_AdminAPI/Upload/BlankMarksheet/MSU_Logo.png";
                //            string imageURL = "F:/inetpub/wwwroot/MSUIS_AdminAPI/Upload/BlankMarksheet/MSU_Logo.png";
                //            Font boldFont = new Font(baseBoldFont, 8);
                //            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                //            jpg.ScaleAbsolute(50f, 50f);
                //            PdfPCell cell = getTableCell(jpg, 3, 1, cellFont, bgColor, 1, solid, null, solid, solid);
                //            cell.Padding = 5;
                //            tableLayout.AddCell(cell);

                //            boldFont = new Font(baseFont, 11, Font.BOLD);
                //            cell = getTableCell("The Maharaja Sayajirao University of Baroda, Vadodara", 1, 4, boldFont, bgColor, 0, solid, solid, null, null);
                //            cell.PaddingTop = 10;
                //            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //            cell.VerticalAlignment = Element.ALIGN_CENTER;
                //            tableLayout.AddCell(cell);

                //            boldFont = new Font(baseFont, 7, Font.BOLD);
                //            cell = getTableCell("Fatehgunj, Vadodara-390 002, Gujarat (India)", 1, 4, boldFont, bgColor, 0, null, solid, null, null);
                //            cell.VerticalAlignment = Element.ALIGN_CENTER;
                //            tableLayout.AddCell(cell);


                //            doc.Add(new Paragraph("\n"));
                //            doc.Add(tableLayout);
                //            //doc.Add(new Paragraph("\n"));

                //            tableLayout = new PdfPTable(1);
                //            tableLayout.WidthPercentage = 100;
                //            tableLayout.SetWidths(new float[] { 1f });

                //            boldFont = new Font(baseFont, 9, Font.BOLD);
                //            cell = getTableCell("Blank Mark List For", 1, 1, boldFont, bgColor, 0, solid, solid, null, solid);
                //            cell.PaddingBottom = 7;
                //            // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //            tableLayout.AddCell(cell);

                //            boldFont = new Font(baseFont, 9, Font.BOLD);
                //            SqlCommand cmd4 = new SqlCommand("GetProgrammeDetailsByProgIstancePartTermId", con);
                //            SqlDataAdapter Da4 = new SqlDataAdapter();
                //            cmd4.CommandType = CommandType.StoredProcedure;
                //            cmd4.Parameters.AddWithValue("@ProgrammeInstancePartTermId", obj[0].ProgInstancePartTermId);
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

                //            string details = programmeDetails + " for " + obj[0].ExamMaster;

                //            cell = getTableCell(details, 1, 1, boldFont, bgColor, 0, null, solid, null, solid);
                //            cell.PaddingBottom = 7;
                //            //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //            tableLayout.AddCell(cell);

                //            boldFont = new Font(baseFont, 9, Font.BOLD);
                //            cell = getTableCell("College : " + obj[0].FacultyName + ", Sayajigunj Vadodara", 1, 1, boldFont, bgColor, 0, null, solid, solid, solid);
                //            cell.PaddingBottom = 7;
                //            //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //            tableLayout.AddCell(cell);

                //            doc.Add(tableLayout);

                //            tableLayout = new PdfPTable(3);
                //            tableLayout.WidthPercentage = 100;
                //            tableLayout.SetWidths(new float[] { 6.5f, 3f, 1.5f });

                //            cell = getTableCell("Paper Name: " + objPaper.PaperName + "(" + objPaper.PaperCode + ")", 1, 1, cellFont, bgColor, 1, solid, solid, null, solid);
                //            cell.PaddingBottom = 7;
                //            // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //            tableLayout.AddCell(cell);

                //            cell = getTableCell(objPaper.AssessmentMethodName + " UA - " + objPaper.SectionName + "(Max Mark:" + objPaper.MaxMarks + " Min Mark:" + objPaper.MinMarks + ")", 1, 1, cellFont, bgColor, 1, solid, solid, null, solid);
                //            cell.PaddingBottom = 7;
                //            // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //            tableLayout.AddCell(cell);

                //            cell = getTableCell("Count of Students:" + Count, 1, 1, cellFont, bgColor, 1, solid, solid, null, solid);
                //            cell.PaddingBottom = 7;
                //            // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //            tableLayout.AddCell(cell);

                //            doc.Add(tableLayout);

                //            tableLayout = new PdfPTable(5);
                //            tableLayout.WidthPercentage = 100;
                //            tableLayout.SetWidths(new float[] { 0.5f, 1.5f, 1.5f, 6f, 1.5f });


                //            cell = getTableCell("Sr No", 1, 1, cellBOLDFont, bgColor, 0, solid, solid, solid, solid);
                //            cell.PaddingBottom = 7;
                //            // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //            tableLayout.AddCell(cell);

                //            cell = getTableCell("Seat Number", 1, 1, cellBOLDFont, bgColor, 0, solid, solid, solid, solid);
                //            cell.PaddingBottom = 7;
                //            // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //            tableLayout.AddCell(cell);

                //            cell = getTableCell("PRN", 1, 1, cellBOLDFont, bgColor, 0, solid, solid, solid, solid);
                //            cell.PaddingBottom = 7;
                //            // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //            tableLayout.AddCell(cell);

                //            cell = getTableCell("Student Name", 1, 1, cellBOLDFont, bgColor, 0, solid, solid, solid, solid);
                //            cell.PaddingBottom = 7;
                //            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //            tableLayout.AddCell(cell);


                //            cell = getTableCell("Total Marks", 1, 1, cellBOLDFont, bgColor, 0, solid, solid, solid, solid);
                //            cell.PaddingBottom = 7;
                //            // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //            tableLayout.AddCell(cell);

                //            for (int j = 1; j <= 25; j++)
                //            {
                //                if (k >= Count)
                //                {
                //                    cell = getTableCell("", 1, 1, cellFont, bgColor, 0, null, solid, solid, solid);
                //                    cell.PaddingBottom = 7;
                //                    // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //                    tableLayout.AddCell(cell);

                //                    cell = getTableCell("", 1, 1, cellFont, bgColor, 0, null, solid, solid, solid);
                //                    // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //                    tableLayout.AddCell(cell);

                //                    cell = getTableCell("", 1, 1, cellFont, bgColor, 0, null, solid, solid, solid);
                //                    // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //                    tableLayout.AddCell(cell);
                //                    cell = getTableCell("", 1, 1, cellFont, bgColor, 0, null, solid, solid, solid);
                //                    // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //                    tableLayout.AddCell(cell);
                //                }
                //                else
                //                {

                //                    int p = k + 1;
                //                    cell = getTableCell(p.ToString(), 1, 1, cellFont, bgColor, 0, null, solid, solid, solid);
                //                    cell.PaddingBottom = 7;
                //                    tableLayout.AddCell(cell);

                //                    cell = getTableCell(obj[k].SeatNumber, 1, 1, cellFont, bgColor, 0, null, solid, solid, solid);
                //                    tableLayout.AddCell(cell);

                //                    cell = getTableCell(obj[k].PRN.ToString(), 1, 1, cellFont, bgColor, 0, null, solid, solid, solid);
                //                    tableLayout.AddCell(cell);

                //                    cell = getTableCell(obj[k].NameAsPerMarksheet, 1, 1, cellFont, bgColor, 0, null, solid, solid, solid);
                //                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //                    tableLayout.AddCell(cell);

                //                }

                //                cell = getTableCell("", 1, 1, cellFont, bgColor, 0, null, solid, solid, solid);
                //                // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //                tableLayout.AddCell(cell);

                //                k++;
                //            }

                //            doc.Add(tableLayout);
                //            doc.Add(new Paragraph("\n"));

                //            tableLayout = new PdfPTable(2);
                //            tableLayout.WidthPercentage = 100;
                //            tableLayout.SetWidths(new float[] { 1f, 0.5f });

                //            cell = getTableCell("Seal", 2, 1, cellFont, bgColor, 1, solid, null, solid, solid);
                //            cell.PaddingBottom = 5;
                //            cell.PaddingTop = 15;
                //            //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //            tableLayout.AddCell(cell);

                //            boldFont = new Font(baseFont, 7, Font.BOLD);
                //            cell = getTableCell("Signature of Examiner/Chairperson", 1, 1, boldFont, bgColor, 1, solid, solid, null, null);
                //            cell.PaddingBottom = 5;
                //            cell.PaddingTop = 15;
                //            //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //            tableLayout.AddCell(cell);

                //            boldFont = new Font(baseFont, 7, Font.BOLD);
                //            cell = getTableCell("Date", 1, 1, boldFont, bgColor, 1, null, solid, solid, null);
                //            cell.PaddingBottom = 5;
                //            cell.PaddingTop = 15;
                //            //cell.HorizontalAlignment = Element.;
                //            tableLayout.AddCell(cell);


                //            doc.Add(tableLayout);

                //            tableLayout = new PdfPTable(1);
                //            tableLayout.WidthPercentage = 100;
                //            tableLayout.SetWidths(new float[] { 1f });

                //            boldFont = new Font(baseFont, 7, Font.BOLD);
                //            cell = getTableCell("Instruction", 1, 1, boldFont, bgColor, 1, solid, solid, null, solid);
                //            tableLayout.AddCell(cell);

                //            boldFont = new Font(baseFont, 7, Font.BOLD);
                //            cell = getTableCell("1. While entering the marks, please ensure a clear, legible hand-writing, without any scratches or over-writing.", 1, 1, boldFont, bgColor, 1, null, solid, null, solid);
                //            tableLayout.AddCell(cell);

                //            boldFont = new Font(baseFont, 7, Font.BOLD);
                //            cell = getTableCell("2. In case of scratches, over-writing or corrections, please re-write the marks separately with your signature.", 1, 1, boldFont, bgColor, 1, null, solid, null, solid);
                //            tableLayout.AddCell(cell);

                //            boldFont = new Font(baseFont, 7, Font.BOLD);
                //            cell = getTableCell("3. Use English number while entering the marks.", 1, 1, boldFont, bgColor, 1, null, solid, null, solid);
                //            tableLayout.AddCell(cell);


                //            boldFont = new Font(baseFont, 7, Font.BOLD);
                //            cell = getTableCell("4. Usage of whitner is strictly prohibited.", 1, 1, boldFont, bgColor, 1, null, solid, solid, solid);
                //            cell.PaddingBottom = 5;
                //            tableLayout.AddCell(cell);

                //            doc.Add(tableLayout);
                //        }

                //        doc.Close();
                //        writer.Close();

                //        PdfReader pdfReader = new PdfReader(path);
                //        int numberOfPages = pdfReader.NumberOfPages;
                //        //  string newpath = "http://172.25.15.22/MSUIS_AdminAPI/Upload/BlankMarksheet/UA/" + filename + "_UA.pdf";
                //        string newpath = "https://admission.msubaroda.ac.in/MSUIS_AdminAPI/Upload/BlankMarksheet/UA/" + filename + "_UA.pdf";
                //        pdfReader.Close();
                //        return newpath;
                //    }
                //}

            }
            catch (Exception e)
            {
                string ErrorMsg = "Error :";
                return ErrorMsg + " " + e.Message;
                // return null;
                //Console.WriteLine(e.Message);
            }
        }
        public string GenerateBlankMarksheetForIA(BlankMarkSheets dataString)
        {
            int TmpErrorCnt = 0;
            string TmpErrorExceptionMsg = "Error :";
            try
            {
                BALPaper func1 = new BALPaper();
                MstPaper objPaper = func1.MstPaperById(dataString.PaperId, "IA", dataString.TeachingLearningMethodId, dataString.AssessmentMethodId);

                BALExamFormMaster func = new BALExamFormMaster();
                List<ExamFormMaster> obj = new List<ExamFormMaster>();
                obj = func.getBlankMarksheetDetailsForIA(dataString, "IA");
                for (int i = 0; i < obj.Count; i++)
                {
                    if (obj[i].ExceptionMsg != null)
                    {
                        TmpErrorCnt = i + 1;
                        TmpErrorExceptionMsg = TmpErrorExceptionMsg + " " + obj[i].ExceptionMsg;
                    }

                }
                if (TmpErrorCnt > 0)
                {
                    //return obj[TmpErrorCnt - 1].ExceptionMsg;
                    return TmpErrorExceptionMsg;
                }
                else
                {
                    Document doc = new Document(iTextSharp.text.PageSize.A4, 35, 35, 10, 30);
                    string filename = "BlankMarksheetFor" + objPaper.PaperCode;
                    // string path = "C:/inetpub/wwwroot/MSUIS_AdminAPI/Upload/BlankMarksheet/IA/" + filename + "_IA.pdf";
                    string path = "F:/inetpub/wwwroot/MSUIS_AdminAPI/Upload/BlankMarksheet/IA/" + filename + "_IA.pdf";
                    // if (File.Exists("C:/inetpub/wwwroot/MSUIS_AdminAPI/Upload/BlankMarksheet/IA/" + filename + "_IA.pdf"))
                    if (File.Exists("F:/inetpub/wwwroot/MSUIS_AdminAPI/Upload/BlankMarksheet/IA/" + filename + "_IA.pdf"))
                    {
                        Console.WriteLine(path);
                        File.Delete(path);
                    }

                    PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));

                    doc.Open();
                    //using footer class
                    //writer.PageEvent = new Footer1();

                    PdfContentByte cd = writer.DirectContent;

                    iTextSharp.text.Font front = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, 8);
                    BaseColor bgColor = BaseColor.WHITE;
                    BaseColor blackBgColor = BaseColor.BLACK;

                    BaseFont baseFont = BaseFont.CreateFont("C:/Windows/Fonts/arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    BaseFont baseBoldFont = BaseFont.CreateFont("C:/Windows/Fonts/arialbd.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    Font cellFont = new Font(baseFont, 9, Font.NORMAL);
                    Font cellFontSmall = new Font(baseFont, 7, Font.NORMAL);
                    Font cellBoldFontSmall = new Font(baseBoldFont, 6, Font.NORMAL);
                    //Font whitecellFont = new Font(baseFont, 6, Font.NORMAL, BaseColor.WHITE);
                    Font cellAddressFont = new Font(baseFont, 9, Font.NORMAL);
                    Font cellBOLDFont = new Font(baseBoldFont, 9, Font.NORMAL);
                    Font cellBOLDFont11Size = new Font(baseBoldFont, 11, Font.NORMAL);
                    //Font whiteCellBOLDFont = new Font(baseBoldFont, 6, Font.NORMAL, BaseColor.WHITE);
                    var solid = new SolidLine(1);// normal
                    var dotted = new DottedLine(0.1f);// dotted
                    var dashed = new DashedLine(1);// dashed line

                    decimal Count = obj.Count;

                    //decimal temp = Count / 8;
                    loop = Math.Ceiling(Count / 25);
                    //int loop = Count/8 ;
                    int k = 0;
                    for (int i = 1; i <= loop; i++)
                    {
                        PdfPTable tableLayout = new PdfPTable(5);
                        tableLayout.WidthPercentage = 100;
                        tableLayout.SetWidths(new float[] { 0.8f, 1f, 1f, 1f, 1f });
                        // tableLayout.DefaultCell.Border = Rectangle.BOX;

                        // string imageURL = "C:/inetpub/wwwroot/MSUIS_AdminAPI/Upload/BlankMarksheet/MSU_Logo.png";
                        string imageURL = "F:/inetpub/wwwroot/MSUIS_AdminAPI/Upload/BlankMarksheet/MSU_Logo.png";
                        Font boldFont = new Font(baseBoldFont, 8);
                        iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                        jpg.ScaleAbsolute(50f, 50f);
                        PdfPCell cell = getTableCell(jpg, 3, 1, cellFont, bgColor, 1, solid, null, solid, solid);
                        cell.Padding = 5;
                        tableLayout.AddCell(cell);

                        boldFont = new Font(baseFont, 11, Font.BOLD);
                        cell = getTableCell("The Maharaja Sayajirao University of Baroda, Vadodara", 1, 4, boldFont, bgColor, 0, solid, solid, null, null);
                        cell.PaddingTop = 10;
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.VerticalAlignment = Element.ALIGN_CENTER;
                        tableLayout.AddCell(cell);

                        boldFont = new Font(baseFont, 7, Font.BOLD);
                        cell = getTableCell("Fatehgunj, Vadodara-390 002, Gujarat (India)", 1, 4, boldFont, bgColor, 0, null, solid, null, null);
                        cell.VerticalAlignment = Element.ALIGN_CENTER;
                        tableLayout.AddCell(cell);

                        //string url = "https://msub.digitaluniversity.ac/";
                        //cell = getTableCell(url, 1, 4, cellFontSmall, bgColor, 0, null, solid, solid, null);
                        //cell.VerticalAlignment = Element.ALIGN_CENTER;
                        //tableLayout.AddCell(cell);


                        doc.Add(new Paragraph("\n"));
                        doc.Add(tableLayout);
                        //doc.Add(new Paragraph("\n"));

                        tableLayout = new PdfPTable(1);
                        tableLayout.WidthPercentage = 100;
                        tableLayout.SetWidths(new float[] { 1f });

                        boldFont = new Font(baseFont, 9, Font.BOLD);
                        cell = getTableCell("Blank Mark List For", 1, 1, boldFont, bgColor, 0, solid, solid, null, solid);
                        cell.PaddingBottom = 7;
                        // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        tableLayout.AddCell(cell);

                        boldFont = new Font(baseFont, 9, Font.BOLD);
                        SqlCommand cmd4 = new SqlCommand("GetProgrammeDetailsByProgIstancePartTermId", con);
                        SqlDataAdapter Da4 = new SqlDataAdapter();
                        cmd4.CommandType = CommandType.StoredProcedure;
                        cmd4.Parameters.AddWithValue("@ProgrammeInstancePartTermId", obj[0].ProgInstancePartTermId);
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

                        string details = programmeDetails + " for " + obj[0].ExamMaster;

                        cell = getTableCell(details, 1, 1, boldFont, bgColor, 0, null, solid, null, solid);
                        cell.PaddingBottom = 7;
                        //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        tableLayout.AddCell(cell);

                        boldFont = new Font(baseFont, 9, Font.BOLD);
                        cell = getTableCell("College : " + obj[0].FacultyName + ", Sayajigunj Vadodara", 1, 1, boldFont, bgColor, 0, null, solid, solid, solid);
                        cell.PaddingBottom = 7;
                        //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        tableLayout.AddCell(cell);

                        doc.Add(tableLayout);

                        tableLayout = new PdfPTable(3);
                        tableLayout.WidthPercentage = 100;
                        tableLayout.SetWidths(new float[] { 6.5f, 3f, 1.5f });

                        cell = getTableCell("Paper Name: " + objPaper.PaperName + "(" + objPaper.PaperCode + ")", 1, 1, cellFont, bgColor, 1, solid, solid, null, solid);
                        cell.PaddingBottom = 7;
                        // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        tableLayout.AddCell(cell);

                        cell = getTableCell(objPaper.AssessmentMethodName + " IA (Max Mark:" + objPaper.MaxMarks + " Min Mark:" + objPaper.MinMarks + ")", 1, 1, cellFont, bgColor, 1, solid, solid, null, solid);
                        cell.PaddingBottom = 7;
                        // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        tableLayout.AddCell(cell);

                        cell = getTableCell("Count of Students:" + Count, 1, 1, cellFont, bgColor, 1, solid, solid, null, solid);
                        cell.PaddingBottom = 7;
                        // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        tableLayout.AddCell(cell);

                        doc.Add(tableLayout);

                        tableLayout = new PdfPTable(5);
                        tableLayout.WidthPercentage = 100;
                        tableLayout.SetWidths(new float[] { 0.5f, 1.5f, 1.5f, 6f, 1.5f });


                        cell = getTableCell("Sr No", 1, 1, cellBOLDFont, bgColor, 0, solid, solid, solid, solid);
                        cell.PaddingBottom = 7;
                        // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        tableLayout.AddCell(cell);

                        cell = getTableCell("Seat Number", 1, 1, cellBOLDFont, bgColor, 0, solid, solid, solid, solid);
                        cell.PaddingBottom = 7;
                        // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        tableLayout.AddCell(cell);

                        cell = getTableCell("PRN", 1, 1, cellBOLDFont, bgColor, 0, solid, solid, solid, solid);
                        cell.PaddingBottom = 7;
                        // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        tableLayout.AddCell(cell);

                        cell = getTableCell("Student Name", 1, 1, cellBOLDFont, bgColor, 0, solid, solid, solid, solid);
                        cell.PaddingBottom = 7;
                        //cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        tableLayout.AddCell(cell);


                        cell = getTableCell("Total Marks", 1, 1, cellBOLDFont, bgColor, 0, solid, solid, solid, solid);
                        cell.PaddingBottom = 7;
                        // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        tableLayout.AddCell(cell);

                        for (int j = 1; j <= 25; j++)
                        {
                            if (k >= Count)
                            {
                                cell = getTableCell("", 1, 1, cellFont, bgColor, 0, null, solid, solid, solid);
                                cell.PaddingBottom = 7;
                                // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                tableLayout.AddCell(cell);

                                cell = getTableCell("", 1, 1, cellFont, bgColor, 0, null, solid, solid, solid);
                                // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                tableLayout.AddCell(cell);

                                cell = getTableCell("", 1, 1, cellFont, bgColor, 0, null, solid, solid, solid);
                                // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                tableLayout.AddCell(cell);
                                cell = getTableCell("", 1, 1, cellFont, bgColor, 0, null, solid, solid, solid);
                                // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                tableLayout.AddCell(cell);
                            }
                            else
                            {

                                int p = k + 1;
                                cell = getTableCell(p.ToString(), 1, 1, cellFont, bgColor, 0, null, solid, solid, solid);
                                cell.PaddingBottom = 7;
                                tableLayout.AddCell(cell);

                                cell = getTableCell(obj[k].SeatNumber, 1, 1, cellFont, bgColor, 0, null, solid, solid, solid);
                                tableLayout.AddCell(cell);

                                cell = getTableCell(obj[k].PRN.ToString(), 1, 1, cellFont, bgColor, 0, null, solid, solid, solid);
                                tableLayout.AddCell(cell);

                                cell = getTableCell(obj[k].NameAsPerMarksheet, 1, 1, cellFont, bgColor, 0, null, solid, solid, solid);
                                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                tableLayout.AddCell(cell);

                            }

                            cell = getTableCell("", 1, 1, cellFont, bgColor, 0, null, solid, solid, solid);
                            // cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableLayout.AddCell(cell);

                            k++;
                        }

                        doc.Add(tableLayout);
                        doc.Add(new Paragraph("\n"));

                        tableLayout = new PdfPTable(2);
                        tableLayout.WidthPercentage = 100;
                        tableLayout.SetWidths(new float[] { 1f, 0.5f });

                        cell = getTableCell("Seal", 2, 1, cellFont, bgColor, 1, solid, null, solid, solid);
                        cell.PaddingBottom = 5;
                        cell.PaddingTop = 15;
                        //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        tableLayout.AddCell(cell);

                        boldFont = new Font(baseFont, 7, Font.BOLD);
                        cell = getTableCell("Signature of Examiner/Chairperson", 1, 1, boldFont, bgColor, 1, solid, solid, null, null);
                        cell.PaddingBottom = 5;
                        cell.PaddingTop = 15;
                        //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        tableLayout.AddCell(cell);

                        boldFont = new Font(baseFont, 7, Font.BOLD);
                        cell = getTableCell("Date", 1, 1, boldFont, bgColor, 1, null, solid, solid, null);
                        cell.PaddingBottom = 5;
                        cell.PaddingTop = 15;
                        //cell.HorizontalAlignment = Element.;
                        tableLayout.AddCell(cell);


                        doc.Add(tableLayout);

                        tableLayout = new PdfPTable(1);
                        tableLayout.WidthPercentage = 100;
                        tableLayout.SetWidths(new float[] { 1f });

                        boldFont = new Font(baseFont, 7, Font.BOLD);
                        cell = getTableCell("Instruction", 1, 1, boldFont, bgColor, 1, solid, solid, null, solid);
                        tableLayout.AddCell(cell);

                        boldFont = new Font(baseFont, 7, Font.BOLD);
                        cell = getTableCell("1. While entering the marks, please ensure a clear, legible hand-writing, without any scratches or over-writing.", 1, 1, boldFont, bgColor, 1, null, solid, null, solid);
                        tableLayout.AddCell(cell);

                        boldFont = new Font(baseFont, 7, Font.BOLD);
                        cell = getTableCell("2. In case of scratches, over-writing or corrections, please re-write the marks separately with your signature.", 1, 1, boldFont, bgColor, 1, null, solid, null, solid);
                        tableLayout.AddCell(cell);

                        boldFont = new Font(baseFont, 7, Font.BOLD);
                        cell = getTableCell("3. Use English number while entering the marks.", 1, 1, boldFont, bgColor, 1, null, solid, null, solid);
                        tableLayout.AddCell(cell);


                        boldFont = new Font(baseFont, 7, Font.BOLD);
                        cell = getTableCell("4. Usage of whitner is strictly prohibited.", 1, 1, boldFont, bgColor, 1, null, solid, solid, solid);
                        cell.PaddingBottom = 5;
                        tableLayout.AddCell(cell);

                        doc.Add(tableLayout);
                    }

                    doc.Close();
                    writer.Close();

                    PdfReader pdfReader = new PdfReader(path);
                    int numberOfPages = pdfReader.NumberOfPages;
                    //   string newpath = "http://172.25.15.22/MSUIS_AdminAPI/Upload/BlankMarksheet/IA/" + filename + "_IA.pdf";
                    string newpath = "https://admission.msubaroda.ac.in/MSUIS_AdminAPI/Upload/BlankMarksheet/IA/" + filename + "_IA.pdf";
                    pdfReader.Close();
                    return newpath;
                }
            }
            catch (Exception e)
            {
                string ErrorMsg = "Error :";
                return ErrorMsg + " " + e.Message;
                // Console.WriteLine(e.Message);
            }
        }

    }

    //public partial class Footer1 : PdfPageEventHelper
    //{
    //    public override void OnEndPage(PdfWriter writer, Document doc)
    //    {
    //        BaseFont baseFont = BaseFont.CreateFont("C:/Windows/Fonts/arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
    //        Font cellFontSmall = new Font(baseFont, 7, Font.NORMAL);


    //        Chunk glue = new Chunk(new VerticalPositionMark());
    //        string datetime = DateTime.Now.ToString("dddd, dd MMMM yyyy");
    //        //Paragraph footer = new Paragraph(" Report Generated By: Ms. Gayatri Lalitkumar Ingale as on " +datetime, cellFontSmall);
    //        Paragraph footer = new Paragraph(" Report Generated on " + datetime, cellFontSmall);
    //        footer.Add(new Chunk(glue));
    //        footer.Add("Page " + writer.CurrentPageNumber + " of " + BlankMarksheet.loop);
    //        doc.Add(footer);
    //        PdfPTable footerTbl = new PdfPTable(1);

    //        // footerTbl.TotalWidth = 10000;
    //        footerTbl.TotalWidth = 100;

    //        PdfPCell cell = new PdfPCell(footer);
    //        cell.HorizontalAlignment = Element.ALIGN_LEFT;
    //        cell.Border = Rectangle.NO_BORDER;

    //        cell.Padding = 3;
    //        footerTbl.AddCell(cell);


    //    }
    //}
}