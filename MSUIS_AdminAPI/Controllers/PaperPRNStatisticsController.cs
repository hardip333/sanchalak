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
    public class PaperPRNStatisticsController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

        #region PaperPRNStatisticsGetByAcademics
        [HttpPost]
        public HttpResponseMessage PaperPRNStatisticsGetByAcademics(PaperPRNStatistics ObjPPRNStat)
        {
            ApplicationStatistics modelobj = new ApplicationStatistics();

            try
            {
                modelobj.InstituteId = ObjPPRNStat.InstituteId;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("PaperPRNStatisticsGetByAcademics", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InstituteId", modelobj.InstituteId);

                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<PaperPRNStatistics> ObjLstPPRN = new List<PaperPRNStatistics>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        PaperPRNStatistics ObjPPRN = new PaperPRNStatistics();

                        //ObjAS.ProgrammeInstancePartTermId = Convert.ToInt32(dr["ProgrammeInstancePartTermId"]);

                        var ProgrammeInstancePartTermId = dr["ProgrammeInstancePartTermId"];
                        if (ProgrammeInstancePartTermId is DBNull)
                        {
                            ProgrammeInstancePartTermId = 0;
                            ObjPPRN.ProgrammeInstancePartTermId = Convert.ToInt32(ProgrammeInstancePartTermId);

                        }
                        else
                        {
                            ObjPPRN.ProgrammeInstancePartTermId = Convert.ToInt32(dr["ProgrammeInstancePartTermId"]);

                        }
                        ObjPPRN.InstancePartTermName = (dr["InstancePartTermName"].ToString());
                        var Intake = dr["Intake"];
                        if (Intake is DBNull) { }
                        else { ObjPPRN.Intake = Convert.ToInt32(dr["Intake"]); }
                        ObjPPRN.AdmittedStudent = Convert.ToInt32(dr["AdmittedStudent"]);
                        ObjPPRN.PrnGenerated = Convert.ToInt32(dr["PrnGenerated"]);
                        ObjPPRN.PaperSelected = Convert.ToInt32(dr["PaperSelected"]);
                        ObjPPRN.PaperPendingSelection = Convert.ToInt32(dr["PaperPendingSelection"]);

                        count = count + 1;
                        ObjPPRN.IndexId = count;
                        ObjLstPPRN.Add(ObjPPRN);
                    }

                }
                return Return.returnHttp("200", ObjLstPPRN, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region MstInstituteGet
        [HttpPost]
        public HttpResponseMessage MstInstituteGet()
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
                SqlCommand cmd = new SqlCommand("MstInstituteGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<PaperPRNStatistics> ObjLstInst = new List<PaperPRNStatistics>();
                dt.Rows.Add("0", "All Institute");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        PaperPRNStatistics ObjInst = new PaperPRNStatistics();
                        ObjInst.Id = Convert.ToInt32(dr["Id"]);
                        ObjInst.InstituteName = (dr["InstituteName"].ToString());

                        ObjLstInst.Add(ObjInst);
                    }

                }

                return Return.returnHttp("200", ObjLstInst, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region MstInstituteGetInstituteNameById
        [HttpPost]
        public HttpResponseMessage MstInstituteGetInstNameById(PaperPRNStatistics ObjAppStat)
        {
            ApplicationStatistics modelobj = new ApplicationStatistics();
            try
            {
                modelobj.InstituteId = ObjAppStat.InstituteId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                SqlCommand cmd = new SqlCommand("MstInstituteGetInstituteNameById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InstituteId", modelobj.InstituteId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<PaperPRNStatistics> ObjLstInst = new List<PaperPRNStatistics>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        PaperPRNStatistics ObjInst = new PaperPRNStatistics();
                        ObjInst.Id = Convert.ToInt32(dr["Id"]);
                        ObjInst.InstituteName = (dr["InstituteName"].ToString());

                        ObjLstInst.Add(ObjInst);
                    }

                }

                return Return.returnHttp("200", ObjLstInst, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region IncAcademicYearCodeGetById
        [HttpPost]
        public HttpResponseMessage IncAcadYearCodeGetById(PaperPRNStatistics ObjAppStat)
        {
            PaperPRNStatistics modelobj = new PaperPRNStatistics();
            try
            {
                modelobj.AcademicYearId = ObjAppStat.AcademicYearId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                SqlCommand cmd = new SqlCommand("IncAcademicYearCodeGetById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", modelobj.AcademicYearId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<PaperPRNStatistics> ObjLstInst = new List<PaperPRNStatistics>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        PaperPRNStatistics ObjInst = new PaperPRNStatistics();
                        ObjInst.Id = Convert.ToInt32(dr["Id"]);
                        ObjInst.AcademicYearCode = (dr["AcademicYearCode"].ToString());

                        ObjLstInst.Add(ObjInst);
                    }

                }

                return Return.returnHttp("200", ObjLstInst, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region PaperPRNStatisticsGetByFacultyId
        [HttpPost]
        public HttpResponseMessage PaperPRNStatisticsGetByFacultyId(PaperPRNStatistics ObjPPRNStat)
        {
            ApplicationStatistics modelobj = new ApplicationStatistics();

            try
            {
                modelobj.InstituteId = ObjPPRNStat.InstituteId;
                modelobj.ProgrammeInstancePartTermId = ObjPPRNStat.ProgrammeInstancePartTermId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("PaperPRNStatisticsGetByFacultyId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InstituteId", modelobj.InstituteId);
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", modelobj.ProgrammeInstancePartTermId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<PaperPRNStatistics> ObjLstPPRN = new List<PaperPRNStatistics>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        PaperPRNStatistics ObjPPRN = new PaperPRNStatistics();

                        //ObjAS.ProgrammeInstancePartTermId = Convert.ToInt32(dr["ProgrammeInstancePartTermId"]);

                        var ProgrammeInstancePartTermId = dr["ProgrammeInstancePartTermId"];
                        if (ProgrammeInstancePartTermId is DBNull)
                        {
                            ProgrammeInstancePartTermId = 0;
                            ObjPPRN.ProgrammeInstancePartTermId = Convert.ToInt32(ProgrammeInstancePartTermId);

                        }
                        else
                        {
                            ObjPPRN.ProgrammeInstancePartTermId = Convert.ToInt32(dr["ProgrammeInstancePartTermId"]);

                        }
                        ObjPPRN.InstancePartTermName = (dr["InstancePartTermName"].ToString());
                        var Intake = dr["Intake"];
                        if (Intake is DBNull) { }
                        else { ObjPPRN.Intake = Convert.ToInt32(dr["Intake"]); }

                        ObjPPRN.AdmittedStudent = Convert.ToInt32(dr["AdmittedStudent"]);
                        ObjPPRN.PrnGenerated = Convert.ToInt32(dr["PrnGenerated"]);
                        ObjPPRN.PaperSelected = Convert.ToInt32(dr["PaperSelected"]);
                        ObjPPRN.PaperPendingSelection = Convert.ToInt32(dr["PaperPendingSelection"]);

                        count = count + 1;
                        ObjPPRN.IndexId = count;
                        ObjLstPPRN.Add(ObjPPRN);
                    }

                }
                return Return.returnHttp("200", ObjLstPPRN, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion
    }
}