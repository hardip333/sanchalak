using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MSUISApi.BAL
{
    public class BALExamCenter
    {
        public ExamCenter setRowIntoObjForAdmin(ExamCenter element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr[0].ToString());
            element.Code = dr[1].ToString();
            element.DisplayName = dr[2].ToString();
            element.Capacity = Convert.ToInt32(dr[3].ToString());
            element.IsActive = Convert.ToBoolean(dr[4].ToString());
            element.IsDeleted = Convert.ToBoolean(dr[5].ToString());
            return element;

        }

        public ExamCenter setRowIntoObjForUser(ExamCenter element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr[0].ToString());
            element.Code = dr[1].ToString();
            element.DisplayName = dr[2].ToString();
            element.Capacity = Convert.ToInt32(dr[3].ToString());
            return element;
        }

        public ExamCenter setRowIntoObjForAll(ExamCenter element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr[0].ToString());
            element.Code = dr[1].ToString();
            element.DisplayName = dr[2].ToString();
            element.Capacity = Convert.ToInt32(dr[3].ToString());
            element.IsActive = Convert.ToBoolean(dr[4].ToString());
            element.CreatedOn = Convert.ToDateTime(dr[5].ToString());
            element.CreatedBy = Convert.ToInt32(dr[6].ToString());
            if (!string.IsNullOrEmpty(dr[7].ToString()))
                element.ModifiedOn = Convert.ToDateTime(dr[7].ToString());
            if (!string.IsNullOrEmpty(dr[8].ToString()))
                element.ModifiedBy = Convert.ToInt32(dr[8].ToString());
            element.IsDeleted = Convert.ToBoolean(dr[9].ToString());
            return element;
        }

        public List<ExamCenter> getExamCenterList(string flag, int? IsDeleted, int? IsActive)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("ExamCenterListGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Flag", flag);
                Cmd.Parameters.AddWithValue("@IsActive", IsActive);
                Cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<ExamCenter> ObjListExamCenter = new List<ExamCenter>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ExamCenter objExamCenter = new ExamCenter();
                        if (flag == "admin")
                        {
                            setRowIntoObjForAdmin(objExamCenter, Dt.Rows[i]);
                        }
                        else if (flag == "user")
                        {
                            setRowIntoObjForUser(objExamCenter, Dt.Rows[i]);
                        }

                        ObjListExamCenter.Add(objExamCenter);
                    }
                }
                return ObjListExamCenter;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}