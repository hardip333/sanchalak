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
using System.IO;

namespace MSUISApi.Controllers
{
    public class ReportForJrSupervisorIAExamController : ApiController 
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));


        #region ExamEventMasterGet
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

                SqlCommand cmd = new SqlCommand("ExamEventMasterGet", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<JrSupervisorIA> ObjJSP = new List<JrSupervisorIA>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        JrSupervisorIA Objspv = new JrSupervisorIA();
                        Objspv.Id = Convert.ToInt64(dr["Id"]);
                        Objspv.DisplayName = (dr["DisplayName"].ToString());
                        ObjJSP.Add(Objspv);
                    }

                }
                return Return.returnHttp("200", ObjJSP, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region JrSuperVisorReportIAPaperList
        [HttpPost]
        public HttpResponseMessage JrSuperVisorReportIAPaperList(ProgIncPartTermGet JRS)
        {
            try
            {
                Int64 BranchId = JRS.BranchId;
                Int64 ProgrammePartTermId = JRS.ProgrammePartTermId;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("JrSuperVisorReportIAPaperList", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchId", BranchId);
                cmd.Parameters.AddWithValue("@ProgrammePartTermId", ProgrammePartTermId);
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                Int32 count = 0;
                List<ProgIncPartTermGet> ObjSPList1 = new List<ProgIncPartTermGet>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        ProgIncPartTermGet ObjspList1 = new ProgIncPartTermGet();
                        ObjspList1.PaperId = Convert.ToInt64(dr["PaperId"].ToString());
                        ObjspList1.PaperCode = (dr["PaperCode"].ToString());
                        ObjspList1.PaperName = (dr["PaperName"].ToString());
                        ObjspList1.ExamDate = (dr["ExamDate"].ToString());
                        count = count + 1;
                        ObjspList1.IndexId = count;
                        ObjSPList1.Add(ObjspList1);
                    }

                }
                return Return.returnHttp("200", ObjSPList1, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region JrSuperVisorReportIAStudentsList
        [HttpPost]
        public HttpResponseMessage JrSuperVisorReportIAStudentsList(ProgIncPartTermGet JRS)
        {
            try
            {
                Int64 BranchId = JRS.BranchId;
                Int64 ProgrammePartTermId = JRS.ProgrammePartTermId;
                Int64 ExamEventId = JRS.ExamEventId;
                Int64 PaperId = JRS.PaperId;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("JrSuperVisorReportIAStudentsList", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchId", BranchId);
                cmd.Parameters.AddWithValue("@ProgrammePartTermId", ProgrammePartTermId);
                cmd.Parameters.AddWithValue("@ExamEventId", ExamEventId);
                cmd.Parameters.AddWithValue("@PaperId", PaperId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                Int32 count = 0;

                BALCommon common = new BALCommon();

                string OldBaseUrlForPhoto = "https://admission.msubaroda.ac.in/MSUISApi/Upload/Photo/";
                //string NewBaseUrlForPhoto = "https://localhost:44374/Upload/Photo/";
                string NewBaseUrlForPhoto = "https://msuis.msubaroda.ac.in/Vidhyarthi_API/Upload/Photo/";
              
                string OldBaseUrlForSign = "https://admission.msubaroda.ac.in/MSUISApi/Upload/Signature/";
                //string NewBaseUrlForSign = "https://localhost:44374/Upload/Signature/";
                string NewBaseUrlForSign = "https://msuis.msubaroda.ac.in/Vidhyarthi_API/Upload/Signature/";

                

                List<JrSupervisorReportIA> ObjSPList2 = new List<JrSupervisorReportIA>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        JrSupervisorReportIA ObjspList2 = new JrSupervisorReportIA();
                        ObjspList2.FullName = Convert.ToString(dr["LastName"]) + " " + Convert.ToString(dr["FirstName"]) + " " + Convert.ToString(dr["MiddleName"]);
                        ObjspList2.PRN = Convert.ToInt64(dr["PRN"]);
                        ObjspList2.SeatNumber = (dr["SeatNumber"].ToString());
                        ObjspList2.StudentSignature = (dr["StudentSignature"].ToString());
                        ObjspList2.StudentPhoto = (dr["StudentPhoto"].ToString());
                        ObjspList2.InstructionMediumName = (dr["InstructionMediumName"].ToString());
                        ObjspList2.ExamEventName = (dr["ExamEventName"].ToString());
                        ObjspList2.ProgrammePartTermName = (dr["ProgrammePartTermName"].ToString());
                        ObjspList2.FromSeatNo = Convert.ToString(dr["FromSeatNo"]);
                        ObjspList2.ToSeatNo = Convert.ToString(dr["ToSeatNo"]);
                        ObjspList2.TotalStudentCount = Convert.ToString(dr["TotalStudentCount"]);

                        //==========Start Code for Fetching Photo from Vidhyarthi or Applicant Side============
                        string FullPathOld = OldBaseUrlForPhoto + ObjspList2.StudentPhoto;
                        string FullPathNew = NewBaseUrlForPhoto + ObjspList2.StudentPhoto;

                        bool FileResponseOldPath = common.ServerFileExists(FullPathOld);
                        bool FileResponseNewPath = common.ServerFileExists(FullPathNew);

                        if (FileResponseNewPath == true)
                        {
                            ObjspList2.StudentPhoto = FullPathNew;
                        }
                        else if (FileResponseOldPath == true)
                        {
                            ObjspList2.StudentPhoto = FullPathOld;
                        }
                        else
                        {
                            ObjspList2.StudentPhoto = NewBaseUrlForPhoto + "DefaultPhoto.png";
                        }
                        //============End Code for Fetching Photo from Vidhyarthi or Applicant Side============

                        //============Start Code for Fetching Signature from Vidhyarthi or Applicant Side============
                        string FullPathOldSign = OldBaseUrlForSign + ObjspList2.StudentSignature;
                        string FullPathNewSign = NewBaseUrlForSign + ObjspList2.StudentSignature;

                        bool FileResponseOldPathSign = common.ServerFileExists(FullPathOldSign);
                        bool FileResponseNewPathSign = common.ServerFileExists(FullPathNewSign);

                        if (FileResponseNewPathSign == true)
                        {
                            ObjspList2.StudentSignature = FullPathNewSign;
                        }
                        else if (FileResponseOldPathSign == true)
                        {
                            ObjspList2.StudentSignature = FullPathOldSign;
                        }
                        else
                        {
                            ObjspList2.StudentSignature = NewBaseUrlForSign + "DefaultPhoto.png";
                        }
                        //===========End Code for Fetching Signature from Vidhyarthi or Applicant Side============

                        count = count + 1;
                        ObjspList2.IndexId = count;
                        ObjSPList2.Add(ObjspList2);
                    }

                }
                return Return.returnHttp("200", ObjSPList2, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        //#region JrSuperVisorReportIAProgIncPartTermGet
        //[HttpPost]
        //public HttpResponseMessage JrSuperVisorReportIAProgIncPartTermGet(ProgIncPartTermGet JRS)
        //{
        //    try
        //    {
        //        Int64 InstituteId = JRS.InstituteId;

        //        String token = Request.Headers.GetValues("token").FirstOrDefault();
        //        TokenOperation ac = new TokenOperation();
        //        string res = ac.ValidateToken(token);

        //        if (res == "0")
        //        {
        //            return Return.returnHttp("0", null, null);
        //        }

        //        SqlCommand cmd = new SqlCommand("JrSuperVisorReportIAProgIncPartTermGet", Con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@InstituteId", InstituteId);
        //        Da.SelectCommand = cmd;
        //        Da.Fill(Dt);
        //        List<ProgIncPartTermGet> ObjSPList1 = new List<ProgIncPartTermGet>();

        //        if (Dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow dr in Dt.Rows)
        //            {
        //                ProgIncPartTermGet ObjspList1 = new ProgIncPartTermGet();
        //                ObjspList1.InstituteId = Convert.ToInt64(dr["InstituteId"]);
        //                ObjspList1.ProgInstPartTermId = Convert.ToInt64(dr["ProgInstPartTermId"]);
        //                ObjspList1.InstancePartTermName = (dr["InstancePartTermName"].ToString());
        //                ObjSPList1.Add(ObjspList1);
        //            }

        //        }
        //        return Return.returnHttp("200", ObjSPList1, null);

        //    }
        //    catch (Exception e)
        //    {
        //        return Return.returnHttp("201", e.Message.ToString(), null);

        //    }
        //}
        //#endregion
    }
}
