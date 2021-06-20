using System.Threading.Tasks;
using InvoiceApp.Models;

namespace InvoiceApp.Interfaces
{
    public interface IBillingRepository : ICommonRepository
    {
        Task<Billing> AddBill(Billing billing);
    }
}