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
    public class StudentPendingForFeeCategoryMappingController : ApiController
    {

        #region Get UnMapped Fee Category Student List
        [HttpPost]

        public HttpResponseMessage getUnmappedFeeCatStudentList(studentCountParameters s1, int SpecId)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            SqlDataAdapter Da = new SqlDataAdapter();
            DataTable Dt = new DataTable();
            SqlCommand Cmd = new SqlCommand("getStudentListwithUnmappedFeeCategory1", Con);
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@AcademicYearId", s1.AcademicYearId);
            //Cmd.Parameters.AddWithValue("@InstituteId", s1.InstituteId);
            Cmd.Parameters.AddWithValue("@ProgrammeId", s1.ProgrammeId);
           //Cmd.Parameters.AddWithValue("@ProgPartId", s1.ProgPartId);
            Cmd.Parameters.AddWithValue("@SpecialisationId", SpecId);
            Da.SelectCommand = Cmd;
            Da.Fill(Dt);
            List<StudentListWithUnMappedFeeCategory> StudListWithUnMappedFeeCategory = new List<StudentListWithUnMappedFeeCategory>();
           // StudListWithUnMappedFeeCategory = Dt.DataTableToList<StudentListWithUnMappedFeeCategory>();
            

            //SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            SqlDataAdapter Da1 = new SqlDataAdapter();
            DataTable Dt1 = new DataTable();
            SqlCommand Cmd1 = new SqlCommand("getMappedFeeCategories", Con);
            Cmd1.CommandType = CommandType.StoredProcedure;
            Cmd1.Parameters.AddWithValue("@AcademicYearId", s1.AcademicYearId);
           // Cmd1.Parameters.AddWithValue("@InstituteId", s1.InstituteId);
            Cmd1.Parameters.AddWithValue("@ProgrammeId", s1.ProgrammeId);
            Cmd1.Parameters.AddWithValue("@ProgPartId", s1.ProgPartId);
            Cmd1.Parameters.AddWithValue("@SpecialisationId", SpecId);
            Da1.SelectCommand = Cmd1;
            Da1.Fill(Dt1);
            List<ApplicableFeeCategories> ApplicableFeeCategories1 = new List<ApplicableFeeCategories>();
            ApplicableFeeCategories1 = Dt1.DataTableToList<ApplicableFeeCategories>();

            //foreach (StudentListWithUnMappedFeeCategory p1 in StudListWithUnMappedFeeCategory)
            //{
            //    List<ApplicableFeeCategories> ApplicableFeeCategories2 = ApplicableFeeCategories1.ToList();
            //    if (ApplicableFeeCategories2.Any())
            //    {
            //        p1.ApplicableFeeCategories = ApplicableFeeCategories2;
            //    }
            //}

            if (Dt.Rows.Count > 0)
            {
                foreach (DataRow dr in Dt.Rows)
                {
                    StudentListWithUnMappedFeeCategory studentlist = new StudentListWithUnMappedFeeCategory();
                    studentlist.PRN = Convert.ToInt64(dr["PRN"]);
                    studentlist.StudentName = Convert.ToString(dr["StudentName"]);
                    studentlist.gender = Convert.ToString(dr["gender"]);
                    //studentlist.oldAdmissionFeeCategoryId = Convert.ToInt32(dr["oldAdmissionFeeCategoryId"]);
                    studentlist.OldFeeCategoryName = Convert.ToString(dr["OldFeeCategoryName"]);
                    studentlist.newProgrammePartTermInstance = Convert.ToInt32(dr["newProgrammePartTermInstance"]);
                    studentlist.ApplicableFeeCategories = ApplicableFeeCategories1;
                    StudListWithUnMappedFeeCategory.Add(studentlist);
                }
            }
            return Return.returnHttp("200", StudListWithUnMappedFeeCategory, null);
            //return Return.returnHttp("201", "success", null);
        }
        #endregion

        #region Updating Assigned New Fee category in database
        [HttpPost]

        public HttpResponseMessage sendNewFeeCategoryList(List<StudentListWithUnMappedFeeCategory> s1)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
            SqlDataAdapter Da = new SqlDataAdapter();
            DataTable Dt = new DataTable();
            Dt.Columns.Add("PRN", typeof(Int64));
            Dt.Columns.Add("FeeCategoryId", typeof(Int16));
            Dt.Columns.Add("ProgrammeInstancePartTermId", typeof(Int16));
            List<StudentListWithUnMappedFeeCategory> FeeCategoryList = s1.Where(e => e.newAdmissionFeeCategoryId > 0).ToList();
            for (int i = 0; i < FeeCategoryList.Count; i++)
            {
                Int64 PRN = FeeCategoryList[i].PRN;
                Int32 FeeCategoryId = FeeCategoryList[i].newAdmissionFeeCategoryId;
                Int32 ProgrammeInstancePartTermId = FeeCategoryList[i].newProgrammePartTermInstance;
                Dt.Rows.Add(PRN, FeeCategoryId, ProgrammeInstancePartTermId);
            }

            SqlCommand Cmd = new SqlCommand("updateFeeCategoryInReplica", Con);
            Cmd.CommandType = CommandType.StoredProcedure;
            //Cmd.Parameters.AddWithValue("@PRN", s1.PRN);
            Cmd.Parameters.AddWithValue("@newFeeCategoryAssign", Dt);
            Con.Open();
            Cmd.ExecuteNonQuery();
            Con.Close();
            //Da.Fill(Dt);
            //List<StudentListWithUnMappedFeeCategory> StudListWithUnMappedFeeCategory = new List<StudentListWithUnMappedFeeCategory>();
            //StudListWithUnMappedFeeCategory = Dt.DataTableToList<StudentListWithUnMappedFeeCategory>();
            return Return.returnHttp("200", "success", null);
        }
        #endregion

    }
}
          


   