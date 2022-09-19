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
    public class ResMarksTemplateController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

        #region ResMarksTemplateGet
        [HttpPost]
        public HttpResponseMessage ResMarksTemplateGet()
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

                SqlCommand cmd = new SqlCommand("ResMarksTemplateGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<ResMarksTemplate> ObjLstMT = new List<ResMarksTemplate>();
                Int32 count = 0;
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ResMarksTemplate ObjMT = new ResMarksTemplate();
                        ObjMT.Id = Convert.ToInt32(dr["Id"]);
                        ObjMT.NoofIntervals = Convert.ToInt32(dr["NoofIntervals"]);
                        ObjMT.MarksTemplateName = (dr["MarksTemplateName"].ToString());
                        ObjMT.MarksTemplateDescription = (dr["MarksTemplateDescription"].ToString());
                        ObjMT.TemplateUsedwith = (dr["TemplateUsedwith"].ToString());
                        count = count + 1;
                        ObjMT.IndexId = count;
                        ObjLstMT.Add(ObjMT);
                    }

                }
                return Return.returnHttp("200", ObjLstMT, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region ResMarksTemplateGetById
        [HttpPost]
        public HttpResponseMessage ResMarksTemplateGetById(ResMarksTemplate ObjMarksTemp)
        {
            ResMarksTemplate modelobj = new ResMarksTemplate();
            try
            {
                modelobj.Id = Convert.ToInt32(ObjMarksTemp.Id);
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("ResMarksTemplateGetById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", modelobj.Id);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<ResMarksTemplate> ObjLstMT = new List<ResMarksTemplate>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ResMarksTemplate ObjMT = new ResMarksTemplate();
                        ObjMT.Id = Convert.ToInt32(dr["Id"]);
                        ObjMT.NoofIntervals = Convert.ToInt32(dr["NoofIntervals"]);
                        ObjMT.MarksTemplateName = (dr["MarksTemplateName"].ToString());
                        ObjMT.MarksTemplateDescription = (dr["MarksTemplateDescription"].ToString());
                        ObjMT.TemplateUsedwith = (dr["TemplateUsedwith"].ToString());
                        ObjLstMT.Add(ObjMT);
                    }

                }
                return Return.returnHttp("200", ObjLstMT, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion


        #region ResMarksTemplateAdd
        [HttpPost]
        public HttpResponseMessage ResMarksTemplateAdd(ResMarksTemplate ObjMarksTemp)
        {
            ResMarksTemplate modelobj = new ResMarksTemplate();

            try
            {
                modelobj.NoofIntervals = ObjMarksTemp.NoofIntervals;
                modelobj.MarksTemplateName = ObjMarksTemp.MarksTemplateName;
                modelobj.MarksTemplateDescription = ObjMarksTemp.MarksTemplateDescription;
                modelobj.TemplateUsedwith = ObjMarksTemp.TemplateUsedwith;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                modelobj.CreatedBy = Convert.ToInt32(res);

                if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.MarksTemplateName)))
                {
                    return Return.returnHttp("201", "Marks Template Name is null or empty", null);
                }
                else if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.MarksTemplateDescription)))
                {
                    return Return.returnHttp("201", "Description is null or empty", null);
                }
                else if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.TemplateUsedwith)))
                {
                    return Return.returnHttp("201", "Please Select Programme or course", null);
                }
                else if (modelobj.NoofIntervals <= 0)
                {
                    return Return.returnHttp("201", "Please Enter Number of Intervals or valid Number of Intervals", null);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("ResMarksTemplateAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NoofIntervals", modelobj.NoofIntervals);
                    cmd.Parameters.AddWithValue("@MarksTemplateName", (modelobj.MarksTemplateName).Trim());
                    cmd.Parameters.AddWithValue("@MarksTemplateDescription", modelobj.MarksTemplateDescription);
                    cmd.Parameters.AddWithValue("@TemplateUsedwith", modelobj.TemplateUsedwith);
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

        #region ResMarksTemplateEdit
        [HttpPost]
        public HttpResponseMessage ResMarksTemplateEdit(ResMarksTemplate ObjMarksTemp)
        {
            ResMarksTemplate modelobj = new ResMarksTemplate();

            try
            {
                modelobj.Id = Convert.ToInt32(ObjMarksTemp.Id);
                modelobj.NoofIntervals = ObjMarksTemp.NoofIntervals;
                modelobj.MarksTemplateName = ObjMarksTemp.MarksTemplateName;
                modelobj.MarksTemplateDescription = ObjMarksTemp.MarksTemplateDescription;
                modelobj.TemplateUsedwith = ObjMarksTemp.TemplateUsedwith;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                modelobj.ModifiedBy = Convert.ToInt32(res);

                if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.MarksTemplateName)))
                {
                    return Return.returnHttp("201", "Marks Template Name is null or empty", null);
                }
                else if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.MarksTemplateDescription)))
                {
                    return Return.returnHttp("201", "Description is null or empty", null);
                }
                else if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.TemplateUsedwith)))
                {
                    return Return.returnHttp("201", "Please Select Programme or course", null);
                }
                else if (modelobj.Id <= 0)
                {
                    return Return.returnHttp("201", "Please Enter Id or valid Id", null);
                }
                else if (modelobj.NoofIntervals <= 0)
                {
                    return Return.returnHttp("201", "Please Enter Number of Intervals or valid Number of Intervals", null);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("ResMarksTemplateEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", modelobj.Id);
                    cmd.Parameters.AddWithValue("@NoofIntervals", modelobj.NoofIntervals);
                    cmd.Parameters.AddWithValue("@MarksTemplateName", (modelobj.MarksTemplateName).Trim());
                    cmd.Parameters.AddWithValue("@MarksTemplateDescription", modelobj.MarksTemplateDescription);
                    cmd.Parameters.AddWithValue("@TemplateUsedwith", modelobj.TemplateUsedwith);
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

        #region ResMarksTemplateDelete
        [HttpPost]
        public HttpResponseMessage ResMarksTemplateDelete(ResMarksTemplate ObjMarksTemp)
        {
            ResMarksTemplate modelobj = new ResMarksTemplate();

            try
            {
                modelobj.Id = Convert.ToInt32(ObjMarksTemp.Id);
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
                    SqlCommand cmd = new SqlCommand("ResMarksTemplateDelete", con);
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
