using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models;
using WebApi.DAL;

namespace WebApi.Classes
{
    public class InquiryInvoiceData
    {
        private InvoiceRepository repos = new InvoiceRepository();
        public List<InvoiceDataTransaction> GetInvoiceTransaction(string criteria, string val)
        {
            var result = new List<InvoiceDataTransaction>();

            switch (criteria)
            {
                case "Currency":
                    result = repos.GetInvoiceByCurrencyCode(val);
                    break;
                case "Date Range":
                    break;
                case "Status":
                    break;
            }
            return result;
        }
    }
}