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
    public class MstInstituteProgrammeMapController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();

        #region MstInstituteProgrammeMapGet
        [HttpPost]
        public HttpResponseMessage MstInstituteProgrammeMapGet(MstInstituteProgrammeMap MIPM)
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

                SqlCommand cmd = new SqlCommand("MstInstituteProgrammeMapGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InstituteId", MIPM.InstituteId);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                //cmd.Parameters.AddWithValue("@ProgrammeId", MIPM.ProgrammeId);
                sda.Fill(dt);
                Int32 count = 0;

                List<MstInstituteProgrammeMap> ObjMIPLists = new List<MstInstituteProgrammeMap>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstInstituteProgrammeMap Objmip = new MstInstituteProgrammeMap();

                        Objmip.Id = Convert.ToInt32(dr["Id"].ToString());
                        Objmip.InstituteId = Convert.ToInt32(dr["InstituteId"].ToString());
                        Objmip.ProgrammeId = Convert.ToInt32(dr["ProgrammeId"]);
                        Objmip.FacultyId = Convert.ToInt32(dr["FacultyId"]);
                        Objmip.InstituteName = dr["InstituteName"].ToString();
                        Objmip.ProgrammeName = dr["ProgrammeName"].ToString();
                        Objmip.FacultyName = dr["FacultyName"].ToString();
                        Objmip.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
                        Objmip.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        Objmip.IsActiveSts = dr["IsActiveSts"].ToString();

                        count = count + 1;
                        Objmip.IndexId = count;

                        ObjMIPLists.Add(Objmip);
                    }
                    return Return.returnHttp("200", ObjMIPLists, null);
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

        #region MstInstituteProgrammeMapAdd

        [HttpPost]
        public HttpResponseMessage MstInstituteProgrammeMapAdd(MstInstituteProgrammeMap ObjPDList)
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

                var ProgList = ObjPDList.ProgList;
                foreach (ProgData IPM in ProgList)
                {
                    Int32 InstituteProgrammeId = Convert.ToInt32(IPM.InstituteProgrammeMapId);
                    Int32 ProgrammeId = Convert.ToInt32(IPM.ProgrammeId);

                    if (IPM.ProgChecked)
                    {
                        //MstInstituteProgrammeMap MIPM = new MstInstituteProgrammeMap();
                        Int32 InstituteId = Convert.ToInt32(ObjPDList.InstituteId);
                        //Int32 ProgrammeId = Convert.ToInt32(ObjPDList.ProgrammeId);
                        Int64 UserId = Convert.ToInt64(res.ToString());

                        SqlCommand cmd = new SqlCommand("MstInstituteProgrammeMapAdd", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@InstituteId", InstituteId);
                        cmd.Parameters.AddWithValue("@ProgrammeId", ProgrammeId);
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
                        Int32 InstituteId = Convert.ToInt32(ObjPDList.InstituteId);
                        //Int32 InstituteProgrammeMapId = Convert.ToInt32(IPM.InstituteProgrammeMapId);
                        //Int32 ProgrammeId = Convert.ToInt32(IPM.ProgrammeId);
                        SqlCommand cmd = new SqlCommand("MstInstituteProgrammeMap_Delete", con);
                        cmd.CommandType = CommandType.StoredProcedure;



                        cmd.Parameters.AddWithValue("@InstituteId", InstituteId);
                        //cmd.Parameters.AddWithValue("@InstituteProgrammeMapId", IPM.InstituteProgrammeMapId);
                        cmd.Parameters.AddWithValue("@ProgrammeId", ProgrammeId);
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

        #region MstInstituteGet
        [HttpPost]
        public HttpResponseMessage MstInstituteGet()
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
                        /*objFaculty.FacultyAddress = Convert.ToString(Dt.Rows[i]["FacultyAddress"]);
                        objFaculty.CityName = Convert.ToString(Dt.Rows[i]["CityName"]);
                        objFaculty.Pincode = Convert.ToInt32(Dt.Rows[i]["Pincode"]);
                        objFaculty.FacultyContactNo = Convert.ToString(Dt.Rows[i]["FacultyContactNo"]);
                        objFaculty.FacultyFaxNo = Convert.ToString(Dt.Rows[i]["FacultyFaxNo"]);
                        objFaculty.FacultyEmail = Convert.ToString(Dt.Rows[i]["FacultyEmail"]);
                        objFaculty.FacultyUrl = Convert.ToString(Dt.Rows[i]["FacultyUrl"]);
                        objFaculty.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);*/
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

        #region MstProgrammeListGet
        [HttpPost]
        public HttpResponseMessage MstProgrammeListGet()
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

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter sda = new SqlDataAdapter("MstProgrammeGet", con);

                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                //sda.SelectCommand.Parameters.AddWithValue("@Flag", "MstProgrammeListGet");
                //sda.SelectCommand.Parameters.Add("@Message", SqlDbType.NVarChar).Value = "True";
                DataTable dt = new DataTable();
                sda.Fill(dt);

                List<MstProgramme> ProgrammeLists = new List<MstProgramme>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstProgramme prg = new MstProgramme();
                        prg.Id = Convert.ToInt32(dr["Id"].ToString());
                        prg.ProgrammeName = dr["ProgrammeName"].ToString();
                        prg.ProgrammeCode = dr["ProgrammeCode"].ToString();
                        prg.ProgrammeDescription = dr["ProgrammeDescription"].ToString();
                        prg.FacultyId = Convert.ToInt32(dr["FacultyId"].ToString());
                        prg.FacultyName = dr["FacultyName"].ToString();
                        prg.ProgrammeLevelId = Convert.ToInt32(dr["ProgrammeLevelId"].ToString());
                        prg.ProgrammeLevelName = dr["ProgrammeLevelName"].ToString();
                        prg.ProgrammeTypeId = Convert.ToInt32(dr["ProgrammeTypeId"].ToString());
                        prg.ProgrammeTypeName = dr["ProgrammeTypeName"].ToString();
                        prg.ProgrammeModeId = Convert.ToInt32(dr["ProgrammeModeId"].ToString());
                        prg.ProgrammeModeName = dr["ProgrammeModeName"].ToString();
                        prg.EvaluationId = Convert.ToInt32(dr["EvaluationId"].ToString());
                        prg.EvaluationName = dr["EvaluationName"].ToString();
                        prg.InstructionMediumId = Convert.ToInt32(dr["InstructionMediumId"].ToString());
                        prg.InstructionMediumName = dr["InstructionMediumName"].ToString();
                        prg.IsCBCS = Convert.ToBoolean(dr["IsCBCS"].ToString());
                        prg.IsSepartePassingHead = Convert.ToBoolean(dr["IsSepartePassingHead"].ToString());
                        prg.MaxMarks = Convert.ToInt32(dr["MaxMarks"].ToString());
                        prg.MinMarks = Convert.ToInt32(dr["MinMarks"].ToString());
                        prg.MaxCredits = Convert.ToDecimal(dr["MaxCredits"].ToString());
                        prg.MinCredits = Convert.ToDecimal(dr["MinCredits"].ToString());
                        prg.IsActiveSts = dr["IsActiveSts"].ToString();
                        prg.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
                        prg.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());

                        ProgrammeLists.Add(prg);
                    }

                }
                return Return.returnHttp("200", ProgrammeLists, null);


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

                SqlCommand cmd = new SqlCommand("MstProgrammeGetByFId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InstituteId", mprog.InstituteId);
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
                        mstpro.ProgChecked = Convert.ToBoolean(Dt.Rows[i]["ProgChecked"]);
                        mstpro.InstituteProgrammeMapId = (((Dt.Rows[i]["InstituteProgrammeMapId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["InstituteProgrammeMapId"]);
                        mstpro.ProgrammeId = (((Dt.Rows[i]["ProgrammeId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);
                        mstpro.FacultyId = (((Dt.Rows[i]["FacultyId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        mstpro.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        mstpro.InstituteId = (((Dt.Rows[i]["InstituteId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["InstituteId"]);
                        //mstpro.InstituteName = (Convert.ToString(Dt.Rows[i]["InstituteName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstituteName"]);

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
    }
}