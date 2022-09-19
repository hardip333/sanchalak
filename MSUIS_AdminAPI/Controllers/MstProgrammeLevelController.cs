using System;
using System.Collections.Generic;
using System.Data;
using MSUISApi.Models;
using MSUISApi.BAL;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MSUIS_TokenManager.App_Start;
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class MstProgrammeLevelController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region Programme Level Get
        [HttpPost]
        public HttpResponseMessage MstProgrammeLevelListGet()
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
                List<MstProgrammeLevel> ObjLstProgLevel = new List<MstProgrammeLevel>();
                BALProgrammeLevel ObjBalProgLevelList = new BALProgrammeLevel();
                ObjLstProgLevel = ObjBalProgLevelList.ProgrammeLevelGet();

                if (ObjLstProgLevel != null)
                    return Return.returnHttp("200", ObjLstProgLevel, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
                
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Programme Level Get For Drop Down
        [HttpPost]
        public HttpResponseMessage MstProgrammeLevelListGetForDropDown()
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
                List<MstProgrammeLevel> ObjLstProgLevel = new List<MstProgrammeLevel>();
                BALProgrammeLevel ObjBalProgLevelList = new BALProgrammeLevel();
                ObjLstProgLevel = ObjBalProgLevelList.ProgrammeLevelGetForDropDown();

                if (ObjLstProgLevel != null)
                    return Return.returnHttp("200", ObjLstProgLevel, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Programme Level Add
        [HttpPost]
        public HttpResponseMessage MstProgrammeLevelAdd(MstProgrammeLevel programmeLevel)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            if (String.IsNullOrWhiteSpace(Convert.ToString(programmeLevel.ProgrammeLevelName)))
            {
                return Return.returnHttp("201", "Please Enter Programme Level name", null);
            }
            else
            {
                try
                {
                    Con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString;
                    string PatternName = Convert.ToString(programmeLevel.ProgrammeLevelName);
                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("MstProgrammeLevelAdd", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@programmeLevelName", PatternName);
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
                        strMessage = "Your data has been added successfully.";
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

        #region Programme Level Edit
        [HttpPost]
        public HttpResponseMessage MstProgrammeLevelEdit(MstProgrammeLevel programmeLevel)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            if (String.IsNullOrWhiteSpace(Convert.ToString(programmeLevel.ProgrammeLevelName)))
            {
                return Return.returnHttp("201", "Please Enter Programme Level name", null);
            }
            else
            {
                try
                {
                    Con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString;
           
                    Int32 Id = Convert.ToInt32(programmeLevel.Id);
                    string PatternName = Convert.ToString(programmeLevel.ProgrammeLevelName);
                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("MstProgrammeLevelEdit", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Id", Id);
                    cmd.Parameters.AddWithValue("programmeLevelName", PatternName);
                    cmd.Parameters.AddWithValue("UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);

                    cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                    Con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@MESSAGE"].Value);
                    Con.Close();

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

        #region Programme Level Delete
        [HttpPost]
        public HttpResponseMessage MstProgrammeLevelDelete(MstProgrammeLevel programmeLevel)
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
                Con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString;
                Int32 Id = Convert.ToInt32(programmeLevel.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("MstProgrammeLevelDelete", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", Id);
                cmd.Parameters.AddWithValue("UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);

                cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@MESSAGE"].Value);
                Con.Close();

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

        #region Programme Level Suspend
        [HttpPost]
        public HttpResponseMessage MstProgrammeLevelIsSuspended(MstProgrammeLevel programmeLevel)
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
                Con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString;
                Int32 Id = Convert.ToInt32(programmeLevel.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("MstProgrammeLevelActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstProgrammeLevelIsActiveDisable");
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@MESSAGE"].Value);
                Con.Close();
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

        #region Programme Level Active
        [HttpPost]
        public HttpResponseMessage MstProgrammeLevelIsActive(MstProgrammeLevel programmeLevel)
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
                Con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString;
                Int32 Id = Convert.ToInt32(programmeLevel.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("MstProgrammeLevelActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstProgrammeLevelIsActiveEnable");
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@MESSAGE"].Value);
                Con.Close();

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

        #region Programme Level Get By Id
        [HttpPost]
        public HttpResponseMessage MstProgrammeLevelGetbyId(MstProgrammeLevel programmeLevel)
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

                MstProgrammeLevel ObjLstProgLevel = new MstProgrammeLevel();
                BALProgrammeLevel ObjBalProgLevelList = new BALProgrammeLevel();
                ObjLstProgLevel = ObjBalProgLevelList.ProgrammeLevelListGetbyId(programmeLevel.Id);

                if (ObjLstProgLevel != null)
                    return Return.returnHttp("200", ObjLstProgLevel, null);
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
