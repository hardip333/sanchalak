using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    #region CountryMaster
    //public class CountryMaster
    //{
    //    public Int64 Id { get; set; }
    //    public string Alpha2Code { get; set; }
    //    public string Alpha3Code { get; set; }
    //    public string CountryName { get; set; }
    //    public int CreatedOn { get; set; }
    //    public int CreatedBy { get; set; }
    //    public string ModifiedOn { get; set; }
    //    public int ModifiedBy { get; set; }
    //    public bool IsDeleted { get; set; }
    //}
    #endregion

    #region StateMaster
    public class StateMaster
    {
        public Int64 Id { get; set; }
        public string StateName { get; set; }
        public Int64 CountryId { get; set; }

        public string CountryName { get; set; }
        public int CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
    #endregion

}