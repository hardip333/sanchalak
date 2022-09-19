using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MSUISApi.BAL
{
    public class BALProvisionalMeritListEducationDetails
    {
        public ProvisionalMeritListEducationDetails setRowIntoObjForAdmin(ProvisionalMeritListEducationDetails element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            if (!string.IsNullOrEmpty(dr["MeritListInstanceId"].ToString()))
                element.MeritListInstanceId = Convert.ToInt32(dr["MeritListInstanceId"].ToString());
            element.MeritListInstance = dr["Name"].ToString();
            if (!string.IsNullOrEmpty(dr["ProvisionalMeritListId"].ToString()))
                element.ProvisionalMeritListId = Convert.ToInt16(dr["ProvisionalMeritListId"].ToString());
            if (!string.IsNullOrEmpty(dr["AdmApplicantRegistrationId"].ToString()))
                element.AdmApplicantRegistrationId = Convert.ToInt64(dr["AdmApplicantRegistrationId"].ToString());
            if (!string.IsNullOrEmpty(dr["Percentage"].ToString()))
                element.Percentage = (float)Convert.ToDecimal(dr["Percentage"].ToString());
            if (!string.IsNullOrEmpty(dr["NoOfTrial"].ToString()))
                element.NoOfTrial = Convert.ToInt16(dr["NoOfTrial"].ToString());
            return element;

        }

        public ProvisionalMeritListEducationDetails setRowIntoObjForUser(ProvisionalMeritListEducationDetails element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            if (!string.IsNullOrEmpty(dr["MeritListInstanceId"].ToString()))
                element.MeritListInstanceId = Convert.ToInt32(dr["MeritListInstanceId"].ToString());
            element.MeritListInstance = dr["Name"].ToString();
            if (!string.IsNullOrEmpty(dr["ProvisionalMeritListId"].ToString()))
                element.ProvisionalMeritListId = Convert.ToInt16(dr["ProvisionalMeritListId"].ToString());
            if (!string.IsNullOrEmpty(dr["AdmApplicantRegistrationId"].ToString()))
                element.AdmApplicantRegistrationId = Convert.ToInt64(dr["AdmApplicantRegistrationId"].ToString());
            if (!string.IsNullOrEmpty(dr["Percentage"].ToString()))
                element.Percentage = (float)Convert.ToDecimal(dr["Percentage"].ToString());
            if (!string.IsNullOrEmpty(dr["NoOfTrial"].ToString()))
                element.NoOfTrial = Convert.ToInt16(dr["NoOfTrial"].ToString());
            return element;
        }

        public ProvisionalMeritListEducationDetails setRowIntoObjForAll(ProvisionalMeritListEducationDetails element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            if (!string.IsNullOrEmpty(dr["MeritListInstanceId"].ToString()))
                element.MeritListInstanceId = Convert.ToInt32(dr["MeritListInstanceId"].ToString());
            element.MeritListInstance = dr["Name"].ToString();
            if (!string.IsNullOrEmpty(dr["ProvisionalMeritListId"].ToString()))
                element.ProvisionalMeritListId = Convert.ToInt16(dr["ProvisionalMeritListId"].ToString());
            if (!string.IsNullOrEmpty(dr["AdmApplicantRegistrationId"].ToString()))
                element.AdmApplicantRegistrationId = Convert.ToInt64(dr["AdmApplicantRegistrationId"].ToString());
            if (!string.IsNullOrEmpty(dr["Percentage"].ToString()))
                element.Percentage = (float)Convert.ToDecimal(dr["Percentage"].ToString());
            if (!string.IsNullOrEmpty(dr["NoOfTrial"].ToString()))
                element.NoOfTrial = Convert.ToInt16(dr["NoOfTrial"].ToString());
            element.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
            return element;
        }

        public List<ProvisionalMeritListEducationDetails> getProvisionalMeritListEducationDetails(string flag, int? IsDeleted, int? MeritListInstanceId, int? ProvisionalMeritListId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("M_ProvisionalMeritListEducationDetailsGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Flag", flag);
                Cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
                Cmd.Parameters.AddWithValue("@MeritListInstanceId", MeritListInstanceId);
                Cmd.Parameters.AddWithValue("@ProvisionalMeritListId", ProvisionalMeritListId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<ProvisionalMeritListEducationDetails> ObjListProvisionalMeritListEducationDetails = new List<ProvisionalMeritListEducationDetails>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ProvisionalMeritListEducationDetails obj = new ProvisionalMeritListEducationDetails();
                        if (flag == "admin")
                        {
                            setRowIntoObjForAdmin(obj, Dt.Rows[i]);
                        }
                        else if (flag == "user")
                        {
                            setRowIntoObjForUser(obj, Dt.Rows[i]);
                        }

                        ObjListProvisionalMeritListEducationDetails.Add(obj);
                    }
                }
                return ObjListProvisionalMeritListEducationDetails;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}