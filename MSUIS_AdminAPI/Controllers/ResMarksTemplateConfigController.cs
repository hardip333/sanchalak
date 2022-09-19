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
    public class ResMarksTemplateConfigController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();
        SqlTransaction ST;

        #region ResMarksTemplateConfigGet
        [HttpPost]
        public HttpResponseMessage ResMarksTemplateConfigGet()
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

                SqlCommand cmd = new SqlCommand("ResMarksTemplateConfigGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<ResMarksTemplateConfig> ObjLstMTC = new List<ResMarksTemplateConfig>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ResMarksTemplateConfig ObjMTC = new ResMarksTemplateConfig();
                        ObjMTC.Id = Convert.ToInt32(dr["Id"]);
                        ObjMTC.MarkTemplateId = Convert.ToInt32(dr["MarkTemplateId"]);
                        ObjMTC.ClassId = Convert.ToInt32(dr["ClassId"]);
                        ObjMTC.RangeFrom = Convert.ToDecimal(dr["RangeFrom"]);
                        ObjMTC.RangeTo = Convert.ToDecimal(dr["RangeTo"]);
                        ObjLstMTC.Add(ObjMTC);
                    }

                }
                return Return.returnHttp("200", ObjLstMTC, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region ResMarksTemplateConfigGetById
        [HttpPost]
        public HttpResponseMessage ResMarksTemplateConfigGetById(ResMarksTemplateConfig ObjMarksTempConfig)
        {
            ResMarksTemplateConfig modelobj = new ResMarksTemplateConfig();
            try
            {
                modelobj.MarkTemplateId = Convert.ToInt32(ObjMarksTempConfig.MarkTemplateId);
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("ResMarksTemplateConfigGetById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MarkTemplateId", modelobj.MarkTemplateId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<ResMarksTemplateConfig> ObjLstMTC = new List<ResMarksTemplateConfig>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ResMarksTemplateConfig ObjMTC = new ResMarksTemplateConfig();
                        ObjMTC.Id = Convert.ToInt32(dr["Id"]);
                        ObjMTC.MarkTemplateId = Convert.ToInt32(dr["MarkTemplateId"]);
                        ObjMTC.ClassId = Convert.ToInt32(dr["ClassId"]);
                        ObjMTC.MarksLevelName = Convert.ToString(dr["MarksLevelName"]);
                        ObjMTC.RangeFrom = Convert.ToDecimal(dr["RangeFrom"]);
                        ObjMTC.RangeTo = Convert.ToDecimal(dr["RangeTo"]);
                        ObjLstMTC.Add(ObjMTC);
                    }

                }
                return Return.returnHttp("200", ObjLstMTC, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion


        #region ResMarksTemplateConfigAdd
        [HttpPost]
        public HttpResponseMessage ResMarksTemplateConfigAdd(ResMarksTemplateConfig ObjMarksTempConfig)
        {
            ResMarksTemplateConfig modelobj = new ResMarksTemplateConfig();

            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                var TemplateConfigList = ObjMarksTempConfig.List1;
                if (String.IsNullOrEmpty(Convert.ToString(TemplateConfigList)))
                {
                    return Return.returnHttp("201", "Template Config List is empty or null", null);
                }
                else
                {
                    SqlCommand[] TempConfArray = new SqlCommand[TemplateConfigList.Count()];
                var i = 0;
                String msg = "";             
                modelobj.CreatedBy = Convert.ToInt32(res);               
                        foreach (List objTempConfig in TemplateConfigList)
                        {
                            int Id = objTempConfig.Id;
                            int MarkTemplateId = objTempConfig.MarkTemplateId;
                            int ClassId = objTempConfig.ClassId;
                            Decimal RangeFrom = objTempConfig.RangeFrom;
                            Decimal RangeTo = objTempConfig.RangeTo;

                            TempConfArray[i] = new SqlCommand("ResMarksTemplateConfigAdd", con, ST);
                            TempConfArray[i].CommandType = CommandType.StoredProcedure;
                            TempConfArray[i].Parameters.AddWithValue("@Id", Id);
                            TempConfArray[i].Parameters.AddWithValue("@MarkTemplateId", MarkTemplateId);
                            TempConfArray[i].Parameters.AddWithValue("@ClassId", ClassId);
                            TempConfArray[i].Parameters.AddWithValue("@RangeFrom", RangeFrom);
                            TempConfArray[i].Parameters.AddWithValue("@RangeTo", RangeTo);
                            TempConfArray[i].Parameters.AddWithValue("@CreatedBy", modelobj.CreatedBy);
                            TempConfArray[i].Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                            TempConfArray[i].Parameters["@Message"].Direction = ParameterDirection.Output;
                            i++;

                        }
                        con.Open();
                        ST = con.BeginTransaction();
                        try
                        {
                            for (int b = 0; b < i; b++)
                            {
                                TempConfArray[b].Transaction = ST;
                                int ans1 = TempConfArray[b].ExecuteNonQuery();
                                msg = msg + ", " + Convert.ToString(TempConfArray[b].Parameters["@Message"].Value);
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

        #region ResMarksTemplateConfigEdit
        [HttpPost]
        public HttpResponseMessage ResMarksTemplateConfigEdit(ResMarksTemplateConfig ObjMarksTempConfig)
        {
            ResMarksTemplateConfig modelobj = new ResMarksTemplateConfig();

            try
            {
                modelobj.Id = Convert.ToInt32(ObjMarksTempConfig.Id);
                modelobj.MarkTemplateId = ObjMarksTempConfig.MarkTemplateId;
                modelobj.ClassId = ObjMarksTempConfig.ClassId;
                modelobj.RangeFrom = ObjMarksTempConfig.RangeFrom;
                modelobj.RangeTo = ObjMarksTempConfig.RangeTo;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                modelobj.ModifiedBy = Convert.ToInt32(res);

                if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.ClassId)))
                {
                    return Return.returnHttp("201", "Class Name is null or empty", null);
                }
                else if (modelobj.Id <= 0)
                {
                    return Return.returnHttp("201", "Please Enter Id or valid Id", null);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("ResMarksTemplateConfigEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", modelobj.Id);
                    cmd.Parameters.AddWithValue("@MarkTemplateId", modelobj.MarkTemplateId);
                    cmd.Parameters.AddWithValue("@ClassName", modelobj.ClassId);
                    cmd.Parameters.AddWithValue("@RangeFrom", modelobj.RangeFrom);
                    cmd.Parameters.AddWithValue("@RangeTo", modelobj.RangeTo);
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

        #region ResMarksTemplateConfigDelete
        [HttpPost]
        public HttpResponseMessage ResMarksTemplateConfigDelete(ResMarksTemplateConfig ObjMarksTempConfig)
        {
            ResMarksTemplateConfig modelobj = new ResMarksTemplateConfig();

            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                var TemplateConfigList = ObjMarksTempConfig.List1;
                if (String.IsNullOrEmpty(Convert.ToString(TemplateConfigList)))
                {
                    return Return.returnHttp("201", "Template Config List is empty or null", null);
                }
                else
                {
                    SqlCommand[] TempConfArray = new SqlCommand[TemplateConfigList.Count()];
                    var i = 0;
                    String msg = "";                  
                    foreach (List objTempConfig in TemplateConfigList)
                    {
                        int Id = objTempConfig.Id;

                        TempConfArray[i] = new SqlCommand("ResMarksTemplateConfigDelete", con, ST);
                        TempConfArray[i].CommandType = CommandType.StoredProcedure;
                        TempConfArray[i].Parameters.AddWithValue("@Id", Id);

                        TempConfArray[i].Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                        TempConfArray[i].Parameters["@Message"].Direction = ParameterDirection.Output;
                        i++;

                    }

                    con.Open();

                    ST = con.BeginTransaction();
                    try
                    {
                        for (int b = 0; b < i; b++)
                        {
                            TempConfArray[b].Transaction = ST;
                            int ans1 = TempConfArray[b].ExecuteNonQuery();
                            msg = msg + ", " + Convert.ToString(TempConfArray[b].Parameters["@Message"].Value);
                        }

                        ST.Commit();
                        return Return.returnHttp("200", "Record deleted successfully", null);
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
    }
}
