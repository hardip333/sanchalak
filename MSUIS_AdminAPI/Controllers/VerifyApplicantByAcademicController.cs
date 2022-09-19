using MSUIS_TokenManager.App_Start;
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

namespace MSUISApi.Controllers
{
    public class VerifyApplicantByAcademicController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();
        VerifyApplicantProfileForm vApplicantDetail = new VerifyApplicantProfileForm();

        [HttpPost]
        public HttpResponseMessage getAdmApplicantRegistrationIdByAdmApplicationId(AdmApplicationApplicantDetails AppId)
        {
            AdmApplicationApplicantDetails modelobj = new AdmApplicationApplicantDetails();
            try
            {
                modelobj.Id = Convert.ToInt64(AppId.Id);
                SqlCommand cmd = new SqlCommand("AdmApplicantRegistrationGetByAdmApplicationId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", AppId.Id);
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", AppId.InstPartTermId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AppId.AcademicYearId);
                cmd.Parameters.AddWithValue("@EligibilityStatus", AppId.EligibilityStatus);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                List<AdmApplicationApplicantDetails> ObjLstApplicantDetail = new List<AdmApplicationApplicantDetails>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {

                        AdmApplicationApplicantDetails AppDetails = new AdmApplicationApplicantDetails();
                        AppDetails.Id = Convert.ToInt64(dr["Id"]);
                        AppDetails.AdmApplicantRegistrationId = Convert.ToInt64(dr["ApplicantRegistrationId"].ToString());
                        AppDetails.UserName = Convert.ToInt64(dr["UserName"].ToString());
                        AppDetails.InstPartTermId = Convert.ToInt64(dr["ProgrammeInstancePartTermId"].ToString());
                        AppDetails.AcademicYearId = Convert.ToInt32(dr["AcademicYearId"].ToString());
                        vApplicantDetail.CheckAdmDateMessage = Convert.ToString(dr["CheckAdmDateMessage"].ToString());
                        vApplicantDetail.objApplicant = GetPersonalInfo(AppDetails.AdmApplicantRegistrationId, AppDetails.Id);
                        vApplicantDetail.objEduLst = GetEducationDetails(AppDetails.UserName);
                        vApplicantDetail.objSubDocsLst = GetSubmittedDocuments(AppDetails.Id, AppDetails.AcademicYearId);
                        vApplicantDetail.objAdditionalDocsLst = GetAdditionalDocuments(AppDetails.Id);
                        vApplicantDetail.objAddOnDocsLst = GetAddOnDetails(AppDetails.Id);
                        vApplicantDetail.objFeeLst = GetFeeAttached(AppDetails.Id, AppDetails.InstPartTermId, AppDetails.EligibilityStatus);
                        vApplicantDetail.objAcademicInfo = GetAdmApplicationForAcademic(AppDetails.Id);
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

        public Applicant GetPersonalInfo(Int64 RegId, Int64 AppId)
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sda = new SqlDataAdapter();
            DataTable dt = new DataTable();
            VerifyApplicantProfileForm vApplicantDetail1 = new VerifyApplicantProfileForm();
            Applicant s = new Applicant();
            cmd.Parameters.AddWithValue("@Id", RegId);

            string serverUrl = "https://admission.msubaroda.ac.in/MSUISApi/Upload/";
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
                    if (DR1["NCLCerti"].ToString() != "" && DR1["NCLCerti"].ToString() != "Not Applicable")
                    {
                        string NCLCertiFilePath = serverUrl + "Documents/" + DR1["NCLCerti"].ToString();
                        s.NCLCerti = NCLCertiFilePath;
                    }
                    if (DR1["EWS_IADoc"].ToString() != "" && DR1["EWS_IADoc"].ToString() != "Not Applicable")
                    {
                        string EWS_IADocFilePath = serverUrl + "Documents/" + DR1["EWS_IADoc"].ToString();
                        s.EWS_IADoc = EWS_IADocFilePath;
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
                        vApplicantDetail1.objApplicant = s;
                        
                    }
                }
            }
            return (s);
        }

        public static List<AdmApplicationEducationDetails> GetEducationDetails(Int64 UserName)
        {
            //VerifyApplicantProfileForm vApplicantDetail1 = new VerifyApplicantProfileForm();
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
                    //aed.ExamPassYear = dr["ExamPassYear"].ToString();
                    if (dr["ExamPassYear"].ToString() != "")
                    {
                        aed.ExamPassYear = (dr["ExamPassYear"].ToString());
                    }
                    else
                    {
                        aed.ExamPassYear = "--";
                    }
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
                    //aed.AttachDocument = dr["AttachDocument"].ToString();

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
                    //aed.IsVerified = (dr["IsVerified"].ToString());
                    //if (dr["IsVerified"].ToString() == "")
                    //{
                    //    aed.IsVerified = "False";
                    //}

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

        public static List<AdmApplicantsSubmittedDocuments> GetSubmittedDocuments(Int64 AppId, Int32 AcademicYearId)
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("PostSubmittedDocumentsGetbyAcademic", con);
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

                    var IsVerifiedByAcademic = dr["IsVerifiedByAcademic"];
                    if (IsVerifiedByAcademic is DBNull)
                    {
                        ObjSubDocs.IsVerifiedByAcademic = null;
                    }
                    else
                    {
                        ObjSubDocs.IsVerifiedByAcademic = Convert.ToString(dr["IsVerifiedByAcademic"].ToString());
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
            SqlCommand cmd = new SqlCommand("PostAdmStudentAddOnInformationGet", con);
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
            VerifyApplicantProfileForm vApplicantDetail1 = new VerifyApplicantProfileForm();
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

        public static PostApplicantVerification GetAdmApplicationForAcademic(Int64 AppId)
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("PostAdmApplicationGetForAcademic", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", AppId);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            VerifyApplicantProfileForm vApplicantDetail1 = new VerifyApplicantProfileForm();
            PostApplicantVerification av = new PostApplicantVerification();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    //PostApplicantVerification av1 = new PostApplicantVerification();

                    av.Id = Convert.ToInt64(dr["Id"].ToString());
                    av.EligibilityByAcademics = Convert.ToString(dr["EligibilityByAcademics"].ToString());
                    av.InstituteName = Convert.ToString(dr["InstituteName"].ToString());
                    av.FeeCategoryName = Convert.ToString(dr["FeeCategoryName"].ToString());
                    av.GroupName = Convert.ToString(dr["GroupName"].ToString());
                    av.AdminRemarkByAcademics = Convert.ToString(dr["AdminRemarkByAcademics"].ToString());
                    var IsPRNGenerated = dr["IsPRNGenerated"];
                    if (IsPRNGenerated is DBNull)
                    {
                        av.IsPRNGenerated = null;
                    }
                    else
                    {
                        av.IsPRNGenerated = Convert.ToBoolean(dr["IsPRNGenerated"]);
                    }

                    vApplicantDetail1.objAcademicInfo = av;

                }
                //vApplicantDetail1.objFee = av;
            }
            return (av);

        }

    }
}










