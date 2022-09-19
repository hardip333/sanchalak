using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MSUISApi.Controllers
{
    public class AdmApplicationFeesPaidController : ApiController
    {

        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

        [HttpPost]
        public HttpResponseMessage AdmApplicationFeesPaidGet(AdmApplicationFeesPaid admafp)
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

                SqlCommand cmd = new SqlCommand("AdmApplicationFeesPaidGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", admafp.Id);
                //cmd.Parameters.AddWithValue("@Id", responseToken);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable Dt = new DataTable();
                sda.Fill(Dt);




                List<AdmApplicationFeesPaid> ObjLstAdmAppFeesPaid = new List<AdmApplicationFeesPaid>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        AdmApplicationFeesPaid ObjAdmApplicationfp = new AdmApplicationFeesPaid();


                        ObjAdmApplicationfp.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        ObjAdmApplicationfp.ApplicationRegistrationId = Convert.ToInt64(Dt.Rows[i]["ApplicationRegistrationId"]);
                        ObjAdmApplicationfp.ProgrammeName = Convert.ToString(Dt.Rows[i]["PartTermName"]);
                        ObjAdmApplicationfp.Amount = Convert.ToDecimal(Dt.Rows[i]["Amount"]);
                        ObjAdmApplicationfp.TransactionId = Convert.ToString(Dt.Rows[i]["TransactionId"]);
                        var TransactionDate = Convert.ToDateTime(Dt.Rows[i]["TransactionDate"]);
                        ObjAdmApplicationfp.TransactionDate = TransactionDate.ToShortDateString() + " " + TransactionDate.ToShortTimeString();
                        ObjAdmApplicationfp.ApplicationNo = Convert.ToInt64(Dt.Rows[i]["ApplicationNo"]);
                        ObjAdmApplicationfp.FirstName = Convert.ToString(Dt.Rows[i]["FirstName"]);
                        ObjAdmApplicationfp.MiddleName = Convert.ToString(Dt.Rows[i]["MiddleName"]);
                        ObjAdmApplicationfp.LastName = Convert.ToString(Dt.Rows[i]["LastName"]);
                        ObjAdmApplicationfp.AcademicYearId = Convert.ToInt32(Dt.Rows[i]["AcademicYearId"]);
                        ObjAdmApplicationfp.AcademicYearCode = Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);
                        ObjAdmApplicationfp.FacultyName =  Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        ObjAdmApplicationfp.BranchName = Convert.ToString(Dt.Rows[i]["BranchName"]);


                        ObjLstAdmAppFeesPaid.Add(ObjAdmApplicationfp);
                    }

                }

                return Return.returnHttp("200", ObjLstAdmAppFeesPaid, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        [HttpPost]
        public HttpResponseMessage AdmApplicationFeesPaidNewGet(AdmApplicationFeesPaid admafp)
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

                SqlCommand cmd = new SqlCommand("AdmApplicationFeesPaidNewGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", admafp.Id);
                //cmd.Parameters.AddWithValue("@Id", responseToken);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable Dt = new DataTable();
                sda.Fill(Dt);




                List<FeeConfiguration> ObjLstAdmAppFeesPaid = new List<FeeConfiguration>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        FeeConfiguration ObjAdmApplicationfp = new FeeConfiguration();


                        ObjAdmApplicationfp.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        ObjAdmApplicationfp.ProgrammeName = Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        ObjAdmApplicationfp.Amount = Convert.ToDouble(Dt.Rows[i]["Amount"]);
                        ObjAdmApplicationfp.FacultyName = Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        ObjAdmApplicationfp.PartName = Convert.ToString(Dt.Rows[i]["PartName"]);
                        ObjAdmApplicationfp.PartTermName = Convert.ToString(Dt.Rows[i]["PartTermName"]);
                        ObjAdmApplicationfp.FeeCategoryName = Convert.ToString(Dt.Rows[i]["FeeCategoryName"]);
                        ObjAdmApplicationfp.FeeTypeName = Convert.ToString(Dt.Rows[i]["FeeTypeName"]);


                        ObjLstAdmAppFeesPaid.Add(ObjAdmApplicationfp);
                    }

                }

                return Return.returnHttp("200", ObjLstAdmAppFeesPaid, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }

    }
}