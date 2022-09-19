using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class StudentQueryMasterFaculty
    {
        public Int32 Id
        { get; set; }
        public Int64 ApplicantRegistrationId
        { get; set; }
        public Int64 UserName
        { get; set; }
        public int IndexId
        { get; set; }
        public string FirstName
        { get; set; }
        public string LastName
        { get; set; }
        public string EmailId
        { get; set; }
        public string DifficultyIn
        { get; set; }

        public string MobileNo
        { get; set; }
       
        public string DetailInBrief
        { get; set; }
        public string UploadImage
        { get; set; }

        public string FacultyRemarks
        { get; set; }

        public string RemarkStatus
        { get; set; }

        public string CreatedOn
        { get; set; }
    }
}