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
    public class MstProgrammeController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        Validation validation = new Validation();

        #region Programme List Get
        [HttpPost]
        public HttpResponseMessage MstProgrammeListGet()
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            try
            {
                List<MstProgramme> ObjLstProg = new List<MstProgramme>();
                BALProgramme ObjBalProgList = new BALProgramme();
                ObjLstProg = ObjBalProgList.ProgrammeGet();

                if (ObjLstProg != null)
                    return Return.returnHttp("200", ObjLstProg, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region Programme List Get For Drop Down
        [HttpPost]
        public HttpResponseMessage MstProgrammeListGetForDropDown()
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            try
            {
                List<MstProgramme> ObjLstProg = new List<MstProgramme>();
                BALProgramme ObjBalProgList = new BALProgramme();
                ObjLstProg = ObjBalProgList.ProgrammeGetForDropDown();

                if (ObjLstProg != null)
                    return Return.returnHttp("200", ObjLstProg, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region Programme List Get By Id Pre Config
        [HttpPost]
        public HttpResponseMessage MstProgrammeListGetByIdPre(MstProgramme ObjProgramme)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            try
            {
                MstProgramme Programme = new MstProgramme();

                Programme.ProgrammeInstancePartTermId = ObjProgramme.ProgrammeInstancePartTermId;
                SqlCommand cmd = new SqlCommand("MstProgrammeGetByIdPre", con);
                cmd.CommandType = CommandType.StoredProcedure;
                // SqlCommand cmd = new SqlCommand();
                SqlDataAdapter cmdda = new SqlDataAdapter();
                //cmdda.SelectCommand.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", Programme.ProgrammeInstancePartTermId);
                cmdda.SelectCommand = cmd;
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

        #region Insert Programme Detail
        [HttpPost]
        public HttpResponseMessage MstProgrammeAdd(MstProgramme ObjProgramme)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            if (String.IsNullOrWhiteSpace(Convert.ToString(ObjProgramme.ProgrammeName)))
            {
                return Return.returnHttp("201", "Please enter programme name", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjProgramme.ProgrammeCode)))
            {
                return Return.returnHttp("201", "Please enter programme code", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjProgramme.ProgrammeDescription)))
            {
                return Return.returnHttp("201", "Please enter programme description", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjProgramme.ProgrammeLevelId)))
            {
                return Return.returnHttp("201", "Please select ProgrammeLevel", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjProgramme.ProgrammeTypeId)))
            {
                return Return.returnHttp("201", "Please select ProgrammeType", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjProgramme.ProgrammeModeId)))
            {
                return Return.returnHttp("201", "Please select ProgrammeMode", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjProgramme.InstructionMediumId)))
            {
                return Return.returnHttp("201", "Please select InstructionMedium", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjProgramme.EvaluationId)))
            {
                return Return.returnHttp("201", "Please select Evaluation", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjProgramme.FacultyId)))
            {
                return Return.returnHttp("201", "Please select faculty", null);
            }
            else if (validation.validateDigit(Convert.ToString(ObjProgramme.MaxMarks)) == false)
            {
                return Return.returnHttp("201", "Please enter Maximum marks", null);
            }
            else if ((validation.validateDigit(Convert.ToString(ObjProgramme.MinMarks)) == false) || (ObjProgramme.MinMarks > ObjProgramme.MaxMarks))
            {
                return Return.returnHttp("201", "Please enter Minimum marks and respective of Maximum Marks", null);
            }
            else if (validation.IsNumeric(Convert.ToString(ObjProgramme.MaxCredits)) == false)
            {
                return Return.returnHttp("201", "Please enter Maximum credits", null);
            }
            else if ((validation.IsNumeric(Convert.ToString(ObjProgramme.MinCredits)) == false) || (ObjProgramme.MinCredits > ObjProgramme.MaxCredits))
            {
                return Return.returnHttp("201", "Please enter Minimum credits and respective of Maximum credits", null);
            }
            else if (validation.validateDigit(Convert.ToString(ObjProgramme.ProgrammeDuration)) == false)
            {
                return Return.returnHttp("201", "Please enter programme duration", null);
            }
            else if (validation.validateDigit(Convert.ToString(ObjProgramme.ProgrammeValidity)) == false)
            {
                return Return.returnHttp("201", "Please enter programme validity", null);
            }
            else if (validation.validateDigit(Convert.ToString(ObjProgramme.TotalParts)) == false)
            {
                return Return.returnHttp("201", "Please enter total parts", null);
            }
            else
            {
                try
                {

                    Int64 UserId = Convert.ToInt64(res.ToString());

                    MstProgramme Programme = new MstProgramme();

                    Programme.ProgrammeName = ObjProgramme.ProgrammeName;
                    Programme.ProgrammeCode = ObjProgramme.ProgrammeCode;
                    Programme.ProgrammeDescription = ObjProgramme.ProgrammeDescription;
                    Programme.ProgrammeLevelId = ObjProgramme.ProgrammeLevelId;
                    Programme.ProgrammeTypeId = ObjProgramme.ProgrammeTypeId;
                    Programme.ProgrammeModeId = ObjProgramme.ProgrammeModeId;
                    Programme.InstructionMediumId = ObjProgramme.InstructionMediumId;
                    Programme.EvaluationId = ObjProgramme.EvaluationId;
                    Programme.FacultyId = ObjProgramme.FacultyId;
                    Programme.IsCBCS = ObjProgramme.IsCBCS;
                    Programme.IsSepartePassingHead = false;
                    //Programme.IsSepartePassingHead = ObjProgramme.IsSepartePassingHead;
                    Programme.MaxMarks = ObjProgramme.MaxMarks;
                    Programme.MinMarks = ObjProgramme.MinMarks;
                    Programme.MaxCredits = ObjProgramme.MaxCredits;
                    Programme.MinCredits = ObjProgramme.MinCredits;
                    Programme.ProgrammeDuration = ObjProgramme.ProgrammeDuration;
                    Programme.ProgrammeValidity = ObjProgramme.ProgrammeValidity;
                    Programme.TotalParts = ObjProgramme.TotalParts;

                    SqlCommand cmd = new SqlCommand("MstProgrammeAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    /*if (Convert.ToString(ObjProgramme.MaxMarks) == null)
                    {

                        cmd.Parameters.AddWithValue("@MaxMarks", null);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@MaxMarks", Programme.MaxMarks);
                    }*/

                    cmd.Parameters.AddWithValue("@ProgrammeName", Programme.ProgrammeName);
                    cmd.Parameters.AddWithValue("@ProgrammeCode", Programme.ProgrammeCode);
                    cmd.Parameters.AddWithValue("@Description", Programme.ProgrammeDescription);
                    cmd.Parameters.AddWithValue("@ProgrammeLevelId", Programme.ProgrammeLevelId);
                    cmd.Parameters.AddWithValue("@ProgrammeModeId", Programme.ProgrammeModeId);
                    cmd.Parameters.AddWithValue("@ProgrammeTypeId", Programme.ProgrammeTypeId);
                    cmd.Parameters.AddWithValue("@InstructionMediumId", Programme.InstructionMediumId);
                    cmd.Parameters.AddWithValue("@EvaluationId", Programme.EvaluationId);
                    cmd.Parameters.AddWithValue("@IsCBCS", Programme.IsCBCS);
                    cmd.Parameters.AddWithValue("@IsSepartePassingHead", Programme.IsSepartePassingHead);
                    cmd.Parameters.AddWithValue("@MaxMarks", Programme.MaxMarks);
                    cmd.Parameters.AddWithValue("@MinMarks", Programme.MinMarks);
                    cmd.Parameters.AddWithValue("@MaxCredits", Programme.MaxCredits);
                    cmd.Parameters.AddWithValue("@MinCredits", Programme.MinCredits);
                    cmd.Parameters.AddWithValue("@ProgrammeDuration", Programme.ProgrammeDuration);
                    cmd.Parameters.AddWithValue("@ProgrammeValidity", Programme.ProgrammeValidity);
                    cmd.Parameters.AddWithValue("@TotalParts", Programme.TotalParts);
                    cmd.Parameters.AddWithValue("@FacultyId", Programme.FacultyId);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters.Add("@ProgId", SqlDbType.Int);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                    cmd.Parameters["@ProgId"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    con.Close();
                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Your data has been added successfully.";
                        Programme.Id = Convert.ToInt32(cmd.Parameters["@ProgId"].Value);
                    }
                    return Return.returnHttp("200", (strMessage.ToString(), Programme.Id), null);
                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message, null);
                }
            }

        }
        #endregion

        #region Update Programme Detail
        [HttpPost]
        public HttpResponseMessage MstProgrammeUpdate(MstProgramme ObjProgramme)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            if (String.IsNullOrWhiteSpace(Convert.ToString(ObjProgramme.ProgrammeName)))
            {
                return Return.returnHttp("201", "Please enter programme name", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjProgramme.ProgrammeCode)))
            {
                return Return.returnHttp("201", "Please enter programme code", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjProgramme.ProgrammeDescription)))
            {
                return Return.returnHttp("201", "Please enter programme description", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjProgramme.ProgrammeLevelId)))
            {
                return Return.returnHttp("201", "Please select ProgrammeLevel", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjProgramme.ProgrammeTypeId)))
            {
                return Return.returnHttp("201", "Please select ProgrammeType", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjProgramme.ProgrammeModeId)))
            {
                return Return.returnHttp("201", "Please select ProgrammeMode", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjProgramme.InstructionMediumId)))
            {
                return Return.returnHttp("201", "Please select InstructionMedium", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjProgramme.EvaluationId)))
            {
                return Return.returnHttp("201", "Please select Evaluation", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjProgramme.FacultyId)))
            {
                return Return.returnHttp("201", "Please select faculty", null);
            }
            else if (validation.validateDigit(Convert.ToString(ObjProgramme.MaxMarks)) == false)
            {
                return Return.returnHttp("201", "Please enter Maximum marks", null);
            }
            else if ((validation.validateDigit(Convert.ToString(ObjProgramme.MinMarks)) == false) || (ObjProgramme.MinMarks > ObjProgramme.MaxMarks))
            {
                return Return.returnHttp("201", "Please enter Minimum marks and respective of Maximum Marks", null);
            }
            else if (validation.IsNumeric(Convert.ToString(ObjProgramme.MaxCredits)) == false)
            {
                return Return.returnHttp("201", "Please enter Maximum credits", null);
            }
            else if ((validation.IsNumeric(Convert.ToString(ObjProgramme.MinCredits)) == false) || (ObjProgramme.MinCredits > ObjProgramme.MaxCredits))
            {
                return Return.returnHttp("201", "Please enter Minimum credits and respective of Maximum credits", null);
            }
            else if (validation.validateDigit(Convert.ToString(ObjProgramme.ProgrammeDuration)) == false)
            {
                return Return.returnHttp("201", "Please enter programme duration", null);
            }
            else if (validation.validateDigit(Convert.ToString(ObjProgramme.ProgrammeValidity)) == false)
            {
                return Return.returnHttp("201", "Please enter programme validity", null);
            }
            else if (validation.validateDigit(Convert.ToString(ObjProgramme.TotalParts)) == false)
            {
                return Return.returnHttp("201", "Please enter total parts", null);
            }
            else
            {
                try
                {
                    MstProgramme Programme = new MstProgramme();
                    Programme.Id = ObjProgramme.Id;
                    Programme.ProgrammeName = ObjProgramme.ProgrammeName;
                    Programme.ProgrammeCode = ObjProgramme.ProgrammeCode;
                    Programme.ProgrammeDescription = ObjProgramme.ProgrammeDescription;
                    Programme.ProgrammeLevelId = ObjProgramme.ProgrammeLevelId;
                    Programme.ProgrammeTypeId = ObjProgramme.ProgrammeTypeId;
                    Programme.ProgrammeModeId = ObjProgramme.ProgrammeModeId;
                    Programme.InstructionMediumId = ObjProgramme.InstructionMediumId;
                    Programme.EvaluationId = ObjProgramme.EvaluationId;
                    Programme.FacultyId = ObjProgramme.FacultyId;
                    Programme.IsCBCS = ObjProgramme.IsCBCS;
                    Programme.IsSepartePassingHead = false;
                    Programme.MaxMarks = ObjProgramme.MaxMarks;
                    Programme.MinMarks = ObjProgramme.MinMarks;
                    Programme.MaxCredits = ObjProgramme.MaxCredits;
                    Programme.MinCredits = ObjProgramme.MinCredits;
                    Programme.ProgrammeDuration = ObjProgramme.ProgrammeDuration;
                    Programme.ProgrammeValidity = ObjProgramme.ProgrammeValidity;
                    Programme.TotalParts = ObjProgramme.TotalParts;
                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("MstProgrammeEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ProgrammeId", Programme.Id);
                    cmd.Parameters.AddWithValue("@ProgrammeName", Programme.ProgrammeName);
                    cmd.Parameters.AddWithValue("@ProgrammeCode", Programme.ProgrammeCode);
                    cmd.Parameters.AddWithValue("@Description", Programme.ProgrammeDescription);
                    cmd.Parameters.AddWithValue("@ProgrammeLevelId", Programme.ProgrammeLevelId);
                    cmd.Parameters.AddWithValue("@ProgrammeModeId", Programme.ProgrammeModeId);
                    cmd.Parameters.AddWithValue("@ProgrammeTypeId", Programme.ProgrammeTypeId);
                    cmd.Parameters.AddWithValue("@InstructionMediumId", Programme.InstructionMediumId);
                    cmd.Parameters.AddWithValue("@EvaluationId", Programme.EvaluationId);
                    cmd.Parameters.AddWithValue("@IsCBCS", Programme.IsCBCS);
                    cmd.Parameters.AddWithValue("@IsSepartePassingHead", Programme.IsSepartePassingHead);
                    cmd.Parameters.AddWithValue("@MaxMarks", Programme.MaxMarks);
                    cmd.Parameters.AddWithValue("@MinMarks", Programme.MinMarks);
                    cmd.Parameters.AddWithValue("@MaxCredits", Programme.MaxCredits);
                    cmd.Parameters.AddWithValue("@MinCredits", Programme.MinCredits);
                    cmd.Parameters.AddWithValue("@ProgrammeDuration", Programme.ProgrammeDuration);
                    cmd.Parameters.AddWithValue("@ProgrammeValidity", Programme.ProgrammeValidity);
                    cmd.Parameters.AddWithValue("@TotalParts", Programme.TotalParts);
                    cmd.Parameters.AddWithValue("@FacultyId", Programme.FacultyId);
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

        }
        #endregion

        #region Active Enable For Programme Detail
        [HttpPost]
        public HttpResponseMessage MstProgrammeIsActiveEnable(MstProgramme ObjProgramme)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            try
            {
                Int64 UserId = Convert.ToInt64(res.ToString());

                MstProgramme Programme = new MstProgramme();

                //dynamic JSonData = JSONObject;

                // JObject ProgrammeJSon = JSonData.Programme;

                Programme.Id = ObjProgramme.Id;

                SqlCommand cmd = new SqlCommand("MstProgrammeActive", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeId", Programme.Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.AddWithValue("@Flag", "MstProgrammeIsActiveEnable");
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();
                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been saved successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region Active Disable for Programme Detail
        [HttpPost]
        public HttpResponseMessage MstProgrammeIsActiveDisable(MstProgramme ObjProgramme)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            try
            {

                Int64 UserId = Convert.ToInt64(res.ToString());
                MstProgramme Programme = new MstProgramme();

                //dynamic JSonData = JSONObject;

                // JObject ProgrammeJSon = JSonData.Programme;

                Programme.Id = ObjProgramme.Id;

                SqlCommand cmd = new SqlCommand("MstProgrammeActive", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeId", Programme.Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.AddWithValue("@Flag", "MstProgrammeIsActiveDisable");
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();
                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been saved successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region Delete Programme Detail
        [HttpPost]
        public HttpResponseMessage MstProgrammeDelete(MstProgramme ObjProgramme)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            try
            {
                Int64 UserId = Convert.ToInt64(res.ToString());

                MstProgramme Programme = new MstProgramme();

                //dynamic JSonData = JSONObject;

                // JObject ProgrammeJSon = JSonData.Programme;

                Programme.Id = ObjProgramme.Id;
                //Programme.ModifiedBy = 1;


                SqlCommand cmd = new SqlCommand("MstProgrammeDelete", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeId", Programme.Id);
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

        #region Programme Get By Id
        [HttpPost]
        public HttpResponseMessage MstProgrammeGetbyId(MstProgramme programme)
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

                MstProgramme ObjLstProg = new MstProgramme();
                BALProgramme ObjBalProgList = new BALProgramme();
                ObjLstProg = ObjBalProgList.ProgrammeListGetbyId(programme.Id);

                if (ObjLstProg != null)
                    return Return.returnHttp("200", ObjLstProg, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region Programme Get By InstituteId
        [HttpPost]
        public HttpResponseMessage MstProgrammeGetByInstituteId(MstProgramme ObjProg)
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

                List<MstProgramme> ObjLstProg = new List<MstProgramme>();
                BALProgramme ObjBalProgList = new BALProgramme();
                ObjLstProg = ObjBalProgList.ProgrammeGetByInstituteId(ObjProg.InstituteId);

                if (ObjLstProg != null)
                    return Return.returnHttp("200", ObjLstProg, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);

            }
            catch (Exception e)
            {

                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }

        #endregion        

        #region Programme Get By Faculty Id
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

                List<MstProgramme> ObjLstProg = new List<MstProgramme>();
                BALProgramme ObjBalProgList = new BALProgramme();
                ObjLstProg = ObjBalProgList.ProgrammeGetByFacId(ObjProg.FacultyId);

                if (ObjLstProg != null)
                    return Return.returnHttp("200", ObjLstProg, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);

            }
            catch (Exception e)
            {

                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }

        #endregion

        #region Programme Get By Faculty Id For Schedule
        [HttpPost]
        public HttpResponseMessage MstProgrammeGetByFacultyIdForSchedule(MstProgramme ObjProg)
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

                List<MstProgramme> ObjLstProg = new List<MstProgramme>();
                BALProgramme ObjBalProgList = new BALProgramme();
                ObjLstProg = ObjBalProgList.ProgrammeGetByFacIdForSchedule(ObjProg.FacultyId);

                if (ObjLstProg != null)
                    return Return.returnHttp("200", ObjLstProg, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);

            }
            catch (Exception e)
            {

                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }

        #endregion


        #region Megha Code
        [HttpPost]
        public HttpResponseMessage MstProgrammeMasterGet(MstProgramme dataString)
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

                List<MstProgramme> ProgrammeList = new List<MstProgramme>();
                BALProgramme func = new BALProgramme();
                //flag=1,IsDeleted=0,IsActive=null
                ProgrammeList = func.getProgrammeListByFacultyExamMapId(dataString.FacultyExamMapId);

                return Return.returnHttp("200", ProgrammeList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        /*#region GroupSelectSave
         [HttpPost]
         public HttpResponseMessage ProgrammePartAddDynamically(MstProgramme MPP)
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


                 DataTable PartList = new DataTable();
                 //PaperList.Columns.Add(new DataColumn("Id", typeof(Int64)));
                 PartList.Columns.Add(new DataColumn("PartName", typeof(String)));
                 PartList.Columns.Add(new DataColumn("PartShortName", typeof(String)));
                 PartList.Columns.Add(new DataColumn("ExamPatternId", typeof(Int64)));
                 PartList.Columns.Add(new DataColumn("SequenceNo", typeof(Int64)));
                 PartList.Columns.Add(new DataColumn("TotalNoOfDuration", typeof(Int64)));
                 PartList.Columns.Add(new DataColumn("NoOfParts", typeof(Int64)));




                 foreach (MstProgrammePartData G in MPP.TotalPartsList)
                    {                       
                     PartList.Rows.Add(G.PartName, G.PartShortName, G.ExamPatternId, G.SequenceNo, G.TotalNoOfDuration, G.NoOfTerms);                          
                     }


                     SqlCommand cmd = new SqlCommand("MstProgrammePartAdd", con);
                     cmd.CommandType = CommandType.StoredProcedure;

                     cmd.Parameters.AddWithValue("@DetailInsersion", PartList);

                     string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                     con.Close();
                     if (string.Equals(strMessage, "TRUE"))
                     {
                         strMessage = "Your data has been added successfully.";

                     }


                 return Return.returnHttp("200", (strMessage.ToString()), null);


             }
             catch (Exception e)
             {
                 return Return.returnHttp("201", e.Message, null);
             }
             finally
             {
                 con.Close();
             }
         }
         #endregion*/


        #region FetchProgramme
        public static List<MstProgramme> FetchProgramme()
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            List<MstProgramme> ObjLstProg = new List<MstProgramme>();
            SqlCommand cmd = new SqlCommand();

            cmd = new SqlCommand("MstProgrammeGet", Con);
            cmd.CommandType = CommandType.StoredProcedure;

            Int32 count = 0;

            SqlDataAdapter Da = new SqlDataAdapter();
            DataTable Dt = new DataTable();
            Da.SelectCommand = cmd;
            Da.Fill(Dt);
            if (Dt.Rows.Count > 0)
            {
                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    
                        MstProgramme objProg = new MstProgramme();

                        objProg.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objProg.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        objProg.ProgrammeCode = (Convert.ToString(Dt.Rows[i]["ProgrammeCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeCode"]);
                        objProg.ProgrammeDescription = ((Convert.ToString(Dt.Rows[i]["ProgrammeDescription"])).IsEmpty() ? "" : (Convert.ToString(Dt.Rows[i]["ProgrammeDescription"])));
                        objProg.FacultyId = (((Dt.Rows[i]["FacultyId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        objProg.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objProg.ProgrammeLevelId = (Convert.ToString(Dt.Rows[i]["ProgrammeLevelId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeLevelId"]);
                        objProg.ProgrammeLevelName = (Convert.ToString(Dt.Rows[i]["ProgrammeLevelName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeLevelName"]);
                        objProg.ProgrammeTypeId = (Convert.ToString(Dt.Rows[i]["ProgrammeTypeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeTypeId"]);
                        objProg.ProgrammeTypeName = (Convert.ToString(Dt.Rows[i]["ProgrammeTypeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeTypeName"]);
                        objProg.ProgrammeModeId = (Convert.ToString(Dt.Rows[i]["ProgrammeModeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeModeId"]);
                        objProg.ProgrammeModeName = (Convert.ToString(Dt.Rows[i]["ProgrammeModeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeModeName"]);
                        objProg.EvaluationId = (Convert.ToString(Dt.Rows[i]["EvaluationId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["EvaluationId"]);
                        objProg.EvaluationName = (Convert.ToString(Dt.Rows[i]["EvaluationName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["EvaluationName"]);
                        objProg.InstructionMediumId = (Convert.ToString(Dt.Rows[i]["InstructionMediumId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["InstructionMediumId"]);
                        objProg.InstructionMediumName = (Convert.ToString(Dt.Rows[i]["InstructionMediumName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstructionMediumName"]);
                        objProg.IsCBCS = (Convert.ToString(Dt.Rows[i]["IsCBCS"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsCBCS"]);
                        objProg.IsSepartePassingHead = (Convert.ToString(Dt.Rows[i]["IsSepartePassingHead"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsSepartePassingHead"]);
                        objProg.MaxMarks = (Convert.ToString(Dt.Rows[i]["MaxMarks"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["MaxMarks"]);
                        objProg.MinMarks = (Convert.ToString(Dt.Rows[i]["MinMarks"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["MinMarks"]);
                        objProg.MaxCredits = (Convert.ToString(Dt.Rows[i]["MaxCredits"])).IsEmpty() ? 0 : Convert.ToDecimal(Dt.Rows[i]["MaxCredits"]);
                        objProg.MinCredits = (Convert.ToString(Dt.Rows[i]["MinCredits"])).IsEmpty() ? 0 : Convert.ToDecimal(Dt.Rows[i]["MinCredits"]);
                        objProg.ProgrammeDuration = (Convert.ToString(Dt.Rows[i]["ProgrammeDuration"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeDuration"]);
                        objProg.ProgrammeValidity = (Convert.ToString(Dt.Rows[i]["ProgrammeValidity"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeValidity"]);
                        objProg.TotalParts = (Convert.ToString(Dt.Rows[i]["TotalParts"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["TotalParts"]);                        
                        objProg.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        objProg.IsSeparatePassingHeadSts = (Convert.ToString(Dt.Rows[i]["IsSepPassHeadSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsSepPassHeadSts"]);
                        objProg.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objProg.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);
                        count = count + 1;
                        objProg.IndexId = count;
                        ObjLstProg.Add(objProg);
                    }

                
                
            }
            return ObjLstProg;
        }
        #endregion

        #region FetchPart
        public static List<MstProgrammePart> FetchProgrammePart()
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            List<MstProgrammePart> ObjLstProgPart = new List<MstProgrammePart>();
            Int32 count = 0;
            
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("MstProgrammePartGet", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter Da = new SqlDataAdapter();
            DataTable Dt = new DataTable();
            Da.SelectCommand = cmd;
            Da.Fill(Dt);
            if (Dt.Rows.Count > 0)
            {
                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    MstProgrammePart objProgPart = new MstProgrammePart();
                    objProgPart.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                    objProgPart.ProgrammeId = (Convert.ToString(Dt.Rows[i]["ProgrammeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);
                    objProgPart.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                    objProgPart.ExamPatternId = (Convert.ToString(Dt.Rows[i]["ExamPatternId"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["ExamPatternId"]);
                    objProgPart.ExaminationPatternName = (Convert.ToString(Dt.Rows[i]["ExaminationPatternName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ExaminationPatternName"]);
                    objProgPart.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                    objProgPart.NoOfTerms = (Convert.ToString(Dt.Rows[i]["NoOfTerms"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["NoOfTerms"]);
                    objProgPart.PartName = (Convert.ToString(Dt.Rows[i]["PartName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PartName"]);
                    objProgPart.PartShortName = (Convert.ToString(Dt.Rows[i]["PartShortName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PartShortName"]);
                    objProgPart.SequenceNo = (Convert.ToString(Dt.Rows[i]["SequenceNo"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["SequenceNo"]);
                    objProgPart.TotalNoOfDuration = (Convert.ToString(Dt.Rows[i]["TotalNoOfDuration"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["TotalNoOfDuration"]);
                    objProgPart.FacultyId = (Convert.ToString(Dt.Rows[i]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                    objProgPart.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                    count = count + 1;
                    objProgPart.IndexId = count;
                    ObjLstProgPart.Add(objProgPart);
                    
                }
            }
            return ObjLstProgPart;
        }
        #endregion

        #region Programme List Get
        [HttpPost]
        public HttpResponseMessage MstProgrammePartListGet()
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            try
            {

                List<MstProgramme> ObjLstProg = MstProgrammeController.FetchProgramme();
                List<MstProgrammePart> ObjLstProgPart = MstProgrammeController.FetchProgrammePart();
               
                foreach (MstProgramme P in ObjLstProg)
                {
                    List<MstProgrammePart> PT = ObjLstProgPart.Where(e => e.ProgrammeId == P.Id).ToList();
                    if (PT.Any())
                    {
                        P.TotalPartsList = PT;
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

        #region MstProgrammePartDynamicallyAdd
        [HttpPost]
        public HttpResponseMessage MstProgrammePartDynamicallyAdd(MstProgramme MPP)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                //String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
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
               // MstProgrammePart ObjInstSM = new MstProgrammePart();



                var TotalPartsList = MPP.TotalPartsList;
                
                foreach (MstProgrammePart IPM in TotalPartsList)
                {
                       Int64 ProgrammeId = Convert.ToInt32(MPP.Id);
                       String PartName = Convert.ToString(IPM.PartName);
                       String PartShortName = Convert.ToString(IPM.PartShortName);
                       Int64 ExamPatternId = Convert.ToInt64(IPM.ExamPatternId);                       
                       Int64 TotalNoOfDuration = Convert.ToInt64(IPM.TotalNoOfDuration);
                       Int64 SequenceNo = Convert.ToInt64(IPM.SequenceNo);
                       Int64 NoOfTerms = Convert.ToInt64(IPM.NoOfTerms);
                
                        Int64 UserId = Convert.ToInt64(res.ToString());

                        SqlCommand cmd = new SqlCommand("MstProgrammePartAdd", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@PartName", PartName);
                        cmd.Parameters.AddWithValue("@ProgrammeId", ProgrammeId);
                        cmd.Parameters.AddWithValue("@PartShortName", PartShortName);
                        cmd.Parameters.AddWithValue("@ExamPatternId", ExamPatternId);
                        cmd.Parameters.AddWithValue("@TotalNoOfDuration", TotalNoOfDuration);
                        cmd.Parameters.AddWithValue("@SequenceNo", SequenceNo);
                        cmd.Parameters.AddWithValue("@NoOfTerms", NoOfTerms);
                     
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
                            strMessage = "Your data has been saved successfully.";
                        }



                  
                }



                return Return.returnHttp("200", "Your Data Has Been Add / Modified Successfully", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region MstProgrammePartDynamicallyEdit
        [HttpPost]
        public HttpResponseMessage MstProgrammePartDynamicallyEdit(MstProgramme MPP)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                //String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                string resRoleToken = ac.ValidateRoleToken(userRoleToken);
                string strMessage="";


                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

               
                var TotalPartsList = MPP.TotalPartsList;
                
                foreach (MstProgrammePart IPM in TotalPartsList)
                {
                    Int64 ProgrammeId = Convert.ToInt32(MPP.Id);
                    Int64 Id = Convert.ToInt32(IPM.Id);
                    String PartName = Convert.ToString(IPM.PartName);
                    String PartShortName = Convert.ToString(IPM.PartShortName);
                    Int64 ExamPatternId = Convert.ToInt64(IPM.ExamPatternId);
                    Int64 TotalNoOfDuration = Convert.ToInt64(IPM.TotalNoOfDuration);
                    Int64 SequenceNo = Convert.ToInt64(IPM.SequenceNo);
                    Int64 NoOfTerms = Convert.ToInt64(IPM.NoOfTerms);

                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("MstProgrammePartEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@PartName", PartName);
                    cmd.Parameters.AddWithValue("@ProgrammeId", ProgrammeId);
                    cmd.Parameters.AddWithValue("@PartShortName", PartShortName);
                    cmd.Parameters.AddWithValue("@ExamPatternId", ExamPatternId);
                    cmd.Parameters.AddWithValue("@TotalNoOfDuration", TotalNoOfDuration);
                    cmd.Parameters.AddWithValue("@SequenceNo", SequenceNo);
                    cmd.Parameters.AddWithValue("@NoOfTerms", NoOfTerms);

                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;



                    con.Open();
                    cmd.ExecuteNonQuery();
                     strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    con.Close();
                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Your data has been Updated successfully.";
                    }
                   
                  
                }
                
                
                return Return.returnHttp("200", strMessage.ToString(), null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region MstProgrammePartDynamicallyDelete
        [HttpPost]
        public HttpResponseMessage MstProgrammePartDynamicallyDelete(MstProgramme MPP)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                //String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                string resRoleToken = ac.ValidateRoleToken(userRoleToken);
                string strMessage = "";


                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }


                var TotalPartsList = MPP.TotalPartsList;

                foreach (MstProgrammePart IPM in TotalPartsList)
                {
                    Int64 ProgrammeId = Convert.ToInt32(MPP.Id);
                    Int64 Id = Convert.ToInt32(IPM.Id);
                 

                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("MstProgrammePartDelete", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", Id);    
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;



                    con.Open();
                    cmd.ExecuteNonQuery();
                    strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    con.Close();
                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Your data has been Deleted successfully.";
                    }


                }


                return Return.returnHttp("200", strMessage.ToString(), null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion




    }
}
