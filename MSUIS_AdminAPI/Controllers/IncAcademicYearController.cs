using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using MSUISApi.BAL;
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
    public class IncAcademicYearController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        /*[HttpPost]
        public HttpResponseMessage AcademicYearGet()
        {
            try
            {
                BALAcademicYearList ObjBalAY = new BALAcademicYearList();

                List<AcademicYear> ObjAYList = ObjBalAY.AcademiYearListGet();

                if (ObjAYList != null)
                {
                    return Return.returnHttp("200", ObjAYList, null);
                }
                else
                {
                    return Return.returnHttp("201", "No Record Found", null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }*/

        #region AcademicYearGet
        [HttpPost]
        public HttpResponseMessage AcademicYearGet()
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                List<AcademicYear> ObjListAcadYear = new List<AcademicYear>();
                BALAcademicYear ObjBalAcadYearList = new BALAcademicYear();
                ObjListAcadYear = ObjBalAcadYearList.AcademicYearGet();

                if (ObjListAcadYear != null)
                    return Return.returnHttp("200", ObjListAcadYear, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region AcademicYearGetAll
        [HttpPost]
        public HttpResponseMessage AcademicYearGetAll()
        {
            try
            {
                //String token = Request.Headers.GetValues("token").FirstOrDefault();
                //TokenOperation TokenOp = new TokenOperation();
                //String ValidTok = TokenOp.ValidateToken(token);

                //if (ValidTok == "0")
                //{
                //    return Return.returnHttp("0", null, null);
                //}

                List<AcademicYear> ObjListAcadYear = new List<AcademicYear>();
                BALAcademicYear ObjBalAcadYearList = new BALAcademicYear();
                ObjListAcadYear = ObjBalAcadYearList.AcademicYearGetAll();

                if (ObjListAcadYear != null)
                    return Return.returnHttp("200", ObjListAcadYear, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region AcademicYearGet For DropDown
        [HttpPost]
        public HttpResponseMessage AcademicYearGetForDropDown()
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                List<AcademicYear> ObjListAcadYear = new List<AcademicYear>();
                BALAcademicYear ObjBalAcadYearList = new BALAcademicYear();
                ObjListAcadYear = ObjBalAcadYearList.AcademicYearGetForDropDown();

                if (ObjListAcadYear != null)
                    return Return.returnHttp("200", ObjListAcadYear, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region AcademicYearAdd
        [HttpPost]
        public HttpResponseMessage AcademicYearAdd(AcademicYear ObjAcadYr)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                if (String.IsNullOrWhiteSpace(Convert.ToString(ObjAcadYr.AcademicYearCode)))
                {
                    return Return.returnHttp("201", "Please type Academic Year Code", null);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("IncAcademicYearAdd", con);

                    String AcademicYearCode = Convert.ToString(ObjAcadYr.AcademicYearCode);
                    DateTime FromDate = TimeZoneInfo.ConvertTimeFromUtc(ObjAcadYr.FromDate, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    DateTime ToDate = TimeZoneInfo.ConvertTimeFromUtc(ObjAcadYr.ToDate, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

                    //DateTime FromDate = Convert.ToDateTime(ObjAcadYr.FromDate);
                    //DateTime ToDate = Convert.ToDateTime(ObjAcadYr.ToDate);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@AcademicYearCode", AcademicYearCode);
                    cmd.Parameters.AddWithValue("@FromDate", FromDate);
                    cmd.Parameters.AddWithValue("@ToDate", ToDate);
                    cmd.Parameters.AddWithValue("@IsOpen", ObjAcadYr.IsOpen);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    con.Close();
                    if (strMessage.Equals("TRUE"))
                    {
                        return Return.returnHttp("200", "Your data has been added successfully", null);
                    }
                    else
                    {
                        return Return.returnHttp("201", strMessage.ToString(), null);
                    }
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region AcademicYearUpdate
        [HttpPost]
        public HttpResponseMessage AcademicYearUpdate(AcademicYear ObjAcadYr)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                if (String.IsNullOrWhiteSpace(Convert.ToString(ObjAcadYr.AcademicYearCode)))
                {
                    return Return.returnHttp("201", "Please type Academic Year Code", null);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("IncAcademicYearEdit", con);

                    Int32 Id = Convert.ToInt32(ObjAcadYr.Id);
                    String AcademicYearCode = Convert.ToString(ObjAcadYr.AcademicYearCode);
                    //DateTime FromDate = Convert.ToDateTime(ObjAcadYr.FromDate);
                    DateTime FromDate = TimeZoneInfo.ConvertTimeFromUtc(ObjAcadYr.FromDate, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    //DateTime ToDate = Convert.ToDateTime(ObjAcadYr.ToDate);
                    DateTime ToDate = TimeZoneInfo.ConvertTimeFromUtc(ObjAcadYr.ToDate, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@AcademicYearCode", AcademicYearCode);
                    cmd.Parameters.AddWithValue("@FromDate", FromDate);
                    cmd.Parameters.AddWithValue("@ToDate", ToDate);
                    cmd.Parameters.AddWithValue("@IsOpen", ObjAcadYr.IsOpen);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    con.Close();
                    if (strMessage.Equals("TRUE"))
                    {
                        return Return.returnHttp("200", "Your data has been modified successfully", null);
                    }
                    else
                    {
                        return Return.returnHttp("201", strMessage.ToString(), null);
                    }
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region AcademicYearIsActiveEnable
        [HttpPost]
        public HttpResponseMessage AcademicYearIsActiveEnable(AcademicYear ObjAcadYr)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                SqlCommand cmd = new SqlCommand("IncAcademicYearActive", con);

                AcademicYear ObjAY = new AcademicYear();
                ObjAY.Id = Convert.ToInt32(ObjAcadYr.Id);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", ObjAY.Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.AddWithValue("@Flag", "IncAcademicYearIsActiveEnable");
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();
                if (strMessage.Equals("TRUE"))
                {
                    return Return.returnHttp("200", "Your data has been saved successfully", null);
                }
                else
                {
                    return Return.returnHttp("201", strMessage.ToString(), null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
#endregion

        #region AcademicYearIsActiveDisable
        [HttpPost]
        public HttpResponseMessage AcademicYearIsActiveDisable(AcademicYear ObjAcadYr)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                SqlCommand cmd = new SqlCommand("IncAcademicYearActive", con);

                AcademicYear ObjAY = new AcademicYear();
                ObjAY.Id = Convert.ToInt32(ObjAcadYr.Id);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", ObjAY.Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.AddWithValue("@Flag", "IncAcademicYearIsActiveDisable");
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();
                if (strMessage.Equals("TRUE"))
                {
                    return Return.returnHttp("200", "Your data has been saved successfully", null);
                }
                else
                {
                    return Return.returnHttp("201", strMessage.ToString(), null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
#endregion

        #region AcademicYearDelete
        [HttpPost]
        public HttpResponseMessage AcademicYearDelete(AcademicYear ObjAcadYr)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                SqlCommand cmd = new SqlCommand("IncAcademicYearDelete", con);
                
                ObjAcadYr.Id = Convert.ToInt32(ObjAcadYr.Id);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", ObjAcadYr.Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();
                if (strMessage.Equals("TRUE"))
                {
                    return Return.returnHttp("200", "Your data has been deleted successfully", null);
                }
                else
                {
                    return Return.returnHttp("201", strMessage.ToString(), null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region AcademicYear Get By Id
        [HttpPost]
        public HttpResponseMessage AcademicYearGetbyId(AcademicYear ObjAcadYear)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();

                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                AcademicYear ObjListAcadYear = new AcademicYear();
                BALAcademicYear ObjBalAcadYearList = new BALAcademicYear();
                ObjListAcadYear = ObjBalAcadYearList.AcademicYearGetById(ObjAcadYear.Id);

                if (ObjListAcadYear != null)
                    return Return.returnHttp("200", ObjListAcadYear, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion
    }
}
