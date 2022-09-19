using MSUIS_TokenManager.App_Start;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.WebPages;

namespace MSUISApi.Models
{
    public class ExamScheduleReportController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        #region ExamEventGet
        [HttpPost]
        public HttpResponseMessage ExamEventGet()
        {
            try
            {

                SqlCommand Cmd = new SqlCommand("ExamEventGetForDropDown", con);
                Cmd.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = Cmd;
                sda.Fill(Dt);

                List<ExamEventMaster> ObjLstEEM = new List<ExamEventMaster>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ExamEventMaster objEEM = new ExamEventMaster();

                        objEEM.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objEEM.DisplayName = Convert.ToString(Dt.Rows[i]["DisplayName"]);
                        objEEM.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objEEM.IsDeleted = Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);

                        ObjLstEEM.Add(objEEM);
                    }
                }
                return Return.returnHttp("200", ObjLstEEM, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region ExamScheduleReportGetByExamEventId
        [HttpPost]
        public HttpResponseMessage ExamScheduleReportGetByExamMasterId(ExamScheduleReport ESR)
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
                Int64 ExamMasterId = Convert.ToInt64(ESR.ExamMasterId);


                SqlCommand cmd = new SqlCommand("ExamScheduleReportGetByExamEventId", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);

                sda.SelectCommand = cmd;
                sda.Fill(Dt);

                List<ExamScheduleReport> ObjLstESR = new List<ExamScheduleReport>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ExamScheduleReport objESR = new ExamScheduleReport();



                        objESR.StudentCount = (Convert.ToString(Dt.Rows[i]["StudentCount"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["StudentCount"]);
                        objESR.PaperName = (Convert.ToString(Dt.Rows[i]["PaperName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["PaperName"]);
                        objESR.PaperCode = (Convert.ToString(Dt.Rows[i]["PaperCode"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["PaperCode"]);
                        objESR.ExamDate = (Convert.ToString(Dt.Rows[i]["ExamDate"])).IsEmpty() ? "-" : string.Format("{0: dd-MM-yyyy}", (Dt.Rows[i]["ExamDate"]));                    
                        objESR.Duration = (Convert.ToString(Dt.Rows[i]["Duration"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["Duration"]);
                        objESR.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        objESR.ProgrammeModeName = (Convert.ToString(Dt.Rows[i]["ProgrammeModeName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["ProgrammeModeName"]);
                        objESR.PartName = (Convert.ToString(Dt.Rows[i]["PartName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["PartName"]);
                        objESR.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["BranchName"]);
                        objESR.PartTermName = (Convert.ToString(Dt.Rows[i]["PartTermName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["PartTermName"]);
                        objESR.InstancePartTermName = (Convert.ToString(Dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                        objESR.TeachingLearningMethodName = (Convert.ToString(Dt.Rows[i]["TeachingLearningMethodName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["TeachingLearningMethodName"]);
                        objESR.AssessmentMethodName = (Convert.ToString(Dt.Rows[i]["AssessmentMethodName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["AssessmentMethodName"]);
                        objESR.AssessmentType = (Convert.ToString(Dt.Rows[i]["AssessmentType"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["AssessmentType"]);
                        
                        objESR.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["FacultyName"]);

                        Count = Count + 1;
                        objESR.IndexId = Count;


                        ObjLstESR.Add(objESR);
                    }
                }
                else
                {
                    return Return.returnHttp("201", "No Record Found", null);
                }
                return Return.returnHttp("200", ObjLstESR, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion
    }
}