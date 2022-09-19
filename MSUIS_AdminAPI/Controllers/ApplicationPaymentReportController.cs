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
    public class ApplicationPaymentReportController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();

        #region ApplicationPaymentReportGet
        [HttpPost]
        public HttpResponseMessage ApplicationPaymentReportGet(ApplicationPaymentReport ObjAPR)
        {
            //String token = Request.Headers.GetValues("token").FirstOrDefault();
            //TokenOperation ac = new TokenOperation();

            //string res = ac.ValidateToken(token);

            //if (res == "0")
            //{
            //    return Return.returnHttp("0", null, null);
            //}
            ApplicationPaymentReport modelAPR = new ApplicationPaymentReport();


            try
            {
                modelAPR.TransactionStatus = ObjAPR.TransactionStatus;
                modelAPR.FromDate = ObjAPR.FromDate;
                modelAPR.ToDate = ObjAPR.ToDate;


                ObjAPR.TransactionDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(ObjAPR.TransactionDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

                SqlCommand cmd = new SqlCommand("GetPaymentReportInformation", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TransactionStatus", ObjAPR.TransactionStatus);
                cmd.Parameters.AddWithValue("@FromDate", ObjAPR.FromDate);
                cmd.Parameters.AddWithValue("@ToDate", ObjAPR.ToDate);
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<ApplicationPaymentReport> ObjListAPR = new List<ApplicationPaymentReport>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ApplicationPaymentReport APR = new ApplicationPaymentReport();



                        APR.Id = Convert.ToInt64(dr["Id"].ToString());
                        APR.ApplicantRegistrationId = Convert.ToInt32(dr["ApplicantRegistrationId"].ToString());
                        APR.FirstName = (Convert.ToString(dr["FirstName"])).IsEmpty() ? "" : Convert.ToString(dr["FirstName"]);
                        APR.MiddleName = (Convert.ToString(dr["MiddleName"])).IsEmpty() ? "" : Convert.ToString(dr["MiddleName"]);
                        APR.LastName = (Convert.ToString(dr["LastName"])).IsEmpty() ? "" : Convert.ToString(dr["LastName"]);
                        APR.EmailId = (Convert.ToString(dr["EmailId"])).IsEmpty() ? "" : Convert.ToString(dr["EmailId"]);
                        APR.MobileNo = (Convert.ToString(dr["MobileNo"])).IsEmpty() ? "" : Convert.ToString(dr["MobileNo"]);
                        APR.ProgrammeId = Convert.ToInt32(dr["ProgrammeId"].ToString());
                        APR.ProgrammeName = (Convert.ToString(dr["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(dr["ProgrammeName"]);
                        APR.TransactionStatus = (Convert.ToString(dr["TransactionStatus"])).IsEmpty() ? "" : Convert.ToString(dr["TransactionStatus"]);
                        APR.TransactionDates = (Convert.ToString(dr["TransactionDate"]));
                        APR.IsDeleted = (Convert.ToString(dr["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(dr["IsDeleted"]);

                        var TransactionDate = dr["TransactionDate"];

                        if (TransactionDate is DBNull)
                        {
                            TransactionDate = "Null";
                        }
                        else
                        {
                            TransactionDate = dr["TransactionDate"];
                        }


                        count = count + 1;
                        APR.IndexId = count;

                        ObjListAPR.Add(APR);
                    }
                }
                return Return.returnHttp("200", ObjListAPR, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion


        #region ApplicationPaymentReportGetByInstitute
        [HttpPost]
        public HttpResponseMessage ApplicationPaymentReportGetByInstitute(ApplicationPaymentReport ObjPaymentReport)
        {
            //String token = Request.Headers.GetValues("token").FirstOrDefault();
            //TokenOperation ac = new TokenOperation();

            //string res = ac.ValidateToken(token);

            //if (res == "0")
            //{
            //    return Return.returnHttp("0", null, null);
            //}

            ApplicationPaymentReport modelobj = new ApplicationPaymentReport();

            try
            {
                modelobj.InstituteId = ObjPaymentReport.InstituteId;
                SqlCommand cmd = new SqlCommand("GetPaymentReportInformationByInstitute", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InstituteId", modelobj.InstituteId);
                //cmd.Parameters.AddWithValue("@Id", admapp.Id);
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<ApplicationPaymentReport> ObjListAPR = new List<ApplicationPaymentReport>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ApplicationPaymentReport APR = new ApplicationPaymentReport();
                        APR.Id = Convert.ToInt64(dr["Id"].ToString());
                        APR.ApplicantRegistrationId = Convert.ToInt32(dr["ApplicantRegistrationId"].ToString());
                        APR.FirstName = (Convert.ToString(dr["FirstName"])).IsEmpty() ? "" : Convert.ToString(dr["FirstName"]);
                        APR.MiddleName = (Convert.ToString(dr["MiddleName"])).IsEmpty() ? "" : Convert.ToString(dr["MiddleName"]);
                        APR.LastName = (Convert.ToString(dr["LastName"])).IsEmpty() ? "" : Convert.ToString(dr["LastName"]);
                        APR.EmailId = (Convert.ToString(dr["EmailId"])).IsEmpty() ? "" : Convert.ToString(dr["EmailId"]);
                        APR.MobileNo = (Convert.ToString(dr["MobileNo"])).IsEmpty() ? "" : Convert.ToString(dr["MobileNo"]);
                        APR.ProgrammeId = Convert.ToInt32(dr["ProgrammeId"].ToString());
                        APR.ProgrammeName = (Convert.ToString(dr["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(dr["ProgrammeName"]);
                        APR.TransactionStatus = (Convert.ToString(dr["TransactionStatus"])).IsEmpty() ? "" : Convert.ToString(dr["TransactionStatus"]);
                        APR.IsDeleted = (Convert.ToString(dr["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(dr["IsDeleted"]);
                        
                        ObjListAPR.Add(APR);
                    }
                }
                return Return.returnHttp("200", ObjListAPR, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        
    }
}