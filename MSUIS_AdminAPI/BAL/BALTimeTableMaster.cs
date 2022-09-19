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
    public class BALTimeTableMaster
    {
        public TimeTableMaster setRowIntoObjForAdmin(TimeTableMaster element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr[0].ToString());
            element.ExamMasterId = Convert.ToInt32(dr[1].ToString());
            element.ProgramInstancePartTermId = Convert.ToInt32(dr[2].ToString());
            element.PaperId = Convert.ToInt32(dr[3].ToString());
            element.AssetmentMethodId = Convert.ToInt32(dr[4].ToString());
            element.AssetmentType = dr[5].ToString();
            if (!string.IsNullOrEmpty(dr[6].ToString()))
            {
                element.ExamDate = Convert.ToDateTime(dr[6].ToString()).ToShortDateString();
                // element.LastDateOfFeesPaymentForStudent = Convert.ToDateTime(dr[5].ToString()).Date;
            }
            element.ExamSlotId = Convert.ToInt32(dr[7].ToString());
            element.IsConfirmedByFaculty = Convert.ToBoolean(dr[8].ToString());
            element.IsPublishedByExamSection = Convert.ToBoolean(dr[9].ToString());
            element.ReasonForUnPublish = dr[10].ToString();
            element.IsActive = Convert.ToBoolean(dr[11].ToString());
            element.IsDeleted = Convert.ToBoolean(dr[12].ToString());
            if (!string.IsNullOrEmpty(dr[13].ToString()))
            {
                element.Sequence = Convert.ToInt32(dr[13].ToString());
            }
            return element;

        }

        public TimeTableMaster setRowIntoObjForUser(TimeTableMaster element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr[0].ToString());
            element.ExamMasterId = Convert.ToInt32(dr[1].ToString());
            element.ProgramInstancePartTermId = Convert.ToInt32(dr[2].ToString());
            element.PaperId = Convert.ToInt32(dr[3].ToString());
            element.AssetmentMethodId = Convert.ToInt32(dr[4].ToString());
            element.AssetmentType = dr[5].ToString();
            if (!string.IsNullOrEmpty(dr[6].ToString()))
            {
                element.ExamDate = Convert.ToDateTime(dr[6].ToString()).ToShortDateString();
                // element.LastDateOfFeesPaymentForStudent = Convert.ToDateTime(dr[5].ToString()).Date;
            }
            element.ExamSlotId = Convert.ToInt32(dr[7].ToString());
            element.IsConfirmedByFaculty = Convert.ToBoolean(dr[8].ToString());
            element.IsPublishedByExamSection = Convert.ToBoolean(dr[9].ToString());
            element.ReasonForUnPublish = dr[10].ToString();
            if (!string.IsNullOrEmpty(dr[11].ToString()))
            {
                element.Sequence = Convert.ToInt32(dr[11].ToString());
            }
            return element;
        }

        public TimeTableMaster setRowIntoObjForAll(TimeTableMaster element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr[0].ToString());
            element.ExamMasterId = Convert.ToInt32(dr[1].ToString());
            element.ProgramInstancePartTermId = Convert.ToInt32(dr[2].ToString());
            element.PaperId = Convert.ToInt32(dr[3].ToString());
            element.AssetmentMethodId = Convert.ToInt32(dr[4].ToString());
            element.AssetmentType = dr[5].ToString();
            if (!string.IsNullOrEmpty(dr[6].ToString()))
            {
                element.ExamDate = Convert.ToDateTime(dr[6].ToString()).ToShortDateString();
                // element.LastDateOfFeesPaymentForStudent = Convert.ToDateTime(dr[5].ToString()).Date;
            }
            element.ExamSlotId = Convert.ToInt32(dr[7].ToString());
            element.IsConfirmedByFaculty = Convert.ToBoolean(dr[8].ToString());
            element.IsPublishedByExamSection = Convert.ToBoolean(dr[9].ToString());
            element.ReasonForUnPublish = dr[10].ToString();
            element.IsActive = Convert.ToBoolean(dr[11].ToString());
            element.CreatedOn = Convert.ToDateTime(dr[12].ToString());
            element.CreatedBy = Convert.ToInt32(dr[13].ToString());
            if (!string.IsNullOrEmpty(dr[14].ToString()))
                element.ModifiedOn = Convert.ToDateTime(dr[14].ToString());
            if (!string.IsNullOrEmpty(dr[15].ToString()))
                element.ModifiedBy = Convert.ToInt32(dr[15].ToString());
            element.IsDeleted = Convert.ToBoolean(dr[16].ToString());
            if (!string.IsNullOrEmpty(dr[17].ToString()))
            {
                element.Sequence = Convert.ToInt32(dr[17].ToString());
            }
            return element;
        }


        public List<TimeTableMaster> getTimeTableMasterList(string flag, int? IsDeleted, int? IsActive, int? PaperId, int? ExamMasterId, int? AssetmentMethodId, int? ExamSlotId, int? ProgramInstancePartTermId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("TimeTableMasterListGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Flag", flag);
                Cmd.Parameters.AddWithValue("@IsActive", IsActive);
                Cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
                Cmd.Parameters.AddWithValue("@PaperId", PaperId);
                Cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                Cmd.Parameters.AddWithValue("@AssetmentMethodId", AssetmentMethodId);
                Cmd.Parameters.AddWithValue("@ExamSlotId", ExamSlotId);
                Cmd.Parameters.AddWithValue("@ProgramInstancePartTermId", ProgramInstancePartTermId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<TimeTableMaster> ObjListTimeTableMaster = new List<TimeTableMaster>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        TimeTableMaster objTimeTableMaster = new TimeTableMaster();
                        if (flag == "admin")
                        {
                            setRowIntoObjForAdmin(objTimeTableMaster, Dt.Rows[i]);
                        }
                        else if (flag == "user")
                        {
                            setRowIntoObjForUser(objTimeTableMaster, Dt.Rows[i]);
                        }

                        ObjListTimeTableMaster.Add(objTimeTableMaster);
                    }
                }
                return ObjListTimeTableMaster;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<TimeTableMaster> getPendingTimeTableList(int? ProgrammePartTermId, int? ExamMasterId, Int32 BranchId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("TimeTableMasterGetPending", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                Cmd.Parameters.AddWithValue("@ProgramPartTermId", ProgrammePartTermId);
                Cmd.Parameters.AddWithValue("@BranchId", BranchId);

                Da.SelectCommand = Cmd;

                Da.Fill(dt);
                if (dt == null || dt.Rows.Count == 0)
                {
                    return null;
                }
                int n = 0, count = dt.Rows.Count;
                List<TimeTableMaster> timetableList = new List<TimeTableMaster>();
                while (n < count)
                {
                    TimeTableMaster element = new TimeTableMaster();
                    //if (!string.IsNullOrEmpty(dt.Rows[n][0].ToString()))
                    //    element.Id = Convert.ToInt32(dt.Rows[n][0].ToString());
                    //element.ProgramInstancePartTermId = Convert.ToInt32(dt.Rows[n][1].ToString());
                    element.PaperId = Convert.ToInt32(dt.Rows[n]["PaperId"].ToString());
                    element.PaperName = dt.Rows[n]["PaperName"].ToString();
                    element.TeachingLearningMethodId = Convert.ToInt32(dt.Rows[n]["TeachingLearningMethodId"].ToString());
                    element.TeachingLearningMethod = dt.Rows[n]["TeachingLearningMethodName"].ToString();
                    element.AssetmentMethodId = Convert.ToInt32(dt.Rows[n]["AssessmentMethodId"].ToString());
                    element.AssetsmentMethodName = dt.Rows[n]["AssessmentMethodName"].ToString();
                    element.AssetmentType = dt.Rows[n]["AssessmentType"].ToString();
                    element.IsConfirm = (Convert.ToString(dt.Rows[n]["IsConfirm"])).IsEmpty() ? false : Convert.ToBoolean(dt.Rows[n]["IsConfirm"]);
                    element.PartTermName = (Convert.ToString(dt.Rows[n]["PartTermShortName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dt.Rows[n]["PartTermShortName"]);
                    element.ProgrammePartTermId = (Convert.ToString(dt.Rows[n]["ProgrammePartTermId"])).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[n]["ProgrammePartTermId"]);
                    element.Branch = (Convert.ToString(dt.Rows[n]["BranchName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dt.Rows[n]["BranchName"]);
                    element.BranchId = (Convert.ToString(dt.Rows[n]["BranchId"])).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[n]["BranchId"]);
                    if (!string.IsNullOrEmpty(dt.Rows[n]["Id"].ToString()))
                    element.Id = Convert.ToInt32(dt.Rows[n]["Id"].ToString());
                    string date = dt.Rows[n]["ExamDate"].ToString();
                    string[] datelist = date.Split(' ');

                    element.ExamDate = datelist[0];
                    if (!string.IsNullOrEmpty(dt.Rows[n]["ExamSlotId"].ToString()))
                    {
                        element.ExamSlotId = Convert.ToInt32(dt.Rows[n]["ExamSlotId"].ToString());
                      
                    }
                    element.ExamSlotName = dt.Rows[n]["SlotName"].ToString();
                    element.ExamMasterId = Convert.ToInt32(ExamMasterId);
                    if (!string.IsNullOrEmpty(dt.Rows[n]["Sequence"].ToString()))
                    {
                        element.Sequence = Convert.ToInt32(dt.Rows[n]["Sequence"].ToString());
                    }
                    timetableList.Add(element);
                    n++;
                }
                return timetableList;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}