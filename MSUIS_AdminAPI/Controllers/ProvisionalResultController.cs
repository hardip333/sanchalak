using MSUISApi.Models;
using System;
using System.Globalization;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Web;
using MSUIS_TokenManager.App_Start;
using Newtonsoft.Json;


namespace MSUISApi.Controllers
{
    public class ProvisionalResultController : ApiController
    {
        [HttpPost]
        /*#region Get_Result_Parameters
        public HttpResponseMessage GetResultParameters(int ProgId)
        {
            string token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation tokenOperation = new TokenOperation();
            string responseToken = tokenOperation.ValidateToken(token);


            if (responseToken == "0")
            {
                return Return.returnHttp("0", null, null);
            }


            else
            {
                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("GetResultParameters", con);
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@enprn", responseToken);
                    //cmd.Parameters.AddWithValue("@CertiId", certiinsert.CertiId);
                    cmd.Parameters.AddWithValue("@ProgrammeId", ProgId);
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    List<ResultParameters> list = new List<ResultParameters>();
                    list = dt.DataTableToList<ResultParameters>();

                    CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
                    TextInfo txtinfo = culinfo.TextInfo;

                    //list[0].NameAsPerMarksheet = txtinfo.ToTitleCase(list[0].NameAsPerMarksheet);
                    //list[0].IssueReason = txtinfo.ToTitleCase(list[0].IssueReason);

                    //list[0].CertificateName = txtinfo.ToTitleCase(list[0].CertificateName);

                    //string dirphoto = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/Photo/");
                    //string picinfophoto = Convert.ToString(list[0].StudentPhoto);
                    //string pathphoto = Path.GetFullPath(dirphoto) + picinfophoto;
                    ////string image = Convert.ToBase64String(pathphoto);
                    //byte[] imageArrayphoto = File.ReadAllBytes(pathphoto);
                    //string base64PhotoRepresentation = Convert.ToBase64String(imageArrayphoto);
                    //////Path.Combine(dirphoto, picinfophoto);
                    //list[0].StudentPhoto = base64PhotoRepresentation;


                    ////string sql = "SELECT ci.PreviousAcademicCode from CertificateIssue as ci where ci.PRN = @enprn";
                    ////SqlDataAdapter ad = new SqlDataAdapter();
                    ////SqlCommand cmd1 = new SqlCommand(sql, con);
                    ////cmd1.Parameters.AddWithValue("@enprn", certiinsert.PRN);
                    ////ad.SelectCommand = cmd1;
                    ////if (con.State == ConnectionState.Closed)
                    ////    con.Open();
                    ////cmd1.Connection = con;
                    ////CertificateIssue c1 = new CertificateIssue();
                    ////c1.PreviousAcademicCode = (string)cmd1.ExecuteScalar();
                    ////con.Close();

                    return Return.returnHttp("200", list, null);

                }

                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message, null);
                }
            }

        }
        #endregion*/
        #region Get_File_Name
        public string SetFileName(Int64 StudPRN, String SeatNumber, Int64 ExamEvent, Int64 BranchId, Int64 ProgrammePartTermId)
        {

            StringBuilder SbDocFile = new StringBuilder("Result_Marksheet");
            SbDocFile.Append("_");
            SbDocFile.Append(StudPRN.ToString());
            SbDocFile.Append("_");
            SbDocFile.Append(SeatNumber.ToString());
            SbDocFile.Append("_");
            SbDocFile.Append(ExamEvent.ToString());
            SbDocFile.Append("_");
            SbDocFile.Append(BranchId.ToString());
            SbDocFile.Append("_");
            SbDocFile.Append(ProgrammePartTermId.ToString());
            //SbDocFile.Append("_");
            //SbDocFile.Append(DateTime.Today.Day.ToString());
            //SbDocFile.Append(DateTime.Today.Month.ToString());
            //SbDocFile.Append(DateTime.Today.Year.ToString());
            //SbDocFile.Append(DateTime.Now.Hour.ToString());
            //SbDocFile.Append(DateTime.Now.Minute.ToString());
            //SbDocFile.Append(DateTime.Now.Second.ToString());
            SbDocFile.Append(".json");
            return SbDocFile.ToString();
        }
        #endregion
       
