using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MSUISApi.BAL
{
    public class BALExamVenue
    {
        public ExamVenue setRowIntoObjForAdmin(ExamVenue element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr[0].ToString());
            if (!string.IsNullOrEmpty(dr[1].ToString()))
            {
                element.ExamCenterId = Convert.ToInt32(dr[1].ToString());
                
            }
            element.ExamCenter = dr[7].ToString();
            element.Code = dr[2].ToString();
            element.DisplayName = dr[3].ToString();
            element.Address = dr[4].ToString();
            element.IsActive = Convert.ToBoolean(dr[5].ToString());
            element.IsDeleted = Convert.ToBoolean(dr[6].ToString());
            return element;

        }

        public ExamVenue setRowIntoObjForUser(ExamVenue element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr[0].ToString());
            if (!string.IsNullOrEmpty(dr[1].ToString()))
            {
                element.ExamCenterId = Convert.ToInt32(dr[1].ToString());
            }
            element.ExamCenter = dr[5].ToString();
            element.Code = dr[2].ToString();
            element.DisplayName = dr[3].ToString();
            element.Address = dr[4].ToString();
            return element;
        }

        public ExamVenue setRowIntoObjForAll(ExamVenue element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr[0].ToString());
            if (!string.IsNullOrEmpty(dr[1].ToString()))
            {
                element.ExamCenterId = Convert.ToInt32(dr[1].ToString());
               
            }
            element.ExamCenter =dr[11].ToString();
            element.Code = dr[2].ToString();
            element.DisplayName = dr[3].ToString();
            element.Address = dr[4].ToString();
            element.IsActive = Convert.ToBoolean(dr[5].ToString());
            element.CreatedOn = Convert.ToDateTime(dr[6].ToString());
            element.CreatedBy = Convert.ToInt32(dr[7].ToString());
            if (!string.IsNullOrEmpty(dr[8].ToString()))
                element.ModifiedOn = Convert.ToDateTime(dr[8].ToString());
            if (!string.IsNullOrEmpty(dr[9].ToString()))
                element.ModifiedBy = Convert.ToInt32(dr[9].ToString());
            element.IsDeleted = Convert.ToBoolean(dr[10].ToString());
            return element;
        }


        public List<ExamVenue> getExamVenueList(string flag, int? IsDeleted, int? IsActive, int? ExamCenterId, int? InstituteId, Int64 UserId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("ExamVenueListGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Flag", flag);
                Cmd.Parameters.AddWithValue("@IsActive", IsActive);
                Cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
                Cmd.Parameters.AddWithValue("@ExamCenterId", ExamCenterId);
                Cmd.Parameters.AddWithValue("@InstituteId", InstituteId);
                Cmd.Parameters.AddWithValue("@UserId", UserId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<ExamVenue> ObjListExamVenue = new List<ExamVenue>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ExamVenue objExamVenue = new ExamVenue();
                        if (flag == "admin")
                        {
                            setRowIntoObjForAdmin(objExamVenue, Dt.Rows[i]);
                        }
                        else if (flag == "user")
                        {
                            setRowIntoObjForUser(objExamVenue, Dt.Rows[i]);
                        }
                        else if (flag == "ByInstitute")
                        {
                            setRowIntoObjForUser(objExamVenue, Dt.Rows[i]);
                        }

                        ObjListExamVenue.Add(objExamVenue);
                    }
                }
                return ObjListExamVenue;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}