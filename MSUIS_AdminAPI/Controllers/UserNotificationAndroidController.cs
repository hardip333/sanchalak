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
using System.Net.Mail;
using System.Web.Http;

namespace MSUISApi.Controllers
{
    public class UserNotificationAndroidController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        #region UserNotificationGet
        [HttpPost]
        public HttpResponseMessage UserNotificationAndroidGet()
        {
            //String token = Request.Headers.GetValues("token").FirstOrDefault();
            //TokenOperation ac = new TokenOperation();

            //string res = ac.ValidateToken(token);

            //if (res == "0")
            //{
            //    return Return.returnHttp("0", null, null);
            //}
            try
            {
                SqlCommand cmd = new SqlCommand("UserNotificationGet", con);

                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<UserNotificationAndroid> ObjUserNotList = new List<UserNotificationAndroid>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        UserNotificationAndroid ObjUN = new UserNotificationAndroid();
                        if (ObjUN.NotificationUserType1 == true && ObjUN.NotificationUserType2 == false && ObjUN.NotificationUserType3 == false)
                        {
                            ObjUN.NotificationUserType = "All";
                        }
                        else if (ObjUN.NotificationUserType2 == true && ObjUN.NotificationUserType3 == false)
                        {
                            ObjUN.NotificationUserType = "Student";
                        }
                        else if (ObjUN.NotificationUserType2 == false && ObjUN.NotificationUserType3 == true)
                        {
                            ObjUN.NotificationUserType = "Faculty";
                        }
                        else if (ObjUN.NotificationUserType2 == true && ObjUN.NotificationUserType3 == true)
                        {
                            ObjUN.NotificationUserType = "Student";
                            ObjUN.NotificationUserType = "Faculty";
                        }
                        else
                        {
                            ObjUN.NotificationUserType = null;
                        }
                        ObjUN.Id = Convert.ToInt64(dr["Id"].ToString());
                        ObjUN.FacultyId = Convert.ToInt32(dr["FacultyId"].ToString());
                        ObjUN.IncInstancePartTermId = Convert.ToInt64(dr["IncInstancePartTermId"].ToString());
                        ObjUN.ProgrammeId = Convert.ToInt32(dr["ProgrammeId"].ToString());
                        ObjUN.NotificationTypeId = Convert.ToInt32(dr["NotificationTypeId"].ToString());
                        ObjUN.FacultyName = dr["FacultyName"].ToString();
                        ObjUN.InstancePartTermName = dr["InstancePartTermName"].ToString();
                        ObjUN.ProgrammeName = dr["ProgrammeName"].ToString();
                        ObjUN.NotificationTypeName = dr["NotificationTypeName"].ToString();
                        ObjUN.NotificationDescription = dr["NotificationDescription"].ToString();
                        ObjUN.NotificationUserType = dr["NotificationUserType"].ToString();
                        ObjUN.NotificationFile = dr["NotificationFile"].ToString();
                        ObjUN.NotificationStartDate = Convert.ToDateTime(dr["NotificationStartDate"]);
                        ObjUN.NotificationEndDate = Convert.ToDateTime(dr["NotificationEndDate"]);
                        ObjUN.NotificationStartDateView = Convert.ToDateTime(dr["NotificationStartDate"]).ToShortDateString();
                        ObjUN.NotificationEndDateView = Convert.ToDateTime(dr["NotificationEndDate"]).ToShortDateString();
                        ObjUN.NotificationFor = dr["NotificationFor"].ToString();
                        ObjUN.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
                        ObjUN.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        //ObjUN.NotificationUserType1 = Convert.ToBoolean(dr["NotificationUserType1"].ToString());
                        //ObjUN.NotificationUserType2 = Convert.ToBoolean(dr["NotificationUserType2"].ToString());
                        //ObjUN.NotificationUserType3 = Convert.ToBoolean(dr["NotificationUserType3"].ToString());

                        ObjUserNotList.Add(ObjUN);

                    }
                }
                return Return.returnHttp("200", ObjUserNotList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion
    }
}