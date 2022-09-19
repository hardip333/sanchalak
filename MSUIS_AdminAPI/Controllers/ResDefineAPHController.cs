using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MSUISApi.Controllers
{
    public class ResDefineAPHController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

        #region ResDefineAPHGet
        [HttpPost]
        public HttpResponseMessage ResDefineAPHGet()
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

                SqlCommand cmd = new SqlCommand("ResDefineAPHGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<ResDefineAPH> ObjLstAPH = new List<ResDefineAPH>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ResDefineAPH ObjAPH = new ResDefineAPH();
                        ObjAPH.Id = Convert.ToInt32(dr["Id"]);
                        ObjAPH.APHCode = (dr["APHCode"].ToString());
                        ObjAPH.APHName = (dr["APHName"].ToString());
                        ObjAPH.APHAbbrev = (dr["APHAbbrev"].ToString());
                        ObjAPH.APHDescription = (dr["APHDescription"].ToString());
                        var APHMinMarks = dr["APHMinMarks"];
                        if (APHMinMarks is DBNull) { }
                        else { ObjAPH.APHMinMarks = Convert.ToInt32(dr["APHMinMarks"]); }
                        var APHMaxMarks = dr["APHMaxMarks"];
                        if (APHMaxMarks is DBNull) { }
                        else { ObjAPH.APHMaxMarks = Convert.ToInt32(dr["APHMaxMarks"]); }
                        var MstEvaluationId = dr["MstEvaluationId"];
                        if (MstEvaluationId is DBNull) { }
                        else
                        {  ObjAPH.MstEvaluationId = Convert.ToInt32(dr["MstEvaluationId"]); }
                        ObjAPH.EvaluationName = (dr["EvaluationName"].ToString());
                        ObjLstAPH.Add(ObjAPH);
                    }

                }
                return Return.returnHttp("200", ObjLstAPH, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region ResDefineAPHGetById
        [HttpPost]
        public HttpResponseMessage ResDefineAPHGetById(ResDefineAPH ObjDefineAPH)
        {
            ResDefineAPH modelobj = new ResDefineAPH();
            try
            {
                modelobj.Id = Convert.ToInt32(ObjDefineAPH.Id);
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("ResDefineAPHGetById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", modelobj.Id);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<ResDefineAPH> ObjLstAPH = new List<ResDefineAPH>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ResDefineAPH ObjAPH = new ResDefineAPH();
                        ObjAPH.Id = Convert.ToInt32(dr["Id"]);
                        ObjAPH.APHCode = (dr["APHCode"].ToString());
                        ObjAPH.APHName = (dr["APHName"].ToString());
                        ObjAPH.APHAbbrev = (dr["APHAbbrev"].ToString());
                        ObjAPH.APHDescription = (dr["APHDescription"].ToString());
                        var APHMinMarks = dr["APHMinMarks"];
                        if (APHMinMarks is DBNull) { }
                        else { ObjAPH.APHMinMarks = Convert.ToInt32(dr["APHMinMarks"]); }
                        var MstEvaluationId = dr["MstEvaluationId"];
                        if (MstEvaluationId is DBNull) { }
                        else { ObjAPH.MstEvaluationId = Convert.ToInt32(dr["MstEvaluationId"]); }                         
                        ObjAPH.EvaluationName = (dr["EvaluationName"].ToString());
                        ObjLstAPH.Add(ObjAPH);
                    }

                }
                return Return.returnHttp("200", ObjLstAPH, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion


        #region ResDefineAPHAdd
        [HttpPost]
        public HttpResponseMessage ResDefineAPHAdd(ResDefineAPH ObjAPH)
        {
            ResDefineAPH modelobj = new ResDefineAPH();

            try
            {
                modelobj.APHCode = ObjAPH.APHCode;
                modelobj.APHName = ObjAPH.APHName;
                modelobj.APHAbbrev = ObjAPH.APHAbbrev;
                modelobj.APHDescription = ObjAPH.APHDescription;
                //modelobj.APHMinMarks = ObjAPH.APHMinMarks;
                //modelobj.MstEvaluationId = ObjAPH.MstEvaluationId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                modelobj.CreatedBy = Convert.ToInt32(res);

                if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.APHCode)))
                {
                    return Return.returnHttp("201", "APH Code is null or empty", null);
                }
                else if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.APHName)))
                {
                    return Return.returnHttp("201", "APH Name is null or empty", null);
                }
                else if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.APHAbbrev)))
                {
                    return Return.returnHttp("201", "APH Abbrev is null or empty", null);
                }
                else if(string.IsNullOrWhiteSpace(Convert.ToString(modelobj.APHDescription)))
                {
                    return Return.returnHttp("201", "APH Description is null or empty", null);
                }
              else
                {
                    SqlCommand cmd = new SqlCommand("ResDefineAPHAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@APHCode", (modelobj.APHCode).Trim());
                    cmd.Parameters.AddWithValue("@APHName", (modelobj.APHName).Trim());
                    cmd.Parameters.AddWithValue("@APHAbbrev", (modelobj.APHAbbrev).Trim());
                    cmd.Parameters.AddWithValue("@APHDescription", (modelobj.APHDescription).Trim());
                    //cmd.Parameters.AddWithValue("@APHMinMarks", modelobj.APHMinMarks);
                    //cmd.Parameters.AddWithValue("@MstEvaluationId", modelobj.MstEvaluationId);
                    cmd.Parameters.AddWithValue("@CreatedBy", modelobj.CreatedBy);
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
            }
            catch (Exception e)
            {

                return Return.returnHttp("201", e.Message.ToString(), null);

            }


        }
        #endregion

        #region ResDefineAPHEdit
        [HttpPost]
        public HttpResponseMessage ResDefineAPHEdit(ResDefineAPH ObjAPH)
        {
            ResDefineAPH modelobj = new ResDefineAPH();

            try
            {
                modelobj.Id = Convert.ToInt32(ObjAPH.Id);
                modelobj.APHCode = ObjAPH.APHCode;
                modelobj.APHName = ObjAPH.APHName;
                modelobj.APHAbbrev = ObjAPH.APHAbbrev;
                modelobj.APHDescription = ObjAPH.APHDescription;
                //modelobj.APHMinMarks = ObjAPH.APHMinMarks;
                //modelobj.MstEvaluationId = ObjAPH.MstEvaluationId;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                modelobj.ModifiedBy = Convert.ToInt32(res);

                if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.APHCode)))
                {
                    return Return.returnHttp("201", "APH Code is null or empty", null);
                }
                else if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.APHName)))
                {
                    return Return.returnHttp("201", "APH Name is null or empty", null);
                }
                else if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.APHAbbrev)))
                {
                    return Return.returnHttp("201", "APH Abbrev is null or empty", null);
                }
                else if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.APHDescription)))
                {
                    return Return.returnHttp("201", "APH Description is null or empty", null);
                }
                else if (modelobj.Id <= 0)
                {
                    return Return.returnHttp("201", "Please Enter Id or valid Id", null);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("ResDefineAPHEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", modelobj.Id);
                    cmd.Parameters.AddWithValue("@APHCode", (modelobj.APHCode).Trim());
                    cmd.Parameters.AddWithValue("@APHName", (modelobj.APHName).Trim());
                    cmd.Parameters.AddWithValue("@APHAbbrev", (modelobj.APHAbbrev).Trim());
                    cmd.Parameters.AddWithValue("@APHDescription", (modelobj.APHDescription).Trim());
                    //cmd.Parameters.AddWithValue("@APHMinMarks", modelobj.APHMinMarks);
                    //cmd.Parameters.AddWithValue("@MstEvaluationId", modelobj.MstEvaluationId);
                    cmd.Parameters.AddWithValue("@ModifiedBy", modelobj.ModifiedBy);

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
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region ResDefineAPHDelete
        [HttpPost]
        public HttpResponseMessage ResDefineAPHDelete(ResDefineAPH ObjAPH)
        {
            ResDefineAPH modelobj = new ResDefineAPH();

            try
            {
                modelobj.Id = Convert.ToInt32(ObjAPH.Id);
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                if (modelobj.Id <= 0)
                {
                    return Return.returnHttp("201", "Please Enter Id or valid Id", null);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("ResDefineAPHDelete", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", modelobj.Id);

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

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion
    }
}
