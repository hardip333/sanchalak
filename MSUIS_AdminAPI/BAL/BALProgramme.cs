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
    public class BALProgramme
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        #region Programme List
        public List<MstProgramme> ProgrammeGet()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstProgrammeGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);
                Int32 count = 0;
                List<MstProgramme> ObjLstProg = new List<MstProgramme>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgramme objProg = new MstProgramme();

                        objProg.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objProg.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        objProg.ProgrammeCode = (Convert.ToString(Dt.Rows[i]["ProgrammeCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeCode"]);
                        objProg.ProgrammeDescription = ((Convert.ToString(Dt.Rows[i]["ProgrammeDescription"])).IsEmpty() ? "" : (Convert.ToString(Dt.Rows[i]["ProgrammeDescription"])));
                        objProg.FacultyId = (((Dt.Rows[i]["FacultyId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        objProg.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objProg.ProgrammeLevelId = (Convert.ToString(Dt.Rows[i]["ProgrammeLevelId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeLevelId"]);
                        objProg.ProgrammeLevelName = (Convert.ToString(Dt.Rows[i]["ProgrammeLevelName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeLevelName"]);
                        objProg.ProgrammeTypeId = (Convert.ToString(Dt.Rows[i]["ProgrammeTypeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeTypeId"]);
                        objProg.ProgrammeTypeName = (Convert.ToString(Dt.Rows[i]["ProgrammeTypeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeTypeName"]);
                        objProg.ProgrammeModeId = (Convert.ToString(Dt.Rows[i]["ProgrammeModeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeModeId"]);
                        objProg.ProgrammeModeName = (Convert.ToString(Dt.Rows[i]["ProgrammeModeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeModeName"]);
                        objProg.EvaluationId = (Convert.ToString(Dt.Rows[i]["EvaluationId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["EvaluationId"]);
                        objProg.EvaluationName = (Convert.ToString(Dt.Rows[i]["EvaluationName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["EvaluationName"]);
                        objProg.InstructionMediumId = (Convert.ToString(Dt.Rows[i]["InstructionMediumId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["InstructionMediumId"]);
                        objProg.InstructionMediumName = (Convert.ToString(Dt.Rows[i]["InstructionMediumName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstructionMediumName"]);
                        objProg.IsCBCS = (Convert.ToString(Dt.Rows[i]["IsCBCS"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsCBCS"]);
                        objProg.IsSepartePassingHead = (Convert.ToString(Dt.Rows[i]["IsSepartePassingHead"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsSepartePassingHead"]);
                        objProg.MaxMarks = (Convert.ToString(Dt.Rows[i]["MaxMarks"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["MaxMarks"]);
                        objProg.MinMarks = (Convert.ToString(Dt.Rows[i]["MinMarks"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["MinMarks"]);
                        objProg.MaxCredits = (Convert.ToString(Dt.Rows[i]["MaxCredits"])).IsEmpty() ? 0 : Convert.ToDecimal(Dt.Rows[i]["MaxCredits"]);
                        objProg.MinCredits = (Convert.ToString(Dt.Rows[i]["MinCredits"])).IsEmpty() ? 0 : Convert.ToDecimal(Dt.Rows[i]["MinCredits"]);
                        objProg.ProgrammeDuration = (Convert.ToString(Dt.Rows[i]["ProgrammeDuration"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeDuration"]);
                        objProg.ProgrammeValidity = (Convert.ToString(Dt.Rows[i]["ProgrammeValidity"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeValidity"]);
                        objProg.TotalParts = (Convert.ToString(Dt.Rows[i]["TotalParts"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["TotalParts"]);
                        objProg.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        objProg.IsSeparatePassingHeadSts = (Convert.ToString(Dt.Rows[i]["IsSepPassHeadSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsSepPassHeadSts"]);
                        objProg.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objProg.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);
                        count = count + 1;
                        objProg.IndexId = count;
                        ObjLstProg.Add(objProg);
                    }
                }
                return ObjLstProg;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Programme List For Drop Down
        public List<MstProgramme> ProgrammeGetForDropDown()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstProgrammeGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<MstProgramme> ObjLstProg = new List<MstProgramme>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgramme objProg = new MstProgramme();

                        objProg.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objProg.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        objProg.ProgrammeCode = (Convert.ToString(Dt.Rows[i]["ProgrammeCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeCode"]);
                        ObjLstProg.Add(objProg);
                    }
                }
                return ObjLstProg;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Programme List Get By Id
        public MstProgramme ProgrammeListGetbyId(int? ProgId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MstProgrammeGetbyId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeId", ProgId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    MstProgramme objProg = new MstProgramme();

                    objProg.Id = Convert.ToInt32(Dt.Rows[0]["Id"]);
                    objProg.ProgrammeName = (Convert.ToString(Dt.Rows[0]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["ProgrammeName"]);
                    objProg.ProgrammeCode = (Convert.ToString(Dt.Rows[0]["ProgrammeCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["ProgrammeCode"]);
                    objProg.ProgrammeDescription = ((Convert.ToString(Dt.Rows[0]["ProgrammeDescription"])).IsEmpty() ? "" : (Convert.ToString(Dt.Rows[0]["ProgrammeDescription"])));
                    objProg.FacultyId = (((Dt.Rows[0]["FacultyId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[0]["FacultyId"]);
                    objProg.FacultyName = (Convert.ToString(Dt.Rows[0]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["FacultyName"]);
                    objProg.ProgrammeLevelId = (Convert.ToString(Dt.Rows[0]["ProgrammeLevelId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["ProgrammeLevelId"]);
                    objProg.ProgrammeLevelName = (Convert.ToString(Dt.Rows[0]["ProgrammeLevelName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["ProgrammeLevelName"]);
                    objProg.ProgrammeTypeId = (Convert.ToString(Dt.Rows[0]["ProgrammeTypeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["ProgrammeTypeId"]);
                    objProg.ProgrammeTypeName = (Convert.ToString(Dt.Rows[0]["ProgrammeTypeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["ProgrammeTypeName"]);
                    objProg.ProgrammeModeId = (Convert.ToString(Dt.Rows[0]["ProgrammeModeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["ProgrammeModeId"]);
                    objProg.ProgrammeModeName = (Convert.ToString(Dt.Rows[0]["ProgrammeModeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["ProgrammeModeName"]);
                    objProg.EvaluationId = (Convert.ToString(Dt.Rows[0]["EvaluationId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["EvaluationId"]);
                    objProg.EvaluationName = (Convert.ToString(Dt.Rows[0]["EvaluationName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["EvaluationName"]);
                    objProg.InstructionMediumId = (Convert.ToString(Dt.Rows[0]["InstructionMediumId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["InstructionMediumId"]);
                    objProg.InstructionMediumName = (Convert.ToString(Dt.Rows[0]["InstructionMediumName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["InstructionMediumName"]);
                    objProg.IsCBCS = (Convert.ToString(Dt.Rows[0]["IsCBCS"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsCBCS"]);
                    objProg.IsSepartePassingHead = (Convert.ToString(Dt.Rows[0]["IsSepartePassingHead"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsSepartePassingHead"]);
                    objProg.MaxMarks = (Convert.ToString(Dt.Rows[0]["MaxMarks"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["MaxMarks"]);
                    objProg.MinMarks = (Convert.ToString(Dt.Rows[0]["MinMarks"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["MinMarks"]);
                    objProg.MaxCredits = (Convert.ToString(Dt.Rows[0]["MaxCredits"])).IsEmpty() ? 0 : Convert.ToDecimal(Dt.Rows[0]["MaxCredits"]);
                    objProg.MinCredits = (Convert.ToString(Dt.Rows[0]["MinCredits"])).IsEmpty() ? 0 : Convert.ToDecimal(Dt.Rows[0]["MinCredits"]);
                    objProg.ProgrammeDuration = (Convert.ToString(Dt.Rows[0]["ProgrammeDuration"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["ProgrammeDuration"]);
                    objProg.ProgrammeValidity = (Convert.ToString(Dt.Rows[0]["ProgrammeValidity"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["ProgrammeValidity"]);
                    objProg.TotalParts = (Convert.ToString(Dt.Rows[0]["TotalParts"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["TotalParts"]);
                    objProg.IsActiveSts = (Convert.ToString(Dt.Rows[0]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["IsActiveSts"]);
                    objProg.IsSeparatePassingHeadSts = (Convert.ToString(Dt.Rows[0]["IsSepPassHeadSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["IsSepPassHeadSts"]);
                    objProg.IsActive = (Convert.ToString(Dt.Rows[0]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsActive"]);
                    objProg.IsDeleted = (Convert.ToString(Dt.Rows[0]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsDeleted"]);

                    return objProg;

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

        #region Programme List Get By FacultyId
        public List<MstProgramme> ProgrammeGetByFacultyId(int? FacultyId)
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstProgrammeGetByFacultyId", Con);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@FacultyId", FacultyId);

                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<MstProgramme> ObjLstProg = new List<MstProgramme>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgramme objProg = new MstProgramme();

                        objProg.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objProg.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        objProg.ProgrammeCode = (Convert.ToString(Dt.Rows[i]["ProgrammeCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeCode"]);
                        objProg.ProgrammeDescription = ((Convert.ToString(Dt.Rows[i]["ProgrammeDescription"])).IsEmpty() ? "" : (Convert.ToString(Dt.Rows[i]["ProgrammeDescription"])));
                        //objProg.FacultyId = (((Dt.Rows[i]["FacultyId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        //objProg.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objProg.ProgrammeLevelId = (Convert.ToString(Dt.Rows[i]["ProgrammeLevelId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeLevelId"]);
                        objProg.ProgrammeLevelName = (Convert.ToString(Dt.Rows[i]["ProgrammeLevelName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeLevelName"]);
                        objProg.ProgrammeTypeId = (Convert.ToString(Dt.Rows[i]["ProgrammeTypeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeTypeId"]);
                        objProg.ProgrammeTypeName = (Convert.ToString(Dt.Rows[i]["ProgrammeTypeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeTypeName"]);
                        objProg.ProgrammeModeId = (Convert.ToString(Dt.Rows[i]["ProgrammeModeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeModeId"]);
                        objProg.ProgrammeModeName = (Convert.ToString(Dt.Rows[i]["ProgrammeModeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeModeName"]);
                        objProg.EvaluationId = (Convert.ToString(Dt.Rows[i]["EvaluationId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["EvaluationId"]);
                        objProg.EvaluationName = (Convert.ToString(Dt.Rows[i]["EvaluationName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["EvaluationName"]);
                        objProg.InstructionMediumId = (Convert.ToString(Dt.Rows[i]["InstructionMediumId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["InstructionMediumId"]);
                        objProg.InstructionMediumName = (Convert.ToString(Dt.Rows[i]["InstructionMediumName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstructionMediumName"]);
                        objProg.IsCBCS = (Convert.ToString(Dt.Rows[i]["IsCBCS"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsCBCS"]);
                        objProg.IsSepartePassingHead = (Convert.ToString(Dt.Rows[i]["IsSepartePassingHead"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsSepartePassingHead"]);
                        objProg.MaxMarks = (Convert.ToString(Dt.Rows[i]["MaxMarks"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["MaxMarks"]);
                        objProg.MinMarks = (Convert.ToString(Dt.Rows[i]["MinMarks"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["MinMarks"]);
                        objProg.MaxCredits = (Convert.ToString(Dt.Rows[i]["MaxCredits"])).IsEmpty() ? 0 : Convert.ToDecimal(Dt.Rows[i]["MaxCredits"]);
                        objProg.MinCredits = (Convert.ToString(Dt.Rows[i]["MinCredits"])).IsEmpty() ? 0 : Convert.ToDecimal(Dt.Rows[i]["MinCredits"]);
                        objProg.ProgrammeDuration = (Convert.ToString(Dt.Rows[i]["ProgrammeDuration"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeDuration"]);
                        objProg.ProgrammeValidity = (Convert.ToString(Dt.Rows[i]["ProgrammeValidity"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeValidity"]);
                        objProg.TotalParts = (Convert.ToString(Dt.Rows[i]["TotalParts"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["TotalParts"]);
                        objProg.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        objProg.IsSeparatePassingHeadSts = (Convert.ToString(Dt.Rows[i]["IsSepPassHeadSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsSepPassHeadSts"]);
                        objProg.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objProg.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);
                        ObjLstProg.Add(objProg);
                    }
                }
                return ObjLstProg;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Programme List Get By FacId
        public List<MstProgramme> ProgrammeGetByFacId(int? FacultyId)
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstProgrammeGetByFacId", Con);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@FacultyId", FacultyId);

                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<MstProgramme> ObjLstProg = new List<MstProgramme>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgramme objProg = new MstProgramme();

                        objProg.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objProg.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        objProg.ProgrammeCode = (Convert.ToString(Dt.Rows[i]["ProgrammeCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeCode"]);
                        objProg.ProgrammeDescription = ((Convert.ToString(Dt.Rows[i]["ProgrammeDescription"])).IsEmpty() ? "" : (Convert.ToString(Dt.Rows[i]["ProgrammeDescription"])));
                        objProg.FacultyId = (((Dt.Rows[i]["FacultyId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        objProg.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objProg.ProgrammeLevelId = (Convert.ToString(Dt.Rows[i]["ProgrammeLevelId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeLevelId"]);
                        objProg.ProgrammeLevelName = (Convert.ToString(Dt.Rows[i]["ProgrammeLevelName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeLevelName"]);
                        objProg.ProgrammeTypeId = (Convert.ToString(Dt.Rows[i]["ProgrammeTypeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeTypeId"]);
                        objProg.ProgrammeTypeName = (Convert.ToString(Dt.Rows[i]["ProgrammeTypeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeTypeName"]);
                        objProg.ProgrammeModeId = (Convert.ToString(Dt.Rows[i]["ProgrammeModeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeModeId"]);
                        objProg.ProgrammeModeName = (Convert.ToString(Dt.Rows[i]["ProgrammeModeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeModeName"]);
                        objProg.EvaluationId = (Convert.ToString(Dt.Rows[i]["EvaluationId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["EvaluationId"]);
                        objProg.EvaluationName = (Convert.ToString(Dt.Rows[i]["EvaluationName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["EvaluationName"]);
                        objProg.InstructionMediumId = (Convert.ToString(Dt.Rows[i]["InstructionMediumId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["InstructionMediumId"]);
                        objProg.InstructionMediumName = (Convert.ToString(Dt.Rows[i]["InstructionMediumName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstructionMediumName"]);
                        objProg.IsCBCS = (Convert.ToString(Dt.Rows[i]["IsCBCS"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsCBCS"]);
                        objProg.IsSepartePassingHead = (Convert.ToString(Dt.Rows[i]["IsSepartePassingHead"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsSepartePassingHead"]);
                        objProg.MaxMarks = (Convert.ToString(Dt.Rows[i]["MaxMarks"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["MaxMarks"]);
                        objProg.MinMarks = (Convert.ToString(Dt.Rows[i]["MinMarks"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["MinMarks"]);
                        objProg.MaxCredits = (Convert.ToString(Dt.Rows[i]["MaxCredits"])).IsEmpty() ? 0 : Convert.ToDecimal(Dt.Rows[i]["MaxCredits"]);
                        objProg.MinCredits = (Convert.ToString(Dt.Rows[i]["MinCredits"])).IsEmpty() ? 0 : Convert.ToDecimal(Dt.Rows[i]["MinCredits"]);
                        objProg.ProgrammeDuration = (Convert.ToString(Dt.Rows[i]["ProgrammeDuration"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeDuration"]);
                        objProg.ProgrammeValidity = (Convert.ToString(Dt.Rows[i]["ProgrammeValidity"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeValidity"]);
                        objProg.TotalParts = (Convert.ToString(Dt.Rows[i]["TotalParts"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["TotalParts"]);
                        objProg.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        objProg.IsSeparatePassingHeadSts = (Convert.ToString(Dt.Rows[i]["IsSepPassHeadSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsSepPassHeadSts"]);
                        objProg.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objProg.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);
                        ObjLstProg.Add(objProg);
                    }
                }
                return ObjLstProg;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Programme List Get By FacId
        public List<MstProgramme> ProgrammeGetByFacIdForSchedule(int? FacultyId)
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstProgrammeGetByFacIdForSchedule", Con);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@FacultyId", FacultyId);

                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<MstProgramme> ObjLstProg = new List<MstProgramme>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgramme objProg = new MstProgramme();

                        objProg.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objProg.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        
                        ObjLstProg.Add(objProg);
                    }
                }
                return ObjLstProg;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region New Programme List Get By FacultyId
        public List<MstProgramme> NewMstProgrammeGetByFacultyId(int? FacultyId)
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("NewMstProgrammeGetByFacultyId", Con);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@FacultyId", FacultyId);

                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<MstProgramme> ObjLstProg = new List<MstProgramme>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgramme objProg = new MstProgramme();

                        objProg.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objProg.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        objProg.ProgrammeCode = (Convert.ToString(Dt.Rows[i]["ProgrammeCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeCode"]);
                        objProg.ProgrammeDescription = ((Convert.ToString(Dt.Rows[i]["ProgrammeDescription"])).IsEmpty() ? "" : (Convert.ToString(Dt.Rows[i]["ProgrammeDescription"])));
                        objProg.FacultyId = (((Dt.Rows[i]["FacultyId"]).ToString()).IsEmpty()) ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        objProg.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objProg.ProgrammeLevelId = (Convert.ToString(Dt.Rows[i]["ProgrammeLevelId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeLevelId"]);
                        objProg.ProgrammeLevelName = (Convert.ToString(Dt.Rows[i]["ProgrammeLevelName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeLevelName"]);
                        objProg.ProgrammeTypeId = (Convert.ToString(Dt.Rows[i]["ProgrammeTypeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeTypeId"]);
                        objProg.ProgrammeTypeName = (Convert.ToString(Dt.Rows[i]["ProgrammeTypeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeTypeName"]);
                        objProg.ProgrammeModeId = (Convert.ToString(Dt.Rows[i]["ProgrammeModeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeModeId"]);
                        objProg.ProgrammeModeName = (Convert.ToString(Dt.Rows[i]["ProgrammeModeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeModeName"]);
                        objProg.EvaluationId = (Convert.ToString(Dt.Rows[i]["EvaluationId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["EvaluationId"]);
                        objProg.EvaluationName = (Convert.ToString(Dt.Rows[i]["EvaluationName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["EvaluationName"]);
                        objProg.InstructionMediumId = (Convert.ToString(Dt.Rows[i]["InstructionMediumId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["InstructionMediumId"]);
                        objProg.InstructionMediumName = (Convert.ToString(Dt.Rows[i]["InstructionMediumName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstructionMediumName"]);
                        objProg.IsCBCS = (Convert.ToString(Dt.Rows[i]["IsCBCS"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsCBCS"]);
                        objProg.IsSepartePassingHead = (Convert.ToString(Dt.Rows[i]["IsSepartePassingHead"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsSepartePassingHead"]);
                        objProg.MaxMarks = (Convert.ToString(Dt.Rows[i]["MaxMarks"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["MaxMarks"]);
                        objProg.MinMarks = (Convert.ToString(Dt.Rows[i]["MinMarks"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["MinMarks"]);
                        objProg.MaxCredits = (Convert.ToString(Dt.Rows[i]["MaxCredits"])).IsEmpty() ? 0 : Convert.ToDecimal(Dt.Rows[i]["MaxCredits"]);
                        objProg.MinCredits = (Convert.ToString(Dt.Rows[i]["MinCredits"])).IsEmpty() ? 0 : Convert.ToDecimal(Dt.Rows[i]["MinCredits"]);
                        objProg.ProgrammeDuration = (Convert.ToString(Dt.Rows[i]["ProgrammeDuration"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeDuration"]);
                        objProg.ProgrammeValidity = (Convert.ToString(Dt.Rows[i]["ProgrammeValidity"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeValidity"]);
                        objProg.TotalParts = (Convert.ToString(Dt.Rows[i]["TotalParts"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["TotalParts"]);
                        objProg.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        objProg.IsSeparatePassingHeadSts = (Convert.ToString(Dt.Rows[i]["IsSepPassHeadSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsSepPassHeadSts"]);
                        objProg.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objProg.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);
                        ObjLstProg.Add(objProg);
                    }
                }
                return ObjLstProg;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Programme List Get By InstituteId
        public List<MstProgramme> ProgrammeGetByInstituteId(int? InstituteId)
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstProgrammeGetByInstituteId", Con);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@InstituteId", InstituteId);

                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<MstProgramme> ObjLstProg = new List<MstProgramme>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgramme objProg = new MstProgramme();

                        objProg.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objProg.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        objProg.ProgrammeCode = (Convert.ToString(Dt.Rows[i]["ProgrammeCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeCode"]);
                        objProg.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        objProg.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objProg.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);
                        ObjLstProg.Add(objProg);
                    }
                }
                return ObjLstProg;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Programme List Get By Faculty Exam Id
        public List<MstProgramme> ProgrammeGetByFacultyExamId(int? FacultyExamId)
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstProgrammeGetByFacultyExamId", Con);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@FacultyExamId", FacultyExamId);

                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<MstProgramme> ObjLstProg = new List<MstProgramme>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgramme objProg = new MstProgramme();

                        objProg.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objProg.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        objProg.ProgrammeCode = (Convert.ToString(Dt.Rows[i]["ProgrammeCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeCode"]);
                        objProg.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        objProg.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objProg.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);
                        ObjLstProg.Add(objProg);
                    }
                }
                return ObjLstProg;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Megha Code
        // Create Dropdown API
        public List<MstProgramme> getProgrammeListByFacultyExamMapId(int? FacultyExamMapId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("getProgrammeListByFacultyExamMapId", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                // Cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                Cmd.Parameters.AddWithValue("@FacultyExamMapId", FacultyExamMapId);
                Da.SelectCommand = Cmd;

                Da.Fill(dt);
                if (dt == null || dt.Rows.Count == 0)
                {
                    return null;
                }
                int n = 0, count = dt.Rows.Count;
                List<MstProgramme> programmeList = new List<MstProgramme>();
                while (n < count)
                {
                    MstProgramme element = new MstProgramme();
                    element.Id = Convert.ToInt32(dt.Rows[n][0].ToString());
                    element.ProgrammeName = dt.Rows[n][1].ToString();
                    element.ProgrammeCode = dt.Rows[n][2].ToString();
                    if (!string.IsNullOrEmpty(dt.Rows[n][3].ToString()))
                    {
                        element.FacultyId = Convert.ToInt32(dt.Rows[n][3].ToString());
                    }
                    element.ProgrammeDescription = dt.Rows[n][4].ToString();
                    if (!string.IsNullOrEmpty(dt.Rows[n][5].ToString()))
                    {
                        element.ProgrammeLevelId = Convert.ToInt32(dt.Rows[n][5].ToString());
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[n][6].ToString()))
                    {
                        element.ProgrammeModeId = Convert.ToInt32(dt.Rows[n][6].ToString());
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[n][7].ToString()))
                    {
                        element.ProgrammeTypeId = Convert.ToInt32(dt.Rows[n][7].ToString());
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[n][8].ToString()))
                    {
                        element.InstructionMediumId = Convert.ToInt32(dt.Rows[n][8].ToString());
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[n][9].ToString()))
                    {
                        element.EvaluationId = Convert.ToInt32(dt.Rows[n][9].ToString());
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[n][10].ToString()))
                    {
                        element.IsCBCS = Convert.ToBoolean(dt.Rows[n][10].ToString());
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[n][11].ToString()))
                    {
                        element.IsSepartePassingHead = Convert.ToBoolean(dt.Rows[n][11].ToString());
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[n][12].ToString()))
                    {
                        element.MaxMarks = Convert.ToInt32(dt.Rows[n][12].ToString());
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[n][13].ToString()))
                    {
                        element.MinMarks = Convert.ToInt32(dt.Rows[n][13].ToString());
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[n][14].ToString()))
                    {
                        element.MaxCredits = Convert.ToDecimal(dt.Rows[n][14].ToString());
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[n][15].ToString()))
                    {
                        element.MinCredits = Convert.ToDecimal(dt.Rows[n][15].ToString());
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[n][16].ToString()))
                    {
                        element.ProgrammeDuration = Convert.ToInt32(dt.Rows[n][16].ToString());
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[n][17].ToString()))
                    {
                        element.ProgrammeValidity = Convert.ToInt32(dt.Rows[n][17].ToString());
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[n][18].ToString()))
                    {
                        element.TotalParts = Convert.ToInt32(dt.Rows[n][18].ToString());
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[n][19].ToString()))
                    {
                        element.IsActive = Convert.ToBoolean(dt.Rows[n][19].ToString());
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[n][20].ToString()))
                    {
                        element.IsDeleted = Convert.ToBoolean(dt.Rows[n][20].ToString());
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[n][21].ToString()))
                    {
                        element.CreatedBy = Convert.ToInt32(dt.Rows[n][21].ToString());
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[n][22].ToString()))
                    {
                        element.CreatedOn = Convert.ToDateTime(dt.Rows[n][22].ToString());
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[n][23].ToString()))
                    {
                        element.ModifiedBy = Convert.ToInt32(dt.Rows[n][23].ToString());
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[n][24].ToString()))
                    {
                        element.ModifiedOn = Convert.ToDateTime(dt.Rows[n][24].ToString());
                    }
                    element.FacultyName =dt.Rows[n][25].ToString();
                    element.ProgrammeLevelName = dt.Rows[n][26].ToString();
                    element.ProgrammeModeName = dt.Rows[n][27].ToString();
                    element.ProgrammeTypeName = dt.Rows[n][28].ToString();
                    element.InstructionMediumName = dt.Rows[n][29].ToString();
                    element.EvaluationName = dt.Rows[n][30].ToString();
                    programmeList.Add(element);
                    n++;
                }
                return programmeList;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion
    }
}