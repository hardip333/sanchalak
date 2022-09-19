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
    public class MstFeeHeadController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        [HttpPost]
        public HttpResponseMessage MstFeeHeadGet()
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

                SqlCommand Cmd = new SqlCommand("MstFeeHeadGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);


                List<MstFeeHead> ObjLstFH = new List<MstFeeHead>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstFeeHead objFH = new MstFeeHead();

                        objFH.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objFH.FeeHeadName = Convert.ToString(Dt.Rows[i]["FeeHeadName"]);
                        objFH.FeeHeadCode = Convert.ToString(Dt.Rows[i]["FeeHeadCode"]);
                        objFH.HasSubHead = Convert.ToBoolean(Dt.Rows[i]["HasSubHead"]);
                        objFH.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjLstFH.Add(objFH);
                    }
                }
                return Return.returnHttp("200", ObjLstFH, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage MstFeeHeadAdd(MstFeeHead FH)
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
                if (String.IsNullOrWhiteSpace(Convert.ToString(FH.FeeHeadName))) { return Return.returnHttp("201", "Kindly Enter Fee Head Name", null); }
                //else if (String.IsNullOrWhiteSpace(Convert.ToString(FH.FeeHeadCode))) { return Return.returnHttp("201", "Kindly Enter Fee Head Code", null); }
                else
                {
                    String FeeHeadName = Convert.ToString(FH.FeeHeadName);
                    String FeeHeadCode = Convert.ToString(FH.FeeHeadCode);
                    Boolean HasSubHead = Convert.ToBoolean(FH.HasSubHead);
                    Int32 FeeSubHeadSrno = 0;
                    Boolean IsEditable = false;
                    if (FH.HasSubHead == false)
                    {
                        FeeSubHeadSrno = Convert.ToInt32(FH.FeeSubHeadSrno);
                        IsEditable = Convert.ToBoolean(FH.IsEditable);
                    }


                    SqlCommand cmd = new SqlCommand("MstFeeHeadAdd", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FeeHeadName", FeeHeadName);
                    cmd.Parameters.AddWithValue("@FeeHeadCode", FeeHeadName);
                    cmd.Parameters.AddWithValue("@HasSubHead", HasSubHead);
                    cmd.Parameters.AddWithValue("@IsEditable", IsEditable);
                    cmd.Parameters.AddWithValue("@FeeSubHeadSrno", FeeSubHeadSrno);
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
        public HttpResponseMessage MstFeeHeadEdit(MstFeeHead FH)
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
                if (String.IsNullOrWhiteSpace(Convert.ToString(FH.FeeHeadName))) { return Return.returnHttp("201", "Kindly Enter Fee Head Name", null); }
                //else if (String.IsNullOrWhiteSpace(Convert.ToString(FH.FeeHeadCode))) { return Return.returnHttp("201", "Kindly Enter Fee Head Code", null); }
                else if (String.IsNullOrEmpty(Convert.ToString(FH.Id))) { return Return.returnHttp("201", "Please Try Again Server Error", null); }
                else
                {
                    Int32 Id = Convert.ToInt32(FH.Id);
                    String FeeHeadName = Convert.ToString(FH.FeeHeadName);
                    String FeeHeadCode = Convert.ToString(FH.FeeHeadCode);
                    Boolean HasSubHead = Convert.ToBoolean(FH.HasSubHead);
                    SqlCommand cmd = new SqlCommand("MstFeeHeadEdit", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@FeeHeadName", FeeHeadName);
                    cmd.Parameters.AddWithValue("@FeeHeadCode", FeeHeadName);
                    cmd.Parameters.AddWithValue("@HasSubHead", HasSubHead);
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
        public HttpResponseMessage MstFeeHeadIsActiveEnable(MstFeeHead FH)
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
                Int32 Id = Convert.ToInt32(FH.Id);

                SqlCommand cmd = new SqlCommand("MstFeeHeadActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.AddWithValue("@Flag", "MstFeeHeadIsActiveEnable");
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
        public HttpResponseMessage MstFeeHeadIsActiveDisable(MstFeeHead FH)
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
                Int32 Id = Convert.ToInt32(FH.Id);

                SqlCommand cmd = new SqlCommand("MstFeeHeadActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.AddWithValue("@Flag", "MstFeeHeadIsActiveDisable");
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
        public HttpResponseMessage MstFeeHeadwithFSHGet(MstFeeHead FH)
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

                string FeeHeadList = Convert.ToString(FH.FeeHeadList);
                string[] FeeHeadId = FeeHeadList.Split(',');
                List<MstFeeHead> ObjLstFH = new List<MstFeeHead>();
                for (int i = 0; i < FeeHeadId.Length; i++)
                {

                    SqlCommand cmd = new SqlCommand("MstFeeHeadGetbyId", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(FeeHeadId[i]));

                    Da.SelectCommand = cmd;
                    Da.Fill(Dt);


                    MstFeeHead objFH = new MstFeeHead();
                    if (Dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < Dt.Rows.Count; j++)
                        {
                            //MstFeeHead objFH = new MstFeeHead();

                            objFH.Id = Convert.ToInt32(Dt.Rows[j]["Id"]);
                            objFH.FeeHeadName = Convert.ToString(Dt.Rows[j]["FeeHeadName"]);
                            objFH.FeeHeadCode = Convert.ToString(Dt.Rows[j]["FeeHeadCode"]);
                            objFH.IsActive = Convert.ToBoolean(Dt.Rows[j]["IsActive"]);
                            objFH.HasSubHead = Convert.ToBoolean(Dt.Rows[i]["HasSubHead"]);
                            List<MstFeeSubHead> ObjLstFSH = new List<MstFeeSubHead>();
                            SqlCommand Cmd = new SqlCommand("MstFeeSubHeadGetbyFeeHeadId", Con);
                            Cmd.CommandType = CommandType.StoredProcedure;

                            Cmd.Parameters.AddWithValue("@FeeHeadId", objFH.Id);
                            SqlDataAdapter Da1 = new SqlDataAdapter();
                            DataTable Dt1 = new DataTable();
                            Da1.SelectCommand = Cmd;
                            Da1.Fill(Dt1);

                            // List<MstFeeSubHead> ObjLstFSH = new List<MstFeeSubHead>();

                            if (Dt1.Rows.Count > 0)
                            {
                                for (int k = 0; k < Dt.Rows.Count; k++)
                                {
                                    MstFeeSubHead objFSH = new MstFeeSubHead();

                                    objFSH.Id = Convert.ToInt32(Dt1.Rows[k]["Id"]);
                                    objFSH.FeeSubHeadName = Convert.ToString(Dt1.Rows[k]["FeeSubHeadName"]);
                                    objFSH.FeeSubHeadCode = Convert.ToString(Dt1.Rows[k]["FeeSubHeadCode"]);
                                    objFSH.FeeHeadId = Convert.ToInt32(Dt1.Rows[k]["FeeHeadId"]);
                                    objFSH.FeeHeadName = Convert.ToString(Dt1.Rows[k]["FeeHeadName"]);
                                    objFSH.FeeHeadCode = Convert.ToString(Dt1.Rows[k]["FeeHeadCode"]);
                                    objFSH.IsActive = Convert.ToBoolean(Dt1.Rows[k]["IsActive"]);
                                    ObjLstFSH.Add(objFSH);
                                }
                                //objFH.FSH = ObjLstFSH;
                            }
                            else { }
                            objFH.FeeSubHeadList = ObjLstFSH;
                            ObjLstFH.Add(objFH);

                        }

                    }
                }
                return Return.returnHttp("200", ObjLstFH, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage MstFeeHeadGetbyId(MstFeeHead FH)
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

                var FeeHeadList = FH.FeeHeadList;
                Int32 Id = 0;
                List<MstFeeHead> ObjLstFH = new List<MstFeeHead>();
                foreach (var FeeHead in FeeHeadList)
                {
                    String Value = Convert.ToString(FeeHead.Value);
                    if (Value == "true")
                    {
                        Id = Convert.ToInt32(FeeHead.Key);

                        //else
                        //{
                        //    return Return.returnHttp("200", "Please Select Fee Head", null);
                        //}

                        SqlCommand cmd = new SqlCommand("MstFeeHeadGetbyId", Con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", Id);

                        Da.SelectCommand = cmd;
                        Da.Fill(Dt);
                        //List<MstFeeHead> ObjLstFH = new List<MstFeeHead>();


                        if (Dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < Dt.Rows.Count; i++)
                            {
                                MstFeeHead objFH = new MstFeeHead();

                                objFH.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                                objFH.FeeHeadName = Convert.ToString(Dt.Rows[i]["FeeHeadName"]);
                                objFH.FeeHeadCode = Convert.ToString(Dt.Rows[i]["FeeHeadCode"]);
                                objFH.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                                objFH.HasSubHead = Convert.ToBoolean(Dt.Rows[i]["HasSubHead"]);
                                SqlCommand Cmd = new SqlCommand("MstFeeSubHeadGetbyFeeHeadId", Con);
                                Cmd.CommandType = CommandType.StoredProcedure;

                                Cmd.Parameters.AddWithValue("@FeeHeadId", Convert.ToInt32(objFH.Id));
                                SqlDataAdapter Da1 = new SqlDataAdapter();
                                DataTable Dt1 = new DataTable();
                                Da1.SelectCommand = Cmd;
                                Da1.Fill(Dt1);

                                List<MstFeeSubHead> ObjLstFSH = new List<MstFeeSubHead>();

                                if (Dt1.Rows.Count > 0)
                                {
                                    for (int k = 0; k < Dt1.Rows.Count; k++)
                                    {
                                        MstFeeSubHead objFSH = new MstFeeSubHead();

                                        objFSH.Id = Convert.ToInt32(Dt1.Rows[k]["Id"]);
                                        objFSH.FeeSubHeadName = Convert.ToString(Dt1.Rows[k]["FeeSubHeadName"]);
                                        objFSH.FeeSubHeadCode = Convert.ToString(Dt1.Rows[k]["FeeSubHeadCode"]);
                                        objFSH.FeeHeadId = Convert.ToInt32(Dt1.Rows[k]["FeeHeadId"]);
                                        objFSH.FeeHeadName = Convert.ToString(Dt1.Rows[k]["FeeHeadName"]);
                                        objFSH.FeeHeadCode = Convert.ToString(Dt1.Rows[k]["FeeHeadCode"]);
                                        objFSH.IsActive = Convert.ToBoolean(Dt1.Rows[k]["IsActive"]);
                                        ObjLstFSH.Add(objFSH);
                                    }
                                }
                                objFH.FeeSubHeadList = ObjLstFSH;
                                ObjLstFH.Add(objFH);
                            }
                        }
                        Dt.Clear();
                        Da.Dispose();
                    }


                }
                return Return.returnHttp("200", ObjLstFH, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage MstFeeHeadGetwithoutsubhead()
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

                SqlCommand Cmd = new SqlCommand("MstFeeHeadGetwithoutsubhead", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);


                List<MstFeeHead> ObjLstFH = new List<MstFeeHead>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstFeeHead objFH = new MstFeeHead();

                        objFH.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objFH.FeeHeadName = Convert.ToString(Dt.Rows[i]["FeeHeadName"]);
                        objFH.FeeHeadCode = Convert.ToString(Dt.Rows[i]["FeeHeadCode"]);
                        objFH.HasSubHead = Convert.ToBoolean(Dt.Rows[i]["HasSubHead"]);
                        objFH.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjLstFH.Add(objFH);
                    }
                }
                return Return.returnHttp("200", ObjLstFH, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
    }
}
