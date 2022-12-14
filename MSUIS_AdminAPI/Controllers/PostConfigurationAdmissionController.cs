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
    public class PostConfigurationAdmissionController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

        #region PostApplicantListByIncProgrammePartTermId

        [HttpPost]
        public HttpResponseMessage PostApplicantListByIncProgrammePartTermId(PostConfigurationAdmission ObjPostConfiguration)
        {
            PostConfigurationAdmission modelobj = new PostConfigurationAdmission();

            try
            {
                modelobj.ProgrammeInstancePartTermId = ObjPostConfiguration.ProgrammeInstancePartTermId;
                modelobj.EligibilityStatus = ObjPostConfiguration.EligibilityStatus;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("PostApplicantListByIncPartTerm", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", modelobj.ProgrammeInstancePartTermId);
                cmd.Parameters.AddWithValue("@EligibilityStatus", modelobj.EligibilityStatus);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<PostConfigurationAdmission> ObjLstPC = new List<PostConfigurationAdmission>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        PostConfigurationAdmission ObjPC = new PostConfigurationAdmission();
                        ObjPC.Id = Convert.ToInt64(dr["Id"]);
                        ObjPC.ApplicantRegistrationId = Convert.ToInt64(dr["ApplicantRegistrationId"]);
                        ObjPC.AcademicYearId = Convert.ToInt64(dr["AcademicYearId"]);  //Add by Mohini on 18-May-2022
                        ObjPC.LastName = (dr["LastName"].ToString());
                        ObjPC.FirstName = (dr["FirstName"].ToString());
                        ObjPC.MiddleName = (dr["MiddleName"].ToString());
                        ObjPC.FullName = ObjPC.LastName + " " + ObjPC.FirstName + " " + ObjPC.MiddleName;
                        ///ObjPC.ApplicationReservationCode = (dr["ApplicationReservationCode"].ToString());
                        ObjPC.AdminRemarkByFaculty = (dr["AdminRemarkByFaculty"].ToString());
                        //ObjPC.FeeTypeCode = (dr["FeeTypeCode"].ToString());
                        ObjPC.FeeCategoryName = (dr["FeeCategoryName"].ToString());
                        ObjPC.AdminRemarkByAcademics = (dr["AdminRemarkByAcademics"].ToString());
                        ObjPC.InstancePartTermName = (dr["InstancePartTermName"].ToString());
                        //ObjPC.EligibilityStatus = (dr["EligibilityStatus"].ToString());
                        if (dr["EligibilityStatus"].ToString() == "Provisionally_Approved")
                        {
                            ObjPC.EligibilityStatus = "Provisionally Approved";
                        }
                        else if (dr["EligibilityStatus"].ToString() == "Not_Approved")
                        {
                            ObjPC.EligibilityStatus = "Not Approved";
                        }
                        else if (dr["EligibilityStatus"].ToString() == "Pending")
                        {
                            ObjPC.EligibilityStatus = "Pending";
                        }
                        else if (dr["EligibilityStatus"].ToString() == "" || dr["EligibilityStatus"].ToString() == null)
                        {
                            ObjPC.EligibilityStatus = "";
                        }

                        //ObjPC.EligibilityByAcademics = (dr["EligibilityByAcademics"].ToString());
                        if (dr["EligibilityByAcademics"].ToString() == "Eligible")
                        {
                            ObjPC.EligibilityByAcademics = "Eligible";
                        }
                        else if (dr["EligibilityByAcademics"].ToString() == "Not_Eligible")
                        {
                            ObjPC.EligibilityByAcademics = "Not Eligible";
                        }
                        else if (dr["EligibilityByAcademics"].ToString() == "Provisionally_Eligible")
                        {
                            ObjPC.EligibilityByAcademics = "Provisionally Eligible";
                        }
                        else if (dr["EligibilityByAcademics"].ToString() == "Pending")
                        {
                            ObjPC.EligibilityByAcademics = "Pending";
                        }
                        else if (dr["EligibilityByAcademics"].ToString() == "" || dr["EligibilityByAcademics"].ToString() == null)
                        {
                            ObjPC.EligibilityByAcademics = "";
                        }

                        var ApprovedByFaculty = dr["ApprovedByFaculty"];
                        if (ApprovedByFaculty is DBNull)
                        {
                            ApprovedByFaculty = 0;
                            ObjPC.ApprovedByFaculty = Convert.ToInt64(ApprovedByFaculty);
                        }
                        else
                        {
                            ObjPC.ApprovedByFaculty = Convert.ToInt64(dr["ApprovedByFaculty"]);
                        }
                        var ApprovedByAcademics = dr["ApprovedByAcademics"];
                        if (ApprovedByAcademics is DBNull)
                        {
                            ApprovedByAcademics = 0;
                            ObjPC.ApprovedByAcademics = Convert.ToInt64(ApprovedByAcademics);
                        }
                        else
                        {
                            ObjPC.ApprovedByAcademics = Convert.ToInt64(dr["ApprovedByAcademics"]);
                        }

                        var IsVerificationSms = dr["IsVerificationSms"];
                        if (IsVerificationSms is DBNull)
                        {
                            IsVerificationSms = 0;
                            ObjPC.IsVerificationSms = Convert.ToBoolean(IsVerificationSms);
                        }
                        else
                        {
                            ObjPC.IsVerificationSms = Convert.ToBoolean(dr["IsVerificationSms"]);
                        }

                        var IsVerificationEmail = dr["IsVerificationEmail"];
                        if (IsVerificationEmail is DBNull)
                        {
                            IsVerificationEmail = 0;
                            ObjPC.IsVerificationEmail = Convert.ToBoolean(IsVerificationEmail);
                        }
                        else
                        {
                            ObjPC.IsVerificationEmail = Convert.ToBoolean(dr["IsVerificationEmail"]);
                        }

                        if (dr["VerificationStatus"].ToString() == "Verified")
                        {
                            ObjPC.VerificationStatus = "Verified";
                        }
                        else if (dr["VerificationStatus"].ToString() == "Not_Approved")
                        {
                            ObjPC.VerificationStatus = "Not Approved";
                        }
                        else if (dr["VerificationStatus"].ToString() == "Pending")
                        {
                            ObjPC.VerificationStatus = "Pending";
                        }
                        else if (dr["VerificationStatus"].ToString() == "" || dr["VerificationStatus"].ToString() == null)
                        {
                            ObjPC.VerificationStatus = "NA";
                        }

                        if (dr["VerificationRemarks"].ToString() != "" && dr["VerificationRemarks"].ToString() != null)
                        {
                            ObjPC.VerificationRemarks = (dr["VerificationRemarks"].ToString());
                        }
                        else
                        {
                            ObjPC.VerificationRemarks = "NA";
                        }
                            

                        count = count + 1;
                        ObjPC.IndexId = count;

                        ObjLstPC.Add(ObjPC);
                    }

                }
                return Return.returnHttp("200", ObjLstPC, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region PostApprovedApplicantListByIncPartTerm

        [HttpPost]
        public HttpResponseMessage PostApprovedApplicantListByIncPartTerm(PostConfigurationAdmission ObjPostConfiguration)
        {
            PostConfigurationAdmission modelobj = new PostConfigurationAdmission();

            try
            {
                modelobj.ProgrammeInstancePartTermId = ObjPostConfiguration.ProgrammeInstancePartTermId;
                modelobj.InstituteId = ObjPostConfiguration.InstituteId;
                modelobj.FacultyEligibilityStatus = ObjPostConfiguration.FacultyEligibilityStatus;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("PostApprovedApplicantListByIncPartTerm", con);
  		        cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", modelobj.ProgrammeInstancePartTermId);
                cmd.Parameters.AddWithValue("@InstituteId", modelobj.InstituteId);
                cmd.Parameters.AddWithValue("@FacultyEligibilityStatus", modelobj.FacultyEligibilityStatus);

                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<PostConfigurationAdmission> ObjLstPC = new List<PostConfigurationAdmission>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        PostConfigurationAdmission ObjPC = new PostConfigurationAdmission();
                        ObjPC.Id = Convert.ToInt64(dr["Id"]);
                        ObjPC.ApplicantRegistrationId = Convert.ToInt64(dr["ApplicantRegistrationId"]);
                        ObjPC.LastName = (dr["LastName"].ToString());
                        ObjPC.FirstName = (dr["FirstName"].ToString());
                        ObjPC.MiddleName = (dr["MiddleName"].ToString());
                        ObjPC.FullName = ObjPC.LastName + " " + ObjPC.FirstName + " " + ObjPC.MiddleName;
                        ///ObjPC.ApplicationReservationCode = (dr["ApplicationReservationCode"].ToString());
                        ObjPC.AdminRemarkByFaculty = (dr["AdminRemarkByFaculty"].ToString());
                        //ObjPC.FeeTypeCode = (dr["FeeTypeCode"].ToString());
                        ObjPC.FeeCategoryName = (dr["FeeCategoryName"].ToString());
                        ObjPC.AdminRemarkByAcademics = (dr["AdminRemarkByAcademics"].ToString());
                        ObjPC.InstancePartTermName = (dr["InstancePartTermName"].ToString());
                        //ObjPC.EligibilityStatus = (dr["EligibilityStatus"].ToString());
                        if (dr["EligibilityStatus"].ToString() == "Provisionally_Approved")
                        {
                            ObjPC.EligibilityStatus = "Provisionally Approved";
                        }
                        else if (dr["EligibilityStatus"].ToString() == "Not_Approved")
                        {
                            ObjPC.EligibilityStatus = "Not Approved";
                        }
                        else if (dr["EligibilityStatus"].ToString() == "Pending")
                        {
                            ObjPC.EligibilityStatus = "Pending";
                        }
                        else if (dr["EligibilityStatus"].ToString() == "" || dr["EligibilityStatus"].ToString() == null)
                        {
                            ObjPC.EligibilityStatus = "";
                        }

                        //ObjPC.EligibilityByAcademics = (dr["EligibilityByAcademics"].ToString());
                        if (dr["EligibilityByAcademics"].ToString() == "Eligible")
                        {
                            ObjPC.EligibilityByAcademics = "Eligible";
                        }
                        else if (dr["EligibilityByAcademics"].ToString() == "Not_Eligible")
                        {
                            ObjPC.EligibilityByAcademics = "Not Eligible";
                        }
                        else if (dr["EligibilityByAcademics"].ToString() == "Provisionally_Eligible")
                        {
                            ObjPC.EligibilityByAcademics = "Provisionally Eligible";
                        }
                        else if (dr["EligibilityByAcademics"].ToString() == "" || dr["EligibilityByAcademics"].ToString() == null)
                        {
                            ObjPC.EligibilityByAcademics = "";
                        }
                        else if (dr["EligibilityByAcademics"].ToString() == "Pending")
                        {
                            ObjPC.EligibilityByAcademics = "Pending";
                        }

                        var ApprovedByFaculty = dr["ApprovedByFaculty"];
                        if (ApprovedByFaculty is DBNull)
                        {
                            ApprovedByFaculty = 0;
                            ObjPC.ApprovedByFaculty = Convert.ToInt64(ApprovedByFaculty);
                        }
                        else
                        {
                            ObjPC.ApprovedByFaculty = Convert.ToInt64(dr["ApprovedByFaculty"]);
                        }
                        var ApprovedByAcademics = dr["ApprovedByAcademics"];
                        if (ApprovedByAcademics is DBNull)
                        {
                            ApprovedByAcademics = 0;
                            ObjPC.ApprovedByAcademics = Convert.ToInt64(ApprovedByAcademics);
                        }
                        else
                        {
                            ObjPC.ApprovedByAcademics = Convert.ToInt64(dr["ApprovedByAcademics"]);
                        }
                        //ObjPC.TotalCount = Convert.ToInt64(dr["TotalCount"]);
                        //ObjPC.EligibleCountByFaculty = Convert.ToInt64(dr["EligibleCountByFaculty"]);
                        //ObjPC.EligibleCountByAcademic = Convert.ToInt64(dr["EligibleCountByAcademic"]);
                        //ObjPC.AdmFeePaidCount = Convert.ToInt64(dr["AdmFeePaidCount"]);

                        ObjPC.EduColorCode = (dr["EduColorCode"].ToString());
                        ObjPC.EduBoardName = (dr["EduBoardName"].ToString());
                        var IsPRNGenerated = dr["IsPRNGenerated"];
                        if (IsPRNGenerated is DBNull)
                        {
                            IsPRNGenerated = 0;
                            ObjPC.IsPRNGenerated = Convert.ToBoolean(IsPRNGenerated);
                        }
                        else
                        {
                            ObjPC.IsPRNGenerated = Convert.ToBoolean(dr["IsPRNGenerated"]);
                        }

                        ObjPC.UserName = Convert.ToInt64(dr["UserName"]);
                        var IsLastQualifyMS = dr["IsLastQualifyMS"];
                        if (IsLastQualifyMS is DBNull)
                        {
                            IsLastQualifyMS = 0;
                            ObjPC.IsLastQualifyMS = Convert.ToBoolean(IsLastQualifyMS);
                        }
                        else
                        {
                            ObjPC.IsLastQualifyMS = Convert.ToBoolean(dr["IsLastQualifyMS"]);
                        }

                        count = count + 1;
                        ObjPC.IndexId = count;

                        ObjLstPC.Add(ObjPC);
                    }

                }
                return Return.returnHttp("200", ObjLstPC, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region PostApplicantListwithStatusEligibleAndFeeAttachedForAcademicSMS-Email

        [HttpPost]
        public HttpResponseMessage PostApplicantListwithStatusEligibleAndFeeAttached(PostConfigurationAdmission ObjPostConfiguration)
        {
            PostConfigurationAdmission modelobj = new PostConfigurationAdmission();

            try
            {
                modelobj.ProgrammeInstancePartTermId = ObjPostConfiguration.ProgrammeInstancePartTermId;
                //modelobj.FacultyEligibilityStatus = ObjPostConfiguration.FacultyEligibilityStatus;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("PostApplicantListwithEligibleAndFeeAttached", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", modelobj.ProgrammeInstancePartTermId);
                //cmd.Parameters.AddWithValue("@FacultyEligibilityStatus", modelobj.FacultyEligibilityStatus);

                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<PostConfigurationAdmission> ObjLstPC = new List<PostConfigurationAdmission>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        PostConfigurationAdmission ObjPC = new PostConfigurationAdmission();
                        ObjPC.Id = Convert.ToInt64(dr["Id"]);
                        ObjPC.ApplicantRegistrationId = Convert.ToInt64(dr["ApplicantRegistrationId"]);
                        //ObjPC.LastName = (dr["LastName"].ToString());
                        //ObjPC.FirstName = (dr["FirstName"].ToString());
                        //ObjPC.MiddleName = (dr["MiddleName"].ToString());
                        ObjPC.EmailId = (dr["EmailId"].ToString());
                        ObjPC.MobileNo = (dr["MobileNo"].ToString());
                        ObjPC.FullName = dr["LastName"].ToString() + " " + dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString();
                        ///ObjPC.ApplicationReservationCode = (dr["ApplicationReservationCode"].ToString());
                        //ObjPC.AdminRemarkByFaculty = (dr["AdminRemarkByFaculty"].ToString());
                        //ObjPC.FeeTypeCode = (dr["FeeTypeCode"].ToString());
                        ObjPC.FeeCategoryName = (dr["FeeCategoryName"].ToString());
                        //ObjPC.AdminRemarkByAcademics = (dr["AdminRemarkByAcademics"].ToString());
                        ObjPC.InstancePartTermName = (dr["InstancePartTermName"].ToString());
                        //ObjPC.EligibilityStatus = (dr["EligibilityStatus"].ToString());
                        /*if (dr["EligibilityStatus"].ToString() == "Provisionally_Approved")
                        {
                            ObjPC.EligibilityStatus = "Provisionally Approved";
                        }
                        else if (dr["EligibilityStatus"].ToString() == "Not_Approved")
                        {
                            ObjPC.EligibilityStatus = "Not Approved";
                        }
                        else if (dr["EligibilityStatus"].ToString() == "Pending")
                        {
                            ObjPC.EligibilityStatus = "Pending";
                        }
                        else if (dr["EligibilityStatus"].ToString() == "" || dr["EligibilityStatus"].ToString() == null)
                        {
                            ObjPC.EligibilityStatus = "";
                        }

                        //ObjPC.EligibilityByAcademics = (dr["EligibilityByAcademics"].ToString());
                        if (dr["EligibilityByAcademics"].ToString() == "Eligible")
                        {
                            ObjPC.EligibilityByAcademics = "Eligible";
                        }
                        if (dr["EligibilityByAcademics"].ToString() == "Pending")
                        {
                            ObjPC.EligibilityByAcademics = "Pending";
                        }
                        else if (dr["EligibilityByAcademics"].ToString() == "Not_Eligible")
                        {
                            ObjPC.EligibilityByAcademics = "Not Eligible";
                        }
                        else if (dr["EligibilityByAcademics"].ToString() == "Provisionally_Eligible")
                        {
                            ObjPC.EligibilityByAcademics = "Provisionally Eligible";
                        }
                        else if (dr["EligibilityByAcademics"].ToString() == "" || dr["EligibilityByAcademics"].ToString() == null)
                        {
                            ObjPC.EligibilityByAcademics = "";
                        }

                        var ApprovedByFaculty = dr["ApprovedByFaculty"];
                        if (ApprovedByFaculty is DBNull)
                        {
                            ApprovedByFaculty = 0;
                            ObjPC.ApprovedByFaculty = Convert.ToInt64(ApprovedByFaculty);
                        }
                        else
                        {
                            ObjPC.ApprovedByFaculty = Convert.ToInt64(dr["ApprovedByFaculty"]);
                        }
                        var ApprovedByAcademics = dr["ApprovedByAcademics"];
                        if (ApprovedByAcademics is DBNull)
                        {
                            ApprovedByAcademics = 0;
                            ObjPC.ApprovedByAcademics = Convert.ToInt64(ApprovedByAcademics);
                        }
                        else
                        {
                            ObjPC.ApprovedByAcademics = Convert.ToInt64(dr["ApprovedByAcademics"]);
                        }*/

                        var IsVerificationSms = dr["IsVerificationSms"];
                        if (IsVerificationSms is DBNull)
                        {
                            IsVerificationSms = 0;
                            ObjPC.IsVerificationSms = Convert.ToBoolean(IsVerificationSms);
                        }
                        else
                        {
                            ObjPC.IsVerificationSms = Convert.ToBoolean(dr["IsVerificationSms"]);
                        }

                        var IsVerificationEmail = dr["IsVerificationEmail"];
                        if (IsVerificationEmail is DBNull)
                        {
                            IsVerificationEmail = 0;
                            ObjPC.IsVerificationEmail = Convert.ToBoolean(IsVerificationEmail);
                        }
                        else
                        {
                            ObjPC.IsVerificationEmail = Convert.ToBoolean(dr["IsVerificationEmail"]);
                        }
                        ObjPC.SelectedCheck = Convert.ToBoolean(dr["SelectedCheck"]);
                        ObjPC.SelectedCheckEmail = Convert.ToBoolean(dr["SelectedCheckEmail"]);
                        ObjPC.ProgrammeName = (dr["ProgrammeName"].ToString());
                        ObjPC.BranchName = (dr["BranchName"].ToString());
                        ObjPC.AcademicYearCode = (dr["AcademicYearCode"].ToString());
                        //ObjPC.AdmissionFeesStopDate = string.Format("{0: dd-MM-yyyy}", dr["AdmissionFeesStopDate"]);
                        ObjPC.IsVerificationSmsOn = string.Format("{0: dd-MM-yyyy h:mm:ss tt}", dr["IsVerificationSmsOn"]);
                        ObjPC.IsVerificationEmailOn = string.Format("{0: dd-MM-yyyy h:mm:ss tt}", dr["IsVerificationEmailOn"]);

                        count = count + 1;
                        ObjPC.IndexId = count;

                        ObjLstPC.Add(ObjPC);
                    }

                }
                return Return.returnHttp("200", ObjLstPC, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

		#region SendVerificationSMSEmailtoApplicant
        [HttpPost]
        public HttpResponseMessage SendVerificationSMSEmailtoApplicant(JObject jsonobject)

        {
            PostApplicantVerification postappverification = new PostApplicantVerification();
            dynamic jsonData = jsonobject;

            try
            {
                postappverification.Id = jsonData.Id;
                postappverification.ApplicantRegId = jsonData.ApplicantRegistrationId;
                postappverification.FirstName = jsonData.FirstName;
                postappverification.MiddleName = jsonData.MiddleName;
                postappverification.LastName = jsonData.LastName;
                postappverification.MobileNo = jsonData.MobileNo;
                postappverification.EmailId = jsonData.EmailId;
                //postappverification.InstPartTerm = Convert.ToString(jsonData.InstPartTerm);
                postappverification.ProgrammeName = jsonData.ProgrammeName;
                postappverification.BranchName = jsonData.BranchName;
                postappverification.AcademicYearCode = jsonData.AcademicYearCode;
                postappverification.EmailKeyName = jsonData.EmailKeyName;
                postappverification.ProgrammeInstancePartTermId = jsonData.ProgrammeInstancePartTermId;
                postappverification.FinalAdmFeeDate = string.Format("{0: dd-MM-yyyy}", jsonData.FinalAdmFeeDate);


                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                
                postappverification.IsVerificationSmsBy = Convert.ToInt64(responseToken);
                postappverification.IsVerificationEmailBy = Convert.ToInt64(responseToken);

                SqlCommand cmd = new SqlCommand("PostSentVerificationSMSEmail", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", postappverification.Id);
                cmd.Parameters.AddWithValue("@IsVerificationSmsBy", postappverification.IsVerificationSmsBy);
                cmd.Parameters.AddWithValue("@IsVerificationEmailBy", postappverification.IsVerificationEmailBy);
                //cmd.Parameters.AddWithValue("@ApplicantRegId", postappverification.ApplicantRegId);
                //cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", postappverification.ProgrammeInstancePartTermId);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();
                //return Return.returnHttp("200", strMessage, null);

                // *************Start SMS & Email***************


                #region SMS and Email for Applicant Verification SMS
                try
                {
                    //Your user name
                    string user = "shaurya";
                    ////Your authentication key
                    string key = "ec07fd3102XX";
                    ////Multiple mobiles numbers separated by comma
                    string mobile = postappverification.MobileNo.Trim();
                    ////Sender ID,While using route4 sender id should be 6 characters long.
                    string senderid = "MSUBIS";
                    ////Your message to send, Add URL encoding here.
                    ////string message = System.Web.HttpUtility.UrlEncode("your OTP for MSU Wi-Fi Registration : " + OTP);
                    //string message = "Dear Applicant, MSUIS login credentials is Username " + postappverification.Id + " and Password " + postappverification.Id + ". Please do not share this information to anyone.\n- Team MSUB";
                    //string message = "Dear, Your Application form no. " + postappverification.Id + " has been successfully verified. Kindly pay admission fees before " + postappverification.FinalAdmFeeDate + ". Regards,\nMSUB";
                    string message = "Dear Applicant, Your Application for Program: AppNo:" + postappverification.Id + " is provisionally approved %26 Fees Payment can be done from Today upto " + postappverification.FinalAdmFeeDate +" Regards, MSUB";


                    //Prepare you post parameters
                    StringBuilder sbPostData = new StringBuilder();
                    sbPostData.AppendFormat("user={0}", user);
                    sbPostData.AppendFormat("&password={0}", key);

                    sbPostData.AppendFormat("&mobiles={0}", mobile);
                    sbPostData.AppendFormat("&sms={0}", message);
                    sbPostData.AppendFormat("&senderid={0}", senderid);
                    sbPostData.AppendFormat("&entityid={0}", "1201159618592707491");
                    //sbPostData.AppendFormat("&tempid={0}", "1207161795812832107");
                    sbPostData.AppendFormat("&tempid={0}", "1207162146825464560");
                    sbPostData.AppendFormat("&accusage={0}", "1");


                    //Call Send SMS API
                    string sendSMSUri = "http://shaurya.mysmsapps.co.in/sendsms.jsp?";
                    //Create HTTPWebrequest
                    HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                    //Prepare and Add URL Encoded data
                    UTF8Encoding encoding = new UTF8Encoding();
                    byte[] data = encoding.GetBytes(sbPostData.ToString());
                    //Specify post method
                    httpWReq.Method = "POST";
                    httpWReq.ContentType = "application/x-www-form-urlencoded";
                    httpWReq.ContentLength = data.Length;
                    using (Stream stream = httpWReq.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                    //Get the response
                    HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string responseString = reader.ReadToEnd();

                    //Close the response
                    reader.Close();
                    response.Close();

                    #region Email for Applicant Verification Email
                    try
                    {
                        SmtpClient smtpClient = new SmtpClient();
                        MailMessage MailMessage = new MailMessage();
                        //dynamic Jsondata = jObject;

                        string ToEmail = Convert.ToString(postappverification.EmailId);

                        MailAddress fromAddress = new MailAddress(postappverification.EmailKeyName, "MSUIS - Verification Status");

                        smtpClient.Host = "smtp.gmail.com";

                        //Default port will be 25
                        smtpClient.Port = 587;

                        //From address will be given as a MailAddress Object
                        MailMessage.From = fromAddress;
                        MailMessage.To.Add(ToEmail);

                        // To address collection of MailAddress
                        MailMessage.Bcc.Add(postappverification.EmailKeyName);
                        MailMessage.Subject = "MSUIS - Verification Process";
                        MailMessage.IsBodyHtml = true;
                        // Message body content
                        MailMessage.Body = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>" +
                        "<html xmlns ='http://www.w3.org/1999/xhtml'>" +
                        "<head>" +
                        "<meta http-equiv='Content-Type' content ='text/html; charset=UTF-8' />" +
                        "<meta name='viewport' content='width=device-width' />" +
                        "<link rel='icon' href='img/favicon.ico' type='image/x-icon'>" +
                        "<style type='text/css'>" +
                        "@media only screen and(max - width: 550px), screen and(max-device - width: 550px) {" +
                        "body[yahoo].buttonwrapper { background - color: transparent!important; }" +
                        "body[yahoo].button { padding: 0!important; }" +
                        "body[yahoo].button a {" +
                        "background - color: #9b59b6; padding: 15px 25px !important; }}" +
                        "@media only screen and(min-device - width: 601px) {" +
                        ".content { width: 600px!important; }" +
                        ".col387 { width: 387px!important; }}" +
                        "</style>" +
                        "</head>" +
                        "<body bgcolor='#34495E' style='margin: 0; padding: 0;' yahoo='fix'>" +
                        "<table align='center' border='0' cellpadding='0' cellspacing='0' style='border-collapse: collapse; width: 100%;' class='content'>" +
                        "<tr>" +
                        "<td style='padding: 15px 10px 15px 10px;'>" +
                        "</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td align='center' bgcolor='#064E89' style='padding: 20px 20px 20px 20px; color: #ffffff; font-family: Arial, sans-serif; font-size: 36px; font-weight: bold; height:100px;'>" +
                        "THE MAHARAJA SAYAJIRAO UNIVERSITY OF BARODA" +
                        /*"<img src='https://localhost:44374/img/msuLogo.png' alt='MSU Logo' width='152' height='152' style='display:block;' />" +*/
                        "</td>" +
                        "</tr>" +
                        "<tr>" +
                        "</tr>" +
                        "<tr>" +
                        "<td align = 'left' bgcolor = '#ffffff' style = 'padding: 40px 20px 40px 20px; color: #555555; font-family: Arial, sans-serif; font-size: 20px; line-height: 30px; border-bottom: 1px solid #f6f6f6;' >" +
                        "Dear Applicant," +
                        "<br/><br/><div>Your Application has been successfully verified in <b>" + postappverification.ProgrammeName + "-" + postappverification.BranchName + "-" + postappverification.AcademicYearCode + "</b>. </div>" +
                        "<br/><div>Please Pay admission fee for above verified course before <u>" + postappverification.FinalAdmFeeDate + "</u>.</div>" +
                        "<br/><div><ul><b>Following are the steps for fee payment :</b>" +
                        "<li>To check Admission fee Start date and End date please <a href='https://admission.msubaroda.ac.in/applicant/#/application-programme-list'>click here.</a></li>" +
                        "<li>Kindly pay admission fees for this programme before <u>" + postappverification.FinalAdmFeeDate + "</u>.</li>" +
                        "<li>Open Application portal (<u style='color: blue;'>https://admission.msubaroda.ac.in/applicant/#/studentlogin</u>) and use your own login credentials.</li>" +
                        "<li>Go to dashboard and click on above verified application name and then click on <b>Pay Fee</b> button.</li>" +
                        "<li>Fee must be pay online. No cash payment accepted.</li>" +
                        "<li>After the payment of admission fee, your admission will be provisionally approved for particular course.</li>" +
                        "</ul></div>" +

                        "<br/><br/><br/>Thank You." +
                        "<br/>MSUB Team" +
                        "</td>" +//
                        "</tr> " +
                        "<tr>" +
                        "<td align = 'center' bgcolor= '#dddddd' style= 'padding: 15px 10px 15px 10px; color: #555555; font-family: Arial, sans-serif; font-size: 12px; line-height: 18px;'>" +
                        "<b> THE MAHARAJA SAYAJIRAO UNIVERSITY OF BARODA</b><br/>Pratapgunj, Vadodara, Gujarat 390002" +
                        "</td>" +
                        "</tr>" +
                        "</table>" +
                        "</body>" +
                        "</html>";

                        System.Net.NetworkCredential myMailCredential = new System.Net.NetworkCredential();
                        myMailCredential.UserName = postappverification.EmailKeyName;
                        myMailCredential.Password = "msu@#$123";
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.EnableSsl = true;
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtpClient.Timeout = 20000;
                        smtpClient.Credentials = myMailCredential;

                        // Send SMTP mail
                        string result = string.Empty;
                        smtpClient.Send(MailMessage);

                        return Return.returnHttp("200", "Application Verified for Student. SMS and Email has been sent successfully.", null);
                    }
                    catch (Exception e)
                    {
                        return Return.returnHttp("201", e.Message.ToString(), null);
                    }
                    #endregion
                    return Return.returnHttp("200", "SMS Sent successfully", null);
                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message.ToString(), null);
                }
                #endregion


                // *************Stop SMS & Email***************



            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion

        #region SendVerificationSMStoApplicant
        [HttpPost]
        public HttpResponseMessage SendVerificationSMStoApplicant(JObject jsonobject)

        {
            PostApplicantVerification postappverification = new PostApplicantVerification();
            dynamic jsonData = jsonobject;
            
            try
            {
                postappverification.Id = jsonData.Id;
                postappverification.ApplicantRegId = jsonData.ApplicantRegistrationId;
                postappverification.FirstName = jsonData.FirstName;
                postappverification.MiddleName = jsonData.MiddleName;
                postappverification.LastName = jsonData.LastName;
                postappverification.MobileNo = jsonData.MobileNo;
                postappverification.EmailId = jsonData.EmailId;
                //postappverification.InstPartTerm = Convert.ToString(jsonData.InstPartTerm);
                postappverification.ProgrammeName = jsonData.ProgrammeName;
                postappverification.BranchName = jsonData.BranchName;
                postappverification.AcademicYearCode = jsonData.AcademicYearCode;
                postappverification.EmailKeyName = jsonData.EmailKeyName;
                postappverification.ProgrammeInstancePartTermId = jsonData.ProgrammeInstancePartTermId;
                postappverification.FinalAdmFeeDate = string.Format("{0: dd-MM-yyyy}", jsonData.FinalAdmFeeDate);
                

                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }


                postappverification.IsVerificationSmsBy = Convert.ToInt64(responseToken);

                SqlCommand cmd = new SqlCommand("PostSentVerificationSMS", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", postappverification.Id);
                cmd.Parameters.AddWithValue("@IsVerificationSmsBy", postappverification.IsVerificationSmsBy);
                //cmd.Parameters.AddWithValue("@ApplicantRegId", postappverification.ApplicantRegId);
                //cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", postappverification.ProgrammeInstancePartTermId);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();
                //return Return.returnHttp("200", strMessage, null);

                // *************Start SMS & Email***************
                if (strMessage == "Verification SMS has been Sent Successfully.")
                {
                    #region SMS and Email for Applicant Verification By Academic
                    try
                    {
                        //Your user name
                        string user = "shaurya";
                        ////Your authentication key
                        string key = "ec07fd3102XX";
                        ////Multiple mobiles numbers separated by comma
                        string mobile = postappverification.MobileNo.Trim();
                        ////Sender ID,While using route4 sender id should be 6 characters long.
                        string senderid = "MSUBIS";
                        ////Your message to send, Add URL encoding here.
                        ////string message = System.Web.HttpUtility.UrlEncode("your OTP for MSU Wi-Fi Registration : " + OTP);
                        //string message = "Dear Applicant, MSUIS login credentials is Username " + postappverification.Id + " and Password " + postappverification.Id + ". Please do not share this information to anyone.\n- Team MSUB";
                        string message = "Dear, Your Application form no. " + postappverification.Id + " has been successfully verified. Kindly pay admission fees before "+ postappverification.FinalAdmFeeDate + ". Regards,\nMSUB";


                        //Prepare you post parameters
                        StringBuilder sbPostData = new StringBuilder();
                        sbPostData.AppendFormat("user={0}", user);
                        sbPostData.AppendFormat("&password={0}", key);

                        sbPostData.AppendFormat("&mobiles={0}", mobile);
                        sbPostData.AppendFormat("&sms={0}", message);
                        sbPostData.AppendFormat("&senderid={0}", senderid);
                        sbPostData.AppendFormat("&entityid={0}", "1201159618592707491");
                        //sbPostData.AppendFormat("&tempid={0}", "1207161795812832107");
                        sbPostData.AppendFormat("&tempid={0}", "1207161908607186569");
                        sbPostData.AppendFormat("&accusage={0}", "1");


                        //Call Send SMS API
                        string sendSMSUri = "http://shaurya.mysmsapps.co.in/sendsms.jsp?";
                        //Create HTTPWebrequest
                        HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                        //Prepare and Add URL Encoded data
                        UTF8Encoding encoding = new UTF8Encoding();
                        byte[] data = encoding.GetBytes(sbPostData.ToString());
                        //Specify post method
                        httpWReq.Method = "POST";
                        httpWReq.ContentType = "application/x-www-form-urlencoded";
                        httpWReq.ContentLength = data.Length;
                        using (Stream stream = httpWReq.GetRequestStream())
                        {
                            stream.Write(data, 0, data.Length);
                        }
                        //Get the response
                        HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                        StreamReader reader = new StreamReader(response.GetResponseStream());
                        string responseString = reader.ReadToEnd();

                        //Close the response
                        reader.Close();
                        response.Close();

                        return Return.returnHttp("200", "Application Verified for Student. SMS has been sent successfully.", null);
                    }
                    catch (Exception e)
                    {
                        return Return.returnHttp("201", e.Message.ToString(), null);
                    }
                    #endregion

                }
                else
                {
                    return Return.returnHttp("201", strMessage, null);
                }
                // *************Stop SMS & Email***************



            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion

        #region SendVerificationEmailtoApplicant
        [HttpPost]
        public HttpResponseMessage SendVerificationEmailtoApplicant(JObject jsonobject)

        {
            PostApplicantVerification postappverification = new PostApplicantVerification();
            dynamic jsonData = jsonobject;

            try
            {
                postappverification.Id = jsonData.Id;
                postappverification.ApplicantRegId = jsonData.ApplicantRegistrationId;
                postappverification.FirstName = jsonData.FirstName;
                postappverification.MiddleName = jsonData.MiddleName;
                postappverification.LastName = jsonData.LastName;
                postappverification.MobileNo = jsonData.MobileNo;
                postappverification.EmailId = jsonData.EmailId;
                //postappverification.InstPartTerm = Convert.ToString(jsonData.InstPartTerm);
                postappverification.ProgrammeName = jsonData.ProgrammeName;
                postappverification.BranchName = jsonData.BranchName;
                postappverification.AcademicYearCode = jsonData.AcademicYearCode;
                postappverification.EmailKeyName = jsonData.EmailKeyName;
                postappverification.ProgrammeInstancePartTermId = jsonData.ProgrammeInstancePartTermId;
                postappverification.FinalAdmFeeDate = string.Format("{0: dd-MM-yyyy}", jsonData.FinalAdmFeeDate);

                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }


                postappverification.IsVerificationEmailBy = Convert.ToInt64(responseToken);

                SqlCommand cmd = new SqlCommand("PostSentVerificationEmail", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", postappverification.Id);
                cmd.Parameters.AddWithValue("@IsVerificationEmailBy", postappverification.IsVerificationEmailBy);
                //cmd.Parameters.AddWithValue("@ApplicantRegId", postappverification.ApplicantRegId);
                //cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", postappverification.ProgrammeInstancePartTermId);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();
                //return Return.returnHttp("200", strMessage, null);

                // *************Start SMS & Email***************
                if (strMessage == "Verification Email has been Sent Successfully.")
                {
                    #region Email for Applicant Verification By Academic
                    try
                    {
                        SmtpClient smtpClient = new SmtpClient();
                        MailMessage MailMessage = new MailMessage();
                        //dynamic Jsondata = jObject;

                        string ToEmail = Convert.ToString(postappverification.EmailId);

                        MailAddress fromAddress = new MailAddress(postappverification.EmailKeyName, "MSUIS - Verification Status");

                        smtpClient.Host = "smtp.gmail.com";

                        //Default port will be 25
                        smtpClient.Port = 587;

                        //From address will be given as a MailAddress Object
                        MailMessage.From = fromAddress;
                        MailMessage.To.Add(ToEmail);

                        // To address collection of MailAddress
                        MailMessage.Bcc.Add(postappverification.EmailKeyName);
                        MailMessage.Subject = "MSUIS - Verification Process";
                        MailMessage.IsBodyHtml = true;
                        // Message body content
                        MailMessage.Body = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>" +
                        "<html xmlns ='http://www.w3.org/1999/xhtml'>" +
                        "<head>" +
                        "<meta http-equiv='Content-Type' content ='text/html; charset=UTF-8' />" +
                        "<meta name='viewport' content='width=device-width' />" +
                        "<link rel='icon' href='img/favicon.ico' type='image/x-icon'>" +
                        "<style type='text/css'>" +
                        "@media only screen and(max - width: 550px), screen and(max-device - width: 550px) {" +
                        "body[yahoo].buttonwrapper { background - color: transparent!important; }" +
                        "body[yahoo].button { padding: 0!important; }" +
                        "body[yahoo].button a {" +
                        "background - color: #9b59b6; padding: 15px 25px !important; }}" +
                        "@media only screen and(min-device - width: 601px) {" +
                        ".content { width: 600px!important; }" +
                        ".col387 { width: 387px!important; }}" +
                        "</style>" +
                        "</head>" +
                        "<body bgcolor='#34495E' style='margin: 0; padding: 0;' yahoo='fix'>" +
                        "<table align='center' border='0' cellpadding='0' cellspacing='0' style='border-collapse: collapse; width: 100%;' class='content'>" +
                        "<tr>" +
                        "<td style='padding: 15px 10px 15px 10px;'>" +
                        "</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td align='center' bgcolor='#064E89' style='padding: 20px 20px 20px 20px; color: #ffffff; font-family: Arial, sans-serif; font-size: 36px; font-weight: bold; height:100px;'>" +
                        "THE MAHARAJA SAYAJIRAO UNIVERSITY OF BARODA" +
                        /*"<img src='https://localhost:44374/img/msuLogo.png' alt='MSU Logo' width='152' height='152' style='display:block;' />" +*/
                        "</td>" +
                        "</tr>" +
                        "<tr>" +
                        "</tr>" +
                        "<tr>" +
                        "<td align = 'left' bgcolor = '#ffffff' style = 'padding: 40px 20px 40px 20px; color: #555555; font-family: Arial, sans-serif; font-size: 20px; line-height: 30px; border-bottom: 1px solid #f6f6f6;' >" +
                        "Dear Applicant," +
                        "<br/><br/><div>Your Application has been successfully verified in <b>" + postappverification.ProgrammeName + "-" + postappverification.BranchName + "-" + postappverification.AcademicYearCode + "</b>. </div>" +
                        "<br/><div>Please Pay admission fee for above verified course before <u>" + postappverification.FinalAdmFeeDate + "</u>.</div>" +
                        "<br/><div><ul><b>Following are the steps for fee payment :</b>" +
                        "<li>To check Admission fee Start date and End date please <a href='https://admission.msubaroda.ac.in/applicant/#/application-programme-list'>click here.</a></li>" +
                        "<li>Kindly pay admission fees for this programme before <u>" + postappverification.FinalAdmFeeDate + "</u>.</li>" +
                        "<li>Open Application portal (<u style='color: blue;'>https://admission.msubaroda.ac.in/applicant/#/studentlogin</u>) and use your own login credentials.</li>" +
                        "<li>Go to dashboard and click on above verified application name and then click on <b>Pay Fee</b> button.</li>" +
                        "<li>Fee must be pay online. No cash payment accepted.</li>" +
                        "<li>After the payment of admission fee, your admission will be provisionally approved for particular course.</li>" +
                        "</ul></div>" +

                        "<br/><br/><br/>Thank You." +
                        "<br/>MSUB Team" +
                        "</td>" +//
                        "</tr> " +
                        "<tr>" +
                        "<td align = 'center' bgcolor= '#dddddd' style= 'padding: 15px 10px 15px 10px; color: #555555; font-family: Arial, sans-serif; font-size: 12px; line-height: 18px;'>" +
                        "<b> THE MAHARAJA SAYAJIRAO UNIVERSITY OF BARODA</b><br/>Pratapgunj, Vadodara, Gujarat 390002" +
                        "</td>" +
                        "</tr>" +
                        "</table>" +
                        "</body>" +
                        "</html>";

                        System.Net.NetworkCredential myMailCredential = new System.Net.NetworkCredential();
                        myMailCredential.UserName = postappverification.EmailKeyName;
                        myMailCredential.Password = "msu@#$123";
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.EnableSsl = true;
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtpClient.Timeout = 20000;
                        smtpClient.Credentials = myMailCredential;

                        // Send SMTP mail
                        string result = string.Empty;
                        smtpClient.Send(MailMessage);

                        return Return.returnHttp("200", "Application Verified for Student. Email has been sent successfully.", null);
                    }
                    catch (Exception e)
                    {
                        return Return.returnHttp("201", e.Message.ToString(), null);
                    }
                    #endregion

                }
                else
                {
                    return Return.returnHttp("201", strMessage, null);
                }
                // *************Stop SMS & Email***************



            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion

        #region SendVerificationBulkSMStoApplicant
        [HttpPost]
        public HttpResponseMessage SendVerificationBulkSMStoApplicant(List<PostSMSSent> PSS)

        {
            foreach (PostSMSSent datacheck in PSS)
            {
                //if (datacheck.IsVerificationSms == false)
                //{
                    
                    string FinalAdmFeeDate = string.Format("{0: dd-MM-yyyy}", datacheck.FinalAdmFeeDate);

                    string token = Request.Headers.GetValues("token").FirstOrDefault();
                    TokenOperation tokenOperation = new TokenOperation();
                    string responseToken = tokenOperation.ValidateToken(token);
                    if (responseToken == "0")
                    {
                        return Return.returnHttp("0", null, null);
                    }


                    datacheck.IsVerificationSmsBy = Convert.ToInt64(responseToken);

                    SqlCommand cmd = new SqlCommand("PostSentVerificationSMS", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", datacheck.Id);
                    cmd.Parameters.AddWithValue("@IsVerificationSmsBy", datacheck.IsVerificationSmsBy);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    con.Close();
                    //return Return.returnHttp("200", strMessage, null);

                    // *************Start SMS & Email***************
                    if (strMessage == "Verification SMS has been Sent Successfully.")
                    {
                        #region SMS and Email for Applicant Verification By Academic

                        //Your user name
                        string user = "shaurya";
                        ////Your authentication key
                        string key = "ec07fd3102XX";
                        ////Multiple mobiles numbers separated by comma
                        string mobile = datacheck.MobileNo.Trim();
                        ////Sender ID,While using route4 sender id should be 6 characters long.
                        string senderid = "MSUBIS";
                        ////Your message to send, Add URL encoding here.
                        ////string message = System.Web.HttpUtility.UrlEncode("your OTP for MSU Wi-Fi Registration : " + OTP);
                        //string message = "Dear Applicant, MSUIS login credentials is Username " + postappverification.Id + " and Password " + postappverification.Id + ". Please do not share this information to anyone.\n- Team MSUB";
                        //string message = "Dear, Your Application form no." + datacheck.Id + " has been successfully verified in " + datacheck.ProgrammeName + "-" + datacheck.BranchName + "-" + datacheck.AcademicYearCode + ". Regards,\nMSUB";
                        //string message = "Dear, Your Application form no. " + datacheck.Id + " has been successfully verified. Kindly pay admission fees before " + FinalAdmFeeDate + ". Regards,\nMSUB";
                        string message = "Dear Applicant, Your Application for Program: AppNo:" + datacheck.Id + " is provisionally approved %26 Fees Payment can be done from Today upto " + FinalAdmFeeDate + " Regards, MSUB";

                        //Prepare you post parameters
                        StringBuilder sbPostData = new StringBuilder();
                        sbPostData.AppendFormat("user={0}", user);
                        sbPostData.AppendFormat("&password={0}", key);

                        sbPostData.AppendFormat("&mobiles={0}", mobile);
                        sbPostData.AppendFormat("&sms={0}", message);
                        sbPostData.AppendFormat("&senderid={0}", senderid);
                        sbPostData.AppendFormat("&entityid={0}", "1201159618592707491");
                        //sbPostData.AppendFormat("&tempid={0}", "1207161795812832107");
                        sbPostData.AppendFormat("&tempid={0}", "1207162146825464560");
                        sbPostData.AppendFormat("&accusage={0}", "1");


                        //Call Send SMS API
                        string sendSMSUri = "http://shaurya.mysmsapps.co.in/sendsms.jsp?";
                        //Create HTTPWebrequest
                        HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                        //Prepare and Add URL Encoded data
                        UTF8Encoding encoding = new UTF8Encoding();
                        byte[] data = encoding.GetBytes(sbPostData.ToString());
                        //Specify post method
                        httpWReq.Method = "POST";
                        httpWReq.ContentType = "application/x-www-form-urlencoded";
                        httpWReq.ContentLength = data.Length;
                        using (Stream stream = httpWReq.GetRequestStream())
                        {
                            stream.Write(data, 0, data.Length);
                        }
                        //Get the response
                        HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                        StreamReader reader = new StreamReader(response.GetResponseStream());
                        string responseString = reader.ReadToEnd();

                        //Close the response
                        reader.Close();
                        response.Close();

                        #endregion
                    }
                //}

                // *************Stop SMS & Email***************
            }
            return Return.returnHttp("200", "Application Verified for Student. SMS has been sent successfully.", null);
        }

        #endregion

        #region SendVerificationBulkEmailtoApplicant
        static int email_flag_noreply = 0;
        [HttpPost]
        public HttpResponseMessage SendVerificationBulkEmailtoApplicant(List<PostEmailSent> PES)

        {
            int index = 0;
            int stuCount = 0;
            string emailKeyName = "";
            string progName = "";
            string branchName = "";
            string academicYear = "";
            string FinalAdmFeeDate = "";
            foreach (PostEmailSent datacheck in PES)
            {
                index++;
                if (index != PES.Count())
                {
                    if (datacheck.IsVerificationEmail == false && stuCount < 500)
                    {
                        string EmailKeyName = datacheck.EmailKeyName;
                        emailKeyName = datacheck.EmailKeyName;
                        progName = datacheck.ProgrammeName;
                        branchName = datacheck.BranchName;
                        academicYear = datacheck.AcademicYearCode;
                        FinalAdmFeeDate = datacheck.FinalAdmFeeDate;
                        string StuEmailList = datacheck.StuEmailList;

                        string token = Request.Headers.GetValues("token").FirstOrDefault();
                        TokenOperation tokenOperation = new TokenOperation();
                        string responseToken = tokenOperation.ValidateToken(token);
                        if (responseToken == "0")
                        {
                            return Return.returnHttp("0", null, null);
                        }


                        datacheck.IsVerificationEmailBy = Convert.ToInt64(responseToken);

                        SqlCommand cmd = new SqlCommand("PostSentVerificationEmail", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", datacheck.Id);
                        cmd.Parameters.AddWithValue("@IsVerificationEmailBy", datacheck.IsVerificationEmailBy);
                        //cmd.Parameters.AddWithValue("@ApplicantRegId", postappverification.ApplicantRegId);
                        //cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", postappverification.ProgrammeInstancePartTermId);
                        cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                        cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        stuCount++;
                        //return Return.returnHttp("200", strMessage, null);
                    }

                }
                else
                {
                    #region Email for Applicant Verification

                    SmtpClient smtpClient = new SmtpClient();
                    MailMessage MailMessage = new MailMessage();
                    //dynamic Jsondata = jObject;


                    string FromEmailAddress = "";
                    string msuis = "noreply-msuis101@msubaroda.ac.in";
                    string msuis1 = "noreply-msuis102@msubaroda.ac.in";
                    string msuis2 = "noreply-msuis103@msubaroda.ac.in";
                    string msuis3 = "noreply-msuis104@msubaroda.ac.in";
                    string msuis4 = "noreply-msuis105@msubaroda.ac.in";
                    string msuis5 = "noreply-msuis106@msubaroda.ac.in";
                    string msuis6 = "noreply-msuis107@msubaroda.ac.in";
                    string msuis7 = "noreply-msuis108@msubaroda.ac.in";
                    string msuis8 = "noreply-msuis109@msubaroda.ac.in";
                    string msuis9 = "noreply-msuis110@msubaroda.ac.in";
                    string msuis10 = "noreply-msuis111@msubaroda.ac.in";
                    string msuis11 = "noreply-msuis112@msubaroda.ac.in";
                    string msuis12 = "noreply-msuis113@msubaroda.ac.in";
                    string msuis13 = "noreply-msuis114@msubaroda.ac.in";
                    string msuis14 = "noreply-msuis115@msubaroda.ac.in";
                    string msuis15 = "noreply-msuis116@msubaroda.ac.in";
                    string msuis16 = "noreply-msuis117@msubaroda.ac.in";
                    string msuis17 = "noreply-msuis118@msubaroda.ac.in";
                    string msuis18 = "noreply-msuis119@msubaroda.ac.in";
                    string msuis19 = "noreply-msuis120@msubaroda.ac.in";
                    string msuis20 = "noreply-msuis121@msubaroda.ac.in";
                    string msuis21 = "noreply-msuis122@msubaroda.ac.in";
                    string msuis22 = "noreply-msuis123@msubaroda.ac.in";
                    string msuis23 = "noreply-msuis124@msubaroda.ac.in";
                    string msuis24 = "noreply-msuis125@msubaroda.ac.in";
                    string msuis25 = "noreply-msuis126@msubaroda.ac.in";
                    string msuis26 = "noreply-msuis127@msubaroda.ac.in";
                    string msuis27 = "noreply-msuis128@msubaroda.ac.in";
                    string msuis28 = "noreply-msuis129@msubaroda.ac.in";
                    string msuis29 = "noreply-msuis130@msubaroda.ac.in";

                    switch (email_flag_noreply % 30)
                    {
                        case 0:
                            FromEmailAddress = msuis;
                            email_flag_noreply++;
                            break;
                        case 1:
                            FromEmailAddress = msuis1;
                            email_flag_noreply++;
                            break;
                        case 2:
                            FromEmailAddress = msuis2;
                            email_flag_noreply++;
                            break;
                        case 3:
                            FromEmailAddress = msuis3;
                            email_flag_noreply++;
                            break;
                        case 4:
                            FromEmailAddress = msuis4;
                            email_flag_noreply++;
                            break;
                        case 5:
                            FromEmailAddress = msuis5;
                            email_flag_noreply++;
                            break;
                        case 6:
                            FromEmailAddress = msuis6;
                            email_flag_noreply++;
                            break;
                        case 7:
                            FromEmailAddress = msuis7;
                            email_flag_noreply++;
                            break;
                        case 8:
                            FromEmailAddress = msuis8;
                            email_flag_noreply++;
                            break;
                        case 9:
                            FromEmailAddress = msuis9;
                            email_flag_noreply++;
                            break;
                        case 10:
                            FromEmailAddress = msuis10;
                            email_flag_noreply++;
                            break;
                        case 11:
                            FromEmailAddress = msuis11;
                            email_flag_noreply++;
                            break;
                        case 12:
                            FromEmailAddress = msuis12;
                            email_flag_noreply++;
                            break;
                        case 13:
                            FromEmailAddress = msuis13;
                            email_flag_noreply++;
                            break;
                        case 14:
                            FromEmailAddress = msuis14;
                            email_flag_noreply++;
                            break;
                        case 15:
                            FromEmailAddress = msuis15;
                            email_flag_noreply++;
                            break;
                        case 16:
                            FromEmailAddress = msuis16;
                            email_flag_noreply++;
                            break;
                        case 17:
                            FromEmailAddress = msuis17;
                            email_flag_noreply++;
                            break;
                        case 18:
                            FromEmailAddress = msuis18;
                            email_flag_noreply++;
                            break;
                        case 19:
                            FromEmailAddress = msuis19;
                            email_flag_noreply++;
                            break;
                        case 20:
                            FromEmailAddress = msuis20;
                            email_flag_noreply++;
                            break;
                        case 21:
                            FromEmailAddress = msuis21;
                            email_flag_noreply++;
                            break;
                        case 22:
                            FromEmailAddress = msuis22;
                            email_flag_noreply++;
                            break;
                        case 23:
                            FromEmailAddress = msuis23;
                            email_flag_noreply++;
                            break;
                        case 24:
                            FromEmailAddress = msuis24;
                            email_flag_noreply++;
                            break;
                        case 25:
                            FromEmailAddress = msuis25;
                            email_flag_noreply++;
                            break;
                        case 26:
                            FromEmailAddress = msuis26;
                            email_flag_noreply++;
                            break;
                        case 27:
                            FromEmailAddress = msuis27;
                            email_flag_noreply++;
                            break;
                        case 28:
                            FromEmailAddress = msuis28;
                            email_flag_noreply++;
                            break;
                        case 29:
                            FromEmailAddress = msuis29;
                            email_flag_noreply++;
                            break;

                    }


                    string ToEmail = Convert.ToString(datacheck.StuEmailList);

                    MailAddress fromAddress = new MailAddress(FromEmailAddress, "MSUIS - Verification Status");

                    smtpClient.Host = "smtp.gmail.com";

                    //Default port will be 25
                    smtpClient.Port = 587;

                    //From address will be given as a MailAddress Object
                    MailMessage.From = fromAddress;
                    MailMessage.To.Add(FromEmailAddress);

                    // To address collection of MailAddress
                    MailMessage.Bcc.Add(ToEmail);
                    MailMessage.Subject = "MSUIS - Verification Process";
                    MailMessage.IsBodyHtml = true;
                    // Message body content
                    MailMessage.Body = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>" +
                    "<html xmlns ='http://www.w3.org/1999/xhtml'>" +
                    "<head>" +
                    "<meta http-equiv='Content-Type' content ='text/html; charset=UTF-8' />" +
                    "<meta name='viewport' content='width=device-width' />" +
                    "<link rel='icon' href='img/favicon.ico' type='image/x-icon'>" +
                    "<style type='text/css'>" +
                    "@media only screen and(max - width: 550px), screen and(max-device - width: 550px) {" +
                    "body[yahoo].buttonwrapper { background - color: transparent!important; }" +
                    "body[yahoo].button { padding: 0!important; }" +
                    "body[yahoo].button a {" +
                    "background - color: #9b59b6; padding: 15px 25px !important; }}" +
                    "@media only screen and(min-device - width: 601px) {" +
                    ".content { width: 600px!important; }" +
                    ".col387 { width: 387px!important; }}" +
                    "</style>" +
                    "</head>" +
                    "<body bgcolor='#34495E' style='margin: 0; padding: 0;' yahoo='fix'>" +
                    "<table align='center' border='0' cellpadding='0' cellspacing='0' style='border-collapse: collapse; width: 100%;' class='content'>" +
                    "<tr>" +
                    "<td style='padding: 15px 10px 15px 10px;'>" +
                    "</td>" +
                    "</tr>" +
                    "<tr>" +
                    "<td align='center' bgcolor='#064E89' style='padding: 20px 20px 20px 20px; color: #ffffff; font-family: Arial, sans-serif; font-size: 36px; font-weight: bold; height:100px;'>" +
                    "THE MAHARAJA SAYAJIRAO UNIVERSITY OF BARODA" +
                    /*"<img src='https://localhost:44374/img/msuLogo.png' alt='MSU Logo' width='152' height='152' style='display:block;' />" +*/
                    "</td>" +
                    "</tr>" +
                    "<tr>" +
                    "</tr>" +
                    "<tr>" +
                    "<td align = 'left' bgcolor = '#ffffff' style = 'padding: 40px 20px 40px 20px; color: #555555; font-family: Arial, sans-serif; font-size: 20px; line-height: 30px; border-bottom: 1px solid #f6f6f6;' >" +
                    "Dear Applicant," +
                    "<br/><br/><div>Your Application has been successfully verified in <b>" + progName + "-" + branchName + "-" + academicYear + "</b>. </div>" +
                    "<br/><div>Please Pay admission fee for above verified course before <u>" + FinalAdmFeeDate + "</u>.</div>" +
                    "<br/><div><ul><b>Following are the steps for fee payment :</b>" +
                    "<li>To check Admission fee Start date and End date please <a href='https://admission.msubaroda.ac.in/applicant/#/application-programme-list'>click here.</a></li>" +
                     "<li>Kindly pay admission fees for this programme before <u>" + FinalAdmFeeDate + "</u>.</li>" +
                    "<li>Open Application portal (<u style='color: blue;'>https://admission.msubaroda.ac.in/applicant/#/studentlogin</u>) and use your own login credentials.</li>" +
                    "<li>Go to dashboard and click on above verified application name and then click on <b>Pay Fee</b> button.</li>" +
                    "<li>Fee must be pay online. No cash payment accepted.</li>" +
                    "<li>After the payment of admission fee, your admission will be provisionally approved for particular course.</li>" +
                    "</ul></div>" +

                    "<br/><br/><br/>Thank You." +
                    "<br/>MSUB Team" +
                    "</td>" +//
                    "</tr> " +
                    "<tr>" +
                    "<td align = 'center' bgcolor= '#dddddd' style= 'padding: 15px 10px 15px 10px; color: #555555; font-family: Arial, sans-serif; font-size: 12px; line-height: 18px;'>" +
                    "<b> THE MAHARAJA SAYAJIRAO UNIVERSITY OF BARODA</b><br/>Pratapgunj, Vadodara, Gujarat 390002" +
                    "</td>" +
                    "</tr>" +
                    "</table>" +
                    "</body>" +
                    "</html>";

                    System.Net.NetworkCredential myMailCredential = new System.Net.NetworkCredential();
                    myMailCredential.UserName = FromEmailAddress;
                    myMailCredential.Password = "msu@#$123";
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.EnableSsl = true;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.Timeout = 20000;
                    smtpClient.Credentials = myMailCredential;

                    // Send SMTP mail
                    string result = string.Empty;
                    smtpClient.Send(MailMessage);

                    #endregion
                }

                // *************Stop SMS & Email***************
            }
            return Return.returnHttp("200", "Application Verified for Student. Email has been sent successfully.", null);
        }

        #endregion

        #region PostApplicantsCountforSentEmailSMS
        [HttpPost]
        public HttpResponseMessage PostApplicantsCountforSentEmailSMS(SentSMSEmail ObjSentSMSEmail)
        {
            SentSMSEmail modelobj = new SentSMSEmail();

            try
            {
                modelobj.ProgrammeInstancePartTermId = ObjSentSMSEmail.ProgrammeInstancePartTermId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("PostApplicantsCountforEmailSMS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", modelobj.ProgrammeInstancePartTermId);


                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<SentSMSEmail> ObjSmsEmailCount = new List<SentSMSEmail>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        SentSMSEmail Objcount = new SentSMSEmail();


                        Objcount.TotalCount = Convert.ToInt64(dr["TotalCount"]);
                        Objcount.SMSSuccessCount = Convert.ToInt64(dr["SMSSuccessCount"]);
                        Objcount.SMSFailureCount = Convert.ToInt64(dr["SMSFailureCount"]);
                        Objcount.EmailSuccessCount = Convert.ToInt64(dr["EmailSuccessCount"]);
                        Objcount.EmailFailureCount = Convert.ToInt64(dr["EmailFailureCount"]);

                        ObjSmsEmailCount.Add(Objcount);
                    }

                }
                return Return.returnHttp("200", ObjSmsEmailCount, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region PostGetAdmFeeEndDateforVerificationSMSEmail

        [HttpPost]
        public HttpResponseMessage PostGetAdmFeeEndDate(ConfigureDates ObjPostConfiguration)
        {
            ConfigureDates modelobj = new ConfigureDates();

            try
            {
                modelobj.ProgrammeInstancePartTermId = ObjPostConfiguration.ProgrammeInstancePartTermId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("PostGetAdmFeeEndDateforSMSEmail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", modelobj.ProgrammeInstancePartTermId);
               
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<ConfigureDates> ObjLstPC = new List<ConfigureDates>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ConfigureDates ObjPC = new ConfigureDates();
                        ObjPC.AdmissionFeesStopDate = string.Format("{0: dd-MM-yyyy}", dr["AdmissionFeesStopDate"]);

                        ObjLstPC.Add(ObjPC);
                    }
                    return Return.returnHttp("200", ObjLstPC, null);
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

        #region PostApplicantsCountforFacultyVerification
        [HttpPost]
        public HttpResponseMessage PostApplicantsCountforFacultyVerification(FacultyVerificationCount ObjFacultyCount)
        {
            FacultyVerificationCount modelobj = new FacultyVerificationCount();

            try
            {
                modelobj.ProgrammeInstancePartTermId = ObjFacultyCount.ProgrammeInstancePartTermId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("PostCountsforFacultyVerification", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", modelobj.ProgrammeInstancePartTermId);


                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<FacultyVerificationCount> ObjFacCount = new List<FacultyVerificationCount>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        FacultyVerificationCount Objfcount = new FacultyVerificationCount();


                        Objfcount.TotalCountforFaculty = Convert.ToInt64(dr["TotalCountforFaculty"]);
                        Objfcount.ProApprovedCountforFaculty = Convert.ToInt64(dr["ProApprovedCountforFaculty"]);
                        Objfcount.NotApprovedCountforFaculty = Convert.ToInt64(dr["NotApprovedCountforFaculty"]);
                        Objfcount.PendingCountforFaculty = Convert.ToInt64(dr["PendingCountforFaculty"]);

                        ObjFacCount.Add(Objfcount);
                    }

                }
                return Return.returnHttp("200", ObjFacCount, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region PostInstituteGetbyFaculty
        [HttpPost]
        public HttpResponseMessage PostInstituteGetbyFaculty(PostConfigurationAdmission ObjPostConfiguration)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("PostInstituteGetbyFaculty", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@FacultyId", ObjPostConfiguration.FacultyId);
                //da.SelectCommand.Parameters.AddWithValue("@InstituteId", ObjPostConfiguration.InstituteId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<PostConfigurationAdmission> ObjLstProgInstPartTerm = new List<PostConfigurationAdmission>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        PostConfigurationAdmission ObjProgPartTerm = new PostConfigurationAdmission();
                        ObjProgPartTerm.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjProgPartTerm.InstituteName = Convert.ToString(dt.Rows[i]["InstituteName"]);

                        ObjLstProgInstPartTerm.Add(ObjProgPartTerm);

                    }
                    return Return.returnHttp("200", ObjLstProgInstPartTerm, null);
                }
                else
                {
                    return Return.returnHttp("200", "No Record Found", null);
                }

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region PreApplicantListByProgPTIDandAcademicID

        [HttpPost]
        public HttpResponseMessage PreApplicantListByProgPTIDandAcademicID(PostConfigurationAdmission ObjPostConfiguration)
        {
            PostConfigurationAdmission modelobj = new PostConfigurationAdmission();

            try
            {
                modelobj.ProgrammeInstancePartTermId = ObjPostConfiguration.ProgrammeInstancePartTermId;
                modelobj.VerificationStatus = ObjPostConfiguration.VerificationStatus;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("PreApplicantListByProgPTIDandAcademicID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", modelobj.ProgrammeInstancePartTermId);
                cmd.Parameters.AddWithValue("@VerificationStatus", modelobj.VerificationStatus);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<PostConfigurationAdmission> ObjLstPC = new List<PostConfigurationAdmission>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        PostConfigurationAdmission ObjPC = new PostConfigurationAdmission();
                        ObjPC.Id = Convert.ToInt64(dr["Id"]);
                        ObjPC.ApplicantRegistrationId = Convert.ToInt64(dr["ApplicantRegistrationId"]);
                        ObjPC.AcademicYearId = Convert.ToInt64(dr["AcademicYearId"]);  //Add by Mohini on 18-May-2022
                        ObjPC.LastName = (dr["LastName"].ToString());
                        ObjPC.FirstName = (dr["FirstName"].ToString());
                        ObjPC.MiddleName = (dr["MiddleName"].ToString());
                        ObjPC.FullName = ObjPC.LastName + " " + ObjPC.FirstName + " " + ObjPC.MiddleName;
                        ///ObjPC.ApplicationReservationCode = (dr["ApplicationReservationCode"].ToString());
                        ObjPC.AdminRemarkByFaculty = (dr["AdminRemarkByFaculty"].ToString());
                        //ObjPC.FeeTypeCode = (dr["FeeTypeCode"].ToString());
                        ObjPC.FeeCategoryName = (dr["FeeCategoryName"].ToString());
                        ObjPC.AdminRemarkByAcademics = (dr["AdminRemarkByAcademics"].ToString());
                        ObjPC.InstancePartTermName = (dr["InstancePartTermName"].ToString());
                        //ObjPC.EligibilityStatus = (dr["EligibilityStatus"].ToString());
                        if (dr["EligibilityStatus"].ToString() == "Provisionally_Approved")
                        {
                            ObjPC.EligibilityStatus = "Provisionally_Approved";
                        }
                        else if (dr["EligibilityStatus"].ToString() == "Not_Approved")
                        {
                            ObjPC.EligibilityStatus = "Not Approved";
                        }
                        else if (dr["EligibilityStatus"].ToString() == "Pending")
                        {
                            ObjPC.EligibilityStatus = "Pending";
                        }
                        else if (dr["EligibilityStatus"].ToString() == "" || dr["EligibilityStatus"].ToString() == null)
                        {
                            ObjPC.EligibilityStatus = "";
                        }

                        //ObjPC.EligibilityByAcademics = (dr["EligibilityByAcademics"].ToString());
                        if (dr["EligibilityByAcademics"].ToString() == "Eligible")
                        {
                            ObjPC.EligibilityByAcademics = "Eligible";
                        }
                        else if (dr["EligibilityByAcademics"].ToString() == "Not_Eligible")
                        {
                            ObjPC.EligibilityByAcademics = "Not Eligible";
                        }
                        else if (dr["EligibilityByAcademics"].ToString() == "Provisionally_Eligible")
                        {
                            ObjPC.EligibilityByAcademics = "Provisionally Eligible";
                        }
                        else if (dr["EligibilityByAcademics"].ToString() == "Pending")
                        {
                            ObjPC.EligibilityByAcademics = "Pending";
                        }
                        else if (dr["EligibilityByAcademics"].ToString() == "" || dr["EligibilityByAcademics"].ToString() == null)
                        {
                            ObjPC.EligibilityByAcademics = "";
                        }

                        var ApprovedByFaculty = dr["ApprovedByFaculty"];
                        if (ApprovedByFaculty is DBNull)
                        {
                            ApprovedByFaculty = 0;
                            ObjPC.ApprovedByFaculty = Convert.ToInt64(ApprovedByFaculty);
                        }
                        else
                        {
                            ObjPC.ApprovedByFaculty = Convert.ToInt64(dr["ApprovedByFaculty"]);
                        }
                        var ApprovedByAcademics = dr["ApprovedByAcademics"];
                        if (ApprovedByAcademics is DBNull)
                        {
                            ApprovedByAcademics = 0;
                            ObjPC.ApprovedByAcademics = Convert.ToInt64(ApprovedByAcademics);
                        }
                        else
                        {
                            ObjPC.ApprovedByAcademics = Convert.ToInt64(dr["ApprovedByAcademics"]);
                        }

                        var IsVerificationSms = dr["IsVerificationSms"];
                        if (IsVerificationSms is DBNull)
                        {
                            IsVerificationSms = 0;
                            ObjPC.IsVerificationSms = Convert.ToBoolean(IsVerificationSms);
                        }
                        else
                        {
                            ObjPC.IsVerificationSms = Convert.ToBoolean(dr["IsVerificationSms"]);
                        }

                        var IsVerificationEmail = dr["IsVerificationEmail"];
                        if (IsVerificationEmail is DBNull)
                        {
                            IsVerificationEmail = 0;
                            ObjPC.IsVerificationEmail = Convert.ToBoolean(IsVerificationEmail);
                        }
                        else
                        {
                            ObjPC.IsVerificationEmail = Convert.ToBoolean(dr["IsVerificationEmail"]);
                        }

                        if (dr["VerificationStatus"].ToString() == "Verified")
                        {
                            ObjPC.VerificationStatus = "Verified";
                        }
                        else if (dr["VerificationStatus"].ToString() == "Not_Approved")
                        {
                            ObjPC.VerificationStatus = "Not Approved";
                        }
                        else if (dr["VerificationStatus"].ToString() == "Pending")
                        {
                            ObjPC.VerificationStatus = "Pending";
                        }
                        else if (dr["VerificationStatus"].ToString() == "" || dr["VerificationStatus"].ToString() == null)
                        {
                            ObjPC.VerificationStatus = "";
                        }

                        ObjPC.VerificationRemarks = (dr["VerificationRemarks"].ToString());

                        count = count + 1;
                        ObjPC.IndexId = count;

                        ObjLstPC.Add(ObjPC);
                    }

                }
                return Return.returnHttp("200", ObjLstPC, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region PreCountsforFacultyVerification
        [HttpPost]
        public HttpResponseMessage PreCountsforFacultyVerification(FacultyVerificationCount ObjFacultyCount)
        {
            FacultyVerificationCount modelobj = new FacultyVerificationCount();

            try
            {
                modelobj.ProgrammeInstancePartTermId = ObjFacultyCount.ProgrammeInstancePartTermId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("PreCountsforFacultyVerification", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", modelobj.ProgrammeInstancePartTermId);


                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<FacultyVerificationCount> ObjFacCount = new List<FacultyVerificationCount>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        FacultyVerificationCount Objfcount = new FacultyVerificationCount();


                        Objfcount.TotalCountforFaculty = Convert.ToInt64(dr["TotalCountforFaculty"]);
                        Objfcount.VerifiedCountforFaculty = Convert.ToInt64(dr["VerifiedCountforFaculty"]);
                        Objfcount.NotApprovedCountforFaculty = Convert.ToInt64(dr["NotApprovedCountforFaculty"]);
                        Objfcount.PendingCountforFaculty = Convert.ToInt64(dr["PendingCountforFaculty"]);

                        ObjFacCount.Add(Objfcount);
                    }

                }
                return Return.returnHttp("200", ObjFacCount, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region PostCountsforAcademicVerification
        [HttpPost]
        public HttpResponseMessage PostCountsforAcademicVerification(AcademicVerificationCount ObjAcademicCount)
        {
            AcademicVerificationCount modelobj = new AcademicVerificationCount();

            try
            {
                modelobj.ProgrammeInstancePartTermId = ObjAcademicCount.ProgrammeInstancePartTermId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("PostCountsforAcademicVerification", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", modelobj.ProgrammeInstancePartTermId);


                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<AcademicVerificationCount> ObjFacCount = new List<AcademicVerificationCount>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AcademicVerificationCount Objfcount = new AcademicVerificationCount();


                        Objfcount.TotalCount = Convert.ToInt64(dr["TotalCount"]);
                        Objfcount.EligibleCountByFaculty = Convert.ToInt64(dr["EligibleCountByFaculty"]);
                        Objfcount.EligibleCountByAcademic = Convert.ToInt64(dr["EligibleCountByAcademic"]);
                        Objfcount.AdmFeePaidCount = Convert.ToInt64(dr["AdmFeePaidCount"]);

                        ObjFacCount.Add(Objfcount);
                    }

                }
                return Return.returnHttp("200", ObjFacCount, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion
    }
}
