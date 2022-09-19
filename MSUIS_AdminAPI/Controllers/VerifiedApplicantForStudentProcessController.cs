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
using System.Text;
using System.Web.Http;
using MSUISApi.BAL;
using System.Threading.Tasks;
using System.Configuration;

namespace MSUISApi.Controllers
{
    public class VerifiedApplicantForStudentProcessController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();
        BALCommon common = new BALCommon();
        DataTable DtBkp = new DataTable();

        //string MailList;
        string[] MailList = new string[] { };
        string ToBcc;
        Int32 DtBkpCount = 0;
        #region VerifiedApplicantList

        [HttpPost]
        public HttpResponseMessage VerifiedApplicantList(VerifiedApplicantForStudentProcess objVerifiedApplicantForStudentProcess)
        {
            //VerifiedApplicantForStudentProcess modelobj = new VerifiedApplicantForStudentProcess();

            try
            {
                // modelobj.ProgrammeInstancePartTermId = objVerifiedApplicantForStudentProcess.ProgrammeInstancePartTermId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("VerifiedApplicantListByIncPartTerm", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", objVerifiedApplicantForStudentProcess.ProgrammeInstancePartTermId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<VerifiedApplicantForStudentProcess> objLstVerifiedApplicantForStudentProcess = new List<VerifiedApplicantForStudentProcess>();
                objVerifiedApplicantForStudentProcess.SerializeNumber = 0;
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        objVerifiedApplicantForStudentProcess.SerializeNumber = objVerifiedApplicantForStudentProcess.SerializeNumber + 1;
                        VerifiedApplicantForStudentProcess ObjVAFS = new VerifiedApplicantForStudentProcess();
                        ObjVAFS.SerializeNumber = objVerifiedApplicantForStudentProcess.SerializeNumber;
                        ObjVAFS.ApplicationId = Convert.ToInt64(dr["ApplicationId"]);
                        ObjVAFS.UserName = Convert.ToInt64(dr["UserName"]);
                        ObjVAFS.LastName = dr["LastName"].ToString();
                        ObjVAFS.FirstName = dr["FirstName"].ToString();

                        ObjVAFS.MiddleName = dr["MiddleName"].ToString();
                        ObjVAFS.IsApprovedByFaculty = Convert.ToBoolean(dr["IsApprovedByFaculty"]);
                        ObjVAFS.IsApprovedByAcademic = Convert.ToBoolean(dr["IsApprovedByAcademic"]);
                        if (Convert.ToBoolean(dr["IsCancelledByStudent"] is DBNull))
                        {
                            ObjVAFS.IsCancelledByStudent = Convert.ToBoolean(null);
                        }
                        else
                        {
                            ObjVAFS.IsCancelledByStudent = Convert.ToBoolean(dr["IsCancelledByStudent"]);
                        }

                        if (Convert.ToBoolean(dr["IsCancelledByFaculty"] is DBNull))
                        {
                            ObjVAFS.IsCancelledByFaculty = Convert.ToBoolean(null);
                        }
                        else
                        {
                            ObjVAFS.IsCancelledByFaculty = Convert.ToBoolean(dr["IsCancelledByFaculty"]);
                        }

                        if (Convert.ToBoolean(dr["IsCancelledByAcademics"] is DBNull))
                        {
                            ObjVAFS.IsCancelledByAcademics = Convert.ToBoolean(null);
                        }
                        else
                        {
                            ObjVAFS.IsCancelledByAcademics = Convert.ToBoolean(dr["IsCancelledByAcademics"]);
                        }

                        ObjVAFS.ProgrammeInstancePartTermId = Convert.ToInt64(dr["ProgrammeInstancePartTermId"]);
                        ObjVAFS.InstancePartTermName = dr["InstancePartTermName"].ToString();
                        ObjVAFS.EligibilityByAcademics = dr["EligibilityByAcademics"].ToString();
                        if (dr["AdminRemarkByAcademics"].ToString() is DBNull || dr["AdminRemarkByAcademics"].ToString().Trim().Equals(""))
                        {
                            ObjVAFS.AdminRemarkByAcademics = Convert.ToString('-');
                        }
                        else
                        {
                            ObjVAFS.AdminRemarkByAcademics = dr["AdminRemarkByAcademics"].ToString();
                        }

                        if (Convert.ToBoolean(dr["IsAdmitted"] is DBNull))
                        {
                            ObjVAFS.IsAdmitted = Convert.ToBoolean(null);
                        }
                        else
                        {
                            ObjVAFS.IsAdmitted = Convert.ToBoolean(dr["IsAdmitted"]);
                        }

                        objLstVerifiedApplicantForStudentProcess.Add(ObjVAFS);
                    }
                }
                return Return.returnHttp("200", objLstVerifiedApplicantForStudentProcess, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region TransferredStudentGetByProgrammeInstancePartTermId

        [HttpPost]
        public HttpResponseMessage TransferredStudentGetByProgrammeInstancePartTermId(TransferredStudentGetByProgrammeInstancePartTermId objTransferredStudentGetByProgrammeInstancePartTermId)
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

                SqlCommand cmd = new SqlCommand("TransferredStudentGetByProgrammeInstancePartTermId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", objTransferredStudentGetByProgrammeInstancePartTermId.ProgrammeInstancePartTermId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<TransferredStudentGetByProgrammeInstancePartTermId> objLstTransferredStudentGetByProgrammeInstancePartTermId = new List<TransferredStudentGetByProgrammeInstancePartTermId>();
                objTransferredStudentGetByProgrammeInstancePartTermId.SerializeNumber = 0;
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        objTransferredStudentGetByProgrammeInstancePartTermId.SerializeNumber = objTransferredStudentGetByProgrammeInstancePartTermId.SerializeNumber + 1;
                        TransferredStudentGetByProgrammeInstancePartTermId ObjTSG = new TransferredStudentGetByProgrammeInstancePartTermId();
                        ObjTSG.SerializeNumber = objTransferredStudentGetByProgrammeInstancePartTermId.SerializeNumber;
                        ObjTSG.PRN = Convert.ToInt64(dr["PRN"]);
                        ObjTSG.FullName = dr["FullName"].ToString();
                        ObjTSG.EmailId = dr["EmailId"].ToString();
                        ObjTSG.MobileNo = dr["MobileNo"].ToString();
                        var IsEmailSend = dr["IsEmailSend"];
                        if (IsEmailSend is DBNull)
                        {
                            ObjTSG.IsEmailSend = false;
                        }
                        else
                        {
                            ObjTSG.IsEmailSend = Convert.ToBoolean(dr["IsEmailSend"]);
                        }
                        var IsSMSSend = dr["IsSMSSend"];
                        if (IsSMSSend is DBNull)
                        {
                            ObjTSG.IsSMSSend = false;
                        }
                        else
                        {
                            ObjTSG.IsSMSSend = Convert.ToBoolean(dr["IsSMSSend"]);
                        }

                        ObjTSG.EligibilityByAcademics = dr["EligibilityByAcademics"].ToString();
                        if (dr["AdminRemarkByAcademics"].ToString() is DBNull || dr["AdminRemarkByAcademics"].ToString().Trim().Equals(""))
                        {
                            ObjTSG.AdminRemarkByAcademics = Convert.ToString('-');
                        }
                        else
                        {
                            ObjTSG.AdminRemarkByAcademics = dr["AdminRemarkByAcademics"].ToString();
                        }

                        objLstTransferredStudentGetByProgrammeInstancePartTermId.Add(ObjTSG);
                    }
                }
                return Return.returnHttp("200", objLstTransferredStudentGetByProgrammeInstancePartTermId, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region ProceedToMakeSingleStudent

        [HttpPost]
        public HttpResponseMessage ProceedToMakeSingleStudent(VerifiedApplicantForStudentProcess objVerifiedApplicantForStudentProcess)
        {

            try
            {
                Int64 UserName = objVerifiedApplicantForStudentProcess.UserName;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else
                {
                    SqlCommand Cmd = new SqlCommand("ProceedToMakeSingleStudent", con);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("@UserName", UserName);
                    Cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", objVerifiedApplicantForStudentProcess.ProgrammeInstancePartTermId);
                    Cmd.Parameters.AddWithValue("@CreatedBy", res);

                    //Cmd.Connection = con;
                    Cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    Cmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                    //Cmd.Parameters.Add("@FullName", SqlDbType.NVarChar, 500);
                    //Cmd.Parameters["@FullName"].Direction = ParameterDirection.Output;
                    //Cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 500);
                    //Cmd.Parameters["@Password"].Direction = ParameterDirection.Output;
                    con.Open();
                    Cmd.ExecuteNonQuery();
                    string StrMessage = Convert.ToString(Cmd.Parameters["@Message"].Value);
                    //string FullName = Convert.ToString(Cmd.Parameters["@FullName"].Value);
                    //string Password = Convert.ToString(Cmd.Parameters["@Password"].Value);
                    con.Close();
                    //string decodedPassword = Encoding.UTF8.GetString(Convert.FromBase64String(Password));
                    if (StrMessage.Equals("TRUE"))
                    {

                        return Return.returnHttp("200", StrMessage, null);
                    }
                    else
                    {
                        return Return.returnHttp("201", StrMessage, null);
                    }
                }

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region ProceedToMakeAllStudent

        [HttpPost]
        public HttpResponseMessage ProceedToMakeAllStudent(VerifiedApplicantForStudentProcess objVerifiedApplicantForStudentProcess)
        {

            try
            {
                Int64 ProgrammeInstancePartTermId = objVerifiedApplicantForStudentProcess.ProgrammeInstancePartTermId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else
                {
                    SqlCommand Cmd = new SqlCommand("ProceedToMakeAllStudent", con);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                    Cmd.Parameters.AddWithValue("@CreatedBy", Convert.ToInt64(res));

                    //Cmd.Connection = con;
                    Cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    Cmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                    // Cmd.Parameters.Add("@MessageJSON", SqlDbType.NVarChar, 400000);
                    //Cmd.Parameters["@MessageJSON"].Direction = ParameterDirection.Output;
                    con.Open();
                    Cmd.ExecuteNonQuery();
                    string StrMessage = Convert.ToString(Cmd.Parameters["@Message"].Value);
                    //string MessageJSON = Convert.ToString(Cmd.Parameters["@MessageJSON"].Value);
                    con.Close();
                    if (StrMessage.Equals("TRUE"))
                    {
                        return Return.returnHttp("200", "All student transfer successfully"/*MessageJSON*/, null);
                    }
                    else
                    {
                        return Return.returnHttp("201", StrMessage, null);
                    }
                }

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region SendSMSToSingleTransferredStudent
        [HttpPost]
        public HttpResponseMessage SingleTransferredStudentForSMSSend(TransferredStudentForSMSSend objSingleTransferredStudentForSMSSend)
        {

            try
            {
                string PRN = objSingleTransferredStudentForSMSSend.PRN;
                string MobileNo = objSingleTransferredStudentForSMSSend.MobileNo;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else
                {
                    SqlCommand Cmd = new SqlCommand("SingleTransferredStudentForSMSSend", con);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("@PRN", PRN);
                    Cmd.Parameters.AddWithValue("@MobileNo", MobileNo);

                    //Cmd.Connection = con;
                    Cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 50);
                    Cmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                    Cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 50);
                    Cmd.Parameters["@Password"].Direction = ParameterDirection.Output;
                    con.Open();
                    Cmd.ExecuteNonQuery();
                    string StrMessage = Convert.ToString(Cmd.Parameters["@Message"].Value);
                    string Password = Convert.ToString(Cmd.Parameters["@Password"].Value);
                    con.Close();
                    if (StrMessage.Equals("TRUE"))
                    {
                        string decodedPassword = Encoding.UTF8.GetString(Convert.FromBase64String(Password));
                        #region SMS for Username and Password
                        try
                        {
                            string Message = "Dear, Link for student portal: https://msuis.msubaroda.ac.in/Vidhyarthi | Username: " + PRN + " and Password: " + decodedPassword + "\nRegards,\nMSUB.";

                            string SMSResponse = common.SendSMS(MobileNo, Message, "1207161908607186569");
                            if (SMSResponse == "true")
                            {
                                Cmd = new SqlCommand("SingleTransferredStudentIsSMSSendFlag", con);
                                Cmd.CommandType = CommandType.StoredProcedure;
                                Cmd.Parameters.AddWithValue("@PRN", PRN);
                                Cmd.Parameters.AddWithValue("@MobileNo", MobileNo);

                                Cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 50);
                                Cmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                                con.Open();
                                Cmd.ExecuteNonQuery();
                                string IsSMSSend = Convert.ToString(Cmd.Parameters["@Message"].Value);
                                if (IsSMSSend.Equals("TRUE"))
                                {
                                    return Return.returnHttp("200", "SMS Sent successfully", null);
                                }
                                else
                                {
                                    return Return.returnHttp("201", "SMS failed - SMS sent - Flag pending", null);
                                }
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
                        #endregion

                    }
                    else
                    {
                        return Return.returnHttp("201", StrMessage, null);
                    }
                }

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region SendSMSToAllTransferredStudent

        [HttpPost]
        public HttpResponseMessage SendSMSToAllTransferredStudent(TransferredStudentForSMSSend objAllTransferredStudentForSMSSend)
        {
            //VerifiedApplicantForStudentProcess modelobj = new VerifiedApplicantForStudentProcess();

            try
            {
                // modelobj.ProgrammeInstancePartTermId = objVerifiedApplicantForStudentProcess.ProgrammeInstancePartTermId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else
                {

                    SqlCommand cmd = new SqlCommand("TransferredStudentGetByProgrammeInstancePartTermIdForBulkSMS", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", objAllTransferredStudentForSMSSend.ProgrammeInstancePartTermId);
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                    List<TransferredStudentForSMSSend> objLstAllTransferredStudentForSMSSend = new List<TransferredStudentForSMSSend>();

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count;)
                        {

                            string PRN = Convert.ToString(dt.Rows[i]["PRN"]);
                            string FullName = Convert.ToString(dt.Rows[i]["FullName"]);
                            string MobileNo = Convert.ToString(dt.Rows[i]["MobileNo"]);
                            string Password = Convert.ToString(dt.Rows[i]["Password"]);
                            string decodedPassword = Encoding.UTF8.GetString(Convert.FromBase64String(Password));

                            string Message = "Dear, Link for student portal: https://msuis.msubaroda.ac.in/Vidhyarthi | Username: " + PRN + " and Password: " + decodedPassword + "\nRegards,\nMSUB.";

                            string SMSResponse = common.SendSMS(MobileNo, Message, "1207161908607186569");
                            if (SMSResponse == "true")
                            {
                                SqlCommand Cmd = new SqlCommand("SingleTransferredStudentIsSMSSendFlag", con);
                                Cmd.CommandType = CommandType.StoredProcedure;
                                Cmd.Parameters.AddWithValue("@PRN", PRN);
                                Cmd.Parameters.AddWithValue("@MobileNo", MobileNo);

                                Cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 50);
                                Cmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                                con.Open();
                                Cmd.ExecuteNonQuery();
                                con.Close();
                                string IsSMSSend = Convert.ToString(Cmd.Parameters["@Message"].Value);
                                if (IsSMSSend.Equals("TRUE"))
                                {
                                    dt.Rows[i].Delete();
                                    i++;
                                }
                                else
                                {
                                    return Return.returnHttp("201", "SMS failed - SMS sent - Flag pending at " + PRN, null);
                                }
                            }
                            else
                            {
                                return Return.returnHttp("201", "SMS failed - SMS not sent", null);
                            }

                        }
                        //return Return.returnHttp("200", "SMS Sent successfully", null);
                    }
                    return Return.returnHttp("200", "SMS send successfully to all."/*objLstAllTransferredStudentForEmailSend*/, null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", "Problem in sending SMS Server", null);
            }
        }
        #endregion

        #region SendEmailToSingleTransferredStudent
        [HttpPost]
        public HttpResponseMessage SendEmailToSingleTransferredStudent(TransferredStudentForEmailSend objSingleTransferredStudentForEmailSend)
        {

            try
            {

                string PRN = objSingleTransferredStudentForEmailSend.PRN;
                string FullName = objSingleTransferredStudentForEmailSend.FullName;
                string EmailId = objSingleTransferredStudentForEmailSend.EmailId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else
                {
                    SqlCommand Cmd = new SqlCommand("SingleTransferredStudentForEmailSend", con);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("@PRN", PRN);
                    Cmd.Parameters.AddWithValue("@EmailId", EmailId);

                    //Cmd.Connection = con;
                    Cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 50);
                    Cmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                    Cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 50);
                    Cmd.Parameters["@Password"].Direction = ParameterDirection.Output;
                    con.Open();
                    Cmd.ExecuteNonQuery();
                    string StrMessage = Convert.ToString(Cmd.Parameters["@Message"].Value);
                    string Password = Convert.ToString(Cmd.Parameters["@Password"].Value);
                    con.Close();
                    if (StrMessage.Equals("TRUE"))
                    {
                        string decodedPassword = Encoding.UTF8.GetString(Convert.FromBase64String(Password));
                        #region Email for Username and Password
                        try
                        {

                            string ToEmail = EmailId;
                            string FromEmail = "noreplymsuis@msubaroda.ac.in";
                            string CcEmail = "noreplymsuis@msubaroda.ac.in";
                            string BccEmail = "noreplymsuis@msubaroda.ac.in";
                            string DisplayName = "MSUIS";
                            string Subject = "MSUIS - Student Portal Link With Credentials for Student Login";
                            string Body = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>" +
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
                             "</td>" +
                             "</tr>" +
                             "<tr>" +
                             "</tr>" +
                             "<tr>" +
                             "<td align = 'left' bgcolor = '#ffffff' style = 'padding: 40px 20px 40px 20px; color: #555555; font-family: Arial, sans-serif; font-size: 20px; line-height: 30px; border-bottom: 1px solid #f6f6f6;' >" +
                             "Dear <b> " + FullName + "</b>," +
                             "<br/><br/><div style='color: green;'>You are now student at THE MAHARAJA SAYAJIRAO UNIVERSITY OF BARODA.</div>" +
                             "<br/>Your login credentials and link for student portal are as below :<br/>" +
                             "<br/>Link for student portal : http://msuis.msubaroda.ac.in/vidhyarthi/" +
                             "<br/>PRN / Username : <b>" + PRN + "</b>" +
                             "<br/>Password : <b>" + decodedPassword + "</b>" +
                             "<br/><br/>Thank You" +
                             "<br/>Regards MSUB<br/>" +
                             "<br/><b style='color: red;'>NOTE : </b><small style='color: red;'>This information regarding THE MAHARAJA SAYAJIRAO UNIVERSITY OF BARODA is very sensitive." +
                             " Please do not share your credentials to anyone that send from our official account by Email or SMS.</small>" +
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
                            string Attachment = "";//"https://drive.google.com/file/d/1FYp-0DrAEWrvs2JFdsHzhJeGZlEdMh1Z/view?usp=sharing";
                            string EmailResponse = common.SendEmail(ToEmail, FromEmail, DisplayName, Subject, Body, Attachment, CcEmail, BccEmail);
                            if (EmailResponse == "true")
                            {
                                Cmd = new SqlCommand("SingleTransferredStudentIsEmailSendFlag", con);
                                Cmd.CommandType = CommandType.StoredProcedure;
                                Cmd.Parameters.AddWithValue("@PRN", PRN);
                                Cmd.Parameters.AddWithValue("@EmailId", EmailId);
                                Cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 50);
                                Cmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                                con.Open();
                                Cmd.ExecuteNonQuery();
                                string IsEmailSend = Convert.ToString(Cmd.Parameters["@Message"].Value);
                                if (IsEmailSend.Equals("TRUE"))
                                {
                                    return Return.returnHttp("200", "Mail successfully sent.", null);
                                }
                                else
                                {
                                    return Return.returnHttp("201", "Mail sent - Flag pending", null);
                                }

                            }
                            else
                            {

                                return Return.returnHttp("201", "Mail fail", null);

                            }

                        }
                        catch (Exception e)
                        {
                            return Return.returnHttp("201", e.Message.ToString(), null);
                        }
                        #endregion

                    }
                    else
                    {
                        return Return.returnHttp("201", StrMessage, null);
                    }
                }

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region SendEmailToAllTransferredStudent

        [HttpPost]
        public HttpResponseMessage SendEmailToAllTransferredStudent(TransferredStudentForEmailSend objAllTransferredStudentForEmailSend)
        {
            try
            {
                // modelobj.ProgrammeInstancePartTermId = objVerifiedApplicantForStudentProcess.ProgrammeInstancePartTermId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else
                {
                    /*SmtpClient smtpClient = new SmtpClient();
                    MailMessage MailMessage = new MailMessage();

                    MailAddress fromAddress = new MailAddress("noreplymsuis@msubaroda.ac.in", "MSUIS");

                    smtpClient.Host = "mail.mail-server.in";//"smtp.gmail.com";

                    //Default port will be 25
                    smtpClient.Port = 587;

                    //From address will be given as a MailAddress Object
                    MailMessage.From = fromAddress;


                    // To address collection of MailAddress
                    MailMessage.CC.Add("noreplymsuis@msubaroda.ac.in");
                    //MailMessage.Bcc.Add("noreplymsuis@msubaroda.ac.in");
                    MailMessage.Subject = "MSUIS - Student Portal Link With Credentials for Student Login";
                    MailMessage.IsBodyHtml = true;*/
                    /*System.Net.Mail.Attachment attachment;
                    
                    attachment = new System.Net.Mail.Attachment("https://drive.google.com/file/d/1FYp-0DrAEWrvs2JFdsHzhJeGZlEdMh1Z/view?usp=sharing");
                    MailMessage.Attachments.Add(attachment);*/
                    SqlConnectionStringBuilder sqlConnection = new SqlConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString)
                    {
                        AsynchronousProcessing = true
                    };
                    SqlCommand cmd = new SqlCommand("TransferredStudentGetByProgrammeInstancePartTermIdForBulkEmail", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", objAllTransferredStudentForEmailSend.ProgrammeInstancePartTermId);
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                    List<TransferredStudentForEmailSend> objLstAllTransferredStudentForEmailSend = new List<TransferredStudentForEmailSend>();
                    string InstancePartTermName = Convert.ToString(dt.Rows[0]["InstancePartTermName"]);
                    string Body = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>" +
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
                           "</td>" +
                           "</tr>" +
                           "<tr>" +
                           "</tr>" +
                           "<tr>" +
                           "<td align = 'left' bgcolor = '#ffffff' style = 'padding: 40px 20px 40px 20px; color: #555555; font-family: Arial, sans-serif; font-size: 20px; line-height: 30px; border-bottom: 1px solid #f6f6f6;' >" +
                           "Dear Student," +
                           "<br/>Greetings of the day,<br/>" +
                           "<br/><br/><div style='color: green;'>Welcome to THE MAHARAJA SAYAJIRAO UNIVERSITY OF BARODA.</div>" +
                           "<br/><br/>Your admission has been confirmed in " + InstancePartTermName + "." +
                           "<br/>Link for student portal : https://msuis.msubaroda.ac.in/vidhyarthi/" +
                           "<br/>User Credentials will be sent in SMS. You are requested to check your SMS and Email frequently for latest updates." +
                           "<br/>Username and Password are same as your application portal." +
                           "<br/><br/>In student portal kindly check your profile information. In <b>Student Profile</b> Tab." +
                           "<br/><br/>" +
                           /*"<br/>PRN / Username : <b>" + PRN + "</b>" +
                           "<br/>Password : <b>" + decodedPassword + "</b>" +*/
                           "<br/><br/>Thank You" +
                           "<br/>Regards MSUB<br/>" +
                           "</td>" +
                           "</tr> " +
                           "<tr>" +
                "<td bgcolor = '#ffffff'>" +
                "<b style='color: red;'>NOTE : </b><small style='color: red;'>Please do not reply to this Email. This information regarding <b>THE MAHARAJA SAYAJIRAO UNIVERSITY OF BARODA</b> is very sensitive." +
                "(Please do not disclose any of your personal or account related information (OTP / Credentials / PRN / Aadhaar number etc.) to anyone that send from our official account by Email or SMS. Stay cautious and alert from fraudulent activities / calls)" +
                "<br/>Disclaimer: This email and any files transmitted with it are confidential and intended solely for the use of the individual or entity to whom they are addressed. Disclosing, copying, distributing or taking any action in reliance on the contents of this information is strictly prohibited Your cooperation in this regard is solicited.</small>" +
                "</td>" +
                "</tr>" +
                "<tr>" +
                "<td align = 'center' bgcolor= '#dddddd' style= 'padding: 15px 10px 15px 10px; color: #555555; font-family: Arial, sans-serif; font-size: 12px; line-height: 18px;'>" +
                "<b> THE MAHARAJA SAYAJIRAO UNIVERSITY OF BARODA</b><br/>Pratapgunj, Vadodara, Gujarat 390002" +
                "</td>" +
                "</tr>" +
                           "</table>" +
                           "</body>" +
                           "</html>";
                    if (dt.Rows.Count > 0)
                    {
                        DtBkp.Rows.Clear();
                        DtBkp.Columns.Add("PRN");
                        DtBkp.Columns.Add("EmailId");
                        if (dt.Rows.Count < 500)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {

                                string PRN = Convert.ToString(dt.Rows[i]["PRN"]);
                                /*string FullName = Convert.ToString(dt.Rows[i]["FullName"]);
                                string Password = Convert.ToString(dt.Rows[i]["Password"]);
                                string decodedPassword = Encoding.UTF8.GetString(Convert.FromBase64String(Password)); */
                                string EmailId = Convert.ToString(dt.Rows[i]["EmailId"]);

                                MailList = new List<string>(MailList) { EmailId }.ToArray();
                                ToBcc = String.Join(",", MailList);
                                string a = dt.Rows[i].ToString();

                                DataRow dr = DtBkp.NewRow();

                                dr[0] = PRN;
                                dr[1] = EmailId;

                                DtBkp.Rows.Add(dr.ItemArray);
                                //DtBkpCount++;
                                dt.Rows[i].Delete();
                            }
                        }
                        else
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {

                                string PRN = Convert.ToString(dt.Rows[i]["PRN"]);
                                /*string FullName = Convert.ToString(dt.Rows[i]["FullName"]);

                                string Password = Convert.ToString(dt.Rows[i]["Password"]);
                                string decodedPassword = Encoding.UTF8.GetString(Convert.FromBase64String(Password)); */
                                string EmailId = Convert.ToString(dt.Rows[i]["EmailId"]);

                                MailList = new List<string>(MailList) { EmailId }.ToArray();
                                ToBcc = String.Join(",", MailList);
                                //string a = dt.Rows[i].ToString();
                                DataRow dr = DtBkp.NewRow();

                                dr[0] = PRN;
                                dr[1] = EmailId;
                                DtBkp.Rows.InsertAt(dr, i);
                                //DtBkpCount++;
                                dt.Rows[i].Delete();
                            }
                        }

                    }
                    string ResEmailPRN = common.SendPRNEmail(ToBcc, Body);
                    if (ResEmailPRN == "true" || ResEmailPRN== "Unable to send to a recipient.")
                    {
                        var SelectedValuesPRN = DtBkp.AsEnumerable().Select(s => s.Field<string>("PRN")).ToArray();
                        string PRNList = string.Join(",", SelectedValuesPRN);
                        var SelectedValuesEmailId = DtBkp.AsEnumerable().Select(s => s.Field<string>("EmailId")).ToArray();
                        string EmailIdList = string.Join(",", SelectedValuesEmailId);

                        SqlCommand Cmd = new SqlCommand("BulkTransferredStudentIsEmailSendFlag", con);
                        Cmd.CommandType = CommandType.StoredProcedure;
                        Cmd.Parameters.AddWithValue("@PRNList", PRNList);
                        Cmd.Parameters.AddWithValue("@EmailIdList", EmailIdList);

                        //Cmd.Connection = con;
                        Cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 50);
                        Cmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                        con.Open();
                        Cmd.ExecuteNonQuery();
                        string StrMessage = Convert.ToString(Cmd.Parameters["@Message"].Value);
                        con.Close();
                        if (StrMessage.Equals("TRUE"))
                        {
                            return Return.returnHttp("200", "Mail send successfully to all.", null);
                        }
                        else
                        {
                            return Return.returnHttp("200", "Mail send but flag pending.", null);
                        }


                    }
                    else
                    {
                        return Return.returnHttp("200", ResEmailPRN, null);
                    }

                    /*MailMessage.Bcc.Add(ToBcc);

                    NetworkCredential myMailCredential = new NetworkCredential();

                    myMailCredential.UserName = "prashant@mail-server.in";
                    myMailCredential.Password = "Programming5";
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = myMailCredential;

                    // Send SMTP mail
                    smtpClient.Send(MailMessage);*/


                }

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", "Problem in sending Email Server", null);
            }
        }
        #endregion
    }
}
