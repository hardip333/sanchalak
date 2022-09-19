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
using System.Web.WebPages;
using MSUISApi.BAL;

namespace MSUISApi.Controllers
{
    public class MstBoardOfStudyController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region BOS Get
        [HttpPost]
        public HttpResponseMessage MstBoardOfStudyGet()
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

                List<BoardOfStudy> ObjLstBos = new List<BoardOfStudy>();
                BALBoardOfStudyList ObjBalBosList = new BALBoardOfStudyList();
                ObjLstBos = ObjBalBosList.BOSListGet();

                if (ObjLstBos != null)
                    return Return.returnHttp("200", ObjLstBos, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);                
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion        

        #region BOS Get For Drop Down
        [HttpPost]
        public HttpResponseMessage MstBoardOfStudyGetForDropDown()
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

                List<BoardOfStudy> ObjLstBos = new List<BoardOfStudy>();
                BALBoardOfStudyList ObjBalBosList = new BALBoardOfStudyList();
                ObjLstBos = ObjBalBosList.BOSListGetForDropDown();

                if (ObjLstBos != null)
                    return Return.returnHttp("200", ObjLstBos, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion        

        #region BOS Add
        [HttpPost]
        public HttpResponseMessage MstBoardOfStudyAdd(BoardOfStudy BoardOfStudy)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            if (String.IsNullOrWhiteSpace(Convert.ToString(BoardOfStudy.BoardName)))
            {
                return Return.returnHttp("201", "Please enter board name", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(BoardOfStudy.FacultyId)))
            {
                return Return.returnHttp("201", "Please select faculty", null);
            }
            else
            {
                try
                {
                    String BoardName = Convert.ToString(BoardOfStudy.BoardName);
                    Int32 FacultyId = Convert.ToInt32(BoardOfStudy.FacultyId);

                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("MstBoardOfStudyAdd", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BoardName", BoardName);
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

        #region BOS Edit
        [HttpPost]
        public HttpResponseMessage MstBoardOfStudyEdit(BoardOfStudy BoardOfStudy)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            if (String.IsNullOrWhiteSpace(Convert.ToString(BoardOfStudy.BoardName)))
            {
                return Return.returnHttp("201", "Please enter board name", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(BoardOfStudy.FacultyId)))
            {
                return Return.returnHttp("201", "Please select faculty", null);
            }
            else
            {
                try
                {
                    Int32 Id = Convert.ToInt32(BoardOfStudy.Id);
                    String BoardName = Convert.ToString(BoardOfStudy.BoardName);
                    Int32 FacultyId = Convert.ToInt32(BoardOfStudy.FacultyId);

                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("MstBoardOfStudyEdit", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@BoardName", BoardName);
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

        #region BOS Delete
        [HttpPost]
        public HttpResponseMessage MstBoardOfStudyDelete(BoardOfStudy BoardOfStudy)
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
                Int32 Id = Convert.ToInt32(BoardOfStudy.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("MstBoardOfStudyDelete", Con);
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

        #region BOS Suspend
        [HttpPost]
        public HttpResponseMessage MstBoardOfStudyIsSuspended(BoardOfStudy BoardOfStudy)
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

                Int32 Id = Convert.ToInt32(BoardOfStudy.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("MstBoardOfStudyActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstBoardOfStudyIsActiveDisable");
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

        #region BOS Active
        [HttpPost]
        public HttpResponseMessage MstBoardOfStudyIsActive(BoardOfStudy BoardOfStudy)
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
                Int32 Id = Convert.ToInt32(BoardOfStudy.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("MstBoardOfStudyActive", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstBoardOfStudyIsActiveEnable");
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

        #region BOS Get By Id
        [HttpPost]
        public HttpResponseMessage MstBoardOfStudyGetbyId(BoardOfStudy BoardOfStudy)
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
                BoardOfStudy ObjLstBos = new BoardOfStudy();
                BALBoardOfStudyList ObjBalBosList = new BALBoardOfStudyList();
                ObjLstBos = ObjBalBosList.BosGetbyId(BoardOfStudy.Id);

                if (ObjLstBos != null)
                    return Return.returnHttp("200", ObjLstBos, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region BOS Get By FacultyId
        [HttpPost]
        public HttpResponseMessage MstBoardOfStudyGetbyFacultyId(BoardOfStudy BoardOfStudy)
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
                List<BoardOfStudy> ObjLstBos = new List<BoardOfStudy>();
                BALBoardOfStudyList ObjBalBosList = new BALBoardOfStudyList();
                ObjLstBos = ObjBalBosList.MstBoardofStudyListGetByFacultyId(BoardOfStudy.FacultyId);

                if (ObjLstBos != null)
                    return Return.returnHttp("200", ObjLstBos, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        /*#region Faculty List Get
        [HttpPost]
        public HttpResponseMessage MstFacultyListGet()
        {
            try
            {

                SqlCommand cmd = new SqlCommand("MstFacultyGet", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                Da.SelectCommand = cmd;
                Da.Fill(Dt);


                List<MstFaculty> ObjLstMstFaculty = new List<MstFaculty>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstFaculty ObjMstFaculty = new MstFaculty();

                        ObjMstFaculty.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        ObjMstFaculty.FacultyName = Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        ObjMstFaculty.FacultyCode = Convert.ToString(Dt.Rows[i]["FacultyCode"]);
                        ObjMstFaculty.FacultyAddress = Convert.ToString(Dt.Rows[i]["FacultyAddress"]);
                        ObjMstFaculty.CityName = Convert.ToString(Dt.Rows[i]["CityName"]);
                        ObjMstFaculty.Pincode = Convert.ToInt32(Dt.Rows[i]["Pincode"]);
                        ObjMstFaculty.FacultyContactNo = Convert.ToString(Dt.Rows[i]["FacultyContactNo"]);
                        ObjMstFaculty.FacultyFaxNo = Convert.ToString(Dt.Rows[i]["FacultyFaxNo"]);
                        ObjMstFaculty.FacultyEmail = Convert.ToString(Dt.Rows[i]["FacultyEmail"]);
                        ObjMstFaculty.FacultyUrl = Convert.ToString(Dt.Rows[i]["FacultyUrl"]);
                        //ObjMstFaculty.IsActiveSts = Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        ObjMstFaculty.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        //ObjMstFaculty.IsDeleted = Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);

                        ObjLstMstFaculty.Add(ObjMstFaculty);
                    }

                }

                return Return.returnHttp("200", ObjLstMstFaculty, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion*/
    }
}
