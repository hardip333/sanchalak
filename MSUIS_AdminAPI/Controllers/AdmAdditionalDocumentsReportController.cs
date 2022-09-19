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
    public class AdmAdditionalDocumentsReportController : ApiController
    {
        
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            SqlDataAdapter sda = new SqlDataAdapter();

            #region AddonDetailsReportGet
            [HttpPost]
            public HttpResponseMessage AdmAdditionalDocumentsReportGet(AdmAdditionalDocumentsReport AADR)
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
                    SqlCommand cmd = new SqlCommand("AdmAdditionalDocumentsReport", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", AADR.ProgrammeInstancePartTermId);
                    cmd.Parameters.AddWithValue("@FacultyId", AADR.FacultyId);
                    DataTable dt = new DataTable();
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);

                    List<AdmAdditionalDocumentsReport> ObjListAADR = new List<AdmAdditionalDocumentsReport>();

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                        AdmAdditionalDocumentsReport ObjAADR = new AdmAdditionalDocumentsReport();

                        ObjAADR.Id = Convert.ToInt64(dr["Id"].ToString());
                        ObjAADR.AdmApplicationId = Convert.ToInt64(dr["AdmApplicationId"].ToString());
                        ObjAADR.ProgrammeInstancePartTermId = Convert.ToInt64(dr["ProgrammeInstancePartTermId"].ToString());
                        ObjAADR.ProgrammeName = dr["ProgrammeName"].ToString();
                        ObjAADR.BranchName = dr["BranchName"].ToString();
                        ObjAADR.FacultyName = dr["FacultyName"].ToString();
                        ObjAADR.InstancePartTermName = dr["InstancePartTermName"].ToString();
                        ObjAADR.DocFileName = dr["DocFileName"].ToString();
                        ObjAADR.IsApprovedByFacultys = dr["IsApprovedByFacultys"].ToString();
                        ObjAADR.IsApprovedByFaculty = Convert.ToBoolean(dr["IsApprovedByFaculty"].ToString());
                        //ObjAADR.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjListAADR.Add(ObjAADR);
                        }
                    }
                    return Return.returnHttp("200", ObjListAADR, null);
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