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
    public class ReportForJrSupervisorAllBlockController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region ExamVenueGet
        [HttpPost]
        public HttpResponseMessage ExamVenueGet()
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

                SqlCommand cmd = new SqlCommand("ExamVenueGet", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<JrSupervisor> ObjJRSP = new List<JrSupervisor>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        JrSupervisor Objsp = new JrSupervisor();
                        Objsp.Id = Convert.ToInt64(dr["Id"]);
                        Objsp.ExamCenterId = Convert.ToInt64(dr["ExamCenterId"]);
                        Objsp.DisplayName = (dr["DisplayName"].ToString());
                        Objsp.Code = (dr["Code"].ToString());
                        ObjJRSP.Add(Objsp);
                    }

                }
                return Return.returnHttp("200", ObjJRSP, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

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
                List<JrSupervisor> ObjJSP = new List<JrSupervisor>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        JrSupervisor Objspv = new JrSupervisor();
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

        #region JrSuperVisorReportList1
        [HttpPost]
        public HttpResponseMessage JrSuperVisorReportList1(JrSupervisor JRS)
        {
            try
            {
                Int64 ExamVenueId = JRS.ExamVenueId;
                Int64 ExamEventId = JRS.ExamEventId;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("JrSuperVisorReportList1", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamVenueId", ExamVenueId);
                cmd.Parameters.AddWithValue("@ExamEventId", ExamEventId);
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                Int32 count = 0;
                List<JrSupervisor> ObjSPList1 = new List<JrSupervisor>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        JrSupervisor ObjspList1 = new JrSupervisor();
                        ObjspList1.ProgrammeInstancePartTermId = Convert.ToInt64(dr["ProgrammeInstancePartTermId"]);
                        ObjspList1.InstancePartTermName = (dr["InstancePartTermName"].ToString());
                        ObjspList1.PaperId = Convert.ToInt64(dr["PaperId"]);
                        ObjspList1.PaperCode = (dr["PaperCode"].ToString());
                        ObjspList1.PaperName = (dr["PaperName"].ToString());
                        ObjspList1.ExamDate = (dr["ExamDate"].ToString());
                        ObjspList1.SlotName = (dr["SlotName"].ToString());
                        ObjspList1.StudentCount = Convert.ToInt64(dr["StudentCount"]);
                        ObjspList1.Block_Allocation_Status = (dr["Block_Allocation_Status"].ToString());

                        //var ExamBlockId = dr["ExamBlockId"];
                        //if (ExamBlockId is DBNull)
                        //{
                        //    ObjspList1.ExamBlockId = null;
                        //}
                        //else
                        //{
                        //    ObjspList1.ExamBlockId = Convert.ToInt32(dr["ExamBlockId"]);
                        //}
                        //ObjspList1.Block_Name = (dr["Block_Name"].ToString());
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

        #region JrSuperVisorReportList2
        [HttpPost]
        public HttpResponseMessage JrSuperVisorReportList2(JrSupervisorReport JRS)
        {
            try
            {
                Int64 ProgrammeInstancePartTermId = JRS.ProgrammeInstancePartTermId;
                Int64 ExamEventId = JRS.ExamEventId;
                Int64 PaperId = JRS.PaperId;
                Int32? BlockId = JRS.ExamBlockId;
                Int64 ExamVenueId = JRS.ExamVenueId;
                Int64 ExamVenueExamCenterId = JRS.ExamVenueExamCenterId;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("JrSuperVisorReportList2AllBlock", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamEventId", ExamEventId);
                cmd.Parameters.AddWithValue("@PaperId", PaperId);
                //cmd.Parameters.AddWithValue("@BlockId", BlockId);
                cmd.Parameters.AddWithValue("@ExamVenueId", ExamVenueId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                Int32 count = 0;

                BALCommon common = new BALCommon();

                string OldBaseUrlForPhoto = "https://admission.msubaroda.ac.in/MSUISApi/Upload/Photo/";
                //string NewBaseUrlForPhoto = "https://localhost:44374/Upload/Photo/";
                string NewBaseUrlForPhoto = "https://msuis.msubaroda.ac.in/Vidhyarthi_API/Upload/Photo/";

                //string sPath = "";
                //sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/Photo/");

                string OldBaseUrlForSign = "https://admission.msubaroda.ac.in/MSUISApi/Upload/Signature/";
                //string NewBaseUrlForSign = "https://localhost:44374/Upload/Signature/";
                string NewBaseUrlForSign = "https://msuis.msubaroda.ac.in/Vidhyarthi_API/Upload/Signature/";

                //string sPathSign = "";
                //sPathSign = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/Signature/");


                List<JrSupervisorReport> ObjSPList2 = new List<JrSupervisorReport>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        JrSupervisorReport ObjspList2 = new JrSupervisorReport();
                        ObjspList2.FullName = Convert.ToString(dr["LastName"]) + " " + Convert.ToString(dr["FirstName"]) + " " + Convert.ToString(dr["MiddleName"]);
                        ObjspList2.PRN = Convert.ToInt64(dr["PRN"]);
                        ObjspList2.SeatNumber = (dr["SeatNumber"].ToString());
                        ObjspList2.StudentSignature = (dr["StudentSignature"].ToString());
                        ObjspList2.StudentPhoto = (dr["StudentPhoto"].ToString());
                        ObjspList2.InstructionMediumName = (dr["InstructionMediumName"].ToString());

                        var ExamBlockId = dr["ExamBlockId"];
                        if (ExamBlockId is DBNull)
                        {
                            ObjspList2.ExamBlockId = null;
                        }
                        else
                        {
                            ObjspList2.ExamBlockId = Convert.ToInt32(dr["ExamBlockId"]);
                        }

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



                        //var ExamVenueId = dr["ExamVenueId"];
                        //if (ExamVenueId is DBNull)
                        //{
                        //    ObjspList2.ExamVenueId = null;
                        //}
                        //else
                        //{
                        //    ObjspList2.ExamVenueId = Convert.ToInt32(dr["ExamVenueId"]);
                        //}

                        count = count + 1;
                        ObjspList2.IndexId = count;
                        ObjSPList2.Add(ObjspList2);
                    }

                }

                List<JrSupervisorBlockReport> BlockList = new List<JrSupervisorBlockReport>();

                SqlCommand cmd1 = new SqlCommand("JrSuperVisorReportExamBlocksGetCenterWise", Con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@ExamEventId", ExamEventId);
                cmd1.Parameters.AddWithValue("@PaperId", PaperId);
                cmd1.Parameters.AddWithValue("@ExamVenueId", ExamVenueId);
                cmd1.Parameters.AddWithValue("@ExamVenueExamCenterId", ExamVenueExamCenterId);

                SqlDataAdapter Da1 = new SqlDataAdapter();
                DataTable Dt1 = new DataTable();

                Da1.SelectCommand = cmd1;
                Da1.Fill(Dt1);
                if (Dt1.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt1.Rows)
                    {
                        JrSupervisorBlockReport Block = new JrSupervisorBlockReport();

                        Block.ExamBlockName = Convert.ToString(dr["ExamBlockName"]);
                        Block.ExamBlockId = Convert.ToInt32(dr["ExamBlockId"]);
                        Block.VanueName = Convert.ToString(dr["VanueName"]);
                        Block.StudentCount = Convert.ToInt32(dr["StudentCount"]);
                        Block.FromSeatNumber = Convert.ToString(dr["FromSeatNumber"]);
                        Block.ToSeatNo = Convert.ToString(dr["ToSeatNo"]);

                        BlockList.Add(Block);
                    }
                }
                foreach (JrSupervisorBlockReport B in BlockList)
                {
                    List<JrSupervisorReport> SList = ObjSPList2.Where(e => e.ExamBlockId == B.ExamBlockId).ToList();
                    if (SList.Any())
                    {
                        B.StudentList = SList;
                    }
                }

                return Return.returnHttp("200", BlockList, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region JrSuperVisorReportExamCenterGet
        [HttpPost]
        public HttpResponseMessage JrSuperVisorReportExamCenterGet(ExamVenueExamCenterNew EVEC)
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
                Int32 ExamVenueId = Convert.ToInt32(EVEC.ExamVenueId);

                SqlCommand cmd = new SqlCommand("JrSuperVisorReportExamCenterGet", Con);
                cmd.Parameters.AddWithValue("@ExamVenueId", ExamVenueId);
                cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<ExamVenueExamCenterNew> ObjVC = new List<ExamVenueExamCenterNew>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        ExamVenueExamCenterNew ObjCenter = new ExamVenueExamCenterNew();
                        ObjCenter.ExamVenueExamCenterId = Convert.ToInt32(dr["ExamVenueExamCenterId"]);
                        ObjCenter.CenterName = (dr["CenterName"].ToString());
                        ObjVC.Add(ObjCenter);
                    }

                }
                return Return.returnHttp("200", ObjVC, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion


    }
}
