using Microsoft.Ajax.Utilities;
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
    public class BALBoardOfStudyList
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        #region Board Of Study List Get
        public List<BoardOfStudy> BOSListGet()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstBoardOfStudyGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);
                Int32 count = 0;
                List<BoardOfStudy> ObjLstBOS = new List<BoardOfStudy>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        BoardOfStudy objBos = new BoardOfStudy();

                        objBos.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objBos.BoardName = (Convert.ToString(Dt.Rows[i]["BoardName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["BoardName"]);
                        objBos.FacultyId = (Convert.ToString(Dt.Rows[i]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        objBos.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objBos.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objBos.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        count = count + 1;
                        objBos.IndexId = count;
                        ObjLstBOS.Add(objBos);
                    }
                }
                return ObjLstBOS;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Board Of Study List Get For Drop Down
        public List<BoardOfStudy> BOSListGetForDropDown()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstBoardOfStudyGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<BoardOfStudy> ObjLstBOS = new List<BoardOfStudy>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        BoardOfStudy objBos = new BoardOfStudy();

                        objBos.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objBos.BoardName = (Convert.ToString(Dt.Rows[i]["BoardName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["BoardName"]);                        
                        ObjLstBOS.Add(objBos);
                    }
                }
                return ObjLstBOS;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Board Of Study List By Id
        public BoardOfStudy BosGetbyId(int BosId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MstBoardOfStudyGetbyId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", BosId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    BoardOfStudy objBos = new BoardOfStudy();

                    objBos.Id = Convert.ToInt32(Dt.Rows[0]["Id"]);
                    objBos.BoardName = (Convert.ToString(Dt.Rows[0]["BoardName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["BoardName"]);
                    objBos.FacultyId = (Convert.ToString(Dt.Rows[0]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["FacultyId"]);
                    objBos.FacultyName = (Convert.ToString(Dt.Rows[0]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["FacultyName"]);
                    //objBos.IsActive = (Convert.ToString(Dt.Rows[0]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsActive"]);
                    //objBos.IsActiveSts = (Convert.ToString(Dt.Rows[0]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["IsActiveSts"]);

                    return objBos;

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

        #region Board of study List By Faculty Id
        public List<BoardOfStudy> MstBoardofStudyListGetByFacultyId(int? FacultyId)
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstBoardOfStudyGetByFacultyId", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<BoardOfStudy> ObjLstBOS = new List<BoardOfStudy>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        BoardOfStudy ObjBOS = new BoardOfStudy();

                        ObjBOS.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        ObjBOS.BoardName = (Convert.ToString(Dt.Rows[i]["BoardName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["BoardName"]);
                        ObjBOS.FacultyId = (Convert.ToInt32(Dt.Rows[i]["FacultyId"]).ToString()).IsNullOrWhiteSpace() ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        ObjBOS.FacultyName = ((Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : (Convert.ToString(Dt.Rows[i]["FacultyName"])));
                        //ObjBOS.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        ObjBOS.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        //ObjBOS.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);

                        ObjLstBOS.Add(ObjBOS);
                    }

                }

                return ObjLstBOS;
            }
            catch (Exception e)
            {
                return null;
            }

        }
        #endregion
    }
}