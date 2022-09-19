using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using MSUISApi.BAL;
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
    public class AdmSpecializationMasterController : ApiController
    {

        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();

        #region AdmSpecializationMasterGet
        [HttpPost]
        public HttpResponseMessage AdmSpecializationMasterGet()
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
                SqlCommand cmd = new SqlCommand("AdmSpecializationGet", con);

                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<AdmSpecializationMaster> ObjListASM = new List<AdmSpecializationMaster>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AdmSpecializationMaster ObASM = new AdmSpecializationMaster();
                        ObASM.Id = Convert.ToInt64(dr["Id"].ToString());
                        ObASM.SpecializationCode = (Convert.ToString(dr["SpecializationCode"])).IsEmpty() ? "" : Convert.ToString(dr["SpecializationCode"]);
                        ObASM.SpecializationName = (Convert.ToString(dr["SpecializationName"])).IsEmpty() ? "" : Convert.ToString(dr["SpecializationName"]);
                        ObASM.EligibleDegreeId = Convert.ToInt32(dr["EligibleDegreeId"].ToString());
                        ObASM.EligibleDegreeName = (Convert.ToString(dr["EligibleDegreeName"])).IsEmpty() ? "" : Convert.ToString(dr["EligibleDegreeName"]);
                        ObASM.IsDeleted = (Convert.ToString(dr["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(dr["IsDeleted"]);
                        
                        ObjListASM.Add(ObASM);
                    }
                }
                return Return.returnHttp("200", ObjListASM, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion


        #region AdmSpecializationMasterAdd
        [HttpPost]
        public HttpResponseMessage AdmSpecializationMasterAdd(AdmSpecializationMaster ASM)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            if (String.IsNullOrWhiteSpace(Convert.ToString(ASM.SpecializationCode)))
            {
                return Return.returnHttp("201", "Please Enter Specialization Code", null);
            }
            else
            if (String.IsNullOrWhiteSpace(Convert.ToString(ASM.SpecializationName)))
            {
                return Return.returnHttp("201", "Please Enter Specialization Name", null);
            }
            else
            {
                try
                {
                    string SpecializationCode = Convert.ToString(ASM.SpecializationCode);
                    string SpecializationName = Convert.ToString(ASM.SpecializationName);
                    Int32 EligibleDegreeId = Convert.ToInt32(ASM.EligibleDegreeId);
                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("AdmSpecializationMasterAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SpecializationCode", SpecializationCode);
                    cmd.Parameters.AddWithValue("@SpecializationName", SpecializationName);
                    cmd.Parameters.AddWithValue("@EligibleDegreeId", EligibleDegreeId);
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
                    return Return.returnHttp("201", e.Message.ToString(), null);
                }
            }
        }
        #endregion

        #region AdmSpecializationMasterUpdate
        [HttpPost]
        public HttpResponseMessage AdmSpecializationMasterUpdate(AdmSpecializationMaster ASM)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            if (String.IsNullOrWhiteSpace(Convert.ToString(ASM.SpecializationCode)))
            {
                return Return.returnHttp("201", "Please Enter SpecializationCode", null);
            }
            else
            if (String.IsNullOrWhiteSpace(Convert.ToString(ASM.SpecializationName)))
            {
                return Return.returnHttp("201", "Please Enter SpecializationName", null);
            }
            else
            {
                try
                {
                    Int64 Id = ASM.Id;
                    string SpecializationCode = ASM.SpecializationCode;
                    string SpecializationName = ASM.SpecializationName;
                    Int32 EligibleDegreeId = Convert.ToInt32(ASM.EligibleDegreeId);
                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("AdmSpecializationMasterEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@SpecializationCode", SpecializationCode);
                    cmd.Parameters.AddWithValue("@SpecializationName", SpecializationName);
                    cmd.Parameters.AddWithValue("@EligibleDegreeId", EligibleDegreeId);
                    //cmd.Parameters.AddWithValue("@IsActive", IsActive);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    //cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);

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
                    return Return.returnHttp("201", e.Message.ToString(), null);
                }
            }
        }
        #endregion

        #region AdmSpecializationMasterDelete
        [HttpPost]
        public HttpResponseMessage AdmSpecializationMasterDelete(AdmSpecializationMaster ASM)
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
                Int64 Id = ASM.Id;
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("AdmSpecializationMasterDelete", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                //cmd.Parameters.AddWithValue("@Flag", "MstEvaluationDelete");
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


        #region AdmEligibleDegreeGet
        [HttpPost]
        public HttpResponseMessage AdmEligibleDegreeGet()
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
                SqlCommand cmd = new SqlCommand("AdmEligibleDegreeGet", con);

                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<AdmEligibleDegree> ObjListAED = new List<AdmEligibleDegree>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AdmEligibleDegree ObjAED = new AdmEligibleDegree();
                        ObjAED.Id = Convert.ToInt32(dr["Id"].ToString());
                        ObjAED.EligibleDegreeName = (Convert.ToString(dr["EligibleDegreeName"])).IsEmpty() ? "" : Convert.ToString(dr["EligibleDegreeName"]);
                       

                        ObjListAED.Add(ObjAED);
                    }
                }
                return Return.returnHttp("200", ObjListAED, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion
    }
}