using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MSUISApi.Models
{
    public class AdmApplicationEducationDetails
    {
        public Int32 Id { get; set; }
        public Int64 ApplicantRegId { get; set; }
        public Int32 AdmApplicantRegistrationId { get; set; }
        public Int32 EligibleDegreeId { get; set; }
        public Int32 SpecializationId { get; set; }
        public Int32 ExaminationBodyId { get; set; }
        public string InstituteAttended { get; set; }
        public Int32 InstituteCityId { get; set; }
        public string ExamPassMonth { get; set; }
        public string ExamPassYear { get; set; }

        [Required(ErrorMessage = "ExamSeatNumber is required")]
        public string ExamSeatNumber { get; set; }
        public string ExamCertificateNumber { get; set; }
        public Int32? MarkObtained { get; set; }
        public Int32? MarkOutof { get; set; }
        public string Grade { get; set; }
        public decimal? CGPA { get; set; }
        public decimal? PercentageEquivalenceCGPA { get; set; }

        public decimal? Percentage { get; set; }
        public Int32? ClassId { get; set; }
        public bool? IsFirstTrial { get; set; }
        public Int32? NoOfTrails { get; set; }
        public bool? IsLastQualifyingExam { get; set; }
        public Int32? TeachingLanguageId { get; set; }
        public string ResultStatus { get; set; }
        public string AttachDocument { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int32 CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Int32 ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string EligibleDegreeName { get; set; }
        public string SpecializationName { get; set; }
        public string ExaminationBodyName { get; set; }
        public string CityName { get; set; }
        public string ExamPassCity { get; set; }
        public string LanguageName { get; set; }
        public string ClassName { get; set; }
        public string OtherCity { get; set; }
        public bool? IsDeclared { get; set; }
        public string IsVerified { get; set; }
        public string SchoolNo { get; set; }
        public Int64 AdmApplicantRegiUserName { get; set; }
    }
   
}