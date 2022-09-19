using Microsoft.Ajax.Utilities;
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
    public class PostApprovalListController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

        #region PostEligibleApprovalListGet

        [HttpPost]
        public HttpResponseMessage PostEligibleApprovalListGet(PostConfigurationAdmission ObjPostConfiguration)
        {
            PostConfigurationAdmission modelobj = new PostConfigurationAdmission();

            try
            {
                //modelobj.FacultyId = ObjPostConfiguration.FacultyId;
                modelobj.InstituteId = ObjPostConfiguration.InstituteId;
               //modelobj.ProgrammeInstancePartTermId = ObjPostConfiguration.ProgrammeInstancePartTermId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("PostEligibleApprovalList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@FacultyId", modelobj.FacultyId);
                cmd.Parameters.AddWithValue("@InstituteId", modelobj.InstituteId);
                //cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", modelobj.ProgrammeInstancePartTermId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<PostConfigurationAdmission> ObjLstPC = new List<PostConfigurationAdmission>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        PostConfigurationAdmission ObjPC = new PostConfigurationAdmission();
                        ObjPC.Id = Convert.ToInt64(dr["Id"]);
                        ObjPC.LastName = (dr["LastName"].ToString());
                        ObjPC.FirstName = (dr["FirstName"].ToString());
                        ObjPC.MiddleName = (dr["MiddleName"].ToString());
                        ObjPC.InstancePartTermName = (dr["InstancePartTermName"].ToString());
                        ObjPC.AdminRemarkByFaculty = (dr["AdminRemarkByFaculty"].ToString());
                        ObjPC.AdminRemarkByAcademics = (dr["AdminRemarkByAcademics"].ToString());
                        ObjPC.EligibilityStatus = (dr["EligibilityStatus"].ToString());
                        ObjPC.EligibilityByAcademics = (dr["EligibilityByAcademics"].ToString());
                        if (dr["IsAdmissionFeePaid"].ToString() != "")
                        {
                            ObjPC.IsAdmissionFeePaid = Convert.ToBoolean(dr["IsAdmissionFeePaid"].ToString());
                        }

                        ObjLstPC.Add(ObjPC);
                    }

                }
                return Return.returnHttp("200", ObjLstPC, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region PostApprovedStudentsListGet

        [HttpPost]
        public HttpResponseMessage PostApprovedStudentsListGet(PostConfigurationAdmission ObjPostConfiguration)
        {
            PostConfigurationAdmission modelobj = new PostConfigurationAdmission();

            try
            {
                //modelobj.FacultyId = ObjPostConfiguration.FacultyId;
                modelobj.InstituteId = ObjPostConfiguration.InstituteId;
                modelobj.ProgrammeInstancePartTermId = ObjPostConfiguration.ProgrammeInstancePartTermId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("PostApprovedStudentsList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@FacultyId", modelobj.FacultyId);
                cmd.Parameters.AddWithValue("@InstituteId", modelobj.InstituteId);
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", modelobj.ProgrammeInstancePartTermId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<PostConfigurationAdmission> ObjLstPC = new List<PostConfigurationAdmission>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        PostConfigurationAdmission ObjPC = new PostConfigurationAdmission();
                        ObjPC.Id = Convert.ToInt64(dr["Id"]);
                        ObjPC.LastName = (dr["LastName"].ToString());
                        ObjPC.FirstName = (dr["FirstName"].ToString());
                        ObjPC.MiddleName = (dr["MiddleName"].ToString());
                        ObjPC.InstancePartTermName = (dr["InstancePartTermName"].ToString());
                        ObjPC.AdminRemarkByFaculty = (dr["AdminRemarkByFaculty"].ToString());
                        ObjPC.AdminRemarkByAcademics = (dr["AdminRemarkByAcademics"].ToString());
                        ObjPC.EligibilityStatus = (dr["EligibilityStatus"].ToString());
                        ObjPC.EligibilityByAcademics = (dr["EligibilityByAcademics"].ToString());
                        if (dr["IsAdmissionFeePaid"].ToString() != "")
                        {
                            ObjPC.IsAdmissionFeePaid = Convert.ToBoolean(dr["IsAdmissionFeePaid"].ToString());
                        }

                        ObjLstPC.Add(ObjPC);
                    }

                }
                return Return.returnHttp("200", ObjLstPC, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

    }
}
