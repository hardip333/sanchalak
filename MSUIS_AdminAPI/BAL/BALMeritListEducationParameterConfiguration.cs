using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MSUISApi.BAL
{
    public class BALMeritListEducationParameterConfiguration
    {
        public MeritListEducationParameterConfiguration setRowIntoObjForAdmin(MeritListEducationParameterConfiguration element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            element.MeritListInstanceId = Convert.ToInt32(dr["MeritListInstanceId"].ToString());
            element.MeritListInstance = dr["Name"].ToString();
            element.FieldName = dr["FieldName"].ToString();
            if (!string.IsNullOrEmpty(dr["Weightage"].ToString()))
                element.Weightage = (float)Convert.ToDecimal(dr["Weightage"].ToString());
            if (!string.IsNullOrEmpty(dr["Level"].ToString()))
                element.level = Convert.ToInt16(dr["Level"].ToString());
            element.LevelName = dr["EligibleDegreeName"].ToString();
            if (!string.IsNullOrEmpty(dr["DenominatorType"].ToString()))
                element.DenominatorType = Convert.ToBoolean(dr["DenominatorType"].ToString());
            if (!string.IsNullOrEmpty(dr["DenominatorValue"].ToString()))
                element.DenominatorValue = (float)Convert.ToDecimal(dr["DenominatorValue"].ToString());
            element.DenominatorField = dr["DenominatorField"].ToString();
            if (!string.IsNullOrEmpty(dr["Sequence"].ToString()))
                element.Sequence = Convert.ToInt16(dr["Sequence"].ToString());
            element.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
            return element;

        }

        public MeritListEducationParameterConfiguration setRowIntoObjForUser(MeritListEducationParameterConfiguration element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            element.MeritListInstanceId = Convert.ToInt32(dr["MeritListInstanceId"].ToString());
            element.MeritListInstance = dr["Name"].ToString();
            element.FieldName = dr["FieldName"].ToString();
            if (!string.IsNullOrEmpty(dr["Weightage"].ToString()))
                element.Weightage = (float)Convert.ToDecimal(dr["Weightage"].ToString());
            if (!string.IsNullOrEmpty(dr["Level"].ToString()))
                element.level = Convert.ToInt16(dr["Level"].ToString());
            element.LevelName = dr["EligibleDegreeName"].ToString();
            if (!string.IsNullOrEmpty(dr["DenominatorType"].ToString()))
                element.DenominatorType = Convert.ToBoolean(dr["DenominatorType"].ToString());
            if (!string.IsNullOrEmpty(dr["DenominatorValue"].ToString()))
                element.DenominatorValue = (float)Convert.ToDecimal(dr["DenominatorValue"].ToString());
            element.DenominatorField = dr["DenominatorField"].ToString();
            if (!string.IsNullOrEmpty(dr["Sequence"].ToString()))
                element.Sequence = Convert.ToInt16(dr["Sequence"].ToString());
            return element;
        }

        public MeritListEducationParameterConfiguration setRowIntoObjForAll(MeritListEducationParameterConfiguration element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            element.MeritListInstanceId = Convert.ToInt32(dr["MeritListInstanceId"].ToString());
            element.MeritListInstance = dr["Name"].ToString();
            element.FieldName = dr["FieldName"].ToString();
            if (!string.IsNullOrEmpty(dr["Weightage"].ToString()))
                element.Weightage = (float)Convert.ToDecimal(dr["Weightage"].ToString());
            if (!string.IsNullOrEmpty(dr["Level"].ToString()))
                element.level = Convert.ToInt16(dr["Level"].ToString());
            element.LevelName = dr["EligibleDegreeName"].ToString();
            if (!string.IsNullOrEmpty(dr["DenominatorType"].ToString()))
                element.DenominatorType = Convert.ToBoolean(dr["DenominatorType"].ToString());
            if (!string.IsNullOrEmpty(dr["DenominatorValue"].ToString()))
                element.DenominatorValue = (float)Convert.ToDecimal(dr["DenominatorValue"].ToString());
            element.DenominatorField = dr["DenominatorField"].ToString();
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

        public List<MeritListEducationParameterConfiguration> getMeritListEducationParameterConfiguration(string flag, int? IsDeleted, int? IsActive,int? MeritListInstanceId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("M_MeritListEducationParameterConfigurationGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Flag", flag);
                Cmd.Parameters.AddWithValue("@IsActive", IsActive);
                Cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
                Cmd.Parameters.AddWithValue("@MeritListInstanceId", MeritListInstanceId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<MeritListEducationParameterConfiguration> ObjListMeritListEducationParameterConfiguration = new List<MeritListEducationParameterConfiguration>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MeritListEducationParameterConfiguration objMeritListEducationParameterConfiguration = new MeritListEducationParameterConfiguration();
                        if (flag == "admin")
                        {
                            setRowIntoObjForAdmin(objMeritListEducationParameterConfiguration, Dt.Rows[i]);
                        }
                        else if (flag == "user")
                        {
                            setRowIntoObjForUser(objMeritListEducationParameterConfiguration, Dt.Rows[i]);
                        }

                        ObjListMeritListEducationParameterConfiguration.Add(objMeritListEducationParameterConfiguration);
                    }
                }
                return ObjListMeritListEducationParameterConfiguration;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}