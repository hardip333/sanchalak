using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;
using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using MSUISApi.BAL;
using Newtonsoft.Json.Linq;

namespace MSUISApi.Controllers
{
    public class GeneralRegisterController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region General Register Report Data Get

        [HttpPost]
        public HttpResponseMessage GeneralRegisterReport(GeneralRegisterReport ObjGenReg)
        {
            try
            {

                //String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String RoleId = Request.Headers.GetValues("roleId").FirstOrDefault();

                Request.Headers.TryGetValues("InstituteId", out IEnumerable<string> InstituteId1);
                var InstituteId = InstituteId1?.FirstOrDefault();

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter cmdda = new SqlDataAdapter("GeneralRegisterDataFacultyWise", con);

                cmdda.SelectCommand.CommandType = CommandType.StoredProcedure;
                cmdda.SelectCommand.Parameters.AddWithValue("@FacultyId", InstituteId);
                cmdda.SelectCommand.Parameters.AddWithValue("@RoleId", RoleId);
                
                DataTable Dt = new DataTable();
                cmdda.Fill(Dt);

                List<GeneralRegisterReport> ObjLstGenRegData = new List<GeneralRegisterReport>();
                Int32 count = 0;
                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        GeneralRegisterReport ObjGenRegData = new GeneralRegisterReport();

                        ObjGenRegData.PRN = (Convert.ToString(Dt.Rows[i]["PRN"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["PRN"]);
                        ObjGenRegData.StudentName = (Convert.ToString(Dt.Rows[i]["StudentName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["StudentName"]);
                        ObjGenRegData.Gender = (Convert.ToString(Dt.Rows[i]["Gender"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["Gender"]);
                        ObjGenRegData.CorrespondenceAddress = (Convert.ToString(Dt.Rows[i]["CorrespondenceAddress"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["CorrespondenceAddress"]);
                        ObjGenRegData.MobileNo = (Convert.ToString(Dt.Rows[i]["MobileNo"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["MobileNo"]);
                        ObjGenRegData.ApplicationReservationName = (Convert.ToString(Dt.Rows[i]["ApplicationReservationName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ApplicationReservationName"]);
                        ObjGenRegData.Caste = (Convert.ToString(Dt.Rows[i]["Caste"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["Caste"]);
                        ObjGenRegData.PlaceOfBirth = (Convert.ToString(Dt.Rows[i]["PlaceOfBirth"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PlaceOfBirth"]);
                        DateTime DOB = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(Dt.Rows[i]["DOB"]).ToUniversalTime(), INDIAN_ZONE);
                        ObjGenRegData.DOB = string.Format("{0: dd-MM-yyyy}", DOB);
                        ObjGenRegData.PreviousSchoolCollegeName = (Convert.ToString(Dt.Rows[i]["InstituteAttended"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstituteAttended"]);
                        //DateTime DateOfAdmission = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(Dt.Rows[i]["DateOfAdmission"]).ToUniversalTime(), INDIAN_ZONE);
                        ObjGenRegData.DateOfAdmission = Convert.ToString(Dt.Rows[i]["DateOfAdmission"]); 
                        ObjGenRegData.CourseName = (Convert.ToString(Dt.Rows[i]["CourseName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["CourseName"]);
                        count = count + 1;
                        ObjGenRegData.IndexId = count;
                        ObjLstGenRegData.Add(ObjGenRegData);
                    }
                    return Return.returnHttp("200", ObjLstGenRegData, null);

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

