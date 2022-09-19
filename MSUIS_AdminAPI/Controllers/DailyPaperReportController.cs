using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class DailyPaperReportController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region BlockWiseReportGet
        [HttpPost]
        public HttpResponseMessage DailyPaperReportGet(DailyPaperReport ObjBlock)
        {
            DailyPaperReport modelobj = new DailyPaperReport();

            try
            {
                //modelobj.FacultyExamMapId = ObjConReport.FacultyExamMapId;
                modelobj.ExamEventId = ObjBlock.ExamEventId;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("DailyPaperReport", con);
                TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime date1 = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(ObjBlock.ExamDate).ToUniversalTime(), INDIAN_ZONE);
                DateTime date2 = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(ObjBlock.ExamDate2).ToUniversalTime(), INDIAN_ZONE);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamEventId", modelobj.ExamEventId);
                //ObjBlock.PaperDate = Convert.ToString(date1.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@Date", date1.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@Date1", date2.ToString("yyyy-MM-dd"));

                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<DailyPaperReport> ObjLstBW = new List<DailyPaperReport>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        DailyPaperReport ObjBW = new DailyPaperReport();

                        //ObjBW.FacultyId = Convert.ToInt32(dr["FacultyId"].ToString());
                        ObjBW.ExamEventId = Convert.ToInt32(dr["ExamEventId"]);
                        //ObjBW.FacultyName = (dr["FacultyName"].ToString());
                        ObjBW.ExamEventName = (dr["ExamEventName"].ToString());
                        ObjBW.CenterCode = (Convert.ToString(dr["CenterCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["CenterCode"]);
                        ObjBW.ExamCenter = (Convert.ToString(dr["ExamCenter"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["ExamCenter"]);
                        ObjBW.ExamCode = (Convert.ToString(dr["ExamCode"])).IsEmpty() ? "Allocation Pending" : Convert.ToString(dr["ExamCode"]);
                        ObjBW.ExamVenueName = (Convert.ToString(dr["ExamVenueName"])).IsEmpty() ? "Allocation Pending" : Convert.ToString(dr["ExamVenueName"]);
                        ObjBW.Course = (dr["Course"].ToString());
                        DateTime ExamDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(dr["ExamDate"]).ToUniversalTime(), INDIAN_ZONE);
                        ObjBW.ExamDate = string.Format("{0: dd-MM-yyyy}", ExamDate);
                        ObjBW.PaperStartTime = (dr["PaperStartTime"].ToString());
                        ObjBW.PaperCode = (dr["PaperCode"].ToString());
                        ObjBW.PaperName = (dr["PaperName"].ToString());
                        ObjBW.StudentCount = (dr["StudentCount"].ToString());
                        count = count + 1;
                        ObjBW.IndexId = count;
                        ObjLstBW.Add(ObjBW);
                    }

                }
                return Return.returnHttp("200", ObjLstBW, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion



        #region ExamEventGet
        [HttpPost]
        public HttpResponseMessage ExamEventGet()
        {
            try
            {

                SqlCommand Cmd = new SqlCommand("ExamEventGet", con);
                Cmd.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = Cmd;

                sda.Fill(dt);


                List<ExamEventMaster> ObjLstEEM = new List<ExamEventMaster>();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ExamEventMaster objEEM = new ExamEventMaster();

                        objEEM.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        objEEM.DisplayName = Convert.ToString(dt.Rows[i]["DisplayName"]);
                        objEEM.IsActive = Convert.ToBoolean(dt.Rows[i]["IsActive"]);
                        objEEM.IsDeleted = Convert.ToBoolean(dt.Rows[i]["IsDeleted"]);

                        ObjLstEEM.Add(objEEM);
                    }
                }
                return Return.returnHttp("200", ObjLstEEM, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion




    }
}