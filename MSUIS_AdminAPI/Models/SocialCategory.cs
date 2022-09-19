using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class SocialCategory
    {
        public Int64 Id { get; set; }
        public string SocialCategoryName { get; set; }
        public string SocialCategoryCode { get; set; }
        public int CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}