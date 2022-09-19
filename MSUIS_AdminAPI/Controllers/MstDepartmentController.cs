using MSUIS_TokenManager.App_Start;
using MSUISApi.BAL;
using MSUISApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class MstDepartmentController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region Department List Get
        [HttpPost]
        public HttpResponseMessage MstDepartmentListGet()
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
                List<MstDepartment> ObjLstDept = new List<MstDepartment>();
                BALDepartmentList ObjBalDeptList = new BALDepartmentList();
                ObjLstDept = ObjBalDeptList.DepartmentListGet();

                if (ObjLstDept != null)
                    return Return.returnHttp("200", ObjLstDept, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);                
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region Department Add
        [HttpPost]
        public HttpResponseMessage MstDepartmentAdd(MstDepartment ObjDept)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            if (String.IsNullOrWhiteSpace(Convert.ToString(ObjDept.DepartmentName)))
            {
                return Return.returnHttp("201", "Please enter department name", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjDept.DepartmentCode)))
            {
                return Return.returnHttp("201", "Please enter department code", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjDept.FacultyId)))
            {
                return Return.returnHttp("201", "Please select faculty", null);
            }
            else
            {
                try
                {
                    Int64 UserId = Convert.ToInt64(res.ToString());

                    string DepartmentName = Convert.ToString(ObjDept.DepartmentName);
                    string DepartmentCode = Convert.ToString(ObjDept.DepartmentCode);
                    int FacultyId = Convert.ToInt32(ObjDept.FacultyId);

                    SqlCommand cmd = new SqlCommand("MstDepartmentAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@DepartmentName", DepartmentName);
                    cmd.Parameters.AddWithValue("@DepartmentCode", DepartmentCode);
                    cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);

                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    con.Close();

                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Your data has been added successfully.";
                    }
                    return Return.returnHttp("200", strMessage.ToString(), null);
                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message, null);
                }
            }

        }
        #endregion

        #region Department Edit
        [HttpPost]
        public HttpResponseMessage MstDepartmentUpdate(MstDepartment ObjDept)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            if (String.IsNullOrWhiteSpace(Convert.ToString(ObjDept.DepartmentName)))
            {
                return Return.returnHttp("201", "Please enter department name", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjDept.DepartmentCode)))
            {
                return Return.returnHttp("201", "Please enter department code", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjDept.FacultyId)))
            {
                return Return.returnHttp("201", "Please select faculty", null);
            }
            else
            {
                try
                {
                    Int64 UserId = Convert.ToInt64(res.ToString());

                    string DepartmentName = Convert.ToString(ObjDept.DepartmentName);
                    string DepartmentCode = Convert.ToString(ObjDept.DepartmentCode);
                    int FacultyId = Convert.ToInt32(ObjDept.FacultyId);
                    int DepartmentId = Convert.ToInt32(ObjDept.Id);

                    SqlCommand cmd = new SqlCommand("MstDepartmentEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@DepartmentName", DepartmentName);
                    cmd.Parameters.AddWithValue("@DepartmentCode", DepartmentCode);
                    cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@DepartmentId", DepartmentId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);

                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    con.Close();

                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Your data has been modified successfully.";
                    }
                    return Return.returnHttp("200", strMessage.ToString(), null);
                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message, null);
                }
            }

        }
        #endregion

        #region Department Active Enable
        [HttpPost]
        public HttpResponseMessage MstDepartmentIsActiveEnable(MstDepartment ObjDept)
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
                Int64 UserId = Convert.ToInt64(res.ToString());
                int DepartmentId = Convert.ToInt32(ObjDept.Id);


                SqlCommand cmd = new SqlCommand("MstDepartmentActive", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DepartmentId", DepartmentId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.AddWithValue("@Flag", "MstDepartmentIsActiveEnable");
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();
                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been saved successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region Department Active Disable
        [HttpPost]
        public HttpResponseMessage MstDepartmentIsActiveDisable(MstDepartment ObjDept)
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
                Int64 UserId = Convert.ToInt64(res.ToString());
                int DepartmentId = Convert.ToInt32(ObjDept.Id);


                SqlCommand cmd = new SqlCommand("MstDepartmentActive", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DepartmentId", DepartmentId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.AddWithValue("@Flag", "MstDepartmentIsActiveDisable");
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();
                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been saved successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region Department Delete
        [HttpPost]
        public HttpResponseMessage MstDepartmentDelete(MstDepartment ObjDept)
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
                Int64 UserId = Convert.ToInt64(res.ToString());
                int DepartmentId = Convert.ToInt32(ObjDept.Id);


                SqlCommand cmd = new SqlCommand("MstDepartmentDelete", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DepartmentId", DepartmentId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();
                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been deleted successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Department Get By Id
        [HttpPost]
        public HttpResponseMessage MstDepartmentListGetbyId(MstDepartment ObjDept)
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
                MstDepartment ObjLstDept = new MstDepartment();
                BALDepartmentList ObjBalDeptList = new BALDepartmentList();
                ObjLstDept = ObjBalDeptList.DepartmentGetbyId(ObjDept.Id);

                if (ObjLstDept != null)
                    return Return.returnHttp("200", ObjLstDept, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region Department Get By FacultyId
        [HttpPost]
        public HttpResponseMessage MstDepartmentGetbyFacultyId(MstDepartment ObjDept)
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
                List<MstDepartment> ObjLstDept = new List<MstDepartment>();
                BALDepartmentList ObjBalDeptList = new BALDepartmentList();
                ObjLstDept = ObjBalDeptList.MstDepartmentGetbyFacultyId(ObjDept.FacultyId);

                if (ObjLstDept != null)
                    return Return.returnHttp("200", ObjLstDept, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion
    }
}
