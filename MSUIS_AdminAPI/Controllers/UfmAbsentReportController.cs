using MSUISApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using MSUIS_TokenManager.App_Start;
using MSUISApi.BAL;
namespace MSUISApi.Controllers
{
    public class UfmAbsentReportController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        [HttpPost]
        #region Get Student List For A Selected Paper
        public HttpResponseMessage getStudentListForPaper(int examMasterId, Int64 PaperId, int ProgrammePartTermId, int BranchId,int ExamVenueId)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string responseToken = ac.ValidateToken(token);
                //string res = ac.ValidateToken(token);

                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("GetStduentListForPaper", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@examMasterId", examMasterId);
                Cmd.Parameters.AddWithValue("@PaperId", PaperId);
                Cmd.Parameters.AddWithValue("@ExamVenueId", ExamVenueId);
                Cmd.Parameters.AddWithValue("@ProgPartTermId", ProgrammePartTermId);
                Cmd.Parameters.AddWithValue("@BranchId", BranchId);
                Cmd.Parameters.AddWithValue("@UserId", Convert.ToInt64(responseToken));
                Da.SelectCommand = Cmd;
                Da.Fill(Dt);

                List<StudentForPaper> list = new List<StudentForPaper>();

                list = Dt.DataTableToList<StudentForPaper>();

                return Return.returnHttp("200", list, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        [HttpPost]
        #region Set UFM/Absent Flags in Database
        public HttpResponseMessage sendToDb(ReportData r1)
        {
            try
            {

                //int x = r1.DropDownValue;
                //List<string> listPRN = r1.PRN.ToList();
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                //string sql = "DECLARE @studentprn studentprn" + "INSERT INTO @studentprn (PRN) VALUES (r1.PRN)";
                //SqlCommand cmd1 = new SqlCommand(sql, Con);
                //Con.Open();
                //cmd1.ExecuteNonQuery();
                //Con.Close();
                string[] t2 = r1.PRNList.Split(',');
                Int64[] t1 = new Int64[t2.Length];
                string s1 = "IncStudentExamPaperMap.PRN in (";
                for (int i = 0; i < t2.Length; i++)
                {
                    t1[i] = Convert.ToInt64(t2[i]);
                    if (i != t2.Length - 1) {
                        s1 = s1 + t1[i] + ",";
                    }
                    else
                    {
                        s1 = s1 + t1[i];
                    }
                }
                s1 = s1 + ")";

                SqlCommand Cmd = new SqlCommand("setUFMAbsentFlags", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@prnvalue", s1);
                Cmd.Parameters.AddWithValue("@PaperId", r1.StudentPaperId);
                Cmd.Parameters.AddWithValue("@DropDownValue", r1.DropDownValue);
                Cmd.Parameters.AddWithValue("@ExamMasterId", r1.ExamMasterId);
                Da.SelectCommand = Cmd;
                //Da.Fill(Dt);
                Con.Open();
                Cmd.ExecuteNonQuery();
                Con.Close();

                return Return.returnHttp("200", "DATA STORED SUCCESSFULLY", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        [HttpPost]
        #region Get UFM/Absent Report
        public HttpResponseMessage getUFMAbsentReport(int examMasterId, Int64 PaperId, int ProgPartTermId, int BranchId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("GetUFMAbsentReport", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@examMasterId", examMasterId);
                Cmd.Parameters.AddWithValue("@PaperId", PaperId);
                Cmd.Parameters.AddWithValue("@ProgPartTermId", ProgPartTermId);
                Cmd.Parameters.AddWithValue("@BranchId", BranchId);
                Da.SelectCommand = Cmd;
                Da.Fill(Dt);

                List<UFMAbsentStatusReport> list = new List<UFMAbsentStatusReport>();

                list = Dt.DataTableToList<UFMAbsentStatusReport>();

                return Return.returnHttp("200", list, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        //Steffi Code Starts
        #region ExamVenueGetByEFPIdForUfmAbsentReport
        [HttpPost]
        public HttpResponseMessage ExamVenueGetByEFPIdForUfmAbsentReport(GetVenueListForUFMStatusReport UFM)
        {
            try
            {

                Int32 ProgrammePartTermId = UFM.ProgrammePartTermId;
                Int32 ExamMasterId = UFM.ExamMasterId;
                Int32 FacultyExamMapId = UFM.FacultyExamMapId;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("GetExamVenueForUfmAbsentReport", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammePartTermId", ProgrammePartTermId);
                cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                cmd.Parameters.AddWithValue("@FacultyExamMapId", FacultyExamMapId);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<GetVenueListForUFMStatusReport> objListUFM = new List<GetVenueListForUFMStatusReport>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        GetVenueListForUFMStatusReport objUFM = new GetVenueListForUFMStatusReport();
                        objUFM.ExamVenueId = Convert.ToInt32(dr["ExamVenueId"]);
                        objUFM.ExamVenueName = (dr["ExamVenueName"].ToString());
                        objListUFM.Add(objUFM);
                    }

                }
                return Return.returnHttp("200", objListUFM, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        //Steffi Code Ends
    }
}
