using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class StudentWisePaperList
    {
        public Int64 PRN { get; set; }

        public string FullName { get; set; }

        public string MobileNo { get; set; }

        public string EmailId { get; set; }

        public string Paper1 { get; set; }

        public string Paper2 { get; set; }

        public string Paper3 { get; set; }

        public string Paper4 { get; set; }

        public string Paper5 { get; set; }

        public string Paper6 { get; set; }

        public string Paper7 { get; set; }

        public string Paper8 { get; set; }

        public string Paper9 { get; set; }

        public string Paper10 { get; set; }

        public string Paper11 { get; set; }

        public string Paper12 { get; set; }

        public string Paper13 { get; set; }

        public string Paper14 { get; set; }

        public string Paper15 { get; set; }

        public Int64 ProgrammeInstancePartTermId { get; set; }

        public string InstancePartTermName { get; set; }

        public Int32 AcademicYearId { get; set; }
        public string AcademicYearCode { get; set; }

        public Int32 InstituteId { get; set; }
        public string InstituteName { get; set; }
        public Int32 ProgrammeId { get; set; }
        
        public Int32 ProgrammePartId { get; set; }
        public string ProgrammeName { get; set; }
        public Int32 ProgrammeInstancePartId { get; set; }
        public string PartName { get; set; }
        public Int32 SpecialisationId { get; set; }
        public string BranchName { get; set; }

        public Int32 IndexId { get; set; }

    }
}