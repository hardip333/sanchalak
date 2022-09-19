using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class getFeeDetailsinput
    { 
        public Int64 PRN { get; set; }

        public int AcademicYearId { get; set; }

        public int ProgrammeId { get; set; }

        public int ProgPartId { get; set; }

        public int ProgInstPartTermId  { get; set; }
    }

    public class progInstPartTermfromProgPart
    {
        public int ProgInstPartTermId { get; set; }

        public string InstancePartTermName { get; set; }
    }

    //public class applicableFeeCategories
    //{
    //    public int FeeCategoryId { get; set; }

    //    public string FeeCategoryName { get; set; }
    //}

    public class allFeeCategory
    {
        public int OldFeeCategoryId { get; set; }

        public string OldFeeCategoryName { get; set; }

        public int NewFeeCategoryId { get; set; }

        public string NewFeeCategoryName { get; set; }
    }
}