using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    /*public class FeeConfiguration
    {
        public Int64 Id { get; set; }
        public String FeeTypeName { get; set; }
        public String FeeCategoryName { get; set; }
        public String PartTermName { get; set; }
        public String PartName { get; set; }
        public String FacultyName { get; set; }
        public String ProgrammeName { get; set; }
        public Double Amount { get; set; }
    }   */
    public class MstAdmissionFee
    {
        public Int64 Id { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public Int64 FeeTypeFeeCategoryMapId { get; set; }
        public Int64 ApplicationId { get; set; }
        public Int64 PRN { get; set; }
        public Int64 StudentAcademicInformationId { get; set; }
        public Int32 AcademicYearId { get; set; }
        public Int32 FeeTypeId { get; set; }
        public Int32 FeeCategoryId { get; set; }
        public Int32 NumberofInstalment { get; set; }
        public Int32 InstalmentNo { get; set; }
        public String FeeTypeName { get; set; }
        public String OrderId { get; set; }
        public String InstancePartTermName { get; set; }
        public String AcademicYearCode { get; set; }
        public String FeeCategoryName { get; set; }
        public String FacultyName { get; set; }
        public String ProgrammeName { get; set; }
        public String InstituteName { get; set; }
        public String NameAsPerMarksheet { get; set; }
        public String BranchName { get; set; }
        public Double TotalAmount { get; set; }
        public Double TotalAmountPaid { get; set; }
        public Boolean IsInstalmentGiven { get; set; }
        public Boolean UserSelection { get; set; }
        public Int32 UserSelectedInstalment { get; set; }
        public Int32 TotalInstalmentSelected { get; set; }
        public Int32 RemainingInstallment { get; set; }
        public Boolean IsInstalmentSelected { get; set; }
        public Boolean IsFullyPaid { get; set; }
        public Boolean IsDeclarationRead { get; set; }
        public Boolean IsFinalConfirm { get; set; }
        public Int32 LastInstalmentNo { get; set; }
        public Int32 InstalmentCount { get; set; }
        public List<MstAdmissionFeeDetail> DetailFee { get; set; }
        public List<StudentPaidFeeDetail> PaidFee { get; set; }
        public Boolean IsICCRApplicant { get; set; }
    }
    public class MstAdmissionFeeDetail
    {
        public Int64 Id { get; set; }
        public Int32 FeeHeadId { get; set; }
        public Int32 FeeSubHeadId { get; set; }
        public Int32 NumberofInstalment { get; set; }
        public String FeeHeadName { get; set; }
        public String FeeSubHeadName { get; set; }
        public Double Amount { get; set; }
        public Double AmountPaid { get; set; }
        public Boolean IsInstalmentAllowed { get; set; }
        public Boolean IsInstalmentGiven { get; set; }
        public Int64 FeeTypeFeeHeadMapId { get; set; }
        public Double TotalFSHAmountPaid { get; set; }
    }
    public class StudentPaidFeeDetail
    {
        public Int64 Id { get; set; }
        public Int64 PRN { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public Int64 ApplicationId { get; set; }
        public Int64 FeeCategoryPartTermMapId { get; set; }
        public Int64 FeeTypeFeeCategoryMapId { get; set; }
        public Int32 TotalInstalmentGiven { get; set; }
        public Int32 TotalInstalmentSelected { get; set; }
        public Int32 RemainingInstallment { get; set; }
        public Int32 InstalmentNo { get; set; }
        public Boolean IsInstalmentSelected { get; set; }
        public Boolean IsFullyPaid { get; set; }
        public Double AmountPaid { get; set; }        
    }

    public class AdmAdmissionFeesPaidReciept
    {
        public Int64 Id { get; set; }
        public Int64 PRN { get; set; }
        public Int64 ApplicationId { get; set; }
        public Int64 ProgrammeInstancePartTermId { get; set; }
        public String FeeStatus { get; set; }
        public String AcademicYearCode { get; set; }
        public Double AmountPaid { get; set; }
        public Int64 FeeTypeFeeCategoryMapId { get; set; }
        public Int64 FeeCategoryPartTermMapId { get; set; }
        public DateTime PaymentDate { get; set; }
        public Int32 InstalmentNo { get; set; }
        public Double TotalAmount { get; set; }
        public String NameAsPerMarksheet { get; set; }
        public String FacultyName { get; set; }
        public String ProgrammeName { get; set; }
        public String BranchName { get; set; }
        public String FeeTypeName { get; set; }
        public String FeeCategoryName { get; set; }
        public String InstancePartName { get; set; }
        public String InstituteName { get; set; }
        public String MobileNo { get; set; }
        public String PaymentDateDisplay { get; set; }
        public Boolean IsFullyPaid { get; set; }
        public Boolean IsInstalmentSelected { get; set; }
        public String OrderId { get; set; }
        public Int32 TotalInstalmentGiven { get; set; }
        public Int32 TotalInstalmentSelected { get; set; }
        public List<AdmissionFeesDetailReciept> AFDR { get; set; }
    }
    public class AdmissionFeesDetailReciept
    {
        public Int64 Id { get; set; }
        public Int64 PRN { get; set; }
        public Int64 ApplicationId { get; set; }
        public String OrderId { get; set; }
        public Double AmountPaid { get; set; }
        public String FeeHeadName { get; set; }
        public String FeeSubHeadName { get; set; }
        public Int32 FeeSubHeadSrno { get; set; }
    }
}