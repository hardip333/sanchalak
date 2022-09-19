using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using MSUISApi.BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;
using Newtonsoft.Json.Linq;

namespace MSUISApi.Controllers
{
    public class PostVerificationAddOnCriteriaController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

        #region GetAdmProgrammeAddOnCriteriabyApplicationId
        [HttpPost]
        public HttpResponseMessage AdmProgrammeAddOnCriteriaGetbyApplicationId(AdmProgrammeAddOnCriteria ObjAddOnCriteria)
        {
            AdmProgrammeAddOnCriteria modelobj = new AdmProgrammeAddOnCriteria();


            try
            {
                modelobj.ProgrammeInstancePartTermId = ObjAddOnCriteria.ProgrammeInstancePartTermId;
                modelobj.PostAdmApplicationId = ObjAddOnCriteria.PostAdmApplicationId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                SqlCommand cmd = new SqlCommand("PostAdmProgrammeAddOnCriteriaGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "AdmProgrammeAddOnCriteriaGetById");
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", modelobj.ProgrammeInstancePartTermId);
                cmd.Parameters.AddWithValue("@AdmApplicationId", modelobj.PostAdmApplicationId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<AdmProgrammeAddOnCriteria> ObjLstAPAC = new List<AdmProgrammeAddOnCriteria>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {

                        AdmProgrammeAddOnCriteria ObjAPAC = new AdmProgrammeAddOnCriteria();

                        ObjAPAC.Id = Convert.ToInt32(dr["Id"]);
                        // ObjAPAC.AdmApplicationId = Convert.ToInt32(dr["AdmApplicationId"]);
                        ObjAPAC.TitleName = (dr["TitleName"].ToString());
                        ObjAPAC.TitleType = (dr["TitleType"].ToString());
                        ObjAPAC.ProgrammeInstancePartTermId = Convert.ToInt32(dr["ProgrammeInstancePartTermId"]);
                        ObjAPAC.AddOnValue = (dr["AddOnValue"].ToString());
                        ObjLstAPAC.Add(ObjAPAC);
                    }

                }
                return Return.returnHttp("200", ObjLstAPAC, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region AddAdmProgrammeAddOnCriteriabyApplicationId
        [HttpPost]
        public HttpResponseMessage AdmStudentAddOnInformationAddbyApplicationId(JObject jsonobject)
        {
            AdmStudentAddOnInformation modelobj = new AdmStudentAddOnInformation();
            dynamic jsonData = jsonobject;

            try
            {
                modelobj.ApplicationId = jsonData.AdmApplicationId;
                modelobj.ProgrammeAddOnCriteriaId = jsonData.ProgrammeAddOnCriteriaId;
                modelobj.AddOnValue = jsonData.AddOnValue;
                modelobj.TitleType = jsonData.TitleType;
                if (Convert.ToString(modelobj.TitleType) == "number")
                {
                    if (Convert.ToInt32(modelobj.AddOnValue) < 0)
                        return Return.returnHttp("201", "Negative Add On Value ", null);
                }

                if (modelobj.ApplicationId <= 0)
                {
                    return Return.returnHttp("201", " Valid Application Id Required", null);
                }
                else if (modelobj.ProgrammeAddOnCriteriaId <= 0)
                {
                    return Return.returnHttp("201", " Valid Add On Criteria Id Required", null);
                }
                else if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.AddOnValue)))
                {
                    return Return.returnHttp("201", "Add On Value Required", null);
                }
                else
                {

                    String token = Request.Headers.GetValues("token").FirstOrDefault();
                    TokenOperation ac = new TokenOperation();
                    string responseToken = ac.ValidateToken(token);

                    if (responseToken == "0")
                    {
                        return Return.returnHttp("0", null, null);
                    }


                    modelobj.CreatedBy = Convert.ToInt64(responseToken);
                    //modelobj.CreatedBy = 4;
                    SqlCommand cmd = new SqlCommand("PostAdmStudentAddOnInformationAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ApplicationId", modelobj.ApplicationId);
                    cmd.Parameters.AddWithValue("@ProgrammeAddOnCriteriaId", modelobj.ProgrammeAddOnCriteriaId);
                    cmd.Parameters.AddWithValue("@AddOnValue", modelobj.AddOnValue);
                    cmd.Parameters.AddWithValue("@CreatedBy", modelobj.CreatedBy);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    con.Close();

                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Your data has been Add and saved successfully.";
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