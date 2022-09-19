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
    public class InstituteSubjectMapController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();

        #region InstituteSubjectMapGet
        [HttpPost]
        public HttpResponseMessage InstituteSubjectMapGet(InstituteSubjectMap ObjInstSub)
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

                SqlCommand cmd = new SqlCommand("InstituteSubjectMapGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InstituteId", ObjInstSub.InstituteId);
                //cmd.Parameters.AddWithValue("@SubjectId", ObjInstSub.SubjectId);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable Dt = new DataTable();

                //cmd.Parameters.AddWithValue("@ProgrammeId", MIPM.ProgrammeId);
                sda.Fill(Dt);
                Int32 count = 0;

                List<InstituteSubjectMap> ObjISLists = new List<InstituteSubjectMap>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        InstituteSubjectMap ObjIS = new InstituteSubjectMap();

                        ObjIS.Id = (((Dt.Rows[i]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["Id"]);
                        ObjIS.FacultyId = (((Dt.Rows[i]["FacultyId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        ObjIS.InstituteId = (((Dt.Rows[i]["InstituteId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["InstituteId"]);
                        ObjIS.SubjectId = (((Dt.Rows[i]["SubjectId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["SubjectId"]);
                        ObjIS.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        ObjIS.InstituteName = (Convert.ToString(Dt.Rows[i]["InstituteName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstituteName"]);
                        ObjIS.SubjectName = (Convert.ToString(Dt.Rows[i]["SubjectName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["SubjectName"]);
                        ObjIS.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        ObjIS.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["BranchName"]);
                        ObjIS.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"].ToString());
                        ObjIS.IsDeleted = Convert.ToBoolean(Dt.Rows[i]["IsDeleted"].ToString());
                        ObjIS.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["IsActive"]);

                        count = count + 1;
                        ObjIS.IndexId = count;

                        ObjISLists.Add(ObjIS);
                    }
                    return Return.returnHttp("200", ObjISLists, null);
                }
                else
                {
                    return Return.returnHttp("201", "Record Not Found", null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region InstituteSubjectMapAdd

        [HttpPost]
        public HttpResponseMessage InstituteSubjectMapAdd(InstituteSubjectMap ObjISM)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                //String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
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
                InstituteSubjectMap ObjInstSM = new InstituteSubjectMap();

                var SubList = ObjISM.SubList;
                foreach (SubData IPM in SubList)
                {
                    Int32 InstituteSubjectMapId = Convert.ToInt32(IPM.InstituteSubjectMapId);
                    Int32 SubjectId = Convert.ToInt32(IPM.SubjectId);
                    

                    if (IPM.SubChecked)
                    {
                        //MstInstituteProgrammeMap MIPM = new MstInstituteProgrammeMap();
                        
                        Int32 InstituteId = Convert.ToInt32(ObjISM.InstituteId);
                        Int32 FacultyId = Convert.ToInt32(ObjISM.FacultyId);
                        Int32 ProgrammeId = Convert.ToInt32(ObjISM.ProgrammeId);
                        Int32 SpecialisationId = Convert.ToInt32(ObjISM.SpecialisationId);
                        Int64 UserId = Convert.ToInt64(res.ToString());

                        SqlCommand cmd = new SqlCommand("InstituteSubjectMapAdd", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                        cmd.Parameters.AddWithValue("@InstituteId", InstituteId);                        
                        cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
                        cmd.Parameters.AddWithValue("@ProgrammeId", ProgrammeId);
                        cmd.Parameters.AddWithValue("@SpecialisationId", SpecialisationId);
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

                    }
                    else
                    {
                        Int32 FacultyId = Convert.ToInt32(ObjISM.FacultyId);
                        Int32 InstituteId = Convert.ToInt32(ObjISM.InstituteId);
                        Int32 ProgrammeId = Convert.ToInt32(ObjISM.ProgrammeId);
                        Int32 SpecialisationId = Convert.ToInt32(ObjISM.SpecialisationId);
                        SqlCommand cmd = new SqlCommand("InstituteSubjectMapDelete", con);
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                        cmd.Parameters.AddWithValue("@InstituteId", InstituteId);
                        cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
                        cmd.Parameters.AddWithValue("@ProgrammeId", ProgrammeId);
                        cmd.Parameters.AddWithValue("@SpecialisationId", SpecialisationId);
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
                    }
                }

                return Return.returnHttp("200", "Your Data Has Been Add / Modified Successfully", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region MstInstituteGet
        [HttpPost]
        public HttpResponseMessage MstInstituteGet()
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

                List<MstInstitute> ObjLstInstitute = new List<MstInstitute>();
                BALInstituteList ObjBalInstituteList = new BALInstituteList();
                ObjLstInstitute = ObjBalInstituteList.InstituteListGet();

                if (ObjLstInstitute != null)
                    return Return.returnHttp("200", ObjLstInstitute, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region FacultyGet
        [HttpPost]
        public HttpResponseMessage FacultyGetByInstituteId(MstFaculty ObjMF)
        {
            try
            {


                SqlCommand Cmd = new SqlCommand("MstFacultyGetByInstituteId", con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@InstituteId",ObjMF.InstituteId);
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

        #region MstSubjectGetByFId
        [HttpPost]
        public HttpResponseMessage MstSubjectGetByFId(MstSubject ObjSub)
        {
            try
            {
                //MstProgramme mprog = new MstProgramme();

                //dynamic Jsondata = BOS;

                //mprog.FacultyId = Convert.ToInt32(Jsondata.FacultyId);

                SqlCommand cmd = new SqlCommand("MstSubjectGetByFId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", ObjSub.FacultyId);
                cmd.Parameters.AddWithValue("@InstituteId", ObjSub.InstituteId);
                cmd.Parameters.AddWithValue("@ProgrammeId", ObjSub.ProgrammeId);
                cmd.Parameters.AddWithValue("@SpecialisationId", ObjSub.SpecialisationId);

                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = cmd;

                Da.Fill(Dt);

                List<MstSubject> ObjLstSub = new List<MstSubject>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstSubject ObjS = new MstSubject();

                        ObjS.Id = (((Dt.Rows[i]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["Id"]);
                        ObjS.SubjectName = (Convert.ToString(Dt.Rows[i]["SubjectName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["SubjectName"]);

                        ObjS.SubChecked = Convert.ToBoolean(Dt.Rows[i]["SubChecked"]);
                        ObjS.InstituteSubjectMapId = (((Dt.Rows[i]["InstituteSubjectMapId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["InstituteSubjectMapId"]);
                        ObjS.SubjectId = (((Dt.Rows[i]["SubjectId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["SubjectId"]);
                        //ObjS.FacultyId = (((Dt.Rows[i]["FacultyId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        //ObjS.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        //ObjS.InstituteId = (((Dt.Rows[i]["InstituteId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["InstituteId"]);
                        //ObjS.InstituteName = (Convert.ToString(Dt.Rows[i]["InstituteName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstituteName"]);
                        //ObjS.ProgrammeId = (((Dt.Rows[i]["ProgrammeId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);
                        //ObjS.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        //ObjS.SpecialisationId = (((Dt.Rows[i]["SpecialisationId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["SpecialisationId"]);
                        //ObjS.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["BranchName"]);

                        ObjLstSub.Add(ObjS);
                    }
                }
                return Return.returnHttp("200", ObjLstSub, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Programme Get By Faculty Id
        [HttpPost]
        public HttpResponseMessage MstProgrammeGetByFacultyId(MstProgramme ObjProg)
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
                ObjLstProg = ObjBalProgList.ProgrammeGetByFacId(ObjProg.FacultyId);

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

        //#region MstSpecialisation Get By FacultyId
        //[HttpPost]
        //public HttpResponseMessage MstSpecialisationGetByFacultyId(MstSpecialisation ObjSpecial)
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
        //        List<MstSpecialisation> ObjLstSpecial = new List<MstSpecialisation>();
        //        BALSpecialisation ObjBalSpecialList = new BALSpecialisation();
        //        ObjLstSpecial = ObjBalSpecialList.SpecialisationGetByFacultyId(ObjSpecial.FacultyId);

        //        if (ObjLstSpecial != null)
        //            return Return.returnHttp("200", ObjLstSpecial, null);
        //        else
        //            return Return.returnHttp("201", "No Record Found", null);
        //    }
        //    catch (Exception e)
        //    {
        //        return Return.returnHttp("201", e.Message, null);
        //    }

        //}
        //#endregion


        #region MstSpecialisationGetByPId
        [HttpPost]
        public HttpResponseMessage MstSpecialisationGetByPId(MstSpecialisation ObjSpe)
        {
            try
            {


                SqlCommand Cmd = new SqlCommand("MstSpecialisationGetByPId", con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@ProgrammeId", ObjSpe.ProgrammeId);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);


                List<MstSpecialisation> ObjLstSpe = new List<MstSpecialisation>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstSpecialisation objS = new MstSpecialisation();

                        objS.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objS.BranchName = Convert.ToString(Dt.Rows[i]["BranchName"]);
                        objS.ProgrammeId = Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);
                        objS.ProgrammeName = Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        ObjLstSpe.Add(objS);
                    }
                }
                return Return.returnHttp("200", ObjLstSpe, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion


    }
}