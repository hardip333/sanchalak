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
    public class AdmEligibilityGroupController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

        #region AdmEligibilityGroupGet
        [HttpPost]
        public HttpResponseMessage AdmEligibilityGroupGet()
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

                SqlCommand cmd = new SqlCommand("AdmEligibilityGroupGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<AdmEligibilityGroup> ObjLstAEG = new List<AdmEligibilityGroup>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AdmEligibilityGroup ObjAEG = new AdmEligibilityGroup();
                        ObjAEG.Id = Convert.ToInt32(dr["Id"]);
                        ObjAEG.EligibilityGroup = (dr["EligibilityGroup"].ToString());
                        ObjAEG.ProgrammeInstancePartTermId = Convert.ToInt32(dr["ProgrammeInstancePartTermId"]);
                        ObjLstAEG.Add(ObjAEG);
                    }

                }
                return Return.returnHttp("200", ObjLstAEG, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region AdmEligibilityGroupGetById
        [HttpPost]
        public HttpResponseMessage AdmEligibilityGroupGetById(AdmEligibilityGroup ObjEligibilityGroup)
        {
            AdmEligibilityGroup modelobj = new AdmEligibilityGroup();

            try
            {
                modelobj.ProgrammeInstancePartTermId = ObjEligibilityGroup.ProgrammeInstancePartTermId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("AdmEligibilityGroupGetById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", modelobj.ProgrammeInstancePartTermId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<AdmEligibilityGroup> ObjLstAEG = new List<AdmEligibilityGroup>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AdmEligibilityGroup ObjAEG = new AdmEligibilityGroup();
                        ObjAEG.Id = Convert.ToInt32(dr["Id"]);
                        ObjAEG.EligibilityGroup = (dr["EligibilityGroup"].ToString());
                        ObjAEG.ProgrammeInstancePartTermId = Convert.ToInt32(dr["ProgrammeInstancePartTermId"]);
                        ObjLstAEG.Add(ObjAEG);
                    }

                }
                return Return.returnHttp("200", ObjLstAEG, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region AdmEligibilityGroupAdd
        [HttpPost]
        public HttpResponseMessage AdmEligibilityGroupAdd(AdmEligibilityGroup ObjEligibilityGroup)
        {
            AdmEligibilityGroup modelobj = new AdmEligibilityGroup();
          
            try
            {
                modelobj.EligibilityGroup = ObjEligibilityGroup.EligibilityGroup;
                modelobj.ProgrammeInstancePartTermId = ObjEligibilityGroup.ProgrammeInstancePartTermId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                modelobj.CreatedBy = Convert.ToInt32(res);
                //modelobj.CreatedBy = 2;
                if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.EligibilityGroup)))
                { 
                    return Return.returnHttp("201", "Eligibility is null or empty", null);
                }
                else if (modelobj.ProgrammeInstancePartTermId <= 0)
                {
                    return Return.returnHttp("201", "Please select programme Instance Part Term", null);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("AdmEligibilityGroupAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EligibilityGroup", (modelobj.EligibilityGroup).Trim());
                    cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", modelobj.ProgrammeInstancePartTermId);
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

        #region AdmEligibilityGroupEdit
        [HttpPost]
        public HttpResponseMessage AdmEligibilityGroupEdit(AdmEligibilityGroup ObjEligibilityGroup)
        {
            AdmEligibilityGroup modelobj = new AdmEligibilityGroup();
           
            try
            {
                modelobj.Id = Convert.ToInt32(ObjEligibilityGroup.Id);
                modelobj.EligibilityGroup = ObjEligibilityGroup.EligibilityGroup;
                modelobj.ProgrammeInstancePartTermId = ObjEligibilityGroup.ProgrammeInstancePartTermId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                modelobj.ModifiedBy = Convert.ToInt32(res);
                //modelobj.ModifiedBy = 5;
                if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.EligibilityGroup)))
                { 
                    return Return.returnHttp("201", "Eligibility is null or empty", null);
                }
                else if (modelobj.Id<=0)
                {
                    return Return.returnHttp("201", "Please Enter Id or valid Id", null);
                }
                else if (modelobj.ProgrammeInstancePartTermId<=0)
                {
                    return Return.returnHttp("201", "Please select programme Instance Part Term or valid Id", null);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("AdmEligibilityGroupEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Flag", "AdmEligibilityGroupEdit");
                    cmd.Parameters.AddWithValue("@Id", modelobj.Id);
                    cmd.Parameters.AddWithValue("@EligibilityGroup", (modelobj.EligibilityGroup).Trim());
                    cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", modelobj.ProgrammeInstancePartTermId);
                    cmd.Parameters.AddWithValue("@ModifiedBy", modelobj.ModifiedBy);
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

        #region AdmEligibilityGroupDelete
        [HttpPost]
        public HttpResponseMessage AdmEligibilityGroupDelete(AdmEligibilityGroup ObjEligibilityGroup)
        {
            AdmEligibilityGroup modelobj = new AdmEligibilityGroup();
           
            try
            {
                modelobj.Id = Convert.ToInt32(ObjEligibilityGroup.Id);
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                if (modelobj.Id<=0)
                {
                    return Return.returnHttp("201", "Please Enter Id or valid Id", null);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("AdmEligibilityGroupDelete", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Flag", "AdmEligibilityGroupDelete");
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
