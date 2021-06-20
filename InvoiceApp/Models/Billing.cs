using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InvoiceApp.Models
{
    public class Billing
    {
        public long Id { get; set; }

        public string ContactName { get; set; }

        public string CompanyName { get; set; }

        public string Email  { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }
    }
}