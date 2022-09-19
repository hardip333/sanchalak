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
    public class EligibilityStatusMasterController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();

        #region EligibilityStatusMasterGet
        [HttpPost]
        public HttpResponseMessage EligibilityStatusMasterGet()
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
                SqlCommand cmd = new SqlCommand("EligibilityStatusMasterGet", con);

                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<EligibilityStatusMaster> ObjListEligSM = new List<EligibilityStatusMaster>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        EligibilityStatusMaster ObjEligSM = new EligibilityStatusMaster();
                        ObjEligSM.Id = Convert.ToInt32(dr["Id"].ToString());
                        ObjEligSM.EligibilityStatus = (Convert.ToString(dr["EligibilityStatus"])).IsEmpty() ? "" : Convert.ToString(dr["EligibilityStatus"]);
                        ObjEligSM.IsActive = (Convert.ToString(dr["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(dr["IsActive"]);
                        ObjEligSM.IsDeleted = (Convert.ToString(dr["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(dr["IsDeleted"]);
                        ObjEligSM.IsActiveSts = (Convert.ToString(dr["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(dr["IsActiveSts"]);
                        ObjListEligSM.Add(ObjEligSM);
                    }
                }
                return Return.returnHttp("200", ObjListEligSM, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region EligibilityStatusMasterGetById
        [HttpPost]
        public HttpResponseMessage EligibilityStatusMasterGetById(EligibilityStatusMaster ESM)
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
                SqlCommand cmd = new SqlCommand("EligibilityStatusMasterGetById", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EligibilityStatusId", ESM.EligibilityStatus);
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<EligibilityStatusMaster> ObjListEligSM = new List<EligibilityStatusMaster>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        EligibilityStatusMaster ObjEligSM = new EligibilityStatusMaster();
                        ObjEligSM.Id = Convert.ToInt32(dr["Id"].ToString());
                        ObjEligSM.EligibilityStatus = (Convert.ToString(dr["EligibilityStatus"])).IsEmpty() ? "" : Convert.ToString(dr["EligibilityStatus"]);
                        ObjEligSM.IsActive = (Convert.ToString(dr["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(dr["IsActive"]);
                        ObjEligSM.IsDeleted = (Convert.ToString(dr["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(dr["IsDeleted"]);
                        ObjEligSM.IsActiveSts = (Convert.ToString(dr["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(dr["IsActiveSts"]);
                        ObjListEligSM.Add(ObjEligSM);
                    }
                }
                return Return.returnHttp("200", ObjListEligSM, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion


        #region EligibilityStatusMasterAdd
        [HttpPost]
        public HttpResponseMessage EligibilityStatusMasterAdd(EligibilityStatusMaster EligStatM)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            if (String.IsNullOrWhiteSpace(Convert.ToString(EligStatM.EligibilityStatus)))
            {
                return Return.returnHttp("201", "Please Enter Eligibility Status", null);
            }
            else
            {
                try
                {
                    string EligibilityStatus = Convert.ToString(EligStatM.EligibilityStatus);
                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("EligibilityStatusMasterAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EligibilityStatus", EligibilityStatus);
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
                        strMessage = "Your data has been Added Successfully.";
                    }
                    return Return.returnHttp("200", strMessage.ToString(), null);
                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message.ToString(), null);
                }
            }
        }
        #endregion

        #region EligibilityStatusMasterUpdate
        [HttpPost]
        public HttpResponseMessage EligibilityStatusMasterUpdate(EligibilityStatusMaster EligStatM)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            if (String.IsNullOrWhiteSpace(Convert.ToString(EligStatM.EligibilityStatus)))
            {
                return Return.returnHttp("201", "Please Enter evaluation name", null);
            }
            else
            {
                try
                {
                    Int64 Id = EligStatM.Id;
                    string EligibilityStatus = EligStatM.EligibilityStatus;
                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("EligibilityStatusMasterEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@EligibilityStatus", EligibilityStatus);
                    //cmd.Parameters.AddWithValue("@IsActive", IsActive);
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
                        strMessage = "Your data has been Modified Successfully.";
                    }

                    return Return.returnHttp("200", strMessage.ToString(), null);
                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message.ToString(), null);
                }
            }
        }
        #endregion

        #region EligibilityStatusMasterDelete
        [HttpPost]
        public HttpResponseMessage EligibilityStatusMasterDelete(EligibilityStatusMaster EligStatM)
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
                Int64 Id = EligStatM.Id;
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("EligibilityStatusMasterDelete", con);
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
                    strMessage = "Your data has been Deleted Successfully.";
                }

                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region EligibilityStatusMasterIsSuspended
        [HttpPost]
        public HttpResponseMessage EligibilityStatusMasterIsSuspended(EligibilityStatusMaster EligStatM)
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
                Int32 Id = Convert.ToInt32(EligStatM.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());
                SqlCommand cmd = new SqlCommand("EligibilityStatusMasterActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "EligibilityStatusMasterIsActiveDisable");
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
                    strMessage = "Your data has been Suspended Successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region EligibilityStatusMasterIsActive
        [HttpPost]
        public HttpResponseMessage EligibilityStatusMasterIsActive(EligibilityStatusMaster EligStatM)
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

                Int32 Id = Convert.ToInt32(EligStatM.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("EligibilityStatusMasterActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "EligibilityStatusMasterIsActiveEnable");
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
                    strMessage = "Your data has been Active Successfully.";
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