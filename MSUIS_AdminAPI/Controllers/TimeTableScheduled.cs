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
using System.Net.Mail;
using System.Text;
using System.Web.Http;

namespace MSUISApi.Controllers
{
    public class TimeTableScheduledController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

        #region AcademicYearList
        [HttpPost]
        public HttpResponseMessage GetAcademicYearList(AcademicYearList ObjAcademicYear)
        {
            AcademicYearList modelobj = new AcademicYearList();

            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                SqlCommand cmd = new SqlCommand("IncAcademicYearGet", con);
                cmd.CommandType = CommandType.StoredProcedure;

                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<AcademicYearList> ObjAcademic = new List<AcademicYearList>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AcademicYearList ObjAC = new AcademicYearList();
                        ObjAC.Id = Convert.ToInt32(dr["Id"]);
                        ObjAC.AcademicYearCode = Convert.ToString(dr["AcademicYearCode"]);

                        ObjAcademic.Add(ObjAC);
                    }

                }
                return Return.returnHttp("200", ObjAcademic, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion 

        #region ExamEventList
        [HttpPost]
        public HttpResponseMessage GetExamEventList(ExamEventList ObjExamEvent)
        {
            ExamEventList modelobj = new ExamEventList();

            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                SqlCommand cmd = new SqlCommand("TimeTableScheduledExamEventList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<ExamEventList> ObjExam = new List<ExamEventList>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ExamEventList ObjEvent = new ExamEventList();
                        ObjEvent.Id = Convert.ToInt32(dr["Id"]);
                        //ObjEvent.AcademicYearId = Convert.ToInt32(dr["AcademicYearId"]);
                        ObjEvent.DisplayName = Convert.ToString(dr["DisplayName"]);

                        ObjExam.Add(ObjEvent);
                    }

                }
                return Return.returnHttp("200", ObjExam, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region GetTimeTableScheduledData
        [HttpPost]
        public HttpResponseMessage GetTimeTableScheduledData(TimeTableScheduled ObjTimeTableScheduled)
        {
            TimeTableScheduled modelobj = new TimeTableScheduled();

            try
            {
                modelobj.AcademicYearId = ObjTimeTableScheduled.AcademicYearId;
                modelobj.ExamEventId = ObjTimeTableScheduled.ExamEventId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("TimeTableScheduledDataGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AcademicYearId", modelobj.AcademicYearId);
                cmd.Parameters.AddWithValue("@ExamEventId", modelobj.ExamEventId);

                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<TimeTableScheduled> TimeTableScheduled = new List<TimeTableScheduled>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        TimeTableScheduled ObjTTS = new TimeTableScheduled();
                        ObjTTS.Id = Convert.ToInt32(dr["Id"]);
                        ObjTTS.PaperName = Convert.ToString(dr["PaperName"]);
                        ObjTTS.PaperCode = Convert.ToString(dr["PaperCode"]);
                        ObjTTS.ExamDate = Convert.ToString(dr["ExamDate"]);
                        ObjTTS.StartTime = Convert.ToString(dr["StartTime"]);
                        ObjTTS.EndTime = Convert.ToString(dr["EndTime"]);
                        ObjTTS.SlotName = Convert.ToString(dr["SlotName"]);
                        ObjTTS.Start_Time_HR = Convert.ToString(dr["Start_Time_HR"]);
                        ObjTTS.Start_Time_MIN = Convert.ToString(dr["Start_Time_MIN"]);
                        ObjTTS.Start_AM_PM = Convert.ToString(dr["Start_AM_PM"]);
                        ObjTTS.End_Time_HR = Convert.ToString(dr["End_Time_HR"]);
                        ObjTTS.End_Time_MIN = Convert.ToString(dr["End_Time_MIN"]);
                        ObjTTS.End_AM_PM = Convert.ToString(dr["End_AM_PM"]);

                        count = count + 1;
                        ObjTTS.IndexId = count;
                        TimeTableScheduled.Add(ObjTTS);
                    }

                }
                return Return.returnHttp("200", TimeTableScheduled, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

    }
}
