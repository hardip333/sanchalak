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
using System.Net.Mail;
using System.Web.Http;
using System.Web.WebPages;

namespace MSUISApi.Controllers

{
    public class SeatingArrangementReportController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();


        #region SeatingArrangementReportGet
        [HttpPost]
        public HttpResponseMessage SeatingArrangementReportGet(SeatingArrangementReport ObjSAR)
        {
            SeatingArrangementReport modelobj = new SeatingArrangementReport();

            try
            {
                //modelobj.FacultyExamMapId = ObjConReport.FacultyExamMapId;
                modelobj.ExamMasterId = ObjSAR.ExamMasterId;
                modelobj.ExamSlotId = ObjSAR.ExamSlotId;
                modelobj.ExamVenueId = ObjSAR.ExamVenueId;
                modelobj.InstituteId = ObjSAR.InstituteId;
                modelobj.ExamVenueExamCenterId = ObjSAR.ExamVenueExamCenterId;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("SeatingArrangementReport", con);
                TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime date1 = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(ObjSAR.ExamDate).ToUniversalTime(), INDIAN_ZONE);
                //DateTime date2 = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(ObjSAR.ExamDate2).ToUniversalTime(), INDIAN_ZONE);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamMasterId", modelobj.ExamMasterId);
                cmd.Parameters.AddWithValue("@ExamSlotId", modelobj.ExamSlotId);
                cmd.Parameters.AddWithValue("@ExamVenueId", modelobj.ExamVenueId);
                cmd.Parameters.AddWithValue("@InstituteId", modelobj.InstituteId);
                cmd.Parameters.AddWithValue("@ExamVenueExamCenterId", modelobj.ExamVenueExamCenterId);
                //ObjBlock.PaperDate = Convert.ToString(date1.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@Date", date1.ToString("yyyy-MM-dd"));
                //cmd.Parameters.AddWithValue("@Date1", date2.ToString("yyyy-MM-dd"));

                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<SeatingArrangementReport> ObjLstSA = new List<SeatingArrangementReport>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        SeatingArrangementReport ObjSA = new SeatingArrangementReport();


                        ObjSA.BlockNo = dr["BlockNo"].ToString();

                        DateTime ExamDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(dr["ExamDate"]).ToUniversalTime(), INDIAN_ZONE);
                        ObjSA.ExamDate = string.Format("{0: dd-MM-yyyy}", ExamDate);

                        //DateTime ExamDate2 = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(dr["ExamDate"]).ToUniversalTime(), INDIAN_ZONE);
                        //ObjSA.ExamDate2 = string.Format("{0: dd-MM-yyyy}", ExamDate2);


