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
    public class GroupAndPaperReportController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));



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

        #region ProgrammePartListGetByProgrammeId
        [HttpPost]
        public HttpResponseMessage ProgrammePartListGetByProgrammeId(ProgrammeInstancePart ObjProg)
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

                List<ProgrammeInstancePart> Programmedata = new List<ProgrammeInstancePart>();

                SqlCommand cmd = new SqlCommand("GroupAndPaperReportGetProgrammePartByProgACADId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeId", ObjProg.ProgrammeId);
                cmd.Parameters.AddWithValue("@AcademicYearId", ObjProg.AcademicYearId);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePart Programme = new ProgrammeInstancePart();
                        Programme.Id = (((Dt.Rows[i]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["Id"]);
                        Programme.PartShortName = (Convert.ToString(Dt.Rows[i]["PartShortName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["PartShortName"]);

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

        #region ProgrammePartTermListGetByProgrammePartId
        [HttpPost]
        public HttpResponseMessage ProgrammePartTermListGetByProgrammePartId(ProgrammeInstancePart ObjProg)
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

                List<PartTerm> Programmedata = new List<PartTerm>();

                SqlCommand cmd = new SqlCommand("GroupAndPaperReportGetProgrammePartTermByProgpartId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeInstancePartId", ObjProg.ProgrammeInstancePartId);
                cmd.Parameters.AddWithValue("@SpecialisationId", ObjProg.SpecialisationId);
                cmd.Parameters.AddWithValue("@InstituteId", ObjProg.InstituteId);
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

                        Programmedata.Add(PT);
                    }
                    return Return.returnHttp("200", Programmedata, null);
                }


                else
                {
                    return Return.returnHttp("201", "Semester Is not launch or Paper is not attached to this Institute", null);
                }
                    

            }
            catch (Exception e)
            {

                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region Programme Get By InstituteId
        [HttpPost]
        public HttpResponseMessage MstProgrammeGetByInstId(MstProgramme ObjProg)
        {
            try
            {
                Request.Headers.TryGetValues("DepartmentId", out IEnumerable<string> values);
                var DepartmentId = values?.FirstOrDefault();
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();

                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                List<MstProgramme> Programmedata = new List<MstProgramme>();

                SqlCommand cmd = new SqlCommand("GroupAndPaperReportGetProgrammeByInstituteId", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (DepartmentId != null || DepartmentId != "" || DepartmentId != "0")
                {
                    cmd.Parameters.AddWithValue("@InstituteId", ObjProg.InstituteId);
                    cmd.Parameters.AddWithValue("@DepartmentId", DepartmentId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@InstituteId", ObjProg.InstituteId);
                }
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

        #region Institute Get By FacultyId
        [HttpPost]
        public HttpResponseMessage MstInstituteGetByFacultyId(MstProgramme ObjProg)
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

                List<MstInstitute> Institutedata = new List<MstInstitute>();

                SqlCommand cmd = new SqlCommand("GroupAndPaperReportGetInstituteByFacultyId", Con);
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
                        MstInstitute Institute = new MstInstitute();
                        Institute.Id = (((Dt.Rows[i]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["Id"]);
                        Institute.InstituteName = (Convert.ToString(Dt.Rows[i]["InstituteName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstituteName"]);

                        Institutedata.Add(Institute);
                    }
                }
                if (Institutedata != null)
                    return Return.returnHttp("200", Institutedata, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);

            }
            catch (Exception e)
            {

                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }

        #endregion     

        #region BranchList Get By ProgrammePartId
        [HttpPost]
        public HttpResponseMessage MstProgrammeBranchListGetByProgrammePartId(ProgrammeInstancePart ObjProg)
        {
            try
            {
                Request.Headers.TryGetValues("DepartmentId", out IEnumerable<string> values);
                var DepartmentId = values?.FirstOrDefault();
                Request.Headers.TryGetValues("FacultyId", out IEnumerable<string> values1);
                var FacultyId = values1?.FirstOrDefault();
                Request.Headers.TryGetValues("InstituteId", out IEnumerable<string> values2);
                var InstituteId = values2?.FirstOrDefault();

                
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

                int ProgrammeId = Convert.ToInt32(ObjProg.ProgrammeId);
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter cmdda = new SqlDataAdapter("MstProgrammeBranchMapGetByProgrammeId", Con);

                cmdda.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (DepartmentId != null || DepartmentId != "" || DepartmentId != "0")
                {
                    cmdda.SelectCommand.Parameters.AddWithValue("@ProgrammeId", ProgrammeId);
                    cmdda.SelectCommand.Parameters.AddWithValue("@FacultyId", FacultyId);
                    cmdda.SelectCommand.Parameters.AddWithValue("@InstituteId", InstituteId);
                    cmdda.SelectCommand.Parameters.AddWithValue("@DepartmentId", DepartmentId);
                }
                else
                {
                    cmdda.SelectCommand.Parameters.AddWithValue("@ProgrammeId", ProgrammeId);
                }

                    

                DataTable Dt = new DataTable();
                cmdda.Fill(Dt);


                List<MstSpecialisation> ObjLstMstSpecialisation = new List<MstSpecialisation>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstSpecialisation ObjMstSpecialisation = new MstSpecialisation();

                        ObjMstSpecialisation.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        ObjMstSpecialisation.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        ObjMstSpecialisation.ProgrammeCode = (Convert.ToString(Dt.Rows[i]["ProgrammeCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeCode"]);
                        ObjMstSpecialisation.BranchName = ((Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "Not Defined" : (Convert.ToString(Dt.Rows[i]["BranchName"])));
                        ObjMstSpecialisation.FacultyId = (((Dt.Rows[i]["FacultyId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        ObjMstSpecialisation.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        ObjMstSpecialisation.ProgrammeId = (Convert.ToString(Dt.Rows[i]["ProgrammeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);
                        ObjMstSpecialisation.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        ObjMstSpecialisation.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjMstSpecialisation.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);

                        ObjLstMstSpecialisation.Add(ObjMstSpecialisation);
                    }
                    return Return.returnHttp("200", ObjLstMstSpecialisation, null);
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
                List<PartTerm> PTData = GroupAndPaperReportController.FetchPartTerm(IPTG.ProgrammeId, IPTG.AcademicYearId, IPTG.SpecialisationId);


                if (PTData != null)
                {
                    return Return.returnHttp("200", PTData, null);
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

        #region Group Report Get by ProgrammeId
        [HttpPost]
        public HttpResponseMessage GroupReportGetbyProgrammeId(IncProgInstPartTermGroup IPTG)
        {
            try
            {
                List<PartTerm> PTData = GroupAndPaperReportController.FetchPartTerm(IPTG.ProgrammeId, IPTG.AcademicYearId, IPTG.SpecialisationId);
                List<GroupPT> GroupData = GroupAndPaperReportController.FetchGroup(IPTG.ProgrammeId, IPTG.AcademicYearId, IPTG.SpecialisationId);
                List<PaperPT> PaperData = GroupAndPaperReportController.FetchPaper(IPTG.ProgrammeId, IPTG.AcademicYearId, IPTG.SpecialisationId);
                if (PTData.Any())
                {
                    if (GroupData.Any())
                    {
                        IncProgInstPartTermGroup OtherDetail = new IncProgInstPartTermGroup();
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
                            IsPaper = e.IsPaper
                        }).ToList();

                        //===============================================================================
                        List<GroupPTDX> GroupItems = GroupData.Select(e => new GroupPTDX
                        {
                            Id = e.Id,
                            ProgrammeInstancePartTermId = e.ProgrammeInstancePartTermId,
                            ParentGroupId = e.ParentGroupId,
                            InstancePartTermName = e.InstancePartTermName,
                            GroupName = e.GroupName,
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
                            IsPaper = e.IsPaper
                        }).ToList();
                        foreach (GroupPTDX Group in GroupItems)
                        {
                            List<GroupPTDX> PaperList = PaperItems.Where(e => e.GroupId == Group.Id).ToList();
                            if (PaperList.Any())
                            {
                                Group.items = PaperList;
                            }
                        }
                        foreach (GroupPTDX Group in GroupItems)
                        {
                            var children = GroupItems.Where(e => e.ParentGroupId == Group.Id && e.ParentGroupId != e.Id).ToList();
                            if (children.Any())
                            {
                                Group.items = children;
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
                            IsCompleted = e.IsCompleted,
                            GroupCompleteStatus = e.GroupCompleteStatus,
                            PaperCompleteStatus = e.PaperCompleteStatus,
                            IsPartTerm = true,
                            expanded = true,
                            ItemType = "Part Term"
                        }).ToList();
                        List<GroupPTDX> GroupList = GroupItems.Where(e => e.ParentGroupId == e.Id).ToList();
                        foreach (PartTermDX PT in PartTermItems)
                        {
                            List<GroupPTDX> GPT = GroupList.Where(e => e.ProgrammeInstancePartTermId == PT.Id).ToList();
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
                    else { return Return.returnHttp("201", "Structure Report Incomplete", null); }
                }
                else { return Return.returnHttp("201", "No data to display", null); }
                
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Assessment Report Get by ProgrammeId
        [HttpPost]
        public HttpResponseMessage AssessmentReportGetbyProgrammeId(IncProgInstPartTermGroup IPTG)
        {
            try
            {
                List<PartTerm> PTData = GroupAndPaperReportController.FetchPartTerm(IPTG.ProgrammeId, IPTG.AcademicYearId, IPTG.SpecialisationId);
                List<PaperPT> PaperData = GroupAndPaperReportController.FetchPaper(IPTG.ProgrammeId, IPTG.AcademicYearId, IPTG.SpecialisationId);
                List<PaperDetail> PaperDetailData = GroupAndPaperReportController.FetchPaperDetail(IPTG.ProgrammeId, IPTG.AcademicYearId, IPTG.SpecialisationId);
                List<PaperATDetail> PaperATDetailData = GroupAndPaperReportController.FetchPaperATDetail(IPTG.ProgrammeId, IPTG.AcademicYearId, IPTG.SpecialisationId);
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
                    IsCompleted = e.IsCompleted,
                    GroupCompleteStatus = e.GroupCompleteStatus,
                    IsCompletedAssessment = e.IsCompletedAssessment,
                    AssessmentCompleteStatus = e.AssessmentCompleteStatus,
                    PaperCompleteStatus = e.PaperCompleteStatus,
                    TLMcheck = e.TLMcheck,
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
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region FetchPartTermByProgrammePartId
        public static List<PartTerm> FetchPartTermByProgrammePartId(Int64 ProgrammeInstancePartId, Int32 SpecialisationId, Int32 InstituteId)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            List<PartTerm> PTdata = new List<PartTerm>();
            SqlCommand cmd = new SqlCommand();
            
            cmd = new SqlCommand("GroupAndPaperReportGetProgrammePartTermByProgpartId", Con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ProgrammeInstancePartId", ProgrammeInstancePartId);
            cmd.Parameters.AddWithValue("@SpecialisationId", SpecialisationId);
            cmd.Parameters.AddWithValue("@InstituteId", InstituteId);

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

        #region FetchPartTerm
        public static List<PartTerm> FetchPartTerm(Int32 ProgrammeId, Int32 AcademicYearId, Int32 SpecialisationId)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            List<PartTerm> PTdata = new List<PartTerm>();
            SqlCommand cmd = new SqlCommand();
            if (SpecialisationId == 0)
            {
                cmd = new SqlCommand("IncProgrammeInstancePartTermGetByProgrammeId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeId", ProgrammeId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            }
            else
            {
                cmd = new SqlCommand("IncProgrammeInstancePartTermGetBySpecialisationId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeId", ProgrammeId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                cmd.Parameters.AddWithValue("@SpecialisationId", SpecialisationId);
            }


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
                    PT.IsCompleted = (Convert.ToString(Dt.Rows[i]["IsCompleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsCompleted"]);
                    PT.PaperCompleteStatus = (Convert.ToString(Dt.Rows[i]["PaperCompleteStatus"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["PaperCompleteStatus"]);
                    PT.IsCompletedAssessment = (Convert.ToString(Dt.Rows[i]["IsCompletedAssessment"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsCompletedAssessment"]);
                    PT.AssessmentCompleteStatus = (Convert.ToString(Dt.Rows[i]["AssessmentCompleteStatus"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["AssessmentCompleteStatus"]);
                    PT.GroupCompleteStatus = (Convert.ToString(Dt.Rows[i]["GroupCompleteStatus"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["GroupCompleteStatus"]);
                    PT.TLMcheck = (Convert.ToString(Dt.Rows[i]["TLMcheck"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["TLMcheck"]);
                    PTdata.Add(PT);
                }
            }
            return PTdata;
        }
        #endregion

        #region FetchGroup
        public static List<GroupPT> FetchGroup(Int32 ProgrammeId, Int32 AcademicYearId, Int32 SpecialisationId)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            List<GroupPT> PTGdata = new List<GroupPT>();

            SqlCommand cmd = new SqlCommand("GroupAndPaperReportGetGroupByProgrammeId", Con);
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
                    if (PTG.ContainsGroup == true)
                    {
                        PTG.GroupName = PTG.GroupName + ": Max = " + PTG.MaxSubGroups + ", Min = " + PTG.MinSubGroups;
                    }
                    else
                    {
                        PTG.GroupName = PTG.GroupName + ": Max = " + PTG.MaxPapers + ", Min = " + PTG.MinPapers;
                    }
                    PTGdata.Add(PTG);
                }
            }
            return PTGdata;
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

        #region FetchGroupSelect
        public static List<GroupPT> FetchGroupSelect(Int64 PartTerm, Int64 PRN)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            List<GroupPT> PTGdata = new List<GroupPT>();
            
            
            SqlCommand cmd = new SqlCommand("GroupAndPaperReportGetGroup", Con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PartTerm", PartTerm);
            cmd.Parameters.AddWithValue("@PRN", PRN);

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
                    PTG.PreferanceGroupCheck = (((Dt.Rows[i]["PreferanceGroupCheck"]).ToString()).IsEmpty()) ? false : Convert.ToBoolean(Dt.Rows[i]["PreferanceGroupCheck"]);
                    PTG.IncPreferanceGroupMapId = (((Dt.Rows[i]["IncPreferanceGroupMapId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["IncPreferanceGroupMapId"]);
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
        public static List<PaperPT> FetchPaperSelect(Int64 PartTerm, Int64 PRN)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            List<PaperPT> Paperdata = new List<PaperPT>();
         

            SqlCommand cmd = new SqlCommand("GroupAndPaperReportGetPaper", Con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PartTerm", PartTerm);
            cmd.Parameters.AddWithValue("@PRN", PRN);

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
                    Paper.PaperCheck = (Convert.ToString(Dt.Rows[i]["PaperSelected"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["PaperSelected"]);
                    Paperdata.Add(Paper);
                }
            }
            return Paperdata;
        }
        #endregion

        #region Group Select Get 
        [HttpPost]
        public HttpResponseMessage GroupSelectGet(StudentList IPTG)
        {
            try
            {
                List<GroupPT> GroupData = GroupAndPaperReportController.FetchGroupSelect(IPTG.Id, IPTG.PRN);
                List<PaperPT> PaperData = GroupAndPaperReportController.FetchPaperSelect(IPTG.Id, IPTG.PRN);

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
                    ProgrammeInstancePartTermId = e.ProgrammeInstancePartTermId,
                    Credits = e.Credits,
                    ATDetail = e.ATDetail,
                    expanded = true,
                    ItemType = e.ItemType,
                    IsPaper = e.IsPaper,
                    icon = "icofont icofont-file-alt"


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

                    IncPreferanceGroupMapId = e.IncPreferanceGroupMapId,
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
                        var Groupd1 = GroupList.Where(e => e.ParentGroupId == Group.Id && e.ParentGroupId != e.Id).ToList();
                        if (Groupd1.Any())
                        {
                            Group.children = Groupd1;
                        }
                    }
                    GroupFinalData = GroupList.Where(e => e.ParentGroupId == e.Id).ToList();
                }
                else
                {
                    foreach (GroupSelect Group in GroupItems)
                    {
                        var Groupd12 = GroupItems.Where(e => e.ParentGroupId == Group.Id && e.ParentGroupId != e.Id).ToList();
                        if (Groupd12.Any())
                        {
                            Group.children = Groupd12;
                        }
                    }
                    GroupFinalData = GroupItems.Where(e => e.ParentGroupId == e.Id).ToList();

                }
                PartTerm PT = new PartTerm();
                PT.Id = IPTG.Id;
                PT.InstancePartTermName = IPTG.InstancePartTermName;
                PT.state = new state();
                    PT.state.opened = true;
                    PT.state.disabled = true;
                    PT.text = GroupFinalData[0].InstancePartTermName;
                    var children = GroupFinalData.Where(e => e.ProgrammeInstancePartTermId == PT.Id).ToList();
                    if (children.Any())
                    {
                        PT.children = children;
                    }
                
                GPreportdata Data = new GPreportdata();
                Data.Parent = PT;
                Data.MinMaxData = GroupData;
                Data.PRN = IPTG.PRN;
                Data.StudentName = IPTG.NameAsPerMarksheet;
                Data.ProgrammeInstancePartTermId = IPTG.Id;

                return Return.returnHttp("200", Data, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region FetchGroupBulkSelect
        public static List<GroupPT> FetchGroupBulkSelect(Int64 PartTerm, Int32 InstituteId)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            List<GroupPT> PTGdata = new List<GroupPT>();
            
            SqlCommand cmd = new SqlCommand("GroupAndPaperReportGetGroupBulkSelect", Con);
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

        #region FetchPaperBulkSelect 
        public static List<PaperPT> FetchPaperBulkSelect(Int64 PartTerm, Int32 InstituteId)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            List<PaperPT> Paperdata = new List<PaperPT>();
            

            SqlCommand cmd = new SqlCommand("GroupAndPaperReportGetPaperBulkSelect", Con);
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
                    Paperdata.Add(Paper);
                }
            }
            return Paperdata;
        }
        #endregion

        #region Get Bulk Tree Select
        [HttpPost]
        public HttpResponseMessage GetBulkTreeSelect(StudentList IPTG)
        {
            try
            {
                List<GroupPT> GroupData = GroupAndPaperReportController.FetchGroupBulkSelect(IPTG.ProgrammeInstancePartTermId, IPTG.InstituteId);
                List<PaperPT> PaperData = GroupAndPaperReportController.FetchPaperBulkSelect(IPTG.ProgrammeInstancePartTermId, IPTG.InstituteId);

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
                    icon = "icofont icofont-file-alt"


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
                PT.text = GroupFinalData[0].InstancePartTermName;
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
                Data.ProgrammeInstancePartTermId = IPTG.ProgrammeInstancePartTermId;

                return Return.returnHttp("200", Data, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region StudentGet
        //[HttpPost]
        //public HttpResponseMessage StudentGet(IncProgInstPartTermGroup IPTG)
        //{
        //    try
        //    {
        //        List<PartTerm> PTData = GroupAndPaperReportController.FetchPartTermByProgrammePartId(IPTG.ProgrammeInstancePartId, IPTG.SpecialisationId, IPTG.InstituteId);
        //        if (PTData.Any())
        //        {


        //            List<StudentList> PTGdata = new List<StudentList>();


        //            SqlCommand cmd = new SqlCommand("GroupAndPaperReportGetStudent", Con);
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            cmd.Parameters.AddWithValue("@PartTermId", PTData[0].Id);
        //            Int32 index = 1;

        //            Da.SelectCommand = cmd;
        //            Da.Fill(Dt);
        //            if (Dt.Rows.Count > 0)
        //            {
        //                for (int i = 0; i < Dt.Rows.Count; i++)
        //                {
        //                    StudentList PTG = new StudentList();

        //                    PTG.Id = (((Dt.Rows[i]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["Id"]);
        //                    PTG.PRN = (((Dt.Rows[i]["PRN"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["PRN"]);
        //                    PTG.ProgrammeInstancePartTermId = (((Dt.Rows[i]["ProgrammeInstancePartTermId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["ProgrammeInstancePartTermId"]);
        //                    PTG.StudentAdmissionId = (((Dt.Rows[i]["StudentAdmissionId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["StudentAdmissionId"]);
        //                    PTG.AcademicYearId = (((Dt.Rows[i]["AcademicYearId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["AcademicYearId"]);
        //                    PTG.NameAsPerMarksheet = (Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"]);
        //                    List<PartTerm> PTList = new List<PartTerm>();
        //                    foreach (PartTerm PT in PTData)
        //                    {
        //                        SqlCommand cmd1 = new SqlCommand("GroupAndPaperReportGetStudentCheckPaper", Con);
        //                        cmd1.CommandType = CommandType.StoredProcedure;

        //                        cmd1.Parameters.AddWithValue("@PartTermId", PT.Id);
        //                        cmd1.Parameters.AddWithValue("@PRN", PTG.PRN);
        //                        SqlDataAdapter Da1 = new SqlDataAdapter();
        //                        DataTable Dt1 = new DataTable();
        //                        Da1.SelectCommand = cmd1;
        //                        Da1.Fill(Dt1);
        //                        if (Dt1.Rows.Count > 0)
        //                        {
        //                            for (int j = 0; j < Dt1.Rows.Count; j++)
        //                            {
        //                                PartTerm P = new PartTerm();
        //                                P.IsPaperSelected = (((Dt1.Rows[j]["IsPaperSelected"]).ToString()).IsEmpty()) ? false : Convert.ToBoolean(Dt1.Rows[j]["IsPaperSelected"]);
        //                                P.PRN = PTG.PRN;
        //                                P.InstancePartTermName = PT.InstancePartTermName;
        //                                P.Id = PT.Id;
        //                                PTList.Add(P);
        //                            }
        //                        }
        //                    }
        //                    PTG.PTList = PTList;
        //                    PTG.Index = index++;
        //                    PTGdata.Add(PTG);
        //                }
        //            }
        //            return Return.returnHttp("200", (PTGdata, PTData), null);
        //        }
        //        else
        //        {
        //            return Return.returnHttp("201", "Semester Is not launch or Paper is not attached to this Institute", null);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return Return.returnHttp("201", e.Message, null);
        //    }
        //}



        [HttpPost]
        public HttpResponseMessage StudentGet(IncProgInstPartTermGroup IPTG)
        {
            try
            {
                List<PartTerm> PTData = GroupAndPaperReportController.FetchPartTermByProgrammePartId(IPTG.ProgrammeInstancePartId, IPTG.SpecialisationId, IPTG.InstituteId);
                if (PTData.Any())
                {
                    List<StudentList> PTGdata = new List<StudentList>();

                    SqlCommand cmd = new SqlCommand("GroupAndPaperReportGetStudent1", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@InstituteId", IPTG.InstituteId);
                    cmd.Parameters.AddWithValue("@ProgrammeInstancePartId", IPTG.ProgrammeInstancePartId);
                    cmd.Parameters.AddWithValue("@SpecialisationId", IPTG.SpecialisationId);
                    Int32 index = 1;

                    Da.SelectCommand = cmd;
                    Da.Fill(Dt);
                    if (Dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            StudentList PTG = new StudentList();

                            PTG.PRN = (((Dt.Rows[i]["PRN"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["PRN"]);
                            PTG.StudentAdmissionId = (((Dt.Rows[i]["StudentAdmissionId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["StudentAdmissionId"]);
                            PTG.AcademicYearId = (((Dt.Rows[i]["AcademicYearId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["AcademicYearId"]);
                            PTG.NameAsPerMarksheet = (Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"]);
                            PTG.PartTerm1 = (((Dt.Rows[i]["PartTerm1"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["PartTerm1"]);
                            PTG.PartTerm2 = (((Dt.Rows[i]["PartTerm2"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["PartTerm2"]);
                            PTG.PartTerm3 = (((Dt.Rows[i]["PartTerm3"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["PartTerm3"]);
                            PTG.PartTermName1= (Convert.ToString(Dt.Rows[i]["PartTermName1"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["PartTermName1"]);
                            PTG.PartTermName2 = (Convert.ToString(Dt.Rows[i]["PartTermName2"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["PartTermName2"]);
                            PTG.PartTermName3 = (Convert.ToString(Dt.Rows[i]["PartTermName3"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["PartTermName3"]);
                            PTG.PaperSelected1 = (Convert.ToString(Dt.Rows[i]["PaperSelected1"])).IsEmpty()? false : Convert.ToBoolean(Dt.Rows[i]["PaperSelected1"]);
                            PTG.PaperSelected2 = (Convert.ToString(Dt.Rows[i]["PaperSelected2"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["PaperSelected2"]);
                            PTG.PaperSelected3 = (Convert.ToString(Dt.Rows[i]["PaperSelected3"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["PaperSelected3"]);
        
                            PTG.Index = index++;
                            PTGdata.Add(PTG);
                        }
                    }
                    return Return.returnHttp("200", (PTGdata, PTData), null);
                }
                else
                {
                    return Return.returnHttp("201", "Semester Is not launch or Paper is not attached to this Institute", null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region GroupSelectSave
        [HttpPost]
        public HttpResponseMessage GroupSelectSave(GPreportdata GAP)
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
                Int64 PRN = Convert.ToInt64(GAP.PRN);
                Int64 ProgrammeInstancePartTermId = 0;
                Int64 StudentAcademicId = Convert.ToInt64(GAP.StudentAcademicId);
                String ErrorMsg = "Kindly Select Paper as per Minimum / Maximum Condition in listed groups : ";
                Boolean flag = true;
                foreach (GroupSelect GP in FinalArray)
                {
                    foreach (GroupPT k in MinMaxData)
                    {
                        if (GP.IsPaper == true)
                        {
                            if ((k.MinPapers > k.Count || k.Count > k.MaxPapers) && GP.ParentGroupId == k.Id)
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
                    //Int64 ProgrammeInstancePartTermId = 0;
                    Int32 Count = 0;
                    //foreach (GroupSelect G in FinalArray)
                    for (int i = 0; i < FinalArray.Count(); i++)
                    {
                        ProgrammeInstancePartTermId = Convert.ToInt64(FinalArray[i].ProgrammeInstancePartTermId);
                        if (FinalArray[i].IsPaper == true)
                        {
                            PaperList.Rows.Add(FinalArray[i].ProgrammeInstancePartTermId, PRN, StudentAcademicId, FinalArray[i].PaperId, FinalArray[i].PaperCode, FinalArray[i].PaperName, FinalArray[i].MinMarks, FinalArray[i].MaxMarks, FinalArray[i].Credits, i, UserId, datetime);
                            PaperIdList = PaperIdList + Convert.ToString(FinalArray[i].PaperId) + ", ";
                            Count = Count + 1;
                        }
                    }
                    Int32 Count11 = GroupAndPaperReportController.SelectedPaperCheck(PaperList, 11, PRN, ProgrammeInstancePartTermId);

                    if (Count11 > 0) { return Return.returnHttp("201", "Prerequisite Condition not fulfilled of Within Semester - None Of This", null); }
                    else
                    {
                        Int32 Count12 = GroupAndPaperReportController.SelectedPaperCheck(PaperList, 12, PRN, ProgrammeInstancePartTermId);

                        if (Count12 > 0) { return Return.returnHttp("201", "Prerequisite Condition not fulfilled of Within Semester - All Of This", null); }
                        else
                        {
                            Int32 Count13 = GroupAndPaperReportController.SelectedPaperCheck(PaperList, 13, PRN, ProgrammeInstancePartTermId);
                            if (Count13 > 0) { return Return.returnHttp("201", "Prerequisite Condition not fulfilled of Within Semester - Any Of This", null); }
                            else
                            {
                                Int32 Count21 = GroupAndPaperReportController.SelectedPaperCheck(PaperList, 21, PRN, ProgrammeInstancePartTermId);
                                if (Count21 > 0) { return Return.returnHttp("201", "Prerequisite Condition not fulfilled of Across Semester - None Of This", null); }
                                else
                                {
                                    Int32 Count22 = GroupAndPaperReportController.SelectedPaperCheck(PaperList, 22, PRN, ProgrammeInstancePartTermId);
                                    if (Count22 > 0) { return Return.returnHttp("201", "Prerequisite Condition not fulfilled of Across Semester - All Of This", null); }
                                    else
                                    {
                                        Int32 Count23 = GroupAndPaperReportController.SelectedPaperCheck(PaperList, 23, PRN, ProgrammeInstancePartTermId);
                                        if (Count23 > 0) { return Return.returnHttp("201", "Prerequisite Condition not fulfilled of Across Semester -- Any Of This", null); }
                                        else
                                        {
                                            String msg = GroupAndPaperReportController.SelectedPaperSave(PaperList, Count, PRN, StudentAcademicId, UserId, datetime, PaperIdList, ProgrammeInstancePartTermId);
                                            if (msg == "TRUE")
                                            {
                                                return Return.returnHttp("200", (PRN, ProgrammeInstancePartTermId), null);
                                            }
                                            else
                                            {
                                                return Return.returnHttp("201", msg, null);
                                                //return Return.returnHttp("201", "Please try again", null);
                                            }

                                        }
                                    }
                                }
                            }
                        }
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

        #region GroupSelectBulkSave
        [HttpPost]
        public HttpResponseMessage GroupSelectBulkSave(GPreportdata GAP)
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
                String ErrorMsg = "Kindly Select Paper as per Minimum / Maximum Condition in listed groups : ";

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
                Int64 PRN = Convert.ToInt64(GAP.PRN);
                Int64 StudentAcademicId = Convert.ToInt64(GAP.StudentAcademicId);
                //Int64 ProgrammeInstancePartTermId = Convert.ToInt64(GAP.ProgrammeInstancePartTermId);
                Int32 InstituteId = Convert.ToInt32(GAP.InstituteId);
                Boolean flag = true;

                foreach (GroupSelect GP in FinalArray)
                {
                    foreach (GroupPT k in MinMaxData)
                    {
                        if (GP.IsPaper == true)
                        {
                            if ((k.MinPapers > k.Count || k.Count > k.MaxPapers) && GP.ParentGroupId == k.Id)
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
                    Int64 ProgrammeInstancePartTermId = 0;
                    foreach (GroupSelect G in FinalArray)
                    {
                        ProgrammeInstancePartTermId = Convert.ToInt64(G.ProgrammeInstancePartTermId);
                        if (G.IsPaper == true)
                        {
                            PaperList.Rows.Add(ProgrammeInstancePartTermId, PRN, StudentAcademicId, G.PaperId, G.PaperCode, G.PaperName, G.MinMarks, G.MaxMarks, G.Credits, 0, UserId, datetime);
                            PaperIdList = PaperIdList + Convert.ToString(G.PaperId) + ", ";
                        }
                    }
                    
                   
                    String msg = GroupAndPaperReportController.SelectedPaperBulkSave(PaperList, InstituteId, datetime, UserId, PaperIdList, ProgrammeInstancePartTermId);
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

        #region GetPreferenceGroup
        [HttpPost]
        public HttpResponseMessage GetPreferenceGroup()
        {
            try
            {

                List<MstPreferenceGroup> PGdata = new List<MstPreferenceGroup>();

                SqlCommand cmd = new SqlCommand("MstPreferenceGroupGet", Con);
                cmd.CommandType = CommandType.StoredProcedure;


                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstPreferenceGroup PG = new MstPreferenceGroup();

                        PG.Id = (((Dt.Rows[i]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["Id"]);
                        PG.GroupName = (Convert.ToString(Dt.Rows[i]["GroupName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["GroupName"]);

                        PGdata.Add(PG);
                    }
                }
                return Return.returnHttp("200", PGdata, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region FetchGroupPT 
        public static List<GroupPT> FetchGroupPT(Int64 PartTermId)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            List<GroupPT> PTGdata = new List<GroupPT>();

            SqlCommand cmd = new SqlCommand("GroupAndPaperReportGetGroupPT", Con);
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

        //#region GroupPTGet 
        //[HttpPost]
        //public HttpResponseMessage GroupPTGet(IncPreferanceGroupMap PGP)
        //{
        //    try
        //    {
        //        List<GroupPT> GroupData = GroupAndPaperReportController.FetchGroupPT(PGP.ProgrammeInstancePartTermId);

        //        List<GroupSelect> GroupFinalData = new List<GroupSelect>();
        //        List<GroupSelect> GroupItems = GroupData.Select(e => new GroupSelect
        //        {
        //            Id = e.Id,
        //            ProgrammeInstancePartTermId = e.ProgrammeInstancePartTermId,
        //            ParentGroupId = e.ParentGroupId,
        //            InstancePartTermName = e.InstancePartTermName,
        //            text = e.GroupName,
        //            ContainsGroup = e.ContainsGroup,
        //            MinPapers = e.MinPapers,
        //            MaxPapers = e.MaxPapers,
        //            MinSubGroups = e.MinSubGroups,
        //            MaxSubGroups = e.MaxSubGroups,
        //            FacultyName = e.FacultyName,
        //            ProgrammeName = e.ProgrammeName,
        //            BranchName = e.BranchName,
        //            SubChildCount = e.SubChildCount,
        //            expanded = true,
        //            ItemType = e.ItemType,

        //        }).ToList();
        //        foreach (GroupSelect Group in GroupItems)
        //        {
        //            Group.state = new state();
        //            Group.state.opened = true;
        //        }
        //        foreach (GroupSelect Group in GroupItems)
        //        {
        //            var children = GroupItems.Where(e => e.ParentGroupId == Group.Id && e.ParentGroupId != e.Id).ToList();
        //            if (children.Any())
        //            {
        //                Group.children = children;
        //            }
        //        }
        //        GroupFinalData = GroupItems.Where(e => e.ParentGroupId == e.Id).ToList();

        //        foreach (GroupSelect G in GroupFinalData)
        //        {
        //            G.state.disabled = true;
        //        }
        //        GPreportdata Data = new GPreportdata();
        //        Data.Parent = GroupFinalData;
        //        Data.MinMaxData = GroupData;

        //        return Return.returnHttp("200", Data, null);
        //    }
        //    catch (Exception e)
        //    {
        //        return Return.returnHttp("201", e.Message, null);
        //    }
        //}
        //#endregion

        #region PGPSelectSave
        [HttpPost]
        public HttpResponseMessage PGPSelectSave(GPreportdata GAP)
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

                List<GroupSelect> FinalArray = GAP.FinalArray;
                Int32 Count = Convert.ToInt32(FinalArray.Count);
                SqlCommand[] CMDarray = new SqlCommand[Count];
                String[] strMessage = new String[Count];
                String msg = "";
                int i = 0;
                Int32 PreferenceId = Convert.ToInt32(GAP.PreferenceId);
                Int64 ProgrammeInstancePartTermId = Convert.ToInt64(GAP.ProgrammeInstancePartTermId);
                foreach (GroupSelect G in FinalArray)
                {
                    Int64 GroupId = Convert.ToInt64(G.Id);
                    CMDarray[i] = new SqlCommand("IncPreferanceGroupMapAdd", Con, ST);
                    CMDarray[i].CommandType = CommandType.StoredProcedure;
                    CMDarray[i].Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                    CMDarray[i].Parameters.AddWithValue("@PreferenceId", PreferenceId);
                    CMDarray[i].Parameters.AddWithValue("@GroupId", GroupId);
                    CMDarray[i].Parameters.AddWithValue("@UserId", UserId);
                    CMDarray[i].Parameters.AddWithValue("@UserTime", datetime);
                    CMDarray[i].Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    CMDarray[i].Parameters["@Message"].Direction = ParameterDirection.Output;
                    i++;

                }


                Con.Open();
                ST = Con.BeginTransaction();
                try
                {
                    for (int j = 0; j < i; j++)
                    {
                        CMDarray[j].Transaction = ST;
                        int ans = CMDarray[j].ExecuteNonQuery();
                        msg = msg + ", " + Convert.ToString(CMDarray[j].Parameters["@Message"].Value);
                    }

                    ST.Commit();
                    return Return.returnHttp("200", msg, null);
                    //return Return.returnHttp("200", "Record Added", null);
                }
                catch (SqlException sqlError)
                {
                    ST.Rollback();
                    String strMessageErr = Convert.ToString(sqlError);
                    return Return.returnHttp("201", strMessageErr, null);
                    //return Return.returnHttp("201", "Record already exists", null);
                }

                //return Return.returnHttp("200", "Data Added Successfully", null);
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

        #region PGPDataListGet
        [HttpPost]
        public HttpResponseMessage PGPDataListGet()
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

                List<IncPreferanceGroupMap> PGPData = new List<IncPreferanceGroupMap>();

                SqlCommand cmd = new SqlCommand("IncPreferanceGroupMapGetFiltered", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        IncPreferanceGroupMap PGP = new IncPreferanceGroupMap();
                        PGP.PreferenceId = (((Dt.Rows[i]["PreferenceId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["PreferenceId"]);
                        PGP.ProgrammeInstancePartTermId = (((Dt.Rows[i]["ProgrammeInstancePartTermId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["ProgrammeInstancePartTermId"]);
                        PGP.InstancePartTermName = (Convert.ToString(Dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                        PGP.GroupName = (Convert.ToString(Dt.Rows[i]["GroupName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["GroupName"]);

                        PGPData.Add(PGP);
                    }
                }
                if (PGPData != null)
                    return Return.returnHttp("200", PGPData, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);

            }
            catch (Exception e)
            {

                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }

        #endregion 

        #region PGPDelete
        [HttpPost]
        public HttpResponseMessage PGPDelete(IncPreferanceGroupMap PGP)
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

                Int32 PreferenceId = Convert.ToInt32(PGP.PreferenceId);
                Int64 ProgrammeInstancePartTermId = Convert.ToInt64(PGP.ProgrammeInstancePartTermId);
                SqlCommand cmd = new SqlCommand("IncPreferanceGroupMapDelete", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@PreferenceId", PreferenceId);
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
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

        #region SelectedPaperCheck
        public static Int32 SelectedPaperCheck(DataTable PaperList, Int32 Flag, Int64 PRN, Int64 PTId)
        {

            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);

            SqlCommand cmd = new SqlCommand("PaperPrerequisiteCheck", Con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PaperList", PaperList);
            cmd.Parameters.AddWithValue("@PRN", PRN);
            cmd.Parameters.AddWithValue("@PTId", PTId);
            cmd.Parameters.AddWithValue("@Flag", Flag);
            //cmd.Parameters.AddWithValue("@PartTermId", PartTermId);
            SqlDataAdapter Da = new SqlDataAdapter();
            DataTable Dt = new DataTable();
            Da.SelectCommand = cmd;
            Da.Fill(Dt);
            Int32 Count = 0;
            if (Dt.Rows.Count > 0)
            {
                foreach (DataRow row in Dt.Rows)
                {
                    Count = (((row["Countdata"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(row["Countdata"]);
                }
            }
            return Count;
        }
        #endregion 

        #region SelectedPaperSave

        public static String SelectedPaperSave(DataTable PaperList, Int32 Count, Int64 PRN, Int64 StudentAcademicId, Int64 UserId, DateTime datetime, String PaperIdList, Int64 ProgrammeInstancePartTermId)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            try
            {

                SqlTransaction ST;
                SqlCommand CMDarray = new SqlCommand();
                String msg = "";
                
                foreach (DataRow dr in PaperList.Rows)
                {

                    CMDarray = new SqlCommand("StudentPartTermPaperMapAdd", Con);
                    CMDarray.CommandType = CommandType.StoredProcedure;
                    CMDarray.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                    CMDarray.Parameters.AddWithValue("@StudentPRN", PRN);
                    CMDarray.Parameters.AddWithValue("@StudentAcademicId", StudentAcademicId);
                    CMDarray.Parameters.AddWithValue("@PaperIdList", PaperIdList);
                    CMDarray.Parameters.AddWithValue("@PaperList", PaperList);
                    CMDarray.Parameters.AddWithValue("@Count", Count);
                    CMDarray.Parameters.AddWithValue("@UserId", UserId);
                    CMDarray.Parameters.AddWithValue("@UserTime", datetime);
                    CMDarray.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    CMDarray.Parameters["@Message"].Direction = ParameterDirection.Output;
                    

                }
                Con.Open();
                ST = Con.BeginTransaction();
                try
                {
                    CMDarray.Transaction = ST;
                    int ans = CMDarray.ExecuteNonQuery();
                    msg = Convert.ToString(CMDarray.Parameters["@Message"].Value);
                    

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

        #region SelectedPaperBulkSave

        public static String SelectedPaperBulkSave(DataTable PaperList, Int32 InstituteId, DateTime date, Int64 UserId, String PaperIdList, Int64 ProgrammeInstancePartTermId)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            try
            {

                SqlTransaction ST;
                Int32 Count = Convert.ToInt32(PaperList.Rows.Count);
                SqlCommand CMDarray = new SqlCommand();
                
                CMDarray = new SqlCommand("StudentPartTermPaperMapBulkAdd", Con);
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

        #region PTCompleteStatusTrue
        [HttpPost]
        public HttpResponseMessage PTCompleteStatusTrue(PartTerm PT)
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
                
                Int64 ProgrammeInstancePartTermId = Convert.ToInt64(PT.Id);
                
                SqlCommand cmd = new SqlCommand("IncProgInstPartTermCompleteStatusTrue", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                Con.Close();
                //return Return.returnHttp("200", strMessage.ToString(), null);
                return Return.returnHttp("200", "Data Verified", null);


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
        #region PTAssessmentCompleteStatusTrue
        [HttpPost]
        public HttpResponseMessage PTAssessmentCompleteStatusTrue(PartTerm PT)
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

                Int64 ProgrammeInstancePartTermId = Convert.ToInt64(PT.Id);

                SqlCommand cmd = new SqlCommand("IncProgInstPartTermAssessmentCompleteStatusTrue", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                Con.Close();
                //return Return.returnHttp("200", strMessage.ToString(), null);
                return Return.returnHttp("200", "Data Verified", null);


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



        // Paper - Student Report
        #region GetPaperListByPTIdandInstId
        [HttpPost]
        public HttpResponseMessage GetPaperListByPTIdandInstId(GPreportdata ObjProg)
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

                List<PaperPT> Paperdata = new List<PaperPT>();

                SqlCommand cmd = new SqlCommand("GroupAndPaperReportGetPaperList", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ObjProg.ProgrammeInstancePartTermId);
                cmd.Parameters.AddWithValue("@InstituteId", ObjProg.InstituteId);
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
                        Paper.PaperId = (((Dt.Rows[i]["PaperId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["PaperId"]);
                        Paper.ProgrammeInstancePartTermId = (((Dt.Rows[i]["ProgrammeInstancePartTermId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["ProgrammeInstancePartTermId"]);

                        Paper.PaperName = (((Dt.Rows[i]["PaperName"]).ToString()).IsEmpty()) ? "Not Defined" : Convert.ToString(Dt.Rows[i]["PaperName"]);



                        Paperdata.Add(Paper);
                    }
                }
                if (Paperdata != null)
                    return Return.returnHttp("200", Paperdata, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);

            }
            catch (Exception e)
            {

                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region GetStudentListByPaperIdandInstId
        [HttpPost]
        public HttpResponseMessage GetStudentListByPaperIdandInstId(StudentList ObjProg)
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

                List<StudentList> StudentData = new List<StudentList>();

                SqlCommand cmd = new SqlCommand("GroupAndPaperReportGetStudentList", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ObjProg.ProgrammeInstancePartTermId);
                cmd.Parameters.AddWithValue("@InstituteId", ObjProg.InstituteId);
                cmd.Parameters.AddWithValue("@PaperId", ObjProg.PaperId);

                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                Int32 count = 0;
                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        StudentList Student = new StudentList();
                        Student.Id = (((Dt.Rows[i]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["Id"]);
                        Student.PaperId = (((Dt.Rows[i]["PaperId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["PaperId"]);
                        Student.ProgrammeInstancePartTermId = (((Dt.Rows[i]["ProgrammeInstancePartTermId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["ProgrammeInstancePartTermId"]);
                        Student.PRN = (((Dt.Rows[i]["PRN"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["PRN"]);
                        Student.PaperName = (((Dt.Rows[i]["PaperName"]).ToString()).IsEmpty()) ? "Not Defined" : Convert.ToString(Dt.Rows[i]["PaperName"]);
                        Student.NameAsPerMarksheet = (((Dt.Rows[i]["NameAsPerMarksheet"]).ToString()).IsEmpty()) ? "Not Defined" : Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"]);
                        Student.EmailId = (((Dt.Rows[i]["EmailId"]).ToString()).IsEmpty()) ? "Not Defined" : Convert.ToString(Dt.Rows[i]["EmailId"]);
                        Student.MobileNo = (((Dt.Rows[i]["MobileNo"]).ToString()).IsEmpty()) ? "Not Defined" : Convert.ToString(Dt.Rows[i]["MobileNo"]);

                        /* Start - Added by Mohini - 18 Jan 2022 */
                        Student.PaperCode = (((Dt.Rows[i]["PaperCode"]).ToString()).IsEmpty()) ? "Not Defined" : Convert.ToString(Dt.Rows[i]["PaperCode"]);
                        Student.SeatNumber = (((Dt.Rows[i]["SeatNumber"]).ToString()).IsEmpty()) ? "NA" : Convert.ToString(Dt.Rows[i]["SeatNumber"]);
                 
                        count = count + 1;
                        Student.IndexId = count;
                        /* End - Added by Mohini - 18 Jan 2022 */

                        StudentData.Add(Student);
                    }
                }
                if (StudentData.Any())
                    return Return.returnHttp("200", StudentData, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);

            }
            catch (Exception e)
            {

                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion


        // Start - Mohini's Code for Show Paper List in Academic On 19 Jan 2022

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
                SqlCommand cmd = new SqlCommand("MstInstituteGet", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<ApplicationStatistics> ObjLstInst = new List<ApplicationStatistics>();
                //Dt.Rows.Add("0", "All Institute");
                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        ApplicationStatistics ObjInst = new ApplicationStatistics();
                        ObjInst.Id = Convert.ToInt32(dr["Id"]);
                        ObjInst.InstituteName = (dr["InstituteName"].ToString());

                        ObjLstInst.Add(ObjInst);
                    }

                }

                return Return.returnHttp("200", ObjLstInst, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        // End - Mohini's Code for Show Paper List in Academic On 19 Jan 2022
        #region Group Select Get Edit
        [HttpPost]
        public HttpResponseMessage GroupSelectGetEdit(StudentList IPTG)
        {
            try
            {
                List<GroupPT> GroupData = GroupAndPaperReportController.FetchGroupSelect(IPTG.Id, IPTG.PRN);
                List<PaperPT> PaperData = GroupAndPaperReportController.FetchPaperSelect(IPTG.Id, IPTG.PRN);

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
                    ProgrammeInstancePartTermId = e.ProgrammeInstancePartTermId,
                    Credits = e.Credits,
                    ATDetail = e.ATDetail,
                    expanded = true,
                    ItemType = e.ItemType,
                    IsPaper = e.IsPaper,
                    PaperCheck = e.PaperCheck,
                    icon = "icofont icofont-file-alt"


                }).ToList();
                foreach (GroupSelect p in PaperItems)
                {
                    if (p.PaperCheck == true)
                    {
                        p.state = new state();
                        
                        p.state.selected = true;
                    }
                }
                
                List<GroupSelect> FinalArray = PaperItems.Where(e => e.PaperCheck == true).ToList();
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

                    IncPreferanceGroupMapId = e.IncPreferanceGroupMapId,
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
                        var Groupd1 = GroupList.Where(e => e.ParentGroupId == Group.Id && e.ParentGroupId != e.Id).ToList();
                        if (Groupd1.Any())
                        {
                            Group.children = Groupd1;
                        }
                    }
                    GroupFinalData = GroupList.Where(e => e.ParentGroupId == e.Id).ToList();
                }
                else
                {
                    foreach (GroupSelect Group in GroupItems)
                    {
                        var Groupd12 = GroupItems.Where(e => e.ParentGroupId == Group.Id && e.ParentGroupId != e.Id).ToList();
                        if (Groupd12.Any())
                        {
                            Group.children = Groupd12;
                        }
                    }
                    GroupFinalData = GroupItems.Where(e => e.ParentGroupId == e.Id).ToList();

                }
                PartTerm PT = new PartTerm();
                PT.Id = IPTG.Id;
                PT.InstancePartTermName = IPTG.InstancePartTermName;
                PT.state = new state();
                PT.state.opened = true;
                PT.state.disabled = true;
                PT.text = GroupFinalData[0].InstancePartTermName;
                var children = GroupFinalData.Where(e => e.ProgrammeInstancePartTermId == PT.Id).ToList();
                if (children.Any())
                {
                    PT.children = children;
                }
                foreach(GroupPT G in GroupData)
                {
                    G.Count = PaperItems.Count(e => e.ParentGroupId == G.Id && e.PaperCheck == true);
                }
                GPreportdata Data = new GPreportdata();
                Data.Parent = PT;
                Data.MinMaxData = GroupData;
                Data.PRN = IPTG.PRN;
                Data.StudentName = IPTG.NameAsPerMarksheet;
                Data.ProgrammeInstancePartTermId = IPTG.Id;
                Data.FinalArray = FinalArray;

                return Return.returnHttp("200", Data, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion
        #region GroupSelectEditSave
        [HttpPost]
        public HttpResponseMessage GroupSelectEditSave(GPreportdata GAP)
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
                Int64 UserId = Convert.ToInt64(res.ToString()); //res.ToString()


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
                PaperList.Columns.Add(new DataColumn("Credits", typeof(decimal)));
                PaperList.Columns.Add(new DataColumn("Counter", typeof(Int32)));
                PaperList.Columns.Add(new DataColumn("UserId", typeof(Int64)));
                PaperList.Columns.Add(new DataColumn("UserTime", typeof(DateTime)));

                List<GroupSelect> FinalArray = GAP.FinalArray;
                List<GroupPT> MinMaxData = GAP.MinMaxData;
                //List<String> Sample = new List<String>();
                Int64 PRN = Convert.ToInt64(GAP.PRN);
                Int64 ProgrammeInstancePartTermId = 0;
                Int64 StudentAcademicId = Convert.ToInt64(GAP.StudentAcademicId);
                String ErrorMsg = "Kindly Select Paper as per Minimum / Maximum Condition in listed groups : ";
                Boolean flag = true;
                foreach (GroupSelect GP in FinalArray)
                {
                    foreach (GroupPT k in MinMaxData)
                    {
                        if (GP.IsPaper == true)
                        {
                            if ((k.MinPapers > k.Count || k.Count > k.MaxPapers) && GP.ParentGroupId == k.Id)
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
                    
                    Int32 Count = 0;
                    //foreach (GroupSelect G in FinalArray)
                    for (int i = 0; i < FinalArray.Count(); i++)
                    {
                        ProgrammeInstancePartTermId = Convert.ToInt64(FinalArray[i].ProgrammeInstancePartTermId);
                        if (FinalArray[i].IsPaper == true)
                        {
                            PaperList.Rows.Add(FinalArray[i].ProgrammeInstancePartTermId, PRN, StudentAcademicId, FinalArray[i].PaperId, FinalArray[i].PaperCode, FinalArray[i].PaperName, FinalArray[i].MinMarks, FinalArray[i].MaxMarks, FinalArray[i].Credits, i, UserId, datetime);
                            PaperIdList = PaperIdList + Convert.ToString(FinalArray[i].PaperId) + ", ";
                            Count = Count + 1;
                        }
                    }
                    Int32 Count11 = GroupAndPaperReportController.SelectedPaperCheck(PaperList, 11, PRN, ProgrammeInstancePartTermId);

                    if (Count11 > 0) { return Return.returnHttp("201", "Prerequisite Condition not fulfilled of Within Semester - None Of This", null); }
                    else
                    {
                        Int32 Count12 = GroupAndPaperReportController.SelectedPaperCheck(PaperList, 12, PRN, ProgrammeInstancePartTermId);

                        if (Count12 > 0) { return Return.returnHttp("201", "Prerequisite Condition not fulfilled of Within Semester - All Of This", null); }
                        else
                        {
                            Int32 Count13 = GroupAndPaperReportController.SelectedPaperCheck(PaperList, 13, PRN, ProgrammeInstancePartTermId);
                            if (Count13 > 0) { return Return.returnHttp("201", "Prerequisite Condition not fulfilled of Within Semester - Any Of This", null); }
                            else
                            {
                                Int32 Count21 = GroupAndPaperReportController.SelectedPaperCheck(PaperList, 21, PRN, ProgrammeInstancePartTermId);
                                if (Count21 > 0) { return Return.returnHttp("201", "Prerequisite Condition not fulfilled of Across Semester - None Of This", null); }
                                else
                                {
                                    Int32 Count22 = GroupAndPaperReportController.SelectedPaperCheck(PaperList, 22, PRN, ProgrammeInstancePartTermId);
                                    if (Count22 > 0) { return Return.returnHttp("201", "Prerequisite Condition not fulfilled of Across Semester - All Of This", null); }
                                    else
                                    {
                                        Int32 Count23 = GroupAndPaperReportController.SelectedPaperCheck(PaperList, 23, PRN, ProgrammeInstancePartTermId);
                                        if (Count23 > 0) { return Return.returnHttp("201", "Prerequisite Condition not fulfilled of Across Semester - Any Of This", null); }
                                        else
                                        {
                                            String msg = GroupAndPaperReportController.SelectedPaperEditSave(PaperList, Count, PRN, StudentAcademicId, UserId, datetime, PaperIdList, ProgrammeInstancePartTermId);
                                            if (msg == "TRUE")
                                            {
                                                return Return.returnHttp("200", (PRN, ProgrammeInstancePartTermId), null);
                                            }
                                            else
                                            {
                                                return Return.returnHttp("201", msg, null);
                                                //return Return.returnHttp("201", "Please try again", null);
                                            }

                                        }
                                    }
                                }
                            }
                        }
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
        #region SelectedPaperEditSave

        public static String SelectedPaperEditSave(DataTable PaperList, Int32 Count, Int64 PRN, Int64 StudentAcademicId, Int64 UserId, DateTime datetime, String PaperIdList, Int64 ProgrammeInstancePartTermId)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            try
            {

                SqlTransaction ST;
                SqlCommand CMDarray = new SqlCommand();
                String msg = "";

                foreach (DataRow dr in PaperList.Rows)
                {

                    CMDarray = new SqlCommand("StudentPartTermPaperMapEdit", Con);
                    CMDarray.CommandType = CommandType.StoredProcedure;
                    CMDarray.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                    CMDarray.Parameters.AddWithValue("@StudentPRN", PRN);
                    CMDarray.Parameters.AddWithValue("@StudentAcademicId", StudentAcademicId);
                    CMDarray.Parameters.AddWithValue("@PaperIdList", PaperIdList);
                    CMDarray.Parameters.AddWithValue("@PaperList", PaperList);
                    CMDarray.Parameters.AddWithValue("@Count", Count);
                    CMDarray.Parameters.AddWithValue("@UserId", UserId);
                    CMDarray.Parameters.AddWithValue("@UserTime", datetime);
                    CMDarray.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    CMDarray.Parameters["@Message"].Direction = ParameterDirection.Output;

                }
                Con.Open();
                ST = Con.BeginTransaction();
                try
                {
                    CMDarray.Transaction = ST;
                    int ans = CMDarray.ExecuteNonQuery();
                    msg = Convert.ToString(CMDarray.Parameters["@Message"].Value);


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

        #region GroupSelectEditSaveByStudent
        [HttpPost]
        public HttpResponseMessage GroupSelectEditSaveByStudent(GPreportdata GAP)
        {
            try
            {
                Int64 UserId = Convert.ToInt64(GAP.PRN);


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
                PaperList.Columns.Add(new DataColumn("Credits", typeof(decimal)));
                PaperList.Columns.Add(new DataColumn("Counter", typeof(Int32)));
                PaperList.Columns.Add(new DataColumn("UserId", typeof(Int64)));
                PaperList.Columns.Add(new DataColumn("UserTime", typeof(DateTime)));

                List<GroupSelect> FinalArray = GAP.FinalArray;
                List<GroupPT> MinMaxData = GAP.MinMaxData;
                //List<String> Sample = new List<String>();
                Int64 PRN = Convert.ToInt64(GAP.PRN);
                Int64 ProgrammeInstancePartTermId = 0;
                Int64 StudentAcademicId = Convert.ToInt64(GAP.StudentAcademicId);
                String ErrorMsg = "Kindly Select Paper as per Minimum / Maximum Condition in listed groups : ";
                Boolean flag = true;
                foreach (GroupSelect GP in FinalArray)
                {
                    foreach (GroupPT k in MinMaxData)
                    {
                        if (GP.IsPaper == true)
                        {
                            if ((k.MinPapers > k.Count || k.Count > k.MaxPapers) && GP.ParentGroupId == k.Id)
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

                    Int32 Count = 0;
                    //foreach (GroupSelect G in FinalArray)
                    for (int i = 0; i < FinalArray.Count(); i++)
                    {
                        ProgrammeInstancePartTermId = Convert.ToInt64(FinalArray[i].ProgrammeInstancePartTermId);
                        if (FinalArray[i].IsPaper == true)
                        {
                            PaperList.Rows.Add(FinalArray[i].ProgrammeInstancePartTermId, PRN, StudentAcademicId, FinalArray[i].PaperId, FinalArray[i].PaperCode, FinalArray[i].PaperName, FinalArray[i].MinMarks, FinalArray[i].MaxMarks, FinalArray[i].Credits, i, UserId, datetime);
                            PaperIdList = PaperIdList + Convert.ToString(FinalArray[i].PaperId) + ", ";
                            Count = Count + 1;
                        }
                    }
                    Int32 Count11 = GroupAndPaperReportController.SelectedPaperCheck(PaperList, 11, PRN, ProgrammeInstancePartTermId);

                    if (Count11 > 0) { return Return.returnHttp("201", "Prerequisite Condition not fulfilled of Within Semester - None Of This", null); }
                    else
                    {
                        Int32 Count12 = GroupAndPaperReportController.SelectedPaperCheck(PaperList, 12, PRN, ProgrammeInstancePartTermId);

                        if (Count12 > 0) { return Return.returnHttp("201", "Prerequisite Condition not fulfilled of Within Semester - All Of This", null); }
                        else
                        {
                            Int32 Count13 = GroupAndPaperReportController.SelectedPaperCheck(PaperList, 13, PRN, ProgrammeInstancePartTermId);
                            if (Count13 > 0) { return Return.returnHttp("201", "Prerequisite Condition not fulfilled of Within Semester - Any Of This", null); }
                            else
                            {
                                Int32 Count21 = GroupAndPaperReportController.SelectedPaperCheck(PaperList, 21, PRN, ProgrammeInstancePartTermId);
                                if (Count21 > 0) { return Return.returnHttp("201", "Prerequisite Condition not fulfilled of Across Semester - None Of This", null); }
                                else
                                {
                                    Int32 Count22 = GroupAndPaperReportController.SelectedPaperCheck(PaperList, 22, PRN, ProgrammeInstancePartTermId);
                                    if (Count22 > 0) { return Return.returnHttp("201", "Prerequisite Condition not fulfilled of Across Semester - All Of This", null); }
                                    else
                                    {
                                        Int32 Count23 = GroupAndPaperReportController.SelectedPaperCheck(PaperList, 23, PRN, ProgrammeInstancePartTermId);
                                        if (Count23 > 0) { return Return.returnHttp("201", "Prerequisite Condition not fulfilled of Across Semester - Any Of This", null); }
                                        else
                                        {
                                            String msg = GroupAndPaperReportController.SelectedPaperEditSaveByStudent(PaperList, Count, PRN, StudentAcademicId, UserId, datetime, PaperIdList, ProgrammeInstancePartTermId);
                                            if (msg == "TRUE")
                                            {
                                                return Return.returnHttp("200", (PRN, ProgrammeInstancePartTermId), null);
                                            }
                                            else
                                            {
                                                return Return.returnHttp("201", msg, null);
                                                //return Return.returnHttp("201", "Please try again", null);
                                            }

                                        }
                                    }
                                }
                            }
                        }
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

        #region SelectedPaperEditSaveByStudent
        public static String SelectedPaperEditSaveByStudent(DataTable PaperList, Int32 Count, Int64 PRN, Int64 StudentAcademicId, Int64 UserId, DateTime datetime, String PaperIdList, Int64 ProgrammeInstancePartTermId)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            try
            {

                SqlTransaction ST;
                SqlCommand CMDarray = new SqlCommand();
                String msg = "";

                foreach (DataRow dr in PaperList.Rows)
                {

                    CMDarray = new SqlCommand("StudentPartTermPaperMapAddByStudent", Con);
                    CMDarray.CommandType = CommandType.StoredProcedure;
                    CMDarray.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                    CMDarray.Parameters.AddWithValue("@StudentPRN", PRN);
                    CMDarray.Parameters.AddWithValue("@StudentAcademicId", StudentAcademicId);
                    CMDarray.Parameters.AddWithValue("@PaperIdList", PaperIdList);
                    CMDarray.Parameters.AddWithValue("@PaperList", PaperList);
                    CMDarray.Parameters.AddWithValue("@Count", Count);
                    CMDarray.Parameters.AddWithValue("@UserId", UserId);
                    CMDarray.Parameters.AddWithValue("@UserTime", datetime);
                    CMDarray.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    CMDarray.Parameters["@Message"].Direction = ParameterDirection.Output;

                }
                Con.Open();
                ST = Con.BeginTransaction();
                try
                {
                    CMDarray.Transaction = ST;
                    int ans = CMDarray.ExecuteNonQuery();
                    msg = Convert.ToString(CMDarray.Parameters["@Message"].Value);


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
    }
}
