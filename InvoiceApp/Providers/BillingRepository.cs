using System.Collections.Generic;
using System.Threading.Tasks;
using InvoiceApp.Interfaces;
using InvoiceApp.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<Billing>> GetAll()
        {
            return await _context.Billings.ToListAsync();
        }

        public async Task<Billing> GetBillingById(long id)
        {
            var billing = await _context.Billings.FirstOrDefaultAsync(x => x.Id == id);

            return billing;
        }
    }
}