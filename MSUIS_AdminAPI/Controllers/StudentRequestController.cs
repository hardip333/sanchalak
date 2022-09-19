using MSUIS_TokenManager.App_Start;
using MSUISApi.BAL;
using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class StudentRequestController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region MstProgrammeListGet
        [HttpPost]
        public HttpResponseMessage MstProgrammeListGet(ProgramInstance ObjProg)
        {
            try
            {
                int InstituteId = Convert.ToInt32(ObjProg.InstituteId);
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter cmdda = new SqlDataAdapter("MstProgrammeGetByFacultyId", con);

                cmdda.SelectCommand.CommandType = CommandType.StoredProcedure;
                cmdda.SelectCommand.Parameters.AddWithValue("@FacultyId", InstituteId);

                DataTable Dt = new DataTable();
                cmdda.Fill(Dt);


                List<MstProgramme> ObjLstMstProgramme = new List<MstProgramme>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgramme ObjMstProgramme = new MstProgramme();

                        ObjMstProgramme.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        ObjMstProgramme.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        ObjMstProgramme.ProgrammeCode = (Convert.ToString(Dt.Rows[i]["ProgrammeCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeCode"]);
                        ObjMstProgramme.ProgrammeDescription = ((Convert.ToString(Dt.Rows[i]["ProgrammeDescription"])).IsEmpty() ? "Not Defined" : (Convert.ToString(Dt.Rows[i]["ProgrammeDescription"])));
                        //ObjMstProgramme.FacultyId = (((Dt.Rows[i]["FacultyId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        // ObjMstProgramme.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        ObjMstProgramme.ProgrammeLevelId = (Convert.ToString(Dt.Rows[i]["ProgrammeLevelId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeLevelId"]);
                        ObjMstProgramme.ProgrammeLevelName = (Convert.ToString(Dt.Rows[i]["ProgrammeLevelName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeLevelName"]);
                        ObjMstProgramme.ProgrammeTypeId = (Convert.ToString(Dt.Rows[i]["ProgrammeTypeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeTypeId"]);
                        ObjMstProgramme.ProgrammeTypeName = (Convert.ToString(Dt.Rows[i]["ProgrammeTypeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeTypeName"]);
                        ObjMstProgramme.ProgrammeModeId = (Convert.ToString(Dt.Rows[i]["ProgrammeModeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeModeId"]);
                        ObjMstProgramme.ProgrammeModeName = (Convert.ToString(Dt.Rows[i]["ProgrammeModeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeModeName"]);
                        ObjMstProgramme.EvaluationId = (Convert.ToString(Dt.Rows[i]["EvaluationId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["EvaluationId"]);
                        ObjMstProgramme.EvaluationName = (Convert.ToString(Dt.Rows[i]["EvaluationName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["EvaluationName"]);
                        ObjMstProgramme.InstructionMediumId = (Convert.ToString(Dt.Rows[i]["InstructionMediumId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["InstructionMediumId"]);
                        ObjMstProgramme.InstructionMediumName = (Convert.ToString(Dt.Rows[i]["InstructionMediumName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstructionMediumName"]);
                        ObjMstProgramme.IsCBCS = (Convert.ToString(Dt.Rows[i]["IsCBCS"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsCBCS"]);
                        ObjMstProgramme.IsSepartePassingHead = (Convert.ToString(Dt.Rows[i]["IsSepartePassingHead"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsSepartePassingHead"]);
                        ObjMstProgramme.MaxMarks = (Convert.ToString(Dt.Rows[i]["MaxMarks"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["MaxMarks"]);
                        ObjMstProgramme.MinMarks = (Convert.ToString(Dt.Rows[i]["MinMarks"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["MinMarks"]);
                        ObjMstProgramme.MaxCredits = (Convert.ToString(Dt.Rows[i]["MaxCredits"])).IsEmpty() ? 0 : Convert.ToDecimal(Dt.Rows[i]["MaxCredits"]);
                        ObjMstProgramme.MinCredits = (Convert.ToString(Dt.Rows[i]["MinCredits"])).IsEmpty() ? 0 : Convert.ToDecimal(Dt.Rows[i]["MinCredits"]);
                        ObjMstProgramme.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        ObjMstProgramme.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjMstProgramme.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);

                        ObjLstMstProgramme.Add(ObjMstProgramme);
                    }

                }

                return Return.returnHttp("200", ObjLstMstProgramme, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region Student Request Get By ProgrammeId
        [HttpPost]
        public HttpResponseMessage StudentRequestGetByProgrammeId(StudentRequestList ObjStudentRequest)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter cmdda = new SqlDataAdapter("StudentRequestGetByProgrammeId", con);

                cmdda.SelectCommand.CommandType = CommandType.StoredProcedure;
                cmdda.SelectCommand.Parameters.AddWithValue("@ProgrammeId", ObjStudentRequest.ProgrammeId);

                DataTable Dt = new DataTable();
                cmdda.Fill(Dt);
                Int32 count = 0;

                BALCommon common = new BALCommon();

                string OldBaseUrlForPhoto = "https://admission.msubaroda.ac.in/MSUISApi/Upload/Photo/";
                //string NewBaseUrlForPhoto = "https://localhost:44374/Upload/Photo/";
                string NewBaseUrlForPhoto = "https://msuis.msubaroda.ac.in/Vidhyarthi_API/Upload/Photo/";

                string OldBaseUrlForDoc = "https://admission.msubaroda.ac.in/MSUISApi/Upload/Documents/";
                //string NewBaseUrlForDoc = "https://localhost:44374/Upload/Documents/";
                string NewBaseUrlForDoc = "https://msuis.msubaroda.ac.in/Vidhyarthi_API/Upload/Documents/";

                string sPath = "";
                sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/Photo/");

                string sPathDoc = "";
                sPathDoc = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/Documents/");

                List<StudentRequestList> ObjListStudentRequest = new List<StudentRequestList>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        StudentRequestList ObjStudentReq = new StudentRequestList();

                        ObjStudentReq.PRN = (Convert.ToString(Dt.Rows[i]["PRN"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["PRN"]);
                        ObjStudentReq.RequestId = Convert.ToInt32(Dt.Rows[i]["RequestId"]);
                        ObjStudentReq.ExistingRecord = (Convert.ToString(Dt.Rows[i]["ExistingRecord"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ExistingRecord"]);
                        ObjStudentReq.ChangeRecord = (Convert.ToString(Dt.Rows[i]["ChangeRecord"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ChangeRecord"]);
                        ObjStudentReq.ReasonOfRequest = (Convert.ToString(Dt.Rows[i]["ReasonOfRequest"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ReasonOfRequest"]);
                        ObjStudentReq.Request = (Convert.ToString(Dt.Rows[i]["Request"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["Request"]);
                        if(ObjStudentReq.Request == "Photo")
                        {
                            string FullPath = OldBaseUrlForPhoto + ObjStudentReq.ExistingRecord;
                            bool FileResponse = common.ServerFileExists(FullPath);

                            if (File.Exists(sPath + Path.GetFileName(ObjStudentReq.ExistingRecord)))
                            {
                                ObjStudentReq.ExistingRecord = NewBaseUrlForPhoto + ObjStudentReq.ExistingRecord;
                            }
                            else if (FileResponse == true)
                            {
                                ObjStudentReq.ExistingRecord = FullPath;
                            }
                            else
                            {
                                ObjStudentReq.ExistingRecord = NewBaseUrlForPhoto + "DefaultPhoto.png";
                            }

                            /*if (File.Exists(sPath + Path.GetFileName(ObjStudentReq.ChangeRecord)))
                            {
                                ObjStudentReq.ChangeRecord = NewBaseUrlForPhoto + ObjStudentReq.ChangeRecord;
                            }
                            else
                            {
                                ObjStudentReq.ChangeRecord = NewBaseUrlForPhoto + "DefaultPhoto.png";
                            }*/
                        }

                        if (ObjStudentReq.Request == "Upload Document (Photo Id)")
                        {
                            string FullPathDoc = OldBaseUrlForDoc + ObjStudentReq.ExistingRecord;
                            bool FileResponseDoc = common.ServerFileExists(FullPathDoc);

                            if (File.Exists(sPathDoc + Path.GetFileName(ObjStudentReq.ExistingRecord)))
                            {
                                ObjStudentReq.ExistingRecord = NewBaseUrlForDoc + ObjStudentReq.ExistingRecord;
                            }
                            else if (FileResponseDoc == true)
                            {
                                ObjStudentReq.ExistingRecord = FullPathDoc;
                            }
                            else
                            {
                                ObjStudentReq.ExistingRecord = "No File Found";
                            }

                            /*if (File.Exists(sPathDoc + Path.GetFileName(ObjStudentReq.ChangeRecord)))
                            {
                                ObjStudentReq.ChangeRecord = NewBaseUrlForDoc + ObjStudentReq.ChangeRecord;
                            }
                            else
                            {
                                ObjStudentReq.ChangeRecord = "No File Found";
                            }*/

                        }
                        ObjStudentReq.StudentName = (Convert.ToString(Dt.Rows[i]["StudentName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["StudentName"]);
                        ObjStudentReq.AttachDocument = (Convert.ToString(Dt.Rows[i]["AttachDocument"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["AttachDocument"]);
                        ObjStudentReq.Status = (Convert.ToString(Dt.Rows[i]["Status"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["Status"]);
                        ObjStudentReq.RemarksByAcademic = (Convert.ToString(Dt.Rows[i]["RemarksByAcademic"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["RemarksByAcademic"]);
                        ObjStudentReq.RemarksByFaculty = (Convert.ToString(Dt.Rows[i]["RemarksByFaculty"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["RemarksByFaculty"]);
                        if (Dt.Rows[i]["RequestedOn"].ToString() != null && Dt.Rows[i]["RequestedOn"].ToString() != "")
                        {
                            ObjStudentReq.RequestedOnView = (Convert.ToString(Dt.Rows[i]["RequestedOn"])).IsEmpty() ? "": Convert.ToDateTime(Dt.Rows[i]["RequestedOn"]).ToShortDateString();
                        }
                        if (Dt.Rows[i]["ModifiedOn"].ToString() != null && Dt.Rows[i]["ModifiedOn"].ToString() != "")
                        {
                            ObjStudentReq.ModifiedOnView = Convert.ToDateTime(Dt.Rows[i]["ModifiedOn"]).ToShortDateString();
                        }
                        count = count + 1;
                        ObjStudentReq.IndexId = count;
                        ObjListStudentRequest.Add(ObjStudentReq);
                    }

                }

                return Return.returnHttp("200", ObjListStudentRequest, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region Student Request Handle Approve
        [HttpPost]
        public HttpResponseMessage StudentRequestHandleApprove(StudentRequestList ObjStudentRequest)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String typePrefix = Request.Headers.GetValues("typePrefix").FirstOrDefault();
                TokenOperation ac = new TokenOperation();

                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("StudentRequestHandleApprove", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (typePrefix == "INS")
                {
                    ObjStudentRequest.Status = "Approved By Faculty";
                    ObjStudentRequest.RemarksByAcademic = null;
                    if (ObjStudentRequest.RemarksByFaculty == "")
                    {
                        ObjStudentRequest.RemarksByFaculty = null;
                    }
                }
                if (typePrefix == "SEC")
                {
                    ObjStudentRequest.Status = "Approved By Academic";
                    ObjStudentRequest.RemarksByFaculty = null;
                    if (ObjStudentRequest.RemarksByAcademic == "")
                    {
                        ObjStudentRequest.RemarksByAcademic = null;
                    }
                }
                

                cmd.Parameters.AddWithValue("@PRN", ObjStudentRequest.PRN);
                cmd.Parameters.AddWithValue("@RequestId", ObjStudentRequest.RequestId);
                cmd.Parameters.AddWithValue("@ChangeRecord", ObjStudentRequest.ChangeRecord);
                cmd.Parameters.AddWithValue("@Status", ObjStudentRequest.Status);
                cmd.Parameters.AddWithValue("@Request", ObjStudentRequest.Request);
                cmd.Parameters.AddWithValue("@RemarksByFaculty", ObjStudentRequest.RemarksByFaculty);
                cmd.Parameters.AddWithValue("@RemarksByAcademic", ObjStudentRequest.RemarksByAcademic);

                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);

                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();

                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);

                con.Close();
                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your Request Approved Successfully";
                }

                return Return.returnHttp("200", strMessage.ToString(), null);
            }

            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
            
        }
        #endregion

        #region Student Request Handle Reject
        [HttpPost]
        public HttpResponseMessage StudentRequestHandleReject(StudentRequestList ObjStudentRequest)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String typePrefix = Request.Headers.GetValues("typePrefix").FirstOrDefault();
                TokenOperation ac = new TokenOperation();

                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("StudentRequestHandleReject", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (typePrefix == "INS")
                {
                    ObjStudentRequest.Status = "Rejected By Faculty";
                    ObjStudentRequest.RemarksByAcademic = null;
                    if (ObjStudentRequest.RemarksByFaculty == "")
                    {
                        ObjStudentRequest.RemarksByFaculty = null;
                    }
                }
                if (typePrefix == "SEC")
                {
                    ObjStudentRequest.Status = "Rejected By Academic";
                    ObjStudentRequest.RemarksByFaculty = null;
                    if (ObjStudentRequest.RemarksByAcademic == "")
                    {
                        ObjStudentRequest.RemarksByAcademic = null;
                    }
                }

                cmd.Parameters.AddWithValue("@PRN", ObjStudentRequest.PRN);
                cmd.Parameters.AddWithValue("@RequestId", ObjStudentRequest.RequestId);                
                cmd.Parameters.AddWithValue("@Request", ObjStudentRequest.Request);                
                cmd.Parameters.AddWithValue("@Status", ObjStudentRequest.Status);
                cmd.Parameters.AddWithValue("@RemarksByFaculty", ObjStudentRequest.RemarksByFaculty);
                cmd.Parameters.AddWithValue("@RemarksByAcademic", ObjStudentRequest.RemarksByAcademic);

                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);

                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();

                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);

                con.Close();
                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your Request is Rejected.";
                }

                return Return.returnHttp("200", strMessage.ToString(), null);
            }

            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion


    }
}

