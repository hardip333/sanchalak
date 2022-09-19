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
    public class VerifyStructureReportController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));


        #region FetchGroupSelect
        public static List<GroupPT> FetchGroupSelect(Int64 PartTermId)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            List<GroupPT> PTGdata = new List<GroupPT>();

            SqlCommand cmd = new SqlCommand("GroupAndPaperReportGetGroupWithoutPRN", Con);
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
                    GroupPT PTG = new GroupPT();

                    PTG.Id = (((Dt.Rows[i]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["Id"]);
                    PTG.GroupName = (Convert.ToString(Dt.Rows[i]["GroupName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["GroupName"]);
                    PTG.ParentGroupId = (((Dt.Rows[i]["ParentGroupId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["ParentGroupId"]);
                    PTG.ProgrammeInstancePartTermId = (((Dt.Rows[i]["ProgrammeInstancePartTermId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["ProgrammeInstancePartTermId"]);
                    PTG.ContainsGroup = (Convert.ToString(Dt.Rows[i]["ContainsSubgroup"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["ContainsSubgroup"]);
                    PTG.MinPapers = (((Dt.Rows[i]["MinPapers"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["MinPapers"]);
                    PTG.MaxPapers = (((Dt.Rows[i]["MaxPapers"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["MaxPapers"]);
                    PTG.MinSubGroups = (((Dt.Rows[i]["MinSubGroup"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["MinSubGroup"]);
                    PTG.MaxSubGroups = (((Dt.Rows[i]["MaxSubGroup"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["MaxSubGroup"]);
                    PTG.InstancePartTermName = (Convert.ToString(Dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                    PTG.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                    PTG.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                    PTG.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["BranchName"]);
                    PTG.SubChildCount = (((Dt.Rows[i]["SubChild"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["SubChild"]);
                    //PTG.PreferanceGroupCheck = (((Dt.Rows[i]["PreferanceGroupCheck"]).ToString()).IsEmpty()) ? false : Convert.ToBoolean(Dt.Rows[i]["PreferanceGroupCheck"]);
                    //PTG.IncPreferanceGroupMapId = (((Dt.Rows[i]["IncPreferanceGroupMapId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["IncPreferanceGroupMapId"]);
                    PTG.ItemType = "Group";
                    PTG.IsPaper = false;
                    PTG.Count = 0;
                    if (PTG.ContainsGroup == true)
                    {
                        PTG.GroupName = PTG.GroupName + ": Max Group Allowed = " + PTG.MaxSubGroups + ", Min Group Allowed = " + PTG.MinSubGroups;
                    }
                    else
                    {
                        PTG.GroupName = PTG.GroupName + ": Max Paper Allowed = " + PTG.MaxPapers + ", Min Paper Allowed = " + PTG.MinPapers;
                    }
                    PTGdata.Add(PTG);
                }
            }
            return PTGdata;
        }
        #endregion

        #region FetchPaperSelect 
        public static List<PaperPT> FetchPaperSelect(Int64 PartTermId)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            List<PaperPT> Paperdata = new List<PaperPT>();

            SqlCommand cmd = new SqlCommand("GroupAndPaperReportGetPaperWithoutPRN", Con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PartTermId", PartTermId);
            //cmd.Parameters.AddWithValue("@PRN", PRN);

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

        #region CheckFlag 
        public static PartTerm CheckFlag(Int64 PartTermId)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            PartTerm flag = new PartTerm();

            SqlCommand cmd = new SqlCommand("CheckFlagForVerifyStructureReport", Con);
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
                    /*flag.CompleteStatusFlag1 = (((Dt.Rows[i]["CompleteStatusFlag1"]).ToString()).IsEmpty()) ? false : Convert.ToBoolean(Dt.Rows[i]["CompleteStatusFlag1"]);
                    flag.CompleteStatusFlag2 = (Convert.ToString(Dt.Rows[i]["CompleteStatusFlag2"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["CompleteStatusFlag2"]);                                       */
                    flag.IsCompletedSts = (Convert.ToString(Dt.Rows[i]["IsCompletedSts"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsCompletedSts"]);                                       
                }
            }
            return flag;
        }
        #endregion

        #region Group Select Get 
        [HttpPost]
        public HttpResponseMessage GroupSelectGet(PartTerm IPTG)
        {
            try
            {
                PartTerm CheckFlag = VerifyStructureReportController.CheckFlag(IPTG.Id);
                //if (CheckFlag.CompleteStatusFlag1 == true && CheckFlag.CompleteStatusFlag2 == true)
                if (CheckFlag.IsCompletedSts == true)
                { 
                List<GroupPT> GroupData = VerifyStructureReportController.FetchGroupSelect(IPTG.Id);
                List<PaperPT> PaperData = VerifyStructureReportController.FetchPaperSelect(IPTG.Id);

                List<GroupSelect> GroupFinalData = new List<GroupSelect>();
                List<GroupSelect> PaperItems = PaperData.Select(e => new GroupSelect
                {
                    PaperPTMapId = e.Id,
                    ParentGroupId = e.GroupId,
                    PaperId = e.PaperId,
                    text = e.PaperName,
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
                    IsPaper = e.IsPaper


                }).ToList();

                List<GroupSelect> GroupItems = GroupData.Select(e => new GroupSelect
                {
                    Id = e.Id,
                    ProgrammeInstancePartTermId = e.ProgrammeInstancePartTermId,
                    ParentGroupId = e.ParentGroupId,
                    InstancePartTermName = e.InstancePartTermName,
                    text = e.GroupName,
                    ContainsGroup = e.ContainsGroup,
                    MinPapers = e.MinPapers,
                    MaxPapers = e.MaxPapers,
                    MinSubGroups = e.MinSubGroups,
                    MaxSubGroups = e.MaxSubGroups,
                    FacultyName = e.FacultyName,
                    ProgrammeName = e.ProgrammeName,
                    BranchName = e.BranchName,
                    SubChildCount = e.SubChildCount,
                    expanded = true,
                    ItemType = e.ItemType,
                    IsPaper = e.IsPaper,
                    PreferanceGroupCheck = e.PreferanceGroupCheck,
                    IncPreferanceGroupMapId = e.IncPreferanceGroupMapId

                }).ToList();
                foreach (GroupSelect Group in GroupItems)
                {
                    Group.state = new state();
                    Group.state.opened = true;
                    List<GroupSelect> PaperList = PaperItems.Where(e => e.ParentGroupId == Group.Id).ToList();
                    if (PaperList.Any())
                    {
                        Group.children = PaperList;
                    }
                }
                foreach (GroupSelect Group in GroupItems)
                {
                    var children = GroupItems.Where(e => e.ParentGroupId == Group.Id && e.ParentGroupId != e.Id).ToList();
                    if (children.Any())
                    {
                        Group.children = children;
                    }
                }
                GroupFinalData = GroupItems.Where(e => e.ParentGroupId == e.Id).ToList();

                GroupSelect PartTerm = new GroupSelect();
                PartTerm.Id = IPTG.Id;
                PartTerm.text = GroupFinalData[0].InstancePartTermName;
                PartTerm.ItemType = "Part Term";
                PartTerm.expanded = true;
                PartTerm.children = GroupFinalData;

                return Return.returnHttp("200", PartTerm, null);
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

        #region GetRemarks
        [HttpPost]
        public HttpResponseMessage GetRemarks(VerifyStructureReport ObjVerifyStructureReport)
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


                SqlCommand cmd = new SqlCommand("GetRemarksForVerifyStructureReport", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ObjVerifyStructureReport.Id);
                
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                
                List<VerifyStructureReport> ObjLstVerify = new List<VerifyStructureReport>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        VerifyStructureReport ObjVerify = new VerifyStructureReport();

                        ObjVerify.Id = Convert.ToInt64(dr["Id"]);
                        ObjVerify.StructureReportRemark = Convert.ToString(dr["StructureReportRemark"]);
                        ObjVerify.IsApprovedStructureReport = Convert.ToBoolean(dr["IsApprovedStructureReport"]);

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

        #region Verify Structure Report Edit
        [HttpPost]
        public HttpResponseMessage VerifyStructureReportEdit(VerifyStructureReport ObjVerifyStructureReport)
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

                if (String.IsNullOrWhiteSpace(Convert.ToString(ObjVerifyStructureReport.StructureReportRemark)))
                {
                    return Return.returnHttp("201", "Please enter Faculty Remarks", null);
                }

                else
                {

                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("VerifyStructureReportEdit", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", ObjVerifyStructureReport.Id);
                    cmd.Parameters.AddWithValue("@IsApprovedStructureReport", ObjVerifyStructureReport.IsApprovedStructureReport);
                    cmd.Parameters.AddWithValue("@StructureReportRemark", ObjVerifyStructureReport.StructureReportRemark);
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
