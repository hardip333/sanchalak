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

namespace MSUISApi.Controllers
{
    public class ApplicationConfigurationByAcademicController : ApiController 
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region MstInstituteGet
        [HttpPost]
        public HttpResponseMessage MstInstituteGet()
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
                SqlCommand cmd = new SqlCommand("MstInstituteGet", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<ApplicationStatistics> ObjLstInst = new List<ApplicationStatistics>();
                //Dt.Rows.Add("0", "All Institute");
                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        ApplicationStatistics ObjInst = new ApplicationStatistics();
                        ObjInst.Id = Convert.ToInt32(dr["Id"]);
                        ObjInst.InstituteName = (dr["InstituteName"].ToString());

                        ObjLstInst.Add(ObjInst);
                    }

                }

                return Return.returnHttp("200", ObjLstInst, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region Programme Get By InstituteId
        [HttpPost]
        public HttpResponseMessage MstProgrammeGetByInstId(MstProgramme ObjProg)
        {
            try
            {
                Request.Headers.TryGetValues("DepartmentId", out IEnumerable<string> values);
                var DepartmentId = values?.FirstOrDefault();
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();

                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                List<MstProgramme> Programmedata = new List<MstProgramme>();

                SqlCommand cmd = new SqlCommand("GroupAndPaperReportGetProgrammeByInstituteId", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (DepartmentId != null || DepartmentId != "" || DepartmentId != "0")
                {
                    cmd.Parameters.AddWithValue("@InstituteId", ObjProg.InstituteId);
                    cmd.Parameters.AddWithValue("@DepartmentId", DepartmentId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@InstituteId", ObjProg.InstituteId);
                }
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgramme Programme = new MstProgramme();
                        Programme.Id = (((Dt.Rows[i]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["Id"]);
                        Programme.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);

                        Programmedata.Add(Programme);
                    }
                }
                if (Programmedata != null)
                    return Return.returnHttp("200", Programmedata, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);

            }
            catch (Exception e)
            {

                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region ProgrammePartListGetByProgrammeId
        [HttpPost]
        public HttpResponseMessage ProgrammePartListGetByProgrammeId(ProgrammeInstancePart ObjProg)
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

                List<ProgrammeInstancePart> Programmedata = new List<ProgrammeInstancePart>();

                SqlCommand cmd = new SqlCommand("GroupAndPaperReportGetProgrammePartByProgACADId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeId", ObjProg.ProgrammeId);
                cmd.Parameters.AddWithValue("@AcademicYearId", ObjProg.AcademicYearId);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePart Programme = new ProgrammeInstancePart();
                        Programme.Id = (((Dt.Rows[i]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["Id"]);
                        Programme.PartShortName = (Convert.ToString(Dt.Rows[i]["PartShortName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["PartShortName"]);

                        Programmedata.Add(Programme);
                    }
                }
                if (Programmedata != null)
                    return Return.returnHttp("200", Programmedata, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);

            }
            catch (Exception e)
            {

                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region BranchList Get By ProgrammePartId
        [HttpPost]
        public HttpResponseMessage MstProgrammeBranchListGetByProgrammePartId(ProgrammeInstancePart ObjProg)
        {
            try
            {
                Request.Headers.TryGetValues("DepartmentId", out IEnumerable<string> values);
                var DepartmentId = values?.FirstOrDefault();
                Request.Headers.TryGetValues("FacultyId", out IEnumerable<string> values1);
                var FacultyId = values1?.FirstOrDefault();
                Request.Headers.TryGetValues("InstituteId", out IEnumerable<string> values2);
                var InstituteId = values2?.FirstOrDefault();


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

                int ProgrammeId = Convert.ToInt32(ObjProg.ProgrammeId);
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter cmdda = new SqlDataAdapter("MstProgrammeBranchMapGetByProgrammeId", Con);

                cmdda.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (DepartmentId != null || DepartmentId != "" || DepartmentId != "0")
                {
                    cmdda.SelectCommand.Parameters.AddWithValue("@ProgrammeId", ProgrammeId);
                    cmdda.SelectCommand.Parameters.AddWithValue("@FacultyId", FacultyId);
                    cmdda.SelectCommand.Parameters.AddWithValue("@InstituteId", InstituteId);
                    cmdda.SelectCommand.Parameters.AddWithValue("@DepartmentId", DepartmentId);
                }
                else
                {
                    cmdda.SelectCommand.Parameters.AddWithValue("@ProgrammeId", ProgrammeId);
                }



                DataTable Dt = new DataTable();
                cmdda.Fill(Dt);


                List<MstSpecialisation> ObjLstMstSpecialisation = new List<MstSpecialisation>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstSpecialisation ObjMstSpecialisation = new MstSpecialisation();

                        ObjMstSpecialisation.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        ObjMstSpecialisation.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        ObjMstSpecialisation.ProgrammeCode = (Convert.ToString(Dt.Rows[i]["ProgrammeCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeCode"]);
                        ObjMstSpecialisation.BranchName = ((Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "Not Defined" : (Convert.ToString(Dt.Rows[i]["BranchName"])));
                        ObjMstSpecialisation.FacultyId = (((Dt.Rows[i]["FacultyId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        ObjMstSpecialisation.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        ObjMstSpecialisation.ProgrammeId = (Convert.ToString(Dt.Rows[i]["ProgrammeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);
                        ObjMstSpecialisation.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        ObjMstSpecialisation.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjMstSpecialisation.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);

                        ObjLstMstSpecialisation.Add(ObjMstSpecialisation);
                    }
                    return Return.returnHttp("200", ObjLstMstSpecialisation, null);
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
        #endregion

        #region ProgrammePartTermListGetByProgrammePartId
        [HttpPost]
        public HttpResponseMessage ProgrammePartTermListGetByProgrammePartId(ProgrammeInstancePart ObjProg)
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
                
                List<PartTerm> Programmedata = new List<PartTerm>();

                SqlCommand cmd = new SqlCommand("AppConfigByAcademicGetProgrammePartTermByProgpartId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeInstancePartId", ObjProg.ProgrammeInstancePartId);
                cmd.Parameters.AddWithValue("@SpecialisationId", ObjProg.SpecialisationId);
                cmd.Parameters.AddWithValue("@InstituteId", ObjProg.InstituteId);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {

                        PartTerm PT = new PartTerm();
                        PT.Id = (((Dt.Rows[i]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["Id"]);
                        PT.InstancePartTermName = (Convert.ToString(Dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                        PT.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        PT.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        PT.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["BranchName"]);
                        PT.AcademicYearCode = (Convert.ToString(Dt.Rows[i]["AcademicYearCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);

                        Programmedata.Add(PT);
                    }
                    return Return.returnHttp("200", Programmedata, null);
                }


                else
                {
                    return Return.returnHttp("201", "No Record Found", null);
                }


            }
            catch (Exception e)
            {

                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region AdmApplicationConfigurationGetById
        [HttpPost]
        public HttpResponseMessage AdmApplicationConfigurationGetById(AdmApplicationConfiguration ObjAppConfig)
        {
            AdmApplicationConfiguration modelobj = new AdmApplicationConfiguration();

            try
            {
                modelobj.ProgrammeInstancePartTermId = ObjAppConfig.ProgrammeInstancePartTermId;
                SqlCommand cmd = new SqlCommand("AdmApplicationConfigurationGetforAcademic", Con);
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
                        ObjLstAAC.AdmInstallementEndDateView = string.Format("{0: dd-MM-yyyy}", dr["AdmInstallementEndDate"]);
                        ObjLstAAC.UpdatedDocLastDateView = string.Format("{0: dd-MM-yyyy}", dr["UpdatedDocLastDate"]);
                        ObjLstAAC.Remarks = Convert.ToString(dr["Remarks"].ToString());
                        ObjLstAAC.ProgramInstancePartTermId = Convert.ToInt32(dr["ProgramInstancePartTermId"].ToString());
                        ObjLstAAC.visibility = Convert.ToString(dr["visibility"].ToString());
                        ObjLstAAC.ProspectusURL = Convert.ToString(dr["ProspectusURL"].ToString());
                        ObjLstAAC.WrittenTestSyllabusURL = Convert.ToString(dr["WrittenTestSyllabusURL"].ToString());
                        ObjLstAAC.ContactPersonEmail = Convert.ToString(dr["ContactPersonEmail"].ToString());
                        ObjLstAAC.ContactPersonName = Convert.ToString(dr["ContactPersonName"].ToString());
                        ObjLstAAC.ContactPersonNo = Convert.ToString(dr["ContactPersonNo"].ToString());
                        ObjLstAAC.TotalAmount = (((dr["TotalAmount"]).ToString()).IsEmpty()) ? 0 : Convert.ToDouble(dr["TotalAmount"].ToString());
                        ObjLstAAC.FeeCategoryName = (((dr["FeeCategoryName"]).ToString()).IsEmpty()) ? "Not Defined" : Convert.ToString(dr["FeeCategoryName"].ToString());
                        ObjLstAAC.FeeSts = (((dr["FeeSts"]).ToString()).IsEmpty()) ? false : Convert.ToBoolean(dr["FeeSts"]);
                        ObjLstAAC.IsPaperSelByStudent = (((dr["IsPaperSelByStudent"]).ToString()).IsEmpty()) ? false : Convert.ToBoolean(dr["IsPaperSelByStudent"]);
                        ObjLstAAC.IsPaperSelBeforeFees = (((dr["IsPaperSelBeforeFees"]).ToString()).IsEmpty()) ? false : Convert.ToBoolean(dr["IsPaperSelBeforeFees"]);
                        ObjLstAAC.SequenceCount = (((dr["SequenceCount"]).ToString()).IsEmpty()) ? false : Convert.ToBoolean(dr["SequenceCount"]);

                        ObjLstAACList.Add(ObjLstAAC);
                    }
                    return Return.returnHttp("200", ObjLstAACList, null);
                }
                else
                {
                    return Return.returnHttp("200", "No Record Found", null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region updateAdmApplicationConfiguration
        [HttpPost]
        public HttpResponseMessage updateAdmApplicationConfiguration(AdmApplicationConfiguration applicationConfiguration)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("AdmApplicationConfigurationEditByAcademic", Con);
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
                //aaconfig.IsPaperSelByStudent = Convert.ToBoolean(applicationConfiguration.IsPaperSelByStudent);
                //aaconfig.IsPaperSelBeforeFees = Convert.ToBoolean(applicationConfiguration.IsPaperSelBeforeFees);

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
                cmd.Parameters.AddWithValue("@AdmInstallementEndDate", aaconfig.AdmInstallementEndDate);
                //cmd.Parameters.AddWithValue("@IsPaperSelByStudent", aaconfig.IsPaperSelByStudent);
                //cmd.Parameters.AddWithValue("@IsPaperSelBeforeFees", aaconfig.IsPaperSelBeforeFees);
                cmd.Parameters.AddWithValue("@ModifiedBy", aaconfig.ModifiedBy);

                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;


                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                //string ProgInstancePart = Convert.ToString(cmd.Parameters["@ProgInstancePart"].Value);
                Con.Close();

                if (strMessage.Equals("TRUE"))
                {
                    strMessage = "Data has been Updated successfully";
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
