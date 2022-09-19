using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;
using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using MSUISApi.BAL;
using Newtonsoft.Json.Linq;

namespace MSUISApi.Controllers
{
    public class FacultyInstituteMapController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region FacultyInstituteMapGet
        [HttpPost]
        public HttpResponseMessage FacultyInstituteMapGet()
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

                List<FacultyInstituteMap> ObjFacInstList = new List<FacultyInstituteMap>();
                BALFacultyInstituteList BalFacInst = new BALFacultyInstituteList();
                ObjFacInstList = BalFacInst.FacultyInstituteListGet();

                if (ObjFacInstList != null)
                    return Return.returnHttp("200", ObjFacInstList, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
                
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region FacultyInstituteMapAdd
        [HttpPost]
        public HttpResponseMessage FacultyInstituteMapAdd(FacultyInstituteMap fimap)
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

                Int32 FacultyId = Convert.ToInt32(fimap.FacultyId.ToString());
                Int32 InstituteId = Convert.ToInt32(fimap.InstituteId.ToString());
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("FacultyInstituteMapAdd", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (String.IsNullOrEmpty(Convert.ToString(FacultyId)))
                {
                    return Return.returnHttp("201", "Please select Faculty Name", null);
                }
                else if (String.IsNullOrEmpty(Convert.ToString(FacultyId)))
                {
                    return Return.returnHttp("201", "Please enter Institute Name", null);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                    cmd.Parameters.AddWithValue("@InstituteId", InstituteId);
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

        #region FacultyInstituteMapUpdate
        [HttpPost]
        public HttpResponseMessage FacultyInstituteMapUpdate(FacultyInstituteMap facimap)
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

                Int32 Id = Convert.ToInt32(facimap.Id.ToString());
                Int32 FacultyId = Convert.ToInt32(facimap.FacultyId.ToString());
                Int32 InstituteId = Convert.ToInt32(facimap.InstituteId.ToString());
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("FacultyInstituteMapEdit", con);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                cmd.Parameters.AddWithValue("@InstituteId", InstituteId);
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

        #region FacultyInstituteMapDelete
        [HttpPost]
        public HttpResponseMessage FacultyInstituteMapDelete(FacultyInstituteMap fintm)
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

                Int32 Id = fintm.Id;
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("FacultyInstituteMapDelete", con);
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

        #region FacultyInstituteMapIsSuspended
        [HttpPost]
        public HttpResponseMessage FacultyInstituteMapIsSuspended(FacultyInstituteMap ObjFacultyInstituteMap)
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

                Int32 Id = Convert.ToInt32(ObjFacultyInstituteMap.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("FacultyInstituteMapActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "FacultyInstituteMapIsActiveDisable");
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

        #region FacultyInstituteMapIsActive
        [HttpPost]
        public HttpResponseMessage FacultyInstituteMapIsActive(FacultyInstituteMap ObjFacultyInstituteMap)
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

                Int32 Id = Convert.ToInt32(ObjFacultyInstituteMap.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("FacultyInstituteMapActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "FacultyInstituteMapIsActiveEnable");
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

        /*#region FacultyGet
        [HttpPost]
        public HttpResponseMessage FacultyGet()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstFacultyGet", con);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);


                List<MstFaculty> ObjLstFaculty = new List<MstFaculty>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstFaculty objFaculty = new MstFaculty();

                        objFaculty.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objFaculty.FacultyName = Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objFaculty.FacultyCode = Convert.ToString(Dt.Rows[i]["FacultyCode"]);
                        objFaculty.FacultyAddress = Convert.ToString(Dt.Rows[i]["FacultyAddress"]);
                        objFaculty.CityName = Convert.ToString(Dt.Rows[i]["CityName"]);
                        objFaculty.Pincode = Convert.ToInt32(Dt.Rows[i]["Pincode"]);
                        objFaculty.FacultyContactNo = Convert.ToString(Dt.Rows[i]["FacultyContactNo"]);
                        objFaculty.FacultyFaxNo = Convert.ToString(Dt.Rows[i]["FacultyFaxNo"]);
                        objFaculty.FacultyEmail = Convert.ToString(Dt.Rows[i]["FacultyEmail"]);
                        objFaculty.FacultyUrl = Convert.ToString(Dt.Rows[i]["FacultyUrl"]);
                        objFaculty.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
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
        #endregion*/

        /*#region MstInstituteGet
        [HttpPost]
        public HttpResponseMessage MstInstituteGet()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter cmdda = new SqlDataAdapter("MstInstituteGet", con);

                cmdda.SelectCommand.CommandType = CommandType.StoredProcedure;

                DataTable Dt = new DataTable();
                cmdda.Fill(Dt);

                List<MstInstitute> ObjListMstInstitute = new List<MstInstitute>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstInstitute ObjMstInst = new MstInstitute();
                        ObjMstInst.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        ObjMstInst.InstituteName = Convert.ToString(Dt.Rows[i]["InstituteName"]);
                        ObjMstInst.InstituteCode = Convert.ToString(Dt.Rows[i]["InstituteCode"]);
                        ObjMstInst.InstituteAddress = Convert.ToString(Dt.Rows[i]["InstituteAddress"]);
                        ObjMstInst.CityName = Convert.ToString(Dt.Rows[i]["CityName"]);
                        ObjMstInst.Pincode = Convert.ToInt32(Dt.Rows[i]["Pincode"]);
                        ObjMstInst.InstituteContactNo = Convert.ToString(Dt.Rows[i]["InstituteContactNo"]);
                        ObjMstInst.InstituteFaxNo = Convert.ToString(Dt.Rows[i]["InstituteFaxNo"]);
                        ObjMstInst.InstituteEmail = Convert.ToString(Dt.Rows[i]["InstituteEmail"]);
                        ObjMstInst.InstituteUrl = Convert.ToString(Dt.Rows[i]["InstituteUrl"]);
                        ObjMstInst.IsActiveSts = Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        ObjMstInst.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjMstInst.IsDeleted = Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);

                        ObjListMstInstitute.Add(ObjMstInst);
                    }
                }
                return Return.returnHttp("200", ObjListMstInstitute, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion*/

        #region Faculty Institute Map Get By Id
        [HttpPost]
        public HttpResponseMessage FacultyInstituteMapGetbyId(FacultyInstituteMap FacInstMap)
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

                FacultyInstituteMap ObjFacInstList = new FacultyInstituteMap();
                BALFacultyInstituteList BalFacInst = new BALFacultyInstituteList();
                ObjFacInstList = BalFacInst.FacultyInstituteListGetById(FacInstMap.Id);

                if (ObjFacInstList != null)
                    return Return.returnHttp("200", ObjFacInstList, null);
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