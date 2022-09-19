using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MSUISApi.BAL
{
    public class BALExamFeesConfiguration
    {

        public ExamFeesConfiguration setRowIntoObjForAdmin(ExamFeesConfiguration element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr[0].ToString());
            element.AppearanceTypeId = Convert.ToInt32(dr[1].ToString());
            element.AppearanceType =dr[5].ToString();
            element.ExamMasterId = Convert.ToInt32(dr[2].ToString());
            element.ProgrammePartTermId = Convert.ToInt16(dr[3].ToString());
            element.IsActive = Convert.ToBoolean(dr[4].ToString());

            return element;

        }

        public ExamFeesConfiguration setRowIntoObjForUser(ExamFeesConfiguration element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr[0].ToString());
            element.AppearanceTypeId = Convert.ToInt32(dr[1].ToString());
            element.AppearanceType =dr[4].ToString();
            element.ExamMasterId = Convert.ToInt32(dr[2].ToString());
            element.ProgrammePartTermId = Convert.ToInt16(dr[3].ToString());
            return element;
        }

        public ExamFeesConfiguration setRowIntoObjForAll(ExamFeesConfiguration element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr[0].ToString());
            element.AppearanceTypeId = Convert.ToInt32(dr[1].ToString());
            element.AppearanceType =dr[10].ToString();
            element.ProgrammePartTermId = Convert.ToInt16(dr[2].ToString());
            element.IsActive = Convert.ToBoolean(dr[3].ToString());
            element.CreatedOn = Convert.ToDateTime(dr[4].ToString());
            element.CreatedBy = Convert.ToInt32(dr[5].ToString());
            if (!string.IsNullOrEmpty(dr[6].ToString()))
                element.ModifiedOn = Convert.ToDateTime(dr[6].ToString());
            if (!string.IsNullOrEmpty(dr[7].ToString()))
                element.ModifiedBy = Convert.ToInt32(dr[7].ToString());
            element.IsDeleted = Convert.ToBoolean(dr[8].ToString());
            element.ExamMasterId = Convert.ToInt32(dr[9].ToString());
            return element;
        }

        public List<ExamFeesConfiguration> getExamFeesConfigurationList(string flag, int? IsDeleted, int? IsActive,int? ExamMasterId,int? AppearanceTypeMasterId,int? ProgrammePartTermId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("ExamFeesConfigurationListGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Flag", flag);
                Cmd.Parameters.AddWithValue("@IsActive", IsActive);
                Cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
                Cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                Cmd.Parameters.AddWithValue("@ProgrammePartTermId", ProgrammePartTermId);
                Cmd.Parameters.AddWithValue("@AppearanceTypeMasterId", AppearanceTypeMasterId);

                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<ExamFeesConfiguration> ObjListExamFeesConfiguration = new List<ExamFeesConfiguration>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ExamFeesConfiguration objExamFeesConfiguration = new ExamFeesConfiguration();
                        if (flag == "admin")
                        {
                            setRowIntoObjForAdmin(objExamFeesConfiguration, Dt.Rows[i]);
                        }
                        else if (flag == "user")
                        {
                            setRowIntoObjForUser(objExamFeesConfiguration, Dt.Rows[i]);
                        }

                        ObjListExamFeesConfiguration.Add(objExamFeesConfiguration);
                    }
                }
                return ObjListExamFeesConfiguration;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}