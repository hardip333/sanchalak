using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.WebPages;

namespace MSUISApi.BAL
{
    public class BALExamFormMaster
    {

        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        public List<ExamFormMaster> ExamFormMasterPartTermStatusGetByCourseScheduleMap(int? FacultyExamMapId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("ExamFormMasterPartTermStatusGetByCourseScheduleMap", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@FacultyExamMapId", FacultyExamMapId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<ExamFormMaster> ObjListExamFormMaster = new List<ExamFormMaster>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ExamFormMaster element = new ExamFormMaster();
                        element.FacultyExamMapId = FacultyExamMapId;
                        element.ProgrammePartTermName = Dt.Rows[i]["PartTermName"].ToString();
                        element.BranchId = Convert.ToInt32(Dt.Rows[i]["SpecialisationId"].ToString());
                        element.BranchName = Dt.Rows[i]["BranchName"].ToString();
                        element.FreshersAvailable = Convert.ToInt32(Dt.Rows[i]["FresherEligible"].ToString());
                        element.FreshersGenerated = Convert.ToInt32(Dt.Rows[i]["FreshersGenerated"].ToString());
                        element.RepeaterAvailable = Convert.ToInt32(Dt.Rows[i]["RepeaterAvailable"].ToString());
                        element.RepeaterGenerated = Convert.ToInt32(Dt.Rows[i]["RepeaterGenerated"].ToString());
                        element.NeverAppeareadAvailable = Convert.ToInt32(Dt.Rows[i]["NeverAppearedAvailable"].ToString());
                        element.NeverAppeareadGenerated = Convert.ToInt32(Dt.Rows[i]["NeverAppearedGenerated"].ToString());
                        element.CourseSchduleMapId = Convert.ToInt32(Dt.Rows[i]["Id"].ToString());
                        element.ProgrammePartTermId = Convert.ToInt32(Dt.Rows[i]["ProgrammePartTermId"].ToString());
                        ObjListExamFormMaster.Add(element);

                    }
                }
                return ObjListExamFormMaster;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<ExamFormMaster> ExamFormMasterPartTermStatusGet(int? ExamMasterId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("ExamFormMasterPartTermStatusGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<ExamFormMaster> ObjListExamFormMaster = new List<ExamFormMaster>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ExamFormMaster element = new ExamFormMaster();
                        element.FacultyExamMapId = Convert.ToInt32(Dt.Rows[i][0].ToString());
                        element.ScheduleCode = Dt.Rows[i][1].ToString();
                        element.FreshersAvailable = Convert.ToInt32(Dt.Rows[i][2].ToString());
                        element.FreshersGenerated = Convert.ToInt32(Dt.Rows[i][3].ToString());
                        element.RepeaterAvailable = Convert.ToInt32(Dt.Rows[i][4].ToString());
                        element.RepeaterGenerated = Convert.ToInt32(Dt.Rows[i][5].ToString());
                        element.NeverAppeareadAvailable = Convert.ToInt32(Dt.Rows[i][6].ToString());
                        element.NeverAppeareadGenerated = Convert.ToInt32(Dt.Rows[i][7].ToString());
                        ObjListExamFormMaster.Add(element);
                    }
                }
                return ObjListExamFormMaster;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<ExamFormMaster> ExamFormMasterGetInward(int? ExamMasterId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("ExamFormMasterGetInward", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<ExamFormMaster> ObjListExamFormMaster = new List<ExamFormMaster>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ExamFormMaster element = new ExamFormMaster();
                        element.FacultyExamMapId = Convert.ToInt32(Dt.Rows[i][0].ToString());
                        element.ScheduleCode = Dt.Rows[i][1].ToString();
                        element.FreshersAvailable = Convert.ToInt32(Dt.Rows[i][2].ToString());
                        element.FreshersGenerated = Convert.ToInt32(Dt.Rows[i][3].ToString());
                        element.FreshersInward = Convert.ToInt32(Dt.Rows[i][4].ToString());
                        element.RepeaterAvailable = Convert.ToInt32(Dt.Rows[i][5].ToString());
                        element.RepeaterGenerated = Convert.ToInt32(Dt.Rows[i][6].ToString());
                        element.RepeaterInward = Convert.ToInt32(Dt.Rows[i][7].ToString());
                        element.NeverAppeareadAvailable = Convert.ToInt32(Dt.Rows[i][8].ToString());
                        element.NeverAppeareadGenerated = Convert.ToInt32(Dt.Rows[i][9].ToString());
                        element.NeverAppeareadInward = Convert.ToInt32(Dt.Rows[i][10].ToString());
                        ObjListExamFormMaster.Add(element);
                    }
                }
                return ObjListExamFormMaster;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<ExamFormMaster> ExamFormMasterGetInwardByFacutyExamMapId(int? FacultyExamMapId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("ExamFormMasterGetInwardByFacultyExamMapId", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@FacultyExamMapId", FacultyExamMapId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<ExamFormMaster> ObjListExamFormMaster = new List<ExamFormMaster>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ExamFormMaster element = new ExamFormMaster();

                        element.FacultyExamMapId = FacultyExamMapId;
                        element.ProgInstancePartTerm = Dt.Rows[i][0].ToString();
                        element.FreshersAvailable = Convert.ToInt32(Dt.Rows[i][1].ToString());
                        element.FreshersGenerated = Convert.ToInt32(Dt.Rows[i][2].ToString());
                        element.FreshersInward = Convert.ToInt32(Dt.Rows[i][3].ToString());
                        element.RepeaterAvailable = Convert.ToInt32(Dt.Rows[i][4].ToString());
                        element.RepeaterGenerated = Convert.ToInt32(Dt.Rows[i][4].ToString());
                        element.RepeaterInward = Convert.ToInt32(Dt.Rows[i][5].ToString());
                        element.NeverAppeareadAvailable = Convert.ToInt32(Dt.Rows[i][6].ToString());
                        element.NeverAppeareadGenerated = Convert.ToInt32(Dt.Rows[i][7].ToString());
                        element.NeverAppeareadInward = Convert.ToInt32(Dt.Rows[i][8].ToString());
                        ObjListExamFormMaster.Add(element);
                    }
                }
                return ObjListExamFormMaster;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string UpdateSeatNo(DataTable dt, int ExamMasterId, int ProgInstancePartTermId)
        {
            try
            {
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlCommand cmd1 = new SqlCommand("UpdateSeatNoInBulk", con);
                cmd1.Parameters.AddWithValue("@udt", dt); // passing Datatable  
                cmd1.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                cmd1.Parameters.AddWithValue("@ProgInstPartTermId", ProgInstancePartTermId);
                cmd1.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd1.Parameters["@Message"].Direction = ParameterDirection.Output;
                cmd1.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd1.ExecuteNonQuery().ToString();
                string strMessage = Convert.ToString(cmd1.Parameters["@Message"].Value);
                con.Close();
                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been modified successfully.";
                }
                return strMessage;
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }

