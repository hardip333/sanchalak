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
    public class CourseAttachVenueController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));


        #region newCode By Stored Procedure - Megha
        [HttpPost]
        public HttpResponseMessage getAttachedCourseVenueMapList(CourseAttachVenue dataString)
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
                List<CourseAttachVenue> courseAttachVenues = new List<CourseAttachVenue>();

                BALCourseAttachVenue func = new BALCourseAttachVenue();
                //flag=1,IsDeleted=0,IsActive=null
                courseAttachVenues = func.getCourseAttachVenueList("admin", 0, 1, dataString.ExamMasterId, dataString.FacultyExamMapId, dataString.CourseScheduleMapId, dataString.ExamVenueId, dataString.ExamCenterId, dataString.ProgrammePartTermId, UserId);

                return Return.returnHttp("200", courseAttachVenues, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage getPendingCourseAttachVenue(CourseAttachVenue dataString)
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

                if (string.IsNullOrEmpty(dataString.CourseScheduleMapId.ToString()))
                {
                    return Return.returnHttp("201", "Please select CourscheduleMap,it's mandatory.", null);
                }
                if(dataString.ExamCenterId==0)
                {
                    dataString.ExamCenterId = null;
                }
                List<CourseAttachVenue> element = new List<CourseAttachVenue>();

                BALCourseAttachVenue func = new BALCourseAttachVenue();
                //flag=1,IsDeleted=0,IsActive=null
                element = func.getPendingCourseVenueMapList(dataString.CourseScheduleMapId, dataString.ExamCenterId, dataString.FacultyExamMapId, dataString.ExamMasterId);

                return Return.returnHttp("200", element, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }


        [HttpPost]
        public HttpResponseMessage CourseAttachVenueAttached(CourseAttachDetachVenue dataString)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();

                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int64 UserId = Convert.ToInt64(res.ToString());
                int count = dataString.AttachedCourseVenueMapList.Count;
                List<CourseAttachVenue> AttachedCourseVenueMapList = dataString.AttachedCourseVenueMapList;
                int count1 = dataString.pendingCourseAttachVenueList.Count;
                List<CourseAttachVenue> pendingCourseAttachVenueList = dataString.pendingCourseAttachVenueList;

                string strMessage = "";
                string strMessage1 = "";
                if (count > 0)
                {
                    if (AttachedCourseVenueMapList.Any(n => n.Capacity == null))
                    {
                        return Return.returnHttp("201", "Please enter capacity.", null);
                    }

                    for (int i = 0; i < count; i++)
                    {
                        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), INDIAN_ZONE);
                        Int32 Id = Convert.ToInt32(AttachedCourseVenueMapList[i].Id);
                        //Int64 UserId = 1;

                        SqlCommand cmd = new SqlCommand("CourseAttachVenueAttached", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ExamMasterId", AttachedCourseVenueMapList[i].ExamMasterId);
                        cmd.Parameters.AddWithValue("@FacultyExamMapId", AttachedCourseVenueMapList[i].FacultyExamMapId);
                        cmd.Parameters.AddWithValue("@CourseScheduleMapId", AttachedCourseVenueMapList[i].CourseScheduleMapId);
                        cmd.Parameters.AddWithValue("@ExamVenueId", AttachedCourseVenueMapList[i].ExamVenueId);
                        cmd.Parameters.AddWithValue("@Capacity", AttachedCourseVenueMapList[i].Capacity);
                        cmd.Parameters.AddWithValue("@Sequence", AttachedCourseVenueMapList[i].Sequence);
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@UserTime", datetime);


                        cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                        cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                        con.Open();
                        cmd.ExecuteNonQuery();
                        strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                        con.Close();

                        if (string.Equals(strMessage, "TRUE"))
                        {
                            strMessage = "Your data has been Attached/Detached successfully.";
                        }
                        else { return Return.returnHttp("201", strMessage, null); }

                      
                    }
                    //return Return.returnHttp("200", strMessage, null);
                }
                
                if (count1 > 0)
                {
                    for (int i = 0; i < count1; i++)
                    {
                        SqlCommand cmd = new SqlCommand("CourseAttachVenueDetach", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", pendingCourseAttachVenueList[i].Id);
                        //cmd.Parameters.AddWithValue("@FacultyExamMapId", dataString[i].FacultyExamMapId);
                        //cmd.Parameters.AddWithValue("@CourseScheduleMapId", dataString[i].CourseScheduleMapId);
                        //cmd.Parameters.AddWithValue("@ExamVenueId", dataString[i].ExamVenueId);
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@UserTime", datetime);
                        cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                        cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                        con.Open();
                        cmd.ExecuteNonQuery();
                        strMessage1= Convert.ToString(cmd.Parameters["@Message"].Value);
                        con.Close();
                        if (string.Equals(strMessage1, "TRUE"))
                        {
                            strMessage1 = "Your data has been Attached/Detached successfully.";
                        }
                        else { return Return.returnHttp("201", strMessage1, null); }
                    }
                    //return Return.returnHttp("200", "Your data has been Detached successfully.", null);
                }
                return Return.returnHttp("200", "Your data has been Attached/Detached successfully.", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage CourseAttachVenueDetach(List<CourseAttachVenue> dataString)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();

                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int64 UserId = Convert.ToInt64(res.ToString());
                int count = dataString.Count;
                if (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        SqlCommand cmd = new SqlCommand("CourseAttachVenueDetach", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", dataString[i].Id);
                        //cmd.Parameters.AddWithValue("@FacultyExamMapId", dataString[i].FacultyExamMapId);
                        //cmd.Parameters.AddWithValue("@CourseScheduleMapId", dataString[i].CourseScheduleMapId);
                        //cmd.Parameters.AddWithValue("@ExamVenueId", dataString[i].ExamVenueId);
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
                            strMessage = "Your data has been Detached successfully.";
                        }
                    }
                    return Return.returnHttp("200", "Course Venue Map  detach successfully.", null);
                }
                return Return.returnHttp("201", "Please select venue for detach.", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion

    }
}