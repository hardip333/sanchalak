using MSUIS_TokenManager.App_Start;
using MSUISApi.BAL;
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
    public class MstChoiceTypeController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        //Validation validation = new Validation();

        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));


        #region ChoiceType Get
        public HttpResponseMessage MstChoiceTypeGet()
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
                SqlCommand cmd = new SqlCommand("MstChoiceTypeGet", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<MstChoiceType> List = new List<MstChoiceType>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstChoiceType ObjListChoice = new MstChoiceType();
                        ObjListChoice.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        ObjListChoice.ChoiceName = Convert.ToString(Dt.Rows[i]["ChoiceName"]);
                        ObjListChoice.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjListChoice.IsDeleted = Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);
                       
                        List.Add(ObjListChoice);
                    }
                }
                return Return.returnHttp("200", List, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion


        #region ChoiceType Add
        [HttpPost]
        public HttpResponseMessage MstChoiceTypeAdd(MstChoiceType ObjChoice)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjChoice.ChoiceName)))
            {
                return Return.returnHttp("201", "Please enter choice name", null);
            }
            else
            {
                try
                {
                    Int32 Id = Convert.ToInt32(ObjChoice.Id);

                    string ChoiceName = Convert.ToString(ObjChoice.ChoiceName);
                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("MstChoiceTypeAdd", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@ChoiceName", ChoiceName);
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

        #region ChoiceType Edit
        [HttpPost]
        public HttpResponseMessage MstChoiceTypeEdit(MstChoiceType ObjChoice)
        {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();

                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjChoice.ChoiceName)))
            {
                return Return.returnHttp("201", "Please enter choice name", null);
            }
            else
                {
                    try
                    {
                        Int32 Id = Convert.ToInt32(ObjChoice.Id);
                        string ChoiceName = Convert.ToString(ObjChoice.ChoiceName);
                        Int64 UserId = Convert.ToInt64(res.ToString());


                        SqlCommand cmd = new SqlCommand("MstChoiceTypeEdit", Con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", Id);
                        cmd.Parameters.AddWithValue("@ChoiceName", ChoiceName);
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

                    catch (Exception e)
                    {
                        return Return.returnHttp("201", e.Message, null);
                    }
                }
            
        }
        #endregion

        #region ChoiceType Delete
        [HttpPost]
        public HttpResponseMessage MstChoiceTypeDelete(MstChoiceType ObjChoice)
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

                Int32 Id = Convert.ToInt32(ObjChoice.Id);
                string ChoiceName = Convert.ToString(ObjChoice.ChoiceName);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("MstChoiceTypeDelete", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@ChoiceName", ChoiceName);
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
                    strMessage = "Your data has been Deleted successfully.";
                }

                return Return.returnHttp("200", strMessage.ToString(), null);
            }

            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

       

        #region Choice Type Active
        [HttpPost]
        public HttpResponseMessage MstChoiceIsActive(MstChoiceType ObjChoice)
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

                Int32 Id = Convert.ToInt32(ObjChoice.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("MstChoiceIsActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstChoiceIsActiveEnable");
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);

                cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@MESSAGE"].Value);
                Con.Close();

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

        #region Choice Type Suspend
        [HttpPost]
        public HttpResponseMessage MstChoiceIsSuspended(MstChoiceType ObjChoice)
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

                Int32 Id = Convert.ToInt32(ObjChoice.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("MstChoiceIsSuspended", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstChoiceIsSuspendedDisable");
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);

                cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@MESSAGE"].Value);
                Con.Close();
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
    }
}
