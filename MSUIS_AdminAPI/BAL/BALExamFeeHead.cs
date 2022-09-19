using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MSUISApi.BAL
{
    public class BALExamFeeHead
    {
        public ExamFeeHead setRowIntoObjForAdmin(ExamFeeHead element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr[0].ToString());
            element.ExamFeeHeadName = dr[1].ToString();
            element.ExamFeeHeadCode = dr[2].ToString();
            element.IsActive = Convert.ToBoolean(dr[3].ToString());
            if (!string.IsNullOrEmpty(dr[4].ToString()))
                element.IsDeleted = Convert.ToBoolean(dr[4].ToString());
            if (!string.IsNullOrEmpty(dr[5].ToString()))
                element.DayRange = Convert.ToInt32(dr[5].ToString());
            return element;

        }

        public ExamFeeHead setRowIntoObjForUser(ExamFeeHead element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr[0].ToString());
            element.ExamFeeHeadName = dr[1].ToString();
            element.ExamFeeHeadCode = dr[2].ToString();
            if (!string.IsNullOrEmpty(dr[3].ToString()))
                element.DayRange = Convert.ToInt32(dr[3].ToString());
            return element;
        }

        public ExamFeeHead setRowIntoObjForAll(ExamFeeHead element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr[0].ToString());
            element.ExamFeeHeadName = dr[1].ToString();
            element.ExamFeeHeadCode = dr[2].ToString();
            element.IsActive = Convert.ToBoolean(dr[3].ToString());
            element.IsDeleted = Convert.ToBoolean(dr[4].ToString());
            element.CreatedBy = Convert.ToInt32(dr[5].ToString());
            element.CreatedOn = Convert.ToDateTime(dr[6].ToString());
            if (!string.IsNullOrEmpty(dr[7].ToString()))
                element.ModifiedBy = Convert.ToInt32(dr[7].ToString());
            if (!string.IsNullOrEmpty(dr[8].ToString()))
                element.ModifiedOn = Convert.ToDateTime(dr[8].ToString());
            if (!string.IsNullOrEmpty(dr[9].ToString()))
                element.DayRange = Convert.ToInt32(dr[9].ToString());


            return element;
        }


        public List<ExamFeeHead> getExamFeeHeadList(string flag, int? IsDeleted, int? IsActive)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("ExamFeeHeadListGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Flag", flag);
                Cmd.Parameters.AddWithValue("@IsActive", IsActive);
                Cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<ExamFeeHead> ObjListExamFeeHead = new List<ExamFeeHead>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ExamFeeHead objExamFeeHead = new ExamFeeHead();
                        if (flag == "admin")
                        {
                            setRowIntoObjForAdmin(objExamFeeHead, Dt.Rows[i]);
                        }
                        else if (flag == "user")
                        {
                            setRowIntoObjForUser(objExamFeeHead, Dt.Rows[i]);
                        }

                        ObjListExamFeeHead.Add(objExamFeeHead);
                    }
                }
                return ObjListExamFeeHead;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<ExamFeeHead> getExamFeeHeadListForFacultyExamMap(string strStartDate, string strLastDateForFeesPaymentForStudent, string strLastDateForFeesPaymentForCollege)
        {

            try
            {

                string strminDate = null;
                DateTime date1 = Convert.ToDateTime(strLastDateForFeesPaymentForStudent);
                DateTime date2 = Convert.ToDateTime(strLastDateForFeesPaymentForCollege);

                int result = DateTime.Compare(date1, date2);
                if (result < 0)
                {
                    strminDate = strLastDateForFeesPaymentForStudent;
                }
                else
                {
                    strminDate = strLastDateForFeesPaymentForCollege;
                }

                DateTime StartDate = Convert.ToDateTime(strStartDate);
                DateTime MinDate = Convert.ToDateTime(strminDate);
                var numberOfDays = (StartDate - MinDate).TotalDays;

                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("getExamFeeHeadListForFacultyExamMap", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@NumberOfDays", numberOfDays);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);
                List<ExamFeeHead> examFeeHeads = new List<ExamFeeHead>();

                if (Dt == null || Dt.Rows.Count == 0)
                {
                    return null;
                }
                int n = 0, count = Dt.Rows.Count;
                while (n < count)
                {
                    ExamFeeHead element = new ExamFeeHead();
                    element.Id = Convert.ToInt32(Dt.Rows[n][0].ToString());
                    element.ExamFeeHeadName = Dt.Rows[n][1].ToString();
                    element.DayRange = Convert.ToInt32(Dt.Rows[n][2].ToString());
                    examFeeHeads.Add(element);
                    n++;
                }
                return examFeeHeads;
            }
            catch (Exception e)
            {
                return null;
            }
        }


    }
}