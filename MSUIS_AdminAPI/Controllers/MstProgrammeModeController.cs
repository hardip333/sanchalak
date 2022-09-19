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
    public class MstProgrammeModeController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region Programme Mode Get
        [HttpPost]
        public HttpResponseMessage MstProgrammeModeListGet()
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
                List<MstProgrammeMode> ObjLstProgMode = new List<MstProgrammeMode>();
                BALProgrammeMode ObjBalProgModeList = new BALProgrammeMode();
                ObjLstProgMode = ObjBalProgModeList.ProgrammeModeGet();

                if (ObjLstProgMode != null)
                    return Return.returnHttp("200", ObjLstProgMode, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Programme Mode Get For Drop Down
        [HttpPost]
        public HttpResponseMessage MstProgrammeModeListGetForDropDown()
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
                List<MstProgrammeMode> ObjLstProgMode = new List<MstProgrammeMode>();
                BALProgrammeMode ObjBalProgModeList = new BALProgrammeMode();
                ObjLstProgMode = ObjBalProgModeList.ProgrammeModeGetForDropDown();

                if (ObjLstProgMode != null)
                    return Return.returnHttp("200", ObjLstProgMode, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Programme Mode Add
        [HttpPost]
        public HttpResponseMessage MstProgrammeModeAdd(MstProgrammeMode ProgrammeMode)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            if (String.IsNullOrWhiteSpace(Convert.ToString(ProgrammeMode.ProgrammeModeName)))
            {
                return Return.returnHttp("201", "Please Enter Programme Mode name", null);
            }
            else
            {
                try
                {
                    Con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString;
                    string ProgrammeModeName = Convert.ToString(ProgrammeMode.ProgrammeModeName);
                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("MstProgrammeModeAdd", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ProgrammeModeName", ProgrammeModeName);
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
                    return Return.returnHttp("201", e.Message.ToString(), null);
                }
            }
        }
        #endregion

        #region Programme Mode Edit
        [HttpPost]
        public HttpResponseMessage MstProgrammeModeEdit(MstProgrammeMode ProgrammeMode)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            if (String.IsNullOrWhiteSpace(Convert.ToString(ProgrammeMode.ProgrammeModeName)))
            {
                return Return.returnHttp("201", "Please Enter Programme Mode name", null);
            }
            else
            {
                try
                {
                    Con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString;

                    Int32 Id = Convert.ToInt32(ProgrammeMode.Id);
                    string ProgrammeModeName = Convert.ToString(ProgrammeMode.ProgrammeModeName);
                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("MstProgrammeModeEdit", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Id", Id);
                    cmd.Parameters.AddWithValue("ProgrammeModeName", ProgrammeModeName);
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

        #region Programme Mode Delete
        [HttpPost]
        public HttpResponseMessage MstProgrammeModeDelete(MstProgrammeMode ProgrammeMode)
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
                Int32 Id = Convert.ToInt32(ProgrammeMode.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("MstProgrammeModeDelete", Con);
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

        #region Programme Mode Suspend
        [HttpPost]
        public HttpResponseMessage MstProgrammeModeIsSuspended(MstProgrammeMode ProgrammeMode)
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
                Int32 Id = Convert.ToInt32(ProgrammeMode.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("MstProgrammeModeActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstProgrammeModeIsActiveDisable");
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

        #region Programme Mode Active
        [HttpPost]
        public HttpResponseMessage MstProgrammeModeIsActive(MstProgrammeMode ProgrammeMode)
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
                Int32 Id = Convert.ToInt32(ProgrammeMode.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("MstProgrammeModeActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstProgrammeModeIsActiveEnable");
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

        #region Programme Mode Get By Id
        [HttpPost]
        public HttpResponseMessage MstProgrammeModeGetbyId(MstProgrammeMode programmeMode)
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

                MstProgrammeMode ObjLstProgMode = new MstProgrammeMode();
                BALProgrammeMode ObjBalProgModeList = new BALProgrammeMode();
                ObjLstProgMode = ObjBalProgModeList.ProgrammeModeListGetbyId(programmeMode.Id);

                if (ObjLstProgMode != null)
                    return Return.returnHttp("200", ObjLstProgMode, null);
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

