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
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class ResCalculationController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();
        SqlTransaction ST;

        #region ResMstFacultyGet
        [HttpPost]
        public HttpResponseMessage ResMstFacultyGet()
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

                SqlCommand cmd = new SqlCommand("ResMstFacultyGet", con);
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
                        ObjRC.FacultyName = Convert.ToString(dr["FacultyName"]);
                    
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

                SqlCommand cmd = new SqlCommand("ResMstEvaluationGetResCalc", con);
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
                        ObjRC.EvaluationName = (dr["EvaluationName"].ToString());
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


        #region ResIncProgrammeInstanceGetByFacultyId
        [HttpPost]
        public HttpResponseMessage ResIncProgrammeInsGetByFacId(ResCalculation ObjResCal)
        {
            ResCalculation modelobj = new ResCalculation();
            try
            {
                modelobj.FacultyId = ObjResCal.FacultyId;
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
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<ResCalculation> ObjLstRC = new List<ResCalculation>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ResCalculation ObjRC = new ResCalculation();
                        ObjRC.Id = Convert.ToInt32(dr["Id"]);
                        ObjRC.InstanceName = Convert.ToString(dr["InstanceName"]);

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

        #region ResIncProgrammeInstancePartGetByProgInstId
        [HttpPost]
        public HttpResponseMessage ResIncProgInstancePartGetByProgInstId(ResCalculation ObjResCal)
        {
            ResCalculation modelobj = new ResCalculation();
            try
            {
                modelobj.ProgramInstanceId = ObjResCal.ProgramInstanceId;
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
                List<ResCalculation> ObjLstRC = new List<ResCalculation>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ResCalculation ObjRC = new ResCalculation();
                        ObjRC.ProgramInstancePartId = Convert.ToInt64(dr["Id"]);
                        ObjRC.PartName = Convert.ToString(dr["PartName"]);
                        ObjRC.PartShortName = Convert.ToString(dr["PartShortName"]);
                        ObjRC.InstancePartTermName = "";
                        // 
                        ObjLstRC.Add(ObjRC);
                        //  List<ResCalculation> ObjLstRIPT = new List<ResCalculation>();
                        //  List <PartTermList> ObjLstRIPT = new List<PartTermList>();
                        SqlCommand Cmd = new SqlCommand("ResIncProgrammeInstancePartTermGetByProgInstPartId", con);
                        Cmd.CommandType = CommandType.StoredProcedure;
                        Cmd.Parameters.AddWithValue("@ProgramInstancePartId", ObjRC.ProgramInstancePartId);
                        //sda.SelectCommand = Cmd;
                        SqlDataAdapter Da1 = new SqlDataAdapter();
                        DataTable Dt1 = new DataTable();
                        Da1.SelectCommand = Cmd;
                        Da1.Fill(Dt1);

                        //// List<ResCalculation> ObjLstRC = new List<ResCalculation>();

                        
                        if (Dt1.Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in Dt1.Rows)
                            {
                                //PartTermList ObjIPT = new PartTermList();
                                ResCalculation ObjIPT = new ResCalculation();
                                ObjIPT.ProgramInstancePartId = Convert.ToInt64(ObjRC.ProgramInstancePartId);
                                ObjIPT.IncProgrammeInstancePartTermId = Convert.ToInt64(dr1["Id"]);
                                ObjIPT.InstancePartTermName = Convert.ToString(dr1["InstancePartTermName"]);

                                ObjLstRC.Add(ObjIPT);
                            }

                        }

                        else { }
                        //ObjRC.InstPartTermList = ObjLstRIPT;
                        
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

        #region ResCalculationGetByProgInsId
        [HttpPost]
        public HttpResponseMessage ResCalculationGetByProgInsId(ResCalculation ObjResCal)
        {
            ResCalculation modelobj = new ResCalculation();
            try
            
            {
                modelobj.ProgramInstanceId = ObjResCal.ProgramInstanceId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("ResCalculationGetByProgInsId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgramInstanceId", modelobj.ProgramInstanceId);
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
                        ObjRC.PartName = Convert.ToString(dr["PartName"]);
                        ObjRC.IncProgrammeInstancePartTermId = Convert.ToInt64(dr["IncProgrammeInstancePartTermId"]);
                        ObjRC.InstancePartTermName = Convert.ToString(dr["InstancePartTermName"]);
                        ObjRC.IsResCalcApplicable = Convert.ToBoolean(dr["IsResCalcApplicable"]);
                        ObjRC.AttachResultClass = Convert.ToBoolean(dr["AttachResultClass"]);
                        ObjRC.MstEvaluationId = Convert.ToInt32(dr["MstEvaluationId"]);
                        ObjRC.IsLaunched = (Convert.ToString(dr["IsLaunched"]).IsEmpty() ? false : Convert.ToBoolean(dr["IsLaunched"]));
                        var MstEvaluationId = dr["MstEvaluationId"];
                        if (MstEvaluationId is 3) { }
                        else
                        {
                            var GradeScaleId = dr["GradeScaleId"];
                            if (GradeScaleId is DBNull)
                                ObjRC.GradeScaleId = 0;
                            else
                                ObjRC.GradeScaleId = Convert.ToInt32(dr["GradeScaleId"]);
                        }
                        var ClassGradeTemplateId= dr["ClassGradeTemplateId"];
                        if(ClassGradeTemplateId is DBNull)
                            ObjRC.ClassGradeTemplateId = 0;
                        else
                        ObjRC.ClassGradeTemplateId = Convert.ToInt32(dr["ClassGradeTemplateId"]);
                        ObjRC.TemplateName = Convert.ToString(dr["TemplateName"]);
                        //ObjRC.IsLaunched = Convert.ToBoolean(dr["IsLaunched"]);

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


        #region ResCalculationAdd
        [HttpPost]
        public HttpResponseMessage ResCalculationAdd(ResCalculation ObjResCal)
        {
            ResCalculation modelobj = new ResCalculation();

            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                modelobj.ProgramInstanceId = ObjResCal.ProgramInstanceId;
                modelobj.MstEvaluationId = ObjResCal.MstEvaluationId;
                modelobj.ClassGradeTemplateId = ObjResCal.ClassGradeTemplateId;
                var InstancePartTermList = ObjResCal.InstPartTermList;
                 if (modelobj.ProgramInstanceId <= 0)
                {
                    return Return.returnHttp("201", "Please enter valid Program Instance Id", null);
                }
                else if (modelobj.MstEvaluationId <= 0)
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

                foreach (PartTermList objInstPartTerm in InstancePartTermList)
                {
                    Int32 Id = objInstPartTerm.Id;
                    Int64 ProgramInstanceId = ObjResCal.ProgramInstanceId;
                    Int64 ProgramInstancePartId = objInstPartTerm.ProgramInstancePartId;
                    Int64 IncProgrammeInstancePartTermId = objInstPartTerm.IncProgrammeInstancePartTermId;
                    Boolean  IsResCalcApplicable = objInstPartTerm.IsResCalcApplicable;
                    Boolean AttachResultClass = objInstPartTerm.AttachResultClass;
                    Int32 MstEvaluationId = ObjResCal.MstEvaluationId;                  
                    Int32 ClassGradeTemplateId = ObjResCal.ClassGradeTemplateId;
                   
                    InstPartTermArray[i] = new SqlCommand("ResCalculationAdd", con, ST);
                    InstPartTermArray[i].CommandType = CommandType.StoredProcedure;
                    InstPartTermArray[i].Parameters.AddWithValue("@Id", Id);
                    InstPartTermArray[i].Parameters.AddWithValue("@ProgramInstanceId", ProgramInstanceId);
                    InstPartTermArray[i].Parameters.AddWithValue("@ProgramInstancePartId", ProgramInstancePartId);
                    InstPartTermArray[i].Parameters.AddWithValue("@IncProgrammeInstancePartTermId", IncProgrammeInstancePartTermId);
                    InstPartTermArray[i].Parameters.AddWithValue("@IsResCalcApplicable", IsResCalcApplicable);
                    InstPartTermArray[i].Parameters.AddWithValue("@AttachResultClass", AttachResultClass);
                    InstPartTermArray[i].Parameters.AddWithValue("@MstEvaluationId", MstEvaluationId);
                    if (IsResCalcApplicable == true || AttachResultClass == true)                    
                        InstPartTermArray[i].Parameters.AddWithValue("@ClassGradeTemplateId", ClassGradeTemplateId);                   
                    else
                        InstPartTermArray[i].Parameters.AddWithValue("@ClassGradeTemplateId", DBNull.Value);
                    InstPartTermArray[i].Parameters.AddWithValue("@CreatedBy", modelobj.CreatedBy);

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

        #region ResCalculationDelete
        [HttpPost]
        public HttpResponseMessage ResCalculationDelete(ResCalculation ObjResCal)
        {
            ResCalculation modelobj = new ResCalculation();

            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                var InstancePartTermList = ObjResCal.InstPartTermList;
                if (String.IsNullOrEmpty(Convert.ToString(InstancePartTermList)))
                {
                    return Return.returnHttp("201", "Instance Part Term List is empty or null", null);
                }
                else
                {
                    SqlCommand[] InstPartTermArray = new SqlCommand[InstancePartTermList.Count()];
                    var i = 0;
                    String msg = "";


                    foreach (PartTermList objInstPartTerm in InstancePartTermList)
                    {
                        int Id = objInstPartTerm.Id;
                        InstPartTermArray[i] = new SqlCommand("ResCalculationDelete", con, ST);
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

        #region ResultConfigurationLaunch
        [HttpPost]
        public HttpResponseMessage ResultConfigurationLaunch(LaunchResultConfiguration ObjResLaunch)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                //if (ObjResLaunch.ResCalculation == true || ObjResLaunch.ResCourseEvalSystem == true || ObjResLaunch.ResExemptionMarkConfig == true)
                //{

                SqlCommand cmd = new SqlCommand("ResConfigurationLaunchforResCal", con);

                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@ProgramInstanceId", ObjResLaunch.ProgramInstanceId);
                cmd.Parameters.AddWithValue("@AcademicYearId", ObjResLaunch.AcademicYearId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                //cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                if (strMessage.Equals("TRUE"))
                {
                    return Return.returnHttp("200", "Programme Launched successfully", null);
                }

                else
                {
                    return Return.returnHttp("201", strMessage.ToString(), null);
                }
                //}
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion
    }
}
