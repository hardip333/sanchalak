using MSUIS_TokenManager.App_Start;
using MSUISApi.BAL;
using MSUISApi.Models;
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
    public class MeritListInstanceController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region newCode By Stored Procedure - Megha
        [HttpPost]
        public HttpResponseMessage MeritListInstanceAdd(MeritListInstance ObjMeritListInstance)
        {
            //String token = Request.Headers.GetValues("token").FirstOrDefault();
            //TokenOperation ac = new TokenOperation();

            //string res = ac.ValidateToken(token);

            //if (res == "0")
            //{
            //    return Return.returnHttp("0", null, null);
            //}
            if (String.IsNullOrWhiteSpace(ObjMeritListInstance.Name))
            {
                return Return.returnHttp("201", "Please enter name", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjMeritListInstance.AcademicYearId)))
            {
                return Return.returnHttp("201", "Please select academic year", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjMeritListInstance.ProgrammeInstancePartTermId)))
            {
                return Return.returnHttp("201", "Please select programme instance part term", null);
            }
            else
            {
                try
                {
                    //Int64 UserId = Convert.ToInt64(res.ToString());
                    Int64 UserId = 1;



                    SqlCommand cmd = new SqlCommand("M_MeritInstanceListAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Name", ObjMeritListInstance.Name);
                    cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ObjMeritListInstance.ProgrammeInstancePartTermId);
                    cmd.Parameters.AddWithValue("@AcademicYearId", ObjMeritListInstance.AcademicYearId);
                    cmd.Parameters.AddWithValue("@Round", ObjMeritListInstance.Round);
                    cmd.Parameters.AddWithValue("@RefMeritListId", ObjMeritListInstance.RefMeritListId);
                    cmd.Parameters.AddWithValue("@TotalSeats", ObjMeritListInstance.TotalSeats);
                    cmd.Parameters.AddWithValue("@ConsiderPreference", ObjMeritListInstance.ConsiderPreference);
                   // cmd.Parameters.AddWithValue("@RefMeritListId", ObjMeritListInstance.RefMeritListId);
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
                    return Return.returnHttp("201", e.Message, null);
                }
            }

        }

        [HttpPost]
        public HttpResponseMessage MeritListInstanceEdit(MeritListInstance ObjMeritListInstance)
        {
            //String token = Request.Headers.GetValues("token").FirstOrDefault();
            //TokenOperation ac = new TokenOperation();

            //string res = ac.ValidateToken(token);

            //if (res == "0")
            //{
            //    return Return.returnHttp("0", null, null);
            //}
            if (String.IsNullOrWhiteSpace(ObjMeritListInstance.Name))
            {
                return Return.returnHttp("201", "Please enter name", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjMeritListInstance.AcademicYearId)))
            {
                return Return.returnHttp("201", "Please select academic year", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjMeritListInstance.ProgrammeInstancePartTermId)))
            {
                return Return.returnHttp("201", "Please select programme instance part term", null);
            }
            else
            {
                try
                {
                    // Int64 UserId = Convert.ToInt64(res.ToString());
                    Int64 UserId = 1;

                    int? Id = Convert.ToInt16(ObjMeritListInstance.Id);

                    SqlCommand cmd = new SqlCommand("M_MeritListInstanceEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@Name", ObjMeritListInstance.Name);
                    cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ObjMeritListInstance.ProgrammeInstancePartTermId);
                    cmd.Parameters.AddWithValue("@AcademicYearId", ObjMeritListInstance.AcademicYearId);
                    cmd.Parameters.AddWithValue("@Round", ObjMeritListInstance.Round);
                    cmd.Parameters.AddWithValue("@RefMeritListId", ObjMeritListInstance.RefMeritListId);
                    cmd.Parameters.AddWithValue("@TotalSeats", ObjMeritListInstance.TotalSeats);
                    cmd.Parameters.AddWithValue("@ConsiderPreference", ObjMeritListInstance.ConsiderPreference);
                   // cmd.Parameters.AddWithValue("@RefMeritListId", ObjMeritListInstance.RefMeritListId);
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

        [HttpPost]
        public HttpResponseMessage MeritListInstanceGet(MeritListInstance dataString)
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

                List<MeritListInstance> MeritListInstanceList = new List<MeritListInstance>();
                BALMeritListInstance func = new BALMeritListInstance();
                //flag=1,IsDeleted=0,IsActive=null
                MeritListInstanceList = func.getMeritListInstanceList("admin", 0,null,dataString.ProgrammeInstancePartTermId,dataString.AcademicYearId,dataString.Round,UserId);

                return Return.returnHttp("200", MeritListInstanceList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage MeritListInstanceGetActive(MeritListInstance dataString)
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

                List<MeritListInstance> MeritListInstanceList = new List<MeritListInstance>();
                BALMeritListInstance func = new BALMeritListInstance();
                //flag=1,IsDeleted=0,IsActive=1
                MeritListInstanceList = func.getMeritListInstanceList("admin", 0, 1,dataString.ProgrammeInstancePartTermId,dataString.AcademicYearId, dataString.Round,UserId);

                return Return.returnHttp("200", MeritListInstanceList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage MeritListInstanceIsActive(MeritListInstance dataString)
        {
            //String token = Request.Headers.GetValues("token").FirstOrDefault();
            //TokenOperation ac = new TokenOperation();

            //string res = ac.ValidateToken(token);

            //if (res == "0")
            //{
            //    return Return.returnHttp("0", null, null);
            //}
            try
            {

                Int32 Id = (int)dataString.Id;
               // Int64 UserId = Convert.ToInt64(res.ToString());
                Int64 UserId = 1;

                SqlCommand cmd = new SqlCommand("M_MeritListInstanceActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "M_MeritListInstanceIsActive");
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
                    strMessage = "Your data has been Active Successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }

        [HttpPost]
        public HttpResponseMessage MeritListInstanceIsInactive(MeritListInstance dataString)
        {
            //String token = Request.Headers.GetValues("token").FirstOrDefault();
            //TokenOperation ac = new TokenOperation();

            //string res = ac.ValidateToken(token);

            //if (res == "0")
            //{
            //    return Return.returnHttp("0", null, null);
            //}
            try
            {

                Int32 Id = (int)dataString.Id;
                //Int64 UserId = Convert.ToInt64(res.ToString());
                Int64 UserId = 1;

                SqlCommand cmd = new SqlCommand("M_MeritListInstanceActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "M_MeritListInstanceIsInactive");
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
                    strMessage = "Your data has been Inactive Successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateConfigurationLocked(MeritListInstance dataString)
        {
            //String token = Request.Headers.GetValues("token").FirstOrDefault();
            //TokenOperation ac = new TokenOperation();

            //string res = ac.ValidateToken(token);

            //if (res == "0")
            //{
            //    return Return.returnHttp("0", null, null);
            //}
            try
            {
                Int32 Id = (int)dataString.Id;
                //Int64 UserId = Convert.ToInt64(res.ToString());
                Int64 UserId = 1;

                SqlCommand cmd = new SqlCommand("M_UpdateConfigurationLocked", con);
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
                    strMessage = "Your data has been locked successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateSeatMatrixLocked(MeritListInstance dataString)
        {
            //String token = Request.Headers.GetValues("token").FirstOrDefault();
            //TokenOperation ac = new TokenOperation();

            //string res = ac.ValidateToken(token);

            //if (res == "0")
            //{
            //    return Return.returnHttp("0", null, null);
            //}
            try
            {
                Int32 Id = (int)dataString.Id;
                //Int64 UserId = Convert.ToInt64(res.ToString());
                Int64 UserId = 1;

                SqlCommand cmd = new SqlCommand("M_UpdateSeatMatrixLocked", con);
                cmd.CommandType = CommandType.StoredProcedure;
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
                    strMessage = "Your data has been locked Successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }

        #endregion
    }
}