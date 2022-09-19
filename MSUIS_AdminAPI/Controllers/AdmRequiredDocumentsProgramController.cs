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
    public class AdmRequiredDocumentsProgramController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

        #region AdmRequiredDocumentsProgramGet
        [HttpPost]
        public HttpResponseMessage AdmRequiredDocumentsProgramGet(JObject jsonobject)
        {
            AdmRequiredDocumentsProgram modelobj = new AdmRequiredDocumentsProgram();
            dynamic jsonData = jsonobject;
            try
            {
                //String token = Request.Headers.GetValues("token").FirstOrDefault();
                //TokenOperation ac = new TokenOperation();
                //string responseToken = ac.ValidateToken(token);

                //if (responseToken == "0")
                //{
                //    return Return.returnHttp("0", null, null);
                //}


                SqlCommand cmd = new SqlCommand("AdmRequiredDocumentsProgramGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "AdmRequiredDocumentsProgramGetAll");
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<AdmRequiredDocumentsProgram> ObjLstRD = new List<AdmRequiredDocumentsProgram>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AdmRequiredDocumentsProgram ObjRD = new AdmRequiredDocumentsProgram();
                        ObjRD.Id = Convert.ToInt32(dr["Id"]);
                        ObjRD.ProgramId = Convert.ToInt32(dr["ProgramId"]);
                        ObjRD.ProgrammeName = (dr["ProgrammeName"].ToString());
                        ObjRD.DocumentId = Convert.ToInt32(dr["DocumentId"]);
                        ObjRD.NameOfTheDocument = (dr["NameOfTheDocument"].ToString());
                        ObjRD.DocumentType = Convert.ToBoolean(dr["DocumentType"]);
                        ObjLstRD.Add(ObjRD);
                    }

                }
                return Return.returnHttp("200", ObjLstRD, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region AdmRequiredDocumentsProgramGetdocument
        [HttpPost]
        public HttpResponseMessage AdmRequiredDocumentsProgramGetdocument(JObject jsonobject)
        {

            AdmRequiredDocumentsProgram modelobj = new AdmRequiredDocumentsProgram();
            dynamic jsonData = jsonobject;
            try
            {
                modelobj.ProgramId = jsonData.ProgrammeId;
                modelobj.AdmissionApplicationId = jsonData.AdmissionApplicationId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string responseToken = ac.ValidateToken(token);

                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                modelobj.Id = Convert.ToInt32(responseToken);
                SqlCommand cmd = new SqlCommand("AdmRequiredDocumentsProgramGet", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Flag", "AdmRequiredDocumentsProgramGetById");
                cmd.Parameters.AddWithValue("@ProgramId", modelobj.ProgramId);
                cmd.Parameters.AddWithValue("@AdmissionApplicationId", modelobj.AdmissionApplicationId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<AdmRequiredDocumentsProgram> ObjLstRD = new List<AdmRequiredDocumentsProgram>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AdmRequiredDocumentsProgram ObjRD = new AdmRequiredDocumentsProgram();
                        ObjRD.DocumentId = Convert.ToInt32(dr["DocumentId"]);
                        ObjRD.NameOfTheDocument = (dr["NameOfTheDocument"].ToString());
                        ObjRD.documentexits = Convert.ToBoolean(dr["documentexits"]);
                        ObjLstRD.Add(ObjRD);
                    }

                }
                return Return.returnHttp("200", ObjLstRD, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region AdmRequiredDocumentsProgramGetByIdPre
        [HttpPost]
        public HttpResponseMessage AdmRequiredDocumentsProgramGetByIdPre(JObject jsonobject)
        {

            AdmRequiredDocumentsProgram modelobj = new AdmRequiredDocumentsProgram();
            dynamic jsonData = jsonobject;
            try
            {
                modelobj.ProgrammeInstancePartTermId = jsonData.ProgrammeInstancePartTermId;               
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string responseToken = ac.ValidateToken(token);

                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                modelobj.Id = Convert.ToInt32(responseToken);
                SqlCommand cmd = new SqlCommand("AdmRequiredDocumentsProgramGet", con);
                cmd.CommandType = CommandType.StoredProcedure;               
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", modelobj.ProgrammeInstancePartTermId);
                cmd.Parameters.AddWithValue("@Flag", "AdmRequiredDocumentsProgramGetByProgrammePartTermId");
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<AdmRequiredDocumentsProgram> ObjLstRD = new List<AdmRequiredDocumentsProgram>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AdmRequiredDocumentsProgram ObjRD = new AdmRequiredDocumentsProgram();
                        ObjRD.Id = Convert.ToInt32(dr["Id"]);
                        ObjRD.DocumentId = Convert.ToInt32(dr["DocumentId"]);
                        ObjRD.NameOfTheDocument = (dr["NameOfTheDocument"].ToString());
                        ObjRD.ProgramId = Convert.ToInt32(dr["ProgramId"]);
                        ObjRD.ProgrammeName = (dr["ProgrammeName"].ToString());
                        ObjRD.DocumentType = Convert.ToBoolean(dr["DocumentType"]);
                        var IsCompulsoryDocument = dr["IsCompulsoryDocument"];
                        if(IsCompulsoryDocument is DBNull)
                            ObjRD.IsCompulsoryDocument = false;
                        else
                        ObjRD.IsCompulsoryDocument = Convert.ToBoolean(dr["IsCompulsoryDocument"]);
                        ObjLstRD.Add(ObjRD);
                    }

                }
                return Return.returnHttp("200", ObjLstRD, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region AdmRequiredDocumentsProgramAdd
        [HttpPost]
        public HttpResponseMessage AdmRequiredDocumentsProgramAdd(JObject jsonobject)
        {
            AdmRequiredDocumentsProgram modelobj = new AdmRequiredDocumentsProgram();
            dynamic jsonData = jsonobject;
            try
            {
                modelobj.ProgramId = jsonData.ProgramId;
                modelobj.DocumentId = jsonData.DocumentId;
                modelobj.DocumentType = jsonData.DocumentType;
                modelobj.IsCompulsoryDocument = jsonData.IsCompulsoryDocument;
                modelobj.ProgrammeInstancePartTermId = jsonData.ProgrammeInstancePartTermId;

                if (modelobj.ProgramId <= 0)
                {
                    return Return.returnHttp("201", " Valid Program Id Required", null);
                }
                else if (modelobj.DocumentId <= 0)
                {
                    return Return.returnHttp("201", " Valid Document Id Required", null);
                }
                else if (modelobj.ProgrammeInstancePartTermId <= 0)
                {
                    return Return.returnHttp("201", "Valid ProgrammeInstancePartTerm Id Required", null);
                }
               
                else if (jsonData.DocumentType == null)
                {
                    return Return.returnHttp("201", "Valid Document Type Required", null);
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

                    modelobj.CreatedBy = Convert.ToInt32(responseToken);

                    SqlCommand cmd = new SqlCommand("AdmRequiredDocumentsProgramAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ProgramId", modelobj.ProgramId);
                    cmd.Parameters.AddWithValue("@DocumentId", modelobj.DocumentId);
                    cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", modelobj.ProgrammeInstancePartTermId);
                    cmd.Parameters.AddWithValue("@DocumentType", modelobj.DocumentType);
                    cmd.Parameters.AddWithValue("@IsCompulsoryDocument", modelobj.IsCompulsoryDocument);
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

        #region AdmRequiredDocumentsProgramEdit
        [HttpPost]
        public HttpResponseMessage AdmRequiredDocumentsProgramEdit(JObject jsonobject)
        {
            AdmRequiredDocumentsProgram modelobj = new AdmRequiredDocumentsProgram();
            dynamic jsonData = jsonobject;
            try
            {
                modelobj.Id = Convert.ToInt32(jsonData.Id);
                modelobj.ProgramId = jsonData.ProgramId;
                modelobj.DocumentId = jsonData.DocumentId;
                modelobj.ProgrammeInstancePartTermId = jsonData.ProgrammeInstancePartTermId;
                modelobj.DocumentType = jsonData.DocumentType;
                modelobj.IsCompulsoryDocument = jsonData.IsCompulsoryDocument;

                if (modelobj.Id <= 0)
                {
                    return Return.returnHttp("201", "Valid Id Required", null);
                }
                else if (modelobj.ProgramId <= 0)
                {
                    return Return.returnHttp("201", "Valid Program Id Required", null);
                }
                else if (modelobj.DocumentId <= 0)
                {
                    return Return.returnHttp("201", "Valid Document Id Required", null);
                }
                else if (modelobj.ProgrammeInstancePartTermId <= 0)
                {
                    return Return.returnHttp("201", "Valid ProgrammeInstancePartTerm Id Required", null);
                }
                else if (jsonData.DocumentType == null)
                {
                    return Return.returnHttp("201", "Valid Document Type Required", null);
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
                    modelobj.ModifiedBy = Convert.ToInt32(responseToken);


                    SqlCommand cmd = new SqlCommand("AdmRequiredDocumentsProgramEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Flag", "AdmRequiredDocumentsProgramEdit");
                    cmd.Parameters.AddWithValue("@Id", modelobj.Id);
                    cmd.Parameters.AddWithValue("@ProgramId", modelobj.ProgramId);
                    cmd.Parameters.AddWithValue("@DocumentId", modelobj.DocumentId);
                    cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", modelobj.@ProgrammeInstancePartTermId);
                    cmd.Parameters.AddWithValue("@DocumentType", modelobj.DocumentType);
                    cmd.Parameters.AddWithValue("@IsCompulsoryDocument", modelobj.IsCompulsoryDocument);
                    cmd.Parameters.AddWithValue("@ModifiedBy", modelobj.ModifiedBy);

                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    con.Close();

                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Your data has been Edit and saved successfully.";
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

        #region AdmRequiredDocumentsProgramDelete
        [HttpPost]
        public HttpResponseMessage AdmRequiredDocumentsProgramDelete(JObject jsonobject)
        {
            AdmRequiredDocumentsProgram modelobj = new AdmRequiredDocumentsProgram();
            dynamic jsonData = jsonobject;
            try
            {
                modelobj.Id = Convert.ToInt32(jsonData.Id);
                if (modelobj.Id <= 0)
                {
                    return Return.returnHttp("201", " Valid Id Required", null);
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

                    SqlCommand cmd = new SqlCommand("AdmRequiredDocumentsProgramDelete", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Flag", "AdmRequiredDocumentsProgramDelete");
                    cmd.Parameters.AddWithValue("@Id", modelobj.Id.ToString());

                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    con.Close();

                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Your data has been Deleted successfully.";
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