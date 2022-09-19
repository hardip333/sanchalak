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
    public class UserRoleMapController : ApiController 
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region MstUserGetforMap
        [HttpPost]
        public HttpResponseMessage MstUserGetforMap()
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

                SqlCommand cmd = new SqlCommand("MstUserGetforMap", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<MstUserNew> ObjMstUser = new List<MstUserNew>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        MstUserNew ObjUser = new MstUserNew();
                        ObjUser.Id = Convert.ToInt64(dr["Id"]);
                        ObjUser.UserName = (dr["UserName"].ToString());

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

        #region MstRoleGet
        [HttpPost]
        public HttpResponseMessage MstRoleGet()
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

                SqlCommand cmd = new SqlCommand("MstRoleGet", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<RolePermissionMap> ObjRolePermission = new List<RolePermissionMap>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        RolePermissionMap ObjRM = new RolePermissionMap();
                        ObjRM.Id = Convert.ToInt64(dr["Id"]);
                        ObjRM.RoleName = (dr["RoleName"].ToString());
                        var IsActive = dr["IsActive"];
                        if (IsActive is DBNull)
                        {
                            ObjRM.IsActive = null;
                        }
                        else
                        {
                            ObjRM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        }
                        ObjRolePermission.Add(ObjRM);
                    }

                }
                return Return.returnHttp("200", ObjRolePermission, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region MstDesignationGet
        [HttpPost]
        public HttpResponseMessage MstDesignationGet()
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

                SqlCommand cmd = new SqlCommand("MstDesignationGet", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<MstDesignation> ObjMstDesg = new List<MstDesignation>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        MstDesignation ObjDesg = new MstDesignation();
                        ObjDesg.Id = Convert.ToInt32(dr["Id"]);
                        ObjDesg.DesignationName = (dr["DesignationName"].ToString());
                        var IsActive = dr["IsActive"];
                        if (IsActive is DBNull)
                        {
                            ObjDesg.IsActive = null;
                        }
                        else
                        {
                            ObjDesg.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        }
                        ObjMstDesg.Add(ObjDesg);
                    }

                }
                return Return.returnHttp("200", ObjMstDesg, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region MstUniversitySectionGet
        [HttpPost]
        public HttpResponseMessage MstUniversitySectionGet()
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

                SqlCommand cmd = new SqlCommand("MstUniversitySectionGet", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<MstUniversitySectionNew> ObjMstUS = new List<MstUniversitySectionNew>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        MstUniversitySectionNew ObjMS = new MstUniversitySectionNew();
                        ObjMS.Id = Convert.ToInt32(dr["Id"]);
                        ObjMS.UniversitySectionName = (dr["UniversitySectionName"].ToString());
                        ObjMstUS.Add(ObjMS);
                    }

                }
                return Return.returnHttp("200", ObjMstUS, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region MstInstituteGet  
        [HttpPost]
        public HttpResponseMessage MstInstituteGet()
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

                SqlCommand cmd = new SqlCommand("MstInstituteGet", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<MstInstitute> ObjMstInst = new List<MstInstitute>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        MstInstitute ObjInst = new MstInstitute();
                        ObjInst.Id = Convert.ToInt32(dr["Id"]);
                        ObjInst.InstituteName = (dr["InstituteName"].ToString());

                        ObjMstInst.Add(ObjInst);
                    }

                }
                return Return.returnHttp("200", ObjMstInst, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region MstDepartmentGetForMap
        [HttpPost]
        public HttpResponseMessage MstDepartmentGetForMap(UserRoleMap ObjUserRoleMap)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("MstDepartmentGetForMap", Con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@FacultyId", ObjUserRoleMap.FacultyId);
                da.SelectCommand.Parameters.AddWithValue("@InstituteId", ObjUserRoleMap.InstituteId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<UserRoleMap> ObjUserRole = new List<UserRoleMap>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        UserRoleMap ObjURM = new UserRoleMap();
                        ObjURM.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjURM.SubjectName = Convert.ToString(dt.Rows[i]["SubjectName"]);

                        ObjUserRole.Add(ObjURM);

                    }
                    return Return.returnHttp("200", ObjUserRole, null);
                }
                else
                {
                    return Return.returnHttp("200", "No Record Found", null);
                }

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region UserRoleMapGet
        [HttpPost]
        public HttpResponseMessage UserRoleMapGet()
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

                SqlCommand cmd = new SqlCommand("UserRoleMapGet", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                Int32 count = 0;
                List<UserRoleMap> ObjUserRoleMap = new List<UserRoleMap>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        UserRoleMap ObjURM = new UserRoleMap();
                        ObjURM.Id = Convert.ToInt64(dr["Id"]);
                        ObjURM.UserId = Convert.ToInt64(dr["UserId"]);
                        ObjURM.UserName = (dr["UserName"].ToString());
                        ObjURM.DisplayName = (dr["DisplayName"].ToString());
                        ObjURM.RoleId = Convert.ToInt64(dr["RoleId"]);
                        ObjURM.RoleName = (dr["RoleName"].ToString());
                        ObjURM.DesignationId = Convert.ToInt64(dr["DesignationId"]);
                        ObjURM.DesignationName = (dr["DesignationName"].ToString());
                        ObjURM.StartDateView = string.Format("{0: dd-MM-yyyy}", dr["StartDate"]);
                        ObjURM.EndDateView = string.Format("{0: dd-MM-yyyy}", dr["EndDate"]);
                        var Tennure = dr["Tennure"];
                        if (Tennure is DBNull)
                        {
                            ObjURM.Tennure = null;
                        }
                        else
                        {
                            ObjURM.Tennure = (dr["Tennure"].ToString());
                        }
                       
                        var FacultyId = dr["FacultyId"];
                        if (FacultyId is DBNull)
                        {
                            ObjURM.FacultyId = null;
                        }
                        else
                        {
                            ObjURM.FacultyId = Convert.ToInt64(dr["FacultyId"]);
                        }
                        ObjURM.FacultyName = (dr["FacultyName"].ToString());
                       
                        var DepartmentId = dr["DepartmentId"];
                        if (DepartmentId is DBNull)
                        {
                            ObjURM.DepartmentId = null;
                        }
                        else
                        {
                            ObjURM.DepartmentId = Convert.ToInt64(dr["DepartmentId"]);
                        }
                        ObjURM.DepartmentName = (dr["DepartmentName"].ToString());
                       
                        var InstituteId = dr["InstituteId"];
                        if (InstituteId is DBNull)
                        {
                            ObjURM.InstituteId = null;
                        }
                        else
                        {
                            ObjURM.InstituteId = Convert.ToInt64(dr["InstituteId"]);
                        }
                        ObjURM.InstituteName = (dr["InstituteName"].ToString());
                       
                        var UniversitySectionId = dr["UniversitySectionId"];
                        if (UniversitySectionId is DBNull)
                        {
                            ObjURM.UniversitySectionId = null;
                        }
                        else
                        {
                            ObjURM.UniversitySectionId = Convert.ToInt64(dr["UniversitySectionId"]);
                        }
                        ObjURM.UniversitySectionName = (dr["UniversitySectionName"].ToString());

                        ObjURM.UserLevelType = (dr["UserLevelType"].ToString());
                        count = count + 1;
                        ObjURM.IndexId = count;
                        ObjUserRoleMap.Add(ObjURM);
                    }

                }
                return Return.returnHttp("200", ObjUserRoleMap, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region UserRoleMapAdd
        [HttpPost]
        public HttpResponseMessage UserRoleMapAdd(UserRoleMap ObjUserRoleMap)
        {
           
            try
            {
                SqlCommand cmd = new SqlCommand("UserRoleMapAdd", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                UserRoleMap URM = new UserRoleMap();

                URM.UserId = ObjUserRoleMap.UserId;
                URM.RoleId = ObjUserRoleMap.RoleId;
                URM.DesignationId = ObjUserRoleMap.DesignationId;
                //URM.StartDate = ObjUserRoleMap.StartDate;
                //URM.EndDate = ObjUserRoleMap.EndDate;
                URM.Tennure = ObjUserRoleMap.Tennure;
                URM.FacultyId = ObjUserRoleMap.FacultyId;
                URM.DepartmentId = ObjUserRoleMap.DepartmentId;
                URM.InstituteId = ObjUserRoleMap.InstituteId;
                URM.UniversitySectionId = ObjUserRoleMap.UniversitySectionId;
                URM.UserLevelType = ObjUserRoleMap.UserLevelType;

                URM.CreatedBy = Convert.ToInt32(responseToken);


                if (ObjUserRoleMap.StartDate != null)
                {
                    DateTime dtAppStartDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(ObjUserRoleMap.StartDate), out dtAppStartDate);
                    if (ChkDt)
                    {
                        //aaconfig.ApplicationStartDate = dtAppStartDate;
                        URM.StartDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(ObjUserRoleMap.StartDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

                    }
                    else
                    {
                        URM.StartDate = null;
                    }
                }
                else
                {
                    URM.StartDate = null;
                }

                if (ObjUserRoleMap.EndDate != null)
                {
                    DateTime dtAppStopDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(ObjUserRoleMap.EndDate), out dtAppStopDate);
                    if (ChkDt)
                    {
                        URM.EndDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(ObjUserRoleMap.EndDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    }
                    else
                    {
                        URM.EndDate = null;
                    }
                }
                else
                {
                    URM.EndDate = null;
                }


                cmd.Parameters.AddWithValue("@Id", URM.Id);
                cmd.Parameters.AddWithValue("@UserId", URM.UserId);
                cmd.Parameters.AddWithValue("@RoleId", URM.RoleId);
                cmd.Parameters.AddWithValue("@DesignationId", URM.DesignationId);
                cmd.Parameters.AddWithValue("@StartDate", URM.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", URM.EndDate);
                cmd.Parameters.AddWithValue("@Tennure", URM.Tennure);
                cmd.Parameters.AddWithValue("@FacultyId", URM.FacultyId);
                cmd.Parameters.AddWithValue("@DepartmentId", URM.DepartmentId);
                cmd.Parameters.AddWithValue("@InstituteId", URM.InstituteId);
                cmd.Parameters.AddWithValue("@UniversitySectionId", URM.UniversitySectionId);
                cmd.Parameters.AddWithValue("@UserLevelType", URM.UserLevelType);
                cmd.Parameters.AddWithValue("@CreatedBy", URM.CreatedBy);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                Con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "User-Role Map has been added successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region UserRoleMapEdit
        [HttpPost]
        public HttpResponseMessage UserRoleMapEdit(JObject jsonobject)

        {
            UserRoleMap UserRoleMap = new UserRoleMap();
            dynamic jsonData = jsonobject;

            try
            {
                UserRoleMap.Id = Convert.ToInt64(jsonData.Id);
                UserRoleMap.UserId = Convert.ToInt64(jsonData.UserId);
                UserRoleMap.RoleId = Convert.ToInt64(jsonData.RoleId);
                UserRoleMap.DesignationId = Convert.ToInt64(jsonData.DesignationId);
                //UserRoleMap.StartDate = Convert.ToInt64(jsonData.StartDate);
                //UserRoleMap.EndDate = Convert.ToInt64(jsonData.EndDate);
                UserRoleMap.Tennure = Convert.ToString(jsonData.Tennure);
               
              
               
                
                UserRoleMap.UserLevelType = Convert.ToString(jsonData.UserLevelType);

                if(UserRoleMap.UserLevelType == "University")
                {
                    UserRoleMap.UniversitySectionId = Convert.ToInt64(jsonData.UniversitySectionId);
                }
                else if (UserRoleMap.UserLevelType == "Institute")
                {
                    if(jsonData.FacultyId != null)
                    {
                        UserRoleMap.FacultyId = Convert.ToInt64(jsonData.FacultyId);
                    }
                    UserRoleMap.InstituteId = Convert.ToInt64(jsonData.InstituteId);
                }
                else if (UserRoleMap.UserLevelType == "Faculty")
                {
                    UserRoleMap.FacultyId = Convert.ToInt64(jsonData.FacultyId);
                }
                else if (UserRoleMap.UserLevelType == "Department")
                {
                    UserRoleMap.FacultyId = Convert.ToInt64(jsonData.FacultyId);
                    UserRoleMap.InstituteId = Convert.ToInt64(jsonData.InstituteId);
                    UserRoleMap.DepartmentId = Convert.ToInt64(jsonData.DepartmentId);
                }
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                if (jsonData.StartDate != null)
                {
                    DateTime dtAppStartDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(jsonData.StartDate), out dtAppStartDate);
                    if (ChkDt)
                    {
                        //aaconfig.ApplicationStartDate = dtAppStartDate;
                        UserRoleMap.StartDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(jsonData.StartDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

                    }
                    else
                    {
                        UserRoleMap.StartDate = null;
                    }
                }
                else
                {
                    UserRoleMap.StartDate = null;
                }

                if (jsonData.EndDate != null)
                {
                    DateTime dtAppStopDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(jsonData.EndDate), out dtAppStopDate);
                    if (ChkDt)
                    {
                        UserRoleMap.EndDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(jsonData.EndDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    }
                    else
                    {
                        UserRoleMap.EndDate = null;
                    }
                }
                else
                {
                    UserRoleMap.EndDate = null;
                }

                UserRoleMap.ModifiedBy = Convert.ToInt64(responseToken);

                SqlCommand cmd = new SqlCommand("UserRoleMapEdit", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", UserRoleMap.Id);
                cmd.Parameters.AddWithValue("@UserId", UserRoleMap.UserId);
                cmd.Parameters.AddWithValue("@RoleId", UserRoleMap.RoleId);
                cmd.Parameters.AddWithValue("@DesignationId", UserRoleMap.DesignationId);
                cmd.Parameters.AddWithValue("@StartDate", UserRoleMap.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", UserRoleMap.EndDate);
                cmd.Parameters.AddWithValue("@Tennure", UserRoleMap.Tennure);
                cmd.Parameters.AddWithValue("@FacultyId", UserRoleMap.FacultyId);
                cmd.Parameters.AddWithValue("@DepartmentId", UserRoleMap.DepartmentId);
                cmd.Parameters.AddWithValue("@InstituteId", UserRoleMap.InstituteId);
                cmd.Parameters.AddWithValue("@UniversitySectionId", UserRoleMap.UniversitySectionId);
                cmd.Parameters.AddWithValue("@UserLevelType", UserRoleMap.UserLevelType);
                cmd.Parameters.AddWithValue("@ModifiedBy", UserRoleMap.ModifiedBy);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                Con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "User-Role Map has been updated successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);

                
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion

        #region UserRoleMapDelete
        [HttpPost]
        public HttpResponseMessage UserRoleMapDelete(UserRoleMap ObjUserRoleMap)
        {
            UserRoleMap modelobj = new UserRoleMap();

            try
            {
                modelobj.Id = Convert.ToInt32(ObjUserRoleMap.Id);
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
              
                
                SqlCommand cmd = new SqlCommand("UserRoleMapDelete", Con);
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
                    strMessage = "User-Role Map has been deleted successfully.";
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
