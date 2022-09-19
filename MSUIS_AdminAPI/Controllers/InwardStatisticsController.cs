using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MSUISApi.BAL;
using System.Dynamic;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class InwardStatisticsController : ApiController 
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region ExamEventMaster Get
        [HttpPost]
        public HttpResponseMessage ExamEventMasterGet()
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

                SqlCommand cmd = new SqlCommand("InwardExamEventGet", Con);
                cmd.Parameters.AddWithValue("@UserId", res);

                cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<InwardStatistics> ObjIWD = new List<InwardStatistics>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        InwardStatistics ObjIW = new InwardStatistics();
                        ObjIW.ExamMasterId = Convert.ToInt64(dr["Id"]);
                        ObjIW.DisplayName = (dr["DisplayName"].ToString());
                        ObjIWD.Add(ObjIW);
                    }

                }
                return Return.returnHttp("200", ObjIWD, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region Faculty Get
        [HttpPost]
        public HttpResponseMessage MstFacultyGet()
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

                List<MstFaculty> ObjLstFaculty = new List<MstFaculty>();
                BALFacultyList ObjBalFacultyList = new BALFacultyList();
                ObjLstFaculty = ObjBalFacultyList.FacultyListGet();

                if (ObjLstFaculty != null)
                    return Return.returnHttp("200", ObjLstFaculty, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Programme List Get
        public List<MstProgramme> MstProgrammeGet(InwardStatistics ObjInwd)
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("InwardProgrammeGetByFaculty", Con);
                Cmd.Parameters.AddWithValue("@FacultyId", ObjInwd.FacultyId);

                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<MstProgramme> ObjLstProg = new List<MstProgramme>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgramme objProg = new MstProgramme();

                        objProg.ProgrammeId = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objProg.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        objProg.ProgrammeCode = (Convert.ToString(Dt.Rows[i]["ProgrammeCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeCode"]);
                        ObjLstProg.Add(objProg);
                    }
                }
                return ObjLstProg;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Specialisation Get For Drop Down
        public List<MstSpecialisation> SpecialisationGet(InwardStatistics ObjInwd)
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("InwardSpecialisationGetByFaculty", Con);
                Cmd.Parameters.AddWithValue("@ProgrammeId", ObjInwd.ProgrammeId);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<MstSpecialisation> ObjListSpecialisation = new List<MstSpecialisation>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstSpecialisation objSpecialisation = new MstSpecialisation();

                        objSpecialisation.SpecialisationId = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objSpecialisation.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["BranchName"]);
                        ObjListSpecialisation.Add(objSpecialisation);
                    }
                }
                return ObjListSpecialisation;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Programme Part List Get By Faculty
        [HttpPost]
        public HttpResponseMessage ProgrammePartListGetbyFaculty(InwardStatistics ObjInwd)
        {
            try
            {
                //modelobj.AcademicYearId = ObjInwd.AcademicYearId;
                Int32 ProgrammeId = Convert.ToInt32(ObjInwd.ProgrammeId);
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("InwardProgrammePartGetByFaculty", Con);
                cmd.Parameters.AddWithValue("@ProgrammeId", ProgrammeId);
                cmd.Parameters.AddWithValue("@UserId", res);

                cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<MstProgrammePart> ObjMstProgrammePart = new List<MstProgrammePart>();
                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgrammePart objProgPart = new MstProgrammePart();

                        objProgPart.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objProgPart.PartName = (Convert.ToString(Dt.Rows[i]["PartName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PartName"]);
                        objProgPart.PartShortName = (Convert.ToString(Dt.Rows[i]["PartShortName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PartShortName"]);
                        ObjMstProgrammePart.Add(objProgPart);
                    }
                    return Return.returnHttp("200", ObjMstProgrammePart, null);

                }
                else
                {
                    return Return.returnHttp("201", "No Record Found", null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        
        #endregion.

        #region Programme Part Term List Get By Faculty
        [HttpPost]
        public HttpResponseMessage ProgrammePartTermListGetbyFaculty(InwardStatistics ObjInwd)
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

                SqlCommand cmd = new SqlCommand("InwardProgrammePartTermGetByFaculty", Con);
                cmd.Parameters.AddWithValue("@ExamMasterId", ObjInwd.ExamMasterId);
                cmd.Parameters.AddWithValue("@ProgrammePartId", ObjInwd.ProgrammePartId);
                cmd.Parameters.AddWithValue("@UserId", res);

                cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<MstProgrammePartTerm> ObjMstProgrammePartTerm = new List<MstProgrammePartTerm>();
                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgrammePartTerm objProgPartTerm = new MstProgrammePartTerm();

                        objProgPartTerm.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objProgPartTerm.PartTermName = (Convert.ToString(Dt.Rows[i]["PartTermName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PartTermName"]);
                        objProgPartTerm.PartTermShortName = (Convert.ToString(Dt.Rows[i]["PartTermShortName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PartTermShortName"]);
                        ObjMstProgrammePartTerm.Add(objProgPartTerm);
                    }
                    return Return.returnHttp("200", ObjMstProgrammePartTerm, null);

                }
                else
                {
                    return Return.returnHttp("201", "No Record Found", null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }

        #endregion

        #region InwardStatList Get
        [HttpPost]
        public HttpResponseMessage InwardStatListGet(InwardStatistics ObjInwd)
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

                SqlCommand cmd = new SqlCommand("InwardStatListGet", Con);
                cmd.Parameters.AddWithValue("@ExamMasterId", ObjInwd.ExamMasterId);
                cmd.Parameters.AddWithValue("@SpecialisationId", ObjInwd.SpecialisationId);
                cmd.Parameters.AddWithValue("@ProgrammePartTermId ", ObjInwd.ProgrammePartTermId);
                cmd.Parameters.AddWithValue("@UserId", res);

                cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                Int32 count = 0;
                List<InwardStatistics> ObjIWDStat = new List<InwardStatistics>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        InwardStatistics ObjIWS = new InwardStatistics();
                        ObjIWS.MstPaperId = Convert.ToInt64(dr["MstPaperId"]);
                        ObjIWS.StudentCount = Convert.ToInt64(dr["StudentCount"]);
                        ObjIWS.PaperCode = (dr["PaperCode"].ToString());
                        ObjIWS.PaperName = (dr["PaperName"].ToString());
                      
                        var ExamDate = dr["ExamDate"];
                        if (ExamDate is DBNull)
                        {
                            ObjIWS.ExamDate = null;
                        }
                        else
                        {
                            ObjIWS.ExamDate = (dr["ExamDate"].ToString());
                        }

                        var ExamSlotId = dr["ExamSlotId"];
                        if (ExamSlotId is DBNull)
                        {
                            ObjIWS.ExamSlotId = null;
                        }
                        else
                        {
                            ObjIWS.ExamSlotId = Convert.ToInt64(dr["ExamSlotId"]);
                        }

                        var SlotName = dr["SlotName"];
                        if (SlotName is DBNull)
                        {
                            ObjIWS.SlotName = null;
                        }
                        else
                        {
                            ObjIWS.SlotName = (dr["SlotName"].ToString());
                        }

                        count = count + 1;
                        ObjIWS.IndexId = count;

                        ObjIWDStat.Add(ObjIWS);
                    }

                }
                return Return.returnHttp("200", ObjIWDStat, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region InwardStatList Get By Student Count
        [HttpPost]
        public HttpResponseMessage InwardStatListGetByStuCount(InwardStatisticsStu ObjInwdStu)
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

                SqlCommand cmd = new SqlCommand("InwardStatListGetByStuCount", Con);
                cmd.Parameters.AddWithValue("@ExamMasterId", ObjInwdStu.ExamMasterId);
                cmd.Parameters.AddWithValue("@SpecialisationId", ObjInwdStu.SpecialisationId);
                cmd.Parameters.AddWithValue("@ProgrammePartTermId ", ObjInwdStu.ProgrammePartTermId);
                cmd.Parameters.AddWithValue("@MstPaperId ", ObjInwdStu.MstPaperId);
                cmd.Parameters.AddWithValue("@UserId", res);

                cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                Int32 count = 0;
                List<InwardStatisticsStu> ObjIWDStatStu = new List<InwardStatisticsStu>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        InwardStatisticsStu ObjIWStu = new InwardStatisticsStu();
                        ObjIWStu.PRN = Convert.ToInt64(dr["PRN"]);
                        ObjIWStu.StudentName = (dr["StudentName"].ToString());
                        ObjIWStu.MobileNo = (dr["MobileNo"].ToString());
                        ObjIWStu.EmailId = (dr["EmailId"].ToString());
                        ObjIWStu.InwardStatus = (dr["InwardStatus"].ToString());
                        if((dr["InwardByTimestamp"].ToString()) != null && (dr["InwardByTimestamp"].ToString() != ""))
                        {
                            ObjIWStu.InwardByTimestamp = (dr["InwardByTimestamp"].ToString());
                        }
                        else
                        {
                            ObjIWStu.InwardByTimestamp = "NA";
                        }
                        
                        ObjIWStu.AppearanceTypeId = Convert.ToInt64(dr["AppearanceTypeId"]);
                        ObjIWStu.AppearanceType = (dr["AppearanceType"].ToString());

                        count = count + 1;
                        ObjIWStu.IndexId = count;

                        ObjIWDStatStu.Add(ObjIWStu);
                    }

                }
                return Return.returnHttp("200", ObjIWDStatStu, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region InwardStat ProgrammeDetails Get
        [HttpPost]
        public HttpResponseMessage InwardStatProgrammeDetailsGet(InwardStatProg ObjInwdProg)
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

                SqlCommand cmd = new SqlCommand("InwardStatProgrammeDetailsGet", Con);
                cmd.Parameters.AddWithValue("@ExamMasterId", ObjInwdProg.ExamMasterId);
                cmd.Parameters.AddWithValue("@SpecialisationId", ObjInwdProg.SpecialisationId);
                cmd.Parameters.AddWithValue("@ProgrammePartTermId ", ObjInwdProg.ProgrammePartTermId);
                cmd.Parameters.AddWithValue("@UserId", res);

                cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<InwardStatProg> ObjIWDStatProg = new List<InwardStatProg>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        InwardStatProg ObjIWSProg = new InwardStatProg();
                        ObjIWSProg.ProgInstPTId = Convert.ToInt64(dr["ProgInstPTId"]);
                        ObjIWSProg.FacultyName = (dr["FacultyName"].ToString());
                        ObjIWSProg.ProgrammeName = (dr["ProgrammeName"].ToString());
                        ObjIWSProg.BranchName = (dr["BranchName"].ToString());
                        ObjIWSProg.PartName = (dr["PartName"].ToString());
                        ObjIWSProg.PartShortName = (dr["PartShortName"].ToString());
                        ObjIWSProg.PartTermName = (dr["PartTermName"].ToString());
                        ObjIWSProg.PartTermShortName = (dr["PartTermShortName"].ToString());
                        ObjIWSProg.EventName = (dr["EventName"].ToString());


                        ObjIWDStatProg.Add(ObjIWSProg);
                    }

                }
                return Return.returnHttp("200", ObjIWDStatProg, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion
    }
}
