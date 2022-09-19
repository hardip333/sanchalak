using MSUIS_TokenManager.App_Start;
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
using System.Web.Http;

namespace MSUISApi.Controllers
{
    public class CancelledByFacultyController : ApiController
    {

        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();

        #region CancelledByStudentGet
        [HttpPost]
        public HttpResponseMessage CancelledByFacultyGet(CancelledByFaculty CASA)
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

                try
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable();

                    List<CancelledByFaculty> Clist = new List<CancelledByFaculty>();
                    Int32 count = 0;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", CASA.ProgrammeInstancePartTermId);
                    cmd.CommandText = "CancelledByFaculty";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    foreach (DataRow DR in
                        dt.Rows)
                    {
                        CancelledByFaculty c = new CancelledByFaculty();

                        c.ApplicationId = DR["ApplicationId"].ToString();
                        c.FacultyName = DR["FacultyName"].ToString();
                        c.InstancePartTermName = DR["InstancePartTermName"].ToString();
                        c.FullName = DR["FullName"].ToString();
                        c.EmailId = DR["EmailId"].ToString();
                        c.MobileNo = DR["MobileNo"].ToString();
                        c.InstituteId = DR["InstituteId"].ToString();
                        c.InstituteName = DR["InstituteName"].ToString();
                        c.GroupName = DR["GroupName"].ToString();
                        c.FeeCategoryName = DR["FeeCategoryName"].ToString();
                        c.IsAdmissionFeePaid = DR["IsAdmissionFeePaid"].ToString();
                        c.IsInstalmentSelected = DR["IsInstalmentSelected"].ToString();
                        c.TotalAmount = Convert.ToDecimal(DR["TotalAmount"].ToString());
                        c.AmountPaid = Convert.ToDecimal(DR["AmountPaid"].ToString());
                        c.IsVerificationEmail = DR["IsVerificationEmail"].ToString();
                        c.IsVerificationSms = DR["IsVerificationSms"].ToString();
                        c.IsVerificationEmailOns = DR["IsVerificationEmailOn"].ToString();
                        c.IsVerificationSmsOns = DR["IsVerificationSmsOn"].ToString();
                        c.IsCancelledByFaculty = Convert.ToBoolean(DR["IsCancelledByFaculty"].ToString());
                        c.CanclledByFaculty = DR["CanclledByFaculty"].ToString();
                        c.FacultyCancelledOns = DR["FacultyCancelledOn"].ToString();
                        c.FacultyCancelledRemark = DR["FacultyCancelledRemark"].ToString();
                        c.Gender = DR["Gender"].ToString();
                        c.IsPhysicallyChanllenged = DR["IsPhysicallyChanllenged"].ToString();
                        c.ApplicationReservationName = DR["ApplicationReservationName"].ToString();
                        c.SocialCategoryName = DR["SocialCategoryName"].ToString();
                        c.CommitteeName = DR["CommitteeName"].ToString();


                        count = count + 1;
                        c.IndexId = count;


                        Clist.Add(c);
                    }
                    return Return.returnHttp("200", Clist, null);
                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message.ToString(), null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion


        #region IncProgInsPartTermListGetByInstituteId
        // This method is for getting InstancePartTerm By Institute Id for Academic Purpose
        [HttpPost]
        public HttpResponseMessage IncProgInsPartTermListGetByInstituteId(ApplicationList ObjAppList)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                List<ApplicationList> slist = new List<ApplicationList>();

                cmd.Parameters.AddWithValue("@InstituteId", ObjAppList.InstituteId);
                cmd.CommandText = "IncProgInstPartTermGetByFacultyId";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow DR in dt.Rows)
                {
                    ApplicationList s = new ApplicationList();

                    s.InstancePartTermId = Convert.ToInt32(DR["Id"].ToString());
                    s.InstancePartTermName = DR["InstancePartTermName"].ToString();

                    slist.Add(s);
                }
                return Return.returnHttp("200", slist, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion


        #region IncProgInsPartTermListGetByInstituteId
        [HttpGet]
        public HttpResponseMessage ApplicationListGet()
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

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                List<ApplicationList> slist = new List<ApplicationList>();

                cmd.Parameters.AddWithValue("@FacultyId", facultyDepartIntituteId);
                cmd.CommandText = "ApplicationListGet";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow DR in dt.Rows)
                {
                    ApplicationList s = new ApplicationList();

                    s.InstancePartTermId = Convert.ToInt32(DR["Id"].ToString());
                    s.InstancePartTermName = DR["InstancePartTermName"].ToString();

                    slist.Add(s);
                }
                return Return.returnHttp("200", slist, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

    }
}