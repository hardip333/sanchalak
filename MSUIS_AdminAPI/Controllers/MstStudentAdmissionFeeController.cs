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

namespace MSUISApi.Controllers
{
    public class MstStudentAdmissionFeeController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        SqlTransaction ST;
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region GetAdmissionFee
        [HttpPost]
        public HttpResponseMessage GetAdmissionFee(MstAdmissionFee MAF)
        {
            try
            {
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int64 ApplicationId = Convert.ToInt64(MAF.ApplicationId);
                SqlCommand cmd = new SqlCommand("AdmissionFeeGetbyApplicationId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ApplicationId", ApplicationId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<MstAdmissionFee> ObjLstMAF = new List<MstAdmissionFee>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstAdmissionFee objMAF = new MstAdmissionFee();

                        objMAF.Id = (((Dt.Rows[i]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objMAF.FeeTypeFeeCategoryMapId = (((Dt.Rows[i]["FeeTypeFeeCategoryMapId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["FeeTypeFeeCategoryMapId"]);
                        objMAF.ProgrammeInstancePartTermId = (((Dt.Rows[i]["ProgrammeInstancePartTermId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["ProgrammeInstancePartTermId"]);
                        objMAF.ApplicationId = (((Dt.Rows[i]["ApplicationId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["ApplicationId"]);
                        objMAF.PRN = (((Dt.Rows[i]["PRN"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["PRN"]);
                        //objMAF.StudentAcademicInformationId = (((Dt.Rows[i]["StudentAcademicInformationId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["StudentAcademicInformationId"]);
                        objMAF.FeeCategoryId = (((Dt.Rows[i]["FeeCategoryId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["FeeCategoryId"]);
                        objMAF.FeeTypeId = (((Dt.Rows[i]["FeeTypeId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["FeeTypeId"]);
                        objMAF.AcademicYearId = (((Dt.Rows[i]["AcademicYearId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["AcademicYearId"]);
                        objMAF.NumberofInstalment = (((Dt.Rows[i]["NumberofInstalment"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["NumberofInstalment"]);
                        objMAF.LastInstalmentNo = (((Dt.Rows[i]["LastInstalmentNo"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["LastInstalmentNo"]);
                        objMAF.FeeCategoryName = (Convert.ToString(Dt.Rows[i]["FeeCategoryName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FeeCategoryName"]);
                        objMAF.FeeTypeName = (Convert.ToString(Dt.Rows[i]["FeeTypeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FeeTypeName"]);
                        objMAF.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["BranchName"]);
                        objMAF.InstituteName = (Convert.ToString(Dt.Rows[i]["InstituteName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstituteName"]);
                        objMAF.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        objMAF.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objMAF.InstancePartTermName = (Convert.ToString(Dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                        objMAF.NameAsPerMarksheet = (Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"]);
                        objMAF.AcademicYearCode = (Convert.ToString(Dt.Rows[i]["AcademicYearCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);
                        objMAF.IsInstalmentGiven = (Convert.ToString(Dt.Rows[i]["IsInstalmentGiven"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsInstalmentGiven"]);
                        objMAF.TotalAmount = (((Dt.Rows[i]["TotalAmount"]).ToString()).IsEmpty()) ? 0 : Convert.ToDouble(Dt.Rows[i]["TotalAmount"]);
                        objMAF.InstalmentCount = (((Dt.Rows[i]["InstalmentCount"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["InstalmentCount"]);
                        objMAF.IsICCRApplicant = (Convert.ToString(Dt.Rows[i]["IsICCRApplicant"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsICCRApplicant"]);
                        ObjLstMAF.Add(objMAF);
                    }
                }
                SqlCommand Cmd = new SqlCommand("AdmissionFeeDetailGetbyApplicationId", Con);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@ApplicationId", ApplicationId);
                SqlDataAdapter Da1 = new SqlDataAdapter();
                DataTable Dt1 = new DataTable();

                Da1.SelectCommand = Cmd;
                Da1.Fill(Dt1);

                List<MstAdmissionFeeDetail> ObjLstMAFD = new List<MstAdmissionFeeDetail>();

                if (Dt1.Rows.Count > 0)
                {
                    for (int j = 0; j < Dt1.Rows.Count; j++)
                    {
                        MstAdmissionFeeDetail objMAFD = new MstAdmissionFeeDetail();

                        objMAFD.Id = (((Dt1.Rows[j]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt1.Rows[j]["Id"]);
                        objMAFD.FeeTypeFeeHeadMapId = (((Dt1.Rows[j]["FeeTypeFeeHeadMapId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt1.Rows[j]["FeeTypeFeeHeadMapId"]);
                        objMAFD.FeeHeadId = (((Dt1.Rows[j]["FeeHeadId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt1.Rows[j]["FeeHeadId"]);
                        objMAFD.FeeSubHeadId = (((Dt1.Rows[j]["FeeSubHeadId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt1.Rows[j]["FeeSubHeadId"]);
                        objMAFD.FeeSubHeadName = (Convert.ToString(Dt1.Rows[j]["FeeSubHeadName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt1.Rows[j]["FeeSubHeadName"]);
                        objMAFD.FeeHeadName = (Convert.ToString(Dt1.Rows[j]["FeeHeadName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt1.Rows[j]["FeeHeadName"]);
                        objMAFD.IsInstalmentAllowed = (Convert.ToString(Dt1.Rows[j]["IsInstalmentAllowed"])).IsEmpty() ? false : Convert.ToBoolean(Dt1.Rows[j]["IsInstalmentAllowed"]);
                        objMAFD.Amount = (((Dt1.Rows[j]["Amount"]).ToString()).IsEmpty()) ? 0 : Convert.ToDouble(Dt1.Rows[j]["Amount"]);
                        objMAFD.TotalFSHAmountPaid = (((Dt1.Rows[j]["TotalFSHAmountPaid"]).ToString()).IsEmpty()) ? 0 : Convert.ToDouble(Dt1.Rows[j]["TotalFSHAmountPaid"]);
                        objMAFD.NumberofInstalment = (((Dt1.Rows[j]["NumberofInstalment"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt1.Rows[j]["NumberofInstalment"]);
                        objMAFD.IsInstalmentGiven = (Convert.ToString(Dt1.Rows[j]["IsInstalmentGiven"])).IsEmpty() ? false : Convert.ToBoolean(Dt1.Rows[j]["IsInstalmentGiven"]);
                        ObjLstMAFD.Add(objMAFD);
                    }
                }
                ObjLstMAF[0].DetailFee = ObjLstMAFD;

                //
                SqlCommand Cmd1 = new SqlCommand("AdmissionFeePaidDetailGetbyApplicationId", Con);
                Cmd1.CommandType = CommandType.StoredProcedure;

                Cmd1.Parameters.AddWithValue("@ApplicationId", ApplicationId);
                SqlDataAdapter Da2 = new SqlDataAdapter();
                DataTable Dt2 = new DataTable();

                Da2.SelectCommand = Cmd1;
                Da2.Fill(Dt2);

                List<StudentPaidFeeDetail> ObjLstSPFD = new List<StudentPaidFeeDetail>();

                if (Dt2.Rows.Count > 0)
                {
                    for (int j = 0; j < Dt2.Rows.Count; j++)
                    {
                        StudentPaidFeeDetail objSPFD = new StudentPaidFeeDetail();

                        objSPFD.Id = (((Dt2.Rows[j]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt2.Rows[j]["Id"]);
                        objSPFD.PRN = (((Dt2.Rows[j]["PRN"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt2.Rows[j]["PRN"]);
                        objSPFD.ProgrammeInstancePartTermId = (((Dt2.Rows[j]["ProgrammeInstancePartTermId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt2.Rows[j]["ProgrammeInstancePartTermId"]);
                        objSPFD.ApplicationId = (((Dt2.Rows[j]["ApplicationId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt2.Rows[j]["ApplicationId"]);
                        objSPFD.FeeCategoryPartTermMapId = (((Dt2.Rows[j]["FeeCategoryPartTermMapId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt2.Rows[j]["FeeCategoryPartTermMapId"]);
                        objSPFD.FeeTypeFeeCategoryMapId = (((Dt2.Rows[j]["FeeTypeFeeCategoryMapId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt2.Rows[j]["FeeTypeFeeCategoryMapId"]);
                        objSPFD.TotalInstalmentGiven = (((Dt2.Rows[j]["TotalInstalmentGiven"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt2.Rows[j]["TotalInstalmentGiven"]);
                        objSPFD.TotalInstalmentSelected = (((Dt2.Rows[j]["TotalInstalmentSelected"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt2.Rows[j]["TotalInstalmentSelected"]);
                        objSPFD.RemainingInstallment = (((Dt2.Rows[j]["RemainingInstallment"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt2.Rows[j]["RemainingInstallment"]);
                        objSPFD.InstalmentNo = (((Dt2.Rows[j]["InstalmentNo"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt2.Rows[j]["InstalmentNo"]);
                        objSPFD.IsInstalmentSelected = (Convert.ToString(Dt2.Rows[j]["IsInstalmentSelected"])).IsEmpty() ? false : Convert.ToBoolean(Dt2.Rows[j]["IsInstalmentSelected"]);
                        objSPFD.IsFullyPaid = (Convert.ToString(Dt2.Rows[j]["IsFullyPaid"])).IsEmpty() ? false : Convert.ToBoolean(Dt2.Rows[j]["IsFullyPaid"]);
                        objSPFD.AmountPaid = (((Dt2.Rows[j]["AmountPaid"]).ToString()).IsEmpty()) ? 0 : Convert.ToDouble(Dt2.Rows[j]["AmountPaid"]);
                        ObjLstSPFD.Add(objSPFD);
                    }
                }
                ObjLstMAF[0].PaidFee = ObjLstSPFD;
                ObjLstMAF[0].TotalAmountPaid = 0;
                foreach (StudentPaidFeeDetail SPFD in ObjLstMAF[0].PaidFee)
                {
                    if(ObjLstMAF[0].LastInstalmentNo == SPFD.InstalmentNo)
                    {
                        ObjLstMAF[0].TotalInstalmentSelected = SPFD.TotalInstalmentSelected;
                        ObjLstMAF[0].RemainingInstallment = SPFD.RemainingInstallment;
                        ObjLstMAF[0].IsInstalmentSelected = SPFD.IsInstalmentSelected;
                        ObjLstMAF[0].IsFullyPaid = SPFD.IsFullyPaid;
                    }
                    ObjLstMAF[0].TotalAmountPaid = ObjLstMAF[0].TotalAmountPaid + SPFD.AmountPaid;
                }
                return Return.returnHttp("200", ObjLstMAF, null);
            }
            catch (Exception e)
            {
                if (e.Message.ToString() == "The given header was not found.")
                {
                    return Return.returnHttp("0", "Session is expired, Kindly login again.", null);
                }
                else
                {
                    return Return.returnHttp("201", e.Message.ToString(), null);
                }
            }

        }
        #endregion

        #region PayAdmissionFees
        [HttpPost]
        public HttpResponseMessage PayAdmissionFees(MstAdmissionFee MAF)
        {
            try
            {
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int64 UserId = Convert.ToInt64(responseToken.ToString());
                //string strMessage = null;
                Int64 ApplicationId = Convert.ToInt64(MAF.ApplicationId);
                Int64 PRN = Convert.ToInt64(MAF.PRN);
                Int32 NumberofInstalment = Convert.ToInt32(MAF.NumberofInstalment);
                Int32 InstalmentNo = Convert.ToInt32(MAF.InstalmentNo);
                Double TotalAmountPaid = Convert.ToDouble(MAF.TotalAmountPaid);
                Boolean IsInstalmentGiven = Convert.ToBoolean(MAF.IsInstalmentGiven);
                Boolean UserSelection = Convert.ToBoolean(MAF.UserSelection);
                Boolean IsDeclarationRead = Convert.ToBoolean(MAF.IsDeclarationRead);
                Boolean IsFinalConfirm = Convert.ToBoolean(MAF.IsFinalConfirm);

                SqlCommand cmd = new SqlCommand("AdmissionFeeGetbyApplicationId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ApplicationId", ApplicationId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<MstAdmissionFee> ObjFee = new List<MstAdmissionFee>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstAdmissionFee Fee = new MstAdmissionFee();

                        Fee.Id = (((Dt.Rows[i]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["Id"]);
                        Fee.FeeTypeFeeCategoryMapId = (((Dt.Rows[i]["FeeTypeFeeCategoryMapId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["FeeTypeFeeCategoryMapId"]);
                        Fee.ProgrammeInstancePartTermId = (((Dt.Rows[i]["ProgrammeInstancePartTermId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["ProgrammeInstancePartTermId"]);
                        Fee.ApplicationId = (((Dt.Rows[i]["ApplicationId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["ApplicationId"]);
                        Fee.PRN = (((Dt.Rows[i]["PRN"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["PRN"]);
                        //Fee.StudentAcademicInformationId = (((Dt.Rows[i]["StudentAcademicInformationId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["StudentAcademicInformationId"]);
                        Fee.FeeCategoryId = (((Dt.Rows[i]["FeeCategoryId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["FeeCategoryId"]);
                        Fee.FeeTypeId = (((Dt.Rows[i]["FeeTypeId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["FeeTypeId"]);
                        Fee.AcademicYearId = (((Dt.Rows[i]["AcademicYearId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["AcademicYearId"]);
                        Fee.NumberofInstalment = (((Dt.Rows[i]["NumberofInstalment"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["NumberofInstalment"]);
                        Fee.LastInstalmentNo = (((Dt.Rows[i]["LastInstalmentNo"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["LastInstalmentNo"]);
                        Fee.FeeCategoryName = (Convert.ToString(Dt.Rows[i]["FeeCategoryName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FeeCategoryName"]);
                        Fee.FeeTypeName = (Convert.ToString(Dt.Rows[i]["FeeTypeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FeeTypeName"]);
                        Fee.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["BranchName"]);
                        Fee.InstituteName = (Convert.ToString(Dt.Rows[i]["InstituteName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstituteName"]);
                        Fee.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        Fee.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        Fee.InstancePartTermName = (Convert.ToString(Dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                        Fee.AcademicYearCode = (Convert.ToString(Dt.Rows[i]["AcademicYearCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);
                        Fee.IsInstalmentGiven = (Convert.ToString(Dt.Rows[i]["IsInstalmentGiven"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsInstalmentGiven"]);
                        Fee.TotalAmount = (((Dt.Rows[i]["TotalAmount"]).ToString()).IsEmpty()) ? 0 : Convert.ToDouble(Dt.Rows[i]["TotalAmount"]);
                        ObjFee.Add(Fee);
                    }
                }
                MstAdmissionFee Fee1 = ObjFee[0];
                SqlCommand Cmd = new SqlCommand("AdmissionFeeDetailGetbyApplicationId", Con);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@ApplicationId", ApplicationId);
                SqlDataAdapter Da1 = new SqlDataAdapter();
                DataTable Dt1 = new DataTable();

                Da1.SelectCommand = Cmd;
                Da1.Fill(Dt1);

                List<MstAdmissionFeeDetail> FeeDetailLst = new List<MstAdmissionFeeDetail>();

                if (Dt1.Rows.Count > 0)
                {
                    for (int j = 0; j < Dt1.Rows.Count; j++)
                    {
                        MstAdmissionFeeDetail FD = new MstAdmissionFeeDetail();

                        FD.Id = (((Dt1.Rows[j]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt1.Rows[j]["Id"]);
                        FD.FeeTypeFeeHeadMapId = (((Dt1.Rows[j]["FeeTypeFeeHeadMapId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt1.Rows[j]["FeeTypeFeeHeadMapId"]);
                        FD.FeeHeadId = (((Dt1.Rows[j]["FeeHeadId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt1.Rows[j]["FeeHeadId"]);
                        FD.FeeSubHeadId = (((Dt1.Rows[j]["FeeSubHeadId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt1.Rows[j]["FeeSubHeadId"]);
                        FD.FeeSubHeadName = (Convert.ToString(Dt1.Rows[j]["FeeSubHeadName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt1.Rows[j]["FeeSubHeadName"]);
                        FD.FeeHeadName = (Convert.ToString(Dt1.Rows[j]["FeeHeadName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt1.Rows[j]["FeeHeadName"]);
                        FD.IsInstalmentAllowed = (Convert.ToString(Dt1.Rows[j]["IsInstalmentAllowed"])).IsEmpty() ? false : Convert.ToBoolean(Dt1.Rows[j]["IsInstalmentAllowed"]);
                        FD.Amount = (((Dt1.Rows[j]["Amount"]).ToString()).IsEmpty()) ? 0 : Convert.ToDouble(Dt1.Rows[j]["Amount"]);
                        FD.TotalFSHAmountPaid = (((Dt1.Rows[j]["TotalFSHAmountPaid"]).ToString()).IsEmpty()) ? 0 : Convert.ToDouble(Dt1.Rows[j]["TotalFSHAmountPaid"]);
                        FD.NumberofInstalment = (((Dt1.Rows[j]["NumberofInstalment"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt1.Rows[j]["NumberofInstalment"]);
                        FD.IsInstalmentGiven = (Convert.ToString(Dt1.Rows[j]["IsInstalmentGiven"])).IsEmpty() ? false : Convert.ToBoolean(Dt1.Rows[j]["IsInstalmentGiven"]);
                        FeeDetailLst.Add(FD);
                    }
                }
                Fee1.DetailFee = FeeDetailLst;
                SqlCommand Cmd1 = new SqlCommand("AdmissionFeePaidDetailGetbyApplicationId", Con);
                SqlDataAdapter Da2 = new SqlDataAdapter();
                DataTable Dt2 = new DataTable();
                Cmd1.CommandType = CommandType.StoredProcedure;

                Cmd1.Parameters.AddWithValue("@ApplicationId", ApplicationId);

                Da2.SelectCommand = Cmd1;
                Da2.Fill(Dt2);

                List<StudentPaidFeeDetail> ObjLstSPFD = new List<StudentPaidFeeDetail>();

                if (Dt2.Rows.Count > 0)
                {
                    for (int j = 0; j < Dt2.Rows.Count; j++)
                    {
                        StudentPaidFeeDetail SPFD = new StudentPaidFeeDetail();

                        SPFD.Id = (((Dt2.Rows[j]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt2.Rows[j]["Id"]);
                        SPFD.PRN = (((Dt2.Rows[j]["PRN"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt2.Rows[j]["PRN"]);
                        SPFD.ProgrammeInstancePartTermId = (((Dt2.Rows[j]["ProgrammeInstancePartTermId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt2.Rows[j]["ProgrammeInstancePartTermId"]);
                        SPFD.ApplicationId = (((Dt2.Rows[j]["ApplicationId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt2.Rows[j]["ApplicationId"]);
                        SPFD.FeeCategoryPartTermMapId = (((Dt2.Rows[j]["FeeCategoryPartTermMapId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt2.Rows[j]["FeeCategoryPartTermMapId"]);
                        SPFD.FeeTypeFeeCategoryMapId = (((Dt2.Rows[j]["FeeTypeFeeCategoryMapId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt2.Rows[j]["FeeTypeFeeCategoryMapId"]);
                        SPFD.TotalInstalmentGiven = (((Dt2.Rows[j]["TotalInstalmentGiven"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt2.Rows[j]["TotalInstalmentGiven"]);
                        SPFD.TotalInstalmentSelected = (((Dt2.Rows[j]["TotalInstalmentSelected"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt2.Rows[j]["TotalInstalmentSelected"]);
                        SPFD.RemainingInstallment = (((Dt2.Rows[j]["RemainingInstallment"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt2.Rows[j]["RemainingInstallment"]);
                        SPFD.InstalmentNo = (((Dt2.Rows[j]["InstalmentNo"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt2.Rows[j]["InstalmentNo"]);
                        SPFD.IsInstalmentSelected = (Convert.ToString(Dt2.Rows[j]["IsInstalmentSelected"])).IsEmpty() ? false : Convert.ToBoolean(Dt2.Rows[j]["IsInstalmentSelected"]);
                        SPFD.IsFullyPaid = (Convert.ToString(Dt2.Rows[j]["IsFullyPaid"])).IsEmpty() ? false : Convert.ToBoolean(Dt2.Rows[j]["IsFullyPaid"]);
                        SPFD.AmountPaid = (((Dt2.Rows[j]["AmountPaid"]).ToString()).IsEmpty()) ? 0 : Convert.ToDouble(Dt2.Rows[j]["AmountPaid"]);
                        ObjLstSPFD.Add(SPFD);
                    }
                }
                Fee1.PaidFee = ObjLstSPFD;
                Fee1.TotalAmountPaid = 0;
                foreach (StudentPaidFeeDetail SPFD in Fee1.PaidFee)
                {
                    if (Fee1.LastInstalmentNo == SPFD.InstalmentNo)
                    {
                        Fee1.TotalInstalmentSelected = SPFD.TotalInstalmentSelected;
                        Fee1.RemainingInstallment = SPFD.RemainingInstallment;
                        Fee1.IsInstalmentSelected = SPFD.IsInstalmentSelected;
                        Fee1.IsFullyPaid = SPFD.IsFullyPaid;
                    }
                    //Fee1.TotalAmountPaid = Fee1.TotalAmountPaid + SPFD.AmountPaid;
                }
                if (Fee1.IsInstalmentGiven == true)
                {
                    if (Fee1.IsInstalmentSelected == true || Convert.ToBoolean(MAF.UserSelection) == true)
                    {
                        if (Convert.ToInt32(MAF.UserSelectedInstalment) > 1 || Fee1.TotalInstalmentSelected > 1)
                        {// for next instalment
                            if (Fee1.LastInstalmentNo > 0)
                            {
                                if (Fee1.RemainingInstallment != 0)
                                {
                                    Fee1.RemainingInstallment = Fee1.RemainingInstallment - 1;
                                    Fee1.InstalmentNo = Fee1.TotalInstalmentSelected - Fee1.RemainingInstallment;
                                    Fee1.TotalAmountPaid = 0;
                                    foreach (MstAdmissionFeeDetail DF in Fee1.DetailFee)
                                    {
                                        DF.AmountPaid = DF.Amount - DF.TotalFSHAmountPaid;
                                        if (DF.IsInstalmentAllowed == true && Fee1.RemainingInstallment > 0)
                                        {
                                            DF.AmountPaid = DF.AmountPaid / (Fee1.RemainingInstallment + 1);
                                            DF.AmountPaid = Convert.ToDouble(DF.AmountPaid.ToString("0.00"));
                                        }
                                        
                                        Fee1.TotalAmountPaid = Fee1.TotalAmountPaid + DF.AmountPaid;
                                    }
                                }
                                else { return Return.returnHttp("201", "You have already paid fees.", null); }
                            }
                            else if (Fee1.LastInstalmentNo == 0 && Fee1.IsFullyPaid == false)
                            {
                                Fee1.InstalmentNo = 1;
                                Fee1.TotalAmountPaid = 0;
                                Fee1.RemainingInstallment = MAF.UserSelectedInstalment - 1;
                                foreach (MstAdmissionFeeDetail DF in Fee1.DetailFee)
                                {
                                    DF.AmountPaid = DF.Amount;
                                    if (DF.IsInstalmentAllowed == true)
                                    {
                                        DF.AmountPaid = DF.AmountPaid / MAF.UserSelectedInstalment;
                                        DF.AmountPaid = Convert.ToDouble(DF.AmountPaid.ToString("0.00"));
                                    }
                                    
                                    Fee1.TotalAmountPaid = Fee1.TotalAmountPaid + DF.AmountPaid;
                                    Fee1.TotalAmountPaid = Convert.ToDouble(Fee1.TotalAmountPaid.ToString("0.00"));
                                }
                            }
                        }
                        else { return Return.returnHttp("201", "Kindly choose Valid Instalment No.", null); }
                    }
                    else
                    {
                        Fee1.InstalmentNo = 1;
                        Fee1.TotalAmountPaid = 0;
                        Fee1.RemainingInstallment = 0;
                        foreach (MstAdmissionFeeDetail DF in Fee1.DetailFee)
                        {
                            DF.AmountPaid = DF.Amount;
                            
                            Fee1.TotalAmountPaid = Fee1.TotalAmountPaid + DF.AmountPaid;
                        }
                    }
                }
                else
                {
                    Fee1.InstalmentNo = 1;
                    Fee1.TotalAmountPaid = 0;
                    Fee1.RemainingInstallment = 0;
                    foreach (MstAdmissionFeeDetail DF in Fee1.DetailFee)
                    {
                        DF.AmountPaid = DF.Amount;
                        
                        Fee1.TotalAmountPaid = Fee1.TotalAmountPaid + DF.AmountPaid;
                    }
                }
                Int64 FeeCategoryPartTermMapId = Convert.ToInt64(Fee1.Id);
                Int64 ProgrammeInstancePartTermId = Convert.ToInt64(Fee1.ProgrammeInstancePartTermId);
                Int64 FeeTypeFeeCategoryMapId = Convert.ToInt64(Fee1.FeeTypeFeeCategoryMapId);
                Int32 UserSelectedInstalment = Convert.ToInt32(MAF.UserSelectedInstalment);
                Double TotalAmount = Convert.ToDouble(Fee1.TotalAmount);
                String FeeCategoryId1 = Fee1.FeeCategoryId.ToString("D3");
                String ProgrammeInstancePartTermId1 = Fee1.ProgrammeInstancePartTermId.ToString("D5");
                String OrderId = string.Format("{0}{1}{2}{3}{4}", "1", FeeCategoryId1, ProgrammeInstancePartTermId1, ApplicationId, InstalmentNo);
                Int32 RemainingInstallment = Convert.ToInt32(Fee1.RemainingInstallment);

                var DetailFee = Fee1.DetailFee;
                Int32 Count = Convert.ToInt32(DetailFee.Count);
                SqlCommand[] CMDarray = new SqlCommand[Count];
                Int32 k = 0;
                String msg = null;
                SqlParameter[] Oid = new SqlParameter[Count];

                foreach (MstAdmissionFeeDetail DF in DetailFee)
                {
                    Int64 FeeConfigurationId = Convert.ToInt64(DF.Id);
                    Int64 FeeTypeFeeHeadMapId = Convert.ToInt64(DF.FeeTypeFeeHeadMapId);
                    Double AmountPaid = Convert.ToDouble(DF.AmountPaid);
                    Boolean IsInstalmentAllowed = Convert.ToBoolean(DF.IsInstalmentAllowed);
                    if (IsDeclarationRead && IsFinalConfirm)
                    {
                    
                        CMDarray[k] = new SqlCommand("TempStudentAdmissionFeeDetailAdd", Con, ST);
                        CMDarray[k].CommandType = CommandType.StoredProcedure;

                        CMDarray[k].Parameters.AddWithValue("@FeeCategoryPartTermMapId", FeeCategoryPartTermMapId);
                        CMDarray[k].Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                        CMDarray[k].Parameters.AddWithValue("@FeeTypeFeeCategoryMapId", FeeTypeFeeCategoryMapId);
                        CMDarray[k].Parameters.AddWithValue("@ApplicationId", ApplicationId);
                        CMDarray[k].Parameters.AddWithValue("@PRN", PRN);
                        CMDarray[k].Parameters.AddWithValue("@NumberofInstalment", NumberofInstalment);
                        CMDarray[k].Parameters.AddWithValue("@InstalmentNo", Fee1.InstalmentNo);
                        CMDarray[k].Parameters.AddWithValue("@UserSelectedInstalment", UserSelectedInstalment);
                        CMDarray[k].Parameters.AddWithValue("@TotalAmountPaid", Fee1.TotalAmountPaid);
                        CMDarray[k].Parameters.AddWithValue("@IsInstalmentGiven", Fee1.IsInstalmentGiven);
                        CMDarray[k].Parameters.AddWithValue("@UserSelection", UserSelection);
                        CMDarray[k].Parameters.AddWithValue("@FeeConfigurationId", FeeConfigurationId);
                        CMDarray[k].Parameters.AddWithValue("@FeeTypeFeeHeadMapId", FeeTypeFeeHeadMapId);
                        CMDarray[k].Parameters.AddWithValue("@AmountPaid", AmountPaid);
                        CMDarray[k].Parameters.AddWithValue("@IsInstalmentAllowed", IsInstalmentAllowed);
                        CMDarray[k].Parameters.AddWithValue("@IsDeclarationRead", IsDeclarationRead);
                        CMDarray[k].Parameters.AddWithValue("@IsFinalConfirm", IsFinalConfirm);
                        CMDarray[k].Parameters.AddWithValue("@NetAmount", TotalAmount);
                       
                        
                        CMDarray[k].Parameters.AddWithValue("@OrderId", OrderId);

                        CMDarray[k].Parameters.AddWithValue("@RemainingInstallment", RemainingInstallment);
                        CMDarray[k].Parameters.AddWithValue("@UserTime", datetime);
                        CMDarray[k].Parameters.AddWithValue("@UserId", UserId);

                        CMDarray[k].Parameters.Add("@Message", SqlDbType.NVarChar, 500);

                        CMDarray[k].Parameters["@Message"].Direction = ParameterDirection.Output;
                        k++;


                    }
                }
                Con.Open();

                ST = Con.BeginTransaction();
                try
                {

                    for (int j = 0; j < k; j++)
                    {
                        CMDarray[j].Transaction = ST;
                        int ans = CMDarray[j].ExecuteNonQuery();
                        msg = msg + ", " + Convert.ToString(CMDarray[j].Parameters["@Message"].Value);
                    }

                    ST.Commit();

                    MstAdmissionFee TempData = new MstAdmissionFee();
                    TempData.OrderId = OrderId;
                    TempData.ProgrammeInstancePartTermId = ProgrammeInstancePartTermId;
                    TempData.FeeTypeFeeCategoryMapId = FeeTypeFeeCategoryMapId;
                    TempData.ApplicationId = ApplicationId;
                    TempData.PRN = PRN;
                    TempData.AcademicYearId = Fee1.AcademicYearId;
                    TempData.FeeTypeId = Fee1.FeeTypeId;
                    TempData.FeeCategoryId = Fee1.FeeCategoryId;
                    TempData.NumberofInstalment = NumberofInstalment;
                    TempData.InstalmentNo = InstalmentNo;
                    TempData.TotalAmount = TotalAmount;
                    TempData.TotalAmountPaid = Fee1.TotalAmountPaid;
                    TempData.IsInstalmentGiven = Fee1.IsInstalmentGiven;
                    TempData.UserSelection = UserSelection;
                    TempData.UserSelectedInstalment = UserSelectedInstalment;
                    TempData.TotalInstalmentSelected = UserSelectedInstalment;
                    TempData.RemainingInstallment = RemainingInstallment;
                    TempData.IsInstalmentSelected = UserSelection;
                    TempData.LastInstalmentNo = Fee1.LastInstalmentNo;
                    TempData.FeeTypeName = Fee1.FeeTypeName.Replace("&", " and "); ;
                    TempData.InstancePartTermName = Fee1.InstancePartTermName.Replace("&", " and ");
                    TempData.AcademicYearCode = Fee1.AcademicYearCode;
                    TempData.FeeCategoryName = Fee1.FeeCategoryName;
                    TempData.FacultyName = Fee1.FacultyName.Replace("&", " and ");
                    TempData.ProgrammeName = Fee1.ProgrammeName.Replace("&", " and ");
                    TempData.InstituteName = Fee1.InstituteName.Replace("&", " and ");
                    TempData.BranchName = Fee1.BranchName.Replace("&", " and ");

                    return Return.returnHttp("200", TempData, null);
                    //return Return.returnHttp("200", "Record Added", null);
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
                if (e.Message.ToString() == "The given header was not found.")
                {
                    return Return.returnHttp("0", "Session is expired, Kindly login again.", null);
                }
                else
                {
                    return Return.returnHttp("201", e.Message.ToString(), null);
                }
            }

        }
        #endregion

        #region PayAdmissionFeesICCR
        [HttpPost]
        public HttpResponseMessage PayAdmissionFeesICCR(MstAdmissionFee MAF)
        {
            try
            {
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int64 UserId = Convert.ToInt64(responseToken.ToString());
                //string strMessage = null;
                Int64 ApplicationId = Convert.ToInt64(MAF.ApplicationId);
                Int64 PRN = Convert.ToInt64(MAF.PRN);
                Int32 NumberofInstalment = Convert.ToInt32(MAF.NumberofInstalment);
                Int32 InstalmentNo = Convert.ToInt32(MAF.InstalmentNo);
                Double TotalAmountPaid = Convert.ToDouble(MAF.TotalAmountPaid);
                Boolean IsInstalmentGiven = Convert.ToBoolean(MAF.IsInstalmentGiven);
                Boolean UserSelection = Convert.ToBoolean(MAF.UserSelection);
                Boolean IsDeclarationRead = Convert.ToBoolean(MAF.IsDeclarationRead);
                Boolean IsFinalConfirm = Convert.ToBoolean(MAF.IsFinalConfirm);
                String msg=null;
                Int64 FeeCategoryPartTermMapId = Convert.ToInt64(MAF.Id);
                Int64 ProgrammeInstancePartTermId = Convert.ToInt64(MAF.ProgrammeInstancePartTermId);
                Int64 FeeTypeFeeCategoryMapId = Convert.ToInt64(MAF.FeeTypeFeeCategoryMapId);
                Int32 UserSelectedInstalment = Convert.ToInt32(MAF.UserSelectedInstalment);
                Double TotalAmount = Convert.ToDouble(MAF.TotalAmount);
                String FeeCategoryId1 = MAF.FeeCategoryId.ToString("D3");
                String ProgrammeInstancePartTermId1 = MAF.ProgrammeInstancePartTermId.ToString("D5");
                String OrderId = string.Format("{0}{1}{2}{3}{4}", "1", FeeCategoryId1, ProgrammeInstancePartTermId1, ApplicationId, InstalmentNo);
                Int32 RemainingInstallment = Convert.ToInt32(MAF.RemainingInstallment);



                DataTable Temp = new DataTable();
                //PaperList.Columns.Add(new DataColumn("Id", typeof(Int64)));
                Temp.Columns.Add(new DataColumn("Id", typeof(Int64)));
                Temp.Columns.Add(new DataColumn("OrderId", typeof(String)));
                Temp.Columns.Add(new DataColumn("FeeCategoryPartTermMapId", typeof(Int64)));
                Temp.Columns.Add(new DataColumn("ProgrammeInstancePartTermId", typeof(Int64)));
                Temp.Columns.Add(new DataColumn("FeeTypeFeeCategoryMapId", typeof(Int64)));
                Temp.Columns.Add(new DataColumn("ApplicationId", typeof(Int64)));
                Temp.Columns.Add(new DataColumn("PRN", typeof(Int64)));
                Temp.Columns.Add(new DataColumn("FeeTypeFeeHeadMapId", typeof(Int64)));
                Temp.Columns.Add(new DataColumn("FeeConfigurationId", typeof(Int64)));
                Temp.Columns.Add(new DataColumn("NumberofInstalment", typeof(Int32)));
                Temp.Columns.Add(new DataColumn("UserSelectedInstalment", typeof(Int32)));
                Temp.Columns.Add(new DataColumn("NetAmount", typeof(Double)));
                Temp.Columns.Add(new DataColumn("TotalAmountPaid", typeof(Double)));
                Temp.Columns.Add(new DataColumn("RemainingInstallment", typeof(Int32)));
                Temp.Columns.Add(new DataColumn("InstalmentNo", typeof(Int32)));
                Temp.Columns.Add(new DataColumn("AmountPaid", typeof(Double)));
                Temp.Columns.Add(new DataColumn("IsInstalmentGiven", typeof(Boolean)));
                Temp.Columns.Add(new DataColumn("UserSelection", typeof(Boolean)));
                Temp.Columns.Add(new DataColumn("IsInstalmentAllowed", typeof(Boolean)));
                Temp.Columns.Add(new DataColumn("IsFinalConfirm", typeof(Boolean)));
                Temp.Columns.Add(new DataColumn("IsDeclarationRead", typeof(Boolean)));
                Temp.Columns.Add(new DataColumn("IsActive", typeof(Boolean)));
                Temp.Columns.Add(new DataColumn("IsDeleted", typeof(Boolean)));
                Temp.Columns.Add(new DataColumn("CreatedBy", typeof(Int64)));
                Temp.Columns.Add(new DataColumn("CreatedOn", typeof(DateTime)));

    

                for (int i=0;i<MAF.DetailFee.Count();i++)// MstAdmissionFeeDetail DF in DetailFee)
                {
                    Temp.Rows.Add(i, OrderId, FeeCategoryPartTermMapId, ProgrammeInstancePartTermId, 
                                FeeTypeFeeCategoryMapId, ApplicationId, PRN, MAF.DetailFee[i].FeeTypeFeeHeadMapId, 
                                MAF.DetailFee[i].Id, NumberofInstalment, UserSelectedInstalment, TotalAmount,
                                TotalAmountPaid, RemainingInstallment, InstalmentNo, MAF.DetailFee[i].AmountPaid,
                                IsInstalmentGiven, UserSelection, MAF.DetailFee[i].IsInstalmentAllowed,
                                IsFinalConfirm, IsDeclarationRead, 1, 0, UserId, datetime);
                }
                Con.Open();

                ST = Con.BeginTransaction();
                try
                {
                    
                    SqlCommand CMDarray = new SqlCommand("AdmissionFeesPaidAddforICCR", Con, ST);
                    CMDarray.CommandType = CommandType.StoredProcedure;
                    CMDarray.Parameters.AddWithValue("@Temp", Temp);
                    CMDarray.Parameters.AddWithValue("@FeeCategoryPartTermMapId", FeeCategoryPartTermMapId);
                    CMDarray.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                    CMDarray.Parameters.AddWithValue("@FeeTypeFeeCategoryMapId", FeeTypeFeeCategoryMapId);
                    CMDarray.Parameters.AddWithValue("@ApplicationId", ApplicationId);
                    CMDarray.Parameters.AddWithValue("@PRN", PRN);
                    CMDarray.Parameters.AddWithValue("@TotalInstalmentGiven", NumberofInstalment);
                    CMDarray.Parameters.AddWithValue("@InstalmentNo", InstalmentNo);
                    CMDarray.Parameters.AddWithValue("@TotalInstalmentSelected", UserSelectedInstalment);
                    CMDarray.Parameters.AddWithValue("@TotalAmountPaid", TotalAmountPaid);
                    CMDarray.Parameters.AddWithValue("@IsInstalmentGiven", IsInstalmentGiven);
                    CMDarray.Parameters.AddWithValue("@IsInstalmentSelected", UserSelection);
                    CMDarray.Parameters.AddWithValue("@NetAmount", TotalAmount);
                    CMDarray.Parameters.AddWithValue("@OrderId", OrderId);
                    CMDarray.Parameters.AddWithValue("@RemainingInstallment", RemainingInstallment);
                    CMDarray.Parameters.AddWithValue("@UserTime", datetime);
                    CMDarray.Parameters.AddWithValue("@UserId", UserId);

                    CMDarray.Parameters.Add("@Message", SqlDbType.NVarChar, 500);

                    CMDarray.Parameters["@Message"].Direction = ParameterDirection.Output;
                    
                    CMDarray.Transaction = ST;
                    int ans = CMDarray.ExecuteNonQuery();
                    msg = Convert.ToString(CMDarray.Parameters["@Message"].Value);
                    

                    ST.Commit();

                    
                    return Return.returnHttp("200", msg, null);
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
                if (e.Message.ToString() == "The given header was not found.")
                {
                    return Return.returnHttp("0", "Session is expired, Kindly login again.", null);
                }
                else
                {
                    return Return.returnHttp("201", e.Message.ToString(), null);
                }
            }

        }
        #endregion

        #region AdmAdmissionFeesPaidRecieptGet
        [HttpPost]
        public HttpResponseMessage AdmAdmissionFeesPaidRecieptGet(AdmAdmissionFeesPaidReciept admafp)
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

                SqlCommand cmd = new SqlCommand("AdmAdmissionFeesPaidReciept", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ApplicationId", admafp.Id);
                
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable Dt = new DataTable();
                sda.Fill(Dt);
                List<AdmAdmissionFeesPaidReciept> ObjLstAdmAppFeesPaid = new List<AdmAdmissionFeesPaidReciept>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        AdmAdmissionFeesPaidReciept ObjAddPR = new AdmAdmissionFeesPaidReciept();
                        ObjAddPR.Id = (Convert.ToString(Dt.Rows[i]["Id"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["Id"]);
                        ObjAddPR.OrderId = (Convert.ToString(Dt.Rows[i]["OrderId"])).IsEmpty() ? "Not Define" : Convert.ToString(Dt.Rows[i]["OrderId"]);
                        ObjAddPR.PRN = (Convert.ToString(Dt.Rows[i]["PRN"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["PRN"]);
                        ObjAddPR.ApplicationId = (Convert.ToString(Dt.Rows[i]["ApplicationId"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["ApplicationId"]);
                        ObjAddPR.ProgrammeInstancePartTermId = (Convert.ToString(Dt.Rows[i]["ProgrammeInstancePartTermId"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["ProgrammeInstancePartTermId"]);
                        ObjAddPR.FeeTypeFeeCategoryMapId = (Convert.ToString(Dt.Rows[i]["FeeTypeFeeCategoryMapId"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["FeeTypeFeeCategoryMapId"]);
                        ObjAddPR.FeeCategoryPartTermMapId = (Convert.ToString(Dt.Rows[i]["FeeCategoryPartTermMapId"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["FeeCategoryPartTermMapId"]);
                        ObjAddPR.InstalmentNo = (Convert.ToString(Dt.Rows[i]["InstalmentNo"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["InstalmentNo"]);
                        ObjAddPR.FeeStatus = (Convert.ToString(Dt.Rows[i]["FeeStatus"])).IsEmpty() ? "No DATA" : Convert.ToString(Dt.Rows[i]["FeeStatus"]);
                        ObjAddPR.AcademicYearCode = (Convert.ToString(Dt.Rows[i]["AcademicYearCode"])).IsEmpty() ? "No DATA" : Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);
                        ObjAddPR.NameAsPerMarksheet = (Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"])).IsEmpty() ? "No DATA" : Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"]);
                        ObjAddPR.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "No DATA" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        ObjAddPR.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "No DATA" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        ObjAddPR.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "No DATA" : Convert.ToString(Dt.Rows[i]["BranchName"]);
                        ObjAddPR.TotalAmount = (Convert.ToString(Dt.Rows[i]["TotalAmount"])).IsEmpty() ? 0 : Convert.ToDouble(Dt.Rows[i]["TotalAmount"]);
                        ObjAddPR.AmountPaid = (Convert.ToString(Dt.Rows[i]["AmountPaid"])).IsEmpty() ? 0 : Convert.ToDouble(Dt.Rows[i]["AmountPaid"]);
                        ObjAddPR.IsFullyPaid = (Convert.ToString(Dt.Rows[i]["IsFullyPaid"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsFullyPaid"]);
                        ObjAddPR.IsInstalmentSelected = (Convert.ToString(Dt.Rows[i]["IsInstalmentSelected"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsInstalmentSelected"]);

                        ObjLstAdmAppFeesPaid.Add(ObjAddPR);
                    }

                }

                return Return.returnHttp("200", ObjLstAdmAppFeesPaid, null);
            }
            catch (Exception e)
            {
                if (e.Message.ToString() == "The given header was not found.")
                {
                    return Return.returnHttp("0", "Session is expired, Kindly login again.", null);
                }
                else
                {
                    return Return.returnHttp("201", e.Message.ToString(), null);
                }
            }

        }
        #endregion

        #region FeesPaidRecieptDetailGet
        [HttpPost]
        public HttpResponseMessage FeesPaidRecieptDetailGet(AdmAdmissionFeesPaidReciept admafp)
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

                SqlCommand cmd = new SqlCommand("AdmissionFeePaidGetbyOrderId", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderId", admafp.OrderId);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable Dt = new DataTable();
                sda.Fill(Dt);
                List<AdmAdmissionFeesPaidReciept> ObjLstAdmAppFeesPaid = new List<AdmAdmissionFeesPaidReciept>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        AdmAdmissionFeesPaidReciept ObjAddPR = new AdmAdmissionFeesPaidReciept();

                        ObjAddPR.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        ObjAddPR.PRN = Convert.ToInt64(Dt.Rows[i]["PRN"]);
                        ObjAddPR.ApplicationId = Convert.ToInt64(Dt.Rows[i]["ApplicationId"]);
                        ObjAddPR.TotalInstalmentGiven = Convert.ToInt32(Dt.Rows[i]["TotalInstalmentGiven"]);
                        ObjAddPR.TotalInstalmentSelected = Convert.ToInt32(Dt.Rows[i]["TotalInstalmentSelected"]);
                        ObjAddPR.InstalmentNo = Convert.ToInt32(Dt.Rows[i]["InstalmentNo"]);
                        ObjAddPR.AmountPaid = Convert.ToDouble(Dt.Rows[i]["AmountPaid"]);
                        ObjAddPR.IsFullyPaid = Convert.ToBoolean(Dt.Rows[i]["IsFullyPaid"]);
                        ObjAddPR.TotalAmount = Convert.ToDouble(Dt.Rows[i]["TotalAmount"]);
                        ObjAddPR.ProgrammeName = Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        ObjAddPR.FacultyName = Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        ObjAddPR.BranchName = Convert.ToString(Dt.Rows[i]["BranchName"]);
                        ObjAddPR.FeeCategoryName = Convert.ToString(Dt.Rows[i]["FeeCategoryName"]);
                        ObjAddPR.FeeTypeName = Convert.ToString(Dt.Rows[i]["FeeTypeName"]);
                        ObjAddPR.NameAsPerMarksheet = Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"]);
                        ObjAddPR.OrderId = Convert.ToString(Dt.Rows[i]["OrderId"]);
                        ObjAddPR.AcademicYearCode = Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);
                        ObjAddPR.PaymentDateDisplay = Convert.ToString(Dt.Rows[i]["PaymentDate"]);
                        ObjAddPR.MobileNo = Convert.ToString(Dt.Rows[i]["MobileNo"]);
                        ObjAddPR.InstancePartName = Convert.ToString(Dt.Rows[i]["InstancePartName"]);
                        ObjAddPR.InstituteName = Convert.ToString(Dt.Rows[i]["InstituteName"]);
                        ObjLstAdmAppFeesPaid.Add(ObjAddPR);
                    }

                }
                SqlCommand cmd1 = new SqlCommand("AdmissionFeePaidDetailGetbyOrderId", Con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@OrderId", admafp.OrderId);

                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable Dt1 = new DataTable();
                sda1.Fill(Dt1);
                List<AdmissionFeesDetailReciept> ObjAdmFeesPaidDetail = new List<AdmissionFeesDetailReciept>();
                if (Dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt1.Rows.Count; i++)
                    {
                        AdmissionFeesDetailReciept AFPD = new AdmissionFeesDetailReciept();

                        AFPD.Id = Convert.ToInt64(Dt1.Rows[i]["Id"]);
                        AFPD.PRN = Convert.ToInt64(Dt1.Rows[i]["PRN"]);
                        AFPD.ApplicationId = Convert.ToInt64(Dt1.Rows[i]["ApplicationId"]);
                        AFPD.OrderId = Convert.ToString(Dt1.Rows[i]["OrderId"]);
                        AFPD.FeeHeadName = Convert.ToString(Dt1.Rows[i]["FeeHeadName"]);
                        AFPD.FeeSubHeadName = Convert.ToString(Dt1.Rows[i]["FeeSubHeadName"]);
                        AFPD.AmountPaid = Convert.ToDouble(Dt1.Rows[i]["AmountPaid"]);
                        AFPD.FeeSubHeadSrno = Convert.ToInt32(Dt1.Rows[i]["FeeSubHeadSrno"]);

                        ObjAdmFeesPaidDetail.Add(AFPD);
                    }

                }
                ObjLstAdmAppFeesPaid[0].AFDR = ObjAdmFeesPaidDetail;
                return Return.returnHttp("200", ObjLstAdmAppFeesPaid, null);
            }
            catch (Exception e)
            {
                if (e.Message.ToString() == "The given header was not found.")
                {
                    return Return.returnHttp("0", "Session is expired, Kindly login again.", null);
                }
                else
                {
                    return Return.returnHttp("201", e.Message.ToString(), null);
                }
            }

        }
        #endregion
    }
}