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
    public class PreferenceWiseReportController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));


        #region IncAcademicYearCodeGetById
        [HttpPost]
        public HttpResponseMessage IncAcadYearCode(AcademicYear ObjAY)
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
                SqlCommand cmd = new SqlCommand("IncAcademicYearGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = cmd;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                List<AcademicYear> ObjLstInst = new List<AcademicYear>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AcademicYear ObjInst = new AcademicYear();
                        ObjInst.AcademicYearId = Convert.ToInt32(dr["Id"]);
                        ObjInst.AcademicYearCode = (dr["AcademicYearCode"].ToString());
                        ObjLstInst.Add(ObjInst);
                    }

                }

                return Return.returnHttp("200", ObjLstInst, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion


        #region MstFacultyGet
        [HttpPost]
        public HttpResponseMessage FacultyGetById(MstFaculty MF)
        {
            //MstFaculty modelobj = new MstFaculty(); 
            try
            {
                //modelobj.Id = MF.Id;
                SqlCommand cmd = new SqlCommand("MstFacGetByIdAYID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", MF.Id);
                cmd.Parameters.AddWithValue("@AcademicYearId", MF.AcademicYearId);
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
                        ObjF.FacultyId = Convert.ToInt32(dr["Id"]);
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

        #region ProgrammePartGetByFIdPID
        [HttpPost]
        public HttpResponseMessage ProgrammePartGetByFIdPID(MstProgrammePart MP)
        {
            //MstFaculty modelobj = new MstFaculty(); 
            try
            {
                //modelobj.Id = MF.Id;
                SqlCommand cmd = new SqlCommand("MstProgrammePartGetByFIDPID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", MP.FacultyId);
                cmd.Parameters.AddWithValue("@ProgrammeId", MP.ProgrammeId);
                sda.SelectCommand = cmd;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                List<MstProgrammePart> ObjLstProgP = new List<MstProgrammePart>();
                //dt.Rows.Add("0", "All Faculty");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstProgrammePart ObjPP = new MstProgrammePart();
                        ObjPP.ProgrammePartId = Convert.ToInt32(dr["Id"]);
                        ObjPP.PartName = (dr["PartName"].ToString());

                        ObjLstProgP.Add(ObjPP);
                    }

                }

                return Return.returnHttp("200", ObjLstProgP, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region MstSpecialisationGetFIDPIDGet
        [HttpPost]
        public HttpResponseMessage MstSpecialisationGetFIDPIDPPIDGet(MstSpecialisation MS)
        {
            //MstFaculty modelobj = new MstFaculty(); 
            try
            {
                //modelobj.Id = MF.Id;
                SqlCommand cmd = new SqlCommand("MstSpecialisationGetByFIDPIDPPID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", MS.FacultyId);
                cmd.Parameters.AddWithValue("@ProgrammeId", MS.ProgrammeId);
                cmd.Parameters.AddWithValue("@ProgrammePartId", MS.ProgrammePartId);
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


        #region MstProgrammePartTermGetByFIDPIDPPIDSIDGet
        [HttpPost]
        public HttpResponseMessage MstProgrammePartTermGetByFIDPIDPPIDSIDGet(MstProgrammePartTerm MPPT)
        {
            //MstFaculty modelobj = new MstFaculty(); 
            try
            {
                //modelobj.Id = MF.Id;
                SqlCommand cmd = new SqlCommand("MstProgrammePartTermGetByFIDPIDPPIDSID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", MPPT.FacultyId);
                cmd.Parameters.AddWithValue("@ProgrammeId", MPPT.ProgrammeId);
                cmd.Parameters.AddWithValue("@ProgrammePartId", MPPT.ProgrammePartId);
                cmd.Parameters.AddWithValue("@SpecialisationId", MPPT.SpecialisationId);
                sda.SelectCommand = cmd;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                List<MstProgrammePartTerm> ObjLstSpec = new List<MstProgrammePartTerm>();
                //dt.Rows.Add("0", "All Faculty");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstProgrammePartTerm ObjP = new MstProgrammePartTerm();
                        ObjP.ProgrammePartTermId = Convert.ToInt32(dr["Id"]);
                        ObjP.PartTermName = (dr["PartTermName"].ToString());

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


        //#region IncProgInstPartTermGetByFIDPIDPPIDSIDGet
        //[HttpPost]
        //public HttpResponseMessage IncProgInstPartTermGetByFIDPIDPPIDSIDGet(IncProgrammeInstancePartTerm MPPT)
        //{
        //    //MstFaculty modelobj = new MstFaculty(); 
        //    try
        //    {
        //        //modelobj.Id = MF.Id;
        //        SqlCommand cmd = new SqlCommand("IncProgrammeInstPartTermGetByFIDPIDPPIDSID", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@FacultyId", MPPT.FacultyId);
        //        cmd.Parameters.AddWithValue("@ProgrammeId", MPPT.ProgrammeId);
        //        cmd.Parameters.AddWithValue("@ProgrammePartId", MPPT.ProgrammePartId);
        //        cmd.Parameters.AddWithValue("@SpecialisationId", MPPT.SpecialisationId);
        //        cmd.Parameters.AddWithValue("@ProgrammePartTermId", MPPT.ProgrammePartTermId);
        //        sda.SelectCommand = cmd;
        //        DataTable dt = new DataTable();
        //        sda.Fill(dt);
        //        List<IncProgrammeInstancePartTerm> ObjLstSpec = new List<IncProgrammeInstancePartTerm>();
        //        //dt.Rows.Add("0", "All Faculty");
        //        if (dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                IncProgrammeInstancePartTerm ObjP = new IncProgrammeInstancePartTerm();
        //                ObjP.ProgInstPartTermId = Convert.ToInt32(dr["Id"]);
        //                ObjP.InstancePartTermName = (dr["InstancePartTermName"].ToString());

        //                ObjLstSpec.Add(ObjP);
        //            }

        //        }

        //        return Return.returnHttp("200", ObjLstSpec, null);

        //    }
        //    catch (Exception e)
        //    {
        //        return Return.returnHttp("201", e.Message.ToString(), null);

        //    }
        //}
        //#endregion


        #region MstPreferenceGroupGetByAYIDFIDPIDPPIDSPIDGet
        [HttpPost]
        public HttpResponseMessage MstPreferenceGroupGetByAYIDFIDPIDPPIDSPIDGet(MstPreferenceGroup MPG)
        {
            //MstFaculty modelobj = new MstFaculty(); 
            try
            {
                //modelobj.Id = MF.Id;
                SqlCommand cmd = new SqlCommand("MstPreferenceGroupGetByAYIDFIDPIDPPIDSPID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AcademicYearId", MPG.AcademicYearId);
                cmd.Parameters.AddWithValue("@FacultyId", MPG.FacultyId);
                cmd.Parameters.AddWithValue("@ProgrammeId", MPG.ProgrammeId);
                cmd.Parameters.AddWithValue("@SpecialisationId", MPG.SpecialisationId);
                cmd.Parameters.AddWithValue("@ProgrammePartId", MPG.ProgrammePartId);
                cmd.Parameters.AddWithValue("@ProgrammePartTermId", MPG.ProgrammePartTermId);

                sda.SelectCommand = cmd;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                List<MstPreferenceGroup> ObjLstSpec = new List<MstPreferenceGroup>();
                //dt.Rows.Add("0", "All Faculty");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstPreferenceGroup ObjP = new MstPreferenceGroup();
                        ObjP.PreferenceGroupId = Convert.ToInt32(dr["Id"]);
                        ObjP.GroupName = (dr["GroupName"].ToString());
                        ObjP.FacultyId = Convert.ToInt32(dr["FacultyId"]);
                        ObjP.FacultyName = (dr["FacultyName"].ToString());

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

        #region MstPreferenceNumberGetByFacIdGet
        [HttpPost]
        public HttpResponseMessage MstPreferenceNumberGetByFacIdGet(ApplicationGroupPreferenceReport MPG)
        {
            //MstFaculty modelobj = new MstFaculty(); 
            try
            {
                //modelobj.Id = MF.Id;
                SqlCommand cmd = new SqlCommand("MstPreferenceNumberGetByFacId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AcademicYearId", MPG.AcademicYearId);
                cmd.Parameters.AddWithValue("@FacultyId", MPG.FacultyId);
                cmd.Parameters.AddWithValue("@ProgrammeId", MPG.ProgrammeId);
                cmd.Parameters.AddWithValue("@SpecialisationId", MPG.SpecialisationId);
                cmd.Parameters.AddWithValue("@ProgrammePartId", MPG.ProgrammePartId);
                cmd.Parameters.AddWithValue("@ProgrammePartTermId", MPG.ProgrammePartTermId);
                cmd.Parameters.AddWithValue("@PreferenceId", MPG.PreferenceId);

                sda.SelectCommand = cmd;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                List<ApplicationGroupPreferenceReport> ObjLstSpec = new List<ApplicationGroupPreferenceReport>();
                //dt.Rows.Add("0", "All Faculty");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ApplicationGroupPreferenceReport ObjP = new ApplicationGroupPreferenceReport();

                        ObjP.PreferenceNo = Convert.ToInt64(dr["PreferenceNo"]);


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


        #region PreferenceWiseReportGet
        [HttpPost]
        public HttpResponseMessage PreferenceWiseReportGet(PreferenceWiseReport ObjPWR)
        {
            PreferenceWiseReport modelobj = new PreferenceWiseReport();
            modelobj.AcademicYearId = ObjPWR.AcademicYearId;
            modelobj.FacultyId = ObjPWR.FacultyId;
            modelobj.ProgrammeId = ObjPWR.ProgrammeId;
            modelobj.ProgrammePartId = ObjPWR.ProgrammePartId;
            modelobj.ProgrammePartTermId = ObjPWR.ProgrammePartTermId;
            modelobj.PreferenceGroupId = ObjPWR.PreferenceGroupId;
            modelobj.PreferenceNo = ObjPWR.PreferenceNo;
            modelobj.EligibleDegreeId = ObjPWR.EligibleDegreeId;
            modelobj.SpecialisationId = ObjPWR.SpecialisationId;
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();

                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                //Int32 InstituteId = Convert.ToInt32(RSR.InstituteId);

                SqlCommand Cmd = new SqlCommand("PreferenceGroupReport", con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@AcademicYearId", modelobj.AcademicYearId);
                Cmd.Parameters.AddWithValue("@FacultyId", modelobj.FacultyId);
                Cmd.Parameters.AddWithValue("@ProgrammeId", modelobj.ProgrammeId);
                Cmd.Parameters.AddWithValue("@ProgrammePartId", modelobj.ProgrammePartId);
                Cmd.Parameters.AddWithValue("@ProgrammePartTermId", modelobj.ProgrammePartTermId);
                Cmd.Parameters.AddWithValue("@PreferenceGroupId", modelobj.PreferenceGroupId);
                Cmd.Parameters.AddWithValue("@PreferenceNo", modelobj.PreferenceNo);
                Cmd.Parameters.AddWithValue("@EligibleDegreeId", modelobj.EligibleDegreeId);
                Cmd.Parameters.AddWithValue("@SpecialisationId", modelobj.SpecialisationId);
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = Cmd;

                DataTable Dt = new DataTable();
                sda.Fill(Dt);


                Int32 count = 0;
                List<PreferenceWiseReport> ObjLstPWR = new List<PreferenceWiseReport>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        PreferenceWiseReport ObjP = new PreferenceWiseReport();


                        ObjP.ApplicationNo = (Convert.ToString(Dt.Rows[i]["ApplicationNo"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ApplicationNo"]);

                        ObjP.ApplicantUserName = (Convert.ToString(Dt.Rows[i]["ApplicantUserName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ApplicantUserName"]);

                        ObjP.NameAsPerMarksheet = (Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"]);

                        ObjP.MobileNo = (Convert.ToString(Dt.Rows[i]["MobileNo"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["MobileNo"]);

                        ObjP.EmailId = (Convert.ToString(Dt.Rows[i]["EmailId"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["EmailId"]);

                        ObjP.MarkObtained = (((Dt.Rows[i]["MarkObtained"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["MarkObtained"]);

                        ObjP.MarkOutof = (((Dt.Rows[i]["MarkOutof"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["MarkOutof"]);

                        ObjP.Percentage = (Convert.ToString(Dt.Rows[i]["Percentage"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["Percentage"]);

                        ObjP.Gender = (Convert.ToString(Dt.Rows[i]["Gender"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["Gender"]);

                        ObjP.IsPhysicallyChanllenged = (Convert.ToString(Dt.Rows[i]["IsPhysicallyChanllenged"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["IsPhysicallyChanllenged"]);

                        ObjP.ApplicationReservationName = (Convert.ToString(Dt.Rows[i]["ApplicationReservationName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ApplicationReservationName"]);

                        ObjP.EWSCategory = (Convert.ToString(Dt.Rows[i]["EWSCategory"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["EWSCategory"]);

                        ObjP.Location = (Convert.ToString(Dt.Rows[i]["Location"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["Location"]);

                        ObjP.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyName"]);

                        ObjP.PreferenceName = (Convert.ToString(Dt.Rows[i]["PreferenceName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["PreferenceName"]);

                        ObjP.PreferenceCodeName = (Convert.ToString(Dt.Rows[i]["PreferenceCodeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["PreferenceCodeName"]);

                        ObjP.PreferenceGroupId = (Convert.ToInt32(Dt.Rows[i]["PreferenceGroupId"]));

                        ObjP.PreferenceNo = (Convert.ToInt32(Dt.Rows[i]["PreferenceNo"]));

                        ObjP.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);

                        ObjP.PartTermName = (Convert.ToString(Dt.Rows[i]["PartTermName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["PartTermName"]);

                        ObjP.AcademicYearCode = (Convert.ToString(Dt.Rows[i]["AcademicYearCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);

                        count = count + 1;
                        ObjP.IndexId = count;

                        ObjLstPWR.Add(ObjP);



                    }
                    return Return.returnHttp("200", ObjLstPWR, null);

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
