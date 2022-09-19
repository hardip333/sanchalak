using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MSUISApi.BAL
{
    public class BALSeatMatrix
    {
        public SeatMatrix setRowIntoObjForAdmin(SeatMatrix element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            element.MeritListInstanceId = Convert.ToInt32(dr["MeritListInstanceId"].ToString());
            element.MeritListInstance = dr["Name"].ToString();
            element.CategoryId = Convert.ToInt16(dr["CategoryId"].ToString());
            element.Category = dr["ApplicationReservationName"].ToString();
            element.Location = Convert.ToInt16(dr["Location"].ToString());
            element.AvailableSeats = Convert.ToInt16(dr["AvailableSeats"].ToString());
            element.TotalSeats = Convert.ToInt16(dr["TotalSeats"].ToString());
            if (!string.IsNullOrEmpty(dr["PreferenceId"].ToString()))
                element.PreferenceId = Convert.ToInt16(dr["PreferenceId"].ToString());
            element.Preference = dr["GroupName"].ToString();
            element.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
            //if (!string.IsNullOrEmpty(dr["EWSReservationPercentage"].ToString()))
            //    element.EWSReservationPercentage = (float)Convert.ToDecimal(dr["EWSReservationPercentage"].ToString());
            //if (!string.IsNullOrEmpty(dr["PhysicallyHandicapReservationPercentage"].ToString()))
            //    element.PhysicallyHandicapReservationPercentage = (float)Convert.ToDecimal(dr["PhysicallyHandicapReservationPercentage"].ToString());
            //element.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
            element.StrGender = dr["StrGender"].ToString();


            BALSeatMatrixSpecialCategoryMapping func = new BALSeatMatrixSpecialCategoryMapping();
            element.SeatMatrixCatMapList = func.getSeatMatrixSpecialCategoryMappingList("admin", 0, 1, element.Id);

            BALSeatMatrixAllowedCategoryMapping func1 = new BALSeatMatrixAllowedCategoryMapping();
            element.SeatMatrixAllowedCatList = func1.getSeatMatrixAllowedCategoryMappingList("admin", 0, 1, element.Id);

            return element;

        }

        public SeatMatrix setRowIntoObjForUser(SeatMatrix element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            element.MeritListInstanceId = Convert.ToInt32(dr["MeritListId"].ToString());
            element.MeritListInstance = dr["Name"].ToString();
            element.CategoryId = Convert.ToInt16(dr["CategoryId"].ToString());
            element.Category = dr["ApplicationReservationName"].ToString();
            element.Location = Convert.ToInt16(dr["Location"].ToString());
            element.AvailableSeats = Convert.ToInt16(dr["AvailableSeats"].ToString());
            element.TotalSeats = Convert.ToInt16(dr["TotalSeats"].ToString());
            if (string.IsNullOrEmpty(dr["PreferenceId"].ToString()))
                element.PreferenceId = Convert.ToInt16(dr["PreferenceId"].ToString());
            element.Preference = dr["GroupName"].ToString();
            //if (!string.IsNullOrEmpty(dr["EWSReservationPercentage"].ToString()))
            //    element.EWSReservationPercentage = (float)Convert.ToDecimal(dr["EWSReservationPercentage"].ToString());
            //if (!string.IsNullOrEmpty(dr["PhysicallyHandicapReservationPercentage"].ToString()))
            //    element.PhysicallyHandicapReservationPercentage = (float)Convert.ToDecimal(dr["PhysicallyHandicapReservationPercentage"].ToString());
            element.StrGender = dr["StrGender"].ToString();
            BALSeatMatrixSpecialCategoryMapping func = new BALSeatMatrixSpecialCategoryMapping();
            element.SeatMatrixCatMapList = func.getSeatMatrixSpecialCategoryMappingList("user", 0, 1, element.Id);
            element.StrGender = dr["StrGender"].ToString();
            return element;
        }

        public SeatMatrix setRowIntoObjForAll(SeatMatrix element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            element.MeritListInstanceId = Convert.ToInt32(dr["MeritListId"].ToString());
            element.MeritListInstance = dr["Name"].ToString();
            element.CategoryId = Convert.ToInt16(dr["CategoryId"].ToString());
            element.Category = dr["ApplicationReservationName"].ToString();
            element.Location = Convert.ToInt16(dr["Location"].ToString());
            element.AvailableSeats = Convert.ToInt16(dr["AvailableSeats"].ToString());
            element.TotalSeats = Convert.ToInt16(dr["TotalSeats"].ToString());
            if (!string.IsNullOrEmpty(dr["PreferenceId"].ToString()))
                element.PreferenceId = Convert.ToInt16(dr["PreferenceId"].ToString());
            element.Preference = dr["GroupName"].ToString();
            //if (!string.IsNullOrEmpty(dr["EWSReservationPercentage"].ToString()))
            //    element.EWSReservationPercentage = (float)Convert.ToDecimal(dr["EWSReservationPercentage"].ToString());
            //if (!string.IsNullOrEmpty(dr["PhysicallyHandicapReservationPercentage"].ToString()))
            //    element.PhysicallyHandicapReservationPercentage = (float)Convert.ToDecimal(dr["PhysicallyHandicapReservationPercentage"].ToString());
            element.StrGender = dr["StrGender"].ToString();
            element.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
            element.CreatedOn = Convert.ToDateTime(dr["CreatedOn"].ToString());
            element.CreatedBy = Convert.ToInt32(dr["CreatedBy"].ToString());
            if (!string.IsNullOrEmpty(dr["ModifiedOn"].ToString()))
                element.ModifiedOn = Convert.ToDateTime(dr["ModifiedOn"].ToString());
            if (!string.IsNullOrEmpty(dr["ModifiedBy"].ToString()))
                element.ModifiedBy = Convert.ToInt32(dr["ModifiedBy"].ToString());
            element.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
            return element;
        }

        public List<SeatMatrix> getSeatMatrixList(string flag, int? IsDeleted, int? IsActive, int? MeritListId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("M_SeatMatrixGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Flag", flag);
                Cmd.Parameters.AddWithValue("@IsActive", IsActive);
                Cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
                Cmd.Parameters.AddWithValue("@MeritListInstanceId", MeritListId);

                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<SeatMatrix> ObjListSeatMatrix = new List<SeatMatrix>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        SeatMatrix objSeatMatrix = new SeatMatrix();
                        if (flag == "admin")
                        {
                            setRowIntoObjForAdmin(objSeatMatrix, Dt.Rows[i]);
                        }
                        else if (flag == "user")
                        {
                            setRowIntoObjForUser(objSeatMatrix, Dt.Rows[i]);
                        }

                        ObjListSeatMatrix.Add(objSeatMatrix);
                    }
                }
                return ObjListSeatMatrix;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string saveSeatMatrixInBulk(SeatMatrix ObjSeatMatrix)
        {
            SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

            SqlCommand cmd = new SqlCommand("M_SeatMatrixAddInBulkNew", Conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MeritListInstanceId", ObjSeatMatrix.MeritListInstanceId);
            cmd.Parameters.AddWithValue("@CategoryId", ObjSeatMatrix.CategoryId);
            cmd.Parameters.AddWithValue("@Location", ObjSeatMatrix.Location);
            //cmd.Parameters.AddWithValue("@AvailableSeats", ObjSeatMatrix.TotalSeats);
            //cmd.Parameters.AddWithValue("@TotalSeats", ObjSeatMatrix.TotalSeats);
            //cmd.Parameters.AddWithValue("@PrefernceId", ObjSeatMatrix.PreferenceId);
            //cmd.Parameters.AddWithValue("@EWSReservationPercentage", ObjSeatMatrix.EWSReservationPercentage);
            //cmd.Parameters.AddWithValue("@PhysicallyHandicapReservationPercentage", ObjSeatMatrix.PhysicallyHandicapReservationPercentage);
            cmd.Parameters.AddWithValue("@StrGender", ObjSeatMatrix.StrGender);
            cmd.Parameters.AddWithValue("@UserId", ObjSeatMatrix.CreatedBy);
            cmd.Parameters.AddWithValue("@UserTime", datetime);

            List<SeatMatrixPrefernceList> PreferenceList = ObjSeatMatrix.SeatMatrixPrefernceList.OfType<SeatMatrixPrefernceList>().ToList();

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("PreferenceId", typeof(int));
            dt2.Columns.Add("TotalSeats", typeof(int));
            foreach (var item in PreferenceList)
            {
                if (item.IsChecked == true)
                {
                    dt2.Rows.Add(item.Id, item.TotalSeats);
                }
                
            }

            cmd.Parameters.AddWithValue("@udtSeatMatrixPrefernceLst", dt2);

            List<SeatMatrixSpecialCategoryMapping> lst = ObjSeatMatrix.SeatMatrixCatMapList.OfType<SeatMatrixSpecialCategoryMapping>().ToList();

            DataTable dt = new DataTable();
            //dt.Columns.Add("id", typeof(int));
            //dt.Columns.Add("SeatMatrixId", typeof(int));
            dt.Columns.Add("SpecialCategoryFieldName", typeof(string));
            dt.Columns.Add("SocialCategoryId", typeof(int));
            dt.Columns.Add("ReservationPercentage", typeof(float));

            foreach (var item in lst)
            {
                //dt.Rows.Add()
                dt.Rows.Add(item.SpecialCategoryFieldName, item.SocialCategoryId, item.ReservationPercentage);
            }

            cmd.Parameters.AddWithValue("@udtSeatMatrixCatMapLst", dt);

            List<SeatMatrixAllowedCategoryMapping> lst1 = ObjSeatMatrix.SeatMatrixAllowedCatList.OfType<SeatMatrixAllowedCategoryMapping>().ToList();

            DataTable dt1 = new DataTable();

            dt1.Columns.Add("CategoryId", typeof(int));


            foreach (var item in lst1)
            {
                dt1.Rows.Add(item.CategoryId);
            }

            cmd.Parameters.AddWithValue("@udtSeatMatrixAllowedCatLst", dt1);


            cmd.Parameters.Add("@message", SqlDbType.NVarChar, 500);
            cmd.Parameters["@message"].Direction = ParameterDirection.Output;

            Conn.Open();
            cmd.ExecuteNonQuery();
            string message = Convert.ToString(cmd.Parameters["@message"].Value);
            Conn.Close();

            return message;
        }
    }
}