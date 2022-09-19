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
    public class BALProgrammeLevel
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        #region Programme Level List
        public List<MstProgrammeLevel> ProgrammeLevelGet()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstProgrammeLevelGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);
                Int32 count = 0;
                List<MstProgrammeLevel> ObjLstProgLevel = new List<MstProgrammeLevel>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgrammeLevel objProgLevel = new MstProgrammeLevel();

                        objProgLevel.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objProgLevel.ProgrammeLevelName = (Convert.ToString(Dt.Rows[i]["ProgrammeLevelName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeLevelName"]);
                        objProgLevel.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        objProgLevel.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objProgLevel.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]); 
                        count = count + 1;
                        objProgLevel.IndexId = count;

                        ObjLstProgLevel.Add(objProgLevel);                        
                    }
                }
                return ObjLstProgLevel;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Programme Level List For Drop Down
        public List<MstProgrammeLevel> ProgrammeLevelGetForDropDown()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstProgrammeLevelGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<MstProgrammeLevel> ObjLstProgLevel = new List<MstProgrammeLevel>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgrammeLevel objProgLevel = new MstProgrammeLevel();

                        objProgLevel.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objProgLevel.ProgrammeLevelName = (Convert.ToString(Dt.Rows[i]["ProgrammeLevelName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeLevelName"]);                        
                        ObjLstProgLevel.Add(objProgLevel);
                    }
                }
                return ObjLstProgLevel;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Programme Level List Get By Id
        public MstProgrammeLevel ProgrammeLevelListGetbyId(Int64? ProgLvlId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MstProgrammeLevelGetbyId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeLevelId", ProgLvlId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if(Dt.Rows.Count > 0)
                {
                    MstProgrammeLevel objProgLevel = new MstProgrammeLevel();

                    objProgLevel.Id = Convert.ToInt64(Dt.Rows[0]["Id"]);
                    objProgLevel.ProgrammeLevelName = (Convert.ToString(Dt.Rows[0]["ProgrammeLevelName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["ProgrammeLevelName"]);
                    objProgLevel.IsActiveSts = (Convert.ToString(Dt.Rows[0]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["IsActiveSts"]);
                    objProgLevel.IsActive = (Convert.ToString(Dt.Rows[0]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsActive"]);
                    objProgLevel.IsDeleted = (Convert.ToString(Dt.Rows[0]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsDeleted"]); 

                    return objProgLevel;

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