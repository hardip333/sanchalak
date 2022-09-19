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
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class PaperWiseInwardController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        #region PaperWiseInwardReportGetByExamEventId
        [HttpPost]
        public HttpResponseMessage PaperWiseInwardReportGetByExamMasterId(PaperWiseInward PWI)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
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
                Int32 Count = 0;
                Int64 ExamMasterId = Convert.ToInt64(PWI.ExamMasterId);
                Int64 FacultyExamMapId = Convert.ToInt64(PWI.FacultyExamMapId);
                Int64 ProgrammeId = Convert.ToInt64(PWI.ProgrammeId);
                Int64 BranchId = Convert.ToInt64(PWI.BranchId);
                Int64 ProgrammePartTermId = Convert.ToInt64(PWI.ProgrammePartTermId);


                SqlCommand cmd = new SqlCommand("PaperWiseInwardReportGetById", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                cmd.Parameters.AddWithValue("@FacultyExamMapId", FacultyExamMapId);
                cmd.Parameters.AddWithValue("@ProgrammeId", ProgrammeId);
                cmd.Parameters.AddWithValue("@BranchId", BranchId);
                cmd.Parameters.AddWithValue("@ProgrammePartTermId", ProgrammePartTermId);

                sda.SelectCommand = cmd;
                sda.Fill(Dt);

                List<PaperWiseInward> ObjLstPWI = new List<PaperWiseInward>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        PaperWiseInward objPWI = new PaperWiseInward();
                        objPWI.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objPWI.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        objPWI.ProgrammeCode = (Convert.ToString(Dt.Rows[i]["ProgrammeCode"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["ProgrammeCode"]);
                        objPWI.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["BranchName"]);
                        objPWI.PartTermName = (Convert.ToString(Dt.Rows[i]["PartTermName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["PartTermName"]);
                        objPWI.ProgrammeModeName = (Convert.ToString(Dt.Rows[i]["ProgrammeModeName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["ProgrammeModeName"]);
                        objPWI.StudentCount = (Convert.ToString(Dt.Rows[i]["StudentCount"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["StudentCount"]);
                        objPWI.PaperName = (Convert.ToString(Dt.Rows[i]["PaperName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["PaperName"]);
                        objPWI.PaperCode = (Convert.ToString(Dt.Rows[i]["PaperCode"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["PaperCode"]);
                        objPWI.ExamDate = (Convert.ToString(Dt.Rows[i]["ExamDate"])).IsEmpty() ? "-" : string.Format("{0: dd-MM-yyyy}", (Dt.Rows[i]["ExamDate"]));
                        objPWI.Duration = (Convert.ToString(Dt.Rows[i]["Duration"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["Duration"]);
                      
                        objPWI.TeachingLearningMethodName = (Convert.ToString(Dt.Rows[i]["TeachingLearningMethodName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["TeachingLearningMethodName"]);
                        objPWI.AssessmentMethodName = (Convert.ToString(Dt.Rows[i]["AssessmentMethodName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["AssessmentMethodName"]);
                        objPWI.AssessmentType = (Convert.ToString(Dt.Rows[i]["AssessmentType"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["AssessmentType"]);
                       
                        Count = Count + 1;
                        objPWI.IndexId = Count;
                        ObjLstPWI.Add(objPWI);
                    }
                }
                else
                {
                    return Return.returnHttp("201", "No Record Found", null);
                }
                return Return.returnHttp("200", ObjLstPWI, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion
    }
}
