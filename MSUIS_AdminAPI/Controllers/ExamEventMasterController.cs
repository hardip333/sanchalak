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
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class ExamEventMasterController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region newCode By Stored Procedure - Megha
        [HttpPost]
        public HttpResponseMessage ExamEventMasterAdd(ExamEventMaster ObjExamEventMaster)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            if (String.IsNullOrWhiteSpace(ObjExamEventMaster.DisplayName))
            {
                return Return.returnHttp("201", "Please enter display name", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjExamEventMaster.AcademicYearId)))
            {
                return Return.returnHttp("201", "Please select academicyear", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjExamEventMaster.Month)))
            {
                return Return.returnHttp("201", "Please select month", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjExamEventMaster.Year)))
            {
                return Return.returnHttp("201", "Please select year", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjExamEventMaster.LateFeesFacultyPower)))
            {
                return Return.returnHttp("201", "Please enter Late Fees Faculty Power", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjExamEventMaster.LateFeesVCPower)))
            {
                return Return.returnHttp("201", "Please enter Late Fees VC Power", null);
            }
            else
            {
                try
                {
                    Int64 UserId = Convert.ToInt64(res.ToString());
                    //Int64 UserId = 1;

                    string DisplayName = Convert.ToString(ObjExamEventMaster.DisplayName);

                    int AcademicYearId = Convert.ToInt32(ObjExamEventMaster.AcademicYearId);
                    int Month = Convert.ToInt32(ObjExamEventMaster.Month);
                    int Year = Convert.ToInt32(ObjExamEventMaster.Year);
                    string LateFeesFacultyPower = Convert.ToString(ObjExamEventMaster.LateFeesFacultyPower);
                    string LateFeesVCPower = Convert.ToString(ObjExamEventMaster.LateFeesVCPower);
                    //bool? IsOnline = Convert.ToBoolean(ObjExamEventMaster.IsOnline);

                    SqlCommand cmd = new SqlCommand("ExamEventMasterAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                    cmd.Parameters.AddWithValue("@Month", Month);
                    cmd.Parameters.AddWithValue("@Year", Year);
                    cmd.Parameters.AddWithValue("@DisplayName", DisplayName);
                    cmd.Parameters.AddWithValue("@LateFeesFacultyPower", LateFeesFacultyPower);
                    cmd.Parameters.AddWithValue("@LateFeesVCPower", LateFeesVCPower);
                    //cmd.Parameters.AddWithValue("@IsOnline", IsOnline);
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
        public HttpResponseMessage ExamEventMasterEdit(ExamEventMaster ObjExamEventMaster)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            if (String.IsNullOrWhiteSpace(ObjExamEventMaster.DisplayName))
            {
                return Return.returnHttp("201", "Please enter display name", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjExamEventMaster.AcademicYearId)))
            {
                return Return.returnHttp("201", "Please select academicyear", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjExamEventMaster.Month)))
            {
                return Return.returnHttp("201", "Please select month", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjExamEventMaster.Year)))
            {
                return Return.returnHttp("201", "Please select year", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjExamEventMaster.LateFeesFacultyPower)))
            {
                return Return.returnHttp("201", "Please enter Late Fees Faculty Power", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjExamEventMaster.LateFeesVCPower)))
            {
                return Return.returnHttp("201", "Please enter Late Fees VC Power", null);
            }
            else
            {
                try
                {
                    Int64 UserId = Convert.ToInt64(res.ToString());
                    //Int64 UserId = 1;

                    string DisplayName = Convert.ToString(ObjExamEventMaster.DisplayName);

                    int AcademicYearId = Convert.ToInt32(ObjExamEventMaster.AcademicYearId);
                    int Month = Convert.ToInt32(ObjExamEventMaster.Month);
                    int Year = Convert.ToInt32(ObjExamEventMaster.Year);
                    int Id = Convert.ToInt32(ObjExamEventMaster.Id);
                    string LateFeesFacultyPower = Convert.ToString(ObjExamEventMaster.LateFeesFacultyPower);
                    string LateFeesVCPower = Convert.ToString(ObjExamEventMaster.LateFeesVCPower);
                    //bool? IsOnline = Convert.ToBoolean(ObjExamEventMaster.IsOnline);

                    SqlCommand cmd = new SqlCommand("ExamEventMasterEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                    cmd.Parameters.AddWithValue("@Month", Month);
                    cmd.Parameters.AddWithValue("@Year", Year);
                    cmd.Parameters.AddWithValue("@DisplayName", DisplayName);
                    cmd.Parameters.AddWithValue("@LateFeesFacultyPower", LateFeesFacultyPower);
                    cmd.Parameters.AddWithValue("@LateFeesVCPower", LateFeesVCPower);
                    //cmd.Parameters.AddWithValue("@IsOnline", IsOnline);
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
        public HttpResponseMessage ExamEventMasterListGet(ExamEventMaster dataString)
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

                List<ExamEventMaster> ExamEventMasterList = new List<ExamEventMaster>();
                BALExamEventMaster func = new BALExamEventMaster();
                //flag=1,IsDeleted=0,IsActive=null
                ExamEventMasterList = func.getExamEventMasterList("admin", 0, null, dataString.AcademicYearId);

                return Return.returnHttp("200", ExamEventMasterList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage ExamEventMasterListGetActive(ExamEventMaster dataString)
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

                List<ExamEventMaster> ExamEventMasterList = new List<ExamEventMaster>();
                BALExamEventMaster func = new BALExamEventMaster();
                //flag=1,IsDeleted=0,IsActive=1
                ExamEventMasterList = func.getExamEventMasterList("admin", 0, 1, dataString.AcademicYearId);

                return Return.returnHttp("200", ExamEventMasterList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        /// ExamEventMasterIsActive and ExamEventMasterIsInactive Created by Jaydev on 24-11-21

        [HttpPost]
        public HttpResponseMessage ExamEventMasterIsActive(ExamEventMaster dataString)
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

                Int32 Id = (int)dataString.Id;
                Int64 UserId = Convert.ToInt64(res.ToString());
                //Int64 UserId = 1;

                SqlCommand cmd = new SqlCommand("ExamEventMasterActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "ExamEventMasterIsActive");
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
        public HttpResponseMessage ExamEventMasterIsInactive(ExamEventMaster dataString)
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

                Int32 Id = (int)dataString.Id;
                Int64 UserId = Convert.ToInt64(res.ToString());
                //Int64 UserId = 1;

                SqlCommand cmd = new SqlCommand("ExamEventMasterActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "ExamEventMasterIsInactive");
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
