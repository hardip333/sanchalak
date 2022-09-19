using MSUIS_TokenManager.App_Start;
using MSUISApi.BAL;
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
    public class ManualEntryOfFeeController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region ManualEntryOfFeeDetailsByProgInsPartTermId
        [HttpPost]
        public HttpResponseMessage ManualEntryOfFeeDetailsByProgInsPartTermId(ManualEntryOfFee ObjProg)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                string resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int32 Count = 0;
                Int64 ProgrammeInstancePartTermId = Convert.ToInt64(ObjProg.ProgrammeInstancePartTermId);
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter cmdda = new SqlDataAdapter("ManualEntryOfFeeByPIPTId", Con);

                cmdda.SelectCommand.CommandType = CommandType.StoredProcedure;
                cmdda.SelectCommand.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);

                DataTable Dt = new DataTable();
                cmdda.Fill(Dt);


                List<ManualEntryOfFee> ObjLstManualEntryOfFee= new List<ManualEntryOfFee>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ManualEntryOfFee ObjManualEntryOfFee = new ManualEntryOfFee();

                        ObjManualEntryOfFee.ApplicationFormNo = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        ObjManualEntryOfFee.ProgrammeInstancePartTermId = Convert.ToInt64(Dt.Rows[i]["ProgrammeInstancePartTermId"]);
                        ObjManualEntryOfFee.FeeCategoryPartTermMapId = Convert.ToInt64(Dt.Rows[i]["FeeCategoryPartTermMapId"]);
                        ObjManualEntryOfFee.FeeCategoryId = Convert.ToInt32(Dt.Rows[i]["FeeCategoryId"]);
                        ObjManualEntryOfFee.NameAsPerMarksheet = ((Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"])).IsEmpty() ? "Not Defined" : (Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"])));
                        ObjManualEntryOfFee.FeeCategoryName = ((Convert.ToString(Dt.Rows[i]["FeeCategoryName"])).IsEmpty() ? "Not Defined" : (Convert.ToString(Dt.Rows[i]["FeeCategoryName"])));
                        ObjManualEntryOfFee.CommitteeName = ((Convert.ToString(Dt.Rows[i]["CommitteeName"])).IsEmpty() ? "Not Defined" : (Convert.ToString(Dt.Rows[i]["CommitteeName"])));
                        ObjManualEntryOfFee.TotalAmount = (Convert.ToDouble(Dt.Rows[i]["TotalAmount"]));
                        ObjManualEntryOfFee.TransactionStatus = ((Convert.ToString(Dt.Rows[i]["TransactionStatus"])).IsEmpty() ? false : (Convert.ToBoolean(Dt.Rows[i]["TransactionStatus"])));
                        ObjManualEntryOfFee.PaymentStatus = ((Convert.ToString(Dt.Rows[i]["PaymentStatus"])).IsEmpty() ? false : (Convert.ToBoolean(Dt.Rows[i]["PaymentStatus"])));
                        Count = Count + 1;
                        ObjManualEntryOfFee.IndexId = Count;
                        ObjLstManualEntryOfFee.Add(ObjManualEntryOfFee);
                    }
                    return Return.returnHttp("200", ObjLstManualEntryOfFee, null);
                }

                else
                {
                    return Return.returnHttp("201", "No Record Found", null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion


        #region PayAdmissionFeesICCR
        [HttpPost]
        public HttpResponseMessage UpdateAdmissionFeesICCR(ManualEntryOfFee UAF)
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
                if (String.IsNullOrEmpty(Convert.ToString(UAF.UniversityAccountNumber)))
                {
                    return Return.returnHttp("201", "Please Enter UniversityAccountNumber", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(UAF.TotalAmountPaid)))
                {
                    return Return.returnHttp("201", "Please Enter TotalAmountPaid", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(UAF.TransactionId)))
                {
                    return Return.returnHttp("201", "Please Enter TransactionId", null);
                }
              
               else if (UAF.TotalAmount != UAF.TotalAmountPaid)
                {
                    return Return.returnHttp("201", "Total Amount And Received Amount Should be same", null); 
                }
                    Int64 UserId = Convert.ToInt64(responseToken.ToString());
                Int64 ApplicationId = Convert.ToInt64(UAF.ApplicationFormNo);
                Int32 UniversityAccountNumber = Convert.ToInt32(UAF.UniversityAccountNumber);
                Int32 ChequeNo = Convert.ToInt32(UAF.ChequeNo);
                Int32 TransactionId = Convert.ToInt32(UAF.TransactionId);
                Int64 FeeCategoryPartTermMapId = Convert.ToInt64(UAF.FeeCategoryPartTermMapId);
                Int64 ProgrammeInstancePartTermId = Convert.ToInt64(UAF.ProgrammeInstancePartTermId);
                Int32 FeeCategoryId = Convert.ToInt32(UAF.FeeCategoryId);
                Double TotalAmountPaid = Convert.ToDouble(UAF.TotalAmountPaid);
                Boolean PaymentStatus = Convert.ToBoolean(UAF.PaymentStatus);
                Boolean TransactionStatus = Convert.ToBoolean(UAF.TransactionStatus);
                String OrderId = "NotNew";
                if (PaymentStatus == false)
                {
                    String FeeCategoryId1 = FeeCategoryId.ToString("D3");
                    String ProgrammeInstancePartTermId1 = ProgrammeInstancePartTermId.ToString("D5");
                    OrderId = string.Format("{0}{1}{2}{3}{4}", "1", FeeCategoryId1, ProgrammeInstancePartTermId1, ApplicationId, "1");
                }
                String msg = null;

                Con.Open();

                ST = Con.BeginTransaction();
                try
                {

                    SqlCommand CMD = new SqlCommand("AdmissionFeesPaidUpdateforICCR", Con, ST);
                    CMD.CommandType = CommandType.StoredProcedure;
                    CMD.Parameters.AddWithValue("@ApplicationId", ApplicationId);
                    CMD.Parameters.AddWithValue("@UniversityAccountNumber", UniversityAccountNumber);
                    CMD.Parameters.AddWithValue("@ChequeNo", ChequeNo);
                    CMD.Parameters.AddWithValue("@TransactionId", TransactionId);
                    CMD.Parameters.AddWithValue("@TotalAmountPaid", TotalAmountPaid);
                    CMD.Parameters.AddWithValue("@FeeCategoryPartTermMapId", FeeCategoryPartTermMapId);
                    CMD.Parameters.AddWithValue("@FeeCategoryId", FeeCategoryId);
                    CMD.Parameters.AddWithValue("@PaymentStatus", PaymentStatus);
                    CMD.Parameters.AddWithValue("@TransactionStatus", TransactionStatus);
                    CMD.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                    CMD.Parameters.AddWithValue("@OrderId", OrderId);
                    CMD.Parameters.AddWithValue("@UserId", UserId);
                    CMD.Parameters.AddWithValue("@UserTime", datetime);

                    CMD.Parameters.Add("@Message", SqlDbType.NVarChar, 500);

                    CMD.Parameters["@Message"].Direction = ParameterDirection.Output;

                    CMD.Transaction = ST;
                    int ans = CMD.ExecuteNonQuery();
                    msg = Convert.ToString(CMD.Parameters["@Message"].Value);


                    ST.Commit();


                    return Return.returnHttp("200", msg, null);
                }
                catch (SqlException sqlError)
                {
                    ST.Rollback();
                    String strMessageErr = Convert.ToString(sqlError);
                    return Return.returnHttp("201", "Please try again!", null);
                    //return Return.returnHttp("201", "Record already exists", null);
                }
            }
            catch (Exception e)
            {
                if (e.Message.ToString() == "The given header was not found.")
                {
                    return Return.returnHttp("0", "Session is expired, Kindly login again.", null);
                }
                else
                {
                    return Return.returnHttp("201", e.Message.ToString(), null);
                }
            }

        }
        #endregion

        #region ManualFeesReport
        [HttpPost]
        public HttpResponseMessage ManualFeesReport(ManualEntryOfFee ObjProg)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                string resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int32 Count = 0;
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter cmdda = new SqlDataAdapter("ManualFeesReport", Con);

                cmdda.SelectCommand.CommandType = CommandType.StoredProcedure;
                cmdda.SelectCommand.Parameters.AddWithValue("@AcademicYearId", ObjProg.AcademicYearId);

                DataTable Dt = new DataTable();
                cmdda.Fill(Dt);


                List<ManualEntryOfFee> ObjLstManualEntryOfFee = new List<ManualEntryOfFee>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ManualEntryOfFee ObjManualEntryOfFee = new ManualEntryOfFee();

                        ObjManualEntryOfFee.ApplicationFormNo = Convert.ToInt64(Dt.Rows[i]["ApplicationFormNo"]);
                        ObjManualEntryOfFee.PRN = Convert.ToInt64(Dt.Rows[i]["PRN"]);
                        ObjManualEntryOfFee.ProgrammeInstancePartTermId = Convert.ToInt64(Dt.Rows[i]["ProgrammeInstancePartTermId"]);
                        ObjManualEntryOfFee.CommitteeName = ((Convert.ToString(Dt.Rows[i]["CommitteeName"])).IsEmpty() ? "Not Defined" : (Convert.ToString(Dt.Rows[i]["CommitteeName"])));
                        ObjManualEntryOfFee.NameAsPerMarksheet = ((Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"])).IsEmpty() ? "Not Defined" : (Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"])));
                        ObjManualEntryOfFee.FeeCategoryName = ((Convert.ToString(Dt.Rows[i]["FeeCategoryName"])).IsEmpty() ? "Not Defined" : (Convert.ToString(Dt.Rows[i]["FeeCategoryName"])));
                        ObjManualEntryOfFee.FacultyName = ((Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : (Convert.ToString(Dt.Rows[i]["FacultyName"])));
                        ObjManualEntryOfFee.ProgrammeName = ((Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "Not Defined" : (Convert.ToString(Dt.Rows[i]["ProgrammeName"])));
                        ObjManualEntryOfFee.BranchName = ((Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "Not Defined" : (Convert.ToString(Dt.Rows[i]["BranchName"]))); Count = Count + 1;
                        ObjManualEntryOfFee.IndexId = Count;
                        ObjLstManualEntryOfFee.Add(ObjManualEntryOfFee);
                    }
                    return Return.returnHttp("200", ObjLstManualEntryOfFee, null);
                }

                else
                {
                    return Return.returnHttp("201", "No Record Found", null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region AcademicYearGet For DropDown
        [HttpPost]
        public HttpResponseMessage AcademicYearGetForDropDown()
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                List<AcademicYear> ObjListAcadYear = new List<AcademicYear>();
                BALAcademicYear ObjBalAcadYearList = new BALAcademicYear();
                ObjListAcadYear = ObjBalAcadYearList.AcademicYearGetForDropDown();

                if (ObjListAcadYear != null)
                    return Return.returnHttp("200", ObjListAcadYear, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion


    }
}
