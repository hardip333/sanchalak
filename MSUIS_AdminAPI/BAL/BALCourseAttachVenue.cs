using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MSUISApi.BAL
{
    public class BALCourseAttachVenue
    {
        public CourseAttachVenue setRowIntoObjForAdmin(CourseAttachVenue element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr[0].ToString());
            if (!string.IsNullOrEmpty(dr[1].ToString()))
            {
                element.ExamMasterId = Convert.ToInt32(dr[1].ToString());
                
            }
            element.ExamMaster = dr[9].ToString();
            if (!string.IsNullOrEmpty(dr[2].ToString()))
            {
                element.FacultyExamMapId = Convert.ToInt32(dr[2].ToString());
                
            }
            element.FacultyExamMap = dr[10].ToString();
            if (!string.IsNullOrEmpty(dr[3].ToString()))
            {
                element.CourseScheduleMapId = Convert.ToInt32(dr[3].ToString());
            }
            if (!string.IsNullOrEmpty(dr[4].ToString()))
            {
                element.ExamVenueId = Convert.ToInt32(dr[4].ToString());
            }
            element.ExamVenue = dr[11].ToString();
            element.IsActive = Convert.ToBoolean(dr[5].ToString());
            element.IsDeleted = Convert.ToBoolean(dr[6].ToString());
            if (!string.IsNullOrEmpty(dr[7].ToString()))
                element.Capacity = Convert.ToInt32(dr[7].ToString());
            if (!string.IsNullOrEmpty(dr[8].ToString()))
                element.Sequence = Convert.ToInt32(dr[8].ToString());
            return element;

        }

        public CourseAttachVenue setRowIntoObjForUser(CourseAttachVenue element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr[0].ToString());
            if (!string.IsNullOrEmpty(dr[1].ToString()))
                element.ExamMasterId = Convert.ToInt32(dr[1].ToString());
            if (!string.IsNullOrEmpty(dr[2].ToString()))
                element.FacultyExamMapId = Convert.ToInt32(dr[2].ToString());
            if (!string.IsNullOrEmpty(dr[3].ToString()))
                element.CourseScheduleMapId = Convert.ToInt32(dr[3].ToString());
            if (!string.IsNullOrEmpty(dr[4].ToString()))
                element.ExamVenueId = Convert.ToInt32(dr[4].ToString());
            if (!string.IsNullOrEmpty(dr[5].ToString()))
                element.Capacity = Convert.ToInt32(dr[5].ToString());
            if (!string.IsNullOrEmpty(dr[6].ToString()))
                element.Sequence = Convert.ToInt32(dr[6].ToString());
            element.ExamMaster = dr[7].ToString();
            element.FacultyExamMap = dr[8].ToString();
            element.ExamVenue = dr[9].ToString();
            return element;
        }

        public CourseAttachVenue setRowIntoObjForAll(CourseAttachVenue element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr[0].ToString());
            if (!string.IsNullOrEmpty(dr[1].ToString()))
                element.ExamMasterId = Convert.ToInt32(dr[1].ToString());
            if (!string.IsNullOrEmpty(dr[2].ToString()))
                element.FacultyExamMapId = Convert.ToInt32(dr[2].ToString());
            if (!string.IsNullOrEmpty(dr[3].ToString()))
                element.CourseScheduleMapId = Convert.ToInt32(dr[3].ToString());
            if (!string.IsNullOrEmpty(dr[4].ToString()))
                element.ExamVenueId = Convert.ToInt32(dr[4].ToString());
            element.IsActive = Convert.ToBoolean(dr[2].ToString());
            element.CreatedOn = dr[3].ToString();
            element.CreatedBy = Convert.ToInt32(dr[4].ToString());
            element.ModifiedOn = dr[5].ToString();
            if (!string.IsNullOrEmpty(dr[6].ToString()))
                element.ModifiedBy = Convert.ToInt32(dr[6].ToString());
            element.IsDeleted = Convert.ToBoolean(dr[7].ToString());
            if (!string.IsNullOrEmpty(dr[8].ToString()))
                element.Capacity = Convert.ToInt32(dr[8].ToString());
            if (!string.IsNullOrEmpty(dr[9].ToString()))
                element.Sequence = Convert.ToInt32(dr[9].ToString());
            return element;
        }


        public List<CourseAttachVenue> getCourseAttachVenueList(string flag, int? IsDeleted, int? IsActive, int? ExamMasterId, int? FacultyExamMapId, int? CourseScheduleMapId, int? ExamVenueId, int? ExamCenterId, int? ProgrammePartTermId, Int64 UserId)
        {
            try 
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("CourseAttachVenueListGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Flag", flag);
                Cmd.Parameters.AddWithValue("@IsActive", IsActive);
                Cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
                Cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                Cmd.Parameters.AddWithValue("@FacultyExamMapId", FacultyExamMapId);
                Cmd.Parameters.AddWithValue("@CourseScheduleMapId", CourseScheduleMapId);
                Cmd.Parameters.AddWithValue("@ProgrammePartTermId", ProgrammePartTermId);
                Cmd.Parameters.AddWithValue("@ExamVenueId", ExamVenueId);
                Cmd.Parameters.AddWithValue("@ExamCenterId", ExamCenterId);
                Cmd.Parameters.AddWithValue("@UserId", UserId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<CourseAttachVenue> ObjListCourseAttachVenue = new List<CourseAttachVenue>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        CourseAttachVenue objCourseAttachVenue = new CourseAttachVenue();
                        if (flag == "admin")
                        {
                            setRowIntoObjForAdmin(objCourseAttachVenue, Dt.Rows[i]);
                        }
                        else if (flag == "user")
                        {
                            setRowIntoObjForUser(objCourseAttachVenue, Dt.Rows[i]);
                        }

                        ObjListCourseAttachVenue.Add(objCourseAttachVenue);
                    }
                }
                return ObjListCourseAttachVenue;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public List<CourseAttachVenue> getPendingCourseVenueMapList(int? CourseScheduleMapId, int? ExamCenterId, int? FacultyExamMapId, int? ExamMasterId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("getPendingCourseAttachVenue", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@CourseScheduleMapId", CourseScheduleMapId);
                Cmd.Parameters.AddWithValue("@ExamCenterId", ExamCenterId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);
                List<CourseAttachVenue> CourseAttachVenueList = new List<CourseAttachVenue>();
                if (Dt == null || Dt.Rows.Count == 0)
                {
                    return CourseAttachVenueList;
                }

                int n = 0, count = Dt.Rows.Count;

                while (n < count)
                {
                    CourseAttachVenue element = new CourseAttachVenue();
                    element.ExamVenueId = Convert.ToInt32(Dt.Rows[n][0].ToString());
                    element.ExamCenterId = Convert.ToInt32(Dt.Rows[n][1].ToString());
                    element.ExamVenue = Dt.Rows[n][3].ToString();
                    element.ExamMasterId = ExamMasterId;
                    element.FacultyExamMapId = FacultyExamMapId;
                    element.CourseScheduleMapId = CourseScheduleMapId;

                    //if (!String.IsNullOrEmpty(Dt.Rows[n][5].ToString()))
                    //    element.Capacity = Convert.ToInt32(Dt.Rows[0][5].ToString());
                    //if (!String.IsNullOrEmpty(Dt.Rows[n][6].ToString()))
                    //    element.Sequence = Convert.ToInt32(Dt.Rows[0][6].ToString());
                    CourseAttachVenueList.Add(element);
                    n++;
                }
                return CourseAttachVenueList;
            }
            catch(Exception e)
            {
                return null;
            }
        }
    }
}