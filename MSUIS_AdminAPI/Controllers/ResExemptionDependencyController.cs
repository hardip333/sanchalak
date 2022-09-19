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
    public class ResExemptionDependencyController : ApiController
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
                SqlDataAdapter da = new SqlDataAdapter("MstProgrammePartGetByProgInstIdNewforExemption", Con);
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
                        //ObjProgInstPart.Id = Convert.ToInt32(dt.Rows[i]["ProgrammeInstancePartId"]);
                        
                        ObjProgInstPart.ProgrammePartId = Convert.ToInt32(dt.Rows[i]["ProgrammePartId"]);
                        //ObjProgInstPart.ProgrammeInstanceId = Convert.ToInt64(dt.Rows[i]["ProgrammeInstanceId"]);
                        //ObjProgInstPart.PartName = Convert.ToString(dt.Rows[i]["PartName"]);
                        ObjProgInstPart.PartShortName = Convert.ToString(dt.Rows[i]["PartShortName"]);
                        //ObjProgInstPart.PartName = Convert.ToString(dt.Rows[i]["PartName"]);
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
                SqlDataAdapter da = new SqlDataAdapter("MstProgrammePartTermGetByProgrammeInstanceIdForResExem", Con);
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


                        //ObjProgPartTerm.Id = Convert.ToInt32(dt.Rows[i]["ProgrammeInstancePartTermId"]);
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

        #region Specialisation List on the basis of ProgrammeInstance Id
        [HttpPost]
        public HttpResponseMessage GetSpecialisationbyProgrammeInstanceId(SpecialisationGetbyProgrammeInstanceId ObjSpecialisationListbyProgInstId)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("SpecialisationGetbyProgrammeInstanceId", Con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.SelectCommand.Parameters.AddWithValue("@ProgrammeInstanceId", ObjSpecialisationListbyProgInstId.ProgrammeInstanceId);

                DataTable dt = new DataTable();
                da.Fill(dt);

                List<SpecialisationGetbyProgrammeInstanceId> ObjSpecbyProgInstId = new List<SpecialisationGetbyProgrammeInstanceId>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        SpecialisationGetbyProgrammeInstanceId ObjSpecibyProgInstId = new SpecialisationGetbyProgrammeInstanceId();

                        ObjSpecibyProgInstId.BranchName = Convert.ToString(dt.Rows[i]["BranchName"]);

                        ObjSpecibyProgInstId.SpecialisationId = Convert.ToInt32(dt.Rows[i]["SpecialisationId"]);
                        ObjSpecibyProgInstId.ProgrammeInstanceId = Convert.ToInt64(dt.Rows[i]["ProgrammeInstanceId"]);

                        ObjSpecbyProgInstId.Add(ObjSpecibyProgInstId);

                    }
                    return Return.returnHttp("200", ObjSpecbyProgInstId, null);
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


        #region Paper List on the basis of Programme Part Term Id
        [HttpPost]
        public HttpResponseMessage PaperListbyProgrammePartTermId(GetPaperListbyPartTermId ObjPaperListbyPTId)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("ResDependencyPaperGetbyPTId", Con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.SelectCommand.Parameters.AddWithValue("@ProgrammePartTermId", ObjPaperListbyPTId.ProgrammePartTermId);
                da.SelectCommand.Parameters.AddWithValue("@SpecialisationId", ObjPaperListbyPTId.SpecialisationId);

                DataTable dt = new DataTable();
                da.Fill(dt);

                List<GetPaperListbyPartTermId> ObjPaperLstbyPT = new List<GetPaperListbyPartTermId>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        GetPaperListbyPartTermId ObjPaperbyPT = new GetPaperListbyPartTermId();



                        ObjPaperbyPT.PaperName = Convert.ToString(dt.Rows[i]["PaperName"]);

                        ObjPaperbyPT.FailPaperId = Convert.ToInt64(dt.Rows[i]["FailPaperId"]);
                        

                        ObjPaperLstbyPT.Add(ObjPaperbyPT);

                    }
                    return Return.returnHttp("200", ObjPaperLstbyPT, null);
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

        #region TLMAMAT on the basis of Paper Id
        [HttpPost]
        public HttpResponseMessage TLMAMATbyPaperId(TLMAMATbyPaperId ObjTLMAMATbyPaperId)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("ResDependencyTLMAMATGetByFailPaperId", Con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                //da.SelectCommand.Parameters.AddWithValue("@ProgrammePartTermId", ObjTLMAMATbyPaperId.ProgrammePartTermId);
                da.SelectCommand.Parameters.AddWithValue("@FailPaperId", ObjTLMAMATbyPaperId.FailPaperId);
                da.SelectCommand.Parameters.AddWithValue("@FPMstPaperTeachingLearningMapId", ObjTLMAMATbyPaperId.FPMstPaperTeachingLearningMapId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<TLMAMATbyPaperId> ObjTLMAMATLstbyPapaerId = new List<TLMAMATbyPaperId>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TLMAMATbyPaperId ObjTLMAMATbyPId = new TLMAMATbyPaperId();



                        ObjTLMAMATbyPId.TLMAMATName = Convert.ToString(dt.Rows[i]["TLMAMATName"]);
                        //ObjTLMAMATbyPId.TLMAMATChecked = Convert.ToBoolean(Dt.Rows[i]["TLMAMATChecked"]);
                        ObjTLMAMATbyPId.FPMstPaperTeachingLearningMapId = Convert.ToInt32(dt.Rows[i]["FPMstPaperTeachingLearningMapId"]);
                        ObjTLMAMATbyPId.TLMAMATChecked = Convert.ToBoolean(dt.Rows[i]["TLMAMATChecked"]);

                        ObjTLMAMATLstbyPapaerId.Add(ObjTLMAMATbyPId);

                    }
                    return Return.returnHttp("200", ObjTLMAMATLstbyPapaerId, null);
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

        #region Paper List on the basis of Programme Part Term Id And TLMAMAT Id
        [HttpPost]
        public HttpResponseMessage PaperListbyProgrammePartTermIdandTLMAMATId(GetPaperListbyPartTermIdandTLMAMATId ObjPaperListbyPTIdandTLMAMATId)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("PaperGetByTLMAMATIdforResDependency", Con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.SelectCommand.Parameters.AddWithValue("@ProgrammePartTermId", ObjPaperListbyPTIdandTLMAMATId.ProgrammePartTermId);
                da.SelectCommand.Parameters.AddWithValue("@DExPMstPaperTeachingLearningMapId", ObjPaperListbyPTIdandTLMAMATId.DExPMstPaperTeachingLearningMapId);
                da.SelectCommand.Parameters.AddWithValue("@FailPaperId", ObjPaperListbyPTIdandTLMAMATId.FailPaperId);
                da.SelectCommand.Parameters.AddWithValue("@SpecialisationId", ObjPaperListbyPTIdandTLMAMATId.SpecialisationId);

                DataTable dt = new DataTable();
                da.Fill(dt);

                List<GetPaperListbyPartTermIdandTLMAMATId> ObjPaperLstbyPTandTLMAMATId = new List<GetPaperListbyPartTermIdandTLMAMATId>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        GetPaperListbyPartTermIdandTLMAMATId ObjPaperbyPTandTLMAMATId = new GetPaperListbyPartTermIdandTLMAMATId();



                        ObjPaperbyPTandTLMAMATId.PaperandAT = Convert.ToString(dt.Rows[i]["PaperandAT"]);
                       
                        ObjPaperbyPTandTLMAMATId.DonotExemptPaperId = Convert.ToInt64(dt.Rows[i]["DonotExemptPaperId"]);
                        ObjPaperbyPTandTLMAMATId.DExPMstPaperTeachingLearningMapId = Convert.ToInt32(dt.Rows[i]["DExPMstPaperTeachingLearningMapId"]);
                        ObjPaperbyPTandTLMAMATId.DoNotExemptPaperChecked = Convert.ToBoolean(dt.Rows[i]["DoNotExemptPaperChecked"]);
                        

                        ObjPaperLstbyPTandTLMAMATId.Add(ObjPaperbyPTandTLMAMATId);

                    }
                    return Return.returnHttp("200", ObjPaperLstbyPTandTLMAMATId, null);
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

        #region ResExemptionDependencyAdd
        [HttpPost]
        public HttpResponseMessage ResExemptionDependencyAdd(List<PaperListforExemptionDependency> PaperList1)
        {

            ResExemptionDependency modelobj = new ResExemptionDependency();

            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                //  modelobj.ProgrammePartTermId = ObjResExemptionDependency.ProgrammePartTermId;

                //var PaperList1 = ObjResExemptionDependency.PaperList1;
                //if (modelobj.ProgrammePartTermId <= 0)
                //{
                //    return Return.returnHttp("201", "Please enter valid Program Part Term Id", null);
                //}
                //else
                if (PaperList1.Count <=0)
                {
                    return Return.returnHttp("201", "Paper List is empty or null", null);
                }
                else
                {
                    SqlCommand[] PaperListArray = new SqlCommand[PaperList1.Count()];
                    var i = 0;
                    String msg = "";

                    modelobj.CreatedBy = Convert.ToInt32(res);


                    foreach (PaperListforExemptionDependency objPaper in PaperList1)
                    {
                        if (objPaper.DoNotExemptPaperChecked && objPaper.TLMAMATChecked)
                        {
                            Int32 ProgrammePartTermId = objPaper.ProgrammePartTermId;
                            Int32 AcademicYearId = objPaper.AcademicYearId;
                            Int64 FailPaperId = objPaper.FailPaperId;
                            Int32 FPMstPaperTeachingLearningMapId = objPaper.FPMstPaperTeachingLearningMapId;
                            Int64 DonotExemptPaperId = objPaper.DonotExemptPaperId;
                            Int32 DExPMstPaperTeachingLearningMapId = objPaper.DExPMstPaperTeachingLearningMapId;


                            PaperListArray[i] = new SqlCommand("ResExemptionDependencyAdd", Con, ST);
                            PaperListArray[i].CommandType = CommandType.StoredProcedure;
                            //PaperListArray[i].Parameters.AddWithValue("@Id", Id);
                            PaperListArray[i].Parameters.AddWithValue("@ProgrammePartTermId", ProgrammePartTermId);
                            PaperListArray[i].Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                            PaperListArray[i].Parameters.AddWithValue("@FailPaperId", FailPaperId);
                            PaperListArray[i].Parameters.AddWithValue("@FPMstPaperTeachingLearningMapId", FPMstPaperTeachingLearningMapId);
                            PaperListArray[i].Parameters.AddWithValue("@DonotExemptPaperId", DonotExemptPaperId);
                            PaperListArray[i].Parameters.AddWithValue("@DExPMstPaperTeachingLearningMapId", DExPMstPaperTeachingLearningMapId);

                            PaperListArray[i].Parameters.AddWithValue("@CreatedBy", modelobj.CreatedBy);
                            PaperListArray[i].Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                            PaperListArray[i].Parameters["@Message"].Direction = ParameterDirection.Output;
                            i++;
                        }
                        else
                        {
                            Int32 ProgrammePartTermId = objPaper.ProgrammePartTermId;
                            Int64 FailPaperId = objPaper.FailPaperId;
                            Int32 DExPMstPaperTeachingLearningMapId = objPaper.DExPMstPaperTeachingLearningMapId;
                            Int64 DonotExemptPaperId = objPaper.DonotExemptPaperId;
                            Int32 FPMstPaperTeachingLearningMapId = objPaper.FPMstPaperTeachingLearningMapId;

                            PaperListArray[i] = new SqlCommand("ResExemptionDependencyDelete", Con, ST);
                            PaperListArray[i].CommandType = CommandType.StoredProcedure;
                            PaperListArray[i].Parameters.AddWithValue("@ProgrammePartTermId", ProgrammePartTermId);
                            PaperListArray[i].Parameters.AddWithValue("@FailPaperId", FailPaperId);
                            PaperListArray[i].Parameters.AddWithValue("@DonotExemptPaperId", DonotExemptPaperId);
                            PaperListArray[i].Parameters.AddWithValue("@FPMstPaperTeachingLearningMapId", FPMstPaperTeachingLearningMapId);
                            PaperListArray[i].Parameters.AddWithValue("@DExPMstPaperTeachingLearningMapId", DExPMstPaperTeachingLearningMapId);
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

        #region ResExemptionDependencyDelete
        [HttpPost]
        public HttpResponseMessage ResExemptionDependencyDelete(List<PaperListforExemptionDependency> PaperList1)
        {
            ResExemptionDependency modelobj = new ResExemptionDependency();

            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                //var PaperList1 = ObjResExemptionDependency.PaperList1;
                if (PaperList1.Count <= 0)
                {
                    return Return.returnHttp("201", "Paper List is empty or null", null);
                }
                else
                {
                    SqlCommand[] PaperListArray = new SqlCommand[PaperList1.Count()];
                    var i = 0;
                    String msg = "";


                    foreach (PaperListforExemptionDependency objPaper in PaperList1)
                    {
                        Int32 ProgrammePartTermId = objPaper.ProgrammePartTermId;
                        Int64 FailPaperId = objPaper.FailPaperId;
                        Int32 DExPMstPaperTeachingLearningMapId = objPaper.DExPMstPaperTeachingLearningMapId;
                        Int64 DonotExemptPaperId = objPaper.DonotExemptPaperId;
                        Int32 FPMstPaperTeachingLearningMapId = objPaper.FPMstPaperTeachingLearningMapId;

                        PaperListArray[i] = new SqlCommand("ResExemptionDependencyDelete", Con, ST);
                        PaperListArray[i].CommandType = CommandType.StoredProcedure;
                        PaperListArray[i].Parameters.AddWithValue("@ProgrammePartTermId", ProgrammePartTermId);
                        PaperListArray[i].Parameters.AddWithValue("@FailPaperId", FailPaperId);
                        PaperListArray[i].Parameters.AddWithValue("@DonotExemptPaperId", DonotExemptPaperId);
                        PaperListArray[i].Parameters.AddWithValue("@FPMstPaperTeachingLearningMapId", FPMstPaperTeachingLearningMapId);
                        PaperListArray[i].Parameters.AddWithValue("@DExPMstPaperTeachingLearningMapId", DExPMstPaperTeachingLearningMapId);
                        PaperListArray[i].Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                        PaperListArray[i].Parameters["@Message"].Direction = ParameterDirection.Output;
                        i++;

                    }

                    Con.Open();

                    ST = Con.BeginTransaction();
                    try
                    {
                        for (int b = 0; b < i; b++)
                        {
                            PaperListArray[b].Transaction = ST;
                            int ans1 = PaperListArray[b].ExecuteNonQuery();
                            msg = msg + ", " + Convert.ToString(PaperListArray[b].Parameters["@Message"].Value);
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






































    }

}