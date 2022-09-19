using MSUIS_TokenManager.App_Start;
using MSUISApi.BAL;
using MSUISApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MSUISApi.Controllers
{
    public class PaperSelectionReportController : ApiController
    {

        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();


        #region PaperSelectionReportPaperGet
        [HttpPost]
        public HttpResponseMessage PaperSelectionReportPaperGet(PaperSelectionReport PSR)
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

                try
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    

                    List<PaperSelectionReport> plist = new List<PaperSelectionReport>();
                    Int32 count = 0;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@ProgrammePartTermId", PSR.ProgrammePartTermId);
                    cmd.Parameters.AddWithValue("@InstituteId", PSR.InstituteId);
                    cmd.Parameters.AddWithValue("@AcademicYearId", PSR.AcademicYearId);
                    cmd.CommandText = "PaperSelectionReportByPaperId";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    foreach (DataRow DR in dt.Rows)
                    {
                        PaperSelectionReport p = new PaperSelectionReport();

                        p.PartTermName = DR["PartTermName"].ToString();
                        p.PaperId = DR["PaperId"].ToString();
                        p.PaperName = DR["PaperName"].ToString();
                        p.PaperCode = DR["PaperCode"].ToString();
                        p.InstituteName = DR["InstituteName"].ToString();
                        p.AcademicYearCode = DR["AcademicYearCode"].ToString();
                        p.PaperWise1 = Convert.ToBoolean(DR["PaperWise"]);
                        if (p.PaperWise1 == true)
                        {
                            p.PaperWise = "Paper Wise";
                        }
                        else
                        {
                            p.PaperWise = null;
                        }



                        count = count + 1;
                        p.IndexId = count;


                        plist.Add(p);
                    }
                    return Return.returnHttp("200", plist, null);
                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message.ToString(), null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion


        #region MstInstituteGet
        [HttpPost]
        public HttpResponseMessage MstInstituteGet()
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
                SqlCommand cmd = new SqlCommand("MstInstituteGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<ApplicationStatistics> ObjLstInst = new List<ApplicationStatistics>();
                dt.Rows.Add("0", "All Institute");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ApplicationStatistics ObjInst = new ApplicationStatistics();
                        ObjInst.Id = Convert.ToInt32(dr["Id"]);
                        ObjInst.InstituteName = (dr["InstituteName"].ToString());

                        ObjLstInst.Add(ObjInst);
                    }

                }

                return Return.returnHttp("200", ObjLstInst, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion


        #region AcademicYearGet For DropDown
        [HttpPost]
        public HttpResponseMessage AcademicYearGetForDropDown()
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                List<AcademicYear> ObjListAcadYear = new List<AcademicYear>();
                BALAcademicYear ObjBalAcadYearList = new BALAcademicYear();
                ObjListAcadYear = ObjBalAcadYearList.AcademicYearGetForDropDown();

                if (ObjListAcadYear != null)
                    return Return.returnHttp("200", ObjListAcadYear, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion


        #region ProgrammePartTermGetByInstIdAcaId
        [HttpPost]
        public HttpResponseMessage ProgrammePartTermGetByInstIdAcaId(ProgrammePartTermGetByInstIdAcaId ProgPT)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InstituteId", ProgPT.InstituteId);
                cmd.Parameters.AddWithValue("@AcademicYearId", ProgPT.AcademicYearId);
                cmd.Parameters.AddWithValue("@ReportType", ProgPT.PaperWise);
                cmd.CommandText = "ProgrammePartTermGetByInstIdAcaId";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                da.SelectCommand = cmd;
                da.Fill(dt);
                
                List<ProgrammePartTermGetByInstIdAcaId> ObjLstProgPartTerm = new List<ProgrammePartTermGetByInstIdAcaId>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProgrammePartTermGetByInstIdAcaId ObjProgPartTerm = new ProgrammePartTermGetByInstIdAcaId();

                        ObjProgPartTerm.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        
                        ObjProgPartTerm.PartTermName = Convert.ToString(dt.Rows[i]["PartTermName"]);

                        ObjProgPartTerm.InstituteId = Convert.ToInt32(dt.Rows[i]["InstituteId"]);

                        ObjProgPartTerm.InstituteName = Convert.ToString(dt.Rows[i]["InstituteName"]);

                        ObjProgPartTerm.AcademicYearId = Convert.ToInt32(dt.Rows[i]["AcademicYearId"]);

                        ObjProgPartTerm.AcademicYearCode = Convert.ToString(dt.Rows[i]["AcademicYearCode"]);

                        ObjProgPartTerm.PaperWise1 = Convert.ToBoolean(dt.Rows[i]["PaperWise"]);
                        if (ObjProgPartTerm.PaperWise1 == true)
                        {
                            ObjProgPartTerm.PaperWise = "Paper Wise";
                        }
                        else
                        {
                            ObjProgPartTerm.PaperWise = null;
                        }
                        
                        ObjLstProgPartTerm.Add(ObjProgPartTerm);

                    }
                    
                }
                return Return.returnHttp("200", ObjLstProgPartTerm, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion


        #region ProgPartTermGetByInstIdAcaId
        [HttpPost]
        public HttpResponseMessage ProgPartTermGetByInstIdAcaId(ProgPartTermGetByInstIdAcaId ProgPT)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InstituteId", ProgPT.InstituteId);
                cmd.Parameters.AddWithValue("@AcademicYearId", ProgPT.AcademicYearId);
                cmd.Parameters.AddWithValue("@ReportType", ProgPT.StudentWise);
                cmd.CommandText = "ProgrammePartTermGetByInstIdAcaId";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                da.SelectCommand = cmd;
                da.Fill(dt);
                
                List<ProgPartTermGetByInstIdAcaId> ObjLstProgPartTerm = new List<ProgPartTermGetByInstIdAcaId>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProgPartTermGetByInstIdAcaId ObjProgPart = new ProgPartTermGetByInstIdAcaId();

                        ObjProgPart.Id = Convert.ToInt32(dt.Rows[i]["Id"]);

                        ObjProgPart.PartTermName = Convert.ToString(dt.Rows[i]["PartTermName"]);

                        ObjProgPart.InstituteId = Convert.ToInt32(dt.Rows[i]["InstituteId"]);

                        ObjProgPart.InstituteName = Convert.ToString(dt.Rows[i]["InstituteName"]);

                        ObjProgPart.AcademicYearId = Convert.ToInt32(dt.Rows[i]["AcademicYearId"]);

                        ObjProgPart.AcademicYearCode = Convert.ToString(dt.Rows[i]["AcademicYearCode"]);

                        ObjProgPart.StudentWise1 = Convert.ToBoolean(dt.Rows[i]["StudentWise"]);
                        if (ObjProgPart.StudentWise1 == true)
                        {
                            ObjProgPart.StudentWise = "Student Wise";
                        }
                        else
                        {
                            ObjProgPart.StudentWise = null;
                        }

                        ObjLstProgPartTerm.Add(ObjProgPart);

                    }

                }
                return Return.returnHttp("200", ObjLstProgPartTerm, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region PaperSelectionReportStudentGet
        [HttpPost]
        public HttpResponseMessage PaperSelectionReportStudentGet(PaperSelectionReport PSR)
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

                try
                {
                    SqlDataAdapter da = new SqlDataAdapter();


                    List<PaperSelectionReport> plist = new List<PaperSelectionReport>();
                    Int32 count = 0;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@ProgrammePartTermId", PSR.ProgrammePartTermId);
                    cmd.Parameters.AddWithValue("@InstituteId", PSR.InstituteId);
                    cmd.Parameters.AddWithValue("@AcademicYearId", PSR.AcademicYearId);
                    cmd.CommandText = "PaperSelectionReportforStudentList";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    foreach (DataRow DR in dt.Rows)
                    {
                        PaperSelectionReport p = new PaperSelectionReport();

                        p.PRN = DR["PRN"].ToString();
                        p.FacultyName = DR["FacultyName"].ToString();
                        p.PartTermName = DR["PartTermName"].ToString();
                        p.NameAsPerMarksheet = DR["NameAsPerMarksheet"].ToString();
                        p.PaperName = DR["PaperName"].ToString();
                        p.PaperCode = DR["PaperCode"].ToString();
                        p.InstituteName = DR["InstituteName"].ToString();
                        p.BranchName = DR["BranchName"].ToString();
                        p.ProgrammeName = DR["ProgrammeName"].ToString();
                        p.AcademicYearCode = DR["AcademicYearCode"].ToString();
                        p.StudentWise1 = Convert.ToBoolean(DR["StudentWise"]);
                        if (p.StudentWise1 == true)
                        {
                            p.StudentWise = "Student Wise";
                        }
                        else
                        {
                            p.StudentWise = null;
                        }
                        
                        count = count + 1;
                        p.IndexId = count;


                        plist.Add(p);
                    }
                    return Return.returnHttp("200", plist, null);
                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message.ToString(), null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion


        #region PaperSelectionReportPaperExcelGet
        [HttpPost]
        public HttpResponseMessage PaperSelectionReportPaperExcelGet(PaperSelectionReportExport PSRE)
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

                try
                {
                    SqlDataAdapter da = new SqlDataAdapter();


                    List<PaperSelectionReportExport> plist = new List<PaperSelectionReportExport>();
                    Int32 count = 0;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@PaperId", PSRE.PaperId);
                    cmd.CommandText = "PaperSelectionReportByPaperIdExport";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    foreach (DataRow DR in dt.Rows)
                    {
                        PaperSelectionReportExport p = new PaperSelectionReportExport();

                        p.PRN = DR["PRN"].ToString();
                        p.FacultyName = DR["FacultyName"].ToString();
                        p.PartTermName = DR["PartTermName"].ToString();
                        p.NameAsPerMarksheet = DR["NameAsPerMarksheet"].ToString();
                        p.PaperId = DR["PaperId"].ToString();
                        p.PaperName = DR["PaperName"].ToString();
                        p.PaperCode = DR["PaperCode"].ToString();
                        p.InstituteName = DR["InstituteName"].ToString();
                        p.BranchName = DR["BranchName"].ToString();
                        p.ProgrammeName = DR["ProgrammeName"].ToString();
                        p.InstituteName = DR["InstituteName"].ToString();
                        p.AcademicYearCode = DR["AcademicYearCode"].ToString();
                        p.PaperWise1 = Convert.ToBoolean(DR["PaperWise"]);
                        if (p.PaperWise1 == true)
                        {
                            p.PaperWise = "Paper Wise";
                        }
                        else
                        {
                            p.PaperWise = null;
                        }



                        count = count + 1;
                        p.IndexId = count;


                        plist.Add(p);
                    }
                    return Return.returnHttp("200", plist, null);
                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message.ToString(), null);
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