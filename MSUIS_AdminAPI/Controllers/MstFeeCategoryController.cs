using MSUIS_TokenManager.App_Start;
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
using MSUISApi.BAL;
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class MstFeeCategoryController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        [HttpPost]
        public HttpResponseMessage MstFeeCategoryGet()
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

                SqlCommand Cmd = new SqlCommand("MstFeeCategoryGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);


                List<MstFeeCategory> ObjLstFC = new List<MstFeeCategory>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstFeeCategory objFC = new MstFeeCategory();

                        objFC.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objFC.FeeCategoryName = Convert.ToString(Dt.Rows[i]["FeeCategoryName"]);
                        objFC.FeeCategoryCode = Convert.ToString(Dt.Rows[i]["FeeCategoryCode"]);
                        objFC.FeeCategoryTypeId = Convert.ToInt32(Dt.Rows[i]["FeeCategoryTypeId"]);
                        objFC.FeeCategoryTypeName = Convert.ToString(Dt.Rows[i]["FeeCategoryTypeName"]);
                        objFC.CategoryPeriodName = (Convert.ToString(Dt.Rows[i]["CategoryPeriodName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["CategoryPeriodName"]);
                        objFC.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        //objFC.Checked = false;
                        ObjLstFC.Add(objFC);
                    }
                }
                return Return.returnHttp("200", ObjLstFC, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage MstFeeCategoryAdd(MstFeeCategory FC)
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

                if (String.IsNullOrWhiteSpace(Convert.ToString(FC.FeeCategoryName))) { return Return.returnHttp("201", "Kindly Enter Fee Category Name", null); }
                //else if (String.IsNullOrWhiteSpace(Convert.ToString(FC.FeeCategoryCode))) { return Return.returnHttp("201", "Kindly Enter Fee Category Code", null); }

                else
                {
                    String FeeCategoryName = Convert.ToString(FC.FeeCategoryName);
                    String FeeCategoryCode = Convert.ToString(FC.FeeCategoryCode);
                    String CategoryPeriodId = Convert.ToString(FC.CategoryPeriodId);
                    Int32 FeeCategoryTypeId = Convert.ToInt32(FC.FeeCategoryTypeId);
                    String FeeCategoryTypeName = Convert.ToString(FC.FeeCategoryTypeName);
                    SqlCommand cmd = new SqlCommand("MstFeeCategoryAdd", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FeeCategoryName", FeeCategoryName);
                    cmd.Parameters.AddWithValue("@FeeCategoryCode", FeeCategoryName);
                    cmd.Parameters.AddWithValue("@CategoryPeriodId", CategoryPeriodId);
                    cmd.Parameters.AddWithValue("@FeeCategoryTypeId", FeeCategoryTypeId);

                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);

                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);

                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    Con.Open();

                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);

                    Con.Close();
                    //return Return.returnHttp("200", "Record Added", null);
                    return Return.returnHttp("200", strMessage.ToString(), null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage MstFeeCategoryEdit(MstFeeCategory FC)
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

                if (String.IsNullOrWhiteSpace(Convert.ToString(FC.FeeCategoryName))) { return Return.returnHttp("201", "Kindly Enter Fee Category Name", null); }
                //else if (String.IsNullOrWhiteSpace(Convert.ToString(FC.FeeCategoryCode))) { return Return.returnHttp("201", "Kindly Enter Fee Category Code", null); }
                else if (String.IsNullOrEmpty(Convert.ToString(FC.Id))) { return Return.returnHttp("201", "Please Try Again Server Error", null); }
                else
                {
                    Int32 Id = Convert.ToInt32(FC.Id);
                    String FeeCategoryName = Convert.ToString(FC.FeeCategoryName);
                    String FeeCategoryCode = Convert.ToString(FC.FeeCategoryCode);
                    String CategoryPeriodId = Convert.ToString(FC.CategoryPeriodId);
                    Int32 FeeCategoryTypeId = Convert.ToInt32(FC.FeeCategoryTypeId);
                    String FeeCategoryTypeName = Convert.ToString(FC.FeeCategoryTypeName);

                    SqlCommand cmd = new SqlCommand("MstFeeCategoryEdit", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@FeeCategoryName", FeeCategoryName);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    Con.Open();

                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);

                    Con.Close();
                    return Return.returnHttp("200", "Record Edited", null);
                    //return Return.returnHttp("200", strMessage.ToString(), null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage MstFeeCategoryIsActiveEnable(MstFeeCategory FC)
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

                Int32 Id = Convert.ToInt32(FC.Id);

                SqlCommand cmd = new SqlCommand("MstFeeCategoryActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.AddWithValue("@Flag", "MstFeeCategoryIsActiveEnable");
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                Con.Close();
                return Return.returnHttp("200", "Set To Active", null);
                //return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }

        [HttpPost]
        public HttpResponseMessage MstFeeCategoryIsActiveDisable(MstFeeCategory FC)
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

                Int32 Id = Convert.ToInt32(FC.Id);

                SqlCommand cmd = new SqlCommand("MstFeeCategoryActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.AddWithValue("@Flag", "MstFeeCategoryIsActiveDisable");
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                Con.Close();
                return Return.returnHttp("200", "Set To Disable", null);
                //return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }

        [HttpPost]
        public HttpResponseMessage MstFeeCategoryDelete(MstFeeCategory FC)
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

                Int32 Id = Convert.ToInt32(FC.Id);

                SqlCommand cmd = new SqlCommand("MstFeeCategoryDelete", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);

                Con.Close();
                return Return.returnHttp("200", "Data Deleted", null);
                //return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }

        [HttpPost]
        public HttpResponseMessage MstFeeCategoryGetbyId(MstFeeCategory FC)
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

                Int32 Id = Convert.ToInt32(FC.Id);

                SqlCommand cmd = new SqlCommand("MstFeeCategoryGetbyId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<MstFeeCategory> ObjLstFC = new List<MstFeeCategory>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstFeeCategory objFC = new MstFeeCategory();

                        objFC.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objFC.FeeCategoryName = Convert.ToString(Dt.Rows[i]["FeeCategoryName"]);
                        objFC.FeeCategoryCode = Convert.ToString(Dt.Rows[i]["FeeCategoryCode"]);
                        objFC.CategoryPeriodName = (Convert.ToString(Dt.Rows[i]["CategoryPeriodName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["CategoryPeriodName"]);
                        objFC.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjLstFC.Add(objFC);
                    }
                }
                return Return.returnHttp("200", ObjLstFC, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
    }
}
