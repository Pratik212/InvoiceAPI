using System.Collections.Generic;
using System.Threading.Tasks;
using InvoiceApp.Interfaces;
using InvoiceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.Providers
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly InvoiceContext _context;

        public InvoiceRepository(InvoiceContext context)
        {
            _context = context;
        }
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Invoice> Add(Invoice invoice)
        {
            var invoiceData = await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();
            return invoiceData.Entity;
        }

        public async Task<List<Invoice>> GetAll()
        {
            return await _context.Invoices.ToListAsync();
        }

        public async Task<Invoice> GetInvoiceById(long id)
        {
            var invoiceData = await _context.Invoices.FirstOrDefaultAsync(x => x.Id == id);

            return invoiceData;
        }
    }
}