using MSUIS_TokenManager.App_Start;
using MSUISApi.BAL;
using MSUISApi.Models;
using Newtonsoft.Json;
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
using System.Text.RegularExpressions;
using System.Web.Helpers;
using System.Web.Http;

namespace MSUISApi.Controllers
{
    public class AdmApplicationEligibilityStatusController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();

        #region AdmApplicationEligibilityStatusGet
        [HttpPost]
        public HttpResponseMessage AdmApplicationEligibilityStatusGet(AdmApplicationEligibilityStatus AAES)
        {
            //String token = Request.Headers.GetValues("token").FirstOrDefault();
            //TokenOperation ac = new TokenOperation();

            //string res = ac.ValidateToken(token);

            //if (res == "0")
            //{
            //    return Return.returnHttp("0", null, null);
            //}
            try
            {
                SqlCommand cmd = new SqlCommand("AdmApplicationEligibilityStatusGet", con);

                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                cmd.Parameters.AddWithValue("@FacultyId", AAES.FacultyId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AAES.AcademicYearId);
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", AAES.ProgrammeInstancePartTermId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<AdmApplicationEligibilityStatus> ObjAAppES = new List<AdmApplicationEligibilityStatus>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AdmApplicationEligibilityStatus ObjAAES = new AdmApplicationEligibilityStatus();
                        ObjAAES.Id = Convert.ToInt64(dr["Id"].ToString());
                        ObjAAES.ApplicantRegistrationId = Convert.ToInt64(dr["ApplicantRegistrationId"].ToString());
                        ObjAAES.ProgrammeInstancePartTermId = Convert.ToInt64(dr["ProgrammeInstancePartTermId"].ToString());
                        ObjAAES.FacultyId = Convert.ToInt32(dr["FacultyId"].ToString());
                        ObjAAES.ProgrammeId = Convert.ToInt32(dr["ProgrammeId"].ToString());
                        ObjAAES.SpecialisationId = Convert.ToInt32(dr["SpecialisationId"].ToString());
                        ObjAAES.FacultyName = dr["FacultyName"].ToString();
                        ObjAAES.ProgrammeName = dr["ProgrammeName"].ToString();
                        ObjAAES.BranchName = dr["BranchName"].ToString();
                        ObjAAES.InstancePartTermName = dr["InstancePartTermName"].ToString();
                        ObjAAES.FirstName = dr["FirstName"].ToString();
                        ObjAAES.MiddleName = dr["MiddleName"].ToString();
                        ObjAAES.LastName = dr["LastName"].ToString();
                        ObjAAES.AcademicYearCode = dr["AcademicYearCode"].ToString();
                        ObjAAES.EligibilityStatus = dr["EligibilityStatus"].ToString();
                        ObjAAES.AdminRemarkByFaculty = dr["AdminRemarkByFaculty"].ToString();
                        ObjAAES.MobileNo = dr["MobileNo"].ToString();
                        ObjAAES.FacultyEmail = dr["FacultyEmail"].ToString();
                        ObjAAES.EmailId = dr["EmailId"].ToString();
                        ObjAAES.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjAAppES.Add(ObjAAES);
                    }
                }
                
