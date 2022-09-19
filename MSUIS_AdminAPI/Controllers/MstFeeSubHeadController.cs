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
    public class MstFeeSubHeadController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        [HttpPost]
        public HttpResponseMessage MstFeeSubHeadGet()
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

                SqlCommand Cmd = new SqlCommand("MstFeeSubHeadGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);


                List<MstFeeSubHead> ObjLstFSH = new List<MstFeeSubHead>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstFeeSubHead objFSH = new MstFeeSubHead();

                        objFSH.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objFSH.FeeSubHeadName = Convert.ToString(Dt.Rows[i]["FeeSubHeadName"]);
                        objFSH.FeeSubHeadCode = Convert.ToString(Dt.Rows[i]["FeeSubHeadCode"]);
                        objFSH.FeeHeadId = Convert.ToInt32(Dt.Rows[i]["FeeHeadId"]);
                        objFSH.FeeSubHeadSrno = Convert.ToInt32(Dt.Rows[i]["FeeSubHeadSrno"]);
                        objFSH.FeeHeadName = Convert.ToString(Dt.Rows[i]["FeeHeadName"]);
                        objFSH.FeeHeadCode = Convert.ToString(Dt.Rows[i]["FeeHeadCode"]);
                        objFSH.IsEditable = (Convert.ToString(Dt.Rows[i]["IsEditable"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsEditable"]);
                        objFSH.AllowsNegativeValue = (Convert.ToString(Dt.Rows[i]["AllowsNegativeValue"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["AllowsNegativeValue"]);
                        objFSH.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);

                        ObjLstFSH.Add(objFSH);
                    }
                }
                return Return.returnHttp("200", ObjLstFSH, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage MstFeeSubHeadAdd(MstFeeSubHead FSH)
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
                if (String.IsNullOrWhiteSpace(Convert.ToString(FSH.FeeSubHeadName))) { return Return.returnHttp("201", "Kindly Enter Fee Sub Head Name", null); }
                //else if (String.IsNullOrWhiteSpace(Convert.ToString(FSH.FeeSubHeadCode))) { return Return.returnHttp("201", "Kindly Enter Fee Sub Head Code", null); }
                else
                {
                    String FeeSubHeadName = Convert.ToString(FSH.FeeSubHeadName);
                    String FeeSubHeadCode = Convert.ToString(FSH.FeeSubHeadCode);
                    Boolean IsEditable = Convert.ToBoolean(FSH.IsEditable);
                    Boolean AllowsNegativeValue = Convert.ToBoolean(FSH.AllowsNegativeValue);
                    Int32 FeeSubHeadSrno = Convert.ToInt32(FSH.FeeSubHeadSrno);
                    Int32 FeeHeadId = Convert.ToInt32(FSH.FeeHeadId);
                    SqlCommand cmd = new SqlCommand("MstFeeSubHeadAdd", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FeeHeadId", FeeHeadId);
                    cmd.Parameters.AddWithValue("@FeeSubHeadName", FeeSubHeadName);
                    cmd.Parameters.AddWithValue("@FeeSubHeadCode", FeeSubHeadName);
                    cmd.Parameters.AddWithValue("@FeeSubHeadSrno", FeeSubHeadSrno);
                    cmd.Parameters.AddWithValue("@IsEditable", IsEditable);
                    cmd.Parameters.AddWithValue("@AllowsNegativeValue", AllowsNegativeValue);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);

                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);

                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    Con.Open();

                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);

                    Con.Close();

                    return Return.returnHttp("200", "Record Added", null);
                    //return Return.returnHttp("200", strMessage.ToString(), null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage MstFeeSubHeadEdit(MstFeeSubHead FSH)
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
                if (String.IsNullOrWhiteSpace(Convert.ToString(FSH.FeeSubHeadName))) { return Return.returnHttp("201", "Kindly Enter Fee Sub Head Name", null); }
                //else if (String.IsNullOrWhiteSpace(Convert.ToString(FSH.FeeSubHeadCode))) { return Return.returnHttp("201", "Kindly Enter Fee Sub Head Code", null); }
                else if (String.IsNullOrEmpty(Convert.ToString(FSH.Id))) { return Return.returnHttp("201", "Please Try Again Server Error", null); }
                else
                {
                    Int32 Id = Convert.ToInt32(FSH.Id);
                    String FeeSubHeadName = Convert.ToString(FSH.FeeSubHeadName);
                    String FeeSubHeadCode = Convert.ToString(FSH.FeeSubHeadCode);
                    Boolean IsEditable = Convert.ToBoolean(FSH.IsEditable);
                    Boolean AllowsNegativeValue = Convert.ToBoolean(FSH.AllowsNegativeValue);
                    Int32 FeeSubHeadSrno = Convert.ToInt32(FSH.FeeSubHeadSrno);
                    Int32 FeeHeadId = Convert.ToInt32(FSH.FeeHeadId);

                    SqlCommand cmd = new SqlCommand("MstFeeSubHeadEdit", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@FeeHeadId", FeeHeadId);
                    cmd.Parameters.AddWithValue("@FeeSubHeadName", FeeSubHeadName);
                    cmd.Parameters.AddWithValue("@FeeSubHeadCode", FeeSubHeadName);
                    cmd.Parameters.AddWithValue("@FeeSubHeadSrno", FeeSubHeadSrno);
                    cmd.Parameters.AddWithValue("@IsEditable", IsEditable);
                    cmd.Parameters.AddWithValue("@AllowsNegativeValue", AllowsNegativeValue);
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
        public HttpResponseMessage MstFeeSubHeadIsActiveEnable(MstFeeSubHead FSH)
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
                Int32 Id = Convert.ToInt32(FSH.Id);

                SqlCommand cmd = new SqlCommand("MstFeeSubHeadActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Flag", "MstFeeSubHeadIsActiveEnable");
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
        public HttpResponseMessage MstFeeSubHeadIsActiveDisable(MstFeeSubHead FSH)
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
                Int32 Id = Convert.ToInt32(FSH.Id);

                SqlCommand cmd = new SqlCommand("MstFeeSubHeadActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Flag", "MstFeeSubHeadIsActiveDisable");
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
        public HttpResponseMessage MstFeeSubHeadDelete(MstFeeSubHead FSH)
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
                Int32 Id = Convert.ToInt32(FSH.Id);

                SqlCommand cmd = new SqlCommand("MstFeeSubHeadDelete", Con);
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
        public HttpResponseMessage MstFeeSubHeadGetbyId(MstFeeSubHead FSH)
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

                Int32 Id = Convert.ToInt32(FSH.Id);

                SqlCommand cmd = new SqlCommand("MstFeeSubHeadGetbyId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<MstFeeSubHead> ObjLstFSH = new List<MstFeeSubHead>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstFeeSubHead objFSH = new MstFeeSubHead();

                        objFSH.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objFSH.FeeSubHeadName = Convert.ToString(Dt.Rows[i]["FeeSubHeadName"]);
                        objFSH.FeeSubHeadCode = Convert.ToString(Dt.Rows[i]["FeeSubHeadCode"]);
                        objFSH.FeeSubHeadSrno = Convert.ToInt32(Dt.Rows[i]["FeeSubHeadSrno"]);
                        objFSH.FeeHeadId = Convert.ToInt32(Dt.Rows[i]["FeeHeadId"]);
                        objFSH.FeeHeadName = Convert.ToString(Dt.Rows[i]["FeeHeadName"]);
                        objFSH.FeeHeadCode = Convert.ToString(Dt.Rows[i]["FeeHeadCode"]);
                        objFSH.IsEditable = (Convert.ToString(Dt.Rows[i]["IsEditable"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsEditable"]);
                        objFSH.AllowsNegativeValue = (Convert.ToString(Dt.Rows[i]["AllowsNegativeValue"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["AllowsNegativeValue"]);
                        objFSH.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjLstFSH.Add(objFSH);
                    }
                }
                return Return.returnHttp("200", ObjLstFSH, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }

        [HttpPost]
        public HttpResponseMessage MstFeeSubHeadGetbyFeeHeadId(MstFeeSubHead FSH)
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

                //Int32 FeeHeadId = Convert.ToInt32(FSH.FeeHeadId);
                string HeadId = Convert.ToString(FSH.FeeHeadList);
                string[] FeeHeadId = HeadId.Split(',');
                List<MstFeeSubHead> ObjLstFSH = new List<MstFeeSubHead>();
                for (int j = 0; j < FeeHeadId.Length; j++)
                {

                    SqlCommand cmd = new SqlCommand("MstFeeSubHeadGetbyFeeHeadId", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FeeHeadId", Convert.ToInt32(FeeHeadId[j]));

                    Da.SelectCommand = cmd;
                    Da.Fill(Dt);

                    //List<MstFeeSubHead> ObjLstFSH = new List<MstFeeSubHead>();

                    if (Dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            MstFeeSubHead objFSH = new MstFeeSubHead();

                            objFSH.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                            objFSH.FeeSubHeadName = Convert.ToString(Dt.Rows[i]["FeeSubHeadName"]);
                            objFSH.FeeSubHeadCode = Convert.ToString(Dt.Rows[i]["FeeSubHeadCode"]);
                            objFSH.FeeSubHeadSrno = Convert.ToInt32(Dt.Rows[i]["FeeSubHeadSrno"]);
                            objFSH.FeeHeadId = Convert.ToInt32(Dt.Rows[i]["FeeHeadId"]);
                            objFSH.FeeHeadName = Convert.ToString(Dt.Rows[i]["FeeHeadName"]);
                            objFSH.FeeHeadCode = Convert.ToString(Dt.Rows[i]["FeeHeadCode"]);
                            objFSH.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                            ObjLstFSH.Add(objFSH);
                        }
                    }

                }

                return Return.returnHttp("200", ObjLstFSH, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
    }
}