using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MSUISApi.BAL
{
    public class BALSeatMatrixAllowedCategoryMapping
    {

        public SeatMatrixAllowedCategoryMapping setRowIntoObjForAdmin(SeatMatrixAllowedCategoryMapping element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            element.SeatMatrixId = Convert.ToInt32(dr["SeatMatrixId"].ToString());
            if (!string.IsNullOrEmpty(dr["CategoryId"].ToString()))
                element.CategoryId = Convert.ToInt32(dr["CategoryId"].ToString());
            element.Category = dr["ApplicationReservationName"].ToString();
            element.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
            // element.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
            return element;

        }

        public SeatMatrixAllowedCategoryMapping setRowIntoObjForUser(SeatMatrixAllowedCategoryMapping element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            element.SeatMatrixId = Convert.ToInt32(dr["SeatMatrixId"].ToString());
            if (!string.IsNullOrEmpty(dr["CategoryId"].ToString()))
                element.CategoryId = Convert.ToInt32(dr["CategoryId"].ToString());
            element.Category = dr["ApplicationReservationName"].ToString();
            return element;
        }

        public SeatMatrixAllowedCategoryMapping setRowIntoObjForAll(SeatMatrixAllowedCategoryMapping element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            element.SeatMatrixId = Convert.ToInt32(dr["SeatMatrixId"].ToString());
            if (!string.IsNullOrEmpty(dr["CategoryId"].ToString()))
                element.CategoryId = Convert.ToInt32(dr["CategoryId"].ToString());
            element.Category = dr["ApplicationReservationName"].ToString();
            element.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
            element.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
            element.CreatedOn = Convert.ToDateTime(dr["CreatedOn"].ToString());
            element.CreatedBy = Convert.ToInt32(dr["CreatedBy"].ToString());
            if (!string.IsNullOrEmpty(dr["ModifiedOn"].ToString()))
                element.ModifiedOn = Convert.ToDateTime(dr["ModifiedOn"].ToString());
            if (!string.IsNullOrEmpty(dr["ModifiedBy"].ToString()))
                element.ModifiedBy = Convert.ToInt32(dr["ModifiedBy"].ToString());
            return element;
        }

        public List<SeatMatrixAllowedCategoryMapping> getSeatMatrixAllowedCategoryMappingList(string flag, int? IsDeleted, int? IsActive, int? SeatMatrixId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("M_SeatMatrixAllowedCategoryMappingGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Flag", flag);
                Cmd.Parameters.AddWithValue("@IsActive", IsActive);
                Cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
                Cmd.Parameters.AddWithValue("@SeatMatrixId", SeatMatrixId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<SeatMatrixAllowedCategoryMapping> ObjListSeatMatrixAllowedCategoryMapping = new List<SeatMatrixAllowedCategoryMapping>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        SeatMatrixAllowedCategoryMapping objSeatMatrixAllowedCategoryMapping = new SeatMatrixAllowedCategoryMapping();
                        if (flag == "admin")
                        {
                            setRowIntoObjForAdmin(objSeatMatrixAllowedCategoryMapping, Dt.Rows[i]);
                        }
                        else if (flag == "user")
                        {
                            setRowIntoObjForUser(objSeatMatrixAllowedCategoryMapping, Dt.Rows[i]);
                        }

                        ObjListSeatMatrixAllowedCategoryMapping.Add(objSeatMatrixAllowedCategoryMapping);
                    }
                }
                return ObjListSeatMatrixAllowedCategoryMapping;
            }
            catch (Exception e)
            {
                return null;
            }
        }


    }
}