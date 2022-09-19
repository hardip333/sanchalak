using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    #region MstUniversitySection
    public class MstUniversitySection
    {
        public Int32 Id { get; set; }
        public String UniversitySectionName { get; set; }
        public String UniversitySectionCode { get; set; }
        public String UniversitySectionAddress { get; set; }
        public String CityName { get; set; }
        public Int32 Pincode { get; set; }
        public string UniversitySectionContactNo { get; set; }
        public string UniversitySectionFaxNo { get; set; }
        public String UniversitySectionEmail { get; set; }
        public String UniversitySectionUrl { get; set; }
        public Boolean IsActive { get; set; }
        public string IsActiveSts { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsDeleted { get; set; }
    }
    #endregion

}
