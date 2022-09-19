using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MSUISApi.BAL
{
    public class BALProvisionalMeritListAddOnInformation
    {
        public ProvisionalMeritListAddOnInformation setRowIntoObjForAdmin(ProvisionalMeritListAddOnInformation element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            if (!string.IsNullOrEmpty(dr["MeritListInstanceId"].ToString()))
                element.MeritListInstanceId = Convert.ToInt32(dr["MeritListInstanceId"].ToString());
            element.MeritListInstance = dr["Name"].ToString();
            if (!string.IsNullOrEmpty(dr["ProvisionalMeritListId"].ToString()))
                element.ProvisionalMeritListId = Convert.ToInt16(dr["ProvisionalMeritListId"].ToString());
            if (!string.IsNullOrEmpty(dr["ApplicationId"].ToString()))
                element.ApplicationId = Convert.ToInt64(dr["ApplicationId"].ToString());
            if (!string.IsNullOrEmpty(dr["AddOnValue"].ToString()))
                element.AddOnValue = (float)Convert.ToDecimal(dr["AddOnValue"].ToString());
            if (!string.IsNullOrEmpty(dr["ProgrammeAddOnCriteriaId"].ToString()))
                element.ProgrammeAddOnCriteriaId = Convert.ToInt16(dr["ProgrammeAddOnCriteriaId"].ToString());
            element.ProgrammeAddOnCriteria = dr["TitleName"].ToString();
            return element;

        }

        public ProvisionalMeritListAddOnInformation setRowIntoObjForUser(ProvisionalMeritListAddOnInformation element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            if (!string.IsNullOrEmpty(dr["MeritListInstanceId"].ToString()))
                element.MeritListInstanceId = Convert.ToInt32(dr["MeritListInstanceId"].ToString());
            element.MeritListInstance = dr["Name"].ToString();
            if (!string.IsNullOrEmpty(dr["ProvisionalMeritListId"].ToString()))
                element.ProvisionalMeritListId = Convert.ToInt16(dr["ProvisionalMeritListId"].ToString());
            if (!string.IsNullOrEmpty(dr["ApplicationId"].ToString()))
                element.ApplicationId = Convert.ToInt64(dr["ApplicationId"].ToString());
            if (!string.IsNullOrEmpty(dr["AddOnValue"].ToString()))
                element.AddOnValue = (float)Convert.ToDecimal(dr["AddOnValue"].ToString());
            if (!string.IsNullOrEmpty(dr["ProgrammeAddOnCriteriaId"].ToString()))
                element.ProgrammeAddOnCriteriaId = Convert.ToInt16(dr["ProgrammeAddOnCriteriaId"].ToString());
            element.ProgrammeAddOnCriteria = dr["TitleName"].ToString();
            return element;
        }

        public ProvisionalMeritListAddOnInformation setRowIntoObjForAll(ProvisionalMeritListAddOnInformation element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            if (!string.IsNullOrEmpty(dr["MeritListInstanceId"].ToString()))
                element.MeritListInstanceId = Convert.ToInt32(dr["MeritListInstanceId"].ToString());
            element.MeritListInstance = dr["Name"].ToString();
            if (!string.IsNullOrEmpty(dr["ProvisionalMeritListId"].ToString()))
                element.ProvisionalMeritListId = Convert.ToInt16(dr["ProvisionalMeritListId"].ToString());
            if (!string.IsNullOrEmpty(dr["ApplicationId"].ToString()))
                element.ApplicationId = Convert.ToInt64(dr["ApplicationId"].ToString());
            if (!string.IsNullOrEmpty(dr["AddOnValue"].ToString()))
                element.AddOnValue = (float)Convert.ToDecimal(dr["AddOnValue"].ToString());
            if (!string.IsNullOrEmpty(dr["ProgrammeAddOnCriteriaId"].ToString()))
                element.ProgrammeAddOnCriteriaId = Convert.ToInt16(dr["ProgrammeAddOnCriteriaId"].ToString());
            element.ProgrammeAddOnCriteria = dr["TitleName"].ToString();
            element.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
            return element;
        }

        public List<ProvisionalMeritListAddOnInformation> getProvisionalMeritListAddOnInformation(string flag, int? IsDeleted, int? MeritListInstanceId, int? ProvisionalMeritListId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("M_ProvisionalMeritListAddOnInformationGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Flag", flag);
                Cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
                Cmd.Parameters.AddWithValue("@MeritListInstanceId", MeritListInstanceId);
                Cmd.Parameters.AddWithValue("@ProvisionalMeritListId", ProvisionalMeritListId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<ProvisionalMeritListAddOnInformation> ObjListProvisionalMeritListAddOnInformation = new List<ProvisionalMeritListAddOnInformation>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ProvisionalMeritListAddOnInformation obj = new ProvisionalMeritListAddOnInformation();
                        if (flag == "admin")
                        {
                            setRowIntoObjForAdmin(obj, Dt.Rows[i]);
                        }
                        else if (flag == "user")
                        {
                            setRowIntoObjForUser(obj, Dt.Rows[i]);
                        }

                        ObjListProvisionalMeritListAddOnInformation.Add(obj);
                    }
                }
                return ObjListProvisionalMeritListAddOnInformation;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}