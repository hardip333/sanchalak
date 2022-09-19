using MSUIS_TokenManager.App_Start;
using MSUISApi.BAL;
using MSUISApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MSUISApi.Controllers
{
    public class SectionwiseblankmarksheetController : ApiController
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MSUISConnectionString"].ConnectionString);
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        SqlDataAdapter sda = new SqlDataAdapter();

       
        [HttpPost]
        public HttpResponseMessage generateBlankMarkSheetSectionwise(Sectionwiseblankmarksheet dataString)
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
                else if (String.IsNullOrEmpty(dataString.PaperId.ToString()))
                {
                    return Return.returnHttp("201", " Please select paper ", null);
                }
                else
                {
                    Sectionwiseblankmarksheet1 func = new Sectionwiseblankmarksheet1();

                    string output = func.GenerateBlankMarksheet(dataString);
                    //string output = func.GenerateBulkHallTicketInOnePDF(dataString);

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

        

        [HttpPost]
        public HttpResponseMessage getPaperListForBlankMarksheet(BlankMarkSheets dataString)
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
                BALExamFormMaster func = new BALExamFormMaster();
                dataString = func.getPaperListForBlankMarksheetSectionwise(dataString);

                return Return.returnHttp("200", dataString, null);


            }
            catch (Exception e)
            {
                return Return.returnHttp("201", e.Message, null);
            }
        }
    }
}
