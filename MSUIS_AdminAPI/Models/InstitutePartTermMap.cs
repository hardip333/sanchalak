using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{

    #region InstitutePartTermMap
    public class InstitutePartTermMap
    {
        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string InstituteName { get; set; }
        public Int64 ProgramInstancePartTermId { get; set; }
        public string InstancePartTermName { get; set; }
        public Int64 PaperId { get; set; }

        public string PaperName { get; set; }
        public int FacultyId { get; set; }
        public int ProgrammeId { get; set; }

        public int AcademicYearId { get; set; }

        public string FacultyName { get; set; }

        public string ProgrammeName { get; set; }

        public string AcademicYearCode { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }

        public string IsActiveSts { get; set; }

        public List<ProgInstPTData> ProgList { get; set; }

        public int count { get; set; }

        public int IndexId { get; set; }
    }
    #endregion

    public class ProgInstPTData
    {
        public Int32 IncProgInstancePartTermId { get; set; }
        public Int32 InstitutePartTermMapId { get; set; }
        public Boolean ProgChecked { get; set; }
    }

}