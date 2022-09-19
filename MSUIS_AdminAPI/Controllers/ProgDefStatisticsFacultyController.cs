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
using System.Net.Mail;
using System.Web.Http;

namespace MSUISApi.Controllers
{
    public class ProgDefStatisticsFacultyController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        #region ProgDefStatisticsFacultyGet
        [HttpPost]
        public HttpResponseMessage ProgDefStatisticsFacultyGet(ProgrammeDefinationStatisticsFaculty ObjProgDefStat)
        {
            ProgrammeDefinationStatisticsFaculty modelobj = new ProgrammeDefinationStatisticsFaculty();

            try
            {
                modelobj.FacultyId = ObjProgDefStat.FacultyId;
                modelobj.AcademicYearId = ObjProgDefStat.AcademicYearId;
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

                SqlCommand cmd = new SqlCommand("ProgrammeDefinationStatisticsFaculty", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", modelobj.FacultyId);
                cmd.Parameters.AddWithValue("@AcademicYearId", modelobj.AcademicYearId);
                sda.SelectCommand = cmd;
                sda.Fill(Dt);
                Int32 count = 0;
                List<ProgrammeDefinationStatisticsFaculty> ObjLstPDS = new List<ProgrammeDefinationStatisticsFaculty>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        ProgrammeDefinationStatisticsFaculty ObjPDS = new ProgrammeDefinationStatisticsFaculty();

                        //ObjAS.ProgrammeInstancePartTermId = Convert.ToInt32(dr["ProgrammeInstancePartTermId"]);

                        var ProgrammeInstancePartTermId = dr["ProgrammeInstancePartTermId"];
                        if (ProgrammeInstancePartTermId is DBNull)
                        {
                            ProgrammeInstancePartTermId = 0;
                            ObjPDS.ProgrammeInstancePartTermId = Convert.ToInt32(ProgrammeInstancePartTermId);

                        }
                        else
                        {
                            ObjPDS.ProgrammeInstancePartTermId = Convert.ToInt32(dr["ProgrammeInstancePartTermId"]);

                        }
                        ObjPDS.InstancePartTermName = (dr["InstancePartTermName"].ToString());
                        ObjPDS.FacultyName = (dr["FacultyName"].ToString());
                        ObjPDS.AcademicYearCode = (dr["AcademicYearCode"].ToString());
                        ObjPDS.VerficationStatus = (dr["VerficationStatus"].ToString());
                        ObjPDS.LaunchStatus = (dr["LaunchStatus"].ToString());
                        ObjPDS.CompleteStatusFlag1 = Convert.ToBoolean(dr["CompleteStatusFlag1"]);
                        ObjPDS.CompleteStatusFlag2 = Convert.ToBoolean(dr["CompleteStatusFlag2"]);
                        if (ObjPDS.CompleteStatusFlag1 == true && ObjPDS.CompleteStatusFlag2 == true)
                        {
                            ObjPDS.CompleteStatus = "Complete";
                        }
                        else
                        {
                            ObjPDS.CompleteStatus = "InComplete";
                        }
                        ObjPDS.VerficationStatusAssessment = (dr["VerficationStatusAssessment"].ToString());
                        ObjPDS.LaunchStatusAssessment = (dr["LaunchStatusAssessment"].ToString());
                        ObjPDS.CompleteStatusAssessmentFlag1 = Convert.ToBoolean(dr["CompleteStatusAssessmentFlag1"]);
                        ObjPDS.CompleteStatusAssessmentFlag2 = Convert.ToBoolean(dr["CompleteStatusAssessmentFlag2"]);
                        if (ObjPDS.CompleteStatusAssessmentFlag1 == true && ObjPDS.CompleteStatusAssessmentFlag2 == true)
                        {
                            ObjPDS.CompleteStatusAssessment = "Complete";
                        }
                        else
                        {
                            ObjPDS.CompleteStatusAssessment = "InComplete";
                        }
                        count = count + 1;
                        ObjPDS.IndexId = count;
                        ObjLstPDS.Add(ObjPDS);
                    }

                }
                return Return.returnHttp("200", ObjLstPDS, null);

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
                //dynamic Jsondata = Faculty;

                // Int32 Id = Convert.ToInt32(Faculty.Id);


                SqlCommand cmd = new SqlCommand("MstInstituteGetTest", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", facultyDepartIntituteId);
                List<MstFaculty> ObjLstFaculty = new List<MstFaculty>();
                sda.SelectCommand = cmd;
                sda.Fill(Dt);
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