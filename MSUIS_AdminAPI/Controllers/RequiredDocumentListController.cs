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

namespace MSUISApi.Controllers
{
    public class RequiredDocumentListController : ApiController
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

            //String InstituteId = Request.Headers.GetValues("InstituteId").FirstOrDefault();
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
                        objFaculty.FacultyCode = Convert.ToString(dt.Rows[i]["InstituteCode"]);
                       

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
       
        #region ProgrammeList Get By FacultyId
        [HttpPost]
        public HttpResponseMessage ProgrammeListGetByInstituteAcademicId(RequiredDocumentList ObjReqDocList)
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
                cmd.Parameters.AddWithValue("@InstituteId", ObjReqDocList.InstituteId);
                cmd.Parameters.AddWithValue("@AcademicYearId", ObjReqDocList.AcademicYearId);
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<RequiredDocumentList> ObjListReqDoc = new List<RequiredDocumentList>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        RequiredDocumentList objReqDoc = new RequiredDocumentList();

                        objReqDoc.Id = Convert.ToInt32(dr["Id"].ToString());
                        objReqDoc.ProgrammeName = dr["ProgrammeName"].ToString();
                        ObjListReqDoc.Add(objReqDoc);
                    }
                }
                return Return.returnHttp("200", ObjListReqDoc, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region ProgrammeInstancePartTermGetByFacIdAndYearId
        [HttpPost]
        public HttpResponseMessage IncProgramInstancePartTermGetbyInsIdAcadIdProgId(RequiredDocumentList ObjReqDocList)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("IncProgrammeInstancePartTermGetByInstituteIdAcaIdProgId", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@InstituteId", ObjReqDocList.InstituteId);
                da.SelectCommand.Parameters.AddWithValue("@AcademicYearId", ObjReqDocList.AcademicYearId);
                da.SelectCommand.Parameters.AddWithValue("@ProgrammeId", ObjReqDocList.ProgrammeId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<RequiredDocumentList> ObjLstReqDocList = new List<RequiredDocumentList>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        RequiredDocumentList objReqDocList = new RequiredDocumentList();
                        objReqDocList.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        objReqDocList.InstancePartTermName = Convert.ToString(dt.Rows[i]["InstancePartTermName"]);
                        ObjLstReqDocList.Add(objReqDocList);

                    }
                    return Return.returnHttp("200", ObjLstReqDocList, null);
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

        #region  RequiredDocument
        [HttpPost]
        public HttpResponseMessage RequiredDocumentExportExcel(RequiredDocumentList objRequired)
        {

            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
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
                Int32 Count = 0;
                   
                SqlCommand cmd = new SqlCommand("RequiredDocumentGetByProgInsPartTermId", con);
                cmd.CommandType = CommandType.StoredProcedure;           
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", objRequired.ProgrammeInstancePartTermId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<RequiredDocumentList> ObjLstReqDoc = new List<RequiredDocumentList>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        RequiredDocumentList ObjReqDoc = new RequiredDocumentList();
                        ObjReqDoc.Id = Convert.ToInt32(dr["Id"].ToString());
                        ObjReqDoc.ProgrammeName = dr["ProgrammeName"].ToString();
                        ObjReqDoc.InstancePartTermName = dr["InstancePartTermName"].ToString();
                        ObjReqDoc.NameOfTheDocument = dr["NameOfTheDocument"].ToString();
                        ObjReqDoc.DocumentType = dr["DocumentType"].ToString();
                        Count = Count + 1;
                        ObjReqDoc.IndexId = Count;


                        ObjLstReqDoc.Add(ObjReqDoc);
                    }

                }
                return Return.returnHttp("200", ObjLstReqDoc, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion
    }
}
