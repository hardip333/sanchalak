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
using System.Dynamic;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class CopyCourseProgrammeDefinationController : ApiController 
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region MstFacultyGet
        [HttpPost]
        public HttpResponseMessage FacultyGetById(MstFaculty MF)
        {
            //MstFaculty modelobj = new MstFaculty(); 
            try
            {
                //modelobj.Id = MF.Id;
                SqlCommand cmd = new SqlCommand("MstFacGetbyId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", MF.Id);
                sda.SelectCommand = cmd;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                List<MstFaculty> ObjLstF = new List<MstFaculty>();
                //dt.Rows.Add("0", "All Faculty");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstFaculty ObjF = new MstFaculty();
                        ObjF.Id = Convert.ToInt32(dr["Id"]);
                        ObjF.FacultyName = (dr["FacultyName"].ToString());

                        ObjLstF.Add(ObjF);
                    }

                }

                return Return.returnHttp("200", ObjLstF, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion


        #region ProgrammeGetByFacId
        [HttpPost]
        public HttpResponseMessage ProgrammeGetByFacId(MstProgramme MP)
        {
            //MstFaculty modelobj = new MstFaculty(); 
            try
            {
                //modelobj.Id = MF.Id;
                SqlCommand cmd = new SqlCommand("MstProgrammeGetByFacId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", MP.FacultyId);
                sda.SelectCommand = cmd;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                List<MstProgramme> ObjLstProg = new List<MstProgramme>();
                //dt.Rows.Add("0", "All Faculty");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstProgramme ObjP = new MstProgramme();
                        ObjP.ProgrammeId = Convert.ToInt32(dr["Id"]);
                        ObjP.ProgrammeName = (dr["ProgrammeName"].ToString());

                        ObjLstProg.Add(ObjP);
                    }

                }

                return Return.returnHttp("200", ObjLstProg, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion



        #region SpecialisationGetByFacultyId
        [HttpPost]
        public HttpResponseMessage SpecialisationGetByFacultyId(MstSpecialisation MS)
        {
            //MstFaculty modelobj = new MstFaculty(); 
            try
            {
                //modelobj.Id = MF.Id;
                SqlCommand cmd = new SqlCommand("MstSpecialisationGetFIDPID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", MS.FacultyId);
                cmd.Parameters.AddWithValue("@ProgrammeId", MS.ProgrammeId);
                sda.SelectCommand = cmd;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                List<MstSpecialisation> ObjLstSpec = new List<MstSpecialisation>();
                //dt.Rows.Add("0", "All Faculty");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstSpecialisation ObjP = new MstSpecialisation();
                        ObjP.SpecialisationId = Convert.ToInt32(dr["Id"]);
                        ObjP.BranchName = (dr["BranchName"].ToString());

                        ObjLstSpec.Add(ObjP);
                    }

                }

                return Return.returnHttp("200", ObjLstSpec, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion


        #region CopyCourseProgrammeDefinationGet
        [HttpPost]
        public HttpResponseMessage CopyCourseProgrammeDefinationGet(CopyCourseProgrammeDefination ObjCCPD)
        {
            CopyCourseProgrammeDefination modelobj = new CopyCourseProgrammeDefination();

            try
            {
                modelobj.FacultyId = ObjCCPD.FacultyId;
                modelobj.ProgrammeId = ObjCCPD.ProgrammeId;
                modelobj.SpecialisationId = ObjCCPD.SpecialisationId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                Int64 UserId = Convert.ToInt64(res.ToString());

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("CopyCourseProgrammeDefination", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", modelobj.FacultyId);
                cmd.Parameters.AddWithValue("@ProgrammeId", modelobj.ProgrammeId);
                cmd.Parameters.AddWithValue("@SpecialisationId", modelobj.SpecialisationId);
                sda.SelectCommand = cmd;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Int32 count = 0;
                List<CopyCourseProgrammeDefination> ObjLstCCP = new List<CopyCourseProgrammeDefination>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        CopyCourseProgrammeDefination ObjCP = new CopyCourseProgrammeDefination();
                                                
                        ObjCP.ProgrammePartTermId = Convert.ToInt32(dr["Id"].ToString());
                        ObjCP.PartTermName = (dr["PartTermName"].ToString());
                       
                        count = count + 1;
                        ObjCP.IndexId = count;
                        ObjLstCCP.Add(ObjCP);

                        
                    }

                    
                }
                return Return.returnHttp("200", ObjLstCCP, null);

                
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion



        #region DestinationAcademicYearGet
        [HttpPost]
        public HttpResponseMessage DestinationAcademicYearGet()
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();
            string res = ac.ValidateToken(token);
            Int64 UserId = Convert.ToInt64(res.ToString());

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            try
            {
                //modelobj.Id = MF.Id;
                SqlCommand cmd = new SqlCommand("DestinationIncAcademicYearGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                
                sda.SelectCommand = cmd;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                List<AcademicYear> ObjLstA = new List<AcademicYear>();
                //dt.Rows.Add("0", "All Faculty");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AcademicYear ObjA = new AcademicYear();
                        ObjA.DestinationAcademicYearId = Convert.ToInt32(dr["Id"]);
                        ObjA.DestinationAcademicYearCode = (dr["DestinationAcademicYearCode"].ToString());

                        ObjLstA.Add(ObjA);
                    }

                }

                return Return.returnHttp("200", ObjLstA, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion



        #region SourceAcademicYearGet
        [HttpPost]
        public HttpResponseMessage SourceAcademicYearGet()
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();
            string res = ac.ValidateToken(token);
            Int64 UserId = Convert.ToInt64(res.ToString());

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            try
            {
                //modelobj.Id = MF.Id;
                SqlCommand cmd = new SqlCommand("IncAcademicYearGetOnSequenceNo", con);
                cmd.CommandType = CommandType.StoredProcedure;

                sda.SelectCommand = cmd;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                List<AcademicYear> ObjLstA = new List<AcademicYear>();
                //dt.Rows.Add("0", "All Faculty");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AcademicYear ObjA = new AcademicYear();
                        ObjA.SourceAcademicYearId = Convert.ToInt32(dr["Id"]);
                        ObjA.SourceAcademicYearCode = (dr["SourceAcademicYearCode"].ToString());

                        ObjLstA.Add(ObjA);
                    }

                }

                return Return.returnHttp("200", ObjLstA, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion


        #region CopyCourseDefinationAdd
        [HttpPost]
        public HttpResponseMessage CopyCourseDefinationAdd(CopyCourseDefination ObjCCD)

        {
            CopyCourseDefination CCD = new CopyCourseDefination();
            //dynamic jsonData = ObjCCD;
            try
            {
                //Int64 DestinationProgrammeInstancePartTermId = Convert.ToInt64(ObjCCD.DestinationProgrammeInstancePartTermId);
                int AcademicYrId = Convert.ToInt32(ObjCCD.DestinationAcademicYearId);
                int ProgrammePartTermId = Convert.ToInt32(ObjCCD.ProgrammePartTermId);
                int SpecialistionId = Convert.ToInt32(ObjCCD.SpecialisationId);
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                Int64 UserId = Convert.ToInt64(responseToken);

                SqlCommand cmd = new SqlCommand("CopyCourseDefination", con);
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@PartTermId", ProgrammePartTermId);
                cmd.Parameters.AddWithValue("@SpecialisationId", SpecialistionId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYrId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();
                if (strMessage == "TRUE")
                {
                    strMessage = "Post Defination of the Selected Part has been Copied Successfully.";
                }
                return Return.returnHttp("200", strMessage, null);


            }
            catch (Exception e)
            {

                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion


       



    }
}
