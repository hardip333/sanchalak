using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class ApplicantFormDocument
    {

        public Int64 Id { get; set; }
        public Int64 AdmApplicantRegistrationId { get; set; }
        public Int64 InstPartTermId { get; set; }
        public Int64 UserName { get; set; }
        public string EligibilityStatus { get; set; }
        public Int32 AcademicYearId { get; set; }
        public List<BulkIdGet> GetId { get; set; }
        public String PreErr { get; set; }

    }

    public class BulkIdGet
    {
        public Int64 AdmApplicantRegistrationId { get; set; }
        public Int64 Id { get; set; }
    }

    public class ApplicantFormAllDocument
    {
        public Int64 Id { get; set; }
        public Int64 AdmApplicantRegistrationId { get; set; }
        public Int64 InstPartTermId { get; set; }
        public Int64 UserName { get; set; }
        public string EligibilityStatus { get; set; }
        public Int32 AcademicYearId { get; set; }
        public List<BulkIdGet> GetId { get; set; }
        public String PreErr { get; set; }
        public List<Applicant> objBulkApplicant { get; set; }
        public Applicant objApplicant { get; set; }
        public String CheckAdmDateMessage { get; set; }
        //public Int64 InstPartTermId { get; set; }
        
        public List<ProfileForm> objFeePayCourseList { get; set; }
        public List<ProfileForm> AdmApplicantRegistrationList { get; set; }
        public List<ProfileForm> BulkAdmApplicantRegistrationList { get; set; }
        public List<ProfileForm> ProfileEducationDetailsList { get; set; }
        public List<ProfileForm> BulkProfileEducationDetailsList { get; set; }
        public List<AdmApplicationEducationDetails> objEduLst { get; set; }
        public List<AdmApplicationEducationDetails> objBulkEduLst { get; set; }
        public List<AdmApplicantsSubmittedDocuments> objSubDocsLst { get; set; }
        public List<AdmApplicantsSubmittedDocuments> objBulkSubDocsLst { get; set; }

        public List<AdmApplicantAdditionalDocument> objAdditionalDocsLst { get; set; }
        public List<AdmApplicantAdditionalDocument> objBulkAdditionalDocsLst { get; set; }

        public List<AdmStudentAddOnInformation> objAddOnDocsLst { get; set; }
        public List<AdmStudentAddOnInformation> objBulkAddOnDocsLst { get; set; }
        public List<ApplicationInstitutePreferenceReport> objPreferenceLst { get; set; }
        public List<ApplicationInstitutePreferenceReport> objInstituteLst { get; set; }

        public List<PostApplicantVerification> objFeeLst { get; set; }
        public PostApplicantVerification objAdmApplicationinfo { get; set; }
        public PostApplicantVerification objAcademicInfo { get; set; }
    }


}