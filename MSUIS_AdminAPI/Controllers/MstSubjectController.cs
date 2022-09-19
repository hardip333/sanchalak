using MSUIS_TokenManager.App_Start;
using MSUISApi.BAL;
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
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class MstSubjectController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region Subject Get
        [HttpPost]
        public HttpResponseMessage MstSubjectGet()
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
                List<MstSubject> ObjLstSubject = new List<MstSubject>();
                BALSubjectList ObjBalSubjectList = new BALSubjectList();
                ObjLstSubject = ObjBalSubjectList.SubjectListGet();

                if (ObjLstSubject != null)
                    return Return.returnHttp("200", ObjLstSubject, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);                
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Subject Get For Drop Down
        [HttpPost]
        public HttpResponseMessage MstSubjectGetForDropDown()
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
                List<MstSubject> ObjLstSubject = new List<MstSubject>();
                BALSubjectList ObjBalSubjectList = new BALSubjectList();
                ObjLstSubject = ObjBalSubjectList.SubjectListGetForDropDown();

                if (ObjLstSubject != null)
                    return Return.returnHttp("200", ObjLstSubject, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Subject Add
        [HttpPost]
        public HttpResponseMessage MstSubjectAdd(MstSubject Subject)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            if (String.IsNullOrWhiteSpace(Convert.ToString(Subject.SubjectName)))
            {
                return Return.returnHttp("201", "Please enter subject name", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(Subject.FacultyId)))
            {
                return Return.returnHttp("201", "Please select Faculty", null);
            }
            else
            {
                try
                {
                    /* BoardOfStudy BoS = new BoardOfStudy();
                     MstSubject sub = new MstSubject();
                     dynamic Jsondata = Subject;
                     JObject BoSjson = Jsondata.Bos;
                     BoS = BoSjson.ToObject<BoardOfStudy>();*/

                    String SubjectName = Convert.ToString(Subject.SubjectName);
                    Int64 FacultyId = Convert.ToInt64(Subject.FacultyId);

                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("MstSubjectAdd", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SubjectName", SubjectName);
                    cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);

                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);

                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    Con.Open();

                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);

                    Con.Close();
                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Your data has been added successfully.";
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

        #region Subject Edit
        [HttpPost]
        public HttpResponseMessage MstSubjectEdit(MstSubject Subject)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            if (String.IsNullOrWhiteSpace(Convert.ToString(Subject.SubjectName)))
            {
                return Return.returnHttp("201", "Please enter subject name", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(Subject.FacultyId)))
            {
                return Return.returnHttp("201", "Please select Faculty", null);
            }
            else
            {
                try
                {
                    /*BoardOfStudy BoS = new BoardOfStudy();
                    MstSubject sub = new MstSubject();

                    dynamic Jsondata = Subject;
                    JObject BoSjson = Jsondata.Bos;

                    BoS = BoSjson.ToObject<BoardOfStudy>();*/

                    Int64 Id = Convert.ToInt64(Subject.Id);
                    String SubjectName = Convert.ToString(Subject.SubjectName);
                    Int64 FacultyId = Convert.ToInt64(Subject.FacultyId);

                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("MstSubjectEdit", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@SubjectName", SubjectName);
                    cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);

                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    Con.Open();

                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);

                    Con.Close();
                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Your data has been modified successfully.";
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

        #region Subject Delete
        [HttpPost]
        public HttpResponseMessage MstSubjectDelete(MstSubject Subject)
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
                /*BoardOfStudy BoS = new BoardOfStudy();
                MstSubject sub = new MstSubject();

                dynamic Jsondata = Subject;
                JObject BoSjson = Jsondata.Bos;

                BoS = BoSjson.ToObject<BoardOfStudy>();*/

                Int64 Id = Convert.ToInt64(Subject.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("MstSubjectDelete", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);

                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);

                Con.Close();
                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been deleted successfully.";
                }

                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region Subject Get By Id
        [HttpPost]
        public HttpResponseMessage MstSubjectGetbyId(MstSubject Subject)
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
                MstSubject ObjLstSubject = new MstSubject();
                BALSubjectList ObjBalSubjectList = new BALSubjectList();
                ObjLstSubject = ObjBalSubjectList.SubjectGetbyId(Subject.Id);

                if (ObjLstSubject != null)
                    return Return.returnHttp("200", ObjLstSubject, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region Subject Suspend
        [HttpPost]
        public HttpResponseMessage MstSubjectIsSuspended(MstSubject Subject)
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
                Con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString;
           
                Int64 Id = Convert.ToInt64(Subject.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("MstSubjectActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstSubjectIsActiveDisable");
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);

                cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@MESSAGE"].Value);
                Con.Close();
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

        #region Subject Active
        [HttpPost]
        public HttpResponseMessage MstSubjectIsActive(MstSubject Subject)
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
                Con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString;
 
                Int64 Id = Convert.ToInt64(Subject.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("MstSubjectActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstSubjectIsActiveEnable");
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);

                cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
                cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@MESSAGE"].Value);
                Con.Close();

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

        #region Subject Get by FacultyId
        [HttpPost]
        public HttpResponseMessage MstSubjectGetbyFacultyId(MstSubject subject)
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
                List<MstSubject> ObjLstSubject = new List<MstSubject>();
                BALSubjectList ObjBalSubjectList = new BALSubjectList();
                ObjLstSubject = ObjBalSubjectList.SubjectListGetByFacultyId(subject.FacultyId);

                if (ObjLstSubject != null)
                    return Return.returnHttp("200", ObjLstSubject, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion        
    }
}
