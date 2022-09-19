using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using System;
using System.Globalization;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using MSUISApi.BAL;

namespace MSUISApi.Controllers
{
    public class TransferCertiController : ApiController
    {
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region Get Programme List
        [HttpPost]
        public HttpResponseMessage getProgrammeList()
        {
            string token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation tokenOperation = new TokenOperation();
            string responseToken = tokenOperation.ValidateToken(token);
            Request.Headers.TryGetValues("InstituteId", out IEnumerable<string> values);
            var InstituteId = values?.FirstOrDefault();

            if (responseToken == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            else
            {
                try
                {
                    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                    string sql = "SELECT MstProgramme.ProgrammeName,MstProgramme.Id as ProgId FROM MstProgramme " +
                        "INNER JOIN MstInstituteProgrammeMap ON MstInstituteProgrammeMap.ProgrammeId = MstProgramme.Id " +
                        "INNER JOIN MstInstitute ON MstInstitute.Id = MstInstituteProgrammeMap.InstituteId WHERE MstProgramme.IsDeleted = 'FALSE' AND MstInstitute.Id =" + InstituteId;

                    SqlDataAdapter ad = new SqlDataAdapter();
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    ad.SelectCommand = cmd;
                    // cmd.Parameters.AddWithValue("@InstituteId", InstituteId);
                    ad.Fill(dt);
                    List<GetStdudentTransferDetails> list = new List<GetStdudentTransferDetails>();

                    foreach (DataRow DR in dt.Rows)
                    {
                        GetStdudentTransferDetails s = new GetStdudentTransferDetails();
                        s.ProgId = Convert.ToInt16(DR["ProgId"]);
                        s.ProgrammeName = DR["ProgrammeName"].ToString();
                        list.Add(s);
                    }
                    return Return.returnHttp("200", list, null);
                }

                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message, null);
                }
            }
        }
        #endregion

