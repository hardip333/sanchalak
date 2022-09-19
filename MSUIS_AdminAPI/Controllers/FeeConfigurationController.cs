using MSUIS_TokenManager.App_Start;
using MSUISApi.BAL;
using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class FeeConfigurationController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region FeeConfigurationGet
        [HttpPost]
        public HttpResponseMessage FeeConfigurationGet()
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

                SqlCommand Cmd = new SqlCommand("FeeCategoryPartTermMapGetfiltered", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);


                List<FeeCategoryPartTermMap> ObjLstFEECONFIG = new List<FeeCategoryPartTermMap>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        FeeCategoryPartTermMap objFEECONFIG = new FeeCategoryPartTermMap();

                        objFEECONFIG.AcademicYearCode = (Convert.ToString(Dt.Rows[i]["AcademicYearCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);
                        objFEECONFIG.FeeTypeName = (Convert.ToString(Dt.Rows[i]["FeeTypeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FeeTypeName"]);
                        objFEECONFIG.InstancePartTermName = (Convert.ToString(Dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                        objFEECONFIG.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objFEECONFIG.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        objFEECONFIG.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["BranchName"]);
                        objFEECONFIG.AcademicYearId = (((Dt.Rows[i]["AcademicYearId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["AcademicYearId"]);
                        objFEECONFIG.ProgrammeInstancePartTermId = (((Dt.Rows[i]["ProgrammeInstancePartTermId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["ProgrammeInstancePartTermId"]);
                        objFEECONFIG.FeeTypeId = (((Dt.Rows[i]["FeeTypeId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["FeeTypeId"]);
                        objFEECONFIG.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objFEECONFIG.IsVerified = (Convert.ToString(Dt.Rows[i]["IsVerified"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsVerified"]);
                        if (objFEECONFIG.IsVerified == true)
                        {
                            objFEECONFIG.IsVerifiedCheck = false;
                        }
                        else
                        {
                            objFEECONFIG.IsVerifiedCheck = true;
                        }
                        objFEECONFIG.IsPublished = (Convert.ToString(Dt.Rows[i]["IsPublished"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsPublished"]);
                        if (objFEECONFIG.IsPublished == true)
                        {
                            objFEECONFIG.IsPublishedCheck = false;
                        }
                        else
                        {
                            objFEECONFIG.IsPublishedCheck = true;
                        }
                        if (objFEECONFIG.IsVerified == false)
                        {
                            objFEECONFIG.IsVerifiedSts = "Verify";
                        }
                        else
                        {
                            if (objFEECONFIG.IsPublished == true) { objFEECONFIG.IsVerifiedSts = "Published"; }
                            else { objFEECONFIG.IsVerifiedSts = "UnVerify"; }
                        }
                        ObjLstFEECONFIG.Add(objFEECONFIG);
                    }
                    return Return.returnHttp("200", ObjLstFEECONFIG, null);
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

        #region FeePaidStudentlistbyAYPTFT
        [HttpPost]
        public HttpResponseMessage FeePaidStudentlistbyAYPTFT(FeeConfiguration FEECONFIG)
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

                Int64 ProgrammeInstancePartTermId = Convert.ToInt64(FEECONFIG.ProgrammeInstancePartTermId);
                Int64 AcademicYearId = Convert.ToInt64(FEECONFIG.AcademicYearId);
                Int64 FeeTypeId = Convert.ToInt64(FEECONFIG.FeeTypeId);

                SqlCommand cmd = new SqlCommand("FeePaidStudentlistbyAYPTFT", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                cmd.Parameters.AddWithValue("@FeeTypeId", FeeTypeId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<AdmStudentAdmissionFeesPaid> ObjLstSPF = new List<AdmStudentAdmissionFeesPaid>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        AdmStudentAdmissionFeesPaid objSPF = new AdmStudentAdmissionFeesPaid();

                        objSPF.Id = (((Dt.Rows[i]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objSPF.InstalmentNo = (((Dt.Rows[i]["InstalmentNo"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["InstalmentNo"]);
                        objSPF.ApplicationId = (((Dt.Rows[i]["ApplicationId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["ApplicationId"]);
                        objSPF.PRN = (((Dt.Rows[i]["PRN"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["PRN"]);
                        objSPF.NameAsPerMarksheet = (((Dt.Rows[i]["NameAsPerMarksheet"]).ToString()).IsEmpty()) ? "Not Defined" : Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"]);
                        objSPF.AmountPaid = (((Dt.Rows[i]["AmountPaid"]).ToString()).IsEmpty()) ? 0 : Convert.ToDouble(Dt.Rows[i]["AmountPaid"]);
                        objSPF.TotalAmount = (((Dt.Rows[i]["TotalAmount"]).ToString()).IsEmpty()) ? 0 : Convert.ToDouble(Dt.Rows[i]["TotalAmount"]);

                        ObjLstSPF.Add(objSPF);
                    }
                }
                return Return.returnHttp("200", ObjLstSPF, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region FeePaidApplicantlistbyAYPTFT
        [HttpPost]
        public HttpResponseMessage FeePaidApplicantlistbyAYPTFT(FeeConfiguration FEECONFIG)
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

                Int64 ProgrammeInstancePartTermId = Convert.ToInt64(FEECONFIG.ProgrammeInstancePartTermId);
                Int64 AcademicYearId = Convert.ToInt64(FEECONFIG.AcademicYearId);
                Int64 FeeTypeId = Convert.ToInt64(FEECONFIG.FeeTypeId);

                SqlCommand cmd = new SqlCommand("FeePaidApplicantlistbyAYPTFT", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                cmd.Parameters.AddWithValue("@FeeTypeId", FeeTypeId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<AdmApplicationFeesPaid> ObjLstAPF = new List<AdmApplicationFeesPaid>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        AdmApplicationFeesPaid objAPF = new AdmApplicationFeesPaid();

                        objAPF.Id = (((Dt.Rows[i]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objAPF.AdmissionApplicationId = (((Dt.Rows[i]["AdmissionApplicationId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["AdmissionApplicationId"]);
                        objAPF.FeeConfigurationId = (((Dt.Rows[i]["FeeConfigurationId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["FeeConfigurationId"]);
                        objAPF.NameAsPerMarksheet = (((Dt.Rows[i]["NameAsPerMarksheet"]).ToString()).IsEmpty()) ? "Not Defined" : Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"]);
                        objAPF.FeesPaidInr = (((Dt.Rows[i]["FeesPaidInr"]).ToString()).IsEmpty()) ? 0 : Convert.ToDouble(Dt.Rows[i]["FeesPaidInr"]);
                        objAPF.IsApplicationFee = true;
                        ObjLstAPF.Add(objAPF);
                    }
                }
                return Return.returnHttp("200", ObjLstAPF, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region getFEECONFIGFilteredList
        [HttpPost]
        public HttpResponseMessage getFEECONFIGFilteredList(FeeConfiguration FEECONFIG)
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
                Int32 FeeTypeId = Convert.ToInt32(FEECONFIG.FeeTypeId);
                Int32 AcademicYearId = Convert.ToInt32(FEECONFIG.AcademicYearId);
                Int32 FacultyId = Convert.ToInt32(FEECONFIG.FacultyId);

                SqlCommand cmd = new SqlCommand("FeeCategoryPartTermMapGetfilteredbyFTAYF", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FeeTypeId", FeeTypeId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                cmd.Parameters.AddWithValue("@FacultyId", FacultyId);


                Da.SelectCommand = cmd;
                Da.Fill(Dt);


                List<FeeCategoryPartTermMap> ObjLstFEECONFIG = new List<FeeCategoryPartTermMap>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        FeeCategoryPartTermMap objFEECONFIG = new FeeCategoryPartTermMap();

                        objFEECONFIG.AcademicYearCode = (Convert.ToString(Dt.Rows[i]["AcademicYearCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);
                        objFEECONFIG.FeeTypeName = (Convert.ToString(Dt.Rows[i]["FeeTypeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FeeTypeName"]);
                        objFEECONFIG.InstancePartTermName = (Convert.ToString(Dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                        objFEECONFIG.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objFEECONFIG.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        objFEECONFIG.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["BranchName"]);
                        objFEECONFIG.AcademicYearId = (((Dt.Rows[i]["AcademicYearId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["AcademicYearId"]);
                        objFEECONFIG.ProgrammeInstancePartTermId = (((Dt.Rows[i]["ProgrammeInstancePartTermId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["ProgrammeInstancePartTermId"]);
                        objFEECONFIG.FeeTypeId = (((Dt.Rows[i]["FeeTypeId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["FeeTypeId"]);
                        objFEECONFIG.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objFEECONFIG.IsVerified = (Convert.ToString(Dt.Rows[i]["IsVerified"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsVerified"]);
                        if (objFEECONFIG.IsVerified == true)
                        {
                            objFEECONFIG.IsVerifiedCheck = false;
                        }
                        else
                        {
                            objFEECONFIG.IsVerifiedCheck = true;
                        }
                        objFEECONFIG.IsPublished = (Convert.ToString(Dt.Rows[i]["IsPublished"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsPublished"]);
                        if (objFEECONFIG.IsPublished == true)
                        {
                            objFEECONFIG.IsPublishedCheck = false;
                        }
                        else
                        {
                            objFEECONFIG.IsPublishedCheck = true;
                        }
                        if (objFEECONFIG.IsVerified == false)
                        {
                            objFEECONFIG.IsVerifiedSts = "Verify";
                        }
                        else
                        {
                            if (objFEECONFIG.IsPublished == true) { objFEECONFIG.IsVerifiedSts = "Published"; }
                            else { objFEECONFIG.IsVerifiedSts = "UnVerify"; }
                        }
                        ObjLstFEECONFIG.Add(objFEECONFIG);
                    }
                    return Return.returnHttp("200", ObjLstFEECONFIG, null);
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

        #region FeeConfigurationMultiAdd
        [HttpPost]
        public HttpResponseMessage FeeConfigurationMultiAdd(FeeConfiguration FEECONFIG)
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
                Int64 UserId = Convert.ToInt64(res.ToString());

                List<String> Sample = new List<String>();
                Int32 Count = Convert.ToInt32(FEECONFIG.Count);
                Int32 CountCategory = Convert.ToInt32(FEECONFIG.countTotal);
                SqlCommand[] CMDarray = new SqlCommand[Count];
                String[] strMessage = new String[Count];
                SqlCommand[] CMDarray1 = new SqlCommand[CountCategory];
                String[] strMessage1 = new String[CountCategory];
                String msg = "";
                String msg1 = "";
                int i = 0;
                int a = 0;


                if (String.IsNullOrEmpty(Convert.ToString(FEECONFIG.FeeTypeId))) { return Return.returnHttp("201", "Please select Fee Type", null); }
                else if (String.IsNullOrEmpty(Convert.ToString(FEECONFIG.AcademicYearId))) { return Return.returnHttp("201", "Please select Academic Year", null); }
                else if (String.IsNullOrEmpty(Convert.ToString(FEECONFIG.ProgrammeInstancePartTermId))) { return Return.returnHttp("201", "Please select Programme Instance Part Term", null); }
                else
                {
                    Int32 AcademicYearId = Convert.ToInt32(FEECONFIG.AcademicYearId);
                    Int64 ProgrammeInstancePartTermId = Convert.ToInt64(FEECONFIG.ProgrammeInstancePartTermId);
                    Int32 FeeTypeId = Convert.ToInt32(FEECONFIG.FeeTypeId);
                    var AmountList = FEECONFIG.AmountList;
                    var TotalAmountList = FEECONFIG.TotalAmountList;

                    foreach (MstFeeSubHead FeeSubHead in AmountList)
                    {
                        if (String.IsNullOrEmpty(Convert.ToString(FeeSubHead.Id))) { return Return.returnHttp("201", "Something Went Wrong Please Try Again", null); }
                        else if (String.IsNullOrEmpty(Convert.ToString(FeeSubHead.FeeHeadId))) { return Return.returnHttp("201", "Something Went Wrong Please Try Again", null); }
                        else
                        {
                            Int32 FeeHeadId = Convert.ToInt32(FeeSubHead.FeeHeadId);
                            Int32 FeeSubHeadId = Convert.ToInt32(FeeSubHead.Id);
                            Boolean AllowsNegativeValue = Convert.ToBoolean(FeeSubHead.AllowsNegativeValue);
                            var Amountlist = FeeSubHead.Amount;
                            foreach (Amount Amount in Amountlist)
                            {
                                if (String.IsNullOrEmpty(Convert.ToString(Amount.Id))) { return Return.returnHttp("201", "Something Went Wrong Please Try Again", null); }
                                else if (String.IsNullOrEmpty(Convert.ToString(Amount.FeeAmount))) { return Return.returnHttp("201", "Please Enter Valid value", null); }
                                //else if (validation.validateDigit(Convert.ToString(Amount.FeeAmount)) == false) { return Return.returnHttp("201", "Please Enter Valid value", null); }
                                else
                                {
                                    Int32 FeeCategoryId = Convert.ToInt32(Amount.Id);
                                    Double FeeAmount = Convert.ToDouble(Amount.FeeAmount);
                                    Boolean IsInstalmentAllowed = Convert.ToBoolean(Amount.IsInstalmentAllowed);
                                    if (AllowsNegativeValue == true)
                                    {
                                        CMDarray[i] = new SqlCommand("FeeConfigurationAdd", Con, ST);
                                        CMDarray[i].CommandType = CommandType.StoredProcedure;
                                        CMDarray[i].Parameters.AddWithValue("@IsInstalmentAllowed", IsInstalmentAllowed);
                                        CMDarray[i].Parameters.AddWithValue("@FeeTypeId", FeeTypeId);
                                        CMDarray[i].Parameters.AddWithValue("@FeeCategoryId", FeeCategoryId);
                                        CMDarray[i].Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                                        CMDarray[i].Parameters.AddWithValue("@FeeHeadId", FeeHeadId);
                                        CMDarray[i].Parameters.AddWithValue("@FeeSubHeadId", FeeSubHeadId);
                                        CMDarray[i].Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                                        CMDarray[i].Parameters.AddWithValue("@Amount", FeeAmount);
                                        CMDarray[i].Parameters.AddWithValue("@UserId", UserId);
                                        CMDarray[i].Parameters.AddWithValue("@UserTime", datetime);
                                        CMDarray[i].Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                                        CMDarray[i].Parameters["@Message"].Direction = ParameterDirection.Output;
                                        i++;
                                    }
                                    else
                                    {
                                        if ((Convert.ToDouble(FeeAmount)) < 0) { return Return.returnHttp("201", "Please enter Enter Valid value", null); }
                                        else
                                        {
                                            CMDarray[i] = new SqlCommand("FeeConfigurationAdd", Con, ST);
                                            CMDarray[i].CommandType = CommandType.StoredProcedure;
                                            CMDarray[i].Parameters.AddWithValue("@IsInstalmentAllowed", IsInstalmentAllowed);
                                            CMDarray[i].Parameters.AddWithValue("@FeeTypeId", FeeTypeId);
                                            CMDarray[i].Parameters.AddWithValue("@FeeCategoryId", FeeCategoryId);
                                            CMDarray[i].Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                                            CMDarray[i].Parameters.AddWithValue("@FeeHeadId", FeeHeadId);
                                            CMDarray[i].Parameters.AddWithValue("@FeeSubHeadId", FeeSubHeadId);
                                            CMDarray[i].Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                                            CMDarray[i].Parameters.AddWithValue("@Amount", FeeAmount);
                                            CMDarray[i].Parameters.AddWithValue("@UserId", UserId);
                                            CMDarray[i].Parameters.AddWithValue("@UserTime", datetime);
                                            CMDarray[i].Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                                            CMDarray[i].Parameters["@Message"].Direction = ParameterDirection.Output;
                                            i++;
                                        }
                                    }

                                    if (Sample.Contains(Convert.ToString(FeeCategoryId)))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        foreach (TotalValue Total in TotalAmountList)
                                        {
                                            if (FeeCategoryId == Convert.ToInt32(Total.Id))
                                            {
                                                Double TotalAmount = Convert.ToDouble(Total.TotalFee);
                                                Boolean IsInstallmentGiven = Convert.ToBoolean(Total.IsInstalmentGiven);
                                                Int32 NoOfInstalment = Convert.ToInt32(Total.NoOfInstalment);
                                                Sample.Add(Convert.ToString(FeeCategoryId));
                                                if (IsInstallmentGiven == true)
                                                {
                                                    if (NoOfInstalment < 2)
                                                    {
                                                        return Return.returnHttp("201", "Please Enter Valid Instalment number", null);
                                                    }
                                                }
                                                else if ((Convert.ToDouble(TotalAmount)) < 0) { return Return.returnHttp("201", "Please Enter Valid value", null); }

                                                CMDarray1[a] = new SqlCommand("FeeCategoryPartTermMapAdd", Con, ST);
                                                CMDarray1[a].CommandType = CommandType.StoredProcedure;
                                                CMDarray1[a].Parameters.AddWithValue("@FeeTypeId", FeeTypeId);
                                                CMDarray1[a].Parameters.AddWithValue("@FeeCategoryId", FeeCategoryId);
                                                CMDarray1[a].Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                                                CMDarray1[a].Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                                                CMDarray1[a].Parameters.AddWithValue("@TotalAmount", TotalAmount);
                                                CMDarray1[a].Parameters.AddWithValue("@IsInstalmentGiven", IsInstallmentGiven);
                                                CMDarray1[a].Parameters.AddWithValue("@NumberofInstalment", NoOfInstalment);
                                                CMDarray1[a].Parameters.AddWithValue("@UserId", UserId);
                                                CMDarray1[a].Parameters.AddWithValue("@UserTime", datetime);
                                                CMDarray1[a].Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                                                CMDarray1[a].Parameters["@Message"].Direction = ParameterDirection.Output;
                                                a++;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
                Con.Open();
                ST = Con.BeginTransaction();
                try
                {
                    for (int b = 0; b < a; b++)
                    {
                        CMDarray1[b].Transaction = ST;
                        int ans1 = CMDarray1[b].ExecuteNonQuery();
                        msg1 = msg1 + ", " + Convert.ToString(CMDarray[b].Parameters["@Message"].Value);
                    }
                    for (int j = 0; j < i; j++)
                    {
                        CMDarray[j].Transaction = ST;
                        int ans = CMDarray[j].ExecuteNonQuery();
                        msg = msg + ", " + Convert.ToString(CMDarray[j].Parameters["@Message"].Value);
                    }

                    ST.Commit();
                    //return Return.returnHttp("200", msg + "-=-" + msg1, null);
                    return Return.returnHttp("200", "Record Added", null);
                }
                catch (SqlException sqlError)
                {
                    ST.Rollback();
                    String strMessageErr = Convert.ToString(sqlError);
                    //return Return.returnHttp("201", strMessageErr, null);
                    return Return.returnHttp("201", "Record already exists", null);
                }

                //return Return.returnHttp("200", "Data Added Successfully", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
            finally
            {
                Con.Close();
            }
        }
        #endregion

        #region FeeConfigurationMulti1Edit
        [HttpPost]
        public HttpResponseMessage FeeConfigurationMulti1Edit(FeeConfiguration FEECONFIG)
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
                Int64 UserId = Convert.ToInt64(res.ToString());
                List<String> Sample = new List<String>();
                Int32 Count = Convert.ToInt32(FEECONFIG.Count);
                Int32 countTotal = Convert.ToInt32(FEECONFIG.countTotal);
                Int32 countDelete = Convert.ToInt32(FEECONFIG.countDelete);
                SqlCommand[] CMDarray = new SqlCommand[Count + countDelete];
                String[] strMessage = new String[Count + countDelete];
                SqlCommand[] CMDarray1 = new SqlCommand[countTotal + countDelete];
                String[] strMessage1 = new String[countTotal + countDelete];

                int i = 0;
                int a = 0;
                if (String.IsNullOrEmpty(Convert.ToString(FEECONFIG.FeeTypeId))) { return Return.returnHttp("201", "Please select Fee Type", null); }
                else if (String.IsNullOrEmpty(Convert.ToString(FEECONFIG.AcademicYearId))) { return Return.returnHttp("201", "Please select Academic Year", null); }
                else if (String.IsNullOrEmpty(Convert.ToString(FEECONFIG.ProgrammeInstancePartTermId))) { return Return.returnHttp("201", "Please select Programme Instance Part Term", null); }
                else
                {
                    Int32 AcademicYearId = Convert.ToInt32(FEECONFIG.AcademicYearId);
                    Int64 ProgrammeInstancePartTermId = Convert.ToInt64(FEECONFIG.ProgrammeInstancePartTermId);
                    Int32 FeeTypeId = Convert.ToInt32(FEECONFIG.FeeTypeId);
                    var AmountEditList = FEECONFIG.AmountEditList;
                    var TotalAmountEditList = FEECONFIG.TotalAmountEditList;
                    var TotalAmountDeleteList = FEECONFIG.TotalAmountDeleteList;
                    foreach (MstFeeSubHead FeeSubHead in AmountEditList)
                    {
                        if (String.IsNullOrEmpty(Convert.ToString(FeeSubHead.FeeHeadId))) { return Return.returnHttp("201", "Something Went Wrong Please Try Again", null); }
                        else if (String.IsNullOrEmpty(Convert.ToString(FeeSubHead.Id))) { return Return.returnHttp("201", "Something Went Wrong Please Try Again", null); }
                        else
                        {
                            Int32 FeeHeadId = Convert.ToInt32(FeeSubHead.FeeHeadId);
                            Int32 FeeSubHeadId = Convert.ToInt32(FeeSubHead.Id);
                            Boolean AllowsNegativeValue = Convert.ToBoolean(FeeSubHead.AllowsNegativeValue);
                            var Amountlist = FeeSubHead.Amount;
                            foreach (Amount Amount1 in Amountlist)
                            {
                                if (String.IsNullOrEmpty(Convert.ToString(Amount1.Id))) { return Return.returnHttp("201", "Something Went Wrong Please Try Again", null); }
                                else if (String.IsNullOrEmpty(Convert.ToString(Amount1.FeeAmount))) { return Return.returnHttp("201", "Please Enter Valid value", null); }
                                //else if (validation.validateDigit(Convert.ToString(Amount1.FeeAmount)) == false) { return Return.returnHttp("201", "Please enter Enter Valid value", null); }
                                //else if ((Convert.ToDouble(Amount1.FeeAmount)) < 0) { return Return.returnHttp("201", "Please enter Enter Valid value", null); }
                                else if (String.IsNullOrEmpty(Convert.ToString(Amount1.FeeConfigId))) { return Return.returnHttp("201", "Something Went Wrong Please Try Again", null); }
                                else
                                {
                                    Int32 FeeCategoryId = Convert.ToInt32(Amount1.Id);
                                    Double FeeAmount = Convert.ToDouble(Amount1.FeeAmount);
                                    Int64 FeeConfigId = Convert.ToInt64(Amount1.FeeConfigId);
                                    Boolean IsInstalmentAllowed = Convert.ToBoolean(Amount1.IsInstalmentAllowed);

                                    if (AllowsNegativeValue == true)
                                    {
                                        if (FeeConfigId == 0)
                                        {
                                            CMDarray[i] = new SqlCommand("FeeConfigurationAdd", Con, ST);
                                            CMDarray[i].CommandType = CommandType.StoredProcedure;
                                            CMDarray[i].Parameters.AddWithValue("@IsInstalmentAllowed", IsInstalmentAllowed);
                                            CMDarray[i].Parameters.AddWithValue("@FeeTypeId", FeeTypeId);
                                            CMDarray[i].Parameters.AddWithValue("@FeeCategoryId", FeeCategoryId);
                                            CMDarray[i].Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                                            CMDarray[i].Parameters.AddWithValue("@FeeHeadId", FeeHeadId);
                                            CMDarray[i].Parameters.AddWithValue("@FeeSubHeadId", FeeSubHeadId);
                                            CMDarray[i].Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                                            CMDarray[i].Parameters.AddWithValue("@Amount", FeeAmount);
                                            CMDarray[i].Parameters.AddWithValue("@UserId", UserId);
                                            CMDarray[i].Parameters.AddWithValue("@UserTime", datetime);
                                            CMDarray[i].Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                                            CMDarray[i].Parameters["@Message"].Direction = ParameterDirection.Output;

                                        }
                                        else
                                        {
                                            CMDarray[i] = new SqlCommand("FeeConfigurationEdit", Con, ST);
                                            CMDarray[i].CommandType = CommandType.StoredProcedure;
                                            CMDarray[i].Parameters.AddWithValue("@Id", FeeConfigId);
                                            CMDarray[i].Parameters.AddWithValue("@FeeTypeId", FeeTypeId);
                                            CMDarray[i].Parameters.AddWithValue("@IsInstalmentAllowed", IsInstalmentAllowed);
                                            CMDarray[i].Parameters.AddWithValue("@FeeCategoryId", FeeCategoryId);
                                            CMDarray[i].Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                                            CMDarray[i].Parameters.AddWithValue("@FeeHeadId", FeeHeadId);
                                            CMDarray[i].Parameters.AddWithValue("@FeeSubHeadId", FeeSubHeadId);
                                            CMDarray[i].Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                                            CMDarray[i].Parameters.AddWithValue("@Amount", FeeAmount);
                                            CMDarray[i].Parameters.AddWithValue("@UserId", UserId);
                                            CMDarray[i].Parameters.AddWithValue("@UserTime", datetime);
                                            CMDarray[i].Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                                            CMDarray[i].Parameters["@Message"].Direction = ParameterDirection.Output;
                                        }


                                        i++;
                                    }
                                    else
                                    {
                                        if ((Convert.ToDouble(FeeAmount)) < 0) { return Return.returnHttp("201", "Please Enter Valid value", null); }
                                        else
                                        {
                                            if (FeeConfigId == 0)
                                            {
                                                CMDarray[i] = new SqlCommand("FeeConfigurationAdd", Con, ST);
                                                CMDarray[i].CommandType = CommandType.StoredProcedure;
                                                CMDarray[i].Parameters.AddWithValue("@IsInstalmentAllowed", IsInstalmentAllowed);
                                                CMDarray[i].Parameters.AddWithValue("@FeeTypeId", FeeTypeId);
                                                CMDarray[i].Parameters.AddWithValue("@FeeCategoryId", FeeCategoryId);
                                                CMDarray[i].Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                                                CMDarray[i].Parameters.AddWithValue("@FeeHeadId", FeeHeadId);
                                                CMDarray[i].Parameters.AddWithValue("@FeeSubHeadId", FeeSubHeadId);
                                                CMDarray[i].Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                                                CMDarray[i].Parameters.AddWithValue("@Amount", FeeAmount);
                                                CMDarray[i].Parameters.AddWithValue("@UserId", UserId);
                                                CMDarray[i].Parameters.AddWithValue("@UserTime", datetime);
                                                CMDarray[i].Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                                                CMDarray[i].Parameters["@Message"].Direction = ParameterDirection.Output;

                                            }
                                            else
                                            {
                                                CMDarray[i] = new SqlCommand("FeeConfigurationEdit", Con, ST);
                                                CMDarray[i].CommandType = CommandType.StoredProcedure;
                                                CMDarray[i].Parameters.AddWithValue("@Id", FeeConfigId);
                                                CMDarray[i].Parameters.AddWithValue("@FeeTypeId", FeeTypeId);
                                                CMDarray[i].Parameters.AddWithValue("@IsInstalmentAllowed", IsInstalmentAllowed);
                                                CMDarray[i].Parameters.AddWithValue("@FeeCategoryId", FeeCategoryId);
                                                CMDarray[i].Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                                                CMDarray[i].Parameters.AddWithValue("@FeeHeadId", FeeHeadId);
                                                CMDarray[i].Parameters.AddWithValue("@FeeSubHeadId", FeeSubHeadId);
                                                CMDarray[i].Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                                                CMDarray[i].Parameters.AddWithValue("@Amount", FeeAmount);
                                                CMDarray[i].Parameters.AddWithValue("@UserId", UserId);
                                                CMDarray[i].Parameters.AddWithValue("@UserTime", datetime);
                                                CMDarray[i].Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                                                CMDarray[i].Parameters["@Message"].Direction = ParameterDirection.Output;
                                            }


                                            i++;
                                        }
                                    }



                                    if (Sample.Contains(Convert.ToString(FeeCategoryId)))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        foreach (TotalValue Total in TotalAmountEditList)
                                        {
                                            if (FeeCategoryId == Convert.ToInt32(Total.Id))
                                            {
                                                Int64 Id = Convert.ToInt64(Total.FeeCategoryPartTermId);
                                                Double TotalAmount = Convert.ToDouble(Total.TotalFee);
                                                Boolean IsInstallmentGiven = Convert.ToBoolean(Total.IsInstalmentGiven);
                                                Int32 NoOfInstalment = Convert.ToInt32(Total.NoOfInstalment);
                                                Sample.Add(Convert.ToString(FeeCategoryId));
                                                if ((Convert.ToDouble(Total.TotalFee)) < 0) { return Return.returnHttp("201", "Please Enter Valid value", null); }
                                                if (Id == 0)
                                                {
                                                    CMDarray1[a] = new SqlCommand("FeeCategoryPartTermMapAdd", Con, ST);
                                                    CMDarray1[a].CommandType = CommandType.StoredProcedure;
                                                    CMDarray1[a].Parameters.AddWithValue("@FeeTypeId", FeeTypeId);
                                                    CMDarray1[a].Parameters.AddWithValue("@FeeCategoryId", FeeCategoryId);
                                                    CMDarray1[a].Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                                                    CMDarray1[a].Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                                                    CMDarray1[a].Parameters.AddWithValue("@TotalAmount", TotalAmount);
                                                    CMDarray1[a].Parameters.AddWithValue("@IsInstalmentGiven", IsInstallmentGiven);
                                                    CMDarray1[a].Parameters.AddWithValue("@NumberofInstalment", NoOfInstalment);
                                                    CMDarray1[a].Parameters.AddWithValue("@UserId", UserId);
                                                    CMDarray1[a].Parameters.AddWithValue("@UserTime", datetime);
                                                    CMDarray1[a].Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                                                    CMDarray1[a].Parameters["@Message"].Direction = ParameterDirection.Output;
                                                }
                                                else
                                                {
                                                    CMDarray1[a] = new SqlCommand("FeeCategoryPartTermMapEdit", Con, ST);
                                                    CMDarray1[a].CommandType = CommandType.StoredProcedure;
                                                    CMDarray1[a].Parameters.AddWithValue("@Id", Id);
                                                    CMDarray1[a].Parameters.AddWithValue("@FeeTypeId", FeeTypeId);
                                                    CMDarray1[a].Parameters.AddWithValue("@FeeCategoryId", FeeCategoryId);
                                                    CMDarray1[a].Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                                                    CMDarray1[a].Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                                                    CMDarray1[a].Parameters.AddWithValue("@TotalAmount", TotalAmount);
                                                    CMDarray1[a].Parameters.AddWithValue("@IsInstalmentGiven", IsInstallmentGiven);
                                                    CMDarray1[a].Parameters.AddWithValue("@NumberofInstalment", NoOfInstalment);
                                                    CMDarray1[a].Parameters.AddWithValue("@UserId", UserId);
                                                    CMDarray1[a].Parameters.AddWithValue("@UserTime", datetime);
                                                    CMDarray1[a].Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                                                    CMDarray1[a].Parameters["@Message"].Direction = ParameterDirection.Output;
                                                }
                                                a++;
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                    foreach (TotalValue Total in TotalAmountDeleteList)
                    {
                        Int32 FeeCateId = Convert.ToInt32(Total.Id);
                        Int64 FCPTMId = Convert.ToInt32(Total.FeeCategoryPartTermId);
                        if (FCPTMId != 0)
                        {
                            CMDarray1[a] = new SqlCommand("FeeConfigurationDelete", Con, ST);
                            CMDarray1[a].CommandType = CommandType.StoredProcedure;
                            CMDarray1[a].Parameters.AddWithValue("@FeeTypeId", FeeTypeId);
                            CMDarray1[a].Parameters.AddWithValue("@FeeCategoryId", FeeCateId);
                            CMDarray1[a].Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                            CMDarray1[a].Parameters.AddWithValue("@UserId", UserId);
                            CMDarray1[a].Parameters.AddWithValue("@UserTime", datetime);
                            CMDarray1[a].Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                            CMDarray1[a].Parameters["@Message"].Direction = ParameterDirection.Output;


                            CMDarray[i] = new SqlCommand("FeeCategoryPartTermMapDelete", Con, ST);
                            CMDarray[i].CommandType = CommandType.StoredProcedure;
                            CMDarray[i].Parameters.AddWithValue("@Id", FCPTMId);
                            CMDarray[i].Parameters.AddWithValue("@UserId", UserId);
                            CMDarray[i].Parameters.AddWithValue("@UserTime", datetime);
                            CMDarray[i].Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                            CMDarray[i].Parameters["@Message"].Direction = ParameterDirection.Output;
                            i++;
                            a++;
                        }


                    }
                }
                Con.Open();
                ST = Con.BeginTransaction();
                try
                {
                    for (int b = 0; b < a; b++)
                    {
                        CMDarray1[b].Transaction = ST;
                        int ans1 = CMDarray1[b].ExecuteNonQuery();
                        strMessage1[b] = Convert.ToString(CMDarray1[b].Parameters["@Message"].Value);
                    }
                    for (int j = 0; j < i; j++)
                    {
                        CMDarray[j].Transaction = ST;
                        int ans = CMDarray[j].ExecuteNonQuery();
                        strMessage[j] = Convert.ToString(CMDarray[j].Parameters["@Message"].Value);
                    }

                    ST.Commit();
                    return Return.returnHttp("200", "Data Edited Successfully", null);
                }
                catch (SqlException sqlError)
                {
                    ST.Rollback();
                    String strMessageErr = Convert.ToString(sqlError);
                    //return Return.returnHttp("201", strMessageErr, null);
                    return Return.returnHttp("201", "Some Error Occuered, Please try again", null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
            finally
            {
                Con.Close();
            }
        }
        #endregion

        #region FeeConfigurationIsActiveEnable
        [HttpPost]
        public HttpResponseMessage FeeConfigurationIsActiveEnable(FeeConfiguration FEECONFIG)
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
                Int64 UserId = Convert.ToInt64(res.ToString());
                Int64 Id = Convert.ToInt64(FEECONFIG.Id);

                SqlCommand cmd = new SqlCommand("FeeConfigurationActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Flag", "FeeConfigurationIsActiveEnable");
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                Con.Close();
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region FeeConfigurationIsActiveDisable
        [HttpPost]
        public HttpResponseMessage FeeConfigurationIsActiveDisable(FeeConfiguration FEECONFIG)
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
                Int64 UserId = Convert.ToInt64(res.ToString());
                Int64 Id = Convert.ToInt64(FEECONFIG.Id);

                SqlCommand cmd = new SqlCommand("FeeConfigurationActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Flag", "FeeConfigurationIsActiveDisable");
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                Con.Close();
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region FeeConfigurationDelete
        [HttpPost]
        public HttpResponseMessage FeeConfigurationDelete(FeeConfiguration FEECONFIG)
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
                Int64 UserId = Convert.ToInt64(res.ToString());
                Int64 Id = Convert.ToInt64(FEECONFIG.Id);

                SqlCommand cmd = new SqlCommand("FeeConfigurationDelete", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);

                Con.Close();
                return Return.returnHttp("201", "Record Deleted", null);
                //return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region FeeConfigurationGetbyId
        [HttpPost]
        public HttpResponseMessage FeeConfigurationGetbyId(FeeConfiguration FEECONFIG)
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

                Int64 Id = Convert.ToInt64(FEECONFIG.Id);

                SqlCommand cmd = new SqlCommand("FeeConfigurationGetbyId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<FeeConfiguration> ObjLstFEECONFIG = new List<FeeConfiguration>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        FeeConfiguration objFEECONFIG = new FeeConfiguration();

                        objFEECONFIG.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objFEECONFIG.FeeTypeFeeCategoryMapId = Convert.ToInt64(Dt.Rows[i]["FeeTypeFeeCategoryMapId"]);
                        objFEECONFIG.FeeTypeId = Convert.ToInt32(Dt.Rows[i]["FeeTypeId"]);
                        objFEECONFIG.FeeTypeName = Convert.ToString(Dt.Rows[i]["FeeTypeName"]);
                        objFEECONFIG.FeeTypeCode = Convert.ToString(Dt.Rows[i]["FeeTypeCode"]);
                        objFEECONFIG.FeeCategoryId = Convert.ToInt32(Dt.Rows[i]["FeeCategoryId"]);
                        objFEECONFIG.FeeCategoryName = Convert.ToString(Dt.Rows[i]["FeeCategoryName"]);
                        objFEECONFIG.FeeCategoryNameOriginal = Convert.ToString(Dt.Rows[i]["FeeCategoryNameOriginal"]);
                        objFEECONFIG.FeeCategoryCode = Convert.ToString(Dt.Rows[i]["FeeCategoryCode"]);

                        objFEECONFIG.ProgrammeInstancePartTermId = Convert.ToInt64(Dt.Rows[i]["ProgrammeInstancePartTermId"]);
                        objFEECONFIG.ProgrammePartTermName = Convert.ToString(Dt.Rows[i]["ProgrammePartTermName"]);

                        objFEECONFIG.FacultyId = Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        objFEECONFIG.FacultyName = Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objFEECONFIG.FacultyCode = Convert.ToString(Dt.Rows[i]["FacultyCode"]);
                        objFEECONFIG.FacultyAddress = Convert.ToString(Dt.Rows[i]["FacultyAddress"]);
                        objFEECONFIG.ProgrammeInstanceId = Convert.ToInt64(Dt.Rows[i]["ProgrammeInstanceId"]);
                        objFEECONFIG.ProgrammeId = Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);
                        objFEECONFIG.ProgrammeName = Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        objFEECONFIG.ProgrammeCode = Convert.ToString(Dt.Rows[i]["ProgrammeCode"]);
                        objFEECONFIG.ProgrammeBranchMapId = Convert.ToInt32(Dt.Rows[i]["ProgrammeBranchMapId"]);
                        objFEECONFIG.ProgrammeInstancePartId = Convert.ToInt64(Dt.Rows[i]["ProgrammeInstancePartId"]);
                        objFEECONFIG.PartName = Convert.ToString(Dt.Rows[i]["PartName"]);

                        objFEECONFIG.FeeHeadId = Convert.ToInt32(Dt.Rows[i]["FeeHeadId"]);
                        objFEECONFIG.FeeHeadName = Convert.ToString(Dt.Rows[i]["FeeHeadName"]);
                        objFEECONFIG.FeeHeadCode = Convert.ToString(Dt.Rows[i]["FeeHeadCode"]);
                        objFEECONFIG.FeeSubHeadId = Convert.ToInt32(Dt.Rows[i]["FeeSubHeadId"]);
                        objFEECONFIG.FeeSubHeadName = Convert.ToString(Dt.Rows[i]["FeeSubHeadName"]);
                        objFEECONFIG.FeeSubHeadCode = Convert.ToString(Dt.Rows[i]["FeeSubHeadCode"]);

                        objFEECONFIG.AcademicYearId = Convert.ToInt32(Dt.Rows[i]["AcademicYearId"]);
                        objFEECONFIG.AcademicYearCode = Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);

                        objFEECONFIG.Amount = Convert.ToDouble(Dt.Rows[i]["Amount"]);
                        objFEECONFIG.IsEditableSts = Convert.ToString(Dt.Rows[i]["IsEditableSts"]);
                        objFEECONFIG.IsEditable = Convert.ToBoolean(Dt.Rows[i]["IsEditable"]);
                        objFEECONFIG.IsVerifiedSts = Convert.ToString(Dt.Rows[i]["IsVerifiedSts"]);
                        objFEECONFIG.IsVerified = Convert.ToBoolean(Dt.Rows[i]["IsVerified"]);
                        objFEECONFIG.IsActiveSts = Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        objFEECONFIG.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjLstFEECONFIG.Add(objFEECONFIG);
                    }
                }
                return Return.returnHttp("200", ObjLstFEECONFIG, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region FeeCategorylistbyAYPTFT
        [HttpPost]
        public HttpResponseMessage FeeCategorylistbyAYPTFT(FeeConfiguration FEECONFIG)
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

                Int64 ProgrammeInstancePartTermId = Convert.ToInt64(FEECONFIG.ProgrammeInstancePartTermId);
                Int64 AcademicYearId = Convert.ToInt64(FEECONFIG.AcademicYearId);
                Int64 FeeTypeId = Convert.ToInt64(FEECONFIG.FeeTypeId);

                SqlCommand cmd = new SqlCommand("FeeCategorylistbyAYPTFT", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                cmd.Parameters.AddWithValue("@FeeTypeId", FeeTypeId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<MstFeeCategory> ObjLstFC = new List<MstFeeCategory>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstFeeCategory objFC = new MstFeeCategory();

                        objFC.FeeCategoryPartTermId = (((Dt.Rows[i]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objFC.Id = (((Dt.Rows[i]["FeeCategoryId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["FeeCategoryId"]);
                        objFC.NoOfInstalment = (((Dt.Rows[i]["NumberofInstalment"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["NumberofInstalment"]);
                        objFC.FeeCategoryName = (Convert.ToString(Dt.Rows[i]["FeeCategoryName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FeeCategoryName"]);
                        objFC.FeeCategoryCode = (Convert.ToString(Dt.Rows[i]["FeeCategoryCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FeeCategoryCode"]);
                        objFC.CategoryPeriodName = (Convert.ToString(Dt.Rows[i]["CategoryPeriodName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["CategoryPeriodName"]);
                        objFC.IsInstalmentGiven = (Convert.ToString(Dt.Rows[i]["IsInstalmentGiven"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsInstalmentGiven"]);
                        objFC.IsVerified = (Convert.ToString(Dt.Rows[i]["IsVerified"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsVerified"]);
                        ObjLstFC.Add(objFC);
                    }
                }
                return Return.returnHttp("200", ObjLstFC, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region FeeCategoryfullistbyAYPTFT
        [HttpPost]
        public HttpResponseMessage FeeCategoryfullistbyAYPTFT(FeeConfiguration FEECONFIG)
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

                Int64 ProgrammeInstancePartTermId = Convert.ToInt64(FEECONFIG.ProgrammeInstancePartTermId);
                Int64 AcademicYearId = Convert.ToInt64(FEECONFIG.AcademicYearId);
                Int64 FeeTypeId = Convert.ToInt64(FEECONFIG.FeeTypeId);

                SqlCommand cmd = new SqlCommand("FeeCategoryfullistbyAYPTFT", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                cmd.Parameters.AddWithValue("@FeeTypeId", FeeTypeId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<MstFeeCategory> ObjLstFC = new List<MstFeeCategory>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstFeeCategory objFC = new MstFeeCategory();

                        objFC.FeeCategoryPartTermId = (((Dt.Rows[i]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objFC.Id = (((Dt.Rows[i]["FeeCategoryId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["FeeCategoryId"]);
                        objFC.NoOfInstalment = (((Dt.Rows[i]["NumberofInstalment"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["NumberofInstalment"]);
                        objFC.FeeCategoryName = (Convert.ToString(Dt.Rows[i]["FeeCategoryName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FeeCategoryName"]);
                        objFC.FeeCategoryCode = (Convert.ToString(Dt.Rows[i]["FeeCategoryCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FeeCategoryCode"]);
                        objFC.CategoryPeriodName = (Convert.ToString(Dt.Rows[i]["CategoryPeriodName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["CategoryPeriodName"]);
                        objFC.IsInstalmentGiven = (Convert.ToString(Dt.Rows[i]["IsInstalmentGiven"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsInstalmentGiven"]);
                        ObjLstFC.Add(objFC);
                    }
                }
                return Return.returnHttp("200", ObjLstFC, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region FeeConfigurationGetByAYPTFT
        [HttpPost]
        public HttpResponseMessage FeeConfigurationGetByAYPTFT(FeeConfiguration FEECONFIG)
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

                Int64 ProgrammeInstancePartTermId = Convert.ToInt64(FEECONFIG.ProgrammeInstancePartTermId);
                Int64 AcademicYearId = Convert.ToInt64(FEECONFIG.AcademicYearId);
                Int64 FeeTypeId = Convert.ToInt64(FEECONFIG.FeeTypeId);
                //Int32 Count = 0;
                SqlCommand cmd = new SqlCommand("FeeSubHeadGetByAYPTFT", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                cmd.Parameters.AddWithValue("@FeeTypeId", FeeTypeId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<MstFeeSubHead> ObjLstFSH = new List<MstFeeSubHead>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstFeeSubHead objFSH = new MstFeeSubHead();

                        objFSH.FeeHeadId = Convert.ToInt32(Dt.Rows[i]["FeeHeadId"]);
                        objFSH.FeeHeadName = Convert.ToString(Dt.Rows[i]["FeeHeadName"]);
                        objFSH.Id = Convert.ToInt32(Dt.Rows[i]["FeeSubHeadId"]);
                        objFSH.FeeSubHeadName = Convert.ToString(Dt.Rows[i]["FeeSubHeadName"]);
                        objFSH.FeeSubHeadSrno = (((Dt.Rows[i]["FeeSubHeadSrno"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["FeeSubHeadSrno"]);
                        objFSH.AllowsNegativeValue = (((Dt.Rows[i]["AllowsNegativeValue"]).ToString()).IsEmpty()) ? false : Convert.ToBoolean(Dt.Rows[i]["AllowsNegativeValue"]);

                        SqlCommand cmdFC = new SqlCommand("FeeCategoryAmountGetByAYPTFTFHFSH", Con);
                        cmdFC.CommandType = CommandType.StoredProcedure;

                        cmdFC.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                        cmdFC.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                        cmdFC.Parameters.AddWithValue("@FeeTypeId", FeeTypeId);
                        cmdFC.Parameters.AddWithValue("@FeeHeadId", objFSH.FeeHeadId);
                        cmdFC.Parameters.AddWithValue("@FeeSubHeadId", objFSH.Id);
                        SqlDataAdapter DaFC = new SqlDataAdapter();
                        DataTable DtFC = new DataTable();
                        DaFC.SelectCommand = cmdFC;
                        DaFC.Fill(DtFC);

                        List<Amount> Amount = new List<Amount>();
                        for (int j = 0; j < DtFC.Rows.Count; j++)
                        {
                            Amount ObjFC = new Amount();
                            ObjFC.FeeCategoryCode = Convert.ToString(DtFC.Rows[j]["FeeCategoryCode"]);
                            ObjFC.FeeCategoryName = Convert.ToString(DtFC.Rows[j]["FeeCategoryName"]);
                            ObjFC.Id = Convert.ToInt32(DtFC.Rows[j]["Id"]);
                            ObjFC.FeeAmount = Convert.ToDouble(DtFC.Rows[j]["Amount"]);
                            ObjFC.FeeConfigId = Convert.ToInt64(DtFC.Rows[j]["FeeConfigId"]);
                            ObjFC.IsInstalmentAllowed = Convert.ToBoolean(DtFC.Rows[j]["IsInstalmentAllowed"]);
                            ObjFC.IsInstalmentGiven = Convert.ToBoolean(DtFC.Rows[j]["IsInstalmentGiven"]);
                            if (ObjFC.IsInstalmentGiven == true)
                            {
                                ObjFC.InstalmentGivenCheck = false;
                            }
                            else
                            {
                                ObjFC.InstalmentGivenCheck = true;
                            }
                            var IsPublished = DtFC.Rows[j]["IsPublished"];
                            if (IsPublished is DBNull)
                            {
                                IsPublished = false;
                                ObjFC.IsPublished = Convert.ToBoolean(IsPublished);
                            }
                            else
                            {
                                ObjFC.IsPublished = Convert.ToBoolean(DtFC.Rows[j]["IsPublished"]);
                            }
                            //Count++;
                            Amount.Add(ObjFC);
                        }
                        objFSH.Amount = Amount;
                        ObjLstFSH.Add(objFSH);
                    }

                }

                return Return.returnHttp("200", ObjLstFSH, null);
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

        #region FacultyGet
        [HttpPost]
        public HttpResponseMessage FacultyGet()
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
                        if (Dt.Rows[i]["Id"].ToString() != "")
                        {
                            objFaculty.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);

                        }
                        if (Dt.Rows[i]["FacultyName"].ToString() != "")
                        {
                            objFaculty.FacultyName = Convert.ToString(Dt.Rows[i]["FacultyName"]);

                        }
                        if (Dt.Rows[i]["FacultyCode"].ToString() != "")
                        {
                            objFaculty.FacultyCode = Convert.ToString(Dt.Rows[i]["FacultyCode"]);

                        }
                        if (Dt.Rows[i]["FacultyAddress"].ToString() != "")
                        {
                            objFaculty.FacultyAddress = Convert.ToString(Dt.Rows[i]["FacultyAddress"]);

                        }
                        if (Dt.Rows[i]["CityName"].ToString() != "")
                        {
                            objFaculty.CityName = Convert.ToString(Dt.Rows[i]["CityName"]);

                        }
                        if (Dt.Rows[i]["Pincode"].ToString() != "")
                        {
                            objFaculty.Pincode = Convert.ToInt32(Dt.Rows[i]["Pincode"]);

                        }
                        if (Dt.Rows[i]["FacultyContactNo"].ToString() != "")
                        {
                            objFaculty.FacultyContactNo = Convert.ToString(Dt.Rows[i]["FacultyContactNo"]);

                        }
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

        #region InstanceListGetbyFacultyIdAndAcadId
        [HttpPost]
        public HttpResponseMessage InstanceListGetbyFacultyIdAndAcadId(ProgramInstance ObjProgInstance)
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

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("IncProgrammeInstanceGetByFacultyIdAndAcadId", Con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@FacultyId", ObjProgInstance.FacultyId);
                da.SelectCommand.Parameters.AddWithValue("@AcademicYearId", ObjProgInstance.AcademicYearId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<ProgramInstance> ObjLstProgInst = new List<ProgramInstance>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProgramInstance ObjProgInst = new ProgramInstance();
                        ObjProgInst.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjProgInst.FacultyName = Convert.ToString(dt.Rows[i]["FacultyName"]);
                        ObjProgInst.FacultyId = Convert.ToInt32(dt.Rows[i]["FacultyId"]);
                        ObjProgInst.ProgrammeId = Convert.ToInt32(dt.Rows[i]["ProgrammeId"]);
                        ObjProgInst.ProgrammeName = Convert.ToString(dt.Rows[i]["ProgrammeName"]);
                        ObjProgInst.InstanceName = Convert.ToString(dt.Rows[i]["InstanceName"]);
                        //ObjProgInst.BranchName = Convert.ToString(dt.Rows[i]["BranchName"]);
                        //ObjProgInst.SpecialisationId = Convert.ToInt32(dt.Rows[i]["SpecialisationId"]);
                        ObjProgInst.AcademicYear = Convert.ToString(dt.Rows[i]["AcademicYearCode"]);
                        ObjProgInst.AcademicYearId = Convert.ToInt32(dt.Rows[i]["AcademicYearId"]);
                        ObjProgInst.Intake = Convert.ToInt32(dt.Rows[i]["Intake"]);
                        ObjProgInst.IsActive = Convert.ToBoolean(dt.Rows[i]["IsActive"]);
                        ObjProgInst.IsActiveSts = Convert.ToString(dt.Rows[i]["IsActiveSts"]);
                        ObjProgInst.IsDeleted = Convert.ToBoolean(dt.Rows[i]["IsDeleted"]);
                        //ObjProgInst.CreatedBy = Convert.ToInt64(dt.Rows[i]["CreatedBy"]);
                        //ObjProgInst.CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]);
                        //ObjProgInst.ModifiedBy = Convert.ToInt64(dt.Rows[i]["ModifiedBy"]);
                        //ObjProgInst.ModifiedOn = Convert.ToDateTime(dt.Rows[i]["ModifiedOn"]);

                        ObjLstProgInst.Add(ObjProgInst);

                    }
                    return Return.returnHttp("200", ObjLstProgInst, null);
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


        #region ProgrammeGetbyFacultyIdAndAcadId
        [HttpPost]
        public HttpResponseMessage ProgrammeGetbyFacultyIdAndAcadId(ProgramInstance ObjProgInstance)
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

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("MstProgrammeGetByFacultyIdAndAcadId", Con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@FacultyId", ObjProgInstance.FacultyId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<ProgramInstance> ObjLstProgInst = new List<ProgramInstance>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProgramInstance ObjProgInst = new ProgramInstance();
                        ObjProgInst.FacultyName = Convert.ToString(dt.Rows[i]["FacultyName"]);
                        ObjProgInst.FacultyId = Convert.ToInt32(dt.Rows[i]["FacultyId"]);
                        ObjProgInst.ProgrammeId = Convert.ToInt32(dt.Rows[i]["ProgrammeId"]);
                        ObjProgInst.ProgrammeName = Convert.ToString(dt.Rows[i]["ProgrammeName"]);

                        ObjLstProgInst.Add(ObjProgInst);

                    }
                    return Return.returnHttp("200", ObjLstProgInst, null);
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
                List<MstProgrammePart> ObjLstProgPart = new List<MstProgrammePart>();
                BALProgrammePart ObjBalProgPartList = new BALProgrammePart();
                ObjLstProgPart = ObjBalProgPartList.ProgrammePartGetByProgrammeId(ObjProgInst.ProgrammeId);
                if (ObjLstProgPart.Any())
                {

                    return Return.returnHttp("200", ObjLstProgPart, null);
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

        #region MstProgrammeBranchListGetByProgrammeId
        [HttpPost]
        public HttpResponseMessage MstProgrammeBranchListGetByProgrammeId(ProgramInstance ObjProg)
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

                int ProgrammeId = Convert.ToInt32(ObjProg.ProgrammeId);
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter cmdda = new SqlDataAdapter("MstProgrammeBranchMapGetByProgrammeId", Con);

                cmdda.SelectCommand.CommandType = CommandType.StoredProcedure;
                cmdda.SelectCommand.Parameters.AddWithValue("@ProgrammeId", ProgrammeId);

                DataTable Dt = new DataTable();
                cmdda.Fill(Dt);


                List<MstSpecialisation> ObjLstMstSpecialisation = new List<MstSpecialisation>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstSpecialisation ObjMstSpecialisation = new MstSpecialisation();

                        ObjMstSpecialisation.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        ObjMstSpecialisation.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        ObjMstSpecialisation.ProgrammeCode = (Convert.ToString(Dt.Rows[i]["ProgrammeCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeCode"]);
                        ObjMstSpecialisation.BranchName = ((Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "Not Defined" : (Convert.ToString(Dt.Rows[i]["BranchName"])));
                        ObjMstSpecialisation.FacultyId = (((Dt.Rows[i]["FacultyId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        ObjMstSpecialisation.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        ObjMstSpecialisation.ProgrammeId = (Convert.ToString(Dt.Rows[i]["ProgrammeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);
                        ObjMstSpecialisation.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        ObjMstSpecialisation.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjMstSpecialisation.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);

                        ObjLstMstSpecialisation.Add(ObjMstSpecialisation);
                    }
                    return Return.returnHttp("200", ObjLstMstSpecialisation, null);
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

        #region MstProgrammeBranchListGetByProgInstanceId
        [HttpPost]
        public HttpResponseMessage MstProgrammeBranchListGetByProgInstanceId(ProgramInstance ObjProg)
        {
            try
            {
                Request.Headers.TryGetValues("DepartmentId", out IEnumerable<string> DepartmentId1);
                var DepartmentId = DepartmentId1?.FirstOrDefault();
                int ProgInstId = Convert.ToInt32(ObjProg.ProgrammeInstanceId);
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter cmdda = new SqlDataAdapter("MstSpecialisationGetbyProgInstId", Con);

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

            //try
            //{
            //    String token = Request.Headers.GetValues("token").FirstOrDefault();
            //    String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
            //    String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
            //    TokenOperation ac = new TokenOperation();
            //    string res = ac.ValidateToken(token);
            //    string resRoleToken = ac.ValidateRoleToken(userRoleToken);

            //    if (res == "0")
            //    {
            //        return Return.returnHttp("0", null, null);
            //    }
            //    else if (resRoleToken == "0")
            //    {
            //        return Return.returnHttp("0", null, null);
            //    }

            //    int ProgInstId = Convert.ToInt32(ObjProg.ProgrammeInstancePartId);
            //    SqlCommand cmd = new SqlCommand();
            //    SqlDataAdapter cmdda = new SqlDataAdapter("MstSpecialisationGetByProgrammeId", Con);

            //    cmdda.SelectCommand.CommandType = CommandType.StoredProcedure;
            //    cmdda.SelectCommand.Parameters.AddWithValue("@ProgrammeId", ProgInstId);

            //    DataTable Dt = new DataTable();
            //    cmdda.Fill(Dt);


            //    List<MstSpecialisation> ObjLstMstSpecialisation = new List<MstSpecialisation>();

            //    if (Dt.Rows.Count > 0)
            //    {
            //        for (int i = 0; i < Dt.Rows.Count; i++)
            //        {
            //            MstSpecialisation objS = new MstSpecialisation();

            //            objS.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
            //            objS.BranchName = Convert.ToString(Dt.Rows[i]["BranchName"]);
            //            objS.ProgrammeId = Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);
            //            objS.ProgrammeName = Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
            //            ObjLstMstSpecialisation.Add(objS);
            //        }
            //        return Return.returnHttp("200", ObjLstMstSpecialisation, null);
            //    }
            //    else
            //    {
            //        return Return.returnHttp("201", "No Record Found", null);
            //    }

            //}
            //catch (Exception e)
            //{
            //    return Return.returnHttp("201", e.Message, null);
            //}

        }
        #endregion

        #region MstSpecialisationGetByPId
        [HttpPost]
        public HttpResponseMessage MstSpecialisationGetByPId(MstSpecialisation ObjSpe)
        {
            try
            {


                SqlCommand Cmd = new SqlCommand("MstSpecialisationGetByPId", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@ProgrammeId", ObjSpe.ProgrammeId);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);


                List<MstSpecialisation> ObjLstSpe = new List<MstSpecialisation>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstSpecialisation objS = new MstSpecialisation();

                        objS.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objS.BranchName = Convert.ToString(Dt.Rows[i]["BranchName"]);
                        objS.ProgrammeId = Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);
                        objS.ProgrammeName = Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        ObjLstSpe.Add(objS);
                    }
                }
                return Return.returnHttp("200", ObjLstSpe, null);
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

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("MstProgrammePartGetByProgrammeId", Con);
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

        #region Programme Part Name List on the basis of Programme Instance Id
        [HttpPost]
        public HttpResponseMessage ProgrammePartGetByProgInstId(ProgrammeInstancePartTerm ObjProgInstPartTerm)
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
                    return Return.returnHttp("201", "No Record Found", null);
                }

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region ProgrammePartTermGetByProgInstId
        [HttpPost]
        public HttpResponseMessage ProgrammePartTermGetByProgInstId(ProgrammeInstancePartTerm ObjProgInstPartTerm)
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

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("IncProgrammePartTermGetByProgrammeInstanceId", Con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@AcademicYearId", ObjProgInstPartTerm.AcademicYearId);
                da.SelectCommand.Parameters.AddWithValue("@FacultyId", ObjProgInstPartTerm.FacultyId);
                da.SelectCommand.Parameters.AddWithValue("@ProgrammeId", ObjProgInstPartTerm.ProgrammeId);
                da.SelectCommand.Parameters.AddWithValue("@ProgrammePartId", ObjProgInstPartTerm.ProgrammePartId);
                da.SelectCommand.Parameters.AddWithValue("@SpecialisationId", ObjProgInstPartTerm.SpecialisationId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<ProgrammeInstancePartTerm> ObjLstProgPartTerm = new List<ProgrammeInstancePartTerm>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePartTerm ObjProgPartTerm = new ProgrammeInstancePartTerm();




                        ObjProgPartTerm.Id = Convert.ToInt32(dt.Rows[i]["ProgrammeInstancePartTermId"]);
                        ObjProgPartTerm.PartTermShortName = Convert.ToString(dt.Rows[i]["InstancePartTermName"]);
                        ObjProgPartTerm.ProgrammePartTermId = Convert.ToInt32(dt.Rows[i]["ProgrammePartTermId"]);

                        ObjLstProgPartTerm.Add(ObjProgPartTerm);

                    }
                    return Return.returnHttp("200", ObjLstProgPartTerm, null);
                }
                else
                {
                    return Return.returnHttp("201", "No Record Found ++", null);
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
        public HttpResponseMessage ProgrammePartTermGetByPartId(ProgrammeInstancePartTerm ObjProgInstPartTerm)
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

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("MstProgrammePartTermGetByPartId", Con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@PartId", ObjProgInstPartTerm.ProgrammePartId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<MstProgrammePartTerm> ObjLstProgPartTerm = new List<MstProgrammePartTerm>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        MstProgrammePartTerm ObjProgPartTerm = new MstProgrammePartTerm();
                        ObjProgPartTerm.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjProgPartTerm.FacultyId = Convert.ToInt32(dt.Rows[i]["FacultyId"]);
                        ObjProgPartTerm.FacultyName = Convert.ToString(dt.Rows[i]["FacultyName"]);
                        ObjProgPartTerm.ProgrammeName = Convert.ToString(dt.Rows[i]["ProgrammeName"]);
                        ObjProgPartTerm.ProgrammeId = Convert.ToInt32(dt.Rows[i]["ProgrammeId"]);
                        ObjProgPartTerm.PartTermName = Convert.ToString(dt.Rows[i]["PartTermName"]);
                        ObjProgPartTerm.PartTermShortName = Convert.ToString(dt.Rows[i]["PartTermShortName"]);
                        ObjProgPartTerm.SequenceNo = Convert.ToInt32(dt.Rows[i]["SequenceNo"]);
                        ObjProgPartTerm.IsActive = Convert.ToBoolean(dt.Rows[i]["IsActive"]);
                        ObjProgPartTerm.IsActiveSts = Convert.ToString(dt.Rows[i]["IsActiveSts"]);
                        ObjProgPartTerm.IsDeleted = Convert.ToBoolean(dt.Rows[i]["IsDeleted"]);
                        //ObjProgPartTerm.CreatedBy = Convert.ToInt64(dt.Rows[i]["CreatedBy"]);
                        //ObjProgPartTerm.CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]);
                        //ObjProgPartTerm.ModifiedBy = Convert.ToInt64(dt.Rows[i]["ModifiedBy"]);
                        //ObjProgPartTerm.ModifiedOn = Convert.ToDateTime(dt.Rows[i]["ModifiedOn"]);

                        ObjLstProgPartTerm.Add(ObjProgPartTerm);

                    }
                    return Return.returnHttp("200", ObjLstProgPartTerm, null);
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

        #region FeeCategorylistbyAYPIFT
        [HttpPost]
        public HttpResponseMessage FeeCategorylistbyAYPIFT(FeeConfiguration FEECONFIG)
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

                Int64 ProgrammeInstanceId = Convert.ToInt64(FEECONFIG.ProgrammeInstanceId);
                Int64 AcademicYearId = Convert.ToInt64(FEECONFIG.AcademicYearId);
                Int64 FeeTypeId = Convert.ToInt64(FEECONFIG.FeeTypeId);

                SqlCommand cmd = new SqlCommand("FeeCategoryPartTermMapListGetById", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeInstanceId", ProgrammeInstanceId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                cmd.Parameters.AddWithValue("@FeeTypeId", FeeTypeId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<FeeCategoryPartTermMap> ObjLstFCPT = new List<FeeCategoryPartTermMap>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        FeeCategoryPartTermMap objFCPT = new FeeCategoryPartTermMap();

                        objFCPT.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objFCPT.FeeCategoryId = Convert.ToInt32(Dt.Rows[i]["FeeCategoryId"]);
                        objFCPT.FeeCategoryName = Convert.ToString(Dt.Rows[i]["FeeCategoryName"]);
                        objFCPT.FeeCategoryCode = Convert.ToString(Dt.Rows[i]["FeeCategoryCode"]);
                        objFCPT.FeeTypeId = Convert.ToInt32(Dt.Rows[i]["FeeTypeId"]);
                        objFCPT.FeeTypeName = Convert.ToString(Dt.Rows[i]["FeeTypeName"]);
                        objFCPT.FeeTypeCode = Convert.ToString(Dt.Rows[i]["FeeTypeCode"]);
                        objFCPT.ProgrammeInstancePartTermId = Convert.ToInt64(Dt.Rows[i]["ProgrammeInstancePartTermId"]);
                        objFCPT.PartTermName = Convert.ToString(Dt.Rows[i]["PartTermName"]);
                        objFCPT.PartTermShortName = Convert.ToString(Dt.Rows[i]["PartTermShortName"]);
                        objFCPT.ProgrammeInstancePartId = Convert.ToInt64(Dt.Rows[i]["ProgrammeInstancePartTermId"]);
                        objFCPT.PartName = Convert.ToString(Dt.Rows[i]["PartName"]);
                        objFCPT.PartShortName = Convert.ToString(Dt.Rows[i]["PartShortName"]);
                        objFCPT.SpecialisationId = Convert.ToInt32(Dt.Rows[i]["SpecialisationId"]);
                        objFCPT.BranchName = Convert.ToString(Dt.Rows[i]["BranchName"]);
                        objFCPT.ProgrammeInstanceId = Convert.ToInt64(Dt.Rows[i]["ProgrammeInstanceId"]);
                        objFCPT.InstanceName = Convert.ToString(Dt.Rows[i]["InstanceName"]);
                        objFCPT.ProgrammeId = Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);
                        objFCPT.ProgrammeName = Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        objFCPT.FacultyId = Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        objFCPT.FacultyName = Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objFCPT.AcademicYearId = Convert.ToInt32(Dt.Rows[i]["AcademicYearId"]);
                        objFCPT.AcademicYearCode = Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);
                        var IsVerified = Dt.Rows[i]["IsVerified"];
                        if (IsVerified is DBNull)
                        {
                            IsVerified = false;
                            objFCPT.IsVerified = Convert.ToBoolean(IsVerified);
                        }
                        else
                        {
                            objFCPT.IsVerified = Convert.ToBoolean(Dt.Rows[i]["IsVerified"]);
                        }
                        var IsPublished = Dt.Rows[i]["IsPublished"];
                        if (IsPublished is DBNull)
                        {
                            IsPublished = false;
                            objFCPT.IsPublished = Convert.ToBoolean(IsPublished);
                        }
                        else
                        {
                            objFCPT.IsPublished = Convert.ToBoolean(Dt.Rows[i]["IsPublished"]);
                        }

                        ObjLstFCPT.Add(objFCPT);
                    }
                    return Return.returnHttp("200", ObjLstFCPT, null);
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

        #region MstFeeSubHeadGetbyFHId
        [HttpPost]
        public HttpResponseMessage MstFeeSubHeadGetbyFHId(FeeConfiguration FEECONFIG)
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
                Int32 FeeTypeId = Convert.ToInt32(FEECONFIG.FeeTypeId);
                Int32 AcademicYearId = Convert.ToInt32(FEECONFIG.AcademicYearId);
                List<FeeTypeFeeHeadMap> LstFTFHM = new List<FeeTypeFeeHeadMap>();
                SqlCommand Cmd = new SqlCommand("FeeTypeFeeHeadMapGetByFeeTypeId", Con);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@FeeTypeId", FeeTypeId);
                Cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                Da.SelectCommand = Cmd;
                Da.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        FeeTypeFeeHeadMap objFTFHM = new FeeTypeFeeHeadMap();
                        objFTFHM.AcademicYearCode = (Convert.ToString(Dt.Rows[i]["AcademicYearCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);
                        objFTFHM.FeeTypeName = (Convert.ToString(Dt.Rows[i]["FeeTypeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FeeTypeName"]);
                        objFTFHM.FeeSubHeadName = (Convert.ToString(Dt.Rows[i]["FeeSubHeadName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FeeSubHeadName"]);
                        objFTFHM.FeeHeadName = (Convert.ToString(Dt.Rows[i]["FeeHeadName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FeeHeadName"]);
                        objFTFHM.Id = (((Dt.Rows[i]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objFTFHM.AcademicYearId = (((Dt.Rows[i]["AcademicYearId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["AcademicYearId"]);
                        objFTFHM.FeeTypeId = (((Dt.Rows[i]["FeeTypeId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["FeeTypeId"]);
                        objFTFHM.FeeSubHeadSrno = (((Dt.Rows[i]["FeeSubHeadSrno"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["FeeSubHeadSrno"]);
                        objFTFHM.FeeHeadId = (((Dt.Rows[i]["FeeHeadId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["FeeHeadId"]);
                        objFTFHM.FeeSubHeadId = (((Dt.Rows[i]["FeeSubHeadId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["FeeSubHeadId"]);
                        objFTFHM.IsEditable = (Convert.ToString(Dt.Rows[i]["IsEditable"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsEditable"]);
                        objFTFHM.AllowsNegativeValue = (Convert.ToString(Dt.Rows[i]["AllowsNegativeValue"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["AllowsNegativeValue"]);

                        LstFTFHM.Add(objFTFHM);
                    }
                }


                return Return.returnHttp("200", LstFTFHM, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        //Verify and Publish Fee Code Start

        #region FeeConfigurationIsVerifiedEnable
        [HttpPost]
        public HttpResponseMessage FeeConfigurationIsVerifiedEnable(FeeConfiguration FEECONFIG)
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
                Int64 UserId = Convert.ToInt64(res.ToString());
                SqlCommand CMDarray = new SqlCommand();
                SqlCommand CMDarray1 = new SqlCommand();

                Int32 AcademicYearId = Convert.ToInt32(FEECONFIG.AcademicYearId);
                Int64 ProgrammeInstancePartTermId = Convert.ToInt64(FEECONFIG.ProgrammeInstancePartTermId);
                Int32 FeeTypeId = Convert.ToInt32(FEECONFIG.FeeTypeId);

                SqlCommand cmd = new SqlCommand("FeeCategoryPartTermMapVerify", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                cmd.Parameters.AddWithValue("@FeeTypeId", FeeTypeId);
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Flag", "FeeCategoryPartTermMapIsVerifiedEnable");
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                Con.Close();
                //return Return.returnHttp("200", strMessage.ToString(), null);
                return Return.returnHttp("200", "Data Verified", null);


            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
            finally
            {
                Con.Close();
            }
        }
        #endregion

        #region FeeConfigurationIsVerifiedDisable
        [HttpPost]
        public HttpResponseMessage FeeConfigurationIsVerifiedDisable(FeeConfiguration FEECONFIG)
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
                Int64 UserId = Convert.ToInt64(res.ToString());
                SqlCommand CMDarray = new SqlCommand();
                SqlCommand CMDarray1 = new SqlCommand();

                Int32 AcademicYearId = Convert.ToInt32(FEECONFIG.AcademicYearId);
                Int64 ProgrammeInstancePartTermId = Convert.ToInt64(FEECONFIG.ProgrammeInstancePartTermId);
                Int32 FeeTypeId = Convert.ToInt32(FEECONFIG.FeeTypeId);

                SqlCommand cmd = new SqlCommand("FeeCategoryPartTermMapVerify", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                cmd.Parameters.AddWithValue("@FeeTypeId", FeeTypeId);
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Flag", "FeeCategoryPartTermMapIsVerifiedDisable");
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                Con.Close();
                //return Return.returnHttp("200", strMessage.ToString(), null);
                return Return.returnHttp("200", "Data UnVerified", null);


            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
            finally
            {
                Con.Close();
            }
        }
        #endregion

        #region FeeCategoryPartTermMapIsPublishedEnable
        [HttpPost]
        public HttpResponseMessage FeeCategoryPartTermMapIsPublishedEnable(FeeCategoryPartTermMap FEECONFIG)
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
                Int64 UserId = Convert.ToInt64(res.ToString());
                //Int64 Id = Convert.ToInt64(FEECONFIG.Id);
                Int32 AcademicYearId = Convert.ToInt32(FEECONFIG.AcademicYearId);
                Int32 FeeTypeId = Convert.ToInt32(FEECONFIG.FeeTypeId);
                Int64 ProgrammeInstancePartTermId = Convert.ToInt64(FEECONFIG.ProgrammeInstancePartTermId);
                SqlCommand CMDarray = new SqlCommand();
                SqlCommand CMDarray1 = new SqlCommand();
                String msg = "";
                String msg1 = "";

                CMDarray = new SqlCommand("FeeCategoryPartTermMapPublish", Con, ST);
                CMDarray.CommandType = CommandType.StoredProcedure;
                CMDarray.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                CMDarray.Parameters.AddWithValue("@FeeTypeId", FeeTypeId);
                CMDarray.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                CMDarray.Parameters.AddWithValue("@Flag", "FeeCategoryPartTermMapIsPublishedEnable");
                CMDarray.Parameters.AddWithValue("@UserId", UserId);
                CMDarray.Parameters.AddWithValue("@UserTime", datetime);
                CMDarray.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                CMDarray.Parameters["@Message"].Direction = ParameterDirection.Output;

                CMDarray1 = new SqlCommand("FeeConfigurationPublish", Con, ST);
                CMDarray1.CommandType = CommandType.StoredProcedure;
                CMDarray1.Parameters.AddWithValue("@FeeTypeId", FeeTypeId);
                CMDarray1.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                CMDarray1.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                CMDarray1.Parameters.AddWithValue("@Flag", "FeeConfigurationIsPublishedEnable");
                CMDarray1.Parameters.AddWithValue("@UserId", UserId);
                CMDarray1.Parameters.AddWithValue("@UserTime", datetime);
                CMDarray1.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                CMDarray1.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                ST = Con.BeginTransaction();
                try
                {

                    CMDarray.Transaction = ST;
                    int ans = CMDarray.ExecuteNonQuery();
                    msg = Convert.ToString(CMDarray.Parameters["@Message"].Value);
                    CMDarray1.Transaction = ST;
                    int ans1 = CMDarray1.ExecuteNonQuery();
                    msg1 = Convert.ToString(CMDarray1.Parameters["@Message"].Value);

                    ST.Commit();
                    //return Return.returnHttp("200", msg + "  " + msg1, null);
                    return Return.returnHttp("200", "Fee Published", null);
                }
                catch (SqlException sqlError)
                {
                    ST.Rollback();
                    String strMessageErr = Convert.ToString(sqlError);
                    //return Return.returnHttp("201", strMessageErr, null);
                    return Return.returnHttp("201", "Record already Published", null);
                }


            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
            finally
            {
                Con.Close();
            }
        }
        #endregion

        #region FeeCategoryPartTermMapIsPublishedDisable
        [HttpPost]
        public HttpResponseMessage FeeCategoryPartTermMapIsPublishedDisable(FeeCategoryPartTermMap FEECONFIG)
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
                Int64 UserId = Convert.ToInt64(res.ToString());
                //Int64 Id = Convert.ToInt64(FEECONFIG.Id);
                Int32 AcademicYearId = Convert.ToInt32(FEECONFIG.AcademicYearId);
                Int32 FeeTypeId = Convert.ToInt32(FEECONFIG.FeeTypeId);
                Int64 ProgrammeInstancePartTermId = Convert.ToInt64(FEECONFIG.ProgrammeInstancePartTermId);
                SqlCommand CMD = new SqlCommand();
                SqlCommand CMD1 = new SqlCommand();
                SqlCommand CMD2 = new SqlCommand();
                String msg = "";
                String msg1 = "";
                String msg2 = "";

                CMD = new SqlCommand("FeeCategoryPartTermMapPublish", Con, ST);
                CMD.CommandType = CommandType.StoredProcedure;
                CMD.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                CMD.Parameters.AddWithValue("@FeeTypeId", FeeTypeId);
                CMD.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                CMD.Parameters.AddWithValue("@Flag", "FeeCategoryPartTermMapIsPublishedDisable");
                CMD.Parameters.AddWithValue("@UserId", UserId);
                CMD.Parameters.AddWithValue("@UserTime", datetime);
                CMD.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                CMD.Parameters["@Message"].Direction = ParameterDirection.Output;

                CMD1 = new SqlCommand("FeeConfigurationPublish", Con, ST);
                CMD1.CommandType = CommandType.StoredProcedure;
                CMD1.Parameters.AddWithValue("@FeeTypeId", FeeTypeId);
                CMD1.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                CMD1.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                CMD1.Parameters.AddWithValue("@Flag", "FeeConfigurationIsPublishedDisable");
                CMD1.Parameters.AddWithValue("@UserId", UserId);
                CMD1.Parameters.AddWithValue("@UserTime", datetime);
                CMD1.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                CMD1.Parameters["@Message"].Direction = ParameterDirection.Output;

                CMD2 = new SqlCommand("FeeUnpublishtagTrue", Con, ST);
                CMD2.CommandType = CommandType.StoredProcedure;
                CMD2.Parameters.AddWithValue("@FeeTypeId", FeeTypeId);
                CMD2.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                CMD2.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                CMD2.Parameters.AddWithValue("@Flag", "FeeConfigurationIsPublishedDisable");
                CMD2.Parameters.AddWithValue("@UserId", UserId);
                CMD2.Parameters.AddWithValue("@UserTime", datetime);
                CMD2.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                CMD2.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                ST = Con.BeginTransaction();
                try
                {

                    CMD.Transaction = ST;
                    int ans = CMD.ExecuteNonQuery();
                    msg = Convert.ToString(CMD.Parameters["@Message"].Value);
                    CMD1.Transaction = ST;
                    int ans1 = CMD1.ExecuteNonQuery();
                    msg1 = Convert.ToString(CMD1.Parameters["@Message"].Value);
                    CMD2.Transaction = ST;
                    int ans2 = CMD2.ExecuteNonQuery();
                    msg2 = Convert.ToString(CMD2.Parameters["@Message"].Value);


                    ST.Commit();
                    //return Return.returnHttp("200", msg +"== " +msg1+"++" + msg2, null);
                    return Return.returnHttp("200", "Fee UnPublished", null);
                }
                catch (SqlException sqlError)
                {
                    ST.Rollback();
                    String strMessageErr = Convert.ToString(sqlError);
                    //return Return.returnHttp("201", strMessageErr, null);
                    return Return.returnHttp("201", "Record already UnPublished", null);
                }


            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
            finally
            {
                Con.Close();
            }
        }
        #endregion       

        #region FeeConfigurationIsVerifyallEnable
        [HttpPost]
        public HttpResponseMessage FeeConfigurationIsVerifyallEnable(FeeConfiguration FEECONFIG)
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
                Int64 UserId = Convert.ToInt64(res.ToString());
                SqlCommand CMDarray = new SqlCommand();
                SqlCommand CMDarray1 = new SqlCommand();

                Int32 FeeTypeId = Convert.ToInt32(FEECONFIG.FeeTypeId);
                Int32 AcademicYearId = Convert.ToInt32(FEECONFIG.AcademicYearId);
                Int32 FacultyId = Convert.ToInt32(FEECONFIG.FacultyId);

                SqlCommand cmd = new SqlCommand("FeeCategoryPartTermMapVerifyAll", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FeeTypeId", FeeTypeId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Flag", "FeeCategoryPartTermMapIsVerifiedEnable");
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                Con.Close();
                //return Return.returnHttp("200", strMessage.ToString(), null);
                return Return.returnHttp("200", "Data Verified", null);


            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
            finally
            {
                Con.Close();
            }
        }
        #endregion

        //Verify and Publish Fee Code End
        //Add Application fee Start

        #region ApplicationFeeGet
        [HttpPost]
        public HttpResponseMessage ApplicationFeeGet(FeeConfiguration FEECONFIG)
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

                Int32 ProgrammeInstancePartTermId = Convert.ToInt32(FEECONFIG.ProgrammeInstancePartTermId);

                SqlCommand cmd = new SqlCommand("MstApplicationFeeGetbyAYId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<MstApplicationFee> ObjLstAF = new List<MstApplicationFee>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstApplicationFee objAF = new MstApplicationFee();

                        objAF.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objAF.FeeTypeId = Convert.ToInt32(Dt.Rows[i]["FeeTypeId"]);
                        objAF.FeeCategoryId = Convert.ToInt32(Dt.Rows[i]["FeeCategoryId"]);
                        objAF.AcademicYearId = Convert.ToInt32(Dt.Rows[i]["AcademicYearId"]);
                        objAF.FeeCategoryName = Convert.ToString(Dt.Rows[i]["FeeCategoryName"]);
                        objAF.FeeTypeName = Convert.ToString(Dt.Rows[i]["FeeTypeName"]);
                        objAF.AcademicYearCode = Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);
                        objAF.Amount = Convert.ToDouble(Dt.Rows[i]["Amount"]);
                        objAF.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjLstAF.Add(objAF);
                    }
                }
                return Return.returnHttp("200", ObjLstAF, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region ApplicationFeeAdd
        [HttpPost]
        public HttpResponseMessage ApplicationFeeAdd(FeeConfiguration FEECONFIG)
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
                Int64 UserId = Convert.ToInt64(res.ToString());
                Int64 ProgrammeInstancePartTermId = Convert.ToInt64(FEECONFIG.ProgrammeInstancePartTermId);
                Int32 FeeTypeId = Convert.ToInt32(FEECONFIG.FeeTypeId);
                Int32 NoOfInstalment = 0;
                Int32 FeeCategoryId = Convert.ToInt32(FEECONFIG.FeeCategoryId);
                Int32 AcademicYearId = Convert.ToInt32(FEECONFIG.AcademicYearId);
                Boolean IsInstalmentAllowed = false;
                Boolean IsInstallmentGiven = false;
                Double FeeAmount = Convert.ToDouble(FEECONFIG.Amount);
                List<String> Sample = new List<String>();
                Int32 Count = Convert.ToInt32(FEECONFIG.Count);
                Int32 CountCategory = Convert.ToInt32(FEECONFIG.countTotal);
                SqlCommand CMDarray = new SqlCommand();
                SqlCommand CMDarray1 = new SqlCommand();
                String msg = "";
                String msg1 = "";


                CMDarray = new SqlCommand("FeeConfigurationAdd", Con, ST);
                CMDarray.CommandType = CommandType.StoredProcedure;
                CMDarray.Parameters.AddWithValue("@IsInstalmentAllowed", IsInstalmentAllowed);
                CMDarray.Parameters.AddWithValue("@FeeTypeId", FeeTypeId);
                CMDarray.Parameters.AddWithValue("@FeeCategoryId", FeeCategoryId);
                CMDarray.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                CMDarray.Parameters.AddWithValue("@FeeHeadId", 1);
                CMDarray.Parameters.AddWithValue("@FeeSubHeadId", 1);
                CMDarray.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                CMDarray.Parameters.AddWithValue("@Amount", FeeAmount);
                CMDarray.Parameters.AddWithValue("@UserId", UserId);
                CMDarray.Parameters.AddWithValue("@UserTime", datetime);
                CMDarray.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                CMDarray.Parameters["@Message"].Direction = ParameterDirection.Output;

                CMDarray1 = new SqlCommand("FeeCategoryPartTermMapAdd", Con, ST);
                CMDarray1.CommandType = CommandType.StoredProcedure;
                CMDarray1.Parameters.AddWithValue("@FeeTypeId", FeeTypeId);
                CMDarray1.Parameters.AddWithValue("@FeeCategoryId", FeeCategoryId);
                CMDarray1.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                CMDarray1.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                CMDarray1.Parameters.AddWithValue("@TotalAmount", FeeAmount);
                CMDarray1.Parameters.AddWithValue("@IsInstalmentGiven", IsInstallmentGiven);
                CMDarray1.Parameters.AddWithValue("@NumberofInstalment", NoOfInstalment);
                CMDarray1.Parameters.AddWithValue("@UserId", UserId);
                CMDarray1.Parameters.AddWithValue("@UserTime", datetime);
                CMDarray1.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                CMDarray1.Parameters["@Message"].Direction = ParameterDirection.Output;




                Con.Open();
                ST = Con.BeginTransaction();
                try
                {
                    CMDarray1.Transaction = ST;
                    int ans1 = CMDarray1.ExecuteNonQuery();
                    msg1 = msg1 + ", " + Convert.ToString(CMDarray.Parameters["@Message"].Value);
                    CMDarray.Transaction = ST;
                    int ans = CMDarray.ExecuteNonQuery();
                    msg = msg + ", " + Convert.ToString(CMDarray.Parameters["@Message"].Value);

                    ST.Commit();
                    return Return.returnHttp("200", "Fee Added", null);
                }
                catch (SqlException sqlError)
                {
                    ST.Rollback();
                    String strMessageErr = Convert.ToString(sqlError);
                    return Return.returnHttp("201", "Record already exists", null);
                }

                //return Return.returnHttp("200", "Data Added Successfully", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
            finally
            {
                Con.Close();
            }
        }
        #endregion

        //Add Application fee End
        //Copy Fee Code Start

        #region PIPTget
        [HttpPost]
        public HttpResponseMessage PIPTget(FeeConfiguration FEECONFIG)
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

                Int32 AcademicYearId = Convert.ToInt32(FEECONFIG.AcademicYearId);
                Int32 FacultyId = Convert.ToInt32(FEECONFIG.FacultyId);
                Int32 ProgrammeLevelId = Convert.ToInt32(FEECONFIG.ProgrammeLevelId);

                SqlCommand cmd = new SqlCommand("FeeCategoryPartTermMapGetonlyPT", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                cmd.Parameters.AddWithValue("@ProgrammeLevelId", ProgrammeLevelId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<FeeCategoryPartTermMap> ObjLstFCPTM = new List<FeeCategoryPartTermMap>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        FeeCategoryPartTermMap objFCPTM = new FeeCategoryPartTermMap();

                        objFCPTM.ProgrammeInstancePartTermId = Convert.ToInt64(Dt.Rows[i]["ProgrammeInstancePartTermId"]);
                        objFCPTM.AcademicYearId = Convert.ToInt32(Dt.Rows[i]["AcademicYearId"]);
                        objFCPTM.InstancePartTermName = Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                        ObjLstFCPTM.Add(objFCPTM);
                    }
                }
                return Return.returnHttp("200", ObjLstFCPTM, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region FeeGetIncPT
        [HttpPost]
        public HttpResponseMessage FeeGetIncPT(FeeConfiguration FEECONFIG)
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
                Int32 FeeTypeId = Convert.ToInt32(FEECONFIG.FeeTypeId);
                Int32 AcademicYearId = Convert.ToInt32(FEECONFIG.AcademicYearId);
                Int32 ProgrammeLevelId = Convert.ToInt32(FEECONFIG.ProgrammeLevelId);
                Int32 FacultyId = Convert.ToInt32(FEECONFIG.FacultyId);
                Int32 ProgrammeInstancePartTermId = Convert.ToInt32(FEECONFIG.ProgrammeInstancePartTermId);

                SqlCommand cmd = new SqlCommand("FeeGetIncPT", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FeeTypeId", FeeTypeId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                //cmd.Parameters.AddWithValue("@ProgrammeLevelId", ProgrammeLevelId);
                cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);


                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<FeeCategoryPartTermMap> ObjLstFCPTM = new List<FeeCategoryPartTermMap>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        FeeCategoryPartTermMap objFCPTM = new FeeCategoryPartTermMap();

                        objFCPTM.ProgrammeInstancePartTermId = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objFCPTM.InstancePartTermName = Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                        objFCPTM.FacultyName = Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objFCPTM.ProgrammeLevelName = Convert.ToString(Dt.Rows[i]["ProgrammeLevelName"]);
                        ObjLstFCPTM.Add(objFCPTM);
                    }
                }
                return Return.returnHttp("200", ObjLstFCPTM, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region FeeGetFTByPT
        [HttpPost]
        public HttpResponseMessage FeeGetFTByPT(FeeConfiguration FEECONFIG)
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

                Int32 ProgrammeInstancePartTermId = Convert.ToInt32(FEECONFIG.ProgrammeInstancePartTermId);

                SqlCommand cmd = new SqlCommand("FeeCategoryPartTermMapGetFTByPT", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<FeeCategoryPartTermMap> ObjLstFCPTM = new List<FeeCategoryPartTermMap>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        FeeCategoryPartTermMap objFCPTM = new FeeCategoryPartTermMap();

                        objFCPTM.FeeTypeId = Convert.ToInt32(Dt.Rows[i]["FeeTypeId"]);
                        objFCPTM.FeeTypeName = Convert.ToString(Dt.Rows[i]["FeeTypeName"]);
                        ObjLstFCPTM.Add(objFCPTM);
                    }
                }
                return Return.returnHttp("200", ObjLstFCPTM, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region FeeGetFCbyPTFT
        [HttpPost]
        public HttpResponseMessage FeeGetFCbyPTFT(FeeConfiguration FEECONFIG)
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

                Int64 ProgrammeInstancePartTermId = Convert.ToInt64(FEECONFIG.ProgrammeInstancePartTermId);
                Int32 FeeTypeId = Convert.ToInt32(FEECONFIG.FeeTypeId);

                SqlCommand cmd = new SqlCommand("FeeCategoryPartTermMapGetFCbyPTFT", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                cmd.Parameters.AddWithValue("@FeeTypeId", FeeTypeId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<FeeCategoryPartTermMap> ObjLstFCPTM = new List<FeeCategoryPartTermMap>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        FeeCategoryPartTermMap objFCPTM = new FeeCategoryPartTermMap();

                        objFCPTM.FeeCategoryId = Convert.ToInt32(Dt.Rows[i]["FeeCategoryId"]);
                        objFCPTM.FeeCategoryName = Convert.ToString(Dt.Rows[i]["FeeCategoryName"]);
                        ObjLstFCPTM.Add(objFCPTM);
                    }
                }
                return Return.returnHttp("200", ObjLstFCPTM, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region FeeCopy
        [HttpPost]
        public HttpResponseMessage FeeCopy(FeeConfiguration FEECONFIG)
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
                Int64 UserId = Convert.ToInt64(res.ToString());
                Int32 Count = 0;
                var FeeCateList = FEECONFIG.FeeCateList;
                var PIPTList = FEECONFIG.PIPTList;
                foreach (var FC in FeeCateList)
                {
                    if (FC.Value == "true")
                    {
                        foreach (var PIPT in PIPTList)
                        {
                            if (PIPT.Value == "true")
                            {
                                Count++;
                            }
                        }
                    }
                }


                SqlCommand[] CMDarray = new SqlCommand[Count];
                SqlCommand[] CMDarray1 = new SqlCommand[Count];
                String msg = "";
                String msg1 = "";
                int i = 0;


                Int64 ProgrammeInstancePartTermId = Convert.ToInt64(FEECONFIG.ProgrammeInstancePartTermId);
                Int32 FeeTypeId = Convert.ToInt32(FEECONFIG.FeeTypeId);
                //Int32 FeeCategoryId = Convert.ToInt32(FEECONFIG.FeeCategoryId);
                foreach (var FC in FeeCateList)
                {
                    if (FC.Value == "true")
                    {
                        Int32 FeeCategoryId = Convert.ToInt32(FC.Key);
                        foreach (var PIPT in PIPTList)
                        {
                            if (PIPT.Value == "true")
                            {
                                Int64 PIPTId = Convert.ToInt64(PIPT.Key);

                                CMDarray[i] = new SqlCommand("FeeCategoryPartTermMapCopy", Con, ST);
                                CMDarray[i].CommandType = CommandType.StoredProcedure;
                                CMDarray[i].Parameters.AddWithValue("@FeeTypeId", FeeTypeId);
                                CMDarray[i].Parameters.AddWithValue("@FeeCategoryId", FeeCategoryId);
                                CMDarray[i].Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                                CMDarray[i].Parameters.AddWithValue("@PIPTId", PIPTId);
                                CMDarray[i].Parameters.AddWithValue("@UserId", UserId);
                                CMDarray[i].Parameters.AddWithValue("@UserTime", datetime);
                                CMDarray[i].Parameters.Add("@MSG", SqlDbType.NVarChar, 500);
                                CMDarray[i].Parameters["@MSG"].Direction = ParameterDirection.Output;


                                CMDarray1[i] = new SqlCommand("FeeConfigurationCopy", Con, ST);
                                CMDarray1[i].CommandType = CommandType.StoredProcedure;
                                CMDarray1[i].Parameters.AddWithValue("@FeeTypeId", FeeTypeId);
                                CMDarray1[i].Parameters.AddWithValue("@FeeCategoryId", FeeCategoryId);
                                CMDarray1[i].Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                                CMDarray1[i].Parameters.AddWithValue("@PIPTId", PIPTId);
                                CMDarray1[i].Parameters.AddWithValue("@UserId", UserId);
                                CMDarray1[i].Parameters.AddWithValue("@UserTime", datetime);
                                CMDarray1[i].Parameters.Add("@MSG", SqlDbType.NVarChar, 500);
                                CMDarray1[i].Parameters["@MSG"].Direction = ParameterDirection.Output;
                                i++;
                            }
                        }
                    }
                }
                Con.Open();
                ST = Con.BeginTransaction();
                try
                {
                    for (int b = 0; b < i; b++)
                    {
                        CMDarray[b].Transaction = ST;
                        int ans1 = CMDarray[b].ExecuteNonQuery();
                        msg1 = msg1 + ", " + Convert.ToString(CMDarray[b].Parameters["@MSG"].Value);
                    }
                    for (int j = 0; j < i; j++)
                    {
                        CMDarray1[j].Transaction = ST;
                        int ans = CMDarray1[j].ExecuteNonQuery();
                        msg = msg + ", " + Convert.ToString(CMDarray1[j].Parameters["@MSG"].Value);
                    }

                    ST.Commit();
                    //return Return.returnHttp("200", msg1 + " - " + msg, null);
                    return Return.returnHttp("200", "Record Added", null);
                }
                catch (SqlException sqlError)
                {
                    ST.Rollback();
                    String strMessageErr = Convert.ToString(sqlError);
                    return Return.returnHttp("201", strMessageErr, null);
                    //return Return.returnHttp("201", "Record already exists", null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
            finally
            {
                Con.Close();
            }
        }
        #endregion

        //Copy Fee Code End
        //Gayatri Start Code

        #region FeeTypeReportPublishGet
        [HttpPost]
        public HttpResponseMessage FeeTypeReportPublishGet(FeeCategoryPartTermMap FTRP)
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

                Int32 FeeTypeId = Convert.ToInt32(FTRP.FeeTypeId);
                Int32 AcademicYearId = Convert.ToInt32(FTRP.AcademicYearId);

                SqlCommand Cmd = new SqlCommand("FeeTypeReportPublishGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                Cmd.Parameters.AddWithValue("@FeeTypeId", FeeTypeId);

                Da.SelectCommand = Cmd;

                Da.Fill(Dt);


                List<FeeCategoryPartTermMap> ObjLstFEETUP = new List<FeeCategoryPartTermMap>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        FeeCategoryPartTermMap objFEETUP = new FeeCategoryPartTermMap();

                        objFEETUP.AcademicYearCode = (Convert.ToString(Dt.Rows[i]["AcademicYearCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);
                        objFEETUP.FeeTypeName = (Convert.ToString(Dt.Rows[i]["FeeTypeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FeeTypeName"]);
                        objFEETUP.InstancePartTermName = (Convert.ToString(Dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                        objFEETUP.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objFEETUP.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        objFEETUP.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["BranchName"]);
                        objFEETUP.TotalAmount = (Convert.ToDouble(Dt.Rows[i]["TotalAmount"]));

                        objFEETUP.FeeCategoryName = (Convert.ToString(Dt.Rows[i]["FeeCategoryName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FeeCategoryName"]);



                        ObjLstFEETUP.Add(objFEETUP);
                    }
                    return Return.returnHttp("200", ObjLstFEETUP, null);
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

        #region FeeTypeReportUnPublishGet
        [HttpPost]
        public HttpResponseMessage FeeTypeReportUnPublishGet(FeeCategoryPartTermMap FTRU)
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

                Int32 FeeTypeId = Convert.ToInt32(FTRU.FeeTypeId);
                Int32 AcademicYearId = Convert.ToInt32(FTRU.AcademicYearId);

                SqlCommand Cmd = new SqlCommand("FeeTypeReportUnPublishGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                Cmd.Parameters.AddWithValue("@FeeTypeId", FeeTypeId);

                Da.SelectCommand = Cmd;

                Da.Fill(Dt);


                List<FeeCategoryPartTermMap> ObjLstFEETUP = new List<FeeCategoryPartTermMap>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        FeeCategoryPartTermMap objFEETUP = new FeeCategoryPartTermMap();

                        objFEETUP.AcademicYearCode = (Convert.ToString(Dt.Rows[i]["AcademicYearCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);
                        objFEETUP.FeeTypeName = (Convert.ToString(Dt.Rows[i]["FeeTypeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FeeTypeName"]);
                        objFEETUP.InstancePartTermName = (Convert.ToString(Dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                        objFEETUP.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objFEETUP.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        objFEETUP.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["BranchName"]);
                        objFEETUP.TotalAmount = (Convert.ToDouble(Dt.Rows[i]["TotalAmount"]));

                        objFEETUP.FeeCategoryName = (Convert.ToString(Dt.Rows[i]["FeeCategoryName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FeeCategoryName"]);



                        ObjLstFEETUP.Add(objFEETUP);
                    }
                    return Return.returnHttp("200", ObjLstFEETUP, null);
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

        #region ApplicationFeesPaid
        [HttpPost]
        public HttpResponseMessage ApplicationFeesPaid(AdmApplicationFeesPaid AFP)
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

                Int32 ProgrammeInstancePartTermId = Convert.ToInt32(AFP.ProgrammeInstancePartTermId);

                SqlCommand Cmd = new SqlCommand("ApplicationFeesPaid", Con);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);

                Da.SelectCommand = Cmd;

                Da.Fill(Dt);


                List<AdmApplicationFeesPaid> ObjLstAFP = new List<AdmApplicationFeesPaid>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        AdmApplicationFeesPaid objAFP = new AdmApplicationFeesPaid();

                        objAFP.AcademicYearCode = (Convert.ToString(Dt.Rows[i]["AcademicYearCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);


                        objAFP.NameAsPerMarksheet = (Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"]);
                        objAFP.AdmissionApplicationId = (Convert.ToInt64(Dt.Rows[i]["AdmissionApplicationId"]));
                        objAFP.InstancePartTermName = (Convert.ToString(Dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                        objAFP.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objAFP.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        objAFP.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["BranchName"]);
                        objAFP.FeesPaidInr = (Convert.ToDouble(Dt.Rows[i]["FeesPaidInr"]));
                        objAFP.PartName = (Convert.ToString(Dt.Rows[i]["PartName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["PartName"]);


                        ObjLstAFP.Add(objAFP);
                    }
                    return Return.returnHttp("200", ObjLstAFP, null);
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

        #region ApplicationFeesUnpaid
        [HttpPost]
        public HttpResponseMessage ApplicationFeesUnpaid(AdmApplicationFeesPaid AFP)
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

                Int32 ProgrammeInstancePartTermId = Convert.ToInt32(AFP.ProgrammeInstancePartTermId);

                SqlCommand Cmd = new SqlCommand("ApplicationFeesUnpaid", Con);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);

                Da.SelectCommand = Cmd;

                Da.Fill(Dt);


                List<AdmApplicationFeesPaid> ObjLstAFP = new List<AdmApplicationFeesPaid>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        AdmApplicationFeesPaid objAFP = new AdmApplicationFeesPaid();

                        objAFP.AcademicYearCode = (Convert.ToString(Dt.Rows[i]["AcademicYearCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);


                        objAFP.NameAsPerMarksheet = (Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"]);
                        objAFP.AdmissionApplicationId = (Convert.ToInt64(Dt.Rows[i]["AdmissionApplicationId"]));
                        objAFP.InstancePartTermName = (Convert.ToString(Dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                        objAFP.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objAFP.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        objAFP.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["BranchName"]);

                        objAFP.PartName = (Convert.ToString(Dt.Rows[i]["PartName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["PartName"]);


                        ObjLstAFP.Add(objAFP);
                    }
                    return Return.returnHttp("200", ObjLstAFP, null);
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

        #region AdmissionFeesReport
        [HttpPost]
        public HttpResponseMessage AdmissionFeesReport(AdmStudentAdmissionFeesPaid ADFP)
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

                Int32 ProgrammeInstancePartTermId = Convert.ToInt32(ADFP.ProgrammeInstancePartTermId);

                SqlCommand Cmd = new SqlCommand("AdmissionFeesReport", Con);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);

                Da.SelectCommand = Cmd;

                Da.Fill(Dt);


                List<AdmStudentAdmissionFeesPaid> ObjLstADFP = new List<AdmStudentAdmissionFeesPaid>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        AdmStudentAdmissionFeesPaid objADFP = new AdmStudentAdmissionFeesPaid();


                        objADFP.NameAsPerMarksheet = (Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"]);
                        objADFP.InstancePartTermName = (Convert.ToString(Dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                        objADFP.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objADFP.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        objADFP.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["BranchName"]);
                        //objADFP.OrderId = (((Dt.Rows[i]["OrderId"]).ToString()).IsEmpty()) ? "Not Defined" : Convert.ToString(Dt.Rows[i]["OrderId"]);

                        objADFP.OrderId = (Convert.ToString(Dt.Rows[i]["OrderId"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["OrderId"]);

                        objADFP.MobileNo = (Convert.ToString(Dt.Rows[i]["MobileNo"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["MobileNo"]);
                        objADFP.EmailId = (Convert.ToString(Dt.Rows[i]["EmailId"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["EmailId"]);
                        objADFP.IsInstalmentSelected = (Convert.ToString(Dt.Rows[i]["IsInstalmentSelected"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsInstalmentSelected"]);
                        objADFP.IsInstalmentSelectedSts = (Convert.ToString(Dt.Rows[i]["IsInstalmentSelected"])).IsEmpty() ? "NULL" : Convert.ToString(Dt.Rows[i]["IsInstalmentSelected"]);


                        objADFP.TotalInstalmentGiven = (((Dt.Rows[i]["TotalInstalmentGiven"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["TotalInstalmentGiven"]);
                        objADFP.TotalInstalmentSelected = (((Dt.Rows[i]["TotalInstalmentSelected"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["TotalInstalmentSelected"]);
                        objADFP.RemainingInstallment = (((Dt.Rows[i]["RemainingInstallment"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["RemainingInstallment"]);
                        objADFP.InstalmentNo = (((Dt.Rows[i]["InstalmentNo"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["InstalmentNo"]);
                        objADFP.TotalAmount = (((Dt.Rows[i]["TotalAmount"]).ToString()).IsEmpty()) ? 0 : Convert.ToDouble(Dt.Rows[i]["TotalAmount"]);
                        objADFP.AmountPaid = (((Dt.Rows[i]["AmountPaid"]).ToString()).IsEmpty()) ? 0 : Convert.ToDouble(Dt.Rows[i]["AmountPaid"]);

                        objADFP.FeeCategoryName = (Convert.ToString(Dt.Rows[i]["FeeCategoryName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FeeCategoryName"]);

                        ObjLstADFP.Add(objADFP);
                    }
                    return Return.returnHttp("200", ObjLstADFP, null);
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

        //Gayatri End Code
    }
}