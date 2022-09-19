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
    public class PrnGeneratedController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();

        #region PRNGeneratedGet
        [HttpPost]
        public HttpResponseMessage PRNGeneratedGet(PrnGenerated PGR)
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

                    List<PrnGenerated> plist = new List<PrnGenerated>();
                    Int32 count = 0;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", PGR.ProgrammeInstancePartTermId);
                    cmd.CommandText = "PRNGenReportForAcademicsandFacultyGet";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    foreach (DataRow DR in dt.Rows)
                    {
                        PrnGenerated p = new PrnGenerated();

                        p.AdmissionId = DR["AdmissionId"].ToString();
                        p.PRN = DR["PRN"].ToString();
                        p.FacultyName = DR["FacultyName"].ToString();
                        p.InstancePartTermName = DR["InstancePartTermName"].ToString();
                        p.NameAsPerMarksheet = DR["NameAsPerMarksheet"].ToString();
                        p.InstituteName = DR["InstituteName"].ToString();
                        p.FeeCategoryName = DR["FeeCategoryName"].ToString();
                        p.ProgrammeName = DR["ProgrammeName"].ToString();
                        p.BranchName = DR["BranchName"].ToString();
                        p.InstituteName = DR["InstituteName"].ToString();
                        p.PRNGenerated = DR["PRNGenerated"].ToString();
                        p.MobileNo = (Convert.ToString(DR["MobileNo"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["MobileNo"]);
                        p.EmailId = (Convert.ToString(DR["EmailId"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["EmailId"]);
                        p.Gender = (Convert.ToString(DR["Gender"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["Gender"]);
                        p.IsPhysicallyChanllenged = (Convert.ToString(DR["IsPhysicallyChanllenged"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["IsPhysicallyChanllenged"]);
                        p.ApplicationReservationName = (Convert.ToString(DR["ApplicationReservationName"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["ApplicationReservationName"]);
                        p.SocialCategoryName = (Convert.ToString(DR["SocialCategoryName"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["SocialCategoryName"]);
                        p.CommitteeName = (Convert.ToString(DR["CommitteeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["CommitteeName"]);
                        p.Nationality = (Convert.ToString(DR["Nationality"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["Nationality"]);
                        p.CurrentAddress = (Convert.ToString(DR["CurrentAddress"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["CurrentAddress"]);
                        p.PermanentAddress = (Convert.ToString(DR["PermanentAddress"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["PermanentAddress"]);
                        p.FatherMotherContactNo = (Convert.ToString(DR["FatherMotherContactNo"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["FatherMotherContactNo"]);
                        p.LocalToVadodara = (Convert.ToString(DR["LocalToVadodara"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["LocalToVadodara"]);
                        p.AdminRemarkByAcademics = (Convert.ToString(DR["AdminRemarkByAcademics"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["AdminRemarkByAcademics"]);
                        if (DR["EligibilityByAcademics"].ToString() == "Provisionally_Eligible")
                        {
                            p.EligibilityByAcademics = "Provisionally Eligible";
                        }
                        else if (DR["EligibilityByAcademics"].ToString() == "Eligible")
                        {
                            p.EligibilityByAcademics = "Eligible";
                        }
                        p.SpouseName = DR["SpouseName"].ToString();
                        p.NameOfMother = DR["NameOfMother"].ToString();
                        p.NameOfFather = DR["NameOfFather"].ToString();


                        p.DOB = DR["DOB"].ToString();
                        p.MaritalStatus = DR["MaritalStatus"].ToString();
                        p.MotherTongueName = DR["MotherTongueName"].ToString();
                        p.BloodGroupName = DR["BloodGroupName"].ToString();
                        p.ReligionName = DR["ReligionName"].ToString();
                        p.HeightInCms = DR["HeightInCms"].ToString();
                        p.WeightInKgs = DR["WeightInKgs"].ToString();
                        p.CitizenCountry = DR["CitizenshipCountryName"].ToString();
                        p.IsCurrentAsPermanent = DR["IsCurrentAsPermanent"].ToString();
                        if (p.IsCurrentAsPermanent == "True")
                        {
                            p.IsCurrentAsPermanent = "Yes";
                        }
                        else
                        {
                            p.IsCurrentAsPermanent = "No";
                        }
                        p.OptionalMobileNo = DR["OptionalMobileNo"].ToString();

                        p.GuardianContactNo = DR["GuardianContactNo"].ToString();
                        p.IsEWS = DR["IsEWS"].ToString();
                        if (p.IsEWS == "True")
                        {
                            p.IsEWS = "Yes";
                        }
                        else
                        {
                            p.IsEWS = "No";
                        }
                        p.IsEmp = DR["IsEmp"].ToString();
                        if (p.IsEmp == "True")
                        {
                            p.IsEmp = "Yes";
                        }
                        else
                        {
                            p.IsEmp = "No";
                        }
                        p.FatherOccupation = DR["FatherOccupation"].ToString();
                        p.MotherOccupation = DR["MotherOccupation"].ToString();
                        p.GuardianOccupation = DR["GuardianOccupation"].ToString();
                        p.FamilyAnnualIncome = DR["FamilyAnnualIncome"].ToString();
                        p.GuardianAnnualIncome = DR["GuardianAnnualIncome"].ToString();
                        p.IsGuardianEbc = DR["IsGuardianEbc"].ToString();
                        if (p.IsGuardianEbc == "True")
                        {
                            p.IsGuardianEbc = "Yes";
                        }
                        else
                        {
                            p.IsGuardianEbc = "No";
                        }
                        p.UserName = DR["UserName"].ToString();
                        p.OtherReligion = DR["OtherReligion"].ToString();
                        p.DOBDoc = DR["DOBDoc"].ToString();
                        p.PhotoIdDoc = DR["PhotoIdDoc"].ToString();
                        p.IsMajorThelesamiaStatus = DR["IsMajorThelesamiaStatus"].ToString();
                        if (p.IsMajorThelesamiaStatus == "True")
                        {
                            p.IsMajorThelesamiaStatus = "Yes";
                        }
                        else
                        {
                            p.IsMajorThelesamiaStatus = "No";
                        }
                        p.PassportNumber = DR["PassportNumber"].ToString();
                        p.PassportDate = DR["PassportDate"].ToString();
                        p.AadharNumber = DR["AadharNumber"].ToString();
                        p.AadharDoc = DR["AadharDoc"].ToString();
                        p.NameOnAadhar = DR["NameOnAadhar"].ToString();

                        p.IsEWSDocSubmitted = DR["IsEWSDocSubmitted"].ToString();
                        if (p.IsEWSDocSubmitted == "True")
                        {
                            p.IsEWSDocSubmitted = "Yes";
                        }
                        else
                        {
                            p.IsEWSDocSubmitted = "No";
                        }
                        p.IsPCDocSubmitted = DR["IsPCDocSubmitted"].ToString();
                        if (p.IsPCDocSubmitted == "True")
                        {
                            p.IsPCDocSubmitted = "Yes";
                        }
                        else
                        {
                            p.IsPCDocSubmitted = "No";
                        }
                        p.IsReservationDocSubmitted = DR["IsReservationDocSubmitted"].ToString();
                        if (p.IsReservationDocSubmitted == "True")
                        {
                            p.IsReservationDocSubmitted = "Yes";
                        }
                        else
                        {
                            p.IsReservationDocSubmitted = "No";
                        }
                        p.SocialCategoryDoc = DR["SocialCategoryDoc"].ToString();
                        p.ReservationCategoryDoc = DR["ReservationCategoryDoc"].ToString();

                        p.GuardianName = DR["GuardianName"].ToString();
                        p.CurrentEmployerName = DR["CurrentEmployerName"].ToString();
                        p.IsSmsPermissionGiven = DR["IsSmsPermissionGiven"].ToString();
                        if (p.IsSmsPermissionGiven == "True")
                        {
                            p.IsSmsPermissionGiven = "Yes";
                        }
                        else
                        {
                            p.IsSmsPermissionGiven = "No";
                        }

                        p.EWSDoc = DR["EWSDoc"].ToString();
                        p.PCDoc = DR["PCDoc"].ToString();
                        p.DisabilityPercentage = DR["DisabilityPercentage"].ToString();
                        p.DisabilityType = DR["DisabilityType"].ToString();
                        p.ApplicantPhoto = DR["ApplicantPhoto"].ToString();
                        p.ApplicantSignature = DR["ApplicantSignature"].ToString();
                        p.VerifiedStatus = DR["IsVerifiedByFaculty"].ToString();
                        if (p.VerifiedStatus == "True")
                        {
                            p.VerifiedStatus = "Verified";
                        }
                        else
                        {
                            p.VerifiedStatus = "Not Verified";
                        }
                        p.IsAdmitted = DR["IsAdmitted"].ToString();
                        if (p.IsAdmitted == "True")
                        {
                            p.IsAdmitted = "Yes";
                        }
                        else
                        {
                            p.IsAdmitted = "No";
                        }
                        p.AdmittedOn = DR["AdmittedOn"].ToString();
                        p.AdmittedInstituteName = DR["InstituteName"].ToString();
                        p.AdminRemarkByFaculty = DR["AdminRemarkByFaculty"].ToString();

                        p.EligibleDegreeName = DR["EligibleDegreeName"].ToString();
                        p.SpecializationName = DR["SpecializationName"].ToString();
                        p.ExaminationBodyName = DR["ExaminationBodyName"].ToString();
                        p.InstituteAttended = DR["InstituteAttended"].ToString();
                        p.SchoolNo = DR["SchoolNo"].ToString();
                        p.CityName = DR["CityName"].ToString();
                        p.OtherCity = DR["OtherCity"].ToString();
                        p.ExamPassCity = DR["ExamPassCity"].ToString();
                        p.ExamPassMonth = DR["ExamPassMonth"].ToString();
                        p.ExamPassYear = DR["ExamPassYear"].ToString();
                        p.ExamSeatNumber = DR["ExamSeatNumber"].ToString();
                        p.ExamCertificateNumber = DR["ExamCertificateNumber"].ToString();
                        p.MarkObtained = DR["MarkObtained"].ToString();
                        p.MarkOutof = DR["MarkOutof"].ToString();
                        p.Grade = DR["Grade"].ToString();
                        p.CGPA = DR["CGPA"].ToString();
                        p.PercentageEquivalenceCGPA = DR["PercentageEquivalenceCGPA"].ToString();
                        p.Percentage = DR["Percentage"].ToString();
                        p.ClassName = DR["ClassName"].ToString();
                        p.IsFirstTrial = DR["IsFirstTrial"].ToString();
                        if (p.IsFirstTrial == "True")
                        {
                            p.IsFirstTrial = "Yes";
                        }
                        else
                        {
                            p.IsFirstTrial = "No";
                        }
                        p.IsLastQualifyingExam = DR["IsLastQualifyingExam"].ToString();
                        if (p.IsLastQualifyingExam == "True")
                        {
                            p.IsLastQualifyingExam = "Yes";
                        }
                        else
                        {
                            p.IsLastQualifyingExam = "No";
                        }
                        p.LanguageName = DR["LanguageName"].ToString();
                        p.ResultStatus = DR["ResultStatus"].ToString();
                        p.AttachDocument = DR["AttachDocument"].ToString();
                        p.IsDeclared = DR["IsDeclared"].ToString();
                        if (p.IsDeclared == "True")
                        {
                            p.IsDeclared = "Yes";
                        }
                        else
                        {
                            p.IsDeclared = "No";
                        }
                        p.IsCancelledByStudent = DR["IsCancelledByStudent"].ToString();
                        if (p.IsCancelledByStudent == "True")
                        {
                            p.IsCancelledByStudent = "Yes";
                        }
                        else
                        {
                            p.IsCancelledByStudent = "No";
                        }

                        count = count + 1;
                        p.IndexId = count;


                        plist.Add(p);
                    }
                    return Return.returnHttp("200", plist, null);
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


        #region ApplicationListGet
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