using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;
using Newtonsoft.Json.Linq;

namespace MSUISApi.Controllers
{
    public class PendingApprovedByAcademicController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlCommand cmd = new SqlCommand();

        #region NotApprovedByAcademicGet
        [HttpPost]
        public HttpResponseMessage PendingApprovedByAcademicGet(PendingApprovedByAcademic AFR)
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

                //Int32 InstituteId = Convert.ToInt32(RSR.InstituteId);

                SqlCommand Cmd = new SqlCommand("PendingApprovedByAcademic", Con);
                Cmd.CommandType = CommandType.StoredProcedure;


                Cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", AFR.ProgrammeInstancePartTermId);

                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                Int32 count = 0;
                List<PendingApprovedByAcademic> ObjLstAFR = new List<PendingApprovedByAcademic>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        PendingApprovedByAcademic ObjAFR = new PendingApprovedByAcademic();


                        ObjAFR.ApplicationId = (Convert.ToString(Dt.Rows[i]["ApplicationId"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ApplicationId"]);

                        ObjAFR.ApplicantUserName = (Convert.ToString(Dt.Rows[i]["ApplicantUserName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ApplicantUserName"]);

                        ObjAFR.ProgrammeInstancePartTermId = (((Dt.Rows[i]["ProgrammeInstancePartTermId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["ProgrammeInstancePartTermId"]);

                        ObjAFR.NameAsPerMarksheet = (Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"]);

                        ObjAFR.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyName"]);

                        ObjAFR.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["BranchName"]);

                        ObjAFR.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);

                        ObjAFR.InstancePartTermName = (Convert.ToString(Dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);

                        //ObjAFR.InstituteName = (Convert.ToString(Dt.Rows[i]["InstituteName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstituteName"]);

                        //ObjAFR.EligibilityByAcademics = (Convert.ToString(Dt.Rows[i]["EligibilityByAcademics"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["EligibilityByAcademics"]);
                        ObjAFR.Gender = (Convert.ToString(Dt.Rows[i]["Gender"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["Gender"]);
                        ObjAFR.IsPhysicallyChanllenged = (Convert.ToString(Dt.Rows[i]["IsPhysicallyChanllenged"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["IsPhysicallyChanllenged"]);
                        ObjAFR.ApplicationReservationName = (Convert.ToString(Dt.Rows[i]["ApplicationReservationName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ApplicationReservationName"]);
                        ObjAFR.SocialCategoryName = (Convert.ToString(Dt.Rows[i]["SocialCategoryName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["SocialCategoryName"]);
                        ObjAFR.CommitteeName = (Convert.ToString(Dt.Rows[i]["CommitteeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["CommitteeName"]);



                        count = count + 1;
                        ObjAFR.IndexId = count;

                        ObjLstAFR.Add(ObjAFR);



                    }
                    return Return.returnHttp("200", ObjLstAFR, null);

                }
                else
                {
                    return Return.returnHttp("200", "No Record Found", null);
                }

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
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
                cmd.Connection = Con;
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
                cmd.Connection = Con;
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