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

namespace MSUISApi.Controllers
{
    public class MstPreferenceCodeMapController : ApiController
    {

        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        Validation validation = new Validation();
        SqlDataAdapter sda = new SqlDataAdapter();

        #region MstPreferenceCodeMapGet
        [HttpPost]
        public HttpResponseMessage MstPreferenceCodeMapGet()
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

                SqlCommand cmd = new SqlCommand("MstPreferenceCodeMapGet", con);
                cmd.CommandType = CommandType.StoredProcedure;

                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<MstPreferenceCodeMap> ObjPCMLists = new List<MstPreferenceCodeMap>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstPreferenceCodeMap Objpcm = new MstPreferenceCodeMap();

                        Objpcm.Id = Convert.ToInt64(dr["Id"].ToString());
                        Objpcm.PreferenceId = Convert.ToInt32(dr["PreferenceId"].ToString());
                        Objpcm.CodeId = Convert.ToInt32(dr["CodeId"]);
                        Objpcm.ProgInstPartTermId = Convert.ToInt32(dr["ProgInstPartTermId"]);
                        Objpcm.FacultyId = Convert.ToInt32(dr["FacultyId"]);
                        Objpcm.AcademicYearId = Convert.ToInt32(dr["AcademicYearId"]);
                        Objpcm.GroupName = dr["GroupName"].ToString();
                        Objpcm.GroupCode = dr["GroupCode"].ToString();
                        Objpcm.InstancePartTermName = dr["InstancePartTermName"].ToString();
                        Objpcm.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
                        Objpcm.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        Objpcm.IsActiveSts = dr["IsActiveSts"].ToString();

                        count = count + 1;
                        Objpcm.IndexId = count;

