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
    public class MstBoardOfStudySubjectMapController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region BoardOfStudySubjectMapGet
        [HttpPost]
        public HttpResponseMessage BoardOfStudySubjectMapGet()
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

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter cmdda = new SqlDataAdapter("BoardofStudySubjectMapGet", con);

                cmdda.SelectCommand.CommandType = CommandType.StoredProcedure;

                DataTable Dt = new DataTable();
                cmdda.Fill(Dt);
                Int32 count = 0;
                List<BoardOfStudySubjectMap> ObjListBosSubject = new List<BoardOfStudySubjectMap>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        BoardOfStudySubjectMap ObjBosSub = new BoardOfStudySubjectMap();
                        ObjBosSub.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        ObjBosSub.BoardofStudyId = (Convert.ToString(Dt.Rows[i]["BoardofStudyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["BoardofStudyId"]);
                        ObjBosSub.BoardName = (Convert.ToString(Dt.Rows[i]["BoardName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["BoardName"]);
                        ObjBosSub.SubjectId = (Convert.ToString(Dt.Rows[i]["SubjectId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["SubjectId"]);
                        ObjBosSub.SubjectName = (Convert.ToString(Dt.Rows[i]["SubjectName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["SubjectName"]);
                        ObjBosSub.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjBosSub.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);
                        count = count + 1;
                        ObjBosSub.IndexId = count;
                        ObjListBosSubject.Add(ObjBosSub);
                    }
                }
                return Return.returnHttp("200", ObjListBosSubject, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region BoardOfStudySubjectMapAdd
        [HttpPost]
        public HttpResponseMessage BoardOfStudySubjectMapAdd(BoardOfStudySubjectMap ObjListBosSubject)
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

                Int32 BOSId = Convert.ToInt32(ObjListBosSubject.BoardofStudyId.ToString());
                Int32 SubjectId = Convert.ToInt32(ObjListBosSubject.SubjectId.ToString());
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("BoardofStudySubjectMapAdd", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (String.IsNullOrEmpty(Convert.ToString(BOSId)))
                {
                    return Return.returnHttp("201", "Please select Faculty Name", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(SubjectId)))
                {
                    return Return.returnHttp("201", "Please enter Institute Name", null);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@BoardofStudyId", BOSId);
                    cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
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
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region BoardOfStudySubjectMapUpdate
        [HttpPost]
        public HttpResponseMessage BoardOfStudySubjectMapUpdate(BoardOfStudySubjectMap ObjListBosSubject)
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

                Int32 Id = Convert.ToInt32(ObjListBosSubject.Id.ToString());
                Int32 BOSId = Convert.ToInt32(ObjListBosSubject.BoardofStudyId.ToString());
                Int32 SubjectId = Convert.ToInt32(ObjListBosSubject.SubjectId.ToString());
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("BoardofStudySubjectMapEdit", con);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@BoardofStudyId", BOSId);
                cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
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
        #endregion

        #region BoardofStudySubjectMapDelete
        [HttpPost]
        public HttpResponseMessage BoardOfStudySubjectMapDelete(BoardOfStudySubjectMap ObjListBosSubject)
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

                Int32 Id = ObjListBosSubject.Id;
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("BoardofStudySubjectMapDelete", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
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

        #region BoardofStudySubjectMapIsSuspended
        [HttpPost]
        public HttpResponseMessage BoardofStudySubjectMapIsSuspended(BoardOfStudySubjectMap ObjListBosSubject)
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

                Int32 Id = Convert.ToInt32(ObjListBosSubject.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("BoardofStudySubjectMapActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "BoardofStudySubjectMapIsActiveDisable");
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

        #region BoardofStudySubjectMapIsActive
        [HttpPost]
        public HttpResponseMessage BoardofStudySubjectMapIsActive(BoardOfStudySubjectMap ObjListBosSubject)
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

                Int32 Id = Convert.ToInt32(ObjListBosSubject.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("BoardofStudySubjectMapActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "BoardofStudySubjectMapIsActiveEnable");
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

        #region BoardofStudySubjectMap Get By Id
        [HttpPost]
        public HttpResponseMessage BoardOfStudySubjectMapGetbyId(BoardOfStudySubjectMap BosSubMap)
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

                BoardOfStudySubjectMap ObjBosSubList = new BoardOfStudySubjectMap();
                BALBoardOfStudySubjectList BalBosSub = new BALBoardOfStudySubjectList();
                ObjBosSubList = BalBosSub.BosSubjectListGetById(BosSubMap.Id);

                if (ObjBosSubList != null)
                    return Return.returnHttp("200", ObjBosSubList, null);
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
