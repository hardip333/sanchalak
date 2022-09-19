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
    public class ReplacePaperController : ApiController 
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));


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



        #region IncProgrammeInstancePartGetByFIDPIDSID
        [HttpPost]
        public HttpResponseMessage IncProgrammeInstancePartGetByFIDPIDSID(ProgrammeInstancePart ObjPIP )
        {
            //MstFaculty modelobj = new MstFaculty(); 
            try
            {
                //modelobj.Id = MF.Id;
                SqlCommand cmd = new SqlCommand("IncProgrammeInstancePartGetByFIDPIDSID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AcademicYearId", ObjPIP.AcademicYearId);
                cmd.Parameters.AddWithValue("@FacultyId", ObjPIP.FacultyId);
                cmd.Parameters.AddWithValue("@ProgrammeId", ObjPIP.ProgrammeId);
                cmd.Parameters.AddWithValue("@SpecialisationId", ObjPIP.SpecialisationId);
                sda.SelectCommand = cmd;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                List<ProgrammeInstancePart> ObjLstPIP = new List<ProgrammeInstancePart>();
                //dt.Rows.Add("0", "All Faculty");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ProgrammeInstancePart ObjPI = new ProgrammeInstancePart();
                        ObjPI.ProgrammeInstancePartId = Convert.ToInt32(dr["Id"]);
                        ObjPI.InstancePartName = (dr["InstancePartName"].ToString());

                        ObjLstPIP.Add(ObjPI);
                    }

                }

                return Return.returnHttp("200", ObjLstPIP, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion



        #region IncProgInstPartTermGetByAIDFIDPIDSIDPIPID
        [HttpPost]
        public HttpResponseMessage IncProgInstPartTermGetByAIDFIDPIDSIDPIPID(ProgrammeInstancePartTerm ObjPIPT)
        {
            //MstFaculty modelobj = new MstFaculty(); 
            try
            {
                //modelobj.Id = MF.Id;
                SqlCommand cmd = new SqlCommand("IncProgInstPartTermGetByAIDFIDPIDSIDPIPID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AcademicYearId", ObjPIPT.AcademicYearId);
                cmd.Parameters.AddWithValue("@FacultyId", ObjPIPT.FacultyId);
                cmd.Parameters.AddWithValue("@ProgrammeId", ObjPIPT.ProgrammeId);
                cmd.Parameters.AddWithValue("@SpecialisationId", ObjPIPT.SpecialisationId);
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartId", ObjPIPT.ProgrammeInstancePartId);
                sda.SelectCommand = cmd;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                List<ProgrammeInstancePartTerm> ObjLstPIPT = new List<ProgrammeInstancePartTerm>();
                //dt.Rows.Add("0", "All Faculty");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ProgrammeInstancePartTerm ObjPIT = new ProgrammeInstancePartTerm();
                        ObjPIT.IncProgInstancePartTermId = Convert.ToInt64(dr["Id"]);
                        ObjPIT.InstancePartTermName = (dr["InstancePartTermName"].ToString());

                        ObjLstPIPT.Add(ObjPIT);
                    }

                }

                return Return.returnHttp("200", ObjLstPIPT, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion



        #region SourcePaperIdGet
        [HttpPost]
        public HttpResponseMessage SourcePaperIdGet(IncProgInstPartTermPaperMap ObjIPI)
        {
            //MstFaculty modelobj = new MstFaculty(); 
            try
            {
                //modelobj.Id = MF.Id;
                SqlCommand cmd = new SqlCommand("SourcePaperId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ObjIPI.IncProgInstancePartTermId);
                
                sda.SelectCommand = cmd;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                List<IncProgInstPartTermPaperMap> ObjLstPIPT = new List<IncProgInstPartTermPaperMap>();
                //dt.Rows.Add("0", "All Faculty");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        IncProgInstPartTermPaperMap ObjPIT = new IncProgInstPartTermPaperMap();
                        ObjPIT.SourcePaperId = Convert.ToInt64(dr["PaperId"]);
                        ObjPIT.SourcePaperNameCode = (dr["SourcePaperNameCode"].ToString());

                        ObjLstPIPT.Add(ObjPIT);
                    }

                }

                return Return.returnHttp("200", ObjLstPIPT, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion


        #region DestinationPaperIdGet
        [HttpPost]
        public HttpResponseMessage DestinationPaperIdGet(IncProgInstPartTermPaperMap ObjIPI)
        {
            //MstFaculty modelobj = new MstFaculty(); 
            try
            {
                //modelobj.Id = MF.Id;
                SqlCommand cmd = new SqlCommand("DestinationPaperId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ObjIPI.IncProgInstancePartTermId);

                sda.SelectCommand = cmd;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                List<IncProgInstPartTermPaperMap> ObjLstPIPT = new List<IncProgInstPartTermPaperMap>();
                //dt.Rows.Add("0", "All Faculty");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        IncProgInstPartTermPaperMap ObjPIT = new IncProgInstPartTermPaperMap();
                        ObjPIT.DestinationPaperId = Convert.ToInt64(dr["PaperId"]);
                        ObjPIT.DestinationPaperNameCode = (dr["DestinationPaperNameCode"].ToString());

                        ObjLstPIPT.Add(ObjPIT);
                    }

                }

                return Return.returnHttp("200", ObjLstPIPT, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion


        #region ReplacePaperAdd
        [HttpPost]
        public HttpResponseMessage ReplacePaperAdd(JObject jsonobject)

        {
            ReplacePaper RP = new ReplacePaper();
            dynamic jsonData = jsonobject;
            try
            {
                Int32 FacultyId = Convert.ToInt32(jsonData.FacultyId);
                Int32 ProgrammeId = Convert.ToInt32(jsonData.ProgrammeId);
                Int32 SpecialisationId = Convert.ToInt32(jsonData.SpecialisationId);
                Int32 ProgrammePartTermId = Convert.ToInt32(jsonData.ProgrammePartTermId);
                Int64 IncProgInstancePartTermId = Convert.ToInt32(jsonData.IncProgInstancePartTermId);
                Int32 SourcePaperId = Convert.ToInt32(jsonData.SourcePaperId);
                Int32 DestinationPaperId = Convert.ToInt32(jsonData.DestinationPaperId);
                

                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                Int64 UserId = Convert.ToInt64(responseToken);

                SqlCommand cmd = new SqlCommand("", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                cmd.Parameters.AddWithValue("@ProgrammeId", ProgrammeId);
                cmd.Parameters.AddWithValue("@SpecialisationId", SpecialisationId);
                cmd.Parameters.AddWithValue("@ProgrammePartTermId", ProgrammePartTermId);
                cmd.Parameters.AddWithValue("@IncProgInstancePartTermId", IncProgInstancePartTermId);
                cmd.Parameters.AddWithValue("@SourcePaperId", SourcePaperId);
                cmd.Parameters.AddWithValue("@DestinationPaperId", DestinationPaperId);
                //cmd.Parameters.AddWithValue("@IncProgrammePartTermSource", IncProgrammePartTermSource);
                //cmd.Parameters.AddWithValue("@IncProgrammePartTermDestination", IncProgrammePartTermDestination);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();
                if (strMessage == "TRUE")
                {
                    strMessage = "Your Data Has Been Added";
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
