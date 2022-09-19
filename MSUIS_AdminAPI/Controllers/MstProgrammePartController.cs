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
    public class MstProgrammePartController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        Validation validation = new Validation();

        #region MstProgrammePartGet
        [HttpGet]
        public HttpResponseMessage MstProgrammePartGet()
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
                List<MstProgrammePart> ObjLstProgPart = new List<MstProgrammePart>();
                BALProgrammePart ObjBalProgPartList = new BALProgrammePart();
                ObjLstProgPart = ObjBalProgPartList.ProgrammePartGet();

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

        #region Programme Part Get By Id
        [HttpPost]
        public HttpResponseMessage MstProgrammePartGetbyId(MstProgrammePart programmePart)
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

                MstProgrammePart ObjLstProgPart = new MstProgrammePart();
                BALProgrammePart ObjBalProgPartList = new BALProgrammePart();
                ObjLstProgPart = ObjBalProgPartList.ProgrammePartListGetbyId(programmePart.Id);

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

        #region Programme Get By FacultyId
        [HttpPost]
        public HttpResponseMessage MstProgrammeGetByFacultyId(MstProgrammePart ObjProgPart)
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
                ObjLstProg = ObjBalProgList.ProgrammeGetByFacultyId(ObjProgPart.FacultyId);

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
        
        #region New Programme Get By FacultyId
        [HttpPost]
        public HttpResponseMessage NewMstProgrammeGetByFacultyId(MstProgrammePart ObjProgPart)
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
                ObjLstProg = ObjBalProgList.NewMstProgrammeGetByFacultyId(ObjProgPart.FacultyId);

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

        #region Insert ProgrammePart Detail
        [HttpPost]
        public HttpResponseMessage MstProgrammePartAdd(MstProgrammePart ObjProgPart)
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

                Models.Function function = new Models.Function();
                // dynamic Jsondata = ProgPart;
                MstProgrammePart ProgramPart = new MstProgrammePart();
                if (String.IsNullOrEmpty(Convert.ToString(ObjProgPart.ProgrammeId)))
                {
                    return Return.returnHttp("201", "Please select Programme", null);
                }
                else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjProgPart.PartName)))
                {
                    return Return.returnHttp("201", "Please enter Part Name", null);
                }
                else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjProgPart.PartShortName)))
                {
                    return Return.returnHttp("201", "Please enter Part Short Name ", null);
                }
                else if (validation.validateDigit(Convert.ToString(ObjProgPart.SequenceNo)) == false)
                {
                    return Return.returnHttp("201", "Please enter Sequence No", null);
                }
                else if (validation.validateDigit(Convert.ToString(ObjProgPart.NoOfTerms)) == false)
                {
                    return Return.returnHttp("201", "Please enter NoOfTerms", null);
                }
                else
                {


                    ProgramPart.PartName = Convert.ToString(ObjProgPart.PartName);
                    ProgramPart.PartShortName = Convert.ToString(ObjProgPart.PartShortName);
                    ProgramPart.ProgrammeId = Convert.ToInt32(ObjProgPart.ProgrammeId);
                    ProgramPart.ExamPatternId = Convert.ToInt32(ObjProgPart.ExamPatternId);
                    ProgramPart.NoOfTerms = Convert.ToInt64(ObjProgPart.NoOfTerms);
                    ProgramPart.SequenceNo = Convert.ToInt64(ObjProgPart.SequenceNo);
                    ProgramPart.CreatedBy = Convert.ToInt64(UserId);
                    ProgramPart.CreatedOn = Convert.ToDateTime(datetime);
                    SqlCommand cmd = new SqlCommand("MstProgrammePartAdd", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProgrammeId", ProgramPart.ProgrammeId);
                    cmd.Parameters.AddWithValue("@ExamPatternId", ProgramPart.ExamPatternId);
                    cmd.Parameters.AddWithValue("@PartName", ProgramPart.PartName);
                    cmd.Parameters.AddWithValue("@PartShortName", ProgramPart.PartShortName);
                    cmd.Parameters.AddWithValue("@NoOfTerms", ProgramPart.NoOfTerms);
                    cmd.Parameters.AddWithValue("@SequenceNo", ProgramPart.SequenceNo);
                    cmd.Parameters.AddWithValue("@UserId", ProgramPart.CreatedBy);
                    cmd.Parameters.AddWithValue("@UserTime", datetime);
                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                    cmd.Parameters.Add("@ProgPartId", SqlDbType.Int);
                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                    cmd.Parameters["@ProgPartId"].Direction = ParameterDirection.Output;

                    Con.Open();
                    cmd.ExecuteNonQuery();
                    string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                    Con.Close();
                    if (string.Equals(strMessage, "TRUE"))
                    {
                        strMessage = "Your data has been added successfully.";
                        ProgramPart.Id = Convert.ToInt32(cmd.Parameters["@ProgPartId"].Value);
                    }
                    return Return.returnHttp("200", (strMessage.ToString(), ProgramPart.Id), null);

                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Update ProgrammePart Detail
        [HttpPost]
        public HttpResponseMessage MstProgrammePartEdit(MstProgrammePart ObjProgPart)
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
                if (String.IsNullOrEmpty(Convert.ToString(ObjProgPart.ProgrammeId)))
                {
                    return Return.returnHttp("201", "Please select Programme", null);
                }
                else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjProgPart.PartName)))
                {
                    return Return.returnHttp("201", "Please enter Part Name", null);
                }
                else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjProgPart.PartShortName)))
                {
                    return Return.returnHttp("201", "Please enter Part Short Name ", null);
                }
                else if (validation.validateDigit(Convert.ToString(ObjProgPart.SequenceNo)) == false)
                {
                    return Return.returnHttp("201", "Please enter Sequence No", null);
                }
                else if (validation.validateDigit(Convert.ToString(ObjProgPart.NoOfTerms)) == false)
                {
                    return Return.returnHttp("201", "Please enter NoOfTerms", null);
                }
                else
                {

                    Int64 UserId = Convert.ToInt64(res.ToString());

                    // dynamic Jsondata = ProgPart;
                    MstProgrammePart ProgramPart = new MstProgrammePart();
                    ProgramPart.Id = Convert.ToInt32(ObjProgPart.Id);
                    ProgramPart.PartName = Convert.ToString(ObjProgPart.PartName);
                    ProgramPart.PartShortName = Convert.ToString(ObjProgPart.PartShortName);
                    ProgramPart.ProgrammeId = Convert.ToInt32(ObjProgPart.ProgrammeId);
                    ProgramPart.ExamPatternId = Convert.ToInt32(ObjProgPart.ExamPatternId);
                    ProgramPart.NoOfTerms = Convert.ToInt64(ObjProgPart.NoOfTerms);
                    ProgramPart.SequenceNo = Convert.ToInt64(ObjProgPart.SequenceNo);
                    //  ProgramPart.ModifiedBy = Convert.ToInt64(ObjProgPart.UserId);

                    SqlCommand cmd = new SqlCommand("MstProgrammePartEdit", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.Parameters.AddWithValue("@Flag", "MstProgrammePartEdit");
                    cmd.Parameters.AddWithValue("@Id", ProgramPart.Id);
                    cmd.Parameters.AddWithValue("@ProgrammeId", ProgramPart.ProgrammeId);
                    cmd.Parameters.AddWithValue("@ExamPatternId", ProgramPart.ExamPatternId);
                    cmd.Parameters.AddWithValue("@PartName", ProgramPart.PartName);
                    cmd.Parameters.AddWithValue("@PartShortName", ProgramPart.PartShortName);
                    cmd.Parameters.AddWithValue("@NoOfTerms", ProgramPart.NoOfTerms);
                    cmd.Parameters.AddWithValue("@SequenceNo", ProgramPart.SequenceNo);
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
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Delete ProgrammePart Detail
        [HttpPost]
        public HttpResponseMessage MstProgrammePartDelete(MstProgrammePart ObjProgPart)
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

                //  dynamic Jsondata = ProgPart;

                Int32 Id = Convert.ToInt32(ObjProgPart.Id);


                SqlCommand cmd = new SqlCommand("MstProgrammePartDelete", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Id", Id);
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

        #region Active Disable For ProgrammePart Detail
        [HttpPost]
        public HttpResponseMessage MstProgrammePartIsSuspended(MstProgrammePart ObjProgPart)
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

                //dynamic JsonData = ProgPart;
                MstProgrammePart ProgramPart = new MstProgrammePart();
                ProgramPart.Id = Convert.ToInt32(ObjProgPart.Id);
                //ProgramPart.ModifiedBy = Convert.ToInt64(JsonData.UserId);
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand("MstProgrammePartActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstProgrammePartIsActiveDisable");
                cmd.Parameters.AddWithValue("@Id", ProgramPart.Id);
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

        #region Active Enable For ProgrammePart Detail
        [HttpPost]
        public HttpResponseMessage MstProgrammePartIsActive(MstProgrammePart ObjProgPart)
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

                // dynamic JsonData = ProgPart;
                MstProgrammePart ProgramPart = new MstProgrammePart();
                ProgramPart.Id = Convert.ToInt32(ObjProgPart.Id);
                // ProgramPart.ModifiedBy = Convert.ToInt64(JsonData.UserId);
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand("MstProgrammePartActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "MstProgrammePartIsActiveEnable");
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.AddWithValue("@Id", ProgramPart.Id);
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

        /*#region FacultyGet
        [HttpGet]
        public HttpResponseMessage MstFacultyGet()
        {
            try
            {
                
                SqlCommand cmd = new SqlCommand("MstFacultyGet",Con);
                cmd.CommandType = CommandType.StoredProcedure;

                List<MstFaculty> ObjLstFaculty = new List<MstFaculty>();     
                
                
                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                foreach (DataRow DR in Dt.Rows)
                {
                    MstFaculty ObjFaculty = new MstFaculty();


                    ObjFaculty.Id = Convert.ToInt32(DR["Id"].ToString());
                    ObjFaculty.FacultyName = DR["FacultyName"].ToString();
                    ObjFaculty.IsActive = Convert.ToBoolean(DR["IsActive"]);
                    ObjLstFaculty.Add(ObjFaculty);

                }


                return Return.returnHttp("200", ObjLstFaculty, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }
        #endregion*/

        /*#region ExaminationPatternGet
        [HttpPost]
        public HttpResponseMessage MstExaminationPatternGet()
        {
            try
            {

                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter("MstExaminationPatternGet", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                da.Fill(dt);
                List<MstExaminationPattern> ExamPatLists = new List<MstExaminationPattern>();
                foreach (DataRow dr in dt.Rows)
                {
                    MstExaminationPattern MExamPatObj = new MstExaminationPattern();
                    MExamPatObj.Id = Convert.ToInt32(dr["Id"].ToString());
                    MExamPatObj.ExaminationPatternName = dr["ExaminationPatternName"].ToString();

                    ExamPatLists.Add(MExamPatObj);
                }

                return Return.returnHttp("200", ExamPatLists, null);


            }
            catch (Exception e)
            {

                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion*/
    }
}

/*#region ProgrammeGetByFacultyId
[HttpPost]
public HttpResponseMessage MstProgrammeGetByFacultyId(MstProgrammePart ObjProgPart)
{
    try
    {

        SqlCommand cmd = new SqlCommand("MstProgrammeGetByFacultyId", Con);
        cmd.CommandType = CommandType.StoredProcedure;
        //da.SelectCommand.Parameters.AddWithValue("@Flag", "MstProgrammeGetByFaculty");
        cmd.Parameters.AddWithValue("@FacultyId", Convert.ToInt32(ObjProgPart.FacultyId));
        Da.SelectCommand = cmd;
        Da.Fill(Dt);
        List<MstProgramme> ProgLists = new List<MstProgramme>();

        foreach (DataRow dr in Dt.Rows)
        {
            MstProgramme MProg = new MstProgramme();
            MProg.Id = Convert.ToInt32(dr["Id"].ToString());
            MProg.ProgrammeName = dr["ProgrammeName"].ToString();

            ProgLists.Add(MProg);
        }

        return Return.returnHttp("200", ProgLists, null);

    }
    catch (Exception e)
    {

        return Return.returnHttp("201", e.Message.ToString(), null);
    }

}

#endregion*/