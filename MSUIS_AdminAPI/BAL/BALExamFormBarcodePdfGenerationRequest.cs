using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace MSUISApi.BAL
{
    public class BALExamFormBarcodePdfGenerationRequest
    {
        public string saveExamFormBarcodePdfGenerationRequest(ExamFormBarcodePdfGenerationRequest dataString)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            SqlDataAdapter Da = new SqlDataAdapter();
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), INDIAN_ZONE);

            SqlCommand cmd = new SqlCommand("ExamFormBarcodePDFGenerationRequestAdd", Con);
            cmd.CommandType = CommandType.StoredProcedure;

            if (string.IsNullOrEmpty(dataString.Id.ToString()))
            {
                cmd.Parameters.AddWithValue("@id", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@id", dataString.Id);
            }
            cmd.Parameters.AddWithValue("@ExamMasterId", dataString.ExamMasterId);
            cmd.Parameters.AddWithValue("@FacultyExamMapId", dataString.FacultyExamMapId);
            cmd.Parameters.AddWithValue("@ProgrammeId", dataString.ProgrammeId);
            cmd.Parameters.AddWithValue("@ProgrammePartTermId", dataString.ProgrammePartTermId);
            cmd.Parameters.AddWithValue("@SpecialisationIds", dataString.SpecialisationIds);
            cmd.Parameters.AddWithValue("@PaperIds", dataString.PaperIds);
            cmd.Parameters.AddWithValue("@UserTime", datetime);
            cmd.Parameters.AddWithValue("@UserId", dataString.CreatedBy);


            cmd.Parameters.Add("@message", SqlDbType.NVarChar, 500);
            cmd.Parameters["@message"].Direction = ParameterDirection.Output;

            Con.Open();
            cmd.ExecuteNonQuery();
            string message = Convert.ToString(cmd.Parameters["@message"].Value);
            Con.Close();


            return message;

        }

        public string updateExamFormBarcodePdfGenerationRequest(ExamFormBarcodePdfGenerationRequest dataString)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            SqlDataAdapter Da = new SqlDataAdapter();
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), INDIAN_ZONE);

            SqlCommand cmd = new SqlCommand("ExamFormBarcodePdfGenerationRequestEdit", Con);
            cmd.CommandType = CommandType.StoredProcedure;
          
            cmd.Parameters.AddWithValue("@Id", dataString.Id);
            cmd.Parameters.AddWithValue("@ZipFile", dataString.ZipFile);
            cmd.Parameters.AddWithValue("@UserTime", datetime);

            cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
            cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

            Con.Open();
            cmd.ExecuteNonQuery();
            string message = Convert.ToString(cmd.Parameters["@Message"].Value);
            Con.Close();


            return message;

        }

        public string UpdateUnderProcessStatus(ExamFormBarcodePdfGenerationRequest dataString)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            SqlDataAdapter Da = new SqlDataAdapter();


            SqlCommand cmd = new SqlCommand("UpdateUnderProcessStatus", Con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", dataString.Id);


            cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
            cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

            Con.Open();
            cmd.ExecuteNonQuery();
            string message = Convert.ToString(cmd.Parameters["@Message"].Value);
            Con.Close();


            return message;

        }

        public List<ExamFormBarcodePdfGenerationRequest> GetDetailsOfExamFormBarcodePdfGenerationRequest()
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("ExamFormBarcodePdfGenerationRequestGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<ExamFormBarcodePdfGenerationRequest> ObjListExamFormBarcode = new List<ExamFormBarcodePdfGenerationRequest>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ExamFormBarcodePdfGenerationRequest element = new ExamFormBarcodePdfGenerationRequest();
                        element.Id = Convert.ToInt16(Dt.Rows[i]["Id"].ToString());
                        element.ExamMasterId = Convert.ToInt16(Dt.Rows[i]["ExamMasterId"].ToString());
                        element.ExamEvent = Dt.Rows[i]["ExamEvent"].ToString();
                        element.FacultyExamMapId = Convert.ToInt16(Dt.Rows[i]["FacultyExamMapId"].ToString());
                        element.FacultyExamMap = Dt.Rows[i]["FacultyExamMap"].ToString();
                        element.ProgrammeId = Convert.ToInt16(Dt.Rows[i]["ProgrammeId"].ToString());
                        element.Programme = Dt.Rows[i]["ProgrammeName"].ToString();
                        element.ProgrammePartTermId = Convert.ToInt16(Dt.Rows[i]["ProgrammePartTermId"].ToString());
                        element.ProgrammePartTerm = Dt.Rows[i]["PartTermName"].ToString();
                        element.SpecialisationIds = Dt.Rows[i]["SpecialisationIds"].ToString();
                        element.SpecialisationNames = Dt.Rows[i]["BranchName"].ToString();
                        element.PaperIds = Dt.Rows[i]["PaperIds"].ToString();
                        element.PaperNames = Dt.Rows[i]["PaperName"].ToString();
                        element.IsGenerated = Convert.ToBoolean(Dt.Rows[i]["IsGenerated"].ToString());
                        element.ZipFile = Dt.Rows[i]["ZipFileName"].ToString();

                        ObjListExamFormBarcode.Add(element);
                    }
                }
                return ObjListExamFormBarcode;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public ExamFormBarcodePdfGenerationRequest GetPendingandUnderProcessExamFormBarcodePDfRequest()
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("GetPendingandUnderProcessExamFormBarcodePDfRequest", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                ExamFormBarcodePdfGenerationRequest element = new ExamFormBarcodePdfGenerationRequest();

                if (Dt.Rows.Count > 0)
                {


                    element.Id = Convert.ToInt16(Dt.Rows[0]["Id"].ToString());
                    element.ExamMasterId = Convert.ToInt16(Dt.Rows[0]["ExamMasterId"].ToString());
                    element.ExamEvent = Dt.Rows[0]["ExamEvent"].ToString();
                    element.FacultyExamMapId = Convert.ToInt16(Dt.Rows[0]["FacultyExamMapId"].ToString());
                    element.FacultyExamMap = Dt.Rows[0]["FacultyExamMap"].ToString();
                    element.ProgrammeId = Convert.ToInt16(Dt.Rows[0]["ProgrammeId"].ToString());
                    element.Programme = Dt.Rows[0]["ProgrammeName"].ToString();
                    element.ProgrammePartTermId = Convert.ToInt16(Dt.Rows[0]["ProgrammePartTermId"].ToString());
                    element.ProgrammePartTerm = Dt.Rows[0]["PartTermName"].ToString();
                    element.SpecialisationIds = Dt.Rows[0]["SpecialisationIds"].ToString();
                    element.PaperIds = Dt.Rows[0]["PaperIds"].ToString();
                    
                    element.IsGenerated = Convert.ToBoolean(Dt.Rows[0]["IsGenerated"].ToString());
                    element.ZipFile = Dt.Rows[0]["ZipFileName"].ToString();

                    return element;

                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}