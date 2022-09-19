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
    public class FeeCategoryChangeAfterAdmissionController : ApiController
    {
        #region Get Programme List from Faculty
        [HttpPost]
        public HttpResponseMessage getProgList()
        {
            string token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation tokenOperation = new TokenOperation();
            string responseToken = tokenOperation.ValidateToken(token);
            

            if (responseToken == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            else
            {
                try
                {
                    SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                    SqlDataAdapter Da = new SqlDataAdapter();
                    DataTable Dt = new DataTable();
                    SqlCommand Cmd = new SqlCommand("getProgListfromInstitute1", Con);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("@UserId", responseToken);
                    Da.SelectCommand = Cmd;
                    Da.Fill(Dt);
                    List<progfromFaculty> list = new List<progfromFaculty>();
                    list = Dt.DataTableToList<progfromFaculty>();
                    return Return.returnHttp("200", list, null);
                }
                catch (Exception e)
                {
                    return Return.returnHttp("201", e.Message, null);
                }
            }
        }
        #endregion

        #region Get Programme Part List from Programme
        [HttpPost]
        public HttpResponseMessage getProgPartfromProg(int ProgId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("getProgPartfromProg", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@ProgId", ProgId);
                Da.SelectCommand = Cmd;
                Da.Fill(Dt);
                List<progPartfromProg> list = new List<progPartfromProg>();
                list = Dt.DataTableToList<progPartfromProg>();
                return Return.returnHttp("200", list, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Get Programme Instance Part Term List from Programme Part
        [HttpPost]
        public HttpResponseMessage getProgInstPartTermfromProgPart(getFeeDetailsinput inputdata)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("getProgInstPartTermfromProgPart", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@AcademicYearId", inputdata.AcademicYearId);
                Cmd.Parameters.AddWithValue("@ProgrammeId", inputdata.ProgrammeId);
                Cmd.Parameters.AddWithValue("@ProgPartId", inputdata.ProgPartId);
                Da.SelectCommand = Cmd;
                Da.Fill(Dt);
                List<progInstPartTermfromProgPart> list = new List<progInstPartTermfromProgPart>();
                list = Dt.DataTableToList<progInstPartTermfromProgPart>();
                return Return.returnHttp("200", list, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region Get Old Fee Category and Applicable Fee DropDown

        [HttpPost]

        public HttpResponseMessage getFeeDetails(getFeeDetailsinput inputdata)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("getFeeDetails", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@PRN", inputdata.PRN);
                Cmd.Parameters.AddWithValue("@AcademicYearId", inputdata.AcademicYearId);
                Cmd.Parameters.AddWithValue("@ProgrammeId", inputdata.ProgrammeId);
                Cmd.Parameters.AddWithValue("@ProgPartId", inputdata.ProgPartId);
                Cmd.Parameters.AddWithValue("@ProgInstPartTermId", inputdata.ProgInstPartTermId);
                Da.SelectCommand = Cmd;
                Da.Fill(Dt);
                List<allFeeCategory> list = new List<allFeeCategory>();
                list = Dt.DataTableToList<allFeeCategory>();
                return Return.returnHttp("200", list, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region update new Fee Category in database

        [HttpPost]
        public HttpResponseMessage storeNewFeeCategory(getFeeDetailsinput inputdata,int NewFeeCategoryId)
        {
            try
            {
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                SqlCommand Cmd = new SqlCommand("storeFeeDetails", Con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@PRN", inputdata.PRN);
               // Cmd.Parameters.AddWithValue("@AcademicYearId", inputdata.AcademicYearId);
                //Cmd.Parameters.AddWithValue("@ProgrammeId", inputdata.ProgrammeId);
                //Cmd.Parameters.AddWithValue("@ProgPartId", inputdata.ProgPartId);
                Cmd.Parameters.AddWithValue("@ProgInstPartTermId", inputdata.ProgInstPartTermId);
                Cmd.Parameters.AddWithValue("@NewFeeCategoryId", NewFeeCategoryId);
                Da.SelectCommand = Cmd;
                Con.Open();
                Cmd.ExecuteNonQuery();
                Con.Close();
                return Return.returnHttp("200", "Success", null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion
    }
}
