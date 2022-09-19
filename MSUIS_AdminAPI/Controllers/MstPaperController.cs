using Newtonsoft.Json.Linq;
using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MSUIS_TokenManager.App_Start;
using System.Web.WebPages;
using MSUISApi.BAL;

namespace MSUISApi.Controllers
{
    public class MstPaperController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region Paper List Get
        [HttpGet]
        public HttpResponseMessage MstPaperGet()
        {
            /* String token = Request.Headers.GetValues("token").FirstOrDefault();
             TokenOperation ac = new TokenOperation();

             string res = ac.ValidateToken(token);

             if (res == "0")
             {
                 return Return.returnHttp("0", null, null);
             }*/

            try
            {
                SqlCommand Cmd = new SqlCommand("MstPaperGet", con);
                Cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = Cmd;

                da.Fill(dt);
                Int32 count = 0;
                List<MstPaper> ObjLstPaper = new List<MstPaper>();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        MstPaper objPaper = new MstPaper();
                        List<PaperTLMMapDetails> ObjMapDataLst = new List<PaperTLMMapDetails>();

                        objPaper.Id = Convert.ToInt64(dt.Rows[i]["Id"]);
                        objPaper.SubjectId = Convert.ToString(dt.Rows[i]["SubjectId"]).IsEmpty() ? 0 : Convert.ToInt64(dt.Rows[i]["SubjectId"]);
                        objPaper.SubjectName = Convert.ToString(dt.Rows[i]["SubjectName"]).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["SubjectName"]);
                        objPaper.FacultyId = Convert.ToString(dt.Rows[i]["FacultyId"]).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[i]["FacultyId"]);
                        objPaper.FacultyName = Convert.ToString(dt.Rows[i]["FacultyName"]).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["FacultyName"]);
                        objPaper.PaperName = Convert.ToString(dt.Rows[i]["PaperName"]).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["PaperName"]);
                        objPaper.PaperCode = Convert.ToString(dt.Rows[i]["PaperCode"]).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["PaperCode"]);
                        objPaper.IsCredit = Convert.ToString(dt.Rows[i]["IsCredit"]).IsEmpty() ? false : Convert.ToBoolean(dt.Rows[i]["IsCredit"]);
                        objPaper.MaxMarks = Convert.ToString(dt.Rows[i]["MaxMarks"]).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[i]["MaxMarks"]);
                        objPaper.MinMarks = Convert.ToString(dt.Rows[i]["MinMarks"]).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[i]["MinMarks"]);
                        //if (dt.Rows[i]["Credits"].ToString() != "")
                        //{
                            objPaper.Credits = Convert.ToString(dt.Rows[i]["Credits"]).IsEmpty() ? 0 : Convert.ToDecimal(dt.Rows[i]["Credits"]);
                        //}
                        //else { objPaper.Credits = 0; }
                        objPaper.IsSeparatePassingHead = Convert.ToString(dt.Rows[i]["IsSeparatePassingHead"]).IsEmpty() ? false : Convert.ToBoolean(dt.Rows[i]["IsSeparatePassingHead"]);
                        objPaper.EvaluationId = Convert.ToString(dt.Rows[i]["EvaluationId"]).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[i]["EvaluationId"]);
                        objPaper.EvaluationName = Convert.ToString(dt.Rows[i]["EvaluationName"]).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["EvaluationName"]);
                        objPaper.IsActive = Convert.ToString(dt.Rows[i]["IsActive"]).IsEmpty() ? false : Convert.ToBoolean(dt.Rows[i]["IsActive"]); ;
                        objPaper.IsActiveSts = Convert.ToString(dt.Rows[i]["IsActiveSts"]).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["IsActiveSts"]);
                       // objPaper.PaperTLMMapData = ObjMapDataLst;
                        count = count + 1;
                        objPaper.IndexId = count;
                        ObjLstPaper.Add(objPaper);
                    }

                }
                if (ObjLstPaper != null)
                    return Return.returnHttp("200", ObjLstPaper, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }

            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region Paper Detail
        [HttpPost]
        public HttpResponseMessage MstPaperDetailGet(MstPaper ObjPaper)
        {
            /* String token = Request.Headers.GetValues("token").FirstOrDefault();
             TokenOperation ac = new TokenOperation();

             string res = ac.ValidateToken(token);

             if (res == "0")
             {
                 return Return.returnHttp("0", null, null);
             }*/

            try
            {
               
                List<PaperTLMMapDetails> ObjMapDataLst = new List<PaperTLMMapDetails>();

                SqlCommand cmd1 = new SqlCommand();
                SqlDataAdapter cmdda1 = new SqlDataAdapter("PaperTLMMAPDetailsGet", con);
                cmdda1.SelectCommand.CommandType = CommandType.StoredProcedure;

                cmdda1.SelectCommand.Parameters.AddWithValue("PaperId", ObjPaper.Id);

                DataTable Dt1 = new DataTable();
                cmdda1.Fill(Dt1);

                if (Dt1.Rows.Count > 0)
                {
                    for (int j = 0; j < Dt1.Rows.Count; j++)
                    {
                        PaperTLMMapDetails ObjMapData = new PaperTLMMapDetails();
                        
                        ObjMapData.TeachingLearningMethodName = Convert.ToString(Dt1.Rows[j]["TeachingLearningMethodName"]).IsEmpty() ? "" : Convert.ToString(Dt1.Rows[j]["TeachingLearningMethodName"]);
                        ObjMapData.AssessmentType = Convert.ToString(Dt1.Rows[j]["AssessmentType"]).IsEmpty() ? "" : Convert.ToString(Dt1.Rows[j]["AssessmentType"]);
                        ObjMapData.AssessmentMethodName = Convert.ToString(Dt1.Rows[j]["AssessmentMethodName"]).IsEmpty() ? "" : Convert.ToString(Dt1.Rows[j]["AssessmentMethodName"]);
                        ObjMapData.AssessmentMethodMarks = Convert.ToString(Dt1.Rows[j]["AssessmentMethodMarks"]).IsEmpty() ? 0 : Convert.ToInt32(Dt1.Rows[j]["AssessmentMethodMarks"]);
                        ObjMapData.AssessmentTypeMaxMarks = Convert.ToString(Dt1.Rows[j]["AssessmentTypeMaxMarks"]).IsEmpty() ? 0 : Convert.ToInt32(Dt1.Rows[j]["AssessmentTypeMaxMarks"]);
                        ObjMapData.AssessmentTypeMinMarks = Convert.ToString(Dt1.Rows[j]["AssessmentTypeMinMarks"]).IsEmpty() ? 0 : Convert.ToInt32(Dt1.Rows[j]["AssessmentTypeMinMarks"]);
                        ObjMapData.NoOfCredits = Convert.ToString(Dt1.Rows[j]["NoOfCredits"]).IsEmpty() ? 0 : Convert.ToDecimal(Dt1.Rows[j]["NoOfCredits"]);
                        ObjMapData.NoOfHoursPerWeek = Convert.ToString(Dt1.Rows[j]["NoOfHoursPerWeek"]).IsEmpty() ? 0 : Convert.ToInt32(Dt1.Rows[j]["NoOfHoursPerWeek"]);
                        ObjMapDataLst.Add(ObjMapData);
                    }
                }
                ObjPaper.PaperTLMMapData = ObjMapDataLst;

               // }
            if (ObjMapDataLst != null)
                return Return.returnHttp("200", ObjPaper, null);
            else
                return Return.returnHttp("201", "No Record Found", null);

        }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region Paper Detail For Report
        [HttpPost]
        public HttpResponseMessage PaperDetailGetForReport()
        {
            /* String token = Request.Headers.GetValues("token").FirstOrDefault();
             TokenOperation ac = new TokenOperation();

             string res = ac.ValidateToken(token);

             if (res == "0")
             {
                 return Return.returnHttp("0", null, null);
             }*/

            try
            {

                List<PaperTLMMapDetails> ObjMapDataLst = new List<PaperTLMMapDetails>();

                SqlCommand cmd1 = new SqlCommand();
                SqlDataAdapter cmdda1 = new SqlDataAdapter("PaperDetailsForReport", con);
                cmdda1.SelectCommand.CommandType = CommandType.StoredProcedure;
                Int32 count = 0;
                DataTable Dt1 = new DataTable();
                cmdda1.Fill(Dt1);

                if (Dt1.Rows.Count > 0)
                {
                    for (int j = 0; j < Dt1.Rows.Count; j++)
                    {
                        PaperTLMMapDetails ObjMapData = new PaperTLMMapDetails();

                        ObjMapData.PaperName = Convert.ToString(Dt1.Rows[j]["PaperName"]).IsEmpty() ? "" : Convert.ToString(Dt1.Rows[j]["PaperName"]);
                        ObjMapData.PaperCode = Convert.ToString(Dt1.Rows[j]["PaperCode"]).IsEmpty() ? "" : Convert.ToString(Dt1.Rows[j]["PaperCode"]);
                        ObjMapData.TeachingLearningMethodName = Convert.ToString(Dt1.Rows[j]["TeachingLearningMethodName"]).IsEmpty() ? "" : Convert.ToString(Dt1.Rows[j]["TeachingLearningMethodName"]);
                        ObjMapData.AssessmentType = Convert.ToString(Dt1.Rows[j]["AssessmentType"]).IsEmpty() ? "" : Convert.ToString(Dt1.Rows[j]["AssessmentType"]);
                        ObjMapData.AssessmentMethodName = Convert.ToString(Dt1.Rows[j]["AssessmentMethodName"]).IsEmpty() ? "" : Convert.ToString(Dt1.Rows[j]["AssessmentMethodName"]);
                        ObjMapData.AssessmentMethodMarks = Convert.ToString(Dt1.Rows[j]["AssessmentMethodMarks"]).IsEmpty() ? 0 : Convert.ToInt32(Dt1.Rows[j]["AssessmentMethodMarks"]);
                        ObjMapData.AssessmentTypeMaxMarks = Convert.ToString(Dt1.Rows[j]["AssessmentTypeMaxMarks"]).IsEmpty() ? 0 : Convert.ToInt32(Dt1.Rows[j]["AssessmentTypeMaxMarks"]);
                        ObjMapData.AssessmentTypeMinMarks = Convert.ToString(Dt1.Rows[j]["AssessmentTypeMinMarks"]).IsEmpty() ? 0 : Convert.ToInt32(Dt1.Rows[j]["AssessmentTypeMinMarks"]);
                        ObjMapData.NoOfCredits = Convert.ToString(Dt1.Rows[j]["NoOfCredits"]).IsEmpty() ? 0 : Convert.ToDecimal(Dt1.Rows[j]["NoOfCredits"]);
                        ObjMapData.NoOfHoursPerWeek = Convert.ToString(Dt1.Rows[j]["NoOfHoursPerWeek"]).IsEmpty() ? 0 : Convert.ToInt32(Dt1.Rows[j]["NoOfHoursPerWeek"]);
                        count = count + 1;
                        ObjMapData.IndexId = count;
                        ObjMapDataLst.Add(ObjMapData);
                    }
                }
                

                // }
                if (ObjMapDataLst != null)
                    return Return.returnHttp("200", ObjMapDataLst, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region Paper List Get For Drop Down
        [HttpGet]
        public HttpResponseMessage MstPaperGetForDropDown()
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            try
            {
                List<MstPaper> ObjLstPaper = new List<MstPaper>();
                BALPaper ObjBalPaper = new BALPaper();
                ObjLstPaper = ObjBalPaper.PaperListGetForDropDown();

                if (ObjLstPaper != null)
                    return Return.returnHttp("200", ObjLstPaper, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion           

        #region Paper Add
        [HttpPost]
        public HttpResponseMessage MstPaperAdd(MstPaper mstPaper)
        {
            try
            {                
                Validation validation = new Validation();                

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();

                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                if (String.IsNullOrEmpty(Convert.ToString(mstPaper.SubjectId)))
                {
                    return Return.returnHttp("201", "Please select subject", null);
                }
                else if (String.IsNullOrWhiteSpace(Convert.ToString(mstPaper.PaperName)))
                {
                    return Return.returnHttp("201", "Please enter paper name", null);
                }
                else if (String.IsNullOrWhiteSpace(Convert.ToString(mstPaper.PaperCode)))
                {
                    return Return.returnHttp("201", "Please enter paper code", null);
                }
                else if (validation.validateDigit(Convert.ToString(mstPaper.MaxMarks)) == false)
                {
                    return Return.returnHttp("201", "Please enter Maximum marks", null);
                }
                else if ((validation.validateDigit(Convert.ToString(mstPaper.MinMarks)) == false) || (mstPaper.MinMarks > mstPaper.MaxMarks))
                {
                    return Return.returnHttp("201", "Please enter Minimum marks and respective of Maximum Marks", null);
                }
                /*else if (validation.validateDigit(Convert.ToString(Jsondata.Credits)) == false)
                {
                    return Return.returnHttp("201", "Please enter Credits", null);
                }*/                
                else if (String.IsNullOrEmpty(Convert.ToString(mstPaper.EvaluationId)))
                {
                    return Return.returnHttp("201", "Please select evaluati1on type", null);
                }
                else
                {
                    Int64 SubjectId = Convert.ToInt64(mstPaper.SubjectId);
                    String PaperName = Convert.ToString(mstPaper.PaperName);
                    String PaperCode = Convert.ToString(mstPaper.PaperCode);
                    Boolean IsCredit = Convert.ToBoolean(mstPaper.IsCredit);
                    Int32 MaxMarks = Convert.ToInt32(mstPaper.MaxMarks);
                    Int32 MinMarks = Convert.ToInt32(mstPaper.MinMarks);
                    Decimal Credits = Convert.ToDecimal(mstPaper.Credits);                   
                    Boolean IsSeparatePassingHead = Convert.ToBoolean(mstPaper.IsSeparatePassingHead);
                    Int64 EvaluationId = Convert.ToInt64(mstPaper.EvaluationId);
                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("MstPaperAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
                    cmd.Parameters.AddWithValue("@PaperName", PaperName);
                    cmd.Parameters.AddWithValue("@PaperCode", PaperCode);
                    cmd.Parameters.AddWithValue("@IsCredit", IsCredit);
                    cmd.Parameters.AddWithValue("@MaxMarks", MaxMarks);
                    cmd.Parameters.AddWithValue("@MinMarks", MinMarks);
                    cmd.Parameters.AddWithValue("@Credits", Credits);                    
                    cmd.Parameters.AddWithValue("@IsSeparatePassingHead",IsSeparatePassingHead);
                    cmd.Parameters.AddWithValue("@EvaluationId", EvaluationId);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters.Add("@PaperId", SqlDbType.BigInt);
                   // cmd.Parameters.Add("@PaperName", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@PaperId"].Direction = ParameterDirection.Output;
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                   // cmd.Parameters["@PaperName"].Direction = ParameterDirection.Output;
                    con.Open();

                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    mstPaper.Id = Convert.ToInt64(cmd.Parameters["@PaperId"].Value);
                    con.Close();
                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Your data has been saved successfully.";
                    }

                    return Return.returnHttp("200", (strMessage.ToString(),mstPaper.Id,PaperName,PaperCode,Credits,MaxMarks), null);
                }
            }

            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Paper Edit
        [HttpPost]
        public HttpResponseMessage MstPaperEdit(MstPaper mstPaper)
        {
            try
            {
                Validation validation = new Validation();               

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();

                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                //MstPaper paper = new MstPaper();
                //dynamic JsonData = mstPaper;

                if (String.IsNullOrEmpty(Convert.ToString(mstPaper.SubjectId)))
                {
                    return Return.returnHttp("201", "Please select subject", null);
                }
                else if (String.IsNullOrWhiteSpace(Convert.ToString(mstPaper.PaperName)))
                {
                    return Return.returnHttp("201", "Please enter paper name", null);
                }
                else if (String.IsNullOrWhiteSpace(Convert.ToString(mstPaper.PaperCode)))
                {
                    return Return.returnHttp("201", "Please enter paper code", null);
                }
                else if (validation.validateDigit(Convert.ToString(mstPaper.MaxMarks)) == false)
                {
                    return Return.returnHttp("201", "Please enter Maximum marks", null);
                }
                else if ((validation.validateDigit(Convert.ToString(mstPaper.MinMarks)) == false) || (mstPaper.MinMarks > mstPaper.MaxMarks))
                {
                    return Return.returnHttp("201", "Please enter Minimum marks and respective of Maximum Marks", null);
                }
                /*else if (validation.validateDigit(Convert.ToString(JsonData.Credits)) == false)
                {
                    return Return.returnHttp("201", "Please enter Credits", null);
                }*/
                else if (String.IsNullOrEmpty(Convert.ToString(mstPaper.EvaluationId)))
                {
                    return Return.returnHttp("201", "Please select evaluation type", null);
                }
                else
                {

                    Int64 Id = Convert.ToInt64(mstPaper.Id);
                    Int64 SubjectId = Convert.ToInt64(mstPaper.SubjectId);
                    String PaperName = Convert.ToString(mstPaper.PaperName);
                    String PaperCode = Convert.ToString(mstPaper.PaperCode);
                    Boolean IsCredit = Convert.ToBoolean(mstPaper.IsCredit);
                    Int32 MaxMarks = Convert.ToInt32(mstPaper.MaxMarks);
                    Int32 MinMarks = Convert.ToInt32(mstPaper.MinMarks);
                    Decimal Credits = Convert.ToDecimal(mstPaper.Credits);                    
                    Boolean IsSeparatePassingHead = Convert.ToBoolean(mstPaper.IsSeparatePassingHead);
                    Int64 EvaluationId = Convert.ToInt64(mstPaper.EvaluationId);
                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("MstPaperEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    
                    cmd.Parameters.AddWithValue("Id", Id);
                    cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
                    cmd.Parameters.AddWithValue("@PaperName", PaperName);
                    cmd.Parameters.AddWithValue("@PaperCode", PaperCode);
                    cmd.Parameters.AddWithValue("@IsCredit", IsCredit);
                    cmd.Parameters.AddWithValue("@MaxMarks", MaxMarks);
                    cmd.Parameters.AddWithValue("@MinMarks", MinMarks);
                    cmd.Parameters.AddWithValue("@Credits", Credits);                   
                    cmd.Parameters.AddWithValue("@IsSeparatePassingHead", IsSeparatePassingHead);
                    cmd.Parameters.AddWithValue("@EvaluationId", EvaluationId);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);

                    cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@MESSAGE"].Value);
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

        #region Paper Delete
        [HttpPost]
        public HttpResponseMessage MstPaperDelete(MstPaper mstPaper)
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

                /*MstPaper paper = new MstPaper();
                dynamic jsonData = mstPaper;*/
                Int64 Id = Convert.ToInt64(mstPaper.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("MstPaperDelete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);

                cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@MESSAGE"].Value);
                con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been saved successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion
		
		#region Paper TLMAM Delete
        [HttpPost]
        public HttpResponseMessage MstPaperTLMAMDelete(MstPaper mstPaper)
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

                /*MstPaper paper = new MstPaper();
                dynamic jsonData = mstPaper;*/
                Int64 Id = Convert.ToInt64(mstPaper.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("MstPaperTeachingLearningMapDelete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);

                cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@MESSAGE"].Value);
                con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been deleted successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region Paper Suspend
        [HttpPost]
        public HttpResponseMessage MstPaperIsSuspended(MstPaper mstPaper)
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

                /*MstPaper paper = new MstPaper();
                dynamic jsonData = jobj;*/
                //paper.Id = jsonData.Id;

                Int64 Id = Convert.ToInt64(mstPaper.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("MstPaperActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstPaperIsActiveDisable");
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);

                cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@MESSAGE"].Value);
                con.Close();
                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been modified successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion     

        #region Paper Active
        [HttpPost]
        public HttpResponseMessage MstPaperIsActive(MstPaper mstPaper)
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

                /*MstPaper paper = new MstPaper();
                dynamic jsonData = jobj;
                paper.Id = jsonData.Id;*/
                Int64 Id = Convert.ToInt64(mstPaper.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("MstPaperActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstPaperIsActiveEnable");
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);

                cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@MESSAGE"].Value);
                con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been saved successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region Paper Data for Export
        [HttpPost]
        public HttpResponseMessage PaperGetForExport()
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            try
            {
                List<MstPaper> ObjListPaper = new List<MstPaper>();
                BALPaper ObjBalPaperList = new BALPaper();
                ObjListPaper = ObjBalPaperList.PaperListGet();

                if (ObjListPaper != null)
                    return Return.returnHttp("200", ObjListPaper, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

    }
}

