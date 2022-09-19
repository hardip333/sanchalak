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
    public class SeatMatrixSpecialCategoryMappingController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region newCode By Stored Procedure - Megha

        [HttpPost]
        public HttpResponseMessage SeatMatrixSpecialCategoryMappingAdd(SeatMatrixSpecialCategoryMapping ObjSeatMatrixSpecialCategoryMapping)
        {
            //String token = Request.Headers.GetValues("token").FirstOrDefault();
            //TokenOperation ac = new TokenOperation();

            //string res = ac.ValidateToken(token);

            //if (res == "0")
            //{
            //    return Return.returnHttp("0", null, null);
            //}
            if (String.IsNullOrWhiteSpace(ObjSeatMatrixSpecialCategoryMapping.SeatMatrixId.ToString()))
            {
                return Return.returnHttp("201", "Please select seat matrix", null);
            }
            //else if (String.IsNullOrWhiteSpace(ObjSeatMatrixSpecialCategoryMapping.SpecialCategoryFieldName))
            //{
            //    return Return.returnHttp("201", "Please enter SpecialCategoryFieldName", null);
            //}
            else if (String.IsNullOrEmpty(Convert.ToString(ObjSeatMatrixSpecialCategoryMapping.SocialCategoryId)))
            {
                return Return.returnHttp("201", "Please select social category", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjSeatMatrixSpecialCategoryMapping.ReservationPercentage)))
            {
                return Return.returnHttp("201", "Please enter reservation Percentage", null);
            }
            else
            {
                try
                {
                    //Int64 UserId = Convert.ToInt64(res.ToString());
                     Int64 UserId = 1;


                    SqlCommand cmd = new SqlCommand("M_SeatMatrixSpecialCategoryMappingAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SeatMatrixId", ObjSeatMatrixSpecialCategoryMapping.SeatMatrixId);
                    cmd.Parameters.AddWithValue("@SpecialCategoryFieldName", ObjSeatMatrixSpecialCategoryMapping.SpecialCategoryFieldName);
                    cmd.Parameters.AddWithValue("@SocialCategoryId", ObjSeatMatrixSpecialCategoryMapping.SocialCategoryId);
                    cmd.Parameters.AddWithValue("@ReservationPercentage", ObjSeatMatrixSpecialCategoryMapping.ReservationPercentage);
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
        public HttpResponseMessage SeatMatrixSpecialCategoryMappingEdit(SeatMatrixSpecialCategoryMapping ObjSeatMatrixSpecialCategoryMapping)
        {
            //String token = Request.Headers.GetValues("token").FirstOrDefault();
            //TokenOperation ac = new TokenOperation();

            //string res = ac.ValidateToken(token);

            //if (res == "0")
            //{
            //    return Return.returnHttp("0", null, null);
            //}
            if (String.IsNullOrWhiteSpace(ObjSeatMatrixSpecialCategoryMapping.SeatMatrixId.ToString()))
            {
                return Return.returnHttp("201", "Please select seat matrix", null);
            }
            //else if (String.IsNullOrWhiteSpace(ObjSeatMatrixSpecialCategoryMapping.SpecialCategoryFieldName))
            //{
            //    return Return.returnHttp("201", "Please enter SpecialCategoryFieldName", null);
            //}
            else if (String.IsNullOrEmpty(Convert.ToString(ObjSeatMatrixSpecialCategoryMapping.SocialCategoryId)))
            {
                return Return.returnHttp("201", "Please select social category", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjSeatMatrixSpecialCategoryMapping.ReservationPercentage)))
            {
                return Return.returnHttp("201", "Please enter reservation Percentage", null);
            }
            else
            {
                try
                {
                    //Int64 UserId = Convert.ToInt64(res.ToString());
                    Int64 UserId = 1;

                    int Id = Convert.ToInt32(ObjSeatMatrixSpecialCategoryMapping.Id);

                    SqlCommand cmd = new SqlCommand("M_SeatMatrixSpecialCategoryMappingEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@SeatMatrixId", ObjSeatMatrixSpecialCategoryMapping.SeatMatrixId);
                    cmd.Parameters.AddWithValue("@SpecialCategoryFieldName", ObjSeatMatrixSpecialCategoryMapping.SpecialCategoryFieldName);
                    cmd.Parameters.AddWithValue("@SocialCategoryId", ObjSeatMatrixSpecialCategoryMapping.SocialCategoryId);
                    cmd.Parameters.AddWithValue("@ReservationPercentage", ObjSeatMatrixSpecialCategoryMapping.ReservationPercentage);
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
        public HttpResponseMessage SeatMatrixSpecialCategoryMappingListGet(SeatMatrixSpecialCategoryMapping dataString)
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

                List<SeatMatrixSpecialCategoryMapping> SeatMatrixSpecialCategoryMappingList = new List<SeatMatrixSpecialCategoryMapping>();
                BALSeatMatrixSpecialCategoryMapping func = new BALSeatMatrixSpecialCategoryMapping();
                //flag=1,IsDeleted=0,IsActive=null
                SeatMatrixSpecialCategoryMappingList = func.getSeatMatrixSpecialCategoryMappingList("admin", 0, null,dataString.SeatMatrixId);

                return Return.returnHttp("200", SeatMatrixSpecialCategoryMappingList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage SeatMatrixSpecialCategoryMappingListGetActive(SeatMatrixSpecialCategoryMapping dataString)
        {
            try
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
                Int64 UserId = Convert.ToInt64(res.ToString());

                List<SeatMatrixSpecialCategoryMapping> SeatMatrixSpecialCategoryMappingList = new List<SeatMatrixSpecialCategoryMapping>();
                BALSeatMatrixSpecialCategoryMapping func = new BALSeatMatrixSpecialCategoryMapping();
                //flag=1,IsDeleted=0,IsActive=1
                SeatMatrixSpecialCategoryMappingList = func.getSeatMatrixSpecialCategoryMappingList("admin", 0, 1,dataString.SeatMatrixId);

                return Return.returnHttp("200", SeatMatrixSpecialCategoryMappingList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }


        /// SeatMatrixSpecialCategoryMappingIsActive and SeatMatrixSpecialCategoryMappingIsInactive Created by Jaydev on 24-11-21

        [HttpPost]
        public HttpResponseMessage SeatMatrixSpecialCategoryMappingIsActive(SeatMatrixSpecialCategoryMapping dataString)
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

                int? Id = dataString.Id;
                //Int64 UserId = Convert.ToInt64(res.ToString());
                 Int64 UserId = 1;

                SqlCommand cmd = new SqlCommand("M_SeatMatrixSpecialCategoryMappingActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "M_SeatMatrixSpecialCategoryMappingIsActive");
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);

                cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@MESSAGE"].Value);
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
        public HttpResponseMessage SeatMatrixSpecialCategoryMappingIsInactive(SeatMatrixSpecialCategoryMapping dataString)
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

                int? Id = dataString.Id;
               // Int64 UserId = Convert.ToInt64(res.ToString());
                Int64 UserId = 1;

                SqlCommand cmd = new SqlCommand("M_SeatMatrixSpecialCategoryMappingActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "M_SeatMatrixSpecialCategoryMappingIsInactive");
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);

                cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@MESSAGE"].Value);
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
        public HttpResponseMessage SeatMatrixSpecialCategoryMappingDelete(SeatMatrixSpecialCategoryMapping dataString)
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

                Int32 Id = (int)dataString.Id;
                 Int64 UserId = 1;//Convert.ToInt64(res.ToString());
                //Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("M_SeatMatrixSpecialCategoryMappingDelete", con);
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
                    strMessage = "Seat Matrix Special Category  Mapping has been deleted Successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion
    }
}