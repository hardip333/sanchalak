using MSUIS_TokenManager.App_Start;
using MSUISApi.BAL;
using MSUISApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MSUISApi.Controllers
{
    public class ProvisionalMeritListManualParameterValuesController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region newCode By Stored Procedure - Megha

        [HttpPost]
        public HttpResponseMessage ProvisionalMeritListManualParameterValuesAdd(ProvisionalMeritListManualParameterValues ObjProvisionalMeritListManualParameterValues)
        {
            //String token = Request.Headers.GetValues("token").FirstOrDefault();
            //TokenOperation ac = new TokenOperation();

            //string res = ac.ValidateToken(token);

            //if (res == "0")
            //{
            //    return Return.returnHttp("0", null, null);
            //}
            if (String.IsNullOrWhiteSpace(ObjProvisionalMeritListManualParameterValues.Value.ToString()))
            {
                return Return.returnHttp("201", "Please enter value", null);
            }
            else if (String.IsNullOrWhiteSpace(ObjProvisionalMeritListManualParameterValues.MeritListInstanceId.ToString()))
            {
                return Return.returnHttp("201", "Please select merit list", null);
            }
            else if (String.IsNullOrWhiteSpace(ObjProvisionalMeritListManualParameterValues.MeritListManualParameterId.ToString()))
            {
                return Return.returnHttp("201", "Please select merit list manual parameter", null);
            }
            else if (String.IsNullOrWhiteSpace(ObjProvisionalMeritListManualParameterValues.ProvisionalMeritListId.ToString()))
            {
                return Return.returnHttp("201", "Please select provisional merit list", null);
            }
            else
            {
                try
                {
                    //Int64 UserId = Convert.ToInt64(res.ToString());
                    Int64 UserId = 1;

                    SqlCommand cmd = new SqlCommand("M_ProvisionalMeritListManualParameterValuesAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MeritListInstanceId", ObjProvisionalMeritListManualParameterValues.MeritListInstanceId);
                    cmd.Parameters.AddWithValue("@ProvisionalMeritListId", ObjProvisionalMeritListManualParameterValues.ProvisionalMeritListId);
                    cmd.Parameters.AddWithValue("@MeritListManualParameterId", ObjProvisionalMeritListManualParameterValues.MeritListManualParameterId);
                    cmd.Parameters.AddWithValue("@Value", ObjProvisionalMeritListManualParameterValues.Value);
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
                        strMessage = "Your data has been added successfully.";
                    }
                    return Return.returnHttp("200", strMessage.ToString(), null);
                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message, null);
                }
            }

        }

        [HttpPost]
        public HttpResponseMessage ProvisionalMeritListManualParameterValuesEdit(ProvisionalMeritListManualParameterValues ObjProvisionalMeritListManualParameterValues)
        {
            //String token = Request.Headers.GetValues("token").FirstOrDefault();
            //TokenOperation ac = new TokenOperation();

            //string res = ac.ValidateToken(token);

            //if (res == "0")
            //{
            //    return Return.returnHttp("0", null, null);
            //}
            if (String.IsNullOrWhiteSpace(ObjProvisionalMeritListManualParameterValues.Value.ToString()))
            {
                return Return.returnHttp("201", "Please enter value", null);
            }
            else
            {
                try
                {
                    // Int64 UserId = Convert.ToInt64(res.ToString());
                    Int64 UserId = 1;

                    int? Id = Convert.ToInt16(ObjProvisionalMeritListManualParameterValues.Id);

                    SqlCommand cmd = new SqlCommand("M_ProvisionalMeritListManualParameterValuesEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@MeritListInstanceId", ObjProvisionalMeritListManualParameterValues.MeritListInstanceId);
                    cmd.Parameters.AddWithValue("@ProvisionalMeritListId", ObjProvisionalMeritListManualParameterValues.ProvisionalMeritListId);
                    cmd.Parameters.AddWithValue("@MeritListManualParaId", ObjProvisionalMeritListManualParameterValues.MeritListManualParameterId);
                    cmd.Parameters.AddWithValue("@Value", ObjProvisionalMeritListManualParameterValues.Value);
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
                        strMessage = "Your data has been modified successfully.";
                    }
                    return Return.returnHttp("200", strMessage.ToString(), null);
                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message, null);
                }
            }

        }

        [HttpPost]
        public HttpResponseMessage ProvisionalMeritListManualParameterValuesUpdate(updateProvisionalManualParameterValues Obj)
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
                // Int64 UserId = Convert.ToInt64(res.ToString());
                Int64 UserId = 1;
                string errorLines = "";

                string MeritListInstanceId = Convert.ToString(Obj.MeritListInstanceId);

                foreach (var element in Obj.data)
                {
                    dynamic Jsondata = element;

                    string ApplicationFormNo = Jsondata["ApplicationFormNo"];
                    for (int i = 1; i < Obj.HeadersName.Length; i++)
                    {
                        string z = Obj.HeadersName[i];
                        float y = (float)Convert.ToDecimal(Jsondata[Obj.HeadersName[i]]);

                        //string ManualParameterName = Convert.ToString(Jsondata.ManualParameterName);
                        //string Value = Convert.ToString(Jsondata.Value);

                        SqlCommand cmd = new SqlCommand("M_ProvisionalMeritListManualParameterValuesUpdate", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@MeritListInstanceId", MeritListInstanceId);
                        cmd.Parameters.AddWithValue("@ApplicationFormNo", ApplicationFormNo);
                        cmd.Parameters.AddWithValue("@ManualParameterName", z);
                        cmd.Parameters.AddWithValue("@Value", y);
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@UserTime", datetime);


                        cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                        cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                        con.Open();
                        cmd.ExecuteNonQuery();
                        string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                        con.Close();

                        if (!string.Equals(strMessage, "TRUE"))
                        {
                            errorLines = errorLines + strMessage + ",";
                        }
                    }

                }

                if (errorLines.Length > 0)
                    errorLines = errorLines.Substring(0, errorLines.Length - 1);
                if (errorLines == "")
                    return Return.returnHttp("200", "Your data has been modified successfully.", null);
                else
                    return Return.returnHttp("201", "Error occured at Row Numbers:" + errorLines + "\n Please try again.", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage ProvisionalMeritListManualParameterValuesGet(ProvisionalMeritListManualParameterValues dataString)
        {
            try
            {
                //String token = Request.Headers.GetValues("token").FirstOrDefault();
                //String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                //String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                //TokenOperation ac = new TokenOperation();
                //string res = ac.ValidateToken(token);
                //string resRoleToken = ac.ValidateRoleToken(userRoleToken);

                //if (res == "0")
                //{
                //    return Return.returnHttp("0", null, null);
                //}
                //else if (resRoleToken == "0")
                //{
                //    return Return.returnHttp("0", null, null);
                //}
                //Int64 UserId = Convert.ToInt64(res.ToString());

                List<ProvisionalMeritListManualParameterValues> ObjList = new List<ProvisionalMeritListManualParameterValues>();
                BALProvisionalMeritListManualParameterValues func = new BALProvisionalMeritListManualParameterValues();
                //flag=1,IsDeleted=0,IsActive=null
                ObjList = func.getProvisionalMeritListManualParameterValues("admin", 0, dataString.MeritListInstanceId, dataString.ProvisionalMeritListId);

                return Return.returnHttp("200", ObjList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage ProvisionalMeritListManualParameterValuesDelete(ProvisionalMeritListManualParameterValues dataString)
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
                // Int64 UserId = Convert.ToInt64(res.ToString());
                Int64 UserId = 1;

                int? Id = Convert.ToInt16(dataString.Id);

                SqlCommand cmd = new SqlCommand("M_ProvisionalMeritListManualParameterValuesDelete", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
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
                    strMessage = "Your data has been deleted successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }


        #endregion
    }
}