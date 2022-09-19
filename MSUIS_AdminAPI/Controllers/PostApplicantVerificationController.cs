using MSUIS_TokenManager.App_Start;
using MSUISApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Web.Http;

namespace MSUISApi.Controllers
{
    public class PostApplicantVerificationController : ApiController 
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

        //string DocURL = "https://admission.msubaroda.ac.in/MSUISApi/Upload/Documents/"; // Live
        string DocURL = "F:/inetpub/wwwroot/MSUISApi/Upload/Documents/"; 
        //string DocURL = "C:/inetpub/wwwroot/MSUISApi/Upload/Documents/"; 
        //string DocURL = "E:/Mona/"; 
        //string DocURL = "https://localhost:44374/MSUISApi/Upload/Documents/"; // D
        //string DocURL = "http://172.25.15.22/MSUISApi/Upload/Documents/"; // C

        #region UpdateDOBDocVerification
        [HttpPost]
        public HttpResponseMessage UpdateDOBDocVerification(JObject jsonobject)

        {
            PostApplicantVerification postappverification = new PostApplicantVerification();
            dynamic jsonData = jsonobject;

            try
            {
                postappverification.ApplicantRegId = jsonData.ApplicantRegId;
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                //postappverification.Id = Convert.ToInt32(responseToken);
                postappverification.IsDOBDocVerifiedEdit = Convert.ToBoolean(jsonData.IsDOBDocVerified);
                postappverification.ApprovedByFaculty = Convert.ToInt32(responseToken);
                //int UserId = Convert.ToInt32(jsonData.ModifiedBy);

                SqlCommand cmd = new SqlCommand("PostAdmRegiDOBDocVerified", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ApplicantRegId", postappverification.ApplicantRegId);
                cmd.Parameters.AddWithValue("@IsDOBDocVerified", postappverification.IsDOBDocVerifiedEdit);
                cmd.Parameters.AddWithValue("@ApprovedByFaculty", postappverification.ApprovedByFaculty);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                return Return.returnHttp("200", strMessage, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion

        #region UpdatePhotoIDVerification
        [HttpPost]
        public HttpResponseMessage UpdatePhotoIDVerification(JObject jsonobject)

        {
            PostApplicantVerification postappverification = new PostApplicantVerification();
            dynamic jsonData = jsonobject;

            try
            {
                postappverification.ApplicantRegId = jsonData.ApplicantRegId;
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                //postappverification.Id = Convert.ToInt32(responseToken);
                postappverification.IsPhotoIdDocVerifiedEdit = Convert.ToBoolean(jsonData.IsPhotoIdDocVerified);
                postappverification.ApprovedByFaculty = Convert.ToInt32(responseToken);
                //int UserId = Convert.ToInt32(jsonData.ModifiedBy);

                SqlCommand cmd = new SqlCommand("PostAdmRegiPhotoIDVerified", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ApplicantRegId", postappverification.ApplicantRegId);
                cmd.Parameters.AddWithValue("@IsPhotoIdDocVerified", postappverification.IsPhotoIdDocVerifiedEdit);
                cmd.Parameters.AddWithValue("@ApprovedByFaculty", postappverification.ApprovedByFaculty);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                return Return.returnHttp("200", strMessage, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion

        #region UpdateAadharDocVerification
        [HttpPost]
        public HttpResponseMessage UpdateAadharDocVerification(JObject jsonobject)

        {
            PostApplicantVerification postappverification = new PostApplicantVerification();
            dynamic jsonData = jsonobject;

            try
            {
                postappverification.ApplicantRegId = jsonData.ApplicantRegId;
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                //postappverification.Id = Convert.ToInt32(responseToken);
                postappverification.IsAadharDocVerifiedEdit = Convert.ToBoolean(jsonData.IsAadharDocVerified);
                postappverification.ApprovedByFaculty = Convert.ToInt32(responseToken);
                //int UserId = Convert.ToInt32(jsonData.ModifiedBy);

                SqlCommand cmd = new SqlCommand("PostAdmRegiAadharVerified", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ApplicantRegId", postappverification.ApplicantRegId);
                cmd.Parameters.AddWithValue("@IsAadharDocVerified", postappverification.IsAadharDocVerifiedEdit);
                cmd.Parameters.AddWithValue("@ApprovedByFaculty", postappverification.ApprovedByFaculty);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                return Return.returnHttp("200", strMessage, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion

        #region UpdateSocialCategoryDocVerification
        [HttpPost]
        public HttpResponseMessage UpdateSocialCategoryDocVerification(JObject jsonobject)

        {
            PostApplicantVerification postappverification = new PostApplicantVerification();
            dynamic jsonData = jsonobject;

            try
            {
                postappverification.ApplicantRegId = jsonData.ApplicantRegId;
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                //postappverification.Id = Convert.ToInt32(responseToken);
                postappverification.IsSocialCategoryDocVerifiedEdit = Convert.ToBoolean(jsonData.IsSocialCategoryDocVerified);
                postappverification.ApprovedByFaculty = Convert.ToInt32(responseToken);
                //int UserId = Convert.ToInt32(jsonData.ModifiedBy);

                SqlCommand cmd = new SqlCommand("PostAdmRegiSocialCategoryVerified", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ApplicantRegId", postappverification.ApplicantRegId);
                cmd.Parameters.AddWithValue("@IsSocialCategoryDocVerified", postappverification.IsSocialCategoryDocVerifiedEdit);
                cmd.Parameters.AddWithValue("@ApprovedByFaculty", postappverification.ApprovedByFaculty);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                return Return.returnHttp("200", strMessage, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion

        #region UpdateReservationCategoryDocVerification
        [HttpPost]
        public HttpResponseMessage UpdateReservationCategoryDocVerification(JObject jsonobject)

        {
            PostApplicantVerification postappverification = new PostApplicantVerification();
            dynamic jsonData = jsonobject;

            try
            {
                postappverification.ApplicantRegId = jsonData.ApplicantRegId;
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                //postappverification.Id = Convert.ToInt32(responseToken);
                postappverification.IsReservationCategoryDocVerifiedEdit = Convert.ToBoolean(jsonData.IsReservationCategoryDocVerified);
                postappverification.ApprovedByFaculty = Convert.ToInt32(responseToken);
                //int UserId = Convert.ToInt32(jsonData.ModifiedBy);

                SqlCommand cmd = new SqlCommand("PostAdmRegiReservationCategoryVerified", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ApplicantRegId", postappverification.ApplicantRegId);
                cmd.Parameters.AddWithValue("@IsReservationCategoryDocVerified", postappverification.IsReservationCategoryDocVerifiedEdit);
                cmd.Parameters.AddWithValue("@ApprovedByFaculty", postappverification.ApprovedByFaculty);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                return Return.returnHttp("200", strMessage, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion

        #region UpdateEWSVerification
        [HttpPost]
        public HttpResponseMessage UpdateEWSVerification(JObject jsonobject)

        {
            PostApplicantVerification postappverification = new PostApplicantVerification();
            dynamic jsonData = jsonobject;

            try
            {
                postappverification.ApplicantRegId = jsonData.ApplicantRegId;
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                //postappverification.Id = Convert.ToInt32(responseToken);
                postappverification.IsEWSDocVerifiedEdit = Convert.ToBoolean(jsonData.IsEWSDocVerified);
                postappverification.ApprovedByFaculty = Convert.ToInt32(responseToken);
                //int UserId = Convert.ToInt32(jsonData.ModifiedBy);

                SqlCommand cmd = new SqlCommand("PostAdmRegiEWSVerified", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ApplicantRegId", postappverification.ApplicantRegId);
                cmd.Parameters.AddWithValue("@IsEWSDocVerified", postappverification.IsEWSDocVerifiedEdit);
                cmd.Parameters.AddWithValue("@ApprovedByFaculty", postappverification.ApprovedByFaculty);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                return Return.returnHttp("200", strMessage, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion

        #region UpdatePCDocVerification
        [HttpPost]
        public HttpResponseMessage UpdatePCDocVerification(JObject jsonobject)

        {
            PostApplicantVerification postappverification = new PostApplicantVerification();
            dynamic jsonData = jsonobject;

            try
            {
                postappverification.ApplicantRegId = jsonData.ApplicantRegId;
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                //postappverification.Id = Convert.ToInt32(responseToken);
                postappverification.IsPCDocVerifiedEdit = Convert.ToBoolean(jsonData.IsPCDocVerified);
                postappverification.ApprovedByFaculty = Convert.ToInt32(responseToken);
                //int UserId = Convert.ToInt32(jsonData.ModifiedBy);

                SqlCommand cmd = new SqlCommand("PostAdmRegiPCDocVerified", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ApplicantRegId", postappverification.ApplicantRegId);
                cmd.Parameters.AddWithValue("@IsPCDocVerified", postappverification.IsPCDocVerifiedEdit);
                cmd.Parameters.AddWithValue("@ApprovedByFaculty", postappverification.ApprovedByFaculty);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                return Return.returnHttp("200", strMessage, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion

        #region UpdateApplicantPhotoVerification
        [HttpPost]
        public HttpResponseMessage UpdateApplicantPhotoVerification(JObject jsonobject)

        {
            PostApplicantVerification postappverification = new PostApplicantVerification();
            dynamic jsonData = jsonobject;

            try
            {
                postappverification.ApplicantRegId = jsonData.ApplicantRegId;
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                //postappverification.Id = Convert.ToInt32(responseToken);
                postappverification.IsApplicantPhotoVerifiedEdit = Convert.ToBoolean(jsonData.IsApplicantPhotoVerified);
                postappverification.ApprovedByFaculty = Convert.ToInt32(responseToken);
                //int UserId = Convert.ToInt32(jsonData.ModifiedBy);

                SqlCommand cmd = new SqlCommand("PostAdmRegiApplicantPhotoVerified", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ApplicantRegId", postappverification.ApplicantRegId);
                cmd.Parameters.AddWithValue("@IsApplicantPhotoVerified", postappverification.IsApplicantPhotoVerifiedEdit);
                cmd.Parameters.AddWithValue("@ApprovedByFaculty", postappverification.ApprovedByFaculty);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                return Return.returnHttp("200", strMessage, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion

        #region UpdateApplicantSignatureVerification
        [HttpPost]
        public HttpResponseMessage UpdateApplicantSignatureVerification(JObject jsonobject)

        {
            PostApplicantVerification postappverification = new PostApplicantVerification();
            dynamic jsonData = jsonobject;

            try
            {
                postappverification.ApplicantRegId = jsonData.ApplicantRegId;
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                //postappverification.Id = Convert.ToInt32(responseToken);
                postappverification.IsApplicantSignatureVerifiedEdit = Convert.ToBoolean(jsonData.IsApplicantSignatureVerified);
                postappverification.ApprovedByFaculty = Convert.ToInt32(responseToken);
                //int UserId = Convert.ToInt32(jsonData.ModifiedBy);

                SqlCommand cmd = new SqlCommand("PostAdmRegiApplicantSignatureVerified", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ApplicantRegId", postappverification.ApplicantRegId);
                cmd.Parameters.AddWithValue("@IsApplicantSignatureVerified", postappverification.IsApplicantSignatureVerifiedEdit);
                cmd.Parameters.AddWithValue("@ApprovedByFaculty", postappverification.ApprovedByFaculty);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                return Return.returnHttp("200", strMessage, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion

        #region UpdateAdmApplicationCategoryRemarks
        [HttpPost]
        public HttpResponseMessage UpdateAdmApplicationCategoryRemarks(JObject jsonobject)

        {
            PostApplicantVerification postappverification = new PostApplicantVerification();
            dynamic jsonData = jsonobject;

            try
            {
                postappverification.Id = jsonData.Id;

                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                postappverification.MainCategoryRemarks = jsonData.MainCategoryRemarks;
                //postappverification.AppliedCategoryRemarks = jsonData.AppliedCategoryRemarks;
                postappverification.ApprovedByFaculty = Convert.ToInt32(responseToken);
                //int UserId = Convert.ToInt32(jsonData.ModifiedBy);

                SqlCommand cmd = new SqlCommand("PostAdmApplicationCategoryRemarks", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", postappverification.Id);
                cmd.Parameters.AddWithValue("@MainCategoryRemarks", postappverification.MainCategoryRemarks);
                //cmd.Parameters.AddWithValue("@AppliedCategoryRemarks", postappverification.AppliedCategoryRemarks);
                cmd.Parameters.AddWithValue("@ApprovedByFaculty", postappverification.ApprovedByFaculty);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                return Return.returnHttp("200", strMessage, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }


        #endregion

        #region UpdateAdmApplicationEduStatus
        [HttpPost]
        public HttpResponseMessage UpdateAdmApplicationEduStatus(JObject jsonobject)

        {
            PostApplicantVerification postappverification = new PostApplicantVerification();
            dynamic jsonData = jsonobject;

            try
            {
                postappverification.Id = jsonData.Id;
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                //postappverification.Id = Convert.ToInt32(responseToken);
                postappverification.EduVerificationEdit = Convert.ToBoolean(jsonData.EduVerification);
                postappverification.ApprovedByFaculty = Convert.ToInt32(responseToken);
                //int UserId = Convert.ToInt32(jsonData.ModifiedBy);

                SqlCommand cmd = new SqlCommand("PostAdmApplicationEditEduStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", postappverification.Id);
                cmd.Parameters.AddWithValue("@EduVerification", postappverification.EduVerificationEdit);
                cmd.Parameters.AddWithValue("@ApprovedByFaculty", postappverification.ApprovedByFaculty);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                return Return.returnHttp("200", strMessage, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion

        #region UpdateAdmApplicationEduStatusByID
        [HttpPost]
        public HttpResponseMessage UpdateAdmApplicationEduStatusByID(JObject jsonobject)

        {
            PostApplicantVerification postappverification = new PostApplicantVerification();
            dynamic jsonData = jsonobject;

            try
            {
                postappverification.Id = Convert.ToInt64(jsonData.ObjEduId);
                postappverification.ApplicantRegId = jsonData.RegId;
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                //postappverification.Id = Convert.ToInt32(responseToken);
                postappverification.IsVerified = Convert.ToBoolean(jsonData.IsVerified);
                postappverification.ApprovedByFaculty = Convert.ToInt32(responseToken);
                //int UserId = Convert.ToInt32(jsonData.ModifiedBy);

                SqlCommand cmd = new SqlCommand("PostAdmApplicationEduStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", postappverification.Id);
                cmd.Parameters.AddWithValue("@ApplicantRegId", postappverification.ApplicantRegId);
                cmd.Parameters.AddWithValue("@IsVerified", postappverification.IsVerified);
                cmd.Parameters.AddWithValue("@ApprovedByFaculty", postappverification.ApprovedByFaculty);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                return Return.returnHttp("200", strMessage, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion

        #region UpdateAdmApplicantSubmittedDocStatus
        [HttpPost]
        public HttpResponseMessage UpdateAdmApplicantSubmittedDocStatus(JObject jsonobject)

        {
            PostApplicantVerification postappverification = new PostApplicantVerification();
            dynamic jsonData = jsonobject;

            try
            {
                postappverification.Id = Convert.ToInt64(jsonData.SubDocId);
                postappverification.AdmissionApplicationId = jsonData.AdmissionApplicationId;
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                //postappverification.Id = Convert.ToInt32(responseToken);
                postappverification.IsVerified = Convert.ToBoolean(jsonData.IsVerified);
                postappverification.ApprovedByFaculty = Convert.ToInt32(responseToken);
                //int UserId = Convert.ToInt32(jsonData.ModifiedBy);

                SqlCommand cmd = new SqlCommand("PostAdmApplicantSubmittedDocStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", postappverification.Id);
                cmd.Parameters.AddWithValue("@AdmissionApplicationId", postappverification.AdmissionApplicationId);
                cmd.Parameters.AddWithValue("@IsVerified", postappverification.IsVerified);
                cmd.Parameters.AddWithValue("@ApprovedByFaculty", postappverification.ApprovedByFaculty);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                return Return.returnHttp("200", strMessage, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion

        #region UpdatePostAdmStudentAddOnInfoStatus
        [HttpPost]
        public HttpResponseMessage UpdatePostAdmStudentAddOnInfoStatus(JObject jsonobject)

        {
            PostApplicantVerification postappverification = new PostApplicantVerification();
            dynamic jsonData = jsonobject;

            try
            {
                postappverification.Id = Convert.ToInt64(jsonData.AddOnId);
                postappverification.ApplicationId = jsonData.ApplicationId;
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                //postappverification.Id = Convert.ToInt32(responseToken);
                postappverification.IsVerified = Convert.ToBoolean(jsonData.IsVerified);
                postappverification.ApprovedByFaculty = Convert.ToInt32(responseToken);
                //int UserId = Convert.ToInt32(jsonData.ModifiedBy);

                SqlCommand cmd = new SqlCommand("PostAdmStudentAddOnInfoStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", postappverification.Id);
                cmd.Parameters.AddWithValue("@ApplicationId", postappverification.ApplicationId);
                cmd.Parameters.AddWithValue("@IsVerified", postappverification.IsVerified);
                cmd.Parameters.AddWithValue("@ApprovedByFaculty", postappverification.ApprovedByFaculty);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                return Return.returnHttp("200", strMessage, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion

        #region UpdateAdmApplicantAdditionalDocStatus
        [HttpPost]
        public HttpResponseMessage UpdateAdmApplicantAdditionalDocStatus(JObject jsonobject)

        {
            PostApplicantVerification postappverification = new PostApplicantVerification();
            dynamic jsonData = jsonobject;

            try
            {
                postappverification.Id = Convert.ToInt64(jsonData.AdditionalDocId);
                postappverification.AdmApplicationId = jsonData.AdmApplicationId;
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                //postappverification.Id = Convert.ToInt32(responseToken);
                postappverification.IsVerified = Convert.ToBoolean(jsonData.IsVerified);
                postappverification.ApprovedByFaculty = Convert.ToInt32(responseToken);
                //int UserId = Convert.ToInt32(jsonData.ModifiedBy);

                SqlCommand cmd = new SqlCommand("PostAdmApplicantAdditionalDocStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", postappverification.Id);
                cmd.Parameters.AddWithValue("@AdmApplicationId", postappverification.AdmApplicationId);
                cmd.Parameters.AddWithValue("@IsVerified", postappverification.IsVerified);
                cmd.Parameters.AddWithValue("@ApprovedByFaculty", postappverification.ApprovedByFaculty);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                return Return.returnHttp("200", strMessage, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion

        #region UpdateMainSubmitButtonforFacultyVerification
        [HttpPost]
        public HttpResponseMessage UpdateFacultyMainSubmit(JObject jsonobject)

        {
            PostApplicantVerification postappverification = new PostApplicantVerification();
            dynamic jsonData = jsonobject;

            try
            {
                postappverification.Id = jsonData.Id;
                postappverification.ApplicantRegId = jsonData.ApplicantRegId;
                postappverification.ProgrammeInstancePartTermId = jsonData.ProgrammeInstancePartTermId;
                postappverification.VerificationStatus = jsonData.VerificationStatus;
                postappverification.AcademicYearId = jsonData.AcademicYearId;
                //postappverification.IsDualAdmission = Convert.ToBoolean(jsonData.IsDualAdmission);


                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("PostAdmApplicationFacultyMainSubmit", con);
                cmd.CommandType = CommandType.StoredProcedure;
                postappverification.VerifiedByFaculty = Convert.ToInt32(responseToken);


                cmd.Parameters.AddWithValue("@Id", postappverification.Id);
                cmd.Parameters.AddWithValue("@ApplicantRegId", postappverification.ApplicantRegId);
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", postappverification.ProgrammeInstancePartTermId);
                cmd.Parameters.AddWithValue("@VerifiedByFaculty", postappverification.VerifiedByFaculty);
                cmd.Parameters.AddWithValue("@VerificationStatus", postappverification.VerificationStatus);
                cmd.Parameters.AddWithValue("@AcademicYearId", postappverification.AcademicYearId);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;


                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                //if(strMessage == "Verification Status for this Applicant has been Not Approved in Pre-Verification Process.")
                //{
                //    return Return.returnHttp("201", strMessage, null);
                //}
                //else if (strMessage == "Already Available.")
                //{
                //    return Return.returnHttp("201", strMessage, null);
                //}
                //else
                //{
                    //return Return.returnHttp("200", strMessage, null);
                //}

                return Return.returnHttp("200", strMessage, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion

        #region UpdateAdmApplicationAdminRemarksByFaculty
        [HttpPost]
        public HttpResponseMessage UpdateAdmApplicationAdminRemarksByFaculty(JObject jsonobject)

        {
            PostApplicantVerification postappverification = new PostApplicantVerification();
            dynamic jsonData = jsonobject;

            try
            {
                postappverification.Id = jsonData.Id;
                postappverification.ApplicantRegId = jsonData.ApplicantRegId;
                postappverification.FirstName = jsonData.FirstName;
                postappverification.MiddleName = jsonData.MiddleName;
                postappverification.LastName = jsonData.LastName;
                postappverification.MobileNo = jsonData.MobileNo;
                postappverification.EmailId = jsonData.EmailId;
                postappverification.InstPartTerm = Convert.ToString(jsonData.InstPartTerm);
                postappverification.ProgrammeName = jsonData.ProgrammeName;
                postappverification.BranchName = jsonData.BranchName;
                postappverification.AcademicYearCode = jsonData.AcademicYearCode;
                postappverification.EmailKeyName = jsonData.EmailKeyName;
                postappverification.ProgrammeInstancePartTermId = jsonData.ProgrammeInstancePartTermId;
                postappverification.EligibilityStatus = jsonData.EligibilityStatus;
                postappverification.AdminRemarkByFaculty = jsonData.AdminRemarkByFaculty;
                postappverification.PendingDocList = jsonData.PendingDocList;

                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }


                postappverification.ApprovedByFaculty = Convert.ToInt32(responseToken);

                SqlCommand cmd = new SqlCommand("PostAdmApplicationAdminRemarksByFaculty", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", postappverification.Id);
                cmd.Parameters.AddWithValue("@ApplicantRegId", postappverification.ApplicantRegId);
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", postappverification.ProgrammeInstancePartTermId);

                cmd.Parameters.AddWithValue("@EligibilityStatus", postappverification.EligibilityStatus);
                cmd.Parameters.AddWithValue("@AdminRemarkByFaculty", postappverification.AdminRemarkByFaculty);
                cmd.Parameters.AddWithValue("@ApprovedByFaculty", postappverification.ApprovedByFaculty);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;


                if (postappverification.EligibilityStatus == "Not_Approved" || postappverification.EligibilityStatus == "Pending")
                {
                    //postappverification.AdminRemarkByFaculty = jsonData.AdminRemarkByFaculty;
                    //cmd.Parameters.AddWithValue("@AdminRemarkByFaculty", postappverification.AdminRemarkByFaculty);

                    //cmd.Parameters.AddWithValue("@Flag", "Not_Approved");

                    if (postappverification.EligibilityStatus == "Not_Approved")
                    {
                        cmd.Parameters.AddWithValue("@Flag", "Not_Approved");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Flag", "Pending");
                    }

                }

                else if ((postappverification.EligibilityStatus == "Provisionally_Approved") && (jsonData.PreferenceGroupId != null))
                {

                    postappverification.FeeCategoryPartTermMapId = jsonData.FeeCategoryPartTermMapId;
                    postappverification.InstituteId = jsonData.InstituteId;
                    postappverification.PreferenceGroupId = jsonData.PreferenceGroupId;
                    postappverification.DestProgID = jsonData.DestProgID;
                    //postappverification.IsDualAdmission = Convert.ToBoolean(jsonData.IsDualAdmission);


                    cmd.Parameters.AddWithValue("@FeeCategoryPartTermMapId", postappverification.FeeCategoryPartTermMapId);
                    cmd.Parameters.AddWithValue("@AdmittedInstituteId", postappverification.InstituteId);
                    cmd.Parameters.AddWithValue("@PreferenceGroupId", postappverification.PreferenceGroupId);
                    cmd.Parameters.AddWithValue("@DestProgID", postappverification.DestProgID);
                    //cmd.Parameters.AddWithValue("@IsDualAdmission", postappverification.IsDualAdmission);
                    cmd.Parameters.AddWithValue("@Flag", "Provisionally_Approved");
                }

                else if ((postappverification.EligibilityStatus == "Provisionally_Approved") && (jsonData.PreferenceGroupId == null))
                {

                    postappverification.FeeCategoryPartTermMapId = jsonData.FeeCategoryPartTermMapId;
                    postappverification.InstituteId = jsonData.InstituteId;
                    postappverification.DestProgID = jsonData.DestProgID;
                    //postappverification.PreferenceGroupId = jsonData.PreferenceGroupId;
                    //postappverification.IsDualAdmission = Convert.ToBoolean(jsonData.IsDualAdmission);


                    cmd.Parameters.AddWithValue("@FeeCategoryPartTermMapId", postappverification.FeeCategoryPartTermMapId);
                    cmd.Parameters.AddWithValue("@AdmittedInstituteId", postappverification.InstituteId);
                    cmd.Parameters.AddWithValue("@DestProgID", postappverification.DestProgID);
                    //cmd.Parameters.AddWithValue("@PreferenceGroupId", postappverification.PreferenceGroupId);
                    //cmd.Parameters.AddWithValue("@IsDualAdmission", postappverification.IsDualAdmission);
                    cmd.Parameters.AddWithValue("@Flag", "Provisionally_Approved_No_Pref");
                }

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();
                //return Return.returnHttp("200", strMessage, null);

                // *************Start SMS & Email***************
                //if (strMessage == "Student Application Verified Successfully.")
                //{
                //    #region SMS and Email for Applicant Verification By Faculty
                //    try
                //    {
                //        //Your user name
                //        string user = "shaurya";
                //        ////Your authentication key
                //        string key = "ec07fd3102XX";
                //        ////Multiple mobiles numbers separated by comma
                //        string mobile = postappverification.MobileNo.Trim();
                //        ////Sender ID,While using route4 sender id should be 6 characters long.
                //        string senderid = "MSUBIS";
                //        ////Your message to send, Add URL encoding here.
                //        ////string message = System.Web.HttpUtility.UrlEncode("your OTP for MSU Wi-Fi Registration : " + OTP);
                //        //string message = "Dear Applicant, MSUIS login credentials is Username " + postappverification.Id + " and Password " + postappverification.Id + ". Please do not share this information to anyone.\n- Team MSUB";
                //        string message = "Dear, Your Application form no." + postappverification.Id + " has been successfully verified in " + postappverification.ProgrammeName + "-" + postappverification.BranchName + "-" + postappverification.AcademicYearCode + ". Regards,\nMSUB";


                //        //Prepare you post parameters
                //        StringBuilder sbPostData = new StringBuilder();
                //        sbPostData.AppendFormat("user={0}", user);
                //        sbPostData.AppendFormat("&password={0}", key);

                //        sbPostData.AppendFormat("&mobiles={0}", mobile);
                //        sbPostData.AppendFormat("&sms={0}", message);
                //        sbPostData.AppendFormat("&senderid={0}", senderid);
                //        sbPostData.AppendFormat("&entityid={0}", "1201159618592707491");
                //        //sbPostData.AppendFormat("&tempid={0}", "1207161795812832107");
                //        sbPostData.AppendFormat("&tempid={0}", "1207161908607186569");
                //        sbPostData.AppendFormat("&accusage={0}", "1");


                //        //Call Send SMS API
                //        string sendSMSUri = "http://shaurya.mysmsapps.co.in/sendsms.jsp?";
                //        //Create HTTPWebrequest
                //        HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                //        //Prepare and Add URL Encoded data
                //        UTF8Encoding encoding = new UTF8Encoding();
                //        byte[] data = encoding.GetBytes(sbPostData.ToString());
                //        //Specify post method
                //        httpWReq.Method = "POST";
                //        httpWReq.ContentType = "application/x-www-form-urlencoded";
                //        httpWReq.ContentLength = data.Length;
                //        using (Stream stream = httpWReq.GetRequestStream())
                //        {
                //            stream.Write(data, 0, data.Length);
                //        }
                //        //Get the response
                //        HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                //        StreamReader reader = new StreamReader(response.GetResponseStream());
                //        string responseString = reader.ReadToEnd();

                //        //Close the response
                //        reader.Close();
                //        response.Close();

                //        #region Email for Applicant Verification By Faculty
                //        try
                //        {
                //            SmtpClient smtpClient = new SmtpClient();
                //            MailMessage MailMessage = new MailMessage();
                //            //dynamic Jsondata = jObject;

                //            string ToEmail = Convert.ToString(postappverification.EmailId);

                //            MailAddress fromAddress = new MailAddress(postappverification.EmailKeyName, "MSUIS - Verification Status");

                //            smtpClient.Host = "smtp.gmail.com";

                //            //Default port will be 25
                //            smtpClient.Port = 587;

                //            //From address will be given as a MailAddress Object
                //            MailMessage.From = fromAddress;
                //            MailMessage.To.Add(ToEmail);

                //            // To address collection of MailAddress
                //            MailMessage.Bcc.Add(postappverification.EmailKeyName);
                //            MailMessage.Subject = "MSUIS - Verification Process";
                //            MailMessage.IsBodyHtml = true;
                //            // Message body content
                //            MailMessage.Body = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>" +
                //            "<html xmlns ='http://www.w3.org/1999/xhtml'>" +
                //            "<head>" +
                //            "<meta http-equiv='Content-Type' content ='text/html; charset=UTF-8' />" +
                //            "<meta name='viewport' content='width=device-width' />" +
                //            "<link rel='icon' href='img/favicon.ico' type='image/x-icon'>" +
                //            "<style type='text/css'>" +
                //            "@media only screen and(max - width: 550px), screen and(max-device - width: 550px) {" +
                //            "body[yahoo].buttonwrapper { background - color: transparent!important; }" +
                //            "body[yahoo].button { padding: 0!important; }" +
                //            "body[yahoo].button a {" +
                //            "background - color: #9b59b6; padding: 15px 25px !important; }}" +
                //            "@media only screen and(min-device - width: 601px) {" +
                //            ".content { width: 600px!important; }" +
                //            ".col387 { width: 387px!important; }}" +
                //            "</style>" +
                //            "</head>" +
                //            "<body bgcolor='#34495E' style='margin: 0; padding: 0;' yahoo='fix'>" +
                //            "<table align='center' border='0' cellpadding='0' cellspacing='0' style='border-collapse: collapse; width: 100%;' class='content'>" +
                //            "<tr>" +
                //            "<td style='padding: 15px 10px 15px 10px;'>" +
                //            "</td>" +
                //            "</tr>" +
                //            "<tr>" +
                //            "<td align='center' bgcolor='#064E89' style='padding: 20px 20px 20px 20px; color: #ffffff; font-family: Arial, sans-serif; font-size: 36px; font-weight: bold; height:100px;'>" +
                //            "THE MAHARAJA SAYAJIRAO UNIVERSITY OF BARODA" +
                //            /*"<img src='https://localhost:44374/img/msuLogo.png' alt='MSU Logo' width='152' height='152' style='display:block;' />" +*/
                //            "</td>" +
                //            "</tr>" +
                //            "<tr>" +
                //            "</tr>" +
                //            "<tr>" +
                //            "<td align = 'left' bgcolor = '#ffffff' style = 'padding: 40px 20px 40px 20px; color: #555555; font-family: Arial, sans-serif; font-size: 20px; line-height: 30px; border-bottom: 1px solid #f6f6f6;' >" +
                //            "Dear " + postappverification.FirstName + "," +
                //            "<br/><br/><div>Your Application form no. <b>" + postappverification.Id + "</b> has been successfully verified in <b>" + postappverification.ProgrammeName + "-" + postappverification.BranchName + "-" + postappverification.AcademicYearCode + "</b>. </div>" +
                //            "<br/><div>Please Pay admission fee for verified course to confirm your admission.</div>" +
                //            "<br/><div><ul><b>Following are the steps for fee payment :</b>" +
                //            "<li>To check Admission fee Start date and End date please <a href='https://admission.msubaroda.ac.in/applicant/#/application-programme-list'>click here.</a></li>" +
                //            "<li>Open Application portal (<u style='color: blue;'>https://admission.msubaroda.ac.in/applicant/#/studentlogin</u>) and use your own login credentials.</li>" +
                //            "<li>Go to dashboard and click on <b>Pay Fee</b> option.</li>" +
                //            "<li>After the payment of admission fee, your admission will be provisionally approved for particular course.</li>" +
                //            "</ul></div>" +

                //            "<br/><br/><br/>Thank You." +
                //            "<br/>MSUB Team" +
                //            "</td>" +//
                //            "</tr> " +
                //            "<tr>" +
                //            "<td align = 'center' bgcolor= '#dddddd' style= 'padding: 15px 10px 15px 10px; color: #555555; font-family: Arial, sans-serif; font-size: 12px; line-height: 18px;'>" +
                //            "<b> THE MAHARAJA SAYAJIRAO UNIVERSITY OF BARODA</b><br/>Pratapgunj, Vadodara, Gujarat 390002" +
                //            "</td>" +
                //            "</tr>" +
                //            "</table>" +
                //            "</body>" +
                //            "</html>";

                //            System.Net.NetworkCredential myMailCredential = new System.Net.NetworkCredential();
                //            myMailCredential.UserName = postappverification.EmailKeyName;
                //            myMailCredential.Password = "msu@#$123";
                //            smtpClient.UseDefaultCredentials = false;
                //            smtpClient.EnableSsl = true;
                //            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                //            smtpClient.Timeout = 20000;
                //            smtpClient.Credentials = myMailCredential;

                //            // Send SMTP mail
                //            string result = string.Empty;
                //            smtpClient.Send(MailMessage);

                //            return Return.returnHttp("200", "Application Verified for Student. SMS and Mail has been successfully sent.", null);
                //        }
                //        catch (Exception e)
                //        {
                //            return Return.returnHttp("201", e.Message.ToString(), null);
                //        }
                //        #endregion
                //        return Return.returnHttp("200", "SMS Sent successfully", null);
                //    }
                //    catch (Exception e)
                //    {
                //        return Return.returnHttp("201", e.Message.ToString(), null);
                //    }
                //    #endregion

                //}

                if (postappverification.EligibilityStatus == "Pending" && strMessage == "Note: Student Application Verification is under process. Your Remarks saved successfully.")
                {
                    #region SMS and Email for Applicant Verification By Faculty (Pending)
                    try
                    {
                        //Your user name
                        string user = "shaurya";
                        ////Your authentication key
                        string key = "ec07fd3102XX";
                        ////Multiple mobiles numbers separated by comma
                        string mobile = postappverification.MobileNo.Trim();
                        ////Sender ID,While using route4 sender id should be 6 characters long.
                        string senderid = "MSUBIS";
                        ////Your message to send, Add URL encoding here.
                        ////string message = System.Web.HttpUtility.UrlEncode("your OTP for MSU Wi-Fi Registration : " + OTP);
                        //string message = "Dear Applicant, MSUIS login credentials is Username " + postappverification.Id + " and Password " + postappverification.Id + ". Please do not share this information to anyone.\n- Team MSUB";
                        //string message = "[MSUB Verification]: Dear, few of your documents are pending, kindly update in at. URL: https://admission.msubaroda.ac.in/. Regards,\nMSUB";
                        String message = "[MSUB Verification]: Dear, few of your documents are pending, kindly update at earliest. URL: https://admission.msubaroda.ac.in/ Regards, MSUB";

                        //Prepare you post parameters
                        StringBuilder sbPostData = new StringBuilder();
                        sbPostData.AppendFormat("user={0}", user);
                        sbPostData.AppendFormat("&password={0}", key);

                        sbPostData.AppendFormat("&mobiles={0}", mobile);
                        sbPostData.AppendFormat("&sms={0}", message);
                        sbPostData.AppendFormat("&senderid={0}", senderid);
                        sbPostData.AppendFormat("&entityid={0}", "1201159618592707491");
                        //sbPostData.AppendFormat("&tempid={0}", "1207161795812832107");
                        //sbPostData.AppendFormat("&tempid={0}", "1207161908607186569");
                        sbPostData.AppendFormat("&tempid={0}", "1207162676783608431");
                        sbPostData.AppendFormat("&accusage={0}", "1");


                        //Call Send SMS API
                        string sendSMSUri = "http://shaurya.mysmsapps.co.in/sendsms.jsp?";
                        //Create HTTPWebrequest
                        HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                        //Prepare and Add URL Encoded data
                        UTF8Encoding encoding = new UTF8Encoding();
                        byte[] data = encoding.GetBytes(sbPostData.ToString());
                        //Specify post method
                        httpWReq.Method = "POST";
                        httpWReq.ContentType = "application/x-www-form-urlencoded";
                        httpWReq.ContentLength = data.Length;
                        using (Stream stream = httpWReq.GetRequestStream())
                        {
                            stream.Write(data, 0, data.Length);
                        }
                        //Get the response
                        HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                        StreamReader reader = new StreamReader(response.GetResponseStream());
                        string responseString = reader.ReadToEnd();

                        //Close the response
                        reader.Close();
                        response.Close();

                        #region Email for Applicant Verification By Faculty (Pending)
                        try
                        {
                            SmtpClient smtpClient = new SmtpClient();
                            MailMessage MailMessage = new MailMessage();
                            //dynamic Jsondata = jObject;

                            string ToEmail = Convert.ToString(postappverification.EmailId);

                            MailAddress fromAddress = new MailAddress(postappverification.EmailKeyName, "MSUIS - Verification Status");

                            smtpClient.Host = "smtp.gmail.com";

                            //Default port will be 25
                            smtpClient.Port = 587;

                            //From address will be given as a MailAddress Object
                            MailMessage.From = fromAddress;
                            MailMessage.To.Add(ToEmail);

                            // To address collection of MailAddress
                            MailMessage.Bcc.Add(postappverification.EmailKeyName);
                            MailMessage.Subject = "MSUIS - Verification Process";
                            MailMessage.IsBodyHtml = true;
                            // Message body content

                            string s = postappverification.PendingDocList;

                            string[] subs = s.Split('-');
                            string PendingDoc = "";
                            for (int i = 1; i < subs.Length; i++)
                            {
                                subs[i] = subs[i].Trim();
                                PendingDoc += "<li>" + subs[i] + "</li>";

                            }
                            MailMessage.Body = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>" +
                            "<html xmlns ='http://www.w3.org/1999/xhtml'>" +
                            "<head>" +
                            "<meta http-equiv='Content-Type' content ='text/html; charset=UTF-8' />" +
                            "<meta name='viewport' content='width=device-width' />" +
                            "<link rel='icon' href='img/favicon.ico' type='image/x-icon'>" +
                            "<style type='text/css'>" +
                            "@media only screen and(max - width: 550px), screen and(max-device - width: 550px) {" +
                            "body[yahoo].buttonwrapper { background - color: transparent!important; }" +
                            "body[yahoo].button { padding: 0!important; }" +
                            "body[yahoo].button a {" +
                            "background - color: #9b59b6; padding: 15px 25px !important; }}" +
                            "@media only screen and(min-device - width: 601px) {" +
                            ".content { width: 600px!important; }" +
                            ".col387 { width: 387px!important; }}" +
                            "</style>" +
                            "</head>" +
                            "<body bgcolor='#34495E' style='margin: 0; padding: 0;' yahoo='fix'>" +
                            "<table align='center' border='0' cellpadding='0' cellspacing='0' style='border-collapse: collapse; width: 100%;' class='content'>" +
                            "<tr>" +
                            "<td style='padding: 15px 10px 15px 10px;'>" +
                            "</td>" +
                            "</tr>" +
                            "<tr>" +
                            "<td align='center' bgcolor='#064E89' style='padding: 20px 20px 20px 20px; color: #ffffff; font-family: Arial, sans-serif; font-size: 30px; font-weight: bold; height:100px;'>" +
                            "THE MAHARAJA SAYAJIRAO UNIVERSITY OF BARODA" +
                            /*"<img src='https://localhost:44374/img/msuLogo.png' alt='MSU Logo' width='152' height='152' style='display:block;' />" +*/
                            "</td>" +
                            "</tr>" +
                            "<tr>" +
                            "</tr>" +
                            "<tr>" +
                            "<td align = 'left' bgcolor = '#ffffff' style = 'padding: 40px 20px 40px 20px; color: #555555; font-family: Arial, sans-serif; font-size: 15px; line-height: 30px; border-bottom: 1px solid #f6f6f6;' >" +
                            "Dear " + postappverification.FirstName + "," +
                            "<br/><br/><div>We request you to login to the admission portal once and check the pending documents.</div>" +
                            "<div><ul><b>Below mentioned documents are pending to upload:</b>" +
                            //"<li>" + subs[2] + "</li>" +
                            PendingDoc +
                            "</ul></div>" +
                            "<br/><div>Use the following link (<u style='color: blue;'>https://admission.msubaroda.ac.in/applicant/#/studentlogin</u>) for submit the documents.</div>" +
                            "<br/><div>To view list of pending documents, click on 'Pending Documents' Button available on your dashboard.</div>" +
                            "<br/><br/><br/>Thank You." +
                            "<br/>MSUB Team" +
                            "</td>" +
                            "</tr> " +
                            "<tr>" +
                            "<td align = 'center' bgcolor= '#dddddd' style= 'padding: 15px 10px 15px 10px; color: #555555; font-family: Arial, sans-serif; font-size: 12px; line-height: 18px;'>" +
                            "<b> THE MAHARAJA SAYAJIRAO UNIVERSITY OF BARODA</b><br/>Pratapgunj, Vadodara, Gujarat 390002" +
                            "</td>" +
                            "</tr>" +
                            "</table>" +
                            "</body>" +
                            "</html>";

                            System.Net.NetworkCredential myMailCredential = new System.Net.NetworkCredential();
                            myMailCredential.UserName = postappverification.EmailKeyName;
                            myMailCredential.Password = "msu@#$123";
                            smtpClient.UseDefaultCredentials = false;
                            smtpClient.EnableSsl = true;
                            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtpClient.Timeout = 20000;
                            smtpClient.Credentials = myMailCredential;

                            // Send SMTP mail
                            string result = string.Empty;
                            smtpClient.Send(MailMessage);

                            return Return.returnHttp("200", "Student Application Verification has been Pending. SMS and Mail has been successfully sent.", null);
                        }
                        catch (Exception e)
                        {
                            return Return.returnHttp("201", e.Message.ToString(), null);
                        }
                        #endregion
                        return Return.returnHttp("200", "SMS Sent successfully", null);
                    }
                    catch (Exception e)
                    {
                        return Return.returnHttp("201", e.Message.ToString(), null);
                    }
                    #endregion

                }

                else if (strMessage == "Student Application Verified Successfully.")
                {
                    return Return.returnHttp("200", strMessage, null);
                }
                else if (strMessage == "Admission Date not configure.Please configure..!!")
                {
                    return Return.returnHttp("200", strMessage, null);
                }
                else if (postappverification.EligibilityStatus == "Not_Approved" && strMessage == "Note: Student Application Verification is under process. Your Remarks saved successfully.")
                {
                    return Return.returnHttp("200", strMessage, null);
                }

                else
                {
                    return Return.returnHttp("201", strMessage, null);
                }
                // *************Stop SMS & Email***************



            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion

        #region UpdateAdmApplicationAcademicRemarks
        [HttpPost]
        public HttpResponseMessage UpdateAdmApplicationAcademicRemarks(JObject jsonobject)

        {
            PostApplicantVerification postappverification = new PostApplicantVerification();
            dynamic jsonData = jsonobject;

            try
            {
                postappverification.Id = jsonData.Id;
                postappverification.ApplicantRegId = jsonData.ApplicantRegId;
                postappverification.EmailKeyName = jsonData.EmailKeyName;
                postappverification.FirstName = jsonData.FirstName;
                postappverification.MiddleName = jsonData.MiddleName;
                postappverification.LastName = jsonData.LastName;
                postappverification.MobileNo = jsonData.MobileNo;
                postappverification.EmailId = jsonData.EmailId;
                postappverification.PendingDocList = jsonData.PendingDocList;

                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                postappverification.EligibilityByAcademics = jsonData.EligibilityByAcademics;
                postappverification.AdminRemarkByAcademics = jsonData.AdminRemarkByAcademics;
                postappverification.ApprovedByAcademics = Convert.ToInt32(responseToken);

                SqlCommand cmd = new SqlCommand("PostAdmApplicationUpdateAcademic", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", postappverification.Id);
                cmd.Parameters.AddWithValue("@ApplicantRegId", postappverification.ApplicantRegId);
                cmd.Parameters.AddWithValue("@EligibilityByAcademics", postappverification.EligibilityByAcademics);
                cmd.Parameters.AddWithValue("@AdminRemarkByAcademics", postappverification.AdminRemarkByAcademics);
                cmd.Parameters.AddWithValue("@ApprovedByAcademics", postappverification.ApprovedByAcademics);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                //return Return.returnHttp("200", strMessage, null);

                // *************Start SMS & Email***************
                

                if (postappverification.EligibilityByAcademics == "Pending")
                {
                    #region SMS and Email for Applicant Verification By Academic (Pending)
                    try
                    {
                        //Your user name
                        string user = "shaurya";
                        ////Your authentication key
                        string key = "ec07fd3102XX";
                        ////Multiple mobiles numbers separated by comma
                        string mobile = postappverification.MobileNo.Trim();
                        ////Sender ID,While using route4 sender id should be 6 characters long.
                        string senderid = "MSUBIS";
                        ////Your message to send, Add URL encoding here.
                        ////string message = System.Web.HttpUtility.UrlEncode("your OTP for MSU Wi-Fi Registration : " + OTP);
                        //string message = "Dear Applicant, MSUIS login credentials is Username " + postappverification.Id + " and Password " + postappverification.Id + ". Please do not share this information to anyone.\n- Team MSUB";
                        string message = "[MSUB Verification]: Dear, few of your documents are pending, kindly update at earliest. URL: https://admission.msubaroda.ac.in/. Regards,\nMSUB";


                        //Prepare you post parameters
                        StringBuilder sbPostData = new StringBuilder();
                        sbPostData.AppendFormat("user={0}", user);
                        sbPostData.AppendFormat("&password={0}", key);

                        sbPostData.AppendFormat("&mobiles={0}", mobile);
                        sbPostData.AppendFormat("&sms={0}", message);
                        sbPostData.AppendFormat("&senderid={0}", senderid);
                        sbPostData.AppendFormat("&entityid={0}", "1201159618592707491");
                        //sbPostData.AppendFormat("&tempid={0}", "1207161795812832107");
                        //sbPostData.AppendFormat("&tempid={0}", "1207161908607186569");
                        sbPostData.AppendFormat("&tempid={0}", "1207162676783608431");
                        sbPostData.AppendFormat("&accusage={0}", "1");


                        //Call Send SMS API
                        string sendSMSUri = "http://shaurya.mysmsapps.co.in/sendsms.jsp?";
                        //Create HTTPWebrequest
                        HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                        //Prepare and Add URL Encoded data
                        UTF8Encoding encoding = new UTF8Encoding();
                        byte[] data = encoding.GetBytes(sbPostData.ToString());
                        //Specify post method
                        httpWReq.Method = "POST";
                        httpWReq.ContentType = "application/x-www-form-urlencoded";
                        httpWReq.ContentLength = data.Length;
                        using (Stream stream = httpWReq.GetRequestStream())
                        {
                            stream.Write(data, 0, data.Length);
                        }
                        //Get the response
                        HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                        StreamReader reader = new StreamReader(response.GetResponseStream());
                        string responseString = reader.ReadToEnd();

                        //Close the response
                        reader.Close();
                        response.Close();

                        #region Email for Applicant Verification By Faculty (Pending)
                        try
                        {
                            SmtpClient smtpClient = new SmtpClient();
                            MailMessage MailMessage = new MailMessage();
                            //dynamic Jsondata = jObject;

                            string ToEmail = Convert.ToString(postappverification.EmailId);

                            MailAddress fromAddress = new MailAddress(postappverification.EmailKeyName, "MSUIS - Verification Status");

                            smtpClient.Host = "smtp.gmail.com";

                            //Default port will be 25
                            smtpClient.Port = 587;

                            //From address will be given as a MailAddress Object
                            MailMessage.From = fromAddress;
                            MailMessage.To.Add(ToEmail);

                            // To address collection of MailAddress
                            MailMessage.Bcc.Add(postappverification.EmailKeyName);
                            MailMessage.Subject = "MSUIS - Verification Process";
                            MailMessage.IsBodyHtml = true;
                            // Message body content

                            string s = postappverification.PendingDocList;

                            string[] subs = s.Split('-');
                            string PendingDoc = "";
                            for (int i = 1; i < subs.Length; i++)
                            {
                                subs[i] = subs[i].Trim();
                                PendingDoc += "<li>" + subs[i] + "</li>";

                            }
                            MailMessage.Body = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>" +
                            "<html xmlns ='http://www.w3.org/1999/xhtml'>" +
                            "<head>" +
                            "<meta http-equiv='Content-Type' content ='text/html; charset=UTF-8' />" +
                            "<meta name='viewport' content='width=device-width' />" +
                            "<link rel='icon' href='img/favicon.ico' type='image/x-icon'>" +
                            "<style type='text/css'>" +
                            "@media only screen and(max - width: 550px), screen and(max-device - width: 550px) {" +
                            "body[yahoo].buttonwrapper { background - color: transparent!important; }" +
                            "body[yahoo].button { padding: 0!important; }" +
                            "body[yahoo].button a {" +
                            "background - color: #9b59b6; padding: 15px 25px !important; }}" +
                            "@media only screen and(min-device - width: 601px) {" +
                            ".content { width: 600px!important; }" +
                            ".col387 { width: 387px!important; }}" +
                            "</style>" +
                            "</head>" +
                            "<body bgcolor='#34495E' style='margin: 0; padding: 0;' yahoo='fix'>" +
                            "<table align='center' border='0' cellpadding='0' cellspacing='0' style='border-collapse: collapse; width: 100%;' class='content'>" +
                            "<tr>" +
                            "<td style='padding: 15px 10px 15px 10px;'>" +
                            "</td>" +
                            "</tr>" +
                            "<tr>" +
                            "<td align='center' bgcolor='#064E89' style='padding: 20px 20px 20px 20px; color: #ffffff; font-family: Arial, sans-serif; font-size: 30px; font-weight: bold; height:100px;'>" +
                            "THE MAHARAJA SAYAJIRAO UNIVERSITY OF BARODA" +
                            /*"<img src='https://localhost:44374/img/msuLogo.png' alt='MSU Logo' width='152' height='152' style='display:block;' />" +*/
                            "</td>" +
                            "</tr>" +
                            "<tr>" +
                            "</tr>" +
                            "<tr>" +
                            "<td align = 'left' bgcolor = '#ffffff' style = 'padding: 40px 20px 40px 20px; color: #555555; font-family: Arial, sans-serif; font-size: 15px; line-height: 30px; border-bottom: 1px solid #f6f6f6;' >" +
                            "Dear " + postappverification.FirstName + "," +
                            "<br/><br/><div>We request you to login to the admission portal once and check the pending documents.</div>" +
                            "<div><ul><b>Below mentioned documents are pending to upload:</b>" +
                          
                            PendingDoc +
                            "</ul></div>" +
                            "<br/><div>Use the following link (<u style='color: blue;'>https://admission.msubaroda.ac.in/applicant/#/studentlogin</u>) for submit the documents.</div>" +
                            "<br/><div>To view list of pending documents, click on 'Pending Documents' Button available on your dashboard.</div>" +
                            "<br/><br/><br/>Thank You." +
                            "<br/>MSUB Team" +
                            "</td>" +
                            "</tr> " +
                            "<tr>" +
                            "<td align = 'center' bgcolor= '#dddddd' style= 'padding: 15px 10px 15px 10px; color: #555555; font-family: Arial, sans-serif; font-size: 12px; line-height: 18px;'>" +
                            "<b> THE MAHARAJA SAYAJIRAO UNIVERSITY OF BARODA</b><br/>Pratapgunj, Vadodara, Gujarat 390002" +
                            "</td>" +
                            "</tr>" +
                            "</table>" +
                            "</body>" +
                            "</html>";

                            System.Net.NetworkCredential myMailCredential = new System.Net.NetworkCredential();
                            myMailCredential.UserName = postappverification.EmailKeyName;
                            myMailCredential.Password = "msu@#$123";
                            smtpClient.UseDefaultCredentials = false;
                            smtpClient.EnableSsl = true;
                            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtpClient.Timeout = 20000;
                            smtpClient.Credentials = myMailCredential;

                            // Send SMTP mail
                            string result = string.Empty;
                            smtpClient.Send(MailMessage);

                            return Return.returnHttp("200", "Student Application Verification has been Pending. SMS and Mail has been successfully sent.", null);
                        }
                        catch (Exception e)
                        {
                            return Return.returnHttp("201", e.Message.ToString(), null);
                        }
                        #endregion
                        return Return.returnHttp("200", "SMS Sent successfully", null);
                    }
                    catch (Exception e)
                    {
                        return Return.returnHttp("201", e.Message.ToString(), null);
                    }
                    #endregion

                }

                else if (strMessage == "Student Application Verified Successfully.")
                {
                    return Return.returnHttp("200", strMessage, null);
                }
               
                else if (postappverification.EligibilityByAcademics == "Not_Approved" || postappverification.EligibilityByAcademics == "Provisionally_Eligible")
                {
                return Return.returnHttp("200", strMessage, null);
            }

                else
                {
                    return Return.returnHttp("201", strMessage, null);
                }
                // *************Stop SMS & Email***************
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion

        #region GenericConfigurationGetByKeyName
        [HttpPost]
        public HttpResponseMessage GenericConfigurationGetByKeyName(JObject Generic)
        {
            try
            {
                GenericConfiguration generic = new GenericConfiguration();
                dynamic JsonData = Generic;
                generic.KeyName = Convert.ToString(JsonData.KeyName);
                SqlCommand Cmd = new SqlCommand("GenericConfigurationGetByKeyName", con);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@KeyName", generic.KeyName);

                sda.SelectCommand = Cmd;
                sda.Fill(dt);

                List<GenericConfiguration> ObjLstGenConfig = new List<GenericConfiguration>();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        GenericConfiguration objGenConfig = new GenericConfiguration();

                        objGenConfig.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        objGenConfig.KeyName = Convert.ToString(dt.Rows[i]["KeyName"]);
                        objGenConfig.Value = Convert.ToString(dt.Rows[i]["Value"]);
                        //objGenConfig.IsActive = Convert.ToBoolean(dt.Rows[i]["IsActive"]);
                        ObjLstGenConfig.Add(objGenConfig);
                    }
                }
                return Return.returnHttp("200", ObjLstGenConfig, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        //#region UpdateDualAdmissionRequestByFaculty
        //[HttpPost]
        //public HttpResponseMessage UpdateDualAdmissionRequestByFaculty(JObject jsonobject)

        //{
        //    PostApplicantVerification postappverification = new PostApplicantVerification();
        //    dynamic jsonData = jsonobject;

        //    try
        //    {
        //        postappverification.Id = jsonData.Id;
        //        postappverification.ApplicantRegId = jsonData.ApplicantRegId;
        //        postappverification.ProgrammeInstancePartTermId = jsonData.ProgrammeInstancePartTermId;
        //        //postappverification.IsDualAdmission = Convert.ToBoolean(jsonData.IsDualAdmission);


        //        string token = Request.Headers.GetValues("token").FirstOrDefault();
        //        TokenOperation tokenOperation = new TokenOperation();
        //        string responseToken = tokenOperation.ValidateToken(token);
        //        if (responseToken == "0")
        //        {
        //            return Return.returnHttp("0", null, null);
        //        }

        //        SqlCommand cmd1 = new SqlCommand("PostDualAdmissionRequestAdd", con);
        //        cmd1.CommandType = CommandType.StoredProcedure;
        //        postappverification.RemarkByFaculty = jsonData.RemarkByFaculty;
        //        postappverification.RequestBy = Convert.ToInt32(responseToken);


        //        cmd1.Parameters.AddWithValue("@Id", postappverification.Id);
        //        cmd1.Parameters.AddWithValue("@AdmApplicationId", postappverification.Id);
        //        cmd1.Parameters.AddWithValue("@ApplicantRegId", postappverification.ApplicantRegId);
        //        cmd1.Parameters.AddWithValue("@ProgrammeInstancePartTermId", postappverification.ProgrammeInstancePartTermId);
        //        //cmd1.Parameters.AddWithValue("@IsDualAdmission", postappverification.IsDualAdmission);
        //        cmd1.Parameters.AddWithValue("@RemarkByFaculty", postappverification.RemarkByFaculty);
        //        cmd1.Parameters.AddWithValue("@RequestBy", postappverification.RequestBy);
        //        cmd1.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
        //        cmd1.Parameters["@Message"].Direction = ParameterDirection.Output;


        //        con.Open();
        //        cmd1.ExecuteNonQuery();
        //        string strMessage1 = Convert.ToString(cmd1.Parameters["@Message"].Value);
        //        con.Close();
        //        //return Return.returnHttp("200", strMessage1, null);


        //        SqlCommand cmd = new SqlCommand("PostAdmApplicationDualRequest", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        postappverification.IsDualAdmission = Convert.ToBoolean(jsonData.IsDualAdmission);
        //        postappverification.ApprovedByFaculty = Convert.ToInt32(responseToken);


        //        cmd.Parameters.AddWithValue("@Id", postappverification.Id);
        //        cmd.Parameters.AddWithValue("@AdmApplicationId", postappverification.Id);
        //        cmd.Parameters.AddWithValue("@ApplicantRegId", postappverification.ApplicantRegId);
        //        cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", postappverification.ProgrammeInstancePartTermId);
        //        cmd.Parameters.AddWithValue("@IsDualAdmission", postappverification.IsDualAdmission);
        //        cmd.Parameters.AddWithValue("@ApprovedByFaculty", postappverification.ApprovedByFaculty);
        //        cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
        //        cmd.Parameters["@Message"].Direction = ParameterDirection.Output;


        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
        //        con.Close();
        //        string commonmsg = strMessage1 + " And " + strMessage;
        //        return Return.returnHttp("200", commonmsg, null);

        //    }
        //    catch (Exception e)
        //    {
        //        return Return.returnHttp("201", e.Message, null);
        //    }
        //}

        //#endregion

        #region UpdateSubmittedDocStatusByAcademic
        [HttpPost]
        public HttpResponseMessage UpdateSubmittedDocStatusByAcademic(JObject jsonobject)

        {
            PostApplicantVerification postappverification = new PostApplicantVerification();
            dynamic jsonData = jsonobject;

            try
            {
                postappverification.Id = Convert.ToInt64(jsonData.SubDocId);
                postappverification.AdmissionApplicationId = jsonData.AdmissionApplicationId;
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                //postappverification.Id = Convert.ToInt32(responseToken);
                postappverification.IsVerifiedByAcademic = Convert.ToBoolean(jsonData.IsVerifiedByAcademic);
                postappverification.ApprovedByAcademic = Convert.ToInt32(responseToken);
                //int UserId = Convert.ToInt32(jsonData.ModifiedBy);

                SqlCommand cmd = new SqlCommand("PostSubmittedDocStatusByAcademic", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", postappverification.Id);
                cmd.Parameters.AddWithValue("@AdmissionApplicationId", postappverification.AdmissionApplicationId);
                cmd.Parameters.AddWithValue("@IsVerifiedByAcademic", postappverification.IsVerifiedByAcademic);
                cmd.Parameters.AddWithValue("@ApprovedByAcademic", postappverification.ApprovedByAcademic);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                return Return.returnHttp("200", strMessage, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion

        #region UpdateDeatilsforPreConfiguration
        [HttpPost]
        public HttpResponseMessage UpdateDeatilsforPreConfiguration(JObject jsonobject)

        {
            PostApplicantVerification postappverification = new PostApplicantVerification();
            dynamic jsonData = jsonobject;

            try
            {
                postappverification.Id = jsonData.Id;
                postappverification.ApplicantRegId = jsonData.ApplicantRegId;
                postappverification.FirstName = jsonData.FirstName;
                postappverification.MiddleName = jsonData.MiddleName;
                postappverification.LastName = jsonData.LastName;
                postappverification.MobileNo = jsonData.MobileNo;
                postappverification.EmailId = jsonData.EmailId;
                postappverification.InstPartTerm = Convert.ToString(jsonData.InstPartTerm);
                postappverification.ProgrammeName = jsonData.ProgrammeName;
                postappverification.BranchName = jsonData.BranchName;
                postappverification.AcademicYearCode = jsonData.AcademicYearCode;
                postappverification.EmailKeyName = jsonData.EmailKeyName;
                postappverification.ProgrammeInstancePartTermId = jsonData.ProgrammeInstancePartTermId;
                postappverification.VerificationStatus = jsonData.VerificationStatus;
                postappverification.VerificationRemarks = jsonData.VerificationRemarks;
                postappverification.PendingDocList = jsonData.PendingDocList;

                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }


                postappverification.ApprovedByFaculty = Convert.ToInt32(responseToken);

                SqlCommand cmd = new SqlCommand("UpdateDeatilsforPreConfiguration", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", postappverification.Id);
                cmd.Parameters.AddWithValue("@ApplicantRegId", postappverification.ApplicantRegId);
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", postappverification.ProgrammeInstancePartTermId);

                cmd.Parameters.AddWithValue("@VerificationStatus", postappverification.VerificationStatus);
                cmd.Parameters.AddWithValue("@VerificationRemarks", postappverification.VerificationRemarks);
                cmd.Parameters.AddWithValue("@ApprovedByFaculty", postappverification.ApprovedByFaculty);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;


                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();
                //return Return.returnHttp("200", strMessage, null);

              
                if (postappverification.VerificationStatus == "Pending" && strMessage == "Note: Student Application Verification is under process. Your Remarks saved successfully.")
                {
                    #region SMS and Email for Applicant Verification By Faculty (Pending)
                    try
                    {
                        //Your user name
                        string user = "shaurya";
                        ////Your authentication key
                        string key = "ec07fd3102XX";
                        ////Multiple mobiles numbers separated by comma
                        string mobile = postappverification.MobileNo.Trim();
                        ////Sender ID,While using route4 sender id should be 6 characters long.
                        string senderid = "MSUBIS";
                        ////Your message to send, Add URL encoding here.
                        ////string message = System.Web.HttpUtility.UrlEncode("your OTP for MSU Wi-Fi Registration : " + OTP);
                        //string message = "Dear Applicant, MSUIS login credentials is Username " + postappverification.Id + " and Password " + postappverification.Id + ". Please do not share this information to anyone.\n- Team MSUB";
                        string message = "[MSUB Verification]: Dear, few of your documents are pending, kindly update at earliest. URL: https://admission.msubaroda.ac.in/. Regards,\nMSUB";


                        //Prepare you post parameters
                        StringBuilder sbPostData = new StringBuilder();
                        sbPostData.AppendFormat("user={0}", user);
                        sbPostData.AppendFormat("&password={0}", key);

                        sbPostData.AppendFormat("&mobiles={0}", mobile);
                        sbPostData.AppendFormat("&sms={0}", message);
                        sbPostData.AppendFormat("&senderid={0}", senderid);
                        sbPostData.AppendFormat("&entityid={0}", "1201159618592707491");
                        //sbPostData.AppendFormat("&tempid={0}", "1207161795812832107");
                        //sbPostData.AppendFormat("&tempid={0}", "1207161908607186569");
                        sbPostData.AppendFormat("&tempid={0}", "1207162676783608431");
                        sbPostData.AppendFormat("&accusage={0}", "1");


                        //Call Send SMS API
                        string sendSMSUri = "http://shaurya.mysmsapps.co.in/sendsms.jsp?";
                        //Create HTTPWebrequest
                        HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                        //Prepare and Add URL Encoded data
                        UTF8Encoding encoding = new UTF8Encoding();
                        byte[] data = encoding.GetBytes(sbPostData.ToString());
                        //Specify post method
                        httpWReq.Method = "POST";
                        httpWReq.ContentType = "application/x-www-form-urlencoded";
                        httpWReq.ContentLength = data.Length;
                        using (Stream stream = httpWReq.GetRequestStream())
                        {
                            stream.Write(data, 0, data.Length);
                        }
                        //Get the response
                        HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                        StreamReader reader = new StreamReader(response.GetResponseStream());
                        string responseString = reader.ReadToEnd();

                        //Close the response
                        reader.Close();
                        response.Close();

                        #region Email for Applicant Verification By Faculty (Pending)
                        try
                        {
                            SmtpClient smtpClient = new SmtpClient();
                            MailMessage MailMessage = new MailMessage();
                            //dynamic Jsondata = jObject;

                            string ToEmail = Convert.ToString(postappverification.EmailId);

                            MailAddress fromAddress = new MailAddress(postappverification.EmailKeyName, "MSUIS - Verification Status");

                            smtpClient.Host = "smtp.gmail.com";

                            //Default port will be 25
                            smtpClient.Port = 587;

                            //From address will be given as a MailAddress Object
                            MailMessage.From = fromAddress;
                            MailMessage.To.Add(ToEmail);

                            // To address collection of MailAddress
                            MailMessage.Bcc.Add(postappverification.EmailKeyName);
                            MailMessage.Subject = "MSUIS - Verification Process";
                            MailMessage.IsBodyHtml = true;
                            // Message body content

                            string s = postappverification.PendingDocList;

                            string[] subs = s.Split('-');
                            string PendingDoc = "";
                            for (int i = 1; i < subs.Length; i++)
                            {
                                subs[i] = subs[i].Trim();
                                PendingDoc += "<li>" + subs[i] + "</li>";

                            }
                            MailMessage.Body = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>" +
                            "<html xmlns ='http://www.w3.org/1999/xhtml'>" +
                            "<head>" +
                            "<meta http-equiv='Content-Type' content ='text/html; charset=UTF-8' />" +
                            "<meta name='viewport' content='width=device-width' />" +
                            "<link rel='icon' href='img/favicon.ico' type='image/x-icon'>" +
                            "<style type='text/css'>" +
                            "@media only screen and(max - width: 550px), screen and(max-device - width: 550px) {" +
                            "body[yahoo].buttonwrapper { background - color: transparent!important; }" +
                            "body[yahoo].button { padding: 0!important; }" +
                            "body[yahoo].button a {" +
                            "background - color: #9b59b6; padding: 15px 25px !important; }}" +
                            "@media only screen and(min-device - width: 601px) {" +
                            ".content { width: 600px!important; }" +
                            ".col387 { width: 387px!important; }}" +
                            "</style>" +
                            "</head>" +
                            "<body bgcolor='#34495E' style='margin: 0; padding: 0;' yahoo='fix'>" +
                            "<table align='center' border='0' cellpadding='0' cellspacing='0' style='border-collapse: collapse; width: 100%;' class='content'>" +
                            "<tr>" +
                            "<td style='padding: 15px 10px 15px 10px;'>" +
                            "</td>" +
                            "</tr>" +
                            "<tr>" +
                            "<td align='center' bgcolor='#064E89' style='padding: 20px 20px 20px 20px; color: #ffffff; font-family: Arial, sans-serif; font-size: 30px; font-weight: bold; height:100px;'>" +
                            "THE MAHARAJA SAYAJIRAO UNIVERSITY OF BARODA" +
                            /*"<img src='https://localhost:44374/img/msuLogo.png' alt='MSU Logo' width='152' height='152' style='display:block;' />" +*/
                            "</td>" +
                            "</tr>" +
                            "<tr>" +
                            "</tr>" +
                            "<tr>" +
                            "<td align = 'left' bgcolor = '#ffffff' style = 'padding: 40px 20px 40px 20px; color: #555555; font-family: Arial, sans-serif; font-size: 15px; line-height: 30px; border-bottom: 1px solid #f6f6f6;' >" +
                            "Dear " + postappverification.FirstName + "," +
                            "<br/><br/><div>We request you to login to the admission portal once and check the pending documents.</div>" +
                            "<div><ul><b>Below mentioned documents are pending to upload:</b>" +
                            //"<li>" + subs[2] + "</li>" +
                            PendingDoc +
                            "</ul></div>" +
                            "<br/><div>Use the following link (<u style='color: blue;'>https://admission.msubaroda.ac.in/applicant/#/studentlogin</u>) for submit the documents.</div>" +
                            "<br/><div>To view list of pending documents, click on 'Pending Documents' Button available on your dashboard.</div>" +
                            "<br/><br/><br/>Thank You." +
                            "<br/>MSUB Team" +
                            "</td>" +
                            "</tr> " +
                            "<tr>" +
                            "<td align = 'center' bgcolor= '#dddddd' style= 'padding: 15px 10px 15px 10px; color: #555555; font-family: Arial, sans-serif; font-size: 12px; line-height: 18px;'>" +
                            "<b> THE MAHARAJA SAYAJIRAO UNIVERSITY OF BARODA</b><br/>Pratapgunj, Vadodara, Gujarat 390002" +
                            "</td>" +
                            "</tr>" +
                            "</table>" +
                            "</body>" +
                            "</html>";

                            System.Net.NetworkCredential myMailCredential = new System.Net.NetworkCredential();
                            myMailCredential.UserName = postappverification.EmailKeyName;
                            myMailCredential.Password = "msu@#$123";
                            smtpClient.UseDefaultCredentials = false;
                            smtpClient.EnableSsl = true;
                            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtpClient.Timeout = 20000;
                            smtpClient.Credentials = myMailCredential;

                            // Send SMTP mail
                            string result = string.Empty;
                            smtpClient.Send(MailMessage);

                            return Return.returnHttp("200", "Student Application Verification has been Pending. SMS and Mail has been successfully sent.", null);
                        }
                        catch (Exception e)
                        {
                            return Return.returnHttp("201", e.Message.ToString(), null);
                        }
                        #endregion
                        return Return.returnHttp("200", "SMS Sent successfully", null);
                    }
                    catch (Exception e)
                    {
                        return Return.returnHttp("201", e.Message.ToString(), null);
                    }
                    #endregion

                }

                else if (strMessage == "Student Application Verified Successfully.")
                {
                    return Return.returnHttp("200", strMessage, null);
                }
                else if (strMessage == "Admission Date not configure.Please configure..!!")
                {
                    return Return.returnHttp("200", strMessage, null);
                }
                else if (postappverification.VerificationStatus == "Not_Approved" && strMessage == "Note: Student Application Verification is under process. Your Remarks saved successfully.")
                {
                    return Return.returnHttp("200", strMessage, null);
                }

                else
                {
                    return Return.returnHttp("201", strMessage, null);
                }
                // *************Stop SMS & Email***************



            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion

        #region UpdateEducationDetailsDataInPreVerification
        [HttpPost]
        public HttpResponseMessage UpdateEducationDetailsDataInPreVerification(JObject jsonobject)
        {
            AdmApplicationEducationDetails aed = new AdmApplicationEducationDetails();
            dynamic jsonData = jsonobject;


            if (jsonData.EligibleDegreeId <= 0)
            {
                return Return.returnHttp("201", "Please add EligibleDegreeId.", null);
            }
            else if (jsonData.SpecializationId <= 0)
            {
                return Return.returnHttp("201", "Please add SpecializationId.", null);
            }
            else if (jsonData.ExaminationBodyId <= 0)
            {
                return Return.returnHttp("201", "Please add ExaminationBodyId.", null);
            }
            else if (string.IsNullOrWhiteSpace(Convert.ToString(jsonData.InstituteAttended)))
            {
                return Return.returnHttp("201", "Please add InstituteAttended.", null);
            }
            else if (jsonData.InstituteCityId <= 0)
            {
                return Return.returnHttp("201", "Please add InstituteCityId.", null);
            }
            if (jsonData.ResultStatus == "Result_With_Marksheet")
            {
                if (string.IsNullOrWhiteSpace(Convert.ToString(jsonData.ExamPassMonth)))
                {
                    return Return.returnHttp("201", "Please add ExamPassMonth.", null);
                }
                else if (string.IsNullOrWhiteSpace(Convert.ToString(jsonData.ExamPassYear)))
                {
                    return Return.returnHttp("201", "Please add ExamPassYear.", null);
                }
                else if (string.IsNullOrWhiteSpace(Convert.ToString(jsonData.ExamSeatNumber)))
                {
                    return Return.returnHttp("201", "Please add ExamSeatNumber.", null);
                }
                else if (string.IsNullOrWhiteSpace(Convert.ToString(jsonData.ExamCertificateNumber)))
                {
                    return Return.returnHttp("201", "Please add ExamCertificateNumber.", null);
                }
                /*else if (jsonData.ClassId <= 0)
                {
                    return Return.returnHttp("201", "Please add ClassId.", null);
                }*/
                else if (jsonData.IsFirstTrial == null)
                {
                    return Return.returnHttp("201", "Please add IsFirstTrial.", null);
                }
                else if (jsonData.IsLastQualifyingExam == null)
                {
                    return Return.returnHttp("201", "Please add IsLastQualifyingExam.", null);
                }
                else if (jsonData.TeachingLanguageId <= 0)
                {
                    return Return.returnHttp("201", "Please add TeachingLanguageId.", null);
                }
                jsonData.IsDeclared = null;
            }
            else
            {
                jsonData.ExamPassMonth = null;
                jsonData.ExamPassYear = null;
                jsonData.ExamSeatNumber = null;
                jsonData.ExamCertificateNumber = null;
                jsonData.MarkObtained = null;
                jsonData.MarkOutof = null;
                jsonData.Grade = null;
                jsonData.CGPA = null;
                jsonData.PercentageEquivalenceCGPA = null;
                jsonData.Percentage = null;
                jsonData.ClassId = null;
                jsonData.IsFirstTrial = null;
                jsonData.NoOfTrails = null;
                jsonData.IsLastQualifyingExam = null;
                jsonData.TeachingLanguageId = null;
                jsonData.AttachDocumentNew = null;
            }

            try
            {

                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                if (!string.IsNullOrWhiteSpace(Convert.ToString(jsonData.OtherCity)))
                {
                    aed.OtherCity = jsonData.OtherCity;
                }
                if (jsonData.IsDeclared != null)
                {
                    aed.IsDeclared = jsonData.IsDeclared;
                }

                //string document = jsonData.AttachDocument;
                aed.Id = Convert.ToInt32(jsonData.Id);
                aed.ApplicantRegId = Convert.ToInt64(jsonData.ApplicantRegId); 
                aed.AdmApplicantRegiUserName = Convert.ToInt64(jsonData.AdmApplicantRegiUserName);
                aed.EligibleDegreeId = jsonData.EligibleDegreeId;
                aed.SpecializationId = jsonData.SpecializationId;
                aed.ExaminationBodyId = jsonData.ExaminationBodyId;
                aed.InstituteAttended = jsonData.InstituteAttended;
                aed.SchoolNo = jsonData.SchoolNo;
                aed.InstituteCityId = jsonData.InstituteCityId;
                if (jsonData.CheckCity != "Other")
                {
                    aed.OtherCity = null;
                }
                aed.ExamPassCity = jsonData.ExamPassCity;
                aed.ExamPassMonth = jsonData.ExamPassMonth;
                aed.ExamPassYear = jsonData.ExamPassYear;
                aed.ExamSeatNumber = jsonData.ExamSeatNumber;
                aed.ExamCertificateNumber = jsonData.ExamCertificateNumber;
                aed.MarkObtained = jsonData.MarkObtained;
                aed.MarkOutof = jsonData.MarkOutof;
                aed.Grade = jsonData.Grade;
                aed.CGPA = jsonData.CGPA;
                if (jsonData.PercentageEquivalenceCGPA != null)
                {
                    aed.PercentageEquivalenceCGPA = Convert.ToDecimal(jsonData.PercentageEquivalenceCGPA);
                }
                if (!string.IsNullOrWhiteSpace(Convert.ToString(jsonData.Percentage)) && (Convert.ToString(jsonData.Percentage)) != "NaN")
                {
                    aed.Percentage = Convert.ToDecimal(jsonData.Percentage);
                }
                aed.ClassId = jsonData.ClassId;
                if (!string.IsNullOrWhiteSpace(Convert.ToString(jsonData.IsFirstTrial)))
                {
                    aed.IsFirstTrial = Convert.ToBoolean(jsonData.IsFirstTrial);
                }
                aed.NoOfTrails = jsonData.NoOfTrails;
                if (!string.IsNullOrWhiteSpace(Convert.ToString(jsonData.IsLastQualifyingExam)))
                {
                    aed.IsLastQualifyingExam = Convert.ToBoolean(jsonData.IsLastQualifyingExam);
                }
                aed.TeachingLanguageId = jsonData.TeachingLanguageId;
                aed.ResultStatus = jsonData.ResultStatus;
                //if (!string.IsNullOrWhiteSpace(Convert.ToString(jsonData.AttachDocument)))
                //{
                //    aed.AttachDocument = responseToken + "_" + jsonData.AttachDocument;
                //}

                aed.AttachDocument = jsonData.AttachDocumentNew;
                Int64 UserId = Convert.ToInt64(responseToken);

                SqlCommand cmd = new SqlCommand("PreVerificationEduDetailsEdit", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", aed.Id);
                cmd.Parameters.AddWithValue("@ApplicantRegId", aed.ApplicantRegId);
                cmd.Parameters.AddWithValue("@AdmApplicantRegiUserName", aed.AdmApplicantRegiUserName);
                //cmd.Parameters.AddWithValue("@AdmApplicantRegistrationId", aed.AdmApplicantRegistrationId);
                cmd.Parameters.AddWithValue("@EligibleDegreeId", aed.EligibleDegreeId);
                cmd.Parameters.AddWithValue("@SpecializationId", aed.SpecializationId);
                cmd.Parameters.AddWithValue("@ExaminationBodyId", aed.ExaminationBodyId);
                cmd.Parameters.AddWithValue("@InstituteAttended", aed.InstituteAttended.Trim());
                cmd.Parameters.AddWithValue("@SchoolNo", aed.SchoolNo);
                cmd.Parameters.AddWithValue("@InstituteCityId", aed.InstituteCityId);
                cmd.Parameters.AddWithValue("@ExamPassCity", aed.ExamPassCity);
                cmd.Parameters.AddWithValue("@ExamPassMonth", aed.ExamPassMonth);
                cmd.Parameters.AddWithValue("@ExamPassYear", aed.ExamPassYear);
                cmd.Parameters.AddWithValue("@ExamSeatNumber", aed.ExamSeatNumber);
                cmd.Parameters.AddWithValue("@ExamCertificateNumber", aed.ExamCertificateNumber);
                cmd.Parameters.AddWithValue("@MarkObtained", aed.MarkObtained);
                cmd.Parameters.AddWithValue("@MarkOutof", aed.MarkOutof);
                cmd.Parameters.AddWithValue("@Grade", aed.Grade);
                cmd.Parameters.AddWithValue("@CGPA", aed.CGPA);
                cmd.Parameters.AddWithValue("@PercentageEquivalenceCGPA", aed.PercentageEquivalenceCGPA);
                cmd.Parameters.AddWithValue("@Percentage", aed.Percentage);
                cmd.Parameters.AddWithValue("@ClassId", aed.ClassId);
                cmd.Parameters.AddWithValue("@IsFirstTrial", aed.IsFirstTrial);
                cmd.Parameters.AddWithValue("@NoOfTrails", aed.NoOfTrails);
                cmd.Parameters.AddWithValue("@IsLastQualifyingExam", aed.IsLastQualifyingExam);
                cmd.Parameters.AddWithValue("@TeachingLanguageId", aed.TeachingLanguageId);
                cmd.Parameters.AddWithValue("@ResultStatus", aed.ResultStatus);
                cmd.Parameters.AddWithValue("@AttachDocument", aed.AttachDocument);
                cmd.Parameters.AddWithValue("@OtherCity", aed.OtherCity);
                cmd.Parameters.AddWithValue("@IsDeclared", aed.IsDeclared);
                cmd.Parameters.AddWithValue("@UserId", UserId);

                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                if (string.Equals(strMessage, "TRUE"))
                {
                    return Return.returnHttp("200", "Your Education details has been updated successfully.", null);
                }
                else
                {
                    return Return.returnHttp("201", strMessage, null);
                }

            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message.ToString(), null);
            }


        }
        #endregion

        #region Upload Educational Docs

        [HttpPost()]
        public HttpResponseMessage UploadEduFiles()
        {
            int iUploadedCnt = 0;

            // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
            string sPath = "";
            sPath = DocURL;
            //sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/Documents/");

            string token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation tokenOperation = new TokenOperation();
            string responseToken = tokenOperation.ValidateToken(token);
            if (responseToken == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            string abc = Request.Headers.GetValues("EducationFileName").FirstOrDefault();
            Int64 AdmApplicantRegiUserName = Convert.ToInt64(Request.Headers.GetValues("AdmApplicantRegiUserName").FirstOrDefault());

            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            // CHECK THE FILE COUNT.
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];

                if (hpf.ContentLength > 0)
                {
                    if (File.Exists(sPath + Path.GetFileName(AdmApplicantRegiUserName + "_" + abc)))
                    {
                        File.Delete(sPath + Path.GetFileName(AdmApplicantRegiUserName + "_" + abc));
                    }
                    hpf.SaveAs(sPath + Path.GetFileName(AdmApplicantRegiUserName + "_" + abc));
                    iUploadedCnt = iUploadedCnt + 1;
                 
                }
            }


            // RETURN A MESSAGE (OPTIONAL).
            if (iUploadedCnt > 0)
            {
                return Return.returnHttp("200", iUploadedCnt + " Files Uploaded Successfully", null);
            }
            else
            {
                return Return.returnHttp("201", iUploadedCnt + "Upload Failed", null);
            }

        }
        #endregion

        #region Upload Personal Docs

        [HttpPost()]
        public HttpResponseMessage UploadPersonalDocs()
        {
            int iUploadedCnt = 0;

            // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
            string sPath = "";
            sPath = DocURL;
            //sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/Documents/");

            string token = Request.Headers.GetValues("token").FirstOrDefault();
            TokenOperation tokenOperation = new TokenOperation();
            string responseToken = tokenOperation.ValidateToken(token);
            if (responseToken == "0")
            {
                return Return.returnHttp("0", null, null);
            }

            string abc = Request.Headers.GetValues("PersonalDocs").FirstOrDefault();
            Int64 AdmApplicantRegiUserName = Convert.ToInt64(Request.Headers.GetValues("AdmApplicantRegiUserName").FirstOrDefault());

            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            // CHECK THE FILE COUNT.
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];

                if (hpf.ContentLength > 0)
                {
                    if (abc == "EWS.pdf")
                    {
                        File.Delete(sPath + Path.GetFileName(AdmApplicantRegiUserName + "_" + "EWS.pdf"));
                    }
                    else if (abc == "SocialCat.pdf")
                    {
                        File.Delete(sPath + Path.GetFileName(AdmApplicantRegiUserName + "_" + "SocialCat.pdf"));
                    }
                    else if (abc == "Reservation.pdf")
                    {
                        File.Delete(sPath + Path.GetFileName(AdmApplicantRegiUserName + "_" + "Reservation.pdf"));
                    }
                    if (abc == "NCLCerti.pdf")
                    {
                        File.Delete(sPath + Path.GetFileName(AdmApplicantRegiUserName + "_" + "NCLCerti.pdf"));
                    }
                    if (abc == "EWS_IADoc.pdf")
                    {
                        File.Delete(sPath + Path.GetFileName(AdmApplicantRegiUserName + "_" + "EWS_IADoc.pdf"));
                    }

                    // SAVE THE FILES IN THE FOLDER.
                    hpf.SaveAs(sPath + Path.GetFileName(AdmApplicantRegiUserName + "_" + abc));
                    iUploadedCnt = iUploadedCnt + 1;

                }
            }


            // RETURN A MESSAGE (OPTIONAL).
            if (iUploadedCnt > 0)
            {
                return Return.returnHttp("200", iUploadedCnt + " Files Uploaded Successfully", null);
            }
            else
            {
                return Return.returnHttp("201", iUploadedCnt + "Upload Failed", null);
            }

        }
        #endregion

        #region UpdatePersonalDetailsDataInPreVerification
        [HttpPost]
        public HttpResponseMessage UpdatePersonalDetailsDataInPreVerification(JObject jsonobject)

        {
            PersonalDetails PersonalDetails = new PersonalDetails();
            dynamic jsonData = jsonobject;

            try
            {
                PersonalDetails.AdmApplicantRegiUserName = Convert.ToInt64(jsonData.AdmApplicantRegiUserName);
                PersonalDetails.ApplicantRegId = Convert.ToInt64(jsonData.ApplicantRegId);
                PersonalDetails.Gender = jsonData.Gender;
                PersonalDetails.NCLCertiNo = Convert.ToString(jsonData.NCLCertiNo);
                PersonalDetails.EWSCertiNo = Convert.ToString(jsonData.EWSCertiNo);
                //PersonalDetails.SocialCategoryId = 0;
                if(jsonData.SocialCategoryId == null)
                {
                    PersonalDetails.SocialCategoryId = null;
                }
                else
                {
                    PersonalDetails.SocialCategoryId = Convert.ToInt32(jsonData.SocialCategoryId);
                }
             
                PersonalDetails.ApplicationCategoryId = Convert.ToInt32(jsonData.ApplicationCategoryId);
                PersonalDetails.IsEWS = jsonData.IsEWS;
                 
                if(jsonData.SocialCategoryDoc != null && jsonData.SocialCategoryDoc != "")
                {
                    PersonalDetails.SocialCategoryDoc = PersonalDetails.AdmApplicantRegiUserName + "_" + jsonData.SocialCategoryDoc;
                }
                else
                {
                    PersonalDetails.SocialCategoryDoc = null;
                }

                if (jsonData.ReservationCategoryDoc != null && jsonData.ReservationCategoryDoc != "")
                {
                    PersonalDetails.ReservationCategoryDoc = PersonalDetails.AdmApplicantRegiUserName + "_" + jsonData.ReservationCategoryDoc;
                }
                else
                {
                    PersonalDetails.ReservationCategoryDoc = null;
                }

                if (jsonData.EWSDoc != null && jsonData.EWSDoc != "")
                {
                    PersonalDetails.EWSDoc = PersonalDetails.AdmApplicantRegiUserName + "_" + jsonData.EWSDoc;
                }
                else
                {
                    PersonalDetails.EWSDoc = null;
                }

                if (jsonData.NCLCerti != null && jsonData.NCLCerti != "")
                {
                    PersonalDetails.NCLCerti = PersonalDetails.AdmApplicantRegiUserName + "_" + jsonData.NCLCerti;
                }
                else
                {
                    PersonalDetails.NCLCerti = null;
                }

                if (jsonData.EWS_IADoc != null && jsonData.EWS_IADoc != "")
                {
                    PersonalDetails.EWS_IADoc = PersonalDetails.AdmApplicantRegiUserName + "_" + jsonData.EWS_IADoc;
                }
                else
                {
                    PersonalDetails.EWS_IADoc = null;
                }

                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

                if (jsonData.NCLCerti_IsuueDate != null)
                {
                    DateTime dtNCLCerti_IsuueDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(jsonData.NCLCerti_IsuueDate), out dtNCLCerti_IsuueDate);
                    if (ChkDt)
                    {
                        //aaconfig.ApplicationStartDate = dtAppStartDate;
                        PersonalDetails.NCLCerti_IsuueDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(jsonData.NCLCerti_IsuueDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

                    }
                    else
                    {
                        PersonalDetails.NCLCerti_IsuueDate = null;
                    }
                }
                else
                {
                    PersonalDetails.NCLCerti_IsuueDate = null;
                }


                if (jsonData.NCLCertiValidityDate != null)
                {
                    DateTime dtNCLCertiValidityDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(jsonData.NCLCertiValidityDate), out dtNCLCertiValidityDate);
                    if (ChkDt)
                    {
                        //aaconfig.ApplicationStartDate = dtAppStartDate;
                        PersonalDetails.NCLCertiValidityDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(jsonData.NCLCertiValidityDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

                    }
                    else
                    {
                        PersonalDetails.NCLCertiValidityDate = null;
                    }
                }
                else
                {
                    PersonalDetails.NCLCertiValidityDate = null;
                }

                if (jsonData.EWSCerti_IsuueDate != null)
                {
                    DateTime dtEWSCerti_IsuueDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(jsonData.EWSCerti_IsuueDate), out dtEWSCerti_IsuueDate);
                    if (ChkDt)
                    {
                        //aaconfig.ApplicationStartDate = dtAppStartDate;
                        PersonalDetails.EWSCerti_IsuueDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(jsonData.EWSCerti_IsuueDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

                    }
                    else
                    {
                        PersonalDetails.EWSCerti_IsuueDate = null;
                    }
                }
                else
                {
                    PersonalDetails.EWSCerti_IsuueDate = null;
                }

                if (jsonData.EWSCertiValidityDate != null)
                {
                    DateTime dtEWSCerti_ValidityDate = new DateTime(1949, 1, 1);
                    bool ChkDt = DateTime.TryParse(Convert.ToString(jsonData.EWSCertiValidityDate), out dtEWSCerti_ValidityDate);
                    if (ChkDt)
                    {
                        //aaconfig.ApplicationStartDate = dtAppStartDate;
                        PersonalDetails.EWSCertiValidityDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(jsonData.EWSCertiValidityDate).ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

                    }
                    else
                    {
                        PersonalDetails.EWSCertiValidityDate = null;
                    }
                }
                else
                {
                    PersonalDetails.EWSCertiValidityDate = null;
                }

                if (jsonData.IsEWS != null)
                {
                    if (jsonData.IsEWS == 0)
                    {
                        jsonData.IsEWS = null;
                    }
                    else if (jsonData.IsEWS == "True")
                    {
                        jsonData.IsEWS = jsonData.IsEWS;
                    }
                    else if (jsonData.IsEWS == "False")
                    {
                        jsonData.IsEWS = jsonData.IsEWS;
                    }
                    else
                    {
                        return Return.returnHttp("201", "Please give valid EWS detail", null);
                    }
                }

                Int64 ModifiedBy = Convert.ToInt64(responseToken);

                SqlCommand cmd = new SqlCommand("UpdatePersonalDetailsDataInPreVerification", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ApplicantRegId", PersonalDetails.ApplicantRegId);
                cmd.Parameters.AddWithValue("@Gender", PersonalDetails.Gender);
                cmd.Parameters.AddWithValue("@SocialCategoryDoc", PersonalDetails.SocialCategoryDoc);
                cmd.Parameters.AddWithValue("@SocialCategoryId", PersonalDetails.SocialCategoryId);
                cmd.Parameters.AddWithValue("@ReservationCategoryDoc", PersonalDetails.ReservationCategoryDoc);
                cmd.Parameters.AddWithValue("@ApplicationCategoryId", PersonalDetails.ApplicationCategoryId);
                cmd.Parameters.AddWithValue("@EWSDoc", PersonalDetails.EWSDoc);
                cmd.Parameters.AddWithValue("@IsEWS", PersonalDetails.IsEWS);
                cmd.Parameters.AddWithValue("@NCLCerti", PersonalDetails.NCLCerti);
                cmd.Parameters.AddWithValue("@NCLCertiNo", PersonalDetails.NCLCertiNo);
                cmd.Parameters.AddWithValue("@NCLCerti_IsuueDate", PersonalDetails.NCLCerti_IsuueDate);
                cmd.Parameters.AddWithValue("@NCLCertiValidityDate", PersonalDetails.NCLCertiValidityDate);
                cmd.Parameters.AddWithValue("@EWS_IADoc", PersonalDetails.EWS_IADoc);
                cmd.Parameters.AddWithValue("@EWSCertiNo", PersonalDetails.EWSCertiNo);
                cmd.Parameters.AddWithValue("@EWSCerti_IsuueDate", PersonalDetails.EWSCerti_IsuueDate);
                cmd.Parameters.AddWithValue("@EWSCertiValidityDate", PersonalDetails.EWSCertiValidityDate);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                return Return.returnHttp("200", strMessage, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        #endregion

        #region UpdateNCLCertiVerification
        [HttpPost]
        public HttpResponseMessage UpdateNCLCertiVerification(JObject jsonobject)

        {
            PostApplicantVerification postappverification = new PostApplicantVerification();
            dynamic jsonData = jsonobject;

            try
            {
                postappverification.ApplicantRegId = jsonData.ApplicantRegId;
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                //postappverification.Id = Convert.ToInt32(responseToken);
                postappverification.IsNCLCertiVerifiedEdit = Convert.ToBoolean(jsonData.IsNCLCertiVerified);
                postappverification.ApprovedByFaculty = Convert.ToInt32(responseToken);
                //int UserId = Convert.ToInt32(jsonData.ModifiedBy);

                SqlCommand cmd = new SqlCommand("PostAdmRegiNCLCertiVerified", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ApplicantRegId", postappverification.ApplicantRegId);
                cmd.Parameters.AddWithValue("@IsNCLCertiVerified", postappverification.IsNCLCertiVerifiedEdit);
                cmd.Parameters.AddWithValue("@ApprovedByFaculty", postappverification.ApprovedByFaculty);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                return Return.returnHttp("200", strMessage, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion

        #region UpdateEWS_IADocVerification
        [HttpPost]
        public HttpResponseMessage UpdateEWS_IADocVerification(JObject jsonobject)

        {
            PostApplicantVerification postappverification = new PostApplicantVerification();
            dynamic jsonData = jsonobject;

            try
            {
                postappverification.ApplicantRegId = jsonData.ApplicantRegId;
                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                //postappverification.Id = Convert.ToInt32(responseToken);
                postappverification.IsEWS_IADocVerifiedEdit = Convert.ToBoolean(jsonData.IsEWS_IADocVerified);
                postappverification.ApprovedByFaculty = Convert.ToInt32(responseToken);
                //int UserId = Convert.ToInt32(jsonData.ModifiedBy);

                SqlCommand cmd = new SqlCommand("PostAdmRegiEWS_IADocVerified", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ApplicantRegId", postappverification.ApplicantRegId);
                cmd.Parameters.AddWithValue("@IsEWS_IADocVerified", postappverification.IsEWS_IADocVerifiedEdit);
                cmd.Parameters.AddWithValue("@ApprovedByFaculty", postappverification.ApprovedByFaculty);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

                return Return.returnHttp("200", strMessage, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }

        #endregion

        #region PreVerificationUpdateFacultyMainSubmit    
        [HttpPost]
        public HttpResponseMessage PreVerificationUpdateFacultyMainSubmit(JObject jsonobject)

        {
            PostApplicantVerification postappverification = new PostApplicantVerification();
            dynamic jsonData = jsonobject;

            try
            {
                postappverification.Id = jsonData.Id;
                postappverification.ApplicantRegId = jsonData.ApplicantRegId;
                postappverification.ProgrammeInstancePartTermId = jsonData.ProgrammeInstancePartTermId;
                postappverification.VerificationStatus = jsonData.VerificationStatus;
                postappverification.AcademicYearId = jsonData.AcademicYearId;
                //postappverification.IsDualAdmission = Convert.ToBoolean(jsonData.IsDualAdmission);


                string token = Request.Headers.GetValues("token").FirstOrDefault();
                TokenOperation tokenOperation = new TokenOperation();
                string responseToken = tokenOperation.ValidateToken(token);
                if (responseToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }

                SqlCommand cmd = new SqlCommand("PreVerificationUpdateFacultyMainSubmit", con);
                cmd.CommandType = CommandType.StoredProcedure;
                postappverification.VerifiedByFaculty = Convert.ToInt32(responseToken);


                cmd.Parameters.AddWithValue("@Id", postappverification.Id);
                cmd.Parameters.AddWithValue("@ApplicantRegId", postappverification.ApplicantRegId);
                cmd.Parameters.AddWithValue("@ProgrammeInstancePartTermId", postappverification.ProgrammeInstancePartTermId);
                cmd.Parameters.AddWithValue("@VerifiedByFaculty", postappverification.VerifiedByFaculty);
                cmd.Parameters.AddWithValue("@VerificationStatus", postappverification.VerificationStatus);
                cmd.Parameters.AddWithValue("@AcademicYearId", postappverification.AcademicYearId);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Message"].Direction = ParameterDirection.Output;


                con.Open();
                cmd.ExecuteNonQuery();
                string strMessage = Convert.ToString(cmd.Parameters["@Message"].Value);
                con.Close();

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
