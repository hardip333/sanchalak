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

namespace MSUISApi.Controllers
{
    public class AddonDetailsReportController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();

        #region AddonDetailsReportGet
        [HttpPost]
        public HttpResponseMessage AddonDetailsReportGet(AddonDetailsReport ADR)
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
                SqlCommand cmd = new SqlCommand("AddonDetailsReport", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ADR.ProgrammeInstancePartTermId);
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<AddonDetailsReport> ObjListADR = new List<AddonDetailsReport>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AddonDetailsReport ObjADR = new AddonDetailsReport();

                        ObjADR.Id = Convert.ToInt64(dr["Id"].ToString());
                        ObjADR.ApplicationId = Convert.ToInt64(dr["ApplicationId"].ToString());
                        ObjADR.ProgrammeInstancePartTermId = Convert.ToInt64(dr["ProgrammeInstancePartTermId"].ToString());
                        ObjADR.ApplicantRegistrationId = Convert.ToInt64(dr["ApplicantRegistrationId"].ToString());
                        ObjADR.TitleName = dr["TitleName"].ToString();
                        ObjADR.AddOnValue = dr["AddOnValue"].ToString();
                        ObjADR.FacultyName = dr["FacultyName"].ToString();
                        ObjADR.ProgrammeName = dr["ProgrammeName"].ToString();
                        ObjADR.InstancePartTermName = dr["InstancePartTermName"].ToString();
                        ObjADR.BranchName = dr["BranchName"].ToString();
                        ObjADR.FirstName = dr["FirstName"].ToString();
                        ObjADR.MiddleName = dr["MiddleName"].ToString();
                        ObjADR.LastName = dr["LastName"].ToString();
                        //ObjADR.ApprovedOnFaculty = Convert.ToDateTime(dr["ApprovedOnFaculty"].ToString());
                        //ObjADR.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjListADR.Add(ObjADR);
                    }
                }
                return Return.returnHttp("200", ObjListADR, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion


        #region ProgrammeInstancePartTermGet
        [HttpPost]
        public HttpResponseMessage IncProgrammeInstancePartTermGet()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("IncProgrammeInstancePartTermGet", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@FacultyId", PIPT.FacultyId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<ProgrammeInstancePartTerm> ObjLstProgInstPartTerm = new List<ProgrammeInstancePartTerm>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePartTerm ObjProgInstPartTerm = new ProgrammeInstancePartTerm();
                        ObjProgInstPartTerm.Id = Convert.ToInt64(dt.Rows[i]["Id"]);

                        ObjProgInstPartTerm.ProgrammePartTermName = Convert.ToString(dt.Rows[i]["ProgrammePartTermName"]);

                        ObjLstProgInstPartTerm.Add(ObjProgInstPartTerm);

                    }
                    return Return.returnHttp("200", ObjLstProgInstPartTerm, null);
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

    }
}