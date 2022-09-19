using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    #region DualAdmission
    public class DualAdmission
    {

        public Int64 PRN { get; set; }
        public Int64 ApplicationId { get; set; }
        public string MobileNo { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public String FullName { get; set; }
        public String Message1 { get; set; }
        public string FacultyName { get; set; }
        public String InstancePartTermName { get; set; }
        public Int64 IndexId { get; set; }

    }
    #endregion

    public class DualProgrammeList
    {
        public Int64 Id { get; set; }
        public Int64 PRN { get; set; }
        public Int64 DestinationIncProgInstPartTermId { get; set; }
        public string InstancePartTermName { get; set; }
        public Int64 UserId { get; set; }
    }

    public class DualProgrammeUpdate
    {
        public Int64 ApplicationId { get; set; }
        public Int64 PRN { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public string DualAdmissionRemark { get; set; }
        public string DualAdmissionDoc { get; set; }
        public Int64 CreatedBy { get; set; }
    }

    public class TransferProgrammeUpdate
    {
        public Int64 ApplicationId { get; set; } 
        public Int64 OldApplicationId { get; set; }
        public Int64 PRN { get; set; }
        public Int64 OldProgrammeInstancePartTermId { get; set; }
        public string DualAdmissionRemark { get; set; }
        public string DualAdmissionDoc { get; set; }
        public Int64 CreatedBy { get; set; }
    }

}