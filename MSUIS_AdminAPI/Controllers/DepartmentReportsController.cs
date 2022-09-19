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

namespace MSUISApi.Controllers
{
    public class DepartmentReportsController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        SqlDataAdapter Da = new SqlDataAdapter();
        #region MstProgrammeGetByFacInstSubId
        [HttpPost]
        public HttpResponseMessage MstProgrammeGetByFacInstSubId(MstProgramme mprog)
        {
            try
            {

                SqlCommand cmd = new SqlCommand("MstPrgGetByFacInstSubId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", mprog.FacultyId);
                cmd.Parameters.AddWithValue("@InstituteId", mprog.InstituteId);
                cmd.Parameters.AddWithValue("@SubjectId", mprog.SubjectId);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = cmd;

                Da.Fill(Dt);

                List<MstProgramme> ObjLstProg = new List<MstProgramme>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgramme mstpro = new MstProgramme();

                        mstpro.Id = (((Dt.Rows[i]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["Id"]);
                        mstpro.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        mstpro.ProgrammeCode = (Convert.ToString(Dt.Rows[i]["ProgrammeCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeCode"]);
                        
                        ObjLstProg.Add(mstpro);
                    }
                }
                return Return.returnHttp("200", ObjLstProg, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region IncProgInstPartTermGetByFacAcProgId
        [HttpPost]
        public HttpResponseMessage IncProgInstPartTermGetByFacAcProgId(ProgrammeInstancePartTerm ObjProgInstPartTerm)
        {
            try
            {
                Request.Headers.TryGetValues("DepartmentId", out IEnumerable<string> DepartmentId1);
                var DepartmentId = DepartmentId1?.FirstOrDefault();
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("IncPrgInstPartTermGetByFacPrgId", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (DepartmentId != null || DepartmentId != "" || DepartmentId != "0")
                {
                    da.SelectCommand.Parameters.AddWithValue("@FacultyId", ObjProgInstPartTerm.FacultyId);
                    da.SelectCommand.Parameters.AddWithValue("@AcademicYearId", ObjProgInstPartTerm.AcademicYearId);
                    da.SelectCommand.Parameters.AddWithValue("@ProgrammeId", ObjProgInstPartTerm.ProgrammeId);
                    da.SelectCommand.Parameters.AddWithValue("@DepartmentId", DepartmentId);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@FacultyId", ObjProgInstPartTerm.FacultyId);
                    da.SelectCommand.Parameters.AddWithValue("@AcademicYearId", ObjProgInstPartTerm.AcademicYearId);
                    da.SelectCommand.Parameters.AddWithValue("@ProgrammeId", ObjProgInstPartTerm.ProgrammeId);
                }
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<ProgrammeInstancePartTerm> ObjLstProgInstPartTerm = new List<ProgrammeInstancePartTerm>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePartTerm ObjProgPartTerm = new ProgrammeInstancePartTerm();
                        ObjProgPartTerm.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjProgPartTerm.InstancePartTermName = Convert.ToString(dt.Rows[i]["InstancePartTermName"]);
                        
                        ObjLstProgInstPartTerm.Add(ObjProgPartTerm);

                    }

                    return Return.returnHttp("200", ObjLstProgInstPartTerm, null);
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
            String facultyDepartIntituteId = Request.Headers.GetValues("FacultyId").FirstOrDefault();
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

            try
            {
                //dynamic Jsondata = Faculty;

                // Int32 Id = Convert.ToInt32(Faculty.Id);


                SqlCommand cmd = new SqlCommand("MstFacultyGetbyId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", facultyDepartIntituteId);

                List<MstFaculty> ObjLstFaculty = new List<MstFaculty>();
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstFaculty objFaculty = new MstFaculty();

                        objFaculty.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objFaculty.FacultyName = Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objFaculty.FacultyCode = Convert.ToString(Dt.Rows[i]["FacultyCode"]);
                       
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

        #region Department Student List

        [HttpPost]
        public HttpResponseMessage DepartmentStudentList(DepartmentStudentList objDepartmentStudent)
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

                SqlCommand cmd = new SqlCommand("DepartmentStudentList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AcademicYearId", objDepartmentStudent.AcademicYearId);
                cmd.Parameters.AddWithValue("@FacultyId", objDepartmentStudent.FacultyId);
                cmd.Parameters.AddWithValue("@InstituteId", objDepartmentStudent.InstituteId);
                cmd.Parameters.AddWithValue("@InstancePartTermId", objDepartmentStudent.InstancePartTermId);
                cmd.Parameters.AddWithValue("@ProgrammeId", objDepartmentStudent.ProgrammeId);
                //StudentData
                cmd.Parameters.AddWithValue("@ReportType", objDepartmentStudent.ReportType);
                sda.SelectCommand = cmd;
                sda.Fill(Dt);
                List<DepartmentStudentList> objDepartmentStudentList = new List<DepartmentStudentList>();
                objDepartmentStudent.SerializeNumber = 0;
                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        objDepartmentStudent.SerializeNumber = objDepartmentStudent.SerializeNumber + 1;
                        DepartmentStudentList ObjDSL = new DepartmentStudentList();
                        ObjDSL.SerializeNumber = objDepartmentStudent.SerializeNumber;
                        ObjDSL.StudentAdmissionId = Convert.ToInt64(dr["StudentAdmissionId"]);
                        ObjDSL.PRN = Convert.ToInt64(dr["PRN"]);
                        ObjDSL.StudentName = dr["StudentName"].ToString();
                        ObjDSL.MobileNo = dr["MobileNo"].ToString();

                        ObjDSL.EmailId = dr["EmailId"].ToString();
                      
                        objDepartmentStudentList.Add(ObjDSL);
                    }
                }
                return Return.returnHttp("200", objDepartmentStudentList, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region Department Student Paper List

        [HttpPost]
        public HttpResponseMessage DepartmentStudentPaperList(DepartmentStudentPaperList objDepartmentStudentPaper)
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
                SqlCommand cmd = new SqlCommand("DepartmentStudentList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AcademicYearId", objDepartmentStudentPaper.AcademicYearId);
                cmd.Parameters.AddWithValue("@FacultyId", objDepartmentStudentPaper.FacultyId);
                cmd.Parameters.AddWithValue("@InstituteId", objDepartmentStudentPaper.InstituteId);
                cmd.Parameters.AddWithValue("@InstancePartTermId", objDepartmentStudentPaper.InstancePartTermId);
                cmd.Parameters.AddWithValue("@ProgrammeId", objDepartmentStudentPaper.ProgrammeId);
                //StudentWisePaperData
                cmd.Parameters.AddWithValue("@ReportType", objDepartmentStudentPaper.ReportType);
                sda.SelectCommand = cmd;
                sda.Fill(Dt);
                List<DepartmentStudentPaperList> objDepartmentStudentPaperList = new List<DepartmentStudentPaperList>();
                objDepartmentStudentPaper.SerializeNumber = 0;
                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        objDepartmentStudentPaper.SerializeNumber = objDepartmentStudentPaper.SerializeNumber + 1;
                        DepartmentStudentPaperList ObjDSPL = new DepartmentStudentPaperList();
                        ObjDSPL.SerializeNumber = objDepartmentStudentPaper.SerializeNumber;
                        ObjDSPL.StudentAdmissionId = Convert.ToInt64(dr["StudentAdmissionId"]);
                        ObjDSPL.PRN = Convert.ToInt64(dr["PRN"]);
                        ObjDSPL.StudentName = dr["StudentName"].ToString();
                        ObjDSPL.MobileNo = dr["MobileNo"].ToString();
                        ObjDSPL.EmailId = dr["EmailId"].ToString();
                        objDepartmentStudentPaperList.Add(ObjDSPL);
                    }
                }
                return Return.returnHttp("200", objDepartmentStudentPaperList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion
    }
}