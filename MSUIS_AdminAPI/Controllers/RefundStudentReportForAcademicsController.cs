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

namespace MSUISApi.Controllers
{
    public class RefundStudentsReportForAcademicsController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlCommand cmd = new SqlCommand();


        #region RefundStudentsReportForAcademicsGet
        [HttpPost]
        public HttpResponseMessage RefundStudentsReportForAcademicsGet(RefundStudentsReportForAcademics RSR)
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

                //Int32 InstituteId = Convert.ToInt32(RSR.InstituteId);

                SqlCommand Cmd = new SqlCommand("RefundStudentsReportForAcademics", Con);
                Cmd.CommandType = CommandType.StoredProcedure;


                Cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", RSR.ProgrammeInstancePartTermId);

                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                Int32 count = 0;
                List<RefundStudentReportForFaculty> ObjLstRSR = new List<RefundStudentReportForFaculty>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        RefundStudentReportForFaculty ObjRSR = new RefundStudentReportForFaculty();


                        ObjRSR.ApplicationId = (((Dt.Rows[i]["ApplicationId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["ApplicationId"]);

                        ObjRSR.ProgrammeInstancePartTermId = (((Dt.Rows[i]["ProgrammeInstancePartTermId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["ProgrammeInstancePartTermId"]);

                        ObjRSR.AcademicYearCode = (Convert.ToString(Dt.Rows[i]["AcademicYearCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);

                        ObjRSR.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyName"]);

                        ObjRSR.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["BranchName"]);

                        ObjRSR.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);

                        ObjRSR.InstancePartTermName = (Convert.ToString(Dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);


                        ObjRSR.TotalAmount = (Convert.ToString(Dt.Rows[i]["TotalAmount"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["TotalAmount"]);

                        ObjRSR.AmountPaid = (Convert.ToString(Dt.Rows[i]["AmountPaid"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["AmountPaid"]);

                        ObjRSR.RefundAmountByAcademic = (Convert.ToString(Dt.Rows[i]["RefundAmountByAcademic"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["RefundAmountByAcademic"]);

                        ObjRSR.AdmittedInstituteId = (((Dt.Rows[i]["AdmittedInstituteId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["AdmittedInstituteId"]);
                        ObjRSR.RefundBy = (((Dt.Rows[i]["RefundBy"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["RefundBy"]);
                        ObjRSR.RefundedOn = (Convert.ToString(Dt.Rows[i]["RefundedOn"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["RefundedOn"]);
                        //ObjRSR.InstituteName = (Convert.ToString(Dt.Rows[i]["InstituteName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstituteName"]);
                        count = count + 1;
                        ObjRSR.IndexId = count;

                        ObjLstRSR.Add(ObjRSR);



                    }
                    return Return.returnHttp("200", ObjLstRSR, null);

                }
                else
                {
                    return Return.returnHttp("200", "No Record Found", null);
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

                SqlCommand Cmd = new SqlCommand("MstFacultyGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);


                List<MstFaculty> ObjLstFaculty = new List<MstFaculty>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstFaculty objFaculty = new MstFaculty();

                        objFaculty.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objFaculty.FacultyName = Convert.ToString(Dt.Rows[i]["FacultyName"]);

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


        #region PostProgPartTermGetByFacultyId
        [HttpPost]
        public HttpResponseMessage ProgPartTermGetByFacultyId(ProgrammeInstancePartTerm ObjProgInstPartTerm)
        {
            try
            {



                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("IncProgrammeInstancePartTermGetByFacultyId", Con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@FacultyId", ObjProgInstPartTerm.FacultyId);
                DataTable dt = new DataTable();
                da.Fill(dt);



                List<ProgrammeInstancePartTerm> ObjLstProgInstPartTerm = new List<ProgrammeInstancePartTerm>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePartTerm ObjProgPartTerm = new ProgrammeInstancePartTerm();
                        ObjProgPartTerm.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjProgPartTerm.InstancePartTermName = Convert.ToString(dt.Rows[i]["InstancePartTermName"]);



                        ObjLstProgInstPartTerm.Add(ObjProgPartTerm);



                    }
                    return Return.returnHttp("200", ObjLstProgInstPartTerm, null);
                }
                else
                {
                    return Return.returnHttp("200", "No Record Found", null);
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