using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MSUISApi.BAL
{
    public class BALProvisionalMeritList
    {
        public ProvisionalMeritList setRowIntoObjForAdmin(ProvisionalMeritList element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            element.MeritListId = Convert.ToInt32(dr["MeritListInstanceId"].ToString());
            element.UserName = dr["UserName"].ToString();
            element.ApplocationFormNo = dr["ApplicationFormNo"].ToString();
            element.Gender = dr["Gender"].ToString();
            element.NameAsPerMarksheet = dr["NameAsPerMarksheet"].ToString();
            if (!string.IsNullOrEmpty(dr["ReservationCategoryId"].ToString())) { 
                element.ReservationCategoryId = Convert.ToInt32(dr["ReservationCategoryId"].ToString());
                element.ReservationCategory = dr["ApplicationReservationName"].ToString();
            }
            if (!string.IsNullOrEmpty(dr["ISEWS"].ToString()))
                element.IsEWS = Convert.ToBoolean(dr["ISEWS"].ToString());
            if (!string.IsNullOrEmpty(dr["SocialCategoryId"].ToString())) { 
                element.SocialCategoryId = Convert.ToInt32(dr["SocialCategoryId"].ToString());
                element.SocialCategory = dr["SocialCategoryName"].ToString();
            }
            if (!string.IsNullOrEmpty(dr["IsPhysicallyChallenged"].ToString()))
                element.IsPhysicallyChallanged = Convert.ToBoolean(dr["IsPhysicallyChallenged"].ToString());
            if (!string.IsNullOrEmpty(dr["LocalToVadodara"].ToString()))
                element.LocalToVadodara = Convert.ToBoolean(dr["LocalToVadodara"].ToString());

            element.LastQualifyingDegreeSchool = dr["school"].ToString();
            if (!string.IsNullOrEmpty(dr["schoolCity"].ToString()))
            {
                element.LastQualifyingDegreeSchoolCityId = Convert.ToInt32(dr["schoolCity"].ToString());
                element.LastQualifyingDegreeSchoolCity = dr["LQDegreeCity"].ToString();
            }
            element.HSCSchool = dr["HscSchool"].ToString();
            if (!string.IsNullOrEmpty(dr["Location"].ToString()))
            { 
                element.HSCSchoolCityId = Convert.ToInt32(dr["Location"].ToString());
                element.HSCSchoolCity = dr["locationCity"].ToString();
            }
            element.Mobile = dr["Mobile"].ToString();
            element.EmailId = dr["Email"].ToString();
            element.Address = dr["Address"].ToString();
            if (!string.IsNullOrEmpty(dr["StateId"].ToString()))
            { 
                element.StateId = Convert.ToInt32(dr["StateId"].ToString());
                element.State = dr["StateName"].ToString();
            }

            if (!string.IsNullOrEmpty(dr["WeightageValue"].ToString()))
                element.WeightageValue = (float)Convert.ToDecimal(dr["WeightageValue"].ToString());
            if (!string.IsNullOrEmpty(dr["IsSeatAllocated"].ToString()))
                element.IsSeatAllocated =Convert.ToBoolean(dr["IsSeatAllocated"].ToString());
            if (!string.IsNullOrEmpty(dr["AllocatedLocation"].ToString()))
                element.AllocatedLocationId =Convert.ToInt16(dr["AllocatedLocation"].ToString());
            element.AllocatedLocation =dr["AllocatedLocationName"].ToString();
            if (!string.IsNullOrEmpty(dr["AllocatedPreference"].ToString()))
                element.AllocatedPreferenceId = Convert.ToInt16(dr["AllocatedPreference"].ToString());
            element.AllocatedPreference = dr["AllocatedPreferenceName"].ToString();
            if (!string.IsNullOrEmpty(dr["AllocatedGenReservationCategoryId"].ToString()))
                element.AllocatedGenReservationCategoryId = Convert.ToInt16(dr["AllocatedGenReservationCategoryId"].ToString());
            element.AllocatedGenReservationCategory = dr["AllocatedGenReservationCategory"].ToString();


            //BALProvisionalMeritListManualParameterValues func1 = new BALProvisionalMeritListManualParameterValues();
            //element.ProvisionalMeritListManualParameterValuesLst = func1.getProvisionalMeritListManualParameterValues("admin",0,element.MeritListId,element.Id);

            //BALProvisionalMeritListEducationDetails func2 = new BALProvisionalMeritListEducationDetails();
            //element.ProvisionalMeritListEducationDetailsLst = func2.getProvisionalMeritListEducationDetails("admin",0,element.MeritListId,element.Id);

            //BALProvisionalMeritListAddOnInformation func3 = new BALProvisionalMeritListAddOnInformation();
            //element.ProvisionalMeritListAddOnInformationLst = func3.getProvisionalMeritListAddOnInformation("admin", 0, element.MeritListId, element.Id);

            return element;

        }

        public ProvisionalMeritList setRowIntoObjForUser(ProvisionalMeritList element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            element.MeritListId = Convert.ToInt32(dr["MeritListInstanceId"].ToString());
            element.UserName = dr["UserName"].ToString();
            element.ApplocationFormNo = dr["ApplicationFormNo"].ToString();
            element.Gender = dr["Gender"].ToString();
            element.NameAsPerMarksheet = dr["NameAsPerMarksheet"].ToString();
            element.ReservationCategoryId = Convert.ToInt32(dr["ReservationCategoryId"].ToString());
            element.IsEWS = Convert.ToBoolean(dr["ISEWS"].ToString());
            element.SocialCategoryId = Convert.ToInt32(dr["SocialCategoryId"].ToString());
            element.IsPhysicallyChallanged = Convert.ToBoolean(dr["IsPhysicallyChallenged"].ToString());
            element.LocalToVadodara = Convert.ToBoolean(dr["LocalToVadodara"].ToString());
            element.LastQualifyingDegreeSchool = dr["school"].ToString();
            if (!string.IsNullOrEmpty(dr["schoolCity"].ToString()))
                element.LastQualifyingDegreeSchoolCityId = Convert.ToInt32(dr["schoolCity"].ToString());
            element.HSCSchool = dr["HscSchool"].ToString();
            element.HSCSchoolCityId = Convert.ToInt32(dr["Location"].ToString());
            element.Mobile = dr["Mobile"].ToString();
            element.EmailId = dr["Email"].ToString();
            element.Address = dr["Address"].ToString();
            element.StateId = Convert.ToInt32(dr["StateId"].ToString());
            if (!string.IsNullOrEmpty(dr["WeightageValue"].ToString()))
                element.WeightageValue = (float)Convert.ToDecimal(dr["WeightageValue"].ToString());
            if (!string.IsNullOrEmpty(dr["IsSeatAllocated"].ToString()))
                element.IsSeatAllocated = Convert.ToBoolean(dr["IsSeatAllocated"].ToString());
            if (!string.IsNullOrEmpty(dr["AllocatedLocation"].ToString()))
                element.AllocatedLocationId = Convert.ToInt16(dr["AllocatedLocation"].ToString());
            element.AllocatedLocation = dr["AllocatedLocationName"].ToString();
            if (!string.IsNullOrEmpty(dr["AllocatedPreference"].ToString()))
                element.AllocatedPreferenceId = Convert.ToInt16(dr["AllocatedPreference"].ToString());
            element.AllocatedPreference = dr["AllocatedPreferenceName"].ToString();
            if (!string.IsNullOrEmpty(dr["AllocatedGenReservationCategoryId"].ToString()))
                element.AllocatedGenReservationCategoryId = Convert.ToInt16(dr["AllocatedGenReservationCategoryId"].ToString());
            element.AllocatedGenReservationCategory = dr["AllocatedGenReservationCategory"].ToString();
            return element;

        }

        public ProvisionalMeritList setRowIntoObjForAll(ProvisionalMeritList element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            element.MeritListId = Convert.ToInt32(dr["MeritListInstanceId"].ToString());
            element.UserName = dr["UserName"].ToString();
            element.ApplocationFormNo = dr["ApplicationFormNo"].ToString();
            element.Gender = dr["Gender"].ToString();
            element.NameAsPerMarksheet = dr["NameAsPerMarksheet"].ToString();
            element.ReservationCategoryId = Convert.ToInt32(dr["ReservationCategoryId"].ToString());
            element.IsEWS = Convert.ToBoolean(dr["ISEWS"].ToString());
            element.SocialCategoryId = Convert.ToInt32(dr["SocialCategoryId"].ToString());
            element.IsPhysicallyChallanged = Convert.ToBoolean(dr["IsPhysicallyChallenged"].ToString());
            element.LocalToVadodara = Convert.ToBoolean(dr["LocalToVadodara"].ToString());
            element.LastQualifyingDegreeSchool = dr["school"].ToString();
            if (!string.IsNullOrEmpty(dr["schoolCity"].ToString()))
                element.LastQualifyingDegreeSchoolCityId = Convert.ToInt32(dr["schoolCity"].ToString());
            element.HSCSchool = dr["HscSchool"].ToString();
            element.HSCSchoolCityId = Convert.ToInt32(dr["Location"].ToString());
            element.Mobile = dr["Mobile"].ToString();
            element.EmailId = dr["Email"].ToString();
            element.Address = dr["Address"].ToString();
            element.StateId = Convert.ToInt32(dr["StateId"].ToString());
            element.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
            element.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
            element.CreatedBy = Convert.ToInt32(dr["CreatedBy"].ToString());
            element.CreatedOn = Convert.ToDateTime(dr["CreatedOn"].ToString());
            if (!string.IsNullOrEmpty(dr["ModifiedBy"].ToString()))
                element.ModifiedBy = Convert.ToInt32(dr["ModifiedBy"].ToString());
            if (!string.IsNullOrEmpty(dr["ModifiedOn"].ToString()))
                element.ModifiedOn = Convert.ToDateTime(dr["ModifiedOn"].ToString());
         
            if (!string.IsNullOrEmpty(dr["WeightageValue"].ToString()))
                element.WeightageValue = (float)Convert.ToDecimal(dr["WeightageValue"].ToString());
            if (!string.IsNullOrEmpty(dr["IsSeatAllocated"].ToString()))
                element.IsSeatAllocated = Convert.ToBoolean(dr["IsSeatAllocated"].ToString());
            if (!string.IsNullOrEmpty(dr["AllocatedLocation"].ToString()))
                element.AllocatedLocationId = Convert.ToInt16(dr["AllocatedLocation"].ToString());
            element.AllocatedLocation = dr["AllocatedLocationName"].ToString();
            if (!string.IsNullOrEmpty(dr["AllocatedPreference"].ToString()))
                element.AllocatedPreferenceId = Convert.ToInt16(dr["AllocatedPreference"].ToString());
            element.AllocatedPreference = dr["AllocatedPreferenceName"].ToString();
            if (!string.IsNullOrEmpty(dr["AllocatedGenReservationCategoryId"].ToString()))
                element.AllocatedGenReservationCategoryId = Convert.ToInt16(dr["AllocatedGenReservationCategoryId"].ToString());
            element.AllocatedGenReservationCategory = dr["AllocatedGenReservationCategory"].ToString();

            return element;
        }


        public List<ProvisionalMeritList> getProvisionalMeritList(string flag, int? IsDeleted, int? IsActive, int? MeritListInstanceId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("M_ProvisionalMeritListGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Flag", flag);
                Cmd.Parameters.AddWithValue("@IsActive", IsActive);
                Cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
                Cmd.Parameters.AddWithValue("@MeritListInstanceId", MeritListInstanceId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<ProvisionalMeritList> ObjListProvisionalMeritList = new List<ProvisionalMeritList>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ProvisionalMeritList objProvisionalMeritList = new ProvisionalMeritList();
                        if (flag == "admin")
                        {
                            setRowIntoObjForAdmin(objProvisionalMeritList, Dt.Rows[i]);
                        }
                        else if (flag == "user")
                        {
                            setRowIntoObjForUser(objProvisionalMeritList, Dt.Rows[i]);
                        }

                        ObjListProvisionalMeritList.Add(objProvisionalMeritList);
                    }
                }
                return ObjListProvisionalMeritList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}