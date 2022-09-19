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
    public class RequiredDocumentPendingListController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();
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

        #region RequiredPendingDocumentForExcel
        [HttpPost]
        public HttpResponseMessage RequiredPendingDocumentForExcel(RequiredDocumentPendingList ObjReqPendingDoc)

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

              
                List<RequiredDocumentPendingList> ObjLstReqPendingDocu = new List<RequiredDocumentPendingList>();

                SqlCommand cmd = new SqlCommand("RequiredDocumentPendingListExcel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AcademicYearId", ObjReqPendingDoc.AcademicYearId);
                cmd.Parameters.AddWithValue("@InstituteId", ObjReqPendingDoc.InstituteId);
                cmd.Parameters.AddWithValue("@ProgrammeId", ObjReqPendingDoc.ProgrammeId);
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ObjReqPendingDoc.ProgrammeInstancePartTermId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        RequiredDocumentPendingList objRPD = new RequiredDocumentPendingList();
                        objRPD.ApplicationFormNum = (dt.Rows[i]["ApplicationFormNo"].ToString());
                        objRPD.UserName = (dt.Rows[i]["PRN"].ToString());
                        objRPD.NameAsPerMarksheet = (dt.Rows[i]["NameAsPerMarksheet"].ToString());                    
                        objRPD.MobileNo = (dt.Rows[i]["MobileNo"].ToString());
                        objRPD.EmailId = (dt.Rows[i]["EmailId"].ToString());
                        objRPD.ProgrammeName = (dt.Rows[i]["ProgrammeName"].ToString());
                        objRPD.BranchName = (dt.Rows[i]["BranchName"].ToString());
                        objRPD.InstancePartTermName = (dt.Rows[i]["InstancePartTermName"].ToString());
                        objRPD.NameOfTheDocument = (dt.Rows[i]["NameOfTheDocument"].ToString());
                        Count = Count + 1;
                        objRPD.IndexId = Count;
                        ObjLstReqPendingDocu.Add(objRPD);




                    }
                    return Return.returnHttp("200", ObjLstReqPendingDocu, null);
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

        #region  Required Pending Documents Get By Ins Acad Prog ProgInstPartTerm Id
        [HttpPost]
        public HttpResponseMessage RequiredPendingDocumentGetByIAPP(RequiredDocumentPendingList ObjReqPendingDoc)

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


               
                List<RequiredDocumentPendingList> ObjLstReqPendingDocu = new List<RequiredDocumentPendingList>();

                SqlCommand cmd = new SqlCommand("RequiredDocumentPendingListGetByAcadId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AcademicYearId", ObjReqPendingDoc.AcademicYearId);
                cmd.Parameters.AddWithValue("@InstituteId", ObjReqPendingDoc.InstituteId);
                cmd.Parameters.AddWithValue("@ProgrammeId", ObjReqPendingDoc.ProgrammeId);
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ObjReqPendingDoc.ProgrammeInstancePartTermId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        RequiredDocumentPendingList objRPD = new RequiredDocumentPendingList();
                        objRPD.ApplicationFormNo = Convert.ToInt64(dt.Rows[i]["ApplicationFormNo"].ToString());
                        objRPD.ApplicantUserName = Convert.ToInt64(dt.Rows[i]["PRN"]);
                        objRPD.NameAsPerMarksheet = (dt.Rows[i]["NameAsPerMarksheet"].ToString());
                        objRPD.MobileNo = (dt.Rows[i]["MobileNo"].ToString());
                        objRPD.EmailId = (dt.Rows[i]["EmailId"].ToString());
                        objRPD.InstancePartTermName = (dt.Rows[i]["InstancePartTermName"].ToString());
                        objRPD.ProgrammeName = (dt.Rows[i]["ProgrammeName"].ToString());
                        Count = Count + 1;
                        objRPD.IndexId = Count;

                        ObjLstReqPendingDocu.Add(objRPD);
                    }
                    return Return.returnHttp("200", ObjLstReqPendingDocu, null);
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

        #region RequiredPendingDocumentsByApplicationId
        [HttpPost]
        public HttpResponseMessage RequiredPendingDocumentAPP(RequiredDocumentPendingList ObjReqPendingDoc)

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

                         
                List<RequiredDocumentPendingList> ObjLstReqPendingDocu = new List<RequiredDocumentPendingList>();

                SqlCommand cmd = new SqlCommand("RequiredDocumentPendingListGetByAdmApplicationId", con);
                cmd.CommandType = CommandType.StoredProcedure;
              
                cmd.Parameters.AddWithValue("@ProgrammeId", ObjReqPendingDoc.ProgrammeId);
                cmd.Parameters.AddWithValue("@AdmApplicationId", ObjReqPendingDoc.ApplicationFormNo);
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        RequiredDocumentPendingList objRDP = new RequiredDocumentPendingList();


                        objRDP.NameOfTheDocument = Convert.ToString(dt.Rows[i]["NameOfTheDocument"]);
                        //objRPD.NameOfTheDocument += "  " + (k + 1) + ")" + Convert.ToString(Dt1.Rows[k]["NameOfTheDocument"]) + "</br>";
                        Count = Count + 1;
                        objRDP.IndexId = Count;


                        ObjLstReqPendingDocu.Add(objRDP);
                       
                       
                    }
                    return Return.returnHttp("200", ObjLstReqPendingDocu, null);
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

    }
}
