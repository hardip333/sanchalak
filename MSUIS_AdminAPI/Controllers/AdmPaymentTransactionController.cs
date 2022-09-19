using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
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
    public class AdmPaymentTransactionController : ApiController
    {
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");      
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

        #region AdmPaymentTransPartTermGetByInstituteId

        [HttpPost]
        public HttpResponseMessage AdmPayTransPartTermGetByInstituteId(AdmPaymentTransaction ObjPaymentTransaction)
        {
            AdmPaymentTransaction modelobj = new AdmPaymentTransaction();

            try
            {
                modelobj.InstituteId = ObjPaymentTransaction.InstituteId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("AdmPaymentTransPartTermGetByInstituteId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InstituteId", modelobj.InstituteId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
               
                List<AdmPaymentTransaction> ObjLstPT = new List<AdmPaymentTransaction>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AdmPaymentTransaction ObjPT = new AdmPaymentTransaction();

                        ObjPT.Id = Convert.ToInt64(dr["Id"]);
                        ObjPT.InstancePartTermName = (dr["InstancePartTermName"].ToString());                       
                        ObjLstPT.Add(ObjPT);
                    }
                    return Return.returnHttp("200", ObjLstPT, null);
                }
                return Return.returnHttp("202", "No Record Found", null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region AdmPaymentTransactionGetByPartTermId

        [HttpPost]
        public HttpResponseMessage AdmPaymentTransGetByPartTermId(AdmPaymentTransaction ObjPaymentTransaction)
        {
            AdmPaymentTransaction modelobj = new AdmPaymentTransaction();

            try
            {
                modelobj.ProgrammeInstancePartTermId = ObjPaymentTransaction.ProgrammeInstancePartTermId;
                modelobj.Status = ObjPaymentTransaction.Status;
                modelobj.FromDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(ObjPaymentTransaction.FromDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
               
                modelobj.ToDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(ObjPaymentTransaction.ToDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                modelobj.ToDateFinal = string.Format("{0: dd-MM-yyyy}", modelobj.ToDate) + " 23:59:00.000";
                modelobj.ToDate =Convert.ToDateTime(modelobj.ToDateFinal);
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("AdmPaymentTransactionGetByPartTermId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", modelobj.ProgrammeInstancePartTermId);
                cmd.Parameters.AddWithValue("@Status", modelobj.Status);
                cmd.Parameters.AddWithValue("@FromDate", modelobj.FromDate);
                cmd.Parameters.AddWithValue("@ToDate", modelobj.ToDate);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<AdmPaymentTransaction> ObjLstPT = new List<AdmPaymentTransaction>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AdmPaymentTransaction ObjPT = new AdmPaymentTransaction();

                        ObjPT.OrderId = Convert.ToInt64(dr["OrderId"]);                     
                        ObjPT.LastName = (dr["LastName"].ToString());
                        ObjPT.FirstName = (dr["FirstName"].ToString());
                        ObjPT.MiddleName = (dr["MiddleName"].ToString());
                        ObjPT.Name = ObjPT.LastName + " " + ObjPT.FirstName + " " + ObjPT.MiddleName;
                        ObjPT.EmailId = (dr["EmailId"].ToString());
                        ObjPT.MobileNo = (dr["MobileNo"].ToString());
                        ObjPT.TransactionStatus = (dr["TransactionStatus"].ToString());
                        ObjPT.TransactionDate = string.Format("{0: dd-MM-yyyy}", dr["TransactionDate"]);
                        ObjPT.InstancePartTermName = (dr["InstancePartTermName"].ToString());

                        count = count + 1;
                        ObjPT.IndexId = count;

                        ObjLstPT.Add(ObjPT);
                    }
                    return Return.returnHttp("200", ObjLstPT, null);
                }
                return Return.returnHttp("202", "No Record Found", null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region AdmPaymentTransactionGetByOrderId

        [HttpPost]
        public HttpResponseMessage AdmPaymentTransGetByOrderId(AdmPaymentTransaction ObjPaymentTransaction)
        {
            AdmPaymentTransaction modelobj = new AdmPaymentTransaction();

            try
            {
                modelobj.OrderId = ObjPaymentTransaction.OrderId;               
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("AdmPaymentTransactionGetByOrderId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderId", modelobj.OrderId);               
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<AdmPaymentTransaction> ObjLstPT = new List<AdmPaymentTransaction>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AdmPaymentTransaction ObjPT = new AdmPaymentTransaction();

                        ObjPT.OrderId = Convert.ToInt64(dr["OrderId"]);
                        ObjPT.LastName = (dr["LastName"].ToString());
                        ObjPT.FirstName = (dr["FirstName"].ToString());
                        ObjPT.MiddleName = (dr["MiddleName"].ToString());
                        ObjPT.Name = ObjPT.LastName + " " + ObjPT.FirstName + " " + ObjPT.MiddleName;
                        ObjPT.EmailId = (dr["EmailId"].ToString());
                        ObjPT.MobileNo = (dr["MobileNo"].ToString());
                        ObjPT.TransactionStatus = (dr["TransactionStatus"].ToString());
                        ObjPT.TransactionDate = string.Format("{0: dd-MM-yyyy}", dr["TransactionDate"]);
                        ObjPT.GatewayRequest = (dr["GatewayRequest"].ToString());
                        ObjPT.GatewayResponse = (dr["GatewayResponse"].ToString());
                        ObjPT.InstalmentNo = Convert.ToInt32(dr["InstalmentNo"]);
                        ObjPT.ProgrammeInstancePartTermId = Convert.ToInt64(dr["ProgrammeInstancePartTermId"]);
                        ObjPT.InstancePartTermName = (dr["InstancePartTermName"].ToString());
                        ObjPT.RemainingInstallment = Convert.ToInt32(dr["RemainingInstallment"]);
                        ObjPT.InstituteId = Convert.ToInt32(dr["InstituteId"]);
                        
                        count = count + 1;
                        ObjPT.IndexId = count;

                        ObjLstPT.Add(ObjPT);
                    }
                    return Return.returnHttp("200", ObjLstPT, null);
                }
                return Return.returnHttp("202", "No Record Found", null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region MstInstituteGet
        [HttpPost]
        public HttpResponseMessage MstInstituteGet()
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
                SqlCommand cmd = new SqlCommand("MstInstituteGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<AdmPaymentTransaction> ObjLstInst = new List<AdmPaymentTransaction>();             
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AdmPaymentTransaction ObjInst = new AdmPaymentTransaction();
                        ObjInst.Id = Convert.ToInt32(dr["Id"]);
                        ObjInst.InstituteName = (dr["InstituteName"].ToString());

                        ObjLstInst.Add(ObjInst);
                    }
                    return Return.returnHttp("200", ObjLstInst, null);
                }

                return Return.returnHttp("202", "No Record Found", null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion


    }
}
