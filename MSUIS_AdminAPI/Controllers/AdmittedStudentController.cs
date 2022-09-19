using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class AdmittedStudentController : ApiController
    {

        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();


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


        #region AdmittedStudentGet
        [HttpPost]
        public HttpResponseMessage AdmittedStudentGet(AdmittedStudent ASAF)
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

                    List<AdmittedStudent> alist = new List<AdmittedStudent>();
                    Int32 count = 0;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ASAF.ProgrammeInstancePartTermId);
                    cmd.CommandText = "AdmittedStudentByAcademicsFaculty";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    foreach (DataRow DR in dt.Rows)
                    {
                        AdmittedStudent a = new AdmittedStudent();

                        a.StudentAdmissionId = Convert.ToInt64(DR["StudentAdmissionId"].ToString());
                        a.PRN = Convert.ToInt64(DR["PRN"].ToString());
                        a.FacultyName = DR["FacultyName"].ToString();
                        a.InstancePartTermName = DR["InstancePartTermName"].ToString();
                        a.NameAsPerMarksheet = DR["NameAsPerMarksheet"].ToString();
                        a.AcademicYearCode = DR["AcademicYearCode"].ToString();
                        a.FeeCategoryName = DR["FeeCategoryName"].ToString();
                        a.InstituteName = DR["InstituteName"].ToString();
                        a.BranchName = DR["BranchName"].ToString();
                        a.ProgrammeName = DR["ProgrammeName"].ToString();
                        a.EligibilityByAcademics = DR["EligibilityByAcademics"].ToString();
                        a.AdminRemarkByAcademics = DR["AdminRemarkByAcademics"].ToString();
                        a.EligibilityByFaculty = DR["EligibilityByFaculty"].ToString();
                        a.AdminRemarkByFaculty = DR["AdminRemarkByFaculty"].ToString();
                        a.AdmissionStatus = DR["AdmissionStatus"].ToString();
                        a.IsDeleted = Convert.ToBoolean(DR["IsDeleted"].ToString());
                        a.Gender = (Convert.ToString(DR["Gender"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["Gender"]);
                        a.IsPhysicallyChanllenged = (Convert.ToString(DR["IsPhysicallyChanllenged"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["IsPhysicallyChanllenged"]);
                        a.ApplicationReservationName = (Convert.ToString(DR["ApplicationReservationName"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["ApplicationReservationName"]);
                        a.SocialCategoryName = (Convert.ToString(DR["SocialCategoryName"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["SocialCategoryName"]);
                        a.CommitteeName = (Convert.ToString(DR["CommitteeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["CommitteeName"]);



                        count = count + 1;
                        a.IndexId = count;


                        alist.Add(a);
                    }
                    return Return.returnHttp("200", alist, null);
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
    }
}