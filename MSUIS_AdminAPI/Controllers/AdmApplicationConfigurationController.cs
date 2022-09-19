using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using MSUISApi.Models;
using Newtonsoft.Json.Linq; 
using System.Reflection.Emit;
using System.Web.WebPages;
using MSUIS_TokenManager.App_Start;

namespace MSUISApi.Controllers
{
    public class AdmApplicationConfigurationController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        //Validation validation = new Validation();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();


        //AdmApplicationConfiguration table CRUD

        [HttpPost]
        public HttpResponseMessage AdmApplicationConfigurationGet()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("AdmApplicationConfigurationGet", con);

                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<AdmApplicationConfiguration> ObjLstAACList = new List<AdmApplicationConfiguration>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AdmApplicationConfiguration ObjLstAAC = new AdmApplicationConfiguration();

                        ObjLstAAC.Id = Convert.ToInt32(dr["Id"]);
                        ObjLstAAC.ProgrammeName = Convert.ToString(dr["ProgrammeName"].ToString());
                        ObjLstAAC.FacultyName = Convert.ToString(dr["FacultyName"].ToString());
                        ObjLstAAC.PartTermName = Convert.ToString(dr["PartTermName"].ToString());
                        ObjLstAAC.ProgrammeId = Convert.ToInt32(dr["ProgrammeId"].ToString());
                        ObjLstAAC.ApplicationStartDateView = string.Format("{0: dd-MM-yyyy}", dr["ApplicationStartDate"]);
                        //ObjLstAAC.ApplicationStartDate = Convert.ToDateTime(Convert.ToDateTime(dr["ApplicationStartDate"]).ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES")));
                        ObjLstAAC.ApplicationStopDateView = string.Format("{0: dd-MM-yyyy}", dr["ApplicationStopDate"]);
                        ObjLstAAC.ApplicationFeeStartDateView = string.Format("{0: dd-MM-yyyy}", dr["ApplicationFeeStartDate"]);
                        ObjLstAAC.ApplicationFeeEndDateView = string.Format("{0: dd-MM-yyyy}", dr["ApplicationFeeEndDate"]);
                        ObjLstAAC.AdmissionFeesStartDateView = string.Format("{0: dd-MM-yyyy}", dr["AdmissionFeesStartDate"]);
                        ObjLstAAC.AdmissionFeesStopDateView = string.Format("{0: dd-MM-yyyy}", dr["AdmissionFeesStopDate"]);
                        ObjLstAAC.UpdatedDocLastDateView = string.Format("{0: dd-MM-yyyy}", dr["UpdatedDocLastDate"]);
                        ObjLstAAC.AdmInstallementEndDateView = string.Format("{0: dd-MM-yyyy}", dr["AdmInstallementEndDate"]);
                        ObjLstAAC.Remarks = Convert.ToString(dr["Remarks"].ToString());
                        ObjLstAAC.ProgramInstancePartTermId = Convert.ToInt32(dr["ProgramInstancePartTermId"].ToString());
                        ObjLstAAC.visibility = Convert.ToString(dr["visibility"].ToString());
                        ObjLstAAC.ProspectusURL = Convert.ToString(dr["ProspectusURL"].ToString());
                        ObjLstAAC.WrittenTestSyllabusURL = Convert.ToString(dr["WrittenTestSyllabusURL"].ToString());
                        ObjLstAAC.ContactPersonEmail = Convert.ToString(dr["ContactPersonEmail"].ToString());
                        ObjLstAAC.ContactPersonName = Convert.ToString(dr["ContactPersonName"].ToString());
                        ObjLstAAC.ContactPersonNo = Convert.ToString(dr["ContactPersonNo"].ToString());

