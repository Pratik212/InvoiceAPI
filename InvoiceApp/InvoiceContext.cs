using InvoiceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp
{
    public class InvoiceContext : DbContext
    {
        public InvoiceContext(DbContextOptions<InvoiceContext> options) : base(options)
        {
        }
        
        public virtual DbSet<Company> Companies { get; set; }
        
        public virtual DbSet<Billing>Billings { get; set; }
        
        public virtual DbSet<Shipping>Shippings { get; set; }
    }
}