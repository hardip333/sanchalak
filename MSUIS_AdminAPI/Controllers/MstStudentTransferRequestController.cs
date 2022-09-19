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

namespace MSUISApi.Controllers
{
    public class MstStudentTransferRequestController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

        #region MstStudentTransferRequestGetByInstId
        [HttpPost]
        public HttpResponseMessage MstStudentTransferRequestGetByInstituteId(MstStudentTransferRequest objstudTransReq)
        {
            MstStudentTransferRequest modelobj = new MstStudentTransferRequest();

            try
            {
                modelobj.InstituteId = objstudTransReq.InstituteId;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("MstStudentTransferRequestGetByInstitute", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InstituteId", modelobj.InstituteId);

                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<MstStudentTransferRequest> ObjLstStuTransfer = new List<MstStudentTransferRequest>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstStudentTransferRequest ObjStuTransfer = new MstStudentTransferRequest();
                        ObjStuTransfer.Id = Convert.ToInt64(dr["Id"].ToString());
                        ObjStuTransfer.StudentPRN = Convert.ToInt64(dr["StudentPRN"].ToString());
                        ObjStuTransfer.InstancePartTermName = (dr["InstancePartTermName"].ToString());
                        ObjStuTransfer.SourceInstituteName = (dr["SourceInstituteName"].ToString());
                        ObjStuTransfer.DestinationInstituteName = (dr["DestinationInstituteName"].ToString());
                        // ObjStuTransfer.SourceRequestProcessedBy = Convert.ToInt64(dr["SourceRequestProcessedBy"].ToString());

                        var SourceInstituteStatus = dr["SourceInstituteStatus"];
                        if (SourceInstituteStatus is DBNull)
                        {
                            SourceInstituteStatus = "";
                            ObjStuTransfer.SourceInstituteStatus = (dr["SourceInstituteStatus"].ToString());
                        }
                        else
                        {
                            ObjStuTransfer.SourceInstituteStatus = (dr["SourceInstituteStatus"].ToString());
                        }

                        ObjLstStuTransfer.Add(ObjStuTransfer);
                    }

                }
                return Return.returnHttp("200", ObjLstStuTransfer, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region MstStudentTransferRequestGetFullDataByInstId
        [HttpPost]
        public HttpResponseMessage MstStudentTransferRequestGetFullDataByInstituteId(MstStudentTransferRequest objstudTransReq)
        {
            MstStudentTransferRequest modelobj = new MstStudentTransferRequest();

            try
            {
                modelobj.InstituteId = objstudTransReq.InstituteId;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("MstStudentTransferRequestGetFullDataByInstitute", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InstituteId", modelobj.InstituteId);

                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<MstStudentTransferRequest> ObjLstStuTransfer = new List<MstStudentTransferRequest>();
                Int32 count = 0;
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstStudentTransferRequest ObjStuTransfer = new MstStudentTransferRequest();
                        ObjStuTransfer.Id = Convert.ToInt64(dr["Id"].ToString());
                        ObjStuTransfer.StudentPRN = Convert.ToInt64(dr["StudentPRN"].ToString());
                        ObjStuTransfer.InstancePartTermName = (dr["InstancePartTermName"].ToString());
                        ObjStuTransfer.SourceInstituteName = (dr["SourceInstituteName"].ToString());
                        ObjStuTransfer.DestinationInstituteName = (dr["DestinationInstituteName"].ToString());
                        ObjStuTransfer.EligibleDegreeName = (dr["EligibleDegreeName"].ToString());
                        ObjStuTransfer.BranchName = (dr["BranchName"].ToString());
                        ObjStuTransfer.ExaminationBodyName = (dr["ExaminationBodyName"].ToString());
                        ObjStuTransfer.InstituteAttended = (dr["InstituteAttended"].ToString());
                        ObjStuTransfer.CityName = (dr["CityName"].ToString());
                        ObjStuTransfer.ExamPassMonth = (dr["ExamPassMonth"].ToString());
                        ObjStuTransfer.ExamPassYear = (dr["ExamPassYear"].ToString());
                        ObjStuTransfer.ExamCertificateNumber = (dr["ExamCertificateNumber"].ToString());
                        ObjStuTransfer.MarkObtained = (dr["MarkObtained"].ToString());
                        ObjStuTransfer.MarkOutof = (dr["MarkOutof"].ToString());
                        ObjStuTransfer.Grade = (dr["Grade"].ToString());
                        ObjStuTransfer.CGPA = (dr["CGPA"].ToString());
                        ObjStuTransfer.PercentageEquivalenceCGPA = (dr["PercentageEquivalenceCGPA"].ToString());
                        ObjStuTransfer.Percentage = (dr["Percentage"].ToString());
                        ObjStuTransfer.ClassName = (dr["ClassName"].ToString());
                        ObjStuTransfer.IsFirstTrial = (dr["IsFirstTrial"].ToString());
                        ObjStuTransfer.IsLastQualifyingExam = (dr["IsLastQualifyingExam"].ToString());
                        ObjStuTransfer.LanguageName = (dr["LanguageName"].ToString());
                        ObjStuTransfer.ResultStatus = (dr["ResultStatus"].ToString());
                        ObjStuTransfer.OtherCity = (dr["OtherCity"].ToString());
                        ObjStuTransfer.SourceInstituteStatus = (dr["SourceInstituteStatus"].ToString());
                        ObjStuTransfer.SourceInstituteRemark = (dr["SourceInstituteRemark"].ToString());
                        ObjStuTransfer.UserName = (dr["UserName"].ToString());
                        ObjStuTransfer.SourceRequestProcessedOnDate = (dr["SourceRequestProcessedOn"].ToString());

                        count = count + 1;
                        ObjStuTransfer.IndexId = count;

                        ObjLstStuTransfer.Add(ObjStuTransfer);
                    }

                }
                return Return.returnHttp("200", ObjLstStuTransfer, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region MstStudentTransferRequestGetByDestInstId
        [HttpPost]
        public HttpResponseMessage MstStudentTransferRequestGetByDestInstituteId(MstStudentTransferRequest objstudTransReq)
        {
            MstStudentTransferRequest modelobj = new MstStudentTransferRequest();

            try
            {
                modelobj.InstituteId = objstudTransReq.InstituteId;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("MstStudentTransferRequestGetByDestinationInstitute", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InstituteId", modelobj.InstituteId);

                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<MstStudentTransferRequest> ObjLstStuTransfer = new List<MstStudentTransferRequest>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstStudentTransferRequest ObjStuTransfer = new MstStudentTransferRequest();
                        ObjStuTransfer.Id = Convert.ToInt64(dr["Id"].ToString());
                        ObjStuTransfer.StudentPRN = Convert.ToInt64(dr["StudentPRN"].ToString());
                        ObjStuTransfer.ProgInstPartTermId = Convert.ToInt64(dr["ProgInstPartTermId"].ToString());
                        ObjStuTransfer.InstancePartTermName = (dr["InstancePartTermName"].ToString());
                        ObjStuTransfer.SourceInstituteName = (dr["SourceInstituteName"].ToString());
                        ObjStuTransfer.DestinationInstituteName = (dr["DestinationInstituteName"].ToString());

                        var DestinationInstituteStatus = dr["DestinationInstituteStatus"];
                        if (DestinationInstituteStatus is DBNull)
                        {
                            DestinationInstituteStatus = "";
                            ObjStuTransfer.DestinationInstituteStatus = (dr["DestinationInstituteStatus"].ToString());
                        }
                        else
                        {
                            ObjStuTransfer.DestinationInstituteStatus = (dr["DestinationInstituteStatus"].ToString());
                        }

                        ObjLstStuTransfer.Add(ObjStuTransfer);
                    }

                }
                return Return.returnHttp("200", ObjLstStuTransfer, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region MstStudentTransferRequestGetFullDataByDestInstId
        [HttpPost]
        public HttpResponseMessage MstStudentTransferRequestGetFullDataByDestInstId(MstStudentTransferRequest objstudTransReq)
        {
            MstStudentTransferRequest modelobj = new MstStudentTransferRequest();

            try
            {
                modelobj.InstituteId = objstudTransReq.InstituteId;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("MstStudentTransferRequestGetFullDataByDestInst", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InstituteId", modelobj.InstituteId);

                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<MstStudentTransferRequest> ObjLstStuTransfer = new List<MstStudentTransferRequest>();
                Int32 count = 0;
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstStudentTransferRequest ObjStuTransfer = new MstStudentTransferRequest();
                        ObjStuTransfer.Id = Convert.ToInt64(dr["Id"].ToString());
                        ObjStuTransfer.StudentPRN = Convert.ToInt64(dr["StudentPRN"].ToString());
                        ObjStuTransfer.InstancePartTermName = (dr["InstancePartTermName"].ToString());
                        ObjStuTransfer.SourceInstituteName = (dr["SourceInstituteName"].ToString());
                        ObjStuTransfer.DestinationInstituteName = (dr["DestinationInstituteName"].ToString());
                        ObjStuTransfer.EligibleDegreeName = (dr["EligibleDegreeName"].ToString());
                        ObjStuTransfer.BranchName = (dr["BranchName"].ToString());
                        ObjStuTransfer.ExaminationBodyName = (dr["ExaminationBodyName"].ToString());
                        ObjStuTransfer.InstituteAttended = (dr["InstituteAttended"].ToString());
                        ObjStuTransfer.CityName = (dr["CityName"].ToString());
                        ObjStuTransfer.ExamPassMonth = (dr["ExamPassMonth"].ToString());
                        ObjStuTransfer.ExamPassYear = (dr["ExamPassYear"].ToString());
                        ObjStuTransfer.ExamCertificateNumber = (dr["ExamCertificateNumber"].ToString());
                        ObjStuTransfer.MarkObtained = (dr["MarkObtained"].ToString());
                        ObjStuTransfer.MarkOutof = (dr["MarkOutof"].ToString());
                        ObjStuTransfer.Grade = (dr["Grade"].ToString());
                        ObjStuTransfer.CGPA = (dr["CGPA"].ToString());
                        ObjStuTransfer.PercentageEquivalenceCGPA = (dr["PercentageEquivalenceCGPA"].ToString());
                        ObjStuTransfer.Percentage = (dr["Percentage"].ToString());
                        ObjStuTransfer.ClassName = (dr["ClassName"].ToString());
                        ObjStuTransfer.IsFirstTrial = (dr["IsFirstTrial"].ToString());
                        ObjStuTransfer.IsLastQualifyingExam = (dr["IsLastQualifyingExam"].ToString());
                        ObjStuTransfer.LanguageName = (dr["LanguageName"].ToString());
                        ObjStuTransfer.ResultStatus = (dr["ResultStatus"].ToString());
                        ObjStuTransfer.OtherCity = (dr["OtherCity"].ToString());
                        ObjStuTransfer.DestinationInstituteStatus = (dr["DestinationInstituteStatus"].ToString());
                        ObjStuTransfer.DestinationInstituteRemark = (dr["DestinationInstituteRemark"].ToString());
                        ObjStuTransfer.UserName = (dr["UserName"].ToString());
                        ObjStuTransfer.DestinationRequestProcessedOnDate = (dr["DestinationRequestProcessedOn"].ToString());
                        ObjStuTransfer.OldFee = (dr["OldFee"].ToString());
                        ObjStuTransfer.NewFee = (dr["NewFee"].ToString());
                        var OldFeeTotalAmount = (dr["OldFeeTotalAmount"]);
                        if (OldFeeTotalAmount is DBNull) { }
                        else
                        {
                            ObjStuTransfer.OldFeeTotalAmount = Convert.ToDecimal(dr["OldFeeTotalAmount"]);
                        }
                        var NewFeeTotalAmount = (dr["NewFeeTotalAmount"]);
                        if (NewFeeTotalAmount is DBNull) { }
                        else
                        {
                            ObjStuTransfer.NewFeeTotalAmount = Convert.ToDecimal(dr["NewFeeTotalAmount"]);
                        }
                        count = count + 1;
                        ObjStuTransfer.IndexId = count;

                        ObjLstStuTransfer.Add(ObjStuTransfer);
                    }

                }
                return Return.returnHttp("200", ObjLstStuTransfer, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region MstStudentTransferRequestSourceInstituteEdit
        [HttpPost]
        public HttpResponseMessage MstStudentTransferRequestSourceInstituteEdit(MstStudentTransferRequest ObjStudentTransfer)
        {
            MstStudentTransferRequest modelobj = new MstStudentTransferRequest();

            try
            {
                modelobj.Id = Convert.ToInt32(ObjStudentTransfer.Id);
                modelobj.SourceInstituteStatus = ObjStudentTransfer.SourceInstituteStatus;
                if (modelobj.SourceInstituteStatus == "Forword")
                { modelobj.SourceInstituteRemark = "NULL"; }
                if (modelobj.SourceInstituteStatus == "Reject")
                { modelobj.SourceInstituteRemark = ObjStudentTransfer.SourceInstituteRemark; }

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                modelobj.SourceRequestProcessedBy = Convert.ToInt64(res);
                //modelobj.ModifiedBy = 5;
                //if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.EligibilityGroup)))
                //{
                //    return Return.returnHttp("201", "Eligibility is null or empty", null);
                //}
                //else if (modelobj.Id <= 0)
                //{
                //    return Return.returnHttp("201", "Please Enter Id or valid Id", null);
                //}
                //else if (modelobj.ProgrammeInstancePartTermId <= 0)
                //{
                //    return Return.returnHttp("201", "Please select programme Instance Part Term or valid Id", null);
                //}
                //else
                //{
                SqlCommand cmd = new SqlCommand("MstStudentTransferRequestSourceInstEdit", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", modelobj.Id);
                cmd.Parameters.AddWithValue("@SourceInstituteStatus", (modelobj.SourceInstituteStatus).Trim());
                cmd.Parameters.AddWithValue("@SourceRequestProcessedBy", modelobj.SourceRequestProcessedBy);
                cmd.Parameters.AddWithValue("@SourceInstituteRemark", (modelobj.SourceInstituteRemark).Trim());

                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();


                return Return.returnHttp("200", strMessage.ToString(), null);



                // }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region MstStudentTransferRequestDestinationInstituteEdit
        [HttpPost]
        public HttpResponseMessage MstStudentTransferRequestDestInstituteEdit(MstStudentTransferRequest ObjStudentTransfer)
        {
            MstStudentTransferRequest modelobj = new MstStudentTransferRequest();

            try
            {
                modelobj.Id = Convert.ToInt64(ObjStudentTransfer.Id);
                modelobj.StudentPRN = Convert.ToInt64(ObjStudentTransfer.StudentPRN);
                modelobj.InstituteId = Convert.ToInt32(ObjStudentTransfer.InstituteId);
                modelobj.DestinationInstituteStatus = ObjStudentTransfer.DestinationInstituteStatus;
                modelobj.IsFeeCategoryChanged = ObjStudentTransfer.IsFeeCategoryChanged;
                modelobj.InstancePartTermName = ObjStudentTransfer.InstancePartTermName;
                modelobj.SourceInstituteName = ObjStudentTransfer.SourceInstituteName;
                modelobj.DestinationInstituteName = ObjStudentTransfer.DestinationInstituteName;
                modelobj.MobileNo = ObjStudentTransfer.MobileNo;
                modelobj.EmailId = ObjStudentTransfer.EmailId;
                modelobj.LastName = ObjStudentTransfer.LastName;
                modelobj.FirstName = ObjStudentTransfer.FirstName;
                modelobj.MiddleName = ObjStudentTransfer.MiddleName;
                modelobj.ChangedFeeCategoryPartTermMapId = ObjStudentTransfer.ChangedFeeCategoryPartTermMapId;
                if (modelobj.DestinationInstituteStatus == "Approve")
                { modelobj.DestinationInstituteRemark = "NULL"; }
                if (modelobj.DestinationInstituteStatus == "Reject")
                { modelobj.DestinationInstituteRemark = ObjStudentTransfer.DestinationInstituteRemark; }

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                modelobj.DestinationRequestProcessedBy = Convert.ToInt64(res);

                if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.DestinationInstituteStatus)))
                {
                    return Return.returnHttp("201", "Destination Institute Status is null or empty", null);
                }
                else if (modelobj.Id <= 0)
                {
                    return Return.returnHttp("201", "Please Enter Id or valid Id", null);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("MstStudentTransferRequestDestinationInstEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", modelobj.Id);
                    cmd.Parameters.AddWithValue("@DestinationInstituteStatus", (modelobj.DestinationInstituteStatus).Trim());
                    cmd.Parameters.AddWithValue("@DestinationRequestProcessedBy", modelobj.DestinationRequestProcessedBy);
                    cmd.Parameters.AddWithValue("@DestinationInstituteRemark", (modelobj.DestinationInstituteRemark).Trim());
                    cmd.Parameters.AddWithValue("@IsFeeCategoryChanged", modelobj.IsFeeCategoryChanged);
                    cmd.Parameters.AddWithValue("@FeeCategoryPartTermMapId", modelobj.ChangedFeeCategoryPartTermMapId);
                    cmd.Parameters.AddWithValue("@PRN", modelobj.StudentPRN);
                    cmd.Parameters.AddWithValue("@InstituteId", modelobj.InstituteId);

                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    //con.Open();
                    //cmd.ExecuteNonQuery();
                    //string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    //con.Close();
                    SqlTransaction SqlTrns;

                    con.Open();
                    SqlTrns = con.BeginTransaction();
                    cmd.Transaction = SqlTrns;
                    string strMessage = null;
                    string Res_code = null;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        SqlTrns.Commit();
                        con.Close();
                        Res_code = "200";
                        strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    }
                    catch (SqlException e)
                    {
                        SqlTrns.Rollback();
                        con.Close();
                        Res_code = "201";
                        strMessage = "Something went Wrong";
                    }
                    // return Return.returnHttp(Res_code, strMessage.ToString(), null);

                    if (strMessage == "Candidate Transfer Request is Approved Yes")
                    {

                        #region SMS and Email for Candidate Transfer Request By Institute
                        try
                        {
                            //Your user name
                            string user = "shaurya";
                            ////Your authentication key
                            string key = "ec07fd3102XX";
                            ////Multiple mobiles numbers separated by comma
                            string mobile = "6353853083";
                            ////Sender ID,While using route4 sender id should be 6 characters long.
                            string senderid = "MSUBIS";
                            ////Your message to send, Add URL encoding here.
                            ////string message = System.Web.HttpUtility.UrlEncode("your OTP for MSU Wi-Fi Registration : " + OTP);
                            //string message = "Dear Applicant, Your Transfer Admission Request from " + modelobj.SourceInstituteName + " to " + modelobj.DestinationInstituteName + " in " + modelobj.InstancePartTermName + " is Processed Kindly pay the fee. Note: If you are not paying fee your admission is cancelled. \n- Team MSUB";
                            string message = "Dear Applicant, Your Transfer Admission Request from " + modelobj.SourceInstituteName + " and Password " + modelobj.DestinationInstituteName + ". Please do not share this information to anyone.\n- Team MSUB";
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


                            #region Email for Candidate Transfer Request By Institute
                            try
                            {
                                SmtpClient smtpClient = new SmtpClient();
                                MailMessage MailMessage = new MailMessage();
                                //dynamic Jsondata = jObject;

                                string ToEmail = Convert.ToString(modelobj.EmailId);

                                MailAddress fromAddress = new MailAddress("noreplymsuis@msubaroda.ac.in", "MSUIS - Student Admission Transfer Process");

                                smtpClient.Host = "smtp.gmail.com";

                                //Default port will be 25
                                smtpClient.Port = 587;

                                //From address will be given as a MailAddress Object
                                MailMessage.From = fromAddress;
                                MailMessage.To.Add(ToEmail);

                                // To address collection of MailAddress
                                MailMessage.Bcc.Add("noreplymsuis@msubaroda.ac.in");
                                MailMessage.Subject = "MSUIS - Student Admission Transfer Process";
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
                                "Dear <b> " + modelobj.FirstName + " " + modelobj.MiddleName + " " + modelobj.LastName + " <br/> PRN: " + modelobj.StudentPRN + "</b>," +
                                "<br/><br/><div style='color: green;'>Dear Applicant, Your Transfer Admission Request from <b>" + modelobj.SourceInstituteName + "</b> to <b>" + modelobj.DestinationInstituteName + "</b> in <b>" + modelobj.InstancePartTermName + "</b> is Processed Kindly pay the fee. Note: If you are not paying fee your admission is cancelled. </div>" +
                                "<br/><div style='color: green;'> " + "</div>" +
                                "<br/><br/>Thank You." +
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
                                myMailCredential.UserName = "noreplymsuis@msubaroda.ac.in";
                                myMailCredential.Password = "msu@#$123";
                                smtpClient.UseDefaultCredentials = false;
                                smtpClient.EnableSsl = true;
                                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                                smtpClient.Timeout = 20000;
                                smtpClient.Credentials = myMailCredential;

                                // Send SMTP mail
                                string result = string.Empty;
                                smtpClient.Send(MailMessage);

                                return Return.returnHttp("200", "Candidate Transfer Request Approved. SMS and EMail has been successfully sent.", null);
                            }
                            catch (Exception e)
                            {
                                return Return.returnHttp("201", e.Message.ToString(), null);
                            }
                            #endregion
                            // return Return.returnHttp("200", "SMS Sent successfully", null);
                        }
                        catch (Exception e)
                        {
                            return Return.returnHttp("201", e.Message.ToString(), null);
                        }
                        #endregion


                    }
                    else if (strMessage == "Candidate Transfer Request is Approved")
                    {
                        #region SMS and Email for Candidate Transfer Request By Institute
                        try
                        {
                            //Your user name
                            string user = "shaurya";
                            ////Your authentication key
                            string key = "ec07fd3102XX";
                            ////Multiple mobiles numbers separated by comma
                            // string mobile = modelobj.MobileNo.Trim();
                            string mobile = "6353853083";
                            ////Sender ID,While using route4 sender id should be 6 characters long.
                            string senderid = "MSUBIS";
                            ////Your message to send, Add URL encoding here.
                            ////string message = System.Web.HttpUtility.UrlEncode("your OTP for MSU Wi-Fi Registration : " + OTP);
                            string message = "Dear Applicant, Your Transfer Admission Request from <b>" + modelobj.SourceInstituteName + "</b> to <b>" + modelobj.DestinationInstituteName + "</b> in <b>" + modelobj.InstancePartTermName + "</b>  is Successful. \n- Team MSUB";
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

                            #region Email for Candidate Transfer Request By Institute
                            try
                            {
                                SmtpClient smtpClient = new SmtpClient();
                                MailMessage MailMessage = new MailMessage();
                                //dynamic Jsondata = jObject;

                                string ToEmail = Convert.ToString(modelobj.EmailId);

                                MailAddress fromAddress = new MailAddress("noreplymsuis@msubaroda.ac.in", "MSUIS - Student Admission Transfer Process");

                                smtpClient.Host = "smtp.gmail.com";

                                //Default port will be 25
                                smtpClient.Port = 587;

                                //From address will be given as a MailAddress Object
                                MailMessage.From = fromAddress;
                                MailMessage.To.Add(ToEmail);

                                // To address collection of MailAddress
                                MailMessage.Bcc.Add("noreplymsuis@msubaroda.ac.in");
                                MailMessage.Subject = "MSUIS - Student Admission Transfer Process";
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
                                "Dear <b> " + modelobj.FirstName + " " + modelobj.MiddleName + " " + modelobj.LastName + " <br/> PRN: " + modelobj.StudentPRN + "</b>," +
                                "<br/><br/><div style='color: green;'>Dear Applicant, Your Transfer Admission Request from <b>" + modelobj.SourceInstituteName + "</b> to <b>" + modelobj.DestinationInstituteName + "</b> in <b>" + modelobj.InstancePartTermName + "</b>  is Successful. </div>" +
                                "<br/><div style='color: green;'> " + "</div>" +
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
                                myMailCredential.UserName = "noreplymsuis@msubaroda.ac.in";
                                myMailCredential.Password = "msu@#$123";
                                smtpClient.UseDefaultCredentials = false;
                                smtpClient.EnableSsl = true;
                                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                                smtpClient.Timeout = 20000;
                                smtpClient.Credentials = myMailCredential;

                                // Send SMTP mail
                                string result = string.Empty;
                                smtpClient.Send(MailMessage);

                                return Return.returnHttp("200", "Candidate Transfer Request Approved No. SMS and EMail has been successfully sent.", null);
                            }
                            catch (Exception e)
                            {
                                return Return.returnHttp("201", e.Message.ToString(), null);
                            }
                            #endregion
                            // return Return.returnHttp("200", "SMS Sent successfully", null);
                        }
                        catch (Exception e)
                        {
                            return Return.returnHttp("201", e.Message.ToString(), null);
                        }
                        #endregion

                    }
                    else if (strMessage == "Candidate Transfer Request is Rejected")
                    {
                        return Return.returnHttp("200", strMessage, null);
                    }
                    else
                    {
                        return Return.returnHttp("201", strMessage, null);
                    }

                    //   return Return.returnHttp("200", strMessage.ToString(), null);



                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region StudentProfileGetByPRN
        [HttpPost]
        public HttpResponseMessage StudentProfileGetByPRN(StudentProfile objstudTransReq)
        {
            StudentProfile modelobj = new StudentProfile();
            try
            {
                modelobj.PRN = objstudTransReq.PRN;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("StudentTransferProfileView", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PRN", modelobj.PRN);

                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<StudentProfile> slist = new List<StudentProfile>();

                foreach (DataRow DR in dt.Rows)
                {
                    StudentProfile s = new StudentProfile();

                    s.PRN = Convert.ToInt64(DR["PRN"].ToString());
                    s.LastName = DR["LastName"].ToString();
                    s.FirstName = DR["FirstName"].ToString();
                    s.EmailId = DR["EmailId"].ToString();
                    s.MobileNo = DR["MobileNo"].ToString();
                    s.MiddleName = DR["MiddleName"].ToString();
                    s.Gender = DR["Gender"].ToString();
                    s.ReligionName = DR["ReligionName"].ToString();
                    s.MaritalStatus = DR["MaritalStatus"].ToString();
                    s.MotherTongueName = DR["MotherTongueName"].ToString();
                    if (!string.IsNullOrEmpty(DR["DOB"].ToString()))
                    {
                        s.DOB1 = Convert.ToDateTime(DR["DOB"]).ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"));
                    }
                    s.BloodGroupName = DR["BloodGroupName"].ToString();
                    s.HeightInCms = DR["HeightInCms"].ToString();
                    s.WeightInKgs = DR["WeightInKgs"].ToString();
                    s.IsMajorThelesamiaStatus = DR["IsMajorThelesamiaStatus"].ToString();
                    s.IsNRI = DR["IsNRI"].ToString();
                    s.CitizenCountry = DR["CitizenshipCountryName"].ToString();
                    s.PassportNumber = DR["PassportNumber"].ToString();
                    s.AadharNumber = DR["AadharNumber"].ToString();
                    s.NameOnAadhar = DR["NameOnAadhar"].ToString();
                    s.NameOfFather = DR["NameOfFather"].ToString();
                    s.NameOfMother = DR["NameOfMother"].ToString();
                    s.OptionalMobileNo = DR["OptionalMobileNo"].ToString();
                    s.IsSmsPermissionGiven = DR["IsSmsPermissionGiven"].ToString();
                    s.IsLocalToVadodara = DR["IsLocalToVadodara"].ToString();
                    s.DOBDoc = DR["DOBDoc"].ToString();
                    s.SpouseName = DR["SpouseName"].ToString();
                    s.NameAsPerMarksheet = DR["NameAsPerMarksheet"].ToString();
                    s.AadharDoc = DR["AadharDoc"].ToString();
                    s.IsEmp = DR["IsEmp"].ToString();
                    s.CurrentEmployerName = DR["CurrentEmployerName"].ToString();
                    s.PhotoIdDoc = DR["PhotoIdDoc"].ToString();
                    s.OtherReligion = DR["OtherReligion"].ToString();
                    s.PermanentAddress = DR["PermanentAddress"].ToString();
                    s.PermanentCountry = DR["PermanentCountryName"].ToString();
                    s.PermanentState = DR["PermanentStateName"].ToString();
                    s.PermanentDistrict = DR["PermanentDistrictName"].ToString();
                    s.PermanentCityVillage = DR["PermanentCityVillage"].ToString();
                    s.PermanentPincode = DR["PermanentPincode"].ToString();
                    s.IsCurrentAsPermanent = DR["IsCurrentAsPermanent"].ToString();
                    s.CurrentAddress = DR["CurrentAddress"].ToString();
                    s.CurrentCountry = DR["CurrentCountryName"].ToString();
                    s.CurrentState = DR["CurrentStateName"].ToString();
                    s.CurrentDistrict = DR["CurrentDistrictName"].ToString();
                    s.CurrentCityVillage = DR["CurrentCityVillage"].ToString();
                    s.CurrentPincode = DR["CurrentPincode"].ToString();
                    s.StudentPhoto = DR["StudentPhoto"].ToString();
                    s.StudentSignature = DR["StudentSignature"].ToString();
                    s.SocialCategoryName = DR["SocialCategoryName"].ToString();
                    s.ReservationCategory = DR["ApplicationReservationName"].ToString();
                    s.IsEWS = DR["IsEWS"].ToString();
                    s.IsPhysicallyChallenged = DR["IsPhysicallyChallenged"].ToString();
                    s.EWSDoc = DR["EWSDoc"].ToString();
                    s.PCDoc = DR["PCDoc"].ToString();
                    s.DisabilityPercentage = DR["DisabilityPercentage"].ToString();
                    s.DisabilityType = DR["DisabilityType"].ToString();
                    s.IsSocialDocSubmitted = DR["IsSocialDocSubmitted"].ToString();
                    s.IsEWSDocSubmitted = DR["IsEWSDocSubmitted"].ToString();
                    s.IsPCDocSubmitted = DR["IsPCDocSubmitted"].ToString();
                    s.IsReservationDocSubmitted = DR["IsReservationDocSubmitted"].ToString();
                    s.SocialCategoryDoc = DR["SocialCategoryDoc"].ToString();
                    s.ReservationCategoryDoc = DR["ReservationCategoryDoc"].ToString();
                    s.GuardianName = DR["GuardianName"].ToString();
                    s.GuardianContactNo = DR["GuardianContactNo"].ToString();
                    s.FamilyAnnualIncome = DR["FamilyAnnualIncome"].ToString();
                    s.GuardianAnnualIncome = DR["GuardianAnnualIncome"].ToString();
                    s.IsGuardianEbc = DR["IsGuardianEbc"].ToString();
                    s.FatherMotherContactNo = DR["FatherMotherContactNo"].ToString();
                    s.FatherOccupation = DR["FatherOccupation"].ToString();
                    s.MotherOccupation = DR["MotherOccupation"].ToString();
                    s.GuardianOccupation = DR["GuardianOccupation"].ToString();
                    s.GSocialCategory = DR["GSocialCategoryName"].ToString();

                    slist.Add(s);
                }
                return Return.returnHttp("200", slist, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region MstStudentTransferRequestGetEducationDetailsByPRN
        [HttpPost]
        public HttpResponseMessage MstStudentTransferRequestGetEducationDetailsByPRN(MstStudentTransferRequest objstudTransReq)
        {
            MstStudentTransferRequest modelobj = new MstStudentTransferRequest();

            try
            {
                modelobj.StudentPRN = objstudTransReq.StudentPRN;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("MstStudentTransferRequestGetEduDataByPRN", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PRN", modelobj.StudentPRN);

                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<MstStudentTransferRequest> ObjLstStuTransfer = new List<MstStudentTransferRequest>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstStudentTransferRequest ObjStuTransfer = new MstStudentTransferRequest();
                        ObjStuTransfer.Id = Convert.ToInt64(dr["Id"].ToString());
                        ObjStuTransfer.EligibleDegreeName = (dr["EligibleDegreeName"].ToString());
                        ObjStuTransfer.BranchName = (dr["BranchName"].ToString());
                        ObjStuTransfer.ExaminationBodyName = (dr["ExaminationBodyName"].ToString());
                        ObjStuTransfer.InstituteAttended = (dr["InstituteAttended"].ToString());
                        ObjStuTransfer.CityName = (dr["CityName"].ToString());
                        ObjStuTransfer.ExamPassMonth = (dr["ExamPassMonth"].ToString());
                        ObjStuTransfer.ExamPassYear = (dr["ExamPassYear"].ToString());
                        ObjStuTransfer.ExamCertificateNumber = (dr["ExamCertificateNumber"].ToString());
                        ObjStuTransfer.MarkObtained = (dr["MarkObtained"].ToString());
                        ObjStuTransfer.MarkOutof = (dr["MarkOutof"].ToString());
                        ObjStuTransfer.Grade = (dr["Grade"].ToString());
                        ObjStuTransfer.CGPA = (dr["CGPA"].ToString());
                        ObjStuTransfer.PercentageEquivalenceCGPA = (dr["PercentageEquivalenceCGPA"].ToString());
                        ObjStuTransfer.Percentage = (dr["Percentage"].ToString());
                        ObjStuTransfer.ClassName = (dr["ClassName"].ToString());
                        ObjStuTransfer.IsFirstTrial = (dr["IsFirstTrial"].ToString());
                        ObjStuTransfer.IsLastQualifyingExam = (dr["IsLastQualifyingExam"].ToString());
                        ObjStuTransfer.LanguageName = (dr["LanguageName"].ToString());
                        ObjStuTransfer.ResultStatus = (dr["ResultStatus"].ToString());
                        ObjStuTransfer.OtherCity = (dr["OtherCity"].ToString());
                        ObjStuTransfer.AttachDocument = (dr["AttachDocument"].ToString());

                        ObjLstStuTransfer.Add(ObjStuTransfer);
                    }

                }
                return Return.returnHttp("200", ObjLstStuTransfer, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region MstStudentTransferRequestFeeCatPartTermMapIdGet
        [HttpPost]
        public HttpResponseMessage MstStudentTransferReqFeeCatPartTermMapIdGet(MstStudentTransferRequest objstudTransReq)
        {
            MstStudentTransferRequest modelobj = new MstStudentTransferRequest();

            try
            {
                modelobj.ProgInstPartTermId = objstudTransReq.ProgInstPartTermId;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("MstStudentTransferRequestFeeCatPartTermMapIdGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", modelobj.ProgInstPartTermId);

                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<MstStudentTransferRequest> ObjLstStuTransfer = new List<MstStudentTransferRequest>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstStudentTransferRequest ObjStuTransfer = new MstStudentTransferRequest();
                        ObjStuTransfer.Id = Convert.ToInt64(dr["Id"].ToString());
                        ObjStuTransfer.ChangedFeeCategoryPartTermMapId = Convert.ToInt64(dr["FeeCategoryPartTermMapId"].ToString());
                        ObjStuTransfer.FeeCategoryName = (dr["FeeCategoryName"].ToString());


                        ObjLstStuTransfer.Add(ObjStuTransfer);
                    }

                }
                return Return.returnHttp("200", ObjLstStuTransfer, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion
    }
}
