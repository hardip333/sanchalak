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
    public class FeeTypeFeeHeadMapController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        [HttpPost]
        public HttpResponseMessage FeeTypeFeeHeadMapGet()
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

                SqlCommand cmd = new SqlCommand("FeeTypeFeeHeadMapGet", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<FeeTypeFeeHeadMap> ObjLstFTFHM = new List<FeeTypeFeeHeadMap>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        FeeTypeFeeHeadMap objFTFHM = new FeeTypeFeeHeadMap();

                        objFTFHM.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objFTFHM.AcademicYearId = Convert.ToInt32(Dt.Rows[i]["AcademicYearId"]);
                        objFTFHM.AcademicYearCode = Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);
                        objFTFHM.FeeTypeId = Convert.ToInt32(Dt.Rows[i]["FeeTypeId"]);
                        objFTFHM.FeeTypeName = Convert.ToString(Dt.Rows[i]["FeeTypeName"]);
                        objFTFHM.FeeTypeCode = Convert.ToString(Dt.Rows[i]["FeeTypeCode"]);
                        objFTFHM.FeeHeadId = Convert.ToInt32(Dt.Rows[i]["FeeHeadId"]);
                        objFTFHM.FeeHeadName = Convert.ToString(Dt.Rows[i]["FeeHeadName"]);
                        objFTFHM.FeeSubHeadId = Convert.ToInt32(Dt.Rows[i]["FeeSubHeadId"]);
                        objFTFHM.FeeSubHeadName = Convert.ToString(Dt.Rows[i]["FeeSubHeadName"]);
                        objFTFHM.IsEditable = Convert.ToBoolean(Dt.Rows[i]["IsEditable"]);
                        objFTFHM.IsActiveSts = Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        objFTFHM.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjLstFTFHM.Add(objFTFHM);
                    }
                }
                return Return.returnHttp("200", ObjLstFTFHM, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage FeeTypeFeeHeadMapAdd(FeeTypeFeeHeadMap FTFHM)
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
                if (String.IsNullOrEmpty(Convert.ToString(FTFHM.FeeTypeId))) { return Return.returnHttp("201", "Please select Fee Type", null); }
                else if (String.IsNullOrEmpty(Convert.ToString(FTFHM.AcademicYearId))) { return Return.returnHttp("201", "Please select Academic Year", null); }
                else
                {
                    Int32 FeeTypeId = Convert.ToInt32(FTFHM.FeeTypeId);
                    Int32 AcademicYearId = Convert.ToInt32(FTFHM.AcademicYearId);
                    var FeeSubHeadList = FTFHM.FeeSHList;
                    int CountSubHead = Convert.ToInt32(FeeSubHeadList.Count);

                    String[] strMessage = new String[CountSubHead];
                    int i = 0;
                    /* String FeeCategoryName = Convert.ToString(FTFHM.FeeCategoryName);
                     if (FeeCategoryName == null)
                     {
                         FeeCategoryName = "-1";
                     }*/

                    foreach (MstFeeSubHead FSH in FeeSubHeadList)
                    {

                        if (FSH.AddCheck == true)
                        {

                            SqlCommand cmd = new SqlCommand("FeeTypeFeeHeadMapAdd", Con);
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                            cmd.Parameters.AddWithValue("@FeeTypeId", FeeTypeId);
                            cmd.Parameters.AddWithValue("@FeeHeadId", FSH.FeeHeadId);
                            cmd.Parameters.AddWithValue("@FeeSubHeadId", FSH.Id);
                            //cmd.Parameters.AddWithValue("@IsEditable", FSH.IsEditable);
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
        public HttpResponseMessage FeeTypeFeeHeadMapEdit(FeeTypeFeeHeadMap FTFHM)
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
                if (String.IsNullOrEmpty(Convert.ToString(FTFHM.FeeTypeId))) { return Return.returnHttp("201", "Please select Fee Type", null); }
                else if (String.IsNullOrEmpty(Convert.ToString(FTFHM.AcademicYearId))) { return Return.returnHttp("201", "Please select Academic Year", null); }
                else if (String.IsNullOrEmpty(Convert.ToString(FTFHM.FeeHeadId))) { return Return.returnHttp("201", "Please select Fee Head", null); }
                else if (String.IsNullOrEmpty(Convert.ToString(FTFHM.Id))) { return Return.returnHttp("201", "Please Try Again Server Error", null); }

                else
                {
                    Int64 Id = Convert.ToInt64(FTFHM.Id);
                    Int32 FeeTypeId = Convert.ToInt32(FTFHM.FeeTypeId);
                    Int32 FeeHeadId = Convert.ToInt32(FTFHM.FeeHeadId);
                    Int32 AcademicYearId = Convert.ToInt32(FTFHM.AcademicYearId);

                    SqlCommand cmd = new SqlCommand("FeeTypeFeeHeadMapEdit", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                    cmd.Parameters.AddWithValue("@FeeTypeId", FeeTypeId);
                    cmd.Parameters.AddWithValue("@FeeHeadId", FeeHeadId);
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
        public HttpResponseMessage FeeTypeFeeHeadMapIsActiveEnable(FeeTypeFeeHeadMap FTFHM)
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
                Int64 Id = Convert.ToInt64(FTFHM.Id);

                SqlCommand cmd = new SqlCommand("FeeTypeFeeHeadMapActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Flag", "FeeTypeFeeHeadMapIsActiveEnable");
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
        public HttpResponseMessage FeeTypeFeeHeadMapIsActiveDisable(FeeTypeFeeHeadMap FTFHM)
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
                Int64 Id = Convert.ToInt64(FTFHM.Id);

                SqlCommand cmd = new SqlCommand("FeeTypeFeeHeadMapActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Flag", "FeeTypeFeeHeadMapIsActiveDisable");
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
        public HttpResponseMessage FeeTypeFeeHeadMapDelete(FeeTypeFeeHeadMap FTFHM)
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
                Int64 Id = Convert.ToInt64(FTFHM.Id);

                SqlCommand cmd = new SqlCommand("FeeTypeFeeHeadMapDelete", Con);
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
        public HttpResponseMessage FeeTypeFeeHeadMapGetbyId(FeeTypeFeeHeadMap FTFHM)
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

                Int64 Id = Convert.ToInt64(FTFHM.Id);

                SqlCommand cmd = new SqlCommand("FeeTypeFeeHeadMapGetbyId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<FeeTypeFeeHeadMap> ObjLstFTFHM = new List<FeeTypeFeeHeadMap>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        FeeTypeFeeHeadMap objFTFHM = new FeeTypeFeeHeadMap();

                        objFTFHM.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objFTFHM.AcademicYearId = Convert.ToInt32(Dt.Rows[i]["AcademicYearId"]);
                        objFTFHM.AcademicYearCode = Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);
                        objFTFHM.FeeTypeId = Convert.ToInt32(Dt.Rows[i]["FeeTypeId"]);
                        objFTFHM.FeeTypeName = Convert.ToString(Dt.Rows[i]["FeeTypeName"]);
                        objFTFHM.FeeTypeCode = Convert.ToString(Dt.Rows[i]["FeeTypeCode"]);
                        objFTFHM.FeeHeadId = Convert.ToInt32(Dt.Rows[i]["FeeHeadId"]);
                        objFTFHM.FeeHeadName = Convert.ToString(Dt.Rows[i]["FeeHeadName"]);
                        objFTFHM.FeeSubHeadId = Convert.ToInt32(Dt.Rows[i]["FeeSubHeadId"]);
                        objFTFHM.FeeSubHeadName = Convert.ToString(Dt.Rows[i]["FeeSubHeadName"]);
                        objFTFHM.IsEditable = Convert.ToBoolean(Dt.Rows[i]["IsEditable"]);
                        objFTFHM.IsActiveSts = Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        objFTFHM.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjLstFTFHM.Add(objFTFHM);
                    }
                }
                return Return.returnHttp("200", ObjLstFTFHM, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }

        [HttpPost]
        public HttpResponseMessage FeeHeadGetbyFeeTypeId(FeeTypeFeeHeadMap FTFHM)
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

                Int32 FeeTypeId = Convert.ToInt32(FTFHM.FeeTypeId);
                Int32 AcademicYearId = Convert.ToInt32(FTFHM.AcademicYearId);

                SqlCommand cmd = new SqlCommand("FeeTypeFeeHeadMapGetByFeeTypeId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FeeTypeId", FeeTypeId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<FeeTypeFeeHeadMap> ObjLstFTFHM = new List<FeeTypeFeeHeadMap>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        FeeTypeFeeHeadMap objFTFHM = new FeeTypeFeeHeadMap();

                        objFTFHM.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objFTFHM.AcademicYearId = Convert.ToInt32(Dt.Rows[i]["AcademicYearId"]);
                        objFTFHM.AcademicYearCode = Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);
                        objFTFHM.FeeTypeId = Convert.ToInt32(Dt.Rows[i]["FeeTypeId"]);
                        objFTFHM.FeeTypeName = Convert.ToString(Dt.Rows[i]["FeeTypeName"]);
                        objFTFHM.FeeTypeCode = Convert.ToString(Dt.Rows[i]["FeeTypeCode"]);
                        objFTFHM.FeeHeadId = Convert.ToInt32(Dt.Rows[i]["FeeHeadId"]);
                        objFTFHM.FeeHeadName = Convert.ToString(Dt.Rows[i]["FeeHeadName"]);
                        objFTFHM.FeeSubHeadId = Convert.ToInt32(Dt.Rows[i]["FeeSubHeadId"]);
                        objFTFHM.FeeSubHeadName = Convert.ToString(Dt.Rows[i]["FeeSubHeadName"]);
                        objFTFHM.IsEditable = Convert.ToBoolean(Dt.Rows[i]["IsEditable"]);
                        objFTFHM.IsActiveSts = Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        objFTFHM.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjLstFTFHM.Add(objFTFHM);

                    }
                }
                return Return.returnHttp("200", ObjLstFTFHM, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }

        [HttpPost]
        public HttpResponseMessage FeeHeadGet(FeeTypeFeeHeadMap FTFHM)
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

                Int32 FeeTypeId = Convert.ToInt32(FTFHM.FeeTypeId);
                Int32 AcademicYearId = Convert.ToInt32(FTFHM.AcademicYearId);

                SqlCommand cmd = new SqlCommand("MstFeeHeadGetFTFHM", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FeeTypeId", FeeTypeId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<FeeTypeFeeHeadMap> ObjLstFTFHM = new List<FeeTypeFeeHeadMap>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        FeeTypeFeeHeadMap objFTFHM = new FeeTypeFeeHeadMap();

                        objFTFHM.FeeHeadId = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objFTFHM.FeeHeadName = Convert.ToString(Dt.Rows[i]["FeeHeadName"]);
                        ObjLstFTFHM.Add(objFTFHM);

                    }
                }
                return Return.returnHttp("200", ObjLstFTFHM, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        [HttpPost]
        public HttpResponseMessage FeeSubHeadGet(FeeTypeFeeHeadMap FTFHM)
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
                List<FeeTypeFeeHeadMap> ObjLstFTFHM = new List<FeeTypeFeeHeadMap>();

                Int32 FeeTypeId = Convert.ToInt32(FTFHM.FeeTypeId);
                Int32 AcademicYearId = Convert.ToInt32(FTFHM.AcademicYearId);
                var FeeHeadList = FTFHM.FeeHeadList;
                foreach (var FeeHead in FeeHeadList)
                {
                    if (String.IsNullOrEmpty(Convert.ToString(FeeHead.Key))) { return Return.returnHttp("201", "Please select Fee Category", null); }
                    else
                    {
                        Int32 FeeHeadId = 0;
                        String Value = Convert.ToString(FeeHead.Value);
                        if (Value == "true")
                        {
                            FeeHeadId = Convert.ToInt32(FeeHead.Key);
                            SqlCommand cmd = new SqlCommand("MstFeeSubHeadGetFTFHM", Con);
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@FeeTypeId", FeeTypeId);
                            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                            cmd.Parameters.AddWithValue("@FeeHeadId", FeeHeadId);

                            Da.SelectCommand = cmd;
                            Da.Fill(Dt);


                            if (Dt.Rows.Count > 0)
                            {
                                for (int i = 0; i < Dt.Rows.Count; i++)
                                {
                                    FeeTypeFeeHeadMap objFTFHM = new FeeTypeFeeHeadMap();
                                    objFTFHM.FeeSubHeadId = Convert.ToInt32(Dt.Rows[i]["Id"]);
                                    objFTFHM.FeeHeadId = Convert.ToInt32(Dt.Rows[i]["FeeHeadId"]);
                                    objFTFHM.FeeSubHeadName = Convert.ToString(Dt.Rows[i]["FeeSubHeadName"]);
                                    objFTFHM.SubHeadChecked = Convert.ToBoolean(Dt.Rows[i]["SubHeadChecked"]);
                                    objFTFHM.IsEditable = (Convert.ToString(Dt.Rows[i]["IsEditable"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsEditable"]);

                                    ObjLstFTFHM.Add(objFTFHM);
                                }
                            }
                            Dt.Clear();
                            Da.Dispose();
                        }
                    }
                }
                return Return.returnHttp("200", ObjLstFTFHM, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }

    }
}
