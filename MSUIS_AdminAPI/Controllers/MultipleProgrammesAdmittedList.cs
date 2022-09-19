using MSUIS_TokenManager.App_Start;
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
using System.Net.Mail;
using System.Text;
using System.Web.Http;

namespace MSUISApi.Controllers
{
    public class MultipleProgrammesAdmittedListController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region AcademicYearList
        [HttpPost]
        public HttpResponseMessage GetAcademicYearList(AcademicYearList ObjAcademicYear)
        {
            AcademicYearList modelobj = new AcademicYearList();

            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                SqlCommand cmd = new SqlCommand("IncAcademicYearGet", con);
                cmd.CommandType = CommandType.StoredProcedure;

                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<AcademicYearList> ObjAcademic = new List<AcademicYearList>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AcademicYearList ObjAC = new AcademicYearList();
                        ObjAC.Id = Convert.ToInt32(dr["Id"]);
                        ObjAC.AcademicYearCode = Convert.ToString(dr["AcademicYearCode"]);

                        ObjAcademic.Add(ObjAC);
                    }

                }
                return Return.returnHttp("200", ObjAcademic, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion 

        #region GetMultipleProgrammesAdmittedList
        [HttpPost]
        public HttpResponseMessage GetMultipleProgrammesAdmittedList(AdmittedApplicantList ObjAdmittedApplicantList)
        {
            AdmittedApplicantList modelobj = new AdmittedApplicantList();

            try
            {
                modelobj.AcademicYearId = ObjAdmittedApplicantList.AcademicYearId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("MultipleProgrammesAdmittedList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AcademicYearId", modelobj.AcademicYearId);

                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<AdmittedApplicantList> AdmittedData = new List<AdmittedApplicantList>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AdmittedApplicantList ObjAAL = new AdmittedApplicantList();
                        ObjAAL.ApplicationId = Convert.ToInt64(dr["ApplicationId"]);
                        ObjAAL.StudentName = Convert.ToString(dr["StudentName"]);
                        ObjAAL.MobileNo = Convert.ToString(dr["MobileNo"]);
                        ObjAAL.EmailId = Convert.ToString(dr["EmailId"]);
                        ObjAAL.AdmittedFaculty = Convert.ToString(dr["AdmittedFaculty"]);
                        ObjAAL.AdmittedCourse = Convert.ToString(dr["AdmittedCourse"]);
                        ObjAAL.EligibleApplicationid = Convert.ToInt64(dr["EligibleApplicationid"]);
                        ObjAAL.EligibleCourseFaculty = Convert.ToString(dr["EligibleCourseFaculty"]);
                        ObjAAL.EligibleCourse = Convert.ToString(dr["EligibleCourse"]);
                        ObjAAL.AdmittedParttermId = Convert.ToInt64(dr["AdmittedParttermId"]);
                        ObjAAL.EligiblePartTermId = Convert.ToInt64(dr["EligiblePartTermId"]);
                        //ObjAAL.PRN = Convert.ToInt64(dr["PRN"]);
                        //ObjAAL.IsPRNGenerated = Convert.ToBoolean(dr["IsPRNGenerated"]);

                        //var IsPRNGenerated = dr["IsPRNGenerated"];
                        //if (IsPRNGenerated is DBNull)
                        //{
                        //    ObjAAL.IsPRNGenerated = null;
                        //}
                        //else
                        //{
                        //    ObjAAL.IsPRNGenerated = Convert.ToBoolean(dr["IsPRNGenerated"]);
                        //}

                        count = count + 1;
                        ObjAAL.IndexId = count;
                        AdmittedData.Add(ObjAAL);
                    }

                }
                return Return.returnHttp("200", AdmittedData, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

    }
}
