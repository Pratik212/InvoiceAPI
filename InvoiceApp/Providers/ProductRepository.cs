using System.Collections.Generic;
using System.Threading.Tasks;
using InvoiceApp.Interfaces;
using InvoiceApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace InvoiceApp.Providers
{
    public class ProductRepository : IProductRepository
    {
        private readonly InvoiceContext _context;

        public ProductRepository(InvoiceContext context)
        {
            _context = context;
        }
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Product> AddProduct(Product product)
        {
            var productData = await _context.Products.AddAsync(product);

            await _context.SaveChangesAsync();
            return productData.Entity;
        }

        public async Task<List<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductById(long id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            return product;
        }

        public async Task Delete(List<long> productIds)
        {
            var productObj = await _context.Products.Where(x => productIds.Contains(x.Id)).ToListAsync();
        
            foreach (var product in productObj) product.IsDeleted = true;
        
            await _context.SaveChangesAsync();
        }
    }
}