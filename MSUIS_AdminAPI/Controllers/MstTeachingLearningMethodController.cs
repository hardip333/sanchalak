using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using MSUISApi.BAL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class MstTeachingLearningMethodController : ApiController
    {

        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region MstTeachingLearningMethodGet
        [HttpPost]
        public HttpResponseMessage MstTeachingLearningMethodGet()
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

                List<MstTeachingLearningMethod> ObjListTLM = new List<MstTeachingLearningMethod>();
                BALTeachingLearning ObjBalTLMList = new BALTeachingLearning();
                ObjListTLM = ObjBalTLMList.TeachingLearningGet();

                if (ObjListTLM != null)
                    return Return.returnHttp("200", ObjListTLM, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region MstTeachingLearningMethodGetForDropDown
        [HttpPost]
        public HttpResponseMessage MstTeachingLearningMethodGetForDropDown()
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

                List<MstTeachingLearningMethod> ObjListTLM = new List<MstTeachingLearningMethod>();
                BALTeachingLearning ObjBalTLMList = new BALTeachingLearning();
                ObjListTLM = ObjBalTLMList.TeachingLearningGetForDropDown();

                if (ObjListTLM != null)
                    return Return.returnHttp("200", ObjListTLM, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region MstTeachingLearningMethodAdd
        [HttpPost]
        public HttpResponseMessage MstTeachingLearningMethodAdd(MstTeachingLearningMethod TLM)
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

                if (String.IsNullOrWhiteSpace(Convert.ToString(TLM.TeachingLearningMethodName)))
                {
                    return Return.returnHttp("201", "Please Teaching Learning Method Name", null);
                }
                else
                {

                    String TeachingLearningMethodName = Convert.ToString(TLM.TeachingLearningMethodName);
                    String TeachingLearningMethodCode = Convert.ToString(TLM.TeachingLearningMethodCode);
                    Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                    SqlCommand cmd = new SqlCommand("MstTeachingLearningMethodAdd", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TeachingLearningMethodName", TeachingLearningMethodName);
                    cmd.Parameters.AddWithValue("@TeachingLearningMethodCode", TeachingLearningMethodCode);
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
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region MstTeachingLearningMethodEdit
        [HttpPost]
        public HttpResponseMessage MstTeachingLearningMethodEdit(MstTeachingLearningMethod TLM)
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

                if (String.IsNullOrWhiteSpace(Convert.ToString(TLM.TeachingLearningMethodName)))
                {
                    return Return.returnHttp("201", "Please Teaching Learning Method Name", null);
                }
                else
                {


                    int Id = Convert.ToInt32(TLM.Id);
                    string TeachingLearningMethodName = Convert.ToString(TLM.TeachingLearningMethodName);
                    string TeachingLearningMethodCode = Convert.ToString(TLM.TeachingLearningMethodCode);
                    Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                    SqlCommand cmd = new SqlCommand("MstTeachingLearningMethodEdit", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@TeachingLearningMethodName", TeachingLearningMethodName);
                    cmd.Parameters.AddWithValue("@TeachingLearningMethodCode", TeachingLearningMethodCode);
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

        #region MstTeachingLearningMethodDelete
        [HttpPost]
        public HttpResponseMessage MstTeachingLearningMethodDelete(MstTeachingLearningMethod TLM)
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

                Int32 Id = Convert.ToInt32(TLM.Id);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                SqlCommand cmd = new SqlCommand("MstTeachingLearningMethodDelete", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
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

        #region MstTeachingLearningMethodSuspend
        [HttpPost]
        public HttpResponseMessage MstTeachingLearningMethodIsSuspended(MstTeachingLearningMethod mstlm)
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

                Int32 Id = Convert.ToInt32(mstlm.Id);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                SqlCommand cmd = new SqlCommand("MstTeachingLearningMethodActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstTeachingLearningMethodIsActiveDisable");
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
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

        #region MstTeachingLearningMethodActive
        [HttpPost]
        public HttpResponseMessage MstTeachingLearningMethodIsActive(MstTeachingLearningMethod mstlm)
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

                Int32 Id = Convert.ToInt32(mstlm.Id);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                SqlCommand cmd = new SqlCommand("MstTeachingLearningMethodActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstTeachingLearningMethodIsActiveEnable");
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
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

        #region MstTeachingLearningMethod Get By Id
        [HttpPost]
        public HttpResponseMessage MstTeachingLearningMethodGetbyId(MstTeachingLearningMethod ObjTLM)
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

                MstTeachingLearningMethod ObjListTLM = new MstTeachingLearningMethod();
                BALTeachingLearning ObjBalTLMList = new BALTeachingLearning();
                ObjListTLM = ObjBalTLMList.TeachingLearningMethodGetById(ObjTLM.Id);

                if (ObjListTLM != null)
                    return Return.returnHttp("200", ObjListTLM, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion


        #region MstTeachingLearningMethod Get By Programme Part Term Id
        [HttpPost]
        public HttpResponseMessage TeachingLearningGetForDropDownByPartTermId(MstTeachingLearningMethod ObjTLM)
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
                int PartTermId1 = Convert.ToInt32(ObjTLM.PartTermId);
                int SpecialisationId1 = Convert.ToInt32(ObjTLM.SpecialisationId);
                List<MstTeachingLearningMethod> ObjListTLM = new List<MstTeachingLearningMethod>();
                BALTeachingLearning ObjBalTLMList = new BALTeachingLearning();
                ObjListTLM = ObjBalTLMList.TeachingLearningGetForDropDownByPartTermId(PartTermId: PartTermId1, SpecialisationId: SpecialisationId1);

                if (ObjListTLM != null)
                    return Return.returnHttp("200", ObjListTLM, null);
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
