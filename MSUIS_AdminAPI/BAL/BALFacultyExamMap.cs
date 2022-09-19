using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http.Results;
using System.Web.WebPages;

namespace MSUISApi.BAL
{
    public class BALFacultyExamMap
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
            element.InstituteName = dr[15].ToString();
            if (!string.IsNullOrEmpty(dr[5].ToString()))
            {
                element.LastDateOfFeesPaymentForStudent = Convert.ToDateTime(dr[5].ToString()).ToShortDateString();
                // element.LastDateOfFeesPaymentForStudent = Convert.ToDateTime(dr[5].ToString()).Date;
            }
            if (!string.IsNullOrEmpty(dr[6].ToString()))
                element.LastDateOfFeesPaymentForCollege = Convert.ToDateTime(dr[6].ToString()).ToShortDateString();
            if (!string.IsNullOrEmpty(dr[7].ToString()))
                element.StartDateOfExam = Convert.ToDateTime(dr[7].ToString()).ToShortDateString();
            if (!string.IsNullOrEmpty(dr[8].ToString()))
                element.EndDateOfExam = Convert.ToDateTime(dr[8].ToString()).ToShortDateString();
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

        public FacultyExamMap setRowIntoObjForUser(FacultyExamMap element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr[0].ToString());
            element.ScheduleCode = dr[1].ToString();
            if (!string.IsNullOrEmpty(dr[2].ToString()))
                element.ExamMasterId = Convert.ToInt32(dr[2].ToString());
            if (!string.IsNullOrEmpty(dr[3].ToString()))
            {
                element.FacultyId = Convert.ToInt32(dr[3].ToString());
            }
            element.FacultyName = dr[12].ToString();
            if (!string.IsNullOrEmpty(dr[4].ToString()))
                element.InstituteId = Convert.ToInt32(dr[4].ToString());
            element.InstituteName = dr[13].ToString();
            if (!string.IsNullOrEmpty(dr[5].ToString()))
                element.LastDateOfFeesPaymentForStudent = Convert.ToDateTime(dr[5].ToString()).ToShortDateString();
            if (!string.IsNullOrEmpty(dr[6].ToString()))
                element.LastDateOfFeesPaymentForCollege = Convert.ToDateTime(dr[6].ToString()).ToShortDateString();
            if (!string.IsNullOrEmpty(dr[7].ToString()))
                element.StartDateOfExam = Convert.ToDateTime(dr[7].ToString()).ToShortDateString();
            if (!string.IsNullOrEmpty(dr[8].ToString()))
                element.EndDateOfExam = Convert.ToDateTime(dr[8].ToString()).ToShortDateString();
            if (!string.IsNullOrEmpty(dr[9].ToString()))
                element.ProbableDateOfResult = Convert.ToDateTime(dr[9].ToString()).ToShortDateString();
            if (!string.IsNullOrEmpty(dr[10].ToString()))
                element.IsConfirmed = Convert.ToBoolean(dr[10].ToString());
            element.DisplayName = dr[11].ToString();
            return element;
        }

        public FacultyExamMap setRowIntoObjForAll(FacultyExamMap element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr[0].ToString());
            element.ScheduleCode = dr[1].ToString();
            if (!string.IsNullOrEmpty(dr[2].ToString()))
                element.ExamMasterId = Convert.ToInt32(dr[2].ToString());
            if (!string.IsNullOrEmpty(dr[3].ToString()))
            {
                element.FacultyId = Convert.ToInt32(dr[3].ToString());
            }
            element.FacultyName = dr[18].ToString();
            if (!string.IsNullOrEmpty(dr[4].ToString()))
                element.InstituteId = Convert.ToInt32(dr[4].ToString());
            element.InstituteName = dr[19].ToString();
            if (!string.IsNullOrEmpty(dr[5].ToString()))
                element.LastDateOfFeesPaymentForStudent = Convert.ToDateTime(dr[5].ToString()).ToShortDateString();
            if (!string.IsNullOrEmpty(dr[6].ToString()))
                element.LastDateOfFeesPaymentForCollege = Convert.ToDateTime(dr[6].ToString()).ToShortDateString();
            if (!string.IsNullOrEmpty(dr[7].ToString()))
                element.StartDateOfExam = Convert.ToDateTime(dr[7].ToString()).ToShortDateString();
            if (!string.IsNullOrEmpty(dr[8].ToString()))
                element.EndDateOfExam = Convert.ToDateTime(dr[8].ToString()).ToShortDateString();
            if (!string.IsNullOrEmpty(dr[9].ToString()))
                element.ProbableDateOfResult = Convert.ToDateTime(dr[9].ToString()).ToShortDateString();
            element.IsActive = Convert.ToBoolean(dr[10].ToString());
            element.CreatedOn = Convert.ToDateTime(dr[11].ToString());
            element.CreatedBy = Convert.ToInt32(dr[12].ToString());
            if (!string.IsNullOrEmpty(dr[13].ToString()))
                element.ModifiedOn = Convert.ToDateTime(dr[13].ToString());
            if (!string.IsNullOrEmpty(dr[14].ToString()))
                element.ModifiedBy = Convert.ToInt32(dr[14].ToString());
            if (!string.IsNullOrEmpty(dr[15].ToString()))
                element.IsDeleted = Convert.ToBoolean(dr[15].ToString());
            if (!string.IsNullOrEmpty(dr[16].ToString()))
                element.IsConfirmed = Convert.ToBoolean(dr[16].ToString());
            element.DisplayName = dr[17].ToString();

            //BALCourseScheduleMap func = new BALCourseScheduleMap();
            //element.attachedCourses = func.getCourseScheduleMapList(1, 0, 1, Convert.ToInt32(dr[0].ToString()));
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
                BALCourseScheduleMap func = new BALCourseScheduleMap();
                element.attachedCourses = func.getCourseScheduleMapList("admin", 0, 1, Convert.ToInt32(Dt.Rows[0][0].ToString()), null, null);

            }
            return element; 
        }

        public List<FacultyExamMap> getFacultyExamMapList(string flag, int? IsDeleted, int? IsActive, int? ExamMasterId, int? FacultyId, string methodNm)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("FacultyExamMapListGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Flag", flag);
                Cmd.Parameters.AddWithValue("@IsActive", IsActive);
                Cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
                Cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                Cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<FacultyExamMap> ObjListFacultyExamMap = new List<FacultyExamMap>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        FacultyExamMap objFacultyExamMap = new FacultyExamMap();
                        if (flag == "admin")
                        {
                            setRowIntoObjForAdmin(objFacultyExamMap, Dt.Rows[i], null);
                        }
                        else if (flag == "user")
                        {
                            setRowIntoObjForUser(objFacultyExamMap, Dt.Rows[i]);
                        }
                        else
                        {
                            setRowIntoObjForAll(objFacultyExamMap, Dt.Rows[i]);
                        }

                        ObjListFacultyExamMap.Add(objFacultyExamMap);
                    }
                }
                return ObjListFacultyExamMap;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<FacultyExamMap> getFacultyExamMapStatus(int? ExamMasterId, int? IsDeleted, int? IsActive, string methodNm)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("FacultyExamMapListGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Flag", "admin");
                Cmd.Parameters.AddWithValue("@IsActive", IsActive);
                Cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
                Cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                Cmd.Parameters.AddWithValue("@FacultyId", null);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<FacultyExamMap> ObjListFacultyExamMap = new List<FacultyExamMap>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        FacultyExamMap element = new FacultyExamMap();
                        element = setRowIntoObjForAdmin(element, Dt.Rows[i], null);
                        DataTable Dt1 = new DataTable();
                        SqlCommand Cmd1 = new SqlCommand("FacultyExamMapStatusGet", Con);
                        Cmd1.CommandType = CommandType.StoredProcedure;
                        Cmd1.Parameters.AddWithValue("@Id", element.Id);
                        Da.SelectCommand = Cmd1;

                        Da.Fill(Dt1);
                        if (Dt1.Rows.Count > 0)
                        {
                            element.AttachCourseCount = Convert.ToInt32(Dt1.Rows[0][0].ToString());
                            element.AttchedVenueCount = Convert.ToInt32(Dt1.Rows[0][1].ToString());
                            element.VenueStatus = Dt1.Rows[0][2].ToString();
                        }
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
        public List<FacultyExamMap> getFacultyExamMapList1(int? ExamMasterId, Int64 UserId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("FacultyExamMapListGet1", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                Cmd.Parameters.AddWithValue("@UserId", UserId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<FacultyExamMap> ObjListFacultyExamMap = new List<FacultyExamMap>();
                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        FacultyExamMap objFacultyExamMap = new FacultyExamMap();

                        
                        objFacultyExamMap.Id = (((dr["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(dr["Id"]);
                        objFacultyExamMap.ScheduleCode = (Convert.ToString(dr["ScheduleCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["ScheduleCode"]);                   
                        objFacultyExamMap.ExamMasterId = (((dr["ExamMasterId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(dr["ExamMasterId"]);
                        objFacultyExamMap.FacultyId = (((dr["FacultyId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(dr["FacultyId"]);
                        objFacultyExamMap.FacultyName = (Convert.ToString(dr["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["FacultyName"]);
                        objFacultyExamMap.InstituteId = (((dr["InstituteId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(dr["InstituteId"]);
                        objFacultyExamMap.InstituteName = (Convert.ToString(dr["InstituteName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["InstituteName"]);
                        objFacultyExamMap.IsActive = (Convert.ToString(dr["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(dr["IsActive"]);
                        objFacultyExamMap.IsDeleted = (Convert.ToString(dr["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(dr["IsDeleted"]);
                        objFacultyExamMap.IsConfirmed = (Convert.ToString(dr["IsConfirmed"])).IsEmpty() ? false : Convert.ToBoolean(dr["IsConfirmed"]);
                        objFacultyExamMap.DisplayName = (Convert.ToString(dr["DisplayName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["DisplayName"]);
                        if (!string.IsNullOrEmpty(dr["LastDateOfFeesPaymentForStudent"].ToString()))
                        {
                            objFacultyExamMap.LastDateOfFeesPaymentForStudent = Convert.ToDateTime(dr["LastDateOfFeesPaymentForStudent"].ToString()).ToShortDateString();
                            
                        }
                        if (!string.IsNullOrEmpty(dr["LastDateOfFeesPaymentForCollege"].ToString()))
                        {
                            objFacultyExamMap.LastDateOfFeesPaymentForCollege = Convert.ToDateTime(dr["LastDateOfFeesPaymentForCollege"].ToString()).ToShortDateString();
                        }
                            
                        if (!string.IsNullOrEmpty(dr["StartDateOfExam"].ToString()))
                        {
                            //objFacultyExamMap.StartDateOfExam = Convert.ToDateTime(dr["StartDateOfExam"].ToString()).ToShortDateString();
                            objFacultyExamMap.StartDateOfExam = string.Format("{0: dd-MM-yyyy}", Convert.ToDateTime(dr["StartDateOfExam"].ToString()));
                        }
                            
                        if (!string.IsNullOrEmpty(dr["EndDateOfExam"].ToString()))
                        {
                            //objFacultyExamMap.EndDateOfExam = Convert.ToDateTime(dr["EndDateOfExam"].ToString()).ToShortDateString();
                            objFacultyExamMap.EndDateOfExam = string.Format("{0: dd-MM-yyyy}", Convert.ToDateTime(dr["EndDateOfExam"].ToString()));
                        }
                            
                        if (!string.IsNullOrEmpty(dr["ProbableDateOfResult"].ToString()))
                        {
                            objFacultyExamMap.ProbableDateOfResult = Convert.ToDateTime(dr["ProbableDateOfResult"].ToString()).ToShortDateString();
                        }
                       



                        ObjListFacultyExamMap.Add(objFacultyExamMap);
                    }
                    return ObjListFacultyExamMap;
                }
                else
                {
                    return null;
                }
                
            }
            catch (Exception e)
            {
                return null;

            }
        }
    }
}