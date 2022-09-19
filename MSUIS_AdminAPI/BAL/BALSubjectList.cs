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
    public class BALSubjectList
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        #region Subject List Get
        public List<MstSubject> SubjectListGet()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstSubjectGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);
                Int32 count = 0;
                List<MstSubject> ObjLstSubject = new List<MstSubject>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstSubject objSubject = new MstSubject();

                        objSubject.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objSubject.SubjectName = (Convert.ToString(Dt.Rows[i]["SubjectName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["SubjectName"]);                        
                        objSubject.FacultyId = (Convert.ToString(Dt.Rows[i]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        objSubject.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objSubject.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objSubject.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        count = count + 1;
                        objSubject.IndexId = count;
                        ObjLstSubject.Add(objSubject);
                    }
                }
                return ObjLstSubject;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Subject List Get For Drop Down
        public List<MstSubject> SubjectListGetForDropDown()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstSubjectGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<MstSubject> ObjLstSubject = new List<MstSubject>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstSubject objSubject = new MstSubject();

                        objSubject.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objSubject.SubjectName = (Convert.ToString(Dt.Rows[i]["SubjectName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["SubjectName"]);                        
                        ObjLstSubject.Add(objSubject);
                    }
                }
                return ObjLstSubject;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Subject List Get By Id
        public MstSubject SubjectGetbyId(Int64? SubjectId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MstSubjectGetbyId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", SubjectId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    MstSubject objSubject = new MstSubject();

                    objSubject.Id = Convert.ToInt64(Dt.Rows[0]["Id"]);
                    objSubject.SubjectName = (Convert.ToString(Dt.Rows[0]["SubjectName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["SubjectName"]);
                    objSubject.FacultyId = (Convert.ToString(Dt.Rows[0]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["FacultyId"]);
                    objSubject.FacultyName = (Convert.ToString(Dt.Rows[0]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["FacultyName"]);
                    objSubject.IsActive = (Convert.ToString(Dt.Rows[0]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsActive"]);
                    
                    return objSubject;

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

        #region Subject List By Faculty Id
        public List<MstSubject> SubjectListGetByFacultyId(int? FacultyId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MstSubjectGetbyFacultyId", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", FacultyId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<MstSubject> ObjLstSubject = new List<MstSubject>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstSubject ObjSubject = new MstSubject();

                        ObjSubject.Id = Convert.ToInt64(Dt.Rows[i]["SubjectId"]);
                        ObjSubject.FacultyId = (Convert.ToString(Dt.Rows[i]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        ObjSubject.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        ObjSubject.SubjectName = (Convert.ToString(Dt.Rows[i]["SubjectName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["SubjectName"]);
                        //ObjSubject.IsActiveSts = Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        ObjSubject.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        //ObjSubject.IsDeleted = Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);

                        ObjLstSubject.Add(ObjSubject);
                    }
                    return ObjLstSubject;
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