using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;
using Newtonsoft.Json.Linq;

namespace MSUISApi.Controllers
{
    public class PaperWiseStudentCountController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        #region PaperWiseStudentCountGet
        [HttpPost]
        public HttpResponseMessage PaperWiseStudentCountGet(PaperWiseStudentCount ObjPaper)
        {
            PaperWiseStudentCount modelobj = new PaperWiseStudentCount();
            if (ObjPaper.ExamDate != null)
            {
                DateTime ExamDate = new DateTime(1949, 1, 1);
                bool ChkDt = DateTime.TryParse(Convert.ToString(ObjPaper.ExamDate), out ExamDate);
                if (ChkDt)
                {
                    modelobj.ExamDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(ObjPaper.ExamDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    modelobj.ExamDateView = string.Format("{0: yyyy-MM-dd}", modelobj.ExamDate);
                }
                else
                {
                    modelobj.ExamDate = null;
                }
            }
            else
            {
                modelobj.ExamDate = null;
            }

            if (ObjPaper.ExamDate1 != null)
            {
                DateTime ExamDate1 = new DateTime(1949, 1, 1);
                bool ChkDt = DateTime.TryParse(Convert.ToString(ObjPaper.ExamDate1), out ExamDate1);
                if (ChkDt)
                {
                    modelobj.ExamDate1 = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(ObjPaper.ExamDate1).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    modelobj.ExamDateView1 = string.Format("{0: yyyy-MM-dd}", modelobj.ExamDate1);
                }
                else
                {
                    modelobj.ExamDate1 = null;
                }
            }
            else
            {
                modelobj.ExamDate1 = null;
            }




            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            try
            {

                SqlCommand cmd = new SqlCommand("PaperWiseStudentCount", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamEventId", ObjPaper.ExamEventId);
                //TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                //DateTime date1 = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(ObjPaper.ExamDate).ToUniversalTime(), INDIAN_ZONE);
                //ObjPaper.ExamDate = Convert.ToString(date1);
                cmd.Parameters.AddWithValue("@ExamDate", ObjPaper.ExamDate);
                //DateTime date2 = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(ObjPaper.ExamDate1).ToUniversalTime(), INDIAN_ZONE);
                //ObjPaper.ExamDate1 = Convert.ToString(date2);
                cmd.Parameters.AddWithValue("@ExamDate1", ObjPaper.ExamDate1);
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                Int32 count = 0;
                sda.Fill(dt);
                

                List<PaperWiseStudentCount> ObjLstPaper = new List<PaperWiseStudentCount>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {

                        PaperWiseStudentCount ObjPaperWise = new PaperWiseStudentCount();

                        ObjPaperWise.ExamEventId = Convert.ToInt32(dr["ExamEventId"].ToString());
                        ObjPaperWise.PaperId = Convert.ToInt64(dr["PaperId"].ToString());
                        ObjPaperWise.StudentCount = Convert.ToInt64(dr["StudentCount"].ToString());
                        ObjPaperWise.PaperName = dr["PaperName"].ToString();
                        ObjPaperWise.PaperCode = dr["PaperCode"].ToString();
                        ObjPaperWise.ExamDate = Convert.ToDateTime(dr["ExamDate"].ToString());
                        ObjPaperWise.ExamDate1 = Convert.ToDateTime(dr["ExamDate"].ToString());
                        ObjPaperWise.ExamDateView = Convert.ToString(dr["ExamDate"].ToString());
                        ObjPaperWise.ExamDateView1 = Convert.ToString(dr["ExamDate"].ToString());

                        count = count + 1;
                        ObjPaperWise.IndexId = count;


                        ObjLstPaper.Add(ObjPaperWise);

                    }
                }
                return Return.returnHttp("200", ObjLstPaper, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion


        #region ExamEventGet
        [HttpPost]
        public HttpResponseMessage ExamEventGet()
        {
            try
            {

                SqlCommand Cmd = new SqlCommand("ExamEventGet", con);
                Cmd.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand = Cmd;

                sda.Fill(Dt);


                List<ExamEventMaster> ObjLstEEM = new List<ExamEventMaster>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ExamEventMaster objEEM = new ExamEventMaster();

                        objEEM.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objEEM.DisplayName = Convert.ToString(Dt.Rows[i]["DisplayName"]);
                        objEEM.IsActive = Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objEEM.IsDeleted = Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);

                        ObjLstEEM.Add(objEEM);
                    }
                }
                return Return.returnHttp("200", ObjLstEEM, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion
    }



}