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
    public class ApplicationPreferenceReportController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);     
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();
        #region AcademicYearId
        [HttpPost]
        public HttpResponseMessage IncAcademicYearGet()
        {
            try
            {

                SqlCommand cmd = new SqlCommand();


                SqlDataAdapter da = new SqlDataAdapter("IncAcademicYearGet", con);

                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<AcademicYear> ObjAYList = new List<AcademicYear>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AcademicYear ObjAY = new AcademicYear();
                        ObjAY.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjAY.AcademicYearCode = Convert.ToString(dt.Rows[i]["AcademicYearCode"]);
                        ObjAYList.Add(ObjAY);
                    }
                    return Return.returnHttp("200", ObjAYList, null);
                }
                else
                {
                    return Return.returnHttp("201", "No Record Found", null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region PreProgrammeInstanceGetByFacIdAndYearId
        [HttpPost]
        public HttpResponseMessage IncProgramInstancePartTermGetbyFacId(IncProgInstancePartTerm ObjInc)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("IncProgrammeInstancePartTermGetByInstituteId", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@InstituteId", ObjInc.InstituteId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<IncProgInstancePartTerm> ObjLstProgInst = new List<IncProgInstancePartTerm>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IncProgInstancePartTerm ObjProgInst = new IncProgInstancePartTerm();
                        ObjProgInst.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjProgInst.IncProgramInstancePartTermName = Convert.ToString(dt.Rows[i]["InstancePartTermName"]);
                       





                        ObjLstProgInst.Add(ObjProgInst);

                    }
                    return Return.returnHttp("200", ObjLstProgInst, null);
                }
                else
                {
                    return Return.returnHttp("201", "No Record Found", null);
                }

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        [HttpPost]
        public HttpResponseMessage ApplicationInstitutePreferenceReport(ApplicationInstitutePreferenceReport ObjInstitute)
        {
            ApplicationInstitutePreferenceReport modelobj = new ApplicationInstitutePreferenceReport();
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                //String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                //string resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
               
               
             
                modelobj.InstituteId = ObjInstitute.InstituteId;            
                modelobj.IncProgInstId = ObjInstitute.IncProgInstId;
                SqlCommand cmd = new SqlCommand("ApplicationInstitutePreferenceReport", con);
                cmd.CommandType = CommandType.StoredProcedure;             
                cmd.Parameters.AddWithValue("@InstituteId", modelobj.InstituteId);               
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", modelobj.IncProgInstId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<ApplicationInstitutePreferenceReport> Applist = new List<ApplicationInstitutePreferenceReport>();
                foreach (DataRow DR in dt.Rows)
                {
                    ApplicationInstitutePreferenceReport AppObj = new ApplicationInstitutePreferenceReport();
                    AppObj.ApplicationNo = DR["ApplicationNo"].ToString();
                    AppObj.InstituteName = DR["InstituteName"].ToString();
                    AppObj.ProgrammeName = DR["ProgrammeName"].ToString();
                    AppObj.BranchName = DR["BranchName"].ToString();
                    AppObj.InstancePartTermName = DR["InstancePartTermName"].ToString();
                    AppObj.PreferenceNo = Convert.ToInt32(DR["PreferenceNo"].ToString());
                    AppObj.IsDeleted = Convert.ToBoolean(DR["IsDeleted"]);
                    AppObj.IsActive = Convert.ToBoolean(DR["IsActive"]);
                    AppObj.PreferenceGivenOn = (Convert.ToString(DR["PreferenceGivenOn"].ToString()));

                    Applist.Add(AppObj);
                }
                return Return.returnHttp("200", Applist, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }

       
    }
}
