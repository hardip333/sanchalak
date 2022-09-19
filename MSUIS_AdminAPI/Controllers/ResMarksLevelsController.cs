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
    public class ResMarksLevelsController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

        #region ResMarksLevelsGet
        [HttpPost]
        public HttpResponseMessage ResMarksLevelsGet()
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

                SqlCommand cmd = new SqlCommand("ResMarksLevelsGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<ResMarksLevels> ObjLstRML = new List<ResMarksLevels>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ResMarksLevels ObjRML = new ResMarksLevels();
                        ObjRML.Id = Convert.ToInt32(dr["Id"]);
                        ObjRML.MarksLevelName = (dr["MarksLevelName"].ToString());
                        ObjLstRML.Add(ObjRML);
                    }

                }
                return Return.returnHttp("200", ObjLstRML, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion


        #region ResMarksLevelsAdd
        [HttpPost]
        public HttpResponseMessage ResMarksLevelsAdd(ResMarksLevels ObjMarksLevel)
        {
            ResMarksLevels modelobj = new ResMarksLevels();

            try
            {
                modelobj.MarksLevelName = ObjMarksLevel.MarksLevelName;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                modelobj.CreatedBy = Convert.ToInt32(res);

                if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.MarksLevelName)))
                {
                    return Return.returnHttp("201", "Marks Level Name is null or empty", null);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("ResMarksLevelsAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MarksLevelName", (modelobj.MarksLevelName).Trim());
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

        #region ResMarksLevelsEdit
        [HttpPost]
        public HttpResponseMessage ResMarksLevelsEdit(ResMarksLevels ObjMarksLevel)
        {
            ResMarksLevels modelobj = new ResMarksLevels();

            try
            {
                modelobj.Id = Convert.ToInt32(ObjMarksLevel.Id);
                modelobj.MarksLevelName = ObjMarksLevel.MarksLevelName;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                modelobj.ModifiedBy = Convert.ToInt32(res);

                if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.MarksLevelName)))
                {
                    return Return.returnHttp("201", "Marks Level Name is null or empty", null);
                }
                else if (modelobj.Id <= 0)
                {
                    return Return.returnHttp("201", "Please Enter Id or valid Id", null);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("ResMarksLevelsEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", modelobj.Id);
                    cmd.Parameters.AddWithValue("@MarksLevelName", (modelobj.MarksLevelName).Trim());
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

        #region ResMarksLevelsDelete
        [HttpPost]
        public HttpResponseMessage ResMarksLevelsDelete(ResMarksLevels ObjMarksLevel)
        {
            ResMarksLevels modelobj = new ResMarksLevels();

            try
            {
                modelobj.Id = Convert.ToInt32(ObjMarksLevel.Id);
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

                    SqlCommand cmd = new SqlCommand("ResMarksLevelsDelete", con);
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
                        strMessage = "Your data has been deleted successfully.";
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
