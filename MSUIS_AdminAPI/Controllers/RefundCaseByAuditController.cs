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
    public class RefundCaseByAuditController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

        #region RefCaseAuditApplicantsList
        [HttpPost]
        public HttpResponseMessage RefCaseAuditApplicantsList(RefundCaseByAudit ObjRefund)
        {
            string serverUrl = "https://admission.msubaroda.ac.in/MSUIS_AdminAPI/Upload/";
            RefundCaseByAudit modelobj = new RefundCaseByAudit();

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

                SqlCommand cmd = new SqlCommand("RefCaseAuditApplicantsList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", modelobj.FacultyId);

                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<RefundCaseByAudit> ObjRefCase = new List<RefundCaseByAudit>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        RefundCaseByAudit ObjRef = new RefundCaseByAudit();
                        ObjRef.Id = Convert.ToInt64(dr["Id"]);
                        ObjRef.ApplicationId = Convert.ToInt64(dr["ApplicationId"]);
                        ObjRef.ProgrammeInstancePartTermId = Convert.ToInt64(dr["ProgrammeInstancePartTermId"]);
                        ObjRef.InstancePartTermName = Convert.ToString(dr["InstancePartTermName"]);
                        ObjRef.FullName = Convert.ToString(dr["LastName"]) + " " + Convert.ToString(dr["FirstName"]) + " " + Convert.ToString(dr["MiddleName"]);
                        ObjRef.RequestType = Convert.ToString(dr["RequestType"]);
                        //var IsApprovedByAudit = dr["IsApprovedByAudit"];
                        //if (IsApprovedByAudit is DBNull)
                        //{
                        //    ObjRef.IsApprovedByAudit = null;
                        //}
                        //else
                        //{
                        //    ObjRef.IsApprovedByAudit = Convert.ToBoolean(dr["IsApprovedByAudit"]);
                        //}
                        //ObjRef.ProcessedOnAudit = string.Format("{0: dd-MM-yyyy}", Convert.ToString(dr["ProcessedOnAudit"]));

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


                        ObjRef.IsApprovedByAcademic = Convert.ToString(dr["IsApprovedByAcademic"]);
                        if (ObjRef.IsApprovedByAcademic == "True")
                        {
                            ObjRef.IsApprovedByAcademic = "YES";
                        }
                        else if (ObjRef.IsApprovedByAcademic == null || ObjRef.IsApprovedByAcademic == "False" || ObjRef.IsApprovedByAcademic == "")
                        {
                            ObjRef.IsApprovedByAcademic = "NO";
                        }

                        ObjRef.ProcessedOnAcademic = string.Format("{0: dd-MM-yyyy}", Convert.ToString(dr["ProcessedOnAcademic"]));
                        if (ObjRef.ProcessedOnAcademic == null || ObjRef.ProcessedOnAcademic == "")
                        {
                            ObjRef.ProcessedOnAcademic = "--";
                        }

                        ObjRef.IsApprovedByAudit = Convert.ToString(dr["IsApprovedByAudit"]);
                        if (ObjRef.IsApprovedByAudit == "True")
                        {
                            ObjRef.IsApprovedByAudit = "YES";
                        }
                        else if (ObjRef.IsApprovedByAudit == null || ObjRef.IsApprovedByAudit == "False" || ObjRef.IsApprovedByAudit == "")
                        {
                            ObjRef.IsApprovedByAudit = "NO";
                        }

                        ObjRef.ProcessedOnAudit = string.Format("{0: dd-MM-yyyy}", Convert.ToString(dr["ProcessedOnAudit"]));
                        if (ObjRef.ProcessedOnAudit == null || ObjRef.ProcessedOnAudit == "")
                        {
                            ObjRef.ProcessedOnAudit = "--";
                        }

                        ObjRef.IsApprovedByAccount = Convert.ToString(dr["IsApprovedByAccount"]);
                        if (ObjRef.IsApprovedByAccount == "True")
                        {
                            ObjRef.IsApprovedByAccount = "YES";
                        }
                        else if (ObjRef.IsApprovedByAccount == null || ObjRef.IsApprovedByAccount == "False" || ObjRef.IsApprovedByAccount == "")
                        {
                            ObjRef.IsApprovedByAccount = "NO";
                        }

                        ObjRef.ProcessedOnAccount = string.Format("{0: dd-MM-yyyy}", Convert.ToString(dr["ProcessedOnAccount"]));
                        if (ObjRef.ProcessedOnAccount == null || ObjRef.ProcessedOnAccount == "")
                        {
                            ObjRef.ProcessedOnAccount = "--";
                        }

                        var AmountPaid = dr["AmountPaid"];
                        if (AmountPaid is DBNull)
                        {
                            ObjRef.AmountPaid = null;
                        }
                        else
                        {
                            ObjRef.AmountPaid = Convert.ToDecimal(dr["AmountPaid"]);
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

        #region RefCaseAuditApplicantsDetails
        [HttpPost]
        public HttpResponseMessage RefCaseAuditApplicantsDetails(RefundCaseAuditApplicantDetail ObjRefundApplicant)
        {
            RefundCaseAuditApplicantDetail modelobj = new RefundCaseAuditApplicantDetail();

            try
            {
                modelobj.ApplicationId = ObjRefundApplicant.ApplicationId;
                modelobj.RequestType = ObjRefundApplicant.RequestType;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("RefCaseAuditApplicantsDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ApplicationId", modelobj.ApplicationId);
                cmd.Parameters.AddWithValue("@RequestType", modelobj.RequestType);

                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<RefundCaseAuditApplicantDetail> ObjRefCaseApp = new List<RefundCaseAuditApplicantDetail>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        RefundCaseAuditApplicantDetail ObjRefApp = new RefundCaseAuditApplicantDetail();
                        ObjRefApp.Id = Convert.ToInt32(dr["Id"]);
                        ObjRefApp.RequestType = Convert.ToString(dr["RequestType"]);
                        ObjRefApp.AcademicRemark = Convert.ToString(dr["AcademicRemark"]);
                        ObjRefApp.OldInstancePartTermName = Convert.ToString(dr["OldInstancePartTermName"]);
                        ObjRefApp.NewInstancePartTermName = Convert.ToString(dr["NewInstancePartTermName"]);
                        ObjRefApp.OldFeeCategoryName = Convert.ToString(dr["OldFeeCategoryName"]);
                        ObjRefApp.NewFeeCategoryName = Convert.ToString(dr["NewFeeCategoryName"]);
                        ObjRefApp.IsAdmissionFeePaid = Convert.ToBoolean(dr["IsAdmissionFeePaid"]);
                        ObjRefApp.NameAsPerMarksheet = Convert.ToString(dr["NameAsPerMarksheet"]);
                        ObjRefApp.FacultyName = Convert.ToString(dr["FacultyName"]);
                        ObjRefApp.FacultyId = Convert.ToInt32(dr["FacultyId"]);
                        ObjRefApp.ProgrammeName = Convert.ToString(dr["ProgrammeName"]);
                        ObjRefApp.BranchName = Convert.ToString(dr["BranchName"]);
                        ObjRefApp.EligibilityStatus = Convert.ToString(dr["EligibilityStatus"]);
                        if (ObjRefApp.EligibilityStatus == "Provisionally_Approved")
                        {
                            ObjRefApp.EligibilityStatus = "Provisionally Approved";
                        }
                        ObjRefApp.EligibilityByAcademics = Convert.ToString(dr["EligibilityByAcademics"]);
                        ObjRefApp.ApplicationId = Convert.ToInt64(dr["ApplicationId"]);
                        ObjRefApp.AdmittedOnView = string.Format("{0: dd-MM-yyyy}", Convert.ToString(dr["AdmittedOn"]));
                        ObjRefApp.IsVerificationSMSOnView = string.Format("{0: dd-MM-yyyy}", Convert.ToString(dr["IsVerificationSmsOn"]));
                        ObjRefApp.IsVerificationEmailOnView = string.Format("{0: dd-MM-yyyy}", Convert.ToString(dr["IsVerificationEmailOn"]));
                        ObjRefApp.IsVerificationSms = Convert.ToBoolean(dr["IsVerificationSms"]);
                        ObjRefApp.IsVerificationEmail = Convert.ToBoolean(dr["IsVerificationEmail"]);
                        ObjRefApp.IsPRNGenerated = Convert.ToBoolean(dr["IsPRNGenerated"]);
                        ObjRefApp.OldInstituteName = Convert.ToString(dr["OldInstituteName"]);
                        ObjRefApp.NewInstituteName = Convert.ToString(dr["NewInstituteName"]);
                        ObjRefApp.OldGroupName = Convert.ToString(dr["OldGroupName"]);
                        ObjRefApp.NewGroupName = Convert.ToString(dr["NewGroupName"]);
                        ObjRefApp.FeePaidDate = string.Format("{0: dd-MM-yyyy}", Convert.ToString(dr["FeePaidDate"]));
                        var AmountPaid = dr["AmountPaid"];
                        if (AmountPaid is DBNull)
                        {
                            ObjRefApp.AmountPaid = null;
                        }
                        else
                        {
                            ObjRefApp.AmountPaid = Convert.ToDecimal(dr["AmountPaid"]);
                        }

                        var TotalAmount = dr["TotalAmount"];
                        if (TotalAmount is DBNull)
                        {
                            ObjRefApp.TotalAmount = null;
                        }
                        else
                        {
                            ObjRefApp.TotalAmount = Convert.ToDecimal(dr["TotalAmount"]);
                        }

                        var RefundAmountByAcademic = dr["RefundAmountByAcademic"];
                        if (RefundAmountByAcademic is DBNull)
                        {
                            ObjRefApp.RefundAmountByAcademic = null;
                        }
                        else
                        {
                            ObjRefApp.RefundAmountByAcademic = Convert.ToDecimal(dr["RefundAmountByAcademic"]);
                        }
                        ObjRefApp.ProcessedOnAcademic = string.Format("{0: dd-MM-yyyy}", Convert.ToString(dr["ProcessedOnAcademic"]));

                        //ObjRefApp.FeeCategoryPartTermMapId = Convert.ToInt64(dr["FeeCategoryPartTermMapId"]);
                        //ObjRefApp.FeeCategoryId = Convert.ToInt32(dr["FeeCategoryId"]);
                        //ObjRefApp.RequestedOn = string.Format("{0: dd-MM-yyyy}", Convert.ToString(dr["RequestedOn"]));

                        ObjRefCaseApp.Add(ObjRefApp);
                    }

                }
                return Return.returnHttp("200", ObjRefCaseApp, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region RefCaseUpdateAuditRefAmount
        [HttpPost]
        public HttpResponseMessage RefCaseUpdateAuditRefAmount(JObject jsonobject)

        {
            RefundedAmountAudit RefundedAmountAudit = new RefundedAmountAudit();
            dynamic jsonData = jsonobject;

            try
            {
                RefundedAmountAudit.ApplicationId = Convert.ToInt64(jsonData.ApplicationId);
                RefundedAmountAudit.AuditRemark = Convert.ToString(jsonData.AuditRemark);
                RefundedAmountAudit.RefundAmountByAudit = Convert.ToDouble(jsonData.AmountRefundByAudit);
                RefundedAmountAudit.RequestType = Convert.ToString(jsonData.RequestType);
                RefundedAmountAudit.RefundAmountByAcademic = Convert.ToDouble(jsonData.RefundAmountByAcademic);
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                RefundedAmountAudit.ProcessedByAudit = Convert.ToInt64(responseToken);

                SqlCommand cmd = new SqlCommand("RefCaseUpdateAuditRefAmount", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ApplicationId", RefundedAmountAudit.ApplicationId);
                cmd.Parameters.AddWithValue("@AuditRemark", RefundedAmountAudit.AuditRemark);
                cmd.Parameters.AddWithValue("@RefundAmountByAcademic", RefundedAmountAudit.RefundAmountByAcademic);
                cmd.Parameters.AddWithValue("@RequestType", RefundedAmountAudit.RequestType);
                cmd.Parameters.AddWithValue("@RefundAmountByAudit", RefundedAmountAudit.RefundAmountByAudit);
                cmd.Parameters.AddWithValue("@ProcessedByAudit", RefundedAmountAudit.ProcessedByAudit);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                return Return.returnHttp("200", strMessage, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion

    }
}
