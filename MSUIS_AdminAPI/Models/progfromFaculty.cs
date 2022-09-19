﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    
    public class progfromFaculty
    {
        public int ProgrammeId { get; set; }

        public string ProgrammeName { get; set; }
    }

    public class progPartfromProg
    {
        public int ProgPartId { get; set; }

        public string PartName { get; set; }
    }

    public class progPartTermfromProgPart
    {
        public int ProgPartTermId { get; set; }

        public string PartTermName { get; set; }
    }

    public class checkForExistingRecords
    {
        public string TypeforEligibilty { get; set; }

        public string Name { get; set; }

        public string Name1 { get; set; }

        public string ResultStatus { get; set; }

        //public string PartShortName { get; set; }
    }

    public class eligibilityCriteriaDetails
    {
        public int AcademicYearId { get; set; }

        public int FacultyId { get; set; }

        public int ProgrammeId { get; set; }

        public int ProgPartId { get; set; }

        public int ProgPartTermId { get; set; }

        public string radioValue { get; set; }
        public bool isEligConsidered { get; set; }
        public bool isPaperSelectionByStudent { get; set; }
        public bool IsPaperSelectionBeforeFees { get; set; }

        // public string configLevel { get; set; }
    }

    public class eligibilityCriteriaSelected
    {
        public string CurrentName { get; set; }

        public string PreviousNames { get; set; }

        public int ProgPartId { get; set; }

        public int ProgPartTermId { get; set; }

        public bool flagforchecked { get; set; }

        public string ResultStatus { get; set; }
    }

    public class sendSelectedData
    {
        public string PreviousName { get; set; }
        public int ProgPartIdforDb { get; set; }
        public int ProgPartTermIdforDb { get; set; }
        public string eligibilityLabel { get; set; }
        public bool EligStatus { get; set; }
    }

    public class ParentData
    {
        public int childId { get; set; }
        public string childName { get; set; }
        public int ParentId { get; set; }
        public string ParentName { get; set; }
        public bool isPart { get; set; }
        public bool isEligConsidered { get; set; }
        public bool isPaperSelectionByStudent { get; set; }
        public bool IsPaperSelectionBeforeFees { get; set; }
        public List<sendSelectedData> dataToBeSent { get; set; }

    }

    public class dataSending
    {
        public List<ParentData> parentData { get; set; }
        public eligibilityCriteriaDetails forDatabase { get; set; }
        //public List<sendSelectedData> dataToBeSent { get; set; }
    }
}