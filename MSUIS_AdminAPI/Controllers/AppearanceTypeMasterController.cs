using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using MSUISApi.BAL;
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
    public class AppearanceTypeMasterController : ApiController
    {
        #region Old Code
        //SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        //DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        //SqlDataAdapter sda = new SqlDataAdapter();

        //#region AppearanceTypeGet
        //[HttpPost]
        //public HttpResponseMessage AppearanceTypeGet()
        //{
        //    String token = Request.Headers.GetValues("token").FirstOrDefault();
        //    TokenOperation ac = new TokenOperation();

        //    string res = ac.ValidateToken(token);

        //    if (res == "0")
        //    {
        //        return Return.returnHttp("0", null, null);
        //    }
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("AppearanceTypeMasterGet", con);

        //        cmd.CommandType = CommandType.StoredProcedure;
        //        DataTable dt = new DataTable();
        //        sda.SelectCommand = cmd;
        //        sda.Fill(dt);

        //        List<AppearanceTypeMaster> ObjListAppType = new List<AppearanceTypeMaster>();

        //        if (dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                AppearanceTypeMaster apptype = new AppearanceTypeMaster();
        //                apptype.Id = Convert.ToInt32(dr["Id"].ToString());
        //                apptype.AppearanceType = (Convert.ToString(dr["AppearanceType"])).IsEmpty() ? "" : Convert.ToString(dr["AppearanceType"]);
        //                apptype.IsActive = (Convert.ToString(dr["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(dr["IsActive"]);
        //                apptype.IsDeleted = (Convert.ToString(dr["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(dr["IsDeleted"]);
        //                apptype.IsActiveSts = (Convert.ToString(dr["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(dr["IsActiveSts"]);
        //                ObjListAppType.Add(apptype);
        //            }
        //        }
        //        return Return.returnHttp("200", ObjListAppType, null);
        //    }
        //    catch (Exception e)
        //    {
        //        return Return.returnHttp("201", e.Message.ToString(), null);
        //    }
        //}
        //#endregion


        //#region AppearanceTypeGetById
        //[HttpPost]
        //public HttpResponseMessage AppearanceTypeGetById(AppearanceTypeMaster ATM)
        //{
        //    String token = Request.Headers.GetValues("token").FirstOrDefault();
        //    TokenOperation ac = new TokenOperation();

        //    string res = ac.ValidateToken(token);

        //    if (res == "0")
        //    {
        //        return Return.returnHttp("0", null, null);
        //    }
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("AppearanceTypeMasterGet", con);

        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@AppearanceTypeId", ATM.AppearanceTypeId);
        //        DataTable dt = new DataTable();
        //        sda.SelectCommand = cmd;
        //        sda.Fill(dt);

        //        List<AppearanceTypeMaster> ObjListAppType = new List<AppearanceTypeMaster>();

        //        if (dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                AppearanceTypeMaster apptype = new AppearanceTypeMaster();
        //                apptype.Id = Convert.ToInt32(dr["Id"].ToString());
        //                apptype.AppearanceType = (Convert.ToString(dr["AppearanceType"])).IsEmpty() ? "" : Convert.ToString(dr["AppearanceType"]);
        //                apptype.IsActive = (Convert.ToString(dr["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(dr["IsActive"]);
        //                apptype.IsDeleted = (Convert.ToString(dr["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(dr["IsDeleted"]);
        //                apptype.IsActiveSts = (Convert.ToString(dr["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(dr["IsActiveSts"]);
        //                ObjListAppType.Add(apptype);
        //            }
        //        }
        //        return Return.returnHttp("200", ObjListAppType, null);
        //    }
        //    catch (Exception e)
        //    {
        //        return Return.returnHttp("201", e.Message.ToString(), null);
        //    }
        //}
        //#endregion

        //#region AppearanceTypeAdd
        //[HttpPost]
        //public HttpResponseMessage AppearanceTypeAdd(AppearanceTypeMaster AppTy)
        //{
        //    String token = Request.Headers.GetValues("token").FirstOrDefault();
        //    TokenOperation ac = new TokenOperation();

        //    string res = ac.ValidateToken(token);

        //    if (res == "0")
        //    {
        //        return Return.returnHttp("0", null, null);
        //    }
        //    if (String.IsNullOrWhiteSpace(Convert.ToString(AppTy.AppearanceType)))
        //    {
        //        return Return.returnHttp("201", "Please Enter Appearance Type", null);
        //    }
        //    else
        //    {
        //        try
        //        {
        //            string AppearanceType = Convert.ToString(AppTy.AppearanceType);
        //            Int64 UserId = Convert.ToInt64(res.ToString());

        //            SqlCommand cmd = new SqlCommand("AppearanceTypeMasterAdd", con);
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            cmd.Parameters.AddWithValue("@AppearanceType", AppearanceType);
        //            cmd.Parameters.AddWithValue("@UserId", UserId);
        //            cmd.Parameters.AddWithValue("@UserTime", datetime);
        //            cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
        //            cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

        //            con.Open();
        //            cmd.ExecuteNonQuery();
        //            string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
        //            con.Close();
        //            if (string.Equals(strMessage, "TRUE"))
        //            {
        //                strMessage = "Your data has been added successfully.";
        //            }
        //            return Return.returnHttp("200", strMessage.ToString(), null);
        //        }
        //        catch (Exception e)
        //        {
        //            return Return.returnHttp("201", e.Message.ToString(), null);
        //        }
        //    }
        //}
        //#endregion

        //#region AppearanceTypeUpdate
        //[HttpPost]
        //public HttpResponseMessage AppearanceTypeUpdate(AppearanceTypeMaster ObjAType)
        //{
        //    String token = Request.Headers.GetValues("token").FirstOrDefault();
        //    TokenOperation ac = new TokenOperation();

        //    string res = ac.ValidateToken(token);

        //    if (res == "0")
        //    {
        //        return Return.returnHttp("0", null, null);
        //    }
        //    if (String.IsNullOrWhiteSpace(Convert.ToString(ObjAType.AppearanceType)))
        //    {
        //        return Return.returnHttp("201", "Please Enter evaluation name", null);
        //    }
        //    else
        //    {
        //        try
        //        {
        //            Int64 Id = ObjAType.Id;
        //            string AppearanceType = ObjAType.AppearanceType;
        //            Int64 UserId = Convert.ToInt64(res.ToString());

        //            SqlCommand cmd = new SqlCommand("AppearanceTypeMasterEdit", con);
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            cmd.Parameters.AddWithValue("@Id", Id);
        //            cmd.Parameters.AddWithValue("@AppearanceType", AppearanceType);
        //            //cmd.Parameters.AddWithValue("@IsActive", IsActive);
        //            cmd.Parameters.AddWithValue("@UserId", UserId);
        //            cmd.Parameters.AddWithValue("@UserTime", datetime);
        //            //cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);

        //            cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
        //            cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

        //            con.Open();
        //            cmd.ExecuteNonQuery();
        //            string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
        //            con.Close();

        //            if (string.Equals(strMessage, "TRUE"))
        //            {
        //                strMessage = "Your data has been modified successfully.";
        //            }

        //            return Return.returnHttp("200", strMessage.ToString(), null);
        //        }
        //        catch (Exception e)
        //        {
        //            return Return.returnHttp("201", e.Message.ToString(), null);
        //        }
        //    }
        //}
        //#endregion

        //#region AppearanceTypeDelete
        //[HttpPost]
        //public HttpResponseMessage AppearanceTypeDelete(AppearanceTypeMaster ObjAppTM)
        //{
        //    String token = Request.Headers.GetValues("token").FirstOrDefault();
        //    TokenOperation ac = new TokenOperation();

        //    string res = ac.ValidateToken(token);

        //    if (res == "0")
        //    {
        //        return Return.returnHttp("0", null, null);
        //    }
        //    try
        //    {
        //        Int64 Id = ObjAppTM.Id;
        //        Int64 UserId = Convert.ToInt64(res.ToString());

        //        SqlCommand cmd = new SqlCommand("AppearanceTypeMasterDelete", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@Id", Id);
        //        cmd.Parameters.AddWithValue("@UserId", UserId);
        //        cmd.Parameters.AddWithValue("@UserTime", datetime);
        //        //cmd.Parameters.AddWithValue("@Flag", "MstEvaluationDelete");
        //        cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
        //        cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
        //        con.Close();

        //        if (string.Equals(strMessage, "TRUE"))
        //        {
        //            strMessage = "Your data has been deleted successfully.";
        //        }

        //        return Return.returnHttp("200", strMessage.ToString(), null);
        //    }
        //    catch (Exception e)
        //    {
        //        return Return.returnHttp("201", e.Message.ToString(), null);
        //    }
        //}
        //#endregion

        //#region AppearanceTypeIsSuspended
        //[HttpPost]
        //public HttpResponseMessage AppearanceTypeIsSuspended(AppearanceTypeMaster ObjATM)
        //{
        //    String token = Request.Headers.GetValues("token").FirstOrDefault();
        //    TokenOperation ac = new TokenOperation();

        //    string res = ac.ValidateToken(token);

        //    if (res == "0")
        //    {
        //        return Return.returnHttp("0", null, null);
        //    }
        //    try
        //    {

        //        /*MstExaminationPattern mstExaminationPattern = new MstExaminationPattern();
        //        dynamic jsonData = jobj;
        //        mstExaminationPattern.Id = jsonData.Id;*/
        //        Int64 Id = Convert.ToInt64(ObjATM.Id);
        //        Int64 UserId = Convert.ToInt64(res.ToString());
        //        SqlCommand cmd = new SqlCommand("AppearanceTypeMasterActive", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@Flag", "AppearanceTypeMasterIsActiveDisable");
        //        cmd.Parameters.AddWithValue("@Id", Id);
        //        cmd.Parameters.AddWithValue("@UserId", UserId);
        //        cmd.Parameters.AddWithValue("@UserTime", datetime);

        //        cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
        //        cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        string strMessage = Convert.ToString(cmd.Parameters["@MESSAGE"].Value);
        //        con.Close();
        //        if (string.Equals(strMessage, "TRUE"))
        //        {
        //            strMessage = "Your data has been saved successfully.";
        //        }
        //        return Return.returnHttp("200", strMessage.ToString(), null);

        //    }
        //    catch (Exception e)
        //    {
        //        return Return.returnHttp("201", e.Message.ToString(), null);
        //    }
        //}
        //#endregion

        //#region AppearanceTypeIsActive
        //[HttpPost]
        //public HttpResponseMessage AppearanceTypeIsActive(AppearanceTypeMaster ObjATM)
        //{
        //    String token = Request.Headers.GetValues("token").FirstOrDefault();
        //    TokenOperation ac = new TokenOperation();

        //    string res = ac.ValidateToken(token);

        //    if (res == "0")
        //    {
        //        return Return.returnHttp("0", null, null);
        //    }
        //    try
        //    {

        //        Int64 Id = Convert.ToInt64(ObjATM.Id);
        //        Int64 UserId = Convert.ToInt64(res.ToString());

        //        SqlCommand cmd = new SqlCommand("AppearanceTypeMasterActive", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@Flag", "AppearanceTypeMasterIsActiveEnable");
        //        cmd.Parameters.AddWithValue("@Id", Id);
        //        cmd.Parameters.AddWithValue("@UserId", UserId);
        //        cmd.Parameters.AddWithValue("@UserTime", datetime);

        //        cmd.Parameters.Add("@MESSAGE", SqlDbType.NVarChar, 500);
        //        cmd.Parameters["@MESSAGE"].Direction = ParameterDirection.Output;

        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        string strMessage = Convert.ToString(cmd.Parameters["@MESSAGE"].Value);
        //        con.Close();

        //        if (string.Equals(strMessage, "TRUE"))
        //        {
        //            strMessage = "Your data has been saved successfully.";
        //        }
        //        return Return.returnHttp("200", strMessage.ToString(), null);
        //    }
        //    catch (Exception e)
        //    {
        //        return Return.returnHttp("201", e.Message.ToString(), null);
        //    }
        //}
        //#endregion
        #endregion

        #region New Code By Megha
        //[HttpPost]
        //public HttpResponseMessage MstAppearanceTypeAdd(AppearanceTypeMaster dataString)
        //{
        //    try
        //    {
        //        //String token = Request.Headers.GetValues("token").FirstOrDefault();
        //        //String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
        //        //String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
        //        //TokenOperation ac = new TokenOperation();
        //        //string res = ac.ValidateToken(token);
        //        //string resRoleToken = ac.ValidateRoleToken(userRoleToken);

        //        //if (res == "0")
        //        //{
        //        //    return Return.returnHttp("0", null, null);
        //        //}
        //        //else if (resRoleToken == "0")
        //        //{
        //        //    return Return.returnHttp("0", null, null);
        //        //}

        //        Int64 Id = dataString.Id;
        //        string AppearanceType = dataString.AppearanceType;

        //        string query = "";

        //        if (string.IsNullOrEmpty(AppearanceType))
        //        {
        //            return Return.returnHttp("201", "Please enter Appearance Type, it's mandatory.", null);
        //        }
        //        AppearanceType = AppearanceType.Replace("'", "''");
        //        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        //        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), INDIAN_ZONE);

        //        if (String.IsNullOrEmpty(Id.ToString()) || Id == 0)
        //        {
        //            query = "insert into AppearanceTypeMaster values('" + AppearanceType + "', 1,'" + datetime + "'," + 1 + ",null,null,0)";
        //        }
        //        else
        //        {
        //            query = "update AppearanceTypeMaster set AppearanceType='" + AppearanceType + "',ModifiedOn='" + datetime + "',ModifiedBy=" + 1 + " where id =" + Id;
        //        }

        //        string output = Database.insertData(query);
        //        if (output != "200")
        //            return Return.returnHttp("201", "Some Internal Issue Occured while entering your data. Please try again." + query + output, null);

        //        return Return.returnHttp("200", " AppearanceTypeMaster saved successfully.", null);

        //    }
        //    catch (Exception e)
        //    {
        //        return Return.returnHttp("201", "Some Internal Issue Occured. Please try again." + e.Message + e.StackTrace, null);
        //    }
        //}

        //[HttpPost]
        //public HttpResponseMessage MstAppearanceTypeIsActiveEnable(AppearanceTypeMaster dataString)
        //{
        //    try
        //    {
        //        //String token = Request.Headers.GetValues("token").FirstOrDefault();
        //        //String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
        //        //String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
        //        //TokenOperation ac = new TokenOperation();
        //        //string res = ac.ValidateToken(token);
        //        //string resRoleToken = ac.ValidateRoleToken(userRoleToken);

        //        //if (res == "0")
        //        //{
        //        //    return Return.returnHttp("0", null, null);
        //        //}
        //        //else if (resRoleToken == "0")
        //        //{
        //        //    return Return.returnHttp("0", null, null);
        //        //}
        //        //Int64 UserId = Convert.ToInt64(res.ToString());

        //        Int32 Id = Convert.ToInt32(dataString.Id);
        //        string output = Database.insertData("update AppearanceTypeMaster set IsActive=1 where id=" + Id);
        //        if (output != "200")
        //            return Return.returnHttp("201", "Some Internal Issue Occured while updating your data. Please try again.", null);

        //        return Return.returnHttp("200", "Set To Active", null);

        //    }
        //    catch (Exception e)
        //    {
        //        return Return.returnHttp("201", e.Message, null);
        //    }

        //}

        //[HttpPost]
        //public HttpResponseMessage MstAppearanceTypeIsActiveDisable(AppearanceTypeMaster dataString)
        //{
        //    try
        //    {
        //        //String token = Request.Headers.GetValues("token").FirstOrDefault();
        //        //String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
        //        //String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
        //        //TokenOperation ac = new TokenOperation();
        //        //string res = ac.ValidateToken(token);
        //        //string resRoleToken = ac.ValidateRoleToken(userRoleToken);

        //        //if (res == "0")
        //        //{
        //        //    return Return.returnHttp("0", null, null);
        //        //}
        //        //else if (resRoleToken == "0")
        //        //{
        //        //    return Return.returnHttp("0", null, null);
        //        //}
        //        //Int64 UserId = Convert.ToInt64(res.ToString());
        //        Int64 UserId = 1;

        //        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        //        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), INDIAN_ZONE);

        //        Int32 Id = Convert.ToInt32(dataString.Id);
        //        string output = Database.insertData("update AppearanceTypeMaster set IsActive=0,ModifiedOn='" + datetime + "',ModifiedBy=" + UserId + " where id=" + Id);
        //        if (output != "200")
        //            return Return.returnHttp("201", "Some Internal Issue Occured while updating your data. Please try again.", null);

        //        return Return.returnHttp("200", "Set To Disable", null);

        //    }
        //    catch (Exception e)
        //    {
        //        return Return.returnHttp("201", e.Message, null);
        //    }

        //}

        //[HttpPost]
        //public HttpResponseMessage MstAppearanceTypeGet()
        //{
        //    try
        //    {
        //        //String token = Request.Headers.GetValues("token").FirstOrDefault();
        //        //String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
        //        //String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
        //        //TokenOperation ac = new TokenOperation();
        //        //string res = ac.ValidateToken(token);
        //        //string resRoleToken = ac.ValidateRoleToken(userRoleToken);

        //        //if (res == "0")
        //        //{
        //        //    return Return.returnHttp("0", null, null);
        //        //}
        //        //else if (resRoleToken == "0")
        //        //{
        //        //    return Return.returnHttp("0", null, null);
        //        //}
        //        //Int64 UserId = Convert.ToInt64(res.ToString());

        //        List<AppearanceTypeMaster> examMasterList = new List<AppearanceTypeMaster>();
        //        BALAppearanceTypeMaster func = new BALAppearanceTypeMaster();
        //        //flag=1,IsDeleted=0,IsActive=null
        //        examMasterList = func.getAppearanceTypeMasterList(1, 0, null);

        //        return Return.returnHttp("200", examMasterList, null);
        //    }
        //    catch (Exception e)
        //    {
        //        return Return.returnHttp("201", e.Message, null);
        //    }
        //}

        //[HttpPost]
        //public HttpResponseMessage MstAppearanceTypeGetActive()
        //{
        //    try
        //    {
        //        //String token = Request.Headers.GetValues("token").FirstOrDefault();
        //        //String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
        //        //String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
        //        //TokenOperation ac = new TokenOperation();
        //        //string res = ac.ValidateToken(token);
        //        //string resRoleToken = ac.ValidateRoleToken(userRoleToken);

        //        //if (res == "0")
        //        //{
        //        //    return Return.returnHttp("0", null, null);
        //        //}
        //        //else if (resRoleToken == "0")
        //        //{
        //        //    return Return.returnHttp("0", null, null);
        //        //}
        //        //Int64 UserId = Convert.ToInt64(res.ToString());

        //        List<AppearanceTypeMaster> examMasterList = new List<AppearanceTypeMaster>();
        //        BALAppearanceTypeMaster func = new BALAppearanceTypeMaster();
        //        //flag=1,IsDeleted=0,IsActive=1
        //        examMasterList = func.getAppearanceTypeMasterList(1, 0, 1);

        //        return Return.returnHttp("200", examMasterList, null);
        //    }
        //    catch (Exception e)
        //    {
        //        return Return.returnHttp("201", e.Message, null);
        //    }
        //}

        //[HttpPost]
        //public HttpResponseMessage MstAppearanceTypeGetbyId(AppearanceTypeMaster dataString)
        //{
        //    try
        //    {
        //        //String token = Request.Headers.GetValues("token").FirstOrDefault();
        //        //String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
        //        //String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
        //        //TokenOperation ac = new TokenOperation();
        //        //string res = ac.ValidateToken(token);
        //        //string resRoleToken = ac.ValidateRoleToken(userRoleToken);

        //        //if (res == "0")
        //        //{
        //        //    return Return.returnHttp("0", null, null);
        //        //}
        //        //else if (resRoleToken == "0")
        //        //{
        //        //    return Return.returnHttp("0", null, null);
        //        //}
        //        //Int64 UserId = Convert.ToInt64(res.ToString());

        //        Int32 Id = Convert.ToInt32(dataString.Id);

        //        AppearanceTypeMaster element = new AppearanceTypeMaster();
        //        BALAppearanceTypeMaster func = new BALAppearanceTypeMaster();
        //        element = func.getAppearanceTypeMasterDetails(Id);

        //        return Return.returnHttp("200", element, null);
        //    }
        //    catch (Exception e)
        //    {
        //        return Return.returnHttp("201", e.Message, null);
        //    }

        //}

        //[HttpPost]
        //public HttpResponseMessage ApperanceTypeGetByProgInstancePartTermId(ExamFormMaster dataString)
        //{
        //    try
        //    {
        //        String token = Request.Headers.GetValues("token").FirstOrDefault();
        //        String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
        //        String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
        //        TokenOperation ac = new TokenOperation();
        //        string res = ac.ValidateToken(token);
        //        string resRoleToken = ac.ValidateRoleToken(userRoleToken);

        //        if (res == "0")
        //        {
        //            return Return.returnHttp("0", null, null);
        //        }
        //        else if (resRoleToken == "0")
        //        {
        //            return Return.returnHttp("0", null, null);
        //        }
        //        Int64 UserId = Convert.ToInt64(res.ToString());

        //        if (String.IsNullOrEmpty(dataString.ProgInstancePartTermId.ToString()))
        //            return Return.returnHttp("201", "Please select Course. ", null);
        //        List<AppearanceTypeMaster> AppearanceTypeMasterList = new List<AppearanceTypeMaster>();
        //        BALAppearanceTypeMaster func = new BALAppearanceTypeMaster();
        //        //flag=1,IsDeleted=0,IsActive=null
        //        AppearanceTypeMasterList = func.ApperanceTypeGetByProgInstancePartTermId(dataString.ProgInstancePartTermId);

        //        return Return.returnHttp("200", AppearanceTypeMasterList, null);
        //    }
        //    catch (Exception e)
        //    {
        //        return Return.returnHttp("201", e.Message, null);
        //    }
        //}

        [HttpPost]
        public HttpResponseMessage ApperanceTypeGetByProgPartTermId(ExamFeesConfiguration dataString)
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
                Int64 UserId = Convert.ToInt64(res.ToString());

                List<AppearanceTypeMaster> AppearanceTypeMasterList = new List<AppearanceTypeMaster>();
                BALAppearanceTypeMaster func = new BALAppearanceTypeMaster();
                //flag=1,IsDeleted=0,IsActive=null
                AppearanceTypeMasterList = func.ApperanceTypeGetByProgPartTermId(dataString.ProgrammePartTermId,dataString.FacultyExamMapId);

                return Return.returnHttp("200", AppearanceTypeMasterList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion
    }
}