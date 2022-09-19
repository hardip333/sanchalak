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
    public class MstProgrammePartTermPaperMapController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
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
     
        #region ProgrammeList Get By FacultyId
        [HttpPost]
        public HttpResponseMessage ProgrammeListGetByFacultyId(MstProgramme ObjMstProg)
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


                Int32 FacultyId  = Convert.ToInt32(ObjMstProg.FacultyId);
                SqlCommand cmd = new SqlCommand("MstProgrammeGetByFacId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                DataTable dt = new DataTable();
                Da.SelectCommand = cmd;
                Da.Fill(dt);

                List<MstProgramme> ObjListMstProg = new List<MstProgramme>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstProgramme ObjMstProgramme = new MstProgramme();
                      
                        ObjMstProgramme.Id = Convert.ToInt32(dr["Id"].ToString());
                        ObjMstProgramme.ProgrammeName = dr["ProgrammeName"].ToString();
                        ObjMstProgramme.ProgrammeCode = dr["ProgrammeCode"].ToString();

                        ObjListMstProg.Add(ObjMstProgramme);
                    }
                }
                return Return.returnHttp("200", ObjListMstProg, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region ProgrammePartGetByProgrammeId
        [HttpPost]
        public HttpResponseMessage ProgrammePartGetByProgrammeId(ProgramInstance ObjProgInst)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("MstProgrammePartGetByProgrammeId", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@ProgrammeId", ObjProgInst.ProgrammeId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<ProgrammeInstancePart> ObjLstProgInstPart = new List<ProgrammeInstancePart>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePart ObjProgPart = new ProgrammeInstancePart();
                        ObjProgPart.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjProgPart.FacultyId = Convert.ToInt32(dt.Rows[i]["FacultyId"]);
                        ObjProgPart.FacultyName = Convert.ToString(dt.Rows[i]["FacultyName"]);
                        ObjProgPart.ProgrammeName = Convert.ToString(dt.Rows[i]["ProgrammeName"]);
                        ObjProgPart.ProgrammePartName = Convert.ToString(dt.Rows[i]["PartName"]);
                        ObjLstProgInstPart.Add(ObjProgPart);

                    }
                    return Return.returnHttp("200", ObjLstProgInstPart, null);
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

        #region ProgrammePartTermGetByPartId
        [HttpPost]
        public HttpResponseMessage ProgrammePartTermGetByPartId(MstProgrammePartTerm ObjProgPartTerm)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("MstProgrammePartTermGetByPartId", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@PartId", ObjProgPartTerm.PartId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<MstProgrammePartTerm> ObjLstMstProgPart = new List<MstProgrammePartTerm>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        MstProgrammePartTerm ObjProgPart = new MstProgrammePartTerm();
                        ObjProgPart.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjProgPart.PartTermName = Convert.ToString(dt.Rows[i]["PartTermName"]);

                        ObjLstMstProgPart.Add(ObjProgPart);

                    }
                    return Return.returnHttp("200", ObjLstMstProgPart, null);
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

        #region Attached Paper List to a particular Programme Instance Part Term
        [HttpPost]
        public HttpResponseMessage AttachedPaperList(MstProgrammePartTermPaperMap ObjProgInstPartTermPaper)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("MstPaperGetByMstProgPartTermId", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                
                da.SelectCommand.Parameters.AddWithValue("@PartTermId", ObjProgInstPartTermPaper.PartTermId);

                DataTable dt = new DataTable();
                da.Fill(dt);

                List<MstProgrammePartTermPaperMap> ObjLstPaper = new List<MstProgrammePartTermPaperMap>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        MstProgrammePartTermPaperMap ObjMPaper = new MstProgrammePartTermPaperMap();

                        ObjMPaper.Id = Convert.ToInt32(dt.Rows[i]["PartTermPaperMapId"]);
                        ObjMPaper.MstProgrammePartTermId = Convert.ToInt64(dt.Rows[i]["MstProgrammePartTermId"]);
                        ObjMPaper.PaperId = Convert.ToInt64(dt.Rows[i]["PaperId"]);
                        ObjMPaper.PaperName = Convert.ToString(dt.Rows[i]["PaperName"]);
                        ObjMPaper.PaperCode = Convert.ToString(dt.Rows[i]["PaperCode"]);
                        ObjMPaper.FacultyId = Convert.ToInt32(dt.Rows[i]["FacultyId"]);
                        ObjMPaper.FacultyName = Convert.ToString(dt.Rows[i]["FacultyName"]);
                        ObjMPaper.SubjectId = Convert.ToInt32(dt.Rows[i]["SubjectId"]);
                        ObjMPaper.SubjectName = Convert.ToString(dt.Rows[i]["SubjectName"]);
                        ObjMPaper.EvaluationId = Convert.ToInt32(dt.Rows[i]["EvaluationId"]);
                        ObjMPaper.EvaluationName = Convert.ToString(dt.Rows[i]["EvaluationName"]);
                        ObjMPaper.IsActiveSts = Convert.ToString(dt.Rows[i]["IsActiveSts"]);
                        ObjMPaper.IsCreditSts = Convert.ToString(dt.Rows[i]["IsCreditSts"]);
                        ObjMPaper.IsSeparatePassingHeadSts = Convert.ToString(dt.Rows[i]["IsSepHeadSts"]);
                        ObjMPaper.Credits = Convert.ToDecimal(dt.Rows[i]["Credits"]);
                        ObjMPaper.MaxMarks = Convert.ToInt32(dt.Rows[i]["MaxMarks"]);
                        ObjMPaper.MinMarks = Convert.ToInt32(dt.Rows[i]["MinMarks"]);
                        //ObjMPaper.NoOfLecturesPerWeek = Convert.ToInt32(dt.Rows[i]["NoOfLecturesPerWeek"]);
                        //ObjProgPartTerm.CreatedBy = Convert.ToInt64(dt.Rows[i]["CreatedBy"]);
                        //ObjProgPartTerm.CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]);
                        //ObjProgPartTerm.ModifiedBy = Convert.ToInt64(dt.Rows[i]["ModifiedBy"]);
                        //ObjProgPartTerm.ModifiedOn = Convert.ToDateTime(dt.Rows[i]["ModifiedOn"]);

                        ObjLstPaper.Add(ObjMPaper);

                    }
                    return Return.returnHttp("200", ObjLstPaper, null);
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

        #region Paper List not attached to a particular Mst Programme Part Term
        [HttpPost]
        public HttpResponseMessage NotAttachedPaperList(MstProgrammePartTermPaperMap ObjProgInstPartTermPaper)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("MstPaperGetNotInMstProgPartTermPaperMap", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@PartTermId", ObjProgInstPartTermPaper.PartTermId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<MstPaper> ObjLstPaper = new List<MstPaper>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        MstPaper ObjMPaper = new MstPaper();

                        ObjMPaper.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjMPaper.PaperName = Convert.ToString(dt.Rows[i]["PaperName"]);
                        ObjMPaper.PaperCode = Convert.ToString(dt.Rows[i]["PaperCode"]);
                        ObjMPaper.FacultyId = Convert.ToInt32(dt.Rows[i]["FacultyId"]);
                        ObjMPaper.FacultyName = Convert.ToString(dt.Rows[i]["FacultyName"]);
                        ObjMPaper.SubjectId = Convert.ToInt32(dt.Rows[i]["SubjectId"]);
                        ObjMPaper.SubjectName = Convert.ToString(dt.Rows[i]["SubjectName"]);
                        ObjMPaper.EvaluationId = Convert.ToInt32(dt.Rows[i]["EvaluationId"]);
                        ObjMPaper.EvaluationName = Convert.ToString(dt.Rows[i]["EvaluationName"]);
                        ObjMPaper.IsActiveSts = Convert.ToString(dt.Rows[i]["IsActiveSts"]);
                        ObjMPaper.IsCreditSts = Convert.ToString(dt.Rows[i]["IsCreditSts"]);
                        ObjMPaper.IsSeparatePassingHeadSts = Convert.ToString(dt.Rows[i]["IsSepHeadSts"]);
                        ObjMPaper.Credits = Convert.ToDecimal(dt.Rows[i]["Credits"]);
                        ObjMPaper.MaxMarks = Convert.ToInt32(dt.Rows[i]["MaxMarks"]);
                        ObjMPaper.MinMarks = Convert.ToInt32(dt.Rows[i]["MinMarks"]);

                        ObjLstPaper.Add(ObjMPaper);

                    }
                    return Return.returnHttp("200", ObjLstPaper, null);
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

        #region Insertion of Record into MstProgInstPartTermPaperMap table
        [HttpPost]
        public HttpResponseMessage MstProgInstPartTermPaperMapAdd(MstProgrammePartTermPaperMap mstprogpmap)
        {
            try
            {
                Validation validation = new Validation();
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
               
                Int64 PartTermId = Convert.ToInt32(mstprogpmap.PartTermId.ToString());
                Int64 PaperId = Convert.ToInt64(mstprogpmap.PaperId.ToString());
                //int PartTermId = Convert.ToInt32(incprogpmap.ProgrammePartTermId);


                SqlCommand cmd = new SqlCommand("MstProgrammePartTermPaperMapAdd", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (String.IsNullOrEmpty(Convert.ToString(PartTermId)))
                {
                    return Return.returnHttp("201", "Please select Programme Instance Part Term", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(PaperId)))
                {
                    return Return.returnHttp("201", "Please enter Paper Name", null);
                }
                else
                {
                    //cmd.Parameters.AddWithValue("@ProgrammeInstanceId", ProgrammeInstanceId);
                    //cmd.Parameters.AddWithValue("@PartTermId", PartTermId);
                    //cmd.Parameters.AddWithValue("@Id", incprogpmap.Id);
                    cmd.Parameters.AddWithValue("@PartTermId", PartTermId);
                    cmd.Parameters.AddWithValue("@PaperId", PaperId);
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
                        strMessage = "Paper has been Attached successfully.";
                    }
                    return Return.returnHttp("200", strMessage.ToString(), null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Deletion of Record from MstProgInstPartTermPaperMap table
        [HttpPost]
        public HttpResponseMessage MstProgInstPartTermPaperMapDelete(MstProgrammePartTermPaperMap incprogpmap)
        {
            try
            {
                Validation validation = new Validation();
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int64 ProgrammeInstancePartTermMapId = Convert.ToInt32(incprogpmap.Id.ToString());


                SqlCommand cmd = new SqlCommand("MstProgPartTermPaperMapDelete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (String.IsNullOrEmpty(Convert.ToString(ProgrammeInstancePartTermMapId)))
                {
                    return Return.returnHttp("201", "Please select Programme Instance Part Term", null);
                }

                else
                {
                    cmd.Parameters.AddWithValue("@Id", ProgrammeInstancePartTermMapId);
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
                        strMessage = "Paper has been Detached successfully.";
                    }
                    return Return.returnHttp("200", strMessage.ToString(), null);
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
