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
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Util;
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class IncProgInstPartTermGroupController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region Academic Year List
        [HttpPost]
        public HttpResponseMessage IncAcademicYearListGet()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter cmdda = new SqlDataAdapter("IncAcademicYearGet", Con);

                cmdda.SelectCommand.CommandType = CommandType.StoredProcedure;

                DataTable Dt = new DataTable();
                cmdda.Fill(Dt);


                List<AcademicYear> ObjLstIncAcademicYear = new List<AcademicYear>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        AcademicYear ObjIncAcademicYear = new AcademicYear();

                        ObjIncAcademicYear.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        ObjIncAcademicYear.AcademicYearCode = Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);
                        ObjIncAcademicYear.IsActiveSts = Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        ObjIncAcademicYear.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjIncAcademicYear.IsDeleted = Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);

                        ObjLstIncAcademicYear.Add(ObjIncAcademicYear);
                    }

                }

                return Return.returnHttp("200", ObjLstIncAcademicYear, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region Faculty List
        [HttpPost]
        public HttpResponseMessage FacultyGet()
        {
            try
            {

                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlCommand Cmd = new SqlCommand("MstFacultyGet", Con);
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
                        //objFaculty.FacultyCode = Convert.ToString(Dt.Rows[i]["FacultyCode"]);
                        //objFaculty.FacultyAddress = Convert.ToString(Dt.Rows[i]["FacultyAddress"]);
                        //objFaculty.CityName = Convert.ToString(Dt.Rows[i]["CityName"]);
                        //objFaculty.CityName = Convert.ToString(Dt.Rows[i]["CityName"]);
                        //objFaculty.Pincode = Convert.ToInt32(Dt.Rows[i]["Pincode"]);
                        //objFaculty.FacultyContactNo = Convert.ToString(Dt.Rows[i]["FacultyContactNo"]);
                        //objFaculty.FacultyFaxNo = Convert.ToString(Dt.Rows[i]["FacultyFaxNo"]);
                        //objFaculty.FacultyEmail = Convert.ToString(Dt.Rows[i]["FacultyEmail"]);
                        //objFaculty.FacultyUrl = Convert.ToString(Dt.Rows[i]["FacultyUrl"]);
                        //objFaculty.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
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

        #region Programme Part Name List on the basis of Programme Instance Id
        [HttpPost]
        public HttpResponseMessage ProgrammePartGetByProgInstId(ProgrammeInstancePartTerm ObjProgInstPartTerm)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("MstProgrammePartGetByProgInstId", Con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@ProgrammeInstanceId", ObjProgInstPartTerm.ProgrammeInstanceId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<ProgrammeInstancePart> ObjLstProgInstPart = new List<ProgrammeInstancePart>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePart ObjProgInstPart = new ProgrammeInstancePart();
                        ObjProgInstPart.Id = Convert.ToInt32(dt.Rows[i]["ProgrammeInstancePartId"]);
                        ObjProgInstPart.FacultyId = Convert.ToInt32(dt.Rows[i]["FacultyId"]);
                        ObjProgInstPart.FacultyName = Convert.ToString(dt.Rows[i]["FacultyName"]);
                        ObjProgInstPart.ProgrammeId = Convert.ToInt32(dt.Rows[i]["ProgrammeId"]);
                        ObjProgInstPart.ProgrammeName = Convert.ToString(dt.Rows[i]["ProgrammeName"]);
                        ObjProgInstPart.ProgrammePartId = Convert.ToInt32(dt.Rows[i]["ProgrammePartId"]);
                        ObjProgInstPart.ProgrammeInstanceId = Convert.ToInt64(dt.Rows[i]["ProgrammeInstanceId"]);
                        // ObjProgInstPart.PartName = Convert.ToString(dt.Rows[i]["PartName"]);
                        ObjProgInstPart.PartShortName = Convert.ToString(dt.Rows[i]["PartShortName"]);
                        // ObjProgInstPart.PartName = Convert.ToString(dt.Rows[i]["PartName"]);
                        ObjLstProgInstPart.Add(ObjProgInstPart);

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

        #region Branch List On the basis of Programme Instance Id
        [HttpPost]
        public HttpResponseMessage MstProgrammeBranchListGetByProgInstanceId(ProgrammeInstancePartTerm ObjProg)
        {
            try
            {
                int ProgInstId = Convert.ToInt32(ObjProg.ProgrammeInstanceId);
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter cmdda = new SqlDataAdapter("MstSpecialisationGetbyProgInstId", Con);

                cmdda.SelectCommand.CommandType = CommandType.StoredProcedure;
                cmdda.SelectCommand.Parameters.AddWithValue("@ProgrammeInstanceId", ProgInstId);

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
                        ObjMstSpecialisation.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
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
                SqlDataAdapter da = new SqlDataAdapter("MstProgrammePartTermGetByProgrammeInstanceId", Con);
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
                        ObjProgPartTerm.ProgrammePartId = Convert.ToInt32(dt.Rows[i]["PartId"]);
                        ObjProgPartTerm.PartTermName = Convert.ToString(dt.Rows[i]["PartTermName"]);
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
                    return Return.returnHttp("404", "No Record Found", null);
                }

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region List of Groups per ProgrammeInstancePartTermId
        [HttpPost]
        public HttpResponseMessage IncProgramInstancePartTermGroupGet(IncProgInstPartTermGroup ObjProgInstPartTermGroup)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("ProgrammeInstancePartTermPaperGroupGetByPartTermId", Con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ObjProgInstPartTermGroup.ProgrammeInstancePartTermId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<IncProgInstPartTermGroup> ObjLstProgInstGroup = new List<IncProgInstPartTermGroup>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IncProgInstPartTermGroup ObjProgInstGroup = new IncProgInstPartTermGroup();
                        ObjProgInstGroup.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjProgInstGroup.InstancePartTermName = (Convert.ToString(dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["InstancePartTermName"]);
                        ObjProgInstGroup.GroupName = (Convert.ToString(dt.Rows[i]["GroupName"])).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["GroupName"]);
                        ObjProgInstGroup.ProgrammeInstancePartTermId = (Convert.ToString(dt.Rows[i]["ProgrammeInstancePartTermId"])).IsEmpty() ? 0 : Convert.ToInt64(dt.Rows[i]["ProgrammeInstancePartTermId"]);
                        ObjProgInstGroup.ContainsGroup = (Convert.ToString(dt.Rows[i]["ContainsSubgroup"])).IsEmpty() ? false : Convert.ToBoolean(dt.Rows[i]["ContainsSubgroup"]);
                        ObjProgInstGroup.IsSubGroup = (Convert.ToString(dt.Rows[i]["IsSubGroup"])).IsEmpty() ? false : Convert.ToBoolean(dt.Rows[i]["IsSubGroup"]);
                        ObjProgInstGroup.IsSubGroupSts = (Convert.ToString(dt.Rows[i]["IsSubGroupSts"])).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["IsSubGroupSts"]);
                        ObjProgInstGroup.MPTGroupId = (Convert.ToString(dt.Rows[i]["MstPartTermGroupId"])).IsEmpty() ? 0 : Convert.ToInt64(dt.Rows[i]["MstPartTermGroupId"]);
                        ObjProgInstGroup.MinCredits = (Convert.ToString(dt.Rows[i]["MinCredits"])).IsEmpty() ? 0 : Convert.ToDecimal(dt.Rows[i]["MinCredits"]);
                        ObjProgInstGroup.MaxCredits = Convert.ToString(dt.Rows[i]["MaxCredits"]).IsEmpty() ? 0 : Convert.ToDecimal(dt.Rows[i]["MaxCredits"]);
                        ObjProgInstGroup.MinPapers = Convert.ToString(dt.Rows[i]["MinPapers"]).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[i]["MinPapers"]);
                        ObjProgInstGroup.MaxPapers = Convert.ToString(dt.Rows[i]["MaxPapers"]).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[i]["MaxPapers"]);
                        ObjProgInstGroup.MinMarks = Convert.ToString(dt.Rows[i]["MinMarks"]).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[i]["MinMarks"]);
                        ObjProgInstGroup.MaxMarks = Convert.ToString(dt.Rows[i]["MaxMarks"]).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[i]["MaxMarks"]);
                        ObjProgInstGroup.SeparatePassingHead = (Convert.ToString(dt.Rows[i]["SeparatePassingHead"])).IsEmpty() ? false : Convert.ToBoolean(dt.Rows[i]["SeparatePassingHead"]);
                        ObjProgInstGroup.MinSubGroups = (Convert.ToString(dt.Rows[i]["MinSubgroup"])).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[i]["MinSubgroup"]);
                        ObjProgInstGroup.MaxSubGroups = (Convert.ToString(dt.Rows[i]["MaxSubgroup"])).IsEmpty() ? 0 : Convert.ToInt32(dt.Rows[i]["MaxSubgroup"]);
                        ObjProgInstGroup.HasSubGroupSts = (Convert.ToString(dt.Rows[i]["HasSubGroupStatus"])).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["HasSubGroupStatus"]);
                        ObjProgInstGroup.IsSeparatePassingHeadSts = (Convert.ToString(dt.Rows[i]["SeparatePassHeadStatus"])).IsEmpty() ? "" : Convert.ToString(dt.Rows[i]["SeparatePassHeadStatus"]);

                        ObjLstProgInstGroup.Add(ObjProgInstGroup);

                    }
                    return Return.returnHttp("200", ObjLstProgInstGroup, null);
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

        #region Insertion of Group Name to Programmer Part Term/Semester

        [HttpPost]
        public HttpResponseMessage IncProgInstPartTermGroupAdd(IncProgInstPartTermGroup ObjProgInstPartTermGroup)
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
                Int64 ProgrammeInstancePartTermId = Convert.ToInt64(ObjProgInstPartTermGroup.ProgrammeInstancePartTermId.ToString());
                Int64 MPTGroupId = Convert.ToInt64(ObjProgInstPartTermGroup.MPTGroupId);

                SqlCommand cmd = new SqlCommand("IncProgInstPartTermGroupAdd", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (Convert.ToBoolean(ObjProgInstPartTermGroup.ContainsGroup == true))
                {
                    ObjProgInstPartTermGroup.MaxPapers = 0;
                    ObjProgInstPartTermGroup.MinPapers = 0;
                    if (ObjProgInstPartTermGroup.MaxSubGroups <= 0 || ObjProgInstPartTermGroup.MinSubGroups <= 0)
                    {
                        return Return.returnHttp("202", "Minimum and maximum number of subgroups should be non-zero value. ", null);
                    }
                }
                if (Convert.ToBoolean(ObjProgInstPartTermGroup.ContainsGroup == false))
                {
                    ObjProgInstPartTermGroup.MaxSubGroups = 0;
                    ObjProgInstPartTermGroup.MinSubGroups = 0;
                    if (ObjProgInstPartTermGroup.MinPapers <= 0 || ObjProgInstPartTermGroup.MaxPapers <= 0)
                    {
                        return Return.returnHttp("202", "Minimum and maximum number of papers should be non-zero value. ", null);
                    }
                }
                if (String.IsNullOrEmpty(Convert.ToString(ProgrammeInstancePartTermId)))
                {
                    return Return.returnHttp("202", "Please select Programme Instance Part Term", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjProgInstPartTermGroup.GroupName)))
                {
                    return Return.returnHttp("202", "Please enter Group Name", null);
                }
                else if (validation.validateDigit(Convert.ToString(ObjProgInstPartTermGroup.MaxSubGroups)) == false)
                {
                    return Return.returnHttp("202", "Please enter Maximum Groups", null);
                }
                else if ((validation.validateDigit(Convert.ToString(ObjProgInstPartTermGroup.MinSubGroups)) == false) || (ObjProgInstPartTermGroup.MinSubGroups > ObjProgInstPartTermGroup.MaxSubGroups))
                {
                    return Return.returnHttp("202", "Please enter Minimum sub groups and respective of Maximum sub groups", null);
                }
                else if (validation.validateDigit(Convert.ToString(ObjProgInstPartTermGroup.MaxPapers)) == false)
                {
                    return Return.returnHttp("202", "Please enter Maximum Papers", null);
                }
                else if ((validation.validateDigit(Convert.ToString(ObjProgInstPartTermGroup.MinPapers)) == false) ||
                    ((ObjProgInstPartTermGroup.MinPapers > ObjProgInstPartTermGroup.MaxPapers) &&
                    (ObjProgInstPartTermGroup.MinPapers != 0 && ObjProgInstPartTermGroup.MaxPapers != 0)))
                {
                    return Return.returnHttp("202", "Minimum no. of papers should not be greater than Maximum no. of papers. ", null);
                }

                else if (validation.validateDigit(Convert.ToString(ObjProgInstPartTermGroup.MaxMarks)) == false)
                {
                    return Return.returnHttp("202", "Please enter Maximum marks", null);
                }
                else if ((validation.validateDigit(Convert.ToString(ObjProgInstPartTermGroup.MinMarks)) == false) ||
                    ((ObjProgInstPartTermGroup.MinMarks > ObjProgInstPartTermGroup.MaxMarks) &&
                    (ObjProgInstPartTermGroup.MinMarks < 0 || ObjProgInstPartTermGroup.MaxMarks < 0)))
                {
                    return Return.returnHttp("202", "Minimum marks should not be greater than Maximum marks.", null);
                }
                //else if (ObjProgInstPartTermGroup.MinMarks == 0 || ObjProgInstPartTermGroup.MaxMarks == 0)
                //{
                //    return Return.returnHttp("202", "Minimum and maximum marks should be non-zero value. ", null);
                //}
                else if (validation.validateDigit(Convert.ToString(ObjProgInstPartTermGroup.MaxCredits)) == false)
                {
                    return Return.returnHttp("202", "Please enter maximum credits", null);
                }
                else if ((validation.validateDigit(Convert.ToString(ObjProgInstPartTermGroup.MinCredits)) == false) ||
                    ((ObjProgInstPartTermGroup.MinCredits > ObjProgInstPartTermGroup.MaxCredits) &&
                    (ObjProgInstPartTermGroup.MinCredits < 0 || ObjProgInstPartTermGroup.MaxCredits < 0)))
                {
                    return Return.returnHttp("202", "Please enter Minimum credits and respective of Maximum credits", null);
                }
                //else if (ObjProgInstPartTermGroup.MinCredits == 0 || ObjProgInstPartTermGroup.MaxCredits == 0)
                //{
                //    return Return.returnHttp("202", "Minimum and maximum credits should be non-zero value. ", null);
                //}
                else
                {
                    cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                    cmd.Parameters.AddWithValue("@MPTGroupId", MPTGroupId);
                    cmd.Parameters.AddWithValue("@ContainsGroup", ObjProgInstPartTermGroup.ContainsGroup);
                    cmd.Parameters.AddWithValue("@IsSubGroup", ObjProgInstPartTermGroup.IsSubGroup);
                    cmd.Parameters.AddWithValue("@MinPapers", ObjProgInstPartTermGroup.MinPapers);
                    cmd.Parameters.AddWithValue("@MaxPapers", ObjProgInstPartTermGroup.MaxPapers);
                    cmd.Parameters.AddWithValue("@MinSubGroups", ObjProgInstPartTermGroup.MinSubGroups);
                    cmd.Parameters.AddWithValue("@MaxSubGroups", ObjProgInstPartTermGroup.MaxSubGroups);
                    cmd.Parameters.AddWithValue("@IsSepPassingHead", ObjProgInstPartTermGroup.SeparatePassingHead);
                    cmd.Parameters.AddWithValue("@MinMarks", ObjProgInstPartTermGroup.MinMarks);
                    cmd.Parameters.AddWithValue("@MaxMarks", ObjProgInstPartTermGroup.MaxMarks);
                    cmd.Parameters.AddWithValue("@MinCredits", ObjProgInstPartTermGroup.MinCredits);
                    cmd.Parameters.AddWithValue("@MaxCredits", ObjProgInstPartTermGroup.MaxCredits);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters.Add("@GroupId", SqlDbType.BigInt);
                    cmd.Parameters.Add("@GroupName", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                    cmd.Parameters["@GroupId"].Direction = ParameterDirection.Output;
                    cmd.Parameters["@GroupName"].Direction = ParameterDirection.Output;
                    Con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    Int64 GroupIdLocal;
                    if ((cmd.Parameters["@GroupId"].Value) != null)
                    {
                        GroupIdLocal = Convert.ToInt64(cmd.Parameters["@GroupId"].Value);
                    }
                    else
                    {
                        GroupIdLocal = -1;
                    }
                    String GroupNameLocal = (Convert.ToString(cmd.Parameters["@GroupName"].Value)).IsEmpty() ? "Not Defined" : Convert.ToString(cmd.Parameters["@GroupName"].Value); ;

                    Con.Close();
                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Your data has been saved successfully.";
                    }
                    return Return.returnHttp("200", (strMessage.ToString(), GroupIdLocal, GroupNameLocal), null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Updation of Group Name to Programmer Part Term/Semester

        [HttpPost]
        public HttpResponseMessage IncProgInstPartTermGroupUpdate(IncProgInstPartTermGroup ObjProgInstPartTermGroup)
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
                Int64 ProgrammeInstancePartTermId = Convert.ToInt64(ObjProgInstPartTermGroup.ProgrammeInstancePartTermId.ToString());
                //string GroupName = Convert.ToString(ObjProgInstPartTermGroup.GroupName);
                Int64 MPTGroupId = Convert.ToInt64(ObjProgInstPartTermGroup.MPTGroupId);

                SqlCommand cmd = new SqlCommand("IncProgInstPartTermGroupEdit", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (Convert.ToBoolean(ObjProgInstPartTermGroup.ContainsGroup == true))
                {
                    ObjProgInstPartTermGroup.MaxPapers = 0;
                    ObjProgInstPartTermGroup.MinPapers = 0;
                    if (ObjProgInstPartTermGroup.MaxSubGroups <= 0 || ObjProgInstPartTermGroup.MinSubGroups <= 0)
                    {
                        return Return.returnHttp("202", "Minimum and maximum number of subgroups should be non-zero value. ", null);
                    }
                }
                if (Convert.ToBoolean(ObjProgInstPartTermGroup.ContainsGroup == false))
                {
                    ObjProgInstPartTermGroup.MaxSubGroups = 0;
                    ObjProgInstPartTermGroup.MinSubGroups = 0;
                    if (ObjProgInstPartTermGroup.MinPapers <= 0 || ObjProgInstPartTermGroup.MaxPapers <= 0)
                    {
                        return Return.returnHttp("202", "Minimum and maximum number of papers should be non-zero value. ", null);
                    }
                }
                if (String.IsNullOrEmpty(Convert.ToString(ProgrammeInstancePartTermId)))
                {
                    return Return.returnHttp("202", "Please select Programme Instance Part Term", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ObjProgInstPartTermGroup.GroupName)))
                {
                    return Return.returnHttp("202", "Please enter Group Name", null);
                }
                else if (validation.validateDigit(Convert.ToString(ObjProgInstPartTermGroup.MaxSubGroups)) == false)
                {
                    return Return.returnHttp("202", "Please enter Maximum Groups", null);
                }
                else if ((validation.validateDigit(Convert.ToString(ObjProgInstPartTermGroup.MinSubGroups)) == false) || (ObjProgInstPartTermGroup.MinSubGroups > ObjProgInstPartTermGroup.MaxSubGroups))
                {
                    return Return.returnHttp("202", "Please enter Minimum sub groups and respective of Maximum sub groups", null);
                }
                else if (validation.validateDigit(Convert.ToString(ObjProgInstPartTermGroup.MaxPapers)) == false)
                {
                    return Return.returnHttp("202", "Please enter Maximum Papers", null);
                }
                else if ((validation.validateDigit(Convert.ToString(ObjProgInstPartTermGroup.MinPapers)) == false) ||
                    ((ObjProgInstPartTermGroup.MinPapers > ObjProgInstPartTermGroup.MaxPapers) &&
                    (ObjProgInstPartTermGroup.MinPapers != 0 && ObjProgInstPartTermGroup.MaxPapers != 0)))
                {
                    return Return.returnHttp("202", "Minimum no. of papers should not be greater than Maximum no. of papers. ", null);
                }
                
                else if (validation.validateDigit(Convert.ToString(ObjProgInstPartTermGroup.MaxMarks)) == false)
                {
                    return Return.returnHttp("202", "Please enter Maximum marks", null);
                }
                else if ((validation.validateDigit(Convert.ToString(ObjProgInstPartTermGroup.MinMarks)) == false) ||
                    ((ObjProgInstPartTermGroup.MinMarks > ObjProgInstPartTermGroup.MaxMarks) &&
                    (ObjProgInstPartTermGroup.MinMarks < 0 || ObjProgInstPartTermGroup.MaxMarks < 0)))
                {
                    return Return.returnHttp("202", "Minimum marks should not be greater than Maximum marks.", null);
                }
                //else if (ObjProgInstPartTermGroup.MinMarks == 0 || ObjProgInstPartTermGroup.MaxMarks == 0)
                //{
                //    return Return.returnHttp("202", "Minimum and maximum marks should be non-zero value. ", null);
                //}
                else if (validation.validateDigit(Convert.ToString(ObjProgInstPartTermGroup.MaxCredits)) == false)
                {
                    return Return.returnHttp("202", "Please enter maximum credits", null);
                }
                else if ((validation.validateDigit(Convert.ToString(ObjProgInstPartTermGroup.MinCredits)) == false) ||
                    ((ObjProgInstPartTermGroup.MinCredits > ObjProgInstPartTermGroup.MaxCredits) && 
                    (ObjProgInstPartTermGroup.MinCredits < 0 || ObjProgInstPartTermGroup.MaxCredits < 0)))
                {
                    return Return.returnHttp("202", "Please enter Minimum credits and respective of Maximum credits", null);
                }
                //else if (ObjProgInstPartTermGroup.MinCredits == 0 || ObjProgInstPartTermGroup.MaxCredits == 0)
                //{
                //    return Return.returnHttp("202", "Minimum and maximum credits should be non-zero value. ", null);
                //}
                else
                {
                    cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                    cmd.Parameters.AddWithValue("@MPTGroupId", MPTGroupId);
                    cmd.Parameters.AddWithValue("@ContainsGroup", ObjProgInstPartTermGroup.ContainsGroup);
                    cmd.Parameters.AddWithValue("@IsSubGroup", ObjProgInstPartTermGroup.IsSubGroup);
                    cmd.Parameters.AddWithValue("@MinPapers", ObjProgInstPartTermGroup.MinPapers);
                    cmd.Parameters.AddWithValue("@MaxPapers", ObjProgInstPartTermGroup.MaxPapers);
                    cmd.Parameters.AddWithValue("@MinSubGroups", ObjProgInstPartTermGroup.MinSubGroups);
                    cmd.Parameters.AddWithValue("@MaxSubGroups", ObjProgInstPartTermGroup.MaxSubGroups);
                    cmd.Parameters.AddWithValue("@IsSepPassingHead", ObjProgInstPartTermGroup.SeparatePassingHead);
                    cmd.Parameters.AddWithValue("@MinMarks", ObjProgInstPartTermGroup.MinMarks);
                    cmd.Parameters.AddWithValue("@MaxMarks", ObjProgInstPartTermGroup.MaxMarks);
                    cmd.Parameters.AddWithValue("@MinCredits", ObjProgInstPartTermGroup.MinCredits);
                    cmd.Parameters.AddWithValue("@MaxCredits", ObjProgInstPartTermGroup.MaxCredits);
                    cmd.Parameters.AddWithValue("@Id", ObjProgInstPartTermGroup.Id);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                    //cmd.Parameters["@GroupId"].Direction = ParameterDirection.Output;
                    Con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);

                    Con.Close();
                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Your data has been modified successfully.";
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

        #region Deletion of Record from IncProgInstPartTermPaperGroupMap table
        [HttpPost]
        public HttpResponseMessage IncProgInstPartTermGroupDelete(IncProgInstPartTermGroup ObjProgInstPartTermGroup)
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
                Int64 GroupId = Convert.ToInt32(ObjProgInstPartTermGroup.Id.ToString());


                SqlCommand cmd = new SqlCommand("IncProgInstPartTermGroupDelete", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (String.IsNullOrEmpty(Convert.ToString(GroupId)))
                {
                    return Return.returnHttp("201", "Please select Programme Instance Part Term", null);
                }

                else
                {
                    cmd.Parameters.AddWithValue("@Id", GroupId);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    Con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    Con.Close();
                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Group has been deleted successfully.";
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

        #region Paperlist by GroupId 
        public HttpResponseMessage PaperListGetByGroupId(IncProgInstPartTermGroup ObjProgInstPartTermGroup)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("GetPaperNamebyGroupId", Con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ObjProgInstPartTermGroup.ProgrammeInstancePartTermId);
                da.SelectCommand.Parameters.AddWithValue("@GroupId", ObjProgInstPartTermGroup.Id);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<IncProgInstPartTermGroup> ObjLstProgInstGroup = new List<IncProgInstPartTermGroup>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IncProgInstPartTermGroup ObjProgInstGroup = new IncProgInstPartTermGroup();
                        ObjProgInstGroup.PaperId = Convert.ToInt64(dt.Rows[i]["PaperId"]);
                        ObjProgInstGroup.PaperName = Convert.ToString(dt.Rows[i]["PaperName"]);
                        ObjProgInstGroup.Id = (Convert.ToString(dt.Rows[i]["Id"])).IsEmpty() ? 0 : (Convert.ToInt64(dt.Rows[i]["Id"]));
                        ObjProgInstGroup.PaperCheckedSts = Convert.ToBoolean(dt.Rows[i]["PaperCheckedSts"]);

                        ObjLstProgInstGroup.Add(ObjProgInstGroup);

                    }
                    return Return.returnHttp("200", ObjLstProgInstGroup, null);
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

        #region GroupList by GroupID
        public HttpResponseMessage GroupListGetByGroupId(IncProgInstPartTermGroup ObjProgInstPartTermGroup)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("GetGroupNamebyParentGroupId", Con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ObjProgInstPartTermGroup.ProgrammeInstancePartTermId);
                da.SelectCommand.Parameters.AddWithValue("@ParentGroupId", ObjProgInstPartTermGroup.ParentGroupId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<IncProgInstPartTermGroup> ObjLstProgInstGroup = new List<IncProgInstPartTermGroup>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IncProgInstPartTermGroup ObjProgInstGroup = new IncProgInstPartTermGroup();
                        ObjProgInstGroup.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjProgInstGroup.MPTGroupId = Convert.ToInt64(dt.Rows[i]["MstPartTermGroupId"]);
                        ObjProgInstGroup.GroupName = Convert.ToString(dt.Rows[i]["GroupName"]);
                        ObjProgInstGroup.ParentGroupId = (Convert.ToString(dt.Rows[i]["ParentGroupId"])).IsEmpty() ? 0 : (Convert.ToInt64(dt.Rows[i]["ParentGroupId"]));
                        ObjProgInstGroup.GroupCheckedSts = Convert.ToBoolean(dt.Rows[i]["GroupCheckedSts"]);

                        ObjLstProgInstGroup.Add(ObjProgInstGroup);

                    }
                    return Return.returnHttp("200", ObjLstProgInstGroup, null);
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

        #region Programme Part term Parent Group detail update
        public HttpResponseMessage ProgInstPartTermParentGroupUpdate(IncProgInstPartTermGroup ObjProgInstPartTermGroup)
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
                Int64 ProgrammeInstancePartTermId = Convert.ToInt64(ObjProgInstPartTermGroup.ProgrammeInstancePartTermId.ToString());
                string GroupName = Convert.ToString(ObjProgInstPartTermGroup.GroupName);

                DataTable DtTypeGroupTbl = new DataTable();
                DtTypeGroupTbl.Columns.Add("Id", typeof(Int64));
                DtTypeGroupTbl.Columns.Add("ProgrammeInstancePartTermId", typeof(Int64));
                DtTypeGroupTbl.Columns.Add("ParentGroupId", typeof(Int64));
                DtTypeGroupTbl.Columns.Add("ModifiedBy", typeof(Int64));
                DtTypeGroupTbl.Columns.Add("ModifiedOn", typeof(DateTime));

                // Add ‘Student’ data as Rows to the Data Table

                for (int i = 0; i < ObjProgInstPartTermGroup.GroupList.Count; i++)
                {
                    DataRow rowStudent = DtTypeGroupTbl.NewRow();
                    rowStudent["Id"] = ObjProgInstPartTermGroup.GroupList[i].Id;
                    rowStudent["ProgrammeInstancePartTermId"] = ProgrammeInstancePartTermId;
                    if (ObjProgInstPartTermGroup.GroupList[i].GroupCheckedSts == false)
                    {
                        rowStudent["ParentGroupId"] = DBNull.Value;
                    }
                    if (ObjProgInstPartTermGroup.GroupList[i].GroupCheckedSts == true)
                    {
                        rowStudent["ParentGroupId"] = ObjProgInstPartTermGroup.ParentGroupId;
                    }
                    rowStudent["ModifiedBy"] = UserId;
                    rowStudent["ModifiedOn"] = datetime;

                    //Add the Data Row to Table

                    DtTypeGroupTbl.Rows.Add(rowStudent);
                }
                SqlCommand cmd = new SqlCommand("ProgInstPartTermParentGroupEdit", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@tbltypePartTermPaperGroup", DtTypeGroupTbl);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                SqlTransaction SqlTrns;

                Con.Open();
                SqlTrns = Con.BeginTransaction();
                cmd.Transaction = SqlTrns;
                string strMessage = null;
                string Res_code = null;
                try
                {
                    cmd.ExecuteNonQuery();

                    SqlTrns.Commit();
                    Con.Close();
                    Res_code = "200";
                    strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                }
                catch (SqlException e)
                {
                    SqlTrns.Rollback();
                    Con.Close();
                    Res_code = "201";
                    strMessage = e.Message;
                }
                return Return.returnHttp(Res_code, strMessage.ToString(), null);
            }

            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Programme Part Term Paper - Group Update
        public HttpResponseMessage ProgInstPartTermPaperGroupDetailUpdate(IncProgInstPartTermGroup ObjProgInstPartTermGroup)
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
                Int64 ProgrammeInstancePartTermId = Convert.ToInt64(ObjProgInstPartTermGroup.ProgrammeInstancePartTermId.ToString());
                Int64 GroupId = Convert.ToInt64(ObjProgInstPartTermGroup.Id.ToString());

                DataTable DtTypePaperTbl = new DataTable();
                // DtTypePaperTbl.Columns.Add("Id", typeof(Int64));
                DtTypePaperTbl.Columns.Add("PaperId", typeof(Int64));
                DtTypePaperTbl.Columns.Add("ProgrammeInstancePartTermId", typeof(Int64));
                DtTypePaperTbl.Columns.Add("GroupId", typeof(Int64));
                DtTypePaperTbl.Columns.Add("ModifiedBy", typeof(Int64));
                DtTypePaperTbl.Columns.Add("ModifiedOn", typeof(DateTime));

                // Add ‘Student’ data as Rows to the Data Table

                for (int i = 0; i < ObjProgInstPartTermGroup.PaperList.Count; i++)
                {

                    DataRow rowStudent = DtTypePaperTbl.NewRow();
                    rowStudent["PaperId"] = ObjProgInstPartTermGroup.PaperList[i].PaperId;
                    rowStudent["ProgrammeInstancePartTermId"] = ProgrammeInstancePartTermId;
                    if (ObjProgInstPartTermGroup.PaperList[i].PaperCheckedSts == false)
                    {
                        rowStudent["GroupId"] = DBNull.Value;
                    }
                    if (ObjProgInstPartTermGroup.PaperList[i].PaperCheckedSts == true)
                    {
                        rowStudent["GroupId"] = ObjProgInstPartTermGroup.Id;
                    }
                    rowStudent["ModifiedBy"] = UserId;
                    rowStudent["ModifiedOn"] = datetime;

                    //Add the Data Row to Table

                    DtTypePaperTbl.Rows.Add(rowStudent);
                }
                SqlCommand cmd = new SqlCommand("ProgInstPartTermPaperGroupEdit", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@tbltypePartTermPaperGroup", DtTypePaperTbl);
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                cmd.Parameters.AddWithValue("@GroupId", GroupId);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                string strMessage = null;
                string Res_code = null;
                SqlTransaction SqlTrns;

                Con.Open();
                SqlTrns = Con.BeginTransaction();
                cmd.Transaction = SqlTrns;
                try
                {
                    cmd.ExecuteNonQuery();

                    SqlTrns.Commit();
                    Con.Close();
                    Res_code = "200";
                    strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                }
                catch (SqlException ex)
                {
                    SqlTrns.Rollback();
                    Con.Close();
                    Res_code = "201";
                    strMessage = ex.Message;
                }
                return Return.returnHttp(Res_code, strMessage.ToString(), null);
            }

            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion



        #region GroupList By MstProgrammePartTermId
        public HttpResponseMessage GroupListGetByMstProgrammePartTermId(IncProgInstPartTermGroup ObjProgInstPartTermGroup)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("MstPartTermGroupMapGetByPTId", Con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ObjProgInstPartTermGroup.ProgrammeInstancePartTermId);

                DataTable dt = new DataTable();
                da.Fill(dt);

                List<MstProgrammePartTermGroupMap> ObjLstProgPTGroup = new List<MstProgrammePartTermGroupMap>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        MstProgrammePartTermGroupMap ObjProgPTGroup = new MstProgrammePartTermGroupMap();
                        ObjProgPTGroup.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjProgPTGroup.GroupName = Convert.ToString(dt.Rows[i]["GroupName"]);
                        ObjProgPTGroup.ProgrammePartTermId = Convert.ToInt64(dt.Rows[i]["ProgrammePartTermId"]);
                        ObjProgPTGroup.IsActive = Convert.ToBoolean(dt.Rows[i]["IsActive"]);
                        ObjProgPTGroup.PartTermName = Convert.ToString(dt.Rows[i]["PartTermName"]);
                        ObjProgPTGroup.PartTermShortName = Convert.ToString(dt.Rows[i]["PartTermShortName"]);

                        ObjLstProgPTGroup.Add(ObjProgPTGroup);

                    }
                    return Return.returnHttp("200", ObjLstProgPTGroup, null);
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
    }
}
