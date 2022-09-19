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
using System.Dynamic;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class MstUserNewController : ApiController 
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region UserTypeMaster Get
        [HttpPost]
        public HttpResponseMessage UserTypeMasterGet()
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            try
            {
                SqlCommand cmd = new SqlCommand("UserTypeMasterGet", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<UserTypeMaster> ObjLstUserTypeMst = new List<UserTypeMaster>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        UserTypeMaster objUserTypeMst = new UserTypeMaster();
                        objUserTypeMst.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objUserTypeMst.UserTypeName = Convert.ToString(Dt.Rows[i]["UserTypeName"]);

                        ObjLstUserTypeMst.Add(objUserTypeMst);
                    }
                }
                return Return.returnHttp("200", ObjLstUserTypeMst, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion      

        #region MstEmployee Get
        [HttpPost]
        public HttpResponseMessage MstEmployeeGetforUser()
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

                SqlCommand cmd = new SqlCommand("MstEmployeeGetforUserEntry", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<EmployeeRegistration> ObjEmpReg = new List<EmployeeRegistration>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        EmployeeRegistration ObjEmp = new EmployeeRegistration();
                        ObjEmp.Id = Convert.ToInt64(dr["Id"]);
                        ObjEmp.UserTypeId = Convert.ToInt64(dr["UserTypeId"].ToString());
                        ObjEmp.UserTypeName = (dr["UserTypeName"].ToString());
                        ObjEmp.FullName = (dr["FullName"].ToString());

                        ObjEmpReg.Add(ObjEmp);
                    }

                }
                return Return.returnHttp("200", ObjEmpReg, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region MstUserNewGet
        [HttpPost]
        public HttpResponseMessage MstUserNewGet()
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

                SqlCommand cmd = new SqlCommand("MstUserGet", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                Int32 count = 0;
                List<MstUserNew> ObjMstUser = new List<MstUserNew>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        MstUserNew ObjUser = new MstUserNew();
                        ObjUser.Id = Convert.ToInt64(dr["Id"]);
                        ObjUser.UserTypeId = Convert.ToInt64(dr["UserTypeId"].ToString());
                        ObjUser.UserTypeName = (dr["UserTypeName"].ToString());
                        
                        var EmployeeId = dr["EmployeeId"];
                        if (EmployeeId is DBNull)
                        {
                            ObjUser.EmployeeId = null;
                        }
                        else
                        {
                            ObjUser.EmployeeId = Convert.ToInt64(dr["EmployeeId"].ToString());
                        }
                        ObjUser.FullName = (dr["FullName"].ToString());
                        ObjUser.UserName = (dr["UserName"].ToString());
                        ObjUser.Password = (dr["Password"].ToString());
                        ObjUser.MobileNo = (dr["MobileNo"].ToString());
                        ObjUser.EmailId = (dr["EmailId"].ToString());
                        ObjUser.DisplayName = (dr["DisplayName"].ToString());
                        var IsActive = dr["IsActive"];
                        if (IsActive is DBNull)
                        {
                            ObjUser.IsActive = null;
                        }
                        else
                        {
                            ObjUser.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        }

                        count = count + 1;
                        ObjUser.IndexId = count;

                        ObjMstUser.Add(ObjUser);
                    }

                }
                return Return.returnHttp("200", ObjMstUser, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region MstUserNewAdd
        [HttpPost]
        public HttpResponseMessage MstUserNewAdd(MstUserNew ObjMstUserNew)
        {
            MstUserNew modelobj = new MstUserNew();

            try
            {
                modelobj.UserTypeId = ObjMstUserNew.UserTypeId;
                modelobj.EmployeeId = ObjMstUserNew.EmployeeId;
                modelobj.UserName = ObjMstUserNew.UserName;
                modelobj.Password = ObjMstUserNew.Password;
                modelobj.MobileNo = ObjMstUserNew.MobileNo;
                modelobj.EmailId = ObjMstUserNew.EmailId;
                modelobj.DisplayName = ObjMstUserNew.DisplayName;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                modelobj.CreatedBy = Convert.ToInt32(res);

                //if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.DesignationName)))
                //{
                //    return Return.returnHttp("201", "Designation Name is null or empty", null);
                //}
                //else
                //{
                SqlCommand cmd = new SqlCommand("MstUserAdd", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserTypeId", (modelobj.UserTypeId));
                cmd.Parameters.AddWithValue("@EmployeeId", (modelobj.EmployeeId));
                cmd.Parameters.AddWithValue("@UserName", (modelobj.UserName).Trim());
                cmd.Parameters.AddWithValue("@Password", (modelobj.Password).Trim());
                cmd.Parameters.AddWithValue("@MobileNo", (modelobj.MobileNo).Trim());
                cmd.Parameters.AddWithValue("@EmailId", (modelobj.EmailId).Trim());
                cmd.Parameters.AddWithValue("@DisplayName", (modelobj.DisplayName).Trim());
                cmd.Parameters.AddWithValue("@CreatedBy", modelobj.CreatedBy);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                Con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "User has been added successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);

                //}
            }
            catch (Exception e)
            {

                return Return.returnHttp("201", e.Message.ToString(), null);

            }

        }
        #endregion

        #region MstUserNewEdit
        [HttpPost]
        public HttpResponseMessage MstUserNewEdit(JObject jsonobject)

        {
            MstUserNew MstUserNew = new MstUserNew();
            dynamic jsonData = jsonobject;

            try
            {
                MstUserNew.Id = Convert.ToInt64(jsonData.Id);
                MstUserNew.UserTypeId = Convert.ToInt64(jsonData.UserTypeId);
                //MstUserNew.EmployeeId = Convert.ToInt64(jsonData.EmployeeId);
                if (jsonData.EmployeeId is DBNull || jsonData.EmployeeId == null)
                {
                    MstUserNew.EmployeeId = null;
                }
                else
                {
                    MstUserNew.EmployeeId = Convert.ToInt64(jsonData.EmployeeId);
                }
                MstUserNew.UserName = Convert.ToString(jsonData.UserName);
                MstUserNew.Password = Convert.ToString(jsonData.Password);
                MstUserNew.MobileNo = Convert.ToString(jsonData.MobileNo);
                MstUserNew.EmailId = Convert.ToString(jsonData.EmailId);
                MstUserNew.DisplayName = Convert.ToString(jsonData.DisplayName);

                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                MstUserNew.ModifiedBy = Convert.ToInt64(responseToken);

                SqlCommand cmd = new SqlCommand("MstUserEdit", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", MstUserNew.Id);
                cmd.Parameters.AddWithValue("@UserTypeId", MstUserNew.UserTypeId);
                cmd.Parameters.AddWithValue("@EmployeeId", MstUserNew.EmployeeId);
                cmd.Parameters.AddWithValue("@UserName", MstUserNew.UserName);
                cmd.Parameters.AddWithValue("@Password", MstUserNew.Password);
                cmd.Parameters.AddWithValue("@MobileNo", MstUserNew.MobileNo);
                cmd.Parameters.AddWithValue("@EmailId", MstUserNew.EmailId);
                cmd.Parameters.AddWithValue("@DisplayName", MstUserNew.DisplayName);
                cmd.Parameters.AddWithValue("@ModifiedBy", MstUserNew.ModifiedBy);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                Con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "User has been updated successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);

                
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion

        #region MstUserNewDelete
        [HttpPost]
        public HttpResponseMessage MstUserNewDelete(MstUserNew ObjMstUserNew)
        {
            MstUserNew modelobj = new MstUserNew();

            try
            {
                modelobj.Id = Convert.ToInt32(ObjMstUserNew.Id);
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
              
                
                SqlCommand cmd = new SqlCommand("MstUserDelete", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", modelobj.Id.ToString());

                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                Con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "User has been deleted successfully.";
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
