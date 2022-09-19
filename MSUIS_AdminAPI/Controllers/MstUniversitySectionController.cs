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
    public class MstUniversitySectionController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();

        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region MstUniversitySectionGet
        [HttpPost]
        public HttpResponseMessage MstUniversitySectionGet()
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


                SqlCommand Cmd = new SqlCommand("MstUniversitySectionGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);


                List<MstUniversitySection> ObjLstUS = new List<MstUniversitySection>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstUniversitySection objUS = new MstUniversitySection();

                        objUS.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objUS.UniversitySectionName = Convert.ToString(Dt.Rows[i]["UniversitySectionName"]);
                        objUS.UniversitySectionCode = Convert.ToString(Dt.Rows[i]["UniversitySectionCode"]);
                        objUS.UniversitySectionAddress = Convert.ToString(Dt.Rows[i]["UniversitySectionAddress"]);
                        objUS.CityName = Convert.ToString(Dt.Rows[i]["CityName"]);
                        objUS.Pincode = Convert.ToInt32(Dt.Rows[i]["Pincode"]);
                        objUS.UniversitySectionContactNo = Convert.ToString(Dt.Rows[i]["UniversitySectionContactNo"]);
                        objUS.UniversitySectionFaxNo = Convert.ToString(Dt.Rows[i]["UniversitySectionFaxNo"]);
                        objUS.UniversitySectionEmail = Convert.ToString(Dt.Rows[i]["UniversitySectionEmail"]);
                        objUS.UniversitySectionUrl = Convert.ToString(Dt.Rows[i]["UniversitySectionUrl"]);
                        objUS.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);

                        ObjLstUS.Add(objUS);
                    }
                }
                return Return.returnHttp("200", ObjLstUS, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region MstUniversitySectionAdd
        [HttpPost]
        public HttpResponseMessage MstUniversitySectionAdd(MstUniversitySection US)
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


                if (String.IsNullOrWhiteSpace(Convert.ToString(US.UniversitySectionName))) { return Return.returnHttp("201", "Kindly Enter University Section Name", null); }
                else if (String.IsNullOrWhiteSpace(Convert.ToString(US.UniversitySectionCode))) { return Return.returnHttp("201", "Kindly Enter University Section Code", null); }
                else
                {
                    String UniversitySectionName = Convert.ToString(US.UniversitySectionName);
                    String UniversitySectionCode = Convert.ToString(US.UniversitySectionCode);
                    String UniversitySectionAddress = Convert.ToString(US.UniversitySectionAddress);
                    String CityName = Convert.ToString(US.CityName);
                    Int32 Pincode = Convert.ToInt32(US.Pincode);
                    String UniversitySectionContactNo = Convert.ToString(US.UniversitySectionContactNo);
                    String UniversitySectionFaxNo = Convert.ToString(US.UniversitySectionFaxNo);
                    String UniversitySectionEmail = Convert.ToString(US.UniversitySectionEmail);
                    String UniversitySectionUrl = Convert.ToString(US.UniversitySectionUrl);
                    Int64 UserId = Convert.ToInt64(res.ToString());


                    SqlCommand cmd = new SqlCommand("MstUniversitySectionAdd", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UniversitySectionName", UniversitySectionName);
                    cmd.Parameters.AddWithValue("@UniversitySectionCode", UniversitySectionCode);
                    cmd.Parameters.AddWithValue("@UniversitySectionAddress", UniversitySectionAddress);
                    cmd.Parameters.AddWithValue("@CityName", CityName);
                    cmd.Parameters.AddWithValue("@Pincode", Pincode);
                    cmd.Parameters.AddWithValue("@UniversitySectionContactNo", UniversitySectionContactNo);
                    cmd.Parameters.AddWithValue("@UniversitySectionFaxNo", UniversitySectionFaxNo);
                    cmd.Parameters.AddWithValue("@UniversitySectionEmail", UniversitySectionEmail);
                    cmd.Parameters.AddWithValue("@UniversitySectionUrl", UniversitySectionUrl);
                    cmd.Parameters.AddWithValue("@UserId", 0);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);

                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    Con.Open();

                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);

                    Con.Close();

                    return Return.returnHttp("200", strMessage.ToString(), null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region MstUniversitySectionEdit
        [HttpPost]
        public HttpResponseMessage MstUniversitySectionEdit(MstUniversitySection US)
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

                if (String.IsNullOrWhiteSpace(Convert.ToString(US.UniversitySectionName))) { return Return.returnHttp("201", "Kindly Enter University Section Name", null); }
                else if (String.IsNullOrWhiteSpace(Convert.ToString(US.UniversitySectionCode))) { return Return.returnHttp("201", "Kindly Enter University Section Code", null); }
                else
                {
                    Int32 Id = Convert.ToInt32(US.Id);
                    String UniversitySectionName = Convert.ToString(US.UniversitySectionName);
                    String UniversitySectionCode = Convert.ToString(US.UniversitySectionCode);
                    String UniversitySectionAddress = Convert.ToString(US.UniversitySectionAddress);
                    String CityName = Convert.ToString(US.CityName);
                    Int32 Pincode = Convert.ToInt32(US.Pincode);
                    String UniversitySectionContactNo = Convert.ToString(US.UniversitySectionContactNo);
                    String UniversitySectionFaxNo = Convert.ToString(US.UniversitySectionFaxNo);
                    String UniversitySectionEmail = Convert.ToString(US.UniversitySectionEmail);
                    String UniversitySectionUrl = Convert.ToString(US.UniversitySectionUrl);
                    Int64 UserId = Convert.ToInt64(res.ToString());


                    SqlCommand cmd = new SqlCommand("MstUniversitySectionEdit", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@UniversitySectionName", UniversitySectionName);
                    cmd.Parameters.AddWithValue("@UniversitySectionCode", UniversitySectionCode);
                    cmd.Parameters.AddWithValue("@UniversitySectionAddress", UniversitySectionAddress);
                    cmd.Parameters.AddWithValue("@CityName", CityName);
                    if (Pincode == 0)
                    {
                        cmd.Parameters.AddWithValue("@Pincode", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Pincode", Pincode);
                    }
                    cmd.Parameters.AddWithValue("@UniversitySectionContactNo", UniversitySectionContactNo);
                    cmd.Parameters.AddWithValue("@UniversitySectionFaxNo", UniversitySectionFaxNo);
                    cmd.Parameters.AddWithValue("@UniversitySectionEmail", UniversitySectionEmail);
                    cmd.Parameters.AddWithValue("@UniversitySectionUrl", UniversitySectionUrl);
                    cmd.Parameters.AddWithValue("@UserId", 0);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);

                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    Con.Open();

                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);

                    Con.Close();

                    return Return.returnHttp("200", strMessage.ToString(), null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region MstUniversitySectionDelete
        [HttpPost]
        public HttpResponseMessage MstUniversitySectionDelete(MstUniversitySection US)
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


                Int32 Id = Convert.ToInt32(US.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("MstUniversitySectionDelete", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId",UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);

                Con.Close();

                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region MstUniversitySectionActive
        [HttpPost]
        public HttpResponseMessage MstUniversitySectionActive(MstUniversitySection US)
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

                Int32 Id = Convert.ToInt32(US.Id);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                SqlCommand cmd = new SqlCommand("MstUniversitySectionActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstUniversitySectionIsActiveEnable");
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);

                cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@MESSAGE"].Value);
                Con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been saved successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region MstUniversitySectionSuspend
        [HttpPost]
        public HttpResponseMessage MstUniversitySectionSuspend(MstUniversitySection US)
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

                Int32 Id = Convert.ToInt32(US.Id);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                SqlCommand cmd = new SqlCommand("MstUniversitySectionActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstUniversitySectionIsActiveDisable");
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);

                cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@MESSAGE"].Value);
                Con.Close();
                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been saved successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region MstUniversitySectionGetbyId
        [HttpPost]
        public HttpResponseMessage MstUniversitySectionGetbyId(MstUniversitySection US)
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

            try
            {
                Int64 UserId = Convert.ToInt64(res.ToString());

                Int32 Id = Convert.ToInt32(US.Id);

                SqlCommand cmd = new SqlCommand("MstUniversitySectionGetbyId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);

                List<MstUniversitySection> ObjLstUS = new List<MstUniversitySection>();
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstUniversitySection objUS = new MstUniversitySection();

                        objUS.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objUS.UniversitySectionName = Convert.ToString(Dt.Rows[i]["UniversitySectionName"]);
                        objUS.UniversitySectionCode = Convert.ToString(Dt.Rows[i]["UniversitySectionCode"]);
                        objUS.UniversitySectionAddress = Convert.ToString(Dt.Rows[i]["UniversitySectionAddress"]);
                        objUS.CityName = Convert.ToString(Dt.Rows[i]["CityName"]);
                        objUS.Pincode = Convert.ToInt32(Dt.Rows[i]["Pincode"]);
                        objUS.UniversitySectionContactNo = Convert.ToString(Dt.Rows[i]["UniversitySectionContactNo"]);
                        objUS.UniversitySectionFaxNo = Convert.ToString(Dt.Rows[i]["UniversitySectionFaxNo"]);
                        objUS.UniversitySectionEmail = Convert.ToString(Dt.Rows[i]["UniversitySectionEmail"]);
                        objUS.UniversitySectionUrl = Convert.ToString(Dt.Rows[i]["UniversitySectionUrl"]);
                        objUS.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);

                        ObjLstUS.Add(objUS);
                    }
                }
                return Return.returnHttp("200", ObjLstUS, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion







    }
}
