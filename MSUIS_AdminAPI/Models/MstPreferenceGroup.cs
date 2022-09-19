using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class MstPreferenceGroup
    {
        public Int32 Id { get; set; }
        public string GroupName { get; set; }
        public string IsActiveSts { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }

        public Int32 FacultyId { get; set; }

        public Int32 PreferenceGroupId { get; set; }

        public string FacultyName { get; set; }


        //bhushan's Code Starts

        public Int32 AcademicYearId { get; set; }
        public Int32 ProgrammePartTermId { get; set; }
        public Int32 ProgrammePartId { get; set; }
        public Int32 ProgrammeId { get; set; }
        public Int32 SpecialisationId { get; set; }
    }
}