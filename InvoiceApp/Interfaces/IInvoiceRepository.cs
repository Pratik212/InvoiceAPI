using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using InvoiceApp.Models;

namespace InvoiceApp.Interfaces
{
    public interface IInvoiceRepository : ICommonRepository
    {
        Task<Invoice> Add(Invoice invoice);
        
        Task<List<Invoice>> GetAll();

        Task<Invoice> GetInvoiceById(long id);
    }
}