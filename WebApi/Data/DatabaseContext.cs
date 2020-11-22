using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApi.Models;

namespace WebApi.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("InvoiceContext")
        {
        }

        public DbSet<InvoiceDataTransaction> InvoiceDataTransaction { get; set; }
        public DbSet<CurrencyCode> CurrencyCode { get; set; }
        public DbSet<StatusMappings> StatusMappings { get; set; }
    }
}