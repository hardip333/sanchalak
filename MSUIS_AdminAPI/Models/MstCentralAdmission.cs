using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class MstCentralAdmission
    {
        public Int32 Id { get; set; }
        public int IndexId { get; set; }
        public Int32 FacultyId { get; set; }
        public Int64 InstituteId { get; set; }
        public Int32 ProgrammeId { get; set; }
        public Int32 SpecialisationId { get; set; }
        public String IncProgramInstancePartTermName { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }

        public Int32 AcademicYearId { get; set; }
        public string FacultyName { get; set; }      
        public string ProgrammeName { get; set; }  
        public string BranchName { get; set; }  
        public string AcademicYearCode { get; set; }
       
        public string InstancePartTermName { get; set; }
        public Int32 AdmissionCommitteeId { get; set; }
        public string CommitteeName { get; set; }
        public string MeritNo { get; set; }
        public string AllotmentNo { get; set; }
        public string MobileNo { get; set; }
        public string ApplicantName { get; set; }
       
        public Int32 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int32 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
     

    }
}