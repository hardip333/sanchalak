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
    public class MstProgrammePartTermGroupMapController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region MstProgrammePartTermGroupMapGetByPPTId
        [HttpPost]
        public HttpResponseMessage MstProgrammePartTermGroupMapGetByPPTId()
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


                SqlCommand Cmd = new SqlCommand("MstProgrammePartTermGroupMapGetByPPTId", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);


                List<MstProgrammePartTermGroupMap> ObjLstPPTID = new List<MstProgrammePartTermGroupMap>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgrammePartTermGroupMap objPPTID = new MstProgrammePartTermGroupMap();

                        objPPTID.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objPPTID.GroupName = Convert.ToString(Dt.Rows[i]["GroupName"]);
                        objPPTID.ProgrammePartTermId = Convert.ToInt32(Dt.Rows[i]["ProgrammePartTermId"]);
                        objPPTID.PartTermName = Convert.ToString(Dt.Rows[i]["PartTermName"]);
                        ObjLstPPTID.Add(objPPTID);
                    }
                }
                return Return.returnHttp("200", ObjLstPPTID, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region MstProgrammePartTermGroupMapAdd
        [HttpPost]
        public HttpResponseMessage MstProgrammePartTermGroupMapAdd(MstProgrammePartTermGroupMap MPPTGM)
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


                if (String.IsNullOrWhiteSpace(Convert.ToString(MPPTGM.GroupName))) { return Return.returnHttp("201", "Kindly Enter Group Name", null); }

                else
                {

                    String GroupName = Convert.ToString(MPPTGM.GroupName);
                    Int64 ProgrammePartTermId = Convert.ToInt64(MPPTGM.ProgrammePartTermId);
                    Int32 GroupTypeNameId = Convert.ToInt32(MPPTGM.GroupTypeNameId);

                    SqlCommand cmd = new SqlCommand("MstProgrammePartTermGroupMapAdd", Con);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@GroupName", GroupName);
                    cmd.Parameters.AddWithValue("@ProgrammePartTermId", ProgrammePartTermId);
                    cmd.Parameters.AddWithValue("@GroupTypeNameId", GroupTypeNameId);
                    cmd.Parameters.AddWithValue("@UserId", 0);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);

                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);

                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    Con.Open();

                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);

                    Con.Close();

                    return Return.returnHttp("200", strMessage.ToString(), null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region MstProgrammePartTermGroupMapDelete
        [HttpPost]
        public HttpResponseMessage MstProgrammePartTermGroupMapDelete(MstProgrammePartTermGroupMap MPPTGM)
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


                Int64 Id = Convert.ToInt64(MPPTGM.Id);

                SqlCommand cmd = new SqlCommand("MstProgrammePartTermGroupMapDelete", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);
               
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);

                Con.Close();

                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

        #region MstProgrammePartTermGroupMapGet
        [HttpPost]
        public HttpResponseMessage MstProgrammePartTermGroupMapGet()
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


                SqlCommand Cmd = new SqlCommand("MstProgrammePartTermGroupMapGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);


                List<MstProgrammePartTermGroupMap> ObjLstMPPTGM = new List<MstProgrammePartTermGroupMap>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgrammePartTermGroupMap objMPPTGM = new MstProgrammePartTermGroupMap();

                        objMPPTGM.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        objMPPTGM.GroupName = Convert.ToString(Dt.Rows[i]["GroupName"]);
                        objMPPTGM.GroupTypeNameId = Convert.ToInt32(Dt.Rows[i]["GroupTypeNameId"]);
                        objMPPTGM.GroupTypeName = Convert.ToString(Dt.Rows[i]["GroupTypeName"]);
                        objMPPTGM.ProgrammePartTermId = Convert.ToInt32(Dt.Rows[i]["ProgrammePartTermId"]);
                        objMPPTGM.PartTermName = Convert.ToString(Dt.Rows[i]["PartTermName"]);
                        objMPPTGM.FacultyName = Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objMPPTGM.ProgrammeName = Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        objMPPTGM.PartName = Convert.ToString(Dt.Rows[i]["PartName"]);
                        ObjLstMPPTGM.Add(objMPPTGM);
                    }
                }
                return Return.returnHttp("200", ObjLstMPPTGM, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region MstProgrammePartTermGroupMapEdit
        [HttpPost]
        public HttpResponseMessage MstProgrammePartTermGroupMapEdit(MstProgrammePartTermGroupMap MPPTGM)
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


                if (String.IsNullOrWhiteSpace(Convert.ToString(MPPTGM.GroupName))) { return Return.returnHttp("201", "Kindly Enter Group Name", null); }
                else if (String.IsNullOrEmpty(Convert.ToString(MPPTGM.Id))) { return Return.returnHttp("201", "Please Try Again Server Error", null); }
                else
                {
                    Int64 Id = Convert.ToInt64(MPPTGM.Id);
                    String GroupName = Convert.ToString(MPPTGM.GroupName);
                    Int32 ProgrammePartTermId = Convert.ToInt32(MPPTGM.ProgrammePartTermId);
                    Int32 GroupTypeNameId = Convert.ToInt32(MPPTGM.GroupTypeNameId);

                    SqlCommand cmd = new SqlCommand("MstProgrammePartTermGroupMapEdit", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@GroupName", GroupName);
                    cmd.Parameters.AddWithValue("@GroupTypeId", GroupTypeNameId);
                    cmd.Parameters.AddWithValue("@ProgrammePartTermId", ProgrammePartTermId);
                    //cmd.Parameters.AddWithValue("@GroupTypeNameId", GroupTypeNameId);

                    cmd.Parameters.AddWithValue("@UserId", 0);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                    Con.Open();

                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);

                    Con.Close();

                    return Return.returnHttp("200", strMessage.ToString(), null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Programme Get By Fac Id
        [HttpPost]
        public HttpResponseMessage MstProgrammeGetByFacId(MstProgrammePart ObjProgPart)
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

                List<MstProgramme> ObjLstProg = new List<MstProgramme>();
                BALProgramme ObjBalProgList = new BALProgramme();
                ObjLstProg = ObjBalProgList.ProgrammeGetByFacId(ObjProgPart.FacultyId);

                if (ObjLstProg != null)
                    return Return.returnHttp("200", ObjLstProg, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);

            }
            catch (Exception e)
            {

                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }

        #endregion        

        #region MstProgrammePartGetByProgrammeId
        [HttpPost]
        public HttpResponseMessage MstProgrammePartGetByProgrammeId(MstProgrammePart ObjProgPart)
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

                List<MstProgrammePart> ObjLstProgPart = new List<MstProgrammePart>();
                BALProgrammePart ObjBalProgPartList = new BALProgrammePart();
                ObjLstProgPart = ObjBalProgPartList.ProgrammePartGetByProgrammeId(ObjProgPart.ProgrammeId);

                if (ObjLstProgPart != null)
                    return Return.returnHttp("200", ObjLstProgPart, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region MstProgrammePartTermGetByPartId
        [HttpPost]
        public HttpResponseMessage MstProgrammePartTermGetByPartId(MstProgrammePartTermGroupMap PT)
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

                Int32 PartId = Convert.ToInt32(PT.PartId);

                SqlCommand Cmd = new SqlCommand("MstProgrammePartTermGetByPartId", Con);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@PartId", PartId);

                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                


                List<MstProgrammePartTermGroupMap> ObjLstPTID = new List<MstProgrammePartTermGroupMap>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgrammePartTermGroupMap objPTID = new MstProgrammePartTermGroupMap();

                        objPTID.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objPTID.PartTermName = Convert.ToString(Dt.Rows[i]["PartTermName"]);
                        ObjLstPTID.Add(objPTID);
                    }
                }
                return Return.returnHttp("200", ObjLstPTID, null);
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

        #region MstGroupTypeGet
        [HttpPost]
        public HttpResponseMessage MstGroupTypeGet()
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
                SqlCommand cmd = new SqlCommand("MstGroupTypeGet", Con);



                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                Da.SelectCommand = cmd;
                Da.Fill(dt);



                List<MstGroupType> ObjGroupTypeList = new List<MstGroupType>();



                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstGroupType ObjGrpType = new MstGroupType();
                        ObjGrpType.Id = Convert.ToInt32(dr["Id"].ToString());
                        ObjGrpType.GroupTypeName = dr["GroupTypeName"].ToString();
                        ObjGrpType.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
                        ObjGrpType.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjGrpType.IsActiveSts = dr["IsActiveSts"].ToString();
                        ObjGroupTypeList.Add(ObjGrpType);
                    }
                }
                return Return.returnHttp("200", ObjGroupTypeList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion
    }
}

