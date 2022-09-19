using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using MSUISApi.BAL;
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
    public class MstPreRequisiteLableController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();

        #region MstPreRequisiteLableGet
        [HttpPost]
        public HttpResponseMessage MstPreRequisiteLableGet()
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
                SqlCommand cmd = new SqlCommand("MstPreRequisiteLableGet", con);

                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<MstPreRequisiteLable> ObjMPRTList = new List<MstPreRequisiteLable>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstPreRequisiteLable ObjMPRT = new MstPreRequisiteLable();
                        ObjMPRT.Id = Convert.ToInt32(dr["Id"].ToString());
                        ObjMPRT.PreRequisiteLableName = dr["PreRequisiteLableName"].ToString();
                        ObjMPRT.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
                        ObjMPRT.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjMPRT.IsActiveSts = dr["IsActiveSts"].ToString();
                        ObjMPRTList.Add(ObjMPRT);
                    }
                }
                return Return.returnHttp("200", ObjMPRTList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion


        #region MstPreRequisiteLableAdd
        [HttpPost]
        public HttpResponseMessage MstPreRequisiteLableAdd(MstPreRequisiteLable MstPreRT)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            if (String.IsNullOrWhiteSpace(Convert.ToString(MstPreRT.PreRequisiteLableName)))
            {
                return Return.returnHttp("201", "Please Enter PreRequisite Type Name", null);
            }
            else
            {
                try
                {
                    string PreRequisiteLableName = Convert.ToString(MstPreRT.PreRequisiteLableName);
                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("MstPreRequisiteLableAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PreRequisiteLableName", PreRequisiteLableName);
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
                        strMessage = "Your data has been Added successfully.";
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

        #region MstPreRequisiteLableUpdate
        [HttpPost]
        public HttpResponseMessage MstPreRequisiteLableUpdate(MstPreRequisiteLable MPreReqT)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            if (String.IsNullOrWhiteSpace(Convert.ToString(MPreReqT.PreRequisiteLableName)))
            {
                return Return.returnHttp("201", "Please Enter Venue Name", null);
            }
            else
            {
                try
                {
                    Int32 Id = MPreReqT.Id;
                    string PreRequisiteLableName = Convert.ToString(MPreReqT.PreRequisiteLableName);
                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("MstPreRequisiteLableEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@PreRequisiteLableName", PreRequisiteLableName);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    //cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);

                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    con.Close();

                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Your data has been Updated.";
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

        #region MstPreRequisiteLableDelete
        [HttpPost]
        public HttpResponseMessage MstPreRequisiteLableDelete(MstPreRequisiteLable MPRT)
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
                Int32 Id = MPRT.Id;
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("MstPreRequisiteLableDelete", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                //cmd.Parameters.AddWithValue("@Flag", "MstEvaluationDelete");
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been Deleted.";
                }

                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region MstPreRequisiteLableIsSuspended
        [HttpPost]
        public HttpResponseMessage MstPreRequisiteLableIsSuspended(MstPreRequisiteLable MPRT)
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
                Int32 Id = Convert.ToInt32(MPRT.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());
                SqlCommand cmd = new SqlCommand("MstPreRequisiteLableActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstPreRequisiteLableIsActiveDisable");
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
                    strMessage = "Your data has been Suspended.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region MstPreRequisiteLableIsActive
        [HttpPost]
        public HttpResponseMessage MstPreRequisiteLableIsActive(MstPreRequisiteLable MPRT)
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

                Int32 Id = Convert.ToInt32(MPRT.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("MstPreRequisiteLableActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstPreRequisiteLableIsActiveEnable");
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
                    strMessage = "Your data has been Active.";
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