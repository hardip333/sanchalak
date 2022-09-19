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
    public class BALTeachingLearning
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        #region Teaching Learning Method Get
        public List<MstTeachingLearningMethod> TeachingLearningGet()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstTeachingLearningMethodGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);
                Int32 count = 0;
                List<MstTeachingLearningMethod> ObjLstTLM = new List<MstTeachingLearningMethod>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstTeachingLearningMethod objTLM = new MstTeachingLearningMethod();

                        objTLM.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objTLM.TeachingLearningMethodName = (Convert.ToString(Dt.Rows[i]["TeachingLearningMethodName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["TeachingLearningMethodName"]);
                        objTLM.TeachingLearningMethodCode = (Convert.ToString(Dt.Rows[i]["TeachingLearningMethodCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["TeachingLearningMethodCode"]);
                        objTLM.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        count = count + 1;
                        objTLM.IndexId = count;
                        ObjLstTLM.Add(objTLM);                        
                    }
                }
                return ObjLstTLM;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Teaching Learning Method Get For Drop Down
        public List<MstTeachingLearningMethod> TeachingLearningGetForDropDown()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstTeachingLearningMethodGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<MstTeachingLearningMethod> ObjLstTLM = new List<MstTeachingLearningMethod>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstTeachingLearningMethod objTLM = new MstTeachingLearningMethod();

                        objTLM.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objTLM.TeachingLearningMethodName = (Convert.ToString(Dt.Rows[i]["TeachingLearningMethodName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["TeachingLearningMethodName"]);
                        objTLM.TeachingLearningMethodCode = (Convert.ToString(Dt.Rows[i]["TeachingLearningMethodCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["TeachingLearningMethodCode"]);
                        ObjLstTLM.Add(objTLM);
                    }
                }
                return ObjLstTLM;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Teaching Learning Method Get For Drop Down by Programme Part Term Id - By Bhavita
        public List<MstTeachingLearningMethod> TeachingLearningGetForDropDownByPartTermId(int? PartTermId, int? SpecialisationId)
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstTeachingLearningMethodGetByPartTermId", Con);

                Cmd.Parameters.AddWithValue("@PartTermId", PartTermId);
                Cmd.Parameters.AddWithValue("@SpecialisationId", SpecialisationId);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<MstTeachingLearningMethod> ObjLstTLM = new List<MstTeachingLearningMethod>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstTeachingLearningMethod objTLM = new MstTeachingLearningMethod();

                        objTLM.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objTLM.TeachingLearningMethodName = (Convert.ToString(Dt.Rows[i]["TeachingLearningMethodName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["TeachingLearningMethodName"]);
                        objTLM.TeachingLearningMethodCode = (Convert.ToString(Dt.Rows[i]["TeachingLearningMethodCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["TeachingLearningMethodCode"]);
                        ObjLstTLM.Add(objTLM);
                    }
                }
                return ObjLstTLM;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Teaching Learning Method Get By Id
        public MstTeachingLearningMethod TeachingLearningMethodGetById(int? TLMId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MstTeachingLearningMethodGetById", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", TLMId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if(Dt.Rows.Count > 0)
                {
                    MstTeachingLearningMethod objTLM = new MstTeachingLearningMethod();

                    objTLM.Id = Convert.ToInt32(Dt.Rows[0]["Id"]);
                    objTLM.TeachingLearningMethodName = (Convert.ToString(Dt.Rows[0]["TeachingLearningMethodName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["TeachingLearningMethodName"]);
                    objTLM.TeachingLearningMethodCode = (Convert.ToString(Dt.Rows[0]["TeachingLearningMethodCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["TeachingLearningMethodCode"]);
                    objTLM.IsActive = (Convert.ToString(Dt.Rows[0]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsActive"]);

                    return objTLM;

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