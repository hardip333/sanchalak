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
    public class RefundCaseRefundReportController : ApiController
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
                List<RefundCaseByAccount> ObjLstF = new List<RefundCaseByAccount>();
                dt.Rows.Add("0", "All Faculty");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        RefundCaseByAccount ObjF = new RefundCaseByAccount();
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

        #region RefCaseAccountApplicantsListBOB
        [HttpPost]
        public HttpResponseMessage RefCaseAccountApplicantsListBOB(RefundCaseByAccount ObjRefund)
        {
            string serverUrl = "https://admission.msubaroda.ac.in/MSUISApi/Upload/";
            RefundCaseByAccount modelobj = new RefundCaseByAccount();

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

                SqlCommand cmd = new SqlCommand("RefCaseRefundReportBOB", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", modelobj.FacultyId);

                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<RefundCaseByAccount> ObjRefCase = new List<RefundCaseByAccount>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        RefundCaseByAccount ObjRef = new RefundCaseByAccount();
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
                        ObjRef.RequestType = Convert.ToString(dr["RequestType"]);

                        ObjRef.PassbookDoc = Convert.ToString(dr["PassbookDoc"]);

                        if (dr["PassbookDoc"].ToString() != "" && dr["RequestType"].ToString() == "Fee_Category_Change")
                        {
                            string PassbookDoc = serverUrl + "RefundPassbookPhoto/" + dr["PassbookDoc"].ToString();
                            ObjRef.PassbookDoc = PassbookDoc;
                        }
                        else if (dr["PassbookDoc"].ToString() != "" && dr["RequestType"].ToString() == "Branch_Change")
                        {
                            string PassbookDoc = serverUrl + "BranchChangePassbook/" + dr["PassbookDoc"].ToString();
                            ObjRef.PassbookDoc = PassbookDoc;
                        }
                        else if (dr["PassbookDoc"].ToString() != "" && dr["RequestType"].ToString() == "Institute_Group_Change")
                        {
                            string PassbookDoc = serverUrl + "RefundPassbookPhoto/" + dr["PassbookDoc"].ToString();
                            ObjRef.PassbookDoc = PassbookDoc;
                        }

                        ObjRef.RTGSForm = Convert.ToString(dr["RTGSForm"]);

                        if (dr["RTGSForm"].ToString() != "" && dr["RequestType"].ToString() == "Fee_Category_Change")
                        {
                            string RTGSForm = serverUrl + "FeeChangeRefundRTGSForm/" + dr["RTGSForm"].ToString();
                            ObjRef.RTGSForm = RTGSForm;
                        }
                        else if (dr["RTGSForm"].ToString() != "" && dr["RequestType"].ToString() == "Branch_Change")
                        {
                            string RTGSForm = serverUrl + "BranchChangeRefundRTGSform/" + dr["RTGSForm"].ToString();
                            ObjRef.RTGSForm = RTGSForm;
                        }
                        else if (dr["RTGSForm"].ToString() != "" && dr["RequestType"].ToString() == "Institute_Group_Change")
                        {
                            string RTGSForm = serverUrl + "InstituteGroupChangeRTGSForm/" + dr["RTGSForm"].ToString();
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

        #region RefCaseAccountApplicantsListOther
        [HttpPost]
        public HttpResponseMessage RefCaseAccountApplicantsListOther(RefundCaseByAccount ObjRefund)
        {
            string serverUrl = "https://admission.msubaroda.ac.in/MSUISApi/Upload/";
            RefundCaseByAccount modelobj = new RefundCaseByAccount();

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

                SqlCommand cmd = new SqlCommand("RefCaseRefundReportOther", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", modelobj.FacultyId);

                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<RefundCaseByAccount> ObjRefCase = new List<RefundCaseByAccount>();
       
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        RefundCaseByAccount ObjRef = new RefundCaseByAccount();
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

                        ObjRef.RequestType = Convert.ToString(dr["RequestType"]);

                        ObjRef.PassbookDoc = Convert.ToString(dr["PassbookDoc"]);

                        if (dr["PassbookDoc"].ToString() != "" && dr["RequestType"].ToString() == "Fee_Category_Change")
                        {
                            string PassbookDoc = serverUrl + "RefundPassbookPhoto/" + dr["PassbookDoc"].ToString();
                            ObjRef.PassbookDoc = PassbookDoc;
                        }
                        else if (dr["PassbookDoc"].ToString() != "" && dr["RequestType"].ToString() == "Branch_Change")
                        {
                            string PassbookDoc = serverUrl + "BranchChangePassbook/" + dr["PassbookDoc"].ToString();
                            ObjRef.PassbookDoc = PassbookDoc;
                        }
                        else if (dr["PassbookDoc"].ToString() != "" && dr["RequestType"].ToString() == "Institute_Group_Change")
                        {
                            string PassbookDoc = serverUrl + "RefundPassbookPhoto/" + dr["PassbookDoc"].ToString();
                            ObjRef.PassbookDoc = PassbookDoc;
                        }

                        ObjRef.RTGSForm = Convert.ToString(dr["RTGSForm"]);

                        if (dr["RTGSForm"].ToString() != "" && dr["RequestType"].ToString() == "Fee_Category_Change")
                        {
                            string RTGSForm = serverUrl + "FeeChangeRefundRTGSForm/" + dr["RTGSForm"].ToString();
                            ObjRef.RTGSForm = RTGSForm;
                        }
                        else if (dr["RTGSForm"].ToString() != "" && dr["RequestType"].ToString() == "Branch_Change")
                        {
                            string RTGSForm = serverUrl + "BranchChangeRefundRTGSform/" + dr["RTGSForm"].ToString();
                            ObjRef.RTGSForm = RTGSForm;
                        }
                        else if (dr["RTGSForm"].ToString() != "" && dr["RequestType"].ToString() == "Institute_Group_Change")
                        {
                            string RTGSForm = serverUrl + "InstituteGroupChangeRTGSForm/" + dr["RTGSForm"].ToString();
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
