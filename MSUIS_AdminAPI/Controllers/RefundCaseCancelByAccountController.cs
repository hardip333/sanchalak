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
using System.Net.Mail;
using System.Text;
using System.Web.Http;

namespace MSUISApi.Controllers
{
    public class RefundCaseCancelByAccountController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region MstFacultyGet
        [HttpPost]
        public HttpResponseMessage MstFacultyGet()
        {

            try
            {
                SqlCommand cmd = new SqlCommand("MstFacultyGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<RefundCaseCancelByAccount> ObjLstF = new List<RefundCaseCancelByAccount>();
                dt.Rows.Add("0", "All Faculty");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        RefundCaseCancelByAccount ObjF = new RefundCaseCancelByAccount();
                        ObjF.Id = Convert.ToInt32(dr["Id"]);
                        ObjF.FacultyName = (dr["FacultyName"].ToString());

                        ObjLstF.Add(ObjF);
                    }

                }

                return Return.returnHttp("200", ObjLstF, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region RefCaseCancelAccountApplicantsListBOB
        [HttpPost]
        public HttpResponseMessage RefCaseCancelAccountApplicantsListBOB(RefundCaseCancelByAccount ObjRefund)
        {
            string serverUrl = "https://admission.msubaroda.ac.in/MSUISApi/Upload/";
            RefundCaseCancelByAccount modelobj = new RefundCaseCancelByAccount();

            try
            {
                modelobj.FacultyId = ObjRefund.FacultyId;

                if (ObjRefund.FromDate != null)
                {
                    DateTime FromDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(ObjRefund.FromDate), out FromDate);
                    if (ChkDt)
                    {
                        modelobj.FromDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(ObjRefund.FromDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                        modelobj.FromDateFinal = string.Format("{0: yyyy-MM-dd}", modelobj.FromDate);
                    }
                    else
                    {
                        modelobj.FromDate = null;
                    }
                }
                else
                {
                    modelobj.FromDate = null;
                }

                if (ObjRefund.ToDate != null)
                {
                    DateTime ToDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(ObjRefund.ToDate), out ToDate);
                    if (ChkDt)
                    {
                        modelobj.ToDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(ObjRefund.ToDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                        modelobj.ToDateFinal = string.Format("{0: yyyy-MM-dd}", modelobj.ToDate);
                    }
                    else
                    {
                        modelobj.ToDate = null;
                    }
                }
                else
                {
                    modelobj.ToDate = null;
                }

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("RefCaseCancelAccountApplicantsListBOB", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", modelobj.FacultyId);
                cmd.Parameters.AddWithValue("@FromDate", modelobj.FromDateFinal);
                cmd.Parameters.AddWithValue("@ToDate", modelobj.ToDateFinal);

                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<RefundCaseCancelByAccount> ObjRefCase = new List<RefundCaseCancelByAccount>();
                RefundCaseCancelByAccount ObjRef1 = new RefundCaseCancelByAccount();
                ObjRefCase.Add(ObjRef1);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        RefundCaseCancelByAccount ObjRef = new RefundCaseCancelByAccount();
                        ObjRef.Id = Convert.ToInt64(dr["Id"]);
                        ObjRef.ApplicationId = Convert.ToInt64(dr["ApplicationId"]);
                        ObjRef.ProgrammeInstancePartTermId = Convert.ToInt64(dr["ProgrammeInstancePartTermId"]);
                        ObjRef.InstancePartTermName = Convert.ToString(dr["InstancePartTermName"]);
                        ObjRef.FacultyName = Convert.ToString(dr["FacultyName"]);
                        ObjRef.FullName = Convert.ToString(dr["LastName"]) + " " + Convert.ToString(dr["FirstName"]) + " " + Convert.ToString(dr["MiddleName"]);

                        var IsApprovedByAccount = dr["IsApprovedByAccount"];
                        if (IsApprovedByAccount is DBNull)
                        {
                            ObjRef.IsApprovedByAccount = null;
                        }
                        else
                        {
                            ObjRef.IsApprovedByAccount = Convert.ToBoolean(dr["IsApprovedByAccount"]);
                        }

                        var RefundAmountByAcademic = dr["RefundAmountByAcademic"];
                        if (RefundAmountByAcademic is DBNull)
                        {
                            ObjRef.RefundAmountByAcademic = null;
                        }
                        else
                        {
                            ObjRef .RefundAmountByAcademic = Convert.ToDecimal(dr["RefundAmountByAcademic"]);
                        }

                        var RefundAmountByAudit = dr["RefundAmountByAudit"];
                        if (RefundAmountByAudit is DBNull)
                        {
                            ObjRef.RefundAmountByAudit = null;
                        }
                        else
                        {
                            ObjRef.RefundAmountByAudit = Convert.ToDecimal(dr["RefundAmountByAudit"]);
                        }

                        var IsBOBAccount = dr["IsBOBAccount"];
                        if (IsBOBAccount is DBNull)
                        {
                            ObjRef.IsBOBAccount = null;
                        }
                        else
                        {
                            ObjRef.IsBOBAccount = Convert.ToBoolean(dr["IsBOBAccount"]);
                        }
                        ObjRef.AccountNumber = Convert.ToString(dr["AccountNumber"]);
                        ObjRef.AccountName = Convert.ToString(dr["AccountName"]);
                        ObjRef.BankName = Convert.ToString(dr["BankName"]);
                        ObjRef.IFSCCode = Convert.ToString(dr["IFSCCode"]);
                        ObjRef.PassbookDoc = Convert.ToString(dr["PassbookDoc"]);
                        if (dr["PassbookDoc"].ToString() != "")
                        {
                            string PassbookDoc = serverUrl + "StudentPassBookImageDocument/" + dr["PassbookDoc"].ToString();
                            ObjRef.PassbookDoc = PassbookDoc;
                        }

                        ObjRef.RTGSForm = Convert.ToString(dr["RTGSForm"]);
                        if (dr["RTGSForm"].ToString() != "")
                        {
                            string RTGSForm = serverUrl + "StudentRTGSImageDocument/" + dr["RTGSForm"].ToString();
                            ObjRef.RTGSForm = RTGSForm;
                        }

                        ObjRef.TransDate = string.Format("{0: dd-MM-yyyy}", Convert.ToString(dr["TransDate"]));
                        ObjRef.Description = "ADMISSION FEE REFUND";
                        ObjRef.Currency = "INR";
                        ObjRef.DC = "C";
                        ObjRef.BgColor = "lightgrey";
                        ObjRef.ProcessedOnAccount = string.Format("{0: dd-MM-yyyy}", Convert.ToString(dr["ProcessedOnAccount"]));
                        ObjRef.RequestStatus = Convert.ToString(dr["RequestStatus"]);
                        //ObjRef.RequestStatusFlag = Convert.ToBoolean(dr["RequestStatusFlag"]);
                        count = count + 1;
                        ObjRef.IndexId = count;

                        ObjRefCase.Add(ObjRef);
                    }

                    ObjRefCase[0].TransDate = string.Format("{0: dd-MM-yyyy}", Convert.ToString(ObjRefCase[1].TransDate));
                    ObjRefCase[0].AccountNumber = "02010200000020";
                    ObjRefCase[0].Description = "TRANSFER";
                    ObjRefCase[0].Currency = "INR";
                    ObjRefCase[0].DC = "D";
                    ObjRefCase[0].BgColor = "skyblue";
                    ObjRefCase[0].InstancePartTermName = "--";
                    ObjRefCase[0].FacultyName = "--";
                    ObjRefCase[0].RequestStatus = "--";
                    ObjRefCase[0].RefundAmountByAcademic = 0;
                    ObjRefCase[0].RefundAmountByAudit = 0;
                    for (int j = 1; j < ObjRefCase.Count(); j++)
                    {
                        ObjRefCase[0].RefundAmountByAudit = ObjRefCase[0].RefundAmountByAudit + ObjRefCase[j].RefundAmountByAudit;
                    }
                    ObjRefCase[0].FullName = "Registrar M S University of Baroda";
                    return Return.returnHttp("200", ObjRefCase, null);
                }
                else
                {
                    return Return.returnHttp("201", "No Records Found.", null);
                }

               

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region RefCaseCancelAccountApplicantsListOther
        [HttpPost]
        public HttpResponseMessage RefCaseCancelAccountApplicantsListOther(RefundCaseCancelByAccount ObjRefund)
        {
            string serverUrl = "https://admission.msubaroda.ac.in/MSUISApi/Upload/";
            RefundCaseCancelByAccount modelobj = new RefundCaseCancelByAccount();

            try
            {
                modelobj.FacultyId = ObjRefund.FacultyId;

                if (ObjRefund.FromDate != null)
                {
                    DateTime FromDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(ObjRefund.FromDate), out FromDate);
                    if (ChkDt)
                    {
                        modelobj.FromDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(ObjRefund.FromDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                        modelobj.FromDateFinal = string.Format("{0: yyyy-MM-dd}", modelobj.FromDate);
                    }
                    else
                    {
                        modelobj.FromDate = null;
                    }
                }
                else
                {
                    modelobj.FromDate = null;
                }

                if (ObjRefund.ToDate != null)
                {
                    DateTime ToDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(ObjRefund.ToDate), out ToDate);
                    if (ChkDt)
                    {
                        modelobj.ToDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(ObjRefund.ToDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                        modelobj.ToDateFinal = string.Format("{0: yyyy-MM-dd}", modelobj.ToDate);
                    }
                    else
                    {
                        modelobj.ToDate = null;
                    }
                }
                else
                {
                    modelobj.ToDate = null;
                }

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("RefCaseCancelAccountApplicantsListOther", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", modelobj.FacultyId);
                cmd.Parameters.AddWithValue("@FromDate", modelobj.FromDateFinal);
                cmd.Parameters.AddWithValue("@ToDate", modelobj.ToDateFinal);

                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<RefundCaseCancelByAccount> ObjRefCase = new List<RefundCaseCancelByAccount>();
                RefundCaseCancelByAccount ObjRef1 = new RefundCaseCancelByAccount();
                ObjRefCase.Add(ObjRef1);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        RefundCaseCancelByAccount ObjRef = new RefundCaseCancelByAccount();
                        ObjRef.Id = Convert.ToInt64(dr["Id"]);
                        ObjRef.ApplicationId = Convert.ToInt64(dr["ApplicationId"]);
                        ObjRef.ProgrammeInstancePartTermId = Convert.ToInt64(dr["ProgrammeInstancePartTermId"]);
                        ObjRef.InstancePartTermName = Convert.ToString(dr["InstancePartTermName"]);
                        ObjRef.FacultyName = Convert.ToString(dr["FacultyName"]);
                        ObjRef.FullName = Convert.ToString(dr["LastName"]) + " " + Convert.ToString(dr["FirstName"]) + " " + Convert.ToString(dr["MiddleName"]);

                        var IsApprovedByAccount = dr["IsApprovedByAccount"];
                        if (IsApprovedByAccount is DBNull)
                        {
                            ObjRef.IsApprovedByAccount = null;
                        }
                        else
                        {
                            ObjRef.IsApprovedByAccount = Convert.ToBoolean(dr["IsApprovedByAccount"]);
                        }

                        var RefundAmountByAcademic = dr["RefundAmountByAcademic"];
                        if (RefundAmountByAcademic is DBNull)
                        {
                            ObjRef.RefundAmountByAcademic = null;
                        }
                        else
                        {
                            ObjRef.RefundAmountByAcademic = Convert.ToDecimal(dr["RefundAmountByAcademic"]);
                        }

                        var RefundAmountByAudit = dr["RefundAmountByAudit"];
                        if (RefundAmountByAudit is DBNull)
                        {
                            ObjRef.RefundAmountByAudit = null;
                        }
                        else
                        {
                            ObjRef.RefundAmountByAudit = Convert.ToDecimal(dr["RefundAmountByAudit"]);
                        }

                        var IsBOBAccount = dr["IsBOBAccount"];
                        if (IsBOBAccount is DBNull)
                        {
                            ObjRef.IsBOBAccount = null;
                        }
                        else
                        {
                            ObjRef.IsBOBAccount = Convert.ToBoolean(dr["IsBOBAccount"]);
                        }
                        ObjRef.AccountNumber = Convert.ToString(dr["AccountNumber"]);
                        ObjRef.AccountName = Convert.ToString(dr["AccountName"]);
                        ObjRef.BankName = Convert.ToString(dr["BankName"]);
                        ObjRef.IFSCCode = Convert.ToString(dr["IFSCCode"]);
                        ObjRef.PassbookDoc = Convert.ToString(dr["PassbookDoc"]);
                        if (dr["PassbookDoc"].ToString() != "")
                        {
                            string PassbookDoc = serverUrl + "StudentPassBookImageDocument/" + dr["PassbookDoc"].ToString();
                            ObjRef.PassbookDoc = PassbookDoc;
                        }

                        ObjRef.RTGSForm = Convert.ToString(dr["RTGSForm"]);
                        if (dr["RTGSForm"].ToString() != "")
                        {
                            string RTGSForm = serverUrl + "StudentRTGSImageDocument/" + dr["RTGSForm"].ToString();
                            ObjRef.RTGSForm = RTGSForm;
                        }

                        ObjRef.DebitAccountNumber = "02010200000020";
                        ObjRef.BgColor = "lightgrey";
                        ObjRef.ProcessedOnAccount = string.Format("{0: dd-MM-yyyy}", Convert.ToString(dr["ProcessedOnAccount"]));
                    
                        ObjRef.RequestStatus = Convert.ToString(dr["RequestStatus"]);
                        //ObjRef.RequestStatusFlag = Convert.ToBoolean(dr["RequestStatusFlag"]);
                        count = count + 1;
                        ObjRef.IndexId = count;

                        ObjRefCase.Add(ObjRef);
                    }

                    //ObjRefCase[0].TransDate = string.Format("{0: dd-MM-yyyy}", Convert.ToString(ObjRefCase[1].TransDate));
                    ObjRefCase[0].DebitAccountNumber = "02010200000020";
                    ObjRefCase[0].InstancePartTermName = "--";
                    ObjRefCase[0].FacultyName = "--";
                    ObjRefCase[0].RequestStatus = "--";
                    ObjRefCase[0].RefundAmountByAcademic = 0;
                    ObjRefCase[0].RefundAmountByAudit = 0;
                    for (int j = 1; j < ObjRefCase.Count(); j++)
                    {
                        ObjRefCase[0].RefundAmountByAudit = ObjRefCase[0].RefundAmountByAudit + ObjRefCase[j].RefundAmountByAudit;
                    }
                    ObjRefCase[0].IFSCCode = "ICIC0002401";
                    ObjRefCase[0].AccountNumber = "240101506026";
                    ObjRefCase[0].FullName = "JISSY JOSEPH";
                    ObjRefCase[0].BankName = "ICICI BANK, AKSHAR CHAWK";
                    ObjRefCase[0].BgColor = "skyblue";
                    return Return.returnHttp("200", ObjRefCase, null);
                }
                else
                {
                    return Return.returnHttp("201", "No Records Found.", null);
                }
                

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region ImportToExcel

        [HttpPost]
        public HttpResponseMessage UpdateRefundDatafromExcel(RefundCancelModelFinal[] RefundArray)
        {
            string token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation tokenOperation = new TokenOperation();
            string responseToken = tokenOperation.ValidateToken(token);
            if (responseToken == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            //RefundModelFinal cm1 = new RefundModelFinal();
            //dynamic jsonData = jsonobject;
            var successcount = 0;
            var failurecount = 0;
            var RefundData = RefundArray;
            con.Open();
            for (var i = 0; i < RefundData.Length; i++)
            {

                try
                {
                    RefundCancelModelFinal cm1 = new RefundCancelModelFinal();
                    dynamic jsonData = RefundData[i];

                    cm1.ApplicationId = jsonData.ApplicationId;
                    cm1.AccountRemark = jsonData.AccountRemark;
                    cm1.RefundedTansactionId = jsonData.RefundedTansactionId;
                    cm1.ProcessedByAccount = Convert.ToInt64(responseToken);

                    SqlCommand cmd = new SqlCommand("RefCaseCancelUpdateAccountbyImport", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ApplicationId", cm1.ApplicationId);
                    cmd.Parameters.AddWithValue("@AccountRemark", cm1.AccountRemark);
                    cmd.Parameters.AddWithValue("@RefundedTansactionId", cm1.RefundedTansactionId);
                    cmd.Parameters.AddWithValue("@ProcessedByAccount", cm1.ProcessedByAccount);

                    cmd.ExecuteNonQuery();


                    successcount++;

                }
                catch (Exception e)
                {
                    failurecount++;
                }

            }



            con.Close();

            //var response = Request.CreateResponse(HttpStatusCode.OK);
            //String resp = "{\"status\":\"SUCCESS\",\"data\":[\"SuccessCount\"}";
            //response.Content = new StringContent(resp, System.Text.Encoding.UTF8, "application/json");
            //return response;

            return Return.returnHttp("200", "Data Uploaded into Database", null);
        }

        #endregion

    }
}
