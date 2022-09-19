using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.WebPages;

namespace MSUISApi.BAL
{
    public class BALProgrammeInstancePartTerm
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        #region Programme Instance Part Term List
        public List<ProgrammeInstancePartTerm> ProgrammeInstancePartTermGet()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("IncProgrammeInstancePartTermGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);
                Int32 count = 0;
                List<ProgrammeInstancePartTerm> ObjLstProgInstPartTerm = new List<ProgrammeInstancePartTerm>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePartTerm ObjProgInstPartTerm = new ProgrammeInstancePartTerm();

                        ObjProgInstPartTerm.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        ObjProgInstPartTerm.FacultyId = (Convert.ToString(Dt.Rows[i]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        ObjProgInstPartTerm.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        ObjProgInstPartTerm.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        ObjProgInstPartTerm.ProgrammeCode = (Convert.ToString(Dt.Rows[i]["ProgrammeCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeCode"]);
                        ObjProgInstPartTerm.ProgrammeInstanceId = (Convert.ToString(Dt.Rows[i]["ProgrammeInstanceId"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["ProgrammeInstanceId"]);
                        ObjProgInstPartTerm.ProgrammeId = (Convert.ToString(Dt.Rows[i]["ProgrammeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);
                        ObjProgInstPartTerm.InstanceName = (Convert.ToString(Dt.Rows[i]["InstanceName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstanceName"]);
                        ObjProgInstPartTerm.SpecialisationId = (Convert.ToString(Dt.Rows[i]["SpecialisationId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["SpecialisationId"]);
                        ObjProgInstPartTerm.BranchName = (Convert.ToString(Dt.Rows[i]["BranchName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["BranchName"]);
                        ObjProgInstPartTerm.PartName = (Convert.ToString(Dt.Rows[i]["PartName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PartName"]);
                        ObjProgInstPartTerm.PartShortName = (Convert.ToString(Dt.Rows[i]["PartShortName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PartShortName"]);
                        ObjProgInstPartTerm.ProgrammePartId = (Convert.ToString(Dt.Rows[i]["ProgrammePartId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammePartId"]);
                        ObjProgInstPartTerm.ProgrammeInstancePartId = (Convert.ToString(Dt.Rows[i]["ProgrammeInstancePartId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeInstancePartId"]);
                        ObjProgInstPartTerm.AcademicYearId = (Convert.ToString(Dt.Rows[i]["AcademicYearId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["AcademicYearId"]);
                        ObjProgInstPartTerm.AcademicYearCode = (Convert.ToString(Dt.Rows[i]["AcademicYearCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);
                        ObjProgInstPartTerm.ProgrammePartTermId = (Convert.ToString(Dt.Rows[i]["ProgrammePartTermId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammePartTermId"]);
                        ObjProgInstPartTerm.ProgrammePartTermName = (Convert.ToString(Dt.Rows[i]["ProgrammePartTermName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammePartTermName"]);
                        ObjProgInstPartTerm.InstancePartTermName = (Convert.ToString(Dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                        ObjProgInstPartTerm.PartTermShortName = (Convert.ToString(Dt.Rows[i]["PartTermShortName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PartTermShortName"]);                        
                        ObjProgInstPartTerm.MaxMarks = (Convert.ToString(Dt.Rows[i]["MaxMarks"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["MaxMarks"]);
                        ObjProgInstPartTerm.MinMarks = (Convert.ToString(Dt.Rows[i]["MinMarks"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["MinMarks"]);
                        ObjProgInstPartTerm.MinPapers = (Convert.ToString(Dt.Rows[i]["MinPapers"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["MinPapers"]);
                        ObjProgInstPartTerm.MaxPapers = (Convert.ToString(Dt.Rows[i]["MaxPapers"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["MaxPapers"]);
                        ObjProgInstPartTerm.IsSeparatePassingHead = (Convert.ToString(Dt.Rows[i]["IsSeparatePassingHead"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsSeparatePassingHead"]);
                        ObjProgInstPartTerm.IsSepratePassHeadSts = (Convert.ToString(Dt.Rows[i]["IsSepratePassHeadSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsSepratePassHeadSts"]);
                        ObjProgInstPartTerm.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjProgInstPartTerm.Islaunch = (Convert.ToString(Dt.Rows[i]["Islaunch"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["Islaunch"]);
                        ObjProgInstPartTerm.IsVerify = (Convert.ToString(Dt.Rows[i]["IsVerify"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsVerify"]);
                        ObjProgInstPartTerm.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        ObjProgInstPartTerm.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);
                        count = count + 1;
                        ObjProgInstPartTerm.IndexId = count;
                        ObjLstProgInstPartTerm.Add(ObjProgInstPartTerm);                        
                    }
                }
                return ObjLstProgInstPartTerm;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Programme Instance Part Term List by fac id and Acad Id
        public List<ProgrammeInstancePartTerm> ProgrammeInstancePartTermGetByFacIdAndAcadId(Int32 FacultyId,Int32 AcadId)
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("IncProgrammeInstancePartTermGetByFacIdAcadId", Con);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                Cmd.Parameters.AddWithValue("@AcadId", AcadId);

                Da.SelectCommand = Cmd;

                Da.Fill(Dt);
                Int32 count = 0;
                List<ProgrammeInstancePartTerm> ObjLstProgInstPartTerm = new List<ProgrammeInstancePartTerm>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePartTerm ObjProgInstPartTerm = new ProgrammeInstancePartTerm();

                        ObjProgInstPartTerm.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        
                        ObjProgInstPartTerm.InstancePartTermName = (Convert.ToString(Dt.Rows[i]["InstancePartTermName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["InstancePartTermName"]);
                        ObjProgInstPartTerm.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        ObjProgInstPartTerm.FacultyCode = (Convert.ToString(Dt.Rows[i]["FacultyCode"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["FacultyCode"]);
                        ObjProgInstPartTerm.StructureReportRemark = (Convert.ToString(Dt.Rows[i]["StructureReportRemark"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["StructureReportRemark"]);
                        ObjProgInstPartTerm.AssessmentReportRemark = (Convert.ToString(Dt.Rows[i]["AssessmentReportRemark"])).IsEmpty() ? "-" : Convert.ToString(Dt.Rows[i]["AssessmentReportRemark"]);
                        ObjProgInstPartTerm.Islaunch = (Convert.ToString(Dt.Rows[i]["Islaunch"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["Islaunch"]);
                        ObjProgInstPartTerm.IsAssessmentLaunched = (Convert.ToString(Dt.Rows[i]["IsAssessmentLaunched"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsAssessmentLaunched"]);
                        ObjProgInstPartTerm.IsApprovedStructureReport = (Convert.ToString(Dt.Rows[i]["IsApprovedStructureReport"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsApprovedStructureReport"]);
                        ObjProgInstPartTerm.IsApprovedAssessmentReport = (Convert.ToString(Dt.Rows[i]["IsApprovedAssessmentReport"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsApprovedAssessmentReport"]);
                        ObjProgInstPartTerm.IsApprovedStructureReportSts = (Convert.ToString(Dt.Rows[i]["IsApprovedStructureReportSts"])).IsEmpty() ? "PENDING" : Convert.ToString(Dt.Rows[i]["IsApprovedStructureReportSts"]);
                        ObjProgInstPartTerm.IsApprovedAssessmentReportSts = (Convert.ToString(Dt.Rows[i]["IsApprovedAssessmentReportSts"])).IsEmpty() ? "PENDING" : Convert.ToString(Dt.Rows[i]["IsApprovedAssessmentReportSts"]);
                        if (Dt.Rows[i]["ApprovedStructureReportOn"].ToString() != null && Dt.Rows[i]["ApprovedStructureReportOn"].ToString() != "")
                        {

                            ObjProgInstPartTerm.ViewApprovedStructureReportOn = Convert.ToDateTime(Dt.Rows[i]["ApprovedStructureReportOn"]).ToShortDateString();
                        }
                        if (Dt.Rows[i]["ApprovedAssessmentReportOn"].ToString() != null && Dt.Rows[i]["ApprovedAssessmentReportOn"].ToString() != "") {
                        
                            ObjProgInstPartTerm.ViewApprovedAssessmentReportOn = Convert.ToDateTime(Dt.Rows[i]["ApprovedAssessmentReportOn"]).ToShortDateString();
                        }
                        count = count + 1;
                        ObjProgInstPartTerm.IndexId = count;
                        ObjLstProgInstPartTerm.Add(ObjProgInstPartTerm);                        
                    }
                }
                return ObjLstProgInstPartTerm;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Programme Instance Part Term List Get By Id
        public ProgrammeInstancePartTerm ProgrammeInstancePartTermListGetbyId(Int64? ProgInstPartTermId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("IncProgrammeInstancePartTermGetById", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", ProgInstPartTermId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if(Dt.Rows.Count > 0)
                {
                    ProgrammeInstancePartTerm ObjProgInstPartTerm = new ProgrammeInstancePartTerm();

                    ObjProgInstPartTerm.Id = Convert.ToInt32(Dt.Rows[0]["Id"]);
                    ObjProgInstPartTerm.FacultyId = (Convert.ToString(Dt.Rows[0]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["FacultyId"]);
                    ObjProgInstPartTerm.FacultyName = (Convert.ToString(Dt.Rows[0]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["FacultyName"]);
                    ObjProgInstPartTerm.ProgrammeName = (Convert.ToString(Dt.Rows[0]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["ProgrammeName"]);
                    ObjProgInstPartTerm.ProgrammeCode = (Convert.ToString(Dt.Rows[0]["ProgrammeCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["ProgrammeCode"]);
                    ObjProgInstPartTerm.ProgrammeInstanceId = (Convert.ToString(Dt.Rows[0]["ProgrammeInstanceId"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[0]["ProgrammeInstanceId"]);
                    ObjProgInstPartTerm.ProgrammeId = (Convert.ToString(Dt.Rows[0]["ProgrammeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["ProgrammeId"]);
                    ObjProgInstPartTerm.InstanceName = (Convert.ToString(Dt.Rows[0]["InstanceName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["InstanceName"]);
                    ObjProgInstPartTerm.SpecialisationId = (Convert.ToString(Dt.Rows[0]["SpecialisationId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["SpecialisationId"]);
                    ObjProgInstPartTerm.BranchName = (Convert.ToString(Dt.Rows[0]["BranchName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["BranchName"]);
                    ObjProgInstPartTerm.PartName = (Convert.ToString(Dt.Rows[0]["PartName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["PartName"]);
                    ObjProgInstPartTerm.PartShortName = (Convert.ToString(Dt.Rows[0]["PartShortName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["PartShortName"]);
                    ObjProgInstPartTerm.ProgrammePartId = (Convert.ToString(Dt.Rows[0]["ProgrammePartId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["ProgrammePartId"]);
                    ObjProgInstPartTerm.ProgrammeInstancePartId = (Convert.ToString(Dt.Rows[0]["ProgrammeInstancePartId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["ProgrammeInstancePartId"]);
                    ObjProgInstPartTerm.AcademicYearId = (Convert.ToString(Dt.Rows[0]["AcademicYearId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["AcademicYearId"]);
                    ObjProgInstPartTerm.AcademicYearCode = (Convert.ToString(Dt.Rows[0]["AcademicYearCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["AcademicYearCode"]);
                    ObjProgInstPartTerm.ProgrammePartTermId = (Convert.ToString(Dt.Rows[0]["ProgrammePartTermId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["ProgrammePartTermId"]);
                    ObjProgInstPartTerm.ProgrammePartTermName = (Convert.ToString(Dt.Rows[0]["ProgrammePartTermName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["ProgrammePartTermName"]);
                    ObjProgInstPartTerm.PartTermShortName = (Convert.ToString(Dt.Rows[0]["PartTermShortName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["PartTermShortName"]);
                    ObjProgInstPartTerm.MaxMarks = (Convert.ToString(Dt.Rows[0]["MaxMarks"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["MaxMarks"]);
                    ObjProgInstPartTerm.MinMarks = (Convert.ToString(Dt.Rows[0]["MinMarks"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["MinMarks"]);
                    ObjProgInstPartTerm.MinPapers = (Convert.ToString(Dt.Rows[0]["MinPapers"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["MinPapers"]);
                    ObjProgInstPartTerm.MaxPapers = (Convert.ToString(Dt.Rows[0]["MaxPapers"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["MaxPapers"]);
                    ObjProgInstPartTerm.IsSeparatePassingHead = (Convert.ToString(Dt.Rows[0]["IsSeparatePassingHead"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsSeparatePassingHead"]);
                    ObjProgInstPartTerm.IsSepratePassHeadSts = (Convert.ToString(Dt.Rows[0]["IsSepratePassHeadSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["IsSepratePassHeadSts"]);
                    ObjProgInstPartTerm.IsActive = (Convert.ToString(Dt.Rows[0]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsActive"]);
                    ObjProgInstPartTerm.IsActiveSts = (Convert.ToString(Dt.Rows[0]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["IsActiveSts"]);
                    ObjProgInstPartTerm.IsDeleted = (Convert.ToString(Dt.Rows[0]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsDeleted"]);

                    return ObjProgInstPartTerm;

                }
                else
                    return null;

            }
            catch (Exception e)
            {
                return null;
            }

        }
        #endregion
    }
}