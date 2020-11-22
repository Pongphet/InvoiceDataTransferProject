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
    public class DataValidation
    {
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
        //public bool IsPassedCurrencyCode(string code)
        //{

        //}
        //public bool IsPassedTransactionDate(string dateStr)
        //{

        //}
        //public bool IsPassedStatus(string status)
        //{

        //}
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
    }
}