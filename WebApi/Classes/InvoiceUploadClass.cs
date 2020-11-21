using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Hosting;
using System.Web.Http;

namespace WebApi.Classes
{
    public class InvoiceUploadClass
    {
        public bool SaveFile(HttpContext Request)
        {
            bool isSuccess = false;
            try
            {
                if (Request.Request.Files.Count > 0)
                {
                    for (int i = 0; i < Request.Request.Files.Count; i++)
                    {
                        HttpPostedFile httpPostedFile = Request.Request.Files[i];
                        if (httpPostedFile != null)
                        {
                            var fileSavePath = Path.Combine(HostingEnvironment.MapPath(ConfigurationManager.AppSettings["fileUpload"]), httpPostedFile.FileName);
                            httpPostedFile.SaveAs(fileSavePath);
                            isSuccess = true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                isSuccess = false;
            }
            return isSuccess;
        }
    }
}