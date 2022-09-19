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
using System.Web.Http;
using MSUISApi.BAL;
using System.Dynamic;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class TransferCertificateReportController : ApiController 
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));


        #region MstFacultyGet
        [HttpPost]
        public HttpResponseMessage FacultyGetById(MstFaculty MF)
        {
            //MstFaculty modelobj = new MstFaculty(); 
            try
            {
                //modelobj.Id = MF.Id;
                SqlCommand cmd = new SqlCommand("MstFacGetbyId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", MF.Id);
                sda.SelectCommand = cmd;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                List<MstFaculty> ObjLstF = new List<MstFaculty>();
                //dt.Rows.Add("0", "All Faculty");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstFaculty ObjF = new MstFaculty();
                        ObjF.FacultyId = Convert.ToInt32(dr["Id"]);
                        ObjF.FacultyName = (dr["FacultyName"].ToString());

                        ObjLstF.Add(ObjF);
                    }

                }

                return Return.returnHttp("200", ObjLstF, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion


        #region ProgrammeGetByFacId
        [HttpPost]
        public HttpResponseMessage ProgrammeGetByFacId(MstProgramme MP)
        {
            //MstFaculty modelobj = new MstFaculty(); 
            try
            {
                //modelobj.Id = MF.Id;
                SqlCommand cmd = new SqlCommand("MstProgrammeGetByFacId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", MP.FacultyId);
                sda.SelectCommand = cmd;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                List<MstProgramme> ObjLstProg = new List<MstProgramme>();
                //dt.Rows.Add("0", "All Faculty");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstProgramme ObjP = new MstProgramme();
                        ObjP.ProgrammeId = Convert.ToInt32(dr["Id"]);
                        ObjP.ProgrammeName = (dr["ProgrammeName"].ToString());

                        ObjLstProg.Add(ObjP);
                    }

                }

                return Return.returnHttp("200", ObjLstProg, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion


        #region CertificateConfigurationGet
        [HttpPost]
        public HttpResponseMessage CertificateConfigurationGet()
        {
            //MstFaculty modelobj = new MstFaculty(); 
            try
            {
                //modelobj.Id = MF.Id;
                SqlCommand cmd = new SqlCommand("CertificateConfigurationGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                
                sda.SelectCommand = cmd;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                List<TransferCertificateReport> ObjLstTCR = new List<TransferCertificateReport>();
                //dt.Rows.Add("0", "All Faculty");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        TransferCertificateReport ObjTCR = new TransferCertificateReport();
                        ObjTCR.CertiId = Convert.ToInt32(dr["CertiId"]);
                        ObjTCR.CertificateName = (dr["CertificateName"].ToString());

                        ObjLstTCR.Add(ObjTCR);
                    }

                }

                return Return.returnHttp("200", ObjLstTCR, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion


        #region TransferCertificateReportGet
        [HttpPost]
        public HttpResponseMessage TransferCertificateReportGet(TransferCertificateReport ObjTCR)
        {
            TransferCertificateReport modelobj = new TransferCertificateReport();
            
            modelobj.FacultyId = ObjTCR.FacultyId;
            modelobj.ProgrammeId = ObjTCR.ProgrammeId;
            modelobj.CertiId = ObjTCR.CertiId;
            
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();

                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                //Int32 InstituteId = Convert.ToInt32(RSR.InstituteId);
               
                SqlCommand Cmd = new SqlCommand("TransferCertificateReport", con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@FacultyId", modelobj.FacultyId);
                Cmd.Parameters.AddWithValue("@ProgrammeId", modelobj.ProgrammeId);
                Cmd.Parameters.AddWithValue("@CertiId", modelobj.CertiId);
               
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = Cmd;
               
                DataTable Dt = new DataTable();
                sda.Fill(Dt);
               

                Int32 count = 0;
                List<TransferCertificateReport> ObjLstTCR = new List<TransferCertificateReport>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        TransferCertificateReport ObjTC = new TransferCertificateReport();


                        ObjTC.PRN = (Convert.ToString(Dt.Rows[i]["PRN"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["PRN"]);

                        ObjTC.NameAsPerMarksheet = (Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"]);

                        ObjTC.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyName"]);

                        ObjTC.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);

                        ObjTC.TCCode = (Convert.ToString(Dt.Rows[i]["TCCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["TCCode"]);

                        ObjTC.FacultyId = (((Dt.Rows[i]["FacultyId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);

                        ObjTC.ProgrammeId = (((Dt.Rows[i]["ProgrammeId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);

                        ObjTC.CertiId = (((Dt.Rows[i]["CertiId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["CertiId"]);

                        ObjTC.CertificateName = (Convert.ToString(Dt.Rows[i]["CertificateName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["CertificateName"]);

                        



                        count = count + 1;
                        ObjTC.IndexId = count;

                        ObjLstTCR.Add(ObjTC);



                    }
                    return Return.returnHttp("200", ObjLstTCR, null);

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













    }
}
