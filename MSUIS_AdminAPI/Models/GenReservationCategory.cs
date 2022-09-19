using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class GenReservationCategory
    {
        public Int32 Id { get; set; }
        public string ApplicationReservationCode { get; set; }
        public string ApplicationReservationName { get; set; }
        public int Sequence { get; set; }
       
    }
}