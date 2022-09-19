using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using MSUISApi.BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class LaunchResultConfigurationController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();

        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region ResultConfigurationLaunchGet
        [HttpPost]
        public HttpResponseMessage ResultConfigurationLaunchGet(LaunchResultConfiguration ResConfig)
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
                //else if (resRoleToken == "0")
                //{
                //    return Return.returnHttp("0", null, null);
                //}

                Int64 FacultyId = Convert.ToInt64(ResConfig.FacultyId);
                Int64 AcademicYearId = Convert.ToInt64(ResConfig.AcademicYearId);

                SqlCommand Cmd = new SqlCommand("ResultConfigurationLaunchGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                Cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);

                Da.SelectCommand = Cmd;
                Da.Fill(Dt);

                Int32 count = 0;
                List<LaunchResultConfiguration> ObjLstResConfig = new List<LaunchResultConfiguration>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        LaunchResultConfiguration objResC = new LaunchResultConfiguration();

                        objResC.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objResC.ProgramInstanceId = Convert.ToInt32(Dt.Rows[i]["ProgramInstanceId"]);
                        objResC.ProgrammePartTermId = Convert.ToInt32(Dt.Rows[i]["ProgrammePartTermId"]);
                        objResC.InstancePartTermName = Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                        objResC.ResCalculation = (Convert.ToString(Dt.Rows[i]["ResCalculation"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["ResCalculation"]);
                        objResC.ResCalculationSts = (Convert.ToString(Dt.Rows[i]["ResCalculationSts"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["ResCalculationSts"]);
                        objResC.ResCourseEvalSystem = (Convert.ToString(Dt.Rows[i]["ResCourseEvalSystem"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["ResCourseEvalSystem"]);
                        objResC.ResCourseEvalSystemSts = (Convert.ToString(Dt.Rows[i]["ResCourseEvalSystemSts"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["ResCourseEvalSystemSts"]);
                        objResC.ResExemptionMarkConfig = (Convert.ToString(Dt.Rows[i]["ResExemptionMarkConfig"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["ResExemptionMarkConfig"]);
                        objResC.ResExemptionMarkConfigSts = (Convert.ToString(Dt.Rows[i]["ResExemptionMarkConfigSts"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["ResExemptionMarkConfigSts"]);
                        count = count + 1;
                        objResC.IndexId = count;

                        ObjLstResConfig.Add(objResC);
                    }
                }
                return Return.returnHttp("200", ObjLstResConfig, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region ResultConfigurationLaunch
        [HttpPost]
        public HttpResponseMessage ResultConfigurationLaunch(LaunchResultConfiguration ObjResLaunch)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                SqlCommand cmd = new SqlCommand("ResConfigurationLaunch", Con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgramInstanceId", ObjResLaunch.ProgramInstanceId);
                cmd.Parameters.AddWithValue("@ProgrammePartTermId", ObjResLaunch.ProgrammePartTermId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                Con.Close();
                if (strMessage.Equals("TRUE"))
                {
                    return Return.returnHttp("200", "Programme Launched successfully", null);
                }
                else
                {
                    return Return.returnHttp("201", strMessage.ToString(), null);
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
