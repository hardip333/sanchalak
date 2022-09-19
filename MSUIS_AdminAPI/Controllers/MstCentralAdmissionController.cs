using MSUIS_TokenManager.App_Start;
using MSUISApi.BAL;
using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MSUISApi.Controllers
{
    public class MstCentralAdmissionController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
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
                MstFaculty AllInstitute = new MstFaculty();
                AllInstitute.Id = 0;
                AllInstitute.FacultyName = "All Institute" + "";
                ObjLstFaculty.Add(AllInstitute);

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

        #region ProgrammeList Get By FacultyId AcademicYearId
        [HttpPost]
        public HttpResponseMessage ProgrammeListGetByInstAcadId(MstCentralAdmission ObjCentralAdmission)
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


                SqlCommand cmd = new SqlCommand("MstProgrammeGetByFacIdAcadIdForCentralAdmission", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InstituteId", ObjCentralAdmission.InstituteId);
                cmd.Parameters.AddWithValue("@AcademicYearId", ObjCentralAdmission.AcademicYearId);
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<MstCentralAdmission> ObjListCA = new List<MstCentralAdmission>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstCentralAdmission objCA = new MstCentralAdmission();

                        objCA.ProgrammeId = Convert.ToInt32(dr["Id"].ToString());
                        objCA.ProgrammeName = dr["ProgrammeName"].ToString();
                        ObjListCA.Add(objCA);
                    }
                }
                return Return.returnHttp("200", ObjListCA, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region ProgrammeparttermList Get By FacultyId AcademicYearId
        [HttpPost]
        public HttpResponseMessage IncProgramInstancePartTermGetByFacultyId(MstCentralAdmission ObjCentralAdmission)
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


                SqlCommand cmd = new SqlCommand("IncProgInstPartTermGetByFacAcadId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", ObjCentralAdmission.FacultyId);
                cmd.Parameters.AddWithValue("@AcademicYearId", ObjCentralAdmission.AcademicYearId);
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<MstCentralAdmission> ObjListCA = new List<MstCentralAdmission>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstCentralAdmission objCA = new MstCentralAdmission();

                        objCA.Id = Convert.ToInt32(dr["Id"].ToString());
                        objCA.IncProgramInstancePartTermName = dr["InstancePartTermName"].ToString();
                        ObjListCA.Add(objCA);
                    }
                }
                return Return.returnHttp("200", ObjListCA, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region BranchListGet By ProgrammeId
        [HttpPost]
        public HttpResponseMessage BranchListGetByProgId(MstCentralAdmission ObjCentralAdmission)
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


                SqlCommand cmd = new SqlCommand("MstBranchListGetByProgIdForCentralAdmission", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammeId", ObjCentralAdmission.ProgrammeId);
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<MstCentralAdmission> ObjListCA = new List<MstCentralAdmission>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstCentralAdmission objCA = new MstCentralAdmission();

                        objCA.SpecialisationId = Convert.ToInt32(dr["SpecialisationId"].ToString());
                        objCA.BranchName = dr["BranchName"].ToString();
                        ObjListCA.Add(objCA);
                    }
                }
                return Return.returnHttp("200", ObjListCA, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region ProgrammeInstancePartTerm GetBy FacId And AcadYearId And ProgrammeId
        [HttpPost]
        public HttpResponseMessage IncProgramInstancePartTermGetbyInsIdAcadIdProgId(MstCentralAdmission ObjCentralAdmission)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("ProgrammeInstancePartTermGetByInsIdAcaIdProgIdForCentralAdmission", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@InstituteId", ObjCentralAdmission.InstituteId);
                da.SelectCommand.Parameters.AddWithValue("@AcademicYearId", ObjCentralAdmission.AcademicYearId);
                da.SelectCommand.Parameters.AddWithValue("@ProgrammeId", ObjCentralAdmission.ProgrammeId);
                da.SelectCommand.Parameters.AddWithValue("@SpecialisationId", ObjCentralAdmission.SpecialisationId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<MstCentralAdmission> ObjListCA = new List<MstCentralAdmission>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        MstCentralAdmission objCA = new MstCentralAdmission();
                        objCA.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        objCA.InstancePartTermName = Convert.ToString(dt.Rows[i]["InstancePartTermName"]);
                        ObjListCA.Add(objCA);

                    }
                    return Return.returnHttp("200", ObjListCA, null);
                }
                else
                {
                    return Return.returnHttp("201", "No Record Found", null);
                }

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion
   

        #region MstAdmissionCommitteeGet
        [HttpPost]
        public HttpResponseMessage MstAdmissionCommitteeGet()
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
                SqlCommand cmd = new SqlCommand("MstAdmissionCommitteeGet", con);

                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<MstAdmissionCommittee> ObjAdmComList = new List<MstAdmissionCommittee>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstAdmissionCommittee ObjAdmCom = new MstAdmissionCommittee();
                        ObjAdmCom.Id = Convert.ToInt32(dr["Id"].ToString());
                        ObjAdmCom.CommitteeName = dr["CommitteeName"].ToString();
                        ObjAdmCom.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
                        ObjAdmCom.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjAdmCom.IsActiveSts = dr["IsActiveSts"].ToString();
                        ObjAdmComList.Add(ObjAdmCom);
                    }
                }
                return Return.returnHttp("200", ObjAdmComList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region MstCentralAdmissionGet
        [HttpPost]
        public HttpResponseMessage MstCentralAdmissionGet()
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
                Int32 Count = 0;
                SqlCommand cmd = new SqlCommand("MstCentralAdmissionGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<MstCentralAdmission> ObjCentralAdmList = new List<MstCentralAdmission>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstCentralAdmission ObjCentralAdm = new MstCentralAdmission();
                        ObjCentralAdm.Id = Convert.ToInt32(dr["Id"].ToString());
                        ObjCentralAdm.FacultyId = Convert.ToInt32(dr["FacultyId"].ToString());
                        ObjCentralAdm.AcademicYearId = Convert.ToInt32(dr["AcademicYearId"].ToString());
                        ObjCentralAdm.FacultyName = dr["FacultyName"].ToString();
                        ObjCentralAdm.ApplicantName = dr["ApplicantName"].ToString();
                        ObjCentralAdm.InstancePartTermName = dr["InstancePartTermName"].ToString();
                        ObjCentralAdm.ProgrammeInstancePartTermId = Convert.ToInt32(dr["ProgramInstancePartTermId"].ToString());
                        ObjCentralAdm.AdmissionCommitteeId = Convert.ToInt32(dr["AdmissionCommitteeId"].ToString());
                        ObjCentralAdm.AcademicYearCode = dr["AcademicYearCode"].ToString();
                        ObjCentralAdm.CommitteeName = dr["CommitteeName"].ToString();
                        ObjCentralAdm.MeritNo = dr["MeritNo"].ToString();
                        ObjCentralAdm.AllotmentNo = dr["AllotmentNo"].ToString();
                        if (dr["ApplicantName"].ToString() != "")
                        {

                            ObjCentralAdm.ApplicantName = dr["ApplicantName"].ToString();
                        }
                        else
                        {
                            ObjCentralAdm.ApplicantName = "-";
                        }

                        if (dr["MobileNo"].ToString() != "")
                        {
                            ObjCentralAdm.MobileNo = Convert.ToString(dr["MobileNo"]);

                        }
                        else
                        {
                            ObjCentralAdm.MobileNo = "-";
                        }
                       
                        Count = Count + 1;
                        ObjCentralAdm.IndexId = Count;
                        ObjCentralAdmList.Add(ObjCentralAdm);
                    }
                }
                return Return.returnHttp("200", ObjCentralAdmList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region MstCentralAdmissionGetByFacultyAndInstituteId
        [HttpPost]
        public HttpResponseMessage MstCentralAdmissionGetByFacultyId(MstCentralAdmission objCentralAdm)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();
            String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            try
            {
              
                Int32 Count = 0;
                Int32 FacultyId = Convert.ToInt32(objCentralAdm.FacultyId);
                Int64 InstituteId = Convert.ToInt64(facultyDepartIntituteId);
                SqlCommand cmd = new SqlCommand("MstCentralAdmissionGetByFacultyId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                cmd.Parameters.AddWithValue("@InstituteId", InstituteId);
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<MstCentralAdmission> ObjCentralAdmList = new List<MstCentralAdmission>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstCentralAdmission ObjCentralAdm = new MstCentralAdmission();
                        ObjCentralAdm.Id = Convert.ToInt32(dr["Id"].ToString());
                        ObjCentralAdm.FacultyId = Convert.ToInt32(dr["FacultyId"].ToString());
                        ObjCentralAdm.AcademicYearId = Convert.ToInt32(dr["AcademicYearId"].ToString());
                        ObjCentralAdm.ProgrammeId = Convert.ToInt32(dr["ProgrammeId"].ToString());                      
                        ObjCentralAdm.SpecialisationId = Convert.ToInt32(dr["SpecialisationId"].ToString());                      
                        ObjCentralAdm.FacultyName = dr["FacultyName"].ToString();
                        ObjCentralAdm.ProgrammeName = dr["ProgrammeName"].ToString();
                        ObjCentralAdm.BranchName = dr["BranchName"].ToString();
                        ObjCentralAdm.InstancePartTermName = dr["InstancePartTermName"].ToString();
                        ObjCentralAdm.ProgrammeInstancePartTermId = Convert.ToInt32(dr["ProgramInstancePartTermId"].ToString());
                        ObjCentralAdm.AdmissionCommitteeId = Convert.ToInt32(dr["AdmissionCommitteeId"].ToString());
                        ObjCentralAdm.AcademicYearCode = dr["AcademicYearCode"].ToString();
                        ObjCentralAdm.CommitteeName = dr["CommitteeName"].ToString();
                        ObjCentralAdm.MeritNo = dr["MeritNo"].ToString();
                        ObjCentralAdm.AllotmentNo = dr["AllotmentNo"].ToString();
                        if (dr["ApplicantName"].ToString() != ""){
                            
                            ObjCentralAdm.ApplicantName = dr["ApplicantName"].ToString();
                        }
                        else
                        {
                            ObjCentralAdm.ApplicantName = "-";
                        }
                       
                        if (dr["MobileNo"].ToString() != "")
                        {
                            ObjCentralAdm.MobileNo = Convert.ToString(dr["MobileNo"]);
                           
                        }
                        else
                        {
                            ObjCentralAdm.MobileNo = "-";
                        }

                       
                        Count = Count + 1;
                        ObjCentralAdm.IndexId = Count;
                        ObjCentralAdmList.Add(ObjCentralAdm);
                    }
                }
                return Return.returnHttp("200", ObjCentralAdmList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region MstCentralAdmissionGetByFacIdAcadIdProgIdInstPartTermId
        [HttpPost]
        public HttpResponseMessage MstCentralAdmissionGetByFAPIPTId(MstCentralAdmission objCentralAdm)
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
                Int32 Count = 0;
                Int32 FacultyId = Convert.ToInt32(objCentralAdm.FacultyId);
                Int32 AcademicYearId = Convert.ToInt32(objCentralAdm.AcademicYearId);
                Int32 ProgrammeId = Convert.ToInt32(objCentralAdm.ProgrammeId);
                Int32 SpecialisationId = Convert.ToInt32(objCentralAdm.SpecialisationId);
                Int32 ProgrammeInstancePartTermId = Convert.ToInt32(objCentralAdm.ProgrammeInstancePartTermId);
                Int32 AdmissionCommitteeId = Convert.ToInt32(objCentralAdm.AdmissionCommitteeId);
                SqlCommand cmd = new SqlCommand("MstCentralAdmissionGetByFacIdAcadIdProgIdInstPartTermId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                cmd.Parameters.AddWithValue("@ProgrammeId", ProgrammeId);
                cmd.Parameters.AddWithValue("@SpecialisationId", SpecialisationId);
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgrammeInstancePartTermId);
                cmd.Parameters.AddWithValue("@AdmissionCommitteeId", AdmissionCommitteeId);
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                List<MstCentralAdmission> ObjCentralAdmList = new List<MstCentralAdmission>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstCentralAdmission ObjCentralAdm = new MstCentralAdmission();
                        ObjCentralAdm.Id = Convert.ToInt32(dr["Id"].ToString());
                    
                        ObjCentralAdm.FacultyName = dr["FacultyName"].ToString();
                        ObjCentralAdm.AcademicYearCode = dr["AcademicYearCode"].ToString();
                        ObjCentralAdm.ProgrammeName = dr["ProgrammeName"].ToString();
                        ObjCentralAdm.BranchName = dr["BranchName"].ToString();
                        ObjCentralAdm.InstancePartTermName = dr["InstancePartTermName"].ToString();
                        ObjCentralAdm.CommitteeName = dr["CommitteeName"].ToString();
                        ObjCentralAdm.MeritNo = dr["MeritNo"].ToString();
                        ObjCentralAdm.AllotmentNo = dr["AllotmentNo"].ToString();
                        if (dr["ApplicantName"].ToString() != "")
                        {

                            ObjCentralAdm.ApplicantName = dr["ApplicantName"].ToString();
                        }
                        else
                        {
                            ObjCentralAdm.ApplicantName = "-";
                        }

                        if (dr["MobileNo"].ToString() != "")
                        {
                            ObjCentralAdm.MobileNo = Convert.ToString(dr["MobileNo"]);

                        }
                        else
                        {
                            ObjCentralAdm.MobileNo = "-";
                        }

                       
                  
                        Count = Count + 1;
                        ObjCentralAdm.IndexId = Count;
                        ObjCentralAdmList.Add(ObjCentralAdm);
                    }
                }
                return Return.returnHttp("200", ObjCentralAdmList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region MstCentralAdmissionAdd
        [HttpPost]
        public HttpResponseMessage MstCentralAdmissionAdd(MstCentralAdmission ObjCentralAdd)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            if (String.IsNullOrWhiteSpace(Convert.ToString(ObjCentralAdd.MeritNo)))
            {
                return Return.returnHttp("201", "Please Enter Merit No", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjCentralAdd.AllotmentNo)))
            {
                return Return.returnHttp("201", "Please Enter Allotment No", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjCentralAdd.MobileNo)))
            {
                return Return.returnHttp("201", "Please Enter Mobile No", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjCentralAdd.ApplicantName)))
            {
                return Return.returnHttp("201", "Please Enter Applicant Name", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjCentralAdd.AllotmentNo)))
            {
                return Return.returnHttp("201", "Please Enter Allotment No", null);
            }

            else if (String.IsNullOrEmpty(Convert.ToString(ObjCentralAdd.AcademicYearId))) 
            { 
                return Return.returnHttp("201", "Please select Academic Year", null); 
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjCentralAdd.AdmissionCommitteeId)))
            {
                return Return.returnHttp("201", "Please select Admission Committee", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjCentralAdd.FacultyId)))
            {
                return Return.returnHttp("201", "Please select Faculty", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjCentralAdd.ProgrammeInstancePartTermId)))
            {
                return Return.returnHttp("201", "Please select ProgrammeInstancePartTerm", null);
            }
            else
            {
                try
                {
                    string ApplicantName = Convert.ToString(ObjCentralAdd.ApplicantName);
                    string MeritNo = Convert.ToString(ObjCentralAdd.MeritNo);
                    string AllotmentNo = Convert.ToString(ObjCentralAdd.AllotmentNo);
                    string MobileNo = Convert.ToString(ObjCentralAdd.MobileNo);
                    Int32 AcademicYearId = Convert.ToInt32(ObjCentralAdd.AcademicYearId);
                    Int64 ProgrammeInstancePartTermId = Convert.ToInt64(ObjCentralAdd.ProgrammeInstancePartTermId);
                    Int32 FacultyId = Convert.ToInt32(ObjCentralAdd.FacultyId);
                    Int32 AdmissionCommitteeId = Convert.ToInt32(ObjCentralAdd.AdmissionCommitteeId);
                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("MstCentralAdmissionAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ApplicantName", ApplicantName);
                    cmd.Parameters.AddWithValue("@MeritNo", MeritNo);
                    cmd.Parameters.AddWithValue("@AllotmentNo", AllotmentNo);
                    cmd.Parameters.AddWithValue("@MobileNo", MobileNo);
                    cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                    cmd.Parameters.AddWithValue("@ProgramInstancePartTermId", ProgrammeInstancePartTermId);
                    cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                    cmd.Parameters.AddWithValue("@AdmissionCommitteeId", AdmissionCommitteeId);
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
                        strMessage = "Your Record has been Added successfully.";
                    }
                    return Return.returnHttp("200", strMessage.ToString(), null);
                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message.ToString(), null);
                }
            }
        }
        #endregion

        #region MstCenterAdmissionAddExcel
        [HttpPost]
        public HttpResponseMessage MstCenterAdmissionAddExcel(List<MstCentralAdmission> ObjMstInstPTPM)
        {
            try
            {

                
                string strMessage = null;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation TokenOp = new TokenOperation();
                String ValidTok = TokenOp.ValidateToken(token);
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                if (ValidTok == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                foreach (MstCentralAdmission obj in ObjMstInstPTPM)
                {
                   
                    Int32 FacultyId = Convert.ToInt32(obj.FacultyId);
                    Int32 AcademicYearId = Convert.ToInt32(obj.AcademicYearId);
                    Int64 ProgrammeInstancePartTermId = Convert.ToInt64(obj.ProgrammeInstancePartTermId);
                    Int32 AdmissionCommitteeId = Convert.ToInt32(obj.AdmissionCommitteeId);
                    String MeritNo = Convert.ToString(obj.MeritNo);
                    String AllotmentNo = Convert.ToString(obj.AllotmentNo);
                    String MobileNo = Convert.ToString(obj.MobileNo);
                    String ApplicantName = Convert.ToString(obj.ApplicantName);

                    SqlCommand cmd = new SqlCommand("MstCentralAdmissionAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ApplicantName", ApplicantName);
                    cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                    cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                    cmd.Parameters.AddWithValue("@ProgramInstancePartTermId", ProgrammeInstancePartTermId);
                    cmd.Parameters.AddWithValue("@AdmissionCommitteeId", AdmissionCommitteeId);
                    cmd.Parameters.AddWithValue("@MeritNo", MeritNo);
                    cmd.Parameters.AddWithValue("@AllotmentNo", AllotmentNo);
                    cmd.Parameters.AddWithValue("@MobileNo", MobileNo);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;


                    con.Open();
                    cmd.ExecuteNonQuery();
                    strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    con.Close();
                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Your data has been saved successfully.";
                    }


                }
                return Return.returnHttp("200", strMessage, null);


            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region MstCentralAdmissionEdit
        [HttpPost]
        public HttpResponseMessage MstCentralAdmissionEdit(MstCentralAdmission ObjCentralEdit)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            if (String.IsNullOrWhiteSpace(Convert.ToString(ObjCentralEdit.MeritNo)))
            {
                return Return.returnHttp("201", "Please Enter MeritNo", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjCentralEdit.AllotmentNo)))
            {
                return Return.returnHttp("201", "Please Enter AllotmentNo", null);
            }

            else if (String.IsNullOrEmpty(Convert.ToString(ObjCentralEdit.AcademicYearId)))
            {
                return Return.returnHttp("201", "Please select Academic Year", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjCentralEdit.AdmissionCommitteeId)))
            {
                return Return.returnHttp("201", "Please select AdmissionCommittee", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjCentralEdit.FacultyId)))
            {
                return Return.returnHttp("201", "Please select Faculty", null);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(ObjCentralEdit.ProgrammeInstancePartTermId)))
            {
                return Return.returnHttp("201", "Please select ProgrammeInstancePartTerm", null);
            }
            else
            {
                try
                {
                    Int32 Id = Convert.ToInt32(ObjCentralEdit.Id);
                    string MeritNo = Convert.ToString(ObjCentralEdit.MeritNo);
                    string AllotmentNo = Convert.ToString(ObjCentralEdit.AllotmentNo);
                    string MobileNo = Convert.ToString(ObjCentralEdit.MobileNo);
                    string ApplicantName = Convert.ToString(ObjCentralEdit.ApplicantName);
                    Int32 AcademicYearId = Convert.ToInt32(ObjCentralEdit.AcademicYearId);
                    Int64 ProgrammeInstancePartTermId = Convert.ToInt64(ObjCentralEdit.ProgrammeInstancePartTermId);
                    Int32 FacultyId = Convert.ToInt32(ObjCentralEdit.FacultyId);
                    Int32 AdmissionCommitteeId = Convert.ToInt32(ObjCentralEdit.AdmissionCommitteeId);
                    Int64 UserId = Convert.ToInt64(res.ToString());

                    SqlCommand cmd = new SqlCommand("MstCentralAdmissionEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@MeritNo", MeritNo);
                    cmd.Parameters.AddWithValue("@ApplicantName", ApplicantName);
                    cmd.Parameters.AddWithValue("@AllotmentNo", AllotmentNo);
                    cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
                    cmd.Parameters.AddWithValue("@ProgramInstancePartTermId", ProgrammeInstancePartTermId);
                    cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                    cmd.Parameters.AddWithValue("@AdmissionCommitteeId", AdmissionCommitteeId);
                    cmd.Parameters.AddWithValue("@MobileNo", MobileNo);
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
                        strMessage = "Your Record has been Updated successfully.";
                    }
                    return Return.returnHttp("200", strMessage.ToString(), null);
                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message.ToString(), null);
                }
            }
        }
        #endregion

        #region MstCentralAdmissionDelete
        [HttpPost]
        public HttpResponseMessage MstCentralAdmissionDelete(MstCentralAdmission ObjCentralAdd)
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
                Int32 Id = ObjCentralAdd.Id;
                Int64 UserId = Convert.ToInt64(ValidTok.ToString());

                SqlCommand cmd = new SqlCommand("MstCentralAdmissionDelete", con);
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
                    strMessage = "Your Record has been deleted successfully.";
                }

                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion

   
        #region FileUpload---ImportingExcel
        [HttpPost]
        public HttpResponseMessage UploadFacultyExcelDocument()
        {
            string token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation tokenOperation = new TokenOperation();
            string responseToken = tokenOperation.ValidateToken(token);
            if (responseToken == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            string renameFile = Request.Headers.GetValues("ImportExcelFile").FirstOrDefault();

            int iUploadedCnt = 0;
            // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
            string sPath = "";
            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/FacultyImportExcel/");

            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            // CHECK THE FILE COUNT.
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];

                if (hpf.ContentLength > 0)
                {
                    if (File.Exists(sPath + Path.GetFileName(responseToken + "_" + renameFile)))
                    {
                        File.Delete(sPath + Path.GetFileName(responseToken + "_" + renameFile));
                    }
                    hpf.SaveAs(sPath + Path.GetFileName(responseToken + "_" + renameFile));
                    iUploadedCnt = iUploadedCnt + 1;

                }
            }
            if (iUploadedCnt > 0)
            {
                return Return.returnHttp("200", iUploadedCnt + " Files Uploaded Successfully", null);
            }
            else
            {
                return Return.returnHttp("201", iUploadedCnt + "Upload Failed", null);
            }

        }
        #endregion
    }
}
