using MSUIS_TokenManager.App_Start;
using MSUISApi.BAL;
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
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class StudentWisePaperListController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable Dt = new DataTable();

        #region StudentWisePaperListGet
        [HttpPost]
        public HttpResponseMessage StudentWisePaperListGet(StudentWisePaperList ObjSWP)
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

                    List<StudentWisePaperList> slist = new List<StudentWisePaperList>();
                    Int32 count = 0;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ObjSWP.ProgrammeInstancePartTermId);
                    cmd.Parameters.AddWithValue("@AcademicYearId", ObjSWP.AcademicYearId);
                    cmd.Parameters.AddWithValue("@InstituteId", ObjSWP.InstituteId);
                    cmd.Parameters.AddWithValue("@ProgrammeId", ObjSWP.ProgrammeId);
                    cmd.Parameters.AddWithValue("@ProgrammePartId", ObjSWP.ProgrammePartId);
                    cmd.Parameters.AddWithValue("@SpecialisationId", ObjSWP.SpecialisationId);
                    cmd.CommandText = "StudentWisePaperList";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    foreach (DataRow DR in dt.Rows)
                    {
                        StudentWisePaperList s = new StudentWisePaperList();

                        s.PRN = (((DR["PRN"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(DR["PRN"]);
                        s.FullName = (Convert.ToString(DR["FullName"])).IsEmpty() ? "" : Convert.ToString(DR["FullName"]);
                        s.EmailId = (Convert.ToString(DR["EmailId"])).IsEmpty() ? "" : Convert.ToString(DR["EmailId"]);
                        s.MobileNo = (Convert.ToString(DR["MobileNo"])).IsEmpty() ? "" : Convert.ToString(DR["MobileNo"]);
                        s.Paper1 = (Convert.ToString(DR["Paper1"])).IsEmpty() ? "" : Convert.ToString(DR["Paper1"]);
                        s.Paper2 = (Convert.ToString(DR["Paper2"])).IsEmpty() ? "" : Convert.ToString(DR["Paper2"]);
                        s.Paper3 = (Convert.ToString(DR["Paper3"])).IsEmpty() ? "" : Convert.ToString(DR["Paper3"]);
                        s.Paper4 = (Convert.ToString(DR["Paper4"])).IsEmpty() ? "" : Convert.ToString(DR["Paper4"]);
                        s.Paper5 = (Convert.ToString(DR["Paper5"])).IsEmpty() ? "" : Convert.ToString(DR["Paper5"]);
                        s.Paper6 = (Convert.ToString(DR["Paper6"])).IsEmpty() ? "" : Convert.ToString(DR["Paper6"]);
                        s.Paper7 = (Convert.ToString(DR["Paper7"])).IsEmpty() ? "" : Convert.ToString(DR["Paper7"]);
                        s.Paper8 = (Convert.ToString(DR["Paper8"])).IsEmpty() ? "" : Convert.ToString(DR["Paper8"]);
                        s.Paper9 = (Convert.ToString(DR["Paper9"])).IsEmpty() ? "" : Convert.ToString(DR["Paper9"]);
                        s.Paper10 = (Convert.ToString(DR["Paper10"])).IsEmpty() ? "" : Convert.ToString(DR["Paper10"]);
                        s.Paper11 = (Convert.ToString(DR["Paper11"])).IsEmpty() ? "" : Convert.ToString(DR["Paper11"]);
                        s.Paper12 = (Convert.ToString(DR["Paper12"])).IsEmpty() ? "" : Convert.ToString(DR["Paper12"]);
                        s.Paper13 = (Convert.ToString(DR["Paper13"])).IsEmpty() ? "" : Convert.ToString(DR["Paper13"]);
                        s.Paper14 = (Convert.ToString(DR["Paper14"])).IsEmpty() ? "" : Convert.ToString(DR["Paper14"]);
                        s.Paper15 = (Convert.ToString(DR["Paper15"])).IsEmpty() ? "" : Convert.ToString(DR["Paper15"]);


                        count = count + 1;
                        s.IndexId = count;


                        slist.Add(s);
                    }
                    return Return.returnHttp("200", slist, null);
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

        #region AcademicYearGet
        [HttpPost]
        public HttpResponseMessage AcademicYearGet()
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
                ObjListAcadYear = ObjBalAcadYearList.AcademicYearGet();

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

        #region MstInstituteGetById
        [HttpPost]
        public HttpResponseMessage MstInstituteGetById(MstInstitute MI)
        {
            //MstFaculty modelobj = new MstFaculty(); 
            try
            {
                //modelobj.Id = MF.Id;
                SqlCommand cmd = new SqlCommand("MstInstGetById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InstituteId", MI.Id);
                sda.SelectCommand = cmd;
                sda.Fill(Dt);
                List<MstInstitute> ObjLstI = new List<MstInstitute>();
                //dt.Rows.Add("0", "All Faculty");
                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        MstInstitute ObjI = new MstInstitute();
                        ObjI.Id = Convert.ToInt32(dr["Id"]);
                        ObjI.InstituteName = (dr["InstituteName"].ToString());

                        ObjLstI.Add(ObjI);
                    }

                }

                return Return.returnHttp("200", ObjLstI, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region Programme Get By InstituteId
        [HttpPost]
        public HttpResponseMessage MstProgrammeGetByInstituteId(MstProgramme ObjProg)
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstProgrammeGetByInstituteId", con);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@InstituteId", ObjProg.InstituteId);

                sda.SelectCommand = Cmd;
                DataTable Dt = new DataTable();
                sda.Fill(Dt);

                List<MstProgramme> ObjLstProg = new List<MstProgramme>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgramme objProg = new MstProgramme();

                        objProg.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objProg.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        objProg.ProgrammeCode = (Convert.ToString(Dt.Rows[i]["ProgrammeCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeCode"]);
                        objProg.InstituteId = Convert.ToInt16(Dt.Rows[i]["InstituteId"].ToString());
                        objProg.InstituteName = (Convert.ToString(Dt.Rows[i]["InstituteName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        objProg.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        objProg.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objProg.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);
                        ObjLstProg.Add(objProg);
                    }
                }
                return Return.returnHttp("200", ObjLstProg, null);
            }
            catch (Exception e)
            {
                return null;
            }

        }

        #endregion        

        #region MstProgrammePartGetByProgrammeId
        [HttpPost]
        public HttpResponseMessage MstProgrammePartGetByProgrammeId(MstProgrammePart ObjProgPart)
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

                List<MstProgrammePart> ObjLstProgPart = new List<MstProgrammePart>();
                BALProgrammePart ObjBalProgPartList = new BALProgrammePart();
                ObjLstProgPart = ObjBalProgPartList.ProgrammePartGetByProgrammeId(ObjProgPart.ProgrammeId);

                if (ObjLstProgPart != null)
                    return Return.returnHttp("200", ObjLstProgPart, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Specialisation Get By ProgrammeId
        [HttpPost]
        public HttpResponseMessage SpecialisationGetByProgrammeId(MstSpecialisation ObjSpec)
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("SpecialisationGetByProgIdPartId", con);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@ProgrammeId", ObjSpec.ProgrammeId);
                Cmd.Parameters.AddWithValue("@ProgrammePartId", ObjSpec.ProgrammePartId);
                sda.SelectCommand = Cmd;
                DataTable Dt = new DataTable();
                sda.Fill(Dt);

                List<MstSpecialisation> ObjLstSpeclisation = new List<MstSpecialisation>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstSpecialisation objSp = new MstSpecialisation();

                        objSp.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objSp.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["BranchName"]);
                        objSp.ProgrammeId = Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);
                        objSp.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        objSp.ProgrammePartId = Convert.ToInt32(Dt.Rows[i]["ProgrammePartId"]);
                        objSp.PartShortName = (Convert.ToString(Dt.Rows[i]["PartShortName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PartShortName"]);

                        ObjLstSpeclisation.Add(objSp);
                    }
                }
                return Return.returnHttp("200", ObjLstSpeclisation, null);
            }
            catch (Exception e)
            {
                return null;
            }

        }

        #endregion        

        #region Programme Get By SpecialisationId
        [HttpPost]
        public HttpResponseMessage ProgrammeInstancePartTermGetBySpecId(ProgrammeInstancePartTerm ObjProg)
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("ProgrammeInstPartTermGetBySpeId", con);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@ProgrammeId", ObjProg.ProgrammeId);
                Cmd.Parameters.AddWithValue("@ProgrammePartId", ObjProg.ProgrammePartId);
                Cmd.Parameters.AddWithValue("@SpecialisationId", ObjProg.SpecialisationId);
                Cmd.Parameters.AddWithValue("@AcademicYearId", ObjProg.AcademicYearId);

                sda.SelectCommand = Cmd;
                DataTable Dt = new DataTable();
                sda.Fill(Dt);

                List<ProgrammeInstancePartTerm> ObjLstProg = new List<ProgrammeInstancePartTerm>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePartTerm objProg = new ProgrammeInstancePartTerm();

                        objProg.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objProg.InstancePartTermName = (Convert.ToString(Dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                        ObjLstProg.Add(objProg);
                    }
                }
                return Return.returnHttp("200", ObjLstProg, null);
            }
            catch (Exception e)
            {
                return null;
            }

        }

        #endregion        

    }
}