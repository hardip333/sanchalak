using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using Newtonsoft.Json.Linq;
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
    //take AdmEligibilityGroupComponent as AdmEligibilityCriteriaComponent here Archana Nigam
    public class AdmEligibilityCriteriaComponentController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

        #region AdmEligibilityCriteriaComponentGetAll
        [HttpPost]
        public HttpResponseMessage AdmEligibilityCriteriaComponentGetAll()
        {
            
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string responseToken = ac.ValidateToken(token);

                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                //modelobj.ProgramInstancePartTermId = ObjGroupComponent.ProgramInstancePartTermId;
                SqlCommand cmd = new SqlCommand("AdmEligibilityGroupComponentGet", con);
                //cmd.Parameters.AddWithValue("@ProgramInstancePartTermId", modelobj.ProgramInstancePartTermId);
                cmd.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<AdmEligibilityCriteriaComponent> ObjLstAECC = new List<AdmEligibilityCriteriaComponent>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AdmEligibilityCriteriaComponent ObjAECC = new AdmEligibilityCriteriaComponent();
                        ObjAECC.Id = Convert.ToInt32(dr["Id"]);
                        ObjAECC.EligibleDegreeId = Convert.ToInt32(dr["EligibleDegreeId"]);
                        ObjAECC.EligibleDegreeName = (dr["EligibleDegreeName"].ToString());
                        ObjAECC.EligibilitySpecializationId = Convert.ToInt32(dr["EligibilitySpecializationId"]);
                        ObjAECC.EligibilitySpecializationName = (dr["EligibilitySpecializationName"].ToString());
                        var MinimumPercentage = dr["MinimumPercentage"];
                        if (MinimumPercentage is DBNull)
                        {
                            ObjAECC.MinimumPercentage = null;
                        }
                        else
                        {
                            ObjAECC.MinimumPercentage = Convert.ToInt32(dr["MinimumPercentage"]);
                        }
                        ObjAECC.GradeCGPA = (dr["GradeCGPA"].ToString());
                        ObjAECC.AdditionalInfo = (dr["AdditionalInfo"].ToString());
                        ObjAECC.EligibilityGroupId = Convert.ToInt32(dr["EligibilityGroupId"]);
                        ObjAECC.EligibilityGroup = (dr["EligibilityGroup"].ToString());
                        
                        ObjLstAECC.Add(ObjAECC);
                    }

                }
                return Return.returnHttp("200", ObjLstAECC, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region AdmEligibilityCriteriaComponentGetById
        [HttpPost]
        public HttpResponseMessage AdmEligibilityCriteriaComponentGetById(AdmEligibilityCriteriaComponent ObjGroupComponent)
        {
            AdmEligibilityCriteriaComponent modelobj = new AdmEligibilityCriteriaComponent();
            
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string responseToken = ac.ValidateToken(token);

                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                modelobj.ProgramInstancePartTermId = ObjGroupComponent.ProgramInstancePartTermId;
                SqlCommand cmd = new SqlCommand("AdmEligibilityGroupComponentGetById", con);
                cmd.Parameters.AddWithValue("@ProgramInstancePartTermId", modelobj.ProgramInstancePartTermId);
                cmd.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<AdmEligibilityCriteriaComponent> ObjLstAECC = new List<AdmEligibilityCriteriaComponent>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AdmEligibilityCriteriaComponent ObjAECC = new AdmEligibilityCriteriaComponent();
                        ObjAECC.Id = Convert.ToInt32(dr["Id"]);
                        ObjAECC.EligibleDegreeId = Convert.ToInt32(dr["EligibleDegreeId"]);
                        ObjAECC.EligibleDegreeName = (dr["EligibleDegreeName"].ToString());
                        ObjAECC.EligibilitySpecializationId = Convert.ToInt32(dr["EligibilitySpecializationId"]);
                        ObjAECC.EligibilitySpecializationName = (dr["EligibilitySpecializationName"].ToString());
                        var MinimumPercentage = dr["MinimumPercentage"];
                        if (MinimumPercentage is DBNull)
                        {
                            ObjAECC.MinimumPercentage = null;
                        }
                        else
                        {
                        ObjAECC.MinimumPercentage = Convert.ToInt32(dr["MinimumPercentage"]);
                        }
                       // ObjAECC.MinimumPercentage = Convert.ToInt32(dr["MinimumPercentage"]);
                        ObjAECC.GradeCGPA = (dr["GradeCGPA"].ToString());
                        ObjAECC.AdditionalInfo = (dr["AdditionalInfo"].ToString());
                        ObjAECC.EligibilityGroupId = Convert.ToInt32(dr["EligibilityGroupId"]);
                        ObjAECC.EligibilityGroup = (dr["EligibilityGroup"].ToString());
                        ObjLstAECC.Add(ObjAECC);
                    }

                }
                return Return.returnHttp("200", ObjLstAECC, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region AdmEligibilityCriteriaComponentAdd
        [HttpPost]
        public HttpResponseMessage AdmEligibilityCriteriaComponentAdd(AdmEligibilityCriteriaComponent ObjGroupComponent)
        {
            AdmEligibilityCriteriaComponent modelobj = new AdmEligibilityCriteriaComponent();
            
            try
            {
                modelobj.EligibleDegreeId = ObjGroupComponent.EligibleDegreeId;
                modelobj.EligibilitySpecializationId = ObjGroupComponent.EligibilitySpecializationId;
                modelobj.MinimumPercentage = ObjGroupComponent.MinimumPercentage;
                modelobj.GradeCGPA = ObjGroupComponent.GradeCGPA;
                modelobj.ProgramInstancePartTermId = ObjGroupComponent.ProgramInstancePartTermId;
                modelobj.EligibilityGroupId = ObjGroupComponent.EligibilityGroupId;
                modelobj.AdditionalInfo = ObjGroupComponent.AdditionalInfo;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string responseToken = ac.ValidateToken(token);

                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                modelobj.CreatedBy = Convert.ToInt32(responseToken);
                //modelobj.CreatedBy = 3;
                if (modelobj.EligibleDegreeId<=0)
                {
                    return Return.returnHttp("201", "Please select Degree", null);
                }
                else if (modelobj.EligibilitySpecializationId<=0)
                {
                    return Return.returnHttp("201", "Please select Specialization", null);
                }
                //else if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.MinimumPercentage)))
                //{
                //    return Return.returnHttp("201", "Please Enter Minimum Percentage", null);
                //}
                //else if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.GradeCGPA)))
                //{
                //    return Return.returnHttp("201", "Please Enter Grade/CGPA", null);
                //}
                else if (modelobj.ProgramInstancePartTermId<=0)
                {
                    return Return.returnHttp("201", "Please select Program Instance Part Term Id", null);
                }
                else if (modelobj.EligibilityGroupId<=0)
                {
                    return Return.returnHttp("201", "Please select Eligibility Group", null);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("AdmEligibilityGroupComponentAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EligibleDegreeId", modelobj.EligibleDegreeId);
                    cmd.Parameters.AddWithValue("@EligibilitySpecializationId", modelobj.EligibilitySpecializationId);
                    if (modelobj.MinimumPercentage == null)
                        cmd.Parameters.AddWithValue("@MinimumPercentage", DBNull.Value);
                    else
                    cmd.Parameters.AddWithValue("@MinimumPercentage", modelobj.MinimumPercentage);
                    cmd.Parameters.AddWithValue("@GradeCGPA", modelobj.GradeCGPA);
                    cmd.Parameters.AddWithValue("@ProgramInstancePartTermId", modelobj.ProgramInstancePartTermId);
                    cmd.Parameters.AddWithValue("@EligibilityGroupId", modelobj.EligibilityGroupId);
                    cmd.Parameters.AddWithValue("@AdditionalInfo", modelobj.AdditionalInfo);
                    cmd.Parameters.AddWithValue("@UserId", modelobj.CreatedBy);
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

        #region AdmEligibilityCriteriaComponentEdit
        [HttpPost]
        public HttpResponseMessage AdmEligibilityCriteriaComponentEdit(AdmEligibilityCriteriaComponent ObjGroupComponent)
        {
            AdmEligibilityCriteriaComponent modelobj = new AdmEligibilityCriteriaComponent();
            
            try
            {
                modelobj.Id = Convert.ToInt32(ObjGroupComponent.Id);
                modelobj.EligibleDegreeId = ObjGroupComponent.EligibleDegreeId;
                modelobj.EligibilitySpecializationId = ObjGroupComponent.EligibilitySpecializationId;
                modelobj.MinimumPercentage = ObjGroupComponent.MinimumPercentage;
                modelobj.GradeCGPA = ObjGroupComponent.GradeCGPA;
                modelobj.ProgramInstancePartTermId = ObjGroupComponent.ProgramInstancePartTermId;
                modelobj.EligibilityGroupId = ObjGroupComponent.EligibilityGroupId;
                modelobj.AdditionalInfo = ObjGroupComponent.AdditionalInfo;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string responseToken = ac.ValidateToken(token);

                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                modelobj.ModifiedBy = Convert.ToInt32(responseToken);
                //modelobj.ModifiedBy = 4;
                if (modelobj.Id<=0)
                {
                    return Return.returnHttp("201", "Please Enter valid Id", null);
                }
                else if (modelobj.EligibleDegreeId <= 0)
                {
                    return Return.returnHttp("201", "Please select Degree", null);
                }
                else if (modelobj.EligibilitySpecializationId <= 0)
                {
                    return Return.returnHttp("201", "Please select Specialization", null);
                }
                //else if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.MinimumPercentage)))
                //{
                //    return Return.returnHttp("201", "Please Enter Minimum Percentage", null);
                //}
                //else if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.GradeCGPA)))
                //{
                //    return Return.returnHttp("201", "Please Enter Grade/CGPA", null);
                //}
                else if (modelobj.ProgramInstancePartTermId <= 0)
                {
                    return Return.returnHttp("201", "Please select Program Instance Part Term Id", null);
                }
                else if (modelobj.EligibilityGroupId <= 0)
                {
                    return Return.returnHttp("201", "Please select Eligibility Group", null);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("AdmEligibilityGroupComponentEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Flag", "AdmEligibilityGroupComponentEdit");
                    cmd.Parameters.AddWithValue("@Id", modelobj.Id);
                    cmd.Parameters.AddWithValue("@EligibleDegreeId", modelobj.EligibleDegreeId);
                    cmd.Parameters.AddWithValue("@EligibilitySpecializationId", modelobj.EligibilitySpecializationId);
                    if (modelobj.MinimumPercentage == null)
                        cmd.Parameters.AddWithValue("@MinimumPercentage", DBNull.Value);
                    else
                    cmd.Parameters.AddWithValue("@MinimumPercentage", modelobj.MinimumPercentage);
                    cmd.Parameters.AddWithValue("@GradeCGPA", modelobj.GradeCGPA);
                    cmd.Parameters.AddWithValue("@ProgramInstancePartTermId", modelobj.ProgramInstancePartTermId);
                    cmd.Parameters.AddWithValue("@EligibilityGroupId", modelobj.EligibilityGroupId);
                    cmd.Parameters.AddWithValue("@AdditionalInfo", modelobj.AdditionalInfo);
                    cmd.Parameters.AddWithValue("@UserId", modelobj.ModifiedBy);
                    //cmd.Parameters.AddWithValue("@UserId", UserId);

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

                        return Return.returnHttp("202", "Invalid Id.", null);
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

        #region AdmEligibilityCriteriaComponentDelete
        [HttpPost]
        public HttpResponseMessage AdmEligibilityCriteriaComponentDelete(AdmEligibilityCriteriaComponent ObjGroupComponent)
        {
            AdmEligibilityCriteriaComponent modelobj = new AdmEligibilityCriteriaComponent();
            
            try
            {
                modelobj.Id = Convert.ToInt32(ObjGroupComponent.Id);

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string responseToken = ac.ValidateToken(token);

                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                if (modelobj.Id<=0)
                {
                    return Return.returnHttp("201", "Please Enter valid Id", null);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("AdmEligibilityGroupComponentDelete", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Flag", "AdmEligibilityGroupComponentDelete");
                    cmd.Parameters.AddWithValue("@Id", modelobj.Id);

                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    con.Close();

                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Your data has been Delete successfully.";
                    }
                    if (modelobj.Id < 1)
                        return Return.returnHttp("202", "Invalid Id.", null);
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
    }
}