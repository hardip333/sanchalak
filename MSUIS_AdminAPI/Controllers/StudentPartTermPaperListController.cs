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
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class StudentPartTermPaperListController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region GetStudentPartTermPaperReport

        [HttpPost]
        public HttpResponseMessage GetStudentPartTermPaperReport()
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
                List<StudentPartTermPaper> SPTPdata = new List<StudentPartTermPaper>();
                SqlCommand cmd = new SqlCommand("StudentPartTermPaperGet", con);
                cmd.CommandType = CommandType.StoredProcedure;

               

                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        StudentPartTermPaper SPTP = new StudentPartTermPaper();
                        SPTP.PRN = (((Dt.Rows[i]["PRN"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["PRN"]);
                        SPTP.PaperCode = (Convert.ToString(Dt.Rows[i]["PaperCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["PaperCode"]);
                        SPTP.PaperName = (Convert.ToString(Dt.Rows[i]["PaperName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["PaperName"]);
                        SPTP.EmailId = (Convert.ToString(Dt.Rows[i]["EmailId"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["EmailId"]);
                        SPTP.MobileNo = (Convert.ToString(Dt.Rows[i]["MobileNo"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["MobileNo"]);
                        SPTP.FullName = (Convert.ToString(Dt.Rows[i]["FullName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FullName"]);
                        SPTP.InstancePartTermName = (Convert.ToString(Dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                        SPTP.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        SPTP.SeatNumber = (Convert.ToString(Dt.Rows[i]["SeatNumber"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["SeatNumber"]);

                        SPTP.Index = i + 1;


                        SPTPdata.Add(SPTP);
                    }
                }

                return Return.returnHttp("200", SPTPdata, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        

        #endregion
    }
}
