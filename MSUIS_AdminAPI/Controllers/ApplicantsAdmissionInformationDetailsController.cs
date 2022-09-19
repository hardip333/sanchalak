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
    public class ApplicantsAdmissionInformationDetailsController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region AdmissionInformationDetails Get
        [HttpPost]
        public HttpResponseMessage ApplicantsAdmissionInfoDetailsGetByUserName(ApplicantsAdmissionInformationDetails AAID)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String typePrefix = Request.Headers.GetValues("typePrefix").FirstOrDefault();
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


                Int32 Count = 0;
                Int32 Count1 = 0;
                Int32 Count2 = 0;
                Int32 Count3 = 0;
                Int64 UserName = Convert.ToInt64(AAID.UserName);

                SqlCommand Cmd = new SqlCommand("ApplicantPersonalInfoGetByUserName", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@UserName", UserName);

                Da.SelectCommand = Cmd;
                Da.Fill(Dt);

                List<ApplicantsAdmissionPersonalDetails> ObjLstPI = new List<ApplicantsAdmissionPersonalDetails>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ApplicantsAdmissionPersonalDetails objAAID = new ApplicantsAdmissionPersonalDetails();
                        objAAID.FirstName = (Convert.ToString(Dt.Rows[i]["FirstName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["FirstName"]);
                        objAAID.UserName = (Convert.ToString(Dt.Rows[i]["UserName"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["UserName"]);                 
                        objAAID.LastName = (Convert.ToString(Dt.Rows[i]["LastName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["LastName"]);
                        objAAID.MiddleName = (Convert.ToString(Dt.Rows[i]["MiddleName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["MiddleName"]);
                        objAAID.NameAsPerMarksheet = (Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"]);
                        objAAID.Gender = (Convert.ToString(Dt.Rows[i]["Gender"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["Gender"]);
                        objAAID.Religion = (Convert.ToString(Dt.Rows[i]["Religion"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["Religion"]);
                        objAAID.MotherTongue = (Convert.ToString(Dt.Rows[i]["MotherTongue"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["MotherTongue"]);
                        objAAID.NameOfFather = (Convert.ToString(Dt.Rows[i]["NameOfFather"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["NameOfFather"]);
                        objAAID.NameOfMother = (Convert.ToString(Dt.Rows[i]["NameOfMother"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["NameOfMother"]);
                        objAAID.OccupationOfFather = (Convert.ToString(Dt.Rows[i]["OccupationOfFather"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["OccupationOfFather"]);
                        objAAID.OccupationOfMother = (Convert.ToString(Dt.Rows[i]["OccupationOfMother"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["OccupationOfMother"]);
                        objAAID.FatherMotherContactNo = (Convert.ToString(Dt.Rows[i]["FatherMotherContactNo"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["FatherMotherContactNo"]);
                        objAAID.OccupationOfGuardian = (Convert.ToString(Dt.Rows[i]["OccupationOfGuardian"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["OccupationOfGuardian"]);
                        objAAID.GuardianAnnualIncome = (Convert.ToString(Dt.Rows[i]["GuardianAnnualIncome"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["GuardianAnnualIncome"]);
                        objAAID.GuardianContactNo = (Convert.ToString(Dt.Rows[i]["GuardianContactNo"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["GuardianContactNo"]);
                        objAAID.GuardianName = (Convert.ToString(Dt.Rows[i]["GuardianName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["GuardianName"]);
                        objAAID.IsGuardianEbc = (Convert.ToString(Dt.Rows[i]["IsGuardianEbc"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsGuardianEbc"]);
                        objAAID.EmailId = (Convert.ToString(Dt.Rows[i]["EmailId"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["EmailId"]);
                        objAAID.MobileNo = (Convert.ToString(Dt.Rows[i]["MobileNo"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["MobileNo"]);
                        objAAID.SpouseName = (Convert.ToString(Dt.Rows[i]["SpouseName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["SpouseName"]);
                        objAAID.OptionalMobileNo = (Convert.ToString(Dt.Rows[i]["OptionalMobileNo"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["OptionalMobileNo"]);
                        objAAID.OtherReligion = (Convert.ToString(Dt.Rows[i]["OtherReligion"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["OtherReligion"]);
                        objAAID.FamilyAnnualIncome = (Convert.ToString(Dt.Rows[i]["FamilyAnnualIncome"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["FamilyAnnualIncome"]);
                        objAAID.MaritalStatus = (Convert.ToString(Dt.Rows[i]["MaritalStatus"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["MaritalStatus"]);
                        objAAID.BloodGroup = (Convert.ToString(Dt.Rows[i]["BloodGroup"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["BloodGroup"]);
                        objAAID.CommunicationLanguage = (Convert.ToString(Dt.Rows[i]["CommunicationLanguage"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["CommunicationLanguage"]);
                        objAAID.NameOnAadhar = (Convert.ToString(Dt.Rows[i]["NameOnAadhar"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["NameOnAadhar"]);
                        objAAID.WeightInKgs = (Convert.ToString(Dt.Rows[i]["WeightInKgs"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["WeightInKgs"]);
                        objAAID.HeightInCms = (Convert.ToString(Dt.Rows[i]["HeightInCms"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["HeightInCms"]);
                        objAAID.AadharNumber = (Convert.ToString(Dt.Rows[i]["AadharNumber"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["AadharNumber"]);
                        objAAID.AadharDoc = (Convert.ToString(Dt.Rows[i]["AadharDoc"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["AadharDoc"]);
                        objAAID.IsAadharDocVerified = (Convert.ToString(Dt.Rows[i]["IsAadharDocVerified"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsAadharDocVerified"]);
                        objAAID.DOB = (Convert.ToString(Dt.Rows[i]["DOB"])).IsEmpty() ? "-" : string.Format("{0: dd-MM-yyyy}", (Dt.Rows[i]["DOB"]));
                        objAAID.DOBDoc = (Convert.ToString(Dt.Rows[i]["DOBDoc"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["DOBDoc"]);
                        objAAID.IsDOBDocVerified = (Convert.ToString(Dt.Rows[i]["IsDOBDocVerified"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDOBDocVerified"]);
                        objAAID.IsCurrentAsPermanent = (Convert.ToString(Dt.Rows[i]["IsCurrentAsPermanent"])).IsEmpty() ? false: Convert.ToBoolean(Dt.Rows[i]["IsCurrentAsPermanent"]);
                        objAAID.CurrentAddress = (Convert.ToString(Dt.Rows[i]["CurrentAddress"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["CurrentAddress"]);
                        objAAID.CurrentCountry = (Convert.ToString(Dt.Rows[i]["CurrentCountry"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["CurrentCountry"]);
                        objAAID.CurrentState = (Convert.ToString(Dt.Rows[i]["CurrentState"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["CurrentState"]);
                        objAAID.CurrentDistrict = (Convert.ToString(Dt.Rows[i]["CurrentDistrict"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["CurrentDistrict"]);
                        objAAID.CurrentCityVillage = (Convert.ToString(Dt.Rows[i]["CurrentCityVillage"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["CurrentCityVillage"]);
                        objAAID.CurrentPincode = (Convert.ToString(Dt.Rows[i]["CurrentPincode"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["CurrentPincode"]);
                        objAAID.PermanentAddress = (Convert.ToString(Dt.Rows[i]["PermanentAddress"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["PermanentAddress"]);
                        objAAID.PermanentCityVillage = (Convert.ToString(Dt.Rows[i]["PermanentCityVillage"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["PermanentCityVillage"]);
                        objAAID.PermanentCountry = (Convert.ToString(Dt.Rows[i]["PermanentCountry"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["PermanentCountry"]);
                        objAAID.PermanentDistrict = (Convert.ToString(Dt.Rows[i]["PermanentDistrict"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["PermanentDistrict"]);
                        objAAID.PermanentPincode = (Convert.ToString(Dt.Rows[i]["PermanentPincode"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["PermanentPincode"]);
                        objAAID.PermanentState = (Convert.ToString(Dt.Rows[i]["PermanentState"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["PermanentState"]);
                        objAAID.IsPhysicallyChallenged = (Convert.ToString(Dt.Rows[i]["IsPhysicallyChallenged"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["IsPhysicallyChallenged"]);
                        objAAID.DisabilityType = (Convert.ToString(Dt.Rows[i]["DisabilityType"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["DisabilityType"]);
                        objAAID.DisabilityPercentage = (Convert.ToString(Dt.Rows[i]["DisabilityPercentage"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["DisabilityPercentage"]);
                        objAAID.PCDoc = (Convert.ToString(Dt.Rows[i]["PCDoc"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["PCDoc"]);
                        objAAID.IsPCDocVerified = (Convert.ToString(Dt.Rows[i]["IsPCDocVerified"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsPCDocVerified"]);
                        objAAID.IsPCDocSubmitted = (Convert.ToString(Dt.Rows[i]["IsPCDocSubmitted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsPCDocSubmitted"]);
                        objAAID.EWSDoc = (Convert.ToString(Dt.Rows[i]["EWSDoc"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["EWSDoc"]);
                        objAAID.IsEWSDocSubmitted = (Convert.ToString(Dt.Rows[i]["IsEWSDocSubmitted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsEWSDocSubmitted"]);
                        objAAID.IsEWSDocVerified = (Convert.ToString(Dt.Rows[i]["IsEWSDocVerified"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsEWSDocVerified"]);
                        objAAID.ReservationCategoryDoc = (Convert.ToString(Dt.Rows[i]["ReservationCategoryDoc"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["ReservationCategoryDoc"]);
                        objAAID.IsReservationCategoryDocVerified = (Convert.ToString(Dt.Rows[i]["IsReservationCategoryDocVerified"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsReservationCategoryDocVerified"]);
                        objAAID.IsReservationDocSubmitted = (Convert.ToString(Dt.Rows[i]["IsReservationDocSubmitted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsReservationDocSubmitted"]);
                        objAAID.SocialCategoryDoc = (Convert.ToString(Dt.Rows[i]["SocialCategoryDoc"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["SocialCategoryDoc"]);
                        objAAID.IsSocialCategoryDocVerified = (Convert.ToString(Dt.Rows[i]["IsSocialCategoryDocVerified"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsSocialCategoryDocVerified"]);
                        objAAID.IsSocialDocSubmitted = (Convert.ToString(Dt.Rows[i]["IsSocialDocSubmitted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsSocialDocSubmitted"]);
                        objAAID.IsApplicantSignatureVerified = (Convert.ToString(Dt.Rows[i]["IsApplicantSignatureVerified"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsApplicantSignatureVerified"]);
                        objAAID.IsApplicantPhotoVerified = (Convert.ToString(Dt.Rows[i]["IsApplicantPhotoVerified"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsApplicantPhotoVerified"]);
                        objAAID.IsPhotoIdDocVerified = (Convert.ToString(Dt.Rows[i]["IsPhotoIdDocVerified"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsPhotoIdDocVerified"]);
                        objAAID.IsConfirm = (Convert.ToString(Dt.Rows[i]["IsConfirm"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsConfirm"]);
                        objAAID.IsEmp = (Convert.ToString(Dt.Rows[i]["IsEmp"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsEmp"]);
                        objAAID.IsLocalToVadodara = (Convert.ToString(Dt.Rows[i]["IsLocalToVadodara"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsLocalToVadodara"]);
                        objAAID.IsNRI = (Convert.ToString(Dt.Rows[i]["IsNRI"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsNRI"]);
                        objAAID.IsSmsPermissionGiven = (Convert.ToString(Dt.Rows[i]["IsSmsPermissionGiven"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsSmsPermissionGiven"]);
                        objAAID.CurrentEmployerName = (Convert.ToString(Dt.Rows[i]["CurrentEmployerName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["CurrentEmployerName"]);
                        objAAID.IsMajorThelesamiaStatus = (Convert.ToString(Dt.Rows[i]["IsMajorThelesamiaStatus"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["IsMajorThelesamiaStatus"]);
                        objAAID.PassportNumber = (Convert.ToString(Dt.Rows[i]["PassportNumber"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["PassportNumber"]);
                        objAAID.PassportDate = (Convert.ToString(Dt.Rows[i]["PassportDate"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["PassportDate"]);

                        string serverUrl = "https://admission.msubaroda.ac.in/MSUISApi/Upload/";

                        string ApplicantPhoto = Dt.Rows[i]["ApplicantPhoto"].ToString();
                        string ApplicantSignature = Dt.Rows[i]["ApplicantSignature"].ToString();
                        string AadharDoc = Dt.Rows[i]["AadharDoc"].ToString();
                        string DOBDoc = Dt.Rows[i]["DOBDoc"].ToString();
                        string EWSDoc = Dt.Rows[i]["EWSDoc"].ToString();
                        string ReservationCategoryDoc = Dt.Rows[i]["ReservationCategoryDoc"].ToString();
                        string SocialCategoryDoc = Dt.Rows[i]["SocialCategoryDoc"].ToString();
                        string PCDoc = Dt.Rows[i]["PCDoc"].ToString();


                        if (ApplicantPhoto == null || ApplicantPhoto == "")
                            objAAID.ApplicantPhoto = null;
                        else
                        {
                            string ApplicantPhotoFilePath = serverUrl + "Photo/" + Dt.Rows[i]["ApplicantPhoto"].ToString();
                            objAAID.ApplicantPhoto = ApplicantPhotoFilePath;

                        }
                        if (ApplicantSignature == null || ApplicantSignature == "")
                            objAAID.ApplicantSignature = null;
                        else
                        {
                            string ApplicantSignatureFilePath = serverUrl + "Signature/" + Dt.Rows[i]["ApplicantSignature"].ToString();
                            objAAID.ApplicantSignature = ApplicantSignatureFilePath;

                        }
                        if (AadharDoc == null || AadharDoc == "")
                            objAAID.AadharDoc = null;
                        else
                        {
                            string AadharDocFilePath = serverUrl + "Documents/" + Dt.Rows[i]["AadharDoc"].ToString();
                            objAAID.AadharDoc = AadharDocFilePath;

                        }
                        if (DOBDoc == null || DOBDoc == "")
                            objAAID.DOBDoc = null;
                        else
                        {
                            string DOBDocFilePath = serverUrl + "Documents/" + Dt.Rows[i]["DOBDoc"].ToString();
                            objAAID.DOBDoc = DOBDocFilePath;

                        }
                        if (EWSDoc == null || EWSDoc == "Not Applicable" || EWSDoc == "")
                            objAAID.EWSDoc = null;
                        else
                        {

                            string EWSDocFilePath = serverUrl + "Documents/" + Dt.Rows[i]["EWSDoc"].ToString();
                            objAAID.EWSDoc = EWSDocFilePath;

                        }
                        if (ReservationCategoryDoc == null || ReservationCategoryDoc == "Not Applicable" || ReservationCategoryDoc == "")
                            objAAID.ReservationCategoryDoc = null;
                        else
                        {
                            string ReservationCategoryDocFilePath = serverUrl + "Documents/" + Dt.Rows[i]["ReservationCategoryDoc"].ToString();
                            objAAID.ReservationCategoryDoc = ReservationCategoryDocFilePath;

                        }
                        if (SocialCategoryDoc == null || SocialCategoryDoc == "Not Applicable" || SocialCategoryDoc == "")
                            objAAID.SocialCategoryDoc = null;
                        else
                        {
                            string SocialCategoryDocFilePath = serverUrl + "SocialDocument/" + Dt.Rows[i]["SocialCategoryDoc"].ToString();
                            objAAID.SocialCategoryDoc = SocialCategoryDocFilePath;

                        }
                        if (PCDoc == null || PCDoc == "Not Applicable" || PCDoc == "")
                            objAAID.PCDoc = null;
                        else
                        {
                            string PCDocFilePath = serverUrl + "Documents/" + Dt.Rows[i]["PCDoc"].ToString();
                            objAAID.PCDoc = PCDocFilePath;

                        }


                        ObjLstPI.Add(objAAID);

                    }

                }

                SqlCommand cmd1 = new SqlCommand("ApplicantAdmissionInfoDetailsGetByUserName", Con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@UserName", UserName);
                SqlDataAdapter sda1 = new SqlDataAdapter();
                DataTable dt1 = new DataTable();
                sda1.SelectCommand = cmd1;
                sda1.Fill(dt1);

                List<ApplicantsAdmissionInformationDetails> ObjLstAdmission = new List<ApplicantsAdmissionInformationDetails>();
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        ApplicantsAdmissionInformationDetails objADM = new ApplicantsAdmissionInformationDetails();
                        objADM.ApplicationStatus = (Convert.ToString(dt1.Rows[i]["ApplicationStatus"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["ApplicationStatus"]);
                        objADM.FacultyName = (Convert.ToString(dt1.Rows[i]["FacultyName"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["FacultyName"]);
                        objADM.BranchName = (Convert.ToString(dt1.Rows[i]["BranchName"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["BranchName"]);
                        objADM.ProgrammeName = (((dt1.Rows[i]["ProgrammeName"]).ToString()).IsEmpty()) ? "-" : Convert.ToString(dt1.Rows[i]["ProgrammeName"]);
                        objADM.InstancePartTermName = (Convert.ToString(dt1.Rows[i]["InstancePartTermName"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["InstancePartTermName"]);
                        objADM.ApplicationId = (Convert.ToString(dt1.Rows[i]["ApplicationId"])).IsEmpty() ? 0 : Convert.ToInt64(dt1.Rows[i]["ApplicationId"]);
                       
                        objADM.IsApprovedByFaculty = (Convert.ToString(dt1.Rows[i]["IsApprovedByFaculty"])).IsEmpty() ? false : Convert.ToBoolean(dt1.Rows[i]["IsApprovedByFaculty"]);
                        objADM.IsApprovedByAcademic = (Convert.ToString(dt1.Rows[i]["IsApprovedByAcademic"])).IsEmpty() ? false : Convert.ToBoolean(dt1.Rows[i]["IsApprovedByAcademic"]);


                        objADM.IsCancelledByStudent = (Convert.ToString(dt1.Rows[i]["IsCancelledByStudent"])).IsEmpty() ? false : Convert.ToBoolean(dt1.Rows[i]["IsCancelledByStudent"]);
                        objADM.IsCancelledByFaculty = (Convert.ToString(dt1.Rows[i]["IsCancelledByFaculty"])).IsEmpty() ? false : Convert.ToBoolean(dt1.Rows[i]["IsCancelledByFaculty"]);
                        objADM.IsCancelledByAcademics = (Convert.ToString(dt1.Rows[i]["IsCancelledByAcademics"])).IsEmpty() ? false : Convert.ToBoolean(dt1.Rows[i]["IsCancelledByAcademics"]);


                        objADM.IsCentrallyAdmission = (Convert.ToString(dt1.Rows[i]["IsCentrallyAdmission"])).IsEmpty() ? false : Convert.ToBoolean(dt1.Rows[i]["IsCentrallyAdmission"]);
                        objADM.IsPaperSelected = (Convert.ToString(dt1.Rows[i]["IsPaperSelected"])).IsEmpty() ? false : Convert.ToBoolean(dt1.Rows[i]["IsPaperSelected"]);
                        objADM.IsDualAdmission = (Convert.ToString(dt1.Rows[i]["IsDualAdmission"])).IsEmpty() ? false : Convert.ToBoolean(dt1.Rows[i]["IsDualAdmission"]);

                        objADM.IsPRNGenerated = (Convert.ToString(dt1.Rows[i]["IsPRNGenerated"])).IsEmpty() ? false : Convert.ToBoolean(dt1.Rows[i]["IsPRNGenerated"]);
                        objADM.IsAdmissionFeePaid = (Convert.ToString(dt1.Rows[i]["IsAdmissionFeePaid"])).IsEmpty() ? false : Convert.ToBoolean(dt1.Rows[i]["IsAdmissionFeePaid"]);
                        objADM.TotalAdmissionFeePaid = (Convert.ToString(dt1.Rows[i]["TotalAdmissionFeePaid"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["TotalAdmissionFeePaid"]);
                        objADM.TotalAmount = (Convert.ToString(dt1.Rows[i]["TotalAmount"])).IsEmpty() ? "Not Defined" : Convert.ToString(dt1.Rows[i]["TotalAmount"]);
                        objADM.AdminRemarkByFaculty = (Convert.ToString(dt1.Rows[i]["AdminRemarkByFaculty"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["AdminRemarkByFaculty"]);
                        objADM.AdminRemarkByAcademics = (Convert.ToString(dt1.Rows[i]["AdminRemarkByAcademics"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["AdminRemarkByAcademics"]);
                        objADM.StudentCancelledRemark = (Convert.ToString(dt1.Rows[i]["StudentCancelledRemark"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["StudentCancelledRemark"]);
                        objADM.AcademicsCancelledRemark = (Convert.ToString(dt1.Rows[i]["AcademicsCancelledRemark"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["AcademicsCancelledRemark"]);
                        objADM.FacultyCancelledRemark = (Convert.ToString(dt1.Rows[i]["FacultyCancelledRemark"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["FacultyCancelledRemark"]);
                        objADM.CancelType = (Convert.ToString(dt1.Rows[i]["CancelType"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["CancelType"]);
                        objADM.CancelledApplicationStatus = (Convert.ToString(dt1.Rows[i]["CancelledApplicationStatus"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["CancelledApplicationStatus"]);
                        objADM.BranchChangeRequest = (Convert.ToString(dt1.Rows[i]["BranchChangeRequest"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["BranchChangeRequest"]);
                        objADM.FeeCategoryChangeRequest = (Convert.ToString(dt1.Rows[i]["FeeCategoryChangeRequest"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["FeeCategoryChangeRequest"]);
                        objADM.InstituteChangeRequest = (Convert.ToString(dt1.Rows[i]["InstituteChangeRequest"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["InstituteChangeRequest"]);
                        objADM.CancelChangeRequest = (Convert.ToString(dt1.Rows[i]["CancelChangeRequest"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["CancelChangeRequest"]);
                        objADM.IsBOBAccount = (Convert.ToString(dt1.Rows[i]["IsBOBAccount"])).IsEmpty() ? false : Convert.ToBoolean(dt1.Rows[i]["IsBOBAccount"]);

                        objADM.EligibilityStatus = (Convert.ToString(dt1.Rows[i]["EligibilityStatus"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["EligibilityStatus"]);
                        objADM.EligibilityByAcademics = (Convert.ToString(dt1.Rows[i]["EligibilityByAcademics"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["EligibilityByAcademics"]);
                        objADM.IsAdmitted = (Convert.ToString(dt1.Rows[i]["IsAdmitted"])).IsEmpty() ? false : Convert.ToBoolean(dt1.Rows[i]["IsAdmitted"]);
                        objADM.IsAdmissionBlock = (Convert.ToString(dt1.Rows[i]["IsAdmissionBlock"])).IsEmpty() ? false : Convert.ToBoolean(dt1.Rows[i]["IsAdmissionBlock"]);
                        objADM.IsSmsSent = (Convert.ToString(dt1.Rows[i]["IsSmsSent"])).IsEmpty() ? false : Convert.ToBoolean(dt1.Rows[i]["IsSmsSent"]);
                        objADM.IsEmailSent = (Convert.ToString(dt1.Rows[i]["IsEmailSent"])).IsEmpty() ? false : Convert.ToBoolean(dt1.Rows[i]["IsEmailSent"]);
                        objADM.IsAdmissionSecuredElesewhere = (Convert.ToString(dt1.Rows[i]["IsAdmissionSecuredElesewhere"])).IsEmpty() ? false : Convert.ToBoolean(dt1.Rows[i]["IsAdmissionSecuredElesewhere"]);
                        objADM.IsAppliedElseWhere = (Convert.ToString(dt1.Rows[i]["IsAppliedElseWhere"])).IsEmpty() ? false : Convert.ToBoolean(dt1.Rows[i]["IsAppliedElseWhere"]);
                        objADM.IsVerifiedByFaculty = (Convert.ToString(dt1.Rows[i]["IsVerifiedByFaculty"])).IsEmpty() ? false : Convert.ToBoolean(dt1.Rows[i]["IsVerifiedByFaculty"]);
                        objADM.IsVerificationSms = (Convert.ToString(dt1.Rows[i]["IsVerificationSms"])).IsEmpty() ? false : Convert.ToBoolean(dt1.Rows[i]["IsVerificationSms"]);
                        objADM.IsVerificationEmail = (Convert.ToString(dt1.Rows[i]["IsVerificationEmail"])).IsEmpty() ? false : Convert.ToBoolean(dt1.Rows[i]["IsVerificationEmail"]);
                       
                        objADM.IsBlocked = (Convert.ToString(dt1.Rows[i]["IsBlocked"])).IsEmpty() ? false : Convert.ToBoolean(dt1.Rows[i]["IsBlocked"]);
                        
                        objADM.ApplicantRegistrationId = (Convert.ToString(dt1.Rows[i]["ApplicantRegistrationId"])).IsEmpty() ? 0 : Convert.ToInt64(dt1.Rows[i]["ApplicantRegistrationId"]);
                       
                        objADM.DestinationIncProgInstPartTerm = (Convert.ToString(dt1.Rows[i]["DestinationIncProgInstPartTerm"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["DestinationIncProgInstPartTerm"]);                     
                        objADM.AdmissionBlockRemark = (Convert.ToString(dt1.Rows[i]["AdmissionBlockRemark"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["AdmissionBlockRemark"]);                     
                        objADM.FeeCategoryName = (Convert.ToString(dt1.Rows[i]["FeeCategoryName"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["FeeCategoryName"]);                     
                        objADM.EligibilityStatus = (Convert.ToString(dt1.Rows[i]["EligibilityStatus"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["EligibilityStatus"]);                     
                        objADM.BlockedRemark = (Convert.ToString(dt1.Rows[i]["BlockedRemark"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["BlockedRemark"]);                     
                        objADM.AllotmentNo = (Convert.ToString(dt1.Rows[i]["AllotmentNo"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["AllotmentNo"]);                     
                        objADM.MeritNo = (Convert.ToString(dt1.Rows[i]["MeritNo"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["MeritNo"]);                     
                        objADM.AdmissionCommittee = (Convert.ToString(dt1.Rows[i]["AdmissionCommittee"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["AdmissionCommittee"]);                     
                        
                        objADM.ApprovedProgrammeInstancePartTerm = (Convert.ToString(dt1.Rows[i]["ApprovedProgrammeInstancePartTerm"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["ApprovedProgrammeInstancePartTerm"]);                     
                        objADM.BankName = (Convert.ToString(dt1.Rows[i]["BankName"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["BankName"]);                     
                        objADM.OtherUniversityName = (Convert.ToString(dt1.Rows[i]["OtherUniversityName"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["OtherUniversityName"]);                     
                        objADM.OtherUniversityProgrammeName = (Convert.ToString(dt1.Rows[i]["OtherUniversityProgrammeName"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["OtherUniversityProgrammeName"]);                     
                        objADM.Reason = (Convert.ToString(dt1.Rows[i]["Reason"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["Reason"]);                     
                        objADM.AccountName = (Convert.ToString(dt1.Rows[i]["AccountName"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["AccountName"]);                     
                        objADM.AccountNumber = (Convert.ToString(dt1.Rows[i]["AccountNumber"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["AccountNumber"]);                     
                        objADM.IFSCCode = (Convert.ToString(dt1.Rows[i]["IFSCCode"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["IFSCCode"]);                                           
                        objADM.AuditRemark = (Convert.ToString(dt1.Rows[i]["AuditRemark"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["AuditRemark"]);                                           
                        objADM.RefundModeByAcademic = (Convert.ToString(dt1.Rows[i]["RefundModeByAcademic"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["RefundModeByAcademic"]);                                           
                        objADM.AccountRemark = (Convert.ToString(dt1.Rows[i]["AccountRemark"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["AccountRemark"]);                                           
                        objADM.RequestStatus = (Convert.ToString(dt1.Rows[i]["RequestStatus"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["RequestStatus"]);                                           
                        objADM.RefundedTansactionId = (Convert.ToString(dt1.Rows[i]["RefundedTansactionId"])).IsEmpty() ? "-" : Convert.ToString(dt1.Rows[i]["RefundedTansactionId"]);                                           
                       
                        objADM.RequestedOn = (Convert.ToString(dt1.Rows[i]["RequestedOn"])).IsEmpty() ? "-" : string.Format("{0: dd-MM-yyyy}", (dt1.Rows[i]["RequestedOn"]));
                        objADM.CancelApprovedOnAcademic = (Convert.ToString(dt1.Rows[i]["CancelApprovedOnAcademic"])).IsEmpty() ? "-" : string.Format("{0: dd-MM-yyyy}", (dt1.Rows[i]["CancelApprovedOnAcademic"]));
                        objADM.ProcessedOnAudit = (Convert.ToString(dt1.Rows[i]["ProcessedOnAudit"])).IsEmpty() ? "-" : string.Format("{0: dd-MM-yyyy}", (dt1.Rows[i]["ProcessedOnAudit"]));
                        objADM.ProcessedOnAccount = (Convert.ToString(dt1.Rows[i]["ProcessedOnAccount"])).IsEmpty() ? "-" : string.Format("{0: dd-MM-yyyy}", (dt1.Rows[i]["ProcessedOnAccount"]));
                        
                        objADM.AutoDeductAmount = (Convert.ToString(dt1.Rows[i]["AutoDeductAmount"])).IsEmpty() ? 0 : Convert.ToDecimal(dt1.Rows[i]["AutoDeductAmount"]);
                        objADM.RefundAmountByAudit = (Convert.ToString(dt1.Rows[i]["RefundAmountByAudit"])).IsEmpty() ? 0 : Convert.ToDecimal(dt1.Rows[i]["RefundAmountByAudit"]);
                        objADM.RefundAmountByAcademic = (Convert.ToString(dt1.Rows[i]["RefundAmountByAcademic"])).IsEmpty() ? 0 : Convert.ToDecimal(dt1.Rows[i]["RefundAmountByAcademic"]);

                        objADM.IsApprovedByAudit = (Convert.ToString(dt1.Rows[i]["IsApprovedByAudit"])).IsEmpty() ? false : Convert.ToBoolean(dt1.Rows[i]["IsApprovedByAudit"]);
                        objADM.IsApprovedByAccount = (Convert.ToString(dt1.Rows[i]["IsApprovedByAccount"])).IsEmpty() ? false : Convert.ToBoolean(dt1.Rows[i]["IsApprovedByAccount"]);
                        objADM.IsRefundedbyAccount = (Convert.ToString(dt1.Rows[i]["IsRefundedbyAccount"])).IsEmpty() ? false : Convert.ToBoolean(dt1.Rows[i]["IsRefundedbyAccount"]);
                        objADM.IsRequestCompleted = (Convert.ToString(dt1.Rows[i]["IsRequestCompleted"])).IsEmpty() ? false : Convert.ToBoolean(dt1.Rows[i]["IsRequestCompleted"]);
                        
                        objADM.DateOfApplication = (Convert.ToString(dt1.Rows[i]["DateOfApplication"])).IsEmpty() ? "-" : string.Format("{0: dd-MM-yyyy}", (dt1.Rows[i]["DateOfApplication"]));
                        objADM.IsVerificationSmsOn = (Convert.ToString(dt1.Rows[i]["IsVerificationSmsOn"])).IsEmpty() ? "-" : string.Format("{0: dd-MM-yyyy}", (dt1.Rows[i]["IsVerificationSmsOn"]));
                        objADM.IsVerificationEmailOn = (Convert.ToString(dt1.Rows[i]["IsVerificationEmailOn"])).IsEmpty() ? "-" : string.Format("{0: dd-MM-yyyy}", (dt1.Rows[i]["IsVerificationEmailOn"]));
                        objADM.AdmittedOn = (Convert.ToString(dt1.Rows[i]["AdmittedOn"])).IsEmpty() ? "-" : string.Format("{0: dd-MM-yyyy}", (dt1.Rows[i]["AdmittedOn"]));
                        objADM.ApprovedOnFaculty = (Convert.ToString(dt1.Rows[i]["ApprovedOnFaculty"])).IsEmpty() ? "-" : string.Format("{0: dd-MM-yyyy}", (dt1.Rows[i]["ApprovedOnFaculty"]));
                        objADM.ApprovedOnAcademics = (Convert.ToString(dt1.Rows[i]["ApprovedOnAcademics"])).IsEmpty() ? "-" : string.Format("{0: dd-MM-yyyy}", (dt1.Rows[i]["ApprovedOnAcademics"]));
                        objADM.BlockedOn = (Convert.ToString(dt1.Rows[i]["BlockedOn"])).IsEmpty() ? "-" : string.Format("{0: dd-MM-yyyy}", (dt1.Rows[i]["BlockedOn"]));
                        objADM.FacultyCancelledOn = (Convert.ToString(dt1.Rows[i]["FacultyCancelledOn"])).IsEmpty() ? "-" : string.Format("{0: dd-MM-yyyy}", (dt1.Rows[i]["FacultyCancelledOn"]));
                        objADM.AcademicsCancelledOn = (Convert.ToString(dt1.Rows[i]["AcademicsCancelledOn"])).IsEmpty() ? "-" : string.Format("{0: dd-MM-yyyy}", (dt1.Rows[i]["AcademicsCancelledOn"]));
                        objADM.VerifiedOnFaculty = (Convert.ToString(dt1.Rows[i]["VerifiedOnFaculty"])).IsEmpty() ? "-" : string.Format("{0: dd-MM-yyyy}", (dt1.Rows[i]["VerifiedOnFaculty"]));
                        objADM.CancelApprovedOnFaculty = (Convert.ToString(dt1.Rows[i]["CancelApprovedOnFaculty"])).IsEmpty() ? "-" : string.Format("{0: dd-MM-yyyy}", (dt1.Rows[i]["CancelApprovedOnFaculty"]));
                       
                        
                        
                        string serverUrl = "https://admission.msubaroda.ac.in/MSUISApi/Upload/";

                        string AuthorityLetter = dt1.Rows[i]["AuthorityLetter"].ToString();
                        string PassbookDoc = dt1.Rows[i]["PassbookDoc"].ToString();
                        string RTGSForm = dt1.Rows[i]["RTGSForm"].ToString();
                        string ApplicationDoc = dt1.Rows[i]["ApplicationDoc"].ToString();


                        if (AuthorityLetter == null || AuthorityLetter == "")
                            objADM.AuthorityLetter = null;
                        else
                        {
                            string AuthorityLetterFilePath = serverUrl + "StudentAuthorityLetterDocument/" + dt1.Rows[i]["AuthorityLetter"].ToString();
                            objADM.AuthorityLetter = AuthorityLetterFilePath;

                        }
                        if (PassbookDoc == null || PassbookDoc == "")
                            objADM.PassbookDoc = null;
                        else
                        {
                            string PassbookDocFilePath = serverUrl + "StudentPassBookImageDocument/" + dt1.Rows[i]["PassbookDoc"].ToString();
                            objADM.PassbookDoc = PassbookDocFilePath;

                        }
                        if (RTGSForm == null || RTGSForm == "")
                            objADM.RTGSForm = null;
                        else
                        {
                            string RTGSFormFilePath = serverUrl + "StudentRTGSImageDocument/" + dt1.Rows[i]["RTGSForm"].ToString();
                            objADM.RTGSForm = RTGSFormFilePath;

                        }
                        if (ApplicationDoc == null || ApplicationDoc == "")
                            objADM.ApplicationDoc = null;
                        else
                        {
                            string ApplicationDocFilePath = serverUrl + "StudentApplicationImageDocument/" + dt1.Rows[i]["ApplicationDoc"].ToString();
                            objADM.ApplicationDoc = ApplicationDocFilePath;

                        }

                        Count = Count + 1;
                        objADM.IndexId = Count;

                        ObjLstAdmission.Add(objADM);

                    }
                }
                SqlCommand cmd2 = new SqlCommand("ApplicantEducationDetailsGetByUserName", Con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@UserName", UserName);
                SqlDataAdapter sda2 = new SqlDataAdapter();
                DataTable dt2 = new DataTable();
                sda2.SelectCommand = cmd2;
                sda2.Fill(dt2);

                List<ApplicantEducationDetails> ObjLstEducation = new List<ApplicantEducationDetails>();
                if (dt2.Rows.Count > 0)
                {
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        ApplicantEducationDetails objEducation = new ApplicantEducationDetails();
                        objEducation.EligibleDegreeName = (Convert.ToString(dt2.Rows[i]["EligibleDegreeName"])).IsEmpty() ? "-" : Convert.ToString(dt2.Rows[i]["EligibleDegreeName"]);
                        objEducation.LanguageName = (Convert.ToString(dt2.Rows[i]["LanguageName"])).IsEmpty() ? "-" : Convert.ToString(dt2.Rows[i]["LanguageName"]);
                        objEducation.ClassName = (Convert.ToString(dt2.Rows[i]["ClassName"])).IsEmpty() ? "-" : Convert.ToString(dt2.Rows[i]["ClassName"]);
                        objEducation.CityName = (((dt2.Rows[i]["CityName"]).ToString()).IsEmpty()) ? "-" : Convert.ToString(dt2.Rows[i]["CityName"]);
                        objEducation.ExaminationBodyName = (Convert.ToString(dt2.Rows[i]["ExaminationBodyName"])).IsEmpty() ? "-" : Convert.ToString(dt2.Rows[i]["ExaminationBodyName"]);
                        objEducation.SpecializationName = (Convert.ToString(dt2.Rows[i]["SpecializationName"])).IsEmpty() ? "-" : Convert.ToString(dt2.Rows[i]["SpecializationName"]);
                        objEducation.ExamCertificateNumber = (Convert.ToString(dt2.Rows[i]["ExamCertificateNumber"])).IsEmpty() ? "-" : Convert.ToString(dt2.Rows[i]["ExamCertificateNumber"]);
                        //objEducation.AttachDocument = (Convert.ToString(dt2.Rows[i]["AttachDocument"])).IsEmpty() ? "-" : Convert.ToString(dt2.Rows[i]["AttachDocument"]);
                        objEducation.ExamPassCity = (Convert.ToString(dt2.Rows[i]["ExamPassCity"])).IsEmpty() ? "-" : Convert.ToString(dt2.Rows[i]["ExamPassCity"]);
                        objEducation.ExamPassMonth = (Convert.ToString(dt2.Rows[i]["ExamPassMonth"])).IsEmpty() ? "-" : Convert.ToString(dt2.Rows[i]["ExamPassMonth"]);
                        objEducation.ExamPassYear = (Convert.ToString(dt2.Rows[i]["ExamPassYear"])).IsEmpty() ? "-" : Convert.ToString(dt2.Rows[i]["ExamPassYear"]);
                        objEducation.ExamSeatNumber = (Convert.ToString(dt2.Rows[i]["ExamSeatNumber"])).IsEmpty() ? "-" : Convert.ToString(dt2.Rows[i]["ExamSeatNumber"]);
                        objEducation.Grade = (Convert.ToString(dt2.Rows[i]["Grade"])).IsEmpty() ? "-" : Convert.ToString(dt2.Rows[i]["Grade"]);
                        objEducation.InstituteAttended = (Convert.ToString(dt2.Rows[i]["InstituteAttended"])).IsEmpty() ? "-" : Convert.ToString(dt2.Rows[i]["InstituteAttended"]);
                        objEducation.OtherCity = (Convert.ToString(dt2.Rows[i]["OtherCity"])).IsEmpty() ? "-" : Convert.ToString(dt2.Rows[i]["OtherCity"]);
                        objEducation.ResultStatus = (Convert.ToString(dt2.Rows[i]["ResultStatus"])).IsEmpty() ? "-" : Convert.ToString(dt2.Rows[i]["ResultStatus"]);
                        objEducation.SchoolNo = (Convert.ToString(dt2.Rows[i]["SchoolNo"])).IsEmpty() ? "-" : Convert.ToString(dt2.Rows[i]["SchoolNo"]);

                        objEducation.MarkObtained = (Convert.ToString(dt2.Rows[i]["MarkObtained"])).IsEmpty() ? 0 : Convert.ToInt64(dt2.Rows[i]["MarkObtained"]);
                        objEducation.MarkOutOf = (Convert.ToString(dt2.Rows[i]["MarkOutOf"])).IsEmpty() ? 0 : Convert.ToInt64(dt2.Rows[i]["MarkOutOf"]);

                        objEducation.IsDeclared = (Convert.ToString(dt2.Rows[i]["IsDeclared"])).IsEmpty() ? false : Convert.ToBoolean(dt2.Rows[i]["IsDeclared"]);
                        objEducation.IsFirstTrial = (Convert.ToString(dt2.Rows[i]["IsFirstTrial"])).IsEmpty() ? false : Convert.ToBoolean(dt2.Rows[i]["IsFirstTrial"]);
                        objEducation.IsLastQualifyingExam = (Convert.ToString(dt2.Rows[i]["IsLastQualifyingExam"])).IsEmpty() ? false : Convert.ToBoolean(dt2.Rows[i]["IsLastQualifyingExam"]);
                        objEducation.IsVerified = (Convert.ToString(dt2.Rows[i]["IsVerified"])).IsEmpty() ? false : Convert.ToBoolean(dt2.Rows[i]["IsVerified"]);

                        objEducation.CGPA = (Convert.ToString(dt2.Rows[i]["CGPA"])).IsEmpty() ? 0 : Convert.ToDecimal(dt2.Rows[i]["CGPA"]);
                        objEducation.Percentage = (Convert.ToString(dt2.Rows[i]["Percentage"])).IsEmpty() ? 0 : Convert.ToDecimal(dt2.Rows[i]["Percentage"]);
                        objEducation.PercentageEquivalenceCGPA = (Convert.ToString(dt2.Rows[i]["PercentageEquivalenceCGPA"])).IsEmpty() ? 0 : Convert.ToDecimal(dt2.Rows[i]["PercentageEquivalenceCGPA"]);
                        string serverUrl = "https://admission.msubaroda.ac.in/MSUISApi/Upload/";

                        string AttachDocument = dt2.Rows[i]["AttachDocument"].ToString();
                        if (AttachDocument == null || AttachDocument == "")
                            objEducation.AttachDocument = null;
                        else
                        {
                            string AttachDocumentFilePath = serverUrl + "Documents/" + dt2.Rows[i]["AttachDocument"].ToString();
                            objEducation.AttachDocument = AttachDocumentFilePath;

                        }

                        Count1 = Count1 + 1;
                        objEducation.IndexEduId = Count1;


                        ObjLstEducation.Add(objEducation);
                    }



                }


                SqlCommand cmd3 = new SqlCommand("ApplicantRequestStatusGetByUserName", Con);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@UserName", UserName);
                SqlDataAdapter sda3 = new SqlDataAdapter();
                DataTable dt3 = new DataTable();
                sda3.SelectCommand = cmd3;
                sda3.Fill(dt3);

                List<ApplicantRequestStatusDetails> ObjLstRequestStatus = new List<ApplicantRequestStatusDetails>();
                if (dt3.Rows.Count > 0)
                {
                    for (int i = 0; i < dt3.Rows.Count; i++)
                    {
                        ApplicantRequestStatusDetails objRequestStatus = new ApplicantRequestStatusDetails();
                        objRequestStatus.ApplicationId = (Convert.ToString(dt3.Rows[i]["ApplicationId"])).IsEmpty() ? 0 : Convert.ToInt64(dt3.Rows[i]["ApplicationId"]);                      
                        objRequestStatus.RequestType = (Convert.ToString(dt3.Rows[i]["RequestType"])).IsEmpty() ? "-" : Convert.ToString(dt3.Rows[i]["RequestType"]);                      
                        objRequestStatus.RequestStatus = (Convert.ToString(dt3.Rows[i]["RequestStatus"])).IsEmpty() ? "-" : Convert.ToString(dt3.Rows[i]["RequestStatus"]);    
                        objRequestStatus.OldFeeCategoryName = (Convert.ToString(dt3.Rows[i]["OldFeeCategoryName"])).IsEmpty() ? "-" : Convert.ToString(dt3.Rows[i]["OldFeeCategoryName"]);
                        objRequestStatus.NewFeeCategoryName = (Convert.ToString(dt3.Rows[i]["NewFeeCategoryName"])).IsEmpty() ? "-" : Convert.ToString(dt3.Rows[i]["NewFeeCategoryName"]);
                        objRequestStatus.OldInstancePartTermName = (Convert.ToString(dt3.Rows[i]["OldInstancePartTermName"])).IsEmpty() ? "-" : Convert.ToString(dt3.Rows[i]["OldInstancePartTermName"]);
                        objRequestStatus.NewInstancePartTermName = (Convert.ToString(dt3.Rows[i]["NewInstancePartTermName"])).IsEmpty() ? "-" : Convert.ToString(dt3.Rows[i]["NewInstancePartTermName"]);
                        objRequestStatus.OldInstituteName = (Convert.ToString(dt3.Rows[i]["OldInstituteName"])).IsEmpty() ? "-" : Convert.ToString(dt3.Rows[i]["OldInstituteName"]);
                        objRequestStatus.NewInstituteName = (Convert.ToString(dt3.Rows[i]["NewInstituteName"])).IsEmpty() ? "-" : Convert.ToString(dt3.Rows[i]["NewInstituteName"]);
                        objRequestStatus.OldGroupName = (Convert.ToString(dt3.Rows[i]["OldGroupName"])).IsEmpty() ? "-" : Convert.ToString(dt3.Rows[i]["OldGroupName"]);
                        objRequestStatus.NewGroupName = (Convert.ToString(dt3.Rows[i]["NewGroupName"])).IsEmpty() ? "-" : Convert.ToString(dt3.Rows[i]["NewGroupName"]);
                        objRequestStatus.IsRequestCompleted = (Convert.ToString(dt3.Rows[i]["IsRequestCompleted"])).IsEmpty() ? false : Convert.ToBoolean(dt3.Rows[i]["IsRequestCompleted"]);
                        objRequestStatus.FacultyRemark = (Convert.ToString(dt3.Rows[i]["FacultyRemark"])).IsEmpty() ? "-" : Convert.ToString(dt3.Rows[i]["FacultyRemark"]);
                        objRequestStatus.NameAsPerMarksheet = (Convert.ToString(dt3.Rows[i]["NameAsPerMarksheet"])).IsEmpty() ? "-" : Convert.ToString(dt3.Rows[i]["NameAsPerMarksheet"]);
                        objRequestStatus.FacultyName = (Convert.ToString(dt3.Rows[i]["FacultyName"])).IsEmpty() ? "-" : Convert.ToString(dt3.Rows[i]["FacultyName"]);
                        objRequestStatus.ProgrammeName = (Convert.ToString(dt3.Rows[i]["ProgrammeName"])).IsEmpty() ? "-" : Convert.ToString(dt3.Rows[i]["ProgrammeName"]);
                        objRequestStatus.BranchName = (Convert.ToString(dt3.Rows[i]["BranchName"])).IsEmpty() ? "-" : Convert.ToString(dt3.Rows[i]["BranchName"]);

                        Count2 = Count2 + 1;
                        objRequestStatus.IndexRequestId = Count2;


                        ObjLstRequestStatus.Add(objRequestStatus);
                    }



                }

                SqlCommand cmd4 = new SqlCommand("ApplicantPreExaminationDetailsGetByUserName", Con);
                cmd4.CommandType = CommandType.StoredProcedure;
                cmd4.Parameters.AddWithValue("@UserName", UserName);
                SqlDataAdapter sda4 = new SqlDataAdapter();
                DataTable dt4 = new DataTable();
                sda4.SelectCommand = cmd4;
                sda4.Fill(dt4);

                List<ApplicantPreExaminationDetails> ObjLstPreExamination = new List<ApplicantPreExaminationDetails>();
                if (dt4.Rows.Count > 0)
                {
                    for (int i = 0; i < dt4.Rows.Count; i++)
                    {
                        ApplicantPreExaminationDetails objPreExamination = new ApplicantPreExaminationDetails();
                        objPreExamination.ApplicationId = (Convert.ToString(dt4.Rows[i]["ApplicationId"])).IsEmpty() ? 0 : Convert.ToInt64(dt4.Rows[i]["ApplicationId"]);
                        //==========Start - Added by Mohini on 08-Apr-2022
                        objPreExamination.PRN = (Convert.ToString(dt4.Rows[i]["PRN"])).IsEmpty() ? 0 : Convert.ToInt64(dt4.Rows[i]["PRN"]);
                        objPreExamination.ExamEventId = (Convert.ToString(dt4.Rows[i]["ExamEventId"])).IsEmpty() ? 0 : Convert.ToInt64(dt4.Rows[i]["ExamEventId"]);
                        objPreExamination.ProgInstPartTermId = (Convert.ToString(dt4.Rows[i]["ProgInstPartTermId"])).IsEmpty() ? 0 : Convert.ToInt64(dt4.Rows[i]["ProgInstPartTermId"]);
                        //==========End - Added by Mohini on 08-Apr-2022
                        objPreExamination.InstancePartTermName = (Convert.ToString(dt4.Rows[i]["InstancePartTermName"])).IsEmpty() ? "-" : Convert.ToString(dt4.Rows[i]["InstancePartTermName"]);
                        objPreExamination.FacultyName = (Convert.ToString(dt4.Rows[i]["FacultyName"])).IsEmpty() ? "-" : Convert.ToString(dt4.Rows[i]["FacultyName"]);
                        objPreExamination.DisplayName = (Convert.ToString(dt4.Rows[i]["DisplayName"])).IsEmpty() ? "-" : Convert.ToString(dt4.Rows[i]["DisplayName"]);
                        objPreExamination.FormNo = (Convert.ToString(dt4.Rows[i]["FormNo"])).IsEmpty() ? "-" : Convert.ToString(dt4.Rows[i]["FormNo"]);
                        objPreExamination.SeatNumber = (Convert.ToString(dt4.Rows[i]["SeatNumber"])).IsEmpty() ? "-" : Convert.ToString(dt4.Rows[i]["SeatNumber"]);
                        objPreExamination.PartTermStatus = (Convert.ToString(dt4.Rows[i]["PartTermStatus"])).IsEmpty() ? "-" : Convert.ToString(dt4.Rows[i]["PartTermStatus"]);
                        objPreExamination.PartStatus = (Convert.ToString(dt4.Rows[i]["PartStatus"])).IsEmpty() ? "-" : Convert.ToString(dt4.Rows[i]["PartStatus"]);
                        objPreExamination.IsExamFeesPaid = (Convert.ToString(dt4.Rows[i]["IsExamFeesPaid"])).IsEmpty() ? false : Convert.ToBoolean(dt4.Rows[i]["IsExamFeesPaid"]);
                        objPreExamination.EvaluationId = (Convert.ToString(dt4.Rows[i]["EvaluationId"])).IsEmpty() ? 0 : Convert.ToInt64(dt4.Rows[i]["EvaluationId"]);
                        objPreExamination.SpecialisationId = (Convert.ToString(dt4.Rows[i]["SpecialisationId"])).IsEmpty() ? 0 : Convert.ToInt64(dt4.Rows[i]["SpecialisationId"]);
                        objPreExamination.ProgrammePartTermId = (Convert.ToString(dt4.Rows[i]["ProgrammePartTermId"])).IsEmpty() ? 0 : Convert.ToInt64(dt4.Rows[i]["ProgrammePartTermId"]);


                        Count3 = Count3 + 1;
                        objPreExamination.IndexPreExaminationId = Count3;
                        ObjLstPreExamination.Add(objPreExamination);
                    }



                }


                List<Programme> Programmedata = ApplicantsAdmissionInformationDetailsController.FetchProgramme(UserName);
                    List<PartTermInst> PTData = ApplicantsAdmissionInformationDetailsController.FetchPartTerm(UserName);
                    List<Paper> PaperData = ApplicantsAdmissionInformationDetailsController.FetchPaper(UserName);
                    foreach (PartTermInst PT in PTData)
                    {
                        List<Paper> GPT = PaperData.Where(e => e.ProgrammeInstancePartTermId == PT.Id).ToList();
                        if (GPT.Any())
                        {
                            PT.PaperList = GPT;
                        }
                    }
                    foreach (Programme P in Programmedata)
                    {
                        List<PartTermInst> PT = PTData.Where(e => e.ProgrammeInstanceId == P.ProgrammeInstanceId).ToList();
                        if (PT.Any())
                        {
                            P.PTList = PT;
                        }

                    }
                   // return Return.returnHttp("200", Programmedata, null);
              
                
                ApplicantList applicantList = new ApplicantList();
                applicantList.PersonalList = ObjLstPI;
                applicantList.AdmissionList = ObjLstAdmission;
                applicantList.EducationList = ObjLstEducation;
                applicantList.RequestStatusList = ObjLstRequestStatus;
                applicantList.ProgrammeList = Programmedata;
                applicantList.PreExaminationList = ObjLstPreExamination;
                if (applicantList.PersonalList.Any())
                {
                    applicantList.PersonalListflag = true;
                }
                if (applicantList.AdmissionList.Any())
                {
                    applicantList.AdmissionListflag = true;
                }
                if (applicantList.EducationList.Any())
                {
                    applicantList.EducationListflag = true;
                }
                if (applicantList.RequestStatusList.Any())
                {
                    applicantList.RequestStatusListflag = true;
                }
                if (applicantList.ProgrammeList.Any())
                {
                    applicantList.ProgrammeListflag = true;
                }
                if (applicantList.PreExaminationList.Any())
                {
                    applicantList.PreExaminationListflag = true;
                }
                return Return.returnHttp("200", applicantList, null);

            }

            /*  return Return.returnHttp("201", "No Record Found", null);*/



            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }


         
        }
        
        
        #region FetchPartTerm
        public static List<PartTermInst> FetchPartTerm(Int64 PRN)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            List<PartTermInst> PTdata = new List<PartTermInst>();
            SqlCommand cmd = new SqlCommand();

            cmd = new SqlCommand("ApplicantIncProgInstAndIncProgInstPTGetByPRN", Con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PRN", PRN);

            SqlDataAdapter Da = new SqlDataAdapter();
            DataTable Dt = new DataTable();
            Da.SelectCommand = cmd;
            Da.Fill(Dt);
            if (Dt.Rows.Count > 0)
            {
                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    PartTermInst PT = new PartTermInst();
                    PT.Id = (((Dt.Rows[i]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["Id"]);
                    PT.InstancePartTermName = (Convert.ToString(Dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                    PT.ProgrammeInstanceId = (((Dt.Rows[i]["ProgrammeInstanceId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["ProgrammeInstanceId"]);
                    PT.InstanceName = (Convert.ToString(Dt.Rows[i]["InstanceName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstanceName"]);
                    TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime DOB = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(Dt.Rows[i]["CreatedOn"]).ToUniversalTime(), INDIAN_ZONE);
                    PT.CreatedOn = string.Format("{0: dd-MM-yyyy}", DOB);
                    PT.EligibilityByFaculty = (Convert.ToString(Dt.Rows[i]["EligibilityByFaculty"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["EligibilityByFaculty"]);
                    PT.EligibilityByAcademics = (Convert.ToString(Dt.Rows[i]["EligibilityByAcademics"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["EligibilityByAcademics"]);
                    PTdata.Add(PT);
                }
            }
            return PTdata;
        }
        #endregion
        #region FetchProgramme
        public static List<Programme> FetchProgramme(Int64 PRN)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            List<Programme> Programmedata = new List<Programme>();
            SqlCommand cmd = new SqlCommand();

            cmd = new SqlCommand("ApplicantIncProgInstGetByPRN", Con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PRN", PRN);

            SqlDataAdapter Da = new SqlDataAdapter();
            DataTable Dt = new DataTable();
            Da.SelectCommand = cmd;
            Da.Fill(Dt);
            if (Dt.Rows.Count > 0)
            {
                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    Programme PT = new Programme();
                    PT.ProgrammeInstanceId = (((Dt.Rows[i]["ProgrammeInstanceId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["ProgrammeInstanceId"]);
                    PT.InstanceName = (Convert.ToString(Dt.Rows[i]["InstanceName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstanceName"]);
                    Programmedata.Add(PT);
                }
            }
            return Programmedata;
        }
        #endregion
        #region FetchPaper
        public static List<Paper> FetchPaper(Int64 PRN)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            List<Paper> Paperdata = new List<Paper>();

            SqlCommand cmd = new SqlCommand("ApplicantPaperListGetByPRN", Con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PRN", PRN);

            SqlDataAdapter Da = new SqlDataAdapter();
            DataTable Dt = new DataTable();
            Da.SelectCommand = cmd;
            Da.Fill(Dt);
            if (Dt.Rows.Count > 0)
            {
                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    Paper Paper = new Paper();

                    Paper.Id = (((Dt.Rows[i]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["Id"]);
                    Paper.PaperId = (((Dt.Rows[i]["PaperId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["PaperId"]);
                    Paper.ProgrammeInstancePartTermId = (((Dt.Rows[i]["ProgrammeInstancePartTermId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["ProgrammeInstancePartTermId"]);
                    Paper.PaperName = (Convert.ToString(Dt.Rows[i]["PaperName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["PaperName"]);

                    Paperdata.Add(Paper);
                }
            }
            return Paperdata;
        }
        #endregion        
        #endregion

        #region ExamFormPrintData
        [HttpPost]
        public HttpResponseMessage ExamFormPrintData(ExamFormPrintData EFD)
        {
            try
            {
                Int64 ProgInstPartTermId = EFD.ProgInstPartTermId;
                Int64 PRN = EFD.PRN;
                Int64 ExamEventId = EFD.ExamEventId;


                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("ExamFormPrintData", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PRN", PRN);
                cmd.Parameters.AddWithValue("@ExamEventId", ExamEventId);
                cmd.Parameters.AddWithValue("@ProgInstPartTermId", ProgInstPartTermId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                BALCommon common = new BALCommon();

                string OldBaseUrlForPhoto = "https://admission.msubaroda.ac.in/MSUISApi/Upload/Photo/";
                //string NewBaseUrlForPhoto = "https://localhost:44374/Upload/Photo/";
                string NewBaseUrlForPhoto = "https://msuis.msubaroda.ac.in/Vidhyarthi_API/Upload/Photo/";

               

                string OldBaseUrlForSign = "https://admission.msubaroda.ac.in/MSUISApi/Upload/Signature/";
                //string NewBaseUrlForSign = "https://localhost:44374/Upload/Signature/";
                string NewBaseUrlForSign = "https://msuis.msubaroda.ac.in/Vidhyarthi_API/Upload/Signature/";

               

                List<ExamFormPrintData> ExamFormData = new List<ExamFormPrintData>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        ExamFormPrintData EFData = new ExamFormPrintData();
                        EFData.FullName = Convert.ToString(dr["LastName"]) + " " + Convert.ToString(dr["FirstName"]) + " " + Convert.ToString(dr["MiddleName"]);
                      
                        EFData.NameOfMother = (dr["NameOfMother"].ToString());
                        EFData.Gender = (dr["Gender"].ToString());
                        EFData.PermanentAddress = (dr["PermanentAddress"].ToString());
                        EFData.PermanentCityVillage = (dr["PermanentCityVillage"].ToString());
                        EFData.DistrictName = (dr["DistrictName"].ToString());
                        EFData.StateName = (dr["StateName"].ToString());
                        EFData.PermanentPincode = Convert.ToInt64(dr["PermanentPincode"]);
                        EFData.MobileNo = (dr["MobileNo"].ToString());
                        EFData.OptionalMobileNo = (dr["OptionalMobileNo"].ToString());
                        EFData.EmailId = (dr["EmailId"].ToString());
                        EFData.DOB = string.Format("{0: dd-MM-yyyy}", dr["DOB"]);
                        EFData.AppearanceType = (dr["AppearanceType"].ToString());
                        EFData.ApplicationReservationName = (dr["ApplicationReservationName"].ToString());
                        EFData.EligibilityByAcademics = (dr["EligibilityByAcademics"].ToString());
                        EFData.InstructionMediumName = (dr["InstructionMediumName"].ToString());
                        EFData.StudentSignature = (dr["StudentSignature"].ToString());
                        EFData.StudentPhoto = (dr["StudentPhoto"].ToString());
                        EFData.PRN = Convert.ToInt64(dr["PRN"]);
                        EFData.FormNo = Convert.ToInt64(dr["FormNo"]);
                        EFData.InstancePartTermName = (dr["InstancePartTermName"].ToString());
                        EFData.FacultyName = (dr["FacultyName"].ToString());
                        EFData.DisplayName = (dr["DisplayName"].ToString());

                        var IsPhysicallyChallenged = dr["IsPhysicallyChallenged"];
                        if (IsPhysicallyChallenged is DBNull)
                        {
                            EFData.IsPhysicallyChallenged = null;
                        }
                        else
                        {
                            EFData.IsPhysicallyChallenged = Convert.ToBoolean(dr["IsPhysicallyChallenged"]);
                        }

                        //==========Start Code for Fetching Photo from Vidhyarthi or Applicant Side============
                        string FullPathOld = OldBaseUrlForPhoto + EFData.StudentPhoto;
                        string FullPathNew = NewBaseUrlForPhoto + EFData.StudentPhoto;

                        bool FileResponseOldPath = common.ServerFileExists(FullPathOld);
                        bool FileResponseNewPath = common.ServerFileExists(FullPathNew);

                        if (FileResponseNewPath == true)
                        {
                            EFData.StudentPhoto = FullPathNew;
                        }
                        else if (FileResponseOldPath == true)
                        {
                            EFData.StudentPhoto = FullPathOld;
                        }
                        else
                        {
                            EFData.StudentPhoto = NewBaseUrlForPhoto + "DefaultPhoto.png";
                        }
                        //============End Code for Fetching Photo from Vidhyarthi or Applicant Side============

                        //============Start Code for Fetching Signature from Vidhyarthi or Applicant Side============
                        string FullPathOldSign = OldBaseUrlForSign + EFData.StudentSignature;
                        string FullPathNewSign = NewBaseUrlForSign + EFData.StudentSignature;

                        bool FileResponseOldPathSign = common.ServerFileExists(FullPathOldSign);
                        bool FileResponseNewPathSign = common.ServerFileExists(FullPathNewSign);

                        if (FileResponseNewPathSign == true)
                        {
                            EFData.StudentSignature = FullPathNewSign;
                        }
                        else if (FileResponseOldPathSign == true)
                        {
                            EFData.StudentSignature = FullPathOldSign;
                        }
                        else
                        {
                            EFData.StudentSignature = NewBaseUrlForSign + "DefaultPhoto.png";
                        }
                        //===========End Code for Fetching Signature from Vidhyarthi or Applicant Side============


                        ExamFormData.Add(EFData);
                    }

                }
                return Return.returnHttp("200", ExamFormData, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region ExamFormPrintDataPaperDetails
        [HttpPost]
        public HttpResponseMessage ExamFormPrintDataPaperDetails(ExamFormPrintDataPaperDetails EFDP)
        {
            try
            {
                Int64 ProgInstPartTermId = EFDP.ProgInstPartTermId;
                Int64 PRN = EFDP.PRN;
                Int64 ExamEventId = EFDP.ExamEventId;


                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("ExamFormPrintDataPaperDetails", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PRN", PRN);
                cmd.Parameters.AddWithValue("@ExamEventId", ExamEventId);
                cmd.Parameters.AddWithValue("@ProgInstPartTermId", ProgInstPartTermId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                Int32 count = 0;
                List<ExamFormPrintDataPaperDetails> ExamFormPaperData = new List<ExamFormPrintDataPaperDetails>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        ExamFormPrintDataPaperDetails EFData = new ExamFormPrintDataPaperDetails();
                        
                        EFData.PaperCode = (dr["PaperCode"].ToString());
                        EFData.PaperName = (dr["PaperName"].ToString());
                        EFData.TLMAMAT = (dr["TLMAMAT"].ToString());
                        count = count + 1;
                        EFData.IndexId = count;

                        ExamFormPaperData.Add(EFData);
                    }

                }
                return Return.returnHttp("200", ExamFormPaperData, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

      
    }

}

