using System;

namespace InvoiceApp.Dtos
{
    public class InvoiceDto
    {
        public int InvoiceNo { get; set; }

        public DateTimeOffset InvoiceDate { get; set; } = DateTime.Now;
    }
}