
using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
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
    public class CenterWiseBlockReportController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        #region ExamEventGet
        [HttpPost]
        public HttpResponseMessage ExamEventGet()
        {
            try
            {

                SqlCommand Cmd = new SqlCommand("ExamEventGetForDropDown", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = Cmd;
                sda.Fill(Dt);

                List<ExamEventMaster> ObjLstEEM = new List<ExamEventMaster>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ExamEventMaster objEEM = new ExamEventMaster();

                        objEEM.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objEEM.DisplayName = Convert.ToString(Dt.Rows[i]["DisplayName"]);
                        objEEM.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objEEM.IsDeleted = Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);

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

        #region VenueGetByExamEventId
        [HttpPost]
        public HttpResponseMessage VenueGetByExamMasterId(CenterWiseBlockReport objvenue)
        {
            try
            {
                Int64 ExamMasterId = objvenue.ExamMasterId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("CenterWiseBlockReportGetVenueByExamMasterId", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                sda.SelectCommand = cmd;
                sda.Fill(Dt);
                List<CenterWiseBlockReport> ObjVenueList = new List<CenterWiseBlockReport>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        CenterWiseBlockReport ObjVenue = new CenterWiseBlockReport();
                        ObjVenue.ExamVenueId = Convert.ToInt32(dr["ExamVenueId"]);
                        ObjVenue.DisplayName = (dr["DisplayName"].ToString());
                       
                        ObjVenueList.Add(ObjVenue);
                    }

                }
                return Return.returnHttp("200", ObjVenueList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region ExamVenueExamCenterGetByVenueId
        [HttpPost]
        public HttpResponseMessage ExamVenueExamCenterGetByVenueId(CenterWiseBlockReport objcenter)
        {
            try
            {
                Int64 ExamVenueId = objcenter.ExamVenueId;
                Int64 ExamMasterId = objcenter.ExamMasterId;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("CenterWiseBlockReportGetExamVenueExamCenterByVenueId", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamVenueId", ExamVenueId);
                cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                sda.SelectCommand = cmd;
                sda.Fill(Dt);
                List<CenterWiseBlockReport> ObjCenterList = new List<CenterWiseBlockReport>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        CenterWiseBlockReport ObjCenter = new CenterWiseBlockReport();
                        ObjCenter.ExamVenueExamCenterId = Convert.ToInt32(dr["ExamVenueExamCenterId"]);
                        ObjCenter.CenterName = (dr["CenterName"].ToString());
                       
                        ObjCenterList.Add(ObjCenter);
                    }

                }
                return Return.returnHttp("200", ObjCenterList, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region SlotMasterGetByExamVenueExamCenterId
        [HttpPost]
        public HttpResponseMessage SlotMasterGetByExamVenueExamCenterId(CenterWiseBlockReport objslot)
        {
            try
            {
                Int64 ExamVenueExamCenterId = objslot.ExamVenueExamCenterId;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("CenterWiseBlockReportGetSlotByExamVenueExamCenterId", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamVenueExamCenterId", ExamVenueExamCenterId);
                sda.SelectCommand = cmd;
                sda.Fill(Dt);
                List<CenterWiseBlockReport> ObjSlotList = new List<CenterWiseBlockReport>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        CenterWiseBlockReport ObjSlot = new CenterWiseBlockReport();
                        ObjSlot.ExamSlotId = Convert.ToInt32(dr["ExamSlotId"]);
                        ObjSlot.SlotName = (dr["SlotName"].ToString());
                      
                        ObjSlotList.Add(ObjSlot);
                    }

                }
                return Return.returnHttp("200", ObjSlotList, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region CenterWiseBlockReportGetById
        [HttpPost]
        public HttpResponseMessage CenterWiseBlockReportGetById(CenterWiseBlockReport objcenterBlock)
        {
            try
            {
                Int64 ExamMasterId = objcenterBlock.ExamMasterId;
                Int64 ExamVenueId = objcenterBlock.ExamVenueId;
                Int64 ExamVenueExamCenterId = objcenterBlock.ExamVenueExamCenterId;             
                Int64 ExamSlotId = objcenterBlock.ExamSlotId;
              

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int32 Count = 0;
                SqlCommand cmd = new SqlCommand("CenterWiseBlockReportGetById", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                cmd.Parameters.AddWithValue("@ExamVenueId", ExamVenueId);
                cmd.Parameters.AddWithValue("@ExamVenueExamCenterId", ExamVenueExamCenterId);
                cmd.Parameters.AddWithValue("@ExamSlotId", ExamSlotId);
                sda.SelectCommand = cmd;
                sda.Fill(Dt);
                List<CenterWiseBlockReport> ObjCenterBlockList = new List<CenterWiseBlockReport>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++) 
                    {
                        CenterWiseBlockReport ObjCenterBlock = new CenterWiseBlockReport();
                        ObjCenterBlock.StudentCount = (Convert.ToString(Dt.Rows[i]["StudentCount"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["StudentCount"]);
                        ObjCenterBlock.PaperName = (Convert.ToString(Dt.Rows[i]["PaperName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["PaperName"]);
                        ObjCenterBlock.PaperCode = (Convert.ToString(Dt.Rows[i]["PaperCode"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["PaperCode"]);
                        ObjCenterBlock.ExamVenueName = (Convert.ToString(Dt.Rows[i]["ExamVenue"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["ExamVenue"]);
                        ObjCenterBlock.ExameventName = (Convert.ToString(Dt.Rows[i]["Examevent"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["Examevent"]);
                        ObjCenterBlock.SlotName = (Convert.ToString(Dt.Rows[i]["SlotName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["SlotName"]);
                        ObjCenterBlock.BlockName = (Convert.ToString(Dt.Rows[i]["BlockName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["BlockName"]);                
                        ObjCenterBlock.CenterName = (Convert.ToString(Dt.Rows[i]["CenterName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["CenterName"]);
                        ObjCenterBlock.Date = (Convert.ToString(Dt.Rows[i]["Date"])).IsEmpty() ? "-" : string.Format("{0: dd-MM-yyyy}", (Dt.Rows[i]["Date"]));
                        Count = Count + 1;
                        ObjCenterBlock.IndexId = Count;

                        ObjCenterBlockList.Add(ObjCenterBlock);
                    }

                }
                return Return.returnHttp("200", ObjCenterBlockList, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

    }
}
