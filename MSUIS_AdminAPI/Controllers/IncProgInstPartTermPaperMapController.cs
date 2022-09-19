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
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class IncProgInstPartTermPaperMapController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region Academic Year List
        [HttpPost]
        public HttpResponseMessage IncAcademicYearListGet()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter cmdda = new SqlDataAdapter("IncAcademicYearGet", Con);

                cmdda.SelectCommand.CommandType = CommandType.StoredProcedure;

                DataTable Dt = new DataTable();
                cmdda.Fill(Dt);


                List<AcademicYear> ObjLstIncAcademicYear = new List<AcademicYear>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        AcademicYear ObjIncAcademicYear = new AcademicYear();

                        ObjIncAcademicYear.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        ObjIncAcademicYear.AcademicYearCode = Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);
                        //ObjIncAcademicYear.FromDateView = Convert.ToString((Dt.Rows[i]["FromDate"]), System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"));
                        //ObjIncAcademicYear.ToDateView = Convert.ToString((Dt.Rows[i]["ToDate"]), System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"));
                        //DateTime FromDate = Convert.ToDateTime(Dt.Rows[i]["FromDate"]);
                        //ObjIncAcademicYear.FromDate = FromDate;
                        //DateTime ToDate = Convert.ToDateTime(Dt.Rows[i]["ToDate"]);
                        //ObjIncAcademicYear.ToDate = ToDate;
                        ObjIncAcademicYear.IsActiveSts = Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        ObjIncAcademicYear.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjIncAcademicYear.IsDeleted = Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);

                        ObjLstIncAcademicYear.Add(ObjIncAcademicYear);
                    }

                }

                return Return.returnHttp("200", ObjLstIncAcademicYear, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region Faculty List
        [HttpPost]
        public HttpResponseMessage FacultyGet()
        {
            try
            {

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

                        objFaculty.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objFaculty.FacultyName = Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objFaculty.FacultyCode = Convert.ToString(Dt.Rows[i]["FacultyCode"]);
                        objFaculty.FacultyAddress = Convert.ToString(Dt.Rows[i]["FacultyAddress"]);
                        objFaculty.CityName = Convert.ToString(Dt.Rows[i]["CityName"]);
                        objFaculty.Pincode = Convert.ToInt32(Dt.Rows[i]["Pincode"]);
                        objFaculty.FacultyContactNo = Convert.ToString(Dt.Rows[i]["FacultyContactNo"]);
                        objFaculty.FacultyFaxNo = Convert.ToString(Dt.Rows[i]["FacultyFaxNo"]);
                        objFaculty.FacultyEmail = Convert.ToString(Dt.Rows[i]["FacultyEmail"]);
                        objFaculty.FacultyUrl = Convert.ToString(Dt.Rows[i]["FacultyUrl"]);
                        objFaculty.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
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

        #region Programme Part Name List on the basis of Programme Instance Id
        [HttpPost]
        public HttpResponseMessage ProgrammePartGetByProgInstId(ProgrammeInstancePartTerm ObjProgInstPartTerm)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("MstProgrammePartGetByProgInstId", Con);
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
                        ObjProgInstPart.Id = Convert.ToInt32(dt.Rows[i]["ProgrammeInstancePartId"]);
                        ObjProgInstPart.FacultyId = Convert.ToInt32(dt.Rows[i]["FacultyId"]);
                        ObjProgInstPart.FacultyName = Convert.ToString(dt.Rows[i]["FacultyName"]);
                        ObjProgInstPart.ProgrammeId = Convert.ToInt32(dt.Rows[i]["ProgrammeId"]);
                        ObjProgInstPart.ProgrammeName = Convert.ToString(dt.Rows[i]["ProgrammeName"]);
                        ObjProgInstPart.ProgrammePartId = Convert.ToInt32(dt.Rows[i]["ProgrammePartId"]);
                        ObjProgInstPart.ProgrammeInstanceId = Convert.ToInt64(dt.Rows[i]["ProgrammeInstanceId"]);
                        // ObjProgInstPart.PartName = Convert.ToString(dt.Rows[i]["PartName"]);
                        ObjProgInstPart.PartShortName = Convert.ToString(dt.Rows[i]["PartShortName"]);
                        // ObjProgInstPart.PartName = Convert.ToString(dt.Rows[i]["PartName"]);
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

        #region Programme Part Name List on the basis of Programme Instance Id----- NEW
        [HttpPost]
        public HttpResponseMessage ProgrammePartGetByProgInstanceId(ProgrammeInstancePartTerm ObjProgInstPartTerm)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("MstProgrammePartGetByProgInstanceId", Con);
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
                        ObjProgInstPart.Id = Convert.ToInt32(dt.Rows[i]["ProgrammeInstancePartId"]);
                                       
                        ObjProgInstPart.ProgrammePartId = Convert.ToInt32(dt.Rows[i]["ProgrammePartId"]);
                        ObjProgInstPart.PartShortName = Convert.ToString(dt.Rows[i]["PartShortName"]);
                        // ObjProgInstPart.PartName = Convert.ToString(dt.Rows[i]["PartName"]);
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

        #region Branch List On the basis of Programme Instance Id
        [HttpPost]
        public HttpResponseMessage MstProgrammeBranchListGetByProgInstanceId(ProgramInstance ObjProg)
        {
            try
            {
                int ProgInstId = Convert.ToInt32(ObjProg.Id);
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter cmdda = new SqlDataAdapter("MstSpecialisationGetbyProgInstId", Con);

                cmdda.SelectCommand.CommandType = CommandType.StoredProcedure;
                cmdda.SelectCommand.Parameters.AddWithValue("@ProgrammeInstanceId", ProgInstId);

                DataTable Dt = new DataTable();
                cmdda.Fill(Dt);


                List<MstSpecialisation> ObjLstMstSpecialisation = new List<MstSpecialisation>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstSpecialisation ObjMstSpecialisation = new MstSpecialisation();

                        ObjMstSpecialisation.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        ObjMstSpecialisation.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"]));
                        ObjMstSpecialisation.FacultyId = Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        ObjMstSpecialisation.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"]));
                        ObjMstSpecialisation.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        ObjMstSpecialisation.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjMstSpecialisation.IsDeleted = Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);

                        ObjLstMstSpecialisation.Add(ObjMstSpecialisation);
                    }

                }

                return Return.returnHttp("200", ObjLstMstSpecialisation, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region Programme Part Term Name List on the basis of Programme Instance Id
        [HttpPost]
        public HttpResponseMessage ProgrammePartTermGetByProgInstId(ProgrammeInstancePartTerm ObjProgInstPartTerm)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("MstProgrammePartTermGetByProgrammeInstanceId", Con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@ProgrammeInstancePartId", ObjProgInstPartTerm.ProgrammeInstancePartId);
                da.SelectCommand.Parameters.AddWithValue("@SpecialisationId", ObjProgInstPartTerm.SpecialisationId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<ProgrammeInstancePartTerm> ObjLstProgPartTerm = new List<ProgrammeInstancePartTerm>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePartTerm ObjProgPartTerm = new ProgrammeInstancePartTerm();

                        //ObjProgPartTerm.FacultyId = Convert.ToInt32(dt.Rows[i]["FacultyId"]);
                        //ObjProgPartTerm.FacultyName = Convert.ToString(dt.Rows[i]["FacultyName"]);
                        //ObjProgPartTerm.ProgrammeName = Convert.ToString(dt.Rows[i]["ProgrammeName"]);
                        //ObjProgPartTerm.ProgrammeId = Convert.ToInt32(dt.Rows[i]["ProgrammeId"]);
                        ObjProgPartTerm.Id = Convert.ToInt32(dt.Rows[i]["ProgrammeInstancePartTermId"]);
                        ObjProgPartTerm.ProgrammePartId = Convert.ToInt32(dt.Rows[i]["PartId"]);
                        ObjProgPartTerm.PartTermName = Convert.ToString(dt.Rows[i]["PartTermName"]);
                        //ObjProgPartTerm.PartShortName = Convert.ToString(dt.Rows[i]["PartShortName"]);
                        ObjProgPartTerm.PartTermShortName = Convert.ToString(dt.Rows[i]["PartTermShortName"]);
                        //ObjProgPartTerm.SequenceNo = Convert.ToInt32(dt.Rows[i]["SequenceNo"]);
                        //ObjProgPartTerm.IsActive = Convert.ToBoolean(dt.Rows[i]["IsActive"]);
                        //ObjProgPartTerm.IsActiveSts = Convert.ToString(dt.Rows[i]["IsActiveSts"]);
                        //ObjProgPartTerm.IsDeleted = Convert.ToBoolean(dt.Rows[i]["IsDeleted"]);
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

        #region Attached Paper List to a particular Programme Instance Part Term
        [HttpPost]
        public HttpResponseMessage AttachedPaperList(IncProgInstPartTermPaperMap ObjProgInstPartTermPaper)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("MstPaperGetByProgInstPartTermId", Con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                //da.SelectCommand.Parameters.AddWithValue("@ProgrammeInstanceId", ObjProgInstPartTermPaper.ProgrammeInstanceId);
                //da.SelectCommand.Parameters.AddWithValue("@PartTermId", ObjProgInstPartTermPaper.ProgrammePartTermId);
                da.SelectCommand.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ObjProgInstPartTermPaper.ProgrammeInstancePartTermId);
                
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<IncProgInstPartTermPaperMap> ObjLstPaper = new List<IncProgInstPartTermPaperMap>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IncProgInstPartTermPaperMap ObjMPaper = new IncProgInstPartTermPaperMap();

                        ObjMPaper.Id = Convert.ToInt32(dt.Rows[i]["PartTermPaperMapId"]);
                        ObjMPaper.ProgrammeInstancePartTermId = Convert.ToInt64(dt.Rows[i]["IncProgrammeInstancePartTermId"]);
                        ObjMPaper.PaperId = Convert.ToInt64(dt.Rows[i]["PaperId"]);
                        ObjMPaper.PaperName = Convert.ToString(dt.Rows[i]["PaperName"]);
                        ObjMPaper.PaperCode = Convert.ToString(dt.Rows[i]["PaperCode"]);
                        ObjMPaper.FacultyId = Convert.ToInt32(dt.Rows[i]["FacultyId"]);
                        ObjMPaper.FacultyName = Convert.ToString(dt.Rows[i]["FacultyName"]);
                        ObjMPaper.SubjectId = Convert.ToInt32(dt.Rows[i]["SubjectId"]);
                        ObjMPaper.SubjectName = Convert.ToString(dt.Rows[i]["SubjectName"]);
                        ObjMPaper.EvaluationId = Convert.ToInt32(dt.Rows[i]["EvaluationId"]);
                        ObjMPaper.EvaluationName = Convert.ToString(dt.Rows[i]["EvaluationName"]);
                        ObjMPaper.IsActiveSts = Convert.ToString(dt.Rows[i]["IsActiveSts"]);
                        ObjMPaper.IsCreditSts = Convert.ToString(dt.Rows[i]["IsCreditSts"]);
                        ObjMPaper.IsSeparatePassingHeadSts = Convert.ToString(dt.Rows[i]["IsSepHeadSts"]);
                        ObjMPaper.Credits = Convert.ToDecimal(dt.Rows[i]["Credits"]);
                        ObjMPaper.MaxMarks = Convert.ToInt32(dt.Rows[i]["MaxMarks"]);
                        ObjMPaper.MinMarks = Convert.ToInt32(dt.Rows[i]["MinMarks"]);
                        //ObjMPaper.NoOfLecturesPerWeek = Convert.ToInt32(dt.Rows[i]["NoOfLecturesPerWeek"]);
                        //ObjProgPartTerm.CreatedBy = Convert.ToInt64(dt.Rows[i]["CreatedBy"]);
                        //ObjProgPartTerm.CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]);
                        //ObjProgPartTerm.ModifiedBy = Convert.ToInt64(dt.Rows[i]["ModifiedBy"]);
                        //ObjProgPartTerm.ModifiedOn = Convert.ToDateTime(dt.Rows[i]["ModifiedOn"]);

                        ObjLstPaper.Add(ObjMPaper);

                    }
                    return Return.returnHttp("200", ObjLstPaper, null);
                }
                else
                {
                    return Return.returnHttp("200", "No Record Found", null);
                }

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Paper List not attached to a particular Programme Instance Part Term
        [HttpPost]
        public HttpResponseMessage NotAttachedPaperList(IncProgInstPartTermPaperMap ObjProgInstPartTermPaper)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("MstPaperGetNotInProgInstPartTermPaperMap", Con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                //da.SelectCommand.Parameters.AddWithValue("@ProgrammeInstanceId", ObjProgInstPartTermPaper.ProgrammeInstanceId);
                //da.SelectCommand.Parameters.AddWithValue("@PartTermId", ObjProgInstPartTermPaper.ProgrammePartTermId);
                da.SelectCommand.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ObjProgInstPartTermPaper.ProgrammeInstancePartTermId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<MstPaper> ObjLstPaper = new List<MstPaper>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        MstPaper ObjMPaper = new MstPaper();

                        ObjMPaper.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjMPaper.PaperName = Convert.ToString(dt.Rows[i]["PaperName"]);
                        ObjMPaper.PaperCode = Convert.ToString(dt.Rows[i]["PaperCode"]);
                        ObjMPaper.FacultyId = Convert.ToInt32(dt.Rows[i]["FacultyId"]);
                        ObjMPaper.FacultyName = Convert.ToString(dt.Rows[i]["FacultyName"]);
                        ObjMPaper.SubjectId = Convert.ToInt32(dt.Rows[i]["SubjectId"]);
                        ObjMPaper.SubjectName = Convert.ToString(dt.Rows[i]["SubjectName"]);
                        ObjMPaper.EvaluationId = Convert.ToInt32(dt.Rows[i]["EvaluationId"]);
                        ObjMPaper.EvaluationName = Convert.ToString(dt.Rows[i]["EvaluationName"]);
                        ObjMPaper.IsActiveSts = Convert.ToString(dt.Rows[i]["IsActiveSts"]);
                        ObjMPaper.IsCreditSts = Convert.ToString(dt.Rows[i]["IsCreditSts"]);
                        ObjMPaper.IsSeparatePassingHeadSts = Convert.ToString(dt.Rows[i]["IsSepHeadSts"]);
                        ObjMPaper.Credits = Convert.ToDecimal(dt.Rows[i]["Credits"]);
                        ObjMPaper.MaxMarks = Convert.ToInt32(dt.Rows[i]["MaxMarks"]);
                        ObjMPaper.MinMarks = Convert.ToInt32(dt.Rows[i]["MinMarks"]);
                        
                        //ObjProgPartTerm.CreatedBy = Convert.ToInt64(dt.Rows[i]["CreatedBy"]);
                        //ObjProgPartTerm.CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]);
                        //ObjProgPartTerm.ModifiedBy = Convert.ToInt64(dt.Rows[i]["ModifiedBy"]);
                        //ObjProgPartTerm.ModifiedOn = Convert.ToDateTime(dt.Rows[i]["ModifiedOn"]);

                        ObjLstPaper.Add(ObjMPaper);

                    }
                    return Return.returnHttp("200", ObjLstPaper, null);
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

        #region Insertion of Record into IncProgInstPartTermPaperMap table
        [HttpPost]
        public HttpResponseMessage IncProgInstPartTermPaperMapAdd(IncProgInstPartTermPaperMap incprogpmap)
        {
            try
            {
                Validation validation = new Validation();
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                //Int64 ProgrammeInstanceId = Convert.ToInt32(incprogpmap.ProgrammeInstanceId.ToString());
                Int64 ProgrammeInstancePartTermId = Convert.ToInt32(incprogpmap.ProgrammeInstancePartTermId.ToString());
                Int64 PaperId = Convert.ToInt64(incprogpmap.PaperId.ToString());
                //int PartTermId = Convert.ToInt32(incprogpmap.ProgrammePartTermId);


                SqlCommand cmd = new SqlCommand("IncProgInstPartTermPaperMapAdd", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (String.IsNullOrEmpty(Convert.ToString(ProgrammeInstancePartTermId)))
                {
                    return Return.returnHttp("201", "Please select Programme Instance Part Term", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(PaperId)))
                {
                    return Return.returnHttp("201", "Please enter Paper Name", null);
                }
                else
                {
                    //cmd.Parameters.AddWithValue("@ProgrammeInstanceId", ProgrammeInstanceId);
                    //cmd.Parameters.AddWithValue("@PartTermId", PartTermId);
                    //cmd.Parameters.AddWithValue("@Id", incprogpmap.Id);
                    cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                    cmd.Parameters.AddWithValue("@PaperId", PaperId);
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
                        strMessage = "Your data has been saved successfully.";
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

        #region Deletion of Record from IncProgInstPartTermPaperMap table
        [HttpPost]
        public HttpResponseMessage IncProgInstPartTermPaperMapDelete(IncProgInstPartTermPaperMap incprogpmap)
        {
            try
            {
                Validation validation = new Validation();
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int64 ProgrammeInstancePartTermMapId = Convert.ToInt32(incprogpmap.Id.ToString());


                SqlCommand cmd = new SqlCommand("IncProgInstPartTermPaperMapDelete", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (String.IsNullOrEmpty(Convert.ToString(ProgrammeInstancePartTermMapId)))
                {
                    return Return.returnHttp("201", "Please select Programme Instance Part Term", null);
                }

                else
                {
                    cmd.Parameters.AddWithValue("@Id", ProgrammeInstancePartTermMapId);
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
                        strMessage = "Paper has been Detached successfully.";
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