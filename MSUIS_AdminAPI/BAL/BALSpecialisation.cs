using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.WebPages;

namespace MSUISApi.BAL
{
    public class BALSpecialisation
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        #region Specialisation Get
        public List<MstSpecialisation> SpecialisationGet()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstSpecialisationGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);
                Int32 count = 0;
                List<MstSpecialisation> ObjListSpecialisation = new List<MstSpecialisation>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstSpecialisation objSpecialisation = new MstSpecialisation();

                        objSpecialisation.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objSpecialisation.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["BranchName"]);
                        objSpecialisation.FacultyId = (Convert.ToString(Dt.Rows[i]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        objSpecialisation.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyName"]);                        
                        objSpecialisation.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objSpecialisation.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);
                        count = count + 1;
                        objSpecialisation.IndexId = count;
                        ObjListSpecialisation.Add(objSpecialisation);
                    }
                }
                return ObjListSpecialisation;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Specialisation Get For Drop Down
        public List<MstSpecialisation> SpecialisationGetForDropDown()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstSpecialisationGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<MstSpecialisation> ObjListSpecialisation = new List<MstSpecialisation>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstSpecialisation objSpecialisation = new MstSpecialisation();

                        objSpecialisation.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objSpecialisation.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["BranchName"]);                        
                        ObjListSpecialisation.Add(objSpecialisation);
                    }
                }
                return ObjListSpecialisation;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Specialisation Get By Id
        public MstSpecialisation SpecialisationGetById(int? SpecialId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MstSpecialisationGetbyId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", SpecialId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    MstSpecialisation objSpecialisation = new MstSpecialisation();

                    objSpecialisation.Id = Convert.ToInt32(Dt.Rows[0]["Id"]);
                    objSpecialisation.BranchName = (Convert.ToString(Dt.Rows[0]["BranchName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["BranchName"]);
                    objSpecialisation.FacultyId = (Convert.ToString(Dt.Rows[0]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["FacultyId"]);
                    objSpecialisation.FacultyName = (Convert.ToString(Dt.Rows[0]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["FacultyName"]);
                    objSpecialisation.IsActive = (Convert.ToString(Dt.Rows[0]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsActive"]);
                    objSpecialisation.IsDeleted = (Convert.ToString(Dt.Rows[0]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsDeleted"]);

                    return objSpecialisation;

                }
                else
                    return null;

            }
            catch (Exception e)
            {
                return null;
            }

        }
        #endregion

        #region Specialisation Get By Faculty Id

        public List<MstSpecialisation> SpecialisationGetByFacultyId(int? FacultyId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MstSpecialisationGetbyFacultyId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FacultyId", FacultyId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<MstSpecialisation> ObjListSpecialisation = new List<MstSpecialisation>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstSpecialisation objSpecialisation = new MstSpecialisation();

                        objSpecialisation.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objSpecialisation.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["BranchName"]);
                        objSpecialisation.FacultyId = (Convert.ToString(Dt.Rows[i]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        objSpecialisation.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objSpecialisation.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objSpecialisation.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);
                        ObjListSpecialisation.Add(objSpecialisation);
                    }
                }
                return ObjListSpecialisation;

            }
            catch (Exception e)
            {
                return null;
            }

        }
        #endregion

        #region Specialisation Get By Institute Id

        public List<MstSpecialisation> SpecialisationGetByInstituteId(int? InstituteId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MstSpecialisationGetbyInstituteId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@InstituteId", InstituteId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<MstSpecialisation> ObjListSpecialisation = new List<MstSpecialisation>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstSpecialisation objSpecialisation = new MstSpecialisation();

                        objSpecialisation.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objSpecialisation.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["BranchName"]);
                        objSpecialisation.BranchInstitute = (Convert.ToString(Dt.Rows[i]["BranchInstitute"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["BranchInstitute"]);
                        objSpecialisation.FacultyId = (Convert.ToString(Dt.Rows[i]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        objSpecialisation.InstituteId = (Convert.ToString(Dt.Rows[i]["InstituteId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["InstituteId"]);
                        //objSpecialisation.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objSpecialisation.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objSpecialisation.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);
                        ObjListSpecialisation.Add(objSpecialisation);
                    }
                }
                return ObjListSpecialisation;

            }
            catch (Exception e)
            {
                return null;
            }

        }
        #endregion
    }
}