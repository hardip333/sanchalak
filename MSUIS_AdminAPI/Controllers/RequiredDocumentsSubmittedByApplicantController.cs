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
    public class RequiredDocumentsSubmittedByApplicantController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

        #region Faculty Get By Id
        [HttpPost]
        public HttpResponseMessage MstFacultyGetbyId(MstFaculty Faculty)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
            String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
            Request.Headers.TryGetValues("InstituteId", out IEnumerable<string> InstituteId1);
            var InstituteId = InstituteId1?.FirstOrDefault();

            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            string resRoleToken = ac.ValidateRoleToken(userRoleToken);



            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            else if (resRoleToken == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            try
            {

                SqlCommand cmd = new SqlCommand("MstInstituteGetTest", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (InstituteId != null || InstituteId != "0" || InstituteId != "")
                {
                    cmd.Parameters.AddWithValue("@Id", InstituteId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Id", facultyDepartIntituteId);
                }

                List<MstFaculty> ObjLstFaculty = new List<MstFaculty>();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        MstFaculty objFaculty = new MstFaculty();

                        objFaculty.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        objFaculty.FacultyName = Convert.ToString(dt.Rows[i]["FacultyName"]);
                        objFaculty.FacultyCode = Convert.ToString(dt.Rows[i]["FacultyCode"]);
                        objFaculty.InstituteId = Convert.ToInt32(dt.Rows[i]["InstituteId"]);
                        objFaculty.InstituteName = Convert.ToString(dt.Rows[i]["InstituteName"]);
                      

                        ObjLstFaculty.Add(objFaculty);
                    }
                }
                return Return.returnHttp("200", ObjLstFaculty, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion
       
        #region AcademicYearId
        [HttpPost]
        public HttpResponseMessage IncAcademicYearGet()
        {
            try
            {

                SqlCommand cmd = new SqlCommand();


                SqlDataAdapter da = new SqlDataAdapter("IncAcademicYearGet", con);

                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<AcademicYear> ObjAYList = new List<AcademicYear>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AcademicYear ObjAY = new AcademicYear();
                        ObjAY.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjAY.AcademicYearCode = Convert.ToString(dt.Rows[i]["AcademicYearCode"]);
                        ObjAYList.Add(ObjAY);
                    }
                    return Return.returnHttp("200", ObjAYList, null);
                }
                else
                {
                    return Return.returnHttp("201", "No Record Found", null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region ProgrammeList Get By FacultyId AcademicYearId
        [HttpPost]
        public HttpResponseMessage ProgrammeListGetByInstituteAcademicId(RequiredDocumentsSubmittedByApplicant ObjReqDocSubmittedList)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            try
            {


                SqlCommand cmd = new SqlCommand("MstProgrammeGetByFacIdAcadIdForRequiredDocList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InstituteId", ObjReqDocSubmittedList.InstituteId);
                cmd.Parameters.AddWithValue("@AcademicYearId", ObjReqDocSubmittedList.AcademicYearId);
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<RequiredDocumentsSubmittedByApplicant> ObjListReqDocSubmitted = new List<RequiredDocumentsSubmittedByApplicant>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        RequiredDocumentsSubmittedByApplicant objReqDocSubmitted = new RequiredDocumentsSubmittedByApplicant();

                        objReqDocSubmitted.Id = Convert.ToInt32(dr["Id"].ToString());
                        objReqDocSubmitted.ProgrammeName = dr["ProgrammeName"].ToString();
                        ObjListReqDocSubmitted.Add(objReqDocSubmitted);
                    }
                }
                return Return.returnHttp("200", ObjListReqDocSubmitted, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region ProgrammeInstancePartTermGetByFacIdAndYearId
        [HttpPost]
        public HttpResponseMessage IncProgramInstancePartTermGetbyInsIdAcadIdProgId(RequiredDocumentsSubmittedByApplicant ObjReqDocSubmittedList)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("IncProgrammeInstancePartTermGetByInstituteIdAcaIdProgId", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@InstituteId", ObjReqDocSubmittedList.InstituteId);
                da.SelectCommand.Parameters.AddWithValue("@AcademicYearId", ObjReqDocSubmittedList.AcademicYearId);
                da.SelectCommand.Parameters.AddWithValue("@ProgrammeId", ObjReqDocSubmittedList.ProgrammeId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<RequiredDocumentsSubmittedByApplicant> ObjListReqDocSubmitted = new List<RequiredDocumentsSubmittedByApplicant>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        RequiredDocumentsSubmittedByApplicant objReqDocSubmitted = new RequiredDocumentsSubmittedByApplicant();
                        objReqDocSubmitted.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        objReqDocSubmitted.InstancePartTermName = Convert.ToString(dt.Rows[i]["InstancePartTermName"]);
                        ObjListReqDocSubmitted.Add(objReqDocSubmitted);

                    }
                    return Return.returnHttp("200", ObjListReqDocSubmitted, null);
                }
                else
                {
                    return Return.returnHttp("201", "No Record Found", null);
                }

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region RequiredSubmittedDocumentsGetByFac Acad Prog ProgInstPartTerm Id
        [HttpPost]
        public HttpResponseMessage RequiredSubmittedDocumentsGetByFAPPTd(RequiredDocumentsSubmittedByApplicant ObjReqDocSubmittedList)

        {
            try
            {

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                string resRoleToken = ac.ValidateRoleToken(userRoleToken);
                Int32 Count = 0;

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

              
                List<RequiredDocumentsSubmittedByApplicant> ObjListReqDocSubmitted = new List<RequiredDocumentsSubmittedByApplicant>();

                SqlCommand cmd = new SqlCommand("RequiredSubmittedDocumentsListGetByIAPPId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AcademicYearId", ObjReqDocSubmittedList.AcademicYearId);
                cmd.Parameters.AddWithValue("@InstituteId", ObjReqDocSubmittedList.InstituteId);
                cmd.Parameters.AddWithValue("@ProgrammeId", ObjReqDocSubmittedList.ProgrammeId);
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ObjReqDocSubmittedList.ProgrammeInstancePartTermId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        RequiredDocumentsSubmittedByApplicant objReqDocSubmittedList = new RequiredDocumentsSubmittedByApplicant();
                        objReqDocSubmittedList.ApplicationFormNo = Convert.ToInt64(dt.Rows[i]["ApplicationFormNo"]);           
                        objReqDocSubmittedList.ApplicantUserName = Convert.ToInt64(dt.Rows[i]["PRN"]);           
                        objReqDocSubmittedList.NameAsPerMarksheet = (dt.Rows[i]["NameAsPerMarksheet"].ToString());
                        objReqDocSubmittedList.MobileNo = (dt.Rows[i]["MobileNo"].ToString());
                        objReqDocSubmittedList.EmailId = (dt.Rows[i]["EmailId"].ToString());
                        objReqDocSubmittedList.InstancePartTermName = (dt.Rows[i]["InstancePartTermName"].ToString());
                        objReqDocSubmittedList.ProgrammeName = (dt.Rows[i]["ProgrammeName"].ToString());                   
                        Count = Count + 1;
                        objReqDocSubmittedList.IndexId = Count;
                        ObjListReqDocSubmitted.Add(objReqDocSubmittedList);


                    }
                    return Return.returnHttp("200", ObjListReqDocSubmitted, null);
                }
                else
                {
                    return Return.returnHttp("201", "No Record Found", null);
                }

            }


            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion
   
        #region RequiredSubmittedDocumentsByApplicationId
        [HttpPost]
        public HttpResponseMessage RequiredSubmittedDocumentGetByAP(RequiredDocumentsSubmittedByApplicant ObjReqSubmittedDoc)

        {
            try
            {

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                string resRoleToken = ac.ValidateRoleToken(userRoleToken);
                Int32 Count = 0;
                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                List<RequiredDocumentsSubmittedByApplicant> ObjLstReqSubmittedDocu = new List<RequiredDocumentsSubmittedByApplicant>();

                SqlCommand cmd = new SqlCommand("RequiredDocumentSubmittedListGetByAppProgId", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeId", ObjReqSubmittedDoc.ProgrammeId);
                cmd.Parameters.AddWithValue("@ApplicationId", ObjReqSubmittedDoc.ApplicationFormNo);
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        RequiredDocumentsSubmittedByApplicant objRDS = new RequiredDocumentsSubmittedByApplicant();

                     
                        objRDS.NameOfTheDocument = Convert.ToString(dt.Rows[i]["NameOfTheDocument"]);
                        Count = Count + 1;
                        objRDS.IndexId = Count;
                        ObjLstReqSubmittedDocu.Add(objRDS);


                    }
                    return Return.returnHttp("200", ObjLstReqSubmittedDocu, null);
                }
                else
                {
                    return Return.returnHttp("201", "No Record Found", null);
                }

            }


            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region RequiredSubmittedDocumentForExcel Get By Fac Acad Prog ProgInstPartTerm Id
        [HttpPost]
        public HttpResponseMessage RequiredSubmittedDocumentListExcelGetByAIPP(RequiredDocumentsSubmittedByApplicant ObjReqSubmittedDoc)

        {
            try
            {

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                string resRoleToken = ac.ValidateRoleToken(userRoleToken);
                Int32 Count = 0;

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                
                List<RequiredDocumentsSubmittedByApplicant> ObjLstReqSubmittedDoc = new List<RequiredDocumentsSubmittedByApplicant>();

                SqlCommand cmd = new SqlCommand("RequiredSubmittedDocumentListExcelGetByAIPP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AcademicYearId", ObjReqSubmittedDoc.AcademicYearId);
                cmd.Parameters.AddWithValue("@InstituteId", ObjReqSubmittedDoc.InstituteId);
                cmd.Parameters.AddWithValue("@ProgrammeId", ObjReqSubmittedDoc.ProgrammeId);
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ObjReqSubmittedDoc.ProgrammeInstancePartTermId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        RequiredDocumentsSubmittedByApplicant objRSD = new RequiredDocumentsSubmittedByApplicant();
                        objRSD.ApplicationFormNum = (dt.Rows[i]["ApplicationFormNo"].ToString());
                        objRSD.UserName = (dt.Rows[i]["PRN"].ToString());
                        //objRPD.AplicantName = (dt.Rows[i]["FirstName"].ToString()) + " " + (dt.Rows[i]["LastName"].ToString());
                        objRSD.NameAsPerMarksheet = (dt.Rows[i]["NameAsPerMarksheet"].ToString());
                        objRSD.MobileNo = (dt.Rows[i]["MobileNo"].ToString());
                        objRSD.EmailId = (dt.Rows[i]["EmailId"].ToString());
                        objRSD.BranchName = (dt.Rows[i]["BranchName"].ToString());
                        objRSD.ProgrammeName = (dt.Rows[i]["ProgrammeName"].ToString());
                        objRSD.InstancePartTermName = (dt.Rows[i]["InstancePartTermName"].ToString());
                        objRSD.NameOfTheDocument = (dt.Rows[i]["NameOfTheDocument"].ToString());
                        Count = Count + 1;
                        objRSD.IndexId = Count;
                        ObjLstReqSubmittedDoc.Add(objRSD);




                    }
                    return Return.returnHttp("200", ObjLstReqSubmittedDoc, null);
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
    }
}
