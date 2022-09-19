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
    public class AdmEligibleDegreeController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();


        //AdmEligibleDegree table CRUD

        [HttpPost]
        public HttpResponseMessage getAdmEligibleDegreeList()
        {
            //EligibilitySpecialization es1 = new EligibilitySpecialization();
            //dynamic jsonData = jsonobject;
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("AdmEligibleDegreeGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                List<AdmEligibleDegree> aedList = new List<AdmEligibleDegree>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AdmEligibleDegree aed1 = new AdmEligibleDegree();
                        aed1.Id = Convert.ToInt32(dr["Id"].ToString());
                        aed1.EligibleDegreeName = dr["EligibleDegreeName"].ToString();
                        aedList.Add(aed1);
                    }

                }
                return Return.returnHttp("200", aedList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }

        [HttpPost]
        public HttpResponseMessage insertAdmEligibleDegreeData(JObject jsonobject)
        {
            AdmEligibleDegree aed1 = new AdmEligibleDegree();
            dynamic jsonData = jsonobject;

            if (string.IsNullOrWhiteSpace(Convert.ToString(jsonData.EligibleDegreeName)))
            {
                return Return.returnHttp("201", "Degree Name Required.", null);
            }
            else
            {
                try
                {
                    string token = Request.Headers.GetValues("token").FirstOrDefault();
                    TokenOperation tokenOperation = new TokenOperation();
                    string responseToken = tokenOperation.ValidateToken(token);
                    if (responseToken == "0")
                    {
                        return Return.returnHttp("0", null, null);
                    }

                    aed1.EligibleDegreeName = jsonData.EligibleDegreeName;
                    int UserId = Convert.ToInt32(responseToken);

                    SqlCommand cmd = new SqlCommand("AdmEligibleDegreeAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EligibleDegreeName", aed1.EligibleDegreeName.Trim());
                    cmd.Parameters.AddWithValue("@UserId", UserId);


                    con.Open();
                    cmd.ExecuteNonQuery();

                    con.Close();

                    return Return.returnHttp("200", "Degree has been Added successfully.", null);
                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message.ToString(), null);
                }
            }
        }

        [HttpPost]
        public HttpResponseMessage updateAdmEligibleDegreeData(JObject jsonobject)

        {
            AdmEligibleDegree aed1 = new AdmEligibleDegree();
            dynamic jsonData = jsonobject;

            if (string.IsNullOrWhiteSpace(Convert.ToString(jsonData.EligibleDegreeName)))
            {
                return Return.returnHttp("201", "Degree Name Required.", null);
            }
            else
            {
                try
                {
                    string token = Request.Headers.GetValues("token").FirstOrDefault();
                    TokenOperation tokenOperation = new TokenOperation();
                    string responseToken = tokenOperation.ValidateToken(token);
                    if (responseToken == "0")
                    {
                        return Return.returnHttp("0", null, null);
                    }

                    aed1.Id = Convert.ToInt32(jsonData.Id);
                    aed1.EligibleDegreeName = jsonData.EligibleDegreeName;
                    int UserId = Convert.ToInt32(responseToken);

                    SqlCommand cmd = new SqlCommand("AdmEligibleDegreeEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", aed1.Id);
                    cmd.Parameters.AddWithValue("@EligibleDegreeName", aed1.EligibleDegreeName.Trim());
                    cmd.Parameters.AddWithValue("@UserId", UserId);

                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    con.Close();

                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Degree has been Updated successfully.";
                    }

                    return Return.returnHttp("200", strMessage.ToString(), null);
                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", "Some Internal Error Occured.", null);
                }
            }
        }

        [HttpPost]
        public HttpResponseMessage deleteAdmEligibleDegreeData(JObject jsonobject)
        {
            AdmEligibleDegree aed1 = new AdmEligibleDegree();
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

                aed1.Id = Convert.ToInt32(jsonData.Id);
                SqlCommand cmd = new SqlCommand("AdmEligibleDegreeDelete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", aed1.Id);

                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Degree has been Deleted successfully.";
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