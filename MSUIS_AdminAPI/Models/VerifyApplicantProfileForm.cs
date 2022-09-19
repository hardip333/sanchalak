using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    
    public class VerifyApplicantProfileForm
    {
        public Applicant objApplicant { get; set; }
        public String CheckAdmDateMessage { get; set; }
        public Int64 InstPartTermId { get; set; }
        public List<AdmApplicationEducationDetails> objEduLst { get; set; }
        public List<AdmApplicantsSubmittedDocuments> objSubDocsLst { get; set; }

        public List<AdmApplicantAdditionalDocument> objAdditionalDocsLst { get; set; }

        public List<AdmStudentAddOnInformation> objAddOnDocsLst { get; set; }

        public List<PostApplicantVerification> objFeeLst { get; set; }
        public PostApplicantVerification objAdmApplicationinfo { get; set; }
        public PostApplicantVerification objAcademicInfo { get; set; }
    }
    public class AdmApplicationApplicantDetails
    {
        
        public Int64 Id { get; set; }
        public Int64 AdmApplicantRegistrationId { get; set; }
        public Int64 InstPartTermId { get; set; }
        public Int32 AcademicYearId { get; set; }
        public Int64 UserName { get; set; }
        public string EligibilityStatus { get; set; }

    }

}