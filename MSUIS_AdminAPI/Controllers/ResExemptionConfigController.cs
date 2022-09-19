using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class ResExemptionConfigController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region AcademicYearGet
        [HttpPost]
        public HttpResponseMessage AcademicYearGet()
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

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("IncAcademicYearGet", Con);

                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<AcademicYear> ObjAYList = new List<AcademicYear>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AcademicYear ObjAY = new AcademicYear();
                        ObjAY.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjAY.AcademicYearCode = Convert.ToString(dt.Rows[i]["AcademicYearCode"]);
                        ObjAYList.Add(ObjAY);
                    }
                    return Return.returnHttp("200", ObjAYList, null);
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

        #region FacultyGet
        [HttpPost]
        public HttpResponseMessage FacultyGet()
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

                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlCommand Cmd = new SqlCommand("MstFacultyGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);


                List<MstFaculty> ObjLstFaculty = new List<MstFaculty>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstFaculty objFaculty = new MstFaculty();
                        if (Dt.Rows[i]["Id"].ToString() != "")
                        {
                            objFaculty.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);

                        }
                        if (Dt.Rows[i]["FacultyName"].ToString() != "")
                        {
                            objFaculty.FacultyName = Convert.ToString(Dt.Rows[i]["FacultyName"]);

                        }
                        if (Dt.Rows[i]["FacultyCode"].ToString() != "")
                        {
                            objFaculty.FacultyCode = Convert.ToString(Dt.Rows[i]["FacultyCode"]);

                        }
                        if (Dt.Rows[i]["FacultyAddress"].ToString() != "")
                        {
                            objFaculty.FacultyAddress = Convert.ToString(Dt.Rows[i]["FacultyAddress"]);

                        }
                        if (Dt.Rows[i]["CityName"].ToString() != "")
                        {
                            objFaculty.CityName = Convert.ToString(Dt.Rows[i]["CityName"]);

                        }
                        if (Dt.Rows[i]["Pincode"].ToString() != "")
                        {
                            objFaculty.Pincode = Convert.ToInt32(Dt.Rows[i]["Pincode"]);

                        }
                        if (Dt.Rows[i]["FacultyContactNo"].ToString() != "")
                        {
                            objFaculty.FacultyContactNo = Convert.ToString(Dt.Rows[i]["FacultyContactNo"]);

                        }
                        //objFaculty.FacultyFaxNo = Convert.ToString(Dt.Rows[i]["FacultyFaxNo"]);
                        //objFaculty.FacultyEmail = Convert.ToString(Dt.Rows[i]["FacultyEmail"]);
                        //objFaculty.FacultyUrl = Convert.ToString(Dt.Rows[i]["FacultyUrl"]);
                        //objFaculty.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjLstFaculty.Add(objFaculty);
                    }
                }
                return Return.returnHttp("200", ObjLstFaculty, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region InstanceListGetbyFacultyIdAndAcadId
        [HttpPost]
        public HttpResponseMessage InstanceListGetbyFacultyIdAndAcadId(ProgramInstance ObjProgInstance)
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

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("IncProgrammeInstanceGetByFacultyIdAndAcadId", Con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@FacultyId", ObjProgInstance.FacultyId);
                da.SelectCommand.Parameters.AddWithValue("@AcademicYearId", ObjProgInstance.AcademicYearId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<ProgramInstance> ObjLstProgInst = new List<ProgramInstance>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProgramInstance ObjProgInst = new ProgramInstance();
                        ObjProgInst.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjProgInst.FacultyName = Convert.ToString(dt.Rows[i]["FacultyName"]);
                        ObjProgInst.FacultyId = Convert.ToInt32(dt.Rows[i]["FacultyId"]);
                        ObjProgInst.ProgrammeId = Convert.ToInt32(dt.Rows[i]["ProgrammeId"]);
                        ObjProgInst.ProgrammeName = Convert.ToString(dt.Rows[i]["ProgrammeName"]);
                        ObjProgInst.InstanceName = Convert.ToString(dt.Rows[i]["InstanceName"]);
                        //ObjProgInst.BranchName = Convert.ToString(dt.Rows[i]["BranchName"]);
                        //ObjProgInst.SpecialisationId = Convert.ToInt32(dt.Rows[i]["SpecialisationId"]);
                        ObjProgInst.AcademicYear = Convert.ToString(dt.Rows[i]["AcademicYearCode"]);
                        ObjProgInst.AcademicYearId = Convert.ToInt32(dt.Rows[i]["AcademicYearId"]);
                        ObjProgInst.Intake = Convert.ToInt32(dt.Rows[i]["Intake"]);
                        ObjProgInst.IsActive = Convert.ToBoolean(dt.Rows[i]["IsActive"]);
                        ObjProgInst.IsActiveSts = Convert.ToString(dt.Rows[i]["IsActiveSts"]);
                        ObjProgInst.IsDeleted = Convert.ToBoolean(dt.Rows[i]["IsDeleted"]);
                        //ObjProgInst.CreatedBy = Convert.ToInt64(dt.Rows[i]["CreatedBy"]);
                        //ObjProgInst.CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]);
                        //ObjProgInst.ModifiedBy = Convert.ToInt64(dt.Rows[i]["ModifiedBy"]);
                        //ObjProgInst.ModifiedOn = Convert.ToDateTime(dt.Rows[i]["ModifiedOn"]);

                        ObjLstProgInst.Add(ObjProgInst);

                    }
                    return Return.returnHttp("200", ObjLstProgInst, null);
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

        #region Programme Part Name List on the basis of Programme Instance Id
        [HttpPost]
        public HttpResponseMessage ProgrammePartGetByProgInstId(ProgrammeInstancePartTerm ObjProgInstPartTerm)
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

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("MstProgrammePartGetByProgInstIdNew", Con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@ProgrammeInstanceId", ObjProgInstPartTerm.ProgrammeInstanceId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<ProgrammeInstancePart> ObjLstProgInstPart = new List<ProgrammeInstancePart>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePart ObjProgInstPart = new ProgrammeInstancePart();
                        
                        ObjProgInstPart.ProgrammePartId = Convert.ToInt32(dt.Rows[i]["ProgrammePartId"]);
                        
                       
                        ObjProgInstPart.PartShortName = Convert.ToString(dt.Rows[i]["PartShortName"]);
                        ObjProgInstPart.ProgrammePartName = Convert.ToString(dt.Rows[i]["PartName"]);
                        ObjLstProgInstPart.Add(ObjProgInstPart);

                    }
                    return Return.returnHttp("200", ObjLstProgInstPart, null);
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

        #region Programme Part Term Name List on the basis of Programme Instance Id
        [HttpPost]
        public HttpResponseMessage ProgrammePartTermGetByProgInstId(GetPartTermShortName ObjProgInstPartTerm)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("MstProgrammePartTermGetByProgrammeInstanceIdForResEval", Con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@ProgrammePartId", ObjProgInstPartTerm.ProgrammePartId);

                DataTable dt = new DataTable();
                da.Fill(dt);

                List<GetPartTermShortName> ObjLstProgPartTerm = new List<GetPartTermShortName>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        GetPartTermShortName ObjProgPartTerm = new GetPartTermShortName();

                        ObjProgPartTerm.PartTermName = Convert.ToString(dt.Rows[i]["PartTermName"]);

                        ObjProgPartTerm.PartTermShortName = Convert.ToString(dt.Rows[i]["PartTermShortName"]);

                        ObjProgPartTerm.ProgrammePartTermId = Convert.ToInt32(dt.Rows[i]["ProgrammePartTermId"]);

                        ObjLstProgPartTerm.Add(ObjProgPartTerm);

                    }
                    return Return.returnHttp("200", ObjLstProgPartTerm, null);
                }
                else
                {
                    return Return.returnHttp("404", "No Record Found ++", null);
                }

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion


        #region Get Paper List for Result Exemption Configuration
        [HttpPost]
        public HttpResponseMessage PaperListForResCourseEvalSystem(ResExemptionConfig ObjResCourseExemption)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("ResExemptionMarkConfigGet", Con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@ProgrammePartTermId", ObjResCourseExemption.ProgrammePartTermId);

                DataTable dt = new DataTable();
                da.Fill(dt);

                List<ResExemptionConfig> ObjLstResCourseExemption = new List<ResExemptionConfig>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ResExemptionConfig ObjResExemption = new ResExemptionConfig();


                        ObjResExemption.MstPaperId = Convert.ToInt64(dt.Rows[i]["MstPaperId"]);
                        //ObjResEvalSystem.ProgrammePartTermId = Convert.ToInt32(dt.Rows[i]["ProgrammePartTermId"]);
                        ObjResExemption.PaperMinMarks = Convert.ToInt32(dt.Rows[i]["PaperMinMarks"]);
                        ObjResExemption.PaperMaxMarks = Convert.ToInt32(dt.Rows[i]["PaperMaxMarks"]);
                        ObjResExemption.PaperTotalMaxMarks = Convert.ToInt32(dt.Rows[i]["PaperTotalMaxMarks"]);
                        ObjResExemption.PaperTotalMinMarks = Convert.ToInt32(dt.Rows[i]["PaperTotalMinMarks"]);
                        ObjResExemption.MstPaperTeachingLearningMapId = Convert.ToInt32(dt.Rows[i]["MstPaperTeachingLearningMapId"]);
                        ObjResExemption.PaperName = Convert.ToString(dt.Rows[i]["PaperName"]);
                        ObjResExemption.AssessmentType = Convert.ToString(dt.Rows[i]["AssessmentType"]);
                        ObjResExemption.TeachingLearningMethodName = Convert.ToString(dt.Rows[i]["TeachingLearningMethodName"]);
                        ObjResExemption.AssessmentMethodName = Convert.ToString(dt.Rows[i]["AssessmentMethodName"]);

                        //ObjResExemption.MinMarksInPercent = Convert.ToDouble(dt.Rows[i]["MinMarksInPercent"]);
                        ObjResExemption.ExemptionMarks = Convert.ToInt32(dt.Rows[i]["ExemptionMarks"]);
                        ObjResExemption.ExemptionMarksPercent = Convert.ToDouble(dt.Rows[i]["ExemptionMarksPercent"]);




                        ObjLstResCourseExemption.Add(ObjResExemption);

                    }
                    return Return.returnHttp("200", ObjLstResCourseExemption, null);
                }
                else
                {
                    return Return.returnHttp("404", "No Record Found ++", null);
                }

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion



        #region FetchPaperForExemtion
        public static List<PaperListforExemtion> FetchPaperForExemtion(Int32 ProgrammePartTermId, Int32 AcademicYearId)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            List<PaperListforExemtion> PaperDetail = new List<PaperListforExemtion>();

            SqlCommand cmd = new SqlCommand("PaperGetforExemptionMarkConfig", Con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ProgrammePartTermId", ProgrammePartTermId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);

            SqlDataAdapter Da = new SqlDataAdapter();
            DataTable Dt = new DataTable();
            Da.SelectCommand = cmd;
            Da.Fill(Dt);
            if (Dt.Rows.Count > 0)
            {
                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    PaperListforExemtion Paper = new PaperListforExemtion();

                    Paper.MstPaperId = (((Dt.Rows[i]["MstPaperId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["MstPaperId"]);
                    //Paper.IncProgrammeInstancePartTermId = (((Dt.Rows[i]["IncProgrammeInstancePartTermId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["IncProgrammeInstancePartTermId"]);
                    Paper.PaperName = (Convert.ToString(Dt.Rows[i]["PaperName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["PaperName"]);
                    Paper.PaperTotalMaxMarks = (((Dt.Rows[i]["PaperTotalMaxMarks"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["PaperTotalMaxMarks"]);
                    Paper.PaperTotalMinMarks = (((Dt.Rows[i]["PaperTotalMinMarks"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["PaperTotalMinMarks"]);
                    //Paper.EvaluationName = (Convert.ToString(Dt.Rows[i]["EvaluationName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["EvaluationName"]);
                    //Paper.EvaluationName = Convert.ToString(Dt.Rows[i]["EvaluationName"]);
                    //Paper.IsCheckSelect = (Convert.ToString(Dt.Rows[i]["IsCheckSelect"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsCheckSelect"]);
                    //Paper.IsCheckSelectSts = (Convert.ToString(Dt.Rows[i]["IsCheckSelectSts"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsCheckSelectSts"]);
                    ////Paper.TemplateName = (Convert.ToString(Dt.Rows[i]["TemplateName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["TemplateName"]);
                    //Paper.TemplateName = Convert.ToString(Dt.Rows[i]["TemplateName"]);
                    Paper.IsLaunched = (Convert.ToString(Dt.Rows[i]["IsLaunched"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsLaunched"]);

                    PaperDetail.Add(Paper);
                }
            }
            return PaperDetail;
        }

        #endregion

        #region FetchPaperTLMAMAT
        public static List<PaperListforExemtion> FetchPaperTLMAMAT(Int32 ProgrammePartTermId,Int32 AcademicYearId)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            List<PaperListforExemtion> TLMAMATDetail = new List<PaperListforExemtion>();

            SqlCommand cmd = new SqlCommand("ResExemptionMarkConfigGet", Con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ProgrammePartTermId", ProgrammePartTermId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);

            SqlDataAdapter Da = new SqlDataAdapter();
            DataTable Dt = new DataTable();
            Da.SelectCommand = cmd;
            Da.Fill(Dt);
            if (Dt.Rows.Count > 0)
            {
                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    PaperListforExemtion TLMAMAT = new PaperListforExemtion();

                    TLMAMAT.MstPaperTeachingLearningMapId = (((Dt.Rows[i]["MstPaperTeachingLearningMapId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["MstPaperTeachingLearningMapId"]);
                    //TLMAMAT.IncProgrammeInstancePartTermId = (((Dt.Rows[i]["IncProgrammeInstancePartTermId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["IncProgrammeInstancePartTermId"]);
                    TLMAMAT.TeachingLearningMethodName = (Convert.ToString(Dt.Rows[i]["TeachingLearningMethodName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["TeachingLearningMethodName"]);
                    TLMAMAT.AssessmentType = (Convert.ToString(Dt.Rows[i]["AssessmentType"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["AssessmentType"]);
                    TLMAMAT.AssessmentMethodName = (Convert.ToString(Dt.Rows[i]["AssessmentMethodName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["AssessmentMethodName"]);
                    TLMAMAT.PaperMinMarks = (((Dt.Rows[i]["PaperMinMarks"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["PaperMinMarks"]);
                    TLMAMAT.PaperMaxMarks = (((Dt.Rows[i]["PaperMaxMarks"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["PaperMaxMarks"]);
                    TLMAMAT.MstPaperId = (((Dt.Rows[i]["MstPaperId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["MstPaperId"]);
                    TLMAMAT.IsLaunched = (Convert.ToString(Dt.Rows[i]["IsLaunched"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsLaunched"]);

                    //TLMAMAT.MinMarksInPercent = (((Dt.Rows[i]["MinMarksInPercent"]).ToString()).IsEmpty()) ? 0 : Convert.ToDouble(Dt.Rows[i]["MinMarksInPercent"]);
                    if (string.IsNullOrEmpty(Convert.ToString(Dt.Rows[i]["ExemptionMarks"])))
                    {
                        TLMAMAT.ExemptionMarks = null;
                    }
                    else
                    {
                        TLMAMAT.ExemptionMarks = Convert.ToInt32(Dt.Rows[i]["ExemptionMarks"]);
                    }
                    if (string.IsNullOrEmpty(Convert.ToString(Dt.Rows[i]["ExemptionMarksPercent"])))
                    {
                        TLMAMAT.ExemptionMarksPercent = null;
                    }
                    else
                    {
                        TLMAMAT.ExemptionMarksPercent = Convert.ToDouble(Dt.Rows[i]["ExemptionMarksPercent"]);
                    }
                    TLMAMATDetail.Add(TLMAMAT);
                }
            }
            return TLMAMATDetail;
        }

        #endregion

        #region Final View For Result Exemption System
        [HttpPost]
        public HttpResponseMessage FinalViewforExemptionConfig(ResExemptionConfig ResExemtion)
        {
            ResExemptionConfig modelobj = new ResExemptionConfig();
            try
            {
                List<PaperListforExemtion> PaperDetail = ResExemptionConfigController.FetchPaperForExemtion(ResExemtion.ProgrammePartTermId, ResExemtion.AcademicYearId);
                List<PaperListforExemtion> TLMAMATDetail = ResExemptionConfigController.FetchPaperTLMAMAT(ResExemtion.ProgrammePartTermId, ResExemtion.AcademicYearId);
                List<ResExemptionConfig> AllDetails = new List<ResExemptionConfig>();

                for (int i = 0; i < PaperDetail.Count; i++)
                {
                    ResExemptionConfig Paper = new ResExemptionConfig();

                    Paper.MstPaperId = (((PaperDetail[i].MstPaperId).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(PaperDetail[i].MstPaperId);
                    //Paper.IncProgrammeInstancePartTermId = (((PaperDetail[i].IncProgrammeInstancePartTermId).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(PaperDetail[i].IncProgrammeInstancePartTermId);
                    Paper.PaperName = (Convert.ToString(PaperDetail[i].PaperName)).IsEmpty() ? "Not Defined" : Convert.ToString(PaperDetail[i].PaperName);
                    Paper.PaperMaxMarks = (((PaperDetail[i].PaperTotalMaxMarks).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(PaperDetail[i].PaperTotalMaxMarks);
                    Paper.PaperMinMarks = (((PaperDetail[i].PaperTotalMinMarks).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(PaperDetail[i].PaperTotalMinMarks);
                    Paper.IsLaunched = (Convert.ToString(PaperDetail[i].IsLaunched)).IsEmpty() ? false : Convert.ToBoolean(PaperDetail[i].IsLaunched);

                    AllDetails.Add(Paper);

                    for (int j = 0; j < TLMAMATDetail.Count; j++)
                    {

                        if (Paper.MstPaperId == TLMAMATDetail[j].MstPaperId)
                        {

                            ResExemptionConfig TLMAMAT = new ResExemptionConfig();

                            TLMAMAT.MstPaperTeachingLearningMapId = (((TLMAMATDetail[j].MstPaperTeachingLearningMapId).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(TLMAMATDetail[j].MstPaperTeachingLearningMapId);
                            //TLMAMAT.IncProgrammeInstancePartTermId = (((TLMAMATDetail[j].IncProgrammeInstancePartTermId).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(TLMAMATDetail[j].IncProgrammeInstancePartTermId);
                            TLMAMAT.TeachingLearningMethodName = (Convert.ToString(TLMAMATDetail[j].TeachingLearningMethodName)).IsEmpty() ? "Not Defined" : Convert.ToString(TLMAMATDetail[j].TeachingLearningMethodName);
                            TLMAMAT.AssessmentType = (Convert.ToString(TLMAMATDetail[j].AssessmentType)).IsEmpty() ? "Not Defined" : Convert.ToString(TLMAMATDetail[j].AssessmentType);
                            TLMAMAT.AssessmentMethodName = (Convert.ToString(TLMAMATDetail[j].AssessmentMethodName)).IsEmpty() ? "Not Defined" : Convert.ToString(TLMAMATDetail[j].AssessmentMethodName);
                            TLMAMAT.PaperMinMarks = (((TLMAMATDetail[j].PaperMinMarks).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(TLMAMATDetail[j].PaperMinMarks);
                            TLMAMAT.PaperMaxMarks = (((TLMAMATDetail[j].PaperMaxMarks).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(TLMAMATDetail[j].PaperMaxMarks);
                            TLMAMAT.MstPaperId = (((TLMAMATDetail[j].MstPaperId).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(TLMAMATDetail[j].MstPaperId);
                            TLMAMAT.IsLaunched = (Convert.ToString(TLMAMATDetail[j].IsLaunched)).IsEmpty() ? false : Convert.ToBoolean(TLMAMATDetail[j].IsLaunched);

                            //TLMAMAT.MinMarksInPercent = (((TLMAMATDetail[j].MinMarksInPercent).ToString()).IsEmpty()) ? 0 : Convert.ToDouble(TLMAMATDetail[j].MinMarksInPercent);
                            //TLMAMAT.ExemptionMarks = (((TLMAMATDetail[j].ExemptionMarks).ToString()).IsEmpty()) ? 999 : Convert.ToInt32(TLMAMATDetail[j].ExemptionMarks);

                            //if (string.IsNullOrEmpty(Convert.ToString(TLMAMATDetail[j].ExemptionMarks)))
                            //{
                            //    TLMAMAT.ExemptionMarks = null;
                            //}
                            //else
                            //{
                            //    TLMAMAT.ExemptionMarks = Convert.ToInt32(TLMAMATDetail[j].ExemptionMarks);
                            //}

                            if (string.IsNullOrEmpty(Convert.ToString(TLMAMATDetail[j].ExemptionMarks)))

                            {
                                TLMAMAT.ExemptionMarks = null;
                            }
                            else
                            {
                                TLMAMAT.ExemptionMarks = Convert.ToInt32(TLMAMATDetail[j].ExemptionMarks);
                            }

                            //TLMAMAT.ExemptionMarksPercent = (((TLMAMATDetail[j].ExemptionMarksPercent).ToString()).IsEmpty()) ? 999 : Convert.ToInt32(TLMAMATDetail[j].ExemptionMarksPercent);
                            if (TLMAMATDetail[j].ExemptionMarksPercent == null)
                            {
                                TLMAMAT.ExemptionMarksPercent = null;
                            }
                            else
                            {
                                TLMAMAT.ExemptionMarksPercent = Convert.ToDouble(TLMAMATDetail[j].ExemptionMarksPercent);
                            }
                            AllDetails.Add(TLMAMAT);
                        }
                    }



                }
                return Return.returnHttp("200", AllDetails, null);



            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region ResExemptionConfigAdd
        [HttpPost]
        public HttpResponseMessage ResExemptionConfigAdd(ResExemptionConfig ObjResExemption)
        {

            ResExemptionConfig modelobj = new ResExemptionConfig();

            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                modelobj.ProgrammePartTermId = ObjResExemption.ProgrammePartTermId;

                var PaperList1 = ObjResExemption.PaperList1;
                if (modelobj.ProgrammePartTermId <= 0)
                {
                    return Return.returnHttp("201", "Please enter valid Program Part Term Id", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(PaperList1)))
                {
                    return Return.returnHttp("201", "Paper List is empty or null", null);
                }
                else
                {
                    SqlCommand[] PaperListArray = new SqlCommand[PaperList1.Count()];
                    var i = 0;
                    String msg = "";

                    modelobj.CreatedBy = Convert.ToInt32(res);
                    //Int64 MstPaperId;

                    foreach (PaperListforExemtion objPaper in PaperList1)
                    {
                        if (objPaper.MstPaperTeachingLearningMapId > 0)
                        {


                            Int32 ProgrammePartTermId = ObjResExemption.ProgrammePartTermId;
                            Int32 AcademicYearId = ObjResExemption.AcademicYearId;
                            Int64 MstPaperTeachingLearningMapId = objPaper.MstPaperTeachingLearningMapId;


                            Int64 MstPaperId = objPaper.MstPaperId;
                            //Double MinMarksInPercent = objPaper.MinMarksInPercent;
                            Double ? ExemptionMarksPercent;
                            if (string.IsNullOrWhiteSpace(Convert.ToString(objPaper.ExemptionMarksPercent)))
                            {
                                ExemptionMarksPercent = null;
                            }
                            else if(objPaper.ExemptionMarksPercent >= 100)
                            {
                                return Return.returnHttp("201", "ExemptionMarksPercent/ExemptionMarks should be below 100", null);
                            }
                            else
                            {
                                ExemptionMarksPercent = Convert.ToDouble(objPaper.ExemptionMarksPercent);
                            }


                            int ? ExemptionMarks;
                            if (string.IsNullOrWhiteSpace(Convert.ToString( objPaper.ExemptionMarks)))    
                                {
                                ExemptionMarks = null;
                            }
                            else if (objPaper.ExemptionMarks >= 100)
                            {
                                return Return.returnHttp("201", "ExemptionMarksPercent/ExemptionMarks should be below 100", null);
                            }
                            else
                            {
                                ExemptionMarks =Convert.ToInt32 (objPaper.ExemptionMarks);
                            }
                                                                                    
                            String AssessmentType;
                            if (objPaper.AssessmentType == null)
                            {
                                AssessmentType = "-";
                            }
                            else
                            {
                                AssessmentType = objPaper.AssessmentType;
                            }


                            PaperListArray[i] = new SqlCommand("ResExemptionMarkConfigAdd", Con, ST);
                            PaperListArray[i].CommandType = CommandType.StoredProcedure;
                            //PaperListArray[i].Parameters.AddWithValue("@Id", Id);
                            PaperListArray[i].Parameters.AddWithValue("@ProgrammePartTermId", ProgrammePartTermId);
                            PaperListArray[i].Parameters.AddWithValue("@MstPaperTeachingLearningMapId", MstPaperTeachingLearningMapId);
                            PaperListArray[i].Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                            PaperListArray[i].Parameters.AddWithValue("@MstPaperId", MstPaperId);
                            
                            PaperListArray[i].Parameters.AddWithValue("@AssessmentType", AssessmentType);
                            PaperListArray[i].Parameters.AddWithValue("@ExemptionMarksPercent", ExemptionMarksPercent);
                        
                            PaperListArray[i].Parameters.AddWithValue("@ExemptionMarks",ExemptionMarks);
                            PaperListArray[i].Parameters.AddWithValue("@CreatedBy", modelobj.CreatedBy);



                            PaperListArray[i].Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                            PaperListArray[i].Parameters["@Message"].Direction = ParameterDirection.Output;
                            i++;
                        }
                    }


                    try
                    {
                        Con.Open();
                        ST = Con.BeginTransaction();
                        for (int b = 0; b < i; b++)
                        {
                            PaperListArray[b].Transaction = ST;
                            int ans1 = PaperListArray[b].ExecuteNonQuery();
                            msg = msg + ", " + Convert.ToString(PaperListArray[b].Parameters["@Message"].Value);
                        }

                        ST.Commit();

                        return Return.returnHttp("200", "Data has been saved successfully", null);
                    }
                    catch (SqlException sqlError)
                    {
                        ST.Rollback();
                        String strMessageErr = Convert.ToString(sqlError);
                        Con.Close();
                        return Return.returnHttp("201", "Server Error", null);
                    }
                    finally
                    {
                        Con.Close();
                    }
                }

            }
            catch (Exception e)
            {

                return Return.returnHttp("201", e.Message.ToString(), null);

            }

        }
        #endregion

        #region ResExemptionDelete
        [HttpPost]
        public HttpResponseMessage ResExemptionDelete(ResExemptionConfig ObjResExemption)
        {
            ResExemptionConfig modelobj = new ResExemptionConfig();

            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                var PaperList1 = ObjResExemption.PaperList1;
                if (String.IsNullOrEmpty(Convert.ToString(PaperList1)))
                {
                    return Return.returnHttp("201", "Instance Part Term List is empty or null", null);
                }
                else
                {
                    SqlCommand[] PaperArray = new SqlCommand[PaperList1.Count()];
                    var i = 0;
                    String msg = "";


                    foreach (PaperListforExemtion objPaper in PaperList1)
                    {
                        Int32 ProgrammePartTermId = ObjResExemption.ProgrammePartTermId;
                        Int64 MstPaperId = objPaper.MstPaperId;
                        //Int64 MstPaperTeachingLearningMapId = objPaper.MstPaperTeachingLearningMapId;

                        //Int32 ClassGradeTemplateId = ObjResEval.ClassGradeTemplateId;

                        PaperArray[i] = new SqlCommand("ResExemptionMarkConfigDelete", Con, ST);
                        PaperArray[i].CommandType = CommandType.StoredProcedure;
                        PaperArray[i].Parameters.AddWithValue("@ProgrammePartTermId", ProgrammePartTermId);
                        PaperArray[i].Parameters.AddWithValue("@MstPaperId", MstPaperId);
                        //PaperArray[i].Parameters.AddWithValue("@MstPaperTeachingLearningMapId", MstPaperTeachingLearningMapId);
                        PaperArray[i].Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                        PaperArray[i].Parameters["@Message"].Direction = ParameterDirection.Output;
                        i++;

                    }

                    Con.Open();

                    ST = Con.BeginTransaction();
                    try
                    {
                        for (int b = 0; b < i; b++)
                        {
                            PaperArray[b].Transaction = ST;
                            int ans1 = PaperArray[b].ExecuteNonQuery();
                            msg = msg + ", " + Convert.ToString(PaperArray[b].Parameters["@Message"].Value);
                        }

                        ST.Commit();
                        return Return.returnHttp("200", "Record deleted successfully", null);
                    }
                    catch (SqlException sqlError)
                    {
                        ST.Rollback();
                        String strMessageErr = Convert.ToString(sqlError);

                        return Return.returnHttp("201", "Server Error", null);
                    }
                    finally
                    {
                        Con.Close();
                    }
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
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
                //if (ObjResLaunch.ResCalculation == true || ObjResLaunch.ResCourseEvalSystem == true || ObjResLaunch.ResExemptionMarkConfig == true)
                //{

                SqlCommand cmd = new SqlCommand("ResConfigurationLaunchforExemption", Con);

                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@ProgrammePartTermId", ObjResLaunch.ProgrammePartTermId);
                cmd.Parameters.AddWithValue("@AcademicYearId", ObjResLaunch.AcademicYearId);
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
                //}
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion














    }

}