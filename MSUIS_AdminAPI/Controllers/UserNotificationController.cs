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
using System.Net.Mail;
using System.Web.Http;

namespace MSUISApi.Controllers

{
    public class UserNotificationController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        #region UserNotificationGet
        [HttpPost]
        public HttpResponseMessage UserNotificationGet()
        {
            //String token = Request.Headers.GetValues("token").FirstOrDefault();
            //TokenOperation ac = new TokenOperation();

            //string res = ac.ValidateToken(token);

            //if (res == "0")
            //{
            //    return Return.returnHttp("0", null, null);
            //}
            try
            {
                
                SqlCommand cmd = new SqlCommand("UserNotificationGet", con);

                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<UserNotification> ObjUserNotList = new List<UserNotification>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        
                        UserNotification ObjUN = new UserNotification();

                        ObjUN.Id = Convert.ToInt64(dr["Id"].ToString());
                        ObjUN.FacultyId = Convert.ToInt32(dr["FacultyId"].ToString());
                        ObjUN.IncInstancePartTermId = Convert.ToInt64(dr["IncInstancePartTermId"].ToString());
                        ObjUN.ProgrammeId = Convert.ToInt32(dr["ProgrammeId"].ToString());
                        ObjUN.NotificationTypeId = Convert.ToInt32(dr["NotificationTypeId"].ToString());
                        ObjUN.FacultyName = dr["FacultyName"].ToString();
                        ObjUN.InstancePartTermName = dr["InstancePartTermName"].ToString();
                        ObjUN.ProgrammeName = dr["ProgrammeName"].ToString();
                        ObjUN.NotificationTypeName = dr["NotificationTypeName"].ToString();
                        ObjUN.NotificationDescription = dr["NotificationDescription"].ToString();
                        ObjUN.NotificationUserType = dr["NotificationUserType"].ToString();
                        ObjUN.NotificationFile = dr["NotificationFile"].ToString();
                        ObjUN.NotificationStartDate = Convert.ToDateTime(dr["NotificationStartDate"]);
                        ObjUN.NotificationEndDate = Convert.ToDateTime(dr["NotificationEndDate"]);
                        ObjUN.NotificationStartDateView = Convert.ToDateTime(dr["NotificationStartDate"]).ToShortDateString();
                        ObjUN.NotificationEndDateView = Convert.ToDateTime(dr["NotificationEndDate"]).ToShortDateString();
                        ObjUN.NotificationFor = dr["NotificationFor"].ToString();
                        ObjUN.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
                        ObjUN.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        

                        ObjUserNotList.Add(ObjUN);

                    }
                }
                return Return.returnHttp("200", ObjUserNotList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion


        #region UserNotificationAdd
        [HttpPost]
        public HttpResponseMessage UserNotificationAdd(UserNotification un)
        {
            //String token = Request.Headers.GetValues("token").FirstOrDefault();
            //TokenOperation ac = new TokenOperation();

            //string res = ac.ValidateToken(token);

            //if (res == "0")
            //{
            //    return Return.returnHttp("0", null, null);
            //}
            if (String.IsNullOrEmpty(Convert.ToString(un.FacultyId)))
            {
                return Return.returnHttp("201", "Please enter Faculty Name", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(un.IncInstancePartTermId)))
            {
                return Return.returnHttp("201", "Please select Query Description", null);
            }
            if (String.IsNullOrEmpty(Convert.ToString(un.ProgrammeId)))
            {
                return Return.returnHttp("201", "Please enter Query", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(un.NotificationTypeId)))
            {
                return Return.returnHttp("201", "Please select Query Description", null);
            }
            if (String.IsNullOrWhiteSpace(Convert.ToString(un.NotificationDescription)))
            {
                return Return.returnHttp("201", "Please Enter evaluation name", null);
            }
            if (String.IsNullOrEmpty(Convert.ToString(un.NotificationFor)))
            {
                return Return.returnHttp("201", "Please enter Faculty Name", null);
            }
            else
            {
                try
                    {
                     Int32 FacultyId = Convert.ToInt32(un.FacultyId);
                     Int64 IncInstancePartTermId = Convert.ToInt64(un.IncInstancePartTermId);
                     Int32 ProgrammeId = Convert.ToInt32(un.ProgrammeId);
                     Int32 NotificationTypeId = Convert.ToInt32(un.NotificationTypeId);
                     string NotificationDescription = Convert.ToString(un.NotificationDescription);
                     string NotificationUserType = Convert.ToString(un.NotificationUserType);
                     string NotificationFile = Convert.ToString(un.NotificationFile);
                     DateTime NotificationStartDate = TimeZoneInfo.ConvertTimeFromUtc(un.NotificationStartDate, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                     DateTime NotificationEndDate = TimeZoneInfo.ConvertTimeFromUtc(un.NotificationEndDate, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                     Boolean NotificationUserType1 = Convert.ToBoolean(un.NotificationUserType1);
                     Boolean NotificationUserType2 = Convert.ToBoolean(un.NotificationUserType2);
                     Boolean NotificationUserType3 = Convert.ToBoolean(un.NotificationUserType3);
                    string NotificationFor = Convert.ToString(un.NotificationFor);
                     Int64 UserId = 1234;

                    if(NotificationUserType1 ==true && NotificationUserType2 == false && NotificationUserType3 == false)
                    {
                        NotificationUserType = "All";
                    }
                    else if(NotificationUserType2 == true && NotificationUserType3 == false)
                    {
                        NotificationUserType = "Student";
                    }
                    else if (NotificationUserType2 == false && NotificationUserType3 == true)
                    {
                        NotificationUserType = "Faculty";
                    }
                    else if (NotificationUserType2 == true && NotificationUserType3 == true)
                    {
                        NotificationUserType = "Student";
                        NotificationUserType = "Faculty";
                    }
                    else
                    {
                        NotificationUserType = null;
                    }

                        SqlCommand cmd = new SqlCommand("UserNotificationAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                    cmd.Parameters.AddWithValue("@IncInstancePartTermId", IncInstancePartTermId);
                    cmd.Parameters.AddWithValue("@ProgrammeId", ProgrammeId);
                    cmd.Parameters.AddWithValue("@NotificationTypeId", NotificationTypeId);
                    cmd.Parameters.AddWithValue("@NotificationDescription", NotificationDescription);
                    cmd.Parameters.AddWithValue("@NotificationUserType", NotificationUserType);
                    cmd.Parameters.AddWithValue("@NotificationFile", NotificationFile);
                    cmd.Parameters.AddWithValue("@NotificationStartDate", NotificationStartDate);
                    cmd.Parameters.AddWithValue("@NotificationEndDate", NotificationEndDate);
                    cmd.Parameters.AddWithValue("@NotificationFor", NotificationFor);
                    //cmd.Parameters.AddWithValue("@NotificationUserType1", NotificationUserType1);
                    //.Parameters.AddWithValue("@NotificationUserType2", NotificationUserType2);
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
                        strMessage = "Your data has been saved successfully.";
                    }
                    return Return.returnHttp("200", strMessage.ToString(), null);
                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message, null);
                }
            }
        }
        #endregion

        #region UserNotificationUpdate
        [HttpPost]
        public HttpResponseMessage UserNotificationUpdate(UserNotification usern)
        {
            //String token = Request.Headers.GetValues("token").FirstOrDefault();
            //TokenOperation ac = new TokenOperation();

            //string res = ac.ValidateToken(token);

            //if (res == "0")
            //{
            //    return Return.returnHttp("0", null, null);
            //}
            //if (String.IsNullOrWhiteSpace(Convert.ToString(ev.EvaluationName)))
            //{
            //    return Return.returnHttp("201", "Please Enter evaluation name", null);
            //}
            if (String.IsNullOrEmpty(Convert.ToString(usern.FacultyId)))
            {
                return Return.returnHttp("201", "Please enter Faculty Name", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(usern.IncInstancePartTermId)))
            {
                return Return.returnHttp("201", "Please select Query Description", null);
            }
            if (String.IsNullOrEmpty(Convert.ToString(usern.ProgrammeId)))
            {
                return Return.returnHttp("201", "Please enter Query", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(usern.NotificationTypeId)))
            {
                return Return.returnHttp("201", "Please select Query Description", null);
            }
            if (String.IsNullOrWhiteSpace(Convert.ToString(usern.NotificationDescription)))
            {
                return Return.returnHttp("201", "Please Enter evaluation name", null);
            }
            if (String.IsNullOrEmpty(Convert.ToString(usern.NotificationFor)))
            {
                return Return.returnHttp("201", "Please enter Faculty Name", null);
            }
            else
            {
            try
            {
                Int64 Id = Convert.ToInt64(usern.Id);
                Int32 FacultyId = Convert.ToInt32(usern.FacultyId);
                Int64 IncInstancePartTermId = Convert.ToInt64(usern.IncInstancePartTermId);
                Int32 ProgrammeId = Convert.ToInt32(usern.ProgrammeId);
                Int32 NotificationTypeId = Convert.ToInt32(usern.NotificationTypeId);
                string NotificationDescription = Convert.ToString(usern.NotificationDescription);
                string NotificationUserType = Convert.ToString(usern.NotificationUserType);
                string NotificationFile = Convert.ToString(usern.NotificationFile);
                DateTime NotificationStartDate = TimeZoneInfo.ConvertTimeFromUtc(usern.NotificationStartDate, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                //Convert.ToDateTime(un.NotificationStartDate);
                DateTime NotificationEndDate = TimeZoneInfo.ConvertTimeFromUtc(usern.NotificationEndDate, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                Boolean NotificationUserType1 = Convert.ToBoolean(usern.NotificationUserType1);
                Boolean NotificationUserType2 = Convert.ToBoolean(usern.NotificationUserType2);
                Boolean NotificationUserType3 = Convert.ToBoolean(usern.NotificationUserType3);
                    string NotificationFor = Convert.ToString(usern.NotificationFor);
                Int64 UserId = 1;

                    if (NotificationUserType1 == true && NotificationUserType2 == false && NotificationUserType3 == false)
                    {
                        NotificationUserType = "All";
                    }
                    else if (NotificationUserType2 == true && NotificationUserType3 == false)
                    {
                        NotificationUserType = "Student";
                    }
                    else if (NotificationUserType2 == false && NotificationUserType3 == true)
                    {
                        NotificationUserType = "Faculty";
                    }
                    else if (NotificationUserType2 == true && NotificationUserType3 == true)
                    {
                        NotificationUserType = "Student";
                        NotificationUserType = "Faculty";
                    }
                    else
                    {
                        NotificationUserType = null;
                    }

                    SqlCommand cmd = new SqlCommand("UserNotificationEdit", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                cmd.Parameters.AddWithValue("@IncInstancePartTermId", IncInstancePartTermId);
                cmd.Parameters.AddWithValue("@ProgrammeId", ProgrammeId);
                cmd.Parameters.AddWithValue("@NotificationTypeId", NotificationTypeId);
                cmd.Parameters.AddWithValue("@NotificationDescription", NotificationDescription);
                cmd.Parameters.AddWithValue("@NotificationUserType", NotificationUserType);
                cmd.Parameters.AddWithValue("@NotificationFile", NotificationFile);
                cmd.Parameters.AddWithValue("@NotificationStartDate", NotificationStartDate);
                cmd.Parameters.AddWithValue("@NotificationEndDate", NotificationEndDate);
                cmd.Parameters.AddWithValue("@NotificationFor", NotificationFor);
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
                    strMessage = "Your data has been saved successfully.";
                    
                }

                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

            }
        }
        #endregion

        #region UserNotificationDelete
        [HttpPost]
        public HttpResponseMessage UserNotificationDelete(UserNotification unotif)
        {
            try
            {
                //String token = Request.Headers.GetValues("token").FirstOrDefault();
                //TokenOperation TokenOp = new TokenOperation();
                //String ValidTok = TokenOp.ValidateToken(token);
                //Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                //if (ValidTok == "0")
                //{
                //    return Return.returnHttp("0", null, null);
                //}
                Int64 Id = unotif.Id;
                string NotificationFile = Convert.ToString(unotif.NotificationFile);
                //int UserId = 1234;


                SqlCommand cmd = new SqlCommand("UserNotificationDelete", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
                //cmd.Parameters.AddWithValue("@UserId", UserId);
                //cmd.Parameters.AddWithValue("@UserTime", datetime);
                //cmd.Parameters.AddWithValue("@Flag", "MstSubSpecialisationDelete");
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been saved successfully.";
                    string sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/UserNotificationImageDocument/");
                    if (File.Exists(sPath + Path.GetFileName(NotificationFile)))
                    {
                        File.Delete(sPath + Path.GetFileName(NotificationFile));
                    }
                }

                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region UserNotificationIsSuspended
        [HttpPost]
        public HttpResponseMessage UserNotificationIsSuspended(UserNotification userNotification)
        {
            /*String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }*/
            try
            {

                /*MstExaminationPattern mstExaminationPattern = new MstExaminationPattern();
                dynamic jsonData = jobj;
                mstExaminationPattern.Id = jsonData.Id;*/
                Int64 Id = Convert.ToInt64(userNotification.Id);
                Int64 UserId = 1234;
                //Convert.ToInt64(res.ToString());
                SqlCommand cmd = new SqlCommand("UserNotificationActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "UserNotificationIsActiveDisable");
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);

                cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@MESSAGE"].Value);
                con.Close();
                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been saved successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region UserNotificationIsActive
        [HttpPost]
        public HttpResponseMessage UserNotificationIsActive(UserNotification userNotification)
        {
            /* String token = Request.Headers.GetValues("token").FirstOrDefault();
             TokenOperation ac = new TokenOperation();

             string res = ac.ValidateToken(token);

             if (res == "0")
             {
                 return Return.returnHttp("0", null, null);
             }*/
            try
            {

                Int64 Id = Convert.ToInt64(userNotification.Id);
                Int64 UserId = 1234;
                //Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("UserNotificationActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "UserNotificationIsActiveEnable");
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);

                cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@MESSAGE"].Value);
                con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been saved successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion


        #region FacultyGet
        [HttpPost]
        public HttpResponseMessage FacultyGet()
        {
            try
            {

                SqlCommand Cmd = new SqlCommand("MstFacultyGet", con);
                Cmd.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = Cmd;

                sda.Fill(Dt);


                List<MstFaculty> ObjLstFaculty = new List<MstFaculty>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstFaculty objFaculty = new MstFaculty();

                        objFaculty.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objFaculty.FacultyName = Convert.ToString(Dt.Rows[i]["FacultyName"]);

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

        #region MstProgrammeListGet
        [HttpPost]
        public HttpResponseMessage MstProgrammeListGet()
        {
            try
            {
                //String token = Request.Headers.GetValues("token").FirstOrDefault();
                //TokenOperation TokenOp = new TokenOperation();
                //String ValidTok = TokenOp.ValidateToken(token);


                //if (ValidTok == "0")
                //{
                //    return Return.returnHttp("0", null, null);
                //}
                
                SqlDataAdapter sda = new SqlDataAdapter("MstProgrammeGet", con);

                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                //sda.SelectCommand.Parameters.AddWithValue("@Flag", "MstProgrammeListGet");
                //sda.SelectCommand.Parameters.Add("@Message", SqlDbType.NVarChar).Value = "True";
                DataTable dt = new DataTable();
                sda.Fill(dt);

                List<MstProgramme> ProgrammeLists = new List<MstProgramme>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstProgramme prg = new MstProgramme();
                        prg.Id = Convert.ToInt32(dr["Id"].ToString());
                        prg.ProgrammeName = dr["ProgrammeName"].ToString();
                        prg.ProgrammeCode = dr["ProgrammeCode"].ToString();
                        prg.ProgrammeDescription = dr["ProgrammeDescription"].ToString();
                        prg.FacultyId = Convert.ToInt32(dr["FacultyId"].ToString());
                        prg.FacultyName = dr["FacultyName"].ToString();
                        prg.ProgrammeLevelId = Convert.ToInt32(dr["ProgrammeLevelId"].ToString());
                        prg.ProgrammeLevelName = dr["ProgrammeLevelName"].ToString();
                        prg.ProgrammeTypeId = Convert.ToInt32(dr["ProgrammeTypeId"].ToString());
                        prg.ProgrammeTypeName = dr["ProgrammeTypeName"].ToString();
                        prg.ProgrammeModeId = Convert.ToInt32(dr["ProgrammeModeId"].ToString());
                        prg.ProgrammeModeName = dr["ProgrammeModeName"].ToString();
                        prg.EvaluationId = Convert.ToInt32(dr["EvaluationId"].ToString());
                        prg.EvaluationName = dr["EvaluationName"].ToString();
                        prg.InstructionMediumId = Convert.ToInt32(dr["InstructionMediumId"].ToString());
                        prg.InstructionMediumName = dr["InstructionMediumName"].ToString();
                        prg.IsCBCS = Convert.ToBoolean(dr["IsCBCS"].ToString());
                        prg.IsSepartePassingHead = Convert.ToBoolean(dr["IsSepartePassingHead"].ToString());
                        prg.MaxMarks = Convert.ToInt32(dr["MaxMarks"].ToString());
                        prg.MinMarks = Convert.ToInt32(dr["MinMarks"].ToString());
                        prg.MaxCredits = Convert.ToDecimal(dr["MinMarks"].ToString());
                        prg.MinCredits = Convert.ToDecimal(dr["MinMarks"].ToString());
                        prg.IsActiveSts = dr["IsActiveSts"].ToString();
                        prg.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
                        prg.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());

                        ProgrammeLists.Add(prg);
                    }

                }
                return Return.returnHttp("200", ProgrammeLists, null);


            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion


        #region MstProgrammeGetByFacultyId
        [HttpPost]
        public HttpResponseMessage MstProgrammeGetByFacultyId(JObject BOS)
        {
            try
            {
                MstProgramme mprog = new MstProgramme();

                dynamic Jsondata = BOS;

                mprog.FacultyId = Convert.ToInt32(Jsondata.FacultyId);
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand("MstProgrammeGetByFacId", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", mprog.FacultyId);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = cmd;

                Da.Fill(Dt);

                List<MstProgramme> ObjLstProg = new List<MstProgramme>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgramme mstpro = new MstProgramme();

                        mstpro.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        mstpro.ProgrammeName = Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        //mstpro.FacultyId = Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        //mstpro.FacultyName = Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        ObjLstProg.Add(mstpro);
                    }
                }
                return Return.returnHttp("200", ObjLstProg, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion



        #region IncProgrammeInstancePartTermGet
        [HttpPost]
        public HttpResponseMessage IncProgrammeInstancePartTermGet()
        {
            try
            {

                SqlCommand Cmd = new SqlCommand("IncProgrammeInstancePartTermGet", con);
                Cmd.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = Cmd;

                sda.Fill(Dt);


                List<ProgrammeInstancePartTerm> ObjLstFaculty = new List<ProgrammeInstancePartTerm>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePartTerm objIPIPT = new ProgrammeInstancePartTerm();

                        objIPIPT.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objIPIPT.InstancePartTermName = Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);

                        ObjLstFaculty.Add(objIPIPT);
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


        #region PostProgPartTermGetByFacultyId
        [HttpPost]
        public HttpResponseMessage ProgPartTermGetByFacultyId(ProgrammeInstancePartTerm ObjProgInstPartTerm)
        {
            try
            {



                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("IncProgrammeInstancePartTermGetByFacultyId", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@FacultyId", ObjProgInstPartTerm.FacultyId);
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


        #region NotificationTypeGet
        [HttpPost]
        public HttpResponseMessage NotificationTypeGet()
        {
            //String token = Request.Headers.GetValues("token").FirstOrDefault();
            //TokenOperation ac = new TokenOperation();

            //string res = ac.ValidateToken(token);

            //if (res == "0")
            //{
            //    return Return.returnHttp("0", null, null);
            //}
            try
            {
                SqlCommand cmd = new SqlCommand("NotificationTypeGet", con);

                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<NotificationType> ObjNotTypeLists = new List<NotificationType>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        NotificationType ObjNT = new NotificationType();
                        ObjNT.Id = Convert.ToInt32(dr["Id"].ToString());
                        ObjNT.NotificationTypeName = (Convert.ToString(dr["NotificationTypeName"]));
                        ObjNT.IsActive = (Convert.ToBoolean(dr["IsActive"]));
                        ObjNT.IsDeleted = (Convert.ToBoolean(dr["IsDeleted"]));
                        ObjNT.IsActiveSts = (Convert.ToString(dr["IsActiveSts"]));
                        ObjNotTypeLists.Add(ObjNT);
                    }
                }
                return Return.returnHttp("200", ObjNotTypeLists, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion


        #region UploadInstituteQueryDocument
        [HttpPost()]
        public HttpResponseMessage UploadUserNotificationDocument()
        {
            string token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation tokenOperation = new TokenOperation();
            string responseToken = tokenOperation.ValidateToken(token);
            if (responseToken == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            string renameFile = Request.Headers.GetValues("UserNotificationImageDoc").FirstOrDefault();

            int iUploadedCnt = 0;
            // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
            string sPath = "";
            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/UserNotificationImageDocument/");

            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            // CHECK THE FILE COUNT.
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];

                if (hpf.ContentLength > 0)
                {
                    if (File.Exists(sPath + Path.GetFileName(renameFile)))
                    {
                        File.Delete(sPath + Path.GetFileName(renameFile));
                    }
                    hpf.SaveAs(sPath + Path.GetFileName(renameFile));
                    iUploadedCnt = iUploadedCnt + 1;
                }
            }
            if (iUploadedCnt > 0)
            {
                return Return.returnHttp("200", iUploadedCnt + " Files Uploaded Successfully", null);
            }
            else
            {
                return Return.returnHttp("201", iUploadedCnt + "Upload Failed", null);
            }

        }
        #endregion

    }
}