        public string GenerateSeatNo(DataTable dt, ExamFormSeatGeneration dataString, Int64 UserId)
        {
            try
            {
                TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlCommand cmd1 = new SqlCommand("UpdateSeatNumber", con);
                cmd1.Parameters.AddWithValue("@udt1", dt); // passing Datatable  
                cmd1.Parameters.AddWithValue("@ExamMasterId", dataString.ExamMasterId);
                cmd1.Parameters.AddWithValue("@ProgrammePartTermId", dataString.ProgrammePartTermId);
                cmd1.Parameters.AddWithValue("@SpecialisationId", dataString.SpecialisationId);
                cmd1.Parameters.AddWithValue("@IncludeFresher", dataString.IncludeFresher);
                cmd1.Parameters.AddWithValue("@IncludeRepeater", dataString.IncludeRepeater);
                cmd1.Parameters.AddWithValue("@IncludeNeverAppeared", dataString.IncludeNeverAppeared);
                cmd1.Parameters.AddWithValue("@UserId", UserId);
                cmd1.Parameters.AddWithValue("@UserTime", datetime);
                cmd1.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd1.Parameters["@Message"].Direction = ParameterDirection.Output;
                cmd1.Parameters.Add("@Count", SqlDbType.Int);
                cmd1.Parameters["@Count"].Direction = ParameterDirection.Output;
                cmd1.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd1.ExecuteNonQuery().ToString();
                string strMessage = Convert.ToString(cmd1.Parameters["@Message"].Value);
                int count = 0;
                if (!string.IsNullOrEmpty(cmd1.Parameters["@Count"].Value.ToString()))
                    count = Convert.ToInt32(cmd1.Parameters["@Count"].Value);
                con.Close();
                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Seat Number generated(" + count + ") process completed.";
                }
                return strMessage;
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }

