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
    public class BALProgrammeType
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        #region Programme Type List
        public List<MstProgrammeType> ProgrammeTypeGet()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstProgrammeTypeGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);
                Int32 count = 0;
                List<MstProgrammeType> ObjLstProgType = new List<MstProgrammeType>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgrammeType objProgType = new MstProgrammeType();

                        objProgType.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objProgType.ProgrammeTypeName = (Convert.ToString(Dt.Rows[i]["ProgrammeTypeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeTypeName"]);
                        objProgType.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        objProgType.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objProgType.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);
                        count = count + 1;
                        objProgType.IndexId = count;

                        ObjLstProgType.Add(objProgType);                        
                    }
                }
                return ObjLstProgType;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Programme Type List For Drop Down
        public List<MstProgrammeType> ProgrammeTypeGetForDropDown()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstProgrammeTypeGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<MstProgrammeType> ObjLstProgType = new List<MstProgrammeType>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgrammeType objProgType = new MstProgrammeType();

                        objProgType.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objProgType.ProgrammeTypeName = (Convert.ToString(Dt.Rows[i]["ProgrammeTypeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeTypeName"]);                      
                        ObjLstProgType.Add(objProgType);
                    }
                }
                return ObjLstProgType;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Programme Type List Get By Id
        public MstProgrammeType ProgrammeTypeListGetbyId(Int64? ProgTypeId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MstProgrammeTypeGetbyId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeTypeId", ProgTypeId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if(Dt.Rows.Count > 0)
                {
                    MstProgrammeType objProgType = new MstProgrammeType();

                    objProgType.Id = Convert.ToInt64(Dt.Rows[0]["Id"]);
                    objProgType.ProgrammeTypeName = (Convert.ToString(Dt.Rows[0]["ProgrammeTypeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["ProgrammeTypeName"]);
                    objProgType.IsActiveSts = (Convert.ToString(Dt.Rows[0]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["IsActiveSts"]);
                    objProgType.IsActive = (Convert.ToString(Dt.Rows[0]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsActive"]);
                    objProgType.IsDeleted = (Convert.ToString(Dt.Rows[0]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsDeleted"]);
                    
                    return objProgType;

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