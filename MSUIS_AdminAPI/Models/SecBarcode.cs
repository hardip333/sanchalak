using System;
using Syncfusion.Pdf.Barcode;
using System.Drawing;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using Syncfusion.Pdf.Graphics;

namespace MSUISApi.Models
{
    public class SecBarcode
    {
        public int? ExamMasterId { get; set; }
        public int? ProgrammeId { get; set; }
        public int? ProgrammePartTermId { get; set; }
        public int? TeachingLearningMethodId { get; set; }
        public int? AssessmentMethodId { get; set; }
        public int? BranchId { get; set; }
        public int? FacultyExamMapId { get; set; }
        public int? MstPaperId { get; set; }
        public List<MstPaper> PaperList { get; set; }
        public String PaperCode { get; set; }
        public String ptbName { get; set; }
        public String BarCodeNumber { get; set; }
        //public Int32 StudentCount { get; set; }

        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        public SecBarcode getPaperListForSecBarcode(SecBarcode dataString)
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("SecondaryBarcodeDetailsGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Flag", "GetPaperListForSecBarcode");
                Cmd.Parameters.AddWithValue("@ExamMasterId", dataString.ExamMasterId);
                Cmd.Parameters.AddWithValue("@ProgrammePartTermId", dataString.ProgrammePartTermId);
                Cmd.Parameters.AddWithValue("@TeachingLearningMethodId", dataString.TeachingLearningMethodId);
                Cmd.Parameters.AddWithValue("@AssessmentMethodId", dataString.AssessmentMethodId);
                Cmd.Parameters.AddWithValue("@BranchId", dataString.BranchId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                SecBarcode element = new SecBarcode();

                if (Dt.Rows.Count > 0)
                {
                    element.PaperList = new List<MstPaper>();
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstPaper objPaper = new MstPaper();
                        objPaper.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objPaper.PaperName = Convert.ToString(Dt.Rows[i]["PaperName"]);
                        objPaper.PaperCode = Convert.ToString(Dt.Rows[i]["PaperCode"]);


                        element.PaperList.Add(objPaper);
                    }
                }
                return element;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public string GenerateSecBarcode(SecBarcode dataString)
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("SecondaryBarcodeDetailsGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Flag", "SecBarcodeCount");
                Cmd.Parameters.AddWithValue("@ExamMasterId", dataString.ExamMasterId);
                Cmd.Parameters.AddWithValue("@ProgrammePartTermId", dataString.ProgrammePartTermId);
                Cmd.Parameters.AddWithValue("@TeachingLearningMethodId", dataString.TeachingLearningMethodId);
                Cmd.Parameters.AddWithValue("@AssessmentMethodId", dataString.AssessmentMethodId);
                Cmd.Parameters.AddWithValue("@BranchId", dataString.BranchId);
                Cmd.Parameters.AddWithValue("@MstPaperId", dataString.MstPaperId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                if (Dt.Rows.Count > 0)
                {
                    //dataString.StudentCount = Convert.ToInt32(Dt.Rows[0]["StudentCount"].ToString());
                    dataString.PaperCode = Dt.Rows[0]["PaperCode"].ToString();                    
                    dataString.ptbName = Dt.Rows[0]["ptbName"].ToString();

                    //Creating a new PDF document
                    Syncfusion.Pdf.PdfDocument document = new Syncfusion.Pdf.PdfDocument();
                    document.PageSettings.SetMargins(10f);
                    //Adding new page to the PDF document
                    Syncfusion.Pdf.PdfPage page = null;
                    page = document.Pages.Add();

                    //Create PdfGraphics for the page
                    PdfGraphics graphics = page.Graphics;

                    //Barcode size            
                    SizeF size = new SizeF(150, 48);
                    //Create a new instance for Code128 barcode
                    PdfCode128Barcode code128 = new PdfCode128Barcode();
                    //Setting size of the barcode
                    code128.Size = size;
                    int row = 15;
                    int col = 27;
                    int Nrow1 = 4;
                    int Ncol1 = 26;
                    int Ncol2 = 216;
                    int Ncol3 = 406;

                    //Draw the string on PDF page
                    string ptbName = dataString.ptbName;

                    for (int i = 0; i < Dt.Rows.Count; i++) 
                    {
                        //code128.Text = dataString.PaperCode + "-" + i;
                        code128.Text = Dt.Rows[i]["BarCodeNumber"].ToString();
                        code128.TextAlignment = PdfBarcodeTextAlignment.Center;
                        code128.Draw(page, new PointF(col, row));
                        graphics.DrawString(ptbName, new PdfStandardFont(PdfFontFamily.Helvetica, 8), PdfBrushes.Black, new PointF(Ncol1, Nrow1));
                        graphics.DrawString(ptbName, new PdfStandardFont(PdfFontFamily.Helvetica, 8), PdfBrushes.Black, new PointF(Ncol2, Nrow1));
                        graphics.DrawString(ptbName, new PdfStandardFont(PdfFontFamily.Helvetica, 8), PdfBrushes.Black, new PointF(Ncol3, Nrow1));

                        col = col + 190;

                        if ((i+1) % 3 == 0)
                        {
                            col = 27;
                            row = row + 84;
                            Nrow1 = Nrow1 + 84;                            
                        }
                        if ((i+1) % 30 == 0)
                        {
                            page = document.Pages.Add();
                            graphics = page.Graphics;
                            row = 15;
                            col = 27;
                            Nrow1 = 4;                            
                        }
                    }
                    /*code39.Text = "TSS2101E01-";
                    //Set center alignment to the barcode text
                    code39.TextAlignment = PdfBarcodeTextAlignment.Center;
                    //Draw barcode into the PDF page
                    code39.Draw(page, new PointF(27, 30));
                    //Set left alignment to the barcode text
                    code39.TextAlignment = PdfBarcodeTextAlignment.Center;
                    //Draw barcode into the PDF page
                    code39.Draw(page, new PointF(217, 30));
                    //Set right alignment to the barcode text
                    code39.TextAlignment = PdfBarcodeTextAlignment.Center;
                    //Draw barcode into the PDF page
                    code39.Draw(page, new PointF(407, 30));
                    //Save and close the document

                    code39.TextAlignment = PdfBarcodeTextAlignment.Center;
                    code39.Draw(page, new PointF(27, 130));
                    code39.TextAlignment = PdfBarcodeTextAlignment.Left;
                    code39.Draw(page, new PointF(217, 130));
                    code39.TextAlignment = PdfBarcodeTextAlignment.Right;
                    code39.Draw(page, new PointF(407, 130));*/

                    string filename = dataString.PaperCode + "_SecBarcode";
                    //document.Save("C:/inetpub/wwwroot/MSUIS_AdminAPI/Upload/Barcode/SecondaryBarcode/" + filename + ".pdf");  // Staging Path
                    document.Save("F:/inetpub/wwwroot/MSUIS_AdminAPI/Upload/Barcode/SecondaryBarcode/" + filename + ".pdf");    // Live Path
                    document.Close(true);

                    //string path = "http://172.25.15.22/MSUIS_AdminAPI/Upload/Barcode/SecondaryBarcode/" + filename + ".pdf";
                    string path = "https://admission.msubaroda.ac.in/MSUIS_AdminAPI/Upload/Barcode/SecondaryBarcode/" + filename + ".pdf";

                    return path;
                }
                else
                {
                    return "No Record Found";
                }                
            }
            catch (Exception e)
            {
                string ErrorMsg = "Error :";
                return ErrorMsg + " " + e.Message;                
            }
        }
    }
}