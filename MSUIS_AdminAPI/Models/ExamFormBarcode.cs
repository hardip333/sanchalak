using Ionic.Zip;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MSUISApi.BAL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class ExamFormBarcode
    {
        public string generatePDF(int? ExamMasterId, string strPaperId, int? requestId, string strSpecialisationId)
        {
            int TmpErrorCnt = 0;
            string TmpErrorExceptionMsg = "Error :";
            //Staging Path
            //string BarcodePath = "C:/inetpub/wwwroot/MSUIS_AdminAPI/Upload/Barcode/PrimaryBarcode/";
            //string BarcodeAtPath = @"C:\inetpub\wwwroot\MSUIS_AdminAPI\Upload\Barcode\PrimaryBarcode\";

            //string BarcodePath = "F:/Sanchalak/MSUIS_AdminAPI/Upload/Barcode/PrimaryBarcode/";
            //string BarcodeAtPath = @"F:\Sanchalak\MSUIS_AdminAPI\Upload\Barcode\PrimaryBarcode\";

            //LIVE Path
            string BarcodePath = "F:/inetpub/wwwroot/MSUIS_AdminAPI/Upload/Barcode/PrimaryBarcode/";
            string BarcodeAtPath = @"F:\inetpub\wwwroot\MSUIS_AdminAPI\Upload\Barcode\PrimaryBarcode\";
            try
            {
                BALExamBlocks func = new BALExamBlocks();
                List<ExamFormBarcodeList> element = func.GetDetailsOfExamFormBarcode(ExamMasterId, strPaperId, strSpecialisationId);

                string FolderName = "";
                iTextSharp.text.Font font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, 8);
                BaseColor bgColor = BaseColor.WHITE;


                BaseFont baseFont = BaseFont.CreateFont("C:/Windows/Fonts/arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                BaseFont baseBoldFont = BaseFont.CreateFont("C:/Windows/Fonts/arialbd.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                Font cellFont = new Font(baseFont, 8);
                Font cellFontSmall = new Font(baseFont, 8);
                Font boldFont = new Font(baseBoldFont, 8);

                var solid = new SolidLine(1);// normal
                var dotted = new DottedLine(0.1f);// dotted
                var dashed = new DashedLine(1);// dashed line

                Barcode128 code128 = new Barcode128(); // barcode type

                int? previousVenueId = 0;
                int? previousPaperId = 0;
                int? previousBlockId = 0;
                PdfPTable tableLayout = null;
                Document doc = null;

                List<String> pdfFileNames = new List<string>();
                for (int q = 0; q < element.Count;)
                {

                    if (previousVenueId != element[q].ExamVenueId || previousPaperId != element[q].PaperId)
                    {
                        if (doc != null)
                        {
                            if (doc.IsOpen())
                            {

                                doc.Close();
                            }
                        }

                        doc = new Document();
                        doc = new Document(iTextSharp.text.PageSize.A4, 10, 10, 32, 0);

                        FolderName = element[q].ProgrammePart;

                        string filename = "MSUISSeatNoBarcode - " + element[q].PaperCode + " - " + element[q].ExamVenueId + "_" + element[q].ExamVenue + "_" + requestId + ".pdf";
                        if (!Directory.Exists(BarcodePath + FolderName))
                        {
                            Directory.CreateDirectory(BarcodePath + FolderName);
                        }
                        string path = BarcodePath + FolderName + "/" + filename;
                        pdfFileNames.Add(path);

                        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
                        doc.Open();
                        PdfContentByte cb = writer.DirectContent;

                        tableLayout = new PdfPTable(2);
                        tableLayout.WidthPercentage = 100;
                        tableLayout.SetWidths(new float[] { 1f, 1f });
                        tableLayout.DefaultCell.Border = Rectangle.NO_BORDER;
                        tableLayout.DefaultCell.Padding = 15;
                        tableLayout.DefaultCell.PaddingLeft = 20;//30

                        previousVenueId = element[q].ExamVenueId;
                        previousBlockId = element[q].ExamBlockId;
                        previousPaperId = element[q].PaperId;
                    }


                    PdfPTable subTableLayout = new PdfPTable(2);
                    subTableLayout.WidthPercentage = 100;
                    subTableLayout.SetWidths(new float[] { 1.4f, 1f });
                    subTableLayout.DefaultCell.Border = Rectangle.NO_BORDER;
                    //subTableLayout.DefaultCell.PaddingLeft = 20;
                    subTableLayout.DefaultCell.PaddingLeft = 10;
                    subTableLayout.DefaultCell.PaddingRight = 10;

                    PdfPCell cell = getTableCell("", 1, 2, cellFont, bgColor, 1, null, null, null, null);
                    cell.FixedHeight = 3;
                    subTableLayout.AddCell(cell);


                    cell = getTableCell("Exam Date: " + element[q].ExamDate, 1, 1, cellFont, bgColor, 1, null, null, null, null);
                    cell.PaddingBottom = 5;
                    subTableLayout.AddCell(cell);

                    Chunk ChunkNote = new Chunk("Seat No:   ", cellFont);
                    Chunk ChunkText = new Chunk(element[q].SeatNo, boldFont);
                    Phrase p1 = new Phrase(ChunkNote);
                    Phrase p2 = new Phrase(ChunkText);
                    Paragraph p = new Paragraph();
                    p.Add(p1);
                    p.Add(p2);

                    cell = getTableCell(p, 1, 1, cellFont, bgColor, 1, null, null, null, null);
                    cell.PaddingLeft = 18;
                    cell.PaddingBottom = 5;
                    subTableLayout.AddCell(cell);

                    if (!Directory.Exists(BarcodePath + FolderName + "/barcode/" + requestId))
                    {
                        Directory.CreateDirectory(BarcodePath + FolderName + "/barcode/" + requestId);
                    }
                    if (!File.Exists(BarcodePath + FolderName + "/barcode/" + requestId + "/" + element[q].UId + ".png"))
                    {
                        code128.Code = element[q].UId;
                        code128.X = 0.001f;
                        code128.N = 0.1f;
                        System.Drawing.Image barCode = code128.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White);
                        barCode.Save(BarcodePath + FolderName + "/barcode/" + requestId + "/" + element[q].UId + ".png"); //save file
                    }


                    string imageURL = BarcodePath + FolderName + "/barcode/" + requestId + "/" + element[q].UId + ".png";
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                    jpg.ScalePercent(45f);
                    //jpg.ScaleAbsoluteHeight(33f);
                    /////////////////////////////////////////////////////////////
                      jpg.ScaleAbsolute(128f, 33f);
                    //jpg.ScaleAbsolute(170f, 20f);

                    cell = getTableCell(jpg, 2, 1, cellFont, bgColor, 1, null, null, null, null);
                    subTableLayout.AddCell(cell);

                    cell = getTableCell(element[q].ProgrammePart, 1, 1, cellFont, bgColor, 1, null, null, null, null);
                    cell.PaddingLeft = 18;
                    subTableLayout.AddCell(cell);

                    var paperName = (element[q].PaperName.Length < 20) ? element[q].PaperName : element[q].PaperName.Substring(0, 20);
                    cell = getTableCell(paperName, 1, 1, cellFontSmall, bgColor, 1, null, null, null, null);
                    cell.PaddingLeft = 18;
                    subTableLayout.AddCell(cell);

                    cell = getTableCell("Paper Code:" + element[q].PaperCode, 1, 1, cellFont, bgColor, 1, null, null, null, null);
                    cell.PaddingTop = 5;
                    subTableLayout.AddCell(cell);

                    cell = getTableCell("Block No." + element[q].RoomNo, 1, 1, cellFont, bgColor, 1, null, null, null, null);
                    cell.PaddingTop = 5;
                    cell.PaddingLeft = 18;
                    subTableLayout.AddCell(cell);

                    cell = getTableCell("Event: " + element[q].ExamEventMaster, 2, 1, cellFont, bgColor, 1, null, null, null, null);
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.PaddingLeft = 10;
                    subTableLayout.AddCell(cell);

                    tableLayout.AddCell(subTableLayout);
                    //doc.Add(tableLayout);



                    //if (q >= element.Count-1 || previousBlockId != element[q+1].ExamBlockId || previousVenueId != element[q + 1].ExamVenueId || previousPaperId != element[q + 1].PaperId)
                    //{
                    //    for (int j = ((q+1) % 16); j > 0; j--)
                    //    {
                    //        tableLayout.AddCell("");
                    //    }
                    //    doc.Add(tableLayout);

                    //    tableLayout = new PdfPTable(2);
                    //    tableLayout.WidthPercentage = 100;
                    //    tableLayout.SetWidths(new float[] { 1f, 1f });
                    //    tableLayout.DefaultCell.Border = Rectangle.NO_BORDER;
                    //    tableLayout.DefaultCell.Padding = 15;
                    //    tableLayout.DefaultCell.PaddingLeft = 30;

                    //    if (q < element.Count-1 && previousBlockId != element[q + 1].ExamBlockId && previousVenueId == element[q + 1].ExamVenueId && previousPaperId == element[q + 1].PaperId)
                    //    {
                    //        doc.NewPage();

                    //        previousBlockId = element[q].ExamBlockId;

                    //    }
                    //    else if(q < element.Count - 1 && (previousPaperId != element[q + 1].PaperId || previousVenueId != element[q + 1].ExamVenueId))
                    //    {
                    //        doc.Close();
                    //    }

                    //    //break;
                    //}


                    //if (q >= (element.Count - 1))
                    //{
                    //    //doc.Add(new Paragraph("")); 

                    //    doc.Close();
                    //}

                    if (q >= element.Count - 1 || previousBlockId != element[q + 1].ExamBlockId || previousVenueId != element[q + 1].ExamVenueId || previousPaperId != element[q + 1].PaperId)
                    {
                        //Should Comment
                        for (int j = ((q + 1) % 16); j > 0; j--)
                        {
                            tableLayout.AddCell("");
                        }
                        //Should Comment
                        doc.Add(tableLayout);
                        
                        tableLayout = new PdfPTable(2);
                        tableLayout.WidthPercentage = 100;
                        tableLayout.SetWidths(new float[] { 1f, 1f });
                        tableLayout.DefaultCell.Border = Rectangle.NO_BORDER;
                        tableLayout.DefaultCell.Padding = 15;
                        tableLayout.DefaultCell.PaddingLeft = 20;//30

                        //                       if (q < element.Count - 1 && previousBlockId != element[q + 1].ExamBlockId && previousVenueId == element[q + 1].ExamVenueId && previousPaperId == element[q + 1].PaperId)
                        if (q < element.Count - 1 && previousBlockId != element[q + 1].ExamBlockId)
                        {
                            doc.NewPage();

                            previousBlockId = element[q + 1].ExamBlockId;

                        }
                        else if (q < element.Count - 1 && (previousPaperId != element[q + 1].PaperId || previousVenueId != element[q + 1].ExamVenueId))
                        {
                            doc.Close();
                        }

                        //break;
                    }


                    if (q >= (element.Count - 1))
                    {
                        //doc.Add(new Paragraph("")); 

                        doc.Close();
                    }



                    //if (q >= (element.Count-1) || previousVenueId != element[q].ExamVenueId || previousPaperId != element[q].PaperId)
                    //{
                    //    //doc.Add(new Paragraph("")); 

                    //    doc.Close();
                    //}
                    q++;
                }

                string[] Filenames = pdfFileNames.ToArray();
                // string path1 = @"C:\temp\" + FolderName;//Location for inside Test Folder  
                //string[] Filenames = Directory.GetFiles(path1);

                using (ZipFile zip = new ZipFile())
                {
                    zip.AddFiles(Filenames, "ExamFormBarcode");//Zip file inside filename  
                    zip.Save(BarcodeAtPath + FolderName + "_" + requestId + ".zip");//location and name for creating zip file  

                }

                if (Directory.Exists(BarcodePath + FolderName + " /barcode/" + requestId))
                {
                    // Delete all files from the Directory  
                    foreach (string filename in Directory.GetFiles(BarcodePath + FolderName + "/barcode/" + requestId))
                    {
                        File.Delete(filename);
                    }
                    // Check al
                    Directory.Delete(BarcodePath + FolderName + "/barcode/" + requestId);
                }

                return FolderName + "_" + requestId + ".zip";
            }
            catch (Exception e)
            {
                string ErrorMsg = "Error :";
                return ErrorMsg + " " + e.Message;
                // Console.WriteLine(e.Message);
            }
        }
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
    }
}