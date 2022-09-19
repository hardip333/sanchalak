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
using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using MSUISApi.BAL;
using Newtonsoft.Json.Linq;

namespace MSUISApi.Controllers
{
    public class MstAssessmentMethodController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region MstAssessmentMethodGet
        [HttpPost]
        public HttpResponseMessage MstAssessmentMethodGet()
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

                List<MstAssessmentMethod> ObjListAM = new List<MstAssessmentMethod>();
                BALAssessmentMethod ObjBalAMList = new BALAssessmentMethod();
                ObjListAM = ObjBalAMList.AssessmentMethodGet();

                if (ObjListAM != null)
                    return Return.returnHttp("200", ObjListAM, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch(Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region MstAssessmentMethodGet For Drop Down
        [HttpPost]
        public HttpResponseMessage MstAssessmentMethodGetForDropDown()
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

                List<MstAssessmentMethod> ObjListAM = new List<MstAssessmentMethod>();
                BALAssessmentMethod ObjBalAMList = new BALAssessmentMethod();
                ObjListAM = ObjBalAMList.AssessmentMethodGetForDropDown();

                if (ObjListAM != null)
                    return Return.returnHttp("200", ObjListAM, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region MstAssessmentMethodAdd
        [HttpPost]
        public HttpResponseMessage MstAssessmentMethodAdd(MstAssessmentMethod Objmstam)
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

                if (String.IsNullOrWhiteSpace(Convert.ToString(Objmstam.AssessmentMethodName)))
                {
                    return Return.returnHttp("201", "Please Type Assessment Method Name", null);
                }
                else
                {
                    string AssessmentMethodName = Convert.ToString(Objmstam.AssessmentMethodName);
                    Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                    SqlCommand cmd = new SqlCommand("MstAssessmentMethodAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@AssessmentMethodName", AssessmentMethodName);
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
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region MstAssessmentMethodEdit
        [HttpPost]
        public HttpResponseMessage MstAssessmentMethodUpdate(MstAssessmentMethod ObjAssM)
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

                if (String.IsNullOrWhiteSpace(Convert.ToString(ObjAssM.AssessmentMethodName)))
                {
                    return Return.returnHttp("201", "Please Type Assessment Method Name", null);
                }
                else
                {
                    Int32 Id = Convert.ToInt32(ObjAssM.Id);
                    string AssessmentMethodName = Convert.ToString(ObjAssM.AssessmentMethodName);
                    Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                    SqlCommand cmd = new SqlCommand("MstAssessmentMethodEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@AssessmentMethodName", AssessmentMethodName);
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
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region MstAssessmentMethodDelete
        [HttpPost]
        public HttpResponseMessage MstAssessmentMethodDelete(MstAssessmentMethod Objmstmethod)
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

                int Id = Convert.ToInt32(Objmstmethod.Id);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                SqlCommand cmd = new SqlCommand("MstAssessmentMethodDelete", con);
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
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region MstAssessmentMethodSuspend
        [HttpPost]
        public HttpResponseMessage MstAssessmentMethodIsSuspended(MstAssessmentMethod mstassessmentmethod)
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

                Int32 Id = Convert.ToInt32(mstassessmentmethod.Id);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                SqlCommand cmd = new SqlCommand("MstAssessmentMethodActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstAssessmentMethodIsActiveDisable");
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

        #region MstAssessmentMethodActive
        [HttpPost]
        public HttpResponseMessage MstAssessmentMethodIsActive(MstAssessmentMethod mstassessmentmethod)
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

                Int32 Id = Convert.ToInt32(mstassessmentmethod.Id);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                SqlCommand cmd = new SqlCommand("MstAssessmentMethodActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstAssessmentMethodIsActiveEnable");
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

        #region MstAssessmentMethod Get By Id
        [HttpPost]
        public HttpResponseMessage MstAssessmentMethodGetbyId(MstAssessmentMethod ObjAM)
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

                MstAssessmentMethod ObjListAM = new MstAssessmentMethod();
                BALAssessmentMethod ObjBalAMList = new BALAssessmentMethod();
                ObjListAM = ObjBalAMList.AssessmentMethodGetById(ObjAM.Id);

                if (ObjListAM != null)
                    return Return.returnHttp("200", ObjListAM, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region MstAssessmentMethod Get By Programme Part Term Id
        [HttpPost]
        public HttpResponseMessage AssessmentGetForDropDownByPartTermId(MstAssessmentMethod ObjAM)
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
                int PartTermId1 = Convert.ToInt32(ObjAM.PartTermId);
                int SpecialisationId1 = Convert.ToInt32(ObjAM.SpecialisationId);                
                
                List<MstAssessmentMethod> ObjListAM = new List<MstAssessmentMethod>();
                BALAssessmentMethod ObjBalAMList = new BALAssessmentMethod();
                ObjListAM = ObjBalAMList.AssessmentGetForDropDownByPartTermId(PartTermId: PartTermId1, SpecialisationId: SpecialisationId1);

                if (ObjListAM != null)
                    return Return.returnHttp("200", ObjListAM, null);
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