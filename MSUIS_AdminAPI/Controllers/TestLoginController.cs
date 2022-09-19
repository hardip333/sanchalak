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
    public class AndroidAppLoginController : ApiController
    {
        #region Login
        [HttpPost]
        public HttpResponseMessage TestLogin(AndroidAppLogin ApplicantInfoJson)
        {
            try
            {
                TokenOperation ac = new TokenOperation();
                AndroidAppLogin obj = new AndroidAppLogin();
                string UserName = Convert.ToString(ApplicantInfoJson.UserName);
                string Password = Convert.ToString(ApplicantInfoJson.Password);
                Boolean IsAndroid = Convert.ToBoolean(ApplicantInfoJson.IsAndroid);


                if (UserName == null || UserName == "" || Password == null || Password == "")
                {
                    return Return.returnHttp("201", "Please enter your user credentials!", "");
                }
                else if (UserName == null || UserName == "")
                {
                    return Return.returnHttp("201", "Please enter your username.", "");
                }
                else if (Password == null || Password == "")
                {
                    return Return.returnHttp("201", "Please enter your password.", "");
                }
                else
                {
                    //SqlCommand Cmd = new SqlCommand("VerifyApplicant", con);
                    //Cmd.CommandType = CommandType.StoredProcedure;
                    //Cmd.Parameters.AddWithValue("@UserName", UserName);
                    //.Parameters.AddWithValue("@Password", Password);
                    //SqlDataAdapter sda = new SqlDataAdapter();
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("ApplicantRegistrationId", typeof(Int64)));
                    dt.Columns.Add(new DataColumn("UserName", typeof(String)));
                    dt.Columns.Add(new DataColumn("Password", typeof(String)));
                    //sda.SelectCommand = Cmd;
                    //sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {


                        obj.ApplicantRegistrationId = 1202100030;
                        obj.token = ac.CreateToken(Convert.ToString(dt.Rows[0]["Id"]));
                        obj.UserName = "admin@admin";
                        obj.Password = "admin";

                        var modifiedBy = dt.Rows[0]["ModifiedBy"];
                        if (modifiedBy is DBNull)
                        {
                            modifiedBy = 0;
                            obj.ModifiedBy = Convert.ToInt32(modifiedBy);
                        }
                        if(IsAndroid)
                        {
                            return Return.returnHttp("200", "TRUE", obj.token);
                        }
                        else {
                            
                            return Return.returnHttp("200", "TRUE", obj.token);

                        }
                        

                    }
                    else
                    {
                        return Return.returnHttp("201", "Incorrect Password. Please try again", "");
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