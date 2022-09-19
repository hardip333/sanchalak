using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MSUISApi.BAL
{
    public class BALFacultyExamMapStatus
    {
        public FacultyExamMap setRowIntoObjForAdmin(FacultyExamMap element, DataRow dr, string methodNm)
        {
            element.Id = Convert.ToInt32(dr[0].ToString());
            element.ScheduleCode = dr[1].ToString();
            if (!string.IsNullOrEmpty(dr[2].ToString()))
                element.ExamMasterId = Convert.ToInt32(dr[2].ToString());
            if (!string.IsNullOrEmpty(dr[3].ToString()))
            {
                element.FacultyId = Convert.ToInt32(dr[3].ToString());
            }
            element.FacultyName = dr[14].ToString();
            if (!string.IsNullOrEmpty(dr[4].ToString()))
                element.InstituteId = Convert.ToInt32(dr[4].ToString());
            if (!string.IsNullOrEmpty(dr[5].ToString()))
            {
                element.LastDateOfFeesPaymentForStudent = Convert.ToDateTime(dr[5].ToString()).ToShortDateString();
            }
            if (!string.IsNullOrEmpty(dr[6].ToString()))
                element.LastDateOfFeesPaymentForCollege = Convert.ToDateTime(dr[6].ToString()).ToShortDateString();
            string date = dr[7].ToString();
            string[] datelist = date.Split(' ');
            element.StartDateOfExam = datelist[0];
            string date1 = dr[8].ToString();
            string[] datelist1 = date1.Split(' ');
            element.EndDateOfExam = datelist1[0];
            if (!string.IsNullOrEmpty(dr[9].ToString()))
                element.ProbableDateOfResult = Convert.ToDateTime(dr[9].ToString()).ToShortDateString();
            if (!string.IsNullOrEmpty(dr[10].ToString()))
                element.IsActive = Convert.ToBoolean(dr[10].ToString());
            if (!string.IsNullOrEmpty(dr[11].ToString()))
                element.IsDeleted = Convert.ToBoolean(dr[11].ToString());
            if (!string.IsNullOrEmpty(dr[12].ToString()))
                element.IsConfirmed = Convert.ToBoolean(dr[12].ToString());
            element.DisplayName = dr[13].ToString();



            return element;

        }
        public FacultyExamMap getFacultyExamMapStatusDetails(int? id)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            SqlDataAdapter Da = new SqlDataAdapter();
            DataTable Dt = new DataTable();
            SqlCommand Cmd = new SqlCommand("FacultyExamMapGetStatusbyId", Con);
            Cmd.CommandType = CommandType.StoredProcedure;

            Cmd.Parameters.AddWithValue("@Id", id);
            Da.SelectCommand = Cmd;

            Da.Fill(Dt);

            FacultyExamMap element = new FacultyExamMap();

            if (Dt.Rows.Count > 0)
            {
                element = setRowIntoObjForAdmin(element, Dt.Rows[0], null);
                Cmd = new SqlCommand("FacultyExamMapStatusGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Flag", "AttachedCoursesList");
                Cmd.Parameters.AddWithValue("@Id", id);
                Da.SelectCommand = Cmd;
                DataTable Dt1 = new DataTable();
                Da.Fill(Dt1);

                if (Dt1.Rows.Count > 0)
                {
                    element.attachedCourses = new List<CourseScheduleMap>();
                    for (int i = 0; i < Dt1.Rows.Count; i++)
                    {
                        CourseScheduleMap ele = new CourseScheduleMap();
                        ele.Id = Convert.ToInt32(Dt1.Rows[i][0].ToString());
                        ele.ProgrammeName = Dt1.Rows[i][1].ToString();
                        ele.PartName = Dt1.Rows[i][2].ToString();
                        ele.ProgrammePartTermName = Dt1.Rows[i][3].ToString();
                        ele.AttchedVenueCount = Convert.ToInt32(Dt1.Rows[i][4].ToString());
                        ele.VenueStatus = Dt1.Rows[i][5].ToString();
                        ele.IsConfirmed = Convert.ToBoolean(Dt1.Rows[i][6].ToString());
                        ele.IsPublished = Convert.ToBoolean(Dt1.Rows[i][7].ToString());
                        element.attachedCourses.Add(ele);
                    }
                }
                // BALCourseScheduleMap func = new BALCourseScheduleMap();
                //element.attachedCourses = func.getCourseScheduleMapList("admin", 0, 1, Convert.ToInt32(Dt.Rows[0][0].ToString()), null);

            }
            return element;
        }

        public List<FacultyExamMap> getFacultyExamMapStatus(int? ExamMasterId, int? IsDeleted, int? IsActive)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("FacultyExamMapStatusGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Flag", "FacultyExamMapStatusGet");
                Cmd.Parameters.AddWithValue("@IsActive", IsActive);
                Cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
                Cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                //Cmd.Parameters.AddWithValue("@FacultyId", null);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<FacultyExamMap> ObjListFacultyExamMap = new List<FacultyExamMap>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        FacultyExamMap element = new FacultyExamMap();
                        element.Id = Convert.ToInt32(Dt.Rows[i][0].ToString());
                        element.ScheduleCode = Dt.Rows[i][1].ToString();
                        string date = Dt.Rows[i][2].ToString();
                        string[] datelist = date.Split(' ');
                        element.StartDateOfExam = datelist[0];
                        //element.StartDateOfExam = Dt.Rows[i][2].ToString();
                        string date1 = Dt.Rows[i][3].ToString();
                        string[] datelist1 = date1.Split(' ');
                        element.EndDateOfExam = datelist1[0];
                        element.AttachCourseCount = Convert.ToInt32(Dt.Rows[i][4].ToString());
                        element.AttchedVenueCount = Convert.ToInt32(Dt.Rows[i][5].ToString());
                        element.VenueStatus = Dt.Rows[i][6].ToString();
                        element.IsConfirmed = Convert.ToBoolean(Dt.Rows[i][7].ToString());

                        ObjListFacultyExamMap.Add(element);
                    }
                }
                return ObjListFacultyExamMap;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}