using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Configuration;

namespace InvoiceDataTransferred.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UploadFiles()
        {
            var httpContext = HttpContext.Request;

            using (var client = new HttpClient())
            {
                using (var formData = new MultipartFormDataContent())
                {
                    if (httpContext.Files.Count > 0)
                    {
                        byte[] fileData;
                        using (var reader = new BinaryReader(httpContext.Files[0].InputStream))
                        {
                            fileData = reader.ReadBytes(httpContext.Files[0].ContentLength);
                        }

                        formData.Add(new StreamContent(new MemoryStream(fileData)), "files", httpContext.Files[0].FileName);
                        var response = client.PostAsync("https://localhost:44398/api/Invoice", formData).Result;
                        return Json(response.StatusCode, JsonRequestBehavior.AllowGet);
                    } else
                    {
                        return Json(HttpStatusCode.NotFound, JsonRequestBehavior.AllowGet);
                    }
                }
            }
        }
    }
}