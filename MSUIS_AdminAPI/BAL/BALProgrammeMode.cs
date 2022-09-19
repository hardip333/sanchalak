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
    public class BALProgrammeMode
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        #region Programme Mode List
        public List<MstProgrammeMode> ProgrammeModeGet()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstProgrammeModeGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);
                Int32 count = 0;
                List<MstProgrammeMode> ObjLstProgMode = new List<MstProgrammeMode>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgrammeMode objProgMode = new MstProgrammeMode();

                        objProgMode.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objProgMode.ProgrammeModeName = (Convert.ToString(Dt.Rows[i]["ProgrammeModeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeModeName"]);
                        objProgMode.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        objProgMode.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objProgMode.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);
                        count = count + 1;
                        objProgMode.IndexId = count;

                        ObjLstProgMode.Add(objProgMode);                        
                    }
                }
                return ObjLstProgMode;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Programme Mode List For Drop Down
        public List<MstProgrammeMode> ProgrammeModeGetForDropDown()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstProgrammeModeGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<MstProgrammeMode> ObjLstProgMode = new List<MstProgrammeMode>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgrammeMode objProgMode = new MstProgrammeMode();

                        objProgMode.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objProgMode.ProgrammeModeName = (Convert.ToString(Dt.Rows[i]["ProgrammeModeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeModeName"]);                        
                        ObjLstProgMode.Add(objProgMode);
                    }
                }
                return ObjLstProgMode;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Programme Mode List Get By Id
        public MstProgrammeMode ProgrammeModeListGetbyId(Int64? ProgModeId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MstProgrammeModeGetbyId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeModeId", ProgModeId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if(Dt.Rows.Count > 0)
                {
                    MstProgrammeMode objProgMode = new MstProgrammeMode();

                    objProgMode.Id = Convert.ToInt64(Dt.Rows[0]["Id"]);
                    objProgMode.ProgrammeModeName = (Convert.ToString(Dt.Rows[0]["ProgrammeModeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["ProgrammeModeName"]);
                    objProgMode.IsActiveSts = (Convert.ToString(Dt.Rows[0]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["IsActiveSts"]);
                    objProgMode.IsActive = (Convert.ToString(Dt.Rows[0]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsActive"]);
                    objProgMode.IsDeleted = (Convert.ToString(Dt.Rows[0]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsDeleted"]);
                    
                    return objProgMode;

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