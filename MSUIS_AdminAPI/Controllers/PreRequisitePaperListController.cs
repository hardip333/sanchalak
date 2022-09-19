using MSUIS_TokenManager.App_Start;
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
    public class PreRequisitePaperListController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlCommand cmd = new SqlCommand();

        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        [HttpPost]
        public HttpResponseMessage PreRequisitePaperListGet(PreRequisitePaperList paperList)
        {
            try
            {
                /*string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }*/
                List<PreRequisitePaperList> slist = new List<PreRequisitePaperList>();

                cmd.CommandText = "PreRequisitePaperListGet";
                cmd.Parameters.AddWithValue("@Flag", "PreRequisitePaperList");
                cmd.Parameters.AddWithValue("@SourcePartTermId", paperList.ProgrammeInstancePartTermId);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                da.SelectCommand = cmd;
                da.Fill(dt);
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                if (string.Equals(strMessage, "422"))
                {
                    return Return.returnHttp("201", "Opps, something went wrong.", null);
                }
                Int32 count = 0;

                foreach (DataRow DR in dt.Rows)
                {
                    PreRequisitePaperList s = new PreRequisitePaperList();

                    s.SourcePartTermId = Convert.ToInt64(DR["SourcePartTermId"].ToString());
                    s.InstancePartTermName = DR["InstancePartTermName"].ToString();
                    s.SourcePaperId = Convert.ToInt64(DR["SourcePaperId"].ToString());
                    s.PaperName = DR["PaperName"].ToString();
                    count = count + 1;
                    s.IndexId = count;

                    slist.Add(s);
                }
                return Return.returnHttp("200", slist, null);
            }
            catch (Exception e)
            {
                /*if (e.Message.ToString() == "The given header was not found.")
                {
                    return Return.returnHttp("0", "Session is expired, Kindly login again.", null);
                }
                else
                {*/
                    return Return.returnHttp("201", e.Message.ToString(), null);
                //}
            }
        }
        [HttpPost]
        public HttpResponseMessage PreRequisitePaperListGetByPTandPaperId(PreRequisitePaperList paperList)
        {
            try
            {                
                List<PreRequisitePaperList> slist = new List<PreRequisitePaperList>();

                cmd.CommandText = "PreRequisitePaperListGet";
                cmd.Parameters.AddWithValue("@Flag", "PreRequisitePaperListGetByPTandPaperId");
                cmd.Parameters.AddWithValue("@SourcePartTermId", paperList.SourcePartTermId);
                cmd.Parameters.AddWithValue("@SourcePaperId", paperList.SourcePaperId);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                da.SelectCommand = cmd;
                da.Fill(dt);                
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                if (string.Equals(strMessage, "422"))
                {
                    return Return.returnHttp("201", "Opps, something went wrong.", null);
                }
                Int32 count = 0;
                Int32 flagWithin = 0;
                Int32 flagAcross = 0;
                foreach (DataRow DR in dt.Rows)
                {
                    PreRequisitePaperList s = new PreRequisitePaperList();

                    s.SourceInstancePartTermName = DR["SourceInstancePartTermName"].ToString();
                    s.DestiInstancePartTermName = DR["DestiInstancePartTermName"].ToString();
                    s.SourcePaperName = DR["SourcePaperName"].ToString();
                    s.DestiPaperName = DR["DestiPaperName"].ToString();
                    //s.PreRequisiteLableName = DR["PreRequisiteLableName"].ToString();                                     
                    if (DR["PreRequisiteLableName"].ToString() == "Within Semester")
                    {
                        if (flagWithin == 0)
                        {
                            s.PreRequisiteLableName = DR["PreRequisiteLableName"].ToString();
                            flagWithin = 1;
                            flagAcross = 0;
                        }
                        else
                        {
                            s.PreRequisiteLableName = "";
                            flagAcross = 0;
                        }
                    }
                    if (DR["PreRequisiteLableName"].ToString() == "Across Semester")
                    {
                        if (flagAcross == 0)
                        {
                            s.PreRequisiteLableName = DR["PreRequisiteLableName"].ToString();
                            flagAcross = 1;
                            flagWithin = 0;
                        }
                        else
                        {
                            s.PreRequisiteLableName = "";
                            flagWithin = 0;
                        }
                    }
                    s.PreRequisiteTypeName = DR["PreRequisiteTypeName"].ToString();
                    s.Minimum = DR["Minimum"].ToString();
                    s.Maximum = DR["Maximum"].ToString();
                    count = count + 1;
                    s.IndexId = count;

                    slist.Add(s);
                }
                return Return.returnHttp("200", slist, null);
            }
            catch (Exception e)
            {                
                return Return.returnHttp("201", e.Message.ToString(), null);                
            }
        }
    }
}
