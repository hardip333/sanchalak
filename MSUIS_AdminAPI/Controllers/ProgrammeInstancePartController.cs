using MSUIS_TokenManager.App_Start;
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
    public class ProgrammeInstancePartController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        Validation validation = new Validation();

        #region ProgrammeInstancePartGet
        [HttpPost]
        public HttpResponseMessage ProgrammeInstancePartGet()
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("IncProgrammeInstancePartGet", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                // da.SelectCommand.Parameters.AddWithValue("@Flag", "AdmProgramInstancePartListGet");
                DataTable dt = new DataTable();
                da.Fill(dt);
                Int32 count = 0;
                List<ProgrammeInstancePart> ObjLstProgInstPart = new List<ProgrammeInstancePart>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePart ObjProgPart = new ProgrammeInstancePart();
                        ObjProgPart.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjProgPart.FacultyName = (Convert.ToString(dt.Rows[i]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["FacultyName"]);
                        ObjProgPart.FacultyId = (Convert.ToString(dt.Rows[i]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[i]["FacultyId"]);
                        
                        ObjProgPart.ProgrammeInstanceId = (Convert.ToString(dt.Rows[i]["ProgrammeInstanceId"])).IsEmpty() ? 0 : Convert.ToInt64(dt.Rows[i]["ProgrammeInstanceId"]);
                        ObjProgPart.InstanceName = (Convert.ToString(dt.Rows[i]["InstanceName"])).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["InstanceName"]);
                        ObjProgPart.ProgrammeId = (Convert.ToString(dt.Rows[i]["ProgrammeId"])).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[i]["ProgrammeId"]);
                        ObjProgPart.ProgrammeName = (Convert.ToString(dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["ProgrammeName"]);
                        //ObjProgPart.SpecialisationId = Convert.ToInt32(dt.Rows[i]["SpecialisationId"]);
                        //ObjProgPart.BranchName = Convert.ToString(dt.Rows[i]["BranchName"]);
                        ObjProgPart.ProgrammePartId = (Convert.ToString(dt.Rows[i]["PorgrammePartId"])).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[i]["PorgrammePartId"]);
                        ObjProgPart.AcademicYearCode = (Convert.ToString(dt.Rows[i]["AcademicYearCode"])).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["AcademicYearCode"]);
                        ObjProgPart.AcademicYearId = (Convert.ToString(dt.Rows[i]["AcademicYearId"])).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[i]["AcademicYearId"]);
                        ObjProgPart.ProgrammePartName = (Convert.ToString(dt.Rows[i]["PartName"])).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["PartName"]);
                        ObjProgPart.PartShortName = (Convert.ToString(dt.Rows[i]["PartShortName"])).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["PartShortName"]);
                        ObjProgPart.SequenceNo = (Convert.ToString(dt.Rows[i]["SequenceNo"])).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[i]["SequenceNo"]);
                        ObjProgPart.ExamPatternId = (Convert.ToString(dt.Rows[i]["ExamPatternId"])).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[i]["ExamPatternId"]);
                        ObjProgPart.ExamPatternName = (Convert.ToString(dt.Rows[i]["ExaminationPatternName"])).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["ExaminationPatternName"]);
                        ObjProgPart.MaxMarks = (Convert.ToString(dt.Rows[i]["MaxMarks"])).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[i]["MaxMarks"]);
                        ObjProgPart.MinMarks = (Convert.ToString(dt.Rows[i]["MinMarks"])).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[i]["MinMarks"]);
                        ObjProgPart.IsSeparatePassingHead = (Convert.ToString(dt.Rows[i]["IsSeparatePassingHead"])).IsEmpty() ? false : Convert.ToBoolean(dt.Rows[i]["IsSeparatePassingHead"]);
                        ObjProgPart.IsSepratePassHeadSts = (Convert.ToString(dt.Rows[i]["IsSeparatePassingHeadSts"])).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["IsSeparatePassingHeadSts"]);
                        ObjProgPart.IsActive = (Convert.ToString(dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(dt.Rows[i]["IsActive"]);
                        ObjProgPart.IsActiveSts = (Convert.ToString(dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["IsActiveSts"]);
                        ObjProgPart.IsDeleted = (Convert.ToString(dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(dt.Rows[i]["IsDeleted"]); ;
                        //ObjProgInst.CreatedBy = Convert.ToInt64(dt.Rows[i]["CreatedBy"]);
                        //ObjProgInst.CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]);
                        //ObjProgInst.ModifiedBy = Convert.ToInt64(dt.Rows[i]["ModifiedBy"]);
                        //ObjProgInst.ModifiedOn = Convert.ToDateTime(dt.Rows[i]["ModifiedOn"]);
                        count = count + 1;
                        ObjProgPart.IndexId = count;

                        ObjLstProgInstPart.Add(ObjProgPart);

                    }
                    return Return.returnHttp("200", ObjLstProgInstPart, null);
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

        #region PreProgrammePartGetByFacultyId
        [HttpPost]
        public HttpResponseMessage PreProgrammePartGetByFacultyId(ProgramInstance ObjProgInst)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("PreIncProgrammeInstancePartGetByFacId", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@InstituteId", ObjProgInst.InstituteId);
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
                        ObjProgPart.ProgrammeInstanceId = Convert.ToInt64(dt.Rows[i]["ProgrammeInstanceId"]);
                        ObjProgPart.InstanceName = Convert.ToString(dt.Rows[i]["InstName"]);
                        ObjProgPart.ProgrammeId = Convert.ToInt32(dt.Rows[i]["ProgrammeId"]);
                        //ObjProgPart.SpecialisationId = Convert.ToInt32(dt.Rows[i]["SpecialisationId"]);
                        //ObjProgPart.BranchName = Convert.ToString(dt.Rows[i]["BranchName"]);
                        ObjProgPart.ProgrammePartId = Convert.ToInt32(dt.Rows[i]["PorgrammePartId"]);
                        //ObjProgPart.AcademicYearId = Convert.ToInt32(dt.Rows[i]["AcademicYearId"]);
                        //ObjProgPart.AcademicYearCode = Convert.ToString(dt.Rows[i]["AcademicYearCode"]);
                        ObjProgPart.ProgrammePartName = Convert.ToString(dt.Rows[i]["PartName"]);
                        ObjProgPart.PartShortName = Convert.ToString(dt.Rows[i]["PartShortName"]);
                        ObjProgPart.SequenceNo = Convert.ToInt32(dt.Rows[i]["SequenceNo"]);
                        ObjProgPart.ExamPatternId = Convert.ToInt32(dt.Rows[i]["ExamPatternId"]);
                        ObjProgPart.ExamPatternName = Convert.ToString(dt.Rows[i]["ExaminationPatternName"]);
                        var MaxMarks1 = dt.Rows[i]["MaxMarks"];
                        if (MaxMarks1 is DBNull)
                        {                                               
                            ObjProgPart.MaxMarks1 = null;
                        }
                        else
                        {
                            ObjProgPart.MaxMarks1 = Convert.ToInt32(dt.Rows[i]["MaxMarks"]);
                        }
                        var MinMarks1 = dt.Rows[i]["MinMarks"];
                        if (MinMarks1 is DBNull)
                        {                         
                            ObjProgPart.MinMarks1 = null;
                        }
                        else
                        {
                            ObjProgPart.MinMarks1 = Convert.ToInt32(dt.Rows[i]["MinMarks"]);
                        }
                        //ObjProgPart.MaxMarks = Convert.ToInt32(dt.Rows[i]["MaxMarks"]);
                        //ObjProgPart.MinMarks = Convert.ToInt32(dt.Rows[i]["MinMarks"]);
                        ObjProgPart.IsSeparatePassingHead = Convert.ToBoolean(dt.Rows[i]["IsSeparatePassingHead"]);
                        ObjProgPart.IsActive = Convert.ToBoolean(dt.Rows[i]["IsActive"]);
                        ObjProgPart.IsActiveSts = Convert.ToString(dt.Rows[i]["IsActiveSts"]);
                        ObjProgPart.IsDeleted = Convert.ToBoolean(dt.Rows[i]["IsDeleted"]);
                        //ObjProgInst.CreatedBy = Convert.ToInt64(dt.Rows[i]["CreatedBy"]);
                        //ObjProgInst.CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]);
                        //ObjProgInst.ModifiedBy = Convert.ToInt64(dt.Rows[i]["ModifiedBy"]);
                        //ObjProgInst.ModifiedOn = Convert.ToDateTime(dt.Rows[i]["ModifiedOn"]);

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

        #region ProgrammeInstancePartAdd
        [HttpPost]
        public HttpResponseMessage ProgrammeInstancePartAdd(ProgrammeInstancePart ObjProgrammeInstPart)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPart.FacultyId)))
                {
                    return Return.returnHttp("201", "Please select faculty", null);
                }
                /*else if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPart.ProgrammeId)))
                {
                    return Return.returnHttp("201", "Please select programme", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPart.SpecialisationId)))
                {
                    return Return.returnHttp("201", "Please select Specialisation", null);
                }*/
                else if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPart.AcademicYearId)))
                {
                    return Return.returnHttp("201", "Please select AcademicYear", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPart.ProgrammePartId)))
                {
                    return Return.returnHttp("201", "Please select Programme Part", null);
                }
                else if (validation.validateDigit(Convert.ToString(ObjProgrammeInstPart.MaxMarks)) == false)
                {
                    return Return.returnHttp("201", "Please enter Maximum marks", null);
                }
                else if ((validation.validateDigit(Convert.ToString(ObjProgrammeInstPart.MinMarks)) == false) || (ObjProgrammeInstPart.MinMarks > ObjProgrammeInstPart.MaxMarks))
                {
                    return Return.returnHttp("201", "Please enter Minimum marks and respective of Maximum Marks", null);
                }                
                else
                {

                    SqlCommand cmd = new SqlCommand("IncProgrammeInstancePartAdd", con);

                    ProgrammeInstancePart ObjProgPart = new ProgrammeInstancePart();


                    ObjProgPart.ProgrammeInstanceId = ObjProgrammeInstPart.ProgrammeInstanceId;
                    //ObjProgPart.ProgrammeId = ObjProgrammeInstPart.ProgrammeId;
                    //ObjProgPart.SpecialisationId = ObjProgrammeInstPart.SpecialisationId;
                    ObjProgPart.AcademicYearId = ObjProgrammeInstPart.AcademicYearId;
                    ObjProgPart.ProgrammePartId = ObjProgrammeInstPart.ProgrammePartId;
                    ObjProgPart.MaxMarks = ObjProgrammeInstPart.MaxMarks;
                    ObjProgPart.MinMarks = ObjProgrammeInstPart.MinMarks;
                    ObjProgPart.IsSeparatePassingHead = ObjProgrammeInstPart.IsSeparatePassingHead;

                    cmd.CommandType = CommandType.StoredProcedure;

                    //cmd.Parameters.AddWithValue("@ProgrammeId", ObjProgPart.ProgrammeId);
                    cmd.Parameters.AddWithValue("@AcademicYearId", ObjProgPart.AcademicYearId);
                    //cmd.Parameters.AddWithValue("@SpecialisationId", ObjProgPart.SpecialisationId);
                    cmd.Parameters.AddWithValue("@ProgrammeInsatnceId", ObjProgPart.ProgrammeInstanceId);
                    cmd.Parameters.AddWithValue("@ProgrammePartId", ObjProgPart.ProgrammePartId);
                    cmd.Parameters.AddWithValue("@MaxMarks", ObjProgPart.MaxMarks);
                    cmd.Parameters.AddWithValue("@MinMarks", ObjProgPart.MinMarks);
                    cmd.Parameters.AddWithValue("@IsSeparatePassingHead", ObjProgPart.IsSeparatePassingHead);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters.Add("@ProgInstPartId", SqlDbType.Int);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                    cmd.Parameters["@ProgInstPartId"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    con.Close();
                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Your data has been added successfully.";
                        ObjProgPart.Id = Convert.ToInt32(cmd.Parameters["@ProgInstPartId"].Value);
                    }
                    return Return.returnHttp("200", (strMessage.ToString(), ObjProgPart.Id), null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region PreProgrammeInstancePartAdd
        [HttpPost]
        public HttpResponseMessage PreProgrammeInstancePartAdd(ProgrammeInstancePart ObjProgrammeInstPart)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                //if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPart.FacultyId)))
                //{
                //    return Return.returnHttp("201", "Please select faculty", null);
                //}
                //else if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPart.ProgrammeId)))
                //{
                //    return Return.returnHttp("201", "Please select programme", null);
                //}
                //else if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPart.SpecialisationId)))
                //{
                //    return Return.returnHttp("201", "Please select Specialisation", null);
                //}
                //else if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPart.AcademicYearId)))
                //{
                //    return Return.returnHttp("201", "Please select AcademicYear", null);
                //}
                else if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPart.ProgrammePartId)))
                {
                    return Return.returnHttp("201", "Please select Programme Part", null);
                }
                //else if (validation.validateDigit(Convert.ToString(ObjProgrammeInstPart.MaxMarks)) == false)
                //{
                //    return Return.returnHttp("201", "Please enter Maximum marks", null);
                //}
                //else if ((validation.validateDigit(Convert.ToString(ObjProgrammeInstPart.MinMarks)) == false) || (ObjProgrammeInstPart.MinMarks > ObjProgrammeInstPart.MaxMarks))
                //{
                //    return Return.returnHttp("201", "Please enter Minimum marks and respective of Maximum Marks", null);
                //}
                else
                {

                    SqlCommand cmd = new SqlCommand("PreIncProgrammeInstancePartAdd", con);

                    ProgrammeInstancePart ObjProgPart = new ProgrammeInstancePart();


                    //ObjProgPart.ProgrammeInstanceId = JsonData.ProgrammeInstanceId;
                    //ObjProgPart.ProgrammeId = ObjProgrammeInstPart.ProgrammeId;
                    //ObjProgPart.SpecialisationId = ObjProgrammeInstPart.SpecialisationId;
                    //ObjProgPart.AcademicYearId = ObjProgrammeInstPart.AcademicYearId;
                    ObjProgPart.ProgrammeInstanceId = ObjProgrammeInstPart.ProgrammeInstanceId;
                    ObjProgPart.ProgrammePartId = ObjProgrammeInstPart.ProgrammePartId;

                    ObjProgPart.MaxMarks1 = ObjProgrammeInstPart.MaxMarks1;
                  
                    ObjProgPart.MinMarks1 = ObjProgrammeInstPart.MinMarks1;
                    ObjProgPart.IsSeparatePassingHead = ObjProgrammeInstPart.IsSeparatePassingHead;

                    cmd.CommandType = CommandType.StoredProcedure;

                    //cmd.Parameters.AddWithValue("@ProgrammeId", ObjProgPart.ProgrammeId);
                    //cmd.Parameters.AddWithValue("@SpecialisationId", ObjProgPart.SpecialisationId);
                    //cmd.Parameters.AddWithValue("@AcademicYearId", ObjProgPart.AcademicYearId);
                    cmd.Parameters.AddWithValue("@ProgrammeInstanceId", ObjProgPart.ProgrammeInstanceId);
                    cmd.Parameters.AddWithValue("@ProgrammePartId", ObjProgPart.ProgrammePartId);
                    if (ObjProgPart.MaxMarks1 == null)                      
                    cmd.Parameters.AddWithValue("@MaxMarks", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@MaxMarks", ObjProgPart.MaxMarks1);
                    if (ObjProgPart.MinMarks1 == null)
                        cmd.Parameters.AddWithValue("@MinMarks", DBNull.Value);
                    else                    
                    cmd.Parameters.AddWithValue("@MinMarks", ObjProgPart.MinMarks1);
                    cmd.Parameters.AddWithValue("@IsSeparatePassingHead", ObjProgPart.IsSeparatePassingHead);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@ProgInstancePart", SqlDbType.BigInt);
                    cmd.Parameters["@ProgInstancePart"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@ProgPartName", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@ProgPartName"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    string ProgInstancePart = Convert.ToString(cmd.Parameters["@ProgInstancePart"].Value);
                    string ProgPartName = Convert.ToString(cmd.Parameters["@ProgPartName"].Value);
                    con.Close();
                    if (strMessage.Equals("TRUE"))
                    {
                        strMessage = "Data has been saved successfully";
                    }
                    //else
                    //{
                    //    return Return.returnHttp("201", strMessage, null);
                    //}
                    PreProgInstance ObjPPI = new PreProgInstance();

                    ObjPPI.strMessage = strMessage;
                    if (ProgInstancePart == "")
                        ObjPPI.ProgInstancePart = 0;
                    else
                        ObjPPI.ProgInstancePart = Convert.ToInt64(ProgInstancePart.ToString());
                    if (ProgPartName == "")
                        ObjPPI.ProgrammePartName = null;
                    else
                        ObjPPI.ProgrammePartName = ProgPartName;
                    ObjPPI.ProgInstance = 0;
                    ObjPPI.ProgInstancePartTerm = 0;
                    ObjPPI.ProgrammeName = null;                   
                    ObjPPI.ProgrammePartTermName = null;
                    return Return.returnHttp("200", ObjPPI, null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region ProgrammePartGetByProgrammeId
        [HttpPost]
        public HttpResponseMessage ProgrammePartGetByProgrammeId(ProgramInstance ObjProgInst)
        {
            try
            {

                //MstProgrammePartGetByProgrammeIdAndProgInstId
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
                        ObjProgPart.PartShortName = Convert.ToString(dt.Rows[i]["PartShortName"]);
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

        #region MstProgrammePartGetByProgrammeIdAndProgInstId
        [HttpPost]
        public HttpResponseMessage MstProgrammePartGetByProgrammeIdAndProgInstId(ProgramInstance ObjProgInst)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("MstProgrammePartGetByProgrammeIdAndProgInstId", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@ProgrammeId", ObjProgInst.ProgrammeId);
                da.SelectCommand.Parameters.AddWithValue("@ProgrammeInstanceId", ObjProgInst.ProgrammeInstanceId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<ProgrammeInstancePart> ObjLstProgInstPart = new List<ProgrammeInstancePart>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePart ObjProgPart = new ProgrammeInstancePart();
                        ObjProgPart.Id = Convert.ToInt32(dt.Rows[i]["IncProgrammeInstancePartId"]);
                        ObjProgPart.ProgrammeId = Convert.ToInt32(dt.Rows[i]["ProgrammeId"]);
                        ObjProgPart.ProgrammePartName = Convert.ToString(dt.Rows[i]["ProgrammePartName"]);
                        ObjProgPart.PartShortName = Convert.ToString(dt.Rows[i]["PartShortName"]);
                      
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

        #region ProgrammePartGet
        [HttpPost]
        public HttpResponseMessage ProgrammePartGet()
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("MstProgrammePartGet", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                // da.SelectCommand.Parameters.AddWithValue("@ProgrammeId", ObjProgInst.ProgrammeId);
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
                        ObjProgPart.PartShortName = Convert.ToString(dt.Rows[i]["PartShortName"]);
                        ObjLstProgInstPart.Add(ObjProgPart);

                    }
                    return Return.returnHttp("200", ObjLstProgInstPart, null);
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

        #region ProgrammeInstancePartUpdate
        [HttpPost]
        public HttpResponseMessage ProgrammeInstancePartUpdate(ProgrammeInstancePart ObjProgrammeInstPart)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPart.FacultyId)))
                {
                    return Return.returnHttp("201", "Please select faculty", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPart.ProgrammeId)))
                {
                    return Return.returnHttp("201", "Please select programme", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPart.SpecialisationId)))
                {
                    return Return.returnHttp("201", "Please select Specialisation", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPart.AcademicYearId)))
                {
                    return Return.returnHttp("201", "Please select AcademicYear", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPart.ProgrammePartId)))
                {
                    return Return.returnHttp("201", "Please select Programme Part", null);
                }
                else if (validation.validateDigit(Convert.ToString(ObjProgrammeInstPart.MaxMarks)) == false)
                {
                    return Return.returnHttp("201", "Please enter Maximum marks", null);
                }
                else if ((validation.validateDigit(Convert.ToString(ObjProgrammeInstPart.MinMarks)) == false) || (ObjProgrammeInstPart.MinMarks > ObjProgrammeInstPart.MaxMarks))
                {
                    return Return.returnHttp("201", "Please enter Minimum marks and respective of Maximum Marks", null);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("IncProgrammeInstancePartEdit", con);

                    ProgrammeInstancePart ObjProgPart = new ProgrammeInstancePart();


                    //ObjProgPart.ProgrammeInstanceId = JsonData.ProgrammeInstanceId;
                    ObjProgPart.ProgrammeId = ObjProgrammeInstPart.ProgrammeId;
                    ObjProgPart.SpecialisationId = ObjProgrammeInstPart.SpecialisationId;
                    ObjProgPart.AcademicYearId = ObjProgrammeInstPart.AcademicYearId;
                    ObjProgPart.ProgrammePartId = ObjProgrammeInstPart.ProgrammePartId;
                    ObjProgPart.MaxMarks = ObjProgrammeInstPart.MaxMarks;
                    ObjProgPart.MinMarks = ObjProgrammeInstPart.MinMarks;
                    ObjProgPart.IsSeparatePassingHead = ObjProgrammeInstPart.IsSeparatePassingHead;
                    ObjProgPart.Id = ObjProgrammeInstPart.Id;
                    cmd.CommandType = CommandType.StoredProcedure;

                    //cmd.Parameters.AddWithValue("@ProgrammeId", ObjProgPart.ProgrammeId);
                    //cmd.Parameters.AddWithValue("@AcademicYearId", ObjProgPart.AcademicYearId);
                    //cmd.Parameters.AddWithValue("@SpecialisationId", ObjProgPart.SpecialisationId);
                    cmd.Parameters.AddWithValue("@ProgrammePartId", ObjProgPart.ProgrammePartId);
                    cmd.Parameters.AddWithValue("@MaxMarks", ObjProgPart.MaxMarks);
                    cmd.Parameters.AddWithValue("@MinMarks", ObjProgPart.MinMarks);
                    cmd.Parameters.AddWithValue("@IsSeparatePassingHead", ObjProgPart.IsSeparatePassingHead);
                    cmd.Parameters.AddWithValue("@ProgrammeInstancePartId", ObjProgPart.Id);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    con.Close();
                    if (strMessage.Equals("TRUE"))
                    {
                        return Return.returnHttp("200", "Your data has been modified successfully.", null);
                    }
                    else
                    {
                        return Return.returnHttp("201", strMessage, null);
                    }
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region PreProgrammeInstancePartUpdate
        [HttpPost]
        public HttpResponseMessage PreProgrammeInstancePartUpdate(ProgrammeInstancePart ObjProgrammeInstPart)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                //if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPart.FacultyId)))
                //{
                //    return Return.returnHttp("201", "Please select faculty", null);
                //}
                //else if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPart.ProgrammeId)))
                //{
                //    return Return.returnHttp("201", "Please select programme", null);
                //}
                //else if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPart.SpecialisationId)))
                //{
                //    return Return.returnHttp("201", "Please select Specialisation", null);
                //}
                //else if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPart.AcademicYearId)))
                //{
                //    return Return.returnHttp("201", "Please select AcademicYear", null);
                //}
                if (String.IsNullOrEmpty(Convert.ToString(ObjProgrammeInstPart.ProgrammePartId)))
                {
                    return Return.returnHttp("201", "Please select Programme Part", null);
                }
                //else if (validation.validateDigit(Convert.ToString(ObjProgrammeInstPart.MaxMarks)) == false)
                //{
                //    return Return.returnHttp("201", "Please enter Maximum marks", null);
                //}
                //else if ((validation.validateDigit(Convert.ToString(ObjProgrammeInstPart.MinMarks)) == false) || (ObjProgrammeInstPart.MinMarks > ObjProgrammeInstPart.MaxMarks))
                //{
                //    return Return.returnHttp("201", "Please enter Minimum marks and respective of Maximum Marks", null);
                //}
                else
                {
                    SqlCommand cmd = new SqlCommand("PreIncProgrammeInstancePartEdit", con);

                    ProgrammeInstancePart ObjProgPart = new ProgrammeInstancePart();


                    //ObjProgPart.ProgrammeInstanceId = JsonData.ProgrammeInstanceId;
                    //ObjProgPart.ProgrammeId = ObjProgrammeInstPart.ProgrammeId;
                    //ObjProgPart.SpecialisationId = ObjProgrammeInstPart.SpecialisationId;
                    //ObjProgPart.AcademicYearId = ObjProgrammeInstPart.AcademicYearId;
                    ObjProgPart.ProgrammeInstanceId = ObjProgrammeInstPart.ProgrammeInstanceId;
                    ObjProgPart.ProgrammePartId = ObjProgrammeInstPart.ProgrammePartId;
                    ObjProgPart.MaxMarks1 = ObjProgrammeInstPart.MaxMarks1;
                    ObjProgPart.MinMarks1 = ObjProgrammeInstPart.MinMarks1;
                    ObjProgPart.IsSeparatePassingHead = ObjProgrammeInstPart.IsSeparatePassingHead;
                    ObjProgPart.Id = ObjProgrammeInstPart.Id;
                    cmd.CommandType = CommandType.StoredProcedure;

                    //cmd.Parameters.AddWithValue("@ProgrammeId", ObjProgPart.ProgrammeId);
                    //cmd.Parameters.AddWithValue("@AcademicYearId", ObjProgPart.AcademicYearId);
                    //cmd.Parameters.AddWithValue("@SpecialisationId", ObjProgPart.SpecialisationId);
                    cmd.Parameters.AddWithValue("@ProgrammeInstanceId", ObjProgPart.ProgrammeInstanceId);
                    cmd.Parameters.AddWithValue("@ProgrammePartId", ObjProgPart.ProgrammePartId);
                    if (ObjProgPart.MaxMarks1 == null)
                        cmd.Parameters.AddWithValue("@MaxMarks", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@MaxMarks", ObjProgPart.MaxMarks1);
                    if (ObjProgPart.MinMarks1 == null)
                        cmd.Parameters.AddWithValue("@MinMarks", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@MinMarks", ObjProgPart.MinMarks1);
                    cmd.Parameters.AddWithValue("@IsSeparatePassingHead", ObjProgPart.IsSeparatePassingHead);
                    cmd.Parameters.AddWithValue("@ProgrammeInstancePartId", ObjProgPart.Id);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    con.Close();
                    if (strMessage.Equals("TRUE"))
                    {
                        return Return.returnHttp("200", "Data has been modified successfully", null);
                    }
                    else
                    {
                        return Return.returnHttp("201", strMessage, null);
                    }
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region ProgrammeInstancePartDelete
        [HttpPost]
        public HttpResponseMessage ProgrammeInstancePartDelete(ProgrammeInstancePart ObjProgPart)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                //ProgrammeInstancePart ObjProgPart = new ProgrammeInstancePart();

                //dynamic JsonData = JsonObj;
                //ObjProgPart.Id = JsonData.Id;

                SqlCommand cmd = new SqlCommand("IncProgrammeInstancePartDelete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", ObjProgPart.Id);
                //cmd.Parameters.AddWithValue("@UserId", UserId);
                //cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string StrMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                if (StrMessage.Equals("TRUE"))
                {
                    return Return.returnHttp("200", "Your data has been deleted successfully.", null);
                }
                else
                {
                    return Return.returnHttp("200", StrMessage, null);
                }

            }
            catch (Exception e)
            {
                return Return.returnHttp("200", e.Message, null);
            }

        }
        #endregion

        #region ProgrammeInstancePartIsActiveEnable

        [HttpPost]
        public HttpResponseMessage ProgrammeInstancePartIsActiveEnable(ProgrammeInstancePart ObjProgPart)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                

                SqlCommand cmd = new SqlCommand("IncProgrammeInstancePartActive", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammePartId", ObjProgPart.Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.AddWithValue("@Flag", "ProgramInstancePartIsActiveEnable");
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();
                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been Modified successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region ProgrammeInstancePartIsActiveDisable
        [HttpPost]
        public HttpResponseMessage ProgrammeInstancePartIsActiveDisable(ProgrammeInstancePart ObjProgPart)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                               
                SqlCommand cmd = new SqlCommand("IncProgrammeInstancePartActive", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammePartId", ObjProgPart.Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.AddWithValue("@Flag", "ProgramInstancePartIsActiveDisable");
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();
                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been Modified successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region Branch List On the basis of Programme Instance Id
        [HttpPost]
        public HttpResponseMessage MstProgrammeBranchListGetByProgInstanceId(ProgrammeInstancePartTerm ObjProg)
        {
            try
            {
                Request.Headers.TryGetValues("DepartmentId", out IEnumerable<string> DepartmentId1);
                var DepartmentId = DepartmentId1?.FirstOrDefault();
                int ProgInstId = Convert.ToInt32(ObjProg.ProgrammeInstanceId);
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter cmdda = new SqlDataAdapter("MstSpecialisationGetbyProgInstId", con);

                cmdda.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (DepartmentId != null || DepartmentId != "" || DepartmentId != "0")
                {
                    cmdda.SelectCommand.Parameters.AddWithValue("@ProgrammeInstanceId", ProgInstId);
                    cmdda.SelectCommand.Parameters.AddWithValue("@DepartmentId", DepartmentId);
                }
                else
                {
                    cmdda.SelectCommand.Parameters.AddWithValue("@ProgrammeInstanceId", ProgInstId);
                }
                

                DataTable Dt = new DataTable();
                cmdda.Fill(Dt);


                List<MstSpecialisation> ObjLstMstSpecialisation = new List<MstSpecialisation>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstSpecialisation ObjMstSpecialisation = new MstSpecialisation();

                        ObjMstSpecialisation.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        ObjMstSpecialisation.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"]));
                        ObjMstSpecialisation.FacultyId = Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        ObjMstSpecialisation.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"]));
                        //ObjMstSpecialisation.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        ObjMstSpecialisation.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjMstSpecialisation.IsDeleted = Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);

                        ObjLstMstSpecialisation.Add(ObjMstSpecialisation);
                    }

                }

                return Return.returnHttp("200", ObjLstMstSpecialisation, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region Programme Part Term Name List on the basis of Programme Instance Id
        [HttpPost]
        public HttpResponseMessage ProgrammePartTermGetByProgInstId(ProgrammeInstancePartTerm ObjProgInstPartTerm)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("MstProgrammePartTermGetByProgrammeInstanceId", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@ProgrammeInstancePartId", ObjProgInstPartTerm.ProgrammeInstancePartId);
                da.SelectCommand.Parameters.AddWithValue("@SpecialisationId", ObjProgInstPartTerm.SpecialisationId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<ProgrammeInstancePartTerm> ObjLstProgPartTerm = new List<ProgrammeInstancePartTerm>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePartTerm ObjProgPartTerm = new ProgrammeInstancePartTerm();

                        //ObjProgPartTerm.FacultyId = Convert.ToInt32(dt.Rows[i]["FacultyId"]);
                        //ObjProgPartTerm.FacultyName = Convert.ToString(dt.Rows[i]["FacultyName"]);
                        //ObjProgPartTerm.ProgrammeName = Convert.ToString(dt.Rows[i]["ProgrammeName"]);
                        //ObjProgPartTerm.ProgrammeId = Convert.ToInt32(dt.Rows[i]["ProgrammeId"]);
                        ObjProgPartTerm.Id = Convert.ToInt32(dt.Rows[i]["ProgrammeInstancePartTermId"]);
                        ObjProgPartTerm.PartTermShortName = Convert.ToString(dt.Rows[i]["PartTermShortName"]);                        
                        ObjProgPartTerm.InstancePartTermName = Convert.ToString(dt.Rows[i]["InstancePartTermName"]);
                        //ObjProgPartTerm.SequenceNo = Convert.ToInt32(dt.Rows[i]["SequenceNo"]);
                        //ObjProgPartTerm.IsActive = Convert.ToBoolean(dt.Rows[i]["IsActive"]);
                        //ObjProgPartTerm.IsActiveSts = Convert.ToString(dt.Rows[i]["IsActiveSts"]);
                        //ObjProgPartTerm.IsDeleted = Convert.ToBoolean(dt.Rows[i]["IsDeleted"]);
                        ObjProgPartTerm.ProgrammePartTermId = Convert.ToInt32(dt.Rows[i]["ProgrammePartTermId"]);

                        ObjLstProgPartTerm.Add(ObjProgPartTerm);

                    }
                    return Return.returnHttp("200", ObjLstProgPartTerm, null);
                }
                else
                {
                    return Return.returnHttp("404", "No Record Found ++", null);
                }

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region ProgrammePartGetByProgrammeInstanceId
        [HttpPost]
        public HttpResponseMessage ProgrammePartGetByProgInstId(ProgramInstance ObjProgInst)
        {
            try
            {

                //MstProgrammePartGetByProgrammeIdAndProgInstId
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("MstProgrammePartGetByProgInstId", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@ProgrammeInstanceId", ObjProgInst.ProgrammeInstanceId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<ProgrammeInstancePart> ObjLstProgInstPart = new List<ProgrammeInstancePart>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePart ObjProgPart = new ProgrammeInstancePart();
                        //ObjProgPart.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjProgPart.FacultyId = Convert.ToInt32(dt.Rows[i]["FacultyId"]);
                        ObjProgPart.FacultyName = Convert.ToString(dt.Rows[i]["FacultyName"]);
                        ObjProgPart.ProgrammeName = Convert.ToString(dt.Rows[i]["ProgrammeName"]);
                        ObjProgPart.PartShortName = Convert.ToString(dt.Rows[i]["PartShortName"]);
                        ObjProgPart.ProgrammePartId = Convert.ToInt64(dt.Rows[i]["ProgrammePartId"]);
                        ObjProgPart.ProgrammeInstancePartId = Convert.ToInt64(dt.Rows[i]["ProgrammeInstancePartId"]);
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

        //Mohini's Code
        #region GetMstProgrammePartByProgrammeId
        [HttpPost]
        public HttpResponseMessage GetMstProgrammePartByProgrammeId(ProgramInstance ObjProgInst)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("GetMstProgrammePartByProgrammeId", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@ProgrammeId", ObjProgInst.ProgrammeId);
                da.SelectCommand.Parameters.AddWithValue("@ProgrammeInstanceId", ObjProgInst.ProgrammeInstanceId);
                da.SelectCommand.Parameters.AddWithValue("@AcademicYearId", ObjProgInst.AcademicYearId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<ProgrammeInstancePart> ObjLstProgInstPart = new List<ProgrammeInstancePart>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePart ObjProgPart = new ProgrammeInstancePart();
                        ObjProgPart.Id = Convert.ToInt32(dt.Rows[i]["IncProgrammeInstancePartId"]);
                        ObjProgPart.ProgrammeId = Convert.ToInt32(dt.Rows[i]["ProgrammeId"]);
                        ObjProgPart.ProgrammePartName = Convert.ToString(dt.Rows[i]["ProgrammePartName"]);
                        ObjProgPart.PartShortName = Convert.ToString(dt.Rows[i]["PartShortName"]);

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

        #region GetMstSpecialisationByProgId
        [HttpPost]
        public HttpResponseMessage GetMstSpecialisationByProgId(ProgrammeInstancePartTerm ObjProg)
        {
            try
            {
                Request.Headers.TryGetValues("DepartmentId", out IEnumerable<string> DepartmentId1);
                var DepartmentId = DepartmentId1?.FirstOrDefault();
                int ProgInstId = Convert.ToInt32(ObjProg.ProgrammeInstanceId);
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter cmdda = new SqlDataAdapter("GetMstSpecialisationByProgId", con);

                cmdda.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (DepartmentId != null || DepartmentId != "" || DepartmentId != "0")
                {
                    cmdda.SelectCommand.Parameters.AddWithValue("@ProgrammeInstanceId", ProgInstId);
                    cmdda.SelectCommand.Parameters.AddWithValue("@DepartmentId", DepartmentId);
                }
                else
                {
                    cmdda.SelectCommand.Parameters.AddWithValue("@ProgrammeInstanceId", ProgInstId);
                }


                DataTable Dt = new DataTable();
                cmdda.Fill(Dt);


                List<MstSpecialisation> ObjLstMstSpecialisation = new List<MstSpecialisation>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstSpecialisation ObjMstSpecialisation = new MstSpecialisation();

                        ObjMstSpecialisation.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        ObjMstSpecialisation.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"]));
                        ObjMstSpecialisation.FacultyId = Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        ObjMstSpecialisation.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"]));

                        ObjLstMstSpecialisation.Add(ObjMstSpecialisation);
                    }

                }

                return Return.returnHttp("200", ObjLstMstSpecialisation, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region Programme Part Term Name List on the basis of Programme Instance Id
        [HttpPost]
        public HttpResponseMessage ProgrammePTGetByProgIdandBranchId(ProgrammeInstancePartTerm ObjProgInstPartTerm)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("ProgrammePTGetByProgIdandBranchId", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                //da.SelectCommand.Parameters.AddWithValue("@ProgrammeInstancePartId", ObjProgInstPartTerm.ProgrammeInstancePartId);
                da.SelectCommand.Parameters.AddWithValue("@ProgrammeInstanceId", ObjProgInstPartTerm.ProgrammeInstanceId);
                da.SelectCommand.Parameters.AddWithValue("@SpecialisationId", ObjProgInstPartTerm.SpecialisationId);
                da.SelectCommand.Parameters.AddWithValue("@AcademicYearId", ObjProgInstPartTerm.AcademicYearId);
                da.SelectCommand.Parameters.AddWithValue("@ProgrammeInstancePartId", ObjProgInstPartTerm.ProgrammeInstancePartId);

                DataTable dt = new DataTable();
                da.Fill(dt);

                List<ProgrammeInstancePartTerm> ObjLstProgPartTerm = new List<ProgrammeInstancePartTerm>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePartTerm ObjProgPartTerm = new ProgrammeInstancePartTerm();

                        //ObjProgPartTerm.FacultyId = Convert.ToInt32(dt.Rows[i]["FacultyId"]);
                        //ObjProgPartTerm.FacultyName = Convert.ToString(dt.Rows[i]["FacultyName"]);
                        //ObjProgPartTerm.ProgrammeName = Convert.ToString(dt.Rows[i]["ProgrammeName"]);
                        //ObjProgPartTerm.ProgrammeId = Convert.ToInt32(dt.Rows[i]["ProgrammeId"]);
                        ObjProgPartTerm.Id = Convert.ToInt32(dt.Rows[i]["ProgrammeInstancePartTermId"]);
                        ObjProgPartTerm.PartTermShortName = Convert.ToString(dt.Rows[i]["PartTermShortName"]);
                        ObjProgPartTerm.InstancePartTermName = Convert.ToString(dt.Rows[i]["InstancePartTermName"]);
                        //ObjProgPartTerm.SequenceNo = Convert.ToInt32(dt.Rows[i]["SequenceNo"]);
                        //ObjProgPartTerm.IsActive = Convert.ToBoolean(dt.Rows[i]["IsActive"]);
                        //ObjProgPartTerm.IsActiveSts = Convert.ToString(dt.Rows[i]["IsActiveSts"]);
                        //ObjProgPartTerm.IsDeleted = Convert.ToBoolean(dt.Rows[i]["IsDeleted"]);
                        ObjProgPartTerm.ProgrammePartTermId = Convert.ToInt32(dt.Rows[i]["ProgrammePartTermId"]);

                        ObjLstProgPartTerm.Add(ObjProgPartTerm);

                    }
                    return Return.returnHttp("200", ObjLstProgPartTerm, null);
                }
                else
                {
                    return Return.returnHttp("404", "No Record Found ++", null);
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
