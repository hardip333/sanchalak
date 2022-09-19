using MSUIS_TokenManager.App_Start;
using MSUISApi.BAL;
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
    public class EligibleStudentsReportController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();

        #region IncProgrammeInstancePartTermGetByFIdAIdGet
        [HttpPost]
        public HttpResponseMessage IncProgrammeInstancePartTermGetByFIdAIdGet(ApplicationList AL)
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

                cmd.Parameters.AddWithValue("@FacultyId", AL.FacultyId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AL.AcademicYearId);
                cmd.Parameters.AddWithValue("@InstituteId", AL.InstituteId);
                cmd.CommandText = "IncProgrammeInstancePartTermGetByFIdAId";
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
        public HttpResponseMessage ApplicationListGet(ApplicationList AL)
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

                cmd.Parameters.AddWithValue("@FacultyId", AL.FacultyId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AL.AcademicYearId);

                cmd.CommandText = "ApplicationListGetPT";
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

        #region EligibleStudentsReportGet
        [HttpPost]
        public HttpResponseMessage EligibleStudentsReportGet(EligibleStudentsReport ObjPESR)
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

                    List<EligibleStudentsReport> alist = new List<EligibleStudentsReport>();
                    Int32 count = 0;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ObjPESR.ProgrammeInstancePartTermId);
                    cmd.Parameters.AddWithValue("@InstituteId", ObjPESR.InstituteId);
                    cmd.CommandText = "EligibleStudentsReport";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    foreach (DataRow DR in dt.Rows)
                    {
                        EligibleStudentsReport p = new EligibleStudentsReport();

                        p.PRN = Convert.ToInt64(DR["PRN"]);
                        p.ApplicationId = Convert.ToString(DR["ApplicationId"]);
                        p.ProgrammeInstancePartTermId = Convert.ToInt64(DR["ProgrammeInstancePartTermId"]);
                        p.InstituteId = Convert.ToInt32(DR["InstituteId"]);
                        p.InstancePartTermName = (Convert.ToString(DR["InstancePartTermName"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["InstancePartTermName"]);
                        p.FullName = (Convert.ToString(DR["FullName"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["FullName"]);
                        p.InstituteName = (Convert.ToString(DR["InstituteName"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["InstituteName"]);
                        p.FacultyName = (Convert.ToString(DR["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["FacultyName"]);
                        p.AcademicYearCode = (Convert.ToString(DR["AcademicYearCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["AcademicYearCode"]);
                        p.FacultyRemarks = (Convert.ToString(DR["FacultyRemarks"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["FacultyRemarks"]);

                        p.AcademicsRemarks = (Convert.ToString(DR["AcademicsRemarks"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["AcademicsRemarks"]);
                        p.PRNGeneratedOn = (Convert.ToString(DR["PRNGeneratedOn"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["PRNGeneratedOn"]);
                        p.EligibilityByAcademics = (Convert.ToString(DR["EligibilityByAcademics"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["EligibilityByAcademics"]);
                        if (DR["EligibilityByAcademics"].ToString() == "Eligible")
                        {
                            p.EligibilityByAcademics = "Eligible";
                        }

                        p.ApprovedOnAcademics = (Convert.ToString(DR["ApprovedOnAcademics"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["ApprovedOnAcademics"]);

                        count = count + 1;
                        p.IndexId = count;


                        alist.Add(p);
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

        #region InstituteGetById
        [HttpPost]
        public HttpResponseMessage InstituteGetById(MstInstitute MF)
        {
            //MstFaculty modelobj = new MstFaculty(); 
            try
            {
                //modelobj.Id = MF.Id;
                SqlCommand cmd = new SqlCommand("MstIntGetById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InstituteId", MF.Id);
                sda.SelectCommand = cmd;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                List<MstInstitute> ObjLstF = new List<MstInstitute>();
                //dt.Rows.Add("0", "All Faculty");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstInstitute ObjF = new MstInstitute();
                        ObjF.Id = Convert.ToInt32(dr["Id"]);
                        ObjF.InstituteName = (dr["InstituteName"].ToString());

                        ObjLstF.Add(ObjF);
                    }

                }

                return Return.returnHttp("200", ObjLstF, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion


    }
}