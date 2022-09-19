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
    public class MandateController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

        #region  MandateGet
        [HttpPost]
        public HttpResponseMessage MandateGet(Mandate ObjMand)
        {
            Mandate modelobj = new Mandate();
            try
            {
                modelobj.ApplicationId = ObjMand.ApplicationId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("MandateGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ApplicationId", modelobj.ApplicationId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<Mandate> ObjLstMandt = new List<Mandate>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Mandate ObjMandt = new Mandate();
                        ObjMandt.Id = Convert.ToInt64(dr["Id"].ToString());
                        ObjMandt.FirstName = (dr["FirstName"].ToString());
                        ObjMandt.MiddleName = (dr["MiddleName"].ToString());
                        ObjMandt.LastName = (dr["LastName"].ToString());
                        ObjMandt.InstancePartTermName = (dr["InstancePartTermName"].ToString());
                        ObjMandt.FacultyName = (dr["FacultyName"].ToString());
                        ObjMandt.MobileNo = (dr["MobileNo"].ToString());
                        ObjMandt.AcademicYear = (dr["AcademicYearCode"].ToString());
                        ObjLstMandt.Add(ObjMandt);
                    }

                }
                return Return.returnHttp("200", ObjLstMandt, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion
    }
}
