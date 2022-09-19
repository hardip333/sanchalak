using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using MSUISApi.BAL;
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
    public class InstitutePartTermMapController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();

        #region InstitutePartTermMapGet
        [HttpPost]
        public HttpResponseMessage InstitutePartTermMapGet(InstitutePartTermMap IPM)
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

                SqlCommand cmd = new SqlCommand("MstInstPartTermMapGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InstituteId", IPM.InstituteId);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Int32 count = 0;

                List<InstitutePartTermMap> ObjListInstPTM = new List<InstitutePartTermMap>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        InstitutePartTermMap ObjIPTM = new InstitutePartTermMap();

                        ObjIPTM.Id = Convert.ToInt32(dr["Id"].ToString());

                        ObjIPTM.InstituteId = Convert.ToInt32(dr["InstituteId"].ToString());

                        ObjIPTM.ProgramInstancePartTermId = Convert.ToInt64(dr["ProgramInstancePartTermId"].ToString());

                        ObjIPTM.InstancePartTermName = dr["InstancePartTermName"].ToString();

                        ObjIPTM.InstituteName = dr["InstituteName"].ToString();

                        ObjIPTM.ProgrammeName = dr["ProgrammeName"].ToString();

                        ObjIPTM.FacultyName = dr["FacultyName"].ToString();

                        ObjIPTM.AcademicYearCode = dr["AcademicYearCode"].ToString();

                        ObjIPTM.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
                        // ObjIPTM.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjIPTM.IsActiveSts = dr["IsActiveSts"].ToString();
                        ObjListInstPTM.Add(ObjIPTM);

                        count = count + 1;
                        ObjIPTM.IndexId = count;
                    }
                    return Return.returnHttp("200", ObjListInstPTM, null);
                }
                else
                {
                    return Return.returnHttp("201", "Record Not Found", null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion


        #region MstInstituteGet
        [HttpPost]
        public HttpResponseMessage MstInstituteGet()
        {
            try
            {
                //String token = Request.Headers.GetValues("token").FirstOrDefault();
                //TokenOperation ac = new TokenOperation();

                //string res = ac.ValidateToken(token);

                //if (res == "0")
                //{
                //    return Return.returnHttp("0", null, null);
                //}

                List<MstInstitute> ObjLstInstitute = new List<MstInstitute>();
                BALInstituteList ObjBalInstituteList = new BALInstituteList();
                ObjLstInstitute = ObjBalInstituteList.InstituteListGet();

                if (ObjLstInstitute != null)
                    return Return.returnHttp("200", ObjLstInstitute, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region FacultyGet
        [HttpPost]
        public HttpResponseMessage FacultyGet()
        {
            try
            {


                SqlCommand Cmd = new SqlCommand("MstFacultyGet", con);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);


                List<MstFaculty> ObjLstFaculty = new List<MstFaculty>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstFaculty objFaculty = new MstFaculty();

                        objFaculty.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objFaculty.FacultyName = Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objFaculty.FacultyCode = Convert.ToString(Dt.Rows[i]["FacultyCode"]);
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


        #region MstProgrammeListGetByFacultyId
        [HttpPost]
        public HttpResponseMessage MstProgrammeGetByFacId(MstProgramme mprog)
        {
            try
            {
                //MstProgramme mprog = new MstProgramme();

                //dynamic Jsondata = BOS;

                //mprog.FacultyId = Convert.ToInt32(Jsondata.FacultyId);

                SqlCommand cmd = new SqlCommand("MstPrgGetByFacId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@InstituteId", mprog.InstituteId);
                cmd.Parameters.AddWithValue("@FacultyId", mprog.FacultyId);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = cmd;

                Da.Fill(Dt);

                List<MstProgramme> ObjLstProg = new List<MstProgramme>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgramme mstpro = new MstProgramme();

                        mstpro.Id = (((Dt.Rows[i]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["Id"]);
                        mstpro.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        mstpro.ProgrammeCode = (Convert.ToString(Dt.Rows[i]["ProgrammeCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeCode"]);
                        mstpro.FacultyId = (((Dt.Rows[i]["FacultyId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        mstpro.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyName"]);

                        ObjLstProg.Add(mstpro);
                    }
                }
                return Return.returnHttp("200", ObjLstProg, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region IncProgInstPartTermGetByFacIdAcIdProgId
        [HttpPost]
        public HttpResponseMessage IncProgInstPartTermGetByFacIdAcIdProgId(ProgrammeInstancePartTerm ObjProgInstPartTerm)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("IncProgInstPartTermGetByFacIdAcIdProgId", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@InstituteId", ObjProgInstPartTerm.InstituteId);
                da.SelectCommand.Parameters.AddWithValue("@ProgrammeId", ObjProgInstPartTerm.ProgrammeId);
                da.SelectCommand.Parameters.AddWithValue("@AcademicYearId", ObjProgInstPartTerm.AcademicYearId);
                DataTable dt = new DataTable();
                da.Fill(dt);


                List<ProgrammeInstancePartTerm> ObjLstProgInstPartTerm = new List<ProgrammeInstancePartTerm>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePartTerm ObjProgPartTerm = new ProgrammeInstancePartTerm();
                        ObjProgPartTerm.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjProgPartTerm.InstancePartTermName = Convert.ToString(dt.Rows[i]["InstancePartTermName"]);
                        ObjProgPartTerm.ProgChecked = Convert.ToBoolean(dt.Rows[i]["ProgChecked"]);
                        ObjProgPartTerm.InstitutePartTermMapId = (((dt.Rows[i]["InstitutePartTermMapId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(dt.Rows[i]["InstitutePartTermMapId"]);
                        ObjProgPartTerm.IncProgInstancePartTermId = (((dt.Rows[i]["IncProgInstancePartTermId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(dt.Rows[i]["IncProgInstancePartTermId"]);



                        ObjLstProgInstPartTerm.Add(ObjProgPartTerm);

                    }

                    return Return.returnHttp("200", ObjLstProgInstPartTerm, null);
                }
                else
                {
                    return Return.returnHttp("201", "No Record Found", null);
                }

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
                //String token = Request.Headers.GetValues("token").FirstOrDefault();
                //TokenOperation TokenOp = new TokenOperation();
                //String ValidTok = TokenOp.ValidateToken(token);

                //if (ValidTok == "0")
                //{
                //    return Return.returnHttp("0", null, null);
                //}

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


        #region InstitutePartTermMapAdd

        [HttpPost]
        public HttpResponseMessage InstitutePartTermMapAdd(InstitutePartTermMap ObjInstPT)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                //String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
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
                MstInstituteProgrammeMap ObjIPM = new MstInstituteProgrammeMap();

                var ProgList = ObjInstPT.ProgList;
                foreach (ProgInstPTData IPM in ProgList)
                {
                    Int32 InstitutePartTermMapId = Convert.ToInt32(IPM.InstitutePartTermMapId);
                    Int32 IncProgInstancePartTermId = Convert.ToInt32(IPM.IncProgInstancePartTermId);

                    if (IPM.ProgChecked)
                    {
                        //MstInstituteProgrammeMap MIPM = new MstInstituteProgrammeMap();
                        Int32 InstituteId = Convert.ToInt32(ObjInstPT.InstituteId);
                        //Int32 ProgrammeId = Convert.ToInt32(ObjPDList.ProgrammeId);
                        Int64 UserId = Convert.ToInt64(res.ToString());

                        SqlCommand cmd = new SqlCommand("InstitutePartTermMapAdd", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@InstituteId", InstituteId);
                        cmd.Parameters.AddWithValue("@IncProgInstancePartTermId", IncProgInstancePartTermId);
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@UserTime", datetime);
                        cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                        cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                        con.Open();
                        cmd.ExecuteNonQuery();
                        string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                        con.Close();
                        if (string.Equals(strMessage, "TRUE"))
                        {
                            strMessage = "Your data has been saved successfully.";
                        }

                    }
                    else
                    {
                        Int32 InstituteId = Convert.ToInt32(ObjInstPT.InstituteId);
                        //Int32 InstituteProgrammeMapId = Convert.ToInt32(IPM.InstituteProgrammeMapId);
                        //Int32 ProgrammeId = Convert.ToInt32(IPM.ProgrammeId);
                        SqlCommand cmd = new SqlCommand("InstitutePartTermMap_Delete", con);
                        cmd.CommandType = CommandType.StoredProcedure;



                        cmd.Parameters.AddWithValue("@InstituteId", InstituteId);
                        //cmd.Parameters.AddWithValue("@InstituteProgrammeMapId", IPM.InstituteProgrammeMapId);
                        cmd.Parameters.AddWithValue("@IncProgInstancePartTermId", IncProgInstancePartTermId);
                        cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                        cmd.Parameters["@Message"].Direction = ParameterDirection.Output;



                        con.Open();
                        cmd.ExecuteNonQuery();
                        string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                        con.Close();



                        if (string.Equals(strMessage, "TRUE"))
                        {
                            strMessage = "Your data has been modified successfully.";
                        }
                    }
                }

                return Return.returnHttp("200", "Your Data Has Been Add / Modified Successfully", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion
    }
}