using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models;
using WebApi.DAL;
using System.Globalization;

namespace WebApi.Classes
{
    public class InquiryInvoiceData
    {
        private InvoiceRepository repos = new InvoiceRepository();
        public List<InvoiceDataOutput> GetInvoiceTransaction(string criteria, string keyword, string startDate, string endDate)
        {
            var result = new List<InvoiceDataOutput>();
            var transaction = new List<InvoiceDataTransaction>();
            switch (criteria)
            {
                case "Currency":
                    transaction = repos.GetInvoiceByCurrencyCode(keyword);
                    break;
                case "Date Range":
                    CultureInfo culture = new CultureInfo("en-US");
                    DateTime outputStartDate = Convert.ToDateTime(startDate, culture);
                    DateTime outputEndDate = Convert.ToDateTime(endDate, culture);
                    transaction = repos.GetInvoiceByDateRange(outputStartDate, outputEndDate);
                    break;
                case "Status":
                    transaction = repos.GetInvoiceByStatus(keyword);
                    break;
            }

            foreach (var items in transaction)
            {
                var outputItems = new InvoiceDataOutput();
                outputItems.id = items.TransactionId;
                outputItems.payment = string.Format("{0} {1}", items.Amount, items.CurrencyCode);

                var statusMapped = repos.GetStatusMapping(items.Status);
                if (statusMapped != null)
                    outputItems.Status = statusMapped.Status;
                else
                    outputItems.Status = string.Empty;

                result.Add(outputItems);
            }
            return result;
        }
    }
}