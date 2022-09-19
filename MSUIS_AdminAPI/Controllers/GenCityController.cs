using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using MSUISApi.Models;
using System.IO;
using Newtonsoft.Json.Linq;

namespace MSUISApi.Controllers
{
    public class GenCityController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();


        //Language table CRUD

        [HttpPost]
        public HttpResponseMessage getGenCityList()
        {
            //GenCity gencity = new GenCity();
            //dynamic jsonData = jsonobject;
            try
            {
                //String token = Request.Headers.GetValues("token").FirstOrDefault();
                //TokenOperation ac = new TokenOperation();
                //string res = ac.ValidateToken(token);
                //if (res == "0")
                //{
                //    return Return.returnHttp("0", null, null);
                //}

                SqlCommand cmd = new SqlCommand("GenCityGet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                List<GenCity> gencityList = new List<GenCity>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        GenCity gencity = new GenCity();
                        gencity.Id = Convert.ToInt32(dr["Id"].ToString());
                        gencity.CityName = dr["CityName"].ToString();
                        //gencity.TalukaId = Convert.ToInt32(dr["TalukaId"].ToString());
                        gencityList.Add(gencity);
                    }

                }
                return Return.returnHttp("200", gencityList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }

        [HttpPost]
        public HttpResponseMessage insertGenCityData(JObject jsonobject)
        {
            GenCity gencity = new GenCity();
            dynamic jsonData = jsonobject;
            try
            {
                //String token = Request.Headers.GetValues("token").FirstOrDefault();
                //TokenOperation ac = new TokenOperation();
                //string res = ac.ValidateToken(token);
                //if (res == "0")
                //{
                //    return Return.returnHttp("0", null, null);
                //}

                gencity.CityName = jsonData.CityName;
                gencity.TalukaId = Convert.ToInt32(jsonData.TalukaId);
                int UserId = 2;

                SqlCommand cmd = new SqlCommand("GenCityAdd", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CityName", gencity.CityName);
                cmd.Parameters.AddWithValue("@TalukaId", gencity.TalukaId);
                cmd.Parameters.AddWithValue("@UserId", UserId);


                con.Open();
                cmd.ExecuteNonQuery();

                con.Close();

                return Return.returnHttp("200", "Successfull", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }

        [HttpPost]
        public HttpResponseMessage updateGenCityData(JObject jsonobject)

        {
            GenCity gencity = new GenCity();
            dynamic jsonData = jsonobject;
            try
            {
                //String token = Request.Headers.GetValues("token").FirstOrDefault();
                //TokenOperation ac = new TokenOperation();
                //string res = ac.ValidateToken(token);
                //if (res == "0")
                //{
                //    return Return.returnHttp("0", null, null);
                //}

                gencity.Id = Convert.ToInt32(jsonData.Id);
                gencity.CityName = jsonData.CityName;
                gencity.TalukaId = Convert.ToInt32(jsonData.TalukaId);
                int UserId = Convert.ToInt32(jsonData.ModifiedBy);

                SqlCommand cmd = new SqlCommand("GenCityEdit", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", gencity.Id);
                cmd.Parameters.AddWithValue("@CityName", gencity.CityName);
                cmd.Parameters.AddWithValue("@TalukaId", gencity.TalukaId);
                cmd.Parameters.AddWithValue("@UserId", UserId);

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
                return Return.returnHttp("201", "Some Internal Error Occured.", null);
            }
        }

        [HttpPost]

        public HttpResponseMessage deleteGenCityData(JObject jsonobject)
        {
            GenCity gencity = new GenCity();
            dynamic jsonData = jsonobject;
            try
            {
                //String token = Request.Headers.GetValues("token").FirstOrDefault();
                //TokenOperation ac = new TokenOperation();
                //string res = ac.ValidateToken(token);
                //if (res == "0")
                //{
                //    return Return.returnHttp("0", null, null);
                //}

                gencity.Id = Convert.ToInt32(jsonData.Id);
                SqlCommand cmd = new SqlCommand("GenCityDelete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", gencity.Id);

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
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
    }
}
