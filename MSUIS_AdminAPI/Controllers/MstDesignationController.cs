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
    public class MstDesignationController : ApiController 
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

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

        #region MstDesignationAdd
        [HttpPost]
        public HttpResponseMessage MstDesignationAdd(MstDesignation ObjMstDesignation)
        {
            MstDesignation modelobj = new MstDesignation();

            try
            {
                modelobj.DesignationName = ObjMstDesignation.DesignationName;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                modelobj.CreatedBy = Convert.ToInt32(res);
                //modelobj.CreatedBy = 2;
                if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.DesignationName)))
                {
                    return Return.returnHttp("201", "Designation Name is null or empty", null);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("MstDesignationAdd", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@DesignationName", (modelobj.DesignationName).Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", modelobj.CreatedBy);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    Con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    Con.Close();

                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Designation has been added successfully.";
                    }
                    return Return.returnHttp("200", strMessage.ToString(), null);

                }
            }
            catch (Exception e)
            {

                return Return.returnHttp("201", e.Message.ToString(), null);

            }

        }
        #endregion

        #region MstDesignationEdit
        [HttpPost]
        public HttpResponseMessage MstDesignationEdit(JObject jsonobject)

        {
            MstDesignation MstDesignation = new MstDesignation();
            dynamic jsonData = jsonobject;

            try
            {
                MstDesignation.Id = Convert.ToInt64(jsonData.Id);
                MstDesignation.DesignationName = Convert.ToString(jsonData.DesignationName);
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                MstDesignation.ModifiedBy = Convert.ToInt64(responseToken);

                SqlCommand cmd = new SqlCommand("MstDesignationEdit", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", MstDesignation.Id);
                cmd.Parameters.AddWithValue("@DesignationName", MstDesignation.DesignationName);
                cmd.Parameters.AddWithValue("@ModifiedBy", MstDesignation.ModifiedBy);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                Con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Designation has been updated successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);

                
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion

        #region MstDesignationDelete
        [HttpPost]
        public HttpResponseMessage MstDesignationDelete(MstDesignation ObjMstDesignation)
        {
            MstDesignation modelobj = new MstDesignation();

            try
            {
                modelobj.Id = Convert.ToInt32(ObjMstDesignation.Id);
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
              
                
                SqlCommand cmd = new SqlCommand("MstDesignationDelete", Con);
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
                    strMessage = "Designation has been deleted successfully.";
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
