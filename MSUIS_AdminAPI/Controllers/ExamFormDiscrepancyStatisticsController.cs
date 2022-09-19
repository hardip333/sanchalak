using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MSUISApi.BAL;
using System.Dynamic;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class ExamFormDiscrepancyStatisticsController : ApiController 
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region CancelExamFormDetailsByProgramme
        [HttpPost]
        public HttpResponseMessage CancelExamFormDetailsByProgramme(ExamFormDiscrepancyStatistics ObjExam)
        {
            try
            {
                Int32 Count = 0;

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("CancelExamFormStatisticsGetByProg", Con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                da.Fill(dt);
                List<ExamFormDiscrepancyStatistics> ObjLstCancelExamForm = new List<ExamFormDiscrepancyStatistics>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ExamFormDiscrepancyStatistics ObjCancelExamForm = new ExamFormDiscrepancyStatistics();           
                        ObjCancelExamForm.PartTermId = Convert.ToInt64(dt.Rows[i]["PartTermId"]);
                        ObjCancelExamForm.ProgrammeId = Convert.ToInt64(dt.Rows[i]["ProgrammeId"]);
                        ObjCancelExamForm.BranchId = Convert.ToInt64(dt.Rows[i]["BranchId"]);
                        ObjCancelExamForm.ExamEventId = Convert.ToInt64(dt.Rows[i]["ExamEventId"]);
                        ObjCancelExamForm.PartTermName = Convert.ToString(dt.Rows[i]["PartTermName"]);
                        ObjCancelExamForm.ProgrammeName = Convert.ToString(dt.Rows[i]["ProgrammeName"]);
                        ObjCancelExamForm.BranchName = Convert.ToString(dt.Rows[i]["BranchName"]);
                        ObjCancelExamForm.ExamEvent = Convert.ToString(dt.Rows[i]["ExamEvent"]);
                        ObjCancelExamForm.ExamFormNotInwarded = Convert.ToString(dt.Rows[i]["ExamFormNotInwarded"]);
                        ObjLstCancelExamForm.Add(ObjCancelExamForm);
                        Count = Count + 1;
                        ObjCancelExamForm.IndexId = Count;

                    }
                    return Return.returnHttp("200", ObjLstCancelExamForm, null);
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

        #region CancelExamFormApplicantDetails
        [HttpPost]
        public HttpResponseMessage CancelExamFormApplicantDetails(ExamFormDiscrepancyStatistics ObjExam)
        {
            try
            {
                Int32 Count = 0;
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("getCancelExamFormStatisticsApplicantDetails", Con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@PartTermId", ObjExam.PartTermId);
                da.SelectCommand.Parameters.AddWithValue("@ProgrammeId", ObjExam.ProgrammeId);
                da.SelectCommand.Parameters.AddWithValue("@BranchId", ObjExam.BranchId);
                da.SelectCommand.Parameters.AddWithValue("@ExamEventId", ObjExam.ExamEventId);
                DataTable dt = new DataTable();
                da.Fill(dt);
                List<ExamFormDiscrepancyStatistics> ObjLstCancelExamForm = new List<ExamFormDiscrepancyStatistics>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ExamFormDiscrepancyStatistics ObjCancelExamForm = new ExamFormDiscrepancyStatistics();

                        ObjCancelExamForm.StudentPRN = (Convert.ToString(dt.Rows[i]["PRN"])).IsEmpty() ? 0:Convert.ToInt64(dt.Rows[i]["PRN"]);                     
                        ObjCancelExamForm.StudentName = (Convert.ToString(dt.Rows[i]["NameAsPerMarksheet"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["NameAsPerMarksheet"]);
                        ObjCancelExamForm.ResultStatus = (Convert.ToString(dt.Rows[i]["ResultStatus"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["ResultStatus"]);                    
                        ObjCancelExamForm.IsCancelled = (Convert.ToString(dt.Rows[i]["IsCancelled"])).IsEmpty() ? false : Convert.ToBoolean(dt.Rows[i]["IsCancelled"]);
                        Count = Count + 1;
                        ObjCancelExamForm.IndexId = Count;
                        ObjLstCancelExamForm.Add(ObjCancelExamForm);

                    }
                    return Return.returnHttp("200", ObjLstCancelExamForm, null);
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
      
        #region UpdateCancelExamFormDiscrepancyStatistics
        [HttpPost]
        public HttpResponseMessage UpdateCancelExamFormDiscrepancyStatistics(JObject jsonobject)

        {
            ExamFormDiscrepancyStatistics EFDS = new ExamFormDiscrepancyStatistics();
            dynamic jsonData = jsonobject;
            try
            {
                EFDS.CancelExamForm1 = Convert.ToString(jsonData.CancelExamForm1);
               
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                EFDS.UserId = Convert.ToInt64(responseToken);

                SqlCommand cmd = new SqlCommand("UpdateExamCancelFormDiscrepancyStatistics", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CancelExamForm1", EFDS.CancelExamForm1);
              
                cmd.Parameters.AddWithValue("@UserId", EFDS.UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                Con.Close();
                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your Data has been Updated successfully.";
                }

                return Return.returnHttp("200", strMessage, null);

                
            }
            catch (Exception e)
            {

                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion



    }
}
