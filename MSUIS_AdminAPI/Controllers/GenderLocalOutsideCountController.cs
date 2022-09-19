using System;
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
using Newtonsoft.Json.Linq;
using MSUISApi.BAL;

namespace MSUISApi.Controllers
{
    public class GenderLocalOutsideCountController : ApiController
    
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();

        #region GenderLocalOutsideCount
        [HttpPost]
        public HttpResponseMessage GenderLocalOutsideCountGet(GenderLocalOutsideCount ObjGen)
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
                SqlCommand cmd = new SqlCommand("GenderLocalOutSideReportGetAcademicYearFacId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", ObjGen.FacultyId);
                cmd.Parameters.AddWithValue("@AcademicYearId", ObjGen.AcademicYearId);
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;

                List<GenderLocalOutsideCount> ObjListGen = new List<GenderLocalOutsideCount>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        GenderLocalOutsideCount ObjGenL = new GenderLocalOutsideCount();

                        //ObjGenL.FacultyId = (((dr["FacultyId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(dr["FacultyId"].ToString());
                        //ObjGenL.AcademicYearId = (((dr["AcademicYearId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(dr["AcademicYearId"].ToString());
                        ObjGenL.Year = (Convert.ToString(dr["Year"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["Year"]);

                        ObjGenL.LocalVadodaraMale = (Convert.ToString(dr["LocalVadodaraMale"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["LocalVadodaraMale"]);
                        ObjGenL.OutSideVadodaraMale = (Convert.ToString(dr["OutSideVadodaraMale"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["OutSideVadodaraMale"]);

                        ObjGenL.LocalGujaratMale = (Convert.ToString(dr["LocalGujaratMale"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["LocalGujaratMale"]);
                        ObjGenL.OutSideGujaratMale = (Convert.ToString(dr["OutSideGujaratMale"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["OutSideGujaratMale"]);

                        ObjGenL.LocalVadodaraFemale = (Convert.ToString(dr["LocalVadodaraFemale"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["LocalVadodaraFemale"]);
                        ObjGenL.OutSideVadodaraFemale = (Convert.ToString(dr["OutSideVadodaraFemale"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["OutSideVadodaraFemale"]);

                        ObjGenL.LocalGujaratFemale = (Convert.ToString(dr["LocalGujaratFemale"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["LocalGujaratFemale"]);
                        ObjGenL.OutSideGujaratFemale = (Convert.ToString(dr["OutSideGujaratFemale"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["OutSideGujaratFemale"]);

                        ObjGenL.LocalVadodaraTransGender = (Convert.ToString(dr["LocalVadodaraTransGender"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["LocalVadodaraTransGender"]);
                        ObjGenL.OutSideVadodaraTransGender = (Convert.ToString(dr["OutSideVadodaraTransGender"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["OutSideVadodaraTransGender"]);

                        ObjGenL.LocalGujaratTransgender = (Convert.ToString(dr["LocalGujaratTransgender"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["LocalGujaratTransgender"]);
                        ObjGenL.OutSideGujaratTransgender = (Convert.ToString(dr["OutSideGujaratTransgender"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["OutSideGujaratTransgender"]);

                        ObjGenL.TotalStudents = (Convert.ToString(dr["TotalStudents"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["TotalStudents"]);
                        ObjGenL.Year = (Convert.ToString(dr["Year"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["Year"]);

                        ObjListGen.Add(ObjGenL);

                        count = count + 1;
                        ObjGenL.IndexId = count;
                    }
                }
                return Return.returnHttp("200", ObjListGen, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region MstFacultyGet
        [HttpPost]
        public HttpResponseMessage FacultyGetById(MstFaculty MF)
        {
            //MstFaculty modelobj = new MstFaculty(); 
            try
            {
                //modelobj.Id = MF.Id;
                SqlCommand cmd = new SqlCommand("MstFacGetbyId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", MF.Id);
                DataTable Dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(Dt);
                List<MstFaculty> ObjLstF = new List<MstFaculty>();
                //dt.Rows.Add("0", "All Faculty");
                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        MstFaculty ObjF = new MstFaculty();
                        ObjF.Id = Convert.ToInt32(dr["Id"]);
                        ObjF.FacultyName = (dr["FacultyName"].ToString());

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

        #region AcademicYearGet For DropDown
        [HttpPost]
        public HttpResponseMessage AcademicYearGetForDropDown()
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                List<AcademicYear> ObjListAcadYear = new List<AcademicYear>();
                BALAcademicYear ObjBalAcadYearList = new BALAcademicYear();
                ObjListAcadYear = ObjBalAcadYearList.AcademicYearGetForDropDown();

                if (ObjListAcadYear != null)
                    return Return.returnHttp("200", ObjListAcadYear, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion



    }
}