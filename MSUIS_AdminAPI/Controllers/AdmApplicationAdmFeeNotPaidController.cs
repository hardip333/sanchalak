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
    public class AdmApplicationAdmFeeNotPaidController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

        #region  AdmApplicationAdmFeeNotPaidGet
        [HttpPost]
        public HttpResponseMessage AdmApplicationAdmFeeNotPaidGetByInstituteId(AdmApplicationAdmFeeNotPaid ObjAdmFeeNotPaid)
        {
            AdmApplicationAdmFeeNotPaid modelobj = new AdmApplicationAdmFeeNotPaid();
            
            try
            {
                modelobj.InstituteId = ObjAdmFeeNotPaid.InstituteId;
                modelobj.ProgrammeInstancePartTermId = ObjAdmFeeNotPaid.ProgrammeInstancePartTermId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("AdmApplicationAdmFeeNotPaidGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InstituteId", modelobj.InstituteId);
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", modelobj.ProgrammeInstancePartTermId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<AdmApplicationAdmFeeNotPaid> ObjLstAdmFee = new List<AdmApplicationAdmFeeNotPaid>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AdmApplicationAdmFeeNotPaid ObjAdmFee = new AdmApplicationAdmFeeNotPaid();
                        
                        ObjAdmFee.FirstName = (dr["FirstName"].ToString());
                        ObjAdmFee.MiddleName = (dr["MiddleName"].ToString());
                        ObjAdmFee.LastName = (dr["LastName"].ToString());
                        ObjAdmFee.InstancePartTermName = (dr["InstancePartTermName"].ToString());
                        ObjAdmFee.BranchName = (dr["BranchName"].ToString());
                        ObjAdmFee.InstituteName = (dr["InstituteName"].ToString());
                        ObjAdmFee.FeeCategoryName = (dr["FeeCategoryName"].ToString());
                        count = count + 1;
                        ObjAdmFee.IndexId = count;
                        ObjAdmFee.Name = ObjAdmFee.FirstName + " " + ObjAdmFee.MiddleName + " " + ObjAdmFee.LastName;

                        ObjLstAdmFee.Add(ObjAdmFee);
                    }

                }
                return Return.returnHttp("200", ObjLstAdmFee, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion
    }
}
