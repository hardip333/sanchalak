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
    public class FacultyWiseCategoryReportController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region FacultyWiseCategoryReportGetbyFId
        [HttpPost]
        public HttpResponseMessage FacultyWiseCategoryReportGetbyFId(FacultyWiseCategoryReport FacCat)
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

                Int64 FacultyId = Convert.ToInt64(FacCat.FacultyId);
                Int64 AcademicYearId = Convert.ToInt64(FacCat.AcademicYearId);
                Int32 Count = 0;

                SqlCommand cmd = new SqlCommand("FacultyWiseCategoryReportGetByFacultyId", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<FacultyWiseCategoryReport> ObjLstFacultyCatReport = new List<FacultyWiseCategoryReport>();
              
                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        FacultyWiseCategoryReport objFacultyCatReport = new FacultyWiseCategoryReport();
                        //objFacultyCatReport.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objFacultyCatReport.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objFacultyCatReport.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        objFacultyCatReport.ProgrammeCode = (Convert.ToString(Dt.Rows[i]["ProgrammeCode"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["ProgrammeCode"]);
                        objFacultyCatReport.ProgrammeModeName = (Convert.ToString(Dt.Rows[i]["ProgrammeModeName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["ProgrammeModeName"]);
                        objFacultyCatReport.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["BranchName"]);
                        objFacultyCatReport.InstancePartTermName = (Convert.ToString(Dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                        objFacultyCatReport.PartName = (Convert.ToString(Dt.Rows[i]["PartName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["PartName"]);
                        objFacultyCatReport.PartTermName = (Convert.ToString(Dt.Rows[i]["PartTermName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["PartTermName"]);
                        objFacultyCatReport.FemalePH = (Convert.ToString(Dt.Rows[i]["FemalePH"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["FemalePH"]);
                        objFacultyCatReport.MalePH = (Convert.ToString(Dt.Rows[i]["MalePH"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["MalePH"]);
                        objFacultyCatReport.FemaleGeneral = (Convert.ToString(Dt.Rows[i]["FemaleGeneral"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["FemaleGeneral"]);
                        objFacultyCatReport.FemaleSC = (Convert.ToString(Dt.Rows[i]["FemaleSC"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["FemaleSC"]);
                        objFacultyCatReport.FemaleST = (Convert.ToString(Dt.Rows[i]["FemaleST"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["FemaleST"]);
                        objFacultyCatReport.FemaleSEBC = (Convert.ToString(Dt.Rows[i]["FemaleSEBC"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["FemaleSEBC"]);
                        objFacultyCatReport.TotalFemale = (Convert.ToString(Dt.Rows[i]["TotalFemale"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["TotalFemale"]);
                        objFacultyCatReport.MaleGeneral = (Convert.ToString(Dt.Rows[i]["MaleGeneral"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["MaleGeneral"]);
                        objFacultyCatReport.MaleSC = (Convert.ToString(Dt.Rows[i]["MaleSC"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["MaleSC"]);
                        objFacultyCatReport.MaleST = (Convert.ToString(Dt.Rows[i]["MaleST"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["MaleST"]);
                        objFacultyCatReport.MaleSEBC = (Convert.ToString(Dt.Rows[i]["MaleSEBC"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["MaleSEBC"]);
                        objFacultyCatReport.TotalMale = (Convert.ToString(Dt.Rows[i]["TotalMale"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["TotalMale"]);
                        objFacultyCatReport.TotalStudent = (Convert.ToString(Dt.Rows[i]["TotalStudent"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["TotalStudent"]);
                        Count = Count + 1;
                        objFacultyCatReport.IndexId = Count;

                        ObjLstFacultyCatReport.Add(objFacultyCatReport);
                    }
                    return Return.returnHttp("200", ObjLstFacultyCatReport, null);
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

        #region AcademicYearGet
        [HttpPost]
        public HttpResponseMessage AcademicYearGet()
        {
            try
            {
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

        #region MstFacultyGet
        [HttpPost]
        public HttpResponseMessage MstFacultyGet()
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
                SqlCommand cmd = new SqlCommand("MstFacultyGet", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<MstFaculty> ObjLstFaculty = new List<MstFaculty>();
                Dt.Rows.Add("0", "All Institute");
                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        MstFaculty ObjInst = new MstFaculty();
                        ObjInst.Id = Convert.ToInt32(dr["Id"]);
                        ObjInst.FacultyName = (dr["FacultyName"].ToString());

                        ObjLstFaculty.Add(ObjInst);
                    }

                }

                return Return.returnHttp("200", ObjLstFaculty, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region Faculty Get By Id
        [HttpPost]
        public HttpResponseMessage MstFacultyGetbyId(MstFaculty Faculty)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
            String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
            Request.Headers.TryGetValues("InstituteId", out IEnumerable<string> InstituteId1);
            var InstituteId = InstituteId1?.FirstOrDefault();

            //String InstituteId = Request.Headers.GetValues("InstituteId").FirstOrDefault();

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
               
                SqlCommand cmd = new SqlCommand("MstInstituteGetTest", Con);
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
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstFaculty objFaculty = new MstFaculty();

                        objFaculty.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objFaculty.FacultyName = Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objFaculty.FacultyCode = Convert.ToString(Dt.Rows[i]["FacultyCode"]);
                        objFaculty.InstituteId = Convert.ToInt32(Dt.Rows[i]["InstituteId"]);
                        objFaculty.InstituteName = Convert.ToString(Dt.Rows[i]["InstituteName"]);
                        objFaculty.FacultyCode = Convert.ToString(Dt.Rows[i]["InstituteCode"]);
                        //objFaculty.FacultyAddress = Convert.ToString(Dt.Rows[i]["FacultyAddress"]);
                        //objFaculty.CityName = Convert.ToString(Dt.Rows[i]["CityName"]);
                        //objFaculty.Pincode = Convert.ToInt32(Dt.Rows[i]["Pincode"]);
                        //objFaculty.FacultyContactNo = Convert.ToString(Dt.Rows[i]["FacultyContactNo"]);
                        //objFaculty.FacultyFaxNo = Convert.ToString(Dt.Rows[i]["FacultyFaxNo"]);
                        //objFaculty.FacultyEmail = Convert.ToString(Dt.Rows[i]["FacultyEmail"]);
                        //objFaculty.FacultyUrl = Convert.ToString(Dt.Rows[i]["FacultyUrl"]);

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


    }
}