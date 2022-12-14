using Newtonsoft.Json.Linq;
using CCA.Util;
using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MSUIS_TokenManager.App_Start;
using System.Web.WebPages;
using MSUISApi.BAL;
using System.IO;
using System.Web.Script.Serialization;
using System.Collections.Specialized;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace MSUISApi.Controllers
{
    public class ReconciliationController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();



        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region Get reconciliation data for application fees 
        [HttpPost]
        public HttpResponseMessage GetReconciliationDataForAppFees(FeeReconciliation ObjAppFees)
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

                SqlCommand cmd = new SqlCommand("ReconciliationDataForApplicationFees", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PRN", ObjAppFees.PRN);
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<FeeReconciliation> ObjLstAppFees = new List<FeeReconciliation>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        FeeReconciliation ObjFees = new FeeReconciliation();

                        ObjFees.TransactionId = Convert.ToInt64(dr["TransactionId"]);
                        ObjFees.NameAsPerMarksheet = Convert.ToString(dr["NameAsPerMarksheet"]);
                        ObjFees.MobileNo = Convert.ToString(dr["MobileNo"]);
                        ObjFees.EmailId = Convert.ToString(dr["EmailId"]);
                        ObjFees.OrderId = Convert.ToString(dr["OrderId"]);
                        ObjFees.BankReferanceNumber = Convert.ToString(dr["BankReferanceNumber"]);
                        ObjFees.TransactionDate = Convert.ToString(dr["TransactionDate"]);
                        ObjFees.TransactionStatus = Convert.ToString(dr["TransactionStatus"]);
                        ObjFees.ApplicationStatus = Convert.ToString(dr["ApplicationStatus"]);
                        ObjFees.InstancePartTermName = Convert.ToString(dr["InstancePartTermName"]);
                        ObjFees.FeesPaidInr = Convert.ToString(dr["FeesPaidInr"]);

                        ObjLstAppFees.Add(ObjFees);
                    }

                    return Return.returnHttp("200", ObjLstAppFees, null);

                }
                else
                {
                    return Return.returnHttp("201", "No Record Found.", null);
                }

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region Get reconciliation data for Exam fees 
        [HttpPost]
        public HttpResponseMessage GetReconciliationDataForExamFees(FeeReconciliation ObjExamFees)
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

                SqlCommand cmd = new SqlCommand("ReconciliationDataForExamFees", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PRN", ObjExamFees.PRN);
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<FeeReconciliation> ObjLstExamFees = new List<FeeReconciliation>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        FeeReconciliation ObjFees = new FeeReconciliation();

                        ObjFees.TransactionId = Convert.ToString(dr["TransactionId"]).IsEmpty() ? 0 : Convert.ToInt64(dr["TransactionId"]);
                        ObjFees.ProgInstancePartTermId = Convert.ToString(dr["ProgInstancePartTermId"]).IsEmpty() ? 0 : Convert.ToInt64(dr["ProgInstancePartTermId"]);
                        ObjFees.ExamMasterId = Convert.ToString(dr["ExamMasterId"]).IsEmpty() ? 0 : Convert.ToInt32(dr["ExamMasterId"]);
                        ObjFees.NameAsPerMarksheet = Convert.ToString(dr["NameAsPerMarksheet"]).IsEmpty() ? "" : Convert.ToString(dr["NameAsPerMarksheet"]);
                        ObjFees.MobileNo = Convert.ToString(dr["MobileNo"]).IsEmpty() ? "" : Convert.ToString(dr["MobileNo"]);
                        ObjFees.EmailId = Convert.ToString(dr["EmailId"]).IsEmpty() ? "" : Convert.ToString(dr["EmailId"]);
                        ObjFees.OrderId = Convert.ToString(dr["OrderId"]).IsEmpty() ? "" : Convert.ToString(dr["OrderId"]);
                        ObjFees.BankReferanceNumber = Convert.ToString(dr["BankReferanceNumber"]).IsEmpty() ? "" : Convert.ToString(dr["BankReferanceNumber"]);
                        ObjFees.TransactionDate = Convert.ToString(dr["TransactionDate"]).IsEmpty() ? "" : Convert.ToString(dr["TransactionDate"]);
                        ObjFees.TransactionStatus = Convert.ToString(dr["TransactionStatus"]).IsEmpty() ? "" : Convert.ToString(dr["TransactionStatus"]);
                        ObjFees.InstancePartTermName = Convert.ToString(dr["InstancePartTermName"]).IsEmpty() ? "" : Convert.ToString(dr["InstancePartTermName"]);
                        ObjFees.AppearanceType = Convert.ToString(dr["AppearanceType"]).IsEmpty() ? "" : Convert.ToString(dr["AppearanceType"]);
                        ObjFees.ExamFeeAmount = Convert.ToString(dr["ExamFeeAmount"]).IsEmpty() ? "" : Convert.ToString(dr["ExamFeeAmount"]);

                        ObjLstExamFees.Add(ObjFees);
                    }

                    return Return.returnHttp("200", ObjLstExamFees, null);

                }
                else
                {
                    return Return.returnHttp("201", "No Record Found.", null);
                }


            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region Get reconciliation data for admission fees - TempStudentAdmissionFeeDetail
        [HttpPost]
        public HttpResponseMessage GetTempStudentAdmissionReconciliationDataForAdmFees(FeeReconciliation ObjTempStudent)
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

                SqlCommand cmd = new SqlCommand("ReconciliationTempStudentAdmissionDataForAdmFees", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PRN", ObjTempStudent.PRN);
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<FeeReconciliation> ObjLstTempStudent = new List<FeeReconciliation>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        FeeReconciliation ObjFees = new FeeReconciliation();

                        //ObjFees.TransactionId = Convert.ToInt64(dr["TransactionId"]);
                        //ObjFees.NameAsPerMarksheet = Convert.ToString(dr["NameAsPerMarksheet"]);
                        //ObjFees.MobileNo = Convert.ToString(dr["MobileNo"]);
                        //ObjFees.EmailId = Convert.ToString(dr["EmailId"]);
                        ObjFees.OrderId = Convert.ToString(dr["OrderId"]);
                        //ObjFees.BankReferanceNumber = Convert.ToString(dr["BankReferanceNumber"]);
                        //ObjFees.TransactionDate = Convert.ToString(dr["TransactionDate"]);
                        ObjFees.TransactionStatus = Convert.ToString(dr["TransactionStatus"]);
                        ObjFees.InstancePartTermName = Convert.ToString(dr["InstancePartTermName"]);

                        ObjLstTempStudent.Add(ObjFees);
                    }

                    return Return.returnHttp("200", ObjLstTempStudent, null);

                }
                else
                {
                    return Return.returnHttp("201", "No Record Found.", null);
                }


            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region Get reconciliation data for admission fees - AdmPaymentTransaction
        [HttpPost]
        public HttpResponseMessage GetPaymentTransactionReconciliationDataForAdmFees(FeeReconciliation ObjPaymentTransaction)
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

                SqlCommand cmd = new SqlCommand("ReconciliationPaymentTransactionDataForAdmFees", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PRN", ObjPaymentTransaction.PRN);
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<FeeReconciliation> ObjLstPaymentTransaction = new List<FeeReconciliation>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        FeeReconciliation ObjFees = new FeeReconciliation();

                        ObjFees.TransactionId = Convert.ToInt64(dr["TransactionId"]);
                        ObjFees.NameAsPerMarksheet = Convert.ToString(dr["NameAsPerMarksheet"]);
                        ObjFees.MobileNo = Convert.ToString(dr["MobileNo"]);
                        ObjFees.EmailId = Convert.ToString(dr["EmailId"]);
                        ObjFees.OrderId = Convert.ToString(dr["OrderId"]);
                        ObjFees.BankReferanceNumber = Convert.ToString(dr["BankReferanceNumber"]);
                        ObjFees.TransactionDate = Convert.ToString(dr["TransactionDate"]);
                        ObjFees.TransactionStatus = Convert.ToString(dr["TransactionStatus"]);
                        ObjFees.InstancePartTermName = Convert.ToString(dr["InstancePartTermName"]);

                        ObjLstPaymentTransaction.Add(ObjFees);
                    }

                    return Return.returnHttp("200", ObjLstPaymentTransaction, null);

                }
                else
                {
                    return Return.returnHttp("201", "No Record Found.", null);
                }


            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region Get reconciliation data for admission fees - AdmStudentAdmissionFee
        [HttpPost]
        public HttpResponseMessage GetStudentAdmissionFeesReconciliationDataForAdmFees(FeeReconciliation ObjStudentAdmissionFees)
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

                SqlCommand cmd = new SqlCommand("ReconciliationStudentAdmissionFeesDataForExamFees", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PRN", ObjStudentAdmissionFees.PRN);
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<FeeReconciliation> ObjLstStudentAdmissionFees = new List<FeeReconciliation>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        FeeReconciliation ObjFees = new FeeReconciliation();

                        ObjFees.TransactionId = Convert.ToInt64(dr["TransactionId"]);
                        ObjFees.NameAsPerMarksheet = Convert.ToString(dr["NameAsPerMarksheet"]);
                        ObjFees.MobileNo = Convert.ToString(dr["MobileNo"]);
                        ObjFees.EmailId = Convert.ToString(dr["EmailId"]);
                        ObjFees.OrderId = Convert.ToString(dr["OrderId"]);
                        ObjFees.BankReferanceNumber = Convert.ToString(dr["BankReferanceNumber"]);
                        ObjFees.TransactionDate = Convert.ToString(dr["TransactionDate"]);
                        ObjFees.TransactionStatus = Convert.ToString(dr["TransactionStatus"]);
                        ObjFees.InstancePartTermName = Convert.ToString(dr["InstancePartTermName"]);

                        ObjLstStudentAdmissionFees.Add(ObjFees);
                    }

                    return Return.returnHttp("200", ObjLstStudentAdmissionFees, null);

                }
                else
                {
                    return Return.returnHttp("201", "No Record Found.", null);
                }


            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region Get reconciliation data for admission fees
        [HttpPost]
        public HttpResponseMessage GetReconciliationDataForAdmFees(FeeReconciliation ObjStudentAdmissionFees)
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

                SqlCommand cmd = new SqlCommand("ReconciliationTempStudentAdmissionDataForAdmFees", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PRN", ObjStudentAdmissionFees.PRN);
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<TempAdmFee> ObjLstTempStudent = new List<TempAdmFee>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        TempAdmFee ObjFees = new TempAdmFee();

                        ObjFees.OrderId = Convert.ToString(dr["OrderId"]);
                        ObjFees.TransactionStatus = Convert.ToString(dr["TransactionStatus"]);
                        ObjFees.InstancePartTermName = Convert.ToString(dr["InstancePartTermName"]);
                        ObjFees.AmountPaid = Convert.ToString(dr["AmountPaid"]);
                        ObjFees.FeeSubHeadName = Convert.ToString(dr["FeeSubHeadName"]);

                        ObjLstTempStudent.Add(ObjFees);
                    }

                }

                SqlDataAdapter sda1 = new SqlDataAdapter();
                DataTable dt1 = new DataTable();

                SqlCommand cmd1 = new SqlCommand("ReconciliationPaymentTransactionDataForAdmFees", con);
                cmd1.CommandType = CommandType.StoredProcedure;

                cmd1.Parameters.AddWithValue("@PRN", ObjStudentAdmissionFees.PRN);
                sda1.SelectCommand = cmd1;
                sda1.Fill(dt1);

                List<FeeReconciliation> ObjLstPaymentTransaction = new List<FeeReconciliation>();

                if (dt1.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt1.Rows)
                    {
                        FeeReconciliation ObjFees = new FeeReconciliation();

                        ObjFees.TransactionId = Convert.ToInt64(dr["TransactionId"]);
                        ObjFees.NameAsPerMarksheet = Convert.ToString(dr["NameAsPerMarksheet"]);
                        ObjFees.MobileNo = Convert.ToString(dr["MobileNo"]);
                        ObjFees.EmailId = Convert.ToString(dr["EmailId"]);
                        ObjFees.OrderId = Convert.ToString(dr["OrderId"]);
                        ObjFees.BankReferanceNumber = Convert.ToString(dr["BankReferanceNumber"]);
                        ObjFees.TransactionDate = Convert.ToString(dr["TransactionDate"]);
                        ObjFees.TransactionStatus = Convert.ToString(dr["TransactionStatus"]);
                        ObjFees.InstancePartTermName = Convert.ToString(dr["InstancePartTermName"]);
                        ObjFees.AmountPaid = Convert.ToString(dr["AmountPaid"]);

                        ObjLstPaymentTransaction.Add(ObjFees);
                    }

                }

                SqlDataAdapter sda2 = new SqlDataAdapter();
                DataTable dt2 = new DataTable();

                SqlCommand cmd2 = new SqlCommand("ReconciliationStudentAdmissionFeesDataForExamFees", con);
                cmd2.CommandType = CommandType.StoredProcedure;

                cmd2.Parameters.AddWithValue("@PRN", ObjStudentAdmissionFees.PRN);
                sda2.SelectCommand = cmd2;
                sda2.Fill(dt2);

                List<FeeReconciliation> ObjLstStudentAdmissionFees = new List<FeeReconciliation>();

                if (dt2.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt2.Rows)
                    {
                        FeeReconciliation ObjFees = new FeeReconciliation();

                        ObjFees.TransactionId = Convert.ToInt64(dr["TransactionId"]);
                        ObjFees.NameAsPerMarksheet = Convert.ToString(dr["NameAsPerMarksheet"]);
                        ObjFees.MobileNo = Convert.ToString(dr["MobileNo"]);
                        ObjFees.EmailId = Convert.ToString(dr["EmailId"]);
                        ObjFees.OrderId = Convert.ToString(dr["OrderId"]);
                        ObjFees.BankReferanceNumber = Convert.ToString(dr["BankReferanceNumber"]);
                        ObjFees.TransactionDate = Convert.ToString(dr["TransactionDate"]);
                        ObjFees.TransactionStatus = Convert.ToString(dr["TransactionStatus"]);
                        ObjFees.InstancePartTermName = Convert.ToString(dr["InstancePartTermName"]);
                        ObjFees.AmountPaid = Convert.ToString(dr["AmountPaid"]);

                        ObjLstStudentAdmissionFees.Add(ObjFees);
                    }

                }

                foreach (FeeReconciliation admfee in ObjLstStudentAdmissionFees)
                {
                    List<TempAdmFee> children = ObjLstTempStudent.Where(e => e.OrderId == admfee.OrderId).ToList();
                    if (children.Any())
                    {
                        admfee.FeeDetails = children;
                    }
                }

                if (ObjLstPaymentTransaction.Count > 0)
                {
                    return Return.returnHttp("200", (ObjLstTempStudent, ObjLstPaymentTransaction, ObjLstStudentAdmissionFees), null);
                }
                else
                {
                    return Return.returnHttp("201", "No Record Found", null);
                }

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region Get reconciliation data for Admission fees - New 
        [HttpPost]
        public HttpResponseMessage GetReconciliationDataForAdmissionFees(FeeReconciliation ObjAdmFees)
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

                SqlCommand cmd = new SqlCommand("ReconciliationDataForAdmissionFees", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PRN", ObjAdmFees.PRN);
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<FeeReconciliation> ObjLstAdmFees = new List<FeeReconciliation>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        FeeReconciliation ObjFees = new FeeReconciliation();

                        ObjFees.TransactionId = Convert.ToString(dr["TransactionId"]).IsEmpty() ? 0 : Convert.ToInt64(dr["TransactionId"]);
                        ObjFees.NameAsPerMarksheet = Convert.ToString(dr["NameAsPerMarksheet"]).IsEmpty() ? "" : Convert.ToString(dr["NameAsPerMarksheet"]);
                        ObjFees.MobileNo = Convert.ToString(dr["MobileNo"]).IsEmpty() ? "" : Convert.ToString(dr["MobileNo"]);
                        ObjFees.EmailId = Convert.ToString(dr["EmailId"]).IsEmpty() ? "" : Convert.ToString(dr["EmailId"]);
                        ObjFees.OrderId = Convert.ToString(dr["OrderId"]).IsEmpty() ? "" : Convert.ToString(dr["OrderId"]);
                        ObjFees.BankReferanceNumber = Convert.ToString(dr["BankReferanceNumber"]).IsEmpty() ? "" : Convert.ToString(dr["BankReferanceNumber"]);
                        ObjFees.TransactionDate = Convert.ToString(dr["PaymentDate"]).IsEmpty() ? "" : Convert.ToString(dr["PaymentDate"]);
                        ObjFees.TransactionStatus = Convert.ToString(dr["TransactionStatus"]).IsEmpty() ? "" : Convert.ToString(dr["TransactionStatus"]);
                        ObjFees.InstancePartTermName = Convert.ToString(dr["InstancePartTermName"]).IsEmpty() ? "" : Convert.ToString(dr["InstancePartTermName"]);
                        ObjFees.TotalAmountPaid = Convert.ToString(dr["TotalAmountPaid"]).IsEmpty() ? "" : Convert.ToString(dr["TotalAmountPaid"]);

                        ObjLstAdmFees.Add(ObjFees);
                    }

                    return Return.returnHttp("200", ObjLstAdmFees, null);

                }
                else
                {
                    return Return.returnHttp("201", "No Record Found.", null);
                }


            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region Reconciliation of application fees 
        [HttpPost]
        public HttpResponseMessage ReconciliationOfApplicationFees(FeeReconciliation ObjAppFees)
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

                SqlCommand cmd = new SqlCommand("ReconciliationOfApplicationFees", con);
                cmd.CommandType = CommandType.StoredProcedure;
                Int64 UserId = Convert.ToInt64(res.ToString());
                cmd.Parameters.AddWithValue("@AdmissionApplicationId", ObjAppFees.OrderId);
                cmd.Parameters.AddWithValue("@TransactionId", ObjAppFees.TransactionId);
                cmd.Parameters.AddWithValue("@BankReferanceNumber", ObjAppFees.BankReferanceNumber);
                cmd.Parameters.AddWithValue("@TransactionDate", ObjAppFees.TransactionDate);
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
                    strMessage = "Reconciliation For Application Fees has been done successfully.";
                }

                return Return.returnHttp("200", strMessage.ToString(), null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region Reconciliation of Exam fees 
        [HttpPost]
        public HttpResponseMessage ReconciliationOfExamFees(FeeReconciliation ObjAppFees)
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

                SqlCommand cmd = new SqlCommand("ReconciliationOfExamFees", con);
                cmd.CommandType = CommandType.StoredProcedure;
                Int64 UserId = Convert.ToInt64(res.ToString());
                cmd.Parameters.AddWithValue("@ExamMasterId", ObjAppFees.ExamMasterId);
                cmd.Parameters.AddWithValue("@ProgInstancePartTermId", ObjAppFees.ProgInstancePartTermId);
                cmd.Parameters.AddWithValue("@PRN", ObjAppFees.PRN);
                cmd.Parameters.AddWithValue("@OrderId", ObjAppFees.OrderId);
                cmd.Parameters.AddWithValue("@TransactionId", ObjAppFees.TransactionId);
                cmd.Parameters.AddWithValue("@BankReferanceNumber", ObjAppFees.BankReferanceNumber);
                cmd.Parameters.AddWithValue("@TransactionDate", ObjAppFees.TransactionDate);
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
                    strMessage = "Reconciliation For Examination Fees has been done successfully.";
                }
                else
                {
                    strMessage = "No Record Found.";
                }

                return Return.returnHttp("200", strMessage.ToString(), null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region Reconciliation of Admission fees 
        [HttpPost]
        public HttpResponseMessage ReconciliationOfAdmissionFees(FeeReconciliation ObjAppFees)
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

                SqlCommand cmd = new SqlCommand("ReconciliationOfAdmissionFees", con);
                cmd.CommandType = CommandType.StoredProcedure;
                Int64 UserId = Convert.ToInt64(res.ToString());
                cmd.Parameters.AddWithValue("@OrderId", ObjAppFees.OrderId);
                cmd.Parameters.AddWithValue("@TransactionId", ObjAppFees.TransactionId);
                cmd.Parameters.AddWithValue("@BankReferanceNumber", ObjAppFees.BankReferanceNumber);
                cmd.Parameters.AddWithValue("@TransactionDate", ObjAppFees.TransactionDate);
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
                    strMessage = "Reconciliation For Admission Fees has been done successfully.";
                }
                else
                {
                    strMessage = "Not Eligible.";
                }

                return Return.returnHttp("200", strMessage.ToString(), null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region Auto Reconciliation - Application Fees
        [HttpPost]
        public HttpResponseMessage AutoReconciliationApplicationFees(HDFCStatusAPI hDFCStatusAPI)
        {
            try
            {
                string Token = hDFCStatusAPI.Token;
                //Staging:
                //string accessCode = "AVHF09IF58AD54FHDA";//from avenues
                //string workingKey = "EF17E229B1169110432BB2E4B23F400D";// from avenues

                //LIVE Cred
                string accessCode = "AVWX16IH25CE54XWEC";//from avenues
                string workingKey = "2E4E1E01215DF43F4FD3CE6C92B917E9";// from avenues
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
                ServicePointManager.ServerCertificateValidationCallback = delegate (object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) { return true; };
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(Token);
                if (responseToken == "0")
                {
                    Return.returnHttp("0", null, null);
                }
                string OrderId1 = hDFCStatusAPI.order_id;
                string orderStatusQueryJson = "{ \"order_no\":" + OrderId1 + "}"; //Ex. { "reference_no":"CCAvenue_Reference_No" , "order_no":"123456"} 
                string encJson = "";

                string queryUrl = "https://login.ccavenue.com/apis/servlet/DoWebTrans";

                //string queryUrl = "https://apitest.ccavenue.com/apis/servlet/DoWebTrans";
                CCACrypto ccaCrypto = new CCACrypto();
                encJson = ccaCrypto.Encrypt(orderStatusQueryJson, workingKey);

                // make query for the status of the order to ccAvenues change the command param as per your need
                string authQueryUrlParam = "enc_request=" + encJson + "&access_code=" + accessCode + "&command=orderStatusTracker&request_type=JSON&response_type=JSON";

                // Url Connection
                String message = postPaymentRequestToGateway(queryUrl, authQueryUrlParam);
                NameValueCollection param = new NameValueCollection();
                param = getResponseMap(message);
                String status = "";
                String encResJson = "";
                if (param != null && param.Count == 2)
                {
                    for (int i = 0; i < param.Count; i++)
                    {
                        if ("status".Equals(param.Keys[i]))
                        {
                            status = param[i];
                        }
                        if ("enc_response".Equals(param.Keys[i]))
                        {
                            encResJson = param[i];
                        }
                    }
                }
                if (!"".Equals(status) && status.Equals("0"))
                {
                    String ResJson = ccaCrypto.Decrypt(encResJson, workingKey);

                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    dynamic paymentDecodeVariables = serializer.DeserializeObject(ResJson);

                    string order_status = string.Empty;

                    try
                    {
                        order_status = Convert.ToString(paymentDecodeVariables["Order_Status_Result"]["order_status"]);
                    }
                    catch
                    {
                        return Return.returnHttp("201", "No Record Found At CC-Avenue.", null);
                    }

                    if (order_status.Equals("Shipped"))
                    {
                        string OrderId = Convert.ToString(paymentDecodeVariables["Order_Status_Result"]["order_no"]);
                        Decimal Amount = paymentDecodeVariables["Order_Status_Result"]["order_amt"];
                        string reference_no = Convert.ToString(paymentDecodeVariables["Order_Status_Result"]["reference_no"]);
                        string order_bank_ref_no = Convert.ToString(paymentDecodeVariables["Order_Status_Result"]["order_bank_ref_no"]);
                        string order_date_time = Convert.ToString(paymentDecodeVariables["Order_Status_Result"]["order_date_time"]);
                        order_status = "Success";
                        string StrSpAdmPaymentTransactionEdit = "ReconciliationAutoUpdateApplicationFees";
                        cmd = new SqlCommand(StrSpAdmPaymentTransactionEdit);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AdmissionApplicationId", Convert.ToInt64(OrderId));
                        cmd.Parameters.AddWithValue("@TransactionDate", order_date_time);
                        cmd.Parameters.AddWithValue("@TransactionId", reference_no);
                        cmd.Parameters.AddWithValue("@BankReferanceNumber", order_bank_ref_no);
                        cmd.Parameters.AddWithValue("@TransactionStatus", order_status);
                        cmd.Parameters.AddWithValue("@GatewayResponse", ResJson);
                        cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                        cmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        string StrMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                        con.Close();
                        if (StrMessage != "TRUE")
                        {
                            return Return.returnHttp("201", StrMessage, null);
                        }
                        return Return.returnHttp("200", "Your payment status is Success kindly Check Receipt", null);
                    }
                    else
                    {
                        string OrderId = Convert.ToString(paymentDecodeVariables["Order_Status_Result"]["order_no"]);
                        Decimal Amount = paymentDecodeVariables["Order_Status_Result"]["order_amt"];
                        string reference_no = Convert.ToString(paymentDecodeVariables["Order_Status_Result"]["reference_no"]);
                        string order_bank_ref_no = string.Empty;
                        string order_date_time = string.Empty;

                        try
                        {
                            order_bank_ref_no = Convert.ToString(paymentDecodeVariables["Order_Status_Result"]["order_bank_ref_no"]);
                        }
                        catch
                        {
                            order_bank_ref_no = "";
                        }

                        try 
                        {
                            order_date_time = Convert.ToString(paymentDecodeVariables["Order_Status_Result"]["order_date_time"]);
                        }
                        catch
                        {
                            order_date_time = "";
                        }

                        string StrSpAdmPaymentTransactionEdit = "ReconciliationAutoUpdateApplicationFees";
                        cmd = new SqlCommand(StrSpAdmPaymentTransactionEdit);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AdmissionApplicationId", Convert.ToInt64(OrderId));
                        cmd.Parameters.AddWithValue("@TransactionId", reference_no);
                        cmd.Parameters.AddWithValue("@TransactionDate", order_date_time);
                        cmd.Parameters.AddWithValue("@BankReferanceNumber", order_bank_ref_no);
                        cmd.Parameters.AddWithValue("@TransactionStatus", order_status);
                        cmd.Parameters.AddWithValue("@GatewayResponse", "Status Api call by sanchalak" + ResJson);
                        cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                        cmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        string StrMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                        con.Close();

                        if (StrMessage != "TRUE")
                        {
                            //return Return.returnHttp("201", StrMessage, null);
                            return Return.returnHttp("201", "No Record Found.", null);
                        }
                        
                        return Return.returnHttp("200", "status updated", null);
                    }

                }
                else if (!"".Equals(status) && status.Equals("1"))
                {
                    return Return.returnHttp("201", encResJson, null);
                }
                return Return.returnHttp("201", "Something went wrong", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region postPaymentRequestToGateway
        private string postPaymentRequestToGateway(String queryUrl, String urlParam)
        {

            String message = "";
            try
            {
                StreamWriter myWriter = null;// it will open a http connection with provided url
                WebRequest objRequest = WebRequest.Create(queryUrl);//send data using objxmlhttp object
                objRequest.Method = "POST";
                //objRequest.ContentLength = TranRequest.Length;
                objRequest.ContentType = "application/x-www-form-urlencoded";//to set content type
                myWriter = new System.IO.StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(urlParam);//send data
                myWriter.Close();//closed the myWriter object

                // Getting Response
                System.Net.HttpWebResponse objResponse = (System.Net.HttpWebResponse)objRequest.GetResponse();//receive the responce from objxmlhttp object 
                using (System.IO.StreamReader sr = new System.IO.StreamReader(objResponse.GetResponseStream()))
                {
                    message = sr.ReadToEnd();
                    //Response.Write(message);
                }
            }
            catch (Exception exception)
            {
                Console.Write("Exception occured while connection." + exception);
            }
            return message;

        }
        #endregion

        #region getResponseMap
        private NameValueCollection getResponseMap(String message)
        {
            NameValueCollection Params = new NameValueCollection();
            if (message != null || !"".Equals(message))
            {
                string[] segments = message.Split('&');
                foreach (string seg in segments)
                {
                    string[] parts = seg.Split('=');
                    if (parts.Length > 0)
                    {
                        string Key = parts[0].Trim();
                        string Value = parts[1].Trim();
                        Params.Add(Key, Value);
                    }
                }
            }
            return Params;
        }
        #endregion

        #region Auto Reconciliation - Exam Fees
        [HttpPost]
        public HttpResponseMessage AutoReconciliationExamFees(HDFCStatusAPI hDFCStatusAPI)
        {
            try
            {
                string Token = hDFCStatusAPI.Token;
                //Staging:
                //string accessCode = "AVHF09IF58AD54FHDA";//from avenues
                //string workingKey = "EF17E229B1169110432BB2E4B23F400D";// from avenues

                //LIVE Cred
                string accessCode = "AVWX16IH25CE54XWEC";//from avenues
                string workingKey = "2E4E1E01215DF43F4FD3CE6C92B917E9";// from avenues
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
                ServicePointManager.ServerCertificateValidationCallback = delegate (object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) { return true; };
                
                string OrderId1 = hDFCStatusAPI.order_id;
                string orderStatusQueryJson = "{ \"order_no\":" + OrderId1 + "}"; //Ex. { "reference_no":"CCAvenue_Reference_No" , "order_no":"123456"} 
                string encJson = "";

                string queryUrl = "https://login.ccavenue.com/apis/servlet/DoWebTrans";

                //string queryUrl = "https://apitest.ccavenue.com/apis/servlet/DoWebTrans";
                CCACrypto ccaCrypto = new CCACrypto();
                encJson = ccaCrypto.Encrypt(orderStatusQueryJson, workingKey);

                // make query for the status of the order to ccAvenues change the command param as per your need
                string authQueryUrlParam = "enc_request=" + encJson + "&access_code=" + accessCode + "&command=orderStatusTracker&request_type=JSON&response_type=JSON";

                // Url Connection
                String message = postPaymentRequestToGateway(queryUrl, authQueryUrlParam);
                NameValueCollection param = new NameValueCollection();
                param = getResponseMap(message);
                String status = "";
                String encResJson = "";
                if (param != null && param.Count == 2)
                {
                    for (int i = 0; i < param.Count; i++)
                    {
                        if ("status".Equals(param.Keys[i]))
                        {
                            status = param[i];
                        }
                        if ("enc_response".Equals(param.Keys[i]))
                        {
                            encResJson = param[i];
                        }
                    }
                }
                if (!"".Equals(status) && status.Equals("0"))
                {
                    String ResJson = ccaCrypto.Decrypt(encResJson, workingKey);

                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    dynamic paymentDecodeVariables = serializer.DeserializeObject(ResJson);

                    string order_status = string.Empty;

                    try
                    {
                        order_status = Convert.ToString(paymentDecodeVariables["Order_Status_Result"]["order_status"]);
                    }
                    catch
                    {
                        return Return.returnHttp("201", "No Record Found At CC-Avenue.", null);
                    }

                    if (order_status.Equals("Shipped"))
                    {
                        string OrderId = Convert.ToString(paymentDecodeVariables["Order_Status_Result"]["order_no"]);
                        Decimal Amount = paymentDecodeVariables["Order_Status_Result"]["order_amt"];
                        Int64 reference_no = Convert.ToInt64(paymentDecodeVariables["Order_Status_Result"]["reference_no"]);
                        string order_bank_ref_no = Convert.ToString(paymentDecodeVariables["Order_Status_Result"]["order_bank_ref_no"]);
                        string order_date_time = Convert.ToString(paymentDecodeVariables["Order_Status_Result"]["order_date_time"]);
                        order_status = "Success";
                        string StrSpAdmPaymentTransactionEdit = "ReconciliationAutoUpdateExamFees";
                        cmd = new SqlCommand(StrSpAdmPaymentTransactionEdit);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@OrderId", OrderId);
                        cmd.Parameters.AddWithValue("@ExamFeeAmount", Amount);
                        cmd.Parameters.AddWithValue("@TransactionDate", order_date_time);
                        cmd.Parameters.AddWithValue("@TransactionId", reference_no);
                        cmd.Parameters.AddWithValue("@BankReferanceNumber", order_bank_ref_no);
                        cmd.Parameters.AddWithValue("@TransactionStatus", order_status);
                        cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                        cmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        string StrMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                        con.Close();
                        if (StrMessage != "Success")
                        {
                            return Return.returnHttp("201", StrMessage, null);
                        }
                        return Return.returnHttp("200", "Your payment status is Success kindly Check Receipt", null);

                    }
                   
                    else
                    {
                        string OrderId = Convert.ToString(paymentDecodeVariables["Order_Status_Result"]["order_no"]);                        
                        Decimal Amount = paymentDecodeVariables["Order_Status_Result"]["order_amt"];
                        Int64 reference_no = Convert.ToInt64(paymentDecodeVariables["Order_Status_Result"]["reference_no"]);
                        string order_bank_ref_no = string.Empty;
                        string order_date_time = string.Empty;
                        try
                        {
                            order_bank_ref_no = Convert.ToString(paymentDecodeVariables["Order_Status_Result"]["order_bank_ref_no"]);
                        }
                        catch
                        {
                            order_bank_ref_no = "";
                        }

                        try
                        {
                            order_date_time = Convert.ToString(paymentDecodeVariables["Order_Status_Result"]["order_date_time"]);
                        }
                        catch
                        {
                            order_date_time = "";
                        }
                        string StrSpAdmPaymentTransactionEdit = "ReconciliationAutoUpdateExamFees";
                        cmd = new SqlCommand(StrSpAdmPaymentTransactionEdit);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@OrderId", OrderId);
                        cmd.Parameters.AddWithValue("@ExamFeeAmount", Amount);
                        cmd.Parameters.AddWithValue("@TransactionId", reference_no);
                        cmd.Parameters.AddWithValue("@BankReferanceNumber", order_bank_ref_no);
                        cmd.Parameters.AddWithValue("@TransactionStatus", order_status);
                        cmd.Parameters.AddWithValue("@TransactionDate", order_date_time);
                        cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                        cmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        string StrMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                        con.Close();
                        if (StrMessage != "TRUE")
                        {
                            return Return.returnHttp("201", "No Record Found.", null);
                        }

                        return Return.returnHttp("200", "status updated", null);
                    }
                }
                else if (!"".Equals(status) && status.Equals("1"))
                {
                    return Return.returnHttp("201", encResJson, null);
                }
                return Return.returnHttp("201", "Something went wrong", null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region Auto Reconciliation Admission Fees
        [HttpPost]
        public HttpResponseMessage AutoReconciliationAdmissionFees(HDFCStatusAPI hDFCStatusAPI)
        {
            try
            {
                //Staging:
                //string accessCode = "AVHF09IF58AD54FHDA";//from avenues
                //string workingKey = "EF17E229B1169110432BB2E4B23F400D";// from avenues

                //LIVE Cred
                string accessCode = "AVWX16IH25CE54XWEC";//from avenues
                string workingKey = "2E4E1E01215DF43F4FD3CE6C92B917E9";// from avenues
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
                ServicePointManager.ServerCertificateValidationCallback = delegate (object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) { return true; };

                string orderStatusQueryJson = "{ \"order_no\":" + hDFCStatusAPI.order_id + "}"; //Ex. { "reference_no":"CCAvenue_Reference_No" , "order_no":"123456"} 
                string encJson = "";

                string queryUrl = "https://login.ccavenue.com/apis/servlet/DoWebTrans";

                //string queryUrl = "https://apitest.ccavenue.com/apis/servlet/DoWebTrans";
                CCACrypto ccaCrypto = new CCACrypto();
                encJson = ccaCrypto.Encrypt(orderStatusQueryJson, workingKey);

                // make query for the status of the order to ccAvenues change the command param as per your need
                string authQueryUrlParam = "enc_request=" + encJson + "&access_code=" + accessCode + "&command=orderStatusTracker&request_type=JSON&response_type=JSON";

                // Url Connection
                String message = postPaymentRequestToGateway(queryUrl, authQueryUrlParam);                
                NameValueCollection param = new NameValueCollection();
                param = getResponseMap(message);
                String status = "";
                String encResJson = "";
                if (param != null && param.Count == 2)
                {
                    for (int i = 0; i < param.Count; i++)
                    {
                        if ("status".Equals(param.Keys[i]))
                        {
                            status = param[i];
                        }
                        if ("enc_response".Equals(param.Keys[i]))
                        {
                            encResJson = param[i];                            
                        }
                    }
                }
                if (!"".Equals(status) && status.Equals("0"))
                {
                    String ResJson = ccaCrypto.Decrypt(encResJson, workingKey);

                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    dynamic paymentDecodeVariables = serializer.DeserializeObject(ResJson);

                    string order_status = string.Empty;

                    try
                    {
                        order_status = Convert.ToString(paymentDecodeVariables["Order_Status_Result"]["order_status"]);
                    }
                    catch
                    {
                        return Return.returnHttp("201", "No Record Found At CC-Avenue.", null);
                    }

                    if (order_status.Equals("Shipped"))
                    {
                        string OrderId = Convert.ToString(paymentDecodeVariables["Order_Status_Result"]["order_no"]);
                        Decimal Amount = paymentDecodeVariables["Order_Status_Result"]["order_amt"];
                        string reference_no = Convert.ToString(paymentDecodeVariables["Order_Status_Result"]["reference_no"]);
                        string order_bank_ref_no = Convert.ToString(paymentDecodeVariables["Order_Status_Result"]["order_bank_ref_no"]);
                        string order_date_time = Convert.ToString(paymentDecodeVariables["Order_Status_Result"]["order_date_time"]);
                        order_status = "Success";
                        string StrSpAdmPaymentTransactionEdit = "ReconciliationAutoUpdateAdmissionFees";
                        cmd = new SqlCommand(StrSpAdmPaymentTransactionEdit);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@OrderId", OrderId);
                        cmd.Parameters.AddWithValue("@AmountPaid", Amount);
                        cmd.Parameters.AddWithValue("@TransactionId", reference_no);
                        cmd.Parameters.AddWithValue("@BankReferanceNumber", order_bank_ref_no);
                        cmd.Parameters.AddWithValue("@TransactionStatus", order_status);
                        cmd.Parameters.AddWithValue("@GatewayResponse", ResJson);
                        cmd.Parameters.AddWithValue("@TransactionDate", order_date_time);
                        cmd.Parameters.AddWithValue("@UpdatedByStatusAPI", 1);
                        cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                        cmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        string StrMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                        con.Close();
                        if (StrMessage != "TRUE")
                        {
                            return Return.returnHttp("201", StrMessage, null);
                        }
                        return Return.returnHttp("200", "Your payment status is Success kindly Check Receipt", null);
                    }
                    else
                   
                    {
                        string OrderId = Convert.ToString(paymentDecodeVariables["Order_Status_Result"]["order_no"]);
                        Decimal Amount = paymentDecodeVariables["Order_Status_Result"]["order_amt"];
                        string reference_no = Convert.ToString(paymentDecodeVariables["Order_Status_Result"]["reference_no"]);
                        string order_bank_ref_no = string.Empty;
                        string order_date_time = string.Empty;
                        try
                        {
                            order_bank_ref_no = Convert.ToString(paymentDecodeVariables["Order_Status_Result"]["order_bank_ref_no"]);
                        }
                        catch
                        {
                            order_bank_ref_no = "";
                        }

                        try
                        {
                            order_date_time = Convert.ToString(paymentDecodeVariables["Order_Status_Result"]["order_date_time"]);
                        }
                        catch
                        {
                            order_date_time = "";
                        }
                        string StrSpAdmPaymentTransactionEdit = "ReconciliationAutoUpdateAdmissionFees";
                        cmd = new SqlCommand(StrSpAdmPaymentTransactionEdit);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@OrderId", OrderId);
                        cmd.Parameters.AddWithValue("@AmountPaid", Amount);
                        cmd.Parameters.AddWithValue("@TransactionId", reference_no);
                        cmd.Parameters.AddWithValue("@BankReferanceNumber", order_bank_ref_no);
                        cmd.Parameters.AddWithValue("@TransactionStatus", order_status);
                        cmd.Parameters.AddWithValue("@GatewayResponse", "Status Api call by sanchalak" + ResJson);
                        cmd.Parameters.AddWithValue("@TransactionDate", order_date_time);
                        cmd.Parameters.AddWithValue("@UpdatedByStatusAPI", 1);
                        cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                        cmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        string StrMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                        con.Close();
                        if (StrMessage != "TRUE")
                        {
                            return Return.returnHttp("201", "No Record Found.", null);
                        }

                        return Return.returnHttp("200", "status updated", null);
                    }
                }
                else if (!"".Equals(status) && status.Equals("1"))
                {
                    return Return.returnHttp("201", encResJson, null);
                }
                return Return.returnHttp("201", "Something went wrong", null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

    }
}

