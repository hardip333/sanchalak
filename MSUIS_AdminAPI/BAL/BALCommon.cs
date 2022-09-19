using java.net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace MSUISApi.BAL
{
    public class BALCommon
    {
        //string FromEmailAddress = "";
        static int email_flag = 0;
        #region SendSMS Common Function
        public string SendSMS(string SenderMobileNo, string MessageBody, string TemplateId)
        {
            try
            {
                //Prepare you post parameters
                StringBuilder sbPostData = new StringBuilder();
                sbPostData.AppendFormat("user={0}", "shaurya");
                sbPostData.AppendFormat("&password={0}", "ec07fd3102XX");
                sbPostData.AppendFormat("&mobiles={0}", SenderMobileNo);
                sbPostData.AppendFormat("&sms={0}", MessageBody);
                sbPostData.AppendFormat("&senderid={0}", "MSUBIS");
                sbPostData.AppendFormat("&entityid={0}", "1201159618592707491");
                sbPostData.AppendFormat("&tempid={0}", TemplateId);
                sbPostData.AppendFormat("&accusage={0}", "1");
                //sbPostData.AppendFormat("&responsein={0}","JSON");

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
                //string responseString = reader.ReadToEnd();
                //String messageid = GetReportParameters(responseString, "<messageid>", "</messageid>");
                //bool StatusSMS = SMSstatus(messageid);
                reader.Close();
                response.Close();
                //if (StatusSMS==true)
                //{
                    return "true";
                //}
                //else
                //{
                    
                //}
                
            }
           catch (Exception e)
            {
                return "false";
                //return e.Message.ToString();
            }

        }
        #endregion

        #region Status of SMS By messageid
        public bool SMSstatus(string messageid)
        {
            StringBuilder sbPostData = new StringBuilder();
            sbPostData.AppendFormat("userid={0}", "shaurya");
            sbPostData.AppendFormat("&password={0}", "ec07fd3102XX");
            sbPostData.AppendFormat("&messageid={0}",messageid);
            string sendSMSUri = "http://shaurya.mysmsapps.co.in/getDLR.jsp?";
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
            string deliverystatus = GetReportParameters(responseString, "<deliverystatus>", "</deliverystatus>");
            if(deliverystatus.Equals("DELIVRD"))
            {
                return true;
            }
            else
            {
                string messageid_Status = GetReportParameters(responseString, "<messageid>", "</messageid>");
                string clientsmsid_Status = GetReportParameters(responseString, "<clientsmsid>", "</clientsmsid>");
                string mobileno_Status = GetReportParameters(responseString, "<mobileno>", "</mobileno>");
                string deliverystatus_Status = GetReportParameters(responseString, "<deliverystatus>", "</deliverystatus>");
                string deliverytime_Status = GetReportParameters(responseString, "<deliverytime>", "</deliverytime>");
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlCommand Cmd = new SqlCommand("FailureSMSAdd", con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@messageid", messageid_Status);
                Cmd.Parameters.AddWithValue("@clientsmsid", clientsmsid_Status);
                Cmd.Parameters.AddWithValue("@mobileno", mobileno_Status);
                Cmd.Parameters.AddWithValue("@deliverystatus", deliverystatus_Status);
                Cmd.Parameters.AddWithValue("@deliverytime", deliverytime_Status);
                Cmd.Parameters.AddWithValue("@project", "Sanchalak");
                Cmd.Parameters.AddWithValue("@module", "PRN Generation");
                con.Open();
                Cmd.ExecuteNonQuery();
                con.Close();
                return false;
            }
            
        }
        #endregion

        #region SendEmail Common Function
        public string SendEmail(string ToEmailAddress, string FromEmailAddress, string DisplayName, string Subject, string EmailBody, string Attachment="", string CcEmailAddress="", string BccEmailAddress = "")
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient();
                MailMessage MailMessage = new MailMessage();

                MailAddress fromAddress = new MailAddress(FromEmailAddress, DisplayName);

                smtpClient.Host = "smtp.gmail.com"; //"mail.mail-server.in";

                //Default port will be 25
                smtpClient.Port = 587;

                //From address will be given as a MailAddress Object
                MailMessage.From = fromAddress;
                MailMessage.To.Add(ToEmailAddress);

                // To address collection of MailAddress
                MailMessage.CC.Add(CcEmailAddress);
                MailMessage.Bcc.Add(BccEmailAddress);
                MailMessage.Subject = Subject;
                MailMessage.IsBodyHtml = true;
                // Message body content
                MailMessage.Body = EmailBody;
                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(Attachment);
                MailMessage.Attachments.Add(attachment);
                NetworkCredential myMailCredential = new NetworkCredential();

                myMailCredential.UserName = "noreplymsuis@msubaroda.ac.in";//"prashant@mail-server.in";
                
                myMailCredential.Password = "msu@#$123"; //"Programming5";
                smtpClient.UseDefaultCredentials = true;
                smtpClient.EnableSsl = true;
                //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtpClient.Timeout = 20000;
                smtpClient.Credentials = myMailCredential;

                // Send SMTP mail
                //string result = string.Empty;
                smtpClient.Send(MailMessage);

                return "true";
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }

        }
        #endregion

        #region Get Report Parameters value
        public string GetReportParameters(string responseString, string From, string To)
        {
            int pFrom = responseString.IndexOf(From) + From.Length;
            int pTo = responseString.LastIndexOf(To);
            String GetValue = responseString.Substring(pFrom, pTo - pFrom);

            return GetValue;
        }
        #endregion

        #region SendEmail PRN smtp Function
        public string SendPRNEmail(string ToBcc, string EmailBody)
        {

            try
            {
                string FromEmailAddress = "";
                string prn1 = "msuis.student-prn1@msubaroda.ac.in";
                string prn2 = "msuis.student-prn2@msubaroda.ac.in";
                string prn3 = "msuis.student-prn3@msubaroda.ac.in";
                string prn4 = "msuis.student-prn4@msubaroda.ac.in";
                string prn5 = "msuis.student-prn5@msubaroda.ac.in";
                string prn6 = "msuis.student-prn6@msubaroda.ac.in";
                string prn7 = "msuis.student-prn7@msubaroda.ac.in";
                string prn8 = "msuis.student-prn8@msubaroda.ac.in";
                string prn9 = "msuis.student-prn9@msubaroda.ac.in";
                string prn10 = "msuis.student-prn10@msubaroda.ac.in";

                string prn11 = "msuis.student-prn11@msubaroda.ac.in";
                string prn12 = "msuis.student-prn12@msubaroda.ac.in";
                string prn13 = "msuis.student-prn13@msubaroda.ac.in";
                string prn14 = "msuis.student-prn14@msubaroda.ac.in";
                string prn15 = "msuis.student-prn15@msubaroda.ac.in";
                string prn16 = "msuis.student-prn16@msubaroda.ac.in";
                string prn17 = "msuis.student-prn17@msubaroda.ac.in";
                string prn18 = "msuis.student-prn18@msubaroda.ac.in";
                string prn19 = "msuis.student-prn19@msubaroda.ac.in";
                string prn20 = "msuis.student-prn20@msubaroda.ac.in";


                SmtpClient smtpClient = new SmtpClient();
                MailMessage MailMessage = new MailMessage();
                switch (email_flag % 20)
                {
                    case 0:
                        FromEmailAddress = prn1;
                        email_flag++;
                        break;
                    case 1:
                        FromEmailAddress = prn2;
                        email_flag++;
                        break;
                    case 2:
                        FromEmailAddress = prn3;
                        email_flag++;
                        break;
                    case 3:
                        FromEmailAddress = prn4;
                        email_flag++;
                        break;
                    case 4:
                        FromEmailAddress = prn5;
                        email_flag++;
                        break;
                    case 5:
                        FromEmailAddress = prn6;
                        email_flag++;
                        break;
                    case 6:
                        FromEmailAddress = prn7;
                        email_flag++;
                        break;
                    case 7:
                        FromEmailAddress = prn8;
                        email_flag++;
                        break;
                    case 8:
                        FromEmailAddress = prn9;
                        email_flag++;
                        break;
                    case 9:
                        FromEmailAddress = prn10;
                        email_flag++;
                        break;
                    case 10:
                        FromEmailAddress = prn11;
                        email_flag++;
                        break;
                    case 11:
                        FromEmailAddress = prn12;
                        email_flag++;
                        break;
                    case 12:
                        FromEmailAddress = prn13;
                        email_flag++;
                        break;
                    case 13:
                        FromEmailAddress = prn14;
                        email_flag++;
                        break;
                    case 14:
                        FromEmailAddress = prn15;
                        email_flag++;
                        break;
                    case 15:
                        FromEmailAddress = prn16;
                        email_flag++;
                        break;
                    case 16:
                        FromEmailAddress = prn17;
                        email_flag++;
                        break;
                    case 17:
                        FromEmailAddress = prn18;
                        email_flag++;
                        break;
                    case 18:
                        FromEmailAddress = prn19;
                        email_flag++;
                        break;
                    case 19:
                        FromEmailAddress = prn20;
                        email_flag++;
                        break;
                    
                }


                MailAddress fromAddress = new MailAddress(FromEmailAddress, "MSUIS - THE MAHARAJA SAYAJIRAO UNIVERSITY OF BARODA");

                smtpClient.Host = "smtp.gmail.com";//"smtp.gmail.com";

                //Default port will be 25
                smtpClient.Port = 587;

                //From address will be given as a MailAddress Object
                MailMessage.From = fromAddress;
                //MailMessage.To.Add(ToEmailAddress);

                //// To address collection of MailAddress
                //MailMessage.CC.Add(FromEmailAddress);
                MailMessage.Bcc.Add(ToBcc);
                MailMessage.Subject = "MSUIS - Student Portal Link for Student Login";
                MailMessage.IsBodyHtml = true;
                // Message body content
                MailMessage.Body = EmailBody;

                NetworkCredential myMailCredential = new NetworkCredential();

                myMailCredential.UserName = FromEmailAddress;
                myMailCredential.Password = "msu@#$123";

                smtpClient.UseDefaultCredentials = true;
                smtpClient.EnableSsl = true;
                //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtpClient.Timeout = 20000;
                smtpClient.Credentials = myMailCredential;

                // Send SMTP mail
                //string result = string.Empty;
                smtpClient.Send(MailMessage);

                return "true";
            }
            catch (Exception e)
            {
                return e.Message.ToString() ;
            }

        }
        #endregion
		
		#region ServerFileExists Common Function
        public bool ServerFileExists(String URLName)
        {
            try
            {
                HttpURLConnection.setFollowRedirects(false);
                // note : you may also need
                // HttpURLConnection.setInstanceFollowRedirects(false)
                HttpURLConnection con =
                (HttpURLConnection)new URL(URLName).openConnection();
                con.setRequestMethod("HEAD");
                return (con.getResponseCode() == HttpURLConnection.HTTP_OK);
            }
            catch (Exception e)
            {
                //e.printStackTrace();
                return false;
            }
        }
        #endregion
    }
}