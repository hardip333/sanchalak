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
    public class BALEvaluation
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        #region Evaluation List
        public List<MstEvaluation> EvaluationListGet()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstEvaluationGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);
                Int32 count = 0;
                List<MstEvaluation> ObjListEval = new List<MstEvaluation>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstEvaluation objEval = new MstEvaluation();

                        objEval.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objEval.EvaluationName = (Convert.ToString(Dt.Rows[i]["EvaluationName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["EvaluationName"]);
                        objEval.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objEval.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);
                        objEval.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        count = count + 1;
                        objEval.IndexId = count;
                        ObjListEval.Add(objEval);                        
                    }
                }
                return ObjListEval;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Evaluation List For Drop Down
        public List<MstEvaluation> EvaluationListGetForDropDown()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstEvaluationGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<MstEvaluation> ObjListEval = new List<MstEvaluation>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstEvaluation objEval = new MstEvaluation();

                        objEval.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objEval.EvaluationName = (Convert.ToString(Dt.Rows[i]["EvaluationName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["EvaluationName"]);                        
                        ObjListEval.Add(objEval);
                    }
                }
                return ObjListEval;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Evaluation List Get By Id
        public MstEvaluation EvaluationListGetbyId(int? EvalId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MstEvaluationGetbyId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EvaluationId", EvalId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if(Dt.Rows.Count > 0)
                {
                    MstEvaluation objEval = new MstEvaluation();

                    objEval.Id = Convert.ToInt32(Dt.Rows[0]["Id"]);
                    objEval.EvaluationName = (Convert.ToString(Dt.Rows[0]["EvaluationName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["EvaluationName"]);
                    objEval.IsActive = (Convert.ToString(Dt.Rows[0]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsActive"]);
                    objEval.IsDeleted = (Convert.ToString(Dt.Rows[0]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsDeleted"]);
                    objEval.IsActiveSts = (Convert.ToString(Dt.Rows[0]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["IsActiveSts"]);

                    return objEval;

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