using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using MSUISApi.BAL;
using Newtonsoft.Json.Linq;

namespace MSUISApi.Controllers
{
    public class AdmApplicationCancelFormController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        BALCommon common = new BALCommon();
        [HttpPost]
        public HttpResponseMessage StudentCancelRequestGetByInstituteId(AdmApplicationCancelForm AppCF)
        {
            try
            {
                Request.Headers.TryGetValues("DepartmentId", out IEnumerable<string> values);
                var DepartmentId = values?.FirstOrDefault();
                //String DepartmentId = Request.Headers.GetValues("DepartmentId").FirstOrDefault();
                Int32 Count = 0;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                Int64 InstituteId = Convert.ToInt64(AppCF.InstituteId);
                SqlCommand cmd = new SqlCommand("StudentCancelRequestGetByInstituteId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (DepartmentId != null || DepartmentId != "" || DepartmentId != "0")
                {
                    cmd.Parameters.AddWithValue("@InstituteId", InstituteId);
                    cmd.Parameters.AddWithValue("@DepartmentId", DepartmentId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@InstituteId", InstituteId);
                }
                //cmd.Parameters.AddWithValue("@InstituteId", InstituteId);
                da.SelectCommand = cmd;
                
                da.Fill(dt);
                List<AdmApplicationCancelForm> Cancellist = new List<AdmApplicationCancelForm>();
                foreach (DataRow DR in dt.Rows)
                {
                    AdmApplicationCancelForm CancelObj = new AdmApplicationCancelForm();
                    CancelObj.Id = Convert.ToInt64(DR["Id"].ToString());
                    CancelObj.ApplicationId = DR["ApplicationFormNo"].ToString();
                    CancelObj.ApplicantUserName = Convert.ToInt64(DR["ApplicantUserName"].ToString());
                    CancelObj.FirstName = DR["FirstName"].ToString();
                    CancelObj.LastName = DR["LastName"].ToString();
                    CancelObj.FullName = DR["FirstName"].ToString()+" " + DR["LastName"].ToString();
                    CancelObj.EmailId = DR["EmailId"].ToString();
                    CancelObj.MobileNo = DR["MobileNo"].ToString();
                    CancelObj.Reason = DR["Reason"].ToString();
                    CancelObj.FacultyRemark = DR["FacultyRemark"].ToString();
                    CancelObj.ApplicationStatus = DR["ApplicationStatus"].ToString();
                    CancelObj.StatusUpdated = Convert.ToBoolean(DR["StatusUpdated"]);
                    CancelObj.InstancePartTermName = DR["InstancePartTermName"].ToString();
                    CancelObj.AllotedInstitute = DR["AllotedInstitute"].ToString();
                    CancelObj.IsAdmissionFeePaid = DR["IsAdmissionFeePaid"].ToString();
                    CancelObj.obj = new ABC();
                    CancelObj.obj.name= DR["ApplicationStatus"].ToString();
                    Count = Count + 1;
                    CancelObj.IndexId = Count;
                   
                    
                   
                    //CancelObj.ApplicationStatus = DR["ApplicationStatus"].ToString();
                    CancelObj.FacultyRemark = DR["FacultyRemark"].ToString();
                    CancelObj.RemarkStatus = DR["RemarkStatus"].ToString();
                    try
                    {
                       string serverUrl = "https://admission.msubaroda.ac.in/MSUISApi/Upload/";
                      
                    var HandwrittenDocument = DR["HandwrittenDocument"];
                    var PassbookDoc = DR["PassbookDoc"];
                    var AuthorityLetter = DR["AuthorityLetter"];
                    var RTGSForm = DR["RTGSForm"];
                        if (HandwrittenDocument is DBNull)
                            CancelObj.HandwrittenDocument = null;
                        else
                        {
                            string HandWrittenFilePath = serverUrl + "StudentApplicationImageDocument/" + DR["HandwrittenDocument"].ToString();
                            CancelObj.HandwrittenDocument = HandWrittenFilePath;
                            
                        }

                        if(PassbookDoc is DBNull)
                            CancelObj.PassbookDoc = null;
                        else
                        {
                            string PassbookDocFilePath = serverUrl + "StudentPassBookImageDocument/" + DR["PassbookDoc"].ToString();
                            CancelObj.PassbookDoc = PassbookDocFilePath;
                        }
                        if (AuthorityLetter is DBNull)
                            CancelObj.AuthorityLetter = null;
                        else
                        {
                            string AuthorityLetterFilePath = serverUrl + "StudentAuthorityLetterDocument/" + DR["AuthorityLetter"].ToString();
                            CancelObj.AuthorityLetter = AuthorityLetterFilePath;
                        }
                        if (RTGSForm is DBNull)
                            CancelObj.RTGSForm = null;
                        else
                        {
                            string RTGSFormFilePath = serverUrl + "StudentRTGSImageDocument/" + DR["RTGSForm"].ToString();
                            CancelObj.RTGSForm = RTGSFormFilePath;
                        }
                    }
                   
                    catch (Exception e)
                    {

                        //return Return.returnHttp("201", e.Message.ToString(), null);

                    }
                    Cancellist.Add(CancelObj);
                }
                return Return.returnHttp("200", Cancellist, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }


        #region AdmApplicationCancelFacultyUpdateEdit
        [HttpPost]
        public HttpResponseMessage AdmApplicationCancelFacultyUpdate(AdmApplicationCancelForm AppCF)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            if (String.IsNullOrWhiteSpace(Convert.ToString(AppCF.FacultyRemark)))
            {
                return Return.returnHttp("201", "Please enter Faculty Remark", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(AppCF.ApplicationStatus)))
            {
                return Return.returnHttp("201", "Please enter ApplicationStatus", null);
            }

            else
            {
                try
                {
                    Int64 UserId = Convert.ToInt64(res.ToString());
                    string today = DateTime.Now.ToString("M/d/yyyy");

                    SqlTransaction ST;
                    String StrMsg = null;
                    String StrMsg1 = null;
                    string FacultyRemark = Convert.ToString(AppCF.FacultyRemark);
                    string FacultyCancelRemark = Convert.ToString(AppCF.FacultyRemark)+"-"+ today;
                    string ApplicationStatus = Convert.ToString(AppCF.ApplicationStatus);
                    Int64 Id = Convert.ToInt64(AppCF.ApplicationId);
                    Boolean IsDeletedApproved = false;
                    Boolean IsDeletedRejected = false;
                    Boolean IsCancelledByFaculty = false;
                    Boolean IsCancelledByStudent = false;
                    if (ApplicationStatus == "Cancel Request Approved")
                    {
                        IsCancelledByFaculty = true;
                        IsCancelledByStudent = true;
                        IsDeletedApproved = true;
                        IsDeletedRejected = false;
                    }
                    if(ApplicationStatus=="Cancel Request Rejected")
                    {
                        IsCancelledByFaculty = false;
                        IsCancelledByStudent = false;
                        IsDeletedRejected = true;
                        IsDeletedApproved = false;
                    }
                    if (ApplicationStatus == "Cancel Request Approved-Forward To Academic")
                    {
                        IsCancelledByFaculty = true;
                        IsCancelledByStudent = true;
                        IsDeletedApproved = false;
                        IsDeletedRejected = false;
                    }
                    if(ApplicationStatus == "Pending")
                    {
                        IsCancelledByFaculty = false;
                        IsCancelledByStudent = false;
                        IsDeletedRejected = true;
                        IsDeletedApproved = false;
                    }
                    con.Open();
                    ST = con.BeginTransaction();
                    try
                    {
                        SqlCommand cmd = new SqlCommand("CancelApplicationFormFacultyEdit", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Transaction = ST;
                     
                        cmd.Parameters.AddWithValue("@ApplicationId", Id);
                        cmd.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
                        cmd.Parameters.AddWithValue("@FacultyRemark", FacultyCancelRemark);
                        cmd.Parameters.AddWithValue("@UserTime", datetime);
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@IsDeleted", IsDeletedRejected);
                        cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                        cmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        StrMsg = Convert.ToString(cmd.Parameters["@Message"].Value);

                        SqlCommand cmd1 = new SqlCommand("AdmApplicationFormFacultyEdit", con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Transaction = ST;
                        cmd1.Parameters.AddWithValue("@Id", Id);
                        cmd1.Parameters.AddWithValue("@FacultyCancelledRemark", FacultyRemark);
                        cmd1.Parameters.AddWithValue("@UserTime", datetime);
                        cmd1.Parameters.AddWithValue("@UserId", UserId);
                        cmd1.Parameters.AddWithValue("@IsDeleted", IsDeletedApproved);
                        cmd1.Parameters.AddWithValue("@IsCancelledByFaculty", IsCancelledByFaculty);
                        cmd1.Parameters.AddWithValue("@IsCancelledByStudent", IsCancelledByStudent);
                        cmd1.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                        cmd1.Parameters["@Message"].Direction = ParameterDirection.Output;
                        
                        cmd1.ExecuteNonQuery();
                        StrMsg1 = Convert.ToString(cmd1.Parameters["@Message"].Value);
                        
                        if (StrMsg1 == "TRUE" && ApplicationStatus == "Pending")
                        {
                            SmtpClient smtpClient = new SmtpClient();
                            MailMessage MailMessage = new MailMessage();
                            //dynamic Jsondata = jObject;

                            //string ToEmail = Convert.ToString(studentFacRemarks.EmailId);
                            //string ToEmail = "jerlin.steffi@gmail.com";
                            string ToEmail = Convert.ToString(AppCF.EmailId);
                           
                            //MailAddress fromAddress = new MailAddress("jerlin.steffi@gmail.com", "MSUIS - Faculty Remarks Regarding Student Query");
                            MailAddress fromAddress = new MailAddress("msuis-remarks@msubaroda.ac.in", "MSUIS - Faculty Remarks Regarding Student Query");

                            smtpClient.Host = "smtp.gmail.com";

                            //Default port will be 25
                            smtpClient.Port = 587;

                            //From address will be given as a MailAddress Object
                            MailMessage.From = fromAddress;
                            MailMessage.To.Add(ToEmail);

                            // To address collection of MailAddress
                            MailMessage.Bcc.Add("jerlin.steffi-cc@msubaroda.ac.in");
                            MailMessage.Subject = "MSUIS - Faculty Remarks Regarding Student Query";
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

                    "</tr>" +
                    "<tr>" +
                    "</tr>" +
                    "<tr>" +
                    "<td align = 'left' bgcolor = '#ffffff' style = 'padding: 40px 20px 40px 20px; color: #555555; font-family: Arial, sans-serif; font-size: 20px; line-height: 30px; border-bottom: 1px solid #f6f6f6;' >" +
                    "Dear <b> " + AppCF.FirstName + " " + AppCF.LastName + "</b>," +
                    "<br/><br/><div style='color: green;'>Your Query has been accepted...</div>" +
                    "<br/>Your Faculty Remarks are as below :" +

                    "<br/>FacultyRemarks : <b>" + AppCF.FacultyRemark + "</b>" +
                    "<br/><br/>Thank You" +
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
                            //myMailCredential.UserName = "jerlin.steffi@gmail.com";
                            myMailCredential.UserName = "msuis-remarks@msubaroda.ac.in";
                            myMailCredential.Password = "msu@#$1234";
                            //myMailCredential.Password = "Joysteve";
                            //smtpClient.UseDefaultCredentials = true;
                            smtpClient.UseDefaultCredentials = false;
                            smtpClient.EnableSsl = true;
                            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtpClient.Timeout = 20000;
                            smtpClient.Credentials = myMailCredential;

                            // Send SMTP mail
                            string result = string.Empty;

                            //Send SMS 
                            string Message = "Dear Applicant, Your Faculty Remark: " + AppCF.FacultyRemark + " for cancellation application form request. \nRegards,\nMSUB.";
                            string SMSResponse = common.SendSMS(AppCF.MobileNo, Message, "1207161908607186569");
                           
                            try
                            {
                                smtpClient.Send(MailMessage);
                                /*if (SMSResponse == "true")
                                {                                  
                                  return Return.returnHttp("200", "SMS Sent successfully", null);
                                }
                               */
                                //strMessage = "Your data has been Updated successfully.";
                            }
                            catch (Exception e)
                            {
                                return Return.returnHttp("201", e.Message.ToString(), null);
                            }
                        }
                     


                        StrMsg1 = "Your Remarks has been Updated successfully.!";
                      

                        ST.Commit();
                        //return Return.returnHttp("200", StrMsg.ToString() + "=====" + StrMsg1.ToString(), null);
                        return Return.returnHttp("200", StrMsg1.ToString(), null);
                    }
                    catch (SqlException sqlError)
                    {
                        ST.Rollback();
                        String strMessageErr = Convert.ToString(sqlError);
                        return Return.returnHttp("201", strMessageErr, null);
                    }
                    //return Return.returnHttp("201", "Hii", null);
                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message, null);
                }
                finally
                {
                    con.Close();
                }
            }
        }
        #endregion

        #region SendMobileOTP
        [HttpPost]
        public HttpResponseMessage SendMobileOTP(JObject jObject)
        {
            dynamic Jsondata = jObject;
            string MobileNo = Convert.ToString(Jsondata.MobileNo);

            string OTP = Convert.ToString(Jsondata.OTP);

            try
            {
                string Message = "Dear Applicant, Your Four Digit OTP No is: " + OTP + " for cancellation application form request. \nRegards,\nMSUB.";
                string SMSResponse = common.SendSMS(MobileNo, Message, "1207161908607186569");
                if (SMSResponse == "true")
                {

                    //code
                    return Return.returnHttp("200", "SMS Sent successfully", null);
                }
                else
                {
                    return Return.returnHttp("201", "SMS failed - SMS not sent", null);
                }



            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion
    }
}
