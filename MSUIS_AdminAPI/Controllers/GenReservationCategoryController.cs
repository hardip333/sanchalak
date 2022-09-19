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
    public class GenReservationCategoryController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        Validation validation = new Validation();


        #region Get GenReservationCategory Detail
        [HttpPost]
        public HttpResponseMessage getGenReservationCategoryList()
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

                SqlCommand cmd = new SqlCommand("GenReservationCategoryGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                List<GenReservationCategory> GRList = new List<GenReservationCategory>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        GenReservationCategory objGenResCat = new GenReservationCategory();
                        objGenResCat.Id = Convert.ToInt32(dr["Id"].ToString());
                        objGenResCat.ApplicationReservationCode = (Convert.ToString(dr["ApplicationReservationCode"])).IsEmpty() ? "-" : Convert.ToString(dr["ApplicationReservationCode"]);
                        objGenResCat.ApplicationReservationName = (Convert.ToString(dr["ApplicationReservationName"])).IsEmpty() ? "-" : Convert.ToString(dr["ApplicationReservationName"]);
                        objGenResCat.Sequence = Convert.ToInt16(dr["Sequence"].ToString());
                        GRList.Add(objGenResCat);
                    }

                }
                return Return.returnHttp("200", GRList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion
       
        #region Insert GenReservationCategory Detail
        [HttpPost]
        public HttpResponseMessage GenReservationCategoryAdd(GenReservationCategory ObjGRC)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            if (String.IsNullOrWhiteSpace(Convert.ToString(ObjGRC.ApplicationReservationCode)))
            {
                return Return.returnHttp("201", "Please enter Application Reservation Code", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjGRC.ApplicationReservationName)))
            {
                return Return.returnHttp("201", "Please enter Application Reservation Name", null);
            }
                      
            else
            {
                try
                {

                    Int64 UserId = Convert.ToInt64(res.ToString());
                    GenReservationCategory objGenRC = new GenReservationCategory();
                    objGenRC.ApplicationReservationCode = ObjGRC.ApplicationReservationCode;
                    objGenRC.ApplicationReservationName = ObjGRC.ApplicationReservationName;                  
                    SqlCommand cmd = new SqlCommand("GenReservationCategoryAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ApplicationReservationCode", objGenRC.ApplicationReservationCode);
                    cmd.Parameters.AddWithValue("@ApplicationReservationName", objGenRC.ApplicationReservationName);
                    cmd.Parameters.AddWithValue("@Sequence", objGenRC.Sequence);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters.Add("@ProgId", SqlDbType.Int);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                    cmd.Parameters["@ProgId"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    con.Close();
                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Your data has been added successfully.";
                       
                    }
                    return Return.returnHttp("200", (strMessage.ToString()), null);
                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message, null);
                }
            }

        }
        #endregion

        #region Update GenReservationCategory Detail
        [HttpPost]
        public HttpResponseMessage GenReservationCategoryUpdate(GenReservationCategory ObjGRC)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            if (String.IsNullOrWhiteSpace(Convert.ToString(ObjGRC.ApplicationReservationCode)))
            {
                return Return.returnHttp("201", "Please Enter Application Reservation Code", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjGRC.ApplicationReservationName)))
            {
                return Return.returnHttp("201", "Please Enter Application Reservation Name", null);
            }
                    
            else
            {
                try
                {
                    GenReservationCategory objGenRC = new GenReservationCategory();
                    objGenRC.Id = ObjGRC.Id;
                    objGenRC.ApplicationReservationCode = ObjGRC.ApplicationReservationCode;
                    objGenRC.ApplicationReservationName = ObjGRC.ApplicationReservationName;
                  
               
                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("GenReservationCategoryEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", objGenRC.Id);
                    cmd.Parameters.AddWithValue("@ApplicationReservationCode", objGenRC.ApplicationReservationCode);
                    cmd.Parameters.AddWithValue("@ApplicationReservationName", objGenRC.ApplicationReservationName);
                    cmd.Parameters.AddWithValue("@Sequence", objGenRC.Sequence);

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
                        strMessage = "Your data has been modified successfully.";
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

        #region Delete GenReservationCategory Detail
        [HttpPost]
        public HttpResponseMessage MstGenReservationCategoryDelete(GenReservationCategory ObjGRC)
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
                GenReservationCategory objGenRC = new GenReservationCategory();
                objGenRC.Id = ObjGRC.Id;
                SqlCommand cmd = new SqlCommand("GenReservationCategoryDelete", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", objGenRC.Id);
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
                    strMessage = "Your data has been deleted successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion


        #region Get GenReservationCategory Detail_M
        [HttpPost]
        public HttpResponseMessage getGenReservationCategoryList_M()
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

                SqlCommand cmd = new SqlCommand("GenReservationCategoryGet_M", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                List<GenReservationCategory> GRList = new List<GenReservationCategory>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        GenReservationCategory objGenResCat = new GenReservationCategory();
                        objGenResCat.Id = Convert.ToInt32(dr["Id"].ToString());
                        objGenResCat.ApplicationReservationCode = (Convert.ToString(dr["ApplicationReservationCode"])).IsEmpty() ? "-" : Convert.ToString(dr["ApplicationReservationCode"]);
                        objGenResCat.ApplicationReservationName = (Convert.ToString(dr["ApplicationReservationName"])).IsEmpty() ? "-" : Convert.ToString(dr["ApplicationReservationName"]);
                        objGenResCat.Sequence = Convert.ToInt16(dr["Sequence"].ToString());
                        GRList.Add(objGenResCat);
                    }

                }
                return Return.returnHttp("200", GRList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion
    }
}
