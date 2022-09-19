using MSUIS_TokenManager.App_Start;
using MSUISApi.BAL;
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
    public class ProgrammeWiseMarksCreditDetailsController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
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

        #region InstituteGetByFacultyId
        [HttpPost]
        public HttpResponseMessage InstituteGetByFacultyId(ProgrammeWiseMarksCreditDetails pwmc)
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
                SqlCommand cmd = new SqlCommand("ProgWisePaperTLMGetInstituteByFacultyId", Con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", pwmc.FacultyId);
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<ProgrammeWiseMarksCreditDetails> ObjListPWCD = new List<ProgrammeWiseMarksCreditDetails>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ProgrammeWiseMarksCreditDetails ObjIQ = new ProgrammeWiseMarksCreditDetails();


                        ObjIQ.InstituteId = Convert.ToInt32(dr["InstituteId"].ToString());
                        ObjIQ.InstituteName = (Convert.ToString(dr["InstituteName"]));




                        ObjListPWCD.Add(ObjIQ);
                    }
                }
                return Return.returnHttp("200", ObjListPWCD, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region IncAcademicYearGet
        [HttpPost]
        public HttpResponseMessage IncAcademicYearGet(ProgrammeWiseMarksCreditDetails pwmc)
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
                SqlCommand cmd = new SqlCommand("IncAcademicYearGet", Con);

                cmd.CommandType = CommandType.StoredProcedure;     
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<ProgrammeWiseMarksCreditDetails> ObjListPWCD = new List<ProgrammeWiseMarksCreditDetails>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ProgrammeWiseMarksCreditDetails ObjIQ = new ProgrammeWiseMarksCreditDetails();


                        ObjIQ.AcademicYearId = Convert.ToInt32(dr["Id"].ToString());
                        ObjIQ.AcademicYearCode = (Convert.ToString(dr["AcademicYearCode"]));




                        ObjListPWCD.Add(ObjIQ);
                    }
                }
                return Return.returnHttp("200", ObjListPWCD, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region ProgrammeWiseMarksCreditDetailsById
        [HttpPost]
        public HttpResponseMessage ProgrammeWiseMarksCreditDetailsById(ProgrammeWiseMarksCreditDetails objprogMarksCredit)
        {
            try
            {
                Int64 FacultyId = objprogMarksCredit.FacultyId;
                Int64 InstituteId = objprogMarksCredit.InstituteId;
                Int64 AcademicYearId = objprogMarksCredit.AcademicYearId;
               
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int32 Count = 0;
                SqlCommand cmd = new SqlCommand("ProgrammeWiseMarksCreditDetailsGetById", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                cmd.Parameters.AddWithValue("@InstituteId", InstituteId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                sda.SelectCommand = cmd;
                sda.Fill(Dt);
                List<ProgrammeWiseMarksCreditDetails> objProgMarksCreditList = new List<ProgrammeWiseMarksCreditDetails>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ProgrammeWiseMarksCreditDetails objProgMarksCredit = new ProgrammeWiseMarksCreditDetails();
                        objProgMarksCredit.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objProgMarksCredit.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        objProgMarksCredit.PartName = (Convert.ToString(Dt.Rows[i]["PartName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["PartName"]);
                        objProgMarksCredit.PartTermName = (Convert.ToString(Dt.Rows[i]["PartTermName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["PartTermName"]);
                        objProgMarksCredit.InstanceName = (Convert.ToString(Dt.Rows[i]["InstanceName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["InstanceName"]);
                        objProgMarksCredit.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["BranchName"]);
                        objProgMarksCredit.FollowCreditSystem = (Convert.ToString(Dt.Rows[i]["FollowCreditSystem"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["FollowCreditSystem"]);
                        objProgMarksCredit.ProgrammePartTermMinMarks = (Convert.ToString(Dt.Rows[i]["ProgrammePartTermMinMarks"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["ProgrammePartTermMinMarks"]);
                        objProgMarksCredit.ProgrammePartTermMaxMarks = (Convert.ToString(Dt.Rows[i]["ProgrammePartTermMaxMarks"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["ProgrammePartTermMaxMarks"]);
                        objProgMarksCredit.ProgrammePartMinMarks = (Convert.ToString(Dt.Rows[i]["ProgrammePartMinMarks"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["ProgrammePartMinMarks"]);
                        objProgMarksCredit.ProgrammePartMaxMarks = (Convert.ToString(Dt.Rows[i]["ProgrammePartMaxMarks"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["ProgrammePartMaxMarks"]);
                       
                      

                        Count = Count + 1;
                        objProgMarksCredit.IndexId = Count;

                        objProgMarksCreditList.Add(objProgMarksCredit);
                    }

                }
                return Return.returnHttp("200", objProgMarksCreditList, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion


    }
}