        public ExamFormDetails GetExamFormDetails(int? ProgrammePartTermId, int? BranchId,int? ExamMasterId)
        {
            try
            {

                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("GetExamFormMasterDetailsByProgrammePartIdAndBranchId", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@ProgrammePartTermId", ProgrammePartTermId); // passing Datatable  
                Cmd.Parameters.AddWithValue("@BranchId", BranchId);
                Cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);

                Da.SelectCommand = Cmd;

                Da.Fill(Dt);
                ExamFormDetails objexamFormDetails = new ExamFormDetails();
                if (Dt.Rows.Count > 0)
                {
                    objexamFormDetails.FresherGeneratedCount = Convert.ToInt32(Dt.Rows[0][0].ToString());
                    objexamFormDetails.RepeaterGeneratedCount = Convert.ToInt32(Dt.Rows[0][1].ToString());
                    objexamFormDetails.NeverAppearedGeneratedCount = Convert.ToInt32(Dt.Rows[0][2].ToString());
                    objexamFormDetails.FresherInwardCount = Convert.ToInt32(Dt.Rows[0][3].ToString());
                    objexamFormDetails.RepeaterInwardCount = Convert.ToInt32(Dt.Rows[0][4].ToString());
                    objexamFormDetails.NeverAppearedInwardCount = Convert.ToInt32(Dt.Rows[0][5].ToString());
                    objexamFormDetails.FresherSeatNoGeneratedCount = Convert.ToInt32(Dt.Rows[0][6].ToString());
                    objexamFormDetails.RepeaterSeatNoGeneratedCount = Convert.ToInt32(Dt.Rows[0][7].ToString());
                    objexamFormDetails.NeverAppearedSeatNoGeneratedCount = Convert.ToInt32(Dt.Rows[0][8].ToString());
                }
                return objexamFormDetails;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<ExamInwardDetails> GetPendingExamInwardList(int? ExamMasterId, Int64 PRN, Int64 UserId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("getPendingExamFormInward", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                Cmd.Parameters.AddWithValue("@PRN", PRN);
                Cmd.Parameters.AddWithValue("@UserId", UserId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<ExamInwardDetails> ObjListExamInward = new List<ExamInwardDetails>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ExamInwardDetails element = new ExamInwardDetails();
                        
                        element.Id = (((Dt.Rows[i]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["Id"]);
                        element.PRN = (((Dt.Rows[i]["PRN"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["PRN"]);
                        element.NameAsPerMarkSheet = (Convert.ToString(Dt.Rows[i]["NameAsPerMarkSheet"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["NameAsPerMarkSheet"]);
                        element.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        element.InstancePartTermName = (Convert.ToString(Dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                        element.DisplayName = (Convert.ToString(Dt.Rows[i]["DisplayName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["DisplayName"]);
                        element.FormNo = (((Dt.Rows[i]["FormNo"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["FormNo"]);
                        element.ExamFeeAmount = (((Dt.Rows[i]["ExamFeeAmount"]).ToString()).IsEmpty()) ? 0 : Convert.ToDecimal(Dt.Rows[i]["ExamFeeAmount"]);
                        element.LateFeesFacultyPower = (((Dt.Rows[i]["LateFeesFacultyPower"]).ToString()).IsEmpty()) ? 0 : Convert.ToDecimal(Dt.Rows[i]["LateFeesFacultyPower"]);
                        element.LateFeesVCPower = (((Dt.Rows[i]["LateFeesVCPower"]).ToString()).IsEmpty()) ? 0 : Convert.ToDecimal(Dt.Rows[i]["LateFeesVCPower"]);
                        element.ExamFeesToBePaid = (((Dt.Rows[i]["ExamFeesToBePaid"]).ToString()).IsEmpty()) ? 0 : Convert.ToDecimal(Dt.Rows[i]["ExamFeesToBePaid"]);
                        element.ExamFeeStartDateView = (Convert.ToString(Dt.Rows[i]["ExamFeeStartDateView"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ExamFeeStartDateView"]);
                        element.ExamFeeEndDateForStudentView = (Convert.ToString(Dt.Rows[i]["ExamFeeEndDateForStudentView"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ExamFeeEndDateForStudentView"]);
                        element.ExamFeeEndDateForFacultyView = (Convert.ToString(Dt.Rows[i]["ExamFeeEndDateForFacultyView"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ExamFeeEndDateForFacultyView"]);
                        element.IsFeeOpenForStudent = (Convert.ToString(Dt.Rows[i]["IsFeeOpenForStudent"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsFeeOpenForStudent"]);
                        element.IsFeeOpenForFaculty = (Convert.ToString(Dt.Rows[i]["IsFeeOpenForFaculty"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsFeeOpenForFaculty"]);
                        element.ExamMasterId = (((Dt.Rows[i]["ExamMasterId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["ExamMasterId"]);
                        element.SpecialisationId = (((Dt.Rows[i]["SpecialisationId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["SpecialisationId"]);
                        element.ProgInstancePartTermId = (((Dt.Rows[i]["ProgInstancePartTermId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["ProgInstancePartTermId"]);
                        element.ProgrammePartTermId = (((Dt.Rows[i]["ProgrammePartTermId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammePartTermId"]);


                        String PRN1 = element.PRN.ToString("D10");
                        String ProgrammeInstancePartTermId1 = element.ProgInstancePartTermId.ToString("D5");
                        String SpecialisationId1 = element.SpecialisationId.ToString("D4");
                        String ExamEventId = element.ExamMasterId.ToString("D3");
                        element.OrderId = string.Format("{0}{1}{2}{3}", PRN1, ProgrammeInstancePartTermId1, SpecialisationId1, ExamEventId);




                        ObjListExamInward.Add(element);
                    }
                }
                return ObjListExamInward;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public HallTicket getSingleHallTicketDetails(HallTicket dataString)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("getHallTicketDetails", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Flag", "getSingleHallTicketDetails");
                Cmd.Parameters.AddWithValue("@ExamMasterId", dataString.ExamMasterId);
                Cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", dataString.ProgrammeInstancePartTermId); // passing Datatable  
                Cmd.Parameters.AddWithValue("@PRN", dataString.PRN);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);
                HallTicket element = new HallTicket();
                if (Dt.Rows.Count > 0)
                {
                    element.ExamEventName = Dt.Rows[0]["ExamEvent"].ToString();
                    element.FacultyId = Convert.ToInt32(Dt.Rows[0]["FacultyId"].ToString());
                    element.FacultyName = Dt.Rows[0]["FacultyName"].ToString();
                    element.PRN = Convert.ToInt64(Dt.Rows[0]["PRN"].ToString());
                    element.SeatNumber = Dt.Rows[0]["SeatNumber"].ToString();
                    element.AppearanceTypeId = Convert.ToInt32(Dt.Rows[0]["AppearanceTypeId"].ToString());
                    element.NameAsPerMarksheet = Dt.Rows[0]["NameAsPerMarkSheet"].ToString();
                    if (!string.IsNullOrEmpty(Dt.Rows[0]["IsPhysicallyChallenged"].ToString()))
                    {
                        if (Convert.ToBoolean(Dt.Rows[0]["IsPhysicallyChallenged"].ToString()) == true)
                        {
                            element.IsPhysicallyChallenged = "Yes";
                        }
                        else
                        {
                            element.IsPhysicallyChallenged = "No";
                        }
                    }
                    else
                    {
                        element.IsPhysicallyChallenged = "-";
                    }
                    element.AppearanceType = Dt.Rows[0]["AppearanceType"].ToString();
                    element.Gender = Dt.Rows[0]["Gender"].ToString();
                    element.ExamCenter = Dt.Rows[0]["ExamCenter"].ToString();
                    element.InwardMode = Dt.Rows[0]["InwardMode"].ToString();
                    if (element.InwardMode == "OFFLINE")
                    {
                        element.MandatoryInstructionForOfflineExam = Dt.Rows[0]["MandatoryInstructionForOfflineExam"].ToString();
                        element.OptionalInstructionForOfflineExam = Dt.Rows[0]["OptionalInstructionForOfflineExam"].ToString();
                    }
                    else if (element.InwardMode == "ONLINE")
                    {
                        element.MandatoryInstructionForOnlineExam = Dt.Rows[0]["MandatoryInstructionForOfflineExam"].ToString();
                        element.OptionalInstructionForOnlineExam = Dt.Rows[0]["OptionalInstructionForOnlineExam"].ToString();
                    }
                    element.DyrExamSign = Dt.Rows[0]["DyrExamSign"].ToString();
                    element.ProgrammeInstancePartTermId = Convert.ToInt32(Dt.Rows[0]["ProgInstancePartTermId"].ToString());
                    element.StudentPhoto = Dt.Rows[0]["StudentPhoto"].ToString();
                    element.StudentSignature = Dt.Rows[0]["StudentSignature"].ToString();
                    element.VenueName = Dt.Rows[0]["VenueName"].ToString();
                }
                return element;
            }
            catch (Exception e)
            {
                return null;
            }
        }

         public List<CourseDetails> getPaperList(int? ExamMasterId, int? ProgrammeInstancePartTermId, Int64? PRN)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("getHallTicketDetails", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Flag", "getPaperDetails");
                Cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                Cmd.Parameters.AddWithValue("@PRN", PRN);
                Cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<CourseDetails> ObjListPaper = new List<CourseDetails>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        CourseDetails element = new CourseDetails();
                        element.Id = Convert.ToInt32(Dt.Rows[i][0].ToString());
                        element.PaperCode = Dt.Rows[i][1].ToString();
                        element.PaperName = Dt.Rows[i][2].ToString();
                        element.AssessmentMethod = Dt.Rows[i][3].ToString();
                        element.AssetmentType = Dt.Rows[i][4].ToString();
                        if (DBNull.Value.Equals(Dt.Rows[i][5]))
                        {
                            element.PaperDate = "Not Defined";
                        }
                        else
                        {
                            DateTime Date = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(Dt.Rows[i][5]).ToUniversalTime(), INDIAN_ZONE);
                            element.PaperDate = string.Format("{0: dd-MM-yyyy}", Date);
                        }
                        //element.PaperDate = Dt.Rows[i][5].ToString();
                        if(DBNull.Value.Equals(Dt.Rows[i][6]))
                        {
                            element.PaperTime = "Not Defined";
                        }
                        else
                        {
                            element.PaperTime = Dt.Rows[i][6].ToString();
                        }
                       

                        ObjListPaper.Add(element);
                    }
                    return ObjListPaper;
                }
                else
                {
                    CourseDetails ErrorObj = new CourseDetails();
                    List<CourseDetails> ErrorObjLst = new List<CourseDetails>();
                    ErrorObj.ExceptionMsg = Convert.ToString("Exam Papers are not configured yet.");
                    ErrorObjLst.Add(ErrorObj);
                    return ErrorObjLst;
                }
               
            }
            catch (Exception e)
            {
                CourseDetails ErrorObj = new CourseDetails();
                List<CourseDetails> ErrorObjLst = new List<CourseDetails>();
                ErrorObj.ExceptionMsg = Convert.ToString(e.Message);
                ErrorObjLst.Add(ErrorObj);
                return ErrorObjLst;
            }
        }
        public List<HallTicket> getBulkHallTicketList(HallTicket dataString)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("getHallTicketDetails", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Flag", "getBulkHallDetails");
                Cmd.Parameters.AddWithValue("@ExamMasterId", dataString.ExamMasterId);
                Cmd.Parameters.AddWithValue("@SpecialisationId", dataString.SpecialisationId);
                Cmd.Parameters.AddWithValue("@ProgrammePartTermId", dataString.ProgrammePartTermId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<HallTicket> ObjHallTicketList = new List<HallTicket>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        HallTicket element = new HallTicket();
                        element.ExamEventName = Dt.Rows[i]["DisplayName"].ToString();
                        element.FacultyId = Convert.ToInt32(Dt.Rows[i]["FacultyId"].ToString());
                        element.FacultyName = Dt.Rows[i]["FacultyName"].ToString();
                        element.PRN = Convert.ToInt64(Dt.Rows[i]["PRN"].ToString());
                        element.SeatNumber = Dt.Rows[i]["SeatNumber"].ToString();
                        element.AppearanceTypeId = Convert.ToInt32(Dt.Rows[i]["AppearanceTypeId"].ToString());
                        element.NameAsPerMarksheet = Dt.Rows[i]["NameAsPerMarkSheet"].ToString();
                        if (!string.IsNullOrEmpty(Dt.Rows[i]["IsPhysicallyChallenged"].ToString()))
                        {
                            if (Convert.ToBoolean(Dt.Rows[i]["IsPhysicallyChallenged"].ToString()) == true)
                            {
                                element.IsPhysicallyChallenged = "Yes";
                            }
                            else
                            {
                                element.IsPhysicallyChallenged = "No";
                            }
                        }
                        else
                        {
                            element.IsPhysicallyChallenged = "-";
                        }
                        element.AppearanceType = Dt.Rows[i]["AppearanceType"].ToString();
                        element.Gender = Dt.Rows[i]["Gender"].ToString();
                        element.ExamCenter = Dt.Rows[i]["ExamCenter"].ToString();
                        element.InwardMode = Dt.Rows[i]["InwardMode"].ToString();
                        if(element.InwardMode=="OFFLINE")
                        {
                            element.MandatoryInstructionForOfflineExam =Dt.Rows[i]["MandatoryInstructionForOfflineExam"].ToString();
                            element.OptionalInstructionForOfflineExam = Dt.Rows[i]["OptionalInstructionForOfflineExam"].ToString(); 
                        }
                        else if(element.InwardMode=="ONLINE")
                        {
                            element.MandatoryInstructionForOnlineExam = Dt.Rows[i]["MandatoryInstructionForOfflineExam"].ToString(); 
                            element.OptionalInstructionForOnlineExam = Dt.Rows[i]["OptionalInstructionForOnlineExam"].ToString(); 
                        }
                        element.DyrExamSign = Dt.Rows[i]["DyrExamSign"].ToString();
                        element.ProgrammeInstancePartTermId = Convert.ToInt32(Dt.Rows[i]["ProgInstancePartTermId"].ToString());
                        element.StudentPhoto = Dt.Rows[i]["StudentPhoto"].ToString();
                        element.StudentSignature = Dt.Rows[i]["StudentSignature"].ToString();
                        element.VenueName = Dt.Rows[i]["VenueName"].ToString();

                        ObjHallTicketList.Add(element);
                    }
                }
                return ObjHallTicketList;
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public BlankMarkSheets getPaperListForBlankMarksheet(BlankMarkSheets dataString)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("GetBlankMarksheetDetails", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Flag", "GetPaperListForBlankMarksheet");
                Cmd.Parameters.AddWithValue("@ExamEventMasterId", dataString.ExamMasterId);
                Cmd.Parameters.AddWithValue("@ProgrammePartTermId", dataString.ProgrammePartTermId);
                Cmd.Parameters.AddWithValue("@TeachingLearningMethodId", dataString.TeachingLearningMethodId);
                Cmd.Parameters.AddWithValue("@AssessmentMethodId", dataString.AssessmentMethodId);
                Cmd.Parameters.AddWithValue("@BranchId", dataString.BranchId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                BlankMarkSheets element = new BlankMarkSheets();

                if (Dt.Rows.Count > 0)
                {
                    element.PaperList = new List<MstPaper>();
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstPaper objPaper = new MstPaper();
                        objPaper.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objPaper.PaperName = Convert.ToString(Dt.Rows[i]["PaperName"]);
                        objPaper.PaperCode = Convert.ToString(Dt.Rows[i]["PaperCode"]);
                        

                        element.PaperList.Add(objPaper);
                    }
                }
                return element;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        //Gayatri's Code
        public BlankMarkSheets getPaperListForBlankMarksheetSectionwise(BlankMarkSheets dataString)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("GetBlankMarksheetDetails", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Flag", "GetPaperListForBlankMarksheetSectionwise");
                Cmd.Parameters.AddWithValue("@ExamEventMasterId", dataString.ExamMasterId);
                Cmd.Parameters.AddWithValue("@ProgrammePartTermId", dataString.ProgrammePartTermId);
                Cmd.Parameters.AddWithValue("@TeachingLearningMethodId", dataString.TeachingLearningMethodId);
                Cmd.Parameters.AddWithValue("@AssessmentMethodId", dataString.AssessmentMethodId);
                Cmd.Parameters.AddWithValue("@BranchId", dataString.BranchId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                BlankMarkSheets element = new BlankMarkSheets();

                if (Dt.Rows.Count > 0)
                {
                    element.PaperList = new List<MstPaper>();
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstPaper objPaper = new MstPaper();
                        objPaper.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objPaper.PaperName = Convert.ToString(Dt.Rows[i]["PaperName"]);
                        objPaper.PaperCode = Convert.ToString(Dt.Rows[i]["PaperCode"]);
                        objPaper.SectionName = Convert.ToString(Dt.Rows[i]["SectionName"]);
                        objPaper.SectionMarks = Convert.ToInt32(Dt.Rows[i]["SectionMarks"]);

                        element.PaperList.Add(objPaper);
                    }
                }
                return element;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<ExamFormMaster> getBlankMarksheetDetailsForUASectionwise(Sectionwiseblankmarksheet dataString)
        {

            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("GetBlankMarksheetDetails", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandTimeout = 360;
                Cmd.Parameters.AddWithValue("@Flag", "GetBlankMarksheetPRNListForUA");
                Cmd.Parameters.AddWithValue("@ExamEventMasterId", dataString.ExamMasterId);
                Cmd.Parameters.AddWithValue("@BranchId", dataString.BranchId);
                Cmd.Parameters.AddWithValue("@ProgrammePartTermId", dataString.ProgrammePartTermId);
                Cmd.Parameters.AddWithValue("@PaperId", dataString.PaperId);
                Cmd.Parameters.AddWithValue("@TeachingLearningMethodId", dataString.TeachingLearningMethodId);
                Cmd.Parameters.AddWithValue("@AssessmentMethodId", dataString.AssessmentMethodId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<ExamFormMaster> ObjList = new List<ExamFormMaster>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ExamFormMaster element = new ExamFormMaster();
                        element.PRN = Dt.Rows[i]["PRN"].ToString();
                        element.ProgInstancePartTermId = Convert.ToInt32(Dt.Rows[i]["ProgInstancePartTermId"].ToString());
                        element.SeatNumber = Dt.Rows[i]["SeatNumber"].ToString();
                        element.ExamMaster = Dt.Rows[i]["DisplayName"].ToString();
                        element.FacultyName = Dt.Rows[i]["InstituteName"].ToString();
                        element.NameAsPerMarksheet = Dt.Rows[i]["NameAsPerMarkSheet"].ToString();

                        ObjList.Add(element);
                    }
                    return ObjList;
                }
                else
                {
                    ExamFormMaster ErrorObj = new ExamFormMaster();
                    List<ExamFormMaster> ErrorObjLst = new List<ExamFormMaster>();
                    ErrorObj.ExceptionMsg = Convert.ToString("No Record Found");
                    ErrorObjLst.Add(ErrorObj);
                    return ErrorObjLst;
                }

            }
            catch (Exception e)
            {
                ExamFormMaster ErrorObj = new ExamFormMaster();
                List<ExamFormMaster> ErrorObjLst = new List<ExamFormMaster>();
                ErrorObj.ExceptionMsg = Convert.ToString(e.Message);
                ErrorObjLst.Add(ErrorObj);
                return ErrorObjLst;
                //return null;
            }
        }
        //End Gayatri's Code

        public List<ExamFormMaster> getBlankMarksheetDetailsForUA(BlankMarkSheets dataString)
        {
            
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("GetBlankMarksheetDetails", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
				Cmd.CommandTimeout = 360;
                Cmd.Parameters.AddWithValue("@Flag", "GetBlankMarksheetPRNListForUA");
                Cmd.Parameters.AddWithValue("@ExamEventMasterId", dataString.ExamMasterId);
                Cmd.Parameters.AddWithValue("@BranchId", dataString.BranchId);
                Cmd.Parameters.AddWithValue("@ProgrammePartTermId", dataString.ProgrammePartTermId);
                Cmd.Parameters.AddWithValue("@PaperId", dataString.PaperId);
                Cmd.Parameters.AddWithValue("@TeachingLearningMethodId", dataString.TeachingLearningMethodId);
                Cmd.Parameters.AddWithValue("@AssessmentMethodId", dataString.AssessmentMethodId);
                Cmd.Parameters.AddWithValue("@InstituteId", dataString.InstituteId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<ExamFormMaster> ObjList = new List<ExamFormMaster>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ExamFormMaster element = new ExamFormMaster();
                        element.PRN = Dt.Rows[i]["PRN"].ToString();
                        element.ProgInstancePartTermId = Convert.ToInt32(Dt.Rows[i]["ProgInstancePartTermId"].ToString());
                        element.SeatNumber = Dt.Rows[i]["SeatNumber"].ToString();
                        element.ExamMaster= Dt.Rows[i]["DisplayName"].ToString();
                        element.FacultyName = Dt.Rows[i]["InstituteName"].ToString();
                        element.NameAsPerMarksheet = Dt.Rows[i]["NameAsPerMarkSheet"].ToString();

                        ObjList.Add(element);
                    }
                    return ObjList;
                }
                else {
                    ExamFormMaster ErrorObj = new ExamFormMaster();
                    List<ExamFormMaster> ErrorObjLst = new List<ExamFormMaster>();
                    ErrorObj.ExceptionMsg = Convert.ToString("No Record Found");
                    ErrorObjLst.Add(ErrorObj);
                    return ErrorObjLst;
                }
                
            }
            catch (Exception e)
            {
                ExamFormMaster ErrorObj = new ExamFormMaster();
                List<ExamFormMaster> ErrorObjLst = new List<ExamFormMaster>();
                ErrorObj.ExceptionMsg = Convert.ToString(e.Message);
                ErrorObjLst.Add(ErrorObj);
                return ErrorObjLst;
                //return null;
            }
        }

        public List<ExamFormMaster> getBlankMarksheetDetailsForIA(BlankMarkSheets dataString, string AssessmentType)
        {

            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("GetBlankMarksheetDetails", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
				Cmd.CommandTimeout = 360;
                Cmd.Parameters.AddWithValue("@Flag", "GetBlankMarksheetPRNListForIA");
                Cmd.Parameters.AddWithValue("@ExamEventMasterId", dataString.ExamMasterId);
                Cmd.Parameters.AddWithValue("@BranchId", dataString.BranchId);
                Cmd.Parameters.AddWithValue("@ProgrammePartTermId", dataString.ProgrammePartTermId);
                Cmd.Parameters.AddWithValue("@PaperId", dataString.PaperId);
                Cmd.Parameters.AddWithValue("@TeachingLearningMethodId", dataString.TeachingLearningMethodId);
                Cmd.Parameters.AddWithValue("@AssessmentMethodId", dataString.AssessmentMethodId);
                Cmd.Parameters.AddWithValue("@AssessmentType", AssessmentType);
                Cmd.Parameters.AddWithValue("@InstituteId", dataString.InstituteId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<ExamFormMaster> ObjList = new List<ExamFormMaster>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ExamFormMaster element = new ExamFormMaster();
                        element.PRN = Dt.Rows[i]["PRN"].ToString();
                        element.ProgInstancePartTermId = Convert.ToInt32(Dt.Rows[i]["ProgInstancePartTermId"].ToString());
                        element.SeatNumber = Dt.Rows[i]["SeatNumber"].ToString();
                        element.ExamMaster = Dt.Rows[i]["DisplayName"].ToString();
                        element.FacultyName = Dt.Rows[i]["InstituteName"].ToString();
                        element.NameAsPerMarksheet = Dt.Rows[i]["NameAsPerMarkSheet"].ToString();

                        ObjList.Add(element);
                    }
                    return ObjList;
                }
                else
                {
                    ExamFormMaster ErrorObj = new ExamFormMaster();
                    List<ExamFormMaster> ErrorObjLst = new List<ExamFormMaster>();
                    ErrorObj.ExceptionMsg = Convert.ToString("No Record Found");
                    ErrorObjLst.Add(ErrorObj);
                    return ErrorObjLst;
                }

            }
            catch (Exception e)
            {
                ExamFormMaster ErrorObj = new ExamFormMaster();
                List<ExamFormMaster> ErrorObjLst = new List<ExamFormMaster>();
                ErrorObj.ExceptionMsg = Convert.ToString(e.Message);
                ErrorObjLst.Add(ErrorObj);
                return ErrorObjLst;
                //return null;
            }
        }

    }
}