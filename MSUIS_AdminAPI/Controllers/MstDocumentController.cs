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


namespace MSUISApi.Controllers
{
    public class MstDocumentController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));


        [HttpPost]
        public HttpResponseMessage MstDocumentGet()
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

                SqlCommand cmd = new SqlCommand("MstDocumentGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<MstDocument> ObjLstMD = new List<MstDocument>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstDocument ObjMD = new MstDocument();
                        ObjMD.Id = Convert.ToInt64(dr["Id"]);
                        ObjMD.NameOfTheDocument = (dr["NameOfTheDocument"].ToString());
                        ObjMD.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        ObjLstMD.Add(ObjMD);
                    }

                }
                return Return.returnHttp("200", ObjLstMD, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }




        #region MstDocument Add
        [HttpPost]
        public HttpResponseMessage MstDocumentAdd(MstDocument mstDoc)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            if (String.IsNullOrWhiteSpace(Convert.ToString(mstDoc.NameOfTheDocument)))
            {
                return Return.returnHttp("201", "Please enter Document Name", null);
            }
           

            else
            {
                try
                {
                    String NameOfTheDocument = Convert.ToString(mstDoc.NameOfTheDocument);                  
                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("MstDocumentAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@NameOfTheDocument", NameOfTheDocument);
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
                        strMessage = "Your data has been added successfully.";
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
       
        #region MstDocument Edit
        [HttpPost]
        public HttpResponseMessage MstDocumentEdit(MstDocument mstDoc)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            if (String.IsNullOrWhiteSpace(Convert.ToString(mstDoc.NameOfTheDocument)))
            {
                return Return.returnHttp("201", "Please enter Document Name", null);
            }
            
            else
            {
                try
                {

                    Int64 Id = Convert.ToInt64(mstDoc.Id);
                    String NameOfTheDocument = Convert.ToString(mstDoc.NameOfTheDocument);
                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("MstDocumentEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@NameOfTheDocument", NameOfTheDocument);
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
                        strMessage = "Your data has been Modified successfully.";
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
        

        [HttpPost]
        public HttpResponseMessage MstDocumentDelete(MstDocument mstDoc)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                //String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                string resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }


                Int64 Id = Convert.ToInt64(mstDoc.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());
                SqlCommand cmd = new SqlCommand("MstDocumentDelete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstDocumentDelete");
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);

                con.Close();
                return Return.returnHttp("200", "Data Deleted", null);
                
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }

        [HttpPost]
        public HttpResponseMessage MstDocumentIsActiveEnable(MstDocument mstDoc)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                //String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                string resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }


                Int64 Id = Convert.ToInt64(mstDoc.Id);

                SqlCommand cmd = new SqlCommand("MstDocumentActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstDocumentIsActiveEnable");
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", 0);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
               
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();
                return Return.returnHttp("200", "Set To Active", null);
                //return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }

        [HttpPost]
        public HttpResponseMessage MstDocumentIsActiveDisable(MstDocument mstDoc)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                //String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                string resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }


                Int64 Id = Convert.ToInt64(mstDoc.Id);

                SqlCommand cmd = new SqlCommand("MstDocumentActive", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", 0);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.AddWithValue("@Flag", "MstDocumentIsActiveDisable");
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();
                return Return.returnHttp("200", "Set To Disable", null);
                //return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }

    }
}