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
    public class BALFacultyInstituteList
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        #region Faculty Institute Map List Get
        public List<FacultyInstituteMap> FacultyInstituteListGet()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("FacultyInstituteMapGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);
                Int32 count = 0;
                List<FacultyInstituteMap> ObjLstFacIns = new List<FacultyInstituteMap>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        FacultyInstituteMap objFacIns = new FacultyInstituteMap();

                        objFacIns.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objFacIns.FacultyId = (Convert.ToString(Dt.Rows[i]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        objFacIns.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objFacIns.InstituteId = (Convert.ToString(Dt.Rows[i]["InstituteId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["InstituteId"]);
                        objFacIns.InstituteName = (Convert.ToString(Dt.Rows[i]["InstituteName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstituteName"]);
                        objFacIns.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objFacIns.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);
                        count = count + 1;
                        objFacIns.IndexId = count;
                        ObjLstFacIns.Add(objFacIns);
                    }
                }
                return ObjLstFacIns;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Faculty Institute Map List Get By Id
        public FacultyInstituteMap FacultyInstituteListGetById(int? Id)
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("FacultyInstituteMapGetById", Con);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@Id", Id);

                Da.SelectCommand = Cmd;
                Da.Fill(Dt);

                if (Dt.Rows.Count > 0)
                {
                    FacultyInstituteMap objFacIns = new FacultyInstituteMap();

                    objFacIns.Id = Convert.ToInt32(Dt.Rows[0]["Id"]);
                    objFacIns.FacultyId = (Convert.ToString(Dt.Rows[0]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["FacultyId"]);
                    objFacIns.FacultyName = (Convert.ToString(Dt.Rows[0]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["FacultyName"]);
                    objFacIns.InstituteId = (Convert.ToString(Dt.Rows[0]["InstituteId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["InstituteId"]);
                    objFacIns.InstituteName = (Convert.ToString(Dt.Rows[0]["InstituteName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["InstituteName"]);
                    objFacIns.IsActive = (Convert.ToString(Dt.Rows[0]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsActive"]);
                    objFacIns.IsDeleted = (Convert.ToString(Dt.Rows[0]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsDeleted"]);
                    return objFacIns;
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
    }
}