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
using System.Web.Http;
namespace MSUISApi.Controllers
{
    public class InstituteQueryController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

        #region InstituteQuery Add
        [HttpPost]
        public HttpResponseMessage InstituteQueryAdd(InstituteQuery instituteQuery)
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
                    InstituteQuery iq = new InstituteQuery();
                

                    Int64 UserRegistrationId = Convert.ToInt64(instituteQuery.UserRegistrationId);
                    Int64 InstituteId = Convert.ToInt32(instituteQuery.InstituteId);
                    Int64 FacultyId = Convert.ToInt32(instituteQuery.FacultyId);
                    string Query = Convert.ToString(instituteQuery.Query);
                    string QueryDescription = Convert.ToString(instituteQuery.QueryDescription);
                    string UploadImage = Convert.ToString(instituteQuery.UploadImage);
                //if (instituteQuery.UploadImage != null)
                //{
                //    instituteQuery.UploadImage = instituteQuery + "_" + instituteQuery.UploadImage;
                //}
                //else
                //{
                //    instituteQuery.UploadImage = instituteQuery.UploadImage;
                //}
                Int64 UserId = 1234;

                    SqlCommand cmd = new SqlCommand("InstituteQueryMasterAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                if (String.IsNullOrEmpty(Convert.ToString(Query)))
                {
                    return Return.returnHttp("201", "Please enter Query", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(QueryDescription)))
                {
                    return Return.returnHttp("201", "Please select Query Description", null);
                }
                else
                {

                    cmd.Parameters.AddWithValue("@UserRegistrationId", UserRegistrationId);
                    cmd.Parameters.AddWithValue("@InstituteId", InstituteId);
                    cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                    cmd.Parameters.AddWithValue("@Query", Query);
                    cmd.Parameters.AddWithValue("@QueryDescription", QueryDescription);
                    cmd.Parameters.AddWithValue("@UploadImage", UploadImage);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);

                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);

                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    con.Open();

                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);

                    con.Close();
                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Your data has been added successfully.";
                        SmtpClient smtpClient = new SmtpClient();
                        MailMessage MailMessage = new MailMessage();
                        dynamic Jsondata = instituteQuery;

                        string FacultyEmail = Convert.ToString(instituteQuery.FacultyEmail);

                        //string base64Encoded = Jsondata.OTP;
                        //string OTP;
                        //byte[] data = System.Convert.FromBase64String(base64Encoded);
                        //OTP = System.Text.ASCIIEncoding.ASCII.GetString(data);

                        MailAddress fromAddress = new MailAddress("support.msuis-cc@msubaroda.ac.in");

                        smtpClient.Host = "smtp.gmail.com";

                        //Default port will be 25
                        smtpClient.Port = 587;

                        //From address will be given as a MailAddress Object
                        MailMessage.From = fromAddress;
                        MailMessage.To.Add(FacultyEmail);

                        // To address collection of MailAddress
                        MailMessage.Bcc.Add("support.msuis-cc@msubaroda.ac.in");
                        MailMessage.Subject = "MSUIS - Institute Query";

                        //Body can be Html or text format
                        //Specify true if it  is html message
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
                            "<td align = 'left' bgcolor = '#ffffff' style = 'padding: 10px 10px 10px 10px; color: #555555; font-family: Arial, sans-serif; font-size: 15px; border-bottom: 1px solid #f6f6f6;' >" +
                            "User Name:<b> " + instituteQuery.UserName + "</b>" +
                            "</td>" +
                            "</tr> " +
                            "<tr>" +
                            "<td align = 'left' bgcolor = '#ffffff' style = 'padding: 10px 10px 10px 10px; color: #555555; font-family: Arial, sans-serif; font-size: 15px; border-bottom: 1px solid #f6f6f6;' >" +
                            "Institute Name:<b> " + instituteQuery.InstituteName + "</b>" +
                            "</td>" +
                            "</tr> " +
                            "<tr>" +
                            "<td align = 'left' bgcolor = '#ffffff' style = ' padding: 10px 10px 10px 10px; color: #555555; font-family: Arial, sans-serif; font-size: 15px; border-bottom: 1px solid #f6f6f6;' >" +
                            "Faculty Name:<b> " + instituteQuery.FacultyName + "</b>" +
                            "</td>" +
                            "</tr> " +
                            "<tr>" +
                            "<td align = 'left' bgcolor = '#ffffff' style = ' padding: 10px 10px 10px 10px; color: #555555; font-family: Arial, sans-serif; font-size: 15px; border-bottom: 1px solid #f6f6f6;' >" +
                            "Query:<b> " + instituteQuery.Query + "</b>" +
                            "</td>" +
                            "</tr> " +
                            "<tr>" +
                            "<td align = 'left' bgcolor = '#ffffff' style = ' padding: 10px 10px 10px 10px; color: #555555; font-family: Arial, sans-serif; font-size: 15px; border-bottom: 1px solid #f6f6f6;' >" +
                            "QueryDescription:<b> " + instituteQuery.QueryDescription + "</b>" +
                            "</td>" +
                            "</tr> " +
                            "<tr>" +
                            "<td align = 'left' bgcolor = '#ffffff' style = ' padding: 10px 10px 10px 10px; color: #555555; font-family: Arial, sans-serif; font-size: 15px; border-bottom: 1px solid #f6f6f6;' >" +
                            "Upload Image:<b> " + instituteQuery.UploadImage + "</b>" +
                            "</td>" +
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
                        myMailCredential.UserName = "support.msuis-cc@msubaroda.ac.in";
                        myMailCredential.Password = "support@123";
                        //smtpClient.UseDefaultCredentials = true;
                        smtpClient.EnableSsl = true;
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtpClient.Timeout = 20000;
                        smtpClient.Credentials = myMailCredential;

                        // Send SMTP mail
                        string result = string.Empty;
                        try
                        {
                            smtpClient.Send(MailMessage);
                            return Return.returnHttp("200", instituteQuery, null);
                        }
                        catch (Exception e)
                        {
                            return Return.returnHttp("201", e.Message.ToString(), null);
                        }
                    }
                    return Return.returnHttp("200", strMessage.ToString(), null);
                }   
                    
                }
                catch (Exception e)
                    {
                    return Return.returnHttp("201", e.Message, null);
                }
            
        }
        #endregion

        #region UploadInstituteQueryDocument
        [HttpPost]
        public HttpResponseMessage UploadInstituteQueryDocument()
        {
            string token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation tokenOperation = new TokenOperation();
            string responseToken = tokenOperation.ValidateToken(token);
            if (responseToken == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            string renameFile = Request.Headers.GetValues("InstituteQueryImageDoc").FirstOrDefault();

            int iUploadedCnt = 0;
            // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
            string sPath = "";
            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/InstituteQueryImageDocument/");

            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            // CHECK THE FILE COUNT.
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];

                if (hpf.ContentLength > 0)
                {
                    if (File.Exists(sPath + Path.GetFileName(responseToken + "_" + renameFile)))
                    {
                        File.Delete(sPath + Path.GetFileName(responseToken + "_" + renameFile));
                    }
                    hpf.SaveAs(sPath + Path.GetFileName(responseToken + "_" + renameFile));
                    iUploadedCnt = iUploadedCnt + 1;
                }
            }
            if (iUploadedCnt > 0)
            {
                return Return.returnHttp("200", iUploadedCnt + " Files Uploaded Successfully", null);
            }
            else
            {
                return Return.returnHttp("201", iUploadedCnt + "Upload Failed", null);
            }

        }
        #endregion


        #region InstituteQueryGet
        [HttpPost]
        public HttpResponseMessage InstituteQueryGet(InstituteQuery iq)
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
                SqlCommand cmd = new SqlCommand("InstituteQueryMasterGet", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", iq.FacultyId);
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<InstituteQuery> ObjListInstQ = new List<InstituteQuery>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        InstituteQuery ObjIQ = new InstituteQuery();

                        ObjIQ.UserRegistrationId = Convert.ToInt64(dr["UserRegistrationId"].ToString());
                        ObjIQ.UserName = (Convert.ToString(dr["UserName"]));
                        ObjIQ.InstituteId = Convert.ToInt32(dr["InstituteId"].ToString());
                        ObjIQ.InstituteName = (Convert.ToString(dr["InstituteName"]));
                        ObjIQ.FacultyId = Convert.ToInt32(dr["FacultyId"].ToString());
                        ObjIQ.FacultyName = (Convert.ToString(dr["FacultyName"]));
                        ObjIQ.FacultyEmail = (Convert.ToString(dr["FacultyEmail"]));
                        ObjIQ.Query = (Convert.ToString(dr["Query"]));
                        ObjIQ.QueryDescription = (Convert.ToString(dr["QueryDescription"]));
                        //ObjIQ.IsActive = (Convert.ToBoolean(dr["IsActive"]));
                        ObjIQ.IsDeleted = (Convert.ToBoolean(dr["IsDeleted"]));
                        ObjIQ.UploadImage = (Convert.ToString(dr["QueryDescription"])); 

                        //string dirPhoto = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/InstituteQueryImageDocument/");
                        //string Photoinfo = Convert.ToString(dr["UploadImage"]);
                        //string Photopath = Path.GetFullPath(dirPhoto) + Photoinfo;
                        //byte[] PhotoArray = System.IO.File.ReadAllBytes(Photopath);
                        //string base64PhotoRepresentation = Convert.ToBase64String(PhotoArray);
                        //ObjIQ.UploadImage = base64PhotoRepresentation;
                        ObjListInstQ.Add(ObjIQ);
                    }
                }
                return Return.returnHttp("200", ObjListInstQ, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion



    }
}