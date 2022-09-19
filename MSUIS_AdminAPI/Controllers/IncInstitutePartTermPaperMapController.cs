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
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class IncInstitutePartTermPaperMapController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));


        [HttpPost]
        public HttpResponseMessage PartTermPaperMapListGet(IncInstitutePartTermPaperMap IPTPM)
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

                SqlCommand cmd = new SqlCommand("IncProgInstPartTermPaperMapGet", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgInstPartTermId", IPTPM.ProgInstPartTermId);
                cmd.Parameters.AddWithValue("@InstituteId", IPTPM.InstituteId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<PaperPT> PaperData= new List<PaperPT>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        PaperPT Paper = new PaperPT();

                        
                        Paper.Id = (((Dt.Rows[i]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["Id"]);
                        Paper.InstitutePartTermPaperMapId = (((Dt.Rows[i]["InstitutePartTermPaperMapId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["InstitutePartTermPaperMapId"]);
                        Paper.PaperCheckSts = (Convert.ToString(Dt.Rows[i]["PaperCheckSts"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["PaperCheckSts"]);
                        
                        Paper.PaperName = (Convert.ToString(Dt.Rows[i]["PaperName"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["PaperName"]);
                        Paper.PaperCode = (Convert.ToString(Dt.Rows[i]["PaperCode"])).IsEmpty() ? "Not Defined" : Convert.ToString(Dt.Rows[i]["PaperCode"]);
                        

                        PaperData.Add(Paper);
                    }
                }
                return Return.returnHttp("200", PaperData, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }


        [HttpPost]
        public HttpResponseMessage IncInstitutePartTermPaperMapGet()
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

                SqlCommand cmd = new SqlCommand("IncInstitutePartTermPaperMapGet", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<IncInstitutePartTermPaperMap> IPTPMData = new List<IncInstitutePartTermPaperMap>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        IncInstitutePartTermPaperMap IPTPM = new IncInstitutePartTermPaperMap();

                        IPTPM.Id = Convert.ToInt64(Dt.Rows[i]["Id"]);
                        IPTPM.PartTermPaperMapId = Convert.ToInt64(Dt.Rows[i]["PartTermPaperMapId"]);
                        IPTPM.ProgInstPartTermId = Convert.ToInt64(Dt.Rows[i]["ProgInstPartTermId"]);

                        IPTPM.InstituteId = Convert.ToInt32(Dt.Rows[i]["InstituteId"]);

                        IPTPM.InstituteName = Convert.ToString(Dt.Rows[i]["InstituteName"]);
                        IPTPM.InstancePartTermName = Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                        IPTPM.PaperName = Convert.ToString(Dt.Rows[i]["PaperName"]);
                        IPTPM.PaperCode = Convert.ToString(Dt.Rows[i]["PaperCode"]);

                        IPTPMData.Add(IPTPM);
                    }
                }
                return Return.returnHttp("200", IPTPMData, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage IncInstitutePartTermPaperMapAdd(IncInstitutePartTermPaperMap IPTPM)
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
                Int32 InstituteId = Convert.ToInt32(IPTPM.InstituteId);
                //Int64 PartTermPaperMapId = Convert.ToInt64(IPTPM.PartTermPaperMapId);
                Int64 ProgInstPartTermId = Convert.ToInt64(IPTPM.ProgInstPartTermId);
                var PaperList = IPTPM.PaperList;
                int Count = Convert.ToInt32(PaperList.Count);
                String strMessage=null;
                SqlCommand[] CMDarray = new SqlCommand[Count];
                int i = 0;
                foreach (PaperPT Paper in PaperList)
                {
                    if (Paper.PaperCheck == true)
                    {
                        Int64 PartTermPaperMapId = Convert.ToInt64(Paper.Id);
                        CMDarray[i] = new SqlCommand("IncInstitutePartTermPaperMapAdd", Con, ST);
                        CMDarray[i].CommandType = CommandType.StoredProcedure;
                        CMDarray[i].Parameters.AddWithValue("@PartTermPaperMapId", PartTermPaperMapId);
                        CMDarray[i].Parameters.AddWithValue("@InstituteId", InstituteId);
                        CMDarray[i].Parameters.AddWithValue("@ProgInstPartTermId", ProgInstPartTermId);
                        CMDarray[i].Parameters.AddWithValue("@UserId", UserId);
                        CMDarray[i].Parameters.AddWithValue("@UserTime", datetime);
                        CMDarray[i].Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                        CMDarray[i].Parameters["@Message"].Direction = ParameterDirection.Output;
                        i++;
                    }

                }
                Con.Open();
                ST = Con.BeginTransaction();
                try
                {
                    for (int j = 0; j < i; j++)
                    {
                        CMDarray[j].Transaction = ST;
                        int ans = CMDarray[j].ExecuteNonQuery();
                        strMessage = strMessage + ", " + Convert.ToString(CMDarray[j].Parameters["@Message"].Value);
                    }

                    ST.Commit();
                    //return Return.returnHttp("200", msg + "-=-" + msg1, null);
                    return Return.returnHttp("200", "Record Added", null);
                }
                catch (SqlException sqlError)
                {
                    ST.Rollback();
                    String strMessageErr = Convert.ToString(sqlError);
                    //return Return.returnHttp("201", strMessageErr, null);
                    return Return.returnHttp("201", "Record already exists", null);
                }



            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }        

        [HttpPost]
        public HttpResponseMessage IncInstitutePartTermPaperMapDelete(IncInstitutePartTermPaperMap IPTPM)
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
                Int64 ProgInstPartTermId = Convert.ToInt64(IPTPM.ProgInstPartTermId);
                Int64 InstituteId = Convert.ToInt32(IPTPM.InstituteId);

                SqlCommand cmd = new SqlCommand("IncInstitutePartTermPaperMapDelete", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@ProgInstPartTermId", ProgInstPartTermId);
                cmd.Parameters.AddWithValue("@InstituteId", InstituteId);
                cmd.Parameters.AddWithValue("@UserTime", datetime);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);

                Con.Close();
                return Return.returnHttp("200", "Data Deleted", null);
                //return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }

        
    }
}
