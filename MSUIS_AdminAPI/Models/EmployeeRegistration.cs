using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class EmployeeRegistration
    {
      
            public Int64 Id { get; set; }
            public Int64 UserTypeId { get; set; }
            public String LastName { get; set; }
            public String FirstName { get; set; }
            public String MiddleName { get; set; }

            public Int64? MaritalStatusId { get; set; }
            public String MaritalStatus { get; set; }

            public Int64? BloodGroupId { get; set; }
            public String BloodGroupName { get; set; }

            public String AadharCardNo { get; set; }

            public String PermanentAddress { get; set; }
            public Int64 PermanentCountryId { get; set; }
            public String PermanentCountry { get; set; }
            public Int64 PermanentStateId { get; set; }
            public String PermanentState { get; set; }
            public Int64 PermanentDistrictId { get; set; }
            public String PermanentDistrict { get; set; }
            public String PermanentCityVillage { get; set; }
            public Int64? PermanentPincode { get; set; }
            public Boolean? IsCurrentAsPermanent { get; set; }
            public String CurrentAddress { get; set; }
            public Int64? CurrentCountryId { get; set; }
            public String CurrentCountry { get; set; }
            public Int64? CurrentStateId { get; set; }
            public String CurrentState { get; set; }
            public Int64? CurrentDistrictId { get; set; }
            public String CurrentDistrict { get; set; }
            public String CurrentCityVillage { get; set; }
            public Int64? CurrentPincode { get; set; }

            public String EmailId { get; set; }
            public String MobileNo { get; set; }
            public String VoterId { get; set; }
            public String PanCardNo { get;set;}
           
            public String NatureofAppointment { get; set; }
            public Int64 CreatedBy { get; set; }
            public DateTime CreatedOn { get; set; }
            public bool IsDeleted { get; set; }
            public String UserTypeName { get; set; }
            public String FullName { get; set; }
            public DateTime ? BirthDate { get; set; }
            public string BirthDateView { get; set; }
            public String WhatsAppMobileNo { get; set; }
            public Int64 IndexId { get; set; }
            public Int64? SocialCategoryId { get; set; }
            public String SocialCategoryName { get; set; }
            public Int64 ModifiedBy { get; set; }
            public DateTime ModifiedOn { get; set; }

    }

    public class GenApplicationUnderCategory
    {
        public int Id { get; set; }
        public string ApplicationReservationCode { get; set; }
        public string ApplicationReservationName { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedOn{ get; set; }
        public int ModifiedBy { get; set; }
        public Boolean IsDeleted { get; set; }
        public Boolean IsActive { get; set; }
    }
}