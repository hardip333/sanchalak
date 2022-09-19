﻿using System;
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
using MSUISApi.BAL;

namespace MSUISApi.Controllers
{
    public class MstProgramInstanceController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        Validation validation = new Validation();

        #region AcademicYearGet
        [HttpPost]
        public HttpResponseMessage AcademicYearGet()
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

        #region MstProgrammeBranchListGetByProgrammeId
        [HttpPost]
        public HttpResponseMessage MstProgrammeBranchListGetByProgrammeId(ProgramInstance ObjProg)
        {
            try
            {
                int ProgrammeId = Convert.ToInt32(ObjProg.ProgrammeId);
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter cmdda = new SqlDataAdapter("MstProgrammeBranchMapGetByProgrammeId", con);

                cmdda.SelectCommand.CommandType = CommandType.StoredProcedure;
                cmdda.SelectCommand.Parameters.AddWithValue("@ProgrammeId", ProgrammeId);

                DataTable Dt = new DataTable();
                cmdda.Fill(Dt);


                List<MstSpecialisation> ObjLstMstSpecialisation = new List<MstSpecialisation>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstSpecialisation ObjMstSpecialisation = new MstSpecialisation();

                        ObjMstSpecialisation.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        ObjMstSpecialisation.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        ObjMstSpecialisation.ProgrammeCode = (Convert.ToString(Dt.Rows[i]["ProgrammeCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeCode"]);
                        ObjMstSpecialisation.BranchName = ((Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "Not Defined" : (Convert.ToString(Dt.Rows[i]["BranchName"])));
                        ObjMstSpecialisation.FacultyId = (((Dt.Rows[i]["FacultyId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        ObjMstSpecialisation.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        ObjMstSpecialisation.ProgrammeId = (Convert.ToString(Dt.Rows[i]["ProgrammeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);
                        ObjMstSpecialisation.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        ObjMstSpecialisation.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjMstSpecialisation.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);

                        ObjLstMstSpecialisation.Add(ObjMstSpecialisation);
                    }

                }

                return Return.returnHttp("200", ObjLstMstSpecialisation, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region MstProgrammeBranchListGet
        public HttpResponseMessage MstProgrammeBranchListGet()
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter cmdda = new SqlDataAdapter("MstProgrammeBranchMapGet", con);

                cmdda.SelectCommand.CommandType = CommandType.StoredProcedure;

                DataTable Dt = new DataTable();
                cmdda.Fill(Dt);


                List<MstSpecialisation> ObjLstMstSpecialisation = new List<MstSpecialisation>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstSpecialisation ObjMstSpecialisation = new MstSpecialisation();

                        ObjMstSpecialisation.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        ObjMstSpecialisation.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        ObjMstSpecialisation.ProgrammeCode = (Convert.ToString(Dt.Rows[i]["ProgrammeCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeCode"]);
                        ObjMstSpecialisation.BranchName = ((Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "Not Defined" : (Convert.ToString(Dt.Rows[i]["BranchName"])));
                        ObjMstSpecialisation.FacultyId = (((Dt.Rows[i]["FacultyId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        ObjMstSpecialisation.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        ObjMstSpecialisation.ProgrammeId = (Convert.ToString(Dt.Rows[i]["ProgrammeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);
                        ObjMstSpecialisation.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        ObjMstSpecialisation.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjMstSpecialisation.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);

                        ObjLstMstSpecialisation.Add(ObjMstSpecialisation);
                    }

                }

                return Return.returnHttp("200", ObjLstMstSpecialisation, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region IncProgramInstanceGet
        [HttpPost]
        public HttpResponseMessage IncProgramInstanceGet()
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("IncProgrammeInstanceGet", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                //da.SelectCommand.Parameters.AddWithValue("@Flag", "IncProgrammeInstanceListGet");
                DataTable dt = new DataTable();
                da.Fill(dt);
                Int32 count = 0;
                List<ProgramInstance> ObjLstProgInst = new List<ProgramInstance>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProgramInstance ObjProgInst = new ProgramInstance();
                        ObjProgInst.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjProgInst.FacultyName = (Convert.ToString(dt.Rows[i]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["FacultyName"]);                         
                        ObjProgInst.FacultyId = (Convert.ToString(dt.Rows[i]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[i]["FacultyId"]);
                        ObjProgInst.ProgrammeId = (Convert.ToString(dt.Rows[i]["ProgrammeId"])).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[i]["ProgrammeId"]);
                        ObjProgInst.ProgrammeName = (Convert.ToString(dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["ProgrammeName"]);
                        //ObjProgInst.BranchName = Convert.ToString(dt.Rows[i]["BranchName"]);
                        //ObjProgInst.SpecialisationId = Convert.ToInt32(dt.Rows[i]["SpecialisationId"]);
                        ObjProgInst.AcademicYear = (Convert.ToString(dt.Rows[i]["AcademicYearCode"])).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["AcademicYearCode"]);
                        ObjProgInst.AcademicYearId = (Convert.ToString(dt.Rows[i]["AcademicYearId"])).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[i]["AcademicYearId"]);
                        //ObjProgInst.ProgrammeInstanceId = Convert.ToInt32(dt.Rows[i]["ProgrammeInstanceId"]);
                        ObjProgInst.InstanceName = (Convert.ToString(dt.Rows[i]["InstanceName"])).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["InstanceName"]);
                        ObjProgInst.Intake = (Convert.ToString(dt.Rows[i]["Intake"])).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[i]["Intake"]);
                        ObjProgInst.AdmissionRight = (Convert.ToString(dt.Rows[i]["AdmissionRight"])).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["AdmissionRight"]);
                        ObjProgInst.IsActive = (Convert.ToString(dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(dt.Rows[i]["IsActive"]);
                        ObjProgInst.IsActiveSts = (Convert.ToString(dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["IsActiveSts"]);
                        ObjProgInst.IsDeleted = (Convert.ToString(dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(dt.Rows[i]["IsDeleted"]); ;
                        //ObjProgInst.CreatedBy = Convert.ToInt64(dt.Rows[i]["CreatedBy"]);
                        //ObjProgInst.CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]);
                        //ObjProgInst.ModifiedBy = Convert.ToInt64(dt.Rows[i]["ModifiedBy"]);
                        //ObjProgInst.ModifiedOn = Convert.ToDateTime(dt.Rows[i]["ModifiedOn"]);
                        count = count + 1;
                        ObjProgInst.IndexId = count;

                        ObjLstProgInst.Add(ObjProgInst);

                    }
                    return Return.returnHttp("200", ObjLstProgInst, null);
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

        #region PreProgrammeInstanceGetByFacultyId
        [HttpPost]
        public HttpResponseMessage PreMstProgrammeInstanceListGetbyFacultyId(ProgramInstance ObjProgInstance)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("PreIncProgrammeInstanceGetByFacultyId", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@InstituteId", ObjProgInstance.InstituteId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<ProgramInstance> ObjLstProgInst = new List<ProgramInstance>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProgramInstance ObjProgInst = new ProgramInstance();
                        ObjProgInst.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjProgInst.FacultyName = Convert.ToString(dt.Rows[i]["FacultyName"]);
                        ObjProgInst.FacultyId = Convert.ToInt32(dt.Rows[i]["FacultyId"]);
                        ObjProgInst.ProgrammeId = Convert.ToInt32(dt.Rows[i]["ProgrammeId"]);
                        ObjProgInst.ProgrammeName = Convert.ToString(dt.Rows[i]["ProgrammeName"]);
                        ObjProgInst.InstanceName = Convert.ToString(dt.Rows[i]["InstanceName"]);
                        //ObjProgInst.BranchName = Convert.ToString(dt.Rows[i]["BranchName"]);
                        //ObjProgInst.SpecialisationId = Convert.ToInt32(dt.Rows[i]["SpecialisationId"]);
                        ObjProgInst.AcademicYear = Convert.ToString(dt.Rows[i]["AcademicYearCode"]);
                        ObjProgInst.AcademicYearId = Convert.ToInt32(dt.Rows[i]["AcademicYearId"]);
                        ObjProgInst.Intake = Convert.ToInt32(dt.Rows[i]["Intake"]);
                        ObjProgInst.AdmissionRight = Convert.ToString(dt.Rows[i]["AdmissionRight"]);
                        ObjProgInst.IsActive = Convert.ToBoolean(dt.Rows[i]["IsActive"]);
                        ObjProgInst.IsActiveSts = Convert.ToString(dt.Rows[i]["IsActiveSts"]);
                        ObjProgInst.IsDeleted = Convert.ToBoolean(dt.Rows[i]["IsDeleted"]);
                        //ObjProgInst.CreatedBy = Convert.ToInt64(dt.Rows[i]["CreatedBy"]);
                        //ObjProgInst.CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]);
                        //ObjProgInst.ModifiedBy = Convert.ToInt64(dt.Rows[i]["ModifiedBy"]);
                        //ObjProgInst.ModifiedOn = Convert.ToDateTime(dt.Rows[i]["ModifiedOn"]);

                        ObjLstProgInst.Add(ObjProgInst);

                    }
                    return Return.returnHttp("200", ObjLstProgInst, null);
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

        #region PreProgrammeInstanceGetById
        [HttpPost]
        public HttpResponseMessage PreMstProgrammeInstanceListGetbyId(ProgramInstance ObjProgInstance)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("PreIncProgrammeInstanceGetById", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Id", ObjProgInstance.Id);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<ProgramInstance> ObjLstProgInst = new List<ProgramInstance>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProgramInstance ObjProgInst = new ProgramInstance();
                        ObjProgInst.Id = Convert.ToInt32(dt.Rows[i]["Id"]);                      
                        ObjProgInst.ProgrammeId = Convert.ToInt32(dt.Rows[i]["ProgrammeId"]);                      
                        ObjProgInst.AcademicYearId = Convert.ToInt32(dt.Rows[i]["AcademicYearId"]);                      
                        ObjLstProgInst.Add(ObjProgInst);

                    }
                    return Return.returnHttp("200", ObjLstProgInst, null);
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


        #region PreProgrammeInstanceGetByFacIdAndYearId
        [HttpPost]
        public HttpResponseMessage PreMstProgrammeInstanceListGetbyFacIdAndYearId(ProgramInstance ObjProgInstance)
        {
            try
            {
                Request.Headers.TryGetValues("DepartmentId", out IEnumerable<string> DepartmentId1);
                var DepartmentId = DepartmentId1?.FirstOrDefault();
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("PreIncProgrammeInstanceGetByFacIdAndYearId", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (DepartmentId != null || DepartmentId != "" || DepartmentId != "0")
                {
                    da.SelectCommand.Parameters.AddWithValue("@InstituteId", ObjProgInstance.InstituteId);
                    da.SelectCommand.Parameters.AddWithValue("@AcademicYearId", ObjProgInstance.AcademicYearId);
                    da.SelectCommand.Parameters.AddWithValue("@DepartmentId", DepartmentId);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@InstituteId", ObjProgInstance.InstituteId);
                    da.SelectCommand.Parameters.AddWithValue("@AcademicYearId", ObjProgInstance.AcademicYearId);
                }
                
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<ProgramInstance> ObjLstProgInst = new List<ProgramInstance>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProgramInstance ObjProgInst = new ProgramInstance();
                        ObjProgInst.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjProgInst.FacultyName = Convert.ToString(dt.Rows[i]["FacultyName"]);
                        ObjProgInst.FacultyId = Convert.ToInt32(dt.Rows[i]["FacultyId"]);
                        ObjProgInst.ProgrammeId = Convert.ToInt32(dt.Rows[i]["ProgrammeId"]);
                        ObjProgInst.ProgrammeName = Convert.ToString(dt.Rows[i]["ProgrammeName"]);
                        ObjProgInst.InstanceName = Convert.ToString(dt.Rows[i]["InstanceName"]);
                        //ObjProgInst.BranchName = Convert.ToString(dt.Rows[i]["BranchName"]);
                        //ObjProgInst.SpecialisationId = Convert.ToInt32(dt.Rows[i]["SpecialisationId"]);
                        ObjProgInst.AcademicYear = Convert.ToString(dt.Rows[i]["AcademicYearCode"]);
                        //ObjProgInst.AcademicYearId = Convert.ToInt32(dt.Rows[i]["AcademicYearId"]);
                        //ObjProgInst.Intake = Convert.ToInt32(dt.Rows[i]["Intake"]);
                        ObjProgInst.IsActive = Convert.ToBoolean(dt.Rows[i]["IsActive"]);
                        ObjProgInst.IsActiveSts = Convert.ToString(dt.Rows[i]["IsActiveSts"]);
                        ObjProgInst.IsDeleted = Convert.ToBoolean(dt.Rows[i]["IsDeleted"]);
                        //ObjProgInst.CreatedBy = Convert.ToInt64(dt.Rows[i]["CreatedBy"]);
                        //ObjProgInst.CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]);
                        //ObjProgInst.ModifiedBy = Convert.ToInt64(dt.Rows[i]["ModifiedBy"]);
                        //ObjProgInst.ModifiedOn = Convert.ToDateTime(dt.Rows[i]["ModifiedOn"]);

                        ObjLstProgInst.Add(ObjProgInst);

                    }
                    return Return.returnHttp("200", ObjLstProgInst, null);
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

        #region Programme Instance Get By FacultyId And AcademicYearId
        [HttpPost]
        public HttpResponseMessage InstanceListGetbyFacultyIdAndAcadId(ProgramInstance ObjProgInstance)
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

                List<ProgramInstance> ObjLstProgInst = new List<ProgramInstance>();
                BALProgrammeInstance ObjBalProgInstList = new BALProgrammeInstance();
                ObjLstProgInst = ObjBalProgInstList.InstanceListGetbyFacultyIdAndAcadId(ObjProgInstance.FacultyId, ObjProgInstance.AcademicYearId);

                if (ObjLstProgInst != null)
                    return Return.returnHttp("200", ObjLstProgInst, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region IncProgrammInstanceAdd
        [HttpPost]
        public HttpResponseMessage IncProgrammInstanceAdd(ProgramInstance ObjProgInstance)
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
                if (String.IsNullOrEmpty(Convert.ToString(ObjProgInstance.FacultyId)))
                {
                    return Return.returnHttp("201", "Please select faculty", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjProgInstance.ProgrammeId)))
                {
                    return Return.returnHttp("201", "Please select programme", null);
                }
                /*else if (String.IsNullOrEmpty(Convert.ToString(ObjProgInstance.SpecialisationId)))
                {
                    return Return.returnHttp("201", "Please select Specialisation", null);
                }*/
                else if (String.IsNullOrEmpty(Convert.ToString(ObjProgInstance.AcademicYearId)))
                {
                    return Return.returnHttp("201", "Please select AcademicYear", null);
                }
                else if (validation.validateDigit(Convert.ToString(ObjProgInstance.Intake)) == false)
                {
                    return Return.returnHttp("201", "Please enter Intake", null);
                }
                else
                {

                    SqlCommand cmd = new SqlCommand("IncProgrammeInstanceAdd", con);

                    ProgramInstance ObjProgInst = new ProgramInstance();

                    //dynamic JsonData = JsonObj;

                    ObjProgInst.FacultyId = ObjProgInstance.FacultyId;
                    ObjProgInst.ProgrammeId = ObjProgInstance.ProgrammeId;
                    //ObjProgInst.SpecialisationId = ObjProgInstance.SpecialisationId;
                    ObjProgInst.AcademicYearId = ObjProgInstance.AcademicYearId;
                    ObjProgInst.Intake = ObjProgInstance.Intake;
                    ObjProgInst.AdmissionRight = ObjProgInstance.AdmissionRight;

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FacultyId", ObjProgInst.FacultyId);
                    cmd.Parameters.AddWithValue("@ProgrammeId", ObjProgInst.ProgrammeId);
                    //cmd.Parameters.AddWithValue("@SpecialisationId", ObjProgInst.SpecialisationId);
                    cmd.Parameters.AddWithValue("@AcademicId", ObjProgInst.AcademicYearId);
                    cmd.Parameters.AddWithValue("@Intake", ObjProgInst.Intake);
                    cmd.Parameters.AddWithValue("@AdmissionRight", ObjProgInst.AdmissionRight);
                    cmd.Parameters.AddWithValue("@UserId", 1);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters.Add("@ProgInstId", SqlDbType.Int);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                    cmd.Parameters["@ProgInstId"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    con.Close();
                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Your data has been added successfully.";
                        ObjProgInst.Id = Convert.ToInt32(cmd.Parameters["@ProgInstId"].Value);
                    }
                    return Return.returnHttp("200", (strMessage.ToString(), ObjProgInst.Id), null);
                   
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion
        
        #region PreIncProgrammInstanceAdd
        [HttpPost]
        public HttpResponseMessage PreIncProgrammInstanceAdd(ProgramInstance ObjProgInstance)
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
                if (String.IsNullOrEmpty(Convert.ToString(ObjProgInstance.FacultyId)))
                {
                    return Return.returnHttp("201", "Please select faculty", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjProgInstance.ProgrammeId)))
                {
                    return Return.returnHttp("201", "Please select programme", null);
                }
                //else if (String.IsNullOrEmpty(Convert.ToString(ObjProgInstance.SpecialisationId)))
                //{
                //    return Return.returnHttp("201", "Please select Specialisation", null);
                //}
                else if (String.IsNullOrEmpty(Convert.ToString(ObjProgInstance.AcademicYearId)))
                {
                    return Return.returnHttp("201", "Please select AcademicYear", null);
                }
                else if (validation.validateDigit(Convert.ToString(ObjProgInstance.Intake)) == false)
                {
                    return Return.returnHttp("201", "Please enter Intake", null);
                }
                else
                {

                    SqlCommand cmd = new SqlCommand("PreIncProgrammeInstanceAdd", con);

                    ProgramInstance ObjProgInst = new ProgramInstance();

                    //dynamic JsonData = JsonObj;

                    ObjProgInst.ProgrammeId = ObjProgInstance.ProgrammeId;
                    ObjProgInst.FacultyId = ObjProgInstance.FacultyId;
                    //ObjProgInst.SpecialisationId = ObjProgInstance.SpecialisationId;
                    ObjProgInst.AcademicYearId = ObjProgInstance.AcademicYearId;
                    ObjProgInst.Intake = ObjProgInstance.Intake;
                    ObjProgInst.AdmissionRight = ObjProgInstance.AdmissionRight;

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ProgrammeId", ObjProgInst.ProgrammeId);
                    cmd.Parameters.AddWithValue("@FacultyId", ObjProgInst.FacultyId);
                    //cmd.Parameters.AddWithValue("@SpecialisationId", ObjProgInst.SpecialisationId);
                    cmd.Parameters.AddWithValue("@AcademicId", ObjProgInst.AcademicYearId);
                    cmd.Parameters.AddWithValue("@Intake", ObjProgInst.Intake);
                    cmd.Parameters.AddWithValue("@AdmissionRight", ObjProgInst.AdmissionRight);
                    cmd.Parameters.AddWithValue("@UserId", 1);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@ProgInstance", SqlDbType.BigInt);
                    cmd.Parameters["@ProgInstance"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@ProgName", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@ProgName"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    string ProgInstance = Convert.ToString(cmd.Parameters["@ProgInstance"].Value);
                    string ProgName = Convert.ToString(cmd.Parameters["@ProgName"].Value);
                    con.Close();
                    if (strMessage.Equals("TRUE"))
                    {
                        strMessage="Data has been saved successfully";
                    }
                    //else
                    //{
                    //    return Return.returnHttp("201", strMessage, null);
                    //}
                    PreProgInstance ObjPPI = new PreProgInstance();

                    ObjPPI.strMessage = strMessage;
                    if(ProgInstance=="")
                    ObjPPI.ProgInstance = 0;
                    else
                     ObjPPI.ProgInstance = Convert.ToInt64(ProgInstance.ToString());
                    ObjPPI.ProgInstancePart = 0;
                    ObjPPI.ProgInstancePartTerm = 0;
                    if (ProgInstance == "")
                        ObjPPI.ProgrammeName = null;
                    else
                        ObjPPI.ProgrammeName = ProgName;
                    ObjPPI.ProgrammePartName = null;
                    ObjPPI.ProgrammePartTermName = null;
                    return Return.returnHttp("200", ObjPPI, null);

                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region IncProgrammInstanceUpdate
        [HttpPost]
        public HttpResponseMessage IncProgrammInstanceUpdate(ProgramInstance ObjProgInstance)
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

                //if (String.IsNullOrEmpty(Convert.ToString(ObjProgInstance.FacultyId)))
                //{
                //    return Return.returnHttp("201", "Please select faculty", null);
                //}
                //else if (String.IsNullOrEmpty(Convert.ToString(ObjProgInstance.ProgrammeId)))
                //{
                //    return Return.returnHttp("201", "Please select programme", null);
                //}
                //else if (String.IsNullOrEmpty(Convert.ToString(ObjProgInstance.SpecialisationId)))
                //{
                //    return Return.returnHttp("201", "Please select Specialisation", null);
                //}
                //else if (String.IsNullOrEmpty(Convert.ToString(ObjProgInstance.AcademicYearId)))
                //{
                //    return Return.returnHttp("201", "Please select AcademicYear", null);
                //}
                if (validation.validateDigit(Convert.ToString(ObjProgInstance.Intake)) == false)
                {
                    return Return.returnHttp("201", "Please enter Intake", null);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("IncProgrammeInstanceEdit", con);

                    ProgramInstance ObjProgInst = new ProgramInstance();

                    //dynamic JsonData = JsonObj;

                    //ObjProgInst.ProgrammeId = ObjProgInstance.ProgrammeId;
                    //ObjProgInst.SpecialisationId = ObjProgInstance.SpecialisationId;
                    //ObjProgInst.AcademicYearId = ObjProgInstance.AcademicYearId;
                    ObjProgInst.Intake = ObjProgInstance.Intake;
                    ObjProgInst.AdmissionRight = ObjProgInstance.AdmissionRight;
                    ObjProgInst.Id = ObjProgInstance.Id;
                    cmd.CommandType = CommandType.StoredProcedure;

                    //cmd.Parameters.AddWithValue("@ProgrammeId", ObjProgInst.ProgrammeId);
                    //cmd.Parameters.AddWithValue("@SpecialisationId", ObjProgInst.SpecialisationId);
                    //cmd.Parameters.AddWithValue("@AcademicId", ObjProgInst.AcademicYearId);
                    cmd.Parameters.AddWithValue("@Intake", ObjProgInst.Intake);
                    cmd.Parameters.AddWithValue("@AdmissionRight", ObjProgInst.AdmissionRight);
                    cmd.Parameters.AddWithValue("@ProgrammeInstanceId", ObjProgInst.Id);
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

        #region PreIncProgrammInstanceUpdate
        [HttpPost]
        public HttpResponseMessage PreIncProgrammInstanceUpdate(ProgramInstance ObjProgInstance)
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

                //if (String.IsNullOrEmpty(Convert.ToString(ObjProgInstance.FacultyId)))
                //{
                //    return Return.returnHttp("201", "Please select faculty", null);
                //}
                //else if (String.IsNullOrEmpty(Convert.ToString(ObjProgInstance.ProgrammeId)))
                //{
                //    return Return.returnHttp("201", "Please select programme", null);
                //}
                //else if (String.IsNullOrEmpty(Convert.ToString(ObjProgInstance.SpecialisationId)))
                //{
                //    return Return.returnHttp("201", "Please select Specialisation", null);
                //}
                //else if (String.IsNullOrEmpty(Convert.ToString(ObjProgInstance.AcademicYearId)))
                //{
                //    return Return.returnHttp("201", "Please select AcademicYear", null);
                //}
                if (validation.validateDigit(Convert.ToString(ObjProgInstance.Intake)) == false)
                {
                    return Return.returnHttp("201", "Please enter Intake", null);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("PreIncProgrammeInstanceEdit", con);

                    ProgramInstance ObjProgInst = new ProgramInstance();

                    //dynamic JsonData = JsonObj;

                    //ObjProgInst.ProgrammeId = ObjProgInstance.ProgrammeId;
                    //ObjProgInst.SpecialisationId = ObjProgInstance.SpecialisationId;
                    //ObjProgInst.AcademicYearId = ObjProgInstance.AcademicYearId;
                    ObjProgInst.Intake = ObjProgInstance.Intake;
                    ObjProgInst.AdmissionRight = ObjProgInstance.AdmissionRight;
                    ObjProgInst.Id = ObjProgInstance.Id;
                    cmd.CommandType = CommandType.StoredProcedure;

                    //cmd.Parameters.AddWithValue("@ProgrammeId", ObjProgInst.ProgrammeId);
                    //cmd.Parameters.AddWithValue("@SpecialisationId", ObjProgInst.SpecialisationId);
                    //cmd.Parameters.AddWithValue("@AcademicId", ObjProgInst.AcademicYearId);
                    cmd.Parameters.AddWithValue("@Intake", ObjProgInst.Intake);
                    cmd.Parameters.AddWithValue("@AdmissionRight", ObjProgInst.AdmissionRight);
                    cmd.Parameters.AddWithValue("@ProgrammeInstanceId", ObjProgInst.Id);
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
                        return Return.returnHttp("200", "Data has been saved successfully", null);
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


        #region IncProgrammInstanceDelete
        [HttpPost]
        public HttpResponseMessage IncProgrammInstanceDelete(ProgramInstance ObjProgInstance)
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

                ProgramInstance ObjProgInst = new ProgramInstance();

                //dynamic JsonData = JsonObj;
                ObjProgInst.Id = ObjProgInstance.Id;

                SqlCommand cmd = new SqlCommand("IncProgrammeInstanceDelete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", ObjProgInst.Id);
                //cmd.Parameters.AddWithValue("@UserId", UserId);
                //cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
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

        #region MstProgrammeInstIsActiveEnable
        [HttpPost]
        public HttpResponseMessage MstProgrammeInstIsActiveEnable(ProgramInstance ObjProgInstance)
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
                ProgramInstance ObjProgInst = new ProgramInstance();


                ObjProgInst.Id = ObjProgInstance.Id;


                SqlCommand cmd = new SqlCommand("IncProgrammeInstanceActive", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeInstanceId", ObjProgInst.Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.AddWithValue("@Flag", "ProgrammeInstanceIsActiveEnable");
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();
                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been Modified successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region MstProgrammeInstIsActiveDisable
        [HttpPost]
        public HttpResponseMessage MstProgrammeInstIsActiveDisable(ProgramInstance ObjProgInstance)
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
                ProgramInstance ObjProgInst = new ProgramInstance();

                //dynamic JSonData = JSONObject;

                // JObject ProgrammeJSon = JSonData.Programme;

                ObjProgInst.Id = ObjProgInstance.Id;

                // Programme.ModifiedBy = 1;


                SqlCommand cmd = new SqlCommand("IncProgrammeInstanceActive", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeInstanceId", ObjProgInst.Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.AddWithValue("@Flag", "ProgrammeInstanceIsActiveDisable");
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();
                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been Modified successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        //Mohini's Code
        #region GetMstProgrammebyFacIdAndYearId
        [HttpPost]
        public HttpResponseMessage GetMstProgrammebyFacIdAndYearId(ProgramInstance ObjProgInstance)
        {
            try
            {
                Request.Headers.TryGetValues("DepartmentId", out IEnumerable<string> DepartmentId1);
                var DepartmentId = DepartmentId1?.FirstOrDefault();
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("GetMstProgrammebyFacIdAndYearId", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (DepartmentId != null || DepartmentId != "" || DepartmentId != "0")
                {
                    da.SelectCommand.Parameters.AddWithValue("@InstituteId", ObjProgInstance.InstituteId);
                    da.SelectCommand.Parameters.AddWithValue("@AcademicYearId", ObjProgInstance.AcademicYearId);
                    da.SelectCommand.Parameters.AddWithValue("@DepartmentId", DepartmentId);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@InstituteId", ObjProgInstance.InstituteId);
                    da.SelectCommand.Parameters.AddWithValue("@AcademicYearId", ObjProgInstance.AcademicYearId);
                }

                DataTable dt = new DataTable();
                da.Fill(dt);

                List<ProgramInstance> ObjLstProgInst = new List<ProgramInstance>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProgramInstance ObjProgInst = new ProgramInstance();
                        ObjProgInst.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjProgInst.FacultyName = Convert.ToString(dt.Rows[i]["FacultyName"]);
                        ObjProgInst.FacultyId = Convert.ToInt32(dt.Rows[i]["FacultyId"]);
                        ObjProgInst.ProgrammeId = Convert.ToInt32(dt.Rows[i]["ProgrammeId"]);
                        ObjProgInst.InstanceName = Convert.ToString(dt.Rows[i]["ProgrammeName"]);
                        ObjProgInst.ProgrammeCode = Convert.ToString(dt.Rows[i]["ProgrammeCode"]);
                        ObjProgInst.AcademicYear = Convert.ToString(dt.Rows[i]["AcademicYearCode"]);

                        ObjLstProgInst.Add(ObjProgInst);

                    }
                    return Return.returnHttp("200", ObjLstProgInst, null);
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

        #region AdmissionYearGet
        [HttpPost]
        public HttpResponseMessage AdmissionYearGet(AcademicYear ObjAYList)
        {
            try
            {
                Int32 AcademicYearId = ObjAYList.AcademicYearId;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("AdmissionYearGet", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                da.SelectCommand = cmd;
                da.Fill(dt);


                List<AcademicYear> ObjAYL = new List<AcademicYear>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AcademicYear ObjAY = new AcademicYear();
                        ObjAY.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjAY.AdmissionYearCode = Convert.ToString(dt.Rows[i]["AdmissionYearCode"]);
                        ObjAYL.Add(ObjAY);
                    }
                    return Return.returnHttp("200", ObjAYL, null);
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

        #region Mohini - Programme Instance Get By FacultyId And AcademicYearId
        [HttpPost]
        public HttpResponseMessage MInstanceListGetbyFacultyIdAndAcadId(ProgramInstance ObjProgInstance)
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

                List<ProgramInstance> ObjLstProgInst = new List<ProgramInstance>();
                BALProgrammeInstance ObjBalProgInstList = new BALProgrammeInstance();
                ObjLstProgInst = ObjBalProgInstList.MInstanceListGetbyFacultyIdAndAcadId(ObjProgInstance.FacultyId, ObjProgInstance.AcademicYearId, ObjProgInstance.AdmissionYearId);

                if (ObjLstProgInst != null)
                    return Return.returnHttp("200", ObjLstProgInst, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion
    }
}
