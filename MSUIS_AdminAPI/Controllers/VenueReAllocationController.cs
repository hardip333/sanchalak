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
    public class VenueReAllocationController : ApiController 
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter Da = new SqlDataAdapter();
        DataTable Dt = new DataTable();
        Validation validation = new Validation();

        #region VenueReAllocationVenueGetByProgPTId
        [HttpPost]
        public HttpResponseMessage VenueReAllocationVenueGetByProgPTId(ReallocationVenueList Venue)
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

                SqlCommand cmd = new SqlCommand("VenueReAllocationVenueGetByProgPTId", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                cmd.Parameters.AddWithValue("@ProgrammePartTermId", ProgrammePartTermId);
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<ReallocationVenueList> ObjVenueList = new List<ReallocationVenueList>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        ReallocationVenueList ObjVenue = new ReallocationVenueList();
                        ObjVenue.ExamVenueId = Convert.ToInt32(dr["ExamVenueId"]);
                        ObjVenue.DisplayName = (dr["DisplayName"].ToString());
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

        #region VenueReAllocationStudentsGet
        [HttpPost]
        public HttpResponseMessage VenueReAllocationStudentsGet(ReAllocationStudent RAS)
        {
            try
            {
                Int32 ExamMasterId = RAS.ExamMasterId;
                Int32 ProgrammeId = RAS.ProgrammeId;
                Int32 SpecialisationId = RAS.BranchId;
                Int32 ProgrammePartTermId = RAS.ProgrammePartTermId;
                Int32 ExamVenueId = RAS.ExamVenueId;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("VenueReAllocationStudentsGet", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                cmd.Parameters.AddWithValue("@ProgrammeId", ProgrammeId);
                cmd.Parameters.AddWithValue("@SpecialisationId", SpecialisationId);
                cmd.Parameters.AddWithValue("@ProgrammePartTermId", ProgrammePartTermId);
                cmd.Parameters.AddWithValue("@ExamVenueId", ExamVenueId);

                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                Int32 count = 0;
                List<ReAllocationStudent> ObjRAS = new List<ReAllocationStudent>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        ReAllocationStudent ObjStu = new ReAllocationStudent();
                        ObjStu.ExamMasterId = Convert.ToInt32(dr["ExamMasterId"]);
                        ObjStu.EventName = Convert.ToString(dr["EventName"]);
                        ObjStu.PRN = Convert.ToInt64(dr["PRN"]);
                        ObjStu.FullName = Convert.ToString(dr["LastName"]) + " " + Convert.ToString(dr["FirstName"]) + " " + Convert.ToString(dr["MiddleName"]);

                        ObjStu.ExamVenueId = Convert.ToInt32(dr["ExamVenueId"]);
                        ObjStu.VenueName = Convert.ToString(dr["VenueName"]);

                        ObjStu.ProgInstancePartTermId = Convert.ToInt64(dr["ProgInstancePartTermId"]);
                        ObjStu.InstancePartTermName = Convert.ToString(dr["InstancePartTermName"]);

                        ObjStu.ProgrammeId = Convert.ToInt32(dr["ProgrammeId"]);
                        ObjStu.ProgrammeName = Convert.ToString(dr["ProgrammeName"]);

                        ObjStu.SpecialisationId = Convert.ToInt32(dr["SpecialisationId"]);
                        ObjStu.BranchName = Convert.ToString(dr["BranchName"]);

                        ObjStu.ProgrammePartTermId = Convert.ToInt32(dr["ProgrammePartTermId"]);
                        ObjStu.PartTermName = Convert.ToString(dr["PartTermName"]);
                        count = count + 1;
                        ObjStu.IndexId = count;
                        ObjRAS.Add(ObjStu);
                    }

                }
                return Return.returnHttp("200", ObjRAS, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion

        #region VenueReAllocationVenueGet
        [HttpPost]
        public HttpResponseMessage VenueReAllocationVenueGet(ReallocationVenueList Venue)
        {
            try
            {
                Int64 ExamMasterId = Venue.ExamMasterId;
                Int64 ProgrammePartTermId = Venue.ProgrammePartTermId;
                Int64 ExamVenueId = Venue.ExamVenueId;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("VenueReAllocationVenueGet", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamMasterId", ExamMasterId);
                cmd.Parameters.AddWithValue("@ProgrammePartTermId", ProgrammePartTermId);
                cmd.Parameters.AddWithValue("@ExamVenueId", ExamVenueId);
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<ReallocationVenueList> ObjVenueList = new List<ReallocationVenueList>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        ReallocationVenueList ObjVenue = new ReallocationVenueList();
                        ObjVenue.ExamVenueId = Convert.ToInt32(dr["ExamVenueId"]);
                        ObjVenue.DisplayName = (dr["DisplayName"].ToString());
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

        #region VenueReAllocationEditforStudents
        [HttpPost]
        public HttpResponseMessage VenueReAllocationEditforStudents(JObject jsonobject)

        {
            ReAllocationStudent ReAllocationStudent = new ReAllocationStudent();
            dynamic jsonData = jsonobject;

            try
            {
                ReAllocationStudent.ReAllocationPRNData = Convert.ToString(jsonData.ReAllocationPRNData);
                ReAllocationStudent.ProgrammePartTermId = Convert.ToInt32(jsonData.ProgrammePartTermId);
                ReAllocationStudent.ExamVenueId = Convert.ToInt32(jsonData.ExamVenueId);
                ReAllocationStudent.ExamReVenueId = Convert.ToInt32(jsonData.ExamReVenueId);
                ReAllocationStudent.SelectedCheckCount = Convert.ToInt32(jsonData.SelectedCheckCount);

                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                ReAllocationStudent.LastVenueReAllocatedBy = Convert.ToInt64(responseToken);

                SqlCommand cmd = new SqlCommand("VenueReAllocationEditforStudents", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReAllocationPRNData", ReAllocationStudent.ReAllocationPRNData);
                cmd.Parameters.AddWithValue("@ProgrammePartTermId", ReAllocationStudent.ProgrammePartTermId);
                cmd.Parameters.AddWithValue("@ExamVenueId", ReAllocationStudent.ExamVenueId);
                cmd.Parameters.AddWithValue("@ExamReVenueId", ReAllocationStudent.ExamReVenueId);
                cmd.Parameters.AddWithValue("@SelectedCheckCount", ReAllocationStudent.SelectedCheckCount);
                cmd.Parameters.AddWithValue("@LastVenueReAllocatedBy", ReAllocationStudent.LastVenueReAllocatedBy);
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
