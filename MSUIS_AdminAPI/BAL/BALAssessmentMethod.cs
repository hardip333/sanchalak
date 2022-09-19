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
    public class BALAssessmentMethod
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        #region Assessment Method Get
        public List<MstAssessmentMethod> AssessmentMethodGet()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstAssessmentMethodGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);
                Int32 count = 0;
                List<MstAssessmentMethod> ObjLstAM = new List<MstAssessmentMethod>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstAssessmentMethod objAM = new MstAssessmentMethod();

                        objAM.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objAM.AssessmentMethodName = (Convert.ToString(Dt.Rows[i]["AssessmentMethodName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["AssessmentMethodName"]);
                        objAM.AssessmentMethodCode = (Convert.ToString(Dt.Rows[i]["AssessmentMethodCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["AssessmentMethodCode"]);
                        objAM.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]); 
                        objAM.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);
                        count = count + 1;
                        objAM.IndexId = count;
                        ObjLstAM.Add(objAM);                        
                    }
                }
                return ObjLstAM;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Assessment Method Get For Drop Down
        public List<MstAssessmentMethod> AssessmentMethodGetForDropDown()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstAssessmentMethodGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<MstAssessmentMethod> ObjLstAM = new List<MstAssessmentMethod>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstAssessmentMethod objAM = new MstAssessmentMethod();

                        objAM.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objAM.AssessmentMethodName = (Convert.ToString(Dt.Rows[i]["AssessmentMethodName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["AssessmentMethodName"]);
                        objAM.AssessmentMethodCode = (Convert.ToString(Dt.Rows[i]["AssessmentMethodCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["AssessmentMethodCode"]);
                        ObjLstAM.Add(objAM);
                    }
                }
                return ObjLstAM;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Assessment Method Get By Id
        public MstAssessmentMethod AssessmentMethodGetById(int? AMId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MstAssessmentMethodGetById", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", AMId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if(Dt.Rows.Count > 0)
                {
                    MstAssessmentMethod objAM = new MstAssessmentMethod();

                    objAM.Id = Convert.ToInt32(Dt.Rows[0]["Id"]);
                    objAM.AssessmentMethodName = (Convert.ToString(Dt.Rows[0]["AssessmentMethodName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["AssessmentMethodName"]);
                    objAM.AssessmentMethodCode = (Convert.ToString(Dt.Rows[0]["AssessmentMethodCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["AssessmentMethodCode"]);
                    objAM.IsActive = (Convert.ToString(Dt.Rows[0]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsActive"]);
                    objAM.IsDeleted = (Convert.ToString(Dt.Rows[0]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsDeleted"]);

                    return objAM;

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

        #region Assessment Method Get For Drop Down by Programme Part Term Id 
        public List<MstAssessmentMethod> AssessmentGetForDropDownByPartTermId(int? PartTermId, int? SpecialisationId)
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstAssessmentMethodGetByPartTermId", Con);

                Cmd.Parameters.AddWithValue("@PartTermId", PartTermId);
                Cmd.Parameters.AddWithValue("@SpecialisationId", SpecialisationId);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<MstAssessmentMethod> ObjLstAM = new List<MstAssessmentMethod>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstAssessmentMethod objAM = new MstAssessmentMethod();

                        objAM.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objAM.AssessmentMethodName = (Convert.ToString(Dt.Rows[i]["AssessmentMethodName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["AssessmentMethodName"]);
                        objAM.AssessmentMethodCode = (Convert.ToString(Dt.Rows[i]["AssessmentMethodCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["AssessmentMethodCode"]);
                        ObjLstAM.Add(objAM);
                    }
                }
                return ObjLstAM;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion
        
    }
}