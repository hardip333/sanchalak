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
    public class RefundCaseByAcademicController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

        #region RefCaseAcademicApplicantsList
        [HttpPost]
        public HttpResponseMessage RefCaseAcademicApplicantsList(RefundCaseByAcademic ObjRefund)
        {
            RefundCaseByAcademic modelobj = new RefundCaseByAcademic();

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

                SqlCommand cmd = new SqlCommand("RefCaseAcademicApplicantsList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", modelobj.FacultyId);
               
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<RefundCaseByAcademic> ObjRefCase = new List<RefundCaseByAcademic>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        RefundCaseByAcademic ObjRef = new RefundCaseByAcademic();
                        ObjRef.Id = Convert.ToInt64(dr["Id"]);
                        ObjRef.ApplicationId = Convert.ToInt64(dr["ApplicationId"]);
                        ObjRef.ProgrammeInstancePartTermId = Convert.ToInt64(dr["ProgrammeInstancePartTermId"]);
                        ObjRef.InstancePartTermName = Convert.ToString(dr["InstancePartTermName"]);
                        ObjRef.FullName = Convert.ToString(dr["LastName"]) + " " + Convert.ToString(dr["FirstName"]) + " " + Convert.ToString(dr["MiddleName"]);
                        ObjRef.RequestType = Convert.ToString(dr["RequestType"]);
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

        #region RefCaseAcademicApplicantsDetails
        [HttpPost]
        public HttpResponseMessage RefCaseAcademicApplicantsDetails(RefundCaseApplicantDetail ObjRefundApplicant)
        {
            RefundCaseApplicantDetail modelobj = new RefundCaseApplicantDetail();

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

                SqlCommand cmd = new SqlCommand("RefCaseAcademicApplicantsDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ApplicationId", modelobj.ApplicationId);
                cmd.Parameters.AddWithValue("@RequestType", modelobj.RequestType);

                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<RefundCaseApplicantDetail> ObjRefCaseApp = new List<RefundCaseApplicantDetail>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        RefundCaseApplicantDetail ObjRefApp = new RefundCaseApplicantDetail();
                        ObjRefApp.Id = Convert.ToInt32(dr["Id"]);
                        ObjRefApp.RequestType = Convert.ToString(dr["RequestType"]);
                        ObjRefApp.FacultyRemark = Convert.ToString(dr["FacultyRemark"]);
                        ObjRefApp.CreatedOn = string.Format("{0: dd-MM-yyyy}", Convert.ToString(dr["CreatedOn"]));
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

        #region RefCaseUpdateAcademicRefAmount
        [HttpPost]
        public HttpResponseMessage RefCaseUpdateAcademicRefAmount(JObject jsonobject)

        {
            RefundedAmount RefundedAmount = new RefundedAmount();
            dynamic jsonData = jsonobject;

            try
            {
                RefundedAmount.ApplicationId = Convert.ToInt64(jsonData.ApplicationId);
                RefundedAmount.AcademicRemark = Convert.ToString(jsonData.AcademicRemark);
                RefundedAmount.AmountPaid = Convert.ToDouble(jsonData.AmountPaid);
                RefundedAmount.RequestType = Convert.ToString(jsonData.RequestType);
                RefundedAmount.RefundAmountByAcademic = Convert.ToDouble(jsonData.RefundAmountByAcademic);
                RefundedAmount.AutoDeductAmount = Convert.ToDouble(jsonData.AutoDeductAmount);
                RefundedAmount.RefundModeByAcademic = Convert.ToString(jsonData.RefundMode);
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                RefundedAmount.ProcessedByAcademic = Convert.ToInt64(responseToken);

                SqlCommand cmd = new SqlCommand("RefCaseUpdateAcademicRefAmount", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ApplicationId", RefundedAmount.ApplicationId);
                cmd.Parameters.AddWithValue("@AcademicRemark", RefundedAmount.AcademicRemark);
                cmd.Parameters.AddWithValue("@AmountPaid", RefundedAmount.AmountPaid);
                cmd.Parameters.AddWithValue("@RequestType", RefundedAmount.RequestType);
                cmd.Parameters.AddWithValue("@RefundAmountByAcademic", RefundedAmount.RefundAmountByAcademic);
                cmd.Parameters.AddWithValue("@AutoDeductAmount", RefundedAmount.AutoDeductAmount);
                cmd.Parameters.AddWithValue("@RefundModeByAcademic", RefundedAmount.RefundModeByAcademic);
                cmd.Parameters.AddWithValue("@ProcessedByAcademic", RefundedAmount.ProcessedByAcademic);
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
