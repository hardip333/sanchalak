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
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class InstituteBranchMapController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        [HttpPost]
        public HttpResponseMessage InstituteBranchMapGet()
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

                SqlCommand Cmd = new SqlCommand("InstituteBranchMapGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);


                List<InstituteBranchMap> ObjLstIBM = new List<InstituteBranchMap>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        InstituteBranchMap objIBM = new InstituteBranchMap();

                        objIBM.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objIBM.InstituteId = Convert.ToInt32(Dt.Rows[i]["InstituteId"]);
                        objIBM.ProgrammeBranchMapId = Convert.ToInt32(Dt.Rows[i]["ProgrammeBranchMapId"]);
                        objIBM.ProgrammeId = Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);
                        objIBM.SpecialisationId = Convert.ToInt32(Dt.Rows[i]["SpecialisationId"]);
                        objIBM.InstituteName = Convert.ToString(Dt.Rows[i]["InstituteName"]);
                        objIBM.ProgrammeName = Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        objIBM.BranchName = Convert.ToString(Dt.Rows[i]["BranchName"]);
                        objIBM.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjLstIBM.Add(objIBM);
                    }
                }
                return Return.returnHttp("200", ObjLstIBM, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage InstituteBranchMapAdd(InstituteBranchMap IBM)
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
                if (String.IsNullOrEmpty(Convert.ToString(IBM.InstituteId))) { return Return.returnHttp("201", "Please select Institute", null); }
                else if (String.IsNullOrEmpty(Convert.ToString(IBM.ProgrammeId))) { return Return.returnHttp("201", "Please select Programme", null); }
                else
                {
                    string strMessage = null;
                    Int32 InstituteId = Convert.ToInt32(IBM.InstituteId);
                    Int32 ProgrammeId = Convert.ToInt32(IBM.ProgrammeId);
                    //Int32 ProgrammeBranchMapId = Convert.ToInt32(IBM.ProgrammeBranchMapId);
                    //Int32 SpecialisationId = Convert.ToInt32(IBM.SpecialisationId);
                    var SpeList = IBM.SpeList;
                    foreach (MstSpecialisation Spe in SpeList)
                    {
                        if (String.IsNullOrEmpty(Convert.ToString(Spe.ProgrammeBranchMapId))) { return Return.returnHttp("201", "Something Went Wrong Please Try Again", null); }
                        else if (String.IsNullOrEmpty(Convert.ToString(Spe.Id))) { return Return.returnHttp("201", "Something Went Wrong Please Try Again", null); }
                        else
                        {
                            Int32 ProgrammeBranchMapId = Convert.ToInt32(Spe.ProgrammeBranchMapId);
                            Int32 SpecialisationId = Convert.ToInt32(Spe.Id);
                            SqlCommand cmd = new SqlCommand("InstituteBranchMapAdd", Con);
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@InstituteId", InstituteId);
                            cmd.Parameters.AddWithValue("@ProgrammeId", ProgrammeId);
                            cmd.Parameters.AddWithValue("@ProgrammeBranchMapId", ProgrammeBranchMapId);
                            cmd.Parameters.AddWithValue("@SpecialisationId", SpecialisationId);
                            cmd.Parameters.AddWithValue("@UserId", 0);
                            cmd.Parameters.AddWithValue("@UserTime", datetime);

                            cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);

                            cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                            Con.Open();

                            cmd.ExecuteNonQuery();
                            strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);

                            Con.Close();

                        }
                    }
                    return Return.returnHttp("200", strMessage.ToString(), null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage InstituteBranchMapEdit(InstituteBranchMap IBM)
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
                if (String.IsNullOrEmpty(Convert.ToString(IBM.InstituteId))) { return Return.returnHttp("201", "Please select Institute", null); }
                else if (String.IsNullOrEmpty(Convert.ToString(IBM.ProgrammeId))) { return Return.returnHttp("201", "Please select Programme", null); }
                else if (String.IsNullOrEmpty(Convert.ToString(IBM.ProgrammeBranchMapId))) { return Return.returnHttp("201", "Something Went Wrong Please Try Again", null); }
                else if (String.IsNullOrEmpty(Convert.ToString(IBM.SpecialisationId))) { return Return.returnHttp("201", "Please select Specialization", null); }
                else
                {
                    Int32 Id = Convert.ToInt32(IBM.Id);
                    Int32 InstituteId = Convert.ToInt32(IBM.InstituteId);
                    Int32 ProgrammeId = Convert.ToInt32(IBM.ProgrammeId);
                    Int32 ProgrammeBranchMapId = Convert.ToInt32(IBM.ProgrammeBranchMapId);
                    Int32 SpecialisationId = Convert.ToInt32(IBM.SpecialisationId);

                    SqlCommand cmd = new SqlCommand("InstituteBranchMapEdit", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@InstituteId", InstituteId);
                    cmd.Parameters.AddWithValue("@ProgrammeId", ProgrammeId);
                    cmd.Parameters.AddWithValue("@ProgrammeBranchMapId", ProgrammeBranchMapId);
                    cmd.Parameters.AddWithValue("@SpecialisationId", SpecialisationId);
                    cmd.Parameters.AddWithValue("@UserId", 0);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    Con.Open();

                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);

                    Con.Close();

                    return Return.returnHttp("200", strMessage.ToString(), null);
                }

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage InstituteBranchMapIsActiveEnable(InstituteBranchMap IBM)
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

                Int64 Id = Convert.ToInt64(IBM.Id);

                SqlCommand cmd = new SqlCommand("InstituteBranchMapActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", 0);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.AddWithValue("@Flag", "InstituteBranchMapIsActiveEnable");
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                Con.Close();
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }

        [HttpPost]
        public HttpResponseMessage InstituteBranchMapIsActiveDisable(InstituteBranchMap IBM)
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

                Int64 Id = Convert.ToInt64(IBM.Id);

                SqlCommand cmd = new SqlCommand("InstituteBranchMapActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", 0);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.AddWithValue("@Flag", "InstituteBranchMapIsActiveDisable");
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                Con.Close();
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }

        [HttpPost]
        public HttpResponseMessage InstituteBranchMapDelete(InstituteBranchMap IBM)
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


                Int64 Id = Convert.ToInt64(IBM.Id);

                SqlCommand cmd = new SqlCommand("InstituteBranchMapDelete", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", 0);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);

                Con.Close();

                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }

        [HttpPost]
        public HttpResponseMessage InstituteBranchMapGetbyId(InstituteBranchMap IBM)
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
                Int64 Id = Convert.ToInt64(IBM.Id);

                SqlCommand cmd = new SqlCommand("InstituteBranchMapGetbyId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<InstituteBranchMap> ObjLstIBM = new List<InstituteBranchMap>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        InstituteBranchMap objIBM = new InstituteBranchMap();

                        objIBM.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objIBM.InstituteId = Convert.ToInt32(Dt.Rows[i]["InstituteId"]);
                        objIBM.ProgrammeBranchMapId = Convert.ToInt32(Dt.Rows[i]["ProgrammeBranchMapId"]);
                        objIBM.ProgrammeId = Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);
                        objIBM.SpecialisationId = Convert.ToInt32(Dt.Rows[i]["SpecialisationId"]);
                        objIBM.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjLstIBM.Add(objIBM);
                    }
                }
                return Return.returnHttp("200", ObjLstIBM, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }

        #region MstProgrammeListGet
        [HttpPost]
        public HttpResponseMessage MstProgrammeListGet(ProgramInstance ObjProg)
        {
            try
            {
                int FacultyId = Convert.ToInt32(ObjProg.FacultyId);
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter cmdda = new SqlDataAdapter("MstProgrammeGetByFacultyId", Con);

                cmdda.SelectCommand.CommandType = CommandType.StoredProcedure;
                cmdda.SelectCommand.Parameters.AddWithValue("@FacultyId", FacultyId);

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
                        ObjMstProgramme.MaxCredits = (Convert.ToString(Dt.Rows[i]["MaxCredits"])).IsEmpty() ? 0 : Convert.ToDecimal(Dt.Rows[i]["MaxCredits"]);
                        ObjMstProgramme.MinCredits = (Convert.ToString(Dt.Rows[i]["MinCredits"])).IsEmpty() ? 0 : Convert.ToDecimal(Dt.Rows[i]["MinCredits"]);
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

        #region MstProgrammeBranchListGetByProgrammeId
        [HttpPost]
        public HttpResponseMessage MstProgrammeBranchListGetByProgrammeId(InstituteBranchMap IBM)
        {
            try
            {
                int ProgrammeId = Convert.ToInt32(IBM.ProgrammeId);
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter cmdda = new SqlDataAdapter("MstProgrammeBranchMapGetByProgrammeIdwithIBM", Con);

                cmdda.SelectCommand.CommandType = CommandType.StoredProcedure;
                cmdda.SelectCommand.Parameters.AddWithValue("@ProgrammeId", ProgrammeId);

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
                        ObjMstSpecialisation.ProgrammeBranchMapId = Convert.ToInt32(Dt.Rows[i]["ProgrammeBranchMapId"]);
                        ObjMstSpecialisation.SpecialisationSts = Convert.ToBoolean(Dt.Rows[i]["SpecialisationSts"]);

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
    }
}
