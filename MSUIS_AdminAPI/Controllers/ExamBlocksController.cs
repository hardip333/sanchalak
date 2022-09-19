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
    public class ExamBlocksController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region newCode By Stored Procedure - Megha

        [HttpPost]
        public HttpResponseMessage ExamBlocksAdd(ExamBlocks ObjExamBlocks)
        {
            string token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                 return Return.returnHttp("0", null, null);
            }
            if (String.IsNullOrWhiteSpace(ObjExamBlocks.Name))
            {
                return Return.returnHttp("201", "Please enter name", null);
            }
            else if (String.IsNullOrWhiteSpace(ObjExamBlocks.ExamVenueId.ToString()))
            {
                return Return.returnHttp("201", "Please select exam venue.", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjExamBlocks.RoomNo)))
            {
                return Return.returnHttp("201", "Please enter roomNo.", null);
            }
            else
            {
                try
                {
                    Int64 UserId = Convert.ToInt64(res.ToString());
                    //Int64 UserId = 1;



                    SqlCommand cmd = new SqlCommand("ExamBlocksAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ExamVenueId", ObjExamBlocks.ExamVenueId);
                    cmd.Parameters.AddWithValue("@Name", ObjExamBlocks.Name);
                    cmd.Parameters.AddWithValue("@RoomNo", ObjExamBlocks.RoomNo);
                    cmd.Parameters.AddWithValue("@SequenceNo", ObjExamBlocks.SequenceNo);
                    cmd.Parameters.AddWithValue("@TotalBench", ObjExamBlocks.TotalBench);
                    cmd.Parameters.AddWithValue("@IsDualCapacityAvailable", ObjExamBlocks.IsDualCapacityAvailable);
                    cmd.Parameters.AddWithValue("@IncludeInAutoAllocation", ObjExamBlocks.IncludeInAutoAllocation);
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
        public HttpResponseMessage ExamBlocksEdit(ExamBlocks ObjExamBlocks)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            if (String.IsNullOrWhiteSpace(ObjExamBlocks.Name))
            {
                return Return.returnHttp("201", "Please enter name", null);
            }
            else if (String.IsNullOrWhiteSpace(ObjExamBlocks.ExamVenueId.ToString()))
            {
                return Return.returnHttp("201", "Please select exam venue.", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjExamBlocks.RoomNo.ToString())))
            {
                return Return.returnHttp("201", "Please enter roomNo.", null);
            }
            else
            {
                try
                {
                    Int64 UserId = Convert.ToInt64(res.ToString());
                    //Int64 UserId = 1;

                    SqlCommand cmd = new SqlCommand("ExamBlocksEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", ObjExamBlocks.Id);
                    cmd.Parameters.AddWithValue("@ExamVenueId", ObjExamBlocks.ExamVenueId);
                    cmd.Parameters.AddWithValue("@Name", ObjExamBlocks.Name);
                    cmd.Parameters.AddWithValue("@RoomNo", ObjExamBlocks.RoomNo);
                    cmd.Parameters.AddWithValue("@SequenceNo", ObjExamBlocks.SequenceNo);
                    cmd.Parameters.AddWithValue("@TotalBench", ObjExamBlocks.TotalBench);
                    cmd.Parameters.AddWithValue("@IsDualCapacityAvailable", ObjExamBlocks.IsDualCapacityAvailable);
                    cmd.Parameters.AddWithValue("@IncludeInAutoAllocation", ObjExamBlocks.IncludeInAutoAllocation);
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
        public HttpResponseMessage ExamBlocksListGet(ExamBlocks dataString)
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

                List<ExamBlocks> ExamBlocksList = new List<ExamBlocks>();
                BALExamBlocks func = new BALExamBlocks();
                //flag=1,IsDeleted=0,IsActive=null
                ExamBlocksList = func.getExamBlocksList("admin", 0, null, dataString.ExamVenueId);

                return Return.returnHttp("200", ExamBlocksList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage ExamBlocksListGetByInstId(ExamBlocks dataString)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                String InstituteId = "";
                    //Request.Headers.GetValues("InstituteId").FirstOrDefault();
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

                List<ExamBlocks> ExamBlocksList = new List<ExamBlocks>();
                BALExamBlocks func = new BALExamBlocks();
                //flag=1,IsDeleted=0,IsActive=null
                ExamBlocksList = func.getExamBlocksListByInstId("admin", 0, null, dataString.ExamVenueId, UserId);

                return Return.returnHttp("200", ExamBlocksList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage ExamBlocksListGetActive(ExamBlocks dataString)
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

                List<ExamBlocks> ExamBlocksList = new List<ExamBlocks>();
                BALExamBlocks func = new BALExamBlocks();
                //flag=1,IsDeleted=0,IsActive=1
                ExamBlocksList = func.getExamBlocksList("admin", 0, 1, dataString.ExamVenueId);

                return Return.returnHttp("200", ExamBlocksList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        /// ExamBlocksIsActive and ExamBlocksIsInactive Created by Jaydev on 24-11-21

        [HttpPost]
        public HttpResponseMessage ExamBlocksIsActive(ExamBlocks dataString)
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


                Int64 UserId = Convert.ToInt64(res.ToString());
                //Int64 UserId = 1;

                SqlCommand cmd = new SqlCommand("ExamBlocksActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "ExamBlocksIsActive");
                cmd.Parameters.AddWithValue("@Id", dataString.Id);
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
        public HttpResponseMessage ExamBlocksIsInactive(ExamBlocks dataString)
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

                Int64 UserId = Convert.ToInt64(res.ToString());
                //Int64 UserId = 1;

                SqlCommand cmd = new SqlCommand("ExamBlocksActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "ExamBlocksIsInactive");
                cmd.Parameters.AddWithValue("@Id",dataString. Id);
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