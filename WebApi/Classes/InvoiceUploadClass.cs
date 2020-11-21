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
using WebApi.Models;

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
                            if ((IsPassedExtensionFile(fileSavePath)) && (IsPassedFileSize(fileSavePath)))
                            {
                                httpPostedFile.SaveAs(fileSavePath);
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return isSuccess;
        }
        private bool IsPassedExtensionFile(string path)
        {
            string extension = Path.GetExtension(path);
            if ((extension.ToUpper().Equals(".CSV")) || (extension.ToUpper().Equals(".XML")))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool IsPassedFileSize(string path)
        {
            FileInfo files = new FileInfo(path);
            long size = files.Length;
            if (files.Length > 1000000)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        private List<InvoiceDataTransaction> GetDataFromFile()
        {
            var transaction = new List<InvoiceDataTransaction>();

            return transaction;
        }
    }
}