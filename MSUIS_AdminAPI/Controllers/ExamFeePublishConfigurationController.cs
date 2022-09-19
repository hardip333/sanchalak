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
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class ExamFeePublishConfigurationController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        #region Exam Event Get Code

        [HttpPost]
        public HttpResponseMessage ExamEventMasterListGet(ExamFeePublishConfiguration dataString)
        {

            try
            {

                SqlCommand Cmd = new SqlCommand("ExamEventMasterGetForDropDown", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = Cmd;
                sda.Fill(Dt);

                List<ExamFeePublishConfiguration> ObjLstExamFeePublish = new List<ExamFeePublishConfiguration>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ExamFeePublishConfiguration ObjExamFeePublish = new ExamFeePublishConfiguration();

                        ObjExamFeePublish.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        ObjExamFeePublish.DisplayName = Convert.ToString(Dt.Rows[i]["DisplayName"]);
                        ObjLstExamFeePublish.Add(ObjExamFeePublish);
                    }
                }
                return Return.returnHttp("200", ObjLstExamFeePublish, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region GetFacultyByExamEventId
        [HttpPost]
        public HttpResponseMessage GetFacultyByExamEventId(ExamFeePublishConfiguration EFPC)
        {
            try
            {

                SqlCommand cmd = new SqlCommand("GetFacultyByExamEventId", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamMasterId", EFPC.ExamEventId);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = cmd;

                Da.Fill(Dt);

                List<ExamFeePublishConfiguration> ObjLstEFPC = new List<ExamFeePublishConfiguration>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ExamFeePublishConfiguration ObjEFPC = new ExamFeePublishConfiguration();

                        ObjEFPC.FacultyId = (((Dt.Rows[i]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["Id"]);
                        ObjEFPC.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyName"]);

                        ObjLstEFPC.Add(ObjEFPC);
                    }
                }
                return Return.returnHttp("200", ObjLstEFPC, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region ExamFeePublishConfigurationGetByEventId
        [HttpPost]
        public HttpResponseMessage ExamFeePublishConfigurationGetByEventId(ExamFeePublishConfiguration ObjExamFeePub)
        {
            try
            {
                Int32 Count = 0;
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("ExamFeeConfigurationGetByEventId", Con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.SelectCommand.Parameters.AddWithValue("@ExamEventId", ObjExamFeePub.ExamEventId);
                da.SelectCommand.Parameters.AddWithValue("@FacultyId", ObjExamFeePub.FacultyId);
                DataTable dt = new DataTable();
                da.Fill(dt);
                List<ExamFeePublishConfiguration> ObjLstExamFeePublish = new List<ExamFeePublishConfiguration>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ExamFeePublishConfiguration ObjExamFeePublish = new ExamFeePublishConfiguration();

                        ObjExamFeePublish.CourseScheduleId = (Convert.ToString(dt.Rows[i]["CourseScheduleId"])).IsEmpty() ? 0 : Convert.ToInt64(dt.Rows[i]["CourseScheduleId"]);
                        ObjExamFeePublish.ScheduleCode = (Convert.ToString(dt.Rows[i]["ScheduleCode"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["ScheduleCode"]);
                        ObjExamFeePublish.FacultyName = (Convert.ToString(dt.Rows[i]["FacultyName"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["FacultyName"]);
                        ObjExamFeePublish.InstituteName = (Convert.ToString(dt.Rows[i]["InstituteName"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["InstituteName"]);
                        ObjExamFeePublish.PartTermName = (Convert.ToString(dt.Rows[i]["PartTermName"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["PartTermName"]);
                        ObjExamFeePublish.ExamFeeAmount = (Convert.ToString(dt.Rows[i]["ExamFeeAmount"])).IsEmpty() ? 0 : Convert.ToDecimal(dt.Rows[i]["ExamFeeAmount"]);
                        ObjExamFeePublish.ExamFeePublish = (Convert.ToString(dt.Rows[i]["ExamFeePublish"])).IsEmpty() ? false : Convert.ToBoolean(dt.Rows[i]["ExamFeePublish"]);
                        ObjExamFeePublish.ExamFeeStartDate = (Convert.ToString(dt.Rows[i]["ExamFeeStartDate"])).IsEmpty() ? "-" : string.Format("{0: dd-MM-yyyy}", (dt.Rows[i]["ExamFeeStartDate"]));
                        ObjExamFeePublish.ExamFeeEndDateForStudent = (Convert.ToString(dt.Rows[i]["ExamFeeEndDateForStudent"])).IsEmpty() ? "-" : string.Format("{0: dd-MM-yyyy}", (dt.Rows[i]["ExamFeeEndDateForStudent"]));
                        ObjExamFeePublish.ExamFeeEndDateForFaculty = (Convert.ToString(dt.Rows[i]["ExamFeeEndDateForFaculty"])).IsEmpty() ? "-" : string.Format("{0: dd-MM-yyyy}", (dt.Rows[i]["ExamFeeEndDateForFaculty"]));

                        Count = Count + 1;
                        ObjExamFeePublish.IndexId = Count;
                        ObjLstExamFeePublish.Add(ObjExamFeePublish);

                    }
                    return Return.returnHttp("200", ObjLstExamFeePublish, null);
                }
                else
                {
                    return Return.returnHttp("201", "No Record Found", null);
                }

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Publish ExamFeeConfiguration
        [HttpPost]
        public HttpResponseMessage UpdateExamFeePublishConfiguration(ExamFeePublishConfiguration ObjExamFeePublish)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();
            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            try
            {
                Int64 UserId = Convert.ToInt64(res.ToString());
                Int32 Id = Convert.ToInt32(ObjExamFeePublish.Id);
                SqlCommand cmd = new SqlCommand("ExamFeePublishActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.AddWithValue("@Flag", "ExamFeePublishIsActive");
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                Con.Close();
                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been Published successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region UnPublish ExamFeeConfiguration
        [HttpPost]
        public HttpResponseMessage UpdateExamFeeUnPublishConfiguration(ExamFeePublishConfiguration ObjExamFeePublish)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            try
            {

                Int64 UserId = Convert.ToInt64(res.ToString());
                Int32 Id = Convert.ToInt32(ObjExamFeePublish.Id);
                SqlCommand cmd = new SqlCommand("ExamFeePublishActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.AddWithValue("@Flag", "ExamFeePublishInActive");
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                Con.Close();
                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been UnPublished successfully.";
                    return Return.returnHttp("200", strMessage.ToString(), null);
                }
                else if(string.Equals(strMessage, "FALSE"))
                {
                    strMessage = "Exam Form Already Generated You Cannot UnPublish";
                    return Return.returnHttp("201", strMessage.ToString(), null);
                }
                else
                {
                    return Return.returnHttp("201", strMessage.ToString(), null);
                }
                
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion
    }
}
