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

namespace MSUISApi.Controllers
{
    public class FeeTypeFeeCategoryMapController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        [HttpPost]
        public HttpResponseMessage FeeTypeFeeCategoryMapGet()
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

                SqlCommand cmd = new SqlCommand("FeeTypeFeeCategoryMapGet", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<FeeTypeFeeCategoryMap> ObjLstFTFCM = new List<FeeTypeFeeCategoryMap>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        FeeTypeFeeCategoryMap objFTFCM = new FeeTypeFeeCategoryMap();

                        objFTFCM.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objFTFCM.AcademicYearId = Convert.ToInt32(Dt.Rows[i]["AcademicYearId"]);
                        objFTFCM.AcademicYearCode = Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);
                        objFTFCM.FeeTypeId = Convert.ToInt32(Dt.Rows[i]["FeeTypeId"]);
                        objFTFCM.FeeTypeName = Convert.ToString(Dt.Rows[i]["FeeTypeName"]);
                        objFTFCM.FeeTypeCode = Convert.ToString(Dt.Rows[i]["FeeTypeCode"]);
                        objFTFCM.FeeCategoryId = Convert.ToInt32(Dt.Rows[i]["FeeCategoryId"]);
                        objFTFCM.FeeCategoryName = Convert.ToString(Dt.Rows[i]["FeeCategoryName"]);
                        objFTFCM.FeeCategoryNameOriginal = Convert.ToString(Dt.Rows[i]["FeeCategoryNameOriginal"]);
                        objFTFCM.FeeCategoryCode = Convert.ToString(Dt.Rows[i]["FeeCategoryCode"]);
                        objFTFCM.IsActiveSts = Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        objFTFCM.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objFTFCM.Checked = true;
                        ObjLstFTFCM.Add(objFTFCM);
                    }
                }
                return Return.returnHttp("200", ObjLstFTFCM, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage FeeTypeFeeCategoryMapAdd(FeeTypeFeeCategoryMap FTFCM)
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
                if (String.IsNullOrEmpty(Convert.ToString(FTFCM.FeeTypeId))) { return Return.returnHttp("201", "Please select Fee Type", null); }
                else if (String.IsNullOrEmpty(Convert.ToString(FTFCM.AcademicYearId))) { return Return.returnHttp("201", "Please select Academic Year", null); }
                else
                {
                    Int32 FeeTypeId = Convert.ToInt32(FTFCM.FeeTypeId);
                    Int32 AcademicYearId = Convert.ToInt32(FTFCM.AcademicYearId);
                    var FeeCategoryList = FTFCM.FeeCategoryList;
                    int Count = Convert.ToInt32(FeeCategoryList.Count);
                    String[] strMessage = new String[Count];
                    int i = 0;
                    /* String FeeCategoryName = Convert.ToString(FTFCM.FeeCategoryName);
                     if (FeeCategoryName == null)
                     {
                         FeeCategoryName = "-1";
                     }*/

                    foreach (var FeeCategory in FeeCategoryList)
                    {
                        if (String.IsNullOrEmpty(Convert.ToString(FeeCategory.Key))) { return Return.returnHttp("201", "Please select Fee Category", null); }
                        else
                        {
                            Int32 FeeCategoryId = 0;
                            String Value = Convert.ToString(FeeCategory.Value);
                            if (Value == "true")
                            {
                                FeeCategoryId = Convert.ToInt32(FeeCategory.Key);
                                SqlCommand cmd = new SqlCommand("FeeTypeFeeCategoryMapAdd", Con);
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                                cmd.Parameters.AddWithValue("@FeeTypeId", FeeTypeId);
                                cmd.Parameters.AddWithValue("@FeeCategoryId", FeeCategoryId);
                                cmd.Parameters.AddWithValue("@FeeCategoryName", -1);
                                cmd.Parameters.AddWithValue("@UserId", UserId);
                                cmd.Parameters.AddWithValue("@UserTime", datetime);
                                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                                Con.Open();

                                cmd.ExecuteNonQuery();
                                strMessage[i] = Convert.ToString(cmd.Parameters["@Message"].Value);

                                Con.Close();
                                i++;
                            }
                        }
                    }
                    return Return.returnHttp("200", "Record Added", null);
                    //return Return.returnHttp("200", strMessage, null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage FeeTypeFeeCategoryMapEdit(FeeTypeFeeCategoryMap FTFCM)
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
                if (String.IsNullOrEmpty(Convert.ToString(FTFCM.FeeTypeId))) { return Return.returnHttp("201", "Please select Fee Type", null); }
                else if (String.IsNullOrEmpty(Convert.ToString(FTFCM.AcademicYearId))) { return Return.returnHttp("201", "Please select Academic Year", null); }
                else if (String.IsNullOrEmpty(Convert.ToString(FTFCM.FeeCategoryId))) { return Return.returnHttp("201", "Please select Fee Category", null); }
                else if (String.IsNullOrEmpty(Convert.ToString(FTFCM.Id))) { return Return.returnHttp("201", "Please Try Again Server Error", null); }
                else if (String.IsNullOrWhiteSpace(Convert.ToString(FTFCM.FeeCategoryName))) { return Return.returnHttp("201", "Kindly Enter Fee Category Name", null); }

                else
                {
                    Int64 Id = Convert.ToInt64(FTFCM.Id);
                    Int32 FeeTypeId = Convert.ToInt32(FTFCM.FeeTypeId);
                    Int32 FeeCategoryId = Convert.ToInt32(FTFCM.FeeCategoryId);
                    String FeeCategoryName = Convert.ToString(FTFCM.FeeCategoryName);
                    Int32 AcademicYearId = Convert.ToInt32(FTFCM.AcademicYearId);

                    SqlCommand cmd = new SqlCommand("FeeTypeFeeCategoryMapEdit", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                    cmd.Parameters.AddWithValue("@FeeTypeId", FeeTypeId);
                    cmd.Parameters.AddWithValue("@FeeCategoryId", FeeCategoryId);
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
        public HttpResponseMessage FeeTypeFeeCategoryMapIsActiveEnable(FeeTypeFeeCategoryMap FTFCM)
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
                Int64 Id = Convert.ToInt64(FTFCM.Id);

                SqlCommand cmd = new SqlCommand("FeeTypeFeeCategoryMapActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Flag", "FeeTypeFeeCategoryMapIsActiveEnable");
                cmd.Parameters.AddWithValue("@UserTime", datetime);
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
        public HttpResponseMessage FeeTypeFeeCategoryMapIsActiveDisable(FeeTypeFeeCategoryMap FTFCM)
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
                Int64 Id = Convert.ToInt64(FTFCM.Id);

                SqlCommand cmd = new SqlCommand("FeeTypeFeeCategoryMapActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Flag", "FeeTypeFeeCategoryMapIsActiveDisable");
                cmd.Parameters.AddWithValue("@UserTime", datetime);
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
        public HttpResponseMessage FeeTypeFeeCategoryMapDelete(FeeTypeFeeCategoryMap FTFCM)
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
                Int64 Id = Convert.ToInt64(FTFCM.Id);

                SqlCommand cmd = new SqlCommand("FeeTypeFeeCategoryMapDelete", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
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
        public HttpResponseMessage FeeTypeFeeCategoryMapGetbyId(FeeTypeFeeCategoryMap FTFCM)
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

                Int64 Id = Convert.ToInt64(FTFCM.Id);

                SqlCommand cmd = new SqlCommand("FeeTypeFeeCategoryMapGetbyId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<FeeTypeFeeCategoryMap> ObjLstFTFCM = new List<FeeTypeFeeCategoryMap>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        FeeTypeFeeCategoryMap objFTFCM = new FeeTypeFeeCategoryMap();

                        objFTFCM.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objFTFCM.AcademicYearId = Convert.ToInt32(Dt.Rows[i]["AcademicYearId"]);
                        objFTFCM.AcademicYearCode = Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);
                        objFTFCM.FeeTypeId = Convert.ToInt32(Dt.Rows[i]["FeeTypeId"]);
                        objFTFCM.FeeTypeName = Convert.ToString(Dt.Rows[i]["FeeTypeName"]);
                        objFTFCM.FeeTypeCode = Convert.ToString(Dt.Rows[i]["FeeTypeCode"]);
                        objFTFCM.FeeCategoryId = Convert.ToInt32(Dt.Rows[i]["FeeCategoryId"]);
                        objFTFCM.FeeCategoryName = Convert.ToString(Dt.Rows[i]["FeeCategoryName"]);
                        objFTFCM.FeeCategoryNameOriginal = Convert.ToString(Dt.Rows[i]["FeeCategoryNameOriginal"]);
                        objFTFCM.FeeCategoryCode = Convert.ToString(Dt.Rows[i]["FeeCategoryCode"]);
                        objFTFCM.IsActiveSts = Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        objFTFCM.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjLstFTFCM.Add(objFTFCM);
                    }
                }
                return Return.returnHttp("200", ObjLstFTFCM, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }

        [HttpPost]
        public HttpResponseMessage FeeCategoryGetbyFeeTypeId(FeeTypeFeeCategoryMap FTFCM)
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

                Int32 FeeTypeId = Convert.ToInt32(FTFCM.FeeTypeId);
                Int32 AcademicYearId = Convert.ToInt32(FTFCM.AcademicYearId);

                SqlCommand cmd = new SqlCommand("FeeTypeFeeCategoryMapGetByFeeTypeId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FeeTypeId", FeeTypeId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<FeeTypeFeeCategoryMap> ObjLstFTFCM = new List<FeeTypeFeeCategoryMap>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        FeeTypeFeeCategoryMap objFTFCM = new FeeTypeFeeCategoryMap();

                        objFTFCM.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objFTFCM.AcademicYearId = Convert.ToInt32(Dt.Rows[i]["AcademicYearId"]);
                        objFTFCM.AcademicYearCode = Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);
                        objFTFCM.FeeTypeId = Convert.ToInt32(Dt.Rows[i]["FeeTypeId"]);
                        objFTFCM.FeeTypeName = Convert.ToString(Dt.Rows[i]["FeeTypeName"]);
                        objFTFCM.FeeTypeCode = Convert.ToString(Dt.Rows[i]["FeeTypeCode"]);
                        objFTFCM.FeeCategoryId = Convert.ToInt32(Dt.Rows[i]["FeeCategoryId"]);
                        objFTFCM.FeeCategoryName = Convert.ToString(Dt.Rows[i]["FeeCategoryName"]);
                        objFTFCM.FeeCategoryNameOriginal = Convert.ToString(Dt.Rows[i]["FeeCategoryNameOriginal"]);
                        objFTFCM.FeeCategoryCode = Convert.ToString(Dt.Rows[i]["FeeCategoryCode"]);
                        objFTFCM.IsActiveSts = Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        objFTFCM.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objFTFCM.Checked = true;
                        ObjLstFTFCM.Add(objFTFCM);
                    }
                }
                return Return.returnHttp("200", ObjLstFTFCM, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
    }
}