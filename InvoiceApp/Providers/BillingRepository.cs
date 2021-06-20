using System.Threading.Tasks;
using InvoiceApp.Interfaces;
using InvoiceApp.Models;

namespace InvoiceApp.Providers
{
    public class BillingRepository : IBillingRepository
    {
        private readonly InvoiceContext _context;

        public BillingRepository(InvoiceContext context)
        {
            _context = context;
        }
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Billing> AddBill(Billing billing)
        {
            var billAdd = await _context.Billings.AddAsync(billing);
            await _context.SaveChangesAsync();
            return billAdd.Entity;
        }
    }
}