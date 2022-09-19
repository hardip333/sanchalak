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
    public class ExamFeeHeadController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region newCode By Stored Procedure - Megha
        [HttpPost]
        public HttpResponseMessage ExamFeeHeadAdd(ExamFeeHead ObjExamFeeHead)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            if (String.IsNullOrWhiteSpace(ObjExamFeeHead.ExamFeeHeadName))
            {
                return Return.returnHttp("201", "Please enter Exam Fee Head name", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjExamFeeHead.ExamFeeHeadCode)))
            {
                return Return.returnHttp("201", "Please enter Exam Fee Head Code", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjExamFeeHead.DayRange)))
            {
                return Return.returnHttp("201", "Please select dayrange", null);
            }
            else
            {
                try
                {
                     Int64 UserId = Convert.ToInt64(res.ToString());
                    // Int64 UserId = 1;

                    string ExamfeeHeadName = Convert.ToString(ObjExamFeeHead.ExamFeeHeadName);
                    string ExamFeeHeadCode = ObjExamFeeHead.ExamFeeHeadCode;

                    int DayRange = Convert.ToInt32(ObjExamFeeHead.DayRange);

                    SqlCommand cmd = new SqlCommand("ExamFeeHeadAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ExamFeeHeadName", ObjExamFeeHead.ExamFeeHeadName);
                    cmd.Parameters.AddWithValue("@ExamFeeHeadCode", ObjExamFeeHead.ExamFeeHeadCode);
                    cmd.Parameters.AddWithValue("@DayRange", ObjExamFeeHead.DayRange);
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
        public HttpResponseMessage ExamFeeHeadEdit(ExamFeeHead ObjExamFeeHead)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            if (String.IsNullOrWhiteSpace(ObjExamFeeHead.ExamFeeHeadName))
            {
                return Return.returnHttp("201", "Please enter exam Fee Head", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjExamFeeHead.ExamFeeHeadCode)))
            {
                return Return.returnHttp("201", "Please enter exam  Fee Code", null);
            }
            else
            {
                try
                {
                    Int64 UserId = Convert.ToInt64(res.ToString());
                    // Int64 UserId = 1;

                    int Id = Convert.ToInt32(ObjExamFeeHead.Id);
                    string ExamFeeHeadName = ObjExamFeeHead.ExamFeeHeadName;
                    string ExamFeeHeadCode = ObjExamFeeHead.ExamFeeHeadCode;

                    int DayRange = Convert.ToInt32(ObjExamFeeHead.DayRange);
                    SqlCommand cmd = new SqlCommand("ExamFeeHeadEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@ExamFeeHeadCode", ExamFeeHeadCode);
                    cmd.Parameters.AddWithValue("@ExamFeeHeadName", ExamFeeHeadName);
                    cmd.Parameters.AddWithValue("@DayRange", DayRange);
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
        public HttpResponseMessage ExamFeeHeadListGet()
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

                List<ExamFeeHead> ExamFeeHeadList = new List<ExamFeeHead>();
                BALExamFeeHead func = new BALExamFeeHead();
                //flag=1,IsDeleted=0,IsActive=null
                ExamFeeHeadList = func.getExamFeeHeadList("admin", 0, null);

                return Return.returnHttp("200", ExamFeeHeadList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage ExamFeeHeadListGetActive()
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

                List<ExamFeeHead> ExamFeeHeadList = new List<ExamFeeHead>();
                BALExamFeeHead func = new BALExamFeeHead();
                //flag=1,IsDeleted=0,IsActive=1
                ExamFeeHeadList = func.getExamFeeHeadList("admin", 0, 1);

                return Return.returnHttp("200", ExamFeeHeadList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage getExamFeeHeadListForFacultyExamMap(FacultyExamMap dataString)
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


                List<ExamFeeHead> ExamFeeHeadList = new List<ExamFeeHead>();
                BALExamFeeHead func = new BALExamFeeHead();
                //flag=1,IsDeleted=0,IsActive=null
                if (!string.IsNullOrEmpty(dataString.StartDateOfExam) && !string.IsNullOrEmpty(dataString.LastDateOfFeesPaymentForStudent) && !string.IsNullOrEmpty(dataString.LastDateOfFeesPaymentForCollege))
                {
                    ExamFeeHeadList = func.getExamFeeHeadListForFacultyExamMap(dataString.StartDateOfExam, dataString.LastDateOfFeesPaymentForStudent, dataString.LastDateOfFeesPaymentForCollege);
                }

                return Return.returnHttp("200", ExamFeeHeadList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        /// ExamFeeHeadIsActive and ExamFeeHeadIsInactive Created by Jaydev on 25-11-21

        [HttpPost]
        public HttpResponseMessage ExamFeeHeadIsActive(ExamFeeHead dataString)
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

                SqlCommand cmd = new SqlCommand("ExamFeeHeadActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "ExamFeeHeadIsActive");
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
        public HttpResponseMessage ExamFeeHeadIsInactive(ExamFeeHead dataString)
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

                SqlCommand cmd = new SqlCommand("ExamFeeHeadActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "ExamFeeHeadIsInactive");
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