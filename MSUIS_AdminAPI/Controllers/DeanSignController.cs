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

namespace MSUISApi.Controllers
{
    public class DeanSignController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlCommand cmd = new SqlCommand();

        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        [HttpGet]
        public HttpResponseMessage DeanSignGet()
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
                List<DeanSign> slist = new List<DeanSign>();

                cmd.CommandText = "DeanSignGet";
                cmd.Parameters.AddWithValue("@UserId", responseToken);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                da.SelectCommand = cmd;
                da.Fill(dt);

                DeanSign s = new DeanSign();

                s.NameOfAuthority = dt.Rows[0]["NameOfAuthority"].ToString();
                s.DSign = dt.Rows[0]["Sign"].ToString();                
                
                return Return.returnHttp("200", s, null);
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
        [HttpPost]
        public HttpResponseMessage DeanSignUpdate(DeanSign deanSign)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                
                //String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                //String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();

                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                //string resRoleToken = ac.ValidateRoleToken(userRoleToken);
                /*if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }*/
                string sPath = "";
                sPath = "F:/inetpub/wwwroot/Vidhyarthi_API/Upload/Signature/";

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (string.IsNullOrEmpty(Convert.ToString(deanSign.NameOfAuthority)) || 
                    string.IsNullOrWhiteSpace(Convert.ToString(deanSign.NameOfAuthority)) ||
                    deanSign.NameOfAuthority == "" || deanSign.NameOfAuthority == null)
                {
                    return Return.returnHttp("201", "Please enter your valid Name", null);
                }
                else if (string.IsNullOrEmpty(Convert.ToString(deanSign.DSign)) ||
                    string.IsNullOrWhiteSpace(Convert.ToString(deanSign.DSign)) ||
                    deanSign.DSign == "" || deanSign.DSign == null)
                {
                    return Return.returnHttp("201", "Please uplaod your valid Signature", null);
                }
                else if (!File.Exists(sPath + Path.GetFileName(deanSign.DSign)))
                {
                    return Return.returnHttp("201", "File has not uplaoded yet", null);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("UpdateDeanSign", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime ModifiedOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), INDIAN_ZONE);

                    cmd.Parameters.AddWithValue("@UserId", res);
                    cmd.Parameters.AddWithValue("@NameOfAuthority", deanSign.NameOfAuthority);
                    cmd.Parameters.AddWithValue("@DeanSign", deanSign.DSign);
                    cmd.Parameters.AddWithValue("@ModifiedOn", ModifiedOn);

                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    if (string.Equals(strMessage, "TRUE"))
                    {
                        return Return.returnHttp("200", "Data has been submitted successfully.", null);
                    }
                    else
                    {
                        return Return.returnHttp("201", "Opps, something went wrong.", null);
                    }
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
        #region GenericConfigurationGetById
        [HttpPost]
        public HttpResponseMessage GenericConfigurationGetById(JObject jid)
        {
            try
            {                
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

        [HttpPost()]
        public HttpResponseMessage UploadFile_Sign()
        {
            try
            {
                int iUploadedCnt = 0;
                string dsFileName = Request.Headers.GetValues("dsFileName").FirstOrDefault();
                // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
                string sPath = "";
                //sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/DeanSign/");
                sPath = "F:/inetpub/wwwroot/Vidhyarthi_API/Upload/Signature/"; //C:/inetpub/wwwroot/Vidhyarthi_API/Upload/Signature/

                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

                // CHECK THE FILE COUNT.
                //for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
                //{
                    System.Web.HttpPostedFile hpf = hfc[0];
                    if (hfc.Count > 0)
                    {
                        // CHECK IF THE SELECTED FILE(S) ALREADY EXISTS IN FOLDER. (AVOID DUPLICATE)
                        //if (!File.Exists(sPath + Path.GetFileName(responseToken + "" + dsFileName)))
                        if (dsFileName == "DeanSign.jpg" || dsFileName == "DeanSign.jpeg" || dsFileName == "DeanSign.png")                        
                        {
                            File.Delete(sPath + Path.GetFileName(responseToken + "_DeanSign.jpg"));
                            File.Delete(sPath + Path.GetFileName(responseToken + "_DeanSign.jpeg"));
                            File.Delete(sPath + Path.GetFileName(responseToken + "_DeanSign.png"));
                        
                            // SAVE THE FILES IN THE FOLDER.                    
                            hpf.SaveAs(sPath + Path.GetFileName(responseToken + "_" + dsFileName));
                            iUploadedCnt = iUploadedCnt + 1;
                        }                                                
                    }
                //}
                // RETURN A MESSAGE (OPTIONAL).
                if (iUploadedCnt > 0)
                {
                    return Return.returnHttp("200", responseToken, null);
                }
                else
                {
                    return Return.returnHttp("201", "Upload Failed", null);
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
    }
}
