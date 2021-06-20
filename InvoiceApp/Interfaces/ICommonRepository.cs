using System.Threading.Tasks;

namespace InvoiceApp.Interfaces
{
    public interface ICommonRepository
    {
        Task SaveChanges();
    }
}