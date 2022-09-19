using MSUIS_TokenManager.App_Start;
using MSUISApi.BAL;
using MSUISApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;


namespace MSUISApi.Controllers
{
    public class ProvisionallyEligibleStudentsReportController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();

        #region IncProgrammeInstancePartTermGetByFIdAIdGet
        [HttpPost]
        public HttpResponseMessage IncProgrammeInstancePartTermGetByFIdAIdGet(ApplicationList AL)
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

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                List<ApplicationList> slist = new List<ApplicationList>();

                cmd.Parameters.AddWithValue("@FacultyId", AL.FacultyId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AL.AcademicYearId);
                cmd.Parameters.AddWithValue("@InstituteId", AL.InstituteId);
                cmd.CommandText = "IncProgrammeInstancePartTermGetByFIdAId";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow DR in dt.Rows)
                {
                    ApplicationList s = new ApplicationList();

                    s.InstancePartTermId = Convert.ToInt32(DR["Id"].ToString());
                    s.InstancePartTermName = DR["InstancePartTermName"].ToString();

                    slist.Add(s);
                }
                return Return.returnHttp("200", slist, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region ApplicationListGet
        [HttpGet]
        public HttpResponseMessage ApplicationListGet(ApplicationList AL)
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

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                List<ApplicationList> slist = new List<ApplicationList>();

                cmd.Parameters.AddWithValue("@FacultyId", AL.FacultyId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AL.AcademicYearId);

                cmd.CommandText = "ApplicationListGetPT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow DR in dt.Rows)
                {
                    ApplicationList s = new ApplicationList();

                    s.InstancePartTermId = Convert.ToInt32(DR["Id"].ToString());
                    s.InstancePartTermName = DR["InstancePartTermName"].ToString();

                    slist.Add(s);
                }
                return Return.returnHttp("200", slist, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region ProvisionallyEligibleStudentsReportGet
        [HttpPost]
        public HttpResponseMessage ProvisionallyEligibleStudentsReportGet(ProvisionallyEligibleStudentsReport ObjPESR)
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

                try
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable();

                    List<ProvisionallyEligibleStudentsReport> alist = new List<ProvisionallyEligibleStudentsReport>();
                    Int32 count = 0;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ObjPESR.ProgrammeInstancePartTermId);
                    cmd.Parameters.AddWithValue("@InstituteId", ObjPESR.InstituteId);
                    cmd.CommandText = "ProvisionallyEligibleStudentsReport";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    foreach (DataRow DR in dt.Rows)
                    {
                        ProvisionallyEligibleStudentsReport p = new ProvisionallyEligibleStudentsReport();

                        p.PRN = Convert.ToInt64(DR["PRN"]);
                        p.ApplicationId = Convert.ToString(DR["ApplicationId"]);
                        p.ProgrammeInstancePartTermId = Convert.ToInt64(DR["ProgrammeInstancePartTermId"]);
                        p.InstituteId = Convert.ToInt32(DR["InstituteId"]);
                        p.InstancePartTermName = (Convert.ToString(DR["InstancePartTermName"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["InstancePartTermName"]);
                        p.FullName = (Convert.ToString(DR["FullName"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["FullName"]);
                        p.InstituteName = (Convert.ToString(DR["InstituteName"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["InstituteName"]);
                        p.FacultyName = (Convert.ToString(DR["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["FacultyName"]);
                        p.AcademicYearCode = (Convert.ToString(DR["AcademicYearCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["AcademicYearCode"]);
                        p.FacultyRemarks = (Convert.ToString(DR["FacultyRemarks"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["FacultyRemarks"]);

                        p.AcademicsRemarks = (Convert.ToString(DR["AcademicsRemarks"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["AcademicsRemarks"]);
                        p.PRNGeneratedOn = (Convert.ToString(DR["PRNGeneratedOn"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["PRNGeneratedOn"]);
                        p.EligibilityByAcademics = (Convert.ToString(DR["EligibilityByAcademics"])).IsEmpty() ? "Not Defined" : Convert.ToString(DR["EligibilityByAcademics"]);
                        if (DR["EligibilityByAcademics"].ToString() == "Provisionally_Eligible")
                        {
                            p.EligibilityByAcademics = "Provisionally Eligible";
                        }
                        //Boolean EligibilityByAcademics1 = Convert.ToBoolean(p.EligibilityByAcademics1);


                        //if (p.EligibilityByAcademics1 == true)
                        //{
                        //    p.EligibilityByAcademics = "Eligible";
                        //}
                        //else if(p.EligibilityByAcademics1 == false)
                        //{
                        //    p.EligibilityByAcademics = "Provisionally_Eligible";
                        //}

                        count = count + 1;
                        p.IndexId = count;


                        alist.Add(p);
                    }
                    return Return.returnHttp("200", alist, null);
                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message.ToString(), null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region ProvisionallyEligibleStudentsReportEdit
        [HttpPost]
        public HttpResponseMessage ProvisionallyEligibleStudentsReportEdit(JObject jsonobject)

        {
            ProvisionallyEligibleStudentsReport pesr = new ProvisionallyEligibleStudentsReport();
            dynamic jsonData = jsonobject;
            try
            {
                pesr.ProvisionEligibleStudent = Convert.ToString(jsonData.ProvisionEligibleStudent);
                pesr.AcademicsRemarks = Convert.ToString(jsonData.AcademicsRemarks);

                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                pesr.UserId = Convert.ToInt64(responseToken);

                SqlCommand cmd = new SqlCommand("ProvisionallyEligibleStudentsReportEditNew", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProvisionEligibleStudent", pesr.ProvisionEligibleStudent);
                cmd.Parameters.AddWithValue("@AcademicsRemarks", pesr.AcademicsRemarks);
                cmd.Parameters.AddWithValue("@UserId", pesr.UserId);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();
                if (strMessage == "TRUE")
                {
                    strMessage = "Your Data Has Been Updated";
                }
                return Return.returnHttp("200", strMessage, null);

                //String token = Request.Headers.GetValues("token").FirstOrDefault();
                //TokenOperation ac = new TokenOperation();

                //string res = ac.ValidateToken(token);

                //if (res == "0")
                //{
                //    return Return.returnHttp("0", null, null);
                //}

                //DataTable ProvEligibleStudent = new DataTable();
                ////PaperList.Columns.Add(new DataColumn("Id", typeof(Int64)));
                //ProvEligibleStudent.Columns.Add("Id", typeof(Int64));
                //ProvEligibleStudent.Columns.Add("EligibilityByAcademics", typeof(string));
                //ProvEligibleStudent.Columns.Add("AdminRemarkByAcademics", typeof(string));
                //ProvEligibleStudent.Columns.Add("UserId", typeof(Int64));
                //ProvEligibleStudent.Columns.Add("UserTime", typeof(DateTime));
                ////int i = 0;
                //foreach (ProvisionallyEligibleStudentsReport p in ObjProg)
                //{


                //    ProvEligibleStudent.Rows.Add(p.ApplicationId
                //                            , p.EligibilityByAcademics
                //                            , p.AcademicsRemarks
                //                            , res
                //                            , datetime);
                //    //i = i + 1;

                //}
                //SqlTransaction ST;
                //SqlCommand CMD = new SqlCommand();

                //CMD = new SqlCommand("ProvisionallyEligibleStudentsReportEdit", con);
                //CMD.CommandType = CommandType.StoredProcedure;
                //CMD.Parameters.AddWithValue("@ProvEligble", ProvEligibleStudent);
                //CMD.Parameters.AddWithValue("@UserId", res);
                //CMD.Parameters.AddWithValue("@UserTime", datetime);
                //CMD.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                //CMD.Parameters["@Message"].Direction = ParameterDirection.Output;

                //con.Open();
                //ST = con.BeginTransaction();
                //try
                //{
                //    CMD.Transaction = ST;
                //    int ans = CMD.ExecuteNonQuery();
                //    String msg = Convert.ToString(CMD.Parameters["@Message"].Value);



                //    ST.Commit();
                //    return Return.returnHttp("200", msg, null);
                //    //return true;
                //}
                //catch (SqlException sqlError)
                //{
                //    ST.Rollback();
                //    String strMessageErr = Convert.ToString(sqlError);
                //    return Return.returnHttp("201", strMessageErr, null);
                //    //return false;
                //}
            }
            catch (Exception e)
            {

                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region InstituteGetById
        [HttpPost]
        public HttpResponseMessage InstituteGetById(MstInstitute MF)
        {
            //MstFaculty modelobj = new MstFaculty(); 
            try
            {
                //modelobj.Id = MF.Id;
                SqlCommand cmd = new SqlCommand("MstIntGetById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InstituteId", MF.Id);
                sda.SelectCommand = cmd;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                List<MstInstitute> ObjLstF = new List<MstInstitute>();
                //dt.Rows.Add("0", "All Faculty");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstInstitute ObjF = new MstInstitute();
                        ObjF.Id = Convert.ToInt32(dr["Id"]);
                        ObjF.InstituteName = (dr["InstituteName"].ToString());

                        ObjLstF.Add(ObjF);
                    }

                }

                return Return.returnHttp("200", ObjLstF, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

    }
}