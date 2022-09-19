using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web.Http;


namespace MSUISApi.Controllers
{
    public class GetStudentStatisticsController : ApiController
    {

        // GET: GetApplicationStatusInfo
        readonly SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        readonly SqlDataAdapter Da = new SqlDataAdapter();
        private DataTable Dt = new DataTable();
        readonly DataSet Ds = new DataSet();
        //Validation validation = new Validation();

        #region GeneralStats
        [HttpPost]
        public HttpResponseMessage MstGeneralStat(GeneralStatusInfo app)
        {
            try
            {

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                String res = ac.ValidateToken(token);
                String resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int64 UserId = Convert.ToInt64(res.ToString());
                Int64 RoleId = Convert.ToInt64(resRoleToken.ToString());

                SqlCommand Cmd = new SqlCommand("GetSubjectListForUser", Con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                Da.SelectCommand = Cmd;

                Cmd.Parameters.AddWithValue("@RoleId", RoleId);
                Cmd.Parameters.AddWithValue("@UserId", UserId);
                Cmd.Parameters.AddWithValue("@ModifiedInstituteId", app.ModifiedInstituteId);
                Cmd.Parameters.AddWithValue("@ModifiedSubjectId", app.ModifiedSubjectId);

                //Opening Connection To get ApplicationStats 
                Con.Open();
                Cmd.ExecuteNonQuery();
                Con.Close();
                //Closing Connection To get ApplicationStats 

                Da.Fill(Dt);

                List<int> SubjectList = new List<int>();
                List<int> InstList = new List<int>();


                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        SubjectList.Add((Dt.Rows[i]["SubjectId"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[i]["SubjectId"]));
                        InstList.Add((Dt.Rows[i]["InstituteId"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[i]["InstituteId"]));

                    }
                }

                DataTable SubInstDt = new DataTable();
                SubInstDt.Columns.Add(new DataColumn("SubjectId", typeof(int)));
                SubInstDt.Columns.Add(new DataColumn("InstituteId", typeof(int)));

                //foreach (var subject in SubjectList)
                for (int subject = 0, inst = 0; inst < Dt.Rows.Count; subject++, inst++)
                {
                    SubInstDt.Rows.Add(SubjectList[subject], InstList[inst]);
                }

                Da.Dispose();
                Dt.Clear();


                Cmd = new SqlCommand("GetGeneralInfo", Con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                Da.SelectCommand = Cmd;

                // these next lines are important to map the C# DataTable object to the correct SQL User Defined Type

                SqlParameter sl = Cmd.Parameters.AddWithValue("@SubInstList", SubInstDt);
                sl.SqlDbType = SqlDbType.Structured;
                sl.TypeName = "dbo.SubInstList";
                Cmd.Parameters.AddWithValue("@AcademicYearCode", app.AcademicYear);
                Cmd.Parameters.AddWithValue("@RoleId", RoleId);
                Cmd.Parameters.AddWithValue("@ModifiedInstituteId", app.ModifiedInstituteId);
                Cmd.Parameters.AddWithValue("@ModifiedSubjectId", app.ModifiedSubjectId);

                //Opening Connection To get ApplicationStats 
                Con.Open();
                Cmd.ExecuteNonQuery();
                Con.Close();
                //Closing Connection To get ApplicationStats 

                Da.Fill(Ds);

                //Passing Paraemters End
                Dt = Ds.Tables[0];
                app.TotalStudents = (Dt.Rows[0]["TotalStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["TotalStudents"]);
                //app.TotalTeachers = Convert.ToInt32(Dt.Rows[0]["TotalTeachers"]);
                app.TotalInstitutes = (Dt.Rows[0]["TotalInstitutes"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["TotalInstitutes"]);
                app.TotalSubjects = (Dt.Rows[0]["TotalSubjects"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["TotalSubjects"]);
                app.TotalPrograms = (Dt.Rows[0]["TotalPrograms"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["TotalPrograms"]);
                //app.TotalStudentAchivements = Convert.ToInt32(Dt.Rows[0]["TotalStudentAchivements"]);
                //app.TotalTeacherAchivements = Convert.ToInt32(Dt.Rows[0]["TotalTeacherAchivements"]);

                List<FacultyList> facultyList = new List<FacultyList>();
                if ((RoleId == 7) && app.ModifiedInstituteId == 0 && app.ModifiedSubjectId == 0)
                {

                    Dt = Ds.Tables[1];
                    if (Dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < Dt.Rows.Count; j++)
                        {
                            FacultyList faculty = new FacultyList();
                            faculty.FacultyId = Convert.ToInt32(Dt.Rows[j]["Id"]);
                            faculty.FacultyName = Convert.ToString(Dt.Rows[j]["InstituteName"]);
                            facultyList.Add(faculty);
                        }

                    }

                    app.Facultylist = facultyList;

                    Dt = Ds.Tables[2];
                    List<CollegeList> collegeList = new List<CollegeList>();
                    if (Dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < Dt.Rows.Count; j++)
                        {
                            CollegeList college = new CollegeList();
                            college.CollegeId = Convert.ToInt32(Dt.Rows[j]["Id"]);
                            college.CollegeName = Convert.ToString(Dt.Rows[j]["InstituteName"]);
                            collegeList.Add(college);
                        }

                    }
                    app.Collegelist = collegeList;

                }
                else if (((RoleId == 7) && app.ModifiedInstituteId != 0 && app.ModifiedSubjectId == 0) || ((RoleId == 3) && app.ModifiedInstituteId == 0 && app.ModifiedSubjectId == 0))
                {
                    List<SubjectList> subjectlist = new List<SubjectList>();
                    Dt = Ds.Tables[1];
                    if (Dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < Dt.Rows.Count; j++)
                        {
                            SubjectList subject = new SubjectList();
                            subject.SubjectId = (Dt.Rows[j]["Id"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[j]["Id"]);
                            subject.SubjectName = (Dt.Rows[j]["SubjectName"] is DBNull) ? "" : Convert.ToString(Dt.Rows[j]["SubjectName"]);
                            subjectlist.Add(subject);
                        }

                    }
                    app.Subjectlist = subjectlist;

                }

                Da.Dispose();
                Dt.Clear();


                //List

                List<GeneralStatusInfo> ObjLstGeneralStatusInfo = new List<GeneralStatusInfo>
                {
                    app
                };

                return Return.returnHttp("200", ObjLstGeneralStatusInfo, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion


        #region Application Status

        [HttpPost]
        public HttpResponseMessage MstStudentApplicationStat(ApplicationStatusInfo app)
        {
            try
            {

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                String res = ac.ValidateToken(token);
                String resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int64 UserId = Convert.ToInt64(res.ToString());
                Int64 RoleId = Convert.ToInt64(resRoleToken.ToString());


                SqlCommand Cmd = new SqlCommand("GetSubjectListForUser", Con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                Da.SelectCommand = Cmd;

                Cmd.Parameters.AddWithValue("@RoleId", RoleId);
                Cmd.Parameters.AddWithValue("@UserId", UserId);
                Cmd.Parameters.AddWithValue("@ModifiedInstituteId", app.ModifiedInstituteId);
                Cmd.Parameters.AddWithValue("@ModifiedSubjectId", app.ModifiedSubjectId);

                //Opening Connection To get ApplicationStats 
                Con.Open();
                Cmd.ExecuteNonQuery();
                Con.Close();
                //Closing Connection To get ApplicationStats 

                Da.Fill(Dt);

                List<int> SubjectList = new List<int>();
                List<int> InstList = new List<int>();


                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        SubjectList.Add((Dt.Rows[i]["SubjectId"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[i]["SubjectId"]));
                        InstList.Add((Dt.Rows[i]["InstituteId"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[i]["InstituteId"]));

                    }
                }

                DataTable SubInstDt = new DataTable();
                SubInstDt.Columns.Add(new DataColumn("SubjectId", typeof(int)));
                SubInstDt.Columns.Add(new DataColumn("InstituteId", typeof(int)));

                //foreach (var subject in SubjectList)
                for (int subject = 0, inst = 0; inst < Dt.Rows.Count; subject++, inst++)
                {
                    SubInstDt.Rows.Add(SubjectList[subject], InstList[inst]);
                }

                Da.Dispose();
                Dt.Clear();


                Cmd = new SqlCommand("GetApplicationStatusInfo", Con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                Da.SelectCommand = Cmd;

                // these next lines are important to map the C# DataTable object to the correct SQL User Defined Type

                SqlParameter sl = Cmd.Parameters.AddWithValue("@SubInstList", SubInstDt);
                sl.SqlDbType = SqlDbType.Structured;
                sl.TypeName = "dbo.SubInstList";
                Cmd.Parameters.AddWithValue("@AcademicYearCode", app.AcademicYear);
                Cmd.Parameters.AddWithValue("@RoleId", RoleId);

                //Opening Connection To get ApplicationStats 
                Con.Open();
                Cmd.ExecuteNonQuery();
                Con.Close();
                //Closing Connection To get ApplicationStats 

                Da.Fill(Dt);

                //Passing Paraemters End

                app.TotalApplications = (Dt.Rows[0]["TotalApplication"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["TotalApplication"]);
                app.ApprovedApplications = (Dt.Rows[0]["ApprovedApplication"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["ApprovedApplication"]);
                app.TotalCancelledorRejectedApplications = (Dt.Rows[0]["TotalApplicationCancelledorRejected"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["TotalApplicationCancelledorRejected"]);
                app.TotalPendingApplications = (Dt.Rows[0]["TotalPendingApplications"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["TotalPendingApplications"]);
                app.RejectedApplicationsByAcademics = (Dt.Rows[0]["ApplicationRejectedByAcademics"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["ApplicationRejectedByAcademics"]);
                app.RejectedApplicationsByFaculty = (Dt.Rows[0]["ApplicationRejectedByFaculty"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["ApplicationRejectedByFaculty"]);
                app.CancelledApplicationsByStudent = (Dt.Rows[0]["ApplicationCancelledByStudent"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["ApplicationCancelledByStudent"]);
                app.ApplicationFeesPaid = (Dt.Rows[0]["ApplicationFeesPaid"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["ApplicationFeesPaid"]);
                app.ApplicationFeesNotPaid = app.TotalApplications - app.ApplicationFeesPaid;
                app.PendingVerificationByFaculty = (Dt.Rows[0]["PendingVerificationByFaculty"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["PendingVerificationByFaculty"]);
                app.PendingStatusByFaculty = (Dt.Rows[0]["PendingStatusByFaculty"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["PendingStatusByFaculty"]);
                app.PendingVerificationByAcademics = (Dt.Rows[0]["PendingVerificationByAcademics"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["PendingVerificationByAcademics"]);
                app.PendingStatusByAcademics = (Dt.Rows[0]["PendingApprovementByAcademics"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["PendingApprovementByAcademics"]);

                Da.Dispose();
                Dt.Clear();


                //List

                List<ApplicationStatusInfo> ObjLstApplicationStatusInfo = new List<ApplicationStatusInfo>
                {
                    app
                };

                return Return.returnHttp("200", ObjLstApplicationStatusInfo, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        public HttpResponseMessage MstStudentApplicationDetails(ApplicationStudentInfo app)
        {
            try
            {

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                String res = ac.ValidateToken(token);
                String resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int64 UserId = Convert.ToInt64(res.ToString());
                Int64 RoleId = Convert.ToInt64(resRoleToken.ToString());

                SqlCommand Cmd = new SqlCommand("GetSubjectListForUser", Con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                Da.SelectCommand = Cmd;

                Cmd.Parameters.AddWithValue("@RoleId", RoleId);
                Cmd.Parameters.AddWithValue("@UserId", UserId);
                Cmd.Parameters.AddWithValue("@ModifiedInstituteId", app.ModifiedInstituteId);
                Cmd.Parameters.AddWithValue("@ModifiedSubjectId", app.ModifiedSubjectId);

                //Opening Connection To get ApplicationStats 
                Con.Open();
                Cmd.ExecuteNonQuery();
                Con.Close();
                //Closing Connection To get ApplicationStats 

                Da.Fill(Dt);

                List<int> SubjectList = new List<int>();
                List<int> InstList = new List<int>();


                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        SubjectList.Add((Dt.Rows[i]["SubjectId"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[i]["SubjectId"]));
                        InstList.Add((Dt.Rows[i]["InstituteId"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[i]["InstituteId"]));

                    }
                }

                DataTable SubInstDt = new DataTable();
                SubInstDt.Columns.Add(new DataColumn("SubjectId", typeof(int)));
                SubInstDt.Columns.Add(new DataColumn("InstituteId", typeof(int)));

                //foreach (var subject in SubjectList)
                for (int subject = 0, inst = 0; inst < Dt.Rows.Count; subject++, inst++)
                {
                    SubInstDt.Rows.Add(SubjectList[subject], InstList[inst]);
                }

                Da.Dispose();
                Dt.Clear();


                Cmd = new SqlCommand("GetApplicationStudentInfo", Con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                Da.SelectCommand = Cmd;

                // these next lines are important to map the C# DataTable object to the correct SQL User Defined Type

                SqlParameter sl = Cmd.Parameters.AddWithValue("@SubInstList", SubInstDt);
                sl.SqlDbType = SqlDbType.Structured;
                sl.TypeName = "dbo.SubInstList";
                Cmd.Parameters.AddWithValue("@AcademicYearCode", app.AcademicYear);
                Cmd.Parameters.AddWithValue("@RoleId", RoleId);
                Cmd.Parameters.AddWithValue("@ApplicationFlag", app.ApplicationFlag);

                //Opening Connection To get ApplicationStats 
                Con.Open();
                Cmd.ExecuteNonQuery();
                Con.Close();
                //Closing Connection To get ApplicationStats 

                Da.Fill(Dt);

                //Passing Paraemters End

                List<StatStudentList> studentlists = new List<StatStudentList>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        StatStudentList student = new StatStudentList();
                        student.PRN = (Dt.Rows[i]["UserName"] is DBNull) ? 0 : Convert.ToInt64(Dt.Rows[i]["UserName"]);
                        student.Name = (Dt.Rows[i]["NameAsPerMarksheet"] is DBNull) ? "" : Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"]);
                        student.Gender = (Dt.Rows[i]["Gender"] is DBNull) ? "" : Convert.ToString(Dt.Rows[i]["Gender"]);
                        student.InstancePartTermName = (Dt.Rows[i]["InstancePartTermName"] is DBNull) ? "" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                        student.InstituteName = (Dt.Rows[i]["InstituteName"] is DBNull) ? "" : Convert.ToString(Dt.Rows[i]["InstituteName"]);
                        student.MobileNo = (Dt.Rows[i]["MobileNo"] is DBNull) ? "" : Convert.ToString(Dt.Rows[i]["MobileNo"]);
                        student.EmailId = (Dt.Rows[i]["EmailId"] is DBNull) ? "" : Convert.ToString(Dt.Rows[i]["EmailId"]);

                        studentlists.Add(student);
                    }
                }



                app.Studentlist = studentlists;

                //Get-AdmissionStudent-Info End

                Da.Dispose();
                Dt.Clear();

                //List

                List<ApplicationStudentInfo> ObjLstApplicationStudentInfo = new List<ApplicationStudentInfo>
                {
                    app
                };

                return Return.returnHttp("200", ObjLstApplicationStudentInfo, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion


        #region Admission Status
        [HttpPost]
        public HttpResponseMessage MstStudentAdmissionStat(AdmissionStatusInfo app)
        {
            try
            {

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                String res = ac.ValidateToken(token);
                String resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int64 UserId = Convert.ToInt64(res.ToString());
                Int64 RoleId = Convert.ToInt64(resRoleToken.ToString());


                SqlCommand Cmd = new SqlCommand("GetSubjectListForUser", Con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                Da.SelectCommand = Cmd;

                Cmd.Parameters.AddWithValue("@RoleId", RoleId);
                Cmd.Parameters.AddWithValue("@UserId", UserId);
                Cmd.Parameters.AddWithValue("@ModifiedInstituteId", app.ModifiedInstituteId);
                Cmd.Parameters.AddWithValue("@ModifiedSubjectId", app.ModifiedSubjectId);

                //Opening Connection To get ApplicationStats 
                Con.Open();
                Cmd.ExecuteNonQuery();
                Con.Close();
                //Closing Connection To get ApplicationStats 

                Da.Fill(Dt);

                List<int> SubjectList = new List<int>();
                List<int> InstList = new List<int>();


                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        SubjectList.Add((Dt.Rows[i]["SubjectId"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[i]["SubjectId"]));
                        InstList.Add((Dt.Rows[i]["InstituteId"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[i]["InstituteId"]));

                    }
                }

                DataTable SubInstDt = new DataTable();
                SubInstDt.Columns.Add(new DataColumn("SubjectId", typeof(int)));
                SubInstDt.Columns.Add(new DataColumn("InstituteId", typeof(int)));

                //foreach (var subject in SubjectList)
                for (int subject = 0, inst = 0; inst < Dt.Rows.Count; subject++, inst++)
                {
                    SubInstDt.Rows.Add(SubjectList[subject], InstList[inst]);
                }

                Da.Dispose();
                Dt.Clear();


                Cmd = new SqlCommand("GetAdmissionStatusInfo", Con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                Da.SelectCommand = Cmd;

                // these next lines are important to map the C# DataTable object to the correct SQL User Defined Type

                SqlParameter sl = Cmd.Parameters.AddWithValue("@SubInstList", SubInstDt);
                sl.SqlDbType = SqlDbType.Structured;
                sl.TypeName = "dbo.SubInstList";
                Cmd.Parameters.AddWithValue("@AcademicYearCode", app.AcademicYear);
                Cmd.Parameters.AddWithValue("@RoleId", RoleId);

                //Opening Connection To get ApplicationStats 
                Con.Open();
                Cmd.ExecuteNonQuery();
                Con.Close();
                //Closing Connection To get ApplicationStats 

                Da.Fill(Dt);

                //Passing Paraemters End

                app.TotalEligible = (Dt.Rows[0]["TotalEligible"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["TotalEligible"]);
                app.TotalProvisionallyEligible = (Dt.Rows[0]["TotalProvisionallyEligible"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["TotalProvisionallyEligible"]);
                app.TotalPRNGenerated = (Dt.Rows[0]["TotalPRNGeneratedApplication"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["TotalPRNGeneratedApplication"]);
                app.TotalPRNNotGenerated = app.TotalEligible + app.TotalProvisionallyEligible - app.TotalPRNGenerated;
                app.TotalPaperSelected = (Dt.Rows[0]["TotalPaperSelected"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["TotalPaperSelected"]);
                app.TotalPaperNotSelected = app.TotalPRNGenerated - app.TotalPaperSelected;
                app.TotalAdmittedStudents = app.TotalEligible + app.TotalProvisionallyEligible;

                app.StudentFeesPaid = (Dt.Rows[0]["StudentFeesPaid"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["StudentFeesPaid"]);
                app.StudentPaidFullyFees = (Dt.Rows[0]["StudentPaidFullyFees"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["StudentPaidFullyFees"]);
                app.StudentPaidPartialAdmissionFees = (Dt.Rows[0]["StudentPaidPartialAdmissionFees"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["StudentPaidPartialAdmissionFees"]);
                app.StudentNotPaidAdmissionFees = (Dt.Rows[0]["StudentNotPaidAdmissionFees"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["StudentNotPaidAdmissionFees"]);

                app.MaleStudents = (Dt.Rows[0]["MaleStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["MaleStudents"]);
                app.FemaleStudents = (Dt.Rows[0]["FemaleStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["FemaleStudents"]);
                app.OtherStudents = (Dt.Rows[0]["OtherStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["OtherStudents"]);

                app.NRIStudents = (Dt.Rows[0]["NRIStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["NRIStudents"]);
                app.LocalStudents = (Dt.Rows[0]["LocalStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["LocalStudents"]);
                app.PhysicallyChallengedStudents = (Dt.Rows[0]["PhysicallyChallengedStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["PhysicallyChallengedStudents"]);

                app.GENERALStudents = (Dt.Rows[0]["GENERALStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["GENERALStudents"]);
                app.EWSStudents = (Dt.Rows[0]["EWSStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["EWSStudents"]);
                app.SEBCStudents = (Dt.Rows[0]["SEBCStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["SEBCStudents"]);
                app.SCStudents = (Dt.Rows[0]["SCStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["SCStudents"]);
                app.STStudents = (Dt.Rows[0]["STStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["STStudents"]);

                app.ExServiceStudents = (Dt.Rows[0]["ExServiceStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["ExServiceStudents"]);
                app.ActServiceStudents = (Dt.Rows[0]["ActServiceStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["ActServiceStudents"]);
                app.WFFStudents = (Dt.Rows[0]["WFFStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["WFFStudents"]);
                app.WPTStudents = (Dt.Rows[0]["WPTStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["WPTStudents"]);
                app.WSTStudents = (Dt.Rows[0]["WSTStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["WSTStudents"]);
                app.DDWStudents = (Dt.Rows[0]["DDWStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["DDWStudents"]);
                app.MPAFStudents = (Dt.Rows[0]["MPAFStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["MPAFStudents"]);
                app.EAFStudents = (Dt.Rows[0]["EAFStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["EAFStudents"]);
                app.FAFStudents = (Dt.Rows[0]["FAFStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["FAFStudents"]);
                app.RTAStudents = (Dt.Rows[0]["RTAStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["RTAStudents"]);
                app.KMStudents = (Dt.Rows[0]["KMStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["KMStudents"]);
                app.TFWStudents = (Dt.Rows[0]["TFWStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["TFWStudents"]);
                app.DPStudents = (Dt.Rows[0]["DPStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["DPStudents"]);
                app.MSUBSQStudents = (Dt.Rows[0]["MSUBSQStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["MSUBSQStudents"]);
                app.NAStudents = (Dt.Rows[0]["NAStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["NAStudents"]);
                app.SQStudents = (Dt.Rows[0]["SQStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["SQStudents"]);

                app.APosBloodGroupStudents = (Dt.Rows[0]["APosBloodGroupStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["APosBloodGroupStudents"]);
                app.ANegBloodGroupStudents = (Dt.Rows[0]["ANegBloodGroupStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["ANegBloodGroupStudents"]);
                app.BPosBloodGroupStudents = (Dt.Rows[0]["BPosBloodGroupStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["BPosBloodGroupStudents"]);
                app.BNegBloodGroupStudents = (Dt.Rows[0]["BNegBloodGroupStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["BNegBloodGroupStudents"]);
                app.OPosBloodGroupStudents = (Dt.Rows[0]["OPosBloodGroupStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["OPosBloodGroupStudents"]);
                app.ONegBloodGroupStudents = (Dt.Rows[0]["ONegBloodGroupStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["ONegBloodGroupStudents"]);
                app.ABPosBloodGroupStudents = (Dt.Rows[0]["ABPosBloodGroupStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["ABPosBloodGroupStudents"]);
                app.ABNegBloodGroupStudents = (Dt.Rows[0]["ABNegBloodGroupStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["ABNegBloodGroupStudents"]);

                //Get-AdmissionStudent-Info End

                Da.Dispose();
                Dt.Clear();

                //List

                List<AdmissionStatusInfo> ObjLstAdmissionStatusInfo = new List<AdmissionStatusInfo>
                {
                    app
                };

                return Return.returnHttp("200", ObjLstAdmissionStatusInfo, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage MstStudentAdmissionDetails(AdmissionStudentInfo app)
        {
            try
            {

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                String res = ac.ValidateToken(token);
                String resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                Int64 UserId = Convert.ToInt64(res.ToString());
                Int64 RoleId = Convert.ToInt64(resRoleToken.ToString());



                SqlCommand Cmd = new SqlCommand("GetSubjectListForUser", Con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                Da.SelectCommand = Cmd;

                Cmd.Parameters.AddWithValue("@RoleId", RoleId);
                Cmd.Parameters.AddWithValue("@UserId", UserId);
                Cmd.Parameters.AddWithValue("@ModifiedInstituteId", app.ModifiedInstituteId);
                Cmd.Parameters.AddWithValue("@ModifiedSubjectId", app.ModifiedSubjectId);

                //Opening Connection To get AdmissionStats 
                Con.Open();
                Cmd.ExecuteNonQuery();
                Con.Close();
                //Closing Connection To get AdmissionStats 

                Da.Fill(Dt);

                List<int> SubjectList = new List<int>();
                List<int> InstList = new List<int>();


                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        SubjectList.Add((Dt.Rows[i]["SubjectId"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[i]["SubjectId"]));
                        InstList.Add((Dt.Rows[i]["InstituteId"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[i]["InstituteId"]));

                    }
                }

                DataTable SubInstDt = new DataTable();
                SubInstDt.Columns.Add(new DataColumn("SubjectId", typeof(int)));
                SubInstDt.Columns.Add(new DataColumn("InstituteId", typeof(int)));

                //foreach (var subject in SubjectList)
                for (int subject = 0, inst = 0; inst < Dt.Rows.Count; subject++, inst++)
                {
                    SubInstDt.Rows.Add(SubjectList[subject], InstList[inst]);
                }

                Da.Dispose();
                Dt.Clear();


                Cmd = new SqlCommand("GetAdmissionStudentInfo", Con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                Da.SelectCommand = Cmd;

                // these next lines are important to map the C# DataTable object to the correct SQL User Defined Type

                SqlParameter sl = Cmd.Parameters.AddWithValue("@SubInstList", SubInstDt);
                sl.SqlDbType = SqlDbType.Structured;
                sl.TypeName = "dbo.SubInstList";
                Cmd.Parameters.AddWithValue("@AcademicYearCode", app.AcademicYear);
                Cmd.Parameters.AddWithValue("@RoleId", RoleId);
                Cmd.Parameters.AddWithValue("@AdmissionFlag", app.AdmissionFlag);

                //Opening Connection To get AdmissionStats 
                Con.Open();
                Cmd.ExecuteNonQuery();
                Con.Close();
                //Closing Connection To get AdmissionStats 

                Da.Fill(Dt);

                //Passing Paraemters End

                List<StatStudentList> AdmStudentlist = new List<StatStudentList>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        StatStudentList AdmStudent = new StatStudentList();

                        AdmStudent.PRN = (Dt.Rows[i]["PRN"] is DBNull) ? 0 : Convert.ToInt64(Dt.Rows[i]["PRN"]);
                        AdmStudent.Name = (Dt.Rows[i]["NameAsPerMarksheet"] is DBNull) ? "" : Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"]);
                        AdmStudent.Gender = (Dt.Rows[i]["Gender"] is DBNull) ? "" : Convert.ToString(Dt.Rows[i]["Gender"]);
                        AdmStudent.InstancePartTermName = (Dt.Rows[i]["InstancePartTermName"] is DBNull) ? "" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                        AdmStudent.InstituteName = (Dt.Rows[i]["InstituteName"] is DBNull) ? "" : Convert.ToString(Dt.Rows[i]["InstituteName"]);
                        AdmStudent.MobileNo = (Dt.Rows[i]["MobileNo"] is DBNull) ? "" : Convert.ToString(Dt.Rows[i]["MobileNo"]);
                        AdmStudent.EmailId = (Dt.Rows[i]["EmailId"] is DBNull) ? "" : Convert.ToString(Dt.Rows[i]["EmailId"]);

                        if (app.AdmissionFlag == "PhysicallyChallengedStudents")
                        {
                            AdmStudent.DisabilityPercentage = (Dt.Rows[i]["DisabilityPercentage"] is DBNull) ? 0 : Convert.ToDecimal(Dt.Rows[i]["DisabilityPercentage"]);
                        }

                        AdmStudentlist.Add(AdmStudent);

                    }
                }


                app.Studentlist = AdmStudentlist;

                //Get-AdmissionStudent-Info End

                Da.Dispose();
                Dt.Clear();

                //List

                List<AdmissionStudentInfo> ObjLstAdmissionStudentInfo = new List<AdmissionStudentInfo>
                {
                    app
                };

                return Return.returnHttp("200", ObjLstAdmissionStudentInfo, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion


        #region Pre-Examination Status
        [HttpPost]
        public HttpResponseMessage MstStudentPreExaminationStat(PreExaminationStatusInfo app)
        {
            try
            {

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                String res = ac.ValidateToken(token);
                String resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int64 UserId = Convert.ToInt64(res.ToString());
                Int64 RoleId = Convert.ToInt64(resRoleToken.ToString());


                SqlCommand Cmd = new SqlCommand("GetSubjectListForUser", Con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                Da.SelectCommand = Cmd;

                Cmd.Parameters.AddWithValue("@RoleId", RoleId);
                Cmd.Parameters.AddWithValue("@UserId", UserId);
                Cmd.Parameters.AddWithValue("@ModifiedInstituteId", app.ModifiedInstituteId);
                Cmd.Parameters.AddWithValue("@ModifiedSubjectId", app.ModifiedSubjectId);

                //Opening Connection To get ApplicationStats 
                Con.Open();
                Cmd.ExecuteNonQuery();
                Con.Close();
                //Closing Connection To get ApplicationStats 

                Da.Fill(Dt);

                List<int> SubjectList = new List<int>();
                List<int> InstList = new List<int>();


                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        SubjectList.Add((Dt.Rows[i]["SubjectId"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[i]["SubjectId"]));
                        InstList.Add((Dt.Rows[i]["InstituteId"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[i]["InstituteId"]));

                    }
                }

                DataTable SubInstDt = new DataTable();
                SubInstDt.Columns.Add(new DataColumn("SubjectId", typeof(int)));
                SubInstDt.Columns.Add(new DataColumn("InstituteId", typeof(int)));

                //foreach (var subject in SubjectList)
                for (int subject = 0, inst = 0; inst < Dt.Rows.Count; subject++, inst++)
                {
                    SubInstDt.Rows.Add(SubjectList[subject], InstList[inst]);
                }

                Da.Dispose();
                Dt.Clear();


                Cmd = new SqlCommand("GetPreExaminationStatusInfo", Con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                Da.SelectCommand = Cmd;

                // these next lines are important to map the C# DataTable object to the correct SQL User Defined Type

                SqlParameter sl = Cmd.Parameters.AddWithValue("@SubInstList", SubInstDt);
                sl.SqlDbType = SqlDbType.Structured;
                sl.TypeName = "dbo.SubInstList";
                Cmd.Parameters.AddWithValue("@AcademicYearCode", app.AcademicYear);
                Cmd.Parameters.AddWithValue("@RoleId", RoleId);
                Cmd.Parameters.AddWithValue("@ModifiedExamEventId", app.ModifiedExamEventId);

                //Opening Connection To get ApplicationStats 
                Con.Open();
                Cmd.ExecuteNonQuery();
                Con.Close();
                //Closing Connection To get ApplicationStats 

                Da.Fill(Ds);

                Dt = Ds.Tables[0];


                //Passing Paraemters End


                app.TotalActiveStudents = (Dt.Rows[0]["TotalActiveStudents"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["TotalActiveStudents"]);
                app.ExamFormsGenereated = (Dt.Rows[0]["TotalExamFormsGenerated"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["TotalExamFormsGenerated"]);
                app.ExamFormsNotGenereated = app.TotalActiveStudents - app.ExamFormsGenereated;
                app.PaperSelected = (Dt.Rows[0]["TotalStudentPaperSelected"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["TotalStudentPaperSelected"]);
                app.NotSelectedPaper = app.TotalActiveStudents - app.PaperSelected;

                app.TotalProgrammePartTermExams = (Dt.Rows[0]["TotalProgrammePartTermExams"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["TotalProgrammePartTermExams"]);
                app.TimeTableConfigured = (Dt.Rows[0]["TotalTimeTableConfigured"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["TotalTimeTableConfigured"]);
                app.TimeTableNotConfigured = app.TotalProgrammePartTermExams - app.TimeTableConfigured;

                app.TotalInstitute = (Dt.Rows[0]["TotalInstitute"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["TotalInstitute"]);
                app.TotalExamScheduled = (Dt.Rows[0]["TotalExamScheduled"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["TotalExamScheduled"]);
                app.TotalExamNotScheduled = app.TotalInstitute - app.TotalExamScheduled;

                app.TotalRepeaters = (Dt.Rows[0]["TotalRepeaters"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["TotalRepeaters"]);
                app.CurrentExamEventId = (Dt.Rows[0]["ExamEventId"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[0]["ExamEventId"]);
                app.CurrentExamEventName = Convert.ToString(Dt.Rows[0]["CurrentExamEventName"]);

                Dt = Ds.Tables[1];

                List<PreExaminationStatusExamEventDetails> exameventlist = new List<PreExaminationStatusExamEventDetails>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        PreExaminationStatusExamEventDetails examevents = new PreExaminationStatusExamEventDetails();
                        examevents.ExamEventId = (Dt.Rows[i]["Id"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[i]["Id"]);
                        examevents.ExamEventName = (Dt.Rows[i]["DisplayName"] is DBNull) ? "" : Convert.ToString(Dt.Rows[i]["DisplayName"]);

                        exameventlist.Add(examevents);
                    }
                }

                app.ExamEventDetails = exameventlist;

                Da.Dispose();
                Dt.Clear();



                //List

                List<PreExaminationStatusInfo> ObjLstPreExaminationStatusInfo = new List<PreExaminationStatusInfo>
                {
                    app
                };

                return Return.returnHttp("200", ObjLstPreExaminationStatusInfo, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage MstStudentPreExaminationDetails(PreExaminationStudentInfo app)
        {
            try
            {

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                String res = ac.ValidateToken(token);
                String resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int64 UserId = Convert.ToInt64(res.ToString());
                Int64 RoleId = Convert.ToInt64(resRoleToken.ToString());


                SqlCommand Cmd = new SqlCommand("GetSubjectListForUser", Con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                Da.SelectCommand = Cmd;

                Cmd.Parameters.AddWithValue("@RoleId", RoleId);
                Cmd.Parameters.AddWithValue("@UserId", UserId);
                Cmd.Parameters.AddWithValue("@ModifiedInstituteId", app.ModifiedInstituteId);
                Cmd.Parameters.AddWithValue("@ModifiedSubjectId", app.ModifiedSubjectId);

                //Opening Connection To get ApplicationStats 
                Con.Open();
                Cmd.ExecuteNonQuery();
                Con.Close();
                //Closing Connection To get ApplicationStats 

                Da.Fill(Dt);

                List<int> SubjectList = new List<int>();
                List<int> InstList = new List<int>();


                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        SubjectList.Add((Dt.Rows[i]["SubjectId"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[i]["SubjectId"]));
                        InstList.Add((Dt.Rows[i]["InstituteId"] is DBNull) ? 0 : Convert.ToInt32(Dt.Rows[i]["InstituteId"]));

                    }
                }

                DataTable SubInstDt = new DataTable();
                SubInstDt.Columns.Add(new DataColumn("SubjectId", typeof(int)));
                SubInstDt.Columns.Add(new DataColumn("InstituteId", typeof(int)));

                for (int subject = 0, inst = 0; inst < Dt.Rows.Count; subject++, inst++)
                {
                    SubInstDt.Rows.Add(SubjectList[subject], InstList[inst]);
                }

                Da.Dispose();
                Dt.Clear();


                Cmd = new SqlCommand("GetPreExaminationStudentInfo", Con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                Da.SelectCommand = Cmd;

                // these next lines are important to map the C# DataTable object to the correct SQL User Defined Type

                SqlParameter sl = Cmd.Parameters.AddWithValue("@SubInstList", SubInstDt);
                sl.SqlDbType = SqlDbType.Structured;
                sl.TypeName = "dbo.SubInstList";
                Cmd.Parameters.AddWithValue("@AcademicYearCode", app.AcademicYear);
                Cmd.Parameters.AddWithValue("@RoleId", RoleId);
                Cmd.Parameters.AddWithValue("@PreExaminationFlag", app.PreExaminationFlag);
                Cmd.Parameters.AddWithValue("@ModifiedExamEventId", app.ModifiedExamEventId);

                //Opening Connection To get PreExaminationStats 
                Con.Open();
                Cmd.ExecuteNonQuery();
                Con.Close();
                //Closing Connection To get PreExaminationStats 

                Da.Fill(Dt);

                //Passing Paraemters End

                if (app.PreExaminationFlag == "TotalActiveStudents" || app.PreExaminationFlag == "PaperSelected" || app.PreExaminationFlag == "NotSelectedPaper" || app.PreExaminationFlag == "ExamFormsGenereated" || app.PreExaminationFlag == "ExamFormsNotGenereated" || app.PreExaminationFlag == "TotalRepeaters")
                {
                    // Student Info
                    List<StatStudentList> studentlists = new List<StatStudentList>();

                    if (Dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            StatStudentList student = new StatStudentList();
                            student.PRN = (Dt.Rows[i]["PRN"] is DBNull) ? 0 : Convert.ToInt64(Dt.Rows[i]["PRN"]);
                            student.Name = (Dt.Rows[i]["NameAsPerMarksheet"] is DBNull) ? "" : Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"]);
                            student.Gender = (Dt.Rows[i]["Gender"] is DBNull) ? "" : Convert.ToString(Dt.Rows[i]["Gender"]);
                            student.InstancePartTermName = (Dt.Rows[i]["InstancePartTermName"] is DBNull) ? "" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                            student.InstituteName = (Dt.Rows[i]["InstituteName"] is DBNull) ? "" : Convert.ToString(Dt.Rows[i]["InstituteName"]);
                            student.MobileNo = (Dt.Rows[i]["MobileNo"] is DBNull) ? "" : Convert.ToString(Dt.Rows[i]["MobileNo"]);
                            student.EmailId = (Dt.Rows[i]["EmailId"] is DBNull) ? "" : Convert.ToString(Dt.Rows[i]["EmailId"]);

                            studentlists.Add(student);
                        }
                    }


                    app.Studentlist = studentlists;
                }

                else if (app.PreExaminationFlag == "TotalProgrammePartTermExams")
                {
                    List<PreExaminationTotalProgramExams> programExamsList = new List<PreExaminationTotalProgramExams>();

                    if (Dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            PreExaminationTotalProgramExams programExam = new PreExaminationTotalProgramExams();
                            programExam.InstancePartTermName = (Dt.Rows[i]["InstancePartTermName"] is DBNull) ? "" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                            programExam.InstituteName = (Dt.Rows[i]["InstituteName"] is DBNull) ? "" : Convert.ToString(Dt.Rows[i]["InstituteName"]);
                            programExam.InstituteEmail = (Dt.Rows[i]["InstituteEmail"] is DBNull) ? "" : Convert.ToString(Dt.Rows[i]["InstituteEmail"]);
                            programExam.StartDateOfExam = (Dt.Rows[i]["StartDateOfExam"] is DBNull) ? "" : Convert.ToString(Dt.Rows[i]["StartDateOfExam"]);
                            programExam.PartName = (Dt.Rows[i]["PartName"] is DBNull) ? "" : Convert.ToString(Dt.Rows[i]["PartName"]);
                            programExam.EndDateOfExam = (Dt.Rows[i]["EndDateOfExam"] is DBNull) ? "" : Convert.ToString(Dt.Rows[i]["EndDateOfExam"]);


                            programExamsList.Add(programExam);
                        }
                    }


                    app.TotalProgrammeExams = programExamsList;
                }

                else if (app.PreExaminationFlag == "TimeTableConfigured" || app.PreExaminationFlag == "TimeTableNotConfigured")
                {
                    List<PreExaminationTotalTimeTable> timeTableList = new List<PreExaminationTotalTimeTable>();

                    if (Dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            PreExaminationTotalTimeTable timeTable = new PreExaminationTotalTimeTable();
                            timeTable.InstituteName = (Dt.Rows[i]["InstituteName"] is DBNull) ? "" : Convert.ToString(Dt.Rows[i]["InstituteName"]);
                            timeTable.InstituteEmail = (Dt.Rows[i]["InstituteEmail"] is DBNull) ? "" : Convert.ToString(Dt.Rows[i]["InstituteEmail"]);
                            timeTable.BranchName = (Dt.Rows[i]["BranchName"] is DBNull) ? "" : Convert.ToString(Dt.Rows[i]["BranchName"]);
                            timeTable.PartTermName = (Dt.Rows[i]["PartTermName"] is DBNull) ? "" : Convert.ToString(Dt.Rows[i]["PartTermName"]);
                            timeTable.DisplayName = (Dt.Rows[i]["DisplayName"] is DBNull) ? "" : Convert.ToString(Dt.Rows[i]["DisplayName"]);


                            timeTableList.Add(timeTable);
                        }
                    }



                    app.TotalTimeTable = timeTableList;

                }

                else if (app.PreExaminationFlag == "TotalExamScheduled" || app.PreExaminationFlag == "TotalExamNotScheduled")
                {
                    List<PreExaminationTotalInstituteExams> instituteExamsList = new List<PreExaminationTotalInstituteExams>();

                    if (Dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            PreExaminationTotalInstituteExams instituteExam = new PreExaminationTotalInstituteExams();
                            instituteExam.DisplayName = (Dt.Rows[i]["DisplayName"] is DBNull) ? "" : Convert.ToString(Dt.Rows[i]["DisplayName"]);
                            instituteExam.InstituteEmail = (Dt.Rows[i]["InstituteEmail"] is DBNull) ? "" : Convert.ToString(Dt.Rows[i]["InstituteEmail"]);
                            instituteExam.InstituteType = (Dt.Rows[i]["InstituteType"] is DBNull) ? "" : Convert.ToString(Dt.Rows[i]["InstituteType"]);
                            instituteExam.InstituteName = (Dt.Rows[i]["InstituteName"] is DBNull) ? "" : Convert.ToString(Dt.Rows[i]["InstituteName"]);


                            instituteExamsList.Add(instituteExam);
                        }
                    }

                    app.TotalInstituteExams = instituteExamsList;
                }

                //Get-AdmissionStudent-Info End

                Da.Dispose();
                Dt.Clear();

                //List

                List<PreExaminationStudentInfo> ObjLstApplicationStudentInfo = new List<PreExaminationStudentInfo>
                {
                    app
                };

                return Return.returnHttp("200", ObjLstApplicationStudentInfo, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion


        #region Student All Details



        #endregion

    }
}