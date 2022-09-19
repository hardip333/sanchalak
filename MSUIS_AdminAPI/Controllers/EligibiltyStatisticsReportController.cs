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
namespace MSUISApi.Controllers
{
    public class EligibiltyStatisticsReportController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

        #region EligibilityStatisticsGet
        [HttpPost]
        public HttpResponseMessage EligibilityStatisticsGet(EligibiltyStatisticsReport ObjEligtat)
        {
            //EligibiltyStatisticsReport modelobj = new EligibiltyStatisticsReport();

            try
            {
                //modelobj.AcademicYearId = ObjEligtat.AcademicYearId;
                //modelobj.FacultyId = ObjEligtat.FacultyId;
                //modelobj.InstituteId = ObjEligtat.InstituteId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("EligibiltyStatisticsReportByAcademics", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AcademicYearId", ObjEligtat.AcademicYearId);
                cmd.Parameters.AddWithValue("@FacultyId", ObjEligtat.FacultyId);
                cmd.Parameters.AddWithValue("@InstituteId", ObjEligtat.InstituteId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<EligibiltyStatisticsReport> ObjLstES = new List<EligibiltyStatisticsReport>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        EligibiltyStatisticsReport ObjES = new EligibiltyStatisticsReport();

                        //ObjAS.ProgrammeInstancePartTermId = Convert.ToInt32(dr["ProgrammeInstancePartTermId"]);

                        var ProgrammeInstancePartTermId = dr["ProgrammeInstancePartTermId"];
                        if (ProgrammeInstancePartTermId is DBNull)
                        {
                            ProgrammeInstancePartTermId = 0;
                            ObjES.ProgrammeInstancePartTermId = Convert.ToInt32(ProgrammeInstancePartTermId);

                        }
                        else
                        {
                            ObjES.ProgrammeInstancePartTermId = Convert.ToInt32(dr["ProgrammeInstancePartTermId"]);

                        }
                        ObjES.AcademicYearId = Convert.ToInt32(dr["AcademicYearId"]);
                        ObjES.FacultyId = Convert.ToInt32(dr["FacultyId"]);
                        ObjES.InstancePartTermName = (dr["InstancePartTermName"].ToString());
                        ObjES.FacultyName = (dr["FacultyName"].ToString());
                        var Intake = dr["Intake"];
                        if (Intake is DBNull) { }
                        else { ObjES.Intake = Convert.ToInt32(dr["Intake"]); }
                        ObjES.AcademicYearCode = (dr["AcademicYearCode"].ToString());
                        ObjES.ProvisionallyEligible = Convert.ToInt32(dr["ProvisionallyEligible"]);
                        ObjES.Eligible = Convert.ToInt32(dr["Eligible"]);
                        ObjES.InstituteId = Convert.ToInt32(dr["InstituteId"]);
                        ObjES.InstituteName = (dr["InstituteName"].ToString());

                        count = count + 1;
                        ObjES.IndexId = count;
                        ObjLstES.Add(ObjES);
                    }

                }
                return Return.returnHttp("200", ObjLstES, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region Academic Year List
        [HttpPost]
        public HttpResponseMessage IncAcademicYearListGet()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter cmdda = new SqlDataAdapter("IncAcademicYearGet", con);

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
                        //ObjIncAcademicYear.FromDateView = Convert.ToString((Dt.Rows[i]["FromDate"]), System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"));
                        //ObjIncAcademicYear.ToDateView = Convert.ToString((Dt.Rows[i]["ToDate"]), System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"));
                        //DateTime FromDate = Convert.ToDateTime(Dt.Rows[i]["FromDate"]);
                        //ObjIncAcademicYear.FromDate = FromDate;
                        //DateTime ToDate = Convert.ToDateTime(Dt.Rows[i]["ToDate"]);
                        //ObjIncAcademicYear.ToDate = ToDate;
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
                sda.Fill(dt);
                List<MstFaculty> ObjLstF = new List<MstFaculty>();
                //dt.Rows.Add("0", "All Faculty");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstFaculty ObjF = new MstFaculty();
                        ObjF.Id = Convert.ToInt32(dr["Id"]);
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

        #region MstInstituteGetByFacultyId
        [HttpPost]
        public HttpResponseMessage MstInstituteGetByFacultyId(MstInstitute FIM)
        {
            //MstFaculty modelobj = new MstFaculty(); 
            try
            {
                //modelobj.Id = MF.Id;
                SqlCommand cmd = new SqlCommand("MstInstGetByFId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", FIM.FacultyId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<MstInstitute> ObjLstF = new List<MstInstitute>();
                //dt.Rows.Add("0", "All Faculty");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstInstitute ObjF = new MstInstitute();
                        ObjF.InstituteId = Convert.ToInt32(dr["Id"]);
                        ObjF.InstituteName = (dr["InstituteName"].ToString());

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
    }
}