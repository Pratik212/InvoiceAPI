using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using InvoiceApp.Models;

namespace InvoiceApp.Interfaces
{
    public interface IProductRepository : ICommonRepository
    {
        Task<Product> AddProduct(Product product);

        Task<List<Product>> GetAll();
        Task<Product> GetProductById(long id);
        Task Delete(List<long> productIds);
    }
}