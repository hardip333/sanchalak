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
    public class RefundStudentReportForFacultyController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlCommand cmd = new SqlCommand();


        #region RefundStudentsReportForFacultyGet
        [HttpPost]
        public HttpResponseMessage RefundStudentsReportForFacultyGet(RefundStudentReportForFaculty RSR)
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

                //Int32 InstituteId = Convert.ToInt32(RSR.InstituteId);

                SqlCommand Cmd = new SqlCommand("RefundStudentsReportForFaculty", Con);
                Cmd.CommandType = CommandType.StoredProcedure;

                
                Cmd.Parameters.AddWithValue("@InstituteId", RSR.AdmittedInstituteId);

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


                SqlCommand cmd = new SqlCommand("MstInstituteGetTest", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", facultyDepartIntituteId);
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