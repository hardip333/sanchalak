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
    public class MstProgrammeTypeController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region Programme Type Get
        [HttpPost]
        public HttpResponseMessage MstProgrammeTypeListGet()
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
                List<MstProgrammeType> ObjLstProgType = new List<MstProgrammeType>();
                BALProgrammeType ObjBalProgTypeList = new BALProgrammeType();
                ObjLstProgType = ObjBalProgTypeList.ProgrammeTypeGet();

                if (ObjLstProgType != null)
                    return Return.returnHttp("200", ObjLstProgType, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Programme Type Get For Drop Down
        [HttpPost]
        public HttpResponseMessage MstProgrammeTypeListGetForDropDown()
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
                List<MstProgrammeType> ObjLstProgType = new List<MstProgrammeType>();
                BALProgrammeType ObjBalProgTypeList = new BALProgrammeType();
                ObjLstProgType = ObjBalProgTypeList.ProgrammeTypeGetForDropDown();

                if (ObjLstProgType != null)
                    return Return.returnHttp("200", ObjLstProgType, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Programme Type Add
        [HttpPost]
        public HttpResponseMessage MstProgrammeTypeAdd(MstProgrammeType ProgrammeType)
        {

            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            if (String.IsNullOrWhiteSpace(Convert.ToString(ProgrammeType.ProgrammeTypeName)))
            {
                return Return.returnHttp("201", "Please Enter Programme Type name", null);
            }
            else
            {
                try
                {
                    Con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString;
                    string ProgrammeTypeName = Convert.ToString(ProgrammeType.ProgrammeTypeName);
                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("MstProgrammeTypeAdd", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ProgrammeTypeName", ProgrammeTypeName);
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

        #region Programme Type Edit
        [HttpPost]
        public HttpResponseMessage MstProgrammeTypeEdit(MstProgrammeType ProgrammeType)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            if (String.IsNullOrWhiteSpace(Convert.ToString(ProgrammeType.ProgrammeTypeName)))
            {
                return Return.returnHttp("201", "Please Enter Programme Type name", null);
            }
            else
            {
                try
                {
                    Con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString;

                    Int32 Id = Convert.ToInt32(ProgrammeType.Id);
                    string ProgrammeTypeName = Convert.ToString(ProgrammeType.ProgrammeTypeName);
                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("MstProgrammeTypeEdit", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Id", Id);
                    cmd.Parameters.AddWithValue("ProgrammeTypeName", ProgrammeTypeName);
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

        #region Programme Type Delete
        [HttpPost]
        public HttpResponseMessage MstProgrammeTypeDelete(MstProgrammeType ProgrammeType)
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
                Int32 Id = Convert.ToInt32(ProgrammeType.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("MstProgrammeTypeDelete", Con);
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

        #region Programme Type Suspend
        [HttpPost]
        public HttpResponseMessage MstProgrammeTypeIsSuspended(MstProgrammeType ProgrammeType)
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
                Int32 Id = Convert.ToInt32(ProgrammeType.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("MstProgrammeTypeActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstProgrammeTypeIsActiveDisable");
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

        #region Programme Type Active
        [HttpPost]
        public HttpResponseMessage MstProgrammeTypeIsActive(MstProgrammeType ProgrammeType)
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
                Int32 Id = Convert.ToInt32(ProgrammeType.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("MstProgrammeTypeActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstProgrammeTypeIsActiveEnable");
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

        #region Programme Type Get By Id
        [HttpPost]
        public HttpResponseMessage MstProgrammeTypeGetbyId(MstProgrammeType programmeType)
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

                MstProgrammeType ObjLstProgType = new MstProgrammeType();
                BALProgrammeType ObjBalProgTypeList = new BALProgrammeType();
                ObjLstProgType = ObjBalProgTypeList.ProgrammeTypeListGetbyId(programmeType.Id);

                if (ObjLstProgType != null)
                    return Return.returnHttp("200", ObjLstProgType, null);
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
