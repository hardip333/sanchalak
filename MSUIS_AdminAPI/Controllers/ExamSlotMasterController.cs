using MSUIS_TokenManager.App_Start;
using MSUISApi.BAL;
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
using System.Web.Http;

namespace MSUISApi.Controllers
{
    public class ExamSlotMasterController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        #region SlotMasterGet
        [HttpPost]
        public HttpResponseMessage ExamSlotMasterGet()
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
                var timespan = TimeSpan.FromHours(24);
                SqlCommand cmd = new SqlCommand("ExamSlotMasterGet", con);

                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<ExamSlotMaster> ObjSLList = new List<ExamSlotMaster>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ExamSlotMaster ObjSL = new ExamSlotMaster();

                        ObjSL.Id = Convert.ToInt64(dr["Id"].ToString());

                        ObjSL.StartTime = TimeSpan.Parse(Convert.ToString(dr["StartTime"]));
                        ObjSL.EndTime = TimeSpan.Parse(Convert.ToString(dr["EndTime"]));
                        //ObjSL.StartTime = Convert.TimeSpan.Parse(dr["StartTime"]);
                        //ObjSL.EndTime = Convert.ToDateTime(dr["EndTime"]);
                        ObjSL.StartTimeView = Convert.ToString(dr["StartTime"]);
                        ObjSL.EndTimeView = Convert.ToString(dr["EndTime"]);
                        ObjSL.SlotName = Convert.ToString(dr["SlotName"]);
                        ObjSL.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
                        ObjSL.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());

                        ObjSLList.Add(ObjSL);
                    }
                }
                return Return.returnHttp("200", ObjSLList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region ExamSlotMasterAdd
        [HttpPost]
        public HttpResponseMessage ExamSlotMasterAdd(ExamSlotMaster SL)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            if (String.IsNullOrWhiteSpace(Convert.ToString(SL.SlotName)))
            {
                return Return.returnHttp("201", "Please Enter evaluation name", null);
            }
            else
            {
                try
                {

                    string SlotName = Convert.ToString(SL.SlotName);

                    TimeSpan StartTime = SL.DtStartTime.TimeOfDay;
                    TimeSpan EndTime = SL.DtEndTime.TimeOfDay;
                    TimeSpan Diff = new TimeSpan(5, 30, 00);
                    StartTime = StartTime.Add(Diff);
                    EndTime = EndTime.Add(Diff);
                    //DateTime StartTime = TimeZoneInfo.ConvertTimeFromUtc(SL.StartTime, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    //DateTime EndTime = TimeZoneInfo.ConvertTimeFromUtc(SL.EndTime, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("ExamSlotMasterAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;



                    cmd.Parameters.AddWithValue("@StartTime", StartTime);
                    cmd.Parameters.AddWithValue("@SlotName", SlotName);
                    cmd.Parameters.AddWithValue("@EndTime", EndTime);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    con.Close();
                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Your data has been saved successfully.";
                    }
                    return Return.returnHttp("200", strMessage.ToString(), null);
                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message, null);
                }
            }
        }
        #endregion

        #region ExamSlotMasterUpdate
        [HttpPost]
        public HttpResponseMessage ExamSlotMasterUpdate(ExamSlotMaster SlotM)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            if (String.IsNullOrWhiteSpace(Convert.ToString(SlotM.SlotName)))
            {
                return Return.returnHttp("201", "Please Enter evaluation name", null);
            }
            else
            {
                try
                {
                    Int64 Id = Convert.ToInt64(SlotM.Id);
                    string SlotName = Convert.ToString(SlotM.SlotName);
                    TimeSpan StartTime = SlotM.EndTime.Subtract(SlotM.StartTime);
                    TimeSpan EndTime = SlotM.StartTime.Subtract(SlotM.EndTime);
                    //DateTime StartTime = TimeZoneInfo.ConvertTimeFromUtc(SlotM.StartTime, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    //DateTime EndTime = TimeZoneInfo.ConvertTimeFromUtc(SlotM.EndTime, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    Int64 UserId = Convert.ToInt64(res.ToString());




                    SqlCommand cmd = new SqlCommand("ExamSlotMasterEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Id);

                    cmd.Parameters.AddWithValue("@SlotName", SlotName);
                    cmd.Parameters.AddWithValue("@StartTime", StartTime);
                    cmd.Parameters.AddWithValue("@EndTime", EndTime);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    con.Close();

                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Your data has been saved successfully.";
                    }

                    return Return.returnHttp("200", strMessage.ToString(), null);
                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message, null);
                }

            }
        }
        #endregion

        #region SlotMasterDelete
        [HttpPost]
        public HttpResponseMessage ExamSlotMasterDelete(ExamSlotMaster SMas)
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
                Int64 Id = SMas.Id;
                Int64 UserId = Convert.ToInt64(res.ToString()); ;


                SqlCommand cmd = new SqlCommand("ExamSlotMasterDelete", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                //cmd.Parameters.AddWithValue("@Flag", "MstSubSpecialisationDelete");
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been saved successfully.";
                }

                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region ExamSlotMasterIsSuspended
        [HttpPost]
        public HttpResponseMessage ExamSlotMasterIsSuspended(ExamSlotMaster SM)
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

                /*MstExaminationPattern mstExaminationPattern = new MstExaminationPattern();
                dynamic jsonData = jobj;
                mstExaminationPattern.Id = jsonData.Id;*/
                Int32 Id = Convert.ToInt32(SM.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());
                //Convert.ToInt64(res.ToString());
                SqlCommand cmd = new SqlCommand("ExamSlotMasterActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "ExamSlotMasterIsActiveDisable");
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);

                cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@MESSAGE"].Value);
                con.Close();
                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been saved successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region ExamSlotMasterIsActive
        [HttpPost]
        public HttpResponseMessage ExamSlotMasterIsActive(ExamSlotMaster SL)
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

                Int32 Id = Convert.ToInt32(SL.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());
                //Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("ExamSlotMasterActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "ExamSlotMasterIsActiveEnable");
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);

                cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@MESSAGE"].Value);
                con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been saved successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        [HttpPost]
        public HttpResponseMessage ExamSlotMasterListGetActive()
        {
            try
            {
                //String token = Request.Headers.GetValues("token").FirstOrDefault();
                //String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                //String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                //TokenOperation ac = new TokenOperation();
                //string res = ac.ValidateToken(token);
                //string resRoleToken = ac.ValidateRoleToken(userRoleToken);

                //if (res == "0")
                //{
                //    return Return.returnHttp("0", null, null);
                //}
                //else if (resRoleToken == "0")
                //{
                //    return Return.returnHttp("0", null, null);
                //}
                //Int64 UserId = Convert.ToInt64(res.ToString());

                List<ExamSlotMaster> ExamSlotMasterList = new List<ExamSlotMaster>();
                BALExamSlotMaster func = new BALExamSlotMaster();
                //flag=1,IsDeleted=0,IsActive=1
                ExamSlotMasterList = func.getExamSlotMasterList("admin", 0, 1);

                return Return.returnHttp("200", ExamSlotMasterList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
    }
}