using MSUISApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using MSUIS_TokenManager.App_Start;
using MSUISApi.BAL;

namespace MSUISApi.Controllers
{
    public class EligibilityCriteriaConfigurationController : ApiController
    {
        #region Get Programme List from Faculty
        [HttpPost]
        public HttpResponseMessage getProgList(int FacultyId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("getProgListfromFaculty", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                Da.SelectCommand = Cmd;
                Da.Fill(Dt);
                List<progfromFaculty> list = new List<progfromFaculty>();
                list = Dt.DataTableToList<progfromFaculty>();
                return Return.returnHttp("200", list, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Get Programme Part List from Programme
        [HttpPost]
        public HttpResponseMessage getProgPartfromProg(int ProgId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("getProgPartfromProg", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@ProgId", ProgId);
                Da.SelectCommand = Cmd;
                Da.Fill(Dt);
                List<progPartfromProg> list = new List<progPartfromProg>();
                list = Dt.DataTableToList<progPartfromProg>();
                return Return.returnHttp("200", list, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Get Programme Part Term List from Programme Part
        [HttpPost]
        public HttpResponseMessage getProgPartTermfromProgPart(int ProgPartId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("getProgPartTermfromProgPart", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@ProgPartId", ProgPartId);
                Da.SelectCommand = Cmd;
                Da.Fill(Dt);
                List<progPartTermfromProgPart> list = new List<progPartTermfromProgPart>();
                list = Dt.DataTableToList<progPartTermfromProgPart>();
                return Return.returnHttp("200", list, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        //#region Checking For Already Existing Eligibility Detials
        //[HttpPost]
        //public HttpResponseMessage checkAlreadyExistingRecords(eligibilityCriteriaDetails ec1)
        //{
        //    try
        //    {
        //        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        //        SqlDataAdapter Da = new SqlDataAdapter();
        //        DataTable Dt = new DataTable();
        //        SqlCommand Cmd = new SqlCommand("checkAlreadyExistingRecords", Con);
        //        Cmd.CommandType = CommandType.StoredProcedure;
        //        Cmd.Parameters.AddWithValue("@ProgrammeId", ec1.ProgrammeId);
        //        Cmd.Parameters.AddWithValue("@ProgPartId", ec1.ProgPartId);
        //        Cmd.Parameters.AddWithValue("@ProgPartTermId", ec1.ProgPartTermId);
        //        //Cmd.Parameters.AddWithValue("@radioValue", ec1.radioValue);
        //        Da.SelectCommand = Cmd;
        //        Da.Fill(Dt);
        //        List<checkForExistingRecords> list = new List<checkForExistingRecords>();
        //        list = Dt.DataTableToList<checkForExistingRecords>();
        //        return Return.returnHttp("200", list, null);
        //    }
        //    catch (Exception e)
        //    {
        //        return Return.returnHttp("201", e.Message, null);
        //    }
        //}
        //#endregion

        //#region Checking for change in Level of Congiguration

        //[HttpPost]
        //public HttpResponseMessage checkLevel(eligibilityCriteriaDetails ec1)
        //{
        //    try
        //    {
        //        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        //        SqlDataAdapter Da = new SqlDataAdapter();
        //        DataTable Dt = new DataTable();
        //        SqlCommand Cmd = new SqlCommand("checkLevel", Con);
        //        Cmd.CommandType = CommandType.StoredProcedure;
        //        Cmd.Parameters.AddWithValue("@ProgrammeId", ec1.ProgrammeId);
        //        Cmd.Parameters.AddWithValue("@ProgPartId", ec1.ProgPartId);
        //        //Cmd.Parameters.AddWithValue("@ProgPartTermId", ec1.ProgPartTermId);
        //        Cmd.Parameters.AddWithValue("@radioValue", ec1.radioValue);
        //        Da.SelectCommand = Cmd;
        //        int radioChange;
        //        Con.Open();
        //        radioChange = Convert.ToInt16(Cmd.ExecuteScalar());
        //        Con.Close();
        //        return Return.returnHttp("200", radioChange, null);
        //    }
        //    catch (Exception e)
        //    {
        //        return Return.returnHttp("201", e.Message, null);
        //    }
        //}
        //#endregion

        #region Get Eligibility Criteria
        [HttpPost]
        public HttpResponseMessage getEligibilityCriteria(eligibilityCriteriaDetails ec1)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("getEligibilityCriteria4", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@ProgrammeId", ec1.ProgrammeId);
                Cmd.Parameters.AddWithValue("@ProgPartId", ec1.ProgPartId);
                //Cmd.Parameters.AddWithValue("@ProgPartTermId", ec1.ProgPartTermId);
                Cmd.Parameters.AddWithValue("@radioValue", ec1.radioValue);
                //Cmd.Parameters.AddWithValue("@configLevel", ec1.configLevel);
                Da.SelectCommand = Cmd;
                Da.Fill(Dt);
                List<ParentData> parent = new List<ParentData>();
                parent = Dt.DataTableToList<ParentData>();

                SqlDataAdapter Da1 = new SqlDataAdapter();
                DataTable Dt1 = new DataTable();
                SqlCommand Cmd1 = new SqlCommand("getEligibilityCriteria3", Con);
                Cmd1.CommandType = CommandType.StoredProcedure;
                Cmd1.Parameters.AddWithValue("@ProgrammeId", ec1.ProgrammeId);
                Cmd1.Parameters.AddWithValue("@ProgPartId", ec1.ProgPartId);
                //Cmd1.Parameters.AddWithValue("@ProgPartTermId", ec1.ProgPartTermId);
                Cmd1.Parameters.AddWithValue("@radioValue", ec1.radioValue);
                //Cmd.Parameters.AddWithValue("@configLevel", ec1.configLevel);
                Da1.SelectCommand = Cmd1;
                Da1.Fill(Dt1);
                List<sendSelectedData> children = new List<sendSelectedData>();
                children = Dt1.DataTableToList<sendSelectedData>();

                foreach (ParentData p1 in parent)
                {
                    if (p1.isPart)
                    {
                        List<sendSelectedData> children1 = children.Where(e => e.ProgPartIdforDb == p1.childId).ToList();
                        if (children1.Any())
                        {
                            p1.dataToBeSent = children1;
                        }
                    }
                    else
                    {
                        List<sendSelectedData> children1 = children.Where(e => e.ProgPartTermIdforDb == p1.childId).ToList();
                        if (children1.Any())
                        {
                            p1.dataToBeSent = children1;
                        }
                    }
                }
                // list = Dt.DataTableToList<dataSending>();
                return Return.returnHttp("200", parent, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Store Eligibility Criteria in Database
        [HttpPost]
        public HttpResponseMessage storeEligibilityCriteria(dataSending d1)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                DataTable DtTypeGroupTbl = new DataTable();
                //DtTypeGroupTbl.Columns.Add("Id", typeof(Int16));
                DtTypeGroupTbl.Columns.Add("ProgrammeId", typeof(Int16));
                DtTypeGroupTbl.Columns.Add("ProgrammePartId", typeof(Int16));
                DtTypeGroupTbl.Columns.Add("ProgrammePartTermId", typeof(Int16));
                DtTypeGroupTbl.Columns.Add("EligibleId", typeof(Int16));
                DtTypeGroupTbl.Columns.Add("AcademicYearId", typeof(Int16));
                DtTypeGroupTbl.Columns.Add("TypeforEligibilty", typeof(string));
                DtTypeGroupTbl.Columns.Add("eligibilityLabel", typeof(string));
                DtTypeGroupTbl.Columns.Add("EligStatus", typeof(bool));
                DtTypeGroupTbl.Columns.Add("isEligConsidered", typeof(int));
                DtTypeGroupTbl.Columns.Add("isPaperSelectionByStudent", typeof(string));
                DtTypeGroupTbl.Columns.Add("IsPaperSelectionBeforeFees", typeof(string));
                //DtTypeGroupTbl.Columns.Add("ResultStatus", typeof(string));

                for (int i = 0; i < d1.parentData.Count; i++)
                {
                    DataRow rowStudent = DtTypeGroupTbl.NewRow();
                    //rowStudent["ProgrammeId"] = d1.forDatabase.ProgrammeId;
                    Int32 ProgrammeId = d1.forDatabase.ProgrammeId;
                    Int32 ProgrammePartId = d1.forDatabase.ProgPartId;
                    Int32 ProgrammePartTermId = d1.forDatabase.ProgPartTermId;
                    Int32 AcademicYearId = d1.forDatabase.AcademicYearId;
                    Int32 EligibleId;
                    //rowStudent["ProgrammePartId"] = d1.forDatabase.ProgPartId;
                    //rowStudent["ProgrammePartTermId"] = d1.forDatabase.ProgPartTermId;
                    //rowStudent["AcademicYearId"] = d1.forDatabase.AcademicYearId;
                    if (d1.forDatabase.radioValue == "Part")
                    {
                        EligibleId = d1.parentData[i].childId;
                        //rowStudent["EligibleId"] = d1.parentData[i].childId;
                    }
                    else if (d1.forDatabase.radioValue == "PartTerm")
                    {
                         EligibleId = d1.parentData[i].childId;
                        //rowStudent["EligibleId"] = d1.parentData[i].childId;
                    }
                    else
                    {
                        return Return.returnHttp("205", "SELECT PROPER LEVEL FOR CONFIGURATION", null);
                    }
                    string TypeforEligibilty = d1.forDatabase.radioValue;
                    //rowStudent["TypeforEligibilty"] = d1.forDatabase.radioValue;
                    for (int j = 0; j < d1.parentData[i].dataToBeSent.Count; j++)
                    {
                        if (d1.parentData[i].dataToBeSent[j].EligStatus == true)
                        {
                            string eligibilityLabel = d1.parentData[i].dataToBeSent[j].eligibilityLabel;
                            bool EligStatus = d1.parentData[i].dataToBeSent[j].EligStatus;
                            bool isEligConsidered = Convert.ToBoolean(d1.forDatabase.isEligConsidered);
                            bool isPaperSelectionByStudent = Convert.ToBoolean(d1.forDatabase.isPaperSelectionByStudent);
                            bool IsPaperSelectionBeforeFees = Convert.ToBoolean(d1.forDatabase.IsPaperSelectionBeforeFees);
                            //rowStudent["eligibilityLabel"] = d1.parentData[i].dataToBeSent[j].eligibilityLabel;
                            //rowStudent["EligStatus"] = d1.parentData[i].dataToBeSent[j].EligStatus;
                            DtTypeGroupTbl.Rows.Add(ProgrammeId, ProgrammePartId, ProgrammePartTermId, EligibleId, AcademicYearId, TypeforEligibilty, eligibilityLabel, EligStatus , isEligConsidered , isPaperSelectionByStudent , IsPaperSelectionBeforeFees);

                            //DtTypeGroupTbl.Rows.Add(rowStudent);
                        }
                        //if (j + 1 < d1.parentData[i].dataToBeSent.Count && d1.parentData[i].dataToBeSent[j + 1].EligStatus == true)
                        //{
                        //    DataRow rowStudent1 = DtTypeGroupTbl.NewRow();
                        //    rowStudent1["ProgrammeId"] = d1.forDatabase.ProgrammeId;
                        //    rowStudent1["ProgrammePartId"] = d1.forDatabase.ProgPartId;
                        //    rowStudent1["ProgrammePartTermId"] = d1.forDatabase.ProgPartTermId;
                        //    rowStudent1["AcademicYearId"] = d1.forDatabase.AcademicYearId;
                        //    if (d1.forDatabase.radioValue == "Part")
                        //    {
                        //        rowStudent1["EligibleId"] = d1.parentData[i].childId;
                        //    }
                        //    else if (d1.forDatabase.radioValue == "PartTerm")
                        //    {
                        //        rowStudent1["EligibleId"] = d1.parentData[i].childId;
                        //    }
                        //    else
                        //    {
                        //        return Return.returnHttp("205", "SELECT PROPER LEVEL FOR CONFIGURATION", null);
                        //    }
                        //    rowStudent1["TypeforEligibilty"] = d1.forDatabase.radioValue;
                        //    rowStudent1["eligibilityLabel"] = d1.parentData[i].dataToBeSent[j+1].eligibilityLabel;
                        //    rowStudent1["EligStatus"] = d1.parentData[i].dataToBeSent[j+1].EligStatus;
                        //    DtTypeGroupTbl.Rows.Add(rowStudent1);
                        //    j += 1;
                        //}
                    }

                    //if(d1.dataToBeSent[i].PASS == true)
                    //{
                    //    rowStudent["ResultStatus"] = "PASS";
                    //    //Add the Data Row to Table

                    //}
                    //if (d1.dataToBeSent[i].checkBoth == true)
                    //{
                    //    DataRow rowStudent1 = DtTypeGroupTbl.NewRow();
                    //    rowStudent1["ProgrammeId"] = d1.forDatabase.ProgrammeId;
                    //    rowStudent1["ProgrammePartId"] = d1.forDatabase.ProgPartId;
                    //    rowStudent1["ProgrammePartTermId"] = d1.forDatabase.ProgPartTermId;
                    //    rowStudent1["AcademicYearId"] = d1.forDatabase.AcademicYearId;
                    //    if (d1.forDatabase.radioValue == "Part")
                    //    {
                    //        rowStudent1["EligibleId"] = d1.dataToBeSent[i].ProgPartIdforDb;
                    //    }
                    //    else if (d1.forDatabase.radioValue == "PartTerm")
                    //    {
                    //        rowStudent1["EligibleId"] = d1.dataToBeSent[i].ProgPartTermIdforDb;
                    //    }
                    //    else
                    //    {
                    //        return Return.returnHttp("205", "SELECT PROPER LEVEL FOR CONFIGURATION", null);
                    //    }
                    //    rowStudent1["TypeforEligibilty"] = d1.forDatabase.radioValue;
                    //    rowStudent["eligibilityLabel"] = "FAIL/ATKT";
                    //    rowStudent["EStatus"] = "FAIL/ATKT";
                    //    //rowStudent1["ResultStatus"] = "FAIL/ATKT";
                    //    //Add the Data Row to Table
                    //    DtTypeGroupTbl.Rows.Add(rowStudent1);
                    //}
                    //else
                    //{
                    //    continue;
                    //}
                    //else if(d1.dataToBeSent[i].FAIL == true)
                    //{
                    //    rowStudent["ResultStatus"] = "FAIL";
                    //    //Add the Data Row to Table
                    //    DtTypeGroupTbl.Rows.Add(rowStudent);
                    //}
                    //else if(d1.dataToBeSent[i].ABSENT == true)
                    //{
                    //    rowStudent["ResultStatus"] = "ABSENT";
                    //    //Add the Data Row to Table
                    //    DtTypeGroupTbl.Rows.Add(rowStudent);
                    //}

                }
                SqlCommand Cmd = new SqlCommand("storeEligibilityCriteria", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                string message = "error!";
                Cmd.Parameters.AddWithValue("@tbladdeligibilityconfiguration", DtTypeGroupTbl);
                Cmd.Parameters.AddWithValue("@AcademicYearId", d1.forDatabase.AcademicYearId);
                Cmd.Parameters.AddWithValue("@ProgPartId", d1.forDatabase.ProgPartId);
                Cmd.Parameters.AddWithValue("@ProgPartTermId", d1.forDatabase.ProgPartTermId);
                Cmd.Parameters.AddWithValue("@Message", message);
                //Cmd.Parameters.AddWithValue("@AcademicYearId", d1.forDatabase.AcademicYearId);
                Con.Open();
                Cmd.ExecuteNonQuery();
                Con.Close();
                //Cmd.Parameters.AddWithValue("@ProgPartId", ec1.ProgPartId);
                //Cmd.Parameters.AddWithValue("@ProgPartTermId", ec1.ProgPartTermId);
                //Cmd.Parameters.AddWithValue("@radioValue", ec1.radioValue);
                //Da.SelectCommand = Cmd;
                //Da.Fill(Dt);
                //List<eligibilityCriteriaSelected> list = new List<eligibilityCriteriaSelected>();
                //list = Dt.DataTableToList<eligibilityCriteriaSelected>();
                return Return.returnHttp("200", "DATA STORED SUCCESSFULLY", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion
    }
}
