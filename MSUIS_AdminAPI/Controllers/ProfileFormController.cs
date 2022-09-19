using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using MSUISApi.Models;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Web;
using MSUIS_TokenManager.App_Start;

namespace MSUISApi.Controllers
{
    public class ProfileFormController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        ApplicantFormAllDocument vApplicantDetail = new ApplicantFormAllDocument();
        string serverUrl = "https://admission.msubaroda.ac.in/MSUISApi/Upload/";
        
        //string serverUrl = "http://172.25.15.22/MSUISApi/Upload/";

        //AdmApplicationEducationDetails table Get
        [HttpPost]
        public HttpResponseMessage getProfileEducationDetailsList(ProfileForm ObjProfileForm)
        {
            //AdmApplicationEducationDetails aed1 = new AdmApplicationEducationDetails();
            //dynamic jsonData = jsonobject;
            try
            {
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                SqlCommand cmd = new SqlCommand("AdmApplicationEducationDetailsGetForDownloadApplicationForm", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", ObjProfileForm.PRN);
                cmd.Parameters.AddWithValue("@ApplicationId", ObjProfileForm.AdmissionApplicationId);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                List<ProfileForm> aedList = new List<ProfileForm>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ProfileForm aed = new ProfileForm();
                        aed.EduId = Convert.ToInt32(dr["Id"].ToString());
                        aed.AdmApplicantRegiUserName = Convert.ToInt64(responseToken);
                        //aed.AdmApplicantRegistrationId = Convert.ToInt32(responseToken);
                        aed.EligibleDegreeId = Convert.ToInt32(dr["EligibleDegreeId"].ToString());
                        aed.EligibleDegreeName = dr["EligibleDegreeName"].ToString();
                        aed.SpecializationId = Convert.ToInt32(dr["SpecializationId"].ToString());
                        aed.SpecializationName = dr["SpecializationName"].ToString();
                        aed.ExaminationBodyId = Convert.ToInt32(dr["ExaminationBodyId"].ToString());
                        aed.ExaminationBodyName = dr["ExaminationBodyName"].ToString();
                        aed.InstituteAttended = dr["InstituteAttended"].ToString();
                        aed.InstituteCityId = Convert.ToInt32(dr["InstituteCityId"].ToString());



                        if (dr["ExamPassMonth"].ToString() != "")
                        {
                            aed.ExamPassMonth = dr["ExamPassMonth"].ToString();
                        }
                        else
                        {
                            aed.ExamPassMonth = "--";
                        }



                        if (dr["ExamPassYear"].ToString() != "")
                        {
                            aed.ExamPassYear = dr["ExamPassYear"].ToString();
                        }
                        else
                        {
                            aed.ExamPassMonth = "--";
                        }



                        if (dr["MarkObtained"].ToString() != "")
                        {
                            aed.MarkObtained = Convert.ToInt32(dr["MarkObtained"].ToString());
                        }



                        if (dr["MarkOutof"].ToString() != "")
                        {
                            aed.MarkOutof = Convert.ToInt32(dr["MarkOutof"].ToString());
                        }



                        if (dr["Grade"].ToString() != "")
                        {
                            aed.Grade = dr["Grade"].ToString();
                        }
                        else
                        {
                            aed.Grade = "--";
                        }



                        if (dr["CGPA"].ToString() != "")
                        {
                            aed.CGPA = Convert.ToDecimal(dr["CGPA"].ToString());
                        }
                        if (dr["PercentageEquivalenceCGPA"].ToString() != "")
                        {
                            aed.PercentageEquivalenceCGPA = Convert.ToDecimal(dr["PercentageEquivalenceCGPA"].ToString());
                        }
                        if (dr["Percentage"].ToString() != "")
                        {
                            aed.Percentage = Convert.ToDecimal(dr["Percentage"].ToString());
                        }
                        if (dr["ClassId"].ToString() != "")
                        {
                            aed.ClassId = Convert.ToInt32(dr["ClassId"].ToString());
                        }



                        if (dr["ClassName"].ToString() != "")
                        {
                            aed.ClassName = dr["ClassName"].ToString();
                        }
                        else
                        {
                            aed.ClassName = "--";
                        }
                        aedList.Add(aed);
                    }
                }
                return Return.returnHttp("200", aedList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }

        [HttpPost]
        public HttpResponseMessage AdmApplicantRegistrationGet(ProfileForm ObjProfileForm)
        {
            try
            {
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                DataSet ds = new DataSet();
                List<ProfileForm> slist = new List<ProfileForm>();
                SqlCommand cmd = new SqlCommand("ProfileGetForDownloadApplicationForm", con);
                // cmd.CommandText = "ProfileGet";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", ObjProfileForm.PRN);
                cmd.Parameters.AddWithValue("@ApplicationId", ObjProfileForm.AdmissionApplicationId);
                cmd.Connection = con;
                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow DR in dt.Rows)
                {
                    ProfileForm s = new ProfileForm();

                    //s.Id = Convert.ToInt64(DR["Id"].ToString());
                    s.Id = Convert.ToInt64(responseToken);
                    //s.UserName = Convert.ToInt64(DR["UserName"].ToString());
                    //s.Password = DR["Password"].ToString();
                    s.LastName = DR["LastName"].ToString();
                    s.FirstName = DR["FirstName"].ToString();
                    s.MiddleName = DR["MiddleName"].ToString();
                    s.Gender = DR["Gender"].ToString();
                    s.ReligionId = Convert.ToInt64(DR["ReligionId"].ToString());
                    s.ReligionName = DR["ReligionName"].ToString();
                    if (DR["MaritalStatus"].ToString() != "")
                    {
                        s.MaritalStatus = Convert.ToString(DR["MaritalStatus"].ToString());
                    }
                    else
                    {
                        s.MaritalStatus = "--";
                    }
                    s.MotherTongueId = Convert.ToInt64(DR["MotherTongueId"].ToString());
                    s.MotherTongueName = DR["MotherTongueName"].ToString();
                    //s.CommunicationLanguageId = Convert.ToInt64(DR["CommunicationLanguageId"].ToString());
                    s.NameAsPerMarksheet = DR["NameAsPerMarksheet"].ToString();
                    //s.DOB = DR["DOB"].ToString();
                    s.DOB = string.Format("{0: dd/MM/yyyy}", DR["DOB"]);

                    if (DR["BloodGroupId"].ToString() != "")
                    {
                        s.BloodGroupId = Convert.ToInt64(DR["BloodGroupId"].ToString());
                    }
                    else
                    {
                        s.BloodGroupId = 0;
                    }
                    if (DR["BloodGroupName"].ToString() != "")
                    {
                        s.BloodGroupName = DR["BloodGroupName"].ToString();

                    }
                    else
                    {
                        s.BloodGroupName = "--";
                    }

                    if (DR["HeightInCms"].ToString() != "")
                    {
                        s.HeightInCms = Convert.ToDecimal(DR["HeightInCms"].ToString());
                    }
                    else
                    {
                        s.HeightInCms = 0;
                    }

                    if (DR["WeightInKgs"].ToString() != "")
                    {
                        s.WeightInKgs = Convert.ToDecimal(DR["WeightInKgs"].ToString());
                    }
                    else
                    {
                        s.WeightInKgs = 0;
                    }

                    s.IsMajorThelesamiaStatus = Convert.ToString(DR["IsMajorThelesamiaStatus"].ToString());
                    s.IsNRI = DR["IsNRI"].ToString();
                    s.CountryIdOfCitizenship = Convert.ToInt64(DR["CountryIdOfCitizenship"].ToString());
                    s.CitizenCountry = DR["CitizenCountry"].ToString();

                    if (DR["PassportNumber"].ToString() != "")
                    {
                        s.PassportNumber = DR["PassportNumber"].ToString();

                    }
                    else
                    {
                        s.PassportNumber = "--";
                    }

                    if (DR["AadharNumber"].ToString() != "")
                    {
                        s.AadharNumber = DR["AadharNumber"].ToString();
                    }


                    if (DR["NameOnAadhar"].ToString() != "")
                    {
                        s.NameOnAadhar = DR["NameOnAadhar"].ToString();
                    }
                    else
                    {
                        s.NameOnAadhar = "--";
                    }

                    s.PermanentAddress = DR["PermanentAddress"].ToString();
                    s.PermanentCountryId = Convert.ToInt64(DR["PermanentCountryId"].ToString());
                    s.PermanentCountry = DR["PermanentCountry"].ToString();
                    s.PermanentStateId = Convert.ToInt64(DR["PermanentStateId"].ToString());
                    s.PermanentState = DR["PermanentState"].ToString();
                    s.PermanentDistrictId = Convert.ToInt64(DR["PermanentDistrictId"].ToString());
                    s.PermanentDistrict = DR["PermanentDistrict"].ToString();
                    s.PermanentCityVillage = DR["PermanentCityVillage"].ToString();
                    s.PermanentPincode = Convert.ToInt64(DR["PermanentPincode"].ToString());

                    if (DR["CurrentAddress"].ToString() != "")
                    {
                        s.CurrentAddress = DR["CurrentAddress"].ToString();
                    }
                    else
                    {
                        s.CurrentAddress = "--";
                    }

                    if (DR["CurrentCountryId"].ToString() != "")
                    {
                        s.CurrentCountryId = Convert.ToInt64(DR["CurrentCountryId"].ToString());
                    }
                    else
                    {
                        s.CurrentCountryId = 0;
                    }
                    s.CurrentCountry = DR["CurrentCountry"].ToString();

                    if (DR["CurrentStateId"].ToString() != "")
                    {
                        s.CurrentStateId = Convert.ToInt64(DR["CurrentStateId"].ToString());
                    }
                    else
                    {
                        s.CurrentStateId = 0;
                    }
                    s.CurrentState = DR["CurrentState"].ToString();

                    if (DR["CurrentDistrictId"].ToString() != "")
                    {
                        s.CurrentDistrictId = Convert.ToInt64(DR["CurrentDistrictId"].ToString());
                    }
                    else
                    {
                        s.CurrentDistrictId = 0;
                    }
                    s.CurrentDistrict = DR["CurrentDistrict"].ToString();

                    if (DR["CurrentCityVillage"].ToString() != "")
                    {
                        s.CurrentCityVillage = DR["CurrentCityVillage"].ToString();
                    }
                    else
                    {
                        s.CurrentCityVillage = "--";
                    }


                    if (DR["CurrentPincode"].ToString() != "")
                    {
                        s.CurrentPincode = Convert.ToInt64(DR["CurrentPincode"].ToString());
                    }
                    else
                    {
                        s.CurrentPincode = 0;
                    }

                    if (DR["NameOfFather"].ToString() != "")
                    {
                        s.NameOfFather = DR["NameOfFather"].ToString();
                    }
                    else
                    {
                        s.NameOfFather = "--";
                    }

                    if (DR["NameOfMother"].ToString() != "")
                    {
                        s.NameOfMother = DR["NameOfMother"].ToString();
                    }
                    else
                    {
                        s.NameOfMother = "--";
                    }


                    if (DR["SocialCategoryId"].ToString() != "")
                    {
                        s.SocialCategoryId = Convert.ToInt64(DR["SocialCategoryId"].ToString());
                    }

                    s.SocialCategoryName = DR["SocialCategoryName"].ToString();
                    //s.GSocialCategoryId = Convert.ToInt64(DR["GSocialCategoryId"].ToString());
                    if (DR["GSocialCategoryId"].ToString() != "")
                    {
                        s.GSocialCategoryId = Convert.ToInt64(DR["GSocialCategoryId"].ToString());
                    }
                    else
                    {
                        s.GSocialCategoryId = 0;
                    }

                    s.GSocialCategory = DR["GSocialCategory"].ToString();

                    if (DR["ReservationCategory"].ToString() != "")
                    {
                        s.ReservationCategory = DR["ReservationCategory"].ToString();
                    }

                    if (DR["GuardianName"].ToString() != "")
                    {
                        s.GuardianName = DR["GuardianName"].ToString();
                    }
                    else
                    {
                        s.GuardianName = "--";
                    }

                    if (DR["GuardianContactNo"].ToString() != "")
                    {
                        s.GuardianContactNo = DR["GuardianContactNo"].ToString();
                    }
                    else
                    {
                        s.GuardianContactNo = "--";
                    }

                    if (DR["FamilyAnnualIncome"].ToString() != "")
                    {
                        s.FamilyAnnualIncome = Convert.ToInt64(DR["FamilyAnnualIncome"].ToString());
                    }
                    else
                    {
                        s.FamilyAnnualIncome = 0;
                    }

                    if (DR["OccupationIdOfFather"].ToString() != "")
                    {
                        s.OccupationIdOfFather = Convert.ToInt64(DR["OccupationIdOfFather"].ToString());
                    }
                    else
                    {
                        s.OccupationIdOfFather = 0;
                    }
                    s.OccupationOfFather = DR["OccupationOfFather"].ToString();

                    if (DR["OccupationIdOfMother"].ToString() != "")
                    {
                        s.OccupationIdOfMother = Convert.ToInt64(DR["OccupationIdOfMother"].ToString());
                    }
                    else
                    {
                        s.OccupationIdOfMother = 0;
                    }
                    s.OccupationOfMother = DR["OccupationOfMother"].ToString();

                    if (DR["OccupationIdOfGuardian"].ToString() != "")
                    {
                        s.OccupationIdOfGuardian = Convert.ToInt64(DR["OccupationIdOfGuardian"].ToString());
                    }
                    else
                    {
                        s.OccupationIdOfGuardian = 0;
                    }
                    s.OccupationOfGuardian = DR["OccupationOfGuardian"].ToString();

                    if (DR["IsEmp"].ToString() != "")
                    {
                        if (Convert.ToBoolean(DR["IsEmp"].ToString()) == true)
                        {
                            s.IsSelfEmpDisplay = "Yes";
                        }
                        else
                        {
                            s.IsSelfEmpDisplay = "No";
                        }
                        // s.IsSelfEmp = Convert.ToBoolean(DR["IsEmp"].ToString());
                    }
                    else
                    {
                        s.IsSelfEmpDisplay = "No";
                    }

                    if (DR["IsGuardianEbc"].ToString() != "")
                    {
                        if (Convert.ToBoolean(DR["IsGuardianEbc"].ToString()) == true)
                        {
                            s.IsEbcDisplay = "Yes";
                        }
                        else
                        {
                            s.IsEbcDisplay = "No";
                        }
                        //s.IsEbc = Convert.ToBoolean(DR["IsGuardianEbc"].ToString());
                    }
                    else
                    {
                        s.IsEbcDisplay = "No";
                    }

                    if (DR["GuardianAnnualIncome"].ToString() != "")
                    {
                        s.GuardianAnnualIncome = Convert.ToInt64(DR["GuardianAnnualIncome"].ToString());
                    }
                    else
                    {
                        s.GuardianAnnualIncome = 0;
                    }

                    s.EmailId = DR["EmailId"].ToString();
                    s.MobileNo = DR["MobileNo"].ToString();

                    if (DR["OptionalMobileNo"].ToString() != "")
                    {
                        s.OptionalMobileNo = DR["OptionalMobileNo"].ToString();
                    }
                    else
                    {
                        s.OptionalMobileNo = "--";
                    }

                    if (Convert.ToBoolean(DR["IsSmsPermissionGiven"].ToString()) == true)
                    {
                        s.IsSmsPermissionGivenDisplay = "Yes";
                    }
                    else
                    {
                        s.IsSmsPermissionGivenDisplay = "No";
                    }
                    //s.IsSmsPermissionGiven = Convert.ToBoolean(DR["IsSmsPermissionGiven"].ToString());

                    if (DR["IsLocalToVadodara"].ToString() != "")
                    {
                        s.IsLocalToVadodara = Convert.ToBoolean(DR["IsLocalToVadodara"].ToString());
                    }
                    else
                    {
                        s.IsLocalToVadodara = null;
                    }

                    //s.Activity = DR["Activity"].ToString();

                    if (DR["ActivityId"].ToString() != "")
                    {
                        s.ActivityId = Convert.ToInt64(DR["ActivityId"].ToString());

                    }
                    else
                    {
                        s.ActivityId = 0;
                    }

                    if (DR["ActivityName"].ToString() != "")
                    {
                        s.ActivityName = DR["ActivityName"].ToString();

                    }
                    else
                    {
                        s.ActivityName = "--";
                    }

                    if (DR["ParticipationLevelsId"].ToString() != "")
                    {
                        s.ParticipationLevelsId = Convert.ToInt64(DR["ParticipationLevelsId"].ToString());

                    }
                    else
                    {
                        s.ParticipationLevelsId = 0;
                    }

                    if (DR["SecuredRankId"].ToString() != "")
                    {
                        s.SecuredRankId = Convert.ToInt64(DR["SecuredRankId"].ToString());

                    }
                    else
                    {
                        s.SecuredRankId = 0;
                    }
                    s.IsEWS = DR["IsEWS"].ToString();

                    if (DR["EWSDoc"].ToString() != "")
                    {
                        s.EWSDoc = DR["EWSDoc"].ToString();

                    }
                    else
                    {
                        s.EWSDoc = "--";
                    }

                    if (DR["IsPhysicallyChallenged"].ToString() != "")
                    {
                        if (DR["IsPhysicallyChallenged"].ToString() == "True")
                        {
                            s.IsPhysicallyChallenged = "Yes";
                        }
                        else
                        {
                            s.IsPhysicallyChallenged = "No";
                        }
                        //s.IsPhysicallyChallenged = DR["IsPhysicallyChallenged"].ToString();
                    }
                    else
                    {
                        s.IsPhysicallyChallenged = "No";
                    }

                    if (DR["PCDoc"].ToString() != "")
                    {
                        s.PCDoc = DR["PCDoc"].ToString();

                    }
                    else
                    {
                        s.PCDoc = "--";
                    }

                    if (DR["DisabilityPercentage"].ToString() != "")
                    {
                        s.DisabilityPercentage = Convert.ToInt64(DR["DisabilityPercentage"].ToString());
                    }
                    else
                    {
                        s.DisabilityPercentage = 0;
                    }

                    if (DR["DisabilityType"].ToString() != "")
                    {
                        s.DisabilityType = DR["DisabilityType"].ToString();

                    }
                    else
                    {
                        s.DisabilityType = "--";
                    }

                    string ApplicantPhotoFilePath = serverUrl + "Photo/" + DR["ApplicantPhoto"].ToString();
                    s.ApplicantPhoto = ApplicantPhotoFilePath;

                    string ApplicantSignatureFilePath = serverUrl + "Signature/" + DR["ApplicantSignature"].ToString();
                    s.ApplicantSignature = ApplicantSignatureFilePath;

                    /*string dirphoto = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/Photo/");
                    string picinfophoto = Convert.ToString(DR["ApplicantPhoto"]);
                    string pathphoto = Path.GetFullPath(dirphoto) + picinfophoto;
                    //string image = Convert.ToBase64String(pathphoto);
                    byte[] imageArrayphoto = System.IO.File.ReadAllBytes(pathphoto);
                    string base64PhotoRepresentation = Convert.ToBase64String(imageArrayphoto);
                    ////Path.Combine(dirphoto, picinfophoto);
                    s.ApplicantPhoto = base64PhotoRepresentation;

                    string dirsignature = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/Signature/");
                    string picinfosignature = Convert.ToString(DR["ApplicantSignature"]);
                    string pathsignature = Path.GetFullPath(dirsignature) + picinfosignature;
                    //string image = Convert.ToBase64String(pathsignature);
                    byte[] imageArraysignature = System.IO.File.ReadAllBytes(pathsignature);
                    string base64SignatureRepresentation = Convert.ToBase64String(imageArraysignature);
                    ////Path.Combine(dirsignature, picinfosignature);
                    s.ApplicantSignature = base64SignatureRepresentation;*/

                    //s.ParticipationLevel = DR["ParticipationLevel"].ToString();
                    //s.SecuredRank = DR["SecuredRank"].ToString();
                    //s.IsDeleted = Convert.ToBoolean(DR["IsDeleted"].ToString());

                    //if (s.IsDeleted == false)
                    //{
                    slist.Add(s);
                    //}

                }


                return Return.returnHttp("200", slist, null);



            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }

        [HttpPost]
        public HttpResponseMessage getFeePayCourseList(JObject jObject)
        {
            try
            {
                dynamic Jsondata = jObject;
                Int64 admAppId = Convert.ToInt64(Jsondata.admAppId);
                /*string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }*/

                SqlCommand cmd = new SqlCommand("FeePayCourseGet", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AdmissionApplicationId", admAppId);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                List<ProfileForm> feepaylist = new List<ProfileForm>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ProfileForm feepay = new ProfileForm();
                        //feepay.Id = Convert.ToInt32(responseToken);
                        //feepay.FeePayCourseId = Convert.ToInt32(responseToken);
                        feepay.AdmissionApplicationId = Convert.ToInt64(dr["AdmissionApplicationId"].ToString());
                        feepay.AdmApplicationCreatedOn = string.Format("{0: dd/MM/yyyy}", dr["AdmApplicationCreatedOn"]);
                        feepay.FacultyName = dr["FacultyName"].ToString();
                        feepay.FacultyAddress = dr["FacultyAddress"].ToString();
                        feepay.AcademicYearCode = dr["AcademicYearCode"].ToString();
                        feepay.Amount = dr["Amount"].ToString();
                        feepay.FeeTypeName = dr["FeeTypeName"].ToString();
                        feepay.PartTermName = dr["PartTermName"].ToString();
                        feepay.PartName = dr["PartName"].ToString();
                        feepay.ProgrammeName = dr["ProgrammeName"].ToString();
                        feepay.BranchName = dr["BranchName"].ToString();
                        feepay.TransectionId = dr["TransactionId"].ToString();
                        feepay.TransectionDate = string.Format("{0: dd/MM/yyyy}", dr["TransactionDate"]);
                        feepay.FeeCategoryName = dr["FeeCategoryName"].ToString();
                        feepay.AllotmentNo = dr["AllotmentNo"].ToString();

                        feepaylist.Add(feepay);
                    }

                }
                return Return.returnHttp("200", feepaylist, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }

        [HttpPost]
        public HttpResponseMessage getAdmApplicantRegistrationIdByAdmApplicationId(ApplicantFormDocument AppId)
        {
            ApplicantFormDocument modelobj = new ApplicantFormDocument();
            try
            {
                modelobj.Id = Convert.ToInt64(AppId.Id);
                SqlCommand cmd = new SqlCommand("ApplicantFormAllDocumentsGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", AppId.Id);
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", AppId.InstPartTermId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AppId.AcademicYearId);
                //cmd.Parameters.AddWithValue("@EligibilityStatus", AppId.EligibilityStatus);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                List<ApplicantFormDocument> ObjLstApplicantDetail = new List<ApplicantFormDocument>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {

                        ApplicantFormDocument AppDetails = new ApplicantFormDocument();
                        AppDetails.Id = Convert.ToInt64(dr["Id"]);
                        AppDetails.AdmApplicantRegistrationId = Convert.ToInt64(dr["ApplicantRegistrationId"].ToString());
                        AppDetails.UserName = Convert.ToInt64(dr["UserName"].ToString());
                        AppDetails.InstPartTermId = Convert.ToInt64(dr["ProgrammeInstancePartTermId"].ToString());
                        AppDetails.AcademicYearId = Convert.ToInt32(dr["AcademicYearId"].ToString());
                        AppDetails.EligibilityStatus = (dr["EligibilityStatus"].ToString());
                        vApplicantDetail.CheckAdmDateMessage = Convert.ToString(dr["CheckAdmDateMessage"].ToString());
                        vApplicantDetail.objApplicant = GetPersonalInfo(AppDetails.AdmApplicantRegistrationId, AppDetails.Id);
                        vApplicantDetail.objEduLst = GetEducationDetails(AppDetails.UserName);
                        vApplicantDetail.objSubDocsLst = GetSubmittedDocuments(AppDetails.Id,AppDetails.AcademicYearId);
                        vApplicantDetail.objAdditionalDocsLst = GetAdditionalDocuments(AppDetails.Id);
                        vApplicantDetail.objAddOnDocsLst = GetAddOnDetails(AppDetails.Id);
                        vApplicantDetail.objFeeLst = GetFeeAttached(AppDetails.Id, AppDetails.InstPartTermId, AppDetails.EligibilityStatus);
                        vApplicantDetail.objAdmApplicationinfo = GetAdmApplicationInfo(AppDetails.Id);
                        vApplicantDetail.objPreferenceLst = GetGroupPreference(AppDetails.Id);
                        vApplicantDetail.objInstituteLst = GetInstitutePreference(AppDetails.Id);
                        //vApplicantDetail.objAcademicInfo = GetAdmApplicationForAcademic(AppDetails.Id);
                        // ObjLstApplicantDetail.Add(AppDetails);
                    }

                }
                return Return.returnHttp("200", vApplicantDetail, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }

        [HttpPost]
        public HttpResponseMessage getAdmApplicantRegistrationIdByAdmApplicationIdBulk(ApplicantFormDocument AppId)
        {
            ApplicantFormDocument modelobj = new ApplicantFormDocument();
            try
            {
                modelobj.Id = Convert.ToInt64(AppId.Id);
                SqlCommand cmd = new SqlCommand("ApplicantFormAllDocumentsGetBulk", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@Id", AppId.Id);
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", AppId.InstPartTermId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AppId.AcademicYearId);
                //cmd.Parameters.AddWithValue("@EligibilityStatus", AppId.EligibilityStatus);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                //List<ApplicantFormDocument> ObjLstApplicantDetail = new List<ApplicantFormDocument>();
                List<ApplicantFormAllDocument> objLstvApplicantDetail = new List<ApplicantFormAllDocument>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {

                        //ApplicantFormDocument AppDetails = new ApplicantFormDocument();
                        ApplicantFormAllDocument vApplicantDetail = new ApplicantFormAllDocument();
                        //AppDetails.Id = 520267616261;
                        vApplicantDetail.Id = Convert.ToInt64(dr["Id"]);
                        vApplicantDetail.AdmApplicantRegistrationId = Convert.ToInt64(dr["ApplicantRegistrationId"].ToString());
                        vApplicantDetail.UserName = Convert.ToInt64(dr["UserName"].ToString());
                        vApplicantDetail.InstPartTermId = Convert.ToInt64(dr["ProgrammeInstancePartTermId"].ToString());
                        vApplicantDetail.AcademicYearId = Convert.ToInt32(dr["AcademicYearId"].ToString());
                        vApplicantDetail.EligibilityStatus = (dr["EligibilityStatus"].ToString());
                        vApplicantDetail.CheckAdmDateMessage = Convert.ToString(dr["CheckAdmDateMessage"].ToString());
                        vApplicantDetail.objBulkApplicant = GetPersonalInfoForBulk(vApplicantDetail.AdmApplicantRegistrationId, vApplicantDetail.Id, vApplicantDetail.InstPartTermId);
                        vApplicantDetail.objBulkEduLst = GetEducationDetailsForBulk(vApplicantDetail.UserName);
                        vApplicantDetail.objBulkSubDocsLst = GetSubmittedDocuments(vApplicantDetail.Id, vApplicantDetail.AcademicYearId);
                        vApplicantDetail.objBulkAdditionalDocsLst = GetAdditionalDocuments(vApplicantDetail.Id);
                        vApplicantDetail.objBulkAddOnDocsLst = GetAddOnDetails(vApplicantDetail.Id);
                        //vApplicantDetail.objFeeLst = GetFeeAttached(AppDetails.Id, AppDetails.InstPartTermId, AppDetails.EligibilityStatus);
                        //vApplicantDetail.objAdmApplicationinfo = GetAdmApplicationInfo(AppDetails.Id);
                        vApplicantDetail.objPreferenceLst = GetGroupPreference(vApplicantDetail.Id);
                        //if (!vApplicantDetail.objBulkPreferenceLst.Any()) { vApplicantDetail.PreErr = "No Preference Found."; }
                        vApplicantDetail.objInstituteLst = GetInstitutePreference(vApplicantDetail.Id);
                        vApplicantDetail.objFeePayCourseList = BulkFeePayCourseList(vApplicantDetail.Id);
                        vApplicantDetail.BulkAdmApplicantRegistrationList = BulkAdmApplicantRegistrationList(vApplicantDetail.Id, vApplicantDetail.UserName);
                        vApplicantDetail.BulkProfileEducationDetailsList = BulkProfileEducationDetailsList(vApplicantDetail.Id, vApplicantDetail.UserName);
                        //vApplicantDetail.objAcademicInfo = GetAdmApplicationForAcademic(AppDetails.Id);
                         //ObjLstApplicantDetail.Add(AppDetails);
                        objLstvApplicantDetail.Add(vApplicantDetail);
                    }

                }
                return Return.returnHttp("200", (objLstvApplicantDetail), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }

        [HttpPost]
        public HttpResponseMessage ApplicantListByInstancePartTerm(ApplicantFormDocument AppId)
        {

            try
            {
                SqlCommand cmd = new SqlCommand("ApplicantListByInstancePartTerm", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", AppId.InstPartTermId);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                List<ApplicantFormDocument> ObjLstApplicantDetail = new List<ApplicantFormDocument>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ApplicantFormDocument AppDetails = new ApplicantFormDocument();
                        AppDetails.Id = Convert.ToInt64(dr["Id"]);
                        AppDetails.UserName = Convert.ToInt64(dr["ApplicantUserName"].ToString());
                        AppDetails.InstPartTermId = Convert.ToInt64(dr["ProgrammeInstancePartTermId"].ToString());
                        AppDetails.AdmApplicantRegistrationId = Convert.ToInt64(dr["ApplicantRegistrationId"].ToString());
                        ObjLstApplicantDetail.Add(AppDetails);

                    }                    
                }
                return Return.returnHttp("200", ObjLstApplicantDetail, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }

        public Applicant GetPersonalInfo(Int64 RegId, Int64 AppId)
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sda = new SqlDataAdapter();
            DataTable dt = new DataTable();
            ApplicantFormAllDocument vApplicantDetail1 = new ApplicantFormAllDocument();
            Applicant s = new Applicant();
            cmd.Parameters.AddWithValue("@Id", RegId);

            //string serverUrl = "https://admission.msubaroda.ac.in/MSUISApi/Upload/";
            cmd.CommandText = "PostAdmApplicantRegistrationGetData";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Flag", "AdmApplicantRegistrationGetPersonalDetail");
            sda.SelectCommand = cmd;
            sda.Fill(dt);
            foreach (DataRow DR in dt.Rows)
            {
                s.FirstName = Convert.ToString(DR["FirstName"].ToString());
                s.MiddleName = Convert.ToString(DR["MiddleName"].ToString());
                s.LastName = Convert.ToString(DR["LastName"].ToString());
                s.Gender = Convert.ToString(DR["Gender"].ToString());
                if (DR["MobileNo"].ToString() != "")
                {
                    s.MobileNo = DR["MobileNo"].ToString();
                }
                if (DR["EmailId"].ToString() != "")
                {
                    s.EmailId = DR["EmailId"].ToString();
                }
                if (DR["DOBDoc"].ToString() != "")
                {
                    /* s.DOBDoc = DR["DOBDoc"].ToString();
                     string DobFilePath = serverUrl + "Documents/" + s.DOBDoc;
                     s.DOBDoc = DobFilePath;*/

                    string DobFilePath = serverUrl + "Documents/" + DR["DOBDoc"].ToString();
                    s.DOBDoc = DobFilePath;

                }
                if (DR["PhotoIdDoc"].ToString() != "")
                {
                    string PhotoIdFilePath = serverUrl + "Documents/" + DR["PhotoIdDoc"].ToString();
                    s.PhotoIdDoc = PhotoIdFilePath;
                }
                if (DR["AadharDoc"].ToString() != "")
                {
                    string AadharFilePath = serverUrl + "Documents/" + DR["AadharDoc"].ToString();
                    s.AadharDoc = AadharFilePath;
                }
                if (DR["ApplicantPhoto"].ToString() != "")
                {
                    string ApplicantPhotoFilePath = serverUrl + "Photo/" + DR["ApplicantPhoto"].ToString();
                    s.ApplicantPhoto = ApplicantPhotoFilePath;
                }
                if (DR["ApplicantSignature"].ToString() != "")
                {
                    string ApplicantSignatureFilePath = serverUrl + "Signature/" + DR["ApplicantSignature"].ToString();
                    s.ApplicantSignature = ApplicantSignatureFilePath;
                }

                if (DR["IsLocalToVadodara"].ToString() != "")
                {
                    s.IsLocalToVadodara = Convert.ToBoolean(DR["IsLocalToVadodara"].ToString());
                }
                if (DR["IsMajorThelesamiaStatus"].ToString() != "")
                {
                    s.IsMajorThelesamiaStatus = DR["IsMajorThelesamiaStatus"].ToString();
                }



                SqlCommand cmd1 = new SqlCommand();
                SqlDataAdapter sda1 = new SqlDataAdapter();
                DataTable dt1 = new DataTable();

                cmd1.Parameters.AddWithValue("@Id", RegId);
                cmd1.Parameters.AddWithValue("@AppId", AppId);
                cmd1.CommandText = "PostAdmApplicantRegistrationGetData";
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Connection = con;
                cmd1.Parameters.AddWithValue("@Flag", "AdmApplicantRegistrationGetSocialReservationDetail");
                sda1.SelectCommand = cmd1;
                sda1.Fill(dt1);
                foreach (DataRow DR1 in dt1.Rows)
                {

                    if (DR1["SocialCategoryId"].ToString() != "")
                    {
                        s.SocialCategoryId = Convert.ToInt64(DR1["SocialCategoryId"].ToString());
                    }
                    if (DR1["SocialCategoryName"].ToString() != "")
                    {
                        s.SocialCategoryName = DR1["SocialCategoryName"].ToString();
                    }
                    if (DR1["SocialCategoryCode"].ToString() != "")
                    {
                        s.SocialCategoryCode = DR1["SocialCategoryCode"].ToString();
                    }
                    if (DR1["ApplicationCategoryId"].ToString() != "")
                    {
                        s.ApplicationCategoryId = Convert.ToInt64(DR1["ApplicationCategoryId"].ToString());
                    }
                    if (DR1["ReservationCategory"].ToString() != "")
                    {
                        s.ReservationCategory = DR1["ReservationCategory"].ToString();
                    }
                    if (DR1["ReservationCategoryCode"].ToString() != "")
                    {
                        s.ReservationCategoryCode = DR1["ReservationCategoryCode"].ToString();
                    }
                    if (DR1["AppliedCategoryId"].ToString() != "")
                    {
                        s.AppliedCategoryId = Convert.ToInt64(DR1["AppliedCategoryId"].ToString());
                    }
                    if (DR1["AppliedCategory"].ToString() != "")
                    {
                        s.AppliedCategory = DR1["AppliedCategory"].ToString();
                    }

                    if (DR1["IsEWS"].ToString() != "")
                    {
                        s.IsEWS = DR1["IsEWS"].ToString();
                    }
                    if (DR1["IsPhysicallyChallenged"].ToString() != "")
                    {
                        s.IsPhysicallyChallenged = DR1["IsPhysicallyChallenged"].ToString();
                    }
                    if (DR1["EWSDoc"].ToString() != "" && DR1["EWSDoc"].ToString() != "Not Applicable")
                    {
                        string EWSDocFilePath = serverUrl + "Documents/" + DR1["EWSDoc"].ToString();
                        s.EWSDoc = EWSDocFilePath;
                    }
                    if (DR1["PCDoc"].ToString() != "" && DR1["PCDoc"].ToString() != "Not Applicable")
                    {
                        string PCDocFilePath = serverUrl + "Documents/" + DR1["PCDoc"].ToString();
                        s.PCDoc = PCDocFilePath;
                        //s.PCDoc = DR1["PCDoc"].ToString();
                    }
                    if (DR1["DisabilityPercentage"].ToString() != "")
                    {
                        s.DisabilityPercentage = Convert.ToInt64(DR1["DisabilityPercentage"].ToString());
                    }
                    if (DR1["DisabilityType"].ToString() != "")
                    {
                        s.DisabilityType = DR1["DisabilityType"].ToString();
                    }
                    if (DR1["IsSocialDocSubmitted"].ToString() != "")
                    {
                        s.IsSocialDocSubmitted = Convert.ToBoolean(DR1["IsSocialDocSubmitted"].ToString());
                    }
                    if (DR1["IsEWSDocSubmitted"].ToString() != "")
                    {
                        s.IsEWSDocSubmitted = Convert.ToBoolean(DR1["IsEWSDocSubmitted"].ToString());
                    }
                    if (DR1["IsPCDocSubmitted"].ToString() != "")
                    {
                        s.IsPCDocSubmitted = Convert.ToBoolean(DR1["IsPCDocSubmitted"].ToString());
                    }
                    if (DR1["IsReservationDocSubmitted"].ToString() != "")
                    {
                        s.IsReservationDocSubmitted = Convert.ToBoolean(DR1["IsReservationDocSubmitted"].ToString());
                    }
                    if (DR1["SocialCategoryDoc"].ToString() != "" && DR1["SocialCategoryDoc"].ToString() != "Not Applicable")
                    {
                        string SocialCategoryDocFilePath = serverUrl + "Documents/" + DR1["SocialCategoryDoc"].ToString();
                        s.SocialCategoryDoc = SocialCategoryDocFilePath;
                    }
                    if (DR1["ReservationCategoryDoc"].ToString() != "" && DR1["ReservationCategoryDoc"].ToString() != "Not Applicable")
                    {
                        string ReservationCategoryDocFilePath = serverUrl + "Documents/" + DR1["ReservationCategoryDoc"].ToString();
                        s.ReservationCategoryDoc = ReservationCategoryDocFilePath;
                    }
                    if (DR1["AppliedCategoryDocument"].ToString() != "")
                    {
                        s.AppliedCategoryDocument = DR1["AppliedCategoryDocument"].ToString();

                    }

                    SqlCommand cmd2 = new SqlCommand();
                    SqlDataAdapter sda2 = new SqlDataAdapter();
                    DataTable dt2 = new DataTable();

                    cmd2.Parameters.AddWithValue("@Id", RegId);
                    cmd2.CommandText = "PostAdmApplicantRegistrationGetData";
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Connection = con;
                    cmd2.Parameters.AddWithValue("@Flag", "AdmApplicantRegistrationGetResidentialAddress");
                    sda2.SelectCommand = cmd2;
                    sda2.Fill(dt2);

                    foreach (DataRow DR2 in dt2.Rows)
                    {

                        if (DR2["PermanentStateId"].ToString() != "")
                        {
                            s.PermanentStateId = Convert.ToInt64(DR2["PermanentStateId"].ToString());
                        }
                        if (DR2["PermanentState"].ToString() != "")
                        {
                            s.PermanentState = DR2["PermanentState"].ToString();
                        }
                        if (DR2["PermanentCityVillage"].ToString() != "")
                        {
                            s.PermanentCityVillage = DR2["PermanentCityVillage"].ToString();
                        }

                        SqlCommand cmd3 = new SqlCommand();
                        SqlDataAdapter sda3 = new SqlDataAdapter();
                        DataTable dt3 = new DataTable();

                        cmd3.Parameters.AddWithValue("@Id", RegId);
                        cmd3.CommandText = "PostAdmApplicantRegistrationGetData";
                        cmd3.CommandType = CommandType.StoredProcedure;
                        cmd3.Connection = con;
                        cmd3.Parameters.AddWithValue("@Flag", "AdmApplicantRegistrationGetVerifiedStatus");
                        sda3.SelectCommand = cmd3;
                        sda3.Fill(dt3);

                        foreach (DataRow DR3 in dt3.Rows)
                        {
                            var IsDOBDocVerified = DR3["IsDOBDocVerified"];
                            if (IsDOBDocVerified is DBNull)
                            {
                                s.IsDOBDocVerified = null;
                            }
                            else
                            {
                                s.IsDOBDocVerified = Convert.ToString(DR3["IsDOBDocVerified"].ToString());
                            }

                            var IsPhotoIdDocVerified = DR3["IsPhotoIdDocVerified"];
                            if (IsPhotoIdDocVerified is DBNull)
                            {
                                s.IsPhotoIdDocVerified = null;
                            }
                            else
                            {
                                s.IsPhotoIdDocVerified = Convert.ToString(DR3["IsPhotoIdDocVerified"].ToString());
                            }

                            var IsAadharDocVerified = DR3["IsAadharDocVerified"];
                            if (IsAadharDocVerified is DBNull)
                            {
                                s.IsAadharDocVerified = null;
                            }
                            else
                            {
                                s.IsAadharDocVerified = Convert.ToString(DR3["IsAadharDocVerified"].ToString());
                            }

                            var IsSocialCategoryDocVerified = DR3["IsSocialCategoryDocVerified"];
                            if (IsSocialCategoryDocVerified is DBNull)
                            {
                                s.IsSocialCategoryDocVerified = null;
                            }
                            else
                            {
                                s.IsSocialCategoryDocVerified = Convert.ToString(DR3["IsSocialCategoryDocVerified"].ToString());
                            }

                            var IsReservationCategoryDocVerified = DR3["IsReservationCategoryDocVerified"];
                            if (IsReservationCategoryDocVerified is DBNull)
                            {
                                s.IsReservationCategoryDocVerified = null;
                            }
                            else
                            {
                                s.IsReservationCategoryDocVerified = Convert.ToString(DR3["IsReservationCategoryDocVerified"].ToString());
                            }

                            var IsEWSDocVerified = DR3["IsEWSDocVerified"];
                            if (IsEWSDocVerified is DBNull)
                            {
                                s.IsEWSDocVerified = null;
                            }
                            else
                            {
                                s.IsEWSDocVerified = Convert.ToString(DR3["IsEWSDocVerified"].ToString());
                            }

                            var IsPCDocVerified = DR3["IsPCDocVerified"];
                            if (IsPCDocVerified is DBNull)
                            {
                                s.IsPCDocVerified = null;
                            }
                            else
                            {
                                s.IsPCDocVerified = Convert.ToString(DR3["IsPCDocVerified"].ToString());
                            }

                            var IsApplicantPhotoVerified = DR3["IsApplicantPhotoVerified"];
                            if (IsApplicantPhotoVerified is DBNull)
                            {
                                s.IsApplicantPhotoVerified = null;
                            }
                            else
                            {
                                s.IsApplicantPhotoVerified = Convert.ToString(DR3["IsApplicantPhotoVerified"].ToString());
                            }

                            var IsApplicantSignatureVerified = DR3["IsApplicantSignatureVerified"];
                            if (IsApplicantSignatureVerified is DBNull)
                            {
                                s.IsApplicantSignatureVerified = null;
                            }
                            else
                            {
                                s.IsApplicantSignatureVerified = Convert.ToString(DR3["IsApplicantSignatureVerified"].ToString());
                            }

                            vApplicantDetail1.objApplicant = s;
                        }
                    }
                }
            }
            return (s);
        }

        public List<Applicant> GetPersonalInfoForBulk(Int64 RegId, Int64 AppId, Int64 InstPartTermId)
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sda = new SqlDataAdapter();
            DataTable dt = new DataTable();
            ApplicantFormAllDocument vApplicantDetail1 = new ApplicantFormAllDocument();
            List<Applicant> sList = new List<Applicant>();
            cmd.Parameters.AddWithValue("@RegId", RegId);

            string serverUrl = "https://admission.msubaroda.ac.in/MSUISApi/Upload/";
            cmd.CommandText = "BulkPostAdmApplicantRegistrationGetData";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            //cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId",InstPartTermId);
            cmd.Parameters.AddWithValue("@Flag", "AdmApplicantRegistrationGetPersonalDetail");
            sda.SelectCommand = cmd;
            sda.Fill(dt);
            foreach (DataRow DR in dt.Rows)
            {
                Applicant s = new Applicant();
                s.FirstName = Convert.ToString(DR["FirstName"].ToString());
                s.MiddleName = Convert.ToString(DR["MiddleName"].ToString());
                s.LastName = Convert.ToString(DR["LastName"].ToString());
                s.Gender = Convert.ToString(DR["Gender"].ToString());
                if (DR["MobileNo"].ToString() != "")
                {
                    s.MobileNo = DR["MobileNo"].ToString();
                }
                if (DR["EmailId"].ToString() != "")
                {
                    s.EmailId = DR["EmailId"].ToString();
                }
                if (DR["DOBDoc"].ToString() != "")
                {
                    /* s.DOBDoc = DR["DOBDoc"].ToString();
                     string DobFilePath = serverUrl + "Documents/" + s.DOBDoc;
                     s.DOBDoc = DobFilePath;*/

                    string DobFilePath = serverUrl + "Documents/" + DR["DOBDoc"].ToString();
                    s.DOBDoc = DobFilePath;

                }
                if (DR["PhotoIdDoc"].ToString() != "")
                {
                    string PhotoIdFilePath = serverUrl + "Documents/" + DR["PhotoIdDoc"].ToString();
                    s.PhotoIdDoc = PhotoIdFilePath;
                }
                if (DR["AadharDoc"].ToString() != "")
                {
                    string AadharFilePath = serverUrl + "Documents/" + DR["AadharDoc"].ToString();
                    s.AadharDoc = AadharFilePath;
                }
                if (DR["ApplicantPhoto"].ToString() != "")
                {
                    string ApplicantPhotoFilePath = serverUrl + "Photo/" + DR["ApplicantPhoto"].ToString();
                    s.ApplicantPhoto = ApplicantPhotoFilePath;
                }
                if (DR["ApplicantSignature"].ToString() != "")
                {
                    string ApplicantSignatureFilePath = serverUrl + "Signature/" + DR["ApplicantSignature"].ToString();
                    s.ApplicantSignature = ApplicantSignatureFilePath;
                }

                if (DR["IsLocalToVadodara"].ToString() != "")
                {
                    s.IsLocalToVadodara = Convert.ToBoolean(DR["IsLocalToVadodara"].ToString());
                }
                if (DR["IsMajorThelesamiaStatus"].ToString() != "")
                {
                    s.IsMajorThelesamiaStatus = DR["IsMajorThelesamiaStatus"].ToString();
                }

                SqlCommand cmd2 = new SqlCommand();
                SqlDataAdapter sda2 = new SqlDataAdapter();
                DataTable dt2 = new DataTable();

                cmd2.Parameters.AddWithValue("@RegId", RegId);
                cmd2.CommandText = "BulkPostAdmApplicantRegistrationGetData";
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Connection = con;
                //cmd2.Parameters.AddWithValue("@ProgrammeInstancePartTermId", InstPartTermId);
                cmd2.Parameters.AddWithValue("@Flag", "AdmApplicantRegistrationGetResidentialAddress");

                sda2.SelectCommand = cmd2;
                sda2.Fill(dt2);

                foreach (DataRow DR2 in dt2.Rows)
                {

                    if (DR2["PermanentStateId"].ToString() != "")
                    {
                        s.PermanentStateId = Convert.ToInt64(DR2["PermanentStateId"].ToString());
                    }
                    if (DR2["PermanentState"].ToString() != "")
                    {
                        s.PermanentState = DR2["PermanentState"].ToString();
                    }
                    if (DR2["PermanentCityVillage"].ToString() != "")
                    {
                        s.PermanentCityVillage = DR2["PermanentCityVillage"].ToString();
                    }

                }

                SqlCommand cmd3 = new SqlCommand();
                SqlDataAdapter sda3 = new SqlDataAdapter();
                DataTable dt3 = new DataTable();

                cmd3.Parameters.AddWithValue("@RegId", RegId);
                cmd3.CommandText = "BulkPostAdmApplicantRegistrationGetData";
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Connection = con;
                //cmd3.Parameters.AddWithValue("@ProgrammeInstancePartTermId", InstPartTermId);
                cmd3.Parameters.AddWithValue("@Flag", "AdmApplicantRegistrationGetVerifiedStatus");

                sda3.SelectCommand = cmd3;
                sda3.Fill(dt3);

                foreach (DataRow DR3 in dt3.Rows)
                {
                    var IsDOBDocVerified = DR3["IsDOBDocVerified"];
                    if (IsDOBDocVerified is DBNull)
                    {
                        s.IsDOBDocVerified = null;
                    }
                    else
                    {
                        s.IsDOBDocVerified = Convert.ToString(DR3["IsDOBDocVerified"].ToString());
                    }

                    var IsPhotoIdDocVerified = DR3["IsPhotoIdDocVerified"];
                    if (IsPhotoIdDocVerified is DBNull)
                    {
                        s.IsPhotoIdDocVerified = null;
                    }
                    else
                    {
                        s.IsPhotoIdDocVerified = Convert.ToString(DR3["IsPhotoIdDocVerified"].ToString());
                    }

                    var IsAadharDocVerified = DR3["IsAadharDocVerified"];
                    if (IsAadharDocVerified is DBNull)
                    {
                        s.IsAadharDocVerified = null;
                    }
                    else
                    {
                        s.IsAadharDocVerified = Convert.ToString(DR3["IsAadharDocVerified"].ToString());
                    }

                    var IsSocialCategoryDocVerified = DR3["IsSocialCategoryDocVerified"];
                    if (IsSocialCategoryDocVerified is DBNull)
                    {
                        s.IsSocialCategoryDocVerified = null;
                    }
                    else
                    {
                        s.IsSocialCategoryDocVerified = Convert.ToString(DR3["IsSocialCategoryDocVerified"].ToString());
                    }

                    var IsReservationCategoryDocVerified = DR3["IsReservationCategoryDocVerified"];
                    if (IsReservationCategoryDocVerified is DBNull)
                    {
                        s.IsReservationCategoryDocVerified = null;
                    }
                    else
                    {
                        s.IsReservationCategoryDocVerified = Convert.ToString(DR3["IsReservationCategoryDocVerified"].ToString());
                    }

                    var IsEWSDocVerified = DR3["IsEWSDocVerified"];
                    if (IsEWSDocVerified is DBNull)
                    {
                        s.IsEWSDocVerified = null;
                    }
                    else
                    {
                        s.IsEWSDocVerified = Convert.ToString(DR3["IsEWSDocVerified"].ToString());
                    }

                    var IsPCDocVerified = DR3["IsPCDocVerified"];
                    if (IsPCDocVerified is DBNull)
                    {
                        s.IsPCDocVerified = null;
                    }
                    else
                    {
                        s.IsPCDocVerified = Convert.ToString(DR3["IsPCDocVerified"].ToString());
                    }

                    var IsApplicantPhotoVerified = DR3["IsApplicantPhotoVerified"];
                    if (IsApplicantPhotoVerified is DBNull)
                    {
                        s.IsApplicantPhotoVerified = null;
                    }
                    else
                    {
                        s.IsApplicantPhotoVerified = Convert.ToString(DR3["IsApplicantPhotoVerified"].ToString());
                    }

                    var IsApplicantSignatureVerified = DR3["IsApplicantSignatureVerified"];
                    if (IsApplicantSignatureVerified is DBNull)
                    {
                        s.IsApplicantSignatureVerified = null;
                    }
                    else
                    {
                        s.IsApplicantSignatureVerified = Convert.ToString(DR3["IsApplicantSignatureVerified"].ToString());
                    }

                    // vApplicantDetail1.objApplicant(s);
                }
                SqlCommand cmd1 = new SqlCommand();
                SqlDataAdapter sda1 = new SqlDataAdapter();
                DataTable dt1 = new DataTable();

                cmd1.Parameters.AddWithValue("@RegId", RegId);
                cmd1.Parameters.AddWithValue("@AppId", AppId);
                cmd1.CommandText = "BulkPostAdmApplicantRegistrationGetData";
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Connection = con;
                //cmd1.Parameters.AddWithValue("@ProgrammeInstancePartTermId", InstPartTermId);
                cmd1.Parameters.AddWithValue("@Flag", "AdmApplicantRegistrationGetSocialReservationDetail");
                
                sda1.SelectCommand = cmd1;
                sda1.Fill(dt1);
                foreach (DataRow DR1 in dt1.Rows)
                {

                    if (DR1["SocialCategoryId"].ToString() != "")
                    {
                        s.SocialCategoryId = Convert.ToInt64(DR1["SocialCategoryId"].ToString());
                    }
                    if (DR1["SocialCategoryName"].ToString() != "")
                    {
                        s.SocialCategoryName = DR1["SocialCategoryName"].ToString();
                    }
                    if (DR1["SocialCategoryCode"].ToString() != "")
                    {
                        s.SocialCategoryCode = DR1["SocialCategoryCode"].ToString();
                    }
                    if (DR1["ApplicationCategoryId"].ToString() != "")
                    {
                        s.ApplicationCategoryId = Convert.ToInt64(DR1["ApplicationCategoryId"].ToString());
                    }
                    if (DR1["ReservationCategory"].ToString() != "")
                    {
                        s.ReservationCategory = DR1["ReservationCategory"].ToString();
                    }
                    if (DR1["ReservationCategoryCode"].ToString() != "")
                    {
                        s.ReservationCategoryCode = DR1["ReservationCategoryCode"].ToString();
                    }
                    if (DR1["AppliedCategoryId"].ToString() != "")
                    {
                        s.AppliedCategoryId = Convert.ToInt64(DR1["AppliedCategoryId"].ToString());
                    }
                    if (DR1["AppliedCategory"].ToString() != "")
                    {
                        s.AppliedCategory = DR1["AppliedCategory"].ToString();
                    }

                    if (DR1["IsEWS"].ToString() != "")
                    {
                        s.IsEWS = DR1["IsEWS"].ToString();
                    }
                    if (DR1["IsPhysicallyChallenged"].ToString() != "")
                    {
                        s.IsPhysicallyChallenged = DR1["IsPhysicallyChallenged"].ToString();
                    }
                    if (DR1["EWSDoc"].ToString() != "" && DR1["EWSDoc"].ToString() != "Not Applicable")
                    {
                        string EWSDocFilePath = serverUrl + "Documents/" + DR1["EWSDoc"].ToString();
                        s.EWSDoc = EWSDocFilePath;
                    }
                    if (DR1["PCDoc"].ToString() != "" && DR1["PCDoc"].ToString() != "Not Applicable")
                    {
                        string PCDocFilePath = serverUrl + "Documents/" + DR1["PCDoc"].ToString();
                        s.PCDoc = PCDocFilePath;
                        //s.PCDoc = DR1["PCDoc"].ToString();
                    }
                    if (DR1["DisabilityPercentage"].ToString() != "")
                    {
                        s.DisabilityPercentage = Convert.ToInt64(DR1["DisabilityPercentage"].ToString());
                    }
                    if (DR1["DisabilityType"].ToString() != "")
                    {
                        s.DisabilityType = DR1["DisabilityType"].ToString();
                    }
                    if (DR1["IsSocialDocSubmitted"].ToString() != "")
                    {
                        s.IsSocialDocSubmitted = Convert.ToBoolean(DR1["IsSocialDocSubmitted"].ToString());
                    }
                    if (DR1["IsEWSDocSubmitted"].ToString() != "")
                    {
                        s.IsEWSDocSubmitted = Convert.ToBoolean(DR1["IsEWSDocSubmitted"].ToString());
                    }
                    if (DR1["IsPCDocSubmitted"].ToString() != "")
                    {
                        s.IsPCDocSubmitted = Convert.ToBoolean(DR1["IsPCDocSubmitted"].ToString());
                    }
                    if (DR1["IsReservationDocSubmitted"].ToString() != "")
                    {
                        s.IsReservationDocSubmitted = Convert.ToBoolean(DR1["IsReservationDocSubmitted"].ToString());
                    }
                    if (DR1["SocialCategoryDoc"].ToString() != "" && DR1["SocialCategoryDoc"].ToString() != "Not Applicable")
                    {
                        string SocialCategoryDocFilePath = serverUrl + "Documents/" + DR1["SocialCategoryDoc"].ToString();
                        s.SocialCategoryDoc = SocialCategoryDocFilePath;
                    }
                    if (DR1["ReservationCategoryDoc"].ToString() != "" && DR1["ReservationCategoryDoc"].ToString() != "Not Applicable")
                    {
                        string ReservationCategoryDocFilePath = serverUrl + "Documents/" + DR1["ReservationCategoryDoc"].ToString();
                        s.ReservationCategoryDoc = ReservationCategoryDocFilePath;
                    }
                    if (DR1["AppliedCategoryDocument"].ToString() != "")
                    {
                        s.AppliedCategoryDocument = DR1["AppliedCategoryDocument"].ToString();

                    }
                   
                }
                sList.Add(s);
            }

            return sList;
        }

        public static List<AdmApplicationEducationDetails> GetEducationDetails(Int64 UserName)
        {
            //ApplicantFormAllDocument vApplicantDetail1 = new ApplicantFormAllDocument();
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("PostAdmApplicationEducationDetailsGet", con);
            cmd.CommandType = CommandType.StoredProcedure;

            string serverUrl = "https://admission.msubaroda.ac.in/MSUISApi/Upload/";

            cmd.Parameters.AddWithValue("@Id", UserName);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            List<AdmApplicationEducationDetails> ObjLstedudetail = new List<AdmApplicationEducationDetails>();
            //TestModel testmodel = new TestModel();
            //testmodel.Test = "No Data Found";
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {

                    AdmApplicationEducationDetails aed = new AdmApplicationEducationDetails();

                    aed.Id = Convert.ToInt32(dr["Id"].ToString());
                    aed.EligibleDegreeId = Convert.ToInt32(dr["EligibleDegreeId"].ToString());
                    aed.EligibleDegreeName = dr["EligibleDegreeName"].ToString();
                    aed.SpecializationId = Convert.ToInt32(dr["SpecializationId"].ToString());
                    aed.SpecializationName = dr["SpecializationName"].ToString();
                    aed.ExaminationBodyId = Convert.ToInt32(dr["ExaminationBodyId"].ToString());
                    aed.ExaminationBodyName = dr["ExaminationBodyName"].ToString();
                    aed.InstituteAttended = dr["InstituteAttended"].ToString();
                    aed.InstituteCityId = Convert.ToInt32(dr["InstituteCityId"].ToString());
                    aed.CityName = dr["CityName"].ToString();
                    aed.ExamPassMonth = dr["ExamPassMonth"].ToString();
                    aed.ExamPassYear = dr["ExamPassYear"].ToString();
                    aed.ExamSeatNumber = dr["ExamSeatNumber"].ToString();
                    aed.ExamCertificateNumber = dr["ExamCertificateNumber"].ToString();
                    if (dr["MarkObtained"].ToString() != "")
                    {
                        aed.MarkObtained = Convert.ToInt32(dr["MarkObtained"].ToString());
                    }
                    if (dr["MarkOutof"].ToString() != "")
                    {
                        aed.MarkOutof = Convert.ToInt32(dr["MarkOutof"].ToString());
                    }
                    aed.Grade = dr["Grade"].ToString();
                    if (dr["CGPA"].ToString() != "")
                    {
                        aed.CGPA = Convert.ToDecimal(dr["CGPA"].ToString());
                    }
                    if (dr["PercentageEquivalenceCGPA"].ToString() != "")
                    {
                        aed.PercentageEquivalenceCGPA = Convert.ToDecimal(dr["PercentageEquivalenceCGPA"].ToString());
                    }
                    if (dr["Percentage"].ToString() != "")
                    {
                        aed.Percentage = Convert.ToDecimal(dr["Percentage"].ToString());
                    }
                    if (dr["ClassId"].ToString() != "")
                    {
                        aed.ClassId = Convert.ToInt32(dr["ClassId"].ToString());
                    }
                    aed.ClassName = dr["ClassName"].ToString();
                    if (dr["IsFirstTrial"].ToString() != "")
                    {
                        aed.IsFirstTrial = Convert.ToBoolean(dr["IsFirstTrial"].ToString());
                    }
                    if (dr["IsLastQualifyingExam"].ToString() != "")
                    {
                        aed.IsLastQualifyingExam = Convert.ToBoolean(dr["IsLastQualifyingExam"].ToString());
                    }
                    if (dr["TeachingLanguageId"].ToString() != "")
                    {
                        aed.TeachingLanguageId = Convert.ToInt32(dr["TeachingLanguageId"].ToString());
                    }
                    aed.LanguageName = dr["LanguageName"].ToString();
                    aed.ResultStatus = dr["ResultStatus"].ToString();

                    if (dr["AttachDocument"].ToString() != "")
                    {
                        string EduDocFilePath = serverUrl + "Documents/" + dr["AttachDocument"].ToString();
                        aed.AttachDocument = EduDocFilePath;
                    }

                    aed.OtherCity = dr["OtherCity"].ToString();
                    if (dr["IsDeclared"].ToString() != "")
                    {
                        aed.IsDeclared = Convert.ToBoolean(dr["IsDeclared"].ToString());
                    }

                    var IsVerified = dr["IsVerified"];
                    if (IsVerified is DBNull)
                    {
                        aed.IsVerified = null;
                    }
                    else
                    {
                        aed.IsVerified = Convert.ToString(dr["IsVerified"].ToString());
                    }

                    ObjLstedudetail.Add(aed);


                }
                //obj.objEduLst = ObjLstedudetail;

            }
            return (ObjLstedudetail);
        }

        public static List<AdmApplicationEducationDetails> GetEducationDetailsForBulk(Int64 UserName)
        {
            //ApplicantFormAllDocument vApplicantDetail1 = new ApplicantFormAllDocument();
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("BulkPostAdmApplicationEducationDetailsGet", con);
            cmd.CommandType = CommandType.StoredProcedure;

            string serverUrl = "https://admission.msubaroda.ac.in/MSUISApi/Upload/";

            cmd.Parameters.AddWithValue("@Id", UserName);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            List<AdmApplicationEducationDetails> ObjLstedudetail = new List<AdmApplicationEducationDetails>();
            //TestModel testmodel = new TestModel();
            //testmodel.Test = "No Data Found";
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {

                    AdmApplicationEducationDetails aed = new AdmApplicationEducationDetails();

                    aed.Id = Convert.ToInt32(dr["Id"].ToString());
                    aed.EligibleDegreeId = Convert.ToInt32(dr["EligibleDegreeId"].ToString());
                    aed.EligibleDegreeName = dr["EligibleDegreeName"].ToString();
                    aed.SpecializationId = Convert.ToInt32(dr["SpecializationId"].ToString());
                    aed.SpecializationName = dr["SpecializationName"].ToString();
                    aed.ExaminationBodyId = Convert.ToInt32(dr["ExaminationBodyId"].ToString());
                    aed.ExaminationBodyName = dr["ExaminationBodyName"].ToString();
                    aed.InstituteAttended = dr["InstituteAttended"].ToString();
                    aed.InstituteCityId = Convert.ToInt32(dr["InstituteCityId"].ToString());
                    aed.CityName = dr["CityName"].ToString();
                    aed.ExamPassMonth = dr["ExamPassMonth"].ToString();
                    aed.ExamPassYear = dr["ExamPassYear"].ToString();
                    aed.ExamSeatNumber = dr["ExamSeatNumber"].ToString();
                    aed.ExamCertificateNumber = dr["ExamCertificateNumber"].ToString();
                    if (dr["MarkObtained"].ToString() != "")
                    {
                        aed.MarkObtained = Convert.ToInt32(dr["MarkObtained"].ToString());
                    }
                    if (dr["MarkOutof"].ToString() != "")
                    {
                        aed.MarkOutof = Convert.ToInt32(dr["MarkOutof"].ToString());
                    }
                    aed.Grade = dr["Grade"].ToString();
                    if (dr["CGPA"].ToString() != "")
                    {
                        aed.CGPA = Convert.ToDecimal(dr["CGPA"].ToString());
                    }
                    if (dr["PercentageEquivalenceCGPA"].ToString() != "")
                    {
                        aed.PercentageEquivalenceCGPA = Convert.ToDecimal(dr["PercentageEquivalenceCGPA"].ToString());
                    }
                    if (dr["Percentage"].ToString() != "")
                    {
                        aed.Percentage = Convert.ToDecimal(dr["Percentage"].ToString());
                    }
                    if (dr["ClassId"].ToString() != "")
                    {
                        aed.ClassId = Convert.ToInt32(dr["ClassId"].ToString());
                    }
                    aed.ClassName = dr["ClassName"].ToString();
                    if (dr["IsFirstTrial"].ToString() != "")
                    {
                        aed.IsFirstTrial = Convert.ToBoolean(dr["IsFirstTrial"].ToString());
                    }
                    if (dr["IsLastQualifyingExam"].ToString() != "")
                    {
                        aed.IsLastQualifyingExam = Convert.ToBoolean(dr["IsLastQualifyingExam"].ToString());
                    }
                    if (dr["TeachingLanguageId"].ToString() != "")
                    {
                        aed.TeachingLanguageId = Convert.ToInt32(dr["TeachingLanguageId"].ToString());
                    }
                    aed.LanguageName = dr["LanguageName"].ToString();
                    aed.ResultStatus = dr["ResultStatus"].ToString();

                    if (dr["AttachDocument"].ToString() != "")
                    {
                        string EduDocFilePath = serverUrl + "Documents/" + dr["AttachDocument"].ToString();
                        aed.AttachDocument = EduDocFilePath;
                    }

                    aed.OtherCity = dr["OtherCity"].ToString();
                    if (dr["IsDeclared"].ToString() != "")
                    {
                        aed.IsDeclared = Convert.ToBoolean(dr["IsDeclared"].ToString());
                    }

                    var IsVerified = dr["IsVerified"];
                    if (IsVerified is DBNull)
                    {
                        aed.IsVerified = null;
                    }
                    else
                    {
                        aed.IsVerified = Convert.ToString(dr["IsVerified"].ToString());
                    }

                    ObjLstedudetail.Add(aed);


                }
                //obj.objEduLst = ObjLstedudetail;

            }
            return (ObjLstedudetail);
        }

        public static List<AdmApplicantsSubmittedDocuments> GetSubmittedDocuments(Int64 AppId,Int32 AcademicYearId)
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("BulkPostAdmApplicantsSubmittedDocumentsGet", con);
            cmd.CommandType = CommandType.StoredProcedure;
            string serverUrl = "https://admission.msubaroda.ac.in/MSUISApi/Upload/";
            cmd.Parameters.AddWithValue("@AdmissionApplicationId", AppId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@Flag", "AdmApplicantsSubmittedDocumentsGetById");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            List<AdmApplicantsSubmittedDocuments> ObjLstSD = new List<AdmApplicantsSubmittedDocuments>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AdmApplicantsSubmittedDocuments ObjSubDocs = new AdmApplicantsSubmittedDocuments();

                    if (dr["Id"].ToString() != "")
                    {
                        ObjSubDocs.Id = Convert.ToInt32(dr["Id"].ToString());
                    }
                    ObjSubDocs.NameOfTheDocument = (dr["NameOfTheDocument"].ToString());
                    ObjSubDocs.FileNameofDocumnetByUser = (dr["FileNameofDocumnetByUser"].ToString());
                    var IsCompulsoryDocument = dr["IsCompulsoryDocument"];
                    if (IsCompulsoryDocument is DBNull)
                    {
                        ObjSubDocs.IsCompulsoryDocument = null;
                    }
                    else
                    {
                        ObjSubDocs.IsCompulsoryDocument = Convert.ToString(dr["IsCompulsoryDocument"].ToString());
                    }
                    //ObjSubDocs.FileNameofDocumnetBySystem = (dr["FileNameofDocumnetBySystem"].ToString());

                    string SubmittedDocFilePath = serverUrl + "RequiredDocuments/" + dr["FileNameofDocumnetBySystem"].ToString();
                    ObjSubDocs.FileNameofDocumnetBySystem = SubmittedDocFilePath;


                    //ObjSubDocs.IsVerified = dr["IsVerified"].ToString();
                    //if (dr["IsVerified"].ToString() == "")
                    //{
                    //    ObjSubDocs.IsVerified = "False";
                    //}

                    var IsVerified = dr["IsVerified"];
                    if (IsVerified is DBNull)
                    {
                        ObjSubDocs.IsVerified = null;
                    }
                    else
                    {
                        ObjSubDocs.IsVerified = Convert.ToString(dr["IsVerified"].ToString());
                    }

                    ObjLstSD.Add(ObjSubDocs);

                }
                //obj.objSubDocsLst = ObjLstSD;
            }
            return ObjLstSD;
        }

        public static List<AdmApplicantAdditionalDocument> GetAdditionalDocuments(Int64 AppId)
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("PostAdmApplicantAdditionalDocumentGet", con);
            cmd.CommandType = CommandType.StoredProcedure;
            string serverUrl = "https://admission.msubaroda.ac.in/MSUISApi/Upload/";
            cmd.Parameters.AddWithValue("@AdmApplicationId", AppId);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);


            List<AdmApplicantAdditionalDocument> ObjLstAddDocument = new List<AdmApplicantAdditionalDocument>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AdmApplicantAdditionalDocument ObjAddDocument = new AdmApplicantAdditionalDocument();
                    ObjAddDocument.Id = Convert.ToInt32(dr["Id"].ToString());
                    ObjAddDocument.DocName = dr["DocName"].ToString();
                    //ObjAddDocument.DocFileName = dr["DocFileName"].ToString();

                    string AdditionalDocFilePath = serverUrl + "AdditionalDocuments/" + dr["DocFileName"].ToString();
                    ObjAddDocument.DocFileName = AdditionalDocFilePath;

                    //ObjAddDocument.IsVerified = dr["IsVerified"].ToString();
                    //if (dr["IsVerified"].ToString() == "")
                    //{
                    //    ObjAddDocument.IsVerified = "False";
                    //}

                    var IsVerified = dr["IsVerified"];
                    if (IsVerified is DBNull)
                    {
                        ObjAddDocument.IsVerified = null;
                    }
                    else
                    {
                        ObjAddDocument.IsVerified = Convert.ToString(dr["IsVerified"].ToString());
                    }

                    ObjLstAddDocument.Add(ObjAddDocument);
                }
                // obj.objAdditionalDocsLst = ObjLstAddDocument;

            }

            return ObjLstAddDocument;

        }

        public static List<AdmStudentAddOnInformation> GetAddOnDetails(Int64 AppId)
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("BulkPostAdmStudentAddOnInformationGet", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AdmApplicationId", AppId);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);


            List<AdmStudentAddOnInformation> ObjLstAddOnDetails = new List<AdmStudentAddOnInformation>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AdmStudentAddOnInformation ObjAddOn = new AdmStudentAddOnInformation();
                    if (dr["Id"].ToString() != "")
                    {
                        ObjAddOn.Id = Convert.ToInt32(dr["Id"].ToString());
                    }
                    ObjAddOn.TitleName = dr["TitleName"].ToString();
                    ObjAddOn.AddOnValue = dr["AddOnValue"].ToString();
                    //ObjAddOn.IsVerified = (dr["IsVerified"].ToString());
                    //if (dr["IsVerified"].ToString() == "")
                    //{
                    //    ObjAddOn.IsVerified = "False";
                    //}

                    var IsVerified = dr["IsVerified"];
                    if (IsVerified is DBNull)
                    {
                        ObjAddOn.IsVerified = null;
                    }
                    else
                    {
                        ObjAddOn.IsVerified = Convert.ToString(dr["IsVerified"].ToString());
                    }

                    ObjLstAddOnDetails.Add(ObjAddOn);
                }
                //obj.objAddOnDocsLst = ObjLstAddOnDetails;

            }

            return ObjLstAddOnDetails;

        }

        public static List<PostApplicantVerification> GetFeeAttached(Int64 AppId, Int64 InstPartTermId, string EligibilityStatus)
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("PostAdmApplicationFinalVerifyGet", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AdmApplicationId", AppId);
            cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", InstPartTermId);
            cmd.Parameters.AddWithValue("@EligibilityStatus", EligibilityStatus);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            ApplicantFormAllDocument vApplicantDetail1 = new ApplicantFormAllDocument();
            List<PostApplicantVerification> av = new List<PostApplicantVerification>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    PostApplicantVerification av1 = new PostApplicantVerification();

                    if (dr["FeeCategoryId"].ToString() != "")
                    {
                        av1.FeeCategoryId = Convert.ToInt32(dr["FeeCategoryId"].ToString());
                    }
                    if (dr["ProgrammeInstancePartTermId"].ToString() != "")
                    {
                        av1.ProgrammeInstancePartTermId = Convert.ToInt32(dr["ProgrammeInstancePartTermId"].ToString());
                    }


                    if (dr["FeeCategoryName"].ToString() != "")
                    {
                        av1.FeeCategoryName = (dr["FeeCategoryName"].ToString());
                    }
                    av1.EligibilityStatus = (dr["EligibilityStatus"].ToString());
                    //av1.AdminRemarkByFaculty = Convert.ToString(dr["AdminRemarkByFaculty"].ToString());
                    if (dr["AdminRemarkByFaculty"].ToString() != "")
                    {
                        av1.AdminRemarkByFaculty = dr["AdminRemarkByFaculty"].ToString();
                    }
                    if (dr["Id"].ToString() != "")
                    {
                        av1.FeeCategoryPartTermMapId = Convert.ToInt64(dr["Id"].ToString());
                    }
                    av1.ApplicationIDStatus = (dr["ApplicationID"].ToString());
                    av.Add(av1);
                    //vApplicantDetail1.objFee = av;

                }
                //vApplicantDetail1.objFee = av;
            }
            return (av);

        }

        public static PostApplicantVerification GetAdmApplicationInfo(Int64 AppId)
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("PostAdmApplicationInfoGet", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", AppId);
            //cmd.Parameters.AddWithValue("@FeeCategoryPartTermMapId", InstPartTermId);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            ApplicantFormAllDocument vApplicantDetail1 = new ApplicantFormAllDocument();
            PostApplicantVerification av = new PostApplicantVerification();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    //PostApplicantVerification av1 = new PostApplicantVerification();

                    av.Id = Convert.ToInt64(dr["Id"].ToString());
                    //av.AdmittedInstituteId = Convert.ToInt64(dr["AdmittedInstituteId"].ToString());
                    if (dr["AdmittedInstituteId"].ToString() != "")
                    {
                        av.AdmittedInstituteId = Convert.ToInt64(dr["AdmittedInstituteId"].ToString());
                    }
                    if (dr["EligibilityStatus"].ToString() != "")
                    {
                        av.EligibilityStatus = Convert.ToString(dr["EligibilityStatus"].ToString());
                    }
                    if (dr["FeeCategoryPartTermMapId"].ToString() != "")
                    {
                        av.FeeCategoryPartTermMapId = Convert.ToInt64(dr["FeeCategoryPartTermMapId"].ToString());
                    }
                    if (dr["PreferenceGroupId"].ToString() != "")
                    {
                        av.PreferenceGroupId = Convert.ToInt32(dr["PreferenceGroupId"].ToString());
                    }
                    if (dr["DestinationIncProgInstPartTermId"].ToString() != "")
                    {
                        av.DestinationIncProgInstPartTermId = Convert.ToInt64(dr["DestinationIncProgInstPartTermId"].ToString());
                    }

                    if (dr["IsDualAdmission"].ToString() != "")
                    {
                        av.IsDualAdmission = Convert.ToBoolean(dr["IsDualAdmission"].ToString());
                    }
                    if (dr["MainCategoryRemarks"].ToString() != "")
                    {
                        av.MainCategoryRemarks = Convert.ToString(dr["MainCategoryRemarks"].ToString());
                    }

                    ////if (dr["AppliedCategoryRemarks"].ToString() != "")
                    ////{
                    ////    av.AppliedCategoryRemarks = Convert.ToString(dr["AppliedCategoryRemarks"].ToString());
                    ////}

                    av.EduVerification = Convert.ToString(dr["EduVerification"].ToString());
                    if (dr["EduVerification"].ToString() == "")
                    {
                        av.EduVerification = "False";
                    }

                    //av.CategoryVerification = Convert.ToString(dr["CategoryVerification"].ToString());
                    //if (dr["CategoryVerification"].ToString() == "")
                    //{
                    //    av.CategoryVerification = "False";
                    //}

                    //var CategoryVerification = dr["CategoryVerification"];
                    //if (CategoryVerification is DBNull)
                    //{
                    //    av.CategoryVerification = null;
                    //}
                    //else
                    //{
                    //    av.CategoryVerification = Convert.ToString(dr["CategoryVerification"].ToString());
                    //}

                    //av.DisabilityVerification = Convert.ToString(dr["DisabilityVerification"].ToString());
                    //if (dr["DisabilityVerification"].ToString() == "")
                    //{
                    //    av.DisabilityVerification = "False";
                    //}

                    //av.AppliedCategoryVerification = Convert.ToString(dr["AppliedCategoryVerification"].ToString());
                    //if (dr["AppliedCategoryVerification"].ToString() == "")
                    //{
                    //    av.AppliedCategoryVerification = "False";
                    //}

                    vApplicantDetail1.objAdmApplicationinfo = av;

                }
                //vApplicantDetail1.objFee = av;
            }
            return (av);

        }

        public static List<ApplicationInstitutePreferenceReport> GetGroupPreference(Int64 AppId)
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("PreferenceGroupGetByApplicationId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ApplicationId", AppId);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            List<ApplicationInstitutePreferenceReport> ObjLstGroupDetails = new List<ApplicationInstitutePreferenceReport>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ApplicationInstitutePreferenceReport ObjGroup = new ApplicationInstitutePreferenceReport();

                    ObjGroup.GroupName = dr["GroupName"].ToString();
                    ObjGroup.PreferenceNo = Convert.ToInt32(dr["PreferenceNo"].ToString());

                    ObjLstGroupDetails.Add(ObjGroup);
                }
                //obj.objAddOnDocsLst = ObjLstAddOnDetails;

            }

            return ObjLstGroupDetails;

        }

        public static List<ApplicationInstitutePreferenceReport> GetInstitutePreference(Int64 AppId)
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("InstituteGroupGetByApplicationId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ApplicationId", AppId);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            List<ApplicationInstitutePreferenceReport> ObjLstInstituteDetails = new List<ApplicationInstitutePreferenceReport>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ApplicationInstitutePreferenceReport ObjInstitute = new ApplicationInstitutePreferenceReport();

                    ObjInstitute.InstituteName = dr["InstituteName"].ToString();
                    ObjInstitute.PreferenceNo = Convert.ToInt32(dr["PreferenceNo"].ToString());

                    ObjLstInstituteDetails.Add(ObjInstitute);
                }
                //obj.objAddOnDocsLst = ObjLstAddOnDetails;

            }

            return ObjLstInstituteDetails;

        }

        public static List<ProfileForm> BulkFeePayCourseList(Int64 AppId)
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("FeePayCourseGet", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AdmissionApplicationId", AppId);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            List<ProfileForm> feepaylist = new List<ProfileForm>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ProfileForm feepay = new ProfileForm();
                    //feepay.Id = Convert.ToInt32(responseToken);
                    //feepay.FeePayCourseId = Convert.ToInt32(responseToken);
                    feepay.AdmissionApplicationId = Convert.ToInt64(dr["AdmissionApplicationId"].ToString());
                    feepay.AdmApplicationCreatedOn = string.Format("{0: dd/MM/yyyy}", dr["AdmApplicationCreatedOn"]);
                    feepay.FacultyName = dr["FacultyName"].ToString();
                    feepay.FacultyAddress = dr["FacultyAddress"].ToString();
                    feepay.AcademicYearCode = dr["AcademicYearCode"].ToString();
                    feepay.Amount = dr["Amount"].ToString();
                    feepay.FeeTypeName = dr["FeeTypeName"].ToString();
                    feepay.PartTermName = dr["PartTermName"].ToString();
                    feepay.PartName = dr["PartName"].ToString();
                    feepay.ProgrammeName = dr["ProgrammeName"].ToString();
                    feepay.BranchName = dr["BranchName"].ToString();
                    feepay.TransectionId = dr["TransactionId"].ToString();
                    feepay.TransectionDate = string.Format("{0: dd/MM/yyyy}", dr["TransactionDate"]);
                    feepay.FeeCategoryName = dr["FeeCategoryName"].ToString();
                    feepay.AllotmentNo = dr["AllotmentNo"].ToString();

                    feepaylist.Add(feepay);
                }
                //obj.objAddOnDocsLst = ObjLstAddOnDetails;

            }

            return feepaylist;

        }

        public static List<ProfileForm> BulkAdmApplicantRegistrationList(Int64 AppId,Int64 PRN)
        {

            string serverUrl = "https://admission.msubaroda.ac.in/MSUISApi/Upload/";
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("BulkProfileGetForDownloadApplicationForm", con);
            
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ApplicationId", AppId);
            cmd.Parameters.AddWithValue("@UserName", PRN);
            DataSet ds = new DataSet();
            List<ProfileForm> slist = new List<ProfileForm>();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            foreach (DataRow DR in dt.Rows)
            {
                ProfileForm s = new ProfileForm();

                //s.Id = Convert.ToInt64(DR["Id"].ToString());
                
                s.Id = Convert.ToInt64(PRN);
                //s.UserName = Convert.ToInt64(DR["UserName"].ToString());
                //s.Password = DR["Password"].ToString();
                s.LastName = DR["LastName"].ToString();
                s.FirstName = DR["FirstName"].ToString();
                s.MiddleName = DR["MiddleName"].ToString();
                s.Gender = DR["Gender"].ToString();
                //s.ReligionId = Convert.ToInt64(DR["ReligionId"].ToString());

                if (DR["ReligionId"].ToString() != "")
                {
                    s.ReligionId = Convert.ToInt64(DR["ReligionId"].ToString());
                }
                else
                {
                    s.ReligionId = 0;
                }

                s.ReligionName = DR["ReligionName"].ToString();
                if (DR["MaritalStatus"].ToString() != "")
                {
                    s.MaritalStatus = Convert.ToString(DR["MaritalStatus"].ToString());
                }
                else
                {
                    s.MaritalStatus = "--";
                }
                //s.MotherTongueId = Convert.ToInt64(DR["MotherTongueId"].ToString());
                if (DR["MotherTongueId"].ToString() != "")
                {
                    s.MotherTongueId = Convert.ToInt64(DR["MotherTongueId"].ToString());
                }
                else
                {
                    s.MotherTongueId = 0;
                }
                s.MotherTongueName = DR["MotherTongueName"].ToString();
                //s.CommunicationLanguageId = Convert.ToInt64(DR["CommunicationLanguageId"].ToString());
                s.NameAsPerMarksheet = DR["NameAsPerMarksheet"].ToString();
                //s.DOB = DR["DOB"].ToString();
                if (DR["DOB"].ToString() != "")
                {
                    s.DOB = string.Format("{0: dd/MM/yyyy}", DR["DOB"]);
                }

                if (DR["BloodGroupId"].ToString() != "")
                {
                    s.BloodGroupId = Convert.ToInt64(DR["BloodGroupId"].ToString());
                }
                else
                {
                    s.BloodGroupId = 0;
                }
                if (DR["BloodGroupName"].ToString() != "")
                {
                    s.BloodGroupName = DR["BloodGroupName"].ToString();

                }
                else
                {
                    s.BloodGroupName = "--";
                }

                if (DR["HeightInCms"].ToString() != "")
                {
                    s.HeightInCms = Convert.ToDecimal(DR["HeightInCms"].ToString());
                }
                else
                {
                    s.HeightInCms = 0;
                }

                if (DR["WeightInKgs"].ToString() != "")
                {
                    s.WeightInKgs = Convert.ToDecimal(DR["WeightInKgs"].ToString());
                }
                else
                {
                    s.WeightInKgs = 0;
                }

                s.IsMajorThelesamiaStatus = Convert.ToString(DR["IsMajorThelesamiaStatus"].ToString());
                s.IsNRI = DR["IsNRI"].ToString();
                //s.CountryIdOfCitizenship = Convert.ToInt64(DR["CountryIdOfCitizenship"].ToString());
                if (DR["CountryIdOfCitizenship"].ToString() != "")
                {
                    s.CountryIdOfCitizenship = Convert.ToInt64(DR["CountryIdOfCitizenship"].ToString());
                }
                else
                {
                    s.CountryIdOfCitizenship = 0;
                }
                s.CitizenCountry = DR["CitizenCountry"].ToString();

                if (DR["PassportNumber"].ToString() != "")
                {
                    s.PassportNumber = DR["PassportNumber"].ToString();

                }
                else
                {
                    s.PassportNumber = "--";
                }

                if (DR["AadharNumber"].ToString() != "")
                {
                    s.AadharNumber = DR["AadharNumber"].ToString();
                }


                if (DR["NameOnAadhar"].ToString() != "")
                {
                    s.NameOnAadhar = DR["NameOnAadhar"].ToString();
                }
                else
                {
                    s.NameOnAadhar = "--";
                }

                s.PermanentAddress = DR["PermanentAddress"].ToString();
                //s.PermanentCountryId = Convert.ToInt64(DR["PermanentCountryId"].ToString());
                if (DR["PermanentCountryId"].ToString() != "")
                {
                    s.PermanentCountryId = Convert.ToInt64(DR["PermanentCountryId"].ToString());
                }
                else
                {
                    s.PermanentCountryId = 0;
                }
                s.PermanentCountry = DR["PermanentCountry"].ToString();
                //s.PermanentStateId = Convert.ToInt64(DR["PermanentStateId"].ToString());
                if (DR["PermanentStateId"].ToString() != "")
                {
                    s.PermanentStateId = Convert.ToInt64(DR["PermanentStateId"].ToString());
                }
                else
                {
                    s.PermanentStateId = 0;
                }
                s.PermanentState = DR["PermanentState"].ToString();
                //s.PermanentDistrictId = Convert.ToInt64(DR["PermanentDistrictId"].ToString());
                if (DR["PermanentDistrictId"].ToString() != "")
                {
                    s.PermanentDistrictId = Convert.ToInt64(DR["PermanentDistrictId"].ToString());
                }
                else
                {
                    s.PermanentDistrictId = 0;
                }
                s.PermanentDistrict = DR["PermanentDistrict"].ToString();
                s.PermanentCityVillage = DR["PermanentCityVillage"].ToString();
                //s.PermanentPincode = Convert.ToInt64(DR["PermanentPincode"].ToString());
                if (DR["PermanentPincode"].ToString() != "")
                {
                    s.PermanentPincode = Convert.ToInt64(DR["PermanentPincode"].ToString());
                }
                else
                {
                    s.PermanentPincode = 0;
                }

                if (DR["CurrentAddress"].ToString() != "")
                {
                    s.CurrentAddress = DR["CurrentAddress"].ToString();
                }
                else
                {
                    s.CurrentAddress = "--";
                }

                if (DR["CurrentCountryId"].ToString() != "")
                {
                    s.CurrentCountryId = Convert.ToInt64(DR["CurrentCountryId"].ToString());
                }
                else
                {
                    s.CurrentCountryId = 0;
                }
                s.CurrentCountry = DR["CurrentCountry"].ToString();

                if (DR["CurrentStateId"].ToString() != "")
                {
                    s.CurrentStateId = Convert.ToInt64(DR["CurrentStateId"].ToString());
                }
                else
                {
                    s.CurrentStateId = 0;
                }
                s.CurrentState = DR["CurrentState"].ToString();

                if (DR["CurrentDistrictId"].ToString() != "")
                {
                    s.CurrentDistrictId = Convert.ToInt64(DR["CurrentDistrictId"].ToString());
                }
                else
                {
                    s.CurrentDistrictId = 0;
                }
                s.CurrentDistrict = DR["CurrentDistrict"].ToString();

                if (DR["CurrentCityVillage"].ToString() != "")
                {
                    s.CurrentCityVillage = DR["CurrentCityVillage"].ToString();
                }
                else
                {
                    s.CurrentCityVillage = "--";
                }


                if (DR["CurrentPincode"].ToString() != "")
                {
                    s.CurrentPincode = Convert.ToInt64(DR["CurrentPincode"].ToString());
                }
                else
                {
                    s.CurrentPincode = 0;
                }

                if (DR["NameOfFather"].ToString() != "")
                {
                    s.NameOfFather = DR["NameOfFather"].ToString();
                }
                else
                {
                    s.NameOfFather = "--";
                }

                if (DR["NameOfMother"].ToString() != "")
                {
                    s.NameOfMother = DR["NameOfMother"].ToString();
                }
                else
                {
                    s.NameOfMother = "--";
                }


                if (DR["SocialCategoryId"].ToString() != "")
                {
                    s.SocialCategoryId = Convert.ToInt64(DR["SocialCategoryId"].ToString());
                }

                s.SocialCategoryName = DR["SocialCategoryName"].ToString();
                //s.GSocialCategoryId = Convert.ToInt64(DR["GSocialCategoryId"].ToString());
                if (DR["GSocialCategoryId"].ToString() != "")
                {
                    s.GSocialCategoryId = Convert.ToInt64(DR["GSocialCategoryId"].ToString());
                }
                else
                {
                    s.GSocialCategoryId = 0;
                }

                s.GSocialCategory = DR["GSocialCategory"].ToString();

                if (DR["ReservationCategory"].ToString() != "")
                {
                    s.ReservationCategory = DR["ReservationCategory"].ToString();
                }

                if (DR["GuardianName"].ToString() != "")
                {
                    s.GuardianName = DR["GuardianName"].ToString();
                }
                else
                {
                    s.GuardianName = "--";
                }

                if (DR["GuardianContactNo"].ToString() != "")
                {
                    s.GuardianContactNo = DR["GuardianContactNo"].ToString();
                }
                else
                {
                    s.GuardianContactNo = "--";
                }

                if (DR["FamilyAnnualIncome"].ToString() != "")
                {
                    s.FamilyAnnualIncome = Convert.ToInt64(DR["FamilyAnnualIncome"].ToString());
                }
                else
                {
                    s.FamilyAnnualIncome = 0;
                }

                if (DR["OccupationIdOfFather"].ToString() != "")
                {
                    s.OccupationIdOfFather = Convert.ToInt64(DR["OccupationIdOfFather"].ToString());
                }
                else
                {
                    s.OccupationIdOfFather = 0;
                }
                s.OccupationOfFather = DR["OccupationOfFather"].ToString();

                if (DR["OccupationIdOfMother"].ToString() != "")
                {
                    s.OccupationIdOfMother = Convert.ToInt64(DR["OccupationIdOfMother"].ToString());
                }
                else
                {
                    s.OccupationIdOfMother = 0;
                }
                s.OccupationOfMother = DR["OccupationOfMother"].ToString();

                if (DR["OccupationIdOfGuardian"].ToString() != "")
                {
                    s.OccupationIdOfGuardian = Convert.ToInt64(DR["OccupationIdOfGuardian"].ToString());
                }
                else
                {
                    s.OccupationIdOfGuardian = 0;
                }
                s.OccupationOfGuardian = DR["OccupationOfGuardian"].ToString();

                if (!string.IsNullOrEmpty(DR["IsEmp"].ToString()) && !string.IsNullOrWhiteSpace(DR["IsEmp"].ToString()) &&
DR["IsEmp"].ToString() != null && DR["IsEmp"].ToString() != "")
                {
                    if (Convert.ToBoolean(DR["IsEmp"].ToString()) == true)
                    {
                        s.IsSelfEmpDisplay = "Yes";
                    }
                    else
                    {
                        s.IsSelfEmpDisplay = "No";
                    }
                    // s.IsSelfEmp = Convert.ToBoolean(DR["IsEmp"].ToString());
                }
                else
                {
                    s.IsSelfEmpDisplay = "No";
                }

                if (!string.IsNullOrEmpty(DR["IsGuardianEbc"].ToString()) && !string.IsNullOrWhiteSpace(DR["IsGuardianEbc"].ToString()) &&
DR["IsGuardianEbc"].ToString() != null && DR["IsGuardianEbc"].ToString() != "")
                {
                    if (Convert.ToBoolean(DR["IsGuardianEbc"].ToString()) == true)
                    {
                        s.IsEbcDisplay = "Yes";
                    }
                    else
                    {
                        s.IsEbcDisplay = "No";
                    }
                    //s.IsEbc = Convert.ToBoolean(DR["IsGuardianEbc"].ToString());
                }
                else
                {
                    s.IsEbcDisplay = "No";
                }

                if (DR["GuardianAnnualIncome"].ToString() != "")
                {
                    s.GuardianAnnualIncome = Convert.ToInt64(DR["GuardianAnnualIncome"].ToString());
                }
                else
                {
                    s.GuardianAnnualIncome = 0;
                }

                s.EmailId = DR["EmailId"].ToString();
                s.MobileNo = DR["MobileNo"].ToString();

                if (DR["OptionalMobileNo"].ToString() != "")
                {
                    s.OptionalMobileNo = DR["OptionalMobileNo"].ToString();
                }
                else
                {
                    s.OptionalMobileNo = "--";
                }

                if (!string.IsNullOrEmpty(DR["IsSmsPermissionGiven"].ToString()) && !string.IsNullOrWhiteSpace(DR["IsSmsPermissionGiven"].ToString()) &&
DR["IsSmsPermissionGiven"].ToString() != null && DR["IsSmsPermissionGiven"].ToString() != "")
                {
                    if (Convert.ToBoolean(DR["IsSmsPermissionGiven"].ToString()) == true)
                    {
                        s.IsSmsPermissionGivenDisplay = "Yes";
                    }
                    else
                    {
                        s.IsSmsPermissionGivenDisplay = "No";
                    }
                }
                //s.IsSmsPermissionGiven = Convert.ToBoolean(DR["IsSmsPermissionGiven"].ToString());

                if (!string.IsNullOrEmpty(DR["IsLocalToVadodara"].ToString()) && !string.IsNullOrWhiteSpace(DR["IsLocalToVadodara"].ToString()) &&
DR["IsLocalToVadodara"].ToString() != null && DR["IsLocalToVadodara"].ToString() != "") {
                    if (DR["IsLocalToVadodara"].ToString() != "")
                    {
                        s.IsLocalToVadodara = Convert.ToBoolean(DR["IsLocalToVadodara"].ToString());
                    }
                    else
                    {
                        s.IsLocalToVadodara = null;
                    }
                }

                //s.Activity = DR["Activity"].ToString();

                if (DR["ActivityId"].ToString() != "")
                {
                    s.ActivityId = Convert.ToInt64(DR["ActivityId"].ToString());

                }
                else
                {
                    s.ActivityId = 0;
                }

                if (DR["ActivityName"].ToString() != "")
                {
                    s.ActivityName = DR["ActivityName"].ToString();

                }
                else
                {
                    s.ActivityName = "--";
                }

                if (DR["ParticipationLevelsId"].ToString() != "")
                {
                    s.ParticipationLevelsId = Convert.ToInt64(DR["ParticipationLevelsId"].ToString());

                }
                else
                {
                    s.ParticipationLevelsId = 0;
                }

                if (DR["SecuredRankId"].ToString() != "")
                {
                    s.SecuredRankId = Convert.ToInt64(DR["SecuredRankId"].ToString());

                }
                else
                {
                    s.SecuredRankId = 0;
                }
                s.IsEWS = DR["IsEWS"].ToString();

                if (DR["EWSDoc"].ToString() != "")
                {
                    s.EWSDoc = DR["EWSDoc"].ToString();

                }
                else
                {
                    s.EWSDoc = "--";
                }

                if (DR["IsPhysicallyChallenged"].ToString() != "")
                {
                    if (DR["IsPhysicallyChallenged"].ToString() == "True")
                    {
                        s.IsPhysicallyChallenged = "Yes";
                    }
                    else
                    {
                        s.IsPhysicallyChallenged = "No";
                    }
                    //s.IsPhysicallyChallenged = DR["IsPhysicallyChallenged"].ToString();
                }
                else
                {
                    s.IsPhysicallyChallenged = "No";
                }

                if (DR["PCDoc"].ToString() != "")
                {
                    s.PCDoc = DR["PCDoc"].ToString();

                }
                else
                {
                    s.PCDoc = "--";
                }

                if (DR["DisabilityPercentage"].ToString() != "")
                {
                    s.DisabilityPercentage = Convert.ToInt64(DR["DisabilityPercentage"].ToString());
                }
                else
                {
                    s.DisabilityPercentage = 0;
                }

                if (DR["DisabilityType"].ToString() != "")
                {
                    s.DisabilityType = DR["DisabilityType"].ToString();

                }
                else
                {
                    s.DisabilityType = "--";
                }

                string ApplicantPhotoFilePath = serverUrl + "Photo/" + DR["ApplicantPhoto"].ToString();
                s.ApplicantPhoto = ApplicantPhotoFilePath;

                string ApplicantSignatureFilePath = serverUrl + "Signature/" + DR["ApplicantSignature"].ToString();
                s.ApplicantSignature = ApplicantSignatureFilePath;
                
                slist.Add(s);
               

            }
            
            return slist;

        }

        public static List<ProfileForm> BulkProfileEducationDetailsList(Int64 AppId, Int64 PRN)
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("BulkAdmApplicationEducationDetailsGetForDownloadApplicationForm", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ApplicationId", AppId);
            cmd.Parameters.AddWithValue("@UserName", PRN);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            List<ProfileForm> aedList = new List<ProfileForm>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ProfileForm aed = new ProfileForm();
                    aed.EduId = Convert.ToInt32(dr["Id"].ToString());
                    aed.AdmApplicantRegiUserName = Convert.ToInt64(PRN);
                    //aed.AdmApplicantRegistrationId = Convert.ToInt32(responseToken);
                    aed.EligibleDegreeId = Convert.ToInt32(dr["EligibleDegreeId"].ToString());
                    aed.EligibleDegreeName = dr["EligibleDegreeName"].ToString();
                    aed.SpecializationId = Convert.ToInt32(dr["SpecializationId"].ToString());
                    aed.SpecializationName = dr["SpecializationName"].ToString();
                    aed.ExaminationBodyId = Convert.ToInt32(dr["ExaminationBodyId"].ToString());
                    aed.ExaminationBodyName = dr["ExaminationBodyName"].ToString();
                    aed.InstituteAttended = dr["InstituteAttended"].ToString();
                    aed.InstituteCityId = Convert.ToInt32(dr["InstituteCityId"].ToString());



                    if (dr["ExamPassMonth"].ToString() != "")
                    {
                        aed.ExamPassMonth = dr["ExamPassMonth"].ToString();
                    }
                    else
                    {
                        aed.ExamPassMonth = "--";
                    }



                    if (dr["ExamPassYear"].ToString() != "")
                    {
                        aed.ExamPassYear = dr["ExamPassYear"].ToString();
                    }
                    else
                    {
                        aed.ExamPassMonth = "--";
                    }



                    if (dr["MarkObtained"].ToString() != "")
                    {
                        aed.MarkObtained = Convert.ToInt32(dr["MarkObtained"].ToString());
                    }



                    if (dr["MarkOutof"].ToString() != "")
                    {
                        aed.MarkOutof = Convert.ToInt32(dr["MarkOutof"].ToString());
                    }



                    if (dr["Grade"].ToString() != "")
                    {
                        aed.Grade = dr["Grade"].ToString();
                    }
                    else
                    {
                        aed.Grade = "--";
                    }



                    if (dr["CGPA"].ToString() != "")
                    {
                        aed.CGPA = Convert.ToDecimal(dr["CGPA"].ToString());
                    }
                    if (dr["PercentageEquivalenceCGPA"].ToString() != "")
                    {
                        aed.PercentageEquivalenceCGPA = Convert.ToDecimal(dr["PercentageEquivalenceCGPA"].ToString());
                    }
                    if (dr["Percentage"].ToString() != "")
                    {
                        aed.Percentage = Convert.ToDecimal(dr["Percentage"].ToString());
                    }
                    if (dr["ClassId"].ToString() != "")
                    {
                        aed.ClassId = Convert.ToInt32(dr["ClassId"].ToString());
                    }



                    if (dr["ClassName"].ToString() != "")
                    {
                        aed.ClassName = dr["ClassName"].ToString();
                    }
                    else
                    {
                        aed.ClassName = "--";
                    }
                    aedList.Add(aed);
                }
            }

            return aedList;

        }
    }
}
