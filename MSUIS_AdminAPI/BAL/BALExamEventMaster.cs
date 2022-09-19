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
    public class BALExamEventMaster
    {
        public ExamEventMaster setRowIntoObjForAdmin(ExamEventMaster element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr[0].ToString());
            element.AcademicYearId = Convert.ToInt32(dr[1].ToString());
            element.AcademicYearCode = dr[8].ToString();
            element.Month = Convert.ToInt32(dr[2].ToString());
            element.Year = Convert.ToInt32(dr[3].ToString());
            element.DisplayName = dr[4].ToString();
            element.IsActive = Convert.ToBoolean(dr[5].ToString());
            element.IsDeleted = Convert.ToBoolean(dr[6].ToString());
            if (!string.IsNullOrEmpty(dr[7].ToString()))
                element.IsClosed = Convert.ToBoolean(dr[7].ToString());
            element.LateFeesFacultyPower = dr[9].ToString();
            element.LateFeesVCPower = dr[10].ToString();
            return element;

        }

        public ExamEventMaster setRowIntoObjForUser(ExamEventMaster element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr[0].ToString());
            element.AcademicYearId = Convert.ToInt32(dr[1].ToString());
            element.AcademicYearCode = dr[8].ToString();
            element.Month = Convert.ToInt32(dr[2].ToString());
            element.Year = Convert.ToInt32(dr[3].ToString());
            element.DisplayName = dr[4].ToString();
            if (!string.IsNullOrEmpty(dr[5].ToString()))
                element.IsClosed = Convert.ToBoolean(dr[5].ToString());
            element.LateFeesFacultyPower = dr[6].ToString();
            element.LateFeesVCPower = dr[7].ToString();
            return element;
        }

        public ExamEventMaster setRowIntoObjForAll(ExamEventMaster element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr[0].ToString());
            element.AcademicYearId = Convert.ToInt32(dr[1].ToString());
            element.AcademicYearCode = dr[11].ToString();
            element.Month = Convert.ToInt32(dr[2].ToString());
            element.Year = Convert.ToInt32(dr[3].ToString());
            element.DisplayName = dr[4].ToString();
            element.IsActive = Convert.ToBoolean(dr[5].ToString());
            element.CreatedOn = Convert.ToDateTime(dr[6].ToString());
            element.CreatedBy = Convert.ToInt32(dr[7].ToString());
            if (!string.IsNullOrEmpty(dr[8].ToString()))
                element.ModifiedOn = Convert.ToDateTime(dr[8].ToString());
            if (!string.IsNullOrEmpty(dr[9].ToString()))
                element.ModifiedBy = Convert.ToInt32(dr[9].ToString());
            element.IsDeleted = Convert.ToBoolean(dr[10].ToString());
            if (!string.IsNullOrEmpty(dr[10].ToString()))
                element.IsClosed = Convert.ToBoolean(dr[10].ToString());
            element.LateFeesFacultyPower = dr[11].ToString();
            element.LateFeesVCPower = dr[11].ToString();
            return element;

        }


        public List<ExamEventMaster> getExamEventMasterList(string flag, int? IsDeleted, int? IsActive, int? AcademicYearId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("ExamEventMasterListGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Flag", flag);
                Cmd.Parameters.AddWithValue("@IsActive", IsActive);
                Cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
                Cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<ExamEventMaster> ObjListExamEventMaster = new List<ExamEventMaster>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ExamEventMaster objExamEventMaster = new ExamEventMaster();
                        if (flag == "admin")
                        {
                            setRowIntoObjForAdmin(objExamEventMaster, Dt.Rows[i]);
                        }
                        else if (flag == "user")
                        {
                            setRowIntoObjForUser(objExamEventMaster, Dt.Rows[i]);
                        }

                        ObjListExamEventMaster.Add(objExamEventMaster);
                    }
                }
                return ObjListExamEventMaster;
            }
            catch (Exception e)
            {
                return null;
            }
        }

#region Exam Event List For Drop Down
        public List<ExamEventMaster> ExamEventGetForDropDown()
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("ExamEventGetForDropDown", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd; Da.Fill(Dt); List<ExamEventMaster> ObjLstExamEvent = new List<ExamEventMaster>(); if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ExamEventMaster objExamEvent = new ExamEventMaster(); objExamEvent.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objExamEvent.AcademicYearCode = (Convert.ToString(Dt.Rows[i]["AcademicYearCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);
                        objExamEvent.DisplayName = (Convert.ToString(Dt.Rows[i]["DisplayName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["DisplayName"]);
                        ObjLstExamEvent.Add(objExamEvent);
                    }
                }
                return ObjLstExamEvent;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion


    }
}