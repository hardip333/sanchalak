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
    public class InstituteGroupChangeController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region Institute Group Change Data
        [HttpPost]
        public HttpResponseMessage InstituteGroupChangeDetails(InstituteGroupChange ObjInstGrp)
        {           

            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("InstituteGroupChangeDetailsGet", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@InstituteId", facultyDepartIntituteId);
                cmd.Parameters.AddWithValue("@ApplicationId", ObjInstGrp.ApplicationId);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<InstituteGroupChange> ObjLstInstGroup = new List<InstituteGroupChange>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        InstituteGroupChange ObjInstGroup = new InstituteGroupChange();
                                                
                        ObjInstGroup.ApplicationId = (Convert.ToString(dr["ApplicationId"])).IsEmpty() ? 0 : Convert.ToInt64(dr["ApplicationId"]);
                        ObjInstGroup.ApplicantUserName = (Convert.ToString(dr["ApplicantUserName"])).IsEmpty() ? 0 : Convert.ToInt64(dr["ApplicantUserName"]);
                        ObjInstGroup.ProgrammeInstancePartTermId = (Convert.ToString(dr["ProgrammeInstancePartTermId"])).IsEmpty() ? 0 : Convert.ToInt64(dr["ProgrammeInstancePartTermId"]);
                        ObjInstGroup.DestinationIncProgInstPartTermId = (Convert.ToString(dr["DestinationIncProgInstPartTermId"])).IsEmpty() ? 0 : Convert.ToInt64(dr["DestinationIncProgInstPartTermId"]);
                        ObjInstGroup.PreferenceGroupId = (Convert.ToString(dr["PreferenceGroupId"])).IsEmpty() ? 0 : Convert.ToInt32(dr["PreferenceGroupId"]);
                        ObjInstGroup.FeeCategoryId = (Convert.ToString(dr["FeeCategoryId"])).IsEmpty() ? 0 : Convert.ToInt32(dr["FeeCategoryId"]);
                        ObjInstGroup.AdmittedInstituteId = (Convert.ToString(dr["AdmittedInstituteId"])).IsEmpty() ? 0 : Convert.ToInt32(dr["AdmittedInstituteId"]);
                        ObjInstGroup.FeeCategoryPartTermMapId = (Convert.ToString(dr["FeeCategoryPartTermMapId"])).IsEmpty() ? 0 : Convert.ToInt64(dr["FeeCategoryPartTermMapId"]);
                        ObjInstGroup.ApplicantName = (Convert.ToString(dr["NameAsPerMarksheet"])).IsEmpty() ? "" : Convert.ToString(dr["NameAsPerMarksheet"]);
                        ObjInstGroup.ProgrammeName = (Convert.ToString(dr["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(dr["ProgrammeName"]);
                        ObjInstGroup.BranchName = (Convert.ToString(dr["BranchName"])).IsEmpty() ? "" : Convert.ToString(dr["BranchName"]);
                        ObjInstGroup.InstituteName = (Convert.ToString(dr["InstituteName"])).IsEmpty() ? "" : Convert.ToString(dr["InstituteName"]);
                        ObjInstGroup.GroupName = (Convert.ToString(dr["GroupName"])).IsEmpty() ? "" : Convert.ToString(dr["GroupName"]);
                        ObjInstGroup.FeeCategoryName = (Convert.ToString(dr["FeeCategoryName"])).IsEmpty() ? "" : Convert.ToString(dr["FeeCategoryName"]);
                        ObjInstGroup.IsApprovedByFacultySts = (Convert.ToString(dr["IsApprovedByFacultySts"])).IsEmpty() ? "" : Convert.ToString(dr["IsApprovedByFacultySts"]);
                        ObjInstGroup.IsApprovedByAcademicSts = (Convert.ToString(dr["IsApprovedByAcademicSts"])).IsEmpty() ? "" : Convert.ToString(dr["IsApprovedByAcademicSts"]);
                        ObjInstGroup.IsAdmissionFeePaidSts = (Convert.ToString(dr["IsAdmissionFeePaidSts"])).IsEmpty() ? "" : Convert.ToString(dr["IsAdmissionFeePaidSts"]);
                        ObjInstGroup.IsAdmissionFeePaid = (Convert.ToString(dr["IsAdmissionFeePaid"])).IsEmpty() ? false : Convert.ToBoolean(dr["IsAdmissionFeePaid"]);
                        ObjInstGroup.IsPRNGenerated = (Convert.ToString(dr["IsPRNGenerated"])).IsEmpty() ? false : Convert.ToBoolean(dr["IsPRNGenerated"]);
                        
                        count = count + 1;
                        ObjInstGroup.IndexId = count;
                        ObjLstInstGroup.Add(ObjInstGroup);
                    }
                    return Return.returnHttp("200", ObjLstInstGroup, null);
                }
                else
                {
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    if (strMessage == "")
                    {
                        return Return.returnHttp("201", "No Record Found", null);
                    }
                    else
                    {
                        return Return.returnHttp("201", strMessage.ToString(), null);
                    }
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region New Institute Get
        [HttpPost]
        public HttpResponseMessage NewInstituteGet(NewInstitute ObjNewInst)
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

                SqlCommand cmd = new SqlCommand("NewInstituteGet", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ApplicationId", ObjNewInst.ApplicationId);
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ObjNewInst.ProgrammeInstancePartTermId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                
                List<NewInstitute> ObjLstNewInst = new List<NewInstitute>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        NewInstitute ObjNInst = new NewInstitute();

                        ObjNInst.AdmittedInstituteId = Convert.ToInt32(dr["AdmittedInstituteId"]);
                        ObjNInst.InstituteName = Convert.ToString(dr["InstituteName"]);

                        ObjLstNewInst.Add(ObjNInst);
                    }

                }
                else
                {
                    return Return.returnHttp("201", "No Record Found.", null);
                }
               
                return Return.returnHttp("200", ObjLstNewInst, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region New Group Get
        [HttpPost]
        public HttpResponseMessage NewGroupGet(NewGroup ObjNewGrp)
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

                SqlCommand cmd = new SqlCommand("NewGroupGet", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ApplicationId", ObjNewGrp.ApplicationId);
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ObjNewGrp.ProgrammeInstancePartTermId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                
                List<NewGroup> ObjLstNewGroup = new List<NewGroup>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        NewGroup ObjNewGroup = new NewGroup();

                        ObjNewGroup.PreferenceGroupId = Convert.ToInt32(dr["PreferenceGroupId"]);
                        ObjNewGroup.GroupName = Convert.ToString(dr["GroupName"]);

                        ObjLstNewGroup.Add(ObjNewGroup);
                    }

                }
                else
                {
                    return Return.returnHttp("201", "No Record Found.", null);
                }
                return Return.returnHttp("200", ObjLstNewGroup, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region New Fee Category Get
        [HttpPost]
        public HttpResponseMessage NewFeeCategoryGet(NewFeeCategory ObjNewFeeCat)
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

                SqlCommand cmd = new SqlCommand("NewFeeCategoryGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                                
                cmd.Parameters.AddWithValue("@FeeCategoryPartTermMapId", ObjNewFeeCat.FeeCategoryPartTermMapId);
                cmd.Parameters.AddWithValue("@DestinationIncProgInstPartTermId", ObjNewFeeCat.DestinationIncProgInstPartTermId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<NewFeeCategory> ObjLstNewFeeCat = new List<NewFeeCategory>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        NewFeeCategory ObjNewFeeCategory = new NewFeeCategory();

                        ObjNewFeeCategory.FeeCategoryId = Convert.ToInt32(dr["FeeCategoryId"]);
                        ObjNewFeeCategory.FeeCategoryPartTermMapId = Convert.ToInt64(dr["FeeCategoryPartTermMapId"]);
                        ObjNewFeeCategory.FeeCategoryName = Convert.ToString(dr["FeeCategoryName"]);

                        ObjLstNewFeeCat.Add(ObjNewFeeCategory);
                    }

                }
                else
                {
                    return Return.returnHttp("201", "No Record Found.", null);
                }

                return Return.returnHttp("200", ObjLstNewFeeCat, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region Institute Group Change Add
        [HttpPost]
        public HttpResponseMessage InstituteGroupChangeDetailsAdd(NewInstituteGroupChange ObjInstGrp)
        {

            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string responseToken = ac.ValidateToken(token);

                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                if (ObjInstGrp.IsAdmissionFeePaid == true && ObjInstGrp.IsFeeCategorySame == false && ObjInstGrp.IFSCShortCode == "BARB0")
                {
                    string sPath = "";
                    sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/RefundPassbookPhoto/");

                    string abc = null;
                    if (Request.Headers.Contains("passbookImage"))
                    {
                        abc = Request.Headers.GetValues("passbookImage").FirstOrDefault();
                    }

                    /*if (!File.Exists(sPath + Path.GetFileName(res + "_" + abc)))
                    {
                        return Return.returnHttp("201", "File Not Exists.", null);
                    }*/                    
                }

                if (ObjInstGrp.IsAdmissionFeePaid == true && ObjInstGrp.IsFeeCategorySame == false && ObjInstGrp.IFSCShortCode != "BARB0")
                {
                    string sPath = "";
                    sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/RefundPassbookPhoto/");

                    string abc = null;
                    if (Request.Headers.Contains("passbookImage"))
                    {
                        abc = Request.Headers.GetValues("passbookImage").FirstOrDefault();
                    }

                    string sPath1 = "";
                    sPath1 = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/InstituteGroupChangeRTGSForm/");

                    string abc1 = null;
                    if (Request.Headers.Contains("InstGroupChangeRTGSForm"))
                    {
                        abc1 = Request.Headers.GetValues("InstGroupChangeRTGSForm").FirstOrDefault();
                    }
                }

                if (String.IsNullOrEmpty(Convert.ToString(ObjInstGrp.ApplicationId)))
                {
                    return Return.returnHttp("201", "Application Number Not Found !", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjInstGrp.OldInstituteId)))
                {
                    return Return.returnHttp("201", "Admitted Institute Not Found !", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjInstGrp.NewInstituteId)))
                {
                    return Return.returnHttp("201", "No Institute For Change !", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjInstGrp.OldFeeCategoryId)))
                {
                    return Return.returnHttp("201", "Admitted Fee-Category Not Found !", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjInstGrp.NewFeeCategoryId)))
                {
                    return Return.returnHttp("201", "New Fee-Category Not Found !", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjInstGrp.OldPreferenceGroupId)))
                {
                    return Return.returnHttp("201", "Admitted Group Not Found !", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjInstGrp.NewPreferenceGroupId)))
                {
                    return Return.returnHttp("201", "New Group Not Found !", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjInstGrp.OldFeeCategoryPartTermMapId)))
                {
                    return Return.returnHttp("201", "Admitted Fee-category Part Term Not Found !", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjInstGrp.NewFeeCategoryPartTermMapId)))
                {
                    return Return.returnHttp("201", "New Fee-category Part Term Not Found !", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjInstGrp.Remarks)))
                {
                    return Return.returnHttp("201", "Please enter Remarks", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjInstGrp.AccountNumber)) && ObjInstGrp.IsAdmissionFeePaid == true && ObjInstGrp.IsFeeCategorySame == false)
                {
                    return Return.returnHttp("201", "Please Add Account Number.", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjInstGrp.AccountName)) && ObjInstGrp.IsAdmissionFeePaid == true && ObjInstGrp.IsFeeCategorySame == false)
                {
                    return Return.returnHttp("201", "Please Add Account Name.", null);
                }
                else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjInstGrp.BankId)) && ObjInstGrp.IsAdmissionFeePaid == true && ObjInstGrp.IsFeeCategorySame == false)
                {
                    return Return.returnHttp("201", "Please select Bank.", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjInstGrp.IFSCCODE)) && ObjInstGrp.IsAdmissionFeePaid == true && ObjInstGrp.IsFeeCategorySame == false)
                {
                    return Return.returnHttp("201", "Please Add IFSC Code.", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjInstGrp.PassbookPhoto)) && ObjInstGrp.IsAdmissionFeePaid == true && ObjInstGrp.IsFeeCategorySame == false)
                {
                    return Return.returnHttp("201", "Please Add Passbook Document.", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjInstGrp.RTGSForm)) && ObjInstGrp.IsAdmissionFeePaid == true && ObjInstGrp.IsFeeCategorySame == false && ObjInstGrp.IFSCShortCode != "BARB0")
                {
                    return Return.returnHttp("201", "Please Add RTGS Form.", null);
                }

                else
                {

                    Int64 UserId = Convert.ToInt64(res.ToString());

                    //SqlCommand cmd = new SqlCommand("TransferInstituteOrGroupAdd", con);
                    SqlCommand cmd = new SqlCommand("TransferInstituteOrGroupAddByFaculty", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ApplicationId", ObjInstGrp.ApplicationId);
                    cmd.Parameters.AddWithValue("@OldInstituteId", ObjInstGrp.OldInstituteId);
                    cmd.Parameters.AddWithValue("@NewInstituteId", ObjInstGrp.NewInstituteId);
                    cmd.Parameters.AddWithValue("@OldFeeCategoryId", ObjInstGrp.OldFeeCategoryId);
                    cmd.Parameters.AddWithValue("@NewFeeCategoryId", ObjInstGrp.NewFeeCategoryId);
                    
                    if(ObjInstGrp.OldPreferenceGroupId == 0) { 
                        cmd.Parameters.AddWithValue("@OldPreferenceGroupId", null);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@OldPreferenceGroupId", ObjInstGrp.OldPreferenceGroupId);
                    }
                    
                    if (ObjInstGrp.OldPreferenceGroupId == 0)
                    {
                        cmd.Parameters.AddWithValue("@NewPreferenceGroupId", null);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@NewPreferenceGroupId", ObjInstGrp.NewPreferenceGroupId);
                    }
                    
                    cmd.Parameters.AddWithValue("@OldFeeCategoryPartTermMapId", ObjInstGrp.OldFeeCategoryPartTermMapId);
                    cmd.Parameters.AddWithValue("@NewFeeCategoryPartTermMapId", ObjInstGrp.NewFeeCategoryPartTermMapId);
                    cmd.Parameters.AddWithValue("@IsFeeCategorySame", ObjInstGrp.IsFeeCategorySame);
                    cmd.Parameters.AddWithValue("@IsAdmissionFeesPaid", ObjInstGrp.IsAdmissionFeePaid);
                    cmd.Parameters.AddWithValue("@IsPRNGenerated", ObjInstGrp.IsPRNGenerated);
                    cmd.Parameters.AddWithValue("@Remarks", ObjInstGrp.Remarks);

                    if (ObjInstGrp.IsFeeCategorySame == false && ObjInstGrp.IsAdmissionFeePaid == true && ObjInstGrp.IFSCShortCode == "BARB0")
                    {
                        cmd.Parameters.AddWithValue("@AccountName", ObjInstGrp.AccountName);
                        cmd.Parameters.AddWithValue("@AccountNumber", ObjInstGrp.AccountNumber);
                        cmd.Parameters.AddWithValue("@BankId", ObjInstGrp.BankId);
                        cmd.Parameters.AddWithValue("@IFSCCODE", ObjInstGrp.IFSCCODE);
                        cmd.Parameters.AddWithValue("@PassbookPhoto", responseToken + "_" + ObjInstGrp.PassbookPhoto);
                        cmd.Parameters.AddWithValue("@RTGSForm", null);
                    }
                    else if (ObjInstGrp.IsFeeCategorySame == false && ObjInstGrp.IsAdmissionFeePaid == true && ObjInstGrp.IFSCShortCode != "BARB0")
                    {
                        cmd.Parameters.AddWithValue("@AccountName", ObjInstGrp.AccountName);
                        cmd.Parameters.AddWithValue("@AccountNumber", ObjInstGrp.AccountNumber);
                        cmd.Parameters.AddWithValue("@BankId", ObjInstGrp.BankId);
                        cmd.Parameters.AddWithValue("@IFSCCODE", ObjInstGrp.IFSCCODE);
                        cmd.Parameters.AddWithValue("@PassbookPhoto", responseToken + "_" + ObjInstGrp.PassbookPhoto);
                        cmd.Parameters.AddWithValue("@RTGSForm", responseToken + "_" + ObjInstGrp.RTGSForm);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@AccountName", null);
                        cmd.Parameters.AddWithValue("@AccountNumber", null);
                        cmd.Parameters.AddWithValue("@BankId", null);
                        cmd.Parameters.AddWithValue("@IFSCCODE", null);
                        cmd.Parameters.AddWithValue("@PassbookPhoto", null);
                        cmd.Parameters.AddWithValue("@RTGSForm", null);
                    }

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
                        strMessage = "Your request has been processed successfully.";
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

        #region Passbook Photo Upload

        [HttpPost()]
        public HttpResponseMessage UploadPassbookPhoto()
        {

            int iUploadedCnt = 0;

            // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
            string sPath = "";
            Boolean already_exist = false;
            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/RefundPassbookPhoto/");

            string token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation tokenOperation = new TokenOperation();
            string responseToken = tokenOperation.ValidateToken(token);
            if (responseToken == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            string abc = Request.Headers.GetValues("passbookImage").FirstOrDefault();

            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            // CHECK THE FILE COUNT.
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];

                if (hpf.ContentLength > 0)
                {
                    /* if (abc == "EWS.pdf" || abc == "EWS.docx")
                     {
                         File.Delete(sPath + Path.GetFileName(responseToken + "_" + "EWS.pdf"));
                         File.Delete(sPath + Path.GetFileName(responseToken + "_" + "EWS.docx"));
                     }*/
                    if (File.Exists(sPath + Path.GetFileName(responseToken + "_" + abc)))
                    {
                        File.Delete(sPath + Path.GetFileName(responseToken + "_" + abc));
                    }

                    // SAVE THE FILES IN THE FOLDER.
                    hpf.SaveAs(sPath + Path.GetFileName(responseToken + "_" + abc));
                    //hpf.SaveAs(sPath + Path.GetFileName(abc));

                    iUploadedCnt = iUploadedCnt + 1;

                }
            }


            // RETURN A MESSAGE (OPTIONAL).
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

        #region RTGS Form Upload

        [HttpPost()]
        public HttpResponseMessage UploadRTGSForm()
        {

            int iUploadedCnt = 0;

            // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
            string sPath1 = "";
            Boolean already_exist = false;
            sPath1 = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/InstituteGroupChangeRTGSForm/");

            string token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation tokenOperation = new TokenOperation();
            string responseToken1 = tokenOperation.ValidateToken(token);
            if (responseToken1 == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            string abc = Request.Headers.GetValues("InstGroupChangeRTGSForm").FirstOrDefault();

            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            // CHECK THE FILE COUNT.
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];

                if (hpf.ContentLength > 0)
                {
                    /* if (abc == "EWS.pdf" || abc == "EWS.docx")
                     {
                         File.Delete(sPath + Path.GetFileName(responseToken + "_" + "EWS.pdf"));
                         File.Delete(sPath + Path.GetFileName(responseToken + "_" + "EWS.docx"));
                     }*/
                    if (File.Exists(sPath1 + Path.GetFileName(responseToken1 + "_" + abc)))
                    {
                        File.Delete(sPath1 + Path.GetFileName(responseToken1 + "_" + abc));
                    }

                    // SAVE THE FILES IN THE FOLDER.
                    hpf.SaveAs(sPath1 + Path.GetFileName(responseToken1 + "_" + abc));
                    //hpf.SaveAs(sPath + Path.GetFileName(abc));

                    iUploadedCnt = iUploadedCnt + 1;

                }
            }


            // RETURN A MESSAGE (OPTIONAL).
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

        #region GenericConfigurationGetById
        [HttpPost]
        public HttpResponseMessage GenericConfigurationGetById(JObject jid)
        {
            try
            {
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                //List<GenMaritalStatus> slist = new List<GenMaritalStatus>();

                GenericConfiguration generic = new GenericConfiguration();
                dynamic JsonData = jid;
                generic.Id = Convert.ToInt16(JsonData.Id);
                //generic.KeyName = Convert.ToString(JsonData.KeyName);
                SqlCommand Cmd = new SqlCommand("GenericConfigurationGetById", con);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@Id", generic.Id);
                //Cmd.Parameters.AddWithValue("@KeyName", generic.KeyName);

                da.SelectCommand = Cmd;
                da.Fill(dt);

                List<GenericConfiguration> ObjLstGenConfig = new List<GenericConfiguration>();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        GenericConfiguration objGenConfig = new GenericConfiguration();

                        objGenConfig.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        objGenConfig.KeyName = Convert.ToString(dt.Rows[i]["KeyName"]);
                        objGenConfig.Value = Convert.ToString(dt.Rows[i]["Value"]);
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

        #region Bank List Get
        [HttpPost]
        public HttpResponseMessage BankListGet()
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

                SqlCommand cmd = new SqlCommand("BankListGet", con);
                cmd.CommandType = CommandType.StoredProcedure;

                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<BankDetails> ObjBankList = new List<BankDetails>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        BankDetails ObjBank = new BankDetails();

                        ObjBank.Id = Convert.ToInt32(dr["Id"]);
                        ObjBank.BankName = Convert.ToString(dr["BankName"]);
                        ObjBank.BankType = Convert.ToString(dr["BankType"]);
                        ObjBank.IfscInitial = Convert.ToString(dr["IfscInitial"]);

                        ObjBankList.Add(ObjBank);
                    }

                }
                else
                {
                    return Return.returnHttp("201", "No Record Found.", null);
                }

                return Return.returnHttp("200", ObjBankList, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region Get Data For Institute Group Change Report
        [HttpPost]
        public HttpResponseMessage GetDataForInstituteGroupChangeReport(InstituteGroupChangeReport objReport)
        {

            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                                

                SqlCommand cmd = new SqlCommand("GetDataForInstituteGroupChangeReport", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (String.IsNullOrEmpty(Convert.ToString(objReport.InstituteId)) || objReport.InstituteId == 0)
                {
                    cmd.Parameters.AddWithValue("@InstituteId", facultyDepartIntituteId);                    
                }
                else
                {
                    cmd.Parameters.AddWithValue("@InstituteId", objReport.InstituteId);
                }
                
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;

                List<InstituteGroupChangeReport> ObjLstInstGrp = new List<InstituteGroupChangeReport>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        InstituteGroupChangeReport ObjInstGrp = new InstituteGroupChangeReport();

                        ObjInstGrp.Application_Form_Number = (Convert.ToString(dr["Application_Form_Number"])).IsEmpty() ? 0 : Convert.ToInt64(dr["Application_Form_Number"]);
                        ObjInstGrp.Instance_Part_Term_Name = (Convert.ToString(dr["Instance_Part_Term_Name"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["Instance_Part_Term_Name"]);
                        ObjInstGrp.Faculty_Name = (Convert.ToString(dr["Faculty_Name"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["Faculty_Name"]);
                        ObjInstGrp.Name_As_Per_Marksheet = (Convert.ToString(dr["Name_As_Per_Marksheet"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["Name_As_Per_Marksheet"]);
                        ObjInstGrp.ChangeType = (Convert.ToString(dr["ChangeType"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["ChangeType"]);
                        ObjInstGrp.OldInstitute = (Convert.ToString(dr["OldInstitute"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["OldInstitute"]);
                        ObjInstGrp.NewInstitute = (Convert.ToString(dr["NewInstitute"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["NewInstitute"]);
                        ObjInstGrp.OldAllotedGroup = (Convert.ToString(dr["OldAllotedGroup"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["OldAllotedGroup"]);
                        ObjInstGrp.NewAllotedGroup = (Convert.ToString(dr["NewAllotedGroup"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["NewAllotedGroup"]);
                        ObjInstGrp.OldFeeCategory = (Convert.ToString(dr["OldFeeCategory"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["OldFeeCategory"]);
                        ObjInstGrp.NewFeeCategory = (Convert.ToString(dr["NewFeeCategory"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["NewFeeCategory"]);
                        ObjInstGrp.Admission_Fee_Payment_Status = (Convert.ToString(dr["Admission_Fee_Payment_Status"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["Admission_Fee_Payment_Status"]);
                        ObjInstGrp.Is_AdmissionFee_Category_Same = (Convert.ToString(dr["Is_AdmissionFee_Category_Same"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["Is_AdmissionFee_Category_Same"]);
                        ObjInstGrp.Is_PRN_Generated_Status = (Convert.ToString(dr["Is_PRN_Generated_Status"])).IsEmpty() ? "" : Convert.ToString(dr["Is_PRN_Generated_Status"]);
                        ObjInstGrp.RequestStatus = (Convert.ToString(dr["RequestStatus"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["RequestStatus"]);
                        
                        count = count + 1;
                        ObjInstGrp.IndexId = count;
                        ObjLstInstGrp.Add(ObjInstGrp);
                    }

                }
                return Return.returnHttp("200", ObjLstInstGrp, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

    }
}
