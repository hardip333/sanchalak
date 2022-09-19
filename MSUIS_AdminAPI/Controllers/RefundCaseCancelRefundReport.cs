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
    public class RefundCaseCancelRefundReportController : ApiController
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

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("RefCaseCancelRefundReportBOB", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", modelobj.FacultyId);

                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<RefundCaseCancelByAccount> ObjRefCase = new List<RefundCaseCancelByAccount>();

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

                        ObjRef.ProcessedOnAccount = string.Format("{0: dd-MM-yyyy}", Convert.ToString(dr["ProcessedOnAccount"]));
                        if (ObjRef.ProcessedOnAccount == null || ObjRef.ProcessedOnAccount == "")
                        {
                            ObjRef.ProcessedOnAccount = "--";
                        }

                        ObjRef.AccountRemark = Convert.ToString(dr["AccountRemark"]);
                        ObjRef.RefundedTansactionId = Convert.ToString(dr["RefundedTansactionId"]);

                        ObjRef.RequestStatus = Convert.ToString(dr["RequestStatus"]);
                        //ObjRef.RequestStatusFlag = Convert.ToBoolean(dr["RequestStatusFlag"]);
                        count = count + 1;
                        ObjRef.IndexId = count;

                        ObjRefCase.Add(ObjRef);
                    }
                   
                   
                }
                return Return.returnHttp("200", ObjRefCase, null);

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

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("RefCaseCancelRefundReportOther", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", modelobj.FacultyId);

                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<RefundCaseCancelByAccount> ObjRefCase = new List<RefundCaseCancelByAccount>();

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

                        ObjRef.ProcessedOnAccount = string.Format("{0: dd-MM-yyyy}", Convert.ToString(dr["ProcessedOnAccount"]));
                        if (ObjRef.ProcessedOnAccount == null || ObjRef.ProcessedOnAccount == "")
                        {
                            ObjRef.ProcessedOnAccount = "--";

                        }
                        ObjRef.AccountRemark = Convert.ToString(dr["AccountRemark"]);
                        ObjRef.RefundedTansactionId = Convert.ToString(dr["RefundedTansactionId"]);

                        ObjRef.RequestStatus = Convert.ToString(dr["RequestStatus"]);
                        //ObjRef.RequestStatusFlag = Convert.ToBoolean(dr["RequestStatusFlag"]);
                        count = count + 1;
                        ObjRef.IndexId = count;

                        ObjRefCase.Add(ObjRef);
                    }
                }
                return Return.returnHttp("200", ObjRefCase, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

    }
}
