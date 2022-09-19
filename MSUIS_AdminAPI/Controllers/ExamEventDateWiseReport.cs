using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MSUISApi.BAL;
using System.Dynamic;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class ExamEventDateWiseReportController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));


        #region Exam Event List Get For Drop Down
        [HttpPost]
        public HttpResponseMessage ExamEventGetForDropDown()
        {

            try
            {
                List<ExamEventMaster> ObjLstExamEvent = new List<ExamEventMaster>();
                BALExamEventMaster ObjBalExamEvent = new BALExamEventMaster();
                ObjLstExamEvent = ObjBalExamEvent.ExamEventGetForDropDown();

                if (ObjLstExamEvent != null)
                    return Return.returnHttp("200", ObjLstExamEvent, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion        

        #region Exam Event - Date Wise Time Table Schedule Report
        [HttpPost]
        public HttpResponseMessage ExamEventDateWiseReportTimeTableSchedule(ExamEventDateWiseReport objReport)
        {           

            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();

                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand Cmd = new SqlCommand("ExamEventDateWiseTimeTableScheduleReport", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@ExamMasterId", objReport.ExamMasterId);
                TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime date1 = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(objReport.ExamDate).ToUniversalTime(), INDIAN_ZONE);
                DateTime date2 = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(objReport.EndDate).ToUniversalTime(), INDIAN_ZONE);                
                objReport.ExamDate = Convert.ToString(date1.ToString("yyyy-MM-dd"));
                Cmd.Parameters.AddWithValue("@Date", date1.ToString("yyyy-MM-dd"));
                Cmd.Parameters.AddWithValue("@Date1", date2.ToString("yyyy-MM-dd"));
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);
                Int32 count = 0;
                List<ExamEventDateWiseReport> ObjLstReportData = new List<ExamEventDateWiseReport>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ExamEventDateWiseReport ObjReportData = new ExamEventDateWiseReport();

                        ObjReportData.PaperName = Convert.ToString(Dt.Rows[i]["PaperName"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PaperName"]);
                        ObjReportData.PaperCode = Convert.ToString(Dt.Rows[i]["PaperCode"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PaperCode"]);
                        ObjReportData.ExamDate = Convert.ToString(Dt.Rows[i]["ExamDate"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ExamDate"]);
                        ObjReportData.S1 = Convert.ToString(Dt.Rows[i]["S1"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["S1"]);
                        ObjReportData.S2 = Convert.ToString(Dt.Rows[i]["S2"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["S2"]);
                        ObjReportData.S3 = Convert.ToString(Dt.Rows[i]["S3"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["S3"]);
                        ObjReportData.E1 = Convert.ToString(Dt.Rows[i]["E1"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["E1"]);
                        ObjReportData.E2 = Convert.ToString(Dt.Rows[i]["E2"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["E2"]);
                        ObjReportData.E3 = Convert.ToString(Dt.Rows[i]["E3"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["E3"]);

                        count = count + 1;
                        ObjReportData.IndexId = count;
                        ObjLstReportData.Add(ObjReportData);
                    }
                    return Return.returnHttp("200", ObjLstReportData, null);
                }
                else
                {
                    return Return.returnHttp("200", "No Record Found", null);
                }
            }

            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region Exam Event - Date Wise Student Paper Map Data Report
        [HttpPost]
        public HttpResponseMessage ExamEventDateWiseReportStudentPaperMapData(ExamEventDateWiseReport objReport)
        {

            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();

                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand Cmd = new SqlCommand("ExamEventDateWiseStudentPaperMapDataReport", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@ExamMasterId", objReport.ExamMasterId);
                TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime date1 = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(objReport.ExamDate).ToUniversalTime(), INDIAN_ZONE);
                DateTime date2 = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(objReport.EndDate).ToUniversalTime(), INDIAN_ZONE);
                objReport.ExamDate = Convert.ToString(date1.ToString("yyyy-MM-dd"));
                Cmd.Parameters.AddWithValue("@Date", date1.ToString("yyyy-MM-dd"));
                Cmd.Parameters.AddWithValue("@Date1", date2.ToString("yyyy-MM-dd"));
                /*objReport.ExamDate = Convert.ToString(date1);
                Cmd.Parameters.AddWithValue("@Date", objReport.ExamDate);*/
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);
                Int32 count = 0;
                List<ExamEventDateWiseReport> ObjLstReportData = new List<ExamEventDateWiseReport>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ExamEventDateWiseReport ObjReportData = new ExamEventDateWiseReport();

                        ObjReportData.PaperName = Convert.ToString(Dt.Rows[i]["PaperName"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PaperName"]);
                        ObjReportData.PaperCode = Convert.ToString(Dt.Rows[i]["PaperCode"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PaperCode"]);
                        ObjReportData.SeatNumber = Convert.ToString(Dt.Rows[i]["SeatNumber"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["SeatNumber"]);
                        ObjReportData.PRN = Convert.ToString(Dt.Rows[i]["PRN"]).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["PRN"]);
                        ObjReportData.ProgInstancePartTermId = Convert.ToString(Dt.Rows[i]["ProgInstancePartTermId"]).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["ProgInstancePartTermId"]);

                        count = count + 1;
                        ObjReportData.IndexId = count;
                        ObjLstReportData.Add(ObjReportData);
                    }
                    return Return.returnHttp("200", ObjLstReportData, null);

                }
                else
                {
                    return Return.returnHttp("200", "No Record Found", null);
                }
            }

            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion
        
        #region Exam Event - Date Wise Conflicted Student list from OES Report
        [HttpPost]
        public HttpResponseMessage ExamEventDateWiseReportConflictedStudentlistData(ExamEventDateWiseReport objReport)
        {

            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();

                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand Cmd = new SqlCommand("ExamEventDateWiseConflictedStudentlistfromOES", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                
                TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime date1 = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(objReport.ExamDate).ToUniversalTime(), INDIAN_ZONE);
                DateTime date2 = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(objReport.EndDate).ToUniversalTime(), INDIAN_ZONE);
                objReport.ExamDate = Convert.ToString(date1.ToString("yyyy-MM-dd"));
                Cmd.Parameters.AddWithValue("@Date", date1.ToString("yyyy-MM-dd"));
                Cmd.Parameters.AddWithValue("@Date1", date2.ToString("yyyy-MM-dd"));

                Da.SelectCommand = Cmd;

                Da.Fill(Dt);
                Int32 count = 0;
                List<ExamEventDateWiseReport> ObjLstReportData = new List<ExamEventDateWiseReport>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ExamEventDateWiseReport ObjReportData = new ExamEventDateWiseReport();

                        ObjReportData.TimeSlotCode = Convert.ToString(Dt.Rows[i]["TimeSlotCode"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["TimeSlotCode"]);
                        ObjReportData.StudentCode = Convert.ToString(Dt.Rows[i]["StudentCode"]).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["StudentCode"]);
                        ObjReportData.PaperCode = Convert.ToString(Dt.Rows[i]["PaperCode"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PaperCode"]);
                        ObjReportData.StudentPRN = Convert.ToString(Dt.Rows[i]["StudentPRN"]).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["StudentPRN"]);
                        ObjReportData.ExamPaperDate = Convert.ToString(Dt.Rows[i]["ExamPaperDate"]).IsEmpty() ? "" : string.Format("{0: dd-MM-yyyy}", Convert.ToString(Dt.Rows[i]["ExamPaperDate"]));
                       
                        count = count + 1;
                        ObjReportData.IndexId = count;
                        ObjLstReportData.Add(ObjReportData);
                    }
                    return Return.returnHttp("200", ObjLstReportData, null);

                }
                else
                {
                    return Return.returnHttp("200", "No Record Found", null);
                }
            }

            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion


    }
}
