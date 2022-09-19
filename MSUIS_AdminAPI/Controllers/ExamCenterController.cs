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
    public class ExamCenterController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region newCode By Stored Procedure - Megha

        [HttpPost]
        public HttpResponseMessage ExamCenterAdd(ExamCenter ObjExamCenter)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            if (String.IsNullOrWhiteSpace(ObjExamCenter.DisplayName))
            {
                return Return.returnHttp("201", "Please enter display name", null);
            }
            else if (String.IsNullOrWhiteSpace(ObjExamCenter.Code))
            {
                return Return.returnHttp("201", "Please enter Code", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjExamCenter.Capacity)))
            {
                return Return.returnHttp("201", "Please enter capacity", null);
            }
            else
            {
                try
                {
                    Int64 UserId = Convert.ToInt64(res.ToString());
                    // Int64 UserId = 1;

                    string DisplayName = ObjExamCenter.DisplayName;
                    string Code = ObjExamCenter.Code;

                    int Capacity = Convert.ToInt32(ObjExamCenter.Capacity);
                   
                    SqlCommand cmd = new SqlCommand("ExamCenterAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Code", Code);
                    cmd.Parameters.AddWithValue("@Capacity", Capacity);
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
        public HttpResponseMessage ExamCenterEdit(ExamCenter ObjExamCenter)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            if (String.IsNullOrWhiteSpace(ObjExamCenter.DisplayName))
            {
                return Return.returnHttp("201", "Please enter display name", null);
            }
            else if (String.IsNullOrWhiteSpace(ObjExamCenter.Code))
            {
                return Return.returnHttp("201", "Please enter Code", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjExamCenter.Capacity)))
            {
                return Return.returnHttp("201", "Please enter capacity", null);
            }
            else
            {
                try
                {
                    Int64 UserId = Convert.ToInt64(res.ToString());
                    //Int64 UserId = 1;

                    string DisplayName = ObjExamCenter.DisplayName;
                    string Code = ObjExamCenter.Code;
                    int Id = Convert.ToInt32(ObjExamCenter.Id);
                    
                    int Capacity = Convert.ToInt32(ObjExamCenter.Capacity);

                    SqlCommand cmd = new SqlCommand("ExamCenterEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@Code", Code);
                    cmd.Parameters.AddWithValue("@Capacity", Capacity);
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
        public HttpResponseMessage ExamCenterListGet()
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
                Int64 UserId = Convert.ToInt64(res.ToString());

                List<ExamCenter> ExamCenterList = new List<ExamCenter>();
                BALExamCenter func = new BALExamCenter();
                //flag=1,IsDeleted=0,IsActive=null
                ExamCenterList = func.getExamCenterList("admin", 0, null);

                return Return.returnHttp("200", ExamCenterList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage ExamCenterListGetActive()
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
                Int64 UserId = Convert.ToInt64(res.ToString());

                List<ExamCenter> ExamCenterList = new List<ExamCenter>();
                BALExamCenter func = new BALExamCenter();
                //flag=1,IsDeleted=0,IsActive=1
                ExamCenterList = func.getExamCenterList("admin", 0, 1);

                return Return.returnHttp("200", ExamCenterList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }


        /// ExamCenterIsActive and ExamCenterIsInactive Created by Jaydev on 24-11-21

        [HttpPost]
        public HttpResponseMessage ExamCenterIsActive(ExamCenter dataString)
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
                // Int64 UserId = 1;

                SqlCommand cmd = new SqlCommand("ExamCenterActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "ExamCenterIsActive");
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
        public HttpResponseMessage ExamCenterIsInactive(ExamCenter dataString)
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

                SqlCommand cmd = new SqlCommand("ExamCenterActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "ExamCenterIsInactive");
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