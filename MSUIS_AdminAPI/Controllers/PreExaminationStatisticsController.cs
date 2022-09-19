using MSUIS_TokenManager.App_Start;
using MSUISApi.BAL;
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
    public class PreExaminationStatisticsController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

        #region PreExaminationStatisticsGet
        [HttpPost]
        public HttpResponseMessage PreExaminationStatisticsGet(PreExaminationStatistics ObjConReport)
        {
            PreExaminationStatistics modelobj = new PreExaminationStatistics();

            try
            {
                //modelobj.FacultyExamMapId = ObjConReport.FacultyExamMapId;
                modelobj.ExamEventId = ObjConReport.ExamEventId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("PreExaminationStatistics", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamEventId", modelobj.ExamEventId);
                //cmd.Parameters.AddWithValue("@FacultyExamMapId", modelobj.FacultyExamMapId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<PreExaminationStatistics> ObjLstCR = new List<PreExaminationStatistics>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        PreExaminationStatistics ObjCR = new PreExaminationStatistics();

                        ObjCR.FacultyExamMapId = Convert.ToInt32(dr["FacultyExamMapId"].ToString());
                        ObjCR.ExamEventId = Convert.ToInt32(dr["ExamEventId"].ToString());
                        ObjCR.DisplayName = (dr["DisplayName"].ToString());
                        ObjCR.ScheduleCode = (dr["ScheduleCode"].ToString());
                        ObjCR.FacultyId = Convert.ToInt32(dr["FacultyId"].ToString());
                        ObjCR.FacultyName = (dr["FacultyName"].ToString());
                        ObjCR.InstituteId = Convert.ToInt32(dr["InstituteId"].ToString());
                        ObjCR.InstituteName = (dr["InstituteName"].ToString());
                        ObjCR.AdmittedStudents = (dr["AdmittedStudents"].ToString());
                        var AdmittedStudents = dr["AdmittedStudents"];
                        if (AdmittedStudents is DBNull)
                        {
                            AdmittedStudents = null;

                        }
                        else
                        {
                            ObjCR.AdmittedStudents = (dr["AdmittedStudents"].ToString());

                        }
                        ObjCR.PaperSelected = (dr["PaperSelected"].ToString());
                        var PaperSelected = dr["PaperSelected"];
                        if (PaperSelected is DBNull)
                        {
                            PaperSelected = null;

                        }
                        else
                        {
                            ObjCR.PaperSelected = (dr["PaperSelected"].ToString());

                        }
                        ObjCR.ExamFormGenerated = (dr["ExamFormGenerated"].ToString());
                        var ExamFormGenerated = dr["ExamFormGenerated"];
                        if (ExamFormGenerated is DBNull)
                        {
                            ExamFormGenerated = null;

                        }
                        else
                        {
                            ObjCR.ExamFormGenerated = (dr["ExamFormGenerated"].ToString());

                        }
                        ObjCR.SeatNoGenerated = (dr["SeatNoGenerated"].ToString());
                        var SeatNoGenerated = dr["SeatNoGenerated"];
                        if (SeatNoGenerated is DBNull)
                        {
                            SeatNoGenerated = null;


                        }
                        else
                        {
                            ObjCR.SeatNoGenerated = (dr["SeatNoGenerated"].ToString());

                        }
                        ObjCR.VenueAllocation = (dr["VenueAllocation"].ToString());
                        var VenueAllocation = dr["VenueAllocation"];
                        if (VenueAllocation is DBNull)
                        {
                            VenueAllocation = null;


                        }
                        else
                        {
                            ObjCR.VenueAllocation = (dr["VenueAllocation"].ToString());

                        }
                        ObjCR.InwardModeStudent = (dr["InwardModeStudent"].ToString());
                        var InwardModeStudent = dr["InwardModeStudent"];
                        if (InwardModeStudent is DBNull)
                        {
                            InwardModeStudent = null;


                        }
                        else
                        {
                            ObjCR.InwardModeStudent = (dr["InwardModeStudent"].ToString());

                        }
                        ObjCR.NotInwardModeStudent = (dr["NotInwardModeStudent"].ToString());
                        var NotInwardModeStudent = dr["NotInwardModeStudent"];
                        if (NotInwardModeStudent is DBNull)
                        {
                            NotInwardModeStudent = null;
                        }
                        else
                        {
                            ObjCR.NotInwardModeStudent = (dr["NotInwardModeStudent"].ToString());

                        }
                        ObjCR.ExamFeesPaid = (dr["ExamFeesPaid"].ToString());
                        var ExamFeesPaid = dr["ExamFeesPaid"];
                        if (ExamFeesPaid is DBNull)
                        {
                            ExamFeesPaid = null;
                        }
                        else
                        {
                            ObjCR.ExamFeesPaid = (dr["ExamFeesPaid"].ToString());

                        }
                        ObjCR.NOExamFeesPaid = (dr["NOExamFeesPaid"].ToString());
                        var NOExamFeesPaid = dr["NOExamFeesPaid"];
                        if (NOExamFeesPaid is DBNull)
                        {
                            NOExamFeesPaid = null;
                        }
                        else
                        {
                            ObjCR.NOExamFeesPaid = (dr["NOExamFeesPaid"].ToString());

                        }

                        count = count + 1;
                        ObjCR.IndexId = count;
                        ObjLstCR.Add(ObjCR);
                    }

                }
                return Return.returnHttp("200", ObjLstCR, null);

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

                        objEEM.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
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


        #region PreExaminationStatisticsFacultyGet
        [HttpPost]
        public HttpResponseMessage PreExaminationStatisticsFacultyGet(PreExaminationStatistics ObjConReport)
        {
            PreExaminationStatistics modelobj = new PreExaminationStatistics();

            try
            {
                modelobj.FacultyId = ObjConReport.FacultyId;
                modelobj.ExamEventId = ObjConReport.ExamEventId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("PreExaminationStatisticsFaculty", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamEventId", modelobj.ExamEventId);
                cmd.Parameters.AddWithValue("@FacultyId", modelobj.FacultyId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<PreExaminationStatistics> ObjLstCR = new List<PreExaminationStatistics>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        PreExaminationStatistics ObjCR = new PreExaminationStatistics();

                        ObjCR.FacultyExamMapId = Convert.ToInt32(dr["FacultyExamMapId"].ToString());
                        ObjCR.ExamEventId = Convert.ToInt32(dr["ExamEventId"].ToString());
                        ObjCR.DisplayName = (dr["DisplayName"].ToString());
                        ObjCR.ScheduleCode = (dr["ScheduleCode"].ToString());
                        ObjCR.FacultyId = Convert.ToInt32(dr["FacultyId"].ToString());
                        ObjCR.FacultyName = (dr["FacultyName"].ToString());
                        ObjCR.InstituteId = Convert.ToInt32(dr["InstituteId"].ToString());
                        ObjCR.InstituteName = (dr["InstituteName"].ToString());
                        ObjCR.AdmittedStudents = (dr["AdmittedStudents"].ToString());
                        var AdmittedStudents = dr["AdmittedStudents"];
                        if (AdmittedStudents is DBNull)
                        {
                            AdmittedStudents = null;

                        }
                        else
                        {
                            ObjCR.AdmittedStudents = (dr["AdmittedStudents"].ToString());

                        }
                        ObjCR.PaperSelected = (dr["PaperSelected"].ToString());
                        var PaperSelected = dr["PaperSelected"];
                        if (PaperSelected is DBNull)
                        {
                            PaperSelected = null;

                        }
                        else
                        {
                            ObjCR.PaperSelected = (dr["PaperSelected"].ToString());

                        }
                        ObjCR.ExamFormGenerated = (dr["ExamFormGenerated"].ToString());
                        var ExamFormGenerated = dr["ExamFormGenerated"];
                        if (ExamFormGenerated is DBNull)
                        {
                            ExamFormGenerated = null;

                        }
                        else
                        {
                            ObjCR.ExamFormGenerated = (dr["ExamFormGenerated"].ToString());

                        }
                        ObjCR.SeatNoGenerated = (dr["SeatNoGenerated"].ToString());
                        var SeatNoGenerated = dr["SeatNoGenerated"];
                        if (SeatNoGenerated is DBNull)
                        {
                            SeatNoGenerated = null;


                        }
                        else
                        {
                            ObjCR.SeatNoGenerated = (dr["SeatNoGenerated"].ToString());

                        }
                        ObjCR.VenueAllocation = (dr["VenueAllocation"].ToString());
                        var VenueAllocation = dr["VenueAllocation"];
                        if (VenueAllocation is DBNull)
                        {
                            VenueAllocation = null;


                        }
                        else
                        {
                            ObjCR.VenueAllocation = (dr["VenueAllocation"].ToString());

                        }
                        ObjCR.InwardModeStudent = (dr["InwardModeStudent"].ToString());
                        var InwardModeStudent = dr["InwardModeStudent"];
                        if (InwardModeStudent is DBNull)
                        {
                            InwardModeStudent = null;


                        }
                        else
                        {
                            ObjCR.InwardModeStudent = (dr["InwardModeStudent"].ToString());

                        }
                        ObjCR.NotInwardModeStudent = (dr["NotInwardModeStudent"].ToString());
                        var NotInwardModeStudent = dr["NotInwardModeStudent"];
                        if (NotInwardModeStudent is DBNull)
                        {
                            NotInwardModeStudent = null;
                        }
                        else
                        {
                            ObjCR.NotInwardModeStudent = (dr["NotInwardModeStudent"].ToString());

                        }
                        ObjCR.ExamFeesPaid = (dr["ExamFeesPaid"].ToString());
                        var ExamFeesPaid = dr["ExamFeesPaid"];
                        if (ExamFeesPaid is DBNull)
                        {
                            ExamFeesPaid = null;
                        }
                        else
                        {
                            ObjCR.ExamFeesPaid = (dr["ExamFeesPaid"].ToString());

                        }
                        ObjCR.NOExamFeesPaid = (dr["NOExamFeesPaid"].ToString());
                        var NOExamFeesPaid = dr["NOExamFeesPaid"];
                        if (NOExamFeesPaid is DBNull)
                        {
                            NOExamFeesPaid = null;
                        }
                        else
                        {
                            ObjCR.NOExamFeesPaid = (dr["NOExamFeesPaid"].ToString());

                        }

                        count = count + 1;
                        ObjCR.IndexId = count;
                        ObjLstCR.Add(ObjCR);
                    }

                }
                return Return.returnHttp("200", ObjLstCR, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion


        #region PreExaminationStatisticsfromPPT
        [HttpPost]
        public HttpResponseMessage PreExaminationStatisticsfromPPT(PreExaminationStatistics ObjConReport)
        {
            PreExaminationStatistics modelobj = new PreExaminationStatistics();

            try
            {
                modelobj.FacultyId = ObjConReport.FacultyId;
                modelobj.ExamEventId = ObjConReport.ExamEventId;
                modelobj.InstituteId = ObjConReport.InstituteId;
                modelobj.FacultyExamMapId = ObjConReport.FacultyExamMapId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("PreExaminationStatisticsfromProgrammePartTerm", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyExamMapId", modelobj.FacultyExamMapId);
                cmd.Parameters.AddWithValue("@FacultyId", modelobj.FacultyId);
                cmd.Parameters.AddWithValue("@InstituteId", modelobj.InstituteId);
                cmd.Parameters.AddWithValue("@ExamEventId", modelobj.ExamEventId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<PreExaminationStatistics> ObjLstCR = new List<PreExaminationStatistics>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        PreExaminationStatistics ObjCR = new PreExaminationStatistics();

                        ObjCR.FacultyExamMapId = Convert.ToInt32(dr["FacultyExamMapId"].ToString());
                        ObjCR.ScheduleCode = (dr["ScheduleCode"].ToString());
                        ObjCR.ProgrammePartTermId = Convert.ToInt32(dr["ProgrammePartTermId"].ToString());
                        ObjCR.PartTermName = (dr["PartTermName"].ToString());
                        ObjCR.SpecialisationId = Convert.ToInt32(dr["SpecialisationId"].ToString());
                        ObjCR.BranchName = (dr["BranchName"].ToString());
                        ObjCR.FacultyId = Convert.ToInt32(dr["FacultyId"].ToString());
                        ObjCR.FacultyName = (dr["FacultyName"].ToString());
                        ObjCR.InstituteId = Convert.ToInt32(dr["InstituteId"].ToString());
                        ObjCR.InstituteName = (dr["InstituteName"].ToString());
                        ObjCR.ExamEventId = Convert.ToInt32(dr["ExamEventId"].ToString());
                        ObjCR.ExamMasterName = (dr["InstituteName"].ToString());

                        ObjCR.AdmittedStudents = (dr["AdmittedStudents"].ToString());
                        var AdmittedStudents = dr["AdmittedStudents"];
                        if (AdmittedStudents is DBNull)
                        {
                            AdmittedStudents = null;

                        }
                        else
                        {
                            ObjCR.AdmittedStudents = (dr["AdmittedStudents"].ToString());

                        }
                        ObjCR.PaperSelected = (dr["PaperSelected"].ToString());
                        var PaperSelected = dr["PaperSelected"];
                        if (PaperSelected is DBNull)
                        {
                            PaperSelected = null;

                        }
                        else
                        {
                            ObjCR.PaperSelected = (dr["PaperSelected"].ToString());

                        }
                        ObjCR.ExamFormGenerated = (dr["ExamFormGenerated"].ToString());
                        var ExamFormGenerated = dr["ExamFormGenerated"];
                        if (ExamFormGenerated is DBNull)
                        {
                            ExamFormGenerated = null;

                        }
                        else
                        {
                            ObjCR.ExamFormGenerated = (dr["ExamFormGenerated"].ToString());

                        }
                        ObjCR.SeatNoGenerated = (dr["SeatNoGenerated"].ToString());
                        var SeatNoGenerated = dr["SeatNoGenerated"];
                        if (SeatNoGenerated is DBNull)
                        {
                            SeatNoGenerated = null;


                        }
                        else
                        {
                            ObjCR.SeatNoGenerated = (dr["SeatNoGenerated"].ToString());

                        }
                        ObjCR.VenueAllocation = (dr["VenueAllocation"].ToString());
                        var VenueAllocation = dr["VenueAllocation"];
                        if (VenueAllocation is DBNull)
                        {
                            VenueAllocation = null;


                        }
                        else
                        {
                            ObjCR.VenueAllocation = (dr["VenueAllocation"].ToString());

                        }
                        ObjCR.TimeTablePublished = (dr["TimeTablePublished"].ToString());
                        var TimeTablePublished = dr["TimeTablePublished"];
                        if (TimeTablePublished is DBNull)
                        {
                            TimeTablePublished = null;


                        }
                        else
                        {
                            ObjCR.TimeTablePublished = (dr["TimeTablePublished"].ToString());

                        }
                        ObjCR.HallTicketPublished = (dr["HallTicketPublished"].ToString());
                        var HallTicketPublished = dr["HallTicketPublished"];
                        if (HallTicketPublished is DBNull)
                        {
                            HallTicketPublished = null;


                        }
                        else
                        {
                            ObjCR.HallTicketPublished = (dr["HallTicketPublished"].ToString());

                        }
                        ObjCR.InwardModeStudent = (dr["InwardModeStudent"].ToString());
                        var InwardModeStudent = dr["InwardModeStudent"];
                        if (InwardModeStudent is DBNull)
                        {
                            InwardModeStudent = null;


                        }
                        else
                        {
                            ObjCR.InwardModeStudent = (dr["InwardModeStudent"].ToString());

                        }
                        ObjCR.NotInwardModeStudent = (dr["NotInwardModeStudent"].ToString());
                        var NotInwardModeStudent = dr["NotInwardModeStudent"];
                        if (NotInwardModeStudent is DBNull)
                        {
                            NotInwardModeStudent = null;
                        }
                        else
                        {
                            ObjCR.NotInwardModeStudent = (dr["NotInwardModeStudent"].ToString());

                        }
                        ObjCR.ExamFeesPaid = (dr["ExamFeesPaid"].ToString());
                        var ExamFeesPaid = dr["ExamFeesPaid"];
                        if (ExamFeesPaid is DBNull)
                        {
                            ExamFeesPaid = null;
                        }
                        else
                        {
                            ObjCR.ExamFeesPaid = (dr["ExamFeesPaid"].ToString());

                        }
                        ObjCR.NOExamFeesPaid = (dr["NOExamFeesPaid"].ToString());
                        var NOExamFeesPaid = dr["NOExamFeesPaid"];
                        if (NOExamFeesPaid is DBNull)
                        {
                            NOExamFeesPaid = null;
                        }
                        else
                        {
                            ObjCR.NOExamFeesPaid = (dr["NOExamFeesPaid"].ToString());

                        }

                        count = count + 1;
                        ObjCR.IndexId = count;
                        ObjLstCR.Add(ObjCR);
                    }

                }
                return Return.returnHttp("200", ObjLstCR, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

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

        #region PreExaminationStatisticsFacultyfromPPT
        [HttpPost]
        public HttpResponseMessage PreExaminationStatisticsFacultyfromPPT(PreExaminationStatistics ObjConReport)
        {
            PreExaminationStatistics modelobj = new PreExaminationStatistics();

            try
            {
                modelobj.FacultyId = ObjConReport.FacultyId;
                modelobj.ExamEventId = ObjConReport.ExamEventId;
                modelobj.InstituteId = ObjConReport.InstituteId;
                modelobj.FacultyExamMapId = ObjConReport.FacultyExamMapId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("PreExaminationStatisticsFacultyfromProgrammePartTerm", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyExamMapId", modelobj.FacultyExamMapId);
                cmd.Parameters.AddWithValue("@FacultyId", modelobj.FacultyId);
                cmd.Parameters.AddWithValue("@InstituteId", modelobj.InstituteId);
                cmd.Parameters.AddWithValue("@ExamEventId", modelobj.ExamEventId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<PreExaminationStatistics> ObjLstCR = new List<PreExaminationStatistics>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        PreExaminationStatistics ObjCR = new PreExaminationStatistics();

                        ObjCR.FacultyExamMapId = Convert.ToInt32(dr["FacultyExamMapId"].ToString());
                        ObjCR.ScheduleCode = (dr["ScheduleCode"].ToString());
                        ObjCR.ProgrammePartTermId = Convert.ToInt32(dr["ProgrammePartTermId"].ToString());
                        ObjCR.PartTermName = (dr["PartTermName"].ToString());
                        ObjCR.SpecialisationId = Convert.ToInt32(dr["SpecialisationId"].ToString());
                        ObjCR.BranchName = (dr["BranchName"].ToString());
                        ObjCR.FacultyId = Convert.ToInt32(dr["FacultyId"].ToString());
                        ObjCR.FacultyName = (dr["FacultyName"].ToString());
                        ObjCR.InstituteId = Convert.ToInt32(dr["InstituteId"].ToString());
                        ObjCR.InstituteName = (dr["InstituteName"].ToString());
                        ObjCR.ExamEventId = Convert.ToInt32(dr["ExamEventId"].ToString());
                        ObjCR.ExamMasterName = (dr["InstituteName"].ToString());

                        ObjCR.AdmittedStudents = (dr["AdmittedStudents"].ToString());
                        var AdmittedStudents = dr["AdmittedStudents"];
                        if (AdmittedStudents is DBNull)
                        {
                            AdmittedStudents = null;

                        }
                        else
                        {
                            ObjCR.AdmittedStudents = (dr["AdmittedStudents"].ToString());

                        }
                        ObjCR.PaperSelected = (dr["PaperSelected"].ToString());
                        var PaperSelected = dr["PaperSelected"];
                        if (PaperSelected is DBNull)
                        {
                            PaperSelected = null;

                        }
                        else
                        {
                            ObjCR.PaperSelected = (dr["PaperSelected"].ToString());

                        }
                        ObjCR.ExamFormGenerated = (dr["ExamFormGenerated"].ToString());
                        var ExamFormGenerated = dr["ExamFormGenerated"];
                        if (ExamFormGenerated is DBNull)
                        {
                            ExamFormGenerated = null;

                        }
                        else
                        {
                            ObjCR.ExamFormGenerated = (dr["ExamFormGenerated"].ToString());

                        }
                        ObjCR.SeatNoGenerated = (dr["SeatNoGenerated"].ToString());
                        var SeatNoGenerated = dr["SeatNoGenerated"];
                        if (SeatNoGenerated is DBNull)
                        {
                            SeatNoGenerated = null;


                        }
                        else
                        {
                            ObjCR.SeatNoGenerated = (dr["SeatNoGenerated"].ToString());

                        }
                        ObjCR.VenueAllocation = (dr["VenueAllocation"].ToString());
                        var VenueAllocation = dr["VenueAllocation"];
                        if (VenueAllocation is DBNull)
                        {
                            VenueAllocation = null;


                        }
                        else
                        {
                            ObjCR.VenueAllocation = (dr["VenueAllocation"].ToString());

                        }
                        ObjCR.TimeTablePublished = (dr["TimeTablePublished"].ToString());
                        var TimeTablePublished = dr["TimeTablePublished"];
                        if (TimeTablePublished is DBNull)
                        {
                            TimeTablePublished = null;


                        }
                        else
                        {
                            ObjCR.TimeTablePublished = (dr["TimeTablePublished"].ToString());

                        }
                        ObjCR.HallTicketPublished = (dr["HallTicketPublished"].ToString());
                        var HallTicketPublished = dr["HallTicketPublished"];
                        if (HallTicketPublished is DBNull)
                        {
                            HallTicketPublished = null;


                        }
                        else
                        {
                            ObjCR.HallTicketPublished = (dr["HallTicketPublished"].ToString());

                        }
                        ObjCR.InwardModeStudent = (dr["InwardModeStudent"].ToString());
                        var InwardModeStudent = dr["InwardModeStudent"];
                        if (InwardModeStudent is DBNull)
                        {
                            InwardModeStudent = null;


                        }
                        else
                        {
                            ObjCR.InwardModeStudent = (dr["InwardModeStudent"].ToString());

                        }
                        ObjCR.NotInwardModeStudent = (dr["NotInwardModeStudent"].ToString());
                        var NotInwardModeStudent = dr["NotInwardModeStudent"];
                        if (NotInwardModeStudent is DBNull)
                        {
                            NotInwardModeStudent = null;
                        }
                        else
                        {
                            ObjCR.NotInwardModeStudent = (dr["NotInwardModeStudent"].ToString());

                        }
                        ObjCR.ExamFeesPaid = (dr["ExamFeesPaid"].ToString());
                        var ExamFeesPaid = dr["ExamFeesPaid"];
                        if (ExamFeesPaid is DBNull)
                        {
                            ExamFeesPaid = null;
                        }
                        else
                        {
                            ObjCR.ExamFeesPaid = (dr["ExamFeesPaid"].ToString());

                        }
                        ObjCR.NOExamFeesPaid = (dr["NOExamFeesPaid"].ToString());
                        var NOExamFeesPaid = dr["NOExamFeesPaid"];
                        if (NOExamFeesPaid is DBNull)
                        {
                            NOExamFeesPaid = null;
                        }
                        else
                        {
                            ObjCR.NOExamFeesPaid = (dr["NOExamFeesPaid"].ToString());

                        }

                        count = count + 1;
                        ObjCR.IndexId = count;
                        ObjLstCR.Add(ObjCR);
                    }

                }
                return Return.returnHttp("200", ObjLstCR, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion



    }
}