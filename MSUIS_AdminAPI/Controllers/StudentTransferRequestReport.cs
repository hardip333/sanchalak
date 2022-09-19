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
    public class StudentTransferRequestController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

        #region Student Transfer Request Report Data 
        [HttpPost]
        public HttpResponseMessage StudentTransferRequestData(StudentTransferRequest ObjTransferReqReport)
        {           

            try
            {                
                /*String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }*/

                SqlCommand cmd = new SqlCommand("StudentTransferRequestDataReport", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgInstPartTermId", ObjTransferReqReport.ProgInstPartTermId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<StudentTransferRequest> ObjLstStudentTransferReqReport = new List<StudentTransferRequest>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        StudentTransferRequest ObjStudentTransferReqReport = new StudentTransferRequest();

                        ObjStudentTransferReqReport.StudentPRN = (Convert.ToString(dr["StudentPRN"])).IsEmpty() ? 0 : Convert.ToInt64(dr["StudentPRN"]); 
                        ObjStudentTransferReqReport.AcademicYearCode = (Convert.ToString(dr["AcademicYearCode"])).IsEmpty() ? "" : Convert.ToString(dr["AcademicYearCode"]);
                        ObjStudentTransferReqReport.SourceInst = (Convert.ToString(dr["SourceInst"])).IsEmpty() ? "" : Convert.ToString(dr["SourceInst"]);
                        ObjStudentTransferReqReport.DestInst = (Convert.ToString(dr["DestInst"])).IsEmpty() ? "" : Convert.ToString(dr["DestInst"]);
                        ObjStudentTransferReqReport.FacultyName = (Convert.ToString(dr["FacultyName"])).IsEmpty() ? "" : Convert.ToString(dr["FacultyName"]);
                        ObjStudentTransferReqReport.InstancePartTermName = (Convert.ToString(dr["InstancePartTermName"])).IsEmpty() ? "" : Convert.ToString(dr["InstancePartTermName"]);
                        ObjStudentTransferReqReport.SourceInstituteRemark = (Convert.ToString(dr["SourceInstituteRemark"])).IsEmpty() ? "" : Convert.ToString(dr["SourceInstituteRemark"]);
                        ObjStudentTransferReqReport.SourceInstituteStatus1 = (Convert.ToString(dr["SourceInstituteStatus1"])).IsEmpty() ? "" : Convert.ToString(dr["SourceInstituteStatus1"]);
                        ObjStudentTransferReqReport.SourceRequestProcessedOnView = (Convert.ToString(dr["SourceRequestProcessedOn"])).IsEmpty() ? "" : Convert.ToString(dr["SourceRequestProcessedOn"]); 
                        ObjStudentTransferReqReport.DestinationInstituteRemark = (Convert.ToString(dr["DestinationInstituteRemark"])).IsEmpty() ? "" : Convert.ToString(dr["DestinationInstituteRemark"]); 
                        ObjStudentTransferReqReport.DestinationInstituteStatus1 = (Convert.ToString(dr["DestinationInstituteStatus1"])).IsEmpty() ? "" : Convert.ToString(dr["DestinationInstituteStatus1"]); 
                        ObjStudentTransferReqReport.DestinationRequestProcessedOnView = (Convert.ToString(dr["DestinationRequestProcessedOn"])).IsEmpty() ? "" : Convert.ToString(dr["DestinationRequestProcessedOn"]); 
                        ObjStudentTransferReqReport.RequestedOnView = (Convert.ToString(dr["RequestedOn"])).IsEmpty() ? "" : Convert.ToString(dr["RequestedOn"]); 
                        ObjStudentTransferReqReport.CreatedOnView = (Convert.ToString(dr["CreatedOn"])).IsEmpty() ? "" : Convert.ToString(dr["CreatedOn"]); 
                        ObjStudentTransferReqReport.IsFeeCategoryChangedSts = (Convert.ToString(dr["IsFeeCategoryChangedSts"])).IsEmpty() ? "" : Convert.ToString(dr["IsFeeCategoryChangedSts"]);
                        ObjStudentTransferReqReport.FeeCategoryName = (Convert.ToString(dr["FeeCategoryName"])).IsEmpty() ? "" : Convert.ToString(dr["FeeCategoryName"]);
                        ObjLstStudentTransferReqReport.Add(ObjStudentTransferReqReport);
                    }

                }
                return Return.returnHttp("200", ObjLstStudentTransferReqReport, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion       
       
    }
}
