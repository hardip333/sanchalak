using iTextSharp.text;
using iTextSharp.text.pdf;
using MSUIS_TokenManager.App_Start;
using MSUISApi.BAL;
using MSUISApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MSUISApi.Controllers
{
    public class SecBarcodeController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage getPaperListForSecBarcode(SecBarcode dataString)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                string resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int64 UserId = Convert.ToInt64(res.ToString());

                int? ExamMasterId = dataString.ExamMasterId, ProgrammePartTermId = dataString.ProgrammePartTermId, TeachingLeaningMapId = dataString.TeachingLearningMethodId, AssessmentMethodId = dataString.AssessmentMethodId;

                if (string.IsNullOrEmpty(ExamMasterId.ToString()))
                {
                    return Return.returnHttp("201", "Please select Exam Event.", null);
                }
                if (string.IsNullOrEmpty(ProgrammePartTermId.ToString()))
                {
                    return Return.returnHttp("201", "Please select Programme Part Term.", null);
                }
                if (string.IsNullOrEmpty(TeachingLeaningMapId.ToString()))
                {
                    return Return.returnHttp("201", "Please select Teaching Learning Method.", null);
                }
                if (string.IsNullOrEmpty(AssessmentMethodId.ToString()))
                {
                    return Return.returnHttp("201", "Please select Assessment Method.", null);
                }
                SecBarcode func = new SecBarcode();
                dataString = func.getPaperListForSecBarcode(dataString);

                return Return.returnHttp("200", dataString, null);
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
        [HttpPost]
        public HttpResponseMessage generateSecBarcode(SecBarcode dataString)
        {
            try
            {
                String token = Request.Headers.GetValues("token").FirstOrDefault();
                String facultyDepartIntituteId = Request.Headers.GetValues("facultyDepartIntituteId").FirstOrDefault();
                String userRoleToken = Request.Headers.GetValues("userRoleToken").FirstOrDefault();
                TokenOperation ac = new TokenOperation();
                string res = ac.ValidateToken(token);
                string resRoleToken = ac.ValidateRoleToken(userRoleToken);

                if (res == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                else if (resRoleToken == "0")
                {
                    return Return.returnHttp("0", null, null);
                }
                Int64 UserId = Convert.ToInt64(res.ToString());

                if (String.IsNullOrEmpty(dataString.ExamMasterId.ToString()))
                {
                    return Return.returnHttp("201", " Please select exam event ", null);
                }
                else if (String.IsNullOrEmpty(dataString.ProgrammePartTermId.ToString()))
                {
                    return Return.returnHttp("201", " Please select programme part term ", null);
                }
                else if (String.IsNullOrEmpty(dataString.BranchId.ToString()))
                {
                    return Return.returnHttp("201", " Please select Branch ", null);
                }
                /*else if (String.IsNullOrEmpty(dataString.MstPaperId.ToString()))
                {
                    return Return.returnHttp("201", " Please select paper ", null);
                }*/
                else
                {
                    SecBarcode func = new SecBarcode();
                    string output = func.GenerateSecBarcode(dataString);
                    
                    if (string.IsNullOrEmpty(output))
                    {
                        return Return.returnHttp("201", " Some Internal issue occured. ", null);
                    }
                    else if (output.StartsWith("Error"))
                    {
                        return Return.returnHttp("201", output, null);
                    }
                    else if (output == "No Record Found")
                    {
                        return Return.returnHttp("200", "No Record Found", null);
                    }
                    else if (output.Contains('/'))
                    {
                        return Return.returnHttp("200", output, null);
                    }

                    else
                    {
                        return Return.returnHttp("201", output, null);
                    }
                }
            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }        
    }
}
