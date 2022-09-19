using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MSUISApi.Controllers
{
    public class FileUploadController : ApiController
    {
        public HttpResponseMessage returnHTTP(Object obj, int a)
        {
            try
            {
                var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string json = javaScriptSerializer.Serialize(obj);
                if (a == 1)
                {
                    return new HttpResponseMessage()
                    {
                        Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json"),
                        StatusCode = System.Net.HttpStatusCode.OK
                    };
                }
                else
                {
                    return new HttpResponseMessage()
                    {
                        Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json"),
                        StatusCode = System.Net.HttpStatusCode.InternalServerError
                    };
                }
            }
            catch
            {
                var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string json = javaScriptSerializer.Serialize("in catch");
                return new HttpResponseMessage()
                {
                    Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
                };
            }
        }

        [HttpPost]
        public HttpResponseMessage UploadFiles()
        {
            int checkpoint = 1;
            try
            {
                int iUploadedCnt = 0;
                string sPath = "";
                sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/uploads/");
                System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
                checkpoint = 2;
                string fileName = "";
                for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
                {
                    checkpoint = 3;
                    System.Web.HttpPostedFile hpf = hfc[iCnt];
                    fileName = hpf.FileName;
                    if (hpf.ContentLength > 0)
                    {
                        while (File.Exists(sPath + Path.GetFileName(fileName)))
                        {
                            fileName = fileName.Substring(0, fileName.LastIndexOf(".")) + "1" + fileName.Substring(fileName.LastIndexOf("."));
                        }
                        checkpoint = 4;
                        if (!File.Exists(sPath + Path.GetFileName(fileName)))
                        {
                            checkpoint = 5;
                            hpf.SaveAs(sPath + Path.GetFileName(fileName));
                            iUploadedCnt = iUploadedCnt + 1;
                        }
                    }
                }
                if (iUploadedCnt > 0)
                {
                    return returnHTTP("200:" + fileName, 1);
                }
                else
                {
                    return returnHTTP("201", 2);
                }
            }
            catch (Exception e)
            {
                return returnHTTP("201", 2);
            }
        }

        [HttpPost]
        public HttpResponseMessage UploadGifFiles()
        {
            int checkpoint = 1;
            try
            {
                int iUploadedCnt = 0;
                string sPath = "";
                sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/gif/");
                System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
                checkpoint = 2;
                string fileName = "";
                for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
                {
                    checkpoint = 3;
                    System.Web.HttpPostedFile hpf = hfc[iCnt];
                    fileName = hpf.FileName;
                    if (hpf.ContentLength > 0)
                    {
                        while (File.Exists(sPath + Path.GetFileName(fileName)))
                        {
                            fileName = fileName.Substring(0, fileName.LastIndexOf(".")) + "1" + fileName.Substring(fileName.LastIndexOf("."));
                        }
                        checkpoint = 4;
                        if (!File.Exists(sPath + Path.GetFileName(fileName)))
                        {
                            checkpoint = 5;
                            hpf.SaveAs(sPath + Path.GetFileName(fileName));
                            iUploadedCnt = iUploadedCnt + 1;
                        }
                    }
                }
                if (iUploadedCnt > 0)
                {
                    return returnHTTP("200:" + fileName, 1);
                }
                else
                {
                    return returnHTTP("201", 2);
                }
            }
            catch (Exception e)
            {
                return returnHTTP("201", 2);
            }
        }

        [HttpPost]
        public HttpResponseMessage UploadImage()
        {
            int checkpoint = 1;
            try
            {
                int iUploadedCnt = 0;
                string sPath = "";
                sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/images/");
                System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
                checkpoint = 2;
                string fileName = "";
                for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
                {
                    checkpoint = 3;
                    System.Web.HttpPostedFile hpf = hfc[iCnt];
                    fileName = hpf.FileName;
                    if (hpf.ContentLength > 0)
                    {
                        while (File.Exists(sPath + Path.GetFileName(fileName)))
                        {
                            fileName = fileName.Substring(0, fileName.LastIndexOf(".")) + "1" + fileName.Substring(fileName.LastIndexOf("."));
                        }
                        checkpoint = 4;
                        if (!File.Exists(sPath + Path.GetFileName(fileName)))
                        {
                            checkpoint = 5;
                            hpf.SaveAs(sPath + Path.GetFileName(fileName));
                            iUploadedCnt = iUploadedCnt + 1;
                        }
                    }
                }
                if (iUploadedCnt > 0)
                {
                    return returnHTTP("200:" + fileName, 1);
                }
                else
                {
                    return returnHTTP("201", 2);
                }
            }
            catch (Exception e)
            {
                return returnHTTP("201", 2);
            }
        }

        [HttpPost]
        public HttpResponseMessage UploadIcon()
        {
            int checkpoint = 1;
            try
            {
                int iUploadedCnt = 0;
                string sPath = "";
                sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/images/icons/");
                System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
                checkpoint = 2;
                string fileName = "";
                for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
                {
                    checkpoint = 3;
                    System.Web.HttpPostedFile hpf = hfc[iCnt];
                    fileName = hpf.FileName;
                    if (hpf.ContentLength > 0)
                    {
                        while (File.Exists(sPath + Path.GetFileName(fileName)))
                        {
                            fileName = fileName.Substring(0, fileName.LastIndexOf(".")) + "1" + fileName.Substring(fileName.LastIndexOf("."));
                        }
                        checkpoint = 4;
                        if (!File.Exists(sPath + Path.GetFileName(fileName)))
                        {
                            checkpoint = 5;
                            hpf.SaveAs(sPath + Path.GetFileName(fileName));
                            iUploadedCnt = iUploadedCnt + 1;
                        }
                    }
                }
                if (iUploadedCnt > 0)
                {
                    return returnHTTP("200:" + fileName, 1);
                }
                else
                {
                    return returnHTTP("201", 2);
                }
            }
            catch (Exception e)
            {
                return returnHTTP("201", 2);
            }
        }

        [HttpPost]
        public HttpResponseMessage UploadImages()
        {
            int checkpoint = 1;
            try
            {
                int iUploadedCnt = 0;
                string sPath = "";
                sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/images/");
                System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
                checkpoint = 2;
                string fileName = "";

                List<String> files = new List<String>();

                for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
                {
                    checkpoint = 3;
                    System.Web.HttpPostedFile hpf = hfc[iCnt];
                    fileName = hpf.FileName;
                    if (hpf.ContentLength > 0)
                    {
                        while (File.Exists(sPath + Path.GetFileName(fileName)))
                        {
                            fileName = fileName.Substring(0, fileName.LastIndexOf(".")) + "1" + fileName.Substring(fileName.LastIndexOf("."));
                        }
                        checkpoint = 4;
                        if (!File.Exists(sPath + Path.GetFileName(fileName)))
                        {
                            checkpoint = 5;
                            hpf.SaveAs(sPath + Path.GetFileName(fileName));
                            iUploadedCnt = iUploadedCnt + 1;
                            files.Add(fileName);
                        }
                    }
                }
                if (iUploadedCnt > 0)
                {
                    return returnHTTP(files, 1);
                }
                else
                {
                    return returnHTTP("201:" + checkpoint, 2);
                }
            }
            catch (Exception e)
            {
                return returnHTTP("203" + e.Message + e.StackTrace, 2);
            }
        }


        [HttpPost]
        public HttpResponseMessage UploadDocs()
        {
            int checkpoint = 1;
            try
            {
                int iUploadedCnt = 0;
                string sPath = "";
                sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/docs/");
                System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
                checkpoint = 2;
                string fileName = "";

                List<String> files = new List<String>();

                for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
                {
                    checkpoint = 3;
                    System.Web.HttpPostedFile hpf = hfc[iCnt];
                    fileName = hpf.FileName;
                    if (hpf.ContentLength > 0)
                    {
                        while (File.Exists(sPath + Path.GetFileName(fileName)))
                        {
                            fileName = fileName.Substring(0, fileName.LastIndexOf(".")) + "1" + fileName.Substring(fileName.LastIndexOf("."));
                        }
                        checkpoint = 4;
                        if (!File.Exists(sPath + Path.GetFileName(fileName)))
                        {
                            checkpoint = 5;
                            hpf.SaveAs(sPath + Path.GetFileName(fileName));
                            iUploadedCnt = iUploadedCnt + 1;
                            files.Add(fileName);
                        }
                    }
                }
                if (iUploadedCnt > 0)
                {
                    return returnHTTP(files, 1);
                }
                else
                {
                    return returnHTTP("201:" + checkpoint, 2);
                }
            }
            catch (Exception e)
            {
                return returnHTTP("203" + e.Message + e.StackTrace, 2);
            }
        }
    }

}
