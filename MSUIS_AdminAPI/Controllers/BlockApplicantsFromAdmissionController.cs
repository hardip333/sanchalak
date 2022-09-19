using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;
using Newtonsoft.Json.Linq;

namespace MSUISApi.Controllers
{
    public class BlockApplicantsFromAdmissionController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region GetApplicantListforBlockAdmission
        [HttpPost]
        public HttpResponseMessage GetApplicantListforBlockAdmission(BlockApplicantsFromAdmission BAFA)
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

                Int32 ProgrammeInstancePartTermId = Convert.ToInt32(BAFA.ProgrammeInstancePartTermId);

                SqlCommand Cmd = new SqlCommand("GetApplicantListforBlockAdmission", Con);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@AcademicYearId", BAFA.AcademicYearId);
                Cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", BAFA.ProgrammeInstancePartTermId);
                Cmd.Parameters.AddWithValue("@InstituteId", BAFA.AdmittedInstituteId);

                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                Int32 count = 0;
                List<BlockApplicantsFromAdmission> ObjLstBAFA = new List<BlockApplicantsFromAdmission>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        BlockApplicantsFromAdmission objBAFA = new BlockApplicantsFromAdmission();


                        objBAFA.Id = (((Dt.Rows[i]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["Id"]);

                        objBAFA.FullName = (Convert.ToString(Dt.Rows[i]["FullName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FullName"]);

                        objBAFA.EmailId = (Convert.ToString(Dt.Rows[i]["EmailId"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["EmailId"]);

                        objBAFA.MobileNo = (Convert.ToString(Dt.Rows[i]["MobileNo"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["MobileNo"]);

                        objBAFA.ApplicantUserName = (((Dt.Rows[i]["ApplicantUserName"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["ApplicantUserName"]);
                        objBAFA.ProgrammeInstancePartTermId = (((Dt.Rows[i]["ProgrammeInstancePartTermId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["ProgrammeInstancePartTermId"]);
                        objBAFA.FeeCategoryName = (Convert.ToString(Dt.Rows[i]["FeeCategoryName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FeeCategoryName"]);

                        var FeeCategoryName = Dt.Rows[i]["FeeCategoryName"];
                        if (FeeCategoryName is DBNull)
                        {
                            FeeCategoryName = "NULL";
                        }
                        else
                        {
                            FeeCategoryName = Dt.Rows[i]["FeeCategoryName"];
                        }


                        objBAFA.IsBlocked = (Convert.ToString(Dt.Rows[i]["IsBlocked"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsBlocked"]);

                        var IsBlocked = Dt.Rows[i]["IsBlocked"];
                        if (IsBlocked is DBNull)
                        {
                            IsBlocked = "NULL";
                        }
                        else
                        {
                            IsBlocked = Dt.Rows[i]["IsBlocked"];
                        }

                        objBAFA.AdmissionBlocked = (Convert.ToString(Dt.Rows[i]["AdmissionBlocked"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["AdmissionBlocked"]);

                        var AdmissionBlocked = Dt.Rows[i]["AdmissionBlocked"];
                        if (AdmissionBlocked is DBNull)
                        {
                            AdmissionBlocked = "NULL";
                        }
                        else
                        {
                            AdmissionBlocked = Dt.Rows[i]["AdmissionBlocked"];
                        }
                        objBAFA.BlockedRemark = (Convert.ToString(Dt.Rows[i]["BlockedRemark"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["BlockedRemark"]);

                        var BlockedRemark = Dt.Rows[i]["BlockedRemark"];
                        if (BlockedRemark is DBNull)
                        {
                            BlockedRemark = "NULL";
                        }
                        else
                        {
                            BlockedRemark = Dt.Rows[i]["BlockedRemark"];
                        }
                        objBAFA.AdmittedInstituteId = (((Dt.Rows[i]["AdmittedInstituteId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["AdmittedInstituteId"]);
                        objBAFA.AcademicYearId = (((Dt.Rows[i]["AcademicYearId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["AcademicYearId"]);
                        objBAFA.InstituteId = (((Dt.Rows[i]["InstituteId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["InstituteId"]);
                        objBAFA.IsVerificationSmsOns = (Convert.ToString(Dt.Rows[i]["IsVerificationSmsOn"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["IsVerificationSmsOn"]);

                        var IsVerificationSmsOn = Dt.Rows[i]["IsVerificationSmsOn"];
                        if (IsVerificationSmsOn is DBNull)
                        {
                            IsVerificationSmsOn = "NULL";
                        }
                        else
                        {
                            IsVerificationSmsOn = Dt.Rows[i]["IsVerificationSmsOn"];
                        }
                        objBAFA.IsVerificationEmailOns = (Convert.ToString(Dt.Rows[i]["IsVerificationEmailOn"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["IsVerificationEmailOn"]);
                        var IsVerificationEmailOn = Dt.Rows[i]["IsVerificationEmailOn"];
                        if (IsVerificationEmailOn is DBNull)
                        {
                            IsVerificationEmailOn = "NULL";
                        }
                        else
                        {
                            IsVerificationEmailOn = Dt.Rows[i]["IsVerificationEmailOn"];
                        }



                        count = count + 1;
                        objBAFA.IndexId = count;

                        ObjLstBAFA.Add(objBAFA);



                    }
                    return Return.returnHttp("200", ObjLstBAFA, null);
                    if (ObjLstBAFA[0].IsVerificationSmsOn != null && ObjLstBAFA[0].IsVerificationEmailOn != null)
                    {
                        return Return.returnHttp("200", ObjLstBAFA, null);
                    }
                    else
                    {
                        return Return.returnHttp("201", "", null);
                    }
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


        #region PostGetAdmFeeEndDateforVerificationSMSEmail

        [HttpPost]
        public HttpResponseMessage PostGetAdmFeeEndDate(ConfigureDates ObjPostConfiguration)
        {
            ConfigureDates modelobj = new ConfigureDates();

            try
            {
                modelobj.ProgrammeInstancePartTermId = ObjPostConfiguration.ProgrammeInstancePartTermId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("PostGetAdmFeeEndDateforSMSEmail", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", modelobj.ProgrammeInstancePartTermId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<ConfigureDates> ObjLstPC = new List<ConfigureDates>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        ConfigureDates ObjPC = new ConfigureDates();
                        ObjPC.AdmissionFeesStopDate = string.Format("{0: dd-MM-yyyy}", dr["AdmissionFeesStopDate"]);

                        ObjLstPC.Add(ObjPC);
                    }
                    return Return.returnHttp("200", ObjLstPC, null);
                }
                else
                {
                    return Return.returnHttp("201", "No Record Found", null);
                }



            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion


        #region MstInstituteGetByProgInstPartTermId
        [HttpPost]
        public HttpResponseMessage MstInstituteGetByProgInstPartTermId(MstInstitute ObjInst)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("MstInstituteGetByProgInstPartTermId", Con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@AcademicYearId", ObjInst.AcademicYearId);
                da.SelectCommand.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ObjInst.ProgrammeInstancePartTermId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<MstInstitute> ObjLstMstInst = new List<MstInstitute>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        MstInstitute ObjMstInstitute = new MstInstitute();
                        ObjMstInstitute.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjMstInstitute.InstituteName = Convert.ToString(dt.Rows[i]["InstituteName"]);
                        ObjMstInstitute.ProgrammeInstancePartTermId = Convert.ToInt32(dt.Rows[i]["ProgrammeInstancePartTermId"]);

                        ObjLstMstInst.Add(ObjMstInstitute);

                    }
                    return Return.returnHttp("200", ObjLstMstInst, null);
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



        #region Faculty Get By Id
        [HttpPost]
        public HttpResponseMessage MstFacultyGetbyId(MstFaculty Faculty)
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

            try
            {
                //dynamic Jsondata = Faculty;

                // Int32 Id = Convert.ToInt32(Faculty.Id);


                SqlCommand cmd = new SqlCommand("MstInstituteGetTest", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", facultyDepartIntituteId);
                List<MstFaculty> ObjLstFaculty = new List<MstFaculty>();
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstFaculty objFaculty = new MstFaculty();

                        objFaculty.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objFaculty.FacultyName = Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objFaculty.FacultyCode = Convert.ToString(Dt.Rows[i]["FacultyCode"]);
                        objFaculty.InstituteId = Convert.ToInt32(Dt.Rows[i]["InstituteId"]);
                        objFaculty.InstituteName = Convert.ToString(Dt.Rows[i]["InstituteName"]);
                        objFaculty.FacultyCode = Convert.ToString(Dt.Rows[i]["InstituteCode"]);
                        //objFaculty.FacultyAddress = Convert.ToString(Dt.Rows[i]["FacultyAddress"]);
                        //objFaculty.CityName = Convert.ToString(Dt.Rows[i]["CityName"]);
                        //objFaculty.Pincode = Convert.ToInt32(Dt.Rows[i]["Pincode"]);
                        //objFaculty.FacultyContactNo = Convert.ToString(Dt.Rows[i]["FacultyContactNo"]);
                        //objFaculty.FacultyFaxNo = Convert.ToString(Dt.Rows[i]["FacultyFaxNo"]);
                        //objFaculty.FacultyEmail = Convert.ToString(Dt.Rows[i]["FacultyEmail"]);
                        //objFaculty.FacultyUrl = Convert.ToString(Dt.Rows[i]["FacultyUrl"]);

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


        #region BlockApplicantsFromAdmissionIsSuspended
        [HttpPost]
        public HttpResponseMessage BlockApplicantsFromAdmissionIsSuspended(BlockAdmission BA)
        {
            try
            {



                SqlTransaction ST;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String typePrefix = Request.Headers.GetValues("typePrefix").FirstOrDefault();
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
                if (typePrefix == "INS")
                {


                    if (res == "0")
                    {
                        return Return.returnHttp("0", null, null);
                    }
                    if (String.IsNullOrWhiteSpace(Convert.ToString(BA.BlockedRemark)))
                    {
                        return Return.returnHttp("201", "Please Enter Blocked Remark", null);
                    }
                    else
                    {


                        //Int64 ProgrammeInstancePartTermId = Convert.ToInt64(BA.ProgrammeInstancePartTermId);
                        Int64 Id = Convert.ToInt64(BA.Id);
                        string BlockedRemark = Convert.ToString(BA.BlockedRemark);
                        //Boolean IsBlocked = Convert.ToBoolean(BA.IsBlocked);
                        //Int64 BlockedBy = Convert.ToInt64(res.ToString());
                        Int64 UserId = Convert.ToInt64(res.ToString());


                        SqlCommand Cmd = new SqlCommand("ApplicantListforBlockAdmissionUnblock", Con);
                        Cmd.CommandType = CommandType.StoredProcedure;
                        //Cmd.Parameters.AddWithValue("@Flag", "ApplicantListforBlockAdmissionIsActiveDisable");

                        //Cmd.Parameters.AddWithValue("@InstituteId", facultyDepartIntituteId);

                        //Cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                        Cmd.Parameters.AddWithValue("@Id", Id);
                        Cmd.Parameters.AddWithValue("@BlockedRemark", BlockedRemark);
                        Cmd.Parameters.AddWithValue("@BlockedBy", UserId);
                        Cmd.Parameters.AddWithValue("@BlockedOn", datetime);
                        Cmd.Parameters.AddWithValue("@UserId", UserId);
                        Cmd.Parameters.AddWithValue("@UserTime", datetime);
                        Cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                        Cmd.Parameters["@Message"].Direction = ParameterDirection.Output;


                        Con.Open();
                        ST = Con.BeginTransaction();

                        try
                        {
                            Cmd.Transaction = ST;
                            Cmd.ExecuteNonQuery();
                            string strMessage = Convert.ToString(Cmd.Parameters["@Message"].Value);
                            ST.Commit();
                            if (string.Equals(strMessage, "TRUE"))
                            {
                                strMessage = "Your data has been Block.";
                            }
                            return Return.returnHttp("200", strMessage.ToString(), null);
                        }
                        catch (SqlException sqlError)
                        {
                            ST.Rollback();
                            String strMessageErr = Convert.ToString(sqlError);
                            Con.Close();
                            return Return.returnHttp("201", strMessageErr, null);

                        }
                        finally
                        {

                            Con.Close();


                        }
                    }
                }

                else
                {
                    return Return.returnHttp("200", "All Applicant are Blocked.", null);
                }


            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion


        #region BlockApplicantsFromAdmissionIsActive
        [HttpPost]
        public HttpResponseMessage BlockApplicantsFromAdmissionIsActive(BlockAdmission BlockAFA)
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
                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                if (String.IsNullOrWhiteSpace(Convert.ToString(BlockAFA.BlockedRemark)))
                {
                    return Return.returnHttp("201", "Please Enter Blocked Remark", null);
                }
                else
                {

                    Int64 Id = Convert.ToInt64(BlockAFA.Id);
                    string BlockedRemark = Convert.ToString(BlockAFA.BlockedRemark);
                    Int64 UserId = Convert.ToInt64(res.ToString());
                    Int64 BlockedBy = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("ApplicantListforBlockAdmissionBlock", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@Flag", "ApplicantListforBlockAdmissionIsActiveEnable");

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@BlockedRemark", BlockedRemark);
                    cmd.Parameters.AddWithValue("@BlockedBy", BlockedBy);
                    cmd.Parameters.AddWithValue("@BlockedOn", datetime);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);

                    cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                    Con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@MESSAGE"].Value);
                    Con.Close();

                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Your data has been Blocked.";
                    }
                    return Return.returnHttp("200", strMessage.ToString(), null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion


        #region BlockApplicantsFromAdmissionEdit
        [HttpPost]
        public HttpResponseMessage BlockApplicantsFromAdmissionEdit(BlockAdmission BA)
        {
            try
            {



                SqlTransaction ST;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String typePrefix = Request.Headers.GetValues("typePrefix").FirstOrDefault();
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
                if (typePrefix == "INS")
                {


                    if (res == "0")
                    {
                        return Return.returnHttp("0", null, null);
                    }
                    if (String.IsNullOrWhiteSpace(Convert.ToString(BA.BlockedRemark1)))
                    {
                        return Return.returnHttp("201", "Please Enter Blocked Remark", null);
                    }
                    else
                    {


                        Int64 ProgrammeInstancePartTermId = Convert.ToInt64(BA.ProgrammeInstancePartTermId);
                        //Int64 ApplicationId = Convert.ToInt64(BA.ApplicationId);
                        string BlockedRemark1 = Convert.ToString(BA.BlockedRemark1);
                        Boolean IsBlocked = Convert.ToBoolean(BA.IsBlocked);
                        //Int64 BlockedBy = Convert.ToInt64(res.ToString());
                        Int64 UserId = Convert.ToInt64(res.ToString());


                        SqlCommand Cmd = new SqlCommand("BlockApplicantsFromAdmissionEdit", Con);
                        Cmd.CommandType = CommandType.StoredProcedure;

                        //Cmd.Parameters.AddWithValue("@InstituteId", facultyDepartIntituteId);

                        Cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                        //Cmd.Parameters.AddWithValue("@ApplicationId", ApplicationId);
                        Cmd.Parameters.AddWithValue("@BlockedRemark", BlockedRemark1);
                        Cmd.Parameters.AddWithValue("@UserId", UserId);
                        Cmd.Parameters.AddWithValue("@UserTime", datetime);
                        Cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                        Cmd.Parameters["@Message"].Direction = ParameterDirection.Output;


                        Con.Open();
                        ST = Con.BeginTransaction();

                        try
                        {
                            Cmd.Transaction = ST;
                            Cmd.ExecuteNonQuery();
                            string strMessage = Convert.ToString(Cmd.Parameters["@Message"].Value);
                            ST.Commit();
                            if (string.Equals(strMessage, "TRUE"))
                            {
                                strMessage = "Your data has been Block.";
                            }
                            return Return.returnHttp("200", strMessage.ToString(), null);
                        }
                        catch (SqlException sqlError)
                        {
                            ST.Rollback();
                            String strMessageErr = Convert.ToString(sqlError);
                            Con.Close();
                            return Return.returnHttp("201", strMessageErr, null);

                        }
                        finally
                        {

                            Con.Close();


                        }
                    }
                }

                else
                {
                    return Return.returnHttp("200", "All Applicant are Blocked.", null);
                }


            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion


        #region PostProgPartTermGetByFacIdandYearId
        [HttpPost]
        public HttpResponseMessage PostProgPartTermGetByFacIdandYearId(ProgrammeInstancePartTerm ObjProgInstPartTerm)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("PostProgPartTermGetByFacIdandYearId", Con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                //da.SelectCommand.Parameters.AddWithValue("@FacultyId", ObjProgInstPartTerm.FacultyId);
                da.SelectCommand.Parameters.AddWithValue("@InstituteId", ObjProgInstPartTerm.InstituteId);
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

                        ObjLstProgInstPartTerm.Add(ObjProgPartTerm);

                    }
                    return Return.returnHttp("200", ObjLstProgInstPartTerm, null);
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

        #region AcademicYearGet
        [HttpPost]
        public HttpResponseMessage AcademicYearGet()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("IncAcademicYearGet", Con);

                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<AcademicYear> ObjAYList = new List<AcademicYear>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AcademicYear ObjAY = new AcademicYear();
                        ObjAY.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjAY.AcademicYearCode = Convert.ToString(dt.Rows[i]["AcademicYearCode"]);
                        ObjAYList.Add(ObjAY);
                    }
                    return Return.returnHttp("200", ObjAYList, null);
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