        #region Convert Numbers into Words - Method
        public string ConvertNumbersToWords(decimal Number)
        {
            
            int inputNumberint = Convert.ToInt32(Number);
            int inputNo = inputNumberint;



            if (inputNo == 0)
                return "Zero";



            int[] numbers = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();



            if (inputNo < 0)
            {
                sb.Append("Minus ");
                inputNo = -inputNo;
            }



            string[] words0 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };
            string[] words1 = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };
            string[] words2 = { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
            string[] words3 = { "Thousand ", "Lakh ", "Crore " };



            numbers[0] = inputNo % 1000; // units
            numbers[1] = inputNo / 1000;
            numbers[2] = inputNo / 100000;
            numbers[1] = numbers[1] - 100 * numbers[2]; // thousands
            numbers[3] = inputNo / 10000000; // crores
            numbers[2] = numbers[2] - 100 * numbers[3]; // lakhs



            for (int i = 3; i > 0; i--)
            {
                if (numbers[i] != 0)
                {
                    first = i;
                    break;
                }
            }
            for (int i = first; i >= 0; i--)
            {
                if (numbers[i] == 0) continue;
                u = numbers[i] % 10; // ones
                t = numbers[i] / 10;
                h = numbers[i] / 100; // hundreds
                t = t - 10 * h; // tens
                if (h > 0) sb.Append(words0[h] + "Hundred ");
                if (u > 0 || t > 0)
                {
                    if (h > 0 || i == 0) sb.Append("and ");
                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }
            return sb.ToString().TrimEnd();
        }
        #endregion

        #region Get_Result
        public HttpResponseMessage GenerateProvisionalResult(StudentDashboard s1)
        {
            string token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation tokenOperation = new TokenOperation();
            string responseToken = tokenOperation.ValidateToken(token);


            if (responseToken == "0")
            {
                return Return.returnHttp("0", null, null);
            }


            else
            {
                try
                {

                    //string strFileName = SetFileName(2018033800122335, "123115", 2, 219, 237);
                    string strFileName = SetFileName(s1.PRN, s1.SeatNumber, s1.ExamEventId, s1.SpecialisationId, s1.ProgrammePartTermId);
                    //string strFileName = SetFileName(2018033800122335, 123115, 2, 219, 237);
                    StudentMarksheetModel ObjStudMarksheet = new StudentMarksheetModel();

                    //-------aws--------
                    S3BucketOperation s3BucketOperation = new S3BucketOperation();
                    string allData = s3BucketOperation.GetFileFromS3(strFileName);
                    //JsonSerializer serializer = new JsonSerializer();                    
                    if (allData == null)
                    {
                        return Return.returnHttp("404", "Not Declare", null);
                    }
                    ObjStudMarksheet = JsonConvert.DeserializeObject<StudentMarksheetModel>(allData);

                    //ObjStudMarksheet = (StudentMarksheetModel)serializer.Deserialize(allData, typeof(StudentMarksheetModel);
                    // ObjStudMarksheet = (StudentMarksheetModel)allData;


                    // ...   


                    //using (StreamReader file = File.OpenText(System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["JsonGet"]) + strFileName))
                    //{
                    //    JsonSerializer serializer = new JsonSerializer();
                    //    ObjStudMarksheet = (StudentMarksheetModel)serializer.Deserialize(file, typeof(StudentMarksheetModel));
                    //}

                    CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
                    TextInfo txtinfo = culinfo.TextInfo;
                    ObjStudMarksheet.ObjStudConsolidatedResult.StudentName = txtinfo.ToLower(ObjStudMarksheet.ObjStudConsolidatedResult.StudentName);
                    ObjStudMarksheet.ObjStudConsolidatedResult.StudentName = txtinfo.ToTitleCase(ObjStudMarksheet.ObjStudConsolidatedResult.StudentName);
                    ObjStudMarksheet.ObjStudConsolidatedResult.MotherName = txtinfo.ToLower(ObjStudMarksheet.ObjStudConsolidatedResult.MotherName);
                    ObjStudMarksheet.ObjStudConsolidatedResult.MotherName = txtinfo.ToTitleCase(ObjStudMarksheet.ObjStudConsolidatedResult.MotherName);

                    if (ObjStudMarksheet.ObjLstPaperResultModel[0].EvaluationId == 3)
                    {
                        ObjStudMarksheet.ObjStudConsolidatedResult.MarkstoWords = ConvertNumbersToWords(ObjStudMarksheet.ObjStudConsolidatedResult.MarksObtained);
                        //getting assessment method code from resmakrgrade entry
                        int i = 0;
                        while (i < ObjStudMarksheet.ObjLstPaperResultModel.Count)
                        {
                            int j = 0;
                            while (j < ObjStudMarksheet.ObjLstResMarkGradeEntry.Count)
                            {
                                if (ObjStudMarksheet.ObjLstResMarkGradeEntry[j].PaperId == ObjStudMarksheet.ObjLstPaperResultModel[i].PaperId)
                                {
                                    ObjStudMarksheet.ObjLstPaperResultModel[i].AssessmentMethodCode = ObjStudMarksheet.ObjLstResMarkGradeEntry[j].AssessmentMethodCode;
                                    i++;
                                    break;
                                }
                                else
                                {
                                    j++;
                                }
                            }
                        }
                        //while (j < ObjStudMarksheet.ObjLstResMarkGradeEntry.Count && i < ObjStudMarksheet.ObjLstPaperResultModel.Count)
                        //{
                        //    if (ObjStudMarksheet.ObjLstResMarkGradeEntry[j].PaperId == ObjStudMarksheet.ObjLstPaperResultModel[i].PaperId)
                        //    {
                        //        ObjStudMarksheet.ObjLstPaperResultModel[i].AssessmentMethodCode = ObjStudMarksheet.ObjLstResMarkGradeEntry[j].AssessmentMethodCode;
                        //        i++; j++;
                        //    }
                        //    else
                        //    {
                        //        j++;
                        //    }
                        //}
                        //----------------over--------------
                    }

                    //if (ObjStudMarksheet.ObjLstPaperResultModel[0].EvaluationId == 3)
                    //{
                    //    ObjStudMarksheet.ObjStudConsolidatedResult.MarkstoWords = ConvertNumbersToWords(ObjStudMarksheet.ObjStudConsolidatedResult.MarksObtained);
                    //    getting assessment method code from resmakrgrade entry
                    //    int i = 0, j = 0;
                    //    while (j < ObjStudMarksheet.ObjLstResMarkGradeEntry.Count && i < ObjStudMarksheet.ObjLstPaperResultModel.Count)
                    //    {
                    //        if (ObjStudMarksheet.ObjLstResMarkGradeEntry[j].PaperId == ObjStudMarksheet.ObjLstPaperResultModel[i].PaperId)
                    //        {
                    //            ObjStudMarksheet.ObjLstPaperResultModel[i].AssessmentMethodCode = ObjStudMarksheet.ObjLstResMarkGradeEntry[j].AssessmentMethodCode;
                    //            i++; j++;
                    //        }
                    //        else
                    //        {
                    //            j++;
                    //        }
                    //    }
                    //    ----------------over--------------
                    //}

                    // to get the count of members of objlstresmarkgradeentry
                    ObjStudMarksheet.ObjStudConsolidatedResult.ObjLstResMarkGradeEntryCount = ObjStudMarksheet.ObjLstResMarkGradeEntry.Count();
                    return Return.returnHttp("200", ObjStudMarksheet, null);

                }

                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message, null);
                }
            }

        }
        #endregion
    }

}
