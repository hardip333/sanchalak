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
using System.Dynamic;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class TimeSlotController : ApiController 
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region TimeSlotGet
        [HttpPost]
        public HttpResponseMessage TimeSlotGet()
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

                var timespan = TimeSpan.FromHours(24);
                SqlCommand cmd = new SqlCommand("TimeSlotGet", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<TimeSlot> ObjTimeSlot = new List<TimeSlot>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        TimeSlot ObjTS = new TimeSlot();
                        ObjTS.Code = Convert.ToInt64(dr["Code"]);
                        ObjTS.StartTime = TimeSpan.Parse(Convert.ToString(dr["StartTime"]));
                        ObjTS.EndTime = TimeSpan.Parse(Convert.ToString(dr["EndTime"]));
                        ObjTS.StartTimeView = Convert.ToString(dr["StartTime"]);
                        ObjTS.EndTimeView = Convert.ToString(dr["EndTime"]);
                        ObjTS.TimeSlotName = (dr["TimeSlotName"].ToString());

                        ObjTimeSlot.Add(ObjTS);
                    }

                }
                return Return.returnHttp("200", ObjTimeSlot, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region TimeSlotAdd
        [HttpPost]
        public HttpResponseMessage TimeSlotAdd(TimeSlot ObjTimeSlot)
        {
            TimeSlot modelobj = new TimeSlot();

            try 
            {
                //modelobj.StartTime = ObjTimeSlot.StartTime;
                //modelobj.EndTime = ObjTimeSlot.EndTime;
                string TimeSlotName = ObjTimeSlot.TimeSlotName;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                TimeSpan StartTime = ObjTimeSlot.DtStartTime.TimeOfDay;
                TimeSpan EndTime = ObjTimeSlot.DtEndTime.TimeOfDay;
                TimeSpan Diff = new TimeSpan(5, 30, 00);
                StartTime = StartTime.Add(Diff);
                EndTime = EndTime.Add(Diff);

                SqlCommand cmd = new SqlCommand("TimeSlotAdd", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StartTime", StartTime);
                cmd.Parameters.AddWithValue("@EndTime", EndTime);
                cmd.Parameters.AddWithValue("@TimeSlotName", TimeSlotName);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                Con.Close();

                return Return.returnHttp("200", strMessage.ToString(), null);

                //}
            }
            catch (Exception e)
            {

                return Return.returnHttp("201", e.Message.ToString(), null);

            }

        }
        #endregion

        #region TimeSlotEdit
        [HttpPost]
        public HttpResponseMessage TimeSlotEdit(TimeSlot TS)

        {

            try
            {
                Int64 Code = Convert.ToInt64(TS.Code);
                //TimeSpan StartTime = TS.EndTime.Subtract(TS.StartTime);
                //TimeSpan EndTime = TS.StartTime.Subtract(TS.EndTime);
                string TimeSlotName = Convert.ToString(TS.TimeSlotName);

                TimeSpan StartTime = TS.DtStartTime.TimeOfDay;
                TimeSpan EndTime = TS.DtEndTime.TimeOfDay;
                TimeSpan Diff = new TimeSpan(5, 30, 00);
                StartTime = StartTime.Add(Diff);
                EndTime = EndTime.Add(Diff);

                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("TimeSlotEdit", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Code", Code);
                cmd.Parameters.AddWithValue("@StartTime", StartTime);
                cmd.Parameters.AddWithValue("@EndTime", EndTime);
                cmd.Parameters.AddWithValue("@TimeSlotName", TimeSlotName);
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

        #region TimeSlotDelete
        [HttpPost]
        public HttpResponseMessage TimeSlotDelete(TimeSlot ObjTimeSlot)
        {
            TimeSlot modelobj = new TimeSlot();

            try
            {
                modelobj.Code = Convert.ToInt64(ObjTimeSlot.Code);
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }


                SqlCommand cmd = new SqlCommand("TimeSlotDelete", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Code", modelobj.Code);

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
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion
    }
}
