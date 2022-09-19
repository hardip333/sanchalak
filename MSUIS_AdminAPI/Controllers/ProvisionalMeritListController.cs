using MSUIS_TokenManager.App_Start;
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
    public class ProvisionalMeritListController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region newCode By Stored Procedure - Megha
        [HttpPost]
        public HttpResponseMessage ProvisionalMeritListAdd(ProvisionalMeritList ObjProvisionalMeritList)
        {
            // String token = Request.Headers.GetValues("token").FirstOrDefault();
            // TokenOperation ac = new TokenOperation();

            // string res = ac.ValidateToken(token);

            // if (res == "0")
            // {
            //     return Return.returnHttp("0", null, null);
            // }
            if (String.IsNullOrWhiteSpace(Convert.ToString(ObjProvisionalMeritList.MeritListId)))
            {
                return Return.returnHttp("201", "Please select merit list", null);
            }

            else
            {
                try
                {
                    //Int64 UserId = Convert.ToInt64(res.ToString());
                     Int64 UserId = 1;

                    int MeritListId = Convert.ToInt32(ObjProvisionalMeritList.MeritListId);


                    SqlCommand cmd = new SqlCommand("M_ProvisionalMeritListAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MeritListInstanceId", MeritListId);
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
        public HttpResponseMessage ProvisionalMeritListEdit(ProvisionalMeritList ObjProvisionalMeritList)
        {
            //String token = Request.Headers.GetValues("token").FirstOrDefault();
            //TokenOperation ac = new TokenOperation();

            //string res = ac.ValidateToken(token);

            //if (res == "0")
            //{
            //    return Return.returnHttp("0", null, null);
            //}
            if (String.IsNullOrWhiteSpace(Convert.ToString(ObjProvisionalMeritList.MeritListId)))
            {
                return Return.returnHttp("201", "Please select merit list", null);
            }
            else
            {
                try
                {
                    //Int64 UserId = Convert.ToInt64(res.ToString());
                    Int64 UserId = 1;

                    SqlCommand cmd = new SqlCommand("M_ProvisionalMeritListEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", ObjProvisionalMeritList.Id);
                    cmd.Parameters.AddWithValue("@MeritListInstanceId",ObjProvisionalMeritList.MeritListId);
                    cmd.Parameters.AddWithValue("@Username",ObjProvisionalMeritList.UserName);
                    cmd.Parameters.AddWithValue("@ApplicationFormNo",ObjProvisionalMeritList.ApplocationFormNo);
                    cmd.Parameters.AddWithValue("@Gender",ObjProvisionalMeritList.Gender);
                    cmd.Parameters.AddWithValue("@NameAsPerMarksheet",ObjProvisionalMeritList.NameAsPerMarksheet);
                    cmd.Parameters.AddWithValue("@ReservationCategoryId",ObjProvisionalMeritList.ReservationCategoryId);
                    cmd.Parameters.AddWithValue("@IsEWS",ObjProvisionalMeritList.IsEWS);
                    cmd.Parameters.AddWithValue("@SocialCategoryId",ObjProvisionalMeritList.SocialCategoryId);
                    cmd.Parameters.AddWithValue("@IsPhysicallyChallenged",ObjProvisionalMeritList.IsPhysicallyChallanged);
                    cmd.Parameters.AddWithValue("@LocalToVadodara",ObjProvisionalMeritList.LocalToVadodara);
                    cmd.Parameters.AddWithValue("@LastQualifyingDegreeSchool",ObjProvisionalMeritList.LastQualifyingDegreeSchool);
                    cmd.Parameters.AddWithValue("@LastQualifyingDegreeLocation",ObjProvisionalMeritList.LastQualifyingDegreeSchoolCityId);
                    cmd.Parameters.AddWithValue("@HscSchool",ObjProvisionalMeritList.HSCSchool);
                    cmd.Parameters.AddWithValue("@Location",ObjProvisionalMeritList.HSCSchoolCityId);
                    cmd.Parameters.AddWithValue("@Mobile",ObjProvisionalMeritList.Mobile);
                    cmd.Parameters.AddWithValue("@Email", ObjProvisionalMeritList.EmailId);
                    cmd.Parameters.AddWithValue("@Address", ObjProvisionalMeritList.Address);
                    cmd.Parameters.AddWithValue("@StateId", ObjProvisionalMeritList.StateId);

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
        public HttpResponseMessage ProvisionalMeritListGet(ProvisionalMeritList dataString)
        {
            try
            {
                //String token = Request.Headers.GetValues("token").FirstOrDefault();
                //String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                //String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                //Request.Headers.TryGetValues("InstituteId", out IEnumerable<string> values);
                //Int32 InstituteId = Convert.ToInt32(values?.FirstOrDefault());
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
                // Int64 UserId = Convert.ToInt64(res.ToString());
                Int64 UserId = 1;
                 List <ProvisionalMeritList> ProvisionalMeritListList = new List<ProvisionalMeritList>();
                BALProvisionalMeritList func = new BALProvisionalMeritList();
                //flag=1,IsDeleted=0,IsActive=null
                ProvisionalMeritListList = func.getProvisionalMeritList("admin", 0, null, dataString.MeritListId);

                return Return.returnHttp("200", ProvisionalMeritListList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage ProvisionalMeritListGetActive(ProvisionalMeritList dataString)
        {
            try
            {
                //String token = Request.Headers.GetValues("token").FirstOrDefault();
                //String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                //String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                ////Int32 InstituteId = Convert.ToInt32(Request.Headers.GetValues("InstituteId").FirstOrDefault());
                //Request.Headers.TryGetValues("InstituteId", out IEnumerable<string> values);
                //Int32 InstituteId = Convert.ToInt32(values?.FirstOrDefault());
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

                List<ProvisionalMeritList> ProvisionalMeritListList = new List<ProvisionalMeritList>();
                BALProvisionalMeritList func = new BALProvisionalMeritList();
                //flag=1,IsDeleted=0,IsActive=1
                ProvisionalMeritListList = func.getProvisionalMeritList("admin", 0, 1, dataString.MeritListId);

                return Return.returnHttp("200", ProvisionalMeritListList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }


        /// ProvisionalMeritListIsActive and ProvisionalMeritListIsInactive Created by Jaydev on 24-11-21

        [HttpPost]
        public HttpResponseMessage ProvisionalMeritListIsActive(ProvisionalMeritList dataString)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            try
            {

                int? Id = dataString.Id;
                Int64 UserId = Convert.ToInt64(res.ToString());
                //Int64 UserId = 1;

                SqlCommand cmd = new SqlCommand("M_ProvisionalMeritListActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "M_ProvisionalMeritListIsActive");
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
                    strMessage = "Your data has been Active Successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }


        [HttpPost]
        public HttpResponseMessage ProvisionalMeritListIsInactive(ProvisionalMeritList dataString)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            try
            {

                int? Id = dataString.Id;
                //Int64 UserId = Convert.ToInt64(res.ToString());
                Int64 UserId = 1;

                SqlCommand cmd = new SqlCommand("M_ProvisionalMeritListActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "M_ProvisionalMeritListIsInactive");
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
                    strMessage = "Your data has been Inactive Successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }

        [HttpPost]
        public HttpResponseMessage ProvisionalMeritListProcess(MeritListInstance dataString)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            try
            {

                int? Id = dataString.Id;
                Int64 UserId = Convert.ToInt64(res.ToString());
                //Int64 UserId = 1;

                SqlCommand cmd = new SqlCommand("CalCulateAndUpdateWeightageValue", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                //cmd.Parameters.AddWithValue("@Flag", "M_ProvisionalMeritListIsInactive");
                cmd.Parameters.AddWithValue("@MeritListInstanceId", Id);
                //cmd.Parameters.AddWithValue("@UserId", UserId);
                //cmd.Parameters.AddWithValue("@UserTime", datetime);

                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    //strMessage = "Your data has been Inactive Successfully.";

                    cmd = new SqlCommand("M_ProvisionalMeritListFilteration", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@Flag", "M_ProvisionalMeritListIsInactive");
                    cmd.Parameters.AddWithValue("@MeritListInstanceId", Id);
                    //cmd.Parameters.AddWithValue("@UserId", UserId);
                    //cmd.Parameters.AddWithValue("@UserTime", datetime);

                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    con.Close();

                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Your data has been Inactive Successfully.";
                        return Return.returnHttp("200", strMessage.ToString(), null);

                    }
                    else
                    {
                        strMessage = "Weightage calculated successfully, but allocation pending.";

                        return Return.returnHttp("201", strMessage.ToString(), null);
                    }

                }
                else
                {
                    strMessage = "Weightage couldn't be calculated.";

                    return Return.returnHttp("201", strMessage.ToString(), null);
                }

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }

        #endregion
    }
}