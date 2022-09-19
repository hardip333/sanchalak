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
    public class SubSpecialisationController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region SubSpecialisationGet
        [HttpPost]
        public HttpResponseMessage SubSpecialisationGet()
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

                List<SubSpecialisation> ObjListSubSpecial = new List<SubSpecialisation>();
                BALSubSpecialisation ObjBalSubSpecialList = new BALSubSpecialisation();
                ObjListSubSpecial = ObjBalSubSpecialList.SubSpecialisationGet();

                if (ObjListSubSpecial != null)
                    return Return.returnHttp("200", ObjListSubSpecial, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region SubSpecialisationAdd
        [HttpPost]
        public HttpResponseMessage SubSpecialisationAdd(SubSpecialisation susp)
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
                
                Int32 SpecialisationId = Convert.ToInt32(susp.SpecialisationId.ToString());
                string SubSpecialisationName = Convert.ToString(susp.SubSpecialisationName);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());
               
                SqlCommand cmd = new SqlCommand("MstSubSpecialisationAdd", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (String.IsNullOrEmpty(Convert.ToString(SpecialisationId)))
                {
                    return Return.returnHttp("201", "Please select Specialisation Id", null);
                }
                else if (String.IsNullOrWhiteSpace(Convert.ToString(SubSpecialisationName)))
                {
                    return Return.returnHttp("201", "Please enter Sub Specialisation Name", null);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@SpecialisationId", SpecialisationId);
                    cmd.Parameters.AddWithValue("@SubSpecialisationName", SubSpecialisationName);
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
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region SubSpecialisationUpdate
        [HttpPost]
        public HttpResponseMessage SubSpecialisationUpdate(SubSpecialisation subp)
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

                Int32 SpecialisationId = subp.SpecialisationId;
                string SubSpecialisationName = subp.SubSpecialisationName;
                Int32 Id = subp.Id;
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());
                
                SqlCommand cmd = new SqlCommand("MstSubSpecialisationEdit", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (String.IsNullOrEmpty(Convert.ToString(SpecialisationId)))
                {
                    return Return.returnHttp("201", "Please select Programme Part", null);
                }
                else if (String.IsNullOrWhiteSpace(Convert.ToString(SubSpecialisationName)))
                {
                    return Return.returnHttp("201", "Please enter Part Term Name", null);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@SpecialisationId", SpecialisationId);
                    cmd.Parameters.AddWithValue("@SubSpecialisationName", SubSpecialisationName);
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

        #region SubSpecialisationDelete
        [HttpPost]
        public HttpResponseMessage SubSpecialisationDelete(SubSpecialisation sspn)
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
                Int32 Id = sspn.Id;
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());
                
                SqlCommand cmd = new SqlCommand("MstSubSpecialisationDelete", con);
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

        #region SubSpecialisationIsSuspended
        [HttpPost]
        public HttpResponseMessage SubSpecialisationIsSuspended(SubSpecialisation subspecial)
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
                
                Int32 Id = Convert.ToInt32(subspecial.Id);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                SqlCommand cmd = new SqlCommand("MstSubSpecialisationActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstSubSpecialisationIsActiveDisable");
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

        #region SubSpecialisationIsActive
        [HttpPost]
        public HttpResponseMessage SubSpecialisationIsActive(SubSpecialisation subspecial)
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
                
                Int32 Id = Convert.ToInt32(subspecial.Id);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                SqlCommand cmd = new SqlCommand("MstSubSpecialisationActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstSubSpecialisationIsActiveEnable");
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

        #region SubSpecialisation Get By Id
        [HttpPost]
        public HttpResponseMessage SubSpecialisationGetById(SubSpecialisation ObjSubSpecial)
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

                SubSpecialisation ObjListSubSpecial = new SubSpecialisation();
                BALSubSpecialisation ObjBalSubSpecialList = new BALSubSpecialisation();
                ObjListSubSpecial = ObjBalSubSpecialList.SubSpecialisationGetById(ObjSubSpecial.Id);

                if (ObjListSubSpecial != null)
                    return Return.returnHttp("200", ObjListSubSpecial, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region SubSpecialisation Get By SpecialisationId
        [HttpPost]
        public HttpResponseMessage SubSpecialisationGetBySpecialisationId(SubSpecialisation ObjSubSpecial)
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

                List<SubSpecialisation> ObjListSubSpecial = new List<SubSpecialisation>();
                BALSubSpecialisation ObjBalSubSpecialList = new BALSubSpecialisation();
                ObjListSubSpecial = ObjBalSubSpecialList.SubSpecialisationGetBySpecialisationId(ObjSubSpecial.SpecialisationId);

                if (ObjListSubSpecial != null)
                    return Return.returnHttp("200", ObjListSubSpecial, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        /*#region SpecialisationGet
        [HttpPost]
        public HttpResponseMessage SpecialisationGet()
        {
            try
            {                               
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter sda = new SqlDataAdapter("MstSpecialisationGet", con);

                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                
                DataTable dt = new DataTable();
                sda.Fill(dt);

                List<MstSpecialisation> ObjLstMstSpecialisation = new List<MstSpecialisation>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstSpecialisation ObjMstSp = new MstSpecialisation();
                        ObjMstSp.Id = Convert.ToInt32(dr["Id"].ToString());
                        ObjMstSp.BranchName = dr["BranchName"].ToString();
                        ObjMstSp.FacultyId = Convert.ToInt32(dr["FacultyId"].ToString());
                        ObjMstSp.FacultyName = dr["FacultyName"].ToString();
                        
                        ObjMstSp.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
                        ObjMstSp.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        //subspl.IsActiveSts = dr["IsActiveSts"].ToString();
                        ObjLstMstSpecialisation.Add(ObjMstSp);
                    }

                }
                return Return.returnHttp("200", ObjLstMstSpecialisation, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion*/
    }
}