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
    public class MstInstituteController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region MstInstituteGet
        [HttpPost]
        public HttpResponseMessage MstInstituteGet()
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

                List<MstInstitute> ObjLstInstitute = new List<MstInstitute>();
                BALInstituteList ObjBalInstituteList = new BALInstituteList();
                ObjLstInstitute = ObjBalInstituteList.InstituteListGet();

                if (ObjLstInstitute != null)
                    return Return.returnHttp("200", ObjLstInstitute, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);                
            }
            catch(Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region MstInstituteGet By Drop Down
        [HttpPost]
        public HttpResponseMessage MstInstituteGetForDropDown()
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

                List<MstInstitute> ObjLstInstitute = new List<MstInstitute>();
                BALInstituteList ObjBalInstituteList = new BALInstituteList();
                ObjLstInstitute = ObjBalInstituteList.InstituteListGetForDropDown();

                if (ObjLstInstitute != null)
                    return Return.returnHttp("200", ObjLstInstitute, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region MstInstituteGet By Institute Type
        [HttpPost]
        public HttpResponseMessage MstInstituteGetByInstituteType()
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

                List<MstInstitute> ObjLstInstitute = new List<MstInstitute>();
                BALInstituteList ObjBalInstituteList = new BALInstituteList();
                ObjLstInstitute = ObjBalInstituteList.InstituteListGetByInstituteType();

                if (ObjLstInstitute != null)
                    return Return.returnHttp("200", ObjLstInstitute, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region MstInstituteAdd
        [HttpPost]
        public HttpResponseMessage MstInstituteAdd(MstInstitute ObjInst)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            if (String.IsNullOrWhiteSpace(Convert.ToString(ObjInst.InstituteName)))
            {
                return Return.returnHttp("201", "Please enter Institute name", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjInst.InstituteCode)))
            {
                return Return.returnHttp("201", "Please enter Institute code", null);
            }
            else
            {
                try
                {
                    String InstituteName = Convert.ToString(ObjInst.InstituteName);
                    String InstituteCode = Convert.ToString(ObjInst.InstituteCode);
                    String InstituteAddress = Convert.ToString(ObjInst.InstituteAddress);
                    String CityName = Convert.ToString(ObjInst.CityName);
                    int Pincode = Convert.ToInt32(ObjInst.Pincode);
                    String InstituteContactNo = Convert.ToString(ObjInst.InstituteContactNo);
                    String InstituteFaxNo = Convert.ToString(ObjInst.InstituteFaxNo);
                    String InstituteEmail = Convert.ToString(ObjInst.InstituteEmail);
                    String InstituteUrl = Convert.ToString(ObjInst.InstituteUrl);
                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("MstInstituteAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@InstituteName", InstituteName);
                    cmd.Parameters.AddWithValue("@InstituteType", ObjInst.InstituteType);
                    cmd.Parameters.AddWithValue("@InstituteCode", InstituteCode);
                    cmd.Parameters.AddWithValue("@InstituteAddress", InstituteAddress);
                    cmd.Parameters.AddWithValue("@CityName", CityName);
                    cmd.Parameters.AddWithValue("@Pincode", Pincode);
                    cmd.Parameters.AddWithValue("@InstituteContactNo", InstituteContactNo);
                    cmd.Parameters.AddWithValue("@InstituteFaxNo", InstituteFaxNo);
                    cmd.Parameters.AddWithValue("@InstituteEmail", InstituteEmail);
                    cmd.Parameters.AddWithValue("@InstituteUrl", InstituteUrl);
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
                    return Return.returnHttp("200", strMessage.ToString(), null);

                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message, null);
                }
            }
        }
        #endregion

        #region MstInstituteUpdate
        [HttpPost]
        public HttpResponseMessage MstInstituteUpdate(MstInstitute ObjMInst)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            if (String.IsNullOrWhiteSpace(Convert.ToString(ObjMInst.InstituteName)))
            {
                return Return.returnHttp("201", "Please enter Institute name", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjMInst.InstituteCode)))
            {
                return Return.returnHttp("201", "Please enter Institute code", null);
            }
            else
            {
                try
                {
                    Int32 Id = Convert.ToInt32(ObjMInst.Id.ToString());
                    String InstituteName = Convert.ToString(ObjMInst.InstituteName);
                    String InstituteType = Convert.ToString(ObjMInst.InstituteType);
                    String InstituteCode = Convert.ToString(ObjMInst.InstituteCode);
                    String InstituteAddress = Convert.ToString(ObjMInst.InstituteAddress);
                    String CityName = Convert.ToString(ObjMInst.CityName);
                    int Pincode = Convert.ToInt32(ObjMInst.Pincode);
                    String InstituteContactNo = Convert.ToString(ObjMInst.InstituteContactNo);
                    String InstituteFaxNo = Convert.ToString(ObjMInst.InstituteFaxNo);
                    String InstituteEmail = Convert.ToString(ObjMInst.InstituteEmail);
                    String InstituteUrl = Convert.ToString(ObjMInst.InstituteUrl);
                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("MstInstituteEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@InstituteName", InstituteName);
                    cmd.Parameters.AddWithValue("@InstituteType", InstituteType);
                    cmd.Parameters.AddWithValue("@InstituteCode", InstituteCode);
                    cmd.Parameters.AddWithValue("@InstituteAddress", InstituteAddress);
                    cmd.Parameters.AddWithValue("@CityName", CityName);
                    //cmd.Parameters.AddWithValue("@Pincode", Pincode);
                    if (Pincode == 0)
                    {
                        cmd.Parameters.AddWithValue("@Pincode", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Pincode", Pincode);
                    }
                    cmd.Parameters.AddWithValue("@InstituteContactNo", InstituteContactNo);
                    cmd.Parameters.AddWithValue("@InstituteFaxNo", InstituteFaxNo);
                    cmd.Parameters.AddWithValue("@InstituteEmail", InstituteEmail);
                    cmd.Parameters.AddWithValue("@InstituteUrl", InstituteUrl);
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
                    return Return.returnHttp("200", strMessage.ToString(), null);

                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message, null);
                }
            }
        }
        #endregion

        #region MstInstituteDelete
        [HttpPost]
        public HttpResponseMessage MstInstituteDelete(MstInstitute ObjMstInt)
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
                Int32 Id = ObjMstInt.Id;
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("MstInstituteDelete", con);
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

        #region MstInstituteIsSuspended
        [HttpPost]
        public HttpResponseMessage MstInstituteIsSuspended(MstInstitute ObjMstInstitute)
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
                
                Int32 Id = Convert.ToInt32(ObjMstInstitute.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("MstInstituteActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstInstituteIsActiveDisable");
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

        #region MstInstituteIsActive
        [HttpPost]
        public HttpResponseMessage MstInstituteIsActive(MstInstitute ObjMstInstitute)
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

                Int32 Id = Convert.ToInt32(ObjMstInstitute.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("MstInstituteActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstInstituteIsActiveEnable");
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

        #region Institute Get By Id
        [HttpPost]
        public HttpResponseMessage MstInstituteGetbyId(MstInstitute Institute)
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

                MstInstitute ObjLstInstitute = new MstInstitute();
                BALInstituteList ObjBalInstituteList = new BALInstituteList();
                ObjLstInstitute = ObjBalInstituteList.InstituteGetbyId(Institute.Id);

                if (ObjLstInstitute != null)
                    return Return.returnHttp("200", ObjLstInstitute, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region  Megha Code
        [HttpPost]
        public HttpResponseMessage MstInstituteGetByFacIdAndInsType(MstInstitute dataString)
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

                List<MstInstitute> ObjLstInstitute = new List<MstInstitute>();
                BALInstituteList ObjBalInstituteList = new BALInstituteList();
                if (string.IsNullOrEmpty(dataString.FacultyId.ToString()))
                {
                    return Return.returnHttp("201", "Plese select faculty", null);
                }
                ObjLstInstitute = ObjBalInstituteList.InstituteListGetByFacIdAndInsType(dataString.FacultyId);

                if (ObjLstInstitute != null)
                    return Return.returnHttp("200", ObjLstInstitute, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Institute Get By Branch And PartTerm
        [HttpPost]
        public HttpResponseMessage InstituteGetForDropDownByPartTermId(MstInstitute ObjInst)
        {
            try
            {

                List<MstInstitute> ObjLstInstitute = new List<MstInstitute>();
                BALInstituteList ObjBalInstituteList = new BALInstituteList();
                ObjLstInstitute = ObjBalInstituteList.InstituteGetForDropDownByPartTermId(ObjInst.BranchId,ObjInst.ProgrammePartTermId);

                if (ObjLstInstitute != null)
                    return Return.returnHttp("200", ObjLstInstitute, null);
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