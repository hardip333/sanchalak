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
    public class MstSpecialisationController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region MstSpecialisationGet
        [HttpPost]
        public HttpResponseMessage MstSpecialisationGet()
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
                List<MstSpecialisation> ObjListSpecial= new List<MstSpecialisation>();
                BALSpecialisation ObjBalSpecialList = new BALSpecialisation();
                ObjListSpecial = ObjBalSpecialList.SpecialisationGet();

                if (ObjListSpecial != null)
                    return Return.returnHttp("200", ObjListSpecial, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch(Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion        

        #region MstSpecialisationGet For Drop Down
        [HttpPost]
        public HttpResponseMessage MstSpecialisationGetForDropDown()
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
                List<MstSpecialisation> ObjListSpecial = new List<MstSpecialisation>();
                BALSpecialisation ObjBalSpecialList = new BALSpecialisation();
                ObjListSpecial = ObjBalSpecialList.SpecialisationGetForDropDown();

                if (ObjListSpecial != null)
                    return Return.returnHttp("200", ObjListSpecial, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion        

        #region MstSpecialisationAdd
        [HttpPost]
        public HttpResponseMessage MstSpecialisationAdd(MstSpecialisation mstsp)
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

                string BranchName = Convert.ToString(mstsp.BranchName);
                Int32 FacultyId = Convert.ToInt32(mstsp.FacultyId.ToString());
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());
                
                SqlCommand cmd = new SqlCommand("MstSpecialisationAdd", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (String.IsNullOrEmpty(Convert.ToString(FacultyId)))
                {
                    return Return.returnHttp("201", "Please select Faculty", null);
                }
                else if (String.IsNullOrWhiteSpace(Convert.ToString(BranchName)))
                {
                    return Return.returnHttp("201", "Please enter Branch Name", null);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                    cmd.Parameters.AddWithValue("@BranchName", BranchName);
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
            catch(Exception e)
            {

                return Return.returnHttp("201", e.Message, null);

            }
        }
        #endregion

        #region MstSpecialisationEdit
        [HttpPost]
        public HttpResponseMessage MstSpecialisationUpdate(MstSpecialisation smst)
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

                Int32 Id = smst.Id;
                Int32 FacultyId = smst.FacultyId;
                string BranchName = smst.BranchName;
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                SqlCommand cmd = new SqlCommand("MstSpecialisationEdit", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (String.IsNullOrEmpty(Convert.ToString(FacultyId)))
                {
                    return Return.returnHttp("201", "Please select Faculty Name", null);
                }
                else if (String.IsNullOrWhiteSpace(Convert.ToString(BranchName)))
                {
                    return Return.returnHttp("201", "Please enter Branch Name", null);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                    cmd.Parameters.AddWithValue("@BranchName", BranchName);
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
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region MstSpecialisationDelete
        [HttpPost]
        public HttpResponseMessage MstSpecialisationDelete(MstSpecialisation ssmst)
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
                Int32 Id = ssmst.Id;
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());
                
                SqlCommand cmd = new SqlCommand("MstSpecialisationDelete", con);
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

        #region MstSpecialisationSuspend
        [HttpPost]
        public HttpResponseMessage MstSpecialisationIsSuspended(MstSpecialisation mstspecial)
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
                
                Int32 Id = Convert.ToInt32(mstspecial.Id);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                SqlCommand cmd = new SqlCommand("MstSpecialisationActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstSpecialisationIsActiveDisable");
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

        #region MstSpecialisationActive
        [HttpPost]
        public HttpResponseMessage MstSpecialisationIsActive(MstSpecialisation mstspecial)
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
                
                Int32 Id = Convert.ToInt32(mstspecial.Id);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                SqlCommand cmd = new SqlCommand("MstSpecialisationActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstSpecialisationIsActiveEnable");
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

        #region MstSpecialisation Get By Id
        [HttpPost]
        public HttpResponseMessage MstSpecialisationGetById(MstSpecialisation ObjSpecial)
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

                MstSpecialisation ObjLstSpecial = new MstSpecialisation();
                BALSpecialisation ObjBalSpecialList = new BALSpecialisation();
                ObjLstSpecial = ObjBalSpecialList.SpecialisationGetById(ObjSpecial.Id);

                if (ObjLstSpecial != null)
                    return Return.returnHttp("200", ObjLstSpecial, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region MstSpecialisation Get By FacultyId
        [HttpPost]
        public HttpResponseMessage MstSpecialisationGetByFacultyId(MstSpecialisation ObjSpecial)
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
                List<MstSpecialisation> ObjLstSpecial = new List<MstSpecialisation>();
                BALSpecialisation ObjBalSpecialList = new BALSpecialisation();
                ObjLstSpecial = ObjBalSpecialList.SpecialisationGetByFacultyId(ObjSpecial.FacultyId);

                if (ObjLstSpecial != null)
                    return Return.returnHttp("200", ObjLstSpecial, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region MstSpecialisation Get By InstituteId
        [HttpPost]
        public HttpResponseMessage MstSpecialisationGetByInstituteId(MstSpecialisation ObjSpecial)
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
                List<MstSpecialisation> ObjLstSpecial = new List<MstSpecialisation>();
                BALSpecialisation ObjBalSpecialList = new BALSpecialisation();
                ObjLstSpecial = ObjBalSpecialList.SpecialisationGetByInstituteId(ObjSpecial.InstituteId);

                if (ObjLstSpecial != null)
                    return Return.returnHttp("200", ObjLstSpecial, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        /*#region MstFacultyGet
        [HttpPost]
        public HttpResponseMessage MstFacultyGet()
        {
            try
            {                
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter sda = new SqlDataAdapter("MstFacultyGet", con);

                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                
                List<MstFaculty> ObjLstMstFaculty = new List<MstFaculty>();

                if(dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        MstFaculty objFaculty = new MstFaculty();

                        objFaculty.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        objFaculty.FacultyName = Convert.ToString(dt.Rows[i]["FacultyName"]);
                        objFaculty.FacultyCode = Convert.ToString(dt.Rows[i]["FacultyCode"]);
                        objFaculty.FacultyAddress = Convert.ToString(dt.Rows[i]["FacultyAddress"]);
                        objFaculty.CityName = Convert.ToString(dt.Rows[i]["CityName"]);
                        objFaculty.Pincode = Convert.ToInt32(dt.Rows[i]["Pincode"]);
                        objFaculty.FacultyContactNo = Convert.ToString(dt.Rows[i]["FacultyContactNo"]);
                        objFaculty.FacultyFaxNo = Convert.ToString(dt.Rows[i]["FacultyFaxNo"]);
                        objFaculty.FacultyEmail = Convert.ToString(dt.Rows[i]["FacultyEmail"]);
                        objFaculty.FacultyUrl = Convert.ToString(dt.Rows[i]["FacultyUrl"]);
                        objFaculty.IsActive = Convert.ToBoolean(dt.Rows[i]["IsActive"]);
                        ObjLstMstFaculty.Add(objFaculty);
                    }
                }
                return Return.returnHttp("200", ObjLstMstFaculty, null);
            }
            catch(Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion*/

    }
}