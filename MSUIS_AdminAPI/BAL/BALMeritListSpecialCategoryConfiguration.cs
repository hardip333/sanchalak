using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MSUISApi.BAL
{
    public class BALMeritListSpecialCategoryConfiguration
    {
        public MeritListSpecialCategoryConfiguration setRowIntoObjForAdmin(MeritListSpecialCategoryConfiguration element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            element.MeritListInstanceId = Convert.ToInt32(dr["MeritListInstanceId"].ToString());
            element.MeritListInstance = dr["Name"].ToString();
            if (!string.IsNullOrEmpty(dr["GenReservationCategoryId"].ToString()))
                element.GenReservationCategoryId = Convert.ToInt32(dr["GenReservationCategoryId"].ToString()); ;
            element.GenReservationCategory =dr["ApplicationReservationName"].ToString();
            if (!string.IsNullOrEmpty(dr["PhysicallyHandicapReservedPercentage"].ToString()))
                element.PhysicallyHandicapReservedPercentage =(float)Convert.ToDecimal(dr["PhysicallyHandicapReservedPercentage"].ToString());
            if (!string.IsNullOrEmpty(dr["EWSReservedPercentage"].ToString()))
                element.EWSReservedPercentage = (float)Convert.ToDecimal(dr["EWSReservedPercentage"].ToString());
            element.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
            return element;

        }

        public MeritListSpecialCategoryConfiguration setRowIntoObjForUser(MeritListSpecialCategoryConfiguration element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            element.MeritListInstanceId = Convert.ToInt32(dr["MeritListInstanceId"].ToString());
            element.MeritListInstance = dr["Name"].ToString();
            if (!string.IsNullOrEmpty(dr["GenReservationCategoryId"].ToString()))
                element.GenReservationCategoryId = Convert.ToInt32(dr["GenReservationCategoryId"].ToString()); ;
            element.GenReservationCategory = dr["ApplicationReservationName"].ToString();
            if (!string.IsNullOrEmpty(dr["PhysicallyHandicapReservedPercentage"].ToString()))
                element.PhysicallyHandicapReservedPercentage = (float)Convert.ToDecimal(dr["PhysicallyHandicapReservedPercentage"].ToString());
            if (!string.IsNullOrEmpty(dr["EWSReservedPercentage"].ToString()))
                element.EWSReservedPercentage = (float)Convert.ToDecimal(dr["EWSReservedPercentage"].ToString());
            element.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
            element.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
            return element;
        }

        public MeritListSpecialCategoryConfiguration setRowIntoObjForAll(MeritListSpecialCategoryConfiguration element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            element.MeritListInstanceId = Convert.ToInt32(dr["MeritListInstanceId"].ToString());
            element.MeritListInstance = dr["Name"].ToString();
            if (!string.IsNullOrEmpty(dr["GenReservationCategoryId"].ToString()))
                element.GenReservationCategoryId = Convert.ToInt32(dr["GenReservationCategoryId"].ToString()); ;
            element.GenReservationCategory = dr["ApplicationReservationName"].ToString();
            if (!string.IsNullOrEmpty(dr["PhysicallyHandicapReservedPercentage"].ToString()))
                element.PhysicallyHandicapReservedPercentage = (float)Convert.ToDecimal(dr["PhysicallyHandicapReservedPercentage"].ToString());
            if (!string.IsNullOrEmpty(dr["EWSReservedPercentage"].ToString()))
                element.EWSReservedPercentage = (float)Convert.ToDecimal(dr["EWSReservedPercentage"].ToString());
            element.CreatedOn = Convert.ToDateTime(dr["CreatedOn"].ToString());
            element.CreatedBy = Convert.ToInt32(dr["CreatedBy"].ToString());
            if (!string.IsNullOrEmpty(dr["ModifiedOn"].ToString()))
                element.ModifiedOn = Convert.ToDateTime(dr["ModifiedOn"].ToString());
            if (!string.IsNullOrEmpty(dr["ModifiedBy"].ToString()))
                element.ModifiedBy = Convert.ToInt32(dr["ModifiedBy"].ToString());
            element.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
            element.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
            return element;
        }

        public List<MeritListSpecialCategoryConfiguration> getMeritListSpecialCategoryConfigurationDetails(string flag, int? IsDeleted, int? IsActive, int? MeritListInstanceId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("M_MeritListSpecialCategoryConfigurationGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Flag", flag);
                Cmd.Parameters.AddWithValue("@IsActive", IsActive);
                Cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
                Cmd.Parameters.AddWithValue("@MeritListInstanceId", MeritListInstanceId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<MeritListSpecialCategoryConfiguration> ObjList = new List<MeritListSpecialCategoryConfiguration>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MeritListSpecialCategoryConfiguration objMeritListSpecialCategoryConfiguration = new MeritListSpecialCategoryConfiguration();
                        if (flag == "admin")
                        {
                            setRowIntoObjForAdmin(objMeritListSpecialCategoryConfiguration, Dt.Rows[i]);
                        }
                        else if (flag == "user")
                        {
                            setRowIntoObjForUser(objMeritListSpecialCategoryConfiguration, Dt.Rows[i]);
                        }

                        ObjList.Add(objMeritListSpecialCategoryConfiguration);
                    }
                }
                return ObjList;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}