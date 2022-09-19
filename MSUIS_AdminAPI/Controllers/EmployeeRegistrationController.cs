using Newtonsoft.Json.Linq;
using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MSUIS_TokenManager.App_Start;
using System.Text.RegularExpressions;

namespace MSUISApi.Controllers
{
    public class EmployeeRegistrationController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        
        #region UserTypeMaster Get
        [HttpPost]
        public HttpResponseMessage UserTypeMasterGet()
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
                SqlCommand cmd = new SqlCommand("UserTypeMasterGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<UserTypeMaster> ObjLstUserTypeMst = new List<UserTypeMaster>();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        UserTypeMaster objUserTypeMst = new UserTypeMaster();
                        objUserTypeMst.Id = Convert.ToInt64(dt.Rows[i]["Id"]);
                        objUserTypeMst.UserTypeName = Convert.ToString(dt.Rows[i]["UserTypeName"]);

                        ObjLstUserTypeMst.Add(objUserTypeMst);
                    }
                }
                return Return.returnHttp("200", ObjLstUserTypeMst, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion      
       
        #region MaritalStatus Get
        
        [HttpPost]
        public HttpResponseMessage MaritalStatusGet()
        {
            try
            {
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                List<GenMaritalStatus> slist = new List<GenMaritalStatus>();

                cmd.CommandText = "MaritalStatusGet";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow DR in dt.Rows)
                {
                    GenMaritalStatus s = new GenMaritalStatus();
                    s.Id = Convert.ToInt32(DR["Id"].ToString());
                    s.MaritalStatus = DR["MaritalStatus"].ToString();

                    slist.Add(s);

                }


                return Return.returnHttp("200", slist, null);



            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region BloodGroup Get
        [HttpPost]
        public HttpResponseMessage BloodGroupGet()
        {
            try
            {
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                List<BloodGroup> slist = new List<BloodGroup>();

                cmd.CommandText = "BloodGroupGet";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow DR in dt.Rows)
                {
                    BloodGroup s = new BloodGroup();
                    s.Id = Convert.ToInt32(DR["Id"].ToString());
                    s.BloodGroupName = DR["BloodGroupName"].ToString();

                    slist.Add(s);

                }


                return Return.returnHttp("200", slist, null);



            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region CountryMaster Get
        [HttpPost]
        public HttpResponseMessage CountryMasterGet()
        {
            try
            {
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                List<CountryMaster> slist = new List<CountryMaster>();

                cmd.CommandText = "CountryMasterGet";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow DR in dt.Rows)
                {
                    CountryMaster s = new CountryMaster();
                    s.Id = Convert.ToInt64(DR["Id"].ToString());
                    s.Alpha2Code = DR["Alpha2Code"].ToString();
                    s.Alpha3Code = DR["Alpha3Code"].ToString();
                    s.CountryName = DR["CountryName"].ToString();

                    slist.Add(s);

                }


                return Return.returnHttp("200", slist, null);



            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region StateMaster Get
        [HttpPost]
        public HttpResponseMessage StateMasterGet(CountryMaster cm)
        {
            try
            {
                Int64 CountryId = cm.Id;

                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                List<StateMaster> slist = new List<StateMaster>();

                cmd.CommandText = "StateMasterGet";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CountryId", CountryId);

                cmd.Connection = con;
                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow DR in dt.Rows)
                {
                    StateMaster s = new StateMaster();
                    s.Id = Convert.ToInt64(DR["Id"].ToString());
                    s.StateName = DR["StateName"].ToString();
                    s.CountryId = Convert.ToInt64(DR["CountryId"].ToString());

                    slist.Add(s);

                }


                return Return.returnHttp("200", slist, null);



            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region DistrictMaster Get
        [HttpPost]
        public HttpResponseMessage DistrictMasterGet(StateMaster sm)
        {
            try
            {
                
                Int64 StateId = sm.Id;

                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                List<DistrictMaster> slist = new List<DistrictMaster>();

                cmd.CommandText = "DistrictMasterGet";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StateId", StateId);

                cmd.Connection = con;
                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow DR in dt.Rows)
                {
                    DistrictMaster s = new DistrictMaster();
                    s.Id = Convert.ToInt64(DR["Id"].ToString());
                    s.DistrictName = DR["DistrictName"].ToString();
                    s.StateId = Convert.ToInt64(DR["StateId"].ToString());

                    slist.Add(s);

                }


                return Return.returnHttp("200", slist, null);



            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region StateMaster Get
        [HttpPost]
        public HttpResponseMessage StateMasterSGet(CountryMaster cm)
        {
            try
            {
                Int64 CountryId = cm.Id;

                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                List<StateMaster> slist = new List<StateMaster>();

                cmd.CommandText = "StateMasterGet";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CountryId", CountryId);

                cmd.Connection = con;
                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow DR in dt.Rows)
                {
                    StateMaster s = new StateMaster();
                    s.Id = Convert.ToInt64(DR["Id"].ToString());
                    s.StateName = DR["StateName"].ToString();
                    s.CountryId = Convert.ToInt64(DR["CountryId"].ToString());

                    slist.Add(s);

                }


                return Return.returnHttp("200", slist, null);



            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region DistrictMaster Get
        [HttpPost]
        public HttpResponseMessage DistrictMasterDGet(StateMaster sm)
        {
            try
            {
                //String StateName = sm.StateName;
                Int64 StateId = sm.Id;

                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                List<DistrictMaster> slist = new List<DistrictMaster>();

                cmd.CommandText = "DistrictMasterGet";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StateId", StateId);

                cmd.Connection = con;
                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow DR in dt.Rows)
                {
                    DistrictMaster s = new DistrictMaster();
                    s.Id = Convert.ToInt64(DR["Id"].ToString());
                    s.DistrictName = DR["DistrictName"].ToString();
                    s.StateId = Convert.ToInt64(DR["StateId"].ToString());

                    slist.Add(s);

                }


                return Return.returnHttp("200", slist, null);



            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region MstEmployee Add
        [HttpPost]
        public HttpResponseMessage MstEmployeeAdd(EmployeeRegistration EmpReg)
        {
            try
            {
                Validation validation = new Validation();
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();

                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                if (EmpReg.AadharCardNo != null)
                {
                    string RegEx = @"^[2-9]{1}[0-9]{3}\-{1}[0-9]{4}\-{1}[0-9]{4}$";
                    var st = new Regex(RegEx);
                    string AdCheck = Convert.ToString(EmpReg.AadharCardNo);

                    if (st.IsMatch(AdCheck) == true)
                    {
                        EmpReg.AadharCardNo = EmpReg.AadharCardNo;
                    }
                    else
                    {
                        return Return.returnHttp("201", "Invalid valid Aadhar Number", null);
                    }
                }

                if (String.IsNullOrWhiteSpace(Convert.ToString(EmpReg.FirstName)))
                {
                    return Return.returnHttp("201", "Please select FirstName", null);
                }
                else if (String.IsNullOrWhiteSpace(Convert.ToString(EmpReg.MiddleName)))
                {
                    return Return.returnHttp("201", "Please enter MiddleName", null);
                }
                else if (String.IsNullOrWhiteSpace(Convert.ToString(EmpReg.LastName)))
                {
                    return Return.returnHttp("201", "Please enter LastName", null);
                }
                else if (String.IsNullOrWhiteSpace(Convert.ToString(EmpReg.PermanentAddress)))
                {
                    return Return.returnHttp("201", "Please enter PermanentAddress", null);
                }
                else if (String.IsNullOrWhiteSpace(Convert.ToString(EmpReg.CurrentAddress)))
                {
                    return Return.returnHttp("201", "Please enter CurrentAddress", null);
                }
                else if (String.IsNullOrWhiteSpace(Convert.ToString(EmpReg.CurrentCityVillage)))
                {
                    return Return.returnHttp("201", "Please enter CurrentCityVillage", null);
                }
                else if (String.IsNullOrWhiteSpace(Convert.ToString(EmpReg.PermanentCityVillage)))
                {
                    return Return.returnHttp("201", "Please enter PermanentCityVillage", null);
                }
                else if (String.IsNullOrWhiteSpace(Convert.ToString(EmpReg.EmailId)))
                {
                    return Return.returnHttp("201", "Please enter Email", null);
                }
                else if (String.IsNullOrWhiteSpace(Convert.ToString(EmpReg.MobileNo)))
                {
                    return Return.returnHttp("201", "Please enter MobileNo", null);
                }
                else if (String.IsNullOrWhiteSpace(Convert.ToString(EmpReg.VoterId)))
                {
                    return Return.returnHttp("201", "Please enter VoterId", null);
                }
                else if (String.IsNullOrWhiteSpace(Convert.ToString(EmpReg.PanCardNo)))
                {
                    return Return.returnHttp("201", "Please enter PanCardNo", null);
                }
             

                else if (String.IsNullOrEmpty(Convert.ToString(EmpReg.NatureofAppointment)))
                {
                    return Return.returnHttp("201", "Please select NatureofAppointment", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(EmpReg.BloodGroupId)))
                {
                    return Return.returnHttp("201", "Please select BloodGroup", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(EmpReg.CurrentCountryId)))
                {
                    return Return.returnHttp("201", "Please select CurrentCountry", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(EmpReg.CurrentStateId)))
                {
                    return Return.returnHttp("201", "Please select CurrentState", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(EmpReg.CurrentDistrictId)))
                {
                    return Return.returnHttp("201", "Please select CurrentDistrict", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(EmpReg.CurrentPincode)))
                {
                    return Return.returnHttp("201", "Please select CurrentPincode", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(EmpReg.PermanentCountryId)))
                {
                    return Return.returnHttp("201", "Please select PermanentCountry", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(EmpReg.PermanentStateId)))
                {
                    return Return.returnHttp("201", "Please select PermanentState", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(EmpReg.PermanentDistrictId)))
                {
                    return Return.returnHttp("201", "Please select PermanentDistrict", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(EmpReg.PermanentPincode)))
                {
                    return Return.returnHttp("201", "Please select PermanentPincode", null);
                }
                else { 
                Int64 UserTypeId = Convert.ToInt64(EmpReg.UserTypeId);
                String FirstName = Convert.ToString(EmpReg.FirstName);
                String MiddleName = Convert.ToString(EmpReg.MiddleName);
                String LastName = Convert.ToString(EmpReg.LastName);            
                Int64 MaritalStatusId = Convert.ToInt64(EmpReg.MaritalStatusId);
                Int64 BloodGroupId = Convert.ToInt64(EmpReg.BloodGroupId);
                String CurrentAddress = Convert.ToString(EmpReg.CurrentAddress);
                Int64 CurrentCountryId = Convert.ToInt64(EmpReg.CurrentCountryId);
                Int64 CurrentStateId = Convert.ToInt64(EmpReg.CurrentStateId);
                Int64 CurrentDistrictId = Convert.ToInt64(EmpReg.CurrentDistrictId);
                String CurrentCityVillage = Convert.ToString(EmpReg.CurrentCityVillage);
                Int64 CurrentPincode = Convert.ToInt64(EmpReg.CurrentPincode);
             
                String PermanentAddress = Convert.ToString(EmpReg.PermanentAddress);
                Int64 PermanentCountryId = Convert.ToInt64(EmpReg.PermanentCountryId);
                Int64 PermanentStateId = Convert.ToInt64(EmpReg.PermanentStateId);
                Int64 PermanentDistrictId = Convert.ToInt64(EmpReg.PermanentDistrictId);
                String PermanentCityVillage = Convert.ToString(EmpReg.PermanentCityVillage);
                Int64 PermanentPincode = Convert.ToInt64(EmpReg.PermanentPincode);
                String EmailId = Convert.ToString(EmpReg.EmailId);
                String MobileNo = Convert.ToString(EmpReg.MobileNo);
                String WhatsAppMobileNo = Convert.ToString(EmpReg.WhatsAppMobileNo);
                Int64 SocialCategoryId = Convert.ToInt64(EmpReg.SocialCategoryId);
                String AadharCardNo = Convert.ToString(EmpReg.AadharCardNo);
                String VoterId = Convert.ToString(EmpReg.VoterId);
                String PanCardNo = Convert.ToString(EmpReg.PanCardNo);
                String NatureofAppointment = Convert.ToString(EmpReg.NatureofAppointment);
                Boolean IsCurrentAsPermanent = Convert.ToBoolean(EmpReg.IsCurrentAsPermanent);
                DateTime? BirthDate;
                Int64 UserId = Convert.ToInt64(res.ToString());

                if (EmpReg.BirthDate != null)
                {
                    DateTime dtBirthDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(EmpReg.BirthDate), out dtBirthDate);
                    if (ChkDt)
                    {
                         BirthDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(EmpReg.BirthDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

                    }
                    else
                    {
                          BirthDate = null;
                    }
                }
                else
                {
                        BirthDate = null;
                }

                SqlCommand cmd = new SqlCommand("MstEmployeeAdd", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserTypeId", UserTypeId);
                cmd.Parameters.AddWithValue("@FirstName", FirstName);
                cmd.Parameters.AddWithValue("@MiddleName", MiddleName);
                cmd.Parameters.AddWithValue("@LastName", LastName);
                cmd.Parameters.AddWithValue("@BirthDate", BirthDate);
                cmd.Parameters.AddWithValue("@MaritalStatusId", MaritalStatusId);
                cmd.Parameters.AddWithValue("@BloodGroupId", BloodGroupId);
                cmd.Parameters.AddWithValue("@CurrentAddress", CurrentAddress);
                cmd.Parameters.AddWithValue("@CurrentCountryId", CurrentCountryId);
                cmd.Parameters.AddWithValue("@CurrentStateId", CurrentStateId);
                cmd.Parameters.AddWithValue("@CurrentDistrictId", CurrentDistrictId);
                cmd.Parameters.AddWithValue("@CurrentCityVillage", CurrentCityVillage);
                cmd.Parameters.AddWithValue("@CurrentPincode", CurrentPincode);
                cmd.Parameters.AddWithValue("@PermanentAddress", PermanentAddress);
                cmd.Parameters.AddWithValue("@PermanentCountryId", PermanentCountryId);
                cmd.Parameters.AddWithValue("@PermanentStateId", PermanentStateId);
                cmd.Parameters.AddWithValue("@PermanentDistrictId", PermanentDistrictId);
                cmd.Parameters.AddWithValue("@PermanentCityVillage", PermanentCityVillage);
                cmd.Parameters.AddWithValue("@PermanentPincode", PermanentPincode);
                cmd.Parameters.AddWithValue("@EmailId", EmailId);
                cmd.Parameters.AddWithValue("@MobileNo", MobileNo);
                cmd.Parameters.AddWithValue("@WhatsAppMobileNo", WhatsAppMobileNo);
                cmd.Parameters.AddWithValue("@SocialCategoryId", SocialCategoryId);
                cmd.Parameters.AddWithValue("@VoterId", VoterId);
                cmd.Parameters.AddWithValue("@AadharCardNo", AadharCardNo);              
                cmd.Parameters.AddWithValue("@PanCardNo", PanCardNo);
                cmd.Parameters.AddWithValue("@NatureofAppointment", NatureofAppointment);
                cmd.Parameters.AddWithValue("@IsCurrentAsPermanent", IsCurrentAsPermanent);

                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);

                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();

                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);

                con.Close();

                return Return.returnHttp("200", strMessage.ToString(), null);
                }
            }

            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region MstEmployee Get
        [HttpPost]
        public HttpResponseMessage MstEmployeeGet()
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

                SqlCommand cmd = new SqlCommand("MstEmployeeGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<EmployeeRegistration> ObjEmpReg = new List<EmployeeRegistration>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        EmployeeRegistration ObjEmp = new EmployeeRegistration();
                        ObjEmp.Id = Convert.ToInt64(dr["Id"]);
                        ObjEmp.UserTypeId = Convert.ToInt64(dr["UserTypeId"].ToString());
                        ObjEmp.UserTypeName = (dr["UserTypeName"].ToString());
                        ObjEmp.FirstName = (dr["FirstName"].ToString());
                        ObjEmp.MiddleName = (dr["MiddleName"].ToString());
                        ObjEmp.LastName = (dr["LastName"].ToString());
                        ObjEmp.FullName = ObjEmp.FirstName + " " + ObjEmp.MiddleName + " " + ObjEmp.LastName;
                        ObjEmp.BirthDateView = string.Format("{0: dd-MM-yyyy}", dr["BirthDate"]);
                        ObjEmp.MobileNo = (dr["MobileNo"].ToString());
                        ObjEmp.EmailId = (dr["EmailId"].ToString());
                        ObjEmp.WhatsAppMobileNo = (dr["WhatsAppMobileNo"].ToString());
                        ObjEmp.MaritalStatusId = Convert.ToInt64(dr["MaritalStatusId"].ToString());
                        ObjEmp.MaritalStatus = (dr["MaritalStatus"].ToString());
                        ObjEmp.BloodGroupId = Convert.ToInt64(dr["BloodGroupId"].ToString());
                        ObjEmp.BloodGroupName = (dr["BloodGroupName"].ToString());
                        ObjEmp.PermanentAddress = (dr["PermanentAddress"].ToString());
                        ObjEmp.PermanentCountryId = Convert.ToInt64(dr["PermanentCountryId"].ToString());
                        ObjEmp.PermanentCountry = (dr["PermanentCountry"].ToString());
                        ObjEmp.PermanentStateId = Convert.ToInt64(dr["PermanentStateId"].ToString());
                        ObjEmp.PermanentState = (dr["PermanentState"].ToString());
                        ObjEmp.PermanentDistrictId = Convert.ToInt64(dr["PermanentDistrictId"].ToString());
                        ObjEmp.PermanentDistrict = (dr["PermanentDistrict"].ToString());
                        ObjEmp.PermanentCityVillage = (dr["PermanentCityVillage"].ToString());
                        ObjEmp.PermanentPincode = Convert.ToInt64(dr["PermanentPincode"].ToString());
                        ObjEmp.CurrentAddress = (dr["CurrentAddress"].ToString());
                        ObjEmp.CurrentCountryId = Convert.ToInt64(dr["CurrentCountryId"].ToString());
                        ObjEmp.CurrentCountry = (dr["CurrentCountry"].ToString());
                        ObjEmp.CurrentStateId = Convert.ToInt64(dr["CurrentStateId"].ToString());
                        ObjEmp.CurrentState = (dr["CurrentState"].ToString());
                        ObjEmp.CurrentDistrictId = Convert.ToInt64(dr["CurrentDistrictId"].ToString());
                        ObjEmp.CurrentDistrict = (dr["CurrentDistrict"].ToString());
                        ObjEmp.CurrentCityVillage = (dr["CurrentCityVillage"].ToString());
                        ObjEmp.CurrentPincode = Convert.ToInt64(dr["CurrentPincode"].ToString());
                        //ObjEmp.SocialCategoryId = Convert.ToInt64(dr["SocialCategoryId"].ToString());

                        var SocialCategoryId = dr["SocialCategoryId"];
                        if (SocialCategoryId is DBNull)
                        {
                            ObjEmp.SocialCategoryId = null;
                        }
                        else
                        {
                            ObjEmp.SocialCategoryId = Convert.ToInt64(dr["SocialCategoryId"]);
                        }
                        ObjEmp.SocialCategoryName = (dr["SocialCategoryName"].ToString());
                        ObjEmp.AadharCardNo = (dr["AadharCardNo"].ToString());
                        ObjEmp.VoterId = (dr["VoterId"].ToString());
                        ObjEmp.PanCardNo = (dr["PanCardNo"].ToString());
                        ObjEmp.NatureofAppointment = (dr["NatureofAppointment"].ToString());
                        //ObjEmp.IsCurrentAsPermanent = Convert.ToBoolean(dr["IsCurrentAsPermanent"].ToString());

                        var IsCurrentAsPermanent = dr["IsCurrentAsPermanent"];
                        if (IsCurrentAsPermanent is DBNull)
                        {
                            ObjEmp.IsCurrentAsPermanent = null;
                        }
                        else
                        {
                            ObjEmp.IsCurrentAsPermanent = Convert.ToBoolean(dr["IsCurrentAsPermanent"]);
                        }

                        count = count + 1;
                        ObjEmp.IndexId = count;
                        ObjEmpReg.Add(ObjEmp);
                    }

                }
                return Return.returnHttp("200", ObjEmpReg, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region MstEmployee Edit
        [HttpPost]
        public HttpResponseMessage MstEmployeeEdit(JObject jsonobject)

        {
            EmployeeRegistration EmployeeRegistration = new EmployeeRegistration();
            dynamic jsonData = jsonobject;

            try
            {
                EmployeeRegistration.Id = Convert.ToInt64(jsonData.Id);
                EmployeeRegistration.UserTypeId = Convert.ToInt64(jsonData.UserTypeId);
                //EmployeeRegistration.UserTypeName =  Convert.ToInt64(jsonData.DesignationName);
                EmployeeRegistration.FirstName = Convert.ToString(jsonData.FirstName);
                EmployeeRegistration.MiddleName = Convert.ToString(jsonData.MiddleName);
                EmployeeRegistration.LastName = Convert.ToString(jsonData.LastName);
                //EmployeeRegistration.BirthDate = Convert.ToString(jsonData.BirthDate);
                EmployeeRegistration.MobileNo = Convert.ToString(jsonData.MobileNo);
                EmployeeRegistration.EmailId = Convert.ToString(jsonData.EmailId);
                EmployeeRegistration.WhatsAppMobileNo = Convert.ToString(jsonData.WhatsAppMobileNo);
                EmployeeRegistration.MaritalStatusId = Convert.ToInt64(jsonData.MaritalStatusId);
                //EmployeeRegistration.MaritalStatus = Convert.ToInt64(jsonData.MaritalStatus);
                EmployeeRegistration.BloodGroupId = Convert.ToInt64(jsonData.BloodGroupId);
                //EmployeeRegistration.BloodGroupName = Convert.ToInt64(jsonData.BloodGroupName);
                EmployeeRegistration.PermanentAddress = Convert.ToString(jsonData.PermanentAddress);
                EmployeeRegistration.PermanentCountryId = Convert.ToInt64(jsonData.PermanentCountryId);
                //EmployeeRegistration.PermanentCountry = Convert.ToInt64(jsonData.PermanentCountry);
                EmployeeRegistration.PermanentStateId = Convert.ToInt64(jsonData.PermanentStateId);
                //EmployeeRegistration.PermanentState = Convert.ToInt64(jsonData.PermanentState);
                EmployeeRegistration.PermanentDistrictId = Convert.ToInt64(jsonData.PermanentDistrictId);
                //EmployeeRegistration.PermanentDistrict = Convert.ToInt64(jsonData.PermanentDistrict);
                EmployeeRegistration.PermanentCityVillage = Convert.ToString(jsonData.PermanentCityVillage);
                EmployeeRegistration.PermanentPincode = Convert.ToInt64(jsonData.PermanentPincode);
                EmployeeRegistration.CurrentAddress = Convert.ToString(jsonData.CurrentAddress);
                EmployeeRegistration.CurrentCountryId = Convert.ToInt64(jsonData.CurrentCountryId);
                //EmployeeRegistration.CurrentCountry = Convert.ToInt64(jsonData.CurrentCountry);
                EmployeeRegistration.CurrentStateId = Convert.ToInt64(jsonData.CurrentStateId);
                //EmployeeRegistration.CurrentState = Convert.ToInt64(jsonData.CurrentState);
                EmployeeRegistration.CurrentDistrictId = Convert.ToInt64(jsonData.CurrentDistrictId);
                //EmployeeRegistration.CurrentDistrict = Convert.ToInt64(jsonData.CurrentDistrict);
                EmployeeRegistration.CurrentCityVillage = Convert.ToString(jsonData.CurrentCityVillage);
                EmployeeRegistration.CurrentPincode = Convert.ToInt64(jsonData.CurrentPincode);
                EmployeeRegistration.SocialCategoryId = Convert.ToInt64(jsonData.SocialCategoryId);
                EmployeeRegistration.NatureofAppointment = Convert.ToString(jsonData.NatureofAppointment);
                EmployeeRegistration.AadharCardNo = Convert.ToString(jsonData.AadharCardNo);
                EmployeeRegistration.VoterId = Convert.ToString(jsonData.VoterId);
                EmployeeRegistration.PanCardNo = Convert.ToString(jsonData.PanCardNo);
                EmployeeRegistration.IsCurrentAsPermanent = Convert.ToBoolean(jsonData.IsCurrentAsPermanent);

                if (Convert.ToDateTime(jsonData.BirthDate) != null)
                {
                    DateTime dtBirthDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(Convert.ToDateTime(jsonData.BirthDate)), out dtBirthDate);
                    if (ChkDt)
                    {
                        EmployeeRegistration.BirthDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(Convert.ToDateTime(jsonData.BirthDate)).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    }
                    else
                    {
                        EmployeeRegistration.BirthDate = null;
                    }
                }
                else
                {
                    EmployeeRegistration.BirthDate = null;
                }


                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                EmployeeRegistration.ModifiedBy = Convert.ToInt64(responseToken);

                SqlCommand cmd = new SqlCommand("MstEmployeeEdit", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", EmployeeRegistration.Id);
                cmd.Parameters.AddWithValue("@UserTypeId", EmployeeRegistration.UserTypeId);
                cmd.Parameters.AddWithValue("@FirstName", EmployeeRegistration.FirstName); 
                cmd.Parameters.AddWithValue("@MiddleName", EmployeeRegistration.MiddleName);
                cmd.Parameters.AddWithValue("@LastName", EmployeeRegistration.LastName);
                cmd.Parameters.AddWithValue("@BirthDate", EmployeeRegistration.BirthDate);
                cmd.Parameters.AddWithValue("@MobileNo", EmployeeRegistration.MobileNo);
                cmd.Parameters.AddWithValue("@EmailId", EmployeeRegistration.EmailId);
                cmd.Parameters.AddWithValue("@WhatsAppMobileNo", EmployeeRegistration.WhatsAppMobileNo);
                cmd.Parameters.AddWithValue("@MaritalStatusId", EmployeeRegistration.MaritalStatusId);
                cmd.Parameters.AddWithValue("@BloodGroupId", EmployeeRegistration.BloodGroupId);
                cmd.Parameters.AddWithValue("@PermanentAddress", EmployeeRegistration.PermanentAddress);
                cmd.Parameters.AddWithValue("@PermanentCountryId", EmployeeRegistration.PermanentCountryId);
                cmd.Parameters.AddWithValue("@PermanentStateId", EmployeeRegistration.PermanentStateId);
                cmd.Parameters.AddWithValue("@PermanentDistrictId", EmployeeRegistration.PermanentDistrictId);
                cmd.Parameters.AddWithValue("@PermanentCityVillage", EmployeeRegistration.PermanentCityVillage);
                cmd.Parameters.AddWithValue("@PermanentPincode", EmployeeRegistration.PermanentPincode);
                cmd.Parameters.AddWithValue("@CurrentAddress", EmployeeRegistration.CurrentAddress);
                cmd.Parameters.AddWithValue("@CurrentCountryId", EmployeeRegistration.CurrentCountryId);
                cmd.Parameters.AddWithValue("@CurrentStateId", EmployeeRegistration.CurrentStateId);
                cmd.Parameters.AddWithValue("@CurrentDistrictId", EmployeeRegistration.CurrentDistrictId);
                cmd.Parameters.AddWithValue("@CurrentCityVillage", EmployeeRegistration.CurrentCityVillage);
                cmd.Parameters.AddWithValue("@CurrentPincode", EmployeeRegistration.CurrentPincode);
                cmd.Parameters.AddWithValue("@SocialCategoryId", EmployeeRegistration.SocialCategoryId);
                cmd.Parameters.AddWithValue("@NatureofAppointment", EmployeeRegistration.NatureofAppointment);
                cmd.Parameters.AddWithValue("@AadharCardNo", EmployeeRegistration.AadharCardNo);
                cmd.Parameters.AddWithValue("@VoterId", EmployeeRegistration.VoterId);
                cmd.Parameters.AddWithValue("@PanCardNo", EmployeeRegistration.PanCardNo);
                cmd.Parameters.AddWithValue("@IsCurrentAsPermanent", EmployeeRegistration.IsCurrentAsPermanent);
                cmd.Parameters.AddWithValue("@ModifiedBy", EmployeeRegistration.ModifiedBy);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Employee Details have been updated successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);


            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion

        #region MstEmployee Delete
        [HttpPost]
        public HttpResponseMessage MstEmployeeDelete(EmployeeRegistration ObjEmpReg)
        {
            EmployeeRegistration modelobj = new EmployeeRegistration();

            try
            {
                modelobj.Id = Convert.ToInt32(ObjEmpReg.Id);
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }


                SqlCommand cmd = new SqlCommand("MstEmployeeDelete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", modelobj.Id.ToString());

                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Employee Details have been deleted successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region GenReservationCategory Get
        [HttpGet]
        public HttpResponseMessage GenReservationCategoryGet()
        {
            try
            {
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand("GenReservationCategoryGet", con);
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                cmd.CommandType = CommandType.StoredProcedure;
                //da.SelectCommand.Parameters.Add("@Message", SqlDbType.VarChar).Value = "True";
                da.SelectCommand = cmd;
                da.Fill(Dt);


                List<GenApplicationUnderCategory> ObjLstAppReservation = new List<GenApplicationUnderCategory>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        GenApplicationUnderCategory ObjReservation = new GenApplicationUnderCategory();

                        //CommonModels ObjPaymentMethod = new CommonModels();
                        ObjReservation.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        ObjReservation.ApplicationReservationCode = Convert.ToString(Dt.Rows[i]["ApplicationReservationCode"]);
                        ObjReservation.ApplicationReservationName = Convert.ToString(Dt.Rows[i]["ApplicationReservationName"]);
                        ObjReservation.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);

                        ObjLstAppReservation.Add(ObjReservation);
                    }

                }

                return Return.returnHttp("200", ObjLstAppReservation, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion
    }
}
