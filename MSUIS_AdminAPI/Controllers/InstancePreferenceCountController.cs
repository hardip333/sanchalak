using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class InstancePreferenceCountController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();


        #region InstancePreferenceCountGet
        [HttpPost]
        public HttpResponseMessage InstancePreferenceCountGet(InstancePreferenceCount IPC)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            try
            {
                SqlCommand cmd = new SqlCommand("InstancePreferenceCountGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@FacultyId", IPC.FacultyId);
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<InstancePreferenceCount> ObjListIPC = new List<InstancePreferenceCount>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        InstancePreferenceCount ObIPC = new InstancePreferenceCount();
                        ObIPC.Id = Convert.ToInt32(dr["Id"].ToString());
                        ObIPC.InstancePartTermId = Convert.ToInt64(dr["InstancePartTermId"].ToString());
                        ObIPC.MinNoOfPreference = Convert.ToInt16(dr["MinNoOfPreference"].ToString());
                        ObIPC.FacultyId = Convert.ToInt32(dr["FacultyId"].ToString());
                        ObIPC.FacultyName = Convert.ToString(dr["FacultyName"].ToString());
                        ObIPC.InstancePartTermName = Convert.ToString(dr["InstancePartTermName"].ToString());
                        ObIPC.IsDeleted = (Convert.ToString(dr["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(dr["IsDeleted"]);

                        ObjListIPC.Add(ObIPC);
                    }
                }
                return Return.returnHttp("200", ObjListIPC, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region InstancePreferenceCountAdd
        [HttpPost]
        public HttpResponseMessage InstancePreferenceCountAdd(InstancePreferenceCount IPC)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            if (String.IsNullOrEmpty(Convert.ToString(IPC.InstancePartTermId)))
            {
                return Return.returnHttp("201", "Please Enter InstancePartTermId", null);
            }
            else
            if (String.IsNullOrWhiteSpace(Convert.ToString(IPC.MinNoOfPreference)))
            {
                return Return.returnHttp("201", "Please Enter SpecializationName", null);
            }
            else
            {
                try
                {
                    Int64 InstancePartTermId = Convert.ToInt64(IPC.InstancePartTermId);
                    Int16 MinNoOfPreference = Convert.ToInt16(IPC.MinNoOfPreference);
                    Int64 UserId = 1; //Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("InstancePreferenceCountAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@InstancePartTermId", InstancePartTermId);
                    cmd.Parameters.AddWithValue("@MinNoOfPreference", MinNoOfPreference);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    con.Close();
                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Your data has been Added Successfully.";
                    }
                    return Return.returnHttp("200", strMessage.ToString(), null);
                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message.ToString(), null);
                }
            }
        }
        #endregion

        #region InstancePreferenceCountUpdate
        [HttpPost]
        public HttpResponseMessage InstancePreferenceCountUpdate(InstancePreferenceCount IPC)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            if (String.IsNullOrEmpty(Convert.ToString(IPC.InstancePartTermId)))
            {
                return Return.returnHttp("201", "Please Enter InstancePartTermId", null);
            }
            else
            if (String.IsNullOrWhiteSpace(Convert.ToString(IPC.MinNoOfPreference)))
            {
                return Return.returnHttp("201", "Please Enter SpecializationName", null);
            }
            else
            {
                try
                {
                    Int32 Id = IPC.Id;
                    Int64 InstancePartTermId = Convert.ToInt64(IPC.InstancePartTermId);
                    Int16 MinNoOfPreference = Convert.ToInt16(IPC.MinNoOfPreference);
                    Int64 UserId = 1; //Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("InstancePreferenceCountEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@InstancePartTermId", InstancePartTermId);
                    cmd.Parameters.AddWithValue("@MinNoOfPreference", MinNoOfPreference);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    //cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);

                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    con.Close();

                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Your data has been Modified Successfully.";
                    }

                    return Return.returnHttp("200", strMessage.ToString(), null);
                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message.ToString(), null);
                }
            }
        }
        #endregion

        #region InstancePreferenceCountDelete
        [HttpPost]
        public HttpResponseMessage InstancePreferenceCountDelete(InstancePreferenceCount IPC)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int64 Id = IPC.Id;
                //Int64 UserId = 1234;


                SqlCommand cmd = new SqlCommand("InstancePreferenceCountDelete", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been Deleted Successfully.";
                }

                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion


        #region PostProgPartTermGetByFacultyId
        [HttpPost]
        public HttpResponseMessage ProgPartTermGetByFacultyId(ProgrammeInstancePartTerm ObjProgInstPartTerm)
        {
            try
            {



                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("IncProgrammeInstancePartTermGetByFId", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Id", ObjProgInstPartTerm.Id);
                DataTable dt = new DataTable();
                da.Fill(dt);



                List<ProgrammeInstancePartTerm> ObjLstProgInstPartTerm = new List<ProgrammeInstancePartTerm>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePartTerm ObjProgPartTerm = new ProgrammeInstancePartTerm();
                        ObjProgPartTerm.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        ObjProgPartTerm.InstancePartTermName = Convert.ToString(dt.Rows[i]["InstancePartTermName"]);



                        ObjLstProgInstPartTerm.Add(ObjProgPartTerm);



                    }
                    return Return.returnHttp("200", ObjLstProgInstPartTerm, null);
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



        #region Faculty Get By Id
        [HttpPost]
        public HttpResponseMessage MstFacultyGetbyId(MstFaculty Faculty)
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

            try
            {
                //dynamic Jsondata = Faculty;

                // Int32 Id = Convert.ToInt32(Faculty.Id);


                SqlCommand cmd = new SqlCommand("MstFacultyGetTest", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", facultyDepartIntituteId);
                List<MstFaculty> ObjLstFaculty = new List<MstFaculty>();
                sda.SelectCommand = cmd;
                DataTable Dt = new DataTable();
                sda.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstFaculty objFaculty = new MstFaculty();

                        objFaculty.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objFaculty.FacultyName = Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objFaculty.FacultyCode = Convert.ToString(Dt.Rows[i]["FacultyCode"]);
                        objFaculty.InstituteId = Convert.ToInt32(Dt.Rows[i]["InstituteId"]);
                        objFaculty.InstituteName = Convert.ToString(Dt.Rows[i]["InstituteName"]);
                        objFaculty.FacultyCode = Convert.ToString(Dt.Rows[i]["InstituteCode"]);
                        //objFaculty.FacultyAddress = Convert.ToString(Dt.Rows[i]["FacultyAddress"]);
                        //objFaculty.CityName = Convert.ToString(Dt.Rows[i]["CityName"]);
                        //objFaculty.Pincode = Convert.ToInt32(Dt.Rows[i]["Pincode"]);
                        //objFaculty.FacultyContactNo = Convert.ToString(Dt.Rows[i]["FacultyContactNo"]);
                        //objFaculty.FacultyFaxNo = Convert.ToString(Dt.Rows[i]["FacultyFaxNo"]);
                        //objFaculty.FacultyEmail = Convert.ToString(Dt.Rows[i]["FacultyEmail"]);
                        //objFaculty.FacultyUrl = Convert.ToString(Dt.Rows[i]["FacultyUrl"]);

                        ObjLstFaculty.Add(objFaculty);
                    }
                }
                return Return.returnHttp("200", ObjLstFaculty, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion



    }
}