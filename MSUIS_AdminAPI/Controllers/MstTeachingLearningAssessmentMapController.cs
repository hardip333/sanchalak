//using MSUIS_TokenManager.App_Start;
using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
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
    public class MstTeachingLearningAssessmentMapController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region MstTeachingLearningAssessmentMapGet
        [HttpPost]
        public HttpResponseMessage MstTeachingLearningAssessmentMapGet()
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
                SqlCommand Cmd = new SqlCommand("MstTeachingLearningAssessmentMethodMapGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                Int32 count = 0;
                List<MstTeachingLearningAssessmentMap> ObjLstTLMAM = new List<MstTeachingLearningAssessmentMap>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstTeachingLearningAssessmentMap objTLMAM = new MstTeachingLearningAssessmentMap();

                        objTLMAM.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objTLMAM.TeachingLearningMethodId = (Convert.ToString(Dt.Rows[i]["TlmId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["TlmId"]);
                        objTLMAM.AssessmentMethodId = (Convert.ToString(Dt.Rows[i]["AmId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["AmId"]);
                        objTLMAM.TeachingLearningMethodName = (Convert.ToString(Dt.Rows[i]["TeachingLearningMethodName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["TeachingLearningMethodName"]);
                        objTLMAM.AssessmentMethodName = (Convert.ToString(Dt.Rows[i]["AssessmentMethodName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["AssessmentMethodName"]);
                        objTLMAM.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objTLMAM.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);
                        count = count + 1;
                        objTLMAM.IndexId = count;
                        ObjLstTLMAM.Add(objTLMAM);
                    }
                }
                return Return.returnHttp("200", ObjLstTLMAM, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region MstTeachingLearningAssessmentMapAdd
        [HttpPost]
        public HttpResponseMessage MstTeachingLearningAssessmentMapAdd(MstTeachingLearningAssessmentMap TLMAM)
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

                if (String.IsNullOrEmpty(Convert.ToString(TLMAM.TeachingLearningMethodId)))
                {
                    return Return.returnHttp("201", "Please select TLM", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(TLMAM.AssessmentMethodId)))
                {
                    return Return.returnHttp("201", "Please select AM", null);
                }
                else
                {

                    Int32 TeachingLearningMethodId = Convert.ToInt32(TLMAM.TeachingLearningMethodId);
                    Int32 AssessmentMethodId = Convert.ToInt32(TLMAM.AssessmentMethodId);
                    Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                    SqlCommand cmd = new SqlCommand("MstTeachingLearningAssessmentMethodMapAdd", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TlmId", TeachingLearningMethodId);
                    cmd.Parameters.AddWithValue("@AmId", AssessmentMethodId);
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

        #region MstTeachingLearningAssessmentMapUpdate
        [HttpPost]
        public HttpResponseMessage MstTeachingLearningAssessmentMapUpdate(MstTeachingLearningAssessmentMap TLMAM)
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

                Int32 Id = Convert.ToInt32(TLMAM.Id);
                Int32 TeachingLearningMethodId = Convert.ToInt32(TLMAM.TeachingLearningMethodId);
                Int32 AssessmentMethodId = Convert.ToInt32(TLMAM.AssessmentMethodId);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                SqlCommand cmd = new SqlCommand("MstTeachingLearningAssessmentMethodMapEdit", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@TlmId", TeachingLearningMethodId);
                cmd.Parameters.AddWithValue("@AmId", AssessmentMethodId);
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
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region MstTeachingLearningAssessmentMapDelete
        [HttpPost]
        public HttpResponseMessage MstTeachingLearningAssessmentMapDelete(MstTeachingLearningAssessmentMap TLMAM)
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

                Int32 Id = Convert.ToInt32(TLMAM.Id);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                SqlCommand cmd = new SqlCommand("MstTeachingLearningAssessmentMethodMapDelete", Con);
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

        #region MstTeachingLearningAssessmentMapIsSuspended
        [HttpPost]
        public HttpResponseMessage MstTeachingLearningAssessmentMapIsSuspended(MstTeachingLearningAssessmentMap msttlaMap)
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
                Int32 Id = Convert.ToInt32(msttlaMap.Id);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                SqlCommand cmd = new SqlCommand("MstTeachingLearningAssessmentMethodMapActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstTlmAmMapIsActiveDisable");
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

        #region MstTeachingLearningAssessmentMapIsActive
        [HttpPost]
        public HttpResponseMessage MstTeachingLearningAssessmentMapIsActive(MstTeachingLearningAssessmentMap msttlam)
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

                Int32 Id = Convert.ToInt32(msttlam.Id);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                SqlCommand cmd = new SqlCommand("MstTeachingLearningAssessmentMethodMapActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstTlmAmMapIsActiveEnable");
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

        /*#region MstTeachingLearningMethodGet
        [HttpPost]
        public HttpResponseMessage MstTeachingLearningMethodGet()
        {
            try
            {
                
                SqlCommand Cmd = new SqlCommand("MstTeachingLearningMethodGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);


                List<MstTeachingLearningMethod> ObjLstTLM = new List<MstTeachingLearningMethod>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstTeachingLearningMethod objTLM = new MstTeachingLearningMethod();

                        objTLM.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objTLM.TeachingLearningMethodName = Convert.ToString(Dt.Rows[i]["TeachingLearningMethodName"]);
                        objTLM.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjLstTLM.Add(objTLM);
                    }
                }
                return Return.returnHttp("200", ObjLstTLM, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion*/

        /*#region MstAssessmentMethodGet
        [HttpPost]
        public HttpResponseMessage MstAssessmentMethodGet()
        {
            try
            {                
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter cmdda = new SqlDataAdapter("MstAssessmentMethodGet", Con);

                cmdda.SelectCommand.CommandType = CommandType.StoredProcedure;

                DataTable Dt = new DataTable();
                cmdda.Fill(Dt);


                List<MstAssessmentMethod> ObjMstAssessmentMethod = new List<MstAssessmentMethod>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstAssessmentMethod ObjMstAM = new MstAssessmentMethod();

                        ObjMstAM.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        ObjMstAM.AssessmentMethodName = Convert.ToString(Dt.Rows[i]["AssessmentMethodName"]);
                        //ObjMstAM.IsActiveSts = Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        ObjMstAM.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjMstAM.IsDeleted = Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);

                        ObjMstAssessmentMethod.Add(ObjMstAM);
                    }

                }

                return Return.returnHttp("200", ObjMstAssessmentMethod, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion*/

        /*[HttpPost]
        public HttpResponseMessage MstAssessmentMethodGetByTeachingLearningMethodId(MstAssessmentMethod mstam)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MstAssessmentMethodGetByTeachingLearningMethodId", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                Int32 TeachingLearningMethodId = Convert.ToInt32(mstam.TeachingLearningMethodId.ToString());
                cmd.Parameters.AddWithValue("@TeachingLearningMethodId", TeachingLearningMethodId);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = cmd;

                Da.Fill(Dt);

                List<MstAssessmentMethod> ObjLstMstAssessmentMethod = new List<MstAssessmentMethod>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstAssessmentMethod Objmstam = new MstAssessmentMethod();

                        Objmstam.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        //Objmstint.InstituteId = Convert.ToInt32(Dt.Rows[i]["InstituteId"]);
                        Objmstam.AssessmentMethodName = Convert.ToString(Dt.Rows[i]["AssessmentMethodName"]);
                        Objmstam.TeachingLearningMethodId = Convert.ToInt32(Dt.Rows[i]["TeachingLearningMethodId"]);
                        Objmstam.TeachingLearningMethodName = Convert.ToString(Dt.Rows[i]["TeachingLearningMethodName"]);
                        Objmstam.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjLstMstAssessmentMethod.Add(Objmstam);
                    }
                }
                return Return.returnHttp("200", ObjLstMstAssessmentMethod, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }*/
    }
}
