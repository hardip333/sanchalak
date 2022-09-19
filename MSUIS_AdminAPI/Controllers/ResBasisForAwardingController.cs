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
    public class ResBasisForAwardingController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();
        SqlTransaction ST;

        #region ResMstProgBranchMapByProgInsId
        [HttpPost]
        public HttpResponseMessage ResMstProgBranchMapByProgInsId(ResBasisForAwarding ObjResBasis)
        {
            ResBasisForAwarding modelobj = new ResBasisForAwarding();
            try
            {
                modelobj.ProgramInstanceId = ObjResBasis.ProgramInstanceId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("ResMstProgrammeBranchMapByProgInsId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgramInstanceId", modelobj.ProgramInstanceId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<ResBasisForAwarding> ObjLstRB = new List<ResBasisForAwarding>();
               
                if (dt.Rows.Count > 0)
                {
                    
                    foreach (DataRow dr in dt.Rows)
                    {
                        ResBasisForAwarding ObjRB = new ResBasisForAwarding();
                        ObjRB.MstProgrammeBranchMapId = Convert.ToInt32(dr["MstProgrammeBranchMapId"]);
                        ObjRB.Id = Convert.ToInt32(dr["Id"]);
                        ObjRB.BranchName = Convert.ToString(dr["BranchName"]);
                       
                        ObjLstRB.Add(ObjRB);
                    }

                }
                return Return.returnHttp("200", ObjLstRB, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion


        #region ResIncProgrammeInstancePartGetByProgInstId
        [HttpPost]
        public HttpResponseMessage ResIncProgInstancePartGetByProgInstId(ResBasisForAwarding ObjResBasis)
        {
            ResBasisForAwarding modelobj = new ResBasisForAwarding();
            try
            {
               
                modelobj.ProgramInstanceId = ObjResBasis.ProgramInstanceId;
                modelobj.MstProgrammeBranchMapId = ObjResBasis.MstProgrammeBranchMapId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("ResIncProgrammeInstancePartGetByProgInstId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgramInstanceId", modelobj.ProgramInstanceId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<ResBasisForAwarding> ObjLstRB = new List<ResBasisForAwarding>();
               
                if (dt.Rows.Count > 0)
                {
                    dt.Rows.Add("0", "Aggregate");
                    foreach (DataRow dr in dt.Rows)
                    {
                        ResBasisForAwarding ObjRB = new ResBasisForAwarding();
                        ObjRB.IncProgramInstancePartId = Convert.ToInt64(dr["Id"]);
                        ObjRB.PartName = Convert.ToString(dr["PartName"]);
                        ObjRB.PartShortName = Convert.ToString(dr["PartShortName"]);
                        if (ObjRB.PartName == "Aggregate")
                        { ObjLstRB.Add(ObjRB); }
                        // ObjLstRB.Add(ObjRB);

                        SqlCommand Cmd = new SqlCommand("ResIncProgInsPartTermGetByProgInstPartIdAndBranchId", con);
                        Cmd.CommandType = CommandType.StoredProcedure;
                        Cmd.Parameters.AddWithValue("@ProgramInstancePartId", ObjRB.IncProgramInstancePartId);
                        Cmd.Parameters.AddWithValue("@MstProgrammeBranchMapId", modelobj.MstProgrammeBranchMapId);

                        SqlDataAdapter Da1 = new SqlDataAdapter();
                        DataTable Dt1 = new DataTable();
                        Da1.SelectCommand = Cmd;
                        Da1.Fill(Dt1);


                        if (Dt1.Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in Dt1.Rows)
                            {
                                ResBasisForAwarding ObjRBPT = new ResBasisForAwarding();
                                //ResBasisForAwarding ObjRBPT = new ResBasisForAwarding();
                                ObjRBPT.IncProgramInstancePartId = Convert.ToInt64(ObjRB.IncProgramInstancePartId);
                                ObjRBPT.PartName = Convert.ToString(ObjRB.PartName);
                                ObjRBPT.IncProgrammeInstancePartTermId = Convert.ToInt64(dr1["Id"]);
                                ObjRBPT.InstancePartTermName = Convert.ToString(dr1["InstancePartTermName"]);                                                          
                                ObjLstRB.Add(ObjRBPT);
                               
                            
                            
                            }                        

                        }                  
                       
                    }
                   

                }
                return Return.returnHttp("200", ObjLstRB, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion


        #region ResCalculationGet
        [HttpPost]
        public HttpResponseMessage ResCalculationGet()
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

                SqlCommand cmd = new SqlCommand("ResCalculationGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<ResCalculation> ObjLstRC = new List<ResCalculation>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ResCalculation ObjRC = new ResCalculation();
                        ObjRC.Id = Convert.ToInt32(dr["Id"]);
                        ObjRC.ProgramInstancePartId = Convert.ToInt64(dr["ProgramInstancePartId"]);
                        ObjRC.IncProgrammeInstancePartTermId = Convert.ToInt64(dr["IncProgrammeInstancePartTermId"]);
                        ObjRC.IsResCalcApplicable = Convert.ToBoolean(dr["IsResCalcApplicable"]);
                        ObjRC.AttachResultClass = Convert.ToBoolean(dr["AttachResultClass"]);
                        ObjRC.ClassGradeTemplateId = Convert.ToInt32(dr["ClassGradeTemplateId"]);
                        ObjRC.IsLaunched = Convert.ToBoolean(dr["IsLaunched"]);

                        ObjLstRC.Add(ObjRC);
                    }

                }
                return Return.returnHttp("200", ObjLstRC, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region ResBasisForAwardingGetByProgInsId
        [HttpPost]
        public HttpResponseMessage ResBasisForAwardingGetByProgInsId(ResBasisForAwarding ObjResBasis)
        {
            ResBasisForAwarding modelobj = new ResBasisForAwarding();
            try
            {
                modelobj.ProgramInstanceId = ObjResBasis.ProgramInstanceId;
                modelobj.MstProgrammeBranchMapId = ObjResBasis.MstProgrammeBranchMapId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("ResBasisForAwardingGetByProgInsId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgramInstanceId", modelobj.ProgramInstanceId);
                cmd.Parameters.AddWithValue("@MstProgrammeBranchMapId", modelobj.MstProgrammeBranchMapId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<ResBasisForAwarding> ObjLstRB = new List<ResBasisForAwarding>();
               
                if (dt.Rows.Count > 0)
                {
                    dt.Rows.Add("0", "0", "Aggregate", "0", null, null, null, null, "0", "0");
                    foreach (DataRow dr in dt.Rows)
                    {
                        ResBasisForAwarding ObjRB = new ResBasisForAwarding();
                        ObjRB.Id = Convert.ToInt32(dr["Id"]);
                        ObjRB.IncProgramInstancePartId = Convert.ToInt64(dr["IncProgramInstancePartId"]);
                        ObjRB.PartName = Convert.ToString(dr["PartName"]);
                        ObjRB.IncProgrammeInstancePartTermId = Convert.ToInt64(dr["IncProgrammeInstancePartTermId"]);
                        ObjRB.InstancePartTermName = Convert.ToString(dr["InstancePartTermName"]);
                        var ProgramInstancePartPercent = dr["ProgramInstancePartPercent"];
                        if (ProgramInstancePartPercent is DBNull)
                            ObjRB.ProgramInstancePartPercent = null;
                        else
                        ObjRB.ProgramInstancePartPercent = Convert.ToDecimal(dr["ProgramInstancePartPercent"]);
                        var ProgramInstancePartTermPercent = dr["ProgramInstancePartTermPercent"];
                        if (ProgramInstancePartTermPercent is DBNull)
                            ObjRB.ProgramInstancePartTermPercent = null;
                        else
                            ObjRB.ProgramInstancePartTermPercent = Convert.ToDecimal(dr["ProgramInstancePartTermPercent"]);
                        ObjRB.Applicable = Convert.ToString(dr["Applicable"]);
                        ObjRB.MstEvaluationId = Convert.ToInt32(dr["MstEvaluationId"]);
                        var MstEvaluationId = dr["MstEvaluationId"];
                        if (MstEvaluationId is 3 || MstEvaluationId is 0) { }
                        else
                        {
                            var GradeScaleId = dr["GradeScaleId"];
                            if (GradeScaleId is DBNull)
                                ObjRB.GradeScaleId = 0;
                            else
                                ObjRB.GradeScaleId = Convert.ToInt32(dr["GradeScaleId"]);
                        }
                        var ClassGradeTemplateId = dr["ClassGradeTemplateId"];
                        if (ClassGradeTemplateId is DBNull)
                            ObjRB.ClassGradeTemplateId = 0;
                        else
                        ObjRB.ClassGradeTemplateId = Convert.ToInt32(dr["ClassGradeTemplateId"]);
                        ObjRB.RoundingOption = Convert.ToString(dr["RoundingOption"]);
                       // ObjRB.AggregatePercentage = Convert.ToDecimal(dr["AggregatePercentage"]);
                        var MinPassingPercentage = dr["MinPassingPercentage"];
                        if (MinPassingPercentage is DBNull)
                            ObjRB.MinPassingPercentage = null;
                        else
                            ObjRB.MinPassingPercentage = Convert.ToDecimal(dr["MinPassingPercentage"]);
                        //ObjRC.IsLaunched = Convert.ToBoolean(dr["IsLaunched"]);

                        ObjLstRB.Add(ObjRB);
                    }

                }
                return Return.returnHttp("200", ObjLstRB, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion


        #region ResBasisForAwardingAdd
        [HttpPost]
        public HttpResponseMessage ResBForAwardingAdd(ResBasisForAwarding ObjResBasis)
        {
            ResBasisForAwarding modelobj = new ResBasisForAwarding();

            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                modelobj.ProgramInstanceId = ObjResBasis.ProgramInstanceId;
                modelobj.MstProgrammeBranchMapId = ObjResBasis.MstProgrammeBranchMapId;
                modelobj.Applicable = ObjResBasis.Applicable;
                modelobj.MstEvaluationId = ObjResBasis.MstEvaluationId;
                modelobj.ClassGradeTemplateId = ObjResBasis.ClassGradeTemplateId;
                modelobj.RoundingOption = ObjResBasis.RoundingOption;
                modelobj.MinPassingPercentage = ObjResBasis.MinPassingPercentage;
                var InstancePartTermList = ObjResBasis.InstPartTermListResBasis;
                if (modelobj.ProgramInstanceId <= 0)
                {
                    return Return.returnHttp("201", "Please enter valid Program Instance Id", null);
                }
                else if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.Applicable)))
                {
                    return Return.returnHttp("201", "Applicable is null or empty", null);
                }
                else if (modelobj.MstEvaluationId <=0)
                {
                    return Return.returnHttp("201", "Please enter valid Evaluation Id", null);
                }
                else if (modelobj.ClassGradeTemplateId <= 0)
                {
                    return Return.returnHttp("201", "Please enter valid Template Id", null);
                }                         
                else if (String.IsNullOrEmpty(Convert.ToString(InstancePartTermList)))
                {
                    return Return.returnHttp("201", "Instance Part Term List is empty or null", null);
                }
                else
                {
                    SqlCommand[] InstPartTermArray = new SqlCommand[InstancePartTermList.Count()];
                var i = 0;
                String msg = "";              
                modelobj.CreatedBy = Convert.ToInt32(res);

                foreach (PartTermListResBasis objInstPartTerm in InstancePartTermList)
                {
                    string PartName = objInstPartTerm.PartName;
                    if (objInstPartTerm.PartName != "Aggregate")
                    {
                        Int32 Id = objInstPartTerm.Id;
                        Int64 ProgramInstanceId = modelobj.ProgramInstanceId;
                        Int64 IncProgramInstancePartId = objInstPartTerm.IncProgramInstancePartId;
                        Int64 IncProgrammeInstancePartTermId = objInstPartTerm.IncProgrammeInstancePartTermId;
                        decimal? ProgramInstancePartPercent = objInstPartTerm.ProgramInstancePartPercent;
                        decimal? ProgramInstancePartTermPercent = objInstPartTerm.ProgramInstancePartTermPercent;
                        string Applicable = modelobj.Applicable;
                        Int32 MstEvaluationId = modelobj.MstEvaluationId;
                        string RoundingOption = modelobj.RoundingOption;
                        decimal? AggregatePercentage = objInstPartTerm.AggregatePercentage;
                        decimal? MinPassingPercentage = modelobj.MinPassingPercentage;
                        Int32 ClassGradeTemplateId = modelobj.ClassGradeTemplateId;

                        InstPartTermArray[i] = new SqlCommand("ResBasisForAwardingAdd", con, ST);
                        InstPartTermArray[i].CommandType = CommandType.StoredProcedure;
                        InstPartTermArray[i].Parameters.AddWithValue("@Id", Id);
                        InstPartTermArray[i].Parameters.AddWithValue("@ProgramInstanceId", ProgramInstanceId);
                        InstPartTermArray[i].Parameters.AddWithValue("@IncProgramInstancePartId", IncProgramInstancePartId);
                        InstPartTermArray[i].Parameters.AddWithValue("@IncProgrammeInstancePartTermId", IncProgrammeInstancePartTermId);
                        InstPartTermArray[i].Parameters.AddWithValue("@MstProgrammeBranchMapId", modelobj.MstProgrammeBranchMapId);
                        InstPartTermArray[i].Parameters.AddWithValue("@ProgramInstancePartPercent", ProgramInstancePartPercent);
                        InstPartTermArray[i].Parameters.AddWithValue("@ProgramInstancePartTermPercent", ProgramInstancePartTermPercent);
                        InstPartTermArray[i].Parameters.AddWithValue("@Applicable", Applicable);
                        InstPartTermArray[i].Parameters.AddWithValue("@MstEvaluationId", MstEvaluationId);
                        InstPartTermArray[i].Parameters.AddWithValue("@RoundingOption", RoundingOption);
                        InstPartTermArray[i].Parameters.AddWithValue("@AggregatePercentage", AggregatePercentage);
                        InstPartTermArray[i].Parameters.AddWithValue("@MinPassingPercentage", MinPassingPercentage);
                        InstPartTermArray[i].Parameters.AddWithValue("@ClassGradeTemplateId", ClassGradeTemplateId);
                        InstPartTermArray[i].Parameters.AddWithValue("@CreatedBy", modelobj.CreatedBy);

                        InstPartTermArray[i].Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                        InstPartTermArray[i].Parameters["@Message"].Direction = ParameterDirection.Output;

                        i++;
                    }

                }


                con.Open();

                ST = con.BeginTransaction();
                try
                {
                    for (int b = 0; b < i; b++)
                    {
                        InstPartTermArray[b].Transaction = ST;
                        int ans1 = InstPartTermArray[b].ExecuteNonQuery();
                        msg = msg + ", " + Convert.ToString(InstPartTermArray[b].Parameters["@Message"].Value);
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

        #region ResBasisForAwardingDelete
        [HttpPost]
        public HttpResponseMessage ResBasisForAwardingDelete(ResBasisForAwarding ObjResBasis)
        {
            ResBasisForAwarding modelobj = new ResBasisForAwarding();

            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                var InstancePartTermList = ObjResBasis.InstPartTermListResBasis;
                if (String.IsNullOrEmpty(Convert.ToString(InstancePartTermList)))
                {
                    return Return.returnHttp("201", "Instance Part Term List is empty or null", null);
                }
                else
                {
                    SqlCommand[] InstPartTermArray = new SqlCommand[InstancePartTermList.Count()];
                    var i = 0;
                    String msg = "";
                    foreach (PartTermListResBasis objInstPartTerm in InstancePartTermList)
                    {
                        int Id = objInstPartTerm.Id;
                        InstPartTermArray[i] = new SqlCommand("ResBasisForAwardingDelete", con, ST);
                        InstPartTermArray[i].CommandType = CommandType.StoredProcedure;
                        InstPartTermArray[i].Parameters.AddWithValue("@Id", Id);
                        InstPartTermArray[i].Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                        InstPartTermArray[i].Parameters["@Message"].Direction = ParameterDirection.Output;
                        i++;

                    }
                    con.Open();
                    ST = con.BeginTransaction();
                    try
                    {
                        for (int b = 0; b < i; b++)
                        {
                            InstPartTermArray[b].Transaction = ST;
                            int ans1 = InstPartTermArray[b].ExecuteNonQuery();
                            msg = msg + ", " + Convert.ToString(InstPartTermArray[b].Parameters["@Message"].Value);
                        }

                        ST.Commit();
                        return Return.returnHttp("200", "Record deleted successfully", null);
                    }
                    catch (SqlException sqlError)
                    {
                        ST.Rollback();
                        String strMessageErr = Convert.ToString(sqlError);

                        return Return.returnHttp("201", "Record Deleted exists", null);
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
