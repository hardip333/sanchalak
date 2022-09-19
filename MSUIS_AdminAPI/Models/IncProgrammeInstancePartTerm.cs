using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class IncProgrammeInstancePartTerm
    {
        public Int64 Id { get; set; }


        public string InstancePartTermName { get; set; }

        public Int32 CodeId { get; set; }

        public Int64 ProgInstPartTermId { get; set; }

        public Int32 FacultyId { get; set; }
        public Int32 AcademicYearId { get; set; }

        public Int32 ProgrammeId { get; set; }

        public Int32 ProgrammePartId { get; set; }

        public Int32 SpecialisationId { get; set; }

        public Int32 ProgrammePartTermId { get; set; }
    }
}