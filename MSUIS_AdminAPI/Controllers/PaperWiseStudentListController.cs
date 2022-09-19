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
    public class PaperWiseStudentListController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        #region PaperWiseStudentListGet
        [HttpPost]
        public HttpResponseMessage PaperWiseStudentListGet(PaperWiseStudentList ObjPaper)
        {
            PaperWiseStudentList modelobj = new PaperWiseStudentList();
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

            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }
            try
            {

                SqlCommand cmd = new SqlCommand("PaperWiseStudentListByPaperId", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PaperId", ObjPaper.PaperId);
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                Int32 count = 0;
                sda.Fill(dt);
                

                List<PaperWiseStudentList> ObjLstPaper = new List<PaperWiseStudentList>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {

                        PaperWiseStudentList ObjPaperWise = new PaperWiseStudentList();

                        ObjPaperWise.ExamEventId = Convert.ToInt32(dr["ExamEventId"].ToString());
                        ObjPaperWise.PaperId = Convert.ToInt64(dr["PaperId"].ToString());
                        ObjPaperWise.StudentPRN = Convert.ToInt64(dr["StudentPRN"].ToString());
                        ObjPaperWise.SeatNumber = dr["SeatNumber"].ToString();
                        ObjPaperWise.PaperName = dr["PaperName"].ToString();
                        ObjPaperWise.PaperCode = dr["PaperCode"].ToString();
                        ObjPaperWise.ExamDate = Convert.ToDateTime(dr["ExamDate"].ToString());
                        ObjPaperWise.ExamDateView = Convert.ToString(dr["ExamDate"].ToString());
                        

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


        
    }



}