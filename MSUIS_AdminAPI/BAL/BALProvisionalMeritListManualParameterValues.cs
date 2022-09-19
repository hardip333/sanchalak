using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MSUISApi.BAL
{
    public class BALProvisionalMeritListManualParameterValues
    {
        public ProvisionalMeritListManualParameterValues setRowIntoObjForAdmin(ProvisionalMeritListManualParameterValues element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            if (!string.IsNullOrEmpty(dr["MeritListInstanceId"].ToString()))
                element.MeritListInstanceId = Convert.ToInt32(dr["MeritListInstanceId"].ToString());
            element.MeritListInstance = dr["Name"].ToString();
            if (!string.IsNullOrEmpty(dr["ProvisionalMeritListId"].ToString()))
                element.ProvisionalMeritListId = Convert.ToInt16(dr["ProvisionalMeritListId"].ToString());
            if (!string.IsNullOrEmpty(dr["M_MeritListManualParaId"].ToString()))
                element.MeritListManualParameterId = Convert.ToInt16(dr["M_MeritListManualParaId"].ToString());
            element.DisplayName = dr["DisplayName"].ToString();
            if (!string.IsNullOrEmpty(dr["Value"].ToString()))
                element.Value = (float)Convert.ToDecimal(dr["Value"].ToString());
            return element;

        }

        public ProvisionalMeritListManualParameterValues setRowIntoObjForUser(ProvisionalMeritListManualParameterValues element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            if (!string.IsNullOrEmpty(dr["MeritListInstanceId"].ToString()))
                element.MeritListInstanceId = Convert.ToInt32(dr["MeritListInstanceId"].ToString());
            element.MeritListInstance = dr["Name"].ToString();
            if (!string.IsNullOrEmpty(dr["ProvisionalMeritListId"].ToString()))
                element.ProvisionalMeritListId = Convert.ToInt16(dr["ProvisionalMeritListId"].ToString());
            if (!string.IsNullOrEmpty(dr["M_MeritListManualParaId"].ToString()))
                element.MeritListManualParameterId = Convert.ToInt16(dr["M_MeritListManualParaId"].ToString());
            element.DisplayName = dr["DisplayName"].ToString();
            if (!string.IsNullOrEmpty(dr["Value"].ToString()))
                element.Value = (float)Convert.ToDecimal(dr["Value"].ToString());
            return element;
        }

        public ProvisionalMeritListManualParameterValues setRowIntoObjForAll(ProvisionalMeritListManualParameterValues element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            if (!string.IsNullOrEmpty(dr["MeritListInstanceId"].ToString()))
                element.MeritListInstanceId = Convert.ToInt32(dr["MeritListInstanceId"].ToString());
            element.MeritListInstance = dr["Name"].ToString();
            if (!string.IsNullOrEmpty(dr["ProvisionalMeritListId"].ToString()))
                element.ProvisionalMeritListId = Convert.ToInt16(dr["ProvisionalMeritListId"].ToString());
            if (!string.IsNullOrEmpty(dr["M_MeritListManualParaId"].ToString()))
                element.MeritListManualParameterId = Convert.ToInt16(dr["M_MeritListManualParaId"].ToString());
            element.DisplayName = dr["DisplayName"].ToString();
            if (!string.IsNullOrEmpty(dr["Value"].ToString()))
                element.Value = (float)Convert.ToDecimal(dr["Value"].ToString());
            element.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
            return element;
        }

        public List<ProvisionalMeritListManualParameterValues> getProvisionalMeritListManualParameterValues(string flag, int? IsDeleted, int? MeritListInstanceId,int? ProvisionalMeritListId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("M_ProvisionalMeritListManualParameterValuesGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Flag", flag);
                Cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
                Cmd.Parameters.AddWithValue("@MeritListInstanceId", MeritListInstanceId);
                Cmd.Parameters.AddWithValue("@ProvisionalMeritListId", ProvisionalMeritListId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<ProvisionalMeritListManualParameterValues> ObjListProvisionalMeritListManualParameterValues = new List<ProvisionalMeritListManualParameterValues>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ProvisionalMeritListManualParameterValues obj = new ProvisionalMeritListManualParameterValues();
                        if (flag == "admin")
                        {
                            setRowIntoObjForAdmin(obj, Dt.Rows[i]);
                        }
                        else if (flag == "user")
                        {
                            setRowIntoObjForUser(obj, Dt.Rows[i]);
                        }

                        ObjListProvisionalMeritListManualParameterValues.Add(obj);
                    }
                }
                return ObjListProvisionalMeritListManualParameterValues;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}