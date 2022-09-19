using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MSUISApi.BAL
{
    public class BALAppearanceTypeMaster
    {
        //public List<AppearanceTypeMaster> getAppearanceTypeMasterList(int? flag, int? IsDeleted, int? IsActive)
        //{
        //    string str = "";
        //    if (flag == 1)
        //    {
        //        str = "Id,AppearanceType,IsActive,IsDeleted";
        //    }
        //    else if (flag == 2)
        //    {
        //        str = "Id,AppearanceType";
        //    }
        //    else
        //    {
        //        str = "*";
        //    }

        //    string query = "select " + str + " from AppearanceTypeMaster";
        //    string where = "";
        //    if (IsDeleted != null)
        //    {
        //        where += " IsDeleted = " + IsDeleted + " and ";
        //    }
        //    if (IsActive != null)
        //    {
        //        where += " IsActive = " + IsActive + " and ";
        //    }

        //    if (where.Length > 0)
        //    {
        //        query += " where " + where.Substring(0, where.Length - 5);
        //    }

        //    DataSet ds = Database.getDataSet(query);
        //    DataTable dt = ds.Tables[0];
        //    if (dt == null || dt.Rows.Count == 0)
        //    {
        //        return null;
        //    }
        //    int n = 0, count = dt.Rows.Count;
        //    List<AppearanceTypeMaster> ExamMasterList = new List<AppearanceTypeMaster>();
        //    while (n < count)
        //    {
        //        AppearanceTypeMaster element = new AppearanceTypeMaster();
        //        if (flag == 1)
        //        {
        //            setRowIntoObjForAdmin(element, dt.Rows[n]);
        //        }
        //        else if (flag == 2)
        //        {
        //            setRowIntoObjForUser(element, dt.Rows[n]);
        //        }
        //        else
        //        {
        //            setRowIntoObjForAll(element, dt.Rows[n]);
        //        }
        //        ExamMasterList.Add(element);
        //        n++;
        //    }
        //    return ExamMasterList;
        //}

        public AppearanceTypeMaster setRowIntoObjForAdmin(AppearanceTypeMaster element, DataRow dr)
        {
            element.Id= Convert.ToInt32(dr[0].ToString());
            element.AppearanceType = dr[1].ToString();
            element.IsActive = Convert.ToBoolean(dr[2].ToString());
            element.IsDeleted = Convert.ToBoolean(dr[3].ToString());
            return element;

        }

        public AppearanceTypeMaster setRowIntoObjForUser(AppearanceTypeMaster element, DataRow dr)
        {
            element.Id = Convert.ToInt64(dr[0].ToString());
            element.AppearanceType = dr[1].ToString();
            return element;
        }

        public AppearanceTypeMaster setRowIntoObjForAll(AppearanceTypeMaster element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr[0].ToString());
            element.AppearanceType = dr[1].ToString();
            element.IsActive = Convert.ToBoolean(dr[2].ToString());
            element.CreatedOn = Convert.ToDateTime(dr[3].ToString());
            element.CreatedBy = Convert.ToInt32(dr[4].ToString());
            if (!string.IsNullOrEmpty(dr[5].ToString()))
                element.ModifiedOn = Convert.ToDateTime(dr[5].ToString());
            if (!string.IsNullOrEmpty(dr[6].ToString()))
                element.ModifiedBy = Convert.ToInt32(dr[6].ToString());
            element.IsDeleted = Convert.ToBoolean(dr[7].ToString());
            return element;
        }

        

        public List<AppearanceTypeMaster> ApperanceTypeGetByProgInstancePartTermId(Int64? ProgInstancePartTermId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("ApperanceTypeGetByProgInstancePartTermId", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@ProgInstancePartTermId", ProgInstancePartTermId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<AppearanceTypeMaster> ObjListAppearanceTypeMaster = new List<AppearanceTypeMaster>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        AppearanceTypeMaster element = new AppearanceTypeMaster();
                        element.Id = Convert.ToInt32(Dt.Rows[i][0].ToString());
                        element.AppearanceType = Dt.Rows[i][1].ToString();

                        ObjListAppearanceTypeMaster.Add(element);
                    }
                }
                return ObjListAppearanceTypeMaster;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<AppearanceTypeMaster> ApperanceTypeGetByProgPartTermId(Int64? ProgInstancePartTermId,Int64? FacultyExamMapId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("ApperanceTypeGetByProgPartTermId", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@ProgPartTermId", ProgInstancePartTermId);
                Cmd.Parameters.AddWithValue("@FacultyExamMapId", FacultyExamMapId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<AppearanceTypeMaster> ObjListAppearanceTypeMaster = new List<AppearanceTypeMaster>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        AppearanceTypeMaster element = new AppearanceTypeMaster();
                        element.Id = Convert.ToInt32(Dt.Rows[i][0].ToString());
                        element.AppearanceType = Dt.Rows[i][1].ToString();

                        ObjListAppearanceTypeMaster.Add(element);
                    }
                }
                return ObjListAppearanceTypeMaster;
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}