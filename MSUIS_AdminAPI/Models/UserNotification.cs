using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class UserNotification
    {
        public Int64 Id { get; set; }

        public Int32 FacultyId { get; set; }

        public Int64 IncInstancePartTermId { get; set; }

        public Int32 ProgrammeId { get; set; }

        public Int32 NotificationTypeId { get; set; }

        public string NotificationTypeName { get; set; }

        public string FacultyName { get; set; }

        public string InstancePartTermName { get; set; }

        public string ProgrammeName { get; set; }

        public string NotificationDescription { get; set; }

        public string NotificationUserType { get; set; }

        public Boolean NotificationUserType1 { get; set; }

        public Boolean NotificationUserType2 { get; set; }

        public Boolean NotificationUserType3 { get; set; }

        public string NotificationFile { get; set; }


        public DateTime NotificationStartDate { get; set; }

        public DateTime NotificationEndDate { get; set; }

        public string NotificationStartDateView { get; set; }
        public string NotificationEndDateView { get; set; }

        public string NotificationFor { get; set; }

        public bool IsActive { get; set; }
        public Int32 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int32 ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }





    }
}