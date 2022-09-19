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
    public class IncStudentAcademicInformationByReportController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();

        #region IncStudentAcademicInformationByReportGet
        [HttpPost]
        public HttpResponseMessage IncStudentAcademicInformationByReportGet()
        {
            //String token = Request.Headers.GetValues("token").FirstOrDefault();
            //TokenOperation ac = new TokenOperation();

            //string res = ac.ValidateToken(token);

            //if (res == "0")
            //{
            //    return Return.returnHttp("0", null, null);
            //}
            try
            {
                SqlCommand cmd = new SqlCommand("IncStudentAcademicInformationByReport", con);

                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<IncStudentAcademicInformationByReport> ObjIncStudInfo = new List<IncStudentAcademicInformationByReport>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        IncStudentAcademicInformationByReport ObjISAIR = new IncStudentAcademicInformationByReport();
                        ObjISAIR.Id = Convert.ToInt64(dr["Id"].ToString());
                        ObjISAIR.PRN = Convert.ToInt64(dr["PRN"].ToString());
                        ObjISAIR.InstancePartTermName = dr["InstancePartTermName"].ToString();
                        ObjISAIR.ProgrammeName = dr["ProgrammeName"].ToString();
                        ObjISAIR.AcademicYearDisplayName = dr["AcademicYearDisplayName"].ToString();
                        ObjISAIR.InstituteName = dr["InstituteName"].ToString();
                        ObjISAIR.FacultyName = dr["FacultyName"].ToString();
                        ObjISAIR.FirstName = dr["FirstName"].ToString();
                        ObjISAIR.MiddleName = dr["MiddleName"].ToString();
                        ObjISAIR.LastName = dr["LastName"].ToString();
                        ObjISAIR.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjISAIR.ApprovedOnAcademics = Convert.ToDateTime(dr["ApprovedOnAcademics"]);
                        ObjISAIR.ApprovedOnAcademicsView = Convert.ToDateTime(dr["ApprovedOnAcademics"]).ToShortDateString();
                        ObjIncStudInfo.Add(ObjISAIR);
                    }
                }
                return Return.returnHttp("200", ObjIncStudInfo, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion
    }
}