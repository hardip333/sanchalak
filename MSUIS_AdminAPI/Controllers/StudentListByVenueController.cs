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
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class StudentListByVenueController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable Dt = new DataTable();


        #region StudentListByVenueGet
        [HttpPost]
        public HttpResponseMessage StudentListByVenueGet(StudentListByVenue ObjSLV)
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
                    

                    List<StudentListByVenue> ObjSLlist = new List<StudentListByVenue>();
                    

                    Int32 count = 0;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@ExamMasterId", ObjSLV.ExamMasterId);
                    cmd.Parameters.AddWithValue("@FacultyExamMapId", ObjSLV.FacultyExamMapId);
                    cmd.Parameters.AddWithValue("@ProgrammeId", ObjSLV.ProgrammeId);
                    cmd.Parameters.AddWithValue("@SpecialisationId", ObjSLV.BranchId);
                    cmd.Parameters.AddWithValue("@ProgrammePartTermId", ObjSLV.ProgrammePartTermId);
                    cmd.Parameters.AddWithValue("@ExamVenueId", ObjSLV.ExamVenueId);
                    cmd.CommandText = "StudentListByVenue";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    da.SelectCommand = cmd;
                    da.Fill(Dt);

                    
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            StudentListByVenue ObjSL = new StudentListByVenue();
                            

                            ObjSL.PRN = Convert.ToInt64(Dt.Rows[i]["PRN"]);
                            ObjSL.ExamMasterId = (Convert.ToString(Dt.Rows[i]["ExamMasterId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ExamMasterId"]);
                            ObjSL.ProgrammeId = (Convert.ToString(Dt.Rows[i]["ProgrammeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);
                            ObjSL.BranchId = (Convert.ToString(Dt.Rows[i]["BranchId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["BranchId"]);
                            ObjSL.ProgrammePartTermId = (Convert.ToString(Dt.Rows[i]["ProgrammePartTermId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammePartTermId"]);
                            ObjSL.ExamVenueId = (Convert.ToString(Dt.Rows[i]["ExamVenueId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ExamVenueId"]);
                            ObjSL.ScheduleCode = (Convert.ToString(Dt.Rows[i]["ScheduleCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ScheduleCode"]);
                            ObjSL.SeatNumber = (Convert.ToString(Dt.Rows[i]["SeatNumber"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["SeatNumber"]);
                            ObjSL.FullName = (Convert.ToString(Dt.Rows[i]["FullName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FullName"]);
                            ObjSL.AppearanceType = (Convert.ToString(Dt.Rows[i]["AppearanceType"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["AppearanceType"]);
                            ObjSL.Gender = (Convert.ToString(Dt.Rows[i]["Gender"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["Gender"]);
                            ObjSL.ApplicationReservationName = (Convert.ToString(Dt.Rows[i]["ApplicationReservationName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ApplicationReservationName"]);
                            ObjSL.ExamEventName = (Convert.ToString(Dt.Rows[i]["ExamEventName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ExamEventName"]);
                        ObjSL.StudentCount = (Convert.ToString(Dt.Rows[i]["StudentCount"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["StudentCount"]);
                        ObjSL.PartTermShortName = (Convert.ToString(Dt.Rows[i]["PartTermShortName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PartTermShortName"]);
                            ObjSL.ExamVenueName = (Convert.ToString(Dt.Rows[i]["ExamVenueName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ExamVenueName"]);
                            ObjSL.ExamVenueAddress = (Convert.ToString(Dt.Rows[i]["ExamVenueAddress"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ExamVenueAddress"]);
                            ObjSL.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                            ObjSL.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["BranchName"]);
                            ObjSL.PaperList = (Convert.ToString(Dt.Rows[i]["PaperList"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PaperList"]);
                        count = count + 1;
                            ObjSL.IndexId = count;


                            ObjSLlist.Add(ObjSL);                        



                        }
                    
                    
                    return Return.returnHttp("200", ObjSLlist, null);
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

        
        #region ExamVenueGetByMstProgrammePartTermIdGet
        [HttpPost]
        public HttpResponseMessage ExamVenueGetByMstProgrammePartTermIdGet(ExamVenue ObjEV)
        {
            try
            {
                
                SqlCommand Cmd = new SqlCommand("ExamVenueGetByMstProgrammePartTermId", con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@ProgrammePartTermId", ObjEV.ProgrammePartTermId);
                sda.SelectCommand = Cmd;

                sda.Fill(Dt);


                List<ExamVenue> ObjLstEEM = new List<ExamVenue>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ExamVenue objEEM = new ExamVenue();

                        objEEM.ExamVenueId = Convert.ToInt32(Dt.Rows[i]["ExamVenueId"]);
                        objEEM.ExamVenueName = Convert.ToString(Dt.Rows[i]["ExamVenueName"]);
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
    }
}