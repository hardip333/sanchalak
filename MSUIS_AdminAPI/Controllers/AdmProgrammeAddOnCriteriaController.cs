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
    public class AdmProgrammeAddOnCriteriaController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

        #region AdmProgrammeAddOnCriteriaGetAll
        [HttpPost]
        public HttpResponseMessage AdmProgrammeAddOnCriteriaGetAll()
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
                SqlCommand cmd = new SqlCommand("AdmProgrammeAddOnCriteriaGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "AdmProgrammeAddOnCriteriaGetAll");
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

        #region AdmProgrammeAddOnCriteriaGet
        [HttpPost]
        public HttpResponseMessage AdmProgrammeAddOnCriteriaGet(AdmProgrammeAddOnCriteria ObjAddOnCriteria)
        {
            AdmProgrammeAddOnCriteria modelobj = new AdmProgrammeAddOnCriteria();
            

            try
            {
                modelobj.ProgrammeInstancePartTermId = ObjAddOnCriteria.ProgrammeInstancePartTermId;
                modelobj.AdmApplicationId = ObjAddOnCriteria.AdmApplicationId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                SqlCommand cmd = new SqlCommand("AdmProgrammeAddOnCriteriaGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "AdmProgrammeAddOnCriteriaGetById");
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", modelobj.ProgrammeInstancePartTermId);
                cmd.Parameters.AddWithValue("@AdmApplicationId", modelobj.AdmApplicationId);
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

        #region AdmProgrammeAddOnCriteriaGetById
        [HttpPost]
        public HttpResponseMessage AdmProgrammeAddOnCriteriaGetById(AdmProgrammeAddOnCriteria ObjAddOnCriteria)
        {
            AdmProgrammeAddOnCriteria modelobj = new AdmProgrammeAddOnCriteria();


            try
            {
                modelobj.ProgrammeInstancePartTermId = ObjAddOnCriteria.ProgrammeInstancePartTermId;              
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                SqlCommand cmd = new SqlCommand("AdmProgrammeAddOnCriteriaGetById", con);
                cmd.CommandType = CommandType.StoredProcedure;
               
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", modelobj.ProgrammeInstancePartTermId);
                
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

        #region AdmProgrammeAddOnCriteriaAdd
        [HttpPost]
        public HttpResponseMessage AdmProgrammeAddOnCriteriaAdd(AdmProgrammeAddOnCriteria ObjAddOnCriteria)
        {
            AdmProgrammeAddOnCriteria modelobj = new AdmProgrammeAddOnCriteria();

            try
            {                
                modelobj.ProgrammeInstancePartTermId = ObjAddOnCriteria.ProgrammeInstancePartTermId;
                modelobj.TitleName = ObjAddOnCriteria.TitleName;
                modelobj.TitleType = ObjAddOnCriteria.TitleType;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                modelobj.CreatedBy = Convert.ToInt32(res);
                //modelobj.CreatedBy = 2;
                if (modelobj.ProgrammeInstancePartTermId<=0)
                {
                    return Return.returnHttp("201", "Programme Instance Part Term Id is null or empty", null);
                }
                else if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.TitleType)))
                {
                    return Return.returnHttp("201", "Please select Title Type", null);
                }
                else if(string.IsNullOrWhiteSpace(Convert.ToString(modelobj.TitleName)))
                {
                    return Return.returnHttp("201", "Title Name is null or empty", null);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("AdmProgrammeAddOnCriteriaAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;                 
                    cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", modelobj.ProgrammeInstancePartTermId);
                    cmd.Parameters.AddWithValue("@TitleName", modelobj.TitleName);
                    cmd.Parameters.AddWithValue("@TitleType", modelobj.TitleType);
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

        #region AdmProgrammeAddOnCriteriaEdit
        [HttpPost]
        public HttpResponseMessage AdmProgrammeAddOnCriteriaEdit(AdmProgrammeAddOnCriteria ObjAddOnCriteria)
        {
            AdmProgrammeAddOnCriteria modelobj = new AdmProgrammeAddOnCriteria();

            try
            {
                modelobj.Id = Convert.ToInt32(ObjAddOnCriteria.Id);
                modelobj.ProgrammeInstancePartTermId = ObjAddOnCriteria.ProgrammeInstancePartTermId;
                modelobj.TitleName = ObjAddOnCriteria.TitleName;
                modelobj.TitleType = ObjAddOnCriteria.TitleType;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                modelobj.ModifiedBy = Convert.ToInt32(res);
                //modelobj.ModifiedBy = 5;
                if (modelobj.Id<=0)
                {
                    return Return.returnHttp("201", "Please Enter valid Id", null);
                }
                else if (modelobj.ProgrammeInstancePartTermId <= 0)
                {
                    return Return.returnHttp("201", "Programme Instance Part Term Id is null or empty", null);
                }
                else if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.TitleType)))
                {
                    return Return.returnHttp("201", "Please select Title Type", null);
                }
                else if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.TitleName)))
                {
                    return Return.returnHttp("201", "Title Name is null or empty", null);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("AdmProgrammeAddOnCriteriaEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Flag", "AdmProgrammeAddOnCriteriaEdit");
                    cmd.Parameters.AddWithValue("@Id", modelobj.Id);
                    cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", modelobj.ProgrammeInstancePartTermId);
                    cmd.Parameters.AddWithValue("@TitleName", modelobj.TitleName);
                    cmd.Parameters.AddWithValue("@TitleType", modelobj.TitleType);
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

        #region AdmProgrammeAddOnCriteriaDelete
        [HttpPost]
        public HttpResponseMessage AdmProgrammeAddOnCriteriaDelete(AdmProgrammeAddOnCriteria ObjAddOnCriteria)
        {
            AdmProgrammeAddOnCriteria modelobj = new AdmProgrammeAddOnCriteria();

            try
            {
                modelobj.Id = Convert.ToInt32(ObjAddOnCriteria.Id);
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                if (modelobj.Id<=0)
                {
                    return Return.returnHttp("201", "Please Enter valid Id", null);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("AdmProgrammeAddOnCriteriaDelete", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Flag", "AdmProgrammeAddOnCriteriaDelete");
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

        #region Megha Code  - API for merit list dropdown
        [HttpPost]
        public HttpResponseMessage AdmProgrammeAddOnCriteriaForMeritList(AdmProgrammeAddOnCriteria ObjAddOnCriteria)
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
                SqlCommand cmd = new SqlCommand("AdmProgrammeAddOnCriteriaForMeritList", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Flag", "admin");
                cmd.Parameters.AddWithValue("@IsDeleted", false);
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ObjAddOnCriteria.ProgrammeInstancePartTermId);
                cmd.Parameters.AddWithValue("@Id", ObjAddOnCriteria.Id);

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

    }
}