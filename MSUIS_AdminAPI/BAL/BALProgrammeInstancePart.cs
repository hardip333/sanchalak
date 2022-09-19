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
    public class BALProgrammeInstancePart
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        #region Programme Instance Part List
        public List<ProgrammeInstancePart> ProgrammeInstancePartGet()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("IncProgrammeInstancePartGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<ProgrammeInstancePart> ObjLstProgInstPart = new List<ProgrammeInstancePart>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePart ObjProgInstPart = new ProgrammeInstancePart();

                        ObjProgInstPart.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        ObjProgInstPart.FacultyId = (Convert.ToString(Dt.Rows[i]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        ObjProgInstPart.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        ObjProgInstPart.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        ObjProgInstPart.ProgrammeInstanceId = (Convert.ToString(Dt.Rows[i]["ProgrammeInstanceId"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["ProgrammeInstanceId"]);
                        ObjProgInstPart.ProgrammeId = (Convert.ToString(Dt.Rows[i]["ProgrammeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);
                        ObjProgInstPart.InstanceName = (Convert.ToString(Dt.Rows[i]["InstanceName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstanceName"]);
                        ObjProgInstPart.ProgrammePartId = (Convert.ToString(Dt.Rows[i]["PorgrammePartId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["PorgrammePartId"]);
                        ObjProgInstPart.AcademicYearId = (Convert.ToString(Dt.Rows[i]["AcademicYearId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["AcademicYearId"]);
                        ObjProgInstPart.AcademicYearCode = (Convert.ToString(Dt.Rows[i]["AcademicYearCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);
                        ObjProgInstPart.ProgrammePartName = (Convert.ToString(Dt.Rows[i]["PartName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PartName"]);
                        ObjProgInstPart.PartShortName = (Convert.ToString(Dt.Rows[i]["PartShortName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PartShortName"]);
                        ObjProgInstPart.SequenceNo = (Convert.ToString(Dt.Rows[i]["SequenceNo"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["SequenceNo"]);
                        ObjProgInstPart.ExamPatternId = (Convert.ToString(Dt.Rows[i]["ExamPatternId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ExamPatternId"]);
                        ObjProgInstPart.ExamPatternName = (Convert.ToString(Dt.Rows[i]["ExaminationPatternName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ExaminationPatternName"]);
                        ObjProgInstPart.MaxMarks = (Convert.ToString(Dt.Rows[i]["MaxMarks"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["MaxMarks"]);
                        ObjProgInstPart.MinMarks = (Convert.ToString(Dt.Rows[i]["MinMarks"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["MinMarks"]);
                        ObjProgInstPart.IsSeparatePassingHead = (Convert.ToString(Dt.Rows[i]["IsSeparatePassingHead"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsSeparatePassingHead"]);
                        ObjProgInstPart.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjProgInstPart.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                       // ObjProgInstPart.IsSeparatePassingHeadSts = (Convert.ToString(Dt.Rows[i]["IsSeparatePassingHeadSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsSeparatePassingHeadSts"]);
                        ObjProgInstPart.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);

                        ObjLstProgInstPart.Add(ObjProgInstPart);                        
                    }
                }
                return ObjLstProgInstPart;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Programme Instance Part List Get By Id
        public ProgrammeInstancePart ProgrammeInstancePartListGetbyId(Int64? ProgInstPartId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("IncProgrammeInstancePartGetById", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PartId", ProgInstPartId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if(Dt.Rows.Count > 0)
                {
                    ProgrammeInstancePart ObjProgInstPart = new ProgrammeInstancePart();

                    ObjProgInstPart.Id = Convert.ToInt32(Dt.Rows[0]["Id"]);
                    ObjProgInstPart.FacultyId = (Convert.ToString(Dt.Rows[0]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["FacultyId"]);
                    ObjProgInstPart.FacultyName = (Convert.ToString(Dt.Rows[0]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["FacultyName"]);
                    ObjProgInstPart.ProgrammeName = (Convert.ToString(Dt.Rows[0]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["ProgrammeName"]);
                    ObjProgInstPart.ProgrammeInstanceId = (Convert.ToString(Dt.Rows[0]["ProgrammeInstanceId"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[0]["ProgrammeInstanceId"]);
                    ObjProgInstPart.ProgrammeId = (Convert.ToString(Dt.Rows[0]["ProgrammeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["ProgrammeId"]);
                    ObjProgInstPart.InstanceName = (Convert.ToString(Dt.Rows[0]["InstanceName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["InstanceName"]);
                    ObjProgInstPart.ProgrammePartId = (Convert.ToString(Dt.Rows[0]["PorgrammePartId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["PorgrammePartId"]);
                    ObjProgInstPart.AcademicYearId = (Convert.ToString(Dt.Rows[0]["AcademicYearId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["AcademicYearId"]);
                    ObjProgInstPart.AcademicYearCode = (Convert.ToString(Dt.Rows[0]["AcademicYearCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["AcademicYearCode"]);
                    ObjProgInstPart.ProgrammePartName = (Convert.ToString(Dt.Rows[0]["PartName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["PartName"]);
                    ObjProgInstPart.PartShortName = (Convert.ToString(Dt.Rows[0]["PartShortName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["PartShortName"]);
                    ObjProgInstPart.SequenceNo = (Convert.ToString(Dt.Rows[0]["SequenceNo"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["SequenceNo"]);
                    ObjProgInstPart.ExamPatternId = (Convert.ToString(Dt.Rows[0]["ExamPatternId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["ExamPatternId"]);
                    ObjProgInstPart.ExamPatternName = (Convert.ToString(Dt.Rows[0]["ExaminationPatternName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["ExaminationPatternName"]);
                    ObjProgInstPart.MaxMarks = (Convert.ToString(Dt.Rows[0]["MaxMarks"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["MaxMarks"]);
                    ObjProgInstPart.MinMarks = (Convert.ToString(Dt.Rows[0]["MinMarks"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["MinMarks"]);
                    ObjProgInstPart.IsSeparatePassingHead = (Convert.ToString(Dt.Rows[0]["IsSeparatePassingHead"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsSeparatePassingHead"]);
                    ObjProgInstPart.IsActive = (Convert.ToString(Dt.Rows[0]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsActive"]);
                    ObjProgInstPart.IsActiveSts = (Convert.ToString(Dt.Rows[0]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["IsActiveSts"]);
                    //ObjProgInstPart.IsSeparatePassingHeadSts = (Convert.ToString(Dt.Rows[0]["IsSeparatePassingHeadSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["IsSeparatePassingHeadSts"]);
                    ObjProgInstPart.IsDeleted = (Convert.ToString(Dt.Rows[0]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsDeleted"]);

                    return ObjProgInstPart;

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