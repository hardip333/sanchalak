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
    public class BALProgrammePartTerm
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        #region Programme Part Term List
        public List<MstProgrammePartTerm> ProgrammePartTermGet()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstProgrammePartTermGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);
                Int32 count = 0 ;
                List<MstProgrammePartTerm> ObjLstProgPartTerm = new List<MstProgrammePartTerm>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgrammePartTerm objProgPartTerm = new MstProgrammePartTerm();

                        objProgPartTerm.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objProgPartTerm.PartId = (Convert.ToString(Dt.Rows[i]["PartId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["PartId"]);
                        objProgPartTerm.PartName = (Convert.ToString(Dt.Rows[i]["PartName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PartName"]);
                        objProgPartTerm.PartTermName = (Convert.ToString(Dt.Rows[i]["PartTermName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PartTermName"]);
                        objProgPartTerm.PartTermShortName = (Convert.ToString(Dt.Rows[i]["PartTermShortName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PartTermShortName"]);
                        objProgPartTerm.SequenceNo = (Convert.ToString(Dt.Rows[i]["SequenceNo"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["SequenceNo"]);
                        objProgPartTerm.FacultyId = (Convert.ToString(Dt.Rows[i]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        objProgPartTerm.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objProgPartTerm.ProgrammeId = (Convert.ToString(Dt.Rows[i]["ProgrammeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);
                        objProgPartTerm.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        objProgPartTerm.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objProgPartTerm.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);
                        objProgPartTerm.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        count = count + 1;
                        objProgPartTerm.IndexId = count;                        
                        ObjLstProgPartTerm.Add(objProgPartTerm);                        
                    }
                }
                return ObjLstProgPartTerm;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Programme Part Term List Get By Id
        public MstProgrammePartTerm ProgrammePartTermListGetbyId(int? ProgPartTermId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MstProgrammePartTermGetbyId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PartTermId", ProgPartTermId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if(Dt.Rows.Count > 0)
                {
                    MstProgrammePartTerm objProgPartTerm = new MstProgrammePartTerm();

                    objProgPartTerm.Id = Convert.ToInt32(Dt.Rows[0]["Id"]);
                    objProgPartTerm.PartId = (Convert.ToString(Dt.Rows[0]["PartId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["PartId"]);
                    objProgPartTerm.PartName = (Convert.ToString(Dt.Rows[0]["PartName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["PartName"]);
                    objProgPartTerm.PartTermName = (Convert.ToString(Dt.Rows[0]["PartTermName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["PartTermName"]);
                    objProgPartTerm.PartTermShortName = (Convert.ToString(Dt.Rows[0]["PartTermShortName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["PartTermShortName"]);
                    objProgPartTerm.SequenceNo = (Convert.ToString(Dt.Rows[0]["SequenceNo"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["SequenceNo"]);
                    objProgPartTerm.FacultyId = (Convert.ToString(Dt.Rows[0]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["FacultyId"]);
                    objProgPartTerm.FacultyName = (Convert.ToString(Dt.Rows[0]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["FacultyName"]);
                    objProgPartTerm.ProgrammeId = (Convert.ToString(Dt.Rows[0]["ProgrammeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["ProgrammeId"]);
                    objProgPartTerm.ProgrammeName = (Convert.ToString(Dt.Rows[0]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["ProgrammeName"]);
                    objProgPartTerm.IsActive = (Convert.ToString(Dt.Rows[0]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsActive"]);
                    objProgPartTerm.IsDeleted = (Convert.ToString(Dt.Rows[0]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsDeleted"]);
                    objProgPartTerm.IsActiveSts = (Convert.ToString(Dt.Rows[0]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["IsActiveSts"]);

                    return objProgPartTerm;

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

        #region ProgrammePartTermGetByPartId        
        public List<MstProgrammePartTerm> ProgrammePartTermGetByPartId(int? partId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MstProgrammePartTermGetByPartId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PartId", partId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<MstProgrammePartTerm> ObjLstProgPartTerm = new List<MstProgrammePartTerm>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgrammePartTerm objProgPartTerm = new MstProgrammePartTerm();

                        objProgPartTerm.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objProgPartTerm.PartId = (Convert.ToString(Dt.Rows[i]["PartId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["PartId"]);
                        objProgPartTerm.PartName = (Convert.ToString(Dt.Rows[i]["PartName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PartName"]);
                        objProgPartTerm.PartTermName = (Convert.ToString(Dt.Rows[i]["PartTermName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PartTermName"]);
                        objProgPartTerm.PartTermShortName = (Convert.ToString(Dt.Rows[i]["PartTermShortName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PartTermShortName"]);
                        objProgPartTerm.SequenceNo = (Convert.ToString(Dt.Rows[i]["SequenceNo"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["SequenceNo"]);
                        objProgPartTerm.FacultyId = (Convert.ToString(Dt.Rows[i]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        objProgPartTerm.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objProgPartTerm.ProgrammeId = (Convert.ToString(Dt.Rows[i]["ProgrammeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);
                        objProgPartTerm.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        objProgPartTerm.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objProgPartTerm.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);
                        objProgPartTerm.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        ObjLstProgPartTerm.Add(objProgPartTerm);
                    }
                }
                return ObjLstProgPartTerm;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        // Code Done By Megha at 22 Oct 2021 16.56
        #region ProgrammePartTermGetByProgrammeId        
        public List<MstProgrammePartTerm> ProgrammePartTermGetByProgrammeId(int? programmeId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MstProgrammePartTermGetByProgrammeId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeId", programmeId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<MstProgrammePartTerm> ObjLstProgPartTerm = new List<MstProgrammePartTerm>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgrammePartTerm objProgPartTerm = new MstProgrammePartTerm();

                        objProgPartTerm.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objProgPartTerm.PartId = (Convert.ToString(Dt.Rows[i]["PartId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["PartId"]);
                        objProgPartTerm.PartName = (Convert.ToString(Dt.Rows[i]["PartName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PartName"]);
                        objProgPartTerm.PartTermName = (Convert.ToString(Dt.Rows[i]["PartTermName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PartTermName"]);
                        objProgPartTerm.PartTermShortName = (Convert.ToString(Dt.Rows[i]["PartTermShortName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PartTermShortName"]);
                        objProgPartTerm.SequenceNo = (Convert.ToString(Dt.Rows[i]["SequenceNo"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["SequenceNo"]);
                        objProgPartTerm.FacultyId = (Convert.ToString(Dt.Rows[i]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        objProgPartTerm.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        objProgPartTerm.ProgrammeId = (Convert.ToString(Dt.Rows[i]["ProgrammeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);
                        objProgPartTerm.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        objProgPartTerm.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objProgPartTerm.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);
                        objProgPartTerm.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        ObjLstProgPartTerm.Add(objProgPartTerm);
                    }
                }
                return ObjLstProgPartTerm;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<MstProgrammePartTerm> ProgrammePartTermGetByProgrammeIdForCourse(int? programmeId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MstProgrammePartTermGetByProgrammeIdwithCourse", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeId", programmeId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<MstProgrammePartTerm> ObjLstProgPartTerm = new List<MstProgrammePartTerm>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgrammePartTerm objProgPartTerm = new MstProgrammePartTerm();

                        objProgPartTerm.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objProgPartTerm.PartId = (Convert.ToString(Dt.Rows[i]["PartId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["PartId"]);
                        objProgPartTerm.PartTermName = (Convert.ToString(Dt.Rows[i]["PartTermName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PartTermName"]);
                        objProgPartTerm.PartTermShortName = (Convert.ToString(Dt.Rows[i]["PartTermShortName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PartTermShortName"]);
                        objProgPartTerm.SequenceNo = (Convert.ToString(Dt.Rows[i]["SequenceNo"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["SequenceNo"]);
                        objProgPartTerm.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objProgPartTerm.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);
                        ObjLstProgPartTerm.Add(objProgPartTerm);
                    }
                }
                return ObjLstProgPartTerm;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<MstProgrammePartTerm> ProgrammePartTermGetByProgrammeIdAndBranchId(int? programmeId,int? BranchId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MstProgrammePartTermListGetByProgIdandBranchId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeId", programmeId);
                cmd.Parameters.AddWithValue("@BranchId", BranchId);
                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<MstProgrammePartTerm> ObjLstProgPartTerm = new List<MstProgrammePartTerm>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgrammePartTerm objProgPartTerm = new MstProgrammePartTerm();

                        objProgPartTerm.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objProgPartTerm.PartId = (Convert.ToString(Dt.Rows[i]["PartId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["PartId"]);
                       // objProgPartTerm.PartName = (Convert.ToString(Dt.Rows[i]["PartName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PartName"]);
                        objProgPartTerm.PartTermName = (Convert.ToString(Dt.Rows[i]["PartTermName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PartTermName"]);
                        objProgPartTerm.PartTermShortName = (Convert.ToString(Dt.Rows[i]["PartTermShortName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PartTermShortName"]);
                        objProgPartTerm.SequenceNo = (Convert.ToString(Dt.Rows[i]["SequenceNo"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["SequenceNo"]);
                        //objProgPartTerm.FacultyId = (Convert.ToString(Dt.Rows[i]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        //objProgPartTerm.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        //objProgPartTerm.ProgrammeId = (Convert.ToString(Dt.Rows[i]["ProgrammeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);
                        //objProgPartTerm.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        objProgPartTerm.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objProgPartTerm.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);
                        //objProgPartTerm.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        ObjLstProgPartTerm.Add(objProgPartTerm);
                    }
                }
                return ObjLstProgPartTerm;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<MstProgrammePartTerm> GetProgrammePartListForExamFeeConfig(int? FacultyExamMapId,int? ExamMasterId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("GetPendingProgrammePartListForExamFeeConfig", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FacultyExamMapId", FacultyExamMapId);
                cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);

                List<MstProgrammePartTerm> ObjLstProgPartTerm = new List<MstProgrammePartTerm>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstProgrammePartTerm objProgPartTerm = new MstProgrammePartTerm();

                        objProgPartTerm.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objProgPartTerm.PartId = (Convert.ToString(Dt.Rows[i]["PartId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["PartId"]);
                        objProgPartTerm.PartTermName = (Convert.ToString(Dt.Rows[i]["PartTermName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PartTermName"]);
                        objProgPartTerm.PartTermShortName = (Convert.ToString(Dt.Rows[i]["PartTermShortName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["PartTermShortName"]);
                        objProgPartTerm.SequenceNo = (Convert.ToString(Dt.Rows[i]["SequenceNo"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["SequenceNo"]);
                        objProgPartTerm.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        objProgPartTerm.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);
                        ObjLstProgPartTerm.Add(objProgPartTerm);
                    }
                }
                return ObjLstProgPartTerm;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion
    }
}