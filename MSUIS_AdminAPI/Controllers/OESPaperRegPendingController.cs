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

namespace MSUISApi.Controllers
{
    public class OESRegPendingController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();

        #region OESPaperRegPending
        // This method is for searching full details of applicant according to ProgrammeInstancePartTermId  whose Application Fee Not Paid
        [HttpPost]
        public HttpResponseMessage OESPaperRegPending(OESPaperRegPendingStatistics ObjOESPaper)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                List<OESPaperRegPendingStatistics> ObjPaperlist = new List<OESPaperRegPendingStatistics>();
                Int32 count = 0;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@ExamEventId", ObjOESPaper.ExamEventId);
                cmd.CommandText = "OESPaperRegistrationPendingStatistics";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow DR in dt.Rows)
                {
                    OESPaperRegPendingStatistics s = new OESPaperRegPendingStatistics();

                    s.ExamEventId = Convert.ToInt32( DR["ExamEventId"].ToString());
                    s.DisplayName = DR["DisplayName"].ToString();
                    s.PaperCode = DR["PaperCode"].ToString();
                    s.PaperName = DR["PaperName"].ToString();
                    s.SubjectName = DR["SubjectName"].ToString();
                    s.Examdate = Convert.ToDateTime(DR["Examdate"].ToString());
                    s.ExamdateView = DR["Examdate"].ToString();


                    count = count + 1;
                    s.IndexId = count;


                    ObjPaperlist.Add(s);
                }
                return Return.returnHttp("200", ObjPaperlist, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region OESStudentRegPending
        // This method is for searching full details of applicant according to ProgrammeInstancePartTermId  whose Application Fee Not Paid
        [HttpPost]
        public HttpResponseMessage OESStudentRegPending(OESStudentRegPendingStatistics ObjOESStudent)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                List<OESStudentRegPendingStatistics> ObjStudlist = new List<OESStudentRegPendingStatistics>();
                Int32 count = 0;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@ExamEventId", ObjOESStudent.ExamEventId);
                cmd.CommandText = "OESStudentRegistrationPendingStatistics";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow DR in dt.Rows)
                {
                    OESStudentRegPendingStatistics s = new OESStudentRegPendingStatistics();

                    s.ExamEventId = Convert.ToInt32(DR["ExamEventId"].ToString());
                    s.DisplayName = DR["DisplayName"].ToString();
                    s.UserName = Convert.ToInt64(DR["UserName"].ToString());
                    s.StudentName = DR["StudentName"].ToString();
                    s.EmailID = DR["EmailID"].ToString();
                    s.MobileNo = DR["MobileNo"].ToString();


                    count = count + 1;
                    s.IndexId = count;


                    ObjStudlist.Add(s);
                }
                return Return.returnHttp("200", ObjStudlist, null);
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
                DataTable dt = new DataTable();
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

        #region OESStudentPaperMapRegPending
        // This method is for searching full details of applicant according to ProgrammeInstancePartTermId  whose Application Fee Not Paid
        [HttpPost]
        public HttpResponseMessage OESStudentPaperMapRegPending(OESStudentPaperMapRegPending ObjOESStudPaperMap)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                List<OESStudentPaperMapRegPending> ObjStudlist = new List<OESStudentPaperMapRegPending>();
                Int32 count = 0;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@ExamEventId", ObjOESStudPaperMap.ExamEventId);
                cmd.CommandText = "OESStudentPaperMapPending";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow DR in dt.Rows)
                {
                    OESStudentPaperMapRegPending s = new OESStudentPaperMapRegPending();

                    s.ExamEventId = Convert.ToInt32(DR["ExamEventId"].ToString());
                    s.UserName = Convert.ToInt64(DR["UserName"].ToString());
                    s.PaperCode = DR["PaperCode"].ToString();
                    s.SeatNumber = DR["SeatNumber"].ToString();


                    count = count + 1;
                    s.IndexId = count;


                    ObjStudlist.Add(s);
                }
                return Return.returnHttp("200", ObjStudlist, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion
    }
}