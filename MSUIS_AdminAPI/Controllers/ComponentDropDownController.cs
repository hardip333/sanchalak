using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MSUISApi.BAL;
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class ComponentDropDownController : ApiController
    {

        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));



        #region AcademicYearDD
        [HttpPost]
        public HttpResponseMessage AcademicYearDD()
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


                SqlCommand Cmd = new SqlCommand("CDDIncAcademicYearGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);


                List<AcademicYear> AYList = new List<AcademicYear>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        AcademicYear AY = new AcademicYear();

                        AY.Id = (Convert.ToString(Dt.Rows[i]["Id"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["Id"]);
                        AY.AcademicYearCode = (Convert.ToString(Dt.Rows[i]["AcademicYearCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);
                        AYList.Add(AY);
                    }
                }
                return Return.returnHttp("200", AYList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region FacultyDD
        [HttpPost]
        public HttpResponseMessage FacultyDD()
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                Request.Headers.TryGetValues("InstituteId", out IEnumerable<string> InstituteId1);
                var InstituteId = InstituteId1?.FirstOrDefault();
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


                SqlCommand Cmd = new SqlCommand("CDDFacultyGet", Con);
                Cmd.Parameters.AddWithValue("@InstituteId", InstituteId);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<MstFaculty> FList = new List<MstFaculty>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstFaculty F = new MstFaculty();

                        F.Id = (Convert.ToString(Dt.Rows[i]["Id"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["Id"]);
                        F.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        F.FacultyCode = (Convert.ToString(Dt.Rows[i]["FacultyCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyCode"]);
                        FList.Add(F);
                    }
                }
                return Return.returnHttp("200", FList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

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
                Cmd.Parameters.AddWithValue("@FacultyId", PI.FacultyId );
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

        #region BranchDD
        [HttpPost]
        public HttpResponseMessage BranchDD(MstSpecialisation specialisation)
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


                SqlCommand Cmd = new SqlCommand("CDDBranchGetByProgrammeInstId", Con);

                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@ProgrammeInstanceId", specialisation.ProgrammeInstanceId);
                
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<MstSpecialisation> SPList = new List<MstSpecialisation>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstSpecialisation SP = new MstSpecialisation();

                        SP.Id = (Convert.ToString(Dt.Rows[i]["Id"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["Id"]);
                        SP.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["BranchName"]);
                        SPList.Add(SP);
                    }
                }
                return Return.returnHttp("200", SPList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region PartInstanceDD
        [HttpPost]
        public HttpResponseMessage PartInstanceDD(ProgrammeInstancePart PartInst)
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


                SqlCommand Cmd = new SqlCommand("CDDProgrammePartInstanceGetByProgrammeInstId", Con);

                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@ProgrammeInstanceId", PartInst.ProgrammeInstanceId);

                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<ProgrammeInstancePart> PIPList = new List<ProgrammeInstancePart>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePart PIP = new ProgrammeInstancePart();

                        PIP.Id = (Convert.ToString(Dt.Rows[i]["Id"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["Id"]);
                        PIP.ProgrammeInstanceId = (Convert.ToString(Dt.Rows[i]["ProgrammeInstanceId"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["ProgrammeInstanceId"]);
                        PIP.InstancePartName = (Convert.ToString(Dt.Rows[i]["InstancePartName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstancePartName"]);
                        PIPList.Add(PIP);
                    }
                }
                return Return.returnHttp("200", PIPList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region PartTermInstanceDD
        [HttpPost]
        public HttpResponseMessage PartTermInstanceDD(ProgrammeInstancePartTerm PartTermInst)
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


                SqlCommand Cmd = new SqlCommand("CDDPartTermInstanceGetByProgrammePartInstIdBranchId", Con);

                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@ProgrammeInstancePartId", PartTermInst.ProgrammeInstancePartId);
                Cmd.Parameters.AddWithValue("@SpecialisationId", PartTermInst.SpecialisationId);

                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<ProgrammeInstancePartTerm> PIPTList = new List<ProgrammeInstancePartTerm>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePartTerm PIPT = new ProgrammeInstancePartTerm();


                        PIPT.Id = (Convert.ToString(Dt.Rows[i]["Id"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["Id"]);
                        PIPT.ProgrammeInstancePartId = (Convert.ToString(Dt.Rows[i]["ProgrammeInstancePartId"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["ProgrammeInstancePartId"]);
                        PIPT.SpecialisationId = (Convert.ToString(Dt.Rows[i]["SpecialisationId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["SpecialisationId"]);
                        PIPT.InstancePartTermName = (Convert.ToString(Dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                        PIPTList.Add(PIPT);
                    }
                }
                return Return.returnHttp("200", PIPTList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion


    }
}
