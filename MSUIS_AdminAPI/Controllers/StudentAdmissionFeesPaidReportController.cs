using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using Newtonsoft.Json.Linq;
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
    public class StudentAdmissionFeesPaidReportController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();




        #region StudentAdmissionFeesNotPaidReport
        // This method is for searching full details of applicant according to ProgrammeInstancePartTermId  whose Application Fee Not Paid
        [HttpPost]
        public HttpResponseMessage StudentAdmissionFeesNotPaidReport(StudentAdmissionFeesNotPaidReport SAFNPR)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                List<StudentAdmissionFeesNotPaidReport> slist = new List<StudentAdmissionFeesNotPaidReport>();
                Int32 count = 0;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", SAFNPR.ProgrammeInstancePartTermId);
                cmd.CommandText = "StudentAdmissionFeesNotPaidReport";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow DR in dt.Rows)
                {
                    StudentAdmissionFeesNotPaidReport s = new StudentAdmissionFeesNotPaidReport();

                    s.ApplicationId = DR["ApplicationId"].ToString();
                    s.FullName = DR["FullName"].ToString();
                    s.EmailId = DR["EmailId"].ToString();
                    s.MobileNo = DR["MobileNo"].ToString();
                    s.InstancePartTermName = DR["InstancePartTermName"].ToString();
                    s.FacultyName = DR["FacultyName"].ToString();
                    s.InstituteName = DR["InstituteName"].ToString();
                    s.GroupName = DR["GroupName"].ToString();
                    s.FeeCategoryName = DR["FeeCategoryName"].ToString();
                    s.IsAdmissionFeePaid = DR["IsAdmissionFeePaid"].ToString();
                    s.IsInstalmentSelected = DR["IsInstalmentSelected"].ToString();
                    s.IsVerificationEmail = DR["IsVerificationEmail"].ToString();
                    s.IsVerificationSms = DR["IsVerificationSms"].ToString();
                    s.IsVerificationEmailOns = DR["IsVerificationEmailOn"].ToString();
                    s.IsVerificationSmsOns = DR["IsVerificationSmsOn"].ToString();
                    s.Gender = (Convert.ToString(DR["Gender"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["Gender"]);
                    s.IsPhysicallyChanllenged = (Convert.ToString(DR["IsPhysicallyChanllenged"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["IsPhysicallyChanllenged"]);
                    s.ApplicationReservationName = (Convert.ToString(DR["ApplicationReservationName"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["ApplicationReservationName"]);
                    s.SocialCategoryName = (Convert.ToString(DR["SocialCategoryName"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["SocialCategoryName"]);
                    s.CommitteeName = (Convert.ToString(DR["CommitteeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["CommitteeName"]);


                    count = count + 1;
                    s.IndexId = count;


                    slist.Add(s);
                }
                return Return.returnHttp("200", slist, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion



        #region StudentAdmissionFeesPaidReportGet
        [HttpPost]
        public HttpResponseMessage StudentAdmissionFeesPaidReportGet(StudentAdmissionFeesPaidReport SAFPR)
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

                try
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable();

                    List<StudentAdmissionFeesPaidReport> slist = new List<StudentAdmissionFeesPaidReport>();
                    Int32 count = 0;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", SAFPR.ProgrammeInstancePartTermId);
                    cmd.CommandText = "StudentAdmissionFeesPaidReport";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    foreach (DataRow DR in
                        dt.Rows)
                    {
                        StudentAdmissionFeesPaidReport s = new StudentAdmissionFeesPaidReport();

                        s.ApplicationId = DR["ApplicationId"].ToString();
                        //s.OrderId = DR["OrderId"].ToString();
                        s.FacultyName = DR["FacultyName"].ToString();
                        s.InstancePartTermName = DR["InstancePartTermName"].ToString();
                        s.FullName = DR["FullName"].ToString();
                        s.EmailId = DR["EmailId"].ToString();
                        s.MobileNo = DR["MobileNo"].ToString();
                        s.InstituteName = DR["InstituteName"].ToString();
                        s.GroupName = DR["GroupName"].ToString();
                        s.FeeCategoryName = DR["FeeCategoryName"].ToString();
                        s.IsAdmissionFeePaid = DR["IsAdmissionFeePaid"].ToString();
                        s.IsInstalmentSelected = (Convert.ToString(DR["IsInstalmentSelected"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["IsInstalmentSelected"]);
                        s.TotalAmount = (((DR["TotalAmount"]).ToString()).IsEmpty()) ? 0 : Convert.ToDecimal(DR["TotalAmount"]);
                        s.AmountPaid = (((DR["AmountPaid"]).ToString()).IsEmpty()) ? 0 : Convert.ToDecimal(DR["AmountPaid"]);
                        s.IsVerificationEmail = DR["IsVerificationEmail"].ToString();
                        s.IsVerificationSms = DR["IsVerificationSms"].ToString();
                        s.IsVerificationEmailOns = DR["IsVerificationEmailOn"].ToString();
                        s.IsVerificationSmsOns = DR["IsVerificationSmsOn"].ToString();
                        s.Gender = (Convert.ToString(DR["Gender"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["Gender"]);
                        s.IsPhysicallyChanllenged = (Convert.ToString(DR["IsPhysicallyChanllenged"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["IsPhysicallyChanllenged"]);
                        s.ApplicationReservationName = (Convert.ToString(DR["ApplicationReservationName"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["ApplicationReservationName"]);
                        s.SocialCategoryName = (Convert.ToString(DR["SocialCategoryName"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["SocialCategoryName"]);
                        s.CommitteeName = (Convert.ToString(DR["CommitteeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["CommitteeName"]);

                        count = count + 1;
                        s.IndexId = count;


                        slist.Add(s);
                    }
                    return Return.returnHttp("200", slist, null);
                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message.ToString(), null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion


        #region IncProgInsPartTermListGetByInstituteId
        // This method is for getting InstancePartTerm By Institute Id for Academic Purpose
        [HttpPost]
        public HttpResponseMessage IncProgInsPartTermListGetByInstituteId(ApplicationList ObjAppList)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                List<ApplicationList> slist = new List<ApplicationList>();

                cmd.Parameters.AddWithValue("@InstituteId", ObjAppList.InstituteId);
                cmd.CommandText = "IncProgInstPartTermGetByFacultyId";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow DR in dt.Rows)
                {
                    ApplicationList s = new ApplicationList();

                    s.InstancePartTermId = Convert.ToInt32(DR["Id"].ToString());
                    s.InstancePartTermName = DR["InstancePartTermName"].ToString();

                    slist.Add(s);
                }
                return Return.returnHttp("200", slist, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion


        #region IncProgInsPartTermListGetByInstituteId
        [HttpGet]
        public HttpResponseMessage ApplicationListGet()
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

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                List<ApplicationList> slist = new List<ApplicationList>();

                cmd.Parameters.AddWithValue("@FacultyId", facultyDepartIntituteId);
                cmd.CommandText = "ApplicationListGet";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow DR in dt.Rows)
                {
                    ApplicationList s = new ApplicationList();

                    s.InstancePartTermId = Convert.ToInt32(DR["Id"].ToString());
                    s.InstancePartTermName = DR["InstancePartTermName"].ToString();

                    slist.Add(s);
                }
                return Return.returnHttp("200", slist, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion
    }
}