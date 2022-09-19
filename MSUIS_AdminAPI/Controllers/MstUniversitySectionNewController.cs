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
    public class MstUniversitySectionNewController : ApiController 
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

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
                        ObjMS.UniversitySectionCode = (dr["UniversitySectionCode"].ToString());
                        ObjMS.UniversitySectionAddress = (dr["UniversitySectionAddress"].ToString());
                        ObjMS.CityName = (dr["CityName"].ToString());
                        ObjMS.Pincode = Convert.ToInt32(dr["Pincode"].ToString());
                        ObjMS.UniversitySectionContactNo = (dr["UniversitySectionContactNo"].ToString());
                        ObjMS.UniversitySectionFaxNo = (dr["UniversitySectionFaxNo"].ToString());
                        ObjMS.UniversitySectionEmail = (dr["UniversitySectionEmail"].ToString());
                        ObjMS.UniversitySectionUrl = (dr["UniversitySectionUrl"].ToString());
                        var IsActive = dr["IsActive"];
                        if (IsActive is DBNull)
                        {
                            ObjMS.IsActive = null;
                        }
                        else
                        {
                            ObjMS.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        }
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

        #region MstUniversitySectionAdd
        [HttpPost]
        public HttpResponseMessage MstUniversitySectionAdd(MstUniversitySectionNew ObjMstUS)
        {
            MstUniversitySectionNew modelobj = new MstUniversitySectionNew();

            try
            {
                modelobj.UniversitySectionName = ObjMstUS.UniversitySectionName;
                modelobj.UniversitySectionCode = ObjMstUS.UniversitySectionCode;
                modelobj.UniversitySectionAddress = ObjMstUS.UniversitySectionAddress;
                modelobj.CityName = ObjMstUS.CityName;
                modelobj.Pincode = ObjMstUS.Pincode;
                modelobj.UniversitySectionContactNo = ObjMstUS.UniversitySectionContactNo;
                modelobj.UniversitySectionFaxNo = ObjMstUS.UniversitySectionFaxNo;
                modelobj.UniversitySectionEmail = ObjMstUS.UniversitySectionEmail;
                modelobj.UniversitySectionUrl = ObjMstUS.UniversitySectionUrl;
               


                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                modelobj.CreatedBy = Convert.ToInt32(res);

                //if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.UniversitySectionName)))
                //{
                //    return Return.returnHttp("201", "University Section Name is null or empty", null);
                //}
                //else
                //{
                    SqlCommand cmd = new SqlCommand("MstUniversitySectionAdd", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UniversitySectionName", (modelobj.UniversitySectionName).Trim());
                    cmd.Parameters.AddWithValue("@UniversitySectionCode", (modelobj.UniversitySectionCode).Trim());
                    cmd.Parameters.AddWithValue("@UniversitySectionAddress", (modelobj.UniversitySectionAddress).Trim());
                    cmd.Parameters.AddWithValue("@CityName", (modelobj.CityName).Trim());
                    cmd.Parameters.AddWithValue("@Pincode", (modelobj.Pincode));
                    cmd.Parameters.AddWithValue("@UniversitySectionContactNo", (modelobj.UniversitySectionContactNo).Trim());
                    cmd.Parameters.AddWithValue("@UniversitySectionFaxNo", (modelobj.UniversitySectionFaxNo).Trim());
                    cmd.Parameters.AddWithValue("@UniversitySectionEmail", (modelobj.UniversitySectionEmail).Trim());
                    cmd.Parameters.AddWithValue("@UniversitySectionUrl", (modelobj.UniversitySectionUrl).Trim());

                    cmd.Parameters.AddWithValue("@CreatedBy", modelobj.CreatedBy);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    Con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    Con.Close();

                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "University Section Details have been added successfully.";
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

        #region MstUniversitySectionEdit
        [HttpPost]
        public HttpResponseMessage MstUniversitySectionEdit(JObject jsonobject)

        {
            MstUniversitySectionNew MstUniversitySectionNew = new MstUniversitySectionNew();
            dynamic jsonData = jsonobject;

            try
            {
                MstUniversitySectionNew.Id = Convert.ToInt32(jsonData.Id);
                MstUniversitySectionNew.UniversitySectionName = Convert.ToString(jsonData.UniversitySectionName);
                MstUniversitySectionNew.UniversitySectionCode = Convert.ToString(jsonData.UniversitySectionCode);
                MstUniversitySectionNew.UniversitySectionAddress = Convert.ToString(jsonData.UniversitySectionAddress);
                MstUniversitySectionNew.CityName = Convert.ToString(jsonData.CityName);
                MstUniversitySectionNew.Pincode = Convert.ToInt32(jsonData.Pincode);
                MstUniversitySectionNew.UniversitySectionContactNo = Convert.ToString(jsonData.UniversitySectionContactNo);
                MstUniversitySectionNew.UniversitySectionFaxNo = Convert.ToString(jsonData.UniversitySectionFaxNo);
                MstUniversitySectionNew.UniversitySectionEmail = Convert.ToString(jsonData.UniversitySectionEmail);
                MstUniversitySectionNew.UniversitySectionUrl = Convert.ToString(jsonData.UniversitySectionUrl);

                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                MstUniversitySectionNew.ModifiedBy = Convert.ToInt64(responseToken);

                SqlCommand cmd = new SqlCommand("MstUniversitySectionEdit", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", MstUniversitySectionNew.Id);
                cmd.Parameters.AddWithValue("@UniversitySectionName", (MstUniversitySectionNew.UniversitySectionName).Trim());
                cmd.Parameters.AddWithValue("@UniversitySectionCode", (MstUniversitySectionNew.UniversitySectionCode).Trim());
                cmd.Parameters.AddWithValue("@UniversitySectionAddress", (MstUniversitySectionNew.UniversitySectionAddress).Trim());
                cmd.Parameters.AddWithValue("@CityName", (MstUniversitySectionNew.CityName).Trim());
                cmd.Parameters.AddWithValue("@Pincode", (MstUniversitySectionNew.Pincode));
                cmd.Parameters.AddWithValue("@UniversitySectionContactNo", (MstUniversitySectionNew.UniversitySectionContactNo).Trim());
                cmd.Parameters.AddWithValue("@UniversitySectionFaxNo", (MstUniversitySectionNew.UniversitySectionFaxNo).Trim());
                cmd.Parameters.AddWithValue("@UniversitySectionEmail", (MstUniversitySectionNew.UniversitySectionEmail).Trim());
                cmd.Parameters.AddWithValue("@UniversitySectionUrl", (MstUniversitySectionNew.UniversitySectionUrl).Trim());

                cmd.Parameters.AddWithValue("@ModifiedBy", MstUniversitySectionNew.ModifiedBy);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                Con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "University Section Details have been updated successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);

                
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion

        #region MstUniversitySectionDelete
        [HttpPost]
        public HttpResponseMessage MstUniversitySectionDelete(MstUniversitySectionNew ObjMstUsNew)
        {
            MstUniversitySectionNew modelobj = new MstUniversitySectionNew();

            try
            {
                modelobj.Id = Convert.ToInt32(ObjMstUsNew.Id);
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
              
                
                SqlCommand cmd = new SqlCommand("MstUniversitySectionDelete", Con);
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
                    strMessage = "University Section Details have been deleted successfully.";
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
