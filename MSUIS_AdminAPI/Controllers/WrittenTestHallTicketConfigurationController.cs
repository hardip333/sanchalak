using MSUIS_TokenManager.App_Start;
using MSUISApi.BAL;
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
using System.Web.Helpers;
using System.Web.Http;
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class WrittenTestHallTicketConfigurationController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region FacultyGet
        [HttpPost]
       public HttpResponseMessage MstFacultyGet()
       {
           
            try
            {
               SqlCommand Cmd = new SqlCommand("MstFacultyGet", Con);
               Cmd.CommandType = CommandType.StoredProcedure;
               Da.SelectCommand = Cmd;

               Da.Fill(Dt);


               List<MstFaculty> ObjLstFaculty = new List<MstFaculty>();

               if (Dt.Rows.Count > 0)
               {
                   for (int i = 0; i < Dt.Rows.Count; i++)
                   {
                       MstFaculty objFaculty = new MstFaculty();

                       objFaculty.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                       objFaculty.FacultyName = Convert.ToString(Dt.Rows[i]["FacultyName"]);
                       objFaculty.FacultyCode = Convert.ToString(Dt.Rows[i]["FacultyCode"]);
                       ObjLstFaculty.Add(objFaculty);
                   }
               }
               return Return.returnHttp("200", ObjLstFaculty, null);
           }
           catch (Exception e)
           {
               return Return.returnHttp("201", e.Message, null);
           }
       }
        #endregion

        #region AcademicYearGet
        [HttpPost]
        public HttpResponseMessage AcademicYearGet()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("IncAcademicYearGet", Con);

                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<AcademicYear> ObjAYList = new List<AcademicYear>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AcademicYear ObjAY = new AcademicYear();
                        ObjAY.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjAY.AcademicYearCode = Convert.ToString(dt.Rows[i]["AcademicYearCode"]);
                        ObjAYList.Add(ObjAY);
                    }
                    return Return.returnHttp("200", ObjAYList, null);
                }
                else
                {
                    return Return.returnHttp("201", "No Record Found", null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region ProgrammeList Get By FacultyId And AcademicYearId
        [HttpPost]
        public HttpResponseMessage ProgrammeListGetByFacAcadId(WrittenTestHallTicketConfiguration ObjWTHTC)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            try
            {
                Int32 FacultyId = Convert.ToInt32(ObjWTHTC.FacultyId);
                Int32 AcademicYearId = Convert.ToInt32(ObjWTHTC.AcademicYearId);
                SqlCommand cmd = new SqlCommand("MstProgrammeGetByFacAcadId", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                DataTable dt = new DataTable();
                Da.SelectCommand = cmd;
                Da.Fill(dt);

                List<MstProgramme> ObjListMstProg = new List<MstProgramme>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstProgramme ObjMstProgramme = new MstProgramme();

                        ObjMstProgramme.Id = Convert.ToInt32(dr["Id"].ToString());
                        ObjMstProgramme.ProgrammeName = dr["ProgrammeName"].ToString();
                        ObjMstProgramme.ProgrammeCode = dr["ProgrammeCode"].ToString();

                        ObjListMstProg.Add(ObjMstProgramme);
                    }
                }
                return Return.returnHttp("200", ObjListMstProg, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region ProgrammeInstancePartTerm Get By FacAcadProgId
        [HttpPost]
        public HttpResponseMessage ProgrammeInstancePartTermGetByFAPId(WrittenTestHallTicketConfiguration ObjWTHTC)
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("ProgrammeInstPartTermGetByFPPBId", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@FacultyId", ObjWTHTC.FacultyId);
                Cmd.Parameters.AddWithValue("@ProgrammeId", ObjWTHTC.ProgrammeId);
                Cmd.Parameters.AddWithValue("@AcademicYearId", ObjWTHTC.AcademicYearId);
               
                Da.SelectCommand = Cmd;
                DataTable Dt = new DataTable();
                Da.Fill(Dt);

                List<ProgrammeInstancePartTerm> ObjLstProg = new List<ProgrammeInstancePartTerm>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePartTerm objProg = new ProgrammeInstancePartTerm();

                        objProg.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objProg.InstancePartTermName = (Convert.ToString(Dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                        ObjLstProg.Add(objProg);
                    }
                }
                return Return.returnHttp("200", ObjLstProg, null);
            }
            catch (Exception e)
            {
                return null;
            }

        }

        #endregion

        #region Student Venue List GetByPIPTId
        [HttpPost]
        public HttpResponseMessage StudentVenueListGetByPIPTId(WrittenTestHallTicketConfiguration ObjWTHTC)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            try
            {
                Int32 Count = 0;
                Int64 ProgramInstancePartTermId = Convert.ToInt64(ObjWTHTC.ProgrammeInstancePartTermId);
                string RadioALLFlag = Convert.ToString(ObjWTHTC.RadioALLFlag);
                string RadioVerifiedFlag = Convert.ToString(ObjWTHTC.RadioVerifiedFlag);

                SqlCommand cmd = new SqlCommand("GetStudentVenueDetailsByPIPTId", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgramInstancePartTermId", ProgramInstancePartTermId);
                cmd.Parameters.AddWithValue("@RadioALLFlag", RadioALLFlag);
                cmd.Parameters.AddWithValue("@RadioVerifiedFlag", RadioVerifiedFlag);
                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<WrittenTestHallTicketConfiguration> ObjListWTHTC = new List<WrittenTestHallTicketConfiguration>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        WrittenTestHallTicketConfiguration ObjWTHTConfig = new WrittenTestHallTicketConfiguration();
                        ObjWTHTConfig.ApplicantUserName = (Convert.ToString(Dt.Rows[i]["ApplicantUserName"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["ApplicantUserName"]);
                        ObjWTHTConfig.ApplicationId = (Convert.ToString(Dt.Rows[i]["ApplicationId"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["ApplicationId"]);
                        ObjWTHTConfig.EntranceTestSeatNo = (Convert.ToString(Dt.Rows[i]["EntranceTestSeatNo"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["EntranceTestSeatNo"]);
                        ObjWTHTConfig.InstancePartTermName = (Convert.ToString(Dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                        ObjWTHTConfig.AppId = (Convert.ToString(Dt.Rows[i]["ApplicationId"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["ApplicationId"]);
                        ObjWTHTConfig.NameAsPerMarksheet = (Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"]);
                        ObjWTHTConfig.MobileNo = (Convert.ToString(Dt.Rows[i]["MobileNo"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["MobileNo"]);
                        ObjWTHTConfig.EmailId = (Convert.ToString(Dt.Rows[i]["EmailId"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["EmailId"]);
                        ObjWTHTConfig.VenueName = (Convert.ToString(Dt.Rows[i]["VenueName"])).IsEmpty() ? null : Convert.ToString(Dt.Rows[i]["VenueName"]);
                        ObjWTHTConfig.Address = (Convert.ToString(Dt.Rows[i]["Address"])).IsEmpty() ? null: Convert.ToString(Dt.Rows[i]["Address"]);
                        ObjWTHTConfig.BlockName = (Convert.ToString(Dt.Rows[i]["BlockName"])).IsEmpty() ? null : Convert.ToString(Dt.Rows[i]["BlockName"]);
                        ObjWTHTConfig.StartTimeView = (Convert.ToString(Dt.Rows[i]["StartTime"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["StartTime"]);
                        ObjWTHTConfig.EndTimeView = (Convert.ToString(Dt.Rows[i]["EndTime"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["EndTime"]);
                        ObjWTHTConfig.ExamDateView = (Convert.ToString(Dt.Rows[i]["ExamDate"])).IsEmpty() ? "-" : string.Format("{0: dd-MM-yyyy}", (Dt.Rows[i]["ExamDate"]));
                        ObjWTHTConfig.IsEntranceTestSms = (Convert.ToString(Dt.Rows[i]["IsEntranceTestSms"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsEntranceTestSms"]);
                        ObjWTHTConfig.IsEntranceTestEmail = (Convert.ToString(Dt.Rows[i]["IsEntranceTestEmail"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsEntranceTestEmail"]);
                        Count = Count + 1;
                        ObjWTHTConfig.IndexId = Count;
                        ObjListWTHTC.Add(ObjWTHTConfig);
                    }
                    return Return.returnHttp("200", ObjListWTHTC, null);
                }
               

                 else
                {
                    return Return.returnHttp("201", "No Records Found", null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region Student Venue List GetByPIPTId After Import Excel
        [HttpPost]
        public HttpResponseMessage GetStudentVenueDetailsByPIPTIdAfterImportExcel(WrittenTestHallTicketConfiguration ObjWTHTC)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            try
            {
                Int32 Count = 0;
                Int64 ProgramInstancePartTermId = Convert.ToInt64(ObjWTHTC.ProgrammeInstancePartTermId);
                string RadioALLFlag = Convert.ToString(ObjWTHTC.RadioALLFlag);
                string RadioVerifiedFlag = Convert.ToString(ObjWTHTC.RadioVerifiedFlag);

                SqlCommand cmd = new SqlCommand("GetStudentVenueDetailsByPIPTIdAfterImportExcel", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgramInstancePartTermId", ProgramInstancePartTermId);
                cmd.Parameters.AddWithValue("@RadioALLFlag", RadioALLFlag);
                cmd.Parameters.AddWithValue("@RadioVerifiedFlag", RadioVerifiedFlag);
                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<WrittenTestHallTicketConfiguration> ObjListWTHTC = new List<WrittenTestHallTicketConfiguration>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        WrittenTestHallTicketConfiguration ObjWTHTConfig = new WrittenTestHallTicketConfiguration();
                        ObjWTHTConfig.ApplicantUserName = (Convert.ToString(Dt.Rows[i]["ApplicantUserName"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["ApplicantUserName"]);
                        ObjWTHTConfig.ApplicationId = (Convert.ToString(Dt.Rows[i]["ApplicationId"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["ApplicationId"]);
                        ObjWTHTConfig.EntranceTestSeatNo = (Convert.ToString(Dt.Rows[i]["EntranceTestSeatNo"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["EntranceTestSeatNo"]);
                        ObjWTHTConfig.InstancePartTermName = (Convert.ToString(Dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                        ObjWTHTConfig.AppId = (Convert.ToString(Dt.Rows[i]["ApplicationId"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["ApplicationId"]);
                        ObjWTHTConfig.NameAsPerMarksheet = (Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"]);
                        ObjWTHTConfig.MobileNo = (Convert.ToString(Dt.Rows[i]["MobileNo"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["MobileNo"]);
                        ObjWTHTConfig.EmailId = (Convert.ToString(Dt.Rows[i]["EmailId"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["EmailId"]);
                        ObjWTHTConfig.VenueName = (Convert.ToString(Dt.Rows[i]["VenueName"])).IsEmpty() ? null : Convert.ToString(Dt.Rows[i]["VenueName"]);
                        ObjWTHTConfig.Address = (Convert.ToString(Dt.Rows[i]["Address"])).IsEmpty() ? null : Convert.ToString(Dt.Rows[i]["Address"]);
                        ObjWTHTConfig.BlockName = (Convert.ToString(Dt.Rows[i]["BlockName"])).IsEmpty() ? null : Convert.ToString(Dt.Rows[i]["BlockName"]);
                        ObjWTHTConfig.StartTimeView = (Convert.ToString(Dt.Rows[i]["StartTime"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["StartTime"]);
                        ObjWTHTConfig.EndTimeView = (Convert.ToString(Dt.Rows[i]["EndTime"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["EndTime"]);
                        ObjWTHTConfig.ExamDateView = (Convert.ToString(Dt.Rows[i]["ExamDate"])).IsEmpty() ? "-" : string.Format("{0: dd-MM-yyyy}", (Dt.Rows[i]["ExamDate"]));
                        ObjWTHTConfig.IsEntranceTestSms = (Convert.ToString(Dt.Rows[i]["IsEntranceTestSms"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsEntranceTestSms"]);
                        ObjWTHTConfig.IsEntranceTestEmail = (Convert.ToString(Dt.Rows[i]["IsEntranceTestEmail"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsEntranceTestEmail"]);
                        Count = Count + 1;
                        ObjWTHTConfig.IndexId = Count;
                        ObjListWTHTC.Add(ObjWTHTConfig);
                    }
                    return Return.returnHttp("200", ObjListWTHTC, null);
                }


                else
                {
                    return Return.returnHttp("201", "No Records Found", null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion


        #region AdmApplicationConfigurationAttachVenueAdd
        [HttpPost]
        public HttpResponseMessage AttachVenueUpdate(WrittenTestHallTicketConfiguration WTHTC)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
           
            if (String.IsNullOrWhiteSpace(Convert.ToString(WTHTC.ExamDate)))
            {
                return Return.returnHttp("201", "Please Select Exam Date", null);
            }
            else
            {
                try
                {
                    WrittenTestHallTicketConfiguration objWTHTC = new WrittenTestHallTicketConfiguration();
                    objWTHTC.ProgrammeInstancePartTermId = Convert.ToInt64(WTHTC.ProgrammeInstancePartTermId);
                                   
                        DateTime dtAppExamDate = new DateTime(1949, 1, 1);
                        bool ChkDt = DateTime.TryParse(Convert.ToString(WTHTC.ExamDate), out dtAppExamDate);
                        if (ChkDt)
                        {

                            objWTHTC.ExamDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(WTHTC.ExamDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

                        }
                        else
                        {
                            objWTHTC.ExamDate = null;
                        }
                   
                   

                    TimeSpan StartTime = WTHTC.DtStartTime.TimeOfDay;       
                    TimeSpan EndTime = WTHTC.DtEndTime.TimeOfDay;
                    TimeSpan Diff = new TimeSpan(5, 30, 00);
                    StartTime = StartTime.Add(Diff);
                    EndTime = EndTime.Add(Diff);
                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("AdmApplicationConfigAttachVenueEdit", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProgramInstancePartTermId", objWTHTC.ProgrammeInstancePartTermId);
                    cmd.Parameters.AddWithValue("@ExamDate", objWTHTC.ExamDate);
                    cmd.Parameters.AddWithValue("@StartTime", StartTime);
                    cmd.Parameters.AddWithValue("@EndTime", EndTime);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    Con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    Con.Close();
                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Your data has been Saved successfully.";
                    }
                    return Return.returnHttp("200", strMessage.ToString(), null);
                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message, null);
                }
            }
        }
        #endregion

        #region EntranceTestCountForSentEmailSMS
        [HttpPost]
        public HttpResponseMessage EntranceTestCountforSentEmailSMS(EntranceTestSentSMSEmail ObjSentSMSEmail)
        {
            EntranceTestSentSMSEmail modelobj = new EntranceTestSentSMSEmail();

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
              
                string RadioALLFlag = Convert.ToString(ObjSentSMSEmail.RadioALLFlag);
                string RadioVerifiedFlag = Convert.ToString(ObjSentSMSEmail.RadioVerifiedFlag);
                SqlCommand cmd = new SqlCommand("EntranceTestApplicantsCountforEmailSMS", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", modelobj.ProgrammeInstancePartTermId);
                cmd.Parameters.AddWithValue("@RadioALLFlag", RadioALLFlag);
                cmd.Parameters.AddWithValue("@RadioVerifiedFlag", RadioVerifiedFlag);


                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<EntranceTestSentSMSEmail> ObjSmsEmailCount = new List<EntranceTestSentSMSEmail>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        EntranceTestSentSMSEmail Objcount = new EntranceTestSentSMSEmail();

                        Objcount.RadioALLFlag = Convert.ToString(dr["RadioALLFlag"]);
                        Objcount.RadioVerifiedFlag = Convert.ToString(dr["RadioVerifiedFlag"]);
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

        #region GenericConfigurationGetByKeyName
        [HttpPost]
        public HttpResponseMessage GenericConfigurationGetByKeyName(JObject Generic)
        {
            try
            {
                GenericConfiguration generic = new GenericConfiguration();
                dynamic JsonData = Generic;
                generic.KeyName = Convert.ToString(JsonData.KeyName);
                SqlCommand Cmd = new SqlCommand("GenericConfigurationGetByKeyName", Con);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@KeyName", generic.KeyName);

                Da.SelectCommand = Cmd;
                Da.Fill(Dt);

                List<GenericConfiguration> ObjLstGenConfig = new List<GenericConfiguration>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        GenericConfiguration objGenConfig = new GenericConfiguration();

                        objGenConfig.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objGenConfig.KeyName = Convert.ToString(Dt.Rows[i]["KeyName"]);
                        objGenConfig.Value = Convert.ToString(Dt.Rows[i]["Value"]);
                        //objGenConfig.IsActive = Convert.ToBoolean(dt.Rows[i]["IsActive"]);
                        ObjLstGenConfig.Add(objGenConfig);
                    }
                }
                return Return.returnHttp("200", ObjLstGenConfig, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region SendVerificationSMSEmailtoApplicant
        [HttpPost]
        public HttpResponseMessage SendVerificationSMSEmailtoApplicant(WrittenTestHallTicketConfiguration Modelobject)

        {
            WrittenTestHallTicketConfiguration objWTHTC = new WrittenTestHallTicketConfiguration();
            //dynamic jsonData = jsonobject;

            try
            {
                objWTHTC.Id = Modelobject.ApplicationId;
                // postappverification.ApplicantRegId = jsonData.ApplicantRegistrationId;
                objWTHTC.MobileNo = Modelobject.MobileNo;
                objWTHTC.EmailId = Modelobject.EmailId;
                objWTHTC.EmailKeyName = Modelobject.EmailKeyName;
               
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }


                objWTHTC.IsEntranceTestSmsBy = Convert.ToInt64(responseToken);
                objWTHTC.IsEntranceTestEmailBy = Convert.ToInt64(responseToken);

                SqlCommand cmd = new SqlCommand("EntranceTestVerificationSMSEmail", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", objWTHTC.Id);
                cmd.Parameters.AddWithValue("@IsEntranceTestSmsBy", objWTHTC.IsEntranceTestSmsBy);
                cmd.Parameters.AddWithValue("@IsEntranceTestEmailBy", objWTHTC.IsEntranceTestEmailBy);       
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                Con.Close();
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
                    string mobile = objWTHTC.MobileNo.Trim();
                    ////Sender ID,While using route4 sender id should be 6 characters long.
                    string senderid = "MSUBIS";
                    ////Your message to send, Add URL encoding here.
                    ////string message = System.Web.HttpUtility.UrlEncode("your OTP for MSU Wi-Fi Registration : " + OTP);
                    //string message = "Dear Applicant, MSUIS login credentials is Username " + postappverification.Id + " and Password " + postappverification.Id + ". Please do not share this information to anyone.\n- Team MSUB";
                    string message = "Dear Applicant, Hall Ticket For Entrance Exam can be download from Applicant Portal" + ". Regards,\nMSUB";


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

                    #region Email for Applicant Verification Email
                    try
                    {
                        SmtpClient smtpClient = new SmtpClient();
                        MailMessage MailMessage = new MailMessage();
                        //dynamic Jsondata = jObject;

                        string ToEmail = Convert.ToString(objWTHTC.EmailId);

                        MailAddress fromAddress = new MailAddress(objWTHTC.EmailKeyName, "MSUIS - Entrance Test Notification Status");

                        smtpClient.Host = "smtp.gmail.com";

                        //Default port will be 25
                        smtpClient.Port = 587;

                        //From address will be given as a MailAddress Object
                        MailMessage.From = fromAddress;
                        MailMessage.To.Add(ToEmail);

                        // To address collection of MailAddress
                        MailMessage.Bcc.Add(objWTHTC.EmailKeyName);
                        MailMessage.Subject = "MSUIS - Entrance Test Notification";
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
                        "<br/><br/><div>Hall Ticket For Entrance/Written Exam Can Be Downloaded From Applicant Portal </div>" +
                                        
                        "<br/><br/><br/>Thank You." +
                        "<br/>Team-MSUIS" +
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
                        myMailCredential.UserName = objWTHTC.EmailKeyName;
                        myMailCredential.Password = "msu@#$123";
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.EnableSsl = true;
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtpClient.Timeout = 20000;
                        smtpClient.Credentials = myMailCredential;

                        // Send SMTP mail
                        string result = string.Empty;
                        smtpClient.Send(MailMessage);

                        return Return.returnHttp("200", "Entrance Test Notification SMS and Email Has Been Sent Successfully to Applicant.", null);
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

        #region SendVerificationBulkSMStoApplicant
        [HttpPost]
        public HttpResponseMessage SendVerificationBulkSMStoApplicant(List<EntranceTestSMSSent> ETSS)

        {
            foreach (EntranceTestSMSSent datacheck in ETSS)
            {
                if (datacheck.IsEntranceTestSms == false)
                {

                    //string FinalAdmFeeDate = string.Format("{0: dd-MM-yyyy}", datacheck.FinalAdmFeeDate);

                    string token = Request.Headers.GetValues("token").FirstOrDefault();
                    TokenOperation tokenOperation = new TokenOperation();
                    string responseToken = tokenOperation.ValidateToken(token);
                    if (responseToken == "0")
                    {
                        return Return.returnHttp("0", null, null);
                    }


                    datacheck.IsEntranceTestSmsBy = Convert.ToInt64(responseToken);

                    SqlCommand cmd = new SqlCommand("EntranceTestSentNotificationSMS", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", datacheck.ApplicationId);
                    cmd.Parameters.AddWithValue("@IsEntranceTestSmsBy", datacheck.IsEntranceTestSmsBy);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    Con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    Con.Close();
                    //return Return.returnHttp("200", strMessage, null);

                    // *************Start SMS & Email***************
                    if (strMessage == "Notification SMS has been Sent Successfully.")
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
                        //string message = "Dear, Your Venue For Exam. " + datacheck.Venue + " has been successfully Generated. Regards,\nMSUB";
                        string message = "Dear Applicant, Hall Ticket For Entrance Exam can be download from Applicant Portal" + ". Regards,\nMSUB";

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

                        #endregion
                    }
                }

                // *************Stop SMS & Email***************
            }
            return Return.returnHttp("200", "Entrance Test Notification SMS Has Been Sent Successfully to Applicant", null);
        }

        #endregion

        #region ImportToExcel

        [HttpPost]
        public HttpResponseMessage UpdateVenueDetailsFromExcelForWTHC(ImportExcelofVenueDetails[] VenueDetailsArray)
        {
            string token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation tokenOperation = new TokenOperation();
            string responseToken = tokenOperation.ValidateToken(token);
            if (responseToken == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            //RefundModelFinal cm1 = new RefundModelFinal();
            //dynamic jsonData = jsonobject;
            var successcount = 0;
            var failurecount = 0;
            var VenueDetailsData = VenueDetailsArray;
            Con.Open();
            for (var i = 0; i < VenueDetailsData.Length; i++)
            {
                if (String.IsNullOrWhiteSpace(Convert.ToString(VenueDetailsData[i].ApplicationId)) || VenueDetailsData[i].ApplicationId == 0)
                {
                    return Return.returnHttp("201", "Invalid Application Id ", null);
                }
                        
                else if (String.IsNullOrEmpty(Convert.ToString(VenueDetailsData[i].VenueName)))
                {
                    return Return.returnHttp("201", "Invalid VenueName", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(VenueDetailsData[i].Address)))
                {
                    return Return.returnHttp("201", "Invalid Address", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(VenueDetailsData[i].BlockName)))
                {
                    return Return.returnHttp("201", "Invalid BlockName", null);
                }
                else if (String.IsNullOrWhiteSpace(Convert.ToString(VenueDetailsData[i].EntranceTestSeatNo))|| VenueDetailsData[i].EntranceTestSeatNo==0)
                {
                    return Return.returnHttp("201", "Invalid EntranceTestSeatNo", null);
                }
                else
                {
                    try { 
                    ImportExcelofVenueDetails cm1 = new ImportExcelofVenueDetails();
                    dynamic jsonData = VenueDetailsData[i];                
                    cm1.ApplicationId = jsonData.ApplicationId;
                    cm1.VenueName = jsonData.VenueName;
                    cm1.Address = jsonData.Address;
                    cm1.BlockName = jsonData.BlockName;
                    cm1.EntranceTestSeatNo = jsonData.EntranceTestSeatNo;

                    SqlCommand cmd = new SqlCommand("UpdateVenueDetailsFromExcelForWTHC", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ApplicationId", cm1.ApplicationId);
                    cmd.Parameters.AddWithValue("@VenueName", cm1.VenueName);
                    cmd.Parameters.AddWithValue("@Address", cm1.Address);
                    cmd.Parameters.AddWithValue("@BlockName", cm1.BlockName);
                    cmd.Parameters.AddWithValue("@EntranceTestSeatNo", cm1.EntranceTestSeatNo);

                    cmd.ExecuteNonQuery();


                    successcount++;

                }
                catch (Exception e)
                {
                    failurecount++;
                }

                }

            }



            Con.Close();

            //var response = Request.CreateResponse(HttpStatusCode.OK);
            //String resp = "{\"status\":\"SUCCESS\",\"data\":[\"SuccessCount\"}";
            //response.Content = new StringContent(resp, System.Text.Encoding.UTF8, "application/json");
            //return response;

            return Return.returnHttp("200", "Data Uploaded into Database", null);
        }

        #endregion




    }
}
