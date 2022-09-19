using MSUIS_TokenManager.App_Start;
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
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class PreExamStatisticsforStudentListController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();


        #region Pre Exam Statistics for Student List
        [HttpPost]
        public HttpResponseMessage PreExamStatisticsforStudentList(PreExamStatisticsforStudentList ObjPESS)
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

                if (ObjPESS.flag == "1")
                {

                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable();

                    List<PreExamStatisticsforStudentList> alist = new List<PreExamStatisticsforStudentList>();
                    Int32 count = 0;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@FacultyExamMapId", ObjPESS.FacultyExamMapId);
                    cmd.Parameters.AddWithValue("@ExamMasterId", ObjPESS.ExamEventId);
                    cmd.Parameters.AddWithValue("@ProgrammePartTermId", ObjPESS.ProgrammePartTermId);
                    cmd.Parameters.AddWithValue("@FacultyId", ObjPESS.FacultyId);
                    cmd.Parameters.AddWithValue("@InstituteId", ObjPESS.InstituteId);
                    cmd.Parameters.AddWithValue("@SpecialisationId", ObjPESS.SpecialisationId);
                    cmd.Parameters.AddWithValue("@Flag", ObjPESS.flag);
                    cmd.CommandText = "PreExaminationStatisticsforStudentList";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    foreach (DataRow DR in dt.Rows)
                    {
                        PreExamStatisticsforStudentList a = new PreExamStatisticsforStudentList();

                        a.PRN = Convert.ToInt64(DR["PRN"].ToString());
                        a.FullName = DR["FullName"].ToString();
                        a.FacultyId = Convert.ToInt32(DR["FacultyId"].ToString());
                        a.FacultyName = DR["FacultyName"].ToString();
                        a.PartTermName = DR["PartTermName"].ToString();
                        a.ProgrammeName = DR["ProgrammeName"].ToString();
                        a.BranchName = DR["BranchName"].ToString();
                        a.InstituteName = DR["InstituteName"].ToString();
                        

                        count = count + 1;
                        a.IndexId = count;

                        alist.Add(a);
                    }
                    return Return.returnHttp("200", alist, null);

                }
                else if (ObjPESS.flag == "2")
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable();

                    List<PreExamStatisticsforStudentList> alist = new List<PreExamStatisticsforStudentList>();
                    Int32 count = 0;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@FacultyExamMapId", ObjPESS.FacultyExamMapId);
                    cmd.Parameters.AddWithValue("@ExamMasterId", ObjPESS.ExamEventId);
                    cmd.Parameters.AddWithValue("@ProgrammePartTermId", ObjPESS.ProgrammePartTermId);
                    cmd.Parameters.AddWithValue("@FacultyId", ObjPESS.FacultyId);
                    cmd.Parameters.AddWithValue("@InstituteId", ObjPESS.InstituteId);
                    cmd.Parameters.AddWithValue("@SpecialisationId", ObjPESS.SpecialisationId);
                    cmd.Parameters.AddWithValue("@Flag", ObjPESS.flag);
                    cmd.CommandText = "PreExaminationStatisticsforStudentList";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    foreach (DataRow DR in dt.Rows)
                    {
                        PreExamStatisticsforStudentList a = new PreExamStatisticsforStudentList();

                        a.PRN = Convert.ToInt64(DR["PRN"].ToString());
                        a.FullName = DR["FullName"].ToString();
                        a.FacultyId = Convert.ToInt32(DR["FacultyId"].ToString());
                        a.FacultyName = DR["FacultyName"].ToString();
                        a.PartTermName = DR["PartTermName"].ToString();
                        a.ProgrammeName = DR["ProgrammeName"].ToString();
                        a.BranchName = DR["BranchName"].ToString();
                        a.InstituteName = DR["InstituteName"].ToString();
                        count = count + 1;
                        a.IndexId = count;

                        alist.Add(a);
                    }
                    return Return.returnHttp("200", alist, null);
                }
                else if (ObjPESS.flag == "3")
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable();

                    List<PreExamStatisticsforStudentList> alist = new List<PreExamStatisticsforStudentList>();
                    Int32 count = 0;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@FacultyExamMapId", ObjPESS.FacultyExamMapId);
                    cmd.Parameters.AddWithValue("@ExamMasterId", ObjPESS.ExamEventId);
                    cmd.Parameters.AddWithValue("@ProgrammePartTermId", ObjPESS.ProgrammePartTermId);
                    cmd.Parameters.AddWithValue("@FacultyId", ObjPESS.FacultyId);
                    cmd.Parameters.AddWithValue("@InstituteId", ObjPESS.InstituteId);
                    cmd.Parameters.AddWithValue("@SpecialisationId", ObjPESS.SpecialisationId);
                    cmd.Parameters.AddWithValue("@Flag", ObjPESS.flag);
                    cmd.CommandText = "PreExaminationStatisticsforStudentList";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    foreach (DataRow DR in dt.Rows)
                    {
                        PreExamStatisticsforStudentList a = new PreExamStatisticsforStudentList();

                        a.PRN = Convert.ToInt64(DR["PRN"].ToString());
                        a.FullName = DR["FullName"].ToString();
                        a.FacultyId = Convert.ToInt32(DR["FacultyId"].ToString());
                        a.FacultyName = DR["FacultyName"].ToString();
                        a.PartTermName = DR["PartTermName"].ToString();
                        a.ProgrammeName = DR["ProgrammeName"].ToString();
                        a.BranchName = DR["BranchName"].ToString();
                        a.InstituteName = DR["InstituteName"].ToString();
                        count = count + 1;
                        a.IndexId = count;

                        alist.Add(a);
                    }
                    return Return.returnHttp("200", alist, null);
                }
                else if (ObjPESS.flag == "4")
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable();

                    List<PreExamStatisticsforStudentList> alist = new List<PreExamStatisticsforStudentList>();
                    Int32 count = 0;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@FacultyExamMapId", ObjPESS.FacultyExamMapId);
                    cmd.Parameters.AddWithValue("@ExamMasterId", ObjPESS.ExamEventId);
                    cmd.Parameters.AddWithValue("@ProgrammePartTermId", ObjPESS.ProgrammePartTermId);
                    cmd.Parameters.AddWithValue("@FacultyId", ObjPESS.FacultyId);
                    cmd.Parameters.AddWithValue("@InstituteId", ObjPESS.InstituteId);
                    cmd.Parameters.AddWithValue("@SpecialisationId", ObjPESS.SpecialisationId);
                    cmd.Parameters.AddWithValue("@Flag", ObjPESS.flag);
                    cmd.CommandText = "PreExaminationStatisticsforStudentList";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    foreach (DataRow DR in dt.Rows)
                    {
                        PreExamStatisticsforStudentList a = new PreExamStatisticsforStudentList();

                        //a.ApplicationId = Convert.ToInt64(DR["ApplicationId"].ToString());
                        a.PRN = Convert.ToInt64(DR["PRN"].ToString());
                        a.SeatNumber = DR["SeatNumber"].ToString();
                        a.FullName = DR["FullName"].ToString();
                        a.FacultyId = Convert.ToInt32(DR["FacultyId"].ToString());
                        a.FacultyName = DR["FacultyName"].ToString();
                        a.PartTermName = DR["PartTermName"].ToString();
                        a.ProgrammeName = DR["ProgrammeName"].ToString();
                        a.BranchName = DR["BranchName"].ToString();
                        a.InstituteName = DR["InstituteName"].ToString();
                        count = count + 1;
                        a.IndexId = count;

                        alist.Add(a);
                    }
                    return Return.returnHttp("200", alist, null);
                }
                else if (ObjPESS.flag == "5")
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable();

                    List<PreExamStatisticsforStudentList> alist = new List<PreExamStatisticsforStudentList>();
                    Int32 count = 0;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@FacultyExamMapId", ObjPESS.FacultyExamMapId);
                    cmd.Parameters.AddWithValue("@ExamMasterId", ObjPESS.ExamEventId);
                    cmd.Parameters.AddWithValue("@ProgrammePartTermId", ObjPESS.ProgrammePartTermId);
                    cmd.Parameters.AddWithValue("@FacultyId", ObjPESS.FacultyId);
                    cmd.Parameters.AddWithValue("@InstituteId", ObjPESS.InstituteId);
                    cmd.Parameters.AddWithValue("@SpecialisationId", ObjPESS.SpecialisationId);
                    cmd.Parameters.AddWithValue("@Flag", ObjPESS.flag);
                    cmd.CommandText = "PreExaminationStatisticsforStudentList";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    foreach (DataRow DR in dt.Rows)
                    {
                        PreExamStatisticsforStudentList a = new PreExamStatisticsforStudentList();

                        //a.ApplicationId = Convert.ToInt64(DR["ApplicationId"].ToString());
                        a.PRN = Convert.ToInt64(DR["PRN"].ToString());
                        a.SeatNumber = DR["SeatNumber"].ToString();
                        a.FullName = DR["FullName"].ToString();
                        a.FacultyId = Convert.ToInt32(DR["FacultyId"].ToString());
                        a.FacultyName = DR["FacultyName"].ToString();
                        a.PartTermName = DR["PartTermName"].ToString();
                        a.ProgrammeName = DR["ProgrammeName"].ToString();
                        a.BranchName = DR["BranchName"].ToString();
                        a.InstituteName = DR["InstituteName"].ToString();
                        a.ExamVenueName = DR["ExamVenueName"].ToString();

                        count = count + 1;
                        a.IndexId = count;

                        alist.Add(a);
                    }
                    return Return.returnHttp("200", alist, null);
                }
                else if (ObjPESS.flag == "6")
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable();

                    List<PreExamStatisticsforStudentList> alist = new List<PreExamStatisticsforStudentList>();
                    Int32 count = 0;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@FacultyExamMapId", ObjPESS.FacultyExamMapId);
                    cmd.Parameters.AddWithValue("@ExamMasterId", ObjPESS.ExamEventId);
                    cmd.Parameters.AddWithValue("@ProgrammePartTermId", ObjPESS.ProgrammePartTermId);
                    cmd.Parameters.AddWithValue("@FacultyId", ObjPESS.FacultyId);
                    cmd.Parameters.AddWithValue("@InstituteId", ObjPESS.InstituteId);
                    cmd.Parameters.AddWithValue("@SpecialisationId", ObjPESS.SpecialisationId);
                    cmd.Parameters.AddWithValue("@Flag", ObjPESS.flag);
                    cmd.CommandText = "PreExaminationStatisticsforStudentList";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    foreach (DataRow DR in dt.Rows)
                    {
                        PreExamStatisticsforStudentList a = new PreExamStatisticsforStudentList();

                        //a.ApplicationId = Convert.ToInt64(DR["ApplicationId"].ToString());
                        a.PRN = Convert.ToInt64(DR["PRN"].ToString());
                        a.SeatNumber = DR["SeatNumber"].ToString();
                        a.FullName = DR["FullName"].ToString();
                        a.FacultyId = Convert.ToInt32(DR["FacultyId"].ToString());
                        a.FacultyName = DR["FacultyName"].ToString();
                        a.PartTermName = DR["PartTermName"].ToString();
                        a.ProgrammeName = DR["ProgrammeName"].ToString();
                        a.BranchName = DR["BranchName"].ToString();
                        a.InstituteName = DR["InstituteName"].ToString();
                        //a.ExamVenueName = DR["ExamVenueName"].ToString();
                        //a.InwardMode = DR["InwardMode"].ToString();


                        count = count + 1;
                        a.IndexId = count;

                        alist.Add(a);
                    }
                    return Return.returnHttp("200", alist, null);
                }
                else if (ObjPESS.flag == "7")
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable();

                    List<PreExamStatisticsforStudentList> alist = new List<PreExamStatisticsforStudentList>();
                    Int32 count = 0;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@FacultyExamMapId", ObjPESS.FacultyExamMapId);
                    cmd.Parameters.AddWithValue("@ExamMasterId", ObjPESS.ExamEventId);
                    cmd.Parameters.AddWithValue("@ProgrammePartTermId", ObjPESS.ProgrammePartTermId);
                    cmd.Parameters.AddWithValue("@FacultyId", ObjPESS.FacultyId);
                    cmd.Parameters.AddWithValue("@InstituteId", ObjPESS.InstituteId);
                    cmd.Parameters.AddWithValue("@SpecialisationId", ObjPESS.SpecialisationId);
                    cmd.Parameters.AddWithValue("@Flag", ObjPESS.flag);
                    cmd.CommandText = "PreExaminationStatisticsforStudentList";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    foreach (DataRow DR in dt.Rows)
                    {
                        PreExamStatisticsforStudentList a = new PreExamStatisticsforStudentList();

                        //a.ApplicationId = Convert.ToInt64(DR["ApplicationId"].ToString());
                        a.PRN = Convert.ToInt64(DR["PRN"].ToString());
                        a.SeatNumber = DR["SeatNumber"].ToString();
                        a.FullName = DR["FullName"].ToString();
                        a.FacultyId = Convert.ToInt32(DR["FacultyId"].ToString());
                        a.FacultyName = DR["FacultyName"].ToString();
                        a.PartTermName = DR["PartTermName"].ToString();
                        a.ProgrammeName = DR["ProgrammeName"].ToString();
                        a.BranchName = DR["BranchName"].ToString();
                        a.InstituteName = DR["InstituteName"].ToString();
                        //a.ExamVenueName = DR["ExamVenueName"].ToString();
                        //a.InwardMode = DR["InwardMode"].ToString();


                        count = count + 1;
                        a.IndexId = count;

                        alist.Add(a);
                    }
                    return Return.returnHttp("200", alist, null);
                }
                else if (ObjPESS.flag == "8")
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable();

                    List<PreExamStatisticsforStudentList> alist = new List<PreExamStatisticsforStudentList>();
                    Int32 count = 0;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@FacultyExamMapId", ObjPESS.FacultyExamMapId);
                    cmd.Parameters.AddWithValue("@ExamMasterId", ObjPESS.ExamEventId);
                    cmd.Parameters.AddWithValue("@ProgrammePartTermId", ObjPESS.ProgrammePartTermId);
                    cmd.Parameters.AddWithValue("@FacultyId", ObjPESS.FacultyId);
                    cmd.Parameters.AddWithValue("@InstituteId", ObjPESS.InstituteId);
                    cmd.Parameters.AddWithValue("@SpecialisationId", ObjPESS.SpecialisationId);
                    cmd.Parameters.AddWithValue("@Flag", ObjPESS.flag);
                    cmd.CommandText = "PreExaminationStatisticsforStudentList";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    foreach (DataRow DR in dt.Rows)
                    {
                        PreExamStatisticsforStudentList a = new PreExamStatisticsforStudentList();

                        //a.ApplicationId = Convert.ToInt64(DR["ApplicationId"].ToString());
                        a.PRN = Convert.ToInt64(DR["PRN"].ToString());
                        a.SeatNumber = DR["SeatNumber"].ToString();
                        a.FullName = DR["FullName"].ToString();
                        a.FacultyId = Convert.ToInt32(DR["FacultyId"].ToString());
                        a.FacultyName = DR["FacultyName"].ToString();
                        a.PartTermName = DR["PartTermName"].ToString();
                        a.ProgrammeName = DR["ProgrammeName"].ToString();
                        a.BranchName = DR["BranchName"].ToString();
                        a.InstituteName = DR["InstituteName"].ToString();
                        a.ExamFees = (Convert.ToString(DR["ExamFees"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["ExamFees"]);
                        //a.InwardMode = DR["InwardMode"].ToString();


                        count = count + 1;
                        a.IndexId = count;

                        alist.Add(a);
                    }
                    return Return.returnHttp("200", alist, null);
                }
                else if (ObjPESS.flag == "9")
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable();

                    List<PreExamStatisticsforStudentList> alist = new List<PreExamStatisticsforStudentList>();
                    Int32 count = 0;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@FacultyExamMapId", ObjPESS.FacultyExamMapId);
                    cmd.Parameters.AddWithValue("@ExamMasterId", ObjPESS.ExamEventId);
                    cmd.Parameters.AddWithValue("@ProgrammePartTermId", ObjPESS.ProgrammePartTermId);
                    cmd.Parameters.AddWithValue("@FacultyId", ObjPESS.FacultyId);
                    cmd.Parameters.AddWithValue("@InstituteId", ObjPESS.InstituteId);
                    cmd.Parameters.AddWithValue("@SpecialisationId", ObjPESS.SpecialisationId);
                    cmd.Parameters.AddWithValue("@Flag", ObjPESS.flag);
                    cmd.CommandText = "PreExaminationStatisticsforStudentList";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    foreach (DataRow DR in dt.Rows)
                    {
                        PreExamStatisticsforStudentList a = new PreExamStatisticsforStudentList();

                        //a.ApplicationId = Convert.ToInt64(DR["ApplicationId"].ToString());
                        a.PRN = Convert.ToInt64(DR["PRN"].ToString());
                        a.SeatNumber = DR["SeatNumber"].ToString();
                        a.FullName = DR["FullName"].ToString();
                        a.FacultyId = Convert.ToInt32(DR["FacultyId"].ToString());
                        a.FacultyName = DR["FacultyName"].ToString();
                        a.PartTermName = DR["PartTermName"].ToString();
                        a.ProgrammeName = DR["ProgrammeName"].ToString();
                        a.BranchName = DR["BranchName"].ToString();
                        a.InstituteName = DR["InstituteName"].ToString();
                        a.ExamFees = (Convert.ToString(DR["ExamFees"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["ExamFees"]);
                        //a.InwardMode = DR["InwardMode"].ToString();


                        count = count + 1;
                        a.IndexId = count;

                        alist.Add(a);
                    }
                    return Return.returnHttp("200", alist, null);
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


        #region Pre Exam Statistics for Student List Faculty
        [HttpPost]
        public HttpResponseMessage PreExamStatisticsforStudentListFaculty(PreExamStatisticsforStudentList ObjPESS)
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

                if (ObjPESS.flag == "1")
                {

                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable();

                    List<PreExamStatisticsforStudentList> alist = new List<PreExamStatisticsforStudentList>();
                    Int32 count = 0;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@FacultyExamMapId", ObjPESS.FacultyExamMapId);
                    cmd.Parameters.AddWithValue("@ExamMasterId", ObjPESS.ExamEventId);
                    cmd.Parameters.AddWithValue("@ProgrammePartTermId", ObjPESS.ProgrammePartTermId);
                    cmd.Parameters.AddWithValue("@FacultyId", ObjPESS.FacultyId);
                    cmd.Parameters.AddWithValue("@InstituteId", ObjPESS.InstituteId);
                    cmd.Parameters.AddWithValue("@SpecialisationId", ObjPESS.SpecialisationId);
                    cmd.Parameters.AddWithValue("@Flag", ObjPESS.flag);
                    cmd.CommandText = "PreExaminationStatisticsforStudentListFaculty";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    foreach (DataRow DR in dt.Rows)
                    {
                        PreExamStatisticsforStudentList a = new PreExamStatisticsforStudentList();

                        a.PRN = Convert.ToInt64(DR["PRN"].ToString());
                        a.FullName = DR["FullName"].ToString();
                        a.FacultyId = Convert.ToInt32(DR["FacultyId"].ToString());
                        a.FacultyName = DR["FacultyName"].ToString();
                        a.PartTermName = DR["PartTermName"].ToString();
                        a.ProgrammeName = DR["ProgrammeName"].ToString();
                        a.BranchName = DR["BranchName"].ToString();
                        a.InstituteName = DR["InstituteName"].ToString();
                        count = count + 1;
                        a.IndexId = count;

                        alist.Add(a);
                    }
                    return Return.returnHttp("200", alist, null);

                }
                else if (ObjPESS.flag == "2")
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable();

                    List<PreExamStatisticsforStudentList> alist = new List<PreExamStatisticsforStudentList>();
                    Int32 count = 0;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@FacultyExamMapId", ObjPESS.FacultyExamMapId);
                    cmd.Parameters.AddWithValue("@ExamMasterId", ObjPESS.ExamEventId);
                    cmd.Parameters.AddWithValue("@ProgrammePartTermId", ObjPESS.ProgrammePartTermId);
                    cmd.Parameters.AddWithValue("@FacultyId", ObjPESS.FacultyId);
                    cmd.Parameters.AddWithValue("@InstituteId", ObjPESS.InstituteId);
                    cmd.Parameters.AddWithValue("@SpecialisationId", ObjPESS.SpecialisationId);
                    cmd.Parameters.AddWithValue("@Flag", ObjPESS.flag);
                    cmd.CommandText = "PreExaminationStatisticsforStudentListFaculty";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    foreach (DataRow DR in dt.Rows)
                    {
                        PreExamStatisticsforStudentList a = new PreExamStatisticsforStudentList();

                        a.PRN = Convert.ToInt64(DR["PRN"].ToString());
                        a.FullName = DR["FullName"].ToString();
                        a.FacultyId = Convert.ToInt32(DR["FacultyId"].ToString());
                        a.FacultyName = DR["FacultyName"].ToString();
                        a.PartTermName = DR["PartTermName"].ToString();
                        a.ProgrammeName = DR["ProgrammeName"].ToString();
                        a.BranchName = DR["BranchName"].ToString();
                        a.InstituteName = DR["InstituteName"].ToString();
                        count = count + 1;
                        a.IndexId = count;

                        alist.Add(a);
                    }
                    return Return.returnHttp("200", alist, null);
                }
                else if (ObjPESS.flag == "3")
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable();

                    List<PreExamStatisticsforStudentList> alist = new List<PreExamStatisticsforStudentList>();
                    Int32 count = 0;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@FacultyExamMapId", ObjPESS.FacultyExamMapId);
                    cmd.Parameters.AddWithValue("@ExamMasterId", ObjPESS.ExamEventId);
                    cmd.Parameters.AddWithValue("@ProgrammePartTermId", ObjPESS.ProgrammePartTermId);
                    cmd.Parameters.AddWithValue("@FacultyId", ObjPESS.FacultyId);
                    cmd.Parameters.AddWithValue("@InstituteId", ObjPESS.InstituteId);
                    cmd.Parameters.AddWithValue("@SpecialisationId", ObjPESS.SpecialisationId);
                    cmd.Parameters.AddWithValue("@Flag", ObjPESS.flag);
                    cmd.CommandText = "PreExaminationStatisticsforStudentListFaculty";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    foreach (DataRow DR in dt.Rows)
                    {
                        PreExamStatisticsforStudentList a = new PreExamStatisticsforStudentList();

                        a.PRN = Convert.ToInt64(DR["PRN"].ToString());
                        a.FullName = DR["FullName"].ToString();
                        a.FacultyId = Convert.ToInt32(DR["FacultyId"].ToString());
                        a.FacultyName = DR["FacultyName"].ToString();
                        a.PartTermName = DR["PartTermName"].ToString();
                        a.ProgrammeName = DR["ProgrammeName"].ToString();
                        a.BranchName = DR["BranchName"].ToString();
                        a.InstituteName = DR["InstituteName"].ToString();
                        count = count + 1;
                        a.IndexId = count;

                        alist.Add(a);
                    }
                    return Return.returnHttp("200", alist, null);
                }
                else if (ObjPESS.flag == "4")
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable();

                    List<PreExamStatisticsforStudentList> alist = new List<PreExamStatisticsforStudentList>();
                    Int32 count = 0;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@FacultyExamMapId", ObjPESS.FacultyExamMapId);
                    cmd.Parameters.AddWithValue("@ExamMasterId", ObjPESS.ExamEventId);
                    cmd.Parameters.AddWithValue("@ProgrammePartTermId", ObjPESS.ProgrammePartTermId);
                    cmd.Parameters.AddWithValue("@FacultyId", ObjPESS.FacultyId);
                    cmd.Parameters.AddWithValue("@InstituteId", ObjPESS.InstituteId);
                    cmd.Parameters.AddWithValue("@SpecialisationId", ObjPESS.SpecialisationId);
                    cmd.Parameters.AddWithValue("@Flag", ObjPESS.flag);
                    cmd.CommandText = "PreExaminationStatisticsforStudentListFaculty";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    foreach (DataRow DR in dt.Rows)
                    {
                        PreExamStatisticsforStudentList a = new PreExamStatisticsforStudentList();

                        //a.ApplicationId = Convert.ToInt64(DR["ApplicationId"].ToString());
                        a.PRN = Convert.ToInt64(DR["PRN"].ToString());
                        a.SeatNumber = DR["SeatNumber"].ToString();
                        a.FullName = DR["FullName"].ToString();
                        a.FacultyId = Convert.ToInt32(DR["FacultyId"].ToString());
                        a.FacultyName = DR["FacultyName"].ToString();
                        a.PartTermName = DR["PartTermName"].ToString();
                        a.ProgrammeName = DR["ProgrammeName"].ToString();
                        a.BranchName = DR["BranchName"].ToString();
                        a.InstituteName = DR["InstituteName"].ToString();
                        count = count + 1;
                        a.IndexId = count;

                        alist.Add(a);
                    }
                    return Return.returnHttp("200", alist, null);
                }
                else if (ObjPESS.flag == "5")
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable();

                    List<PreExamStatisticsforStudentList> alist = new List<PreExamStatisticsforStudentList>();
                    Int32 count = 0;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@FacultyExamMapId", ObjPESS.FacultyExamMapId);
                    cmd.Parameters.AddWithValue("@ExamMasterId", ObjPESS.ExamEventId);
                    cmd.Parameters.AddWithValue("@ProgrammePartTermId", ObjPESS.ProgrammePartTermId);
                    cmd.Parameters.AddWithValue("@FacultyId", ObjPESS.FacultyId);
                    cmd.Parameters.AddWithValue("@InstituteId", ObjPESS.InstituteId);
                    cmd.Parameters.AddWithValue("@SpecialisationId", ObjPESS.SpecialisationId);
                    cmd.Parameters.AddWithValue("@Flag", ObjPESS.flag);
                    cmd.CommandText = "PreExaminationStatisticsforStudentListFaculty";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    foreach (DataRow DR in dt.Rows)
                    {
                        PreExamStatisticsforStudentList a = new PreExamStatisticsforStudentList();

                        //a.ApplicationId = Convert.ToInt64(DR["ApplicationId"].ToString());
                        a.PRN = Convert.ToInt64(DR["PRN"].ToString());
                        a.SeatNumber = DR["SeatNumber"].ToString();
                        a.FullName = DR["FullName"].ToString();
                        a.FacultyId = Convert.ToInt32(DR["FacultyId"].ToString());
                        a.FacultyName = DR["FacultyName"].ToString();
                        a.PartTermName = DR["PartTermName"].ToString();
                        a.ProgrammeName = DR["ProgrammeName"].ToString();
                        a.BranchName = DR["BranchName"].ToString();
                        a.InstituteName = DR["InstituteName"].ToString();
                        a.ExamVenueName = DR["ExamVenueName"].ToString();

                        count = count + 1;
                        a.IndexId = count;

                        alist.Add(a);
                    }
                    return Return.returnHttp("200", alist, null);
                }
                else if (ObjPESS.flag == "6")
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable();

                    List<PreExamStatisticsforStudentList> alist = new List<PreExamStatisticsforStudentList>();
                    Int32 count = 0;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@FacultyExamMapId", ObjPESS.FacultyExamMapId);
                    cmd.Parameters.AddWithValue("@ExamMasterId", ObjPESS.ExamEventId);
                    cmd.Parameters.AddWithValue("@ProgrammePartTermId", ObjPESS.ProgrammePartTermId);
                    cmd.Parameters.AddWithValue("@FacultyId", ObjPESS.FacultyId);
                    cmd.Parameters.AddWithValue("@InstituteId", ObjPESS.InstituteId);
                    cmd.Parameters.AddWithValue("@SpecialisationId", ObjPESS.SpecialisationId);
                    cmd.Parameters.AddWithValue("@Flag", ObjPESS.flag);
                    cmd.CommandText = "PreExaminationStatisticsforStudentListFaculty";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    foreach (DataRow DR in dt.Rows)
                    {
                        PreExamStatisticsforStudentList a = new PreExamStatisticsforStudentList();

                        //a.ApplicationId = Convert.ToInt64(DR["ApplicationId"].ToString());
                        a.PRN = Convert.ToInt64(DR["PRN"].ToString());
                        a.SeatNumber = DR["SeatNumber"].ToString();
                        a.FullName = DR["FullName"].ToString();
                        a.FacultyId = Convert.ToInt32(DR["FacultyId"].ToString());
                        a.FacultyName = DR["FacultyName"].ToString();
                        a.PartTermName = DR["PartTermName"].ToString();
                        a.ProgrammeName = DR["ProgrammeName"].ToString();
                        a.BranchName = DR["BranchName"].ToString();
                        a.InstituteName = DR["InstituteName"].ToString();
                        //a.ExamVenueName = DR["ExamVenueName"].ToString();
                        a.InwardMode = DR["InwardMode"].ToString();


                        count = count + 1;
                        a.IndexId = count;

                        alist.Add(a);
                    }
                    return Return.returnHttp("200", alist, null);
                }
                else if (ObjPESS.flag == "7")
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable();

                    List<PreExamStatisticsforStudentList> alist = new List<PreExamStatisticsforStudentList>();
                    Int32 count = 0;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@FacultyExamMapId", ObjPESS.FacultyExamMapId);
                    cmd.Parameters.AddWithValue("@ExamMasterId", ObjPESS.ExamEventId);
                    cmd.Parameters.AddWithValue("@ProgrammePartTermId", ObjPESS.ProgrammePartTermId);
                    cmd.Parameters.AddWithValue("@FacultyId", ObjPESS.FacultyId);
                    cmd.Parameters.AddWithValue("@InstituteId", ObjPESS.InstituteId);
                    cmd.Parameters.AddWithValue("@SpecialisationId", ObjPESS.SpecialisationId);
                    cmd.Parameters.AddWithValue("@Flag", ObjPESS.flag);
                    cmd.CommandText = "PreExaminationStatisticsforStudentListFaculty";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    foreach (DataRow DR in dt.Rows)
                    {
                        PreExamStatisticsforStudentList a = new PreExamStatisticsforStudentList();

                        //a.ApplicationId = Convert.ToInt64(DR["ApplicationId"].ToString());
                        a.PRN = Convert.ToInt64(DR["PRN"].ToString());
                        a.SeatNumber = DR["SeatNumber"].ToString();
                        a.FullName = DR["FullName"].ToString();
                        a.FacultyId = Convert.ToInt32(DR["FacultyId"].ToString());
                        a.FacultyName = DR["FacultyName"].ToString();
                        a.PartTermName = DR["PartTermName"].ToString();
                        a.ProgrammeName = DR["ProgrammeName"].ToString();
                        a.BranchName = DR["BranchName"].ToString();
                        a.InstituteName = DR["InstituteName"].ToString();
                        //a.ExamVenueName = DR["ExamVenueName"].ToString();
                       
                        a.InwardMode = DR["InwardMode"].ToString();


                        count = count + 1;
                        a.IndexId = count;

                        alist.Add(a);
                    }
                    return Return.returnHttp("200", alist, null);
                }
                else if (ObjPESS.flag == "8")
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable();

                    List<PreExamStatisticsforStudentList> alist = new List<PreExamStatisticsforStudentList>();
                    Int32 count = 0;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@FacultyExamMapId", ObjPESS.FacultyExamMapId);
                    cmd.Parameters.AddWithValue("@ExamMasterId", ObjPESS.ExamEventId);
                    cmd.Parameters.AddWithValue("@ProgrammePartTermId", ObjPESS.ProgrammePartTermId);
                    cmd.Parameters.AddWithValue("@FacultyId", ObjPESS.FacultyId);
                    cmd.Parameters.AddWithValue("@InstituteId", ObjPESS.InstituteId);
                    cmd.Parameters.AddWithValue("@SpecialisationId", ObjPESS.SpecialisationId);
                    cmd.Parameters.AddWithValue("@Flag", ObjPESS.flag);
                    cmd.CommandText = "PreExaminationStatisticsforStudentList";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    foreach (DataRow DR in dt.Rows)
                    {
                        PreExamStatisticsforStudentList a = new PreExamStatisticsforStudentList();

                        //a.ApplicationId = Convert.ToInt64(DR["ApplicationId"].ToString());
                        a.PRN = Convert.ToInt64(DR["PRN"].ToString());
                        a.SeatNumber = DR["SeatNumber"].ToString();
                        a.FullName = DR["FullName"].ToString();
                        a.FacultyId = Convert.ToInt32(DR["FacultyId"].ToString());
                        a.FacultyName = DR["FacultyName"].ToString();
                        a.PartTermName = DR["PartTermName"].ToString();
                        a.ProgrammeName = DR["ProgrammeName"].ToString();
                        a.BranchName = DR["BranchName"].ToString();
                        a.InstituteName = DR["InstituteName"].ToString();
                        a.ExamFees = (Convert.ToString(DR["ExamFees"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["ExamFees"]);
                        //a.InwardMode = DR["InwardMode"].ToString();


                        count = count + 1;
                        a.IndexId = count;

                        alist.Add(a);
                    }
                    return Return.returnHttp("200", alist, null);
                }
                else if (ObjPESS.flag == "9")
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable();

                    List<PreExamStatisticsforStudentList> alist = new List<PreExamStatisticsforStudentList>();
                    Int32 count = 0;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@FacultyExamMapId", ObjPESS.FacultyExamMapId);
                    cmd.Parameters.AddWithValue("@ExamMasterId", ObjPESS.ExamEventId);
                    cmd.Parameters.AddWithValue("@ProgrammePartTermId", ObjPESS.ProgrammePartTermId);
                    cmd.Parameters.AddWithValue("@FacultyId", ObjPESS.FacultyId);
                    cmd.Parameters.AddWithValue("@InstituteId", ObjPESS.InstituteId);
                    cmd.Parameters.AddWithValue("@SpecialisationId", ObjPESS.SpecialisationId);
                    cmd.Parameters.AddWithValue("@Flag", ObjPESS.flag);
                    cmd.CommandText = "PreExaminationStatisticsforStudentList";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    foreach (DataRow DR in dt.Rows)
                    {
                        PreExamStatisticsforStudentList a = new PreExamStatisticsforStudentList();

                        //a.ApplicationId = Convert.ToInt64(DR["ApplicationId"].ToString());
                        a.PRN = Convert.ToInt64(DR["PRN"].ToString());
                        a.SeatNumber = DR["SeatNumber"].ToString();
                        a.FullName = DR["FullName"].ToString();
                        a.FacultyId = Convert.ToInt32(DR["FacultyId"].ToString());
                        a.FacultyName = DR["FacultyName"].ToString();
                        a.PartTermName = DR["PartTermName"].ToString();
                        a.ProgrammeName = DR["ProgrammeName"].ToString();
                        a.BranchName = DR["BranchName"].ToString();
                        a.InstituteName = DR["InstituteName"].ToString();
                        a.ExamFees = (Convert.ToString(DR["ExamFees"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["ExamFees"]);
                        //a.InwardMode = DR["InwardMode"].ToString();


                        count = count + 1;
                        a.IndexId = count;

                        alist.Add(a);
                    }
                    return Return.returnHttp("200", alist, null);
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