using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MSUISApi.BAL
{
    public class BALMeritListInstance
    {
        public MeritListInstance setRowIntoObjForAdmin(MeritListInstance element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            element.Name = dr["Name"].ToString();
            if (!string.IsNullOrEmpty(dr["programmeInstancePartTermId"].ToString()))
            {
                element.ProgrammeInstancePartTermId = Convert.ToInt32(dr["programmeInstancePartTermId"].ToString());
                element.ProgrammeInstancePartTerm = dr["InstancePartTermName"].ToString();
            }
            if (!string.IsNullOrEmpty(dr["AcademicYearId"].ToString()))
            {
                element.AcademicYearId = Convert.ToInt16(dr["AcademicYearId"].ToString());
                element.AcademicYearCode = dr["AcademicYearCode"].ToString();
            }
            element.Round = Convert.ToInt32(dr["Round"].ToString());
            if (!string.IsNullOrEmpty(dr["RefMeritListId"].ToString()))
                element.RefMeritListId = Convert.ToInt32(dr["RefMeritListId"].ToString());
            element.RefMeritList = dr["refMeritList"].ToString();
            element.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
            if (!string.IsNullOrEmpty(dr["TotalSeats"].ToString()))
                element.TotalSeats = Convert.ToInt16(dr["TotalSeats"].ToString());
            if (!string.IsNullOrEmpty(dr["ConsiderPreference"].ToString()))
                element.ConsiderPreference = Convert.ToBoolean(dr["ConsiderPreference"].ToString());
            if (!string.IsNullOrEmpty(dr["FacultyId"].ToString()))
            {
                element.FacultyId = Convert.ToInt16(dr["FacultyId"].ToString());
                element.FacultyName = dr["FacultyName"].ToString();
            }
            if (!string.IsNullOrEmpty(dr["ConfigurationLocked"].ToString()))
                element.ConfigurationLocked = Convert.ToBoolean(dr["ConfigurationLocked"].ToString());
            if (!string.IsNullOrEmpty(dr["ConfigurationLockedBy"].ToString()))
                element.ConfigurationLockedById = Convert.ToInt32(dr["ConfigurationLockedBy"].ToString());
            element.ConfigurationLockedByTimeStamp = dr["ConfigurationLockedOn"].ToString();
            if (!string.IsNullOrEmpty(dr["SeatMatrixLocked"].ToString()))
                element.SeatMatrixLocked = Convert.ToBoolean(dr["SeatMatrixLocked"].ToString());
            if (!string.IsNullOrEmpty(dr["SeatMatrixLockedBy"].ToString()))
                element.SeatMatrixById = Convert.ToInt32(dr["SeatMatrixLockedBy"].ToString());

            element.SeatMatrixByTimeStamp = dr["SeatMatrixLockedOn"].ToString();
            // element.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
            return element;

        }

        public MeritListInstance setRowIntoObjForUser(MeritListInstance element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            element.Name = dr["Name"].ToString();
            if (!string.IsNullOrEmpty(dr["programmeInstancePartTermId"].ToString()))
            {
                element.ProgrammeInstancePartTermId = Convert.ToInt32(dr["programmeInstancePartTermId"].ToString());
                element.ProgrammeInstancePartTerm = dr["InstancePartTermName"].ToString();
            }
            if (!string.IsNullOrEmpty(dr["AcademicYearId"].ToString()))
            {
                element.AcademicYearId = Convert.ToInt16(dr["AcademicYearId"].ToString());
                element.AcademicYearCode = dr["AcademicYearCode"].ToString();
            }
            element.Round = Convert.ToInt32(dr["Round"].ToString());
            if (!string.IsNullOrEmpty(dr["RefMeritListId"].ToString()))
                element.RefMeritListId = Convert.ToInt32(dr["RefMeritListId"].ToString());
            element.RefMeritList = dr["refMeritList"].ToString();
            if (!string.IsNullOrEmpty(dr["TotalSeats"].ToString()))
                element.TotalSeats = Convert.ToInt16(dr["TotalSeats"].ToString());
            if (!string.IsNullOrEmpty(dr["ConsiderPreference"].ToString()))
                element.ConsiderPreference = Convert.ToBoolean(dr["ConsiderPreference"].ToString());
            if (!string.IsNullOrEmpty(dr["FacultyId"].ToString()))
            {
                element.FacultyId = Convert.ToInt16(dr["FacultyId"].ToString());
                element.FacultyName = dr["FacultyName"].ToString();
            }
            if (!string.IsNullOrEmpty(dr["ConfigurationLocked"].ToString()))
                element.ConfigurationLocked = Convert.ToBoolean(dr["ConfigurationLocked"].ToString());
            if (!string.IsNullOrEmpty(dr["ConfigurationLockedBy"].ToString()))
                element.ConfigurationLockedById = Convert.ToInt32(dr["ConfigurationLockedBy"].ToString());
            element.ConfigurationLockedByTimeStamp = dr["ConfigurationLockedOn"].ToString();
            if (!string.IsNullOrEmpty(dr["SeatMatrixLocked"].ToString()))
                element.SeatMatrixLocked = Convert.ToBoolean(dr["SeatMatrixLocked"].ToString());
            if (!string.IsNullOrEmpty(dr["SeatMatrixLockedBy"].ToString()))
                element.SeatMatrixById = Convert.ToInt32(dr["SeatMatrixLockedBy"].ToString());

            element.SeatMatrixByTimeStamp = dr["SeatMatrixLockedOn"].ToString();
            return element;
        }

        public MeritListInstance setRowIntoObjForAll(MeritListInstance element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            element.Name = dr["Name"].ToString();
            if (!string.IsNullOrEmpty(dr["programmeInstancePartTermId"].ToString()))
            {
                element.ProgrammeInstancePartTermId = Convert.ToInt32(dr["programmeInstancePartTermId"].ToString());
                element.ProgrammeInstancePartTerm = dr["InstancePartTermName"].ToString();
            }
            if (!string.IsNullOrEmpty(dr["AcademicYearId"].ToString()))
            {
                element.AcademicYearId = Convert.ToInt16(dr["AcademicYearId"].ToString());
                element.AcademicYearCode = dr["AcademicYearCode"].ToString();
            }
            element.Round = Convert.ToInt32(dr["Round"].ToString());
            if (!string.IsNullOrEmpty(dr["RefMeritListId"].ToString()))
                element.RefMeritListId = Convert.ToInt32(dr["RefMeritListId"].ToString());
            element.RefMeritList = dr["refMeritList"].ToString();
            if (!string.IsNullOrEmpty(dr["ConfigurationLocked"].ToString()))
                element.ConfigurationLocked = Convert.ToBoolean(dr["ConfigurationLocked"].ToString());
            if (!string.IsNullOrEmpty(dr["ConfigurationLockedBy"].ToString()))
                element.ConfigurationLockedById = Convert.ToInt32(dr["ConfigurationLockedBy"].ToString());
            element.ConfigurationLockedByTimeStamp = dr["ConfigurationLockedOn"].ToString();
            if (!string.IsNullOrEmpty(dr["SeatMatrixLocked"].ToString()))
                element.SeatMatrixLocked = Convert.ToBoolean(dr["SeatMatrixLocked"].ToString());
            if (!string.IsNullOrEmpty(dr["SeatMatrixLockedBy"].ToString()))
                element.SeatMatrixById = Convert.ToInt32(dr["SeatMatrixLockedBy"].ToString());

            element.SeatMatrixByTimeStamp = dr["SeatMatrixLockedOn"].ToString();
            element.CreatedOn = Convert.ToDateTime(dr["CreatedOn"].ToString());
            element.CreatedBy = Convert.ToInt32(dr["CreatedBy"].ToString());
            if (!string.IsNullOrEmpty(dr["ModifiedOn"].ToString()))
                element.ModifiedOn = Convert.ToDateTime(dr["ModifiedOn"].ToString());
            if (!string.IsNullOrEmpty(dr["ModifiedBy"].ToString()))
                element.ModifiedBy = Convert.ToInt32(dr["ModifiedBy"].ToString());
            element.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
            element.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
            if (!string.IsNullOrEmpty(dr["TotalSeats"].ToString()))
                element.TotalSeats = Convert.ToInt16(dr["TotalSeats"].ToString());
            if (!string.IsNullOrEmpty(dr["ConsiderPreference"].ToString()))
                element.ConsiderPreference = Convert.ToBoolean(dr["ConsiderPreference"].ToString());
            if (!string.IsNullOrEmpty(dr["FacultyId"].ToString()))
            {
                element.FacultyId = Convert.ToInt16(dr["FacultyId"].ToString());
                element.FacultyName = dr["FacultyName"].ToString();
            }
            return element;
        }

        public List<MeritListInstance> getMeritListInstanceList(string flag, int? IsDeleted, int? IsActive, int? ProgrammeInstancePartTermId, int? AcademicYearId,int? Round,Int64 UserId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);

                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("M_MeritListInstanceListGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Flag", flag);
                Cmd.Parameters.AddWithValue("@IsActive", IsActive);
                Cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
                Cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                Cmd.Parameters.AddWithValue("@Round", Round);
                Cmd.Parameters.AddWithValue("@programmeInstancePartTermId", ProgrammeInstancePartTermId);
                Cmd.Parameters.AddWithValue("@UserId", UserId);
                SqlDataAdapter Da = new SqlDataAdapter();
                Da.SelectCommand = Cmd;
                Da.Fill(Dt);

                List<MeritListInstance> ObjListMeritListInstance = new List<MeritListInstance>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MeritListInstance objMeritListInstnace = new MeritListInstance();
                        if (flag == "admin")
                        {
                            setRowIntoObjForAdmin(objMeritListInstnace, Dt.Rows[i]);
                        }
                        else if (flag == "user")
                        {
                            setRowIntoObjForUser(objMeritListInstnace, Dt.Rows[i]);
                        }

                        ObjListMeritListInstance.Add(objMeritListInstnace);
                    }
                }
                return ObjListMeritListInstance;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}