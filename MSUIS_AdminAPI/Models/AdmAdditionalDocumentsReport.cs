using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class AdmAdditionalDocumentsReport
    {
        public Int64 Id { get; set; }

        public Int64 AdmApplicationId { get; set; }

        public Int64 ProgrammeInstancePartTermId { get; set; }

        public Int32 FacultyId { get; set; }

        public string FacultyName { get; set; }
        public string ProgrammeName { get; set; }

        public string BranchName { get; set; }

        public string InstancePartTermName { get; set; }


        public string DocFileName { get; set; }

        public Int64 ApprovedByFaculty { get; set; }
        public DateTime ApprovedOnFaculty { get; set; }

        public string IsApprovedByFacultys { get; set; }

        public Boolean IsApprovedByFaculty { get; set; }
        public Boolean IsDeleted { get; set; }

    }
}