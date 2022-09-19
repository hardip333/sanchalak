using MSUIS_TokenManager.App_Start;
using MSUISApi.BAL;
using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class EventClosureController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

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

        #region Institute Get By Exam Event
        [HttpPost]
        public HttpResponseMessage MstInstituteGetByExamEvent(MstInstitute ObjInst)
        {
            try
            {

                List<MstInstitute> ObjLstInstitute = new List<MstInstitute>();
                BALInstituteList ObjBalInstituteList = new BALInstituteList();
                ObjLstInstitute = ObjBalInstituteList.InstituteGetByExamEvent(ObjInst.ExamMasterId);

                if (ObjLstInstitute != null)
                    return Return.returnHttp("200", ObjLstInstitute, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion 

        #region Get Data For Event Closure
        [HttpPost]
        public HttpResponseMessage GetDataForEventClosure(EventClosure objEvent)
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

                SqlCommand Cmd = new SqlCommand("GetDataForEventClosure", con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@FacultyExamId", objEvent.FacultyExamId);
                Cmd.Parameters.AddWithValue("@ExamMasterId", objEvent.ExamMasterId);
                da.SelectCommand = Cmd;

                da.Fill(dt);
                Int32 count = 0;
                List<EventClosure> ObjLstEvent = new List<EventClosure>();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        EventClosure objEveClosure = new EventClosure();

                        objEveClosure.PartTermName = Convert.ToString(dt.Rows[i]["PartTermName"]).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["PartTermName"]);
                        objEveClosure.CourseScheduleMapId = Convert.ToString(dt.Rows[i]["CourseScheduleMapId"]).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[i]["CourseScheduleMapId"]);
                        objEveClosure.ProgrammeName = Convert.ToString(dt.Rows[i]["ProgrammeName"]).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["ProgrammeName"]);
                        objEveClosure.BranchName = Convert.ToString(dt.Rows[i]["BranchName"]).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["BranchName"]);
                        objEveClosure.SeatNumberCount = Convert.ToString(dt.Rows[i]["TotalSeatNumber"]).IsEmpty() ? 0 : Convert.ToInt64(dt.Rows[i]["TotalSeatNumber"]);
                        objEveClosure.ResultDeclaredCount = Convert.ToString(dt.Rows[i]["TotalResultDeclare"]).IsEmpty() ? 0 : Convert.ToInt64(dt.Rows[i]["TotalResultDeclare"]);
                        objEveClosure.IsClosed = Convert.ToString(dt.Rows[i]["IsClosed"]).IsEmpty() ? false : Convert.ToBoolean(dt.Rows[i]["IsClosed"]); ;
                       
                        count = count + 1;
                        objEveClosure.IndexId = count;
                        ObjLstEvent.Add(objEveClosure);
                    }

                }
                if (ObjLstEvent != null)
                    return Return.returnHttp("200", ObjLstEvent, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }

            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region Close Event
        [HttpPost]
        public HttpResponseMessage CloseEvent(EventClosure objEvent)
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

                SqlCommand Cmd = new SqlCommand("CloseEvent", con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@CourseScheduleMapId", objEvent.CourseScheduleMapId);
                Int64 UserId = Convert.ToInt64(res.ToString());

                Cmd.Parameters.AddWithValue("@UserId", UserId);
                Cmd.Parameters.AddWithValue("@UserTime", datetime);

                Cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                Cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                con.Open();
                Cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(Cmd.Parameters["@MESSAGE"].Value);
                con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your event has been closed successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

    }
}
