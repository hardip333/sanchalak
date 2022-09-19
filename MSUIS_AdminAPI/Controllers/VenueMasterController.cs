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
    public class VenueMasterController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();

        #region VenueMasterGet
        [HttpPost]
        public HttpResponseMessage VenueMasterGet()
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
                SqlCommand cmd = new SqlCommand("VenueMasterGet", con);

                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<VenueMaster> ObjVMList = new List<VenueMaster>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        VenueMaster ObjVM = new VenueMaster();
                        ObjVM.Id = Convert.ToInt32(dr["Id"].ToString());
                        ObjVM.VenueName = dr["VenueName"].ToString();
                        ObjVM.VenueAddress = dr["VenueAddress"].ToString();
                        ObjVM.VenueCapacity = dr["VenueCapacity"].ToString();
                        ObjVM.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
                        ObjVM.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjVM.IsActiveSts = dr["IsActiveSts"].ToString();
                        ObjVMList.Add(ObjVM);
                    }
                }
                return Return.returnHttp("200", ObjVMList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion


        #region VenueMasterAdd
        [HttpPost]
        public HttpResponseMessage VenueMasterAdd(VenueMaster VNM)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            if (String.IsNullOrWhiteSpace(Convert.ToString(VNM.VenueName)))
            {
                return Return.returnHttp("201", "Please Enter Venue Name", null);
            }
            else
            if (String.IsNullOrWhiteSpace(Convert.ToString(VNM.VenueAddress)))
            {
                return Return.returnHttp("201", "Please Enter Venue Address", null);
            }
            else
            if (String.IsNullOrWhiteSpace(Convert.ToString(VNM.VenueCapacity)))
            {
                return Return.returnHttp("201", "Please Enter Venue Capacity", null);
            }
            else
            {
                try
                {
                    string VenueName = Convert.ToString(VNM.VenueName);
                    string VenueAddress = Convert.ToString(VNM.VenueAddress);
                    string VenueCapacity = Convert.ToString(VNM.VenueCapacity);
                    Int64 UserId = 1;//Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("VenueMasterAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@VenueName", VenueName);
                    cmd.Parameters.AddWithValue("@VenueAddress", VenueAddress);
                    cmd.Parameters.AddWithValue("@VenueCapacity", VenueCapacity);
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

        #region VenueMasterUpdate
        [HttpPost]
        public HttpResponseMessage VenueMasterUpdate(VenueMaster VNM)
        {
            //String token = Request.Headers.GetValues("token").FirstOrDefault();
            //TokenOperation ac = new TokenOperation();

            //string res = ac.ValidateToken(token);

            //if (res == "0")
            //{
            //    return Return.returnHttp("0", null, null);
            //}
            if (String.IsNullOrWhiteSpace(Convert.ToString(VNM.VenueName)))
            {
                return Return.returnHttp("201", "Please Enter Venue Name", null);
            }
            else
            if (String.IsNullOrWhiteSpace(Convert.ToString(VNM.VenueAddress)))
            {
                return Return.returnHttp("201", "Please Enter Venue Address", null);
            }
            else
            if (String.IsNullOrWhiteSpace(Convert.ToString(VNM.VenueCapacity)))
            {
                return Return.returnHttp("201", "Please Enter Venue Capacity", null);
            }
            else
            {
                try
                {
                    Int32 Id = VNM.Id;
                    string VenueName = Convert.ToString(VNM.VenueName);
                    string VenueAddress = Convert.ToString(VNM.VenueAddress);
                    string VenueCapacity = Convert.ToString(VNM.VenueCapacity);
                    Int64 UserId = 1;//Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("VenueMasterEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@VenueName", VenueName);
                    cmd.Parameters.AddWithValue("@VenueAddress", VenueAddress);
                    cmd.Parameters.AddWithValue("@VenueCapacity", VenueCapacity);
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
                    return Return.returnHttp("201", e.Message, null);
                }
            }
        }
        #endregion

        #region VenueMasterDelete
        [HttpPost]
        public HttpResponseMessage VenueMasterDelete(VenueMaster VNM)
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
                Int32 Id = VNM.Id;
                Int64 UserId = 1; //Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("VenueMasterDelete", con);
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
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region VenueMasterIsSuspended
        [HttpPost]
        public HttpResponseMessage VenueMasterIsSuspended(VenueMaster VNM)
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
                Int32 Id = Convert.ToInt32(VNM.Id);
                Int64 UserId = 1; //Convert.ToInt64(res.ToString());
                SqlCommand cmd = new SqlCommand("VenueMasterActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "VenueMasterIsActiveDisable");
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

        #region VenueMasterIsActive
        [HttpPost]
        public HttpResponseMessage VenueMasterIsActive(VenueMaster VNM)
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

                Int32 Id = Convert.ToInt32(VNM.Id);
                Int64 UserId = 1;//Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("VenueMasterActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "VenueMasterIsActiveEnable");
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