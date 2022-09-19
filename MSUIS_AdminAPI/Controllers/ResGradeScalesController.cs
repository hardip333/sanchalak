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
    public class ResGradeScalesController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

        #region ResGradeScalesGet
        [HttpPost]
        public HttpResponseMessage ResGradeScalesGet()
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

                SqlCommand cmd = new SqlCommand("ResGradeScalesGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<ResGradeScales> ObjLstGS = new List<ResGradeScales>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ResGradeScales ObjGS = new ResGradeScales();
                        ObjGS.Id = Convert.ToInt32(dr["Id"]);
                        ObjGS.GradeScaleName = (dr["GradeScaleName"].ToString());
                        ObjGS.MaxGradePoints = Convert.ToInt32(dr["MaxGradePoints"]);
                        ObjLstGS.Add(ObjGS);
                    }

                }
                return Return.returnHttp("200", ObjLstGS, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion


        #region ResGradeScalesAdd
        [HttpPost]
        public HttpResponseMessage ResGradeScalesAdd(ResGradeScales ObjGradeScale)
        {
            ResGradeScales modelobj = new ResGradeScales();

            try
            {
                modelobj.GradeScaleName = ObjGradeScale.GradeScaleName;
                modelobj.MaxGradePoints = ObjGradeScale.MaxGradePoints;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                modelobj.CreatedBy = Convert.ToInt32(res);

                if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.GradeScaleName)))
                {
                    return Return.returnHttp("201", "Grade Scale Name is null or empty", null);
                }
                else if (modelobj.MaxGradePoints <= 0)
                {
                    return Return.returnHttp("201", "Please Enter Maximum Grade Points or valid Grade Points", null);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("ResGradeScalesAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@GradeScaleName", (modelobj.GradeScaleName).Trim());
                    cmd.Parameters.AddWithValue("@MaxGradePoints", modelobj.MaxGradePoints);
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

        #region ResGradeScalesEdit
        [HttpPost]
        public HttpResponseMessage ResGradeScalesEdit(ResGradeScales ObjGradeScale)
        {
            ResGradeScales modelobj = new ResGradeScales();

            try
            {
                modelobj.Id = Convert.ToInt32(ObjGradeScale.Id);
                modelobj.GradeScaleName = ObjGradeScale.GradeScaleName;
                modelobj.MaxGradePoints = ObjGradeScale.MaxGradePoints;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                modelobj.ModifiedBy = Convert.ToInt32(res);

                if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.GradeScaleName)))
                {
                    return Return.returnHttp("201", "Grade Scale Name is null or empty", null);
                }
                else if (modelobj.Id <= 0)
                {
                    return Return.returnHttp("201", "Please Enter Id or valid Id", null);
                }
                else if (modelobj.MaxGradePoints <= 0)
                {
                    return Return.returnHttp("201", "Please Enter Maximum Grade Points or valid Grade Points", null);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("ResGradeScalesEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", modelobj.Id);
                    cmd.Parameters.AddWithValue("@GradeScaleName", (modelobj.GradeScaleName).Trim());
                    cmd.Parameters.AddWithValue("@MaxGradePoints", modelobj.MaxGradePoints);
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

        #region ResGradeScalesDelete
        [HttpPost]
        public HttpResponseMessage ResGradeScalesDelete(ResGradeScales ObjGradeScale)
        {
            ResGradeScales modelobj = new ResGradeScales();

            try
            {
                modelobj.Id = Convert.ToInt32(ObjGradeScale.Id);
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
                    SqlCommand cmd = new SqlCommand("ResGradeScalesDelete", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", modelobj.Id.ToString());

                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    con.Close();

                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Your data has been deleted.";
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
