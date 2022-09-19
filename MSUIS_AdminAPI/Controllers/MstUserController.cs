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
    public class MstUserController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();
        #region AdmEligibilityGroupEdit
        [HttpPost]
        public HttpResponseMessage MstUserPasswordEdit(MstUser ObjMstUser)
        {
            MstUser modelobj = new MstUser();

            try
            {
               // modelobj.Id = Convert.ToInt32(ObjMstUser.Id);
                modelobj.ExistingPassword = ObjMstUser.ExistingPassword;
                modelobj.NewPassword = ObjMstUser.NewPassword;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                modelobj.Id = Convert.ToInt32(res);
                modelobj.ModifiedBy = Convert.ToInt32(res);
                //modelobj.ModifiedBy = 5;
                if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.ExistingPassword)))
                {
                    return Return.returnHttp("201", "Password is not null or empty", null);
                }
                else if (modelobj.Id <= 0)
                {
                    return Return.returnHttp("201", "Please Enter Id or valid Id", null);
                }
                else if (string.IsNullOrWhiteSpace(Convert.ToString(modelobj.NewPassword)))
                {
                    return Return.returnHttp("201", "Password is not null or empty", null);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("MstUserPasswordEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                   
                    cmd.Parameters.AddWithValue("@Id", modelobj.Id);
                    cmd.Parameters.AddWithValue("@ExistingPassword", (modelobj.ExistingPassword).Trim());
                    cmd.Parameters.AddWithValue("@NewPassword", (modelobj.NewPassword).Trim());
                    cmd.Parameters.AddWithValue("@ModifiedBy", modelobj.ModifiedBy);
                    //cmd.Parameters.AddWithValue("@UserId", UserId);

                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    con.Close();

                    //if (string.Equals(strMessage, "Password Changed"))
                    //{
                    //    strMessage = "Your data has been saved successfully.";
                    //}
                   

                        return Return.returnHttp("200", strMessage.ToString(), null);                   

                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

    }
}
