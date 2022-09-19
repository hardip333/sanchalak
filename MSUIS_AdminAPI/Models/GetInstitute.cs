using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{

    public class GetInstitute
    {
        public int InstituteId { get; set; }

        public string InstituteName { get; set; }
    }

    public class studentCountParameters
    {
        public int AcademicYearId { get; set; }

        public int InstituteId { get; set; }

        public int ProgrammeId { get; set; }

        public int ProgPartId { get; set; }

        public int isEligConsidered { get; set; }

    }

    public class studentListCountforBranch
    {
        public bool checkvalue { get; set; }
        public int SpecialisationId { get; set; }
        public string BranchName { get; set; }
        public int unmappedstudentCount { get; set; }
        public int mappedstudentcount { get; set; }

    }

    public class studentListStoring
    {
        public string admlevel { get; set; }

        public List<studentListCountforBranch> studentCount { get; set; }

        public bool isEligConsidered { get; set; }

        public studentCountParameters objstudentCountParameters { get; set; }
    }
}