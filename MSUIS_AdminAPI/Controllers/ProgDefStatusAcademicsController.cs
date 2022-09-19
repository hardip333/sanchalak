using MSUIS_TokenManager.App_Start;
using MSUISApi.BAL;
using MSUISApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;

namespace MSUISApi.Controllers
{
    public class ProgDefStatusAcademicsController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        #region ProgDefStatusAcademicsGet
        [HttpPost]
        public HttpResponseMessage ProgDefStatusAcademicsGet(ProgDefStatusAcademics ObjProgDefStat)
        {
            ProgDefStatusAcademics modelobj = new ProgDefStatusAcademics();

            try
            {
                modelobj.FacultyId = ObjProgDefStat.FacultyId;
                modelobj.AcademicYearId = ObjProgDefStat.AcademicYearId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("ProgDefinationStatusCompletedAca", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", modelobj.FacultyId);
                cmd.Parameters.AddWithValue("@AcademicYearId", modelobj.AcademicYearId);
                sda.SelectCommand = cmd;
                sda.Fill(Dt);
                Int32 count = 0;
                List<ProgDefStatusAcademics> ObjLstPDS = new List<ProgDefStatusAcademics>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        ProgDefStatusAcademics ObjPDS = new ProgDefStatusAcademics();

                        //ObjAS.ProgrammeInstancePartTermId = Convert.ToInt32(dr["ProgrammeInstancePartTermId"]);

                        
                        ObjPDS.Id = Convert.ToInt32(dr["Id"]);
                        ObjPDS.InstancePartTermName = (dr["InstancePartTermName"].ToString());
                        ObjPDS.FacultyId = Convert.ToInt32(dr["FacultyId"]);
                        ObjPDS.FacultyName = (dr["FacultyName"].ToString());
                        ObjPDS.AcademicYearId = Convert.ToInt32(dr["AcademicYearId"]);
                        ObjPDS.AcademicYearCode = (dr["AcademicYearCode"].ToString());
                        ObjPDS.IsCompleted = Convert.ToString(dr["IsCompleted"]);
                        var IsCompleted = dr["IsCompleted"];
                        if (IsCompleted is DBNull)
                        {
                            IsCompleted = null;
                        }
                        else
                        {
                            IsCompleted = dr["IsCompleted"];
                        }
                        ObjPDS.CompleteStatusFlag1 = Convert.ToBoolean(dr["CompleteStatusFlag1"]);
                        ObjPDS.CompleteStatusFlag2 = Convert.ToBoolean(dr["CompleteStatusFlag2"]);
                        if (ObjPDS.CompleteStatusFlag1 == true && ObjPDS.CompleteStatusFlag2 == true)
                        {
                            ObjPDS.CompleteStatus = "Complete";
                        }
                        else
                        {
                            ObjPDS.CompleteStatus = "InComplete";
                        }
                        

                        

                        count = count + 1;
                        ObjPDS.IndexId = count;
                        ObjLstPDS.Add(ObjPDS);
                    }

                }
                return Return.returnHttp("200", ObjLstPDS, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region FacultyGet
        [HttpPost]
        public HttpResponseMessage FacultyGet()
        {
            try
            {

                SqlCommand Cmd = new SqlCommand("MstFacultyGet", con);
                Cmd.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = Cmd;

                sda.Fill(Dt);


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



        #region ProgDefStatusAcademicsIsSuspended
        [HttpPost]
        public HttpResponseMessage ProgDefStatusAcademicsIsSuspended(ProgDefStatusAcademics PDSA)
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
                Int32 Id = Convert.ToInt32(PDSA.Id);
                Int64 UserId = Convert.ToInt64(res.ToString());
                SqlCommand cmd = new SqlCommand("ProgDefStatusCompImComp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "ProgDefStatusIsCompletedDisable");
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
                    strMessage = "Your data has been Suspended Successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        #region ProgDefStatusAcademicsIsActive
        [HttpPost]
        public HttpResponseMessage ProgDefStatusAcademicsIsActive(ProgDefStatusAcademics VNM)
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

                Int32 Id = Convert.ToInt32(VNM.Id);
                Int64 UserId = 1;//Convert.ToInt64(res.ToString());

                SqlCommand cmd = new SqlCommand("ProgDefStatusCompImComp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "ProgDefStatusIsCompletedEnable");
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
        #endregion








    }
}