using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class DualAdmissionByAcademicController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

        #region DualAdmissionSearchByPRN
        [HttpPost]
        public HttpResponseMessage DualAdmissionSearchByPRN(DualAdmission DA)
        {
            DualAdmission modelobj = new DualAdmission();

            try
                {
                modelobj.PRN = DA.PRN;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("DualAdmissionSearchByPRN", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PRN", modelobj.PRN);
                //cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                //cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<DualAdmission> ObjDualAdm = new List<DualAdmission>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        DualAdmission ObjDA = new DualAdmission();
                        ObjDA.ApplicationId = Convert.ToInt64(dr["ApplicationId"]);
                        ObjDA.MobileNo = Convert.ToString(dr["MobileNo"]);
                        ObjDA.ProgrammeInstancePartTermId = Convert.ToInt64(dr["ProgrammeInstancePartTermId"]);
                        ObjDA.InstancePartTermName = Convert.ToString(dr["InstancePartTermName"]);
                        ObjDA.FullName = Convert.ToString(dr["LastName"]) + " " + Convert.ToString(dr["FirstName"]) + " " + Convert.ToString(dr["MiddleName"]);
                        ObjDA.FacultyName = Convert.ToString(dr["FacultyName"]);
                        ObjDA.Message1 = Convert.ToString(dr["Message1"]);
                        count = count + 1;
                        ObjDA.IndexId = count;

                        ObjDualAdm.Add(ObjDA);
                    }

                }
                else
                {
                    //string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    
                     return Return.returnHttp("201", "Not Admitted Yet.", null);
                    
                }

                return Return.returnHttp("200", ObjDualAdm, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region GetProgrammeListforDualAdm
        [HttpPost]
        public HttpResponseMessage GetProgrammeListforDualAdm(DualProgrammeList ObjDualProg)
        {
            try
            {
                DualProgrammeList modelobj = new DualProgrammeList();
                modelobj.PRN = ObjDualProg.PRN;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                modelobj.UserId = Convert.ToInt64(res);

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("DualAdmissionProgrammeList", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@PRN", modelobj.PRN);
                da.SelectCommand.Parameters.AddWithValue("@UserId", modelobj.UserId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<DualProgrammeList> ObjLstDualProg = new List<DualProgrammeList>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DualProgrammeList ObjDual = new DualProgrammeList();
                        ObjDual.Id = Convert.ToInt64(dt.Rows[i]["Id"]);
                        ObjDual.DestinationIncProgInstPartTermId = Convert.ToInt64(dt.Rows[i]["DestinationIncProgInstPartTermId"]);
                        ObjDual.InstancePartTermName = Convert.ToString(dt.Rows[i]["InstancePartTermName"]);

                        ObjLstDualProg.Add(ObjDual);

                    }
                    return Return.returnHttp("200", ObjLstDualProg, null);
                }
                else
                {
                    return Return.returnHttp("200", "No Record Found", null);
                }

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region GenericConfigurationGetById
        [HttpPost]
        public HttpResponseMessage GenericConfigurationGetById(JObject jid)
        {
            try
            {
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                //List<GenMaritalStatus> slist = new List<GenMaritalStatus>();



                GenericConfiguration generic = new GenericConfiguration();
                dynamic JsonData = jid;
                generic.Id = Convert.ToInt16(JsonData.Id);
                //generic.KeyName = Convert.ToString(JsonData.KeyName);
                SqlCommand Cmd = new SqlCommand("GenericConfigurationGetById", con);
                Cmd.CommandType = CommandType.StoredProcedure;



                Cmd.Parameters.AddWithValue("@Id", generic.Id);
                //Cmd.Parameters.AddWithValue("@KeyName", generic.KeyName);



                da.SelectCommand = Cmd;
                da.Fill(dt);



                List<GenericConfiguration> ObjLstGenConfig = new List<GenericConfiguration>();



                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        GenericConfiguration objGenConfig = new GenericConfiguration();



                        objGenConfig.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        objGenConfig.KeyName = Convert.ToString(dt.Rows[i]["KeyName"]);
                        objGenConfig.Value = Convert.ToString(dt.Rows[i]["Value"]);
                        //objGenConfig.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjLstGenConfig.Add(objGenConfig);
                    }
                }
                return Return.returnHttp("200", ObjLstGenConfig, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Dual Admission Document Upload

        [HttpPost()]
        public HttpResponseMessage UploadDualAdmissionDoc()
        {

            int iUploadedCnt = 0;

            // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
            string sPath = "";
            Boolean already_exist = false;
            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/DualAdmissionDocuments/");


            string token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation tokenOperation = new TokenOperation();
            string responseToken = tokenOperation.ValidateToken(token);
            if (responseToken == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            string abc = Request.Headers.GetValues("DualAdmissionDoc").FirstOrDefault();

            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            // CHECK THE FILE COUNT.
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];

                if (hpf.ContentLength > 0)
                {
                    /* if (abc == "EWS.pdf" || abc == "EWS.docx")
                    {
                    File.Delete(sPath + Path.GetFileName(responseToken + "_" + "EWS.pdf"));
                    File.Delete(sPath + Path.GetFileName(responseToken + "_" + "EWS.docx"));
                    }*/
                    if (File.Exists(sPath + Path.GetFileName(responseToken + "_" + abc)))
                    {
                        File.Delete(sPath + Path.GetFileName(responseToken + "_" + abc));
                    }


                    // SAVE THE FILES IN THE FOLDER.
                    hpf.SaveAs(sPath + Path.GetFileName(responseToken + "_" + abc));
                    //hpf.SaveAs(sPath + Path.GetFileName(abc));

                    iUploadedCnt = iUploadedCnt + 1;

                }
            }

            // RETURN A MESSAGE (OPTIONAL).
            if (iUploadedCnt > 0)
            {
                return Return.returnHttp("200", iUploadedCnt + " Files Uploaded Successfully", null);

            }
            else
            {
                return Return.returnHttp("201", iUploadedCnt + "Upload Failed", null);
            }

        }
        #endregion

        #region DualAdmissionUpdate
        [HttpPost]
        public HttpResponseMessage DualAdmissionUpdate(DualProgrammeUpdate DualProgrammeUpdate)

        {
          
            try
            {
                Int64 ApplicationId = Convert.ToInt64(DualProgrammeUpdate.ApplicationId);
                Int64 PRN = Convert.ToInt64(DualProgrammeUpdate.PRN);
                //DualProgrammeUpdate.ProgrammeInstancePartTermId = Convert.ToInt64(jsonData.DestinationIncProgInstPartTermId);
                string AdminRemarkByAcademics = Convert.ToString(DualProgrammeUpdate.DualAdmissionRemark);
                string DualAdmissionDoc = Convert.ToString(DualProgrammeUpdate.DualAdmissionDoc);
                //DualProgrammeUpdate.CreatedBy = Convert.ToInt64(jsonData.CreatedBy);
                
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                Int64 CreatedBy = Convert.ToInt64(responseToken);

                string sPathDualDoc = "";
                sPathDualDoc = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/DualAdmissionDocuments/");
                string abcDualDoc = null;
                if (Request.Headers.Contains("DualAdmissionDoc"))
                {
                    abcDualDoc = Request.Headers.GetValues("DualAdmissionDoc").FirstOrDefault();
                }
                
                if (!File.Exists(sPathDualDoc + Path.GetFileName(responseToken + "_" + abcDualDoc)))
                {
                    return Return.returnHttp("201", "Dual Admission Doc File Not Exists.", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(AdminRemarkByAcademics)))
                {
                    return Return.returnHttp("201", "Please Add Academic Remarks.", null);
                }
                else if (String.IsNullOrWhiteSpace(Convert.ToString(ApplicationId)))
                {
                    return Return.returnHttp("201", "Please select Application Id.", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(DualAdmissionDoc)))
                {
                    return Return.returnHttp("201", "Please Add Dual Admission Document.", null);
                }
                else {
                    SqlCommand cmd = new SqlCommand("DualAdmission", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ApplicationId", ApplicationId);
                    cmd.Parameters.AddWithValue("@PRN", PRN);
                    //cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", DualProgrammeUpdate.ProgrammeInstancePartTermId);
                    cmd.Parameters.AddWithValue("@AdminRemarkByAcademics", AdminRemarkByAcademics);
                    cmd.Parameters.AddWithValue("@DualAdmissionDoc", DualAdmissionDoc);
                    cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    con.Close();

                    return Return.returnHttp("200", strMessage, null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion

        #region TransferAdmissionUpdate
        [HttpPost]
        public HttpResponseMessage TransferAdmissionUpdate(TransferProgrammeUpdate TransferProgrammeUpdate)

        {

            try
            {
                Int64 ApplicationId = Convert.ToInt64(TransferProgrammeUpdate.ApplicationId);
                Int64 OldApplicationId = Convert.ToInt64(TransferProgrammeUpdate.OldApplicationId);
                Int64 PRN = Convert.ToInt64(TransferProgrammeUpdate.PRN);
                Int64 OldProgrammeInstancePartTermId = Convert.ToInt64(TransferProgrammeUpdate.OldProgrammeInstancePartTermId);
                string TransferAdmissionRemarksByFaculty = "";
                string TransferAdmissionRemarksByAcademic = "";


                string TransferAdmissionDoc = Convert.ToString(TransferProgrammeUpdate.DualAdmissionDoc);

                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                Int64 CreatedBy = Convert.ToInt64(responseToken);

                if(CreatedBy == 1000000024 || CreatedBy == 1000000028)
                {
                    TransferAdmissionRemarksByAcademic = Convert.ToString(TransferProgrammeUpdate.DualAdmissionRemark);
                }
                else
                {
                    TransferAdmissionRemarksByFaculty = Convert.ToString(TransferProgrammeUpdate.DualAdmissionRemark);
                }

                string sPathDualDoc = "";
                sPathDualDoc = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/DualAdmissionDocuments/");
                string abcDualDoc = null;
                if (Request.Headers.Contains("DualAdmissionDoc"))
                {
                    abcDualDoc = Request.Headers.GetValues("DualAdmissionDoc").FirstOrDefault();
                }

                if (!File.Exists(sPathDualDoc + Path.GetFileName(responseToken + "_" + abcDualDoc)))
                {
                    return Return.returnHttp("201", "Dual Admission Doc File Not Exists.", null);
                }
                else if (String.IsNullOrWhiteSpace(Convert.ToString(ApplicationId)))
                {
                    return Return.returnHttp("201", "Please select Application Id.", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(TransferAdmissionDoc)))
                {
                    return Return.returnHttp("201", "Please Add Dual Admission Document.", null);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("TransferAdmission", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ApplicationId", ApplicationId);
                    cmd.Parameters.AddWithValue("@OldApplicationId", OldApplicationId);
                    cmd.Parameters.AddWithValue("@PRN", PRN);
                    cmd.Parameters.AddWithValue("@OldProgrammeInstancePartTermId", OldProgrammeInstancePartTermId);
                    cmd.Parameters.AddWithValue("@TransferAdmissionRemarksByFaculty", TransferAdmissionRemarksByFaculty);
                    cmd.Parameters.AddWithValue("@TransferAdmissionRemarksByAcademic", TransferAdmissionRemarksByAcademic);
                    cmd.Parameters.AddWithValue("@TransferAdmissionDoc", TransferAdmissionDoc);
                    cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    con.Close();

                    return Return.returnHttp("200", strMessage, null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion

    }

}