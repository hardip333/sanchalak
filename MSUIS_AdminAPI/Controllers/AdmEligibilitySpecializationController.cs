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
    public class AdmEligibilitySpecializationController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

        [HttpPost]
        public HttpResponseMessage AdmEligibilitySpecializationGet(JObject jsonobject)
        {
            AdmEligibilitySpecialization modelobj = new AdmEligibilitySpecialization();
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
                SqlCommand cmd = new SqlCommand("AdmEligibilitySpecializationGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<AdmEligibilitySpecialization> ObjLstAES = new List<AdmEligibilitySpecialization>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AdmEligibilitySpecialization ObjAES = new AdmEligibilitySpecialization();
                        ObjAES.Id = Convert.ToInt32(dr["Id"]);
                        ObjAES.EligibilitySpecializationName = (dr["EligibilitySpecializationName"].ToString());
                        ObjLstAES.Add(ObjAES);
                    }

                }
                return Return.returnHttp("200", ObjLstAES, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
    }
}