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
    public class CourseScheduleMapController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region newCode By Stored Procedure - Megha
        [HttpPost]
        public HttpResponseMessage CourseScheduleMapAdd(CourseScheduleMap dataString)
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
                Int64 UserId = Convert.ToInt64(res.ToString());
                //Int64 UserId = 1;

                //string DisplayName = Convert.ToString(ObjExamMaster.DisplayName);

                int ProgrammePartTermId = Convert.ToInt32(dataString.ProgrammePartTermId);
                int FacultyExamMapId = Convert.ToInt32(dataString.FacultyExamMapId);
                bool? FresherEligible = Convert.ToBoolean(dataString.FresherEligible);
                bool? FresherProvisionalEligible = Convert.ToBoolean(dataString.FresherProvisionalEligible);
                bool? RepeaterEligible = Convert.ToBoolean(dataString.RepeaterEligible);
                bool? RepeaterProvisonalEligible = Convert.ToBoolean(dataString.RepeaterProvisionalEligible);
                bool? NeverAppearedEligible = Convert.ToBoolean(dataString.NeverAppearedEligible);
                bool? NeverAppearedProvisionalEligible = Convert.ToBoolean(dataString.NeverAppearedProvisionalEligible);
                bool? IsScheduledWithoutExamForm = Convert.ToBoolean(dataString.IsScheduledWithOutExamForm);
                int MaxTLMAMATCount = Convert.ToInt32(dataString.MaxTLMAMATCount);

                // if (!string.IsNullOrEmpty(dataString.DeamedInward.ToString()))
                bool? DeamedInward = Convert.ToBoolean(dataString.DeamedInward);

                DateTime ExamFeeStartDate = Convert.ToDateTime(dataString.ExamFeeStartDate);
                DateTime ExamFeeEndDateForStudent = Convert.ToDateTime(dataString.ExamFeeEndDateForStudent);
                DateTime ExamFeeEndDateforFaculty = Convert.ToDateTime(dataString.ExamFeeEndDateforFaculty);

                if (Convert.ToDateTime(dataString.ExamFeeStartDate) != null)
                {
                    DateTime dtAdmFeeStartDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(Convert.ToDateTime(dataString.ExamFeeStartDate)), out dtAdmFeeStartDate);
                    if (ChkDt)
                    {
                        dataString.ExamFeeStartDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(Convert.ToDateTime(dataString.ExamFeeStartDate)).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    }
                    else
                    {
                        dataString.ExamFeeStartDate = null;
                    }
                }
                else
                {
                    dataString.ExamFeeStartDate = null;
                }

                if (Convert.ToDateTime(dataString.ExamFeeEndDateForStudent) != null)
                {
                    DateTime dtAdmFeeStartDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(Convert.ToDateTime(dataString.ExamFeeEndDateForStudent)), out dtAdmFeeStartDate);
                    if (ChkDt)
                    {
                        dataString.ExamFeeEndDateForStudent = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(Convert.ToDateTime(dataString.ExamFeeEndDateForStudent)).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    }
                    else
                    {
                        dataString.ExamFeeEndDateForStudent = null;
                    }
                }
                else
                {
                    dataString.ExamFeeEndDateForStudent = null;
                }
                if (Convert.ToDateTime(dataString.ExamFeeEndDateforFaculty) != null)
                {
                    DateTime dtAdmFeeStartDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(Convert.ToDateTime(dataString.ExamFeeEndDateforFaculty)), out dtAdmFeeStartDate);
                    if (ChkDt)
                    {
                        dataString.ExamFeeEndDateforFaculty = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(Convert.ToDateTime(dataString.ExamFeeEndDateforFaculty)).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    }
                    else
                    {
                        dataString.ExamFeeEndDateforFaculty = null;
                    }
                }
                else
                {
                    dataString.ExamFeeEndDateforFaculty = null;
                }



                SqlCommand cmd = new SqlCommand("CourseScheduleMapAdd", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammePartTermId", ProgrammePartTermId);
                cmd.Parameters.AddWithValue("@FacultyExamMapId", FacultyExamMapId);
                cmd.Parameters.AddWithValue("@FresherEligible", FresherEligible);
                cmd.Parameters.AddWithValue("@FresherProvisionalEligible", FresherProvisionalEligible);
                cmd.Parameters.AddWithValue("@RepeaterEligible", RepeaterEligible);
                cmd.Parameters.AddWithValue("@RepeaterProvisonalEligible", RepeaterProvisonalEligible);
                cmd.Parameters.AddWithValue("@NeverAppearedEligible", NeverAppearedEligible);
                cmd.Parameters.AddWithValue("@NeverAppearedProvisionalEligible", NeverAppearedProvisionalEligible);
                cmd.Parameters.AddWithValue("@MaxTLMAMATCount", MaxTLMAMATCount);
                cmd.Parameters.AddWithValue("@IsScheduledWithoutExamForm", IsScheduledWithoutExamForm);
                cmd.Parameters.AddWithValue("@DeamedInward", DeamedInward);
                cmd.Parameters.AddWithValue("@ExamFeeStartDate", ExamFeeStartDate);
                cmd.Parameters.AddWithValue("@ExamFeeEndDateForStudent", ExamFeeEndDateForStudent);
                cmd.Parameters.AddWithValue("@ExamFeeEndDateforFaculty", ExamFeeEndDateforFaculty);
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

        [HttpPost]
        public HttpResponseMessage CourseScheduleMapListGet(CourseScheduleMap dataString)
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

                List<CourseScheduleMap> CourseScheduleMapList = new List<CourseScheduleMap>();
                BALCourseScheduleMap func = new BALCourseScheduleMap();
                //flag=1,IsDeleted=0,IsActive=null
                CourseScheduleMapList = func.getCourseScheduleMapList("admin", 0, null, dataString.FacultyExamMapId, dataString.ProgrammeInstancePartTermId, dataString.FacultyId);

                return Return.returnHttp("200", CourseScheduleMapList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage CourseScheduleMapListGetActive(CourseScheduleMap dataString)
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

                List<CourseScheduleMap> CourseScheduleMapList = new List<CourseScheduleMap>();
                BALCourseScheduleMap func = new BALCourseScheduleMap();
                //flag=1,IsDeleted=0,IsActive=1
                CourseScheduleMapList = func.getCourseScheduleMapList("admin", 0, 1, dataString.FacultyExamMapId, dataString.ProgrammeInstancePartTermId, dataString.FacultyId);

                return Return.returnHttp("200", CourseScheduleMapList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage PendingCourseScheduleMapList(CourseScheduleMap dataString)
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

                Int32 ExamMasterId = dataString.ExamMasterId;
                if (string.IsNullOrEmpty(ExamMasterId.ToString()))
                {
                    return Return.returnHttp("201", "Please select Exam Event, It's Mandatory.", null);
                }

                List<CourseScheduleMap> courseScheduleMapList = new List<CourseScheduleMap>();
                BALCourseScheduleMap func = new BALCourseScheduleMap();
                courseScheduleMapList = func.getPendingCourseScheduleMapList(dataString.ProgrammeId, ExamMasterId, dataString.FacultyExamMapId);

                return Return.returnHttp("200", courseScheduleMapList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        ///created CourseScheduleMapIsConfirmed and CourseScheduleMapIsUnconfirmed API
        // Code By Jaydev
        [HttpPost]
        public HttpResponseMessage CourseScheduleMapIsConfirmed(CourseScheduleMap dataString)
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
                //Int64 UserId = 1;//
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("CourseScheduleMapConfirmed", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "CourseScheduleMapIsConfirmed");
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
        public HttpResponseMessage CourseScheduleMapIsUnconfirmed(CourseScheduleMap dataString)
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
                //Int64 UserId = 1;//
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("CourseScheduleMapConfirmed", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "CourseScheduleMapIsUnconfirmed");
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


        /// CourseScheduleMapIsActive and CourseScheduleMapIsInactive Created by Jaydev on 25-11-21

        [HttpPost]
        public HttpResponseMessage CourseScheduleMapIsActive(CourseScheduleMap dataString)
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
                //Int64 UserId = 1;//
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("CourseScheduleMapActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "CourseScheduleMapIsActive");
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
        public HttpResponseMessage CourseScheduleMapIsInactive(CourseScheduleMap dataString)
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
                //Int64 UserId = 1;//
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("CourseScheduleMapActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "CourseScheduleMapIsInactive");
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
        public HttpResponseMessage CourseScheduleMapIsPublished(CourseScheduleMap dataString)
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
                //Int64 UserId = 1;//
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("CourseScheduleMapPublished", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "CourseScheduleMapIsPublished");
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
                    strMessage = "Your data has been Published Successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }


        [HttpPost]
        public HttpResponseMessage CourseScheduleMapIsUnpublished(CourseScheduleMap dataString)
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
                //Int64 UserId = 1;//
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("CourseScheduleMapPublished", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "CourseScheduleMapIsUnpublished");
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
                    strMessage = "Your data has been Unpublished Successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }


        // CourseScheduleMapEdit created by jaydev on 25-11-2021
        [HttpPost]
        public HttpResponseMessage CourseScheduleMapEdit(CourseScheduleMap dataString)
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
                //Int64 UserId = 1;//
                Int64 UserId = Convert.ToInt64(res.ToString());

                int ProgrammePartTermId = Convert.ToInt32(dataString.ProgrammePartTermId);
                int FacultyExamMapId = Convert.ToInt32(dataString.FacultyExamMapId);
                bool? FresherEligible = Convert.ToBoolean(dataString.FresherEligible);
                bool? FresherProvisionalEligible = Convert.ToBoolean(dataString.FresherProvisionalEligible);
                bool? RepeaterEligible = Convert.ToBoolean(dataString.RepeaterEligible);
                bool? RepeaterProvisonalEligible = Convert.ToBoolean(dataString.RepeaterProvisionalEligible);
                bool? NeverAppearedEligible = Convert.ToBoolean(dataString.NeverAppearedEligible);
                bool? NeverAppearedProvisionalEligible = Convert.ToBoolean(dataString.NeverAppearedProvisionalEligible);
                bool? IsScheduledWithoutExamForm = Convert.ToBoolean(dataString.IsScheduledWithOutExamForm);
                int MaxTLMAMATCount = Convert.ToInt32(dataString.MaxTLMAMATCount);
                bool? DeamedInward = Convert.ToBoolean(dataString.DeamedInward);
                DateTime ExamFeeStartDate = Convert.ToDateTime(dataString.ExamFeeStartDate);
                DateTime ExamFeeEndDateForStudent = Convert.ToDateTime(dataString.ExamFeeEndDateForStudent);
                DateTime ExamFeeEndDateforFaculty = Convert.ToDateTime(dataString.ExamFeeEndDateforFaculty);

                if (Convert.ToDateTime(dataString.ExamFeeStartDate) != null)
                {
                    DateTime dtAdmFeeStartDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(Convert.ToDateTime(dataString.ExamFeeStartDate)), out dtAdmFeeStartDate);
                    if (ChkDt)
                    {
                        dataString.ExamFeeStartDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(Convert.ToDateTime(dataString.ExamFeeStartDate)).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    }
                    else
                    {
                        dataString.ExamFeeStartDate = null;
                    }
                }
                else
                {
                    dataString.ExamFeeStartDate = null;
                }

                if (Convert.ToDateTime(dataString.ExamFeeEndDateForStudent) != null)
                {
                    DateTime dtAdmFeeStartDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(Convert.ToDateTime(dataString.ExamFeeEndDateForStudent)), out dtAdmFeeStartDate);
                    if (ChkDt)
                    {
                        dataString.ExamFeeEndDateForStudent = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(Convert.ToDateTime(dataString.ExamFeeEndDateForStudent)).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    }
                    else
                    {
                        dataString.ExamFeeEndDateForStudent = null;
                    }
                }
                else
                {
                    dataString.ExamFeeEndDateForStudent = null;
                }
                if (Convert.ToDateTime(dataString.ExamFeeEndDateforFaculty) != null)
                {
                    DateTime dtAdmFeeStartDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(Convert.ToDateTime(dataString.ExamFeeEndDateforFaculty)), out dtAdmFeeStartDate);
                    if (ChkDt)
                    {
                        dataString.ExamFeeEndDateforFaculty = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(Convert.ToDateTime(dataString.ExamFeeEndDateforFaculty)).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    }
                    else
                    {
                        dataString.ExamFeeEndDateforFaculty = null;
                    }
                }
                else
                {
                    dataString.ExamFeeEndDateforFaculty = null;
                }


                SqlCommand cmd = new SqlCommand("CourseScheduleMapEdit", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@ProgrammePartTermId", dataString.ProgrammePartTermId);
                cmd.Parameters.AddWithValue("@FacultyExamMapId", dataString.FacultyExamMapId);
                cmd.Parameters.AddWithValue("@FresherEligible", dataString.FresherEligible);
                cmd.Parameters.AddWithValue("@FresherProvisionalEligible", dataString.FresherProvisionalEligible);
                cmd.Parameters.AddWithValue("@RepeaterEligible", dataString.RepeaterEligible);
                cmd.Parameters.AddWithValue("@RepeaterProvisonalEligible", dataString.RepeaterProvisionalEligible);
                cmd.Parameters.AddWithValue("@NeverAppearedEligible", dataString.NeverAppearedEligible);
                cmd.Parameters.AddWithValue("@NeverAppearedProvisionalEligible", dataString.NeverAppearedProvisionalEligible);
                cmd.Parameters.AddWithValue("@MaxTLMAMATCount", dataString.MaxTLMAMATCount);
                cmd.Parameters.AddWithValue("@IsScheduledWithoutExamForm", dataString.IsScheduledWithOutExamForm);
                cmd.Parameters.AddWithValue("@DeamedInward", dataString.DeamedInward);
                cmd.Parameters.AddWithValue("@ExamFeeStartDate", dataString.ExamFeeStartDate);
                cmd.Parameters.AddWithValue("@ExamFeeEndDateForStudent", dataString.ExamFeeEndDateForStudent);
                cmd.Parameters.AddWithValue("@ExamFeeEndDateforFaculty", dataString.ExamFeeEndDateforFaculty);
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

        [HttpPost]
        public HttpResponseMessage CourseScheduleMapListAdd(List<CourseScheduleMap> dataString)
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

                if (dataString != null)
                {
                    if (!dataString.Any(n => n.ToBeAttached == true))
                    {
                        return Return.returnHttp("201", "Please Attach minimum one couse.", null);
                    }
                    //Int64 UserId = 1;//
                    Int64 UserId = Convert.ToInt64(res.ToString());
                    int? Count = dataString.Count;

                    for (int i = 0; i < Count; i++)
                    {
                        Int64 Id = dataString[i].Id, ProgrammePartTermId = dataString[i].ProgrammePartTermId, MaxTLMAMATCount = dataString[i].MaxTLMAMATCount, FacultyExamMapId = dataString[i].FacultyExamMapId;
                        Boolean FresherEligible = dataString[i].FresherEligible, FresherProvisionalEligible = dataString[i].FresherProvisionalEligible, RepeaterEligible = dataString[i].RepeaterEligible, RepeaterProvisionalEligible = dataString[i].RepeaterProvisionalEligible, NeverAppearedEligible = dataString[i].NeverAppearedEligible, NeverAppearedProvisionalEligible = dataString[i].NeverAppearedProvisionalEligible, IsScheduledWithOutExamForm = dataString[i].IsScheduledWithOutExamForm, ToBeAttached = dataString[i].ToBeAttached, DeamedInward = dataString[i].DeamedInward;
                        DateTime ExamFeeStartDate = Convert.ToDateTime(dataString[i].ExamFeeStartDate), ExamFeeEndDateForStudent = Convert.ToDateTime(dataString[i].ExamFeeEndDateForStudent), ExamFeeEndDateforFaculty = Convert.ToDateTime(dataString[i].ExamFeeEndDateforFaculty);

                        if (Convert.ToDateTime(dataString[i].ExamFeeStartDate) != null)
                        {
                            DateTime dtAdmFeeStartDate = new DateTime(1949, 1, 1);
                            bool ChkDt = DateTime.TryParse(Convert.ToString(Convert.ToDateTime(dataString[i].ExamFeeStartDate)), out dtAdmFeeStartDate);
                            if (ChkDt)
                            {
                                dataString[i].ExamFeeStartDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(Convert.ToDateTime(dataString[i].ExamFeeStartDate)).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                            }
                            else
                            {
                                dataString[i].ExamFeeStartDate = null;
                            }
                        }
                        else
                        {
                            dataString[i].ExamFeeStartDate = null;
                        }

                        if (Convert.ToDateTime(dataString[i].ExamFeeEndDateForStudent) != null)
                        {
                            DateTime dtAdmFeeStartDate = new DateTime(1949, 1, 1);
                            bool ChkDt = DateTime.TryParse(Convert.ToString(Convert.ToDateTime(dataString[i].ExamFeeEndDateForStudent)), out dtAdmFeeStartDate);
                            if (ChkDt)
                            {
                                dataString[i].ExamFeeEndDateForStudent = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(Convert.ToDateTime(dataString[i].ExamFeeEndDateForStudent)).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                            }
                            else
                            {
                                dataString[i].ExamFeeEndDateForStudent = null;
                            }
                        }
                        else
                        {
                            dataString[i].ExamFeeEndDateForStudent = null;
                        }
                        if (Convert.ToDateTime(dataString[i].ExamFeeEndDateforFaculty) != null)
                        {
                            DateTime dtAdmFeeStartDate = new DateTime(1949, 1, 1);
                            bool ChkDt = DateTime.TryParse(Convert.ToString(Convert.ToDateTime(dataString[i].ExamFeeEndDateforFaculty)), out dtAdmFeeStartDate);
                            if (ChkDt)
                            {
                                dataString[i].ExamFeeEndDateforFaculty = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(Convert.ToDateTime(dataString[i].ExamFeeEndDateforFaculty)).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                            }
                            else
                            {
                                dataString[i].ExamFeeEndDateforFaculty = null;
                            }

                        }
                        else
                        {
                            dataString[i].ExamFeeEndDateforFaculty = null;
                        }

                        if (ToBeAttached == true)
                        {
                            if (!(FresherEligible == true || FresherProvisionalEligible == true) && !(RepeaterEligible == true || RepeaterProvisionalEligible == true) && !(NeverAppearedEligible == true || NeverAppearedProvisionalEligible == true))
                            {
                                return Return.returnHttp("201", "Please select eligible or provisional eligible.", null);
                            }
                            SqlCommand cmd = new SqlCommand("CourseScheduleMapAdd", con);
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@ProgrammePartTermId", dataString[i].ProgrammePartTermId);
                            cmd.Parameters.AddWithValue("@FacultyExamMapId", dataString[i].FacultyExamMapId);
                            cmd.Parameters.AddWithValue("@FresherEligible", dataString[i].FresherEligible);
                            cmd.Parameters.AddWithValue("@FresherProvisionalEligible", dataString[i].FresherProvisionalEligible);
                            cmd.Parameters.AddWithValue("@RepeaterEligible", dataString[i].RepeaterEligible);
                            cmd.Parameters.AddWithValue("@RepeaterProvisonalEligible", dataString[i].RepeaterProvisionalEligible);
                            cmd.Parameters.AddWithValue("@NeverAppearedEligible", dataString[i].NeverAppearedEligible);
                            cmd.Parameters.AddWithValue("@NeverAppearedProvisionalEligible", dataString[i].NeverAppearedProvisionalEligible);
                            cmd.Parameters.AddWithValue("@MaxTLMAMATCount", dataString[i].MaxTLMAMATCount);
                            cmd.Parameters.AddWithValue("@IsScheduledWithoutExamForm", dataString[i].IsScheduledWithOutExamForm);
                            cmd.Parameters.AddWithValue("@DeamedInward", dataString[i].DeamedInward);
                            cmd.Parameters.AddWithValue("@ExamFeeStartDate", dataString[i].ExamFeeStartDate);
                            cmd.Parameters.AddWithValue("@ExamFeeEndDateForStudent", dataString[i].ExamFeeEndDateForStudent);
                            cmd.Parameters.AddWithValue("@ExamFeeEndDateforFaculty", dataString[i].ExamFeeEndDateforFaculty);
                            cmd.Parameters.AddWithValue("@UserId", UserId);
                            cmd.Parameters.AddWithValue("@UserTime", datetime);

                            cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                            cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                            con.Open();
                            cmd.ExecuteNonQuery();
                            string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                            con.Close();

                            if (!string.Equals(strMessage, "TRUE"))
                            {
                                //strMessage = "Your data has been added successfully.";
                                return Return.returnHttp("200", strMessage.ToString(), null);
                            }

                        }


                    }
                    return Return.returnHttp("200", " CourseScheduleMap saved successfully.", null);

                }
                else
                {
                    return Return.returnHttp("201", "List is Empty.", null);
                }


            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }


        [HttpPost]
        public HttpResponseMessage CourseScheduleMapDelete(CourseScheduleMap dataString)
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
                // Int64 UserId = 1;//Convert.ToInt64(res.ToString());
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("CourseScheduleMapDelete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);

                cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@MESSAGE"].Value);
                con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Course Schedule has been deleted Successfully.";
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