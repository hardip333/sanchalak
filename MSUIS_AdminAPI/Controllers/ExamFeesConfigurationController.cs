using MSUIS_TokenManager.App_Start;
using MSUISApi.BAL;
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
    public class ExamFeesConfigurationController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        
        #region newCode By Stored Procedure - Megha
        [HttpPost]
        public HttpResponseMessage ExamFeeHeadConfigurationAdd(ExamFeesConfiguration ObjExamFeesConfiguration)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            if (String.IsNullOrWhiteSpace(ObjExamFeesConfiguration.AppearanceTypeId.ToString()))
            {
                return Return.returnHttp("201", "Please select AppearanceType ", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjExamFeesConfiguration.ProgrammePartTermId)))
            {
                return Return.returnHttp("201", "Please enter programme part term", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjExamFeesConfiguration.ExamMasterId)))
            {
                return Return.returnHttp("201", "Please enter exam master", null);
            }
            else
            {
                try
                {
                     Int64 UserId = Convert.ToInt64(res.ToString());
                    //Int64 UserId = 1;

                    int AppearanceTypeId = Convert.ToInt32(ObjExamFeesConfiguration.AppearanceTypeId);

                    int ProgrammeInstancePartTermId = Convert.ToInt32(ObjExamFeesConfiguration.ProgrammePartTermId);
                    int ExamMasterId = Convert.ToInt32(ObjExamFeesConfiguration.ExamMasterId);

                    SqlCommand cmd = new SqlCommand("ExamFeeConfigurationAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@AppearanceTypeMasterId", ObjExamFeesConfiguration.AppearanceTypeId);
                    cmd.Parameters.AddWithValue("@ProgrammePartTermId", ObjExamFeesConfiguration.ProgrammePartTermId);
                    cmd.Parameters.AddWithValue("@ExamMasterId",ObjExamFeesConfiguration.ExamMasterId);
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

        [HttpPost]
        public HttpResponseMessage ExamFeeHeadConfigurationEdit(ExamFeesConfiguration ObjExamFeesConfig)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            if (String.IsNullOrWhiteSpace(ObjExamFeesConfig.AppearanceTypeId.ToString()))
            {
                return Return.returnHttp("201", "Please select AppearanceType ", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjExamFeesConfig.ProgrammePartTermId)))
            {
                return Return.returnHttp("201", "Please enter programme  part term", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjExamFeesConfig.ExamMasterId)))
            {
                return Return.returnHttp("201", "Please enter exam master", null);
            }
            else
            {
                try
                {
                      Int64 UserId = Convert.ToInt64(res.ToString());
                     //Int64 UserId = 1;

                    int Id = Convert.ToInt32(ObjExamFeesConfig.Id);
                    int AppearanceTypeId = Convert.ToInt32(ObjExamFeesConfig.AppearanceTypeId);

                    int ProgrammeInstancePartTermId = Convert.ToInt32(ObjExamFeesConfig.ProgrammePartTermId);
                    int ExamMasterId = Convert.ToInt32(ObjExamFeesConfig.ExamMasterId);

                    SqlCommand cmd = new SqlCommand("ExamFeeConfigurationEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", ObjExamFeesConfig.Id);
                    cmd.Parameters.AddWithValue("@AppearanceTypeMasterId", ObjExamFeesConfig.AppearanceTypeId);
                    cmd.Parameters.AddWithValue("@ProgrammePartTermId", ObjExamFeesConfig.ProgrammePartTermId);
                    cmd.Parameters.AddWithValue("@ExamMasterId", ObjExamFeesConfig.ExamMasterId);
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

        [HttpPost]
        public HttpResponseMessage ExamFeesConfigurationListGet(ExamFeesConfiguration dataString)
        {
            try
            {
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                string facultydepartintituteid = Request.Headers.GetValues("facultydepartintituteid").FirstOrDefault();
                string userroletoken = Request.Headers.GetValues("userroletoken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                string resroletoken = ac.ValidateRoleToken(userroletoken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resroletoken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int64 userid = Convert.ToInt64(res.ToString());

                List<ExamFeesConfiguration> ExamFeesConfigurationList = new List<ExamFeesConfiguration>();
                BALExamFeesConfiguration func = new BALExamFeesConfiguration();
                //flag=1,IsDeleted=0,IsActive=null
                ExamFeesConfigurationList = func.getExamFeesConfigurationList("admin", 0, null, dataString.ExamMasterId,dataString.AppearanceTypeId,dataString.ProgrammePartTermId);

                return Return.returnHttp("200", ExamFeesConfigurationList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage ExamFeesConfigurationListGetActive(ExamFeesConfiguration dataString)
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

                List<ExamFeesConfiguration> ExamFeesConfigurationList = new List<ExamFeesConfiguration>();
                BALExamFeesConfiguration func = new BALExamFeesConfiguration();
                //flag=1,IsDeleted=0,IsActive=1
                ExamFeesConfigurationList = func.getExamFeesConfigurationList("admin", 0, 1, dataString.ExamMasterId, dataString.AppearanceTypeId, dataString.ProgrammePartTermId);

                return Return.returnHttp("200", ExamFeesConfigurationList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        /// ExamFeeHeadIsActive and ExamFeeHeadIsInactive Created by Jaydev on 25-11-21

        [HttpPost]
        public HttpResponseMessage ExamFeeConfigurationIsActive(ExamFeesConfiguration dataString)
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

                Int32 Id = (int)dataString.Id;
                //Int64 UserId = 1;
                Int64 UserId =Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("ExamFeeConfigurationActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "ExamFeeConfigurationIsActive");
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
                    strMessage = "Your data has been Active Successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }


        [HttpPost]
        public HttpResponseMessage ExamFeeConfigurationIsInactive(ExamFeesConfiguration dataString)
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

                Int32 Id = (int)dataString.Id;
                //Int64 UserId = 1;
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("ExamFeeConfigurationActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "ExamFeeConfigurationIsInactive");
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
                    strMessage = "Your data has been Inactive Successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }


        #endregion
    }
}