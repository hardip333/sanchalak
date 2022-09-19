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
    public class BALInstructionMedium
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        #region Instruction Medium List
        public List<InstructionMedium> InstructionMediumGet()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstInstructionMediumGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);
                Int32 count = 0;
                List<InstructionMedium> ObjLstInstMed = new List<InstructionMedium>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        InstructionMedium objInstMed = new InstructionMedium();

                        objInstMed.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objInstMed.InstructionMediumName = (Convert.ToString(Dt.Rows[i]["InstructionMediumName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstructionMediumName"]);
                        objInstMed.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objInstMed.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]); 
                        count = count + 1;
                        objInstMed.IndexId = count;
                        ObjLstInstMed.Add(objInstMed);                        
                    }
                }
                return ObjLstInstMed;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Instruction Medium List For Drop Down
        public List<InstructionMedium> InstructionMediumGetForDropDown()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstInstructionMediumGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<InstructionMedium> ObjLstInstMed = new List<InstructionMedium>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        InstructionMedium objInstMed = new InstructionMedium();

                        objInstMed.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objInstMed.InstructionMediumName = (Convert.ToString(Dt.Rows[i]["InstructionMediumName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstructionMediumName"]);                        
                        ObjLstInstMed.Add(objInstMed);
                    }
                }
                return ObjLstInstMed;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Instruction Medium List Get By Id
        public InstructionMedium InstructionMediumGetById(int? InstMedId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MstInstructionMediumGetById", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@InstMediumId", InstMedId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if(Dt.Rows.Count > 0)
                {
                    InstructionMedium objInstMed = new InstructionMedium();

                    objInstMed.Id = Convert.ToInt32(Dt.Rows[0]["Id"]);
                    objInstMed.InstructionMediumName = (Convert.ToString(Dt.Rows[0]["InstructionMediumName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["InstructionMediumName"]); ;
                    objInstMed.IsActive = (Convert.ToString(Dt.Rows[0]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsActive"]);
                    objInstMed.IsDeleted = (Convert.ToString(Dt.Rows[0]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsDeleted"]);
                    objInstMed.IsActiveSts = (Convert.ToString(Dt.Rows[0]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["IsActiveSts"]);

                    return objInstMed;

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