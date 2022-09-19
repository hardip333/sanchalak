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
    public class BALBoardOfStudySubjectList
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        #region Bos-Subject List Get
        public List<BoardOfStudySubjectMap> BOSSubjectListGet()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("BoardofStudySubjectMapGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<BoardOfStudySubjectMap> ObjLstBOSSubject = new List<BoardOfStudySubjectMap>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        BoardOfStudySubjectMap objBosSubject = new BoardOfStudySubjectMap();

                        objBosSubject.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objBosSubject.BoardofStudyId = (Convert.ToString(Dt.Rows[i]["BoardofStudyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["BoardofStudyId"]);
                        objBosSubject.BoardName = (Convert.ToString(Dt.Rows[i]["BoardName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["BoardName"]);
                        objBosSubject.SubjectId = (Convert.ToString(Dt.Rows[i]["SubjectId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["SubjectId"]);
                        objBosSubject.SubjectName = (Convert.ToString(Dt.Rows[i]["SubjectName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["SubjectName"]);
                        objBosSubject.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objBosSubject.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);
                        
                        ObjLstBOSSubject.Add(objBosSubject);
                    }
                }
                return ObjLstBOSSubject;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Bos-Subject Map Get By Id
        public BoardOfStudySubjectMap BosSubjectListGetById(int? Id)
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("BoardofStudySubjectMapGetById", Con);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@Id", Id);

                Da.SelectCommand = Cmd;
                Da.Fill(Dt);

                if (Dt.Rows.Count > 0)
                {
                    BoardOfStudySubjectMap objBosSub = new BoardOfStudySubjectMap();

                    objBosSub.Id = Convert.ToInt32(Dt.Rows[0]["Id"]);
                    objBosSub.BoardofStudyId = (Convert.ToString(Dt.Rows[0]["BoardofStudyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["BoardofStudyId"]);
                    objBosSub.BoardName = (Convert.ToString(Dt.Rows[0]["BoardName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["BoardName"]);
                    objBosSub.SubjectId = (Convert.ToString(Dt.Rows[0]["SubjectId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["SubjectId"]);
                    objBosSub.SubjectName = (Convert.ToString(Dt.Rows[0]["SubjectName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["SubjectName"]);
                    objBosSub.IsActive = (Convert.ToString(Dt.Rows[0]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsActive"]);
                    objBosSub.IsDeleted = (Convert.ToString(Dt.Rows[0]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsDeleted"]);
                    
                    return objBosSub;
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