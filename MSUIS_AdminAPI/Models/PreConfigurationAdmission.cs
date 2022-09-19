using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class PreConfigurationAdmission
    {
      public  int ProgrammeId
       { get; set; }
      public int TotalProgramme
        { get; set; }
        public int InstituteId
        { get; set; }
        public int TotalApplicantsApproved
        { get; set; }
        public int TotalApplicantsFeesPaid
        { get; set; }
        public int TotalApplicants
        { get; set; }
        public int FacultyId
        { get; set; }
      public  int AdmApplicationId
       { get; set; }
      public int  AdmApplicationApprovedId
        { get; set; }
      public int AdmApplicationFeesPaidId
        { get; set; }
    }
}