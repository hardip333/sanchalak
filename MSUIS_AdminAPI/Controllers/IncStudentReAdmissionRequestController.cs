﻿using MSUIS_TokenManager.App_Start;
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
    public class IncStudentReAdmissionRequestController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        //SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        #region Stuedent Information Get By PRN
        [HttpPost]
        public HttpResponseMessage StuedentInformationGetByPRN(IncStudentReAdmissionRequest ISRR)
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


                Int64 PRN = Convert.ToInt64(ISRR.PRN);
                Int64 UserId = Convert.ToInt64(res);

                int Count = 0;
                SqlCommand Cmd = new SqlCommand("StudentInfoGetByPRNForReAdmissionRequest", Con);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@UserId", UserId);
                Cmd.Parameters.AddWithValue("@PRN", PRN);
                Cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                Cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Da.SelectCommand = Cmd;
                Da.Fill(Dt);

                List<IncStudentReAdmissionRequest> ObjLstISRR = new List<IncStudentReAdmissionRequest>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        IncStudentReAdmissionRequest objISRR = new IncStudentReAdmissionRequest();

                        objISRR.NameAsPerMarksheet = (Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"]);
                        objISRR.ApplicationId = (((Dt.Rows[i]["ApplicationId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["ApplicationId"]);
                        objISRR.PRN = (((Dt.Rows[i]["PRN"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["PRN"]);
                        objISRR.ProgrammeInstancePartTermId = (((Dt.Rows[i]["ProgrammeInstancePartTermId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["ProgrammeInstancePartTermId"]);               
                        objISRR.ProgrammeInstanceId = (((Dt.Rows[i]["ProgrammeInstanceId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["ProgrammeInstanceId"]);               
                        objISRR.EmailId = (Convert.ToString(Dt.Rows[i]["EmailId"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["EmailId"]);
                        objISRR.MobileNo = (Convert.ToString(Dt.Rows[i]["MobileNo"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["MobileNo"]);
                        objISRR.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objISRR.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["BranchName"]);
                        objISRR.ProgrammeInstanceName = (Convert.ToString(Dt.Rows[i]["ProgrammeInstanceName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeInstanceName"]);
                        objISRR.InstancePartTermName = (Convert.ToString(Dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                        objISRR.FacultyRemark = (Convert.ToString(Dt.Rows[i]["FacultyRemark"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyRemark"]);
                        objISRR.StudentHandWrittenDocument = (Convert.ToString(Dt.Rows[i]["StudentHandWrittenDocument"])).IsEmpty() ? null : Convert.ToString(Dt.Rows[i]["StudentHandWrittenDocument"]);                  
                        objISRR.IncStudentReAdmissionRequestStatus = (Convert.ToString(Dt.Rows[i]["IncStudentReAdmissionRequestStatus"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IncStudentReAdmissionRequestStatus"]);
                        //objISRR.PartName = (Convert.ToString(Dt.Rows[i]["PartName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["PartName"]);
                        //objISRR.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        Count = Count + 1;
                        objISRR.IndexId = Count;
                       
                        ObjLstISRR.Add(objISRR);
                    }





                }
                else
                {
                    string strMessage = Convert.ToString(Cmd.Parameters["@Message"].Value);
                    if (strMessage == "")
                    {
                        return Return.returnHttp("201", "This PRN is not belongs to this Faculty.", null);
                    }
                    else
                    {
                        return Return.returnHttp("201", strMessage.ToString(), null);
                    }
                }


                return Return.returnHttp("200", ObjLstISRR, null);


            }


            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Student ReAdmission Request Add Update
        [HttpPost]
        public HttpResponseMessage IncStudentReAdmissionRequestAddUpdate(IncStudentReAdmissionRequest ISRAR)
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
               
                    TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

                    Int64 PRN = Convert.ToInt64(ISRAR.PRN);
                    Int64 ApplicationId = Convert.ToInt64(ISRAR.ApplicationId);
                    Int64 ProgrammeInstanceId = Convert.ToInt64(ISRAR.ProgrammeInstanceId);
                                               
                    string FacultyRemark = Convert.ToString(ISRAR.FacultyRemark);                 
                    //Boolean IsAdmissionFeePaid = Convert.ToBoolean(AFCC.IsAdmissionFeePaid);                                                   
                    string StudentHandWrittenDocument = Convert.ToString(ISRAR.StudentHandWrittenDocument);
                  
                    Int64 UserId = Convert.ToInt64(res);
                 

                    if (StudentHandWrittenDocument == null)
                    {
                    StudentHandWrittenDocument = null;
                    }
                    else
                    {
                    StudentHandWrittenDocument = res + "_" + StudentHandWrittenDocument;
                    }

                               
                    string sPath = "";
                    sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/HandWrittenDocForReAdmissionRequest/");
                    string abc = null;
                    if (Request.Headers.Contains("HandWrittenDocument"))
                    {
                        abc = Request.Headers.GetValues("HandWrittenDocument").FirstOrDefault();
                    }
                    //else
                    //{
                    //    return Return.returnHttp("201", "Passbook Document File Not Exists.", null);
                    //}



                    if (!File.Exists(sPath + Path.GetFileName(res + "_" + abc)))
                    {
                        return Return.returnHttp("201", "Student Hand Written Document Not Exists.", null);
                    }
                   
                    
                    else if (String.IsNullOrWhiteSpace(Convert.ToString(PRN)))
                    {
                        return Return.returnHttp("201", "PRN Is NULL.", null);
                    }
                    
                    else if (String.IsNullOrEmpty(Convert.ToString(FacultyRemark)))
                   {
                       return Return.returnHttp("201", "Please Add Faculty Remarks.", null);
                    }

                else
                {
                        SqlCommand Cmd = new SqlCommand("IncStudentAddandUpdateForReAdmissionRequest", Con);
                        Cmd.CommandType = CommandType.StoredProcedure;

                        //Cmd.Parameters.AddWithValue("@InstituteId", facultyDepartIntituteId);

                       Cmd.Parameters.AddWithValue("@PRN", PRN);
                       Cmd.Parameters.AddWithValue("@ApplicationId", ApplicationId);
                       Cmd.Parameters.AddWithValue("@ProgrammeInstanceId", ProgrammeInstanceId);
                       //Cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);                                    
                       Cmd.Parameters.AddWithValue("@StudentHandWrittenDocument", StudentHandWrittenDocument);
                       Cmd.Parameters.AddWithValue("@FacultyRemark", FacultyRemark);
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
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
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

      

        #region FileUpload---HandwrittenDocument
        [HttpPost]
        public HttpResponseMessage UploadStudentHandWrittenDocument()
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
                string renameFile = Request.Headers.GetValues("HandWrittenDocument").FirstOrDefault();
                string NewFile = responseToken + "_" + renameFile;
                int iUploadedCnt = 0;
                // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
                string sPath = "";
                sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/HandWrittenDocForReAdmissionRequest/");

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
                    //return Return.returnHttp("200", iUploadedCnt + " Files Uploaded Successfully", null);
                    return Return.returnHttp("200", NewFile, null);
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
