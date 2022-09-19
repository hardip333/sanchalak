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
    public class ResGPATemplateController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

        #region ResMstEvaluationGet
        [HttpPost]
        public HttpResponseMessage ResMstEvaluationGet()
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

                SqlCommand cmd = new SqlCommand("ResMstEvaluationGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<ResGPATemplate> ObjLstRGT = new List<ResGPATemplate>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ResGPATemplate ObjRGT = new ResGPATemplate();
                        ObjRGT.Id = Convert.ToInt32(dr["Id"]);
                        ObjRGT.EvaluationName = (dr["EvaluationName"].ToString());                     
                        ObjLstRGT.Add(ObjRGT);
                    }

                }
                return Return.returnHttp("200", ObjLstRGT, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion


        #region ResGPATemplateGet
        [HttpPost]
        public HttpResponseMessage ResGPATemplateGet()
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

                SqlCommand cmd = new SqlCommand("ResGPATemplateGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<ResGPATemplate> ObjLstRGT = new List<ResGPATemplate>();
                Int32 count = 0;
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ResGPATemplate ObjRGT = new ResGPATemplate();
                        ObjRGT.Id = Convert.ToInt32(dr["Id"]);
                        ObjRGT.GradeScaleId = Convert.ToInt32(dr["GradeScaleId"]);
                        ObjRGT.MstEvaluationId = Convert.ToInt32(dr["MstEvaluationId"]);
                        ObjRGT.NoofIntervals = Convert.ToInt32(dr["NoofIntervals"]);
                        ObjRGT.GPATemplateName = (dr["GPATemplateName"].ToString());
                        ObjRGT.GPATemplateDescription = (dr["GPATemplateDescription"].ToString());
                        ObjRGT.GradeScaleName = (dr["GradeScaleName"].ToString());
                        ObjRGT.EvaluationName = (dr["EvaluationName"].ToString());
                        count = count + 1;
                        ObjRGT.IndexId = count;
                        ObjLstRGT.Add(ObjRGT);
                    }

                }
                return Return.returnHttp("200", ObjLstRGT, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region ResGPATemplateGetById
        [HttpPost]
        public HttpResponseMessage ResGPATemplateGetById(ResGPATemplate ObjGPATemplate)
        {
            ResGPATemplate modelobj = new ResGPATemplate();
            try
            {
                modelobj.Id = Convert.ToInt32(ObjGPATemplate.Id);
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("ResGPATemplateGetById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", modelobj.Id);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<ResGPATemplate> ObjLstRGT = new List<ResGPATemplate>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ResGPATemplate ObjRGT = new ResGPATemplate();
                        ObjRGT.Id = Convert.ToInt32(dr["Id"]);
                        ObjRGT.GradeScaleId = Convert.ToInt32(dr["GradeScaleId"]);
                        ObjRGT.MstEvaluationId = Convert.ToInt32(dr["MstEvaluationId"]);
                        ObjRGT.NoofIntervals = Convert.ToInt32(dr["NoofIntervals"]);
                        ObjRGT.GPATemplateName = (dr["GPATemplateName"].ToString());
                        ObjRGT.GPATemplateDescription = (dr["GPATemplateDescription"].ToString());
                        ObjRGT.GradeScaleName = (dr["GradeScaleName"].ToString());
                        ObjRGT.EvaluationName = (dr["EvaluationName"].ToString());
                        ObjLstRGT.Add(ObjRGT);
                    }

                }
                return Return.returnHttp("200", ObjLstRGT, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region ResGPATemplateGetByGradeScaleId
        [HttpPost]
        public HttpResponseMessage ResGPATemplateGetByGScaleId(ResGPATemplate ObjGPATemplate)
        {
            ResGPATemplate modelobj = new ResGPATemplate();
            try
            {
                modelobj.GradeScaleId = Convert.ToInt32(ObjGPATemplate.GradeScaleId);
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("ResGPATemplateGetByGradeScaleId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GradeScaleId", modelobj.GradeScaleId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<ResGPATemplate> ObjLstRGT = new List<ResGPATemplate>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ResGPATemplate ObjRGT = new ResGPATemplate();
                        ObjRGT.Id = Convert.ToInt32(dr["Id"]);
                        ObjRGT.GradeScaleId = Convert.ToInt32(dr["GradeScaleId"]);
                        ObjRGT.MstEvaluationId = Convert.ToInt32(dr["MstEvaluationId"]);
                        ObjRGT.NoofIntervals = Convert.ToInt32(dr["NoofIntervals"]);
                        ObjRGT.GPATemplateName = (dr["GPATemplateName"].ToString());
                        ObjRGT.GPATemplateDescription = (dr["GPATemplateDescription"].ToString());
                        ObjRGT.GradeScaleName = (dr["GradeScaleName"].ToString());
                        ObjRGT.EvaluationName = (dr["EvaluationName"].ToString());
                        ObjLstRGT.Add(ObjRGT);
                    }

                }
                return Return.returnHttp("200", ObjLstRGT, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region ResGPATemplateGetByGScaleIdAndEvalId
        [HttpPost]
        public HttpResponseMessage ResGPATemplateGetByGScaleIdAndEvalId(ResGPATemplate ObjGPATemplate)
        {
            ResGPATemplate modelobj = new ResGPATemplate();
            try
            {
                modelobj.MstEvaluationId = Convert.ToInt32(ObjGPATemplate.MstEvaluationId);
                modelobj.GradeScaleId = Convert.ToInt32(ObjGPATemplate.GradeScaleId);
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("ResGPATemplateGetByGScaleIdAndEvalId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MstEvaluationId", modelobj.MstEvaluationId);
                cmd.Parameters.AddWithValue("@GradeScaleId", modelobj.GradeScaleId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<ResGPATemplate> ObjLstRGT = new List<ResGPATemplate>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ResGPATemplate ObjRGT = new ResGPATemplate();
                        ObjRGT.Id = Convert.ToInt32(dr["Id"]);
                        ObjRGT.GradeScaleId = Convert.ToInt32(dr["GradeScaleId"]);
                        ObjRGT.MstEvaluationId = Convert.ToInt32(dr["MstEvaluationId"]);
                        ObjRGT.NoofIntervals = Convert.ToInt32(dr["NoofIntervals"]);
                        ObjRGT.GPATemplateName = (dr["GPATemplateName"].ToString());
                        ObjRGT.GPATemplateDescription = (dr["GPATemplateDescription"].ToString());
                        ObjRGT.GradeScaleName = (dr["GradeScaleName"].ToString());
                        ObjRGT.EvaluationName = (dr["EvaluationName"].ToString());
                        ObjLstRGT.Add(ObjRGT);
                    }

                }
                return Return.returnHttp("200", ObjLstRGT, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region ResGPATemplateAdd
        [HttpPost]
        public HttpResponseMessage ResGPATemplateAdd(ResGPATemplate ObjGPATemplate)
        {
            ResGPATemplate modelobj = new ResGPATemplate();

            try
            {
                modelobj.GradeScaleId = ObjGPATemplate.GradeScaleId;
                modelobj.MstEvaluationId = ObjGPATemplate.MstEvaluationId;
                modelobj.NoofIntervals = ObjGPATemplate.NoofIntervals;
                modelobj.GPATemplateName = ObjGPATemplate.GPATemplateName;
                modelobj.GPATemplateDescription = ObjGPATemplate.GPATemplateDescription;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                modelobj.CreatedBy = Convert.ToInt32(res);

                if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.GPATemplateName)))
                {
                    return Return.returnHttp("201", "GPA Template Name is null or empty", null);
                }
                else if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.GPATemplateDescription)))
                {
                    return Return.returnHttp("201", "GPA Template Description is null or empty", null);
                }
                else if (modelobj.NoofIntervals <= 0)
                {
                    return Return.returnHttp("201", "Please  Enter  number of Intervals or valid number of Intervals", null);
                }
                else if (modelobj.GradeScaleId <= 0)
                {
                    return Return.returnHttp("201", "Please Select Grade Scale Id or valid Grade Scale Id", null);
                }
                else if (modelobj.MstEvaluationId <= 0)
                {
                    return Return.returnHttp("201", "Please Select Evaluation Id or valid Evaluation Id", null);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("ResGPATemplateAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@GradeScaleId", modelobj.GradeScaleId);
                    cmd.Parameters.AddWithValue("@MstEvaluationId", modelobj.MstEvaluationId);
                    cmd.Parameters.AddWithValue("@NoofIntervals", modelobj.NoofIntervals);
                    cmd.Parameters.AddWithValue("@GPATemplateName", (modelobj.GPATemplateName).Trim());
                    cmd.Parameters.AddWithValue("@GPATemplateDescription", (modelobj.GPATemplateDescription).Trim());
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

        #region ResGPATemplateEdit
        [HttpPost]
        public HttpResponseMessage ResGPATemplateEdit(ResGPATemplate ObjGPATemplate)
        {
            ResGPATemplate modelobj = new ResGPATemplate();

            try
            {
                modelobj.Id = Convert.ToInt32(ObjGPATemplate.Id);
                modelobj.GradeScaleId = ObjGPATemplate.GradeScaleId;
                modelobj.MstEvaluationId = ObjGPATemplate.MstEvaluationId;
                modelobj.NoofIntervals = ObjGPATemplate.NoofIntervals;
                modelobj.GPATemplateName = ObjGPATemplate.GPATemplateName;
                modelobj.GPATemplateDescription = ObjGPATemplate.GPATemplateDescription;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                modelobj.ModifiedBy = Convert.ToInt32(res);

                if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.GPATemplateName)))
                {
                    return Return.returnHttp("201", "GPA Template Name is null or empty", null);
                }
                else if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.GPATemplateDescription)))
                {
                    return Return.returnHttp("201", "GPA Template Description is null or empty", null);
                }
                else if (modelobj.NoofIntervals <= 0)
                {
                    return Return.returnHttp("201", "Please  Enter  number of Intervals or valid number of Intervals", null);
                }
                else if (modelobj.GradeScaleId <= 0)
                {
                    return Return.returnHttp("201", "Please Select Grade Scale Id or valid Grade Scale Id", null);
                }
                else if (modelobj.MstEvaluationId <= 0)
                {
                    return Return.returnHttp("201", "Please Select Evaluation Id or valid Evaluation Id", null);
                }
                else if (modelobj.Id <= 0)
                {
                    return Return.returnHttp("201", "Please Enter Id or valid Id", null);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("ResGPATemplateEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", modelobj.Id);
                    cmd.Parameters.AddWithValue("@GradeScaleId", modelobj.GradeScaleId);
                    cmd.Parameters.AddWithValue("@MstEvaluationId", modelobj.MstEvaluationId);
                    cmd.Parameters.AddWithValue("@NoofIntervals", modelobj.NoofIntervals);
                    cmd.Parameters.AddWithValue("@GPATemplateName", (modelobj.GPATemplateName).Trim());
                    cmd.Parameters.AddWithValue("@GPATemplateDescription", (modelobj.GPATemplateDescription).Trim());
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

        #region ResGPATemplateDelete
        [HttpPost]
        public HttpResponseMessage ResGPATemplateDelete(ResGPATemplate ObjGPATemplate)
        {
            ResGPATemplate modelobj = new ResGPATemplate();

            try
            {
                modelobj.Id = Convert.ToInt32(ObjGPATemplate.Id);
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
                    SqlCommand cmd = new SqlCommand("ResGPATemplateDelete", con);
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
