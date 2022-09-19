using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class ApplicantAcademicVerificationPendingReportController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        #region AcademicYearGet
        [HttpPost]
        public HttpResponseMessage AcademicYearGet()
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

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("IncAcademicYearGet", Con);

                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<AcademicYear> ObjAYList = new List<AcademicYear>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AcademicYear ObjAY = new AcademicYear();
                        ObjAY.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjAY.AcademicYearCode = Convert.ToString(dt.Rows[i]["AcademicYearCode"]);
                        ObjAYList.Add(ObjAY);
                    }
                    return Return.returnHttp("200", ObjAYList, null);
                }
                else
                {
                    return Return.returnHttp("201", "No Record Found", null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region FacultyGet
        [HttpPost]
        public HttpResponseMessage FacultyGet()
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

                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlCommand Cmd = new SqlCommand("MstFacultyGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);


                List<MstFaculty> ObjLstFaculty = new List<MstFaculty>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstFaculty objFaculty = new MstFaculty();
                        if (Dt.Rows[i]["Id"].ToString() != "")
                        {
                            objFaculty.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);

                        }
                        if (Dt.Rows[i]["FacultyName"].ToString() != "")
                        {
                            objFaculty.FacultyName = Convert.ToString(Dt.Rows[i]["FacultyName"]);

                        }
                        if (Dt.Rows[i]["FacultyCode"].ToString() != "")
                        {
                            objFaculty.FacultyCode = Convert.ToString(Dt.Rows[i]["FacultyCode"]);

                        }
                        if (Dt.Rows[i]["FacultyAddress"].ToString() != "")
                        {
                            objFaculty.FacultyAddress = Convert.ToString(Dt.Rows[i]["FacultyAddress"]);

                        }
                        if (Dt.Rows[i]["CityName"].ToString() != "")
                        {
                            objFaculty.CityName = Convert.ToString(Dt.Rows[i]["CityName"]);

                        }
                        if (Dt.Rows[i]["Pincode"].ToString() != "")
                        {
                            objFaculty.Pincode = Convert.ToInt32(Dt.Rows[i]["Pincode"]);

                        }
                        if (Dt.Rows[i]["FacultyContactNo"].ToString() != "")
                        {
                            objFaculty.FacultyContactNo = Convert.ToString(Dt.Rows[i]["FacultyContactNo"]);

                        }
                      
                        ObjLstFaculty.Add(objFaculty);
                    }
                }
                return Return.returnHttp("200", ObjLstFaculty, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region ApplicantAcademicVerificationPendingListGetbyFacultyIdAcademicYearId
        [HttpPost]
        public HttpResponseMessage ApplicantAcademicVerificationPendingListGetbyFIdAId(ApplicantAcademicVerificationPendingReport AAPR)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();            
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
                Int32 Count = 0;
                Int64 FacultyId = Convert.ToInt64(AAPR.FacultyId);
                Int64 AcademicYearId = Convert.ToInt64(AAPR.AcademicYearId);

                SqlCommand cmd = new SqlCommand("ApplicantAcademicVerificationPendingList", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<ApplicantAcademicVerificationPendingReport> ObjLstAAVPR = new List<ApplicantAcademicVerificationPendingReport>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ApplicantAcademicVerificationPendingReport objAAVPR = new ApplicantAcademicVerificationPendingReport();

                       
                        objAAVPR.Id = (Convert.ToString(Dt.Rows[i]["Id"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["Id"]);

                        objAAVPR.NameAsPerMarksheet = (Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"]);
                        objAAVPR.InstancePartTermName = (Convert.ToString(Dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                        objAAVPR.EligibilityByAcademics = (Convert.ToString(Dt.Rows[i]["EligibilityByAcademics"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["EligibilityByAcademics"]);
                        objAAVPR.AdminRemarkByAcademics = (Convert.ToString(Dt.Rows[i]["AdminRemarkByAcademics"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["AdminRemarkByAcademics"]);
                        objAAVPR.ApprovedOnAcademics = (Convert.ToString(Dt.Rows[i]["ApprovedOnAcademics"])).IsEmpty() ? "-" : string.Format("{0: dd-MM-yyyy}", (Dt.Rows[i]["ApprovedOnAcademics"]));

                        Count = Count + 1;
                        objAAVPR.IndexId = Count;


                        ObjLstAAVPR.Add(objAAVPR);
                    }
                }
                else
                {
                    return Return.returnHttp("201", "No Record Found", null);
                }
                return Return.returnHttp("200", ObjLstAAVPR, null);
               
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion
    }
}
