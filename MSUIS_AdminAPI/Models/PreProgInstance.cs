using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class PreProgInstance
    {
        public Int64 ProgInstance { get; set; }
        public Int64 ProgInstancePartTerm { get; set; }
        public Int64 ProgInstancePart { get; set; }
        
        public string strMessage { get; set; }
        public string ProgInstanceName { get; set; }
        public string ProgrammeName { get; set; }
        public string ProgrammePartName { get; set; }
        public string ProgrammePartTermName { get; set; }
    }
}