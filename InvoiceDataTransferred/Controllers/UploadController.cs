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
        public JsonResult UploadFiles(HttpPostedFileBase[] files)
        {
            using (var client = new HttpClient())
            {
                using (var formData = new MultipartFormDataContent())
                {
                    foreach (var file in files)
                    {
                        byte[] fileData;
                        using (var reader = new BinaryReader(file.InputStream))
                        {
                            fileData = reader.ReadBytes(file.ContentLength);
                        }

                        formData.Add(new StreamContent(new MemoryStream(fileData)), "files", file.FileName);
                    }
                    var response = client.PostAsync("https://localhost:44398/api/Invoice", formData).Result;
                    return Json(response.StatusCode, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}