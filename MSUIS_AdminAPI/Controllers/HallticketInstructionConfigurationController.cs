
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
using System.Web.Http;
using System.Web.WebPages;
namespace MSUISApi.Controllers
{
    public class HallticketInstructionConfigurationController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        #region Exam Event Get Code

        [HttpPost]
        public HttpResponseMessage ExamEventMasterListGet(HallticketInstructionConfiguration dataString)
        {

            try
            {

                SqlCommand Cmd = new SqlCommand("ExamEventGetForDropDown", con);
                Cmd.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = Cmd;
                sda.Fill(Dt);

                List<HallticketInstructionConfiguration> ObjLstHallTicket = new List<HallticketInstructionConfiguration>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        HallticketInstructionConfiguration objHallTicket = new HallticketInstructionConfiguration();

                        objHallTicket.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objHallTicket.DisplayName = Convert.ToString(Dt.Rows[i]["DisplayName"]);
                        objHallTicket.MandatoryInstructionForOnlineExam = Convert.ToString(Dt.Rows[i]["MandatoryInstructionForOnlineExam"]);
                        objHallTicket.OptionalInstructionForOnlineExam = Convert.ToString(Dt.Rows[i]["OptionalInstructionForOnlineExam"]);
                        objHallTicket.MandatoryInstructionForOfflineExam = Convert.ToString(Dt.Rows[i]["MandatoryInstructionForOfflineExam"]);
                        objHallTicket.OptionalInstructionForOfflineExam = Convert.ToString(Dt.Rows[i]["OptionalInstructionForOfflineExam"]);
                        objHallTicket.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objHallTicket.IsClosed = (Convert.ToString(Dt.Rows[i]["IsClosed"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsClosed"]);

                        objHallTicket.IsDeleted = Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);

                        ObjLstHallTicket.Add(objHallTicket);
                    }
                }
                return Return.returnHttp("200", ObjLstHallTicket, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Exam Event Get By Id Code

        [HttpPost]
        public HttpResponseMessage ExamEventMasterListGetById(HallticketInstructionConfiguration objHallTicketIns)
        {

            try
            {

                SqlCommand Cmd = new SqlCommand("ExamEventMasterGetById", con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Id", objHallTicketIns.Id);
                sda.SelectCommand = Cmd;
                sda.Fill(Dt);

                List<HallticketInstructionConfiguration> ObjLstHallTicket = new List<HallticketInstructionConfiguration>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        HallticketInstructionConfiguration objHallTicket = new HallticketInstructionConfiguration();


                        objHallTicket.DisplayName = Convert.ToString(Dt.Rows[i]["DisplayName"]);
                        objHallTicket.MandatoryInstructionForOnlineExam = Convert.ToString(Dt.Rows[i]["MandatoryInstructionForOnlineExam"]);
                        objHallTicket.OptionalInstructionForOnlineExam = Convert.ToString(Dt.Rows[i]["OptionalInstructionForOnlineExam"]);
                        objHallTicket.MandatoryInstructionForOfflineExam = Convert.ToString(Dt.Rows[i]["MandatoryInstructionForOfflineExam"]);
                        objHallTicket.OptionalInstructionForOfflineExam = Convert.ToString(Dt.Rows[i]["OptionalInstructionForOfflineExam"]);
                        objHallTicket.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objHallTicket.IsClosed = Convert.ToBoolean(Dt.Rows[i]["IsClosed"]);
                        objHallTicket.IsDeleted = Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);
                        objHallTicket.EditStatus = Convert.ToBoolean(Dt.Rows[i]["EditStatus"]);
                        objHallTicket.DyrExamSign = Convert.ToString(Dt.Rows[i]["DyrExamSign"]);
                       
                        ObjLstHallTicket.Add(objHallTicket);
                    }
                }
                return Return.returnHttp("200", ObjLstHallTicket, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Update HallticketInstructionConfiguration Detail
        [HttpPost]
        public HttpResponseMessage HallticketInstructionConfigurationUpdate(HallticketInstructionConfiguration ObjHallTicket)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            Boolean IsClosed = Convert.ToBoolean(ObjHallTicket.IsClosed);
            
            if (IsClosed == true)
            {
                return Return.returnHttp("201", "Hall Ticket Instruction Configuration Closed You Cannot Edit", null);
            }
            
                 else if (String.IsNullOrEmpty(Convert.ToString(ObjHallTicket.DyrExamSign)) )
                    {
                        return Return.returnHttp("201", "Please Upload Signature.", null);
                    }
                    else {
                try
                {
                    Int64 Id = Convert.ToInt64(ObjHallTicket.Id);
                    String MandatoryInstructionForOnlineExam = Convert.ToString(ObjHallTicket.MandatoryInstructionForOnlineExam);
                    String OptionalInstructionForOnlineExam = Convert.ToString(ObjHallTicket.OptionalInstructionForOnlineExam);
                    String MandatoryInstructionForOfflineExam = Convert.ToString(ObjHallTicket.MandatoryInstructionForOfflineExam);
                    String OptionalInstructionForOfflineExam = Convert.ToString(ObjHallTicket.OptionalInstructionForOfflineExam);
                    String DyrExamSign = Convert.ToString(ObjHallTicket.DyrExamSign);

                    if (MandatoryInstructionForOnlineExam == "") { MandatoryInstructionForOnlineExam = null; }
                    if (OptionalInstructionForOnlineExam == "") { OptionalInstructionForOnlineExam = null; }
                    if (MandatoryInstructionForOfflineExam == "") { MandatoryInstructionForOfflineExam = null; }
                    if (OptionalInstructionForOfflineExam == "") { OptionalInstructionForOfflineExam = null; }
                    if ((MandatoryInstructionForOnlineExam != null) && (OptionalInstructionForOnlineExam == null))
                    {
                        return Return.returnHttp("201", "Please Enter Optional Instruction For Online Instruction", null);
                    }
                    if ((OptionalInstructionForOnlineExam != null) && (MandatoryInstructionForOnlineExam == null))
                    {
                        return Return.returnHttp("201", "Please Enter Mandatory Instruction For Online", null);
                    }
                    if ((MandatoryInstructionForOfflineExam != null) && (OptionalInstructionForOfflineExam == null))
                    {
                        return Return.returnHttp("201", "Please Enter Optional Instruction For Offline", null);
                    }
                    if ((OptionalInstructionForOfflineExam != null) && (MandatoryInstructionForOfflineExam == null))
                    {
                        return Return.returnHttp("201", "Please Enter Mandatory Instruction For Offline", null);
                    }
                    if ((MandatoryInstructionForOnlineExam == null) && (OptionalInstructionForOnlineExam == null)&&(MandatoryInstructionForOfflineExam==null)&&(OptionalInstructionForOfflineExam == null))
                    {
                        return Return.returnHttp("201", "Please Enter either Online or Offline Instruction", null);
                    }

                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("HallTicketInstructionConfigurationEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", ObjHallTicket.Id);
                    cmd.Parameters.AddWithValue("@MandatoryInstructionForOnlineExam", MandatoryInstructionForOnlineExam);
                    cmd.Parameters.AddWithValue("@OptionalInstructionForOnlineExam", OptionalInstructionForOnlineExam);
                    cmd.Parameters.AddWithValue("@MandatoryInstructionForOfflineExam", MandatoryInstructionForOfflineExam);
                    cmd.Parameters.AddWithValue("@OptionalInstructionForOfflineExam", OptionalInstructionForOfflineExam);
                    cmd.Parameters.AddWithValue("@DyrExamSign", DyrExamSign);
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
                        strMessage = "Your data has been Modified successfully.";
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

        #region GenericConfigurationGetByShortCode
        [HttpPost]
        public HttpResponseMessage GenericConfigurationGetByShortCode(JObject genShortCode)
        {
            try
            {
                GenericConfiguration generic = new GenericConfiguration();
                dynamic JsonData = genShortCode;
                generic.ShortCode = Convert.ToString(JsonData.ShortCode);
                SqlCommand Cmd = new SqlCommand("GenericConfigurationGetByShortCode", con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@ShortCode", generic.ShortCode);
                sda.SelectCommand = Cmd;
                sda.Fill(Dt);
                List<GenericConfiguration> ObjLstGenConfig = new List<GenericConfiguration>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        GenericConfiguration objGenConfig = new GenericConfiguration();

                        objGenConfig.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objGenConfig.ShortCode = Convert.ToString(Dt.Rows[i]["ShortCode"]);
                        objGenConfig.KeyName = Convert.ToString(Dt.Rows[i]["KeyName"]);
                        objGenConfig.Value = Convert.ToString(Dt.Rows[i]["Value"]);
                        //objGenConfig.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
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

        #region FileUpload---HallTicketSignature
        [HttpPost]
        public HttpResponseMessage UploadHallTicketSignature()
        {

            try
            {
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                string renameFile = Request.Headers.GetValues("HallTicketInsSignature").FirstOrDefault();
                string NewFile = responseToken + "_" + renameFile;
                int iUploadedCnt = 0;
                // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
                string sPath = "";
                sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/HallTicketSignature/");

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
                    return Return.returnHttp("200", NewFile, null);
                    //return Return.returnHttp("200", responseToken, null);
                    //iUploadedCnt + " Files Uploaded Successfully"
                }
                else
                {
                    return Return.returnHttp("201", iUploadedCnt + "Upload Failed", null);
                }
            }
            catch (Exception e)
            {
                if (e.Message.ToString() == "The given header was not found.")
                {
                    return Return.returnHttp("0", "Session is expired, Kindly login again.", null);
                }
                else
                {
                    return Return.returnHttp("201", e.Message.ToString(), null);
                }
            }

        }
        #endregion






    }








}










