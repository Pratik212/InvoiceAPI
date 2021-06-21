using System;

namespace InvoiceApp.Models
{
    public class Invoice
    {
        public long Id { get; set; }

        public int InvoiceNo { get; set; }

        public DateTimeOffset InvoiceDate { get; set; } = DateTime.Now;

        public DateTimeOffset DueDate { get; set; }
    }
}