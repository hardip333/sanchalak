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
    public class MeritListSpecialCategoryConfigurationController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region newCode By Stored Procedure - Megha

        [HttpPost]
        public HttpResponseMessage MeritListSpecialCategoryConfigurationAdd(MeritListSpecialCategoryConfiguration ObjMeritListSpecialCategoryConfiguration)
        {
            //String token = Request.Headers.GetValues("token").FirstOrDefault();
            //TokenOperation ac = new TokenOperation();

            //string res = ac.ValidateToken(token);

            //if (res == "0")
            //{
            //    return Return.returnHttp("0", null, null);
            //}
            if (String.IsNullOrWhiteSpace(ObjMeritListSpecialCategoryConfiguration.MeritListInstanceId.ToString()))
            {
                return Return.returnHttp("201", "Please select merit list instance", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjMeritListSpecialCategoryConfiguration.GenReservationCategoryId.ToString())))
            {
                return Return.returnHttp("201", "Please select gen reservation category.", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjMeritListSpecialCategoryConfiguration.PhysicallyHandicapReservedPercentage)))
            {
                return Return.returnHttp("201", "Please enter physically handicap percentage", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjMeritListSpecialCategoryConfiguration.EWSReservedPercentage)))
            {
                return Return.returnHttp("201", "Please enter EWS percentage", null);
            }
            else
            {
                try
                {
                    //Int64 UserId = Convert.ToInt64(res.ToString());
                    Int64 UserId = 1;

                    SqlCommand cmd = new SqlCommand("M_MeritListSpecialCategoryConfigurationAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MeritListInstanceId", ObjMeritListSpecialCategoryConfiguration.MeritListInstanceId);
                    cmd.Parameters.AddWithValue("@GenReservationCategoryId", ObjMeritListSpecialCategoryConfiguration.GenReservationCategoryId);
                    cmd.Parameters.AddWithValue("@PhysicallyHandicapReservedPercentage", ObjMeritListSpecialCategoryConfiguration.PhysicallyHandicapReservedPercentage);
                    cmd.Parameters.AddWithValue("@EWSReservedPercentage", ObjMeritListSpecialCategoryConfiguration.EWSReservedPercentage);
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
        public HttpResponseMessage MeritListSpecialCategoryConfigurationEdit(MeritListSpecialCategoryConfiguration ObjMeritListSpecialCategoryConfiguration)
        {
            //String token = Request.Headers.GetValues("token").FirstOrDefault();
            //TokenOperation ac = new TokenOperation();

            //string res = ac.ValidateToken(token);

            //if (res == "0")
            //{
            //    return Return.returnHttp("0", null, null);
            //}
            if (String.IsNullOrWhiteSpace(ObjMeritListSpecialCategoryConfiguration.MeritListInstanceId.ToString()))
            {
                return Return.returnHttp("201", "Please select merit list instance", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjMeritListSpecialCategoryConfiguration.GenReservationCategoryId.ToString())))
            {
                return Return.returnHttp("201", "Please select gen reservation category.", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjMeritListSpecialCategoryConfiguration.PhysicallyHandicapReservedPercentage)))
            {
                return Return.returnHttp("201", "Please enter physically handicap percentage", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjMeritListSpecialCategoryConfiguration.EWSReservedPercentage)))
            {
                return Return.returnHttp("201", "Please enter EWS percentage", null);
            }
            else
            {
                try
                {
                    // Int64 UserId = Convert.ToInt64(res.ToString());
                    Int64 UserId = 1;

                    int? Id = Convert.ToInt16(ObjMeritListSpecialCategoryConfiguration.Id);

                    SqlCommand cmd = new SqlCommand("M_MeritListSpecialCategoryConfigurationEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                   
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@MeritListInstanceId", ObjMeritListSpecialCategoryConfiguration.MeritListInstanceId);
                    cmd.Parameters.AddWithValue("@GenReservationCategoryId", ObjMeritListSpecialCategoryConfiguration.GenReservationCategoryId);
                    cmd.Parameters.AddWithValue("@PhysicallyHandicapReservedPercentage", ObjMeritListSpecialCategoryConfiguration.PhysicallyHandicapReservedPercentage);
                    cmd.Parameters.AddWithValue("@EWSReservedPercentage", ObjMeritListSpecialCategoryConfiguration.EWSReservedPercentage);
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
        public HttpResponseMessage MeritListSpecialCategoryConfigurationGet(MeritListSpecialCategoryConfiguration dataString)
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

                List<MeritListSpecialCategoryConfiguration> MeritListSpecialCategoryConfigurationList = new List<MeritListSpecialCategoryConfiguration>();
                BALMeritListSpecialCategoryConfiguration func = new BALMeritListSpecialCategoryConfiguration();
                //flag=1,IsDeleted=0,IsActive=null
                MeritListSpecialCategoryConfigurationList = func.getMeritListSpecialCategoryConfigurationDetails("admin", 0, null, dataString.MeritListInstanceId);

                return Return.returnHttp("200", MeritListSpecialCategoryConfigurationList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage MeritListSpecialCategoryConfigurationGetActive(MeritListSpecialCategoryConfiguration dataString)
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

                List<MeritListSpecialCategoryConfiguration> MeritListSpecialCategoryConfigurationList = new List<MeritListSpecialCategoryConfiguration>();
                BALMeritListSpecialCategoryConfiguration func = new BALMeritListSpecialCategoryConfiguration();
                //flag=1,IsDeleted=0,IsActive=1
                MeritListSpecialCategoryConfigurationList = func.getMeritListSpecialCategoryConfigurationDetails("admin", 0, 1, dataString.MeritListInstanceId);

                return Return.returnHttp("200", MeritListSpecialCategoryConfigurationList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage MeritListSpecialCategoryConfigurationIsActive(MeritListSpecialCategoryConfiguration dataString)
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

                SqlCommand cmd = new SqlCommand("M_MeritListSpecialCategoryConfigurationActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "M_MeritListSpecialCategoryConfigurationIsActive");
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
        public HttpResponseMessage MeritListSpecialCategoryConfigurationIsInactive(MeritListSpecialCategoryConfiguration dataString)
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

                SqlCommand cmd = new SqlCommand("M_MeritListSpecialCategoryConfigurationActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "M_MeritListSpecialCategoryConfigurationIsInactive");
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
        public HttpResponseMessage MeritListSpecialCategoryConfigurationDelete(MeritListSpecialCategoryConfiguration dataString)
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

                SqlCommand cmd = new SqlCommand("M_MeritListSpecialCategoryConfigurationDelete", con);
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