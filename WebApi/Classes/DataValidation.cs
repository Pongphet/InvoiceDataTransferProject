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
using WebApi.DAL;
using System.Globalization;

namespace WebApi.Classes
{
    public class DataValidation
    {
        private InvoiceRepository repos = new InvoiceRepository();
        public bool IsPassedExtensionFile(string path)
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
        public bool IsPassedFileSize(string path)
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
        public bool IsPassedValidateTransactionId(string transactionId)
        {
            if (transactionId != null)
            {
                if (transactionId.Length > 50)
                    return false;
                else
                    return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsPassedValidateAmount(string amount)
        {
            if (amount != null)
            {
                decimal value;
                if (Decimal.TryParse(amount, out value))
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }

        }
        public bool IsPassedCurrencyCode(string code)
        {
            if (code != null)
            {
                var master = repos.GetCurrencyCodeByCode(code);
                if (master != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public bool IsPassedTransactionDate(string dateStr)
        {
            if (dateStr != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsPassedStatus(string status, string fileTypes)
        {
            if (status != null)
            {
                var master = new StatusMappings();
                if (fileTypes.Equals(".CSV"))
                {
                    master = repos.GetStatusCsvFile(status);
                }
                else if (fileTypes.Equals(".XML"))
                {
                    master = repos.GetStatusXmlFile(status);
                }
                else
                {
                    return false;
                }

                if (master != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public string GetTransactionId(string transactionId)
        {
            if (IsPassedValidateTransactionId(transactionId))
                return transactionId;
            else
                return string.Empty;
        }
        public decimal GetAmount(string amount)
        {
            if (IsPassedValidateAmount(amount))
                return Convert.ToDecimal(amount);
            else
                return 0;
        }
        public string GetCurrencyCode(string code)
        {
            if (IsPassedCurrencyCode(code))
                return code;
            else
                return string.Empty;
        }
        public DateTime GetTransactionDate(string dateStr)
        {
            string formatCsv = "dd/MM/yyyy HH:mm:ss";
            string formatXml = "yyyy-MM-ddTHH:mm:ss";
            String culture = "en-CA";

            if (IsPassedTransactionDate(dateStr))
            {          
                DateTime output;
                if (DateTime.TryParseExact(dateStr, formatCsv, CultureInfo.CreateSpecificCulture(culture),
                    DateTimeStyles.None, out output))
                {
                    return output;
                } else if (DateTime.TryParseExact(dateStr, formatXml, CultureInfo.CreateSpecificCulture(culture),
                    DateTimeStyles.None, out output))
                {
                    return output;
                }
            }
            return new DateTime();
        }
        public string GetStatusCode(string status, string fileTypes)
        {
            if (IsPassedStatus(status, fileTypes))
                return status;
            else
                return string.Empty;
        }
    }
}