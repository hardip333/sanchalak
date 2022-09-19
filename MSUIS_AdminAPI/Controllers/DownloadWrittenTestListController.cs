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
    public class DownloadWrittenTestListController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        string PhotoURL = "https://admission.msubaroda.ac.in/MSUISApi/Upload/Photo/";
        string SignatureURL = "https://admission.msubaroda.ac.in/MSUISApi/Upload/Signature/";

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

        #region ProgPartTermGetByInstIdandYearId
        [HttpPost]
        public HttpResponseMessage ProgPartTermGetByInstIdandYearId(ProgrammeInstancePartTerm ObjProgInstPartTerm)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("ProgPartTermGetByInstIdandYearId", con);
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

        #region WrittenTestScheduleGet
        [HttpPost]
        public HttpResponseMessage WrittenTestScheduleGet(WrittenTestSchedule ObjWTSC)
        {
            WrittenTestSchedule modelobj = new WrittenTestSchedule();
            try
            {
                modelobj.ProgrammeInstancePartTermId = ObjWTSC.ProgrammeInstancePartTermId;


                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("WrittenTestScheduleDetailsGet", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@ProgrammeInstancePartTermId", modelobj.ProgrammeInstancePartTermId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<WrittenTestSchedule> ObjWTSchedule = new List<WrittenTestSchedule>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        WrittenTestSchedule ObjWTS = new WrittenTestSchedule();
                        ObjWTS.Venue = Convert.ToString(dr["Venue"]);
                        ObjWTS.ExamDate = Convert.ToString(dr["ExamDate"]);
                        ObjWTS.StartTimeView = Convert.ToString(dr["StartTime"]);
                        ObjWTS.EndTimeView = Convert.ToString(dr["EndTime"]);
                        ObjWTSchedule.Add(ObjWTS);

                    }
                    return Return.returnHttp("200", ObjWTSchedule, null);
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

        #region getWrittenTestApplicantsList

        [HttpPost]
        public HttpResponseMessage getWrittenTestApplicantsList(DownloadWrittenTestList ObjWrittenTest)
        {
            DownloadWrittenTestList modelobj = new DownloadWrittenTestList();

            try
            {
                modelobj.ProgrammeInstancePartTermId = ObjWrittenTest.ProgrammeInstancePartTermId;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("WrittenTestApplicantsListGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", modelobj.ProgrammeInstancePartTermId);
                da.SelectCommand = cmd;
                da.Fill(dt);
                Int32 count = 0;
                List<DownloadWrittenTestList> ObjWRTest = new List<DownloadWrittenTestList>();
                BALCommon common = new BALCommon();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        DownloadWrittenTestList ObjWT = new DownloadWrittenTestList();
                        ObjWT.ApplicationId = Convert.ToInt64(dr["ApplicationId"]);
                        ObjWT.ApplicantUserName = Convert.ToInt64(dr["ApplicantUserName"]);
                        ObjWT.EntranceTestSeatNo = Convert.ToInt64(dr["EntranceTestSeatNo"]);
                        ObjWT.FullName = (dr["FullName"].ToString());
                        //if(ObjWT.ApplicantPhoto != "" || ObjWT.ApplicantPhoto != null)
                        //{
                        //    ObjWT.ApplicantPhoto = PhotoURL + (dr["ApplicantPhoto"].ToString());
                        //}
                        //else
                        //{
                        //    ObjWT.ApplicantPhoto = PhotoURL + "DefaultPhoto.png";
                        //}

                        //if (ObjWT.ApplicantSignature != "" || ObjWT.ApplicantSignature != null)
                        //{
                        //    ObjWT.ApplicantSignature = SignatureURL + (dr["ApplicantSignature"].ToString());
                        //}
                        //else
                        //{
                        //    ObjWT.ApplicantSignature = SignatureURL + "DefaultPhoto.png";
                        //}

                        //Start=============For Applicant Photo==================
                        string picinfophoto = (dr["ApplicantPhoto"].ToString());
                        string pathphoto = PhotoURL + picinfophoto;
                        bool NewFileResponsePhoto = common.ServerFileExists(pathphoto);

                        if (NewFileResponsePhoto == true)
                        {
                            ObjWT.ApplicantPhoto = pathphoto;
                        }
                        else if (NewFileResponsePhoto == false)
                        {
                            string pathphotoDefault = PhotoURL + "JPGDefaultPhoto.jpg";
                            ObjWT.ApplicantPhoto = pathphotoDefault;
                        }
                        //End=============For Applicant Photo==================

                        //Start=============For Applicant Signature==================
                        string picinfosignature = (dr["ApplicantSignature"].ToString());
                        string pathsignature = SignatureURL + picinfosignature;
                        bool NewFileResponseSign = common.ServerFileExists(pathsignature);

                        if (NewFileResponseSign == true)
                        {
                            ObjWT.ApplicantSignature = pathsignature;
                        }
                        else if (NewFileResponseSign == false)
                        {
                            string pathSignDefault = SignatureURL + "JPGDefaultSign.jpg";
                            ObjWT.ApplicantSignature = pathSignDefault;
                        }   
                        //End=============For Applicant Signature==================

                        count = count + 1;
                        ObjWT.IndexId = count;

                        ObjWRTest.Add(ObjWT);
                    }
                    return Return.returnHttp("200", ObjWRTest, null);
                }
                else
                {
                    return Return.returnHttp("200", "No Record Found", null);
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

