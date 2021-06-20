using System;

namespace InvoiceApp.Models
{
    public class Company
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTimeOffset CreatedAt { get; set; } = DateTime.Now;
        
        public DateTimeOffset UpdatedAt { get; set; } = DateTime.Now;
    }
}