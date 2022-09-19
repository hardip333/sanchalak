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
using MSUISApi.BAL;


namespace MSUISApi.Controllers
{
    public class SocialCategoryController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region SocialCategoryGet
        [HttpPost]
        public HttpResponseMessage MstSocialCategoryGet()
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
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


                SqlCommand Cmd = new SqlCommand("MstSocialCategoryGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);


                List<SocialCategory> ObjLstSC = new List<SocialCategory>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        SocialCategory objSC = new SocialCategory();

                        objSC.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objSC.SocialCategoryCode = Convert.ToString(Dt.Rows[i]["SocialCategoryCode"]);
                        objSC.SocialCategoryName = Convert.ToString(Dt.Rows[i]["SocialCategoryName"]);
                        objSC.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjLstSC.Add(objSC);
                    }
                }
                return Return.returnHttp("200", ObjLstSC, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region SocialCategoryAdd
        [HttpPost]
        public HttpResponseMessage MstSocialCategoryAdd(SocialCategory SC)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
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


                if (String.IsNullOrWhiteSpace(Convert.ToString(SC.SocialCategoryName))) { return Return.returnHttp("201", "Kindly Enter Social Category Name", null); }

                else
                {
                    String SocialCategoryName = Convert.ToString(SC.SocialCategoryName);
                    String SocialCategoryCode = Convert.ToString(SC.SocialCategoryCode);
                   
                    SqlCommand cmd = new SqlCommand("MstSocialCategoryAdd", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SocialCategoryName", SocialCategoryName);
                    cmd.Parameters.AddWithValue("@SocialCategoryCode", SocialCategoryCode);
                    
                    cmd.Parameters.AddWithValue("@UserId", 0);
                    //cmd.Parameters.AddWithValue("@UserTime", datetime);

                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);

                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    Con.Open();

                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);

                    Con.Close();

                    return Return.returnHttp("200", strMessage.ToString(), null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region SocialCategoryEdit
        [HttpPost]
        public HttpResponseMessage MstSocialCategoryEdit(SocialCategory SC)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
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


                if (String.IsNullOrWhiteSpace(Convert.ToString(SC.SocialCategoryName))) { return Return.returnHttp("201", "Kindly Enter Social Category Name", null); }
                else if (String.IsNullOrEmpty(Convert.ToString(SC.Id))) { return Return.returnHttp("201", "Please Try Again Server Error", null); }
                else
                {
                    Int32 Id = Convert.ToInt32(SC.Id);
                    String SocialCategoryName = Convert.ToString(SC.SocialCategoryName);
                    String SocialCategoryCode = Convert.ToString(SC.SocialCategoryCode);
                    

                    SqlCommand cmd = new SqlCommand("MstSocialCategoryEdit", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@SocialCategoryName", SocialCategoryName);
                    cmd.Parameters.AddWithValue("@SocialCategoryCode", SocialCategoryCode);
                    
                    cmd.Parameters.AddWithValue("@UserId", 0);
                    //cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    Con.Open();

                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);

                    Con.Close();

                    return Return.returnHttp("200", strMessage.ToString(), null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region SocialCategoryDelete
        [HttpPost]
        public HttpResponseMessage MstSocialCategoryDelete(SocialCategory SC)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
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


                Int32 Id = Convert.ToInt32(SC.Id);

                SqlCommand cmd = new SqlCommand("MstSocialCategoryDelete", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", 0);
                cmd.Parameters.AddWithValue("@Id", Id);
                //cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);

                Con.Close();

                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region SocialCategoryActive
        [HttpPost]
        public HttpResponseMessage MstSocialCategoryActive(SocialCategory SC)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                Int32 Id = Convert.ToInt32(SC.Id);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                SqlCommand cmd = new SqlCommand("MstSocialCategoryActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstSocialCategoryIsActiveEnable");
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

        #region SocialCategorySuspend
        [HttpPost]
        public HttpResponseMessage MstSocialCategorySuspend(SocialCategory SC)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                Int32 Id = Convert.ToInt32(SC.Id);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                SqlCommand cmd = new SqlCommand("MstSocialCategoryActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstSocialCategoryIsActiveDisable");
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

        #region MstSocialCategoryGetbyId
        [HttpPost]
        public HttpResponseMessage MstSocialCategoryGetbyId(SocialCategory SC)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
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

            try
            {
                Int64 UserId = Convert.ToInt64(res.ToString());

                Int32 Id = Convert.ToInt32(SC.Id);

                SqlCommand cmd = new SqlCommand("MstSocialCategoryGetbyId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);

                List<SocialCategory> ObjLstSC = new List<SocialCategory>();
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        SocialCategory objSC = new SocialCategory();

                        objSC.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objSC.SocialCategoryName = Convert.ToString(Dt.Rows[i]["SocialCategoryName"]);
                        objSC.SocialCategoryCode = Convert.ToString(Dt.Rows[i]["SocialCategoryCode"]);

                        objSC.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);

                        ObjLstSC.Add(objSC);
                    }
                }
                return Return.returnHttp("200", ObjLstSC, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion




    }
}
