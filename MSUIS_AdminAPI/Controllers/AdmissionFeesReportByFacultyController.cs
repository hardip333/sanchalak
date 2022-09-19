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
    public class AdmissionFeesReportByFacultyController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        #region AdmissionFeesReportByFacultyGet
        [HttpPost]
        public HttpResponseMessage AdmissionFeesReportByFacultyGet(AdmissionFeesReportByFaculty AFR)
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
                SqlCommand cmd = new SqlCommand("AdmissionFeesReportByFaculty", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", AFR.FacultyId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AFR.AcademicYearId);
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;

                List<AdmissionFeesReportByFaculty> ObjListAFR = new List<AdmissionFeesReportByFaculty>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AdmissionFeesReportByFaculty ObjAFR = new AdmissionFeesReportByFaculty();

                        ObjAFR.Id = (((dr["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(dr["Id"].ToString());
                        ObjAFR.ProgrammeInstancePartTermId = (((dr["ProgrammeInstancePartTermId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(dr["ProgrammeInstancePartTermId"].ToString());
                        ObjAFR.FeeCategoryId = (((dr["FeeCategoryId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(dr["FeeCategoryId"].ToString());
                        ObjAFR.FacultyId = (((dr["FacultyId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(dr["FacultyId"].ToString());
                        ObjAFR.InstituteId = (((dr["InstituteId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(dr["InstituteId"].ToString());
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
                        ObjAFR.InstituteName = (Convert.ToString(dr["InstituteName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["InstituteName"]);
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