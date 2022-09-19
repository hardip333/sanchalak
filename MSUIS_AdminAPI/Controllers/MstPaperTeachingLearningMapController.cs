using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class MstPaperTeachingLearningMapController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region Teaching Learning Method List on the basis of Paper Id
        [HttpPost]
        public HttpResponseMessage MstPaperTeachingLearningMapGetByPaperId(JObject ObjPaperTLM)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);
                dynamic Jsondata = ObjPaperTLM;

                Int64 PaperId = Convert.ToInt64(Jsondata.PaperId);

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter cmdda = new SqlDataAdapter("MstPaperTeachingLearningMapGetByPaperId", con);

                cmdda.SelectCommand.CommandType = CommandType.StoredProcedure;

                cmdda.SelectCommand.Parameters.AddWithValue("@PaperId", PaperId);
                DataTable Dt = new DataTable();
                cmdda.Fill(Dt);
                List<MstPaperTeachingLearningMap> ObjLstTeachingLearning = new List<MstPaperTeachingLearningMap>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstPaperTeachingLearningMap ObjTeachingLearning = new MstPaperTeachingLearningMap();
                        List<AssessmentTypeList> ObjATLst = new List<AssessmentTypeList>();                        

                        //ObjTeachingLearning.Id = Convert.ToInt32(Dt.Rows[i]["Id"].ToString());
                        ObjTeachingLearning.PaperId = Convert.ToInt64(Dt.Rows[i]["PaperId"].ToString());
                        ObjTeachingLearning.PaperName = Convert.ToString(Dt.Rows[i]["PaperName"]);
                        ObjTeachingLearning.TeachingLearningMethodId = Convert.ToInt32(Dt.Rows[i]["TeachingLearningMethodId"].ToString());
                        ObjTeachingLearning.TeachingLearningMethodName = Convert.ToString(Dt.Rows[i]["TeachingLearningMethodName"]);
                        ObjTeachingLearning.AssessmentMethodId = Convert.ToInt32(Dt.Rows[i]["AssessmentMethodId"].ToString());
                        ObjTeachingLearning.AssessmentMethodName = Convert.ToString(Dt.Rows[i]["AssessmentMethodName"]);                        
                        ObjTeachingLearning.AssessmentMethodMarks = (Dt.Rows[i]["AssessmentMethodMarks"].ToString().IsEmpty())?0:Convert.ToInt32(Dt.Rows[i]["AssessmentMethodMarks"].ToString());
                        ObjTeachingLearning.NoOfCredits = Convert.ToString(Dt.Rows[i]["NoOfCredits"]).IsEmpty() ? 0 : Convert.ToDecimal(Dt.Rows[i]["NoOfCredits"]);
                        ObjTeachingLearning.NoOfLecturesPerWeek = Convert.ToString(Dt.Rows[i]["NoOfLecturesPerWeek"]).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["NoOfLecturesPerWeek"]);
                        ObjTeachingLearning.NoOfHoursPerWeek = Convert.ToString(Dt.Rows[i]["NoOfHoursPerWeek"]).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["NoOfHoursPerWeek"]);

                        SqlCommand cmd1 = new SqlCommand();
                        SqlDataAdapter cmdda1 = new SqlDataAdapter("AssessTypeGetByPaperId", con);

                        cmdda1.SelectCommand.CommandType = CommandType.StoredProcedure;

                        cmdda1.SelectCommand.Parameters.AddWithValue("@PaperId", ObjTeachingLearning.PaperId);
                        cmdda1.SelectCommand.Parameters.AddWithValue("@TLMId", ObjTeachingLearning.TeachingLearningMethodId);
                        cmdda1.SelectCommand.Parameters.AddWithValue("@AMId", ObjTeachingLearning.AssessmentMethodId);
                        DataTable Dt1 = new DataTable();
                        cmdda1.Fill(Dt1);

                        if (Dt1.Rows.Count > 0)
                        {
                            for (int j = 0; j < Dt1.Rows.Count; j++)
                            {
                                AssessmentTypeList ObjAT = new AssessmentTypeList();
                                ObjAT.ATName = Convert.ToString(Dt1.Rows[j]["AssessmentType"]);
                                ObjAT.Max = (Dt1.Rows[j]["AssessmentTypeMaxMarks"].ToString().IsEmpty())?0:Convert.ToInt32(Dt1.Rows[j]["AssessmentTypeMaxMarks"].ToString());
                                ObjAT.Min = (Dt1.Rows[j]["AssessmentTypeMinMarks"].ToString().IsEmpty())?0:Convert.ToInt32(Dt1.Rows[j]["AssessmentTypeMinMarks"].ToString());
                                ObjAT.CheckBox = true;
                                ObjATLst.Add(ObjAT);
                            }
                            
                        }
                        ObjTeachingLearning.AssessType = ObjATLst;

                        if (ObjTeachingLearning.AssessType.Count < 2)

                        {
                            Int32 count = ObjTeachingLearning.AssessType.Count;
                            if(count == 0)
                            {
                                AssessmentTypeList o1 = new AssessmentTypeList();
                                AssessmentTypeList o2 = new AssessmentTypeList();
                                o1.ATName = "UA";
                                o2.ATName = "IA";
                                o1.Min = 0;
                                o1.Max = 0;
                                o2.Min = 0;
                                o2.Max = 0;
                                o1.CheckBox = false;
                                o2.CheckBox = false;
                                ObjTeachingLearning.AssessType.Add(o1);
                                ObjTeachingLearning.AssessType.Add(o2);
                            }
                            else
                            {
                                AssessmentTypeList o1 = new AssessmentTypeList();
                                if(ObjTeachingLearning.AssessType[0].ATName == "UA")
                                {
                                    o1.ATName = "IA";
                                    o1.Min = 0;
                                    o1.Max = 0;
                                    o1.CheckBox = false;
                                    ObjTeachingLearning.AssessType.Add(o1);
                                }
                                else
                                {
                                    o1.ATName = "UA";
                                    o1.Min = 0;
                                    o1.Max = 0;
                                    o1.CheckBox = false;
                                    ObjTeachingLearning.AssessType.Add(o1);
                                }
                            }
                        }

                                ObjLstTeachingLearning.Add(ObjTeachingLearning);
                    }

                }               

                return Return.returnHttp("200", ObjLstTeachingLearning, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region Teaching Method List on the basis of Paper ID with left join
        [HttpPost]
        public HttpResponseMessage MstTLMAMPaperMapbyPaperId(JObject ObjPaperTLM)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);
                dynamic Jsondata = ObjPaperTLM;

                Int64 PaperId = Convert.ToInt64(Jsondata.PaperId);

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter cmdda = new SqlDataAdapter("MstTeachingLearningMethodGetbyPaperId", con);

                cmdda.SelectCommand.CommandType = CommandType.StoredProcedure;

                cmdda.SelectCommand.Parameters.AddWithValue("@PaperId1", PaperId);
                DataTable Dt = new DataTable();
                cmdda.Fill(Dt);
                List<MstTeachingLearningMethod> ObjLstTeachingLearning = new List<MstTeachingLearningMethod>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstTeachingLearningMethod ObjTeachingLearning = new MstTeachingLearningMethod();

                        //ObjTeachingLearning.Id = Convert.ToInt32(Dt.Rows[i]["Id"].ToString());
                        //ObjTeachingLearning.PaperId = (Dt.Rows[i]["PaperId"].ToString().IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["PaperId"].ToString());
                        ObjTeachingLearning.Id = Convert.ToInt32(Dt.Rows[i]["TeachingLearningMethodId"].ToString());
                        ObjTeachingLearning.TeachingLearningMethodName = Convert.ToString(Dt.Rows[i]["TeachingLearningMethodName"]);
                        //ObjTeachingLearning.AssessmentMethodId = Convert.ToInt32(Dt.Rows[i]["AssessmentMethodId"].ToString());
                        //ObjTeachingLearning.AssessmentMethodName = Convert.ToString(Dt.Rows[i]["AssessmentMethodName"]);
                        ObjTeachingLearning.TLMCheckedStatus = Convert.ToBoolean(Dt.Rows[i]["TLMStatus"]);                        
                        
                        ObjLstTeachingLearning.Add(ObjTeachingLearning);
                    }

                }

                return Return.returnHttp("200", ObjLstTeachingLearning, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion       

        #region Insertion of Records of Paper's Assessment Marks 
        [HttpPost]
        public HttpResponseMessage MstPaperTLMMarksAdd(List<MstPaperTeachingLearningMap> ObjMstPTLMLst)
        {
            try
            {
                string Finalmsg = null;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                for (int i = 0; i < ObjMstPTLMLst.Count; i++)
                {
                    /*if (String.IsNullOrEmpty(Convert.ToString(ObjMstPTLMLst[i].PaperId)))
                    {
                        return Return.returnHttp("201", "Please select one paper", null);
                    }
                    if (String.IsNullOrEmpty(Convert.ToString(ObjMstPTLMLst[i].TeachingLearningMethodId)))
                    {
                        return Return.returnHttp("201", "Please select Teaching Learning Method", null);
                    }
                    if (String.IsNullOrEmpty(Convert.ToString(ObjMstPTLMLst[i].AssessmentMethodId)))
                    {
                        return Return.returnHttp("201", "Please select Assessment Method", null);
                    }*/

                    Int64 PaperId = Convert.ToInt64(ObjMstPTLMLst[i].PaperId);
                    Int32 TeachingLearningMethodId = Convert.ToInt32(ObjMstPTLMLst[i].TeachingLearningMethodId);
                    Int32 AssessmentMethodId = Convert.ToInt32(ObjMstPTLMLst[i].AssessmentMethodId);
                    Int32 AssessmentMethodMarks = Convert.ToInt32(ObjMstPTLMLst[i].AssessmentMethodMarks);
                    Decimal NoOfCredits = Convert.ToDecimal(ObjMstPTLMLst[i].NoOfCredits);
                    Int32 NoOfLecturesPerWeek = Convert.ToInt32(ObjMstPTLMLst[i].NoOfLecturesPerWeek);
                    Int32 NoOfHoursPerWeek = Convert.ToInt32(ObjMstPTLMLst[i].NoOfHoursPerWeek);
                    var AssessType = ObjMstPTLMLst[i].AssessType;
                    foreach(AssessmentTypeList AT in AssessType)
                    {
                        string ATName = AT.ATName;
                        Int32 MaxMarks = AT.Max;
                        Int32 MinMarks = AT.Min;
                        if (AT.CheckBox) 
                        {
                            //Insert or Update Code
                            SqlCommand cmd = new SqlCommand("MstPaperTLMAMMarksAdd", con);
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@PaperId", PaperId);
                            cmd.Parameters.AddWithValue("@TeachingLearningMethodId", TeachingLearningMethodId);
                            cmd.Parameters.AddWithValue("@AssessmentMethodId", AssessmentMethodId);
                            cmd.Parameters.AddWithValue("@AssessmentType", ATName);
                            cmd.Parameters.AddWithValue("@AssessmentTypeMaxMarks", MaxMarks);
                            cmd.Parameters.AddWithValue("@AssessmentTypeMinMarks", MinMarks);
                            cmd.Parameters.AddWithValue("@AssessmentMethodMarks", AssessmentMethodMarks);
                            cmd.Parameters.AddWithValue("@NoOfCredits", NoOfCredits);
                            cmd.Parameters.AddWithValue("@NoOfLecturesPerWeek", NoOfLecturesPerWeek);
                            cmd.Parameters.AddWithValue("@NoOfHoursPerWeek", NoOfHoursPerWeek);
                            cmd.Parameters.AddWithValue("@UserId", UserId);
                            cmd.Parameters.AddWithValue("@UserTime", datetime);

                            cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                            cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                            con.Open();
                            cmd.ExecuteNonQuery();
                            string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                            con.Close();
                           // if (string.Equals(strMessage, "TRUE"))
                           // {
                                strMessage = "Your data has been saved successfully.";
                          //  }

                            //Finalmsg += strMessage + ",";
                            Finalmsg = strMessage;
                        }
                        else 
                        {
                            //delete code
                            SqlCommand cmd = new SqlCommand("MstPaperTeachingLearningMap_Delete", con);
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@PaperId", PaperId);
                            cmd.Parameters.AddWithValue("@TLMId", TeachingLearningMethodId);
                            cmd.Parameters.AddWithValue("@AMId", AssessmentMethodId);
                            cmd.Parameters.AddWithValue("@AssessmentType", ATName);                           
                            cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                            cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                            con.Open();
                            cmd.ExecuteNonQuery();
                            string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                            con.Close();

                          //  if (string.Equals(strMessage, "TRUE"))
                           // {
                          //      strMessage = "Your data has been modified successfully.";
                           // }

                            Finalmsg = strMessage;                           

                        }
                    }                   
                   
                }
                return Return.returnHttp("200", Finalmsg, null);
                

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion               

        #region Paper List
        [HttpPost]
        public HttpResponseMessage MstPaperGet()
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
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter cmdda = new SqlDataAdapter("MstPaperGet", con);

                cmdda.SelectCommand.CommandType = CommandType.StoredProcedure;

                DataTable Dt = new DataTable();
                cmdda.Fill(Dt);

                List<MstPaper> PaperList = new List<MstPaper>();
                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstPaper paper = new MstPaper();

                        paper.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);                        
                        paper.PaperName = Convert.ToString(Dt.Rows[i]["PaperName"]);
                        paper.PaperCode = Convert.ToString(Dt.Rows[i]["PaperCode"]);                       
                        PaperList.Add(paper);
                    }

                }

                return Return.returnHttp("200", PaperList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion     

        #region Assessment Method List on the basis of Teaching Learning Method
        [HttpPost]
        public HttpResponseMessage MstAssessmentMethodGetByTLM(MstPaperTeachingLearningMap ObjPaperTLMLst)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                List<MstPaperTeachingLearningMap> ObjPaperTLMMapLst = new List<MstPaperTeachingLearningMap>();
                for (int j = 0; j < ObjPaperTLMLst.TeachingLearningMethodList.Count; j++)
                {
                    MstPaperTeachingLearningMap ObjPaperTLMMap = new MstPaperTeachingLearningMap();
                   // ObjPaperTLMMap.TeachingLearningMethodId = ObjPaperTLMLst.TeachingLearningMethodList[j].Id;
                    //ObjPaperTLMMap.TeachingLearningMethodName = ObjPaperTLMLst.TeachingLearningMethodList[j].TeachingLearningMethodName;

                    List<MstAssessmentMethod> ObjMstAssessmentMethodLst = new List<MstAssessmentMethod>();
                    SqlCommand cmd = new SqlCommand();
                    SqlDataAdapter cmdda = new SqlDataAdapter("MstAssessmentMethodGetByTeachingLearningMethodId", con);

                    cmdda.SelectCommand.CommandType = CommandType.StoredProcedure;
                    cmdda.SelectCommand.Parameters.AddWithValue("@TeachingLearningMethodId", ObjPaperTLMLst.TeachingLearningMethodList[j].Id);
                    DataTable Dt = new DataTable();
                    cmdda.Fill(Dt);
                    if (Dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            MstAssessmentMethod ObjMstAM = new MstAssessmentMethod();

                            ObjMstAM.Id = Convert.ToInt32(Dt.Rows[i]["AssessmentMethodId"]);
                            ObjMstAM.AssessmentMethodName = Convert.ToString(Dt.Rows[i]["AssessmentMethodName"]);
                            ObjMstAM.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);

                            ObjMstAssessmentMethodLst.Add(ObjMstAM);
                        }

                    }
                    Dt.Dispose();
                    ObjPaperTLMMap.AssessmentMethodList = ObjMstAssessmentMethodLst;
                    ObjPaperTLMMapLst.Add(ObjPaperTLMMap);

                }

                return Return.returnHttp("200", ObjPaperTLMMapLst, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Assessment Method List on the basis of Teaching Learning Method on the basis of PaperId
        [HttpPost]
        public HttpResponseMessage MstAssessmentMethodGetByTLMAMMap(MstPaperTeachingLearningMap ObjPaperTLMLst)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                List<MstPaperTeachingLearningMap> ObjPaperTLMMapLst = new List<MstPaperTeachingLearningMap>();
                for (int j = 0; j < ObjPaperTLMLst.TeachingLearningMethodList.Count; j++)
                {
                    MstPaperTeachingLearningMap ObjPaperTLMMap = new MstPaperTeachingLearningMap();
                    ObjPaperTLMMap.TeachingLearningMethodId = ObjPaperTLMLst.TeachingLearningMethodList[j].Id;
                    ObjPaperTLMMap.TeachingLearningMethodName = ObjPaperTLMLst.TeachingLearningMethodList[j].TeachingLearningMethodName;

                    List<MstAssessmentMethod> ObjMstAssessmentMethodLst = new List<MstAssessmentMethod>();
                    SqlCommand cmd = new SqlCommand();
                    SqlDataAdapter cmdda = new SqlDataAdapter("MstAssessmentMethodGetByTLMAMMapId", con);

                    cmdda.SelectCommand.CommandType = CommandType.StoredProcedure;
                    cmdda.SelectCommand.Parameters.AddWithValue("@TeachingLearningMethodId", ObjPaperTLMLst.TeachingLearningMethodList[j].Id);
                    cmdda.SelectCommand.Parameters.AddWithValue("@PaperId", ObjPaperTLMLst.PaperId);
                    DataTable Dt = new DataTable();
                    cmdda.Fill(Dt);
                    if (Dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            MstAssessmentMethod ObjMstAM = new MstAssessmentMethod();

                            ObjMstAM.Id = Convert.ToInt32(Dt.Rows[i]["AssessmentMethodId"]);
                            ObjMstAM.AssessmentMethodName = Convert.ToString(Dt.Rows[i]["AssessmentMethodName"]);
                            //ObjMstAM.TeachingLearningMethodId = Convert.ToInt32(Dt.Rows[i]["TeachingLearningMethodId"]);
                            //ObjMstAM.TeachingLearningMethodName = Convert.ToString(Dt.Rows[i]["TeachingLearningMethodName"]);
                            ObjMstAM.AMCheckedStatus = Convert.ToBoolean(Dt.Rows[i]["AMStatus"]);

                            ObjMstAssessmentMethodLst.Add(ObjMstAM);
                        }

                    }
                    Dt.Dispose();
                     ObjPaperTLMMap.AssessmentMethodList = ObjMstAssessmentMethodLst;
                     ObjPaperTLMMapLst.Add(ObjPaperTLMMap);

                }

                return Return.returnHttp("200", ObjPaperTLMMapLst, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

    }
}