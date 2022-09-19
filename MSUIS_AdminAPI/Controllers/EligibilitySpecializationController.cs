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

namespace MSUISApi.Controllers
{
    public class EligibilitySpecializationController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();


        //EligibilitySpecialization table CRUD

        [HttpPost]
        public HttpResponseMessage getEligibilitySpecializationList()
        {
            //EligibilitySpecialization es1 = new EligibilitySpecialization();
            //dynamic jsonData = jsonobject;
            try
            {
                SqlCommand cmd = new SqlCommand("AdmEligibilitySpecializationGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                List<EligibilitySpecialization> esList = new List<EligibilitySpecialization>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        EligibilitySpecialization es1 = new EligibilitySpecialization();
                        es1.Id = Convert.ToInt32(dr["Id"].ToString());
                        es1.EligibilitySpecializationName = dr["EligibilitySpecializationName"].ToString();
                        esList.Add(es1);
                    }

                }
                return Return.returnHttp("200", esList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }

        [HttpPost]
        public HttpResponseMessage insertEligibilitySpecializationData(JObject jsonobject)
        {
            EligibilitySpecialization es1 = new EligibilitySpecialization();
            dynamic jsonData = jsonobject;
            try
            {
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                es1.EligibilitySpecializationName = jsonData.EligibilitySpecializationName;
                int UserId = Convert.ToInt32(responseToken);

                SqlCommand cmd = new SqlCommand("AdmEligibilitySpecializationAdd", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EligibilitySpecializationName", es1.EligibilitySpecializationName);
                cmd.Parameters.AddWithValue("@UserId", UserId);


                con.Open();
                cmd.ExecuteNonQuery();

                con.Close();

                return Return.returnHttp("200", "Specialization has been Added successfully.", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }

        [HttpPost]
        public HttpResponseMessage updateEligibilitySpecializationData(JObject jsonobject)

        {
            EligibilitySpecialization es1 = new EligibilitySpecialization();
            dynamic jsonData = jsonobject;
            try
            {
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                es1.Id = Convert.ToInt32(jsonData.Id);
                es1.EligibilitySpecializationName = jsonData.EligibilitySpecializationName;
                int UserId = Convert.ToInt32(responseToken);

                SqlCommand cmd = new SqlCommand("AdmEligibilitySpecializationEdit", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", es1.Id);
                cmd.Parameters.AddWithValue("@EligibilitySpecializationName", es1.EligibilitySpecializationName);
                cmd.Parameters.AddWithValue("@UserId", UserId);

                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Specialization has been updated successfully.";
                }

                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", "Some Internal Error Occured.", null);
            }
        }

        [HttpPost]
        public HttpResponseMessage deleteEligibilitySpecializationData(JObject jsonobject)
        {
            EligibilitySpecialization es1 = new EligibilitySpecialization();
            dynamic jsonData = jsonobject;
            try
            {

                es1.Id = Convert.ToInt32(jsonData.Id);
                SqlCommand cmd = new SqlCommand("AdmEligibilitySpecializationDelete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", es1.Id);

                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Specialization has been deleted successfully.";
                }

                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
    }
}

