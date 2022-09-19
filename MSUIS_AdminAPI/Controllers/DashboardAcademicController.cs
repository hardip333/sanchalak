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


namespace MSUISApi.Controllers
{
    public class DashboardAcademicController : ApiController
    {
       
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            #region MstProgrammeCountAcasemicGet
            [HttpPost]
            public HttpResponseMessage MstProgrammeCountAcademicGet(PreConfigurationAdmission pca)
            {
                try
                {
                   //Int32 InstituteId = Convert.ToInt32(pca.InstituteId);
                    SqlCommand Cmd = new SqlCommand("PreConfigMstProgrammeCountAcademicGet", con);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    //Cmd.Parameters.AddWithValue("@InstituteId", InstituteId);
                    da.SelectCommand = Cmd;
                    da.Fill(dt);

                    List<PreConfigurationAdmission> ObjLstpca = new List<PreConfigurationAdmission>();

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            PreConfigurationAdmission Objpca = new PreConfigurationAdmission();

                            Objpca.TotalProgramme = Convert.ToInt32(dt.Rows[i]["TotalProgramme"]);

                            ObjLstpca.Add(Objpca);
                        }
                    }
                    return Return.returnHttp("200", ObjLstpca, null);
                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message, null);
                }
            }
        #endregion

        #region AdmApplicantsCountAcademicGet


        [HttpPost]
            public HttpResponseMessage AdmApplicantsCountAcademicGet(PreConfigurationAdmission pca)
            {
                try
                {

                   // Int32 InstituteId = Convert.ToInt32(pca.InstituteId);
                    SqlCommand Cmd = new SqlCommand("PreConfigAdmApplicationCountAcademicGet", con);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    //Cmd.Parameters.AddWithValue("@InstituteId", InstituteId);
                    da.SelectCommand = Cmd;
                    da.Fill(dt);

                    List<PreConfigurationAdmission> ObjLstpca = new List<PreConfigurationAdmission>();

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            PreConfigurationAdmission Objpca = new PreConfigurationAdmission();

                            Objpca.TotalApplicants = Convert.ToInt32(dt.Rows[i]["TotalApplicants"]);

                            ObjLstpca.Add(Objpca);
                        }
                    }
                    return Return.returnHttp("200", ObjLstpca, null);
                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message, null);
                }
            }
        #endregion

        #region AdmApprovedApplicantsCountAcademicGet

        [HttpPost]
            public HttpResponseMessage AdmApprovedApplicantsCountAcademicGet(PreConfigurationAdmission pca)
            {
                try
                {


                    //Int32 InstituteId = Convert.ToInt32(pca.InstituteId);
                    SqlCommand Cmd = new SqlCommand("PreConfigAdmApplicationApprovedCountAcademicGet", con);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    //Cmd.Parameters.AddWithValue("@InstituteId", InstituteId);

                    da.SelectCommand = Cmd;
                    da.Fill(dt);

                    List<PreConfigurationAdmission> ObjLstpca = new List<PreConfigurationAdmission>();

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            PreConfigurationAdmission Objpca = new PreConfigurationAdmission();

                            Objpca.TotalApplicantsApproved = Convert.ToInt32(dt.Rows[i]["TotalApplicantsApproved"]);

                            ObjLstpca.Add(Objpca);
                        }
                    }
                    return Return.returnHttp("200", ObjLstpca, null);
                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message, null);
                }
            }
            #endregion

            #region PreConfigAdmApprovedApplicationFeesPaidCountGet


            [HttpPost]
            public HttpResponseMessage AdmApplicationFeesPaidCountAcademicGet(PreConfigurationAdmission pca)
            {
                try
                {

                    //Int32 InstituteId = Convert.ToInt32(pca.InstituteId);
                    SqlCommand Cmd = new SqlCommand("PreConfigAdmApplicationFeesPaidCountAcademicGet", con);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    //Cmd.Parameters.AddWithValue("@InstituteId", InstituteId);
                    da.SelectCommand = Cmd;
                    da.Fill(dt);

                    List<PreConfigurationAdmission> ObjLstpca = new List<PreConfigurationAdmission>();

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            PreConfigurationAdmission Objpca = new PreConfigurationAdmission();

                            Objpca.TotalApplicantsFeesPaid = Convert.ToInt32(dt.Rows[i]["TotalApplicantsFeesPaid"]);

                            ObjLstpca.Add(Objpca);
                        }
                    }
                    return Return.returnHttp("200", ObjLstpca, null);
                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message, null);
                }
            }
            #endregion

        
    }
}