                        ObjPCMLists.Add(Objpcm);
                    }

                }
                return Return.returnHttp("200", ObjPCMLists, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region MstPreferenceCodeMapAdd

        [HttpPost]
        public HttpResponseMessage MstPreferenceCodeMapAdd(MstPreferenceCodeMap pcm)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                Validation validation = new Validation();
                Int32 PreferenceId = Convert.ToInt32(pcm.PreferenceId.ToString());
                Int32 CodeId = Convert.ToInt32(pcm.CodeId.ToString());
                Int64 ProgInstPartTermId = Convert.ToInt64(pcm.ProgInstPartTermId.ToString());
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                SqlCommand cmd = new SqlCommand("MstPreferenceCodeMapAdd", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (String.IsNullOrEmpty(Convert.ToString(PreferenceId)))
                {
                    return Return.returnHttp("201", "Please select Programme Part", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(CodeId)))
                {
                    return Return.returnHttp("201", "Please enter Part Term Name", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ProgInstPartTermId)))
                {
                    return Return.returnHttp("201", "Please enter paper code", null);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@PreferenceId", PreferenceId);
                    cmd.Parameters.AddWithValue("@CodeId", CodeId);
                    cmd.Parameters.AddWithValue("@ProgInstPartTermId", ProgInstPartTermId);
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
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region MstPreferenceCodeMapUpdate
        [HttpPost]
        public HttpResponseMessage MstPreferenceCodeMapUpdate(MstPreferenceCodeMap prbm)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                Int64 Id = Convert.ToInt64(prbm.Id.ToString());
                Int32 PreferenceId = Convert.ToInt32(prbm.PreferenceId.ToString());
                Int32 CodeId = Convert.ToInt32(prbm.CodeId.ToString());
                Int64 ProgInstPartTermId = Convert.ToInt64(prbm.ProgInstPartTermId.ToString());
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                SqlCommand cmd = new SqlCommand("MstPreferenceCodeMapEdit", con);
                cmd.CommandType = CommandType.StoredProcedure;


                if (String.IsNullOrEmpty(Convert.ToString(PreferenceId)))
                {
                    return Return.returnHttp("201", "Please select Programme Part", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(CodeId)))
                {
                    return Return.returnHttp("201", "Please enter Part Term Name", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(ProgInstPartTermId)))
                {
                    return Return.returnHttp("201", "Please enter paper code", null);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@PreferenceId", PreferenceId);
                    cmd.Parameters.AddWithValue("@CodeId", CodeId);
                    cmd.Parameters.AddWithValue("@ProgInstPartTermId", ProgInstPartTermId);
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
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region MstPreferenceCodeMapDelete
        [HttpPost]
        public HttpResponseMessage MstPreferenceCodeMapDelete(MstPreferenceCodeMap mpcmap)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                Int64 Id = mpcmap.Id;
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                SqlCommand cmd = new SqlCommand("MstPreferenceCodeMapDelete", con);
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
                    strMessage = "Your data has been saved successfully.";
                }

                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region MstPreferenceCodeMapIsSuspended
        [HttpPost]
        public HttpResponseMessage MstPreferenceCodeMapIsSuspended(MstPreferenceCodeMap mpcm)
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

                /*MstExaminationPattern mstExaminationPattern = new MstExaminationPattern();
                dynamic jsonData = jobj;
                mstExaminationPattern.Id = jsonData.Id;*/
                Int64 Id = Convert.ToInt64(mpcm.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());
                SqlCommand cmd = new SqlCommand("MstPreferenceCodeMapActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstPreferenceCodeMapIsActiveDisable");
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

        #region MstPreferenceCodeMapIsActive
        [HttpPost]
        public HttpResponseMessage MstPreferenceCodeMapIsActive(MstPreferenceCodeMap mpcm)
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

                Int32 Id = Convert.ToInt32(mpcm.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("MstPreferenceCodeMapActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstPreferenceCodeMapIsActiveEnable");
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

        #region MstPreferenceGroupGet
        [HttpPost]
        public HttpResponseMessage MstPreferenceGroupGet(MstPreferenceGroup ObjMPG)
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
                SqlCommand cmd = new SqlCommand("MstPreferenceGroupGetByFacultyId", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FacultyId", ObjMPG.FacultyId);

                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<MstPreferenceGroup> ObjPrefGroup = new List<MstPreferenceGroup>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstPreferenceGroup ObjPrefG = new MstPreferenceGroup();
                        ObjPrefG.Id = Convert.ToInt32(dr["Id"].ToString());
                        ObjPrefG.GroupName = dr["GroupName"].ToString();
                        //ObjPrefG.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
                        // ObjPrefG.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        // ObjPrefG.IsActiveSts = dr["IsActiveSts"].ToString();
                        ObjPrefGroup.Add(ObjPrefG);
                    }
                }
                return Return.returnHttp("200", ObjPrefGroup, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion


        #region MstGroupCodeGet
        [HttpPost]
        public HttpResponseMessage MstGroupCodeGet()
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
                SqlCommand cmd = new SqlCommand("MstGroupCodeGet", con);

                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<MstGroupCode> ObjGroupList = new List<MstGroupCode>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstGroupCode ObjGrpC = new MstGroupCode();
                        ObjGrpC.Id = Convert.ToInt32(dr["Id"].ToString());
                        ObjGrpC.GroupCode = dr["GroupCode"].ToString();
                        ObjGrpC.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
                        ObjGrpC.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjGrpC.IsActiveSts = dr["IsActiveSts"].ToString();
                        ObjGroupList.Add(ObjGrpC);
                    }
                }
                return Return.returnHttp("200", ObjGroupList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion


        #region ProgrammeInstancePartTermGet
        [HttpPost]
        public HttpResponseMessage IncProgrammeInstancePartTermGet(IncProgrammeInstancePartTerm PIPT)
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
                SqlCommand cmd = new SqlCommand("IncProgInstPartTermFIdAId", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", PIPT.FacultyId);
                cmd.Parameters.AddWithValue("@AcademicYearId", PIPT.AcademicYearId);
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<IncProgrammeInstancePartTerm> ObjLstProgInstPartTerm = new List<IncProgrammeInstancePartTerm>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        IncProgrammeInstancePartTerm ObjProgInstPartTerm = new IncProgrammeInstancePartTerm();
                        ObjProgInstPartTerm.ProgInstPartTermId = Convert.ToInt64(dr["Id"]);

                        ObjProgInstPartTerm.InstancePartTermName = Convert.ToString(dr["InstancePartTermName"]);

                        ObjLstProgInstPartTerm.Add(ObjProgInstPartTerm);

                    }
                }
                return Return.returnHttp("200", ObjLstProgInstPartTerm, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region MstFacultyGet
        [HttpPost]
        public HttpResponseMessage FacultyGetById(MstFaculty MF)
        {
            //MstFaculty modelobj = new MstFaculty(); 
            try
            {
                //modelobj.Id = MF.Id;
                SqlCommand cmd = new SqlCommand("MstFacGetbyId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", MF.Id);
                sda.SelectCommand = cmd;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                List<MstFaculty> ObjLstF = new List<MstFaculty>();
                //dt.Rows.Add("0", "All Faculty");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstFaculty ObjF = new MstFaculty();
                        ObjF.Id = Convert.ToInt32(dr["Id"]);
                        ObjF.FacultyName = (dr["FacultyName"].ToString());

                        ObjLstF.Add(ObjF);
                    }

                }

                return Return.returnHttp("200", ObjLstF, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region AcademicYearGet For DropDown
        [HttpPost]
        public HttpResponseMessage AcademicYearGetForDropDown()
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                List<AcademicYear> ObjListAcadYear = new List<AcademicYear>();
                BALAcademicYear ObjBalAcadYearList = new BALAcademicYear();
                ObjListAcadYear = ObjBalAcadYearList.AcademicYearGetForDropDown();

                if (ObjListAcadYear != null)
                    return Return.returnHttp("200", ObjListAcadYear, null);
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