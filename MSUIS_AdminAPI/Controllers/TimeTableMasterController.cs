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
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class TimeTableMasterController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region newCode By Stored Procedure - Megha
        [HttpPost]
        public HttpResponseMessage TimeTableMasterAdd(TimeTableMaster dataString)
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
               Int64 UserId = Convert.ToInt64(res.ToString());
               // Int64 UserId = 1;

                //string DisplayName = Convert.ToString(ObjExamMaster.DisplayName);

                int ProgramInstancePartTermId = dataString.ProgramInstancePartTermId;
                int ExamMasterId = dataString.ExamMasterId;
                int PaperId = dataString.PaperId;
                int AssetmentMethodId = dataString.AssetmentMethodId;
                int ExamSlotId = dataString.ExamSlotId;
                string AssetmentType = dataString.AssetmentType;
                string ExamDate = dataString.ExamDate;
                int? Sequence = dataString.Sequence;


                SqlCommand cmd = new SqlCommand("TimeTableMasterAdd", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgramInstancePartTermId", ProgramInstancePartTermId);
                cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                cmd.Parameters.AddWithValue("@PaperId", PaperId);
                cmd.Parameters.AddWithValue("@AssetmentMethodId", AssetmentMethodId);
                cmd.Parameters.AddWithValue("@AssetmentType", AssetmentType);
                cmd.Parameters.AddWithValue("@ExamSlotId", ExamSlotId);
                cmd.Parameters.AddWithValue("@Sequence", Sequence);
                cmd.Parameters.AddWithValue("@ExamDate", ExamDate);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);

                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been added successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }

        [HttpPost]
        public HttpResponseMessage TimeTableMasterEdit(TimeTableMaster dataString)
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
                 Int64 UserId = Convert.ToInt64(res.ToString());
                //Int64 UserId = 1;
                Int32 Id = (int)dataString.Id;
                //string DisplayName = Convert.ToString(ObjExamMaster.DisplayName);

                int ProgramInstancePartTermId = dataString.ProgramInstancePartTermId;
                int ExamMasterId = dataString.ExamMasterId;
                int PaperId = dataString.PaperId;
                int AssetmentMethodId = dataString.AssetmentMethodId;
                int ExamSlotId = dataString.ExamSlotId;
                string AssetmentType = dataString.AssetmentType;
                string ExamDate = dataString.ExamDate;


                SqlCommand cmd = new SqlCommand("TimeTableMasterEdit", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                cmd.Parameters.AddWithValue("@PaperId", PaperId);
                cmd.Parameters.AddWithValue("@ExamSlotId", ExamSlotId);
                cmd.Parameters.AddWithValue("@Sequence", dataString.Sequence);
                cmd.Parameters.AddWithValue("@ExamDate", ExamDate);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);

                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been modified successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }

        [HttpPost]
        public HttpResponseMessage TimeTableMasterListGet(TimeTableMaster dataString)
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
                Int64 UserId = Convert.ToInt64(res.ToString());

                List<TimeTableMaster> TimeTableMasterList = new List<TimeTableMaster>();
                BALTimeTableMaster func = new BALTimeTableMaster();
                //flag=1,IsDeleted=0,IsActive=null
                TimeTableMasterList = func.getTimeTableMasterList("admin", 0, null,dataString.PaperId,dataString.ExamMasterId,dataString.AssetmentMethodId,dataString.ExamSlotId,dataString.ProgramInstancePartTermId);

                return Return.returnHttp("200", TimeTableMasterList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage TimeTableMasterListGetActive(TimeTableMaster dataString)
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
                Int64 UserId = Convert.ToInt64(res.ToString());

                List<TimeTableMaster> TimeTableMasterList = new List<TimeTableMaster>();
                BALTimeTableMaster func = new BALTimeTableMaster();
                //flag=1,IsDeleted=0,IsActive=1
                TimeTableMasterList = func.getTimeTableMasterList("admin", 0, 1, dataString.PaperId, dataString.ExamMasterId, dataString.AssetmentMethodId, dataString.ExamSlotId, dataString.ProgramInstancePartTermId);

                return Return.returnHttp("200", TimeTableMasterList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage TimeTableMasterGetPending(TimeTableMaster dataString)
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
                Int64 UserId = Convert.ToInt64(res.ToString());

                int? ProgrammePartTermId = dataString.ProgrammePartTermId, ExamMasterId = dataString.ExamMasterId;
                Int32 BranchId = dataString.BranchId;

                if (string.IsNullOrEmpty(ProgrammePartTermId.ToString()))
                {
                    return Return.returnHttp("201", "Select Programme Part Term. It's Mandatory.", null);
                }
                if (string.IsNullOrEmpty(ExamMasterId.ToString()))
                {
                    return Return.returnHttp("201", "Select Exam Event. It's Mandatory.", null);
                }
                if (string.IsNullOrEmpty(BranchId.ToString()))
                {
                    return Return.returnHttp("201", "Select Branch. It's Mandatory.", null);
                }

                List<TimeTableMaster> TimeTableMasterList = new List<TimeTableMaster>();
                BALTimeTableMaster func = new BALTimeTableMaster();
                //flag=1,IsDeleted=0,IsActive=null
                TimeTableMasterList = func.getPendingTimeTableList(ProgrammePartTermId, ExamMasterId, BranchId);

                return Return.returnHttp("200", TimeTableMasterList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        /// TimeTableMasterIsActive and TimeTableMasterIsInactive Created by Jaydev on 24-11-21

        [HttpPost]
        public HttpResponseMessage TimeTableMasterIsActive(TimeTableConfig dataString)
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

                DataTable TTM = new DataTable();
                //PaperList.Columns.Add(new DataColumn("Id", typeof(Int64)));
                TTM.Columns.Add(new DataColumn("Id", typeof(Int64)));
                TTM.Columns.Add(new DataColumn("ExamMasterId", typeof(Int32)));
                TTM.Columns.Add(new DataColumn("PaperId", typeof(Int64)));
                TTM.Columns.Add(new DataColumn("AssetmentMethodId", typeof(Int32)));                
                TTM.Columns.Add(new DataColumn("AssetmentType", typeof(String)));
                TTM.Columns.Add(new DataColumn("ExamDate", typeof(DateTime)));
                TTM.Columns.Add(new DataColumn("ExamSlotId", typeof(Int64)));
                TTM.Columns.Add(new DataColumn("Sequence", typeof(Int32)));
                TTM.Columns.Add(new DataColumn("IsConfirm", typeof(Boolean)));
                TTM.Columns.Add(new DataColumn("ConfirmBy", typeof(Int64)));
                TTM.Columns.Add(new DataColumn("ConfirmOn", typeof(DateTime)));

                Int64 UserId = Convert.ToInt64(res.ToString());
                foreach(TimeTableMaster TT in dataString.TTM)
                {
                    if(TT.ExamDate==""|| TT.ExamDate==null)
                    {

                    }
                    else
                    {
                        TTM.Rows.Add(TT.Id, TT.ExamMasterId, TT.PaperId, TT.AssetmentMethodId, TT.AssetmentType, Convert.ToDateTime(TT.ExamDate), TT.ExamSlotId, TT.Sequence, "TRUE", UserId, datetime);
                    }
                    
                }


                SqlCommand cmd = new SqlCommand("TimeTableMasterActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "TimeTableMasterIsActive");
                cmd.Parameters.AddWithValue("@TTM", TTM);
                cmd.Parameters.AddWithValue("@ProgramPartTermId", dataString.ProgrammePartTermId);
                cmd.Parameters.AddWithValue("@BranchId", dataString.BranchId);
                cmd.Parameters.AddWithValue("@ExamMasterId", dataString.ExamMasterId);
                cmd.Parameters.AddWithValue("@FacultyExamMapId", dataString.FacultyExamMapId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);




                cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@MESSAGE"].Value);
                con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been Active Successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }

        [HttpPost]
        public HttpResponseMessage TimeTableMasterIsInactive(TimeTableConfig dataString)
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

                DataTable TTM = new DataTable();
                //PaperList.Columns.Add(new DataColumn("Id", typeof(Int64)));
                TTM.Columns.Add(new DataColumn("Id", typeof(Int64)));
                TTM.Columns.Add(new DataColumn("ExamMasterId", typeof(Int32)));
                TTM.Columns.Add(new DataColumn("PaperId", typeof(Int64)));
                TTM.Columns.Add(new DataColumn("AssetmentMethodId", typeof(Int32)));
                TTM.Columns.Add(new DataColumn("AssetmentType", typeof(String)));
                TTM.Columns.Add(new DataColumn("ExamDate", typeof(DateTime)));
                TTM.Columns.Add(new DataColumn("ExamSlotId", typeof(Int64)));
                TTM.Columns.Add(new DataColumn("Sequence", typeof(Int32)));
                TTM.Columns.Add(new DataColumn("IsConfirm", typeof(Boolean)));
                TTM.Columns.Add(new DataColumn("ConfirmBy", typeof(Int64)));
                TTM.Columns.Add(new DataColumn("ConfirmOn", typeof(DateTime)));

                Int64 UserId = Convert.ToInt64(res.ToString());
                foreach (TimeTableMaster TT in dataString.TTM)
                {
                    if (TT.ExamDate == "" || TT.ExamDate == null)
                    {

                    }
                    else
                    {
                        TTM.Rows.Add(TT.Id, TT.ExamMasterId, TT.PaperId, TT.AssetmentMethodId, TT.AssetmentType, Convert.ToDateTime(TT.ExamDate), TT.ExamSlotId, TT.Sequence, "TRUE", UserId, datetime);
                    }
                }


                SqlCommand cmd = new SqlCommand("TimeTableMasterActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "TimeTableMasterIsInactive");
                cmd.Parameters.AddWithValue("@TTM", TTM);
                cmd.Parameters.AddWithValue("@ProgramPartTermId", dataString.ProgrammePartTermId);
                cmd.Parameters.AddWithValue("@BranchId", dataString.BranchId);
                cmd.Parameters.AddWithValue("@ExamMasterId", dataString.ExamMasterId);
                cmd.Parameters.AddWithValue("@FacultyExamMapId", dataString.FacultyExamMapId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);




                cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@MESSAGE"].Value);
                con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been Active Successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        [HttpPost]
        public HttpResponseMessage GetTTCDetail(TimeTableConfig dataString)
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
                Int64 UserId = Convert.ToInt64(res.ToString());

                Int32 ExamMasterId = dataString.ExamMasterId;
                Int32 FacultyExamMapId = Convert.ToInt32(dataString.FacultyExamMapId);

                if (string.IsNullOrEmpty(ExamMasterId.ToString()))
                {
                    return Return.returnHttp("201", "Select Exam Event. It's Mandatory.", null);
                }
                if (string.IsNullOrEmpty(FacultyExamMapId.ToString()))
                {
                    return Return.returnHttp("201", "Select Schedule. It's Mandatory.", null);
                }

                List<TimeTableConfig> TimeTableConfigList = new List<TimeTableConfig>();
                SqlCommand cmd = new SqlCommand("TimeTableConfigGetDetail", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                cmd.Parameters.AddWithValue("@FacultyExamMapId", FacultyExamMapId);

                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        TimeTableConfig TTC = new TimeTableConfig();
                        TTC.Id = (((Dt.Rows[i]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["Id"]);
                        TTC.PartTermName = (Convert.ToString(Dt.Rows[i]["PartTermShortName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["PartTermShortName"]);
                        TTC.Branch = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["BranchName"]);
                        TTC.ExamMaster = (Convert.ToString(Dt.Rows[i]["EventName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["EventName"]);
                        TTC.FacultyExamMap = (Convert.ToString(Dt.Rows[i]["ScheduleName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ScheduleName"]);
                        TTC.IsConfirm = (Convert.ToString(Dt.Rows[i]["IsConfirm"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsConfirm"]);
                        TTC.IsPublish = (Convert.ToString(Dt.Rows[i]["IsPublish"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsPublish"]);
                        TTC.IsDisplay = (Convert.ToString(Dt.Rows[i]["IsDisplay"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDisplay"]);
                        TTC.BranchId = (((Dt.Rows[i]["BranchId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["BranchId"]);
                        TTC.ExamMasterId = (((Dt.Rows[i]["ExamMasterId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["ExamMasterId"]);
                        TTC.FacultyExamMapId = (((Dt.Rows[i]["FacultyExamMapId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyExamMapId"]);
                        TTC.ProgrammePartTermId = (((Dt.Rows[i]["ProgrammePartTermId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammePartTermId"]);

                        TimeTableConfigList.Add(TTC);
                    }
                }

                return Return.returnHttp("200", TimeTableConfigList, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        [HttpPost]
        public HttpResponseMessage GetTT(TimeTableConfig dataString)
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
                Int64 UserId = Convert.ToInt64(res.ToString());

                Int32 ExamMasterId = Convert.ToInt32(dataString.ExamMasterId);
                Int32 FacultyExamMapId = Convert.ToInt32(dataString.FacultyExamMapId);
                Int32 BranchId = Convert.ToInt32(dataString.BranchId);
                Int32 ProgrammePartTermId = Convert.ToInt32(dataString.ProgrammePartTermId);

                List<TimeTableMaster> TimeTableList = new List<TimeTableMaster>();
                SqlCommand cmd = new SqlCommand("TimeTableMasterGetByPTB", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                cmd.Parameters.AddWithValue("@FacultyExamMapId", FacultyExamMapId);
                cmd.Parameters.AddWithValue("@BranchId", BranchId);
                cmd.Parameters.AddWithValue("@ProgrammePartTermId", ProgrammePartTermId);

                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        TimeTableMaster element = new TimeTableMaster();
                        //if (!string.IsNullOrEmpty(Dt.Rows[i][0].ToString()))
                        // element.Id = Convert.ToInt32(Dt.Rows[i][0].ToString());
                        //element.ProgramInstancePartTermId = Convert.ToInt32(Dt.Rows[i][1].ToString());
                        element.PaperName = Dt.Rows[i]["PaperName"].ToString();
                        element.TeachingLearningMethod = Dt.Rows[i]["TeachingLearningMethodName"].ToString();
                        element.AssetsmentMethodName = Dt.Rows[i]["AssessmentMethodName"].ToString();
                        element.AssetmentType = Dt.Rows[i]["AssessmentType"].ToString();
                        element.PartTermName = (Convert.ToString(Dt.Rows[i]["PartTermShortName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["PartTermShortName"]);
                        element.Branch = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["BranchName"]);
                        element.PaperId = (Convert.ToInt32(Dt.Rows[i]["PaperId"]));
                        element.buttonVisibility = Convert.ToBoolean(Dt.Rows[i]["buttonVisibility"]);
                        string date = Dt.Rows[i]["ExamDate"].ToString();
                        string[] datelist = date.Split(' ');



                        element.ExamDate = datelist[0];
                        element.ExamSlotName = Dt.Rows[i]["SlotName"].ToString();
                        if (!string.IsNullOrEmpty(Dt.Rows[i]["Sequence"].ToString()))
                        {
                            element.Sequence = Convert.ToInt32(Dt.Rows[i]["Sequence"].ToString());
                        }




                        TimeTableList.Add(element);

                    }
                }

                return Return.returnHttp("200", TimeTableList, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        [HttpPost]
        public HttpResponseMessage TTCPublish(TimeTableConfig dataString)
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

                
                Int64 UserId = Convert.ToInt64(res.ToString());
                

                SqlCommand cmd = new SqlCommand("TimeTableConfigPublish", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "TTCPublish");
                cmd.Parameters.AddWithValue("@Id", dataString.Id);
                
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);




                cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@MESSAGE"].Value);
                con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been Publish Successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }

        [HttpPost]
        public HttpResponseMessage TTCUnPublish(TimeTableConfig dataString)
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

                Int64 UserId = Convert.ToInt64(res.ToString());
                

                SqlCommand cmd = new SqlCommand("TimeTableConfigPublish", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "TTCUnPublish");
                cmd.Parameters.AddWithValue("@Id", dataString.Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);




                cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@MESSAGE"].Value);
                con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been Unpublish Successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }

        [HttpPost]
        public HttpResponseMessage TTCDisplayOn(TimeTableConfig dataString)
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


                Int64 UserId = Convert.ToInt64(res.ToString());


                SqlCommand cmd = new SqlCommand("TimeTableConfigDisplay", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "TTCDisplayOn");
                cmd.Parameters.AddWithValue("@Id", dataString.Id);

                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);




                cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@MESSAGE"].Value);
                con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been Set to Display On Successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }

        [HttpPost]
        public HttpResponseMessage TTCDisplayOff(TimeTableConfig dataString)
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

                Int64 UserId = Convert.ToInt64(res.ToString());


                SqlCommand cmd = new SqlCommand("TimeTableConfigDisplay", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "TTCDisplayOff");
                cmd.Parameters.AddWithValue("@Id", dataString.Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);




                cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@MESSAGE"].Value);
                con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been Set to Display Off Successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }




        
        [HttpPost]
        public HttpResponseMessage GetTimeTableConfig(TimeTableMaster dataString)
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
                Int64 UserId = Convert.ToInt64(res.ToString());

                int? ProgrammePartTermId = dataString.ProgrammePartTermId, ExamMasterId = dataString.ExamMasterId;
                Int32 BranchId = dataString.BranchId;
                Int32 FacultyExamMapId = Convert.ToInt32(dataString.FacultyExamMapId);

                if (string.IsNullOrEmpty(ProgrammePartTermId.ToString()))
                {
                    return Return.returnHttp("201", "Select Programme Part Term. It's Mandatory.", null);
                }
                if (string.IsNullOrEmpty(ExamMasterId.ToString()))
                {
                    return Return.returnHttp("201", "Select Exam Event. It's Mandatory.", null);
                }
                if (string.IsNullOrEmpty(BranchId.ToString()))
                {
                    return Return.returnHttp("201", "Select Branch. It's Mandatory.", null);
                }
                if (string.IsNullOrEmpty(FacultyExamMapId.ToString()))
                {
                    return Return.returnHttp("201", "Select Schedule. It's Mandatory.", null);
                }

                TimeTableConfig TimeTableConfig = new TimeTableConfig();
                SqlCommand cmd = new SqlCommand("TimeTableConfigGet", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgramPartTermId", ProgrammePartTermId);
                cmd.Parameters.AddWithValue("@BranchId", BranchId);
                cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                cmd.Parameters.AddWithValue("@FacultyExamMapId", FacultyExamMapId);

                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        TimeTableConfig TTC = new TimeTableConfig();
                        TTC.Id = (((Dt.Rows[i]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["Id"]);
                        TTC.PartTermName = (Convert.ToString(Dt.Rows[i]["PartTermShortName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["PartTermShortName"]);
                        TTC.Branch = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["BranchName"]);
                        TTC.ExamMaster = (Convert.ToString(Dt.Rows[i]["EventName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["EventName"]);
                        TTC.FacultyExamMap = (Convert.ToString(Dt.Rows[i]["ScheduleName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ScheduleName"]);
                        TTC.IsConfirm = (Convert.ToString(Dt.Rows[i]["IsConfirm"])).IsEmpty() ?false : Convert.ToBoolean(Dt.Rows[i]["IsConfirm"]);
                        TTC.IsPublish = (Convert.ToString(Dt.Rows[i]["IsPublish"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsPublish"]);

                        TimeTableConfig =TTC;
                    }
                }
               
                return Return.returnHttp("200", TimeTableConfig, null);
                
                    


                
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        [HttpPost]
        public HttpResponseMessage TimeTableMasterAddInBulk(TimeTableMaster dataString)
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
                //Int64 UserId = 1;//
                Int64 UserId = Convert.ToInt64(res.ToString());

                //string DisplayName = Convert.ToString(ObjExamMaster.DisplayName);

                //int ProgramInstancePartTermId = dataString.ProgramInstancePartTermId;
                int ExamMasterId = dataString.ExamMasterId;
                int PaperId = dataString.PaperId;
                int AssetmentMethodId = dataString.AssetmentMethodId;
                int ExamSlotId = dataString.ExamSlotId;
                string AssetmentType = dataString.AssetmentType;
                string ExamDate = dataString.ExamDate;
                int? Sequence = dataString.Sequence;


                SqlCommand cmd = new SqlCommand("TimeTableMasterAddInBulk", con);
                cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.AddWithValue("@ProgramInstancePartTermId", ProgramInstancePartTermId);
                cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                cmd.Parameters.AddWithValue("@PaperId", PaperId);
                cmd.Parameters.AddWithValue("@AssetmentMethodId", AssetmentMethodId);
                cmd.Parameters.AddWithValue("@AssetmentType", AssetmentType);
                cmd.Parameters.AddWithValue("@ExamSlotId", ExamSlotId);
                cmd.Parameters.AddWithValue("@Sequence", Sequence);
                cmd.Parameters.AddWithValue("@ExamDate", ExamDate);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);

                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been added successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion
    }
}