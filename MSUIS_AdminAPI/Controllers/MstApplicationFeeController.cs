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
    public class MstApplicationFeeController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        [HttpPost]
        public HttpResponseMessage MstApplicationFeeGet()
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

                SqlCommand Cmd = new SqlCommand("MstApplicationFeeGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);


                List<MstApplicationFee> ObjLstAPFee = new List<MstApplicationFee>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstApplicationFee objAPFee = new MstApplicationFee();

                        objAPFee.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objAPFee.FeeCategoryId = Convert.ToInt32(Dt.Rows[i]["FeeCategoryId"]);
                        objAPFee.FeeTypeId = Convert.ToInt32(Dt.Rows[i]["FeeTypeId"]);
                        objAPFee.AcademicYearId = Convert.ToInt32(Dt.Rows[i]["AcademicYearId"]);
                        objAPFee.Amount = Convert.ToDouble(Dt.Rows[i]["Amount"]);
                        objAPFee.FeeCategoryName = Convert.ToString(Dt.Rows[i]["FeeCategoryName"]);
                        objAPFee.FeeTypeName = Convert.ToString(Dt.Rows[i]["FeeTypeName"]);
                        objAPFee.AcademicYearCode = Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);

                        objAPFee.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);

                        ObjLstAPFee.Add(objAPFee);
                    }
                }
                return Return.returnHttp("200", ObjLstAPFee, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage MstApplicationFeeAdd(MstApplicationFee APFee)
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

                if (String.IsNullOrWhiteSpace(Convert.ToString(APFee.Amount))) { return Return.returnHttp("201", "Kindly Enter Amount", null); }


                else
                {

                    Int32 FeeCategoryId = Convert.ToInt32(APFee.FeeCategoryId);
                    Int32 FeeTypeId = Convert.ToInt32(APFee.FeeTypeId);
                    Double Amount = Convert.ToDouble(APFee.Amount);
                    if (Amount > 999999 || Amount < 1) { return Return.returnHttp("200", "Enter Valid Amount", null); }
                    Int32 AcademicYearId = Convert.ToInt32(APFee.AcademicYearId);
                    SqlCommand cmd = new SqlCommand("MstApplicationFeeAdd", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                    cmd.Parameters.AddWithValue("@FeeCategoryId", FeeCategoryId);
                    //cmd.Parameters.AddWithValue("@FeeTypeId", FeeTypeId);
                    cmd.Parameters.AddWithValue("@Amount", Amount);
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
        public HttpResponseMessage MstApplicationFeeEdit(MstApplicationFee APFee)
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

                if (String.IsNullOrWhiteSpace(Convert.ToString(APFee.Amount))) { return Return.returnHttp("201", "Kindly Enter Amount", null); }

                else if (String.IsNullOrEmpty(Convert.ToString(APFee.Id))) { return Return.returnHttp("201", "Please Try Again Server Error", null); }
                else
                {

                    Int32 FeeCategoryId = Convert.ToInt32(APFee.FeeCategoryId);
                    Int32 Id = Convert.ToInt32(APFee.Id);
                    Int32 FeeTypeId = Convert.ToInt32(APFee.FeeTypeId);
                    Double Amount = Convert.ToDouble(APFee.Amount);
                    if (Amount > 999999 || Amount < 1) { return Return.returnHttp("200", "Enter Valid Amount", null); }
                    String FeeCategoryName = Convert.ToString(APFee.FeeCategoryName);
                    String FeeTypeName = Convert.ToString(APFee.FeeTypeName);
                    String AcademicYearCode = Convert.ToString(APFee.AcademicYearCode);
                    Int32 AcademicYearId = Convert.ToInt32(APFee.AcademicYearId);
                    SqlCommand cmd = new SqlCommand("MstApplicationFeeEdit", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@FeeCategoryId", FeeCategoryId);
                    cmd.Parameters.AddWithValue("@FeeTypeId", FeeTypeId);
                    cmd.Parameters.AddWithValue("@Amount", Amount);
                    cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    Con.Open();

                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);

                    Con.Close();
                    return Return.returnHttp("200", "Record Updated Successully", null);
                    //return Return.returnHttp("200", strMessage.ToString(), null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage MstApplicationFeeDelete(MstApplicationFee APFee)
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

                Int32 Id = Convert.ToInt32(APFee.Id);

                SqlCommand cmd = new SqlCommand("MstApplicationFeeDelete", Con);
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
                //return Return.returnHttp("200", "Data Deleted", null);
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }

        public HttpResponseMessage MstApplicationFeeFeeCategoryIdGet(MstApplicationFee APFee)
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
                Int32 AcademicYearId = Convert.ToInt32(APFee.AcademicYearId);

                SqlCommand cmd = new SqlCommand("MstApplicationFeeByFeeCategoryId", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = cmd;


                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);

                Da.Fill(Dt);


                List<MstApplicationFee> ObjLstAPFeeByFCId = new List<MstApplicationFee>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstApplicationFee objAPFeeByFCId = new MstApplicationFee();


                        objAPFeeByFCId.FeeCategoryId = Convert.ToInt32(Dt.Rows[i]["FeeCategoryId"]);
                        objAPFeeByFCId.FeeCategoryName = Convert.ToString(Dt.Rows[i]["FeeCategoryName"]);


                        ObjLstAPFeeByFCId.Add(objAPFeeByFCId);
                    }
                }
                return Return.returnHttp("200", ObjLstAPFeeByFCId, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }


    }
}
