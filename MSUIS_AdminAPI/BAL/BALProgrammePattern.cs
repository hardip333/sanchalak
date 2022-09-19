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
    public class BALProgrammePattern
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        #region Programme Pattern List
        public List<MstExaminationPattern> ProgrammePatternGet()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstExaminationPatternGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);
                Int32 count = 0;
                List<MstExaminationPattern> ObjLstProgPattern = new List<MstExaminationPattern>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstExaminationPattern objProgPattern = new MstExaminationPattern();
                        
                        objProgPattern.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objProgPattern.ExaminationPatternName = (Convert.ToString(Dt.Rows[i]["ExaminationPatternName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ExaminationPatternName"]);
                        objProgPattern.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        count = count + 1;
                        objProgPattern.IndexId = count;
                        ObjLstProgPattern.Add(objProgPattern);                        
                    }
                }
                return ObjLstProgPattern;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Programme Pattern List For Drop Down
        public List<MstExaminationPattern> ProgrammePatternGetForDropDown()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstExaminationPatternGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<MstExaminationPattern> ObjLstProgPattern = new List<MstExaminationPattern>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstExaminationPattern objProgPattern = new MstExaminationPattern();

                        objProgPattern.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objProgPattern.ExaminationPatternName = (Convert.ToString(Dt.Rows[i]["ExaminationPatternName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ExaminationPatternName"]);                       
                        ObjLstProgPattern.Add(objProgPattern);
                    }
                }
                return ObjLstProgPattern;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Programme Pattern List Get By Id
        public MstExaminationPattern ProgrammePatternListGetbyId(int? ProgpatternId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MstExaminationPatternGetbyId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", ProgpatternId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if(Dt.Rows.Count > 0)
                {
                    MstExaminationPattern objProgPattern = new MstExaminationPattern();

                    objProgPattern.Id = Convert.ToInt32(Dt.Rows[0]["Id"]);
                    objProgPattern.ExaminationPatternName = (Convert.ToString(Dt.Rows[0]["ExaminationPatternName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["ExaminationPatternName"]);
                    objProgPattern.IsActive = (Convert.ToString(Dt.Rows[0]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsActive"]);

                    return objProgPattern;

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