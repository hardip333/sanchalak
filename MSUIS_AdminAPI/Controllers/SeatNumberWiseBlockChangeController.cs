using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;

namespace MSUISApi.Controllers
{
    public class SeatNumberWiseBlockChangeController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

        #region SeatNumberWiseBlockChange
        [HttpPost]
        public HttpResponseMessage SeatNumberWiseBlockChange(SeatNumberWiseBlockChange ObjSN)
        {
            SeatNumberWiseBlockChange modelobj = new SeatNumberWiseBlockChange();

            try
                {
                modelobj.SeatNumber = ObjSN.SeatNumber;
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("SeatNumberWiseBlockChange", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SeatNumber", modelobj.SeatNumber);
                

                sda.SelectCommand = cmd;
                sda.Fill(dt);
                Int32 count = 0;
                List<SeatNumberWiseBlockChange> ObjLstObjSNW = new List<SeatNumberWiseBlockChange>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        SeatNumberWiseBlockChange ObjSNWB = new SeatNumberWiseBlockChange();
                        ObjSNWB.PRN = Convert.ToInt64(dr["PRN"]);
                        ObjSNWB.SeatNumber = Convert.ToString(dr["SeatNumber"]);

                        ObjSNWB.MstPaperId = (((dr["MstPaperId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(dr["MstPaperId"]);

                        ObjSNWB.ExamVenueId = (((dr["ExamVenueId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(dr["ExamVenueId"]);
                        ObjSNWB.ExamVenueName = (Convert.ToString(dr["ExamVenueName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["ExamVenueName"]);
                        
                        ObjSNWB.ExamBlockId = (((dr["ExamBlockId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(dr["ExamBlockId"]);
                        ObjSNWB.ExamBlockId1 = (((dr["ExamBlockId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(dr["ExamBlockId"]);
                        
                        ObjSNWB.ExamBlockName = (Convert.ToString(dr["ExamBlockName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["ExamBlockName"]);
                        ObjSNWB.ExamBlockName1 = (Convert.ToString(dr["ExamBlockName"])).IsEmpty() ? "Not Defined" : Convert.ToString(dr["ExamBlockName"]);
                        ObjSNWB.PaperCodeName = Convert.ToString(dr["PaperCodeName"]);
                        count = count + 1;
                        ObjSNWB.IndexId = count;

                        ObjLstObjSNW.Add(ObjSNWB);
                    }

                }
                return Return.returnHttp("200", ObjLstObjSNW, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion


        #region ExamBlockGet
        [HttpPost]
        public HttpResponseMessage ExamBlockGet(ExamBlocks EB)
        {
            ExamBlocks modelobj = new ExamBlocks(); 
            try
            {  
                SqlCommand cmd = new SqlCommand("ExamBlockDropDown", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamBlockId", EB.ExamBlockId);
                cmd.Parameters.AddWithValue("@ExamVenueId", EB.ExamVenueId);
                sda.SelectCommand = cmd;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                List<ExamBlocks> ObjLstF = new List<ExamBlocks>();
                //dt.Rows.Add("0", "All Faculty");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ExamBlocks ObjF = new ExamBlocks();
                        ObjF.ExamBlockId = Convert.ToInt32(dr["Id"]);
                        ObjF.ExamBlockName = (dr["ExamBlockName"].ToString());

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

        #region SeatNumberWiseBlockChangeEdit
        [HttpPost]
        public HttpResponseMessage SeatNumberWiseBlockChangeEdit(SeatNumberWiseBlockChange SNWBC)

        {
            //SeatNumberWiseBlockChange SNWBC = new SeatNumberWiseBlockChange();
            //dynamic jsonData = jsonobject;
            try
            {
                Int32 ExamBlockId = Convert.ToInt32(SNWBC.ExamBlockId);
                Int64 MstPaperId = Convert.ToInt64(SNWBC.MstPaperId);

                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

               Int64 UserId = Convert.ToInt64(responseToken);

                SqlCommand cmd = new SqlCommand("SeatNumberWiseBlockChangeEdit", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamBlockId", ExamBlockId);
                cmd.Parameters.AddWithValue("@MstPaperId", MstPaperId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();
                if (strMessage == "TRUE")
                {
                    strMessage = "Your Data Has Been Updated";
                }
                return Return.returnHttp("200", strMessage, null);

                
            }
            catch (Exception e)
            {

                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

    }

}