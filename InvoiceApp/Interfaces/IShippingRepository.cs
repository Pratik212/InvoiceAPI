using System.Collections.Generic;
using System.Threading.Tasks;
using InvoiceApp.Models;

namespace InvoiceApp.Interfaces
{
    public interface IShippingRepository : ICommonRepository
    {
        Task<Shipping> Add(Shipping shipping);

        Task<IEnumerable<Shipping>> GetAll();

        Task<Shipping> GetById(long id);
        
    }
}