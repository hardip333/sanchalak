using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MSUISApi.BAL
{
    public class BALCourseScheduleMap
    {

        public CourseScheduleMap setRowIntoObjForAdmin(CourseScheduleMap element, DataRow dr)
        {
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime date1 = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(element.ExamFeeStartDate).ToUniversalTime(), INDIAN_ZONE);
            DateTime date2 = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(element.ExamFeeEndDateForStudent).ToUniversalTime(), INDIAN_ZONE);
            DateTime date3 = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(element.ExamFeeEndDateforFaculty).ToUniversalTime(), INDIAN_ZONE);
            element.Id = Convert.ToInt32(dr[0].ToString());
            if (!string.IsNullOrEmpty(dr[1].ToString()))
            {
                element.ProgrammePartTermId = Convert.ToInt32(dr[1].ToString());

            }
            if (!string.IsNullOrEmpty(dr[2].ToString()))
                element.FacultyExamMapId = Convert.ToInt32(dr[2].ToString());
            element.FresherEligible = Convert.ToBoolean(dr[3].ToString());
            element.FresherProvisionalEligible = Convert.ToBoolean(dr[4].ToString());
            element.RepeaterEligible = Convert.ToBoolean(dr[5].ToString());
            element.RepeaterProvisionalEligible = Convert.ToBoolean(dr[6].ToString());
            element.NeverAppearedEligible = Convert.ToBoolean(dr[7].ToString());
            element.NeverAppearedProvisionalEligible = Convert.ToBoolean(dr[8].ToString());
            element.MaxTLMAMATCount = Convert.ToInt16(dr[9].ToString());
            element.IsScheduledWithOutExamForm = Convert.ToBoolean(dr[10].ToString());
            element.IsActive = Convert.ToBoolean(dr[11].ToString());
            if (!string.IsNullOrEmpty(dr[12].ToString()))
                element.IsConfirmed = Convert.ToBoolean(dr[12].ToString());
            if (!string.IsNullOrEmpty(dr[13].ToString()))
                element.IsPublished = Convert.ToBoolean(dr[13].ToString());
            if (!string.IsNullOrEmpty(dr[14].ToString()))
                element.DeamedInward = Convert.ToBoolean(dr[14].ToString());
            element.ProgrammePartTermName = dr[15].ToString();
            element.ProgrammePartId = Convert.ToInt32(dr[16].ToString());
            element.PartName = dr[17].ToString();
            element.ProgrammeId = Convert.ToInt32(dr[18].ToString());
            element.ProgrammeName = dr[19].ToString();

            var ExamFeeStartDate = dr[20];
            if (ExamFeeStartDate is DBNull)
            {
                element.ExamFeeStartDate = null;
                element.ExamFeeStartDateView = "-";
            }
            else
            {
                element.ExamFeeStartDate = Convert.ToDateTime(dr[20].ToString());
                element.ExamFeeStartDateView = string.Format("{0: dd-MM-yyyy}", (dr[20]));

            }

            var ExamFeeEndDateForStudent = dr[21];
            if (ExamFeeEndDateForStudent is DBNull)
            {
                element.ExamFeeEndDateForStudent = null;
                element.ExamFeeEndDateForStudentView = "-";
            }
            else
            {
                element.ExamFeeEndDateForStudent = Convert.ToDateTime(dr[21].ToString());
                element.ExamFeeEndDateForStudentView = string.Format("{0: dd-MM-yyyy}", (dr[21]));
            }
            var ExamFeeEndDateforFaculty = dr[22];
            if (ExamFeeEndDateforFaculty is DBNull)
            {
                element.ExamFeeEndDateforFaculty = null;
                element.ExamFeeEndDateforFacultyView = "-";
            }
            else
            {
                element.ExamFeeEndDateforFaculty = Convert.ToDateTime(dr[22].ToString());
                element.ExamFeeEndDateforFacultyView = string.Format("{0: dd-MM-yyyy}", (dr[22]));
            }

            return element;

        }

        public CourseScheduleMap setRowIntoObjForUser(CourseScheduleMap element, DataRow dr)
        {
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime date1 = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(element.ExamFeeStartDate).ToUniversalTime(), INDIAN_ZONE);
            DateTime date2 = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(element.ExamFeeEndDateForStudent).ToUniversalTime(), INDIAN_ZONE);
            DateTime date3 = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(element.ExamFeeEndDateforFaculty).ToUniversalTime(), INDIAN_ZONE);

            element.Id = Convert.ToInt32(dr[0].ToString());
            element.ProgrammePartTermId = Convert.ToInt32(dr[1].ToString());
            if (!string.IsNullOrEmpty(dr[2].ToString()))
                element.FacultyExamMapId = Convert.ToInt32(dr[2].ToString());
            element.FresherEligible = Convert.ToBoolean(dr[3].ToString());
            element.FresherProvisionalEligible = Convert.ToBoolean(dr[4].ToString());
            element.RepeaterEligible = Convert.ToBoolean(dr[5].ToString());
            element.RepeaterProvisionalEligible = Convert.ToBoolean(dr[6].ToString());
            element.NeverAppearedEligible = Convert.ToBoolean(dr[7].ToString());
            element.NeverAppearedProvisionalEligible = Convert.ToBoolean(dr[8].ToString());
            element.MaxTLMAMATCount = Convert.ToInt16(dr[9].ToString());
            element.IsScheduledWithOutExamForm = Convert.ToBoolean(dr[10].ToString());
            element.IsConfirmed = Convert.ToBoolean(dr[11].ToString());
            element.IsPublished = Convert.ToBoolean(dr[12].ToString());
            if (!string.IsNullOrEmpty(dr[13].ToString()))
                element.DeamedInward = Convert.ToBoolean(dr[13].ToString());
            element.ProgrammePartTermName = dr[14].ToString();
            element.ProgrammePartId = Convert.ToInt32(dr[15].ToString());
            element.PartName = dr[16].ToString();
            element.ProgrammeId = Convert.ToInt32(dr[17].ToString());
            element.ProgrammeName = dr[18].ToString();

            var ExamFeeStartDate = dr[19];
            if (ExamFeeStartDate is DBNull)
            {
                element.ExamFeeStartDate = null;
                element.ExamFeeStartDateView = "-";

            }
            else
            {
                element.ExamFeeStartDate = Convert.ToDateTime(dr[19]);
                element.ExamFeeStartDateView = string.Format("{0: dd-MM-yyyy}", (dr[19]));
            }

            var ExamFeeEndDateForStudent = dr[20];
            if (ExamFeeEndDateForStudent is DBNull)
            {
                element.ExamFeeEndDateForStudent = null;
                element.ExamFeeEndDateForStudentView = "-";

            }
            else
            {
                element.ExamFeeEndDateForStudent = Convert.ToDateTime(dr[20]);
                element.ExamFeeEndDateForStudentView = string.Format("{0: dd-MM-yyyy}", (dr[20]));
            }
            var ExamFeeEndDateforFaculty = dr[21];
            if (ExamFeeEndDateforFaculty is DBNull)
            {
                element.ExamFeeEndDateforFaculty = null;
                element.ExamFeeEndDateforFacultyView = "-";

            }
            else
            {
                element.ExamFeeEndDateforFaculty = Convert.ToDateTime(dr[21]);
                element.ExamFeeEndDateforFacultyView = string.Format("{0: dd-MM-yyyy}", (dr[21]));
            }
            return element;
        }

        public CourseScheduleMap setRowIntoObjForAll(CourseScheduleMap element, DataRow dr)
        {
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime date1 = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(element.ExamFeeStartDate).ToUniversalTime(), INDIAN_ZONE);
            DateTime date2 = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(element.ExamFeeEndDateForStudent).ToUniversalTime(), INDIAN_ZONE);
            DateTime date3 = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(element.ExamFeeEndDateforFaculty).ToUniversalTime(), INDIAN_ZONE);

            element.Id = Convert.ToInt32(dr[0].ToString());
            element.ProgrammePartTermId = Convert.ToInt32(dr[1].ToString());
            if (!string.IsNullOrEmpty(dr[2].ToString()))
                element.FacultyExamMapId = Convert.ToInt32(dr[2].ToString());
            element.FresherEligible = Convert.ToBoolean(dr[3].ToString());
            element.FresherProvisionalEligible = Convert.ToBoolean(dr[4].ToString());
            element.RepeaterEligible = Convert.ToBoolean(dr[5].ToString());
            element.RepeaterProvisionalEligible = Convert.ToBoolean(dr[6].ToString());
            element.NeverAppearedEligible = Convert.ToBoolean(dr[7].ToString());
            element.NeverAppearedProvisionalEligible = Convert.ToBoolean(dr[8].ToString());
            element.MaxTLMAMATCount = Convert.ToInt16(dr[9].ToString());
            element.IsScheduledWithOutExamForm = Convert.ToBoolean(dr[10].ToString());
            element.IsActive = Convert.ToBoolean(dr[11].ToString());
            element.CreatedOn = Convert.ToDateTime(dr[12].ToString());
            element.CreatedBy = Convert.ToInt32(dr[13].ToString());
            if (!string.IsNullOrEmpty(dr[14].ToString()))
                element.ModifiedOn = Convert.ToDateTime(dr[14].ToString());
            if (!string.IsNullOrEmpty(dr[15].ToString()))
                element.ModifiedBy = Convert.ToInt32(dr[15].ToString());
            element.IsDeleted = Convert.ToBoolean(dr[16].ToString());
            element.IsConfirmed = Convert.ToBoolean(dr[17].ToString());
            element.IsPublished = Convert.ToBoolean(dr[18].ToString());
            if (!string.IsNullOrEmpty(dr[19].ToString()))
                element.DeamedInward = Convert.ToBoolean(dr[19]);

            var ExamFeeStartDate = dr[20];
            if (ExamFeeStartDate is DBNull)
            {
                element.ExamFeeStartDate = null;
                element.ExamFeeStartDateView = "-";
            }
            else
            {
                element.ExamFeeStartDate = Convert.ToDateTime(dr[20]);
                element.ExamFeeStartDateView = string.Format("{0: dd-MM-yyyy}", (dr[20]));


            }

            var ExamFeeEndDateForStudent = dr[21];
            if (ExamFeeEndDateForStudent is DBNull)
            {
                element.ExamFeeEndDateForStudent = null;
                element.ExamFeeEndDateForStudentView = "-";
            }
            else
            {
                element.ExamFeeEndDateForStudent = Convert.ToDateTime(dr[21]);
                element.ExamFeeEndDateForStudentView = string.Format("{0: dd-MM-yyyy}", (dr[21]));
            }
            var ExamFeeEndDateforFaculty = dr[22];
            if (ExamFeeEndDateforFaculty is DBNull)
            {
                element.ExamFeeEndDateforFaculty = null;
                element.ExamFeeEndDateforFacultyView = "-";
            }
            else
            {
                element.ExamFeeEndDateforFaculty = Convert.ToDateTime(dr[22]);
                element.ExamFeeEndDateforFacultyView = string.Format("{0: dd-MM-yyyy}", (dr[22]));
            }
            return element;
        }

        public List<CourseScheduleMap> getCourseScheduleMapList(string flag, int? IsDeleted, int? IsActive, int? FacultyExamMapId, int? ProgrammePartTermId, int? FacultyId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("CourseScheduleMapListGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Flag", flag);
                Cmd.Parameters.AddWithValue("@IsActive", IsActive);
                Cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
                Cmd.Parameters.AddWithValue("@FacultyExamMapId", FacultyExamMapId);
                Cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                Cmd.Parameters.AddWithValue("@ProgrammePartTermId", ProgrammePartTermId);

                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<CourseScheduleMap> ObjListCourseScheduleMap = new List<CourseScheduleMap>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        CourseScheduleMap objCourseScheduleMap = new CourseScheduleMap();
                        if (flag == "admin")
                        {
                            setRowIntoObjForAdmin(objCourseScheduleMap, Dt.Rows[i]);
                        }
                        else if (flag == "user")
                        {
                            setRowIntoObjForUser(objCourseScheduleMap, Dt.Rows[i]);
                        }

                        ObjListCourseScheduleMap.Add(objCourseScheduleMap);
                    }
                }
                return ObjListCourseScheduleMap;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<CourseScheduleMap> getPendingCourseScheduleMapList(int ProgrammeId, int? ExamMasterId, int FacultyExamMapId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("PendingCourseScheduleMapList", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                Cmd.Parameters.AddWithValue("@ProgrammeId", ProgrammeId);
                Da.SelectCommand = Cmd;

                Da.Fill(dt);
                List<CourseScheduleMap> courseScheduleMapList = new List<CourseScheduleMap>();
                if (dt == null || dt.Rows.Count == 0)
                {
                    return null;
                }
                int n = 0, count = dt.Rows.Count;
                while (n < count)
                {
                    CourseScheduleMap element = new CourseScheduleMap();
                    element.FacultyExamMapId = FacultyExamMapId;
                    element.ProgrammePartTermId = Convert.ToInt32(dt.Rows[n][0].ToString());
                    element.ProgrammeId = ProgrammeId;
                    element.ProgrammeName = dt.Rows[n][3].ToString();
                    element.ProgrammePartId = Convert.ToInt32(dt.Rows[n][1].ToString());
                    element.PartName = dt.Rows[n][4].ToString();
                    element.ProgrammePartTermName = dt.Rows[n][2].ToString();
                    courseScheduleMapList.Add(element);
                    n++;
                }
                return courseScheduleMapList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}