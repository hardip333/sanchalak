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
    public class ExamFeeConfigurationController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        #region Exam Event Get Code

        [HttpPost]
        public HttpResponseMessage ExamEventMasterListGet(ExamFeeConfiguration dataString)
        {

            try
            {

                SqlCommand Cmd = new SqlCommand("ExamEventMasterGetForDropDown", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = Cmd;
                sda.Fill(Dt);

                List<ExamFeeConfiguration> ObjLstExamFeeConfig = new List<ExamFeeConfiguration>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ExamFeeConfiguration objExamFeeConfig = new ExamFeeConfiguration();

                        objExamFeeConfig.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objExamFeeConfig.DisplayName = Convert.ToString(Dt.Rows[i]["DisplayName"]);
                        ObjLstExamFeeConfig.Add(objExamFeeConfig);
                    }
                }
                return Return.returnHttp("200", ObjLstExamFeeConfig, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region GetFacultyByExamEventId
        [HttpPost]
        public HttpResponseMessage GetFacultyByExamEventId(ExamFeeConfiguration EFC)
        {
            try
            {
                
                SqlCommand cmd = new SqlCommand("GetFacultyByExamEventId", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamMasterId", EFC.ExamEventId);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = cmd;

                Da.Fill(Dt);

                List<ExamFeeConfiguration> ObjLstEFC = new List<ExamFeeConfiguration>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ExamFeeConfiguration ObjEFC = new ExamFeeConfiguration();

                        ObjEFC.FacultyId = (((Dt.Rows[i]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["Id"]);
                        ObjEFC.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyName"]);

                        ObjLstEFC.Add(ObjEFC);
                    }
                }
                return Return.returnHttp("200", ObjLstEFC, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region ExamFeeConfigurationGetByEventId
        [HttpPost]
        public HttpResponseMessage ExamFeeConfigurationGetByEventId(ExamFeeConfiguration ObjExamFee)
        {
            try
            {
                Int32 Count = 0;
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("ExamFeeConfigurationGetByEventId", Con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                
                da.SelectCommand.Parameters.AddWithValue("@ExamEventId", ObjExamFee.ExamEventId);
                da.SelectCommand.Parameters.AddWithValue("@FacultyId", ObjExamFee.FacultyId);
                DataTable dt = new DataTable();
                da.Fill(dt);
                List<ExamFeeConfiguration> ObjLstExamFeeConfig = new List<ExamFeeConfiguration>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ExamFeeConfiguration ObjExamFeeConfig = new ExamFeeConfiguration();
                       
                        ObjExamFeeConfig.CourseScheduleId = (Convert.ToString(dt.Rows[i]["CourseScheduleId"])).IsEmpty() ? 0 : Convert.ToInt64(dt.Rows[i]["CourseScheduleId"]);
                        ObjExamFeeConfig.ScheduleCode = (Convert.ToString(dt.Rows[i]["ScheduleCode"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["ScheduleCode"]);
                        ObjExamFeeConfig.FacultyName = (Convert.ToString(dt.Rows[i]["FacultyName"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["FacultyName"]);
                        ObjExamFeeConfig.InstituteName = (Convert.ToString(dt.Rows[i]["InstituteName"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["InstituteName"]);
                        ObjExamFeeConfig.PartTermName = (Convert.ToString(dt.Rows[i]["PartTermName"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["PartTermName"]);
                        ObjExamFeeConfig.ExamFeeAmount = (Convert.ToString(dt.Rows[i]["ExamFeeAmount"])).IsEmpty() ? 0 : Convert.ToDecimal(dt.Rows[i]["ExamFeeAmount"]);
                        ObjExamFeeConfig.ExamFeePublish = (Convert.ToString(dt.Rows[i]["ExamFeePublish"])).IsEmpty() ? false : Convert.ToBoolean(dt.Rows[i]["ExamFeePublish"]);
                        ObjExamFeeConfig.ExamFeeStartDate = (Convert.ToString(dt.Rows[i]["ExamFeeStartDate"])).IsEmpty() ? "-" : string.Format("{0: dd-MM-yyyy}", (dt.Rows[i]["ExamFeeStartDate"]));
                        ObjExamFeeConfig.ExamFeeEndDateForStudent = (Convert.ToString(dt.Rows[i]["ExamFeeEndDateForStudent"])).IsEmpty() ? "-" : string.Format("{0: dd-MM-yyyy}", (dt.Rows[i]["ExamFeeEndDateForStudent"]));
                        ObjExamFeeConfig.ExamFeeEndDateForFaculty = (Convert.ToString(dt.Rows[i]["ExamFeeEndDateForFaculty"])).IsEmpty() ? "-" : string.Format("{0: dd-MM-yyyy}", (dt.Rows[i]["ExamFeeEndDateForFaculty"]));

                        Count = Count + 1;
                        ObjExamFeeConfig.IndexId = Count;
                        ObjLstExamFeeConfig.Add(ObjExamFeeConfig);

                    }
                    return Return.returnHttp("200", ObjLstExamFeeConfig, null);
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

        #region UpdateExamFeeConfiguration
        [HttpPost]
        public HttpResponseMessage UpdateExamFeeConfiguration(JObject jsonobject)

        {
            ExamFeeConfiguration EFC = new ExamFeeConfiguration();
            dynamic jsonData = jsonobject;
            try
            {
                EFC.ExamFeeConfig = Convert.ToString(jsonData.ExamFeeConfig);   
                string a = jsonData.ExamFeeAmount;
                EFC.ExamFeeAmount = decimal.Parse(a);

                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                EFC.UserId = Convert.ToInt64(responseToken);

                SqlCommand cmd = new SqlCommand("UpdateExamFeeConfiguration", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamFeeConfig", EFC.ExamFeeConfig);
                cmd.Parameters.AddWithValue("@ExamFeeAmount", EFC.ExamFeeAmount);
                cmd.Parameters.AddWithValue("@UserId", EFC.UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                Con.Close();
                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your Data has been Updated successfully.";
                }

                return Return.returnHttp("200", strMessage, null);


            }
            catch (Exception e)
            {

                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region UpdateExamFeeConfigurationGetByCourseScheduleId
        [HttpPost]
        public HttpResponseMessage UpdateExamFeeConfigurationByCourseScheduleId(ExamFeeConfiguration ObjExamFeeConfig)
        {
            try
            {
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                if (String.IsNullOrEmpty(Convert.ToString(ObjExamFeeConfig.ExamFeeAmount)))
                {
                    return Return.returnHttp("201", "Please Type Exam Fee Amount", null);
                }
                else
                {
                    Int32 Id = Convert.ToInt32(ObjExamFeeConfig.Id);
                    Decimal ExamFeeAmount = Convert.ToDecimal(ObjExamFeeConfig.ExamFeeAmount);
                  
                    Int64 UserId = Convert.ToInt64(responseToken);

                    SqlCommand cmd = new SqlCommand("UpdateExamFeeConfigurationByCourseScheduleId", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@ExamFeeAmount", ExamFeeAmount);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);

                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    Con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    Con.Close();

                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Your data has been modified successfully.";
                    }
                    return Return.returnHttp("200", strMessage.ToString(), null);
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
