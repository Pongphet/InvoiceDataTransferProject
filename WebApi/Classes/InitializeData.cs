using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models;
using WebApi.DAL;

namespace WebApi.Classes
{
    public class InitializeData
    {
        private InvoiceRepository repos = new InvoiceRepository();

        public void InitialStatusMappingData()
        {
            var master = repos.GetStatusMapping("Approved");
            if (master == null)
            {
                List<StatusMappings> statusMappings = new List<StatusMappings>();
                statusMappings.Add(new StatusMappings() { CsvStatus = "Approved", XmlStatus = "Approved", Status = "A" });
                statusMappings.Add(new StatusMappings() { CsvStatus = "Failed", XmlStatus = "Rejected", Status = "R" });
                statusMappings.Add(new StatusMappings() { CsvStatus = "Finished", XmlStatus = "Done", Status = "D" });

                foreach (var items in statusMappings)
                {
                    repos.InsertStatusMapping(items);
                }
            }
        }
        public void InitialCurrencyCodeData()
        {
            var master = repos.GetCurrencyCodeByCode("USD");
            if (master == null)
            {
                List<CurrencyCode> currencyCodes = new List<CurrencyCode>();
                currencyCodes.Add(new CurrencyCode() { Country = "AMERICAN SAMOA", Currency = "US Dollar", Code = "USD", Number = 840 });
                currencyCodes.Add(new CurrencyCode() { Country = "ÅLAND ISLANDS", Currency = "Euro", Code = "EUR", Number = 978 });

                foreach (var items in currencyCodes)
                {
                    repos.InsertCurrencyCode(items);
                }
            }
        }
    }
}