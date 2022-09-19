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

namespace MSUISApi.Controllers
{
    public class ApplicationStatisticsController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

        #region ApplicationStatisticsGetByFacultyId
        [HttpPost]
        public HttpResponseMessage ApplicationStatisticsGetByFacultyId(ApplicationStatistics ObjAppStat)
        {
            ApplicationStatistics modelobj = new ApplicationStatistics();

            try
            {
               
                modelobj.InstituteId = ObjAppStat.InstituteId;
                modelobj.AcademicYearId = ObjAppStat.AcademicYearId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("ApplicationStatisticsGetByFacultyId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InstituteId", modelobj.InstituteId);
                cmd.Parameters.AddWithValue("@AcademicYearId", modelobj.AcademicYearId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<ApplicationStatistics> ObjLstAS = new List<ApplicationStatistics>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ApplicationStatistics ObjAS = new ApplicationStatistics();

                        //ObjAS.ProgrammeInstancePartTermId = Convert.ToInt32(dr["ProgrammeInstancePartTermId"]);
                       
                        var ProgrammeInstancePartTermId = dr["ProgrammeInstancePartTermId"];
                        if (ProgrammeInstancePartTermId is DBNull)
                        {
                            ProgrammeInstancePartTermId = 0;
                            ObjAS.ProgrammeInstancePartTermId = Convert.ToInt32(ProgrammeInstancePartTermId);
                
                        }
                        else
                        {
                            ObjAS.ProgrammeInstancePartTermId = Convert.ToInt32(dr["ProgrammeInstancePartTermId"]);
                            
                        }
                        ObjAS.InstancePartTermName = (dr["InstancePartTermName"].ToString());
                        ObjAS.FacultyName = (dr["FacultyName"].ToString());
                        var Intake = dr["Intake"];
                        if (Intake is DBNull) { }
                        else { ObjAS.Intake = Convert.ToInt32(dr["Intake"]); }                      
                        ObjAS.TotalApplication = Convert.ToInt32(dr["TotalApplication"]);
                        ObjAS.ApplicationFeeNotPaid = Convert.ToInt32(dr["ApplicationFeeNotPaid"]);
                        ObjAS.PreVerificationVerifried = Convert.ToInt32(dr["PreVerificationVerifried"]);
                        ObjAS.PreVerificationPending = Convert.ToInt32(dr["PreVerificationPending"]);
                        ObjAS.PreVerificationNotApproved = Convert.ToInt32(dr["PreVerificationNotApproved"]);
                        ObjAS.PreVerificationPendingVerified = Convert.ToInt32(dr["PreVerificationPendingVerified"]);
                        ObjAS.ApprovedByFaculty = Convert.ToInt32(dr["ApprovedByFaculty"]);
                        ObjAS.NotApprovedByFaculty = Convert.ToInt32(dr["NotApprovedByFaculty"]);
                        ObjAS.ApprovedByAcademic = Convert.ToInt32(dr["ApprovedByAcademic"]);
                        ObjAS.NotApprovedByAcademic = Convert.ToInt32(dr["NotApprovedByAcademic"]);
                        ObjAS.PendingApprovedByAcademic = Convert.ToInt32(dr["PendingApprovedByAcademic"]);
                        ObjAS.AdmissionFeePaid = Convert.ToInt32(dr["AdmissionFeePaid"]);
                        ObjAS.AdmissionFeeNotPaid = Convert.ToInt32(dr["AdmissionFeeNotPaid"]);
                        ObjAS.AdmittedStudent = Convert.ToInt32(dr["AdmittedStudent"]);
                        ObjAS.PrnGenerated = Convert.ToInt32(dr["PrnGenerated"]);
                        ObjAS.PRNNotGenerated = Convert.ToInt32(dr["PRNNotGenerated"]);
                        ObjAS.PaperSelected = Convert.ToInt32(dr["PaperSelected"]);
                        ObjAS.CancelledByAcademics = Convert.ToInt32(dr["CancelledByAcademics"]);
                        ObjAS.CancelledByFaculty = Convert.ToInt32(dr["CancelledByFaculty"]);
                        ObjAS.CancelledByStudent = Convert.ToInt32(dr["CancelledByStudent"]);
                        ObjAS.RefundRequestByStudent = Convert.ToInt32(dr["RefundRequestByStudent"]);
                        ObjAS.RefundRequestByFaculty = Convert.ToInt32(dr["RefundRequestByFaculty"]);
                        ObjAS.RefundRequestByAcademics = Convert.ToInt32(dr["RefundRequestByAcademics"]);
                        ObjAS.RefundRequestByAudit = Convert.ToInt32(dr["RefundRequestByAudit"]);
                        ObjAS.RefundRequestByAccount = Convert.ToInt32(dr["RefundRequestByAccount"]);
                        count = count + 1;
                        ObjAS.IndexId = count;
                        ObjLstAS.Add(ObjAS);
                    }

                }
                return Return.returnHttp("200", ObjLstAS, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region ProgrammeConfigurationStatisticsByInstituteIdAndAcademicYear
        [HttpPost]
        public HttpResponseMessage ProgeConfigStatsByInstIdAndAcdYear(ApplicationStatistics ObjAppStat)
        {
            ApplicationStatistics modelobj = new ApplicationStatistics();

            try
            {
                Request.Headers.TryGetValues("DepartmentId", out IEnumerable<string> values);
                var DepartmentId = values?.FirstOrDefault();
                modelobj.InstituteId = ObjAppStat.InstituteId;
                modelobj.AcademicYearId = ObjAppStat.AcademicYearId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("ProgrammeConfigurationStatistics", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (DepartmentId != null || DepartmentId != "" || DepartmentId != "0")
                {
                    cmd.Parameters.AddWithValue("@InstituteId", modelobj.InstituteId);
                    cmd.Parameters.AddWithValue("@AcademicYearId", modelobj.AcademicYearId);
                    cmd.Parameters.AddWithValue("@DepartmentId", DepartmentId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@InstituteId", modelobj.InstituteId);
                    cmd.Parameters.AddWithValue("@AcademicYearId", modelobj.AcademicYearId);
                }

                
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<ApplicationStatistics> ObjLstPS = new List<ApplicationStatistics>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ApplicationStatistics ObjPS = new ApplicationStatistics();

                        //ObjPS.ProgrammeName = (dr["ProgrammeName"].ToString());
                       // ObjPS.BranchName = (dr["BranchName"].ToString());
                        ObjPS.InstancePartTermName = (dr["InstancePartTermName"].ToString());
                        ObjPS.EligibilityGroupCount = Convert.ToInt32(dr["EligibilityGroupCount"]);
                        if (ObjPS.EligibilityGroupCount > 0) { ObjPS.EligibilityGroup = "Yes";}
                        else { ObjPS.EligibilityGroup = "No"; }
                        ObjPS.EligibilityGroupComponentCount = Convert.ToInt32(dr["EligibilityGroupComponentCount"]);
                        if (ObjPS.EligibilityGroupComponentCount > 0) { ObjPS.EligibilityGroupComponent = "Yes"; }
                        else { ObjPS.EligibilityGroupComponent = "No"; }
                        ObjPS.ProgrammeAddOnCount = Convert.ToInt32(dr["ProgrammeAddOnCount"]);
                        if (ObjPS.ProgrammeAddOnCount > 0) { ObjPS.ProgrammeAddOn = "Yes"; }
                        else { ObjPS.ProgrammeAddOn = "No"; }
                        ObjPS.RequiredDocumentCount = Convert.ToInt32(dr["RequiredDocumentCount"]);
                        if (ObjPS.RequiredDocumentCount > 0) { ObjPS.RequiredDocument = "Yes"; }
                        else { ObjPS.RequiredDocument = "No"; }
                        ObjPS.ApplicationConfigurationCount = Convert.ToInt32(dr["ApplicationConfigurationCount"]);
                        if (ObjPS.ApplicationConfigurationCount > 0) { ObjPS.ApplicationConfiguration = "Yes"; }
                        else { ObjPS.ApplicationConfiguration = "No"; }
                        ObjPS.ApplicationFeeConfigured = Convert.ToInt32(dr["ApplicationFeeConfigured"]);
                        if (ObjPS.ApplicationFeeConfigured > 0) { ObjPS.ApplicationFeeConfig = "Yes"; }
                        else { ObjPS.ApplicationFeeConfig = "No"; }
                        ObjPS.ApplicationFeePublish = Convert.ToInt32(dr["ApplicationFeePublish"]);
                        if (ObjPS.ApplicationFeePublish > 0) { ObjPS.AppFeePublish = "Yes"; }
                        else { ObjPS.AppFeePublish = "No"; }
                        ObjPS.AdmissionFeeConfigured = Convert.ToInt32(dr["AdmissionFeeConfigured"]);
                        if (ObjPS.AdmissionFeeConfigured > 0) { ObjPS.AdmFeeConfig = "Yes"; }
                        else { ObjPS.AdmFeeConfig = "No"; }
                        ObjPS.AdmissionFeePublish = Convert.ToInt32(dr["AdmissionFeePublish"]);
                        if (ObjPS.AdmissionFeePublish > 0) { ObjPS.AdmFeePublish = "Yes"; }
                        else { ObjPS.AdmFeePublish = "No"; }
                        count = count + 1;
                        ObjPS.IndexId = count;
                        ObjLstPS.Add(ObjPS);
                    }

                }
                return Return.returnHttp("200", ObjLstPS, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

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
                SqlCommand cmd = new SqlCommand("MstInstituteGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<ApplicationStatistics> ObjLstInst = new List<ApplicationStatistics>();
                dt.Rows.Add("0", "All Institute");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
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

        #region MstInstituteGetInstituteNameById
        [HttpPost]
        public HttpResponseMessage MstInstituteGetInstNameById(ApplicationStatistics ObjAppStat)
        {
            ApplicationStatistics modelobj = new ApplicationStatistics();
            try
            {
                modelobj.InstituteId = ObjAppStat.InstituteId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                SqlCommand cmd = new SqlCommand("MstInstituteGetInstituteNameById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InstituteId", modelobj.InstituteId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<ApplicationStatistics> ObjLstInst = new List<ApplicationStatistics>();
             
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
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

        #region IncAcademicYearCodeGetById
        [HttpPost]
        public HttpResponseMessage IncAcadYearCodeGetById(ApplicationStatistics ObjAppStat)
        {
            ApplicationStatistics modelobj = new ApplicationStatistics();
            try
            {
                modelobj.AcademicYearId = ObjAppStat.AcademicYearId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                SqlCommand cmd = new SqlCommand("IncAcademicYearCodeGetById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", modelobj.AcademicYearId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<ApplicationStatistics> ObjLstInst = new List<ApplicationStatistics>();
              
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ApplicationStatistics ObjInst = new ApplicationStatistics();
                        ObjInst.Id = Convert.ToInt32(dr["Id"]);
                        ObjInst.AcademicYearCode = (dr["AcademicYearCode"].ToString());
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
    }
}
