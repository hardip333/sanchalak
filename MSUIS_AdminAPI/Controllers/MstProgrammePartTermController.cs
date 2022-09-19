using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;
using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using MSUISApi.BAL;
using Newtonsoft.Json.Linq;

namespace MSUISApi.Controllers
{
    public class MstProgrammePartTermController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region MstProgrammePartTermListGet
        [HttpPost]
        public HttpResponseMessage MstProgrammePartTermListGet()
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

                List<MstProgrammePartTerm> ObjLstProgPartTerm = new List<MstProgrammePartTerm>();
                BALProgrammePartTerm ObjBalProgPartTermList = new BALProgrammePartTerm();
                ObjLstProgPartTerm = ObjBalProgPartTermList.ProgrammePartTermGet();

                if (ObjLstProgPartTerm != null)
                    return Return.returnHttp("200", ObjLstProgPartTerm, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region MstProgrammePartGet
        [HttpPost]
        public HttpResponseMessage MstProgrammePartGet()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter sda = new SqlDataAdapter("MstProgrammePartGet", con);

                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                sda.Fill(dt);

                List<MstProgrammePart> ObjLstProgPart = new List<MstProgrammePart>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstProgrammePart objPP = new MstProgrammePart();

                        objPP.Id = Convert.ToInt32(dr["Id"].ToString());
                        objPP.PartName = Convert.ToString(dr["PartName"].ToString());
                        objPP.PartShortName = Convert.ToString(dr["PartShortName"].ToString());
                        objPP.SequenceNo = Convert.ToInt32(dr["SequenceNo"]);
                        objPP.NoOfTerms = Convert.ToInt32(dr["NoOfTerms"].ToString());
                        objPP.FacultyId = Convert.ToInt32(dr["FacultyId"].ToString());
                        objPP.FacultyName = Convert.ToString(dr["FacultyName"].ToString());
                        objPP.ProgrammeId = Convert.ToInt32(dr["ProgrammeId"].ToString());
                        objPP.ProgrammeName = Convert.ToString(dr["ProgrammeName"].ToString());
                        objPP.ExamPatternId = Convert.ToInt32(dr["ExamPatternId"].ToString());
                        objPP.ExaminationPatternName = Convert.ToString(dr["ExaminationPatternName"].ToString());
                        objPP.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
                        objPP.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        //objPP.IsActiveSts = Convert.ToString(dr["IsActiveSts"].ToString());

                        ObjLstProgPart.Add(objPP);
                    }
                }
                return Return.returnHttp("200", ObjLstProgPart, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region MstProgrammePartGetByProgrammeId
        [HttpPost]
        public HttpResponseMessage MstProgrammePartGetByProgrammeId(MstProgrammePart ObjProgPart)
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

                List<MstProgrammePart> ObjLstProgPart = new List<MstProgrammePart>();
                BALProgrammePart ObjBalProgPartList = new BALProgrammePart();
                ObjLstProgPart = ObjBalProgPartList.ProgrammePartGetByProgrammeId(ObjProgPart.ProgrammeId);

                if (ObjLstProgPart != null)
                    return Return.returnHttp("200", ObjLstProgPart, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Programme Part Term Get By Id
        [HttpPost]
        public HttpResponseMessage MstProgrammePartTermGetbyId(MstProgrammePartTerm programmePartTerm)
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

                MstProgrammePartTerm ObjLstProgPartTerm = new MstProgrammePartTerm();
                BALProgrammePartTerm ObjBalProgPartTermList = new BALProgrammePartTerm();
                ObjLstProgPartTerm = ObjBalProgPartTermList.ProgrammePartTermListGetbyId(programmePartTerm.Id);

                if (ObjLstProgPartTerm != null)
                    return Return.returnHttp("200", ObjLstProgPartTerm, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region MstProgrammeGetByFacultyId
        [HttpPost]
        public HttpResponseMessage MstProgrammeGetByFacultyId(JObject BOS)
        {
            try
            {
                MstProgramme mprog = new MstProgramme();

                dynamic Jsondata = BOS;

                mprog.FacultyId = Convert.ToInt32(Jsondata.FacultyId);
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand("MstProgrammeGetByFacultyId", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", mprog.FacultyId);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = cmd;

                Da.Fill(Dt);

                List<MstProgramme> ObjLstProg = new List<MstProgramme>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgramme mstpro = new MstProgramme();

                        mstpro.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        mstpro.ProgrammeName = Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        mstpro.ProgrammeCode = Convert.ToString(Dt.Rows[i]["ProgrammeCode"]);
                        mstpro.ProgrammeDescription = Convert.ToString(Dt.Rows[i]["ProgrammeDescription"]);
                        mstpro.ProgrammeLevelId = Convert.ToInt32(Dt.Rows[i]["ProgrammeLevelId"]);
                        mstpro.ProgrammeModeId = Convert.ToInt32(Dt.Rows[i]["ProgrammeModeId"]);
                        mstpro.ProgrammeTypeId = Convert.ToInt32(Dt.Rows[i]["ProgrammeTypeId"]);
                        mstpro.InstructionMediumId = Convert.ToInt32(Dt.Rows[i]["InstructionMediumId"]);
                        mstpro.EvaluationId = Convert.ToInt32(Dt.Rows[i]["EvaluationId"]);
                        mstpro.IsCBCS = Convert.ToBoolean(Dt.Rows[i]["IsCBCS"]);
                        mstpro.IsSepartePassingHead = Convert.ToBoolean(Dt.Rows[i]["IsSepartePassingHead"]);
                        mstpro.MinMarks = Convert.ToInt32(Dt.Rows[i]["MinMarks"]);
                        mstpro.MaxMarks = Convert.ToInt32(Dt.Rows[i]["MaxMarks"]);
                        mstpro.MinCredits = Convert.ToDecimal(Dt.Rows[i]["MinCredits"]);
                        mstpro.MaxCredits = Convert.ToDecimal(Dt.Rows[i]["MaxCredits"]);
                        mstpro.FacultyId = Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        mstpro.FacultyName = Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        ObjLstProg.Add(mstpro);
                    }
                }
                return Return.returnHttp("200", ObjLstProg, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region MstProgrammePartTermAdd
        [HttpPost]
        public HttpResponseMessage MstProgrammePartTermAdd(MstProgrammePartTerm ObjMProgPT)
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

                Validation validation = new Validation();

                Int64 PartId = Convert.ToInt32(ObjMProgPT.PartId.ToString());
                string PartTermName = ObjMProgPT.PartTermName;
                string PartTermShortName = ObjMProgPT.PartTermShortName;
                int SequenceNo = Convert.ToInt32(ObjMProgPT.SequenceNo.ToString());
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                SqlCommand cmd = new SqlCommand("MstProgrammePartTermAdd", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (String.IsNullOrEmpty(Convert.ToString(PartId)))
                {
                    return Return.returnHttp("201", "Please select Programme Part", null);
                }
                else if (String.IsNullOrWhiteSpace(Convert.ToString(PartTermName)))
                {
                    return Return.returnHttp("201", "Please enter Part Term Name", null);
                }
                else if (String.IsNullOrWhiteSpace(Convert.ToString(PartTermShortName)))
                {
                    return Return.returnHttp("201", "Please enter paper code", null);
                }
                else if (validation.validateDigit(Convert.ToString(SequenceNo)) == false)
                {
                    return Return.returnHttp("201", "Please enter Maximum marks", null);
                }
                else
                {

                    cmd.Parameters.AddWithValue("@PartId", PartId);
                    cmd.Parameters.AddWithValue("@PartTermName", PartTermName);
                    cmd.Parameters.AddWithValue("@PartTermShortName", PartTermShortName);
                    cmd.Parameters.AddWithValue("@SequenceNo", SequenceNo);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    con.Close();
                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Your data has been added successfully.";
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

        #region MstProgrammePartTermUpdate
        [HttpPost]
        public HttpResponseMessage MstProgrammePartTermUpdate(MstProgrammePartTerm propt)
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

                Validation validation = new Validation();

                Int64 Id = Convert.ToInt32(propt.Id.ToString());
                Int64 PartId = Convert.ToInt32(propt.PartId.ToString());
                string PartTermName = propt.PartTermName;
                string PartTermShortName = propt.PartTermShortName;
                int SequenceNo = Convert.ToInt32(propt.SequenceNo.ToString());
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                SqlCommand cmd = new SqlCommand("MstProgrammePartTermEdit", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (String.IsNullOrEmpty(Convert.ToString(PartId)))
                {
                    return Return.returnHttp("201", "Please select Programme Part", null);
                }
                else if (String.IsNullOrWhiteSpace(Convert.ToString(PartTermName)))
                {
                    return Return.returnHttp("201", "Please enter Part Term Name", null);
                }
                else if (String.IsNullOrWhiteSpace(Convert.ToString(PartTermShortName)))
                {
                    return Return.returnHttp("201", "Please enter paper code", null);
                }
                else if (validation.validateDigit(Convert.ToString(SequenceNo)) == false)
                {
                    return Return.returnHttp("201", "Please enter Maximum marks", null);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@PartId", PartId);
                    cmd.Parameters.AddWithValue("@PartTermName", PartTermName);
                    cmd.Parameters.AddWithValue("@PartTermShortName", PartTermShortName);
                    cmd.Parameters.AddWithValue("@SequenceNo", SequenceNo);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    con.Close();

                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Your data has been modified successfully.";
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

        #region MstProgrammePartTermDelete
        [HttpPost]
        public HttpResponseMessage MstProgrammePartTermDelete(MstProgrammePartTerm mstptte)
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

                Int32 Id = mstptte.Id;
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                SqlCommand cmd = new SqlCommand("MstProgrammePartTermDelete", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been deleted successfully.";
                }

                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region MstProgrammePartTermIsSuspended
        [HttpPost]
        public HttpResponseMessage MstProgrammePartTermIsSuspended(MstProgrammePartTerm programmepartterm)
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

                Int32 Id = Convert.ToInt32(programmepartterm.Id);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                SqlCommand cmd = new SqlCommand("MstProgrammePartTermActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "ProgrammePartTermIsActiveDisable");
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);

                cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@MESSAGE"].Value);
                con.Close();
                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been saved successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region MstProgrammePartTermIsActive
        [HttpPost]
        public HttpResponseMessage MstProgrammePartTermIsActive(MstProgrammePartTerm programmepartterm)
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

                Int32 Id = Convert.ToInt32(programmepartterm.Id);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                SqlCommand cmd = new SqlCommand("MstProgrammePartTermActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "ProgrammePartTermIsActiveEnable");
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);

                cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@MESSAGE"].Value);
                con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been saved successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        // Code Done By Megha at 22 Oct 2021 16.56
        #region MstProgrammePartGetByProgrammeId
        [HttpPost]
        public HttpResponseMessage MstProgrammePartTermGetByProgrammeId(MstProgrammePartTerm ObjMProgPT)
        {
            try
            {
                //String token = Request.Headers.GetValues("token").FirstOrDefault();
                //TokenOperation TokenOp = new TokenOperation();
                //String ValidTok = TokenOp.ValidateToken(token);

                //if (ValidTok == "0")
                //{
                //    return Return.returnHttp("0", null, null);
                //}

                List<MstProgrammePartTerm> ObjLstProgPartTerm = new List<MstProgrammePartTerm>();
                BALProgrammePartTerm ObjBalProgPartTermList = new BALProgrammePartTerm();
                ObjLstProgPartTerm = ObjBalProgPartTermList.ProgrammePartTermGetByProgrammeId(ObjMProgPT.ProgrammeId);

                if (ObjLstProgPartTerm != null)
                    return Return.returnHttp("200", ObjLstProgPartTerm, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        // Code Done By Megha at 21 Dec 2021
        #region MstProgrammePartGetByProgrammeIdAndBranchId 
        [HttpPost]
        public HttpResponseMessage MstProgrammePartTermGetByProgrammeIdAndBranchId(TimeTableMaster dataString)
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

                List<MstProgrammePartTerm> ObjLstProgPartTerm = new List<MstProgrammePartTerm>();
                BALProgrammePartTerm ObjBalProgPartTermList = new BALProgrammePartTerm();
                ObjLstProgPartTerm = ObjBalProgPartTermList.ProgrammePartTermGetByProgrammeIdAndBranchId(dataString.ProgrammeId, dataString.BranchId);

                if (ObjLstProgPartTerm != null)
                    return Return.returnHttp("200", ObjLstProgPartTerm, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage ProgrammePartTermGetByProgrammeIdForCourse(MstProgrammePartTerm dataString)
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

                List<MstProgrammePartTerm> ObjLstProgPartTerm = new List<MstProgrammePartTerm>();
                BALProgrammePartTerm ObjBalProgPartTermList = new BALProgrammePartTerm();
                ObjLstProgPartTerm = ObjBalProgPartTermList.ProgrammePartTermGetByProgrammeIdForCourse(dataString.ProgrammeId);

                if (ObjLstProgPartTerm != null)
                    return Return.returnHttp("200", ObjLstProgPartTerm, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage GetProgrammePartListForExamFeeConfig(CopyExamFeeConfiguration dataString)
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
                dataString.programmePartTermList = new List<MstProgrammePartTerm>();
                // List<MstProgrammePartTerm> ObjLstProgPartTerm = new List<MstProgrammePartTerm>();
                BALProgrammePartTerm ObjBalProgPartTermList = new BALProgrammePartTerm();
                dataString.programmePartTermList = ObjBalProgPartTermList.GetProgrammePartListForExamFeeConfig(dataString.FacultyExamMapId, dataString.ExamMasterId);

                if (dataString.programmePartTermList != null)
                    return Return.returnHttp("200", dataString, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion


        /*#region FacultyGet
       [HttpPost]
       public HttpResponseMessage FacultyGet()
       {
           try
           {

               SqlCommand Cmd = new SqlCommand("MstFacultyGet", con);
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
       #endregion*/

        /*#region MstProgrammeGet
        [HttpPost]
        public HttpResponseMessage MstProgrammeGet()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter sda = new SqlDataAdapter("MstProgrammeGet", con);

                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                
                DataTable dt = new DataTable();
                sda.Fill(dt);

                List<MstProgramme> ProgrammeLists = new List<MstProgramme>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstProgramme prg = new MstProgramme();
                        prg.Id = Convert.ToInt32(dr["Id"].ToString());
                        prg.ProgrammeName = dr["ProgrammeName"].ToString();
                        prg.ProgrammeCode = dr["ProgrammeCode"].ToString();
                        prg.ProgrammeDescription = dr["ProgrammeDescription"].ToString();
                        prg.FacultyId = Convert.ToInt32(dr["FacultyId"].ToString());
                        prg.FacultyName = dr["FacultyName"].ToString();
                        prg.ProgrammeLevelId = Convert.ToInt32(dr["ProgrammeLevelId"].ToString());
                        prg.ProgrammeLevelName = dr["ProgrammeLevelName"].ToString();
                        prg.ProgrammeTypeId = Convert.ToInt32(dr["ProgrammeTypeId"].ToString());
                        prg.ProgrammeTypeName = dr["ProgrammeTypeName"].ToString();
                        prg.ProgrammeModeId = Convert.ToInt32(dr["ProgrammeModeId"].ToString());
                        prg.ProgrammeModeName = dr["ProgrammeModeName"].ToString();
                        prg.EvaluationId = Convert.ToInt32(dr["EvaluationId"].ToString());
                        prg.EvaluationName = dr["EvaluationName"].ToString();
                        prg.InstructionMediumId = Convert.ToInt32(dr["InstructionMediumId"].ToString());
                        prg.InstructionMediumName = dr["InstructionMediumName"].ToString();
                        prg.IsCBCS = Convert.ToBoolean(dr["IsCBCS"].ToString());
                        prg.IsSepartePassingHead = Convert.ToBoolean(dr["IsSepartePassingHead"].ToString());
                        prg.MaxMarks = Convert.ToInt32(dr["MaxMarks"].ToString());
                        prg.MinMarks = Convert.ToInt32(dr["MinMarks"].ToString());
                        prg.MaxCredits = Convert.ToInt32(dr["MinMarks"].ToString());
                        prg.MinCredits = Convert.ToInt32(dr["MinMarks"].ToString());
                        prg.IsActiveSts = dr["IsActiveSts"].ToString();
                        prg.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
                        prg.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());

                        ProgrammeLists.Add(prg);
                    }

                }
                return Return.returnHttp("200", ProgrammeLists, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion*/
    }
}

/*#region MstProgrammePartGetByProgrammeId
[HttpPost]
public HttpResponseMessage MstProgrammePartGetByProgrammeId(JObject BOS)
{
    try
    {
        MstProgrammePart progp = new MstProgrammePart();

        dynamic Jsondata = BOS;

        progp.ProgrammeId = Convert.ToInt32(Jsondata.ProgrammeId);
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlCommand cmd = new SqlCommand("MstProgrammePartGetByProgrammeId", Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ProgrammeId", progp.ProgrammeId);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Da.SelectCommand = cmd;

        Da.Fill(Dt);

        List<MstProgrammePart> ObjLstFST = new List<MstProgrammePart>();

        if (Dt.Rows.Count > 0)
        {
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                MstProgrammePart mstppart = new MstProgrammePart();

                mstppart.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                mstppart.ExamPatternId = Convert.ToInt32(Dt.Rows[i]["ExamPatternId"]);
                mstppart.FacultyId = Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                mstppart.FacultyName = Convert.ToString(Dt.Rows[i]["FacultyName"]);
                mstppart.PartName = Convert.ToString(Dt.Rows[i]["PartName"]);
                mstppart.PartShortName = Convert.ToString(Dt.Rows[i]["PartShortName"]);
                mstppart.SequenceNo = Convert.ToInt32(Dt.Rows[i]["SequenceNo"]);
                mstppart.NoOfTerms = Convert.ToInt32(Dt.Rows[i]["NoOfTerms"]);
                mstppart.ProgrammeId = Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);
                mstppart.ProgrammeName = Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                ObjLstFST.Add(mstppart);
            }
        }
        return Return.returnHttp("200", ObjLstFST, null);
    }
    catch (Exception e)
    {
        return Return.returnHttp("201", e.Message, null);
    }
}
#endregion*/