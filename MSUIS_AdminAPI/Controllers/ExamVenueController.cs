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
    public class ExamVenueController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region newCode By Stored Procedure - Megha
        [HttpPost]
        public HttpResponseMessage ExamVenueAdd(ExamVenue ObjExamVenue)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            if (String.IsNullOrWhiteSpace(ObjExamVenue.DisplayName))
            {
                return Return.returnHttp("201", "Please enter display name", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjExamVenue.ExamCenterId)))
            {
                return Return.returnHttp("201", "Please select examcenter", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjExamVenue.Code)))
            {
                return Return.returnHttp("201", "Please enter code", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjExamVenue.Address)))
            {
                return Return.returnHttp("201", "Please enter address", null);
            }
            else
            {
                try
                {
                     Int64 UserId = Convert.ToInt64(res.ToString());
                    // Int64 UserId = 1;

                    string DisplayName = Convert.ToString(ObjExamVenue.DisplayName);

                    int ExamcenterId = Convert.ToInt32(ObjExamVenue.ExamCenterId);
                    string Code = ObjExamVenue.Code;
                    string Address = ObjExamVenue.Address;


                    SqlCommand cmd = new SqlCommand("ExamVenueAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ExamCenterId", ExamcenterId);
                    cmd.Parameters.AddWithValue("@Code", Code);
                    cmd.Parameters.AddWithValue("@Address", Address);
                    cmd.Parameters.AddWithValue("@DisplayName", DisplayName);
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
        public HttpResponseMessage ExamVenueEdit(ExamVenue ObjExamVenue)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            if (String.IsNullOrWhiteSpace(ObjExamVenue.DisplayName))
            {
                return Return.returnHttp("201", "Please enter display name", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjExamVenue.ExamCenterId)))
            {
                return Return.returnHttp("201", "Please select examcenter", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjExamVenue.Code)))
            {
                return Return.returnHttp("201", "Please enter code", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjExamVenue.Address)))
            {
                return Return.returnHttp("201", "Please enter address", null);
            }
            else
            {
                try
                {
                    Int64 UserId = Convert.ToInt64(res.ToString());
                    //Int64 UserId = 1;

                    string DisplayName = Convert.ToString(ObjExamVenue.DisplayName);
                    int Id = Convert.ToInt32(ObjExamVenue.Id);
                    int ExamcenterId = Convert.ToInt32(ObjExamVenue.ExamCenterId);
                    string Code = ObjExamVenue.Code;
                    string Address = ObjExamVenue.Address;


                    SqlCommand cmd = new SqlCommand("ExamVenueEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@ExamCenterId", ExamcenterId);
                    cmd.Parameters.AddWithValue("@Code", Code);
                    cmd.Parameters.AddWithValue("@Address", Address);
                    cmd.Parameters.AddWithValue("@DisplayName", DisplayName);
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
        public HttpResponseMessage ExamVenueListGet(ExamVenue dataString)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                Request.Headers.TryGetValues("InstituteId", out IEnumerable<string> values);
                Int32 InstituteId = Convert.ToInt32(values?.FirstOrDefault());
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
                Int64 UserId = Convert.ToInt64(res.ToString());

                List<ExamVenue> ExamVenueList = new List<ExamVenue>();
                BALExamVenue func = new BALExamVenue();
                //flag=1,IsDeleted=0,IsActive=null
                ExamVenueList = func.getExamVenueList("admin", 0, null, dataString.ExamCenterId, InstituteId, UserId);

                return Return.returnHttp("200", ExamVenueList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage ExamVenueListGetActive(ExamVenue dataString)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                //Int32 InstituteId = Convert.ToInt32(Request.Headers.GetValues("InstituteId").FirstOrDefault());
                Request.Headers.TryGetValues("InstituteId", out IEnumerable<string> values);
                Int32 InstituteId = Convert.ToInt32(values?.FirstOrDefault());
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
                Int64 UserId = Convert.ToInt64(res.ToString());

                List<ExamVenue> ExamVenueList = new List<ExamVenue>();
                BALExamVenue func = new BALExamVenue();
                //flag=1,IsDeleted=0,IsActive=1
                ExamVenueList = func.getExamVenueList("ByInstitute", 0, 1, dataString.ExamCenterId, InstituteId, UserId);

                return Return.returnHttp("200", ExamVenueList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }


        /// ExamVenueIsActive and ExamVenueIsInactive Created by Jaydev on 24-11-21

        [HttpPost]
        public HttpResponseMessage ExamVenueIsActive(ExamCenter dataString)
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

                Int32 Id = dataString.Id;
                Int64 UserId = Convert.ToInt64(res.ToString());
                //Int64 UserId = 1;

                SqlCommand cmd = new SqlCommand("ExamVenueActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "ExamVenueIsActive");
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
        public HttpResponseMessage ExamVenueIsInactive(ExamVenue dataString)
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

                Int32 Id = dataString.Id;
                //Int64 UserId = Convert.ToInt64(res.ToString());
                Int64 UserId = 1;

                 SqlCommand cmd = new SqlCommand("ExamVenueActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "ExamVenueIsInactive");
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

        #endregion
    }
}