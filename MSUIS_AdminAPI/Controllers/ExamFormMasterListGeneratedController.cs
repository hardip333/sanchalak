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
    public class ExamFormMasterListGeneratedController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region ExamFormMasterListGeneratedGet
        [HttpPost]
        public HttpResponseMessage ExamFormMasterListGeneratedGet(ExamFormMasterListGenerated ObjEFM)
        {
            ExamFormMasterListGenerated modelobj = new ExamFormMasterListGenerated();

            try
            {
                modelobj.ProgrammeId = ObjEFM.ProgrammeId;
                modelobj.ExamMasterId = ObjEFM.ExamMasterId;
                modelobj.FacultyId = ObjEFM.FacultyId;
                modelobj.SpecialisationId = ObjEFM.SpecialisationId;
                modelobj.ProgrammePartTermId = ObjEFM.ProgrammePartTermId;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("ExamFormMasterListGenerated", con);
                TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammeId", modelobj.ProgrammeId);
                cmd.Parameters.AddWithValue("@ExamMasterId", modelobj.ExamMasterId);
                cmd.Parameters.AddWithValue("@FacultyId", modelobj.FacultyId);
                cmd.Parameters.AddWithValue("@SpecialisationId", modelobj.SpecialisationId);
                cmd.Parameters.AddWithValue("@ProgrammePartTermId", modelobj.ProgrammePartTermId);


                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<ExamFormMasterListGenerated> ObjLstEFM = new List<ExamFormMasterListGenerated>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ExamFormMasterListGenerated ObjEFML = new ExamFormMasterListGenerated();

                        ObjEFML.PRN = Convert.ToInt64(dr["PRN"].ToString());

                        ObjEFML.ProgInstancePartTermId = Convert.ToInt64(dr["ProgInstancePartTermId"].ToString());

                        ObjEFML.FullNume = (dr["FullNume"].ToString());

                        ObjEFML.SeatNumber = (Convert.ToString(dr["SeatNumber"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["SeatNumber"]);

                        ObjEFML.ExamVenueName = (Convert.ToString(dr["ExamVenueName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["ExamVenueName"]);

                        ObjEFML.ProgrammeName = (Convert.ToString(dr["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["ProgrammeName"]);

                        ObjEFML.AppearanceType = (Convert.ToString(dr["AppearanceType"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["AppearanceType"]);

                        //DateTime Date = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(dr["InwardByTimestamp"]).ToUniversalTime(), INDIAN_ZONE);
                        //ObjEFML.InwardByTimestampview = string.Format("{0: dd-MM-yyyy}", Date);
                        ObjEFML.InwardDate = (Convert.ToString(dr["InwardDate"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["InwardDate"]);

                        ObjEFML.ExamMasterId = Convert.ToInt32(dr["ExamMasterId"].ToString());

                        ObjEFML.ProgrammeId = Convert.ToInt32(dr["ProgrammeId"].ToString());

                        ObjEFML.FacultyId = Convert.ToInt32(dr["FacultyId"].ToString());

                        ObjEFML.SpecialisationId = Convert.ToInt32(dr["SpecialisationId"].ToString());

                        ObjEFML.ProgrammePartTermId = Convert.ToInt32(dr["ProgrammePartTermId"].ToString());

                        ObjEFML.FacultyName = (Convert.ToString(dr["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["FacultyName"]);

                        ObjEFML.BranchName = (Convert.ToString(dr["BranchName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["BranchName"]);

                        ObjEFML.PartTermName = (Convert.ToString(dr["PartTermName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["PartTermName"]);

                        ObjEFML.AppearanceType = (Convert.ToString(dr["AppearanceType"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["AppearanceType"]);



                        count = count + 1;
                        ObjEFML.IndexId = count;
                        ObjLstEFM.Add(ObjEFML);
                    }

                }
                return Return.returnHttp("200", ObjLstEFM, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion



        #region ExamEventGet
        [HttpPost]
        public HttpResponseMessage ExamEventGet()
        {
            try
            {

                SqlCommand Cmd = new SqlCommand("ExamEventGet", con);
                Cmd.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = Cmd;

                sda.Fill(dt);


                List<ExamEventMaster> ObjLstEEM = new List<ExamEventMaster>();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ExamEventMaster objEEM = new ExamEventMaster();

                        objEEM.ExamMasterId = Convert.ToInt32(dt.Rows[i]["Id"]);
                        objEEM.DisplayName = Convert.ToString(dt.Rows[i]["DisplayName"]);
                        objEEM.IsActive = Convert.ToBoolean(dt.Rows[i]["IsActive"]);
                        objEEM.IsDeleted = Convert.ToBoolean(dt.Rows[i]["IsDeleted"]);

                        ObjLstEEM.Add(objEEM);
                    }
                }
                return Return.returnHttp("200", ObjLstEEM, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion


        #region Programme Get By Faculty Id
        [HttpPost]
        public HttpResponseMessage MstProgrammeGetByFacultyId(MstProgramme ObjProg)
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

                List<MstProgramme> ObjLstProg = new List<MstProgramme>();
                BALProgramme ObjBalProgList = new BALProgramme();
                ObjLstProg = ObjBalProgList.ProgrammeGetByFacId(ObjProg.FacultyId);

                if (ObjLstProg != null)
                    return Return.returnHttp("200", ObjLstProg, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);

            }
            catch (Exception e)
            {

                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }

        #endregion


        #region MstSpecialisatinGetByProgrammmeId
        [HttpPost]
        public HttpResponseMessage MstSpecialisatinGetByProgrammmeId(MstSpecialisation ObjSpec)
        {
            try
            {

                SqlCommand Cmd = new SqlCommand("MstSpecialisationGetByPId", con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@ProgrammeId", ObjSpec.ProgrammeId);
                sda.SelectCommand = Cmd;

                sda.Fill(dt);


                List<MstSpecialisation> ObjLstEEM = new List<MstSpecialisation>();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        MstSpecialisation objEEM = new MstSpecialisation();

                        objEEM.SpecialisationId = Convert.ToInt32(dt.Rows[i]["Id"]);
                        objEEM.BranchName = Convert.ToString(dt.Rows[i]["BranchName"]);
                        objEEM.ProgrammeId = Convert.ToInt32(dt.Rows[i]["ProgrammeId"]);
                        objEEM.ProgrammeName = Convert.ToString(dt.Rows[i]["ProgrammeName"]);


                        ObjLstEEM.Add(objEEM);
                    }
                }
                return Return.returnHttp("200", ObjLstEEM, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion


        #region MstProgrammePartTermGetBySpeId
        [HttpPost]
        public HttpResponseMessage MstProgrammePartTermGetBySpeId(MstProgrammePartTerm ObjPPT)
        {
            try
            {

                SqlCommand Cmd = new SqlCommand("MstProgrammePartTermGetBySpeId", con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@SpecialisationId", ObjPPT.SpecialisationId);
                sda.SelectCommand = Cmd;

                sda.Fill(dt);


                List<MstProgrammePartTerm> ObjLstEEM = new List<MstProgrammePartTerm>();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        MstProgrammePartTerm objEEM = new MstProgrammePartTerm();

                        objEEM.ProgrammePartTermId = Convert.ToInt32(dt.Rows[i]["Id"]);
                        objEEM.PartTermName = Convert.ToString(dt.Rows[i]["PartTermName"]);                       


                        ObjLstEEM.Add(objEEM);
                    }
                }
                return Return.returnHttp("200", ObjLstEEM, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region MstFacultyGet
        [HttpPost]
        public HttpResponseMessage FacultyGetById(MstFaculty MF)
        {
            //MstFaculty modelobj = new MstFaculty(); 
            try
            {
                //modelobj.Id = MF.Id;
                SqlCommand cmd = new SqlCommand("MstFacGetbyId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", MF.Id);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<MstFaculty> ObjLstF = new List<MstFaculty>();
                //dt.Rows.Add("0", "All Faculty");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstFaculty ObjF = new MstFaculty();
                        ObjF.Id = Convert.ToInt32(dr["Id"]);
                        ObjF.FacultyName = (dr["FacultyName"].ToString());

                        ObjLstF.Add(ObjF);
                    }

                }

                return Return.returnHttp("200", ObjLstF, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion


        #region PaperListGetByExamFormMaster
        [HttpPost]
        public HttpResponseMessage PaperListGetByExamFormMaster(PaperListGetByExamFormMaster ObjPaperEFM)
        {
            PaperListGetByExamFormMaster modelobj = new PaperListGetByExamFormMaster();

            try
            {
                
                modelobj.ExamMasterId = ObjPaperEFM.ExamMasterId;
                modelobj.PRN = ObjPaperEFM.PRN;
                modelobj.ProgInstancePartTermId = ObjPaperEFM.ProgInstancePartTermId;


                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("PaperListGetByExamFormMaster", con);
                TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@ExamMasterId", modelobj.ExamMasterId);
                cmd.Parameters.AddWithValue("@PRN", modelobj.PRN);
                cmd.Parameters.AddWithValue("@ProgInstancePartTermId", modelobj.ProgInstancePartTermId);
                


                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<PaperListGetByExamFormMaster> ObjLstPLE = new List<PaperListGetByExamFormMaster>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        PaperListGetByExamFormMaster ObjPLE = new PaperListGetByExamFormMaster();

                        ObjPLE.PRN = Convert.ToInt64(dr["PRN"].ToString());

                        ObjPLE.ProgInstancePartTermId = Convert.ToInt64(dr["ProgInstancePartTermId"].ToString());

                        ObjPLE.ExamMasterId = Convert.ToInt32(dr["ExamMasterId"].ToString());

                        ObjPLE.PaperName = (Convert.ToString(dr["PaperName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["PaperName"]);

                        ObjPLE.PaperCode = (Convert.ToString(dr["PaperCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["PaperCode"]);

                        ObjPLE.ExamDate = (Convert.ToString(dr["ExamDate"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["ExamDate"]);

                        ObjPLE.SlotName = (Convert.ToString(dr["SlotName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["SlotName"]);

                        ObjPLE.ExamBlockName = (Convert.ToString(dr["ExamBlockName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["ExamBlockName"]);

                        ObjPLE.ExamFeesPaidStatus = (Convert.ToString(dr["ExamFeesPaidStatus"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["ExamFeesPaidStatus"]);

                        ObjPLE.ExamMasterName = (Convert.ToString(dr["ExamMasterName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["ExamMasterName"]);

                        ObjPLE.InstancePartTermName = (Convert.ToString(dr["InstancePartTermName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["InstancePartTermName"]);
                        count = count + 1;
                        ObjPLE.IndexId = count;
                        ObjLstPLE.Add(ObjPLE);
                    }

                }
                return Return.returnHttp("200", ObjLstPLE, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion






    }
}