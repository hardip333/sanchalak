using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using MSUISApi.Models;
using Newtonsoft.Json.Linq;
using System.Reflection.Emit;
using System.Web.WebPages;
using MSUIS_TokenManager.App_Start;

namespace MSUISApi.Controllers
{
    public class MstProgrammeInstancePartTermController : ApiController
    {

        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        Validation validation = new Validation();

        #region ProgrammeInstancePartTermGet

        [HttpPost]
        public HttpResponseMessage ProgrammeInstancePartTermGet()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("IncProgrammeInstancePartTermGet", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                da.Fill(dt);
                Int32 count = 0;
                List<ProgrammeInstancePartTerm> ObjLstProgInstPartTerm = new List<ProgrammeInstancePartTerm>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePartTerm ObjProgPartTerm = new ProgrammeInstancePartTerm();
                        ObjProgPartTerm.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjProgPartTerm.FacultyName = (Convert.ToString(dt.Rows[i]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["FacultyName"]);
                        ObjProgPartTerm.FacultyId = (Convert.ToString(dt.Rows[i]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[i]["FacultyId"]);
                        ObjProgPartTerm.ProgrammeInstanceId = (Convert.ToString(dt.Rows[i]["ProgrammeInstanceId"])).IsEmpty() ? 0 : Convert.ToInt64(dt.Rows[i]["ProgrammeInstanceId"]);
                        ObjProgPartTerm.ProgrammeId = (Convert.ToString(dt.Rows[i]["ProgrammeId"])).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[i]["ProgrammeId"]);
                        ObjProgPartTerm.ProgrammeName = (Convert.ToString(dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["ProgrammeName"]);
                        ObjProgPartTerm.ProgrammeCode = (Convert.ToString(dt.Rows[i]["ProgrammeCode"])).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["ProgrammeCode"]);
                        ObjProgPartTerm.SpecialisationId = (Convert.ToString(dt.Rows[i]["SpecialisationId"])).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[i]["SpecialisationId"]);
                        ObjProgPartTerm.BranchName = (Convert.ToString(dt.Rows[i]["BranchName"])).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["BranchName"]);
                        ObjProgPartTerm.PartName = (Convert.ToString(dt.Rows[i]["PartName"])).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["PartName"]);
                        ObjProgPartTerm.PartShortName = (Convert.ToString(dt.Rows[i]["PartShortName"])).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["PartShortName"]);
                        ObjProgPartTerm.ProgrammePartId = (Convert.ToString(dt.Rows[i]["ProgrammePartId"])).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[i]["ProgrammePartId"]);
                        ObjProgPartTerm.AcademicYearCode = (Convert.ToString(dt.Rows[i]["AcademicYearCode"])).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["AcademicYearCode"]);
                        ObjProgPartTerm.AcademicYearId = (Convert.ToString(dt.Rows[i]["AcademicYearId"])).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[i]["AcademicYearId"]);
                        ObjProgPartTerm.ProgrammePartTermId = (Convert.ToString(dt.Rows[i]["ProgrammePartTermId"])).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[i]["ProgrammePartTermId"]);
                        ObjProgPartTerm.ProgrammePartTermName = (Convert.ToString(dt.Rows[i]["ProgrammePartTermName"])).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["ProgrammePartTermName"]);
                        ObjProgPartTerm.PartTermShortName = (Convert.ToString(dt.Rows[i]["PartTermShortName"])).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["ProgrammePartTermName"]);
                        //ObjProgPartTerm.ExamPatternId = Convert.ToInt32(dt.Rows[i]["ExamPatternId"]);
                        //ObjProgPartTerm.ExaminationPatternName = Convert.ToString(dt.Rows[i]["ExaminationPatternName"]);
                        ObjProgPartTerm.MaxMarks = (Convert.ToString(dt.Rows[i]["MaxMarks"])).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[i]["MaxMarks"]);
                        ObjProgPartTerm.MinMarks = (Convert.ToString(dt.Rows[i]["MinMarks"])).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[i]["MinMarks"]);
                        ObjProgPartTerm.MinPapers = (Convert.ToString(dt.Rows[i]["MinPapers"])).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[i]["MinPapers"]);
                        ObjProgPartTerm.MaxPapers = (Convert.ToString(dt.Rows[i]["MaxPapers"])).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[i]["MaxPapers"]);
                        ObjProgPartTerm.IsCentrallyAdmission = (Convert.ToString(dt.Rows[i]["IsCentrallyAdmission"])).IsEmpty() ? false : Convert.ToBoolean(dt.Rows[i]["IsCentrallyAdmission"]);
                        ObjProgPartTerm.IsSeparatePassingHead = (Convert.ToString(dt.Rows[i]["IsSeparatePassingHead"])).IsEmpty() ? false : Convert.ToBoolean(dt.Rows[i]["IsSeparatePassingHead"]);
                        ObjProgPartTerm.IsSepratePassHeadSts = (Convert.ToString(dt.Rows[i]["IsSepratePassHeadSts"])).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["IsSepratePassHeadSts"]);
                        ObjProgPartTerm.IsActive = (Convert.ToString(dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(dt.Rows[i]["IsActive"]);
                        ObjProgPartTerm.IsActiveSts = (Convert.ToString(dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["IsActiveSts"]);
                        ObjProgPartTerm.IsDeleted = (Convert.ToString(dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(dt.Rows[i]["IsDeleted"]); ;
                        ObjProgPartTerm.Intake = (Convert.ToString(dt.Rows[i]["Intake"])).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[i]["Intake"]);
                        ObjProgPartTerm.IsForAdmission = (Convert.ToString(dt.Rows[i]["IsForAdmission"])).IsEmpty() ? false : Convert.ToBoolean(dt.Rows[i]["IsForAdmission"]);
                        ObjProgPartTerm.IsForApplication = (Convert.ToString(dt.Rows[i]["IsForApplication"])).IsEmpty() ? false : Convert.ToBoolean(dt.Rows[i]["IsForApplication"]);
						ObjProgPartTerm.InstanceName = (Convert.ToString(dt.Rows[i]["InstanceName"])).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["InstanceName"]);
                        //ObjProgInst.CreatedBy = Convert.ToInt64(dt.Rows[i]["CreatedBy"]);
                        //ObjProgInst.CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]);
                        //ObjProgInst.ModifiedBy = Convert.ToInt64(dt.Rows[i]["ModifiedBy"]);
                        //ObjProgInst.ModifiedOn = Convert.ToDateTime(dt.Rows[i]["ModifiedOn"]);
                        ObjProgPartTerm.AdmissionYearId = (Convert.ToString(dt.Rows[i]["AdmissionYearId"])).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[i]["AdmissionYearId"]);
                        
                        count = count + 1;
                        ObjProgPartTerm.IndexId = count;

                        ObjLstProgInstPartTerm.Add(ObjProgPartTerm);

                    }
                    return Return.returnHttp("200", ObjLstProgInstPartTerm, null);
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

        #region ProgrammeInstancePartTermGet by academic year

        [HttpPost]
        public HttpResponseMessage IncProgrammeInstancePartTermGetByAcadYear(ProgrammeInstancePartTerm ObjIncPartTerm)
        {
            try
            {

                SqlCommand cmd = new SqlCommand("IncProgrammeInstancePartTermGetByAcadYear", con);
                cmd.CommandType = CommandType.StoredProcedure;
                
                SqlDataAdapter da = new SqlDataAdapter();
                
                cmd.Parameters.AddWithValue("@AcademicYearId", ObjIncPartTerm.AcademicYearId);
                cmd.Parameters.AddWithValue("@FacultyId", ObjIncPartTerm.InstituteId);
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);              
               
                List<ProgrammeInstancePartTerm> ObjLstProgInstPartTerm = new List<ProgrammeInstancePartTerm>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePartTerm ObjProgPartTerm = new ProgrammeInstancePartTerm();
                        ObjProgPartTerm.Id = Convert.ToInt64(dt.Rows[i]["Id"]);                          
                        ObjProgPartTerm.InstancePartTermName = (Convert.ToString(dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["InstancePartTermName"]);
                      
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

        #region ProgrammePartTermGetByPartId
        [HttpPost]
        public HttpResponseMessage ProgrammePartTermGetByPartId(ProgrammeInstancePartTerm ObjProgInstPartTerm)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("MstProgrammePartTermGetByPartId", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@PartId", ObjProgInstPartTerm.ProgrammePartId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<MstProgrammePartTerm> ObjLstProgPartTerm = new List<MstProgrammePartTerm>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        MstProgrammePartTerm ObjProgPartTerm = new MstProgrammePartTerm();
                        ObjProgPartTerm.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjProgPartTerm.FacultyId = Convert.ToInt32(dt.Rows[i]["FacultyId"]);
                        ObjProgPartTerm.FacultyName = Convert.ToString(dt.Rows[i]["FacultyName"]);
                        ObjProgPartTerm.ProgrammeName = Convert.ToString(dt.Rows[i]["ProgrammeName"]);
                        ObjProgPartTerm.ProgrammeId = Convert.ToInt32(dt.Rows[i]["ProgrammeId"]);
                        ObjProgPartTerm.PartTermName = Convert.ToString(dt.Rows[i]["PartTermName"]);
                        ObjProgPartTerm.PartTermShortName = Convert.ToString(dt.Rows[i]["PartTermShortName"]);
                        ObjProgPartTerm.SequenceNo = Convert.ToInt32(dt.Rows[i]["SequenceNo"]);
                        ObjProgPartTerm.IsActive = Convert.ToBoolean(dt.Rows[i]["IsActive"]);
                        ObjProgPartTerm.IsActiveSts = Convert.ToString(dt.Rows[i]["IsActiveSts"]);
                        ObjProgPartTerm.IsDeleted = Convert.ToBoolean(dt.Rows[i]["IsDeleted"]);
                        //ObjProgPartTerm.CreatedBy = Convert.ToInt64(dt.Rows[i]["CreatedBy"]);
                        //ObjProgPartTerm.CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]);
                        //ObjProgPartTerm.ModifiedBy = Convert.ToInt64(dt.Rows[i]["ModifiedBy"]);
                        //ObjProgPartTerm.ModifiedOn = Convert.ToDateTime(dt.Rows[i]["ModifiedOn"]);

                        ObjLstProgPartTerm.Add(ObjProgPartTerm);

                    }
                    return Return.returnHttp("200", ObjLstProgPartTerm, null);
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

        #region PreProgPartTermGetByFacultyId
        [HttpPost]
        public HttpResponseMessage PreProgPartTermGetByFacultyId(ProgrammeInstancePartTerm ObjProgInstPartTerm)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("PreIncProgrammeInstancePartTermGet", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@FacultyId", ObjProgInstPartTerm.FacultyId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<ProgrammeInstancePartTerm> ObjLstProgInstPartTerm = new List<ProgrammeInstancePartTerm>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePartTerm ObjProgPartTerm = new ProgrammeInstancePartTerm();
                        ObjProgPartTerm.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjProgPartTerm.FacultyId = Convert.ToInt32(dt.Rows[i]["FacultyId"]);
                        ObjProgPartTerm.FacultyName = Convert.ToString(dt.Rows[i]["FacultyName"]);
                        ObjProgPartTerm.ProgrammeName = Convert.ToString(dt.Rows[i]["ProgrammeName"]);
                        ObjProgPartTerm.ProgrammeCode = Convert.ToString(dt.Rows[i]["ProgrammeCode"]);
                        ObjProgPartTerm.ProgrammeInstanceId = Convert.ToInt64(dt.Rows[i]["ProgrammeInstanceId"]);
                        ObjProgPartTerm.ProgrammeId = Convert.ToInt32(dt.Rows[i]["ProgrammeId"]);
                        ObjProgPartTerm.SpecialisationId = Convert.ToInt32(dt.Rows[i]["SpecialisationId"]);
                        ObjProgPartTerm.BranchName = Convert.ToString(dt.Rows[i]["BranchName"]);
                        ObjProgPartTerm.PartName = Convert.ToString(dt.Rows[i]["PartName"]);
                        ObjProgPartTerm.PartShortName = Convert.ToString(dt.Rows[i]["PartShortName"]);
                        ObjProgPartTerm.ProgrammePartId = Convert.ToInt32(dt.Rows[i]["ProgrammePartId"]);
                        ObjProgPartTerm.AcademicYearId = Convert.ToInt32(dt.Rows[i]["AcademicYearId"]);
                        ObjProgPartTerm.AcademicYearCode = Convert.ToString(dt.Rows[i]["AcademicYearCode"]);
                        ObjProgPartTerm.ProgrammePartTermId = Convert.ToInt32(dt.Rows[i]["ProgrammePartTermId"]);
                        ObjProgPartTerm.ProgrammePartTermName = Convert.ToString(dt.Rows[i]["ProgrammePartTermName"]);
                        ObjProgPartTerm.PartTermShortName = Convert.ToString(dt.Rows[i]["PartTermShortName"]);
                        //ObjProgPartTerm.ExamPatternId = Convert.ToInt32(dt.Rows[i]["ExamPatternId"]);
                        //ObjProgPartTerm.ExaminationPatternName = Convert.ToString(dt.Rows[i]["ExaminationPatternName"]);
                        var MaxMarks1 = dt.Rows[i]["MaxMarks"];
                        if (MaxMarks1 is DBNull)
                        {
                            ObjProgPartTerm.MaxMarks1 = null;
                        }
                        else
                        {
                            ObjProgPartTerm.MaxMarks1 = Convert.ToInt32(dt.Rows[i]["MaxMarks"]);
                        }
                        var MinMarks1 = dt.Rows[i]["MinMarks"];
                        if (MinMarks1 is DBNull)
                        {
                            ObjProgPartTerm.MinMarks1 = null;
                        }
                        else
                        {
                            ObjProgPartTerm.MinMarks1 = Convert.ToInt32(dt.Rows[i]["MinMarks"]);
                        }
                        //ObjProgPartTerm.MaxMarks = Convert.ToInt32(dt.Rows[i]["MaxMarks"]);
                        //ObjProgPartTerm.MinMarks = Convert.ToInt32(dt.Rows[i]["MinMarks"]);
                        ObjProgPartTerm.MinPapers = Convert.ToInt32(dt.Rows[i]["MinPapers"]);
                        ObjProgPartTerm.MaxPapers = Convert.ToInt32(dt.Rows[i]["MaxPapers"]);
                        var Intake = dt.Rows[i]["Intake"];
                        if (Intake is DBNull) { }
                        else { ObjProgPartTerm.Intake = Convert.ToInt32(dt.Rows[i]["Intake"]); }
                        var IsForApplication = dt.Rows[i]["IsForApplication"];
                        if (IsForApplication is DBNull) { }
                        else { ObjProgPartTerm.IsForApplication = Convert.ToBoolean(dt.Rows[i]["IsForApplication"]); }
                        var IsForAdmission = dt.Rows[i]["IsForAdmission"];
                        if (IsForAdmission is DBNull) { }
                        else { ObjProgPartTerm.IsForAdmission = Convert.ToBoolean(dt.Rows[i]["IsForAdmission"]); }
                        var IsCentrallyAdmission = dt.Rows[i]["IsCentrallyAdmission"];
                        if (IsCentrallyAdmission is DBNull) { }
                        else { ObjProgPartTerm.IsCentrallyAdmission = Convert.ToBoolean(dt.Rows[i]["IsCentrallyAdmission"]); }

                        ObjProgPartTerm.IsSeparatePassingHead = Convert.ToBoolean(dt.Rows[i]["IsSeparatePassingHead"]);
                        ObjProgPartTerm.IsSepratePassHeadSts = Convert.ToString(dt.Rows[i]["IsSepratePassHeadSts"]);
                        ObjProgPartTerm.IsActive = Convert.ToBoolean(dt.Rows[i]["IsActive"]);
                        ObjProgPartTerm.IsActiveSts = Convert.ToString(dt.Rows[i]["IsActiveSts"]);
                        ObjProgPartTerm.IsDeleted = Convert.ToBoolean(dt.Rows[i]["IsDeleted"]);
                        //ObjProgInst.CreatedBy = Convert.ToInt64(dt.Rows[i]["CreatedBy"]);
                        //ObjProgInst.CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]);
                        //ObjProgInst.ModifiedBy = Convert.ToInt64(dt.Rows[i]["ModifiedBy"]);
                        //ObjProgInst.ModifiedOn = Convert.ToDateTime(dt.Rows[i]["ModifiedOn"]);

                        ObjLstProgInstPartTerm.Add(ObjProgPartTerm);

                    }
                    return Return.returnHttp("200", ObjLstProgInstPartTerm, null);
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

        #region PostProgPartTermGetByFacultyId
        [HttpPost]
        public HttpResponseMessage PostProgPartTermGetByFacultyId(ProgrammeInstancePartTerm ObjProgInstPartTerm)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("PostIncProgrammeInstancePartTermGetByFacId", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                //da.SelectCommand.Parameters.AddWithValue("@FacultyId", ObjProgInstPartTerm.FacultyId);
                da.SelectCommand.Parameters.AddWithValue("@InstituteId", ObjProgInstPartTerm.InstituteId);
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
                    return Return.returnHttp("200", "No Record Found", null);
                }

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion


        #region PostProgPartTermGetByFaculty
        [HttpPost]
        public HttpResponseMessage PostProgPartTermGetByFaculty(ProgrammeInstancePartTerm ObjProgInstPartTerm)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("PostIncProgrammeInstancePartTermGetByFac", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@FacultyId", ObjProgInstPartTerm.FacultyId);
                da.SelectCommand.Parameters.AddWithValue("@InstituteId", ObjProgInstPartTerm.InstituteId);
                da.SelectCommand.Parameters.AddWithValue("@AcademicYearId", ObjProgInstPartTerm.AcademicYearId);
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



        #region PostProgPartTermGetByFacultyOnly
        [HttpPost]
        public HttpResponseMessage PostProgPartTermGetByFacultyOnly(ProgrammeInstancePartTerm ObjProgInstPartTerm)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("PostIncProgrammeInstancePartTermGetByFac", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@FacultyId", ObjProgInstPartTerm.FacultyId);
                //da.SelectCommand.Parameters.AddWithValue("@InstituteId", ObjProgInstPartTerm.InstituteId);
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
                    return Return.returnHttp("200", "No Record Found", null);
                }

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion



        #region PostGetFullProgNameByProgPTID
        [HttpPost]
        public HttpResponseMessage PostGetFullProgNameByProgPTID(ProgrammeInstancePartTerm ObjProgInstPartTerm)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("PostGetFullProgNameByProgPTID", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@IncProgInstancePartTermId", ObjProgInstPartTerm.IncProgInstancePartTermId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<ProgrammeInstancePartTerm> ObjLstProgInstPartTerm = new List<ProgrammeInstancePartTerm>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePartTerm ObjProgPartTerm = new ProgrammeInstancePartTerm();
                        //ObjProgPartTerm.IncProgInstancePartTermId = Convert.ToInt32(dt.Rows[i]["IncProgInstancePartTermId"]);
                        ObjProgPartTerm.ProgrammeName = Convert.ToString(dt.Rows[i]["ProgrammeName"]);
                        ObjProgPartTerm.BranchName = Convert.ToString(dt.Rows[i]["BranchName"]);
                        ObjProgPartTerm.AcademicYearCode = Convert.ToString(dt.Rows[i]["AcademicYearCode"]);


                        ObjLstProgInstPartTerm.Add(ObjProgPartTerm);

                    }
                    return Return.returnHttp("200", ObjLstProgInstPartTerm, null);
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

        #region ProgrammePartTermGet
        [HttpPost]
        public HttpResponseMessage ProgrammePartTermGet()
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
                SqlDataAdapter da = new SqlDataAdapter("MstProgrammePartTermGet", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                DataTable dt = new DataTable();
                da.Fill(dt);

                List<MstProgrammePartTerm> ObjLstProgPartTerm = new List<MstProgrammePartTerm>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        MstProgrammePartTerm ObjProgPartTerm = new MstProgrammePartTerm();
                        ObjProgPartTerm.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjProgPartTerm.FacultyId = Convert.ToInt32(dt.Rows[i]["FacultyId"]);
                        ObjProgPartTerm.FacultyName = Convert.ToString(dt.Rows[i]["FacultyName"]);
                        ObjProgPartTerm.ProgrammeName = Convert.ToString(dt.Rows[i]["ProgrammeName"]);
                        ObjProgPartTerm.ProgrammeId = Convert.ToInt32(dt.Rows[i]["ProgrammeId"]);
                        ObjProgPartTerm.PartTermName = Convert.ToString(dt.Rows[i]["PartTermName"]);
                        ObjProgPartTerm.PartTermShortName = Convert.ToString(dt.Rows[i]["PartTermShortName"]);
                        ObjProgPartTerm.SequenceNo = Convert.ToInt32(dt.Rows[i]["SequenceNo"]);
                        ObjProgPartTerm.IsActive = Convert.ToBoolean(dt.Rows[i]["IsActive"]);
                        ObjProgPartTerm.IsActiveSts = Convert.ToString(dt.Rows[i]["IsActiveSts"]);
                        ObjProgPartTerm.IsDeleted = Convert.ToBoolean(dt.Rows[i]["IsDeleted"]);
                        //ObjProgPartTerm.CreatedBy = Convert.ToInt64(dt.Rows[i]["CreatedBy"]);
                        //ObjProgPartTerm.CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]);
                        //ObjProgPartTerm.ModifiedBy = Convert.ToInt64(dt.Rows[i]["ModifiedBy"]);
                        //ObjProgPartTerm.ModifiedOn = Convert.ToDateTime(dt.Rows[i]["ModifiedOn"]);

                        ObjLstProgPartTerm.Add(ObjProgPartTerm);

                    }
                    return Return.returnHttp("200", ObjLstProgPartTerm, null);
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

        #region PreProgrammeInstancePartTermGet

        [HttpPost]
        public HttpResponseMessage PreProgrammeInstancePartTermGet(ProgrammeInstancePartTerm ObjProgInstPartTerm)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("PreIncProgramInstancePartTermGet", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                //da.SelectCommand.Parameters.AddWithValue("@FacultyId", ObjProgInstPartTerm.FacultyId);
                //da.SelectCommand.Parameters.AddWithValue("@ProgrammeId", ObjProgInstPartTerm.ProgrammeId);
                //da.SelectCommand.Parameters.AddWithValue("@SpecialisationId", ObjProgInstPartTerm.SpecialisationId);
                //da.SelectCommand.Parameters.AddWithValue("@AcademicYearId", ObjProgInstPartTerm.AcademicYearId);
                //da.SelectCommand.Parameters.AddWithValue("@ProgrammePartId", ObjProgInstPartTerm.ProgrammePartId);
                da.SelectCommand.Parameters.AddWithValue("@ProgrammePartTermId", ObjProgInstPartTerm.Id);
                //  da.SelectCommand.Parameters.AddWithValue("@ProgrammeInstanceId", ObjProgInstPartTerm.ProgrammeInstanceId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<ProgrammeInstancePartTerm> ObjLstProgInstPartTerm = new List<ProgrammeInstancePartTerm>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePartTerm ObjProgPartTerm = new ProgrammeInstancePartTerm();
                        ObjProgPartTerm.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjProgPartTerm.InstanceName = Convert.ToString(dt.Rows[i]["InstanceName"]);
                        ObjProgPartTerm.PartTermName = Convert.ToString(dt.Rows[i]["PartTermName"]);
                        //ObjProgPartTerm.FacultyId = Convert.ToInt32(dt.Rows[i]["FacultyId"]);
                        //ObjProgPartTerm.FacultyName = Convert.ToString(dt.Rows[i]["FacultyName"]);
                        ObjProgPartTerm.ProgrammeName = Convert.ToString(dt.Rows[i]["ProgrammeName"]);
                        //ObjProgPartTerm.ProgrammeCode = Convert.ToString(dt.Rows[i]["ProgrammeCode"]);
                        //ObjProgPartTerm.ProgrammeInstanceId = Convert.ToInt64(dt.Rows[i]["ProgrammeInstanceId"]);
                        //ObjProgPartTerm.ProgrammeId = Convert.ToInt32(dt.Rows[i]["ProgrammeId"]);
                        //ObjProgPartTerm.SpecialisationId = Convert.ToInt32(dt.Rows[i]["SpecialisationId"]);
                        //ObjProgPartTerm.BranchName = Convert.ToString(dt.Rows[i]["BranchName"]);
                        ObjProgPartTerm.PartName = Convert.ToString(dt.Rows[i]["PartName"]);
                        //ObjProgPartTerm.PartShortName = Convert.ToString(dt.Rows[i]["PartShortName"]);
                        //ObjProgPartTerm.ProgrammePartId = Convert.ToInt32(dt.Rows[i]["ProgrammePartId"]);
                        //ObjProgPartTerm.AcademicYearId = Convert.ToInt32(dt.Rows[i]["AcademicYearId"]);
                        //ObjProgPartTerm.AcademicYearCode = Convert.ToString(dt.Rows[i]["AcademicYearCode"]);
                        //ObjProgPartTerm.ProgrammePartTermId = Convert.ToInt32(dt.Rows[i]["ProgrammePartTermId"]);
                        ObjProgPartTerm.ProgrammePartTermName = Convert.ToString(dt.Rows[i]["PartTermName"]);
                        //ObjProgPartTerm.PartTermShortName = Convert.ToString(dt.Rows[i]["PartTermShortName"]);
                        ////ObjProgPartTerm.ExamPatternId = Convert.ToInt32(dt.Rows[i]["ExamPatternId"]);
                        ////ObjProgPartTerm.ExaminationPatternName = Convert.ToString(dt.Rows[i]["ExaminationPatternName"]);
                        //ObjProgPartTerm.MaxMarks = Convert.ToInt32(dt.Rows[i]["MaxMarks"]);
                        //ObjProgPartTerm.MinMarks = Convert.ToInt32(dt.Rows[i]["MinMarks"]);
                        //ObjProgPartTerm.MinPapers = Convert.ToInt32(dt.Rows[i]["MinPapers"]);
                        //ObjProgPartTerm.MaxPapers = Convert.ToInt32(dt.Rows[i]["MaxPapers"]);
                        //ObjProgPartTerm.IsSeparatePassingHead = Convert.ToBoolean(dt.Rows[i]["IsSeparatePassingHead"]);
                        //ObjProgPartTerm.IsSepratePassHeadSts = Convert.ToString(dt.Rows[i]["IsSepratePassHeadSts"]);
                        //ObjProgPartTerm.IsActive = Convert.ToBoolean(dt.Rows[i]["IsActive"]);
                        //ObjProgPartTerm.IsActiveSts = Convert.ToString(dt.Rows[i]["IsActiveSts"]);
                        //ObjProgPartTerm.IsDeleted = Convert.ToBoolean(dt.Rows[i]["IsDeleted"]);
                        //ObjProgInst.CreatedBy = Convert.ToInt64(dt.Rows[i]["CreatedBy"]);
                        //ObjProgInst.CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]);
                        //ObjProgInst.ModifiedBy = Convert.ToInt64(dt.Rows[i]["ModifiedBy"]);
                        //ObjProgInst.ModifiedOn = Convert.ToDateTime(dt.Rows[i]["ModifiedOn"]);

                        ObjLstProgInstPartTerm.Add(ObjProgPartTerm);

                    }
                    return Return.returnHttp("200", ObjLstProgInstPartTerm, null);
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

        #region ProgrammeInstancePartTermAdd
        [HttpPost]
        public HttpResponseMessage ProgrammeInstancePartTermAdd(ProgrammeInstancePartTerm ObjProgrammeInstPartTerm)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPartTerm.FacultyId)))
                {
                    return Return.returnHttp("201", "Please select faculty", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPartTerm.ProgrammeId)))
                {
                    return Return.returnHttp("201", "Please select programme", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPartTerm.SpecialisationId)))
                {
                    return Return.returnHttp("201", "Please select Specialisation", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPartTerm.AcademicYearId)))
                {
                    return Return.returnHttp("201", "Please select AcademicYear", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPartTerm.ProgrammePartId)))
                {
                    return Return.returnHttp("201", "Please select Programme Part", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPartTerm.ProgrammePartTermId)))
                {
                    return Return.returnHttp("201", "Please select Programme Part term", null);
                }
                else if (validation.validateDigit(Convert.ToString(ObjProgrammeInstPartTerm.MaxMarks)) == false)
                {
                    return Return.returnHttp("201", "Please enter Maximum marks", null);
                }
                else if ((validation.validateDigit(Convert.ToString(ObjProgrammeInstPartTerm.MinMarks)) == false) || (ObjProgrammeInstPartTerm.MinMarks > ObjProgrammeInstPartTerm.MaxMarks))
                {
                    return Return.returnHttp("201", "Please enter Minimum marks and respective of Maximum Marks", null);
                }
                else if (validation.validateDigit(Convert.ToString(ObjProgrammeInstPartTerm.MaxPapers)) == false)
                {
                    return Return.returnHttp("201", "Please enter Maximum papers", null);
                }
                else if ((validation.validateDigit(Convert.ToString(ObjProgrammeInstPartTerm.MinPapers)) == false) || (ObjProgrammeInstPartTerm.MinPapers > ObjProgrammeInstPartTerm.MaxPapers))
                {
                    return Return.returnHttp("201", "Please enter Minimum papers and respective of Maximum papers", null);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("IncProgrammeInstancePartTermAdd", con);

                    ProgrammeInstancePartTerm ObjProgPartTerm = new ProgrammeInstancePartTerm();


                    //ObjProgPart.ProgrammeInstanceId = JsonData.ProgrammeInstanceId;
                    ObjProgPartTerm.AdmissionYearId = ObjProgrammeInstPartTerm.AdmissionYearId; //By Mohini on 27Aug2022
                   // ObjProgPartTerm.ProgrammeId = ObjProgrammeInstPartTerm.ProgrammeId;
                    ObjProgPartTerm.SpecialisationId = ObjProgrammeInstPartTerm.SpecialisationId;
                    ObjProgPartTerm.AcademicYearId = ObjProgrammeInstPartTerm.AcademicYearId;
                    ObjProgPartTerm.ProgrammePartId = ObjProgrammeInstPartTerm.ProgrammePartId;
                    ObjProgPartTerm.ProgrammeInstanceId = ObjProgrammeInstPartTerm.ProgrammeInstanceId;
                   // ObjProgPartTerm.ProgrammeInstancePartId = ObjProgrammeInstPartTerm.ProgrammeInstancePartId;
                    ObjProgPartTerm.ProgrammePartTermId = ObjProgrammeInstPartTerm.ProgrammePartTermId;
                    ObjProgPartTerm.FacultyId = ObjProgrammeInstPartTerm.FacultyId;
                    ObjProgPartTerm.MaxMarks = ObjProgrammeInstPartTerm.MaxMarks;
                    ObjProgPartTerm.MinMarks = ObjProgrammeInstPartTerm.MinMarks;
                    ObjProgPartTerm.MinPapers = ObjProgrammeInstPartTerm.MinPapers;
                    ObjProgPartTerm.MaxPapers = ObjProgrammeInstPartTerm.MaxPapers;
                    ObjProgPartTerm.Intake = ObjProgrammeInstPartTerm.Intake;
                    ObjProgPartTerm.IsSeparatePassingHead = ObjProgrammeInstPartTerm.IsSeparatePassingHead;
                    ObjProgPartTerm.IsCentrallyAdmission = ObjProgrammeInstPartTerm.IsCentrallyAdmission;
                    ObjProgPartTerm.IsForApplication = ObjProgrammeInstPartTerm.IsForApplication;
                    ObjProgPartTerm.IsForAdmission = ObjProgrammeInstPartTerm.IsForAdmission;

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@AdmissionYearId", ObjProgPartTerm.AdmissionYearId);
                   // cmd.Parameters.AddWithValue("@ProgrammeId", ObjProgPartTerm.ProgrammeId);
                    cmd.Parameters.AddWithValue("@AcademicYearId", ObjProgPartTerm.AcademicYearId);
                    cmd.Parameters.AddWithValue("@SpecialisationId", ObjProgPartTerm.SpecialisationId);
                    cmd.Parameters.AddWithValue("@ProgrammePartId", ObjProgPartTerm.ProgrammePartId);
                    cmd.Parameters.AddWithValue("@ProgrammeInstanceId", ObjProgPartTerm.ProgrammeInstanceId);
                    //cmd.Parameters.AddWithValue("@ProgrammeInstancePartId", ObjProgPartTerm.ProgrammeInstancePartId);
                    cmd.Parameters.AddWithValue("@ProgrammePartTermId", ObjProgPartTerm.ProgrammePartTermId);
                    cmd.Parameters.AddWithValue("@FacultyId", ObjProgPartTerm.FacultyId);
                    cmd.Parameters.AddWithValue("@MaxMarks", ObjProgPartTerm.MaxMarks);
                    cmd.Parameters.AddWithValue("@MinMarks", ObjProgPartTerm.MinMarks);
                    cmd.Parameters.AddWithValue("@MinPapers", ObjProgPartTerm.MinPapers);
                    cmd.Parameters.AddWithValue("@MaxPapers", ObjProgPartTerm.MaxPapers);
                    cmd.Parameters.AddWithValue("@Intake", ObjProgPartTerm.Intake);
                    cmd.Parameters.AddWithValue("@IsCentrallyAdmission", ObjProgPartTerm.IsCentrallyAdmission);
                    cmd.Parameters.AddWithValue("@IsForApplication", ObjProgPartTerm.IsForApplication);
                    cmd.Parameters.AddWithValue("@IsForAdmission", ObjProgPartTerm.IsForAdmission);
                    cmd.Parameters.AddWithValue("@IsSeparatePassingHead", ObjProgPartTerm.IsSeparatePassingHead);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    con.Close();
                    if (strMessage.Equals("TRUE"))
                    {
                        return Return.returnHttp("200", "Your data has been added successfully.", null);
                    }
                    else
                    {
                        return Return.returnHttp("201", strMessage, null);
                    }
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region PreProgrammeInstancePartTermAdd
        [HttpPost]
        public HttpResponseMessage PreProgrammeInstancePartTermAdd(ProgrammeInstancePartTerm ObjProgrammeInstPartTerm)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPartTerm.FacultyId)))
                {
                    return Return.returnHttp("201", "Please select faculty", null);
                }
                //else if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPartTerm.ProgrammeId)))
                //{
                //    return Return.returnHttp("201", "Please select programme", null);
                //}
                else if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPartTerm.SpecialisationId)))
                {
                    return Return.returnHttp("201", "Please select Specialisation", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPartTerm.AcademicYearId)))
                {
                    return Return.returnHttp("201", "Please select AcademicYear", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPartTerm.ProgrammePartId)))
                {
                    return Return.returnHttp("201", "Please select Programme Part", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPartTerm.ProgrammePartTermId)))
                {
                    return Return.returnHttp("201", "Please select Programme Part term", null);
                }
                //else if (validation.validateDigit(Convert.ToString(ObjProgrammeInstPartTerm.MaxMarks)) == false)
                //{
                //    return Return.returnHttp("201", "Please enter Maximum marks", null);
                //}
                //else if ((validation.validateDigit(Convert.ToString(ObjProgrammeInstPartTerm.MinMarks)) == false) || (ObjProgrammeInstPartTerm.MinMarks > ObjProgrammeInstPartTerm.MaxMarks))
                //{
                //    return Return.returnHttp("201", "Please enter Minimum marks and respective of Maximum Marks", null);
                //}
                else if (validation.validateDigit(Convert.ToString(ObjProgrammeInstPartTerm.MaxPapers)) == false)
                {
                    return Return.returnHttp("201", "Please enter Maximum papers", null);
                }
                else if ((validation.validateDigit(Convert.ToString(ObjProgrammeInstPartTerm.MinPapers)) == false) || (ObjProgrammeInstPartTerm.MinPapers > ObjProgrammeInstPartTerm.MaxPapers))
                {
                    return Return.returnHttp("201", "Please enter Minimum papers and respective of Maximum papers", null);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("PreIncProgramInstancePartTermAdd", con);

                    ProgrammeInstancePartTerm ObjProgPartTerm = new ProgrammeInstancePartTerm();


                    //ObjProgPart.ProgrammeInstanceId = JsonData.ProgrammeInstanceId;
                    ObjProgPartTerm.ProgrammeId = ObjProgrammeInstPartTerm.ProgrammeId;
                    ObjProgPartTerm.SpecialisationId = ObjProgrammeInstPartTerm.SpecialisationId;
                    ObjProgPartTerm.AcademicYearId = ObjProgrammeInstPartTerm.AcademicYearId;
                    ObjProgPartTerm.ProgrammePartId = ObjProgrammeInstPartTerm.ProgrammePartId;
                    ObjProgPartTerm.ProgrammeInstancePartId = ObjProgrammeInstPartTerm.ProgrammeInstancePartId;
                    ObjProgPartTerm.ProgrammePartTermId = ObjProgrammeInstPartTerm.ProgrammePartTermId;
                    ObjProgPartTerm.FacultyId = ObjProgrammeInstPartTerm.FacultyId;
                    ObjProgPartTerm.MaxMarks1 = ObjProgrammeInstPartTerm.MaxMarks1;
                    ObjProgPartTerm.MinMarks1 = ObjProgrammeInstPartTerm.MinMarks1;
                    ObjProgPartTerm.MinPapers = ObjProgrammeInstPartTerm.MinPapers;
                    ObjProgPartTerm.MaxPapers = ObjProgrammeInstPartTerm.MaxPapers;
                    ObjProgPartTerm.Intake = ObjProgrammeInstPartTerm.Intake;
                    ObjProgPartTerm.IsSeparatePassingHead = ObjProgrammeInstPartTerm.IsSeparatePassingHead;
                    ObjProgPartTerm.IsForApplication = ObjProgrammeInstPartTerm.IsForApplication;
                    ObjProgPartTerm.IsForAdmission = ObjProgrammeInstPartTerm.IsForAdmission;
                    ObjProgPartTerm.IsForAdmission = ObjProgrammeInstPartTerm.IsForAdmission;
                    ObjProgPartTerm.IsCentrallyAdmission = ObjProgrammeInstPartTerm.IsCentrallyAdmission;

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ProgrammeId", ObjProgPartTerm.ProgrammeId);
                    cmd.Parameters.AddWithValue("@AcademicYearId", ObjProgPartTerm.AcademicYearId);
                    cmd.Parameters.AddWithValue("@SpecialisationId", ObjProgPartTerm.SpecialisationId);
                    cmd.Parameters.AddWithValue("@ProgrammePartId", ObjProgPartTerm.ProgrammePartId);
                    // cmd.Parameters.AddWithValue("@ProgrammeInstancePartId", ObjProgPartTerm.ProgrammeInstancePartId);
                    cmd.Parameters.AddWithValue("@ProgrammePartTermId", ObjProgPartTerm.ProgrammePartTermId);
                    cmd.Parameters.AddWithValue("@FacultyId", ObjProgPartTerm.FacultyId);
                    if (ObjProgPartTerm.MaxMarks1 == null)
                        cmd.Parameters.AddWithValue("@MaxMarks", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@MaxMarks", ObjProgPartTerm.MaxMarks1);
                    if (ObjProgPartTerm.MinMarks1 == null)
                        cmd.Parameters.AddWithValue("@MinMarks", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@MinMarks", ObjProgPartTerm.MinMarks1);
                    cmd.Parameters.AddWithValue("@MinPapers", ObjProgPartTerm.MinPapers);
                    cmd.Parameters.AddWithValue("@MaxPapers", ObjProgPartTerm.MaxPapers);
                    if (ObjProgPartTerm.Intake == null)
                        cmd.Parameters.AddWithValue("@Intake", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@Intake", ObjProgPartTerm.Intake);
                    cmd.Parameters.AddWithValue("@IsSeparatePassingHead", ObjProgPartTerm.IsSeparatePassingHead);
                    cmd.Parameters.AddWithValue("@IsForApplication", ObjProgPartTerm.IsForApplication);
                    cmd.Parameters.AddWithValue("@IsForAdmission", ObjProgPartTerm.IsForAdmission);
                    cmd.Parameters.AddWithValue("@IsCentrallyAdmission", ObjProgPartTerm.IsCentrallyAdmission);

                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@ProgInstancePartTerm", SqlDbType.BigInt);
                    cmd.Parameters["@ProgInstancePartTerm"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@ProgInstanceName", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@ProgInstanceName"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@ProgName", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@ProgName"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@ProgPartName", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@ProgPartName"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@ProgPartTermName", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@ProgPartTermName"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    string ProgInstancePartTerm = Convert.ToString(cmd.Parameters["@ProgInstancePartTerm"].Value);
                    string ProgInstanceName = Convert.ToString(cmd.Parameters["@ProgInstanceName"].Value);
                    string ProgrammeName = Convert.ToString(cmd.Parameters["@ProgName"].Value);
                    string ProgrammePartName = Convert.ToString(cmd.Parameters["@ProgPartName"].Value);
                    string ProgrammePartTermName = Convert.ToString(cmd.Parameters["@ProgPartTermName"].Value);
                    con.Close();
                    if (strMessage.Equals("TRUE"))
                    {
                        strMessage = "Data has been saved successfully";
                    }
                    //else
                    //{
                    //    return Return.returnHttp("201", strMessage, null);
                    //}
                    PreProgInstance ObjPPI = new PreProgInstance();

                    ObjPPI.strMessage = strMessage;
                    if (ProgInstancePartTerm == "")
                        ObjPPI.ProgInstancePartTerm = 0;
                    else
                        ObjPPI.ProgInstancePartTerm = Convert.ToInt64(ProgInstancePartTerm.ToString());
                    if (ProgInstanceName == "")
                        ObjPPI.ProgInstanceName = null;
                    else
                        ObjPPI.ProgInstanceName = ProgInstanceName;
                    if (ProgrammeName == "")
                        ObjPPI.ProgrammeName = null;
                    else
                        ObjPPI.ProgrammeName = ProgrammeName;
                    if (ProgrammePartName == "")
                        ObjPPI.ProgrammePartName = null;
                    else
                        ObjPPI.ProgrammePartName = ProgrammePartName;
                    if (ProgrammePartTermName == "")
                        ObjPPI.ProgrammePartTermName = null;
                    else
                        ObjPPI.ProgrammePartTermName = ProgrammePartTermName;
                    return Return.returnHttp("200", ObjPPI, null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region ProgrammePartGetByProgrammeId
        [HttpPost]
        public HttpResponseMessage ProgrammePartGetByProgrammeId(ProgramInstance ObjProgInst)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("MstProgrammePartGetByProgrammeId", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@ProgrammeId", ObjProgInst.ProgrammeId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<ProgrammeInstancePart> ObjLstProgInstPart = new List<ProgrammeInstancePart>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePart ObjProgPart = new ProgrammeInstancePart();
                        ObjProgPart.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjProgPart.FacultyId = Convert.ToInt32(dt.Rows[i]["FacultyId"]);
                        ObjProgPart.FacultyName = Convert.ToString(dt.Rows[i]["FacultyName"]);
                        ObjProgPart.ProgrammeName = Convert.ToString(dt.Rows[i]["ProgrammeName"]);
                        ObjProgPart.PartShortName = Convert.ToString(dt.Rows[i]["PartShortName"]);
                        ObjLstProgInstPart.Add(ObjProgPart);

                    }
                    return Return.returnHttp("200", ObjLstProgInstPart, null);
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

        #region ProgrammePartGet
        [HttpPost]
        public HttpResponseMessage ProgrammePartGet()
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("MstProgrammePartGet", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                // da.SelectCommand.Parameters.AddWithValue("@ProgrammeId", ObjProgInst.ProgrammeId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<ProgrammeInstancePart> ObjLstProgInstPart = new List<ProgrammeInstancePart>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePart ObjProgPart = new ProgrammeInstancePart();
                        ObjProgPart.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjProgPart.FacultyId = Convert.ToInt32(dt.Rows[i]["FacultyId"]);
                        ObjProgPart.FacultyName = Convert.ToString(dt.Rows[i]["FacultyName"]);
                        ObjProgPart.ProgrammeName = Convert.ToString(dt.Rows[i]["ProgrammeName"]);
                        ObjProgPart.PartShortName = Convert.ToString(dt.Rows[i]["PartShortName"]);
                        ObjLstProgInstPart.Add(ObjProgPart);

                    }
                    return Return.returnHttp("200", ObjLstProgInstPart, null);
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

        #region ProgrammeInstancePartTermUpdate
        [HttpPost]
        public HttpResponseMessage ProgrammeInstancePartTermUpdate(ProgrammeInstancePartTerm ObjProgrammeInstPartTerm)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPartTerm.FacultyId)))
                {
                    return Return.returnHttp("201", "Please select faculty", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPartTerm.ProgrammeId)))
                {
                    return Return.returnHttp("201", "Please select programme", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPartTerm.SpecialisationId)))
                {
                    return Return.returnHttp("201", "Please select Specialisation", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPartTerm.AcademicYearId)))
                {
                    return Return.returnHttp("201", "Please select AcademicYear", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPartTerm.ProgrammePartId)))
                {
                    return Return.returnHttp("201", "Please select Programme Part", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPartTerm.ProgrammePartTermId)))
                {
                    return Return.returnHttp("201", "Please select Programme Part term", null);
                }
                else if (validation.validateDigit(Convert.ToString(ObjProgrammeInstPartTerm.MaxMarks)) == false)
                {
                    return Return.returnHttp("201", "Please enter Maximum marks", null);
                }
                else if ((validation.validateDigit(Convert.ToString(ObjProgrammeInstPartTerm.MinMarks)) == false) || (ObjProgrammeInstPartTerm.MinMarks > ObjProgrammeInstPartTerm.MaxMarks))
                {
                    return Return.returnHttp("201", "Please enter Minimum marks and respective of Maximum Marks", null);
                }
                else if (validation.validateDigit(Convert.ToString(ObjProgrammeInstPartTerm.MaxPapers)) == false)
                {
                    return Return.returnHttp("201", "Please enter Maximum papers", null);
                }
                else if ((validation.validateDigit(Convert.ToString(ObjProgrammeInstPartTerm.MinPapers)) == false) || (ObjProgrammeInstPartTerm.MinPapers > ObjProgrammeInstPartTerm.MaxPapers))
                {
                    return Return.returnHttp("201", "Please enter Minimum papers and respective of Maximum papers", null);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("IncProgrammeInstancePartTermEdit", con);

                    ProgrammeInstancePartTerm ObjProgPartTerm = new ProgrammeInstancePartTerm();


                    //ObjProgPart.ProgrammeInstanceId = JsonData.ProgrammeInstanceId;
                    ObjProgPartTerm.ProgrammeId = ObjProgrammeInstPartTerm.ProgrammeId;
                    ObjProgPartTerm.SpecialisationId = ObjProgrammeInstPartTerm.SpecialisationId;
                    ObjProgPartTerm.AcademicYearId = ObjProgrammeInstPartTerm.AcademicYearId;
                    ObjProgPartTerm.ProgrammePartId = ObjProgrammeInstPartTerm.ProgrammePartId;
                    ObjProgPartTerm.MaxMarks = ObjProgrammeInstPartTerm.MaxMarks;
                    ObjProgPartTerm.MinMarks = ObjProgrammeInstPartTerm.MinMarks;
                    ObjProgPartTerm.MinPapers = ObjProgrammeInstPartTerm.MinPapers;
                    ObjProgPartTerm.MaxPapers = ObjProgrammeInstPartTerm.MaxPapers;
                    ObjProgPartTerm.Intake = ObjProgrammeInstPartTerm.Intake;
                    ObjProgPartTerm.IsForApplication = ObjProgrammeInstPartTerm.IsForApplication;
                    ObjProgPartTerm.IsForAdmission = ObjProgrammeInstPartTerm.IsForAdmission;
                    ObjProgPartTerm.IsSeparatePassingHead = ObjProgrammeInstPartTerm.IsSeparatePassingHead;
                    ObjProgPartTerm.IsCentrallyAdmission = ObjProgrammeInstPartTerm.IsCentrallyAdmission;
                    ObjProgPartTerm.Id = ObjProgrammeInstPartTerm.Id;

                    cmd.CommandType = CommandType.StoredProcedure;

                    //cmd.Parameters.AddWithValue("@ProgrammeId", ObjProgPart.ProgrammeId);
                    //cmd.Parameters.AddWithValue("@AcademicYearId", ObjProgPart.AcademicYearId);
                    //cmd.Parameters.AddWithValue("@SpecialisationId", ObjProgPart.SpecialisationId);
                    //cmd.Parameters.AddWithValue("@ProgrammePartId", ObjProgPartTerm.ProgrammePartId);
                    cmd.Parameters.AddWithValue("@MaxMarks", ObjProgPartTerm.MaxMarks);
                    cmd.Parameters.AddWithValue("@MinMarks", ObjProgPartTerm.MinMarks);
                    cmd.Parameters.AddWithValue("@MinPapers", ObjProgPartTerm.MinPapers);
                    cmd.Parameters.AddWithValue("@MaxPapers", ObjProgPartTerm.MaxPapers);
                    cmd.Parameters.AddWithValue("@Intake", ObjProgPartTerm.Intake);
                    cmd.Parameters.AddWithValue("@IsForApplication", ObjProgPartTerm.IsForApplication);
                    cmd.Parameters.AddWithValue("@IsForAdmission", ObjProgPartTerm.IsForAdmission);
                    cmd.Parameters.AddWithValue("@IsCentrallyAdmission", ObjProgPartTerm.IsCentrallyAdmission);
                    cmd.Parameters.AddWithValue("@IsSeparatePassingHead", ObjProgPartTerm.IsSeparatePassingHead);
                    cmd.Parameters.AddWithValue("@Id", ObjProgPartTerm.Id);
                    //cmd.Parameters.AddWithValue("@ProgrammeInstancePartId", ObjProgPartTerm.ProgrammeInstancePartId);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    con.Close();
                    if (strMessage.Equals("TRUE"))
                    {
                        return Return.returnHttp("200", "Your data has been modified successfully.", null);
                    }
                    else
                    {
                        return Return.returnHttp("201", strMessage, null);
                    }
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region PreProgrammeInstancePartTermUpdate
        [HttpPost]
        public HttpResponseMessage PreProgrammeInstancePartTermUpdate(ProgrammeInstancePartTerm ObjProgrammeInstPartTerm)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPartTerm.FacultyId)))
                {
                    return Return.returnHttp("201", "Please select faculty", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPartTerm.ProgrammeId)))
                {
                    return Return.returnHttp("201", "Please select programme", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPartTerm.SpecialisationId)))
                {
                    return Return.returnHttp("201", "Please select Specialisation", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPartTerm.AcademicYearId)))
                {
                    return Return.returnHttp("201", "Please select AcademicYear", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPartTerm.ProgrammePartId)))
                {
                    return Return.returnHttp("201", "Please select Programme Part", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPartTerm.ProgrammePartTermId)))
                {
                    return Return.returnHttp("201", "Please select Programme Part term", null);
                }
                //else if (validation.validateDigit(Convert.ToString(ObjProgrammeInstPartTerm.MaxMarks)) == false)
                //{
                //    return Return.returnHttp("201", "Please enter Maximum marks", null);
                //}
                //else if ((validation.validateDigit(Convert.ToString(ObjProgrammeInstPartTerm.MinMarks)) == false) || (ObjProgrammeInstPartTerm.MinMarks > ObjProgrammeInstPartTerm.MaxMarks))
                //{
                //    return Return.returnHttp("201", "Please enter Minimum marks and respective of Maximum Marks", null);
                //}
                else if (validation.validateDigit(Convert.ToString(ObjProgrammeInstPartTerm.MaxPapers)) == false)
                {
                    return Return.returnHttp("201", "Please enter Maximum papers", null);
                }
                else if ((validation.validateDigit(Convert.ToString(ObjProgrammeInstPartTerm.MinPapers)) == false) || (ObjProgrammeInstPartTerm.MinPapers > ObjProgrammeInstPartTerm.MaxPapers))
                {
                    return Return.returnHttp("201", "Please enter Minimum papers and respective of Maximum papers", null);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("PreIncProgrammeInstancePartTermEdit", con);

                    ProgrammeInstancePartTerm ObjProgPartTerm = new ProgrammeInstancePartTerm();


                    //ObjProgPart.ProgrammeInstanceId = JsonData.ProgrammeInstanceId;
                    ObjProgPartTerm.ProgrammeId = ObjProgrammeInstPartTerm.ProgrammeId;
                    ObjProgPartTerm.SpecialisationId = ObjProgrammeInstPartTerm.SpecialisationId;
                    ObjProgPartTerm.AcademicYearId = ObjProgrammeInstPartTerm.AcademicYearId;
                    ObjProgPartTerm.ProgrammePartId = ObjProgrammeInstPartTerm.ProgrammePartId;
                    ObjProgPartTerm.MaxMarks1 = ObjProgrammeInstPartTerm.MaxMarks1;
                    ObjProgPartTerm.MinMarks1 = ObjProgrammeInstPartTerm.MinMarks1;
                    ObjProgPartTerm.MinPapers = ObjProgrammeInstPartTerm.MinPapers;
                    ObjProgPartTerm.MaxPapers = ObjProgrammeInstPartTerm.MaxPapers;
                    ObjProgPartTerm.Intake = ObjProgrammeInstPartTerm.Intake;
                    ObjProgPartTerm.IsSeparatePassingHead = ObjProgrammeInstPartTerm.IsSeparatePassingHead;
                    ObjProgPartTerm.IsForApplication = ObjProgrammeInstPartTerm.IsForApplication;
                    ObjProgPartTerm.IsForAdmission = ObjProgrammeInstPartTerm.IsForAdmission;
                    ObjProgPartTerm.IsCentrallyAdmission = ObjProgrammeInstPartTerm.IsCentrallyAdmission;

                    ObjProgPartTerm.Id = ObjProgrammeInstPartTerm.Id;
                    cmd.CommandType = CommandType.StoredProcedure;

                    //cmd.Parameters.AddWithValue("@ProgrammeId", ObjProgPart.ProgrammeId);
                    //cmd.Parameters.AddWithValue("@AcademicYearId", ObjProgPart.AcademicYearId);
                    //cmd.Parameters.AddWithValue("@SpecialisationId", ObjProgPart.SpecialisationId);
                    //cmd.Parameters.AddWithValue("@ProgrammePartId", ObjProgPartTerm.ProgrammePartId);
                    if (ObjProgPartTerm.MaxMarks1 == null)
                        cmd.Parameters.AddWithValue("@MaxMarks", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@MaxMarks", ObjProgPartTerm.MaxMarks1);
                    if (ObjProgPartTerm.MinMarks1 == null)
                        cmd.Parameters.AddWithValue("@MinMarks", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@MinMarks", ObjProgPartTerm.MinMarks1);
                    cmd.Parameters.AddWithValue("@MinPapers", ObjProgPartTerm.MinPapers);
                    cmd.Parameters.AddWithValue("@MaxPapers", ObjProgPartTerm.MaxPapers);
                    if (ObjProgPartTerm.Intake == null)
                        cmd.Parameters.AddWithValue("@Intake", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@Intake", ObjProgPartTerm.Intake);
                    cmd.Parameters.AddWithValue("@IsForApplication", ObjProgPartTerm.IsForApplication);
                    cmd.Parameters.AddWithValue("@IsForAdmission", ObjProgPartTerm.IsForAdmission);
                    cmd.Parameters.AddWithValue("@IsCentrallyAdmission", ObjProgPartTerm.IsCentrallyAdmission);
                    cmd.Parameters.AddWithValue("@IsSeparatePassingHead", ObjProgPartTerm.IsSeparatePassingHead);
                    cmd.Parameters.AddWithValue("@Id", ObjProgPartTerm.Id);
                    //cmd.Parameters.AddWithValue("@ProgrammeInstancePartId", ObjProgPartTerm.ProgrammeInstancePartId);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    con.Close();
                    if (strMessage.Equals("TRUE"))
                    {
                        return Return.returnHttp("200", "Data has been modified successfully", null);
                    }
                    else
                    {
                        return Return.returnHttp("201", strMessage, null);
                    }
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region ProgrammeInstancePartTermDelete
        [HttpPost]
        public HttpResponseMessage ProgrammeInstancePartTermDelete(ProgrammeInstancePartTerm ObjProgPartTerm)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                //ProgrammeInstancePart ObjProgPart = new ProgrammeInstancePart();

                //dynamic JsonData = JsonObj;
                //ObjProgPart.Id = JsonData.Id;

                SqlCommand cmd = new SqlCommand("IncProgrammeInstancePartTermDelete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", ObjProgPartTerm.Id);
                //cmd.Parameters.AddWithValue("@UserId", UserId);
                //cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 50000);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string StrMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                if (StrMessage.Equals("TRUE"))
                {
                    return Return.returnHttp("200", "Your data has been deleted successfully.", null);
                }
                else
                {
                    return Return.returnHttp("200", StrMessage, null);
                }

            }
            catch (Exception e)
            {
                return Return.returnHttp("200", e.Message, null);
            }

        }
        #endregion

        #region ProgrammeInstancePartTermIsActiveEnable
        [HttpPost]
        public HttpResponseMessage ProgrammeInstancePartTermIsActiveEnable(ProgrammeInstancePartTerm ObjProgPartTerm)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                //ProgrammeInstancePart ObjProgPart = new ProgrammeInstancePart();

                //dynamic JsonData = JsonObj;
                //ObjProgPart.Id = JsonData.Id;

                SqlCommand cmd = new SqlCommand("IncProgrammeInstancePartTermActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammePartTermId", ObjProgPartTerm.Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.AddWithValue("@Flag", "ProgramInstancePartTermIsActiveEnable");
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string StrMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                if (StrMessage.Equals("TRUE"))
                {
                    return Return.returnHttp("200", "Your data has been Modified successfully.", null);
                }
                else
                {
                    return Return.returnHttp("200", StrMessage, null);
                }

            }
            catch (Exception e)
            {
                return Return.returnHttp("200", e.Message, null);
            }
        }
        #endregion

        #region ProgrammeInstancePartTermIsActiveDisable
        [HttpPost]
        public HttpResponseMessage ProgrammeInstancePartTermIsActiveDisable(ProgrammeInstancePartTerm ObjProgPartTerm)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                //ProgrammeInstancePart ObjProgPart = new ProgrammeInstancePart();

                //dynamic JsonData = JsonObj;
                //ObjProgPart.Id = JsonData.Id;

                SqlCommand cmd = new SqlCommand("IncProgrammeInstancePartTermActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammePartTermId", ObjProgPartTerm.Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.AddWithValue("@Flag", "ProgramInstancePartTermIsActiveDisable");
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string StrMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                if (StrMessage.Equals("TRUE"))
                {
                    return Return.returnHttp("200", "Your data has been Modified successfully.", null);
                }
                else
                {
                    return Return.returnHttp("200", StrMessage, null);
                }

            }
            catch (Exception e)
            {
                return Return.returnHttp("200", e.Message, null);
            }

        }
        #endregion

        #region PostProgPartTermGetByFacIdandYearId
        [HttpPost]
        public HttpResponseMessage PostProgPartTermGetByFacIdandYearId(ProgrammeInstancePartTerm ObjProgInstPartTerm)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("PostProgPartTermGetByFacIdandYearId", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                //da.SelectCommand.Parameters.AddWithValue("@FacultyId", ObjProgInstPartTerm.FacultyId);
                da.SelectCommand.Parameters.AddWithValue("@InstituteId", ObjProgInstPartTerm.InstituteId);
                da.SelectCommand.Parameters.AddWithValue("@AcademicYearId", ObjProgInstPartTerm.AcademicYearId);
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
    }
}
