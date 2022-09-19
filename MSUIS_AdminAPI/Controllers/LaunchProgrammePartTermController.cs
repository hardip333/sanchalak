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

namespace MSUISApi.Controllers
{
    public class LaunchProgrammePartTermController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        Validation validation = new Validation();

        #region InstitutePartTermMapGetForLaunch

        [HttpPost]
        public HttpResponseMessage InstitutePartTermMapGetForLaunch(InstitutePartTermMap objInstPart)
        {
            try
            {

                SqlCommand cmd = new SqlCommand("InstitutePartTermMapGet", con);
                cmd.CommandType = CommandType.StoredProcedure;

                da.SelectCommand = cmd;
                da.Fill(dt);

                List<InstitutePartTermMap> ObjLstInstPartTermMap = new List<InstitutePartTermMap>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        InstitutePartTermMap ObjInstPartTermMap = new InstitutePartTermMap();
                        ObjInstPartTermMap.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjInstPartTermMap.InstituteName = Convert.ToString(dt.Rows[i]["InstituteName"]);
                        //ObjInstPartTermMap.PaperId = Convert.ToInt64(dt.Rows[i]["PaperId"]);
                        //ObjInstPartTermMap.PaperName = Convert.ToString(dt.Rows[i]["PaperName"]);
                        ObjInstPartTermMap.InstancePartTermName = Convert.ToString(dt.Rows[i]["InstancePartTermName"]);

                        ObjLstInstPartTermMap.Add(ObjInstPartTermMap);

                    }
                    return Return.returnHttp("200", ObjLstInstPartTermMap, null);
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

        #region ProgrammeInstancePartTermGet

        [HttpPost]
        public HttpResponseMessage ProgrammeInstancePartTermGet()
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

                List<ProgrammeInstancePartTerm> ObjLstProgInstPartTerm = new List<ProgrammeInstancePartTerm>();
                BALProgrammeInstancePartTerm ObjBalProgInstPartTermList = new BALProgrammeInstancePartTerm();
                ObjLstProgInstPartTerm = ObjBalProgInstPartTermList.ProgrammeInstancePartTermGet();

                if (ObjLstProgInstPartTerm != null)
                    return Return.returnHttp("200", ObjLstProgInstPartTerm, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region ProgrammeInstancePartTermGetByFacIdAndAcadId

        [HttpPost]
        public HttpResponseMessage ProgrammeInstancePartTermGetByFacIdAndAcadId(ProgrammeInstancePartTerm objPartTerm)
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

                List<ProgrammeInstancePartTerm> ObjLstProgInstPartTerm = new List<ProgrammeInstancePartTerm>();
                BALProgrammeInstancePartTerm ObjBalProgInstPartTermList = new BALProgrammeInstancePartTerm();
                ObjLstProgInstPartTerm = ObjBalProgInstPartTermList.ProgrammeInstancePartTermGetByFacIdAndAcadId(objPartTerm.FacultyId,objPartTerm.AcademicYearId);

                if (ObjLstProgInstPartTerm != null)
                    return Return.returnHttp("200", ObjLstProgInstPartTerm, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region ProgrammeInstancePartTermLaunch
        [HttpPost]
        public HttpResponseMessage ProgrammeInstancePartTermLaunch(ProgrammeInstancePartTerm ObjProgInstPartTerm)
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
                SqlCommand cmd = new SqlCommand("IncProgrammeInstancePartTermLaunch", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", ObjProgInstPartTerm.Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);               
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();
                if (strMessage.Equals("TRUE"))
                {
                    return Return.returnHttp("200", "Programme Launched successfully", null);
                }
                else
                {
                    return Return.returnHttp("201", strMessage.ToString(), null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region ProgrammeInstancePartTermUnLaunch
        [HttpPost]
        public HttpResponseMessage ProgrammeInstancePartTermUnLaunch(ProgrammeInstancePartTerm ObjProgInstPartTerm)
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
                SqlCommand cmd = new SqlCommand("IncProgrammeInstancePartTermUnLaunch", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", ObjProgInstPartTerm.Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();
                if (strMessage.Equals("TRUE"))
                {
                    return Return.returnHttp("200", "Programme Un-Launched successfully", null);
                }
                else
                {
                    return Return.returnHttp("201", strMessage.ToString(), null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region Assessment Report Launch
        [HttpPost]
        public HttpResponseMessage AssessmentReportLaunch(ProgrammeInstancePartTerm ObjProgInstPartTerm)
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
                SqlCommand cmd = new SqlCommand("AssessmentReportLaunch", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", ObjProgInstPartTerm.Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();
                if (strMessage.Equals("TRUE"))
                {
                    return Return.returnHttp("200", "Assessment Report Launched successfully", null);
                }
                else
                {
                    return Return.returnHttp("201", strMessage.ToString(), null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region ProgrammeInstancePartTermUnLaunch
        [HttpPost]
        public HttpResponseMessage AssessmentReportUnLaunch(ProgrammeInstancePartTerm ObjProgInstPartTerm)
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
                SqlCommand cmd = new SqlCommand("AssessmentReportUnLaunch", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", ObjProgInstPartTerm.Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();
                if (strMessage.Equals("TRUE"))
                {
                    return Return.returnHttp("200", "Assessment Report Un-Launched successfully", null);
                }
                else
                {
                    return Return.returnHttp("201", strMessage.ToString(), null);
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
