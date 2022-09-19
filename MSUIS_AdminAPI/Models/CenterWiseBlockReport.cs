using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class CenterWiseBlockReport
    {

        public Int32 ExamMasterId { get; set; }
        public Int32 ExamVenueId { get; set; }
        public Int32 ExamVenueExamCenterId { get; set; }

        public Int32 ExamSlotId { get; set; }
        public Int32 IndexId { get; set; }
        public string DisplayName { get; set; }     
        public string CenterName { get; set; }
     
        public string SlotName { get; set; }
        public string ExameventName { get; set; }
        public string ExamVenueName { get; set; }
       
        public string Date { get; set; }
        public string BlockName { get; set; }
        public string PaperCode { get; set; }
        public string PaperName { get; set; }
        public Int64 StudentCount { get; set; }
        
                        
                      

      
    }
}