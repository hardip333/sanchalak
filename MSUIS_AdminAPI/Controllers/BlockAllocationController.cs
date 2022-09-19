using MSUIS_TokenManager.App_Start;
using MSUISApi.BAL;
using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MSUISApi.Controllers
{
    public class BlockAllocationController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        [HttpPost]
        public HttpResponseMessage MstSpecialisationGetByPId(AssignBlocks dataString)
        {
            try
            {
                SqlCommand Cmd = new SqlCommand("MstSpecialisationGetByPId", con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@ProgrammeId", dataString.ProgrammeId);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();
                Da.SelectCommand = Cmd;

                Da.Fill(Dt);

                dataString.SpecialisationsList = new List<MstSpecialisation>();

                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        MstSpecialisation objS = new MstSpecialisation();

                        objS.Id = Convert.ToInt32(Dt.Rows[i]["Id"]);
                        objS.BranchName = Convert.ToString(Dt.Rows[i]["BranchName"]);
                        objS.ProgrammeId = Convert.ToInt32(Dt.Rows[i]["ProgrammeId"]);
                        objS.ProgrammeName = Convert.ToString(Dt.Rows[i]["ProgrammeName"]);
                        dataString.SpecialisationsList.Add(objS);
                    }
                }
                return Return.returnHttp("200", dataString, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage getAvailableExamBlockList(AssignBlocks dataString)
        {
            try
            {
                //AssignBlocks element = new AssignBlocks();
                dataString.ExamBlockList = new List<AvailableExamBlock>();
                BALExamBlocks func = new BALExamBlocks();
                dataString.ExamBlockList = func.getAvailableExamBlockList(dataString.ProgrammePartTermId,dataString.ExamMasterId,dataString.SpecialisationId,dataString.PaperId,dataString.ExamVenueId);
                return Return.returnHttp("200", dataString, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        [HttpPost]
        public HttpResponseMessage AssignBlocks(AssignBlocks dataString)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            try
            {
                Int64 UserId = Convert.ToInt64(res.ToString());
                //Int64 UserId = 1;

                //if(dataString.SpecialisationsList!=null)
                //{


                //dataString.SpecialisationId = string.Join(",", dataString.SpecialisationsList.Where(p => p.IsSelected == true)
                //                 .Select(p => p.Id.ToString()));
                //}

                SqlCommand cmd = new SqlCommand("AssignBlocks", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammeId", dataString.ProgrammeId);
                cmd.Parameters.AddWithValue("@ProgrammePartTermId", dataString.ProgrammePartTermId);
                cmd.Parameters.AddWithValue("@ExamEventMasterId", dataString.ExamMasterId);
                cmd.Parameters.AddWithValue("@ExamVenueId", dataString.ExamVenueId);
                cmd.Parameters.AddWithValue("@SpecialisationId", dataString.SpecialisationId);
                cmd.Parameters.AddWithValue("@ExamVenueExamCenterId", dataString.ExamVenueExamCenterId);
                cmd.Parameters.AddWithValue("@PaperId", dataString.PaperId);
                cmd.Parameters.AddWithValue("@userId", UserId);
                cmd.Parameters.AddWithValue("@userTime", datetime);

                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been added successfully.";
                    return Return.returnHttp("200", strMessage.ToString(), null);
                }
                else if (string.Equals(strMessage, "FAIL"))
                {
                    strMessage = "Please select Exam Center";
                    return Return.returnHttp("201", strMessage.ToString(), null);
                }
                else
                {
                    return Return.returnHttp("201", strMessage.ToString(), null);
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }

        [HttpPost]
        public HttpResponseMessage ManualAssignBlocks(AssignBlocks dataString)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            try
            {
                Int64 UserId = Convert.ToInt64(res.ToString());
                //Int64 UserId = 1;

                //if(dataString.SpecialisationsList!=null)
                //{


                //dataString.SpecialisationId = string.Join(",", dataString.SpecialisationsList.Where(p => p.IsSelected == true)
                //                 .Select(p => p.Id.ToString()));
                //}

                SqlCommand cmd = new SqlCommand("ManualAssignBlocks", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProgrammeId", dataString.ProgrammeId);
                cmd.Parameters.AddWithValue("@ProgrammePartTermId", dataString.ProgrammePartTermId);
                cmd.Parameters.AddWithValue("@ExamEventMasterId", dataString.ExamMasterId);
                cmd.Parameters.AddWithValue("@ExamVenueId", dataString.ExamVenueId);
                cmd.Parameters.AddWithValue("@SpecialisationId", dataString.SpecialisationId);
                cmd.Parameters.AddWithValue("@PaperId", dataString.PaperId);
                cmd.Parameters.AddWithValue("@ExamBlockId", dataString.ExamBlockId);
                cmd.Parameters.AddWithValue("@ExamVenueExamCenterId", dataString.ExamVenueExamCenterId);
                cmd.Parameters.AddWithValue("@userId", UserId);
                cmd.Parameters.AddWithValue("@userTime", datetime);

                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been added successfully.";
                    return Return.returnHttp("200", strMessage.ToString(), null);
                }
                else if (string.Equals(strMessage, "FAIL"))
                {
                    strMessage = "Please select Exam Center";
                    return Return.returnHttp("201", strMessage.ToString(), null);
                }
                else
                {
                    return Return.returnHttp("201", strMessage.ToString(), null);
                }
                
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }

        [HttpPost]
        public HttpResponseMessage MstPaperListGetByProgPartTermIdAndSpeciliationId(AssignBlocks dataString)
        {
            try
            {
                BALPaper func = new BALPaper();
                dataString.PaperList = new List<MstPaper>();
                dataString.PaperList = func.GetPaperListBySpecialisationIdAndProgPartTermId(dataString.ProgrammePartTermId, dataString.SpecialisationId, dataString.ExamMasterId);
                return Return.returnHttp("200", dataString, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        //[HttpPost]
        //public HttpResponseMessage generateExamFormBarcode(ExamFormBarcodeList dataString)
        //{
        //    try
        //    {
        //        String token = Request.Headers.GetValues("token").FirstOrDefault();
        //        String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
        //        String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
        //        TokenOperation ac = new TokenOperation();
        //        string res = ac.ValidateToken(token);
        //        string resRoleToken = ac.ValidateRoleToken(userRoleToken);

        //        if (res == "0")
        //        {
        //            return Return.returnHttp("0", null, null);
        //        }
        //        else if (resRoleToken == "0")
        //        {
        //            return Return.returnHttp("0", null, null);
        //        }
        //        Int64 UserId = Convert.ToInt64(res.ToString());
        //        //Int64 UserId = 1;

        //        if (string.IsNullOrEmpty(dataString.ExamMasterId.ToString()))
        //        {
        //            return Return.returnHttp("201", "Please select ExamEvent ", null);
        //        }
        //        if (string.IsNullOrEmpty(dataString.strPaperId.ToString()))
        //        {
        //            return Return.returnHttp("201", "Please select paper ", null);
        //        }
        //        ExamFormBarcode func = new ExamFormBarcode();

        //        string output = func.generatePDF(dataString.ExamMasterId,dataString.strPaperId,1);

        //        if (string.IsNullOrEmpty(output))
        //        {
        //            return Return.returnHttp("200", " pdf generated successfully. ", null);
        //        }
        //        //else if (output.StartsWith("Error"))
        //        //{
        //        //    return Return.returnHttp("201", output, null);
        //        //}
        //        //else if (output == "No Record Found")
        //        //{
        //        //    return Return.returnHttp("200", "No Record Found", null);
        //        //}

        //        //else if (output.Contains('/'))
        //        //{
        //        //    return Return.returnHttp("200", output, null);
        //        //}

        //        else
        //        {
        //            return Return.returnHttp("200", output, null);
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        return Return.returnHttp("201", e.Message, null);
        //    }
        //}

        [HttpPost]
        public HttpResponseMessage saveExamFormBarcodePdfGenerationRequest(ExamFormBarcodePdfGenerationRequest dataString)
        {
            String token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation ac = new TokenOperation();

            string res = ac.ValidateToken(token);

            if (res == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            try
            {
                Int64 UserId = Convert.ToInt64(res.ToString());
                //Int64 UserId = 1;
                dataString.CreatedBy = UserId;
                BALExamFormBarcodePdfGenerationRequest func = new BALExamFormBarcodePdfGenerationRequest();
                string strMessage = func.saveExamFormBarcodePdfGenerationRequest(dataString);

                if (string.Equals(strMessage, "TRUE"))
                {
                    strMessage = "Your data has been added successfully.";
                }
                return Return.returnHttp("200", strMessage.ToString(), null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }

        }

        [HttpPost]
        public HttpResponseMessage getExamFormBarcodePdfgenRequestList()
        {
            try
            {
                List<ExamFormBarcodePdfGenerationRequest> objList = new List<ExamFormBarcodePdfGenerationRequest>();
                BALExamFormBarcodePdfGenerationRequest func = new BALExamFormBarcodePdfGenerationRequest();
                objList = func.GetDetailsOfExamFormBarcodePdfGenerationRequest();
                return Return.returnHttp("200", objList, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        //Start - Mohini's Code 23-Apr-2022============================

        #region ExamVenueGetforAssignBlocks
        [HttpPost]
        public HttpResponseMessage ExamVenueGetforAssignBlocks(ExamVenueGetforAssignBlocks EVA)
        {
            try
            {
                Int32 ExamVenueId = EVA.ExamVenueId;

                String token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("ExamVenueGetforAssignBlocks", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamVenueId", ExamVenueId);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable Dt = new DataTable();     
                Da.SelectCommand = cmd;
                Da.Fill(Dt);
                List<ExamVenueGetforAssignBlocks> ObjVC = new List<ExamVenueGetforAssignBlocks>();

                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {
                        ExamVenueGetforAssignBlocks ObjCenter = new ExamVenueGetforAssignBlocks();
                        ObjCenter.ExamVenueExamCenterId = Convert.ToInt32(dr["ExamVenueExamCenterId"]);
                        ObjCenter.CenterName = (dr["CenterName"].ToString());
                        ObjVC.Add(ObjCenter);
                    }

                }
                return Return.returnHttp("200", ObjVC, null);

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);

            }
        }
        #endregion


        //End - Mohini's Code 23-Apr-2022============================

    }
}