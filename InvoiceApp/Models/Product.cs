namespace InvoiceApp.Models
{
    public class Product
    {
        public long Id { get; set; }

        public string Description { get; set; }

        public int Qty { get; set; }

        public double UnitPrice { get; set; }

        public double Total { get; set; }

        public double  SubTotal { get; set; }
    }
}