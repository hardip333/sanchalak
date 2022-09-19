using MSUISApi.BAL;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MSUISApi.Models
{
    public class BarcodePDF : IJob
    {
        //Staging Path
        //string BarcodePath = "C:/inetpub/wwwroot/MSUIS_AdminAPI/Upload/Barcode/PrimaryBarcode/";
        //string BarcodePath = "F:/Sanchalak/MSUIS_AdminAPI/Upload/Barcode/PrimaryBarcode/";


        //Live Path
        string BarcodePath = "F:/inetpub/wwwroot/MSUIS_AdminAPI/Upload/Barcode/PrimaryBarcode/";

        async Task IJob.Execute(IJobExecutionContext context)
        {
            BALExamFormBarcodePdfGenerationRequest func = new BALExamFormBarcodePdfGenerationRequest();
            
            ExamFormBarcodePdfGenerationRequest dataString = func.GetPendingandUnderProcessExamFormBarcodePDfRequest();
         
            while(dataString!=null)
            {
                string output = func.UpdateUnderProcessStatus(dataString);
                
                if (output.Equals("TRUE"))
                {
                    ExamFormBarcode func1 = new ExamFormBarcode();
                    output = func1.generatePDF(dataString.ExamMasterId,dataString.PaperIds,dataString.Id,dataString.SpecialisationIds);
                    if (output.Contains(".zip"))
                    {
                        if (File.Exists(BarcodePath + output))
                        {
                            dataString.ZipFile = output;
                            string output1 = func.updateExamFormBarcodePdfGenerationRequest(dataString);
                        }
                    }
                }
                dataString = func.GetPendingandUnderProcessExamFormBarcodePDfRequest();
            }

           
        }


        //Task IJob.Execute(IJobExecutionContext context)
        //{
        //    throw new NotImplementedException();
        //}
    }
}