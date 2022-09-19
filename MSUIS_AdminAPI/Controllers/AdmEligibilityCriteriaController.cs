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
    public class AdmEligibilityCriteriaController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

        [HttpPost]
        public HttpResponseMessage AdmEligibilityCriteriaGet(JObject jsonobject)
        {
            AdmEligibilityCriteria modelobj = new AdmEligibilityCriteria();
            dynamic jsonData = jsonobject;
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                SqlCommand cmd = new SqlCommand("AdmEligibilityCriteriaGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<AdmEligibilityCriteria> ObjLstAEC = new List<AdmEligibilityCriteria>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AdmEligibilityCriteria ObjAEC = new AdmEligibilityCriteria();
                        ObjAEC.Id = Convert.ToInt32(dr["Id"]);
                        ObjAEC.EligibilityCriteria = (dr["EligibilityCriteria"].ToString());
                        ObjLstAEC.Add(ObjAEC);
                    }

                }
                return Return.returnHttp("200", ObjLstAEC, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
    }
}