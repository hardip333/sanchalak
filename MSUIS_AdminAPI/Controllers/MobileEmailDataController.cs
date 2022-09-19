using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;
using Newtonsoft.Json.Linq;

namespace MSUISApi.Controllers
{
    public class MobileEmailDataController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlCommand cmd = new SqlCommand();

        #region MobileEmailDataGet
        [HttpPost]
        public HttpResponseMessage MobileEmailDataGet(MobileEmailData MED)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                string resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                Int64 ApplicationId = Convert.ToInt64(MED.ApplicationId);

                SqlCommand Cmd = new SqlCommand("MobileEmailDataGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;


                Cmd.Parameters.AddWithValue("@InstituteId", facultyDepartIntituteId);
                Cmd.Parameters.AddWithValue("@ApplicationId", ApplicationId);

                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                Int32 count = 0;
                List<MobileEmailData> ObjLstMED = new List<MobileEmailData>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MobileEmailData ObjMED = new MobileEmailData();


                        ObjMED.ApplicationId = (((Dt.Rows[i]["ApplicationId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["ApplicationId"]);

                        ObjMED.ProgrammeInstancePartTermId = (((Dt.Rows[i]["ProgrammeInstancePartTermId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["ProgrammeInstancePartTermId"]);

                        ObjMED.NameAsPerMarksheet = (Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["NameAsPerMarksheet"]);

                        ObjMED.EmailId = (Convert.ToString(Dt.Rows[i]["EmailId"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["EmailId"]);
                        
                        ObjMED.MobileNo = (Convert.ToString(Dt.Rows[i]["MobileNo"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["MobileNo"]);

                        ObjMED.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyName"]);

                        ObjMED.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["BranchName"]);

                        ObjMED.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);

                        ObjMED.InstancePartTermName = (Convert.ToString(Dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                        
                        ObjMED.InstituteName = (Convert.ToString(Dt.Rows[i]["InstituteName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstituteName"]);

                        ObjMED.GroupName = (Convert.ToString(Dt.Rows[i]["GroupName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["GroupName"]);

                        ObjMED.SEmailId = (Convert.ToString(Dt.Rows[i]["EmailId"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["SEmailId"]);

                        ObjMED.SMobileNo = (Convert.ToString(Dt.Rows[i]["SMobileNo"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["SMobileNo"]);


                        ObjLstMED.Add(ObjMED);



                    }
                    return Return.returnHttp("200", ObjLstMED, null);

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


        #region MobileEmailDataEdit
        [HttpPost]
        public HttpResponseMessage MobileEmailDataEdit(MobileEmailUpdateRequest MER)
        {
            try
            {

                SqlTransaction ST;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String typePrefix = Request.Headers.GetValues("typePrefix").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                string resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                if (typePrefix == "INS")
                {


                    if (res == "0")
                    {
                        return Return.returnHttp("0", null, null);
                    }
                    if (String.IsNullOrWhiteSpace(Convert.ToString(MER.SMobileNo)))
                    {
                        return Return.returnHttp("201", "Please Enter New Mobile No.", null);
                    }
                    else
                    if (String.IsNullOrWhiteSpace(Convert.ToString(MER.SEmailId)))
                    {
                        return Return.returnHttp("201", "Please Enter New Mobile No.", null);
                    }
                    else
                    {


                        Int64 ApplicationId = Convert.ToInt64(MER.ApplicationId);
                        string SMobileNo = Convert.ToString(MER.SMobileNo);
                        string SEmailId = Convert.ToString(MER.SEmailId);
                        //string OldMobileNo = Convert.ToString(MER.OldMobileNo);
                        //string NewMobileNo = Convert.ToString(MER.NewMobileNo);
                        //string OldEmailId = Convert.ToString(MER.OldEmailId);
                        //string NewEmailId = Convert.ToString(MER.NewEmailId);
                        Int64 UserId = Convert.ToInt64(res.ToString());


                        SqlCommand Cmd = new SqlCommand("MobileEmailDataUpdate", Con);
                        Cmd.CommandType = CommandType.StoredProcedure;

                        //Cmd.Parameters.AddWithValue("@InstituteId", facultyDepartIntituteId);

                        Cmd.Parameters.AddWithValue("@ApplicationId", ApplicationId);
                        Cmd.Parameters.AddWithValue("@MobileNo", SMobileNo);
                        Cmd.Parameters.AddWithValue("@EmailId", SEmailId);
                        //Cmd.Parameters.AddWithValue("@OldMobileNo", OldMobileNo);
                        //Cmd.Parameters.AddWithValue("@NewMobileNo", NewMobileNo);
                        //Cmd.Parameters.AddWithValue("@OldEmailId", OldEmailId);
                        //Cmd.Parameters.AddWithValue("@NewEmailId", NewEmailId);
                        Cmd.Parameters.AddWithValue("@UserId", UserId);
                        Cmd.Parameters.AddWithValue("@UserTime", datetime);
                        Cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                        Cmd.Parameters["@Message"].Direction = ParameterDirection.Output;


                        Con.Open();
                        ST = Con.BeginTransaction();

                        try
                        {
                            Cmd.Transaction = ST;
                            Cmd.ExecuteNonQuery();
                            string strMessage = Convert.ToString(Cmd.Parameters["@Message"].Value);
                            ST.Commit();
                            if (string.Equals(strMessage, "TRUE"))
                            {
                                strMessage = "Your data has been Updated.";
                            }
                            return Return.returnHttp("200", strMessage.ToString(), null);
                        }
                        catch (SqlException sqlError)
                        {
                            ST.Rollback();
                            String strMessageErr = Convert.ToString(sqlError);
                            Con.Close();
                            return Return.returnHttp("201", strMessageErr, null);

                        }
                        finally
                        {

                            Con.Close();


                        }
                    }
                }

                else
                {
                    return Return.returnHttp("200", "All Applicant are Blocked.", null);
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