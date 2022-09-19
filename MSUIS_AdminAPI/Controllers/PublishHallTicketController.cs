using MSUIS_TokenManager.App_Start;
using MSUISApi.BAL;
using MSUISApi.Models;
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
    public class PublishHallTicketController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        [HttpPost]
        public HttpResponseMessage FacultyExamMapStatusGetById(PublishHallTicket dataString)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                String res = ac.ValidateToken(token);
                String resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int64 UserId = Convert.ToInt64(res.ToString());
                
                Int32 Id = Convert.ToInt32(dataString.FacultyExamMapId);
                //String NewBaseUrlForPhoto = "https://localhost:44374/Upload/HallTicketSignature/";

                String dirphoto = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/HallTicketSignature/");
                SqlCommand Cmd = new SqlCommand("PublishHallTicketGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@Id", Id);
                Da.SelectCommand = Cmd;




                Da.Fill(Dt);
                List<PublishHallTicket> List = new List<PublishHallTicket>();
                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        PublishHallTicket ele = new PublishHallTicket();
                        ele.Id = (((Dt.Rows[i]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["Id"]);
                        ele.AttachedVenueCount = (((Dt.Rows[i]["AttachedVenueCount"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["AttachedVenueCount"]);
                        ele.ExamFormGenratedCount = (((Dt.Rows[i]["ExamFormGenratedCount"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["ExamFormGenratedCount"]);
                        ele.ExamformInwardCount = (((Dt.Rows[i]["ExamformInwardCount"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["ExamformInwardCount"]);
                        ele.ExamformSeatNoGenCount = (((Dt.Rows[i]["ExamformSeatNoGenCount"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["ExamformSeatNoGenCount"]);
                        ele.ExamformVenueAttachedCount = (((Dt.Rows[i]["ExamformVenueAttachedCount"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["ExamformVenueAttachedCount"]);
                        ele.ExamMasterId = (((Dt.Rows[i]["ExamMasterId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["ExamMasterId"]);
                        ele.FacultyExamMapId = (((Dt.Rows[i]["FacultyExamMapId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyExamMapId"]);
                        ele.ProgrammePartTermId = (((Dt.Rows[i]["ProgrammePartTermId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammePartTermId"]);
                        ele.PartTermName = (Convert.ToString(Dt.Rows[i]["PartTermName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["PartTermName"]);
                        ele.PartName = (Convert.ToString(Dt.Rows[i]["PartName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["PartName"]);
                        ele.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        ele.MandatoryInstructionForOfflineExam = (Convert.ToString(Dt.Rows[i]["MandatoryInstructionForOfflineExam"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["MandatoryInstructionForOfflineExam"]);
                        ele.MandatoryInstructionForOnlineExam = (Convert.ToString(Dt.Rows[i]["MandatoryInstructionForOnlineExam"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["MandatoryInstructionForOnlineExam"]);
                        ele.OptionalInstructionForOfflineExam = (Convert.ToString(Dt.Rows[i]["OptionalInstructionForOfflineExam"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["OptionalInstructionForOfflineExam"]);
                        ele.OptionalInstructionForOnlineExam = (Convert.ToString(Dt.Rows[i]["OptionalInstructionForOnlineExam"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["OptionalInstructionForOnlineExam"]);
                        ele.HallTicketPublished = (((Dt.Rows[i]["HallTicketPublished"]).ToString()).IsEmpty()) ? false : Convert.ToBoolean(Dt.Rows[i]["HallTicketPublished"]);
                        ele.DyrExamSignDisplay = (Convert.ToString(Dt.Rows[i]["DyrExamSign"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["DyrExamSign"]);
                        var DyrExamSign = Dt.Rows[i]["DyrExamSign"];
                        if (DyrExamSign is DBNull)
                        {
                            //ele.DyrExamSign = NewBaseUrlForPhoto + "DemoSignature_HallTicket.jpg";
                            string pathphoto = Path.GetFullPath(dirphoto) + "DemoSignature_HallTicket.jpg";
                            byte[] imageArrayphoto = System.IO.File.ReadAllBytes(pathphoto);
                            string base64PhotoRepresentation = Convert.ToBase64String(imageArrayphoto);
                            ele.DyrExamSign = base64PhotoRepresentation;
                        }
                        else
                        {
                            //ele.DyrExamSign = NewBaseUrlForPhoto + Convert.ToString(Dt.Rows[i]["DyrExamSign"]);
                            string pathphoto = Path.GetFullPath(dirphoto) + Convert.ToString(Dt.Rows[i]["DyrExamSign"]);
                            byte[] imageArrayphoto = System.IO.File.ReadAllBytes(pathphoto);
                            string base64PhotoRepresentation = Convert.ToBase64String(imageArrayphoto);
                            ele.DyrExamSign = base64PhotoRepresentation;
                        }
                        List.Add(ele);
                    }                    
                }

                return Return.returnHttp("200", List, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        
        [HttpPost]
        public HttpResponseMessage PublishHallTicketTrue(PublishHallTicket data)
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
                if (data.IsExamModeOnline == true)
                {
                    if (data.MandatoryInstructionForOnlineExam == "Not Defined" && data.OptionalInstructionForOnlineExam == "Not Defined")
                    {
                        return Return.returnHttp("201", "Instructions can not be Blank", null);
                    }
                }
                else
                {
                    if (data.MandatoryInstructionForOfflineExam == "Not Defined" && data.OptionalInstructionForOfflineExam == "Not Defined")
                    {
                        return Return.returnHttp("201", "Instructions can not be Blank", null);
                    }
                }
                if (data.DyrExamSignDisplay == "Not Defined") 
                {
                    return Return.returnHttp("201", "Dyr Exam Sign can not be blank", null);
                }

                SqlCommand cmd = new SqlCommand("HallticketPublish", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "HallticketPublish");
                cmd.Parameters.AddWithValue("@Id", data.Id);
                cmd.Parameters.AddWithValue("@IsExamModeOnline", data.IsExamModeOnline);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);




                cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@MESSAGE"].Value);
                Con.Close();

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


    }
}

