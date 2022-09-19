using MSUIS_TokenManager.App_Start;
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
    public class PRNNotGeneratedController : ApiController
    {

        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();

        #region PRNNotGeneratedGet
        [HttpPost]
        public HttpResponseMessage PRNNotGeneratedGet(PRNNotGenerated NPGR)
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

                try
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable();

                    List<PRNNotGenerated> plist = new List<PRNNotGenerated>();
                    Int32 count = 0;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", NPGR.ProgrammeInstancePartTermId);
                    cmd.CommandText = "NotPRNGeneratedReportGetByProgInstPartTermId";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    foreach (DataRow DR in dt.Rows)
                    {
                        PRNNotGenerated p = new PRNNotGenerated();
                        p.ApplicationId = DR["ApplicationId"].ToString();                      
                        p.FacultyName = DR["FacultyName"].ToString();
                        p.InstancePartName = DR["InstancePartName"].ToString();
                        p.InstancePartTermName = DR["InstancePartTermName"].ToString();
                        p.ApplicantUserName = DR["ApplicantUserName"].ToString();
                        p.AdmittedCourse = DR["AdmittedCourse"].ToString();
                        p.ProgrammeName = DR["ProgrammeName"].ToString();
                        p.BranchName = DR["BranchName"].ToString();
                        p.PRNGenerated = DR["PRNGenerated"].ToString();
                        p.Gender = DR["Gender"].ToString();
                        p.IsPhysicallyChanllenged = DR["IsPhysicallyChanllenged"].ToString();
                        p.ApplicationReservationName = DR["ApplicationReservationName"].ToString();
                        p.SocialCategoryName = DR["SocialCategoryName"].ToString();
                        p.CommitteeName = DR["CommitteeName"].ToString();

                        count = count + 1;
                        p.IndexId = count;


                        plist.Add(p);
                    }
                    return Return.returnHttp("200", plist, null);
                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message.ToString(), null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion


        #region IncProgInsPartTermListGetByInstituteId
        // This method is for getting InstancePartTerm By Institute Id for Academic Purpose
        [HttpPost]
        public HttpResponseMessage IncProgInsPartTermListGetByInstituteId(ApplicationList ObjAppList)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                List<ApplicationList> slist = new List<ApplicationList>();

                cmd.Parameters.AddWithValue("@InstituteId", ObjAppList.InstituteId);
                cmd.CommandText = "IncProgInstPartTermGetByFacultyId";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow DR in dt.Rows)
                {
                    ApplicationList s = new ApplicationList();

                    s.InstancePartTermId = Convert.ToInt32(DR["Id"].ToString());
                    s.InstancePartTermName = DR["InstancePartTermName"].ToString();

                    slist.Add(s);
                }
                return Return.returnHttp("200", slist, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion


        #region ApplicationListGet
        [HttpGet]
        public HttpResponseMessage ApplicationListGet()
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

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                List<ApplicationList> slist = new List<ApplicationList>();

                cmd.Parameters.AddWithValue("@FacultyId", facultyDepartIntituteId);
                cmd.CommandText = "ApplicationListGet";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow DR in dt.Rows)
                {
                    ApplicationList s = new ApplicationList();

                    s.InstancePartTermId = Convert.ToInt32(DR["Id"].ToString());
                    s.InstancePartTermName = DR["InstancePartTermName"].ToString();

                    slist.Add(s);
                }
                return Return.returnHttp("200", slist, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion
    }
}
