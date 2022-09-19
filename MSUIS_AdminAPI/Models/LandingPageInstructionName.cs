using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class LandingPageInstructionName
    {
        public Int32 Id { get; set; }
        public string InstructionName { get; set; }
        public int CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }

        public Boolean IsActive { get; set; }

    }
}