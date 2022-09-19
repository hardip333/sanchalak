using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class PreRequisitePaperList
    {
        public Int64 SourcePartTermId { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }        
        public String InstancePartTermName { get; set; }
        public Int64 SourcePaperId { get; set; }
        public String PaperName { get; set; }
        public Int32 IndexId { get; set; }           
        public String SourceInstancePartTermName { get; set; }
        public String DestiInstancePartTermName { get; set; }
        public String SourcePaperName { get; set; }
        public String DestiPaperName { get; set; }
        public String PreRequisiteLableName { get; set; }
        public String PreRequisiteTypeName { get; set; }
        public String Minimum { get; set; }
        public String Maximum { get; set; }
    }
}