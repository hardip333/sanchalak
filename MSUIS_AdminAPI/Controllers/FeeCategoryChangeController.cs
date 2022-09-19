using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;

namespace MSUISApi.Controllers 
{
    public class FeeCategoryChangeController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        //private object strMessage;

        #region FeeCategoryChange
        [HttpPost]
        public HttpResponseMessage FeeCategoryChange(FeeCategoryChange FCC)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String typePrefix = Request.Headers.GetValues("typePrefix").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                string resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                if (typePrefix == "INS")
                {

                    Int64 ApplicationId = Convert.ToInt64(FCC.ApplicationId);

                    //SqlCommand Cmd = new SqlCommand("FeeCategoryChange", Con);
                    SqlCommand Cmd = new SqlCommand("FeeCategoryChangeGetbyAppId", Con);
                    Cmd.CommandType = CommandType.StoredProcedure;

                    Cmd.Parameters.AddWithValue("@InstituteId", facultyDepartIntituteId);
                    Cmd.Parameters.AddWithValue("@ApplicationId", ApplicationId);
                    Cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    Cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    Da.SelectCommand = Cmd;
                    Da.Fill(Dt);

                    List<FeeCategoryChange> ObjLstFCC = new List<FeeCategoryChange>();

                    if (Dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            FeeCategoryChange objFCC = new FeeCategoryChange();

                            objFCC.NameAsPerMarksheet = (Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"]);
                            objFCC.ApplicationStatus = (Convert.ToString(Dt.Rows[i]["ApplicationStatus"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ApplicationStatus"]);
                            objFCC.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                            objFCC.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                            objFCC.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["BranchName"]);
                            objFCC.InstancePartTermName = (Convert.ToString(Dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                            objFCC.EligibilityStatus = (Convert.ToString(Dt.Rows[i]["EligibilityStatus"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["EligibilityStatus"]);
                            if (objFCC.EligibilityStatus == "Provisionally_Approved")
                            {
                                objFCC.EligibilityStatus = "Provisionally Approved";
                            }
                            objFCC.FeeCategoryName = (Convert.ToString(Dt.Rows[i]["FeeCategoryName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FeeCategoryName"]);
                            objFCC.TotalAmount = (((Dt.Rows[i]["TotalAmount"]).ToString()).IsEmpty()) ? 0 : Convert.ToDouble(Dt.Rows[i]["TotalAmount"]);
                            objFCC.IsVerificationSms = (Convert.ToString(Dt.Rows[i]["IsVerificationSms"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsVerificationSms"]);
                            objFCC.IsVerificationEmail = (Convert.ToString(Dt.Rows[i]["IsVerificationEmail"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsVerificationEmail"]);
                            objFCC.FeeCategoryPartTermMapId = (((Dt.Rows[i]["FeeCategoryPartTermMapId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["FeeCategoryPartTermMapId"]);

                            //Mohini's Code Start

                            objFCC.ApplicationId = (((Dt.Rows[i]["ApplicationId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["ApplicationId"]);
                            objFCC.EligibilityByAcademics = (Convert.ToString(Dt.Rows[i]["EligibilityByAcademics"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["EligibilityByAcademics"]);
                            objFCC.GroupName = (Convert.ToString(Dt.Rows[i]["GroupName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["GroupName"]);
                            objFCC.AdmittedInstituteName = (Convert.ToString(Dt.Rows[i]["AdmittedInstituteName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["AdmittedInstituteName"]);
                            //objFCC.IsVerificationEmailOn = (Convert.ToDateTime(Dt.Rows[i]["GroupName"])).IsEmpty() ? "Not Defined" : Convert.ToDateTime(Dt.Rows[i]["GroupName"]);
                            objFCC.IsAdmissionFeePaid = (Convert.ToString(Dt.Rows[i]["IsAdmissionFeePaid"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsAdmissionFeePaid"]);
                            objFCC.FeeCategoryId = (((Dt.Rows[i]["FeeCategoryId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["FeeCategoryId"]);
                            objFCC.IsPRNGenerated = (Convert.ToString(Dt.Rows[i]["IsPRNGenerated"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsPRNGenerated"]);
                            objFCC.IsVerificationSMSOnView = string.Format("{0: dd-MM-yyyy}", (Dt.Rows[i]["IsVerificationSmsOn"]));
                            objFCC.IsVerificationEmailOnView = string.Format("{0: dd-MM-yyyy}", (Dt.Rows[i]["IsVerificationEmailOn"]));


                            //Mohini's Code End

                            ObjLstFCC.Add(objFCC);
                        }
                        if (ObjLstFCC[0].FeeCategoryPartTermMapId != 0)
                        {
                                return Return.returnHttp("200", ObjLstFCC, null);
                            }
                            else
                            {
                            return Return.returnHttp("201", "As Application is not approved by faculty, You are not able to change Fee Category.", null);
                        }


                    }
                    else
                    {
                        string strMessage = Convert.ToString(Cmd.Parameters["@Message"].Value);
                        if (strMessage == "")
                        {
                        return Return.returnHttp("201", "This Application Number is not belongs to faculty.", null);
                    }
                        else
                        {
                            return Return.returnHttp("201", strMessage.ToString(), null);
                        }
                    }
                    
                                      
                }

                else
                {
                    return Return.returnHttp("201", "This is not Faculty Login, You can not change Fee Category.", null);
                }
                

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region FeeCategoryChange1
        [HttpPost]
        public HttpResponseMessage FeeCategoryChange1(FeeCategoryChange FCC)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String typePrefix = Request.Headers.GetValues("typePrefix").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                string resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                if (typePrefix == "INS")
                {

                    Int64 ApplicationId = Convert.ToInt64(FCC.ApplicationId);

                    SqlCommand Cmd = new SqlCommand("FeeCategoryChange1", Con);
                    Cmd.CommandType = CommandType.StoredProcedure;

                    //Cmd.Parameters.AddWithValue("@InstituteId", facultyDepartIntituteId);
                    Cmd.Parameters.AddWithValue("@ApplicationId", ApplicationId);

                    Da.SelectCommand = Cmd;
                    Da.Fill(Dt);

                    List<FeeCategoryChange> ObjLstFCC = new List<FeeCategoryChange>();

                    if (Dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            FeeCategoryChange objFCC = new FeeCategoryChange();

                            
                            objFCC.FeeCategoryName = (Convert.ToString(Dt.Rows[i]["FeeCategoryName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FeeCategoryName"]);
                            //objFCC.ApplicationId = (((Dt.Rows[i]["ApplicationId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["ApplicationId"]);
                            objFCC.FeeCategoryId = (((Dt.Rows[i]["FeeCategoryId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["FeeCategoryId"]);
                            objFCC.FeeCategoryPartTermMapId = (((Dt.Rows[i]["FeeCategoryPartTermMapId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["FeeCategoryPartTermMapId"]);
                            objFCC.TotalAmount = (((Dt.Rows[i]["TotalAmount"]).ToString()).IsEmpty()) ? 0 : Convert.ToDouble(Dt.Rows[i]["TotalAmount"]);
                            ObjLstFCC.Add(objFCC);
                        }

                        return Return.returnHttp("200", ObjLstFCC, null);

                    }

                    else
                    {
                        return Return.returnHttp("201", "", null);
                    }

                }

                else
                {
                    return Return.returnHttp("201", "This is not Faculty Login, You can not change Fee Category.", null);
                }


            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region AdmissionFeeCategoryChangeAdd
        [HttpPost]
        public HttpResponseMessage AdmissionFeeCategoryChangeAdd(AdmissionFeeCategoryChange AFCC)
        {
            try
            {
                SqlTransaction ST;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                
                String typePrefix = Request.Headers.GetValues("typePrefix").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                string resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                if (typePrefix == "INS")
                {

                    TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

                    Int64 ApplicationId = Convert.ToInt64(AFCC.ApplicationId);
                    Int64 FeeCategoryPartTermMapId = Convert.ToInt64(AFCC.FeeCategoryPartTermMapId);
                    Int64 OldFeeCategoryId = Convert.ToInt64(AFCC.OldFeeCategoryId);
                    //Int64 NewFeeCategoryId = Convert.ToInt64(AFCC.FeeCategoryId);
                    string RemarksByFaculty = Convert.ToString(AFCC.FeeCategoryChangeRemark);
                    //DateTime ? IsVerificationSmsOn = Convert.ToDateTime(AFCC.IsVerificationSmsOn);
                    //DateTime ? IsVerificationEmailOn = Convert.ToDateTime(AFCC.IsVerificationEmailOn);
                    Boolean IsAdmissionFeePaid = Convert.ToBoolean(AFCC.IsAdmissionFeePaid);
                    Boolean IsPRNGenerated = Convert.ToBoolean(AFCC.IsPRNGenerated);
                    Boolean IsRequestCompleted = Convert.ToBoolean(AFCC.IsRequestCompleted);
                    Boolean ? IsBOBAccount = Convert.ToBoolean(AFCC.IsBOBAccount);
                    if(IsAdmissionFeePaid == false && IsBOBAccount == false)
                    {
                        IsBOBAccount = null;

                    }
                    string AccountNumber = Convert.ToString(AFCC.AccountNumber);
                    string AccountName = Convert.ToString(AFCC.AccountName);
                    Int32? BankId = Convert.ToInt32(AFCC.BankId);
                    string IFSCCode = Convert.ToString(AFCC.IFSCCode);
                    string ShortIFSCCode = Convert.ToString(AFCC.ShortIFSCCode);
                    string PassbookDoc = Convert.ToString(AFCC.PassbookDoc);
                    string RTGSForm = Convert.ToString(AFCC.RTGSForm);    
                    Int64 UserId = Convert.ToInt64(res);

                    if (BankId == 0)
                    {
                        BankId = null;
                    }
                    else
                    {
                        BankId = Convert.ToInt32(AFCC.BankId);
                    }

                    if (PassbookDoc == null)
                    {
                        PassbookDoc = null;
                    }
                    else
                    {
                        PassbookDoc = res + "_" + PassbookDoc;
                    }

                    if (RTGSForm == null)
                    {
                        RTGSForm = null;
                    }
                    else
                    {
                        RTGSForm = res + "_" + RTGSForm;
                    }

                    string RequestStatus = Convert.ToString(AFCC.RequestStatus);

                    
                    if (IsAdmissionFeePaid == true)
                    {
                        IsRequestCompleted = false;
                        RequestStatus = "Forwarded to Academic";
                    }
                    else if (IsAdmissionFeePaid == false)
                    {
                        IsRequestCompleted = true;
                        RequestStatus = "Complete";
                        
                    }


                    

                    string sPath = "";
                    sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/RefundPassbookPhoto/");
                    string abc = null;
                    if (Request.Headers.Contains("PassbookDoc") && (IsAdmissionFeePaid == true))
                    {
                        abc = Request.Headers.GetValues("PassbookDoc").FirstOrDefault();
                    }
                    //else
                    //{
                    //    return Return.returnHttp("201", "Passbook Document File Not Exists.", null);
                    //}


                    string sPathRTGS = "";
                    sPathRTGS = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/FeeChangeRefundRTGSForm/");
                    string abcRTGS = null;
                    if (Request.Headers.Contains("RTGSForm") && (IsAdmissionFeePaid == true && ShortIFSCCode != "BARB0"))
                    {
                        abcRTGS = Request.Headers.GetValues("RTGSForm").FirstOrDefault();
                    }
                    //else
                    //{
                    //    return Return.returnHttp("201", "RTGS Document File Not Exists.", null);
                    //}

                    if (!File.Exists(sPath + Path.GetFileName(res + "_" + abc)) && IsAdmissionFeePaid == true)
                    {
                        return Return.returnHttp("201", "PassbookDoc File Not Exists.", null);
                    }
                    else if (!File.Exists(sPathRTGS + Path.GetFileName(res + "_" + abcRTGS)) && (IsAdmissionFeePaid == true && ShortIFSCCode != "BARB0"))
                    {
                        return Return.returnHttp("201", "RTGS File Not Exists.", null);
                    }
                    else if (String.IsNullOrEmpty(Convert.ToString(AccountNumber)) && IsAdmissionFeePaid == true)
                    {
                        return Return.returnHttp("201", "Please Add Account Number.", null);
                    }
                    else if (String.IsNullOrEmpty(Convert.ToString(AccountName)) && IsAdmissionFeePaid == true)
                    {
                        return Return.returnHttp("201", "Please Add Account Name.", null);
                    }
                    else if (String.IsNullOrWhiteSpace(Convert.ToString(BankId)) && IsAdmissionFeePaid == true)
                    {
                        return Return.returnHttp("201", "Please select BankId.", null);
                    }
                    else if (String.IsNullOrEmpty(Convert.ToString(IFSCCode)) && IsAdmissionFeePaid == true)
                    {
                        return Return.returnHttp("201", "Please Add IFSC Code.", null);
                    }
                    else if (String.IsNullOrEmpty(Convert.ToString(PassbookDoc)) && IsAdmissionFeePaid == true)
                    {
                        return Return.returnHttp("201", "Please Add Passbook Document.", null);
                    }
                    //else if (String.IsNullOrEmpty(Convert.ToString(RTGSForm)) && IsAdmissionFeePaid == true)
                    //{
                    //    return Return.returnHttp("201", "Please Add RTGS Document.", null);
                    //}
                    else if (String.IsNullOrWhiteSpace(Convert.ToString(FeeCategoryPartTermMapId)))
                    {
                        return Return.returnHttp("201", "Please select FeeCategoryPartTermMapId.", null);
                    }
                    else if (String.IsNullOrEmpty(Convert.ToString(RemarksByFaculty)))
                    {
                        return Return.returnHttp("201", "Please Add Fee Category Change Remark.", null);
                    }

                    else
                    {
                        SqlCommand Cmd = new SqlCommand("FeeCategoryChangeAddByFaculty", Con);
                        Cmd.CommandType = CommandType.StoredProcedure;

                        //Cmd.Parameters.AddWithValue("@InstituteId", facultyDepartIntituteId);

                        Cmd.Parameters.AddWithValue("@ApplicationId", ApplicationId);
                        Cmd.Parameters.AddWithValue("@FeeCategoryPartTermMapId", FeeCategoryPartTermMapId);
	                    Cmd.Parameters.AddWithValue("@OldFeeCategoryId", OldFeeCategoryId);
	                    //Cmd.Parameters.AddWithValue("@NewFeeCategoryId", NewFeeCategoryId);
	                    Cmd.Parameters.AddWithValue("@RemarksByFaculty ", RemarksByFaculty);
	                    Cmd.Parameters.AddWithValue("@IsAdmissionFeePaid", IsAdmissionFeePaid);
	                    Cmd.Parameters.AddWithValue("@IsPRNGenerated", IsPRNGenerated);
	                    Cmd.Parameters.AddWithValue("@IsRequestCompleted", IsRequestCompleted);
                        Cmd.Parameters.AddWithValue("@IsBOBAccount", IsBOBAccount);
                        Cmd.Parameters.AddWithValue("@AccountNumber", AccountNumber);
                        Cmd.Parameters.AddWithValue("@AccountName", AccountName);
                        Cmd.Parameters.AddWithValue("@BankId", BankId);
                        Cmd.Parameters.AddWithValue("@IFSCCode", IFSCCode);
                        Cmd.Parameters.AddWithValue("@PassbookDoc", PassbookDoc);
                        Cmd.Parameters.AddWithValue("@RTGSForm", RTGSForm);
	                    Cmd.Parameters.AddWithValue("@RequestStatus", RequestStatus);

                        Cmd.Parameters.AddWithValue("@UserId", UserId);
                        Cmd.Parameters.AddWithValue("@UserTime", datetime);
                        Cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                        Cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                        
                    Con.Open();
                    ST = Con.BeginTransaction();
                    
                    try
                    {
                        Cmd.Transaction = ST;
                        Cmd.ExecuteNonQuery();
                        string strMessage = Convert.ToString(Cmd.Parameters["@Message"].Value);
                        ST.Commit();
                        return Return.returnHttp("200", strMessage.ToString(), null);
                    }
                    catch (SqlException sqlError)
                    {
                        ST.Rollback();
                        // String strMessageErr = Convert.ToString(sqlError);
                        String strMessageErr = Convert.ToString(Cmd.Parameters["@Message"].Value);
                        Con.Close();
                        return Return.returnHttp("201", strMessageErr, null);

                    }
                    finally
                    {
                        Con.Close();
                    }
                }

                }

                else
                {
                    return Return.returnHttp("201", "This is not Faculty Login, You can not change Fee Category.", null);
                }


            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region GetBankNameList
        [HttpPost]
        public HttpResponseMessage GetBankNameList()
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

                SqlCommand cmd = new SqlCommand("BankListGet", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<BankNameList> ObjBankName = new List<BankNameList>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        BankNameList ObjBank = new BankNameList();

                        ObjBank.Id = Convert.ToInt32(dr["Id"]);
                        ObjBank.BankName = Convert.ToString(dr["BankName"]);
                        ObjBank.BankType = Convert.ToString(dr["BankType"]);
                        ObjBank.IfscInitial = Convert.ToString(dr["IfscInitial"]);

                        ObjBankName.Add(ObjBank);
                    }
                }
                else
                {
                    return Return.returnHttp("201", "No Record Found.", null);
                }

                return Return.returnHttp("200", ObjBankName, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
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

            string abc = Request.Headers.GetValues("PassbookDoc").FirstOrDefault();

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

        #region RTGS Document Upload
        [HttpPost()]
        public HttpResponseMessage UploadRTGSForm()
        {

            int iUploadedCnt = 0;

            // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
            string sPath = "";
            Boolean already_exist = false;
            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/FeeChangeRefundRTGSForm/");


            string token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation tokenOperation = new TokenOperation();
            string responseToken = tokenOperation.ValidateToken(token);
            if (responseToken == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            string abc = Request.Headers.GetValues("RTGSForm").FirstOrDefault();

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
    }

}