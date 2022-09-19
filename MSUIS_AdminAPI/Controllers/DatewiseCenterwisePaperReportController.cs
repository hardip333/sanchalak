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
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class DatewiseCenterwisePaperReportController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));


        #region DatewiseCenterwisePaperReportGet
        [HttpPost]
        public HttpResponseMessage DatewiseCenterwisePaperReportGet(DatewiseCenterwisePaperReport ObjVenue)
        {
            DatewiseCenterwisePaperReport modelobj = new DatewiseCenterwisePaperReport();

            try
            {
                //modelobj.FacultyExamMapId = ObjConReport.FacultyExamMapId;
                modelobj.ExamEventId = ObjVenue.ExamEventId;
                modelobj.FacultyId = ObjVenue.FacultyId;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("DatewiseCenterwisePaperReport", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamEventId", modelobj.ExamEventId);
                cmd.Parameters.AddWithValue("@FacultyId", modelobj.FacultyId);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<DatewiseCenterwisePaperReport> ObjLstVW = new List<DatewiseCenterwisePaperReport>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        DatewiseCenterwisePaperReport ObjVW = new DatewiseCenterwisePaperReport();

                        ObjVW.FacultyId = Convert.ToInt32(dr["FacultyId"].ToString());
                        ObjVW.ExamEventId = Convert.ToInt32(dr["ExamEventId"].ToString());
                        ObjVW.FacultyName = (dr["FacultyName"].ToString());
                        ObjVW.ExamEventName = (dr["ExamEventName"].ToString());
                        ObjVW.PaperCode = (dr["PaperCode"].ToString());
                        ObjVW.PaperName = (dr["PaperName"].ToString());
                        ObjVW.TLMAMAT = (dr["TLMAMAT"].ToString());
                        DateTime Date = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(dr["Date"]).ToUniversalTime(), INDIAN_ZONE);
                        ObjVW.Date = string.Format("{0: dd-MM-yyyy}", Date);
                        //ObjVW.Date = (dr["Date"].ToString());
                        ObjVW.Duration = (dr["Duration"].ToString());

                        ObjVW.ProgrammeName = (dr["ProgrammeName"].ToString());
                        ObjVW.PartName = (dr["PartName"].ToString());
                        // ObjVW.MOL = (dr["MOL"].ToString());
                        //ObjVW.Pattern = (dr["Pattern"].ToString());
                        ObjVW.BranchName = (dr["BranchName"].ToString());
                        ObjVW.PartTermName = (dr["PartTermName"].ToString());
                        //ObjVW.ExamVenueName = (dr["ExamVenueName"].ToString());
                        //ObjVW.ExamCenterName = (dr["ExamCenterName"].ToString());
                        ObjVW.ExamVenueName = (Convert.ToString(dr["ExamVenueName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["ExamVenueName"]);
                        ObjVW.ExamCenterName = (Convert.ToString(dr["ExamCenterName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["ExamCenterName"]);

                        ObjVW.StudentCount = Convert.ToInt32(dr["StudentCount"].ToString());


                        count = count + 1;
                        ObjVW.IndexId = count;
                        ObjLstVW.Add(ObjVW);
                    }

                }
                return Return.returnHttp("200", ObjLstVW, null);

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

                sda.Fill(dt);


                List<ExamEventMaster> ObjLstEEM = new List<ExamEventMaster>();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ExamEventMaster objEEM = new ExamEventMaster();

                        objEEM.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        objEEM.DisplayName = Convert.ToString(dt.Rows[i]["DisplayName"]);
                        objEEM.IsActive = Convert.ToBoolean(dt.Rows[i]["IsActive"]);
                        objEEM.IsDeleted = Convert.ToBoolean(dt.Rows[i]["IsDeleted"]);

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


        #region MstFacultyGet
        [HttpPost]
        public HttpResponseMessage FacultyGetById(MstFaculty MF)
        {
            //MstFaculty modelobj = new MstFaculty(); 
            try
            {
                //modelobj.Id = MF.Id;
                SqlCommand cmd = new SqlCommand("MstFacGetbyId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyId", MF.Id);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                List<MstFaculty> ObjLstF = new List<MstFaculty>();
                //dt.Rows.Add("0", "All Faculty");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MstFaculty ObjF = new MstFaculty();
                        ObjF.Id = Convert.ToInt32(dr["Id"]);
                        ObjF.FacultyName = (dr["FacultyName"].ToString());

                        ObjLstF.Add(ObjF);
                    }

                }

                return Return.returnHttp("200", ObjLstF, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

    }
}