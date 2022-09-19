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
    public class ProgrammeWisePaperTLMDetailsController : ApiController
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
        public HttpResponseMessage InstituteGetByFacultyId(ProgrammeWisePaperTLMDetails pwpt)
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
                cmd.Parameters.AddWithValue("@FacultyId", pwpt.FacultyId);
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<ProgrammeWisePaperTLMDetails> ObjListPWPTD = new List<ProgrammeWisePaperTLMDetails>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ProgrammeWisePaperTLMDetails ObjIQ = new ProgrammeWisePaperTLMDetails();


                        ObjIQ.InstituteId = Convert.ToInt32(dr["InstituteId"].ToString());
                        ObjIQ.InstituteName = (Convert.ToString(dr["InstituteName"]));




                        ObjListPWPTD.Add(ObjIQ);
                    }
                }
                return Return.returnHttp("200", ObjListPWPTD, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region IncAcademicYearGet
        [HttpPost]
        public HttpResponseMessage IncAcademicYearGet(ProgrammeWisePaperTLMDetails pwpt)
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

                List<ProgrammeWisePaperTLMDetails> ObjListPWPTD = new List<ProgrammeWisePaperTLMDetails>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ProgrammeWisePaperTLMDetails ObjIQ = new ProgrammeWisePaperTLMDetails();


                        ObjIQ.AcademicYearId = Convert.ToInt32(dr["Id"].ToString());
                        ObjIQ.AcademicYearCode = (Convert.ToString(dr["AcademicYearCode"]));




                        ObjListPWPTD.Add(ObjIQ);
                    }
                }
                return Return.returnHttp("200", ObjListPWPTD, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region ProgrammeWisePaperTLMDetailsById
        [HttpPost]
        public HttpResponseMessage ProgrammeWisePaperTLMDetailsById(ProgrammeWisePaperTLMDetails objprogPaperTLM)
        {
            try
            {
                Int64 FacultyId = objprogPaperTLM.FacultyId;
                Int64 InstituteId = objprogPaperTLM.InstituteId;
                Int64 AcademicYearId = objprogPaperTLM.AcademicYearId;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int32 Count = 0;
                SqlCommand cmd = new SqlCommand("ProgrammeWisepaperTLMDetailsGetById", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                cmd.Parameters.AddWithValue("@InstituteId", InstituteId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                sda.SelectCommand = cmd;
                sda.Fill(Dt);
                List<ProgrammeWisePaperTLMDetails> objProgPaperTLMList = new List<ProgrammeWisePaperTLMDetails>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        
                        ProgrammeWisePaperTLMDetails objProgPaperTLM = new ProgrammeWisePaperTLMDetails();
                        objProgPaperTLM.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objProgPaperTLM.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        objProgPaperTLM.PartName = (Convert.ToString(Dt.Rows[i]["PartName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["PartName"]);
                        objProgPaperTLM.PartTermName = (Convert.ToString(Dt.Rows[i]["PartTermName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["PartTermName"]);
                        objProgPaperTLM.InstancePartTermName = (Convert.ToString(Dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                        objProgPaperTLM.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["BranchName"]);
                        objProgPaperTLM.PaperCode = (Convert.ToString(Dt.Rows[i]["PaperCode"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["PaperCode"]);
                        objProgPaperTLM.PaperName = (Convert.ToString(Dt.Rows[i]["PaperName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["PaperName"]);
                        objProgPaperTLM.PaperCredit = (Convert.ToString(Dt.Rows[i]["PaperCredit"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["PaperCredit"]);
                        objProgPaperTLM.MaxMarks = (Convert.ToString(Dt.Rows[i]["MaxMarks"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["MaxMarks"]);
                        objProgPaperTLM.MinMarks = (Convert.ToString(Dt.Rows[i]["MinMarks"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["MinMarks"]);
                        objProgPaperTLM.TLM = (Convert.ToString(Dt.Rows[i]["TLM"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["TLM"]);
                        objProgPaperTLM.AM = (Convert.ToString(Dt.Rows[i]["AM"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["AM"]);
                        objProgPaperTLM.AssessmentMethodMarks = (Convert.ToString(Dt.Rows[i]["AssessmentMethodMarks"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["AssessmentMethodMarks"]);
                        objProgPaperTLM.FollowCreditSystem = (Convert.ToString(Dt.Rows[i]["FollowCreditSystem"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["FollowCreditSystem"]);
                        objProgPaperTLM.AssessmentType = (Convert.ToString(Dt.Rows[i]["AssessmentType"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["AssessmentType"]);
                        objProgPaperTLM.AssessmentTypeMaxMarks = (Convert.ToString(Dt.Rows[i]["AssessmentTypeMaxMarks"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["AssessmentTypeMaxMarks"]);
                        objProgPaperTLM.AssessmentTypeMinMarks = (Convert.ToString(Dt.Rows[i]["AssessmentTypeMinMarks"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["AssessmentTypeMinMarks"]);

                       

                        Count = Count + 1;
                        objProgPaperTLM.IndexId = Count;

                        objProgPaperTLMList.Add(objProgPaperTLM);
                    }

                }
                return Return.returnHttp("200", objProgPaperTLMList, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion
    }
}
