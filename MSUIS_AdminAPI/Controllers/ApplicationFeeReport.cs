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

namespace MSUISApi.Controllers
{
    public class ApplicationFeeReportController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

        #region Application Fee Report Data By Acad Id
        [HttpPost]
        public HttpResponseMessage ApplicationFeeReportDataByAcadId(FeeCategoryPartTermMap ObjFeeReport)
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

                SqlCommand cmd = new SqlCommand("GetDataForApplicationFeeReport", con);
                cmd.CommandType = CommandType.StoredProcedure;                
                cmd.Parameters.AddWithValue("@Flag", "ReportDataByAcadId");
                cmd.Parameters.AddWithValue("@AcadId", ObjFeeReport.AcademicYearId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<FeeCategoryPartTermMap> ObjLstFeeReport = new List<FeeCategoryPartTermMap>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        FeeCategoryPartTermMap ObjFeeReport1 = new FeeCategoryPartTermMap();

                        ObjFeeReport1.InstancePartTermName = (dr["InstancePartTermName"].ToString());
                        ObjFeeReport1.FeeCategoryName = Convert.ToString(dr["FeeCategoryName"]);
                        ObjFeeReport1.TotalAmount = Convert.ToInt32(dr["TotalAmount"]);
                        ObjFeeReport1.IsPublishedSts = Convert.ToString(dr["IsPublishedSts"]);
                        ObjFeeReport1.FacultyName = Convert.ToString(dr["FacultyName"]);
                        count = count + 1;
                        ObjFeeReport1.IndexId = count;
                        ObjLstFeeReport.Add(ObjFeeReport1);
                    }

                }
                return Return.returnHttp("200", ObjLstFeeReport, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region Application Fee Report Data By Acad Id And Institute Id
        [HttpPost]
        public HttpResponseMessage ApplicationFeeReportDataByAcadIdAndInstId(FeeCategoryPartTermMap ObjFeeReport)
        {
           
            try
            {               
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("GetDataForApplicationFeeReport", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "ReportDataByInstituteId");
                cmd.Parameters.AddWithValue("@AcadId", ObjFeeReport.AcademicYearId);
                cmd.Parameters.AddWithValue("@InstituteId", facultyDepartIntituteId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;

                List<FeeCategoryPartTermMap> ObjLstFeeReport = new List<FeeCategoryPartTermMap>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        FeeCategoryPartTermMap ObjFeeReport1 = new FeeCategoryPartTermMap();

                        ObjFeeReport1.InstancePartTermName = (dr["InstancePartTermName"].ToString());
                        ObjFeeReport1.FeeCategoryName = Convert.ToString(dr["FeeCategoryName"]);
                        ObjFeeReport1.TotalAmount = Convert.ToInt32(dr["TotalAmount"]);
                        ObjFeeReport1.IsPublishedSts = Convert.ToString(dr["IsPublishedSts"]);
                        count = count + 1;
                        ObjFeeReport1.IndexId = count;
                        ObjLstFeeReport.Add(ObjFeeReport1);
                    }

                }
                return Return.returnHttp("200", ObjLstFeeReport, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion
       
    }
}
