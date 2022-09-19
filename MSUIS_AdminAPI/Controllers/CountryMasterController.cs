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
    public class CountryMasterController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region CountryMasterGet
        [HttpPost]
        public HttpResponseMessage CountryMasterGet()
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


                SqlCommand Cmd = new SqlCommand("CountryMasterGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);


                List<CountryMaster> ObjLstCM = new List<CountryMaster>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        CountryMaster objCM = new CountryMaster();

                        objCM.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objCM.Alpha2Code = Convert.ToString(Dt.Rows[i]["Alpha2Code"]);
                        objCM.Alpha3Code = Convert.ToString(Dt.Rows[i]["Alpha3Code"]);
                        objCM.CountryName = Convert.ToString(Dt.Rows[i]["CountryName"]);

                        ObjLstCM.Add(objCM);
                    }
                }
                return Return.returnHttp("200", ObjLstCM, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region CountryMasterAdd
        [HttpPost]
        public HttpResponseMessage CountryMasterAdd(CountryMaster CM)
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


                if (String.IsNullOrWhiteSpace(Convert.ToString(CM.CountryName))) { return Return.returnHttp("201", "Kindly Enter Country Name", null); }

                else
                {
                    String CountryName = Convert.ToString(CM.CountryName);
                    String Alpha2Code = Convert.ToString(CM.Alpha2Code);
                    if (Alpha2Code.Length > 2) { return Return.returnHttp("200", "Enter Only 2 Digit Code", null); }
                    String Alpha3Code = Convert.ToString(CM.Alpha3Code);
                    if (Alpha3Code.Length > 3) { return Return.returnHttp("200", "Enter Only 3 Digit Code", null); }

                    SqlCommand cmd = new SqlCommand("CountryMasterAdd", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CountryName", CountryName);
                    cmd.Parameters.AddWithValue("@Alpha2Code", Alpha2Code);
                    cmd.Parameters.AddWithValue("@Alpha3Code", Alpha3Code);
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

        #region CountryMasterEdit
        [HttpPost]
        public HttpResponseMessage CountryMasterEdit(CountryMaster CM)
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


                if (String.IsNullOrWhiteSpace(Convert.ToString(CM.CountryName))) { return Return.returnHttp("201", "Kindly Enter Country Name", null); }
                else if (String.IsNullOrEmpty(Convert.ToString(CM.Id))) { return Return.returnHttp("201", "Please Try Again Server Error", null); }
                else
                {
                    Int32 Id = Convert.ToInt32(CM.Id);
                    String CountryName = Convert.ToString(CM.CountryName);
                    String Alpha2Code = Convert.ToString(CM.Alpha2Code);
                    String Alpha3Code = Convert.ToString(CM.Alpha3Code);

                    SqlCommand cmd = new SqlCommand("CountryMasterEdit", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@CountryName", CountryName);
                    cmd.Parameters.AddWithValue("@Alpha2Code", Alpha2Code);
                    cmd.Parameters.AddWithValue("@Alpha3Code", Alpha3Code);
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

        #region CountryMasterDelete
        [HttpPost]
        public HttpResponseMessage CountryMasterDelete(CountryMaster CM)
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


                Int32 Id = Convert.ToInt32(CM.Id);

                SqlCommand cmd = new SqlCommand("CountryMasterDelete", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.AddWithValue("@UserId", 0);
                cmd.Parameters.AddWithValue("@Id", Id);
                //cmd.Parameters.AddWithValue("@UserTime", datetime);
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

    }
}