                return Return.returnHttp("200", ObjAAppES, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region AcademicYearGet
        [HttpPost]
        public HttpResponseMessage AcademicYearGet()
        {
            try
            {
                //String token = Request.Headers.GetValues("token").FirstOrDefault();
                //TokenOperation TokenOp = new TokenOperation();
                //String ValidTok = TokenOp.ValidateToken(token);

                //if (ValidTok == "0")
                //{
                //    return Return.returnHttp("0", null, null);
                //}

                List<AcademicYear> ObjListAcadYear = new List<AcademicYear>();
                BALAcademicYear ObjBalAcadYearList = new BALAcademicYear();
                ObjListAcadYear = ObjBalAcadYearList.AcademicYearGet();

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

        #region ProgrammeInstancePartTermGet
        [HttpPost]
        public HttpResponseMessage IncProgrammeInstancePartTermGet()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("IncProgrammeInstancePartTermGet", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@FacultyId", PIPT.FacultyId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<ProgrammeInstancePartTerm> ObjLstProgInstPartTerm = new List<ProgrammeInstancePartTerm>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePartTerm ObjProgInstPartTerm = new ProgrammeInstancePartTerm();
                        ObjProgInstPartTerm.Id = Convert.ToInt64(dt.Rows[i]["Id"]);

                        ObjProgInstPartTerm.ProgrammePartTermName = Convert.ToString(dt.Rows[i]["ProgrammePartTermName"]);

                        ObjLstProgInstPartTerm.Add(ObjProgInstPartTerm);

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

        #region UpdateAdmApplicationEligibilityStatus
        [HttpPost]
        public HttpResponseMessage AdmApplicationEligibilityStatusAction(AdmApplicationEligibilityStatus ApplicantInfoJson)

        {
            AdmApplicationEligibilityStatus admApplicationEligibilityStatus = new AdmApplicationEligibilityStatus();
            //dynamic jsonData = jsonobject;

            try
            {
                admApplicationEligibilityStatus.ApplicantRegistrationId = ApplicantInfoJson.ApplicantRegistrationId;
                admApplicationEligibilityStatus.FirstName = ApplicantInfoJson.FirstName;
                admApplicationEligibilityStatus.MiddleName = ApplicantInfoJson.MiddleName;
                admApplicationEligibilityStatus.LastName = ApplicantInfoJson.LastName;
                admApplicationEligibilityStatus.ProgrammeName = ApplicantInfoJson.ProgrammeName;
                admApplicationEligibilityStatus.BranchName = ApplicantInfoJson.BranchName;
                admApplicationEligibilityStatus.EligibilityStatus = ApplicantInfoJson.EligibilityStatus;
                admApplicationEligibilityStatus.AdminRemarkByFaculty = ApplicantInfoJson.AdminRemarkByFaculty;
                admApplicationEligibilityStatus.FacultyEmail = ApplicantInfoJson.FacultyEmail;
                admApplicationEligibilityStatus.MobileNo = ApplicantInfoJson.MobileNo;
                admApplicationEligibilityStatus.EmailId = ApplicantInfoJson.EmailId;
                admApplicationEligibilityStatus.ProgrammeInstancePartTermId = ApplicantInfoJson.ProgrammeInstancePartTermId;


                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                if (admApplicationEligibilityStatus.EligibilityStatus == "Not_Eligible" || admApplicationEligibilityStatus.EligibilityStatus == "Pending")
                {
                    admApplicationEligibilityStatus.AdminRemarkByFaculty = ApplicantInfoJson.AdminRemarkByFaculty;
                 
                }

                
                
                    #region SMS and Email for Applicant Verification By Faculty
                    try
                    {
                        //Your user name
                        string user = "shaurya";
                        ////Your authentication key
                        string key = "ec07fd3102XX";
                        ////Multiple mobiles numbers separated by comma
                        string mobile = admApplicationEligibilityStatus.MobileNo.Trim();
                        ////Sender ID,While using route4 sender id should be 6 characters long.
                        string senderid = "MSUBIS";
                    ////Your message to send, Add URL encoding here.
                    ////string message = System.Web.HttpUtility.UrlEncode("your OTP for MSU Wi-Fi Registration : " + OTP);
                    string message = "Dear Applicant, MSUIS login credentials is Username " + admApplicationEligibilityStatus.ApplicantRegistrationId + " and Password " + admApplicationEligibilityStatus.EligibilityStatus + ". Please do not share this information to anyone.\n- Team MSUB";
                    //string message = "Dear "+ postappverification.Id + ",Your Application has been successfully verified for " + postappverification.InstPartTerm + ". Please Keep Message for Further Verification.\n- Team MSUB";


                    //Prepare you post parameters
                    StringBuilder sbPostData = new StringBuilder();
                        sbPostData.AppendFormat("user={0}", user);
                        sbPostData.AppendFormat("&password={0}", key);
                        sbPostData.AppendFormat("&mobiles={0}", mobile);
                        sbPostData.AppendFormat("&sms={0}", message);
                        sbPostData.AppendFormat("&senderid={0}", senderid);
                        sbPostData.AppendFormat("&entityid={0}", "1201159618592707491");
                        sbPostData.AppendFormat("&tempid={0}", "1207161795812832107");
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

                        #region Email for Applicant Verification By Faculty
                        try
                        {
                            SmtpClient smtpClient = new SmtpClient();
                            MailMessage MailMessage = new MailMessage();
                            //dynamic Jsondata = jObject;

                            string ToEmail = Convert.ToString(admApplicationEligibilityStatus.EmailId);

                            MailAddress fromAddress = new MailAddress(admApplicationEligibilityStatus.FacultyEmail, "MSUIS - Verification Status");

                            smtpClient.Host = "smtp.gmail.com";

                            //Default port will be 25
                            smtpClient.Port = 587;

                            //From address will be given as a MailAddress Object
                            MailMessage.From = fromAddress;
                            MailMessage.To.Add(ToEmail);

                            // To address collection of MailAddress
                            MailMessage.Bcc.Add(admApplicationEligibilityStatus.FacultyEmail);
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
                            "<table align='center' border='0' cellpadding='0' cellspacing='0' style='border-collapse: collapse; width: 100%; max-width: 600px;' class='content'>" +
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
                            "Dear <b> " + admApplicationEligibilityStatus.ApplicantRegistrationId + "</b>," +
                            "<br/><br/><div style='color: green;'>Your Eligibility Status has been  verified for </div>" +
                            "<br/><div style='color: green;'> " + admApplicationEligibilityStatus.FirstName +admApplicationEligibilityStatus.MiddleName+admApplicationEligibilityStatus.LastName+ "</div>" +
                            "<br/><div style='color: green;'> " + admApplicationEligibilityStatus.EligibilityStatus + "</div>" +
                            "<br/><br/>Thank You." +
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
                            myMailCredential.UserName = admApplicationEligibilityStatus.FacultyEmail;
                            myMailCredential.Password = "msu@#$123";
                            smtpClient.UseDefaultCredentials = false;
                            smtpClient.EnableSsl = true;
                            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtpClient.Timeout = 20000;
                            smtpClient.Credentials = myMailCredential;

                            // Send SMTP mail
                            string result = string.Empty;
                            smtpClient.Send(MailMessage);

                            return Return.returnHttp("200", "Please Check Your Eligiblity Status. SMS and Mail has been successfully sent.", null);
                        }
                        catch (Exception e)
                        {
                            return Return.returnHttp("201", e.Message.ToString(), null);
                        }
                        #endregion
                        //return Return.returnHttp("200", "SMS Sent successfully", null);
                    }
                    catch (Exception e)
                    {
                        return Return.returnHttp("201", e.Message.ToString(), null);
                    }
                    #endregion
                    
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion


    }
}