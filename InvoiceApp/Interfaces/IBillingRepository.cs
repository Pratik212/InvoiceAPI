using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using InvoiceApp.Models;

namespace InvoiceApp.Interfaces
{
    public interface IBillingRepository : ICommonRepository
    {
        Task<Billing> AddBill(Billing billing);

        Task<IEnumerable<Billing>> GetAll();

        Task<Billing> GetBillingById(long id);
    }
}