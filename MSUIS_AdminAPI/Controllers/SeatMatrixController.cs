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
    public class SeatMatrixController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region newCode By Stored Procedure - Megha

        [HttpPost]
        public HttpResponseMessage SeatMatrixAdd(SeatMatrix ObjSeatMatrix)
        {
            //String token = Request.Headers.GetValues("token").FirstOrDefault();
            //TokenOperation ac = new TokenOperation();

            //string res = ac.ValidateToken(token);

            //if (res == "0")
            //{
            //    return Return.returnHttp("0", null, null);
            //}
            if (String.IsNullOrWhiteSpace(ObjSeatMatrix.MeritListInstanceId.ToString()))
            {
                return Return.returnHttp("201", "Please select merit list instance", null);
            }
            else if (String.IsNullOrWhiteSpace(ObjSeatMatrix.CategoryId.ToString()))
            {
                return Return.returnHttp("201", "Please  select category", null);
            }
            else
            {
                try
                {
                    // Int64 UserId = Convert.ToInt64(res.ToString());
                    Int64 UserId = 1;

                    //string DisplayName = ObjExamCenter.DisplayName;
                    //string Code = ObjExamCenter.Code;

                    int AvailableSeats = Convert.ToInt32(ObjSeatMatrix.AvailableSeats);

                    SqlCommand cmd = new SqlCommand("M_SeatMatrixAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MeritListInstanceId", ObjSeatMatrix.MeritListInstanceId);
                    cmd.Parameters.AddWithValue("@CategoryId", ObjSeatMatrix.CategoryId);
                    cmd.Parameters.AddWithValue("@Location", ObjSeatMatrix.Location);
                    cmd.Parameters.AddWithValue("@AvailableSeats", ObjSeatMatrix.TotalSeats);
                    cmd.Parameters.AddWithValue("@TotalSeats", ObjSeatMatrix.TotalSeats);
                    cmd.Parameters.AddWithValue("@PreferenceId", ObjSeatMatrix.PreferenceId);
                    cmd.Parameters.AddWithValue("@StrGender", ObjSeatMatrix.StrGender);
                    //cmd.Parameters.AddWithValue("@EWSReservationPercentage", ObjSeatMatrix.EWSReservationPercentage);
                    //cmd.Parameters.AddWithValue("@PhysicallyHandicapReservationPercentage", ObjSeatMatrix.PhysicallyHandicapReservationPercentage);
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
        public HttpResponseMessage SeatMatrixAddWithList(SeatMatrix ObjSeatMatrix)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            if (String.IsNullOrWhiteSpace(ObjSeatMatrix.MeritListInstanceId.ToString()))
            {
                return Return.returnHttp("201", "Please select merit list instance", null);
            }
            else if (String.IsNullOrWhiteSpace(ObjSeatMatrix.CategoryId.ToString()))
            {
                return Return.returnHttp("201", "Please  select category", null);
            }
            //else if (ObjSeatMatrix.SeatMatrixCatMapList ==null)
            //{
            //    return Return.returnHttp("201", "Please insert SeatMatrix Special Category Mapping List", null);
            //}
            //else if (ObjSeatMatrix.SeatMatrixAllowedCatList == null)
            //{
            //    return Return.returnHttp("201", "Please insert SeatMatrix Allowed Category Mapping List", null);
            //}
            else
            {
                try
                {
                    BALSeatMatrix func = new BALSeatMatrix();
                    ObjSeatMatrix.CreatedBy = 1;
                    string strMessage = func.saveSeatMatrixInBulk(ObjSeatMatrix);
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
        public HttpResponseMessage SeatMatrixEdit(SeatMatrix ObjSeatMatrix)
        {
            //String token = Request.Headers.GetValues("token").FirstOrDefault();
            //TokenOperation ac = new TokenOperation();

            //string res = ac.ValidateToken(token);

            //if (res == "0")
            //{
            //    return Return.returnHttp("0", null, null);
            //}
            if (String.IsNullOrWhiteSpace(ObjSeatMatrix.MeritListInstanceId.ToString()))
            {
                return Return.returnHttp("201", "Please select merit list instance", null);
            }
            else if (String.IsNullOrWhiteSpace(ObjSeatMatrix.CategoryId.ToString()))
            {
                return Return.returnHttp("201", "Please  select category", null);
            }
            else
            {
                try
                {
                    //Int64 UserId = Convert.ToInt64(res.ToString());
                    Int64 UserId = 1;

                    int AvailableSeats = Convert.ToInt32(ObjSeatMatrix.AvailableSeats);

                    SqlCommand cmd = new SqlCommand("M_SeatMatrixEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", ObjSeatMatrix.Id);
                    cmd.Parameters.AddWithValue("@MeritListInstanceId", ObjSeatMatrix.MeritListInstanceId);
                    cmd.Parameters.AddWithValue("@CategoryId", ObjSeatMatrix.CategoryId);
                    cmd.Parameters.AddWithValue("@Location", ObjSeatMatrix.Location);
                    cmd.Parameters.AddWithValue("@AvailableSeats", ObjSeatMatrix.TotalSeats);
                    cmd.Parameters.AddWithValue("@TotalSeats", ObjSeatMatrix.TotalSeats);
                    cmd.Parameters.AddWithValue("@PreferenceId", ObjSeatMatrix.PreferenceId);
                    //cmd.Parameters.AddWithValue("@EWSReservationPercentage", ObjSeatMatrix.EWSReservationPercentage);
                    //cmd.Parameters.AddWithValue("@PhysicallyHandicapReservationPercentage", ObjSeatMatrix.PhysicallyHandicapReservationPercentage);
                    cmd.Parameters.AddWithValue("@StrGender", ObjSeatMatrix.StrGender);
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
        public HttpResponseMessage SeatMatrixListGet(SeatMatrix dataString)
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

                List<SeatMatrix> SeatMatrixList = new List<SeatMatrix>();
                BALSeatMatrix func = new BALSeatMatrix();
                //flag=1,IsDeleted=0,IsActive=null
                SeatMatrixList = func.getSeatMatrixList("admin", 0, null,dataString.MeritListInstanceId);

                return Return.returnHttp("200", SeatMatrixList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage SeatMatrixListGetActive(SeatMatrix dataString)
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

                List<SeatMatrix> SeatMatrixList = new List<SeatMatrix>();
                BALSeatMatrix func = new BALSeatMatrix();
                //flag=1,IsDeleted=0,IsActive=1
                SeatMatrixList = func.getSeatMatrixList("admin", 0, 1,dataString.MeritListInstanceId);

                return Return.returnHttp("200", SeatMatrixList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage SeatMatrixIsActive(SeatMatrix dataString)
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

                SqlCommand cmd = new SqlCommand("M_SeatMatrixActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "M_SeatMatrixIsActive");
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
        public HttpResponseMessage SeatMatrixIsInactive(SeatMatrix dataString)
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

                SqlCommand cmd = new SqlCommand("M_SeatMatrixActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "M_SeatMatrixIsInactive");
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
        public HttpResponseMessage SeatMatrixDelete(SeatMatrix dataString)
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

                SqlCommand cmd = new SqlCommand("M_SeatMatrixDelete", con);
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
                    strMessage = "Seat Matrix has been deleted Successfully.";
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