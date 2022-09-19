using MSUISApi.BAL;
using MSUISApi.Models;
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
    public class ProvisionalMeritListEducationDetailsController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region newCode By Stored Procedure - Megha

        //[HttpPost]
        //public HttpResponseMessage ProvisionalMeritListEducationDetailsAdd(ProvisionalMeritListEducationDetails ObjProvisionalMeritListEducationDetails)
        //{
        //    //String token = Request.Headers.GetValues("token").FirstOrDefault();
        //    //TokenOperation ac = new TokenOperation();

        //    //string res = ac.ValidateToken(token);

        //    //if (res == "0")
        //    //{
        //    //    return Return.returnHttp("0", null, null);
        //    //}
        //    if (String.IsNullOrWhiteSpace(ObjProvisionalMeritListEducationDetails.Percentage.ToString()))
        //    {
        //        return Return.returnHttp("201", "Please enter percentage", null);
        //    }
        //    else if (String.IsNullOrWhiteSpace(ObjProvisionalMeritListEducationDetails.MeritListInstanceId.ToString()))
        //    {
        //        return Return.returnHttp("201", "Please select merit list", null);
        //    }
        //    else if (String.IsNullOrWhiteSpace(ObjProvisionalMeritListEducationDetails.ProvisionalMeritListId.ToString()))
        //    {
        //        return Return.returnHttp("201", "Please select provisional merit list", null);
        //    }
        //    else
        //    {
        //        try
        //        {
        //            //Int64 UserId = Convert.ToInt64(res.ToString());
        //            Int64 UserId = 1;

        //            SqlCommand cmd = new SqlCommand("M_ProvisionalMeritListEducationDetailsAdd", con);
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            cmd.Parameters.AddWithValue("@MeritListInstanceId", ObjProvisionalMeritListEducationDetails.MeritListInstanceId);
        //            cmd.Parameters.AddWithValue("@ProvisionalMeritListId", ObjProvisionalMeritListEducationDetails.ProvisionalMeritListId);
        //            cmd.Parameters.AddWithValue("@AdmApplicantRegistrationId", ObjProvisionalMeritListEducationDetails.AdmApplicantRegistrationId);
        //            cmd.Parameters.AddWithValue("@Percentage", ObjProvisionalMeritListEducationDetails.Percentage);
        //            cmd.Parameters.AddWithValue("@NoOfTrial", ObjProvisionalMeritListEducationDetails.NoOfTrial);
        //            cmd.Parameters.AddWithValue("@UserId", UserId);
        //            cmd.Parameters.AddWithValue("@UserTime", datetime);


        //            cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
        //            cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

        //            con.Open();
        //            cmd.ExecuteNonQuery();
        //            string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
        //            con.Close();

        //            if (string.Equals(strMessage, "TRUE"))
        //            {
        //                strMessage = "Your data has been added successfully.";
        //            }
        //            return Return.returnHttp("200", strMessage.ToString(), null);
        //        }
        //        catch (Exception e)
        //        {
        //            return Return.returnHttp("201", e.Message, null);
        //        }
        //    }

        //}

        [HttpPost]
        public HttpResponseMessage ProvisionalMeritListEducationDetailsEdit(ProvisionalMeritListEducationDetails ObjProvisionalMeritListEducationDetails)
        {
            //String token = Request.Headers.GetValues("token").FirstOrDefault();
            //TokenOperation ac = new TokenOperation();

            //string res = ac.ValidateToken(token);

            //if (res == "0")
            //{
            //    return Return.returnHttp("0", null, null);
            //}
            if (String.IsNullOrWhiteSpace(ObjProvisionalMeritListEducationDetails.Percentage.ToString()))
            {
                return Return.returnHttp("201", "Please enter percentage", null);
            }
            else
            {
                try
                {
                    // Int64 UserId = Convert.ToInt64(res.ToString());
                    Int64 UserId = 1;

                    int? Id = Convert.ToInt16(ObjProvisionalMeritListEducationDetails.Id);

                    SqlCommand cmd = new SqlCommand("M_ProvisionalMeritListEducationDetailsEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@MeritListInstanceId", ObjProvisionalMeritListEducationDetails.MeritListInstanceId);
                    cmd.Parameters.AddWithValue("@ProvisionalMeritListId", ObjProvisionalMeritListEducationDetails.ProvisionalMeritListId);
                    cmd.Parameters.AddWithValue("@AdmApplicantRegistrationId", ObjProvisionalMeritListEducationDetails.AdmApplicantRegistrationId);
                    cmd.Parameters.AddWithValue("@Percentage", ObjProvisionalMeritListEducationDetails.Percentage);
                    cmd.Parameters.AddWithValue("@NoOfTrial", ObjProvisionalMeritListEducationDetails.NoOfTrial);
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
        public HttpResponseMessage ProvisionalMeritListEducationDetailsGet(ProvisionalMeritListEducationDetails dataString)
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

                List<ProvisionalMeritListEducationDetails> ObjList = new List<ProvisionalMeritListEducationDetails>();
                BALProvisionalMeritListEducationDetails func = new BALProvisionalMeritListEducationDetails();
                //flag=1,IsDeleted=0,IsActive=null
                ObjList = func.getProvisionalMeritListEducationDetails("admin", 0, dataString.MeritListInstanceId, dataString.ProvisionalMeritListId);

                return Return.returnHttp("200", ObjList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage ProvisionalMeritListEducationDetailsDelete(ProvisionalMeritListEducationDetails dataString)
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

                SqlCommand cmd = new SqlCommand("M_ProvisionalMeritListEducationDetailsDelete", con);
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