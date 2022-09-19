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
    public class ReportForJrSupervisorCenterWiseController : ApiController 
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
                List<JrSupervisorNew> ObjJRSP = new List<JrSupervisorNew>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        JrSupervisorNew Objsp = new JrSupervisorNew();
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
                List<JrSupervisorNew> ObjJSP = new List<JrSupervisorNew>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        JrSupervisorNew Objspv = new JrSupervisorNew();
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
        public HttpResponseMessage JrSuperVisorReportList1(JrSupervisorNew JRS)
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
                List<JrSupervisorNew> ObjSPList1 = new List<JrSupervisorNew>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        JrSupervisorNew ObjspList1 = new JrSupervisorNew();
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
        public HttpResponseMessage JrSuperVisorReportList2(JrSupervisorReportNew JRS)
        {
            try
            {
                Int64 ProgrammeInstancePartTermId = JRS.ProgrammeInstancePartTermId;
                Int64 ExamEventId = JRS.ExamEventId;
                Int64 PaperId = JRS.PaperId;
                Int32? BlockId = JRS.ExamBlockId;
                Int64 ExamVenueId = JRS.ExamVenueId;


                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("JrSuperVisorReportList2", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                cmd.Parameters.AddWithValue("@ExamEventId", ExamEventId);
                cmd.Parameters.AddWithValue("@PaperId", PaperId);
                cmd.Parameters.AddWithValue("@BlockId", BlockId);
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


                List<JrSupervisorReportNew> ObjSPList2 = new List<JrSupervisorReportNew>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        JrSupervisorReportNew ObjspList2 = new JrSupervisorReportNew();
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
                return Return.returnHttp("200", ObjSPList2, null);

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

        #region JrSuperVisorReportExamBlocksGet
        [HttpPost]
        public HttpResponseMessage JrSuperVisorReportExamBlocksGet(ExamBlocksGetNew EB)
        {
            try
            {
                Int64 ExamEventId = EB.ExamEventId;
                Int64 PaperId = EB.PaperId;
                Int64 ExamVenueId = EB.ExamVenueId;
                Int64 ExamVenueExamCenterId = EB.ExamVenueExamCenterId;
                //Int64 ProgrammeInstancePartTermId = EB.ProgrammeInstancePartTermId;
                //Int64 ExamBlockId = EB.ExamBlockId;


                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("JrSuperVisorReportExamBlocksGetCenterWise", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamEventId", ExamEventId);
                cmd.Parameters.AddWithValue("@PaperId", PaperId);
                cmd.Parameters.AddWithValue("@ExamVenueId", ExamVenueId);  
                cmd.Parameters.AddWithValue("@ExamVenueExamCenterId", ExamVenueExamCenterId);
                //cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                //cmd.Parameters.AddWithValue("@ExamBlockId", ExamBlockId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<JrSupervisorBlockReportNew> ExamBlock = new List<JrSupervisorBlockReportNew>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        JrSupervisorBlockReportNew Block = new JrSupervisorBlockReportNew();

                        Block.ExamBlockName = Convert.ToString(dr["ExamBlockName"]);
                        Block.ExamBlockId = Convert.ToInt32(dr["ExamBlockId"]);
                        Block.VanueName = Convert.ToString(dr["VanueName"]);
                        Block.StudentCount = Convert.ToInt32(dr["StudentCount"]);
                        Block.FromSeatNumber = Convert.ToString(dr["FromSeatNumber"]);
                        Block.ToSeatNo = Convert.ToString(dr["ToSeatNo"]);
                        ExamBlock.Add(Block);
                    }

                }
                return Return.returnHttp("200", ExamBlock, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion  
   
    }
}
