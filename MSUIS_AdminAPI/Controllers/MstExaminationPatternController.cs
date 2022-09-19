using Newtonsoft.Json.Linq;
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
using MSUIS_TokenManager.App_Start;
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class MstExaminationPatternController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();

        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region Examination Pattern Get
        [HttpGet]
        public HttpResponseMessage MstExaminationPatternGet()
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

                List<MstExaminationPattern> ObjLstProgPattern = new List<MstExaminationPattern>();
                BALProgrammePattern ObjBalProgPatternList = new BALProgrammePattern();
                ObjLstProgPattern = ObjBalProgPatternList.ProgrammePatternGet();

                if (ObjLstProgPattern != null)
                    return Return.returnHttp("200", ObjLstProgPattern, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region Examination Pattern Get For Drop Down
        [HttpGet]
        public HttpResponseMessage MstExaminationPatternGetForDropDown()
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

                List<MstExaminationPattern> ObjLstProgPattern = new List<MstExaminationPattern>();
                BALProgrammePattern ObjBalProgPatternList = new BALProgrammePattern();
                ObjLstProgPattern = ObjBalProgPatternList.ProgrammePatternGetForDropDown();

                if (ObjLstProgPattern != null)
                    return Return.returnHttp("200", ObjLstProgPattern, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region Examination Pattern Add
        [HttpPost]
        public HttpResponseMessage MstExaminationPatternAdd(MstExaminationPattern examinationPattern)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            if (String.IsNullOrWhiteSpace(Convert.ToString(examinationPattern.ExaminationPatternName)))
            {
                return Return.returnHttp("201", "Please Enter Examination Pattern name", null);
            }
            else
            {
                try
                {                    
                    //dynamic Jsondata = examinationPattern; in () --- JObject examinationPattern
                    string PatternName = Convert.ToString(examinationPattern.ExaminationPatternName);
                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("MstExaminationPatternAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ExaminationPatternName", PatternName);
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
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message.ToString(), null);
                }
            }
        }
        #endregion

        #region Examination Pattern Edit
        [HttpPost]
        public HttpResponseMessage MstExaminationPatternEdit(MstExaminationPattern examinationPattern)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            if (String.IsNullOrWhiteSpace(Convert.ToString(examinationPattern.ExaminationPatternName)))
            {
                return Return.returnHttp("201", "Please Enter Examination Pattern name", null);
            }
            else
            {
                try
                {
                    
                    Int32 Id = Convert.ToInt32(examinationPattern.Id);
                    string PatternName = Convert.ToString(examinationPattern.ExaminationPatternName);
                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("MstExaminationPatternEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Id", Id);
                    cmd.Parameters.AddWithValue("ExaminationPatternName", PatternName);
                    cmd.Parameters.AddWithValue("UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);

                    cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@MESSAGE"].Value);
                    con.Close();

                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Your data has been modified successfully.";
                    }
                    return Return.returnHttp("200", strMessage.ToString(), null);
                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message.ToString(), null);
                }
            }
        }
        #endregion

        #region Examination Pattern Delete
        [HttpPost]
        public HttpResponseMessage MstExaminationPatternDelete(MstExaminationPattern examinationPattern)
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
                
                Int32 Id = Convert.ToInt32(examinationPattern.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("MstExaminationPatternDelete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id",Id);
                cmd.Parameters.AddWithValue("UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);

                cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@MESSAGE"].Value);
                con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been deleted successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region Examination Pattern Suspend
        [HttpPost]
        public HttpResponseMessage MstExaminationPatternIsSuspended(MstExaminationPattern examinationPattern)
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
                Int32 Id = Convert.ToInt32(examinationPattern.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("MstExaminationPatternActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstExaminationPatternIsActiveDisable");
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

        #region Examination Pattern Active
        [HttpPost]
        public HttpResponseMessage MstExaminationPatternIsActive(MstExaminationPattern examinationPattern)
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
                
                Int32 Id = Convert.ToInt32(examinationPattern.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("MstExaminationPatternActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstExaminationPatternIsActiveEnable");
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

        #region Examination Pattern Get By Id
        [HttpPost]
        public HttpResponseMessage MstExaminationPatternGetbyId(MstExaminationPattern programmePattern)
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

                MstExaminationPattern ObjLstProgPattern = new MstExaminationPattern();
                BALProgrammePattern ObjBalProgPatternList = new BALProgrammePattern();
                ObjLstProgPattern = ObjBalProgPatternList.ProgrammePatternListGetbyId(programmePattern.Id);

                if (ObjLstProgPattern != null)
                    return Return.returnHttp("200", ObjLstProgPattern, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion
    }
}
