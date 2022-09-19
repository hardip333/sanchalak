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
    public class StudentReAdmissionReportController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();



        #region Faculty List Get By UserId
        [HttpPost]
        public HttpResponseMessage FacultyListGetByUserId(StudentReAdmissionReport SRAR)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String typePrefix = Request.Headers.GetValues("typePrefix").FirstOrDefault();
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


             
                Int64 UserId = Convert.ToInt64(res);

               
                SqlCommand Cmd = new SqlCommand("GetFacultyListByUserIdForStudentReAdmissionReport", con);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@UserId", UserId);

                sda.SelectCommand = Cmd;
                sda.Fill(dt);

                List<StudentReAdmissionReport> ObjLstSRAR = new List<StudentReAdmissionReport>();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        StudentReAdmissionReport objSRAR = new StudentReAdmissionReport();
                        objSRAR.FacultyId = (((dt.Rows[i]["FacultyId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(dt.Rows[i]["FacultyId"]);
                        //objSRAR.InstituteId = (((dt.Rows[i]["InstituteId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(dt.Rows[i]["InstituteId"]);
                        objSRAR.FacultyName = (Convert.ToString(dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dt.Rows[i]["FacultyName"]);
                        //objSRAR.InstituteName = (Convert.ToString(dt.Rows[i]["InstituteName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dt.Rows[i]["InstituteName"]);
                        

                        ObjLstSRAR.Add(objSRAR);
                    }



                }
                                     

                return Return.returnHttp("200", ObjLstSRAR, null);


            }


            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Institute List Get By Faculty Id
        [HttpPost]
        public HttpResponseMessage InstituteListGetByFacultyId(StudentReAdmissionReport SRAR)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String typePrefix = Request.Headers.GetValues("typePrefix").FirstOrDefault();
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

                SqlCommand Cmd = new SqlCommand("GetInstituteListByFacultyIdForStudentReAdmissionReport", con);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@FacultyId", SRAR.FacultyId);

                sda.SelectCommand = Cmd;
                sda.Fill(dt);

                List<StudentReAdmissionReport> ObjLstSRAR = new List<StudentReAdmissionReport>();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        StudentReAdmissionReport objSRAR = new StudentReAdmissionReport();
   
                        objSRAR.InstituteId = (((dt.Rows[i]["InstituteId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(dt.Rows[i]["InstituteId"]);
                        objSRAR.InstituteName = (Convert.ToString(dt.Rows[i]["InstituteName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dt.Rows[i]["InstituteName"]);


                        ObjLstSRAR.Add(objSRAR);
                    }



                }


                return Return.returnHttp("200", ObjLstSRAR, null);


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
        public HttpResponseMessage ProgrammeListGetByInstituteAcademicId(StudentReAdmissionReport ObjSRAR)
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


                SqlCommand cmd = new SqlCommand("MstProgrammeGetByFacIdAcadIdForStudentReAdmissionReport", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InstituteId", ObjSRAR.InstituteId);
                cmd.Parameters.AddWithValue("@AcademicYearId", ObjSRAR.AcademicYearId);
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<StudentReAdmissionReport> ObjListSRAR = new List<StudentReAdmissionReport>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        StudentReAdmissionReport objSRAR = new StudentReAdmissionReport();

                        objSRAR.Id = Convert.ToInt32(dr["Id"].ToString());
                        objSRAR.ProgrammeName = dr["ProgrammeName"].ToString();
                        ObjListSRAR.Add(objSRAR);
                    }
                }
                return Return.returnHttp("200", ObjListSRAR, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region ProgrammeInstancePartTermGetByFacIdAndYearId
        [HttpPost]
        public HttpResponseMessage IncProgramInstancePartTermGetbyInsIdAcadIdProgId(StudentReAdmissionReport ObjSRAR)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("IncProgrInstPartTermGetByInstIdAcaIdProgIdForStudentReAdmissionReport", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@InstituteId", ObjSRAR.InstituteId);
                da.SelectCommand.Parameters.AddWithValue("@AcademicYearId", ObjSRAR.AcademicYearId);
                da.SelectCommand.Parameters.AddWithValue("@ProgrammeId", ObjSRAR.ProgrammeId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<StudentReAdmissionReport> ObjListSRAR = new List<StudentReAdmissionReport>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        StudentReAdmissionReport objSRAR = new StudentReAdmissionReport();
                        objSRAR.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        objSRAR.InstancePartTermName = Convert.ToString(dt.Rows[i]["InstancePartTermName"]);
                        ObjListSRAR.Add(objSRAR);

                    }
                    return Return.returnHttp("200", ObjListSRAR, null);
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
     
        #region  Student ReAdmissionReport By ProgInstPartTerm Id
        [HttpPost]
        public HttpResponseMessage StudentReAdmissionReportGetByPIPTID(StudentReAdmissionReport ObjSRSR)

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



                List<StudentReAdmissionReport> ObjLstSRAR = new List<StudentReAdmissionReport>();

                SqlCommand cmd = new SqlCommand("StudentReAdmissionReportGetByPIPTID", con);
                cmd.CommandType = CommandType.StoredProcedure;
              
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ObjSRSR.ProgrammeInstancePartTermId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        StudentReAdmissionReport objSRAR = new StudentReAdmissionReport();
                        objSRAR.NameAsPerMarksheet = (Convert.ToString(dt.Rows[i]["NameAsPerMarksheet"])).IsEmpty() ? "Not Defined" : Convert.ToString(dt.Rows[i]["NameAsPerMarksheet"]);
                        objSRAR.IncStudentAcademicInformationId = (Convert.ToString(dt.Rows[i]["IncStudentAcademicInformationId"])).IsEmpty() ? "Not Defined" : Convert.ToString(dt.Rows[i]["IncStudentAcademicInformationId"]);
                        objSRAR.IncStudentAdmissionId = (((dt.Rows[i]["IncStudentAdmissionId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(dt.Rows[i]["IncStudentAdmissionId"]);
                        objSRAR.PRN = (((dt.Rows[i]["PRN"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(dt.Rows[i]["PRN"]);
                        objSRAR.EmailId = (Convert.ToString(dt.Rows[i]["EmailId"])).IsEmpty() ? "Not Defined" : Convert.ToString(dt.Rows[i]["EmailId"]);
                        objSRAR.ApplicationFormNum = (Convert.ToString(dt.Rows[i]["IncStudentAdmissionId"])).IsEmpty() ? "Not Defined" : Convert.ToString(dt.Rows[i]["IncStudentAdmissionId"]);
                        objSRAR.UserName = (Convert.ToString(dt.Rows[i]["PRN"])).IsEmpty() ? "Not Defined" : Convert.ToString(dt.Rows[i]["PRN"]);
                        objSRAR.MobileNo = (Convert.ToString(dt.Rows[i]["MobileNo"])).IsEmpty() ? "Not Defined" : Convert.ToString(dt.Rows[i]["MobileNo"]);
                        objSRAR.FacultyName = (Convert.ToString(dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dt.Rows[i]["FacultyName"]);
                        objSRAR.BranchName = (Convert.ToString(dt.Rows[i]["BranchName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dt.Rows[i]["BranchName"]);
                        objSRAR.ProgrammeName = (Convert.ToString(dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dt.Rows[i]["ProgrammeName"]);
                        objSRAR.InstancePartTermName = (Convert.ToString(dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dt.Rows[i]["InstancePartTermName"]);
                        objSRAR.FacultyRemark = (Convert.ToString(dt.Rows[i]["FacultyRemark"])).IsEmpty() ? "Not Defined" : Convert.ToString(dt.Rows[i]["FacultyRemark"]);
                        objSRAR.StudentHandWrittenDocument = (Convert.ToString(dt.Rows[i]["StudentHandWrittenDocument"])).IsEmpty() ? null : Convert.ToString(dt.Rows[i]["StudentHandWrittenDocument"]);
                        
                        Count = Count + 1;
                        objSRAR.IndexId = Count;

                        ObjLstSRAR.Add(objSRAR);
                    }
                    return Return.returnHttp("200", ObjLstSRAR, null);
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
