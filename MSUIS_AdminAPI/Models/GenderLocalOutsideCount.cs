using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class GenderLocalOutsideCount
    {
        public string LocalVadodaraMale { get; set; }
        public string OutSideVadodaraMale { get; set; }

        public string LocalGujaratMale { get; set; }
        public string OutSideGujaratMale { get; set; }

        public string LocalVadodaraFemale { get; set; }
        public string OutSideVadodaraFemale { get; set; }

        public string LocalGujaratFemale { get; set; }
        public string OutSideGujaratFemale { get; set; }

        public string LocalVadodaraTransGender { get; set; }
        public string OutSideVadodaraTransGender { get; set; }

        public string LocalGujaratTransgender { get; set; }
        public string OutSideGujaratTransgender { get; set; }

        public string TotalStudents { get; set; }
        public string Year { get; set; }
        public Int32 AcademicYearId { get; set; }

        public Int32 FacultyId { get; set; }

        public Int32 IndexId { get; set; }

        

    }
}