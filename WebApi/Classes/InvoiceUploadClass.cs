using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Web.Hosting;
using System.Web.Http;
using System.Data;
using Microsoft.VisualBasic.FileIO;
using WebApi.Models;
using WebApi.DAL;

namespace WebApi.Classes
{
    public class InvoiceUploadClass
    {
        private InvoiceRepository repos = new InvoiceRepository();
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
                            httpPostedFile.SaveAs(fileSavePath);

                            if ((validation.IsPassedExtensionFile(fileSavePath)) && (validation.IsPassedFileSize(fileSavePath)))
                            {
                                var transaction = GetDataFromFile(fileSavePath, Path.GetExtension(fileSavePath).ToUpper());
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
        public void SubmitInvoiceTransaction(List<InvoiceDataTransaction> transaction)
        {
            foreach (var items in transaction)
            {
                if (repos.GetInvoiceTransactionById(items.TransactionId) == null)
                {
                    repos.InsertInvoiceTransaction(items);
                }
            }
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
            TextFieldParser parser = new TextFieldParser(path);
            parser.HasFieldsEnclosedInQuotes = true;
            parser.SetDelimiters(",");

            string[] fields;
            while (!parser.EndOfData)
            {
                fields = parser.ReadFields();
                if (fields.Count() == 5)
                {
                    var items = new InvoiceDataTransaction();
                    items.TransactionId = validation.GetTransactionId(fields[0].Trim()) ;
                    items.Amount = validation.GetAmount(fields[1].Trim());
                    items.CurrencyCode = validation.GetCurrencyCode(fields[2].ToUpper().Trim());
                    items.TransactionDate = validation.GetTransactionDate(fields[3].Trim());
                    items.Status = validation.GetStatusCode(fields[4].Trim(), ".CSV");
                    transaction.Add(items);
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