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
    public class InstitutePartTermPaperMapController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));


        #region MstInstituteGet
        [HttpPost]
        public HttpResponseMessage MstInstituteGet()
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

                List<MstInstitute> ObjLstInstitute = new List<MstInstitute>();
                BALInstituteList ObjBalInstituteList = new BALInstituteList();
                ObjLstInstitute = ObjBalInstituteList.InstituteListGet();

                if (ObjLstInstitute != null)
                    return Return.returnHttp("200", ObjLstInstitute, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Programme Get By FacultyId
        [HttpPost]
        public HttpResponseMessage MstProgrammeGetByFacultyId(MstProgramme ObjProg)
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

                List<MstProgramme> Programmedata = new List<MstProgramme>();

                SqlCommand cmd = new SqlCommand("GroupAndPaperReportGetProgrammeByFacultyId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FacultyId", ObjProg.FacultyId);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgramme Programme = new MstProgramme();
                        Programme.Id = (((Dt.Rows[i]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["Id"]);
                        Programme.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);

                        Programmedata.Add(Programme);
                    }
                }
                if (Programmedata != null)
                    return Return.returnHttp("200", Programmedata, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);

            }
            catch (Exception e)
            {

                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }

        #endregion

        #region PartTerm Get By ProgrammeId
        [HttpPost]
        public HttpResponseMessage PartTermGetByProgrammeId(IncProgInstPartTermGroup IPTG)
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
                List<PartTerm> PTdata = new List<PartTerm>();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("InstitutePartTermMapGetbyProgId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeId", IPTG.ProgrammeId);
                cmd.Parameters.AddWithValue("@AcademicYearId", IPTG.AcademicYearId);
                cmd.Parameters.AddWithValue("@InstituteId", IPTG.InstituteId);
                

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
                        PTdata.Add(PT);
                    }
                }
                


                if (PTdata.Any())
                {
                    return Return.returnHttp("200", PTdata, null);
                }

                else
                {
                    return Return.returnHttp("201", "No Record Found", null);
                }

            }
            catch (Exception e)
            {

                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }

        #endregion

        #region FetchGroupBulkSelect
        public static List<GroupPT> FetchGroupBulkSelect(Int64 PartTerm)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            List<GroupPT> PTGdata = new List<GroupPT>();

            SqlCommand cmd = new SqlCommand("IPTPMGetGroup", Con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PartTerm", PartTerm);
            
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

        #region FetchPaperBulkSelect 
        public static List<PaperPT> FetchPaperBulkSelect(Int64 PartTerm, Int32 InstituteId)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            List<PaperPT> Paperdata = new List<PaperPT>();


            SqlCommand cmd = new SqlCommand("IPTPMGetPaper", Con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PartTerm", PartTerm);
            cmd.Parameters.AddWithValue("@InstituteId", InstituteId);

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
                    Paper.ProgrammeInstancePartTermId = (((Dt.Rows[i]["ProgrammeInstancePartTermId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["ProgrammeInstancePartTermId"]);
                    Paper.PaperCheck= (((Dt.Rows[i]["PaperSelected"]).ToString()).IsEmpty()) ? false : Convert.ToBoolean(Dt.Rows[i]["PaperSelected"]);
                    Paperdata.Add(Paper);
                }
            }
            return Paperdata;
        }
        #endregion

        #region Get Paper Map Tree
        [HttpPost]
        public HttpResponseMessage GetPaperMapTree(StudentList IPTG)
        {
            try
            {
                List<GroupPT> GroupData = InstitutePartTermPaperMapController.FetchGroupBulkSelect(IPTG.ProgrammeInstancePartTermId);
                List<PaperPT> PaperData = InstitutePartTermPaperMapController.FetchPaperBulkSelect(IPTG.ProgrammeInstancePartTermId, IPTG.InstituteId);

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
                    ProgrammeInstancePartTermId = e.ProgrammeInstancePartTermId,
                    EvaluationName = e.EvaluationName,
                    Credits = e.Credits,
                    ATDetail = e.ATDetail,
                    expanded = true,
                    ItemType = e.ItemType,
                    IsPaper = e.IsPaper,
                    PaperCheck=e.PaperCheck,
                    icon = "icofont icofont-file-alt"


                }).ToList();
                foreach(GroupSelect p in PaperItems)
                {
                    if (p.PaperCheck == true)
                    {
                        p.state = new state();
                        p.state.disabled = true;
                        p.state.selected = true;
                    }
                }
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
                    icon = "icofont icofont-folder"

                }).ToList();
                foreach (GroupSelect Group in GroupItems)
                {
                    Group.state = new state();
                    Group.state.opened = true;
                    Group.state.disabled = true;
                    List<GroupSelect> PaperList = PaperItems.Where(e => e.ParentGroupId == Group.Id).ToList();
                    if (PaperList.Any())
                    {
                        Group.children = PaperList;
                    }
                }
                List<GroupSelect> GroupList = GroupItems.Where(e => e.PreferanceGroupCheck == true).ToList();
                if (GroupList.Any())
                {
                    foreach (GroupSelect Group in GroupList)
                    {
                        var Group1 = GroupList.Where(e => e.ParentGroupId == Group.Id && e.ParentGroupId != e.Id).ToList();
                        if (Group1.Any())
                        {
                            Group.children = Group1;
                        }
                    }
                    GroupFinalData = GroupList.Where(e => e.ParentGroupId == e.Id).ToList();
                }
                else
                {
                    foreach (GroupSelect Group in GroupItems)
                    {
                        var Group12 = GroupItems.Where(e => e.ParentGroupId == Group.Id && e.ParentGroupId != e.Id).ToList();
                        if (Group12.Any())
                        {
                            Group.children = Group12;
                        }
                    }
                    GroupFinalData = GroupItems.Where(e => e.ParentGroupId == e.Id).ToList();

                }
                //List<PartTerm> PTList = IPTG.PTList;
                //PartTerm PT1 = new PartTerm();
                //foreach (PartTerm PT in PTList)

                PartTerm PT = new PartTerm();
                PT.Id = IPTG.ProgrammeInstancePartTermId;
                PT.InstancePartTermName = IPTG.InstancePartTermName;
                PT.state = new state();
                PT.state.opened = true;
                PT.state.disabled = true;
                PT.text = PT.InstancePartTermName;
                var children = GroupFinalData.Where(e => e.ProgrammeInstancePartTermId == PT.Id).ToList();
                if (children.Any())
                {
                    PT.children = children;
                }
                GPreportdata Data = new GPreportdata();
                //Data.Parent = GroupFinalData;
                Data.Parent = PT;
                Data.MinMaxData = GroupData;
                Data.PRN = IPTG.PRN;
                Data.StudentAcademicId = IPTG.Id;
                Data.ProgrammeInstancePartTermId = IPTG.ProgrammeInstancePartTermId;

                return Return.returnHttp("200", Data, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region SavePaperMapTree
        [HttpPost]
        public HttpResponseMessage SavePaperMapTree(GPreportdata GAP)
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
                Int64 UserId = Convert.ToInt64(res.ToString());
                String ErrorMsg = "Kindly Select Paper more than Maximum Condition in listed groups :";

                DataTable PaperList = new DataTable();
                //PaperList.Columns.Add(new DataColumn("Id", typeof(Int64)));
                PaperList.Columns.Add(new DataColumn("ProgrammeInstancePartTermId", typeof(Int64)));
                PaperList.Columns.Add(new DataColumn("StudentPRN", typeof(Int64)));
                PaperList.Columns.Add(new DataColumn("StudentAcademicId", typeof(Int64)));
                PaperList.Columns.Add(new DataColumn("PaperId", typeof(Int64)));
                PaperList.Columns.Add(new DataColumn("PaperCode", typeof(String)));
                PaperList.Columns.Add(new DataColumn("PaperName", typeof(String)));
                PaperList.Columns.Add(new DataColumn("MinMarks", typeof(Int32)));
                PaperList.Columns.Add(new DataColumn("MaxMarks", typeof(Int32)));
                PaperList.Columns.Add(new DataColumn("Credits", typeof(Decimal)));
                PaperList.Columns.Add(new DataColumn("Counter", typeof(Int32)));
                PaperList.Columns.Add(new DataColumn("UserId", typeof(Int64)));
                PaperList.Columns.Add(new DataColumn("UserTime", typeof(DateTime)));



                List<GroupSelect> FinalArray = GAP.FinalArray;
                List<GroupPT> MinMaxData = GAP.MinMaxData;
                //List<String> Sample = new List<String>();
                Int64 ProgrammeInstancePartTermId = Convert.ToInt64(GAP.ProgrammeInstancePartTermId);
                Int32 InstituteId = Convert.ToInt32(GAP.InstituteId);
                Boolean flag = true;

                foreach (GroupSelect GP in FinalArray)
                {
                    foreach (GroupPT k in MinMaxData)
                    {
                        if (GP.IsPaper == true)
                        {
                            if (k.Count < k.MaxPapers && GP.ParentGroupId == k.Id)
                            {
                                flag = false;
                                ErrorMsg = ErrorMsg + " " + k.GroupName + ",";
                            }
                            else if (GP.ParentGroupId == k.Id)
                            {
                                break;
                            }
                        }
                    }
                }
                if (flag == true)
                {
                    String PaperIdList = "";
                    foreach (GroupSelect G in FinalArray)
                    {
                        ProgrammeInstancePartTermId = Convert.ToInt64(G.ProgrammeInstancePartTermId);
                        if (G.IsPaper == true)
                        {
                            PaperList.Rows.Add(0, 0, 0, G.PaperId, G.PaperCode, G.PaperName, G.MinMarks, G.MaxMarks, G.Credits, 0, UserId, datetime);
                            PaperIdList = PaperIdList + Convert.ToString(G.PaperId) + ", ";
                        }
                    }
                    String msg = InstitutePartTermPaperMapController.IPTPMSave(PaperList, InstituteId, datetime, UserId, PaperIdList, ProgrammeInstancePartTermId);
                    if (msg == "TRUE")
                    {
                        return Return.returnHttp("200", msg, null);
                    }
                    else
                    {
                        return Return.returnHttp("201", msg, null);
                        //return Return.returnHttp("201", "Please try again", null);
                    }

                }
                else
                {
                    return Return.returnHttp("200", ErrorMsg, null);
                }

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
            finally
            {
                Con.Close();
            }
        }
        #endregion

        #region IPTPMSave

        public static String IPTPMSave(DataTable PaperList, Int32 InstituteId, DateTime date, Int64 UserId, String PaperIdList, Int64 ProgrammeInstancePartTermId)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            try
            {

                SqlTransaction ST;
                Int32 Count = Convert.ToInt32(PaperList.Rows.Count);
                SqlCommand CMDarray = new SqlCommand();

                CMDarray = new SqlCommand("IPTPMAdd", Con);
                CMDarray.CommandType = CommandType.StoredProcedure;

                CMDarray.Parameters.AddWithValue("@PaperList", PaperList);
                CMDarray.Parameters.AddWithValue("@PaperIdList", PaperIdList);
                CMDarray.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                CMDarray.Parameters.AddWithValue("@InstituteId", InstituteId);
                CMDarray.Parameters.AddWithValue("@UserTime", date);
                CMDarray.Parameters.AddWithValue("@UserId", UserId);
                CMDarray.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                CMDarray.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                ST = Con.BeginTransaction();
                try
                {
                    CMDarray.Transaction = ST;
                    int ans = CMDarray.ExecuteNonQuery();
                    String msg = Convert.ToString(CMDarray.Parameters["@Message"].Value);

                    ST.Commit();
                    return msg;
                    //return true;
                }
                catch (SqlException sqlError)
                {
                    ST.Rollback();
                    String strMessageErr = Convert.ToString(sqlError);
                    return strMessageErr;
                    //return false;
                }
            }
            catch (Exception e)
            {
                String error = e.Message;
                return error;
                //return false;
            }
            finally
            {
                Con.Close();
            }
        }
        #endregion

        #region IPTPMGetList
        [HttpPost]
        public HttpResponseMessage IPTPMGetList(InstitutePartTermPaperMap IPTPM)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("IPTPMGet", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InstituteId", IPTPM.InstituteId);
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", IPTPM.ProgrammeInstancePartTermId);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                
                List<InstitutePartTermPaperMap> IPTPMList = new List<InstitutePartTermPaperMap>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        InstitutePartTermPaperMap ObjIPTPM = new InstitutePartTermPaperMap();

                        ObjIPTPM.Id = (((dr["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(dr["Id"]);
                        ObjIPTPM.InstituteId = (((dr["InstituteId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(dr["InstituteId"]);
                        ObjIPTPM.InstituteName = (Convert.ToString(dr["InstituteName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["InstituteName"]);
                        ObjIPTPM.ProgrammeInstancePartTermId = (((dr["ProgrammeInstancePartTermId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(dr["ProgrammeInstancePartTermId"]);
                        ObjIPTPM.InstancePartTermName = (Convert.ToString(dr["InstancePartTermName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["InstancePartTermName"]);
                        ObjIPTPM.PaperId = (((dr["PartTermPaperMapId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(dr["PartTermPaperMapId"]);
                        ObjIPTPM.PaperName = (Convert.ToString(dr["PaperName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["PaperName"]);

                        IPTPMList.Add(ObjIPTPM);
                    }
                    return Return.returnHttp("200", IPTPMList, null);
                }
                else
                {
                    return Return.returnHttp("201", "Record Not Found", null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion
        #region IPTPMDelete
        [HttpPost]
        public HttpResponseMessage IPTPMDelete(InstitutePartTermPaperMap IPTPM)
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
                Int64 UserId = Convert.ToInt64(res.ToString());

                Int32 Id = Convert.ToInt32(IPTPM.Id);

                SqlCommand cmd = new SqlCommand("IPTPMDelete", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);

                Con.Close();
                return Return.returnHttp("200", "Data Deleted", null);
                //return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion


    }
}