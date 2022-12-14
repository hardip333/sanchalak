using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class ApplicationGroupPreferenceReportController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();
        #region Faculty Get By Id
        [HttpPost]
        public HttpResponseMessage MstFacultyGetbyId(MstFaculty Faculty)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
            String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
            Request.Headers.TryGetValues("InstituteId", out IEnumerable<string> InstituteId1);
            var InstituteId = InstituteId1?.FirstOrDefault();

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

            try
            {

                SqlCommand cmd = new SqlCommand("MstInstituteGetTest", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (InstituteId != null || InstituteId != "0" || InstituteId != "")
                {
                    cmd.Parameters.AddWithValue("@Id", InstituteId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Id", facultyDepartIntituteId);
                }

                List<MstFaculty> ObjLstFaculty = new List<MstFaculty>();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        MstFaculty objFaculty = new MstFaculty();

                        objFaculty.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        objFaculty.FacultyName = Convert.ToString(dt.Rows[i]["FacultyName"]);
                        objFaculty.FacultyCode = Convert.ToString(dt.Rows[i]["FacultyCode"]);
                        objFaculty.InstituteId = Convert.ToInt32(dt.Rows[i]["InstituteId"]);
                        objFaculty.InstituteName = Convert.ToString(dt.Rows[i]["InstituteName"]);


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

        #region AcademicYearId
        [HttpPost]
        public HttpResponseMessage IncAcademicYearGet()
        {
            try
            {

                SqlCommand cmd = new SqlCommand();


                SqlDataAdapter da = new SqlDataAdapter("IncAcademicYearGet", con);

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

        #region ProgrammeList Get By FacultyId AcademicYearId
        [HttpPost]
        public HttpResponseMessage ProgrammeListGetByInstituteAcademicId(ApplicationGroupPreferenceReport ObjGroupPreference)
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


                SqlCommand cmd = new SqlCommand("MstProgrammeGetByFacIdAcadIdForGroupPreference", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InstituteId", ObjGroupPreference.InstituteId);
                cmd.Parameters.AddWithValue("@AcademicYearId", ObjGroupPreference.AcademicYearId);
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<ApplicationGroupPreferenceReport> ObjListGroupPreference = new List<ApplicationGroupPreferenceReport>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ApplicationGroupPreferenceReport objGroupPreference = new ApplicationGroupPreferenceReport();

                        objGroupPreference.Id = Convert.ToInt32(dr["Id"].ToString());
                        objGroupPreference.ProgrammeName = dr["ProgrammeName"].ToString();
                        ObjListGroupPreference.Add(objGroupPreference);
                    }
                }
                return Return.returnHttp("200", ObjListGroupPreference, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region ProgrammeInstancePartTermGetByFacIdAndYearId
        [HttpPost]
        public HttpResponseMessage IncProgramInstancePartTermGetbyInsIdAcadIdProgId(ApplicationGroupPreferenceReport ObjGroupPreference)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("ProgrammeInstancePartTermGetByInsIdAcaIdProgIdForGroupPreference", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@InstituteId", ObjGroupPreference.InstituteId);
                da.SelectCommand.Parameters.AddWithValue("@AcademicYearId", ObjGroupPreference.AcademicYearId);
                da.SelectCommand.Parameters.AddWithValue("@ProgrammeId", ObjGroupPreference.ProgrammeId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<RequiredDocumentsSubmittedByApplicant> ObjListReqDocSubmitted = new List<RequiredDocumentsSubmittedByApplicant>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        RequiredDocumentsSubmittedByApplicant objReqDocSubmitted = new RequiredDocumentsSubmittedByApplicant();
                        objReqDocSubmitted.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        objReqDocSubmitted.InstancePartTermName = Convert.ToString(dt.Rows[i]["InstancePartTermName"]);
                        ObjListReqDocSubmitted.Add(objReqDocSubmitted);

                    }
                    return Return.returnHttp("200", ObjListReqDocSubmitted, null);
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

        #region GroupPreferenceReportGetByProgInstPartTerm Id
        [HttpPost]
        public HttpResponseMessage GroupPreferenceReportGetByProgInstPartTermId(ApplicationGroupPreferenceReport ObjGroupPreference)

        {
            try
            {

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                string resRoleToken = ac.ValidateRoleToken(userRoleToken);
                Int32 Count = 0;

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }


                List<ApplicationGroupPreferenceReport> ObjListGroupPreference = new List<ApplicationGroupPreferenceReport>();

                SqlCommand cmd = new SqlCommand("GroupPreferenceReportGetByPIPTID", con);
                cmd.CommandType = CommandType.StoredProcedure;        
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ObjGroupPreference.ProgrammeInstancePartTermId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ApplicationGroupPreferenceReport objGroupPreference = new ApplicationGroupPreferenceReport();
                        objGroupPreference.ApplicationNo = (Convert.ToString(dt.Rows[i]["ApplicationId"])).IsEmpty()?"_":Convert.ToString(dt.Rows[i]["ApplicationId"]);
                        objGroupPreference.NameAsPerMarksheet = (Convert.ToString(dt.Rows[i]["NameAsPerMarksheet"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["NameAsPerMarksheet"]);
                        objGroupPreference.UserName = (Convert.ToString(dt.Rows[i]["UserName"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["UserName"]);
                        objGroupPreference.EmailId = (Convert.ToString(dt.Rows[i]["EmailId"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["EmailId"]);
                        objGroupPreference.MobileNo = (Convert.ToString(dt.Rows[i]["MobileNo"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["MobileNo"]);
                        objGroupPreference.SocialCategoryName = (Convert.ToString(dt.Rows[i]["SocialCategoryName"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["SocialCategoryName"]);
                        objGroupPreference.EligibleDegreeName = (Convert.ToString(dt.Rows[i]["EligibleDegreeName"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["EligibleDegreeName"]);
                        objGroupPreference.Percentage = (Convert.ToString(dt.Rows[i]["Percentage"])).IsEmpty() ? 0 : Convert.ToDecimal(dt.Rows[i]["Percentage"]);
                        //objGroupPreference.ExamPassCity = (Convert.ToString(dt.Rows[i]["ExamPassCity"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["ExamPassCity"]);
                        //objGroupPreference.PassingYear = (Convert.ToString(dt.Rows[i]["PassingYear"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["PassingYear"]);
                        objGroupPreference.FacultyName = (Convert.ToString(dt.Rows[i]["FacultyName"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["FacultyName"]);
                        objGroupPreference.AcademicYearCode = (Convert.ToString(dt.Rows[i]["AcademicYearCode"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["AcademicYearCode"]);
                        objGroupPreference.ProgrammeName = (Convert.ToString(dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["ProgrammeName"]);
                        objGroupPreference.BranchName = (Convert.ToString(dt.Rows[i]["BranchName"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["BranchName"]);
                        objGroupPreference.InstancePartTermName = (Convert.ToString(dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["InstancePartTermName"]);
                        objGroupPreference.Choice1 = (Convert.ToString(dt.Rows[i]["Choice1"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["Choice1"]);
                        objGroupPreference.Choice2 = (Convert.ToString(dt.Rows[i]["Choice2"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["Choice2"]);
                        objGroupPreference.Choice3 = (Convert.ToString(dt.Rows[i]["Choice3"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["Choice3"]);
                        objGroupPreference.Choice4 = (Convert.ToString(dt.Rows[i]["Choice4"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["Choice4"]);
                        objGroupPreference.Choice5 = (Convert.ToString(dt.Rows[i]["Choice5"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["Choice5"]);
                        objGroupPreference.Choice6 = (Convert.ToString(dt.Rows[i]["Choice6"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["Choice6"]);
                        objGroupPreference.Choice7 = (Convert.ToString(dt.Rows[i]["Choice7"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["Choice7"]);
                        objGroupPreference.Choice8 = (Convert.ToString(dt.Rows[i]["Choice8"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["Choice8"]);
                        objGroupPreference.Choice9 = (Convert.ToString(dt.Rows[i]["Choice9"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["Choice9"]);
                        objGroupPreference.Choice10 = (Convert.ToString(dt.Rows[i]["Choice10"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["Choice10"]);
                        objGroupPreference.Choice11 = (Convert.ToString(dt.Rows[i]["Choice11"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["Choice11"]);
                        objGroupPreference.Choice12 = (Convert.ToString(dt.Rows[i]["Choice12"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["Choice12"]);
                        objGroupPreference.Choice13 = (Convert.ToString(dt.Rows[i]["Choice13"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["Choice13"]);
                        objGroupPreference.Choice14 = (Convert.ToString(dt.Rows[i]["Choice14"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["Choice14"]);
                        objGroupPreference.Choice15 = (Convert.ToString(dt.Rows[i]["Choice15"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["Choice15"]);
                        objGroupPreference.Choice16 = (Convert.ToString(dt.Rows[i]["Choice16"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["Choice16"]);
                        objGroupPreference.Choice17 = (Convert.ToString(dt.Rows[i]["Choice17"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["Choice17"]);
                        objGroupPreference.Choice18 = (Convert.ToString(dt.Rows[i]["Choice18"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["Choice18"]);
                        objGroupPreference.Choice19 = (Convert.ToString(dt.Rows[i]["Choice19"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["Choice19"]);
                        objGroupPreference.Choice20 = (Convert.ToString(dt.Rows[i]["Choice20"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["Choice20"]);
                        objGroupPreference.Choice21 = (Convert.ToString(dt.Rows[i]["Choice21"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["Choice21"]);
                        objGroupPreference.Choice22 = (Convert.ToString(dt.Rows[i]["Choice22"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["Choice22"]);
                        objGroupPreference.Choice23 = (Convert.ToString(dt.Rows[i]["Choice23"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["Choice23"]);
                        objGroupPreference.Choice24 = (Convert.ToString(dt.Rows[i]["Choice24"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["Choice24"]);
                        objGroupPreference.Choice25 = (Convert.ToString(dt.Rows[i]["Choice25"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["Choice25"]);
                        objGroupPreference.Choice26 = (Convert.ToString(dt.Rows[i]["Choice26"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["Choice26"]);
                        objGroupPreference.Choice27 = (Convert.ToString(dt.Rows[i]["Choice27"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["Choice27"]);
                        objGroupPreference.Choice28 = (Convert.ToString(dt.Rows[i]["Choice28"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["Choice28"]);
                        objGroupPreference.Choice29 = (Convert.ToString(dt.Rows[i]["Choice29"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["Choice29"]);
                        objGroupPreference.Choice30 = (Convert.ToString(dt.Rows[i]["Choice30"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["Choice30"]);
                        objGroupPreference.Choice31 = (Convert.ToString(dt.Rows[i]["Choice31"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["Choice31"]);
                      
                       
                        Count = Count + 1;
                        objGroupPreference.IndexId = Count;
                        ObjListGroupPreference.Add(objGroupPreference);


                    }
                    return Return.returnHttp("200", ObjListGroupPreference, null);
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
    }
}
