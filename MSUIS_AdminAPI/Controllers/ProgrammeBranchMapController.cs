using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using MSUISApi.BAL;
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
    public class ProgrammeBranchMapController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        Validation validation = new Validation();

        #region ProgrammeBranchMapGet
        [HttpPost]
        public HttpResponseMessage ProgrammeBranchMapGet()
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

                List<ProgrammeBranchMap> ObjLstPBM = new List<ProgrammeBranchMap>();
                BALProgrammeBranchMap ObjBalPBMList = new BALProgrammeBranchMap();
                ObjLstPBM = ObjBalPBMList.ProgrammeBranchMapGet();

                if (ObjLstPBM != null)
                    return Return.returnHttp("200", ObjLstPBM, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region MstPrgrmListGet
        [HttpPost]
        public HttpResponseMessage MstPrgrmListGet(ProgrammeBranchMap ObjProg)
        {

            try
            {
                int FacultyId = Convert.ToInt32(ObjProg.FacultyId);
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter cmdda = new SqlDataAdapter("MstProgrammeGet", con);

                cmdda.SelectCommand.CommandType = CommandType.StoredProcedure;
                cmdda.SelectCommand.Parameters.AddWithValue("@FacultyId", FacultyId);
                cmdda.SelectCommand.Parameters.AddWithValue("@Flag", "MstProgrammeGetByFaculty");

                DataTable Dt = new DataTable();
                cmdda.Fill(Dt);

                List<MstProgramme> ObjLstMstProgramme = new List<MstProgramme>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgramme ObjMstProgramme = new MstProgramme();

                        ObjMstProgramme.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        ObjMstProgramme.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        ObjMstProgramme.ProgrammeCode = (Convert.ToString(Dt.Rows[i]["ProgrammeCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeCode"]);
                        ObjMstProgramme.ProgrammeDescription = ((Convert.ToString(Dt.Rows[i]["ProgrammeDescription"])).IsEmpty() ? "Not Defined" : (Convert.ToString(Dt.Rows[i]["ProgrammeDescription"])));
                        ObjMstProgramme.FacultyId = (((Dt.Rows[i]["FacultyId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        ObjMstProgramme.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        ObjMstProgramme.ProgrammeLevelId = (Convert.ToString(Dt.Rows[i]["ProgrammeLevelId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeLevelId"]);
                        ObjMstProgramme.ProgrammeLevelName = (Convert.ToString(Dt.Rows[i]["ProgrammeLevelName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeLevelName"]);
                        ObjMstProgramme.ProgrammeTypeId = (Convert.ToString(Dt.Rows[i]["ProgrammeTypeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeTypeId"]);
                        ObjMstProgramme.ProgrammeTypeName = (Convert.ToString(Dt.Rows[i]["ProgrammeTypeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeTypeName"]);
                        ObjMstProgramme.ProgrammeModeId = (Convert.ToString(Dt.Rows[i]["ProgrammeModeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeModeId"]);
                        ObjMstProgramme.ProgrammeModeName = (Convert.ToString(Dt.Rows[i]["ProgrammeModeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeModeName"]);
                        ObjMstProgramme.EvaluationId = (Convert.ToString(Dt.Rows[i]["EvaluationId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["EvaluationId"]);
                        ObjMstProgramme.EvaluationName = (Convert.ToString(Dt.Rows[i]["EvaluationName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["EvaluationName"]);
                        ObjMstProgramme.InstructionMediumId = (Convert.ToString(Dt.Rows[i]["InstructionMediumId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["InstructionMediumId"]);
                        ObjMstProgramme.InstructionMediumName = (Convert.ToString(Dt.Rows[i]["InstructionMediumName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstructionMediumName"]);
                        ObjMstProgramme.IsCBCS = (Convert.ToString(Dt.Rows[i]["IsCBCS"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsCBCS"]);
                        ObjMstProgramme.IsSepartePassingHead = (Convert.ToString(Dt.Rows[i]["IsSepartePassingHead"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsSepartePassingHead"]);
                        ObjMstProgramme.MaxMarks = (Convert.ToString(Dt.Rows[i]["MaxMarks"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["MaxMarks"]);
                        ObjMstProgramme.MinMarks = (Convert.ToString(Dt.Rows[i]["MinMarks"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["MinMarks"]);
                        ObjMstProgramme.MaxCredits = (Convert.ToString(Dt.Rows[i]["MaxCredits"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["MaxCredits"]);
                        ObjMstProgramme.MinCredits = (Convert.ToString(Dt.Rows[i]["MinCredits"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["MinCredits"]);
                        ObjMstProgramme.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        ObjMstProgramme.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjMstProgramme.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);

                        ObjLstMstProgramme.Add(ObjMstProgramme);
                    }

                }

                return Return.returnHttp("200", ObjLstMstProgramme, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region MstProgrammeListGet
        [HttpPost]
        public HttpResponseMessage MstProgrammeListGet()
        {
            try
            {
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter sda = new SqlDataAdapter("MstProgrammeGet", con);

                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@Flag", "MstProgrammeListGet");
                //sda.SelectCommand.Parameters.Add("@Message", SqlDbType.NVarChar).Value = "True";
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
        #endregion

        #region MstSubjectGet
        [HttpPost]
        public HttpResponseMessage MstSubjectGet()
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlCommand Cmd = new SqlCommand("MstSubjectGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);


                List<MstSubject> ObjLstSubject = new List<MstSubject>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstSubject objSubject = new MstSubject();

                        objSubject.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objSubject.SubjectName = Convert.ToString(Dt.Rows[i]["SubjectName"]);
                        // objSubject.BoardOfStudyId = Convert.ToInt32(Dt.Rows[i]["BoardOfStudyId"]);
                        objSubject.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjLstSubject.Add(objSubject);
                    }
                }
                return Return.returnHttp("200", ObjLstSubject, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion        

        #region MstSubSpecialisationGetbyMstSpecialisationId
        [HttpPost]
        public HttpResponseMessage MstSubSpecialisationGetbyMstSpecialisationId(ProgrammeBranchMap ObjProg)
        {
            int SpecialisationId = Convert.ToInt32(ObjProg.SpecialisationId);
            try
            {
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter sda = new SqlDataAdapter("MstSubSpecialisationGetbyMstSpecialisationId", con);

                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                //sda.SelectCommand.Parameters.AddWithValue("@Flag", "MstSubSpecialisationListGet");
                sda.SelectCommand.Parameters.AddWithValue("@SpecialisationId", SpecialisationId);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                List<SubSpecialisation> SubSpecialisationLists = new List<SubSpecialisation>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        SubSpecialisation subspl = new SubSpecialisation();
                        subspl.Id = Convert.ToInt32(dr["Id"].ToString());
                        subspl.SpecialisationId = Convert.ToInt32(dr["SpecialisationId"].ToString());
                        subspl.SubSpecialisationName = dr["SubSpecialisationName"].ToString();
                        //subspl.SubjectName = dr["SubjectName"].ToString();
                        subspl.FacultyName = dr["FacultyName"].ToString();
                        subspl.FacultyId = Convert.ToInt32(dr["FacultyId"]);
                        //subspl.BoardOfStudyId = Convert.ToInt32(dr["BoardOfStudyId"]);
                        //subspl.BoardName = dr["BoardName"].ToString();
                        subspl.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
                        subspl.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        //subspl.IsActiveSts = dr["IsActiveSts"].ToString();
                        SubSpecialisationLists.Add(subspl);
                    }
                }
                return Return.returnHttp("200", SubSpecialisationLists, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region ProgrammeBranchMapAdd

        [HttpPost]
        public HttpResponseMessage ProgrammeBranchMapAdd(ProgrammeBranchMap pbm)
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
                Int32 ProgrammeId = Convert.ToInt32(pbm.ProgrammeId.ToString());
                Int32 SpecialisationId = Convert.ToInt32(pbm.SpecialisationId.ToString());
                //Int32 SubSpecialisationId = 123;
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                SqlCommand cmd = new SqlCommand("MstProgrammeBranchMapAdd", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (String.IsNullOrEmpty(Convert.ToString(ProgrammeId)))
                {
                    return Return.returnHttp("201", "Please select Programme Part", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(SpecialisationId)))
                {
                    return Return.returnHttp("201", "Please enter Part Term Name", null);
                }
                /*else if (String.IsNullOrEmpty(Convert.ToString(SubSpecialisationId)))
                {
                    return Return.returnHttp("201", "Please enter paper code", null);
                }*/
                else
                {
                    cmd.Parameters.AddWithValue("@ProgrammeId", ProgrammeId);
                    cmd.Parameters.AddWithValue("@SpecialisationId", SpecialisationId);
                   // cmd.Parameters.AddWithValue("@SubSpecialisationId", SubSpecialisationId);

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

        #region ProgrammeBranchMapUpdate
        [HttpPost]
        public HttpResponseMessage ProgrammeBranchMapUpdate(ProgrammeBranchMap prbm)
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

                Int32 Id = Convert.ToInt32(prbm.Id.ToString());
                Int32 ProgrammeId = Convert.ToInt32(prbm.ProgrammeId.ToString());
                Int32 SpecialisationId = Convert.ToInt32(prbm.SpecialisationId.ToString());
               // Int32 SubSpecialisationId = 0;
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                SqlCommand cmd = new SqlCommand("MstProgrammeBranchMapEdit", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@ProgrammeId", ProgrammeId);
                cmd.Parameters.AddWithValue("@SpecialisationId", SpecialisationId);
                //cmd.Parameters.AddWithValue("@SubSpecialisationId", SubSpecialisationId);
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
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region ProgrammeBranchMapDelete
        [HttpPost]
        public HttpResponseMessage ProgrammeBranchMapDelete(ProgrammeBranchMap pbmp)
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

                Int32 Id = pbmp.Id;
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                SqlCommand cmd = new SqlCommand("MstProgrammeBranchMapDelete", con);
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

        #region ProgrammeBranchMapIsActiveEnable
        [HttpPost]
        public HttpResponseMessage ProgrammeBranchMapIsActiveEnable(ProgrammeBranchMap ObjProgBranchMap)
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
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                SqlCommand cmd = new SqlCommand("MstProgrammeBranchMapActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstProgrammeBranchMapIsActiveEnable");
                cmd.Parameters.AddWithValue("@Id", ObjProgBranchMap.Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string StrMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                if (StrMessage.Equals("TRUE"))
                {
                    return Return.returnHttp("200", "Your data has been saved successfully.", null);
                }
                else
                {
                    return Return.returnHttp("200", StrMessage.ToString(), null);
                }

            }
            catch (Exception e)
            {
                return Return.returnHttp("200", e.Message, null);
            }
        }
        #endregion

        #region ProgrammeBranchMapIsActiveDisable
        [HttpPost]
        public HttpResponseMessage ProgrammeBranchMapIsActiveDisable(ProgrammeBranchMap ObjProgBranchMap)
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

                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                SqlCommand cmd = new SqlCommand("MstProgrammeBranchMapActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstProgrammeBranchMapIsActiveDisable");
                cmd.Parameters.AddWithValue("@Id", ObjProgBranchMap.Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string StrMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                if (StrMessage.Equals("TRUE"))
                {
                    return Return.returnHttp("200", "Your data has been saved successfully.", null);
                }
                else
                {
                    return Return.returnHttp("200", StrMessage.ToString(), null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("200", e.Message, null);
            }
        }
        #endregion

        #region ProgrammeBranchMap Get By Id
        [HttpPost]
        public HttpResponseMessage ProgrammeBranchMapGetById(ProgrammeBranchMap ObjPBM)
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

                ProgrammeBranchMap ObjLstPBM = new ProgrammeBranchMap();
                BALProgrammeBranchMap ObjBalPBMList = new BALProgrammeBranchMap();
                ObjLstPBM = ObjBalPBMList.ProgrammeBranchMapGetbyId(ObjPBM.Id);

                if (ObjLstPBM != null)
                    return Return.returnHttp("200", ObjLstPBM, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region MstSpecialisationListGetByFacultyId ----------

        [HttpPost]
        public HttpResponseMessage MstBranchListGet(ProgrammeBranchMap ObjSpec)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter cmdda = new SqlDataAdapter("MstSpecialisationGetByFacultyId", con);

                cmdda.SelectCommand.CommandType = CommandType.StoredProcedure;
                cmdda.SelectCommand.Parameters.AddWithValue("@FacultyId", ObjSpec.FacultyId);
                DataTable Dt = new DataTable();
                cmdda.Fill(Dt);


                List<MstSpecialisation> ObjLstMstSpecialisation = new List<MstSpecialisation>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstSpecialisation ObjMstSpecialisation = new MstSpecialisation();

                        ObjMstSpecialisation.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        //ObjMstSpecialisation.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        //ObjMstSpecialisation.ProgrammeCode = (Convert.ToString(Dt.Rows[i]["ProgrammeCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeCode"]);
                        ObjMstSpecialisation.BranchName = ((Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "Not Defined" : (Convert.ToString(Dt.Rows[i]["BranchName"])));
                        ObjMstSpecialisation.FacultyId = (((Dt.Rows[i]["FacultyId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        ObjMstSpecialisation.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                       // ObjMstSpecialisation.ProgrammeId = (Convert.ToString(Dt.Rows[i]["ProgrammeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);
                        ObjMstSpecialisation.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        ObjMstSpecialisation.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjMstSpecialisation.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);

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

        /*#region SubSpecialisationGet
        [HttpPost]
        public HttpResponseMessage SubSpecialisationGet()
        {
            try
            {
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter sda = new SqlDataAdapter("MstSubSpecialisationGet", con);

                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                //sda.SelectCommand.Parameters.AddWithValue("@Flag", "MstSubSpecialisationListGet");
                //sda.SelectCommand.Parameters.Add("@Message", SqlDbType.NVarChar).Value = "True";
                DataTable dt = new DataTable();
                sda.Fill(dt);

                List<SubSpecialisation> SubSpecialisationLists = new List<SubSpecialisation>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        SubSpecialisation subspl = new SubSpecialisation();
                        subspl.Id = Convert.ToInt32(dr["Id"].ToString());
                        //subspl.SubjectId = Convert.ToInt32(dr["SubjectId"].ToString());
                        subspl.SubSpecialisationName = dr["SubSpecialisationName"].ToString();
                        //subspl.SubjectName = dr["SubjectName"].ToString();
                        subspl.FacultyName = dr["FacultyName"].ToString();
                        subspl.FacultyId = Convert.ToInt32(dr["FacultyId"]);
                        //subspl.BoardOfStudyId = Convert.ToInt32(dr["BoardOfStudyId"]);
                        //subspl.BoardName = dr["BoardName"].ToString();
                        subspl.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
                        subspl.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        //subspl.IsActiveSts = dr["IsActiveSts"].ToString();
                        SubSpecialisationLists.Add(subspl);
                    }

                }

                return Return.returnHttp("200", SubSpecialisationLists, null);


            }
            catch (Exception e)
            {

                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion*/
    }
}