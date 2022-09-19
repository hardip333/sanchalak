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
    public class StudentPaymentPendingReportController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        #region ProgrammeInstanceDD
        [HttpPost]
        public HttpResponseMessage ProgrammeInstanceDD(ProgramInstance PI)
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


                SqlCommand Cmd = new SqlCommand("CDDProgrammeGetByFacultyandAcadYear", Con);

                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@FacultyId", PI.FacultyId);
                Cmd.Parameters.AddWithValue("@AcademicYearId", PI.AcademicYearId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<ProgramInstance> ProgInstList = new List<ProgramInstance>();
           
                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ProgramInstance ProgInst = new ProgramInstance();

                        ProgInst.Id = (Convert.ToString(Dt.Rows[i]["Id"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["Id"]);
                        ProgInst.ProgrammeId = (Convert.ToString(Dt.Rows[i]["ProgrammeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);
                        ProgInst.FacultyId = (Convert.ToString(Dt.Rows[i]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        ProgInst.AcademicYearId = (Convert.ToString(Dt.Rows[i]["AcademicYearId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["AcademicYearId"]);
                        ProgInst.InstanceName = (Convert.ToString(Dt.Rows[i]["InstanceName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstanceName"]);
                        ProgInstList.Add(ProgInst);
                    }
                }
                return Return.returnHttp("200", ProgInstList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion
       
        #region StudentPaymentPendingReport
        [HttpPost]
        public HttpResponseMessage StudentPaymentPendingReportGeyById(StudentPaymentPendingReport ObjSPP)
        {
            
            try
            {
                Int32 Count = 0;
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("StudentPaymentPendingReportGetById", Con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@FacultyId", ObjSPP.FacultyId);
                da.SelectCommand.Parameters.AddWithValue("@AcademicYearId", ObjSPP.AcademicYearId);
                da.SelectCommand.Parameters.AddWithValue("@ProgrammeInstanceId", ObjSPP.ProgrammeInstanceId);
                da.SelectCommand.Parameters.AddWithValue("@BranchId", ObjSPP.BranchId);
                da.SelectCommand.Parameters.AddWithValue("@ProgrammeInstancePartId", ObjSPP.ProgrammeInstancePartId);
                da.SelectCommand.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ObjSPP.ProgrammeInstancePartTermId);
                DataTable dt = new DataTable();
                da.Fill(dt);
                List<StudentPaymentPendingReport> ObjLstStudentPayment = new List<StudentPaymentPendingReport>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        StudentPaymentPendingReport ObjStudentPaymentPending = new StudentPaymentPendingReport();
                        ObjStudentPaymentPending.StudentPRN = (Convert.ToString(dt.Rows[i]["PRN"])).IsEmpty() ? 0 : Convert.ToInt64(dt.Rows[i]["PRN"]);
                        ObjStudentPaymentPending.NameAsPerMarksheet = (Convert.ToString(dt.Rows[i]["NameAsPerMarksheet"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["NameAsPerMarksheet"]);
                        ObjStudentPaymentPending.EmailId = (Convert.ToString(dt.Rows[i]["EmailId"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["EmailId"]);
                        ObjStudentPaymentPending.MobileNo = (Convert.ToString(dt.Rows[i]["MobileNo"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["MobileNo"]);
                        ObjStudentPaymentPending.TotalAmount = (Convert.ToString(dt.Rows[i]["TotalAmount"])).IsEmpty() ?"_" : Convert.ToString(dt.Rows[i]["TotalAmount"]);
                        ObjStudentPaymentPending.AmountPaid = (Convert.ToString(dt.Rows[i]["AmountPaid"])).IsEmpty() ? "_": Convert.ToString(dt.Rows[i]["AmountPaid"]);
                        ObjStudentPaymentPending.InstalmentNo = (Convert.ToString(dt.Rows[i]["InstalmentNo"])).IsEmpty() ? 0 : Convert.ToInt64(dt.Rows[i]["InstalmentNo"]);
                        ObjStudentPaymentPending.TotalInstalmentGiven = (Convert.ToString(dt.Rows[i]["TotalInstalmentGiven"])).IsEmpty() ? 0 : Convert.ToInt64(dt.Rows[i]["TotalInstalmentGiven"]);
                        ObjStudentPaymentPending.TotalInstalmentSelected = (Convert.ToString(dt.Rows[i]["TotalInstalmentSelected"])).IsEmpty() ? 0 : Convert.ToInt64(dt.Rows[i]["TotalInstalmentSelected"]);
                        ObjStudentPaymentPending.RemainingInstallment = (Convert.ToString(dt.Rows[i]["RemainingInstallment"])).IsEmpty() ? 0 : Convert.ToInt64(dt.Rows[i]["RemainingInstallment"]);
                        ObjStudentPaymentPending.FacultyName = (Convert.ToString(dt.Rows[i]["FacultyName"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["FacultyName"]);
                        ObjStudentPaymentPending.AcademicYearCode = (Convert.ToString(dt.Rows[i]["AcademicYearCode"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["AcademicYearCode"]);
                        ObjStudentPaymentPending.ProgrammeInstanceName = (Convert.ToString(dt.Rows[i]["ProgrammeInstanceName"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["ProgrammeInstanceName"]);
                        ObjStudentPaymentPending.BranchName = (Convert.ToString(dt.Rows[i]["BranchName"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["BranchName"]);
                        ObjStudentPaymentPending.ProgrammeInstancePartName = (Convert.ToString(dt.Rows[i]["ProgrammeInstancePartName"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["ProgrammeInstancePartName"]);
                        ObjStudentPaymentPending.InstancePartTermName = (Convert.ToString(dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "-" : Convert.ToString(dt.Rows[i]["InstancePartTermName"]);
                        ObjStudentPaymentPending.IsInstalmentSelected = (Convert.ToString(dt.Rows[i]["IsInstalmentSelected"])).IsEmpty() ? "_" : Convert.ToString(dt.Rows[i]["IsInstalmentSelected"]);
                        Count = Count + 1;
                        ObjStudentPaymentPending.IndexId = Count;
                        ObjLstStudentPayment.Add(ObjStudentPaymentPending);

                    }
                    return Return.returnHttp("200", ObjLstStudentPayment, null);
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
