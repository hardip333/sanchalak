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
    public class AdmissionFeesReportByAcademicController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();

        #region AdmissionFeesReportGet
        [HttpPost]
        public HttpResponseMessage AdmissionFeesReportByAcademicGet(AdmissionFeesReportByAcademic AFR)
        {
            //String token = Request.Headers.GetValues("token").FirstOrDefault();
            //TokenOperation ac = new TokenOperation();

            //string res = ac.ValidateToken(token);

            //if (res == "0")
            //{
            //    return Return.returnHttp("0", null, null);
            //}
            try
            {
                SqlCommand cmd = new SqlCommand("AdmissionFeesReportByAcademic", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", AFR.FacultyId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AFR.AcademicYearId);
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;

                List<AdmissionFeesReportByAcademic> ObjListAFR = new List<AdmissionFeesReportByAcademic>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AdmissionFeesReportByAcademic ObjAFR = new AdmissionFeesReportByAcademic();

                        ObjAFR.Id = (((dr["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(dr["Id"].ToString());
                        ObjAFR.ProgrammeInstancePartTermId = (((dr["ProgrammeInstancePartTermId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(dr["ProgrammeInstancePartTermId"].ToString());
                        ObjAFR.FeeCategoryId = (((dr["FeeCategoryId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(dr["FeeCategoryId"].ToString());
                        ObjAFR.FacultyId = (((dr["FacultyId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(dr["FacultyId"].ToString());
                        ObjAFR.AcademicYearId = (((dr["AcademicYearId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(dr["AcademicYearId"].ToString());
                        ObjAFR.IsPublished = (Convert.ToString(dr["IsPublished"])).IsEmpty() ? false : Convert.ToBoolean(dr["IsPublished"]);

                        var IsPublished = dr["IsPublished"];
                        if (IsPublished is DBNull)
                        {
                            IsPublished = "NULL";
                        }
                        else
                        {
                            IsPublished = dr["IsPublished"];
                        }
                        ObjAFR.PublishedStatus = (Convert.ToString(dr["PublishedStatus"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["PublishedStatus"]);
                        ObjAFR.AdmissionFeeStatus = (Convert.ToString(dr["AdmissionFeeStatus"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["AdmissionFeeStatus"]);
                        ObjAFR.InstancePartTermName = (Convert.ToString(dr["InstancePartTermName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["InstancePartTermName"]);
                        ObjAFR.AcademicYearCode = (Convert.ToString(dr["AcademicYearCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["AcademicYearCode"]);
                        ObjAFR.FacultyName = (Convert.ToString(dr["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["FacultyName"]);
                        ObjAFR.FeeCategoryName = (Convert.ToString(dr["FeeCategoryName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["FeeCategoryName"]);
                        ObjAFR.TotalAmount = (Convert.ToDecimal(dr["TotalAmount"]).ToString()).IsEmpty() ? 0 : Convert.ToDecimal(dr["TotalAmount"]);
                        ObjAFR.FeeTypeName = (Convert.ToString(dr["FeeTypeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["FeeTypeName"]);
                        ObjAFR.ProgrammeName = (Convert.ToString(dr["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["ProgrammeName"]);
                        ObjAFR.BranchName = (Convert.ToString(dr["BranchName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["BranchName"]);
                        ObjAFR.FeeTypeName = (Convert.ToString(dr["FeeTypeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["FeeTypeName"]);


                        ObjAFR.IsDeleted = (Convert.ToString(dr["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(dr["IsDeleted"]);

                        ObjListAFR.Add(ObjAFR);

                        count = count + 1;
                        ObjAFR.IndexId = count;
                    }
                }
                return Return.returnHttp("200", ObjListAFR, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region Faculty Get For Drop Down
        [HttpPost]
        public HttpResponseMessage MstFacultyGetForDropDown()
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

                List<MstFaculty> ObjLstFaculty = new List<MstFaculty>();
                BALFacultyList ObjBalFacultyList = new BALFacultyList();
                ObjLstFaculty = ObjBalFacultyList.FacultyListGetForDropDown();

                if (ObjLstFaculty != null)
                    return Return.returnHttp("200", ObjLstFaculty, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
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