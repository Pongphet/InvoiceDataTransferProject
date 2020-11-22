using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models;
using WebApi.Data;

namespace WebApi.DAL
{
    public class InvoiceRepository
    {
        private DatabaseContext context = new DatabaseContext();

        public InvoiceDataTransaction GetInvoiceTransactionById(string transactionId)
        {
            var result = context.InvoiceDataTransaction.FirstOrDefault(
                x => x.TransactionId.Equals(transactionId));
            return result;
        }
        public List<InvoiceDataTransaction> GetInvoiceByCurrencyCode(string code)
        {
            var result = context.InvoiceDataTransaction.Where(x => x.CurrencyCode.Equals(code)).ToList();
            return result;
        }
        public List<InvoiceDataTransaction> GetInvoiceByDateRange(DateTime startDate, DateTime endDate)
        {
            var result = context.InvoiceDataTransaction.Where(x =>
            x.TransactionDate >= startDate &&
            x.TransactionDate <= endDate).ToList();
            return result;
        }
        public List<InvoiceDataTransaction> GetInvoiceByStatus(string status)
        {
            var result = context.InvoiceDataTransaction.Where(x => x.Status.Equals(status)).ToList();
            return result;
        }
        public void InsertInvoiceTransaction(InvoiceDataTransaction entity)
        {
            var invoice = context.Set<InvoiceDataTransaction>();
            invoice.Add(entity);
            context.SaveChanges();
        }

        public void InsertStatusMapping(StatusMappings entity)
        {
            var status = context.Set<StatusMappings>();
            status.Add(entity);
            context.SaveChanges();
        }
        public void InsertCurrencyCode(CurrencyCode entity)
        {
            var currencyCodes = context.Set<CurrencyCode>();
            currencyCodes.Add(entity);
            context.SaveChanges();
        }
        public CurrencyCode GetCurrencyCodeByCode(string code)
        {
            var result = context.CurrencyCode.FirstOrDefault(x => x.Code.Equals(code));
            return result;
        }
        public StatusMappings GetStatusMapping(string status)
        {
            var csvStatus = context.StatusMappings.FirstOrDefault(x => x.CsvStatus.Equals(status));
            if (csvStatus != null)
                return csvStatus;
            else
            {
                var xmlStatus = context.StatusMappings.FirstOrDefault(x => x.XmlStatus.Equals(status));
                if (xmlStatus != null)
                    return xmlStatus;
                else
                    return null;
            }    
        }
        public StatusMappings GetStatusCsvFile(string status)
        {
            var result = context.StatusMappings.FirstOrDefault(x => x.CsvStatus.Equals(status));
            return result;
        }
        public StatusMappings GetStatusXmlFile(string status)
        {
            var result = context.StatusMappings.FirstOrDefault(x => x.XmlStatus.Equals(status));
            return result;
        }
    }
}