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
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class StudentBlockChangeController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region ExamVenueGetByPIPTIdForStudentBlockChange
        [HttpPost]
        public HttpResponseMessage ExamVenueGetByPIPTId(StudentBlockChange SBC)
        {
            try
            {
               
                Int64 ProgrammePartTermId = SBC.ProgrammePartTermId;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("ExamVenueGetForStudentBlockChange", con);
                cmd.CommandType = CommandType.StoredProcedure;
               
                cmd.Parameters.AddWithValue("@ProgrammePartTermId", ProgrammePartTermId);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<StudentBlockChange> objListSBC = new List<StudentBlockChange>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        StudentBlockChange objSBC = new StudentBlockChange();
                        objSBC.ExamVenueId = Convert.ToInt64(dr["Id"]);
                        objSBC.ExamVenueName = (dr["DisplayName"].ToString());
                        objListSBC.Add(objSBC);
                    }

                }
                return Return.returnHttp("200", objListSBC, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion
       
        #region ExamBlocksGetByVenueIdPIPTIdForStudentBlockChange
        [HttpPost]
        public HttpResponseMessage ExamBlockGetByVenueIdPIPTId(StudentBlockChange SBC)
        {
            try
            {
                Int64 ExamVenueId = SBC.ExamVenueId;
                Int64 ProgrammePartTermId = SBC.ProgrammePartTermId;
                Int64 BranchId = SBC.BranchId;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("ExamBlocksGetForStudentBlockChange", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamVenueId", ExamVenueId);
                cmd.Parameters.AddWithValue("@ProgrammePartTermId", ProgrammePartTermId);
                cmd.Parameters.AddWithValue("@BranchId", BranchId);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<StudentBlockChange> objListSBC = new List<StudentBlockChange>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        StudentBlockChange objSBC = new StudentBlockChange();
                        objSBC.ExamBlockId=Convert.ToInt64(dr["ExamBlockId"]);
                        objSBC.ExamBlockName = (dr["ExamBlockName"].ToString());
                        objListSBC.Add(objSBC);
                    }

                }
                return Return.returnHttp("200", objListSBC, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region NotSelectedExamBlocksGetByVenueIdPIPTIdForStudentBlockChange
        [HttpPost]
        public HttpResponseMessage NotSelectedExamBlockGetByVenueIdPIPTId(StudentBlockChange SBC)
        {
            try
            {
                Int64 ExamVenueId = SBC.ExamVenueId;
                Int64 ProgrammePartTermId = SBC.ProgrammePartTermId;
                Int64 ExamBlockId = SBC.ExamBlockId;
                Int64 BranchId = SBC.BranchId;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("NotSelectedExamBlocksGetForStudentBlockChange", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamVenueId", ExamVenueId);
                cmd.Parameters.AddWithValue("@ProgrammePartTermId", ProgrammePartTermId);
                cmd.Parameters.AddWithValue("@ExamBlockId", ExamBlockId);
               
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<StudentBlockChange> objListSBC = new List<StudentBlockChange>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        StudentBlockChange objSBC = new StudentBlockChange();
                        objSBC.ExamBlkId = Convert.ToInt64(dr["ExamBlockId"]);
                        objSBC.AvailableCapacity = Convert.ToInt32(dr["AvailableCapacity"]);
                        objSBC.ExamBlkName = (dr["Name"].ToString());
                        objListSBC.Add(objSBC);
                    }

                }
                return Return.returnHttp("200", objListSBC, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region ExamVenueExamCenterGetforStudentBlocksChange
        [HttpPost]
        public HttpResponseMessage ExamVenueExamCenterGetForStudentBlockChange(StudentBlockChange SBC)
        {
            try
            {
                Int64 ExamVenueId = SBC.ExamVenueId;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("ExamVenueGetforAssignBlocks", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamVenueId", ExamVenueId);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<StudentBlockChange> ObjVC = new List<StudentBlockChange>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        StudentBlockChange ObjCenter = new StudentBlockChange();
                        ObjCenter.ExamVenExamCentId = Convert.ToInt32(dr["ExamVenueExamCenterId"]);
                        ObjCenter.ExamCenterName = (dr["CenterName"].ToString());
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

        #region ExamCenterGetforStudentBlocks
        [HttpPost]
        public HttpResponseMessage ExamCenterGetForStudentBlockChange(StudentBlockChange SBC)
        {
            try
            {
                Int64 ExamVenueId = SBC.ExamVenueId;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("ExamVenueGetforAssignBlocks", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamVenueId", ExamVenueId);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<StudentBlockChange> ObjVC = new List<StudentBlockChange>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        StudentBlockChange ObjCenter = new StudentBlockChange();
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


        #region BranchGetforStudentBlockChange
        [HttpPost]
        public HttpResponseMessage MstSpecialisationGetByPId(AssignBlocks dataString)
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstSpecialisationGetByPId", con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@ProgrammeId", dataString.ProgrammeId);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                dataString.SpecialisationsList = new List<MstSpecialisation>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstSpecialisation objS = new MstSpecialisation();

                        objS.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objS.BranchName = Convert.ToString(Dt.Rows[i]["BranchName"]);
                        objS.ProgrammeId = Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);
                        objS.ProgrammeName = Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        dataString.SpecialisationsList.Add(objS);
                    }
                }
                return Return.returnHttp("200", dataString, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion
        
        #region PaperExamDateGetByExamBlockIdForStudentBlockChange
        [HttpPost]
        public HttpResponseMessage ExamDateGetByExamBlockIdForStudentBlockChange(StudentBlockChange SBC)
        {
            try
            {
                Int64 ExamBlockId = SBC.ExamBlockId;
                Int64 ExamMasterId = SBC.ExamMasterId;
                Int64 ProgrammePartTermId = SBC.ProgrammePartTermId;
                Int64 BranchId = SBC.BranchId;
                Int64 ExamVenueId = SBC.ExamVenueId;
                Int32 Count = 0;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("ExamDateGetByExamBlockIdForStudentBlockChange", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamBlockId", ExamBlockId);
                cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                cmd.Parameters.AddWithValue("@ProgrammePartTermId", ProgrammePartTermId);
                cmd.Parameters.AddWithValue("@BranchId", BranchId);
                cmd.Parameters.AddWithValue("@ExamVenueId", ExamVenueId);              
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<StudentBlockChange> objListSBC = new List<StudentBlockChange>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        StudentBlockChange objSBC = new StudentBlockChange();
                        objSBC.ExamMasterId = Convert.ToInt64(dr["ExamMasterId"]);
                        objSBC.MstPaperId = Convert.ToInt64(dr["MstPaperId"]);
                        objSBC.PaperCode = (dr["PaperCode"].ToString());
                        objSBC.PaperName = (dr["PaperName"].ToString());
                        objSBC.ExamDateView = (Convert.ToString(dr["ExamDate"])).IsEmpty() ? "-" : string.Format("{0: dd-MM-yyyy}", (dr["ExamDate"]));
                        objSBC.SlotName = (dr["SlotName"].ToString());
                        Count = Count + 1;
                        objSBC.IndexId = Count;
                        objListSBC.Add(objSBC);
                    }

                }
                return Return.returnHttp("200", objListSBC, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region GetStudentDetailsForPaperWiseBlockChange
        [HttpPost]
        public HttpResponseMessage GetStudentForPaperWiseBlockChange(StudentBlockChange SBC)
        {
            try
            {
               
                Int64 ExamMasterId = SBC.ExamMasterId;
                Int64 MstpaperId = SBC.MstPaperId;
                Int64 ExamBlockId = SBC.ExamBlockId;
                Int32 Count = 0;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("GetStudentForPaperWiseBlockChange", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MstpaperId", MstpaperId);
                cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                cmd.Parameters.AddWithValue("@ExamBlockId", ExamBlockId);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<StudentBlockChange> objListSBC = new List<StudentBlockChange>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        StudentBlockChange objSBC = new StudentBlockChange();

                        objSBC.IncStudentExamPaperMapId = Convert.ToInt64(dr["Id"]);
                        objSBC.ProgInstancePartTermId = Convert.ToInt64(dr["ProgInstancePartTermId"]);
                        objSBC.PaperCode = (dr["PaperCode"].ToString());
                        objSBC.PaperName = (dr["PaperName"].ToString());
                        objSBC.SeatNumber = (dr["SeatNumber"].ToString());
                        objSBC.InstancePartTermName = (dr["InstancePartTermName"].ToString());
                        objSBC.ExamDateView = (Convert.ToString(dr["ExamDate"])).IsEmpty() ? "-" : string.Format("{0: dd-MM-yyyy}", (dr["ExamDate"]));
                        objSBC.BranchName = (dr["BranchName"].ToString());
                        objSBC.SlotName = (dr["SlotName"].ToString());
                        objSBC.ProgrammeName = (dr["ProgrammeName"].ToString());
                        objSBC.VenueName = (dr["VenueName"].ToString());
                        objSBC.CenterName = (dr["CenterName"].ToString());
                        objSBC.ExamBlockName = (dr["ExamBlockName"].ToString());
                        objSBC.ExamEventName = (dr["ExamEventName"].ToString());
                        objSBC.PRN = Convert.ToInt64(dr["PRN"]);
                       
                        Count = Count + 1;
                        objSBC.IndexId = Count;
                        objListSBC.Add(objSBC);
                    }

                }
                return Return.returnHttp("200", objListSBC, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region UpdateStudentBlockChange
        [HttpPost]
        public HttpResponseMessage UpdateStudentBlockChange(StudentBlockChange SBC)

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
                DataTable StudentList = new DataTable();

                StudentList.Columns.Add(new DataColumn("ProgrammeInstancePartTermId", typeof(Int64)));
                StudentList.Columns.Add(new DataColumn("PRN", typeof(Int64)));
                StudentList.Columns.Add(new DataColumn("ExamMasterId", typeof(Int32)));
                foreach(StudentExamPaper s in SBC.StudentList)
                {
                    StudentList.Rows.Add(s.ProgInstancePartTermId, s.PRN, SBC.ExamMasterId);
                }
                

                SqlCommand cmd = new SqlCommand("PaperWiseStudentBlockChangeUpdate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Parameters.AddWithValue("@StudentList", StudentList);
                 cmd.Parameters.AddWithValue("@ExamBlockIdNew", SBC.ExamBlockIdNew);
                 cmd.Parameters.AddWithValue("@ExamBlockIdOld", SBC.ExamBlockIdOld);                
                 cmd.Parameters.AddWithValue("@ProgrammePartTermId", SBC.ProgrammePartTermId);
                 cmd.Parameters.AddWithValue("@ExamMasterId", SBC.ExamMasterId);
                 cmd.Parameters.AddWithValue("@ExamVenueId", SBC.ExamVenueId);
                 cmd.Parameters.AddWithValue("@BranchId", SBC.BranchId);
                 cmd.Parameters.AddWithValue("@MstPaperId", SBC.MstPaperId);
                 cmd.Parameters.AddWithValue("@ExamVenueExamCenterId", SBC.ExamVenueExamCenterId);           
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
                    strMessage = "Your Data has been Updated successfully.";
                }

                return Return.returnHttp("200", strMessage, null);


            }
            catch (Exception e)
            {

                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

    }
}
