using System.Collections.Generic;
using System.Threading.Tasks;
using InvoiceApp.Interfaces;
using InvoiceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.Providers
{
    public class ShippingRepository : IShippingRepository
    {
        private readonly InvoiceContext _context;

        public ShippingRepository(InvoiceContext context)
        {
            _context = context;
        }
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Shipping> Add(Shipping shipping)
        {
            var shipAdd = await _context.Shippings.AddAsync(shipping);
            await _context.SaveChangesAsync();
            return shipAdd.Entity;
        }

        public async Task<IEnumerable<Shipping>> GetAll()
        {
            return await _context.Shippings.ToListAsync();
        }

        public async Task<Shipping> GetById(long id)
        {
            var shipAdd = await _context.Shippings.FirstOrDefaultAsync(x => x.Id == id);
            return shipAdd;
        }
    }
}