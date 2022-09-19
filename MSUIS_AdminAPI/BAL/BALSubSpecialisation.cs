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
    public class BALSubSpecialisation
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        #region SubSpecialisation Get
        public List<SubSpecialisation> SubSpecialisationGet()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstSubSpecialisationGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<SubSpecialisation> ObjListSubSpecial = new List<SubSpecialisation>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        SubSpecialisation objSubSpecial = new SubSpecialisation();

                        objSubSpecial.Id = Convert.ToInt32(Dt.Rows[i]["Id"].ToString());
                        objSubSpecial.SpecialisationId = (Convert.ToString(Dt.Rows[i]["SpecialisationId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["SpecialisationId"]);
                        objSubSpecial.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["BranchName"]);
                        objSubSpecial.SubSpecialisationName = (Convert.ToString(Dt.Rows[i]["SubSpecialisationName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["SubSpecialisationName"]); ;
                        objSubSpecial.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objSubSpecial.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);
                        ObjListSubSpecial.Add(objSubSpecial);
                    }
                }
                return ObjListSubSpecial;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region SubSpecialisation Get By Id
        public SubSpecialisation SubSpecialisationGetById(int? SubSpecialId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MstSubSpecialisationGetById", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", SubSpecialId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    SubSpecialisation objSubSpecial = new SubSpecialisation();

                    objSubSpecial.Id = Convert.ToInt32(Dt.Rows[0]["Id"].ToString());
                    objSubSpecial.SpecialisationId = (Convert.ToString(Dt.Rows[0]["SpecialisationId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["SpecialisationId"]);
                    objSubSpecial.BranchName = (Convert.ToString(Dt.Rows[0]["BranchName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["BranchName"]);
                    objSubSpecial.SubSpecialisationName = (Convert.ToString(Dt.Rows[0]["SubSpecialisationName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["SubSpecialisationName"]); ;
                    objSubSpecial.IsActive = (Convert.ToString(Dt.Rows[0]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsActive"]);
                    objSubSpecial.IsDeleted = (Convert.ToString(Dt.Rows[0]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsDeleted"]);

                    return objSubSpecial;

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

        #region SubSpecialisation Get By SpecialisationId
        public List<SubSpecialisation> SubSpecialisationGetBySpecialisationId(int? SpecialId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MstSubSpecialisationGetbyMstSpecialisationId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SpecialisationId", SpecialId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<SubSpecialisation> ObjListSubSpecial = new List<SubSpecialisation>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        SubSpecialisation objSubSpecial = new SubSpecialisation();

                        objSubSpecial.Id = Convert.ToInt32(Dt.Rows[i]["Id"].ToString());
                        objSubSpecial.SpecialisationId = (Convert.ToString(Dt.Rows[i]["SpecialisationId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["SpecialisationId"]);
                        objSubSpecial.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["BranchName"]);
                        objSubSpecial.SubSpecialisationName = (Convert.ToString(Dt.Rows[i]["SubSpecialisationName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["SubSpecialisationName"]); ;
                        objSubSpecial.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objSubSpecial.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);
                        ObjListSubSpecial.Add(objSubSpecial);
                    }
                }
                return ObjListSubSpecial;

            }
            catch (Exception e)
            {
                return null;
            }

        }
        #endregion
    }
}