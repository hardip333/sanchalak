using MSUIS_TokenManager.App_Start;
using PassDec.App_Start;
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
using static MSUISApi.Models.Applicant;


namespace MSUISApi.Controllers
{
    public class VerificationController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();


        #region Retrive decoded password
        [HttpPost]
        public HttpResponseMessage RetriveDecodedPassword(DecodePassword decodePassword)
        {
            try
            {
                if (decodePassword.UserName == null)
                {
                    return Return.returnHttp("201", "Please enter Username / Mobile / Email / FirstName / MiddleName / LastName.", null);
                }
                else
                {
                    if (decodePassword.UserName != null)
                    {
                        String token = Request.Headers.GetValues("token").FirstOrDefault();
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
                        Int64 UserId = Convert.ToInt64(res);
                        PassDecryption passDec = new PassDecryption();
                        List<DecodePassword> RetrivedObject = new List<DecodePassword>();
                        cmd.Parameters.AddWithValue("@UserName", decodePassword.UserName);
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.CommandText = "RetriveApplicantPasswordByUsername";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        da.SelectCommand = cmd;
                        da.Fill(dt);
                        if(dt.Rows.Count > 0)
                        {
                            foreach (DataRow DR in dt.Rows)
                            {
                                DecodePassword RetriveInfo = new DecodePassword();
                                RetriveInfo.UserName = DR["UserName"].ToString();
                                RetriveInfo.FullName = DR["FullName"].ToString();
                                RetriveInfo.Password = DR["Password"].ToString();
                                RetriveInfo.SaltKey = DR["SaltKey"].ToString();
                                RetriveInfo.HashPassword = DR["HashPassword"].ToString();
                              
                              if (RetriveInfo.Password=="******")
                                {
                                    RetriveInfo.RetrivedPassword=RetriveInfo.Password;
                                }
                                else
                                {
                                    RetriveInfo.RetrivedPassword = passDec.DecodePassword(RetriveInfo.Password, RetriveInfo.SaltKey, RetriveInfo.HashPassword);
                                }
                              
                                RetriveInfo.EmailId = DR["EmailId"].ToString();
                                RetriveInfo.MobileNo = DR["MobileNo"].ToString();
                                RetriveInfo.IsDeleted = Convert.ToBoolean(DR["IsDeleted"]);
                                RetrivedObject.Add(RetriveInfo);
                            }
                            return Return.returnHttp("200", RetrivedObject, null);
                        }    
                       else
                        {
                            return Return.returnHttp("201", "No record found for "+decodePassword.UserName, null);
                        }
                       
                    }
                    else
                    {
                        return Return.returnHttp("201", "No record found like " + decodePassword.UserName, null);
                    }

                }

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", "Some Internal Issue Occured. Please try again." + e.Message + e.StackTrace, "");
            }
        }
        #endregion



    }
}
