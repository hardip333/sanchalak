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
    public class VenueAllocationController : ApiController 
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();
        SqlTransaction ST;
        //TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        //DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        #region VenueAllocationDetailsCount
        [HttpPost]
        public HttpResponseMessage VenueAllocationDetailsCount(VenueAllocation ObjVA)
        {
            VenueAllocation modelobj = new VenueAllocation();

            try
            {
                modelobj.ExamMasterId = ObjVA.ExamMasterId;
                modelobj.ProgrammeId = ObjVA.ProgrammeId;
                modelobj.SpecialisationId = ObjVA.BranchId;
                modelobj.ProgrammePartTermId = ObjVA.ProgrammePartTermId;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("VenueAllocationDetailsCount", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamMasterId", modelobj.ExamMasterId);
                cmd.Parameters.AddWithValue("@ProgrammeId", modelobj.ProgrammeId);
                cmd.Parameters.AddWithValue("@SpecialisationId", modelobj.SpecialisationId);
                cmd.Parameters.AddWithValue("@ProgrammePartTermId", modelobj.ProgrammePartTermId);
                cmd.Parameters.Add("@SeatNoGeneratedCount", SqlDbType.BigInt);
                cmd.Parameters["@SeatNoGeneratedCount"].Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@VenueAllocatedCount", SqlDbType.BigInt);
                cmd.Parameters["@VenueAllocatedCount"].Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@VenueAllocatedPendingCount", SqlDbType.BigInt);
                cmd.Parameters["@VenueAllocatedPendingCount"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();

                Int64 SeatNoGeneratedCount = Convert.ToInt64(cmd.Parameters["@SeatNoGeneratedCount"].Value);
                Int64 VenueAllocatedCount = Convert.ToInt64(cmd.Parameters["@VenueAllocatedCount"].Value);
                Int64 VenueAllocatedPendingCount = Convert.ToInt64(cmd.Parameters["@VenueAllocatedPendingCount"].Value);

                Con.Close();

                VenueAllocation VenueAllocation = new VenueAllocation();
                VenueAllocation.SeatNoGeneratedCount = SeatNoGeneratedCount;
                VenueAllocation.VenueAllocatedCount = VenueAllocatedCount;
                VenueAllocation.VenueAllocatedPendingCount = VenueAllocatedPendingCount;

                return Return.returnHttp("200", VenueAllocation, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region VenueAllocationVenueGet
        [HttpPost]
        public HttpResponseMessage VenueAllocationVenueGet(VenueList Venue)
        {
            try
            {
                Int64 ExamMasterId = Venue.ExamMasterId;
                Int64 ProgrammePartTermId = Venue.ProgrammePartTermId;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("VenueAllocationVenueGet", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                cmd.Parameters.AddWithValue("@ProgrammePartTermId", ProgrammePartTermId);
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                //Int32 count = 0;
                List<VenueList> ObjVenueList = new List<VenueList>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        VenueList ObjVenue = new VenueList();
                        ObjVenue.Capacity = Convert.ToInt32(dr["Capacity"]);
                        ObjVenue.PendingCapacity = Convert.ToInt32(dr["PendingCapacity"]);
                        ObjVenue.Sequence = Convert.ToInt32(dr["Sequence"]);
                        ObjVenue.ExamVenueId = Convert.ToString(dr["ExamVenueId"]);
                        ObjVenue.DisplayName = (dr["DisplayName"].ToString());
                        ObjVenue.VenueSelected = false;
                        //count = count + 1;
                        //ObjVenue.IndexId = count;
                        ObjVenueList.Add(ObjVenue);
                    }

                }
                return Return.returnHttp("200", ObjVenueList, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region VenueAllocationForPendingStudents
        [HttpPost]
        public HttpResponseMessage VenueAllocationForPendingStudents(List<VenueList> ObjVenueList)

        {
            VenueList VenueList = new VenueList();
            //dynamic jsonData = jsonobject;

            try
            {

                //VenueList.ExamVenueId = Convert.ToString(jsonData.ExamVenueId);
                //VenueList.ProgrammePartTermId = Convert.ToInt32(jsonData.ProgrammePartTermId);
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                VenueList.UserId = Convert.ToInt64(responseToken);

                DataTable VenueList1 = new DataTable();
                VenueList1.Columns.Add(new DataColumn("Id", typeof(Int32)));
                foreach (VenueList v in ObjVenueList)
                {
                    VenueList.ExamMasterId = Convert.ToInt32(v.ExamMasterId);
                    VenueList.ProgrammePartTermId = Convert.ToInt32(v.ProgrammePartTermId);
                    VenueList.SpecialisationId = Convert.ToInt32(v.SpecialisationId);
                    if (v.VenueSelected)
                    {
                        VenueList1.Rows.Add(v.ExamVenueId);
                    }
                }
                SqlCommand cmd = new SqlCommand("VenueAllocationForPendingStudents", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VenueList", VenueList1);
                cmd.Parameters.AddWithValue("@ExamMasterId", VenueList.ExamMasterId);
                cmd.Parameters.AddWithValue("@ProgrammePartTermId", VenueList.ProgrammePartTermId);
                cmd.Parameters.AddWithValue("@SpecialisationId", VenueList.SpecialisationId);
                cmd.Parameters.AddWithValue("@UserId", VenueList.UserId);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                Con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                Con.Close();

                return Return.returnHttp("200", strMessage, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion
    }
}