        #region Get Branch List
        [HttpPost]
        public HttpResponseMessage getBranchList(int ProgId)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("GetBranchListFromProgId", con);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@prn", PRN);
            cmd.Parameters.AddWithValue("@ProgId", ProgId);
            da.Fill(dt);
            List<GetBranchList> list = new List<GetBranchList>();
            list = dt.DataTableToList<GetBranchList>();
            //cmd.Parameters.AddWithValue("@yrcode", Convert.ToString(certiinsert.AcademicYearCode));
            //cmd.Parameters.AddWithValue("@CertiId", 1);
            //da.SelectCommand = cmd;
            //if (con.State == ConnectionState.Closed)
            con.Open();
            //cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
            return Return.returnHttp("200", list, null);
        }
        #endregion

        #region Enter Data into Certificate Issue Table
        [HttpPost]

        public HttpResponseMessage enterDetails(Int64 PRN, int ProgId)
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

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand("TransferCertificateData", con);
                SqlDataAdapter da = new SqlDataAdapter();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prn", PRN);
                cmd.Parameters.AddWithValue("@ProgId", ProgId);
                cmd.Parameters.AddWithValue("@UserId", Convert.ToInt64(res));
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                //cmd.Parameters.AddWithValue("@yrcode", Convert.ToString(certiinsert.AcademicYearCode));
                //cmd.Parameters.AddWithValue("@CertiId", 1);
                //da.SelectCommand = cmd;
                //if (con.State == ConnectionState.Closed)
                con.Open();
                //cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();
                return Return.returnHttp("200", "Data Stored Successfully", null);
            }

            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Generate Transfer Certificate
        [HttpPost]
        public HttpResponseMessage GenerateTransferCertificate(GetStdudentTransferDetails s1)
        {
            string token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation tokenOperation = new TokenOperation();
            string responseToken = tokenOperation.ValidateToken(token);

            if (responseToken == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            else
            {
                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("TransferCertificateGeneratebyDeannew", con);
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@enprn", s1.PRN);
                    cmd.Parameters.AddWithValue("@ProgrammeId", s1.ProgId);
                    cmd.Parameters.AddWithValue("@SpecId", s1.SpecId);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                    // cmd.Parameters.AddWithValue("@Message", s1.SpecId);
                    //cmd.Parameters.AddWithValue("@ReqId", reqId);
                    //cmd.Parameters.AddWithValue("@CertiId", CertiId);

                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    if (dt.Rows.Count == 0 && cmd.Parameters["@Message"].Value != null)
                    {
                        return Return.returnHttp("601", "Already generated!", null);
                    }

                    else
                    {
                        List<TransferCertificate> list = new List<TransferCertificate>();
                        //TransferCertificate s = new TransferCertificate();
                        list = dt.DataTableToList<TransferCertificate>();
                        //foreach (DataRow DR in dt.Rows)
                        //{
                        //    s.InstituteName = DR["InstituteName"].ToString();
                        //    s.InstituteAddress = DR["InstituteAddress"].ToString();
                        //    s.CertificateName = DR["CertificateName"].ToString();
                        //    s.PRN = DR["PRN"].ToString();
                        //    s.NameAsPerMarksheet = DR["NameAsPerMarksheet"].ToString();
                        //    s.Gender = DR["Gender"].ToString();
                        //    //s.ResultStatus = DR["ResultStatus"].ToString();
                        //    s.PartName = DR["PartName"].ToString();
                        //    s.BranchName = DR["BranchName"].ToString();
                        //    s.ProgrammeModeName = DR["ProgrammeModeName"].ToString();
                        //    s.PartTermShortName = DR["PartTermShortName"].ToString();
                        //    s.AcademicYearCode = DR["AcademicYearCode"].ToString();
                        //    s.DisplayName = DR["DisplayName"].ToString();
                        //    s.StudentPhoto = DR["StudentPhoto"].ToString();
                        //    s.StudentSignature = DR["StudentSignature"].ToString();
                        //    s.Sign = DR["Sign"].ToString();
                        //    s.DOB = DR["DOB"].ToString();
                        //    s.TCCode = DR["TCCode"].ToString();
                        //    s.AuthorityType = DR["AuthorityType"].ToString();
                        //    s.NameOfAuthority = DR["NameOfAuthority"].ToString();
                        //    s.Part1 = DR["Part1"].ToString();
                        //    s.Part2 = DR["Part2"].ToString();
                        //    s.Part3 = DR["Part3"].ToString();
                        //    s.Part4 = DR["Part4"].ToString();
                        //    s.Part5 = DR["Part5"].ToString();
                        //    s.Part6 = DR["Part6"].ToString();
                        //    s.Part7 = DR["Part7"].ToString();
                        //    s.Part8 = DR["Part8"].ToString();
                        //    s.Part9 = DR["Part9"].ToString();
                        //    s.Part10 = DR["Part10"].ToString();
                        //    s.Part11 = DR["Part11"].ToString();
                        //    s.Part12 = DR["Part12"].ToString();
                        //    s.Part13 = DR["Part13"].ToString();
                        //    s.Part14 = DR["Part14"].ToString();
                        //    s.Part15 = DR["Part15"].ToString();
                        //    list.Add(s);
                        //}
                        //list[0].NameAsPerMarksheet = txtinfo.ToTitleCase(list[0].NameAsPerMarksheet);

                        //list[0].PRN = list[0].PRN.ToString();
                        //list[0].CertificateName = list[0].CertificateName.ToString();
                        // BALCommon common = new BALCommon();

                        //main commented on 25 - 07 - 2022

                        //string pathphoto1 = Path.GetFullPath(dirphoto1) + picinfophoto1;
                        // byte[] imageArrayphoto1 = File.ReadAllBytes(pathphoto1);
                        // string image = Convert.ToBase64String(pathphoto1);
                        //byte[] imageArrayphoto1 = File.ReadAllBytes(pathphoto1);
                        //string base64PhotoRepresentation1 = Convert.ToBase64String(imageArrayphoto1);
                        //list[0].StudentPhoto = base64PhotoRepresentation1;
                        // over



                        //Path.Combine(dirphoto, picinfophoto);


                        //student photo process
                        //string dirphoto1 = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/Photo/");
                        string picinfophoto1 = Convert.ToString(list[0].StudentPhoto);
                        string OldBaseUrlForPhoto = "https://admission.msubaroda.ac.in/MSUISApi/Upload/Photo/";
                        string NewBaseUrlForPhoto = "https://msuis.msubaroda.ac.in/Vidhyarthi_API/Upload/Photo/";
                        BALCommon common = new BALCommon();
                        string FullPath = OldBaseUrlForPhoto + picinfophoto1;
                        string FullPath1 = NewBaseUrlForPhoto + picinfophoto1; 
                        bool FileResponse = common.ServerFileExists(FullPath);
                        bool FileResponse1 = common.ServerFileExists(FullPath1);

                        if (FileResponse == true)
                        {
                            list[0].StudentPhoto = FullPath;
                        }
                        else if (FileResponse1 == true)
                        {
                            list[0].StudentPhoto = FullPath1;
                        }
                        else
                        {
                            list[0].StudentPhoto = NewBaseUrlForPhoto + "DefaultPhoto.png";
                        }

                        //main commented on 25 - 07 - 2022


                        //list.Add(list[0]);

                        //over

                        //string dirphoto = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/Signature/");


                        //string pathphoto = Path.GetFullPath(dirphoto) + picinfophoto;
                        //string image = Convert.ToBase64String(pathphoto1);
                        //byte[] imageArrayphoto = File.ReadAllBytes(pathphoto);
                        //string base64PhotoRepresentation = Convert.ToBase64String(imageArrayphoto);
                        //Path.Combine(dirphoto, picinfophoto);

                        //student sign process
                        string picinfophoto = Convert.ToString(list[0].StudentSignature);
                        string OldBaseUrlForSign = "https://admission.msubaroda.ac.in/MSUISApi/Upload/Signature/";
                        string NewBaseUrlForSign = "https://msuis.msubaroda.ac.in/Vidhyarthi_API/Upload/Signature/";
                        string FullPath2 = OldBaseUrlForSign + picinfophoto;
                        string FullPath3 = NewBaseUrlForSign + picinfophoto;

                        bool FileResponse2 = common.ServerFileExists(FullPath2);
                        bool FileResponse3 = common.ServerFileExists(FullPath3);
                        if (FileResponse2 == true)
                        {
                            list[0].StudentSignature = FullPath2;
                        }
                        else if (FileResponse3 == true)
                        {
                            list[0].StudentSignature = FullPath3;
                        }
                        else
                        {
                            list[0].StudentSignature = NewBaseUrlForSign + "DefaultSign.png";
                        }
                        //list[0].StudentSignature = base64PhotoRepresentation;
                        //list.Add(list[0]);

                        //string OldBaseUrlForPhoto = "https://msuis.msubaroda.ac.in/Vidhyarthi_API/Upload/Signature/";
                        ////string NewBaseUrlForPhoto = "https://msuis.msubaroda.ac.in/Vidhyarthi_API/Upload/Signature/";  //"https://localhost:44374/Upload/Photo/";
                        //string picinfophoto1 = Convert.ToString(list[0].EnteredbySign);
                        //string FullPath = OldBaseUrlForPhoto + picinfophoto1;
                        ///* BALCommon common = new BALCommon();
                        // bool FileResponse = common.ServerFileExists(FullPath);*/
                        ////string FullPath1 = "https://msuis.msubaroda.ac.in/Vidhyarthi_API/Upload/Signature/" + picinfophoto1;

                        ////string pathphoto1 = Path.GetFullPath(dirphoto1) + picinfophoto1;

                        //list[0].EnteredbySign = FullPath;


                        //main for dean sign
                        //string dirphoto2 = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/Signature/");
                        //string picinfophoto2 = Convert.ToString(list[0].Sign);
                        //string pathphoto2 = Path.GetFullPath(dirphoto2) + picinfophoto2;
                        //string image = Convert.ToBase64String(pathphoto1);
                        //byte[] imageArrayphoto2 = File.ReadAllBytes(pathphoto2);
                        //string base64PhotoRepresentation2 = Convert.ToBase64String(imageArrayphoto2);
                        //Path.Combine(dirphoto, picinfophoto);
                        //list[0].Sign = base64PhotoRepresentation2;


                        string picinfophoto3 = Convert.ToString(list[0].Sign);
                        //string OldBaseUrlForSign = "https://admission.msubaroda.ac.in/MSUISApi/Upload/Signature/";
                        string NewBaseUrlForSign1 = "https://msuis.msubaroda.ac.in/Vidhyarthi_API/Upload/Signature/";
                        //string FullPath = OldBaseUrlForSign + picinfophoto;
                        string FullPath4 = NewBaseUrlForSign1 + picinfophoto3;

                       // bool FileResponse2 = common.ServerFileExists(FullPath1);
                        bool FileResponse4 = common.ServerFileExists(FullPath4);
                        if (FileResponse4 == true)
                        {
                            list[0].Sign = FullPath4;
                        }
                        else
                        {
                            list[0].Sign = NewBaseUrlForSign1 + "DefaultSign.png";
                        }

                        //string dirphoto3 = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/Signature/");
                        //string picinfophoto3 = Convert.ToString(list[0].Sign);
                        //string pathphoto3 = Path.GetFullPath(dirphoto3) + picinfophoto3;
                        //string image = Convert.ToBase64String(pathphoto1);
                        //byte[] imageArrayphoto3 = File.ReadAllBytes(pathphoto3);
                        //string base64PhotoRepresentation3 = Convert.ToBase64String(imageArrayphoto3);
                        //Path.Combine(dirphoto, picinfophoto);
                        //list[0].Sign = base64PhotoRepresentation3;

                        return Return.returnHttp("200", list, null);
                    }

                }

                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message, null);
                }
            }
        }
        #endregion

        #region Confirm Student Transfer
        [HttpPost]

        public HttpResponseMessage ConfirmTransfer(Int64 PRN, bool isConfirmTransfer, int ProgId)
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
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand("storeStudentTransferConformation", con);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@enprn", PRN);
                cmd.Parameters.AddWithValue("@isConfirmTransfer", isConfirmTransfer);
                cmd.Parameters.AddWithValue("@ProgId", ProgId);
                cmd.Parameters.AddWithValue("@UserId", isConfirmTransfer);
                cmd.Parameters.AddWithValue("@UserTime", isConfirmTransfer);
                //da.Fill(dt);
                //List<GetBranchList> list = new List<GetBranchList>();
                //list = dt.DataTableToList<GetBranchList>();
                //cmd.Parameters.AddWithValue("@yrcode", Convert.ToString(certiinsert.AcademicYearCode));
                //cmd.Parameters.AddWithValue("@CertiId", 1);
                //da.SelectCommand = cmd;
                //if (con.State == ConnectionState.Closed)
                con.Open();
                //cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();
                return Return.returnHttp("200", "Success", null);
            }

            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Verify Transfer Certificate
        [HttpPost]
        public HttpResponseMessage VerifyTransferCertificate(CertificateIssue certiinsert)
        {
            string token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation tokenOperation = new TokenOperation();
            string responseToken = tokenOperation.ValidateToken(token);


            if (responseToken == "0")
            {
                return Return.returnHttp("0", null, null);
            }


            else
            {
                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("VerifyTransferCertificate", con);
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@enprn", certiinsert.PRN);
                    cmd.Parameters.AddWithValue("@CertiId", certiinsert.CertiId);
                    cmd.Parameters.AddWithValue("@ProgrammeId", certiinsert.ProgrammeId);
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    List<TransferCertificateVerification> list = new List<TransferCertificateVerification>();
                    list = dt.DataTableToList<TransferCertificateVerification>();

                    CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
                    TextInfo txtinfo = culinfo.TextInfo;

                    list[0].NameAsPerMarksheet = txtinfo.ToTitleCase(list[0].NameAsPerMarksheet);
                    list[0].IssueReason = txtinfo.ToTitleCase(list[0].IssueReason);

                    list[0].CertificateName = txtinfo.ToTitleCase(list[0].CertificateName);

                    string dirphoto = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/Photo/");
                    string picinfophoto = Convert.ToString(list[0].StudentPhoto);
                    string pathphoto = Path.GetFullPath(dirphoto) + picinfophoto;
                    //string image = Convert.ToBase64String(pathphoto);
                    byte[] imageArrayphoto = File.ReadAllBytes(pathphoto);
                    string base64PhotoRepresentation = Convert.ToBase64String(imageArrayphoto);
                    ////Path.Combine(dirphoto, picinfophoto);
                    list[0].StudentPhoto = base64PhotoRepresentation;


                    //string sql = "SELECT ci.PreviousAcademicCode from CertificateIssue as ci where ci.PRN = @enprn";
                    //SqlDataAdapter ad = new SqlDataAdapter();
                    //SqlCommand cmd1 = new SqlCommand(sql, con);
                    //cmd1.Parameters.AddWithValue("@enprn", certiinsert.PRN);
                    //ad.SelectCommand = cmd1;
                    //if (con.State == ConnectionState.Closed)
                    //    con.Open();
                    //cmd1.Connection = con;
                    //CertificateIssue c1 = new CertificateIssue();
                    //c1.PreviousAcademicCode = (string)cmd1.ExecuteScalar();
                    //con.Close();

                    return Return.returnHttp("200", list[0], null);

                }

                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message, null);
                }
            }

        }
        #endregion

    }
}

