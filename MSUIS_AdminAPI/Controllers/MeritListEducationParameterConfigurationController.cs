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
    public class MeritListEducationParameterConfigurationController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region newCode By Stored Procedure - Megha

        [HttpPost]
        public HttpResponseMessage MeritListEducationParameterConfigurationAdd(MeritListEducationParameterConfiguration ObjMeritListInstance)
        {
            //String token = Request.Headers.GetValues("token").FirstOrDefault();
            //TokenOperation ac = new TokenOperation();

            //string res = ac.ValidateToken(token);

            //if (res == "0")
            //{
            //    return Return.returnHttp("0", null, null);
            //}
            if (String.IsNullOrWhiteSpace(ObjMeritListInstance.FieldName))
            {
                return Return.returnHttp("201", "Please enter Field  Name", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjMeritListInstance.level)))
            {
                return Return.returnHttp("201", "Please enter level", null);
            }
            //else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjMeritListInstance.DenominatorType)))
            //{
            //    return Return.returnHttp("201", "Please select Denominator type", null);
            //}
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



                    SqlCommand cmd = new SqlCommand("M_MeritListEducationParameterConfigurationAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MeritListInstanceId", ObjMeritListInstance.MeritListInstanceId);
                    cmd.Parameters.AddWithValue("@FieldName", ObjMeritListInstance.FieldName);
                    cmd.Parameters.AddWithValue("@Level", ObjMeritListInstance.level);
                    cmd.Parameters.AddWithValue("@Weightage", ObjMeritListInstance.Weightage);
                    cmd.Parameters.AddWithValue("@DenominatorType", ObjMeritListInstance.DenominatorType);
                    cmd.Parameters.AddWithValue("@DenominatorValue", ObjMeritListInstance.DenominatorValue);
                    cmd.Parameters.AddWithValue("@DenominatorField", ObjMeritListInstance.DenominatorField);
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
        public HttpResponseMessage MeritListEducationParameterConfigurationEdit(MeritListEducationParameterConfiguration ObjMeritListInstance)
        {
            //String token = Request.Headers.GetValues("token").FirstOrDefault();
            //TokenOperation ac = new TokenOperation();

            //string res = ac.ValidateToken(token);

            //if (res == "0")
            //{
            //    return Return.returnHttp("0", null, null);
            //}
            if (String.IsNullOrWhiteSpace(ObjMeritListInstance.FieldName))
            {
                return Return.returnHttp("201", "Please enter Display Name", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjMeritListInstance.Weightage)))
            {
                return Return.returnHttp("201", "Please enter weightage", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjMeritListInstance.level)))
            {
                return Return.returnHttp("201", "Please enter level", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjMeritListInstance.DenominatorType)))
            {
                return Return.returnHttp("201", "Please select Denominator type", null);
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

                    SqlCommand cmd = new SqlCommand("M_MeritListEducationParameterConfigurationEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@MeritListInstanceId", ObjMeritListInstance.MeritListInstanceId);
                    cmd.Parameters.AddWithValue("@FieldName", ObjMeritListInstance.FieldName);
                    cmd.Parameters.AddWithValue("@Level",ObjMeritListInstance.level);
                    cmd.Parameters.AddWithValue("@Weightage", ObjMeritListInstance.Weightage);
                    cmd.Parameters.AddWithValue("@DenominatorType", ObjMeritListInstance.DenominatorType);
                    cmd.Parameters.AddWithValue("@DenominatorValue", ObjMeritListInstance.DenominatorValue);
                    cmd.Parameters.AddWithValue("@DenominatorField", ObjMeritListInstance.DenominatorField);
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
        public HttpResponseMessage MeritListEducationParameterConfigurationGet(MeritListEducationParameterConfiguration dataString)
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

                List<MeritListEducationParameterConfiguration> MeritListEducationParameterConfigurationList = new List<MeritListEducationParameterConfiguration>();
                BALMeritListEducationParameterConfiguration func = new BALMeritListEducationParameterConfiguration();
                //flag=1,IsDeleted=0,IsActive=null
                MeritListEducationParameterConfigurationList = func.getMeritListEducationParameterConfiguration("admin", 0, null, dataString.MeritListInstanceId);

                return Return.returnHttp("200", MeritListEducationParameterConfigurationList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage MeritListEducationParameterConfigurationGetActive(MeritListEducationParameterConfiguration dataString)
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

                List<MeritListEducationParameterConfiguration> MeritListEducationParameterConfigurationList = new List<MeritListEducationParameterConfiguration>();
                BALMeritListEducationParameterConfiguration func = new BALMeritListEducationParameterConfiguration();
                //flag=1,IsDeleted=0,IsActive=1
                MeritListEducationParameterConfigurationList = func.getMeritListEducationParameterConfiguration("admin", 0, 1, dataString.MeritListInstanceId);

                return Return.returnHttp("200", MeritListEducationParameterConfigurationList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage MeritListEducationParameterConfigurationIsActive(MeritListEducationParameterConfiguration dataString)
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

                SqlCommand cmd = new SqlCommand("M_MeritListEducationParameterConfigurationActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "M_MeritListEducationParameterConfigurationIsActive");
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
        public HttpResponseMessage MeritListEducationParameterConfigurationIsInactive(MeritListEducationParameterConfiguration dataString)
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

                SqlCommand cmd = new SqlCommand("M_MeritListEducationParameterConfigurationActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "M_MeritListEducationParameterConfigurationIsInactive");
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
        public HttpResponseMessage MeritListEducationParameterConfigurationDelete(MeritListEducationParameterConfiguration dataString)
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

                SqlCommand cmd = new SqlCommand("M_MeritListEducationParameterConfigurationDelete", con);
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