                        ObjLstAACList.Add(ObjLstAAC);
                    }
                    return Return.returnHttp("200", ObjLstAACList, null);
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

        [HttpPost]
        public HttpResponseMessage AdmApplicationConfigurationGetById(AdmApplicationConfiguration ObjAppConfig)
        {
            AdmApplicationConfiguration modelobj = new AdmApplicationConfiguration();

            try
            {
                modelobj.ProgrammeInstancePartTermId = ObjAppConfig.ProgrammeInstancePartTermId;
                SqlCommand cmd = new SqlCommand("PreAdmApplicationConfigurationGet", con);
                SqlDataAdapter da = new SqlDataAdapter();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", modelobj.ProgrammeInstancePartTermId);
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<AdmApplicationConfiguration> ObjLstAACList = new List<AdmApplicationConfiguration>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AdmApplicationConfiguration ObjLstAAC = new AdmApplicationConfiguration();

                        ObjLstAAC.Id = Convert.ToInt32(dr["Id"]);
                        ObjLstAAC.ProgrammeName = Convert.ToString(dr["ProgrammeName"].ToString());
                        ObjLstAAC.FacultyName = Convert.ToString(dr["FacultyName"].ToString());
                        ObjLstAAC.PartTermName = Convert.ToString(dr["PartTermName"].ToString());
                        ObjLstAAC.ProgrammeId = Convert.ToInt32(dr["ProgrammeId"].ToString());
                        ObjLstAAC.ApplicationStartDateView = string.Format("{0: dd-MM-yyyy}", dr["ApplicationStartDate"]);
                        //ObjLstAAC.ApplicationStartDate = Convert.ToDateTime(Convert.ToDateTime(dr["ApplicationStartDate"]).ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES")));
                        ObjLstAAC.ApplicationStopDateView = string.Format("{0: dd-MM-yyyy}", dr["ApplicationStopDate"]);
                        ObjLstAAC.ApplicationFeeStartDateView = string.Format("{0: dd-MM-yyyy}", dr["ApplicationFeeStartDate"]);
                        ObjLstAAC.ApplicationFeeEndDateView = string.Format("{0: dd-MM-yyyy}", dr["ApplicationFeeEndDate"]);
                        ObjLstAAC.AdmissionFeesStartDateView = string.Format("{0: dd-MM-yyyy}", dr["AdmissionFeesStartDate"]);
                        ObjLstAAC.AdmissionFeesStopDateView = string.Format("{0: dd-MM-yyyy}", dr["AdmissionFeesStopDate"]);
                        ObjLstAAC.ApplicationApprovalStartDateView = string.Format("{0: dd-MM-yyyy}", dr["ApplicationApprovalStartDate"]);
                        ObjLstAAC.ApplicationApprovalStopDateView = string.Format("{0: dd-MM-yyyy}", dr["ApplicationApprovalStopDate"]);
                        ObjLstAAC.UpdatedDocLastDateView = string.Format("{0: dd-MM-yyyy}", dr["UpdatedDocLastDate"]);
                        ObjLstAAC.AdmInstallementEndDateView = string.Format("{0: dd-MM-yyyy}", dr["AdmInstallementEndDate"]);
                        ObjLstAAC.Remarks = Convert.ToString(dr["Remarks"].ToString());
                        ObjLstAAC.ProgramInstancePartTermId = Convert.ToInt32(dr["ProgramInstancePartTermId"].ToString());
                        ObjLstAAC.visibility = Convert.ToString(dr["visibility"].ToString());
                        ObjLstAAC.ProspectusURL = Convert.ToString(dr["ProspectusURL"].ToString());
                        ObjLstAAC.WrittenTestSyllabusURL = Convert.ToString(dr["WrittenTestSyllabusURL"].ToString());
                        ObjLstAAC.WrittenTestScheduleURL = Convert.ToString(dr["WrittenTestScheduleURL"].ToString());
                        ObjLstAAC.ContactPersonEmail = Convert.ToString(dr["ContactPersonEmail"].ToString());
                        ObjLstAAC.ContactPersonName = Convert.ToString(dr["ContactPersonName"].ToString());
                        ObjLstAAC.ContactPersonNo = Convert.ToString(dr["ContactPersonNo"].ToString());
                        ObjLstAAC.TotalAmount = (((dr["TotalAmount"]).ToString()).IsEmpty()) ? 0 : Convert.ToDouble(dr["TotalAmount"].ToString());                        
                        ObjLstAAC.FeeCategoryName = (((dr["FeeCategoryName"]).ToString()).IsEmpty()) ? "Not Defined" : Convert.ToString(dr["FeeCategoryName"].ToString());
                        ObjLstAAC.FeeSts = (((dr["FeeSts"]).ToString()).IsEmpty()) ? false : Convert.ToBoolean(dr["FeeSts"]);

                        ObjLstAACList.Add(ObjLstAAC);
                    }
                    return Return.returnHttp("200", ObjLstAACList, null);
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

        [HttpPost]
        public HttpResponseMessage insertAdmApplicationConfiguration(AdmApplicationConfiguration applicationConfiguration)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("AdmApplicationConfigurationAdd", con);
                cmd.CommandType = CommandType.StoredProcedure;

                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                AdmApplicationConfiguration aaconfig = new AdmApplicationConfiguration();

                TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

                aaconfig.ProgramInstancePartTermId = Convert.ToInt32(applicationConfiguration.ProgramInstancePartTermId);
                aaconfig.AcademicYearId = applicationConfiguration.AcademicYearId;
                aaconfig.FacultyId = applicationConfiguration.FacultyId;
                aaconfig.ProgrammeId = applicationConfiguration.ProgrammeId;
                aaconfig.SpecialisationId = applicationConfiguration.SpecialisationId;
                aaconfig.ProgrammePartId = applicationConfiguration.ProgrammePartId;
                aaconfig.ProgrammePartTermId = applicationConfiguration.ProgrammePartTermId;

                aaconfig.Remarks = applicationConfiguration.Remarks;
                aaconfig.ProspectusURL = applicationConfiguration.ProspectusURL;
                aaconfig.WrittenTestSyllabusURL = applicationConfiguration.WrittenTestSyllabusURL;
                aaconfig.WrittenTestScheduleURL = applicationConfiguration.WrittenTestScheduleURL;
                aaconfig.ContactPersonEmail = applicationConfiguration.ContactPersonEmail;
                aaconfig.ContactPersonName = applicationConfiguration.ContactPersonName;
                aaconfig.ContactPersonNo = applicationConfiguration.ContactPersonNo;
                aaconfig.CreatedBy = Convert.ToInt32(responseToken);


                if (applicationConfiguration.ApplicationStartDate != null)
                {
                    DateTime dtAppStartDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(applicationConfiguration.ApplicationStartDate), out dtAppStartDate);
                    if (ChkDt)
                    {
                        //aaconfig.ApplicationStartDate = dtAppStartDate;
                        aaconfig.ApplicationStartDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(applicationConfiguration.ApplicationStartDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

                    }
                    else
                    {
                        aaconfig.ApplicationStartDate = null;
                    }
                }
                else
                {
                    aaconfig.ApplicationStartDate = null;
                }

                if (applicationConfiguration.ApplicationStopDate != null)
                {
                    DateTime dtAppStopDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(applicationConfiguration.ApplicationStopDate), out dtAppStopDate);
                    if (ChkDt)
                    {
                        aaconfig.ApplicationStopDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(applicationConfiguration.ApplicationStopDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    }
                    else
                    {
                        aaconfig.ApplicationStopDate = null;
                    }
                }
                else
                {
                    aaconfig.ApplicationStopDate = null;
                }

                if (applicationConfiguration.ApplicationFeeStartDate != null)
                {
                    DateTime dtAppFeeStartDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(applicationConfiguration.ApplicationFeeStartDate), out dtAppFeeStartDate);
                    if (ChkDt)
                    {
                        aaconfig.ApplicationFeeStartDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(applicationConfiguration.ApplicationFeeStartDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    }
                    else
                    {
                        aaconfig.ApplicationFeeStartDate = null;
                    }
                }
                else
                {
                    aaconfig.ApplicationFeeStartDate = null;
                }

                if (applicationConfiguration.ApplicationFeeEndDate != null)
                {
                    DateTime dtAppFeeEndDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(applicationConfiguration.ApplicationFeeEndDate), out dtAppFeeEndDate);
                    if (ChkDt)
                    {
                        aaconfig.ApplicationFeeEndDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(applicationConfiguration.ApplicationFeeEndDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    }
                    else
                    {
                        aaconfig.ApplicationFeeEndDate = null;
                    }
                }
                else
                {
                    aaconfig.ApplicationFeeEndDate = null;
                }

                if (applicationConfiguration.AdmissionFeesStartDate != null)
                {
                    DateTime dtAdmFeeStartDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(applicationConfiguration.AdmissionFeesStartDate), out dtAdmFeeStartDate);
                    if (ChkDt)
                    {
                        aaconfig.AdmissionFeesStartDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(applicationConfiguration.AdmissionFeesStartDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    }
                    else
                    {
                        aaconfig.AdmissionFeesStartDate = null;
                    }
                }
                else
                {
                    aaconfig.AdmissionFeesStartDate = null;
                }

                if (applicationConfiguration.AdmissionFeesStopDate != null)
                {
                    DateTime dtAdmFeeStopDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(applicationConfiguration.AdmissionFeesStopDate), out dtAdmFeeStopDate);
                    if (ChkDt)
                    {
                        aaconfig.AdmissionFeesStopDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(applicationConfiguration.AdmissionFeesStopDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    }
                    else
                    {
                        aaconfig.AdmissionFeesStopDate = null;
                    }
                }
                else
                {
                    aaconfig.AdmissionFeesStopDate = null;
                }

                if (applicationConfiguration.ApplicationApprovalStartDate != null)
                {
                    DateTime dtApprovalStartDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(applicationConfiguration.ApplicationApprovalStartDate), out dtApprovalStartDate);
                    if (ChkDt)
                    {
                        aaconfig.ApplicationApprovalStartDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(applicationConfiguration.ApplicationApprovalStartDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    }
                    else
                    {
                        aaconfig.ApplicationApprovalStartDate = null;
                    }
                }
                else
                {
                    aaconfig.ApplicationApprovalStartDate = null;
                }

                if (applicationConfiguration.ApplicationApprovalStopDate != null)
                {
                    DateTime dtApprovalStopDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(applicationConfiguration.ApplicationApprovalStopDate), out dtApprovalStopDate);
                    if (ChkDt)
                    {
                        aaconfig.ApplicationApprovalStopDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(applicationConfiguration.ApplicationApprovalStopDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    }
                    else
                    {
                        aaconfig.ApplicationApprovalStopDate = null;
                    }
                }
                else
                {
                    aaconfig.ApplicationApprovalStopDate = null;
                }

                if (applicationConfiguration.UpdatedDocLastDate != null)
                {
                    DateTime dtdocLastDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(applicationConfiguration.UpdatedDocLastDate), out dtdocLastDate);
                    if (ChkDt)
                    {
                        aaconfig.UpdatedDocLastDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(applicationConfiguration.UpdatedDocLastDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    }
                    else
                    {
                        aaconfig.UpdatedDocLastDate = null;
                    }
                }
                else
                {
                    aaconfig.UpdatedDocLastDate = null;
                }

                if (applicationConfiguration.AdmInstallementEndDate != null)
                {
                    DateTime dtAdmInstallementEndDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(applicationConfiguration.AdmInstallementEndDate), out dtAdmInstallementEndDate);
                    if (ChkDt)
                    {
                        aaconfig.AdmInstallementEndDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(applicationConfiguration.AdmInstallementEndDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    }
                    else
                    {
                        aaconfig.AdmInstallementEndDate = null;
                    }
                }
                else
                {
                    aaconfig.AdmInstallementEndDate = null;
                }



                cmd.Parameters.AddWithValue("@FinalInstancePartTermId", aaconfig.ProgramInstancePartTermId);
                cmd.Parameters.AddWithValue("@AcademicYearId", aaconfig.AcademicYearId);
                cmd.Parameters.AddWithValue("@FacultyId", aaconfig.FacultyId);
                cmd.Parameters.AddWithValue("@ProgrammeId", aaconfig.ProgrammeId);
                cmd.Parameters.AddWithValue("@SpecialisationId", aaconfig.SpecialisationId);
                cmd.Parameters.AddWithValue("@ProgrammePartId", aaconfig.ProgrammePartId);
                cmd.Parameters.AddWithValue("@ProgrammePartTermId", aaconfig.ProgrammePartTermId);
                cmd.Parameters.AddWithValue("@ApplicationStartDate", aaconfig.ApplicationStartDate);
                cmd.Parameters.AddWithValue("@ApplicationStopDate", aaconfig.ApplicationStopDate);
                cmd.Parameters.AddWithValue("@ApplicationFeeStartDate", aaconfig.ApplicationFeeStartDate);
                cmd.Parameters.AddWithValue("@ApplicationFeeEndDate", aaconfig.ApplicationFeeEndDate);
                cmd.Parameters.AddWithValue("@AdmissionFeesStartDate", aaconfig.AdmissionFeesStartDate);
                cmd.Parameters.AddWithValue("@AdmissionFeesStopDate", aaconfig.AdmissionFeesStopDate);
                cmd.Parameters.AddWithValue("@ApplicationApprovalStartDate", aaconfig.ApplicationApprovalStartDate);
                cmd.Parameters.AddWithValue("@ApplicationApprovalStopDate", aaconfig.ApplicationApprovalStopDate);
                cmd.Parameters.AddWithValue("@UpdatedDocLastDate", aaconfig.UpdatedDocLastDate);
                cmd.Parameters.AddWithValue("@AdmInstallementEndDate", aaconfig.AdmInstallementEndDate);
                cmd.Parameters.AddWithValue("@Remarks", aaconfig.Remarks);
                cmd.Parameters.AddWithValue("@ProspectusURL", aaconfig.ProspectusURL);
                cmd.Parameters.AddWithValue("@WrittenTestSyllabusURL", aaconfig.WrittenTestSyllabusURL);
                cmd.Parameters.AddWithValue("@WrittenTestScheduleURL", aaconfig.WrittenTestScheduleURL);
                cmd.Parameters.AddWithValue("@ContactPersonNo", aaconfig.ContactPersonNo);
                cmd.Parameters.AddWithValue("@ContactPersonName", aaconfig.ContactPersonName);
                cmd.Parameters.AddWithValue("@ContactPersonEmail", aaconfig.ContactPersonEmail);
                cmd.Parameters.AddWithValue("@CreatedBy", aaconfig.CreatedBy);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;


                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                //string ProgInstancePart = Convert.ToString(cmd.Parameters["@ProgInstancePart"].Value);
                con.Close();

                if (strMessage.Equals("TRUE"))
                {
                    strMessage = "Data has been saved successfully";
                }

                return Return.returnHttp("200", strMessage, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }

        [HttpPost]
        public HttpResponseMessage updateAdmApplicationConfiguration(AdmApplicationConfiguration applicationConfiguration)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("AdmApplicationConfigurationEdit", con);
                cmd.CommandType = CommandType.StoredProcedure;

                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }


                AdmApplicationConfiguration aaconfig = new AdmApplicationConfiguration();

                TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

                aaconfig.Id = Convert.ToInt32(applicationConfiguration.Id);
                aaconfig.ProgramInstancePartTermId = Convert.ToInt32(applicationConfiguration.ProgramInstancePartTermId);
                aaconfig.Remarks = applicationConfiguration.Remarks;
                aaconfig.ProspectusURL = applicationConfiguration.ProspectusURL;
                aaconfig.WrittenTestSyllabusURL = applicationConfiguration.WrittenTestSyllabusURL;
                aaconfig.WrittenTestScheduleURL = applicationConfiguration.WrittenTestScheduleURL;
                aaconfig.ContactPersonEmail = applicationConfiguration.ContactPersonEmail;
                aaconfig.ContactPersonName = applicationConfiguration.ContactPersonName;
                aaconfig.ContactPersonNo = applicationConfiguration.ContactPersonNo;
                aaconfig.ModifiedBy = Convert.ToInt32(responseToken);

                if (applicationConfiguration.ApplicationStartDate != null)
                {
                    DateTime dtAppStartDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(applicationConfiguration.ApplicationStartDate), out dtAppStartDate);
                    if (ChkDt)
                    {
                        //aaconfig.ApplicationStartDate = dtAppStartDate;
                        aaconfig.ApplicationStartDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(applicationConfiguration.ApplicationStartDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

                    }
                    else
                    {
                        aaconfig.ApplicationStartDate = null;
                    }
                }
                else
                {
                    aaconfig.ApplicationStartDate = null;
                }

                if (applicationConfiguration.ApplicationStopDate != null)
                {
                    DateTime dtAppStopDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(applicationConfiguration.ApplicationStopDate), out dtAppStopDate);
                    if (ChkDt)
                    {
                        aaconfig.ApplicationStopDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(applicationConfiguration.ApplicationStopDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    }
                    else
                    {
                        aaconfig.ApplicationStopDate = null;
                    }
                }
                else
                {
                    aaconfig.ApplicationStopDate = null;
                }

                if (applicationConfiguration.ApplicationFeeStartDate != null)
                {
                    DateTime dtAppFeeStartDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(applicationConfiguration.ApplicationFeeStartDate), out dtAppFeeStartDate);
                    if (ChkDt)
                    {
                        aaconfig.ApplicationFeeStartDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(applicationConfiguration.ApplicationFeeStartDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    }
                    else
                    {
                        aaconfig.ApplicationFeeStartDate = null;
                    }
                }
                else
                {
                    aaconfig.ApplicationFeeStartDate = null;
                }

                if (applicationConfiguration.ApplicationFeeEndDate != null)
                {
                    DateTime dtAppFeeEndDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(applicationConfiguration.ApplicationFeeEndDate), out dtAppFeeEndDate);
                    if (ChkDt)
                    {
                        aaconfig.ApplicationFeeEndDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(applicationConfiguration.ApplicationFeeEndDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    }
                    else
                    {
                        aaconfig.ApplicationFeeEndDate = null;
                    }
                }
                else
                {
                    aaconfig.ApplicationFeeEndDate = null;
                }

                if (applicationConfiguration.AdmissionFeesStartDate != null)
                {
                    DateTime dtAdmFeeStartDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(applicationConfiguration.AdmissionFeesStartDate), out dtAdmFeeStartDate);
                    if (ChkDt)
                    {
                        aaconfig.AdmissionFeesStartDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(applicationConfiguration.AdmissionFeesStartDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    }
                    else
                    {
                        aaconfig.AdmissionFeesStartDate = null;
                    }
                }
                else
                {
                    aaconfig.AdmissionFeesStartDate = null;
                }

                if (applicationConfiguration.AdmissionFeesStopDate != null)
                {
                    DateTime dtAdmFeeStopDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(applicationConfiguration.AdmissionFeesStopDate), out dtAdmFeeStopDate);
                    if (ChkDt)
                    {
                        aaconfig.AdmissionFeesStopDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(applicationConfiguration.AdmissionFeesStopDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    }
                    else
                    {
                        aaconfig.AdmissionFeesStopDate = null;
                    }
                }
                else
                {
                    aaconfig.AdmissionFeesStopDate = null;
                }

                if (applicationConfiguration.ApplicationApprovalStartDate != null)
                {
                    DateTime dtApprovalStartDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(applicationConfiguration.ApplicationApprovalStartDate), out dtApprovalStartDate);
                    if (ChkDt)
                    {
                        aaconfig.ApplicationApprovalStartDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(applicationConfiguration.ApplicationApprovalStartDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    }
                    else
                    {
                        aaconfig.ApplicationApprovalStartDate = null;
                    }
                }
                else
                {
                    aaconfig.ApplicationApprovalStartDate = null;
                }


                if (applicationConfiguration.ApplicationApprovalStopDate != null)
                {
                    DateTime dtApprovalStopDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(applicationConfiguration.ApplicationApprovalStopDate), out dtApprovalStopDate);
                    if (ChkDt)
                    {
                        aaconfig.ApplicationApprovalStopDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(applicationConfiguration.ApplicationApprovalStopDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    }
                    else
                    {
                        aaconfig.ApplicationApprovalStopDate = null;
                    }
                }
                else
                {
                    aaconfig.ApplicationApprovalStopDate = null;
                }

                if (applicationConfiguration.UpdatedDocLastDate != null)
                {
                    DateTime dtdocLastDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(applicationConfiguration.UpdatedDocLastDate), out dtdocLastDate);
                    if (ChkDt)
                    {
                        aaconfig.UpdatedDocLastDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(applicationConfiguration.UpdatedDocLastDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    }
                    else
                    {
                        aaconfig.UpdatedDocLastDate = null;
                    }
                }
                else
                {
                    aaconfig.UpdatedDocLastDate = null;
                }

                if (applicationConfiguration.AdmInstallementEndDate != null)
                {
                    DateTime dtAdmInstallementEndDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(applicationConfiguration.AdmInstallementEndDate), out dtAdmInstallementEndDate);
                    if (ChkDt)
                    {
                        aaconfig.AdmInstallementEndDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(applicationConfiguration.AdmInstallementEndDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    }
                    else
                    {
                        aaconfig.AdmInstallementEndDate = null;
                    }
                }
                else
                {
                    aaconfig.AdmInstallementEndDate = null;
                }

                cmd.Parameters.AddWithValue("@Id", aaconfig.Id);
                cmd.Parameters.AddWithValue("@ProgramInstancePartTermId", aaconfig.ProgramInstancePartTermId);
                cmd.Parameters.AddWithValue("@ApplicationStartDate", aaconfig.ApplicationStartDate);
                cmd.Parameters.AddWithValue("@ApplicationStopDate", aaconfig.ApplicationStopDate);
                cmd.Parameters.AddWithValue("@ApplicationFeeStartDate", aaconfig.ApplicationFeeStartDate);
                cmd.Parameters.AddWithValue("@ApplicationFeeEndDate", aaconfig.ApplicationFeeEndDate);
                cmd.Parameters.AddWithValue("@AdmissionFeesStartDate", aaconfig.AdmissionFeesStartDate);
                cmd.Parameters.AddWithValue("@AdmissionFeesStopDate", aaconfig.AdmissionFeesStopDate);
                cmd.Parameters.AddWithValue("@ApplicationApprovalStartDate", aaconfig.ApplicationApprovalStartDate);
                cmd.Parameters.AddWithValue("@ApplicationApprovalStopDate", aaconfig.ApplicationApprovalStopDate);
                cmd.Parameters.AddWithValue("@UpdatedDocLastDate", aaconfig.UpdatedDocLastDate);
                cmd.Parameters.AddWithValue("@AdmInstallementEndDate", aaconfig.AdmInstallementEndDate);
                cmd.Parameters.AddWithValue("@Remarks", aaconfig.Remarks);
                cmd.Parameters.AddWithValue("@ProspectusURL", aaconfig.ProspectusURL);
                cmd.Parameters.AddWithValue("@WrittenTestSyllabusURL", aaconfig.WrittenTestSyllabusURL);
                cmd.Parameters.AddWithValue("@WrittenTestScheduleURL", aaconfig.WrittenTestScheduleURL);
                cmd.Parameters.AddWithValue("@ModifiedBy", aaconfig.ModifiedBy);
                cmd.Parameters.AddWithValue("@ContactPersonNo", aaconfig.ContactPersonNo);
                cmd.Parameters.AddWithValue("@ContactPersonName", aaconfig.ContactPersonName);
                cmd.Parameters.AddWithValue("@ContactPersonEmail", aaconfig.ContactPersonEmail);

                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;


                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                //string ProgInstancePart = Convert.ToString(cmd.Parameters["@ProgInstancePart"].Value);
                con.Close();

                if (strMessage.Equals("TRUE"))
                {
                    strMessage = "Data has been Updated successfully";
                }

                //else if (strMessage.Equals("APP_START_DATE_NO_CHANGE"))
                //{
                //    strMessage = "APP_START_DATE_NO_CHANGE";
                //}

                return Return.returnHttp("200", strMessage, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }

        [HttpPost]
        public HttpResponseMessage deleteAdmApplicationConfiguration(AdmApplicationConfiguration applicationConfiguration)
        {
            try
            {
                //String token = Request.Headers.GetValues("token").FirstOrDefault();
                //TokenOperation ac = new TokenOperation();
                //string res = ac.ValidateToken(token);
                //if (res == "0")
                //{
                //    return Return.returnHttp("0", null, null);
                //}

                AdmApplicationConfiguration aaconfig = new AdmApplicationConfiguration();

                //dynamic JsonData = JsonObj;
                aaconfig.Id = applicationConfiguration.Id;

                SqlCommand cmd = new SqlCommand("AdmApplicationConfigurationDelete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", aaconfig.Id);

                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been deleted successfully.";
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
