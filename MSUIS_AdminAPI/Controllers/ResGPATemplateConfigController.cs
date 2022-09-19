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
    public class ResGPATemplateConfigController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();
        SqlTransaction ST;

        #region ResGPATemplateConfigGet
        [HttpPost]
        public HttpResponseMessage ResGPATemplateConfigGet()
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

                SqlCommand cmd = new SqlCommand("ResGPATemplateConfigGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<ResGPATemplateConfig> ObjLstRGT = new List<ResGPATemplateConfig>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ResGPATemplateConfig ObjRGT = new ResGPATemplateConfig();
                        ObjRGT.Id = Convert.ToInt32(dr["Id"]);
                        ObjRGT.GradeTemplateId = Convert.ToInt32(dr["GradeTemplateId"]);
                        ObjRGT.GradeAbbrev = (dr["GradeAbbrev"].ToString());
                        ObjRGT.MarkPercentRangeFrom = Convert.ToDecimal(dr["MarkPercentRangeFrom"]);
                        ObjRGT.MarkPercentRangeTo = Convert.ToDecimal(dr["MarkPercentRangeTo"]);
                        ObjRGT.Status = (dr["Status"].ToString());
                        ObjRGT.GPAFrom = Convert.ToDecimal(dr["GPAFrom"]);
                        ObjRGT.GPATo = Convert.ToDecimal(dr["GPATo"]);
                        ObjRGT.GradePoint = Convert.ToSingle(dr["GradePoint"]);
                        ObjRGT.GradeLevelId = Convert.ToInt32(dr["GradeLevelId"]);
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

        #region ResGPATemplateConfigGetByGradeTemplateId
        [HttpPost]
        public HttpResponseMessage ResGPATemplateConfigGetByGradeTempId(ResGPATemplateConfig ObjGPATempConfig)
        {
            ResGPATemplateConfig modelobj = new ResGPATemplateConfig();

            try
            {
                modelobj.GradeTemplateId = ObjGPATempConfig.GradeTemplateId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("ResGPATemplateConfigGetByGradeTemplateId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GradeTemplateId", modelobj.GradeTemplateId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<ResGPATemplateConfig> ObjLstRGT = new List<ResGPATemplateConfig>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ResGPATemplateConfig ObjRGT = new ResGPATemplateConfig();
                        ObjRGT.Id = Convert.ToInt32(dr["Id"]);
                        ObjRGT.GradeTemplateId = Convert.ToInt32(dr["GradeTemplateId"]);
                        ObjRGT.GradeAbbrev = (dr["GradeAbbrev"].ToString());
                        var MarkPercentRangeFrom = dr["MarkPercentRangeFrom"];
                        if (MarkPercentRangeFrom is DBNull)
                        {
                            ObjRGT.MarkPercentRangeFrom = null;
                        }
                        else
                        {
                            ObjRGT.MarkPercentRangeFrom = Convert.ToDecimal(dr["MarkPercentRangeFrom"]);
                        }
                        var MarkPercentRangeTo = dr["MarkPercentRangeTo"];
                        if (MarkPercentRangeTo is DBNull)
                        {
                            ObjRGT.MarkPercentRangeTo = null;
                        }
                        else
                        {
                            ObjRGT.MarkPercentRangeTo = Convert.ToDecimal(dr["MarkPercentRangeTo"]);
                        }
                       
                        ObjRGT.Status = (dr["Status"].ToString());
                        var GPAFrom = dr["GPAFrom"];
                        if (GPAFrom is DBNull)
                        {
                            ObjRGT.GPAFrom = null;
                        }
                        else
                        {
                            ObjRGT.GPAFrom = Convert.ToDecimal(dr["GPAFrom"]);
                        }
                        var GPATo = dr["GPATo"];
                        if (GPATo is DBNull)
                        {
                            ObjRGT.GPATo = null;
                        }
                        else
                        {
                            ObjRGT.GPATo = Convert.ToDecimal(dr["GPATo"]);
                        }
                      
                        ObjRGT.GradePoint = Convert.ToSingle(dr["GradePoint"]);
                        ObjRGT.GradeLevelId = Convert.ToInt32(dr["GradeLevelId"]);
                        ObjRGT.GradeLevelName = Convert.ToString(dr["GradeLevelName"]);
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


        #region ResGPATemplateConfigAdd
        [HttpPost]
        public HttpResponseMessage ResGPATemplateConfigAdd(ResGPATemplateConfig ObjGPATempConfig)
        {
            ResGPATemplateConfig modelobj = new ResGPATemplateConfig();

            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                var GradeTempConfigList = ObjGPATempConfig.GTempConfigList;
                if (String.IsNullOrEmpty(Convert.ToString(GradeTempConfigList)))
                {
                    return Return.returnHttp("201", "Paper List is empty or null", null);
                }
                else
                {
                    SqlCommand[] GradeTempConfArray = new SqlCommand[GradeTempConfigList.Count()];
                    var i = 0;
                    String msg = "";

                    modelobj.CreatedBy = Convert.ToInt32(res);

                    foreach (GPATemplateList objGTempConfig in GradeTempConfigList)
                    {
                        int Id = objGTempConfig.Id;
                        int GradeTemplateId = objGTempConfig.GradeTemplateId;
                        string GradeAbbrev = objGTempConfig.GradeAbbrev;
                        decimal? MarkPercentRangeFrom = objGTempConfig.MarkPercentRangeFrom;
                        decimal? MarkPercentRangeTo = objGTempConfig.MarkPercentRangeTo;
                        string Status = objGTempConfig.Status;
                        decimal? GPAFrom = objGTempConfig.GPAFrom;
                        decimal? GPATo = objGTempConfig.GPATo;
                        float GradePoint = objGTempConfig.GradePoint;
                        int GradeLevelId = objGTempConfig.GradeLevelId;

                        GradeTempConfArray[i] = new SqlCommand("ResGPATemplateConfigAdd", con, ST);
                        GradeTempConfArray[i].CommandType = CommandType.StoredProcedure;
                        GradeTempConfArray[i].Parameters.AddWithValue("@Id", Id);
                        GradeTempConfArray[i].Parameters.AddWithValue("@GradeTemplateId", GradeTemplateId);
                        GradeTempConfArray[i].Parameters.AddWithValue("@GradeAbbrev", GradeAbbrev);
                        if (objGTempConfig.MarkPercentRangeFrom == null)
                            GradeTempConfArray[i].Parameters.AddWithValue("@MarkPercentRangeFrom", DBNull.Value);
                        else
                            GradeTempConfArray[i].Parameters.AddWithValue("@MarkPercentRangeFrom", MarkPercentRangeFrom);
                        if (objGTempConfig.MarkPercentRangeTo == null)
                            GradeTempConfArray[i].Parameters.AddWithValue("@MarkPercentRangeTo", DBNull.Value);
                        else
                            GradeTempConfArray[i].Parameters.AddWithValue("@MarkPercentRangeTo", MarkPercentRangeTo);
                        GradeTempConfArray[i].Parameters.AddWithValue("@Status", Status);
                        if (objGTempConfig.GPAFrom == null)
                            GradeTempConfArray[i].Parameters.AddWithValue("@GPAFrom", DBNull.Value);
                        else
                            GradeTempConfArray[i].Parameters.AddWithValue("@GPAFrom", GPAFrom);
                        if (objGTempConfig.GPATo == null)
                            GradeTempConfArray[i].Parameters.AddWithValue("@GPATo", DBNull.Value);
                        else
                            GradeTempConfArray[i].Parameters.AddWithValue("@GPATo", GPATo);
                        GradeTempConfArray[i].Parameters.AddWithValue("@GradePoint", GradePoint);
                        GradeTempConfArray[i].Parameters.AddWithValue("@GradeLevelId", GradeLevelId);
                        GradeTempConfArray[i].Parameters.AddWithValue("@CreatedBy", modelobj.CreatedBy);
                        GradeTempConfArray[i].Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                        GradeTempConfArray[i].Parameters["@Message"].Direction = ParameterDirection.Output;
                        i++;

                    }

                    con.Open();

                    ST = con.BeginTransaction();
                    try
                    {
                        for (int b = 0; b < i; b++)
                        {
                            GradeTempConfArray[b].Transaction = ST;
                            int ans1 = GradeTempConfArray[b].ExecuteNonQuery();
                            msg = msg + ", " + Convert.ToString(GradeTempConfArray[b].Parameters["@Message"].Value);
                        }

                        ST.Commit();
                        return Return.returnHttp("200", "Data has been saved successfully", null);
                    }
                    catch (SqlException sqlError)
                    {
                        ST.Rollback();
                        String strMessageErr = Convert.ToString(sqlError);
                        return Return.returnHttp("201", "Server Error", null);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            catch (Exception e)
            {

                return Return.returnHttp("201", e.Message.ToString(), null);

            }

        }
        #endregion

        #region ResGPATemplateConfigEdit
        [HttpPost]
        public HttpResponseMessage ResGPATemplateConfigEdit(ResGPATemplateConfig ObjGPATempConfig)
        {
            ResGPATemplateConfig modelobj = new ResGPATemplateConfig();

            try
            {
                modelobj.Id = Convert.ToInt32(ObjGPATempConfig.Id);
                modelobj.GradeTemplateId = ObjGPATempConfig.GradeTemplateId;
                modelobj.GradeAbbrev = ObjGPATempConfig.GradeAbbrev;
                modelobj.MarkPercentRangeFrom = ObjGPATempConfig.MarkPercentRangeFrom;
                modelobj.MarkPercentRangeTo = ObjGPATempConfig.MarkPercentRangeTo;
                modelobj.Status = ObjGPATempConfig.Status;
                modelobj.GPAFrom = ObjGPATempConfig.GPAFrom;
                modelobj.GPATo = ObjGPATempConfig.GPATo;
                modelobj.GradePoint = ObjGPATempConfig.GradePoint;
                modelobj.GradeLevelId = ObjGPATempConfig.GradeLevelId;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                modelobj.ModifiedBy = Convert.ToInt32(res);

                if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.GradeAbbrev)))
                {
                    return Return.returnHttp("201", "Grade Abbrev Name is null or empty", null);
                }
                else if (modelobj.Id <= 0)
                {
                    return Return.returnHttp("201", "Please Enter Id or valid Id", null);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("ResGPATemplateConfigEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", modelobj.Id);
                    cmd.Parameters.AddWithValue("@GradeTemplateId", modelobj.GradeTemplateId);
                    cmd.Parameters.AddWithValue("@GradeAbbrev", (modelobj.GradeAbbrev).Trim());
                    cmd.Parameters.AddWithValue("@MarkPercentRangeFrom", modelobj.MarkPercentRangeFrom);
                    cmd.Parameters.AddWithValue("@MarkPercentRangeTo", modelobj.MarkPercentRangeTo);
                    cmd.Parameters.AddWithValue("@Status", modelobj.Status);
                    cmd.Parameters.AddWithValue("@GPAFrom", modelobj.GPAFrom);
                    cmd.Parameters.AddWithValue("@GPATo", modelobj.GPATo);
                    cmd.Parameters.AddWithValue("@GradePoint", modelobj.GradePoint);
                    cmd.Parameters.AddWithValue("@GradeLevelId", modelobj.GradeLevelId);
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
                    if (modelobj.Id < 1)
                    {

                        return Return.returnHttp("202", "ok", null);
                    }
                    else
                    {

                        return Return.returnHttp("200", strMessage.ToString(), null);

                    }

                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region ResGPATemplateConfigDelete
        [HttpPost]
        public HttpResponseMessage ResGPATemplateConfigDelete(ResGPATemplateConfig ObjGPATempConfig)
        {
            ResGPATemplateConfig modelobj = new ResGPATemplateConfig();

            try
            {
                var GradeTempConfigList = ObjGPATempConfig.GTempConfigList;
                if (String.IsNullOrEmpty(Convert.ToString(GradeTempConfigList)))
                {
                    return Return.returnHttp("201", "Paper List is empty or null", null);
                }
                else
                {
                    SqlCommand[] GradeTempConfArray = new SqlCommand[GradeTempConfigList.Count()];
                    var i = 0;
                    String msg = "";

                    String token = Request.Headers.GetValues("token").FirstOrDefault();
                    TokenOperation ac = new TokenOperation();
                    string res = ac.ValidateToken(token);

                    if (res == "0")
                    {
                        return Return.returnHttp("0", null, null);
                    }
                    foreach (GPATemplateList objGTempConfig in GradeTempConfigList)
                    {
                        int Id = objGTempConfig.Id;
                        GradeTempConfArray[i] = new SqlCommand("ResGPATemplateConfigDelete", con, ST);
                        GradeTempConfArray[i].CommandType = CommandType.StoredProcedure;
                        GradeTempConfArray[i].Parameters.AddWithValue("@Id", Id);
                        GradeTempConfArray[i].Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                        GradeTempConfArray[i].Parameters["@Message"].Direction = ParameterDirection.Output;
                        i++;

                    }

                    con.Open();

                    ST = con.BeginTransaction();
                    try
                    {
                        for (int b = 0; b < i; b++)
                        {
                            GradeTempConfArray[b].Transaction = ST;
                            int ans1 = GradeTempConfArray[b].ExecuteNonQuery();
                            msg = msg + ", " + Convert.ToString(GradeTempConfArray[b].Parameters["@Message"].Value);
                        }

                        ST.Commit();
                        return Return.returnHttp("200", "Record deleted successfully", null);
                    }
                    catch (SqlException sqlError)
                    {
                        ST.Rollback();
                        String strMessageErr = Convert.ToString(sqlError);

                        return Return.returnHttp("201", "Record not Deleted", null);
                    }
                    finally
                    {
                        con.Close();
                    }
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
