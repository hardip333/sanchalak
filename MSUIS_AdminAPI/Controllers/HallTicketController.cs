using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;
using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using MSUISApi.BAL;
using Newtonsoft.Json.Linq;

namespace MSUISApi.Controllers
{
    public class HallTicketController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region Exam Event List Get For Drop Down
        [HttpPost]
        public HttpResponseMessage ExamEventGetForDropDown()
        {

            try
            {
                List<ExamEventMaster> ObjLstExamEvent = new List<ExamEventMaster>();
                BALExamEventMaster ObjBalExamEvent = new BALExamEventMaster();
                ObjLstExamEvent = ObjBalExamEvent.ExamEventGetForDropDown();

                if (ObjLstExamEvent != null)
                    return Return.returnHttp("200", ObjLstExamEvent, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region Institute Get By Exam Event
        [HttpPost]
        public HttpResponseMessage MstInstituteGetByExamEvent(MstInstitute ObjInst)
        {
            try
            {
                
                List<MstInstitute> ObjLstInstitute = new List<MstInstitute>();
                BALInstituteList ObjBalInstituteList = new BALInstituteList();
                ObjLstInstitute = ObjBalInstituteList.InstituteGetByExamEvent(ObjInst.ExamMasterId);

                if (ObjLstInstitute != null)
                    return Return.returnHttp("200", ObjLstInstitute, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion        

        #region Programme List Get by Faculty Exam Id
        [HttpPost]
        public HttpResponseMessage MstProgrammeGetByFacultyExamId(MstProgramme ObjProg)
        {

            try
            {
                List<MstProgramme> ObjLstProg = new List<MstProgramme>();
                BALProgramme ObjBalProgList = new BALProgramme();
                ObjLstProg = ObjBalProgList.ProgrammeGetByFacultyExamId(ObjProg.FacultyExamMapId);

                if (ObjLstProg != null)
                    return Return.returnHttp("200", ObjLstProg, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
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
                        ObjMstSpecialisation.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        ObjMstSpecialisation.ProgrammeCode = (Convert.ToString(Dt.Rows[i]["ProgrammeCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeCode"]);
                        ObjMstSpecialisation.BranchName = ((Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "" : (Convert.ToString(Dt.Rows[i]["BranchName"])));
                        ObjMstSpecialisation.FacultyId = (((Dt.Rows[i]["FacultyId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        ObjMstSpecialisation.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        ObjMstSpecialisation.ProgrammeId = (Convert.ToString(Dt.Rows[i]["ProgrammeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);
                        ObjMstSpecialisation.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
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

        #region Programme Part Term Get By Programme Id
        [HttpPost]
        public HttpResponseMessage MstProgrammePartTermGetbyProgrammeId(MstProgrammePartTerm programmePartTerm)
        {
            try
            {                
                List<MstProgrammePartTerm> ObjLstProgPartTerm = new List<MstProgrammePartTerm>();
                BALProgrammePartTerm ObjBalProgPartTermList = new BALProgrammePartTerm();
                ObjLstProgPartTerm = ObjBalProgPartTermList.ProgrammePartTermGetByProgrammeId(programmePartTerm.ProgrammeId);

                if (ObjLstProgPartTerm != null)
                    return Return.returnHttp("200", ObjLstProgPartTerm, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region Programme Part Term Get By Part term And Branch 
        [HttpPost]
        public HttpResponseMessage IncProgrammeInstancePartTermGetByPartTermAndBranch(ProgrammeInstancePartTerm ObjProg)
        {
            try
            {
                
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter cmdda = new SqlDataAdapter("IncProgrammeInstancePartTermGetByPartTermAndBranch", con);

                cmdda.SelectCommand.CommandType = CommandType.StoredProcedure;
                cmdda.SelectCommand.Parameters.AddWithValue("@PartTermId", ObjProg.ProgrammePartTermId);
                cmdda.SelectCommand.Parameters.AddWithValue("@SpecialisationId", ObjProg.SpecialisationId);

                DataTable Dt = new DataTable();
                cmdda.Fill(Dt);


                List<ProgrammeInstancePartTerm> ObjLstInstancePartTerm = new List<ProgrammeInstancePartTerm>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePartTerm ObjInstancePartTerm = new ProgrammeInstancePartTerm();

                        ObjInstancePartTerm.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        ObjInstancePartTerm.InstancePartTermName = (Convert.ToString(Dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);

                        ObjLstInstancePartTerm.Add(ObjInstancePartTerm);
                    }

                }

                return Return.returnHttp("200", ObjLstInstancePartTerm, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion        

        #region Get Pre Exam Data

        [HttpPost]
        public HttpResponseMessage PreExamDataGetForReport(PreExamDataReport ObjPreExam)
        {
            try
            {                
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter cmdda = new SqlDataAdapter("PreExamDataReportGetByExamEventBranchPartTerm", con);

                cmdda.SelectCommand.CommandType = CommandType.StoredProcedure;
                cmdda.SelectCommand.Parameters.AddWithValue("@ExamMasterId", ObjPreExam.ExamMasterId);
                cmdda.SelectCommand.Parameters.AddWithValue("@BranchId", ObjPreExam.BranchId);
                cmdda.SelectCommand.Parameters.AddWithValue("@ProgrammePartTermId", ObjPreExam.ProgrammePartTermId);

                DataTable Dt = new DataTable();
                cmdda.Fill(Dt);

                List<PreExamDataReport> ObjLstPreExamData = new List<PreExamDataReport>();
                Int32 count = 0;
                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        PreExamDataReport ObjPreExamData = new PreExamDataReport();

                        ObjPreExamData.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        ObjPreExamData.InstancePartTermName = (Convert.ToString(Dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                        ObjPreExamData.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        ObjPreExamData.ProgrammeCode = (Convert.ToString(Dt.Rows[i]["ProgrammeCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeCode"]);
                        ObjPreExamData.ProgrammePartName = (Convert.ToString(Dt.Rows[i]["ProgrammePartName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammePartName"]);
                        ObjPreExamData.ProgrammePartTermName = (Convert.ToString(Dt.Rows[i]["ProgrammePartTermName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammePartTermName"]);
                        ObjPreExamData.ProgrammeModeName = (Convert.ToString(Dt.Rows[i]["ProgrammeModeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeModeName"]);
                        ObjPreExamData.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["BranchName"]);
                        ObjPreExamData.PRN = (Convert.ToString(Dt.Rows[i]["PRN"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["PRN"]);
                        ObjPreExamData.NameAsPerMarksheet = (Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"]);
                        ObjPreExamData.FirstName = (Convert.ToString(Dt.Rows[i]["FirstName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FirstName"]);
                        ObjPreExamData.MiddleName = (Convert.ToString(Dt.Rows[i]["MiddleName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["MiddleName"]);
                        ObjPreExamData.LastName = (Convert.ToString(Dt.Rows[i]["LastName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["LastName"]);
                        ObjPreExamData.NameOfFather = (Convert.ToString(Dt.Rows[i]["NameOfFather"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["NameOfFather"]);
                        ObjPreExamData.NameOfMother = (Convert.ToString(Dt.Rows[i]["NameOfMother"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["NameOfMother"]);
                        DateTime DOB = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(Dt.Rows[i]["DOB"]).ToUniversalTime(), INDIAN_ZONE);
                        ObjPreExamData.DOB = string.Format("{0: dd-MM-yyyy}", DOB);
                        ObjPreExamData.InstructionMediumName = (Convert.ToString(Dt.Rows[i]["InstructionMediumName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstructionMediumName"]);
                        ObjPreExamData.ApplicationReservationName = (Convert.ToString(Dt.Rows[i]["ApplicationReservationName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ApplicationReservationName"]);
                        ObjPreExamData.OptionalMobileNo = (Convert.ToString(Dt.Rows[i]["OptionalMobileNo"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["OptionalMobileNo"]);
                        ObjPreExamData.MobileNo = (Convert.ToString(Dt.Rows[i]["MobileNo"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["MobileNo"]);
                        ObjPreExamData.EmailId = (Convert.ToString(Dt.Rows[i]["EmailId"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["EmailId"]);
                        ObjPreExamData.Gender = (Convert.ToString(Dt.Rows[i]["Gender"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["Gender"]);
                        ObjPreExamData.MaritalStatus = (Convert.ToString(Dt.Rows[i]["MaritalStatus"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["MaritalStatus"]);
                        ObjPreExamData.CurrentAddress = (Convert.ToString(Dt.Rows[i]["CurrentAddress"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["CurrentAddress"]);
                        ObjPreExamData.CurrentCityVillage = (Convert.ToString(Dt.Rows[i]["CurrentCityVillage"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["CurrentCityVillage"]);
                        ObjPreExamData.StateName = (Convert.ToString(Dt.Rows[i]["StateName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["StateName"]);
                        ObjPreExamData.DistrictName = (Convert.ToString(Dt.Rows[i]["DistrictName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["DistrictName"]);
                        ObjPreExamData.CurrentPincode = (Convert.ToString(Dt.Rows[i]["CurrentPincode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["CurrentPincode"]);
                        ObjPreExamData.PhysicalDisability = (Convert.ToString(Dt.Rows[i]["PhysicalDisability"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PhysicalDisability"]);
                        ObjPreExamData.InstituteCode = (Convert.ToString(Dt.Rows[i]["InstituteCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstituteCode"]);
                        ObjPreExamData.InstituteName = (Convert.ToString(Dt.Rows[i]["InstituteName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstituteName"]);
                        ObjPreExamData.FormNo = (Convert.ToString(Dt.Rows[i]["FormNo"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["FormNo"]);
                        ObjPreExamData.SeatNumber = (Convert.ToString(Dt.Rows[i]["SeatNumber"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["SeatNumber"]);
                        ObjPreExamData.BarCode = (Convert.ToString(Dt.Rows[i]["UId"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["UId"]);
                        ObjPreExamData.EligibilityByAcademics = (Convert.ToString(Dt.Rows[i]["EligibilityByAcademics"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["EligibilityByAcademics"]);
                        ObjPreExamData.InwardByTimestamp = (Convert.ToString(Dt.Rows[i]["InwardByTimestamp"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InwardByTimestamp"]);
                        ObjPreExamData.InwardDate = (Convert.ToString(Dt.Rows[i]["InwardDate"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InwardDate"]);
                        ObjPreExamData.InwardTime = (Convert.ToString(Dt.Rows[i]["InwardTime"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InwardTime"]);
                        ObjPreExamData.PaperCode = ((Convert.ToString(Dt.Rows[i]["PaperCode"])).IsEmpty() ? "" : (Convert.ToString(Dt.Rows[i]["PaperCode"])));
                        ObjPreExamData.PaperName = ((Convert.ToString(Dt.Rows[i]["PaperName"])).IsEmpty() ? "" : (Convert.ToString(Dt.Rows[i]["PaperName"])));
                        ObjPreExamData.AssessmentType = ((Convert.ToString(Dt.Rows[i]["AssessmentType"])).IsEmpty() ? "" : (Convert.ToString(Dt.Rows[i]["AssessmentType"])));
                        ObjPreExamData.TeachingLearningMethodName = ((Convert.ToString(Dt.Rows[i]["TeachingLearningMethodName"])).IsEmpty() ? "" : (Convert.ToString(Dt.Rows[i]["TeachingLearningMethodName"])));
                        ObjPreExamData.AssessmentMethodName = ((Convert.ToString(Dt.Rows[i]["AssessmentMethodName"])).IsEmpty() ? "" : (Convert.ToString(Dt.Rows[i]["AssessmentMethodName"])));
                        ObjPreExamData.AppearanceType = ((Convert.ToString(Dt.Rows[i]["AppearanceType"])).IsEmpty() ? "" : (Convert.ToString(Dt.Rows[i]["AppearanceType"])));
                        DateTime Date = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(Dt.Rows[i]["ExamDate"]).ToUniversalTime(), INDIAN_ZONE);
                        ObjPreExamData.ExamDate = string.Format("{0: dd-MM-yyyy}", Date);                   
                        ObjPreExamData.Duration = ((Convert.ToString(Dt.Rows[i]["Duration"])).IsEmpty() ? "" : (Convert.ToString(Dt.Rows[i]["Duration"])));
                        ObjPreExamData.ExamEvent = ((Convert.ToString(Dt.Rows[i]["ExamEvent"])).IsEmpty() ? "" : (Convert.ToString(Dt.Rows[i]["ExamEvent"])));
                        ObjPreExamData.VenueName = ((Convert.ToString(Dt.Rows[i]["VenueName"])).IsEmpty() ? "" : (Convert.ToString(Dt.Rows[i]["VenueName"])));
                        ObjPreExamData.CenterName = ((Convert.ToString(Dt.Rows[i]["CenterName"])).IsEmpty() ? "" : (Convert.ToString(Dt.Rows[i]["CenterName"])));
                        ObjPreExamData.VenueCode = ((Convert.ToString(Dt.Rows[i]["VenueCode"])).IsEmpty() ? "" : (Convert.ToString(Dt.Rows[i]["VenueCode"])));
                        count = count + 1;
                        ObjPreExamData.IndexId = count;
                        ObjLstPreExamData.Add(ObjPreExamData);
                    }
                    return Return.returnHttp("200", ObjLstPreExamData, null);

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

