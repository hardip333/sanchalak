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
    public class ResConfigAPHController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();
        SqlTransaction ST;

        #region ResConfigAPHGet
        [HttpPost]
        public HttpResponseMessage ResConfigAPHGet()
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

                SqlCommand cmd = new SqlCommand("ResConfigAPHGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<ResConfigAPH> ObjLstAPHConfig = new List<ResConfigAPH>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ResConfigAPH ObjAPHConfig = new ResConfigAPH();
                        ObjAPHConfig.Id = Convert.ToInt32(dr["Id"]);
                        ObjAPHConfig.IncProgrammeInstancePartTermId = Convert.ToInt64(dr["IncProgrammeInstancePartTermId"]);
                        ObjAPHConfig.APHId = Convert.ToInt32(dr["APHId"]);
                        ObjAPHConfig.MstPaperId = Convert.ToInt64(dr["MstPaperId"]);
                        ObjAPHConfig.MstPaperTeachingLearningMapId = Convert.ToInt64(dr["MstPaperTeachingLearningMapId"]);
                        ObjAPHConfig.PaperMaxMarks = Convert.ToInt32(dr["PaperMaxMarks"]);
                        
                        ObjLstAPHConfig.Add(ObjAPHConfig);
                    }

                }
                return Return.returnHttp("200", ObjLstAPHConfig, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region ResConfigAPHGetByAPHId
        [HttpPost]
        public HttpResponseMessage ResConfigAPHGetByAPHId(ResConfigAPH ObjAPHConfg)
        {
            ResConfigAPH modelobj = new ResConfigAPH();
            try
            {
                modelobj.APHId = ObjAPHConfg.APHId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("ResConfigAPHGetByAPHId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@APHId", modelobj.APHId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<ResConfigAPH> ObjLstAPHConfig = new List<ResConfigAPH>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ResConfigAPH ObjAPHConfig = new ResConfigAPH();
                        var Id = dr["Id"];
                        if (Id is DBNull) { }
                        else { ObjAPHConfig.Id = Convert.ToInt32(dr["Id"]); }                          
                        var IncProgrammeInstancePartTermId = dr["IncProgrammeInstancePartTermId"];
                        if (IncProgrammeInstancePartTermId is DBNull) { }
                        else { ObjAPHConfig.IncProgrammeInstancePartTermId = Convert.ToInt64(dr["IncProgrammeInstancePartTermId"]); }
                        var APHId = dr["APHId"];
                        if (APHId is DBNull) { }
                        else { ObjAPHConfig.APHId = Convert.ToInt32(dr["APHId"]); }                  
                        ObjAPHConfig.MstPaperId = Convert.ToInt64(dr["MstPaperId"]);
                        ObjAPHConfig.MstPaperTeachingLearningMapId = Convert.ToInt64(dr["MstPaperTeachingLearningMapId"]);
                        ObjAPHConfig.PaperMaxMarks = Convert.ToInt32(dr["PaperMaxMarks"]);
                        ObjAPHConfig.ProgrammeInstanceId = Convert.ToInt64(dr["ProgrammeInstanceId"]);
                        ObjAPHConfig.IncProgrammeInstancePartId = Convert.ToInt64(dr["ProgrammeInstancePartId"]);
                        ObjAPHConfig.FacultyId = Convert.ToInt32(dr["FacultyId"]);
                        ObjAPHConfig.APHMinMarks = Convert.ToInt32(dr["APHMinMarks"]);
                        ObjAPHConfig.MstEvaluationId = Convert.ToInt32(dr["MstEvaluationId"]);
                        var MstEvaluationId = dr["MstEvaluationId"];
                        if (MstEvaluationId is 3) { }
                        else
                        {
                            var GradeScaleId = dr["GradeScaleId"];
                            if (GradeScaleId is DBNull)
                                ObjAPHConfig.GradeScaleId = 0;
                            else
                                ObjAPHConfig.GradeScaleId = Convert.ToInt32(dr["GradeScaleId"]);
                        }
                        ObjAPHConfig.ClassGradeTemplateId = Convert.ToInt32(dr["ClassGradeTemplateId"]);
                        ObjAPHConfig.PaperName = Convert.ToString(dr["PaperName"]);
                        ObjAPHConfig.AssessmentType = Convert.ToString(dr["AssessmentType"]);
                        ObjAPHConfig.TeachingLearningMethodName = Convert.ToString(dr["TeachingLearningMethodName"]);
                        ObjAPHConfig.AssessmentMethodName = Convert.ToString(dr["AssessmentMethodName"]);
                        ObjAPHConfig.PaperFlag = Convert.ToBoolean(dr["PaperFlag"]);
                        ObjLstAPHConfig.Add(ObjAPHConfig);
                    }

                }
                return Return.returnHttp("200", ObjLstAPHConfig, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region ResIncProgInstPartTermPaperMapGetByPartTermId
        [HttpPost]
        public HttpResponseMessage ResIncProgInstPartTermPaperMapGetByPTId(ResConfigAPH ObjAPHConfg)
        {
            ResConfigAPH modelobj = new ResConfigAPH();
            try
            {
                modelobj.IncProgrammeInstancePartTermId = ObjAPHConfg.IncProgrammeInstancePartTermId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("ResIncProgInstPartTermPaperMapGetByPartTermId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IncProgrammeInstancePartTermId", modelobj.IncProgrammeInstancePartTermId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<ResConfigAPH> ObjLstAPHConfig = new List<ResConfigAPH>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ResConfigAPH ObjAPHConfig = new ResConfigAPH();

                        ObjAPHConfig.PaperCode = (dr["PaperCode"].ToString());
                        ObjAPHConfig.PaperName = (dr["PaperName"].ToString());

                        ObjLstAPHConfig.Add(ObjAPHConfig);
                    }

                }
                return Return.returnHttp("200", ObjLstAPHConfig, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion


        #region ResIncProgrammeInfoGet
        [HttpPost]
        public HttpResponseMessage ResIncProgrammeInfoGet(ResConfigAPH ObjAPHConfg)
        {
            ResConfigAPH modelobj = new ResConfigAPH();
            try
            {
                modelobj.IncProgrammeInstancePartTermId = ObjAPHConfg.IncProgrammeInstancePartTermId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("ResIncProgrammeInfoGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IncProgrammeInstancePartTermId", modelobj.IncProgrammeInstancePartTermId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<ResConfigAPH> ObjLstAPHConfig = new List<ResConfigAPH>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ResConfigAPH ObjAPHConfig = new ResConfigAPH();
                        //var ProgrammeInstanceId = dr["ProgrammeInstanceId"];
                        //if (ProgrammeInstanceId is DBNull) { }
                        //else { ObjAPHConfig.ProgrammeInstanceId = Convert.ToInt64(dr["ProgrammeInstanceId"]); }
                        //ObjAPHConfig.InstanceName = (dr["InstanceName"].ToString());
                        //var IncProgrammeInstancePartId = dr["IncProgrammeInstancePartId"];
                        //if (IncProgrammeInstancePartId is DBNull) { }
                        //else { ObjAPHConfig.IncProgrammeInstancePartId = Convert.ToInt64(dr["IncProgrammeInstancePartId"]); }
                        //ObjAPHConfig.PartName = (dr["PartName"].ToString());
                        //var IncProgrammeInstancePartTermId = dr["IncProgrammeInstancePartTermId"];
                        //if (IncProgrammeInstancePartTermId is DBNull) { }
                        //else { ObjAPHConfig.IncProgrammeInstancePartTermId = Convert.ToInt64(dr["IncProgrammeInstancePartTermId"]); }
                        //ObjAPHConfig.InstancePartTermName = (dr["InstancePartTermName"].ToString());
                        var MstPaperId = dr["MstPaperId"];
                        if (MstPaperId is DBNull) { }
                        else { ObjAPHConfig.MstPaperId = Convert.ToInt64(dr["MstPaperId"]); }
                        ObjAPHConfig.PaperName = (dr["PaperName"].ToString());
                        var MstPaperTeachingLearningMapId = dr["MstPaperTeachingLearningMapId"];
                        if (MstPaperTeachingLearningMapId is DBNull) { }
                        else { ObjAPHConfig.MstPaperTeachingLearningMapId = Convert.ToInt64(dr["MstPaperTeachingLearningMapId"]); }
                        ObjAPHConfig.AssessmentType = (dr["AssessmentType"].ToString());
                        var AssessmentMethodId = dr["AssessmentMethodId"];
                        if (AssessmentMethodId is DBNull) { }
                        else { ObjAPHConfig.AssessmentMethodId = Convert.ToInt32(dr["AssessmentMethodId"]); }
                        var PaperMaxMarks = dr["PaperMaxMarks"];
                        if (PaperMaxMarks is DBNull) { }
                        else { ObjAPHConfig.PaperMaxMarks = Convert.ToInt32(dr["PaperMaxMarks"]); }
                        ObjAPHConfig.TeachingLearningMethodName = (dr["TeachingLearningMethodName"].ToString());
                        ObjAPHConfig.AssessmentMethodName = (dr["AssessmentMethodName"].ToString());

                        ObjLstAPHConfig.Add(ObjAPHConfig);
                    }

                }
                return Return.returnHttp("200", ObjLstAPHConfig, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region ResIncProgrammeInstanceGetByFacultyId
        [HttpPost]
        public HttpResponseMessage ResIncProgrammeInsGetByFacId(ResConfigAPH ObjAPHConfg)
        {
            ResConfigAPH modelobj = new ResConfigAPH();
            try
            {
                modelobj.FacultyId = ObjAPHConfg.FacultyId;
                modelobj.AcademicYearId = ObjAPHConfg.AcademicYearId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("ResIncProgrammeInstanceGetByFacultyId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", modelobj.FacultyId);
                cmd.Parameters.AddWithValue("@AcademicYearId", modelobj.AcademicYearId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<ResConfigAPH> ObjLstRCAPH = new List<ResConfigAPH>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ResConfigAPH ObjRCAPH = new ResConfigAPH();
                        ObjRCAPH.Id = Convert.ToInt32(dr["Id"]);
                        ObjRCAPH.InstanceName = Convert.ToString(dr["InstanceName"]);

                        ObjLstRCAPH.Add(ObjRCAPH);
                    }

                }
                return Return.returnHttp("200", ObjLstRCAPH, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        //#region ResIncProgrammeInfoGet
        //[HttpPost]
        //public HttpResponseMessage ResIncProgrammeInfoGet(ResConfigAPH ObjAPHConfg)
        //{
        //    ResConfigAPH modelobj = new ResConfigAPH();
        //    try
        //    {
        //        modelobj.ProgrammeInstanceId = ObjAPHConfg.ProgrammeInstanceId;
        //        String token = Request.Headers.GetValues("token").FirstOrDefault();
        //        TokenOperation ac = new TokenOperation();
        //        string res = ac.ValidateToken(token);

        //        if (res == "0")
        //        {
        //            return Return.returnHttp("0", null, null);
        //        }

        //        SqlCommand cmd = new SqlCommand("ResIncProgrammeInfoGet", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@ProgrammeInstanceId", modelobj.ProgrammeInstanceId);
        //        sda.SelectCommand = cmd;
        //        sda.Fill(dt);
        //        List<ResConfigAPH> ObjLstAPHConfig = new List<ResConfigAPH>();

        //        if (dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                ResConfigAPH ObjAPHConfig = new ResConfigAPH();
        //                var ProgrammeInstanceId = dr["ProgrammeInstanceId"];
        //                if (ProgrammeInstanceId is DBNull) { }
        //                else { ObjAPHConfig.ProgrammeInstanceId = Convert.ToInt64(dr["ProgrammeInstanceId"]); }                     
        //                ObjAPHConfig.InstanceName = (dr["InstanceName"].ToString());
        //                var IncProgrammeInstancePartId = dr["IncProgrammeInstancePartId"];
        //                if (IncProgrammeInstancePartId is DBNull) { }
        //                else { ObjAPHConfig.IncProgrammeInstancePartId = Convert.ToInt64(dr["IncProgrammeInstancePartId"]); }
        //                ObjAPHConfig.PartName = (dr["PartName"].ToString());
        //                var IncProgrammeInstancePartTermId = dr["IncProgrammeInstancePartTermId"];
        //                if (IncProgrammeInstancePartTermId is DBNull) { }
        //                else { ObjAPHConfig.IncProgrammeInstancePartTermId = Convert.ToInt64(dr["IncProgrammeInstancePartTermId"]); }
        //                ObjAPHConfig.InstancePartTermName = (dr["InstancePartTermName"].ToString());
        //                var MstPaperId = dr["MstPaperId"];
        //                if (MstPaperId is DBNull) { }
        //                else { ObjAPHConfig.MstPaperId = Convert.ToInt64(dr["MstPaperId"]); }
        //                ObjAPHConfig.PaperName = (dr["PaperName"].ToString());
        //                var MstPaperTeachingLearningMapId = dr["MstPaperTeachingLearningMapId"];
        //                if (MstPaperTeachingLearningMapId is DBNull) { }
        //                else { ObjAPHConfig.MstPaperTeachingLearningMapId = Convert.ToInt64(dr["MstPaperTeachingLearningMapId"]); }
        //                ObjAPHConfig.AssessmentType = (dr["AssessmentType"].ToString());
        //                var AssessmentMethodId = dr["AssessmentMethodId"];
        //                if (AssessmentMethodId is DBNull) { }
        //                else { ObjAPHConfig.AssessmentMethodId = Convert.ToInt32(dr["AssessmentMethodId"]); }
        //                var PaperMaxMarks = dr["PaperMaxMarks"];
        //                if (PaperMaxMarks is DBNull) { }
        //                else { ObjAPHConfig.PaperMaxMarks = Convert.ToInt32(dr["PaperMaxMarks"]); }
        //                ObjAPHConfig.TeachingLearningMethodName = (dr["TeachingLearningMethodName"].ToString());
        //                ObjAPHConfig.AssessmentMethodName = (dr["AssessmentMethodName"].ToString());

        //                ObjLstAPHConfig.Add(ObjAPHConfig);
        //            }

        //        }
        //        return Return.returnHttp("200", ObjLstAPHConfig, null);

        //    }
        //    catch (Exception e)
        //    {
        //        return Return.returnHttp("201", e.Message.ToString(), null);

        //    }
        //}
        //#endregion

        #region ResIncProgrammeInstancePartGetByProgInstId
        [HttpPost]
        public HttpResponseMessage ResIncProgInsPartGetByProgInstId(ResConfigAPH ObjAPHConfg)
        {
            ResConfigAPH modelobj = new ResConfigAPH();
            try
            {
                modelobj.ProgrammeInstanceId = ObjAPHConfg.ProgrammeInstanceId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("ResIncProgrammeInstancePartGetByProgInstId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgramInstanceId", modelobj.ProgrammeInstanceId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<ResConfigAPH> ObjLstAPHConfig = new List<ResConfigAPH>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ResConfigAPH ObjAPHConfig = new ResConfigAPH();
                       
                        ObjAPHConfig.Id = Convert.ToInt64(dr["Id"]); 
                        ObjAPHConfig.PartName = (dr["PartName"].ToString());
                        ObjAPHConfig.PartShortName = (dr["PartShortName"].ToString());

                        ObjLstAPHConfig.Add(ObjAPHConfig);
                    }

                }
                return Return.returnHttp("200", ObjLstAPHConfig, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region ResIncProgrammeInstancePartTermGetByProgInstPartId
        [HttpPost]
        public HttpResponseMessage ResIncProgInsPartTermGetByProgInstPartId(ResConfigAPH ObjAPHConfg)
        {
            ResConfigAPH modelobj = new ResConfigAPH();
            try
            {
                modelobj.IncProgrammeInstancePartId = ObjAPHConfg.IncProgrammeInstancePartId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("ResIncProgrammeInstancePartTermGetByProgInstPartId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgramInstancePartId", modelobj.IncProgrammeInstancePartId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<ResConfigAPH> ObjLstAPHConfig = new List<ResConfigAPH>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ResConfigAPH ObjAPHConfig = new ResConfigAPH();

                        ObjAPHConfig.Id = Convert.ToInt64(dr["Id"]);
                        ObjAPHConfig.InstancePartTermName = (dr["InstancePartTermName"].ToString());
                     
                        ObjLstAPHConfig.Add(ObjAPHConfig);
                    }

                }
                return Return.returnHttp("200", ObjLstAPHConfig, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region ResConfigAPHAdd
        [HttpPost]
        public HttpResponseMessage ResConfigAPHAdd(ResConfigAPH ObjAPHConfig)
        {
            ResConfigAPH modelobj = new ResConfigAPH();

            try
            {
                modelobj.IncProgrammeInstancePartTermId = ObjAPHConfig.IncProgrammeInstancePartTermId;
                modelobj.APHId = ObjAPHConfig.APHId;               
                modelobj.APHMinMarks = ObjAPHConfig.APHMinMarks;            
                modelobj.MstEvaluationId = ObjAPHConfig.MstEvaluationId;
                modelobj.ClassGradeTemplateId = ObjAPHConfig.ClassGradeTemplateId;
                var APHConfList = ObjAPHConfig.APHConfgList;
                if (modelobj.IncProgrammeInstancePartTermId <= 0)
                {
                    return Return.returnHttp("201", "Please enter valid Programme Instance Part Term Id", null);
                }
                else if (modelobj.APHId <= 0)
                {
                    return Return.returnHttp("201", "Please enter valid APH Id", null);
                }
                else if (modelobj.APHMinMarks < 0)
                {
                    return Return.returnHttp("201", "Please enter valid APH Min Marks", null);
                }
                else if (modelobj.MstEvaluationId <= 0)
                {
                    return Return.returnHttp("201", "Please enter valid Evaluation Id", null);
                }
                else if (modelobj.ClassGradeTemplateId <= 0)
                {
                    return Return.returnHttp("201", "Please enter valid Template Id", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjAPHConfig.APHConfgList)))
                {
                    return Return.returnHttp("201", "Paper List is empty or null", null);
                }
                else
                {
                    SqlCommand[] APHConfgArray = new SqlCommand[APHConfList.Count()];
                var i = 0;
                String msg = "";

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                modelobj.CreatedBy = Convert.ToInt32(res);
               
                    foreach (APHConfigList objAPHCon in APHConfList)
                    {
                        Int64 Id = objAPHCon.Id;
                        Int64 IncProgrammeInstancePartTermId = ObjAPHConfig.IncProgrammeInstancePartTermId;
                        Int32 APHId = ObjAPHConfig.APHId;
                        Int64 MstPaperId = objAPHCon.MstPaperId;
                        Int64 MstPaperTeachingLearningMapId = objAPHCon.MstPaperTeachingLearningMapId;
                        Int32 PaperMaxMarks = objAPHCon.PaperMaxMarks;
                        bool CheckPaper = objAPHCon.CheckPaper;
                       
                        if (CheckPaper == true)
                        {
                            modelobj.APHMaxMarks = modelobj.APHMaxMarks + PaperMaxMarks;
                            APHConfgArray[i] = new SqlCommand("ResConfigAPHAdd", con, ST);
                            APHConfgArray[i].CommandType = CommandType.StoredProcedure;
                            APHConfgArray[i].Parameters.AddWithValue("@Id", Id);
                            APHConfgArray[i].Parameters.AddWithValue("@IncProgrammeInstancePartTermId", IncProgrammeInstancePartTermId);
                            APHConfgArray[i].Parameters.AddWithValue("@APHId", APHId);
                            APHConfgArray[i].Parameters.AddWithValue("@MstPaperId", MstPaperId);
                            APHConfgArray[i].Parameters.AddWithValue("@MstPaperTeachingLearningMapId", MstPaperTeachingLearningMapId);
                            APHConfgArray[i].Parameters.AddWithValue("@PaperMaxMarks", PaperMaxMarks);
                            APHConfgArray[i].Parameters.AddWithValue("@CreatedBy", modelobj.CreatedBy);
                            APHConfgArray[i].Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                            APHConfgArray[i].Parameters["@Message"].Direction = ParameterDirection.Output;
                            i++;
                        }

                    }
                    SqlCommand cmd = new SqlCommand("ResDefineAPHEditFromResConfigAPH", con, ST);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", modelobj.APHId);
                    cmd.Parameters.AddWithValue("@APHMinMarks", modelobj.APHMinMarks);
                    cmd.Parameters.AddWithValue("@APHMaxMarks", modelobj.APHMaxMarks);
                    cmd.Parameters.AddWithValue("@MstEvaluationId", modelobj.MstEvaluationId);
                    cmd.Parameters.AddWithValue("@ClassGradeTemplateId", modelobj.ClassGradeTemplateId);
                    cmd.Parameters.AddWithValue("@ModifiedBy", modelobj.CreatedBy);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    con.Open();

                    ST = con.BeginTransaction();
                    try
                    {
                        cmd.Transaction = ST;
                        cmd.ExecuteNonQuery();
                        for (int b = 0; b < i; b++)
                        {
                            APHConfgArray[b].Transaction = ST;
                            int ans1 = APHConfgArray[b].ExecuteNonQuery();
                            msg = msg + ", " + Convert.ToString(APHConfgArray[b].Parameters["@Message"].Value);
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

        #region ResConfigAPHDelete
        [HttpPost]
        public HttpResponseMessage ResConfigAPHDelete(ResConfigAPH ObjAPHConfig)
        {
            ResConfigAPH modelobj = new ResConfigAPH();

            try
            {
                modelobj.APHId = ObjAPHConfig.APHId;
                var APHConfList = ObjAPHConfig.APHConfgList;
                if (modelobj.APHId <= 0)
                {
                    return Return.returnHttp("201", "Please enter valid APH Id", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjAPHConfig.APHConfgList)))
                {
                    return Return.returnHttp("201", "APH Configuration is empty or null", null);
                }
                else
                {
                    SqlCommand[] APHConfgArray = new SqlCommand[APHConfList.Count()];
                    var i = 0;
                    String msg = "";

                    String token = Request.Headers.GetValues("token").FirstOrDefault();
                    TokenOperation ac = new TokenOperation();
                    string res = ac.ValidateToken(token);

                    if (res == "0")
                    {
                        return Return.returnHttp("0", null, null);
                    }
                    modelobj.ModifiedBy = Convert.ToInt32(res);

                    foreach (APHConfigList objAPHCon in APHConfList)
                    {
                        Int64 Id = objAPHCon.Id;
                        APHConfgArray[i] = new SqlCommand("ResConfigAPHDelete", con, ST);
                        APHConfgArray[i].CommandType = CommandType.StoredProcedure;
                        APHConfgArray[i].Parameters.AddWithValue("@Id", Id);
                        APHConfgArray[i].Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                        APHConfgArray[i].Parameters["@Message"].Direction = ParameterDirection.Output;
                        i++;
                    }
                    SqlCommand cmd = new SqlCommand("ResDefineAPHEditFromResConfigAPH", con, ST);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", modelobj.APHId);
                    cmd.Parameters.AddWithValue("@APHMinMarks", DBNull.Value);
                    cmd.Parameters.AddWithValue("@APHMaxMarks", DBNull.Value);
                    cmd.Parameters.AddWithValue("@MstEvaluationId", DBNull.Value);
                    cmd.Parameters.AddWithValue("@ClassGradeTemplateId", DBNull.Value);
                    cmd.Parameters.AddWithValue("@ModifiedBy", modelobj.ModifiedBy);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;


                    con.Open();
                    ST = con.BeginTransaction();
                    try
                    {
                        cmd.Transaction = ST;
                        cmd.ExecuteNonQuery();
                        for (int b = 0; b < i; b++)
                        {
                            APHConfgArray[b].Transaction = ST;
                            int ans1 = APHConfgArray[b].ExecuteNonQuery();
                            msg = msg + ", " + Convert.ToString(APHConfgArray[b].Parameters["@Message"].Value);
                        }

                        ST.Commit();
                        return Return.returnHttp("200", "Record deleted successfully", null);
                    }
                    catch (SqlException sqlError)
                    {
                        ST.Rollback();
                        String strMessageErr = Convert.ToString(sqlError);

                        return Return.returnHttp("201", "Record Deleted not exists", null);
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
