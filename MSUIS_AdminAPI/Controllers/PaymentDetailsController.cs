//using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
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
    public class PaymentDetailsController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();

        #region PaymentDetailsGet
        [HttpPost]
        public HttpResponseMessage AdmApplicationGet(AdmApplication aap)
        {
            //String token = Request.Headers.GetValues("token").FirstOrDefault();
            //TokenOperation ac = new TokenOperation();

            //string res = ac.ValidateToken(token);

            //if (res == "0")
            //{
            //    return Return.returnHttp("0", null, null);
            //}
            try
            {
                

                SqlCommand cmd = new SqlCommand("GetPaymentDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", aap.Id);
                //cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", aap.ProgrammeInstancePartTermId);
                //cmd.Parameters.AddWithValue("@Id", responseToken);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable Dt = new DataTable();
                sda.Fill(Dt);

                List<AdmApplication> ObjListPD = new List<AdmApplication>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        AdmApplication app = new AdmApplication();

                        app.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);

                        app.ApplicationRegistrationId = Convert.ToInt64(Dt.Rows[i]["ApplicationRegistrationId"]);

                        app.FirstName = Convert.ToString(Dt.Rows[i]["FirstName"]);

                        app.MiddleName = Convert.ToString(Dt.Rows[i]["MiddleName"]);

                        app.LastName = Convert.ToString(Dt.Rows[i]["LastName"]);

                        app.ProgrammeId = Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);

                        app.ProgrammeName = Convert.ToString(Dt.Rows[i]["ProgrammeName"]);

                        app.ApplicationFeesPaid = Convert.ToString(Dt.Rows[i]["ApplicationFeesPaid"]);

                        app.TransactionId = Convert.ToString(Dt.Rows[i]["TransactionId"]);

                        app.TransactionDate = Convert.ToString(Dt.Rows[i]["TransactionDate"]);

                        app.TransactionStatus = Convert.ToString(Dt.Rows[i]["TransactionStatus"]);

                        app.GatewayRequest = Convert.ToString(Dt.Rows[i]["GatewayRequest"]);

                        app.GatewayResponse = Convert.ToString(Dt.Rows[i]["GatewayResponse"]);

                        app.Error = Convert.ToString(Dt.Rows[i]["Error"]);

                        app.IpAddress = Convert.ToString(Dt.Rows[i]["IpAddress"]);

                        app.Browser = Convert.ToString(Dt.Rows[i]["Browser"]);

                        app.OS = Convert.ToString(Dt.Rows[i]["OS"]);

                        app.DeviceName = Convert.ToString(Dt.Rows[i]["DeviceName"]);

                        app.IsDeleted = Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);

                        ObjListPD.Add(app);
                    }
                }
                return Return.returnHttp("200", ObjListPD, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion


    }
}