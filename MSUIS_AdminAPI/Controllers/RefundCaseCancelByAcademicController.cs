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
    public class RefundCaseCancelByAcademicController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

        #region RefCaseCancelAcademicApplicantsList
        [HttpPost]
        public HttpResponseMessage RefCaseCancelAcademicApplicantsList(RefundCaseCancelByAcademic ObjRefund)
        {
            RefundCaseCancelByAcademic modelobj = new RefundCaseCancelByAcademic();

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

                SqlCommand cmd = new SqlCommand("RefCaseCancelAcademicApplicantsList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", modelobj.FacultyId);
               
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<RefundCaseCancelByAcademic> ObjRefCase = new List<RefundCaseCancelByAcademic>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        RefundCaseCancelByAcademic ObjRef = new RefundCaseCancelByAcademic();
                        ObjRef.Id = Convert.ToInt64(dr["Id"]);
                        ObjRef.ApplicationId = Convert.ToInt64(dr["ApplicationId"]);
                        ObjRef.ProgrammeInstancePartTermId = Convert.ToInt64(dr["ProgrammeInstancePartTermId"]);
                        ObjRef.InstancePartTermName = Convert.ToString(dr["InstancePartTermName"]);
                        ObjRef.FullName = Convert.ToString(dr["LastName"]) + " " + Convert.ToString(dr["FirstName"]) + " " + Convert.ToString(dr["MiddleName"]);

                        ObjRef.IsApprovedByAcademic = Convert.ToString(dr["IsApprovedByAcademic"]);
                        if (ObjRef.IsApprovedByAcademic == "True")
                        {
                            ObjRef.IsApprovedByAcademic = "YES";
                        }
                        else if (ObjRef.IsApprovedByAcademic == null || ObjRef.IsApprovedByAcademic == "False" || ObjRef.IsApprovedByAcademic == "")
                        {
                            ObjRef.IsApprovedByAcademic = "NO";
                        }

                        ObjRef.ApprovedOnAcademic = string.Format("{0: dd-MM-yyyy}", Convert.ToString(dr["ApprovedOnAcademic"]));
                        if (ObjRef.ApprovedOnAcademic == null || ObjRef.ApprovedOnAcademic == "")
                        {
                            ObjRef.ApprovedOnAcademic = "--";
                        }

                        ObjRef.IsApprovedByAudit = Convert.ToString(dr["IsApprovedByAudit"]);
                        if (ObjRef.IsApprovedByAudit == "True")
                        {
                            ObjRef.IsApprovedByAudit = "YES";
                        }
                        else if(ObjRef.IsApprovedByAudit == null || ObjRef.IsApprovedByAudit == "False" || ObjRef.IsApprovedByAudit == "")
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

        #region RefCaseCancelAcademicApplicantsDetails
        [HttpPost]
        public HttpResponseMessage RefCaseCancelAcademicApplicantsDetails(RefundCaseCancelApplicantDetail ObjRefundApplicant)
        {
            RefundCaseCancelApplicantDetail modelobj = new RefundCaseCancelApplicantDetail();

            try
            {
                modelobj.ApplicationId = ObjRefundApplicant.ApplicationId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("RefCaseCancelAcademicApplicantsDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ApplicationId", modelobj.ApplicationId);

                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<RefundCaseCancelApplicantDetail> ObjRefCaseApp = new List<RefundCaseCancelApplicantDetail>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        RefundCaseCancelApplicantDetail ObjRefApp = new RefundCaseCancelApplicantDetail();
                        ObjRefApp.Id = Convert.ToInt32(dr["Id"]);
                        ObjRefApp.RequestedOn = string.Format("{0: dd-MM-yyyy}", Convert.ToString(dr["RequestedOn"]));
                        ObjRefApp.FacultyRemark = Convert.ToString(dr["FacultyRemark"]);
                        ObjRefApp.ApprovedOnFaculty = string.Format("{0: dd-MM-yyyy}", Convert.ToString(dr["ApprovedOnFaculty"]));
                        ObjRefApp.InstancePartTermName = Convert.ToString(dr["InstancePartTermName"]);
                        ObjRefApp.FeeName = Convert.ToString(dr["FeeName"]);
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
                        //ObjRefApp.AdmittedOnView = string.Format("{0: dd-MM-yyyy}", Convert.ToString(dr["AdmittedOn"]));
                        ObjRefApp.IsVerificationSMSOnView = string.Format("{0: dd-MM-yyyy}", Convert.ToString(dr["IsVerificationSmsOn"]));
                        ObjRefApp.IsVerificationEmailOnView = string.Format("{0: dd-MM-yyyy}", Convert.ToString(dr["IsVerificationEmailOn"]));
                        ObjRefApp.IsVerificationSms = Convert.ToBoolean(dr["IsVerificationSms"]);
                        ObjRefApp.IsVerificationEmail = Convert.ToBoolean(dr["IsVerificationEmail"]);
                        ObjRefApp.IsPRNGenerated = Convert.ToBoolean(dr["IsPRNGenerated"]);
                        ObjRefApp.InstituteName = Convert.ToString(dr["InstituteName"]);
                        ObjRefApp.GroupName = Convert.ToString(dr["GroupName"]);
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
                        ObjRefApp.StudentCancelledRemark = Convert.ToString(dr["StudentCancelledRemark"]);

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

        #region RefCaseCancelUpdateAcademicRefAmount
        [HttpPost]
        public HttpResponseMessage RefCaseCancelUpdateAcademicRefAmount(JObject jsonobject)

        {
            RefundedCancelAmount RefundedCancelAmount = new RefundedCancelAmount();
            dynamic jsonData = jsonobject;

            try
            {
                RefundedCancelAmount.ApplicationId = Convert.ToInt64(jsonData.ApplicationId);
                RefundedCancelAmount.AcademicRemark = Convert.ToString(jsonData.AcademicRemark);
                RefundedCancelAmount.AmountPaid = Convert.ToDouble(jsonData.AmountPaid);
                RefundedCancelAmount.RefundAmountByAcademic = Convert.ToDouble(jsonData.RefundAmountByAcademic);
                RefundedCancelAmount.AutoDeductAmount = Convert.ToDouble(jsonData.AutoDeductAmount);
                RefundedCancelAmount.RefundModeByAcademic = Convert.ToString(jsonData.RefundMode);
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                RefundedCancelAmount.ApprovedByAcademic = Convert.ToInt64(responseToken);

                SqlCommand cmd = new SqlCommand("RefCaseCancelUpdateAcademicRefAmount", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ApplicationId", RefundedCancelAmount.ApplicationId);
                cmd.Parameters.AddWithValue("@AcademicRemark", RefundedCancelAmount.AcademicRemark);
                cmd.Parameters.AddWithValue("@AmountPaid", RefundedCancelAmount.AmountPaid);
                cmd.Parameters.AddWithValue("@RefundAmountByAcademic", RefundedCancelAmount.RefundAmountByAcademic);
                cmd.Parameters.AddWithValue("@AutoDeductAmount", RefundedCancelAmount.AutoDeductAmount);
                cmd.Parameters.AddWithValue("@RefundModeByAcademic", RefundedCancelAmount.RefundModeByAcademic);
                cmd.Parameters.AddWithValue("@ApprovedByAcademic", RefundedCancelAmount.ApprovedByAcademic);
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
