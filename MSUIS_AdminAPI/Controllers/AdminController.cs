using MSUISApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using MSUIS_TokenManager.App_Start;
using MSUISApi.BAL;

namespace MSUISApi.Controllers
{
    public class AdminController : ApiController
    {
        BALCommon common = new BALCommon();

        [HttpPost]
        public HttpResponseMessage login(AdminLogin dataString)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                dynamic Jsondata = dataString;
                string username =  Convert.ToString(Jsondata.username);
                string password = Convert.ToString(Jsondata.password);
               
                AdminLogin obj = new AdminLogin();
                SqlCommand Cmd = new SqlCommand("VerifyUserLogin", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@UserName", username);
                Cmd.Parameters.AddWithValue("@Password", password);
                Da.SelectCommand = Cmd;
                Da.Fill(Dt);
                //string username = dataString.username, password = dataString.password;

                if (username == null || username == "" || username == null || username == "")
                {
                    return Return.returnHttp("201", "Invalid Login details. Please try again.", "");
                }
               

                if (Dt.Rows.Count > 0)
                {
                    obj.id = Convert.ToString(Dt.Rows[0]["Id"]);
                    obj.userTypeName = Convert.ToString(Dt.Rows[0]["UserTypeName"]);
                    //OTP Related Code By Harsh Mistry - 08 March 2022 START
                    //obj.OTP = Convert.ToString(dataString.OTP);
                    
                    //if(obj.OTP is DBNull || obj.OTP==null || obj.OTP=="")
                    //{
                    //    obj.IsOTP = false;
                    //}
                    //else
                    //{
                    //    obj.IsOTP = Convert.ToBoolean(Dt.Rows[0]["IsOTP"]);//.Equals(null) ? false : Convert.ToBoolean(Dt.Rows[0]["IsOTP"]);
                    //}
                    
                    obj.MobileNo = (Convert.ToString(Dt.Rows[0]["MobileNo"]).Equals(null)) ? null : Convert.ToString(Dt.Rows[0]["MobileNo"]);

                    Nullable<bool> OTPBool = Convert.ToBoolean(Dt.Rows[0]["IsOTP"]);
                    if(OTPBool is DBNull || OTPBool == null || OTPBool != true)
                    {
                        OTPBool = false;
                        TokenOperation ac1 = new TokenOperation();
                        string tokeninfo1 = ac1.CreateToken(obj.id);
                        return Return.returnHttp("200", obj, tokeninfo1);
                    }
                    if (OTPBool == true)
                    { 
                        obj.IsOTP = true; 
                        if (obj.MobileNo == null || obj.MobileNo == "")
                        {
                            return Return.returnHttp("201", "Register your mobile number! Contact your administrator.", null);
                        }
                        else
                        {
                            string base64Encoded = Convert.ToString(dataString.OTP);
                            string OTP;
                            byte[] data1 = System.Convert.FromBase64String(base64Encoded);
                            OTP = System.Text.ASCIIEncoding.ASCII.GetString(data1);
                            if (OTP == null || OTP == "")
                            {
                                return Return.returnHttp("201", "Try Again", null);
                            }
                            else
                            {
                                string Message = "Dear, OTP for Sanchalak Login is " + OTP + "\nRegards,\nMSUB.";

                                string SMSResponse = common.SendSMS(obj.MobileNo, Message, "1207161908607186569");
                                //string SMSResponse = "true";
                                if (SMSResponse == "true")
                                {
                                    TokenOperation ac1 = new TokenOperation();
                                    string tokeninfo1 = ac1.CreateToken(obj.id);
                                    return Return.returnHttp("200", obj, tokeninfo1);
                                }
                            }

                        }
                    }
					//OTP Related Code By Harsh Mistry - 08 March 2022 END
                    TokenOperation ac = new TokenOperation();
                    string tokeninfo = ac.CreateToken(obj.id);
                    return Return.returnHttp("200", obj, tokeninfo);


                }
                else
                {
                    return Return.returnHttp("201", "Incorrect Password. Please try again", "");
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", "Please try again!" + e.Message + e.StackTrace,null);
            }
        }
    }
}
