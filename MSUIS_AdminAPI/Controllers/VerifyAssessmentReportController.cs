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
    public class VerifyAssessmentReportController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region Assessment Report Get by ProgrammeId
        [HttpPost]
        public HttpResponseMessage AssessmentReportGetbyProgrammeId(IncProgInstPartTermGroup IPTG)
        {
            try
            {
                PartTerm CheckFlag = VerifyAssessmentReportController.CheckFlag(IPTG.ProgrammeInstancePartTermId);
                
                if (CheckFlag.IsCompletedAssessment == true)
                {
                    List<PartTerm> PTData = VerifyAssessmentReportController.FetchPartTerm(IPTG.ProgrammeInstancePartTermId);
                List<PaperPT> PaperData = VerifyAssessmentReportController.FetchPaper(IPTG.ProgrammeId, IPTG.AcademicYearId, IPTG.SpecialisationId);
                List<PaperDetail> PaperDetailData = VerifyAssessmentReportController.FetchPaperDetail(IPTG.ProgrammeId, IPTG.AcademicYearId, IPTG.SpecialisationId);
                List<PaperATDetail> PaperATDetailData = VerifyAssessmentReportController.FetchPaperATDetail(IPTG.ProgrammeId, IPTG.AcademicYearId, IPTG.SpecialisationId);
                IncProgInstPartTermGroup OtherDetail = new IncProgInstPartTermGroup();
                foreach (PaperDetail PaperDetail in PaperDetailData)
                {
                    var PaperATDetail = PaperATDetailData.Where(e => e.PaperId == PaperDetail.PaperId && e.TeachingLearningMethodId == PaperDetail.TeachingLearningMethodId && e.AssessmentMethodId == PaperDetail.AssessmentMethodId).ToList();
                    if (PaperATDetail.Any())
                    {
                        PaperDetail.ATdata = PaperATDetail;
                    }
                }
                List<GroupPTDX> PaperItems = PaperData.Select(e => new GroupPTDX
                {
                    PaperPTMapId = e.Id,
                    GroupId = e.GroupId,
                    PaperId = e.PaperId,
                    GroupName = e.PaperName,
                    PaperName = e.PaperName,
                    PaperCode = e.PaperCode,
                    MaxMarks = e.MaxMarks,
                    MinMarks = e.MinMarks,
                    EvaluationId = e.EvaluationId,
                    EvaluationName = e.EvaluationName,
                    Credits = e.Credits,
                    ATDetail = e.ATDetail,
                    expanded = true,
                    ItemType = e.ItemType,
                    IsPaper = e.IsPaper,
                    ProgrammeInstancePartTermId = e.ProgrammeInstancePartTermId
                }).ToList();
                foreach (GroupPTDX Paper in PaperItems)
                {
                    var PaperDetail = PaperDetailData.Where(e => e.PaperId == Paper.PaperId).ToList();
                    if (PaperDetail.Any())
                    {
                        Paper.ATDetail = PaperDetail;
                    }
                }


                //===============================================================================
                List<PartTermDX> PartTermItems = PTData.Select(e => new PartTermDX
                {
                    Id = e.Id,
                    InstancePartTermName = e.InstancePartTermName,
                    FacultyName = e.FacultyName,
                    ProgrammeName = e.ProgrammeName,
                    BranchName = e.BranchName,
                    AcademicYearCode = e.AcademicYearCode,
                    IsPartTerm = true,
                    expanded = true,
                    ItemType = "Part Term"
                }).ToList();
                //List<GroupPTDX> GroupList = GroupItems.Where(e => e.ParentGroupId == e.Id).ToList();
                foreach (PartTermDX PT in PartTermItems)
                {
                    List<GroupPTDX> GPT = PaperItems.Where(e => e.ProgrammeInstancePartTermId == PT.Id).ToList();
                    if (GPT.Any())
                    {
                        PT.items = GPT;
                    }
                    OtherDetail.FacultyName = PT.FacultyName;
                    OtherDetail.ProgrammeName = PT.ProgrammeName;
                    OtherDetail.BranchName = PT.BranchName;
                    OtherDetail.AcademicYearCode = PT.AcademicYearCode;
                }
                return Return.returnHttp("200", (PartTermItems, OtherDetail), null);
                }
                else
                {
                    return Return.returnHttp("201", "Incomplete", null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region CheckFlag 
        public static PartTerm CheckFlag(Int64 PartTermId)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            PartTerm flag = new PartTerm();

            SqlCommand cmd = new SqlCommand("CheckFlagForVerifyAssessmentReport", Con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PartTermId", PartTermId);

            SqlDataAdapter Da = new SqlDataAdapter();
            DataTable Dt = new DataTable();
            Da.SelectCommand = cmd;
            Da.Fill(Dt);
            if (Dt.Rows.Count > 0)
            {
                for (int i = 0; i < Dt.Rows.Count; i++)
                {                    
                    flag.IsCompletedAssessment = (Convert.ToString(Dt.Rows[i]["IsCompletedAssessmentSts"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsCompletedAssessmentSts"]);
                }
            }
            return flag;
        }
        #endregion

        #region FetchPartTerm
        public static List<PartTerm> FetchPartTerm(Int64 ProgrammeInstancePartTermId)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            List<PartTerm> PTdata = new List<PartTerm>();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("ProgrammeInstancePartTermGetByPartTermId", Con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
            


            SqlDataAdapter Da = new SqlDataAdapter();
            DataTable Dt = new DataTable();
            Da.SelectCommand = cmd;
            Da.Fill(Dt);
            if (Dt.Rows.Count > 0)
            {
                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    PartTerm PT = new PartTerm();
                    PT.Id = (((Dt.Rows[i]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["Id"]);
                    PT.InstancePartTermName = (Convert.ToString(Dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                    PT.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                    PT.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                    PT.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["BranchName"]);
                    PT.AcademicYearCode = (Convert.ToString(Dt.Rows[i]["AcademicYearCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);

                    PTdata.Add(PT);
                }
            }
            return PTdata;
        }
        #endregion

        #region FetchPaper
        public static List<PaperPT> FetchPaper(Int32 ProgrammeId, Int32 AcademicYearId, Int32 SpecialisationId)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            List<PaperPT> Paperdata = new List<PaperPT>();

            SqlCommand cmd = new SqlCommand("GroupAndPaperReportGetPaperByProgrammeId", Con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ProgrammeId", ProgrammeId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@SpecialisationId", SpecialisationId);

            SqlDataAdapter Da = new SqlDataAdapter();
            DataTable Dt = new DataTable();
            Da.SelectCommand = cmd;
            Da.Fill(Dt);
            if (Dt.Rows.Count > 0)
            {
                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    PaperPT Paper = new PaperPT();

                    Paper.Id = (((Dt.Rows[i]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["Id"]);
                    Paper.GroupName = (Convert.ToString(Dt.Rows[i]["GroupName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["GroupName"]);
                    Paper.GroupId = (((Dt.Rows[i]["GroupId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["GroupId"]);
                    Paper.PaperId = (((Dt.Rows[i]["PaperId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["PaperId"]);
                    Paper.ProgrammeInstancePartTermId = (((Dt.Rows[i]["ProgrammeInstancePartTermId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["ProgrammeInstancePartTermId"]);
                    Paper.PaperName = (Convert.ToString(Dt.Rows[i]["PaperName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["PaperName"]);
                    Paper.PaperCode = (Convert.ToString(Dt.Rows[i]["PaperCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["PaperCode"]);
                    Paper.PaperName = Paper.PaperName + ": ( " + Paper.PaperCode + " )";
                    Paper.ItemType = "Paper";
                    Paper.IsPaper = true;
                    Paper.MaxMarks = (((Dt.Rows[i]["MaxMarks"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["MaxMarks"]);
                    Paper.MinMarks = (((Dt.Rows[i]["MinMarks"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["MinMarks"]);
                    Paper.Credits = (((Dt.Rows[i]["Credits"]).ToString()).IsEmpty()) ? 0 : Convert.ToDecimal(Dt.Rows[i]["Credits"]);
                    Paper.EvaluationId = (((Dt.Rows[i]["EvaluationId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["EvaluationId"]);
                    Paper.EvaluationName = (Convert.ToString(Dt.Rows[i]["EvaluationName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["EvaluationName"]);

                    Paperdata.Add(Paper);
                }
            }
            return Paperdata;
        }

        #endregion

        #region FetchPaperDetail
        public static List<PaperDetail> FetchPaperDetail(Int32 ProgrammeId, Int32 AcademicYearId, Int32 SpecialisationId)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            List<PaperDetail> PaperDetaildata = new List<PaperDetail>();

            SqlCommand cmd = new SqlCommand("GroupAndPaperReportGetPaperDetailByProgrammeId", Con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ProgrammeId", ProgrammeId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@SpecialisationId", SpecialisationId);

            SqlDataAdapter Da = new SqlDataAdapter();
            DataTable Dt = new DataTable();
            Da.SelectCommand = cmd;
            Da.Fill(Dt);
            if (Dt.Rows.Count > 0)
            {
                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    PaperDetail PaperDetail = new PaperDetail();

                    PaperDetail.AssessmentMethodId = (((Dt.Rows[i]["AssessmentMethodId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["AssessmentMethodId"]);
                    PaperDetail.TeachingLearningMethodId = (((Dt.Rows[i]["TeachingLearningMethodId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["TeachingLearningMethodId"]);
                    PaperDetail.PaperId = (((Dt.Rows[i]["PaperId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["PaperId"]);
                    PaperDetail.PaperName = (Convert.ToString(Dt.Rows[i]["PaperName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["PaperName"]);
                    PaperDetail.PaperCode = (Convert.ToString(Dt.Rows[i]["PaperCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["PaperCode"]);
                    PaperDetail.AssessmentMethodName = (Convert.ToString(Dt.Rows[i]["AssessmentMethodName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["AssessmentMethodName"]);
                    PaperDetail.TeachingLearningMethodName = (Convert.ToString(Dt.Rows[i]["TeachingLearningMethodName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["TeachingLearningMethodName"]);
                    PaperDetail.NoOfHoursPerWeek = (((Dt.Rows[i]["NoOfHoursPerWeek"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["NoOfHoursPerWeek"]);
                    PaperDetail.AssessmentMethodMarks = (((Dt.Rows[i]["AssessmentMethodMarks"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["AssessmentMethodMarks"]);
                    PaperDetail.NoOfCredits = (((Dt.Rows[i]["NoOfCredits"]).ToString()).IsEmpty()) ? 0 : Convert.ToDecimal(Dt.Rows[i]["NoOfCredits"]);

                    PaperDetaildata.Add(PaperDetail);
                }
            }
            return PaperDetaildata;
        }
        #endregion

        #region FetchPaperATDetail
        public static List<PaperATDetail> FetchPaperATDetail(Int32 ProgrammeId, Int32 AcademicYearId, Int32 SpecialisationId)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            List<PaperATDetail> PaperATDetaildata = new List<PaperATDetail>();

            SqlCommand cmd = new SqlCommand("GroupAndPaperReportGetPaperATDetailByProgrammeId", Con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ProgrammeId", ProgrammeId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@SpecialisationId", SpecialisationId);

            SqlDataAdapter Da = new SqlDataAdapter();
            DataTable Dt = new DataTable();
            Da.SelectCommand = cmd;
            Da.Fill(Dt);
            if (Dt.Rows.Count > 0)
            {
                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    PaperATDetail PaperATDetail = new PaperATDetail();

                    PaperATDetail.AssessmentTypeMaxMarks = (((Dt.Rows[i]["AssessmentTypeMaxMarks"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["AssessmentTypeMaxMarks"]);
                    PaperATDetail.AssessmentTypeMinMarks = (((Dt.Rows[i]["AssessmentTypeMinMarks"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["AssessmentTypeMinMarks"]);
                    PaperATDetail.AssessmentType = (Convert.ToString(Dt.Rows[i]["AssessmentType"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["AssessmentType"]);
                    PaperATDetail.PaperId = (((Dt.Rows[i]["PaperId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["PaperId"]);
                    PaperATDetail.AssessmentMethodId = (((Dt.Rows[i]["AssessmentMethodId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["AssessmentMethodId"]);
                    PaperATDetail.TeachingLearningMethodId = (((Dt.Rows[i]["TeachingLearningMethodId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["TeachingLearningMethodId"]);

                    PaperATDetaildata.Add(PaperATDetail);
                }
            }
            return PaperATDetaildata;
        }
        #endregion

        #region GetRemarks
        [HttpPost]
        public HttpResponseMessage GetRemarks(VerifyAssessmentReport ObjVerifyAssessmentReport)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }


                SqlCommand cmd = new SqlCommand("GetRemarksForVerifyAssessmentReport", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ObjVerifyAssessmentReport.Id);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<VerifyAssessmentReport> ObjLstVerify = new List<VerifyAssessmentReport>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        VerifyAssessmentReport ObjVerify = new VerifyAssessmentReport();

                        ObjVerify.Id = Convert.ToInt64(dr["Id"]);
                        ObjVerify.AssessmentReportRemark = Convert.ToString(dr["AssessmentReportRemark"]);
                        ObjVerify.IsApprovedAssessmentReport = Convert.ToBoolean(dr["IsApprovedAssessmentReport"]);

                        ObjLstVerify.Add(ObjVerify);
                    }

                }
                return Return.returnHttp("200", ObjLstVerify, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region Verify Assessment Report Edit
        [HttpPost]
        public HttpResponseMessage VerifyAssessmentReportEdit(VerifyAssessmentReport ObjVerifyAssessmentReport)
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

                if (String.IsNullOrWhiteSpace(Convert.ToString(ObjVerifyAssessmentReport.AssessmentReportRemark)))
                {
                    return Return.returnHttp("201", "Please enter Faculty Remarks", null);
                }

                else
                {

                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("VerifyAssessmentReportEdit", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", ObjVerifyAssessmentReport.Id);
                    cmd.Parameters.AddWithValue("@IsApprovedAssessmentReport", ObjVerifyAssessmentReport.IsApprovedAssessmentReport);
                    cmd.Parameters.AddWithValue("@AssessmentReportRemark", ObjVerifyAssessmentReport.AssessmentReportRemark);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);

                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    Con.Open();

                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);

                    Con.Close();
                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Your changes approved successfully.";
                    }

                    return Return.returnHttp("200", strMessage.ToString(), null);
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
