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
    public class BALProgrammePart
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        #region Programme Part List
        public List<MstProgrammePart> ProgrammePartGet()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstProgrammePartGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);
                Int32 count = 0;
                List<MstProgrammePart> ObjLstProgPart = new List<MstProgrammePart>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgrammePart objProgPart = new MstProgrammePart();

                        objProgPart.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objProgPart.ProgrammeId = (Convert.ToString(Dt.Rows[i]["ProgrammeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);
                        objProgPart.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        objProgPart.ExamPatternId = (Convert.ToString(Dt.Rows[i]["ExamPatternId"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["ExamPatternId"]);
                        objProgPart.ExaminationPatternName = (Convert.ToString(Dt.Rows[i]["ExaminationPatternName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ExaminationPatternName"]);
                        objProgPart.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objProgPart.NoOfTerms = (Convert.ToString(Dt.Rows[i]["NoOfTerms"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["NoOfTerms"]);
                        objProgPart.PartName = (Convert.ToString(Dt.Rows[i]["PartName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PartName"]);
                        objProgPart.PartShortName = (Convert.ToString(Dt.Rows[i]["PartShortName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PartShortName"]);
                        objProgPart.SequenceNo = (Convert.ToString(Dt.Rows[i]["SequenceNo"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["SequenceNo"]);
                        objProgPart.FacultyId = (Convert.ToString(Dt.Rows[i]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        objProgPart.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        count = count + 1;
                        objProgPart.IndexId = count;
                        ObjLstProgPart.Add(objProgPart);                        
                    }
                }
                return ObjLstProgPart;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Programme Part List Get By Id
        public MstProgrammePart ProgrammePartListGetbyId(int? ProgPartId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MstProgrammePartGetbyId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", ProgPartId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if(Dt.Rows.Count > 0)
                {
                    MstProgrammePart objProgPart = new MstProgrammePart();

                    objProgPart.Id = Convert.ToInt32(Dt.Rows[0]["Id"]);
                    objProgPart.ProgrammeId = (Convert.ToString(Dt.Rows[0]["ProgrammeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["ProgrammeId"]);
                    objProgPart.ProgrammeName = (Convert.ToString(Dt.Rows[0]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["ProgrammeName"]);
                    objProgPart.ExamPatternId = (Convert.ToString(Dt.Rows[0]["ExamPatternId"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[0]["ExamPatternId"]);
                    objProgPart.ExaminationPatternName = (Convert.ToString(Dt.Rows[0]["ExaminationPatternName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["ExaminationPatternName"]);
                    objProgPart.FacultyName = (Convert.ToString(Dt.Rows[0]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["FacultyName"]);
                    objProgPart.NoOfTerms = (Convert.ToString(Dt.Rows[0]["NoOfTerms"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[0]["NoOfTerms"]);
                    objProgPart.PartName = (Convert.ToString(Dt.Rows[0]["PartName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["PartName"]);
                    objProgPart.PartShortName = (Convert.ToString(Dt.Rows[0]["PartShortName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["PartShortName"]);
                    objProgPart.SequenceNo = (Convert.ToString(Dt.Rows[0]["SequenceNo"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[0]["SequenceNo"]);
                    objProgPart.FacultyId = (Convert.ToString(Dt.Rows[0]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["FacultyId"]);
                    objProgPart.IsActive = (Convert.ToString(Dt.Rows[0]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsActive"]);

                    return objProgPart;

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

        #region Programme Part List Get By ProgrammeId
        public List<MstProgrammePart> ProgrammePartGetByProgrammeId(int? progId)
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstProgrammePartGetByProgrammeId", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@ProgrammeId",progId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<MstProgrammePart> ObjLstProgPart = new List<MstProgrammePart>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgrammePart objProgPart = new MstProgrammePart();

                        objProgPart.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objProgPart.ProgrammeId = (Convert.ToString(Dt.Rows[i]["ProgrammeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);
                        objProgPart.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        objProgPart.ExamPatternId = (Convert.ToString(Dt.Rows[i]["ExamPatternId"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["ExamPatternId"]);
                        objProgPart.ExaminationPatternName = (Convert.ToString(Dt.Rows[i]["ExaminationPatternName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ExaminationPatternName"]);
                        objProgPart.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objProgPart.NoOfTerms = (Convert.ToString(Dt.Rows[i]["NoOfTerms"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["NoOfTerms"]);
                        objProgPart.PartName = (Convert.ToString(Dt.Rows[i]["PartName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PartName"]);
                        objProgPart.PartShortName = (Convert.ToString(Dt.Rows[i]["PartShortName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PartShortName"]);
                        objProgPart.SequenceNo = (Convert.ToString(Dt.Rows[i]["SequenceNo"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["SequenceNo"]);
                        objProgPart.FacultyId = (Convert.ToString(Dt.Rows[i]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        objProgPart.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjLstProgPart.Add(objProgPart);
                    }
                }
                return ObjLstProgPart;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Programme Part List Get By ProgInstId
        public List<ProgrammeInstancePart> ProgrammePartGetByProgInstId(Int64? progInstId)
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstProgrammePartGetByProgInstId", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@ProgrammeInstanceId", progInstId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<ProgrammeInstancePart> ObjLstProgInstPart = new List<ProgrammeInstancePart>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ProgrammeInstancePart objProgInstPart = new ProgrammeInstancePart();

                        objProgInstPart.Id = Convert.ToInt32(Dt.Rows[i]["ProgrammeInstancePartId"]);
                        objProgInstPart.FacultyId = (Convert.ToString(Dt.Rows[i]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        objProgInstPart.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objProgInstPart.ProgrammeId = (Convert.ToString(Dt.Rows[i]["ProgrammeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);
                        objProgInstPart.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        objProgInstPart.ProgrammePartId = (Convert.ToString(Dt.Rows[i]["ProgrammePartId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammePartId"]);
                        objProgInstPart.ProgrammeInstanceId = (Convert.ToString(Dt.Rows[i]["ProgrammeInstanceId"])).IsEmpty() ? 0 : Convert.ToInt64(Dt.Rows[i]["ProgrammeInstanceId"]);
                        objProgInstPart.PartShortName = (Convert.ToString(Dt.Rows[i]["PartShortName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PartShortName"]);                        
                        ObjLstProgInstPart.Add(objProgInstPart);
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
    }
}