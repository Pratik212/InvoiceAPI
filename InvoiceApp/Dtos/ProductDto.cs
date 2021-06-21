using InvoiceApp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InvoiceApp.Dtos
{
    public class ProductDto
    {
        public string Description { get; set; }

        public int Qty { get; set; }

        public double UnitPrice { get; set; }

        public double Total { get; set; }

        public double SubTotal { get; set; }
    }
}