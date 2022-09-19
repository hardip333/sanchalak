using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MSUISApi.BAL
{
    public class BALExamSlotMaster
    {

        public ExamSlotMaster setRowIntoObjForAdmin(ExamSlotMaster element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr[0].ToString());
            element.StartTime = TimeSpan.Parse(dr[1].ToString());
            element.EndTime = TimeSpan.Parse(dr[2].ToString());
            element.SlotName = dr[3].ToString();
            element.IsActive = Convert.ToBoolean(dr[4].ToString());
            element.IsDeleted = Convert.ToBoolean(dr[5].ToString());
            return element;

        }

        public ExamSlotMaster setRowIntoObjForUser(ExamSlotMaster element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr[0].ToString());
            element.StartTime = TimeSpan.Parse(dr[1].ToString());
            element.EndTime = TimeSpan.Parse(dr[2].ToString());
            element.SlotName = dr[3].ToString();
            return element;
        }

        public ExamSlotMaster setRowIntoObjForAll(ExamSlotMaster element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr[0].ToString());
            element.StartTime = TimeSpan.Parse(dr[1].ToString());
            element.EndTime = TimeSpan.Parse(dr[2].ToString());
            element.SlotName = dr[3].ToString();
            element.IsActive = Convert.ToBoolean(dr[4].ToString());
            element.CreatedBy = Convert.ToInt32(dr[5].ToString());
            element.CreatedOn = Convert.ToDateTime(dr[6].ToString());
            if (!string.IsNullOrEmpty(dr[7].ToString()))
                element.ModifiedBy = Convert.ToInt32(dr[7].ToString());
            if (!string.IsNullOrEmpty(dr[8].ToString()))
                element.ModifiedOn = Convert.ToDateTime(dr[8].ToString());
            element.IsDeleted = Convert.ToBoolean(dr[9].ToString());
            return element;
        }


        public List<ExamSlotMaster> getExamSlotMasterList(string flag, int? IsDeleted, int? IsActive)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("ExamSlotMasterListGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Flag", flag);
                Cmd.Parameters.AddWithValue("@IsActive", IsActive);
                Cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<ExamSlotMaster> ObjListExamSlotMaster = new List<ExamSlotMaster>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ExamSlotMaster objExamSlotMaster = new ExamSlotMaster();
                        if (flag == "admin")
                        {
                            setRowIntoObjForAdmin(objExamSlotMaster, Dt.Rows[i]);
                        }
                        else if (flag == "user")
                        {
                            setRowIntoObjForUser(objExamSlotMaster, Dt.Rows[i]);
                        }

                        ObjListExamSlotMaster.Add(objExamSlotMaster);
                    }
                }
                return ObjListExamSlotMaster;
            }
            catch (Exception e)
            {
                return null;
            }
        }


    }
}