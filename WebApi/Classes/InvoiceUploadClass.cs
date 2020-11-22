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
        private DataValidation validation = new DataValidation();
        public UploadResult SaveFile(HttpContext Request)
        {
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
                            if ((validation.IsPassedExtensionFile(fileSavePath)) && (validation.IsPassedFileSize(fileSavePath)))
                            {
                                httpPostedFile.SaveAs(fileSavePath);
                                var transaction = GetDataFromFile(fileSavePath, Path.GetExtension(fileSavePath));
                                return new UploadResult() { isSuccess = true, InvoiceDataTransaction = transaction };
                            }
                            else
                            {
                                return new UploadResult() { isSuccess = false };
                            }
                        }
                        else
                        {
                            return new UploadResult() { isSuccess = false };
                        }
                    }
                }
                else
                {
                    return new UploadResult() { isSuccess = false };
                }
            }
            catch (Exception)
            {
                return new UploadResult() { isSuccess = false };
            }
            return new UploadResult() { isSuccess = false };
        }

        private List<InvoiceDataTransaction> GetDataFromFile(string path, string extension)
        {
            var transaction = new List<InvoiceDataTransaction>();

            switch (extension)
            {
                case ".CSV":
                    transaction = GetInvoiceTransactionFromCsvFile(path);
                    break;
                case ".XML":
                    break;
            }

            return transaction;
        }
        private List<InvoiceDataTransaction> GetInvoiceTransactionFromCsvFile(string path)
        {
            var transaction = new List<InvoiceDataTransaction>();
            string[] lines = System.IO.File.ReadAllLines(path);
            foreach (string line in lines)
            {
                string[] data = line.Split(',');
                if (data.Count() == 5)
                {
                    var items = new InvoiceDataTransaction();
                    items.TransactionId = validation.GetTransactionId(data[0]);
                    items.Amount = validation.GetAmount(data[1]);
                    items.CurrencyCode = validation.GetCurrencyCode(data[2]);
                    items.TransactionDate = validation.GetTransactionDate(data[3]);
                    items.Status = validation.GetStatusCode(data[4], ".CSV");
                }
            }
            return transaction;
        }
    }

    public class UploadResult
    {
        public bool isSuccess { get; set; }
        public HttpResponseMessage ResponseMessage { get; set; }
        public List<InvoiceDataTransaction> InvoiceDataTransaction { get; set; }
    }
}