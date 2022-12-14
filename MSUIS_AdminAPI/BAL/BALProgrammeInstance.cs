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
    public class BALProgrammeInstance
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();

        #region Programme Instance List
        public List<ProgramInstance> ProgrammeInstanceGet()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("IncProgrammeInstanceGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<ProgramInstance> ObjLstProgInst = new List<ProgramInstance>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ProgramInstance ObjProgInst = new ProgramInstance();
                        
                        ObjProgInst.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        ObjProgInst.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        ObjProgInst.FacultyId = (Convert.ToString(Dt.Rows[i]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        ObjProgInst.ProgrammeId = (Convert.ToString(Dt.Rows[i]["ProgrammeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);
                        ObjProgInst.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        ObjProgInst.InstanceName = (Convert.ToString(Dt.Rows[i]["InstanceName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstanceName"]);                        
                        ObjProgInst.AcademicYear = (Convert.ToString(Dt.Rows[i]["AcademicYearCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);
                        ObjProgInst.AcademicYearId = (Convert.ToString(Dt.Rows[i]["AcademicYearId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["AcademicYearId"]);
                        ObjProgInst.Intake = (Convert.ToString(Dt.Rows[i]["Intake"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["Intake"]);
                        ObjProgInst.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjProgInst.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        ObjProgInst.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);

                        ObjLstProgInst.Add(ObjProgInst);                        
                    }
                }
                return ObjLstProgInst;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Programme Instance List Get By Id
        public ProgramInstance ProgrammeInstanceListGetbyId(Int64? ProgInstId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("IncProgrammeInstanceGetbyId", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProgrammeInstanceId", ProgInstId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                if(Dt.Rows.Count > 0)
                {
                    ProgramInstance ObjProgInst = new ProgramInstance();

                    ObjProgInst.Id = Convert.ToInt32(Dt.Rows[0]["Id"]);
                    ObjProgInst.FacultyName = (Convert.ToString(Dt.Rows[0]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["FacultyName"]);
                    ObjProgInst.FacultyId = (Convert.ToString(Dt.Rows[0]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["FacultyId"]);
                    ObjProgInst.ProgrammeId = (Convert.ToString(Dt.Rows[0]["ProgrammeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["ProgrammeId"]);
                    ObjProgInst.ProgrammeName = (Convert.ToString(Dt.Rows[0]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["ProgrammeName"]);
                    ObjProgInst.InstanceName = (Convert.ToString(Dt.Rows[0]["InstanceName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["InstanceName"]);
                    ObjProgInst.AcademicYear = (Convert.ToString(Dt.Rows[0]["AcademicYearCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["AcademicYearCode"]);
                    ObjProgInst.AcademicYearId = (Convert.ToString(Dt.Rows[0]["AcademicYearId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["AcademicYearId"]);
                    ObjProgInst.Intake = (Convert.ToString(Dt.Rows[0]["Intake"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[0]["Intake"]);
                    ObjProgInst.IsActive = (Convert.ToString(Dt.Rows[0]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsActive"]);
                    ObjProgInst.IsActiveSts = (Convert.ToString(Dt.Rows[0]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[0]["IsActiveSts"]);
                    ObjProgInst.IsDeleted = (Convert.ToString(Dt.Rows[0]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[0]["IsDeleted"]);
                    
                    return ObjProgInst;

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

        #region Programme Instance Get By FacultyId        
        public List<ProgramInstance> ProgrammeInstanceListGetbyFacultyId(int? FacultyId)
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("IncProgrammeInstanceGetByFacultyId", Con);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@FacultyId", FacultyId);

                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<ProgramInstance> ObjLstProgInst = new List<ProgramInstance>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ProgramInstance ObjProgInst = new ProgramInstance();

                        ObjProgInst.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        ObjProgInst.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        ObjProgInst.FacultyId = (Convert.ToString(Dt.Rows[i]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        ObjProgInst.ProgrammeId = (Convert.ToString(Dt.Rows[i]["ProgrammeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);
                        ObjProgInst.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        ObjProgInst.InstanceName = (Convert.ToString(Dt.Rows[i]["InstanceName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstanceName"]);
                        ObjProgInst.AcademicYear = (Convert.ToString(Dt.Rows[i]["AcademicYearCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);
                        ObjProgInst.AcademicYearId = (Convert.ToString(Dt.Rows[i]["AcademicYearId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["AcademicYearId"]);
                        ObjProgInst.Intake = (Convert.ToString(Dt.Rows[i]["Intake"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["Intake"]);
                        ObjProgInst.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjProgInst.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        ObjProgInst.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);

                        ObjLstProgInst.Add(ObjProgInst);
                    }
                }
                return ObjLstProgInst;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Programme Instance Get By FacultyId And AcademicYearId
        public List<ProgramInstance> InstanceListGetbyFacultyIdAndAcadId(int? FacultyId,int? AcadId)
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("IncProgrammeInstanceGetByFacultyIdAndAcadId", Con);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                Cmd.Parameters.AddWithValue("@AcademicYearId", AcadId);

                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<ProgramInstance> ObjLstProgInst = new List<ProgramInstance>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ProgramInstance ObjProgInst = new ProgramInstance();

                        ObjProgInst.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        ObjProgInst.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        ObjProgInst.FacultyId = (Convert.ToString(Dt.Rows[i]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        ObjProgInst.ProgrammeId = (Convert.ToString(Dt.Rows[i]["ProgrammeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);
                        ObjProgInst.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        ObjProgInst.InstanceName = (Convert.ToString(Dt.Rows[i]["InstanceName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstanceName"]);
                        ObjProgInst.AcademicYear = (Convert.ToString(Dt.Rows[i]["AcademicYearCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);
                        ObjProgInst.AcademicYearId = (Convert.ToString(Dt.Rows[i]["AcademicYearId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["AcademicYearId"]);
                        ObjProgInst.Intake = (Convert.ToString(Dt.Rows[i]["Intake"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["Intake"]);
                        ObjProgInst.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjProgInst.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        ObjProgInst.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);

                        ObjLstProgInst.Add(ObjProgInst);
                    }
                }
                return ObjLstProgInst;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region Mohini - Programme Instance Get By FacultyId And AcademicYearId
        public List<ProgramInstance> MInstanceListGetbyFacultyIdAndAcadId(int? FacultyId, int? AcadId, int? AdmissionYearId)
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MIncProgrammeInstanceGetByFacultyIdAndAcadId", Con);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@FacultyId", FacultyId);
                Cmd.Parameters.AddWithValue("@AcademicYearId", AcadId);
                Cmd.Parameters.AddWithValue("@AdmissionYearId", AdmissionYearId);

                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<ProgramInstance> ObjLstProgInst = new List<ProgramInstance>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ProgramInstance ObjProgInst = new ProgramInstance();

                        ObjProgInst.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        ObjProgInst.FacultyName = (Convert.ToString(Dt.Rows[i]["FacultyName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["FacultyName"]);
                        ObjProgInst.FacultyId = (Convert.ToString(Dt.Rows[i]["FacultyId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["FacultyId"]);
                        ObjProgInst.ProgrammeId = (Convert.ToString(Dt.Rows[i]["ProgrammeId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);
                        ObjProgInst.ProgrammeName = (Convert.ToString(Dt.Rows[i]["ProgrammeName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        ObjProgInst.InstanceName = (Convert.ToString(Dt.Rows[i]["InstanceName"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["InstanceName"]);
                        ObjProgInst.AcademicYear = (Convert.ToString(Dt.Rows[i]["AcademicYearCode"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["AcademicYearCode"]);
                        ObjProgInst.AcademicYearId = (Convert.ToString(Dt.Rows[i]["AcademicYearId"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["AcademicYearId"]);
                        ObjProgInst.Intake = (Convert.ToString(Dt.Rows[i]["Intake"])).IsEmpty() ? 0 : Convert.ToInt32(Dt.Rows[i]["Intake"]);
                        ObjProgInst.IsActive = (Convert.ToString(Dt.Rows[i]["IsActive"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsActive"]);
                        ObjProgInst.IsActiveSts = (Convert.ToString(Dt.Rows[i]["IsActiveSts"])).IsEmpty() ? "" : Convert.ToString(Dt.Rows[i]["IsActiveSts"]);
                        ObjProgInst.IsDeleted = (Convert.ToString(Dt.Rows[i]["IsDeleted"])).IsEmpty() ? false : Convert.ToBoolean(Dt.Rows[i]["IsDeleted"]);

                        ObjLstProgInst.Add(ObjProgInst);
                    }
                }
                return ObjLstProgInst;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion
    }
}