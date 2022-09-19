using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MSUISApi.BAL
{
    public class BALSeatMatrixSpecialCategoryMapping
    {
        public SeatMatrixSpecialCategoryMapping setRowIntoObjForAdmin(SeatMatrixSpecialCategoryMapping element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            element.SeatMatrixId = Convert.ToInt32(dr["SeatMatrixId"].ToString());
            element.SpecialCategoryFieldName = dr["SpecialCategoryFieldName"].ToString();
            if (!string.IsNullOrEmpty(dr["SocialCategoryId"].ToString()))
                element.SocialCategoryId = Convert.ToInt16(dr["SocialCategoryId"].ToString()) ;
            element.SocialCategory = dr["SocialCategoryName"].ToString();
                if (!string.IsNullOrEmpty(dr["ReservationPercentage"].ToString()))
                element.ReservationPercentage = (float)Convert.ToDecimal(dr["ReservationPercentage"].ToString());
            element.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
           // element.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
            return element;

        }

        public SeatMatrixSpecialCategoryMapping setRowIntoObjForUser(SeatMatrixSpecialCategoryMapping element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            element.SeatMatrixId = Convert.ToInt32(dr["SeatMatrixId"].ToString());
            element.SpecialCategoryFieldName = dr["SpecialCategoryFieldName"].ToString();
            if (!string.IsNullOrEmpty(dr["SocialCategoryId"].ToString()))
                element.SocialCategoryId = Convert.ToInt16(dr["SocialCategoryId"].ToString());
            element.SocialCategory = dr["SocialCategoryName"].ToString();
            if (!string.IsNullOrEmpty(dr["ReservationPercentage"].ToString()))
                element.ReservationPercentage = (float)Convert.ToDecimal(dr["ReservationPercentage"].ToString());
            return element;
        }

        public SeatMatrixSpecialCategoryMapping setRowIntoObjForAll(SeatMatrixSpecialCategoryMapping element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            element.SeatMatrixId = Convert.ToInt32(dr["SeatMatrixId"].ToString());
            element.SpecialCategoryFieldName = dr["SpecialCategoryFieldName"].ToString();
            if (!string.IsNullOrEmpty(dr["ReservationPercentage"].ToString()))
                element.ReservationPercentage = (float)Convert.ToDecimal(dr["ReservationPercentage"].ToString());
            if (!string.IsNullOrEmpty(dr["SocialCategoryId"].ToString()))
                element.SocialCategoryId = Convert.ToInt16(dr["SocialCategoryId"].ToString());
            element.SocialCategory = dr["SocialCategoryName"].ToString();
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

        public List<SeatMatrixSpecialCategoryMapping> getSeatMatrixSpecialCategoryMappingList(string flag, int? IsDeleted, int? IsActive,int? SeatMatrixId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("M_SeatMatrixSpecialCategoryMappingGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Flag", flag);
                Cmd.Parameters.AddWithValue("@IsActive", IsActive);
                Cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
                Cmd.Parameters.AddWithValue("@SeatMatrixId", SeatMatrixId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<SeatMatrixSpecialCategoryMapping> ObjListSeatMatrixSpecialCategoryMapping = new List<SeatMatrixSpecialCategoryMapping>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        SeatMatrixSpecialCategoryMapping objSeatMatrixSpecialCategoryMapping = new SeatMatrixSpecialCategoryMapping();
                        if (flag == "admin")
                        {
                            setRowIntoObjForAdmin(objSeatMatrixSpecialCategoryMapping, Dt.Rows[i]);
                        }
                        else if (flag == "user")
                        {
                            setRowIntoObjForUser(objSeatMatrixSpecialCategoryMapping, Dt.Rows[i]);
                        }

                        ObjListSeatMatrixSpecialCategoryMapping.Add(objSeatMatrixSpecialCategoryMapping);
                    }
                }
                return ObjListSeatMatrixSpecialCategoryMapping;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}