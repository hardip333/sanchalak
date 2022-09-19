using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MSUISApi.BAL
{
    public class BALExamBlocks
    {
        public ExamBlocks setRowIntoObjForAdmin(ExamBlocks element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            element.ExamVenueId = Convert.ToInt32(dr["ExamVenueId"].ToString());
            element.ExamVenue = dr["ExamVenue"].ToString();
            element.Name = dr["Name"].ToString();
            if (!string.IsNullOrEmpty(dr["RoomNo"].ToString()))
                element.RoomNo = dr["RoomNo"].ToString();
            if (!string.IsNullOrEmpty(dr["SequenceNo"].ToString()))
                element.SequenceNo = Convert.ToInt32(dr["SequenceNo"].ToString());
            if (!string.IsNullOrEmpty(dr["TotalBench"].ToString()))
                element.TotalBench = Convert.ToInt32(dr["TotalBench"].ToString());
            if (!string.IsNullOrEmpty(dr["IsDualCapacityAvailable"].ToString()))
                element.IsDualCapacityAvailable = Convert.ToBoolean(dr["IsDualCapacityAvailable"].ToString());
            if (!string.IsNullOrEmpty(dr["IncludeInAutoAllocation"].ToString()))
                element.IncludeInAutoAllocation = Convert.ToBoolean(dr["IncludeInAutoAllocation"].ToString());
            element.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
            element.IsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
            element.ExamCenterId = Convert.ToInt16(dr["ExamCenterId"].ToString());
            return element;

        }

        public ExamBlocks setRowIntoObjForUser(ExamBlocks element, DataRow dr)
        {
            element.Id = Convert.ToInt32(dr["Id"].ToString());
            element.ExamVenueId = Convert.ToInt32(dr["ExamVenueId"].ToString());
            element.ExamVenue = dr["ExamVenue"].ToString();
            element.Name = dr["Name"].ToString();
            if (!string.IsNullOrEmpty(dr["RoomNo"].ToString()))
                element.RoomNo = dr["RoomNo"].ToString();
            if (!string.IsNullOrEmpty(dr["SequenceNo"].ToString()))
                element.SequenceNo = Convert.ToInt32(dr["SequenceNo"].ToString());
            if (!string.IsNullOrEmpty(dr["TotalBench"].ToString()))
                element.TotalBench = Convert.ToInt32(dr["TotalBench"].ToString());
            if (!string.IsNullOrEmpty(dr["IsDualCapacityAvailable"].ToString()))
                element.IsDualCapacityAvailable = Convert.ToBoolean(dr["IsDualCapacityAvailable"].ToString());
            if (!string.IsNullOrEmpty(dr["IncludeInAutoAllocation"].ToString()))
                element.IncludeInAutoAllocation = Convert.ToBoolean(dr["IncludeInAutoAllocation"].ToString());
            element.ExamCenterId = Convert.ToInt16(dr["ExamCenterId"].ToString());
            return element;
        }

        public List<ExamBlocks> getExamBlocksList(string flag, int? IsDeleted, int? IsActive, int? ExamVenueId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("ExamBlocksListGet", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Flag", flag);
                Cmd.Parameters.AddWithValue("@IsActive", IsActive);
                Cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
                Cmd.Parameters.AddWithValue("@ExamVenueId", ExamVenueId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<ExamBlocks> ObjListExamBlocks = new List<ExamBlocks>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ExamBlocks objExamBlocks = new ExamBlocks();
                        if (flag == "admin")
                        {
                            setRowIntoObjForAdmin(objExamBlocks, Dt.Rows[i]);
                        }
                        else if (flag == "user")
                        {
                            setRowIntoObjForUser(objExamBlocks, Dt.Rows[i]);
                        }

                        ObjListExamBlocks.Add(objExamBlocks);
                    }
                }
                return ObjListExamBlocks;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<ExamBlocks> getExamBlocksListByInstId(string flag, int? IsDeleted, int? IsActive, int? ExamVenueId, Int64 UserId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("ExamBlocksListGetByInstId", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@Flag", flag);
                Cmd.Parameters.AddWithValue("@IsActive", IsActive);
                Cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
                Cmd.Parameters.AddWithValue("@ExamVenueId", ExamVenueId);
                //Cmd.Parameters.AddWithValue("@InstituteId", InstituteId);
                Cmd.Parameters.AddWithValue("@UserId", UserId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<ExamBlocks> ObjListExamBlocks = new List<ExamBlocks>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ExamBlocks objExamBlocks = new ExamBlocks();
                        if (flag == "admin")
                        {
                            setRowIntoObjForAdmin(objExamBlocks, Dt.Rows[i]);
                        }
                        else if (flag == "user")
                        {
                            setRowIntoObjForUser(objExamBlocks, Dt.Rows[i]);
                        }

                        ObjListExamBlocks.Add(objExamBlocks);
                    }
                }
                return ObjListExamBlocks;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<AvailableExamBlock> getAvailableExamBlockList(int? ProgrammePartTermId,int? ExamMasterId,string SpecialisationId,string PaperId,int? ExamVenueId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("getAvailableExamBlocksList", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@ProgrammePartTermId", ProgrammePartTermId);
                Cmd.Parameters.AddWithValue("@ExamEventMasterId", ExamMasterId);
                Cmd.Parameters.AddWithValue("@SpecialisationId", SpecialisationId);
                Cmd.Parameters.AddWithValue("@ExamVenueId", ExamVenueId);
                Cmd.Parameters.AddWithValue("@PaperId", PaperId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<AvailableExamBlock> ObjListAvailExamBlocks = new List<AvailableExamBlock>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        AvailableExamBlock element = new AvailableExamBlock();
                        element.Id = Convert.ToInt32(Dt.Rows[i]["ExamBlockId"].ToString());
                        element.ExamVenueId = Convert.ToInt32(Dt.Rows[i]["ExamVenueId"].ToString());
                        element.ExamVenue = Dt.Rows[i]["ExamVenue"].ToString();
                        element.Name = Dt.Rows[i]["Name"].ToString();
                        element.RoomNo = Dt.Rows[i]["RoomNo"].ToString();
                        if (!string.IsNullOrEmpty(Dt.Rows[i]["AvailableCapacity"].ToString()))
                            element.Capacity = Convert.ToInt16(Dt.Rows[i]["AvailableCapacity"].ToString());


                        ObjListAvailExamBlocks.Add(element);
                    }
                }
                return ObjListAvailExamBlocks;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<ExamFormBarcodeList> GetDetailsOfExamFormBarcode(int? examMasterId, string PaperId, string specialisationId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("GetDetailsOfExamFormBarcode", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@ExamMasterId", examMasterId);
                Cmd.Parameters.AddWithValue("@PaperId", PaperId);
                Cmd.Parameters.AddWithValue("@SpecialisationId", specialisationId);
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                List<ExamFormBarcodeList> ObjListExamFormBarcode = new List<ExamFormBarcodeList>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ExamFormBarcodeList element = new ExamFormBarcodeList();
                        element.UId = Dt.Rows[i]["uid"].ToString();
                        element.SeatNo = Dt.Rows[i]["SeatNumber"].ToString();
                        element.ExamVenue = Dt.Rows[i]["ExamVenue"].ToString();
                        element.ExamEventMaster = Dt.Rows[i]["ExamEventMaster"].ToString();
                        element.RoomNo = Dt.Rows[i]["RoomNo"].ToString();
                        element.PaperName = Dt.Rows[i]["PaperName"].ToString();
                        element.PaperId = Convert.ToInt16(Dt.Rows[i]["MstPaperId"].ToString());
                        element.ProgrammePart = Dt.Rows[i]["ProgrammePart"].ToString().Trim();
                        element.PaperCode = Dt.Rows[i]["PaperCode"].ToString().Trim();
                        element.ExamDate = Dt.Rows[i]["ExamDate"].ToString();
                        //element.ExamBlockId = Convert.ToInt16(Dt.Rows[i]["ExamBlockId"].ToString());
                        var ExamBlockId = Dt.Rows[i]["ExamBlockId"];
                        if (ExamBlockId is DBNull)
                        {
                            element.ExamBlockId = 0;

                        }
                        else
                        {
                            element.ExamBlockId = Convert.ToInt16(Dt.Rows[i]["ExamBlockId"].ToString());

                        }
                        element.ExamVenueId = Convert.ToInt16(Dt.Rows[i]["ExamVenueId"].ToString());

                        ObjListExamFormBarcode.Add(element);
                    }
                }
                return ObjListExamFormBarcode;
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}