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
    public class MstFacultyController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();

        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region Faculty Get
        [HttpPost]
        public HttpResponseMessage MstFacultyGet()
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

                List<MstFaculty> ObjLstFaculty = new List<MstFaculty>();
                BALFacultyList ObjBalFacultyList = new BALFacultyList();
                ObjLstFaculty = ObjBalFacultyList.FacultyListGet();

                if (ObjLstFaculty != null)
                    return Return.returnHttp("200", ObjLstFaculty, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Faculty Get For Drop Down
        [HttpPost]
        public HttpResponseMessage MstFacultyGetForDropDown()
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

                List<MstFaculty> ObjLstFaculty = new List<MstFaculty>();
                BALFacultyList ObjBalFacultyList = new BALFacultyList();
                ObjLstFaculty = ObjBalFacultyList.FacultyListGetForDropDown();

                if (ObjLstFaculty != null)
                    return Return.returnHttp("200", ObjLstFaculty, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Faculty Add
        [HttpPost]
        public HttpResponseMessage MstFacultyAdd(MstFaculty Faculty)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            if (String.IsNullOrWhiteSpace(Convert.ToString(Faculty.FacultyName)))
            {
                return Return.returnHttp("201", "Please enter faculty name", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(Faculty.FacultyCode)))
            {
                return Return.returnHttp("201", "Please enter faculty code", null);
            }
            /*else if (String.IsNullOrWhiteSpace(Convert.ToString(Faculty.FacultyAddress)))
            {
                return Return.returnHttp("201", "Please enter faculty address", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(Faculty.CityName)))
            {
                return Return.returnHttp("201", "Please enter city", null);
            }
            else if (validation.validateDigit(Convert.ToString(Faculty.Pincode)) == false)
            {
                return Return.returnHttp("201", "Please enter pincode", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(Faculty.FacultyContactNo)))
            {
                return Return.returnHttp("201", "Please enter faculty contact no.", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(Faculty.FacultyFaxNo)))
            {
                return Return.returnHttp("201", "Please enter faculty fax no.", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(Faculty.FacultyEmail)))
            {
                return Return.returnHttp("201", "Please enter faculty email", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(Faculty.FacultyUrl)))
            {
                return Return.returnHttp("201", "Please enter faculty url", null);
            }*/
            else
            {
                try
                {
                    // dynamic Jsondata = Faculty;

                    //Int64 Id = Convert.ToInt64(Jsondata.Id);
                    String FacultyName = Convert.ToString(Faculty.FacultyName);
                    String FacultyCode = Convert.ToString(Faculty.FacultyCode);
                    String FacultyAddress = Convert.ToString(Faculty.FacultyAddress);
                    String CityName = Convert.ToString(Faculty.CityName);
                    Int32 Pincode = Convert.ToInt32(Faculty.Pincode);
                    String FacultyContactNo = Convert.ToString(Faculty.FacultyContactNo);
                    String FacultyFaxNo = Convert.ToString(Faculty.FacultyFaxNo);
                    String FacultyEmail = Convert.ToString(Faculty.FacultyEmail);
                    String FacultyUrl = Convert.ToString(Faculty.FacultyUrl);
                    //Int64 UserId = Convert.ToInt64(Jsondata.UserId);
                    //for Testing
                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("MstFacultyAdd", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FacultyName", FacultyName);
                    cmd.Parameters.AddWithValue("@FacultyCode", FacultyCode);
                    cmd.Parameters.AddWithValue("@FacultyAddress", FacultyAddress);
                    cmd.Parameters.AddWithValue("@CityName", CityName);
                    cmd.Parameters.AddWithValue("@Pincode", Pincode);
                    cmd.Parameters.AddWithValue("@FacultyContactNo", FacultyContactNo);
                    cmd.Parameters.AddWithValue("@FacultyFaxNo", FacultyFaxNo);
                    cmd.Parameters.AddWithValue("@FacultyEmail", FacultyEmail);
                    cmd.Parameters.AddWithValue("@FacultyUrl", FacultyUrl);
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

        #region Faculty Edit
        [HttpPost]
        public HttpResponseMessage MstFacultyEdit(MstFaculty Faculty)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            if (String.IsNullOrWhiteSpace(Convert.ToString(Faculty.FacultyName)))
            {
                return Return.returnHttp("201", "Please select faculty name", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(Faculty.FacultyCode)))
            {
                return Return.returnHttp("201", "Please enter faculty code", null);
            }
            /*else if (String.IsNullOrWhiteSpace(Convert.ToString(Faculty.FacultyAddress)))
            {
                return Return.returnHttp("201", "Please enter faculty address", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(Faculty.CityName)))
            {
                return Return.returnHttp("201", "Please enter city", null);
            }
            else if (validation.validateDigit(Convert.ToString(Faculty.Pincode)) == false)
            {
                return Return.returnHttp("201", "Please enter pincode", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(Faculty.FacultyContactNo)))
            {
                return Return.returnHttp("201", "Please enter faculty contact no.", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(Faculty.FacultyFaxNo)))
            {
                return Return.returnHttp("201", "Please enter faculty fax no.", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(Faculty.FacultyEmail)))
            {
                return Return.returnHttp("201", "Please enter faculty email", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(Faculty.FacultyUrl)))
            {
                return Return.returnHttp("201", "Please enter faculty url", null);
            }*/
            else
            {
                try
                {
                    //dynamic Jsondata = Faculty;

                    Int32 Id = Convert.ToInt32(Faculty.Id);
                    String FacultyName = Convert.ToString(Faculty.FacultyName);
                    String FacultyCode = Convert.ToString(Faculty.FacultyCode);
                    String FacultyAddress = Convert.ToString(Faculty.FacultyAddress);
                    String CityName = Convert.ToString(Faculty.CityName);
                    Int32 Pincode = Convert.ToInt32(Faculty.Pincode);
                    String FacultyContactNo = Convert.ToString(Faculty.FacultyContactNo);
                    String FacultyFaxNo = Convert.ToString(Faculty.FacultyFaxNo);
                    String FacultyEmail = Convert.ToString(Faculty.FacultyEmail);
                    String FacultyUrl = Convert.ToString(Faculty.FacultyUrl);
                    //Int64 UserId = Convert.ToInt64(Jsondata.UserId);
                    //for Testing
                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("MstFacultyEdit", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@FacultyName", FacultyName);
                    cmd.Parameters.AddWithValue("@FacultyCode", FacultyCode);
                    cmd.Parameters.AddWithValue("@FacultyAddress", FacultyAddress);
                    cmd.Parameters.AddWithValue("@CityName", CityName);
                    if (Pincode == 0)
                    {
                        cmd.Parameters.AddWithValue("@Pincode", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Pincode", Pincode);
                    }
                    cmd.Parameters.AddWithValue("@FacultyContactNo", FacultyContactNo);
                    cmd.Parameters.AddWithValue("@FacultyFaxNo", FacultyFaxNo);
                    cmd.Parameters.AddWithValue("@FacultyEmail", FacultyEmail);
                    cmd.Parameters.AddWithValue("@FacultyUrl", FacultyUrl);
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

        #region Faculty Delete
        [HttpPost]
        public HttpResponseMessage MstFacultyDelete(MstFaculty Faculty)
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
                //dynamic Jsondata = Faculty;

                Int32 Id = Convert.ToInt32(Faculty.Id);
                //Int64 UserId = Convert.ToInt64(Jsondata.User);
                //for Testing
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("MstFacultyDelete", Con);
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

        #region Faculty Suspend
        [HttpPost]
        public HttpResponseMessage MstFacultyIsSuspended(MstFaculty Faculty)
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

                Int32 Id = Convert.ToInt32(Faculty.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("MstFacultyActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstFacultyIsActiveDisable");
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

        #region Faculty Active
        [HttpPost]
        public HttpResponseMessage MstFacultyIsActive(MstFaculty Faculty)
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
                Int32 Id = Convert.ToInt32(Faculty.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("MstFacultyActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstFacultyIsActiveEnable");
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

        #region Faculty Get By Id
        [HttpPost]
        public HttpResponseMessage MstFacultyGetbyId(MstFaculty Faculty)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
            String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
            Request.Headers.TryGetValues("InstituteId", out IEnumerable<string> InstituteId1);
            var InstituteId = InstituteId1?.FirstOrDefault();

            //String InstituteId = Request.Headers.GetValues("InstituteId").FirstOrDefault();

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


                SqlCommand cmd = new SqlCommand("MstInstituteGetTest", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (InstituteId != null || InstituteId != "0" || InstituteId != "")
                {
                    cmd.Parameters.AddWithValue("@Id", InstituteId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Id", facultyDepartIntituteId);
                }
                
                List<MstFaculty> ObjLstFaculty = new List<MstFaculty>();
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
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