                        ObjSA.RoomNo = (Convert.ToString(dr["RoomNo"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["RoomNo"]);


                        ObjSA.ProgrammePartTermId = Convert.ToInt32(dr["ProgrammePartTermId"]);

                        ObjSA.PartBranchName = dr["PartBranchName"].ToString();
                        ObjSA.InstituteId = Convert.ToInt32(dr["InstituteId"].ToString());
                        ObjSA.InstituteName = (Convert.ToString(dr["InstituteName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["InstituteName"]);
                        ObjSA.SlotName = (Convert.ToString(dr["SlotName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["SlotName"]);
                        ObjSA.FromSeatNumber = (Convert.ToString(dr["FromSeatNumber"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["FromSeatNumber"]);
                        ObjSA.ToSeatNo = (Convert.ToString(dr["ToSeatNo"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["ToSeatNo"]);
                        ObjSA.TotalSeatNumber = (Convert.ToString(dr["TotalSeatNumber"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["TotalSeatNumber"]);
                        ObjSA.Details = (Convert.ToString(dr["Details"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["Details"]);
                        ObjSA.ExamCenterName = (Convert.ToString(dr["ExamCenterName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["ExamCenterName"]);

                        ObjSA.PaperStartTime = (Convert.ToString(dr["PaperStartTime"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["PaperStartTime"]);

                        ObjSA.PaperEndTime = (Convert.ToString(dr["PaperEndTime"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["PaperEndTime"]);

                        ObjSA.SeatingArrangeMent1 = Convert.ToBoolean(dr["SeatingArrangeMent"]);
                        if (ObjSA.SeatingArrangeMent1 == true)
                        {
                            ObjSA.SeatingArrangeMent = "Paper Wise Block Allocation";
                        }
                        else
                        {
                            ObjSA.SeatingArrangeMent = null;
                        }

                        count = count + 1;
                        ObjSA.IndexId = count;
                        ObjLstSA.Add(ObjSA);
                    }

                }
                return Return.returnHttp("200", ObjLstSA, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion


        #region ExamEventMasterListGet
        [HttpPost]
        public HttpResponseMessage ExamEventMasterListGet()
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
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

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                List<ExamEventMaster> ObjListEV = new List<ExamEventMaster>();

                cmd.CommandText = "ExamEventGet";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow DR in dt.Rows)
                {
                    ExamEventMaster s = new ExamEventMaster();

                    s.Id = Convert.ToInt32(DR["Id"].ToString());
                    s.ExamMasterId = Convert.ToInt32(DR["Id"].ToString());
                    s.DisplayName = DR["DisplayName"].ToString();

                    ObjListEV.Add(s);
                }
                return Return.returnHttp("200", ObjListEV, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region ExamVenueListGet
        [HttpPost]
        public HttpResponseMessage ExamVenueListGet(ExamVenue ObjEV)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
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

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                List<ExamVenue> ObjListEV = new List<ExamVenue>();


                cmd.CommandText = "ExamVenueListGetByInstId";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InstituteId", ObjEV.InstituteId);
                cmd.Connection = con;
                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow DR in dt.Rows)
                {
                    ExamVenue s = new ExamVenue();

                    s.Id = Convert.ToInt32(DR["Id"].ToString());
                    s.ExamVenueId = Convert.ToInt32(DR["Id"].ToString());
                    s.ExamVenueName = DR["ExamVenueName"].ToString();
                    s.InstituteId = Convert.ToInt32(DR["InstituteId"].ToString());

                    ObjListEV.Add(s);
                }
                return Return.returnHttp("200", ObjListEV, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region ExamSlotMasterGet
        [HttpPost]
        public HttpResponseMessage ExamSlotMasterGet(ExamSlotMaster ObjESM)
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
                var timespan = TimeSpan.FromHours(24);
                SqlCommand cmd = new SqlCommand("ExamSlotGetByExamDate", con);
                TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime date = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(ObjESM.ExamDate).ToUniversalTime(), INDIAN_ZONE);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Date", date.ToString("yyyy-MM-dd"));
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<ExamSlotMaster> ObjSLList = new List<ExamSlotMaster>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ExamSlotMaster ObjSL = new ExamSlotMaster();

                        ObjSL.Id = Convert.ToInt64(dr["Id"].ToString());
                        ObjSL.ExamSlotId = Convert.ToInt64(dr["Id"].ToString());
                        ObjSL.SlotName = Convert.ToString(dr["SlotName"]);


                        ObjSLList.Add(ObjSL);
                    }
                }
                return Return.returnHttp("200", ObjSLList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region ExamVenueExamCenterGet
        [HttpPost]
        public HttpResponseMessage ExamVenueExamCenterGet(ExamVenueExamCenter1 ObjEV)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
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

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                List<ExamVenueExamCenter1> ObjListEV = new List<ExamVenueExamCenter1>();


                cmd.CommandText = "ExamVenueExamCenterGetByExamVenueId";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamVenueId", ObjEV.ExamVenueId);
                cmd.Connection = con;
                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow DR in dt.Rows)
                {
                    ExamVenueExamCenter1 s = new ExamVenueExamCenter1();

                    s.Id = Convert.ToInt32(DR["Id"].ToString());
                    s.ExamVenueExamCenterId = Convert.ToInt32(DR["Id"].ToString());
                    s.CenterName = DR["CenterName"].ToString();

                    ObjListEV.Add(s);
                }
                return Return.returnHttp("200", ObjListEV, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion


        #region BlockWisePaperReportGet
        [HttpPost]
        public HttpResponseMessage BlockWisePaperReportGet(BlockWisePaperReport ObjBAP)
        {
            SeatingArrangementReport modelobj = new SeatingArrangementReport();

            try
            {
                //modelobj.FacultyExamMapId = ObjConReport.FacultyExamMapId;
                modelobj.ExamMasterId = ObjBAP.ExamMasterId;
                modelobj.ExamSlotId = ObjBAP.ExamSlotId;
                modelobj.ExamVenueId = ObjBAP.ExamVenueId;
                modelobj.InstituteId = ObjBAP.InstituteId;
                modelobj.ExamVenueExamCenterId = ObjBAP.ExamVenueExamCenterId;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("BlockAllocationPaperWise", con);
                TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime date1 = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(ObjBAP.ExamDate).ToUniversalTime(), INDIAN_ZONE);
                //DateTime date2 = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(ObjSAR.ExamDate2).ToUniversalTime(), INDIAN_ZONE);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamMasterId", modelobj.ExamMasterId);
                cmd.Parameters.AddWithValue("@ExamSlotId", modelobj.ExamSlotId);
                cmd.Parameters.AddWithValue("@ExamVenueId", modelobj.ExamVenueId);
                cmd.Parameters.AddWithValue("@InstituteId", modelobj.InstituteId);
                cmd.Parameters.AddWithValue("@ExamVenueExamCenterId", modelobj.ExamVenueExamCenterId);
                //ObjBlock.PaperDate = Convert.ToString(date1.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@Date", date1.ToString("yyyy-MM-dd"));
                //cmd.Parameters.AddWithValue("@Date1", date2.ToString("yyyy-MM-dd"));

                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<BlockWisePaperReport> ObjLstBP = new List<BlockWisePaperReport>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        BlockWisePaperReport ObjBP = new BlockWisePaperReport();


                        ObjBP.BlockNo = dr["BlockNo"].ToString();

                        DateTime ExamDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(dr["ExamDate"]).ToUniversalTime(), INDIAN_ZONE);
                        ObjBP.ExamDate = string.Format("{0: dd-MM-yyyy}", ExamDate);

                        ObjBP.SlotName = (Convert.ToString(dr["SlotName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["SlotName"]);

                        ObjBP.ExamBlockName = (Convert.ToString(dr["ExamBlockName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["ExamBlockName"]);


                        ObjBP.RoomNo = (Convert.ToString(dr["RoomNo"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["RoomNo"]);


                        ObjBP.ProgInstancePartTermId = Convert.ToInt64(dr["ProgInstancePartTermId"]);

                        ObjBP.PartBranchName = dr["PartBranchName"].ToString();

                        ObjBP.PaperName = (Convert.ToString(dr["PaperName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["PaperName"]);

                        ObjBP.PaperCode = (Convert.ToString(dr["PaperCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["PaperCode"]);

                        ObjBP.ExamVenueName = (Convert.ToString(dr["ExamVenueName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["ExamVenueName"]);

                        ObjBP.StudentCount = (Convert.ToString(dr["StudentCount"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["StudentCount"]);

                        ObjBP.InstituteId = Convert.ToInt32(dr["InstituteId"].ToString());

                        ObjBP.InstituteName = (Convert.ToString(dr["InstituteName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["InstituteName"]);


                        ObjBP.ExamVenueExamCenterName = (Convert.ToString(dr["ExamVenueExamCenterName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["ExamVenueExamCenterName"]);

                        ObjBP.PaperStartTime = (Convert.ToString(dr["PaperStartTime"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["PaperStartTime"]);

                        ObjBP.PaperEndTime = (Convert.ToString(dr["PaperEndTime"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["PaperEndTime"]);



                        ObjBP.BlockAllocationPaper1 = Convert.ToBoolean(dr["BlockAllocationPaper"]);
                        if (ObjBP.BlockAllocationPaper1 == true)
                        {
                            ObjBP.BlockAllocationPaper = "Paper Wise Block Allocation";
                        }
                        else
                        {
                            ObjBP.BlockAllocationPaper = null;
                        }


                        count = count + 1;
                        ObjBP.IndexId = count;
                        ObjLstBP.Add(ObjBP);
                    }

                }
                return Return.returnHttp("200", ObjLstBP, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region InstituteGetById
        [HttpPost]
        public HttpResponseMessage InstituteGetById(MstInstitute MF)
        {
            //MstFaculty modelobj = new MstFaculty(); 
            try
            {
                //modelobj.Id = MF.Id;
                SqlCommand cmd = new SqlCommand("MstIntGetById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InstituteId", MF.Id);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<MstInstitute> ObjLstF = new List<MstInstitute>();
                //dt.Rows.Add("0", "All Faculty");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstInstitute ObjF = new MstInstitute();
                        ObjF.Id = Convert.ToInt32(dr["Id"]);
                        ObjF.InstituteName = (dr["InstituteName"].ToString());

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












    }
}