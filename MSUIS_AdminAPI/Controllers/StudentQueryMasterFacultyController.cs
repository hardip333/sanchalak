using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
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
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class StudentQueryMasterFacultyController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region StudentQueryMaster List Get For Faculty
        [HttpGet]
        public HttpResponseMessage StudentQueryMasterGetByFaculty()
        {
            try
            {
                con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                Int32 Count = 0;
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                SqlCommand cmd = new SqlCommand("StudentQueryMasterGetByFaculty", con);
                cmd.CommandType = CommandType.StoredProcedure;

                da.SelectCommand = cmd;
                da.Fill(dt);

                List<StudentQueryMasterFaculty> ListStuQueryFac = new List<StudentQueryMasterFaculty>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        StudentQueryMasterFaculty objStuQueryFac = new StudentQueryMasterFaculty();
                        objStuQueryFac.Id = Convert.ToInt32(dr["Id"]);
                        objStuQueryFac.ApplicantRegistrationId = Convert.ToInt64(dr["ApplicantRegistrationId"]);
                        objStuQueryFac.FirstName = (Convert.ToString(dr["FirstName"])).IsEmpty() ? "-" : Convert.ToString(dr["FirstName"]);
                        objStuQueryFac.LastName = (Convert.ToString(dr["LastName"])).IsEmpty() ? "-" : Convert.ToString(dr["LastName"]);
                        objStuQueryFac.EmailId = (Convert.ToString(dr["EmailId"])).IsEmpty() ? "-" : Convert.ToString(dr["EmailId"]);
                        objStuQueryFac.MobileNo = (Convert.ToString(dr["MobileNo"])).IsEmpty() ? "-" : Convert.ToString(dr["MobileNo"]);
                        objStuQueryFac.DifficultyIn = (Convert.ToString(dr["DifficultyIn"])).IsEmpty() ? "-" : Convert.ToString(dr["DifficultyIn"]);
                        objStuQueryFac.DetailInBrief = (Convert.ToString(dr["DetailInBrief"])).IsEmpty() ? "-" : Convert.ToString(dr["DetailInBrief"]);
                        objStuQueryFac.FacultyRemarks = (Convert.ToString(dr["FacultyRemarks"])).IsEmpty() ? "-" : Convert.ToString(dr["FacultyRemarks"]);
                        objStuQueryFac.UserName = (Convert.ToString(dr["UserName"])).IsEmpty() ? 0 : Convert.ToInt64(dr["UserName"]);
                        objStuQueryFac.CreatedOn = (Convert.ToString(dr["CreatedOn"])).IsEmpty() ? "-" : string.Format("{0: dd-MM-yyyy}", (dr["CreatedOn"]));
                        objStuQueryFac.RemarkStatus = dr["RemarkStatus"].ToString();
                        Count = Count + 1;
                        objStuQueryFac.IndexId = Count;
                        


                        ListStuQueryFac.Add(objStuQueryFac);

                    }
                }

                return Return.returnHttp("200", ListStuQueryFac, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region StudentQueryMaster List Get By Id
        [HttpPost]
        public HttpResponseMessage StudentQueryMasterGetById(StudentQueryMasterFaculty stuQuery)
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
                
                int Id = Convert.ToInt32(stuQuery.Id);
                SqlCommand cmd = new SqlCommand("StudentQueryMasterGetById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);

                da.SelectCommand = cmd;
                da.Fill(dt);

                List<StudentQueryMasterFaculty> ListStuQueryFac = new List<StudentQueryMasterFaculty>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        StudentQueryMasterFaculty objStuQueryFac = new StudentQueryMasterFaculty();
                        objStuQueryFac.Id = Convert.ToInt32(dr["Id"]);
                        objStuQueryFac.FacultyRemarks = dr["FacultyRemarks"].ToString();
                        objStuQueryFac.FirstName = (Convert.ToString(dr["FirstName"])).IsEmpty() ? "-" : Convert.ToString(dr["FirstName"]);
                        objStuQueryFac.LastName = (Convert.ToString(dr["LastName"])).IsEmpty() ? "-" : Convert.ToString(dr["LastName"]);
                        objStuQueryFac.EmailId = (Convert.ToString(dr["EmailId"])).IsEmpty() ? "-" : Convert.ToString(dr["EmailId"]);
                        objStuQueryFac.MobileNo = (Convert.ToString(dr["MobileNo"])).IsEmpty() ? "-" : Convert.ToString(dr["MobileNo"]);
                        objStuQueryFac.DifficultyIn = (Convert.ToString(dr["DifficultyIn"])).IsEmpty() ? "-" : Convert.ToString(dr["DifficultyIn"]);
                        objStuQueryFac.DetailInBrief = (Convert.ToString(dr["DetailInBrief"])).IsEmpty() ? "-" : Convert.ToString(dr["DetailInBrief"]);
                        //objStuQueryFac.FacultyRemarks = (Convert.ToString(dr["FacultyRemarks"])).IsEmpty() ? "-" : Convert.ToString(dr["FacultyRemarks"]);
                   
                        try
                        {

                            string serverUrl = "https://admission.msubaroda.ac.in/MSUISApi/Upload/";

                            var UploadImage = dr["UploadImage"];
                            if (UploadImage is DBNull)
                                objStuQueryFac.UploadImage = null;
                            else
                            {


                                string DobFilePath = serverUrl + "StudentQueryImageDocument/" + dr["UploadImage"].ToString();
                                objStuQueryFac.UploadImage = DobFilePath;

                            }

                        }
                        catch(Exception e) {
                        }


                        ListStuQueryFac.Add(objStuQueryFac);

                    }
                }

                return Return.returnHttp("200", ListStuQueryFac, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region StudentQueryMaster List Get FacultyRemarks By Id
        [HttpPost]
        public HttpResponseMessage StudentQueryMasterGetFacultyRemarksById(StudentQueryMasterFaculty stuQueryFacRemarks)
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
                int Id = Convert.ToInt32(stuQueryFacRemarks.Id);
                SqlCommand cmd = new SqlCommand("StudentQueryMasterGetFacultyRemarksById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);

                da.SelectCommand = cmd;
                da.Fill(dt);

                List<StudentQueryMasterFaculty> ListStuQueryFacRemarks = new List<StudentQueryMasterFaculty>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        StudentQueryMasterFaculty objStuQueryFacRemarks = new StudentQueryMasterFaculty();
                        objStuQueryFacRemarks.Id = Convert.ToInt32(dr["Id"]);
                        objStuQueryFacRemarks.ApplicantRegistrationId = Convert.ToInt64(dr["ApplicantRegistrationId"]);
                        objStuQueryFacRemarks.FirstName = (Convert.ToString(dr["FirstName"])).IsEmpty() ? "-" : Convert.ToString(dr["FirstName"]);
                        objStuQueryFacRemarks.LastName = (Convert.ToString(dr["LastName"])).IsEmpty() ? "-" : Convert.ToString(dr["LastName"]);
                        objStuQueryFacRemarks.EmailId = (Convert.ToString(dr["EmailId"])).IsEmpty() ? "-" : Convert.ToString(dr["EmailId"]);
                        objStuQueryFacRemarks.MobileNo = (Convert.ToString(dr["MobileNo"])).IsEmpty() ? "-" : Convert.ToString(dr["MobileNo"]);
                        objStuQueryFacRemarks.DifficultyIn = (Convert.ToString(dr["DifficultyIn"])).IsEmpty() ? "-" : Convert.ToString(dr["DifficultyIn"]);
                        objStuQueryFacRemarks.DetailInBrief = (Convert.ToString(dr["DetailInBrief"])).IsEmpty() ? "-" : Convert.ToString(dr["DetailInBrief"]);
                        objStuQueryFacRemarks.FacultyRemarks = (Convert.ToString(dr["FacultyRemarks"])).IsEmpty() ? "-" : Convert.ToString(dr["FacultyRemarks"]);
                       

                        ListStuQueryFacRemarks.Add(objStuQueryFacRemarks);

                    }
                }

                return Return.returnHttp("200", ListStuQueryFacRemarks, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region StudentQueryMasterFacultyRemarks Add

        [HttpPost]
        public HttpResponseMessage StudentQueryMasterFacultyRemarksAdd(StudentQueryMasterFaculty studentFacRemarks)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            if (String.IsNullOrWhiteSpace(Convert.ToString(studentFacRemarks.FacultyRemarks)))
            {
                return Return.returnHttp("201", "Please enter Faculty Remarks", null);
            }
           
            else
            {
                try
                {

                    Int32 Id = Convert.ToInt32(studentFacRemarks.Id);
                    String FacultyRemarks = Convert.ToString(studentFacRemarks.FacultyRemarks);
                    Int64 UserId = Convert.ToInt64(res.ToString());
                    SqlCommand cmd = new SqlCommand("StudentQueryMasterFacultyRemarksAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                 

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@FacultyRemarks", FacultyRemarks);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);

                    con.Close();
                     if (strMessage == "TRUE")
                     {
                         SmtpClient smtpClient = new SmtpClient();
                         MailMessage MailMessage = new MailMessage();
                         //dynamic Jsondata = jObject;

                         //string ToEmail = Convert.ToString(studentFacRemarks.EmailId);
                         //string ToEmail = "jerlin.steffi@gmail.com";
                        string ToEmail = Convert.ToString(studentFacRemarks.EmailId);

                        MailAddress fromAddress = new MailAddress("noreplymsuis@msubaroda.ac.in", "MSUIS - Faculty Remarks Regarding Student Query");

                         smtpClient.Host = "smtp.gmail.com";

                         //Default port will be 25
                         smtpClient.Port = 587;

                         //From address will be given as a MailAddress Object
                         MailMessage.From = fromAddress;
                         MailMessage.To.Add(ToEmail);

                         // To address collection of MailAddress
                         MailMessage.Bcc.Add("noreplymsuis@msubaroda.ac.in");
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
                 "Dear <b> " + studentFacRemarks.FirstName + " " + studentFacRemarks.LastName + "</b>," +
                 "<br/><br/><div style='color: green;'>Your Query has been accepted...</div>" +
                 "<br/>Your Faculty Remarks are as below :" +

                 "<br/>FacultyRemarks : <b>" + studentFacRemarks.FacultyRemarks + "</b>" +
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
                         myMailCredential.UserName = "noreplymsuis@msubaroda.ac.in";
                         myMailCredential.Password = "msu@#$123";
                         //smtpClient.UseDefaultCredentials = true;
                         smtpClient.UseDefaultCredentials = false;
                         smtpClient.EnableSsl = true;
                         smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                         smtpClient.Timeout = 20000;
                         smtpClient.Credentials = myMailCredential;

                         // Send SMTP mail
                         string result = string.Empty;
                         try
                         {
                             smtpClient.Send(MailMessage);
                             //strMessage = "Your data has been Updated successfully.";
                         }
                         catch (Exception e)
                         {
                             return Return.returnHttp("201", e.Message.ToString(), null);
                         }
                     }

                    strMessage = "Your data has been Updated successfully.";
                    return Return.returnHttp("200", strMessage.ToString(), null);
                 
                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message, null);
                }
            }
        }
        #endregion

    }
}
