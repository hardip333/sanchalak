using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class MstUniversitySectionNew
    {
        public Int32 Id { get; set; }
        public String UniversitySectionName { get; set; }
        public String UniversitySectionCode { get; set; }
        public String UniversitySectionAddress { get; set; }
        public String CityName { get; set; }
        public Int32 Pincode { get; set; }
        public String UniversitySectionContactNo { get; set; }
        public String UniversitySectionFaxNo { get; set; }
        public String UniversitySectionEmail { get; set; }
        public String UniversitySectionUrl { get; set; }
        public bool ? IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}