using MSUIS_TokenManager.App_Start;
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
    public class VerificationReportController : ApiController
    {

        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

        #region VerificationReportGet

        [HttpPost]
        public HttpResponseMessage VerificationReportGet(VerificationReport VR)
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
                SqlCommand cmd = new SqlCommand();
                if (VR.ChoiceValue == "ALL")
                {
                    cmd = new SqlCommand("AdmApplicantListByIPTAll", con);
                }
                else if (VR.ChoiceValue == "PWI")
                {
                    cmd = new SqlCommand("AdmApplicantListByIPTPWI", con);
                }
                else if (VR.ChoiceValue == "PWC")
                {
                    cmd = new SqlCommand("AdmApplicantListByIPTPWC", con);
                } 
                else if (VR.ChoiceValue == "NV")
                {
                    cmd = new SqlCommand("AdmApplicantListByIPTNV", con);
                }
                else if (VR.ChoiceValue == "VERIFIED")
                {
                    cmd = new SqlCommand("AdmApplicantListByIPTVERIFIED", con);
                }
                else if (VR.ChoiceValue == "VANA")
                {
                    cmd = new SqlCommand("AdmApplicantListByIPTVANA", con);
                }
                else if (VR.ChoiceValue == "VBNS")
                {
                    cmd = new SqlCommand("AdmApplicantListByIPTVBNS", con);
                }
                else if (VR.ChoiceValue == "VAPA")
                {
                    cmd = new SqlCommand("AdmApplicantListByIPTVAPA", con);
                }
                else
                {
                    return Return.returnHttp("201", "No Selection", null);
                }
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", Convert.ToInt64(VR.ProgrammeInstancePartTermId));
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<VerificationReport> ObjLstVR = new List<VerificationReport>();
                Boolean ShowEyeButton = false;
                Boolean ShowVerifyButton = false;
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        VerificationReport ObjVR = new VerificationReport();
                        ObjVR.Id = (((dr["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(dr["Id"]);
                        ObjVR.ApplicantRegistrationId = (((dr["ApplicantRegistrationId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(dr["ApplicantRegistrationId"]);
                        ObjVR.ProgrammeInstancePartTermId = (((dr["ProgrammeInstancePartTermId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(dr["ProgrammeInstancePartTermId"]);

                        ObjVR.NameAsPerMarksheet = (Convert.ToString(dr["NameAsPerMarksheet"])).IsEmpty() ? "Not Available" : Convert.ToString(dr["NameAsPerMarksheet"]);
                        ObjVR.AdminRemarkByFaculty = (Convert.ToString(dr["AdminRemarkByFaculty"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["AdminRemarkByFaculty"]);
                        ObjVR.InstancePartTermName = (Convert.ToString(dr["InstancePartTermName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["InstancePartTermName"]);
                        ObjVR.InstituteName = (Convert.ToString(dr["InstituteName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["InstituteName"]);
                        ObjVR.FeeCategoryName = (Convert.ToString(dr["FeeCategoryName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["FeeCategoryName"]);
                        ObjVR.GroupName = (Convert.ToString(dr["GroupName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["GroupName"]);
                        if (dr["EligibilityStatus"].ToString() == "Provisionally_Approved")
                        {
                            ObjVR.EligibilityStatus = "Provisionally Approved";
                        }
                        else if (dr["EligibilityStatus"].ToString() == "Not_Approved")
                        {
                            ObjVR.EligibilityStatus = "Not Approved";
                        }
                        else if (dr["EligibilityStatus"].ToString() == "Pending")
                        {
                            ObjVR.EligibilityStatus = "Pending";
                        }
                        else if (dr["EligibilityStatus"].ToString() == "" || dr["EligibilityStatus"].ToString() == null)
                        {
                            ObjVR.EligibilityStatus = "Not Verified";
                        }
                        

                        count = count + 1;
                        ObjVR.IndexId = count;

                        ObjLstVR.Add(ObjVR);
                    }
                    
                    if (VR.ChoiceValue == "PWI")
                    {
                        ShowEyeButton = true;
                    }
                    if (VR.ChoiceValue == "PWC")
                    {
                        ShowVerifyButton = true;
                    }
                }
                return Return.returnHttp("200", (ObjLstVR, ShowEyeButton, ShowVerifyButton), null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region FetchDocbyAppId

        [HttpPost]
        public HttpResponseMessage FetchDocbyAppId(VerificationReport VR)
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

                SqlCommand cmd = new SqlCommand("AdmApplicantListPendingDocByAppId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AdmApplicationId", VR.Id);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<VerificationReport> ObjLstVR = new List<VerificationReport>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        VerificationReport ObjVR = new VerificationReport();
                        ObjVR.DocName = (dr["DocName"].ToString());
                        
                        ObjLstVR.Add(ObjVR);
                    }

                }
                return Return.returnHttp("200", ObjLstVR, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion
    }
}
