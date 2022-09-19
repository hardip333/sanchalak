using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MSUISApi.BAL
{
    public class BALMeritListConfiguration
    {
        public MeritListConfiguration setRowIntoObjForAdmin(MeritListConfiguration element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            element.MeritListId = Convert.ToInt32(dr["MeritListId"].ToString());
            element.FieldName = dr["FieldName"].ToString();
            element.DisplayName = dr["DisplayName"].ToString();
            element.Type = Convert.ToInt32(dr["Type"].ToString());
            element.FieldTableRef = dr["FieldTableRef"].ToString();
            if (!string.IsNullOrEmpty(dr["Weightage"].ToString()))
                element.Weightage = (float)Convert.ToDecimal(dr["Weightage"].ToString());
            element.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
           // element.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
            return element;

        }

        public MeritListConfiguration setRowIntoObjForUser(MeritListConfiguration element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            element.MeritListId = Convert.ToInt32(dr["MeritListId"].ToString());
            element.FieldName = dr["FieldName"].ToString();
            element.DisplayName = dr["DisplayName"].ToString();
            element.Type = Convert.ToInt32(dr["Type"].ToString());
            element.FieldTableRef = dr["FieldTableRef"].ToString();
            if (!string.IsNullOrEmpty(dr["Weightage"].ToString()))
                element.Weightage = (float)Convert.ToDecimal(dr["Weightage"].ToString());
            return element;
        }

        public MeritListConfiguration setRowIntoObjForAll(MeritListConfiguration element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            element.MeritListId = Convert.ToInt32(dr["MeritListId"].ToString());
            element.FieldName = dr["FieldName"].ToString();
            element.DisplayName = dr["DisplayName"].ToString();
            element.Type = Convert.ToInt32(dr["Type"].ToString());
            element.FieldTableRef = dr["FieldTableRef"].ToString();
            if (!string.IsNullOrEmpty(dr["Weightage"].ToString()))
                element.Weightage = (float)Convert.ToDecimal(dr["Weightage"].ToString());
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

        public List<MeritListConfiguration> getMeritListConfigurationList(string flag, int? IsDeleted, int? IsActive)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("M_MeritListConfigurationGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Flag", flag);
                Cmd.Parameters.AddWithValue("@IsActive", IsActive);
                Cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<MeritListConfiguration> ObjListMeritListConfiguration = new List<MeritListConfiguration>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MeritListConfiguration objMeritListConfiguration = new MeritListConfiguration();
                        if (flag == "admin")
                        {
                            setRowIntoObjForAdmin(objMeritListConfiguration, Dt.Rows[i]);
                        }
                        else if (flag == "user")
                        {
                            setRowIntoObjForUser(objMeritListConfiguration, Dt.Rows[i]);
                        }

                        ObjListMeritListConfiguration.Add(objMeritListConfiguration);
                    }
                }
                return ObjListMeritListConfiguration;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}