using MSUIS_TokenManager.App_Start;
using MSUISApi.BAL;
using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MSUISApi.Controllers
{
    public class ExamFormMasterController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();

        [HttpPost]
        public HttpResponseMessage ExamFormMasterGetInwardByFacutyExamMapId(ExamFormMaster dataString)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                string resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int64 UserId = Convert.ToInt64(res.ToString());

                if (String.IsNullOrEmpty(dataString.FacultyExamMapId.ToString()))
                {
                    return Return.returnHttp("200", "Please select Schedule.", null);
                }

                List<ExamFormMaster> ExamFormMasterList = new List<ExamFormMaster>();
                BALExamFormMaster func = new BALExamFormMaster();
                //flag=1,IsDeleted=0,IsActive=null
                ExamFormMasterList = func.ExamFormMasterGetInwardByFacutyExamMapId(dataString.FacultyExamMapId);

                return Return.returnHttp("200", ExamFormMasterList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage ExamFormMasterPartTermStatusGetByCourseScheduleMap(ExamFormMaster dataString)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                string resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int64 UserId = Convert.ToInt64(res.ToString());

                if (String.IsNullOrEmpty(dataString.FacultyExamMapId.ToString()))
                {
                    return Return.returnHttp("200", "Please select Schedule.", null);
                }

                List<ExamFormMaster> ExamFormMasterList = new List<ExamFormMaster>();
                BALExamFormMaster func = new BALExamFormMaster();
                //flag=1,IsDeleted=0,IsActive=null
                ExamFormMasterList = func.ExamFormMasterPartTermStatusGetByCourseScheduleMap(dataString.FacultyExamMapId);

                return Return.returnHttp("200", ExamFormMasterList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage ExamFormMasterGetInward(ExamFormMaster dataString)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                string resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int64 UserId = Convert.ToInt64(res.ToString());

                List<ExamFormMaster> ExamFormMasterList = new List<ExamFormMaster>();
                BALExamFormMaster func = new BALExamFormMaster();
                //flag=1,IsDeleted=0,IsActive=null
                ExamFormMasterList = func.ExamFormMasterGetInward(dataString.ExamMasterId);

                return Return.returnHttp("200", ExamFormMasterList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage ExamFormMasterPartTermStatusGet(ExamFormMaster dataString)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                string resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int64 UserId = Convert.ToInt64(res.ToString());

                List<ExamFormMaster> ExamFormMasterList = new List<ExamFormMaster>();
                BALExamFormMaster func = new BALExamFormMaster();
                //flag=1,IsDeleted=0,IsActive=null
                ExamFormMasterList = func.ExamFormMasterPartTermStatusGet(dataString.ExamMasterId);

                return Return.returnHttp("200", ExamFormMasterList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage ExamFormMasterAdd(ExamFormMaster dataString)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                string resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int64 UserId = Convert.ToInt64(res.ToString());
                //Int64 UserId = 1;

                //if (String.IsNullOrEmpty(dataString.FacultyExamMapId.ToString()))
                //{
                //    return Return.returnHttp("200", "Please select Schedule.", null);
                //}
                if (String.IsNullOrEmpty(dataString.ExamMasterId.ToString()))
                {
                    return Return.returnHttp("200", "Please select Exam Master.", null);
                }
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                //SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                //SqlCommand Cmd = new SqlCommand("ExamFormMasterListGetByIncStudentAcademicInformation", Con);
                //Cmd.CommandType = CommandType.StoredProcedure;
                //Cmd.Parameters.AddWithValue("@ExamMasterId", dataString.ExamMasterId);
                //Cmd.Parameters.AddWithValue("@FacultyExamMapId", dataString.FacultyExamMapId);
                //Da.SelectCommand = Cmd;

                //Da.Fill(Dt);

                //List<ExamFormMaster> ObjListExamFormMaster = new List<ExamFormMaster>();
                //if (Dt.Rows.Count > 0)
                //{
                string strMessage = string.Empty;
                //for (int i = 0; i < Dt.Rows.Count; i++)
                //{
                //ExamFormMaster element = new ExamFormMaster();
                SqlCommand cmd1 = new SqlCommand("ExamFormMasterAdd", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                //     cmd1.Parameters.AddWithValue("@IncStudentAcademicInfoId", Convert.ToInt64(Dt.Rows[i][0].ToString()));
                // cmd1.Parameters.AddWithValue("@PRN", Convert.ToInt64(Dt.Rows[i][1].ToString()));
                //  cmd1.Parameters.AddWithValue("@ProgInstancePartTermId", Convert.ToInt64(Dt.Rows[i][2].ToString()));
                // cmd1.Parameters.AddWithValue("@ProgrammeId", Convert.ToInt32(Dt.Rows[i][3].ToString()));
                // cmd1.Parameters.AddWithValue("@FacultyId", Convert.ToInt32(Dt.Rows[i][4].ToString()));
                //if (String.IsNullOrEmpty(Dt.Rows[i][5].ToString()))
                //    cmd1.Parameters.AddWithValue("@AppearanceTypeId", 1);
                //else if ((Dt.Rows[i][5].ToString() == "Fail") || (Dt.Rows[i][5].ToString() == "ATKT") || (Dt.Rows[i][5].ToString() == "Fail-ATKT"))
                //    cmd1.Parameters.AddWithValue("@AppearanceTypeId", 2);
                //else if (Dt.Rows[i][5].ToString() == "Absent")
                //    cmd1.Parameters.AddWithValue("@AppearanceTypeId", 3);
                cmd1.Parameters.AddWithValue("@ExamMasterId", Convert.ToInt64(dataString.ExamMasterId));
                cmd1.Parameters.AddWithValue("@ProgrammePartTermId", Convert.ToInt64(dataString.ProgrammePartTermId));
                cmd1.Parameters.AddWithValue("@BranchId", Convert.ToInt64(dataString.BranchId));
                // cmd1.Parameters.AddWithValue("@ProgInstancePartTermId", Convert.ToInt64(Dt.Rows[i][2].ToString()));
                //cmd1.Parameters.AddWithValue("@InwardMode", "Offline");
                cmd1.Parameters.AddWithValue("@UserId", UserId);
                cmd1.Parameters.AddWithValue("@UserTime", datetime);
                cmd1.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                cmd1.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                con.Open();
                cmd1.ExecuteNonQuery();
                strMessage = Convert.ToString(cmd1.Parameters["@MESSAGE"].Value);
                con.Close();
                //}
                if (!string.Equals(strMessage, "TRUE"))
                {
                    return Return.returnHttp("201", strMessage, null);
                }
                else
                {
                    return Return.returnHttp("200", "Your Exam Form Generated Successfully.", null);
                }
                //}
                //else
                //{
                //    return Return.returnHttp("201", "Record Not Found.", null);
                //}
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        ///created ExamFormMasterAddByCourseScheduleMapId API in ExamFormMaster

        [HttpPost]
        public HttpResponseMessage ExamFormMasterAddByCourseScheduleMapId(ExamFormMaster dataString)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                string resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int64 UserId = Convert.ToInt64(res.ToString());


                if (String.IsNullOrEmpty(dataString.CourseSchduleMapId.ToString()) || dataString.CourseSchduleMapId == 0)
                {
                    return Return.returnHttp("201", "Please select Course schedule.", null);
                }
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("ExamFormMasterGetByCourseScheduleMapId", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@CourseScheduleMapId", dataString.CourseSchduleMapId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<ExamFormMaster> ObjListExamFormMaster = new List<ExamFormMaster>();

                if (Dt.Rows.Count > 0)
                {
                    bool? flag = true;
                    string strMessage = string.Empty;
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        //ExamFormMaster element = new ExamFormMaster();
                        SqlCommand cmd1 = new SqlCommand("ExamFormMasterAdd", con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@IncStudentAcademicInfoId", Convert.ToInt64(Dt.Rows[i][0].ToString()));
                        cmd1.Parameters.AddWithValue("@PRN", Convert.ToInt64(Dt.Rows[i][1].ToString()));
                        cmd1.Parameters.AddWithValue("@ProgInstancePartTermId", Convert.ToInt64(Dt.Rows[i][2].ToString()));
                        cmd1.Parameters.AddWithValue("@ProgrammeId", Convert.ToInt32(Dt.Rows[i][3].ToString()));
                        cmd1.Parameters.AddWithValue("@FacultyId", Convert.ToInt32(Dt.Rows[i][4].ToString()));
                        if (String.IsNullOrEmpty(Dt.Rows[i][5].ToString()))
                            cmd1.Parameters.AddWithValue("@AppearanceTypeId", 1);
                        else if ((Dt.Rows[i][5].ToString() == "Fail") || (Dt.Rows[i][5].ToString() == "ATKT") || (Dt.Rows[i][5].ToString() == "Fail-ATKT"))
                            cmd1.Parameters.AddWithValue("@AppearanceTypeId", 2);
                        else if (Dt.Rows[i][5].ToString() == "Absent")
                            cmd1.Parameters.AddWithValue("@AppearanceTypeId", 3);
                        if (!String.IsNullOrEmpty(Dt.Rows[i][6].ToString()))
                            cmd1.Parameters.AddWithValue("@ExamMasterId", Convert.ToInt64(Dt.Rows[i][6].ToString()));
                        cmd1.Parameters.AddWithValue("@InwardMode", "Offline");
                        cmd1.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                        cmd1.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                        con.Open();
                        cmd1.ExecuteNonQuery();
                        strMessage = Convert.ToString(cmd1.Parameters["@MESSAGE"].Value);
                        con.Close();

                    }

                    if (!string.Equals(strMessage, "TRUE"))
                    {
                        flag = false;
                        return Return.returnHttp("201", strMessage, null);
                    }
                    else
                    {
                        return Return.returnHttp("200", "Your Exam Form Generated Successfully.", null);
                    }
                }
                else
                {
                    return Return.returnHttp("201", "Record Not Found.", null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage ExamFormAddInBulk(ExamFormMaster dataString)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                string resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int64 UserId = Convert.ToInt64(res.ToString());

                if (String.IsNullOrEmpty(dataString.FacultyExamMapId.ToString()))
                {
                    return Return.returnHttp("200", "Please select Schedule.", null);
                }
                if (String.IsNullOrEmpty(dataString.ExamMasterId.ToString()))
                {
                    return Return.returnHttp("200", "Please select Exam Master.", null);
                }
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("ExamFormMasterListGetByIncStudentAcademicInformation", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@ExamMasterId", dataString.ExamMasterId);
                Cmd.Parameters.AddWithValue("@FacultyExamMapId", dataString.FacultyExamMapId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                if (Dt.Rows.Count > 0)
                {
                    SqlCommand cmd1 = new SqlCommand("sp_NewProc", con);
                    cmd1.Parameters.AddWithValue("@udt", Dt); // passing Datatable  
                    cmd1.Parameters.AddWithValue("@ExamMasterId", dataString.ExamMasterId);
                    cmd1.Parameters.AddWithValue("@ProgInstPartTermId", dataString.ProgInstancePartTermId);
                    //cmd1.Parameters.AddWithValue("@AppearanceTypeId", dataString.AppearanceTypeId);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd1.ExecuteNonQuery();
                    con.Close();

                    // return Return.returnHttp("200", "TRUE", null);

                }
                return Return.returnHttp("200", "TRUE", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        //[HttpPost]
        //public HttpResponseMessage UpdateSeatNoInBulk(UploadExcel dataString)
        //{
        //    try
        //    {
        //        String token = Request.Headers.GetValues("token").FirstOrDefault();
        //        String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
        //        String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
        //        TokenOperation ac = new TokenOperation();
        //        string res = ac.ValidateToken(token);
        //        string resRoleToken = ac.ValidateRoleToken(userRoleToken);

        //        if (res == "0")
        //        {
        //            return Return.returnHttp("0", null, null);
        //        }
        //        else if (resRoleToken == "0")
        //        {
        //            return Return.returnHttp("0", null, null);
        //        }
        //        Int64 UserId = Convert.ToInt64(res.ToString());

        //        string fileName = dataString.fileName;
        //        if (string.IsNullOrEmpty(fileName))
        //        {
        //            return Return.returnHttp("201", "Invalid filename. Please try again.", null);
        //        }

        //        string filepath = System.IO.Path.GetFullPath(System.Web.Hosting.HostingEnvironment.MapPath("~/docs/" + fileName));
        //        DataSet ds = Function.ReadExcelData(filepath, "sheet1");
        //        if (ds == null)
        //            return Return.returnHttp("201", "Invalid or Empty Excel file updoaded. Please try again.", null);
        //        DataTable dt = ds.Tables[0];

        //        dt.Rows.RemoveAt(0);
        //        int n = dt.Rows.Count;
        //        if (dt.Rows.Count > 0)
        //        {
        //            //SqlCommand cmd1 = new SqlCommand("UpdateSeatNoInBulk", con);
        //            //cmd1.Parameters.AddWithValue("@udt", dt); // passing Datatable  
        //            //cmd1.Parameters.AddWithValue("@ExamMasterId", dataString.ExamMasterId);
        //            //cmd1.Parameters.AddWithValue("@ProgInstPartTermId", dataString.ProgInstancePartTermId);
        //            //cmd1.CommandType = CommandType.StoredProcedure;
        //            //con.Open();
        //            //cmd1.ExecuteNonQuery().ToString();
        //            //con.Close();

        //            //return Return.returnHttp("200", "SeatNo Updated Successfully.", null);
        //            BALExamFormMaster func = new BALExamFormMaster();
        //            string output = func.UpdateSeatNo(dt, dataString.ExamMasterId, dataString.ProgInstancePartTermId);
        //            return Return.returnHttp("200", output, null);
        //        }
        //        return Return.returnHttp("201", null, null);
        //    }
        //    catch (Exception e)
        //    {
        //        return Return.returnHttp("201", e.Message, null);
        //    }
        //}

        [HttpPost]
        public HttpResponseMessage updateIsExamFeesPaidByPRN(ExamFormInward dataString)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                string resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int64 UserId = Convert.ToInt64(res.ToString());
               // Int64 UserId = 1;

                string PRNNo = dataString.PRNNo;
                if (String.IsNullOrEmpty(Convert.ToString(dataString.ProgInstancePartTermId)))
                {
                    return Return.returnHttp("201", "Please select programme instance part term.", null);
                }
                if (!string.IsNullOrEmpty(PRNNo))
                {
                    string[] PRNNos = PRNNo.Split(',');

                    //DataTable dt = new DataTable();
                    //dt.Columns.Add("PRNNo", typeof(string));
                    //for (int i = 0; i < PRNNos.Length; i++)
                    //    dt.Rows.Add(PRNNos[i]);

                    //int n = dt.Rows.Count;
                    //if (dt.Rows.Count > 0)
                    //{
                    //    BALExamFormMaster func = new BALExamFormMaster();
                    //    string output = func.UpdateSeatNo(dt, dataString.ExamMasterId, dataString.ProgInstancePartTermId);
                    //    return Return.returnHttp("200", output, null);
                    //}
                    bool? flag = true;
                    string strMessage = string.Empty;
                    for (int i = 0; i < PRNNos.Length; i++)
                    {
                        SqlCommand cmd = new SqlCommand("updateIsExamFeesPaidByPRN", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@PRN", PRNNos[i]);
                        cmd.Parameters.AddWithValue("@ProgInstancePartTermId", dataString.ProgInstancePartTermId);
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@UserTime", datetime);

                        cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                        cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                        con.Open();
                        cmd.ExecuteNonQuery();
                        strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                        con.Close();
                        if (!string.Equals(strMessage, "TRUE"))
                        {
                            flag = false;
                            return Return.returnHttp("201", strMessage, null);
                        }
                    }
                    if (string.Equals(strMessage, "TRUE"))
                    {
                        return Return.returnHttp("200", "You data modified successfully.", null);
                    }
                }
                return Return.returnHttp("201", "Please enter PRNNo.", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateSeatNoUsingList(StudentListForExamForm dataString)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                string resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int64 UserId = Convert.ToInt64(res.ToString());

                List<StudentProfile> studentsList = dataString.students;


                string students = string.Join(",", studentsList.Select(student => student.PRN));

                string[] PRNNos = students.Split(',');

                DataTable dt = new DataTable();
                dt.Columns.Add("PRNNo", typeof(string));
                for (int i = 0; i < PRNNos.Length; i++)
                    dt.Rows.Add(PRNNos[i]);

                int n = dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {
                    BALExamFormMaster func = new BALExamFormMaster();
                    string output = func.UpdateSeatNo(dt, dataString.ExamMasterId, dataString.ProgInstancePartTermId);
                    return Return.returnHttp("200", output, null);
                }

                return Return.returnHttp("201", null, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        //Code By Jaydev
        //[HttpPost]
        //public HttpResponseMessage ApplicationFormForExaminationDetails(ApplicationFormForExaminationDetail dataString)
        //{
        //    try
        //    {
        //        //String token = Request.Headers.GetValues("token").FirstOrDefault();
        //        //String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
        //        //String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
        //        //TokenOperation ac = new TokenOperation();
        //        //string res = ac.ValidateToken(token);
        //        //string resRoleToken = ac.ValidateRoleToken(userRoleToken);

        //        //if (res == "0")
        //        //{
        //        // return Return.returnHttp("0", null, null);
        //        //}
        //        //else if (resRoleToken == "0")
        //        //{
        //        // return Return.returnHttp("0", null, null);
        //        //}
        //        //Int64 UserId = Convert.ToInt64(res.ToString());
        //        if (String.IsNullOrEmpty(dataString.PRN.ToString()))
        //        {
        //            return Return.returnHttp("201", " Please enter PRN ", null);
        //        }

        //        ApplicationFormForExamination func = new ApplicationFormForExamination();
        //        string output = func.GenerateApplicationFormForExamination(dataString);

        //        if (String.IsNullOrEmpty(output))
        //            return Return.returnHttp("200", " Some Internal issue occured. ", null);

        //        return Return.returnHttp("200", " Application Form For Examination is Generated by " + output + " file. ", null);

        //    }
        //    catch (Exception e)
        //    {
        //        return Return.returnHttp("201", e.Message, null);
        //    }
        //}

        //[HttpPost]
        //public HttpResponseMessage GenerateHallTicket(ApplicationFormForExaminationDetail dataString)
        //{
        //    try
        //    {
        //        //String token = Request.Headers.GetValues("token").FirstOrDefault();
        //        //String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
        //        //String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
        //        //TokenOperation ac = new TokenOperation();
        //        //string res = ac.ValidateToken(token);
        //        //string resRoleToken = ac.ValidateRoleToken(userRoleToken);

        //        //if (res == "0")
        //        //{
        //        // return Return.returnHttp("0", null, null);
        //        //}
        //        //else if (resRoleToken == "0")
        //        //{
        //        // return Return.returnHttp("0", null, null);
        //        //}
        //        //Int64 UserId = Convert.ToInt64(res.ToString());
        //        if (String.IsNullOrEmpty(dataString.PRN.ToString()))
        //        {
        //            return Return.returnHttp("201", " Please enter PRN ", null);
        //        }

        //        ExaminationHallTicket func = new ExaminationHallTicket();
        //        string output = func.GenerateHallTicket(dataString);

        //        if (String.IsNullOrEmpty(output))
        //            return Return.returnHttp("200", " Some Internal issue occured. ", null);

        //        return Return.returnHttp("200", " Application Form For Examination is Generated by " + output + " file. ", null);

        //    }
        //    catch (Exception e)
        //    {
        //        return Return.returnHttp("201", e.Message, null);
        //    }
        //}

        //[HttpPost]
        //public HttpResponseMessage GenerateAttendanceAndSupervisorReport(ApplicationFormForExaminationDetail dataString)
        //{
        //    try
        //    {
        //        //String token = Request.Headers.GetValues("token").FirstOrDefault();
        //        //String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
        //        //String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
        //        //TokenOperation ac = new TokenOperation();
        //        //string res = ac.ValidateToken(token);
        //        //string resRoleToken = ac.ValidateRoleToken(userRoleToken);

        //        //if (res == "0")
        //        //{
        //        // return Return.returnHttp("0", null, null);
        //        //}
        //        //else if (resRoleToken == "0")
        //        //{
        //        // return Return.returnHttp("0", null, null);
        //        //}
        //        //Int64 UserId = Convert.ToInt64(res.ToString());
        //        if (String.IsNullOrEmpty(dataString.ExamMasterId.ToString()))
        //        {
        //            return Return.returnHttp("201", " Please select Exam Event ", null);
        //        }

        //        AttendanceSheetAndSupervisorReport func = new AttendanceSheetAndSupervisorReport();
        //        string output = func.GenerateAttendanceSheetAndSupervisorReport(dataString.ExamMasterId, dataString.ProgInstancePartTermId);

        //        if (String.IsNullOrEmpty(output))
        //            return Return.returnHttp("200", " Some Internal issue occured. ", null);

        //        return Return.returnHttp("200", " Attendance Sheet and Supervisor report is Generated by " + output + " file. ", null);

        //    }
        //    catch (Exception e)
        //    {
        //        return Return.returnHttp("201", e.Message, null);
        //    }
        //}

        [HttpPost]
        public HttpResponseMessage GenerateSeatNo(ExamFormSeatGeneration dataString)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                string resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int64 UserId = Convert.ToInt64(res.ToString());
                //Int64 UserId = 1;

                if (String.IsNullOrEmpty(dataString.ExamMasterId.ToString()))
                {
                    return Return.returnHttp("201", " Please select Exam Event ", null);
                }

                if (!(dataString.IncludeFresher || dataString.IncludeRepeater || dataString.IncludeNeverAppeared))
                {
                    return Return.returnHttp("201", " Please select AppearanceType.", null);
                }

                if (dataString.SeatNoGeneration.Length == 0)
                {
                    return Return.returnHttp("201", " Array is empty. ", null);
                }

                List<SeatNo> lst = dataString.SeatNoGeneration.OfType<SeatNo>().ToList();

                DataTable dt = new DataTable();
                dt.Columns.Add("FieldName", typeof(string));
                dt.Columns.Add("DisplayName", typeof(string));
                dt.Columns.Add("IsSelected", typeof(Boolean));
                dt.Columns.Add("SequenceNo", typeof(int));
                dt.Columns.Add("OrderingType", typeof(int));
                foreach (var item in lst)
                {
                    //dt.Rows.Add()
                    dt.Rows.Add(item.FieldName, item.DisplayName, item.IsSelected, item.SequenceNo, item.OrderingType);
                }

                BALExamFormMaster func = new BALExamFormMaster();
                string Message = func.GenerateSeatNo(dt, dataString, UserId);
                if (Message == "TRUE")
                {
                    return Return.returnHttp("200", Message, null);
                }
                return Return.returnHttp("201", Message, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage ExamFormDetails(ExamFormDetails dataString)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();

                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                if (dataString.ProgrammePartTermId == 0)
                {
                    return Return.returnHttp("201", "Please select Programme Part Term", null);
                }

                if (dataString.BranchId == 0)
                {
                    return Return.returnHttp("201", "Please select Branch", null);
                }
                ExamFormDetails element = new ExamFormDetails();
                BALExamFormMaster func = new BALExamFormMaster();
                element = func.GetExamFormDetails(dataString.ProgrammePartTermId, dataString.BranchId, dataString.ExamMasterId);

                return Return.returnHttp("200", element, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        

	    [HttpPost]
        public HttpResponseMessage generateSingleHallTicket(HallTicket dataString)
        {
            try
            {
                if (dataString.Vidhyarthi != true)
                {
                    String token = Request.Headers.GetValues("token").FirstOrDefault();
                    String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                    String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                    TokenOperation ac = new TokenOperation();
                    string res = ac.ValidateToken(token);
                    string resRoleToken = ac.ValidateRoleToken(userRoleToken);

                    if (res == "0")
                    {
                        return Return.returnHttp("0", null, null);
                    }
                    else if (resRoleToken == "0")
                    {
                        return Return.returnHttp("0", null, null);
                    }
                    Int64 UserId = Convert.ToInt64(res.ToString());
                    if (String.IsNullOrEmpty(dataString.PRN.ToString()))
                    {
                        return Return.returnHttp("201", " Please enter PRN ", null);
                    }
                    if (String.IsNullOrEmpty(dataString.ExamMasterId.ToString()))
                    {
                        return Return.returnHttp("201", " Please select exam event ", null);
                    }
                    if (String.IsNullOrEmpty(dataString.ProgrammeInstancePartTermId.ToString()))
                    {
                        return Return.returnHttp("201", " Please select programme instance part term ", null);
                    }
                }                
                ExaminationHallTicket func = new ExaminationHallTicket();
                string output = func.GenerateSingleHallTicket(dataString);

                if (String.IsNullOrEmpty(output))
                    return Return.returnHttp("201", " Some Internal issue occured. ", null);

                return Return.returnHttp("200",output, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage generateBulkHallTicket(HallTicket dataString)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                string resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int64 UserId = Convert.ToInt64(res.ToString());

                if (String.IsNullOrEmpty(dataString.ExamMasterId.ToString()))
                {
                    return Return.returnHttp("201", " Please select exam event ", null);
                }
                if (String.IsNullOrEmpty(dataString.ProgrammePartTermId.ToString()))
                {
                    return Return.returnHttp("201", " Please select programme part term ", null);
                }

                ExaminationHallTicket func = new ExaminationHallTicket();

                string output = func.GenerateBulkHallTicket(dataString);
                //string output = func.GenerateBulkHallTicketInOnePDF(dataString);

                /*if (output != "200")
                    return Return.returnHttp("201", " Some Internal issue occured. ", null);

                return Return.returnHttp("200", " Hall ticket For Examination is Generated successfully. ", null);*/

                if (String.IsNullOrEmpty(output))
                    return Return.returnHttp("201", " Some Internal issue occured. ", null);

                return Return.returnHttp("200", output, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage generateBulkHallTicketInSinglePdf(HallTicket dataString)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                string resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int64 UserId = Convert.ToInt64(res.ToString());

                if (String.IsNullOrEmpty(dataString.ExamMasterId.ToString()))
                {
                    return Return.returnHttp("201", " Please select exam event ", null);
                }
                if (String.IsNullOrEmpty(dataString.ProgrammePartTermId.ToString()))
                {
                    return Return.returnHttp("201", " Please select programme part term ", null);
                }

                ExaminationHallTicket func = new ExaminationHallTicket();

                string output = func.GenerateBulkHallTicketInOnePDF(dataString);
                //string output = func.GenerateBulkHallTicketInOnePDF(dataString);

                /*if (output != "200")
                    return Return.returnHttp("201", " Some Internal issue occured. ", null);

                return Return.returnHttp("200", " Hall ticket For Examination is Generated successfully. ", null);*/

                if (String.IsNullOrEmpty(output))
                    return Return.returnHttp("201", " Some Internal issue occured. ", null);

                return Return.returnHttp("200", output, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }


        [HttpPost]
        public HttpResponseMessage generateBlankMarkSheet(BlankMarkSheets dataString)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                string resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int64 UserId = Convert.ToInt64(res.ToString());

                if (String.IsNullOrEmpty(dataString.ExamMasterId.ToString()))
                {
                    return Return.returnHttp("201", " Please select exam event ", null);
                }
                else if (String.IsNullOrEmpty(dataString.ProgrammePartTermId.ToString()))
                {
                    return Return.returnHttp("201", " Please select programme part term ", null);
                }
                else if (String.IsNullOrEmpty(dataString.BranchId.ToString()))
                {
                    return Return.returnHttp("201", " Please select Branch ", null);
                }
                else if (String.IsNullOrEmpty(dataString.PaperId.ToString()))
                {
                    return Return.returnHttp("201", " Please select paper ", null);
                }
                else
                {
                    BlankMarksheet func = new BlankMarksheet();

                    string output = func.GenerateBlankMarksheet(dataString);
                    //string output = func.GenerateBulkHallTicketInOnePDF(dataString);

                    if (string.IsNullOrEmpty(output))
                    {
                        return Return.returnHttp("201", " Some Internal issue occured. ", null);
                    }
                    else if (output.StartsWith("Error"))
                    {
                        return Return.returnHttp("201", output, null);
                    }
                    else if (output == "No Record Found")
                    {
                        return Return.returnHttp("200", "No Record Found", null);
                    }
                    else if (output.Contains('/'))
                    {
                        return Return.returnHttp("200", output, null);
                    }
                    
                    else
                    {
                        return Return.returnHttp("201", output, null);
                    }
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage generateBlankMarksheetForIA(BlankMarkSheets dataString)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                string resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int64 UserId = Convert.ToInt64(res.ToString());

                if (String.IsNullOrEmpty(dataString.ExamMasterId.ToString()))
                {
                    return Return.returnHttp("201", " Please select exam event ", null);
                }
                else if (String.IsNullOrEmpty(dataString.ProgrammePartTermId.ToString()))
                {
                    return Return.returnHttp("201", " Please select programme part term ", null);
                }
                else if (String.IsNullOrEmpty(dataString.BranchId.ToString()))
                {
                    return Return.returnHttp("201", " Please select Branch ", null);
                }
                else if (String.IsNullOrEmpty(dataString.PaperId.ToString()))
                {
                    return Return.returnHttp("201", " Please select paper ", null);
                }
                else
                {
                    BlankMarksheet func = new BlankMarksheet();

                    string output = func.GenerateBlankMarksheetForIA(dataString);
                    //string output = func.GenerateBulkHallTicketInOnePDF(dataString);

                    if (string.IsNullOrEmpty(output))
                    {
                        return Return.returnHttp("201", " Some Internal issue occured. ", null);
                    }
                    else if (output.StartsWith("Error"))
                    {
                        return Return.returnHttp("201", output, null);
                    }
                    else if (output == "No Record Found")
                    {
                        return Return.returnHttp("200", "No Record Found", null);
                    }

                    else if (output.Contains('/'))
                    {
                        return Return.returnHttp("200", output, null);
                    }

                    else
                    {
                        return Return.returnHttp("200", output, null);
                    }
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage getPaperListForBlankMarksheet(BlankMarkSheets dataString)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                string resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int64 UserId = Convert.ToInt64(res.ToString());

                int? ExamMasterId = dataString.ExamMasterId, ProgrammePartTermId = dataString.ProgrammePartTermId, TeachingLeaningMapId = dataString.TeachingLearningMethodId, AssessmentMethodId = dataString.AssessmentMethodId;

                if (string.IsNullOrEmpty(ExamMasterId.ToString()))
                {
                    return Return.returnHttp("201", "Please select Exam Event.", null);
                }
                if (string.IsNullOrEmpty(ProgrammePartTermId.ToString()))
                {
                    return Return.returnHttp("201", "Please select Programme Part Term.", null);
                }
                if (string.IsNullOrEmpty(TeachingLeaningMapId.ToString()))
                {
                    return Return.returnHttp("201", "Please select Teaching Learning Method.", null);
                }
                if (string.IsNullOrEmpty(AssessmentMethodId.ToString()))
                {
                    return Return.returnHttp("201", "Please select Assessment Method.", null);
                }
                BALExamFormMaster func = new BALExamFormMaster();
                dataString = func.getPaperListForBlankMarksheet(dataString);

                return Return.returnHttp("200", dataString, null);


            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        [HttpPost]
        public HttpResponseMessage GetPendingExamInwardList(ExamInwardDetails dataString)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();

                string res = ac.ValidateToken(token);
                Int64 UserId = Convert.ToInt64(res.ToString());
                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                if (dataString.PRN == 0)
                {
                    return Return.returnHttp("201", "Please enter PRN", null);
                }

                if (dataString.ExamMasterId == 0 || string.IsNullOrEmpty(dataString.ExamMasterId.ToString()))
                {
                    return Return.returnHttp("201", "Please select exam event", null);
                }
                List<ExamInwardDetails> element = new List<ExamInwardDetails>();
                BALExamFormMaster func = new BALExamFormMaster();
                element = func.GetPendingExamInwardList(dataString.ExamMasterId, dataString.PRN, UserId);

                return Return.returnHttp("200", element, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage updateExamInward(ExamInwardDetails dataString)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            
            try
            {
                Int64 UserId = Convert.ToInt64(res.ToString());
                //Int64 UserId = 1;
                string strMessage = string.Empty;
                if (dataString != null)
                {
                    if (dataString.ExamFeesToBePaid == 0)
                    {
                        return Return.returnHttp("201", "Please Enter Exam Fee Amount.", null);
                    }
                    if (dataString.TransactionId == "" || dataString.TransactionId == null)
                    {
                        return Return.returnHttp("201", "Please Enter Transaction No.", null);
                    }
                    SqlCommand cmd = new SqlCommand("ExamInwardUpdate", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.AddWithValue("@Id", dataString.Id);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.AddWithValue("@ExamFeesToBePaid", dataString.ExamFeesToBePaid);
                    cmd.Parameters.AddWithValue("@TransactionId", dataString.TransactionId);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    if (strMessage != "TRUE")
                    {
                        return Return.returnHttp("201", strMessage.ToString(), null);
                    }
                    con.Close(); ;
                    
                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Your data has been modified successfully.";

                    }
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
            //    }

        }
    }
}
