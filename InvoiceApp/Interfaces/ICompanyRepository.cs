using System.Threading.Tasks;
using InvoiceApp.Models;

namespace InvoiceApp.Interfaces
{
    public interface ICompanyRepository : ICommonRepository
    {
        Task<Company> AddCompany(Company company);
    }
}