using Microsoft.Ajax.Utilities;
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
    public class ApplicationListController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlCommand cmd = new SqlCommand();

        // This method is for getting InstancePartTerm
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
        // This method is for searching short details of applicant according to ProgrammeInstancePartTermId
        [HttpPost]
        public HttpResponseMessage ApplicationListSearch(ApplicationListShort applicationList)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                List<ApplicationListShort> slist = new List<ApplicationListShort>();

                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", applicationList.InstancePartTermId);
                cmd.CommandText = "ApplicationListSearch";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                da.SelectCommand = cmd;
                da.Fill(dt);

                var valueDistinctByIdColumn = dt.AsEnumerable().DistinctBy(row => new { Id = row["Id"] });
                DataTable dt2 = valueDistinctByIdColumn.CopyToDataTable();
                Int32 count = 0;
                foreach (DataRow DR in dt2.Rows)
                {
                    ApplicationListShort s = new ApplicationListShort();

                    s.FirstName = DR["FirstName"].ToString();
                    s.LastName = DR["LastName"].ToString();
                    s.EmailId = DR["EmailId"].ToString();
                    s.MobileNo = DR["MobileNo"].ToString();
                    s.IsEWS = DR["IsEWS"].ToString();
                    if (s.IsEWS == "True")
                    {
                        s.IsEWS = "Yes";
                    }
                    else
                    {
                        s.IsEWS = "No";
                    }
                    s.IsPhysicallyChallenged = DR["IsPhysicallyChallenged"].ToString();
                    if (s.IsPhysicallyChallenged == "True")
                    {
                        s.IsPhysicallyChallenged = "Yes";
                    }
                    else
                    {
                        s.IsPhysicallyChallenged = "No";
                    }
                    s.SocialCategoryName = DR["SocialCategoryName"].ToString();
                    s.ReservationCategory = DR["ApplicationReservationName"].ToString();
                    s.IsLocalToVadodara = DR["IsLocalToVadodara"].ToString();
                    if (s.IsLocalToVadodara == "True")
                    {
                        s.IsLocalToVadodara = "Yes";
                    }
                    else
                    {
                        s.IsLocalToVadodara = "No";
                    }
                    s.DisabilityPercentage = DR["DisabilityPercentage"].ToString();
                    s.DisabilityType = DR["DisabilityType"].ToString();
                    s.InstancePartTermName = DR["InstancePartTermName"].ToString();
                    s.AdmApplicationIdStr = DR["Id"].ToString();
                    s.VerifiedStatus = DR["IsVerifiedByFaculty"].ToString();
                    if (s.VerifiedStatus == "True")
                    {
                        s.VerifiedStatus = "Verified";
                    }
                    else
                    {
                        s.VerifiedStatus = "Not Verified";
                    }
                    s.IsAdmitted = DR["IsAdmitted"].ToString();
                    if (s.IsAdmitted == "True")
                    {
                        s.IsAdmitted = "Yes";
                    }
                    else
                    {
                        s.IsAdmitted = "No";
                    }
                    s.AdmittedOn = DR["AdmittedOn"].ToString();
                    s.AdmittedInstituteName = DR["InstituteName"].ToString();
                    s.AdminRemarkByFaculty = DR["AdminRemarkByFaculty"].ToString();
                    s.AdminRemarkByAcademics = DR["AdminRemarkByAcademics"].ToString();
                    s.IsCancelledByStudent = DR["IsCancelledByStudent"].ToString();
                    if (s.IsCancelledByStudent == "True")
                    {
                        s.IsCancelledByStudent = "Yes";
                    }
                    else
                    {
                        s.IsCancelledByStudent = "No";
                    }

                    count = count + 1;
                    s.IndexId = count;
                    s.Name = s.FirstName + " " + s.LastName;

                    slist.Add(s);
                }
                return Return.returnHttp("200", slist, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        // This method is for searching full details of applicant according to ProgrammeInstancePartTermId
        [HttpPost]
        public HttpResponseMessage ApplicationListSearchFull(ApplicationList applicationList)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                List<ApplicationList> slist = new List<ApplicationList>();

                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", applicationList.InstancePartTermId);
                cmd.CommandText = "ApplicationListSearch";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                da.SelectCommand = cmd;
                da.Fill(dt);
                int count = 0;

                foreach (DataRow DR in dt.Rows)
                {
                    ApplicationList s = new ApplicationList();

                    s.FirstName = DR["FirstName"].ToString();
                    s.LastName = DR["LastName"].ToString();
                    s.EmailId = DR["EmailId"].ToString();
                    s.MobileNo = DR["MobileNo"].ToString();
                    s.MiddleName = DR["MiddleName"].ToString();
                    s.SpouseName = DR["SpouseName"].ToString();
                    s.NameOfMother = DR["NameOfMother"].ToString();
                    s.NameOfFather = DR["NameOfFather"].ToString();
                    s.NameAsPerMarksheet = DR["NameAsPerMarksheet"].ToString();
                    s.Gender = DR["Gender"].ToString();
                    s.DOB = DR["DOB"].ToString();
                    s.AdmApplicantRegistrationId = DR["AdmApplicantRegistrationId"].ToString();
                    s.MaritalStatus = DR["MaritalStatus"].ToString();
                    s.MotherTongueName = DR["MotherTongueName"].ToString();
                    s.BloodGroupName = DR["BloodGroupName"].ToString();
                    s.ReligionName = DR["ReligionName"].ToString();
                    s.IsNRI = DR["IsNRI"].ToString();
                    if (s.IsNRI == "True")
                    {
                        s.IsNRI = "Yes";
                    }
                    else
                    {
                        s.IsNRI = "No";
                    }
                    s.HeightInCms = DR["HeightInCms"].ToString();
                    s.WeightInKgs = DR["WeightInKgs"].ToString();
                    s.CitizenCountry = DR["CitizenshipCountryName"].ToString();
                    s.PermanentAddress = DR["PermanentAddress"].ToString();
                    s.PermanentCountry = DR["PermanentCountryName"].ToString();
                    s.PermanentState = DR["PermanentStateName"].ToString();
                    s.PermanentDistrict = DR["PermanentDistrictName"].ToString();
                    s.PermanentCityVillage = DR["PermanentCityVillage"].ToString();
                    s.PermanentPincode = DR["PermanentPincode"].ToString();
                    s.IsCurrentAsPermanent = DR["IsCurrentAsPermanent"].ToString();
                    if (s.IsCurrentAsPermanent == "True")
                    {
                        s.IsCurrentAsPermanent = "Yes";
                    }
                    else
                    {
                        s.IsCurrentAsPermanent = "No";
                    }
                    s.CurrentAddress = DR["CurrentAddress"].ToString();
                    s.CurrentCountry = DR["CurrentCountryName"].ToString();
                    s.CurrentState = DR["CurrentStateName"].ToString();
                    s.CurrentDistrict = DR["CurrentDistrictName"].ToString();
                    s.CurrentCityVillage = DR["CurrentCityVillage"].ToString();
                    s.CurrentPincode = DR["CurrentPincode"].ToString();
                    s.OptionalMobileNo = DR["OptionalMobileNo"].ToString();
                    s.FatherMotherContactNo = DR["FatherMotherContactNo"].ToString();
                    s.GuardianContactNo = DR["GuardianContactNo"].ToString();
                    s.IsEWS = DR["IsEWS"].ToString();
                    if (s.IsEWS == "True")
                    {
                        s.IsEWS = "Yes";
                    }
                    else
                    {
                        s.IsEWS = "No";
                    }
                    s.IsPhysicallyChallenged = DR["IsPhysicallyChallenged"].ToString();
                    if (s.IsPhysicallyChallenged == "True")
                    {
                        s.IsPhysicallyChallenged = "Yes";
                    }
                    else
                    {
                        s.IsPhysicallyChallenged = "No";
                    }
                    s.IsEmp = DR["IsEmp"].ToString();
                    if (s.IsEmp == "True")
                    {
                        s.IsEmp = "Yes";
                    }
                    else
                    {
                        s.IsEmp = "No";
                    }
                    s.FatherOccupation = DR["FatherOccupation"].ToString();
                    s.MotherOccupation = DR["MotherOccupation"].ToString();
                    s.GuardianOccupation = DR["GuardianOccupation"].ToString();
                    s.FamilyAnnualIncome = DR["FamilyAnnualIncome"].ToString();
                    s.GuardianAnnualIncome = DR["GuardianAnnualIncome"].ToString();
                    s.IsGuardianEbc = DR["IsGuardianEbc"].ToString();
                    if (s.IsGuardianEbc == "True")
                    {
                        s.IsGuardianEbc = "Yes";
                    }
                    else
                    {
                        s.IsGuardianEbc = "No";
                    }
                    s.UserName = DR["UserName"].ToString();
                    s.OtherReligion = DR["OtherReligion"].ToString();
                    s.DOBDoc = DR["DOBDoc"].ToString();
                    s.PhotoIdDoc = DR["PhotoIdDoc"].ToString();
                    s.IsMajorThelesamiaStatus = DR["IsMajorThelesamiaStatus"].ToString();
                    if (s.IsMajorThelesamiaStatus == "True")
                    {
                        s.IsMajorThelesamiaStatus = "Yes";
                    }
                    else
                    {
                        s.IsMajorThelesamiaStatus = "No";
                    }
                    s.PassportNumber = DR["PassportNumber"].ToString();
                    s.PassportDate = DR["PassportDate"].ToString();
                    s.AadharNumber = DR["AadharNumber"].ToString();
                    s.AadharDoc = DR["AadharDoc"].ToString();
                    s.NameOnAadhar = DR["NameOnAadhar"].ToString();
                    s.SocialCategoryName = DR["SocialCategoryName"].ToString();
                    s.GSocialCategory = DR["GSocialCategoryName"].ToString();
                    s.IsSocialDocSubmitted = DR["IsSocialDocSubmitted"].ToString();
                    if (s.IsSocialDocSubmitted == "True")
                    {
                        s.IsSocialDocSubmitted = "Yes";
                    }
                    else
                    {
                        s.IsSocialDocSubmitted = "No";
                    }
                    s.IsEWSDocSubmitted = DR["IsEWSDocSubmitted"].ToString();
                    if (s.IsEWSDocSubmitted == "True")
                    {
                        s.IsEWSDocSubmitted = "Yes";
                    }
                    else
                    {
                        s.IsEWSDocSubmitted = "No";
                    }
                    s.IsPCDocSubmitted = DR["IsPCDocSubmitted"].ToString();
                    if (s.IsPCDocSubmitted == "True")
                    {
                        s.IsPCDocSubmitted = "Yes";
                    }
                    else
                    {
                        s.IsPCDocSubmitted = "No";
                    }
                    s.IsReservationDocSubmitted = DR["IsReservationDocSubmitted"].ToString();
                    if (s.IsReservationDocSubmitted == "True")
                    {
                        s.IsReservationDocSubmitted = "Yes";
                    }
                    else
                    {
                        s.IsReservationDocSubmitted = "No";
                    }
                    s.SocialCategoryDoc = DR["SocialCategoryDoc"].ToString();
                    s.ReservationCategoryDoc = DR["ReservationCategoryDoc"].ToString();
                    s.ReservationCategory = DR["ApplicationReservationName"].ToString();
                    s.GuardianName = DR["GuardianName"].ToString();
                    s.CurrentEmployerName = DR["CurrentEmployerName"].ToString();
                    s.IsSmsPermissionGiven = DR["IsSmsPermissionGiven"].ToString();
                    if (s.IsSmsPermissionGiven == "True")
                    {
                        s.IsSmsPermissionGiven = "Yes";
                    }
                    else
                    {
                        s.IsSmsPermissionGiven = "No";
                    }
                    s.IsLocalToVadodara = DR["IsLocalToVadodara"].ToString();
                    if (s.IsLocalToVadodara == "True")
                    {
                        s.IsLocalToVadodara = "Yes";
                    }
                    else
                    {
                        s.IsLocalToVadodara = "No";
                    }
                    s.EWSDoc = DR["EWSDoc"].ToString();
                    s.PCDoc = DR["PCDoc"].ToString();
                    s.DisabilityPercentage = DR["DisabilityPercentage"].ToString();
                    s.DisabilityType = DR["DisabilityType"].ToString();
                    s.ApplicantPhoto = DR["ApplicantPhoto"].ToString();
                    s.ApplicantSignature = DR["ApplicantSignature"].ToString();
                    s.InstancePartTermName = DR["InstancePartTermName"].ToString();
                    s.AdmApplicationIdStr = DR["Id"].ToString();
                    s.VerifiedStatus = DR["IsVerifiedByFaculty"].ToString();
                    if (s.VerifiedStatus == "True")
                    {
                        s.VerifiedStatus = "Verified";
                    }
                    else
                    {
                        s.VerifiedStatus = "Not Verified";
                    }
                    s.IsAdmitted = DR["IsAdmitted"].ToString();
                    if (s.IsAdmitted == "True")
                    {
                        s.IsAdmitted = "Yes";
                    }
                    else
                    {
                        s.IsAdmitted = "No";
                    }
                    s.AdmittedOn = DR["AdmittedOn"].ToString();
                    s.AdmittedInstituteName = DR["InstituteName"].ToString();
                    s.AdminRemarkByFaculty = DR["AdminRemarkByFaculty"].ToString();
                    s.AdminRemarkByAcademics = DR["AdminRemarkByAcademics"].ToString();
                    s.EligibleDegreeName = DR["EligibleDegreeName"].ToString();
                    s.SpecializationName = DR["SpecializationName"].ToString();
                    s.ExaminationBodyName = DR["ExaminationBodyName"].ToString();
                    s.InstituteAttended = DR["InstituteAttended"].ToString();
                    s.SchoolNo = DR["SchoolNo"].ToString();
                    s.CityName = DR["CityName"].ToString();
                    s.OtherCity = DR["OtherCity"].ToString();
                    s.ExamPassCity = DR["ExamPassCity"].ToString();
                    s.ExamPassMonth = DR["ExamPassMonth"].ToString();
                    s.ExamPassYear = DR["ExamPassYear"].ToString();
                    s.ExamSeatNumber = DR["ExamSeatNumber"].ToString();
                    s.ExamCertificateNumber = DR["ExamCertificateNumber"].ToString();
                    s.MarkObtained = DR["MarkObtained"].ToString();
                    s.MarkOutof = DR["MarkOutof"].ToString();
                    s.Grade = DR["Grade"].ToString();
                    s.CGPA = DR["CGPA"].ToString();
                    s.PercentageEquivalenceCGPA = DR["PercentageEquivalenceCGPA"].ToString();
                    s.Percentage = DR["Percentage"].ToString();
                    s.ClassName = DR["ClassName"].ToString();
                    s.IsFirstTrial = DR["IsFirstTrial"].ToString();
                    if (s.IsFirstTrial == "True")
                    {
                        s.IsFirstTrial = "Yes";
                    }
                    else
                    {
                        s.IsFirstTrial = "No";
                    }
                    s.IsLastQualifyingExam = DR["IsLastQualifyingExam"].ToString();
                    if (s.IsLastQualifyingExam == "True")
                    {
                        s.IsLastQualifyingExam = "Yes";
                    }
                    else
                    {
                        s.IsLastQualifyingExam = "No";
                    }
                    s.LanguageName = DR["LanguageName"].ToString();
                    s.ResultStatus = DR["ResultStatus"].ToString();
                    s.AttachDocument = DR["AttachDocument"].ToString();
                    s.IsDeclared = DR["IsDeclared"].ToString();
                    if (s.IsDeclared == "True")
                    {
                        s.IsDeclared = "Yes";
                    }
                    else
                    {
                        s.IsDeclared = "No";
                    }
                    s.IsCancelledByStudent = DR["IsCancelledByStudent"].ToString();
                    if (s.IsCancelledByStudent == "True")
                    {
                        s.IsCancelledByStudent = "Yes";
                    }
                    else
                    {
                        s.IsCancelledByStudent = "No";
                    }

                    count = count + 1;
                    s.IndexId = count;
                    s.Name = s.FirstName + " " + s.MiddleName + " " + s.LastName;

                    slist.Add(s);
                }
                return Return.returnHttp("200", slist, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }

        //-Archana's Code Start------Created By Archana Nigam On 29June2021 --------

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
                cmd.CommandText = "AppStatPartTermGetByInstituteId";
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

        #region ApplicationListSearchByAppStatusIncomplete
        // This method is for searching short details of applicant according to ProgrammeInstancePartTermId whose Application Fee Not Paid
        [HttpPost]
        public HttpResponseMessage ApplicationListSearchByAppStatusInComp(ApplicationListShort applicationList)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                List<ApplicationListShort> slist = new List<ApplicationListShort>();

                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", applicationList.InstancePartTermId);
                cmd.CommandText = "ApplicationListSearchByAppStatusInComp";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                da.SelectCommand = cmd;
                da.Fill(dt);

                var valueDistinctByIdColumn = dt.AsEnumerable().DistinctBy(row => new { Id = row["Id"] });
                DataTable dt2 = valueDistinctByIdColumn.CopyToDataTable();

                Int32 count = 0;
                foreach (DataRow DR in dt2.Rows)
                {
                    ApplicationListShort s = new ApplicationListShort();

                    s.FirstName = DR["FirstName"].ToString();
                    s.LastName = DR["LastName"].ToString();
                    s.EmailId = DR["EmailId"].ToString();
                    s.MobileNo = DR["MobileNo"].ToString();
                    s.IsEWS = DR["IsEWS"].ToString();
                    if (s.IsEWS == "True")
                    {
                        s.IsEWS = "Yes";
                    }
                    else
                    {
                        s.IsEWS = "No";
                    }
                    s.IsPhysicallyChallenged = DR["IsPhysicallyChallenged"].ToString();
                    if (s.IsPhysicallyChallenged == "True")
                    {
                        s.IsPhysicallyChallenged = "Yes";
                    }
                    else
                    {
                        s.IsPhysicallyChallenged = "No";
                    }
                    s.SocialCategoryName = DR["SocialCategoryName"].ToString();
                    s.ReservationCategory = DR["ApplicationReservationName"].ToString();
                    s.IsLocalToVadodara = DR["IsLocalToVadodara"].ToString();
                    if (s.IsLocalToVadodara == "True")
                    {
                        s.IsLocalToVadodara = "Yes";
                    }
                    else
                    {
                        s.IsLocalToVadodara = "No";
                    }
                    s.DisabilityPercentage = DR["DisabilityPercentage"].ToString();
                    s.DisabilityType = DR["DisabilityType"].ToString();
                    s.InstancePartTermName = DR["InstancePartTermName"].ToString();
                    s.AdmApplicationIdStr = DR["Id"].ToString();
                    s.VerifiedStatus = DR["IsVerifiedByFaculty"].ToString();
                    if (s.VerifiedStatus == "True")
                    {
                        s.VerifiedStatus = "Verified";
                    }
                    else
                    {
                        s.VerifiedStatus = "Not Verified";
                    }
                    s.IsAdmitted = DR["IsAdmitted"].ToString();
                    if (s.IsAdmitted == "True")
                    {
                        s.IsAdmitted = "Yes";
                    }
                    else
                    {
                        s.IsAdmitted = "No";
                    }
                    s.AdmittedOn = DR["AdmittedOn"].ToString();
                    s.AdmittedInstituteName = DR["InstituteName"].ToString();
                    s.AdminRemarkByFaculty = DR["AdminRemarkByFaculty"].ToString();
                    s.AdminRemarkByAcademics = DR["AdminRemarkByAcademics"].ToString();

                    count = count + 1;
                    s.IndexId = count;
                    s.Name = s.FirstName + " " + s.LastName;

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

        #region ApplicationListSearchFullByAppStatusIncomplete
        // This method is for searching full details of applicant according to ProgrammeInstancePartTermId  whose Application Fee Not Paid
        [HttpPost]
        public HttpResponseMessage ApplicationListSearchFullByAppStatusInComp(ApplicationList applicationList)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                List<ApplicationList> slist = new List<ApplicationList>();
                Int32 count = 0;
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", applicationList.InstancePartTermId);
                cmd.CommandText = "ApplicationListSearchByAppStatusInComp";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow DR in dt.Rows)
                {
                    ApplicationList s = new ApplicationList();

                    s.FirstName = DR["FirstName"].ToString();
                    s.LastName = DR["LastName"].ToString();
                    s.EmailId = DR["EmailId"].ToString();
                    s.MobileNo = DR["MobileNo"].ToString();
                    s.MiddleName = DR["MiddleName"].ToString();
                    s.SpouseName = DR["SpouseName"].ToString();
                    s.NameOfMother = DR["NameOfMother"].ToString();
                    s.NameOfFather = DR["NameOfFather"].ToString();
                    s.NameAsPerMarksheet = DR["NameAsPerMarksheet"].ToString();
                    s.Gender = DR["Gender"].ToString();
                    s.DOB = DR["DOB"].ToString();
                    s.AdmApplicantRegistrationId = DR["AdmApplicantRegistrationId"].ToString();
                    s.MaritalStatus = DR["MaritalStatus"].ToString();
                    s.MotherTongueName = DR["MotherTongueName"].ToString();
                    s.BloodGroupName = DR["BloodGroupName"].ToString();
                    s.ReligionName = DR["ReligionName"].ToString();
                    s.IsNRI = DR["IsNRI"].ToString();
                    if (s.IsNRI == "True")
                    {
                        s.IsNRI = "Yes";
                    }
                    else
                    {
                        s.IsNRI = "No";
                    }
                    s.HeightInCms = DR["HeightInCms"].ToString();
                    s.WeightInKgs = DR["WeightInKgs"].ToString();
                    s.CitizenCountry = DR["CitizenshipCountryName"].ToString();
                    s.PermanentAddress = DR["PermanentAddress"].ToString();
                    s.PermanentCountry = DR["PermanentCountryName"].ToString();
                    s.PermanentState = DR["PermanentStateName"].ToString();
                    s.PermanentDistrict = DR["PermanentDistrictName"].ToString();
                    s.PermanentCityVillage = DR["PermanentCityVillage"].ToString();
                    s.PermanentPincode = DR["PermanentPincode"].ToString();
                    s.IsCurrentAsPermanent = DR["IsCurrentAsPermanent"].ToString();
                    if (s.IsCurrentAsPermanent == "True")
                    {
                        s.IsCurrentAsPermanent = "Yes";
                    }
                    else
                    {
                        s.IsCurrentAsPermanent = "No";
                    }
                    s.CurrentAddress = DR["CurrentAddress"].ToString();
                    s.CurrentCountry = DR["CurrentCountryName"].ToString();
                    s.CurrentState = DR["CurrentStateName"].ToString();
                    s.CurrentDistrict = DR["CurrentDistrictName"].ToString();
                    s.CurrentCityVillage = DR["CurrentCityVillage"].ToString();
                    s.CurrentPincode = DR["CurrentPincode"].ToString();
                    s.OptionalMobileNo = DR["OptionalMobileNo"].ToString();
                    s.FatherMotherContactNo = DR["FatherMotherContactNo"].ToString();
                    s.GuardianContactNo = DR["GuardianContactNo"].ToString();
                    s.IsEWS = DR["IsEWS"].ToString();
                    if (s.IsEWS == "True")
                    {
                        s.IsEWS = "Yes";
                    }
                    else
                    {
                        s.IsEWS = "No";
                    }
                    s.IsPhysicallyChallenged = DR["IsPhysicallyChallenged"].ToString();
                    if (s.IsPhysicallyChallenged == "True")
                    {
                        s.IsPhysicallyChallenged = "Yes";
                    }
                    else
                    {
                        s.IsPhysicallyChallenged = "No";
                    }
                    s.IsEmp = DR["IsEmp"].ToString();
                    if (s.IsEmp == "True")
                    {
                        s.IsEmp = "Yes";
                    }
                    else
                    {
                        s.IsEmp = "No";
                    }
                    s.FatherOccupation = DR["FatherOccupation"].ToString();
                    s.MotherOccupation = DR["MotherOccupation"].ToString();
                    s.GuardianOccupation = DR["GuardianOccupation"].ToString();
                    s.FamilyAnnualIncome = DR["FamilyAnnualIncome"].ToString();
                    s.GuardianAnnualIncome = DR["GuardianAnnualIncome"].ToString();
                    s.IsGuardianEbc = DR["IsGuardianEbc"].ToString();
                    if (s.IsGuardianEbc == "True")
                    {
                        s.IsGuardianEbc = "Yes";
                    }
                    else
                    {
                        s.IsGuardianEbc = "No";
                    }
                    s.UserName = DR["UserName"].ToString();
                    s.OtherReligion = DR["OtherReligion"].ToString();
                    s.DOBDoc = DR["DOBDoc"].ToString();
                    s.PhotoIdDoc = DR["PhotoIdDoc"].ToString();
                    s.IsMajorThelesamiaStatus = DR["IsMajorThelesamiaStatus"].ToString();
                    if (s.IsMajorThelesamiaStatus == "True")
                    {
                        s.IsMajorThelesamiaStatus = "Yes";
                    }
                    else
                    {
                        s.IsMajorThelesamiaStatus = "No";
                    }
                    s.PassportNumber = DR["PassportNumber"].ToString();
                    s.PassportDate = DR["PassportDate"].ToString();
                    s.AadharNumber = DR["AadharNumber"].ToString();
                    s.AadharDoc = DR["AadharDoc"].ToString();
                    s.NameOnAadhar = DR["NameOnAadhar"].ToString();
                    s.SocialCategoryName = DR["SocialCategoryName"].ToString();
                    s.GSocialCategory = DR["GSocialCategoryName"].ToString();
                    s.IsSocialDocSubmitted = DR["IsSocialDocSubmitted"].ToString();
                    if (s.IsSocialDocSubmitted == "True")
                    {
                        s.IsSocialDocSubmitted = "Yes";
                    }
                    else
                    {
                        s.IsSocialDocSubmitted = "No";
                    }
                    s.IsEWSDocSubmitted = DR["IsEWSDocSubmitted"].ToString();
                    if (s.IsEWSDocSubmitted == "True")
                    {
                        s.IsEWSDocSubmitted = "Yes";
                    }
                    else
                    {
                        s.IsEWSDocSubmitted = "No";
                    }
                    s.IsPCDocSubmitted = DR["IsPCDocSubmitted"].ToString();
                    if (s.IsPCDocSubmitted == "True")
                    {
                        s.IsPCDocSubmitted = "Yes";
                    }
                    else
                    {
                        s.IsPCDocSubmitted = "No";
                    }
                    s.IsReservationDocSubmitted = DR["IsReservationDocSubmitted"].ToString();
                    if (s.IsReservationDocSubmitted == "True")
                    {
                        s.IsReservationDocSubmitted = "Yes";
                    }
                    else
                    {
                        s.IsReservationDocSubmitted = "No";
                    }
                    s.SocialCategoryDoc = DR["SocialCategoryDoc"].ToString();
                    s.ReservationCategoryDoc = DR["ReservationCategoryDoc"].ToString();
                    s.ReservationCategory = DR["ApplicationReservationName"].ToString();
                    s.GuardianName = DR["GuardianName"].ToString();
                    s.CurrentEmployerName = DR["CurrentEmployerName"].ToString();
                    s.IsSmsPermissionGiven = DR["IsSmsPermissionGiven"].ToString();
                    if (s.IsSmsPermissionGiven == "True")
                    {
                        s.IsSmsPermissionGiven = "Yes";
                    }
                    else
                    {
                        s.IsSmsPermissionGiven = "No";
                    }
                    s.IsLocalToVadodara = DR["IsLocalToVadodara"].ToString();
                    if (s.IsLocalToVadodara == "True")
                    {
                        s.IsLocalToVadodara = "Yes";
                    }
                    else
                    {
                        s.IsLocalToVadodara = "No";
                    }
                    s.EWSDoc = DR["EWSDoc"].ToString();
                    s.PCDoc = DR["PCDoc"].ToString();
                    s.DisabilityPercentage = DR["DisabilityPercentage"].ToString();
                    s.DisabilityType = DR["DisabilityType"].ToString();
                    s.ApplicantPhoto = DR["ApplicantPhoto"].ToString();
                    s.ApplicantSignature = DR["ApplicantSignature"].ToString();
                    s.InstancePartTermName = DR["InstancePartTermName"].ToString();
                    s.AdmApplicationIdStr = DR["Id"].ToString();
                    s.VerifiedStatus = DR["IsVerifiedByFaculty"].ToString();
                    if (s.VerifiedStatus == "True")
                    {
                        s.VerifiedStatus = "Verified";
                    }
                    else
                    {
                        s.VerifiedStatus = "Not Verified";
                    }
                    s.IsAdmitted = DR["IsAdmitted"].ToString();
                    if (s.IsAdmitted == "True")
                    {
                        s.IsAdmitted = "Yes";
                    }
                    else
                    {
                        s.IsAdmitted = "No";
                    }
                    s.AdmittedOn = DR["AdmittedOn"].ToString();
                    s.AdmittedInstituteName = DR["InstituteName"].ToString();
                    s.AdminRemarkByFaculty = DR["AdminRemarkByFaculty"].ToString();
                    s.AdminRemarkByAcademics = DR["AdminRemarkByAcademics"].ToString();
                    s.EligibleDegreeName = DR["EligibleDegreeName"].ToString();
                    s.SpecializationName = DR["SpecializationName"].ToString();
                    s.ExaminationBodyName = DR["ExaminationBodyName"].ToString();
                    s.InstituteAttended = DR["InstituteAttended"].ToString();
                    s.SchoolNo = DR["SchoolNo"].ToString();
                    s.CityName = DR["CityName"].ToString();
                    s.OtherCity = DR["OtherCity"].ToString();
                    s.ExamPassCity = DR["ExamPassCity"].ToString();
                    s.ExamPassMonth = DR["ExamPassMonth"].ToString();
                    s.ExamPassYear = DR["ExamPassYear"].ToString();
                    s.ExamSeatNumber = DR["ExamSeatNumber"].ToString();
                    s.ExamCertificateNumber = DR["ExamCertificateNumber"].ToString();
                    s.MarkObtained = DR["MarkObtained"].ToString();
                    s.MarkOutof = DR["MarkOutof"].ToString();
                    s.Grade = DR["Grade"].ToString();
                    s.CGPA = DR["CGPA"].ToString();
                    s.PercentageEquivalenceCGPA = DR["PercentageEquivalenceCGPA"].ToString();
                    s.Percentage = DR["Percentage"].ToString();
                    s.ClassName = DR["ClassName"].ToString();
                    s.IsFirstTrial = DR["IsFirstTrial"].ToString();
                    if (s.IsFirstTrial == "True")
                    {
                        s.IsFirstTrial = "Yes";
                    }
                    else
                    {
                        s.IsFirstTrial = "No";
                    }
                    s.IsLastQualifyingExam = DR["IsLastQualifyingExam"].ToString();
                    if (s.IsLastQualifyingExam == "True")
                    {
                        s.IsLastQualifyingExam = "Yes";
                    }
                    else
                    {
                        s.IsLastQualifyingExam = "No";
                    }
                    s.LanguageName = DR["LanguageName"].ToString();
                    s.ResultStatus = DR["ResultStatus"].ToString();
                    s.AttachDocument = DR["AttachDocument"].ToString();
                    s.IsDeclared = DR["IsDeclared"].ToString();
                    if (s.IsDeclared == "True")
                    {
                        s.IsDeclared = "Yes";
                    }
                    else
                    {
                        s.IsDeclared = "No";
                    }
                    count = count + 1;
                    s.IndexId = count;
                    s.Name = s.FirstName + " " + s.MiddleName + " " + s.LastName;

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
        //----Archana's Code End

    }
}
