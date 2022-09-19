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
    public class ExamFeeConfigurationHeadMapController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);

        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region newCode By Stored Procedure - Megha
        [HttpPost]
        public HttpResponseMessage ExamFeeConfigurationHeadMapAdd(ExamFeeConfigurationHeadMap ObjexamFeeConfigurationHeadMap)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            Int64 UserId = Convert.ToInt64(res.ToString());
            //Int64 UserId = 1;
            if (String.IsNullOrWhiteSpace(ObjexamFeeConfigurationHeadMap.ExamFeeConfigurationId.ToString()))
            {
                if (!String.IsNullOrWhiteSpace(ObjexamFeeConfigurationHeadMap.ExamFeeConfiguration.ProgrammePartTermId.ToString()) && !String.IsNullOrWhiteSpace(ObjexamFeeConfigurationHeadMap.ExamFeeConfiguration.AppearanceTypeId.ToString()) && !String.IsNullOrWhiteSpace(ObjexamFeeConfigurationHeadMap.ExamFeeConfiguration.ExamMasterId.ToString()))
                {
                    SqlCommand cmd = new SqlCommand("ExamFeeConfigurationGetId", con);
                    SqlDataAdapter Da = new SqlDataAdapter();
                    DataTable Dt = new DataTable();
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@AppearanceTypeId", ObjexamFeeConfigurationHeadMap.ExamFeeConfiguration.AppearanceTypeId);
                    cmd.Parameters.AddWithValue("@ProgrammePartTermId", ObjexamFeeConfigurationHeadMap.ExamFeeConfiguration.ProgrammePartTermId);
                    cmd.Parameters.AddWithValue("@ExamMasterId", ObjexamFeeConfigurationHeadMap.ExamFeeConfiguration.ExamMasterId);

                    Da.SelectCommand = cmd;

                    Da.Fill(Dt);

                    if (Dt == null || Dt.Rows.Count == 0)
                    {
                        SqlCommand cmd1 = new SqlCommand("ExamFeeConfigurationAdd", con);
                        cmd1.CommandType = CommandType.StoredProcedure;

                        cmd1.Parameters.AddWithValue("@AppearanceTypeId", ObjexamFeeConfigurationHeadMap.ExamFeeConfiguration.AppearanceTypeId);
                        cmd1.Parameters.AddWithValue("@ProgrammePartTermId", ObjexamFeeConfigurationHeadMap.ExamFeeConfiguration.ProgrammePartTermId);
                        cmd1.Parameters.AddWithValue("@ExamMasterId", ObjexamFeeConfigurationHeadMap.ExamFeeConfiguration.ExamMasterId);
                        cmd1.Parameters.AddWithValue("@UserId", UserId);
                        cmd1.Parameters.AddWithValue("@UserTime", datetime);

                        cmd1.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                        cmd1.Parameters["@Message"].Direction = ParameterDirection.Output;

                        con.Open();
                        cmd1.ExecuteNonQuery();
                        string strMessage = Convert.ToString(cmd1.Parameters["@Message"].Value);
                        con.Close();
                        if (!string.Equals(strMessage, "TRUE"))
                        {
                            return Return.returnHttp("201", "Something went wrong please try again.", null);
                        }
                        Da.Fill(Dt);
                        if (Dt != null || Dt.Rows.Count != 0)
                            ObjexamFeeConfigurationHeadMap.ExamFeeConfigurationId = Convert.ToInt32(Dt.Rows[0][0].ToString());
                    }
                    else
                        ObjexamFeeConfigurationHeadMap.ExamFeeConfigurationId = Convert.ToInt32(Dt.Rows[0][0].ToString());

                }
            }

            if (String.IsNullOrWhiteSpace(Convert.ToString(ObjexamFeeConfigurationHeadMap.ExamFeesHeadId.ToString())))
            {
                return Return.returnHttp("201", "Please enter Exam Fee Head.", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjexamFeeConfigurationHeadMap.FeesAmount)))
            {
                return Return.returnHttp("201", "Please enter fee amount", null);
            }
            else
            {
                try
                {
                     //Int64 UserId = Convert.ToInt64(res.ToString());


                    int ExamFeeConfigurationId = Convert.ToInt32(ObjexamFeeConfigurationHeadMap.ExamFeeConfigurationId);

                    int ExamFeeHeadId = Convert.ToInt32(ObjexamFeeConfigurationHeadMap.ExamFeesHeadId);
                    float FeeAmount = (float)Convert.ToDouble(ObjexamFeeConfigurationHeadMap.FeesAmount);
                    float IncrementedBy = (float)Convert.ToDouble(ObjexamFeeConfigurationHeadMap.IncrementedBy);
                    bool? IsEditable = Convert.ToBoolean(ObjexamFeeConfigurationHeadMap.IsEditable);
                    bool? IsDayWiseApplicable = Convert.ToBoolean(ObjexamFeeConfigurationHeadMap.IsDayWiseApplicable);

                    SqlCommand cmd = new SqlCommand("ExamFeeConfigurationHeadMapAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ExamFeeConfigurationId", ExamFeeConfigurationId);
                    cmd.Parameters.AddWithValue("@ExamFeeHeadId", ExamFeeHeadId);
                    cmd.Parameters.AddWithValue("@FeeAmount", FeeAmount);
                    cmd.Parameters.AddWithValue("@IncrementedBy", IncrementedBy);
                    cmd.Parameters.AddWithValue("@IsEditable", IsEditable);
                    cmd.Parameters.AddWithValue("@IsDayWiseApplicable", IsDayWiseApplicable);
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
        public HttpResponseMessage ExamFeeConfigurationHeadMapEdit(ExamFeeConfigurationHeadMap ObjexamFeeConfigurationHeadMap)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            if (String.IsNullOrWhiteSpace(ObjexamFeeConfigurationHeadMap.ExamFeeConfigurationId.ToString()))
            {
                return Return.returnHttp("201", "Please select Exam Fee Configuration. ", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjexamFeeConfigurationHeadMap.ExamFeesHeadId.ToString())))
            {
                return Return.returnHttp("201", "Please enter Exam Fee Head.", null);
            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ObjexamFeeConfigurationHeadMap.FeesAmount)))
            {
                return Return.returnHttp("201", "Please enter fee amount", null);
            }
            else
            {
                try
                {
                     Int64 UserId = Convert.ToInt64(res.ToString());
                    //Int64 UserId = 1;

                    int ExamFeeConfigurationId = Convert.ToInt32(ObjexamFeeConfigurationHeadMap.ExamFeeConfigurationId);

                    int ExamFeeHeadId = Convert.ToInt32(ObjexamFeeConfigurationHeadMap.ExamFeesHeadId);
                    float FeeAmount = (float)Convert.ToDouble(ObjexamFeeConfigurationHeadMap.FeesAmount);
                    float IncrementedBy = (float)Convert.ToDouble(ObjexamFeeConfigurationHeadMap.IncrementedBy);
                    //bool? IsEditable = Convert.ToBoolean(ObjexamFeeConfigurationHeadMap.IsEditable);
                    //bool? IsDayWiseApplicable = Convert.ToBoolean(ObjexamFeeConfigurationHeadMap.IsDayWiseApplicable);

                    SqlCommand cmd = new SqlCommand("ExamFeeConfigurationHeadMapEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", ObjexamFeeConfigurationHeadMap.Id);
                    cmd.Parameters.AddWithValue("@ExamFeeConfigurationId", ExamFeeConfigurationId);
                    cmd.Parameters.AddWithValue("@ExamFeeHeadId", ExamFeeHeadId);
                    cmd.Parameters.AddWithValue("@FeeAmount", FeeAmount);
                    cmd.Parameters.AddWithValue("@IncrementedBy", IncrementedBy);
                    //cmd.Parameters.AddWithValue("@IsEditable", IsEditable);
                    //cmd.Parameters.AddWithValue("@IsDayWiseApplicable", IsDayWiseApplicable);
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
        public HttpResponseMessage ExamFeeConfigurationHeadMapListGet(ExamFeeConfigurationHeadMap dataString)
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

                List<ExamFeeConfigurationHeadMap> ExamFeeConfigurationHeadMapList = new List<ExamFeeConfigurationHeadMap>();
                BALExamFeeConfigurationHeadMap func = new BALExamFeeConfigurationHeadMap();
                //flag=1,IsDeleted=0,IsActive=null
                ExamFeeConfigurationHeadMapList = func.getExamFeeConfigurationHeadMapList("admin", 0, null, dataString.ExamFeesHeadId, dataString.ExamFeeConfigurationId);

                return Return.returnHttp("200", ExamFeeConfigurationHeadMapList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage ExamFeeConfigurationHeadMapListGetActive(ExamFeeConfigurationHeadMap dataString)
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

                List<ExamFeeConfigurationHeadMap> ExamFeeConfigurationHeadMapList = new List<ExamFeeConfigurationHeadMap>();
                BALExamFeeConfigurationHeadMap func = new BALExamFeeConfigurationHeadMap();
                //flag=1,IsDeleted=0,IsActive=null
                ExamFeeConfigurationHeadMapList = func.getExamFeeConfigurationHeadMapList("admin", 0, null, dataString.ExamFeesHeadId, dataString.ExamFeeConfigurationId);

                return Return.returnHttp("200", ExamFeeConfigurationHeadMapList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage getPendingExamFeeConfigurationList(ExamFeesConfiguration dataString)
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
                ExamFeeCongfigGet feeCongfigGet = new ExamFeeCongfigGet();

                BALExamFeeConfigurationHeadMap func = new BALExamFeeConfigurationHeadMap();
                //flag=1,IsDeleted=0,IsActive=null
                feeCongfigGet = func.getPendingExamFeeConfigurationList(dataString.ProgrammePartTermId, dataString.FacultyExamMapId, dataString.ExamMasterId, dataString.AppearanceTypeId);

                return Return.returnHttp("200", feeCongfigGet, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        /// ExamFeeConfigurationHeadMapIsActive and ExamFeeConfigurationHeadMapIsInactive Created by Jaydev on 25-11-21

        [HttpPost]
        public HttpResponseMessage ExamFeeConfigurationHeadMapIsActive(ExamFeeConfigurationHeadMap dataString)
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

                Int32 Id = (int)dataString.Id;
                Int64 UserId = Convert.ToInt64(res.ToString());//1;//

                SqlCommand cmd = new SqlCommand("ExamFeeConfigurationHeadMapActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "ExamFeeConfigurationHeadMapIsActive");
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


        [HttpPost]
        public HttpResponseMessage ExamFeeConfigurationHeadMapIsInactive(ExamFeeConfigurationHeadMap dataString)
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

                Int32 Id = (int)dataString.Id;
                Int64 UserId = Convert.ToInt64(res.ToString());//1;//

                SqlCommand cmd = new SqlCommand("ExamFeeConfigurationHeadMapActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "ExamFeeConfigurationHeadMapIsInactive");
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
                    strMessage = "Your data has been Inactive Successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }


        [HttpPost]
        public HttpResponseMessage CopyExamFeesConfiguratonHeadMap(CopyExamFeeConfiguration ObjCopyExamFeeConfiguration)
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
                if (ObjCopyExamFeeConfiguration.ExamMasterId==0)
                {
                    return Return.returnHttp("201", "Please select ExamMaster.", null);
                }
                else if (ObjCopyExamFeeConfiguration.FacultyExamMapId == 0)
                {
                    return Return.returnHttp("201", "Please select schedule.", null);
                }
                else if (ObjCopyExamFeeConfiguration.ProgrammeId == 0)
                {
                    return Return.returnHttp("201", "Please select programme.", null);
                }
                else if (ObjCopyExamFeeConfiguration.ProgrammePartTermIdFrom == 0)
                {
                    return Return.returnHttp("201", "Please select programme part term.", null);
                }
                else if (ObjCopyExamFeeConfiguration.programmePartTermList==null)
                {
                    return Return.returnHttp("201", "List is Empty.", null);
                }
                else
                {
                    try
                    {
                         Int64 UserId = Convert.ToInt64(res.ToString());
                        //Int64 UserId = 1;

                        if (!ObjCopyExamFeeConfiguration.programmePartTermList.Any(n => n.IsSelected == true))
                        {
                            return Return.returnHttp("201", "Please select minimum one programme part term.", null);
                        }

                        int ExamMasterId = Convert.ToInt32(ObjCopyExamFeeConfiguration.ExamMasterId);
                        int FacultyExamMapId = Convert.ToInt32(ObjCopyExamFeeConfiguration.FacultyExamMapId);
                        int ProgrammePartTermIdFrom = Convert.ToInt32(ObjCopyExamFeeConfiguration.ProgrammePartTermIdFrom);
                        int Cnt = ObjCopyExamFeeConfiguration.programmePartTermList.Count;
                        bool? flag = true;
                        string strMessage = string.Empty;
                        if (Cnt > 0)
                        {
                            for (int i = 0; i < Cnt; i++)
                            {
                                if (ObjCopyExamFeeConfiguration.programmePartTermList[i].IsSelected == true)
                                {
                                    int ProgrammePartTermId = ObjCopyExamFeeConfiguration.programmePartTermList[i].Id;
                                    SqlCommand cmd = new SqlCommand("CopyExamFeeConfiguration", con);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@ProgrammePartTermId", ProgrammePartTermId);
                                    cmd.Parameters.AddWithValue("@ProgrammePartTermIdFrom", ProgrammePartTermIdFrom);
                                    cmd.Parameters.AddWithValue("@FacultyExamMapId", FacultyExamMapId);
                                    cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                                    cmd.Parameters.AddWithValue("@UserId", UserId);
                                    cmd.Parameters.AddWithValue("@UserTime", datetime);


                                    cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                                    cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                                    if (strMessage != "TRUE")
                                    {
                                        flag = false;

                                    }
                                    con.Close();
                                }
                            }
                        }

                        if (flag == true)
                        {
                            strMessage = "Your data has been copied successfully.";
                            return Return.returnHttp("200", strMessage.ToString(), null);
                        }

                        return Return.returnHttp("201", strMessage, null);
                    }
                    catch (Exception e)
                    {
                        return Return.returnHttp("201", e.Message, null);
                    }
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }


        #endregion
    }
}