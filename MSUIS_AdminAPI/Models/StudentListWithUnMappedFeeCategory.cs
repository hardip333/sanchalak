using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using java.net;
namespace MSUISApi.Models
{

    public class ApplicableFeeCategories
    {
        public int FeeCategoryId { get; set; }

        public string FeeCategoryName { get; set; }

        public int ProgrammeInstancePartTermId { get; set; }
    }
    public class StudentListWithUnMappedFeeCategory
    {
        public Int64 PRN { get; set; }

        public string StudentName { get; set; }

        public string gender { get; set; }

        public int oldAdmissionFeeCategoryId { get; set; }

        public int newAdmissionFeeCategoryId { get; set; }

        public string OldFeeCategoryName { get; set; }

        public int newProgrammePartTermInstance { get; set; }

        public List<ApplicableFeeCategories> ApplicableFeeCategories { get; set; }
    }

    public class studentListWithNewMappedFeeCategory
    {
        public Int64 PRN { get; set; }

        public int FeeCategoryId { get; set; }

        public int ProgrammeInstancePartTermId { get; set; }

    }
}