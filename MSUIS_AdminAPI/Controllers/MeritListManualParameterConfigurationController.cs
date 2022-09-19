using MSUIS_TokenManager.App_Start;
using MSUISApi.BAL;
using MSUISApi.Models;
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
    public class MeritListManualParameterConfigurationController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region newCode By Stored Procedure - Megha
       
        [HttpPost]
        public HttpResponseMessage MeritListManualParameterConfigurationAdd(MeritListManualParameterConfiguration ObjMeritListInstance)
        {
            //String token = Request.Headers.GetValues("token").FirstOrDefault();
            //TokenOperation ac = new TokenOperation();

            //string res = ac.ValidateToken(token);

            //if (res == "0")
            //{
            //    return Return.returnHttp("0", null, null);
            //}
            if (String.IsNullOrWhiteSpace(ObjMeritListInstance.DisplayName))
            {
                return Return.returnHttp("201", "Please enter Display Name", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjMeritListInstance.Weightage)))
            {
                return Return.returnHttp("201", "Please enter weightage", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjMeritListInstance.Denominator)))
            {
                return Return.returnHttp("201", "Please enter Denominator", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjMeritListInstance.Sequence)))
            {
                return Return.returnHttp("201", "Please enter Sequence", null);
            }
            else
            {
                try
                {
                    //Int64 UserId = Convert.ToInt64(res.ToString());
                    Int64 UserId = 1;



                    SqlCommand cmd = new SqlCommand("M_MeritListManualParameterConfigurationAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MeritListInstanceId", ObjMeritListInstance.MeritListInstanceId);
                    cmd.Parameters.AddWithValue("@DisplayName", ObjMeritListInstance.DisplayName);
                    cmd.Parameters.AddWithValue("@Weightage", ObjMeritListInstance.Weightage);
                    cmd.Parameters.AddWithValue("@Value", ObjMeritListInstance.Value);
                    cmd.Parameters.AddWithValue("@Denominator", ObjMeritListInstance.Denominator);
                    cmd.Parameters.AddWithValue("@Sequence", ObjMeritListInstance.Sequence);
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

        [HttpPost]
        public HttpResponseMessage MeritListManualParameterConfigurationEdit(MeritListManualParameterConfiguration ObjMeritListInstance)
        {
            //String token = Request.Headers.GetValues("token").FirstOrDefault();
            //TokenOperation ac = new TokenOperation();

            //string res = ac.ValidateToken(token);

            //if (res == "0")
            //{
            //    return Return.returnHttp("0", null, null);
            //}
            if (String.IsNullOrWhiteSpace(ObjMeritListInstance.DisplayName))
            {
                return Return.returnHttp("201", "Please enter Display Name", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjMeritListInstance.Weightage)))
            {
                return Return.returnHttp("201", "Please enter weightage", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjMeritListInstance.Denominator)))
            {
                return Return.returnHttp("201", "Please enter Denominator", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjMeritListInstance.Sequence)))
            {
                return Return.returnHttp("201", "Please enter Sequence", null);
            }
            else
            {
                try
                {
                    // Int64 UserId = Convert.ToInt64(res.ToString());
                    Int64 UserId = 1;

                    int? Id = Convert.ToInt16(ObjMeritListInstance.Id);

                    SqlCommand cmd = new SqlCommand("M_MeritListManualParameterConfigurationEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@MeritListInstanceId", ObjMeritListInstance.MeritListInstanceId);
                    cmd.Parameters.AddWithValue("@DisplayName", ObjMeritListInstance.DisplayName);
                    cmd.Parameters.AddWithValue("@Weightage", ObjMeritListInstance.Weightage);
                    cmd.Parameters.AddWithValue("@Value", ObjMeritListInstance.Value);
                    cmd.Parameters.AddWithValue("@Denominator", ObjMeritListInstance.Denominator);
                    cmd.Parameters.AddWithValue("@Sequence", ObjMeritListInstance.Sequence);
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

        [HttpPost]
        public HttpResponseMessage MeritListManualParameterConfigurationGet(MeritListManualParameterConfiguration dataString)
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

                List<MeritListManualParameterConfiguration> MeritListManualParameterConfigurationList = new List<MeritListManualParameterConfiguration>();
                BALMeritListManualParameterConfiguration func = new BALMeritListManualParameterConfiguration();
                //flag=1,IsDeleted=0,IsActive=null
                MeritListManualParameterConfigurationList = func.getMeritListManualParameterConfiguration("admin", 0, null,dataString.MeritListInstanceId);

                return Return.returnHttp("200", MeritListManualParameterConfigurationList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage MeritListManualParameterConfigurationGetActive(MeritListManualParameterConfiguration dataString)
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

                List<MeritListManualParameterConfiguration> MeritListManualParameterConfigurationList = new List<MeritListManualParameterConfiguration>();
                BALMeritListManualParameterConfiguration func = new BALMeritListManualParameterConfiguration();
                //flag=1,IsDeleted=0,IsActive=1
                MeritListManualParameterConfigurationList = func.getMeritListManualParameterConfiguration("admin", 0, 1, dataString.MeritListInstanceId);

                return Return.returnHttp("200", MeritListManualParameterConfigurationList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage MeritListManualParameterConfigurationIsActive(MeritListManualParameterConfiguration dataString)
        {
            //String token = Request.Headers.GetValues("token").FirstOrDefault();
            //TokenOperation ac = new TokenOperation();

            //string res = ac.ValidateToken(token);

            //if (res == "0")
            //{
            //    return Return.returnHttp("0", null, null);
            //}
            try
            {

                Int32 Id = (int)dataString.Id;
                // Int64 UserId = Convert.ToInt64(res.ToString());
                Int64 UserId = 1;

                SqlCommand cmd = new SqlCommand("M_MeritListManualParameterConfigurationActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "M_MeritListManualParameterConfigurationIsActive");
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

        [HttpPost]
        public HttpResponseMessage MeritListManualParameterConfigurationIsInactive(MeritListManualParameterConfiguration dataString)
        {
            //String token = Request.Headers.GetValues("token").FirstOrDefault();
            //TokenOperation ac = new TokenOperation();

            //string res = ac.ValidateToken(token);

            //if (res == "0")
            //{
            //    return Return.returnHttp("0", null, null);
            //}
            try
            {

                Int32 Id = (int)dataString.Id;
                //Int64 UserId = Convert.ToInt64(res.ToString());
                Int64 UserId = 1;

                SqlCommand cmd = new SqlCommand("M_MeritListManualParameterConfigurationActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "M_MeritListManualParameterConfigurationIsInactive");
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
                    strMessage = "Your data has been Inactive Successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }

        [HttpPost]
        public HttpResponseMessage MeritListManualParameterConfigurationDelete(MeritListManualParameterConfiguration dataString)
        {
            //String token = Request.Headers.GetValues("token").FirstOrDefault();
            //TokenOperation ac = new TokenOperation();

            //string res = ac.ValidateToken(token);

            //if (res == "0")
            //{
            //    return Return.returnHttp("0", null, null);
            //}
            try
            {
                // Int64 UserId = Convert.ToInt64(res.ToString());
                Int64 UserId = 1;

                int? Id = Convert.ToInt16(dataString.Id);

                SqlCommand cmd = new SqlCommand("M_MeritListManualParameterConfigurationDelete", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
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

    }
}