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
    public class ExamVenueExamCenterController : ApiController 
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;

        #region ExamVenueExamCenterGetforVenue
        [HttpPost]
        public HttpResponseMessage ExamVenueExamCenterGetforVenue()
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
                Int64 UserId = Convert.ToInt64(res);

                SqlCommand cmd = new SqlCommand("ExamVenueExamCenterGetforVenue", Con);
                cmd.Parameters.AddWithValue("@UserId", UserId);         
                cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<ExamVenueExamCenter> ObjVC = new List<ExamVenueExamCenter>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        ExamVenueExamCenter ObjCenter = new ExamVenueExamCenter();
                        ObjCenter.ExamVenueId = Convert.ToInt32(dr["ExamVenueId"]);
                        //ObjCenter.ExamCenterId = Convert.ToInt64(dr["ExamCenterId"]);
                        ObjCenter.DisplayName = (dr["DisplayName"].ToString());
                        ObjCenter.Code = (dr["Code"].ToString());
                        ObjVC.Add(ObjCenter);
                    }

                }
                return Return.returnHttp("200", ObjVC, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region ExamVenueExamCenterGet
        [HttpPost]
        public HttpResponseMessage ExamVenueExamCenterGet()
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
                Int64 UserId = Convert.ToInt64(res);

                SqlCommand cmd = new SqlCommand("ExamVenueExamCenterGet", Con);
                cmd.Parameters.AddWithValue("@UserId", UserId);

                cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                Int32 count = 0;
                List<ExamVenueExamCenter> ObjEVEC = new List<ExamVenueExamCenter>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        ExamVenueExamCenter ObjEC = new ExamVenueExamCenter();
                        ObjEC.Id = Convert.ToInt32(dr["Id"]);
                        ObjEC.ExamVenueId = Convert.ToInt32(dr["ExamVenueId"]);
                        ObjEC.CenterName = (dr["CenterName"].ToString());
                        ObjEC.VenueName = (dr["VenueName"].ToString());
                        var IsActive = dr["IsActive"];
                        if (IsActive is DBNull)
                        {
                            ObjEC.IsActive = null;
                        }
                        else
                        {
                            ObjEC.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        }
                        count = count + 1;
                        ObjEC.IndexId = count;
                        ObjEVEC.Add(ObjEC);
                    }

                }
                return Return.returnHttp("200", ObjEVEC, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region ExamVenueExamCenterAdd
        [HttpPost]
        public HttpResponseMessage ExamVenueExamCenterAdd(ExamVenueExamCenter ObjExamVenueExamCenter)
        {
            ExamVenueExamCenter modelobj = new ExamVenueExamCenter();

            try
            {
                modelobj.CenterName = ObjExamVenueExamCenter.CenterName;
                modelobj.ExamVenueId = ObjExamVenueExamCenter.ExamVenueId;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                modelobj.CreatedBy = Convert.ToInt32(res);
              
                SqlCommand cmd = new SqlCommand("ExamVenueExamCenterAdd", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamVenueId", modelobj.ExamVenueId);
                cmd.Parameters.AddWithValue("@CenterName", (modelobj.CenterName).Trim());
                cmd.Parameters.AddWithValue("@CreatedBy", modelobj.CreatedBy);
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

                return Return.returnHttp("201", e.Message.ToString(), null);

            }

        }
        #endregion

        #region ExamVenueExamCenterEdit
        [HttpPost]
        public HttpResponseMessage ExamVenueExamCenterEdit(JObject jsonobject)

        {
            ExamVenueExamCenter ExamVenueExamCenter = new ExamVenueExamCenter();
            dynamic jsonData = jsonobject;

            try
            {
                ExamVenueExamCenter.Id = Convert.ToInt32(jsonData.Id);
                ExamVenueExamCenter.ExamVenueId = Convert.ToInt32(jsonData.ExamVenueId);
                ExamVenueExamCenter.CenterName = Convert.ToString(jsonData.CenterName);

                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                ExamVenueExamCenter.ModifiedBy = Convert.ToInt64(responseToken);

                SqlCommand cmd = new SqlCommand("ExamVenueExamCenterEdit", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", ExamVenueExamCenter.Id);
                cmd.Parameters.AddWithValue("@ExamVenueId", ExamVenueExamCenter.ExamVenueId);
                cmd.Parameters.AddWithValue("@CenterName", ExamVenueExamCenter.CenterName);
                cmd.Parameters.AddWithValue("@ModifiedBy", ExamVenueExamCenter.ModifiedBy);
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

        #region ExamVenueExamCenterDelete
        [HttpPost]
        public HttpResponseMessage ExamVenueExamCenterDelete(ExamVenueExamCenter ObjExamVenueExamCenter)
        {
            ExamVenueExamCenter modelobj = new ExamVenueExamCenter();

            try
            {
                modelobj.Id = Convert.ToInt32(ObjExamVenueExamCenter.Id);
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
              
                
                SqlCommand cmd = new SqlCommand("ExamVenueExamCenterDelete", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", modelobj.Id);

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
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion
    }
}
