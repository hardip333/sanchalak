using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MSUISApi.BAL
{
    public class BALMeritListManualParameterConfiguration
    {
        public MeritListManualParameterConfiguration setRowIntoObjForAdmin(MeritListManualParameterConfiguration element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            element.MeritListInstanceId = Convert.ToInt32(dr["MeritListInstanceId"].ToString());
            element.MeritListInstance = dr["Name"].ToString();
            element.DisplayName = dr["DisplayName"].ToString();
            if (!string.IsNullOrEmpty(dr["Weightage"].ToString()))
                element.Weightage = (float)Convert.ToDecimal(dr["Weightage"].ToString());
            if (!string.IsNullOrEmpty(dr["Value"].ToString()))
                element.Value = (float)Convert.ToDecimal(dr["Value"].ToString());
            if (!string.IsNullOrEmpty(dr["Denominator"].ToString()))
                element.Denominator = (float)Convert.ToDecimal(dr["Denominator"].ToString());
            if (!string.IsNullOrEmpty(dr["Sequence"].ToString()))
                element.Sequence = Convert.ToInt16(dr["Sequence"].ToString());
            element.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
            return element;

        }

        public MeritListManualParameterConfiguration setRowIntoObjForUser(MeritListManualParameterConfiguration element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            element.MeritListInstanceId = Convert.ToInt32(dr["MeritListInstanceId"].ToString());
            element.MeritListInstance = dr["Name"].ToString();
            element.DisplayName = dr["DisplayName"].ToString();
            if (!string.IsNullOrEmpty(dr["Weightage"].ToString()))
                element.Weightage = (float)Convert.ToDecimal(dr["Weightage"].ToString());
            if (!string.IsNullOrEmpty(dr["Value"].ToString()))
                element.Value = (float)Convert.ToDecimal(dr["Value"].ToString());
            if (!string.IsNullOrEmpty(dr["Denominator"].ToString()))
                element.Denominator = (float)Convert.ToDecimal(dr["Denominator"].ToString());
            if (!string.IsNullOrEmpty(dr["Sequence"].ToString()))
                element.Sequence = Convert.ToInt16(dr["Sequence"].ToString());
            return element;
        }

        public MeritListManualParameterConfiguration setRowIntoObjForAll(MeritListManualParameterConfiguration element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            element.MeritListInstanceId = Convert.ToInt32(dr["MeritListInstanceId"].ToString());
            element.MeritListInstance = dr["Name"].ToString();
            element.DisplayName = dr["DisplayName"].ToString();
            if (!string.IsNullOrEmpty(dr["Weightage"].ToString()))
                element.Weightage = (float)Convert.ToDecimal(dr["Weightage"].ToString());
            if (!string.IsNullOrEmpty(dr["Value"].ToString()))
                element.Weightage = (float)Convert.ToDecimal(dr["Value"].ToString());
            if (!string.IsNullOrEmpty(dr["Denominator"].ToString()))
                element.Denominator = (float)Convert.ToDecimal(dr["Denominator"].ToString());
            if (!string.IsNullOrEmpty(dr["Sequence"].ToString()))
                element.Sequence = Convert.ToInt16(dr["Sequence"].ToString());
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

        public List<MeritListManualParameterConfiguration> getMeritListManualParameterConfiguration(string flag, int? IsDeleted, int? IsActive,int? MeritListInstanceId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("M_MeritListManualParameterConfigurationGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Flag", flag);
                Cmd.Parameters.AddWithValue("@IsActive", IsActive);
                Cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
                Cmd.Parameters.AddWithValue("@MeritListInstanceId", MeritListInstanceId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<MeritListManualParameterConfiguration> ObjListMeritListManualParameterConfiguration = new List<MeritListManualParameterConfiguration>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MeritListManualParameterConfiguration objMeritListManualParameterConfiguration = new MeritListManualParameterConfiguration();
                        if (flag == "admin")
                        {
                            setRowIntoObjForAdmin(objMeritListManualParameterConfiguration, Dt.Rows[i]);
                        }
                        else if (flag == "user")
                        {
                            setRowIntoObjForUser(objMeritListManualParameterConfiguration, Dt.Rows[i]);
                        }

                        ObjListMeritListManualParameterConfiguration.Add(objMeritListManualParameterConfiguration);
                    }
                }
                return ObjListMeritListManualParameterConfiguration;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}