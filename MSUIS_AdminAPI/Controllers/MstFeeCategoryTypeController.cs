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
    public class MstFeeCategoryTypeController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region MstFeeCategoryTypeGet
        [HttpPost]
        public HttpResponseMessage MstFeeCategoryTypeGet()
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


                SqlCommand Cmd = new SqlCommand("MstFeeCategoryTypeGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);


                List<MstFeeCategoryType> ObjLstFCT = new List<MstFeeCategoryType>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstFeeCategoryType objFCT = new MstFeeCategoryType();

                        objFCT.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objFCT.FeeCategoryTypeName = Convert.ToString(Dt.Rows[i]["FeeCategoryTypeName"]);
                        objFCT.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjLstFCT.Add(objFCT);
                    }
                }
                return Return.returnHttp("200", ObjLstFCT, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region MstFeeCategoryTypeAdd
        [HttpPost]
        public HttpResponseMessage MstFeeCategoryTypeAdd(MstFeeCategoryType FCT)
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


                if (String.IsNullOrWhiteSpace(Convert.ToString(FCT.FeeCategoryTypeName))) { return Return.returnHttp("201", "Kindly Enter Fee Category Type Name", null); }

                else
                {
                    String FeeCategoryTypeName = Convert.ToString(FCT.FeeCategoryTypeName);


                    SqlCommand cmd = new SqlCommand("MstFeeCategoryTypeAdd", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FeeCategoryTypeName", FeeCategoryTypeName);

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

        #region MstFeeCategoryTypeEdit
        [HttpPost]
        public HttpResponseMessage MstFeeCategoryTypeEdit(MstFeeCategoryType FCT)
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


                if (String.IsNullOrWhiteSpace(Convert.ToString(FCT.FeeCategoryTypeName))) { return Return.returnHttp("201", "Kindly Enter Fee Category Type Name", null); }
                else if (String.IsNullOrEmpty(Convert.ToString(FCT.Id))) { return Return.returnHttp("201", "Please Try Again Server Error", null); }
                else
                {
                    Int32 Id = Convert.ToInt32(FCT.Id);
                    String FeeCategoryTypeName = Convert.ToString(FCT.FeeCategoryTypeName);

                    SqlCommand cmd = new SqlCommand("MstFeeCategoryTypeEdit", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@FeeCategoryTypeName", FeeCategoryTypeName);
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

        #region MstFeeCategoryTypeDelete
        [HttpPost]
        public HttpResponseMessage MstFeeCategoryTypeDelete(MstFeeCategoryType FCT)
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


                Int32 Id = Convert.ToInt32(FCT.Id);

                SqlCommand cmd = new SqlCommand("MstFeeCategoryTypeDelete", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", 0);
                cmd.Parameters.AddWithValue("@Id", Id);
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

        #region MstFeeCategoryTypeGetbyId
        [HttpPost]
        public HttpResponseMessage MstFeeCategoryTypeGetbyId(MstFeeCategoryType FCT)
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


                Int32 Id = Convert.ToInt32(FCT.Id);

                SqlCommand cmd = new SqlCommand("MstFeeCategoryTypeGetbyId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<MstFeeCategoryType> ObjLstFT = new List<MstFeeCategoryType>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstFeeCategoryType objFCT = new MstFeeCategoryType();

                        objFCT.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objFCT.FeeCategoryTypeName = Convert.ToString(Dt.Rows[i]["FeeCategoryTypeName"]);

                        objFCT.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjLstFT.Add(objFCT);
                    }
                }
                return Return.returnHttp("200", ObjLstFT, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion
    }
}
