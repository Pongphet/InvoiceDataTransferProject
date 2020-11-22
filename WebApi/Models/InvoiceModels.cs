using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class InvoiceDataTransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(50)]
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        [MaxLength(5)]
        public string CurrencyCode { get; set; }
        public DateTime TransactionDate { get; set; }
        [MaxLength(10)]
        public string Status { get; set; }
    }

    public class CurrencyCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Country { get; set; }
        [MaxLength(50)]
        public string Currency { get; set; }
        [MaxLength(5)]
        public string Code { get; set; }
        public int Number { get; set; }
    }

    public class StatusMappings
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(10)]
        public string CsvStatus { get; set; }
        [MaxLength(10)]
        public string XmlStatus { get; set; }
        [MaxLength(5)]
        public string Status { get; set; }
    }
}