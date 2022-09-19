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
using System.Text;
using System.Web.Http;

namespace MSUISApi.Controllers
{
    public class FacultyExamMapController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region newCode By Stored Procedure - Megha
        [HttpPost]
        public HttpResponseMessage FacultyExamMapAdd(FacultyExamMap ObjFacultyExamMap)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            if (String.IsNullOrEmpty(ObjFacultyExamMap.DisplayName))
            {
                return Return.returnHttp("201", "Please enter display name", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjFacultyExamMap.ExamMasterId)))
            {
                return Return.returnHttp("201", "Please select Exam Master", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjFacultyExamMap.FacultyId)))
            {
                return Return.returnHttp("201", "Please select faculty", null);
            }
            else if (String.IsNullOrEmpty(ObjFacultyExamMap.ScheduleCode))
            {
                return Return.returnHttp("201", "Please enter schedule Code", null);
            }
            else
            {
                try
                {

                    if (!string.IsNullOrEmpty(ObjFacultyExamMap.StartDateOfExam) && !string.IsNullOrEmpty(ObjFacultyExamMap.LastDateOfFeesPaymentForCollege) && !string.IsNullOrEmpty(ObjFacultyExamMap.LastDateOfFeesPaymentForStudent))
                    {
                        DateTime StartDate = Convert.ToDateTime(ObjFacultyExamMap.StartDateOfExam);
                        DateTime EndDate = Convert.ToDateTime(ObjFacultyExamMap.EndDateOfExam);
                        // DateTime ProbableDate = Convert.ToDateTime(ObjFacultyExamMap.ProbableDateOfResult);
                        DateTime LastDateOfFeesForStudent = Convert.ToDateTime(ObjFacultyExamMap.LastDateOfFeesPaymentForStudent);
                        DateTime LastDateOfFeesForCollege = Convert.ToDateTime(ObjFacultyExamMap.LastDateOfFeesPaymentForCollege);

                        if (EndDate.Date < StartDate.Date)
                        {
                            return Return.returnHttp("201", "Please Enter End Date Greater Than Start Date.", null);
                        }
                        if (StartDate.Date < LastDateOfFeesForStudent.Date)
                        {
                            return Return.returnHttp("201", "Start Date should be grater than Late Fee without for student.", null);
                        }
                        if (StartDate.Date < LastDateOfFeesForCollege.Date)
                        {
                            return Return.returnHttp("201", "Start Date should be grater than Late Fee without for College.", null);
                        }
                        if (EndDate.Date < LastDateOfFeesForStudent.Date)
                        {
                            return Return.returnHttp("201", "End Date should be grater than Late Fee without for student.", null);
                        }
                        if (EndDate.Date < LastDateOfFeesForCollege.Date)
                        {
                            return Return.returnHttp("201", "End Date should be grater than Late Fee without for College.", null);
                        }
                        //if (ProbableDate.Date < LastDateOfFeesForStudent.Date)
                        //{
                        //    return Return.returnHttp("201", "Probable Date should be grater than Late Fee without for student.", null);
                        //}
                        //if (ProbableDate.Date < LastDateOfFeesForCollege.Date)
                        //{
                        //    return Return.returnHttp("201", "Probable Date should be grater than Late Fee without for College.", null);
                        //}
                        //if (ProbableDate.Date < StartDate.Date || ProbableDate.Date > EndDate.Date)
                        //{
                        //    return Return.returnHttp("201", "Please Enter Probable Date between Start Date and End Date.", null);
                        //}

                        Int64 UserId = Convert.ToInt64(res.ToString());
                        // Int64 UserId = 1;

                        string DisplayName = ObjFacultyExamMap.DisplayName;
                        string ScheduleCode = ObjFacultyExamMap.ScheduleCode;
                        int ExamMasterId = Convert.ToInt16(ObjFacultyExamMap.ExamMasterId.ToString());
                        int FacultyId = Convert.ToInt16(ObjFacultyExamMap.FacultyId.ToString());

                        string strInstituteId = "null";
                        if (!string.IsNullOrEmpty(ObjFacultyExamMap.InstituteId.ToString()))
                        {
                            strInstituteId = ObjFacultyExamMap.InstituteId.ToString();
                        }

                        SqlCommand cmd = new SqlCommand("FacultyExamMapAdd", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                        cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                        cmd.Parameters.AddWithValue("@InstituteId", strInstituteId);
                        cmd.Parameters.AddWithValue("@DisplayName", DisplayName);
                        cmd.Parameters.AddWithValue("@ScheduleCode", ScheduleCode);
                        cmd.Parameters.AddWithValue("@LastDateOfFeesPaymentForStudent", LastDateOfFeesForStudent);
                        cmd.Parameters.AddWithValue("@LastDateOfFeesPaymentForCollege", LastDateOfFeesForCollege);
                        cmd.Parameters.AddWithValue("@StartDateOfExam", StartDate);
                        cmd.Parameters.AddWithValue("@EndDateOfExam", EndDate);
                        cmd.Parameters.AddWithValue("@ProbableDateOfResult", "");//ProbableDate

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
                    return Return.returnHttp("200", null, null);
                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message, null);
                }
            }

        }

        [HttpPost]
        public HttpResponseMessage FacultyExamMapEdit(FacultyExamMap ObjFacultyExamMap)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            if (String.IsNullOrWhiteSpace(ObjFacultyExamMap.DisplayName))
            {
                return Return.returnHttp("201", "Please enter display name", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjFacultyExamMap.ExamMasterId)))
            {
                return Return.returnHttp("201", "Please select Exam Master", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjFacultyExamMap.FacultyId)))
            {
                return Return.returnHttp("201", "Please select faculty", null);
            }
            else if (String.IsNullOrEmpty(ObjFacultyExamMap.ScheduleCode))
            {
                return Return.returnHttp("201", "Please enter schedule Code", null);
            }
            else
            {
                try
                {
                    string strMessage = string.Empty;
                    if (!string.IsNullOrEmpty(ObjFacultyExamMap.StartDateOfExam) && !string.IsNullOrEmpty(ObjFacultyExamMap.LastDateOfFeesPaymentForCollege) && !string.IsNullOrEmpty(ObjFacultyExamMap.LastDateOfFeesPaymentForStudent))
                    {
                        DateTime StartDate = Convert.ToDateTime(ObjFacultyExamMap.StartDateOfExam);
                        DateTime EndDate = Convert.ToDateTime(ObjFacultyExamMap.EndDateOfExam);
                        // DateTime ProbableDate = Convert.ToDateTime(ObjFacultyExamMap.ProbableDateOfResult);
                        DateTime LastDateOfFeesForStudent = Convert.ToDateTime(ObjFacultyExamMap.LastDateOfFeesPaymentForStudent);
                        DateTime LastDateOfFeesForCollege = Convert.ToDateTime(ObjFacultyExamMap.LastDateOfFeesPaymentForCollege);

                        if (EndDate.Date < StartDate.Date)
                        {
                            return Return.returnHttp("201", "Please Enter End Date Greater Than Start Date.", null);
                        }
                        if (StartDate.Date < LastDateOfFeesForStudent.Date)
                        {
                            return Return.returnHttp("201", "Start Date should be grater than Late Fee without for student.", null);
                        }
                        if (StartDate.Date < LastDateOfFeesForCollege.Date)
                        {
                            return Return.returnHttp("201", "Start Date should be grater than Late Fee without for College.", null);
                        }
                        if (EndDate.Date < LastDateOfFeesForStudent.Date)
                        {
                            return Return.returnHttp("201", "End Date should be grater than Late Fee without for student.", null);
                        }
                        if (EndDate.Date < LastDateOfFeesForCollege.Date)
                        {
                            return Return.returnHttp("201", "End Date should be grater than Late Fee without for College.", null);
                        }
                        //if (ProbableDate.Date < LastDateOfFeesForStudent.Date)
                        //{
                        //    return Return.returnHttp("201", "Probable Date should be grater than Late Fee without for student.", null);
                        //}
                        //if (ProbableDate.Date < LastDateOfFeesForCollege.Date)
                        //{
                        //    return Return.returnHttp("201", "Probable Date should be grater than Late Fee without for College.", null);
                        //}
                        //if (ProbableDate.Date < StartDate.Date || ProbableDate.Date > EndDate.Date)
                        //{
                        //    return Return.returnHttp("201", "Please Enter Probable Date between Start Date and End Date.", null);
                        //}

                        Int64 UserId = Convert.ToInt64(res.ToString());
                        //Int64 UserId = 1;

                        string DisplayName = ObjFacultyExamMap.DisplayName;
                        string ScheduleCode = ObjFacultyExamMap.ScheduleCode;
                        int ExamMasterId = Convert.ToInt16(ObjFacultyExamMap.ExamMasterId.ToString());
                        int FacultyId = Convert.ToInt16(ObjFacultyExamMap.FacultyId.ToString());

                        string strInstituteId = "null";
                        if (!string.IsNullOrEmpty(ObjFacultyExamMap.InstituteId.ToString()))
                        {
                            strInstituteId = ObjFacultyExamMap.InstituteId.ToString();
                        }

                        SqlCommand cmd = new SqlCommand("FacultyExamMapEdit", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", ObjFacultyExamMap.Id);
                        cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                        cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                        cmd.Parameters.AddWithValue("@InstituteId", strInstituteId);
                        cmd.Parameters.AddWithValue("@DisplayName", DisplayName);
                        cmd.Parameters.AddWithValue("@ScheduleCode", ScheduleCode);
                        cmd.Parameters.AddWithValue("@LastDateOfFeesPaymentForStudent", LastDateOfFeesForStudent);
                        cmd.Parameters.AddWithValue("@LastDateOfFeesPaymentForCollege", LastDateOfFeesForCollege);
                        cmd.Parameters.AddWithValue("@StartDateOfExam", StartDate);
                        cmd.Parameters.AddWithValue("@EndDateOfExam", EndDate);
                        cmd.Parameters.AddWithValue("@ProbableDateOfResult", ""); //ProbableDate

                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@UserTime", datetime);


                        cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                        cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                        con.Open();
                        cmd.ExecuteNonQuery();
                        strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                        con.Close();

                        if (!string.Equals(strMessage, "TRUE"))
                        {
                            return Return.returnHttp("201", strMessage.ToString(), null);
                        }
                    }
                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Your data has been modified successfully.";
                    }
                    return Return.returnHttp("200", strMessage, null);
                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message, null);
                }
            }

        }

        [HttpPost]
        public HttpResponseMessage FacultyExamMapListGet(FacultyExamMap dataString)
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

                List<FacultyExamMap> facultyExamMapList = new List<FacultyExamMap>();
                BALFacultyExamMap func = new BALFacultyExamMap();
                //flag=1,IsDeleted=0,IsActive=1,ExamId,FacultyId

                facultyExamMapList = func.getFacultyExamMapList("admin", 0, null, dataString.ExamMasterId, dataString.FacultyId, null);

                return Return.returnHttp("200", facultyExamMapList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        [HttpPost]
        public HttpResponseMessage FacultyExamMapListGetActive(FacultyExamMap dataString)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                string resRoleToken = ac.ValidateRoleToken(userRoleToken);
                String typePrefix = Request.Headers.GetValues("typePrefix").FirstOrDefault();
                //Int32 FacultyId = Request.Headers.GetValues("typePrefix").FirstOrDefault();
                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int64 UserId = Convert.ToInt64(res.ToString());

                List<FacultyExamMap> facultyExamMapList = new List<FacultyExamMap>();
                BALFacultyExamMap func = new BALFacultyExamMap();
                //flag=1,IsDeleted=0,IsActive=1,ExamId,FacultyId
                //facultyExamMapList = func.getFacultyExamMapList("admin", 0, 1, dataString.ExamMasterId, dataString.FacultyId, null);
                facultyExamMapList = func.getFacultyExamMapList1(dataString.ExamMasterId, UserId);
                if (facultyExamMapList.Any())
                {
                return Return.returnHttp("200", facultyExamMapList, null);
                }
                else
                {
                    return Return.returnHttp("201", "No record found", null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        ///created FacultyExamMapIsConfirmed and FacultyExamMapIsUnconfirmed API
        // Code By Jaydev on 23-11-21
        [HttpPost]
        public HttpResponseMessage FacultyExamMapIsConfirmed(FacultyExamMap dataString)
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
                //Int64 UserId = 1;
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("FacultyExamMapConfirmed", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "FacultyExamMapIsConfirmed");
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
                    strMessage = "Your data has been Confirmed Successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }

        [HttpPost]
        public HttpResponseMessage FacultyExamMapIsUnconfirmed(FacultyExamMap dataString)
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

                SqlCommand cmd = new SqlCommand("FacultyExamMapConfirmed", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "FacultyExamMapIsUnconfirmed");
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
                    strMessage = "Your data has been Unconfirmed Successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }


        /// FacultyExamMapIsActive and FacultyExamMapIsInactive Created by Jaydev on 24-11-21

        [HttpPost]
        public HttpResponseMessage FacultyExamMapIsActive(FacultyExamMap dataString)
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

                SqlCommand cmd = new SqlCommand("FacultyExamMapActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "FacultyExamMapIsActive");
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
        public HttpResponseMessage FacultyExamMapIsInactive(FacultyExamMap dataString)
        {
            string token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            try
            {

                Int32 Id = (int)dataString.Id;
                //Int64 UserId = 1;
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("FacultyExamMapActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "FacultyExamMapIsInactive");
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

        //[HttpPost]
        //public HttpResponseMessage FacultyExamMapGetStatusbyId(FacultyExamMap dataString)
        //{
        //    try
        //    {
        //        //String token = Request.Headers.GetValues("token").FirstOrDefault();
        //        //String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
        //        //String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
        //        //TokenOperation ac = new TokenOperation();
        //        //string res = ac.ValidateToken(token);
        //        //string resRoleToken = ac.ValidateRoleToken(userRoleToken);

        //        //if (res == "0")
        //        //{
        //        //    return Return.returnHttp("0", null, null);
        //        //}
        //        //else if (resRoleToken == "0")
        //        //{
        //        //    return Return.returnHttp("0", null, null);
        //        //}
        //        //Int64 UserId = Convert.ToInt64(res.ToString());

        //        Int32 Id = Convert.ToInt32(dataString.Id);

        //        FacultyExamMap element = new FacultyExamMap();
        //        BALFacultyExamMap func = new BALFacultyExamMap();
        //        element = func.getFacultyExamMapStatusDetails(Id);

        //        return Return.returnHttp("200", element, null);
        //    }
        //    catch (Exception e)
        //    {
        //        return Return.returnHttp("201", e.Message, null);
        //    }

        //}

        //[HttpPost]
        //public HttpResponseMessage FacultyExamMapStatusGet(FacultyExamMap dataString)
        //{
        //    try
        //    {
        //        //String token = Request.Headers.GetValues("token").FirstOrDefault();
        //        //String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
        //        //String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
        //        //TokenOperation ac = new TokenOperation();
        //        //string res = ac.ValidateToken(token);
        //        //string resRoleToken = ac.ValidateRoleToken(userRoleToken);

        //        //if (res == "0")
        //        //{
        //        //    return Return.returnHttp("0", null, null);
        //        //}
        //        //else if (resRoleToken == "0")
        //        //{
        //        //    return Return.returnHttp("0", null, null);
        //        //}
        //        //Int64 UserId = Convert.ToInt64(res.ToString());

        //        List<FacultyExamMap> FacultyExamMapList = new List<FacultyExamMap>();
        //        BALFacultyExamMap func = new BALFacultyExamMap();
        //        //flag=1,IsDeleted=0,IsActive=null
        //        FacultyExamMapList = func.getFacultyExamMapStatus(Convert.ToInt32(dataString.ExamMasterId), 0, 1, "FacultyExamMapStatusGet");

        //        return Return.returnHttp("200", FacultyExamMapList, null);
        //    }
        //    catch (Exception e)
        //    {
        //        return Return.returnHttp("201", e.Message, null);
        //    }
        //}

        ///created FacultyExamMapIsPublished and FacultyExamMapIsUnpublished API
        // Code By Megha on 07-12-21
        [HttpPost]
        public HttpResponseMessage FacultyExamMapIsPublished(FacultyExamMap dataString)
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
                SqlCommand cmd = new SqlCommand("FacultyExamMapPublished", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "FacultyExamMapIsPublished");
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
                    strMessage = "Your data has been published successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }

        [HttpPost]
        public HttpResponseMessage FacultyExamMapIsUnPublished(FacultyExamMap dataString)
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

                SqlCommand cmd = new SqlCommand("FacultyExamMapPublished", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "FacultyExamMapIsUnpublished");
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
                    strMessage = "Your data has been unpublished successfully.";
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