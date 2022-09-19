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
    public class PrerequisiteController : ApiController
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));


        #region GetSourcePaperListByPTId
        [HttpPost]
        public HttpResponseMessage GetSourcePaperListByPTId(Prerequisite ObjProg)
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

                List<Prerequisite> PaperList = new List<Prerequisite>();

                SqlCommand cmd = new SqlCommand("PrerequisiteGetSourcePaperByPTid", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ObjProg.SourcePartTermId);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        Prerequisite Paper = new Prerequisite();
                        Paper.Id = (((Dt.Rows[i]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["Id"]);
                        Paper.SourcePaperId = (((Dt.Rows[i]["PaperId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["PaperId"]);
                        Paper.SourcePartTermId = (((Dt.Rows[i]["ProgrammeInstancePartTermId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["ProgrammeInstancePartTermId"]);
                        Paper.SourcePaperName = (((Dt.Rows[i]["PaperName"]).ToString()).IsEmpty()) ? "Not Defined" : Convert.ToString(Dt.Rows[i]["PaperName"]);
                        Paper.SourcePartTermName = (((Dt.Rows[i]["InstancePartTermName"]).ToString()).IsEmpty()) ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);


                        PaperList.Add(Paper);
                    }
                }
                if (PaperList != null)
                    return Return.returnHttp("200", PaperList, null);
                else
                    return Return.returnHttp("201", "No Record Found", null);

            }
            catch (Exception e)
            {

                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region GetDestinationPaperListByPTIdPaperId
        [HttpPost]
        public HttpResponseMessage GetDestinationPaperListByPTIdPaperId(Prerequisite ObjProg)
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

                List<Prerequisite> PaperList = new List<Prerequisite>();

                SqlCommand cmd = new SqlCommand("PrerequisiteGetDestinationPaperByPTidPaperidPrereqid", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SourcePartTermId", ObjProg.SourcePartTermId);
                cmd.Parameters.AddWithValue("@SourceIncPaperId", ObjProg.SourceIncPaperId);
                cmd.Parameters.AddWithValue("@PrerequisiteTypeId", ObjProg.PrerequisiteTypeId);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    //for (int i = 0; i < Dt.Rows.Count; i++)
                    foreach(DataRow d in Dt.Rows)
                    {
                        Prerequisite Paper = new Prerequisite();
                        
                        Paper.DestinationIncPaperId = (((d["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(d["Id"]);
                       
                        Paper.DestinationPaperId = (((d["PaperId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(d["PaperId"]);
                        Paper.DestinationPartTermId = (((d["ProgrammeInstancePartTermId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(d["ProgrammeInstancePartTermId"]);
                        Paper.DestinationPaperName = (((d["PaperName"]).ToString()).IsEmpty()) ? "Not Defined" : Convert.ToString(d["PaperName"]);
                        Paper.DestinationPartTermName = (((d["InstancePartTermName"]).ToString()).IsEmpty()) ? "Not Defined" : Convert.ToString(d["InstancePartTermName"]);
                        Paper.DestinationPaperSelected = (((d["DestinationPaperSelected"]).ToString()).IsEmpty()) ? false : Convert.ToBoolean(d["DestinationPaperSelected"]);
                        Paper.Minimum = (((d["Minimum"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(d["Minimum"]);
                        Paper.Maximum = (((d["Maximum"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(d["Maximum"]);
                        Paper.PreRequisiteTypeNameCurrent = (((d["PreRequisiteTypeNameCurrent"]).ToString()).IsEmpty()) ? "" : Convert.ToString(d["PreRequisiteTypeNameCurrent"]);
                        Paper.PaperSelectedDisabled = Paper.DestinationPaperSelected;
                        Paper.SourceIncPaperId = ObjProg.SourceIncPaperId;
                        Paper.SourcePaperId = ObjProg.SourcePaperId;
                        Paper.SourcePaperName = ObjProg.SourcePaperName;
                        Paper.SourcePartTermId = ObjProg.SourcePartTermId;
                        Paper.SourcePartTermName = ObjProg.SourcePartTermName;                        
                        Paper.PrerequisiteTypeId = ObjProg.PrerequisiteTypeId;
                        Paper.PrerequisiteTypeName = ObjProg.PrerequisiteTypeName;
                        Paper.PrerequisiteLableId = 1;
                        PaperList.Add(Paper);
                    }
                }
                if (PaperList != null)
                { return Return.returnHttp("200", PaperList, null); }
                else
                { return Return.returnHttp("201", "No Record Found", null); }

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region AddPrereqWithin
        [HttpPost]
        public HttpResponseMessage AddPrereqWithin(List<Prerequisite> ObjProg)
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

                DataTable Prereq = new DataTable();
                //PaperList.Columns.Add(new DataColumn("Id", typeof(Int64)));
                Prereq.Columns.Add(new DataColumn("SourcePartTermId", typeof(Int64)));
                Prereq.Columns.Add(new DataColumn("DestinationPartTermId", typeof(Int64)));
                Prereq.Columns.Add(new DataColumn("SourcePaperId", typeof(Int64)));
                Prereq.Columns.Add(new DataColumn("DestinationPaperId", typeof(Int64)));
                Prereq.Columns.Add(new DataColumn("SourceIncPaperId", typeof(Int64)));
                Prereq.Columns.Add(new DataColumn("DestinationIncPaperId", typeof(Int64)));
                Prereq.Columns.Add(new DataColumn("PrerequisiteLableId", typeof(Int32)));
                Prereq.Columns.Add(new DataColumn("PrerequisiteTypeId", typeof(Int32)));
                Prereq.Columns.Add(new DataColumn("Maximum", typeof(Int32)));
                Prereq.Columns.Add(new DataColumn("Minimum", typeof(Int32)));
                
                Prereq.Columns.Add(new DataColumn("UserId", typeof(Int64)));
                Prereq.Columns.Add(new DataColumn("UserTime", typeof(DateTime)));
                foreach (Prerequisite p in ObjProg)
                {
                    if (p.DestinationPaperSelected == true && p.PaperSelectedDisabled == false)
                    {
                        if (p.PrerequisiteTypeId != 3)
                        {
                            Prereq.Rows.Add(p.SourcePartTermId
                                            , p.DestinationPartTermId
                                            , p.SourcePaperId
                                            , p.DestinationPaperId
                                            , p.SourceIncPaperId
                                            , p.DestinationIncPaperId
                                            , p.PrerequisiteLableId
                                            , p.PrerequisiteTypeId
                                            , p.Maximum
                                            , p.Minimum
                                            , res
                                            , datetime);
                        }
                        else
                        {
                            if(p.Minimum >0 && p.Maximum >= p.Minimum)
                            {

                                Prereq.Rows.Add(p.SourcePartTermId
                                            , p.DestinationPartTermId
                                            , p.SourcePaperId
                                            , p.DestinationPaperId
                                            , p.SourceIncPaperId
                                            , p.DestinationIncPaperId
                                            , p.PrerequisiteLableId
                                            , p.PrerequisiteTypeId
                                            , p.Maximum
                                            , p.Minimum
                                            , res
                                            , datetime);
                            }
                            else { return Return.returnHttp("201", "Minimum Maximum can not be blank", null); }
                        }
                        
                    }
                }
                SqlTransaction ST;
                SqlCommand CMD = new SqlCommand();
                
                CMD = new SqlCommand("PrerequisitePaperMapAdd", Con);
                CMD.CommandType = CommandType.StoredProcedure;
                CMD.Parameters.AddWithValue("@Prereq", Prereq);
                CMD.Parameters.AddWithValue("@UserId", res);
                CMD.Parameters.AddWithValue("@UserTime", datetime);
                CMD.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                CMD.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                ST = Con.BeginTransaction();
                try
                {
                    CMD.Transaction = ST;
                    int ans = CMD.ExecuteNonQuery();
                    String msg = Convert.ToString(CMD.Parameters["@Message"].Value);



                    ST.Commit();
                    return Return.returnHttp("200", msg, null);
                    //return true;
                }
                catch (SqlException sqlError)
                {
                    ST.Rollback();
                    String strMessageErr = Convert.ToString(sqlError);
                    return Return.returnHttp("201", strMessageErr, null);
                    //return false;
                }
            }
            catch (Exception e)
            {

                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region GetDestinationAccrossPaperListByPTIdPaperId
        [HttpPost]
        public HttpResponseMessage GetDestinationAccrossPaperListByPTIdPaperId(Prerequisite ObjProg)
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
                //Get All PartTerm
                List<ProgrammeInstancePartTerm> PTList = new List<ProgrammeInstancePartTerm>();

                SqlCommand cmd = new SqlCommand("PrerequisiteGetPartTermListByPTidPaperidPrereqid", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SourcePartTermId", ObjProg.SourcePartTermId);
                
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePartTerm PT = new ProgrammeInstancePartTerm();
                        PT.Id = (((Dt.Rows[i]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt.Rows[i]["Id"]);
                        PT.InstancePartTermName = (((Dt.Rows[i]["InstancePartTermName"]).ToString()).IsEmpty()) ? "Not Defined" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                        
                        PTList.Add(PT);
                    }
                }
                //Get All Paper
                List<Prerequisite> PaperList = new List<Prerequisite>();

                SqlCommand cmd1 = new SqlCommand("PrerequisiteGetAcrossDestinationPaperByPTidPaperidPrereqid", Con);
                cmd1.CommandType = CommandType.StoredProcedure;

                cmd1.Parameters.AddWithValue("@SourcePartTermId", ObjProg.SourcePartTermId);
                cmd1.Parameters.AddWithValue("@SourceIncPaperId", ObjProg.SourceIncPaperId);
                cmd1.Parameters.AddWithValue("@PrerequisiteTypeId", ObjProg.PrerequisiteTypeId);
                SqlDataAdapter Da1 = new SqlDataAdapter();
                DataTable Dt1 = new DataTable();
                Da1.SelectCommand = cmd1;
                Da1.Fill(Dt1);
                if (Dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt1.Rows.Count; i++)
                    {
                        Prerequisite Paper = new Prerequisite();
                        Paper.DestinationIncPaperId = (((Dt1.Rows[i]["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt1.Rows[i]["Id"]);
                        Paper.DestinationPaperId = (((Dt1.Rows[i]["PaperId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt1.Rows[i]["PaperId"]);
                        Paper.DestinationPartTermId = (((Dt1.Rows[i]["ProgrammeInstancePartTermId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(Dt1.Rows[i]["ProgrammeInstancePartTermId"]);
                        Paper.DestinationPaperName = (((Dt1.Rows[i]["PaperName"]).ToString()).IsEmpty()) ? "Not Defined" : Convert.ToString(Dt1.Rows[i]["PaperName"]);
                        Paper.DestinationPartTermName = (((Dt1.Rows[i]["InstancePartTermName"]).ToString()).IsEmpty()) ? "Not Defined" : Convert.ToString(Dt1.Rows[i]["InstancePartTermName"]);
                        Paper.DestinationPaperSelected = (((Dt1.Rows[i]["DestinationPaperSelected"]).ToString()).IsEmpty()) ? false : Convert.ToBoolean(Dt1.Rows[i]["DestinationPaperSelected"]);
                        Paper.Minimum = (((Dt1.Rows[i]["Minimum"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt1.Rows[i]["Minimum"]);
                        Paper.Maximum = (((Dt1.Rows[i]["Maximum"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt1.Rows[i]["Maximum"]);                        
                        Paper.PreRequisiteTypeNameCurrent = (((Dt1.Rows[i]["PreRequisiteTypeNameCurrent"]).ToString()).IsEmpty()) ? "" : Convert.ToString(Dt1.Rows[i]["PreRequisiteTypeNameCurrent"]);
                        Paper.PaperSelectedDisabled = Paper.DestinationPaperSelected;
                        Paper.SourceIncPaperId = ObjProg.SourceIncPaperId;
                        Paper.SourcePaperId = ObjProg.SourcePaperId;
                        Paper.SourcePaperName = ObjProg.SourcePaperName;
                        Paper.SourcePartTermId = ObjProg.SourcePartTermId;
                        Paper.SourcePartTermName = ObjProg.SourcePartTermName;
                        Paper.PrerequisiteTypeId = ObjProg.PrerequisiteTypeId;
                        Paper.PrerequisiteTypeName = ObjProg.PrerequisiteTypeName;
                        Paper.PrerequisiteLableId = 2;
                        PaperList.Add(Paper);
                    }
                }
                //Add Respective paper to part term
                foreach (ProgrammeInstancePartTerm p in PTList)
                {
                   
                    List<Prerequisite> Paper1List = PaperList.Where(e => e.DestinationPartTermId == p.Id).ToList();
                    if (PaperList.Any())
                    {
                        p.PaperPrereqList = Paper1List;
                    }
                }


                if (PTList != null)
                { return Return.returnHttp("200", PTList, null); }
                else
                { return Return.returnHttp("201", "No Record Found", null); }

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region AddPrereqAcross
        [HttpPost]
        public HttpResponseMessage AddPrereqAcross(List<ProgrammeInstancePartTerm> ObjProg)
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

                DataTable Prereq = new DataTable();
                Prereq.Columns.Add(new DataColumn("SourcePartTermId", typeof(Int64)));
                Prereq.Columns.Add(new DataColumn("DestinationPartTermId", typeof(Int64)));
                Prereq.Columns.Add(new DataColumn("SourcePaperId", typeof(Int64)));
                Prereq.Columns.Add(new DataColumn("DestinationPaperId", typeof(Int64)));
                Prereq.Columns.Add(new DataColumn("SourceIncPaperId", typeof(Int64)));
                Prereq.Columns.Add(new DataColumn("DestinationIncPaperId", typeof(Int64)));
                Prereq.Columns.Add(new DataColumn("PrerequisiteLableId", typeof(Int32)));
                Prereq.Columns.Add(new DataColumn("PrerequisiteTypeId", typeof(Int32)));
                Prereq.Columns.Add(new DataColumn("Maximum", typeof(Int32)));
                Prereq.Columns.Add(new DataColumn("Minimum", typeof(Int32)));

                Prereq.Columns.Add(new DataColumn("UserId", typeof(Int64)));
                Prereq.Columns.Add(new DataColumn("UserTime", typeof(DateTime)));
                foreach (ProgrammeInstancePartTerm pt in ObjProg)
                {
                    foreach (Prerequisite p in pt.PaperPrereqList)
                    {
                        if (p.DestinationPaperSelected == true && p.PaperSelectedDisabled == false)
                        { 
                            if (p.PrerequisiteTypeId != 3)
                            {
                                Prereq.Rows.Add(p.SourcePartTermId
                                                , p.DestinationPartTermId
                                                , p.SourcePaperId
                                                , p.DestinationPaperId
                                                , p.SourceIncPaperId
                                                , p.DestinationIncPaperId
                                                , p.PrerequisiteLableId
                                                , p.PrerequisiteTypeId
                                                , p.Maximum
                                                , p.Minimum
                                                , res
                                                , datetime);
                            }
                            else
                            {
                                if (p.Minimum > 0 && p.Maximum >= p.Minimum)
                                {
                                    Prereq.Rows.Add(p.SourcePartTermId
                                                , p.DestinationPartTermId
                                                , p.SourcePaperId
                                                , p.DestinationPaperId
                                                , p.SourceIncPaperId
                                                , p.DestinationIncPaperId
                                                , p.PrerequisiteLableId
                                                , p.PrerequisiteTypeId
                                                , p.Maximum
                                                , p.Minimum
                                                , res
                                                , datetime);
                                }
                                else { return Return.returnHttp("201", "Minimum Maximum can not be blank", null); }
                            }

                        }
                    }
                }
                SqlTransaction ST;
                SqlCommand CMD = new SqlCommand();

                CMD = new SqlCommand("PrerequisitePaperMapAddAcross", Con);
                CMD.CommandType = CommandType.StoredProcedure;
                CMD.Parameters.AddWithValue("@Prereq", Prereq);
                CMD.Parameters.AddWithValue("@UserId", res);
                CMD.Parameters.AddWithValue("@UserTime", datetime);
                CMD.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                CMD.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                ST = Con.BeginTransaction();
                try
                {
                    CMD.Transaction = ST;
                    int ans = CMD.ExecuteNonQuery();
                    String msg = Convert.ToString(CMD.Parameters["@Message"].Value);



                    ST.Commit();
                    return Return.returnHttp("200", msg, null);
                    //return true;
                }
                catch (SqlException sqlError)
                {
                    ST.Rollback();
                    String strMessageErr = Convert.ToString(sqlError);
                    return Return.returnHttp("201", strMessageErr, null);
                    //return false;
                }
            }
            catch (Exception e)
            {

                return Return.returnHttp("201", e.Message.ToString(), null);
            }

        }
        #endregion

        #region GetPrereqList
        [HttpPost]
        public HttpResponseMessage GetPrereqList(Prerequisite ObjProg)
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
                SqlCommand cmd = new SqlCommand("PrerequisiteGetListbySPIdAndSPTId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SourcePartTermId", ObjProg.SourcePartTermId);
                cmd.Parameters.AddWithValue("@SourceIncPaperId", ObjProg.SourceIncPaperId);
                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<Prerequisite> ObjPrereqList = new List<Prerequisite>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow d in Dt.Rows)
                    {
                        Prerequisite Paper = new Prerequisite();
                        Paper.Id = (((d["Id"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(d["Id"]);
                        Paper.DestinationIncPaperId = (((d["DestinationIncPaperId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(d["DestinationIncPaperId"]);
                        Paper.DestinationPaperId = (((d["DestinationPaperId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(d["DestinationPaperId"]);
                        Paper.DestinationPartTermId = (((d["DestinationPartTermId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(d["DestinationPartTermId"]);
                        Paper.DestinationPaperName = (((d["DestinationPaperName"]).ToString()).IsEmpty()) ? "Not Defined" : Convert.ToString(d["DestinationPaperName"]);
                        Paper.DestinationPartTermName = (((d["DestinationPartTermName"]).ToString()).IsEmpty()) ? "Not Defined" : Convert.ToString(d["DestinationPartTermName"]);
                        Paper.Minimum = (((d["Minimum"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(d["Minimum"]);
                        Paper.Maximum = (((d["Maximum"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(d["Maximum"]);
                        Paper.SourceIncPaperId = (((d["SourceIncPaperId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(d["SourceIncPaperId"]);
                        Paper.SourcePaperId = (((d["SourcePaperId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(d["SourcePaperId"]);
                        Paper.SourcePaperName = (((d["SourcePaperName"]).ToString()).IsEmpty()) ? "Not Defined" : Convert.ToString(d["SourcePaperName"]);
                        Paper.SourcePartTermId = (((d["SourcePartTermId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt64(d["SourcePartTermId"]);
                        Paper.SourcePartTermName = (((d["SourcePartTermName"]).ToString()).IsEmpty()) ? "Not Defined" : Convert.ToString(d["SourcePartTermName"]);
                        Paper.PrerequisiteTypeId = (((d["PrerequisiteTypeId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(d["PrerequisiteTypeId"]);
                        Paper.PrerequisiteTypeName = (((d["PrerequisiteTypeName"]).ToString()).IsEmpty()) ? "Not Defined" : Convert.ToString(d["PrerequisiteTypeName"]);
                        Paper.PrerequisiteLableId = (((d["PrerequisiteLableId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(d["PrerequisiteLableId"]);
                        Paper.PrerequisiteLableName= (((d["PrerequisiteLableName"]).ToString()).IsEmpty()) ? "Not Defined" : Convert.ToString(d["PrerequisiteLableName"]);


                        ObjPrereqList.Add(Paper);
                    }
                }
                return Return.returnHttp("200", ObjPrereqList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }
        }
        #endregion

        [HttpPost]
        public HttpResponseMessage DeletePrereq(Prerequisite P)
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

                Int64 Id = Convert.ToInt64(P.Id);

                SqlCommand cmd = new SqlCommand("PrerequisitePaperMapDelete", Con);
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
