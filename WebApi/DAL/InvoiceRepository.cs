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

        public CurrencyCode GetCurrencyCodeByCode(string code)
        {
            var result = context.CurrencyCode.FirstOrDefault(x => x.Code.Equals(code));
            return result;
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